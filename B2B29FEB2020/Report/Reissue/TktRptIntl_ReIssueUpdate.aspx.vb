Imports System
Imports System.Data
Imports YatraBilling
Imports ITZLib
Partial Class SprReports_Reissue_TktRptIntl_ReIssueUpdate
    Inherits System.Web.UI.Page

    Dim GrdDS As New DataSet()
    Private ST As New SqlTransaction()
    Private clsInsSelectflt As New clsInsertSelectedFlight()
    Private ClsCorp As New ClsCorporate
    Dim STDom As New SqlTransactionDom
#Region "Pawan Kumar"
    Dim objItzBal As New ITZGetbalance
    Dim objItzTrans As New ITZcrdb
    Dim objParamBal As New _GetBalance
    Dim objDebResp As New DebitResponse
    Dim objBalResp As New GetBalanceResponse
    Dim objParamDeb As New _CrOrDb
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
            Try
                If GrdDS.Tables.Count > 0 Then
                    If GrdDS.Tables(0).Rows.Count > 0 Then
                        txt_pnr.Value = GrdDS.Tables(0).Rows(0).Item("pnr_locator").ToString.Trim()
                        txtAirPnr.Value = GrdDS.Tables(0).Rows(0).Item("AirPnr").ToString.Trim()
                        txtTktNo.Value = GrdDS.Tables(0).Rows(0).Item("tkt_No").ToString.Trim()

                        Dim AgencyDS As New DataSet()
                        AgencyDS = ST.GetAgencyDetails(GrdDS.Tables(0).Rows(0).Item("UserID").ToString.Trim())
                        If AgencyDS.Tables(0).Rows.Count > 0 Then
                            Dim dt1 As New DataTable()
                            dt1 = AgencyDS.Tables(0)
                            td_AgentID.InnerText = GrdDS.Tables(0).Rows(0).Item("UserID").ToString()
                            td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString() & ", " & dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
                            td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
                            td_Email.InnerText = dt1.Rows(0)("Email").ToString()
                            td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
                            td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
                        End If
                        'Flight Details strat
                        Bindflight(ST.GetFltDtlsReissue(GrdDS.Tables(0).Rows(0).Item("OrderId").ToString()))
                    Else
                        lblreissuemsg.Text = "Ticket Allready Update."
                    End If
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
    End Sub
    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Update.Click
        Dim objItzT As New Itz_Trans_Dal
        Dim inst As Boolean = False
        Dim objIzT As New ITZ_Trans
        Try
            Dim i As Integer = 0
            For Each row As GridViewRow In ReissueUpdateGrd.Rows
                Dim HdrDs, paxdtlDS As New DataSet()
                Dim lblorderId As String = DirectCast(row.FindControl("lblorderId"), Label).Text
                Dim sector As String = DirectCast(row.FindControl("lblsector"), Label).Text
                Dim paxname As String = DirectCast(row.FindControl("lblpaxfname"), Label).Text
                Dim paxtype As String = DirectCast(row.FindControl("lblpaxtype"), Label).Text.Trim.ToUpper
                Dim PaxID As String = DirectCast(row.FindControl("lblPaxID"), Label).Text

                Dim Status As Boolean = ST.CheckRefundReissueUpdate(PaxID, StatusClass.Ticketed.ToString(), "REISSUE")
                If (Status = 0) Then
                    i = i + 1
                    Try
                        If ReissueUpdateGrd.Rows.Count >= i Then
                            GrdDS = ViewState("GrdDS")
                            If GrdDS.Tables(0).Rows.Count = 1 Then
                                Dim GridTD As DataTable = GrdDS.Tables(0)
                                Session("_DCODE") = GrdDS.Tables(0).Rows(0)("UserID")
                                Dim Sum As Double = Convert.ToDouble(Convert.ToDouble(Request("txt_Reissue_charge").Trim()) + Convert.ToDouble(Request("txt_Service_charge").Trim()) + Convert.ToDouble(Request("txt_farediff").Trim()))

                                Try
                                    objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                    objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                    objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                    objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                    objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                Catch ex As Exception
                                End Try

                                ''Dim BalanceCheck As Integer = ST.CheckBalance(td_AgentID.InnerText.Trim(), Sum)
                                ''If (BalanceCheck = 0) Then
                                If (Convert.ToDouble(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE) > Sum) Then
                                    'Subtract Balance from Agent A/C
                                    'Dim CheckBalStatus As Boolean = False
                                    'Dim ablBalance As Double = ST.UpdateNew_RegsReIssue(td_AgentID.InnerText.Trim(), Sum)
                                    ''Check for available balance
                                    'If (ablBalance = 0) Then
                                    '    Dim dtavl As New DataTable()
                                    '    dtavl = STDom.GetAgencyDetails(td_AgentID.InnerText.Trim()).Tables(0)
                                    '    Dim CurrAvlBal As Double
                                    '    CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
                                    '    If (ablBalance <> CurrAvlBal) Then
                                    '        CheckBalStatus = True
                                    '    End If
                                    'End If
                                    Dim NewOrderID As String = clsInsSelectflt.getRndNum()
                                    Try
                                        objParamDeb._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") IsNot Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                        objParamDeb._PASSWORD = IIf(Session("_PASSWORD") IsNot Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                        objParamDeb._DECODE = IIf(Session("_DCODE") IsNot Nothing, Session("_DCODE").ToString().Trim(), " ")
                                        objParamDeb._AMOUNT = Sum
                                        objParamDeb._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("ITZMode") IsNot Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                                        objParamDeb._ORDERID = IIf(NewOrderID IsNot Nothing AndAlso NewOrderID IsNot "", NewOrderID, " ")
                                        objParamDeb._DESCRIPTION = "Amount debit for reissue to orderId- " & lblorderId.Trim()
                                        ''objParamDeb._CHECKSUM = " "
                                        Dim stringtoenc As String = "MERCHANTKEY=" & objParamDeb._MERCHANT_KEY & "&ORDERID=" & objParamDeb._ORDERID & "&AMOUNT=" & objParamDeb._AMOUNT & "&MODE=" & objParamDeb._MODE
                                        objParamDeb._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                                        objParamDeb._SERVICE_TYPE = IIf(Session("_SvcTypeITZ") <> Nothing, Session("_SvcTypeITZ").ToString().Trim(), "MERCHANTDB") ''IIf(ConfigurationManager.AppSettings("ITZSvcType") IsNot Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                                        objDebResp = objItzTrans.ITZDebit(objParamDeb)
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        objItzT = New Itz_Trans_Dal()
                                        objIzT.AMT_TO_DED = Sum
                                        objIzT.AMT_TO_CRE = "0"
                                        objIzT.B2C_MBLNO_ITZ = IIf(objDebResp.B2C_MOBILENO IsNot Nothing, objDebResp.B2C_MOBILENO, " ")
                                        objIzT.COMMI_ITZ = IIf(objDebResp.COMMISSION IsNot Nothing, objDebResp.COMMISSION, " ")
                                        objIzT.CONVFEE_ITZ = IIf(objDebResp.CONVENIENCEFEE IsNot Nothing, objDebResp.CONVENIENCEFEE, " ")
                                        objIzT.DECODE_ITZ = IIf(objDebResp.DECODE IsNot Nothing AndAlso objDebResp.DECODE <> "" AndAlso objDebResp.DECODE <> " ", objDebResp.DECODE, IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " "))
                                        objIzT.EASY_ORDID_ITZ = IIf(objDebResp.EASY_ORDER_ID IsNot Nothing, objDebResp.EASY_ORDER_ID, " ")
                                        objIzT.EASY_TRANCODE_ITZ = IIf(objDebResp.EASY_TRAN_CODE IsNot Nothing, objDebResp.EASY_TRAN_CODE, " ")
                                        objIzT.ERROR_CODE = IIf(objDebResp.ERROR_CODE IsNot Nothing, objDebResp.ERROR_CODE, " ")
                                        objIzT.MERCHANT_KEY_ITZ = IIf(objDebResp.MERCHANT_KEY IsNot Nothing AndAlso objDebResp.MERCHANT_KEY <> "" AndAlso objDebResp.MERCHANT_KEY <> " ", objDebResp.MERCHANT_KEY, IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " "))
                                        objIzT.MESSAGE_ITZ = IIf(objDebResp.MESSAGE IsNot Nothing, objDebResp.MESSAGE, " ")
                                        objIzT.ORDERID = IIf(NewOrderID IsNot Nothing AndAlso NewOrderID IsNot "", NewOrderID, " ")
                                        objIzT.RATE_GROUP_ITZ = IIf(objDebResp.RATEGROUP IsNot Nothing, objDebResp.RATEGROUP, " ")
                                        objIzT.REFUND_TYPE_ITZ = " "
                                        objIzT.SERIAL_NO_FROM = IIf(objDebResp.SERIALNO_FROM IsNot Nothing, objDebResp.SERIALNO_FROM, " ")
                                        objIzT.SERIAL_NO_TO = IIf(objDebResp.SERIALNO_TO IsNot Nothing, objDebResp.SERIALNO_TO, " ")
                                        objIzT.SVC_TAX_ITZ = IIf(objDebResp.SERVICETAX IsNot Nothing, objDebResp.SERVICETAX, " ")
                                        objIzT.TDS_ITZ = IIf(objDebResp.TDS IsNot Nothing, objDebResp.TDS, " ")
                                        objIzT.TOTAL_AMT_DED_ITZ = IIf(objDebResp.TOTALAMOUNT IsNot Nothing, objDebResp.TOTALAMOUNT, " ")
                                        objIzT.TRANS_TYPE = "REISSUE"
                                        objIzT.USER_NAME_ITZ = IIf(objDebResp.USERNAME IsNot Nothing AndAlso objDebResp.USERNAME <> "" AndAlso objDebResp.USERNAME <> " ", objDebResp.USERNAME, IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " "))
                                        Try
                                            objBalResp = New GetBalanceResponse()
                                            objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                            objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                            objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                            objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                            objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                            objIzT.ACCTYPE_NAME_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME, " ")
                                            objIzT.AVAIL_BAL_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                                        Catch ex As Exception
                                        End Try
                                        inst = objItzT.InsertItzTrans(objIzT)
                                    Catch ex As Exception
                                    End Try

                                    'End Check for available balance
                                    ''If (CheckBalStatus = False) Then
                                    'New OrderID
                                    If objDebResp IsNot Nothing Then
                                        If objDebResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then
                                            'Insert data in Transaction Report
                                            Dim rm As String = "Reissue Charge Against Ticket No " & txtTktNo.Value.Trim()

                                            ' ''Try
                                            ' ''    objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                            ' ''    objParamBal._MERCHANT_KEY = IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                            ' ''    objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                            ' ''    objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                            ' ''    objBalResp = New GetBalanceResponse()
                                            ' ''    objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                            ' ''Catch ex As Exception
                                            ' ''End Try

                                            'ST.InsertTransactionRepot(txt_pnr.Value.Trim(), td_AgentID.InnerText, 0, ablBalance.ToString(), Sum.ToString, sector, paxname, GrdDS.Tables(0).Rows(0)("Flightno"), td_AgencyName.InnerText, rm, StatusClass.Ticketed)
                                            'Get Header Data
                                            HdrDs = ST.GetHdrDetails(lblorderId)
                                            Dim CORPBILLNO As String = Nothing
                                            Dim HdrTD As New DataTable
                                            If HdrDs.Tables(0).Rows.Count > 0 Then
                                                HdrTD = HdrDs.Tables(0)
                                            End If
                                            If (HdrTD.Rows(0)("BillNoCorp") IsNot Nothing AndAlso HdrTD.Rows(0)("BillNoCorp").ToString() <> "") Then
                                                'Dim ClsCorp As New ClsCorporate
                                                CORPBILLNO = ClsCorp.GenerateBillNoCorp("I").ToString()
                                            End If
                                            Dim ProjectId As String = If(IsDBNull(HdrTD.Rows(0).Item("ProjectID")), Nothing, HdrTD.Rows(0).Item("ProjectID").ToString().Trim())
                                            Dim BookedBy As String = If(IsDBNull(HdrTD.Rows(0).Item("BookedBy")), Nothing, HdrTD.Rows(0).Item("BookedBy").ToString().Trim())

                                            ' Insert Data in LedgerDetails Table
                                            STDom.insertLedgerDetails(td_AgentID.InnerText, td_AgencyName.InnerText, NewOrderID, txt_pnr.Value.Trim(), txtTktNo.Value.Trim(), "", "", "", Session("UID").ToString, Request.UserHostAddress, _
                                                           Convert.ToDecimal(Sum), 0, IIf(objBalResp IsNot Nothing, IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL.Length > 0, Convert.ToDecimal(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE), 0), 0), "Ticket ReIssue", rm, Convert.ToInt32(PaxID), ProjectId, BookedBy, CORPBILLNO)

                                            'Update ReissueIntl Table After ReIssue  , lbltktno.Text, lblorderId, txtTktNo.Text.Trim, txt_pnr.Text.Trim, txtAirPnr.Text.Trim, txt_date.Text.Trim, txtDepTime.Text.Trim,
                                            ST.UpdateReissueIntl(Convert.ToInt32(Request("Counter")), Session("UID").ToString, Request("txt_Reissue_charge").Trim(), Request("txt_Service_charge").Trim(), Request("txt_farediff").Trim(), StatusClass.Ticketed)

                                            'Update Status =Cancelled in FltHeader and FltPaxDetails 
                                            ST.UpdateStatus_Pax_Header(lblorderId, GridTD.Rows(0)("pnr_locator").ToString(), Convert.ToInt32(PaxID), StatusClass.ReIssue)



                                            'Insert data in FltPaxDetails
                                            paxdtlDS = ST.GetPaxDetailsReIssue(PaxID, GridTD.Rows(0)("Tkt_No").ToString())
                                            If paxdtlDS.Tables(0).Rows.Count > 0 Then
                                                Dim paxTD As DataTable = paxdtlDS.Tables(0)
                                                Try
                                                    If HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "G8" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "6E" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "SG" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "IX" Then
                                                        ST.insertPaxDetailsReIssue(NewOrderID, txtTktNo.Value.Trim(), GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, _
                                                        paxTD.Rows(0)("MName").ToString, GridTD.Rows(0)("pax_lname").ToString, GridTD.Rows(0)("Pax_Type").ToString, paxTD.Rows(0)("DOB").ToString, _
                                                        paxTD.Rows(0)("FFNumber").ToString, paxTD.Rows(0)("FFAirline").ToString, paxTD.Rows(0)("MealType").ToString, paxTD.Rows(0)("SeatType").ToString, _
                                                        False, paxTD.Rows(0)("InfAssociatePaxName").ToString, StatusClass.Ticketed)
                                                    Else
                                                        ST.insertPaxDetailsReIssue(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txtTktNo.Value.Trim, GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, _
                                                        paxTD.Rows(0)("MName").ToString, GridTD.Rows(0)("pax_lname").ToString, GridTD.Rows(0)("Pax_Type").ToString, paxTD.Rows(0)("DOB").ToString, _
                                                        paxTD.Rows(0)("FFNumber").ToString, paxTD.Rows(0)("FFAirline").ToString, paxTD.Rows(0)("MealType").ToString, paxTD.Rows(0)("SeatType").ToString, _
                                                        False, paxTD.Rows(0)("InfAssociatePaxName").ToString, StatusClass.Ticketed)
                                                    End If
                                                Catch ex As Exception
                                                    clsErrorLog.LogInfo(ex)
                                                End Try
                                            End If

                                            If HdrDs.Tables(0).Rows.Count > 0 Then
                                                'Dim HdrTD As DataTable = HdrDs.Tables(0)
                                                Dim charge, ServiceCharge, FareDiff As Double

                                                'Checking Its Allready ReIssued or not
                                                If HdrDs.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso HdrDs.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
                                                    charge = Convert.ToDouble(Request("txt_Reissue_charge").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuCharge"))
                                                    ServiceCharge = Convert.ToDouble(Request("txt_Service_charge").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuServiseCharge"))
                                                    FareDiff = Convert.ToDouble(Request("txt_farediff").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuFareDiff"))
                                                Else
                                                    charge = Convert.ToDouble(Request("txt_Reissue_charge").Trim())
                                                    ServiceCharge = Convert.ToDouble(Request("txt_Service_charge").Trim())
                                                    FareDiff = Convert.ToDouble(Request("txt_farediff").Trim())
                                                End If
                                                'Insert data in FltHeader

                                                ST.insertFltHdrDetailsReissu(NewOrderID, txt_pnr.Value.Trim(), txtAirPnr.Value.Trim(), GridTD.Rows(0)("Sector").ToString, StatusClass.Ticketed, _
                                                HdrTD.Rows(0)("Duration").ToString, HdrTD.Rows(0)("TripType").ToString, Convert.ToDouble(HdrTD.Rows(0)("TotalBookingCost")), _
                                                Convert.ToDouble(HdrTD.Rows(0)("TotalAfterDis")), Convert.ToInt32(HdrTD.Rows(0)("Adult")), Convert.ToInt32(HdrTD.Rows(0)("Child")), _
                                                Convert.ToInt32(HdrTD.Rows(0)("Infant")), td_AgentID.InnerText, HdrTD.Rows(0)("AgencyName").ToString, "Spring", Session("UID").ToString, _
                                                GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, GridTD.Rows(0)("pax_lname").ToString, HdrTD.Rows(0)("PgMobile").ToString, _
                                                HdrTD.Rows(0)("PgEmail").ToString, HdrTD.Rows(0)("VC").ToString, Convert.ToDouble(HdrTD.Rows(0)("AdditionalMarkup")), _
                                                HdrTD.Rows(0)("Trip").ToString, GridTD.Rows(0)("ReIssueID").ToString, charge, ServiceCharge, FareDiff, ProjectId, CORPBILLNO, BookedBy)
                                            End If
                                            'Insert data in FltDetails 

                                            Dim FltTD As DataTable = DirectCast(ViewState("fltdsds"), DataSet).Tables(0)
                                            For flt As Integer = 0 To FltTD.Rows.Count - 1
                                                Try
                                                    ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
                                                       FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, FltTD.Rows(flt)("DEPDATE").ToString(), FltTD.Rows(flt)("DEPTIME").ToString(), _
                                                       FltTD.Rows(flt)("ARRDATE").ToString(), FltTD.Rows(flt)("ARRTIME").ToString(), FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
                                                       FltTD.Rows(flt)("FLTNUMBER").ToString(), FltTD.Rows(flt)("AirCraft").ToString())
                                                Catch ex As Exception
                                                    clsErrorLog.LogInfo(ex)
                                                End Try
                                            Next
                                            ''Insert data in FltDetails 
                                            'FltDs = ST.GetFltDtlsReissue(lblorderId)
                                            'Try
                                            '    If FltDs.Tables(0).Rows.Count > 0 Then
                                            '        Dim FltTD As DataTable = FltDs.Tables(0)
                                            '        'InsertFlightDetails(FltTD, GridTD, NewOrderID)
                                            '        InsertFlightDetails(GridTD, NewOrderID)
                                            '    End If
                                            'Catch ex As Exception
                                            '    clsErrorLog.LogInfo(ex)
                                            'End Try
                                            Dim fareDtlDS As New DataSet()
                                            'Insert data in FareDetails
                                            fareDtlDS = ST.GetFltFareDtlReIssue(lblorderId, paxtype)
                                            Try
                                                If fareDtlDS.Tables(0).Rows.Count > 0 Then
                                                    Dim fareDT As DataTable = fareDtlDS.Tables(0)
                                                    Dim MgtFee As String = If(IsDBNull(fareDT.Rows(0).Item("MgtFee")), Nothing, fareDT.Rows(0).Item("MgtFee").ToString().Trim())
                                                    ST.insertFareDetailsReIssue(NewOrderID, GridTD.Rows(0)("Base_Fare").ToString, GridTD.Rows(0)("YQ").ToString, fareDT.Rows(0)("YR").ToString, _
                                                    fareDT.Rows(0)("WO").ToString, fareDT.Rows(0)("OT").ToString, fareDT.Rows(0)("TotalTax").ToString, GridTD.Rows(0)("TotalFare").ToString, _
                                                    GridTD.Rows(0)("Service_Tax").ToString, GridTD.Rows(0)("Tran_Fees").ToString, fareDT.Rows(0)("AdminMrk").ToString, fareDT.Rows(0)("AgentMrk").ToString, _
                                                    fareDT.Rows(0)("DistrMrk").ToString, GridTD.Rows(0)("Discount").ToString, GridTD.Rows(0)("TDS").ToString, GridTD.Rows(0)("VC").ToString, _
                                                    GridTD.Rows(0)("Trip").ToString, GridTD.Rows(0)("TotalFareAfterDiscount").ToString, GridTD.Rows(0)("pax_type").ToString, MgtFee)
                                                End If
                                            Catch ex As Exception
                                                clsErrorLog.LogInfo(ex)
                                            End Try
                                            'NAV METHOD  CALL START
                                            Try

                                                'Dim objNav As New AirService.clsConnection(NewOrderID, "1", "0")
                                                'objNav.airBookingNav(NewOrderID, "", 1)

                                            Catch ex As Exception

                                            End Try
                                            'Nav METHOD END'
                                            'Online Yatra
                                            Try
                                                'Dim AirObj As New AIR_YATRA
                                                'AirObj.ProcessYatra_Air(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim(), "R")
                                            Catch ex As Exception
                                                clsErrorLog.LogInfo(ex)
                                            End Try
                                            'Online Yatra  end

                                            'For Win Yatra Update
                                            'Try
                                            '    Dim STYTR As New SqlTransactionYatra
                                            '    STYTR.InsertYatra_MIRHEADER(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
                                            '    STYTR.InsertYatra_PAX(NewOrderID, "RE" & Right(Request("Counter"), 2).ToString.Trim & "-" & txt_pnr.Value.Trim())
                                            '    STYTR.InsertYatra_SEGMENT(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
                                            '    STYTR.InsertYatra_RESUFARE(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
                                            '    STYTR.InsertYatra_DIFTLINES(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
                                            'Catch ex As Exception
                                            '    clsErrorLog.LogInfo(ex)
                                            'End Try

                                            BindGrid()
                                            Response.Write("<script>javascript:window.close();</script>")
                                            ''Else
                                            ''    lblreissuemsg.Text = "Unable to reissue.Please try after some time."
                                            ''End If
                                        Else
                                            lblreissuemsg.Text = "Unable to reissue.Please try after some time."
                                        End If
                                    Else
                                        lblreissuemsg.Text = "Unable to reissue.Please try after some time."
                                    End If
                                    ''Else
                                    ''End If
                                Else
                                    lblreissuemsg.Text = "Agent Dose Not Have Sufficient Credit Limit.Please contact administrator."
                                End If
                            End If
                            'ResetFormControlValues(Me)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                        lblreissuemsg.Text = ex.Message
                    End Try
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Already Reissued');javascript:window.close();", True)
                End If
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            GrdDS = ST.GetReIssueCancelIntl(Convert.ToInt32(Request("Counter")), "R", StatusClass.InProcess)
            ReissueUpdateGrd.DataSource = GrdDS
            ReissueUpdateGrd.DataBind()
            ViewState("GrdDS") = GrdDS
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script>javascript:window.close();</script>")
    End Sub
    Private Sub Bindflight(ByVal ds As DataSet)
        Try
            ViewState("fltdsds") = ds
            Flightgrid.DataSource = ds
            Flightgrid.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub Flightgrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Flightgrid.RowUpdating
        Try
            Flightgrid.EditIndex = -1
            Dim DS As New DataSet()
            DS = ViewState("fltdsds")
            For Each strow In DS.Tables(0).Rows
                If strow.Item("FltId").ToString = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label).Text Then
                    strow.Item("DEPDATE") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtDEPDATEs"), TextBox), TextBox).Text
                    strow.Item("DEPTIME") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtDEPTIMEs"), TextBox), TextBox).Text
                    strow.Item("ARRDATE") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtARRDATEs"), TextBox), TextBox).Text
                    strow.Item("ARRTIME") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtARRTIMEs"), TextBox), TextBox).Text
                    strow.Item("FLTNUMBER") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtFLTNUMBERs"), TextBox), TextBox).Text
                    DS.AcceptChanges()
                End If
            Next
            Bindflight(DS)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub Flightgrid_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Flightgrid.RowCancelingEdit
        Flightgrid.EditIndex = -1
        Bindflight(ViewState("fltdsds"))
    End Sub

    Protected Sub Flightgrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Flightgrid.RowEditing
        Flightgrid.EditIndex = e.NewEditIndex
        Bindflight(ViewState("fltdsds"))
    End Sub
    'Private Sub InsertFlightDetails(ByVal GridTD As DataTable, ByVal NewOrderID As String)
    '    Try
    '        Dim FltTD As DataTable = DirectCast(ViewState("fltdsds"), DataSet).Tables(0)
    '        Dim flt As Integer = 0
    '        For Each row As GridViewRow In Flightgrid.Rows
    '            Try
    '                Dim txt_date As String = DirectCast(row.FindControl("DEPDATEs"), Label).Text.Trim()
    '                Dim txtDepTime As String = DirectCast(row.FindControl("DEPTIMEs"), Label).Text.Trim()
    '                Dim txtArivalDate As String = DirectCast(row.FindControl("ARRDATEs"), Label).Text.Trim()
    '                Dim txtArivalTime As String = DirectCast(row.FindControl("ARRTIMEs"), Label).Text.Trim()
    '                Dim txtFltNo As String = DirectCast(row.FindControl("FLTNUMBERs"), Label).Text.Trim()

    '                ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                                      FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date, txtDepTime, _
    '                                      txtArivalDate, txtArivalTime, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                                      txtFltNo, FltTD.Rows(flt)("AirCraft").ToString())
    '            Catch ex As Exception
    '                clsErrorLog.LogInfo(ex)
    '            End Try
    '            flt = flt + 1
    '        Next    
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub
    'For flt As Integer = 0 To FltTD.Rows.Count - 1
    '    Try
    '        If flt = 0 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                           FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date.Value, txtDepTime.Value, _
    '                           txtArivalDate.Value, txtArivalTime.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                           txtFltNo.Value, FltTD.Rows(flt)("AirCraft").ToString())
    '        ElseIf flt = 1 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date2.Value, txtDepTime2.Value, _
    '                          txtArivalDate2.Value, txtArivalTime2.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                          txtFltNo2.Value, FltTD.Rows(flt)("AirCraft").ToString())

    '        ElseIf flt = 2 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date3.Value, txtDepTime3.Value, _
    '                          txtArivalDate3.Value, txtArivalTime3.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                          txtFltNo3.Value, FltTD.Rows(flt)("AirCraft").ToString())
    '        ElseIf flt = 3 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date4.Value, txtDepTime4.Value, _
    '                          txtArivalDate4.Value, txtArivalTime4.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                          txtFltNo4.Value, FltTD.Rows(flt)("AirCraft").ToString())
    '        ElseIf flt = 4 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date5.Value, txtDepTime5.Value, _
    '                          txtArivalDate5.Value, txtArivalTime5.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                          txtFltNo5.Value, FltTD.Rows(flt)("AirCraft").ToString())
    '        ElseIf flt = 5 Then
    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date6.Value, txtDepTime6.Value, _
    '                          txtArivalDate6.Value, txtArivalTime6.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
    '                          txtFltNo6.Value, FltTD.Rows(flt)("AirCraft").ToString())
    '        End If
    '    Catch ex As Exception
    '    clsErrorLog.LogInfo(ex)
    'End Try
    'Next



    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Not IsPostBack Then
    '        BindGrid()
    '        Try
    '            If GrdDS.Tables.Count > 0 Then
    '                If GrdDS.Tables(0).Rows.Count > 0 Then
    '                    Dim AgencyDS As New DataSet()
    '                    AgencyDS = ST.GetAgencyDetails(GrdDS.Tables(0).Rows(0).Item("UserID").ToString.Trim())
    '                    If AgencyDS.Tables(0).Rows.Count > 0 Then
    '                        Dim dt1 As New DataTable()
    '                        dt1 = AgencyDS.Tables(0)
    '                        td_AgentID.InnerText = GrdDS.Tables(0).Rows(0).Item("UserID").ToString()
    '                        td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString() & ", " & dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
    '                        td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
    '                        td_Email.InnerText = dt1.Rows(0)("Email").ToString()
    '                        td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
    '                        td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
    '                    End If
    '                    'Flight Details strat
    '                    Dim FltDS As New DataSet()
    '                    FltDS = ST.GetFltDtlsReissue(GrdDS.Tables(0).Rows(0).Item("OrderId").ToString())

    '                    Bindflight(FltDS)


    '                    If FltDS.Tables.Count > 0 Then
    '                        If FltDS.Tables(0).Rows.Count > 0 Then
    '                            ViewState("FltDT") = FltDS.Tables(0)
    '                            txt_date.Value = FltDS.Tables(0).Rows(0)("DepDate").ToString()
    '                            txtDepTime.Value = FltDS.Tables(0).Rows(0)("DepTime").ToString()
    '                            txtArivalDate.Value = FltDS.Tables(0).Rows(0)("ArrDate").ToString()
    '                            txtArivalTime.Value = FltDS.Tables(0).Rows(0)("ArrTime").ToString()
    '                            txtFltNo.Value = FltDS.Tables(0).Rows(0)("FltNumber").ToString()
    '                            txt_pnr.Value = GrdDS.Tables(0).Rows(0).Item("pnr_locator").ToString()
    '                            txtAirPnr.Value = GrdDS.Tables(0).Rows(0).Item("AirPnr").ToString()
    '                            txtTktNo.Value = GrdDS.Tables(0).Rows(0).Item("Tkt_No").ToString()

    '                            If FltDS.Tables(0).Rows.Count > 1 Then
    '                                Sector1.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(0)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(0)("ARRCITYORAIRPORTCODE").ToString()
    '                                Sector2.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(1)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(1)("ARRCITYORAIRPORTCODE").ToString()
    '                                SectorTR1.Visible = True
    '                                SectorTR2.Visible = True
    '                                TRSecondFlt.Visible = True
    '                                TRSecondFlt2.Visible = True

    '                                For i As Integer = 1 To FltDS.Tables(0).Rows.Count - 1
    '                                    If i = 1 Then
    '                                        txt_date2.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
    '                                        txtDepTime2.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
    '                                        txtArivalDate2.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
    '                                        txtArivalTime2.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
    '                                        txtFltNo2.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
    '                                    End If
    '                                    If i = 2 Then
    '                                        Sector3.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
    '                                        SectorTr3.Visible = True
    '                                        TRThirdFlt.Visible = True
    '                                        TRThirdFlt2.Visible = True
    '                                        txt_date3.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
    '                                        txtDepTime3.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
    '                                        txtArivalDate3.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
    '                                        txtArivalTime3.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
    '                                        txtFltNo3.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
    '                                    End If
    '                                    If i = 3 Then
    '                                        Sector4.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
    '                                        SectorTr4.Visible = True
    '                                        TRFourthFlt.Visible = True
    '                                        TRFourthFlt2.Visible = True
    '                                        txt_date4.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
    '                                        txtDepTime4.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
    '                                        txtArivalDate4.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
    '                                        txtArivalTime4.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
    '                                        txtFltNo4.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
    '                                    End If
    '                                    If i = 4 Then
    '                                        Sector5.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
    '                                        SectorTr5.Visible = True
    '                                        TRFifthFlt.Visible = True
    '                                        TRFifthFlt.Visible = True
    '                                        txt_date5.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
    '                                        txtDepTime5.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
    '                                        txtArivalDate5.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
    '                                        txtArivalTime5.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
    '                                        txtFltNo5.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
    '                                    End If
    '                                    If i = 5 Then
    '                                        Sector6.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
    '                                        SectorTr6.Visible = True
    '                                        TRSixthFlt.Visible = True
    '                                        TRSixthFlt2.Visible = True
    '                                        txt_date6.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
    '                                        txtDepTime6.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
    '                                        txtArivalDate6.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
    '                                        txtArivalTime6.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
    '                                        txtFltNo6.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
    '                                    End If
    '                                Next
    '                            End If
    '                        End If
    '                    End If
    '                    'Flight Details END
    '                Else
    '                    lblreissuemsg.Text = "Ticket Allready Update."
    '                End If
    '            End If
    '        Catch ex As Exception
    '            clsErrorLog.LogInfo(ex)
    '        End Try
    '    End If
    'End Sub
End Class


''Imports System
''Imports System.Data
''Imports YatraBilling
''Partial Class SprReports_Reissue_TktRptIntl_ReIssueUpdate
''    Inherits System.Web.UI.Page

''    Dim GrdDS As New DataSet()
''    Private ST As New SqlTransaction()
''    Private clsInsSelectflt As New clsInsertSelectedFlight()
''    Private ClsCorp As New ClsCorporate
''    Dim STDom As New SqlTransactionDom
''    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
''        If Not IsPostBack Then
''            BindGrid()
''            Try
''                If GrdDS.Tables.Count > 0 Then
''                    If GrdDS.Tables(0).Rows.Count > 0 Then
''                        txt_pnr.Value = GrdDS.Tables(0).Rows(0).Item("pnr_locator").ToString.Trim()
''                        txtAirPnr.Value = GrdDS.Tables(0).Rows(0).Item("AirPnr").ToString.Trim()
''                        txtTktNo.Value = GrdDS.Tables(0).Rows(0).Item("tkt_No").ToString.Trim()

''                        Dim AgencyDS As New DataSet()
''                        AgencyDS = ST.GetAgencyDetails(GrdDS.Tables(0).Rows(0).Item("UserID").ToString.Trim())
''                        If AgencyDS.Tables(0).Rows.Count > 0 Then
''                            Dim dt1 As New DataTable()
''                            dt1 = AgencyDS.Tables(0)
''                            td_AgentID.InnerText = GrdDS.Tables(0).Rows(0).Item("UserID").ToString()
''                            td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString() & ", " & dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
''                            td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
''                            td_Email.InnerText = dt1.Rows(0)("Email").ToString()
''                            td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
''                            td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
''                        End If
''                        'Flight Details strat
''                        Bindflight(ST.GetFltDtlsReissue(GrdDS.Tables(0).Rows(0).Item("OrderId").ToString()))
''                    Else
''                        lblreissuemsg.Text = "Ticket Allready Update."
''                    End If
''                End If
''            Catch ex As Exception
''            clsErrorLog.LogInfo(ex)
''        End Try
''        End If
''    End Sub
''    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Update.Click
''        Try
''            Dim i As Integer = 0
''            For Each row As GridViewRow In ReissueUpdateGrd.Rows
''                Dim HdrDs, paxdtlDS As New DataSet()
''                Dim lblorderId As String = DirectCast(row.FindControl("lblorderId"), Label).Text
''                Dim sector As String = DirectCast(row.FindControl("lblsector"), Label).Text
''                Dim paxname As String = DirectCast(row.FindControl("lblpaxfname"), Label).Text
''                Dim paxtype As String = DirectCast(row.FindControl("lblpaxtype"), Label).Text.Trim.ToUpper
''                Dim PaxID As String = DirectCast(row.FindControl("lblPaxID"), Label).Text

''                Dim Status As Boolean = ST.CheckRefundReissueUpdate(PaxID, StatusClass.Ticketed.ToString(), "REISSUE")
''                If (Status = 0) Then
''                    i = i + 1
''                    Try
''                        If ReissueUpdateGrd.Rows.Count >= i Then
''                            GrdDS = ViewState("GrdDS")
''                            If GrdDS.Tables(0).Rows.Count = 1 Then
''                                Dim GridTD As DataTable = GrdDS.Tables(0)
''                                Dim Sum As Double = Convert.ToDouble(Convert.ToDouble(Request("txt_Reissue_charge").Trim()) + Convert.ToDouble(Request("txt_Service_charge").Trim()) + Convert.ToDouble(Request("txt_farediff").Trim()))

''                                Dim BalanceCheck As Integer = ST.CheckBalance(td_AgentID.InnerText.Trim(), Sum)
''                                If (BalanceCheck = 0) Then
''                                    'Subtract Balance from Agent A/C
''                                    Dim CheckBalStatus As Boolean = False
''                                    Dim ablBalance As Double = ST.UpdateNew_RegsReIssue(td_AgentID.InnerText.Trim(), Sum)
''                                    'Check for available balance
''                                    If (ablBalance = 0) Then
''                                        Dim dtavl As New DataTable()
''                                        dtavl = STDom.GetAgencyDetails(td_AgentID.InnerText.Trim()).Tables(0)
''                                        Dim CurrAvlBal As Double
''                                        CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
''                                        If (ablBalance <> CurrAvlBal) Then
''                                            CheckBalStatus = True
''                                        End If
''                                    End If
''                                    'End Check for available balance
''                                    If (CheckBalStatus = False) Then
''                                        'New OrderID
''                                        Dim NewOrderID As String = clsInsSelectflt.getRndNum()

''                                        'Insert data in Transaction Report
''                                        Dim rm As String = "Reissue Charge Against Ticket No " & txtTktNo.Value.Trim()
''                                        'ST.InsertTransactionRepot(txt_pnr.Value.Trim(), td_AgentID.InnerText, 0, ablBalance.ToString(), Sum.ToString, sector, paxname, GrdDS.Tables(0).Rows(0)("Flightno"), td_AgencyName.InnerText, rm, StatusClass.Ticketed)
''                                        'Get Header Data
''                                        HdrDs = ST.GetHdrDetails(lblorderId)
''                                        Dim CORPBILLNO As String = Nothing
''                                        Dim HdrTD As New DataTable
''                                        If HdrDs.Tables(0).Rows.Count > 0 Then
''                                            HdrTD = HdrDs.Tables(0)
''                                        End If
''                                        If (HdrTD.Rows(0)("BillNoCorp") IsNot Nothing AndAlso HdrTD.Rows(0)("BillNoCorp").ToString() <> "") Then
''                                            'Dim ClsCorp As New ClsCorporate
''                                            CORPBILLNO = ClsCorp.GenerateBillNoCorp("I").ToString()
''                                        End If
''                                        Dim ProjectId As String = If(IsDBNull(HdrTD.Rows(0).Item("ProjectID")), Nothing, HdrTD.Rows(0).Item("ProjectID").ToString().Trim())
''                                        Dim BookedBy As String = If(IsDBNull(HdrTD.Rows(0).Item("BookedBy")), Nothing, HdrTD.Rows(0).Item("BookedBy").ToString().Trim())

''                                        ' Insert Data in LedgerDetails Table
''                                        STDom.insertLedgerDetails(td_AgentID.InnerText, td_AgencyName.InnerText, NewOrderID, txt_pnr.Value.Trim(), txtTktNo.Value.Trim(), "", "", "", Session("UID").ToString, Request.UserHostAddress, _
''                                                            Convert.ToDecimal(Sum), 0, Convert.ToDecimal(ablBalance), "Ticket ReIssue", rm, Convert.ToInt32(PaxID), ProjectId, BookedBy, CORPBILLNO)

''                                        'Update ReissueIntl Table After ReIssue  , lbltktno.Text, lblorderId, txtTktNo.Text.Trim, txt_pnr.Text.Trim, txtAirPnr.Text.Trim, txt_date.Text.Trim, txtDepTime.Text.Trim,
''                                        ST.UpdateReissueIntl(Convert.ToInt32(Request("Counter")), Session("UID").ToString, Request("txt_Reissue_charge").Trim(), Request("txt_Service_charge").Trim(), Request("txt_farediff").Trim(), StatusClass.Ticketed)

''                                        'Update Status =Cancelled in FltHeader and FltPaxDetails 
''                                        ST.UpdateStatus_Pax_Header(lblorderId, GridTD.Rows(0)("pnr_locator").ToString(), Convert.ToInt32(PaxID), StatusClass.ReIssue)



''                                        'Insert data in FltPaxDetails
''                                        paxdtlDS = ST.GetPaxDetailsReIssue(PaxID, GridTD.Rows(0)("Tkt_No").ToString())
''                                        If paxdtlDS.Tables(0).Rows.Count > 0 Then
''                                            Dim paxTD As DataTable = paxdtlDS.Tables(0)
''                                            Try
''                                                If HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "G8" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "6E" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "SG" And HdrDs.Tables(0).Rows(0)("VC").ToString.Trim.ToUpper <> "IX" Then
''                                                    ST.insertPaxDetailsReIssue(NewOrderID, txtTktNo.Value.Trim(), GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, _
''                                                    paxTD.Rows(0)("MName").ToString, GridTD.Rows(0)("pax_lname").ToString, GridTD.Rows(0)("Pax_Type").ToString, paxTD.Rows(0)("DOB").ToString, _
''                                                    paxTD.Rows(0)("FFNumber").ToString, paxTD.Rows(0)("FFAirline").ToString, paxTD.Rows(0)("MealType").ToString, paxTD.Rows(0)("SeatType").ToString, _
''                                                    False, paxTD.Rows(0)("InfAssociatePaxName").ToString, StatusClass.Ticketed)
''                                                Else
''                                                    ST.insertPaxDetailsReIssue(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txtTktNo.Value.Trim, GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, _
''                                                    paxTD.Rows(0)("MName").ToString, GridTD.Rows(0)("pax_lname").ToString, GridTD.Rows(0)("Pax_Type").ToString, paxTD.Rows(0)("DOB").ToString, _
''                                                    paxTD.Rows(0)("FFNumber").ToString, paxTD.Rows(0)("FFAirline").ToString, paxTD.Rows(0)("MealType").ToString, paxTD.Rows(0)("SeatType").ToString, _
''                                                    False, paxTD.Rows(0)("InfAssociatePaxName").ToString, StatusClass.Ticketed)
''                                                End If
''                                            Catch ex As Exception
''                                                clsErrorLog.LogInfo(ex)
''                                            End Try
''                                        End If

''                                        If HdrDs.Tables(0).Rows.Count > 0 Then
''                                            'Dim HdrTD As DataTable = HdrDs.Tables(0)
''                                            Dim charge, ServiceCharge, FareDiff As Double

''                                            'Checking Its Allready ReIssued or not
''                                            If HdrDs.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso HdrDs.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
''                                                charge = Convert.ToDouble(Request("txt_Reissue_charge").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuCharge"))
''                                                ServiceCharge = Convert.ToDouble(Request("txt_Service_charge").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuServiseCharge"))
''                                                FareDiff = Convert.ToDouble(Request("txt_farediff").Trim()) + Convert.ToDouble(HdrTD.Rows(0)("ResuFareDiff"))
''                                            Else
''                                                charge = Convert.ToDouble(Request("txt_Reissue_charge").Trim())
''                                                ServiceCharge = Convert.ToDouble(Request("txt_Service_charge").Trim())
''                                                FareDiff = Convert.ToDouble(Request("txt_farediff").Trim())
''                                            End If
''                                            'Insert data in FltHeader

''                                            ST.insertFltHdrDetailsReissu(NewOrderID, txt_pnr.Value.Trim(), txtAirPnr.Value.Trim(), GridTD.Rows(0)("Sector").ToString, StatusClass.Ticketed, _
''                                            HdrTD.Rows(0)("Duration").ToString, HdrTD.Rows(0)("TripType").ToString, Convert.ToDouble(HdrTD.Rows(0)("TotalBookingCost")), _
''                                            Convert.ToDouble(HdrTD.Rows(0)("TotalAfterDis")), Convert.ToInt32(HdrTD.Rows(0)("Adult")), Convert.ToInt32(HdrTD.Rows(0)("Child")), _
''                                            Convert.ToInt32(HdrTD.Rows(0)("Infant")), td_AgentID.InnerText, HdrTD.Rows(0)("AgencyName").ToString, "Spring", Session("UID").ToString, _
''                                            GridTD.Rows(0)("Title").ToString, GridTD.Rows(0)("pax_fname").ToString, GridTD.Rows(0)("pax_lname").ToString, HdrTD.Rows(0)("PgMobile").ToString, _
''                                            HdrTD.Rows(0)("PgEmail").ToString, HdrTD.Rows(0)("VC").ToString, Convert.ToDouble(HdrTD.Rows(0)("AdditionalMarkup")), _
''                                            HdrTD.Rows(0)("Trip").ToString, GridTD.Rows(0)("ReIssueID").ToString, charge, ServiceCharge, FareDiff, ProjectId, CORPBILLNO, BookedBy)
''                                        End If
''                                        'Insert data in FltDetails 

''                                        Dim FltTD As DataTable = DirectCast(ViewState("fltdsds"), DataSet).Tables(0)
''                                        For flt As Integer = 0 To FltTD.Rows.Count - 1
''                                            Try
''                                                ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''                                                   FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, FltTD.Rows(flt)("DEPDATE").ToString(), FltTD.Rows(flt)("DEPTIME").ToString(), _
''                                                   FltTD.Rows(flt)("ARRDATE").ToString(), FltTD.Rows(flt)("ARRTIME").ToString(), FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''                                                   FltTD.Rows(flt)("FLTNUMBER").ToString(), FltTD.Rows(flt)("AirCraft").ToString())
''                                            Catch ex As Exception
''                                                clsErrorLog.LogInfo(ex)
''                                            End Try
''                                        Next
''                                        ''Insert data in FltDetails 
''                                        'FltDs = ST.GetFltDtlsReissue(lblorderId)
''                                        'Try
''                                        '    If FltDs.Tables(0).Rows.Count > 0 Then
''                                        '        Dim FltTD As DataTable = FltDs.Tables(0)
''                                        '        'InsertFlightDetails(FltTD, GridTD, NewOrderID)
''                                        '        InsertFlightDetails(GridTD, NewOrderID)
''                                        '    End If
''                                        'Catch ex As Exception
''                                        '    clsErrorLog.LogInfo(ex)
''                                        'End Try
''                                        Dim fareDtlDS As New DataSet()
''                                        'Insert data in FareDetails
''                                        fareDtlDS = ST.GetFltFareDtlReIssue(lblorderId, paxtype)
''                                        Try
''                                            If fareDtlDS.Tables(0).Rows.Count > 0 Then
''                                                Dim fareDT As DataTable = fareDtlDS.Tables(0)
''                                                Dim MgtFee As String = If(IsDBNull(fareDT.Rows(0).Item("MgtFee")), Nothing, fareDT.Rows(0).Item("MgtFee").ToString().Trim())
''                                                ST.insertFareDetailsReIssue(NewOrderID, GridTD.Rows(0)("Base_Fare").ToString, GridTD.Rows(0)("YQ").ToString, fareDT.Rows(0)("YR").ToString, _
''                                                fareDT.Rows(0)("WO").ToString, fareDT.Rows(0)("OT").ToString, fareDT.Rows(0)("TotalTax").ToString, GridTD.Rows(0)("TotalFare").ToString, _
''                                                GridTD.Rows(0)("Service_Tax").ToString, GridTD.Rows(0)("Tran_Fees").ToString, fareDT.Rows(0)("AdminMrk").ToString, fareDT.Rows(0)("AgentMrk").ToString, _
''                                                fareDT.Rows(0)("DistrMrk").ToString, GridTD.Rows(0)("Discount").ToString, GridTD.Rows(0)("TDS").ToString, GridTD.Rows(0)("VC").ToString, _
''                                                GridTD.Rows(0)("Trip").ToString, GridTD.Rows(0)("TotalFareAfterDiscount").ToString, GridTD.Rows(0)("pax_type").ToString, MgtFee)
''                                            End If
''                                        Catch ex As Exception
''                                            clsErrorLog.LogInfo(ex)
''                                        End Try
''                                        'NAV METHOD  CALL START
''                                        Try

''                                            'Dim objNav As New AirService.clsConnection(NewOrderID, "1", "0")
''                                            'objNav.airBookingNav(NewOrderID, "", 1)

''                                        Catch ex As Exception

''                                        End Try
''                                        'Nav METHOD END'
''                                        'Online Yatra
''                                        Try
''                                            'Dim AirObj As New AIR_YATRA
''                                            'AirObj.ProcessYatra_Air(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim(), "R")
''                                        Catch ex As Exception
''                                            clsErrorLog.LogInfo(ex)
''                                        End Try
''                                        'Online Yatra  end

''                                        'For Win Yatra Update
''                                        'Try
''                                        '    Dim STYTR As New SqlTransactionYatra
''                                        '    STYTR.InsertYatra_MIRHEADER(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
''                                        '    STYTR.InsertYatra_PAX(NewOrderID, "RE" & Right(Request("Counter"), 2).ToString.Trim & "-" & txt_pnr.Value.Trim())
''                                        '    STYTR.InsertYatra_SEGMENT(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
''                                        '    STYTR.InsertYatra_RESUFARE(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
''                                        '    STYTR.InsertYatra_DIFTLINES(NewOrderID, "RE" & Right(Request("Counter").ToString.Trim, 2) & "-" & txt_pnr.Value.Trim())
''                                        'Catch ex As Exception
''                                        '    clsErrorLog.LogInfo(ex)
''                                        'End Try

''                                        BindGrid()
''                                        Response.Write("<script>javascript:window.close();</script>")
''                                    Else
''                                        lblreissuemsg.Text = "Unable to reissue.Please try after some time."
''                                    End If

''                                Else

''                                End If
''                            End If
''                            'ResetFormControlValues(Me)
''                        End If
''                    Catch ex As Exception
''                        clsErrorLog.LogInfo(ex)
''                        lblreissuemsg.Text = ex.Message
''                    End Try
''                Else
''                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Already Reissued');javascript:window.close();", True)
''                End If
''            Next
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)
''        End Try
''    End Sub
''    Private Sub BindGrid()
''        Try
''            GrdDS = ST.GetReIssueCancelIntl(Convert.ToInt32(Request("Counter")), "R", StatusClass.InProcess)
''            ReissueUpdateGrd.DataSource = GrdDS
''            ReissueUpdateGrd.DataBind()
''            ViewState("GrdDS") = GrdDS
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)

