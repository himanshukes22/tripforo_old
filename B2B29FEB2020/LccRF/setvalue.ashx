<%@ WebHandler Language="VB" Class="setvalue" %>

Imports System
Imports System.Web

Public Class setvalue : Implements IHttpHandler, IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        context.Response.Write(GetRailUrl(context.Request("type")))
    End Sub
    Public Function GetRailUrl(ByVal type As String) As String
        If type = "RAIL" Then
        Dim incoming As HttpContext = HttpContext.Current
            Dim oldpath As String = incoming.Request.Path.ToLower()
             If (InStr(oldpath, "/flight/")) Then
             
                Return "../irctc/e-train.aspx?uid=" & HttpContext.Current.Session("UID") & ""
            ElseIf (InStr(oldpath, "/adv_search/")) Then
            
            Return "../irctc/e-train.aspx?uid=" & HttpContext.Current.Session("UID") & ""
            Else
            
 Return "irctc/e-train.aspx?uid=" & HttpContext.Current.Session("UID") & ""
            end if
            Else
            Return ""
        End If
    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
           Return False
        End Get
    End Property

End Class