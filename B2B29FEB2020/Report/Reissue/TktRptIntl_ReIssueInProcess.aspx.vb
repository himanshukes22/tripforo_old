Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Reports_Reissue_TktRptIntl_ReIssueInProcess
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Dim ds As New DataSet
    Private counter As String
    Dim ids As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If (IsPostBack) Then
            Else
                BindGrid()
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid()
        Try
            ds = ST.ReIssueRefundInProcessGrd(Session("UID").ToString, "R", StatusClass.InProcess, "I")
            ReIssueInProcessGRD.DataSource = ds
            ReIssueInProcessGRD.DataBind()
            ViewState("ds") = ds
            ReIssueInProcessGRD.Columns(17).Visible = False
            ReIssueInProcessGRD.Columns(18).Visible = False
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub ReIssueInProcessGRD_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ReIssueInProcessGRD.RowCommand
        If e.CommandName = "reject" Then
            Try
                ViewState("counter") = Integer.Parse(e.CommandArgument)
                ReIssueInProcessGRD.Columns(17).Visible = True
                ReIssueInProcessGRD.Columns(18).Visible = True
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                ViewState("RowIndex") = RowIndex
                Dim txtRemark As TextBox = DirectCast(ReIssueInProcessGRD.Rows(RowIndex).FindControl("txtRemark"), TextBox)
                Dim lnkSubmit As LinkButton = DirectCast(ReIssueInProcessGRD.Rows(RowIndex).FindControl("lnkSubmit"), LinkButton)
                Dim lnkHides As LinkButton = DirectCast(ReIssueInProcessGRD.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
                lnkHides.Visible = True
                txtRemark.Visible = True
                lnkSubmit.Visible = True
                gvr.BackColor = System.Drawing.Color.Yellow
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If
        If e.CommandName = "lnkupdate" Then
            Try
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.GreenYellow
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If
    End Sub

    Protected Sub lnkupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnkupdate As String = CType(sender, LinkButton).CommandArgument.ToString
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "window.open('TktRptIntl_ReIssueUpdate.aspx?Counter=" & lnkupdate & "','Print','scrollbars=yes,width=920,height=410,top=20,left=150');", True)
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btnCanFee_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
        Dim txtRemark As TextBox = DirectCast(ReIssueInProcessGRD.Rows(RowIndex).FindControl("txtRemark"), TextBox)
        Try
            'Update Status and Remark in ReIssueIntl Table after Reject 
            Dim i As Integer = ST.UpdateReIssueCancelReject(Convert.ToInt32(ViewState("counter")), Session("UID").ToString, txtRemark.Text, "R", StatusClass.InProcess)
            If i = 1 Then
                ShowAlertMessage("Reject succesfully")
                'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
            Else
                ShowAlertMessage("This ticket Number has Allready ReIssued")
                'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This ticket No has Allready ReIssued'); ", True)
            End If

            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnkHides_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'ReIssueInProcessGRD.Columns(17).Visible = False
            'ReIssueInProcessGRD.Columns(18).Visible = False
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

    'Protected Sub ReIssueInProcessGRD_RowDataBound(sender As Object, e As GridViewRowEventArgs)
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
