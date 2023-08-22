Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class Reports_Accounts_LedgerSingleOrderID
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
                'tr_Agency.Visible = False
                tr_AgencyName.Visible = False
                tr_UploadType.Visible = False
                tr_Cat.Visible = False
            End If
            If Session("User_Type") <> "ACC" Then
                tr_UploadType.Visible = False
                tr_Cat.Visible = False
                tr_BookingType.Visible = True
            End If
            If (Session("TypeID").ToString() = "AD2") Then
                tr_BookingType.Visible = True
            End If
            If Session("User_Type") = "SALES" Then
                tr_UploadType.Visible = False
                tr_Cat.Visible = False
            End If
            If Session("User_Type") = "DI" Then
                tr_SearchType.Visible = True
                tr_BookingType.Visible = True
            End If

            If Not IsPostBack Then

                ddl_BookingType.DataSource = GetDistinctBookingType()
                ddl_BookingType.DataTextField = "BookingType"
                ddl_BookingType.DataValueField = "BookingType"
                ddl_BookingType.DataBind()

                If (Session("user_type") = "SALES") Then
                    'Dim dtag As New DataTable
                    'dtag = STDom.getAgencybySalesRef(Session("UID").ToString).Tables(0)
                    'ddl_AgencyName.AppendDataBoundItems = True
                    'ddl_AgencyName.Items.Clear()
                    'ddl_AgencyName.Items.Insert(0, "--Select Agency--")
                    'ddl_AgencyName.DataSource = dtag
                    'ddl_AgencyName.DataTextField = "Agency_Name"
                    'ddl_AgencyName.DataValueField = "user_id"
                    'ddl_AgencyName.DataBind()
                Else
                    Dim DTUT As New DataTable
                    DTUT = STDom.GetUploadType().Tables(0)
                    RBL_Type.DataSource = DTUT
                    RBL_Type.DataTextField = "UploadCategoryText"
                    RBL_Type.DataValueField = "UploadCategory"
                    RBL_Type.DataBind()
                    RBL_Type.SelectedIndex = 0
                    Dim DTUC As New DataTable
                    DTUC = STDom.GetCategory("CA").Tables(0)
                    If DTUC.Rows.Count > 0 Then
                        ddl_Category.AppendDataBoundItems = True
                        ddl_Category.Items.Clear()
                        ddl_Category.Items.Insert(0, "--Select Category--")
                        ddl_Category.DataSource = DTUC
                        ddl_Category.DataTextField = "SubCategory"
                        ddl_Category.DataValueField = "GroupType"
                        ddl_Category.DataBind()

                        ' Dim agdt As New DataTable
                        'agdt = ST.GetAgencyDetailsDDL().Tables(0)
                        'ddl_AgencyName.AppendDataBoundItems = True
                        'ddl_AgencyName.Items.Clear()
                        'ddl_AgencyName.Items.Insert(0, "--Select Agency--")
                        'ddl_AgencyName.DataSource = agdt
                        'ddl_AgencyName.DataTextField = "Agency_Name"
                        'ddl_AgencyName.DataValueField = "user_id"
                        'ddl_AgencyName.DataBind()

                    End If



                    'Dim DTBType As New DataTable
                    'DTBType = STDom.GetLedgerBookingType().Tables(0)
                    'ddl_BookingType.AppendDataBoundItems = True
                    'ddl_BookingType.Items.Clear()
                    'ddl_BookingType.Items.Insert(0, "--Booking Type--")
                    'ddl_BookingType.DataSource = DTBType
                    'ddl_BookingType.DataTextField = "BookingType"
                    'ddl_BookingType.DataValueField = "BookingType"
                    'ddl_BookingType.DataBind()
                    'Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                    'Dim curr_date1 = Now()
                    'Dim AgentID As String
                    'If ddl_AgencyName.SelectedItem.Text = "--Select Agency--" Then
                    '    AgentID = Nothing
                    'Else
                    '    AgentID = ddl_AgencyName.SelectedValue
                    'End If
                    'Dim dtsaerch As New DataTable
                    'dtsaerch = STDom.GetLedgerDetails(Session("User_Type"), Session("UID").ToString, curr_date, curr_date1, AgentID).Tables(0)
                    'Grid_Ledger.DataSource = dtsaerch
                    'Grid_Ledger.DataBind()
                    'Attribute to show the Plus Minus Button.
                    Grid_Ledger.HeaderRow.Cells(0).Attributes("data-class") = "expand"

                    'Attribute to hide column in Phone.
                    Grid_Ledger.HeaderRow.Cells(2).Attributes("data-hide") = "phone"
                    Grid_Ledger.HeaderRow.Cells(3).Attributes("data-hide") = "phone"

                    'Adds THEAD and TBODY to GridView.
                    Grid_Ledger.HeaderRow.TableSection = TableRowSection.TableHeader

                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub RBL_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL_Type.SelectedIndexChanged
        Try
            Dim DTUC As New DataTable
            DTUC = STDom.GetCategory(RBL_Type.SelectedValue).Tables(0)
            If DTUC.Rows.Count > 0 Then
                ddl_Category.AppendDataBoundItems = True
                ddl_Category.Items.Clear()
                ddl_Category.Items.Insert(0, "--Select Category--")
                ddl_Category.DataSource = DTUC
                ddl_Category.DataTextField = "SubCategory"
                ddl_Category.DataValueField = "GroupType"
                ddl_Category.DataBind()
                'BindGrid()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Function GetDistinctBookingType() As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable()
        Try

            con.Open()
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_Get_Distinct_BookingTypeFromLedger"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            con.Close()


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()

        End Try
        Return dt
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

            Dim Amount As String = txtAmount.Text
            Dim OrderNo As String = txtOrderNo.Text
            Dim AirCode As String = txtAirCode.Text

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim BookingType As String = ""
            If (ddl_BookingType.SelectedIndex > 0) Then
                BookingType = ddl_BookingType.SelectedValue
            End If
            Dim SearchType As String = ""
            If (RB_Agent.Checked = True AndAlso Session("User_Type") = "DI") Then
                SearchType = RB_Agent.Text
            Else
                SearchType = RB_Distr.Text
            End If
            Dim dtsaerch As New DataTable

            dtsaerch = STDom.GetLedgerDetailsSingleOrderID(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, SearchType, Amount, OrderNo, AirCode).Tables(0)
            Dim dtClosBal As New DataTable
            dtClosBal = STDom.GetLedgerDetailsSingleOrderID(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, "CLOSINGBAL", Amount, OrderNo, AirCode).Tables(0)
            Dim ClosingBal As String = ""
            If dtClosBal.Rows.Count > 0 Then
                ClosingBal = dtClosBal.Rows(0)("ClosingBalance")
                lblTitle.Text = "Closing Balance:"
                lblClosingBal.Text = Convert.ToDouble(ClosingBal).ToString("0.00")
            End If

            If (dtsaerch Is Nothing OrElse dtsaerch.Rows.Count = 0) Then
                dtsaerch = dtClosBal
                'Else
                '    SearchType = RB_Distr.Text
            End If

            ViewState("dtsaerch") = dtsaerch
            Grid_Ledger.DataSource = dtsaerch
            Grid_Ledger.DataBind()

            txtAmount.Text = ""
            txtOrderNo.Text = ""
            txtAirCode.Text = ""
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

            Dim Amount As String = txtAmount.Text
            Dim OrderNo As String = txtOrderNo.Text
            Dim AirCode As String = txtAirCode.Text

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim BookingType As String = ""
            If (ddl_BookingType.SelectedIndex > 0) Then
                BookingType = ddl_BookingType.SelectedValue
            End If
            Dim SearchType As String = ""
            If (RB_Agent.Checked = True AndAlso Session("User_Type") = "DI") Then
                SearchType = RB_Agent.Text
            Else
                SearchType = RB_Distr.Text
            End If
            Dim dtsaerch As New DataSet
            Dim StrDataTable As New DataTable
            dtsaerch = STDom.GetLedgerDetailsSingleOrderID(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, AgentID, BookingType, SearchType, Amount, OrderNo, AirCode)
            'STDom.ExportData(dtsaerch)
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then
                StrDataTable = dtsaerch.Tables(0)
            Else
                If dtsaerch.Tables(0).Columns.Contains("C") = True Then
                    StrDataTable = dtsaerch.Tables(0)
                    'StrDataTable.Columns.Remove("TicketingCarrier")
                    StrDataTable.Columns.Remove("YatraAccountID")
                    StrDataTable.Columns.Remove("AccountID")
                    StrDataTable.Columns.Remove("ExecutiveID")
                    'StrDataTable.Columns.Remove("Aval_Balance")
                    StrDataTable.Columns.Remove("C")
                    StrDataTable.Columns.Remove("D")
                    StrDataTable.Columns.Remove("PaymentMode")
                    StrDataTable.Columns.Remove("Link")
                    StrDataTable.Columns.Remove("InvoiceLink")
                    'StrDataTable.Columns.Remove("Aircode")
                    'StrDataTable.Columns.Remove("TicketNo")
                    'StrDataTable.Columns.Remove("DueAmount")
                    'StrDataTable.Columns.Remove("CreditLimit")
                    STDom.ExportData(dtsaerch)
                End If
            End If

            txtAmount.Text = ""
            txtOrderNo.Text = ""
            txtAirCode.Text = ""
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

End Class

