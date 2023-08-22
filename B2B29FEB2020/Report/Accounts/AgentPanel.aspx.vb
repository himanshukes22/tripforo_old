Imports System
Imports System.Collections.Generic

Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Web.Security
Imports System.IO

Partial Class Reports_Accounts_AgentPanel
    Inherits System.Web.UI.Page

    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Private Distr As New Distributor
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not Me.IsPostBack Then
            Me.BindGrid()
        End If
        Try
            If Not IsPostBack Then
                Dim DtAg As New DataTable
                DtAg = ST.GetAgencyDetails(Session("UID")).Tables(0)

                'Dim ds As New DataSet()
                'ds = upload.GetAgency(id)


                ddl_modepayment.AppendDataBoundItems = True
                ddl_modepayment.Items.Clear()
                ddl_modepayment.Items.Insert(0, "Select Payment Mode")
                ddl_modepayment.Items.Insert(1, "Cash")
                ddl_modepayment.Items.Insert(2, "Cash Deposite In Bank")
                ddl_modepayment.Items.Insert(3, "Cheque")
                ddl_modepayment.Items.Insert(4, "Online Banking")
                'ddl_modepayment.Items.Insert(5, "RTGS")
                Dim DtBank As New DataTable
                DtBank = STDom.BankInformation(Session("UID")).Tables(0)
                ddl_BankName.AppendDataBoundItems = True
                ddl_BankName.Items.Clear()
                ddl_BankName.Items.Insert(0, "--Select Bank--")
                ddl_BankName.DataSource = DtBank
                ddl_BankName.DataTextField = "BankName"
                ddl_BankName.DataValueField = "BankName"
                ddl_BankName.DataBind()

                Dim DtOffice As New DataTable
                DtOffice = Distr.GetDepositeOffice(Session("UID")).Tables(0)
                ddl_Office.AppendDataBoundItems = True
                ddl_Office.Items.Clear()
                ddl_Office.Items.Insert(0, "--Select Office--")
                ddl_Office.DataSource = DtOffice
                ddl_Office.DataTextField = "OFFICE"
                ddl_Office.DataValueField = "OFFICE"
                ddl_Office.DataBind()
            End If
            Try
                Dim dtdist As New DataTable()
                dtdist = STDom.GetAgencyDetails(Session("UID")).Tables(0)
                Dim DistrId = dtdist.Rows(0)("Distr").ToString()
                If (DistrId.ToUpper = "FWU") Then
                    td_UploadType.Visible = True
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub ddl_modepayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_modepayment.SelectedIndexChanged
        Try
            If ddl_modepayment.SelectedItem.Text = "Cheque" Then
                check_info.Visible = True
                td_Bank.Visible = True
                td_Bank1.Visible = True
                td_BACode.Visible = True
                td_BACode1.Visible = True
                td_BCode.Visible = False
                td_BCode1.Visible = False
                td_transid.Visible = False
                td_transid1.Visible = False
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
                tr_Deposite.Visible = False
                tr_conper.Visible = False
                ddl_BankName.SelectedIndex = 0

            End If
            If ddl_modepayment.SelectedItem.Text = "Cash" Then
                tr_Deposite.Visible = True
                tr_conper.Visible = True
                check_info.Visible = False
                td_Bank.Visible = False
                td_Bank1.Visible = False
                td_BACode.Visible = False
                td_BACode1.Visible = False

                td_BCode.Visible = False
                td_BCode1.Visible = False
                td_transid.Visible = False
                td_transid1.Visible = False
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
                ddl_BankName.SelectedIndex = 0

            End If
            If ddl_modepayment.SelectedItem.Text = "Select Payment Mode" Then
                tr_Deposite.Visible = False
                tr_conper.Visible = False
                check_info.Visible = False
                td_Bank.Visible = False
                td_Bank1.Visible = False
                td_BACode.Visible = False
                td_BACode1.Visible = False
                td_BCode.Visible = False
                td_BCode1.Visible = False
                td_transid.Visible = False
                td_transid1.Visible = False
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
                ddl_BankName.SelectedIndex = 0
            End If
            If ddl_modepayment.SelectedItem.Text = "Cash Deposite In Bank" Then
                td_BCode.Visible = True
                td_BCode1.Visible = True
                td_Bank.Visible = True
                td_Bank1.Visible = True
                td_BACode.Visible = True
                td_BACode1.Visible = True
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
                check_info.Visible = False
                tr_Deposite.Visible = False
                tr_conper.Visible = False
                td_transid.Visible = False
                td_transid1.Visible = False
                ddl_BankName.SelectedIndex = 0

            End If

            If ddl_modepayment.SelectedItem.Text = "NetBanking" Or ddl_modepayment.SelectedItem.Text = "RTGS" Then
                td_transid.Visible = True
                td_transid1.Visible = True
                td_Bank.Visible = True
                td_Bank1.Visible = True
                td_BACode.Visible = True
                td_BACode1.Visible = True
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
                check_info.Visible = False
                tr_Deposite.Visible = False
                tr_conper.Visible = False
                td_BCode.Visible = False
                td_BCode1.Visible = False
                ddl_BankName.SelectedIndex = 0

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_submitt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_submitt.Click
        Try
            Dim OTPRefNo As String = ""
            Dim LoginByOTP As String = ""
            Dim OTPId As String = ""
            Dim OTP As String = ""
            Dim OTPCreatedBy As String = ""
            Try
                If Session("LoginByOTP") IsNot Nothing AndAlso Convert.ToString(Session("LoginByOTP")) <> "" AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                    'If String.IsNullOrEmpty(Session("LoginByOTP")) AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                    OTPRefNo = "OTP" + DateTime.Now.ToString("yyyyMMddHHmmssffffff").Substring(7, 13)
                    LoginByOTP = Session("LoginByOTP")
                    OTPId = Session("OTPID")
                    OTP = Session("OTP")
                    OTPCreatedBy = Session("OTPCreatedBy")
                    'OTPRefNo, LoginByOTP, OTPId
                End If
            Catch ex As Exception
                'clsErrorLog.LogInfo(ex)
                EXCEPTION_LOG.ErrorLog.FileHandling("UploadRequest", "Error_102", ex, " SprReports\Accounts\AgentPanel.aspx.vb-btn_submitt_Click")
            End Try

            Dim file_pan As String = ""
            Dim len_pan As Integer = fluPdfUpload.PostedFile.ContentLength
            If len_pan > 0 Then
                Dim finfo_pan As New FileInfo(fluPdfUpload.FileName)
                Dim fileExtension_pan As String = finfo_pan.Extension.ToLower()
                If fileExtension_pan = ".jpg" Or fileExtension_pan = ".pdf" Then
                    Dim g As Guid = Guid.NewGuid()


                    If fluPdfUpload.HasFile = True Then
                        Dim filepath_pan As String = Server.MapPath("/CreditAmtFile/" + g.ToString())
                        filepath_pan = filepath_pan + fileExtension_pan
                        fluPdfUpload.SaveAs(filepath_pan.ToString())
                        file_pan = Path.GetFileName(g.ToString() + fileExtension_pan)
						
						Dim kkpath As String = "C:\\WEBSITES\\Tripforo\\Admin\\New-Admin\\localhost_56359\\CreditAmtFile\\" & file_pan.ToString()
                        fluPdfUpload.SaveAs(kkpath.ToString())
                    End If
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please upload only pdf attachment');", True)
                End If
            End If

            Dim DtAg As New DataTable
            DtAg = ST.GetAgencyDetails(Session("UID")).Tables(0)
            Dim AgencyName As String = DtAg.Rows(0)("Agency_Name").ToString
            Dim SalesExecID As String = DtAg.Rows(0)("SalesExecID").ToString
            Dim AgentType As String = DtAg.Rows(0)("Agent_Type").ToString
            Dim pendingStatus As String = If(RBL_UploadType.SelectedValue = "AD", "ADPending", "Pending")

            txt_areacode.Text = ""
            If ddl_modepayment.SelectedValue = "Cash" Then
                STDom.insertDeposite(AgencyName, Session("UID").ToString, txt_amount.Text.Trim, ddl_modepayment.SelectedValue, "", "", "", "", "", "", TxtRefNo.Text.Trim, txt_city.Text.Trim, txt_depositedate.Text.Trim, txt_remark.Text.Trim, pendingStatus, RBL_UploadType.SelectedValue, ddl_Office.SelectedValue, txt_concernperson.Text.Trim, txt_ReceiptNo.Text.Trim, "", "", SalesExecID, AgentType, OTPRefNo, LoginByOTP, OTPId, file_pan)
            End If
            If ddl_modepayment.SelectedValue = "Cash Deposite In Bank" Then
                STDom.insertDeposite(AgencyName, Session("UID").ToString, txt_amount.Text.Trim, ddl_modepayment.SelectedValue, ddl_BankName.SelectedValue, ddl_Banch.SelectedValue, ddl_Account.SelectedValue, "", "", "", TxtRefNo.Text.Trim, "", "", txt_remark.Text.Trim, pendingStatus, RBL_UploadType.SelectedValue, "", "", "", txt_BranchCode.Text.Trim, "", SalesExecID, AgentType, OTPRefNo, LoginByOTP, OTPId, file_pan)
            End If
            If ddl_modepayment.SelectedValue = "Cheque" Then
                STDom.insertDeposite(AgencyName, Session("UID").ToString, txt_amount.Text.Trim, ddl_modepayment.SelectedValue, ddl_BankName.SelectedValue, ddl_Banch.SelectedValue, ddl_Account.SelectedValue, txt_chequeno.Text.Trim, txt_chequedate.Text.Trim, "", TxtRefNo.Text.Trim, "", "", txt_remark.Text.Trim, pendingStatus, RBL_UploadType.SelectedValue, "", "", "", "", txt_BankName.Text.Trim, SalesExecID, AgentType, OTPRefNo, LoginByOTP, OTPId, file_pan)
            End If
            If ddl_modepayment.SelectedValue = "Online Banking" Or ddl_modepayment.SelectedValue = "RTGS" Then
                STDom.insertDeposite(AgencyName, Session("UID").ToString, txt_amount.Text.Trim, ddl_modepayment.SelectedValue, "", "", "", "", "", txt_tranid.Text.Trim, TxtRefNo.Text.Trim, "", "", txt_remark.Text.Trim, pendingStatus, "", "", "", "", "", "", "", AgentType, OTPRefNo, LoginByOTP, OTPId, file_pan)
            End If


            ddl_modepayment.SelectedIndex = 0
            txt_amount.Text = ""
            TxtRefNo.Text = ""
            tr_Deposite.Visible = False
            tr_conper.Visible = False
            check_info.Visible = False
            td_Bank.Visible = False
            td_Bank1.Visible = False
            td_BACode.Visible = False
            td_BACode1.Visible = False
            td_BCode.Visible = False
            td_BCode1.Visible = False
            td_transid.Visible = False
            td_transid1.Visible = False
            td_BranchAcc.Visible = False
            div_Bankinfo.Visible = False
            ddl_BankName.SelectedIndex = 0
            txt_remark.Text = ""
            'Dim curr_date2 = Now.Date() & " " & "12:00:00 AM"
            'Dim curr_date3 = Now()
            'grd_deposit.DataSource = STDom.GetDepositDetails(curr_date2, curr_date3, Session("UID"))
            'grd_deposit.DataBind()
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Upload Request Sent Sucessfully');", True)


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub ResetAll()
        txt_amount.Text = ""
        ddl_BankName.SelectedIndex = 0
        ddl_Banch.SelectedIndex = 0
        ddl_Account.SelectedIndex = 0
        txt_chequeno.Text = ""
        txt_chequedate.Text = ""
        txt_tranid.Text = ""
        txt_areacode.Text = ""
        txt_city.Text = ""
        txt_remark.Text = ""
    End Sub

    Public Sub sendmail()
        Try
            Dim DtAg As New DataTable
            DtAg = ST.GetAgencyDetails(Session("UID")).Tables(0)
            Dim AgencyName As String = DtAg.Rows(0)("Agency_Name").ToString
            Dim mytable As String = ""
            mytable += "<table cellspacing='2' cellpadding='2' width='90%' border='1' border-width='thin'>"
            mytable += "<tr>"
            mytable += "<td>"
            mytable += "Agency Name:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + AgencyName + ""
            mytable += "</td>"
            mytable += "<td>"
            mytable += "Agency ID:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + Session("UID").ToString + ""
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "<tr>"
            mytable += "<td>"
            mytable += "Amount:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_amount.Text + ""
            mytable += "</td>"
            mytable += "<td>"
            mytable += "Mode Of Payment:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + ddl_modepayment.SelectedItem.Text + ""
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "<tr>"
            mytable += "<td>"
            mytable += "Bank Name:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + ddl_BankName.SelectedValue + ""
            mytable += "</td>"
            mytable += "<td>"
            mytable += "Cheque No:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_chequeno.Text + ""
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "<tr>"
            mytable += "<td>"
            mytable += "Cheque Date:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_chequedate.Text + ""
            mytable += "</td>"
            mytable += "<td>"
            mytable += "Transaction ID:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_tranid.Text + ""
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "<tr>"
            mytable += "<td>"
            mytable += "Bank AreaCode:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_areacode.Text + ""
            mytable += "</td>"
            mytable += "<td>"
            mytable += "Deposit City:"
            mytable += "</td>"
            mytable += "<td>"
            mytable += "" + txt_city.Text + ""
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "</table>"
            Dim email As New MailMessage()
            email.From = New MailAddress("b2bsalessupport@flywidus.com")
            email.[To].Add("jeet@flywidus.com")
            email.Subject = "Payment Details"
            email.IsBodyHtml = True
            email.Body = mytable
            Dim smtp As New SmtpClient()
            smtp.Host = "203.185.191.71"
            smtp.Port = 25
            smtp.Credentials = New System.Net.NetworkCredential("b2badmin@flywidus.com", "america")
            smtp.Send(email)

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Private Sub BindGrid()
        Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Bank_Accounts")
                cmd.Parameters.AddWithValue("@Action", "SELECT")
                cmd.Parameters.AddWithValue("@BankName", "SELECT")
                cmd.Parameters.AddWithValue("@BranchName", "SELECT")
                cmd.Parameters.AddWithValue("@Area", "SELECT")
                cmd.Parameters.AddWithValue("@AccountNumber", "SELECT")
                cmd.Parameters.AddWithValue("@NEFTCode", "SELECT")
                cmd.Parameters.AddWithValue("@DISTRID", "SELECT")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridView1.DataSource = dt
                        GridView1.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub ddl_BankName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_BankName.SelectedIndexChanged
        Try
            If ddl_BankName.SelectedValue = "--Select Bank--" Then
                td_BranchAcc.Visible = False
                div_Bankinfo.Visible = False
            Else
                td_BranchAcc.Visible = True
                div_Bankinfo.Visible = True
            End If


            td_BACode.Visible = False
            td_BACode1.Visible = False
            txt_areacode.Visible = False

            Dim Branchdt As New DataTable
            Branchdt = STDom.GetBranchAccount(ddl_BankName.SelectedValue, "Branch" & Session("UID")).Tables(0)
            Dim Accdt As New DataTable
            Accdt = STDom.GetBranchAccount(ddl_BankName.SelectedValue, "Acc" & Session("UID")).Tables(0)

            'ddl_Banch.AppendDataBoundItems = True
            'ddl_Banch.Items.Clear()
            'ddl_Banch.Items.Insert(0, "--Select Branch--")
            ddl_Banch.DataSource = Branchdt
            ddl_Banch.DataTextField = "BranchName"
            ddl_Banch.DataValueField = "BranchName"
            ddl_Banch.DataBind()

            'ddl_Account.AppendDataBoundItems = True
            'ddl_Account.Items.Clear()
            'ddl_Account.Items.Insert(0, "--Select Account--")
            ddl_Account.DataSource = Accdt
            ddl_Account.DataTextField = "AccountNumber"
            ddl_Account.DataValueField = "AccountNumber"
            ddl_Account.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
