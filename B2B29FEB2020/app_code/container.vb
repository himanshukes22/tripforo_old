Imports Microsoft.VisualBasic
Imports System.SerializableAttribute
Imports System.Web.HttpContext
Imports System.Web.SessionState
Imports System.Resources
Imports System.Resources.ResourceWriter
Imports System.Reflection
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Diagnostics
Imports HtlLibrary

<Serializable()> _
Public Class Collection
    Private Shared _results As New Hashtable()
    Private Shared _resultsDtls As New Hashtable()
    Private Shared _SearchQuery As New Hashtable()
    Public Shared Function GetRoomDtls(ByVal id As String) As DataTable
        Return _results(id)
    End Function
    Public Shared Sub AddRoomDtls(ByVal id As String, ByVal value As DataTable)
        _results(id) = value
    End Sub
    Public Shared Sub RemoveRoomDtls(ByVal id As String)
        _results.Remove(id)
    End Sub
    Public Shared Function GetHtlDtls(ByVal id As String) As DataTable
        Return _resultsDtls(id)
    End Function
    Public Shared Sub AddHtlDtls(ByVal id As String, ByVal value As DataTable)
        _resultsDtls(id) = value
    End Sub
    Public Shared Sub RemoveHtlDtls(ByVal id As String)
        _resultsDtls.Remove(id)
    End Sub
    Public Shared Function GetSearchDtls(ByVal id As String) As Htl_Property
        Return _SearchQuery(id)
    End Function
    Public Shared Sub AddSearchDtls(ByVal id As String, ByVal value As Htl_Property)
        _SearchQuery(id) = value
    End Sub
    Public Shared Sub RemoveSearchDtls(ByVal id As String)
        _SearchQuery.Remove(id)
    End Sub
End Class
