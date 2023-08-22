<%@ Application Language="VB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    'Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim app As HttpApplication = CType(sender, HttpApplication)
    '    Dim acceptEncoding As String = app.Request.Headers("Accept-Encoding")
    '    Dim prevUncompressedStream As Stream = app.Response.Filter

    '    If (IsNothing(acceptEncoding) Or acceptEncoding.Length = 0) Then
    '        Return
    '    End If
        
    '    If (Request.Path.EndsWith("axd")) Then
    '        Return
    '    End If
        
    '    acceptEncoding = acceptEncoding.ToLower()

    '    If (acceptEncoding.Contains("gzip")) Then
    '        ' gzip
            
    '        app.Response.Filter = New GZipStream(prevUncompressedStream, CompressionMode.Compress)
    '        app.Response.AppendHeader("Content-Encoding", "gzip")
    '    ElseIf (acceptEncoding.Contains("deflate")) Then
    '        ' defalte
    '        app.Response.Filter = New DeflateStream(prevUncompressedStream, CompressionMode.Compress)
    '        app.Response.AppendHeader("Content-Encoding", "deflate")
    '    End If
    'End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>