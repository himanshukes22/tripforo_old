Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports ITZLib
Imports System.Configuration.ConfigurationManager
Partial Class HtlRefundUpdate
    Inherits System.Web.UI.Page
    Private SqlT As New SqlTransaction()
    Private ST As New HotelDAL.HotelDA()
    Private STDom As New SqlTransactionDom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            End If
            If Not IsPostBack Then
                Dim GrdDS, AgencyDS As New DataSet
                GrdDS = BindGrid()
                If GrdDS.Tables(0).Rows.Count > 0 Then
                    AgencyDS = ST.GetAgencyDetails(GrdDS.Tables(0).Rows(0).Item("BookedBy").ToString.Trim())
                    If AgencyDS.Tables(0).Rows.Count > 0 Then
                        Dim dt As New DataTable()
                        dt = AgencyDS.Tables(0)
                        Dim addr As String = dt.Rows(0)("city").ToString() & ", " & dt.Rows(0)("State").ToString() & ", " & dt.Rows(0)("country").ToString() & ", " & dt.Rows(0)("zipcode").ToString()
                        td_AgentID.InnerText = GrdDS.Tables(0).Rows(0).Item("BookedBy").ToString()
                        td_AgentName.InnerText = dt.Rows(0)("Name").ToString()
                        td_AgentAddress.InnerText = dt.Rows(0)("Address").ToString() & ", " & addr
                        td_AgentMobNo.InnerText = dt.Rows(0)("Mobile").ToString()
                        td_Email.InnerText = dt.Rows(0)("Email").ToString()
                        td_AgencyName.InnerText = dt.Rows(0)("Agency_Name").ToString()
                        td_CardLimit.InnerText = dt.Rows(0)("Crd_Limit").ToString()
                    End If
                    lblCancelPoli.Text = GrdDS.Tables(0).Rows(0)("CancallationPolicy")
                Else
                    Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Hotel already cancelled." & "); ", True)
                    Response.Write("<script>javascript:window.close();</script>")
                End If
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Private Function BindGrid() As DataSet
        Dim GrdDS As New DataSet
        Try
            GrdDS = ST.HtlRefundDetail(StatusClass.InProcess.ToString(), Request.QueryString("OrderID"), Session("UID").ToString, "UpdeteGET", "")
            grd_HtlUpdate.DataSource = GrdDS
            grd_HtlUpdate.DataBind()
            Session("htlRfndDs") = GrdDS
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return GrdDS
    End Function

    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Update.Click
        Try
            For Each row As GridViewRow In grd_HtlUpdate.Rows
                Dim orderId As String = DirectCast(row.FindControl("lblOrderId"), Label).Text
                Dim lblBID As String = DirectCast(row.FindControl("lblBID"), Label).Text
                Dim lblNetCost As Decimal = Convert.ToDecimal(DirectCast(row.FindControl("lblNetCost"), Label).Text)
                Dim CancelCharge As Decimal = Convert.ToDecimal(Request("txtRefundCharge").Trim())
                Dim ServiceCharge As Integer = Convert.ToInt32(Request("txtServiceCharge").Trim())
                Dim Refundfare As Decimal = Convert.ToDecimal(lblNetCost) - (CancelCharge + ServiceCharge)
                Dim GrdDS As New DataSet()
                GrdDS = Session("htlRfndDs")
                If GrdDS IsNot Nothing Then
                    If GrdDS.Tables.Count > 0 Then
                        If GrdDS.Tables(0).Rows.Count > 0 Then
                            RefundAmountAndLadgerEntry(orderId, Refundfare, td_AgentID.InnerText, lblBID, GrdDS.Tables(0).Rows(0)("Provider"), GrdDS.Tables(0).Rows(0)("HotelName"), td_AgencyName.InnerText, GrdDS.Tables(0).Rows(0)("TripType"))
                            Dim i As Integer = ST.UpdateHltRefund(StatusClass.Cancelled.ToString(), orderId, Refundfare, CancelCharge, ServiceCharge, Request("txtRemark").Trim)

                            Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Refund Successfully." & "); ", True)
                        End If
                    End If
                End If
                'Adding Refund Amount in Agent balance
                'Dim ablBalance As Double = SqlT.UpdateNew_RegsRefund(td_AgentID.InnerText, Refundfare)
                '' Insert Data in LedgerDetails Table
                'STDom.insertLedgerDetails(td_AgentID.InnerText, td_AgencyName.InnerText, orderId, lblBID, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, 0, _
                '                    Refundfare, Convert.ToDecimal(ablBalance), "Hotel Refund", "Hotel Cancellation for " & orderId, 0)

              
            Next
            BindGrid()
            Response.Write("<script>javascript:window.close();</script>")
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Write("<script>javascript:window.close();</script>")
    End Sub
    Protected Function RefundAmountAndLadgerEntry(ByVal orderid As String, ByVal RefundAmount As Decimal, ByVal AgentID As String, ByVal BookingID As String, ByVal Provider As String, ByVal HotelName As String, ByVal AgencyName As String, ByVal TripType As String) As Double
        Dim objItzT As New Itz_Trans_Dal
        Dim inst As Boolean = False
        Dim objIzT As New ITZ_Trans
        Dim ablBalance As Double = 0
        Dim objRefnResp As New RefundResponse
        Dim objParamCrd As New _CrOrDb
        Dim objCrd As New ITZcrdb
        Dim objItzBal As New ITZGetbalance
        Dim objParamBal As New _GetBalance
        Dim objBalResp As New GetBalanceResponse
        Dim rndnum As New RandomKeyGenerator()
        Dim numRand As String = rndnum.Generate()
        Try
            objParamCrd._MERCHANT_KEY = IIf(AppSettings("HotelITZMerchantKey") <> Nothing, AppSettings("HotelITZMerchantKey"), " ")
            objParamCrd._AMOUNT = IIf(RefundAmount.ToString() <> Nothing, RefundAmount.ToString(), 0)
            objParamCrd._ORDERID = IIf(numRand <> Nothing AndAlso numRand <> "", numRand.Trim(), " ")
            objParamCrd._REFUNDORDERID = IIf(orderid <> Nothing AndAlso orderid <> "", orderid, " ")
            objParamCrd._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ")
            objParamCrd._REFUNDTYPE = "F"
            ''objParamCrd._CHECKSUM = " "
            Dim stringtoenc As String = "MERCHANTKEY=" & objParamCrd._MERCHANT_KEY & "&ORDERID=" & objParamCrd._ORDERID & "&REFUNDTYPE=" & objParamCrd._REFUNDTYPE
            objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
            objParamCrd._DESCRIPTION = "refund to agent -" & AgentID & " against PNR-" & BookingID
            objRefnResp = objCrd.ITZRefund(objParamCrd)
        Catch ex As Exception
        End Try
        objItzT = New Itz_Trans_Dal()
        Try
            objIzT.AMT_TO_DED = "0"
            objIzT.AMT_TO_CRE = IIf(RefundAmount.ToString() <> Nothing, RefundAmount.ToString(), 0)
            objIzT.B2C_MBLNO_ITZ = " "
            objIzT.COMMI_ITZ = " "
            objIzT.CONVFEE_ITZ = " "
            objIzT.DECODE_ITZ = IIf(AgentID <> Nothing, AgentID, " ")
            objIzT.EASY_ORDID_ITZ = IIf(objRefnResp.EASY_ORDER_ID IsNot Nothing, objRefnResp.EASY_ORDER_ID, " ")
            objIzT.EASY_TRANCODE_ITZ = IIf(objRefnResp.EASY_TRAN_CODE IsNot Nothing, objRefnResp.EASY_TRAN_CODE, " ")
            objIzT.ERROR_CODE = IIf(objRefnResp.ERROR_CODE IsNot Nothing, objRefnResp.ERROR_CODE, " ")
            objIzT.MERCHANT_KEY_ITZ = IIf(AppSettings("HotelITZMerchantKey") <> Nothing, AppSettings("HotelITZMerchantKey"), " ")
            objIzT.MESSAGE_ITZ = IIf(objRefnResp.MESSAGE IsNot Nothing, objRefnResp.MESSAGE, " ")
            objIzT.ORDERID = IIf(orderid <> Nothing AndAlso orderid <> "", orderid, " ")
            objIzT.RATE_GROUP_ITZ = " "
            objIzT.REFUND_TYPE_ITZ = IIf(objRefnResp.REFUND_TYPE IsNot Nothing AndAlso objRefnResp.REFUND_TYPE <> "" AndAlso objRefnResp.REFUND_TYPE <> " ", objRefnResp.REFUND_TYPE, " ")
            objIzT.SERIAL_NO_FROM = " "
            objIzT.SERIAL_NO_TO = " "
            objIzT.SVC_TAX_ITZ = " "
            objIzT.TDS_ITZ = " "
            objIzT.TOTAL_AMT_DED_ITZ = " "
            objIzT.TRANS_TYPE = "HTLRFND"
            objIzT.USER_NAME_ITZ = IIf(AgentID <> Nothing, AgentID, " ")
            Try
                objBalResp = New GetBalanceResponse()
                objParamBal._DCODE = IIf(AgentID <> Nothing, AgentID, " ")
                objParamBal._MERCHANT_KEY = IIf(AppSettings("HotelITZMerchantKey") <> Nothing, AppSettings("HotelITZMerchantKey"), " ")
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

            Dim Result As Integer = STDom.LedgerEntry_Common(orderid, 0, RefundAmount, ablBalance, Provider, HotelName, BookingID, "HTLRFND", AgentID, AgencyName, Session("UID").ToString(), _
                                     Request.UserHostAddress.ToString(), "", "", objIzT.EASY_ORDID_ITZ, "Hotel Refund agence " + orderid, TripType.Substring(0, 1), 0)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return ablBalance
    End Function
End Class

  
