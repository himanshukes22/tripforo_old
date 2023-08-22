Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Web.Security
Imports IPTracker
Partial Class UserControl_LoginControl
    Inherits System.Web.UI.UserControl
    Dim User, userid, pwd, id As String
    Dim AgencyName, usertype, typeid As String
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As SqlDataAdapter
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Private det As New Details()
    Dim msgout As String = ""
    Dim LoginType As String = ""
    Dim StaffUserId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        lblerror.Visible=False
        Dim dset As New DataSet
        Dim STDom As New SqlTransactionDom
        Dim dt As DataTable
        Dim MobileDT As String = ""
        Dim AgencyDT As String = ""
        Dim EMAILDT As String = ""
        Dim AgencyNameDT As String = ""
        Dim OTPBaseLogin As Boolean = False
        Dim PwdCondition As Boolean = False
        Dim passexp As Integer = 0
        Dim IsWhiteLabel As Boolean = 0
        Dim Branch As String = ""

        Dim flag1 As Integer

        Dim LoginByStaff As String = "false"
        LoginType = ""

        Try
            Dim loginTypeSelected As String = ""
            If rdoAgent.Checked = True Then
                loginTypeSelected = "agent"
            ElseIf rdoSupplier.Checked = True Then
                loginTypeSelected = "supplier"
            End If

            userid = UserLogin.UserName
            pwd = UserLogin.Password
            Try
                LoginType = ""
                Dim StaffDs As DataSet = New DataSet()
                StaffDs = AgentStaffLogin(userid, pwd)
                'If Not String.IsNullOrEmpty(OTP) AndAlso SmsCrd IsNot Nothing AndAlso SmsCrd.Tables.Count > 0 AndAlso SmsCrd.Tables(0).Rows.Count > 0 AndAlso Convert.ToBoolean(SmsCrd.Tables(0).Rows(0)("Status")) = True Then
                If (Not String.IsNullOrEmpty(LoginType) AndAlso LoginType.ToUpper() = "STAFF") Then
                    If (StaffDs IsNot Nothing AndAlso StaffDs.Tables.Count > 0 AndAlso StaffDs.Tables(0).Rows.Count > 0) Then
                        If Convert.ToBoolean(StaffDs.Tables(0).Rows(0)("Status")) = True AndAlso Convert.ToString(StaffDs.Tables(0).Rows(0)("Agent_status")) = "ACTIVE" Then
                            StaffUserId = Convert.ToString(StaffDs.Tables(0).Rows(0)("UserId")) 'userid UserId
                            userid = Convert.ToString(StaffDs.Tables(0).Rows(0)("AgentUserId"))
                            pwd = Convert.ToString(StaffDs.Tables(0).Rows(0)("AgentPassword"))
                            LoginByStaff = "true"
                            Session("StaffUserId") = StaffUserId
                            Session("FlightActive") = Convert.ToString(StaffDs.Tables(0).Rows(0)("Flight"))
                            Session("HotelActive") = Convert.ToString(StaffDs.Tables(0).Rows(0)("Hotel"))
                            Session("BusActive") = Convert.ToString(StaffDs.Tables(0).Rows(0)("Bus"))
                            Session("RailActive") = Convert.ToString(StaffDs.Tables(0).Rows(0)("Rail"))
                            ' dset = user_auth(userid, pwd)
                        ElseIf Convert.ToBoolean(StaffDs.Tables(0).Rows(0)("Status")) = False Then
                            'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                            lblerror.Text = "UserId is not active."
                            lblerror.Visible = True
                            Return
                        ElseIf Convert.ToString(StaffDs.Tables(0).Rows(0)("Agent_status")) <> "ACTIVE" Then
                            'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                            lblerror.Text = "Please contact your admin."
                            lblerror.Visible = True
                            Return
                        Else
                            'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                            lblerror.Text = "Please contact your admin."
                            lblerror.Visible = True
                            Return
                        End If
                    Else
                        'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                        lblerror.Text = "Please contact your admin."
                        lblerror.Visible = True
                        Return
                    End If
                    '
                End If
            Catch ex As Exception

            End Try
            dset = user_auth(userid, pwd)


            If dset.Tables(0).Rows(0)(0).ToString() = "Not a Valid ID" Then
                'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                lblerror.Text = "Your UserID Seems to be Incorrect"
                lblerror.Visible = True
            ElseIf dset.Tables(0).Rows(0)(0).ToString() = "incorrect password" Then
                'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                lblerror.Text = "Your Password Seems to be Incorrect"
                lblerror.Visible = True
            ElseIf dset.Tables(0).Rows(0)(0).ToString() = "notactive" Then
                lblerror.Text = "Your account is not active yet, Please contact your admin."
                lblerror.Visible = True
            Else

                If loginTypeSelected = "agent" Then
                    dt = STDom.GetAgencyDetails(userid).Tables(0)

                    If (Convert.ToBoolean(dt.Rows(0)("IsWhiteLabel").ToString()) = True) Then
                        If (LoginType <> "STAFF") Then
                            If (dt.Rows(0)("Branch").ToString().ToUpper() = "MUMBAI") Then
                                Response.Redirect("https://www.Richa Travel.co/mumbai/AccessLogin.aspx?UserID=" + userid + "&Code=" + pwd)
                            End If
                            If (dt.Rows(0)("Branch").ToString().ToUpper() = "PUNJAB") Then

                            End If
                        End If

                    Else
                        OTPBaseLogin = Convert.ToBoolean(dt.Rows(0)("OTPLoginStatus").ToString())
                        PwdCondition = Convert.ToBoolean(dt.Rows(0)("PasswordExpMsg").ToString())
                        Try
                            passexp = Convert.ToInt32(dt.Rows(0)("PasswordChangeDate").ToString())
                        Catch ex As Exception
                            passexp = 0
                        End Try

                        If (passexp <= 0 And PwdCondition = False) Then
                            Session("UID_USER") = dset.Tables(0).Rows(0)("UID").ToString()
                            Response.Redirect("PasswordRedirect.aspx", True)
                            'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                            'lblerr.Text = "Your Password Expaired Now!"
                        Else
                            If (OTPBaseLogin = True) Then

                                EMAILDT = dt.Rows(0)("Email").ToString()
                                MobileDT = dt.Rows(0)("Mobile").ToString()
                                AgencyDT = dt.Rows(0)("AgencyId").ToString()
                                AgencyNameDT = dt.Rows(0)("Agency_Name").ToString()

                                flag1 = sendOTP(userid, AgencyDT, AgencyNameDT, MobileDT, EMAILDT)
                            Else

                                id = dset.Tables(0).Rows(0)("UID").ToString()
                                usertype = dset.Tables(0).Rows(0)("UserType").ToString()
                                typeid = dset.Tables(0).Rows(0)("TypeID").ToString()
                                User = dset.Tables(0).Rows(0)("Name").ToString()

                                Try
                                    Dim lastLogin As New DataSet
                                    lastLogin = LastLoginTime(id)
                                    Session("LastloginTime") = lastLogin.Tables(0).Rows(0)("LoginTime").ToString()

                                    'Dim strHostName As String
                                    Dim strIPAddress As String
                                    'strHostName = System.Net.Dns.GetHostName()
                                    strIPAddress = Request.UserHostAddress

                                    InsertLoginTime(id, strIPAddress)
                                Catch ex As Exception

                                End Try


                                If usertype = "TA" Then
                                    AgencyName = dset.Tables(0).Rows(0)("AgencyName").ToString()
                                    Session("AgencyId") = dset.Tables(0).Rows(0)("AgencyId").ToString()
                                    ' Session("User_Type") = "AGENT"
                                End If
                                Session("LoginByOTP") = ""    'when login through otp LoginByOTP=true
                                Session("firstNameITZ") = userid
                                Session("AgencyName") = AgencyName
                                Session("UID") = id ''dset.Tables(0).Rows(0)("UID").ToString()
                                Session("UserType") = usertype '' "TA"
                                Session("TypeID") = typeid ''"TA1"

                                Session("IsCorp") = False
                                Session("SalesExecID") = dt.Rows(0)("SalesExecID")
                                Session("AGTY") = dset.Tables(0).Rows(0)("Agent_Type") ''"TYPE1"
                                Session("agent_type") = dset.Tables(0).Rows(0)("Agent_Type") ''"TYPE1"
                                Session("User_Type") = User

                                Session("LoginByStaff") = LoginByStaff
                                Session("LoginType") = LoginType
                                FormsAuthentication.RedirectFromLoginPage(userid, False)

                                If User = "ACC" Then
                                    Session("UID") = dset.Tables(0).Rows(0)("UID").ToString()
                                    Session("UserType") = "AC"
                                    Response.Redirect("SprReports/Accounts/Ledger.aspx", False)
                                ElseIf User = "ADMIN" Then
                                    Session("ADMINLogin") = userid
                                    Session("UID") = dset.Tables(0).Rows(0)("UID").ToString()
                                    Session("UserType") = "AD"
                                    Response.Redirect("Search.aspx", False)

                                ElseIf User = "EXEC" Then
                                    Session("User_Type") = "EXEC"
                                    Session("TripExec") = dset.Tables(0).Rows(0)("Trip").ToString()
                                    Session("UID") = dset.Tables(0).Rows(0)("UID").ToString()
                                    Session("UserType") = "EC"
                                    Response.Redirect("Report/admin/profile.aspx", False)
                                ElseIf User = "AGENT" And typeid = "TA1" Then
                                    If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                                        Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp"))
                                    End If
                                    Response.Redirect("Search.aspx", False)
                                ElseIf User = "AGENT" And typeid = "TA2" Then
                                    If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                                        Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp").ToString())
                                    End If
                                    Response.Redirect("Report/Accounts/Ledger.aspx", False)
                                ElseIf usertype = "DI" Then
                                    Session("AgencyId") = dset.Tables(0).Rows(0)("AgencyId").ToString()
                                    Session("AgencyName") = dset.Tables(0).Rows(0)("AgencyName").ToString()
                                    Response.Redirect("Report/Accounts/Ledger.aspx", False)
                                    'END CHANGES FOR DISTR
                                End If

                            End If


                        End If
                    End If
                Else
                    Dim IsSupplier As Boolean = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsSupplier").ToString())
                    If IsSupplier Then
                        Response.Redirect("http://fixeddeparture.tripforo.com?userdetail=" + userid + "|" + pwd, False)
                    Else
                        'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                        lblerror.Text = "You are not authoried to access this service."
                        lblerror.Visible = True
                    End If

                End If
            End If



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Function user_auth(ByVal uid As String, ByVal passwd As String) As DataSet
        Dim ds As New DataSet()
        Try
            adap = New SqlDataAdapter("UserLoginNew", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
            adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd)

            adap.Fill(ds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return ds
    End Function

    Public Function LastLoginTime(ByVal uid As String) As DataSet
        Dim ds As New DataSet()
        Try
            adap = New SqlDataAdapter("Sp_Tbl_UserLoginTime", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@userID", uid)

            adap.Fill(ds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return ds
    End Function
    Public Function InsertLoginTime(ByVal UID As String, ByVal ipaddress As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@userID", UID)
        paramHashtable.Add("@IPAdress", ipaddress)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Sp_Tbl_UserLoginTime_Insert", 1)
    End Function
    Protected Function GenerateOTP() As String
        Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
        Dim numbers As String = "1234567890"
        Dim Length As String = "6"
        Dim OTPType As String = "2"
        Dim characters As String = numbers
        If OTPType = "1" Then
            characters += alphabets & small_alphabets & numbers
        End If

        Dim length1 As Integer = Integer.Parse(Length)
        Dim otp As String = String.Empty
        For i As Integer = 0 To length1 - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().[Next](0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1

            otp += character
        Next

        Return otp
    End Function
    Private Function InsertOTP(ByVal UserId As String, ByVal AgencyId As String, ByVal OTP As String, ByVal Status As Boolean, ByVal MobileNo As String, ByVal MStatus As Boolean, ByVal EmailId As String, ByVal EmailStatus As Boolean, ByVal Remark As String, ByVal OTPId As String) As Integer
        Dim flag As Integer = 0
        Try
            Dim RandomNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
            Dim InvoiceNo As String = "CL" & RandomNo.Substring(7, 13)
            Dim IPAddress As String
            IPAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If IPAddress = "" OrElse IPAddress Is Nothing Then IPAddress = Request.ServerVariables("REMOTE_ADDR")
            Dim cmd As SqlCommand = New SqlCommand("SP_INSERT_OTP", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserId", UserId)
            cmd.Parameters.AddWithValue("@AgencyId", AgencyId)
            cmd.Parameters.AddWithValue("@OTP", OTP)
            cmd.Parameters.AddWithValue("@Status", Status)
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo)
            cmd.Parameters.AddWithValue("@MStatus", MStatus)
            cmd.Parameters.AddWithValue("@EmailId", EmailId)
            cmd.Parameters.AddWithValue("@EmailStatus", EmailStatus)
            cmd.Parameters.AddWithValue("@CreatedBy", UserId)
            cmd.Parameters.AddWithValue("@IPAddress", IPAddress)
            cmd.Parameters.AddWithValue("@Remark", Remark)
            cmd.Parameters.AddWithValue("@OTPId", OTPId)
            cmd.Parameters.AddWithValue("@ActionType", "INSERTOTP")
            cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100)
            cmd.Parameters("@Msg").Direction = ParameterDirection.Output
            If con.State = ConnectionState.Closed Then con.Open()
            flag = cmd.ExecuteNonQuery()
            con.Close()
            msgout = cmd.Parameters("@Msg").Value.ToString()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()
        End Try

        Return flag
    End Function



    Public Function sendOTP(ByVal UserId As String, ByVal agencyID As String, ByVal agencyName As String, ByVal Mobile As String, ByVal EMAILID As String) As Integer
        Dim flag As Integer = 0
        Try
            Dim AgentLimit As String = ""

            If Mobile.Length = 10 Then
                msgout = ""
                Dim OTP As String = GenerateOTP()
                Dim STDOM As SqlTransactionDom = New SqlTransactionDom()
                Dim M As DataSet = STDOM.GetMailingDetails("OTP", "")
                Try
                    Dim smsMsg As String = ""
                    Dim smsStatus As String = ""
                    Dim MailSent As Boolean = False
                    Dim OtpStatus As Boolean = False
                    Dim objSMSAPI As SMSAPI.SMS = New SMSAPI.SMS()
                    Dim objSql As SqlTransactionNew = New SqlTransactionNew()
                    Dim SmsCrd As DataSet = New DataSet()
                    Dim objDA As SqlTransaction = New SqlTransaction()
                    SmsCrd = objDA.SmsCredential(Convert.ToString(SMS.EMULATE))
                    If Not String.IsNullOrEmpty(OTP) AndAlso SmsCrd IsNot Nothing AndAlso SmsCrd.Tables.Count > 0 AndAlso SmsCrd.Tables(0).Rows.Count > 0 AndAlso Convert.ToBoolean(SmsCrd.Tables(0).Rows(0)("Status")) = True Then
                        OtpStatus = True
                        Try
                            Dim dt As DataTable = New DataTable()
                            dt = SmsCrd.Tables(0)
                            smsMsg = "Your One Time Password(OTP) is " & OTP & " for login and valid for next 20 mins."
                            Dim MobileNo As String = Convert.ToString(Mobile)
                            smsStatus = objSMSAPI.SendSmsForAnyService(MobileNo, smsMsg, dt)
                            objSql.SmsLogDetails(Convert.ToString(UserId), Convert.ToString(MobileNo), smsMsg, smsStatus)
                        Catch ex As Exception
                        End Try

                        Try
                            Dim Sent As Integer = 0
                            Sent = SendEmail(agencyName, OTP, EMAILID)
                            If Sent > 0 Then
                                MailSent = True
                            Else
                                MailSent = False
                            End If
                        Catch ex As Exception
                        End Try

                        Dim RandomNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                        Dim strOtp As String = OTP.Substring(4, 2)
                        Dim OTPId As String = RandomNo.Substring(2, 18) + OTP.Substring(4, 2)
                        flag = InsertOTP(UserId, agencyID, OTP, OtpStatus, Mobile, True, EMAILID, MailSent, "agentLoginOtpBase", OTPId)

                        If flag > 1 Then

                            ''ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('http://localhost:53943/OTPValidateUser.aspx?Param=" & agencyID & "&ProductID=" & OTPId & "&Userid=" & UserId & "');", True)
                            Response.Redirect("OTPValidateUser.aspx?Param=" & agencyID & "&ProductID=" & OTPId & "&Userid=" & UserId & "")

                        End If
                    Else

                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "redirect", "alert('" & msgout & "');", True)
                    End If
                Catch ex As Exception
                End Try
            Else

                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "redirect", "alert('Please enter valid agent id !!');", True)

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            flag = 0
        End Try
        Return flag
    End Function

    Private Function SendEmail(ByVal Name As String, ByVal OTP As String, ByVal ToEmailId As String) As Integer
        Dim SendMail As Integer = 0
        Try
            Dim MailDs As DataSet = New DataSet()
            Dim MailDt As DataTable = New DataTable()
            Dim STDom As SqlTransactionDom = New SqlTransactionDom()
            MailDs = STDom.GetMailingDetails(MAILING.REGISTRATION_AGENT.ToString().Trim(), "FWS")
            If MailDs IsNot Nothing AndAlso MailDs.Tables.Count > 0 AndAlso MailDs.Tables(0).Rows.Count > 0 Then
                MailDt = MailDs.Tables(0)
            End If

            Dim strBody As String
            Dim mailbody As String = ""
            mailbody += "<table border='0' cellpadding='0' cellspacing='0' width='575' style='border-collapse:collapse;width:431pt'>"
            mailbody += "<tbody>"
            mailbody += "<tr height='102' style='height:76.5pt'>"
            mailbody += "<td height='102' class='m_4924402671878462581xl66' width='575' style='height:76.5pt;width:431pt'>"
            mailbody += "Dear &nbsp;&nbsp; " & Name & ",<br> <br>" & OTP & " &nbsp;is your one time password (<span class='il'>OTP</span>). <br>Please enter the <span class='il'>OTP</span> to proceed and valid for next 20 mins.<br>"
            mailbody += "<br> Thank you,"
            mailbody += "<br><br> Team Support"
            mailbody += "</td>"
            mailbody += "</tr>"
            mailbody += "</tbody>"
            mailbody += "</table>"
            Try
                If (MailDt.Rows.Count > 0) Then
                    Dim Status As Boolean = False
                    Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                    If Status = True Then
                        Dim MailSubject As String = "your one time password(OTP) is   " & OTP & "  for login "
                        SendMail = STDom.SendMail(ToEmailId, Convert.ToString(MailDt.Rows(0)("MAILFROM")), Convert.ToString(MailDt.Rows(0)("BCC")), Convert.ToString(MailDt.Rows(0)("CC")), Convert.ToString(MailDt.Rows(0)("SMTPCLIENT")), Convert.ToString(MailDt.Rows(0)("UserId")), Convert.ToString(MailDt.Rows(0)("Pass")), mailbody, MailSubject, "")
                    End If
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return SendMail
    End Function


    Private Function AgentStaffLogin(ByVal UserId As String, ByVal Password As String) As DataSet
        Dim flag As Integer = 0
        Dim IPAddress As String
        IPAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If IPAddress = "" OrElse IPAddress Is Nothing Then IPAddress = Request.ServerVariables("REMOTE_ADDR")
        Dim ds As New DataSet()
        Try
            adap = New SqlDataAdapter("SP_AGENT_STAFFLOGIN", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@UserId", UserId)
            adap.SelectCommand.Parameters.AddWithValue("@Password", Password)
            adap.SelectCommand.Parameters.AddWithValue("@IPAddress", IPAddress)
            adap.SelectCommand.Parameters.AddWithValue("@ActionType", "GETUSERTYPE")
            adap.SelectCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 100)
            adap.SelectCommand.Parameters("@Msg").Direction = ParameterDirection.Output
            If con.State = ConnectionState.Closed Then con.Open()
            adap.Fill(ds)
            con.Close()
            LoginType = Convert.ToString(adap.SelectCommand.Parameters("@Msg").Value).ToUpper()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()
        End Try
        Return ds
    End Function
End Class







'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Web
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Configuration
'Imports System.Web.Security
'Imports IPTracker
'Imports ITZLib
'Imports System.Xml.Linq

'Partial Class UserControl_LoginControl
'    Inherits System.Web.UI.UserControl
'    Dim userid As String
'    Dim pwd As String
'    Dim AgencyName As String
'    Dim User As String
'    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
'    Dim adap As SqlDataAdapter
'    Dim id As String
'    Dim usertype As String
'    Dim typeid As String
'    Dim dset As DataSet
'    Private det As New Details()
'    Dim objUm As New UrlMapping()

'#Region "Pawan Kumar"
'    Dim objVL As New _validateLogin()
'    Dim objSvcLgn As New ITZLoginUsers()
'    Dim loginMsg As String
'    Dim splitVals As String()
'    Dim objAdvParm As New AdvLoginPar()
'    Dim advUserInfo As New UserInfo()
'#End Region

'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

'    End Sub



'    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
'        Dim umMapp As String = ""
'        Dim custmUrl As String = ""
'        Dim hashedpwd As String = ""
'        Dim dsUrlRe As New DataSet()
'        Try
'            Dim ddllog As DropDownList
'            ddllog = CType(UserLogin.FindControl("ddlLogType"), DropDownList)
'            userid = UserLogin.UserName
'            pwd = UserLogin.Password

'            ''objVL.strModeType = "WEB"
'            ''objVL.strPassword = UserLogin.Password.Trim()
'            ''objVL.strUserName = UserLogin.UserName.Trim()
'            objAdvParm.strModeType = "WEB"
'            objAdvParm.strPassword = UserLogin.Password.Trim()
'            objAdvParm.strUserName = UserLogin.UserName.Trim()
'            If UserLogin.UserName.ToUpper().Contains("ITZ") Then
'                Try
'                    hashedpwd = VGCheckSum.genPassHash(objAdvParm.strPassword.Trim())
'                    objAdvParm.strPassword = hashedpwd.Trim()
'                    ''loginMsg = objSvcLgn.ITZLoginUser(objVL)
'                    advUserInfo = objSvcLgn.Advance_Login(objAdvParm)
'                    splitVals = loginMsg.Split("~")
'                Catch ex As Exception
'                End Try
'            End If
'            ''''If (splitVals.Length = 6) Then
'            ''''dset = user_auth(UserLogin.UserName, UserLogin.Password, "D000010496", "2016-01-22 11:24 AM")
'            custmUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
'            Dim urlDoc = XDocument.Load(Context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
'            Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
'            Session("ModeTypeITZ") = "WEB"
'            Session("MchntKeyITZ") = ConfigurationManager.AppSettings("MerchantKey").ToString()
'            Session("_SvcTypeITZ") = "MERCHANTDB"

'            ''If ddllog.SelectedItem.Value.ToUpper().Equals("C") AndAlso splitVals.Length = 6 Then

'            ''If advUserInfo.customerInfo.dcode <> Nothing Then
'            dset = user_auth(UserLogin.UserName, UserLogin.Password, "", "")

'            ''End If


'            If dset.Tables.Count > 0 And Convert.ToString(dset.Tables(0).Rows(0)("UID")) = "Not a Valid ID" And UserLogin.UserName.ToUpper().Contains("ITZ") Then

'                Session("_PASSWORD") = hashedpwd
'                Session("_USERNAME") = UserLogin.UserName


'                ''çhanged for decode
'                ''Session("UID") = UserLogin.UserName
'                Session("UID") = advUserInfo.customerInfo.dcode

'                Session("UserType") = "TA"
'                Session("TypeID") = "TA1"
'                Session("User_Type") = "AGENT"
'                Session("IsCorp") = False

'                Session("AgencyName") = advUserInfo.lastName
'                Session("AGTY") = dset.Tables(0).Rows(0)("Agent_Type") ''"TYPE1"
'                Session("agent_type") = dset.Tables(0).Rows(0)("Agent_Type") ''"TYPE1"
'                ''"DEFAULT_ITILHE"
'                ''''Session("ModeTypeITZ") = ds.Tables(0).Rows(0)("ModeType_ITZ").ToString().Trim()


'                Session("_DCODE") = advUserInfo.customerInfo.dcode

'                ''Session("_DCODE") = UserLogin.UserName
'                Session("_SvcTypeITZ") = "MERCHANTDB"

'                ''change for welcome in master page
'                Session("firstNameITZ") = advUserInfo.firstName

'                Dim abc = Insert_Agent_ITZ(advUserInfo.firstName, advUserInfo.mobile, advUserInfo.email, advUserInfo.firstName, advUserInfo.customerInfo.dcode, UserLogin.Password, dset.Tables(0).Rows(0)("Agent_Type"), 0, "TA1", False, UserLogin.UserName, "", "", "", "", "", advUserInfo.customerInfo.address)

'                '' FormsAuthentication.RedirectFromLoginPage(userid, False)
'                dset = user_auth(UserLogin.UserName, UserLogin.Password, advUserInfo.customerInfo.dcode, advUserInfo.lastAccessTime)

'                ''''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                ''Session("SearchUrl") = mapUrlValAgtA2


'                Response.Redirect("Search.aspx")
'                ''''Response.Redirect(mapUrlValAgtA2)
'                ''dset = user_auth(UserLogin.UserName, UserLogin.Password, splitVals(3).Trim(), splitVals(5).Trim())
'                'If ddllog.SelectedItem.Value.ToUpper().Equals("M") Then

'            End If


'            ''''dset = user_auth(userid, pwd)

'            If dset IsNot Nothing AndAlso dset.Tables.Count > 0 AndAlso dset.Tables(0).Rows.Count > 0 Then
'                Dim userTpe As String = dset.Tables(0).Rows(0)("Name").ToString()
'                If UserLogin.UserName.Trim().ToUpper().Contains("ITZ") AndAlso Not String.IsNullOrEmpty(advUserInfo.error) And userTpe <> "EXEC" And userTpe <> "ACC" And userTpe <> "ADMIN" Then
'                    Dim lblerrItz As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
'                    lblerrItz.Text = advUserInfo.error
'                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "Not a Valid ID" Then
'                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
'                    lblerr.Text = "Your UserID Seems to be Incorrect"
'                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "incorrect password" Then
'                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
'                    lblerr.Text = "Your Password Seems to be Incorrect"
'                Else
'                    id = dset.Tables(0).Rows(0)("UID").ToString()
'                    usertype = dset.Tables(0).Rows(0)("UserType").ToString()
'                    typeid = dset.Tables(0).Rows(0)("TypeID").ToString()
'                    User = dset.Tables(0).Rows(0)("Name").ToString()
'                    If usertype = "TA" Then
'                        AgencyName = dset.Tables(0).Rows(0)("Name").ToString()
'                        Session("AgencyName") = AgencyName
'                    End If

'                    Session("_PASSWORD") = UserLogin.Password
'                    Session("_USERNAME") = UserLogin.UserName
'                    Session("ModeTypeITZ") = "WEB"
'                    Session("UID") = id
'                    Session("UserType") = usertype
'                    Session("TypeID") = typeid
'                    Session("User_Type") = User
'                    If usertype.Trim.ToUpper = "TA" Then
'                        Session("agent_type") = dset.Tables(0).Rows(0)("agent_type").ToString()
'                    End If

'                    Session("firstNameITZ") = id
'                    Session("IsCorp") = False
'                    FormsAuthentication.RedirectFromLoginPage(userid, False)

'                    If User = "ACC" Then
'                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = " "
'                        ' ''Response.Redirect(mapUrlVal)
'                    ElseIf User = "ADMIN" Then
'                        Session("ADMINLogin") = userid
'                        Response.Redirect("Search.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = mapUrlVal
'                        ' ''Response.Redirect(mapUrlVal)
'                    ElseIf User = "EXEC" Then
'                        ''''Session("ExecTrip") = dset.Tables(0).Rows(0)("Trip").ToString()
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/admin/profile.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Dim mapUrlValExec = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ''''Session("SearchUrl") = mapUrlValExec
'                        Try
'                            Session("TripExec") = dset.Tables(0).Rows(0)("Trip").ToString()
'                        Catch ex As Exception
'                        End Try

'                        Response.Redirect("SprReports/admin/profile.aspx")
'                        ''''Response.Redirect(mapUrlVal)
'                    ElseIf User = "AGENT" And typeid = "TA1" Then
'                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
'                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp"))
'                        End If
'                        Response.Redirect("Search.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = mapUrlVal
'                        ' ''Response.Redirect(mapUrlVal)
'                    ElseIf User = "AGENT" And typeid = "TA2" Then
'                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
'                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp").ToString())
'                        End If
'                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = mapUrlValAgtA2
'                        ' ''Response.Redirect(mapUrlVal)
'                    ElseIf User = "SALES" Then
'                        Response.Redirect("Search.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = mapUrlVal
'                        ' ''Response.Redirect(mapUrlVal)
'                    ElseIf usertype = "DI" Then
'                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
'                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Dim mapUrlValDI = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
'                        ' ''Session("SearchUrl") = mapUrlValDI
'                        ' ''Response.Redirect(mapUrlVal)
'                        'END CHANGES FOR DISTR
'                    End If
'                End If
'            End If
'            ''''End If
'        Catch ex As Exception
'            'lblerr.Text = ex.Message
'            If UserLogin.UserName.ToUpper().Trim() = "TEST02" Then


'                Session("_PASSWORD") = UserLogin.Password
'                Session("_USERNAME") = UserLogin.UserName
'                Session("ModeTypeITZ") = "WEB"
'                Session("UID") = UserLogin.UserName
'                Session("UserType") = "TA"
'                Session("TypeID") = "TA1"
'                Session("User_Type") = "AGENT"

'                Session("agent_type") = "Type1"


'                Session("firstNameITZ") = UserLogin.UserName
'                Session("IsCorp") = False
'                FormsAuthentication.RedirectFromLoginPage(userid, False)
'                Response.Redirect("Search.aspx")
'            End If

'        End Try
'    End Sub
'    Public Function user_auth(ByVal uid As String, ByVal passwd As String, ByVal decod As String, ByVal lastlog As String) As DataSet

'        adap = New SqlDataAdapter("UserLogin", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
'        adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd)
'        adap.SelectCommand.Parameters.AddWithValue("@parmdecodeitz", decod)
'        adap.SelectCommand.Parameters.AddWithValue("@parmlastloginitz", lastlog)
'        Dim ds As New DataSet()
'        adap.Fill(ds)
'        Return ds
'    End Function
'    Public Function Insert_Agent_ITZ(ByVal fname As String, ByVal mbl As String, ByVal email As String, ByVal agencyname As String, ByVal user_id As String,
'                                     ByVal pwd As String, ByVal agent_type As String, ByVal crdLimit As Decimal, ByVal status As String, ByVal isCorp As Boolean,
'                                     ByVal decodeITZ As String, ByVal mKeyITZ As String, ByVal pwdITZ As String, ByVal lastLoginITZ As String, ByVal modeITZ As String,
'                                     ByVal svcITZ As String, ByVal addITZ As String) As Boolean
'        Dim inst As Boolean = False
'        Dim rows As Integer
'        Dim sqlConn As New SqlConnection()

'        Try
'            sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
'            Dim cmdITZ As New SqlCommand("USP_INSERT_AGENT_ITZ", sqlConn)
'            cmdITZ.CommandType = CommandType.StoredProcedure
'            cmdITZ.Parameters.AddWithValue("@FNAME", fname)
'            cmdITZ.Parameters.AddWithValue("@MOBILE", mbl)
'            cmdITZ.Parameters.AddWithValue("@EMAIL", email)
'            cmdITZ.Parameters.AddWithValue("@AGENCY_NAME", agencyname)
'            cmdITZ.Parameters.AddWithValue("@USER_ID", user_id)
'            cmdITZ.Parameters.AddWithValue("@PWD", pwd)
'            cmdITZ.Parameters.AddWithValue("@AGENT_TYPE", agent_type)
'            cmdITZ.Parameters.AddWithValue("@CRD_LIMIT", crdLimit)
'            cmdITZ.Parameters.AddWithValue("@STATUS", status)
'            cmdITZ.Parameters.AddWithValue("@ISCORP", isCorp)
'            cmdITZ.Parameters.AddWithValue("@DECODE", decodeITZ)
'            cmdITZ.Parameters.AddWithValue("@MRCHNT_KEY", mKeyITZ)
'            cmdITZ.Parameters.AddWithValue("@PWD_ITZ", pwdITZ)
'            cmdITZ.Parameters.AddWithValue("@LASTLOGIN_ITZ", lastLoginITZ)
'            cmdITZ.Parameters.AddWithValue("@MODETYPE_ITZ", modeITZ)
'            cmdITZ.Parameters.AddWithValue("@SVCTYPE_ITZ", svcITZ)
'            cmdITZ.Parameters.AddWithValue("@Add_ITZ", addITZ)
'            sqlConn.Open()
'            rows = cmdITZ.ExecuteNonQuery()
'            sqlConn.Close()
'            If rows > 0 Then
'                inst = True
'            End If
'        Catch ex As Exception

'        End Try
'        Return inst
'    End Function


'End Class





''Imports System.Collections.Generic
''Imports System.Linq
''Imports System.Web
''Imports System.Web.UI
''Imports System.Web.UI.WebControls
''Imports System.Data.SqlClient
''Imports System.Data
''Imports System.Configuration
''Imports System.Web.Security
''Imports IPTracker
''Imports ITZLib
''Imports System.Xml.Linq

''Partial Class UserControl_LoginControl
''    Inherits System.Web.UI.UserControl
''    Dim userid As String
''    Dim pwd As String
''    Dim AgencyName As String
''    Dim User As String
''    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
''    Dim adap As SqlDataAdapter
''    Dim id As String
''    Dim usertype As String
''    Dim typeid As String
''    Dim dset As DataSet
''    Private det As New Details()
''    Dim objUm As New UrlMapping()

''#Region "Pawan Kumar"
''    Dim objVL As New _validateLogin()
''    Dim objSvcLgn As New ITZLoginUsers()
''    Dim loginMsg As String
''    Dim splitVals As String()
''    Dim objAdvParm As New AdvLoginPar()
''    Dim advUserInfo As New UserInfo()
''#End Region

''    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

''    End Sub



''    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
''        Dim umMapp As String = ""
''        Dim custmUrl As String = ""
''        Dim hashedpwd As String = ""
''        Dim dsUrlRe As New DataSet()
''        Try
''            Dim ddllog As DropDownList
''            ddllog = CType(UserLogin.FindControl("ddlLogType"), DropDownList)
''            userid = UserLogin.UserName
''            pwd = UserLogin.Password

''            ''objVL.strModeType = "WEB"
''            ''objVL.strPassword = UserLogin.Password.Trim()
''            ''objVL.strUserName = UserLogin.UserName.Trim()
''            objAdvParm.strModeType = "WEB"
''            objAdvParm.strPassword = UserLogin.Password.Trim()
''            objAdvParm.strUserName = UserLogin.UserName.Trim()
''            If UserLogin.UserName.ToUpper().Contains("ITZ") Then
''                Try
''                    hashedpwd = VGCheckSum.genPassHash(objAdvParm.strPassword.Trim())
''                    objAdvParm.strPassword = hashedpwd.Trim()
''                    ''loginMsg = objSvcLgn.ITZLoginUser(objVL)
''                    advUserInfo = objSvcLgn.Advance_Login(objAdvParm)
''                    splitVals = loginMsg.Split("~")
''                Catch ex As Exception
''                End Try
''            End If
''            ''''If (splitVals.Length = 6) Then
''            ''''dset = user_auth(UserLogin.UserName, UserLogin.Password, "D000010496", "2016-01-22 11:24 AM")
''            custmUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
''            Dim urlDoc = XDocument.Load(Context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
''            Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
''            Session("ModeTypeITZ") = "WEB"
''            Session("MchntKeyITZ") = ConfigurationManager.AppSettings("MerchantKey").ToString()
''            Session("_SvcTypeITZ") = "MERCHANTDB"

''            ''If ddllog.SelectedItem.Value.ToUpper().Equals("C") AndAlso splitVals.Length = 6 Then

''            dset = user_auth(UserLogin.UserName, UserLogin.Password, "", "")

''            If dset.Tables.Count > 0 And Convert.ToString(dset.Tables(0).Rows(0)("UID")) = "Not a Valid ID" And UserLogin.UserName.ToUpper().Contains("ITZ") Then

''                Session("_PASSWORD") = hashedpwd
''                Session("_USERNAME") = UserLogin.UserName


''                ''çhanged for decode
''                ''Session("UID") = UserLogin.UserName
''                Session("UID") = advUserInfo.customerInfo.dcode

''                Session("UserType") = "TA"
''                Session("TypeID") = "TA1"
''                Session("User_Type") = "AGENT"
''                Session("IsCorp") = False

''                Session("AgencyName") = advUserInfo.lastName
''                Session("AGTY") = "TYPE1"
''                Session("agent_type") = "TYPE1"
''                ''"DEFAULT_ITILHE"
''                ''''Session("ModeTypeITZ") = ds.Tables(0).Rows(0)("ModeType_ITZ").ToString().Trim()


''                Session("_DCODE") = advUserInfo.customerInfo.dcode

''                ''Session("_DCODE") = UserLogin.UserName
''                Session("_SvcTypeITZ") = "MERCHANTDB"

''                ''change for welcome in master page
''                Session("firstNameITZ") = advUserInfo.firstName

''                Dim abc = Insert_Agent_ITZ(advUserInfo.firstName, advUserInfo.mobile, advUserInfo.email, advUserInfo.firstName, advUserInfo.customerInfo.dcode, UserLogin.Password, "TYPE1", 0, "TA1", False, UserLogin.UserName, "", "", "", "", "", advUserInfo.customerInfo.address)

''                '' FormsAuthentication.RedirectFromLoginPage(userid, False)
''                dset = user_auth(UserLogin.UserName, UserLogin.Password, advUserInfo.customerInfo.dcode, advUserInfo.lastAccessTime)

''                ''''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                ''Session("SearchUrl") = mapUrlValAgtA2


''                Response.Redirect("Search.aspx")
''                ''''Response.Redirect(mapUrlValAgtA2)
''                ''dset = user_auth(UserLogin.UserName, UserLogin.Password, splitVals(3).Trim(), splitVals(5).Trim())
''                'If ddllog.SelectedItem.Value.ToUpper().Equals("M") Then

''            End If


''            ''''dset = user_auth(userid, pwd)

''            If dset IsNot Nothing AndAlso dset.Tables.Count > 0 AndAlso dset.Tables(0).Rows.Count > 0 Then
''                Dim userTpe As String = dset.Tables(0).Rows(0)("Name").ToString()
''                If UserLogin.UserName.Trim().ToUpper().Contains("ITZ") AndAlso Not String.IsNullOrEmpty(advUserInfo.error) And userTpe <> "EXEC" And userTpe <> "ACC" And userTpe <> "ADMIN" Then
''                    Dim lblerrItz As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
''                    lblerrItz.Text = advUserInfo.error
''                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "Not a Valid ID" Then
''                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
''                    lblerr.Text = "Your UserID Seems to be Incorrect"
''                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "incorrect password" Then
''                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
''                    lblerr.Text = "Your Password Seems to be Incorrect"
''                Else
''                    id = dset.Tables(0).Rows(0)("UID").ToString()
''                    usertype = dset.Tables(0).Rows(0)("UserType").ToString()
''                    typeid = dset.Tables(0).Rows(0)("TypeID").ToString()
''                    User = dset.Tables(0).Rows(0)("Name").ToString()
''                    If usertype = "TA" Then
''                        AgencyName = dset.Tables(0).Rows(0)("Name").ToString()
''                        Session("AgencyName") = AgencyName
''                    End If

''                    Session("_PASSWORD") = UserLogin.Password
''                    Session("_USERNAME") = UserLogin.UserName
''                    Session("ModeTypeITZ") = "WEB"
''                    Session("UID") = id
''                    Session("UserType") = usertype
''                    Session("TypeID") = typeid
''                    Session("User_Type") = User
''                    If usertype.Trim.ToUpper = "TA" Then
''                        Session("agent_type") = dset.Tables(0).Rows(0)("agent_type").ToString()
''                    End If

''                    Session("firstNameITZ") = id
''                    Session("IsCorp") = False
''                    FormsAuthentication.RedirectFromLoginPage(userid, False)

''                    If User = "ACC" Then
''                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = " "
''                        ' ''Response.Redirect(mapUrlVal)
''                    ElseIf User = "ADMIN" Then
''                        Session("ADMINLogin") = userid
''                        Response.Redirect("Search.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = mapUrlVal
''                        ' ''Response.Redirect(mapUrlVal)
''                    ElseIf User = "EXEC" Then
''                        ''''Session("ExecTrip") = dset.Tables(0).Rows(0)("Trip").ToString()
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/admin/profile.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Dim mapUrlValExec = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ''''Session("SearchUrl") = mapUrlValExec
''                        Try
''                            Session("TripExec") = dset.Tables(0).Rows(0)("Trip").ToString()
''                        Catch ex As Exception
''                        End Try

''                        Response.Redirect("SprReports/admin/profile.aspx")
''                        ''''Response.Redirect(mapUrlVal)
''                    ElseIf User = "AGENT" And typeid = "TA1" Then
''                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
''                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp"))
''                        End If
''                        Response.Redirect("Search.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = mapUrlVal
''                        ' ''Response.Redirect(mapUrlVal)
''                    ElseIf User = "AGENT" And typeid = "TA2" Then
''                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
''                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp").ToString())
''                        End If
''                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = mapUrlValAgtA2
''                        ' ''Response.Redirect(mapUrlVal)
''                    ElseIf User = "SALES" Then
''                        Response.Redirect("Search.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = mapUrlVal
''                        ' ''Response.Redirect(mapUrlVal)
''                    ElseIf usertype = "DI" Then
''                        Response.Redirect("SprReports/Accounts/Ledger.aspx")
''                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "SprReports/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Dim mapUrlValDI = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
''                        ' ''Session("SearchUrl") = mapUrlValDI
''                        ' ''Response.Redirect(mapUrlVal)
''                        'END CHANGES FOR DISTR
''                    End If
''                End If
''            End If
''            ''''End If
''        Catch ex As Exception
''            'lblerr.Text = ex.Message

''        End Try
''    End Sub
''    Public Function user_auth(ByVal uid As String, ByVal passwd As String, ByVal decod As String, ByVal lastlog As String) As DataSet

''        adap = New SqlDataAdapter("UserLogin", con)
''        adap.SelectCommand.CommandType = CommandType.StoredProcedure
''        adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
''        adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd)
''        adap.SelectCommand.Parameters.AddWithValue("@parmdecodeitz", decod)
''        adap.SelectCommand.Parameters.AddWithValue("@parmlastloginitz", lastlog)
''        Dim ds As New DataSet()
''        adap.Fill(ds)
''        Return ds
''    End Function
''    Public Function Insert_Agent_ITZ(ByVal fname As String, ByVal mbl As String, ByVal email As String, ByVal agencyname As String, ByVal user_id As String,
''                                     ByVal pwd As String, ByVal agent_type As String, ByVal crdLimit As Decimal, ByVal status As String, ByVal isCorp As Boolean,
''                                     ByVal decodeITZ As String, ByVal mKeyITZ As String, ByVal pwdITZ As String, ByVal lastLoginITZ As String, ByVal modeITZ As String,
''                                     ByVal svcITZ As String, ByVal addITZ As String) As Boolean
''        Dim inst As Boolean = False
''        Dim rows As Integer
''        Dim sqlConn As New SqlConnection()

''        Try
''            sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
''            Dim cmdITZ As New SqlCommand("USP_INSERT_AGENT_ITZ", sqlConn)
''            cmdITZ.CommandType = CommandType.StoredProcedure
''            cmdITZ.Parameters.AddWithValue("@FNAME", fname)
''            cmdITZ.Parameters.AddWithValue("@MOBILE", mbl)
''            cmdITZ.Parameters.AddWithValue("@EMAIL", email)
''            cmdITZ.Parameters.AddWithValue("@AGENCY_NAME", agencyname)
''            cmdITZ.Parameters.AddWithValue("@USER_ID", user_id)
''            cmdITZ.Parameters.AddWithValue("@PWD", pwd)
''            cmdITZ.Parameters.AddWithValue("@AGENT_TYPE", agent_type)
''            cmdITZ.Parameters.AddWithValue("@CRD_LIMIT", crdLimit)
''            cmdITZ.Parameters.AddWithValue("@STATUS", status)
''            cmdITZ.Parameters.AddWithValue("@ISCORP", isCorp)
''            cmdITZ.Parameters.AddWithValue("@DECODE", decodeITZ)
''            cmdITZ.Parameters.AddWithValue("@MRCHNT_KEY", mKeyITZ)
''            cmdITZ.Parameters.AddWithValue("@PWD_ITZ", pwdITZ)
''            cmdITZ.Parameters.AddWithValue("@LASTLOGIN_ITZ", lastLoginITZ)
''            cmdITZ.Parameters.AddWithValue("@MODETYPE_ITZ", modeITZ)
''            cmdITZ.Parameters.AddWithValue("@SVCTYPE_ITZ", svcITZ)
''            cmdITZ.Parameters.AddWithValue("@Add_ITZ", addITZ)
''            sqlConn.Open()
''            rows = cmdITZ.ExecuteNonQuery()
''            sqlConn.Close()
''            If rows > 0 Then
''                inst = True
''            End If
''        Catch ex As Exception

''        End Try
''        Return inst
''    End Function


''End Class

