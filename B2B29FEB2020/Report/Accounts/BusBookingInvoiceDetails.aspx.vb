Imports System.Data.SqlClient
Imports System.Data

Partial Class SprReports_Accounts_BusBookingInvoiceDetails
    Inherits System.Web.UI.Page
    Dim DS As New DataSet()
    Private STDom As New SqlTransactionDom()
    Dim splitfromDate As String()
    Dim splittoDate As String()
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

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            Dim FromDate As String
            Dim ToDate As String
            If Request.Form("dropstatus").ToString() = "Cancelled" Then
                GrdBusReport.Columns(0).Visible = False
                GrdBusReport.Columns(1).Visible = True

            End If
            If (Request.Form("dropstatus").ToString()) = "Booked" Then
                GrdBusReport.Columns(0).Visible = True
                GrdBusReport.Columns(1).Visible = False
            End If
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
            Dim status As String = If([String].IsNullOrEmpty(Request.Form("dropstatus")), Nothing, Request.Form("dropstatus"))

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
            adap.SelectCommand.Parameters.AddWithValue("@Status", status)
            adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID)
            adap.SelectCommand.Parameters.AddWithValue("@usertype", Session("User_Type").ToString())
            adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID").ToString())
            adap.Fill(DS)
            GrdBusReport.DataSource = DS
            GrdBusReport.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            Dim FromDate As String
            Dim ToDate As String
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
            Dim status As String = If([String].IsNullOrEmpty(Request.Form("dropstatus")), Nothing, Request.Form("dropstatus"))

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
            adap.SelectCommand.Parameters.AddWithValue("@Status", status)
            adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID)
            adap.SelectCommand.Parameters.AddWithValue("@usertype", Session("User_Type").ToString())
            adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID").ToString())
            adap.Fill(DS)
            STDom.ExportData(DS)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

End Class