''        End Try
''    End Sub

''    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
''        Response.Write("<script>javascript:window.close();</script>")
''    End Sub
''    Private Sub Bindflight(ByVal ds As DataSet)
''        Try
''            ViewState("fltdsds") = ds
''            Flightgrid.DataSource = ds
''            Flightgrid.DataBind()
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)
''        End Try
''    End Sub

''    Protected Sub Flightgrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Flightgrid.RowUpdating
''        Try
''            Flightgrid.EditIndex = -1
''            Dim DS As New DataSet()
''            DS = ViewState("fltdsds")
''            For Each strow In DS.Tables(0).Rows
''                If strow.Item("FltId").ToString = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label).Text Then
''                    strow.Item("DEPDATE") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtDEPDATEs"), TextBox), TextBox).Text
''                    strow.Item("DEPTIME") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtDEPTIMEs"), TextBox), TextBox).Text
''                    strow.Item("ARRDATE") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtARRDATEs"), TextBox), TextBox).Text
''                    strow.Item("ARRTIME") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtARRTIMEs"), TextBox), TextBox).Text
''                    strow.Item("FLTNUMBER") = TryCast(DirectCast(Flightgrid.Rows(e.RowIndex).FindControl("txtFLTNUMBERs"), TextBox), TextBox).Text
''                    DS.AcceptChanges()
''                End If
''            Next
''            Bindflight(DS)
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)
''        End Try
''    End Sub

