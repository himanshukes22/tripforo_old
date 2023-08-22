Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class HotelSearchs
    Inherits System.Web.Services.WebService

    'Hotel City Search
    <WebMethod()> _
      Public Function HtlCityList(ByVal city As String) As List(Of HotelShared.CitySearch)
        Dim HTLST As New HotelDAL.HotelDA()
        Dim citylist As New List(Of HotelShared.CitySearch)
        Try
            citylist = HTLST.GetCityList(city)
        Catch ex As Exception

            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return citylist
    End Function
    'Hotel Name Search
    <WebMethod()> _
      Public Function HotelNameSearch(ByVal HotelName As String, ByVal city As String) As List(Of HotelShared.CitySearch)
        Dim citylist As New List(Of HotelShared.CitySearch)
        Try
            Dim HTLST As New HotelDAL.HotelDA()
            citylist = HTLST.GetCityList2(city, HotelName)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return citylist
    End Function

End Class
