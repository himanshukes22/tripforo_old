
Partial Class Hotel_HtlBookwait
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
