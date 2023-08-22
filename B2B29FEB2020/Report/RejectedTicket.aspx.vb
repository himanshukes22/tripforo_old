Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Globalization
Partial Class SprReports_RejectedTicket
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Private ST As New SqlTransaction()
    Dim AgencyDDLDS, grdds As New DataSet()
    Dim FromDate1 As String
    Dim ToDate1 As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim AgentID As String = Nothing
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False
                    AgentID = Session("UID").ToString()
                Else
                    ' Bind Agency Name DDL
                    'If (Session("user_type") = "SALES") Then
                    '    AgencyDDLDS= STDom.getAgencybySalesRef(Session("UID").ToString)
                    'Else
                    '    AgencyDDLDS = ST.GetAgencyDetailsDDL()
                    'End If
                    'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                    '    ddl_AgencyName.AppendDataBoundItems = True
                    '    ddl_AgencyName.Items.Clear()
                    '    ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    '    ddl_AgencyName.DataSource = AgencyDDLDS
                    '    ddl_AgencyName.DataTextField = "Agency_Name"
                    '    ddl_AgencyName.DataValueField = "user_id"
                    '    ddl_AgencyName.DataBind()
                    'End If
                End If

                Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                Dim curr_date1 = Now()
                'GridView Date Set
                grdds.Clear()
                grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, curr_date, curr_date1, Nothing, Nothing, Nothing, Nothing, Nothing, AgentID, Nothing, StatusClass.Rejected)
                BindGrid(grdds)
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Private Sub BindGrid(ByVal Gds As DataSet)
        Try
            ViewState("Grdds") = Gds
            'If Session("User_Type").ToString = "AGENT" Then
            '    ticket_grdview.Columns(11).Visible = False
            '    ticket_grdview.Columns(12).Visible = False
            'End If
            ticket_grdview.DataSource = Gds
            ticket_grdview.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    'Filtering data from Search click
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

    Protected Sub ticket_grdview_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ticket_grdview.PageIndexChanging
        Try
            ticket_grdview.PageIndex = e.NewPageIndex
            BindGrid(ViewState("Grdds"))
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

            Dim trip As String
            If ddlTrip.SelectedIndex > 0 Then
                trip = ddlTrip.SelectedValue
            Else
                trip = ""
            End If

            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl_Rejected(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, AirPNR, AgentID, trip, StatusClass.Rejected)
            BindGrid(grdds)

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
                ExportEmpty()
            End If
        Else
            ExportEmpty()
        End If
    End Sub
    Public Sub ExportEmpty()
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

            Dim trip As String
            If ddlTrip.SelectedIndex > 0 Then
                trip = ddlTrip.SelectedValue
            Else
                trip = ""
            End If

            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl_Rejected(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, AirPNR, AgentID, trip, StatusClass.Rejected)
            'STDom.ExportData(pnrds)
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then
                STDom.ExportData(grdds)
            Else
                Dim StrDataTable As New DataTable
                'If grdds.Tables(0).Columns.Contains("PartnerName") = True Then
                StrDataTable = grdds.Tables(0)
                ' StrDataTable.Columns.Remove("PartnerName")
                '' StrDataTable.Columns.Remove("PaymentMode")
                'StrDataTable.Columns.Remove("PgCharges")
                STDom.ExportData(grdds)
                'End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Protected Sub ticket_grdview_RowDataBound(sender As Object, e As GridViewRowEventArgs)
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

