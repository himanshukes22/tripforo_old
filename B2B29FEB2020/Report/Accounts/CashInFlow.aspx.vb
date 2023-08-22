Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Partial Class Reports_Accounts_CashInFlow
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As New SqlDataAdapter
    Dim id As String
    Dim ds As New DataSet
    Private Inflow As New InFlowDetails()
    Dim agency As String
    Dim upload As String

    Dim todate As String
    Dim total As Double
    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If (Session("User_Type") = "ACC") Then
                grd_CashInflow.Columns(11).Visible = True
                grd_CashInflow.Columns(12).Visible = False
                grd_CashInflow.Columns(13).Visible = True
            Else
                grd_CashInflow.Columns(11).Visible = False
                grd_CashInflow.Columns(12).Visible = False
                grd_CashInflow.Columns(13).Visible = False
            End If
            If (Session("User_Type") = "SALES") Then
                grd_CashInflow.Columns(16).Visible = True
                tr_UploadType.Visible = False
                tr_UploadCategory.Visible = False

            ElseIf (Session("User_Type") = "AGENT") Then
                tr_UploadType.Visible = False
                tr_UploadCategory.Visible = False
                tr_Agency.Visible = False
                tr_AgencyName.Visible = False
                grd_CashInflow.Columns(10).Visible = False
                grd_CashInflow.Columns(9).Visible = False

            End If
            If (Session("User_Type") = "DI") Then
                tr_SearchType.Visible = True
                tr_UploadType.Visible = False
                tr_UploadCategory.Visible = False
                grd_CashInflow.Columns(12).Visible = True
                grd_CashInflow.Columns(1).Visible = False
                grd_CashInflow.Columns(10).Visible = False
                grd_CashInflow.Columns(9).Visible = False
            End If
            'If (Session("User_Type") <> "SALES") Then
            '    grd_CashInflow.Columns(10).Visible = False
            'End If
            ' If Page.IsPostBack Then lbl_Amt.Text = "sdffsdgfsd"
            If Not IsPostBack Then


                If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then

                    If (Session("user_type") = "SALES") Then

                        'Dim dtag As New DataTable
                        'dtag = STDom.getAgencybySalesRef(Session("UID").ToString).Tables(0)
                        'ddl_agency.AppendDataBoundItems = True
                        'ddl_agency.Items.Clear()
                        'ddl_agency.Items.Insert(0, "---ALL---")
                        'ddl_agency.DataSource = dtag
                        'ddl_agency.DataTextField = "Agency_Name"
                        'ddl_agency.DataValueField = "user_id"
                        'ddl_agency.DataBind()
                    Else
                        id = Session("UID").ToString()
                        'ddl_agency.AppendDataBoundItems = True
                        'ddl_agency.Items.Clear()
                        'ddl_agency.Items.Insert(0, "---ALL---")
                        'ddl_agency.DataSource = Inflow.GetAgency()
                        'ddl_agency.DataTextField = "Agency_Name"
                        'ddl_agency.DataValueField = "User_ID"
                        'ddl_agency.DataBind()
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
                            'Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                            'Dim curr_date1 = Now()
                            'Session("fromdate") = curr_date
                            'Session("todate") = curr_date1
                            'Session("agency") = Nothing
                            'Session("uploadtype") = Nothing
                            'Session("flowtype") = ddlInflowtype.SelectedItem.Text
                            'ds = LoadBind(curr_date, curr_date1, Nothing, Nothing, ddlInflowtype.SelectedItem.Text, Session("UID").ToString(), Session("User_Type").ToString())
                            'grd_CashInflow.DataSource = ds
                            'grd_CashInflow.DataBind()

                        End If

                    End If



                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Function LoadBind(ByVal fromdate As String, ByVal todate As String, ByVal agency As String, ByVal uploadtype As String, ByVal flowtype As String, ByVal id As String, ByVal usertype As String, ByVal searchtype As String) As DataSet
        ds.Clear()
        Try
            ds = Inflow.GetCashInflowDetails(fromdate, todate, agency, uploadtype, flowtype, id, usertype, searchtype)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
        Return ds
    End Function
    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Try
            td_SalesRmk.Visible = False
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Nothing
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Nothing
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If


            agency = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))

            If ddl_UploadType.SelectedValue = "Select Type" Then

                upload = Nothing
            Else
                upload = ddl_UploadType.SelectedValue
            End If
            'upload = RBL_Type.SelectedValue
            Dim SearchType As String = ""
            If (RB_Agent.Checked = True AndAlso Session("User_Type") = "DI") Then
                SearchType = RB_Agent.Text
            Else
                SearchType = RB_Distr.Text
            End If
            If (SearchType.Trim.ToUpper = "OWN" AndAlso Session("User_Type") = "DI") Then
                grd_CashInflow.Columns(12).Visible = False
            End If


            ds = LoadBind(FromDate, ToDate, agency, upload, ddlInflowtype.SelectedItem.Text, Session("UID").ToString(), Session("User_Type").ToString(), SearchType)
            If ds.Tables(0).Rows.Count > 0 Then
                grd_CashInflow.DataSource = ds
                grd_CashInflow.DataBind()

                Try
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim amount As Double = Convert.ToDouble(ds.Tables(0).Rows(i)("Amount"))
                        total += amount
                        lbl_amount.Text = "Total Amount : " & Convert.ToString(total)
                    Next
                Catch ex As Exception
                End Try

            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('No Records Found');", True)
                'Response.Write("<script>alert('No Records Found')</script>")
                grd_CashInflow.DataSource = ds
                grd_CashInflow.DataBind()
                lbl_amount.Text = ""
            End If

            Session("fromdate") = FromDate
            Session("todate") = ToDate
            agency = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))
            Session("uploadtype") = upload
            'End If
            Session("flowtype") = ddlInflowtype.SelectedItem.Text
            Session("agency") = agency
            Session("SearchType") = SearchType
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Protected Sub grd_CashInflow_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grd_CashInflow.RowEditing
        Try
            grd_CashInflow.EditIndex = e.NewEditIndex
            grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
            grd_CashInflow.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grd_CashInflow_RowCancelingEdit1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grd_CashInflow.RowCancelingEdit
        Try
            grd_CashInflow.EditIndex = -1
            grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
            grd_CashInflow.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grd_CashInflow_RowUpdating1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grd_CashInflow.RowUpdating
        Try
            Dim accid As String = ""
            accid = grd_CashInflow.DataKeys(e.RowIndex).Value.ToString()
            Dim count As Label = DirectCast(grd_CashInflow.Rows(e.RowIndex).FindControl("lbl_counter"), Label)
            Dim Counter As String = count.Text
            Dim txttype As TextBox = DirectCast(grd_CashInflow.Rows(e.RowIndex).FindControl("txt_updatetype"), TextBox)
            Dim txtremark As TextBox = DirectCast(grd_CashInflow.Rows(e.RowIndex).FindControl("txt_updateremark"), TextBox)
            Dim YtrRcptNo As TextBox = DirectCast(grd_CashInflow.Rows(e.RowIndex).FindControl("txt_YtrRcptNo"), TextBox)
            Inflow.Update(txttype.Text.Trim(), txtremark.Text.Trim(), YtrRcptNo.Text.Trim(), accid, ddlInflowtype.SelectedItem.Text, Convert.ToInt32(Counter))
            grd_CashInflow.EditIndex = -1
            grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
            grd_CashInflow.DataBind()
        Catch ex As Exception

            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grd_CashInflow_PageIndexChanging1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd_CashInflow.PageIndexChanging
        Try
            grd_CashInflow.PageIndex = e.NewPageIndex
            grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
            grd_CashInflow.DataBind()
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
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
   

    Protected Sub btn_Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Export.Click
        Try

            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Nothing
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Nothing
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If


            agency = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))


            If ddl_UploadType.SelectedValue = "Select Type" Then

                upload = Nothing
            Else
                upload = ddl_UploadType.SelectedValue
            End If
            Dim SearchType As String = ""
            If (RB_Agent.Checked = True AndAlso Session("User_Type") = "DI") Then
                SearchType = RB_Agent.Text
            Else
                SearchType = RB_Distr.Text
            End If
            'upload = RBL_Type.SelectedValue
            ds = LoadBind(FromDate, ToDate, agency, upload, ddlInflowtype.SelectedItem.Text, Session("UID").ToString(), Session("User_Type").ToString(), SearchType)
            STDom.ExportData(ds)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    grd_CashInflow.DataSource = ds
            '    grd_CashInflow.DataBind()

            '    'For Each row As GridViewRow In grd_CashInflow.Rows
            '    '    Dim lblamt As Label = DirectCast(row.FindControl("lbl_amt"), Label)
            '    '    Dim amount As Double = Convert.ToDouble(lblamt.Text)
            '    '    total += amount
            '    '    lbl_amount.Text = "Total Amount : " & Convert.ToString(total)

            '    'Next
            'Else
            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('No Records Found');", True)
            '    'Response.Write("<script>alert('No Records Found')</script>")
            '    grd_CashInflow.DataSource = ds
            '    grd_CashInflow.DataBind()
            '    lbl_amount.Text = ""
            'End If

            'Session("fromdate") = FromDate
            'Session("todate") = ToDate
            'agency = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))
            'Session("uploadtype") = upload
            ''End If
            'Session("flowtype") = ddlInflowtype.SelectedItem.Text


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub grd_CashInflow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_CashInflow.RowCommand
        If (e.CommandName = "EditDetail") Then
            Try
                Dim Counter As Integer
                Counter = e.CommandArgument.ToString
                Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                rw.BackColor = System.Drawing.Color.Yellow
                Dim lnk As LinkButton = DirectCast(rw.FindControl("lnk_EditPayment"), LinkButton)
                Dim lblamt As Label = DirectCast(rw.FindControl("lbl_amt"), Label)
                Session("Amt") = lblamt.Text
                Session("Counter") = Counter
                lbl_msg.Text = ""
                td_SalesRmk.Visible = True
                chk_status.Checked = False
                txt_Amt.Text = ""
                txt_DBN.Text = ""
                txt_Rmk.Text = ""
                txt_TC.Text = ""
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "<script type='text/javascript'>openDialog();</script>", False)
                'Dim RowIndex As Integer = gvr.RowIndex
                'Dim totpax As Label = DirectCast(grd_processSeries.Rows(RowIndex).FindControl("lbltotpax"), Label)

            Catch ex As Exception

            End Try
           
            
        End If
        If (e.CommandName = "View") Then
            Try
                Dim dtup As DataTable
                dtup = ST.GetUploadDetailsByCounter(e.CommandArgument.ToString).Tables(0)
                td_Amt.InnerText = dtup.Rows(0)("Amount").ToString
                td_DBN.InnerText = dtup.Rows(0)("DepositeBankName").ToString

                td_DPB.InnerText = dtup.Rows(0)("DebitPortalBalance").ToString
                td_Rmk.InnerText = dtup.Rows(0)("RemarkSales").ToString


                td_TC.InnerText = dtup.Rows(0)("TransactionIDOrChequeNo").ToString
                td_UD.InnerText = dtup.Rows(0)("UpdatedDateSales").ToString
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "<script type='text/javascript'>openDialog();</script>", False)

            Catch ex As Exception

            End Try
            
        End If
    End Sub

   

    Protected Sub grd_CashInflow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_CashInflow.RowDataBound
        Try
    

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim Counter As Label = DirectCast(e.Row.FindControl("lbl_counter"), Label)

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                con.Open()
                Dim cmd As SqlCommand
                cmd = New SqlCommand("SP_GET_ADJUSTMENTDETAILS", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@COUNTER", Convert.ToInt32(Counter.Text.Trim))
                cmd.Parameters.AddWithValue("@OPERATION", "COUNT")
                Dim CNTDTLS As Integer = cmd.ExecuteScalar()
                con.Close()

                If (CNTDTLS = 0) Then
                    Dim rowidadj As LinkButton = DirectCast(e.Row.FindControl("lnk_adjdtl"), LinkButton)
                    rowidadj.Visible = False
                End If

                

                If DataBinder.Eval(e.Row.DataItem, "UploadType").ToString() = "CA" Then
                    Dim rowid As LinkButton = DirectCast(e.Row.FindControl("lnk_EditPayment"), LinkButton)
                    'Dim adjrow As LinkButton = DirectCast(e.Row.FindControl("lnk_adj"), LinkButton)
                    rowid.Visible = False
                    'adjrow.Visible = False
                End If

                If DataBinder.Eval(e.Row.DataItem, "UpdatedDateSales").ToString() <> "" Then
                    Dim rowid As LinkButton = DirectCast(e.Row.FindControl("lnk_EditPayment"), LinkButton)
                    rowid.Text = "(AlreadyUpdated)"
                    rowid.Enabled = False
                    rowid.ForeColor = Drawing.Color.Gray
                    rowid.Font.Underline = False
                    rowid.Font.Bold = False

                End If
                If DataBinder.Eval(e.Row.DataItem, "UploadType").ToString() = "CR" AndAlso DataBinder.Eval(e.Row.DataItem, "UpdatedDateSales").ToString() <> "" Then
                    Dim rowview As LinkButton = DirectCast(e.Row.FindControl("lnk_View"), LinkButton)
                    rowview.Visible = True
                End If

            End If
        Catch ex As Exception

        End Try
        

    End Sub

    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
        Try
            If (Convert.ToInt32(txt_Amt.Text.Trim) < Convert.ToInt32(Session("Amt")) AndAlso chk_status.Checked = False) Then
                lbl_msg.Text = "CheckBox should be checked"
            Else
                Dim ChkStatus As Boolean
                If (chk_status.Checked = True) Then
                    ChkStatus = True
                Else
                    ChkStatus = False
                End If
                ST.UpdateDetailsSales(Session("Counter"), txt_TC.Text.Trim, txt_DBN.Text.Trim, txt_Rmk.Text.Trim, txt_Amt.Text.Trim, ChkStatus)
                grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
                grd_CashInflow.DataBind()
                lbl_msg.Text = ""
                td_SalesRmk.Visible = False
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "<script type='text/javascript'>alert('Details Updated Sucessfully')</script>", False)
            End If
        Catch ex As Exception

        End Try
       
    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            td_SalesRmk.Visible = False
            grd_CashInflow.DataSource = LoadBind(Session("fromdate"), Session("todate"), Session("agency"), Session("uploadtype"), Session("flowtype"), Session("UID").ToString(), Session("User_Type").ToString(), Session("SearchType"))
            grd_CashInflow.DataBind()
        Catch ex As Exception

        End Try

    End Sub
End Class

