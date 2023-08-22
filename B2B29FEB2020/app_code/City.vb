Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Collections.Generic

''' <summary>
''' Summary description for City
''' </summary>
Public Class City
    Public Property ID() As Integer
        Get
            Return m_ID
        End Get
        Set(ByVal value As Integer)
            m_ID = Value
        End Set
    End Property
    'For Agency
    Public Property Agency_Name() As String
        Get
            Return m_AgencyName
        End Get
        Set(ByVal value As String)
            m_AgencyName = value
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
    Private m_AgencyName As String
    Private m_User_Id As String
    Private m_ID As Integer
    Public Property CityName() As String
        Get
            Return m_CityName
        End Get
        Set(ByVal value As String)
            m_CityName = Value
        End Set
    End Property
    Private m_CityName As String
    Public Property AirportCode() As String
        Get
            Return m_AirportCode
        End Get
        Set(ByVal value As String)
            m_AirportCode = value
        End Set
    End Property
    Private m_AirportCode As String
    Public Property CountryCode() As String
        Get
            Return m_CountryCode
        End Get
        Set(ByVal value As String)
            m_CountryCode = value
        End Set
    End Property
    Private m_CountryCode As String
    Public Property CountryName() As String
        Get
            Return m_CountryName
        End Get
        Set(ByVal value As String)
            m_CountryName = value
        End Set
    End Property
    Private m_CountryName As String
    Public Property ALName() As String
        Get
            Return m_ALName
        End Get
        Set(ByVal value As String)
            m_ALName = value
        End Set
    End Property
    Private m_ALName As String
    Public Property ALCode() As String
        Get
            Return m_ALCode
        End Get
        Set(ByVal value As String)
            m_ALCode = value
        End Set
    End Property
    Private m_ALCode As String

    Public Property HName() As String
        Get
            Return m_HName
        End Get
        Set(ByVal value As String)
            m_HName = value
        End Set
    End Property
    Private m_HName As String
    '
    ' TODO: Add constructor logic here
    '

    Public Sub New()
    End Sub
    Public Property HtlCountry() As String
        Get
            Return m_HtlCountry
        End Get
        Set(ByVal value As String)
            m_HtlCountry = value
        End Set
    End Property
    Private m_HtlCountry As String
    Public Function GetCityList(ByVal dt As DataTable) As List(Of City)
        Dim cityList As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            cityList.Add(New City() With {.ID = i, .CityName = dt.Rows(i)("CityName").ToString().Trim(), .AirportCode = dt.Rows(i)("AirportCode").ToString().Trim(), .CountryCode = dt.Rows(i)("CountryCode").ToString().Trim()})
        Next
        Return cityList
    End Function
    Public Function GetAirlineList(ByVal dt As DataTable) As List(Of City)
        Dim airlineList As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            airlineList.Add(New City() With {.ID = i, .ALName = dt.Rows(i)("AL_Name").ToString().Trim(), .ALCode = dt.Rows(i)("AL_Code").ToString().Trim()})
        Next
        Return airlineList
    End Function
    'Hotel Search
    Public Function GetHtlName(ByVal HtlArray As Array) As List(Of City)
        Dim HtlName As New List(Of City)()
        For i As Integer = 0 To HtlArray.Length - 1
            HtlName.Add(New City() With {.ID = i, .HName = (HtlArray(i)("HtlName"))})
        Next
        Return HtlName
    End Function

    ''HtlCity Search
    'Public Function GetHtlCityList(ByVal dt As DataTable) As List(Of City)
    '    Dim cityList As New List(Of City)()
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        cityList.Add(New City() With {.ID = i, .CityName = dt.Rows(i)("CityName").ToString().Trim(), .AirportCode = dt.Rows(i)("CityCode").ToString().Trim(), .CountryCode = dt.Rows(i)("CountryCode").ToString().Trim()})
    '    Next
    '    Return cityList
    'End Function
    'For Agency
    Public Function GetAgencyList(ByVal dt As DataTable) As List(Of City)
        Dim AgencyList As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            AgencyList.Add(New City() With {.ID = i, .Agency_Name = dt.Rows(i)("Agency_Name").ToString().Trim(), .User_Id = dt.Rows(i)("User_Id").ToString().Trim()})
        Next
        Return AgencyList
    End Function
    ''Hotel City Search
    'Public Function GetHtlCityList(ByVal dt As DataTable) As List(Of City)
    '    Dim cityList As New List(Of City)()
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        cityList.Add(New City() With {.ID = i, .CityName = dt.Rows(i)("CityName").ToString().Trim(), .AirportCode = dt.Rows(i)("CityCode").ToString().Trim(), .CountryCode = dt.Rows(i)("CountryCode").ToString().Trim(), .HtlCountry = dt.Rows(i)("Country").ToString().Trim()})
    '    Next
    '    Return cityList
    'End Function

    'Hotel City Search
    Public Function GetHtlCityList(ByVal dt As DataTable) As List(Of City)
        Dim cityList As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            cityList.Add(New City() With {.ID = i, .CityName = dt.Rows(i)("CityName").ToString().Trim(), .AirportCode = dt.Rows(i)("CityCode").ToString().Trim(), .CountryCode = dt.Rows(i)("CountryCode").ToString().Trim(), .HtlCountry = dt.Rows(i)("Country").ToString().Trim(), .ALCode = dt.Rows(i)("SearchType").ToString().Trim()})
        Next
        Return cityList
    End Function
    'Country and City Search for Hotel Markup 
    Public Function GetHotelHarkupCitySearch(ByVal dt As DataTable) As List(Of City)
        Dim cityList As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            cityList.Add(New City() With {.ID = i, .CityName = dt.Rows(i)("Name").ToString().Trim(), .AirportCode = dt.Rows(i)("Code").ToString().Trim()})
        Next
        Return cityList
    End Function
    Public Function GetCountryCode(ByVal dt As DataTable) As List(Of City)
        Dim Countrylist As New List(Of City)()
        For i As Integer = 0 To dt.Rows.Count - 1
            Countrylist.Add(New City() With {.ID = i, .CountryName = dt.Rows(i)("CountryName").ToString().Trim(), .CountryCode = dt.Rows(i)("CountryCode").ToString().Trim()})
        Next
        Return Countrylist
    End Function
End Class