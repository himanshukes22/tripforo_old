Imports Microsoft.VisualBasic

Public Class FltDetails

    Public Property DepDate() As String
        Get
            Return m_DepDate
        End Get
        Set(ByVal value As String)
            m_DepDate = value
        End Set
    End Property
    Private m_DepDate As String
    Public Property Pnr() As String
        Get
            Return m_Pnr
        End Get
        Set(ByVal value As String)
            m_Pnr = value
        End Set
    End Property
    Private m_Pnr As String



    Public Property DepTime() As String
        Get
            Return m_DepTime
        End Get
        Set(ByVal value As String)
            m_DepTime = value
        End Set
    End Property
    Private m_DepTime As String


    Public Property Sector() As String
        Get
            Return m_Sector
        End Get
        Set(ByVal value As String)
            m_Sector = Value
        End Set
    End Property
    Private m_Sector As String

    Public Property OrderID() As String
        Get
            Return m_OrderID
        End Get
        Set(ByVal value As String)
            m_OrderID = Value
        End Set
    End Property
    Private m_OrderID As String
End Class