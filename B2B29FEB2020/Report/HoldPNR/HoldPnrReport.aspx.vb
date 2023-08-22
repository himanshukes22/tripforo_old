Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Globalization
Partial Class SprReports_HoldPNR_HoldPnrReport
    Inherits System.Web.UI.Page
    Dim Distr As New Distributor()
    Private STDom As New SqlTransactionDom()
    Dim AgencyDDLDS As New DataSet()
    Dim objDA As New SqlTransaction
    Dim con As New SqlConnection()
    Dim con1 As New SqlConnection()
    Dim adp As SqlDataAdapter
    Public pnrds As New DataSet
    Dim ds As New DataSet()
    Dim FromDate1 As String
    Dim ToDate1 As String
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
            'If Session("User_Type").ToString().Trim().ToUpper().Equals("ADMIN") Then
            '    td_Agency.Visible = False
            'End If
            If Session("User_Type") = "AGENT" Then
                td_Agency.Visible = False
               
            End If
            If Session("User_Type") = "EXEC" Then
                tr_ExecID.Visible = False
                tdTripNonExec1.Visible = False
                tdTripNonExec2.Visible = False
            End If
            'If Page.IsPostBack Then
            '    BindGrid(True)
            'Else
            '    BindGrid(False)
            'End If
            ''If Page.IsPostBack = False Then
            ''    CheckEmptyValue()
            ''End If
            If Not IsPostBack Then
                CheckEmptyValue()
                Dim dtExecutive As New DataTable
                Dim dtStatus As New DataTable
                Dim dsddls As New DataSet()
                dsddls = STDom.GetStatusExecutiveID("Hold")
                dtStatus = dsddls.Tables(0)
                dtExecutive = dsddls.Tables(1)

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
    Public Sub BindGrid(ByVal isPost As Boolean)
        Try
            Dim dSet As New DataSet()
            ''''dSet = Distr.GetHoldPnrReport(Session("UID"), Session("User_Type"), "D")
            If isPost = True Then
                dSet = Distr.GetHoldPnrReportNew(Session("UID"), Session("User_Type"), IIf(ddlTripRefunDomIntl.SelectedItem.Value IsNot Nothing, ddlTripRefunDomIntl.SelectedItem.Value.Trim(), ""), _
                        IIf(Request("From") IsNot Nothing, Request("From").ToString(), ""), IIf(Request("To") IsNot Nothing, Request("To"), ""), _
                    IIf(txt_OrderId.Text IsNot Nothing, txt_OrderId.Text.Trim(), ""), IIf(txt_PNR.Text IsNot Nothing, txt_PNR.Text.Trim(), ""), _
                    IIf(txt_AirPNR.Text IsNot Nothing, txt_AirPNR.Text.Trim(), ""), IIf(txt_PaxName.Text IsNot Nothing, txt_PaxName.Text.Trim(), ""), _
                   IIf(txt_TktNo.Text IsNot Nothing, txt_TktNo.Text.Trim(), ""), "", "", "", "")
            Else
                dSet = Distr.GetHoldPnrReportNew(Session("UID"), Session("User_Type"), "", "", "", "", "", "", "", "", "", "", "", "")
            End If

            If dSet IsNot Nothing Then
                If dSet.Tables.Count > 0 Then
                    ''If dSet.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = dSet
                    GridView1.DataBind()
                    If dSet.Tables(0).Rows.Count > 0 Then
                        divReport.Visible = True
                    Else
                        divReport.Visible = False
                    End If
                    ''End If
                End If
            End If
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
            ''''BindGrid(Page.IsPostBack)
            CheckEmptyValue()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Sub CheckEmptyValue()
        Try
            pnrds.Clear()
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
            Dim trip As String = "" ''''IIf(Session("User_Type") = "EXEC", IIf([String].IsNullOrEmpty(Session("TripExec")), "", Session("TripExec").ToString().Trim()), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))
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
            Dim distrHold As String = ""
            If UserType.Trim().ToUpper() = "DI" Then
                distrHold = UserID
            End If
            pnrds = Distr.GetHoldPnrReportNew(UserID, UserType, trip, FromDate, ToDate, OrderID, PNR, Air, PaxName, TicketNo, AgentID, ExecID, distrHold, "REPORT")
            'adp.Fill(pnrds)
            Session("pnrds") = pnrds
            If Session("user_type").ToString = "AGENT" Then
                'GridView1.Columns(15).Visible = False
                'GridView1.Columns(16).Visible = False
                ' GridView1.Columns(17).Visible = False
            End If
            GridView1.DataSource = pnrds
            GridView1.DataBind()
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
            pnrds.Clear()
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


            'Dim trip As String = IIf(Session("User_Type") = "EXEC", IIf(Session("TripExec") IsNot Nothing, Session("TripExec").ToString().Trim(), ""), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))

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
            Dim distrHold As String = ""
            If UserType.Trim().ToUpper() = "DI" Then
                distrHold = UserID
            End If
            pnrds = Distr.GetHoldPnrReportNew(UserID, UserType, trip, FromDate, ToDate, OrderID, PNR, Air, PaxName, TicketNo, AgentID, ExecID, distrHold, "")
            'STDom.ExportData(pnrds)
            Dim StrDataTable As New DataTable
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then
                StrDataTable = pnrds.Tables(0)
                StrDataTable.Columns.Remove("FareRule")
                STDom.ExportData(pnrds)

            Else

                If pnrds.Tables(0).Columns.Contains("PartnerName") = True Then

                    StrDataTable = pnrds.Tables(0)
                    StrDataTable.Columns.Remove("PartnerName")
                    StrDataTable.Columns.Remove("FareRule")
                    'StrDataTable.Columns.Remove("PaymentMode")
                    'StrDataTable.Columns.Remove("PgCharges")
                    'StrDataTable.Columns.Remove("PaymentMode1")
                    'StrDataTable.Columns.Remove("PgTid")
                    'StrDataTable.Columns.Remove("PgCharges1")
                    STDom.ExportData(pnrds)
                End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Dim lblpaymentmode As Label
    '    Dim lblPGCharges As Label

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        lblpaymentmode = e.Row.FindControl("lbl_Payment")
    '        lblPGCharges = e.Row.FindControl("lbl_PGCharges")
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
