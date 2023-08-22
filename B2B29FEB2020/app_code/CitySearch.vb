Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ScriptService()> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CitySearch
    Inherits System.Web.Services.WebService
    <WebMethod()>
    Public Function FetchCityList(ByVal city As String) As List(Of City)
        Dim CityDS As DataSet
        Dim objSqlTrans As New SqlTransactionNew
        CityDS = objSqlTrans.GetAirportList(city)
        Dim ct = New City()
        Dim fetchCity = ct.GetCityList(CityDS.Tables(0))
        Return fetchCity.ToList()
    End Function

    <WebMethod()>
    Public Function FetchCityListFD(ByVal city As String) As List(Of City)
        Dim CityDS As DataSet
        Dim objSqlTrans As New SqlTransactionNew
        CityDS = objSqlTrans.GetAirportListFD(city)
        Dim ct = New City()
        Dim fetchCity = ct.GetCityList(CityDS.Tables(0))
        Return fetchCity.ToList()
    End Function
    <WebMethod()> _
      Public Function FetchAirlineList(ByVal airline As String) As List(Of City)
        Dim AirlineDS As DataSet
        Dim objSqlTrans As New SqlTransactionNew
        AirlineDS = objSqlTrans.GetAirlinesList(airline)
        Dim ct = New City()
        Dim fetchAirline = ct.GetAirlineList(AirlineDS.Tables(0))
        Return fetchAirline.ToList()
    End Function
    '<WebMethod()> _
    '  Public Function HtlCityList(ByVal city As String) As List(Of City)
    '    Dim CityDS As DataSet
    '    Dim HTLST As New HtlLibrary.HtlSqlTrans
    '    CityDS = HTLST.GetCityList(city)
    '    Dim ct = New City()
    '    Dim fetchCity = ct.GetHtlCityList(CityDS.Tables(0))
    '    Return fetchCity.ToList()
    'End Function
    'Hotel City Search
    <WebMethod()> _
      Public Function HtlCityList(ByVal city As String) As List(Of City)
        Dim CityDS As DataSet
        Dim HTLST As New HtlLibrary.HtlSqlTrans
        CityDS = HTLST.GetCityList(city)
        Dim ct = New City()
        Dim fetchCity = ct.GetHtlCityList(CityDS.Tables(0))
        Return fetchCity.ToList()
    End Function


    'City Search for Hotel Markup 
    <WebMethod()> _
    Public Function HtlMrkCityList(ByVal city As String, ByVal country As String) As List(Of City)
        Dim CityDS As DataSet
        Dim HTLST As New HtlLibrary.HtlSqlTrans
        CityDS = HTLST.GetMrkCityList(city, country)
        Dim ct = New City()
        Dim fetchCity = ct.GetHotelHarkupCitySearch(CityDS.Tables(0))
        Return fetchCity.ToList()
    End Function


    'Country Search for Hotel Markup 
    <WebMethod()> _
     Public Function HtlMrkCountryList(ByVal country As String, ByVal HtlType As String) As List(Of City)
        Dim CityDS As DataSet
        Dim HTLST As New HtlLibrary.HtlSqlTrans
        CityDS = HTLST.GetCountry(HtlType, country)
        Dim ct = New City()
        Dim fetchCity = ct.GetHotelHarkupCitySearch(CityDS.Tables(0))
        Return fetchCity.ToList()
    End Function
    <WebMethod()> _
    Public Function GetCountryCd(ByVal country As String) As List(Of City)
        Dim CountryDS As DataSet
        Dim objSqlTrans As New SqlTransactionNew
        CountryDS = objSqlTrans.GetCountryCode(country)
        Dim ct = New City()
        Dim fetchCountryCode = ct.GetCountryCode(CountryDS.Tables(0))
        Return fetchCountryCode.ToList()
    End Function
End Class
