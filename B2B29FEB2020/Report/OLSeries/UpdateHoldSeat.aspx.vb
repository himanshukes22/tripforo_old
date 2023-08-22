Imports System.Data
Imports System.Collections
Imports System.IO
Partial Class SprReports_OLSeries_UpdateHoldSeat
    Inherits System.Web.UI.Page
    Dim series As New SeriesDepart

    Protected Sub grd_Updateholdseat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd_Updateholdseat.SelectedIndexChanged


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bind()

        End If

    End Sub
    Private Sub bind()
        Try
            Dim dt As New DataTable
            dt = series.GetFlightDetails(4, Session("UID"))
            grd_Updateholdseat.DataSource = dt
            grd_Updateholdseat.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grd_Updateholdseat_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_Updateholdseat.RowCommand

        Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
        Dim btnConfirm As Button = DirectCast(rw.FindControl("btnConfirm"), Button)
        Dim btnRelease As Button = DirectCast(rw.FindControl("btnRelease"), Button)
        Dim btnConfirmUpdate As Button = DirectCast(rw.FindControl("btnConfirmUpdate"), Button)
        Dim btnConfirmCancel As Button = DirectCast(rw.FindControl("btnConfirmCancel"), Button)
        Dim btnReleaseUpdate As Button = DirectCast(rw.FindControl("btnReleaseUpdate"), Button)
        Dim btnReleaseCancel As Button = DirectCast(rw.FindControl("btnReleaseCancel"), Button)
        Dim lblcounter As Label = DirectCast(rw.FindControl("lblcounter"), Label)
        Dim offlinehold As Label = DirectCast(rw.FindControl("lblofflinehold"), Label)
        Dim offlineconfirm As TextBox = DirectCast(rw.FindControl("txtconfirmholdseat"), TextBox)
        Dim avlseat As Label = DirectCast(rw.FindControl("lblavlseat"), Label)
        Dim seriesid As Label = DirectCast(rw.FindControl("lblseriesid"), Label)
        grd_Updateholdseat.Columns(13).Visible = False
        'btnConfirmUpdate.Visible = False
        'btnConfirmCancel.Visible = False
        'btnReleaseUpdate.Visible = False
        'btnReleaseCancel.Visible = False
        If e.CommandName = "Confirm" Then

            btnConfirmUpdate.Visible = True
            btnConfirmCancel.Visible = True
            grd_Updateholdseat.Columns(13).Visible = True
            offlineconfirm.Visible = True
            btnConfirm.Visible = False
            btnRelease.Visible = False

        End If
        If e.CommandName = "ConfirmCancel" Then
            grd_Updateholdseat.EditIndex = -1
            bind()
        End If
        If e.CommandName = "ConfirmUpdate" Then

            If (Convert.ToInt32(offlineconfirm.Text) <= Convert.ToInt32(offlinehold.Text)) Then
                Dim add As Integer = 0
                add = Convert.ToInt32(offlinehold.Text) - Convert.ToInt32(offlineconfirm.Text.Trim())
                series.UpdateOfflineHoldSeat(Convert.ToInt32(lblcounter.Text), Convert.ToInt32(offlineconfirm.Text.Trim()), add, Convert.ToInt32(seriesid.Text), "Maintable")
                series.UpdateOfflineHoldSeat(Convert.ToInt32(lblcounter.Text), Convert.ToInt32(offlineconfirm.Text.Trim()), add, Convert.ToInt32(seriesid.Text), "Requesttable")
                grd_Updateholdseat.EditIndex = -1
                ShowAlertMessage("Confirm Successfully")
                bind()
            Else
                ShowAlertMessage("Only " & offlinehold.Text & " Seats Avaliable You are requested for " & offlineconfirm.Text & " ")

                grd_Updateholdseat.Columns(13).Visible = True
                offlineconfirm.Visible = True
            End If



        End If
        If (e.CommandName = "Release") Then
            series.UpdateOfflineHoldSeat(Convert.ToInt32(lblcounter.Text), Convert.ToInt32(offlinehold.Text), 0, Convert.ToInt32(seriesid.Text), "ReleaseMaintable")
            series.UpdateOfflineHoldSeat(Convert.ToInt32(lblcounter.Text), Convert.ToInt32(offlinehold.Text), 0, Convert.ToInt32(seriesid.Text), "ReleaseRequesttable")
            ' Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Released Successfully');", True)
            ShowAlertMessage("Released Successfully")
            bind()
        End If


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
