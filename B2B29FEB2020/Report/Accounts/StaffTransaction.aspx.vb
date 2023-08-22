Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class Reports_Accounts_StaffTransaction
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type") = "AGENT" Then
                tr_Cat.Visible = False
            End If
            ddl_BookingType.Visible = False
            btn_export.Visible = False

            If Not IsPostBack Then
                Dim ds As New DataSet()
                ds = GetDistinctBookingType()
                'ddl_BookingType.DataSource = ds.Tables(0) 'GetDistinctBookingType()
                'ddl_BookingType.DataTextField = "BookingType"
                'ddl_BookingType.DataValueField = "BookingType"
                'ddl_BookingType.DataBind()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub


    Public Function GetDistinctBookingType() As DataSet
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim ds As New DataSet()
        Dim cmd As New SqlCommand()
        Try
            cmd.CommandText = "GetStaffTransaction"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgentId", Convert.ToString(Session("UID")))
            cmd.Parameters.AddWithValue("@SearchType", "DROPDOWN") '@SearchType='DROPDOWN'
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            con.Close()
            'If con.State = ConnectionState.Closed Then con.Open()
            'Dim da As New SqlDataAdapter(cmd)
            'da.Fill(ds)
            'con.Close()
            'cmd.Dispose()
        Catch ex As Exception
            con.Close()
            cmd.Dispose()
            clsErrorLog.LogInfo(ex)
        End Try
        Return ds
    End Function
    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Dim FromDate1 As String
        Dim ToDate1 As String
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

    Protected Sub Grid_Ledger_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid_Ledger.PageIndexChanging
        Grid_Ledger.PageIndex = e.NewPageIndex
        Grid_Ledger.DataSource = ViewState("dtsaerch")
        Grid_Ledger.DataBind()

    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Dim FromDate1 As String
        Dim ToDate1 As String
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
            Dim BookingType As String = ""
            If (ddl_BookingType.SelectedIndex > 0) Then
                BookingType = ddl_BookingType.SelectedValue
            End If

            Dim dtsaerch As New DataTable
            Dim UserType As String = "AGENT"
            Dim StaffUserId As String = ""
            AgentID = Session("UID")
            Dim sModule As String = ""
            Dim ServiceType As String = ""
            Dim SearchType As String = "GRIDBIND"
            Dim OrderId As String = ""
            Try
                If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true") Then
                    StaffUserId = Session("StaffUserId")
                    UserType = Session("LoginType")
                End If
            Catch ex As Exception

            End Try
            'UserType, StaffUserId , FormDate , ToDate, AgentId, sModule , ServiceType,SearchType ,OrderId
            dtsaerch = STDom.GetStaffTransaction(UserType, StaffUserId, FromDate, ToDate, AgentID, sModule, ServiceType, SearchType, OrderId).Tables(0)
            'dtsaerch = STDom.GetLedgerDetails(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, SearchType).Tables(0)
            'Dim dtClosBal As New DataTable
            'dtClosBal = STDom.GetLedgerDetails(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, "CLOSINGBAL").Tables(0)
            'Dim ClosingBal As String = ""
            'If dtClosBal.Rows.Count > 0 Then
            '    ClosingBal = dtClosBal.Rows(0)("ClosingBalance")
            '    lblTitle.Text = "Closing Balance:"
            '    lblClosingBal.Text = Convert.ToDouble(ClosingBal).ToString("0.00")
            'End If

            'If (dtsaerch Is Nothing OrElse dtsaerch.Rows.Count = 0) Then
            '    dtsaerch = dtClosBal
            '    'Else
            '    '    SearchType = RB_Distr.Text
            'End If

            ViewState("dtsaerch") = dtsaerch
            Grid_Ledger.DataSource = dtsaerch
            Grid_Ledger.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
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
            Dim BookingType As String = ""
            If (ddl_BookingType.SelectedIndex > 0) Then
                BookingType = ddl_BookingType.SelectedValue
            End If
            Dim SearchType As String = ""
            Dim dtsaerch As New DataSet
            Dim StrDataTable As New DataTable
            dtsaerch = STDom.GetLedgerDetails(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, SearchType, "", "", "")
            'STDom.ExportData(dtsaerch)
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then
                StrDataTable = dtsaerch.Tables(0)
            Else
                If dtsaerch.Tables(0).Columns.Contains("C") = True Then
                    StrDataTable = dtsaerch.Tables(0)
                    StrDataTable.Columns.Remove("TicketingCarrier")
                    StrDataTable.Columns.Remove("AccountID")
                    StrDataTable.Columns.Remove("ExecutiveID")
                    StrDataTable.Columns.Remove("Aval_Balance")
                    StrDataTable.Columns.Remove("C")
                    StrDataTable.Columns.Remove("D")
                    StrDataTable.Columns.Remove("PaymentMode")
                    StrDataTable.Columns.Remove("Link")
                    StrDataTable.Columns.Remove("InvoiceLink")
                    STDom.ExportData(dtsaerch)
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

End Class

