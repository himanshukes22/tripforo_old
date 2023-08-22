Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports STD.Shared
Imports STD.BAL
Imports EXCEPTION_LOG
Partial Class FlightInt_CustomerInfo
    Inherits System.Web.UI.Page

    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim objFareBreakup As New clsCalcCommAndPlb
    Dim objDA As New SqlTransaction
    '    Dim objSqlTrans As New SqlTransaction
    Dim IntAirDt As DataTable
    Dim trackId As String, LIN As String
    Dim Adult As Integer
    Dim Child As Integer
    Dim Infant As Integer
    Dim SelectedFltArray As Array
    Dim strFlt As String = "", strFare As String = ""
    Dim fareHashtbl As Hashtable
    Dim STDom As New SqlTransactionDom()
    Dim clsCorp As New ClsCorporate()
    'varaibles
    Dim objSql As New SqlTransactionNew
    Dim VCOB As String = "", VCIB As String = ""
    Dim VCOBSPL As String = "", VCIBSPL As String = ""
    Dim TripOB As String = "", TripIB As String = ""
    Dim ATOB As String = "", ATIB As String = ""
    Dim FLT_STAT As String = ""
    Dim Adti As Integer = 0, Chdi As Integer = 0
    Dim OBFltDs As New DataSet
    Dim FZBagListO As New List(Of FZServiceQuoteResponse)()
    Dim FZBagListR As New List(Of FZServiceQuoteResponse)()
    Dim IXBagListO As New List(Of G8ServiceQuoteResponse)()
    Dim IXBagListR As New List(Of G8ServiceQuoteResponse)()
    Dim IXMealListO As New List(Of G8ServiceQuoteResponse)()
    Dim IXMealListR As New List(Of G8ServiceQuoteResponse)()
    Dim IXSeatListO As New List(Of G8ServiceQuoteResponse)()
    Dim IXSeatListR As New List(Of G8ServiceQuoteResponse)()

    Dim FZBagListMain2 As New List(Of G8ServiceQuoteResponse)()
    Dim objUMSvc As New FltSearch1()
    Dim G9SSR As New List(Of GALWS.AirArabia.AirArabiaSSR)()
    Dim AKSSR, AKSSRIB As New List(Of GALWS.AirAsia.AirAsiaSSR)()
    Dim AKSeat, AKSeatIB As New List(Of GALWS.AirAsia.AirAsiaSeat)()
    Dim Provider As String = "", ProviderIB As String = ""
    Dim TBOSSR As New STD.BAL.TBO.SSR.SSRResponse()
    Dim TBOSSRIB As New STD.BAL.TBO.SSR.SSRResponse()
    Dim Constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    Dim objbal As New FlightCommonBAL(Constr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            Else
                If Not Page.IsPostBack Then

                    Dim AgencyDs As DataSet
                    AgencyDs = objDA.GetAgencyDetails(Session("UID"))
                    If AgencyDs.Tables.Count > 0 Then
                        If AgencyDs.Tables(0).Rows.Count > 0 Then
                            'BindStateGst("India")

                            'If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_State"))) Then
                            '    ddlStateGst.SelectedValue = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_State_Code"))
                            '    BindCityGst(ddlStateGst.SelectedValue)
                            '    If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_City"))) Then
                            '        ddlCityGst.SelectedItem.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_City"))
                            '    End If
                            'End If
                            If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile"))) Then
                                txt_MobNo.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile"))
                            End If

                            If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email"))) Then
                                txt_Email.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email"))
                            End If


                            If Convert.ToString(AgencyDs.Tables(0).Rows(0)("Is_GST_Apply")).ToLower() = "true" Then
                                txtGstNo.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GSTNO"))
                                txtGstCmpName.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Company_Name"))
                                txtGstAddress.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GSTAddress"))
                                txtPinCode.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Pincode"))
                                txtGstPhone.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_PhoneNo"))
                                txtGstEmail.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Email"))
                            End If
                        End If
                    End If

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



                    Dim query As String = HttpContext.Current.Request.QueryString(0).ToString()
                    Dim Track As String() = query.Split(",")
                    ViewState("trackid") = Track(0)
                    hdn_Psprt.Value = CheckIsPassportMandatory(Track(0))
                    'If (Session("Provider") = "1A") Then
                    '    OBFltDs = objDA.GetFltDtlsInt(Track(0), Session("UID"))
                    'ElseIf (Session("Provider") = "1G" Or Session("Provider") = "G9") Then
                    '    OBFltDs = objDA.GetFltDtls(Track(0), Session("UID"))
                    'End If
                    OBFltDs = objDA.GetFltDtls(Track(0), Session("UID"))
                    'mk
                    Try
                        Dim DSTRIP As DataSet = objDA.GetDomIntTrip(OBFltDs.Tables(0).Rows(0)("OrgDestFrom"), OBFltDs.Tables(0).Rows(0)("OrgDestTo"), "SELECT", Track(0))
                        Session("FDTRIP") = DSTRIP.Tables(0).Rows(0)("Trip").ToString
                    Catch ex As Exception

                    End Try




                    Provider = OBFltDs.Tables(0).Rows(0)("Provider")
                    Provider = OBFltDs.Tables(0).Rows(0)("Provider")


                    If (Provider = "FDD") Then
                        gstfd.Visible = False
                    End If

                    If (Provider.Trim.ToUpper = "LCC" And VCOB <> "IX") Then
                        InBoundFTseat_ibSec1.InnerHtml = "<a class='selectbtns topbutton topopup edit button' title='Select Seat' id='btnaddseat'>Seat Map " & OBFltDs.Tables(0).Rows(0)("Sector") & "</a>"
                        OBTrackIds.Value = Track(0)
                        OBValidatingCarrier.Value = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    End If
                    IntAirDt = OBFltDs.Tables(0)
                    Session("IntAirDt") = IntAirDt

                    Adult = Convert.ToInt16(IntAirDt.Rows(0)("Adult"))
                    Child = Convert.ToInt16(IntAirDt.Rows(0)("Child"))
                    Infant = Convert.ToInt16(IntAirDt.Rows(0)("Infant"))

                    lbl_adult.Text = Convert.ToInt16(IntAirDt.Rows(0)("Adult"))
                    lbl_child.Text = Convert.ToInt16(IntAirDt.Rows(0)("Child"))
                    lbl_infant.Text = Convert.ToInt16(IntAirDt.Rows(0)("Infant"))

                    'SelectedFltArray = IntAirDt.Select("Track_id='" & Track(0) & "'", "") 'IntAirDt.Select("LineItemNumber='" & LIN & "'", "")
                    'Try
                    '    For i As Integer = 0 To SelectedFltArray.Length - 1
                    '        strFlt = strFlt & (SelectedFltArray(i)("DepartureLocation")) & " - " & (SelectedFltArray(i)("ArrivalLocation")) & " Date : " & (SelectedFltArray(i)("Departure_Date")) & " " & (SelectedFltArray(i)("MarketingCarrier")) & "-" & (SelectedFltArray(i)("FlightIdentification")) & "<br/>"
                    '        strFlt = strFlt & "Dep : " & " " & (SelectedFltArray(i)("DepartureTime")) & "Hrs. Arr : " & (SelectedFltArray(i)("ArrivalTime")) & "Hrs. class : " & (SelectedFltArray(i)("RBD")) & "<br/><br/>"
                    '    Next
                    'Catch ex As Exception

                    'End Try
                    divFltDtls1.InnerHtml = showFltDetails(OBFltDs) 'strFlt
                    divtotFlightDetails.InnerHtml = STDom.CustFltDetails_Intl(OBFltDs)
                    ' Added Code
                    VCOB = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    VCOBSPL = OBFltDs.Tables(0).Rows(0)("AdtFareType")
                    TripOB = OBFltDs.Tables(0).Rows(0)("Trip")
                    TripOB = Session("FDTRIP").ToString()
                    hdn_vc.Value = VCOB
                    FLT_STAT = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("TripType")

                    If VCOB = "IX" Then
                        Dim searchvalue As String = OBFltDs.Tables(0).Rows(0)("SearchValue")
                        Dim sno As String = OBFltDs.Tables(0).Rows(0)("sno")
                        Dim FZUrls As New IXSvcAndMethodUrls()
                        If Not sno.ToLower().Contains(":basic") Then

                            Dim objsrvlist As New List(Of G8ServiceQuote)()
                            Dim objsrvQ As New G8ServiceQuote()
                            objsrvQ.LogicalFlightID = searchvalue.Split(":"c)(0)
                            objsrvQ.DepartureDate = OBFltDs.Tables(0).Rows(0)("sno").Split(":")(3).Split("T")(0)
                            objsrvQ.AirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                            objsrvQ.ServiceCode = ""
                            objsrvQ.Cabin = OBFltDs.Tables(0).Rows(0)("AdtCabin")
                            objsrvQ.Category = ""
                            objsrvQ.FareClass = OBFltDs.Tables(0).Rows(0)("AdtRbd")
                            objsrvQ.FareBasisCode = OBFltDs.Tables(0).Rows(0)("AdtFarebasis")
                            objsrvQ.DestinationAirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestTo")
                            objsrvlist.Add(objsrvQ)
                            Dim objFZFQ As New STD.BAL.G8FareQuote(sno.Split(":"c)(1), "", "", "")
                            Dim expsrvQoute As String = ""
                            'FZBagListO = objFZFQ.GetServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist, Adult + Child, searchvalue, sno, expsrvQoute)

                            Dim FRet As Integer = Convert.ToInt16(OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("Flight"))
                            Dim objsrvQ1 As New G8ServiceQuote()
                            Dim searchvalue1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("SearchValue")
                            Dim sno1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno")
                            Dim FZBagListMain As New List(Of G8ServiceQuoteResponse)()
                            FZBagListMain = objFZFQ.GetIXServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist, Adult + Child, expsrvQoute)
                            IXBagListO = objFZFQ.GetIXBagList(FZBagListMain, searchvalue, sno, objsrvQ.LogicalFlightID, "Baggage")
                            IXMealListO = objFZFQ.GetIXBagList(FZBagListMain, searchvalue, sno, objsrvQ.LogicalFlightID, "Meal")
                            '' IXSeatListO = objFZFQ.GetIXBagList(FZBagListMain, searchvalue, sno, objsrvQ.LogicalFlightID, "Seat")
                            If FRet = 2 Then
                                Dim objsrvlist1 As New List(Of G8ServiceQuote)()
                                objsrvQ1.LogicalFlightID = searchvalue1.Split(":"c)(0)
                                objsrvQ1.DepartureDate = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno").Split(":")(3).Split("T")(0)
                                objsrvQ1.AirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestTo")
                                objsrvQ1.ServiceCode = ""
                                objsrvQ1.Cabin = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtCabin")
                                objsrvQ1.Category = ""
                                objsrvQ1.FareClass = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtRbd")
                                objsrvQ1.FareBasisCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtFarebasis")
                                objsrvQ1.DestinationAirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestFrom")
                                objsrvlist1.Add(objsrvQ1)
                                Dim objFZFQ1 As New STD.BAL.FZFareQuote(sno1.Split(":"c)(1), "", "", "")
                                FZBagListMain2 = New List(Of G8ServiceQuoteResponse)()
                                FZBagListMain2 = objFZFQ.GetIXServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist1, Adult + Child, expsrvQoute)
                                IXBagListR = objFZFQ.GetIXBagList(FZBagListMain2, searchvalue1, sno1, objsrvQ1.LogicalFlightID, "Baggage")
                                IXMealListR = objFZFQ.GetIXBagList(FZBagListMain2, searchvalue1, sno1, objsrvQ1.LogicalFlightID, "Meal")
                                '' IXSeatListR = objFZFQ.GetIXBagList(FZBagListMain2, searchvalue1, sno1, objsrvQ1.LogicalFlightID, "Seat")
                            End If

                        End If
                    End If
                    'If VCOB = "FZ" Then
                    '    Dim searchvalue As String = OBFltDs.Tables(0).Rows(0)("SearchValue")
                    '    Dim sno As String = OBFltDs.Tables(0).Rows(0)("sno")
                    '    Dim FZUrls As New FZSvcAndMethodUrls()
                    '    If Not sno.ToLower().Contains(":basic") Then

                    '        Dim objsrvlist As New List(Of FZServiceQuote)()
                    '        Dim objsrvQ As New FZServiceQuote()
                    '        objsrvQ.LogicalFlightID = searchvalue.Split(":"c)(0)
                    '        objsrvQ.DepartureDate = OBFltDs.Tables(0).Rows(0)("sno").Split(":")(3).Split("T")(0)
                    '        objsrvQ.AirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                    '        objsrvQ.ServiceCode = ""
                    '        objsrvQ.Cabin = OBFltDs.Tables(0).Rows(0)("AdtCabin")
                    '        objsrvQ.Category = ""
                    '        objsrvQ.FareClass = OBFltDs.Tables(0).Rows(0)("AdtRbd")
                    '        objsrvQ.FareBasisCode = OBFltDs.Tables(0).Rows(0)("AdtFarebasis")
                    '        objsrvQ.DestinationAirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestTo")
                    '        objsrvlist.Add(objsrvQ)
                    '        Dim objFZFQ As New STD.BAL.FZFareQuote(sno.Split(":"c)(1), "", "", "")
                    '        Dim expsrvQoute As String = ""
                    '        'FZBagListO = objFZFQ.GetServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist, Adult + Child, searchvalue, sno, expsrvQoute)

                    '        Dim FRet As Integer = Convert.ToInt16(OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("Flight"))
                    '        Dim objsrvQ1 As New FZServiceQuote()
                    '        Dim searchvalue1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("SearchValue")
                    '        Dim sno1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno")
                    '        If FRet = 2 Then


                    '            Dim objsrvlist1 As New List(Of FZServiceQuote)()

                    '            objsrvQ1.LogicalFlightID = searchvalue1.Split(":"c)(0)
                    '            objsrvQ1.DepartureDate = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno").Split(":")(3).Split("T")(0)
                    '            objsrvQ1.AirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestTo")
                    '            objsrvQ1.ServiceCode = ""
                    '            objsrvQ1.Cabin = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtCabin")
                    '            objsrvQ1.Category = ""
                    '            objsrvQ1.FareClass = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtRbd")
                    '            objsrvQ1.FareBasisCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtFarebasis")
                    '            objsrvQ1.DestinationAirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestFrom")
                    '            objsrvlist.Add(objsrvQ1)
                    '            ' Dim objFZFQ1 As New STD.BAL.FZFareQuote(sno1.Split(":"c)(1), "", "", "")
                    '            'FZBagListR = objFZFQ1.GetServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist1, Adult + Child, searchvalue1, sno1, expsrvQoute)



                    '        End If

                    '        Dim FZBagListMain As New List(Of FZServiceQuoteResponse)()

                    '        FZBagListMain = objFZFQ.GetServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist, Adult + Child, expsrvQoute)
                    '        FZBagListO = objFZFQ.GetBagList(FZBagListMain, searchvalue, sno, objsrvQ.LogicalFlightID)
                    '        If FRet = 2 Then
                    '            FZBagListR = objFZFQ.GetBagList(FZBagListMain, searchvalue1, sno1, objsrvQ1.LogicalFlightID)
                    '        End If



                    '    End If



                    'End If



                    Dim Eq As Integer
                    Try
                        If InStr(VCOBSPL, "Spl") = 0 And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And Provider.Trim().ToUpper() <> "TB" And Provider.Trim().ToUpper() <> "AK" Then
                            If (VCOB = "SG") Then
                                If (Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Tot_Dur").ToString().Substring(0, 2)) < 1) Then
                                    ATOB = "Q400"
                                Else
                                    If (OBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim() = "DH8") Then
                                        ATOB = "Q400"
                                    ElseIf (Int32.TryParse(OBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim(), Eq)) Then
                                        If (Eq >= 737 And Eq <= 900) Then
                                            ATOB = "Boeing"
                                        Else
                                            ATOB = ""
                                        End If
                                    End If
                                End If
                            ElseIf (VCOB = "6E") Then
                                ATOB = "ALL"
                            ElseIf (VCOB = "G8") Then
                                ATOB = "ALL"
                            End If
                        End If
                        If FLT_STAT = "R" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") Then
                            Dim Org As String = OBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                            Dim Dest As String = OBFltDs.Tables(0).Rows(0)("OrgDestTo")
                            Dim Dt As DataRow() = OBFltDs.Tables(0).Select("OrgDestFrom = '" & Dest & "'")
                            Dim row As Integer = OBFltDs.Tables(0).Rows.Count
                            Dim EqIB As Integer
                            If (VCOB = "SG") Then
                                If (Convert.ToInt16(Dt(0)("Tot_Dur").ToString().Substring(0, 2)) < 1) Then
                                    ATIB = "Q400"
                                Else
                                    If (Dt(0)("EQ").ToString().Trim() = "DH8") Then
                                        ATIB = "Q400"
                                    ElseIf (Int32.TryParse(Dt(0)("EQ").ToString().Trim(), EqIB)) Then
                                        If (EqIB >= 737 And EqIB <= 900) Then
                                            ATIB = "Boeing"
                                        Else
                                            ATIB = ""
                                        End If
                                    End If
                                End If
                            ElseIf (VCOB = "6E") Then
                                ATIB = "ALL"
                            ElseIf (VCOB = "G8") Then
                                ATIB = "ALL"
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    If Provider.Trim().ToUpper() = "TB" Then

                        Dim exep As String = ""
                        Dim dsCrd As DataSet = objSql.GetCredentials("TB", "", "I")
                        Dim snoArr As String() = Convert.ToString(OBFltDs.Tables(0).Rows(0)("sno")).Split(":")

                        Dim objBook As New STD.BAL.TBO.TBOBook()

                        Dim log As New Dictionary(Of String, String)

                        objBook.GetFareQuote(dsCrd, snoArr(1), snoArr(0), log, exep)

                        Dim objssr As New STD.BAL.TBO.SSR.TOBSSR()

                        TBOSSR = objssr.GetSSR("", snoArr(1), snoArr(0), dsCrd)
                    ElseIf VCOB = "AK" Or VCOB = "I5" Or VCOB = "D7" Or VCOB = "FD" Or VCOB = "QZ" Or VCOB = "Z2" Or VCOB = "XJ" Or VCOB = "XT" Or VCOB = "DJ" Then
                        Try
                            Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "I")
                            Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                            Dim NewSessionId As String = ""
                            'Call Sell Transaction
                            Dim sellfare As Decimal = Math.Round(objAirAsia.GETSELL(OBFltDs.Tables(0), dsCrd, "", Constr, NewSessionId), 2)

                            Dim orgfare As Decimal = Math.Round(Convert.ToDecimal(OBFltDs.Tables(0).Rows(0)("OriginalTF")), 2)
                            'update table value with new sessionid
                            For sr As Integer = 0 To OBFltDs.Tables(0).Rows.Count - 1
                                OBFltDs.Tables(0).Rows(sr)("Searchvalue") = NewSessionId
                                OBFltDs.AcceptChanges()
                            Next
                            If (sellfare = 0) Then
                                ShowAlertMessage("We can not book this due to Fare has been changed in Sell Journey ")
                                book.Visible = False
                            ElseIf (sellfare > orgfare) Then
                                ShowAlertMessage("Fare has been changed. Now Rs " & (Convert.ToDecimal(OBFltDs.Tables(0).Rows(0)("totFare")) + (sellfare - orgfare)).ToString() & "")
                                book.Visible = False
                            End If
                            'call SSR Availibity
                            Dim serverpath As String = "D:\\" 'Server.MapPath("D:\\")
                            AKSSR = objAirAsia.GetSSRAvailabilityForBooking(AKSSR, dsCrd, OBFltDs.Tables(0), serverpath, "")
                        Catch ex As Exception
                            ITZERRORLOG.ExecptionLogger.FileHandling("Pagelode_AirAsiaSELL", "Error_001", ex, "Flight")
                            book.Visible = False
                        End Try
                    ElseIf Provider.Trim().ToUpper() = "LCC" And VCOB = "G9" Then
                        Dim dsCrd As DataSet = objSql.GetCredentials("G9", "", "I")
                        Dim objAirArabia As New GALWS.AirArabia.AirArabiaFlightSearch()
                        '  Dim Constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString 
                        G9SSR = objAirArabia.MealBaggageAvailability(G9SSR, dsCrd, OBFltDs.Tables(0), Constr)
                    End If

                    'fareHashtbl = objFareBreakup.getIntFareDetails(LIN)
                    'divFareDtls.InnerHtml = fareBreakupfun(IntAirDt)
                    div_fare.InnerHtml = fareBreakupfun(OBFltDs.Tables(0))

                    Bind_pax(Adult, Child, Infant)


                    If (Session("FDTRIP").ToString() = "I") Then

                    Else
                        For Each rw As RepeaterItem In Repeater_Adult.Items
                            '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                            Dim Fda As HtmlGenericControl = DirectCast(rw.FindControl("divFdA"), HtmlGenericControl)
                            Fda.Visible = False
                        Next
                        For Each rw As RepeaterItem In Repeater_Child.Items
                            '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                            Dim Fdc As HtmlGenericControl = DirectCast(rw.FindControl("divFdC"), HtmlGenericControl)
                            Fdc.Visible = False
                        Next

                        For Each rw As RepeaterItem In Repeater_Child.Items
                            '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                            Dim FdI As HtmlGenericControl = DirectCast(rw.FindControl("divFdI"), HtmlGenericControl)
                            FdI.Visible = False
                        Next


                    End If



                End If
                'Book Button Show Hide - Staff
                Try
                    If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true" AndAlso Convert.ToString(Session("LoginByStaff")) = "STAFF" AndAlso Convert.ToString(Session("FlightActive")) <> "True") Then
                        book.Visible = False
                    End If
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Private Function fareBreakupfun(ByVal IntAirDt As DataTable) As String
        Try
            Dim tax(), tax1() As String, yq As Integer = 0, tx As Integer = 0
            'tax = fareHashtbl("AdtTax").ToString.Split("#")
            tax = IntAirDt.Rows(0)("Adt_Tax").ToString.Split("#")
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                Else
                    tax1 = tax(i).Split(":")
                    tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                End If
            Next

            Dim tcperpax As Integer = Convert.ToInt32(IntAirDt.Rows(0)("TC")) / (Convert.ToInt32(IntAirDt.Rows(0)("Adult")) + Convert.ToInt32(IntAirDt.Rows(0)("Child")))
            'Changed ID 02/04/2014
            strFare = strFare & "<div id='OB_FT' class='w100 padding2 bgw brdr' style='border-top:1px solid #ccc;'>"
            strFare = strFare & "<div class='w100 lft'>"
            strFare = strFare & "<div>"
            strFare = strFare & "<div class='w70 lft '>Adult Fare</div>"
            'strFare = strFare & "<td width='93'>" & fareHashtbl("AdtBFare") & "</td>"
            strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("AdtBFare") & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div>"
            strFare = strFare & "<div class='w70 lft '>Fuel Surcharge</div>"
            strFare = strFare & "<div class='w30 lft'>" & yq & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div>"
            strFare = strFare & "<div class='w70 lft '>Tax</div>"
            strFare = strFare & "<div class='w30 lft'>" & (tx + tcperpax) & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div>"
            strFare = strFare & "<div class='w70 lft '>Total</div>"
            'strFare = strFare & "<td>" & fareHashtbl("AdtTotal") & "</td>"
            strFare = strFare & "<div class='w30 lft'>" & (Convert.ToInt32(IntAirDt.Rows(0)("AdtFare")) + tcperpax) & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='clear1'></div>"
            strFare = strFare & "<div class='hr'></div>"
            strFare = strFare & "</div>"

            If Child > 0 Then
                Try
                    yq = 0
                    tx = 0
                    'tax = fareHashtbl("ChdTax").ToString.Split("#")
                    tax = IntAirDt.Rows(0)("Chd_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        End If
                    Next
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try

                strFare = strFare & "<div class='w100 lft'>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Child Fare</div>"
                'strFare = strFare & "<td width='130'>" & fareHashtbl("ChdBFare") & "</td>"
                strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("ChdBFare") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Fuel Surcharge </div>"
                strFare = strFare & "<div class='w30 lft'>" & yq & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Tax</div>"
                strFare = strFare & "<div class='w30 lft'>" & (tx + tcperpax) & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Total</div>"
                'strFare = strFare & "<td>" & fareHashtbl("ChdTotal") & "</td>"
                strFare = strFare & "<div class='w30 lft'>" & (IntAirDt.Rows(0)("ChdFare") + tcperpax) & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='clear1'></div>"
                strFare = strFare & "<div class='hr'></div>"
                strFare = strFare & "</div>"
            End If

            If Infant > 0 Then
                Try
                    yq = 0
                    tx = 0
                    'tax = fareHashtbl("InfTax").ToString.Split("#")
                    tax = IntAirDt.Rows(0)("Inf_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        End If
                    Next
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
                strFare = strFare & "<div class='w100 lft'>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft '>Infant Fare</div>"
                'strFare = strFare & "<td width='130'>" & fareHashtbl("InfBFare") & "</td>"
                strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("InfBFare") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft '>Tax</div>"
                strFare = strFare & "<div class='w30 lft'>" & tx & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft '>Total</div>"
                'strFare = strFare & "<td>" & fareHashtbl("InfTotal") & "</td>"
                strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("InfFare") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='clear1'></div>"
                strFare = strFare & "<div class='hr'></div>"
                strFare = strFare & "</div>"
            End If
            strFare = strFare & "<div class='w100 lft' >"
            strFare = strFare & "<div>"
            strFare = strFare & "<div class='w70 lft'>Srv. Tax</div>"
            'strFare = strFare & "<td width='130'>" & fareHashtbl("SrvTax") & "</td>"
            strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("SrvTax") & "</div>"
            strFare = strFare & "</div>"

            'If (Convert.ToDouble(IntAirDt.Rows(0)("TOTMGTFEE")) > 0) Then
            If (IntAirDt.Rows(0)("IsCorp") = True) Then
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Mgnt. Fee</div>"
                strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("TOTMGTFEE") & "</div>"
                strFare = strFare & "</div>"
            Else
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Tran. Fee</div>"
                'strFare = strFare & "<td>" & fareHashtbl("TFee") & "</td>"
                strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("TFee") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div>"
                strFare = strFare & "<div class='w70 lft'>Tran. Charge</div>"
                'strFare = strFare & "<td>" & fareHashtbl("TC") & "</td>"
                strFare = strFare & "<div class='w30 lft'>0</div>"
                strFare = strFare & "</div>"

            End If

            strFare = strFare & "<div id='trtotfare' onmouseover=funcnetfare('block','trnetfare'); onmouseout=funcnetfare('none','trnetfare'); style='cursor:pointer;color: #004b91'>"
            strFare = strFare & "<div class='w70 lft bld'>Total Fare(" & Adult & " Adt," & Child & " Chd," & Infant & " Inf)</div>"
            'strFare = strFare & "<td>" & fareHashtbl("totFare") & "</td>"
            strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("totFare") & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div id='trnetfare' style='display:none'>"

            strFare = strFare & "<div class='w70 lft bld'>Commission</div>"
            strFare = strFare & "<div class='rgt w30'>" & IntAirDt.Rows(0)("DisCount") & "</div>"


            Dim Totaltds As Double = Convert.ToDouble(IntAirDt.Rows(0)("AdtTds") * IntAirDt.Rows(0)("Adult")) + Convert.ToDouble(IntAirDt.Rows(0)("ChdTds") * IntAirDt.Rows(0)("Child"))

            strFare = strFare & "<div class='w70 lft bld'>TDS</div>"
            strFare = strFare & "<div class='rgt w30'>" & Totaltds & "</div>"

            strFare = strFare & "<div class='w70 lft bld'>Net Fare</div>"
            'strFare = strFare & "<td>" & fareHashtbl("netFare") & "</td>"
            strFare = strFare & "<div class='w30 lft'>" & IntAirDt.Rows(0)("netFare") & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div><div class='clear'></div>"
            strFare = strFare & "</div>"

            Dim TotalFare As Double
            Dim NetFare As Double
            NetFare = Convert.ToDouble(IntAirDt.Rows(0)("netFare"))
            'If FT = "InBound" Then
            '    TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("TotFare"))
            'Else
            TotalFare = Convert.ToDouble(IntAirDt.Rows(0)("totFare"))
            ' End If
            strFare = strFare & "<div class='large-12 medium-12 small-12 columns' style='border-top:1px solid #ccc;'><div style='font-size:18px; text-align:center; font-weight:bold;  color:#000042; line-height:40px;'>Total Fare : " & TotalFare & "</div><div id='tr_totnetfare' style='display:none;position:absolute;background:#F1F1F1;border: thin solid #D1D1D1;padding:10px; font-weight:bold; font-size:14px; color:#000;'>Net Fare: " & NetFare & "</div></div>"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
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
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Protected Sub book_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles book.Click
        Session("search_type") = "FltInt"
        Dim AgencyDs As DataSet
        Dim FltDs As DataSet
        Dim totFare As Double = 0
        Dim netFare As Double = 0
        Dim FltHdr As New DataSet
        Dim ProjectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
        Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)
        Dim CorpBillNo As String = Nothing
        Dim Prvdr As String = ""
        FltDs = objDA.GetFltDtls(ViewState("trackid"), Session("UID"))
        VCOB = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")
        VCOBSPL = FltDs.Tables(0).Rows(0)("AdtFareType") 'New Code
        AgencyDs = objDA.GetAgencyDetails(Session("UID"))
        FLT_STAT = FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") ' Added
        Prvdr = FltDs.Tables(0).Rows(0)("Provider")
        Dim SeatL As String = ""
        SeatL = seatSelect.Value


        Try
            If AgencyDs.Tables.Count > 0 And FltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And FltDs.Tables(0).Rows.Count > 0 Then
                    totFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("totFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    netFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("netFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("totFare") = totFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("totFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("netFare") = netFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("netFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    ' FltDs.AcceptChanges()
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        Dim agentBal As String = ""
                        agentBal = objUMSvc.GetAgencyBal()
                        ''''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                        '' If Convert.ToDouble(agentBal) > netFare Then
                        FltHdr = objDA.GetHdrDetails(ViewState("trackid"))
                        If FltHdr.Tables.Count > 0 Then
                            If FltHdr.Tables(0).Rows.Count = 0 Then
                                If Not IsDBNull(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                    If Convert.ToBoolean(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                        CorpBillNo = clsCorp.GenerateBillNoCorp("I")
                                    End If

                                End If
                                objDA.insertFltHdrDetails(FltDs, AgencyDs, Session("UID"), ddl_PGTitle.SelectedValue, txt_PGFName.Text, txt_PGLName.Text, txt_MobNo.Text, txt_Email.Text, "I", ProjectId, BookedBy, CorpBillNo, "InBound", ViewState("trackid"))
                                objDA.insertFlightDetails(FltDs)
                                objDA.insertFareDetails(FltDs, "I")
                                If (VCOB = "IX") Then
                                    InsertPaxDetail(ViewState("trackid"), VCOB, SeatL)
                                Else
                                    InsertPaxDetail(ViewState("trackid"), VCOB, SeatL)
                                End If


                                ' Dim gstcity As String = If(ddlCityGst.SelectedItem.Text = "Select City", "", ddlCityGst.SelectedItem.Text.Trim())
                                'Dim gstState As String = If(ddlStateGst.SelectedItem.Text = "Select State", "", ddlStateGst.SelectedItem.Text.Trim())
                                Dim gstcity As String = If(GSTCityHid.Value = "Select City", "", GSTCityHid.Value.Trim())
                                Dim gstState As String = If(GSTStateHid.Value = "Select State", "", GSTStateHid.Value.Trim())

                                UpdateGSTDetail(FltDs.Tables(0).Rows(0)("Track_id"), txtGstNo.Text.Trim(), txtGstCmpName.Text.Trim(), txtGstAddress.Text & "/" & gstcity & "/" & gstState.Replace(" ", [String].Empty) & "/" & txtPinCode.Text.Trim(), txtGstPhone.Text.Trim(), txtGstEmail.Text.Trim(), txtRemarks.InnerText.Trim())

                                Try
                                    If Session("LoginByOTP") IsNot Nothing AndAlso Convert.ToString(Session("LoginByOTP")) <> "" AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                                        'Dim OTPRefNo As String = "OTP" + DateTime.Now.ToString("yyyyMMddHHmmssffffff").Substring(7, 13)
                                        Dim UserId As String = Session("UID")
                                        Dim Remark As String = txtRemarks.InnerText.Trim()
                                        Dim OTPRefNo As String = FltDs.Tables(0).Rows(0)("Track_id")
                                        Dim LoginByOTP As String = Session("LoginByOTP")
                                        Dim OTPId As String = Session("OTPID")
                                        Dim ServiceType As String = "FLIGHT-TICKET-INT"
                                        Dim flag As Integer = 0
                                        Dim OTPST As New SqlTransaction
                                        flag = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType)
                                    End If
                                Catch ex As Exception
                                    'clsErrorLog.LogInfo(ex)
                                    EXCEPTION_LOG.ErrorLog.FileHandling("FLIGHT-INT", "Error_102", ex, "FlightInt-CustomerInfo")
                                End Try

                                Try
                                    Dim ObjFRule As New GALWS.InsertFareRule
                                    ObjFRule.GetFareRule(OBFltDs, ViewState("trackid"))
                                Catch ex As Exception

                                End Try

                                Dim sellcheck As String = ""
                                If (VCOB = "SG" Or VCOB = "6E" Or VCOB = "FZ" Or VCOB = "G8" Or VCOB = "G9" Or VCOB = "AK" Or VCOB = "I5" Or VCOB = "D7" Or VCOB = "FD" Or VCOB = "QZ" Or VCOB = "Z2" Or VCOB = "XJ" Or VCOB = "XT" Or VCOB = "DJ") And InStr(VCOBSPL, "Spl") = 0 Or Prvdr = "TB" Or Prvdr = "YA" Or VCOB = "IX" Then
                                    Dim Paxdt As DataSet = objDA.Get_MEAL_BAG_PaxDetails(ViewState("trackid").ToString())
                                    Insert_MEAL_BAG_Detail(ViewState("trackid"), FltDs, Paxdt, "OB", "O") 'New Code
                                    If FLT_STAT = "R" Then 'New Code
                                        Insert_MEAL_BAG_Detail(ViewState("trackid"), FltDs, Paxdt, "IB", "R") 'New Code
                                    End If
                                    'Calling GST in Air Asia
                                    If (VCOB = "AK" Or VCOB = "I5" Or VCOB = "D7" Or VCOB = "FD" Or VCOB = "QZ" Or VCOB = "Z2" Or VCOB = "XJ" Or VCOB = "XT" Or VCOB = "DJ") And txtGstNo.Text.Trim() <> "" Then
                                        Dim SNNO() As String = FltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                                        Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "I")
                                        Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                                        Dim sessionvalue As String = objAirAsia.UpdateContactsGST(dsCrd, FltDs.Tables(0).Rows(0)("Searchvalue").ToString(), FltDs.Tables(0).Rows(0)("Track_id").ToString(), txtGstCmpName.Text.Trim(), txtGstNo.Text.Trim(), txtGstEmail.Text.Trim(), txtGstPhone.Text.Trim(), txtGstAddress.Text & "/" & gstcity & "/" & gstState & "/" & txtPinCode.Text.Trim(), Constr, SNNO(7), SNNO(8))
                                    End If

                                    If VCOB = "FZ" Or Prvdr = "TB" Or Prvdr = "YA" Or VCOB = "IX" Then
                                        Dim msg As String = UpadteFZFare(ViewState("trackid"))
                                        If msg <> "1" Then
                                            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                                        End If
                                    Else
                                        'calling SELL SSR
                                        sellcheck = SELL_SSR(ViewState("trackid"))
                                    End If
                                    Dim seatlist As List(Of MySeatDetails) = New List(Of MySeatDetails)()
                                    If String.IsNullOrEmpty(SeatL) = False Then seatlist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of MySeatDetails))(SeatL)
                                    UpadteSeatFare(seatlist, ViewState("trackid"))
                                End If
                                If sellcheck <> "FAILURE" Then
                                    Response.Redirect("../International/PriceDetails.aspx?tid=" & ViewState("trackid"), False)
                                Else
                                    Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                                End If
                            Else
                                Response.Redirect("BookingMsg.aspx?msg=1")
                            End If
                        Else
                            Response.Redirect("BookingMsg.aspx?msg=2")
                        End If
                    Else
                        Response.Redirect("BookingMsg.aspx?msg=NA")
                    End If
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try


    End Sub
    Public Function UpadteSeatFare(ByRef seatlist As List(Of MySeatDetails), ByRef trackIds As String) As String

        Dim SeatFare As Decimal = 0
        If (seatlist.Count > 0) Then
            For jj As Integer = 0 To seatlist.Count - 1
                SeatFare = SeatFare + Convert.ToDecimal(seatlist(jj).Amount)
            Next
        End If
        Return objbal.Update_NET_TOT_Fare_Seat(trackIds, (SeatFare).ToString())

    End Function
    Public Function UpadteFZFare(ByVal trackId As String) As String

        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(trackId, "")
        Dim BgPr As Decimal = 0
        If (MBDT.Tables(0).Rows.Count > 0) Then
            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
            Next

        End If
        Return objSql.Update_NET_TOT_Fare(trackId, (BgPr).ToString())


    End Function

    Public Sub InsertPaxDetail(ByVal trackid As String, ByVal pr As String, ByVal SeatReq As String)
        Try
            IntAirDt = Session("IntAirDt")
            Adult = Convert.ToInt16(IntAirDt.Rows(0)("Adult"))
            Child = Convert.ToInt16(IntAirDt.Rows(0)("Child"))
            Infant = Convert.ToInt16(IntAirDt.Rows(0)("Infant"))
            Dim counter As Integer = 0
            Dim seatlist As List(Of MySeatDetails) = New List(Of MySeatDetails)()
            If String.IsNullOrEmpty(SeatReq) = False Then seatlist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of MySeatDetails))(SeatReq)
            Dim seat As String = ""
            For Each rw As RepeaterItem In Repeater_Adult.Items
                counter += 1

                Dim ddl_ATitle As DropDownList = DirectCast(rw.FindControl("ddl_ATitle"), DropDownList)
                Dim ddl_AGender As DropDownList = DirectCast(rw.FindControl("ddl_AGender"), DropDownList)
                Dim txtAFirstName As TextBox = DirectCast(rw.FindControl("txtAFirstName"), TextBox)
                Dim txtAMiddleName As TextBox = DirectCast(rw.FindControl("txtAMiddleName"), TextBox)
                If txtAMiddleName.Text = "Middle Name" Then
                    txtAMiddleName.Text = ""
                End If
                Dim txtALastName As TextBox = DirectCast(rw.FindControl("txtALastName"), TextBox)

                Dim txtadultDOB As TextBox = DirectCast(rw.FindControl("Txt_AdtDOB"), TextBox)
                Dim DOB As String = ""
                DOB = txtadultDOB.Text.Trim

                Dim gender As String
                gender = ddl_AGender.SelectedValue.Trim
                'If ddl_ATitle.SelectedValue.Trim.ToLower = "dr" Or ddl_ATitle.SelectedValue.Trim.ToLower = "prof" Then


                'ElseIf ddl_ATitle.SelectedValue.Trim.ToLower = "mr" Then
                'gender = "M"

                'End If

                '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                Dim IssuingCountry_Adl As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_IssuingCountry_Adl"), HtmlInputHidden)
                Dim Nationality_Adl As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_Nationality_Adl"), HtmlInputHidden)
                Dim Passport_Adl As TextBox = DirectCast(rw.FindControl("txt_Passport_Adl"), TextBox)
                Dim Ex_date_Adl As TextBox = DirectCast(rw.FindControl("txt_ex_date_Adl"), TextBox)
                Dim Ddl_A_Meal_Obhid As HiddenField = DirectCast(rw.FindControl("Ddl_A_Meal_Obhid"), HiddenField)
                Dim Ddl_A_EB_Obhid As HiddenField = DirectCast(rw.FindControl("Ddl_A_EB_Obhid"), HiddenField)
                Dim SeattypeAhid As HiddenField = DirectCast(rw.FindControl("SeattypeAhid"), HiddenField)
                'If IssuingCountry_Adl.Value = "" Or Nothing Then
                '    ShowAlertMessage("Invalid country value for adult,please select the valid country")
                'End If
                'If Nationality_Adl.Value = "" Or Nothing Then
                '    ShowAlertMessage("Invalid nationality for adult,please select the valid nationality")
                'End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim ddl_AMealPrefer As DropDownList = New DropDownList()

                Dim ddl_ABagPrefer As DropDownList = New DropDownList()
                Dim ddl_ASeatPrefer As DropDownList = New DropDownList()

                If pr = "IX" Then
                    ddl_AMealPrefer = DirectCast(rw.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                    ddl_ABagPrefer = DirectCast(rw.FindControl("Ddl_A_EB_Ob"), DropDownList)
                    ddl_ASeatPrefer = DirectCast(rw.FindControl("SeattypeA"), DropDownList)
                    ddl_AMealPrefer.SelectedValue = Ddl_A_Meal_Obhid.Value
                    ddl_ABagPrefer.SelectedValue = Ddl_A_EB_Obhid.Value
                    ddl_ASeatPrefer.SelectedValue = SeattypeAhid.Value

                Else
                    ddl_AMealPrefer = DirectCast(rw.FindControl("ddl_AMealPrefer"), DropDownList)
                    ddl_ASeatPrefer = DirectCast(rw.FindControl("ddl_ASeatPrefer"), DropDownList)
                End If
                

             
                Dim txt_AAirline As HtmlInputHidden = DirectCast(rw.FindControl("hidtxtAirline_int"), HtmlInputHidden)

                Dim txt_ANumber As TextBox = DirectCast(rw.FindControl("txt_ANumber"), TextBox)
                If txt_AAirline.Value = "Airline" Then
                    txt_AAirline.Value = ""
                End If
                If txt_ANumber.Text = "Number" Then
                    txt_ANumber.Text = ""
                End If


                Dim seatlistO As List(Of MySeatDetails) = New List(Of MySeatDetails)()

                seatlistO = seatlist.Where(Function(s) s.Title = ddl_ATitle.SelectedValue And s.FName = txtAFirstName.Text.Trim() And s.LName = txtALastName.Text.Trim()).ToList()
                For s As Integer = 0 To seatlistO.Count - 1
                    seat = seat & seatlistO(s).Sector & " - " + seatlistO(s).Seat & " - " + seatlistO(s).SeatAlignment & ","
                Next

                If (String.IsNullOrEmpty(seat)) Then
                    If pr = "IX" Then
                        If counter <= Infant Then
                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                             "true", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                        Else

                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), Ddl_A_Meal_Obhid.Value, SeattypeAhid.Value, _
                         "false", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())

                    End If
                Else
                    If counter <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                         "true", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                    Else

                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                             "false", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                        End If
                    End If
                Else

                    If pr = "IX" Then
                        If counter <= Infant Then
                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, seat, _
                             "true", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                        Else

                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), Ddl_A_Meal_Obhid.Value, seat, _
                             "false", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())

                        End If
                    Else
                        If counter <= Infant Then
                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, seat, _
                             "true", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                        Else

                            objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                             "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, seat, _
                             "false", "", gender, Ex_date_Adl.Text.Trim(), Passport_Adl.Text.Trim(), IssuingCountry_Adl.Value.Trim(), Nationality_Adl.Value.Trim())
                        End If
                    End If
                End If
                Dim objs As STD.BAL.FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                If seatlist.Count > 0 Then
                    Dim Fullname As String = ""
                    For s As Integer = 0 To seatlistO.Count - 1
                        Dim SeatIns As STD.Shared.Seat = New STD.Shared.Seat()
                        '' SeatIns.PaxId = PaxId
                        SeatIns.OrderId = trackid
                        SeatIns.SeatDesignator = seatlistO(s).Seat
                        SeatIns.Amount = Convert.ToInt32(seatlistO(s).Amount)
                        SeatIns.Origin = seatlistO(s).Sector.Split("-"c)(0)
                        SeatIns.Destination = seatlistO(s).Sector.Split("-"c)(1)
                        SeatIns.FlightNumber = seatlistO(s).FlightNumber
                        SeatIns.FlightTime = seatlistO(s).FlightTime
                        SeatIns.VC = VCOB
                        SeatIns.SeatAlignment = seatlistO(s).SeatAlignment
                        SeatIns.OptionalServiceRef = seatlistO(s).OptionalServiceRef
                        SeatIns.Group = seatlistO(s).Group
                        SeatIns.ClassOfService = seatlistO(s).ClassOfService
                        SeatIns.Equipment = seatlistO(s).Equipment
                        SeatIns.Carrier = seatlistO(s).Carrier
                        SeatIns.Paid = seatlistO(s).Paid
                        Fullname = seatlistO(s).Title & seatlistO(s).FName & seatlistO(s).LName
                        objs.insertSeat(SeatIns, Fullname)
                    Next
                End If
            Next

            If Child > 0 Then
                For Each rw As RepeaterItem In Repeater_Child.Items

                    Dim ddl_CTitle As DropDownList = DirectCast(rw.FindControl("ddl_CTitle"), DropDownList)
                    Dim ddl_CGender As DropDownList = DirectCast(rw.FindControl("ddl_CGender"), DropDownList)
                    Dim txtCFirstName As TextBox = DirectCast(rw.FindControl("txtCFirstName"), TextBox)
                    Dim txtCMiddleName As TextBox = DirectCast(rw.FindControl("txtCMiddleName"), TextBox)
                    If txtCMiddleName.Text = "Middle Name" Then
                        txtCMiddleName.Text = ""
                    End If
                    Dim txtCLastName As TextBox = DirectCast(rw.FindControl("txtCLastName"), TextBox)

                    Dim txtchildDOB As TextBox = DirectCast(rw.FindControl("Txt_chDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtchildDOB.Text.Trim

                    Dim gender As String
                    gender = ddl_CGender.SelectedValue.Trim
                    'If ddl_CTitle.SelectedValue.Trim.ToLower = "mstr" Then
                    '    gender = "M"

                    'End If
                    '''''''''''''''''''''''' for Child Possport/and all '''''''''''''''''''''''''''''''''''
                    Dim Passport_Chd As TextBox = DirectCast(rw.FindControl("txt_Passport_Chd"), TextBox)
                    Dim Ex_date_Chd As TextBox = DirectCast(rw.FindControl("txt_ex_date_Chd"), TextBox)
                    Dim IssuingCountry_Chd As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_IssuingCountry_Chd"), HtmlInputHidden)
                    Dim Nationality_Chd As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_Nationality_Chd"), HtmlInputHidden)

                    'If IssuingCountry_Chd.Value = "" Or Nothing Then
                    '    ShowAlertMessage("Invalid country value for child,please select the valid country")
                    'End If
                    'If Nationality_Chd.Value = "" Or Nothing Then
                    '    ShowAlertMessage("Invalid nationality for child,please select the valid nationality")
                    'End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim ddl_CMealPrefer As DropDownList = DirectCast(rw.FindControl("ddl_CMealPrefer"), DropDownList)
                    Dim ddl_CSeatPrefer As DropDownList = DirectCast(rw.FindControl("ddl_CSeatPrefer"), DropDownList)

                    Dim seatlistO As List(Of MySeatDetails) = New List(Of MySeatDetails)()
                    seatlistO = seatlist.Where(Function(s) s.Title = ddl_CTitle.SelectedValue And s.FName = txtCFirstName.Text.Trim() And s.LName = txtCLastName.Text.Trim()).ToList()
                    For s As Integer = 0 To seatlistO.Count - 1
                        seat = seat & seatlistO(s).Sector & " - " + seatlistO(s).Seat & " - " + seatlistO(s).SeatAlignment & ","
                    Next
                    If (String.IsNullOrEmpty(seat)) Then
                        objDA.insertPaxDetails(trackid, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), _
                         "CHD", DOB, "", "", ddl_CMealPrefer.SelectedValue, ddl_CSeatPrefer.SelectedValue, _
                         "false", "", gender, Ex_date_Chd.Text.Trim(), Passport_Chd.Text.Trim(), IssuingCountry_Chd.Value.Trim(), Nationality_Chd.Value.Trim())
                    Else
                        objDA.insertPaxDetails(trackid, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), _
                  "CHD", DOB, "", "", ddl_CMealPrefer.SelectedValue, seat, _
                  "false", "", gender, Ex_date_Chd.Text.Trim(), Passport_Chd.Text.Trim(), IssuingCountry_Chd.Value.Trim(), Nationality_Chd.Value.Trim())
                    End If

                    Dim objs As STD.BAL.FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                    If seatlist.Count > 0 Then
                        Dim Fullname As String = ""
                        For s As Integer = 0 To seatlistO.Count - 1
                            Dim SeatIns As STD.Shared.Seat = New STD.Shared.Seat()
                            '' SeatIns.PaxId = PaxId
                            SeatIns.OrderId = trackid
                            SeatIns.SeatDesignator = seatlistO(s).Seat
                            SeatIns.Amount = Convert.ToInt32(seatlistO(s).Amount)
                            SeatIns.Origin = seatlistO(s).Sector.Split("-"c)(0)
                            SeatIns.Destination = seatlistO(s).Sector.Split("-"c)(1)
                            SeatIns.FlightNumber = seatlistO(s).FlightNumber
                            SeatIns.FlightTime = seatlistO(s).FlightTime
                            SeatIns.VC = VCOB
                            SeatIns.SeatAlignment = seatlistO(s).SeatAlignment
                            SeatIns.OptionalServiceRef = seatlistO(s).OptionalServiceRef
                            SeatIns.Group = seatlistO(s).Group
                            SeatIns.ClassOfService = seatlistO(s).ClassOfService
                            SeatIns.Equipment = seatlistO(s).Equipment
                            SeatIns.Carrier = seatlistO(s).Carrier
                            SeatIns.Paid = seatlistO(s).Paid
                            Fullname = seatlistO(s).Title & seatlistO(s).FName & seatlistO(s).LName
                            objs.insertSeat(SeatIns, Fullname)
                        Next
                    End If

                Next
            End If

            If Infant > 0 Then
                Dim counter1 As Integer = 0
                For Each rw As RepeaterItem In Repeater_Infant.Items

                    Dim ddl_ITitle As DropDownList = DirectCast(rw.FindControl("ddl_ITitle"), DropDownList)
                    Dim ddl_IGender As DropDownList = DirectCast(rw.FindControl("ddl_IGender"), DropDownList)
                    Dim txtIFirstName As TextBox = DirectCast(rw.FindControl("txtIFirstName"), TextBox)
                    Dim txtIMiddleName As TextBox = DirectCast(rw.FindControl("txtIMiddleName"), TextBox)
                    If txtIMiddleName.Text = "Middle Name" Then
                        txtIMiddleName.Text = ""
                    End If
                    Dim txtILastName As TextBox = DirectCast(rw.FindControl("txtILastName"), TextBox)

                    Dim txtinfantDOB As TextBox = DirectCast(rw.FindControl("Txt_InfantDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtinfantDOB.Text.Trim
                    'For Each rw1 As RepeaterItem In Repeater_Adult.Items

                    Dim gender As String
                    gender = ddl_IGender.SelectedValue.Trim()
                    'If ddl_ITitle.SelectedValue.Trim.ToLower = "mstr" Then
                    '    gender = "M"

                    'End If
                    '''''''''''''''''''''''' for Infant Possport/and all '''''''''''''''''''''''''''''''''''
                    Dim Passport_Inf As TextBox = DirectCast(rw.FindControl("txt_Passport_Inf"), TextBox)
                    Dim Ex_date_Inf As TextBox = DirectCast(rw.FindControl("txt_ex_date_Inf"), TextBox)
                    Dim IssuingCountry_Inf As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_IssuingCountry_Inf"), HtmlInputHidden)
                    Dim Nationality_Inf As HtmlInputHidden = DirectCast(rw.FindControl("Hdn_Nationality_Inf"), HtmlInputHidden)

                    'If IssuingCountry_Inf.Value = "" Or Nothing Then
                    '    ShowAlertMessage("Invalid country value for infant,please select the valid country")
                    'End If
                    'If Nationality_Inf.Value = "" Or Nothing Then
                    '    ShowAlertMessage("Invalid nationality for infant,please select the valid nationality")
                    'End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim txtAFirstName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAFirstName"), TextBox)
                    Dim txtAMiddleName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAMiddleName"), TextBox)
                    Dim txtALastName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtALastName"), TextBox)

                    'Dim txtAFirstName As TextBox = DirectCast(rw1.FindControl("txtAFirstName"), TextBox)
                    'Dim txtAMiddleName As TextBox = DirectCast(rw1.FindControl("txtAMiddleName"), TextBox)
                    'Dim txtALastName As TextBox = DirectCast(rw1.FindControl("txtALastName"), TextBox)
                    Dim Name As String = ""
                    Name = txtAFirstName.Text.Trim() + txtAMiddleName.Text.Trim() + txtALastName.Text.Trim()
                    If counter1 <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ITitle.SelectedValue, txtIFirstName.Text.Trim(), txtIMiddleName.Text.Trim(), txtILastName.Text.Trim(), _
                         "INF", DOB, "", "", "", "", _
                         "false", Name, gender, Ex_date_Inf.Text.Trim(), Passport_Inf.Text.Trim(), IssuingCountry_Inf.Value.Trim(), Nationality_Inf.Value.Trim())
                    End If
                    counter1 += 1
                Next
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub Repeater_Adult_ItemCreated(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim Flight As Char = "1" 'For RoundTrip SpecialCase'
        Adti = Adti + 1
        If (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And Provider.Trim.ToUpper <> "TB" And Provider.Trim.ToUpper <> "AK" Then
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ds.Clear()
                ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("trackid").ToString(), Flight)

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl2.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl2.DataBind()

                div_ADT.Style("Display") = "block"

                If FLT_STAT = "R" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") Then
                    VCIB = VCOB
                    TripIB = TripOB
                    Flight = "2"
                End If

                If VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8" Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                        Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)


                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                            ddl_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                        Next
                        ddl_Ib.DataBind()

                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                        ds_Ib.Clear()
                        ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("trackid").ToString(), Flight)

                        ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                        For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                        Next
                        ddl2_Ib.DataBind()

                        div_Ib.Style("Display") = "Display:block"
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try

        ElseIf Provider.Trim.ToUpper = "TB" Then

            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage) = objmB.GetTBOBaggage(TBOSSR, "O")


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next


                'ddl.AutoPostBack = True
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ds.Clear()
                ' ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)


                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic) = objmB.GetTBOMeals(TBOSSR, "O")

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next
                ddl2.DataBind()

                div_ADT.Style("Display") = "block"

            Catch ex As Exception

            End Try

            If FLT_STAT = "R" Then
                VCIB = VCOB
                TripIB = TripOB
                Flight = "2"


                Try

                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                    ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                    Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                    Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage)
                    'If FLT_STAT = "RTF" Then
                    '    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                    'Else
                    '    baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                    'End If
                    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To baglist.Count - 1
                        ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next

                    ddl_Ib.DataBind()

                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                    ''ds_Ib.Clear()
                    Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic)
                    'If FLT_STAT = "RTF" Then
                    '            Meallist = objmB.GetTBOMeals(TBOSSR, "R")
                    'Else
                    '            Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")
                    'End If

                    Meallist = objmB.GetTBOMeals(TBOSSR, "R")

                    ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2_Ib.DataBind()

                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If
        ElseIf VCOB = "IX" Then
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To IXBagListO.Count - 1
                    ddl.Items.Add(New ListItem(IXBagListO(i).Description + "--INR" + IXBagListO(i).AmountWithTax.ToString(), IXBagListO(i).CodeType + ":" + IXBagListO(i).SSRCategory + "_" + IXBagListO(i).ServiceID + ":" + IXBagListO(i).Description + ":" + Adti.ToString() + ":" + IXBagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To IXMealListO.Count - 1
                    ddl2.Items.Add(New ListItem(IXMealListO(i).Description + "--INR" + IXMealListO(i).AmountWithTax.ToString(), IXMealListO(i).CodeType + ":" + IXMealListO(i).SSRCategory + "_" + IXMealListO(i).ServiceID + ":" + IXMealListO(i).Description + ":" + Adti.ToString() + ":" + IXMealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl2.DataBind()


                'Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("SeattypeA"), DropDownList)
                'ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))
                'For i As Integer = 0 To IXSeatListO.Count - 1
                '    ddl3.Items.Add(New ListItem(IXSeatListO(i).Description + "--INR" + IXSeatListO(i).AmountWithTax.ToString(), IXSeatListO(i).CodeType + ":" + IXSeatListO(i).SSRCategory + "_" + IXSeatListO(i).ServiceID + ":" + IXSeatListO(i).Description + ":" + Adti.ToString() + ":" + IXSeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                'Next
                'ddl3.DataBind()


                div_ADT.Style("Display") = "block"

            Catch ex As Exception

            End Try

            If FLT_STAT = "R" Then
                VCIB = VCOB
                TripIB = TripOB
                Flight = "2"
                Try

                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To IXBagListR.Count - 1
                        ddl_Ib.Items.Add(New ListItem(IXBagListR(i).Description + "--INR" + IXBagListR(i).AmountWithTax.ToString(), IXBagListR(i).CodeType + ":" + IXBagListR(i).SSRCategory + "_" + IXBagListR(i).ServiceID + ":" + IXBagListR(i).Description + ":" + Adti.ToString() + ":" + IXBagListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                    ddl_Ib.DataBind()
                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                    ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To IXMealListR.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(IXMealListR(i).Description + "--INR" + IXMealListR(i).AmountWithTax.ToString(), IXMealListR(i).CodeType + ":" + IXMealListR(i).SSRCategory + "_" + IXMealListR(i).ServiceID + ":" + IXMealListR(i).Description + ":" + Adti.ToString() + ":" + IXMealListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                    ddl2_Ib.DataBind()


                    'Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("SeattypeR"), DropDownList)
                    'ddl3_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    'For i As Integer = 0 To IXSeatListR.Count - 1
                    '    ddl3_Ib.Items.Add(New ListItem(IXSeatListR(i).Description + "--INR" + IXSeatListR(i).AmountWithTax.ToString(), IXSeatListR(i).CodeType + ":" + IXSeatListR(i).SSRCategory + "_" + IXSeatListR(i).ServiceID + ":" + IXSeatListR(i).Description + ":" + Adti.ToString() + ":" + IXSeatListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    'Next
                    'ddl3_Ib.DataBind()
                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If
        ElseIf VCOB = "FZ" Then
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)
                Dim div_mealO_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealO_ADT"), HtmlControls.HtmlGenericControl)
                Dim div_mealOD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealOD_ADT"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                '' Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)
                div_mealO_ADT.Style("Display") = "none"
                div_mealOD_ADT.Style("Display") = "none"


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To FZBagListO.Count - 1
                    ddl.Items.Add(New ListItem(FZBagListO(i).Description + "--INR" + FZBagListO(i).AmountWithTax.ToString(), FZBagListO(i).CodeType + ":" + FZBagListO(i).SSRCategory + "_" + FZBagListO(i).ServiceID + ":" + FZBagListO(i).Description + ":" + Adti.ToString() + ":" + FZBagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()
                If FZBagListO.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                Else
                    div_ADT.Style("Display") = "none"
                End If

                If FZBagListR.Count > 0 Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)
                        Dim div_mealR_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealR_ADT"), HtmlControls.HtmlGenericControl)
                        Dim div_mealRD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealRD_ADT"), HtmlControls.HtmlGenericControl)

                        div_mealR_ADT.Style("Display") = "none"
                        div_mealRD_ADT.Style("Display") = "none"

                        ' Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)


                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To FZBagListR.Count - 1
                            ddl_Ib.Items.Add(New ListItem(FZBagListR(i).Description + "--INR" + FZBagListR(i).AmountWithTax.ToString(), FZBagListR(i).CodeType + ":" + FZBagListR(i).SSRCategory + "_" + FZBagListR(i).ServiceID + ":" + FZBagListR(i).Description + ":" + Adti.ToString() + ":" + FZBagListR(i).Amount.ToString()))
                        Next
                        ddl_Ib.DataBind()
                        div_Ib.Style("Display") = "Display:block"
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        ElseIf VCOB = "AK" Or VCOB = "I5" Or VCOB = "D7" Or VCOB = "FD" Or VCOB = "QZ" Or VCOB = "Z2" Or VCOB = "XJ" Or VCOB = "XT" Or VCOB = "DJ" Then
            Try
                Dim sno As String() = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim segmentcount As Integer = Convert.ToInt32(sno(6))

                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim()).ToList()
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim()).ToList()

                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                    For i As Integer = 0 To baglist.Count - 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_A_Bag"), HtmlGenericControl).Style.Add("display", "none")
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                    ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_A_Meal"), HtmlGenericControl).Style.Add("display", "none")
                End If

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                End If


                If OBFltDs.Tables(0).Rows.Count > 1 Then
                    Dim sector1 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    Dim sector2 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString() + "</div>"
                    DirectCast(e.Item.FindControl("Seg1_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                    DirectCast(e.Item.FindControl("Seg2_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                    DirectCast(e.Item.FindControl("Seg2_Ob"), HtmlGenericControl).Style.Add("display", "block")

                    If (segmentcount > 1) Then
                        baglist = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If baglist.Count > 0 Then
                            Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob_Seg2"), DropDownList)
                            ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                            For i As Integer = 0 To baglist.Count - 1
                                ddlSeg_Ob.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddlSeg_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_A_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                        End If
                        If Meallist.Count > 0 Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))

                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_A_ExtraMeal"), HtmlGenericControl).Style.Add("display", "none")
                        End If
                    Else
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If Meallist.Count > 0 Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))

                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_Ob"), HtmlGenericControl).Style.Add("display", "none")
                        End If
                    End If
                End If
            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Adult_ItemCreated Air Asia", "Error_001", ex, "Flight")
            End Try
            'AirAsia Mael and Baggage end
        ElseIf Provider.Trim.ToUpper = "LCC" And VCOB = "G9" Then
            Try
                Dim RoundTrip As DataRow() = OBFltDs.Tables(0).Select("TripType='R'")
                Dim Onway As DataRow() = OBFltDs.Tables(0).Select("TripType='O'")
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim baglist As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Baggage" And ssr.WayType = "O").ToList()
                Dim Meallist As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = Onway(0)("DepartureLocation").ToString().Trim() And ssr.Destination = Onway(0)("ArrivalLocation").ToString().Trim()).ToList()

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                End If
                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To baglist.Count - 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_A_Bag"), HtmlGenericControl).Style.Add("display", "none")
                End If

                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                    ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_A_Meal"), HtmlGenericControl).Style.Add("display", "none")
                End If ' 'Seg2_ExtraCBag Seg2_ExtraCMeal Seg1_C_Meal  Seg1_C_Bag
                If Onway.Length > 1 Then
                    Dim Meallist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = Onway(1)("DepartureLocation").ToString().Trim() And ssr.Destination = Onway(1)("ArrivalLocation").ToString().Trim()).ToList()
                    If Meallist2.Count > 0 Then
                        Dim sector1 As String = "<div class='tablinks'>Sector:" + Onway(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + Onway(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sector2 As String = "<div class='tablinks'>Sector:" + Onway(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + Onway(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg1_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                        DirectCast(e.Item.FindControl("Seg2_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                        DirectCast(e.Item.FindControl("Seg2_Ob"), HtmlGenericControl).Style.Add("display", "block")
                        DirectCast(e.Item.FindControl("Seg2_A_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                        Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                        ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))

                        For i As Integer = 0 To Meallist2.Count - 1
                            ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddl2Seg2_Ob.DataBind()
                    End If

                End If
                If RoundTrip.Length > 0 Then
                    Dim div_ADT_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)
                    Dim baglist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Baggage" And ssr.WayType = "R").ToList()
                    Dim Meallist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = RoundTrip(0)("DepartureLocation").ToString().Trim() And ssr.Destination = RoundTrip(0)("ArrivalLocation").ToString().Trim()).ToList()

                    If Meallist2.Count > 0 Or baglist2.Count > 0 Then
                        div_ADT_Ib.Style("Display") = "block"
                    End If
                    If baglist2.Count > 0 Then
                        Dim ddlSeg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        ddlSeg_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                        For i As Integer = 0 To baglist2.Count - 1
                            ddlSeg_Ib.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddlSeg_Ib.DataBind()
                    Else
                        DirectCast(e.Item.FindControl("Seg2_A_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                    End If
                    If Meallist2.Count > 0 Then
                        Dim ddl2Seg1_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ib"), DropDownList)
                        ddl2Seg1_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))

                        For i As Integer = 0 To Meallist2.Count - 1
                            ddl2Seg1_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddl2Seg1_Ib.DataBind()
                    End If
                    If RoundTrip.Length > 1 Then
                        Dim sectorO As String = "<div class='tablinks'>Sector:" + RoundTrip(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + RoundTrip(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sectorR As String = "<div class='tablinks'>Sector:" + RoundTrip(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + RoundTrip(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg1_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sectorO
                        DirectCast(e.Item.FindControl("Seg2_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sectorR

                        Dim Meallist3 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = RoundTrip(1)("DepartureLocation").ToString().Trim() And ssr.Destination = RoundTrip(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If Meallist3.Count > 0 Then
                            div_ADT_Ib.Style("Display") = "block"
                            DirectCast(e.Item.FindControl("Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")
                            DirectCast(e.Item.FindControl("Seg2_A_Ib_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                            Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ib_Seg2"), DropDownList)
                            ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))

                            For i As Integer = 0 To Meallist3.Count - 1
                                ddl2Seg2_Ib.Items.Add(New ListItem(Meallist3(i).Description + "--INR" + Meallist3(i).Amount.ToString(), Meallist3(i).Code + ":" + Meallist3(i).Description + "_" + Meallist3(i).Origin + "_" + Meallist3(i).Destination + ":" + Meallist3(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ib.DataBind()
                        End If
                    End If
                End If

            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Adult_ItemCreated Air Arabia", "Error_001", ex, "Flight")
            End Try
        Else
            DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "block")
        End If


    End Sub
    Protected Sub Repeater_Child_ItemCreated(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim Flight As Char = "1" 'For RoundTrip SpecialCase'
        Chdi = Chdi + 1
        If (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And Provider.Trim.ToUpper <> "TB" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next


                ds.Clear()
                ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("trackid").ToString(), Flight)

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl2.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next

                div_CHD.Style("Display") = "block"
            Catch ex As Exception

            End Try

            If FLT_STAT = "R" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") Then
                VCIB = VCOB
                TripIB = TripOB
                Flight = "2"
            End If

            If VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8" Then
                Try
                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                    Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCOB)


                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                        ddl_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                    Next

                    ds_Ib.Clear()
                    ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("trackid").ToString(), Flight)

                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                    ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                    Next

                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If

        ElseIf Provider.Trim.ToUpper = "TB" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                ''Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage) = objmB.GetTBOBaggage(TBOSSR, "O")


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next




                'ds.Clear()
                'ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)
                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic) = objmB.GetTBOMeals(TBOSSR, "O")

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next



                div_CHD.Style("Display") = "block"


            Catch ex As Exception

            End Try


            If FLT_STAT = "R" Then
                VCIB = VCOB
                TripIB = TripOB
                Flight = "2"

                Try
                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                    ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                    Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                    Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage)
                    'If FLT_STAT = "RTF" Then
                    '    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                    'Else
                    '    baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                    'End If
                    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To baglist.Count - 1
                        ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next



                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                    ''ds_Ib.Clear()
                    Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic)
                    'If FLT_STAT = "RTF" Then
                    '    Meallist = objmB.GetTBOMeals(TBOSSR, "R")
                    'Else
                    '    Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")
                    'End If

                    Meallist = objmB.GetTBOMeals(TBOSSR, "R")

                    ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2_Ib.DataBind()


                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If
        ElseIf VCOB = "IX" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To IXBagListO.Count - 1
                    ddl.Items.Add(New ListItem(IXBagListO(i).Description + "--INR" + IXBagListO(i).AmountWithTax.ToString(), IXBagListO(i).CodeType + ":" + IXBagListO(i).SSRCategory + "_" + IXBagListO(i).ServiceID + ":" + IXBagListO(i).Description + ":" + Adti.ToString() + ":" + IXBagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To IXMealListO.Count - 1
                    ddl2.Items.Add(New ListItem(IXMealListO(i).Description + "--INR" + IXMealListO(i).AmountWithTax.ToString(), IXMealListO(i).CodeType + ":" + IXMealListO(i).SSRCategory + "_" + IXMealListO(i).ServiceID + ":" + IXMealListO(i).Description + ":" + Adti.ToString() + ":" + IXMealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl2.DataBind()


                Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("SeattypeOC"), DropDownList)
                ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))
                For i As Integer = 0 To IXSeatListO.Count - 1
                    ddl3.Items.Add(New ListItem(IXSeatListO(i).Description + "--INR" + IXSeatListO(i).AmountWithTax.ToString(), IXSeatListO(i).CodeType + ":" + IXSeatListO(i).SSRCategory + "_" + IXSeatListO(i).ServiceID + ":" + IXSeatListO(i).Description + ":" + Adti.ToString() + ":" + IXSeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl3.DataBind()


                div_CHD.Style("Display") = "block"

            Catch ex As Exception

            End Try

            If FLT_STAT = "R" Then
                VCIB = VCOB
                TripIB = TripOB
                Flight = "2"
                Try

                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To IXBagListR.Count - 1
                        ddl_Ib.Items.Add(New ListItem(IXBagListR(i).Description + "--INR" + IXBagListR(i).AmountWithTax.ToString(), IXBagListR(i).CodeType + ":" + IXBagListR(i).SSRCategory + "_" + IXBagListR(i).ServiceID + ":" + IXBagListR(i).Description + ":" + Adti.ToString() + ":" + IXBagListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                    ddl_Ib.DataBind()
                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_C_Meal_Ib"), DropDownList)
                    ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To IXMealListR.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(IXMealListR(i).Description + "--INR" + IXMealListR(i).AmountWithTax.ToString(), IXMealListR(i).CodeType + ":" + IXMealListR(i).SSRCategory + "_" + IXMealListR(i).ServiceID + ":" + IXMealListR(i).Description + ":" + Adti.ToString() + ":" + IXMealListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                    ddl2_Ib.DataBind()


                    Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("SeattypeOR"), DropDownList)
                    ddl3_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To IXSeatListR.Count - 1
                        ddl3_Ib.Items.Add(New ListItem(IXSeatListR(i).Description + "--INR" + IXSeatListR(i).AmountWithTax.ToString(), IXSeatListR(i).CodeType + ":" + IXSeatListR(i).SSRCategory + "_" + IXSeatListR(i).ServiceID + ":" + IXSeatListR(i).Description + ":" + Adti.ToString() + ":" + IXSeatListR(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                    ddl3_Ib.DataBind()
                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If
         
        ElseIf VCOB = "FZ" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim div_mealO_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealO_CHD"), HtmlControls.HtmlGenericControl)
                Dim div_mealOD_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealOD_CHD"), HtmlControls.HtmlGenericControl)

                div_mealO_CHD.Style("Display") = "none"
                div_mealOD_CHD.Style("Display") = "none"

                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                'Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To FZBagListO.Count - 1
                    ddl.Items.Add(New ListItem(FZBagListO(i).Description + "--INR" + FZBagListO(i).AmountWithTax.ToString(), FZBagListO(i).CodeType + ":" + FZBagListO(i).SSRCategory + "_" + FZBagListO(i).ServiceID + ":" + FZBagListO(i).Description + ":" + Chdi.ToString() + ":" + FZBagListO(i).Amount.ToString()))
                Next

                If FZBagListO.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                Else
                    div_CHD.Style("Display") = "none"
                End If

            Catch ex As Exception

            End Try

            If FZBagListR.Count > 0 Then
                Try
                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                    Dim div_mealR_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealR_CHD"), HtmlControls.HtmlGenericControl)
                    Dim div_mealRD_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealRD_CHD"), HtmlControls.HtmlGenericControl)

                    div_mealR_CHD.Style("Display") = "none"
                    div_mealRD_CHD.Style("Display") = "none"

                    ' Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCOB)


                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To FZBagListR.Count - 1
                        ddl_Ib.Items.Add(New ListItem(FZBagListR(i).Description + "--INR" + FZBagListR(i).AmountWithTax.ToString(), FZBagListR(i).CodeType + ":" + FZBagListR(i).SSRCategory + "_" + FZBagListR(i).ServiceID + ":" + FZBagListR(i).Description + ":" + Chdi.ToString() + ":" + FZBagListR(i).Amount.ToString()))
                    Next

                    div_Ib.Style("Display") = "block"
                Catch ex As Exception

                End Try
            End If
        ElseIf VCOB = "AK" Or VCOB = "I5" Or VCOB = "D7" Or VCOB = "FD" Or VCOB = "QZ" Or VCOB = "Z2" Or VCOB = "XJ" Or VCOB = "XT" Or VCOB = "DJ" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR)
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR)
                baglist = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim()).ToList()
                Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim()).ToList()

                If baglist.Count > 0 Or Meallist.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                End If
                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                    For i As Integer = 0 To baglist.Count - 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_C_Bag"), HtmlGenericControl).Style.Add("display", "none")
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                    ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))

                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description.ToString() + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_C_Meal"), HtmlGenericControl).Style.Add("display", "none")
                End If
                If OBFltDs.Tables(0).Rows.Count > 1 Then

                    Dim sno As String() = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                    Dim segmentcount As Integer = Convert.ToInt32(sno(6))
                    Dim sector As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    Dim sector2 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString() + "</div>"
                    DirectCast(e.Item.FindControl("Seg2_C_Ob"), HtmlGenericControl).Style.Add("display", "block")
                    DirectCast(e.Item.FindControl("Seg1_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector
                    DirectCast(e.Item.FindControl("Seg2_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2

                    If segmentcount > 1 Then
                        baglist = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If (baglist.Count > 0) Then
                            Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob_Seg2"), DropDownList)
                            ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                            For i As Integer = 0 To baglist.Count - 1
                                ddlSeg_Ob.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddlSeg_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_ExtraCBag"), HtmlGenericControl).Style.Add("display", "none")
                        End If
                        If (Meallist.Count > 0) Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_ExtraCMeal"), HtmlGenericControl).Style.Add("display", "none")
                        End If
                    Else
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If (Meallist.Count > 0) Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))

                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        Else
                            DirectCast(e.Item.FindControl("Seg2_C_Ob"), HtmlGenericControl).Style.Add("display", "block")
                        End If
                    End If

                End If

            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Child_ItemCreated Air Asia", "Error_001", ex, "AirAsiaFlight")
            End Try
        ElseIf Provider.Trim.ToUpper = "LCC" And VCOB = "G9" Then
            Try
                Dim RoundTrip As DataRow() = OBFltDs.Tables(0).Select("TripType='R'")
                Dim Onway As DataRow() = OBFltDs.Tables(0).Select("TripType='O'")
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHILD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)

                Dim baglist As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Baggage" And ssr.WayType = "O").ToList()
                Dim Meallist As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = Onway(0)("DepartureLocation").ToString().Trim() And ssr.Destination = Onway(0)("ArrivalLocation").ToString().Trim()).ToList()

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_CHILD.Style("Display") = "block"
                End If
                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To baglist.Count - 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Chdi.ToString()))
                    Next
                    ddl.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_C_Bag"), HtmlGenericControl).Style.Add("display", "none")
                End If
                'Seg2_ExtraCMeal Seg2_ExtraCBag
                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                    ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Chdi.ToString()))
                    Next
                    ddl2.DataBind()
                Else
                    DirectCast(e.Item.FindControl("Seg1_C_Meal"), HtmlGenericControl).Style.Add("display", "none")
                End If
                If Onway.Length > 1 Then
                    Dim Meallist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = Onway(1)("DepartureLocation").ToString().Trim() And ssr.Destination = Onway(1)("ArrivalLocation").ToString().Trim()).ToList()
                    If Meallist2.Count > 0 Then
                        Dim sector As String = "<div class='tablinks'>Sector:" + Onway(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + Onway(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sector2 As String = "<div class='tablinks'>Sector:" + Onway(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + Onway(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg2_C_Ob"), HtmlGenericControl).Style.Add("display", "block")
                        DirectCast(e.Item.FindControl("Seg1_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector
                        DirectCast(e.Item.FindControl("Seg2_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                        DirectCast(e.Item.FindControl("Seg2_ExtraCBag"), HtmlGenericControl).Style.Add("display", "none")
                        Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                        ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))

                        For i As Integer = 0 To Meallist2.Count - 1
                            ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Chdi.ToString()))
                        Next
                        ddl2Seg2_Ob.DataBind()
                    End If
                End If
                If RoundTrip.Length > 0 Then
                    Dim div_CHD_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                    Dim baglist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Baggage" And ssr.WayType = "R").ToList()
                    Dim Meallist2 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = RoundTrip(0)("DepartureLocation").ToString().Trim() And ssr.Destination = RoundTrip(0)("ArrivalLocation").ToString().Trim()).ToList()
                    If Meallist2.Count > 0 Or baglist2.Count > 0 Then
                        div_CHD_Ib.Style("Display") = "block"
                    End If
                    If baglist2.Count > 0 Then
                        Dim ddlSeg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                        ddlSeg_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                        For i As Integer = 0 To baglist2.Count - 1
                            ddlSeg_Ib.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddlSeg_Ib.DataBind()
                    Else
                        DirectCast(e.Item.FindControl("Seg2_C_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                    End If
                    If Meallist2.Count > 0 Then
                        Dim ddl2Seg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                        ddl2Seg_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))

                        For i As Integer = 0 To Meallist2.Count - 1
                            ddl2Seg_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddl2Seg_Ib.DataBind()
                    End If
                    If RoundTrip.Length > 1 Then
                        Dim sectorO As String = "<div class='tablinks'>Sector:" + RoundTrip(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + RoundTrip(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sectorR As String = "<div class='tablinks'>Sector:" + RoundTrip(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + RoundTrip(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg1_C_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sectorO
                        DirectCast(e.Item.FindControl("Seg2_C_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sectorR

                        Dim Meallist3 As List(Of GALWS.AirArabia.AirArabiaSSR) = (From ssr In G9SSR Where ssr.SSRType = "Meal" And ssr.Origin = RoundTrip(1)("DepartureLocation").ToString().Trim() And ssr.Destination = RoundTrip(1)("ArrivalLocation").ToString().Trim()).ToList()
                        If Meallist3.Count > 0 Then
                            div_CHD_Ib.Style("Display") = "block"
                            DirectCast(e.Item.FindControl("Seg2_C_Ib"), HtmlGenericControl).Style.Add("display", "block")
                            DirectCast(e.Item.FindControl("Seg2_C_Ib_ExtraBag"), HtmlGenericControl).Style.Add("display", "none")
                            Dim ddl2Seg3_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib_Seg2"), DropDownList)
                            ddl2Seg3_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))

                            For i As Integer = 0 To Meallist3.Count - 1
                                ddl2Seg3_Ib.Items.Add(New ListItem(Meallist3(i).Description + "--INR" + Meallist3(i).Amount.ToString(), Meallist3(i).Code + ":" + Meallist3(i).Description + "_" + Meallist3(i).Origin + "_" + Meallist3(i).Destination + ":" + Meallist3(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg3_Ib.DataBind()
                        End If
                    End If
                End If
            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Child_ItemCreated AirArabia", "Error_001", ex, "Flight")
            End Try
        Else
            DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "block")
        End If
    End Sub

    Public Sub Insert_MEAL_BAG_Detail(ByVal trackid As String, ByVal FltDs As DataSet, ByVal Paxdt As DataSet, ByVal Type As String, ByVal TripType As String)
        Try
            Adult = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult"))
            Child = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child"))
            Infant = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant"))

            'NO Need for this only assign Code in the DDL option value
            Dim MealCd_OB As String = "", BagCd_OB As String = "", MealPr_OB As Decimal = 0, BagPr_OB As Decimal = 0
            Dim MealCd_IB As String = "", BagCd_IB As String = "", MealPr_IB As Decimal = 0, BagPr_IB As Decimal = 0
            Dim counter As Integer = 0
            Dim Dt, Dt_Seg2 As New DataTable
            getTableColumn(Dt)
            getTableColumn(Dt_Seg2)
            Dim VCR As String = FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim()
            If (Type = "OB") Then

                Split_MB_Detail(lbl_A_MB_OB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                If (Child > 0) Then
                    Split_MB_Detail(lbl_C_MB_OB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                End If
                CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier")) ', FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                ''Code To insert in T_Flt_Meal_And_Baggage_Request
                If (VCR = "AK" Or VCR = "I5" Or VCR = "D7" Or VCR = "FD" Or VCR = "QZ" Or VCR = "Z2" Or VCR = "XJ" Or VCR = "XT" Or VCR = "DJ" Or VCR = "G9" And FltDs.Tables(0).Rows.Count > 1) Then
                    Split_MB_Detail(lbl_A_MB_OB_Seg2.Value, Adult, Dt, "ADT", VCR, FltDs.Tables(0).Rows(1)("Provider").ToString().Trim())
                    If (Child > 0) Then
                        Split_MB_Detail(lbl_C_MB_OB_Seg2.Value, Child, Dt, "CHD", VCR, FltDs.Tables(0).Rows(1)("Provider").ToString().Trim())
                    End If
                    CreateFinalTable(Dt_Seg2, Adult, Child, Paxdt.Tables(0), trackid, TripType, VCR)
                End If
            ElseIf (Type = "IB") Then

                Split_MB_Detail(lbl_A_MB_IB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                If (Child > 0) Then
                    Split_MB_Detail(lbl_C_MB_IB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                End If
                CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier")) ', FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                ''Code To insert in T_Flt_Meal_And_Baggage_Request
                If (FltDs.Tables(0).Rows(0)("Provider").ToString().Trim() = "AK" And FltDs.Tables(0).Rows.Count > 1) Then
                    Split_MB_Detail(lbl_A_MB_IB_Seg2.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(1)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(1)("Provider").ToString().Trim())
                    If (Child > 0) Then
                        Split_MB_Detail(lbl_C_MB_IB_Seg2.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(1)("ValiDatingCarrier").ToString().Trim(), FltDs.Tables(0).Rows(1)("Provider").ToString().Trim())
                    End If
                    CreateFinalTable(Dt_Seg2, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(1)("ValiDatingCarrier")) ', FltDs.Tables(0).Rows(0)("Provider").ToString().Trim())
                End If
                End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Public Sub Split_MB_Detail(ByVal Text As String, ByVal Pxcnt As Integer, ByRef Dt As DataTable, ByVal PaxType As String, ByVal VC As String, ByVal Prvdr As String)

        Dim MB() As String
        Try
            MB = Text.Split("#")
            Dim tax() As String
            tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            If Prvdr = "TB" Then
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()

                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split("@")(0).Split(":")(1)
                    dr("MealCategory") = tax(i).Split("@")(0).Split(":")(2)
                    dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ") Then
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split(":")(1)
                    dr("MealCategory") = ""
                    dr("MealPriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "G9") Then
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Split(":")(0).Replace(":", "")
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split(":")(1)
                    dr("MealCategory") = ""
                    dr("MealPriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "IX") Then
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Split(":")(0).Replace(":", "")
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("MealCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
            Else

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
                    dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = ""
                    dr("MealCategory") = ""
                    dr("MealPriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next

            End If
            ''Baggage
            Array.Clear(tax, 0, tax.Length)
            tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            If VC = "FZ" Then
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()

                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
            ElseIf VC = "IX" Then
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()

                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next

            ElseIf Prvdr.Trim() = "TB" Then

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()


                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(2)

                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ") Then
                ''Baggage
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split(":")(1)
                    dr("BaggageCategory") = ""
                    dr("BaggagePriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "G9") Then
                ''Baggage
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 5)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split(":")(1)
                    dr("BaggageCategory") = ""
                    dr("BaggagePriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
            Else

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
                    dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = ""
                    dr("BaggageCategory") = ""
                    dr("BaggagePriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next

            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Public Function getTableColumn(ByRef dt As DataTable) As DataTable

        Try
            dt.Columns.Add("BookingRefNo", GetType(String))
            dt.Columns.Add("Flt_HeaderID", GetType(String))
            dt.Columns.Add("PaxID", GetType(Integer))
            dt.Columns.Add("PaxType", GetType(String)) 'Extra
            dt.Columns.Add("TripType", GetType(String))
            dt.Columns.Add("MealCode", GetType(String))
            dt.Columns.Add("MealPrice", GetType(Decimal))
            dt.Columns.Add("MealDesc", GetType(String))
            dt.Columns.Add("MealCategory", GetType(String))
            dt.Columns.Add("MealPriceWithNoTax", GetType(Decimal))
            dt.Columns.Add("BaggageCode", GetType(String))
            dt.Columns.Add("BaggagePrice", GetType(Decimal))
            dt.Columns.Add("AirLineCode", GetType(String))
            dt.Columns.Add("BaggageDesc", GetType(String))
            dt.Columns.Add("BaggageCategory", GetType(String))
            dt.Columns.Add("BaggagePriceWithNoTax", GetType(Decimal))
            dt.Columns.Add("SeatCode", GetType(String))
            dt.Columns.Add("SeatPrice", GetType(Decimal))
            dt.Columns.Add("SeatDesc", GetType(String))
            dt.Columns.Add("SeatCategory", GetType(String))
            dt.Columns.Add("SeatPriceWithNoTax", GetType(Decimal))

            Return dt
        Catch ex As Exception
            Return dt
        End Try
    End Function
    Public Function CreateFinalTable(ByVal Dt As DataTable, ByVal Adult As Integer, ByVal Child As Integer, ByVal Paxdt As DataTable, ByVal OID As String, ByVal Trip As String, ByVal VC As String) As DataTable
        Dim Ft As New DataTable
        getTableColumn(Ft)
        Try
            If VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ" Or VC = "G9" Then
                For i As Integer = 1 To Adult
                    Dim fltml As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + " AND MealPrice Is Not NULL")
                    Dim fltbg As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + " AND BaggageCode Is Not NULL")
                    Dim fltse As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + " AND SeatPrice Is Not NULL")

                    Dim rownummber As Integer = 0
                    If fltml.Length >= fltbg.Length And fltml.Length >= fltse.Length Then
                        rownummber = fltml.Length
                    ElseIf fltbg.Length >= fltml.Length And fltbg.Length >= fltse.Length Then
                        rownummber = fltbg.Length
                    Else
                        rownummber = fltse.Length
                    End If

                    For j As Integer = 0 To rownummber - 1
                        Dim dr As DataRow = Ft.NewRow()
                        dr("PaxID") = Paxdt.Rows(i - 1)("PaxID")
                        dr("Flt_HeaderID") = Paxdt.Rows(i - 1)("Flt_HeaderID")
                        If (fltml.Length > j) Then
                            dr("MealCode") = fltml(j)("MealCode")
                            dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                            dr("MealDesc") = fltml(j)("MealDesc")
                            dr("MealCategory") = fltml(j)("MealCategory")
                            dr("MealPriceWithNoTax") = fltml(j)("MealPriceWithNoTax")
                        Else
                            dr("MealCode") = ""
                            dr("MealPrice") = 0
                            dr("MealDesc") = ""
                            dr("MealCategory") = ""
                            dr("MealPriceWithNoTax") = 0
                        End If
                        If (fltbg.Length > j) Then
                            dr("BaggageCode") = fltbg(j)("BaggageCode")
                            dr("BaggagePrice") = Convert.ToDecimal(fltbg(j)("BaggagePrice"))
                            dr("BaggageDesc") = fltbg(j)("BaggageDesc")
                            dr("BaggageCategory") = fltbg(j)("BaggageCategory")
                            dr("BaggagePriceWithNoTax") = fltbg(j)("BaggagePriceWithNoTax")

                        Else
                            dr("BaggageCode") = ""
                            dr("BaggagePrice") = 0
                            dr("BaggageDesc") = ""
                            dr("BaggageCategory") = ""
                            dr("BaggagePriceWithNoTax") = 0
                        End If

                        If (fltse.Length > 0) Then
                            dr("SeatCode") = fltse(0)("SeatCode")
                            dr("SeatPrice") = Convert.ToDecimal(fltse(j)("SeatPrice"))
                            dr("SeatDesc") = fltse(j)("SeatDesc")
                            dr("SeatCategory") = fltse(j)("SeatCategory")
                            dr("SeatPriceWithNoTax") = fltse(j)("SeatPriceWithNoTax")
                        Else
                            dr("SeatCode") = ""
                            dr("SeatPrice") = 0
                            dr("SeatDesc") = ""
                            dr("SeatCategory") = ""
                            dr("SeatPriceWithNoTax") = 0
                        End If

                        dr("TripType") = Trip
                        Ft.Rows.Add(dr)

                        'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                        If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                            Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LABAK:
                            If (ret > 0) Then
                            Else
                                ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                                GoTo LABAK
                            End If
                        End If
                    Next
                Next
                If (Child > 0) Then
                    For i As Integer = 1 To Child
                        Dim fltml As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + " AND MealCode Is Not NULL")
                        Dim fltbg As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + " AND BaggageCode Is Not NULL")
                        Dim fltse As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + " AND SeatCode Is Not NULL")

                        Dim rownummber As Integer = 0
                        If fltml.Length >= fltbg.Length And fltml.Length >= fltse.Length Then
                            rownummber = fltml.Length
                        ElseIf fltbg.Length >= fltml.Length And fltbg.Length >= fltse.Length Then
                            rownummber = fltbg.Length
                        Else
                            rownummber = fltse.Length
                        End If

                        For j As Integer = 0 To rownummber - 1
                            Dim dr As DataRow = Ft.NewRow()
                            dr("PaxID") = Paxdt.Rows((Adult + i) - 1)("PaxID")
                            dr("Flt_HeaderID") = Paxdt.Rows((Adult + i) - 1)("Flt_HeaderID")
                            If (fltml.Length > j) Then
                                dr("MealCode") = fltml(j)("MealCode")
                                dr("MealPrice") = Convert.ToDecimal(fltml(j)("MealPrice"))
                                dr("MealDesc") = fltml(j)("MealDesc")
                                dr("MealCategory") = fltml(j)("MealCategory")
                                dr("MealPriceWithNoTax") = fltml(j)("MealPriceWithNoTax")
                            Else
                                dr("MealCode") = ""
                                dr("MealPrice") = 0
                                dr("MealDesc") = ""
                                dr("MealCategory") = ""
                                dr("MealPriceWithNoTax") = 0
                            End If
                            If (fltbg.Length > j) Then
                                dr("BaggageCode") = fltbg(j)("BaggageCode")
                                dr("BaggagePrice") = Convert.ToDecimal(fltbg(j)("BaggagePrice"))
                                dr("BaggageDesc") = fltbg(j)("BaggageDesc")
                                dr("BaggageCategory") = fltbg(j)("BaggageCategory")
                                dr("BaggagePriceWithNoTax") = fltbg(j)("BaggagePriceWithNoTax")
                            Else
                                dr("BaggageCode") = ""
                                dr("BaggagePrice") = 0
                                dr("BaggageDesc") = ""
                                dr("BaggageCategory") = ""
                                dr("BaggagePriceWithNoTax") = 0
                            End If

                            If (fltse.Length > 0) Then
                                dr("SeatCode") = fltse(0)("SeatCode")
                                dr("SeatPrice") = Convert.ToDecimal(fltse(j)("SeatPrice"))
                                dr("SeatDesc") = fltse(j)("SeatDesc")
                                dr("SeatCategory") = fltse(j)("SeatCategory")
                                dr("SeatPriceWithNoTax") = fltse(j)("SeatPriceWithNoTax")
                            Else
                                dr("SeatCode") = ""
                                dr("SeatPrice") = 0
                                dr("SeatDesc") = ""
                                dr("SeatCategory") = ""
                                dr("SeatPriceWithNoTax") = 0
                            End If
                            dr("TripType") = Trip
                            Ft.Rows.Add(dr)
                            'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                            If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                                Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LAB2AK:
                                If (ret > 0) Then
                                Else
                                    ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                                    GoTo LAB2AK
                                End If
                            End If
                        Next
                    Next

                End If
            Else
                For i As Integer = 1 To Adult

                    Dim fltml As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND MealPrice >0")
                    Dim fltbg As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND BaggagePrice >0")
                    Dim fltse As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND SeatPrice >0")
                    Dim dr As DataRow = Ft.NewRow()
                    dr("PaxID") = Paxdt.Rows(i - 1)("PaxID")
                    dr("Flt_HeaderID") = Paxdt.Rows(i - 1)("Flt_HeaderID")
                    If (fltml.Length > 0) Then
                        dr("MealCode") = fltml(0)("MealCode")
                        dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                        dr("MealDesc") = fltml(0)("MealDesc")
                        dr("MealCategory") = fltml(0)("MealCategory")
                        dr("MealPriceWithNoTax") = fltml(0)("MealPriceWithNoTax")
                    Else
                        dr("MealCode") = ""
                        dr("MealPrice") = 0
                        dr("MealDesc") = ""
                        dr("MealCategory") = ""
                        dr("MealPriceWithNoTax") = 0
                    End If
                    If (fltbg.Length > 0) Then
                        dr("BaggageCode") = fltbg(0)("BaggageCode")
                        dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
                        dr("BaggageDesc") = fltbg(0)("BaggageDesc")
                        dr("BaggageCategory") = fltbg(0)("BaggageCategory")
                        dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")

                    Else
                        dr("BaggageCode") = ""
                        dr("BaggagePrice") = 0
                        dr("BaggageDesc") = ""
                        dr("BaggageCategory") = ""
                        dr("BaggagePriceWithNoTax") = 0
                    End If

                    If (fltse.Length > 0) Then
                        dr("SeatCode") = fltse(0)("SeatCode")
                        dr("SeatPrice") = Convert.ToDecimal(fltse(0)("SeatPrice"))
                        dr("SeatDesc") = fltse(0)("SeatDesc")
                        dr("SeatCategory") = fltse(0)("SeatCategory")
                        dr("SeatPriceWithNoTax") = fltse(0)("SeatPriceWithNoTax")
                    Else
                        dr("SeatCode") = ""
                        dr("SeatPrice") = 0
                        dr("SeatDesc") = ""
                        dr("SeatCategory") = ""
                        dr("SeatPriceWithNoTax") = 0
                    End If

                    dr("TripType") = Trip
                    Ft.Rows.Add(dr)
                    'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                    If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                        Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LAB:
                        If (ret > 0) Then
                        Else
                            ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                            GoTo LAB
                        End If
                    End If
                Next
                If (Child > 0) Then
                    For i As Integer = 1 To Child
                        Dim fltml As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND MealPrice >0")
                        Dim fltbg As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND BaggagePrice >0")
                        Dim fltse As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND SeatPrice >0")
                        Dim dr As DataRow = Ft.NewRow()
                        dr("PaxID") = Paxdt.Rows((Adult + i) - 1)("PaxID")
                        dr("Flt_HeaderID") = Paxdt.Rows((Adult + i) - 1)("Flt_HeaderID")
                        If (fltml.Length > 0) Then
                            dr("MealCode") = fltml(0)("MealCode")
                            dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                            dr("MealDesc") = fltml(0)("MealDesc")
                            dr("MealCategory") = fltml(0)("MealCategory")
                            dr("MealPriceWithNoTax") = fltml(0)("MealPriceWithNoTax")
                        Else
                            dr("MealCode") = ""
                            dr("MealPrice") = 0
                            dr("MealDesc") = ""
                            dr("MealCategory") = ""
                            dr("MealPriceWithNoTax") = 0
                        End If
                        If (fltbg.Length > 0) Then
                            dr("BaggageCode") = fltbg(0)("BaggageCode")
                            dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
                            dr("BaggageDesc") = fltbg(0)("BaggageDesc")
                            dr("BaggageCategory") = fltbg(0)("BaggageCategory")
                            dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")
                        Else
                            dr("BaggageCode") = ""
                            dr("BaggagePrice") = 0
                            dr("BaggageDesc") = ""
                            dr("BaggageCategory") = ""
                            dr("BaggagePriceWithNoTax") = 0
                        End If

                        If (fltse.Length > 0) Then
                            dr("SeatCode") = fltse(0)("SeatCode")
                            dr("SeatPrice") = Convert.ToDecimal(fltse(0)("SeatPrice"))
                            dr("SeatDesc") = fltse(0)("SeatDesc")
                            dr("SeatCategory") = fltse(0)("SeatCategory")
                            dr("SeatPriceWithNoTax") = fltse(0)("SeatPriceWithNoTax")
                        Else
                            dr("SeatCode") = ""
                            dr("SeatPrice") = 0
                            dr("SeatDesc") = ""
                            dr("SeatCategory") = ""
                            dr("SeatPriceWithNoTax") = 0
                        End If
                        dr("TripType") = Trip
                        Ft.Rows.Add(dr)
                        'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                        If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                            Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LAB2:
                            If (ret > 0) Then
                            Else
                                ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                                GoTo LAB2
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return Ft
    End Function


    Private Function SELL_SSR(ByVal TrackId As String) As String
        Dim SSRPRICE As String = "", SJKAMT As String = "" ', ViaArrv As String = ""
        Dim Signature As String = ""
        Dim Trip As Integer = 1
        Dim Diff As Decimal = 0
        Dim Bag As Boolean = False
        Dim SSRCode As String = ""
        Try

            Dim FltDs As DataSet = objDA.GetFltDtls(TrackId, Session("UID"))
            Dim PaxDs As DataSet = objDA.GetPaxDetails(TrackId)
            Dim VC As String = FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString.Trim()
            Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
            Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
            Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
            If (MBDT.Tables(0).Rows.Count > 0) Then
                For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                    MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
                    BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                Next
            End If

            Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
            Dim dsCrd As DataSet = objSql.GetCredentials(VC, Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "I")
            Dim Org As String = "", Dest As String = ""
            Dim objInputs As New STD.Shared.FlightSearch
            If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
            objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
            objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
            objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
            objInputs.HidTxtAirLine = VC
            If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
            End If
            SSRCode = Convert.ToString(FltDs.Tables(0).Rows(0)("SSRCode"))
            Dim inx As Integer = 0
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                inx = 1
                Trip = 2
            End If

            Dim seginfo As New ArrayList()
            Dim Utlobj As New SpiceIndigoUTL()

            Dim FNO As String = ""
            Dim JSK(inx), FSK(inx) As String 'CC(inx), FNO(inx), DD(inx) 
            Dim ViaArr(inx) As String

            Dim dt = FltDs.Tables(0).DefaultView.ToTable(True, "FlightIdentification") 'Get Distinct FlightID's
            For jj As Integer = 0 To dt.Rows.Count - 1
                Dim dt1 = FltDs.Tables(0).Select("FlightIdentification='" & dt.Rows(jj)("FlightIdentification") & "'", "")
                FNO = dt1(0)("FlightIdentification").Trim()
                Dim Seg As New Dictionary(Of String, String)
                Seg.Add("FNO", FNO)
                Seg.Add("STD", dt1(0)("depdatelcc"))
                Seg.Add("Departure", dt1(0)("DepartureLocation"))
                Seg.Add("Arrival", dt1(dt1.Length - 1)("ArrivalLocation"))
                Seg.Add("VC", VC)
                Seg.Add("Flight", dt1(0)("Flight"))
                seginfo.Add(Seg)
            Next
            For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                If (ii = 0) Then
                    Dim Seg As New Dictionary(Of String, String)
                    Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
                    Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
                    'Changes 8March
                    'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
                    objInputs.HidTxtDepCity = Org
                    objInputs.HidTxtArrCity = Dest
                End If
                If (Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom").ToString()) Then
                    JSK(0) = FltDs.Tables(0).Rows(ii)("SNO")
                    FSK(0) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                    ViaArr(0) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                ElseIf (Org = FltDs.Tables(0).Rows(ii)("OrgDestTo").ToString()) Then
                    JSK(1) = FltDs.Tables(0).Rows(ii)("SNO")
                    FSK(1) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                    ViaArr(1) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                End If
            Next
            objInputs.HidTxtAirLine = VC
            objInputs.Cabin = FltDs.Tables(0).Rows(0)("AdtCabin").ToString().ToUpper()
            ''Devesh add for PROMO CODE
            Dim FareTypeSettingsList As List(Of FareTypeSettings)
            Dim FT As String() = Nothing
            Dim PROMOCODE As String = ""
            Dim AppliedOn As String = ""
            Try
                If [String].IsNullOrEmpty(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString()) = False Then
                    PROMOCODE = Split(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString(), "/")(0)
                    AppliedOn = Split(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString(), "/")(3)
                End If
                If AppliedOn = "BOTH" Then
                    Dim objfltBal As New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                    FareTypeSettingsList = objfltBal.GetFareTypeSettings("", FltDs.Tables(0).Rows(0)("Trip").ToString(), "")
                    FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = VC AndAlso x.Trip = FltDs.Tables(0).Rows(0)("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")).ToUpper().Trim()).ToList()
                    FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                End If
            Catch ex As Exception

            End Try
            ''END Devesh add for PROMO CODE
            Dim Xml As New Dictionary(Of String, String)
            If (objInputs.Infant > 0) Then
                objInputs.Infant = 0 ' Set Infant to 0
            End If
            If (MBPR > 0 Or (VC = "SG" AndAlso objInputs.Cabin = "BUSINESS")) Then
                If (VC = "6E") Then

                    If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                        Dim obj6E As New STD.BAL._6ENAV420._6ENAV(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 420)
                        Signature = obj6E.Spice_Login()
                        SJKAMT = obj6E.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = obj6E.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        obj6E.Spice_Logout(Signature)
                    Else
                        Dim obj6E As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 340)
                        Signature = obj6E.Spice_Login()
                        SJKAMT = obj6E.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = obj6E.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        obj6E.Spice_Logout(Signature)
                    End If
                ElseIf (VC = "SG") Then
                    If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                        Dim objSG As New STD.BAL.SGNAV420.SGNAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0)
                        Signature = objSG.Spice_Login()
                        SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        objSG.Spice_Logout(Signature)
                    Else
                        Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0)
                        Signature = objSG.Spice_Login()
                        SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        objSG.Spice_Logout(Signature)
                    End If
                ElseIf (VC = "G8") Then
                    Dim objSG As New STD.BAL.G8NAV.G8NAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 411) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    Signature = objSG.Spice_Login()
                    SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                    SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr)
                    objSG.Spice_Logout(Signature)
                ElseIf (VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ") Then
                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("AK", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "I")
                    SSRPRICE = objAirAsia.SellSSR(CredencialDs, FltDs.Tables(0), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, MBDT.Tables(0), Xml)
                ElseIf (VC = "G9") Then
                    Dim objAirArabiaia As New GALWS.AirArabia.AirArabiaBooking()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("G9", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "I")
                    SSRPRICE = objAirArabiaia.PricingWithMealBaggege(CredencialDs, FltDs.Tables(0), MBDT.Tables(0), PaxDs.Tables(0), txtGstNo.Text.Trim(), Xml)

                End If
                Try
                    objSql.Insert_SSR_Log(TrackId, Signature, Xml("SSR"), SSRPRICE, Convert.ToDecimal(FltDs.Tables(0).Rows(0)("totFare"))) ' Enter Log
                Catch ex As Exception
                    SSRPRICE = "FAILURE"
                End Try
                If (SSRPRICE <> "FAILURE") Then
                    If (Convert.ToDecimal(SSRPRICE) > (OriginalTF)) Then
                        ' Logic to Update MB table
                        Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
                        objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
                    ElseIf Convert.ToDecimal(SSRPRICE) < (OriginalTF) And VC = "G9" Then
                        Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
                        objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
                    End If
                Else
                    If (Xml("SSR").Contains("The requested class of service is sold out")) Then
                        Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
                    ElseIf (Xml("SSR").Contains("not available on flight")) Then
                        Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                    End If
                End If
                Dim msg = objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString()) 'Update Flt Header and Selected FLight Details_Gal

                If msg <> "1" Then
                    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                End If

            Else
                If VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ" Then
                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("AK", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "I")
                    SSRPRICE = objAirAsia.SellSSR(CredencialDs, FltDs.Tables(0), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, MBDT.Tables(0), Xml)
                    Try
                        objSql.Insert_SSR_Log(TrackId, Signature, Xml("SSR"), SSRPRICE, Convert.ToDecimal(FltDs.Tables(0).Rows(0)("totFare"))) ' Enter Log
                    Catch ex As Exception
                        SSRPRICE = "FAILURE"
                    End Try
                    If (SSRPRICE <> "FAILURE") Then
                        If (Convert.ToDecimal(SSRPRICE) <> (OriginalTF)) Then
                            ' Logic to Update MB table
                            Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
                            'Dim DifPerpax As Decimal = Math.Round(Diff / ((objInputs.Adult + objInputs.Child) * Trip), 0)
                            objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
                        End If
                    Else
                        'Alert(Code')
                        If (Xml("SSR").Contains("The requested class of service is sold out")) Then
                            Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
                        ElseIf (Xml("SSR").Contains("not available on flight")) Then
                            Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                        End If
                    End If
                    objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString())
                ElseIf (VC = "G9") Then
                    Dim objAirArabiaia As New GALWS.AirArabia.AirArabiaBooking()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("G9", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "I")
                    SSRPRICE = objAirArabiaia.PricingWithMealBaggege(CredencialDs, FltDs.Tables(0), MBDT.Tables(0), PaxDs.Tables(0), txtGstNo.Text.Trim(), Xml)

                    objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString())
                End If

            End If
        Catch ex As Exception
            Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
        End Try

        Return SSRPRICE
    End Function
    Private Function showFltDetails(ByVal OBDS As DataSet) As String ', ByVal IBDS As DataSet, ByVal FT As String
        Try

            Dim droneway As DataRow()
            Dim drround As DataRow() = New DataRow(-1) {}

            'If FltHdr.Tables(0).Rows(0)("TripType").ToString().ToUpper() = "O" Then

            'droneway = FltDsGAL.Tables(0).[Select]("flight=1", "counter asc")
            'Else
            droneway = OBDS.Tables(0).[Select]("flight=1", "counter asc")
            drround = OBDS.Tables(0).[Select]("flight=2", "counter asc")
            'End If

            strFlt = ""
            'Dim kk As Integer = VCCount1(droneway)
            Dim Logo As String = ""
            Dim Airline As String = ""
            Dim DepartureTime As String = ""
            Dim ArrivalTime As String = ""
            'If (VCCount(OBDS.Tables(0)) = 0) Then
            If (VCCount1(droneway) = 0) Then
                'Logo = MultiValueFunction(OBDS.Tables(0), "Logo")
                'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                Logo = "../Airlogo/sm" & droneway(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
                'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                Airline = droneway(0)("AirlineName") & "(" & droneway(0)("MarketingCarrier") & "-" & droneway(0)("FlightIdentification") & ")"
            Else
                Logo = "../Airlogo/multiple.png"
                Airline = "Multiple Airline"
            End If

            strFlt = strFlt & "<div class='row'>"
            strFlt = strFlt & "<div class='large-12 medium-12 small-12 bld'>Flight Details</div>"
            strFlt = strFlt & "<div class='large-12 medium-12 small-12'>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='" & Logo & "'/><br />" & Airline & "</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("DepartureLocation") & "(" & droneway(0)("DepartureCityName") & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(droneway.Length - 1)("ArrivalLocation") & "(" & droneway(droneway.Length - 1)("ArrivalCityName") & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("Stops") & "</div>"
            strFlt = strFlt & "</div>"
            strFlt = strFlt & "<div class='large-12 medium-12 small-12'>"
            DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, droneway(0)("DepartureTime"))
            ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, droneway(droneway.Length - 1)("ArrivalTime"))
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("Departure_Date") & " (" & DepartureTime & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(droneway.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</div>"
            If droneway(0)("depdatelcc").ToString.Trim() <> "" Then
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(droneway(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "</div>"
            Else
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'></div>"
            End If
            strFlt = strFlt & "</div>"
            strFlt = strFlt & "</div>"
            strFlt = strFlt & "<div class='clear1'></div>"

            If (drround.Length > 0) Then
                Airline = ""
                Logo = ""
                If (VCCount1(drround) = 0) Then
                    'Logo = MultiValueFunction(OBDS.Tables(0), "Logo")
                    'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                    Logo = "../Airlogo/sm" & drround(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
                    'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                    Airline = drround(0)("AirlineName") & "(" & drround(0)("MarketingCarrier") & "-" & drround(0)("FlightIdentification") & ")"
                Else
                    Logo = "../Airlogo/multiple.png"
                    Airline = "Multiple Airline"
                End If

                strFlt = strFlt & "<div class='row'>"
                strFlt = strFlt & "<div class='large-12 medium-12 small-12'>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='" & Logo & "'/><br />" & Airline & "</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("DepartureLocation") & "(" & drround(0)("DepartureCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(drround.Length - 1)("ArrivalLocation") & "(" & drround(drround.Length - 1)("ArrivalCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("Stops") & "</div>"
                strFlt = strFlt & "</div>"

                strFlt = strFlt & "<div class='large-12 medium-12 small-12'>"
                DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, drround(0)("DepartureTime"))
                ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, drround(drround.Length - 1)("ArrivalTime"))
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("Departure_Date") & " (" & DepartureTime & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(drround.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</div>"
                If drround(0)("depdatelcc").ToString.Trim() <> "" Then
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(drround(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "</div>"
                Else
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'></div>"
                End If
                strFlt = strFlt & "</div>"
                strFlt = strFlt & "</div>"

            End If
            'If FT = "InBound" Then
            '    If (VCCount(IBDS.Tables(0)) = 0) Then
            '        Logo = MultiValueFunction(IBDS.Tables(0), "Logo")
            '        Airline = MultiValueFunction(IBDS.Tables(0), "Airline")
            '    Else
            '        Logo = "../Airlogo/multiple.png"
            '        Airline = "Multiple Airline"
            '    End If
            '    strFlt = strFlt & "<tr>"
            '    strFlt = strFlt & "<td><img alt='' src='" & Logo & "'/><br />" & Airline & "</td>"
            '    strFlt = strFlt & "<td style='font-size:18px;'>" & IBDS.Tables(0).Rows(0)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(0)("DepartureCityName") & ")</td>"
            '    strFlt = strFlt & "<td style='font-size:18px;'>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalCityName") & ")</td>"
            '    strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(0)("Stops") & "</td>"
            '    strFlt = strFlt & "</tr>"
            '    strFlt = strFlt & "<tr>"
            '    strFlt = strFlt & "<td></td>"
            '    DepartureTime = MultiValueFunction(IBDS.Tables(0), "Dep")
            '    ArrivalTime = MultiValueFunction(IBDS.Tables(0), "Arr")
            '    strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(0)("Departure_Date") & " (" & DepartureTime & ")</td>"
            '    strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("Arrival_Date") & " (" & ArrivalTime & ")</td>"
            '    strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "</td>"
            '    strFlt = strFlt & "</tr>"

            'End If




            Dim strPax As String = ""

            strPax = strPax & "<div class='row ' style='padding: 10px 8px 0px 8px;'>"
            strPax = strPax & "<div class='large-12 medium-12 small-12  headbgs'><i class='fa fa-inr' aria-hidden='true'></i> Fare Details</div>"
            strPax = strPax & "<div class='large-12 medium-12 small-12 bgs'>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Adult</div>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Child</div>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Infant</div>"
            strPax = strPax & "</div>"
            strPax = strPax & "<div class='large-12 medium-12 small-12 bgs'>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/adt.png'/>(" & OBDS.Tables(0).Rows(0)("Adult") & ")</div>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>&nbsp; <img alt='' src='../images/chd.png'/>(" & OBDS.Tables(0).Rows(0)("Child") & ")</div>"
            strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>&nbsp; <img alt='' src='../images/inf.png'/>(" & OBDS.Tables(0).Rows(0)("Infant") & ")</div>"
            strPax = strPax & "<div class='clear1'> </div>"
            strPax = strPax & "</div>"
            'strPax = strPax & "<tr id='tr_tottotfare' onmouseover=funcnetfare('block','tr_totnetfare'); onmouseout=funcnetfare('none','tr_totnetfare'); style='cursor:pointer;color: #004b91'>"
            'Dim TotalFare As Double
            'Dim NetFare As Double
            'NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare"))
            ''If FT = "InBound" Then
            ''    TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("TotFare"))
            ''Else
            'TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare"))
            '' End If
            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'><span style='font-size:14px; font-weight:bold; color:#000042; line-height:40px;'>Total Fare : " & TotalFare & "</span><div id='tr_totnetfare' style='display:none;position:absolute;background:#F1F1F1;border: thin solid #D1D1D1;padding:10px; font-weight:bold; font-size:14px; color:#000;'>Net Fare: " & NetFare & "</div></div>"
            strPax = strPax & "<div class='large-12 medium-12 small-12 columns'>"
            ''strPax = strPax & "<div class='large-5 medium-12 small-6 column btn' id='ctl00_ContentPlaceHolder1_divtotFlight' onclick='ddshow(this.id);'>Flight Summary</div><div class='large-5 medium-12 small-6 column btn' id='div_faredd' onclick='ddshow(this.id);'>Fare Summary</div></div>"
            strPax = strPax & "</div>"

            divtotalpax.InnerHtml = strPax
            ' divtotFlightDetails.InnerHtml = CustFltDetails(OBDS, IBDS, FT)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


        Return strFlt
    End Function
    Private Function CustFltDetails(ByVal OBDS As DataSet) As String
        Dim FlightDtlsTotalInfo As String = ""
        'Dim FlightType = ""
        'If FT = "InBound" Then
        '    FlightType = "OutBound"
        'End If
        Dim DepTerminal As String
        Dim ArrTerminal As String
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row'>"
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-6 medium-6 small-6 column bld'>Flight Details</div><div class='large-6 medium-6 small-6 column bld'>" & OBDS.Tables(0).Rows(0)("AdtFareType") & "</div>"
        For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-12 medium-12 small-12'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & OBDS.Tables(0).Rows(i)("AirlineName") & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>(" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Depall", i) & ")</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Arrall", i) & ")</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-12 medium-12 small-12'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>&nbsp;</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>Class(" & OBDS.Tables(0).Rows(i)("RBD") & ") </div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "(" & OBDS.Tables(0).Rows(i)("DepartureCityName") & ")</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & "(" & OBDS.Tables(0).Rows(i)("ArrivalCityName") & ")</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            DepTerminal = ""
            ArrTerminal = ""
            If OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
                DepTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
            End If
            If OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
                ArrTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
            End If

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-12 medium-12 small-12 columns'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>&nbsp;</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='large-3 medium-3 small-3 columns'>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
        Next
        'If FT = "InBound" Then
        '    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td style='padding-top: 20px'> </td></tr>"
        '    ' FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td colspan='4' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;'>Outbound Flight Details<td><tr>"
        '    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;' >InBound Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "</td><tr>"
        '    For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & IBDS.Tables(0).Rows(i)("AirlineName") & "(" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(i)("DepartureCityName") & ")</td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(i)("ArrivalCityName") & ")</td>"
        '        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='color:#004b91; font-size:14px;'>" & IBDS.Tables(0).Rows(i)("AdtFareType") & "</td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='padding-left: 25px'>Class(" & IBDS.Tables(0).Rows(i)("RBD") & ") </td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Depall", i) & ")</td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Arrall", i) & ")</td>"
        '        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(0)("Tot_Dur") & "</td>"
        '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
        '    Next
        'End If
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


        Return FlightDtlsTotalInfo
    End Function
    Private Function VCCount(ByVal dt As DataTable) As Integer
        Dim Count As Integer = (From row In dt.Rows Select row Where row("MarketingCarrier").ToString <> dt.Rows(0)("MarketingCarrier")).Count
        Return Count
    End Function
    Private Function VCCount1(ByVal drr As DataRow()) As Integer

        Dim Count1 As Integer = (From row In drr Select row Where row("MarketingCarrier").ToString <> drr(0)("MarketingCarrier")).Count
        Return Count1
    End Function
    Private Function MultiValueFunction(ByVal dt As DataTable, ByVal Type As String, Optional ByVal Position As Int32 = 0, Optional ByVal dtrow As String = "") As String
        Dim OutputString As String = ""
        If (Type = "Logo") Then
            OutputString = "../Airlogo/sm" & dt.Rows(0)("MarketingCarrier") & ".gif"
        ElseIf Type = "Airline" Then
            OutputString = dt.Rows(0)("AirlineName") & "(" & dt.Rows(0)("MarketingCarrier") & "-" & dt.Rows(0)("FlightIdentification") & ")"
        ElseIf Type = "Dep" Then
            If (dt.Rows(0)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(0)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(0)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(0)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Deprow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If

        ElseIf Type = "Arrrow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If
        ElseIf Type = "Arr" Then
            If (dt.Rows(0)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Depall" Then
            If (dt.Rows(Position)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(Position)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Arrall" Then
            If (dt.Rows(Position)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(Position)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("ArrivalTime").ToString().Substring(2, 2)

            End If
        End If
        Return OutputString
    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub


    Public Function CheckIsPassportMandatory(ByVal OrderID As String) As Integer

        Dim con1 As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim rslt As Integer = 0
        Try
            con1.Open()

            Dim ds1 As New DataSet

            Dim cmd As New SqlClient.SqlCommand()
            Dim da As New SqlClient.SqlDataAdapter(cmd)
            cmd.CommandText = "usp_Is_Passport_Mandatory"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@OrderID", SqlDbType.VarChar).Value = OrderID
            cmd.Connection = con1


            rslt = Convert.ToInt32(cmd.ExecuteScalar())


        Catch ex As Exception

        Finally
            con1.Close()

        End Try

        '' da.Fill(ds1)
        Return rslt
    End Function

    Protected Function UpdateGSTDetail(ByVal OrderId As String, ByVal GSTNO As String, ByVal CompanyName As String, ByVal CompanyAddress As String, ByVal GSTPHoneNO As String, ByVal GST_Email As String, ByVal GSTRemark As String) As DataSet
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim cmd As New SqlCommand()
        Dim Paxds As New DataSet()
        Dim ii As Integer = 0
        Try
            If String.IsNullOrEmpty(GSTNO) Then
                CompanyName = ""
                CompanyAddress = ""
                GSTPHoneNO = ""
                GST_Email = ""
                'Else
            End If
            cmd.CommandText = "SP_UpdateGSTInHeader" 'OrderID varchar(50), @GSTNO varchar(50), @GST_Company_Name varchar(500), @GST_Company_Address varchar(500), @GST_PhoneNo varchar(20), @GST_Email varchar(200), @GSTRemark
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = OrderId
            cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = GSTNO
            cmd.Parameters.Add("@GST_Company_Name", SqlDbType.VarChar).Value = CompanyName.ToUpper()
            cmd.Parameters.Add("@GST_Company_Address", SqlDbType.VarChar).Value = CompanyAddress.ToUpper()
            cmd.Parameters.Add("@GST_PhoneNo", SqlDbType.VarChar).Value = GSTPHoneNO.ToUpper()
            cmd.Parameters.Add("@GST_Email", SqlDbType.VarChar).Value = GST_Email.ToUpper()
            cmd.Parameters.Add("@GSTRemark", SqlDbType.VarChar).Value = GSTRemark.ToUpper()
            cmd.Connection = con1
            con1.Open()
            ii = cmd.ExecuteNonQuery()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        Finally
            con1.Close()
            cmd.Dispose()
        End Try
        Return Paxds
    End Function

    'Protected Sub ddlStateGst_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim code As String = ddlStateGst.SelectedValue
    '        Dim name As String = ddlStateGst.SelectedItem.Text
    '        BindCityGst(code)
    '    Catch ex As Exception
    '        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub

    'Private Sub BindStateGst(countrycode As String)
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '    Try
    '        Dim dt As New DataTable()
    '        Dim da As New SqlDataAdapter((Convert.ToString("select STATEID as Code,STATE as Name from  [dbo].[Tbl_STATE] where COUNTRY='") & countrycode) + "'order by STATE", con)
    '        da.SelectCommand.CommandType = CommandType.Text
    '        da.Fill(dt)
    '        ddlStateGst.DataSource = dt
    '        ddlStateGst.DataValueField = "Code"
    '        ddlStateGst.DataTextField = "Name"
    '        ddlStateGst.DataBind()
    '        ddlStateGst.Items.Insert(0, New ListItem("Select State", "select"))
    '        BindCityGst(ddlStateGst.SelectedValue)

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub
    'Private Sub BindCityGst(stateCode As String)
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '    Try
    '        Dim dt As New DataTable()
    '        Dim da As New SqlDataAdapter((Convert.ToString("select CITY, STATEID from  [dbo].[TBL_CITY]  where STATEID='") & stateCode) + "'order by CITY", con)
    '        da.SelectCommand.CommandType = CommandType.Text
    '        da.Fill(dt)
    '        ddlCityGst.DataSource = dt
    '        ddlCityGst.DataValueField = "CITY"
    '        ddlCityGst.DataTextField = "CITY"
    '        ddlCityGst.DataBind()
    '        ddlCityGst.Items.Insert(0, New ListItem("Select City", "select"))
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub
End Class
