Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_Admin_profile
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Dim user_type, str1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            user_type = UCase(Session("user_type"))
            If Not Page.IsPostBack Then
                str1 = "select * from admin_b2b where user_id='" & Session("UID") & "'"
                If con.State = ConnectionState.Open Then con.Close()
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Dim adp As SqlDataAdapter
                Dim dt1 As New DataTable
                Dim dt_admin As New DataTable
                adp = New SqlDataAdapter(str1, con)
                adp.Fill(dt_admin)
                Dim ctr = dt_admin.Rows.Count
                If ctr > 0 Then
                    txt_crd_val.Enabled = True
                    Try
                        Me.uid.Text = UCase((dt_admin.Rows(0).Item("user_id")).ToString())
                        Me.pwd.Text = UCase((dt_admin.Rows(0).Item("user_pwd")).ToString())
                        Me.Fname.Text = UCase((dt_admin.Rows(0).Item("Fname")).ToString()) & " " & UCase((dt_admin.Rows(0).Item("Lname")).ToString())
                        Me.Address.Text = UCase((dt_admin.Rows(0).Item("address")).ToString())
                        Me.txt_pcode.Text = UCase((dt_admin.Rows(0).Item("zipcode")).ToString())
                        Me.txt_city.Text = UCase((dt_admin.Rows(0).Item("city")).ToString())
                        Me.txt_country.Text = UCase((dt_admin.Rows(0).Item("country")).ToString())
                        Me.txt_hphone.Text = UCase((dt_admin.Rows(0).Item("contactno")).ToString())
                        Me.txt_wphone.Text = UCase((dt_admin.Rows(0).Item("Work_phone")).ToString())
                        Me.txt_mobile.Text = UCase((dt_admin.Rows(0).Item("Mobileno")).ToString())
                        Me.txt_email.Text = UCase((dt_admin.Rows(0).Item("email")).ToString())
                        Me.txt_crd.Text = UCase((dt_admin.Rows(0).Item("credit_limit")).ToString())
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Sub edit_admininfo()
        Try
            Dim uid, pwd, name, fname, lname, address, city, country, email, hphone, wphone, mobile, pin
            uid = Me.uid.Text
            pwd = Me.pwd.Text
            name = Me.Fname.Text
            name = Split(name, " ")
            fname = name(0)
            lname = name(1)

            address = Me.Address.Text
            city = Me.txt_city.Text
            country = Me.txt_country.Text
            email = Me.txt_email.Text
            hphone = Me.txt_hphone.Text
            wphone = Me.txt_wphone.Text
            mobile = Me.txt_mobile.Text
            pin = Me.txt_pcode.Text

            Dim cmd As SqlCommand
            Dim str As String


            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            str = "update admin_b2b set user_pwd='" & pwd & "',address='" & address & "',city='" & city & "',zipcode='" & pin & "',country='" & country & "',email='" & email & "',contactno='" & hphone & "', Work_phone='" & wphone & "',mobileno='" & mobile & "' where user_id='" & uid & "'"
            cmd = New SqlCommand(str, con)
            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Protected Sub Book_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Book.Click
        Try
            edit_admininfo()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class

