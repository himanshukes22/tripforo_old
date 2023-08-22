Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Diagnostics
<Serializable()> _
Public Class CollectInformation
    Private Shared _results As New Hashtable()
    Private Shared _results1 As New Hashtable()
    '***************************** New Indigo**********************************
    Public Shared Function GetResultString_I(ByVal id As String) As String
        If _results.Contains(id) Then
            Return _results(id)
        Else
            Dim a As String
            Return a
        End If
    End Function
    Public Shared Sub AddSTRING_I(ByVal id As String, ByVal value As String)
        _results(id) = value
    End Sub

    Public Shared Sub RemoveString_I(ByVal id As String)
        _results.Remove(id)
    End Sub
    '***************************** New Indigo**********************************


    '***************************** New SpiceJet**********************************
    Public Shared Function GetResultString_S(ByVal id As String) As String
        If _results1.Contains(id) Then
            Return _results1(id)
        Else
            Dim a As String
            Return a
        End If
    End Function
    Public Shared Sub AddSTRING_S(ByVal id As String, ByVal value As String)
        _results1(id) = value
    End Sub

    Public Shared Sub RemoveString_S(ByVal id As String)
        _results1.Remove(id)
    End Sub
    '***************************** New SpiceJet**********************************

End Class
