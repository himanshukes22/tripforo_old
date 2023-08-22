Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Generic


Public Class Stockist
    Public Property ID() As Integer
        Get
            Return m_ID
        End Get
        Set(ByVal value As Integer)
            m_ID = Value
        End Set
    End Property
    Public Property User_Id() As String
        Get
            Return m_User_Id
        End Get
        Set(ByVal value As String)
            m_User_Id = value
        End Set
    End Property
    Public Property Agency_Name() As String
        Get
            Return m_AgencyName
        End Get
        Set(ByVal value As String)
            m_AgencyName = value
        End Set
    End Property
    'Public Property Agent_Type() As String
    '    Get
    '        Return Agent_Type
    '    End Get
    '    Set(ByVal value As String)
    '        Agent_Type = value
    '    End Set
    'End Property
    Private m_AgencyName As String
    Private m_User_Id As String
    Private m_ID As Integer
    'Public Property Crd_Limit() As String
    '    Get
    '        Return Crd_Limit
    '    End Get
    '    Set(ByVal value As String)
    '        Crd_Limit = value
    '    End Set
    'End Property
    'Public Property Agent_Limit() As String
    '    Get
    '        Return Agent_Limit
    '    End Get
    '    Set(ByVal value As String)
    '        Agent_Limit = value
    '    End Set
    'End Property

    Public Function GetStockistList(ByVal dt As DataTable) As List(Of Stockist)
        Dim AgencyList As New List(Of Stockist)()
        For i As Integer = 0 To dt.Rows.Count - 1
            AgencyList.Add(New Stockist() With {.ID = i, .Agency_Name = dt.Rows(i)("Agency_Name").ToString().Trim(), .User_Id = dt.Rows(i)("User_Id").ToString().Trim()})
        Next
        Return AgencyList
    End Function

End Class
