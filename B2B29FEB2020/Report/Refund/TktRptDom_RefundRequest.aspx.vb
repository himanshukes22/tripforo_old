Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Refund_TktRptDom_RefundRequest
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Dim ds As New DataSet
    Dim filterArray As Array
    Dim PaxType As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If IsPostBack Then
            Else
                BindGrid()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid()
        Try
            ds = ST.RefundReIssueRequestGrd("C", StatusClass.Pending, "D")
            Acept_grdview.DataSource = ds
            Acept_grdview.DataBind()
            ViewState("ds") = ds
            Acept_grdview.Columns(18).Visible = False
            Acept_grdview.Columns(19).Visible = False
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Acept_grdview.RowCommand
        Try
            If e.CommandName = "Accept" Then
                Try
                    'Update Status in CancellationIntl Table after Accept
                    ST.UpdateReIssueCancleAccept(Integer.Parse(e.CommandArgument), Session("UID").ToString, "C", StatusClass.InProcess)
                    Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                    Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                    gvr.BackColor = System.Drawing.Color.GreenYellow
                    BindGrid()
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)

                End Try
            End If
            If e.CommandName = "Reject" Then
                Try
                    ViewState("Counter") = Integer.Parse(e.CommandArgument)
                    Acept_grdview.Columns(18).Visible = True
                    Acept_grdview.Columns(19).Visible = True
                    Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                    Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                    Dim RowIndex As Integer = gvr.RowIndex
                    ViewState("RowIndex") = RowIndex
                    Dim txtRemark As TextBox = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
                    Dim lnkSubmit As LinkButton = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("lnkSubmit"), LinkButton)
                    Dim lnkHides As LinkButton = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
                    lnkHides.Visible = True
                    txtRemark.Visible = True
                    lnkSubmit.Visible = True
                    gvr.BackColor = System.Drawing.Color.Yellow
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)

                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Protected Sub btnCanFee_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
    '        Dim txtRemark As TextBox = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '        'Update Status and Remark in CancellationIntl Table after Reject
    '        ST.UpdateReIssueCancelReject(Convert.ToInt32(ViewState("Counter")), Session("UID").ToString, txtRemark.Text, "C", StatusClass.Pending)
    '        'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
    '        ShowAlertMessage("Reject succesfully")
    '        BindGrid()
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub

    Protected Sub btnCanFee_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim paxname As String
            Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
            Dim txtRemark As TextBox = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
            Dim OrderId As Label = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("OrderId"), Label)
            Dim PnrNo As Label = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("GdsPNR"), Label)
            Dim Pax As Label = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("PaxType"), Label)
            Dim NetAmount As Label = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("lbltotalfare"), Label)
            Dim dtpaxname As New DataTable()
            Dim ObjIntDetails As New IntlDetails()
            dtpaxname = ObjIntDetails.PaxName_OrderID(OrderId.Text.Trim().ToString(), "C_REJECTED", ViewState("Counter"))
            paxname = dtpaxname.Rows(0)(0).ToString()
            'Update Status and Remark in CancellationIntl Table after Reject
            ST.UpdateReIssueCancelReject(Convert.ToInt32(ViewState("Counter")), Session("UID").ToString, txtRemark.Text, "C", StatusClass.Pending)
            'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
            Dim dt As New DataTable()
            ' Dim ToEmail As String
            ' Dim TKTCopyPath As String
            Dim strMailMsgHold As String
            strMailMsgHold = "<table>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><h2> Dom Refund Request Booking Summary </h2>"
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OrderId.Text
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>PnrNo: </b>" + PnrNo.Text
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Pax Name: </b>" + paxname
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Pax Type: </b>" + Pax.Text
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Net Amount: </b>" + NetAmount.Text
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Remark: </b>" + txtRemark.Text
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "</table>"

            dt = ObjIntDetails.Email_Credentilas(OrderId.Text.Trim().ToString(), "C_REJECTED", ViewState("Counter"))
            Dim STDOM As New SqlTransactionDom
            Dim MailDt As New DataTable
            MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

            Try
                If (MailDt.Rows.Count > 0) Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        STDOM.SendMail(dt.Rows(j)(1).ToString(), "info@RWT.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Reissue Request", "")
                    Next
                End If
            Catch ex As Exception

            End Try
            'For i As Integer = 0 To dt.Rows.Count - 1
            '    ToEmail += "; " + dt.Rows(i)(1).ToString()
            'Next
            'ToEmail = ToEmail.Substring(2)
            'Dim STDOM As New SqlTransactionDom
            'Dim MailDt As New DataTable
            'MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

            'Try
            '    If (MailDt.Rows.Count > 0) Then
            '        TKTCopyPath = Session("strFileNmPdf")
            '        STDOM.SendMail(ToEmail, " info@RWT.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Refund Request", TKTCopyPath)
            '    End If
            'Catch ex As Exception

            'End Try
            ShowAlertMessage("Reject succesfully")
            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnkHides_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Acept_grdview.Columns(18).Visible = False
            Acept_grdview.Columns(19).Visible = False
            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

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

    'Protected Sub Acept_grdview_RowDataBound(sender As Object, e As GridViewRowEventArgs)
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
