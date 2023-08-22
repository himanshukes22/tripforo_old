Imports System.Data
Partial Class SprReports_OLSeries_SeriesAccountReport
    Inherits System.Web.UI.Page
    Dim series As New SeriesDepart
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Dim FromDate As String
        Dim ToDate As String
        If [String].IsNullOrEmpty(Request("From")) Then
            FromDate = ""
        Else
            'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

            FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
            FromDate = FromDate + " " + "12:00:00 AM"
        End If
        If [String].IsNullOrEmpty(Request("To")) Then
            ToDate = ""
        Else
            ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
            ToDate = ToDate & " " & "11:59:59 PM"
        End If


        Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
        Dim Airline As String = ""

        'If airline <> "" AndAlso airline IsNot Nothing Then
        '    airline = Request("txtairline")
        'Else
        '    airline = Nothing
        'End If

        Dim dt As New DataTable
        dt = series.SeriesFlightReport(FromDate, ToDate, Session("UID").ToString(), Session("User_Type").ToString(), Airline, AgentID, "AccRqquest", "")
        grd_SeriesAccreport.DataSource = dt
        grd_SeriesAccreport.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then

            Response.Redirect("~/Login.aspx")

        End If
    End Sub


End Class
