
Partial Class LccRF_LccBkgConfirmation
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Session("RTFBookIng") = "TRUE" Then
            lblTkt.Text = Session("DomRtfStrTktCopy")
        End If
    End Sub
End Class
