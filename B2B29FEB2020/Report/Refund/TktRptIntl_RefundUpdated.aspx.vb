Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports YatraBilling
Imports ITZLib

Partial Class TktRptIntl_RefundUpdated
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Dim GridDS As New DataSet()
    Private Refundfare As Double
    Private STDom As New SqlTransactionDom
#Region "Pawan"
    Dim objRefnResp As New RefundResponse
    Dim objParamCrd As New _CrOrDb
    Dim objCrd As New ITZcrdb
    Dim objItzBal As New ITZGetbalance
    Dim objParamBal As New _GetBalance
    Dim objBalResp As New GetBalanceResponse
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            BindGrid()
            lbluserid.Text = Session("UID")
            Try
                If GridDS.Tables(0).Rows.Count <> 0 Then
                    'Bind Agency DDL
                    Dim AgencyDS As New DataSet()
                    AgencyDS = ST.GetAgencyDetails(GridDS.Tables(0).Rows(0).Item("UserID").ToString.Trim)
                    Dim dt1 As New DataTable()
                    dt1 = AgencyDS.Tables(0)
                    Dim addr As String = dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
                    td_AgentID.InnerText = GridDS.Tables(0).Rows(0).Item("UserID").ToString.Trim
                    td_AgentName.InnerText = dt1.Rows(0)("Name").ToString()
                    td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString()
                    td_Street.InnerText = addr
                    td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
                    td_Email.InnerText = dt1.Rows(0)("Email").ToString()
                    td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
                    td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
                    hdnRefundid.Value = GridDS.Tables(0).Rows(0)("RefundID").ToString()
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If
    End Sub
    Private Sub BindGrid()
        Try
            GridDS = ST.GetReIssueCancelIntl(Convert.ToInt32(Request.QueryString("counter")), "C", StatusClass.InProcess)             'GetCancelletionInt(Request.QueryString("counter").ToString())
            grd_Pax.DataSource = GridDS
            grd_Pax.DataBind()
            ViewState("GridDS") = GridDS
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Update.Click
        Dim result As String = "Ticket Refund Failed"
        Dim objItzT As New Itz_Trans_Dal
        Dim inst As Boolean = False
        Dim objIzT As New ITZ_Trans
        Dim ablBalance As Double = 0
        Try
            For Each row As GridViewRow In grd_Pax.Rows
                Dim lblTktNo As String = DirectCast(row.FindControl("lbltktno"), Label).Text
                Dim lblfare As String = DirectCast(row.FindControl("lbltotalfareafterdiscount"), Label).Text
                Dim lbluser As String = DirectCast(row.FindControl("lbluserid"), Label).Text
                Dim lblSector As String = DirectCast(row.FindControl("lblSector"), Label).Text
                Dim lblpnr As String = DirectCast(row.FindControl("lblpnr"), Label).Text
                Dim lblpaxfname As String = DirectCast(row.FindControl("lblpaxfname"), Label).Text
                Dim lblagencyname As String = DirectCast(row.FindControl("lblagencyname"), Label).Text
                Dim lblorderId As String = DirectCast(row.FindControl("lblorderId"), Label).Text
                Dim TDS As String = DirectCast(row.FindControl("lblTDS"), Label).Text
                Dim BookingDate As String = DirectCast(row.FindControl("lblBookingDate"), Label).Text
                Dim lblPaxID As Integer = Convert.ToInt32(DirectCast(row.FindControl("lblPaxID"), Label).Text)

                Dim mgtFee As String = DirectCast(row.FindControl("HiddenMgtFee"), HiddenField).Value
                Dim mgtFeeVal As Double = If(String.IsNullOrEmpty(mgtFee), 0, Convert.ToDouble(mgtFee))
                Dim ServiceTax As Double = 0
                If (mgtFeeVal > 0) Then
                    Dim SrvTax As String = DirectCast(row.FindControl("HiddenSrvTax"), HiddenField).Value
                    ServiceTax = If(String.IsNullOrEmpty(SrvTax), 0, Convert.ToDouble(SrvTax))
                End If
                Dim FareDiff As Double = 0
                Dim TotlCharge As Double = 0
                Dim AgentId As String = td_AgentID.InnerText.Trim()
                Dim Status As Boolean = ST.CheckRefundReissueUpdate(lblPaxID, StatusClass.Cancelled.ToString(), "REFUND")
                If (Status = 0) Then
                    Dim HdrDs As New DataSet()
                    GetMerchantKey(lblorderId)
                    HdrDs = ST.GetHdrDetails(lblorderId)
                    If HdrDs.Tables(0).Rows.Count <> 0 Then
                        'Checking Its Allready ReIssued or not
                        'If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

                        If HdrDs.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso HdrDs.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
                            FareDiff = Convert.ToDouble(HdrDs.Tables(0).Rows(0)("ResuFareDiff"))
                        End If
                    End If

                    'Checking Booking Month and Today Month is not Same then TDS charge will be deducted 
                    Dim m1 As String = DatePart(DateInterval.Month, Date.Now)
                    Dim m() As String = BookingDate.Split("/")
                    Dim m2 As String = m(0)
                    If m1 = m2 Then
                        TotlCharge = Convert.ToDouble(txt_charge.Text.Trim) + Convert.ToDouble(txt_Service.Text.Trim) + mgtFeeVal + ServiceTax - FareDiff
                        Refundfare = Convert.ToDouble(lblfare) - TotlCharge
                    Else
                        TotlCharge = Convert.ToDouble(txt_charge.Text.Trim) + Convert.ToDouble(txt_Service.Text.Trim) + Convert.ToDouble(TDS) + mgtFeeVal + ServiceTax - FareDiff
                        Refundfare = Convert.ToDouble(lblfare) - TotlCharge
                    End If

                    'Adding Refund Amount in Agent balance

                    'Adding Refund Amount in Agent balance
                    objParamCrd._DECODE = IIf(AgentId <> Nothing, AgentId.ToString().Trim(), " ")

                    Try
                        objParamCrd._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        objParamCrd._AMOUNT = IIf(Refundfare <> Nothing, Refundfare, 0)
                        objParamCrd._ORDERID = IIf(hdnRefundid.Value <> Nothing AndAlso hdnRefundid.Value <> "", hdnRefundid.Value.Trim(), " ")
                        objParamCrd._REFUNDORDERID = IIf(lblorderId <> Nothing AndAlso lblorderId <> "", lblorderId.Trim(), " ")
                        objParamCrd._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                        objParamCrd._REFUNDTYPE = "P"
                        ''objParamCrd._CHECKSUM = " "
                        Dim stringtoenc As String = "MERCHANTKEY=" & objParamCrd._MERCHANT_KEY & "&ORDERID=" & objParamCrd._ORDERID & "&REFUNDTYPE=" & objParamCrd._REFUNDTYPE
                        objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                        'objParamCrd._SERVICE_TYPE = IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                        objParamCrd._DESCRIPTION = "refund to agent -" & AgentId & " against PNR-" & lblpnr
                        objRefnResp = objCrd.ITZRefund(objParamCrd)
                    Catch ex As Exception
                    End Try

                    Try
                        objItzT = New Itz_Trans_Dal()
                        objIzT.AMT_TO_DED = "0"
                        objIzT.AMT_TO_CRE = IIf(Refundfare <> Nothing, Refundfare, 0)
                        objIzT.B2C_MBLNO_ITZ = " "
                        objIzT.COMMI_ITZ = " "
                        objIzT.CONVFEE_ITZ = " "
                        objIzT.DECODE_ITZ = IIf(AgentId <> Nothing, AgentId.ToString().Trim(), " ")
                        objIzT.EASY_ORDID_ITZ = IIf(objRefnResp.EASY_ORDER_ID IsNot Nothing, objRefnResp.EASY_ORDER_ID, " ")
                        objIzT.EASY_TRANCODE_ITZ = IIf(objRefnResp.EASY_TRAN_CODE IsNot Nothing, objRefnResp.EASY_TRAN_CODE, " ")
                        objIzT.ERROR_CODE = IIf(objRefnResp.ERROR_CODE IsNot Nothing, objRefnResp.ERROR_CODE, " ")
                        objIzT.MERCHANT_KEY_ITZ = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        objIzT.MESSAGE_ITZ = IIf(objRefnResp.MESSAGE IsNot Nothing, objRefnResp.MESSAGE, " ")
                        objIzT.ORDERID = IIf(lblorderId <> Nothing AndAlso lblorderId <> "", lblorderId.Trim(), " ")
                        objIzT.RATE_GROUP_ITZ = " "
                        objIzT.REFUND_TYPE_ITZ = IIf(objRefnResp.REFUND_TYPE IsNot Nothing AndAlso objRefnResp.REFUND_TYPE <> "" AndAlso objRefnResp.REFUND_TYPE <> " ", objRefnResp.REFUND_TYPE, " ")
                        objIzT.SERIAL_NO_FROM = " "
                        objIzT.SERIAL_NO_TO = " "
                        objIzT.SVC_TAX_ITZ = " "
                        objIzT.TDS_ITZ = " "
                        objIzT.TOTAL_AMT_DED_ITZ = " "
                        objIzT.TRANS_TYPE = "REFUND"
                        objIzT.USER_NAME_ITZ = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                        Try
                            objBalResp = New GetBalanceResponse()
                            objParamBal._DCODE = IIf(AgentId <> Nothing, AgentId.ToString().Trim(), " ")
                            objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                            objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                            objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                            objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                            objIzT.ACCTYPE_NAME_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME, " ")
                            objIzT.AVAIL_BAL_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                            Session("CL") = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                            ablBalance = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                        Catch ex As Exception
                        End Try
                        inst = objItzT.InsertItzTrans(objIzT)
                    Catch ex As Exception
                    End Try

                    ablBalance = ST.UpdateNew_RegsRefund(lbluser, Refundfare)

                    ''If objRefnResp IsNot Nothing Then
                    ''    If objRefnResp.MESSAGE.Trim().Contains("successfully execute") Then
                    ''Session("CL") = ablBalance
                    ''objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                    ''objParamBal._MERCHANT_KEY = IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    ''objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                    ''objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                    ''objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                    ' ''Session("CL") = ablBalance
                    ''Session("CL") = objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE.Trim()
                    ''ablBalance = objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE.Trim()
                    If objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then

                        Dim rm As String = "ReFund Charge Against Ticket No " & lblTktNo

                        'Insert data in Transaction Report
                        ST.InsertTransactionRepot(lblpnr, lbluser, Refundfare.ToString, ablBalance.ToString, TotlCharge.ToString, lblSector, lblpaxfname, "", lblagencyname, rm, StatusClass.Cancelled)

                        ' Insert Data in LedgerDetails Table
                        STDom.insertLedgerDetails(lbluser, lblagencyname, lblorderId, lblpnr, lblTktNo, "", objIzT.EASY_ORDID_ITZ, "", Session("UID").ToString, Request.UserHostAddress, 0, _
                                            Convert.ToDecimal(Refundfare), Convert.ToDecimal(ablBalance), "Ticket Refund", rm, 0)

                        ' Update Cancellationintl table after Refund 
                        ST.UpdateCancelIntlTbl(Convert.ToInt32(Request.QueryString("counter")), Convert.ToInt32(txt_charge.Text), Convert.ToInt32(txt_Service.Text), Convert.ToInt32(Refundfare), txtRemark.Text, StatusClass.Cancelled)

                        'Update Status =Cancelled in FltHeader and FltPaxDetails 
                        ST.UpdateStatus_Pax_Header(lblorderId, lblpnr, lblPaxID, StatusClass.Cancelled)

                        result = "Ticket Refund Sucessfull"

                    Else
                        result = "Ticket Refund was not Sucessfull,Please Retry"

                    End If

                    ''    Else
                    ''        result = objRefnResp.MESSAGE.Trim()
                    ''    End If
                    ''Else
                    ''    result = "Could not refund the ticket, please try after some time"
                    ''End If

                    'NAV METHOD  CALL START
                    Try

                        'Dim objNav As New AirService.clsConnection(lblorderId, "2", lblPaxID)
                        'objNav.airBookingNav(lblorderId, "", 2)

                    Catch ex As Exception

                    End Try
                    'Nav METHOD END'



                    'Online Yatra
                    Try
                        'Dim AirObj As New AIR_YATRA
                        'AirObj.ProcessYatra_Air_CN(lblorderId, lblpnr, Convert.ToInt32(lblPaxID))
                    Catch ex As Exception

                    End Try
                    'Online Yatra End

                    BindGrid()
                    ''ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Refund Sucessfull');javascript:window.close();window.location='ProcessImportPnr.aspx';", True)
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" + result + "');javascript:window.close();window.location='ProcessImportPnr.aspx';", True)

                    'Response.Write("<script>javascript:window.close();</script>")

                Else

                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Already Refunded');javascript:window.close();window.location='ProcessImportPnr.aspx';", True)

                End If






            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            lblmessage.Text = ex.Message
            Throw
        End Try

    End Sub

    Public Function GetMerchantKey(ByVal orderID As String) As String


        Dim mrchntKey As String = ConfigurationManager.AppSettings("MerchantKey").ToString()

        Try


            Dim provider12 As String = ""
            Dim sqldom As New SqlTransactionDom()

            Dim dsp As New DataSet()

            dsp = sqldom.GetTicketingProvider(orderID)




            If (dsp.Tables.Count > 0) Then
                If (dsp.Tables(0).Rows.Count > 0) Then

                    provider12 = dsp.Tables(0).Rows(0)(0)

                End If


            End If


            If provider12.ToLower().Trim() = "yatra" Then

                mrchntKey = ConfigurationManager.AppSettings("YatraITZMerchantKey").ToString()
            End If

        Catch ex As Exception
            mrchntKey = ConfigurationManager.AppSettings("MerchantKey").ToString()
        End Try
        Session("MchntKeyITZ") = mrchntKey
        Return mrchntKey

    End Function

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script>javascript:window.close();</script>")
    End Sub
