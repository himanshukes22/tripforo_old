Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class UrlMapping
    Public Function GetMappedString(ByVal pagename As String) As String
        Dim mappped As String
        Dim conn As New SqlConnection()
        Try
            conn.ConnectionString = "Data Source=172.18.80.75;Initial Catalog=ITZ;User ID=sa;Password=Password123;Max Pool Size=1000"
            Dim cmd As New SqlCommand("select mapurl from Tbl_Urls_To_Map where pagename='" + pagename.Trim() + "'")
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = conn
            If (conn.State = Data.ConnectionState.Closed) Then
                conn.Open()
            End If
            mappped = cmd.ExecuteScalar()
            If conn.State = Data.ConnectionState.Open Then
                conn.Close()
            End If
            cmd.Dispose()
        Catch ex As Exception
            Dim abc As String
        End Try
        Return mappped
    End Function
    Public Function GetMappedString2(ByVal mapurl As String) As String
        Dim mappped As String
        Dim conn As New SqlConnection()
        Try
            conn.ConnectionString = "Data Source=172.18.80.75;Initial Catalog=ITZ;User ID=sa;Password=Password123;Max Pool Size=1000"
            Dim cmd As New SqlCommand("select pagename from Tbl_Urls_To_Map where mapurl='" + mapurl.Trim() + "'")
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = conn
            If (conn.State = Data.ConnectionState.Closed) Then
                conn.Open()
            End If
            mappped = cmd.ExecuteScalar()
            If conn.State = Data.ConnectionState.Open Then
                conn.Close()
            End If
            cmd.Dispose()
        Catch ex As Exception
            Dim abc As String
        End Try
        Return mappped
    End Function

    Public Function GetMappedDataSet() As DataSet
        Dim mappped As New DataSet()
        Dim conn As New SqlConnection()
        Try
            'If (HttpContext.Current.Session("UrlTable") IsNot Nothing) Then
            '    mappped = CType(HttpContext.Current.Session("UrlTable"), DataSet)
            'Else
            conn.ConnectionString = "Data Source=172.18.80.75;Initial Catalog=ITZ;User ID=sa;Password=Password123;Max Pool Size=1000"
            Dim da As New SqlDataAdapter("select * from Tbl_Urls_To_Map", conn)
            da.Fill(mappped)
            'HttpContext.Current.Session("UrlTable") = mappped
            da.Dispose()
            'End If
        Catch ex As Exception
        End Try
        Return mappped
    End Function

End Class
