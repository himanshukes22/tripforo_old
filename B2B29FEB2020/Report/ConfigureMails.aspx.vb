Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class SprReports_ConfigureMails
    Inherits System.Web.UI.Page
    Dim objDS As New Distributor()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetData()
        End If
    End Sub

    Protected Sub btnConfigureMail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfigureMail.Click
        Dim rows As Integer = 0
        Dim isAct As String = ""
        isAct = IIf(rbtnIsActiveTrue.Checked = True, "True", "False")
        rows = objDS.InsertUpdateConfigureMail(0, ddlModuleType.SelectedItem.Text.Trim(), txtToEmail.Text.Trim(), Session("UID").ToString(), isAct)
        If rows > 0 Then
            GetData()
            ''ClientScriptManager.RegisterStartupScript(Me.Page.GetType(), "alert", "alert('Data set successfully');")
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data submit successfully');", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data could not submit,pleaes try after some time');", True)
        End If
    End Sub

    Public Sub GetData()
        Dim ds As New DataSet()
        ds = objDS.GetConfigureMails()
        Session("MailConfigData") = ds
        GridView1.DataSource = ds
        GridView1.DataBind()
        If ds.Tables(0).Rows.Count > 0 Then
            divReport.Visible = True
        Else
            divReport.Visible = False
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ctl As New Label()
                ctl = DirectCast(e.Row.FindControl("lbl_IsActive"), Label)
                If ctl.Text.Trim().ToLower() = "true" Then
                    DirectCast(e.Row.FindControl("lbl_IsActive"), Label).Text = "Active"
                ElseIf ctl.Text.Trim().ToLower() = "false" Then
                    DirectCast(e.Row.FindControl("lbl_IsActive"), Label).Text = "Deactive"
                End If
            End If
        Catch ex As Exception

        End Try
        
    End Sub


    'Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    Try
    '        'If e.CommandName = "Edit" Then
    '        '    'GridView1.EditIndex = e.CommandSource
    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            GridView1.EditIndex = e.NewEditIndex
            GridView1.DataSource = DirectCast(Session("MailConfigData"), DataSet)
            GridView1.DataBind()
            If DirectCast(Session("MailConfigData"), DataSet).Tables(0).Rows.Count > 0 Then
                divReport.Visible = True
            Else
                divReport.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        Try
            Dim cmd As String = ""
            cmd = "Update"
            Dim rows As Integer = 0
            Dim sno As Integer = 0
            sno = Convert.ToInt32(DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_Sno"), Label).Text)
            Dim mail As String = ""
            mail = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txt_ToEmail"), TextBox).Text
            Dim isact As String = ""
            isact = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddlActDact"), DropDownList).SelectedItem.Value
            rows = objDS.UpdateDeleteConfigureMail(sno, "", mail, Session("UID").ToString(), isact, cmd)
            If rows > 0 Then
                GridView1.EditIndex = -1
                GetData()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data updated successfully');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data could not updated, pleaes try after some time');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            Dim cmd As String = ""
            cmd = "Delete"
            Dim rows As Integer = 0
            Dim sno As Integer = 0
            sno = Convert.ToInt32(DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_Sno"), Label).Text)
            rows = objDS.UpdateDeleteConfigureMail(sno, "", "", Session("UID").ToString(), "", cmd)
            If rows > 0 Then
                GetData()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data deleted successfully');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "javascript:alert('Data could not deleted, pleaes try after some time');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        Try
            GridView1.EditIndex = -1
            GetData()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        GetData()
    End Sub
End Class
