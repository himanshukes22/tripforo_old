
Partial Class SprReports_Abc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim def As String
        Dim ghi As String
        def = Request.QueryString("Value1").ToString()
        ghi = Request.QueryString("Value2").ToString()
    End Sub

End Class
