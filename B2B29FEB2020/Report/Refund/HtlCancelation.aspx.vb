
Partial Class HtlCancelation
    Inherits System.Web.UI.Page
    '' Dim ccraObj As New HtlLibrary.CCRA
    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        ''lblConfi.Text = ccraObj.htlcalncle(txtCancle.Text)
        'tblbookInfo.Visible = False
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'tblbookInfo.Visible = True
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
    End Sub
End Class
