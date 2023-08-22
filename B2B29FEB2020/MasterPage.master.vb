
Imports IPTracker
Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim State As New StateCollection()
            Dim objIP As New IPDetails()
            State.SessionID = Session.SessionID
            State.Path = Request.CurrentExecutionFilePath
            State.Username = Page.User.Identity.Name
            State.VISTING_TIME = DateTime.Now.ToString()
            Dim objST As New SessionTrack()
            objST.Add(State, Request.CurrentExecutionFilePath)
        Catch ex As Exception
        End Try
    End Sub
  
End Class