''    Protected Sub Flightgrid_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Flightgrid.RowCancelingEdit
''        Flightgrid.EditIndex = -1
''        Bindflight(ViewState("fltdsds"))
''    End Sub

''    Protected Sub Flightgrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Flightgrid.RowEditing
''        Flightgrid.EditIndex = e.NewEditIndex
''        Bindflight(ViewState("fltdsds"))
''    End Sub
''    'Private Sub InsertFlightDetails(ByVal GridTD As DataTable, ByVal NewOrderID As String)
''    '    Try
''    '        Dim FltTD As DataTable = DirectCast(ViewState("fltdsds"), DataSet).Tables(0)
''    '        Dim flt As Integer = 0
''    '        For Each row As GridViewRow In Flightgrid.Rows
''    '            Try
''    '                Dim txt_date As String = DirectCast(row.FindControl("DEPDATEs"), Label).Text.Trim()
''    '                Dim txtDepTime As String = DirectCast(row.FindControl("DEPTIMEs"), Label).Text.Trim()
''    '                Dim txtArivalDate As String = DirectCast(row.FindControl("ARRDATEs"), Label).Text.Trim()
''    '                Dim txtArivalTime As String = DirectCast(row.FindControl("ARRTIMEs"), Label).Text.Trim()
''    '                Dim txtFltNo As String = DirectCast(row.FindControl("FLTNUMBERs"), Label).Text.Trim()

