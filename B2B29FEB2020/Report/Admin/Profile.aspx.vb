Imports System.Data.SqlClient
Imports System.Data
Partial Class SprReports_Admin_Profile
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 If Session("UID") = "" Or Session("UID") Is Nothing Or Session("User_Type") <> "EXEC" Then
            Response.Redirect("~/Login.aspx")
        End If
        Dim adap As SqlDataAdapter
        Info()
        Dim dt As New DataTable
        adap = New SqlDataAdapter("SP_GETEXECDETAILS", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@UID", Session("UID"))
        adap.Fill(dt)

        If Not IsPostBack Then
            txt_email.Text = dt.Rows(0)("EmailId").ToString()
            txt_Mobile.Text = dt.Rows(0)("MobileNo").ToString()
            txt_name.Text = dt.Rows(0)("Name").ToString()
        End If

    End Sub
    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Try
            If (txt_password.Text.Trim = txt_cpassword.Text.Trim) Then
                If txt_password.Text <> "" AndAlso txt_password.Text IsNot Nothing Then
                    con.Open()
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand("SP_UPDATE_EXECDETAILS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@UPDATETYPE", "LOGIN")
                    cmd.Parameters.AddWithValue("@UID", Session("UID").ToString())
                    cmd.Parameters.AddWithValue("@PWD", txt_cpassword.Text.Trim())
                    cmd.Parameters.AddWithValue("@NAME", "")
                    cmd.Parameters.AddWithValue("@EMAILID", "")
                    cmd.Parameters.AddWithValue("@MOBILE", "")
                    Dim i As Integer = cmd.ExecuteNonQuery()
                    td_login.Visible = True
                    td_login1.Visible = False
                    lbl_msg.Text = ""
                Else
                    lbl_msg.Text = "Enter Password"
                End If

            Else
                lbl_msg.Text = "Enter Same Password"
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Saveadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Saveadd.Click
        Try
            con.Open()
            Dim cmd As SqlCommand
            cmd = New SqlCommand("SP_UPDATE_EXECDETAILS", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UPDATETYPE", "ADDRESS")
            cmd.Parameters.AddWithValue("@UID", Session("UID").ToString())
            cmd.Parameters.AddWithValue("@PWD", "")
            cmd.Parameters.AddWithValue("@NAME", txt_name.Text.Trim())
            cmd.Parameters.AddWithValue("@EMAILID", txt_email.Text.Trim())
            cmd.Parameters.AddWithValue("@MOBILE", txt_Mobile.Text.Trim())
            Dim i As Integer = cmd.ExecuteNonQuery()
            td_Address.Visible = True
            td_Address1.Visible = False
            Info()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub LinkEditAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkEditAdd.Click
        Try
            td_Address.Visible = False
            td_Address1.Visible = True

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub lnk_CancelAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_CancelAdd.Click
        Try
            td_Address.Visible = True
            td_Address1.Visible = False

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub LinkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkEdit.Click
        Try
            td_login.Visible = False
            td_login1.Visible = True

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnk_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Cancel.Click
        Try
            td_login1.Visible = False
            td_login.Visible = True
            lbl_msg.Text = ""

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub Info()
        Try


            Dim dtinfo As New DataTable
            ' If Not IsPostBack Then
            Dim adap As SqlDataAdapter

            adap = New SqlDataAdapter("SP_GETEXECDETAILS", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@UID", Session("UID"))
            adap.Fill(dtinfo)

            td_username.InnerText = dtinfo.Rows(0)("user_id").ToString()
            td_name.InnerText = dtinfo.Rows(0)("Name").ToString()
            td_email.InnerText = dtinfo.Rows(0)("EmailId").ToString()
            td_mobile.InnerText = dtinfo.Rows(0)("MobileNo").ToString()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
