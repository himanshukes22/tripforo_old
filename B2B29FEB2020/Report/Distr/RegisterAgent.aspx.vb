Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class SprReports_Distr_RegisterAgent
    Inherits System.Web.UI.Page
    Public dt As New System.Data.DataTable
    Public CID As String
    Dim Title, FirstName, LastName, Address, City, State, Country, Zip, Phone As String
    Dim Mobile, Email, AEmail, Fax, Agency_Name, WebSite, Pan, Status, Stax, Remark, Sec_Qes As String
    Dim Sec_Ans, Password, Type As String
    Dim SalesExecutive As String
    Private P As New ProxyClass()
    Private ST As New SqlTransaction
    Private STDom As New SqlTransactionDom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If ((Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "DI") Then
            Response.Redirect("~/Login.aspx")
        End If


    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Dim dist As String = ""
        Dim UserType As String = ""

        If (Session("User_Type") = "DI") Then
            dist = Session("UID")
            UserType = Session("User_Type")
        End If



        Try
            Title = tit_drop.SelectedValue
            FirstName = Fname_txt.Text.Trim
            LastName = Lname_txt.Text.Trim
            Address = Add_txt.Text
            City = City_txt.Text
            State = Stat_txt.Text
            Country = Coun_txt.Text.Trim
            Zip = Pin_txt.Text.Trim
            Phone = Ph_txt.Text.Trim
            Mobile = Mob_txt.Text.Trim
            Email = Email_txt.Text.Trim
            AEmail = Aemail_txt.Text.Trim
            Fax = Fax_txt.Text.Trim
            Agency_Name = Agn_txt.Text.Trim
            WebSite = Web_txt.Text.Trim
            Pan = Pan_txt.Text.Trim
            Status = Stat_drop.SelectedValue.Trim
            Stax = Stax_txt.Text.Trim()
            Remark = Rem_txt.Text.Trim

            Sec_Qes = SecQ_drop.SelectedValue.Trim
            Sec_Ans = Ans_txt.Text.Trim
            Password = Pass_text.Text.Trim

            Dim dtdist As New DataTable()
            dtdist = STDom.GetAgencyDetails(Session("UID")).Tables(0)
            Type = dtdist.Rows(0)("Agent_Type").ToString()

            'If (Sales_DDL.SelectedItem.Text = "Select Sales Ref.") Then
            '    SalesExecutive = ""
            'Else
            '    SalesExecutive = Sales_DDL.SelectedValue
            'End If
            SalesExecutive = If(IsDBNull(dtdist.Rows(0).Item("SalesExecID")), Nothing, dtdist.Rows(0).Item("SalesExecID").ToString().Trim())
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        If State = "" Then State = "NA"
        If Country = "" Then Country = "NA"
        If Zip = "" Then Zip = "0"
        If Phone = "" Then Phone = "0"
        If AEmail = "" Then AEmail = "0"
        If Fax = "" Then Fax = "0"
        If WebSite = "" Then WebSite = "NA"
        If Stax = "" Then Stax = "NA"
        If Remark = "" Then Remark = "NA"
        Try

            Dim DtAg As New DataTable
            DtAg = STDom.GetDetailByEmailMobile(Email, Mobile).Tables(0)
            If (DtAg.Rows.Count > 0) Then
                If (UserType.ToUpper() = "DI") Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('*Agent Alreday Registered with ');", True)
                End If

            Else
                Try
                    Dim Count As New Double
                    Count = STDom.getmaxcount()
                    CID = Count
                    CID = CID + 1
                Catch ex As Exception
                    CID = "1"
                End Try
                If Status = "TA" Then
                    If (UserType.ToUpper() = "DI") Then
                        CID = "DITA" + Left(FirstName, 1) & Left(LastName, 1) & Left(Agency_Name, 1) & CID
                    End If

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
                            'STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file)
                            SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                            table_reg.Visible = False
                            table_Message.Visible = True
                        End If

                    Else
                        'STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "")
                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                        table_reg.Visible = False
                        table_Message.Visible = True

                    End If

                Else
                    CID = "CC" & Left(FirstName, 1) & Left(LastName, 1) & CID
                    Type = "Type2"
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
                            'STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, file)
                            SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                            table_reg.Visible = False
                            table_Message.Visible = True

                        End If
                    Else
                        'STDom.InsertRegistration(CID, Title, FirstName, LastName, Address, City, State, Country, Zip, Phone, Mobile, Email, AEmail, Fax, Agency_Name, WebSite, Pan, Status, Stax, Remark, Sec_Qes, Sec_Ans, Password, Type, dist, SalesExecutive, "")
                        SendEmail(Title, LastName, CID, Email, Password, Agency_Name)
                        table_reg.Visible = False
                        table_Message.Visible = True
                    End If
                    Try
                    Catch ex As Exception

                        clsErrorLog.LogInfo(ex)
                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To some Problem User Id is not created.Please try again.');", True)


                    End Try

                End If
            End If

        Catch ex As Exception

            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Private Sub SendEmail(ByVal Title As String, ByVal LastName As String, ByVal CID As String, ByVal Email As String, ByVal Password As String, ByVal agency As String)
        Try
            Dim MailDt As New DataTable
            MailDt = STDom.GetMailingDetails(MAILING.REGISTRATION_SUBAGENT.ToString().Trim(), Session("UID").ToString()).Tables(0)

            Dim strBody As String
            strBody = "<html><head><title></title><meta http-equiv=Content-Type content=text/html; charset=iso-8859-1></head><body>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Dear " & agency & "</strong></font></p>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Thank you for registering with <strong>" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(0).ToString() & ", The Only Online web Access for Travel Agents in India.</strong></font></p>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">We thank you for taking the time to register with us. Your </font></p>User Name : <font color='blue'>" & CID & "</font><br/>Password : <font color='blue'> " & Password & "</font>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">If you need immediate assistance or have any questions, concerns or suggestions, please do not hesitate to email us at <a href=""mailto:" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(1).ToString() & """>" & MailDt.Rows(0)("Body").ToString().Trim().Split("&")(1).ToString() & "</a>.</font></p>"
            'strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">We value you as a customer and would like to thank  you for your interest.</font></p>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">Sincerely,</font></p>"
            strBody = strBody + "<p><font face=""Verdana, Arial, Helvetica, sans-serif"">" & MailDt.Rows(0)("REGARDS").ToString() & "<br />"
            strBody = strBody + "</body></html>"
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
End Class
