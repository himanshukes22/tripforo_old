Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.IO
Imports System.Net
Imports System.Configuration
Public Class GoogleMapApi
    Private Shared _GoogleMapsKey As String '= "AIzaSyBfj23ZGs2rKZWPYlHEN0GxgaT7P-_jC2E"

    Public Shared Function ResolveAddress(ByVal query As String) As System.Nullable(Of Geolocation)
        If String.IsNullOrEmpty(_GoogleMapsKey) Then
            _GoogleMapsKey = ConfigurationManager.AppSettings("GoogleMapsKey")
        End If
        Dim url As String = "http://maps.googleapis.com/maps/api/geocode/xml?address=" & query & "&sensor=false"
        'Dim url As String = "http://maps.google.com/maps/geo?q={0}&output=xml&key=" & _GoogleMapsKey
        url = [String].Format(url, query)
        Dim coords As XmlNode = Nothing
        Try
            Dim xmlString As String = GetUrl(url)
            Dim xd As New XmlDocument()
            xd.LoadXml(xmlString)
            Dim xnm As New XmlNamespaceManager(xd.NameTable)
            'coords = xd.GetElementsByTagName("coordinates")(0)
            coords = xd.GetElementsByTagName("location")(0)
        Catch
        End Try

        Dim gl As System.Nullable(Of Geolocation) = Nothing
        If coords IsNot Nothing Then
            If coords.ChildNodes.Count = 2 Then
                gl = New Geolocation(Convert.ToDecimal(coords.ChildNodes(0).InnerText), Convert.ToDecimal(coords.ChildNodes(1).InnerText))
            End If
            'Dim coordinateArray As String() = coords.InnerText.Split(","c)
            'If coordinateArray.Length >= 2 Then
            '    gl = New Geolocation(Convert.ToDecimal(coordinateArray(1).ToString()), Convert.ToDecimal(coordinateArray(0).ToString()))
            'End If
        End If
        Return gl
    End Function
    Public Shared Function ResolveAddress(ByVal address As String, ByVal city As String, ByVal state As String, ByVal postcode As String, ByVal country As String) As System.Nullable(Of Geolocation)
        Return ResolveAddress(address & "," & city & "," & state & "," & postcode & " " & country)
    End Function
    Private Shared Function GetUrl(ByVal url As String) As String
        Dim result As String = String.Empty
        Dim Client As New WebClient()
        Using strm As Stream = Client.OpenRead(url)
            Dim sr As New StreamReader(strm)
            result = sr.ReadToEnd()
        End Using
        Return result
    End Function
End Class

Public Structure Geolocation


    Public Lat As Decimal


    Public Lon As Decimal


    Public Sub New(ByVal lat__1 As Decimal, ByVal lon__2 As Decimal)


        Lat = lat__1


        Lon = lon__2
    End Sub


    Public Overrides Function ToString() As String


        Return "Latitude: " & Lat.ToString() & " Longitude: " & Lon.ToString()

    End Function

    Public Function ToQueryString() As String


        Return "+to:" & Lat & "%2B" & Lon

    End Function


End Structure