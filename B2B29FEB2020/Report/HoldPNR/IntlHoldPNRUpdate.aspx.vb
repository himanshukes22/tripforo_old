﻿Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports ITZLib

Partial Class Reports_HoldPNR_IntlHoldPNRUpdate
    Inherits System.Web.UI.Page
    Private ID As New IntlDetails
    Private ST As New SqlTransaction()
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As SqlDataAdapter
    Private STDom As New SqlTransactionDom()

    Dim objRefnResp As New RefundResponse
    Dim objParamCrd As New _CrOrDb
    Dim objCrd As New ITZcrdb
    Dim objItzBal As New ITZGetbalance
    Dim objParamBal As New _GetBalance
    Dim objBalResp As New GetBalanceResponse


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub BindGrid()
        Try
            'Dim OrderId As String = Request.QueryString("ProxyID")
            GridHoldPNRAccept.DataSource = ID.IntlUpdateHoldPNR("InProcess", "I", Session("UID"))
            GridHoldPNRAccept.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridHoldPNRAccept_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridHoldPNRAccept.RowCommand
        Try
            If e.CommandName = "Reject" Then

                Dim Orderid As String = e.CommandArgument.ToString()
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.Yellow

                td_Reject.Visible = True
                Dim ID As String = e.CommandArgument.ToString()
                ViewState("ID") = ID
                'End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub ITZ_Accept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim OrderId = CType(sender, LinkButton).CommandArgument.ToString
            'Response.Write("<script>javascript:window.open('UpdateHoldPNRIntl.aspx?OrderId=" & OrderId & "','Print','scrollbars=yes,width=800,height=500,top=20,left=150')</script>")
            Dim popupScript As String = "window.open('UpdateHoldPNRIntl.aspx?OrderId=" & OrderId & "','Print','scrollbars=yes,width=800,height=500,top=20,left=150');"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "popup", popupScript, True)

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        td_Reject.Visible = False

    End Sub

    Protected Sub btn_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Comment.Click
        Try
            Dim OrderID As String = ""
            OrderID = ViewState("ID").ToString()
            Dim objItzT As New Itz_Trans_Dal
            Dim inst As Boolean = False
            Dim objIzT As New ITZ_Trans
            Dim ablBalance As Double = 0

            If txt_Reject.Text IsNot Nothing AndAlso txt_Reject.Text <> "" Then
                Dim ds As New DataSet()
                ds = ST.GetFltHeaderDetail(OrderID)
                Dim dtID As New DataTable()
                dtID = ds.Tables(0)
                'adap = New SqlDataAdapter("Select GdsPnr,AgencyName,AgentId,TotalAfterDis From FltHeader where OrderId='" & OrderID & "'", con)
                'adap.Fill(dtID)

                Dim Aval_Bal As Double = ST.AddCrdLimit(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("TotalAfterDis").ToString())
                ''ST.RejectHoldPNRStatusIntl(OrderID, Session("UID").ToString(), "Rejected", txt_Reject.Text.Trim(), dtID.Rows(0)("GdsPnr").ToString(), "Rejected", dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Intl PNR Rejected Against  OrderID=" & OrderID, dtID.Rows(0)("AgencyName").ToString(), dtID.Rows(0)("AgentId").ToString())
                GetMerchantKey(OrderID)
                Dim rndnum As New RandomKeyGenerator()

                Dim numRand As String = rndnum.Generate()
                Try
                    objParamCrd._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    objParamCrd._AMOUNT = IIf(dtID.Rows(0)("TotalAfterDis").ToString() <> Nothing, dtID.Rows(0)("TotalAfterDis").ToString(), 0)
                    objParamCrd._ORDERID = IIf(numRand <> Nothing AndAlso numRand <> "", numRand.Trim(), " ")
                    objParamCrd._REFUNDORDERID = IIf(OrderID <> Nothing AndAlso OrderID <> "", OrderID.Trim(), " ")
                    objParamCrd._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                    objParamCrd._REFUNDTYPE = "F"
                    ''objParamCrd._CHECKSUM = " "
                    Dim stringtoenc As String = "MERCHANTKEY=" & objParamCrd._MERCHANT_KEY & "&ORDERID=" & objParamCrd._ORDERID & "&REFUNDTYPE=" & objParamCrd._REFUNDTYPE
                    objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                    'objParamCrd._SERVICE_TYPE = IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                    objParamCrd._DESCRIPTION = "refund to agent -" & dtID.Rows(0)("AgentId").ToString() & " against PNR-" & dtID.Rows(0)("GdsPnr").ToString()
                    objRefnResp = objCrd.ITZRefund(objParamCrd)

                    If objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then
                        ST.RejectHoldPNRStatusIntl(OrderID, Session("UID").ToString(), "Rejected", txt_Reject.Text.Trim(), dtID.Rows(0)("GdsPnr").ToString(), "Rejected", dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Intl PNR Rejected Against  OrderID=" & OrderID, dtID.Rows(0)("AgencyName").ToString(), dtID.Rows(0)("AgentId").ToString())
                        STDom.insertLedgerDetails(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("AgencyName").ToString(), OrderID, dtID.Rows(0)("GdsPnr").ToString(), "", "", "", "", Session("UID").ToString(), Request.UserHostAddress, 0, dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Intl Rejection", "Intl PNR Rejected Against  OrderID=" & OrderID, 0)


                        Try
                            objItzT = New Itz_Trans_Dal()
                            objIzT.AMT_TO_DED = "0"
                            objIzT.AMT_TO_CRE = IIf(dtID.Rows(0)("TotalAfterDis") <> Nothing, dtID.Rows(0)("TotalAfterDis").ToString(), 0)
                            objIzT.B2C_MBLNO_ITZ = " "
                            objIzT.COMMI_ITZ = " "
                            objIzT.CONVFEE_ITZ = " "
                            objIzT.DECODE_ITZ = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
                            objIzT.EASY_ORDID_ITZ = IIf(objRefnResp.EASY_ORDER_ID IsNot Nothing, objRefnResp.EASY_ORDER_ID, " ")
                            objIzT.EASY_TRANCODE_ITZ = IIf(objRefnResp.EASY_TRAN_CODE IsNot Nothing, objRefnResp.EASY_TRAN_CODE, " ")
                            objIzT.ERROR_CODE = IIf(objRefnResp.ERROR_CODE IsNot Nothing, objRefnResp.ERROR_CODE, " ")
                            objIzT.MERCHANT_KEY_ITZ = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                            objIzT.MESSAGE_ITZ = IIf(objRefnResp.MESSAGE IsNot Nothing, objRefnResp.MESSAGE, " ")
                            objIzT.ORDERID = IIf(OrderID <> Nothing AndAlso OrderID <> "", OrderID.Trim(), " ")
                            objIzT.RATE_GROUP_ITZ = " "
                            objIzT.REFUND_TYPE_ITZ = IIf(objRefnResp.REFUND_TYPE IsNot Nothing AndAlso objRefnResp.REFUND_TYPE <> "" AndAlso objRefnResp.REFUND_TYPE <> " ", objRefnResp.REFUND_TYPE, " ")
                            objIzT.SERIAL_NO_FROM = " "
                            objIzT.SERIAL_NO_TO = " "
                            objIzT.SVC_TAX_ITZ = " "
                            objIzT.TDS_ITZ = " "
                            objIzT.TOTAL_AMT_DED_ITZ = " "
                            objIzT.TRANS_TYPE = "REFUND"
                            objIzT.USER_NAME_ITZ = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
                            Try
                                objBalResp = New GetBalanceResponse()
                                objParamBal._DCODE = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
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


                        'Ledger


                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('PNR Rejected Sucessfully');", True)

                        BindGrid()
                        td_Reject.Visible = False


                    Else
                        Response.Write("<script>alert('Unable to  Refund, Please try again')</script>")

                    End If
                Catch ex As Exception
                End Try

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridHoldPNRAccept_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridHoldPNRAccept.PageIndexChanging
        Try
            GridHoldPNRAccept.PageIndex = e.NewPageIndex
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

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

    'Protected Sub GridHoldPNRAccept_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Dim lblpaymentmode As Label
    '    Dim lblPGCharges As Label

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        lblpaymentmode = e.Row.FindControl("lbl_PaymentMode")
    '        lblPGCharges = e.Row.FindControl("lbl_Charges")
    '        If lblpaymentmode.Text = "" Or lblpaymentmode.Text Is Nothing Then
    '            lblpaymentmode.Text = "Wallet"
    '        End If
    '        If lblPGCharges.Text = "" Or lblPGCharges.Text Is Nothing Then
    '            lblPGCharges.Text = "0.0000"
    '        End If
    '        lblpaymentmode.Text = lblpaymentmode.Text.ToUpper()
    '        lblPGCharges.Text = lblPGCharges.Text.Substring(0, lblPGCharges.Text.Length - 2)
    '    End If
    'End Sub
End Class