''    '                ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                                      FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date, txtDepTime, _
''    '                                      txtArivalDate, txtArivalTime, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                                      txtFltNo, FltTD.Rows(flt)("AirCraft").ToString())
''    '            Catch ex As Exception
''    '                clsErrorLog.LogInfo(ex)
''    '            End Try
''    '            flt = flt + 1
''    '        Next    
''    '    Catch ex As Exception
''    '        clsErrorLog.LogInfo(ex)
''    '    End Try
''    'End Sub
''    'For flt As Integer = 0 To FltTD.Rows.Count - 1
''    '    Try
''    '        If flt = 0 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                           FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date.Value, txtDepTime.Value, _
''    '                           txtArivalDate.Value, txtArivalTime.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                           txtFltNo.Value, FltTD.Rows(flt)("AirCraft").ToString())
''    '        ElseIf flt = 1 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date2.Value, txtDepTime2.Value, _
''    '                          txtArivalDate2.Value, txtArivalTime2.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                          txtFltNo2.Value, FltTD.Rows(flt)("AirCraft").ToString())

''    '        ElseIf flt = 2 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date3.Value, txtDepTime3.Value, _
''    '                          txtArivalDate3.Value, txtArivalTime3.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                          txtFltNo3.Value, FltTD.Rows(flt)("AirCraft").ToString())
''    '        ElseIf flt = 3 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date4.Value, txtDepTime4.Value, _
''    '                          txtArivalDate4.Value, txtArivalTime4.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                          txtFltNo4.Value, FltTD.Rows(flt)("AirCraft").ToString())
''    '        ElseIf flt = 4 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date5.Value, txtDepTime5.Value, _
''    '                          txtArivalDate5.Value, txtArivalTime5.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                          txtFltNo5.Value, FltTD.Rows(flt)("AirCraft").ToString())
''    '        ElseIf flt = 5 Then
''    '            ST.insertFltDetailsReIssue(NewOrderID, FltTD.Rows(flt)("DepCityOrAirportCode").ToString, FltTD.Rows(flt)("DepCityOrAirportName").ToString, _
''    '                          FltTD.Rows(flt)("ArrCityOrAirportCode").ToString, FltTD.Rows(flt)("ArrCityOrAirportName").ToString, txt_date6.Value, txtDepTime6.Value, _
''    '                          txtArivalDate6.Value, txtArivalTime6.Value, FltTD.Rows(flt)("AirlineCode").ToString, FltTD.Rows(flt)("AirlineName").ToString, _
''    '                          txtFltNo6.Value, FltTD.Rows(flt)("AirCraft").ToString())
''    '        End If
''    '    Catch ex As Exception
''    '    clsErrorLog.LogInfo(ex)
''    'End Try
''    'Next



