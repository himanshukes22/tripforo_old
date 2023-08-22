Imports System.Data
Imports System.Drawing
Partial Class SprReports_OLSeries_FlightDetails
    Inherits System.Web.UI.Page
    Dim serise As New SeriesDepart
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                If (Session("User_Type") <> "AGENT") Then
                    Response.Redirect("~/Login.aspx")
                End If
            End If


            If Not IsPostBack Then
                Dim dt As New DataTable
                dt = serise.GetFlightDetails(0, "", Request.QueryString("id").ToString())
                grd_flight.DataSource = dt
                grd_flight.DataBind()
                Dim dtAsector As New DataTable
                dtAsector = serise.GetFlightDetails(3)
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
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub grd_flight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_flight.RowDataBound
        Try
            Dim table As String = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                If DataBinder.Eval(e.Row.DataItem, "Available_Seat").ToString() = "0" Then
                    Dim rowid As LinkButton = DirectCast(e.Row.FindControl("lnk"), LinkButton)
                    rowid.Text = "SOLD"
                    rowid.ForeColor = Color.Yellow
                    e.Row.BackColor = Color.Red
                    ' e.Row.ForeColor = Color.Yellow

                    'rowid.Enabled = False
                End If
                If DataBinder.Eval(e.Row.DataItem, "Available_Seat").ToString() = "1" OrElse DataBinder.Eval(e.Row.DataItem, "Available_Seat").ToString() = "2" OrElse DataBinder.Eval(e.Row.DataItem, "Available_Seat").ToString() = "3" Then
                    table = "<IMG SRC='image/anibull1.GIF' ALT='OnProcess'  BORDER='0'>"
                    Dim lblind As Label = DirectCast(e.Row.FindControl("lbl_indicator"), Label)
                    lblind.Text = table


                End If
            End If
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
        dt = serise.SeriesFlightReport(FromDate, ToDate, "", sector, AirlineName, "", "AgentSearch", Request.QueryString("id").ToString())
        grd_flight.DataSource = dt
        grd_flight.DataBind()
    End Sub

    Protected Sub grd_flight_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_flight.RowCommand
        ' If multiple buttons are used in a GridView control, use the
        ' CommandName property to determine which button was clicked.
        If e.CommandName = "getItinerary" Then
            Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            'LinkButton lbItinerary = (LinkButton)grd_flight.Rows(index).FindControl("lblaircode")
            Dim lbItinerary As Label = DirectCast(rw.FindControl("lblaircode"), Label)
            lblShowItnry.Text = lbItinerary.Text
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "<script type='text/javascript'>openDialog();</script>", False)

        End If
    End Sub



End Class
