Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ITZLib

Partial Class Login
    Inherits System.Web.UI.Page

    Dim userid As String
    Dim pwd As String
    Dim AgencyName As String
    Dim User As String
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As SqlDataAdapter
    Dim id As String
    Dim usertype As String
    Dim typeid As String
    Dim dset As DataSet
    Private det As New Details()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim STDOM As New SqlTransactionDom
        'Dim MailDt As New DataTable
        'MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

        'Try
        '    If (MailDt.Rows.Count > 0) Then
        '        STDOM.SendMail("paritoshsingh1984@gmail.com", "info@RWT.com", "", "", "", "", "", "hi", "Ticket Refund Request", "")
        '    End If
        'Catch ex As Exception

        'End Try

        'Dim objVL = New _validateLogin()
        'Dim objSvcLgn = New ITZLoginUsers()
        'Dim loginMsg As String
        'Dim objSaml As New SamlReqResp()
        Try
            'objVL.strModeType = "WEB"
            'objVL.strPassword = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
            ' ''objVL.strPassword = "D000010496"
            'objVL.strUserName = "ITZIC01161"
            ''blncReqVal._DCODE = "D000010496"
            ''blncReqVal._PASSWORD = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
            ''blncReqVal._USERNAME = "ITZIC01161"
            ''blncReqVal._MERCHANT_KEY = "DEFAULT_ITILHE"
            ''objSaml.CreateSamlRequest()
            ''objBlnc = objGetBlnc.GetBalanceCustomer(blncReqVal)
            'loginMsg = objSvcLgn.ITZLoginUser(objVL)
            'Dim splitVals = loginMsg.Split("~")
            'If (splitVals.Length = 6) Then
            '    ''objBalParm._DCODE = splitVals(3).Trim()
            '    ''objBalParm._MERCHANT_KEY = "DEFAULT_ITILHE"
            '    ''objBalParm._PASSWORD = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
            '    ''objBalParm._USERNAME = "ITZIC01161"
            '    'Session("_DCODE") = splitVals(3).Trim()
            '    'Session("_MERCHANT_KEY") = ConfigurationManager.AppSettings("MerchantKey").Trim()
            '    Session("_PASSWORD") = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
            '    Session("_USERNAME") = splitVals(0).Trim()
            '    Session("ModeTypeITZ") = "WEB"
            '    '    Dim bal = objSvcGetBal.GetBalanceCustomer(objBalParm)
            '    '    If (Not String.IsNullOrEmpty(splitVals(0).Trim())) Then
            '    '        'Session("UID") = "test02" ''splitVals(0).Trim()
            '    '    End If
            '    '    'Session("AgencyName") = "test agency1" ''splitVals(2).Trim()
            '    '    'Session("UserType") = "AGENT"
            '    '    'Session("TypeID") = "TA1"
            '    '    'Session("User_Type") = "type1" ''"TA"
            '    '    'Session("IsCorp") = False
            '    '    'Session("ADMINLogin") = ""
            '    '    'Session("ExecTrip") = ""
            '    '    'Response.Redirect("Search.aspx")
            '    '    'Response.Redirect("Login.aspx")
            'Else
            '    'Session("_DCODE") = " "
            '    Session("_PASSWORD") = " "
            '    Session("_USERNAME") = " "
            '    Session("ModeTypeITZ") = ""
            'End If
            'LogInValidUser(splitVals(0).Trim(), "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig=", splitVals(3).Trim(), splitVals(5).Trim())
            ''LogInValidUser("test02", "test2", splitVals(3).Trim(), splitVals(5).Trim())
        Catch ex As Exception
            ''Response.Write("Message: " + DirectCast(ex, System.NullReferenceException).Message.ToString() + "<br/>")
            ''Dim inner = ex.InnerException
            ''If inner IsNot Nothing Then
            ''    Response.Write("Inner Exception: " + inner.InnerException.ToString())
            ''Else
            ''    Response.Write("Inner Exception: Nothing" + "<br/>")
            ''End If
            ''Response.Write(" Stack Trace: " + DirectCast(ex, System.NullReferenceException).StackTrace.ToString() + "<br/>")
        End Try
        'Dim objVL = New _validateLogin()
        'Dim objSvcLgn = New ITZLoginUsers()
        'Dim objSvcGetBal = New ITZGetbalance()
        'Dim objBalParm = New _GetBalance()
        'Dim loginMsg As String
        'Try
        '    objVL.strModeType = "WEB"
        '    objVL.strPassword = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
        '    'objVL.strPassword = "D000010496"
        '    objVL.strUserName = "ITZIC01161"
        '    'loginMsg = objSvcLgn.ITZLoginUser(objVL)
        '    'Dim splitVals = loginMsg.Split("~")
        '    'If (splitVals.Length = 6) Then
        '    '    objBalParm._DCODE = splitVals(3).Trim()
        '    '    objBalParm._MERCHANT_KEY = "DEFAULT_ITILHE"
        '    '    objBalParm._PASSWORD = "admOKbtKZkaqLgkjAZhWDU4jlxHFXXNUkfntpJx/6ig="
        '    '    objBalParm._USERNAME = "ITZIC01161"
        '    '    Dim bal = objSvcGetBal.GetBalanceCustomer(objBalParm)
        '    '    If (Not String.IsNullOrEmpty(splitVals(0).Trim())) Then
        '    '        'Session("UID") = "test02" ''splitVals(0).Trim()
        '    '    End If
        '    '    'Session("AgencyName") = "test agency1" ''splitVals(2).Trim()
        '    '    'Session("UserType") = "AGENT"
        '    '    'Session("TypeID") = "TA1"
        '    '    'Session("User_Type") = "type1" ''"TA"
        '    '    'Session("IsCorp") = False
        '    '    'Session("ADMINLogin") = ""
        '    '    'Session("ExecTrip") = ""
        '    '    'Response.Redirect("Search.aspx")
        '    '    'Response.Redirect("Login.aspx")
        '    'Else
        '    'End If
        '    LogInValidUser("test02", "test2")
        'Catch ex As Exception

        'End Try
    End Sub
    Public Sub LogInValidUser(ByVal uid As String, ByVal upwd As String, ByVal decod As String, ByVal lastlog As String)
        Try

            userid = uid
            pwd = upwd
            dset = user_auth(userid, pwd, decod, lastlog)
            If dset.Tables(0).Rows(0)(0).ToString() = "Not a Valid ID" Then
                'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                'lblerr.Text = "Your UserID Seems to be Incorrect"
            ElseIf dset.Tables(0).Rows(0)(0).ToString() = "incorrect password" Then
                'Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                'lblerr.Text = "Your Password Seems to be Incorrect"
            Else
                id = dset.Tables(0).Rows(0)("UID").ToString()
                usertype = dset.Tables(0).Rows(0)("UserType").ToString()
                typeid = dset.Tables(0).Rows(0)("TypeID").ToString()
                User = dset.Tables(0).Rows(0)("Name").ToString()
                If usertype = "TA" Then
                    AgencyName = dset.Tables(0).Rows(0)("Name").ToString()
                    Session("AgencyName") = AgencyName
                End If

                Session("UID") = id
                Session("UserType") = usertype
                Session("TypeID") = typeid
                Session("User_Type") = User
                Session("IsCorp") = False
                FormsAuthentication.RedirectFromLoginPage(userid, False)

                If User = "ACC" Then
                    Response.Redirect("Report/Accounts/Ledger.aspx")
                ElseIf User = "ADMIN" Then
                    Session("ADMINLogin") = userid
                    Response.Redirect("Search.aspx")
                    'Response.Redirect("/FlightSearch")
                    'Response.Redirect("/Report/Abc.aspx?Value1=myval1&Value2=myval2")
                    'Response.Redirect(GetMappedString("/Report/Abc.aspx") + "?Value1=myval1&Value2=myval2")
                ElseIf User = "EXEC" Then
                    Session("ExecTrip") = dset.Tables(0).Rows(0)("Trip").ToString()
                    Response.Redirect("Report/admin/profile.aspx")
                ElseIf User = "AGENT" And typeid = "TA1" Then
                    If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                        Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp"))
                    End If
                    Response.Redirect("Search.aspx")
                    'Response.Redirect("/FlightSearch")
                    'Response.Redirect("/Report/Abc.aspx?Value1=myval1&Value2=myval2")
                    'Response.Redirect(GetMappedString("/Report/Abc.aspx") + "?Value1=myval1&Value2=myval2")
                ElseIf User = "AGENT" And typeid = "TA2" Then
                    If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                        Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp").ToString())
                    End If
                    Response.Redirect("Report/Accounts/Ledger.aspx")
                ElseIf User = "SALES" Then
                    Response.Redirect("Search.aspx")
                    'Response.Redirect("/FlightSearch")
                    'Response.Redirect("/Report/Abc.aspx?Value1=myval1&Value2=myval2")
                    'Response.Redirect(GetMappedString("/Report/Abc.aspx") + "?Value1=myval1&Value2=myval2")
                ElseIf usertype = "DI" Then

                    Response.Redirect("Report/Accounts/Ledger.aspx")
                    'END CHANGES FOR DISTR
                End If


            End If
        Catch ex As Exception
            'lblerr.Text = ex.Message

        End Try
    End Sub
    Public Function GetMappedString(ByVal pagename As String) As String
        Dim mappped As String
        Dim conn As New SqlConnection()
        Try
            conn.ConnectionString = "Data Source=172.18.80.75;Initial Catalog=ITZ;User ID=sa;Password=Password123;Max Pool Size=1000"
            Dim cmd As New SqlCommand("select mapurl from Tbl_Urls_To_Map where pagename='" + pagename.Trim() + "'")
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = conn
            If (conn.State = Data.ConnectionState.Closed) Then
                conn.Open()
            End If
            mappped = cmd.ExecuteScalar()
            If conn.State = Data.ConnectionState.Open Then
                conn.Close()
            End If
            cmd.Dispose()
        Catch ex As Exception
            Dim abc As String
        End Try
        Return mappped
    End Function
    Public Function user_auth(ByVal uid As String, ByVal passwd As String, ByVal decod As String, ByVal lastlog As String) As DataSet

        adap = New SqlDataAdapter("UserLogin", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
        adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd)
        adap.SelectCommand.Parameters.AddWithValue("@parmdecodeitz", decod)
        adap.SelectCommand.Parameters.AddWithValue("@parmlastloginitz", lastlog)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    'Dim conn As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    'Public User_Type As String
    'Dim Email As String
    'Dim FirstName As String
    'Dim LastName As String
    'Dim AgencyName As String
    'Dim Address As String
    'Dim City As String
    'Dim State As String
    'Dim Country As String
    'Dim Agnttype As String
    'Dim Dist As String
    'Dim Zip As String
    'Dim Phone As String
    'Dim credit_limit
    'Dim Ex_Id As String
    'Dim exe_dept As String
    'Dim agent_type As String
    'Dim agent_status As String
    'Dim user_agcc As String
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    'If Page.IsPostBack Then

    '    Response.Buffer = True
    '    Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
    '    Response.Expires = -1500
    '    Response.CacheControl = "no-cache"
    '    'End If
    '    User_Type = Request("user_type")
    '    user_agcc = Request("agcc")
    '    Session("User_Type") = ""
    '    Session("UID") = ""
    '    If Session("User_Type") = "ADMIN" And Session("UID") <> "" Then
    '        Response.Redirect("ibe.aspx")
    '    ElseIf Session("User_Type") = "AGENT" And Session("UID") <> "" Then
    '        Response.Redirect("ibe.aspx")
    '    End If
    '    DirectCast(Login1.FindControl("FailureText"), Literal).Text = ""
    'End Sub
    'Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
    '    Dim Authenticated As Boolean = False
    '    Session("User_Type") = User_Type
    '    ''Session Use end
    '    If User_Type = "AGENT" And user_agcc = "TA" Then  'Check Agent Authentication
    '        Authenticated = AuthenticationMethod(Login1.UserName, Login1.Password, User_Type)
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            FormsAuthentication.RedirectFromLoginPage("agent", False)
    '            Session("UID") = Email
    '            Session("UserId") = Email
    '            Session("FN") = FirstName
    '            Session("LN") = LastName
    '            Session("CL") = credit_limit
    '            Session("DI") = Dist
    '            Session("AGTY") = agent_type
    '            Session("agent_status") = agent_status
    '            Response.Redirect("IBE.aspx?agent_type=" & agent_type & "") '?Email=" & Email & "&FirstName=" & FirstName & "&LastName=" & LastName & "&AgencyName=" & AgencyName & "&Address=" & Address & "&City=" & City & "&Agnttype=" & Agnttype & "&Phone=" & Phone & "&Dist=" & Dist & "&User_Type=" & User_Type & "", True)
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If
    '    ElseIf User_Type = "AGENT" And user_agcc = "CC" Then  'Check Agent Authentication
    '        Authenticated = AuthenticationMethod_CC(Login1.UserName, Login1.Password, User_Type)
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            FormsAuthentication.RedirectFromLoginPage("agent", False)
    '            Session("UID") = Email
    '            Session("FN") = FirstName
    '            Session("LN") = LastName
    '            Session("CL") = credit_limit
    '            Session("DI") = Dist
    '            Session("AGTY") = agent_type
    '            Session("agent_status") = agent_status
    '            Response.Redirect("IBE.aspx?agent_type=" & agent_type & "") '?Email=" & Email & "&FirstName=" & FirstName & "&LastName=" & LastName & "&AgencyName=" & AgencyName & "&Address=" & Address & "&City=" & City & "&Agnttype=" & Agnttype & "&Phone=" & Phone & "&Dist=" & Dist & "&User_Type=" & User_Type & "", True)
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If
    '    ElseIf User_Type = "ADMIN" Then 'Check Admin Authentication
    '        Authenticated = AuthenticationMethod_admin(Login1.UserName, Login1.Password)
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            FormsAuthentication.RedirectFromLoginPage("agents", False)
    '            Session("UID") = Email
    '            Session("FN") = FirstName
    '            Session("LN") = LastName
    '            Session("CL") = credit_limit
    '            Session("DI") = Dist
    '            Response.Redirect("IBE.aspx?agent_type=Admin") '?Email=" & Email & "&FirstName=" & FirstName & "&LastName=" & LastName & "&AgencyName=" & AgencyName & "&Address=" & Address & "&City=" & City & "&Agnttype=" & Agnttype & "&Phone=" & Phone & ",&Dist=" & Dist & ",&User_Type=" & User_Type & "", True)
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If
    '    ElseIf User_Type = "DIST" Then
    '        Authenticated = AuthenticationMethod(Login1.UserName, Login1.Password, User_Type)
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            FormsAuthentication.RedirectFromLoginPage("agents", False)
    '            Session("UID") = Email
    '            Session("FN") = FirstName
    '            Session("LN") = LastName
    '            Session("CL") = credit_limit
    '            Response.Redirect("IBE.aspx") '?Email=" & Email & "&FirstName=" & FirstName & "&LastName=" & LastName & "&AgencyName=" & AgencyName & "&Address=" & Address & "&City=" & City & "&Agnttype=" & Agnttype & "&Phone=" & Phone & ",&Dist=" & Dist & ",&User_Type=" & User_Type & "", True)
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If



    '    ElseIf User_Type = "SALES" Then
    '        Authenticated = AuthenticationMethod_Sales(Login1.UserName, Login1.Password, User_Type)
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            FormsAuthentication.RedirectFromLoginPage("agents", False)
    '            Session("UID") = Email
    '            Session("FN") = FirstName
    '            Session("LN") = LastName

    '            Response.Redirect("AgentDealSeat.aspx?type=Sales") '?Email=" & Email & "&FirstName=" & FirstName & "&LastName=" & LastName & "&AgencyName=" & AgencyName & "&Address=" & Address & "&City=" & City & "&Agnttype=" & Agnttype & "&Phone=" & Phone & ",&Dist=" & Dist & ",&User_Type=" & User_Type & "", True)
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If


    '    ElseIf User_Type = "EXEC" Then
    '        Authenticated = Authentication_exe(Login1.UserName, Login1.Password, "EXEC")
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            Session("UID") = Ex_Id
    '            Session("DEPT") = exe_dept
    '            If (exe_dept = "E") Then
    '                Response.Redirect("Hold_pnr.aspx")
    '            Else
    '                DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Invaid Department. Please contact Administrator."
    '                Session("User_Type") = ""
    '            End If
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If
    '    ElseIf User_Type = "ACC" Then
    '        Authenticated = Authentication_exe(Login1.UserName, Login1.Password, "ACC")
    '        e.Authenticated = Authenticated
    '        If Authenticated = True Then
    '            Session("UID") = Ex_Id
    '            Session("DEPT") = exe_dept
    '            If (exe_dept = "A") Then
    '                Response.Redirect("Account.aspx")
    '            Else
    '                DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Invaid Department. Please contact Administrator."
    '                Session("User_Type") = ""
    '            End If
    '        Else
    '            DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your login attempt was not successful. Please try again."
    '            Session("User_Type") = ""
    '        End If
    '    Else
    '        DirectCast(Login1.FindControl("FailureText"), Literal).Text = "Your are not a valid User."
    '        Session("User_Type") = ""
    '    End If
    'End Sub
    'Private Function Authentication_exe(ByVal UserName As String, ByVal Password As String, ByVal usertype As String) As Boolean
    '    Try
    '        Dim boolReturnValue As Boolean = False
    '        Dim Connection As New SqlConnection(conn)
    '        Connection.Open()

    '        Dim strSQL As String
    '        If usertype = "EXEC" Then
    '            strSQL = "SELECT * FROM Execu where Dept='e'"
    '        ElseIf usertype = "ACC" Then
    '            strSQL = "SELECT * FROM Execu where Dept='a'"
    '        End If
    '        UserName = UserName
    '        Password = Password
    '        Dim adp As SqlDataAdapter
    '        adp = New SqlDataAdapter(strSQL, Connection)
    '        Dim dt As New DataTable
    '        adp.Fill(dt)
    '        Dim i As Integer
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("user_pwd")).ToString())) Then
    '                    Ex_Id = UCase((dt.Rows(i).Item("user_id")).ToString())
    '                    exe_dept = UCase((dt.Rows(i).Item("dept")).ToString())
    '                    boolReturnValue = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        Return boolReturnValue
    '    Catch
    '    End Try
    'End Function
    'Private Function AuthenticationMethod_admin(ByVal UserName As String, ByVal Password As String) As Boolean
    '    Try
    '        Dim boolReturnValue As Boolean = False
    '        Dim Connection As New SqlConnection(conn)
    '        Connection.Open()
    '        Dim strSQL As String = "SELECT * FROM ADMIN_b2b"
    '        UserName = UserName
    '        Password = Password
    '        Dim adp As SqlDataAdapter
    '        adp = New SqlDataAdapter(strSQL, Connection)
    '        Dim dt As New DataTable
    '        adp.Fill(dt)
    '        Dim i As Integer
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("user_pwd")).ToString())) Then
    '                    Email = UCase((dt.Rows(i).Item("user_id")).ToString())
    '                    FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
    '                    LastName = UCase((dt.Rows(i).Item("lname")).ToString())
    '                    Address = UCase((dt.Rows(i).Item("address")).ToString())
    '                    City = UCase((dt.Rows(i).Item("city")).ToString())
    '                    Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
    '                    Country = UCase((dt.Rows(i).Item("country")).ToString())
    '                    Phone = UCase((dt.Rows(i).Item("Mobileno")).ToString())
    '                    'Agnttype = UCase((dt.Rows(i).Item("AgentType")).ToString())
    '                    'AgencyName = UCase((dt.Rows(i).Item("company")).ToString())
    '                    'Dist = UCase((dt.Rows(i).Item("Distributor")).ToString())
    '                    boolReturnValue = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        Return boolReturnValue
    '    Catch ex As Exception
    '    End Try
    'End Function

    'Private Function AuthenticationMethod(ByVal UserName As String, ByVal Password As String, ByVal User_Type As String) As Boolean
    '    Try
    '        Dim strSQL
    '        Dim uid = UserName
    '        Dim pwd = Password
    '        Dim boolReturnValue As Boolean = False
    '        Dim Connection As New SqlConnection(conn)
    '        Connection.Open()
    '        If User_Type = "DIST" Then
    '            strSQL = "SELECT * FROM New_Regs where agent_Type='DA'"
    '        Else
    '            strSQL = "SELECT * FROM New_Regs where User_Id='" & UserName & "' and (agent_Type <> 'DA')and (Status = 'TA')and (Agent_Status = 'ACTIVE')"
    '        End If
    '        Dim adp As SqlDataAdapter
    '        adp = New SqlDataAdapter(strSQL, Connection)
    '        Dim dt As New DataTable
    '        adp.Fill(dt)
    '        Dim i As Integer
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("PWD")).ToString())) Then
    '                    Email = UCase((dt.Rows(i).Item("user_id")).ToString())
    '                    FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
    '                    LastName = UCase((dt.Rows(i).Item("lname")).ToString())
    '                    Address = UCase((dt.Rows(i).Item("address")).ToString())
    '                    City = UCase((dt.Rows(i).Item("city")).ToString())
    '                    Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
    '                    Country = UCase((dt.Rows(i).Item("country")).ToString())
    '                    Phone = UCase((dt.Rows(i).Item("Mobile")).ToString())
    '                    AgencyName = UCase((dt.Rows(i).Item("Agency_Name")).ToString())
    '                    Dist = UCase((dt.Rows(i).Item("Distr")).ToString())
    '                    credit_limit = UCase((dt.Rows(i).Item("Crd_Limit")).ToString())
    '                    agent_type = dt.Rows(i).Item("Agent_Type").ToString()
    '                    agent_status = dt.Rows(i).Item("Agent_Status").ToString()
    '                    Session("AgencyName") = AgencyName
    '                    Session("Mobile") = Phone
    '                    Session("FranchiseeName") = AgencyName
    '                    Session("Address") = Address
    '                    boolReturnValue = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        Return boolReturnValue
    '    Catch
    '    End Try
    'End Function
    'Private Function AuthenticationMethod_Sales(ByVal UserName As String, ByVal Password As String, ByVal User_Type As String) As Boolean
    '    Try
    '        Dim strSQL
    '        Dim uid = UserName
    '        Dim pwd = Password
    '        Dim boolReturnValue As Boolean = False
    '        Dim Connection As New SqlConnection(conn)
    '        Connection.Open()
    '        If User_Type = "SALES" Then

    '            strSQL = "SELECT * FROM SalesExecReg where EmailId='" & UserName & "' and (Status = 'ACTIVE')"
    '        End If
    '        Dim adp As SqlDataAdapter
    '        adp = New SqlDataAdapter(strSQL, Connection)
    '        Dim dt As New DataTable
    '        adp.Fill(dt)
    '        Dim i As Integer
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If (LCase(UserName) = LCase((dt.Rows(i).Item("EmailId")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("Password")).ToString())) Then
    '                    Email = UCase((dt.Rows(i).Item("EmailId")).ToString())
    '                    FirstName = UCase((dt.Rows(i).Item("FirstName")).ToString())
    '                    LastName = UCase((dt.Rows(i).Item("LastName")).ToString())
    '                    Address = UCase((dt.Rows(i).Item("Location")).ToString())
    '                    Phone = UCase((dt.Rows(i).Item("MobileNo")).ToString())

    '                    'Session("AgencyName") = AgencyName
    '                    'Session("Mobile") = Phone
    '                    boolReturnValue = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        Return boolReturnValue
    '    Catch
    '    End Try
    'End Function
    'Private Function AuthenticationMethod_CC(ByVal UserName As String, ByVal Password As String, ByVal User_Type As String) As Boolean
    '    Try
    '        Dim strSQL
    '        Dim uid = UserName
    '        Dim pwd = Password
    '        Dim boolReturnValue As Boolean = False
    '        Dim Connection As New SqlConnection(conn)
    '        Connection.Open()
    '        If User_Type = "DIST" Then
    '            strSQL = "SELECT * FROM New_Regs where agent_Type='DA'"
    '        Else
    '            strSQL = "SELECT * FROM New_Regs where User_Id='" & UserName & "' and (agent_Type <> 'DA')and (Status = 'CC')"
    '        End If
    '        Dim adp As SqlDataAdapter
    '        adp = New SqlDataAdapter(strSQL, Connection)
    '        Dim dt As New DataTable
    '        adp.Fill(dt)
    '        Dim i As Integer
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("PWD")).ToString())) Then
    '                    Email = UCase((dt.Rows(i).Item("user_id")).ToString())
    '                    FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
    '                    LastName = UCase((dt.Rows(i).Item("lname")).ToString())
    '                    Address = UCase((dt.Rows(i).Item("address")).ToString())
    '                    City = UCase((dt.Rows(i).Item("city")).ToString())
    '                    Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
    '                    Country = UCase((dt.Rows(i).Item("country")).ToString())
    '                    Phone = UCase((dt.Rows(i).Item("Mobile")).ToString())
    '                    AgencyName = UCase((dt.Rows(i).Item("Agency_Name")).ToString())
    '                    Dist = UCase((dt.Rows(i).Item("Distr")).ToString())
    '                    credit_limit = UCase((dt.Rows(i).Item("Crd_Limit")).ToString())
    '                    agent_type = dt.Rows(i).Item("Agent_Type").ToString()
    '                    agent_status = dt.Rows(i).Item("Agent_Status").ToString()
    '                    Session("AgencyName") = AgencyName
    '                    Session("Mobile") = Phone
    '                    boolReturnValue = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '        Return boolReturnValue
    '    Catch
    '    End Try
    'End Function
    ''Private Function AuthenticationMethod_Dist(ByVal UserName As String, ByVal Password As String) As Boolean
    ''    Dim boolReturnValue As Boolean = False
    ''    Dim Connection As New SqlConnection(conn)
    ''    Connection.Open()
    ''    Dim strSQL As String = "SELECT * FROM distributor_agents"
    ''    Dim adp As SqlDataAdapter
    ''    adp = New SqlDataAdapter(strSQL, Connection)
    ''    Dim dt As New DataTable
    ''    adp.Fill(dt)
    ''    Dim i As Integer
    ''    If dt.Rows.Count > 0 Then
    ''        For i = 0 To dt.Rows.Count - 1
    ''            If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) And (LCase(Password) = LCase((dt.Rows(i).Item("user_pwd")).ToString())) Then
    ''                Email = UCase((dt.Rows(i).Item("user_id")).ToString())
    ''                FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
    ''                LastName = UCase((dt.Rows(i).Item("lname")).ToString())
    ''                Address = UCase((dt.Rows(i).Item("address")).ToString())
    ''                City = UCase((dt.Rows(i).Item("city")).ToString())
    ''                Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
    ''                Country = UCase((dt.Rows(i).Item("country")).ToString())
    ''                Phone = UCase((dt.Rows(i).Item("Mobileno")).ToString())
    ''                Agnttype = UCase((dt.Rows(i).Item("AgentType")).ToString())
    ''                AgencyName = UCase((dt.Rows(i).Item("company")).ToString())
    ''                Dist = UCase((dt.Rows(i).Item("Distributor")).ToString())
    ''                boolReturnValue = True
    ''                Exit For
    ''            End If
    ''        Next
    ''    End If
    ''    Return boolReturnValue
    ''End Function
    'Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    'End Sub
    'Protected Sub LoginButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    'End Sub
    'Public Sub display()
    '    Dim pgstr As String
    '    pgstr = "<table algin='center'><tr><td style='color:#00CC99'>Yoy Can Not Access This Page Directly</td></tr></table>"
    '    Response.Write(pgstr)
    'End Sub

    'Protected Sub LoginButton_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    'End Sub
End Class
