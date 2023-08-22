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
Imports ITZLib
Imports System.Xml.Linq

Partial Class UserControl_LoginControl
    Inherits System.Web.UI.UserControl
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
    Dim objUm As New UrlMapping()

#Region "Pawan Kumar"
    Dim objVL As New _validateLogin()
    Dim objSvcLgn As New ITZLoginUsers()
    Dim loginMsg As String
    Dim splitVals As String()
    Dim objAdvParm As New AdvLoginPar()
    Dim advUserInfo As New UserInfo()
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim umMapp As String = ""
        Dim custmUrl As String = ""
        Dim hashedpwd As String = ""
        Dim dsUrlRe As New DataSet()
        Dim RequestID As String
        Dim Fare As String
        Dim GRPUserID As String
        Dim Trip As String
        Dim ISPaxDtls As String
        Dim status As String
        Dim Triptype As String
        Dim Payment As String
        Try
            Dim ddllog As DropDownList
            ddllog = CType(UserLogin.FindControl("ddlLogType"), DropDownList)
            userid = UserLogin.UserName
            pwd = UserLogin.Password

            ''objVL.strModeType = "WEB"
            ''objVL.strPassword = UserLogin.Password.Trim()
            ''objVL.strUserName = UserLogin.UserName.Trim()
            objAdvParm.strModeType = "WEB"
            objAdvParm.strPassword = UserLogin.Password.Trim()
            objAdvParm.strUserName = UserLogin.UserName.Trim()
            If UserLogin.UserName.ToUpper().Contains("ITZ") Then
                Try
                    hashedpwd = VGCheckSum.genPassHash(objAdvParm.strPassword.Trim())
                    objAdvParm.strPassword = hashedpwd.Trim()
                    ''loginMsg = objSvcLgn.ITZLoginUser(objVL)
                    advUserInfo = objSvcLgn.Advance_Login(objAdvParm)
                    splitVals = loginMsg.Split("~")
                Catch ex As Exception
                End Try
            End If
            ''''If (splitVals.Length = 6) Then
            ''''dset = user_auth(UserLogin.UserName, UserLogin.Password, "D000010496", "2016-01-22 11:24 AM")
            custmUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
            Dim urlDoc = XDocument.Load(Context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
            Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
            Session("ModeTypeITZ") = "WEB"
            Session("MchntKeyITZ") = ConfigurationManager.AppSettings("MerchantKey").ToString()
            Session("_SvcTypeITZ") = "MERCHANTDB"

            ''If ddllog.SelectedItem.Value.ToUpper().Equals("C") AndAlso splitVals.Length = 6 Then

            dset = user_auth(UserLogin.UserName, UserLogin.Password, "", "")

            If dset.Tables.Count > 0 And Convert.ToString(dset.Tables(0).Rows(0)("UID")) = "Not a Valid ID" And UserLogin.UserName.ToUpper().Contains("ITZ") Then

                Session("_PASSWORD") = hashedpwd
                Session("_USERNAME") = UserLogin.UserName


                ''çhanged for decode
                ''Session("UID") = UserLogin.UserName
                Session("UID") = advUserInfo.customerInfo.dcode

                Session("UserType") = "TA"
                Session("TypeID") = "TA1"
                Session("User_Type") = "AGENT"
                Session("IsCorp") = False

                Session("AgencyName") = advUserInfo.lastName
                Session("AGTY") = "TYPE1"
                Session("agent_type") = "TYPE1"
                ''"DEFAULT_ITILHE"
                ''''Session("ModeTypeITZ") = ds.Tables(0).Rows(0)("ModeType_ITZ").ToString().Trim()


                Session("_DCODE") = advUserInfo.customerInfo.dcode

                ''Session("_DCODE") = UserLogin.UserName
                Session("_SvcTypeITZ") = "MERCHANTDB"

                ''change for welcome in master page
                Session("firstNameITZ") = advUserInfo.firstName

                Dim abc = Insert_Agent_ITZ(advUserInfo.firstName, advUserInfo.mobile, advUserInfo.email, advUserInfo.firstName, advUserInfo.customerInfo.dcode, UserLogin.Password, "TYPE1", 0, "TA1", False, UserLogin.UserName, "", "", "", "", "", advUserInfo.customerInfo.address)

                '' FormsAuthentication.RedirectFromLoginPage(userid, False)
                dset = user_auth(UserLogin.UserName, UserLogin.Password, advUserInfo.customerInfo.dcode, advUserInfo.lastAccessTime)

                ''''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Report/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                ''Session("SearchUrl") = mapUrlValAgtA2



                RequestID = Request.QueryString("RequestID")
                Fare = Request.QueryString("fare")
                GRPUserID = Request.QueryString("userid")
                Trip = Request.QueryString("Trip")
                ISPaxDtls = Request.QueryString("PD")
                status = Request.QueryString("Status")
                Triptype = Request.QueryString("TT")
                Payment = Request.QueryString("Payment").ToString()
                If ISPaxDtls = "false" And status = "freezed" Then
                    Response.Redirect("~/GroupSearch/GroupPayment.aspx?RequestID=" & RequestID & "&Fare=" & Fare & "&GRPUserID=" & GRPUserID & "&Trip=" & Trip & "&ISPaxDtls=" & ISPaxDtls & "&Status=" & status & "&TT=" & Triptype & "&Payment=" & Payment)
                    ' ElseIf ISPaxDtls = "false" Or status <> "freezed" Then
                    '    If Triptype = "I" Then
                    'Response.Redirect("~/GroupSearch/CustomerInfoIntl.aspx?RefRequestID=" & RequestID & "&Fare=" & Fare & "&GRPUserID=" & GRPUserID & "&Trip=" & Trip & "&ISPaxDtls=" & ISPaxDtls & "&Status=" & status)
                    ''Else
                    '   Response.Redirect("~/GroupSearch/CustomerInfoDom.aspx?RefRequestID=" & RequestID & "&Fare=" & Fare & "&GRPUserID=" & GRPUserID & "&Trip=" & Trip & "&ISPaxDtls=" & ISPaxDtls & "&Status=" & status)
                    'End If
                    ' Response.Redirect("Search.aspx")
                    ''''Response.Redirect(mapUrlValAgtA2)
                    ''dset = user_auth(UserLogin.UserName, UserLogin.Password, splitVals(3).Trim(), splitVals(5).Trim())
                    'If ddllog.SelectedItem.Value.ToUpper().Equals("M") Then
                End If
            End If


            ''''dset = user_auth(userid, pwd)

            If dset IsNot Nothing AndAlso dset.Tables.Count > 0 AndAlso dset.Tables(0).Rows.Count > 0 Then
                Dim userTpe As String = dset.Tables(0).Rows(0)("Name").ToString()
                If UserLogin.UserName.Trim().ToUpper().Contains("ITZ") AndAlso Not String.IsNullOrEmpty(advUserInfo.error) And userTpe <> "EXEC" And userTpe <> "ACC" And userTpe <> "ADMIN" Then
                    Dim lblerrItz As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                    lblerrItz.Text = advUserInfo.error
                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "Not a Valid ID" Then
                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                    lblerr.Text = "Your UserID Seems to be Incorrect"
                ElseIf dset.Tables(0).Rows(0)(0).ToString() = "incorrect password" Then
                    Dim lblerr As Label = DirectCast(UserLogin.FindControl("lblerror"), Label)
                    lblerr.Text = "Your Password Seems to be Incorrect"
                Else
                    id = dset.Tables(0).Rows(0)("UID").ToString()
                    usertype = dset.Tables(0).Rows(0)("UserType").ToString()
                    typeid = dset.Tables(0).Rows(0)("TypeID").ToString()
                    User = dset.Tables(0).Rows(0)("Name").ToString()
                    If usertype = "TA" Then
                        AgencyName = dset.Tables(0).Rows(0)("Name").ToString()
                        Session("AgencyName") = AgencyName
                    End If

                    Session("_PASSWORD") = UserLogin.Password
                    Session("_USERNAME") = UserLogin.UserName
                    Session("ModeTypeITZ") = "WEB"
                    Session("UID") = id
                    Session("UserType") = usertype
                    Session("TypeID") = typeid
                    Session("User_Type") = User
                    If usertype.Trim.ToUpper = "TA" Then
                        Session("agent_type") = dset.Tables(0).Rows(0)("agent_type").ToString()
                    End If

                    Session("firstNameITZ") = id
                    Session("IsCorp") = False
                    FormsAuthentication.RedirectFromLoginPage(userid, False)

                    If User = "ACC" Then
                        Response.Redirect("Report/Accounts/Ledger.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Report/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = " "
                        ' ''Response.Redirect(mapUrlVal)
                    ElseIf User = "ADMIN" Then
                        Session("ADMINLogin") = userid
                        Response.Redirect("Search.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = mapUrlVal
                        ' ''Response.Redirect(mapUrlVal)
                    ElseIf User = "EXEC" Then
                        ''''Session("ExecTrip") = dset.Tables(0).Rows(0)("Trip").ToString()
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Report/admin/profile.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Dim mapUrlValExec = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ''''Session("SearchUrl") = mapUrlValExec
                        Try
                            Session("TripExec") = dset.Tables(0).Rows(0)("Trip").ToString()
                        Catch ex As Exception
                        End Try

                        Response.Redirect("Report/admin/profile.aspx")
                        ''''Response.Redirect(mapUrlVal)
                    ElseIf User = "AGENT" And typeid = "TA1" Then
                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp"))
                        End If
                        Response.Redirect("GroupSearch/GroupPayment.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = mapUrlVal
                        ' ''Response.Redirect(mapUrlVal)
                    ElseIf User = "AGENT" And typeid = "TA2" Then
                        If (dset.Tables(0).Rows(0)("IsCorp").ToString() <> "" AndAlso dset.Tables(0).Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                            Session("IsCorp") = Convert.ToBoolean(dset.Tables(0).Rows(0)("IsCorp").ToString())
                        End If
                        Response.Redirect("Report/Accounts/Ledger.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Report/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Dim mapUrlValAgtA2 = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = mapUrlValAgtA2
                        ' ''Response.Redirect(mapUrlVal)
                    ElseIf User = "SALES" Then
                        Response.Redirect("Search.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = mapUrlVal
                        ' ''Response.Redirect(mapUrlVal)
                    ElseIf usertype = "DI" Then
                        Response.Redirect("Report/Accounts/Ledger.aspx")
                        ' ''Dim mapUrlVal = (From i In isSameVal Where i.Element("PageName").Value = "Report/Accounts/Ledger.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Dim mapUrlValDI = (From i In isSameVal Where i.Element("PageName").Value = "Search.aspx" Select i.Element("MapUrl").Value).FirstOrDefault().ToString()
                        ' ''Session("SearchUrl") = mapUrlValDI
                        ' ''Response.Redirect(mapUrlVal)
                        'END CHANGES FOR DISTR
                    End If
                End If
            End If
            ''''End If
        Catch ex As Exception
            'lblerr.Text = ex.Message
            ' Response.Redirect("http://localhost:56359/GroupBookingLogin.aspx?userid=& GRPUserID &fare=" & Fare & "&RequestID=" & RequestID & "&Trip=" & Trip & "&PD=" & ISPaxDtls.ToLower() & "&Status=" & status.ToLower() & "&TT=" & Triptype.ToString())
            'Throw ex
        End Try
    End Sub
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
    Public Function Insert_Agent_ITZ(ByVal fname As String, ByVal mbl As String, ByVal email As String, ByVal agencyname As String, ByVal user_id As String,
                                     ByVal pwd As String, ByVal agent_type As String, ByVal crdLimit As Decimal, ByVal status As String, ByVal isCorp As Boolean,
                                     ByVal decodeITZ As String, ByVal mKeyITZ As String, ByVal pwdITZ As String, ByVal lastLoginITZ As String, ByVal modeITZ As String,
                                     ByVal svcITZ As String, ByVal addITZ As String) As Boolean
        Dim inst As Boolean = False
        Dim rows As Integer
        Dim sqlConn As New SqlConnection()

        Try
            sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim cmdITZ As New SqlCommand("USP_INSERT_AGENT_ITZ", sqlConn)
            cmdITZ.CommandType = CommandType.StoredProcedure
            cmdITZ.Parameters.AddWithValue("@FNAME", fname)
            cmdITZ.Parameters.AddWithValue("@MOBILE", mbl)
            cmdITZ.Parameters.AddWithValue("@EMAIL", email)
            cmdITZ.Parameters.AddWithValue("@AGENCY_NAME", agencyname)
            cmdITZ.Parameters.AddWithValue("@USER_ID", user_id)
            cmdITZ.Parameters.AddWithValue("@PWD", pwd)
            cmdITZ.Parameters.AddWithValue("@AGENT_TYPE", agent_type)
            cmdITZ.Parameters.AddWithValue("@CRD_LIMIT", crdLimit)
            cmdITZ.Parameters.AddWithValue("@STATUS", status)
            cmdITZ.Parameters.AddWithValue("@ISCORP", isCorp)
            cmdITZ.Parameters.AddWithValue("@DECODE", decodeITZ)
            cmdITZ.Parameters.AddWithValue("@MRCHNT_KEY", mKeyITZ)
            cmdITZ.Parameters.AddWithValue("@PWD_ITZ", pwdITZ)
            cmdITZ.Parameters.AddWithValue("@LASTLOGIN_ITZ", lastLoginITZ)
            cmdITZ.Parameters.AddWithValue("@MODETYPE_ITZ", modeITZ)
            cmdITZ.Parameters.AddWithValue("@SVCTYPE_ITZ", svcITZ)
            cmdITZ.Parameters.AddWithValue("@Add_ITZ", addITZ)
            sqlConn.Open()
            rows = cmdITZ.ExecuteNonQuery()
            sqlConn.Close()
            If rows > 0 Then
                inst = True
            End If
        Catch ex As Exception

        End Try
        Return inst
    End Function


End Class

