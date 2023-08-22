Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class sendmail
    Inherits System.Web.UI.Page

    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)
      
        '    Dim STDOM As New SqlTransactionDom
        'Try


        '    STDOM.SendMail("paritoshsingh1984@gmail.com, rahulm@itzcash.com", "info@RWT.com", "", "", "", "", "", "", "Ticket Reissue Request", "")

        'Catch ex As Exception
        '    Response.Write(ex.Message)


        'End Try

        Dim dt As New DataTable()
        Dim ToEmail As String
        Dim strMailMsgHold As String
        Dim ObjIntDetails As New IntlDetails()
        Try


            dt = ObjIntDetails.Email_Credentilas("20d7718c9ViwvHo5", "Refund_REJECTED", "5")

            Response.Write(dt.Rows.Count.ToString())
            For k As Integer = 0 To dt.Rows.Count - 1
                ToEmail += "; " + dt.Rows(k)(1).ToString()
            Next

            Response.Write(ToEmail)
            ToEmail = ToEmail.Remove(0, 1)

            Response.Write(ToEmail)


            Dim STDOM As New SqlTransactionDom
            Dim MailDt As New DataTable
            MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), "").Tables(0)
            Response.Write(MailDt.Rows.Count.ToString())
            Try
                If (MailDt.Rows.Count > 0) Then
                    Response.Write("mail")
                    SendMail("paritoshsingh1984@gmail.com, paritosh.singh@galileo.co.in", "info@RWT.com", "", "", "", "", "", "", "Ticket Reissue Request", "")
                    ''STDOM.SendMail(ToEmail, " info@RWT.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Refund Request", "")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)

            End Try


        Catch ex1 As Exception
            Response.Write(ex1.Message)
        End Try





    End Sub
    Public Function SendMail(ByVal toEMail As String, ByVal from As String, ByVal bcc As String, ByVal cc As String, ByVal smtpClient As String, ByVal userID As String, ByVal pass As String, ByVal body As String, ByVal subject As String, ByVal AttachmentFile As String) As Integer
        Response.Write("statrt mail")
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
        If AttachmentFile <> "" Then
            msgMail.Attachments.Add(New System.Net.Mail.Attachment(AttachmentFile))
        End If

        msgMail.Subject = subject
        msgMail.IsBodyHtml = True
        msgMail.Body = body


        Try
            Response.Write("send mail")
            objMail.Credentials = New System.Net.NetworkCredential(userID, pass)
            objMail.Host = smtpClient
            objMail.Send(msgMail)


            Response.Write(msgMail.To.ToString() & msgMail.From.ToString() & userID & pass & smtpClient)
            Response.Write("mail sent")
            Return 1

        Catch ex As Exception
            Response.Write(ex.Message)

            Return 0
            Response.Write(0)
        End Try
    End Function
End Class