End Class

''Imports System.Data
''Imports System.Collections.Generic
''Imports System.Web
''Imports System.Web.UI
''Imports System.Web.UI.WebControls
''Imports YatraBilling

''Partial Class TktRptIntl_RefundUpdated
''    Inherits System.Web.UI.Page
''    Private ST As New SqlTransaction()
''    Dim GridDS As New DataSet()
''    Private Refundfare As Double
''    Private STDom As New SqlTransactionDom
''    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
''        Response.Cache.SetCacheability(HttpCacheability.NoCache)

''        If Session("UID") = "" Or Session("UID") Is Nothing Then
''            Response.Redirect("~/Login.aspx")
''        End If
''        If Not IsPostBack Then
''            BindGrid()
''            lbluserid.Text = Session("UID")
''            Try
''                If GridDS.Tables(0).Rows.Count <> 0 Then
''                    'Bind Agency DDL
''                    Dim AgencyDS As New DataSet()
''                    AgencyDS = ST.GetAgencyDetails(GridDS.Tables(0).Rows(0).Item("UserID").ToString.Trim)
''                    Dim dt1 As New DataTable()
''                    dt1 = AgencyDS.Tables(0)
''                    Dim addr As String = dt1.Rows(0)("city").ToString() & ", " & dt1.Rows(0)("State").ToString() & ", " & dt1.Rows(0)("country").ToString() & ", " & dt1.Rows(0)("zipcode").ToString()
''                    td_AgentID.InnerText = GridDS.Tables(0).Rows(0).Item("UserID").ToString.Trim
''                    td_AgentName.InnerText = dt1.Rows(0)("Name").ToString()
''                    td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString()
''                    td_Street.InnerText = addr
''                    td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
''                    td_Email.InnerText = dt1.Rows(0)("Email").ToString()
''                    td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
''                    td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
''                End If
''            Catch ex As Exception
''                clsErrorLog.LogInfo(ex)

