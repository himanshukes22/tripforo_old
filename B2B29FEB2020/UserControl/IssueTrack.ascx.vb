Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Net.Mail
Imports System.Data.SqlClient

Partial Class UserControl_IssueTrack
    Inherits System.Web.UI.UserControl
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)

    
    Protected Sub b1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim mytable As String = ""
            mytable += "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%;height: 200px; border-collapse: collapse'>"

            mytable += "<tr>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "<td align='left'>"

            mytable += "</td>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "</tr>"

            mytable += "<tr>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "<td>"

            mytable += "<table border='0' cellpadding='0' cellspacing='0' style='width: 70%; border-collapse: collapse'>"
            mytable += "<tr valign='top'><td><br /><span style='font-family: Tahoma'><strong>"
            mytable += " </strong></span>"
            mytable += "</td>"
            mytable += "<td> </td><td><br/></td></tr>"


            mytable += "</table>"

            mytable += "</td>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "</tr>"

            mytable += "<tr>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "<td align='left'>"


            mytable += "<table border='2' cellpadding='0' cellspacing='0' class='innerTEXT' style='width:70%;height: 200px;border-collapse: collapse'>"
            mytable += "<tr><td colspan='2' align='center' valign='middle' style='color:Blue;'><strong>Customer Report For Issue</strong><br/><br/></td></tr>"

            mytable += "<tr><td><b> Name : </b></td><td>" + txtFirstName.Value + " " + txtLastName.Value + " </td></tr>"
            mytable += "<tr><td><b> UserID: </b></td><td>" + UserID.Value + " </td></tr>"
            mytable += "<tr><td><b> EmailID : </b></td><td>" + txtguestemail.Value + " </td></tr>"
            mytable += "<tr><td><b> Contact No : </b></td><td>" + txtmobileno.Value + " </td></tr>"
            mytable += "<tr><td><b> Agency Name : </b></td><td>" + txtAgencyName.Value + " </td></tr>"
            mytable += "<tr><td><b>Department  : </b></td><td>" + ddl_Depart.SelectedValue + " </td></tr>"

            mytable += "<tr><td><b> ISSUES : </b></td><td>" + txtissue.Text + " </td></tr>"




            mytable += "</table>"


            mytable += "</td>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "</tr>"


            mytable += "<tr>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "<td valign='top'>"



            mytable += "</td>"

            mytable += "<td>"
            mytable += "</td>"
            mytable += "</tr>"

            mytable += "<tr>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "<td align='left'>"

            mytable += "</td>"
            mytable += "<td>"
            mytable += "</td>"
            mytable += "</tr>"
            mytable += "</table>"

            Dim msgobj As New MailMessage()


            Dim From As String = txtguestemail.Value.ToString()
            msgobj.From = New MailAddress(From)

            msgobj.To.Add(New System.Net.Mail.MailAddress("customer1@RWT.com"))
            msgobj.Bcc.Add(New System.Net.Mail.MailAddress("itsupport@RWT.com"))
            msgobj.Subject = "Customer Issue"
            msgobj.Body = mytable
            msgobj.IsBodyHtml = True

            InsertQuery()

            Try
                Dim smtp As New SmtpClient("mail.ITZ.com")
                smtp.Credentials = New System.Net.NetworkCredential("b2bticketing", "america")
                smtp.Send(msgobj)
                'Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');</script>");
                ScriptManager.RegisterStartupScript(Me, Page.[GetType](), "Alert", "alert('Your issue request has been submitted successfully');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Page.[GetType](), "Alert", "alert('Your issue request has been submitted successfully');", True)
            End Try
            reset()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Page.[GetType](), "Alert", "alert('Unable to process your issue. Please try after some time.');", True)
        End Try
        reset()

    End Sub
    Public Sub InsertQuery()
        Dim adap As SqlDataAdapter

        Dim cmd As SqlCommand
        Try



            Dim ds As New DataSet()

            cmd = New SqlCommand("SP_Reportissue", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserID", UserID.Value)
            cmd.Parameters.AddWithValue("@ContactNo", txtmobileno.Value)
            cmd.Parameters.AddWithValue("@EmailID", txtguestemail.Value)
            cmd.Parameters.AddWithValue("@Name", txtFirstName.Value + " " + txtLastName.Value)
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyName.Value)
            cmd.Parameters.AddWithValue("@Department", ddl_Depart.SelectedValue)
            cmd.Parameters.AddWithValue("@ISSUES", txtissue.Text)

            con.Open()
            Dim ifexists As String = cmd.ExecuteScalar().ToString()
            cmd.Dispose()

            con.Close()
        Catch ex As Exception
        End Try

    End Sub

    Public Sub reset()

        txtFirstName.Value = ""

        txtLastName.Value = ""


        UserID.Value = ""

        txtguestemail.Value = ""
        txtmobileno.Value = ""

        txtAgencyName.Value = ""
        txtissue.Text = ""
    End Sub
    'Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    reset()
    'End Sub
End Class
