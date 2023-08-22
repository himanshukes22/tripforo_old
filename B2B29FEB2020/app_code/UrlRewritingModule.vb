Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Data
Imports System.Linq
Imports System.Web.SessionState
Imports System.Diagnostics
Imports System.Web.Security
Imports System.Xml.Linq

Public Class ITZUrlRewritingModule
    Implements IHttpModule, IRequiresSessionState
    Dim objUm As New UrlMapping()

    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

    End Sub

    Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
        AddHandler (context.BeginRequest), New EventHandler(AddressOf context_BeginRequest)
        AddHandler (context.PreRequestHandlerExecute), New EventHandler(AddressOf context_PreRequestHandlerExecute)
        AddHandler (context.EndRequest), New EventHandler(AddressOf context_EndRequest)
        AddHandler (context.AuthorizeRequest), New EventHandler(AddressOf context_AuthorizeRequest)
    End Sub

    Private Sub context_AuthorizeRequest(ByVal sender As Object, ByVal e As EventArgs)
        'We change uri for invoking correct handler
        Dim context As HttpContext = (CType(sender, HttpApplication)).Context

        ''If context.Request.RawUrl.Contains("/FlightSearch") Then
        ''    Dim url As String = context.Request.RawUrl.Replace("/FlightSearch", "/Search.aspx")
        ''    context.RewritePath(url)
        ''End If 
        If context.Items("originalUrl") IsNot Nothing Then
            Try
                If context.Request.RawUrl.Contains(CType(context.Items("originalUrl"), String)) Then
                    Dim qStr As String = ""
                    If context.Request.RawUrl.Contains("?") Then
                        qStr = context.Request.RawUrl.Substring(context.Request.RawUrl.IndexOf("?"), context.Request.RawUrl.Length - context.Request.RawUrl.IndexOf("?"))
                    End If
                    Try
                        Dim umUrl = CType(context.Items("originalUrl"), String).Substring(1)
                        Dim urlDoc = XDocument.Load(context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
                        Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
                        Dim pageName = (From i In isSameVal Where i.Element("MapUrl").Value.ToLower() = umUrl.ToLower() Select i.Element("PageName").Value).FirstOrDefault()
                        Dim url As String = context.Request.RawUrl.Replace(context.Request.RawUrl, "/" + pageName + qStr)
                        context.RewritePath(url)
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub




    Private Sub context_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        'We set back the original url on browser
        Dim context As HttpContext = (CType(sender, HttpApplication)).Context

        If Not context.Items("originalUrl") Is Nothing Then
            ' ''context.RewritePath(CType(context.Items("originalUrl"), String))
            ' ''If context.Request.RawUrl.Contains("?") Then
            ' ''    context.RewritePath(context.Request.RawUrl.Substring(0, context.Request.RawUrl.IndexOf("?")))
            ' ''Else
            context.RewritePath(context.Items("originalUrl"))
            ''''End If

        End If
    End Sub

    Private Sub context_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        'We processed the request
    End Sub

    Private Sub context_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        'We received a request, so we save the original URL here
        Dim context As HttpContext = (CType(sender, HttpApplication)).Context
        Try
            Dim urlDoc = XDocument.Load(context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
            Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
            Dim abcStr As String = ""
            Dim rawurl As String = ""
            Try
                If context.Request.RawUrl.Contains("?") Then
                    abcStr = context.Request.RawUrl.Substring(1, context.Request.RawUrl.IndexOf("?") - 1)
                Else
                    abcStr = context.Request.RawUrl.Substring(1).ToString()
                End If
            Catch ex As Exception

            End Try
            Try
                Dim abc = (From i In isSameVal Where i.Element("MapUrl").Value.Trim().ToLower() = abcStr.Trim().ToLower() Select i.Element("MapUrl").Value.Trim()).FirstOrDefault()
                If abc Is Nothing Then
                    abc = ""
                End If
                If abcStr.Trim().ToLower().Equals(abc.Trim().ToLower()) Then
                    Try
                        If context.Request.RawUrl.Contains("?") Then
                            context.Items("originalUrl") = context.Request.RawUrl.Substring(0, context.Request.RawUrl.IndexOf("?"))
                        Else
                            context.Items("originalUrl") = context.Request.RawUrl
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub

    ' a temp handler used to force the SessionStateModule to load session state
    'Public Class MyHttpHandler
    '    Implements IHttpHandler
    '    Implements IRequiresSessionState
    '    Friend ReadOnly OriginalHandler As IHttpHandler

    '    Public Sub New(originalHandler__1 As IHttpHandler)
    '        OriginalHandler = originalHandler__1
    '    End Sub

    '    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
    '        ' do not worry, ProcessRequest() will not be called, but let's be safe
    '        Throw New InvalidOperationException("MyHttpHandler cannot process requests.")
    '    End Sub

    '    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
    '        ' IsReusable must be set to false since class has a member!
    '        Get
    '            Return False
    '        End Get
    '    End Property
    'End Class


    ''Public Function GetMappedString2(ByVal mapurl As String) As String
    ''    Dim mappped As String
    ''    Dim conn As New SqlConnection()
    ''    Try
    ''        conn.ConnectionString = "Data Source=172.18.80.75;Initial Catalog=ITZ;User ID=sa;Password=Password123;Max Pool Size=1000"
    ''        Dim cmd As New SqlCommand("select pagename from Tbl_Urls_To_Map where mapurl='" + mapurl.Trim() + "'")
    ''        cmd.CommandType = Data.CommandType.Text
    ''        cmd.Connection = conn
    ''        If (conn.State = Data.ConnectionState.Closed) Then
    ''            conn.Open()
    ''        End If
    ''        mappped = cmd.ExecuteScalar()
    ''        If conn.State = Data.ConnectionState.Open Then
    ''            conn.Close()
    ''        End If
    ''        cmd.Dispose()
    ''    Catch ex As Exception
    ''        Dim abc As String
    ''    End Try
    ''    Return mappped
    ''End Function



End Class
Public Class UrlXmlDataSet
    Public Property IsSame As Boolean
    Public Property UrlPageName As String
End Class
