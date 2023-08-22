Imports System.Data
Partial Class SprReports_OLSeries_AcceptRequestSeriesDepart
    Inherits System.Web.UI.Page

    Dim series As New SeriesDepart
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            If (Session("User_Type") <> "EXEC") Then
                Response.Redirect("~/Login.aspx")
            End If
        End If


        If Not IsPostBack Then
            Try
                Pendingbind()
                'rejectTD.Visible = False
                divProcess.Visible = False
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub grd_requestseries_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_requestseries.RowCommand
        Try
            Dim counter As Integer
            Dim btn As Button = TryCast(e.CommandSource, Button)
            Dim gvr As GridViewRow = TryCast(btn.Parent.Parent, GridViewRow)
            If e.CommandName = "accept" Then
                Dim ID As String = Session("UID").ToString()
                counter = Convert.ToInt32(e.CommandArgument.ToString())
                Dim i As Integer = series.UpdateSeriesPendingRequest(ID, Request.UserHostAddress, "InProcess", counter, "")
                If i <> 0 Then

                    'lblmsg.Text = "Request Accepted Successfully"
                Else
                    'lblmsg.Text = "Already Accepted"
                End If
            ElseIf e.CommandName = "reject" Then
                counter = Convert.ToInt32(e.CommandArgument.ToString())
                ViewState("Ctr") = counter
                rejectTD.Visible = True
                Dim lbrequest As Button = TryCast(e.CommandSource, Button)
                Dim gvrrequest As GridViewRow = TryCast(lbrequest.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                Dim totpaxreq As Label = DirectCast(grd_requestseries.Rows(RowIndex).FindControl("lbltotpaxre"), Label)
                Dim lblseriesidreq As Label = DirectCast(grd_requestseries.Rows(RowIndex).FindControl("lblseriesidreq"), Label)

                ViewState("totpaxreq") = totpaxreq.Text
                ViewState("seriesidreq") = lblseriesidreq.Text
                ViewState("RType") = "Request"
            End If
            Pendingbind()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Pendingbind()
        Try
            Dim dtTrip As DataTable
            dtTrip = series.GetExecutiveTripType(Session("UID"))
            Dim dt As New DataTable
            dt = series.GetRequestSeriesDetails("SeriesRequest", dtTrip.Rows(0)("Trip"))
            grd_requestseries.DataSource = dt
            grd_requestseries.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Processbind()
        Try
            Dim dt As New DataTable
            dt = series.GetProcessSeries("SeriesR", Session("UID").ToString())
            grd_processSeries.DataSource = dt
            grd_processSeries.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub grd_requestseries_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_requestseries.RowDataBound
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

                If (ViewState("RType") = "Request") Then
                    series.Update_ConfirmHoldSeat(ViewState("totpaxreq"), ViewState("seriesidreq"), "Reject")
                    Dim i As Integer = series.UpdateSeriesPendingRequest(Session("UID").ToString(), Request.UserHostAddress, "Rejected", counter, txt_rejectRmk.Text)
                    If i = 1 Then

                        '  Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Successfully');", True)
                        ShowAlertMessage("Rejected Successfully")
                    End If
                End If
                If (ViewState("RType") = "Process") Then
                    'ViewState("totpax") = lblseriesid.Text
                    'ViewState("seriesid") = lblseriesid.Text
                    'series.Update_ConfirmHoldSeat(Convert.ToInt32(totpax.Text), Convert.ToInt32(lblseriesid.Text), "Request", 0)
                    series.Update_ConfirmHoldSeat(ViewState("totpax"), ViewState("seriesid"), "Reject")
                    Dim i As Integer = series.UpdateSeriesPendingRequest(Session("UID").ToString(), Request.UserHostAddress, "Rejected", counter, txt_rejectRmk.Text)
                    If i = 1 Then

                        'Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Successfully');", True)
                        ShowAlertMessage("Rejected Successfully")
                    End If
                End If




               
            End If
            rejectTD.Visible = False
            Pendingbind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        rejectTD.Visible = False


    End Sub

    Protected Sub grd_processSeries_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_processSeries.RowCommand
        Try

            If e.CommandName = "modify" Then
                grd_processSeries.Columns(10).Visible = True
                Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                Dim B As Button = DirectCast(rw.FindControl("btn_reject"), Button)
                Dim BTn As Button = DirectCast(rw.FindControl("Button1"), Button)
                Dim BTnCan As Button = DirectCast(rw.FindControl("Button2"), Button)
                Dim BTnreject As Button = DirectCast(rw.FindControl("btnreject"), Button)
                Dim BTnrelease As Button = DirectCast(rw.FindControl("btnrelease"), Button)
                Dim lb As Button = TryCast(e.CommandSource, Button)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex

                Dim txt_updAvlseat As TextBox = DirectCast(grd_processSeries.Rows(RowIndex).FindControl("txt_updAvlseat"), TextBox)

                txt_updAvlseat.Visible = True
                txt_updAvlseat.Focus()

                B.Visible = False
                BTn.Visible = True
                BTnCan.Visible = True
                BTnreject.Visible = False
                BTnrelease.Visible = False
                'Processbind()

            Else
                Dim lb As Button = TryCast(e.CommandSource, Button)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                Dim txt_updAvlseat As TextBox = DirectCast(grd_processSeries.Rows(RowIndex).FindControl("txt_updAvlseat"), TextBox)
                txt_updAvlseat.Visible = False
            End If
            If e.CommandName = "reject1" Then
                Dim counter3 As Integer = 0
                Dim btn As Button = TryCast(e.CommandSource, Button)
                Dim gvr1 As GridViewRow = TryCast(btn.Parent.Parent, GridViewRow)
                counter3 = Convert.ToInt32(e.CommandArgument.ToString())
                ViewState("Ctr") = counter3
                ViewState("RType") = "Process"
                rejectTD.Visible = True
                Dim lb As Button = TryCast(e.CommandSource, Button)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                Dim totpax As Label = DirectCast(grd_processSeries.Rows(RowIndex).FindControl("lbltotpax"), Label)
                Dim lblseriesid As Label = DirectCast(grd_processSeries.Rows(RowIndex).FindControl("lblseriesid"), Label)
                ViewState("totpax") = totpax.Text
                ViewState("seriesid") = lblseriesid.Text

                'Pendingbind()
            End If
            If e.CommandName = "release" Then
                Dim counter As Integer = Convert.ToInt32(e.CommandArgument.ToString())
                series.ReleaseSeriesRequest(Session("UID"), Request.UserHostAddress, "Pending", counter, "")
                'Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Release Successfully');", True)
                ShowAlertMessage("Release Successfully")
                Processbind()
            End If


            If e.CommandName = "Can" Then
                Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                Dim BT2 As Button = DirectCast(rw.FindControl("Button1"), Button)
                Dim BTnCan2 As Button = DirectCast(rw.FindControl("Button2"), Button)
                Dim BTnCan3 As Button = DirectCast(rw.FindControl("btn_reject"), Button)
                Dim BTnreject As Button = DirectCast(rw.FindControl("btnreject"), Button)
                Dim BTnrelease As Button = DirectCast(rw.FindControl("btnrelease"), Button)
                BT2.Visible = False
                BTnCan2.Visible = False
                BTnCan3.Visible = True
                BTnreject.Visible = True
                BTnrelease.Visible = True
                grd_processSeries.Columns(10).Visible = False
            End If
            If e.CommandName = "Change" Then

                'Dim counter As Integer = Convert.ToInt32(e.CommandArgument.ToString())
                Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                Dim txtavl As TextBox = DirectCast(rw.FindControl("txt_updAvlseat"), TextBox)
                Dim lblcounter As Label = DirectCast(rw.FindControl("lblCounter"), Label)
                Dim BT As Button = DirectCast(rw.FindControl("Button1"), Button)
                Dim BTnCan1 As Button = DirectCast(rw.FindControl("Button2"), Button)

                Dim counter As Integer = Convert.ToInt32(e.CommandArgument.ToString())
                Dim rwadd As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                Dim totpax As Label = DirectCast(rwadd.FindControl("lbltotpax"), Label)
                Dim add As Integer = 0
                If (Convert.ToInt32(txtavl.Text) <= Convert.ToInt32(totpax.Text)) Then
                    Dim i As Integer = series.UpdateSeriesRequest(Convert.ToInt32(txtavl.Text.Trim()), lblcounter.Text, "Request")
                    add = Convert.ToInt32(totpax.Text) - Convert.ToInt32(txtavl.Text.Trim())
                    Dim ik As Integer = series.Update_ConfirmHoldSeat1(Convert.ToInt32(txtavl.Text.Trim()), counter, "Inprocess", add)
                    series.UpdateConfirmSeats(lblcounter.Text, Convert.ToInt32(txtavl.Text.Trim()))
                    'If (add <> 0) Then
                    '    series.Update_ConfirmHoldSeat(Convert.ToInt32(txtavl.Text.Trim()), counter, "Inprocess")
                    '    series.UpdateConfirmSeats(lblcounter.Text, Convert.ToInt32(txtavl.Text.Trim()))
                    'Else
                    '    series.Update_ConfirmHoldSeat1(Convert.ToInt32(txtavl.Text.Trim()), counter, "Inprocess")
                    '    series.UpdateConfirmSeats(lblcounter.Text, Convert.ToInt32(txtavl.Text.Trim()))
                    'End If
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Updated Successfully');", True)
                    ShowAlertMessage("Updated Successfully")
                    Processbind()
                Else
                    'Dim dt As New DataTable
                    'dt = series.GetFlightDetails()
                    'Dim counter As Integer = Convert.ToInt32(e.CommandArgument.ToString())
                    'Dim dtseries As Array = dt.Select("Counter='" & counter & "'", "")
                    'Dim AvlSeat As Integer = dtseries(0)("Available_Seat").ToString()
                    'Dim txt As TextBox = TryCast(e.CommandSource, TextBox)
                    'Dim gvr As GridViewRow = TryCast(txt.Parent.Parent, GridViewRow)
                    'Dim index As Integer = gvr.RowIndex
                    'Dim txtavl As TextBox = DirectCast(grd_processSeries.Rows(index).FindControl("txt_updAvlseat"), TextBox)
                    Dim rw1 As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                    Dim totpax1 As Label = DirectCast(rw1.FindControl("lbltotpax"), Label)
                    ' Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Only " & totpax1.Text & " Seat Requested  and You are looking for " & txtavl.Text.Trim() & " Seat ');", True)
                    ShowAlertMessage("Only " & totpax1.Text & " Seat Requested  and You are looking for " & txtavl.Text.Trim() & " Seat ")
                    grd_processSeries.Columns(10).Visible = True
                    Dim txt_updAvlseat As TextBox = DirectCast(rwadd.FindControl("txt_updAvlseat"), TextBox)
                    txt_updAvlseat.Visible = True
                    txt_updAvlseat.Text = ""

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkclose.Click
        Try
            divProcess.Visible = False
            divrequest.Visible = True
            Pendingbind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkviewProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkviewProcess.Click
        Try
            divProcess.Visible = True
            divrequest.Visible = False
            Processbind()
        Catch ex As Exception

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
End Class

