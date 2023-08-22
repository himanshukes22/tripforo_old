Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

''' <summary> 
''' Summary description for FltRequest 
''' </summary> 
Public Class FltRequest
    Private _Trip As String
    Private _DestFrom As String
    Private _DestTo As String
    Private _Deptdate As String
    Private _Retdate As String
    Private _Cabin As String
    Private _Currency As String
    Private _Adult As Int16
    Private _Child As Int16
    Private _Infant As Int16
    Private _Airline As String
    Private _Preference As String
    Private _NoOfPax As Int16
    Private _NsF As String
    Private _TimeD As String
    Private _TimeR As String
    Public Property Trip() As String
        Get
            Return _Trip
        End Get
        Set(ByVal value As String)
            _Trip = value
        End Set
    End Property
    Public Property DestFrom() As String
        Get
            Return _DestFrom
        End Get
        Set(ByVal value As String)
            _DestFrom = value
        End Set
    End Property
    Public Property DestTo() As String
        Get
            Return _DestTo
        End Get
        Set(ByVal value As String)
            _DestTo = value
        End Set
    End Property
    Public Property Deptdate() As String
        Get
            Return _Deptdate
        End Get
        Set(ByVal value As String)
            _Deptdate = value
        End Set
    End Property
    Public Property Retdate() As String
        Get
            Return _Retdate
        End Get
        Set(ByVal value As String)
            _Retdate = value
        End Set
    End Property

    Public Property Cabin() As String
        Get
            Return _Cabin
        End Get
        Set(ByVal value As String)
            _Cabin = value
        End Set
    End Property
    Public Property Currency() As String
        Get
            Return _Currency
        End Get
        Set(ByVal value As String)
            _Currency = value
        End Set
    End Property
    Public Property Adult() As Int16
        Get
            Return _Adult
        End Get
        Set(ByVal value As Int16)
            _Adult = value
        End Set
    End Property
    Public Property Child() As Int16
        Get
            Return _Child
        End Get
        Set(ByVal value As Int16)
            _Child = value
        End Set
    End Property
    Public Property Infant() As Int16
        Get
            Return _Infant
        End Get
        Set(ByVal value As Int16)
            _Infant = value
        End Set
    End Property
    Public Property Airline() As String
        Get
            Return _Airline
        End Get
        Set(ByVal value As String)
            _Airline = value
        End Set
    End Property
    Public Property Preference() As String
        Get
            Return _Preference
        End Get
        Set(ByVal value As String)
            _Preference = value
        End Set
    End Property
    Public Property NoOfPax() As Int16
        Get
            Return _NoOfPax
        End Get
        Set(ByVal value As Int16)
            _NoOfPax = value
        End Set
    End Property
    Public Property NoNStop() As String
        Get
            Return _NsF
        End Get
        Set(ByVal value As String)
            _NsF = value
        End Set
    End Property
    Public Property TimeD() As String
        Get
            Return _TimeD
        End Get
        Set(ByVal value As String)
            _TimeD = value
        End Set
    End Property
    Public Property TimeR() As String
        Get
            Return _TimeR
        End Get
        Set(ByVal value As String)
            _TimeD = value
        End Set
    End Property
End Class