''            End Try
''        End If
''    End Sub
''    Private Sub BindGrid()
''        Try
''            GridDS = ST.GetReIssueCancelIntl(Convert.ToInt32(Request.QueryString("counter")), "C", StatusClass.InProcess)             'GetCancelletionInt(Request.QueryString("counter").ToString())
''            grd_Pax.DataSource = GridDS
''            grd_Pax.DataBind()
''            ViewState("GridDS") = GridDS
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)

''        End Try
''    End Sub

''    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Update.Click
''        Try
''            For Each row As GridViewRow In grd_Pax.Rows
''                Dim lblTktNo As String = DirectCast(row.FindControl("lbltktno"), Label).Text
''                Dim lblfare As String = DirectCast(row.FindControl("lbltotalfareafterdiscount"), Label).Text
''                Dim lbluser As String = DirectCast(row.FindControl("lbluserid"), Label).Text
''                Dim lblSector As String = DirectCast(row.FindControl("lblSector"), Label).Text
''                Dim lblpnr As String = DirectCast(row.FindControl("lblpnr"), Label).Text
''                Dim lblpaxfname As String = DirectCast(row.FindControl("lblpaxfname"), Label).Text
''                Dim lblagencyname As String = DirectCast(row.FindControl("lblagencyname"), Label).Text
''                Dim lblorderId As String = DirectCast(row.FindControl("lblorderId"), Label).Text
''                Dim TDS As String = DirectCast(row.FindControl("lblTDS"), Label).Text
''                Dim BookingDate As String = DirectCast(row.FindControl("lblBookingDate"), Label).Text
''                Dim lblPaxID As Integer = Convert.ToInt32(DirectCast(row.FindControl("lblPaxID"), Label).Text)

