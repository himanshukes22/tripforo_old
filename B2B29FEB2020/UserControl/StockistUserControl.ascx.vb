Imports System.Data.SqlClient
Imports System.Data

Partial Class UserControl_StockistUserControl
    Inherits System.Web.UI.UserControl
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Private dt As New DataTable()
    Private da As New SqlDataAdapter()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If IsPostBack <> True Then
            Try
                da = New SqlDataAdapter("SP_GETSTOCKISTLIST", con)
                da.SelectCommand.CommandType = CommandType.StoredProcedure
                da.SelectCommand.Parameters.AddWithValue("@AGENTID", Session("UID"))
                da.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
                hdnAgentState.Value = dt.Rows(0)("State").ToString().Trim()
            Catch ex As Exception

            End Try
        End If

    End Sub
End Class
