Imports System.Data
Imports System.Data.SqlClient
Imports YatraBilling

Partial Class Reports_Import_UpdateImportPnrIntl
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim pinfo As New pnrinfo()
    Private P As New ProxyClass()
    Private ST As New SqlTransaction
    Private STDom As New SqlTransactionDom
    Private STYTR As New SqlTransactionYatra
    Private CCAP As New clsCalcCommAndPlb
    Private objSql As New SqlTransactionNew
    Dim dt_fltheader As New DataTable()
    Dim dt_FltfareDetail As New DataTable()
    Dim dt_Fltfltdetail As New DataTable()
    Private con As New SqlConnection()
    Private adp As SqlDataAdapter
    Dim IsCorp As Boolean = False
    Private ClsCorp As New ClsCorporate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            IsCorp = ViewState("IsCorp")
            If Not Page.IsPostBack Then
                ViewState("Status") = "NotActive"
                ''  ds = ST.ImportPNRDetailsIntl("Pending", "I", Request("OrderId"))
                '' defaultset()
                ds = ImportPNRDetailsIntl("Pending", "I", Request("OrderId"))
                ViewState("PnrDs") = ds

                Dim strarr As String = ds.Tables(0).Rows(0)("Fltheadr_column").ToString()
                Dim str() As String
                str = strarr.Split("#")

                Dim Air_pnr As String = ""
                Dim vcno As String = ""

                Air_pnr = str(0)
                vcno = str(1)

                txtAirPnr.Text = Air_pnr
                txtVC.Text = vcno

                lblGdsPnr.Text = ds.Tables(0).Rows(0)("PNRNo")
                If ds.Tables.Count > 1 Then
                    GridViewshow.DataSource = ds.Tables(0)
                    GridViewshow.DataBind()

                    paxdetals_Grid.DataSource = ds.Tables(1)
                    paxdetals_Grid.DataBind()

                    paxRPTR.DataSource = ds.Tables(1)
                    paxRPTR.DataBind()
                    If ds.Tables(1).Rows.Count > 0 Then
                        Dim cnt As Integer = 0
                        For Each rowItem As RepeaterItem In paxRPTR.Items
                            DirectCast(rowItem.FindControl("PaxName"), Label).Text = ds.Tables(1).Rows(cnt)("Title") & " " & ds.Tables(1).Rows(cnt)("FName") & " " & ds.Tables(1).Rows(cnt)("LName")
                            DirectCast(rowItem.FindControl("PaxID"), Label).Text = ds.Tables(1).Rows(cnt)("PaxId")

                            If (String.IsNullOrEmpty(ds.Tables(1).Rows(cnt)("PaxType").ToString())) Then
                                DirectCast(rowItem.FindControl("PaxType"), DropDownList).SelectedValue = 0
                            Else
                                DirectCast(rowItem.FindControl("PaxType"), DropDownList).SelectedValue = ds.Tables(1).Rows(cnt)("PaxType")
                            End If

                            cnt = cnt + 1
                        Next
                    Else
                    End If
                Else
                End If
                Try
                    Dim ds1 As New DataSet
                    Dim dt As New DataTable
                    ds1 = ST.PnrImportIntlDetails(Request("OrderId"), "I")
                    dt = ds1.Tables(0)
                    Dim dsAg As New DataSet
                    Dim dtAg As New DataTable
                    dsAg = ST.GetAgencyDetails(dt.Rows(0)("AgentID").ToString)
                    dtAg = dsAg.Tables(0)
                    crdLmt.Text = dtAg.Rows(0)("Crd_Limit").ToString
                    td_AgencyID.InnerText = dtAg.Rows(0)("user_id").ToString
                    td_AgencyType.InnerText = dtAg.Rows(0)("Agent_Type").ToString
                    td_AgencyName.InnerText = dtAg.Rows(0)("Agency_Name").ToString
                    lbl_mn.Text = dtAg.Rows(0)("Mobile").ToString
                    td_AgencyAddress.InnerText = dtAg.Rows(0)("Address").ToString & "," & dtAg.Rows(0)("City").ToString & "," & dtAg.Rows(0)("State").ToString & "," & dtAg.Rows(0)("Country").ToString & "," & dtAg.Rows(0)("Zipcode").ToString
                    'ViewState("IsCorp") = dtAg.Rows(0)("IsCorp").ToString()
                    'IsCorp = Convert.ToBoolean(ViewState("IsCorp").ToString())
                    If (dtAg.Rows(0)("IsCorp").ToString() <> "" AndAlso dtAg.Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                        ViewState("IsCorp") = dtAg.Rows(0)("IsCorp").ToString()
                    Else
                        ViewState("IsCorp") = False
                    End If
                    IsCorp = Convert.ToBoolean(ViewState("IsCorp").ToString())
                    ViewState("ProjectId") = If(IsDBNull(dt.Rows(0)("ProjectID")), Nothing, dt.Rows(0)("ProjectID").ToString())
                    ViewState("BookedBy") = If(IsDBNull(dt.Rows(0)("BookedBy")), Nothing, dt.Rows(0)("BookedBy").ToString())
                    If ViewState("ProjectId") Is Nothing Then
                        tr_corp.Visible = False
                    Else

                        tr_corp.Visible = True
                        td_ProjectId.InnerText = ViewState("ProjectId")
                        td_BookedBy.InnerText = ViewState("BookedBy")
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)

                End Try


                td_Ticket.Visible = True
                divAdult.Style(HtmlTextWriterStyle.Display) = "none"
                divChild.Style(HtmlTextWriterStyle.Display) = "none"
                divInfant.Style(HtmlTextWriterStyle.Display) = "none"
                btnCalc.Style(HtmlTextWriterStyle.Display) = "none"
                btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "none"
                farecalctbl.Style(HtmlTextWriterStyle.Display) = "none"
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)


        End Try
    End Sub
    Protected Sub pnrimporContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnrimporContinue.Click
        Try


            td_Ticket.Visible = False
            Dim paxint As Integer = 0, adt As Integer = 0, chd As Integer = 0, inf As Integer = 0
            For Each rw As RepeaterItem In paxRPTR.Items
                Dim txtTkt As TextBox = DirectCast(rw.FindControl("PaxTktNo"), TextBox)
                Dim dd As String = Request(DirectCast(rw.FindControl("PaxTktNo"), TextBox).UniqueID)
                If Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "ADT" Then
                    adt = adt + 1
                ElseIf Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "CHD" Then
                    chd = chd + 1
                ElseIf Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "INF" Then
                    inf = inf + 1
                End If
                paxint += ST.UpdateTktnoIntl(DirectCast(rw.FindControl("PaxID"), Label).Text, Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID), Request(DirectCast(rw.FindControl("PaxTktNo"), TextBox).UniqueID))
            Next
            ViewState("adt") = adt
            ViewState("chd") = chd
            ViewState("inf") = inf
            defaultset("OFF")
            td_Ticket.Visible = True

            Dim DSS As DataSet
            DSS = ImportPNRDetailsIntl("Pending", "D", Request("OrderId"))
            paxdetals_Grid.DataSource = ""
            paxdetals_Grid.DataSource = DSS.Tables(1)
            paxdetals_Grid.DataBind()


            paxdetals_Grid.Visible = True
            '' GridViewshow.Style(HtmlTextWriterStyle.Display) = "none"
            paxTbl.Style(HtmlTextWriterStyle.Display) = "none"
            If paxint = (adt + chd + inf) Then
                pnrimporContinue.Style(HtmlTextWriterStyle.Display) = "none"
                btnCalc.Style(HtmlTextWriterStyle.Display) = "block"
                divAdult.Style(HtmlTextWriterStyle.Display) = "block"
                If chd > 0 Then divChild.Style(HtmlTextWriterStyle.Display) = "block" Else divChild.Style(HtmlTextWriterStyle.Display) = "none"
                If inf > 0 Then divInfant.Style(HtmlTextWriterStyle.Display) = "block" Else divInfant.Style(HtmlTextWriterStyle.Display) = "none"
            Else

                divAdult.Style(HtmlTextWriterStyle.Display) = "block"
                divChild.Style(HtmlTextWriterStyle.Display) = "block"
                divInfant.Style(HtmlTextWriterStyle.Display) = "block"
                btnCalc.Style(HtmlTextWriterStyle.Display) = "block"
            End If

            btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "block"
            farecalctbl.Style(HtmlTextWriterStyle.Display) = "block"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btnCalc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        Try

            Dim adtBF As Double = 0, adtTax As Double = 0, adtYQ As Double = 0, adtYR As Double = 0, adtWO As Double = 0, adtGST As Double = 0, adtOT As Double = 0, adtTtl As Double = 0
            Dim chdBF As Double = 0, chdTax As Double = 0, chdYQ As Double = 0, chdYR As Double = 0, chdWO As Double = 0, chdGST As Double = 0, chdOT As Double = 0, chdTtl As Double = 0
            Dim infBF As Double = 0, infTax As Double = 0, infYQ As Double = 0, infYR As Double = 0, infWO As Double = 0, infGST As Double = 0, infOT As Double = 0, infTtl As Double = 0
            Dim totFare As Double = 0, srvTax As Double = 0, tf As Double = 0, dis As Double = 0, disAdt As Double = 0, disChd As Double = 0, cb As Double = 0, tfAfterDis As Double = 0, tds As Double = 0
            Dim ADT As Integer = ViewState("adt")
            Dim CHD As Integer = ViewState("chd")
            Dim INF As Integer = ViewState("inf")
            If txt_ABaseFare.Text Is Nothing OrElse txt_ABaseFare.Text = "" Then
                txt_ABaseFare.Text = 0
            End If
            If txt_CBaseFare.Text Is Nothing OrElse txt_CBaseFare.Text = "" Then
                txt_CBaseFare.Text = 0
            End If
            If txt_IBaseFare.Text Is Nothing OrElse txt_IBaseFare.Text = "" Then
                txt_IBaseFare.Text = 0
            End If
            If txt_AYQ.Text Is Nothing OrElse txt_AYQ.Text = "" Then
                txt_AYQ.Text = 0
            End If
            If txt_CYQ.Text Is Nothing OrElse txt_CYQ.Text = "" Then
                txt_CYQ.Text = 0
            End If
            If txt_IYQ.Text Is Nothing OrElse txt_IYQ.Text = "" Then
                txt_IYQ.Text = 0
            End If

            If txt_AYR.Text Is Nothing OrElse txt_AYR.Text = "" Then
                txt_AYR.Text = 0
            End If
            If txt_CYR.Text Is Nothing OrElse txt_CYR.Text = "" Then
                txt_CYR.Text = 0
            End If
            If txt_IYR.Text Is Nothing OrElse txt_IYR.Text = "" Then
                txt_IYR.Text = 0
            End If

            If txt_AWO.Text Is Nothing OrElse txt_AWO.Text = "" Then
                txt_AWO.Text = 0
            End If
            If txt_CWO.Text Is Nothing OrElse txt_CWO.Text = "" Then
                txt_CWO.Text = 0
            End If
            If txt_IWO.Text Is Nothing OrElse txt_IWO.Text = "" Then
                txt_IWO.Text = 0
            End If

            ''GST
            If txt_AGST.Text Is Nothing OrElse txt_AGST.Text = "" Then
                txt_AGST.Text = 0
            End If
            If txt_CGST.Text Is Nothing OrElse txt_CGST.Text = "" Then
                txt_CGST.Text = 0
            End If
            If txt_IGST.Text Is Nothing OrElse txt_IGST.Text = "" Then
                txt_IGST.Text = 0
            End If

            If txt_AOT.Text Is Nothing OrElse txt_AOT.Text = "" Then
                txt_AOT.Text = 0
            End If
            If txt_COT.Text Is Nothing OrElse txt_COT.Text = "" Then
                txt_COT.Text = 0
            End If
            If txt_IOT.Text Is Nothing OrElse txt_IOT.Text = "" Then
                txt_IOT.Text = 0
            End If
            If ADT > 0 Then
                adtBF = Convert.ToDouble(txt_ABaseFare.Text)
                adtYQ = Convert.ToDouble(txt_AYQ.Text)
                adtYR = Convert.ToDouble(txt_AYR.Text)
                adtWO = Convert.ToDouble(txt_AWO.Text)
                adtGST = Convert.ToDouble(txt_AGST.Text)
                adtOT = Convert.ToDouble(txt_AOT.Text)
                adtTtl = adtBF + adtYQ + adtYR + adtWO + adtGST + adtOT
                lblATotal.Text = adtTtl.ToString
            Else
                lblATotal.Text = 0
            End If

            If CHD > 0 Then
                chdBF = Convert.ToDouble(txt_CBaseFare.Text)
                chdYQ = Convert.ToDouble(txt_CYQ.Text)
                chdYR = Convert.ToDouble(txt_CYR.Text)
                chdWO = Convert.ToDouble(txt_CWO.Text)
                chdGST = Convert.ToDouble(txt_CGST.Text)
                chdOT = Convert.ToDouble(txt_COT.Text)
                chdTtl = chdBF + chdYQ + chdYR + chdWO + chdGST + chdOT
                lblCTotal.Text = chdTtl.ToString
            Else
                lblCTotal.Text = 0
            End If
            If INF > 0 Then
                infBF = Convert.ToDouble(txt_IBaseFare.Text)
                infYQ = Convert.ToDouble(txt_IYQ.Text)
                infYR = Convert.ToDouble(txt_IYR.Text)
                infWO = Convert.ToDouble(txt_IWO.Text)
                infGST = Convert.ToDouble(txt_IGST.Text)
                infOT = Convert.ToDouble(txt_IOT.Text)
                infTtl = infBF + infYQ + infYR + infWO + infGST + infOT
                lblITotal.Text = infTtl.ToString
            Else
                lblITotal.Text = 0
            End If
            Dim cls As String = "", Origin As String = "", Dest As String = "", GroupType As String = ""
            Dim ds As New DataSet
            Dim IntAirDt As New DataTable
            Dim dsG As New DataSet
            Dim DtG As New DataTable
            Dim CommADT As Integer = 0, CommCHD As Integer = 0, CommINF As Integer = 0, TotalComm As Integer = 0
            Dim CommADT1 As Double = 0, CommCHD1 As Double = 0, CommINF1 As Double = 0, TotalComm1 As Double = 0 '*****
            Dim tdsper As String
            Dim TdsOn As Integer
            Dim OrderId As String = Request("OrderId")
            Dim VC As String = txtVC.Text.Trim
            Dim ds1 As New DataSet
            Dim dt As New DataTable
            Dim Tax As String
            Dim STax As Double
            Dim TFee As Integer
            ds1 = ST.calcServicecharge(VC, "I")
            dt = ds1.Tables(0)

            Tax = dt.Rows(0)("SrvTax").ToString

            If Tax <> "" AndAlso Tax IsNot Nothing Then
                STax = Convert.ToDouble(Tax)
            Else
                STax = 0
            End If
            TFee = 0
            Dim ADTBaseFare As Double = 0, CHDBaseFare As Double = 0, INFBaseFare As Double = 0, SrviTax As Double = 0
            Dim SrviTax1 As Double = 0 '*****
            If txt_ABaseFare.Text <> "" AndAlso txt_ABaseFare.Text IsNot Nothing Then
                ADTBaseFare = Convert.ToDouble(txt_ABaseFare.Text)
            Else
                ADTBaseFare = 0
            End If

            If txt_CBaseFare.Text <> "" AndAlso txt_CBaseFare.Text IsNot Nothing Then
                CHDBaseFare = Convert.ToDouble(txt_CBaseFare.Text)
            Else
                CHDBaseFare = 0
            End If

            If txt_IBaseFare.Text <> "" AndAlso txt_IBaseFare.Text IsNot Nothing Then
                INFBaseFare = Convert.ToDouble(txt_IBaseFare.Text)
            Else
                INFBaseFare = 0
            End If

            'SrviTax = Math.Round(((((ADTBaseFare * ADT) + (CHDBaseFare * CHD) + (INFBaseFare * INF)) * STax) / 100), 0)
            ' TFee = 0


            ds = ST.PnrImportIntlDetails(Request("OrderId"), "I")
            IntAirDt = ds.Tables(0)
            Dim DepartDate As String = ""
            Dim ReturnDate As String = ""
            If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
                DepartDate = IntAirDt.Rows(0)("departdate").ToString()
                ReturnDate = ""
            Else
                DepartDate = IntAirDt.Rows(0)("departdate").ToString()
                ReturnDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
            End If

            For i As Integer = 0 To IntAirDt.Rows.Count - 1
                cls = cls & IntAirDt.Rows(i)("RDB") & ":"
            Next
            Origin = IntAirDt.Rows(0)("Departure")
            Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")
            dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
            DtG = dsG.Tables(0)
            GroupType = DtG.Rows(0)("agent_type").ToString
            Dim TotalFare As Double = 0
            Dim TotalYQ As Double = 0
            Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0
            Dim STaxPerADT1 As Double = 0, STaxADT1 As Double = 0, STaxPerCHD1 As Double = 0, STaxCHD1 As Double = 0, STaxPerINF1 As Double = 0, STaxINF1 As Double = 0 '*****
            'MARKUP

            Dim Adminmrkadt As Double = 0
            Dim Adminmrkchd As Double = 0
            Dim dtgetmrk As New DataTable
            'END MARKUP
            If (IsCorp = True) Then
                tr_admrkmgt.Visible = True
                Dim dtmgtfeeadt As New DataTable
                Dim dtmgtfeechd As New DataTable
                dtmgtfeeadt = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), "I", Convert.ToDouble(lblATotal.Text)).Tables(0)
                CommADT = Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTFEE").ToString())
                'MARKUP
                dtgetmrk = ClsCorp.GetMarkUp(IntAirDt.Rows(0)("AgentID").ToString(), "SPRING", "I", "AD").Tables(0)
                If (dtgetmrk.Rows.Count > 0) Then
                    Adminmrkadt = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), "I")
                    ViewState("Adminmrkadt") = Adminmrkadt
                End If
                'END MARKUP
                If (CHD > 0) Then
                    dtmgtfeechd = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), "I", Convert.ToDouble(lblCTotal.Text)).Tables(0)
                    CommCHD = Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTFEE").ToString())
                    'MARKUP
                    If (dtgetmrk.Rows.Count > 0) Then
                        Adminmrkchd = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), "I")
                        ViewState("Adminmrkchd") = Adminmrkchd
                    End If
                    'END MARKUP
                End If
                Dim MgtFeeINF As Double = 0
                Dim dtMgtFee As New DataTable
                If (INF > 0) Then
                    dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_IBaseFare.Text), Convert.ToDouble(txt_IYQ.Text), "D", Convert.ToDouble(lblITotal.Text)).Tables(0)
                    MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * INF
                End If
                TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD) + MgtFeeINF
                'End Cal Commission

                '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
                STaxPerADT = Math.Round(((Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
                STaxADT = Math.Round((STaxPerADT), 0) * ADT
                If (CHD > 0) Then
                    STaxPerCHD = Math.Round(((Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
                End If
                If (INF > 0) Then
                    STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxINF = Math.Round((STaxPerINF), 0) * INF
                End If
                SrviTax = STaxADT + STaxCHD + STaxPerINF

                '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
            Else
                tr_admrkmgt.Visible = False
                CommADT = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
                CommADT1 = CommADT '*****
                If (CHD > 0) Then
                    CommCHD = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
                    CommCHD1 = CommCHD '*****
                End If

                TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD)
                TotalComm1 = TotalComm
                'End Cal Commission

                '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
                STaxPerADT = Math.Round(((CommADT * STax) / 100), 0)
                STaxADT = Math.Round((STaxPerADT), 0) * ADT
                STaxADT1 = STaxADT '*****
                STaxPerADT1 = STaxPerADT
                If (CHD > 0) Then
                    STaxPerCHD = Math.Round(((CommCHD * STax) / 100), 0)
                    STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
                    STaxCHD1 = STaxCHD1 '*****
                    STaxPerCHD1 = STaxPerCHD
                End If
                SrviTax = STaxADT + STaxCHD
                SrviTax1 = STaxADT + STaxCHD

                '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
                TotalComm = TotalComm1 - SrviTax1 '*****
                SrviTax = 0 '******
            End If
            'Cal TDS
            Dim TDSPerADT As Double = 0
            Dim TDSADT As Double = 0
            Dim TDSPerCHD As Double = 0
            Dim TDSCHD As Double = 0

            Dim TDSPerADT1 As Double = 0 '*****
            Dim TDSADT1 As Double = 0
            Dim TDSPerCHD1 As Double = 0
            Dim TDSCHD1 As Double = 0
            If (IsCorp = True) Then

                TDSPerADT = 0
                TDSADT = 0
                If (CHD > 0) Then
                    TDSPerCHD = 0 '((Convert.ToDouble(CommCHD) * Convert.ToDouble(tdsper)) / 100)
                    TDSCHD = 0 'Math.Round((TDSPerCHD), 0) * CHD
                End If
                TdsOn = TDSADT + TDSCHD
                'End Cal TDS
                If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
                    lblCTotal.Text = 0
                End If
                If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
                    lblITotal.Text = 0
                End If
                lbl_mgtfee.Text = TotalComm
                lblTtlFare.Text = Adminmrkadt + Adminmrkchd + TotalComm + Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
                lblSrvTax.Text = SrviTax
                lblTF.Text = TFee
                lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn)
                lblTtlDis.Text = 0
                lblTtlCB.Text = 0
                lblTds.Text = TdsOn
                lbl_admrk.Text = (Adminmrkadt + Adminmrkchd)
            Else

                'tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
                'TdsOn = Math.Round(((TotalComm * Convert.ToDouble(tdsper)) / 100), 0)
                tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
                TDSPerADT = ((Convert.ToDouble(CommADT - STaxPerADT1) * Convert.ToDouble(tdsper)) / 100)
                TDSADT = Math.Round((TDSPerADT), 0) * ADT
                TDSPerADT1 = ((Convert.ToDouble(CommADT)) * Convert.ToDouble(tdsper)) / 100
                TDSADT1 = Math.Round((TDSPerADT1), 0) * ADT

                If (CHD > 0) Then
                    TDSPerCHD = ((Convert.ToDouble(CommCHD - STaxPerCHD1) * Convert.ToDouble(tdsper)) / 100)
                    TDSCHD = Math.Round((TDSPerCHD), 0) * CHD
                    TDSPerCHD1 = ((Convert.ToDouble(CommCHD)) * Convert.ToDouble(tdsper)) / 100
                    TDSCHD1 = Math.Round((TDSPerCHD1), 0) * CHD
                End If
                TdsOn = TDSADT + TDSCHD
                'End Cal TDS
                If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
                    lblCTotal.Text = 0
                End If
                If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
                    lblITotal.Text = 0
                End If
                lblTtlFare.Text = Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
                lblSrvTax.Text = SrviTax
                lblTF.Text = TFee
                lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn) - TotalComm
                lblTtlDis.Text = TotalComm
                lblTtlCB.Text = 0
                lblTds.Text = TdsOn
            End If



            btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "block"
            farecalctbl.Style(HtmlTextWriterStyle.Display) = "block"

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btnUpdateImpPnr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateImpPnr.Click
        Dim Status As Boolean = False
        Dim CheckBalStatus As Boolean = False
        Dim CheckBalStatusIMP As Boolean = False
        table()
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()
            Dim cmd As SqlCommand
            cmd = New SqlCommand("SP_CheckBookingByOrderId", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@OrderId", Request("OrderId"))
            cmd.Parameters.AddWithValue("@TableName", "FLTHEADER")
            Status = cmd.ExecuteScalar()
            con.Close()
        Catch ex As Exception

        End Try


        If (ViewState("Status") = "NotActive") AndAlso Status = False Then
            ViewState("Status") = "Active"
            Try
                Dim ADT As Integer = ViewState("adt")
                Dim CHD As Integer = ViewState("chd")
                Dim INF As Integer = ViewState("inf")
                Dim ds As New DataSet
                Dim dt As New DataTable
                ds = ST.PnrImportIntlDetails(Request("OrderId"), "I")
                dt = ds.Tables(0)

                Dim dsAg As New DataSet
                Dim dtAg As New DataTable
                dsAg = ST.GetAgencyDetails(dt.Rows(0)("AgentID").ToString)
                dtAg = dsAg.Tables(0)
                Dim CrdLimit As Double = Convert.ToDouble(dtAg.Rows(0)("Crd_Limit").ToString)

                Dim ImportCharge As Double = 0
                If txt_ExtraCharge.Text <> "" AndAlso txt_ExtraCharge.Text IsNot Nothing Then
                    ImportCharge = Convert.ToDouble(txt_ExtraCharge.Text.Trim())
                End If

                If CrdLimit > Convert.ToDouble(lbltfAftrDis.Text) + Convert.ToDouble(txtpnrImpCharge.Text.Trim) + ImportCharge Then

                    Dim CORPBILLNO As String = Nothing
                    If (IsCorp = True) Then
                        CORPBILLNO = ClsCorp.GenerateBillNoCorp("I").ToString()
                    End If
                    'Insert Import Extra Charge into Ledger
                    If txt_ExtraCharge.Text.Trim <> "0" AndAlso txt_ExtraCharge.Text.Trim <> "" AndAlso txt_ExtraCharge.Text IsNot Nothing Then
                        Dim A_BalPXC As Double
                        A_BalPXC = ST.UpdateCrdLimit(dt.Rows(0)("AgentID"), Convert.ToDouble(txt_ExtraCharge.Text.Trim))
                        STDom.insertLedgerDetails(dt.Rows(0)("AgentID"), dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ExtraCharge.Text.Trim, 0, A_BalPXC, "ExtraChargeImportDom", "Extra Charge with OrderId: " & Request("OrderId") & " and Pnr:" & lblGdsPnr.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                        Dim ds_cur As New DataSet
                        adp = New SqlDataAdapter("UpdateProxyImportCharge", con)
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure
                        adp.SelectCommand.Parameters.AddWithValue("@ID", Request("OrderId"))
                        adp.SelectCommand.Parameters.AddWithValue("@Charge", txtpnrImpCharge.Text.Trim)
                        adp.SelectCommand.Parameters.AddWithValue("@Type", "IMPORTOW")
                        adp.Fill(ds_cur)
                    End If






                    'Insert Import Pnr Charge
                    If txtpnrImpCharge.Text <> "0" Then
                        Dim A_Bal As Double
                        A_Bal = ST.UpdateCrdLimit(dt.Rows(0)("AgentID").ToString, txtpnrImpCharge.Text.Trim)
                        'ST.InsertEsTransCharge(dt.Rows(0)("AgentID").ToString, lblGdsPnr.Text.Trim, A_Bal, txtpnrImpCharge.Text.Trim, "ES Charge with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, dt.Rows(0)("Ag_Name").ToString)
                        'Check for available balance
                        If (A_Bal = 0) Then
                            Dim dtavlIMP As New DataTable()
                            dtavlIMP = STDom.GetAgencyDetails(dt.Rows(0)("AgentID").ToString).Tables(0)
                            Dim CurrAvlBalIMP As Double
                            CurrAvlBalIMP = Convert.ToDouble(dtavlIMP.Rows(0)("Crd_Limit").ToString)
                            If (A_Bal <> CurrAvlBalIMP) Then
                                CheckBalStatusIMP = True
                            End If
                        End If
                        'End Check for available balance
                        If (CheckBalStatusIMP = False) Then
                            STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), "", "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txtpnrImpCharge.Text, 0, A_Bal, "ImoprtChargeIntl", "ES Charge with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        End If

                    End If
                    Dim Aval_Bal As Double
                    'Update Credit Limit
                    Aval_Bal = ST.UpdateCrdLimit(dt.Rows(0)("AgentID").ToString, lbltfAftrDis.Text)

                    'Check for available balance
                    If (Aval_Bal = 0) Then
                        Dim dtavl As New DataTable()
                        dtavl = STDom.GetAgencyDetails(dt.Rows(0)("AgentID").ToString).Tables(0)
                        Dim CurrAvlBal As Double
                        CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
                        If (Aval_Bal <> CurrAvlBal) Then
                            CheckBalStatus = True
                        End If
                    End If
                    'End Check for available balance
                    If (CheckBalStatus = False AndAlso CheckBalStatusIMP = False) Then
                        Dim TotalBookingCost As Double
                        TotalBookingCost = Convert.ToDouble(lblTtlFare.Text) + Convert.ToDouble(lblSrvTax.Text) + Convert.ToDouble(lblTF.Text)
                        'Insert Header Details
                        ' Dim projectId As String = If(IsDBNull(dt.Rows(0)("ProjectID")), Nothing, dt.Rows(0)("ProjectID").ToString().Trim())
                        insertHeaderDetailsPnrImport(Request("OrderId"), dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, "Ticketed", lblGdsPnr.Text.Trim, txtAirPnr.Text.Trim, txtVC.Text.Trim, dt.Rows(0)("TripType").ToString, "I", TotalBookingCost, lbltfAftrDis.Text, "0", ViewState("adt"), ViewState("chd"), ViewState("inf"), dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, "SPRING", Session("UID").ToString(), "CL", ViewState("PnrDs").Tables(1).Rows(0)("Title"), ViewState("PnrDs").Tables(1).Rows(0)("FName"), ViewState("PnrDs").Tables(1).Rows(0)("LName"), txtpnrImpCharge.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        'Insert Transaction Details
                        ST.InsertTransReportPnrImport(dt.Rows(0)("AgentID").ToString, lblGdsPnr.Text.Trim, "Ticketed", Aval_Bal, TotalBookingCost, dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, "Intl Pnr Updated by Import Pnr with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, lbltfAftrDis.Text, dt.Rows(0)("Ag_Name").ToString)

                        'Insert Fare Details
                        If (ADT > 0) Then
                            CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "ADT", Convert.ToDouble(txt_ABaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkadt")), txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AGST.Text.Trim, txt_AOT.Text.Trim, Convert.ToDouble(ViewState("Adminmrkadt")))
                        End If
                        If (CHD > 0) Then
                            CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "CHD", Convert.ToDouble(txt_CBaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkchd")), txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_CGST.Text.Trim, txt_COT.Text.Trim, Convert.ToDouble(ViewState("Adminmrkadt")))
                        End If
                        If (INF > 0) Then
                            CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IGST.Text.Trim, txt_IOT.Text.Trim, 0)
                        End If
                        'Insert Flight Details
                        Dim dsAirNameDepart As New DataSet
                        Dim DtAirNameDepart As New DataTable

                        Dim dsAirNameDest As New DataSet
                        Dim DtAirNameDest As New DataTable

                        Dim dsAirName As New DataSet
                        Dim DtAirName As New DataTable
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Try
                                dsAirNameDepart = ST.GetCityNameByCode(dt.Rows(i)("Departure").ToString)
                                Dim AirlineNameDepart As String = ""
                                Dim AirNameDest As String = ""
                                Dim AirlineName As String = ""
                                DtAirNameDepart = dsAirNameDepart.Tables(0)
                                If DtAirNameDepart.Rows.Count > 0 Then
                                    AirlineNameDepart = DtAirNameDepart.Rows(0)("city").ToString.Trim
                                End If

                                dsAirNameDest = ST.GetCityNameByCode(dt.Rows(i)("Destination").ToString)
                                DtAirNameDest = dsAirNameDest.Tables(0)

                                If DtAirNameDest.Rows.Count > 0 Then
                                    AirNameDest = DtAirNameDest.Rows(0)("city").ToString.Trim
                                End If

                                dsAirName = ST.GetAirlineNameByCode(dt.Rows(i)("Airline").ToString)
                                DtAirName = dsAirName.Tables(0)
                                If DtAirName.Rows.Count > 0 Then
                                    AirlineName = DtAirName.Rows(0)("AL_Name").ToString.Trim
                                End If
                                'DtAirNameDepart = dsAirNameDepart.Tables(0)
                                'dsAirNameDest = ST.GetCityNameByCode(dt.Rows(i)("Destination").ToString)
                                'DtAirNameDest = dsAirNameDest.Tables(0)
                                'dsAirName = ST.GetAirlineNameByCode(dt.Rows(i)("Airline").ToString)
                                'DtAirName = dsAirName.Tables(0)
                                insertFlightDetailsPnrImport(Request("OrderId"), dt.Rows(i)("Departure").ToString.Trim, AirlineNameDepart, dt.Rows(i)("Destination").ToString.Trim, AirNameDest, dt.Rows(i)("DepartDate").ToString.Trim, dt.Rows(i)("DepartTime").ToString.Trim, dt.Rows(i)("ArrivalDate").ToString, dt.Rows(i)("ArrivalTime").ToString.Trim, dt.Rows(i)("Airline").ToString.Trim, AirlineName, dt.Rows(i)("FlightNo").ToString.Trim, "", dt.Rows(i)("AdtFareBasis").ToString, dt.Rows(i)("ChdFareBasis").ToString, dt.Rows(i)("InfFareBasis").ToString, dt.Rows(i)("RDB").ToString.Trim, dt.Rows(i)("RDB").ToString.Trim, dt.Rows(i)("RDB").ToString.Trim, ADT, CHD, INF)
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                            End Try
                        Next


                        ''bulk insert

                        Dim consString As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                        Using con As New SqlConnection(consString)
                            Using cmd As New SqlCommand("BULK_INSERT_FLT")
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Connection = con
                                cmd.Parameters.AddWithValue("@tblHeader", dt_fltheader)
                                cmd.Parameters.AddWithValue("@tblfare", dt_FltfareDetail)
                                cmd.Parameters.AddWithValue("@tblfltdetails", dt_Fltfltdetail)
                                con.Open()
                                cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using


                        ''BULK END

                        'Ledger
                        Dim DebitADT As Double = 0, CreditADT As Double = 0, DebitCHD As Double = 0, CreditCHD As Double = 0, DebitINF As Double = 0, CreditINF As Double = 0
                        Dim DtFltFare As New DataTable
                        DtFltFare = ST.GetFltFareDtl(Request("OrderId")).Tables(0)
                        Dim DtFltHeaderADT As New DataTable
                        DtFltHeaderADT = ST.GetFltHeaderDetail(Request("OrderId")).Tables(0)
                        Dim AvalBalance As Double = Convert.ToDouble(DtFltHeaderADT.Rows(0)("TotalAfterDis")) + Aval_Bal
                        Dim IP As String = Request.UserHostAddress
                        For Each rw In paxRPTR.Items


                            Dim ddltitle As DropDownList = DirectCast(rw.FindControl("PaxType"), DropDownList)
                            Dim txtTkt As TextBox = DirectCast(rw.FindControl("PaxTktNo"), TextBox)

                            If ddltitle.SelectedValue = "ADT" Then
                                DebitADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalAfterDis").ToString())
                                CreditADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalDiscount").ToString())
                                AvalBalance = AvalBalance - DebitADT
                                STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitADT, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                            End If
                            If (CHD > 0) Then
                                If ddltitle.SelectedValue = "CHD" Then
                                    DebitCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
                                    CreditCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
                                    AvalBalance = AvalBalance - DebitCHD
                                    STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitCHD, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)

                                End If
                            End If
                            If (INF > 0) Then
                                If ddltitle.SelectedValue = "INF" Then





                                    If (DtFltFare.Rows(1)("PaxType").ToString() = "INF") Then
                                        DebitINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
                                        CreditINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
                                        AvalBalance = AvalBalance - DebitINF
                                        STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitINF, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                                    Else
                                        DebitINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalAfterDis").ToString())
                                        CreditINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalDiscount").ToString())
                                        AvalBalance = AvalBalance - DebitINF
                                        STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitINF, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)

                                    End If





                                End If
                            End If

                        Next


                        'NAV METHOD  CALL START
                        Try

                            'Dim objNav As New AirService.clsConnection(Request("OrderId"), "0", "0")
                            'objNav.airBookingNav(Request("OrderId"), "", 0)

                        Catch ex As Exception

                        End Try
                        'Nav METHOD END'


                        'Update PnrImportIntl
                        'Update PnrImportDom
                        'Yatra Billing
                        'Online
                        Try
                            'Dim AirObj As New AIR_YATRA
                            'AirObj.ProcessYatra_Air(Request("OrderId"), lblGdsPnr.Text.Trim, "B")
                        Catch ex As Exception

                        End Try
                        'online end
                        'offline
                        'Try
                        ST.UpdateInltPnrImportTicketed(Request("OrderId").ToString, "Ticketed", txtpnrImpCharge.Text)
                        'offline end
                        'yatra billing end
                        Try
                            Dim smsStatus As String = ""
                            Dim smsMsg As String = ""
                            Dim objSMSAPI As New SMSAPI.SMS

                            ''    smsStatus = objSMSAPI.sendSms(Request("OrderId"), lbl_mn.Text, dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, txtVC.Text.Trim, "", dt.Rows(0)("DepartDate").ToString.Trim, lblGdsPnr.Text, smsMsg)
                            ''  objSql.SmsLogDetails(Request("OrderId"), lbl_mn.Text, smsMsg, smsStatus)
                        Catch ex As Exception

                        End Try

                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Pnr Imported Sucessfully');window.location='ProcessImportPnrIntl.aspx';", True)
                    Else
                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to update import.Please try after some time.');", True)
                    End If

                Else
                    'msgboxdiv.InnerHtml = "<script>alert('insufficient Credit Limit');</script>"
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('insufficient Credit Limit');", True)

                End If

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Import already updated');", True)
        End If

    End Sub

    Public Sub CalFareDetails(ByVal Orderid As String, ByVal VC As String, ByVal PaxType As String, ByVal BaseFare As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal GST As Integer, ByVal OT As Integer, ByVal Adminmrk As Double)
        Try


            Dim Tax As String = ""
            Tax = "YQ:" & YQ.ToString() & "#YR:" & YR.ToString() & "#OT:" & OT.ToString() & "#WO:" & WO.ToString() & "#GST:" & GST.ToString() & "#"
            'Calculate Commission
            Dim cls As String = ""
            Dim Origin As String = ""
            Dim Dest As String = ""
            Dim GroupType As String = ""
            Dim ds As New DataSet
            Dim IntAirDt As New DataTable
            Dim dsG As New DataSet
            Dim DtG As New DataTable
            Dim Comm As Integer = 0
            Dim tdsper As Double = 0
            Dim Tds As Integer = 0
            Dim srvtax As Double = 0

            If (IsCorp = True) Then
                '=====================================================CORPORATE==============================================

                ds = ST.PnrImportIntlDetails(Orderid, "I")
                IntAirDt = ds.Tables(0)
                dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
                DtG = dsG.Tables(0)
                GroupType = DtG.Rows(0)("agent_type").ToString
                Dim dtmgtfee As New DataTable
                'If PaxType <> "INF" Then
                dtmgtfee = ClsCorp.GetManagementFeeSrvTax(GroupType, VC, BaseFare, YQ, "I", (BaseFare + YQ + YR + WO + OT + GST)).Tables(0)
                Comm = Convert.ToDouble(dtmgtfee.Rows(0)("MGTFEE").ToString())
                srvtax = Convert.ToDouble(dtmgtfee.Rows(0)("MGTSRVTAX").ToString())
                Tds = 0
                'End If
                ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, Adminmrk, 0, 0, Comm, 0, srvtax, 0, 0, VC, "I")

                '=====================================================END CORPORATE==============================================


            Else
                ds = ST.PnrImportIntlDetails(Orderid, "I")
                IntAirDt = ds.Tables(0)
                For i As Integer = 0 To IntAirDt.Rows.Count - 1
                    cls = cls & IntAirDt.Rows(i)("RDB") & ":"
                Next
                Origin = IntAirDt.Rows(0)("Departure")
                Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")

                dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
                DtG = dsG.Tables(0)
                GroupType = DtG.Rows(0)("agent_type").ToString
                Dim DttDate As String = ""
                Dim RtDate As String = ""

                If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
                    DttDate = IntAirDt.Rows(0)("departdate").ToString()
                    RtDate = ""
                Else
                    DttDate = IntAirDt.Rows(0)("departdate").ToString()
                    RtDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
                End If
                Dim dtsrv As New DataTable, srvtax1 As String, STax As Double = 0, Comm1 As Double = 0
                If PaxType <> "INF" Then
                    Comm = CCAP.calcComm(GroupType, VC, BaseFare, YQ, Origin, Dest, cls, 0, DttDate, RtDate)
                    '*****
                    dtsrv = ST.calcServicecharge(VC, "I").Tables(0)
                    srvtax1 = dtsrv.Rows(0)("SrvTax").ToString
                    If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
                        STax = Math.Round(((Comm * srvtax1) / 100), 0)
                    Else
                        STax = 0
                    End If
                    '*****
                    Comm1 = Comm
                    Comm = Comm - STax
                    'Cal TDS
                    tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
                    Tds = Comm * Convert.ToDouble(tdsper) / 100
                End If




                calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, Tds, VC, "I", Comm1, "")
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Sub calcFare(ByVal tax() As String, ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal cb As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String, ByVal comm1 As Integer, ByVal FareType As String)
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        Dim tax1() As String
        Dim YQ As Integer = 0, YR As Integer = 0, WO As Integer = 0, GST As Integer = 0, OT As Integer = 0, QTax As Integer = 0, totTax As Integer = 0
        Dim totFare As Integer = 0, netFare As Integer = 0, TF As Integer = 0, SrvTax As Double = 0, TF1 As Double = 0, SrvTax1 As Double = 0
        Dim originalSrvTax As Double = 0

        Dim ds As New DataSet
        ds = calcServicecharge(vc, trip)
        If ds.Tables(0).Rows.Count > 0 Then
            TF1 = ds.Tables(0).Rows(0)("TranFee")
        End If

        Try
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    YQ = YQ + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "YR") Then
                    tax1 = tax(i).Split(":")
                    YR = YR + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "WO") Then
                    tax1 = tax(i).Split(":")
                    WO = WO + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "GST") Then
                    tax1 = tax(i).Split(":")
                    GST = GST + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "Q") Then
                    tax1 = tax(i).Split(":")
                    QTax = QTax + Convert.ToInt32(tax1(1))
                Else
                    tax1 = tax(i).Split(":")
                    OT = OT + Convert.ToInt32(tax1(1))
                End If
            Next
            If paxtype <> "INF" Then
                TF = ((basefare + YQ) * TF1) / 100
            End If
            Try
                SrvTax1 = ds.Tables(0).Rows(0)("SrvTax")
                If vc.ToUpper.Trim = "6E" OrElse vc.ToUpper.Trim = "SG" OrElse vc.ToUpper.Trim = "G8" Then
                    ' SrvTax = Math.Round(((((comm - cb) - TF) * SrvTax1) / 100), 0)
                    originalSrvTax = Math.Round(((((comm1 - cb) - TF) * SrvTax1) / 100), 0)
                Else
                    ' SrvTax = Math.Round((((comm - cb) * SrvTax1) / 100), 0)
                    originalSrvTax = Math.Round(((((comm1 - cb) - TF) * SrvTax1) / 100), 0)
                End If
            Catch ex As Exception
            End Try

            SrvTax = 0
            totTax = YQ + YR + WO + OT + GST
            totFare = basefare + totTax + SrvTax + TF + admrk
            netFare = (totFare + tds) - comm
        Catch ex As Exception

        End Try
        Dim dr_FARE As DataRow

        dr_FARE = dt_FltfareDetail.NewRow()

        dr_FARE("OrderId") = trackid
        dr_FARE("BaseFare") = basefare
        dr_FARE("YQ") = YQ
        dr_FARE("YR") = YR
        dr_FARE("K3") = GST
        dr_FARE("WO") = WO
        dr_FARE("OT") = OT
        dr_FARE("Qtax") = QTax
        dr_FARE("TotalTax") = totTax
        dr_FARE("TotalFare") = totFare
        dr_FARE("ServiceTax") = SrvTax
        dr_FARE("TranFee") = TF
        dr_FARE("AdminMrk") = admrk
        dr_FARE("AgentMrk") = agmrk
        dr_FARE("DistrMrk") = dismrk
        dr_FARE("TotalDiscount") = comm
        dr_FARE("PLb") = 0
        dr_FARE("Discount") = comm - cb
        dr_FARE("CashBack") = cb
        dr_FARE("Tds") = tds
        dr_FARE("TdsOn") = comm - cb
        dr_FARE("TotalAfterDis") = netFare
        dr_FARE("PaxType") = paxtype
        dr_FARE("UpdateDate") = DateTime.Now
        dr_FARE("ServiceTax1") = originalSrvTax
        dr_FARE("Discount1") = comm1
        dr_FARE("FareType") = FareType
        dt_FltfareDetail.Rows.Add(dr_FARE)


        'paramHashtable.Clear()
        'paramHashtable.Add("@OrderID", trackid)
        'paramHashtable.Add("@BaseFare", basefare)
        'paramHashtable.Add("@YQ", YQ)
        'paramHashtable.Add("@YR", YR)
        'paramHashtable.Add("@WO", WO)
        'paramHashtable.Add("@OT", OT)
        'paramHashtable.Add("@GST", GST)
        'paramHashtable.Add("@Qtax", QTax)
        'paramHashtable.Add("@TotalTax", totTax)
        'paramHashtable.Add("@TotalFare", totFare)
        'paramHashtable.Add("@ServiceTax", SrvTax)
        'paramHashtable.Add("@TranFee", TF)
        'paramHashtable.Add("@AdminMrk", admrk)
        'paramHashtable.Add("@AgentMrk", agmrk)
        'paramHashtable.Add("@DistrMrk", dismrk)
        'paramHashtable.Add("@TotalDiscount", comm)
        'paramHashtable.Add("@PLb", 0)
        'paramHashtable.Add("@Discount", comm - cb)
        'paramHashtable.Add("@CashBack", cb)
        'paramHashtable.Add("@Tds", tds)
        'paramHashtable.Add("@TdsOn", comm - cb)
        'paramHashtable.Add("@TotalAfterDis", netFare)
        'paramHashtable.Add("@PaxType", paxtype)
        'paramHashtable.Add("@UpdateDate", DateTime.Now)
        'paramHashtable.Add("@ServiceTax1", originalSrvTax)
        'paramHashtable.Add("@Discount1", comm1)
        'paramHashtable.Add("@FareType", FareType)
        'objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails_GST", 1)
    End Sub


    Public Function calcServicecharge(ByVal vc As String, ByVal trip As String) As DataSet
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@vc", vc)
        paramHashtable.Add("@trip", trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ServiceCharge", 3)
    End Function
    Public Function ImportPNRDetailsIntl(ByVal st As String, ByVal Tri As String, Optional ByVal OrderId As String = "", Optional ByVal id As String = "") As DataSet
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@st", st)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@id", id)
        paramHashtable.Add("@Tri", Tri)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ImportPNRDetailIntl_NewON", 3)
    End Function


    Public Sub defaultset(ByVal on_off As String)
        Try

            Dim adtBF As Double = 0, adtTax As Double = 0, adtYQ As Double = 0, adtYR As Double = 0, adtWO As Double = 0, adtGST As Double = 0, adtOT As Double = 0, adtTtl As Double = 0
            Dim chdBF As Double = 0, chdTax As Double = 0, chdYQ As Double = 0, chdYR As Double = 0, chdWO As Double = 0, chdGST As Double = 0, chdOT As Double = 0, chdTtl As Double = 0
            Dim infBF As Double = 0, infTax As Double = 0, infYQ As Double = 0, infYR As Double = 0, infWO As Double = 0, infGST As Double = 0, infOT As Double = 0, infTtl As Double = 0
            Dim totFare As Double = 0, srvTax As Double = 0, tf As Double = 0, dis As Double = 0, disAdt As Double = 0, disChd As Double = 0, cb As Double = 0, tfAfterDis As Double = 0, tds As Double = 0
            Dim ADT As Integer = ViewState("adt")
            Dim CHD As Integer = ViewState("chd")
            Dim INF As Integer = ViewState("inf")

            If (on_off = "OFF") Then
                ADT = ViewState("adt")
                CHD = ViewState("chd")
                INF = ViewState("inf")
            End If
            Dim dss As DataSet = ViewState("PnrDs")

            If dss.Tables(2).Rows.Count > 0 Then
                For i As Integer = 0 To dss.Tables(2).Rows.Count - 1

                    If (dss.Tables(2).Rows(i)("PaxType").ToString().ToUpper() = "ADT") Then
                        txt_ABaseFare.Text = dss.Tables(2).Rows(i)("BaseFare").ToString()
                        txt_AYQ.Text = dss.Tables(2).Rows(i)("YQ").ToString()
                        txt_AYR.Text = dss.Tables(2).Rows(i)("YR").ToString()
                        txt_AWO.Text = dss.Tables(2).Rows(i)("WO").ToString()
                        txt_AGST.Text = dss.Tables(2).Rows(i)("GST").ToString()
                        txt_AOT.Text = dss.Tables(2).Rows(i)("OT").ToString()
                    End If

                    If (dss.Tables(2).Rows(i)("PaxType").ToString().ToUpper() = "CHD") Then
                        txt_CBaseFare.Text = dss.Tables(2).Rows(i)("BaseFare").ToString()
                        txt_CYQ.Text = dss.Tables(2).Rows(i)("YQ").ToString()
                        txt_CYR.Text = dss.Tables(2).Rows(i)("YR").ToString()
                        txt_CWO.Text = dss.Tables(2).Rows(i)("WO").ToString()
                        txt_CGST.Text = dss.Tables(2).Rows(i)("GST").ToString()
                        txt_COT.Text = dss.Tables(2).Rows(i)("OT").ToString()
                    End If

                    If (dss.Tables(2).Rows(i)("PaxType").ToString().ToUpper() = "INF") Then
                        txt_IBaseFare.Text = dss.Tables(2).Rows(i)("BaseFare").ToString()
                        txt_IYQ.Text = dss.Tables(2).Rows(i)("YQ").ToString()
                        txt_IYR.Text = dss.Tables(2).Rows(i)("YR").ToString()
                        txt_IWO.Text = dss.Tables(2).Rows(i)("WO").ToString()
                        txt_IGST.Text = dss.Tables(2).Rows(i)("GST").ToString()
                        txt_IOT.Text = dss.Tables(2).Rows(i)("OT").ToString()
                    End If
                Next
            End If

            If txt_ABaseFare.Text Is Nothing OrElse txt_ABaseFare.Text = "" Then
                txt_ABaseFare.Text = 0
            End If
            If txt_CBaseFare.Text Is Nothing OrElse txt_CBaseFare.Text = "" Then
                txt_CBaseFare.Text = 0
            End If
            If txt_IBaseFare.Text Is Nothing OrElse txt_IBaseFare.Text = "" Then
                txt_IBaseFare.Text = 0
            End If
            If txt_AYQ.Text Is Nothing OrElse txt_AYQ.Text = "" Then
                txt_AYQ.Text = 0
            End If
            If txt_CYQ.Text Is Nothing OrElse txt_CYQ.Text = "" Then
                txt_CYQ.Text = 0
            End If
            If txt_IYQ.Text Is Nothing OrElse txt_IYQ.Text = "" Then
                txt_IYQ.Text = 0
            End If

            If txt_AYR.Text Is Nothing OrElse txt_AYR.Text = "" Then
                txt_AYR.Text = 0
            End If
            If txt_CYR.Text Is Nothing OrElse txt_CYR.Text = "" Then
                txt_CYR.Text = 0
            End If
            If txt_IYR.Text Is Nothing OrElse txt_IYR.Text = "" Then
                txt_IYR.Text = 0
            End If

            If txt_AWO.Text Is Nothing OrElse txt_AWO.Text = "" Then
                txt_AWO.Text = 0
            End If
            If txt_CWO.Text Is Nothing OrElse txt_CWO.Text = "" Then
                txt_CWO.Text = 0
            End If
            If txt_IWO.Text Is Nothing OrElse txt_IWO.Text = "" Then
                txt_IWO.Text = 0
            End If

            ''GST
            If txt_AGST.Text Is Nothing OrElse txt_AGST.Text = "" Then
                txt_AGST.Text = 0
            End If
            If txt_CGST.Text Is Nothing OrElse txt_CGST.Text = "" Then
                txt_CGST.Text = 0
            End If
            If txt_IGST.Text Is Nothing OrElse txt_IGST.Text = "" Then
                txt_IGST.Text = 0
            End If

            If txt_AOT.Text Is Nothing OrElse txt_AOT.Text = "" Then
                txt_AOT.Text = 0
            End If
            If txt_COT.Text Is Nothing OrElse txt_COT.Text = "" Then
                txt_COT.Text = 0
            End If
            If txt_IOT.Text Is Nothing OrElse txt_IOT.Text = "" Then
                txt_IOT.Text = 0
            End If
            If ADT > 0 Then
                adtBF = Convert.ToDouble(txt_ABaseFare.Text)
                adtYQ = Convert.ToDouble(txt_AYQ.Text)
                adtYR = Convert.ToDouble(txt_AYR.Text)
                adtWO = Convert.ToDouble(txt_AWO.Text)
                adtGST = Convert.ToDouble(txt_AGST.Text)
                adtOT = Convert.ToDouble(txt_AOT.Text)
                adtTtl = adtBF + adtYQ + adtYR + adtWO + adtGST + adtOT
                lblATotal.Text = adtTtl.ToString
            Else
                lblATotal.Text = 0
            End If

            If CHD > 0 Then
                chdBF = Convert.ToDouble(txt_CBaseFare.Text)
                chdYQ = Convert.ToDouble(txt_CYQ.Text)
                chdYR = Convert.ToDouble(txt_CYR.Text)
                chdWO = Convert.ToDouble(txt_CWO.Text)
                chdGST = Convert.ToDouble(txt_CGST.Text)
                chdOT = Convert.ToDouble(txt_COT.Text)
                chdTtl = chdBF + chdYQ + chdYR + chdWO + chdGST + chdOT
                lblCTotal.Text = chdTtl.ToString
            Else
                lblCTotal.Text = 0
            End If
            If INF > 0 Then
                infBF = Convert.ToDouble(txt_IBaseFare.Text)
                infYQ = Convert.ToDouble(txt_IYQ.Text)
                infYR = Convert.ToDouble(txt_IYR.Text)
                infWO = Convert.ToDouble(txt_IWO.Text)
                infGST = Convert.ToDouble(txt_IGST.Text)
                infOT = Convert.ToDouble(txt_IOT.Text)
                infTtl = infBF + infYQ + infYR + infWO + infGST + infOT
                lblITotal.Text = infTtl.ToString
            Else
                lblITotal.Text = 0
            End If
            Dim cls As String = "", Origin As String = "", Dest As String = "", GroupType As String = ""
            Dim ds As New DataSet
            Dim IntAirDt As New DataTable
            Dim dsG As New DataSet
            Dim DtG As New DataTable
            Dim CommADT As Integer = 0, CommCHD As Integer = 0, CommINF As Integer = 0, TotalComm As Integer = 0
            Dim CommADT1 As Double = 0, CommCHD1 As Double = 0, CommINF1 As Double = 0, TotalComm1 As Double = 0 '*****
            Dim tdsper As String
            Dim TdsOn As Integer
            Dim OrderId As String = Request("OrderId")
            Dim VC As String = txtVC.Text.Trim
            Dim ds1 As New DataSet
            Dim dt As New DataTable
            Dim Tax As String
            Dim STax As Double
            Dim TFee As Integer
            ds1 = ST.calcServicecharge(VC, "I")
            dt = ds1.Tables(0)

            Tax = dt.Rows(0)("SrvTax").ToString

            If Tax <> "" AndAlso Tax IsNot Nothing Then
                STax = Convert.ToDouble(Tax)
            Else
                STax = 0
            End If
            TFee = 0
            Dim ADTBaseFare As Double = 0, CHDBaseFare As Double = 0, INFBaseFare As Double = 0, SrviTax As Double = 0
            Dim SrviTax1 As Double = 0 '*****
            If txt_ABaseFare.Text <> "" AndAlso txt_ABaseFare.Text IsNot Nothing Then
                ADTBaseFare = Convert.ToDouble(txt_ABaseFare.Text)
            Else
                ADTBaseFare = 0
            End If

            If txt_CBaseFare.Text <> "" AndAlso txt_CBaseFare.Text IsNot Nothing Then
                CHDBaseFare = Convert.ToDouble(txt_CBaseFare.Text)
            Else
                CHDBaseFare = 0
            End If

            If txt_IBaseFare.Text <> "" AndAlso txt_IBaseFare.Text IsNot Nothing Then
                INFBaseFare = Convert.ToDouble(txt_IBaseFare.Text)
            Else
                INFBaseFare = 0
            End If

            'SrviTax = Math.Round(((((ADTBaseFare * ADT) + (CHDBaseFare * CHD) + (INFBaseFare * INF)) * STax) / 100), 0)
            ' TFee = 0


            ds = ST.PnrImportIntlDetails(Request("OrderId"), "I")
            IntAirDt = ds.Tables(0)
            Dim DepartDate As String = ""
            Dim ReturnDate As String = ""
            If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
                DepartDate = IntAirDt.Rows(0)("departdate").ToString()
                ReturnDate = ""
            Else
                DepartDate = IntAirDt.Rows(0)("departdate").ToString()
                ReturnDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
            End If

            For i As Integer = 0 To IntAirDt.Rows.Count - 1
                cls = cls & IntAirDt.Rows(i)("RDB") & ":"
            Next
            Origin = IntAirDt.Rows(0)("Departure")
            Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")
            dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
            DtG = dsG.Tables(0)
            GroupType = DtG.Rows(0)("agent_type").ToString
            Dim TotalFare As Double = 0
            Dim TotalYQ As Double = 0
            Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0
            Dim STaxPerADT1 As Double = 0, STaxADT1 As Double = 0, STaxPerCHD1 As Double = 0, STaxCHD1 As Double = 0, STaxPerINF1 As Double = 0, STaxINF1 As Double = 0 '*****
            'MARKUP

            Dim Adminmrkadt As Double = 0
            Dim Adminmrkchd As Double = 0
            Dim dtgetmrk As New DataTable
            'END MARKUP
            If (IsCorp = True) Then
                tr_admrkmgt.Visible = True
                Dim dtmgtfeeadt As New DataTable
                Dim dtmgtfeechd As New DataTable
                dtmgtfeeadt = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), "I", Convert.ToDouble(lblATotal.Text)).Tables(0)
                CommADT = Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTFEE").ToString())
                'MARKUP
                dtgetmrk = ClsCorp.GetMarkUp(IntAirDt.Rows(0)("AgentID").ToString(), "SPRING", "I", "AD").Tables(0)
                If (dtgetmrk.Rows.Count > 0) Then
                    Adminmrkadt = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), "I")
                    ViewState("Adminmrkadt") = Adminmrkadt
                End If
                'END MARKUP
                If (CHD > 0) Then
                    dtmgtfeechd = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), "I", Convert.ToDouble(lblCTotal.Text)).Tables(0)
                    CommCHD = Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTFEE").ToString())
                    'MARKUP
                    If (dtgetmrk.Rows.Count > 0) Then
                        Adminmrkchd = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), "I")
                        ViewState("Adminmrkchd") = Adminmrkchd
                    End If
                    'END MARKUP
                End If
                Dim MgtFeeINF As Double = 0
                Dim dtMgtFee As New DataTable
                If (INF > 0) Then
                    dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_IBaseFare.Text), Convert.ToDouble(txt_IYQ.Text), "D", Convert.ToDouble(lblITotal.Text)).Tables(0)
                    MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * INF
                End If
                TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD) + MgtFeeINF
                'End Cal Commission

                '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
                STaxPerADT = Math.Round(((Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
                STaxADT = Math.Round((STaxPerADT), 0) * ADT
                If (CHD > 0) Then
                    STaxPerCHD = Math.Round(((Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
                End If
                If (INF > 0) Then
                    STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxINF = Math.Round((STaxPerINF), 0) * INF
                End If
                SrviTax = STaxADT + STaxCHD + STaxPerINF

                '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
            Else
                tr_admrkmgt.Visible = False
                CommADT = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
                CommADT1 = CommADT '*****
                If (CHD > 0) Then
                    CommCHD = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
                    CommCHD1 = CommCHD '*****
                End If

                TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD)
                TotalComm1 = TotalComm
                'End Cal Commission

                '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
                STaxPerADT = Math.Round(((CommADT * STax) / 100), 0)
                STaxADT = Math.Round((STaxPerADT), 0) * ADT
                STaxADT1 = STaxADT '*****
                STaxPerADT1 = STaxPerADT
                If (CHD > 0) Then
                    STaxPerCHD = Math.Round(((CommCHD * STax) / 100), 0)
                    STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
                    STaxCHD1 = STaxCHD1 '*****
                    STaxPerCHD1 = STaxPerCHD
                End If
                SrviTax = STaxADT + STaxCHD
                SrviTax1 = STaxADT + STaxCHD

                '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
                TotalComm = TotalComm1 - SrviTax1 '*****
                SrviTax = 0 '******
            End If
            'Cal TDS
            Dim TDSPerADT As Double = 0
            Dim TDSADT As Double = 0
            Dim TDSPerCHD As Double = 0
            Dim TDSCHD As Double = 0

            Dim TDSPerADT1 As Double = 0 '*****
            Dim TDSADT1 As Double = 0
            Dim TDSPerCHD1 As Double = 0
            Dim TDSCHD1 As Double = 0
            If (IsCorp = True) Then

                TDSPerADT = 0
                TDSADT = 0
                If (CHD > 0) Then
                    TDSPerCHD = 0 '((Convert.ToDouble(CommCHD) * Convert.ToDouble(tdsper)) / 100)
                    TDSCHD = 0 'Math.Round((TDSPerCHD), 0) * CHD
                End If
                TdsOn = TDSADT + TDSCHD
                'End Cal TDS
                If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
                    lblCTotal.Text = 0
                End If
                If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
                    lblITotal.Text = 0
                End If
                lbl_mgtfee.Text = TotalComm
                lblTtlFare.Text = Adminmrkadt + Adminmrkchd + TotalComm + Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
                lblSrvTax.Text = SrviTax
                lblTF.Text = TFee
                lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn)
                lblTtlDis.Text = 0
                lblTtlCB.Text = 0
                lblTds.Text = TdsOn
                lbl_admrk.Text = (Adminmrkadt + Adminmrkchd)
            Else

                'tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
                'TdsOn = Math.Round(((TotalComm * Convert.ToDouble(tdsper)) / 100), 0)
                tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
                TDSPerADT = ((Convert.ToDouble(CommADT - STaxPerADT1) * Convert.ToDouble(tdsper)) / 100)
                TDSADT = Math.Round((TDSPerADT), 0) * ADT
                TDSPerADT1 = ((Convert.ToDouble(CommADT)) * Convert.ToDouble(tdsper)) / 100
                TDSADT1 = Math.Round((TDSPerADT1), 0) * ADT

                If (CHD > 0) Then
                    TDSPerCHD = ((Convert.ToDouble(CommCHD - STaxPerCHD1) * Convert.ToDouble(tdsper)) / 100)
                    TDSCHD = Math.Round((TDSPerCHD), 0) * CHD
                    TDSPerCHD1 = ((Convert.ToDouble(CommCHD)) * Convert.ToDouble(tdsper)) / 100
                    TDSCHD1 = Math.Round((TDSPerCHD1), 0) * CHD
                End If
                TdsOn = TDSADT + TDSCHD
                'End Cal TDS
                If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
                    lblCTotal.Text = 0
                End If
                If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
                    lblITotal.Text = 0
                End If
                lblTtlFare.Text = Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
                lblSrvTax.Text = SrviTax
                lblTF.Text = TFee
                lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn) - TotalComm
                lblTtlDis.Text = TotalComm
                lblTtlCB.Text = 0
                lblTds.Text = TdsOn
            End If



            btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "block"
            farecalctbl.Style(HtmlTextWriterStyle.Display) = "block"

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try



    End Sub
    Public Sub table()
        dt_fltheader.Columns.AddRange(New DataColumn() {New DataColumn("OrderId", GetType(String)), _
                         New DataColumn("sector", GetType(String)), _
                         New DataColumn("Status", GetType(String)), _
                         New DataColumn("GdsPnr", GetType(String)), _
                         New DataColumn("AirlinePnr", GetType(String)), _
                         New DataColumn("VC", GetType(String)), _
                         New DataColumn("TripType", GetType(String)), _
                         New DataColumn("Trip", GetType(String)), _
                         New DataColumn("TotalBookingCost", GetType(Decimal)), _
                         New DataColumn("TotalAfterDis", GetType(Decimal)), _
                         New DataColumn("SFDis", GetType(Decimal)), _
                         New DataColumn("AdditionalMarkup", GetType(Decimal)), _
                         New DataColumn("Adult", GetType(Integer)), _
                         New DataColumn("Child", GetType(Integer)), _
                         New DataColumn("Infant", GetType(Integer)), _
                         New DataColumn("AgentId", GetType(String)), _
                         New DataColumn("AgencyName", GetType(String)), _
                         New DataColumn("DistrId", GetType(String)), _
                         New DataColumn("ExecutiveId", GetType(String)), _
                         New DataColumn("PaymentType", GetType(String)), _
                         New DataColumn("PgTitle", GetType(String)), _
                         New DataColumn("PgFName", GetType(String)), _
                         New DataColumn("PgLName", GetType(String)), _
                         New DataColumn("ImportCharge", GetType(Decimal)), _
                         New DataColumn("ProjectID", GetType(String)), _
                         New DataColumn("BillNoCorp", GetType(String)), _
                         New DataColumn("BookedBy", GetType(String)), _
                         New DataColumn("PgMobile", GetType(String)), _
                         New DataColumn("PgEmail", GetType(String))})




        dt_FltfareDetail.Columns.AddRange(New DataColumn() {New DataColumn("OrderId", GetType(String)), _
                        New DataColumn("BaseFare", GetType(Decimal)), _
                        New DataColumn("YQ", GetType(Decimal)), _
                        New DataColumn("YR", GetType(Decimal)), _
                        New DataColumn("K3", GetType(Decimal)), _
                        New DataColumn("WO", GetType(Decimal)), _
                        New DataColumn("OT", GetType(Decimal)), _
                        New DataColumn("Qtax", GetType(Decimal)), _
                        New DataColumn("TotalTax", GetType(Decimal)), _
                        New DataColumn("TotalFare", GetType(Decimal)), _
                        New DataColumn("ServiceTax", GetType(Decimal)), _
                        New DataColumn("TranFee", GetType(Decimal)), _
                        New DataColumn("AdminMrk", GetType(Decimal)), _
                        New DataColumn("AgentMrk", GetType(Decimal)), _
                        New DataColumn("DistrMrk", GetType(Decimal)), _
                        New DataColumn("TotalDiscount", GetType(Decimal)), _
                        New DataColumn("PLb", GetType(Decimal)), _
                        New DataColumn("Discount", GetType(Decimal)), _
                        New DataColumn("CashBack", GetType(Decimal)), _
                        New DataColumn("Tds", GetType(Decimal)), _
                        New DataColumn("TdsOn", GetType(Decimal)), _
                        New DataColumn("TotalAfterDis", GetType(Decimal)), _
                        New DataColumn("PaxType", GetType(String)), _
                        New DataColumn("UpdateDate", GetType(Date)), _
                        New DataColumn("ServiceTax1", GetType(Decimal)), _
                        New DataColumn("Discount1", GetType(Decimal)), _
                        New DataColumn("FareType", GetType(String))})



        dt_Fltfltdetail.Columns.AddRange(New DataColumn() {New DataColumn("OrderId", GetType(String)), _
                      New DataColumn("DepCityOrAirportCode", GetType(String)), _
                      New DataColumn("DepCityOrAirportName", GetType(String)), _
                      New DataColumn("ArrCityOrAirportCode", GetType(String)), _
                      New DataColumn("ArrCityOrAirportName", GetType(String)), _
                      New DataColumn("DepDate", GetType(String)), _
                      New DataColumn("DepTime", GetType(String)), _
                      New DataColumn("ArrDate", GetType(String)), _
                      New DataColumn("ArrTime", GetType(String)), _
                      New DataColumn("AirlineCode", GetType(String)), _
                      New DataColumn("AirlineName", GetType(String)), _
                      New DataColumn("FltNumber", GetType(String)), _
                      New DataColumn("AirCraft", GetType(String)), _
                      New DataColumn("AdtFareBasis", GetType(String)), _
                      New DataColumn("ChdFareBasis", GetType(String)), _
                      New DataColumn("InfFareBasis", GetType(String)), _
                      New DataColumn("AdtRbd", GetType(String)), _
                      New DataColumn("ChdRbd", GetType(String)), _
                      New DataColumn("InfRbd", GetType(String)), _
                      New DataColumn("AvlSeat", GetType(Integer)), _
                      New DataColumn("UpdateDate", GetType(Date))})

    End Sub
    Public Function insertHeaderDetailsPnrImport(ByVal OrderId As String, ByVal Sector As String, ByVal Status As String, ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal VC As String, ByVal TripType As String, ByVal Trip As String, ByVal TotalBookingCost As Decimal, ByVal TotalAfterDis As Decimal, ByVal AdditionalMarkup As Decimal, ByVal Adult As Integer, ByVal Child As Integer, ByVal Infant As Integer, ByVal AgentId As String, ByVal AgencyName As String, ByVal DistrId As String, ByVal ExecutiveId As String, ByVal PaymentType As String, ByVal PgTitle As String, ByVal PgFName As String, ByVal PgLName As String, ByVal ImportCharge As Decimal, ByVal SFDis As Decimal, ByVal projectId As String, ByVal BookedBy As String, ByVal BillNo As String) As Integer


        Dim dr As DataRow = dt_fltheader.NewRow()
        dr("OrderId") = OrderId
        dr("sector") = Sector
        dr("Status") = Status
        dr("GdsPnr") = GdsPnr
        dr("AirlinePnr") = AirlinePnr
        dr("VC") = VC
        dr("TripType") = TripType
        dr("Trip") = Trip
        dr("SFDis") = SFDis
        dr("TotalBookingCost") = TotalBookingCost
        dr("TotalAfterDis") = TotalAfterDis
        dr("AdditionalMarkup") = AdditionalMarkup
        dr("Adult") = Adult
        dr("Child") = Child
        dr("Infant") = Infant
        dr("AgentId") = AgentId
        dr("AgencyName") = AgencyName
        dr("DistrId") = DistrId
        dr("ExecutiveId") = ExecutiveId
        dr("PaymentType") = PaymentType
        dr("PgTitle") = PgTitle
        dr("PgFName") = PgFName
        dr("PgLName") = PgLName
        dr("ImportCharge") = ImportCharge
        dr("ProjectID") = projectId
        dr("BookedBy") = BookedBy
        dr("BillNoCorp") = BillNo
        dr("PgMobile") = ""
        dr("PgEmail") = ""

        dt_fltheader.Rows.Add(dr)
        'Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        'Dim paramHashtable As New Hashtable
        'paramHashtable.Clear()
        'paramHashtable.Add("@OrderId", OrderId)
        'paramHashtable.Add("@Sector", Sector)
        'paramHashtable.Add("@Status", Status)
        'paramHashtable.Add("@GdsPnr", GdsPnr)
        'paramHashtable.Add("@AirlinePnr", AirlinePnr)
        'paramHashtable.Add("@VC", VC)
        'paramHashtable.Add("@TripType", TripType)
        'paramHashtable.Add("@Trip", Trip)
        'paramHashtable.Add("@TotalBookingCost", TotalBookingCost)
        'paramHashtable.Add("@TotalAfterDis", TotalAfterDis)
        'paramHashtable.Add("@AdditionalMarkup", AdditionalMarkup)
        'paramHashtable.Add("@Adult", Adult)
        'paramHashtable.Add("@Child", Child)
        'paramHashtable.Add("@Infant", Infant)
        'paramHashtable.Add("@AgentId", AgentId)
        'paramHashtable.Add("@AgencyName", AgencyName)
        'paramHashtable.Add("@DistrId", DistrId)
        'paramHashtable.Add("@ExecutiveId", ExecutiveId)
        'paramHashtable.Add("@PaymentType", PaymentType)
        'paramHashtable.Add("@PgTitle", PgTitle)
        'paramHashtable.Add("@PgFName", PgFName)
        'paramHashtable.Add("@PgLName", PgLName)
        'paramHashtable.Add("@ImportCharge", ImportCharge)
        'paramHashtable.Add("@SFDis", SFDis)
        'paramHashtable.Add("@ProjectID", projectId)
        'paramHashtable.Add("@BookedBy", BookedBy)
        'paramHashtable.Add("@BillNo", BillNo)
        'Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPnrImportDetails", 1)
        Return 1
    End Function

    Public Function insertFlightDetailsPnrImport(ByVal OrderID As String, ByVal DCOrAC As String, ByVal DCOrAN As String, ByVal ACOrAC As String, _
                ByVal ACOrAN As String, ByVal DDate As String, ByVal DTime As String, ByVal ADate As String, ByVal ATime As String, ByVal AirCode As String, _
        ByVal AirName As String, ByVal FltNo As String, ByVal AirCraft As String, ByVal AdtFB As String, ByVal ChdFB As String, ByVal InfFB As String, _
       ByVal AdtRbd As String, ByVal ChdRbd As String, ByVal InfRbd As String, ByVal ADV As String, ByVal CHV As String, ByVal INV As String) As Integer


        Dim dr_flt As DataRow

        dr_flt = dt_Fltfltdetail.NewRow()

        dr_flt("OrderId") = OrderID
        dr_flt("DepCityOrAirportCode") = DCOrAC
        dr_flt("DepCityOrAirportName") = DCOrAN
        dr_flt("ArrCityOrAirportCode") = ACOrAC
        dr_flt("ArrCityOrAirportName") = ACOrAN
        dr_flt("DepDate") = DDate
        dr_flt("DepTime") = DTime
        dr_flt("ArrDate") = ADate
        dr_flt("ArrTime") = ATime
        dr_flt("AirlineCode") = AirCode
        dr_flt("AirlineName") = AirName
        dr_flt("FltNumber") = FltNo
        dr_flt("AirCraft") = AirCraft
        dr_flt("AdtFareBasis") = AdtFB
        dr_flt("ChdFareBasis") = ChdFB
        dr_flt("InfFareBasis") = InfFB
        If (ADV > 0) Then
            dr_flt("AdtRbd") = AdtRbd
        Else
            dr_flt("AdtRbd") = ""
        End If

        If (CHV > 0) Then
            dr_flt("ChdRbd") = ChdRbd
        Else
            dr_flt("ChdRbd") = ""
        End If

        If (INV > 0) Then
            dr_flt("InfRbd") = InfRbd

        Else
            dr_flt("InfRbd") = ""
        End If
        dr_flt("AvlSeat") = 0
        dr_flt("UpdateDate") = DateTime.Now


        dt_Fltfltdetail.Rows.Add(dr_flt)

        'paramHashtable.Clear()
        'paramHashtable.Add("@OrderID", OrderID)
        'paramHashtable.Add("@DCOrAC", DCOrAC)
        'paramHashtable.Add("@DCOrAN", DCOrAN)
        'paramHashtable.Add("@ACOrAC", ACOrAC)
        'paramHashtable.Add("@ACOrAN", ACOrAN)
        'paramHashtable.Add("@DDate", DDate)
        'paramHashtable.Add("@DTime", DTime)
        'paramHashtable.Add("@ADate", ADate)
        'paramHashtable.Add("@ATime", ATime)
        'paramHashtable.Add("@AirCode", AirCode)
        'paramHashtable.Add("@AirName", AirName)
        'paramHashtable.Add("@FltNo", FltNo)
        'paramHashtable.Add("@AirCraft", AirCraft)
        'paramHashtable.Add("@AdtFB", AdtFB)
        'paramHashtable.Add("@ChdFB", ChdFB)
        'paramHashtable.Add("@InfFB", InfFB)
        'paramHashtable.Add("@AdtRbd", AdtRbd)
        'paramHashtable.Add("@ChdRbd", ChdRbd)
        'paramHashtable.Add("@InfRbd", InfRbd)
        'paramHashtable.Add("@AvlSeat", 0)
        'paramHashtable.Add("@UpdateDate", DateTime.Now)
        'objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)
        Return 1
    End Function


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    If Session("UID") = "" Or Session("UID") Is Nothing Then
    '        Response.Redirect("~/Login.aspx")
    '    End If
    '    Try
    '        IsCorp = ViewState("IsCorp")
    '        If Not Page.IsPostBack Then
    '            ViewState("Status") = "NotActive"
    '            ds = ST.ImportPNRDetailsIntl("Pending", "I", Request("OrderId"))
    '            ViewState("PnrDs") = ds

    '            lblGdsPnr.Text = ds.Tables(0).Rows(0)("PNRNo")
    '            If ds.Tables.Count > 1 Then
    '                GridViewshow.DataSource = ds.Tables(0)
    '                GridViewshow.DataBind()
    '                paxRPTR.DataSource = ds.Tables(1)
    '                paxRPTR.DataBind()
    '                If ds.Tables(1).Rows.Count > 0 Then
    '                    Dim cnt As Integer = 0
    '                    For Each rowItem As RepeaterItem In paxRPTR.Items
    '                        DirectCast(rowItem.FindControl("PaxName"), Label).Text = ds.Tables(1).Rows(cnt)("Title") & " " & ds.Tables(1).Rows(cnt)("FName") & " " & ds.Tables(1).Rows(cnt)("LName")
    '                        DirectCast(rowItem.FindControl("PaxID"), Label).Text = ds.Tables(1).Rows(cnt)("PaxId")
    '                        cnt = cnt + 1
    '                    Next
    '                Else
    '                End If
    '            Else
    '            End If
    '            Try
    '                Dim ds1 As New DataSet
    '                Dim dt As New DataTable
    '                ds1 = ST.PnrImportIntlDetails(Request("OrderId"), "I")
    '                dt = ds1.Tables(0)
    '                Dim dsAg As New DataSet
    '                Dim dtAg As New DataTable
    '                dsAg = ST.GetAgencyDetails(dt.Rows(0)("AgentID").ToString)
    '                dtAg = dsAg.Tables(0)
    '                crdLmt.Text = dtAg.Rows(0)("Crd_Limit").ToString
    '                td_AgencyID.InnerText = dtAg.Rows(0)("user_id").ToString
    '                td_AgencyType.InnerText = dtAg.Rows(0)("Agent_Type").ToString
    '                td_AgencyName.InnerText = dtAg.Rows(0)("Agency_Name").ToString
    '                lbl_mn.Text = dtAg.Rows(0)("Mobile").ToString
    '                td_AgencyAddress.InnerText = dtAg.Rows(0)("Address").ToString & "," & dtAg.Rows(0)("City").ToString & "," & dtAg.Rows(0)("State").ToString & "," & dtAg.Rows(0)("Country").ToString & "," & dtAg.Rows(0)("Zipcode").ToString
    '                'ViewState("IsCorp") = dtAg.Rows(0)("IsCorp").ToString()
    '                'IsCorp = Convert.ToBoolean(ViewState("IsCorp").ToString())
    '                If (dtAg.Rows(0)("IsCorp").ToString() <> "" AndAlso dtAg.Rows(0)("IsCorp").ToString() IsNot Nothing) Then
    '                    ViewState("IsCorp") = dtAg.Rows(0)("IsCorp").ToString()
    '                Else
    '                    ViewState("IsCorp") = False
    '                End If
    '                IsCorp = Convert.ToBoolean(ViewState("IsCorp").ToString())
    '                ViewState("ProjectId") = If(IsDBNull(dt.Rows(0)("ProjectID")), Nothing, dt.Rows(0)("ProjectID").ToString())
    '                ViewState("BookedBy") = If(IsDBNull(dt.Rows(0)("BookedBy")), Nothing, dt.Rows(0)("BookedBy").ToString())
    '                If ViewState("ProjectId") Is Nothing Then
    '                    tr_corp.Visible = False
    '                Else

    '                    tr_corp.Visible = True
    '                    td_ProjectId.InnerText = ViewState("ProjectId")
    '                    td_BookedBy.InnerText = ViewState("BookedBy")
    '                End If
    '            Catch ex As Exception
    '                clsErrorLog.LogInfo(ex)

    '            End Try


    '            td_Ticket.Visible = True
    '            divAdult.Style(HtmlTextWriterStyle.Display) = "none"
    '            divChild.Style(HtmlTextWriterStyle.Display) = "none"
    '            divInfant.Style(HtmlTextWriterStyle.Display) = "none"
    '            btnCalc.Style(HtmlTextWriterStyle.Display) = "none"
    '            btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "none"
    '            farecalctbl.Style(HtmlTextWriterStyle.Display) = "none"
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)


    '    End Try
    'End Sub

    'Protected Sub pnrimporContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnrimporContinue.Click
    '    Try


    '        td_Ticket.Visible = False
    '        Dim paxint As Integer = 0, adt As Integer = 0, chd As Integer = 0, inf As Integer = 0
    '        For Each rw As RepeaterItem In paxRPTR.Items
    '            Dim txtTkt As TextBox = DirectCast(rw.FindControl("PaxTktNo"), TextBox)
    '            Dim dd As String = Request(DirectCast(rw.FindControl("PaxTktNo"), TextBox).UniqueID)
    '            If Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "ADT" Then
    '                adt = adt + 1
    '            ElseIf Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "CHD" Then
    '                chd = chd + 1
    '            ElseIf Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID) = "INF" Then
    '                inf = inf + 1
    '            End If
    '            paxint += ST.UpdateTktnoIntl(DirectCast(rw.FindControl("PaxID"), Label).Text, Request(DirectCast(rw.FindControl("PaxType"), DropDownList).UniqueID), Request(DirectCast(rw.FindControl("PaxTktNo"), TextBox).UniqueID))
    '        Next
    '        ViewState("adt") = adt
    '        ViewState("chd") = chd
    '        ViewState("inf") = inf
    '        GridViewshow.Style(HtmlTextWriterStyle.Display) = "none"
    '        paxTbl.Style(HtmlTextWriterStyle.Display) = "none"
    '        If paxint = (adt + chd + inf) Then
    '            pnrimporContinue.Style(HtmlTextWriterStyle.Display) = "none"
    '            btnCalc.Style(HtmlTextWriterStyle.Display) = "block"
    '            divAdult.Style(HtmlTextWriterStyle.Display) = "block"
    '            If chd > 0 Then divChild.Style(HtmlTextWriterStyle.Display) = "block" Else divChild.Style(HtmlTextWriterStyle.Display) = "none"
    '            If inf > 0 Then divInfant.Style(HtmlTextWriterStyle.Display) = "block" Else divInfant.Style(HtmlTextWriterStyle.Display) = "none"
    '        Else
    '            divAdult.Style(HtmlTextWriterStyle.Display) = "none"
    '            divChild.Style(HtmlTextWriterStyle.Display) = "none"
    '            divInfant.Style(HtmlTextWriterStyle.Display) = "none"
    '            btnCalc.Style(HtmlTextWriterStyle.Display) = "none"
    '        End If
    '        btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "none"
    '        farecalctbl.Style(HtmlTextWriterStyle.Display) = "none"
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub

    'Protected Sub btnCalc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalc.Click
    '    Try


    '        Dim adtBF As Double = 0, adtTax As Double = 0, adtYQ As Double = 0, adtYR As Double = 0, adtWO As Double = 0, adtOT As Double = 0, adtTtl As Double = 0
    '        Dim chdBF As Double = 0, chdTax As Double = 0, chdYQ As Double = 0, chdYR As Double = 0, chdWO As Double = 0, chdOT As Double = 0, chdTtl As Double = 0
    '        Dim infBF As Double = 0, infTax As Double = 0, infYQ As Double = 0, infYR As Double = 0, infWO As Double = 0, infOT As Double = 0, infTtl As Double = 0
    '        Dim totFare As Double = 0, srvTax As Double = 0, tf As Double = 0, dis As Double = 0, disAdt As Double = 0, disChd As Double = 0, cb As Double = 0, tfAfterDis As Double = 0, tds As Double = 0
    '        Dim ADT As Integer = ViewState("adt")
    '        Dim CHD As Integer = ViewState("chd")
    '        Dim INF As Integer = ViewState("inf")
    '        If txt_ABaseFare.Text Is Nothing OrElse txt_ABaseFare.Text = "" Then
    '            txt_ABaseFare.Text = 0
    '        End If
    '        If txt_CBaseFare.Text Is Nothing OrElse txt_CBaseFare.Text = "" Then
    '            txt_CBaseFare.Text = 0
    '        End If
    '        If txt_IBaseFare.Text Is Nothing OrElse txt_IBaseFare.Text = "" Then
    '            txt_IBaseFare.Text = 0
    '        End If
    '        If txt_AYQ.Text Is Nothing OrElse txt_AYQ.Text = "" Then
    '            txt_AYQ.Text = 0
    '        End If
    '        If txt_CYQ.Text Is Nothing OrElse txt_CYQ.Text = "" Then
    '            txt_CYQ.Text = 0
    '        End If
    '        If txt_IYQ.Text Is Nothing OrElse txt_IYQ.Text = "" Then
    '            txt_IYQ.Text = 0
    '        End If

    '        If txt_AYR.Text Is Nothing OrElse txt_AYR.Text = "" Then
    '            txt_AYR.Text = 0
    '        End If
    '        If txt_CYR.Text Is Nothing OrElse txt_CYR.Text = "" Then
    '            txt_CYR.Text = 0
    '        End If
    '        If txt_IYR.Text Is Nothing OrElse txt_IYR.Text = "" Then
    '            txt_IYR.Text = 0
    '        End If

    '        If txt_AWO.Text Is Nothing OrElse txt_AWO.Text = "" Then
    '            txt_AWO.Text = 0
    '        End If
    '        If txt_CWO.Text Is Nothing OrElse txt_CWO.Text = "" Then
    '            txt_CWO.Text = 0
    '        End If
    '        If txt_IWO.Text Is Nothing OrElse txt_IWO.Text = "" Then
    '            txt_IWO.Text = 0
    '        End If

    '        If txt_AOT.Text Is Nothing OrElse txt_AOT.Text = "" Then
    '            txt_AOT.Text = 0
    '        End If
    '        If txt_COT.Text Is Nothing OrElse txt_COT.Text = "" Then
    '            txt_COT.Text = 0
    '        End If
    '        If txt_IOT.Text Is Nothing OrElse txt_IOT.Text = "" Then
    '            txt_IOT.Text = 0
    '        End If
    '        If ADT > 0 Then
    '            adtBF = Convert.ToDouble(txt_ABaseFare.Text)
    '            adtYQ = Convert.ToDouble(txt_AYQ.Text)
    '            adtYR = Convert.ToDouble(txt_AYR.Text)
    '            adtWO = Convert.ToDouble(txt_AWO.Text)
    '            adtOT = Convert.ToDouble(txt_AOT.Text)
    '            adtTtl = adtBF + adtYQ + adtYR + adtWO + adtOT
    '            lblATotal.Text = adtTtl.ToString
    '        Else
    '            lblATotal.Text = 0
    '        End If

    '        If CHD > 0 Then
    '            chdBF = Convert.ToDouble(txt_CBaseFare.Text)
    '            chdYQ = Convert.ToDouble(txt_CYQ.Text)
    '            chdYR = Convert.ToDouble(txt_CYR.Text)
    '            chdWO = Convert.ToDouble(txt_CWO.Text)
    '            chdOT = Convert.ToDouble(txt_COT.Text)
    '            chdTtl = chdBF + chdYQ + chdYR + chdWO + chdOT
    '            lblCTotal.Text = chdTtl.ToString
    '        Else
    '            lblCTotal.Text = 0
    '        End If
    '        If INF > 0 Then
    '            infBF = Convert.ToDouble(txt_IBaseFare.Text)
    '            infYQ = Convert.ToDouble(txt_IYQ.Text)
    '            infYR = Convert.ToDouble(txt_IYR.Text)
    '            infWO = Convert.ToDouble(txt_IWO.Text)
    '            infOT = Convert.ToDouble(txt_IOT.Text)
    '            infTtl = infBF + infYQ + infYR + infWO + infOT
    '            lblITotal.Text = infTtl.ToString
    '        Else
    '            lblITotal.Text = 0
    '        End If
    '        Dim cls As String = "", Origin As String = "", Dest As String = "", GroupType As String = ""
    '        Dim ds As New DataSet
    '        Dim IntAirDt As New DataTable
    '        Dim dsG As New DataSet
    '        Dim DtG As New DataTable
    '        Dim CommADT As Integer = 0, CommCHD As Integer = 0, CommINF As Integer = 0, TotalComm As Integer = 0
    '        Dim CommADT1 As Double = 0, CommCHD1 As Double = 0, CommINF1 As Double = 0, TotalComm1 As Double = 0 '*****
    '        Dim tdsper As String
    '        Dim TdsOn As Integer
    '        Dim OrderId As String = Request("OrderId")
    '        Dim VC As String = txtVC.Text
    '        Dim ds1 As New DataSet
    '        Dim dt As New DataTable
    '        Dim Tax As String
    '        Dim STax As Double
    '        Dim TFee As Integer
    '        ds1 = ST.calcServicecharge(VC, "I")
    '        dt = ds1.Tables(0)

    '        Tax = dt.Rows(0)("SrvTax").ToString

    '        If Tax <> "" AndAlso Tax IsNot Nothing Then
    '            STax = Convert.ToDouble(Tax)
    '        Else
    '            STax = 0
    '        End If
    '        TFee = 0
    '        Dim ADTBaseFare As Double = 0, CHDBaseFare As Double = 0, INFBaseFare As Double = 0, SrviTax As Double = 0
    '        Dim SrviTax1 As Double = 0 '*****
    '        If txt_ABaseFare.Text <> "" AndAlso txt_ABaseFare.Text IsNot Nothing Then
    '            ADTBaseFare = Convert.ToDouble(txt_ABaseFare.Text)
    '        Else
    '            ADTBaseFare = 0
    '        End If

    '        If txt_CBaseFare.Text <> "" AndAlso txt_CBaseFare.Text IsNot Nothing Then
    '            CHDBaseFare = Convert.ToDouble(txt_CBaseFare.Text)
    '        Else
    '            CHDBaseFare = 0
    '        End If

    '        If txt_IBaseFare.Text <> "" AndAlso txt_IBaseFare.Text IsNot Nothing Then
    '            INFBaseFare = Convert.ToDouble(txt_IBaseFare.Text)
    '        Else
    '            INFBaseFare = 0
    '        End If

    '        'SrviTax = Math.Round(((((ADTBaseFare * ADT) + (CHDBaseFare * CHD) + (INFBaseFare * INF)) * STax) / 100), 0)
    '        ' TFee = 0


    '        ds = ST.PnrImportIntlDetails(Request("OrderId"), "I")
    '        IntAirDt = ds.Tables(0)
    '        Dim DepartDate As String = ""
    '        Dim ReturnDate As String = ""
    '        If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
    '            DepartDate = IntAirDt.Rows(0)("departdate").ToString()
    '            ReturnDate = ""
    '        Else
    '            DepartDate = IntAirDt.Rows(0)("departdate").ToString()
    '            ReturnDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
    '        End If

    '        For i As Integer = 0 To IntAirDt.Rows.Count - 1
    '            cls = cls & IntAirDt.Rows(i)("RDB") & ":"
    '        Next
    '        Origin = IntAirDt.Rows(0)("Departure")
    '        Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")
    '        dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
    '        DtG = dsG.Tables(0)
    '        GroupType = DtG.Rows(0)("agent_type").ToString
    '        Dim TotalFare As Double = 0
    '        Dim TotalYQ As Double = 0
    '        Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0
    '        Dim STaxPerADT1 As Double = 0, STaxADT1 As Double = 0, STaxPerCHD1 As Double = 0, STaxCHD1 As Double = 0, STaxPerINF1 As Double = 0, STaxINF1 As Double = 0 '*****
    '        'MARKUP

    '        Dim Adminmrkadt As Double = 0
    '        Dim Adminmrkchd As Double = 0
    '        Dim dtgetmrk As New DataTable
    '        'END MARKUP
    '        If (IsCorp = True) Then
    '            tr_admrkmgt.Visible = True
    '            Dim dtmgtfeeadt As New DataTable
    '            Dim dtmgtfeechd As New DataTable
    '            dtmgtfeeadt = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), "I", Convert.ToDouble(lblATotal.Text)).Tables(0)
    '            CommADT = Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTFEE").ToString())
    '            'MARKUP
    '            dtgetmrk = ClsCorp.GetMarkUp(IntAirDt.Rows(0)("AgentID").ToString(), "SPRING", "I", "AD").Tables(0)
    '            If (dtgetmrk.Rows.Count > 0) Then
    '                Adminmrkadt = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), "I")
    '                ViewState("Adminmrkadt") = Adminmrkadt
    '            End If
    '            'END MARKUP
    '            If (CHD > 0) Then
    '                dtmgtfeechd = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), "I", Convert.ToDouble(lblCTotal.Text)).Tables(0)
    '                CommCHD = Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTFEE").ToString())
    '                'MARKUP
    '                If (dtgetmrk.Rows.Count > 0) Then
    '                    Adminmrkchd = ClsCorp.CalcMarkup(dtgetmrk, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), "I")
    '                    ViewState("Adminmrkchd") = Adminmrkchd
    '                End If
    '                'END MARKUP
    '            End If
    '            Dim MgtFeeINF As Double = 0
    '            Dim dtMgtFee As New DataTable
    '            If (INF > 0) Then
    '                dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_IBaseFare.Text), Convert.ToDouble(txt_IYQ.Text), "D", Convert.ToDouble(lblITotal.Text)).Tables(0)
    '                MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * INF
    '            End If
    '            TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD) + MgtFeeINF
    '            'End Cal Commission

    '            '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
    '            STaxPerADT = Math.Round(((Convert.ToDouble(dtmgtfeeadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
    '            STaxADT = Math.Round((STaxPerADT), 0) * ADT
    '            If (CHD > 0) Then
    '                STaxPerCHD = Math.Round(((Convert.ToDouble(dtmgtfeechd.Rows(0)("MGTSRVTAX").ToString()))), 0)
    '                STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
    '            End If
    '            If (INF > 0) Then
    '                STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
    '                STaxINF = Math.Round((STaxPerINF), 0) * INF
    '            End If
    '            SrviTax = STaxADT + STaxCHD + STaxPerINF

    '            '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
    '        Else
    '            tr_admrkmgt.Visible = False
    '            CommADT = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_ABaseFare.Text), Convert.ToDouble(txt_AYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
    '            CommADT1 = CommADT '*****
    '            If (CHD > 0) Then
    '                CommCHD = CCAP.calcComm(GroupType, txtVC.Text.Trim, Convert.ToDouble(txt_CBaseFare.Text), Convert.ToDouble(txt_CYQ.Text), Origin, Dest, cls, 0, DepartDate, ReturnDate)
    '                CommCHD1 = CommCHD '*****
    '            End If

    '            TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD)
    '            TotalComm1 = TotalComm
    '            'End Cal Commission

    '            '''''''''''''''''''Calculate ServiceTax'''''''''''''''''''''''''''''''''''''
    '            STaxPerADT = Math.Round(((CommADT * STax) / 100), 0)
    '            STaxADT = Math.Round((STaxPerADT), 0) * ADT
    '            STaxADT1 = STaxADT '*****
    '            STaxPerADT1 = STaxPerADT
    '            If (CHD > 0) Then
    '                STaxPerCHD = Math.Round(((CommCHD * STax) / 100), 0)
    '                STaxCHD = Math.Round((STaxPerCHD), 0) * CHD
    '                STaxCHD1 = STaxCHD1 '*****
    '                STaxPerCHD1 = STaxPerCHD
    '            End If
    '            SrviTax = STaxADT + STaxCHD
    '            SrviTax1 = STaxADT + STaxCHD

    '            '''''''''''''''''''''''''''End Calculation ServiceTax''''''''''''''''''''''''''''
    '            TotalComm = TotalComm1 - SrviTax1 '*****
    '            SrviTax = 0 '******
    '        End If
    '        'Cal TDS
    '        Dim TDSPerADT As Double = 0
    '        Dim TDSADT As Double = 0
    '        Dim TDSPerCHD As Double = 0
    '        Dim TDSCHD As Double = 0

    '        Dim TDSPerADT1 As Double = 0 '*****
    '        Dim TDSADT1 As Double = 0
    '        Dim TDSPerCHD1 As Double = 0
    '        Dim TDSCHD1 As Double = 0
    '        If (IsCorp = True) Then

    '            TDSPerADT = 0
    '            TDSADT = 0
    '            If (CHD > 0) Then
    '                TDSPerCHD = 0 '((Convert.ToDouble(CommCHD) * Convert.ToDouble(tdsper)) / 100)
    '                TDSCHD = 0 'Math.Round((TDSPerCHD), 0) * CHD
    '            End If
    '            TdsOn = TDSADT + TDSCHD
    '            'End Cal TDS
    '            If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
    '                lblCTotal.Text = 0
    '            End If
    '            If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
    '                lblITotal.Text = 0
    '            End If
    '            lbl_mgtfee.Text = TotalComm
    '            lblTtlFare.Text = Adminmrkadt + Adminmrkchd + TotalComm + Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
    '            lblSrvTax.Text = SrviTax
    '            lblTF.Text = TFee
    '            lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn)
    '            lblTtlDis.Text = 0
    '            lblTtlCB.Text = 0
    '            lblTds.Text = TdsOn
    '            lbl_admrk.Text = (Adminmrkadt + Adminmrkchd)
    '        Else

    '            'tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
    '            'TdsOn = Math.Round(((TotalComm * Convert.ToDouble(tdsper)) / 100), 0)
    '            tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
    '            TDSPerADT = ((Convert.ToDouble(CommADT - STaxPerADT1) * Convert.ToDouble(tdsper)) / 100)
    '            TDSADT = Math.Round((TDSPerADT), 0) * ADT
    '            TDSPerADT1 = ((Convert.ToDouble(CommADT)) * Convert.ToDouble(tdsper)) / 100
    '            TDSADT1 = Math.Round((TDSPerADT1), 0) * ADT

    '            If (CHD > 0) Then
    '                TDSPerCHD = ((Convert.ToDouble(CommCHD - STaxPerCHD1) * Convert.ToDouble(tdsper)) / 100)
    '                TDSCHD = Math.Round((TDSPerCHD), 0) * CHD
    '                TDSPerCHD1 = ((Convert.ToDouble(CommCHD)) * Convert.ToDouble(tdsper)) / 100
    '                TDSCHD1 = Math.Round((TDSPerCHD1), 0) * CHD
    '            End If
    '            TdsOn = TDSADT + TDSCHD
    '            'End Cal TDS
    '            If lblCTotal.Text Is Nothing OrElse lblCTotal.Text = "" Then
    '                lblCTotal.Text = 0
    '            End If
    '            If lblITotal.Text Is Nothing OrElse lblITotal.Text = "" Then
    '                lblITotal.Text = 0
    '            End If
    '            lblTtlFare.Text = Convert.ToDouble(lblATotal.Text) * ADT + Convert.ToDouble(lblCTotal.Text) * CHD + Convert.ToDouble(lblITotal.Text) * INF
    '            lblSrvTax.Text = SrviTax
    '            lblTF.Text = TFee
    '            lbltfAftrDis.Text = (Convert.ToDouble(lblTtlFare.Text) + SrviTax + TFee + TdsOn) - TotalComm
    '            lblTtlDis.Text = TotalComm
    '            lblTtlCB.Text = 0
    '            lblTds.Text = TdsOn
    '        End If



    '        btnUpdateImpPnr.Style(HtmlTextWriterStyle.Display) = "block"
    '        farecalctbl.Style(HtmlTextWriterStyle.Display) = "block"

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub

    'Protected Sub btnUpdateImpPnr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateImpPnr.Click
    '    Dim Status As Boolean = False
    '    Dim CheckBalStatus As Boolean = False
    '    Dim CheckBalStatusIMP As Boolean = False
    '    Try
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    '        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    '        con.Open()
    '        Dim cmd As SqlCommand
    '        cmd = New SqlCommand("SP_CheckBookingByOrderId", con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("@OrderId", Request("OrderId"))
    '        cmd.Parameters.AddWithValue("@TableName", "FLTHEADER")
    '        Status = cmd.ExecuteScalar()
    '        con.Close()
    '    Catch ex As Exception

    '    End Try


    '    If (ViewState("Status") = "NotActive") AndAlso Status = False Then
    '        ViewState("Status") = "Active"
    '        Try
    '            Dim ADT As Integer = ViewState("adt")
    '            Dim CHD As Integer = ViewState("chd")
    '            Dim INF As Integer = ViewState("inf")
    '            Dim ds As New DataSet
    '            Dim dt As New DataTable
    '            ds = ST.PnrImportIntlDetails(Request("OrderId"), "I")
    '            dt = ds.Tables(0)

    '            Dim dsAg As New DataSet
    '            Dim dtAg As New DataTable
    '            dsAg = ST.GetAgencyDetails(dt.Rows(0)("AgentID").ToString)
    '            dtAg = dsAg.Tables(0)
    '            Dim CrdLimit As Double = Convert.ToDouble(dtAg.Rows(0)("Crd_Limit").ToString)

    '            Dim ImportCharge As Double = 0
    '            If txt_ExtraCharge.Text <> "" AndAlso txt_ExtraCharge.Text IsNot Nothing Then
    '                ImportCharge = Convert.ToDouble(txt_ExtraCharge.Text.Trim())
    '            End If

    '            If CrdLimit > Convert.ToDouble(lbltfAftrDis.Text) + Convert.ToDouble(txtpnrImpCharge.Text.Trim) + ImportCharge Then

    '                Dim CORPBILLNO As String = Nothing
    '                If (IsCorp = True) Then
    '                    CORPBILLNO = ClsCorp.GenerateBillNoCorp("I").ToString()
    '                End If
    '                'Insert Import Extra Charge into Ledger
    '                If txt_ExtraCharge.Text.Trim <> "0" AndAlso txt_ExtraCharge.Text.Trim <> "" AndAlso txt_ExtraCharge.Text IsNot Nothing Then
    '                    Dim A_BalPXC As Double
    '                    A_BalPXC = ST.UpdateCrdLimit(dt.Rows(0)("AgentID"), Convert.ToDouble(txt_ExtraCharge.Text.Trim))
    '                    STDom.insertLedgerDetails(dt.Rows(0)("AgentID"), dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ExtraCharge.Text.Trim, 0, A_BalPXC, "ExtraChargeImportDom", "Extra Charge with OrderId: " & Request("OrderId") & " and Pnr:" & lblGdsPnr.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
    '                    If con.State = ConnectionState.Open Then
    '                        con.Close()
    '                    End If
    '                    con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    '                    Dim ds_cur As New DataSet
    '                    adp = New SqlDataAdapter("UpdateProxyImportCharge", con)
    '                    adp.SelectCommand.CommandType = CommandType.StoredProcedure
    '                    adp.SelectCommand.Parameters.AddWithValue("@ID", Request("OrderId"))
    '                    adp.SelectCommand.Parameters.AddWithValue("@Charge", txtpnrImpCharge.Text.Trim)
    '                    adp.SelectCommand.Parameters.AddWithValue("@Type", "IMPORTOW")
    '                    adp.Fill(ds_cur)
    '                End If






    '                'Insert Import Pnr Charge
    '                If txtpnrImpCharge.Text <> "0" Then
    '                    Dim A_Bal As Double
    '                    A_Bal = ST.UpdateCrdLimit(dt.Rows(0)("AgentID").ToString, txtpnrImpCharge.Text.Trim)
    '                    'ST.InsertEsTransCharge(dt.Rows(0)("AgentID").ToString, lblGdsPnr.Text.Trim, A_Bal, txtpnrImpCharge.Text.Trim, "ES Charge with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, dt.Rows(0)("Ag_Name").ToString)
    '                    'Check for available balance
    '                    If (A_Bal = 0) Then
    '                        Dim dtavlIMP As New DataTable()
    '                        dtavlIMP = STDom.GetAgencyDetails(dt.Rows(0)("AgentID").ToString).Tables(0)
    '                        Dim CurrAvlBalIMP As Double
    '                        CurrAvlBalIMP = Convert.ToDouble(dtavlIMP.Rows(0)("Crd_Limit").ToString)
    '                        If (A_Bal <> CurrAvlBalIMP) Then
    '                            CheckBalStatusIMP = True
    '                        End If
    '                    End If
    '                    'End Check for available balance
    '                    If (CheckBalStatusIMP = False) Then
    '                        STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), "", "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txtpnrImpCharge.Text, 0, A_Bal, "ImoprtChargeIntl", "ES Charge with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
    '                    End If

    '                End If
    '                Dim Aval_Bal As Double
    '                'Update Credit Limit
    '                Aval_Bal = ST.UpdateCrdLimit(dt.Rows(0)("AgentID").ToString, lbltfAftrDis.Text)

    '                'Check for available balance
    '                If (Aval_Bal = 0) Then
    '                    Dim dtavl As New DataTable()
    '                    dtavl = STDom.GetAgencyDetails(dt.Rows(0)("AgentID").ToString).Tables(0)
    '                    Dim CurrAvlBal As Double
    '                    CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
    '                    If (Aval_Bal <> CurrAvlBal) Then
    '                        CheckBalStatus = True
    '                    End If
    '                End If
    '                'End Check for available balance
    '                If (CheckBalStatus = False AndAlso CheckBalStatusIMP = False) Then
    '                    Dim TotalBookingCost As Double
    '                    TotalBookingCost = Convert.ToDouble(lblTtlFare.Text) + Convert.ToDouble(lblSrvTax.Text) + Convert.ToDouble(lblTF.Text)
    '                    'Insert Header Details
    '                    ' Dim projectId As String = If(IsDBNull(dt.Rows(0)("ProjectID")), Nothing, dt.Rows(0)("ProjectID").ToString().Trim())
    '                    ST.insertHeaderDetailsPnrImport(Request("OrderId"), dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, "Ticketed", lblGdsPnr.Text.Trim, txtAirPnr.Text.Trim, txtVC.Text.Trim, dt.Rows(0)("TripType").ToString, "I", TotalBookingCost, lbltfAftrDis.Text, "0", ViewState("adt"), ViewState("chd"), ViewState("inf"), dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, "SPRING", Session("UID").ToString(), "CL", ViewState("PnrDs").Tables(1).Rows(0)("Title"), ViewState("PnrDs").Tables(1).Rows(0)("FName"), ViewState("PnrDs").Tables(1).Rows(0)("LName"), txtpnrImpCharge.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
    '                    'Insert Transaction Details
    '                    ST.InsertTransReportPnrImport(dt.Rows(0)("AgentID").ToString, lblGdsPnr.Text.Trim, "Ticketed", Aval_Bal, TotalBookingCost, dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, "Intl Pnr Updated by Import Pnr with OrderId: " & Request("OrderId") & "Pnr:" & lblGdsPnr.Text, lbltfAftrDis.Text, dt.Rows(0)("Ag_Name").ToString)

    '                    'Insert Fare Details
    '                    If (ADT > 0) Then
    '                        CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "ADT", Convert.ToDouble(txt_ABaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkadt")), txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AOT.Text.Trim, Convert.ToDouble(ViewState("Adminmrkadt")))
    '                    End If
    '                    If (CHD > 0) Then
    '                        CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "CHD", Convert.ToDouble(txt_CBaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkchd")), txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_COT.Text.Trim, Convert.ToDouble(ViewState("Adminmrkadt")))
    '                    End If
    '                    If (INF > 0) Then
    '                        CalFareDetails(Request("OrderId"), txtVC.Text.Trim, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IOT.Text.Trim, 0)
    '                    End If
    '                    'Insert Flight Details
    '                    Dim dsAirNameDepart As New DataSet
    '                    Dim DtAirNameDepart As New DataTable

    '                    Dim dsAirNameDest As New DataSet
    '                    Dim DtAirNameDest As New DataTable

    '                    Dim dsAirName As New DataSet
    '                    Dim DtAirName As New DataTable
    '                    For i As Integer = 0 To dt.Rows.Count - 1
    '                        Try
    '                            dsAirNameDepart = ST.GetCityNameByCode(dt.Rows(i)("Departure").ToString)
    '                            Dim AirlineNameDepart As String = ""
    '                            Dim AirNameDest As String = ""
    '                            Dim AirlineName As String = ""
    '                            DtAirNameDepart = dsAirNameDepart.Tables(0)
    '                            If DtAirNameDepart.Rows.Count > 0 Then
    '                                AirlineNameDepart = DtAirNameDepart.Rows(0)("city").ToString.Trim
    '                            End If

    '                            dsAirNameDest = ST.GetCityNameByCode(dt.Rows(i)("Destination").ToString)
    '                            DtAirNameDest = dsAirNameDest.Tables(0)

    '                            If DtAirNameDest.Rows.Count > 0 Then
    '                                AirNameDest = DtAirNameDest.Rows(0)("city").ToString.Trim
    '                            End If

    '                            dsAirName = ST.GetAirlineNameByCode(dt.Rows(i)("Airline").ToString)
    '                            DtAirName = dsAirName.Tables(0)
    '                            If DtAirName.Rows.Count > 0 Then
    '                                AirlineName = DtAirName.Rows(0)("AL_Name").ToString.Trim
    '                            End If
    '                            'DtAirNameDepart = dsAirNameDepart.Tables(0)
    '                            'dsAirNameDest = ST.GetCityNameByCode(dt.Rows(i)("Destination").ToString)
    '                            'DtAirNameDest = dsAirNameDest.Tables(0)
    '                            'dsAirName = ST.GetAirlineNameByCode(dt.Rows(i)("Airline").ToString)
    '                            'DtAirName = dsAirName.Tables(0)
    '                            ST.insertFlightDetailsPnrImport(Request("OrderId"), dt.Rows(i)("Departure").ToString.Trim, AirlineNameDepart, dt.Rows(i)("Destination").ToString.Trim, AirNameDest, dt.Rows(i)("DepartDate").ToString.Trim, dt.Rows(i)("DepartTime").ToString.Trim, dt.Rows(i)("ArrivalDate").ToString, dt.Rows(i)("ArrivalTime").ToString.Trim, dt.Rows(i)("Airline").ToString.Trim, AirlineName, dt.Rows(i)("FlightNo").ToString.Trim, "", "", "", "", "", "", "")
    '                        Catch ex As Exception
    '                            clsErrorLog.LogInfo(ex)
    '                        End Try
    '                    Next
    '                    'Ledger
    '                    Dim DebitADT As Double = 0, CreditADT As Double = 0, DebitCHD As Double = 0, CreditCHD As Double = 0, DebitINF As Double = 0, CreditINF As Double = 0
    '                    Dim DtFltFare As New DataTable
    '                    DtFltFare = ST.GetFltFareDtl(Request("OrderId")).Tables(0)
    '                    Dim DtFltHeaderADT As New DataTable
    '                    DtFltHeaderADT = ST.GetFltHeaderDetail(Request("OrderId")).Tables(0)
    '                    Dim AvalBalance As Double = Convert.ToDouble(DtFltHeaderADT.Rows(0)("TotalAfterDis")) + Aval_Bal
    '                    Dim IP As String = Request.UserHostAddress
    '                    For Each rw In paxRPTR.Items


    '                        Dim ddltitle As DropDownList = DirectCast(rw.FindControl("PaxType"), DropDownList)
    '                        Dim txtTkt As TextBox = DirectCast(rw.FindControl("PaxTktNo"), TextBox)

    '                        If ddltitle.SelectedValue = "ADT" Then
    '                            DebitADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalAfterDis").ToString())
    '                            CreditADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalDiscount").ToString())
    '                            AvalBalance = AvalBalance - DebitADT
    '                            STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitADT, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
    '                        End If
    '                        If (CHD > 0) Then
    '                            If ddltitle.SelectedValue = "CHD" Then
    '                                DebitCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
    '                                CreditCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
    '                                AvalBalance = AvalBalance - DebitCHD
    '                                STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitCHD, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)

    '                            End If
    '                        End If
    '                        If (INF > 0) Then
    '                            If ddltitle.SelectedValue = "INF" Then





    '                                If (DtFltFare.Rows(1)("PaxType").ToString() = "INF") Then
    '                                    DebitINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
    '                                    CreditINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
    '                                    AvalBalance = AvalBalance - DebitINF
    '                                    STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitINF, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
    '                                Else
    '                                    DebitINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalAfterDis").ToString())
    '                                    CreditINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalDiscount").ToString())
    '                                    AvalBalance = AvalBalance - DebitINF
    '                                    STDom.insertLedgerDetails(dt.Rows(0)("AgentID").ToString, dt.Rows(0)("Ag_Name").ToString, Request("OrderId"), lblGdsPnr.Text.Trim, txtTkt.Text, txtVC.Text.Trim, "", "", Session("UID").ToString, IP, DebitINF, 0, AvalBalance, "ImoprtIntl", "Created By PNR Import Intl", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)

    '                                End If





    '                            End If
    '                        End If

    '                    Next


    '                    'NAV METHOD  CALL START
    '                    Try

    '                        'Dim objNav As New AirService.clsConnection(Request("OrderId"), "0", "0")
    '                        'objNav.airBookingNav(Request("OrderId"), "", 0)

    '                    Catch ex As Exception

    '                    End Try
    '                    'Nav METHOD END'


    '                    'Update PnrImportIntl
    '                    'Update PnrImportDom
    '                    'Yatra Billing
    '                    'Online
    '                    Try
    '                        'Dim AirObj As New AIR_YATRA
    '                        'AirObj.ProcessYatra_Air(Request("OrderId"), lblGdsPnr.Text.Trim, "B")
    '                    Catch ex As Exception

    '                    End Try
    '                    'online end
    '                    'offline
    '                    'Try
    '                    ST.UpdateInltPnrImportTicketed(Request("OrderId").ToString, "Ticketed", txtpnrImpCharge.Text)
    '                    'offline end
    '                    'yatra billing end
    '                    Try
    '                        Dim smsStatus As String = ""
    '                        Dim smsMsg As String = ""
    '                        Dim objSMSAPI As New SMSAPI.SMS

    '                        smsStatus = objSMSAPI.sendSms(Request("OrderId"), lbl_mn.Text, dt.Rows(0)("Departure").ToString & ":" & dt.Rows(dt.Rows.Count - 1)("Destination").ToString, txtVC.Text.Trim, "", dt.Rows(0)("DepartDate").ToString.Trim, lblGdsPnr.Text, smsMsg)
    '                        objSql.SmsLogDetails(Request("OrderId"), lbl_mn.Text, smsMsg, smsStatus)
    '                    Catch ex As Exception

    '                    End Try

    '                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Pnr Imported Sucessfully');window.location='ProcessImportPnrIntl.aspx';", True)
    '                Else
    '                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to update import.Please try after some time.');", True)
    '                End If

    '            Else
    '                'msgboxdiv.InnerHtml = "<script>alert('insufficient Credit Limit');</script>"
    '                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('insufficient Credit Limit');", True)

    '            End If

    '        Catch ex As Exception
    '            clsErrorLog.LogInfo(ex)

    '        End Try
    '    Else
    '        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Import already updated');", True)
    '    End If

    'End Sub

    'Public Sub CalFareDetails(ByVal Orderid As String, ByVal VC As String, ByVal PaxType As String, ByVal BaseFare As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal OT As Integer, ByVal Adminmrk As Double)
    '    Try


    '        Dim Tax As String = ""
    '        Tax = "YQ:" & YQ.ToString() & "#YR:" & YR.ToString() & "#OT:" & OT.ToString() & "#WO:" & WO.ToString() & "#"
    '        'Calculate Commission
    '        Dim cls As String = ""
    '        Dim Origin As String = ""
    '        Dim Dest As String = ""
    '        Dim GroupType As String = ""
    '        Dim ds As New DataSet
    '        Dim IntAirDt As New DataTable
    '        Dim dsG As New DataSet
    '        Dim DtG As New DataTable
    '        Dim Comm As Integer = 0
    '        Dim tdsper As Double = 0
    '        Dim Tds As Integer = 0
    '        Dim srvtax As Double = 0

    '        If (IsCorp = True) Then
    '            '=====================================================CORPORATE==============================================

    '            ds = ST.PnrImportIntlDetails(Orderid, "I")
    '            IntAirDt = ds.Tables(0)
    '            dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
    '            DtG = dsG.Tables(0)
    '            GroupType = DtG.Rows(0)("agent_type").ToString
    '            Dim dtmgtfee As New DataTable
    '            'If PaxType <> "INF" Then
    '            dtmgtfee = ClsCorp.GetManagementFeeSrvTax(GroupType, VC, BaseFare, YQ, "I", (BaseFare + YQ + YR + WO + OT)).Tables(0)
    '            Comm = Convert.ToDouble(dtmgtfee.Rows(0)("MGTFEE").ToString())
    '            srvtax = Convert.ToDouble(dtmgtfee.Rows(0)("MGTSRVTAX").ToString())
    '            Tds = 0
    '            'End If
    '            ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, Adminmrk, 0, 0, Comm, 0, srvtax, 0, 0, VC, "I")

    '            '=====================================================END CORPORATE==============================================


    '        Else
    '            ds = ST.PnrImportIntlDetails(Orderid, "I")
    '            IntAirDt = ds.Tables(0)
    '            For i As Integer = 0 To IntAirDt.Rows.Count - 1
    '                cls = cls & IntAirDt.Rows(i)("RDB") & ":"
    '            Next
    '            Origin = IntAirDt.Rows(0)("Departure")
    '            Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")

    '            dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
    '            DtG = dsG.Tables(0)
    '            GroupType = DtG.Rows(0)("agent_type").ToString
    '            Dim DttDate As String = ""
    '            Dim RtDate As String = ""

    '            If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
    '                DttDate = IntAirDt.Rows(0)("departdate").ToString()
    '                RtDate = ""
    '            Else
    '                DttDate = IntAirDt.Rows(0)("departdate").ToString()
    '                RtDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
    '            End If
    '            Dim dtsrv As New DataTable, srvtax1 As String, STax As Double = 0, Comm1 As Double = 0
    '            If PaxType <> "INF" Then
    '                Comm = CCAP.calcComm(GroupType, VC, BaseFare, YQ, Origin, Dest, cls, 0, DttDate, RtDate)
    '                '*****
    '                dtsrv = ST.calcServicecharge(VC, "I").Tables(0)
    '                srvtax1 = dtsrv.Rows(0)("SrvTax").ToString
    '                If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
    '                    STax = Math.Round(((Comm * srvtax1) / 100), 0)
    '                Else
    '                    STax = 0
    '                End If
    '                '*****
    '                Comm1 = Comm
    '                Comm = Comm - STax
    '                'Cal TDS
    '                tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
    '                Tds = Comm * Convert.ToDouble(tdsper) / 100
    '            End If




    '            ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, Tds, VC, "I", Comm1, "")
    '        End If


    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
    'Public Sub CalFareDetailsCorp(ByVal Orderid As String, ByVal VC As String, ByVal PaxType As String, ByVal BaseFare As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal OT As Integer)
    '    Try


    '        Dim Tax As String = ""
    '        Tax = "YQ:" & YQ.ToString() & "#YR:" & YR.ToString() & "#OT:" & OT.ToString() & "#WO:" & WO.ToString() & "#"
    '        'Calculate Commission
    '        Dim cls As String = ""
    '        Dim Origin As String = ""
    '        Dim Dest As String = ""
    '        Dim GroupType As String = ""
    '        Dim ds As New DataSet
    '        Dim IntAirDt As New DataTable
    '        Dim dsG As New DataSet
    '        Dim DtG As New DataTable
    '        Dim Comm As Integer = 0
    '        Dim tdsper As Double = 0
    '        Dim Tds As Integer = 0


    '        ds = ST.PnrImportIntlDetails(Orderid, "I")
    '        IntAirDt = ds.Tables(0)
    '        For i As Integer = 0 To IntAirDt.Rows.Count - 1
    '            cls = cls & IntAirDt.Rows(i)("RDB") & ":"
    '        Next
    '        Origin = IntAirDt.Rows(0)("Departure")
    '        Dest = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("Destination")

    '        dsG = ST.GetAgencyDetails(IntAirDt.Rows(0)("AgentID").ToString)
    '        DtG = dsG.Tables(0)
    '        GroupType = DtG.Rows(0)("agent_type").ToString
    '        Dim DttDate As String = ""
    '        Dim RtDate As String = ""

    '        If IntAirDt.Rows(0)("TripType").ToString().ToUpper.Trim = "O" Then
    '            DttDate = IntAirDt.Rows(0)("departdate").ToString()
    '            RtDate = ""
    '        Else
    '            DttDate = IntAirDt.Rows(0)("departdate").ToString()
    '            RtDate = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("departdate").ToString()
    '        End If

    '        If PaxType <> "INF" Then
    '            Comm = CCAP.calcComm(GroupType, VC, BaseFare, YQ, Origin, Dest, cls, 0, DttDate, RtDate)
    '            'Cal TDS
    '            tdsper = CCAP.geTdsPercentagefromDb(IntAirDt.Rows(0)("AgentID").ToString)
    '            Tds = Comm * Convert.ToDouble(tdsper) / 100
    '        End If
    '        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, Tds, VC, "I")

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
End Class


