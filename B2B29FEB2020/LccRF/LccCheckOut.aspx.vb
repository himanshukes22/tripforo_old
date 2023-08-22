Imports System.Data
Partial Class LccRF_LccCheckOut
    Inherits System.Web.UI.Page

    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim objFareBreakup As New clsCalcCommAndPlb
    Dim objDA As New SqlTransaction
    '    Dim objSqlTrans As New SqlTransaction
    Dim DomAirDt As DataTable
    Dim trackId As String, LIND As String, LINR As String
    Dim OBTrackId As String, IBTrackId As String
    Dim FT As String = ""
    Dim Adult As Integer
    Dim Child As Integer
    Dim Infant As Integer
    Dim SelectedFltArray As Array
    Dim strFlt As String = "", strFare As String = ""
    Dim fareHashtbl, fareHashtblR As Hashtable
    Dim clsCorp As New ClsCorporate()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        Else
            If Not Page.IsPostBack Then


                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
                If ds Is Nothing Then

                Else
                    If ds.Tables(0).Rows.Count > 0 Then
                        DropDownListProject.Items.Clear()
                        Dim item As New ListItem("Select")
                        DropDownListProject.AppendDataBoundItems = True
                        DropDownListProject.Items.Insert(0, item)
                        DropDownListProject.DataSource = ds.Tables(0)
                        DropDownListProject.DataTextField = "ProjectName"
                        DropDownListProject.DataValueField = "ProjectId"
                        DropDownListProject.DataBind()
                        spn_Projects.Visible = True
                        spn_Projects1.Visible = True
                    Else
                        spn_Projects.Visible = False
                        spn_Projects1.Visible = False

                    End If

                End If


                Dim dsbooked As DataSet = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "BB")
                If dsbooked.Tables(0).Rows.Count > 0 Then
                    DropDownListBookedBy.AppendDataBoundItems = True
                    DropDownListBookedBy.Items.Clear()
                    DropDownListBookedBy.Items.Insert(0, "Select")
                    DropDownListBookedBy.DataSource = dsbooked
                    DropDownListBookedBy.DataTextField = "BOOKEDBY"
                    DropDownListBookedBy.DataValueField = "BOOKEDBY"
                    DropDownListBookedBy.DataBind()

                    Spn_BookedBy.Visible = True
                    Spn_BookedBy1.Visible = True
                Else
                    Spn_BookedBy.Visible = False
                    Spn_BookedBy1.Visible = False

                End If

                LIND = Request.QueryString("LinD")
                LINR = Request.QueryString("LinR")
                fareHashtbl = objFareBreakup.getRTFareDetails(LIND, "DEP")
                OBTrackId = objSelectedfltCls.getRndNum
                ViewState("OBTrackId") = OBTrackId
                objSelectedfltCls.InsertFlightData(OBTrackId, LIND, Session("depDV"), "", fareHashtbl("AdtTax").ToString, fareHashtbl("ChdTax").ToString, fareHashtbl("InfTax").ToString, fareHashtbl("SrvTax"), fareHashtbl("TFee"), fareHashtbl("TC"), fareHashtbl("adtTds"), fareHashtbl("chdTds"), fareHashtbl("adtComm"), fareHashtbl("chdComm"), fareHashtbl("adtCB"), fareHashtbl("chdCB"), fareHashtbl("totFare"), fareHashtbl("netFare"), Session("UID"))
                fareHashtblR = objFareBreakup.getRTFareDetails(LINR, "RET")
                IBTrackId = objSelectedfltCls.getRndNum
                ViewState("IBTrackId") = IBTrackId
                objSelectedfltCls.InsertFlightData(IBTrackId, LINR, Session("retDV"), "", fareHashtblR("AdtTax").ToString, fareHashtblR("ChdTax").ToString, fareHashtblR("InfTax").ToString, fareHashtblR("SrvTax"), fareHashtblR("TFee"), fareHashtblR("TC"), fareHashtblR("adtTds"), fareHashtblR("chdTds"), fareHashtblR("adtComm"), fareHashtblR("chdComm"), fareHashtblR("adtCB"), fareHashtblR("chdCB"), fareHashtblR("totFare"), fareHashtblR("netFare"), Session("UID"))


                Dim OBFltDs, IBFltDs As DataSet
                OBFltDs = objDA.GetFltDtlsInt(OBTrackId, Session("UID"))
                IBFltDs = objDA.GetFltDtlsInt(IBTrackId, Session("UID"))

                Adult = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                Child = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                Infant = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))

                lbl_adult.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                lbl_child.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                lbl_infant.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))
                divFltDtls.InnerHtml = showFltDetails(OBFltDs, IBFltDs, "InBound")

                divFareDtls.InnerHtml = "<b>OutBound :-</b><br/> <br/>" & fareBreakupfun(OBFltDs, "OutBound")

                divFareDtlsR.InnerHtml = "<b>InBound :-</b><br/> <br/>" & fareBreakupfun(IBFltDs, "InBound")

                Bind_pax(Adult, Child, Infant)
            End If
        End If
    End Sub

    Private Function showFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
        Try
            strFlt = "<b>OutBound :-</b><br/> <br/>"
            For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
                strFlt = strFlt & OBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & OBDS.Tables(0).Rows(i)("ArrivalLocation") & " Date : " & OBDS.Tables(0).Rows(i)("Departure_Date") & " " & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & "<br/>"
                strFlt = strFlt & "Dep : " & " " & Left(OBDS.Tables(0).Rows(i)("DepartureTime"), 5) & "Hrs. Arr : " & Left(OBDS.Tables(0).Rows(i)("ArrivalTime"), 5) & "Hrs. class : " & OBDS.Tables(0).Rows(i)("RBD") & "<br/><br/>"
            Next

            If FT = "InBound" Then
                strFlt = strFlt & "<b>InBound :-</b><br/> <br/>"
                For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
                    strFlt = strFlt & IBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & IBDS.Tables(0).Rows(i)("ArrivalLocation") & " Date : " & IBDS.Tables(0).Rows(i)("Departure_Date") & " " & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & "<br/>"
                    strFlt = strFlt & "Dep : " & " " & Left(IBDS.Tables(0).Rows(i)("DepartureTime"), 5) & "Hrs. Arr : " & Left(IBDS.Tables(0).Rows(i)("ArrivalTime"), 5) & "Hrs. class : " & IBDS.Tables(0).Rows(i)("RBD") & "<br/><br/>"
                Next
            End If
        Catch ex As Exception

        End Try

        Return strFlt
    End Function

    Private Function fareBreakupfun(ByVal OFareDS As DataSet, ByVal Ft As String) As String
        Try
            Dim tax(), tax1() As String, yq As Integer = 0, tx As Integer = 0
            tax = OFareDS.Tables(0).Rows(0)("Adt_Tax").ToString.Split("#")
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    yq = yq + Convert.ToInt32(tax1(1))
                Else
                    tax1 = tax(i).Split(":")
                    tx = tx + Convert.ToInt32(tax1(1))
                End If
            Next

            strFare = "<table width='250' border='0' cellspacing='2' cellpadding='2'>"
            strFare = strFare & "<tr>"
            strFare = strFare & "<td width='143' style='font-weight:bold;'>Adult Fare</td>"
            strFare = strFare & "<td width='93'>" & OFareDS.Tables(0).Rows(0)("AdtBFare") & "</td>"
            strFare = strFare & "</tr>"
            strFare = strFare & "<tr>"
            strFare = strFare & "<td style='font-weight:bold;'>Fuel Surcharge</td>"
            strFare = strFare & "<td>" & yq & "</td>"
            strFare = strFare & "</tr>"
            strFare = strFare & "<tr>"
            strFare = strFare & "<td style='font-weight:bold;'>Tax</td>"
            strFare = strFare & "<td>" & tx & "</td>"
            strFare = strFare & "</tr>"
            strFare = strFare & "<tr>"
            strFare = strFare & "<td style='font-weight:bold;'>Total</td>"
            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("AdtFare") & "</td>"
            strFare = strFare & "</tr>"
            strFare = strFare & "<tr>"
            strFare = strFare & "<td>&nbsp;</td>"
            strFare = strFare & "<td>&nbsp;</td>"
            strFare = strFare & "</tr>"

            If Child > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Chd_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(tax1(1))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(tax1(1))
                        End If
                    Next
                Catch ex As Exception
                End Try

                strFare = strFare & "<tr>"
                strFare = strFare & "<td width='106' style='font-weight:bold;'>Child Fare</td>"
                strFare = strFare & "<td width='130'>" & OFareDS.Tables(0).Rows(0)("ChdBFare") & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Fuel Surcharge </td>"
                strFare = strFare & "<td>" & yq & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Tax</td>"
                strFare = strFare & "<td>" & tx & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Total</td>"
                strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("ChdFare") & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td>&nbsp;</td>"
                strFare = strFare & "<td>&nbsp;</td>"
                strFare = strFare & "</tr>"
            End If

            If Infant > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Inf_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(tax1(1))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(tax1(1))
                        End If
                    Next
                Catch ex As Exception
                End Try

                strFare = strFare & "<tr>"
                strFare = strFare & "<td width='106' style='font-weight:bold;'>Infant Fare</td>"
                strFare = strFare & "<td width='130'>" & OFareDS.Tables(0).Rows(0)("InfBFare") & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Tax</td>"
                strFare = strFare & "<td>" & tx & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Total</td>"
                strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("InfFare") & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td>&nbsp;</td>"
                strFare = strFare & "<td>&nbsp;</td>"
                strFare = strFare & "</tr>"
            End If
            strFare = strFare & "<tr>"
            strFare = strFare & "<td width='106' style='font-weight:bold;'>Srv. Tax</td>"
            strFare = strFare & "<td width='130'>" & OFareDS.Tables(0).Rows(0)("SrvTax") & "</td>"
            strFare = strFare & "</tr>"

            If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Mgnt. Fee</td>"
                strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</td>"
                strFare = strFare & "</tr>"
            Else
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Tran. Fee</td>"
                strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("TFee") & "</td>"
                strFare = strFare & "</tr>"
                strFare = strFare & "<tr>"
                strFare = strFare & "<td style='font-weight:bold;'>Tran. Charge</td>"
                strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("TC") & "</td>"
                strFare = strFare & "</tr>"
            End If
            'strFare = strFare & "<tr>"
            'strFare = strFare & "<td style='font-weight:bold;'>Tran. Fee</td>"
            'strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("TFee") & "</td>"
            'strFare = strFare & "</tr>"
            'strFare = strFare & "<tr>"
            'strFare = strFare & "<td style='font-weight:bold;'>Tran. Charge</td>"
            'strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("TC") & "</td>"
            'strFare = strFare & "</tr>"
            If Ft = "OutBound" Then
                strFare = strFare & "<tr onmouseover=funcnetfare('block','trnetfare'); onmouseout=funcnetfare('none','trnetfare'); style='cursor:pointer;color: #004b91'>"
            ElseIf Ft = "InBound" Then
                strFare = strFare & "<tr onmouseover=funcnetfare('block','trnetfareR'); onmouseout=funcnetfare('none','trnetfareR'); style='cursor:pointer;color: #004b91'>"
            End If

            strFare = strFare & "<td><strong>Total Fare</strong><br/>(" & Adult & " Adt," & Child & " Chd," & Infant & " Inf)</td>"
            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("totFare") & "</td>"
            strFare = strFare & "</tr>"
            If Ft = "OutBound" Then
                strFare = strFare & "<tr id='trnetfare' style='display:none'>"
            ElseIf Ft = "InBound" Then
                strFare = strFare & "<tr id='trnetfareR' style='display:none'>"
            End If
            strFare = strFare & "<td style='font-weight:bold;'>Net Fare</td>"
            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("netFare") & "</td>"
            strFare = strFare & "</tr>"
            strFare = strFare & "</table>"
        Catch ex As Exception

        End Try

        Return strFare
    End Function

    Public Sub Bind_pax(ByVal cntAdult As Integer, ByVal cntChild As Integer, ByVal cntInfant As Integer)
        Try
            Dim PaxTbl As New DataTable()
            Dim cntTblColumn As DataColumn = Nothing
            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.Double")
            cntTblColumn.ColumnName = "Counter"
            PaxTbl.Columns.Add(cntTblColumn)

            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.String")
            cntTblColumn.ColumnName = "PaxTP"
            PaxTbl.Columns.Add(cntTblColumn)
            Dim cntrow As DataRow = Nothing
            For i As Integer = 1 To cntAdult
                cntrow = PaxTbl.NewRow()
                cntrow("Counter") = i
                cntrow("PaxTP") = "Passenger " & i.ToString() & " (Adult)"
                PaxTbl.Rows.Add(cntrow)
            Next
            Repeater_Adult.DataSource = PaxTbl
            Repeater_Adult.DataBind()


            PaxTbl.Clear()
            If cntChild > 0 Then

                For i As Integer = 1 To cntChild
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Child)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Child.DataSource = PaxTbl
                Repeater_Child.DataBind()
            End If


            PaxTbl.Clear()

            If cntInfant > 0 Then

                For i As Integer = 1 To cntInfant
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Infant)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Infant.DataSource = PaxTbl
                Repeater_Infant.DataBind()
            End If


        Catch ex As Exception
        End Try


    End Sub

    Protected Sub book_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles book.Click
        Session("search_type") = "RTF"
        Dim AgencyDs As DataSet
        Dim OBFltDs, IBFltDs As DataSet
        Dim totFare As Double = 0
        Dim netFare As Double = 0
        OBFltDs = objDA.GetFltDtlsInt(ViewState("OBTrackId"), Session("UID"))
        IBFltDs = objDA.GetFltDtlsInt(ViewState("IBTrackId"), Session("UID"))
        AgencyDs = objDA.GetAgencyDetails(Session("UID"))
        Try
            If AgencyDs.Tables.Count > 0 And OBFltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And OBFltDs.Tables(0).Rows.Count > 0 Then
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        totFare = OBFltDs.Tables(0).Rows(0)("totFare") + IBFltDs.Tables(0).Rows(0)("totFare")
                        netFare = OBFltDs.Tables(0).Rows(0)("netFare") + IBFltDs.Tables(0).Rows(0)("netFare")
                        If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                            Dim ProjectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
                            Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)
                            Dim CorpBillNo As String = Nothing
                            If Not IsDBNull(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                If Convert.ToBoolean(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                    CorpBillNo = clsCorp.GenerateBillNoCorp("D")
                                End If

                            End If
                            objDA.insertFltHdrDetails(OBFltDs, AgencyDs, Session("UID"), ddl_PGTitle.SelectedValue, txt_PGFName.Text, txt_PGLName.Text, txt_MobNo.Text, txt_Email.Text, "D", ProjectId, BookedBy, CorpBillNo, "", "")
                            objDA.insertFlightDetailsIntl(OBFltDs)
                            objDA.insertFareDetails(OBFltDs, "D")
                            InsertPaxDetail(ViewState("OBTrackId"), OBFltDs)

                            If Not IsDBNull(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                If Convert.ToBoolean(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                    CorpBillNo = clsCorp.GenerateBillNoCorp("D")
                                End If

                            End If
                            objDA.insertFltHdrDetails(IBFltDs, AgencyDs, Session("UID"), ddl_PGTitle.SelectedValue, txt_PGFName.Text, txt_PGLName.Text, txt_MobNo.Text, txt_Email.Text, "D", ProjectId, BookedBy, CorpBillNo, "", "")
                            objDA.insertFlightDetailsIntl(IBFltDs)
                            objDA.insertFareDetails(IBFltDs, "D")
                            InsertPaxDetail(ViewState("IBTrackId"), IBFltDs)
                            Response.Redirect("../wait.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=RTFLCC")
                        Else
                            Response.Redirect("../Domestic/BookingMsg.aspx?msg=CL")
                        End If
                    Else
                        Response.Redirect("../Domestic/BookingMsg.aspx?msg=NA")
                    End If
                End If
            End If
        Catch ex As Exception

        End Try


    End Sub

    Public Sub InsertPaxDetail(ByVal trackid As String, ByVal FltDs As DataSet)
        Try
            Adult = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult"))
            Child = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child"))
            Infant = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant"))
            Dim counter As Integer = 0

            For Each rw As RepeaterItem In Repeater_Adult.Items
                counter += 1

                Dim ddl_ATitle As DropDownList = DirectCast(rw.FindControl("ddl_ATitle"), DropDownList)
                Dim txtAFirstName As TextBox = DirectCast(rw.FindControl("txtAFirstName"), TextBox)
                Dim txtAMiddleName As TextBox = DirectCast(rw.FindControl("txtAMiddleName"), TextBox)
                If txtAMiddleName.Text = "Middle Name" Then
                    txtAMiddleName.Text = ""
                End If
                Dim txtALastName As TextBox = DirectCast(rw.FindControl("txtALastName"), TextBox)


                'Dim ddl_ADate As DropDownList = DirectCast(rw.FindControl("ddl_ADate"), DropDownList)
                'Dim ddl_AMonth As DropDownList = DirectCast(rw.FindControl("ddl_AMonth"), DropDownList)
                Dim txtadultDOB As TextBox = DirectCast(rw.FindControl("Txt_AdtDOB"), TextBox)
                Dim DOB As String = ""
                DOB = txtadultDOB.Text.Trim

                Dim ddl_AMealPrefer As DropDownList = DirectCast(rw.FindControl("ddl_AMealPrefer"), DropDownList)
                Dim ddl_ASeatPrefer As DropDownList = DirectCast(rw.FindControl("ddl_ASeatPrefer"), DropDownList)
                Dim txt_AAirline As TextBox = DirectCast(rw.FindControl("txt_AAirline"), TextBox)
                Dim txt_ANumber As TextBox = DirectCast(rw.FindControl("txt_ANumber"), TextBox)
                If txt_AAirline.Text = "Airline" Then
                    txt_AAirline.Text = ""
                End If
                If txt_ANumber.Text = "Number" Then
                    txt_ANumber.Text = ""
                End If
                If counter <= Infant Then
                    objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                     "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Text.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                     "true", "", "", "", "", "", "")
                Else

                    objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                     "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Text.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                     "false", "", "", "", "", "", "")

                End If
            Next

            If Child > 0 Then
                For Each rw As RepeaterItem In Repeater_Child.Items

                    Dim ddl_CTitle As DropDownList = DirectCast(rw.FindControl("ddl_CTitle"), DropDownList)
                    Dim txtCFirstName As TextBox = DirectCast(rw.FindControl("txtCFirstName"), TextBox)
                    Dim txtCMiddleName As TextBox = DirectCast(rw.FindControl("txtCMiddleName"), TextBox)
                    If txtCMiddleName.Text = "Middle Name" Then
                        txtCMiddleName.Text = ""
                    End If
                    Dim txtCLastName As TextBox = DirectCast(rw.FindControl("txtCLastName"), TextBox)

                    Dim txtchildDOB As TextBox = DirectCast(rw.FindControl("Txt_chDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtchildDOB.Text.Trim

                    Dim ddl_CMealPrefer As DropDownList = DirectCast(rw.FindControl("ddl_CMealPrefer"), DropDownList)
                    Dim ddl_CSeatPrefer As DropDownList = DirectCast(rw.FindControl("ddl_CSeatPrefer"), DropDownList)

                    objDA.insertPaxDetails(trackid, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), _
                     "CHD", DOB, "", "", ddl_CMealPrefer.SelectedValue, ddl_CSeatPrefer.SelectedValue, _
                     "false", "", "", "", "", "", "")
                Next
            End If

            If Infant > 0 Then
                Dim counter1 As Integer = 0
                For Each rw As RepeaterItem In Repeater_Infant.Items

                    Dim ddl_ITitle As DropDownList = DirectCast(rw.FindControl("ddl_ITitle"), DropDownList)
                    Dim txtIFirstName As TextBox = DirectCast(rw.FindControl("txtIFirstName"), TextBox)
                    Dim txtIMiddleName As TextBox = DirectCast(rw.FindControl("txtIMiddleName"), TextBox)
                    If txtIMiddleName.Text = "Middle Name" Then
                        txtIMiddleName.Text = ""
                    End If
                    Dim txtILastName As TextBox = DirectCast(rw.FindControl("txtILastName"), TextBox)

                    Dim txtinfantDOB As TextBox = DirectCast(rw.FindControl("Txt_InfantDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtinfantDOB.Text.Trim

                    Dim txtAFirstName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAFirstName"), TextBox)
                    Dim txtAMiddleName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAMiddleName"), TextBox)
                    Dim txtALastName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtALastName"), TextBox)

                    Dim Name As String = ""
                    Name = txtAFirstName.Text.Trim() + txtAMiddleName.Text.Trim() + txtALastName.Text.Trim()
                    If counter1 <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ITitle.SelectedValue, txtIFirstName.Text.Trim(), txtIMiddleName.Text.Trim(), txtILastName.Text.Trim(), _
                         "INF", DOB, "", "", "", "", _
                         "false", Name, "", "", "", "", "")
                    End If
                    counter1 += 1
                Next
            End If

        Catch ex As Exception
        End Try

    End Sub
End Class
