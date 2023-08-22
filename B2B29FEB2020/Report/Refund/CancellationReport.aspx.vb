Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Globalization


Partial Class SprReports_Refund_CancellationReport
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Dim AgencyDDLDS As New DataSet()
    Dim objDA As New SqlTransaction
    Private ID As New IntlDetails()
    Dim con As New SqlConnection()
    Dim con1 As New SqlConnection()
    Dim adp As SqlDataAdapter
    Public pnrds As New DataSet
    Dim req_id, data_field
    Dim ds As New DataSet()
    Dim FromDate1 As String
    Dim ToDate1 As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try


            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type") = "AGENT" Then
                td_Agency.Visible = False
            End If
            If Session("User_Type") = "EXEC" Then
                tr_ExecID.Visible = False
                tdTripNonExec1.Visible = False
                tdTripNonExec2.Visible = False
            End If

            If Not IsPostBack Then

                Dim dtExecutive As New DataTable
                Dim dtStatus As New DataTable
                dtStatus = STDom.GetStatusExecutiveID("Cancellation").Tables(0)
                dtExecutive = STDom.GetStatusExecutiveID("Cancellation").Tables(1)

                ddl_ExecID.AppendDataBoundItems = True
                ddl_ExecID.Items.Clear()
                ddl_ExecID.Items.Insert(0, "--Select--")
                ddl_ExecID.DataSource = dtExecutive
                ddl_ExecID.DataTextField = "ExecutiveID"
                ddl_ExecID.DataValueField = "ExecutiveID"
                ddl_ExecID.DataBind()

                ddl_Status.AppendDataBoundItems = True
                ddl_Status.Items.Clear()
                ddl_Status.Items.Insert(0, "--Select--")
                ddl_Status.DataSource = dtStatus
                ddl_Status.DataTextField = "Status"
                ddl_Status.DataValueField = "Status"
                ddl_Status.DataBind()



                'AgencyDDLDS = objDA.GetAgencyDetailsDDL()
                'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                '    ddl_AgencyName.AppendDataBoundItems = True
                '    ddl_AgencyName.Items.Clear()
                '    ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                '    ddl_AgencyName.DataSource = AgencyDDLDS
                '    ddl_AgencyName.DataTextField = "Agency_Name"
                '    ddl_AgencyName.DataValueField = "user_id"
                '    ddl_AgencyName.DataBind()
                '    'BindGrid()
                'End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub CheckEmptyValue()
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

            Dim UserID As String = Session("UID").ToString
            Dim UserType As String = Session("User_Type")
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Air As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim trip As String = ""
            If Session("User_Type") = "EXEC" Then
                If [String].IsNullOrEmpty(Session("TripExec")) Then
                    trip = ""
                Else
                    trip = Session("TripExec").ToString().Trim()
                End If
            Else
                trip = If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim())
            End If
            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = ""
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = ""
            End If
            pnrds = objDA.GetCancellationDetail(UserID, UserType, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, Air, AgentID, ExecID, Status, trip.Trim())
            Session("pnrds") = pnrds
            'If Session("User_Type") = "AGENT" Then
            '    grd_report.Columns(24).Visible = False
            '    grd_report.Columns(25).Visible = False
            'End If
            grd_report.DataSource = pnrds
            grd_report.DataBind()
            If pnrds.Tables(0).Rows.Count > 0 Then
                divReport.Visible = True
            Else
                divReport.Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid()

        Try
            pnrds.Clear()
            'Dim ds_cur As New DataTable
            Dim Fromdate = Now() & " " & "AM"
            Dim Todate = Request("To") & " " & "PM"
            Dim curr_date = Now.Date() & " " & "12:00:00 AM"
            Dim curr_date1 = Now()
            pnrds = objDA.GetCancellationDetailCurrent(Session("User_Type"), Session("UID"), curr_date, curr_date1, "D")
            Session("pnrds") = pnrds
            'If Session("User_Type") = "AGENT" Then
            '    grd_report.Columns(24).Visible = False
            '    grd_report.Columns(25).Visible = False
            'End If
            grd_report.DataSource = pnrds
            grd_report.DataBind()
            If pnrds.Tables(0).Rows.Count > 0 Then
                divReport.Visible = True
            Else
                divReport.Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        FromDate1 = Request("From")
        ToDate1 = Request("To")
        If FromDate1.ToString <> Nothing And ToDate1 <> Nothing Then
            If DateTime.ParseExact(FromDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(ToDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", True)
            Else
                CheckEmptyValue()
            End If
        Else
            CheckEmptyValue()
        End If
    End Sub

    Protected Sub grd_report_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd_report.PageIndexChanging
        Try
            grd_report.PageIndex = e.NewPageIndex
            grd_report.DataSource = Session("pnrds")
            grd_report.DataBind()
            If DirectCast(grd_report.DataSource, DataSet).Tables(0).Rows.Count > 0 Then
                divReport.Visible = True
            Else
                divReport.Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        FromDate1 = Request("From")
        ToDate1 = Request("To")
        If FromDate1.ToString <> Nothing And ToDate1 <> Nothing Then
            If DateTime.ParseExact(FromDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(ToDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", True)
            Else
                ExportEmptyValue()
            End If
        Else
            ExportEmptyValue()
        End If
    End Sub
    Public Sub ExportEmptyValue()
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

            Dim UserID As String = Session("UID").ToString
            Dim UserType As String = Session("User_Type")
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Air As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim trip As String = ""
            If Session("User_Type") = "EXEC" Then
                If [String].IsNullOrEmpty(Session("TripExec")) Then
                    trip = ""
                Else
                    trip = Session("TripExec").ToString().Trim()
                End If
            Else
                trip = If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim())
            End If
            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = ""
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = ""
            End If
            pnrds = objDA.GetCancellationDetail(UserID, UserType, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, Air, AgentID, ExecID, Status, ddlTripRefunDomIntl.SelectedItem.Value.Trim())
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then
                STDom.ExportData(pnrds)
            Else
                Dim StrDataTable As New DataTable
                If pnrds.Tables(0).Columns.Contains("PartnerName") = True Then
                    StrDataTable = pnrds.Tables(0)
                    StrDataTable.Columns.Remove("PartnerName")
                    'StrDataTable.Columns.Remove("PaymentMode")
                    'StrDataTable.Columns.Remove("PGCharges")
                    STDom.ExportData(pnrds)
                End If
            End If
            'STDom.ExportData(pnrds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    'Protected Sub grd_report_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Dim lblpaymentmode As Label
    '    Dim lblPGCharges As Label

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        lblpaymentmode = e.Row.FindControl("lbl_PaymentMode")
    '        lblPGCharges = e.Row.FindControl("lbl_Charges")
    '        If lblpaymentmode.Text = "" Or lblpaymentmode.Text Is Nothing Then
    '            lblpaymentmode.Text = "Wallet"
    '        End If
    '        If lblPGCharges.Text = "" Or lblPGCharges.Text Is Nothing Then
    '            lblPGCharges.Text = "0.0000"
    '        End If
    '        lblpaymentmode.Text = lblpaymentmode.Text.ToUpper()
    '        lblPGCharges.Text = lblPGCharges.Text.Substring(0, lblPGCharges.Text.Length - 2)
    '    End If
    'End Sub
End Class
