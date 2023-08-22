Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Partial Class Reports_Admin_SalesExecRegistration
    Inherits System.Web.UI.Page

    Private S As New Sales()
    Dim con As New SqlConnection
    Dim cmd As SqlCommand
    Dim adap As SqlDataAdapter
    Public CID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                lbl_msg.Text = ""
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Submit_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Submit.Click
        Try


            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()

            cmd = New SqlCommand("SELECT EmailId, MobileNo FROM SalesExecReg WHERE EmailId='" & txt_EmailID.Text.Trim() & "' or MobileNo='" & txt_Mno.Text.Trim() & "'", con)
            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            If rdr.Read() Then
                Response.Write("<script>alert('You Have Already Registered !!')</script>")
            Else
                S.InsertSalesRegistration(txt_Fname.Text.Trim(), txt_LName.Text.Trim(), txt_Loc.Text.Trim(), txt_Mno.Text.Trim(), txt_EmailID.Text.Trim(), txt_Pwd.Text.Trim(), _
             "ACTIVE")
                S.ResetFormControlValues(Me)
                lbl_msg.Text = "Registration Sucessfully"
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            lbl_msg.Text = "Due To some Problem Sales Executive is not registered.Please try again."
        End Try
    End Sub
End Class

