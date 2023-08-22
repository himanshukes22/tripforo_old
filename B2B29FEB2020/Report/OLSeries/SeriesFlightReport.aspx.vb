Imports System.Data
Partial Class SprReports_OLSeries_SeriesFlightReport
    Inherits System.Web.UI.Page

    Dim series As New SeriesDepart
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then

            Response.Redirect("~/Login.aspx")

        End If
        If Session("User_Type") <> "" AndAlso Session("User_Type") IsNot Nothing Then
            Dim usertype = Session("User_Type").ToString()
            If usertype = "AGENT" Then
                agencyrow.Visible = False
                grd_seriesreport.Columns(0).Visible = False
                grd_seriesreport.Columns(1).Visible = False
            End If
        End If
        Try
            If Not IsPostBack Then

                LoadBind()
            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub LoadBind()
        Try
            Dim curr_date As String = System.DateTime.Now
            Dim curr_date1 As String = System.DateTime.Now
            Dim ds As New DataSet
            Dim usertype As String = Session("User_Type").ToString()
            Dim ID As String = Session("UID").ToString()
            Dim airline As String = Request("txtairline")
            Dim agencyname As String = Request("txtagency")
            If airline = "" Then
                airline = Nothing
            Else
                airline = Request("txtairline")
            End If
            If agencyname = "" Then
                agencyname = Nothing
            Else
                agencyname = Request("txtagency")
            End If
            Dim dt As New DataTable
            dt = series.SeriesFlightReport(curr_date.Split(" ")(0) & " " & "12:00:00 AM", curr_date1.Split(" ")(0) & " " & "11:59:59 PM", ID, usertype, airline, agencyname, "ExecRqquest", "")
            grd_seriesreport.DataSource = dt
            grd_seriesreport.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
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
            Dim Airline As String = If([String].IsNullOrEmpty(Request("txtairline")), "", Request("txtairline"))

            'If airline <> "" AndAlso airline IsNot Nothing Then
            '    airline = Request("txtairline")
            'Else
            '    airline = Nothing
            'End If

            Dim dt As New DataTable
            dt = series.SeriesFlightReport(FromDate, ToDate, Session("UID").ToString(), Session("User_Type").ToString(), Airline, AgentID, "ExecRqquest", "")
            grd_seriesreport.DataSource = dt
            grd_seriesreport.DataBind()
        Catch ex As Exception

        End Try

    End Sub
End Class

