Imports System.Data
Partial Class SprReports_Distr_DepositeOffice
    Inherits System.Web.UI.Page
    Dim Distr As New Distributor

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ((Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "DI") Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            Bindgrid()
        End If

    End Sub

    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Try
            Dim i As Integer
            i = Distr.InsertDepositeBranchName(txt_branchname.Text, Session("UID"), "INSERT", 0)
            If (i = 1) Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Branch name submitted sucessfully');", True)
            End If
            Bindgrid()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GrdMarkup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdMarkup.RowDeleting
        Try

            Dim Counter As Label = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label)
            Dim i As Integer
            i = Distr.InsertDepositeBranchName("", Session("UID"), "DELETE", Convert.ToInt32(Counter.Text))
            If i > 0 Then
                ShowAlertMessage("Branch details deleted successfully")
            Else
                ShowAlertMessage("Please Try Again")
            End If
            Bindgrid()
        Catch ex As Exception
            '  HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub
    Protected Sub GrdMarkup_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GrdMarkup.RowUpdating
        Try

            Dim BranchName As TextBox = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtBranchName"), TextBox), TextBox)
            Dim Counter As Label = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label)
            Dim i As Integer
            i = Distr.InsertDepositeBranchName(BranchName.Text, Session("UID"), "UPDATE", Convert.ToInt32(Counter.Text))
            GrdMarkup.EditIndex = -1
            If i > 0 Then
                ShowAlertMessage("Updated Successfully.")
                'Response.Redirect("~/Report/Admin/AdminHtlMarkup.aspx", False)
            Else
                ShowAlertMessage("Please try again")
            End If
            Bindgrid()
        Catch ex As Exception
        End Try

    End Sub
    Protected Sub GrdMarkup_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GrdMarkup.RowEditing
        GrdMarkup.EditIndex = e.NewEditIndex
        Bindgrid()
    End Sub

    Protected Sub GrdMarkup_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GrdMarkup.RowCancelingEdit
        GrdMarkup.EditIndex = -1
        Bindgrid()
    End Sub

    Protected Sub GrdMarkup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdMarkup.PageIndexChanging
        GrdMarkup.PageIndex = e.NewPageIndex
        Bindgrid()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Bindgrid()
        Try
            Dim ds As New DataSet
            ds = Distr.GetDistDepositeBranchName(Session("UID"))
            GrdMarkup.DataSource = ds
            GrdMarkup.DataBind()

        Catch ex As Exception

        End Try
    End Sub
End Class
