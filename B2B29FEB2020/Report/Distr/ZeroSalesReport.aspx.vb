Imports System.Data
Partial Class SprReports_Distr_ZeroSales
    Inherits System.Web.UI.Page
    Private Distr As New Distributor()
    Private STDom As New SqlTransactionDom
    Protected Sub btn_serach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_serach.Click
        Dim FromDate As String
        Dim ToDate As String
        If [String].IsNullOrEmpty(Request("From")) Then
            FromDate = ""
        Else

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
        Dim ds As New DataSet()
        ds = Distr.GetZeroSalesReport(FromDate, ToDate, AgentID, Session("User_Type"), Session("UID"))
        Grid_ZeroSales.DataSource = ds
        Grid_ZeroSales.DataBind()
        Session("ds") = ds
    End Sub

    Protected Sub Grid_ZeroSales_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid_ZeroSales.PageIndexChanging
        Try
            Grid_ZeroSales.PageIndex = e.NewPageIndex
            Grid_ZeroSales.DataSource = Session("ds")
            Grid_ZeroSales.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Dim FromDate As String
        Dim ToDate As String
        If [String].IsNullOrEmpty(Request("From")) Then
            FromDate = ""
        Else

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
        Dim ds As New DataSet()
        ds = Distr.GetZeroSalesReport(FromDate, ToDate, AgentID, Session("User_Type"), Session("UID"))
        STDom.ExportData(ds)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
End Class
