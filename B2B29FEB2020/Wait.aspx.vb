
Partial Class Wait
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("BookIng") = "TRUE" Then
            Else
                Session("BookIng") = "FALSE"
            End If
            If Session("RTFBookIng") = "TRUE" Then
            Else
                Session("RTFBookIng") = "FALSE"
            End If
            If Session("IntBookIng") = "TRUE" Then
            Else
                Session("IntBookIng") = "FALSE"
            End If

        Catch ex As Exception
           
        End Try

    End Sub
End Class
