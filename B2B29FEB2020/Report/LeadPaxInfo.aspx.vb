
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class SprReports_LeadPaxInfo
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Private con As New SqlConnection()
    Private con1 As New SqlConnection()
    Private adp As SqlDataAdapter
    Private ds As New DataSet()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            If Session("User_Type") = "AGENT" Then
                td_agency.Visible = False
            End If


        End If
        Search()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub Search()
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
            Dim ds_cur As New DataTable
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            adp = New SqlDataAdapter("SP_RAIL_GETIDINFO", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
            adp.Fill(ds_cur)
            Session("SearchData") = ds_cur
            Try
                If ds_cur.Rows.Count > 0 Then

                End If

            Catch ex As Exception
            End Try
            Gridpaxinfo.DataSource = ds_cur
            Gridpaxinfo.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

            ex.Message.ToString()
        End Try

    End Sub
End Class