''    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
''    '    If Not IsPostBack Then
''    '        BindGrid()
''    '        Try
''    '            If GrdDS.Tables.Count > 0 Then
''    '                If GrdDS.Tables(0).Rows.Count > 0 Then
''    '                    Dim AgencyDS As New DataSet()
''    '                    AgencyDS = ST.GetAgencyDetails(GrdDS.Tables(0).Rows(0).Item("UserID").ToString.Trim())
''    '                    If AgencyDS.Tables(0).Rows.Count > 0 Then
''    '                        Dim dt1 As New DataTable()
''    '                        dt1 = AgencyDS.Tables(0)
''    '                        td_AgentID.InnerText = GrdDS.Tables(0).Rows(0).Item("UserID").ToString()
''    '                        td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString() & ", " & dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
''    '                        td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
''    '                        td_Email.InnerText = dt1.Rows(0)("Email").ToString()
''    '                        td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
''    '                        td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
''    '                    End If
''    '                    'Flight Details strat
''    '                    Dim FltDS As New DataSet()
''    '                    FltDS = ST.GetFltDtlsReissue(GrdDS.Tables(0).Rows(0).Item("OrderId").ToString())

''    '                    Bindflight(FltDS)


''    '                    If FltDS.Tables.Count > 0 Then
''    '                        If FltDS.Tables(0).Rows.Count > 0 Then
''    '                            ViewState("FltDT") = FltDS.Tables(0)
''    '                            txt_date.Value = FltDS.Tables(0).Rows(0)("DepDate").ToString()
''    '                            txtDepTime.Value = FltDS.Tables(0).Rows(0)("DepTime").ToString()
''    '                            txtArivalDate.Value = FltDS.Tables(0).Rows(0)("ArrDate").ToString()
''    '                            txtArivalTime.Value = FltDS.Tables(0).Rows(0)("ArrTime").ToString()
''    '                            txtFltNo.Value = FltDS.Tables(0).Rows(0)("FltNumber").ToString()
''    '                            txt_pnr.Value = GrdDS.Tables(0).Rows(0).Item("pnr_locator").ToString()
''    '                            txtAirPnr.Value = GrdDS.Tables(0).Rows(0).Item("AirPnr").ToString()
''    '                            txtTktNo.Value = GrdDS.Tables(0).Rows(0).Item("Tkt_No").ToString()

