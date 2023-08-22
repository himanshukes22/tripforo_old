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
        Dim encrptrailid As New EncryptDecrypt.EncryptDecrypt
        Dim incoming As HttpContext = HttpContext.Current
            Dim oldpath As String = incoming.Request.Path.ToLower()
             If (InStr(oldpath, "/flight/")) Then
             
                Return "http://www.kandharitravels.in/irctc/e-train.aspx?rail=" & encrptrailid.TripleDESEncode(HttpContext.Current.Session("UID"), "RailService") & ""
            ElseIf (InStr(oldpath, "/adv_search/")) Then
            
                Return "http://www.kandharitravels.in/irctc/e-train.aspx?rail=" & encrptrailid.TripleDESEncode(HttpContext.Current.Session("UID"), "RailService") & ""
            Else
            
                Return "http://www.kandharitravels.in/irctc/e-train.aspx?rail=" & encrptrailid.TripleDESEncode(HttpContext.Current.Session("UID"), "RailService") & ""
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