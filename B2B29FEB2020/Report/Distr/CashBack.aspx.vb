Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class SprReports_Distr_CashBack
    Inherits System.Web.UI.Page
    Private Distr As New Distributor()
    Private STDom As New SqlTransactionDom()
    Private ST As New SqlTransaction()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim dt As New DataTable()
            dt = Distr.GetDistributorCB(ddl_DistrID.SelectedValue).Tables(0)
            If (dt.Rows(0)("CBMESSAGE").ToString.ToUpper = "ALREADYDONE") Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Cash Back already credited to " & ddl_DistrID.SelectedValue & " for " & Convert.ToDateTime(dt.Rows(0)("CBDATE")).ToString("dd/MM/yyyy") & "');", True)
            Else
                tr_details.Visible = True
                lbl_CBAir.Text = dt.Rows(0)("AIRCB")
                lbl_CBRail.Text = dt.Rows(0)("RAILCB")
                If (lbl_CBAir.Text = "0.00" AndAlso lbl_CBRail.Text = "0.00") Then
                    btn_add.Visible = False
                    tr_rm.Visible = False
                Else
                    btn_add.Visible = True
                    tr_rm.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
        
    End Sub
    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click

        Try
            Dim dt As New DataTable()
            dt = Distr.GetDistributorCB(ddl_DistrID.SelectedValue).Tables(0)
            Dim objSelectedfltCls As New clsInsertSelectedFlight
            Dim CBOID As String = objSelectedfltCls.getRndNum()
            Dim i As Integer = 0
            i = Distr.InsertCBDetails(CBOID, ddl_DistrID.SelectedValue, dt.Rows(0)("AIRTICKET"), dt.Rows(0)("AIRSALE"), dt.Rows(0)("AIRCB"), dt.Rows(0)("AIRCBAMT"), dt.Rows(0)("RAILTICKET"), dt.Rows(0)("RAILSALE"), dt.Rows(0)("RAILCB"), dt.Rows(0)("RAILCBAMT"), dt.Rows(0)("CBDATE"))
            If (i > 0) Then

                'FOR AIR
                If (Convert.ToDouble(lbl_CBAir.Text.Trim) > 0) Then
                    Dim CurrentAval_Bal As Double = 0
                    Dim CheckBalStatus As Boolean = False
                    CurrentAval_Bal = ST.AddCrdLimit(ddl_DistrID.SelectedValue, Convert.ToDouble(lbl_CBAir.Text))

                    Dim dtavl As New DataTable()
                    dtavl = STDom.GetAgencyDetails(ddl_DistrID.SelectedValue).Tables(0)
                    'Check for available balance
                    If (CurrentAval_Bal = 0) Then
                        Dim CurrAvlBal As Double
                        CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
                        If (CurrentAval_Bal <> CurrAvlBal) Then
                            CheckBalStatus = True
                        End If
                    End If
                    'End Check for available balance
                    If (CheckBalStatus = False) Then
                        Dim IP As String = Request.UserHostAddress
                        STDom.insertLedgerDetails(ddl_DistrID.SelectedValue, dtavl.Rows(0)("Agency_Name"), "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, lbl_CBAir.Text.Trim, CurrentAval_Bal, "Credit", txt_rm.Text.Trim, 0)
                        Dim LastAval_Bal As Double = 0
                        LastAval_Bal = CurrentAval_Bal - Convert.ToDouble(lbl_CBAir.Text.Trim)
                        'Dim UploadTypeDt As New DataTable
                        'UploadTypeDt = STDom.GetUploadTypeByType(dtavl.Rows(0)("Agency_Type")).Tables(0)
                        STDom.insertUploadDetails(ddl_DistrID.SelectedValue, dtavl.Rows(0)("Agency_Name"), Session("UID").ToString(), IP, 0, lbl_CBAir.Text.Trim, txt_rm.Text.Trim, "CA", LastAval_Bal, CurrentAval_Bal, "")
                    End If

                End If
                'FOR RAIL
                If (Convert.ToDouble(lbl_CBRail.Text.Trim) > 0) Then
                    Dim CurrentAval_Bal_Rail As Double = 0
                    Dim CheckBalStatus_Rail As Boolean = False
                    CurrentAval_Bal_Rail = ST.AddCrdLimit(ddl_DistrID.SelectedValue, Convert.ToDouble(lbl_CBRail.Text))

                    Dim dtavl_Rail As New DataTable()
                    dtavl_Rail = STDom.GetAgencyDetails(ddl_DistrID.SelectedValue).Tables(0)
                    'Check for available balance
                    If (CurrentAval_Bal_Rail = 0) Then
                        Dim CurrAvlBal_Rail As Double
                        CurrAvlBal_Rail = Convert.ToDouble(dtavl_Rail.Rows(0)("Crd_Limit").ToString)
                        If (CurrentAval_Bal_Rail <> CurrAvlBal_Rail) Then
                            CheckBalStatus_Rail = True
                        End If
                    End If
                    'End Check for available balance
                    If (CheckBalStatus_Rail = False) Then
                        Dim IP As String = Request.UserHostAddress
                        STDom.insertLedgerDetails(ddl_DistrID.SelectedValue, dtavl_Rail.Rows(0)("Agency_Name"), "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, lbl_CBRail.Text.Trim, CurrentAval_Bal_Rail, "Credit", txt_rm.Text.Trim, 0)
                        Dim LastAval_Bal As Double = 0
                        LastAval_Bal = CurrentAval_Bal_Rail - Convert.ToDouble(lbl_CBAir.Text.Trim)
                        'Dim UploadTypeDt As New DataTable
                        'UploadTypeDt = STDom.GetUploadTypeByType(dtavl.Rows(0)("Agency_Type")).Tables(0)
                        STDom.insertUploadDetails(ddl_DistrID.SelectedValue, dtavl_Rail.Rows(0)("Agency_Name"), Session("UID").ToString(), IP, 0, lbl_CBRail.Text.Trim, txt_rm.Text.Trim, "CA", LastAval_Bal, CurrentAval_Bal_Rail, "")

                    End If

                End If

                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Cash back credited to " & ddl_DistrID.SelectedValue & " sucessfully. ');", True)
                tr_details.Visible = False
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to credit cash back.Please try after some time.');", True)
            End If
        Catch ex As Exception

        End Try
       
    End Sub
    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        tr_details.Visible = False
    End Sub
End Class