''    '                            If FltDS.Tables(0).Rows.Count > 1 Then
''    '                                Sector1.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(0)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(0)("ARRCITYORAIRPORTCODE").ToString()
''    '                                Sector2.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(1)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(1)("ARRCITYORAIRPORTCODE").ToString()
''    '                                SectorTR1.Visible = True
''    '                                SectorTR2.Visible = True
''    '                                TRSecondFlt.Visible = True
''    '                                TRSecondFlt2.Visible = True

''    '                                For i As Integer = 1 To FltDS.Tables(0).Rows.Count - 1
''    '                                    If i = 1 Then
''    '                                        txt_date2.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
''    '                                        txtDepTime2.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
''    '                                        txtArivalDate2.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
''    '                                        txtArivalTime2.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
''    '                                        txtFltNo2.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
''    '                                    End If
''    '                                    If i = 2 Then
''    '                                        Sector3.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
''    '                                        SectorTr3.Visible = True
''    '                                        TRThirdFlt.Visible = True
''    '                                        TRThirdFlt2.Visible = True
''    '                                        txt_date3.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
''    '                                        txtDepTime3.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
''    '                                        txtArivalDate3.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
''    '                                        txtArivalTime3.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
''    '                                        txtFltNo3.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
''    '                                    End If
''    '                                    If i = 3 Then
''    '                                        Sector4.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
''    '                                        SectorTr4.Visible = True
''    '                                        TRFourthFlt.Visible = True
''    '                                        TRFourthFlt2.Visible = True
''    '                                        txt_date4.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
''    '                                        txtDepTime4.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
''    '                                        txtArivalDate4.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
''    '                                        txtArivalTime4.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
''    '                                        txtFltNo4.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
''    '                                    End If
''    '                                    If i = 4 Then
''    '                                        Sector5.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
''    '                                        SectorTr5.Visible = True
''    '                                        TRFifthFlt.Visible = True
''    '                                        TRFifthFlt.Visible = True
''    '                                        txt_date5.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
''    '                                        txtDepTime5.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
''    '                                        txtArivalDate5.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
''    '                                        txtArivalTime5.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
''    '                                        txtFltNo5.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
''    '                                    End If
''    '                                    If i = 5 Then
''    '                                        Sector6.InnerText = "SECTOR - " + FltDS.Tables(0).Rows(i)("DEPCITYORAIRPORTCODE").ToString() + ":" + FltDS.Tables(0).Rows(i)("ARRCITYORAIRPORTCODE").ToString()
''    '                                        SectorTr6.Visible = True
''    '                                        TRSixthFlt.Visible = True
''    '                                        TRSixthFlt2.Visible = True
''    '                                        txt_date6.Value = FltDS.Tables(0).Rows(i)("DEPDATE").ToString()
''    '                                        txtDepTime6.Value = FltDS.Tables(0).Rows(i)("DEPTIME").ToString()
''    '                                        txtArivalDate6.Value = FltDS.Tables(0).Rows(i)("ARRDATE").ToString()
''    '                                        txtArivalTime6.Value = FltDS.Tables(0).Rows(i)("ARRTIME").ToString()
''    '                                        txtFltNo6.Value = FltDS.Tables(0).Rows(i)("FLTNUMBER").ToString()
''    '                                    End If
''    '                                Next
''    '                            End If
''    '                        End If
''    '                    End If
''    '                    'Flight Details END
''    '                Else
''    '                    lblreissuemsg.Text = "Ticket Allready Update."
''    '                End If
''    '            End If
''    '        Catch ex As Exception
''    '            clsErrorLog.LogInfo(ex)
''    '        End Try
''    '    End If
''    'End Sub
''End Class
