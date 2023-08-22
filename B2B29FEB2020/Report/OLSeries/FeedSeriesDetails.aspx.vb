Imports System.Data
Imports System.Collections
Imports System.IO
Partial Class SprReports_OLSeries_FeedSeriesDetails
    Inherits System.Web.UI.Page

    Dim series As New SeriesDepart

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            If (Session("User_Type") <> "EXEC") Then
                Response.Redirect("~/Login.aspx")
            End If
        End If
        If Not IsPostBack Then
            grdTD.Visible = False

            Dim dtAsector As New DataTable
            dtAsector = series.GetFlightDetails(3)
            ddl_Airline.AppendDataBoundItems = True
            ddl_Airline.Items.Clear()
            ddl_Airline.Items.Insert(0, "--Airline--")
            ddl_Airline.DataSource = dtAsector
            ddl_Airline.DataTextField = "AirlineName"
            ddl_Airline.DataValueField = "AirlineName"
            ddl_Airline.DataBind()

            ddl_sector.AppendDataBoundItems = True
            ddl_sector.Items.Clear()
            ddl_sector.Items.Insert(0, "--Sector--")
            ddl_sector.DataSource = dtAsector
            ddl_sector.DataTextField = "Sector"
            ddl_sector.DataValueField = "Sector"
            ddl_sector.DataBind()

        End If
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            Dim airline As String = Request("txtairline")
            Dim aircode As String = Request("txtaircode")
            Dim sector As String = Request("txtsector")
            Dim amount As String = Request("txtamount")
            Dim avlseat As Integer = Convert.ToInt32(Request.Form("txtseat"))
            Dim deptdate As String = Request("From")
            Dim retdate As String = Request("To")
            Dim trip As String = DdlTrip.SelectedValue.ToString()
            Dim i As Integer = series.InsertSeriesDetails(airline.Trim.ToUpper, aircode.Trim.ToUpper, sector.Trim.ToUpper, amount, avlseat, deptdate, retdate, trip, (Session("UID").ToString()))
            If i <> 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Details Submmitted Successfully');", True)

            End If
        Catch ex As Exception

        End Try


    End Sub



    Protected Sub grd_Seriesflight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grd_Seriesflight.RowCancelingEdit
        Try
            grd_Seriesflight.EditIndex = -1
            bind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grd_Seriesflight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd_Seriesflight.RowDeleting
        Try
            Dim counter As Integer = grd_Seriesflight.DataKeys(e.RowIndex).Value.ToString()
            If counter > 0 Then
                Dim i As Integer = series.DeleteSeriesDeparture(counter)
            End If
            bind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grd_Seriesflight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grd_Seriesflight.RowEditing
        grd_Seriesflight.EditIndex = e.NewEditIndex
        bind()
    End Sub
    Public Sub bind()
        Try
            Dim dt As New DataTable
            dt = series.GetFlightDetails(0, Session("UID"))
            grd_Seriesflight.DataSource = dt
            grd_Seriesflight.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grd_Seriesflight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grd_Seriesflight.RowUpdating
        Try
            Dim counter As Integer = grd_Seriesflight.DataKeys(e.RowIndex).Value.ToString()
            If counter > 0 Then
                Dim lbalirline As Label = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("lblairline"), Label)
                Dim txtairline As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txt_aircode"), TextBox)
                Dim sector As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txt_sector"), TextBox)
                Dim txtamount As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txt_amount"), TextBox)
                Dim txtavlseats As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txtavlseats"), TextBox)
                Dim txtdeptdate As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txt_deptdate"), TextBox)
                Dim txtretdate As TextBox = DirectCast(grd_Seriesflight.Rows(e.RowIndex).FindControl("txt_retdate"), TextBox)
                Dim i As Integer = series.UpdateSeriseDeparture(txtairline.Text.Trim, counter, sector.Text.Trim(), Convert.ToInt32(txtamount.Text.Trim()), Convert.ToInt32(txtavlseats.Text.Trim()), txtdeptdate.Text.Trim(), txtretdate.Text.Trim())
                grd_Seriesflight.EditIndex = -1
                ' Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Updated Successfully!');", True)
                ShowAlertMessage("Updated Successfully")
            End If
            bind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Try
            grdTD.Visible = False
            seriesDetailsTD.Visible = True
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_modify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_modify.Click
        Try
            seriesDetailsTD.Visible = False
            grdTD.Visible = True
            btn_close.Visible = True
            bind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Dim FromDate As String
        Dim ToDate As String
        If [String].IsNullOrEmpty(Request("From")) Then
            FromDate = ""
        Else
            'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

            'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
            FromDate = Request("From") '+ " " + "12:00:00 AM"
        End If
        If [String].IsNullOrEmpty(Request("To")) Then
            ToDate = ""
        Else
            'ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
            ToDate = Request("To") '& " " & "11:59:59 PM"
        End If


        'Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
        Dim Airline As String = If([String].IsNullOrEmpty(Request("txtairline")), "", Request("txtairline"))
        Dim sector As String = ""
        If (ddl_sector.SelectedIndex > 0) Then
            sector = ddl_sector.SelectedValue
        End If
        Dim AirlineName As String = ""
        If (ddl_Airline.SelectedIndex > 0) Then
            AirlineName = ddl_Airline.SelectedValue
        End If
        'If airline <> "" AndAlso airline IsNot Nothing Then
        '    airline = Request("txtairline")
        'Else
        '    airline = Nothing
        'End If

        Dim dt As New DataTable
        dt = series.SeriesFlightReport(FromDate, ToDate, Session("UID"), sector, AirlineName, "", "ExecSearch", "")
        grd_Seriesflight.DataSource = dt
        grd_Seriesflight.DataBind()
    End Sub

    Protected Sub btn_Offline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Offline.Click
        seriesDetailsTD.Visible = False
        Offline_ReqTD.Visible = True
        grd_OfflineReq.Columns(9).Visible = False
        grd_OfflineReq.Columns(10).Visible = False
        grd_OfflineReq.Columns(11).Visible = False
        Offlinebind()

    End Sub
    Public Sub Offlinebind()
        Try
            Dim dt As New DataTable

            dt = series.GetFlightDetails(0, Session("UID"))
            grd_OfflineReq.DataSource = dt
            grd_OfflineReq.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grd_OfflineReq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_OfflineReq.RowCommand
        Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
        Dim hold As Button = DirectCast(rw.FindControl("btnHold"), Button)
        Dim confirm As Button = DirectCast(rw.FindControl("btnConfirm"), Button)
        Dim update As Button = DirectCast(rw.FindControl("btnUpdate"), Button)
        Dim cancel As Button = DirectCast(rw.FindControl("btnCancel"), Button)
        Dim ConfirmUpdate As Button = DirectCast(rw.FindControl("btnConfirmUpdate"), Button)
        Dim ConfirmCancel As Button = DirectCast(rw.FindControl("btnConfirmCancel"), Button)
        Dim counter As Label = DirectCast(rw.FindControl("lblcounter"), Label)
        Dim airline As Label = DirectCast(rw.FindControl("lblAirline"), Label)
        Dim airline_code As Label = DirectCast(rw.FindControl("lblAirlinecode"), Label)
        Dim sector As Label = DirectCast(rw.FindControl("lblSector"), Label)
        Dim amount As Label = DirectCast(rw.FindControl("lblAmount"), Label)
        Dim avlseat As Label = DirectCast(rw.FindControl("lblAvlSeats"), Label)
        Dim depdate As Label = DirectCast(rw.FindControl("lblDepdate"), Label)
        Dim retdate As Label = DirectCast(rw.FindControl("lblretdate"), Label)
        Dim offlinehold As TextBox = DirectCast(rw.FindControl("txtHold"), TextBox)
        Dim offlineconfirm As TextBox = DirectCast(rw.FindControl("txtConfirm"), TextBox)
        Dim txtrm As TextBox = DirectCast(rw.FindControl("txtRemark"), TextBox)
        txtrm.Visible = True
        offlinehold.Visible = True
        offlineconfirm.Visible = True



        If (e.CommandName = "Hold") Then
            grd_OfflineReq.Columns(9).Visible = True
            grd_OfflineReq.Columns(11).Visible = True
            update.Visible = True
            cancel.Visible = True
            confirm.Visible = False
            hold.Visible = False
            ConfirmUpdate.Visible = False
            ConfirmCancel.Visible = False

        End If
        If (e.CommandName = "Confirm") Then
            grd_OfflineReq.Columns(10).Visible = True
            grd_OfflineReq.Columns(11).Visible = True
            confirm.Visible = False
            update.Visible = False
            cancel.Visible = False
            hold.Visible = False
            ConfirmUpdate.Visible = True
            ConfirmCancel.Visible = True

        End If
        If (e.CommandName = "Offlineupdate") Then
            If (offlinehold.Text = "" OrElse offlinehold.Text Is Nothing) Then
                offlinehold.Text = 0
            End If
            If (Convert.ToInt32(offlinehold.Text) <= Convert.ToInt32(avlseat.Text)) Then
                ' If(txtrm.Text<>""
                If txtrm.Text <> "" AndAlso txtrm.Text IsNot Nothing Then
                    series.InsertOfflineHold(airline.Text, airline_code.Text, sector.Text, amount.Text, avlseat.Text, depdate.Text, retdate.Text, "Hold", Session("UID").ToString(), counter.Text, offlinehold.Text, 0, "Hold", txtrm.Text.Trim)
                    grd_OfflineReq.EditIndex = -1
                    'Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Updated Successfully');", True)
                    ShowAlertMessage("Updated Successfully")
                    Offlinebind()
                    grd_OfflineReq.Columns(9).Visible = False
                    grd_OfflineReq.Columns(11).Visible = False
                Else
                    'Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Please enter Remark');", True)
                    ShowAlertMessage("Please enter Remark")
                End If

            Else
                ' Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Only " & avlseat.Text & " seats availiable Please enter less than availiable seat');", True)
                ShowAlertMessage("Only " & avlseat.Text & " seats availiable Please enter less than availiable seat")
            End If

        End If
        If (e.CommandName = "ConfirmUpdate") Then
            If txtrm.Text <> "" AndAlso txtrm.Text IsNot Nothing Then
                If (offlineconfirm.Text = "" OrElse offlineconfirm.Text Is Nothing) Then
                    offlineconfirm.Text = 0
                End If
                If (Convert.ToInt32(offlineconfirm.Text) <= Convert.ToInt32(avlseat.Text)) Then
                    series.InsertOfflineHold(airline.Text, airline_code.Text, sector.Text, amount.Text, avlseat.Text, depdate.Text, retdate.Text, "Confirm", Session("UID").ToString(), counter.Text, 0, offlineconfirm.Text, "Confirm", txtrm.Text.Trim)
                    grd_OfflineReq.EditIndex = -1
                    'Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Updated Successfully');", True)
                    ShowAlertMessage("Updated Successfully")
                    Offlinebind()
                    grd_OfflineReq.Columns(10).Visible = False
                    grd_OfflineReq.Columns(11).Visible = False
                Else
                    'Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Only " & avlseat.Text & " seats availiable Please enter less than availiable seat');", True)
                    ShowAlertMessage("Only " & avlseat.Text & " seats availiable Please enter less than availiable seat")
                End If

            Else
                'Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Please enter Remark');", True)
                ShowAlertMessage("Please enter Remark")
            End If

        End If
        If (e.CommandName = "ConfirmCancel") Then
            grd_OfflineReq.EditIndex = -1
            Offlinebind()
            grd_OfflineReq.Columns(10).Visible = False
            grd_OfflineReq.Columns(11).Visible = False
        End If
    End Sub

    Protected Sub grd_OfflineReq_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grd_OfflineReq.RowCancelingEdit
        grd_OfflineReq.EditIndex = -1
        grd_OfflineReq.Columns(9).Visible = False
        grd_OfflineReq.Columns(10).Visible = False
        grd_OfflineReq.Columns(11).Visible = False
        Offlinebind()
    End Sub

    Protected Sub cancelhold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelhold.Click
        seriesDetailsTD.Visible = True
        grdTD.Visible = False
        Offline_ReqTD.Visible = False
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

