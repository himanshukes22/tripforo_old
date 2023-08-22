Imports System.Data.SqlClient

Imports System.Configuration
Imports System.Data
Partial Class Report_Proxy_ProxyPaxDetails
    Inherits System.Web.UI.Page
    Private con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Session("UID") Is Nothing Then
            Response.Redirect("/Login.aspx")
        End If

        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim user_id As String = Session("UID").ToString()

        Try
            con.Open()
            Dim dt As DataTable = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter("select * from ProxyTicket where AgentID='" & user_id & "'", con)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Update_GV.DataSource = dt
                Update_GV.DataBind()
            End If

            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