''                Dim mgtFee As String = DirectCast(row.FindControl("HiddenMgtFee"), HiddenField).Value
''                Dim mgtFeeVal As Double = If(String.IsNullOrEmpty(mgtFee), 0, Convert.ToDouble(mgtFee))
''                Dim ServiceTax As Double = 0
''                If (mgtFeeVal > 0) Then
''                    Dim SrvTax As String = DirectCast(row.FindControl("HiddenSrvTax"), HiddenField).Value
''                    ServiceTax = If(String.IsNullOrEmpty(SrvTax), 0, Convert.ToDouble(SrvTax))
''                End If
''                Dim FareDiff As Double = 0
''                Dim TotlCharge As Double = 0




''                Dim Status As Boolean = ST.CheckRefundReissueUpdate(lblPaxID, StatusClass.Cancelled.ToString(), "REFUND")
''                If (Status = 0) Then
''                    Dim HdrDs As New DataSet()
''                    HdrDs = ST.GetHdrDetails(lblorderId)
''                    If HdrDs.Tables(0).Rows.Count <> 0 Then
''                        'Checking Its Allready ReIssued or not
''                        'If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

''                        If HdrDs.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso HdrDs.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
''                            FareDiff = Convert.ToDouble(HdrDs.Tables(0).Rows(0)("ResuFareDiff"))
''                        End If
''                    End If

