
Partial Class NoResult
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("HtlError") <> "" Then
            errormsg.Text = Request.QueryString("HtlError")
        End If
    End Sub
End Class
