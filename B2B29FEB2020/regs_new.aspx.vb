﻿Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class regs_new
    Inherits System.Web.UI.Page
    Public dt As New System.Data.DataTable
    Public CID, Password, Mobile, Email, AgencyId As String
    Dim Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone As String
    Dim WhMobile, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes As String
    Dim Sec_Ans, Type As String
    Dim SalesExecutive As String
    Private P As New ProxyClass()
    Private ST As New SqlTransaction
    Private STDom As New SqlTransactionDom
    Dim objSMSAPI As New SMSAPI.SMS
    Dim objSql As New SqlTransactionNew

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            dt = STDom.GetSalesRef().Tables(0)

        Catch ex As Exception

        End Try



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


                DD_Branch.AppendDataBoundItems = True
                DD_Branch.Items.Clear()
                DD_Branch.Items.Insert(0, New ListItem("--Select Branch--", "DELHI"))
                DD_Branch.DataSource = Branch()
                DD_Branch.DataTextField = "Branch"
                DD_Branch.DataValueField = "Branch"
                DD_Branch.DataBind()

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try

            BindStateGst()
        End If

    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'If Session("Captcha") = "" Or Session("Captcha") Is Nothing Then
        '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please fill captcha text');", True)
        '    Return
        'End If
        'If (Session("Captcha").ToString() = TextBox1.Text) Then
        Dim flag As Integer = 0
        'Dim dist = "HEADOFFICE"
        'If dist = "" Then
        '    dist = "HEADOFFICE"
        'End If
        Dim dist = "RWT"
        If dist = "" Then
            dist = "RWT"
        End If



        Try

            divErrorMsg.InnerText = ""
            divErrorMsg.Visible = False

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
            WhMobile = WMob_txt.Text.Trim
            Email = Email_txt.Text.Trim
            AEmail = Aemail_txt.Text.Trim
            Fax = Fax_txt.Text.Trim
            Agency_Name = Agn_txt.Text.Trim
            WebSite = Web_txt.Text.Trim
            NameOnPan = TextBox_NameOnPard.Text.Trim
            Pan = Pan_txt.Text.Trim
            Status = "TA" 'Stat_drop.SelectedValue
            'Stax = Stax_txt.Text.Trim()
            Remark = Rem_txt.Text.Trim


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


            'Sec_Qes = SecQ_drop.SelectedValue.Trim
            'Sec_Ans = Ans_txt.Text.Trim
            Password = Pass_text.Text.Trim
            If (Sales_DDL.SelectedItem.Text = "Select Sales Ref.") Then
                SalesExecutive = ""
            Else
                SalesExecutive = Sales_DDL.SelectedValue
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        'chkIsGST Section

        Dim gstNo As String = ""
        Dim gstCmpName As String = ""
        Dim gstCmpAdd As String = ""
        Dim gstPhone As String = ""
        Dim gstEmail As String = ""
        Dim gstApply As Boolean = False
        Dim gstCity As String = ""
        Dim gstState As String = ""
        Dim gstStateCode As String = ""
        Dim gstPinCode As String = ""

        If chkIsGST.Checked = True Then
            gstNo = txtGstNumber.Text
            gstCmpName = txtGSTCompanyName.Text
            gstCmpAdd = txtGSTCompanyAddress.Text
            gstPhone = txtGSTPhoneNo.Text
            gstEmail = txtGSTEMailId.Text
            gstApply = True
            gstCity = hdnSelectedCity.Value
            gstState = ddlGSTState.Items(ddlGSTState.SelectedIndex).Text
            gstStateCode = ddlGSTState.SelectedValue
            gstPinCode = txtGSTPoincoe.Text
        End If

        If State = "" Then State = "NA"
        If Country = "" Then Country = "NA"
        'Bipin
        If Area = "" Then Area = "NA"
        'If Zip = "" Then Zip = "0"
        If Phone = "" Then Phone = "0"
        If AEmail = "" Then AEmail = "0"
        If Fax = "" Then Fax = "0"
        If WebSite = "" Then WebSite = "NA"
        If Stax = "" Then Stax = "NA"
        If Remark = "" Then Remark = "NA"
        TxtUserIdreg.Text = Mobile
        Try
            If chkIsGST.Checked = True Then
                divGSTInformation.Visible = True
            End If

            Dim UserID As String = TxtUserIdreg.Text.Trim
            Dim skMobile As String = ""
            Dim skEmail As String = ""
            Dim panNumber As String = ""

            If Not String.IsNullOrEmpty(UserID) Then

                Dim DtCheckUser As New DataTable
                DtCheckUser = STDom.CheckAgentUserId(TxtUserIdreg.Text.Trim, "USERID").Tables(0)
                If (DtCheckUser.Rows.Count > 0) Then
                    'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Userid already exists. Please try another Userid');", True)
                    divErrorMsg.InnerText = "Userid already exists. Please try another Userid"
                    divErrorMsg.Visible = True
                End If

                Dim DtCheckAgentUserIdEmailMobile As New DataTable
                DtCheckUser = STDom.CheckAgentUserIdEmailMobile(TxtUserIdreg.Text.Trim, Email_txt.Text.Trim, Mob_txt.Text.Trim, Pan_txt.Text).Tables(0)
                If (DtCheckUser.Rows.Count > 0) Then
                    skEmail = DtCheckUser.Rows(0)("email").ToString().Trim()
                    skMobile = DtCheckUser.Rows(0)("mobile").ToString().Trim()
                    panNumber = DtCheckUser.Rows(0)("PanNo").ToString().Trim()
                End If

                If (skMobile = Mob_txt.Text.Trim) Then
                    'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Mobile number already exists. Please try another Mobile number');", True)
                    divErrorMsg.InnerText = "*Mobile number already exists. Please try another Mobile number"
                    divErrorMsg.Visible = True
                ElseIf (skEmail = Email_txt.Text.Trim) Then
                    'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Email Id already exists. Please try another Email Id');", True)
                    divErrorMsg.InnerText = "*Email ID already exists. Please try another Email ID"
                    divErrorMsg.Visible = True
                ElseIf (panNumber = Pan_txt.Text.Trim) Then
                    'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Pan number already exists. Please try another Pan number');", True)
                    divErrorMsg.InnerText = "*PAN Number already exists. Please try another Mobile number"
                    divErrorMsg.Visible = True
                Else
                    Dim DtAg As New DataTable
                    'DtAg = STDom.GetDetailByEmailMobile(Email, Mobile).Tables(0)
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
                            'ClientScript.RegisterStartupScript(Page.[GetType](), "Alert", "alert(" + msg + ");", True)
                            divErrorMsg.InnerText = msg
                            divErrorMsg.Visible = True

                        Else
                            'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Mobile or EmailId already exists. Please enter another mobile no and emailid');", True)
                            divErrorMsg.InnerText = "*Mobile or EmailId already exists. Please enter another mobile no and emailid"
                            divErrorMsg.Visible = True
                        End If

                    Else
                        Try
                            CID = "0000000"
                            Dim Count As New Double
                            Count = STDom.getmaxcount()
                            'CID = Left(CID, (CID.Length - (Count + 1).ToString().Length)) & (Count + 1).ToString()
                            CID = TxtUserIdreg.Text.Trim()
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
                            'CID = ("HEADOFFICE" & Left(FirstName, 1) & Left(LastName, 1) & Left(Agency_Name, 1) & CID).ToString().Trim().ToUpper()
                            CID = TxtUserIdreg.Text.Trim()
                            Type = "Type1"
                            'PANCARD IMAGE
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

                            'END PANCARD IMAGE

                            'COMPANY ADDRESS IMAGE
                            Dim len_CompAdd As Integer = flu_CompanyAddress.PostedFile.ContentLength
                            If len_CompAdd > 0 Then
                                Dim finfo_CAdd As New FileInfo(flu_CompanyAddress.FileName)
                                Dim fileExtension_CAdd As String = finfo_CAdd.Extension.ToLower()
                                If fileExtension_CAdd = ".jpg" Then

                                    Dim file_CAdd As String = ""
                                    If flu_CompanyAddress.HasFile = True Then
                                        Dim filepath_CAdd As String = Server.MapPath("AgentCompanyAdd/" + "CADD_" + CID)
                                        filepath_CAdd = filepath_CAdd + ".jpg"
                                        flu_CompanyAddress.SaveAs(filepath_CAdd.ToString())
                                        file_CAdd = Path.GetFileName("CADD_" + CID + ".jpg")
                                    End If
                                Else

                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                                End If
                            End If

                            'END COMPANY ADDRESS IMAGE

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
                                    flag = STDom.InsertRegistration_AgentReg(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, WhMobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file, DD_Branch.SelectedValue, gstNo, gstCmpName, gstCmpAdd, gstPhone, gstEmail, gstApply, gstCity, gstState, gstStateCode, gstPinCode)
                                    If flag > 0 Then
                                        SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name, AgencyId, Mobile)
                                        'table_reg.Visible = False
                                        table_Message.Visible = True
                                    Else
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                    End If

                                End If

                            Else
                                flag = STDom.InsertRegistration_AgentReg(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, WhMobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "", DD_Branch.SelectedValue, gstNo, gstCmpName, gstCmpAdd, gstPhone, gstEmail, gstApply, gstCity, gstState, gstStateCode, gstPinCode)
                                If flag > 0 Then
                                    SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                    SendEmail(Title, LastName, CID, Email, Password, Agency_Name, AgencyId, Mobile)
                                    table_reg.Visible = False
                                    table_Message.Visible = True
                                Else
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                End If


                            End If
                            'Else
                            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                            'End If
                            'Else
                            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload Pancard Image');", True)
                            'End If
                        Else
                            'CID = "CC" & Left(FirstName, 1) & Left(LastName, 1) & CID
                            CID = TxtUserIdreg.Text.Trim()
                            Type = "Type1"
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
                                    flag = STDom.InsertRegistration_AgentReg(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, WhMobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file, DD_Branch.SelectedValue, gstNo, gstCmpName, gstCmpAdd, gstPhone, gstEmail, gstApply, gstCity, gstState, gstStateCode, gstPinCode)
                                    If flag > 0 Then
                                        SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name, AgencyId, Mobile)
                                        table_reg.Visible = False
                                        table_Message.Visible = True
                                    Else
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                    End If


                                End If
                            Else
                                flag = STDom.InsertRegistration_AgentReg(CID, Title, FirstName, LastName, Address, City, State, Country, Area, Zip, Phone, Mobile, WhMobile, Email, AEmail, Fax, Agency_Name, WebSite, NameOnPan, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "", DD_Branch.SelectedValue, gstNo, gstCmpName, gstCmpAdd, gstPhone, gstEmail, gstApply, gstCity, gstState, gstStateCode, gstPinCode)
                                If flag > 0 Then
                                    SendSMS(FirstName, LastName, CID, Mobile, Password, Agency_Name)
                                    SendEmail(Title, LastName, CID, Email, Password, Agency_Name, AgencyId, Mobile)
                                    table_reg.Visible = False
                                    table_Message.Visible = True
                                    divGSTInformation.Visible = False
                                Else
                                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try again.');", True)
                                End If

                            End If
                            Try
                            Catch ex As Exception

                                clsErrorLog.LogInfo(ex)
                                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)


                            End Try

                        End If
                        'Else
                        'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter valid text from captcha.');", True)
                    End If
                End If
            Else
                'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please enter User Id');", True)
                divErrorMsg.InnerText = "Please enter User Id"
                divErrorMsg.Visible = True
            End If

        Catch ex As Exception

            clsErrorLog.LogInfo(ex)

        End Try





    End Sub
    Private Sub SendEmail(ByVal Title As String, ByVal LastName As String, ByVal CID As String, ByVal Email As String, ByVal Password As String, ByVal agency As String, ByVal AgencyId As String, ByVal Mobile As String)
        Try
            'Email = "devesh.mailme@gmail.com"
            Dim MailDt As New DataTable
            MailDt = STDom.GetMailingDetails(MAILING.RWT_REGISTRATION.ToString().Trim(), "").Tables(0)
            Dim strBody As String = "" 'funcMail(CID, Password)


            If (MailDt.Rows.Count > 0) Then

                Dim divbody As String = MailDt.Rows(0)("Body").ToString()

                strBody = divbody.Replace("Your_Name", (Title + " " + LastName)).Replace("User_ID", CID).Replace("Pass_word", Password)


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
                        Dim i As Integer = STDom.SendMail(Email, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strBody, MailDt.Rows(0)("SUBJECT").ToString() + " User Id:" + CID, "")

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


    Protected Function Branch() As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable
        Dim adap As SqlDataAdapter
        adap = New SqlDataAdapter("Sp_Branch_Opertion", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@Action", "selectNew")
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


    Private Function funcMail(ByVal strUser As String, ByVal strpass As String)
        Dim strMail As String = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>"
        strMail = strMail & "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Flywidus</title></head>"
        strMail = strMail & "<body><table width='650' border='0' cellspacing='0' cellpadding='0' align='center' style='border-right:#CCC 1px solid; border-left:#CCC 1px solid; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<tr><td align='left' style='line-height:0px;' valign='top'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-01.jpg' width='650' height='3' /></td>"
        strMail = strMail & "</tr><tr><td align='center' style='' valign='middle' height='60'><a href='http://www.richaworldtravels.com/' target='_blank'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-02.jpg' width='219' alt='Welcome to' height='53' border='0' /></a></td>"
        strMail = strMail & "</tr> <tr><td align='center' style='line-height:0px;' valign='top'><a href='http://www.richaworldtravels.com/' target='_blank'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-04.jpg' width='311' height='87' alt='Flywidus.com' border='0' /></a></td>"
        strMail = strMail & "</tr>  <tr><td align='center' style='line-height:0px;' valign='middle' height='74'><a href='http://www.richaworldtravels.com/' target='_blank'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-05.jpg' width='531' height='64' alt='Thank you for Choosing us' border='0' /></a></td>"
        strMail = strMail & "</tr> <tr><td align='center' style='' valign='top'><table width='450' border='0' cellspacing='0' cellpadding='0' height='84' style='background-color:#0e4ca1;  border-radius: 15px; color:#FFF; font-family:Arial, Helvetica, sans-serif; font-size:22px;'>"
        strMail = strMail & "<tr align='center' valign='middle'><td>USER NAME: " & strUser & "</td></tr><tr align='center' valign='middle'><td>PASSWORD:" & strpass & "</td>"""
        strMail = strMail & " </tr>    </table></td>  </tr>  <tr>    <td align='center' style='line-height:0px;' valign='middle' height='75'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-03.jpg' width='523' alt=''Great Offers' and 'Holiday Packages'Your EXPERIENCE is our PRIORITY ' height='54' /></td>"
        strMail = strMail & "</tr> <tr><td align='left' style=' padding:10px 0;' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td width='50%' style='font-size:24px; color:#333030; padding-right:5px;' align='right'>And please send us</td>"
        strMail = strMail & "<td style='padding-left:5px;'><a href='#' target='_blank'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-06.jpg' width='164' height='32' alt='Feedback' border='0' /></a></td></tr></table></td></tr><tr>"
        strMail = strMail & "<td align='left' style='font-size:25px; padding-left:50px; color:#0e4ca1;' valign='top'>Let us know what you think about Flywidus so we<br /> can make it even better!</td></tr><tr><td align='center' style='font-size:25px; padding:8px 0; color:#000000; font-weight:500;' valign='top'>Please do not hesitate to contact our 24x7 helpdesk</td></tr><tr>"
        strMail = strMail & "<td align='center' style='' valign='top'><table width='270' border='0' cellspacing='0' cellpadding='0'><tr>"
        strMail = strMail & "<td align='center' height='32' style='background-color:#074b7d; color:#FFF; font-size:22px; font-weight:bold; border-radius:10px 10px 0px 0px;'>Escalation Details...</td>"
        strMail = strMail & "</tr></table></td></tr><tr><td align='center' style='' valign='top'><table width='634' border='0' cellspacing='0' cellpadding='0' style='background:#032f4f;'>"
        strMail = strMail & "<tr valign='middle' height='40' style='font-size:16px; color:#fddc00; '><td style='border-bottom: 1px solid #4f6e84;' align='center'>DEPARTMENT</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>RESPONSIBILITES</td><td style='border-bottom: 1px solid #4f6e84;' align='center'>LAND LINE</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>MOBILE</td><td style='border-bottom:1px solid #4f6e84;' align='center'>EMAIL-ID</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'><td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>TICKETING DEPTT.</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>DOMESTIC BOOKING- GDS</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677740</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540674222</td><td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>dom@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'><td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>022-40773333</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>domestic@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>INTERNATIONAL BOOKING</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677783</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540658222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>intl@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'><td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>NUTAN</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677778</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718169222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>intl@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>022-40773333</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>7835082251</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>int@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>GROUPS BOOKING</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677765/66</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718324222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>groupdesk@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>022-40773333</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>7835082252</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>groups@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>CANCELLATION</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677701</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540613222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>cancellation@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>PACKAGES</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677703</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718139222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>packages@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>holidays@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>FOR NIGHT SHIFT</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>INTERNATIONAL BOOKING</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677781</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718284222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>sevenseaztravelz@gmail.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>FOR NIGHT SHIFT</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>DOMESTIC BOOKING</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677722</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540574222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>dom@richaworldtravels.com</td>"
        strMail = strMail & "</tr><tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>FOR NIGHT SHIFT</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>CANCELLATION</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677729</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540574222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>cancellation@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>ESCALATION</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>OPERATION - HEAD</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677742</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540657222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>intl@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>ACCOUNTS</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>ACCOUNTS - HEAD</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677772</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718016222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>accountsouthex@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>PROCESS REFUND</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>022-40773333<br />"
        strMail = strMail & "47677732</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>7835082253</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>finance@richaworldtravels.com<br />"
        strMail = strMail & "accounts@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>PROCESS COMMISSION</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677771</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>refunds@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>TECHNICAL </td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>PAYMENT UPLOAD</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677730</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>&nbsp;</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>upload@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>ASSISTANCE</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>IT- HEAD &amp; DISTRIBUTION<br />"
        strMail = strMail & "CHANNEL</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677763<br /></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9540222666</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>subhash@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'></td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Customer Support</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>47677756/57</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>9718050222</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>support@richaworldtravels.com</td>"
        strMail = strMail & "</tr>"

        strMail = strMail & "</table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td align='left' style='' valign='top'>&nbsp;</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td align='center' style='' valign='top'><table width='270' border='0' cellspacing='0' cellpadding='0'>"
        strMail = strMail & " <tr>"
        strMail = strMail & " <td align='center' height='32' style='background-color:#074b7d; color:#FFF; font-size:22px; font-weight:bold; border-radius:10px 10px 0px 0px;'>Bank Details...</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & " </table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td align='center' style='' valign='top'><table width='634' border='0' cellspacing='0' cellpadding='0' style='background:#032f4f;'>"
        strMail = strMail & " <tr valign='middle' height='40' style='font-size:16px; color:#fddc00; '>"
        strMail = strMail & " <td style='border-bottom: 1px solid #4f6e84;' align='center'>&nbsp;</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>Canara Bank</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>ICICI Bank Ltd.</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>Bank of Baroda</td>"
        strMail = strMail & "<td style='border-bottom:1px solid #4f6e84;' align='center'>Axis Bank</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>A/C No</td>"
        strMail = strMail & "  <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>0270201006360</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>039605001710</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>19920200000643</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>913020021620860</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & " <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>A/C <br />"
        strMail = strMail & "  Name</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & "Vacations Pvt. Ltd.</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & " Vacations Pvt. Ltd.</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & " Vacations Pvt. Ltd.</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & "  Vacations Pvt. Ltd.</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & " <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>Branch</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>GOLE MARKET<br />"
        strMail = strMail & " NEW DELHI - <br />"
        strMail = strMail & "110001</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>D -16 SOUTH <br />"
        strMail = strMail & "EXTENSION PART 2 <br />"
        strMail = strMail & " NEW DELHI - 110024</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>BHICAJI CAMA <br />"
        strMail = strMail & " PLACE New DELHI - <br/>"
        strMail = strMail & "  110066</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>DEFENCE COLONY,<br />"
        strMail = strMail & " NEW DELHI - 110024</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & " <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "  <td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84; color:#fddc00;'>IFSC Code</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>CNRB0000270</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>ICIC0000396</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>BARB0BHICKA</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>UTIB0000357</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "    </table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & " <td align='left' style='' valign='top'>&nbsp;</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & " <td align='center' style='' valign='top'><table width='634' border='0' cellspacing='0' cellpadding='0' style='background:#032f4f;'>"
        strMail = strMail & "<tr valign='middle' height='40' style='font-size:16px; color:#fddc00; '>"
        strMail = strMail & " <td style='border-bottom: 1px solid #4f6e84;' align='center'>&nbsp;</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>Kotak Bank</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'> HDFC Bank</td>"
        strMail = strMail & "<td style='border-bottom: 1px solid #4f6e84;' align='center'>State Bank of India</td>"
        strMail = strMail & "<td style='border-bottom:1px solid #4f6e84;' align='center'>IDBI Bank</td>"
        strMail = strMail & "  </tr>"
        strMail = strMail & " <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "<td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>A/C No</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>4511178135</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>05842020000243</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>33196436902</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>0011102000080945</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & " <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & " <td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>A/C <br />"
        strMail = strMail & "  Name</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & "  Vacations Pvt. Ltd.</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & " Vacations Pvt. Ltd.</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & "  Vacations Pvt. Ltd.</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>Seven Seaz <br />"
        strMail = strMail & "  Vacations Pvt. Ltd.</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "  <tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "  <td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; color:#fddc00; border-right:1px solid #4f6e84;'>Branch</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>M-57,LAJPAT NAGAR <br />"
        strMail = strMail & " PART 2 GRO.  FLR<br />"
        strMail = strMail & "   NEW DELHI - 110024</td>"
        strMail = strMail & "<td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>GOLE MARKET NEW<br />"
        strMail = strMail & "  DELHI - 110001</td>"
        strMail = strMail & "  <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>M-2,SOUTH EXT<br />"
        strMail = strMail & "  PART-2 City NEW <br />"
        strMail = strMail & "  DELHI - 110049 </td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>SURYA KIRAN <br />"
        strMail = strMail & "  BUILDING, K.G MARG<br />"
        strMail = strMail & " NEW DELHI </td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "<tr style=' font-size:13px; color:#FFF; font-family:Arial, Helvetica, sans-serif;'>"
        strMail = strMail & "  <td height='24px' style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84; color:#fddc00;'>IFSC Code</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>KKBK0000198</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>HDFC0000584</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84; border-right:1px solid #4f6e84;'>SBIN0003219</td>"
        strMail = strMail & " <td style='padding-left:6px; border-bottom:1px solid #4f6e84;'>IBKL0000011</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & " </table></td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td align='left' style='' valign='top'>&nbsp;</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & " <td align='center' style=' background:#000; padding-top:10px;' valign='top' ><table width='634' border='0' cellspacing='0' cellpadding='0'>"
        strMail = strMail & "  <tr >"
        strMail = strMail & "  <td align='center' width='78' style='background:#ed1c24; font-size:20px; color:#FFF;'>Delhi</td>"
        strMail = strMail & "  <td align='left' style='font-size:13px; padding-left:15px; color:#FFF;'>A-188-189, 2nd Floor, Bhishm Pitamaha Marg, Kotla Mubarakpur, New Delhi-110003</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & " <td align='center' style='background:#ed1c24; font-size:20px; color:#FFF; '>Office</td>"
        strMail = strMail & " <td align='left' style='font-size:20px; padding-left:15px; color:#FFF;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>"
        strMail = strMail & "  <tr>"
        strMail = strMail & "  <td><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-08.jpg' width='25' height='23' /></td>"
        strMail = strMail & " <td>support@richaworldtravels.com</td>"
        strMail = strMail & " <td><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-07.jpg' width='26' height='25' /></td>"
        strMail = strMail & " <td> + 91-11-47 67 77 77</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & "</table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "</table></td>"
        strMail = strMail & " </tr>"
        strMail = strMail & " <tr>"
        strMail = strMail & "<td align='center' style=' background:#000; padding-top:10px;' valign='top' ><table width='634' border='0' cellspacing='0' cellpadding='0'>"
        strMail = strMail & " <tr >"
        strMail = strMail & "<td align='center' width='78' style='background:#0e4ca1; font-size:20px; color:#FFF;'>Mumbai</td>"
        strMail = strMail & "<td align='left' style='font-size:13px; padding-left:15px; color:#FFF;'>A-401, 4th  Floor, Universal Business Park, Chandivali Farm Road, Off Saki Vihar Road,<br />"
        strMail = strMail & "  Andheri (East), Mumbai - 400 072</td>"
        strMail = strMail & " </tr>"
        strMail = strMail & " <tr>"
        strMail = strMail & "  <td align='center' style='background:#0e4ca1; font-size:20px; color:#FFF; '>Office</td>"
        strMail = strMail & "  <td align='left' style='font-size:20px; padding-left:15px; color:#FFF;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-08.jpg' width='25' height='23' /></td>"
        strMail = strMail & "<td>enquiry@richaworldtravels.com</td>"
        strMail = strMail & "<td><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-07.jpg' width='26' height='25' /></td>"
        strMail = strMail & "<td>+91-22-40 77 33 33</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "</table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "</table></td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr>"
        strMail = strMail & "<td align='left' style=' background:#000;' valign='top' >&nbsp;</td>"
        strMail = strMail & "</tr>"
        strMail = strMail & "<tr><td align='left' style='line-height:0px;' valign='top'><img src='http://www.richaworldtravels.com/emm/registerd-17-11-18-01.jpg' width='650' height='3' /></td></tr></table></body></html>"

        Return strMail

    End Function

    Private Sub BindStateGst()
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select STATEID as Code,STATE as Name from  [dbo].[Tbl_STATE] where COUNTRY='") & "India") + "'order by STATE", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlGSTState.DataSource = dt
            ddlGSTState.DataValueField = "Code"
            ddlGSTState.DataTextField = "Name"
            ddlGSTState.DataBind()
            ddlGSTState.Items.Insert(0, New ListItem("--Select State--", "select"))
            BindCityGst(ddlGSTState.SelectedValue)


        Catch ex As Exception
        End Try

    End Sub
    Private Sub BindCityGst(stateCode As String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select CITY, STATEID from  [dbo].[TBL_CITY]  where STATEID='") & stateCode) + "'order by CITY", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlGSTCity.DataSource = dt
            ddlGSTCity.DataValueField = "CITY"
            ddlGSTCity.DataTextField = "CITY"
            ddlGSTCity.DataBind()
            ddlGSTCity.Items.Insert(0, New ListItem("--Select City --", "select"))

            'ddl_city.DataSource = dt
            'ddl_city.DataValueField = "CITY"
            'ddl_city.DataTextField = "CITY"
            'ddl_city.DataBind()
            'ddl_city.Items.Insert(0, New ListItem("--Select City --", "select"))
        Catch ex As Exception
        End Try
    End Sub
End Class

