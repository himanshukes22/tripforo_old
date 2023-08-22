Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class Reports_HoldPNR_DomHoldPNRReport
    Inherits System.Web.UI.Page
    Private Distr As New Distributor
    'Private ST As New SqlTransaction()
    'Private STDom As New SqlTransactionDom()
    'Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    'Dim adap As SqlDataAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub BindGrid()
        Try

            GridView1.DataSource = Distr.GetHoldPnrReport(Session("UID"), Session("User_Type"), "D")
            GridView1.DataBind()

            'Dim ds As New DataSet()
            'If (Session("User_Type") = "AGENT") Then
            '    ds = ID.IntlHoldPNRAgentReport("Confirm", "D", Session("UID"), "InProcess")
            '    GridView1.DataSource = ds
            '    GridView1.DataBind()
            'Else
            '    ds = ID.IntlConfirmHoldPNR("Confirm", "D", "InProcess")
            '    GridView1.DataSource = ds
            '    GridView1.DataBind()
            'End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    'Protected Sub GridHoldPNRAccept_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridHoldPNRAccept.PageIndexChanging

    'End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
