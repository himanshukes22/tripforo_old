Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class SprReports_Distr_AddAgent
    Inherits System.Web.UI.Page
    Public dt As New System.Data.DataTable
    Public CID As String
    Dim Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone As String
    Dim Mobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes As String
    Dim Sec_Ans, Password, Type As String
    Dim SalesExecutive As String
    Private P As New ProxyClass()
    Private ST As New SqlTransaction
    Private STDom As New SqlTransactionDom
    Dim objSMSAPI As New SMSAPI.SMS
    Dim objSql As New SqlTransactionNew

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ((Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "DI") Then
            Response.Redirect("~/Login.aspx")
        End If

        dt = STDom.GetSalesRef().Tables(0)
        If Not IsPostBack Then
            Session("Captcha") = Nothing
            Try

                Sales_DDL.AppendDataBoundItems = True
                Sales_DDL.Items.Clear()
                Sales_DDL.Items.Insert(0, "Select Sales Ref.")
                Sales_DDL.DataSource = dt
                Sales_DDL.DataTextField = "Name"
                Sales_DDL.DataValueField = "EmailId"

                Sales_DDL.DataBind()


                ddl_state.AppendDataBoundItems = True
                ddl_state.Items.Clear()
                ddl_state.Items.Insert(0, "--Select State--")
                ddl_state.DataSource = GETCITYSTATE(ddl_country.SelectedValue, "COUNTRY")
                ddl_state.DataTextField = "STATE"
                ddl_state.DataValueField = "STATEID"
                ddl_state.DataBind()

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try


        End If

    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'If Session("Captcha") = "" Or Session("Captcha") Is Nothing Then
        '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please fill captcha text');", True)
        '    Return
        'End If
        'If (Session("Captcha").ToString() = TextBox1.Text) Then

        'Dim dist = "FWU"
        'If dist = "" Then
        '    dist = "FWU"
        'End If
        Dim flag As Integer = 0
        Dim dist As String = ""
        Dim UserType As String = ""
        Sec_Ans = "NA"

        If (Session("User_Type") = "DI") Then
            dist = Session("UID")
            UserType = Session("User_Type")
        End If
        Try
            Title = tit_drop.SelectedValue
            FirstName = Fname_txt.Text.Trim
            LastName = Lname_txt.Text.Trim
            Address = Add_txt.Text
            'City = ddl_city.Value
            'State = Stat_txt.Text
            'Country = Coun_txt.Text.Trim
            Area = TextBox_Area.Text.Trim
            Zip = Pin_txt.Text.Trim
            Phone = Ph_txt.Text.Trim
            Mobile = Mob_txt.Text.Trim
            Email = Email_txt.Text.Trim
            AEmail = Aemail_txt.Text.Trim
            Fax = Fax_txt.Text.Trim
            Agency_Name = Agn_txt.Text.Trim
            WebSite = Web_txt.Text.Trim
            NameOnPan = TextBox_NameOnPard.Text.Trim
            Pan = Pan_txt.Text.Trim
            Status = Stat_drop.SelectedValue
            Stax = Stax_txt.Text.Trim()
            Remark = Rem_txt.Text.Trim

            'Sec_Qes = SecQ_drop.SelectedValue.Trim
            'Sec_Ans = Ans_txt.Text.Trim
            Password = Pass_text.Text.Trim

            If (Sales_DDL.SelectedItem.Text = "Select Sales Ref.") Then
                SalesExecutive = ""
            Else
                SalesExecutive = Sales_DDL.SelectedValue
            End If


            Dim CountryType As String = ""
            CountryType = ddl_country.SelectedValue
            If CountryType = "India" Then
                City = ddl_city.Value
                State = ddl_state.SelectedItem.Text
                Country = ddl_country.SelectedItem.Text
            End If
            If CountryType = "Other" Then
                City = Other_City.Text
                State = Stat_txt.Text
                Country = Coun_txt.Text.Trim
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        If State = "" Then State = "NA"
        If Country = "" Then Country = "NA"
        If Area = "" Then Area = "NA"
        'If Zip = "" Then Zip = "0"
        If Phone = "" Then Phone = "0"
        If AEmail = "" Then AEmail = "0"
        If Fax = "" Then Fax = "0"
        If WebSite = "" Then WebSite = "NA"
        If Stax = "" Then Stax = "NA"
        If Remark = "" Then Remark = "NA"
        Try
            Dim UserID As String = TxtUserId.Text.Trim
            If Not String.IsNullOrEmpty(UserID) Then

                Dim DtCheckUser As New DataTable
                DtCheckUser = STDom.CheckAgentUserId(TxtUserId.Text.Trim, "USERID").Tables(0)
                If (DtCheckUser.Rows.Count > 0) Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Userid already exists. Please try another Userid');", True)
                Else

                    Dim DtAg As New DataTable
                    DtAg = STDom.GetDetailByEmailMobile(Email, Mobile).Tables(0)
                    If (DtAg.Rows.Count > 0) Then
                        'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*You Have Alreday Registered');", True)

                        Dim EmailAgent As String = Convert.ToString(DtAg.Rows(0)("email"))
                        Dim MobileAgent As String = Convert.ToString(DtAg.Rows(0)("mobile"))
                        Dim MsgType As Integer = 0
                        Dim msg As String = ""
                        If MobileAgent = Mobile Then
                            msg = "Mobile no already exists. Please enter another mobile no"
                            MsgType = 1
                        End If
                        If EmailAgent = Email Then
                            msg = "EmailId already exists. Please enter another emailid"
                            MsgType = 2
                        End If
                        If MobileAgent = Mobile AndAlso EmailAgent = Email Then
                            MsgType = 3
                            msg = "Mobile and EmailId already exists. Please enter another mobile no and emailid"
                        End If

                        If MsgType > 0 Then
                            ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert(" + msg + ");", True)

                        Else
                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Mobile or EmailId already exists. Please enter another mobile no and emailid');", True)
                        End If

                    Else
                        Try
                            CID = "0000000"
                            Dim Count As New Double
                            Count = STDom.getmaxcount()
                            'CID = Left(CID, (CID.Length - (Count + 1).ToString().Length)) & (Count + 1).ToString()
                            CID = TxtUserId.Text.Trim()

                            Dim AgencyDs As DataSet
                            AgencyDs = ST.GetAgencyDetails(Session("UID"))
                            If AgencyDs.Tables.Count > 0 Then
                                If AgencyDs.Tables(0).Rows.Count > 0 Then
                                    Type = Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agent_Type"))
                                End If
                            End If
                            If String.IsNullOrEmpty(Type) Then
                                Type = "Type1"
                            End If
                            'If (dist.ToUpper() = "FWU") Then
                            '    Type = "Type1"
                            'End If

                            'CID = Count
                            'CID = CID + 1
                        Catch ex As Exception
                            'CID = "1"
                        End Try
                        'If (Session("Captcha").ToString() = TextBox1.Text) Then
                        If Status = "TA" Then
                            If (Country.ToUpper() = "NA") Then
                                Country = ddl_country.SelectedValue
                                City = ddl_city.Value
                                State = ddl_state.SelectedItem.Text
                            End If
                            'CID = ("FWU" & Left(FirstName, 1) & Left(LastName, 1) & Left(Agency_Name, 1) & CID).ToString().Trim().ToUpper()
                            CID = TxtUserId.Text.Trim()
                            'Type = "Type1"
                            'PANCARD IMAGE

                            Try
                                Dim len_pan As Integer = fld_pan.PostedFile.ContentLength
                                If len_pan > 0 Then
                                    Dim finfo_pan As New FileInfo(fld_pan.FileName)
                                    Dim fileExtension_pan As String = finfo_pan.Extension.ToLower()
                                    If fileExtension_pan = ".jpg" Then

                                        Dim file_pan As String = ""
                                        If fld_pan.HasFile = True Then
                                            Dim filepath_pan As String = Server.MapPath("AgentPancard/" + "PAN_" + CID)
                                            filepath_pan = filepath_pan + ".jpg"
                                            fld_pan.SaveAs(filepath_pan.ToString())
                                            file_pan = Path.GetFileName("PAN_" + CID + ".jpg")
                                        End If
                                    Else

                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                                    End If
                                End If
                            Catch ex As Exception
                            End Try


                            'END PANCARD IMAGE

                            Dim len As Integer = fld_1.PostedFile.ContentLength
                            If len > 0 Then
                                Dim finfo As New FileInfo(fld_1.FileName)
                                Dim fileExtension As String = finfo.Extension.ToLower()
                                If fileExtension <> ".jpg" Then
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                                Else

                                    Dim file As String = ""
                                    If fld_1.HasFile = True Then
                                        Dim filepath As String = Server.MapPath("AgentLogo/" + CID)
                                        filepath = filepath + ".jpg"
                                        fld_1.SaveAs(filepath.ToString())
                                        file = Path.GetFileName(CID + ".jpg")
                                    End If
                                    Try
                                        flag = STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file)
                                        If flag > 0 Then
                                            SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                                            SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                            table_reg.Visible = False
                                            table_Message.Visible = True
                                            ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Successful Registered');", True)
                                        Else
                                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                        End If


                                    Catch ex As Exception

                                        clsErrorLog.LogInfo(ex)
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)
                                    End Try

                                End If

                            Else
                                Try
                                    flag = STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "")
                                    If flag > 0 Then
                                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                                        SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                        table_reg.Visible = False
                                        table_Message.Visible = True
                                        ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Successful Registered');", True)
                                    Else
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                    End If


                                Catch ex As Exception

                                    clsErrorLog.LogInfo(ex)
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)
                                End Try


                            End If
                            'Else
                            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                            'End If
                            'Else
                            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload Pancard Image');", True)
                            'End If
                        Else
                            'CID = "CC" & Left(FirstName, 1) & Left(LastName, 1) & CID
                            CID = TxtUserId.Text.Trim()
                            'Type = "Type2"
                            Dim len As Integer = fld_1.PostedFile.ContentLength
                            If len > 0 Then
                                Dim finfo As New FileInfo(fld_1.FileName)
                                Dim fileExtension As String = finfo.Extension.ToLower()
                                If fileExtension <> ".jpg" Then
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                                Else

                                    Dim file As String = ""
                                    If fld_1.HasFile = True Then
                                        Dim filepath As String = Server.MapPath("AgentLogo/" + CID)
                                        filepath = filepath + ".jpg"
                                        fld_1.SaveAs(filepath.ToString())
                                        file = Path.GetFileName(CID + ".jpg")
                                    End If
                                    Try
                                        flag = STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file)
                                        If flag > 0 Then
                                            SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                                            SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                            table_reg.Visible = False
                                            table_Message.Visible = True
                                            ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Successful Registered');", True)
                                        Else
                                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                        End If
                                    Catch ex As Exception

                                        clsErrorLog.LogInfo(ex)
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)
                                    End Try


                                End If
                            Else
                                Try
                                    flag = STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "")
                                    If flag > 0 Then
                                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                                        SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                        table_reg.Visible = False
                                        table_Message.Visible = True
                                        ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert('Successful Registered');", True)
                                    Else
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                    End If
                                Catch ex As Exception
                                    clsErrorLog.LogInfo(ex)
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)
                                End Try

                            End If

                        End If
                        'Else
                        '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter valid text from captcha.');", True)
                        'End If

                    End If

                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter User Id');", True)
            End If

        Catch ex As Exception

            clsErrorLog.LogInfo(ex)

        End Try

        'Else
        '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Captcha text is incorrect');", True)
        'End If



    End Sub
    Private Sub SendEmail(ByVal Title As String, ByVal LastName As String, ByVal CID As String, ByVal Email As String, ByVal Password As String, ByVal agency As String)
        Try
            Dim MailDt As New DataTable
            MailDt = STDom.GetMailingDetails(MAILING.RWT_REGISTRATION.ToString().Trim(), "").Tables(0)
            Dim strBody As String
            If (MailDt.Rows.Count > 0) Then

                Dim divbody As String = MailDt.Rows(0)("Body").ToString()

                Dim body() As String = divbody.Split("}")
                strBody = body(0)
                strBody = strBody + body(1).Replace("SubjectDYN", Convert.ToString(MailDt.Rows(0)("SUBJECT").ToString())).Replace("CIDDYN", CID).Replace("PASSWORDDYN", Password).Replace("XYZDYN", MailDt.Rows(0)("REGARDS").ToString())
                strBody = strBody + body(2).ToString()


            End If


            'strBody = "<html><head><title></title><meta http-equiv=Content-Type content=text/html; charset=iso-8859-1></head><body>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Dear " & agency & "</strong></font></p>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Thank you for registering with <strong>" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(0).ToString() & ", The Only Online web Access for Travel Agents in India.</strong></font></p>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">We thank you for taking the time to register with us. Your </font></p>User Name : <font color='blue'>" & CID & "</font><br/>Password : <font color='blue'> " & Password & "</font>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">If you need immediate assistance or have any questions, concerns or suggestions, please do not hesitate to email us at <a href=""mailto:" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(1).ToString() & """>" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(1).ToString() & "</a>.</font></p>"
            ''strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">We value you as a customer and would like to thank  you for your interest.</font></p>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Sincerely,</font></p>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">" & MailDt.Rows(0)("REGARDS").ToString() & "<br />"
            'strBody = strBody + "</body></html>"


            Try
                If (MailDt.Rows.Count > 0) Then
                    Dim Status As Boolean = False
                    Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())

                    If Status = True Then
                        Dim i As Integer = STDom.SendMail(Email, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strBody, MailDt.Rows(0)("SUBJECT").ToString(), "")

                    End If
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Protected Sub ddl_country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_country.SelectedIndexChanged
        If (ddl_country.SelectedValue = "Other") Then
            Stat_txt.Visible = True
            ddl_state.Visible = False
            ddl_city.Visible = False
            Other_City.Visible = True
            'ddl_city.Visible = False
            Coun_txt.Visible = True
            'ddl_country.Visible = False
        Else
            Stat_txt.Visible = False
            ddl_state.Visible = True
            Other_City.Visible = False
            ddl_city.Visible = True
            Coun_txt.Visible = False
            ddl_country.Visible = True
        End If
    End Sub
    'Protected Sub ddl_state_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_state.SelectedIndexChanged
    '    ddl_city.AppendDataBoundItems = True
    '    ddl_city.Items.Clear()
    '    ddl_city.Items.Insert(0, "--Select City--")

    '    ddl_city.DataSource = GETCITYSTATE(ddl_state.SelectedValue, "STATE")
    '    ddl_city.DataTextField = "CITY"
    '    ddl_city.DataValueField = "CITY"

    '    ddl_city.DataBind()
    '    ddl_city.Items.Insert(ddl_city.Items.Count, "Other")
    'End Sub
    Protected Function GETCITYSTATE(ByVal INPUT As String, ByVal SEARCH As String) As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable
        Dim adap As SqlDataAdapter
        adap = New SqlDataAdapter("SP_GET_STATECITY", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@INPUT", INPUT)
        adap.SelectCommand.Parameters.AddWithValue("@SEARCH", SEARCH)

        adap.Fill(dt)

        Return dt
    End Function

    Private Sub SendSMS(ByVal FirstName As String, ByVal LastName As String, ByVal CID As String, ByVal Mobile As String, ByVal Password As String, ByVal agency As String)
        Try
            Dim smsStatus As String = ""
            Dim smsMsg As String = ""
            Try
                'Mobile = "9871186224"
                Dim FullName As String = FirstName + " " + LastName
                Dim SmsCrd As DataTable
                SmsCrd = ST.SmsCredential("AGENTREGISTER").Tables(0)
                'SmsCrd = ST.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)
                'ByVal Name As String, ByVal UserId As String, ByVal Pwd As String, ByVal mobno As String, ByRef smstext As String, ByVal DtCrd As DataTable
                If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                    smsStatus = objSMSAPI.SendSmsUserId(FullName, CID, Password, Mobile, smsMsg, SmsCrd)
                    objSql.SmsLogDetails(CID, Mobile, smsMsg, smsStatus)
                End If

            Catch ex As Exception
            End Try
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
End Class
