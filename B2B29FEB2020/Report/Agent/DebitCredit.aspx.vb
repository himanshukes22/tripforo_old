Imports System.Data
Imports System.Data.SqlClient
Imports YatraBilling
Partial Class SprReports_Agent_DebitCredit
    Inherits System.Web.UI.Page

    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Dim AgentType As String
    Dim series As New SeriesDepart
    Private Distr As New Distributor
    Private objSql As New SqlTransactionNew
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            'If (Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "DI" Then
            If (Session("UID") = "" Or Session("UID") Is Nothing) Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                Dim StaffUserId As String = Request("UserId")
                Dim StaffId As String = Request("ID")
                Dim Dt As New DataTable
                Dim count As String
                count = ""
                Dim rec As String
                Dim crdt As DataTable
                'crdt = CheckCRRecord(agent, count)
                rec = ""
                'If crdt.Rows.Count > 0 Then
                '    rec = "Todays Upload \n"
                '    For i As Integer = 0 To crdt.Rows.Count - 1

                '        Dim struptype As String = ""
                '        If (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CA") Then
                '            struptype = crdt.Rows(i)("UploadType").ToString() + " (Cash)"
                '        ElseIf (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CR") Then
                '            struptype = crdt.Rows(i)("UploadType").ToString() + " (Credit)"
                '        ElseIf (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CC") Then
                '            struptype = crdt.Rows(i)("UploadType").ToString() + " (CreditCard)"
                '        End If

                '        rec = rec + (i + 1).ToString() + "." + struptype + "-" + " INR. " + crdt.Rows(i)("Credit").ToString() + "\n"

                '    Next


                'End If

                plus.Attributes.Add("OnClick", "return checkCreditTrasac('" + count + "','" + rec + "')")
                Dt = GetStaffDetailsById(StaffUserId, StaffId)
                'Select Case StaffId, UserId, Password, OwnerId, AgencyId, Name, Mobile, Email, Address, CreditLimit, CreditLimitTrnsDate, StaffLimit, StaffLimitTrnsDate, UserType, 
                '         RoleType, Status, Flight, Hotel, Bus, Rail, Cab, Holidays, GiftCard, HomeStay, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DelStatus, DeletedBy,
                '         DeletedDate, CheckBalance
                If Dt.Rows.Count > 0 Then
                    td_AgentID.InnerText = Convert.ToString(Dt.Rows(0)("UserId"))
                    td_AgencyName.InnerText = Convert.ToString(Dt.Rows(0)("Name"))
                    td_Address.InnerText = Convert.ToString(Dt.Rows(0)("Address"))
                    td_Address1.InnerText = ""
                    td_Aval_Bal.InnerText = Convert.ToString(Dt.Rows(0)("CreditLimit"))
                    td_Email.InnerText = Convert.ToString(Dt.Rows(0)("Email"))
                    td_Mobile.InnerText = Convert.ToString(Dt.Rows(0)("Mobile"))
                    td_pan.InnerText = ""
                    'AgentType = Dt.Rows(0)("Agent_Type").ToString
                    Label1.Text = Convert.ToString(Dt.Rows(0)("UserType")) '
                    HiddAgentAgencyId.Value = Convert.ToString(Dt.Rows(0)("AgencyId"))
                    TdAgencyId.InnerHtml = Convert.ToString(Dt.Rows(0)("AgencyId"))
                    HiddenStaffUserId.Value = Convert.ToString(Dt.Rows(0)("UserId"))
                    HiddenStaffId.Value = Convert.ToString(Dt.Rows(0)("StaffId"))
                    minus.Visible = True
                    plus.Visible = True

                    'If (Session("TypeID") = "AC1") Then
                    '    td_spl.Visible = True
                    'Else
                    '    td_spl.Visible = False
                    'End If
                Else
                    minus.Visible = False
                    plus.Visible = False

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub plus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles plus.Click

        Try
            If (Session("UID") = "" Or Session("UID") Is Nothing) Then
                Response.Redirect("~/Login.aspx")
            End If
            If txt_crd_val.Text.Trim = "" Or txt_crd_val.Text.Trim Is Nothing Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter amount');", True)
                Return
            Else
                If (Convert.ToDouble(txt_crd_val.Text.Trim) > 0) Then
                    Try
                        Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                        Dim InvoiceNo As String = "SFT" + ReferenceNo.Substring(4, 16)
                        Dim IP As String = Request.UserHostAddress
                        'Balance Check and deduct and Transaction Log - Staff Login
                        Dim DebitSataus As String = ""
                        Dim CreditSataus As String = ""
                        Dim CheckBalance As String = ""
                        Dim AgentStatus As String = ""
                        Dim StaffBalCheck As String = ""
                        Dim StaffBalCheckStatus As String = ""
                        Dim CurrentTotAmt As String = ""
                        Dim TransAmount As String = ""
                        Dim BookTicket As String = "true"
                        Try
                            Dim BoookingByStaff As String = "True"
                            Dim sOrderId As String = InvoiceNo
                            Dim sTransAmount As String = txt_crd_val.Text.Trim
                            Dim sStaffUserId As String = HiddenStaffUserId.Value
                            Dim sOwnerId As String = Session("UID")
                            Dim sIPAddress As String = Request.UserHostAddress
                            Dim sRemark As String = txt_rm.Text + "_AgencyId_" + HiddAgentAgencyId.Value + "_To_StaffUserId_" + HiddenStaffUserId.Value + "_Amount_" + Convert.ToString(sTransAmount) + "_RefNo_" + sOrderId
                            Dim sCreatedBy As String = Session("StaffUserId")
                            Dim ModuleType As String = "FUNDTRANSFER"
                            Dim sServiceType As String = "UPLOAD"
                            Dim DebitCredit As String = "CREDIT"
                            Dim ActionType As String = "DEBITCREDIT"
                            Dim flag As Integer = 0
                            Dim objSqlDom As New SqlTransactionDom
                            flag = objSqlDom.StaffTransaction(sOrderId, sServiceType, sTransAmount, sStaffUserId, sOwnerId, sIPAddress, sRemark, sCreatedBy, DebitCredit, ModuleType, ActionType)
                            If (flag > 0) Then
                                tbl_Upload.Visible = False
                                td_msg.Visible = True
                                td_msg.InnerText = "Amount credited sucessfully ."
                                'td_msg.InnerText = "Amount credited to staff account sucessfully . " & td_AgencyName.InnerText & "( " & Request("AgentID") & " ) Available Balance " & CurrentAval_Bal & " INR. " & Environment.NewLine & " Amount debited to your account sucessfully .Available Balance " & Distr_CurrentAval_Bal & " INR"
                            End If
                        Catch ex As Exception

                        End Try
                        'END: Balance Check and deduct and Transaction Log - Staff
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                        tbl_Upload.Visible = False
                        td_msg.Visible = True
                        td_msg.InnerText = "Please check your balance."
                    End Try
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter valid amount');", True)
                    Return
                End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            tbl_Upload.Visible = False
            td_msg.Visible = True
            td_msg.InnerText = "Please try again"

        End Try
    End Sub
    Protected Sub minus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles minus.Click
        Try
            If (Session("UID") = "" Or Session("UID") Is Nothing) Then
                Response.Redirect("~/Login.aspx")
            End If
            If txt_crd_val.Text.Trim = "" Or txt_crd_val.Text.Trim Is Nothing Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter amount');", True)
                Return
            Else
                If (Convert.ToDouble(txt_crd_val.Text.Trim) > 0) Then
                    Try
                        Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                        Dim InvoiceNo As String = "SFT" + ReferenceNo.Substring(4, 16)
                        Dim IP As String = Request.UserHostAddress
                        'Balance Check and deduct and Transaction Log - Staff Login
                        Dim DebitSataus As String = ""
                        Dim CreditSataus As String = ""
                        Dim CheckBalance As String = ""
                        Dim AgentStatus As String = ""
                        Dim StaffBalCheck As String = ""
                        Dim StaffBalCheckStatus As String = ""
                        Dim CurrentTotAmt As String = ""
                        Dim TransAmount As String = ""
                        Dim BookTicket As String = "true"
                        Try
                            Dim BoookingByStaff As String = "True"
                            Dim sOrderId As String = InvoiceNo
                            Dim sTransAmount As String = txt_crd_val.Text.Trim
                            Dim sStaffUserId As String = HiddenStaffUserId.Value
                            Dim sOwnerId As String = Session("UID")
                            Dim sIPAddress As String = Request.UserHostAddress
                            Dim sRemark As String = txt_rm.Text + "_AgencyId_" + HiddAgentAgencyId.Value + "_To_StaffUserId_" + HiddenStaffUserId.Value + "_Amount_" + Convert.ToString(sTransAmount) + "_RefNo_" + sOrderId
                            Dim sCreatedBy As String = Session("StaffUserId")
                            Dim ModuleType As String = "FUNDREVERSAL"
                            Dim sServiceType As String = "UPLOAD"
                            Dim DebitCredit As String = "DEBIT"
                            Dim ActionType As String = "DEBITCREDIT"
                            Dim flag As Integer = 0
                            Dim objSqlDom As New SqlTransactionDom
                            flag = objSqlDom.StaffTransaction(sOrderId, sServiceType, sTransAmount, sStaffUserId, sOwnerId, sIPAddress, sRemark, sCreatedBy, DebitCredit, ModuleType, ActionType)
                            If (flag > 0) Then
                                tbl_Upload.Visible = False
                                td_msg.Visible = True
                                td_msg.InnerText = "Amount debited sucessfully ."
                                'td_msg.InnerText = "Amount credited to staff account sucessfully . " & td_AgencyName.InnerText & "( " & Request("AgentID") & " ) Available Balance " & CurrentAval_Bal & " INR. " & Environment.NewLine & " Amount debited to your account sucessfully .Available Balance " & Distr_CurrentAval_Bal & " INR"
                            End If
                        Catch ex As Exception

                        End Try
                        'END: Balance Check and deduct and Transaction Log - Staff
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                        tbl_Upload.Visible = False
                        td_msg.Visible = True
                        td_msg.InnerText = "Please check your balance."
                    End Try
                Else
                    'ShowAlertMessage("Amount is greater than requested amount")
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter valid amount');", True)
                    Return
                End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            tbl_Upload.Visible = False
            td_msg.Visible = True
            td_msg.InnerText = "Please try again"

        End Try
    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Public Function GetStaffDetailsById(ByVal StaffUserId As String, ByVal StaffId As String) As DataTable
        Dim con As New SqlConnection
        Dim adp As SqlDataAdapter
        Dim dt As New DataTable
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            adp = New SqlDataAdapter("SP_StaffAmountDebitCredit", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@StaffUserId", StaffUserId)
            adp.SelectCommand.Parameters.AddWithValue("@AgentStaffId", StaffId)
            adp.SelectCommand.Parameters.AddWithValue("@OwnerId", Session("UID"))
            adp.SelectCommand.Parameters.AddWithValue("@ServiceType", "FUNDTRANSFER")
            adp.SelectCommand.Parameters.AddWithValue("@Module", "UPLOAD")
            adp.SelectCommand.Parameters.AddWithValue("@ActionType", "GETDETAILS")
            'adp.SelectCommand.Parameters.Add("@Count", SqlDbType.Int)
            'adp.SelectCommand.Parameters("@Count").Direction = ParameterDirection.Output
            adp.Fill(dt)
            con.Close()
            adp.Dispose()
            ''count = adp.SelectCommand.Parameters("@Count").Value.ToString()
        Catch ex As Exception
            con.Close()

        End Try


        Return dt

    End Function
End Class

