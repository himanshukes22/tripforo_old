Imports System.Data
Imports ITZLib
Imports System.Configuration.ConfigurationManager
Partial Class SprReports_Hotel_HoldHotelAccept
    Inherits System.Web.UI.Page

    Private SqlTrans As New HotelDAL.HotelDA()
    Private STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "EXEC" Then
                    BindGrid()
                End If
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub btnHoldReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoldReject.Click
        Dim STDom As New SqlTransactionDom()
        Dim SqlT As New SqlTransaction()
        Dim HoldGrdds As New DataSet()
        Try
            HoldGrdds = Session("HoldHotelInProcessDS")
            For Each strow In HoldGrdds.Tables(0).Rows
                If strow.Item("OrderID").ToString() = Request("OrderIDS").Trim() Then
                    Dim Refundfare As Decimal = Convert.ToDecimal(strow.Item("NetCost"))
                    'Update Header table  After Reject
                    Dim i As Integer = SqlTrans.RejectHoldBooking(StatusClass.Rejected.ToString(), Request("OrderIDS").ToString(), Refundfare, Session("UID").ToString(), Request("txtRemark").ToString())
                    If i > 0 Then

                        If HoldGrdds IsNot Nothing Then
                            If HoldGrdds.Tables.Count > 0 Then
                                If HoldGrdds.Tables(0).Rows.Count > 0 Then
                                    RefundAmountAndLadgerEntry(Request("OrderIDS").ToString(), Refundfare, strow.Item("AgentID").ToString(), "", HoldGrdds.Tables(0).Rows(0)("Provider"), HoldGrdds.Tables(0).Rows(0)("HotelName"), strow.Item("AgencyName").ToString(), HoldGrdds.Tables(0).Rows(0)("TripType"))
                                    BindGrid()
                                    Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject Successfully." & "); ", True)
                                End If
                            End If
                        End If

                        ''Adding Refund Amount in Agent balance
                        'Dim ablBalance As Double = SqlT.UpdateNew_RegsRefund(strow.Item("AgentID"), Refundfare)
                        '' Insert Data in LedgerDetails Table
                        'STDom.insertLedgerDetails(strow.Item("AgentID").ToString(), strow.Item("AgencyName").ToString(), Request("OrderIDS").ToString(), "", "", "", "", "", Session("UID").ToString(), Request.UserHostAddress, 0, _
                        '                    Refundfare, Convert.ToDecimal(ablBalance), "Hotel Refund", "Rejected Hold Hotel Booking.", 0)
                        'Send Mail to Agent
                        'Dim HtlLogObj As New HtlLibrary.HtlLog
                        'HtlLogObj.SendEmailForHold_Reject("", "", "", strow.Item("AgentID").ToString())


                        ' Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject Successfully." & "); ", True)
                        Exit For
                    Else
                        Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Try later." & "); ", True)
                    End If
                End If
            Next
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btnHoldUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoldUpdate.Click
        Try
            Dim i As Integer = SqlTrans.UpdateHrdHold(Request("OrderIDS").Trim(), Request("txtHtlBID").Trim(), Request("txtHtlConfNo").Trim(), StatusClass.Confirm.ToString(), Session("UID").ToString(), Request("txtRemark"))
            If i > 0 Then
                Dim HoldGrdds As New DataSet()
                HoldGrdds = Session("HoldHotelInProcessDS")
                For Each strow In HoldGrdds.Tables(0).Rows
                    If strow.Item("OrderID").ToString() = Request("OrderIDS").Trim() Then
                        'Send Mail to Agent
                        Dim HtlLogObj As New HotelBAL.HotelSendMail_Log()
                        '' HtlLogObj.SendEmailForHold_Reject(Request("OrderIDS").Trim(), strow.Item("HotelName").ToString(), Request("txtHtlBID").Trim(), strow.Item("AgentID").ToString())

                        BindGrid()
                    End If
                Next
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Update succesfully'); ", True)
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim TripType As String = ""
            If Session("TripExec").ToString() = "D" Then
                TripType = "Domestic"
            ElseIf Session("ExecTrip").ToString() = "I" Then
                TripType = "International"
            End If
            Dim HoldHotelInProcessDS As New DataSet()
            HoldHotelInProcessDS = SqlTrans.GetHoldHotel(Session("UID").ToString(), StatusClass.InProcess.ToString(), TripType)
            HoldHotelAcceptGrd.DataSource = HoldHotelInProcessDS
            HoldHotelAcceptGrd.DataBind()
            Session("HoldHotelInProcessDS") = HoldHotelInProcessDS
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
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
                                     Request.UserHostAddress.ToString(), "", "", objIzT.EASY_ORDID_ITZ, "Hold Hotel Reject against " + orderid, TripType.Substring(0, 1), 0)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return ablBalance
    End Function

End Class
