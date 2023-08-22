Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Partial Class SprReports_BusReport
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Dim DS As New DataSet()
    Dim splitfromDate As String()
    Dim splittoDate As String()
    Dim FromDate As String
    Dim ToDate As String
    Dim Status As String
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim AgentID As String = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False
                    td_Agency1.Visible = False
                    GrdBusReport.Columns(1).Visible = True

                End If
                splitfromDate = DateTime.Now.ToString().Split("/")
                FromDate = splitfromDate(0).Trim() + "-" + splitfromDate(1).Trim() + "-" + splitfromDate(2).Substring(0, 4).Trim()
                splittoDate = DateTime.Now.ToString().Split("/")
                ToDate = splittoDate(0).Trim() + "-" + splittoDate(1).Trim() + "-" + splittoDate(2).Substring(0, 4).Trim()
                Dim adap As SqlDataAdapter
                adap = New SqlDataAdapter("SP_BUS_REPORT", con)
                adap.SelectCommand.CommandType = CommandType.StoredProcedure
                adap.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate)
                adap.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate)
                adap.SelectCommand.Parameters.AddWithValue("@Status", "BOOKED")
                adap.SelectCommand.Parameters.AddWithValue("@usertype", Session("User_Type").ToString())
                adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID").ToString())
                adap.Fill(DS)
                'GrdBusReport.DataSource = DS
                'GrdBusReport.DataBind()

                'If Status Is "BOOKED" Then
                'tbl_amount.Visible = True
                BindSales(DS)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
           
            If Request("From").ToString() <> Nothing Then
                splitfromDate = Request("From").ToString().Split("-")
                FromDate = splitfromDate(1).Trim() + "-" + splitfromDate(0).Trim() + "-" + splitfromDate(2)
            Else
                splitfromDate = DateTime.Now.ToString().Split("/")
                FromDate = splitfromDate(0).Trim() + "-" + splitfromDate(1).Trim() + "-" + splitfromDate(2).Substring(0, 4).Trim()
            End If
            If Request("To").ToString() <> Nothing Then
                splittoDate = Request("To").ToString().Split("-")
                ToDate = splittoDate(1).Trim() + "-" + splittoDate(0).Trim() + "-" + splittoDate(2)
            Else
                splittoDate = DateTime.Now.ToString().Split("/")
                ToDate = splittoDate(0).Trim() + "-" + splittoDate(1).Trim() + "-" + splittoDate(2).Substring(0, 4).Trim()
            End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))

            Dim Source As String = If([String].IsNullOrEmpty(TxtSource.Text), Nothing, TxtSource.Text.Trim)
            Dim Destination As String = If([String].IsNullOrEmpty(TxtDestination.Text), Nothing, TxtDestination.Text.Trim)
            Dim orderID As String = If([String].IsNullOrEmpty(txtOrderID.Text), Nothing, txtOrderID.Text.Trim)
            Dim BusOperator As String = If([String].IsNullOrEmpty(txtBusOperator.Text), Nothing, txtBusOperator.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txtTicketNo.Text), Nothing, txtTicketNo.Text.Trim)
            Dim Pnr As String = If([String].IsNullOrEmpty(txtPnr.Text), Nothing, txtPnr.Text.Trim)
            If ddl_Status.SelectedValue = "select" Then
                Status = Nothing
            Else
                Status = ddl_Status.SelectedValue
            End If

            Dim adap As SqlDataAdapter
            adap = New SqlDataAdapter("SP_BUS_REPORT", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate)
            adap.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate)
            adap.SelectCommand.Parameters.AddWithValue("@ORDERID", orderID)
            adap.SelectCommand.Parameters.AddWithValue("@SOURCE", Source)
            adap.SelectCommand.Parameters.AddWithValue("@DESTINATION", Destination)
            adap.SelectCommand.Parameters.AddWithValue("@BUSOPERATOR", BusOperator)
            adap.SelectCommand.Parameters.AddWithValue("@TICKETNO", TicketNo)
            adap.SelectCommand.Parameters.AddWithValue("@PNR", Pnr)
            adap.SelectCommand.Parameters.AddWithValue("@Status", Status)
            adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID)
            adap.SelectCommand.Parameters.AddWithValue("@usertype", Session("User_Type").ToString())
            adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID").ToString())
            adap.Fill(DS)
            GrdBusReport.DataSource = DS
            GrdBusReport.DataBind()

            'If Status Is "BOOKED" Then
            'tbl_amount.Visible = True
            BindSales(DS)
            'Else
            'tbl_amount.Visible = False
            'End If




        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindSales(ByVal ds As DataSet)
        Try
            ' tbl_amount.Visible = True
            Dim dt As DataTable
            Dim Db As String = ""
            Dim sum As Double = 0
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Db = dr("TA_NET_FARE").ToString()
                    If Db Is Nothing OrElse Db = "" Then
                        Db = 0
                    Else
                        sum += Db
                    End If
                Next
            End If
            lbl_Total.Text = "0"
            If sum <> 0 Then
                lbl_Total.Text = sum.ToString
            End If
            lbl_Sales.Text = dt.Rows.Count
        Catch ex As Exception
            ' HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            If Request("From").ToString() <> Nothing Then
                splitfromDate = Request("From").ToString().Split("-")
                FromDate = splitfromDate(1).Trim() + "-" + splitfromDate(0).Trim() + "-" + splitfromDate(2)
            Else
                splitfromDate = DateTime.Now.ToString().Split("/")
                FromDate = splitfromDate(0).Trim() + "-" + splitfromDate(1).Trim() + "-" + splitfromDate(2).Substring(0, 4).Trim()
            End If
            If Request("To").ToString() <> Nothing Then
                splittoDate = Request("To").ToString().Split("-")
                ToDate = splittoDate(1).Trim() + "-" + splittoDate(0).Trim() + "-" + splittoDate(2)
            Else
                splittoDate = DateTime.Now.ToString().Split("/")
                ToDate = splittoDate(0).Trim() + "-" + splittoDate(1).Trim() + "-" + splittoDate(2).Substring(0, 4).Trim()
            End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))

            Dim Source As String = If([String].IsNullOrEmpty(TxtSource.Text), Nothing, TxtSource.Text.Trim)
            Dim Destination As String = If([String].IsNullOrEmpty(TxtDestination.Text), Nothing, TxtDestination.Text.Trim)
            Dim orderID As String = If([String].IsNullOrEmpty(txtOrderID.Text), Nothing, txtOrderID.Text.Trim)
            Dim BusOperator As String = If([String].IsNullOrEmpty(txtBusOperator.Text), Nothing, txtBusOperator.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txtTicketNo.Text), Nothing, txtTicketNo.Text.Trim)
            Dim Pnr As String = If([String].IsNullOrEmpty(txtPnr.Text), Nothing, txtPnr.Text.Trim)
            If ddl_Status.SelectedValue = "select" Then
                Status = Nothing
            Else
                Status = ddl_Status.SelectedValue
            End If

            Dim adap As SqlDataAdapter
            adap = New SqlDataAdapter("SP_BUS_REPORT", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate)
            adap.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate)
            adap.SelectCommand.Parameters.AddWithValue("@ORDERID", orderID)
            adap.SelectCommand.Parameters.AddWithValue("@SOURCE", Source)
            adap.SelectCommand.Parameters.AddWithValue("@DESTINATION", Destination)
            adap.SelectCommand.Parameters.AddWithValue("@BUSOPERATOR", BusOperator)
            adap.SelectCommand.Parameters.AddWithValue("@TICKETNO", TicketNo)
            adap.SelectCommand.Parameters.AddWithValue("@PNR", Pnr)
            adap.SelectCommand.Parameters.AddWithValue("@Status", Status)
            adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID)
            adap.SelectCommand.Parameters.AddWithValue("@usertype", Session("User_Type").ToString())
            adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID").ToString())
            adap.Fill(DS)
            STDom.ExportData(DS)
        Catch ex As Exception

        End Try
        
    End Sub
End Class
