Imports System.Data
Partial Class ForgotPassword
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

        
            Dim dsalert As New DataSet
            dsalert = STDom.CheckForgotPassword(txt_UserID.Text.Trim, txt_EmailID.Text.Trim, txt_MobileNo.Text.Trim)

            If InStr(dsalert.Tables(0).Rows(0)("Messagetigive").ToString().ToUpper, "SUCCESSFULLY") > 0 Then
                'Mail code Here
                Try
                    Dim strBody As String
                    Dim email, pwd, fname, lname As String

                    Dim dt As New DataTable
                    dt = STDom.GetAgencyDetails(txt_UserID.Text.Trim).Tables(0)
                    If dt.Rows.Count > 0 Then
                        fname = dt.Rows(0)("FName")
                        lname = dt.Rows(0)("LName")
                        email = dt.Rows(0)("Email")
                        pwd = dt.Rows(0)("PWD")
                     
                   

                        Dim STDom As New SqlTransactionDom
                        Dim MailDt As New DataTable
                        MailDt = STDom.GetMailingDetails(MAILING.FORGOTPASSWORD.ToString().Trim(), "").Tables(0)

                        If (MailDt.Rows.Count > 0) Then

                            Dim divbody As String = MailDt.Rows(0)("Body").ToString()                           
							
							strBody = divbody.Replace("#name#", (fname + " " + lname)).Replace("#User_ID#", txt_UserID.Text.Trim).Replace("#Password#", pwd)


                        End If

                        Try
                            If (MailDt.Rows.Count > 0) Then
                                Dim Status As Boolean = False
                                Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())

                                If Status = True Then
                                    Dim i As Integer = STDom.SendMail(email, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strBody, "Your password-   " + pwd, "")

                                    If i > 0 Then
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" & dsalert.Tables(0).Rows(0)("Messagetigive").ToString() & "')", True)
                                    Else
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('try again.')", True)
                                    End If

                                End If
                            End If
                        Catch ex As Exception
                        End Try
                       
                    End If

                Catch ex As Exception
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" & ex.Message & "')", True)
                End Try
                '
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" & dsalert.Tables(0).Rows(0)("Messagetigive").ToString() & "')", True)
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
