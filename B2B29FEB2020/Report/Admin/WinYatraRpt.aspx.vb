Imports System.Data
Partial Class WinYatraRpt
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Dim GridDS, AgencyDDLDS As New DataSet
    Dim RptType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try

            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            RptType = Request.QueryString("RPT")

            If Not IsPostBack Then
                AgencyDDLDS = ST.GetAgencyDetailsDDL()
                If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                    ddlAgencyName.AppendDataBoundItems = True
                    ddlAgencyName.Items.Clear()
                    ddlAgencyName.Items.Insert(0, "--Select Agency Name--")
                    ddlAgencyName.DataSource = AgencyDDLDS
                    ddlAgencyName.DataTextField = "Agency_Name"
                    ddlAgencyName.DataValueField = "user_id"
                    ddlAgencyName.DataBind()

                    Dim curr_date = Now.Date() & " " & "00:00:00 AM"
                    Dim curr_date1 = Now()
                    GridDS.Clear()

                    If RptType = "T" Then
                        titles.InnerText = "Search Win Yatra Ticket Reports"
                        GridDS = ST.GetWinYatraRpt(curr_date, curr_date1, Nothing, "Ticketed")
                    ElseIf RptType = "R" Then
                        titles.InnerText = "Search Win Yatra Refund Reports"
                        GridDS = ST.GetWinYatraRpt(curr_date, curr_date1, Nothing, "Refund")
                    End If
                    BindGrid(GridDS)
                End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid(ByVal ds As DataSet)
        'Dim dt As DataTable
        'Dim amt As Integer = 0
        'Dim sum As Integer = 0
        Try
            If Session("User_Type").ToString = "ADMIN" Then
                RptGrd.DataSource = ds
                RptGrd.DataBind()
                ViewState("GridDS") = ds

                '    lblCount.Text = "0"
                '    dt = ds.Tables(0)
                '    If dt.Rows.Count > 0 Then
                '        For Each dr As DataRow In dt.Rows

                '            amt = Convert.ToInt32(dr("Amount"))
                '            If amt = 0 Then
                '                amt = 0
                '            Else
                '                sum += amt
                '            End If
                '        Next
                '        lblCount.Text = dt.Rows.Count.ToString()
                '    End If
                '    lblTotal.Text = "0"
                '    If sum <> 0 Then
                '        lblTotal.Text = sum.ToString()
                '    End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Protected Sub RptGrd_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RptGrd.PageIndexChanging
        Try
            RptGrd.PageIndex = e.NewPageIndex
            BindGrid(ViewState("GridDS"))

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            Dim FromDate, ToDate As String
            Dim AgentID As String = Nothing

            If [String].IsNullOrEmpty(From.Text) Then
                FromDate = Nothing
            Else
                FromDate = Strings.Mid((From.Text).Split(" ")(0), 4, 2) + "/" + Strings.Left((From.Text).Split(" ")(0), 2) + "/" + Strings.Right((From.Text).Split(" ")(0), 4)
                FromDate = FromDate + " " + "00:00:00 AM"
            End If
            If [String].IsNullOrEmpty([To].Text) Then
                ToDate = Nothing
            Else
                ToDate = Mid(([To].Text).Split(" ")(0), 4, 2) & "/" & Left(([To].Text).Split(" ")(0), 2) & "/" & Right(([To].Text).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            If ddlAgencyName.SelectedIndex > 0 Then
                AgentID = ddlAgencyName.SelectedValue
            End If

            GridDS.Clear()
            If RptType = "T" Then
                GridDS = ST.GetWinYatraRpt(FromDate, ToDate, AgentID, "Ticketed")
            ElseIf RptType = "R" Then
                GridDS = ST.GetWinYatraRpt(FromDate, ToDate, AgentID, "Refund")
            End If
            BindGrid(GridDS)

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
