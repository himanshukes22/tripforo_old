Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Text
Public Class clsSpiceJet
    Dim objDB As New clsDbSpiceJet
    Dim TrackId As String = GetTraceIDForSpiceJet()
    Dim sClientID As String = ""
    Dim sAPISource As String = ""
    Dim sDistNo As String = ""
    Dim sCarrierAccount As String = ""
    Dim sAirline As String = ""
    Dim sUrl As String = ""
    Dim Conn As New SqlConnection
    Public Sub New(ByVal ClientID As String, ByVal APIsource As String, ByVal Distributor As String, ByVal CarrierAccount As String, ByVal Airline As String, ByVal Url As String, ByVal strCon As String)
        sClientID = ClientID
        sAPISource = APIsource
        sDistNo = Distributor
        sCarrierAccount = CarrierAccount
        sAirline = Airline
        sUrl = Url
        Conn.ConnectionString = strCon
    End Sub

    Public Function getAvailabilty(ByVal Trip As String, ByVal TourType As String, ByVal sOrigin As String, ByVal sDestination As String, ByVal sDateOfDep As String, _
                                   ByVal sReturnDate As String, ByVal iTotalNo_Adt As Integer, _
                                   ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String) As DataTable
        Dim dtAvailbility As New DataTable
        Dim sResponse As String = ""
        Dim sSearch As String = ""
        Dim iFareNo As Integer = 1
        Dim dtFareQuote As New DataTable
        Dim dtTemp As New DataTable
        sSearch = GetSearchCriteria(sClientID, sAPISource, sDistNo, sOrigin, sDestination, sDateOfDep, sReturnDate, _
                                    iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, TourType, sCarrierAccount, iFareNo)
        sResponse = PostXml(sUrl, sSearch)
        dtTemp = GetAvailibityDT(sResponse, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, iFareNo, sOrigin, sDestination)
        If dtTemp.Rows.Count > 0 Then
            dtFareQuote = GetFareQuoteDT(dtTemp, sClientID, sCarrierAccount, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, iFareNo, Inf_Basic, Inf_Tax)
            dtAvailbility = GetAvailDT(dtTemp, dtFareQuote, sAirline, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, sOrigin, sDestination, Trip, sAgentId, sDistr, searchValue, TourType, tCnt, Ft, TrackId)
        End If
        Return dtAvailbility
    End Function

    Private Function GetSearchCriteria(ByVal sClientID As String, ByVal sAPISource As String, ByVal sDistNo As String, _
                                       ByVal sOrigin As String, ByVal sDestination As String, ByVal sDateOfDep As String, _
                                       ByVal sReturnDate As String, ByVal iTotalNo_Adt As Integer, _
                                       ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, _
                                       ByVal TourType As String, ByVal sCarrierAccount As String, ByVal iFareNo As Integer) As String

        Dim iTotalPass As Integer = iTotalNo_Adt + iTotalNo_Chd
        If TourType <> "Return" Then
            sReturnDate = ""
        End If
        Dim sXML As String = "<?xml version='1.0' encoding='UTF-8'?>"
        sXML += "<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ns1='urn:os:avl' xmlns:ns2='http://xml.apache.org/xml-soap' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>"
        sXML += "<SOAP-ENV:Header>"
        sXML += "<ClientId>" & sClientID & "</ClientId>"
        sXML += "<TraceId>" & TrackId & "</TraceId>"
        sXML += "</SOAP-ENV:Header>"
        sXML += "<SOAP-ENV:Body>"
        sXML += "<ns1:getAvailability SOAP-ENV:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>"
        sXML += "<input xsi:type='ns1:GetAvailabilityRequest'>"
        sXML += "<CarrierAccount xsi:type='xsd:string'>" & sCarrierAccount & "</CarrierAccount>"
        sXML += "<AvailabilityQueries xsi:type='ns2:Vector'>"
        sXML += "<item xsi:type='ns1:AvailabilityQuery'>"
        sXML += "<DepartureDate xsi:type='xsd:dateTime'>" & sDateOfDep & "T00:00:00Z</DepartureDate>"
        sXML += "<Origin xsi:type='xsd:string'>" & sOrigin & "</Origin>"
        sXML += "<Destination xsi:type='xsd:string'>" & sDestination & "</Destination>"
        sXML += "<NumberFares xsi:type='xsd:int'>" & iFareNo & "</NumberFares>"
        sXML += "<NumberPassengers xsi:type='xsd:int'>" & iTotalPass & "</NumberPassengers>"
        sXML += "<Options xsi:type='ns1:FareOption'>"
        sXML += "<CurrencyCode xsi:type='xsd:string'>INR</CurrencyCode>"
        sXML += "<BaseCurrency xsi:type='xsd:string'>INR</BaseCurrency>"
        sXML += "</Options>"
        sXML += "</item>"
        If sReturnDate <> "" Then
            sXML += "<item xsi:type='ns1:AvailabilityQuery'>"
            sXML += "<DepartureDate xsi:type='xsd:dateTime'>" & sReturnDate & "T00:00:00Z</DepartureDate>"
            sXML += "<Origin xsi:type='xsd:string'>" & sDestination & "</Origin>"
            sXML += "<Destination xsi:type='xsd:string'>" & sOrigin & "</Destination>"
            sXML += "<NumberFares xsi:type='xsd:int'>" & iFareNo & "</NumberFares>"
            sXML += "<NumberPassengers xsi:type='xsd:int'>" & iTotalPass & "</NumberPassengers>"
            sXML += "<Options xsi:type='ns1:FareOption'>"
            sXML += "<CurrencyCode xsi:type='xsd:string'>INR</CurrencyCode>"
            sXML += "<BaseCurrency xsi:type='xsd:string'>INR</BaseCurrency>"
            sXML += "</Options>"
            sXML += "</item>"
        End If
        sXML += "</AvailabilityQueries>"
        sXML += "</input>"
        sXML += "</ns1:getAvailability>"
        sXML += "</SOAP-ENV:Body>"
        sXML += "</SOAP-ENV:Envelope>"
        Return sXML
    End Function

    'Private Function GetAvailibityDT(ByVal sResponse As String, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Inf As Integer, ByVal iFareNo As Integer, ByVal Origin As String, ByVal Destination As String) As DataTable
    '    Dim dtTemp As New DataTable
    '    Dim row As DataRow
    '    Dim xmlDoc As New XmlDocument
    '    Dim sTransID As String = ""
    '    Dim sSessionId As String = ""
    '    Dim sDepDate As String = ""
    '    Dim sOrigin As String = ""
    '    Dim sDestination As String = ""
    '    Dim dBaseAmt As Double = 0.0
    '    Dim sPassTypeCode As String = ""
    '    Dim sFareGroup As String = ""
    '    Dim iSell As Integer = 0
    '    Dim sCabin As String = ""
    '    Dim sFBCode As String = ""
    '    Dim sArrivalDateTime As String = ""
    '    Dim iNumberOfStops As Integer = 0
    '    Dim sCarrierCode As String = ""
    '    Dim sFlightNumber As String = ""
    '    Dim sServiceType As String = ""
    '    Dim sAircraftType As String = ""
    '    Dim sAircraftCode As String = ""
    '    Dim sCapacity As String = ""
    '    Dim sArrivalAirport As String = ""
    '    Dim sDepartureAirport As String = ""
    '    Dim sFlightDesignator As String = ""
    '    Dim sScheduledDepartureTime As String = ""
    '    Dim sScheduledArrivalTime As String = ""
    '    Dim sLid As String = ""
    '    Dim sSold As String = ""
    '    Dim sArrivalDateTime1() As String, sDepDate1() As String, sAircraftType1() As String, sServiceType1() As String, sFlightNumber1() As String
    '    Dim sAircraftCode1() As String
    '    Dim sCapacity1() As String
    '    Dim sArrivalAirport1() As String
    '    Dim sDepartureAirport1() As String
    '    Dim sFlightDesignator1() As String
    '    Dim sScheduledDepartureTime1() As String
    '    Dim sScheduledArrivalTime1() As String
    '    Dim sLid1() As String
    '    Dim sSold1() As String
    '    Dim iCtr As Integer = 0
    '    Dim iCtr1 As Integer = 0
    '    Dim iCtr2 As Integer = 0
    '    Dim iCtr3 As Integer = 0
    '    Dim iCtr4 As Integer = 0
    '    Dim legcnt As Integer = 1, trtp As Integer = 0
    '    Dim legcnt1 As Integer = 1
    '    Dim triptp As String = "O"
    '    Dim fareNotAvailable As Boolean = False
    '    Try
    '        dtTemp = objDB.CreateDataTable()
    '        xmlDoc.LoadXml(sResponse)
    '        Dim availGrp As XmlNodeList = xmlDoc.GetElementsByTagName("AvailabilityGroup")
    '        For i As Integer = 0 To availGrp(0).ChildNodes.Count - 1
    '            For ii As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes.Count - 1
    '                If availGrp(0).ChildNodes(i).HasChildNodes Then
    '                    For od As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes.Count - 1
    '                        If availGrp(0).ChildNodes(i).ChildNodes(od).Name = "Origin" Then
    '                            sOrigin = availGrp(0).ChildNodes(i).ChildNodes(od).InnerText
    '                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(od).Name = "Destination" Then
    '                            sDestination = availGrp(0).ChildNodes(i).ChildNodes(od).InnerText
    '                        End If
    '                    Next
    '                End If
    '                If availGrp(0).ChildNodes(i).ChildNodes(ii).Name = "Components" Then
    '                    For flt As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes.Count - 1
    '                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).Name = "item" Then
    '                            For flt1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes.Count - 1
    '                                Try
    '                                    If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).Name = "Fares" Then
    '                                        ' If fare not available
    '                                        fareNotAvailable = False
    '                                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).HasChildNodes = False Then
    '                                            fareNotAvailable = True
    '                                        End If
    '                                        ' If fare not available end
    '                                        For f1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes.Count - 1
    '                                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).Name = "item" Then
    '                                                For f2 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes.Count - 1
    '                                                    If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "BaseAmount" Then
    '                                                        For bamt As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes.Count - 1
    '                                                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes(bamt).Name = "Amount" Then
    '                                                                dBaseAmt = Math.Round(Double.Parse(availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes(bamt).InnerText), 0)
    '                                                            End If
    '                                                        Next
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "PassengerTypeCode" Then
    '                                                        sPassTypeCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "FareGroup" Then
    '                                                        sFareGroup = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "ToSell" Then
    '                                                        iSell = Integer.Parse(availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim)
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "Cabin" Then
    '                                                        sCabin = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "FareBasisCode" Then
    '                                                        sFBCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Next
    '                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).Name = "Flights" Then

    '                                        For fltLeg As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes.Count - 1
    '                                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).Name = "item" Then
    '                                                For f1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes.Count - 1
    '                                                    If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "ArrivalDateTime" Then
    '                                                        sArrivalDateTime = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "NumberOfStops" Then
    '                                                        iNumberOfStops = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "DepartureDateTime" Then
    '                                                        sDepDate = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "AircraftType" Then
    '                                                        sAircraftType = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "ServiceType" Then
    '                                                        sServiceType = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "FlightLegs" Then
    '                                                        For f2 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes.Count - 1
    '                                                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).Name = "item" Then
    '                                                                For f3 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes.Count - 1
    '                                                                    If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "FlightDesignator" Then
    '                                                                        sFlightDesignator = sFlightDesignator & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ScheduledDepartureTime" Then
    '                                                                        sScheduledDepartureTime = sScheduledDepartureTime & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ScheduledArrivalTime" Then
    '                                                                        sScheduledArrivalTime = sScheduledArrivalTime & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Lid" Then
    '                                                                        sLid = sLid & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Sold" Then
    '                                                                        sSold = sSold & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "DepartureAirport" Then
    '                                                                        sDepartureAirport = sDepartureAirport & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "DepartureTerminal" Then
    '                                                                        'depterminal
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ArrivalTerminal" Then
    '                                                                        'arrivalterminal
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "AircraftCode" Then
    '                                                                        sAircraftCode = sAircraftCode & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Capacity" Then
    '                                                                        sCapacity = sCapacity & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ArrivalAirport" Then
    '                                                                        sArrivalAirport = sArrivalAirport & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
    '                                                                    End If
    '                                                                Next
    '                                                            End If
    '                                                        Next
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "FlightNumber" Then
    '                                                        sFlightNumber = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                    ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "CarrierCode" Then
    '                                                        sCarrierCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
    '                                                        'Insert into Datatable 
    '                                                        Try
    '                                                            sFlightDesignator1 = Split(sFlightDesignator, ",")
    '                                                            sScheduledDepartureTime1 = Split(sScheduledDepartureTime, ",")
    '                                                            sScheduledArrivalTime1 = Split(sScheduledArrivalTime, ",")
    '                                                            sLid1 = Split(sLid, ",")
    '                                                            sSold1 = Split(sSold, ",")
    '                                                            sDepartureAirport1 = Split(sDepartureAirport, ",")
    '                                                            sAircraftCode1 = Split(sAircraftCode, ",")
    '                                                            sCapacity1 = Split(sCapacity, ",")
    '                                                            sArrivalAirport1 = Split(sArrivalAirport, ",")
    '                                                            If sOrigin.Trim.ToUpper = Destination.Trim.ToUpper And sDestination.Trim.ToUpper = Origin.Trim.ToUpper Then
    '                                                                If trtp <> 1 Then
    '                                                                    legcnt = 1
    '                                                                    triptp = "R"
    '                                                                End If
    '                                                                trtp = 1
    '                                                            End If
    '                                                            If fareNotAvailable <> True Then
    '                                                                For iFlt As Integer = 1 To UBound(sFlightDesignator1)
    '                                                                    row = dtTemp.NewRow()
    '                                                                    row("Origin") = sOrigin
    '                                                                    row("Destination") = sDestination
    '                                                                    row("TransID") = legcnt1.ToString
    '                                                                    row("TrackId") = TrackId
    '                                                                    row("DepDate") = sDepDate
    '                                                                    row("BaseAmt") = dBaseAmt
    '                                                                    row("PassTypeCode") = sPassTypeCode
    '                                                                    row("FareGroup") = sFareGroup
    '                                                                    row("ToSell") = iSell
    '                                                                    row("Cabin") = sCabin
    '                                                                    row("FBCode") = sFBCode
    '                                                                    row("ArrivalDateTime") = sArrivalDateTime
    '                                                                    row("Stops") = iNumberOfStops
    '                                                                    row("AircraftType") = sAircraftType
    '                                                                    row("CarrierCode") = sCarrierCode
    '                                                                    row("FlightNumber") = sFlightNumber
    '                                                                    row("AircraftCode") = sAircraftCode1(iFlt)
    '                                                                    row("ServiceType") = sServiceType
    '                                                                    row("Capacity") = sCapacity1(iFlt)
    '                                                                    row("ArrivalAirport") = sArrivalAirport1(iFlt)
    '                                                                    row("DepartureAirport") = sDepartureAirport1(iFlt)
    '                                                                    row("FlightDesignator") = sFlightDesignator1(iFlt)
    '                                                                    row("ScheduledDepartureTime") = sScheduledDepartureTime1(iFlt)
    '                                                                    row("ScheduledArrivalTime") = sScheduledArrivalTime1(iFlt)
    '                                                                    row("Lid") = sLid1(iFlt)
    '                                                                    row("Sold") = sSold1(iFlt)
    '                                                                    row("LegCnt") = legcnt.ToString
    '                                                                    row("TripType") = triptp
    '                                                                    row("Leg") = iFlt.ToString
    '                                                                    dtTemp.Rows.Add(row)
    '                                                                Next
    '                                                            End If
    '                                                            sFlightNumber = ""
    '                                                            sFlightDesignator = ""
    '                                                            sScheduledDepartureTime = ""
    '                                                            sScheduledArrivalTime = ""
    '                                                            sLid = ""
    '                                                            sSold = ""
    '                                                            sDepartureAirport = ""
    '                                                            sAircraftCode = ""
    '                                                            sCapacity = ""
    '                                                            sArrivalAirport = ""
    '                                                        Catch ex As Exception
    '                                                        End Try
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Next
    '                                        dBaseAmt = 0
    '                                        sPassTypeCode = ""
    '                                        sFareGroup = ""
    '                                        iSell = 0
    '                                        sCabin = ""
    '                                        sFBCode = ""
    '                                        If fareNotAvailable <> True Then
    '                                            legcnt = legcnt + 1
    '                                            legcnt1 = legcnt1 + 1
    '                                        End If
    '                                    End If
    '                                Catch ex As Exception
    '                                End Try
    '                            Next
    '                        End If
    '                    Next
    '                End If
    '            Next
    '        Next
    '    Catch ex As Exception
    '    End Try
    '    Return dtTemp
    'End Function
    Private Function GetAvailibityDT(ByVal sResponse As String, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Inf As Integer, ByVal iFareNo As Integer, ByVal Origin As String, ByVal Destination As String) As DataTable
        Dim dtTemp As New DataTable
        Dim row As DataRow
        Dim xmlDoc As New XmlDocument
        Dim sTransID As String = ""
        Dim sSessionId As String = ""
        Dim sDepDate As String = ""
        Dim sOrigin As String = ""
        Dim sDestination As String = ""
        Dim sOrigin1() As String
        Dim sDestination1() As String
        Dim dBaseAmt As Double = 0.0
        Dim sPassTypeCode As String = ""
        Dim sFareGroup As String = ""
        Dim iSell As Integer = 0
        Dim sCabin As String = ""
        Dim sFBCode As String = ""
        Dim sArrivalDateTime As String = ""
        Dim iNumberOfStops As Integer = 0
        Dim sCarrierCode As String = ""
        Dim sFlightNumber As String = ""
        Dim sServiceType As String = ""
        Dim sAircraftType As String = ""
        Dim sAircraftCode As String = ""
        Dim sCapacity As String = ""
        Dim sArrivalAirport As String = ""
        Dim sDepartureAirport As String = ""
        Dim sFlightDesignator As String = ""
        Dim sScheduledDepartureTime As String = ""
        Dim sScheduledArrivalTime As String = ""
        Dim sLid As String = ""
        Dim sSold As String = ""
        Dim sArrivalDateTime1() As String, sDepDate1() As String, sAircraftType1() As String, sServiceType1() As String, sFlightNumber1() As String
        Dim sAircraftCode1() As String
        Dim sCapacity1() As String
        Dim sArrivalAirport1() As String
        Dim sDepartureAirport1() As String
        Dim sFlightDesignator1() As String
        Dim sScheduledDepartureTime1() As String
        Dim sScheduledArrivalTime1() As String
        Dim sLid1() As String
        Dim sSold1() As String
        Dim iCtr As Integer = 0
        Dim iCtr1 As Integer = 0
        Dim iCtr2 As Integer = 0
        Dim iCtr3 As Integer = 0
        Dim iCtr4 As Integer = 0
        Dim legcnt As Integer = 1, trtp As Integer = 0
        Dim legcnt1 As Integer = 1
        Dim triptp As String = "O"
        Dim fareNotAvailable As Boolean = False
        Try
            dtTemp = objDB.CreateDataTable()
            xmlDoc.LoadXml(sResponse)
            Dim availGrp As XmlNodeList = xmlDoc.GetElementsByTagName("AvailabilityGroup")
            For i As Integer = 0 To availGrp(0).ChildNodes.Count - 1
                For ii As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes.Count - 1
                    If availGrp(0).ChildNodes(i).HasChildNodes Then
                        For od As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes.Count - 1
                            If availGrp(0).ChildNodes(i).ChildNodes(od).Name = "Origin" Then
                                sOrigin = availGrp(0).ChildNodes(i).ChildNodes(od).InnerText
                            ElseIf availGrp(0).ChildNodes(i).ChildNodes(od).Name = "Destination" Then
                                sDestination = availGrp(0).ChildNodes(i).ChildNodes(od).InnerText
                            End If
                        Next
                    End If
                    If availGrp(0).ChildNodes(i).ChildNodes(ii).Name = "Components" Then
                        For flt As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes.Count - 1
                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).Name = "item" Then
                                For flt1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes.Count - 1
                                    Try
                                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).Name = "Fares" Then
                                            ' If fare not available
                                            fareNotAvailable = False
                                            If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).HasChildNodes = False Then
                                                fareNotAvailable = True
                                            End If
                                            ' If fare not available end
                                            For f1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes.Count - 1
                                                If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).Name = "item" Then
                                                    For f2 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes.Count - 1
                                                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "BaseAmount" Then
                                                            For bamt As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes.Count - 1
                                                                If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes(bamt).Name = "Amount" Then
                                                                    dBaseAmt = Math.Round(Double.Parse(availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).ChildNodes(bamt).InnerText), 0)
                                                                End If
                                                            Next
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "PassengerTypeCode" Then
                                                            sPassTypeCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "FareGroup" Then
                                                            sFareGroup = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "ToSell" Then
                                                            iSell = Integer.Parse(availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim)
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "Cabin" Then
                                                            sCabin = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).Name = "FareBasisCode" Then
                                                            sFBCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(f1).ChildNodes(f2).InnerText.Trim
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).Name = "Flights" Then

                                            For fltLeg As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes.Count - 1
                                                If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).Name = "item" Then
                                                    For f1 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes.Count - 1
                                                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "ArrivalDateTime" Then
                                                            sArrivalDateTime = sArrivalDateTime & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "NumberOfStops" Then
                                                            iNumberOfStops += availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "DepartureDateTime" Then
                                                            sDepDate = sDepDate & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "AircraftType" Then
                                                            sAircraftType = sAircraftType & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "ServiceType" Then
                                                            sServiceType = sServiceType & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "FlightLegs" Then
                                                            For f2 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes.Count - 1
                                                                If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).Name = "item" Then
                                                                    For f3 As Integer = 0 To availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes.Count - 1
                                                                        If availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "FlightDesignator" Then
                                                                            sFlightDesignator = sFlightDesignator & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ScheduledDepartureTime" Then
                                                                            sScheduledDepartureTime = sScheduledDepartureTime & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ScheduledArrivalTime" Then
                                                                            sScheduledArrivalTime = sScheduledArrivalTime & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Lid" Then
                                                                            sLid = sLid & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Sold" Then
                                                                            sSold = sSold & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "DepartureAirport" Then
                                                                            sDepartureAirport = sDepartureAirport & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "DepartureTerminal" Then
                                                                            'depterminal
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ArrivalTerminal" Then
                                                                            'arrivalterminal
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "AircraftCode" Then
                                                                            sAircraftCode = sAircraftCode & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "Capacity" Then
                                                                            sCapacity = sCapacity & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).Name = "ArrivalAirport" Then
                                                                            sArrivalAirport = sArrivalAirport & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).ChildNodes(f2).ChildNodes(f3).InnerText.Trim
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "FlightNumber" Then
                                                            sFlightNumber = sFlightNumber & "," & availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                        ElseIf availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).Name = "CarrierCode" Then
                                                            sCarrierCode = availGrp(0).ChildNodes(i).ChildNodes(ii).ChildNodes(flt).ChildNodes(flt1).ChildNodes(fltLeg).ChildNodes(f1).InnerText.Trim
                                                            'Insert into Datatable 

                                                        End If
                                                    Next
                                                End If
                                            Next
                                            Try
                                                'sOrigin1 = Split(sOrigin, ",")
                                                'sDestination1 = Split(sDestination, ",")
                                                sFlightNumber1 = Split(sFlightNumber, ",")
                                                sArrivalDateTime1 = Split(sArrivalDateTime, ",")
                                                sDepDate1 = Split(sDepDate, ",")
                                                sAircraftType1 = Split(sAircraftType, ",")
                                                sServiceType1 = Split(sServiceType, ",")
                                                sFlightDesignator1 = Split(sFlightDesignator, ",")
                                                sScheduledDepartureTime1 = Split(sScheduledDepartureTime, ",")
                                                sScheduledArrivalTime1 = Split(sScheduledArrivalTime, ",")
                                                sLid1 = Split(sLid, ",")
                                                sSold1 = Split(sSold, ",")
                                                sDepartureAirport1 = Split(sDepartureAirport, ",")
                                                sAircraftCode1 = Split(sAircraftCode, ",")
                                                sCapacity1 = Split(sCapacity, ",")
                                                sArrivalAirport1 = Split(sArrivalAirport, ",")
                                                If sOrigin.Trim.ToUpper = Destination.Trim.ToUpper And sDestination.Trim.ToUpper = Origin.Trim.ToUpper Then
                                                    If trtp <> 1 Then
                                                        legcnt = 1
                                                        triptp = "R"
                                                    End If
                                                    trtp = 1
                                                End If
                                                If fareNotAvailable <> True Then
                                                    For iFlt As Integer = 1 To UBound(sFlightDesignator1)
                                                        row = dtTemp.NewRow()
                                                        row("Origin") = sOrigin
                                                        row("Destination") = sDestination
                                                        row("TransID") = legcnt1.ToString
                                                        row("TrackId") = TrackId
                                                        row("DepDate") = sDepDate1(1)
                                                        row("BaseAmt") = dBaseAmt
                                                        row("PassTypeCode") = sPassTypeCode
                                                        row("FareGroup") = sFareGroup
                                                        row("ToSell") = iSell
                                                        row("Cabin") = sCabin
                                                        row("FBCode") = sFBCode
                                                        row("ArrivalDateTime") = sArrivalDateTime1(sArrivalDateTime1.Length - 1)
                                                        row("Stops") = sFlightDesignator1.Length - 2
                                                        row("AircraftType") = sAircraftType1(1)
                                                        row("CarrierCode") = sCarrierCode
                                                        row("FlightNumber") = Mid(sFlightDesignator1(iFlt), 3, sFlightDesignator1(iFlt).Length - 1).ToString.Trim 'sFlightNumber1(1)
                                                        row("AircraftCode") = sAircraftCode1(iFlt)
                                                        row("ServiceType") = sServiceType1(1)
                                                        row("Capacity") = sCapacity1(iFlt)
                                                        row("ArrivalAirport") = sArrivalAirport1(iFlt)
                                                        row("DepartureAirport") = sDepartureAirport1(iFlt)
                                                        row("FlightDesignator") = sFlightDesignator1(iFlt)
                                                        row("ScheduledDepartureTime") = sScheduledDepartureTime1(iFlt)
                                                        row("ScheduledArrivalTime") = sScheduledArrivalTime1(iFlt)
                                                        row("Lid") = sLid1(iFlt)
                                                        row("Sold") = sSold1(iFlt)
                                                        row("LegCnt") = legcnt.ToString
                                                        row("TripType") = triptp
                                                        row("Leg") = iFlt.ToString
                                                        dtTemp.Rows.Add(row)
                                                    Next
                                                End If
                                                sFlightNumber = ""
                                                sFlightDesignator = ""
                                                sScheduledDepartureTime = ""
                                                sScheduledArrivalTime = ""
                                                sLid = ""
                                                sSold = ""
                                                sDepartureAirport = ""
                                                sAircraftCode = ""
                                                sCapacity = ""
                                                sArrivalAirport = ""
                                                sDepDate = ""
                                                sArrivalDateTime = ""
                                                sAircraftType = ""
                                                sServiceType = ""
                                            Catch ex As Exception
                                            End Try
                                            dBaseAmt = 0
                                            sPassTypeCode = ""
                                            sFareGroup = ""
                                            iSell = 0
                                            sCabin = ""
                                            sFBCode = ""
                                            If fareNotAvailable <> True Then
                                                legcnt = legcnt + 1
                                                legcnt1 = legcnt1 + 1
                                            End If
                                        End If
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If
                        Next
                    End If
                Next
            Next
        Catch ex As Exception
        End Try
        Return dtTemp
    End Function

    Private Function GetFareQuoteDT(ByVal dtTable As DataTable, ByVal sClientID As String, ByVal sCarrierAccount As String, ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, ByVal iFareNo As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double) As DataTable
        Dim dtTemp As New DataTable
        Dim dtTemp2 As New DataTable
        Dim iCnt As Integer = 0
        Dim row As DataRow
        Dim sRequest As String
        Dim sResponse As String
        Dim sPassTypeCode As String = ""
        Dim dTaxAmount_Adt As Double = 0.0
        Dim dTaxAmount_Chd As Double = 0.0
        Dim dTaxAmount_Inf As Double = 0.0
        Dim xmlDoc As New XmlDocument
        Dim iCtr As Integer = 0
        Dim iCounter As Integer = 0
        Dim iData As Integer = 0
        dtTemp = objDB.CreateFQTable()
        Try
            Do While iData <= dtTable.Rows.Count - 1
                If Integer.Parse(dtTable.Rows(dtTable.Rows.Count - 1)("TransID")) <= iData Then
                    Exit Do
                Else
                    dtTemp2 = GetTop8(dtTable, iData, iData + 7)
                End If

                sRequest = GetFareQuoteReq(dtTemp2, sClientID, sCarrierAccount, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, iFareNo)
                sResponse = PostXml(sUrl, sRequest)
                xmlDoc.LoadXml(sResponse)
                For i As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.Count - 1
                    row = dtTemp.NewRow()
                    If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(0).LocalName = "SelectedFareBasisCode" Then
                        row("FBCode") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(0).InnerText
                    End If
                    If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).LocalName = "Flights" Then
                        For i1 As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                            If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "ArrivalDateTime" Then
                                row("ArrivalDateTime") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "DepartureDateTime" Then
                                row("DepDate") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "Origin" Then
                                row("Origin") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "Destination" Then
                                row("Destination") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "FlightNumber" Then
                                row("FlightNumber") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).LocalName = "CarrierCode" Then
                                row("CarrierCode") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(2).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i1).InnerText
                            End If
                        Next
                    End If
                    If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).LocalName = "Fares" Then
                        For i1 As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.Count - 1
                            sPassTypeCode = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(2).InnerText
                            row("PassTypeCode") = sPassTypeCode
                            iCtr = 0
                            Do While iCtr <= xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.Count - 1
                                If sPassTypeCode = "ADT" Then
                                    If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "TotalAmount" Then
                                        row("TotalAmt_Adt") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                    ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "BaseAmount" Then
                                        row("BaseAmt_Adt") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                    ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "Taxes" Then
                                        For i3 As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.Count - 1
                                            dTaxAmount_Adt = dTaxAmount_Adt + xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(i3).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                        Next
                                        row("TaxAmt_Adt") = dTaxAmount_Adt
                                        row("AdtFSur") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(1).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                    End If
                                End If
                                If iTotalNo_Chd > 0 Then
                                    If sPassTypeCode = "CHD" Then
                                        If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "TotalAmount" Then
                                            row("TotalAmt_Chd") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                        ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "BaseAmount" Then
                                            row("BaseAmt_Chd") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                        ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "Taxes" Then
                                            For i3 As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.Count - 1
                                                dTaxAmount_Chd = dTaxAmount_Chd + xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(i3).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                            Next
                                            row("TaxAmt_Chd") = dTaxAmount_Chd
                                            row("ChdFSur") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(1).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                        End If
                                    End If
                                Else
                                    row("TotalAmt_Chd") = 0
                                    row("BaseAmt_Chd") = 0
                                    row("TaxAmt_Chd") = 0
                                    row("ChdFSur") = 0
                                End If
                                If iTotalNo_Inf > 0 Then
                                    row("BaseAmt_Inf") = Inf_Basic
                                    row("TaxAmt_Inf") = Inf_Tax
                                    row("TotalAmt_Inf") = Inf_Basic + Inf_Tax
                                    row("InfFSur") = 0
                                    'If sPassTypeCode = "INF" Then
                                    '    If xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "TotalAmount" Then
                                    '        row("TotalAmt_Inf") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                    '    ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "BaseAmount" Then
                                    '        row("BaseAmt_Inf") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).InnerText
                                    '    ElseIf xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).LocalName = "Taxes" Then
                                    '        For i3 As Integer = 0 To xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.Count - 1
                                    '            dTaxAmount_Inf = dTaxAmount_Inf + xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(i3).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                    '        Next
                                    '        row("TaxAmt_Inf") = dTaxAmount_Inf
                                    '        row("InfFSur") = xmlDoc.GetElementsByTagName("AirComponents").ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(1).ChildNodes.ItemOf(i1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(1).ChildNodes.ItemOf(1).ChildNodes.ItemOf(0).InnerText
                                    '    End If
                                    'End If
                                Else
                                    row("TotalAmt_Inf") = 0
                                    row("BaseAmt_Inf") = 0
                                    row("TaxAmt_Inf") = 0
                                    row("InfFSur") = 0
                                End If
                                iCtr = iCtr + 1
                            Loop
                        Next
                    End If
                    dtTemp.Rows.Add(row)
                    dTaxAmount_Adt = 0
                    dTaxAmount_Chd = 0
                    dTaxAmount_Inf = 0
                Next
                iData = iData + 8
            Loop
        Catch ex As Exception

        End Try
        Return dtTemp
    End Function

    Private Function GetAvailDT(ByVal dtAvail As DataTable, ByVal dtFareQuote As DataTable, ByVal sAirline As String, ByVal Adult As Integer, _
                                ByVal Child As Integer, ByVal Infant As Integer, ByVal sOrigin As String, ByVal sDestinatioin As String, ByVal Trip As String, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal TourType As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String) As DataTable
        Dim dtTemp As New DataTable
        Dim dtDataView As New DataView
        Dim dtTempNew As New DataTable
        Dim sDepDate() As String
        Dim sArrDate() As String
        Dim Fltcnt As Integer = 0
        Dim Flt As String = ""
        Dim ileg As Integer = 1
        Dim lin As Integer = 0
        Dim row As DataRow
        Conn.Open()

        Try
            dtTemp = objDB.ResultTable()
            'Calculation For AgentMarkUp'
            Dim dtAgentMarkup As New DataTable
            dtAgentMarkup = GetMarkUp(sAgentId, sDistr, Trip, "TA")
            'Calculation For AdminMarkUp'
            Dim dtAdminMarkup As New DataTable
            dtAdminMarkup = GetMarkUp(sAgentId, sDistr, Trip, "AD")

            For i As Integer = 0 To dtAvail.Rows.Count - 1
                row = dtTemp.NewRow()
                Dim tblSource As DataTable = CType(dtFareQuote, DataTable)
                'If i <> 0 Then
                '    If dtAvail.Rows(i)("LegCnt") = dtAvail.Rows(i - 1)("LegCnt") And dtAvail.Rows(i)("FlightNumber") <> dtAvail.Rows(i - 1)("FlightNumber") Then
                '        tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i - 1)("FlightNumber") & "'"
                '    Else
                '        tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i)("FlightNumber") & "'"
                '    End If
                'Else
                '    tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i)("FlightNumber") & "'"
                'End If
                If i <> 0 Then
                    If dtAvail.Rows(i)("LegCnt") = dtAvail.Rows(i - 1)("LegCnt") And dtAvail.Rows(i)("FlightNumber") <> dtAvail.Rows(i - 1)("FlightNumber") And dtAvail.Rows(i)("TripType") = dtAvail.Rows(i - 1)("TripType") Then
                        tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i - 1)("FlightNumber") & "'"
                    Else
                        If dtAvail.Rows(i)("Leg") = "3" Then
                            tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i - 2)("FlightNumber") & "'"
                        Else
                            tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i)("FlightNumber") & "'"
                        End If

                    End If
                Else
                    tblSource.DefaultView.RowFilter = "FlightNumber='" & dtAvail.Rows(i)("FlightNumber") & "'"
                End If
                dtDataView = tblSource.DefaultView
                dtTempNew = dtDataView.ToTable
                If dtTempNew.Rows(0)("PassTypeCode") <> "" Then
                    lin = lin + Val(dtAvail.Rows(i)("LegCnt").ToString)
                    If dtAvail.Rows(i)("Origin").ToString = sOrigin Then
                        row("TripType") = "O"
                        row("OrgDestFrom") = sOrigin
                        row("OrgDestTo") = sDestinatioin
                        row("Sector") = sOrigin & ":" & sDestinatioin
                        Fltcnt = 1
                    ElseIf dtAvail.Rows(i)("Origin").ToString = sDestinatioin Then
                        row("TripType") = "R"
                        row("OrgDestFrom") = sDestinatioin
                        row("OrgDestTo") = sOrigin
                        row("Sector") = sDestinatioin & ":" & sOrigin
                        Fltcnt = 2
                    End If
                    '// New entry
                    row("DepartureLocation") = dtAvail.Rows(i)("DepartureAirport").ToString
                    row("ArrivalLocation") = dtAvail.Rows(i)("ArrivalAirport").ToString
                    row("DepartureCityName") = city_name(dtAvail.Rows(i)("DepartureAirport").ToString)
                    row("ArrivalCityName") = city_name(dtAvail.Rows(i)("ArrivalAirport").ToString)
                    'sDepDate = Split(dtAvail.Rows(i)("DepDate").ToString, "T")
                    'sArrDate = Split(dtAvail.Rows(i)("ArrivalDateTime").ToString, "T")
                    sDepDate = Split(dtAvail.Rows(i)("ScheduledDepartureTime").ToString, "T")
                    sArrDate = Split(dtAvail.Rows(i)("ScheduledArrivalTime").ToString, "T")
                    row("depdatelcc") = sDepDate(0)
                    row("arrdatelcc") = sArrDate(0)
                    row("DepartureDate") = Right(sDepDate(0), 2) & Mid(sDepDate(0), 6, 2) & Mid(sDepDate(0), 3, 2)
                    row("Departure_Date") = Right(sDepDate(0), 2) & " " & datecon(Mid(sDepDate(0), 6, 2))
                    row("DepartureTime") = sDepDate(1) 'Left(dtAvail.Rows(i)("ScheduledDepartureTime").ToString, 5).Replace(":", "")
                    row("ArrivalDate") = Right(sArrDate(0), 2) & Mid(sArrDate(0), 6, 2) & Mid(sArrDate(0), 3, 2)
                    row("Arrival_Date") = Right(sArrDate(0), 2) & " " & datecon(Mid(sArrDate(0), 6, 2))
                    row("ArrivalTime") = sArrDate(1) 'Left(dtAvail.Rows(i)("ScheduledArrivalTime").ToString, 5).Replace(":", "")
                    row("MarketingCarrier") = dtAvail.Rows(i)("CarrierCode").ToString
                    row("AirLineName") = sAirline
                    row("FlightIdentification") = dtAvail.Rows(i)("FlightNumber").ToString
                    row("RBD") = dtAvail.Rows(i)("ServiceType").ToString
                    row("AvailableSeats") = dtAvail.Rows(i)("ToSell").ToString
                    row("ValiDatingCarrier") = dtAvail.Rows(i)("CarrierCode").ToString.Trim
                    row("EQ") = dtAvail.Rows(i)("AircraftType").ToString

                    row("Stops") = dtAvail.Rows(i)("Stops").ToString & "-Stop"
                    row("fareBasis") = dtAvail.Rows(i)("FBCode").ToString
                    row("FBPaxType") = "ADT"
                    row("LineItemNumber") = dtAvail.Rows(i)("LegCnt").ToString
                    row("Searchvalue") = searchValue.ToString
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    If i > 0 Then
                        If Val(dtAvail.Rows(i - 1)("LegCnt")) = Val(dtAvail.Rows(i)("LegCnt")) Then
                            ileg = ileg + 1
                        Else
                            ileg = 1
                        End If
                    End If
                    row("Leg") = ileg 'dtAvail.Rows(i)("Leg").ToString
                    row("Flight") = Fltcnt
                    row("Tot_Dur") = ""
                    row("Trip") = TourType.ToString

                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = "Rs."
                    row("sno") = dtAvail.Rows(i)("TrackId").ToString
                    '//


                    'row("AircraftCode") = dtAvail.Rows(i)("AircraftCode").ToString
                    'row("FareGroup") = dtAvail.Rows(i)("FareGroup").ToString
                    'row("AircraftType") = dtAvail.Rows(i)("AircraftType").ToString
                    'row("Capacity") = dtAvail.Rows(i)("Capacity").ToString
                    'row("Lid") = dtAvail.Rows(i)("Lid").ToString
                    'row("Sold") = dtAvail.Rows(i)("Sold").ToString
                    If dtTempNew.Rows.Count > 0 Then
                        'Calculation of Transaction details
                        Dim totBFWInf As Double = 0
                        Dim totBFWOInf As Double = 0
                        Dim totFS As Double = 0
                        Dim totTax As Double = 0
                        Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                        Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0
                        Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0

                        totBFWInf = (dtTempNew.Rows(0)("BaseAmt_Adt") * Adult) + (dtTempNew.Rows(0)("BaseAmt_Chd") * Child) + (dtTempNew.Rows(0)("BaseAmt_Inf") * Infant)
                        totBFWOInf = (dtTempNew.Rows(0)("BaseAmt_Adt") * Adult) + (dtTempNew.Rows(0)("BaseAmt_Chd") * Child)
                        totFS = (dtTempNew.Rows(0)("AdtFSur") * Adult) + (dtTempNew.Rows(0)("ChdFSur") * Child)
                        totTax = (dtTempNew.Rows(0)("TaxAmt_Adt") * Adult) + (dtTempNew.Rows(0)("TaxAmt_Chd") * Child) + (dtTempNew.Rows(0)("TaxAmt_Inf") * Infant)
                        'Dim HsTblSTax As Hashtable = ServiceTax(dtAvail.Rows(i)("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                        Dim HsTblSTax As Hashtable = ServiceTax(dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("BaseAmt_Adt"), dtTempNew.Rows(0)("BaseAmt_Adt"), dtTempNew.Rows(0)("AdtFSur"), Trip)
                        AStax = HsTblSTax("STax") * Adult
                        ATF = HsTblSTax("TF") * Adult
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("BaseAmt_Chd"), dtTempNew.Rows(0)("BaseAmt_Chd"), dtTempNew.Rows(0)("ChdFSur"), Trip)
                        CStax = HsTblSTax("STax") * Child
                        CTF = HsTblSTax("TF") * Child
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("BaseAmt_Inf"), 0, 0, Trip)
                        IStax = HsTblSTax("STax") * Infant
                        ITF = HsTblSTax("TF") * Infant


                        ADTAgentMrk = CalcMarkup(dtAgentMarkup, dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("TotalAmt_Adt"), Trip)
                        If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("TotalAmt_Chd"), Trip) Else CHDAgentMrk = 0
                        ADTAdminMrk = CalcMarkup(dtAdminMarkup, dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("TotalAmt_Adt"), Trip)
                        If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, dtAvail.Rows(i)("CarrierCode").ToString.Trim, dtTempNew.Rows(0)("TotalAmt_Chd"), Trip) Else CHDAgentMrk = 0
                        Dim totMrk As Double = 0
                        totMrk = ADTAdminMrk * Adult
                        totMrk = totMrk + ADTAgentMrk * Adult
                        totMrk = totMrk + CHDAdminMrk * Child
                        totMrk = totMrk + CHDAgentMrk * Child
                        'Calculation of Transaction details end
                        '// New entry
                        row("AdtBfare") = dtTempNew.Rows(0)("BaseAmt_Adt")
                        row("AdtFSur") = dtTempNew.Rows(0)("AdtFSur")
                        row("AdtTax") = dtTempNew.Rows(0)("TaxAmt_Adt")
                        row("AdtFare") = dtTempNew.Rows(0)("TotalAmt_Adt")
                        row("ChdBfare") = dtTempNew.Rows(0)("BaseAmt_Chd")
                        row("ChdFSur") = dtTempNew.Rows(0)("ChdFSur")
                        row("ChdTax") = dtTempNew.Rows(0)("TaxAmt_Chd")
                        row("ChdFare") = dtTempNew.Rows(0)("TotalAmt_Chd")
                        row("InfBfare") = dtTempNew.Rows(0)("BaseAmt_Inf")
                        row("InfFSur") = dtTempNew.Rows(0)("InfFSur")
                        row("InfTax") = dtTempNew.Rows(0)("TaxAmt_Inf")
                        row("InfFare") = dtTempNew.Rows(0)("TotalAmt_Inf")
                        row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                        row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                        row("IATAComm") = HsTblSTax("IATAComm")
                        row("ADTAgentMrk") = ADTAgentMrk
                        row("CHDAgentMrk") = CHDAgentMrk
                        row("ADTAdminMrk") = ADTAdminMrk
                        row("CHDAdminMrk") = CHDAdminMrk
                        row("TotalBfare") = totBFWInf
                        row("TotalFuelSur") = (dtTempNew.Rows(0)("AdtFSur") * Adult) + (dtTempNew.Rows(0)("ChdFSur") * Child) + (dtTempNew.Rows(0)("InfFSur") * Infant)
                        row("TotalTax") = totTax
                        row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                        row("DisCount") = "0"
                        row("OriginalTF") = totBFWOInf + (totTax - (dtTempNew.Rows(0)("TaxAmt_Inf") * Infant))
                        row("OriginalTT") = (totTax - (dtTempNew.Rows(0)("TaxAmt_Inf") * Infant))
                        row("Track_id") = TrackId
                        row("FType") = Ft
                        '//

                    End If

                    dtTemp.Rows.Add(row)
                Else
                    'lin = lin - 1
                End If
            Next
        Catch ex As Exception

        End Try
        Conn.Close()
        Return dtTemp
    End Function

    Private Function GetFareQuoteReq(ByVal dtTable As DataTable, ByVal sClientID As String, ByVal sCarrierAccount As String, ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, ByVal iFareNo As Integer) As String
        Dim sXML As String = ""
        Dim sFlightNo As String = ""
        Try
            sXML = "<?xml version='1.0' encoding='UTF-8'?>"
            sXML += "<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ns1='urn:os:farequote' xmlns:ns2='http://xml.apache.org/xml-soap' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>"
            sXML += "<SOAP-ENV:Header>"
            sXML += "<ClientId>" & sClientID & "</ClientId>"
            sXML += "<TraceId>" & TrackId & "</TraceId>"
            sXML += "</SOAP-ENV:Header>"
            sXML += "<SOAP-ENV:Body>"
            sXML += "<ns1:getFareQuote SOAP-ENV:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>"
            sXML += "<input xsi:type='ns1:FareQuoteRequest'>"
            sXML += "<CarrierAccount xsi:type='xsd:string'>" & sCarrierAccount & "</CarrierAccount>"
            sXML += "<DisplayCurrency xsi:type='xsd:string'>INR</DisplayCurrency>"
            sXML += "<BaseCurrency xsi:type='xsd:string'>INR</BaseCurrency>"
            '*********************** Passenger List Start *******************
            sXML += "<PassengerList xsi:type='ns2:Vector'>"
            If iTotalNo_Adt > 0 Then
                sXML += "<Item xsi:type='ns1:Passenger'>"
                sXML += "<TypeCode xsi:type='xsd:string'>ADT</TypeCode>"
                sXML += "</Item>"
            End If
            If iTotalNo_Chd > 0 Then
                sXML += "<Item xsi:type='ns1:Passenger'>"
                sXML += "<TypeCode xsi:type='xsd:string'>CHD</TypeCode>"
                sXML += "</Item>"
            End If
            sXML += "</PassengerList>"
            '*********************** Passenger List End *******************
            '*********************** Air Components Start *******************
            sXML += "<AirComponents xsi:type='ns2:Vector'>"

            Dim fltdt As New DataTable
            fltdt = dtTable.Clone()
            Dim cnt As Integer = 1
            Dim i As Integer = 0
            While i <= dtTable.Rows.Count - 1
                sXML += "<item xsi:type='ns1:AirComponent'>"
                sXML += "<SelectedFareBasisCode xsi:type='xsd:string'>" & dtTable.Rows(i)("FBCode") & "</SelectedFareBasisCode>"
                sXML += "<Fares xsi:type='ns2:Vector'>"
                sXML += "<item xsi:type='ns1:AvailableFare'>"
                sXML += "<BaseAmount xsi:type='ns1:Currency'>"
                sXML += "<Amount xsi:type='xsd:double'>" & dtTable.Rows(i)("BaseAmt") & "</Amount>"
                sXML += "<CurrencyCode xsi:type='xsd:string'>INR</CurrencyCode>"
                sXML += "</BaseAmount>"
                sXML += "<PassengerTypeCode xsi:type='xsd:string'>ADT</PassengerTypeCode>"
                sXML += "<Meal xsi:type='xsd:string' />"
                sXML += "<FareGroup xsi:type='xsd:string'>" & dtTable.Rows(i)("FareGroup") & "</FareGroup>"
                sXML += "<ToSell xsi:type='xsd:int'>" & dtTable.Rows(i)("ToSell") & "</ToSell>"
                sXML += "<Cabin xsi:type='xsd:string'>1</Cabin>"
                sXML += "<FareBasisCode xsi:type='xsd:string'>" & dtTable.Rows(i)("FBCode") & "</FareBasisCode>"
                sXML += "</item>"
                sXML += "</Fares>"
                sXML += "<Flights xsi:type='ns2:Vector'>"

                'If dtTable.Rows(i)("TripType") = "R" Then cnt = dtTable.Rows(i)("LegCnt")
                cnt = dtTable.Rows(i)("LegCnt")
                Dim fltArray As Array = dtTable.Select("LegCnt='" & cnt.ToString & "' And TripType='" & dtTable.Rows(i)("TripType") & "'", "")
                For j As Integer = 0 To fltArray.Length - 1
                    fltdt.Clear()
                    fltdt.ImportRow(fltArray(j))
                    If sFlightNo <> fltdt.Rows(0)("FlightNumber") Then
                        sXML += "<item xsi:type='ns1:Flight'>"
                        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & fltdt.Rows(0)("ArrivalDateTime") & "</ArrivalDateTime>"
                        sXML += "<OpSuffix xsi:type='xsd:string'/>"
                        sXML += "<NumberOfStops xsi:type='xsd:int'>" & fltdt.Rows(0)("Stops") & "</NumberOfStops>"
                        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & fltdt.Rows(0)("DepDate") & "</DepartureDateTime>"
                        If fltArray.Length > 1 Then
                            If j + 1 <= fltArray.Length - 1 Then
                                If fltArray(j).ItemArray(15) <> fltArray(j + 1).ItemArray(15) Then
                                    sXML += "<Origin xsi:type='xsd:string'>" & fltdt.Rows(0)("DepartureAirport") & "</Origin>"
                                    sXML += "<Destination xsi:type='xsd:string'>" & fltdt.Rows(0)("ArrivalAirport") & "</Destination>"
                                Else
                                    sXML += "<Origin xsi:type='xsd:string'>" & (fltArray(j)("DepartureAirport")) & "</Origin>"
                                    sXML += "<Destination xsi:type='xsd:string'>" & (fltArray(j + 1)("ArrivalAirport")) & "</Destination>"
                                End If
                            Else
                                sXML += "<Origin xsi:type='xsd:string'>" & fltdt.Rows(0)("DepartureAirport") & "</Origin>"
                                sXML += "<Destination xsi:type='xsd:string'>" & fltdt.Rows(0)("ArrivalAirport") & "</Destination>"
                            End If
                        Else
                            sXML += "<Origin xsi:type='xsd:string'>" & fltdt.Rows(0)("Origin") & "</Origin>"
                            sXML += "<Destination xsi:type='xsd:string'>" & fltdt.Rows(0)("Destination") & "</Destination>"
                        End If
                        sXML += "<CarrierCode xsi:type='xsd:string'>" & fltdt.Rows(0)("CarrierCode") & "</CarrierCode>"
                        sXML += "<FlightNumber xsi:type='xsd:string'>" & fltdt.Rows(0)("FlightNumber") & "</FlightNumber>"
                        sXML += "</item>"
                        sFlightNo = fltdt.Rows(0)("FlightNumber")
                    End If
                    i = i + 1
                Next
                cnt = cnt + 1
                sXML += "</Flights>"
                sXML += "</item>"
            End While
            sXML += "</AirComponents>"
            '*********************** Air Components End *******************
            sXML += "</input>"
            sXML += "</ns1:getFareQuote>"
            sXML += "</SOAP-ENV:Body>"
            sXML += "</SOAP-ENV:Envelope>"
        Catch ex As Exception

        End Try

        Return sXML
    End Function

    Public Function GetBookingDT(ByVal dtTemp As DataTable, ByVal custinfo As Hashtable, ByVal TotalFare As String, _
                                   ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, _
                                   ByVal TourType As String) As DataTable
        Dim dtBookingDT As New DataTable
        Dim row As DataRow
        Dim xmlDoc As New XmlDocument
        Dim sTransID As String = "", TraceId As String = ""
        Dim sSessionId As String = ""
        Dim sCreateDateTime As String = ""
        Dim sStatus As String = ""
        Dim sPNRId As String = ""
        Dim sAltPNRId As String = ""
        Dim sCreateUser As String = ""
        Dim sResponse As String = ""
        Dim SRequest As String = ""
        Try
            dtBookingDT = objDB.CreateBookingDT
            row = dtBookingDT.NewRow()
            SRequest = GetBookingReq(dtTemp, custinfo, TotalFare, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, TourType)
            sResponse = PostXml(sUrl, SRequest)
            xmlDoc.LoadXml(sResponse)
            sTransID = xmlDoc.DocumentElement("SOAP-ENV:Header").ChildNodes.ItemOf(0).InnerText
            TraceId = xmlDoc.DocumentElement("SOAP-ENV:Header").ChildNodes.ItemOf(1).InnerText
            For i As Integer = 0 To xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).LocalName = "SessionID" Then
                    sSessionId = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).InnerText
                End If
                If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).LocalName = "Pnr" Then
                    For ii As Integer = 0 To xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.Count - 1
                        If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).LocalName = "CreateDateTime" Then
                            sCreateDateTime = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).InnerText
                        End If
                        If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).LocalName = "Status" Then
                            sStatus = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).InnerText
                        End If
                        If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).LocalName = "PNRId" Then
                            sPNRId = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).InnerText
                        End If
                        If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).LocalName = "AltPNRId" Then
                            sAltPNRId = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).InnerText
                        End If
                        If xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).LocalName = "CreateUser" Then
                            sCreateUser = xmlDoc.DocumentElement("SOAP-ENV:Body").ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(i).ChildNodes.ItemOf(ii).InnerText
                        End If
                    Next
                End If
            Next
            Try
                row("TransID") = sTransID
                row("SessionId") = sSessionId
                row("TraceId") = TraceId
                row("CreateDateTime") = sCreateDateTime
                row("Status") = sStatus
                row("PNRId") = sPNRId
                row("AltPNRId") = sAltPNRId
                row("CreateUser") = sCreateUser
                row("ReqXml") = SRequest
                row("ResXml") = sResponse
                row("BKGStatus") = "TRUE"
            Catch ex As Exception
                row("PNRId") = sPNRId
                row("ReqXml") = SRequest
                row("ResXml") = sResponse
                row("BKGStatus") = "FALSE"
                dtBookingDT.Rows.Add(row)
            End Try
            dtBookingDT.Rows.Add(row)
        Catch ex As Exception
            row("PNRId") = sPNRId
            row("ReqXml") = SRequest
            row("ResXml") = sResponse
            row("BKGStatus") = "FALSE"
            dtBookingDT.Rows.Add(row)
        End Try
        Return dtBookingDT
    End Function

    Private Function GetBookingReq(ByVal dtTemp As DataTable, ByVal custinfo As Hashtable, ByVal TotalFare As String, _
                                   ByVal iTotalNo_Adt As Integer, ByVal iTotalNo_Chd As Integer, ByVal iTotalNo_Inf As Integer, _
                                   ByVal TourType As String) As String
        Dim iTotalpass As Integer = iTotalNo_Adt + iTotalNo_Chd
        Dim sXML As String = "<?xml version='1.0' encoding='UTF-8'?>"
        sXML += "<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ns1='urn:os:bookreservation' xmlns:ns2='http://xml.apache.org/xml-soap' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>"
        sXML += "<SOAP-ENV:Header>"
        sXML += "<ClientId>" & sClientID & "</ClientId>"
        sXML += "<TraceId>" & dtTemp.Rows(0)("sno") & "</TraceId>"
        'sXML += "<ApiSource>API0S</ApiSource>"
        'sXML += "<AuthorizationKey />"
        sXML += "<DistributorNumber>" & sDistNo & "</DistributorNumber>"
        sXML += "</SOAP-ENV:Header>"
        sXML += "<SOAP-ENV:Body>"
        sXML += "<ns1:bookreservation SOAP-ENV:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>"
        sXML += "<input xsi:type='ns1:BookReservationRequest'>"
        sXML += "<CarrierAccount xsi:type='xsd:string'>" & sCarrierAccount & "</CarrierAccount>"
        sXML += "<Reservation xsi:type='ns1:Reservation'>"
        sXML += "<CallerName xsi:type='xsd:string'>" & custinfo("LName") & "/" & custinfo("FName") & "</CallerName>"
        sXML += "<DistributionOption xsi:type='xsd:string'>E</DistributionOption>"
        sXML += "<ContactInfo xsi:type='ns1:Contact'>"
        sXML += "<Address xsi:type='ns1:Address'>"
        sXML += "<AddressName xsi:type='xsd:string'>" & custinfo("sAddName") & "</AddressName>"
        sXML += "<AddressType xsi:type='xsd:string'>M</AddressType>"
        sXML += "<City xsi:type='xsd:string'>" & custinfo("sCity") & "</City>"
        sXML += "<Country xsi:type='xsd:string' />"
        sXML += "<Line1 xsi:type='xsd:string'>" & custinfo("sLine1") & "</Line1>"
        sXML += "<Line2 xsi:type='xsd:string'>" & custinfo("sLine2") & "</Line2>"
        sXML += "<Line3 xsi:type='xsd:string' />"
        sXML += "<Province xsi:type='xsd:string' />"
        sXML += "<State xsi:type='xsd:string'>" & custinfo("sState") & "</State>"
        sXML += "<Zip xsi:type='xsd:string'>" & custinfo("sZip") & "</Zip>"
        sXML += "</Address>"
        sXML += "<HomePhone xsi:type='xsd:string'>" & custinfo("sHomePhn") & "</HomePhone>"
        sXML += "<EmailAddress xsi:type='xsd:string'>" & custinfo("Customeremail") & "</EmailAddress>"
        sXML += "<AgencyPhone xsi:type='xsd:string'>" & custinfo("sAgencyPhn") & "</AgencyPhone>"
        sXML += "<FirstName xsi:type='xsd:string'>" & custinfo("FName") & "</FirstName>"
        sXML += "<LastName xsi:type='xsd:string'>" & custinfo("LName") & "</LastName>"
        sXML += "<LanguageCode xsi:type='xsd:string'>EN</LanguageCode>"
        sXML += "</ContactInfo>"
        sXML += "<Payments xsi:type='ns2:Vector'>"
        sXML += "<item xsi:type='ns1:Payment'>"
        sXML += "<AgencyPayment xsi:type='ns1:AgencyPayment'>"
        sXML += "<Comment xsi:type='xsd:string'>" & custinfo("sComments") & "</Comment>"
        sXML += "</AgencyPayment>"
        sXML += "<BaseAmount xsi:type='ns1:Currency'>"
        sXML += "<Amount xsi:type='xsd:double'>" & TotalFare & "</Amount>" ' (Adult Total Fare * No of ADT) + (Child total Fare * No of CHD)
        sXML += "<CurrencyCode xsi:type='xsd:string'>INR</CurrencyCode>"
        sXML += "</BaseAmount>"
        sXML += "<PaymentType xsi:type='xsd:string'>AG</PaymentType>"
        sXML += "</item>"
        sXML += "</Payments>"
        sXML += "<NumberPassengers xsi:type='xsd:int'>" & iTotalpass & "</NumberPassengers>"
        sXML += "<PassengerList xsi:type='ns2:Vector'>"
        If iTotalNo_Adt > 0 Then
            For iCtr As Integer = 1 To iTotalNo_Adt
                sXML += "<item xsi:type='ns1:Passenger'>"
                sXML += "<TypeCode xsi:type='xsd:string'>ADT</TypeCode>"
                If custinfo("Title_ADT" & iCtr).ToString.Trim.ToUpper = "MR" Then
                    sXML += "<Gender xsi:type='xsd:string'>M</Gender>"
                Else
                    sXML += "<Gender xsi:type='xsd:string'>F</Gender>"
                End If
                sXML += "<FirstName xsi:type='xsd:string'>" & custinfo("FNameADT" & iCtr) & "</FirstName>"
                sXML += "<LastName xsi:type='xsd:string'>" & custinfo("LnameADT" & iCtr) & "</LastName>"
                sXML += "<Title xsi:type='xsd:string'>" & custinfo("Title_ADT" & iCtr) & "</Title>"
                sXML += "</item>"
            Next
        End If

        If iTotalNo_Chd > 0 Then
            For iCtr As Integer = 1 To iTotalNo_Chd
                sXML += "<item xsi:type='ns1:Passenger'>"
                sXML += "<TypeCode xsi:type='xsd:string'>CHD</TypeCode>"
                If custinfo("Title_ADT" & iCtr).ToString.Trim.ToUpper = "MR" Then
                    sXML += "<Gender xsi:type='xsd:string'>M</Gender>"
                Else
                    sXML += "<Gender xsi:type='xsd:string'>F</Gender>"
                End If
                sXML += "<FirstName xsi:type='xsd:string'>" & custinfo("FNameCHD" & iCtr) & "</FirstName>"
                sXML += "<LastName xsi:type='xsd:string'>" & custinfo("LnameCHD" & iCtr) & "</LastName>"
                sXML += "<Title xsi:type='xsd:string'>" & custinfo("Title_CHD" & iCtr) & "</Title>"
                sXML += "</item>"
            Next
        End If

        '***********Infant booking not available in Spice API.**************
        'If iTotalNo_Inf > 0 Then
        '    For iCtr As Integer = 1 To iTotalNo_Inf
        '        sXML += "<item xsi:type='ns1:Passenger'>"
        '        sXML += "<TypeCode xsi:type='xsd:string'>INF</TypeCode>"
        '        sXML += "<Gender xsi:type='xsd:string'/>"
        '        sXML += "<FirstName xsi:type='xsd:string'>" & custinfo("FNameINF" & iCtr) & "</FirstName>"
        '        sXML += "<LastName xsi:type='xsd:string'>" & custinfo("LnameINF" & iCtr) & "</LastName>"
        '        sXML += "<Title xsi:type='xsd:string'>" & custinfo("Title_INF" & iCtr) & "</Title>"
        '        sXML += "</item>"
        '    Next
        'End If

        sXML += "</PassengerList>"
        sXML += "<AirComponents xsi:type='ns2:Vector'>"
        Dim fltArrayD As Array = dtTemp.Select("TripType='O'", "")
        sXML += "<item xsi:type='ns1:AirComponent'>"
        sXML += "<SelectedFareBasisCode xsi:type='xsd:string'>" & (fltArrayD(0)("fareBasis")) & "</SelectedFareBasisCode>"
        sXML += "<Flights xsi:type='ns2:Vector'>"

        'For i As Integer = 0 To fltArrayD.Length - 1
        '    sXML += "<item xsi:type='ns1:Flight'> "
        '    If fltArrayD.Length > 2 Then
        '        If ((fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification"))) And ((fltArrayD(0)("FlightIdentification")) = (fltArrayD(2)("FlightIdentification"))) Then
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '            Exit For
        '        Else                    
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("DepartureLocation")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("ArrivalLocation")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '        End If

        '    ElseIf fltArrayD.Length > 1 Then
        '        If (fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification")) Then
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '            Exit For
        '        Else
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("DepartureLocation")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("ArrivalLocation")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '        End If
        '    Else
        '        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '        sXML += "<OpSuffix xsi:type='xsd:string' />"
        '        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '        sXML += "</item>"
        '    End If
        'Next
        Dim j As Integer = 0
        While j <= fltArrayD.Length - 1
            sXML += "<item xsi:type='ns1:Flight'> "
            sXML += "<Status xsi:type='xsd:string'>SS</Status>"


            If fltArrayD.Length > 2 Then
                If ((fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification"))) And ((fltArrayD(0)("FlightIdentification")) = (fltArrayD(2)("FlightIdentification"))) Then
                    sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
                    'sXML += "<OpSuffix xsi:type='xsd:string' />"
                    sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                    sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestFrom")) & "</Origin>"
                    sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestTo")) & "</Destination>"
                    sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                    sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                    sXML += "</item>"
                    Exit While
                Else
                    If j + 1 <= fltArrayD.Length - 1 Then
                        If (fltArrayD(j)("FlightIdentification")) <> (fltArrayD(j + 1)("FlightIdentification")) Then
                            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(j)("ArrivalTime")) & "</ArrivalDateTime>"
                            'sXML += "<OpSuffix xsi:type='xsd:string' />"
                            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("DepartureLocation")) & "</Origin>"
                            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("ArrivalLocation")) & "</Destination>"
                            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                            sXML += "</item>"
                            j += 1
                        Else
                            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(j)("ArrivalTime")) & "</ArrivalDateTime>"
                            'sXML += "<OpSuffix xsi:type='xsd:string' />"
                            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j + 1)("depdatelcc")) & "T" & (fltArrayD(j + 1)("DepartureTime")) & "</DepartureDateTime>"
                            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("DepartureLocation")) & "</Origin>"
                            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j + 1)("ArrivalLocation")) & "</Destination>"
                            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                            sXML += "</item>"
                            j += 2
                        End If
                    Else
                        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(j)("ArrivalTime")) & "</ArrivalDateTime>"
                        'sXML += "<OpSuffix xsi:type='xsd:string' />"
                        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("DepartureLocation")) & "</Origin>"
                        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("ArrivalLocation")) & "</Destination>"
                        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                        sXML += "</item>"
                        j += 1
                    End If


                    'sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
                    ''sXML += "<OpSuffix xsi:type='xsd:string' />"
                    'sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
                    'sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("DepartureLocation")) & "</Origin>"
                    'sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("ArrivalLocation")) & "</Destination>"
                    'sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
                    'sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
                    'sXML += "</item>"
                End If

            ElseIf fltArrayD.Length > 1 Then
                If (fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification")) Then
                    sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
                    'sXML += "<OpSuffix xsi:type='xsd:string' />"
                    sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                    sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestFrom")) & "</Origin>"
                    sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestTo")) & "</Destination>"
                    sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                    sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                    sXML += "</item>"
                    Exit While
                Else
                    sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(j)("ArrivalTime")) & "</ArrivalDateTime>"
                    'sXML += "<OpSuffix xsi:type='xsd:string' />"
                    sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                    sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("DepartureLocation")) & "</Origin>"
                    sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("ArrivalLocation")) & "</Destination>"
                    sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                    sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                    sXML += "</item>"
                    j += 1
                End If
            Else
                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("arrdatelcc")) & "T" & (fltArrayD(j)("ArrivalTime")) & "</ArrivalDateTime>"
                'sXML += "<OpSuffix xsi:type='xsd:string' />"
                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(j)("depdatelcc")) & "T" & (fltArrayD(j)("DepartureTime")) & "</DepartureDateTime>"
                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestFrom")) & "</Origin>"
                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(j)("OrgDestTo")) & "</Destination>"
                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(j)("FlightIdentification")) & "</FlightNumber>"
                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(j)("MarketingCarrier")) & "</CarrierCode>"
                sXML += "</item>"
                j += 1
            End If

        End While
        sXML += "</Flights>"
        sXML += "</item>"
        If TourType.ToUpper.Trim = "RETURN" Then
            Dim fltArrayR As Array = dtTemp.Select("TripType='R'", "")
            sXML += "<item xsi:type='ns1:AirComponent'>"
            sXML += "<SelectedFareBasisCode xsi:type='xsd:string'>" & (fltArrayR(0)("fareBasis")) & "</SelectedFareBasisCode>"
            sXML += "<Flights xsi:type='ns2:Vector'>"
            'For i As Integer = 0 To fltArrayR.Length - 1
            '    sXML += "<item xsi:type='ns1:Flight'> "

            '    If fltArrayR.Length > 2 Then
            '        If (fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification")) = (fltArrayR(2)("FlightIdentification")) Then
            '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
            '            sXML += "<OpSuffix xsi:type='xsd:string' />"
            '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
            '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
            '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
            '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
            '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
            '            sXML += "</item>"
            '            Exit For
            '        Else
            '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
            '            sXML += "<OpSuffix xsi:type='xsd:string' />"
            '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
            '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("DepartureLocation")) & "</Origin>"
            '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("ArrivalLocation")) & "</Destination>"
            '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
            '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
            '            sXML += "</item>"
            '        End If
            '    ElseIf fltArrayR.Length > 1 Then
            '        If (fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification")) Then
            '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
            '            sXML += "<OpSuffix xsi:type='xsd:string' />"
            '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
            '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
            '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
            '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
            '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
            '            sXML += "</item>"
            '            Exit For
            '        Else
            '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
            '            sXML += "<OpSuffix xsi:type='xsd:string' />"
            '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
            '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("DepartureLocation")) & "</Origin>"
            '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("ArrivalLocation")) & "</Destination>"
            '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
            '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
            '            sXML += "</item>"
            '        End If
            '    Else
            '        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
            '        sXML += "<OpSuffix xsi:type='xsd:string' />"
            '        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
            '        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
            '        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
            '        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
            '        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
            '        sXML += "</item>"
            '    End If
            'Next
            Dim k As Integer = 0
            While k <= fltArrayR.Length - 1
                sXML += "<item xsi:type='ns1:Flight'> "
                sXML += "<Status xsi:type='xsd:string'>SS</Status>"


                If fltArrayR.Length > 2 Then
                    If ((fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification"))) And ((fltArrayR(0)("FlightIdentification")) = (fltArrayR(2)("FlightIdentification"))) Then
                        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
                        'sXML += "<OpSuffix xsi:type='xsd:string' />"
                        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestFrom")) & "</Origin>"
                        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestTo")) & "</Destination>"
                        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                        sXML += "</item>"
                        Exit While
                    Else
                        If k + 1 <= fltArrayR.Length - 1 Then
                            If (fltArrayR(k)("FlightIdentification")) <> (fltArrayR(k + 1)("FlightIdentification")) Then
                                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(k)("ArrivalTime")) & "</ArrivalDateTime>"
                                'sXML += "<OpSuffix xsi:type='xsd:string' />"
                                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("DepartureLocation")) & "</Origin>"
                                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("ArrivalLocation")) & "</Destination>"
                                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                                sXML += "</item>"
                                k += 1
                            Else
                                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(k)("ArrivalTime")) & "</ArrivalDateTime>"
                                'sXML += "<OpSuffix xsi:type='xsd:string' />"
                                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k + 1)("depdatelcc")) & "T" & (fltArrayR(k + 1)("DepartureTime")) & "</DepartureDateTime>"
                                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("DepartureLocation")) & "</Origin>"
                                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k + 1)("ArrivalLocation")) & "</Destination>"
                                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                                sXML += "</item>"
                                k += 2
                            End If
                        Else
                            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(k)("ArrivalTime")) & "</ArrivalDateTime>"
                            'sXML += "<OpSuffix xsi:type='xsd:string' />"
                            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("DepartureLocation")) & "</Origin>"
                            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("ArrivalLocation")) & "</Destination>"
                            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                            sXML += "</item>"
                            k += 1
                        End If


                        'sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
                        ''sXML += "<OpSuffix xsi:type='xsd:string' />"
                        'sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
                        'sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("DepartureLocation")) & "</Origin>"
                        'sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("ArrivalLocation")) & "</Destination>"
                        'sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
                        'sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
                        'sXML += "</item>"
                    End If

                ElseIf fltArrayR.Length > 1 Then
                    If (fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification")) Then
                        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
                        'sXML += "<OpSuffix xsi:type='xsd:string' />"
                        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestFrom")) & "</Origin>"
                        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestTo")) & "</Destination>"
                        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                        sXML += "</item>"
                        Exit While
                    Else
                        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(k)("ArrivalTime")) & "</ArrivalDateTime>"
                        'sXML += "<OpSuffix xsi:type='xsd:string' />"
                        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("DepartureLocation")) & "</Origin>"
                        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("ArrivalLocation")) & "</Destination>"
                        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                        sXML += "</item>"
                        k += 1
                    End If
                Else
                    sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("arrdatelcc")) & "T" & (fltArrayR(k)("ArrivalTime")) & "</ArrivalDateTime>"
                    'sXML += "<OpSuffix xsi:type='xsd:string' />"
                    sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(k)("depdatelcc")) & "T" & (fltArrayR(k)("DepartureTime")) & "</DepartureDateTime>"
                    sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestFrom")) & "</Origin>"
                    sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(k)("OrgDestTo")) & "</Destination>"
                    sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(k)("FlightIdentification")) & "</FlightNumber>"
                    sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(k)("MarketingCarrier")) & "</CarrierCode>"
                    sXML += "</item>"
                    k += 1
                End If

            End While
            sXML += "</Flights>"
            sXML += "</item>"
        End If
        sXML += "</AirComponents>"
        sXML += "</Reservation>"
        sXML += "</input>"
        sXML += "</ns1:bookreservation>"
        sXML += "</SOAP-ENV:Body>"
        sXML += "</SOAP-ENV:Envelope>"
        'sXML += "<AirComponents xsi:type='ns2:Vector'>"
        'Dim fltArrayD As Array = dtTemp.Select("TripType='O'", "")
        'sXML += "<item xsi:type='ns1:AirComponent'>"
        'sXML += "<SelectedFareBasisCode xsi:type='xsd:string'>" & (fltArrayD(0)("fareBasis")) & "</SelectedFareBasisCode>"
        'sXML += "<Flights xsi:type='ns2:Vector'>"
        'For i As Integer = 0 To fltArrayD.Length - 1
        '    sXML += "<item xsi:type='ns1:Flight'> "


        '    If fltArrayD.Length > 2 Then
        '        If (fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification")) = (fltArrayD(2)("FlightIdentification")) Then
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '            Exit For
        '        Else
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("DepartureLocation")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("ArrivalLocation")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '        End If

        '    ElseIf fltArrayD.Length > 1 Then
        '        If (fltArrayD(0)("FlightIdentification")) = (fltArrayD(1)("FlightIdentification")) Then
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(fltArrayD.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '            Exit For
        '        Else
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("DepartureLocation")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("ArrivalLocation")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '        End If
        '    Else
        '        sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("arrdatelcc")) & "T" & (fltArrayD(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '        sXML += "<OpSuffix xsi:type='xsd:string' />"
        '        sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayD(i)("depdatelcc")) & "T" & (fltArrayD(i)("DepartureTime")) & "</DepartureDateTime>"
        '        sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestFrom")) & "</Origin>"
        '        sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayD(i)("OrgDestTo")) & "</Destination>"
        '        sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayD(i)("FlightIdentification")) & "</FlightNumber>"
        '        sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayD(i)("MarketingCarrier")) & "</CarrierCode>"
        '        sXML += "</item>"
        '    End If
        'Next
        'sXML += "</Flights>"
        'sXML += "</item>"
        'If TourType.ToUpper.Trim = "RETURN" Then
        '    Dim fltArrayR As Array = dtTemp.Select("TripType='R'", "")
        '    sXML += "<item xsi:type='ns1:AirComponent'>"
        '    sXML += "<SelectedFareBasisCode xsi:type='xsd:string'>" & (fltArrayR(0)("fareBasis")) & "</SelectedFareBasisCode>"
        '    sXML += "<Flights xsi:type='ns2:Vector'>"
        '    For i As Integer = 0 To fltArrayR.Length - 1
        '        sXML += "<item xsi:type='ns1:Flight'> "

        '        If fltArrayR.Length > 2 Then
        '            If (fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification")) = (fltArrayR(2)("FlightIdentification")) Then
        '                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '                sXML += "<OpSuffix xsi:type='xsd:string' />"
        '                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
        '                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
        '                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
        '                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
        '                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
        '                sXML += "</item>"
        '                Exit For
        '            Else
        '                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '                sXML += "<OpSuffix xsi:type='xsd:string' />"
        '                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
        '                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("DepartureLocation")) & "</Origin>"
        '                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("ArrivalLocation")) & "</Destination>"
        '                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
        '                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
        '                sXML += "</item>"
        '            End If
        '        ElseIf fltArrayR.Length > 1 Then
        '            If (fltArrayR(0)("FlightIdentification")) = (fltArrayR(1)("FlightIdentification")) Then
        '                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(fltArrayR.Length - 1)("ArrivalTime")) & "</ArrivalDateTime>"
        '                sXML += "<OpSuffix xsi:type='xsd:string' />"
        '                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
        '                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
        '                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
        '                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
        '                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
        '                sXML += "</item>"
        '                Exit For
        '            Else
        '                sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '                sXML += "<OpSuffix xsi:type='xsd:string' />"
        '                sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
        '                sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("DepartureLocation")) & "</Origin>"
        '                sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("ArrivalLocation")) & "</Destination>"
        '                sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
        '                sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
        '                sXML += "</item>"
        '            End If
        '        Else
        '            sXML += "<ArrivalDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("arrdatelcc")) & "T" & (fltArrayR(i)("ArrivalTime")) & "</ArrivalDateTime>"
        '            sXML += "<OpSuffix xsi:type='xsd:string' />"
        '            sXML += "<DepartureDateTime xsi:type='xsd:dateTime'>" & (fltArrayR(i)("depdatelcc")) & "T" & (fltArrayR(i)("DepartureTime")) & "</DepartureDateTime>"
        '            sXML += "<Origin xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestFrom")) & "</Origin>"
        '            sXML += "<Destination xsi:type='xsd:string'>" & (fltArrayR(i)("OrgDestTo")) & "</Destination>"
        '            sXML += "<FlightNumber xsi:type='xsd:string'>" & (fltArrayR(i)("FlightIdentification")) & "</FlightNumber>"
        '            sXML += "<CarrierCode xsi:type='xsd:string'>" & (fltArrayR(i)("MarketingCarrier")) & "</CarrierCode>"
        '            sXML += "</item>"
        '        End If
        '    Next
        '    sXML += "</Flights>"
        '    sXML += "</item>"
        'End If
        'sXML += "</AirComponents>"
        'sXML += "</Reservation>"
        'sXML += "</input>"
        'sXML += "</ns1:bookreservation>"
        'sXML += "</SOAP-ENV:Body>"
        'sXML += "</SOAP-ENV:Envelope>"
        Return sXML
    End Function

    Private Function PostXml(ByVal url As String, ByVal xml As String) As String
        Dim bytes As Byte() = UTF8Encoding.UTF8.GetBytes(xml)
        Dim strResult As String = String.Empty
        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentLength = bytes.Length
            request.ContentType = "text/xml"
            request.KeepAlive = False
            request.Timeout = 3600 * 1000
            request.ReadWriteTimeout = 3600 * 1000
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(bytes, 0, bytes.Length)
            End Using
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", response.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim xmldoc As New XmlDocument()
                    Dim reader As StreamReader = Nothing
                    Dim responseStream As Stream = response.GetResponseStream()
                    reader = New StreamReader(responseStream)
                    strResult = reader.ReadToEnd()
                    response.Close()
                    responseStream.Close()
                    reader.Close()
                End If
            End Using
        Catch ex As Exception
        End Try
        Return strResult
    End Function

    Private Function GetTop8(ByVal dt As DataTable, ByVal _Start As Integer, ByVal RowCount As Integer) As DataTable
        Dim _table As DataTable
        _table = dt.Clone()
        For i As Integer = _Start To RowCount
            If i >= dt.Rows.Count Then
                Exit For
            Else
                Dim dtArray As Array = dt.Select("TransID='" & i + 1 & "'", "")
                For ii As Integer = 0 To dtArray.Length - 1
                    _table.ImportRow(dtArray(ii))
                Next
            End If
        Next
        Return _table
    End Function

    Private Function GetTraceIDForSpiceJet() As String
        Dim allowedChars As String = ""
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,@,$"
        Dim rnmd1 As String = GetRandomNumber(allowedChars)
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim rnmd2 As String = GetRandomNumber(allowedChars)
        Return rnmd1 & "|" & rnmd2
    End Function

    Private Function GetRandomNumber(ByVal allowedChars1 As String) As String
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars1.Split(sep)
        Dim rndString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To 9
            temp = arr(rand.[Next](0, arr.Length))
            rndString += temp
        Next
        Return rndString
    End Function

    Private Function ServiceTax(ByVal VC As String, ByVal TotBFWI As Double, ByVal TotBFWOI As Double, ByVal FS As Double, ByVal Trip As String) As Hashtable
        Dim dtTax As New DataTable
        Dim AirlineCharges As New Hashtable
        Dim sqlcom As New SqlCommand
        Dim SqlQuery = "ServiceCharge"
        sqlcom = New SqlCommand(SqlQuery, Conn)
        sqlcom.Parameters.Add("@vc", SqlDbType.VarChar).Value = VC
        sqlcom.Parameters.Add("@trip", SqlDbType.VarChar).Value = Trip
        sqlcom.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter(sqlcom)
        da.Fill(dtTax)

        Try
            If dtTax.Rows.Count > 0 Then
                AirlineCharges.Add("STax", 0) 'Math.Round(((TotBFWI * dtTax.Rows(0)("SrvTax")) / 100), 0)
                AirlineCharges.Add("TF", Math.Round((((TotBFWOI + FS) * dtTax.Rows(0)("TranFee")) / 100), 0))
                AirlineCharges.Add("IATAComm", dtTax.Rows(0)("IATAComm"))
            Else
                AirlineCharges.Add("STax", 0)
                ' AirlineCharges.Add("STaxP", 0)
                AirlineCharges.Add("TF", 0)
                AirlineCharges.Add("IATAComm", 0)
            End If
        Catch ex As Exception
            AirlineCharges.Add("STax", 0)
            'AirlineCharges.Add("STaxP", 0)
            AirlineCharges.Add("TF", 0)
            AirlineCharges.Add("IATAComm", 0)
        End Try
        Return AirlineCharges
    End Function

    Private Function GetMarkUp(ByVal AgentID As String, ByVal distrubid As String, ByVal Trip As String, ByVal typeId As String) As DataTable
        Dim dt As New DataTable
        Dim sqlcom As New SqlCommand
        Dim SqlQuery = "GetMarkup"
        sqlcom = New SqlCommand(SqlQuery, Conn)
        sqlcom.Parameters.Add("@trip", SqlDbType.VarChar).Value = Trip
        sqlcom.Parameters.Add("@agid", SqlDbType.VarChar).Value = AgentID
        sqlcom.Parameters.Add("@distrid", SqlDbType.VarChar).Value = distrubid
        sqlcom.Parameters.Add("@idtype", SqlDbType.VarChar).Value = typeId
        sqlcom.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter(sqlcom)
        da.Fill(dt)
        Return dt
    End Function

    Private Function CalcMarkup(ByVal Mrkdt As DataTable, ByVal VC As String, ByVal fare As Double, ByVal Trip As String) As Double
        Dim airMrkArray As Array
        Dim mrkamt As Double = 0
        airMrkArray = Mrkdt.Select("AirlineCode='" & VC & "'", "")
        Try
            If airMrkArray.Length > 0 Then
                If Trip = "I" Then
                    If (airMrkArray(0))("MarkupType") = "P" Then
                        mrkamt = Math.Round((fare * (airMrkArray(0))("MarkupValue")) / 100, 0)
                    ElseIf (airMrkArray(0))("MarkupType") = "F" Then
                        mrkamt = (airMrkArray(0))("MarkupValue")
                    End If
                Else
                    mrkamt = (airMrkArray(0))("MarkUp")
                End If
            Else
                mrkamt = 0
            End If
        Catch ex As Exception
            mrkamt = 0
        End Try
        Return mrkamt
    End Function

    Private Function airLineNames(ByVal air_code As String) As String
        Dim da As New SqlDataAdapter("SELECT AL_Name FROM AirLineNames WHERE (AL_Code = '" & UCase(air_code).Trim & "')", Conn)
        Dim dt As New DataTable()
        da.Fill(dt)
        Dim City_N As String
        If dt.Rows.Count > 0 Then
            City_N = dt.Rows(0)("AL_Name").ToString
        Else
            City_N = air_code
        End If
        Return City_N
    End Function

    Private Function city_name(ByVal CityName) As String

        Dim City_N As String
        Dim da As New SqlDataAdapter("SELECT city FROM airport_info WHERE (airportid = '" & CityName & "') ", Conn)
        Dim dt As New DataTable()
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            City_N = dt.Rows(0).Item("city")
        Else
            Return CityName
        End If
        Return City_N
    End Function

    Private Function datecon(ByVal MM As String) As String
        Dim mm_str As String
        Select Case MM
            Case "01"
                mm_str = "JAN"
            Case "02"
                mm_str = "FEB"
            Case "03"
                mm_str = "MAR"
            Case "04"
                mm_str = "APR"
            Case "05"
                mm_str = "MAY"
            Case "06"
                mm_str = "JUN"
            Case "07"
                mm_str = "JUL"
            Case "08"
                mm_str = "AUG"
            Case "09"
                mm_str = "SEP"
            Case "10"
                mm_str = "OCT"
            Case "11"
                mm_str = "NOV"
            Case "12"
                mm_str = "DEC"
            Case Else
        End Select
        Return mm_str
    End Function

    'Public Function RetriveBooking(ByVal pnr As String, ByVal trackId As String) As String
    '    Dim str As String = "", bookingRes As String = ""
    '    Dim sClientID As String = "spicejet-tbn"
    '    Dim sCarrierAccount As String = "0S"
    '    str = "<?xml version='1.0' encoding='UTF-8'?>"
    '    str = str & "<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>"
    '    str = str & "<SOAP-ENV:Header>"
    '    str = str & "<ClientId>" & sClientID & "</ClientId>"
    '    str = str & "<CarrierAccount xsi:type='xsd:string' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>" & sCarrierAccount & "</CarrierAccount>"
    '    str = str & "<TraceId>" & trackId & "</TraceId>"
    '    str = str & "</SOAP-ENV:Header>"
    '    str = str & "<SOAP-ENV:Body>"
    '    str = str & "<ns1:displayReservation SOAP-ENV:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/' xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ns1='urn:os:displayreservation'>"
    '    str = str & "<input xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:type='ns1:DisplayReservationRequest'>"
    '    str = str & "<PnrId xsi:type='xsd:string'>" & pnr & "</PnrId>"
    '    str = str & "</input>"
    '    str = str & "<AltPnrId xsi:type='xsd:string'/>"
    '    str = str & "</ns1:displayReservation>"
    '    str = str & "</SOAP-ENV:Body>"
    '    str = str & "</SOAP-ENV:Envelope>"
    '    bookingRes = PostXml("http://directnettest.navitaire.com:8080/servlet/rpcrouter", str)
    '    Return bookingRes
    'End Function
End Class
