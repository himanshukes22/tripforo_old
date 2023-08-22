Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class SprReports_Distr_TotalSaleDistributor
    Inherits System.Web.UI.Page
    Private Distr As New Distributor()
    Private STDom As New SqlTransactionDom()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Or (Session("User_Type") <> "ADMIN" And Session("User_Type") <> "ACC") Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            Try
                Dim dtdistr As New DataTable
                dtdistr = Distr.GetDistinctDistrId.Tables(0)
                dtdistr.Columns.Add("DistrAg", GetType(String), "Distr + Agency_Name")
                ddl_DistrID.AppendDataBoundItems = True
                ddl_DistrID.Items.Clear()
                ddl_DistrID.Items.Insert(0, "--Select--")
                ddl_DistrID.DataSource = dtdistr
                ddl_DistrID.DataTextField = "DistrAg"
                ddl_DistrID.DataValueField = "Distr"
                ddl_DistrID.DataBind()

            Catch ex As Exception

            End Try
            
        End If
        
    End Sub

    Protected Sub btn_serach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_serach.Click
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


            Dim DistrId As String = ""
            If ddl_DistrID.SelectedIndex > 0 Then
                DistrId = ddl_DistrID.SelectedValue
            
            End If

            Dim ds As New DataSet()
            ds = Distr.GetDistributorSale(FromDate, ToDate, DistrId)
            Grid_Distr.DataSource = ds
            Grid_Distr.DataBind()



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
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


            Dim DistrId As String = ""
            If ddl_DistrID.SelectedIndex > 0 Then
                DistrId = ddl_DistrID.SelectedValue

            End If

            Dim ds As New DataSet()
            ds = Distr.GetDistributorSale(FromDate, ToDate, DistrId)
            STDom.ExportData(ds)


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
