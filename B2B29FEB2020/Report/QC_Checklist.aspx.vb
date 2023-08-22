
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Configuration



Partial Class SprReports_QC_Checklist
    Inherits System.Web.UI.Page

    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)

    Public Sub CheckEmptyValue()
        Try
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

            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim ExecutiveId As String = If([String].IsNullOrEmpty(DdlExecId.SelectedValue.ToString()), "", DdlExecId.SelectedValue.ToString())
            Dim ds As New DataSet
            ds = ST.GetQcChecklist(FromDate, ToDate, OrderID, ExecutiveId)
            Session("Report") = ds
            GvQcChecklist.DataSource = ds

            GvQcChecklist.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            CheckEmptyValue()
        Catch ex As Exception

        End Try

    End Sub
  
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
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

            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim ExecutiveId As String = If([String].IsNullOrEmpty(DdlExecId.SelectedValue.ToString()), "", DdlExecId.SelectedValue.ToString())
            Dim ds As New DataSet
            ds = ST.GetQcChecklist(FromDate, ToDate, OrderID, ExecutiveId)
            STDom.ExportData(ds)

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub GvQcChecklist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvQcChecklist.PageIndexChanging
        Try
            GvQcChecklist.PageIndex = e.NewPageIndex
            GvQcChecklist.DataSource = Session("Report")

            GvQcChecklist.DataBind()

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                DdlExecId.AppendDataBoundItems = True
                DdlExecId.Items.Insert(0, New ListItem("-Select-", ""))
                Dim adap As New SqlDataAdapter("Select Distinct ExecutiveId from Tbl_Qcchecklist", con)
                Dim ds As New DataSet
                adap.Fill(ds)
                DdlExecId.DataSource = ds
                DdlExecId.DataTextField = "ExecutiveId"
                DdlExecId.DataBind()

            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
