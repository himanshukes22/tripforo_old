Imports System.Data
Imports System.IO
Imports System.Net
Imports Ionic.Zip
Imports System.Data.SqlClient
Partial Class PasswordRedirect
    Inherits System.Web.UI.Page
    Dim STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID_USER") = "" Or Session("UID_USER") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            logIDDiv.Visible = False
            Dim STDom As New SqlTransactionDom

            Dim dt As DataTable
            dt = STDom.GetAgencyDetails(Session("UID_USER").ToString).Tables(0)
                'txt_aemail.Text = dt.Rows(0)("Alt_Email").ToString()
                'txt_fax.Text = dt.Rows(0)("Fax_no").ToString()
                'txt_landline.Text = dt.Rows(0)("Phone").ToString()
                txtAgencyName.Text = dt.Rows(0)("Agency_Name").ToString()
                oldpasshndfld.Value = dt.Rows(0)("PWD").ToString()
                'GST

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub



    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Try
            divErrorMsg.InnerText = ""
            divErrorMsg.Visible = False

            Dim ReturnValue As Integer = 0
            If (txt_oldpassword.Text.Trim = oldpasshndfld.Value.Trim) Then

                If (txt_password.Text.Trim = txt_cpassword.Text.Trim) Then
                    If txt_password.Text <> "" AndAlso txt_password.Text IsNot Nothing Then
                        ReturnValue = UpdateAgentProfileDetails("Login", Session("UID_USER"), txt_password.Text.Trim, txt_oldpassword.Text.Trim)

                        If ReturnValue > 0 Then
                            'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "<scriptlanguage='JavaScript'>alert(Password Is Changed Successfully');</script>")
                            logIDDiv.Visible = True
                        Else
                            'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Try Again');", True)
                            divErrorMsg.InnerText = "Some error occured, please try again !"
                            divErrorMsg.Visible = True

                        End If
                    Else
                        'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Enter Password');", True)
                        divErrorMsg.InnerText = "Enter Password"
                        divErrorMsg.Visible = True
                    End If

                Else
                    'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Enter Same Password');", True)
                    divErrorMsg.InnerText = "Enter Same Password"
                    divErrorMsg.Visible = True
                End If
            Else
                'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Enter old Password Correctly');", True)
                divErrorMsg.InnerText = "Enter Old Password Correctly"
                divErrorMsg.Visible = True
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
  
    Public Function UpdateAgentProfileDetails(ByVal Type As String, ByVal UID As String, ByVal pwd As String, ByVal oldPassword As String) As Integer


        Dim paramHashtable As New Hashtable
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        paramHashtable.Clear()
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@pwd", pwd)
        paramHashtable.Add("@Oldpwd", oldPassword)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateAgentProfilePassword", 1)
    End Function

    Protected Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Response.Redirect("~/Login.aspx")
    End Sub
End Class
