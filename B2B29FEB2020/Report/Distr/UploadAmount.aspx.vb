Imports System.Data
Imports System.Data.SqlClient
Imports YatraBilling
Partial Class SprReports_Distr_UploadAmount
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
            If (Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "DI" Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                Dim agent As String = Request("AgentID")
                Dim ID As String = Request("ID")
                Dim Dt As New DataTable
                Dim count As String
                count = ""
                Dim rec As String
                Dim crdt As DataTable
                crdt = CheckCRRecord(agent, count)

                rec = ""
                If crdt.Rows.Count > 0 Then
                    rec = "Todays Upload \n"
                    For i As Integer = 0 To crdt.Rows.Count - 1

                        Dim struptype As String = ""
                        If (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CA") Then
                            struptype = crdt.Rows(i)("UploadType").ToString() + " (Cash)"
                        ElseIf (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CR") Then
                            struptype = crdt.Rows(i)("UploadType").ToString() + " (Credit)"
                        ElseIf (crdt.Rows(i)("UploadType").ToString().ToUpper() = "CC") Then
                            struptype = crdt.Rows(i)("UploadType").ToString() + " (CreditCard)"
                        End If

                        rec = rec + (i + 1).ToString() + "." + struptype + "-" + " INR. " + crdt.Rows(i)("Credit").ToString() + "\n"



                    Next


                End If

                plus.Attributes.Add("OnClick", "return checkCreditTrasac('" + count + "','" + rec + "')")




                Dt = ST.GetAgencyDetails(agent).Tables(0)
                td_AgentID.InnerText = agent
                td_AgencyName.InnerText = Dt.Rows(0)("Agency_Name").ToString
                td_Address.InnerText = Dt.Rows(0)("Address").ToString
                td_Address1.InnerText = Dt.Rows(0)("City").ToString & "," & Dt.Rows(0)("State").ToString & "," & Dt.Rows(0)("Country").ToString
                td_Aval_Bal.InnerText = Dt.Rows(0)("Crd_Limit").ToString
                td_Email.InnerText = Dt.Rows(0)("Email").ToString
                td_Mobile.InnerText = Dt.Rows(0)("Mobile").ToString
                td_pan.InnerText = Dt.Rows(0)("PanNo").ToString
                'AgentType = Dt.Rows(0)("Agent_Type").ToString
                Label1.Text = Dt.Rows(0)("Agent_Type").ToString
                HiddAgentAgencyId.Value = Dt.Rows(0)("AgencyId").ToString
                TdAgencyId.InnerHtml = Dt.Rows(0)("AgencyId").ToString

                txt_crd_val.Text = Request("Amount")
                If ID <> "" Then
                    minus.Visible = False
                    'Else
                    '    plus.Visible = False
                End If
                If Request("Counter") <> "" Then
                    minus.Visible = False
                    plus.Visible = False
                    minus_Series.Visible = True
                    txt_crd_val.Text = Request("Amt")
                    'Else
                    '    plus.Visible = False
                End If
                If (Session("UID").ToString.ToUpper.Trim = "MUJTABA1" AndAlso Session("TypeID") = "AC1") Then
                    td_spl.Visible = True
                Else
                    td_spl.Visible = False
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckCRRecord(ByVal agentId As String, ByRef count As String) As DataTable
        Dim con As New SqlConnection
        Dim adp As SqlDataAdapter
        Dim dt As New DataTable
        Dim cmd As New SqlCommand
        If con.State = ConnectionState.Open Then con.Close()
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString

        'cmd = New SqlCommand("SP_CKECKCRDETAILS", con)
        'cmd.Parameters.AddWithValue("@AGENTID", td_AgentID.InnerText)
        'cmd.Parameters.Add("@Count", SqlDbType.Int)
        'cmd.Parameters(


        adp = New SqlDataAdapter("SP_CKECKCRDETAILS", con)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        adp.SelectCommand.Parameters.AddWithValue("@AGENTID", agentId)
        adp.SelectCommand.Parameters.Add("@Count", SqlDbType.Int)
        adp.SelectCommand.Parameters("@Count").Direction = ParameterDirection.Output
        adp.Fill(dt)

        count = adp.SelectCommand.Parameters("@Count").Value.ToString()

        Return dt

    End Function
    Public Function GetMailingDetails(ByVal department As String) As DataTable
        Dim con As New SqlConnection
        Dim adp As SqlDataAdapter
        Dim dt As New DataTable
        Dim cmd As New SqlCommand
        If con.State = ConnectionState.Open Then con.Close()
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        adp = New SqlDataAdapter("SP_GetMailDetailsBydepartmant", con)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        adp.SelectCommand.Parameters.AddWithValue("@Department", department)
        adp.SelectCommand.Parameters.AddWithValue("@DistrId", Session("UID"))
        adp.Fill(dt)

        Return dt

    End Function
    Public Function GetMailBody(ByVal AgentName As String, ByVal UploadType As String, ByVal Amount As String, ByVal UploadDate As String, ByVal AVLBalance As String, ByVal Regards As String) As String
        Dim Body As String = ""


        Body += " <tr><td>Dear " & AgentName & ", </td><td></td></tr>"
        Body += "<tr><td colspan='2'>Amount has been " & UploadType & " to your account with <b> INR. " & Amount & "</b>( " & UploadDate & ").Now your current balance is <b>INR." + AVLBalance + "</b>. </td></tr>"
       

   



        'Body = "<table cellpadding='0' cellspacing='0'>"
        'Body += " <tr><td>Dear " & AgentName & ", </td><td></td></tr>"

        'Body += "<tr><td colspan='2'>Amount has been " & UploadType & " to your account with <b> INR. " & Amount & "</b>( " & UploadDate & ").Now your current balance is <b>INR." + AVLBalance + "</b>. </td></tr>"
        'Body += "<tr><td >&nbsp;</td></tr>"
        'Body += "<tr><td >Thanks & Regards</td></tr>"
        'Body += "<tr><td > " & Regards & "</td></tr>"

        Return Body

    End Function
    Public Function SendMail(ByVal toEMail As String, ByVal from As String, ByVal bcc As String, ByVal cc As String, ByVal smtpClient As String, ByVal userID As String, ByVal pass As String, ByVal body As String, ByVal subject As String) As Integer


        Dim objMail As New System.Net.Mail.SmtpClient
        Dim msgMail As New System.Net.Mail.MailMessage
        msgMail.To.Clear()
        msgMail.To.Add(New System.Net.Mail.MailAddress(toEMail))
        msgMail.From = New System.Net.Mail.MailAddress(from)
        If bcc <> "" Then
            msgMail.Bcc.Add(New System.Net.Mail.MailAddress(bcc))
        End If
        If cc <> "" Then
            msgMail.CC.Add(New System.Net.Mail.MailAddress(cc))
        End If

        msgMail.Subject = subject
        msgMail.IsBodyHtml = True
        msgMail.Body = body


        Try
            objMail.Credentials = New System.Net.NetworkCredential(userID, pass)
            objMail.Host = smtpClient
            objMail.Send(msgMail)
            Return 1

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            Return 0

        End Try
    End Function
    Protected Sub plus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles plus.Click
        Dim AmountStatus As Boolean = False
        AmountStatus = Convert.ToBoolean(Distr.CheckDistrBalance(Request("AgentID"), Convert.ToDecimal(txt_crd_val.Text.Trim)))
        If (AmountStatus = True) Then
            Try
                Dim Sts As Boolean
                Sts = True
                Dim body As String = ""
                Dim subject As String = ""
                Dim email_id As String = ""
                Dim MobileDis As String = ""
                Dim DistrId As String = ""
                Dim dtagdetails As New DataTable
                Dim dtDistri As New DataTable
                Dim MailDt As New DataTable
                Dim ObjIntDetails As New IntlDetails()
                Try
                    MailDt = GetMailingDetails("ACCOUNT")
                    dtagdetails = ObjIntDetails.AgentIDInfo(Request("AgentID"))
                    email_id = dtagdetails.Rows(0)("Email").ToString()

                    ''DISTR
                    dtDistri = ObjIntDetails.AgentIDInfo(Session("UID"))
                    MobileDis = dtDistri.Rows(0)("Mobile").ToString()


                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try


                If (ChkSpl.Checked = True OrElse Sts = True) Then
                    If (Request("ID") <> "" AndAlso Request("AgentID") IsNot Nothing) Then
                        Dim DtDDetails As New DataTable
                        DtDDetails = STDom.GetDepositDetailsByID(Request("ID")).Tables(0)
                        Dim Status As String = DtDDetails.Rows(0)("Status").ToString
                        Dim IDS As String = DtDDetails.Rows(0)("Counter").ToString
                        Dim RequestId As Integer = Convert.ToInt32(Request("ID"))
                        If IDS = RequestId AndAlso Status = "DIConfirm" Then
                            'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Deposite amount is already Credited');", True)
                            ShowAlertMessage("Deposite amount is already Credited")
                        Else
                            If (Convert.ToDouble(txt_crd_val.Text.Trim) <= Convert.ToDouble(Request("Amount"))) Then
                                'Dim CurrentAval_Bal As Double = 0
                                'CurrentAval_Bal = Distr.AddCrdLimit(Request("AgentID"), Convert.ToDouble(txt_crd_val.Text), Session("UID"))
                                Dim dtavlbalance As New DataTable
                                Dim CurrentAval_Bal As Double = 0
                                Dim Distr_CurrentAval_Bal As Double = 0
                                dtavlbalance = Distr.AddCrdLimit(Request("AgentID"), Convert.ToDouble(txt_crd_val.Text), Session("UID")).Tables(0)
                                CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("AgentBalance").ToString())
                                Distr_CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("DistrBalance").ToString())
                                Dim dtdist As New DataTable()
                                dtdist = STDom.GetAgencyDetails(Session("UID")).Tables(0)
                                Dim AgencyName = dtdist.Rows(0)("Agency_Name").ToString()
                                Dim AgencyID_Dist = dtdist.Rows(0)("AgencyId").ToString()


                                Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                                Dim InvoiceNo As String = "FT" + ReferenceNo.Substring(4, 16)
                                Dim IP As String = Request.UserHostAddress

                                If (String.IsNullOrEmpty(IP)) Then
                                    IP = ":"
                                End If

                                'STDom.insertLedgerDetails(Request("AgentID"), td_AgencyName.InnerText, "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, txt_crd_val.Text.Trim, CurrentAval_Bal, "Credit", txt_rm.Text.Trim, 0)
                                'STDom.insertLedgerDetails(Session("UID"), AgencyName, "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, txt_crd_val.Text.Trim, 0, Distr_CurrentAval_Bal, "Debit", txt_rm.Text.Trim, 0)
                                STDom.insertLedgerDetails(Request("AgentID"), td_AgencyName.InnerText, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, txt_crd_val.Text.Trim, CurrentAval_Bal, "FUNDTRANSFER", "Fund Transfer From " + Session("AgencyId").ToString() + " to " + HiddAgentAgencyId.Value + " , " + txt_rm.Text.Trim, 0)
                                    STDom.insertLedgerDetails(Session("UID"), AgencyName, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP.Trim, txt_crd_val.Text.Trim, 0, Distr_CurrentAval_Bal, "FUNDTRANSFER", "Fund Transfer From " + Session("AgencyId").ToString() + " to " + HiddAgentAgencyId.Value + " , " + txt_rm.Text.Trim, 0)
                                    Dim LastAval_Bal As Double = 0
                                    LastAval_Bal = CurrentAval_Bal - Convert.ToDouble(txt_crd_val.Text.Trim)
                                    Dim UploadTypeDt As New DataTable
                                    UploadTypeDt = STDom.GetUploadTypeByType(Label1.Text).Tables(0)
                                    STDom.insertUploadDetails(Request("AgentID"), td_AgencyName.InnerText, Session("UID").ToString(), IP, 0, txt_crd_val.Text.Trim, txt_rm.Text.Trim, ddl_uploadtype.SelectedValue, LastAval_Bal, CurrentAval_Bal, txt_Yatra.Text.Trim)
                                    tbl_Upload.Visible = False
                                    td_msg.Visible = True
                                    td_msg.InnerText = "Amount credited to agent account sucessfully . " & td_AgencyName.InnerText & " Available Balance " & CurrentAval_Bal & " INR  " & Environment.NewLine & ". Amount debited to your account sucessfully. Available Balance" & Distr_CurrentAval_Bal & " INR "

                                    Dim smsMsg As String = ""
                                    Dim smsStatus As String = ""

                                    Dim smsMsg1 As String = ""
                                    Dim smsStatus1 As String = ""

                                    Try
                                        Dim objSMSAPI As New SMSAPI.SMS
                                        Dim dtagentmob As New DataTable()
                                        dtagentmob = STDom.GetAgencyDetails(Request("AgentId")).Tables(0)
                                        Dim Name As String = dtagentmob.Rows(0)("Title").ToString & " " & dtagentmob.Rows(0)("FName").ToString & " " & dtagentmob.Rows(0)("LName").ToString
                                        Dim AgentID_ag As String = dtagentmob.Rows(0)("AgencyId").ToString

                                        Dim SmsCrd As DataTable
                                        Dim objDA As New SqlTransaction
                                        SmsCrd = objDA.SmsCredential(SMS.UPLOADCREDIT.ToString()).Tables(0)
                                        If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                            smsStatus = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgentID_ag, td_Mobile.InnerText.Trim(), txt_crd_val.Text.Trim(), CurrentAval_Bal, td_AgencyName.InnerText.Trim(), ddl_uploadtype.SelectedValue.ToString(), Name, smsMsg, SmsCrd, "CREDIT")
                                            objSql.SmsLogDetails(AgentID_ag, td_Mobile.InnerText.Trim(), smsMsg, smsStatus)


                                            smsStatus1 = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgencyID_Dist, MobileDis, txt_crd_val.Text.Trim(), Distr_CurrentAval_Bal, "", "", "", smsMsg1, SmsCrd, "DEBIT")
                                            objSql.SmsLogDetails(AgencyID_Dist, MobileDis, smsMsg1, smsStatus1)
                                        End If
                                    Catch ex As Exception
                                    clsErrorLog.LogInfo(ex)
                                End Try


                                    'Dim smsMsg As String = ""
                                    'Dim smsStatus As String = ""
                                    'Try
                                    '    Dim objSMSAPI As New SMSAPI.SMS                                   
                                    '    Dim dtagentmob As New DataTable()
                                    '    dtagentmob = STDom.GetAgencyDetails(Request("AgentId")).Tables(0)
                                    '    Dim Name As String = dtagentmob.Rows(0)("Title").ToString & " " & dtagentmob.Rows(0)("FName").ToString & " " & dtagentmob.Rows(0)("LName").ToString
                                    '    smsStatus = objSMSAPI.sendUploadSms(td_AgencyName.InnerText.Trim(), Request("AgentId").ToString(), Name, td_Mobile.InnerText.Trim(), txt_crd_val.Text.Trim(), CurrentAval_Bal.ToString(), ddl_uploadtype.SelectedValue.ToString(), smsMsg)
                                    '    smsStatus = objSMSAPI.sendSms("dfdf", td_Mobile.InnerText.Trim(), "DEL:BOM", "123", "", "10-10-2013", "JHDJSS", smsMsg)
                                    'Catch ex As Exception

                                    'End Try

                                    Try
                                        body = GetMailBody(dtagdetails.Rows(0)("FName").ToString() & " " & dtagdetails.Rows(0)("LName").ToString(), "Credited", txt_crd_val.Text, DateTime.Now.ToString("dd-MM-yyyy"), CurrentAval_Bal, MailDt.Rows(0)("REGARDS").ToString())

                                    If (MailDt.Rows.Count > 0) Then
                                        Dim strBody As String
                                        Dim divbody As String = MailDt.Rows(0)("Body").ToString()

                                        Dim body1() As String = divbody.Split("}")
                                        strBody = body(0)
                                        strBody = strBody + body
                                        strBody = strBody + body1(2).ToString()


                                    End If


                                        SendMail(email_id, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserID").ToString(), MailDt.Rows(0)("Pass").ToString(), body, "Regarding Upload")

                                    Catch ex As Exception

                                    End Try

                                    If Request("ID") <> "" AndAlso Request("AgentID") <> "" Then
                                        STDom.UpdateDepositDetails(Request("ID"), Request("AgentID"), "DIConfirm", "Con", Session("UID"), "")
                                    End If
                                Else
                                    ' ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Amount is greater than requested amount');", True)
                                    ShowAlertMessage("Amount is greater than requested amount")
                            End If
                        End If
                    Else
                        Dim dtavlbalance As New DataTable
                        Dim CurrentAval_Bal As Double = 0
                        Dim Distr_CurrentAval_Bal As Double = 0
                        dtavlbalance = Distr.AddCrdLimit(td_AgentID.InnerText, Convert.ToDouble(txt_crd_val.Text), Session("UID")).Tables(0)
                        CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("AgentBalance").ToString())
                        Distr_CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("DistrBalance").ToString())
                        Dim dtdist As New DataTable()
                        dtdist = STDom.GetAgencyDetails(Session("UID")).Tables(0)
                        Dim AgencyName = dtdist.Rows(0)("Agency_Name").ToString()
                        Dim AgencyID_Dist = dtdist.Rows(0)("AgencyId").ToString()
                        Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                        Dim InvoiceNo As String = "FT" + ReferenceNo.Substring(4, 16)
                        Dim IP As String = Request.UserHostAddress


                        If (String.IsNullOrEmpty(IP)) Then
                            IP = ":"
                        End If
                        'STDom.insertLedgerDetails(td_AgentID.InnerText, td_AgencyName.InnerText, "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, txt_crd_val.Text.Trim, CurrentAval_Bal, "Credit", txt_rm.Text.Trim, 0)
                        'STDom.insertLedgerDetails(Session("UID"), AgencyName, "", "", "", "", "", Session("UID").ToString(), "", IP.Trim, txt_crd_val.Text.Trim, 0, Distr_CurrentAval_Bal, "Debit", txt_rm.Text.Trim, 0)
                        STDom.insertLedgerDetails(td_AgentID.InnerText, td_AgencyName.InnerText, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP.Trim, 0, txt_crd_val.Text.Trim, CurrentAval_Bal, "FUNDTRANSFER", "Fund Transfer From " + Session("AgencyId").ToString() + " to " + HiddAgentAgencyId.Value + " , " + txt_rm.Text.Trim, 0)
                        STDom.insertLedgerDetails(Session("UID"), AgencyName, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP.Trim, txt_crd_val.Text.Trim, 0, Distr_CurrentAval_Bal, "FUNDTRANSFER", "Fund Transfer From " + Session("AgencyId").ToString() + " to " + HiddAgentAgencyId.Value + " , " + txt_rm.Text.Trim, 0)

                        Dim LastAval_Bal As Double = 0
                        LastAval_Bal = CurrentAval_Bal - Convert.ToDouble(txt_crd_val.Text.Trim)
                        Dim UploadTypeDt As New DataTable
                        UploadTypeDt = STDom.GetUploadTypeByType(Label1.Text).Tables(0)
                        STDom.insertUploadDetails(Request("AgentID"), td_AgencyName.InnerText, Session("UID").ToString(), IP, 0, txt_crd_val.Text.Trim, txt_rm.Text.Trim, ddl_uploadtype.SelectedValue, LastAval_Bal, CurrentAval_Bal, txt_Yatra.Text.Trim)
                        tbl_Upload.Visible = False
                        td_msg.Visible = True
                        td_msg.InnerText = "Amount credited to agent account sucessfully . " & td_AgencyName.InnerText & "( " & Request("AgentID") & " ) Available Balance " & CurrentAval_Bal & " INR. " & Environment.NewLine & " Amount debited to your account sucessfully .Available Balance " & Distr_CurrentAval_Bal & " INR"


                        Dim smsMsg As String = ""
                        Dim smsStatus As String = ""

                        Dim smsMsg1 As String = ""
                        Dim smsStatus1 As String = ""

                        Try
                            Dim objSMSAPI As New SMSAPI.SMS
                            Dim dtagentmob As New DataTable()
                            dtagentmob = STDom.GetAgencyDetails(Request("AgentId")).Tables(0)

                            Dim Name As String = dtagentmob.Rows(0)("Title").ToString & " " & dtagentmob.Rows(0)("FName").ToString & " " & dtagentmob.Rows(0)("LName").ToString
                            Dim AgentID_ag As String = dtagentmob.Rows(0)("AgencyId").ToString

                            Dim SmsCrd As DataTable
                            Dim objDA As New SqlTransaction
                            SmsCrd = objDA.SmsCredential(SMS.UPLOADCREDIT.ToString()).Tables(0)
                            If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                smsStatus = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgentID_ag, td_Mobile.InnerText.Trim(), txt_crd_val.Text.Trim(), CurrentAval_Bal, td_AgencyName.InnerText.Trim(), ddl_uploadtype.SelectedValue.ToString(), Name, smsMsg, SmsCrd, "CREDIT")
                                objSql.SmsLogDetails(AgentID_ag, td_Mobile.InnerText.Trim(), smsMsg, smsStatus)


                                smsStatus1 = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgencyID_Dist, MobileDis, txt_crd_val.Text.Trim(), Distr_CurrentAval_Bal, "", "", "", smsMsg1, SmsCrd, "DEBIT")
                                objSql.SmsLogDetails(AgencyID_Dist, MobileDis, smsMsg1, smsStatus1)
                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                        End Try

                        Try
                            body = GetMailBody(dtagdetails.Rows(0)("FName").ToString() & " " & dtagdetails.Rows(0)("LName").ToString(), "Credited", txt_crd_val.Text, DateTime.Now.ToString("dd-MM-yyyy"), CurrentAval_Bal, MailDt.Rows(0)("REGARDS").ToString())
                            SendMail(email_id, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserID").ToString(), MailDt.Rows(0)("Pass").ToString(), body, "Regarding Amount Upload")

                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                        End Try



                    End If
                    'Yatra Party Regestration
                    Try
                        'Dim P_Reg As New Party
                        'P_Reg.InsertParty_Details(Request("AgentID").ToString())
                    Catch ex As Exception
                    End Try
                    'Yatra Party Regestration end

                    ChkSpl.Checked = False

                Else
                    Dim MSG As String = ""
                    ' MSG = dt.Rows(0)("CRMsg").ToString()
                    ' ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "TEMP", "alert('" & MSG & "')", True)
                    ' ShowAlertMessage(MSG)
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        Else
            tbl_Upload.Visible = False
            td_msg.Visible = True
            td_msg.InnerText = "Please check your balance."
        End If
       
    End Sub
    Protected Sub minus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles minus.Click
        Try
            
            Dim body As String = ""
            Dim subject As String = ""
            Dim email_id As String = ""
            Dim MobileDis As String = ""
            Dim dtagdetails As New DataTable
            Dim MailDt As New DataTable
            Dim ObjIntDetails As New IntlDetails()
            Try
                MailDt = GetMailingDetails("ACCOUNT")
                dtagdetails = ObjIntDetails.AgentIDInfo(Request("AgentID"))
                email_id = dtagdetails.Rows(0)("Email").ToString()

            Catch ex As Exception

            End Try
            Dim dtdist As New DataTable()
            dtdist = STDom.GetAgencyDetails(Session("UID")).Tables(0)
            Dim AgencyName = dtdist.Rows(0)("Agency_Name").ToString()
            MobileDis = dtdist.Rows(0)("Mobile").ToString()
            Dim AgencyID_Dist = dtdist.Rows(0)("AgencyId").ToString()
            Dim dtag As New DataTable()
            dtag = STDom.GetAgencyDetails(Request("AgentID")).Tables(0)
            Dim crd_limt As Double = 0
            crd_limt = Convert.ToDouble(dtag.Rows(0)("crd_limit").ToString())
            'Dim CurrentAval_Bal As Double = 0
            'CurrentAval_Bal = Distr.UpdateCrdLimit(Request("AgentID"), Convert.ToDouble(txt_crd_val.Text), Session("UID"))

            

            If (crd_limt >= Convert.ToDouble(txt_crd_val.Text)) Then
                Dim dtavlbalance As New DataTable
                Dim CurrentAval_Bal As Double = 0
                Dim Distr_CurrentAval_Bal As Double = 0
                dtavlbalance = Distr.UpdateCrdLimit(Request("AgentID"), Convert.ToDouble(txt_crd_val.Text), Session("UID")).Tables(0)
                CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("AgentBalance").ToString())
                Distr_CurrentAval_Bal = Convert.ToDouble(dtavlbalance.Rows(0)("DistrBalance").ToString())
                Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                Dim InvoiceNo As String = "FR" + ReferenceNo.Substring(4, 16)
                Dim IP1 As String = Request.UserHostAddress


                If (String.IsNullOrEmpty(IP1)) Then
                    IP1 = ":"
                End If
                'STDom.insertLedgerDetails(Request("AgentID"), td_AgencyName.InnerText, "", "", "", "", "", Session("UID").ToString(), "", IP1.Trim, txt_crd_val.Text.Trim, 0, CurrentAval_Bal, "Debit", txt_rm.Text.Trim, 0)
                'STDom.insertLedgerDetails(Session("UID"), AgencyName, "", "", "", "", "", Session("UID").ToString(), "", IP1.Trim, 0, txt_crd_val.Text.Trim, Distr_CurrentAval_Bal, "Credit", txt_rm.Text.Trim, 0) 
                STDom.insertLedgerDetails(Request("AgentID"), td_AgencyName.InnerText, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP1.Trim, txt_crd_val.Text.Trim, 0, CurrentAval_Bal, "FUNDREVERSAL", "Fund Reversal From " + HiddAgentAgencyId.Value + " To " + Session("AgencyId").ToString() + " , " + txt_rm.Text.Trim, 0)
                STDom.insertLedgerDetails(Session("UID"), AgencyName, InvoiceNo, "", "", "", "", Session("UID").ToString(), "", IP1.Trim, 0, txt_crd_val.Text.Trim, Distr_CurrentAval_Bal, "FUNDREVERSAL", "Fund Reversal From " + HiddAgentAgencyId.Value + " To " + Session("AgencyId").ToString() + " , " + txt_rm.Text.Trim, 0)
                Dim LastAval_Bal As Double = 0
                LastAval_Bal = CurrentAval_Bal + Convert.ToDouble(txt_crd_val.Text.Trim)
                Dim UploadTypeDt As New DataTable
                UploadTypeDt = STDom.GetUploadTypeByType(Label1.Text).Tables(0)

                STDom.insertUploadDetails(Request("AgentID"), td_AgencyName.InnerText, Session("UID").ToString(), IP1, txt_crd_val.Text.Trim, 0, txt_rm.Text.Trim, ddl_uploadtype.SelectedValue, LastAval_Bal, CurrentAval_Bal, txt_Yatra.Text.Trim)
                tbl_Upload.Visible = False
                td_msg.Visible = True
                td_msg.InnerText = "Amount debited to agent account sucessfully .  " & td_AgencyName.InnerText & "(" & Request("AgentID") & " ) Available Balance " & CurrentAval_Bal & " INR. " & Environment.NewLine & "  Amount credited to your account sucessfully . Available Balance " & Distr_CurrentAval_Bal & " INR"


                Dim smsMsg As String = ""
                Dim smsStatus As String = ""

                Dim smsMsg1 As String = ""
                Dim smsStatus1 As String = ""

                Try
                    Dim objSMSAPI As New SMSAPI.SMS
                    Dim dtagentmob As New DataTable()
                    dtagentmob = STDom.GetAgencyDetails(Request("AgentId")).Tables(0)
                    Dim Name As String = dtagentmob.Rows(0)("Title").ToString & " " & dtagentmob.Rows(0)("FName").ToString & " " & dtagentmob.Rows(0)("LName").ToString

                    Dim AgentID_ag As String = dtagentmob.Rows(0)("AgencyId").ToString

                    Dim SmsCrd As DataTable
                    Dim objDA As New SqlTransaction
                    SmsCrd = objDA.SmsCredential(SMS.UPLOADCREDIT.ToString()).Tables(0)
                    If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                        smsStatus = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgentID_ag, td_Mobile.InnerText.Trim(), txt_crd_val.Text.Trim(), CurrentAval_Bal, td_AgencyName.InnerText.Trim(), ddl_uploadtype.SelectedValue.ToString(), Name, smsMsg, SmsCrd, "DEBIT")
                        objSql.SmsLogDetails(AgentID_ag, td_Mobile.InnerText.Trim(), smsMsg, smsStatus)


                        smsStatus1 = objSMSAPI.sendUploadSmsCreditDebit(InvoiceNo, AgencyID_Dist, MobileDis, txt_crd_val.Text.Trim(), Distr_CurrentAval_Bal, "", "", "", smsMsg1, SmsCrd, "CREDIT")
                        objSql.SmsLogDetails(AgencyID_Dist, MobileDis, smsMsg1, smsStatus1)
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try

                ' STDom.SendMail(email_id, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserID").ToString(), MailDt.Rows(0)("Pass").ToString(), body, subject)
                Try
                    body = GetMailBody(dtagdetails.Rows(0)("FName").ToString() & " " & dtagdetails.Rows(0)("LName").ToString(), "Debited", txt_crd_val.Text, DateTime.Now.ToString("dd-MM-yyyy"), CurrentAval_Bal, MailDt.Rows(0)("REGARDS").ToString())
                    SendMail(email_id, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserID").ToString(), MailDt.Rows(0)("Pass").ToString(), body, "Regarding Amount Upload")

                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try

            Else
                tbl_Upload.Visible = False
                td_msg.Visible = True
                td_msg.InnerText = "Agent credit limit should be greater or equal to requested amount.Please try again with valid amount."
            End If


            
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub minus_SeriesClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles minus_Series.Click
        'Try
        '    Dim CurrentAval_Bal As Double = 0
        '    CurrentAval_Bal = Distr.UpdateCrdLimit(Request("AgentID"), Convert.ToDouble(txt_crd_val.Text), Session("UID"))
        '    Dim IP1 As String = Request.UserHostAddress
        '    STDom.insertLedgerDetails(Request("AgentID"), td_AgencyName.InnerText, "", "", "", "", "", Session("UID").ToString(), "", IP1.Trim, txt_crd_val.Text.Trim, 0, CurrentAval_Bal, "Debit", txt_rm.Text.Trim, 0)
        '    Dim LastAval_Bal As Double = 0
        '    LastAval_Bal = CurrentAval_Bal + Convert.ToDouble(txt_crd_val.Text.Trim)
        '    Dim UploadTypeDt As New DataTable
        '    UploadTypeDt = STDom.GetUploadTypeByType(Label1.Text).Tables(0)

        '    STDom.insertUploadDetails(Request("AgentID"), td_AgencyName.InnerText, Session("UID").ToString(), IP1, txt_crd_val.Text.Trim, 0, txt_rm.Text.Trim, ddl_uploadtype.SelectedValue, LastAval_Bal, CurrentAval_Bal, txt_Yatra.Text.Trim)
        '    Dim i As Integer = series.UpdateSeriesPendingRequest(Session("UID").ToString, IP1, "Confirm", Request("Counter"), txt_rm.Text.Trim)
        '    tbl_Upload.Visible = False
        '    td_msg.Visible = True

        '    td_msg.InnerText = "Amount Debited Sucessfully. " & td_AgencyName.InnerText & " current balance " & CurrentAval_Bal & " INR"

        'Catch ex As Exception

        'End Try
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
End Class

