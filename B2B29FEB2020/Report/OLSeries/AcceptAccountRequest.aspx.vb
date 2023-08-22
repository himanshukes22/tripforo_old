Imports System.Data

Partial Class SprReports_OLSeries_AcceptAccountRequest
    Inherits System.Web.UI.Page

    Dim series As New SeriesDepart
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Session("UID") = "" Or Session("UID") Is Nothing Then
            If (Session("User_Type") <> "ACC") Then
                Response.Redirect("~/Login.aspx")
            End If
        End If
        If Not IsPostBack Then
            Try
                PendingBind()
            Catch ex As Exception

            End Try

        End If
    End Sub
    Protected Sub grd_requestAcc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_requestAcc.RowCommand
        Try
            Dim counter As Integer
            Dim btn As Button = TryCast(e.CommandSource, Button)
            Dim gvr As GridViewRow = TryCast(btn.Parent.Parent, GridViewRow)
            If e.CommandName = "accept" Then
                Dim ID As String = Session("UID").ToString()
                counter = Convert.ToInt32(e.CommandArgument.ToString())
                Dim i As Integer = series.UpdatePendingSeriesAcc(ID, Request.UserHostAddress, "InProcess", counter)
                If i <> 0 Then
                    divProcess.Visible = True
                    divrequest.Visible = False
                    PendingBind()
                Else
                    'lblmsg.Text = "Already Accepted"
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Already Accepted');", True)

                End If
            ElseIf e.CommandName = "reject" Then
                counter = Convert.ToInt32(e.CommandArgument.ToString())
                ViewState("Ctr") = counter
                rejectTD.Visible = True
                'gvr.BackColor = Drawing.Color.Aqua
            End If
            PendingBind()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub PendingBind()
        Try
            Dim dt As New DataTable
            dt = series.GetProcessSeries("SeriesRequest", Session("UID").ToString())
            grd_requestAcc.DataSource = dt
            grd_requestAcc.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ProcessBind()
        Try
            Dim dt As New DataTable
            dt = series.GetPendingAccountRequest("SeriesAccounting", Session("UID").ToString())
            grd_processAcc.DataSource = dt
            grd_processAcc.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grd_requestAcc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_requestAcc.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btn As Button = DirectCast(e.Row.FindControl("btn_reject"), Button)
                btn.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to Reject')")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            Dim counter As Integer = Convert.ToInt32(ViewState("Ctr").ToString())
            If txt_rejectRmk.Text <> "" AndAlso txt_rejectRmk.Text IsNot Nothing Then
                Dim ID As String = Session("UID").ToString()
                Dim i As Integer = series.UpdatePendingSeriesAcc(ID, Request.UserHostAddress, "Rejected", counter, txt_rejectRmk.Text.Trim)

                'lblmsg.Text = "Rejected Successfully"
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Successfully');", True)
                rejectTD.Visible = False
                PendingBind()
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Fill Remark ');", True)
            End If
        Catch ex As Exception

        End Try

       
    End Sub

  

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            rejectTD.Visible = False
            PendingBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkviewProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkviewProcess.Click
        Try
            ProcessBind()
            divProcess.Visible = True
            divrequest.Visible = False
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Try
            PendingBind()
            divProcess.Visible = False
            divrequest.Visible = True
        Catch ex As Exception

        End Try

    End Sub
End Class
