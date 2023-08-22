Imports System.Web

Partial Class Hotel_HtlResult
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx", False)
        End If
    End Sub
End Class
