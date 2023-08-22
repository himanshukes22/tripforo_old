Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Reports_Sales_Profile
    Inherits System.Web.UI.Page
    Private S As New Sales()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'S.SalesExecProfile("pari@gmail.com");
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            Try




                Dim ds As New DataSet()
                ds = S.SalesExecProfile(Session("UID").ToString)
                Dim dt As New DataTable()
                dt = ds.Tables(0)
                td_Fname.InnerText = dt.Rows(0)("FirstName").ToString()
                td_Lname.InnerText = dt.Rows(0)("LastName").ToString()
                td_Location.InnerText = dt.Rows(0)("Location").ToString()
                txt_MNo.Text = dt.Rows(0)("MobileNo").ToString()
                td_Email.InnerText = dt.Rows(0)("EmailId").ToString()
                txt_Pwd.Text = dt.Rows(0)("Password").ToString()
                td_RegDate.InnerText = dt.Rows(0)("RegDate").ToString()
                ID.InnerText = dt.Rows(0)("ID").ToString()
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
    End Sub
    Protected Sub btn_Update_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try


            S.UpdateSalesExecProfile(ID.InnerText, txt_MNo.Text, txt_Pwd.Text)
            td_msg.InnerText = "Profile Updated Sucessfully"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class

