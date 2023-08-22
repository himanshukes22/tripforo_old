Imports System.Data.SqlClient
Imports System.Data

Partial Class SprReports_Accounts_ChangePassword
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblmsg.Text = ""

    End Sub

    Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        Dim result As Integer = 0
        Try

            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_changePassword"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@userID", SqlDbType.NVarChar, 500).Value = Session("UID").ToString()
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 500).Value = TextBoxNewPass.Text.Trim()
            cmd.Connection = con
            con.Open()
            result = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

        If (result > 0) Then
            lblmsg.Text = "Password Changed sucessfully."

        Else

            lblmsg.Text = " unable to change Password, Please try after some time."

        End If
    End Sub

End Class