''                    'Checking Booking Month and Today Month is not Same then TDS charge will be deducted 
''                    Dim m1 As String = DatePart(DateInterval.Month, Date.Now)
''                    Dim m() As String = BookingDate.Split("/")
''                    Dim m2 As String = m(0)
''                    If m1 = m2 Then
''                        TotlCharge = Convert.ToDouble(txt_charge.Text.Trim) + Convert.ToDouble(txt_Service.Text.Trim) + mgtFeeVal + ServiceTax - FareDiff
''                        Refundfare = Convert.ToDouble(lblfare) - TotlCharge
''                    Else
''                        TotlCharge = Convert.ToDouble(txt_charge.Text.Trim) + Convert.ToDouble(txt_Service.Text.Trim) + Convert.ToDouble(TDS) + mgtFeeVal + ServiceTax - FareDiff
''                        Refundfare = Convert.ToDouble(lblfare) - TotlCharge
''                    End If

''                    'Adding Refund Amount in Agent balance
''                    Dim ablBalance As Double = ST.UpdateNew_RegsRefund(lbluser, Refundfare)
''                    Session("CL") = ablBalance
''                    Dim rm As String = "ReFund Charge Against Ticket No " & lblTktNo

''                    'Insert data in Transaction Report
''                    ST.InsertTransactionRepot(lblpnr, lbluser, Refundfare.ToString, ablBalance.ToString, TotlCharge.ToString, lblSector, lblpaxfname, "", lblagencyname, rm, StatusClass.Cancelled)

''                    ' Insert Data in LedgerDetails Table
''                    STDom.insertLedgerDetails(lbluser, lblagencyname, lblorderId, lblpnr, lblTktNo, "", "", "", Session("UID").ToString, Request.UserHostAddress, 0, _
''                                        Convert.ToDecimal(Refundfare), Convert.ToDecimal(ablBalance), "Ticket Refund", rm, 0)

''                    ' Update Cancellationintl table after Refund 
''                    ST.UpdateCancelIntlTbl(Convert.ToInt32(Request.QueryString("counter")), Convert.ToInt32(txt_charge.Text), Convert.ToInt32(txt_Service.Text), Convert.ToInt32(Refundfare), txtRemark.Text, StatusClass.Cancelled)

''                    'Update Status =Cancelled in FltHeader and FltPaxDetails 
''                    ST.UpdateStatus_Pax_Header(lblorderId, lblpnr, lblPaxID, StatusClass.Cancelled)

''                    'NAV METHOD  CALL START
''                    Try

''                        'Dim objNav As New AirService.clsConnection(lblorderId, "2", lblPaxID)
''                        'objNav.airBookingNav(lblorderId, "", 2)

''                    Catch ex As Exception

''                    End Try
''                    'Nav METHOD END'



''                    'Online Yatra
''                    Try
''                        'Dim AirObj As New AIR_YATRA
''                        'AirObj.ProcessYatra_Air_CN(lblorderId, lblpnr, Convert.ToInt32(lblPaxID))
''                    Catch ex As Exception

''                    End Try
''                    'Online Yatra End

''                    BindGrid()
''                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Refund Sucessfull');javascript:window.close();window.location='ProcessImportPnr.aspx';", True)

''                    'Response.Write("<script>javascript:window.close();</script>")

''                Else

''                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Ticket Already Refunded');javascript:window.close();window.location='ProcessImportPnr.aspx';", True)

''                End If






''            Next
''        Catch ex As Exception
''            clsErrorLog.LogInfo(ex)
''            lblmessage.Text = ex.Message
''            Throw
''        End Try

''    End Sub

''    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
''        Response.Write("<script>javascript:window.close();</script>")
''    End Sub
''End Class
