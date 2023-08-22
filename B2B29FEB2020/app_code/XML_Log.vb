Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic


Public Class XML_Log
    Public Sub InsertLogs(ByVal XmlReq As String, ByVal XmlRes As String, ByVal Searchvalue As String, ByVal destfrom As String, ByVal destto As String)
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        con.Open()
        cmd = New SqlCommand("insert into searchLog(RequestXml,ResponseXml,DestFrmDestTo,searchValue)values('" & XmlReq & "','" & XmlRes & "','" & destfrom & destto & "','" & Searchvalue & "')", con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
    Public Function datecon(ByVal MM)
        Dim mm_str
        Select Case MM
            Case "01"
                mm_str = "JAN"
            Case "02"
                mm_str = "FEB"
            Case "03"
                mm_str = "MAR"
            Case "04"
                mm_str = "APR"
            Case "05"
                mm_str = "MAY"
            Case "06"
                mm_str = "JUN"
            Case "07"
                mm_str = "JUL"
            Case "08"
                mm_str = "AUG"
            Case "09"
                mm_str = "SEP"
            Case "10"
                mm_str = "OCT"
            Case "11"
                mm_str = "NOV"
            Case "12"
                mm_str = "DEC"
            Case Else

        End Select

        Return mm_str

    End Function
End Class
