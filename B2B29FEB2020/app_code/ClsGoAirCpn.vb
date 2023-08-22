Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports GoAirServiceDll

Public Class clsGoAirCpn
    Dim objRadixxSecurity As New RadixxSecurity
    Dim objRadixxtravelAgents As New RadixxTravelAgents
    Dim objRadixxFlight As New RadixxFlights
    Dim objRadixxBooking As New RadixxBooking
    Dim objDBHelper As New clsDbGoAir
    Dim dAdt_Basic As Decimal
    Dim dChd_Basic As Decimal
    Dim dInf_Basic As Decimal
    Dim sAdt_FC As String = ""
    Dim sChd_FC As String = ""
    Dim sInf_FC As String = ""
    Dim sAdt_FareID As String = ""
    Dim sChd_FareID As String = ""
    Dim sInf_FareID As String = ""
    Dim sAdt_FB As String = ""
    Dim sChd_FB As String = ""
    Dim sInf_FB As String = ""
    Dim dFuel_Adt As Decimal
    Dim dPSF_Adt As Decimal
    Dim dUDF_Adt As Decimal
    Dim dPHF_Adt As Decimal
    Dim dST_Adt As Decimal
    Dim dothers_Adt As Decimal
    Dim dFuel_Chd As Decimal
    Dim dPSF_Chd As Decimal
    Dim dUDF_Chd As Decimal
    Dim dPHF_Chd As Decimal
    Dim dST_Chd As Decimal
    Dim dothers_Chd As Decimal
    Dim dFuel_Inf As Decimal
    Dim dPSF_Inf As Decimal
    Dim dUDF_Inf As Decimal
    Dim dPHF_Inf As Decimal
    Dim dST_Inf As Decimal
    Dim dothers_Inf As Decimal
    Dim sFuel_Def As String = ""
    Dim Conn As New SqlConnection
    Dim sAirline As String
    Public Sub New(ByVal strCon As String, ByVal Airline As String)
        Conn.ConnectionString = strCon
        sAirline = Airline
    End Sub

    Public Function GetSecurityGUID(ByVal LoginId As String, ByVal LoginPassword As String) As String
        Dim sGUID As String
        sGUID = objRadixxSecurity.GetSecurityGUID(LoginId, LoginPassword)
        Return sGUID
    End Function

    Public Function ValidateGUID(ByVal sGUID As String) As Boolean
        Dim sValidateResult As String
        Dim bValid As Boolean = False
        sValidateResult = objRadixxSecurity.ValidateGUID(sGUID)
        If sValidateResult = "The current GUID is still valid?: True" Then
            bValid = True
        ElseIf sValidateResult = "The current GUID is not valid?: False" Then
            bValid = False
        Else
            bValid = False
        End If
        Return bValid
    End Function

    Public Function GetAvailability(ByVal sGUID As String, ByVal CorporateId As String, ByVal UserId As String, ByVal Password As String, ByVal Trip As String, ByVal TourType As String, ByVal sOrigin As String, _
                                      ByVal sDestination As String, ByVal sDateOfDep As String, ByVal sFareTypeCat As String, _
                                      ByVal dTotalNo_Adt As Decimal, ByVal dTotalNo_Chd As Decimal, ByVal dTotalNo_Inf As Decimal, ByVal sCabin As String, ByVal IPAddress As String, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal IsCpn As Boolean) As DataTable
        Dim sSearchCriteria As String
        Dim dtTemp As New DataTable
        Dim myTALoginResult As Boolean
        Dim sGetFareQuote As String = ""
        myTALoginResult = objRadixxtravelAgents.LoginTravelAgencyUser(sGUID, CorporateId, UserId, Password)
        If myTALoginResult = True Then
            sSearchCriteria = GetSearchCriteria(CorporateId, "INR", sOrigin, sDestination, sDateOfDep, sFareTypeCat, "102", dTotalNo_Adt, dTotalNo_Chd, dTotalNo_Inf, sCabin)
            sGetFareQuote = objRadixxFlight.GetFareQuote(sGUID, sSearchCriteria, IPAddress)
            dtTemp = GetDataTable(sGetFareQuote, dTotalNo_Adt, dTotalNo_Chd, dTotalNo_Inf, Trip, TourType, sAgentId, sDistr, searchValue, sOrigin, sDestination, tCnt, Ft, TrackId, sGUID, IsCpn)
        End If
        Return dtTemp
    End Function

    Private Function GetSearchCriteria(ByVal sIATA As String, ByVal sCurrency As String, ByVal sOrigin As String, _
                                      ByVal sDestination As String, ByVal sDateOfDep As String, _
                                      ByVal sFareTypeCat As String, _
                                      ByVal sFareFilter As String, _
                                      ByVal dTotalNo_Adt As Decimal, _
                                      ByVal dTotalNo_Chd As Decimal, ByVal dTotalNo_inf As Decimal, ByVal sCabin As String) As String

        Dim myXML As String = ""
        myXML += "<?xml version='1.0' encoding='UTF-8'?>"
        myXML += "<RadixxFareQuoteRequest >"
        myXML += "<CurrencyOfFareQuote>" & sCurrency & "</CurrencyOfFareQuote>"
        myXML += "<PromotionalCode />"
        myXML += "<IataNumberOfRequestor>" & sIATA & "</IataNumberOfRequestor>"
        myXML += "<FareQuoteDetails>"
        myXML += "<FareQuoteDetail>"
        myXML += "<Origin>" & sOrigin & "</Origin> "
        myXML += "<Destination>" & sDestination & "</Destination>"
        myXML += "<DateOfDeparture>" & sDateOfDep & "</DateOfDeparture>"
        myXML += "<FareTypeCategory>" & sFareTypeCat & "</FareTypeCategory>"
        myXML += "<Cabin>" & sCabin & "</Cabin>"
        myXML += "<NumberOfDaysBefore>0</NumberOfDaysBefore>"
        myXML += "<NumberOfDaysAfter>0</NumberOfDaysAfter>"
        myXML += "<LanguageCode/>"
        myXML += "<FareFilterMethod>" & sFareFilter & "</FareFilterMethod>"
        myXML += "<FareQuoteRequestInfos>"
        If dTotalNo_Adt <> "0" Then
            myXML += "<FareQuoteRequestInfo>"
            myXML += "<PassengerTypeID>1</PassengerTypeID>"
            myXML += "<TotalSeatsRequired>" & dTotalNo_Adt & "</TotalSeatsRequired>"
            myXML += "</FareQuoteRequestInfo>"
        End If
        If dTotalNo_Chd <> "0" Then
            myXML += "<FareQuoteRequestInfo>"
            myXML += "<PassengerTypeID>6</PassengerTypeID>"
            myXML += "<TotalSeatsRequired>" & dTotalNo_Chd & "</TotalSeatsRequired>"
            myXML += "</FareQuoteRequestInfo>"
        End If
        If dTotalNo_inf <> "0" Then
            myXML += "<FareQuoteRequestInfo>"
            myXML += "<PassengerTypeID>5</PassengerTypeID>"
            myXML += "<TotalSeatsRequired>" & dTotalNo_inf & "</TotalSeatsRequired>"
            myXML += "</FareQuoteRequestInfo>"
        End If
        myXML += "</FareQuoteRequestInfos>"
        myXML += "</FareQuoteDetail>"
        myXML += "</FareQuoteDetails>"
        myXML += "</RadixxFareQuoteRequest>"
        GetSearchCriteria = myXML
    End Function

    Private Function GetDataTable(ByVal sXML As String, ByVal dTotalNo_Adt As Decimal, _
                                      ByVal dTotalNo_Chd As Decimal, ByVal dTotalNo_Inf As Decimal, ByVal Trip As String, ByVal TourType As String, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal sOrigin As String, ByVal sDestination As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal sGUID As String, ByVal IsCpn As Boolean) As DataTable
        Dim dtTable As New DataTable
        Dim row As DataRow
        Dim xmlDoc As New XmlDocument
        Dim sLFID As String
        Dim iLegCount As Integer
        Dim sDepDate As String
        Dim sFareTypeName As String
        Dim sSeatAvail As String = ""
        Dim scabin As String = ""
        Dim sStop As String
        Dim sTaxId As String
        Dim sCurrCode As String = ""
        Dim sCarrier As String = ""
        Dim sProCode As String = ""
        Dim sPTCID As String = ""
        Dim sOthers As String = ""
        Dim sPFID As String = ""
        Dim sSegmentDtl As String
        Dim sLegDtl As String
        Dim sSegmentDetail() As String
        Dim sTaxDtl As String
        Dim sLegDetail() As String
        Dim iCtr As Integer = "0"
        Dim iCounter As Integer = "0"
        Dim iCtr1 As Integer = "0"
        Dim iCounter1 As Integer = "0"
        Dim iCount As Integer = "0"
        Dim iCount1 As Integer = "0"
        Dim iFlt_Count As Integer = "0"
        xmlDoc.LoadXml(sXML)
        dtTable = objDBHelper.CreateDataTable()
        sCurrCode = xmlDoc.DocumentElement.Attributes.ItemOf("RequestedCurrencyOfFareQuote").Value
        If xmlDoc.DocumentElement.ChildNodes.ItemOf(0).Name = "FlightSegmentDetails" Then
            Do While iCounter <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.Count - 1
                iCount1 = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(1).ChildNodes.Count
                sLFID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("LFID").Value
                sDepDate = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("DepartureDate").Value
                iLegCount = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("LegCount").Value
                Do While iCtr <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                    sFareTypeName = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).Attributes("FareTypeName").Value
                    Init()
                    Do While iCounter1 <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                        sPTCID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("PTCID").Value
                        If sFareTypeName = "GoBusiness" Then
                            scabin = "BUSINESS"
                        Else
                            scabin = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("Cabin").Value
                        End If
                        sSeatAvail = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("SeatsAvailable").Value
                        If sPTCID = "1" Then
                            Do While iCount <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                                sTaxId = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("TaxID").Value
                                sTaxDtl = Trim(getTaxDtl(sTaxId, xmlDoc))
                                If sTaxDtl = "FUEL" Then
                                    dFuel_Adt = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PSF" Then
                                    dPSF_Adt += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "UDF" Then
                                    dUDF_Adt += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PHF" Then
                                    dPHF_Adt += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "FUEL_Def" Then
                                    sFuel_Def = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "ST" Then
                                    dST_Adt += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                Else
                                    dothers_Adt += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                End If
                                iCount = iCount + 1
                            Loop
                            iCount = 0
                            sAdt_FareID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FareID").Value
                            dAdt_Basic = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("BaseFareAmt").Value
                            sAdt_FC = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FCCode").Value
                            sAdt_FB = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FBCode").Value
                        End If
                        If sPTCID = "5" Then
                            Do While iCount <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                                sTaxId = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("TaxID").Value
                                sTaxDtl = Trim(getTaxDtl(sTaxId, xmlDoc))
                                If sTaxDtl = "FUEL" Then
                                    dFuel_Inf = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PSF" Then
                                    dPSF_Inf += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "UDF" Then
                                    dUDF_Inf += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PHF" Then
                                    dPHF_Inf += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "FUEL_Def" Then
                                    sFuel_Def = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "ST" Then
                                    dST_Inf += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                Else
                                    dothers_Inf += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                End If
                                iCount = iCount + 1
                            Loop
                            iCount = 0
                            sInf_FareID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FareID").Value
                            dInf_Basic = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("BaseFareAmt").Value
                            sInf_FC = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FCCode").Value
                            sInf_FB = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FBCode").Value
                        End If
                        If sPTCID = "6" Then
                            Do While iCount <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                                sTaxId = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("TaxID").Value
                                sTaxDtl = Trim(getTaxDtl(sTaxId, xmlDoc))
                                If sTaxDtl = "FUEL" Then
                                    dFuel_Chd = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PSF" Then
                                    dPSF_Chd += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "UDF" Then
                                    dUDF_Chd += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "PHF" Then
                                    dPHF_Chd += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "FUEL_Def" Then
                                    sFuel_Def = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                ElseIf sTaxDtl = "ST" Then
                                    dST_Chd += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                Else
                                    dothers_Chd += xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount).Attributes.ItemOf("Amt").Value
                                End If
                                iCount = iCount + 1
                            Loop
                            iCount = 0
                            sChd_FareID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FareID").Value
                            dChd_Basic = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("BaseFareAmt").Value
                            sChd_FC = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FCCode").Value
                            sChd_FB = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCtr.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter1).Attributes("FBCode").Value
                        End If
                        iCounter1 = iCounter1 + 1
                    Loop
                    Do While iCtr1 <= xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(1).ChildNodes.Count - 1
                        row = dtTable.NewRow()
                        If sAdt_FareID <> "" Then
                            If iCount1 = 1 Then
                                iFlt_Count = iFlt_Count + 1
                            Else
                                If iCtr1 = 0 Then
                                    iFlt_Count = iFlt_Count + 1
                                Else
                                    iFlt_Count = iFlt_Count
                                End If

                            End If
                        End If
                        sPFID = xmlDoc.DocumentElement("FlightSegmentDetails").ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(1).ChildNodes.ItemOf(iCtr1.ToString).Attributes("PFID").Value
                        sSegmentDtl = getSegmentDtl(sLFID, xmlDoc)
                        sSegmentDetail = Split(sSegmentDtl, ",")
                        sLegDtl = getLegDtl(sPFID, xmlDoc)
                        sLegDetail = Split(sLegDtl, ",")
                        sCarrier = sSegmentDetail(2).ToString
                        row("Flt_cnt") = iFlt_Count
                        row("Currency") = sCurrCode
                        row("FareTypeName") = sFareTypeName
                        row("FareID_Adt") = sAdt_FareID
                        row("FareID_Chd") = sChd_FareID
                        row("FareID_Inf") = sInf_FareID
                        row("FCCode_Adt") = sAdt_FC
                        row("FCCode_Chd") = sChd_FC
                        row("FCCode_Inf") = sInf_FC
                        row("FBCode_Adt") = sAdt_FB
                        row("FBCode_Chd") = sChd_FB
                        row("FBCode_Inf") = sInf_FB
                        row("BaseFareAmt_Adt") = dAdt_Basic
                        row("BaseFareAmt_Chd") = dChd_Basic
                        row("BaseFareAmt_Inf") = dInf_Basic
                        row("Cabin") = scabin
                        row("SeatAvail") = sSeatAvail
                        row("PFID") = sPFID
                        If iCount1 = 1 Then
                            row("DepDate") = sDepDate
                            row("Origin") = sSegmentDetail(0).ToString
                            row("Destination") = sSegmentDetail(1).ToString
                            row("CarrierCode") = sCarrier
                            row("ArrivalDate") = sSegmentDetail(3).ToString
                            sStop = sSegmentDetail(4).ToString()
                            row("Stops") = sStop
                            row("FlightTime") = sSegmentDetail(5).ToString
                            row("FlightNo") = sSegmentDetail(8).ToString
                        Else
                            row("Origin") = sLegDetail(0).ToString
                            row("Destination") = sLegDetail(1).ToString
                            row("DepDate") = sLegDetail(2).ToString
                            row("CarrierCode") = sCarrier
                            row("ArrivalDate") = sLegDetail(5).ToString
                            sStop = iCount1 - 1
                            row("Stops") = sStop
                            row("FlightTime") = sLegDetail(6).ToString
                            row("FlightNo") = sLegDetail(3).ToString
                        End If
                        row("FUEL_Adt") = dFuel_Adt
                        row("Adt_Tax") = dFuel_Adt + dPSF_Adt + dUDF_Adt + dPHF_Adt + dST_Adt + dothers_Adt
                        row("FUEL_Chd") = dFuel_Chd
                        row("Chd_Tax") = dFuel_Chd + dPSF_Chd + dUDF_Chd + dPHF_Chd + dST_Chd + dothers_Chd
                        row("FUEL_Inf") = dFuel_Inf
                        row("Inf_Tax") = dFuel_Inf + dPSF_Inf + dUDF_Inf + dPHF_Inf + dST_Inf + dothers_Inf
                        row("Total_Adt") = dAdt_Basic + row("Adt_Tax").ToString
                        row("Total_Chd") = dChd_Basic + row("Chd_Tax").ToString
                        row("Total_Inf") = dInf_Basic + row("Inf_Tax").ToString
                        row("Total_Fare") = (dTotalNo_Adt * dAdt_Basic) + (dTotalNo_Chd * dChd_Basic) + (dTotalNo_Inf * dInf_Basic)
                        row("Total_Tax") = (dTotalNo_Adt * row("Adt_Tax").ToString) + (dTotalNo_Chd * row("Chd_Tax").ToString) + (dTotalNo_Inf * row("Inf_Tax").ToString)
                        row("TotalPay") = ((dTotalNo_Adt * dAdt_Basic) + (dTotalNo_Chd * dChd_Basic) + (dTotalNo_Inf * dInf_Basic)) + ((dTotalNo_Adt * row("Adt_Tax").ToString) + (dTotalNo_Chd * row("Chd_Tax").ToString) + (dTotalNo_Inf * row("Inf_Tax").ToString))
                        row("Leg") = (iCtr1 + 1).ToString
                        dtTable.Rows.Add(row)
                        iCtr1 = iCtr1 + 1
                    Loop
                    iCtr1 = 0
                    iCounter1 = 0
                    iCtr = iCtr + 1
                Loop
                iCtr = 0
                iCounter = iCounter + 1
            Loop
        End If
        iCounter = 0
        If IsCpn = True Then
            GetDataTable = CreateMainTableCpn(dtTable, dTotalNo_Adt, dTotalNo_Chd, dTotalNo_Inf, Trip, TourType, sAgentId, sDistr, searchValue, sOrigin, sDestination, tCnt, Ft, TrackId, sGUID)
        Else
            GetDataTable = CreateMainTable(dtTable, dTotalNo_Adt, dTotalNo_Chd, dTotalNo_Inf, Trip, TourType, sAgentId, sDistr, searchValue, sOrigin, sDestination, tCnt, Ft, TrackId, sGUID)
        End If

    End Function

    Private Function getSegmentDtl(ByVal v_sLFID As String, ByVal xmlDoc As XmlDocument) As String
        Dim sSegmentDtl As String
        Dim iCounter As Integer = 0
        Dim iCtr As Integer = 0
        Dim sLFID As String
        sSegmentDtl = ""
        If xmlDoc.DocumentElement.ChildNodes.ItemOf(1).Name = "SegmentDetails" Then
            Do While iCounter <= xmlDoc.DocumentElement("SegmentDetails").ChildNodes.Count - 1
                sLFID = xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("LFID").Value
                If sLFID = v_sLFID Then
                    Do While iCtr <= 1
                        If sSegmentDtl = "" Then
                            sSegmentDtl = xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("Origin").Value
                        Else
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("Destination").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("CarrierCode").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("ArrivalDate").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("Stops").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("FlightTime").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("AircraftType").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("SellingCarrier").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("FlightNum").Value
                            sSegmentDtl = sSegmentDtl & "," & xmlDoc.DocumentElement("SegmentDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("OperatingFlightNum").Value
                        End If
                        iCtr = iCtr + 1
                    Loop
                End If
                iCounter = iCounter + 1
            Loop
        End If
        getSegmentDtl = sSegmentDtl
    End Function

    Private Function getLegDtl(ByVal v_sPFID As String, ByVal xmlDoc As XmlDocument) As String
        Dim sLegDtl As String
        Dim iCounter As Integer = 0
        Dim iCtr As Integer = 0
        Dim sPFID As String
        sLegDtl = ""
        If xmlDoc.DocumentElement.ChildNodes.ItemOf(2).Name = "LegDetails" Then
            Do While iCounter <= xmlDoc.DocumentElement("LegDetails").ChildNodes.Count - 1
                sPFID = xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("PFID").Value
                If sPFID = v_sPFID Then
                    Do While iCtr <= 1
                        If sLegDtl = "" Then
                            sLegDtl = xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("Origin").Value
                        Else
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("Destination").Value
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("DepartureDate").Value
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("FlightNum").Value
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("International").Value
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("ArrivalDate").Value
                            sLegDtl = sLegDtl & "," & xmlDoc.DocumentElement("LegDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("FlightTime").Value
                        End If
                        iCtr = iCtr + 1
                    Loop
                End If
                iCounter = iCounter + 1
            Loop
        End If
        getLegDtl = sLegDtl
    End Function

    Private Function getTaxDtl(ByVal v_sTaxID As String, ByVal xmlDoc As XmlDocument) As String
        Dim sTaxDtl As String
        Dim iCounter As Integer = 0
        Dim sTaxID As String
        sTaxDtl = ""
        If v_sTaxID = "1353" Then
            sTaxDtl = "FUEL_Def"
        Else
            If xmlDoc.DocumentElement.ChildNodes.ItemOf(3).Name = "TaxDetails" Then
                Do While iCounter <= xmlDoc.DocumentElement("TaxDetails").ChildNodes.Count - 1
                    sTaxID = xmlDoc.DocumentElement("TaxDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("TaxID").Value
                    If sTaxID = v_sTaxID Then
                        sTaxDtl = xmlDoc.DocumentElement("TaxDetails").ChildNodes.ItemOf(iCounter.ToString).Attributes.ItemOf("TaxCode").Value
                    End If
                    iCounter = iCounter + 1
                Loop
            End If
        End If

        getTaxDtl = sTaxDtl
    End Function

    Private Function CreateMainTable(ByVal v_dtTemp As DataTable, ByVal dTotalNo_Adt As Decimal, _
                                      ByVal dTotalNo_Chd As Decimal, ByVal dTotalNo_inf As Decimal, ByVal Trip As String, ByVal TourType As String, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal sOrigin As String, ByVal sDestination As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal sGUID As String) As DataTable
        Dim dtTemp As New DataTable
        Dim row As DataRow
        Dim iCtr As Integer = "0"
        Conn.Open()

        'dtTemp = objDBHelper.CreateMainDataTable()
        dtTemp = objDBHelper.ResultTable()
        'Calculation For AgentMarkUp'
        Dim dtAgentMarkup As New DataTable
        dtAgentMarkup = GetMarkUp(sAgentId, sDistr, Trip, "TA")
        'Calculation For AdminMarkUp'
        Dim dtAdminMarkup As New DataTable
        dtAdminMarkup = GetMarkUp(sAgentId, sDistr, Trip, "AD")

        Do While iCtr <= v_dtTemp.Rows.Count - 1
            If dTotalNo_Adt <> 0 And dTotalNo_Chd <> 0 And dTotalNo_inf <> 0 Then
                If v_dtTemp.Rows(iCtr).Item("FareID_Adt") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Chd") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Inf") <> "" Then
                    row = dtTemp.NewRow()

                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = sOrigin
                    row("OrgDestTo") = sDestination
                    row("Sector") = sOrigin & ":" & sDestination
                    row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                    row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                    row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                    row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                    row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                    row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                    row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                    row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                    row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                    row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                    row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                    row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                    row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("AirLineName") = sAirline
                    row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                    row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                    row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                    row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                    row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                    row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                    row("FBPaxType") = "ADT"
                    row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                    row("Searchvalue") = searchValue.ToString
                    row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                    row("Adult") = dTotalNo_Adt
                    row("Child") = dTotalNo_Chd
                    row("Infant") = dTotalNo_inf
                    row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                    row("Flight") = "1"
                    row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                    row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper 'v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                    totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                    totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                    'Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt"), v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt"), v_dtTemp.Rows(iCtr).Item("FUEL_Adt"), Trip)
                    AStax = HsTblSTax("STax") * dTotalNo_Adt
                    ATF = HsTblSTax("TF") * dTotalNo_Adt
                    HsTblSTax.Clear()
                    HsTblSTax = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd"), v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd"), v_dtTemp.Rows(iCtr).Item("FUEL_Chd"), Trip)
                    CStax = HsTblSTax("STax") * dTotalNo_Chd
                    CTF = HsTblSTax("TF") * dTotalNo_Chd
                    HsTblSTax.Clear()
                    HsTblSTax = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf"), 0, 0, Trip)
                    IStax = HsTblSTax("STax") * dTotalNo_inf
                    ITF = HsTblSTax("TF") * dTotalNo_inf




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0

                    'Calculation of Transaction details end

                    If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                        row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                        row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                        row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                        row("TFee") = 0 ''ATF + CTF + ITF 'HsTblSTax("TF")
                        totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = ADTAdminMrk
                        row("CHDAdminMrk") = CHDAdminMrk
                    Else
                        row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                        row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                        row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                        row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                        'totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = 0
                        row("CHDAdminMrk") = 0
                    End If
                    'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                    'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                    'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                    row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                    'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                    'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                    'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                    row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                    'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                    'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                    'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                    row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    'row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    'row("ADTAdminMrk") = ADTAdminMrk
                    'row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                    row("DisCount") = "0"
                    row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    '//
                    'row.Item("Cabin") = v_dtTemp.Rows(iCtr).Item("Cabin")
                    dtTemp.Rows.Add(row)
                End If
            ElseIf dTotalNo_Adt <> 0 And dTotalNo_inf <> 0 Then
                If v_dtTemp.Rows(iCtr).Item("FareID_Adt") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Inf") <> "" Then
                    row = dtTemp.NewRow()
                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = sOrigin
                    row("OrgDestTo") = sDestination
                    row("Sector") = sOrigin & ":" & sDestination
                    row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                    row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                    row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                    row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                    row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                    row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                    row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                    row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                    row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                    row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                    row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                    row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                    row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("AirLineName") = sAirline
                    row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                    row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                    row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                    row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                    row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                    row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                    row("FBPaxType") = "ADT"
                    row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                    row("Searchvalue") = searchValue.ToString
                    row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                    row("Adult") = dTotalNo_Adt
                    row("Child") = dTotalNo_Chd
                    row("Infant") = dTotalNo_inf
                    row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                    row("Flight") = "1"
                    row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                    row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper 'v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0

                    totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                    totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                    totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                    Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)

                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0

                    'Calculation of Transaction details end

                    If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                        row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                        row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                        row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                        row("TFee") = 0 ''HsTblSTax("TF")
                        totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = ADTAdminMrk
                        row("CHDAdminMrk") = CHDAdminMrk
                    Else
                        row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                        row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                        row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                        row("TFee") = HsTblSTax("TF")
                        'totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = 0
                        row("CHDAdminMrk") = 0
                    End If
                    'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                    'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                    'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                    row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                    'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                    'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                    'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                    row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                    'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                    'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                    'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                    row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                    row("STax") = HsTblSTax("STax")
                    'row("TFee") = HsTblSTax("TF")
                    row("IATAComm") = HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    'row("ADTAdminMrk") = ADTAdminMrk
                    'row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                    row("DisCount") = "0"
                    row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    dtTemp.Rows.Add(row)
                End If
            ElseIf dTotalNo_Adt <> 0 Then
                row = dtTemp.NewRow()
                '//new entry
                row("TripType") = "O"
                row("OrgDestFrom") = sOrigin
                row("OrgDestTo") = sDestination
                row("Sector") = sOrigin & ":" & sDestination
                row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                row("AirLineName") = sAirline
                row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                row("FBPaxType") = "ADT"
                row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                row("Searchvalue") = searchValue.ToString
                row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                row("Adult") = dTotalNo_Adt
                row("Child") = dTotalNo_Chd
                row("Infant") = dTotalNo_inf
                row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                row("Flight") = "1"
                row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                row("Trip") = TourType.ToString
                row("TripCnt") = tCnt
                row("Currency") = "INR"
                row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper ' v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                'Calculation of Transaction details
                Dim totBFWInf As Double = 0
                Dim totBFWOInf As Double = 0
                Dim totFS As Double = 0
                Dim totTax As Double = 0
                Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0

                totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)

                ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                Dim totMrk As Double = 0

                'Calculation of Transaction details end

                If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                    row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                    row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                    row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                    row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                    row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                    row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                    row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                    row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                    row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                    row("TFee") = 0 ''HsTblSTax("TF")
                    totMrk = ADTAdminMrk * dTotalNo_Adt
                    totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                    totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                    totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                Else
                    row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                    row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                    row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                    row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                    row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                    row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                    row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                    row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                    row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                    row("TFee") = HsTblSTax("TF")
                    'totMrk = ADTAdminMrk * dTotalNo_Adt
                    totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                    'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                    totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                    row("ADTAdminMrk") = 0
                    row("CHDAdminMrk") = 0
                End If
                'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                row("STax") = HsTblSTax("STax")
                'row("TFee") = HsTblSTax("TF")
                row("IATAComm") = HsTblSTax("IATAComm")
                row("ADTAgentMrk") = ADTAgentMrk
                row("CHDAgentMrk") = CHDAgentMrk
                'row("ADTAdminMrk") = ADTAdminMrk
                'row("CHDAdminMrk") = CHDAdminMrk
                row("TotalBfare") = totBFWInf
                row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                row("TotalTax") = totTax
                row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                row("DisCount") = "0"
                row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                row("OriginalTT") = totTax
                row("Track_id") = TrackId
                row("FType") = Ft
                dtTemp.Rows.Add(row)
            End If
            iCtr = iCtr + 1
        Loop
        iCtr = 0
        CreateMainTable = dtTemp
    End Function

    Private Function CreateMainTableCpn(ByVal v_dtTemp As DataTable, ByVal dTotalNo_Adt As Decimal, _
                                      ByVal dTotalNo_Chd As Decimal, ByVal dTotalNo_inf As Decimal, ByVal Trip As String, ByVal TourType As String, ByVal sAgentId As String, ByVal sDistr As String, ByVal searchValue As String, ByVal sOrigin As String, ByVal sDestination As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal sGUID As String) As DataTable
        Dim dtTemp As New DataTable
        Dim row As DataRow
        Dim iCtr As Integer = "0"
        Conn.Open()

        'dtTemp = objDBHelper.CreateMainDataTable()
        dtTemp = objDBHelper.ResultTable()
        'Calculation For AgentMarkUp'
        Dim dtAgentMarkup As New DataTable
        dtAgentMarkup = GetMarkUp(sAgentId, sDistr, Trip, "TA")
        'Calculation For AdminMarkUp'
        Dim dtAdminMarkup As New DataTable
        dtAdminMarkup = GetMarkUp(sAgentId, sDistr, Trip, "AD")

        Do While iCtr <= v_dtTemp.Rows.Count - 1
            If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                If dTotalNo_Adt <> 0 And dTotalNo_Chd <> 0 And dTotalNo_inf <> 0 Then
                    If v_dtTemp.Rows(iCtr).Item("FareID_Adt") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Chd") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Inf") <> "" Then
                        row = dtTemp.NewRow()

                        '//new entry
                        row("TripType") = "O"
                        row("OrgDestFrom") = sOrigin
                        row("OrgDestTo") = sDestination
                        row("Sector") = sOrigin & ":" & sDestination
                        row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                        row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                        row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                        row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                        row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                        row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                        row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                        row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                        row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                        row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                        row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                        row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                        row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                        row("AirLineName") = sAirline
                        row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                        row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                        row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                        row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                        row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                        row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                        row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                        row("FBPaxType") = "ADT"
                        row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                        row("Searchvalue") = searchValue.ToString
                        row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                        row("Adult") = dTotalNo_Adt
                        row("Child") = dTotalNo_Chd
                        row("Infant") = dTotalNo_inf
                        row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                        row("Flight") = "1"
                        row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                        row("Trip") = TourType.ToString
                        row("TripCnt") = tCnt
                        row("Currency") = "INR"
                        row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                        row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper 'v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                        'Calculation of Transaction details
                        Dim totBFWInf As Double = 0
                        Dim totBFWOInf As Double = 0
                        Dim totFS As Double = 0
                        Dim totTax As Double = 0
                        Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                        Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                        Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                        totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                        totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                        totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                        totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                        'Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                        Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt"), v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt"), v_dtTemp.Rows(iCtr).Item("FUEL_Adt"), Trip)
                        AStax = HsTblSTax("STax") * dTotalNo_Adt
                        ATF = HsTblSTax("TF") * dTotalNo_Adt
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd"), v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd"), v_dtTemp.Rows(iCtr).Item("FUEL_Chd"), Trip)
                        CStax = HsTblSTax("STax") * dTotalNo_Chd
                        CTF = HsTblSTax("TF") * dTotalNo_Chd
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf"), 0, 0, Trip)
                        IStax = HsTblSTax("STax") * dTotalNo_inf
                        ITF = HsTblSTax("TF") * dTotalNo_inf




                        ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                        If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                        ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                        If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                        Dim totMrk As Double = 0

                        'Calculation of Transaction details end

                        If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                            row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                            row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                            row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                            row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                            row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                            row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                            row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                            row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                            row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                            row("TFee") = 0 ''ATF + CTF + ITF 'HsTblSTax("TF")
                            totMrk = ADTAdminMrk * dTotalNo_Adt
                            totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                            totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                            totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                            row("ADTAdminMrk") = ADTAdminMrk
                            row("CHDAdminMrk") = CHDAdminMrk
                        Else
                            row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                            row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                            row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                            row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                            row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                            row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                            row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                            row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                            row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                            row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                            'totMrk = ADTAdminMrk * dTotalNo_Adt
                            totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                            'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                            totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                            row("ADTAdminMrk") = 0
                            row("CHDAdminMrk") = 0
                        End If
                        'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                        row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                        'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                        row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                        'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                        row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                        row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                        'row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                        row("IATAComm") = HsTblSTax("IATAComm")
                        row("ADTAgentMrk") = ADTAgentMrk
                        row("CHDAgentMrk") = CHDAgentMrk
                        'row("ADTAdminMrk") = ADTAdminMrk
                        'row("CHDAdminMrk") = CHDAdminMrk
                        row("TotalBfare") = totBFWInf
                        row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                        row("TotalTax") = totTax
                        row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                        row("DisCount") = "0"
                        row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                        row("OriginalTT") = totTax
                        row("Track_id") = TrackId
                        row("FType") = Ft
                        '//
                        'row.Item("Cabin") = v_dtTemp.Rows(iCtr).Item("Cabin")
                        dtTemp.Rows.Add(row)
                    End If
                ElseIf dTotalNo_Adt <> 0 And dTotalNo_inf <> 0 Then
                    If v_dtTemp.Rows(iCtr).Item("FareID_Adt") <> "" And v_dtTemp.Rows(iCtr).Item("FareID_Inf") <> "" Then
                        row = dtTemp.NewRow()
                        '//new entry
                        row("TripType") = "O"
                        row("OrgDestFrom") = sOrigin
                        row("OrgDestTo") = sDestination
                        row("Sector") = sOrigin & ":" & sDestination
                        row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                        row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                        row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                        row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                        row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                        row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                        row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                        row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                        row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                        row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                        row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                        row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                        row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                        row("AirLineName") = sAirline
                        row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                        row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                        row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                        row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                        row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                        row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                        row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                        row("FBPaxType") = "ADT"
                        row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                        row("Searchvalue") = searchValue.ToString
                        row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                        row("Adult") = dTotalNo_Adt
                        row("Child") = dTotalNo_Chd
                        row("Infant") = dTotalNo_inf
                        row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                        row("Flight") = "1"
                        row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                        row("Trip") = TourType.ToString
                        row("TripCnt") = tCnt
                        row("Currency") = "INR"
                        row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                        row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper 'v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                        'Calculation of Transaction details
                        Dim totBFWInf As Double = 0
                        Dim totBFWOInf As Double = 0
                        Dim totFS As Double = 0
                        Dim totTax As Double = 0
                        Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0

                        totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                        totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                        totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                        totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                        Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)

                        ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                        If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                        ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                        If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                        Dim totMrk As Double = 0

                        'Calculation of Transaction details end

                        If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                            row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                            row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                            row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                            row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                            row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                            row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                            row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                            row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                            row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                            row("TFee") = 0 ''HsTblSTax("TF")
                            totMrk = ADTAdminMrk * dTotalNo_Adt
                            totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                            totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                            totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                            row("ADTAdminMrk") = ADTAdminMrk
                            row("CHDAdminMrk") = CHDAdminMrk
                        Else
                            row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                            row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                            row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                            row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                            row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                            row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                            row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                            row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                            row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                            row("TFee") = HsTblSTax("TF")
                            'totMrk = ADTAdminMrk * dTotalNo_Adt
                            totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                            'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                            totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                            row("ADTAdminMrk") = 0
                            row("CHDAdminMrk") = 0
                        End If
                        'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                        row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                        'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                        row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                        'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                        row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                        row("STax") = HsTblSTax("STax")
                        'row("TFee") = HsTblSTax("TF")
                        row("IATAComm") = HsTblSTax("IATAComm")
                        row("ADTAgentMrk") = ADTAgentMrk
                        row("CHDAgentMrk") = CHDAgentMrk
                        'row("ADTAdminMrk") = ADTAdminMrk
                        'row("CHDAdminMrk") = CHDAdminMrk
                        row("TotalBfare") = totBFWInf
                        row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                        row("TotalTax") = totTax
                        row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                        row("DisCount") = "0"
                        row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                        row("OriginalTT") = totTax
                        row("Track_id") = TrackId
                        row("FType") = Ft
                        dtTemp.Rows.Add(row)
                    End If
                ElseIf dTotalNo_Adt <> 0 Then
                    row = dtTemp.NewRow()
                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = sOrigin
                    row("OrgDestTo") = sDestination
                    row("Sector") = sOrigin & ":" & sDestination
                    row("DepartureLocation") = v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim
                    row("ArrivalLocation") = v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim
                    row("DepartureCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Origin").ToString.Trim)
                    row("ArrivalCityName") = city_name(v_dtTemp.Rows(iCtr).Item("Destination").ToString.Trim)
                    row("depdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("DepDate"), 10)
                    row("arrdatelcc") = Left(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 10)
                    row("DepartureDate") = Right(row("depdatelcc"), 2) & Mid(row("depdatelcc"), 6, 2) & Mid(row("depdatelcc"), 3, 2)
                    row("Departure_Date") = Right(row("depdatelcc"), 2) & " " & datecon(Mid(row("depdatelcc"), 6, 2))
                    row("DepartureTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("DepDate"), 8), 5).Replace(":", "")
                    row("ArrivalDate") = Right(row("arrdatelcc"), 2) & Mid(row("arrdatelcc"), 6, 2) & Mid(row("arrdatelcc"), 3, 2)
                    row("Arrival_Date") = Right(row("arrdatelcc"), 2) & " " & datecon(Mid(row("arrdatelcc"), 6, 2))
                    row("ArrivalTime") = Left(Right(v_dtTemp.Rows(iCtr).Item("ArrivalDate"), 8), 5).Replace(":", "")
                    row("MarketingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("AirLineName") = sAirline
                    row("FlightIdentification") = v_dtTemp.Rows(iCtr).Item("FlightNo").ToString
                    row("RBD") = v_dtTemp.Rows(iCtr).Item("FCCode_Adt").ToString & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FCCode_Inf")
                    row("AvailableSeats") = v_dtTemp.Rows(iCtr).Item("SeatAvail").ToString
                    row("ValiDatingCarrier") = v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim
                    row("EQ") = "" 'dtAvail.Rows(i)("AircraftType").ToString

                    row("Stops") = v_dtTemp.Rows(iCtr).Item("Stops").ToString & "-Stop"
                    row("fareBasis") = v_dtTemp.Rows(iCtr).Item("FBCode_Adt").ToString
                    row("FBPaxType") = "ADT"
                    row("LineItemNumber") = v_dtTemp.Rows(iCtr).Item("Flt_cnt").ToString
                    row("Searchvalue") = searchValue.ToString
                    row("TotPax") = dTotalNo_Adt + dTotalNo_Chd
                    row("Adult") = dTotalNo_Adt
                    row("Child") = dTotalNo_Chd
                    row("Infant") = dTotalNo_inf
                    row("Leg") = v_dtTemp.Rows(iCtr).Item("Leg")
                    row("Flight") = "1"
                    row("Tot_Dur") = v_dtTemp.Rows(iCtr).Item("FlightTime")
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper
                    row("sno") = v_dtTemp.Rows(iCtr).Item("FareID_Adt") & ":" & sGUID & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper ' v_dtTemp.Rows(iCtr).Item("FareID_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FareID_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Chd") & ":" & v_dtTemp.Rows(iCtr).Item("FBCode_Inf") & ":" & v_dtTemp.Rows(iCtr).Item("FareTypeName")


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0

                    totBFWInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                    totBFWOInf = (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd)
                    totTax = (v_dtTemp.Rows(iCtr).Item("Adt_Tax") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("Chd_Tax") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("Inf_Tax") * dTotalNo_inf)
                    Dim HsTblSTax As Hashtable = ServiceTax(v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)

                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr).Item("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Adt"), Trip)
                    If dTotalNo_Chd > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, v_dtTemp.Rows(iCtr)("CarrierCode").ToString.Trim, v_dtTemp.Rows(iCtr).Item("Total_Chd"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0

                    'Calculation of Transaction details end

                    If v_dtTemp.Rows(iCtr).Item("FareTypeName").ToString.Trim.ToUpper = "GOSPECIAL" Then
                        row("AdtBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")) + Val(v_dtTemp.Rows(iCtr).Item("Adt_Tax"))
                        row("ChdBfare") = 0 'v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = 0 ' v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")) + Val(v_dtTemp.Rows(iCtr).Item("Chd_Tax"))
                        row("InfBfare") = 0 ' v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = 0 'v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = Val(v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")) + Val(v_dtTemp.Rows(iCtr).Item("Inf_Tax"))
                        row("TFee") = 0 ''HsTblSTax("TF")
                        totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = ADTAdminMrk
                        row("CHDAdminMrk") = CHDAdminMrk
                    Else
                        row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                        row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                        row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                        row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                        row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                        row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                        row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                        row("TFee") = HsTblSTax("TF")
                        'totMrk = ADTAdminMrk * dTotalNo_Adt
                        totMrk = totMrk + ADTAgentMrk * dTotalNo_Adt
                        'totMrk = totMrk + CHDAdminMrk * dTotalNo_Chd
                        totMrk = totMrk + CHDAgentMrk * dTotalNo_Chd
                        row("ADTAdminMrk") = 0
                        row("CHDAdminMrk") = 0
                    End If
                    'row("AdtBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Adt")
                    'row("AdtFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Adt")
                    'row("AdtTax") = v_dtTemp.Rows(iCtr).Item("Adt_Tax")
                    row("AdtFare") = v_dtTemp.Rows(iCtr).Item("Total_Adt")
                    'row("ChdBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Chd")
                    'row("ChdFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Chd")
                    'row("ChdTax") = v_dtTemp.Rows(iCtr).Item("Chd_Tax")
                    row("ChdFare") = v_dtTemp.Rows(iCtr).Item("Total_Chd")
                    'row("InfBfare") = v_dtTemp.Rows(iCtr).Item("BaseFareAmt_Inf")
                    'row("InfFSur") = v_dtTemp.Rows(iCtr).Item("FUEL_Inf")
                    'row("InfTax") = v_dtTemp.Rows(iCtr).Item("Inf_Tax")
                    row("InfFare") = v_dtTemp.Rows(iCtr).Item("Total_Inf")
                    row("STax") = HsTblSTax("STax")
                    'row("TFee") = HsTblSTax("TF")
                    row("IATAComm") = HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    'row("ADTAdminMrk") = ADTAdminMrk
                    'row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (v_dtTemp.Rows(iCtr).Item("FUEL_Adt") * dTotalNo_Adt) + (v_dtTemp.Rows(iCtr).Item("FUEL_Chd") * dTotalNo_Chd) + (v_dtTemp.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                    row("DisCount") = "0"
                    row("OriginalTF") = v_dtTemp.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    dtTemp.Rows.Add(row)
                End If
            End If
            iCtr = iCtr + 1
        Loop
        iCtr = 0
        CreateMainTableCpn = dtTemp
    End Function

    Private Function BookRequest(ByVal sIATA As String, ByVal sGUID As String, ByVal sAddress1 As String, _
                                      ByVal sAddress2 As String, ByVal sCity As String, _
                                      ByVal sState As String, ByVal sPostal As String, _
                                      ByVal sCountry As String, ByVal sContactValue As String, _
                                      ByVal sCCurrency As String, ByVal sDCurrency As String, _
                                      ByVal sEmailId As String, ByVal sFax As String, _
                                      ByVal sMobile As String, ByVal sUser As String, _
                                      ByRef custinfo As Hashtable, _
                                      ByVal sContactType As String, ByVal sContactNum As String, _
                                      ByVal sEmail As String, ByVal sPassMob As String, ByVal sPassAddress1 As String, ByVal sPassAddress2 As String, _
                                      ByVal sPassCity As String, ByVal sPassPostal As String, _
                                      ByVal sPassState As String, ByVal sPassCountry As String, ByVal sFareInformationID As String, ByVal dNo_Adt As Decimal, ByVal dNo_Chd As Decimal, ByVal dNo_Inf As Decimal) As String
        Dim sPromoCode As String = ""
        Dim myXML As String = ""
        Dim iCtr As Integer = 1
        myXML += "<?xml version='1.0' encoding='utf-8' standalone='no'?>"
        myXML += "<RABookReqV3DS xmlns='http://services.radixx/WSRadixx/RABookReqV3DS.xsd'>"
        myXML += "<Reservation>"
        myXML += "<Address>" & sAddress1 & "</Address>"
        myXML += "<Address2>" & sAddress2 & "</Address2>"
        myXML += "<City>" & sCity & "</City>"
        myXML += "<State>" & sState & "</State>"
        myXML += "<Postal>" & sPostal & "</Postal>"
        myXML += "<Country>" & sCountry & "</Country>"
        myXML += "<HomePhone>" & sContactValue & "</HomePhone>"
        myXML += "<CarrierCurrency>" & sCCurrency & "</CarrierCurrency>"
        myXML += "<DisplayCurrency>" & sDCurrency & "</DisplayCurrency>"
        myXML += "<Email>" & sEmailId & "</Email>"
        myXML += "<Fax>" & sFax & "</Fax>"
        myXML += "<Mobile>" & sMobile & "</Mobile>"
        myXML += "<IATANum>" & sIATA & "</IATANum>"
        myXML += "<WebBookingID>" & sGUID & "</WebBookingID>"
        myXML += "<User>" & sUser & "</User>"
        myXML += "<PromoCode>" & sPromoCode & "</PromoCode>"
        If dNo_Adt > 0 Then
            myXML += "<Segment>"
            myXML += "<FareInformationID>" & sFareInformationID & "</FareInformationID>"
            myXML += "<MarketingCode />"
            myXML += "</Segment>"
        End If
        If dNo_Adt > 0 Then
            Do While iCtr <= dNo_Adt
                myXML += "<Person>"
                myXML += "<LastName>" & custinfo("LnameADT" & iCtr) & "</LastName>"
                myXML += "<FirstName>" & custinfo("FNameADT" & iCtr) & "</FirstName>"
                myXML += "<Title>" & custinfo("Title_ADT" & iCtr) & "</Title>"
                myXML += "<PassengerAge>" & custinfo("ADTAge" & iCtr) & "</PassengerAge>"
                myXML += "<PTCID>1</PTCID>"
                myXML += "<ContactType>" & sContactType & "</ContactType>"
                myXML += "<ContactNum>" & sContactNum & "</ContactNum>"
                If iCtr = 1 Then
                    myXML += "<Mobile>" & sPassMob & "</Mobile>"
                    myXML += "<Email>" & sEmail & "</Email>"
                Else
                    myXML += "<Mobile />"
                    myXML += "<Email />"
                End If
                myXML += "<BusinessPhone />"
                myXML += "<Address />"
                myXML += "<Address2 />"
                myXML += "<City />"
                myXML += "<Postal />"
                myXML += "<Country />"
                myXML += "</Person>"
                iCtr = iCtr + 1
            Loop
            iCtr = 1
        End If
        If dNo_Chd > 0 Then
            Do While iCtr <= dNo_Chd
                myXML += "<Person>"
                myXML += "<LastName>" & custinfo("LnameCHD" & iCtr) & "</LastName>"
                myXML += "<FirstName>" & custinfo("FNameCHD" & iCtr) & "</FirstName>"
                myXML += "<Title>" & custinfo("Title_CHD" & iCtr) & "</Title>"
                myXML += "<PassengerAge>" & custinfo("CHDAge" & iCtr) & "</PassengerAge>"
                myXML += "<PTCID>6</PTCID>"
                myXML += "<ContactType>" & sContactType & "</ContactType>"
                myXML += "<ContactNum>" & sContactNum & "</ContactNum>"
                myXML += "<Mobile />"
                myXML += "<Email />"
                myXML += "<BusinessPhone />"
                myXML += "<Address />"
                myXML += "<Address2 />"
                myXML += "<City />"
                myXML += "<Postal />"
                myXML += "<Country />"
                myXML += "</Person>"
                iCtr = iCtr + 1
            Loop
            iCtr = 1
        End If
        If dNo_Inf > 0 Then
            Do While iCtr <= dNo_Inf
                myXML += "<Person>"
                myXML += "<LastName>" & custinfo("LnameINF" & iCtr) & "</LastName>"
                myXML += "<FirstName>" & custinfo("FNameINF" & iCtr) & "</FirstName>"
                myXML += "<Title>" & custinfo("Title_INF" & iCtr) & "</Title>"
                myXML += "<PassengerAge>" & custinfo("INFAge" & iCtr) & "</PassengerAge>"
                myXML += "<PTCID>5</PTCID>"
                myXML += "<ContactType>" & sContactType & "</ContactType>"
                myXML += "<ContactNum>" & sContactNum & "</ContactNum>"
                myXML += "<Mobile />"
                myXML += "<Email />"
                myXML += "<BusinessPhone />"
                myXML += "<Address />"
                myXML += "<Address2 />"
                myXML += "<City />"
                myXML += "<Postal />"
                myXML += "<Country />"
                myXML += "</Person>"
                iCtr = iCtr + 1
            Loop
            iCtr = 1
        End If
        myXML += "</Reservation>"
        myXML += "</RABookReqV3DS>"
        BookRequest = myXML
    End Function

    Public Function GetBookingDetails(ByVal sGUID As String, ByVal sAddress1 As String, _
                                      ByVal sAddress2 As String, ByVal sCity As String, _
                                      ByVal sState As String, ByVal sPostal As String, _
                                      ByVal sCountry As String, ByVal sContactValue As String, _
                                      ByVal sCCurrency As String, ByVal sDCurrency As String, _
                                      ByVal sEmailId As String, ByVal sFax As String, _
                                      ByVal sMobile As String, _
                                      ByRef custinfo As Hashtable, _
                                      ByVal sContactType As String, ByVal sContactNum As String, _
                                      ByVal sEmail As String, ByVal sPassMob As String, ByVal sPassAddress1 As String, ByVal sPassAddress2 As String, _
                                      ByVal sPassCity As String, ByVal sPassPostal As String, _
                                      ByVal sPassState As String, ByVal sPassCountry As String, ByVal sFareInformationID As String, ByVal dNo_Adt As Decimal, ByVal dNo_Chd As Decimal, ByVal dNo_Inf As Decimal, ByVal dTotalAmount As Decimal, ByVal CorporateId As String, ByVal UserId As String) As DataTable
        Dim sGetSummary As String = ""
        Dim dAmount As Decimal = 0
        Dim sCriteria As String = ""
        Dim dtGetCommSumm As DataTable
        Dim dtSaveResBOXml As DataTable
        sCriteria = BookRequest(CorporateId, sGUID, sAddress1, sAddress2, _
                                sCity, sState, sPostal, sCountry, sContactValue, _
                                sCCurrency, sDCurrency, sEmailId, sFax, sMobile, _
                                UserId, custinfo, _
                                sContactType, sContactNum, _
                                sEmail, sPassMob, sPassAddress1, sPassAddress2, _
                                sPassCity, sPassPostal, sPassState, sPassCountry, sFareInformationID, dNo_Adt, dNo_Chd, dNo_Inf)
        objRadixxBooking.GetApplicableTransactionFeesExtendedXML(sGUID, "INR")
        sGetSummary = objRadixxBooking.GetSummary(sGUID, sCriteria)
        Try
            ' booklog("G8", sCriteria, sGetSummary)
        Catch ex As Exception
        End Try
        dAmount = GetPayment(sGetSummary)
        If dTotalAmount = dAmount Then
            dtGetCommSumm = GetCommitSummary(sGUID)
            dtSaveResBOXml = GetPayBOXML(sGUID, dtGetCommSumm, dTotalAmount, sAddress1, sAddress2, sCity, sCountry, sPostal, custinfo, sFareInformationID, sCriteria, sGetSummary)
            Return dtSaveResBOXml
        Else
            Return GetDataTableSave(sCriteria, sGetSummary, "", "", sGetSummary, dTotalAmount, sFareInformationID)
        End If
    End Function

    Private Function AddPaymentXML(ByVal sAddress1 As String, _
                                      ByVal sAddress2 As String, ByVal dAmount As Decimal, _
                                      ByVal sCity As String, _
                                      ByVal sCountry As String, ByVal sPaidDate As String, _
                                      ByVal sPostal As String, ByVal sVoucherNo As String, _
                                      ByVal sLastName_Adt As String, ByVal sFirstName_Adt As String) As String
        Dim myXML As String = ""
        myXML += "<?xml version='1.0' encoding='UTF-8'?>"
        myXML += "<Payments>"
        myXML += "<Payment>"
        myXML += "<Address>" & sAddress1 & "</Address>"
        myXML += "<Address2>" & sAddress2 & "</Address2>"
        myXML += "<BaseAmount>" & dAmount & "</BaseAmount>"
        myXML += "<BaseCurrency>INR</BaseCurrency>"
        'myXML += "<CardHolder>Test Cleartrip</CardHolder>"
        'myXML += "<CardNumber>345678901234564</CardNumber>"
        'myXML += "<CheckNumber>" & sCheckNo & "</CheckNumber>"
        myXML += "<City>" & sCity & "</City>"
        myXML += "<CompanyName>RWT</CompanyName>"
        myXML += "<Country>" & sCountry & "</Country>"
        myXML += "<CurrencyPaid>INR</CurrencyPaid>"
        'myXML += "<CVCode>" & sCVCode & "</CVCode>"
        myXML += "<DatePaid>" & sPaidDate & "</DatePaid>"
        myXML += "<DocumentReceivedBy/>"
        'myXML += "<ExpirationDate>" & sExpDate & "</ExpirationDate>"
        myXML += "<ExchangeRate>1</ExchangeRate>"
        myXML += "<ExchangeRateDate>" & sPaidDate & "</ExchangeRateDate>"
        myXML += "<FFNumber>0</FFNumber>"
        myXML += "<FirstName>" & sFirstName_Adt & "</FirstName>"
        myXML += "<LastName>" & sLastName_Adt & "</LastName>"
        myXML += "<PaymentComment/>"
        myXML += "<PaymentAmount>" & dAmount & "</PaymentAmount>"
        myXML += "<PaymentMethod>INVC</PaymentMethod>"
        myXML += "<Postal>" & sPostal & "</Postal>"
        myXML += "<Reference>REF1</Reference>"
        myXML += "<State>Delhi</State>"
        myXML += "<TerminalID>0</TerminalID>"
        myXML += "<UserData/>"
        myXML += "<ValueCode/>"
        myXML += "<VoucherNumber>" & sVoucherNo & "</VoucherNumber>"
        myXML += "<GcxID/>"
        myXML += "<GcxOptOption/>"
        myXML += "<OriginalCurrency>INR</OriginalCurrency>"
        myXML += "<OriginalAmount>" & dAmount & "</OriginalAmount>"
        myXML += "</Payment>"
        myXML += "</Payments>"
        AddPaymentXML = myXML
    End Function

    Private Function GetCommitSummary(ByVal sGUID As String) As DataTable
        Dim sCommSummary As String
        sCommSummary = objRadixxBooking.CommitSummary(sGUID)
        GetCommitSummary = GetDataTableSummery(sCommSummary)
    End Function

    Private Function GetPayBOXML(ByVal sGUID As String, ByVal dtTemp As DataTable, ByVal dTotalPaidAmt As Decimal, ByVal sAddress1 As String, _
                                      ByVal sAddress2 As String, _
                                      ByVal sCity As String, _
                                      ByVal sCountry As String, ByVal sPostal As String, _
                                      ByRef custinfo As Hashtable, ByVal sFareInformationID As String, ByVal BookReq As String, ByVal BookRes As String) As DataTable
        Dim sSerialNo As String = ""
        Dim sConfirmNo As String = ""
        Dim sVoucherNo As String = ""
        Dim sPayXML As String
        Dim sPaidDate As String = Format(Today, "yyyy-MM-dd")
        Dim sGetPayBOXML As String
        Dim sSaveResBOXml As String
        Dim sLName As String = custinfo("LName")
        Dim sFName As String = custinfo("FName")
        sSerialNo = dtTemp.Rows(0)("SerialNo").ToString
        sConfirmNo = dtTemp.Rows(0)("ConfirmationNo").ToString
        sVoucherNo = dtTemp.Rows(0)("VoucherNo").ToString
        sPayXML = AddPaymentXML(sAddress1, sAddress2, dTotalPaidAmt, sCity, sCountry, sPaidDate, sPostal, sVoucherNo, sLName, sFName)
        sGetPayBOXML = objRadixxBooking.ResAddPayment(sGUID, sSerialNo, sConfirmNo, sPayXML)
        sSaveResBOXml = GetSaveBOXML(sGUID, sSerialNo, sConfirmNo)
        GetPayBOXML = GetDataTableSave(BookReq, BookRes, sPayXML, sGetPayBOXML, sSaveResBOXml, dTotalPaidAmt, sFareInformationID)
    End Function

    Private Function GetSaveBOXML(ByVal sGUID As String, ByVal sSerialNo As String, ByVal sConfirmNo As String) As String
        Dim sGetSaveBOXML As String
        sGetSaveBOXML = objRadixxBooking.ResSave(sGUID, sSerialNo, sConfirmNo)
        GetSaveBOXML = sGetSaveBOXML
    End Function

    Private Function GetDataTableSummery(ByVal sXML As String) As DataTable
        Dim dtTable As New DataTable
        Dim row As DataRow
        Dim sFareAmount As String = ""
        Dim sPromoID As String = ""
        Dim sProfileID As String = ""
        Dim sRecieptLanguageID As String = ""
        Dim sSerial As String = ""
        Dim sConfirmation As String = ""
        Dim sVoucherNo As String = ""
        Dim xmlDoc As New XmlDocument
        Dim iCounter As Integer = "0"
        xmlDoc.LoadXml(sXML)
        dtTable = objDBHelper.CreateSummeryDataTable
        If xmlDoc.DocumentElement.ChildNodes.ItemOf(0).Name = "Reservation" Then
            Do While iCounter <= xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "SeriesNumber" Then
                    sSerial = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ConfirmationNumber" Then
                    sConfirmation = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "PromotionalID" Then
                    sPromoID = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ProfileID" Then
                    sProfileID = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "RecieptLanguageID" Then
                    sRecieptLanguageID = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                iCounter = iCounter + 1
            Loop
            iCounter = 0
        End If
        Try
            '***************New Code For Voucher Number Start****************
            For i As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes.Count - 1
                If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).Name = "Physical_Flight" Then
                    For ii As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes.Count - 1
                        If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).Name = "Customers" Then
                            For iii As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes.Count - 1
                                If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).Name = "Customer" Then
                                    For c As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes.Count - 1
                                        If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).Name = "AirLine_Persons" Then
                                            For al As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes.Count - 1
                                                If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).Name = "AirLine_Person" Then
                                                    For al1 As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes.Count - 1
                                                        If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).Name = "Charges" Then
                                                            For ch As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).ChildNodes.Count - 1
                                                                If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).ChildNodes(ch).Name = "Charge" Then
                                                                    For ch1 As Integer = 0 To xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).ChildNodes(ch).ChildNodes.Count - 1
                                                                        If xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).ChildNodes(ch).ChildNodes(ch1).Name = "VoucherNumber" Then
                                                                            sVoucherNo = xmlDoc.GetElementsByTagName("Physical_Flights")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(c).ChildNodes(al).ChildNodes(al1).ChildNodes(ch).ChildNodes(ch1).InnerText
                                                                            GoTo 1
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
1:
            '***************New Code For Voucher Number End****************
        Catch ex As Exception
            sVoucherNo = ""
        End Try


        'If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(36).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(29).ChildNodes.ItemOf(0).ChildNodes.ItemOf(27).ChildNodes.ItemOf(0).ChildNodes.ItemOf(4).ChildNodes.ItemOf(0).ChildNodes.ItemOf(56).ChildNodes.ItemOf(0).ChildNodes.ItemOf(10).Name = "VoucherNumber" Then
        '    sVoucherNo = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(36).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(29).ChildNodes.ItemOf(0).ChildNodes.ItemOf(27).ChildNodes.ItemOf(0).ChildNodes.ItemOf(4).ChildNodes.ItemOf(0).ChildNodes.ItemOf(56).ChildNodes.ItemOf(0).ChildNodes.ItemOf(10).ChildNodes.ItemOf(0).Value
        'End If
        row = dtTable.NewRow()
        row("SerialNo") = sSerial
        row("ConfirmationNo") = sConfirmation
        row("PromotionalID") = sPromoID
        row("RecieptLanguageID") = sRecieptLanguageID
        row("ProfileID") = sProfileID
        row("VoucherNo") = sVoucherNo
        row("FareAmount") = sFareAmount
        dtTable.Rows.Add(row)
        GetDataTableSummery = dtTable
    End Function

    Private Function GetDataTableSave(ByVal BookReq As String, ByVal BookRes As String, ByVal PaymentReq As String, ByVal PaymentRes As String, ByVal sXML As String, ByVal dTotalAmount As Decimal, ByVal sFareInformationID As String) As DataTable
        Dim dtTable As New DataTable
        Dim row As DataRow
        Dim sFareAmount As String = ""
        Dim sSerial As String = ""
        Dim sConfirmation As String = ""
        Dim sBookingAgent As String = ""
        Dim sBookDate As String = ""
        Dim sPassengerTitle As String = ""
        Dim sPassengerFName As String = ""
        Dim sPassengerFName1 As String = ""
        Dim sPassengerLName As String = ""
        Dim sPassengerAge As String = ""
        Dim sCabin As String = ""
        Dim dReservationBalance As Decimal = 0
        Dim sOrigin As String = ""
        Dim sDestination As String = ""
        Dim sDept_Date As String = ""
        Dim sDept_Time As String = ""
        Dim sArr_Date As String = ""
        Dim sArr_Time As String = ""
        Dim xmlDoc As New XmlDocument
        Dim iCounter As Integer = 0
        Dim iCtr As Integer = 0
        Dim iCtr1 As Integer = 0
        Dim iCount As Integer = 0
        dtTable = objDBHelper.CreateSaveDataTable
        Try
            xmlDoc.LoadXml(sXML)
            iCount = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.Count - 1
            If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).Name = "Airlines" Then
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(6).Name = "Origin" Then
                    sOrigin = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(6).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(8).Name = "Destination" Then
                    sDestination = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(8).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(10).Name = "DepartureTime" Then
                    sDept_Date = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(10).ChildNodes.ItemOf(0).Value
                End If
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(11).Name = "Arrivaltime" Then
                    sArr_Date = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCount.ToString).ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(11).ChildNodes.ItemOf(0).Value
                End If
            End If
            If xmlDoc.DocumentElement.ChildNodes.ItemOf(0).Name = "Reservation" Then
                Do While iCounter <= xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "SeriesNumber" Then
                        sSerial = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ConfirmationNumber" Then
                        sConfirmation = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "BookingAgent" Then
                        sBookingAgent = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "BookDate" Then
                        sBookDate = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "Cabin" Then
                        sCabin = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ReservationBalance" Then
                        dReservationBalance = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                    End If
                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ReservationContacts" Then
                        Do While iCtr1 <= xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.Count - 1
                            If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).Name = "ReservationContact" Then
                                Do While iCtr <= xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.Count - 1
                                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).Name = "Title" Then
                                        If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).ChildNodes.Count <> 0 Then
                                            sPassengerTitle = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).Value
                                        End If
                                    End If
                                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).Name = "FirstName" Then
                                        sPassengerFName = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).Value
                                        If sPassengerFName = sPassengerFName1 Then
                                            GoTo 1
                                        End If
                                    End If
                                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).Name = "LastName" Then
                                        sPassengerLName = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).Value
                                    End If
                                    If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).Name = "Age" Then
                                        sPassengerAge = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(iCtr1).ChildNodes.ItemOf(iCtr).ChildNodes.ItemOf(0).Value
                                    End If
                                    iCtr = iCtr + 1
                                Loop
                                iCtr = 0
                            End If
                            row = dtTable.NewRow()
                            row("SerialNo") = sSerial
                            row("ConfirmationNo") = sConfirmation
                            row("BookingAgent") = sBookingAgent
                            row("BookDate") = sBookDate
                            row("PassengerTitle") = sPassengerTitle
                            row("PassengerFName") = sPassengerFName
                            row("PassengerLName") = sPassengerLName
                            row("PassengerAge") = sPassengerAge
                            row("Cabin") = sCabin
                            row("Origin") = sOrigin
                            row("Destination") = sDestination
                            row("Dept_Date") = "" 'Left(sDept_Date, 10)
                            row("Dept_Time") = "" 'Right(sDept_Date, 8)
                            row("Arr_Date") = "" 'Left(sArr_Date, 10)
                            row("Arr_Time") = "" 'Right(sArr_Date, 8)
                            row("ReservationBalance") = dReservationBalance
                            row("Total_Fare") = dTotalAmount
                            row("Fare_ID") = sFareInformationID
                            If dReservationBalance <> 0 Then
                                row("Message") = "Paid amount is not correct"
                            Else
                                row("Message") = ""
                            End If
                            row("BookReq") = BookReq
                            row("BookRes") = BookRes
                            row("AddPayReq") = PaymentReq
                            row("AddPayRes") = PaymentRes
                            row("ConfirmPayRes") = sXML
                            dtTable.Rows.Add(row)
                            sPassengerFName1 = sPassengerFName
1:
                            iCtr1 = iCtr1 + 1
                        Loop
                        iCtr1 = 0
                    End If
                    iCounter = iCounter + 1
                Loop
                iCounter = 0
            End If
        Catch ex As Exception
            Try
                row = dtTable.NewRow()
                row("SerialNo") = sSerial
                row("ConfirmationNo") = sConfirmation
                row("BookingAgent") = sBookingAgent
                row("BookDate") = sBookDate
                row("PassengerTitle") = sPassengerTitle
                row("PassengerFName") = sPassengerFName
                row("PassengerLName") = sPassengerLName
                row("PassengerAge") = sPassengerAge
                row("Cabin") = sCabin
                row("Origin") = sOrigin
                row("Destination") = sDestination
                row("Dept_Date") = Left(sDept_Date, 10)
                row("Dept_Time") = Right(sDept_Date, 8)
                row("Arr_Date") = Left(sArr_Date, 10)
                row("Arr_Time") = Right(sArr_Date, 8)
                row("ReservationBalance") = dReservationBalance
                row("Total_Fare") = dTotalAmount
                row("Fare_ID") = sFareInformationID
                If dReservationBalance <> 0 Then
                    row("Message") = "Paid amount is not correct"
                Else
                    row("Message") = ""
                End If
                row("BookReq") = BookReq
                row("BookRes") = BookRes
                row("AddPayReq") = PaymentReq
                row("AddPayRes") = PaymentRes
                row("ConfirmPayRes") = sXML
                dtTable.Rows.Add(row)
            Catch ex1 As Exception
            End Try
        End Try
        GetDataTableSave = dtTable
    End Function

    Private Function GetPayment(ByVal sXML As String) As Decimal
        Dim xmlDoc As New XmlDocument
        Dim dPayment As Decimal = 0
        Dim iCounter As Integer = 0
        xmlDoc.LoadXml(sXML)
        If xmlDoc.DocumentElement.ChildNodes.ItemOf(0).Name = "Reservation" Then
            Do While iCounter <= xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.Count - 1
                If xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).Name = "ReservationBalance" Then
                    dPayment = xmlDoc.ChildNodes.ItemOf(0).ChildNodes.ItemOf(0).ChildNodes.ItemOf(iCounter.ToString).ChildNodes.ItemOf(0).Value
                End If
                iCounter = iCounter + 1
            Loop
        End If
        GetPayment = dPayment
    End Function

    Private Sub Init()
        dFuel_Adt = 0
        dPSF_Adt = 0
        dUDF_Adt = 0
        dPHF_Adt = 0
        sFuel_Def = ""
        dST_Adt = 0
        sAdt_FareID = ""
        dAdt_Basic = 0
        sAdt_FC = ""
        sAdt_FC = ""
        dFuel_Inf = 0
        dPSF_Inf = 0
        dUDF_Inf = 0
        dPHF_Inf = 0
        dST_Inf = 0
        sInf_FareID = ""
        dInf_Basic = 0
        sInf_FC = ""
        sInf_FC = ""
        dFuel_Chd = 0
        dPSF_Chd = 0
        dUDF_Chd = 0
        dPHF_Chd = 0
        dST_Chd = 0
        sChd_FareID = ""
        dChd_Basic = 0
        sChd_FC = ""
        sChd_FC = ""
        dothers_Adt = 0
        dothers_Chd = 0
        dothers_Inf = 0
    End Sub

    Private Sub booklog(ByVal airline As String, ByVal req As String, ByVal res As String)
        'Dim consql As New SqlConnection
        'Try
        '    Dim Str = "insert into Booking_log  (Airline,book_req,book_res) values('" & airline & "','" & Replace(req, "'", "") & "','" & Replace(res, "'", "") & "')"
        '    consql.ConnectionString = "uid=airpakixc;pwd=airpak-ecomm;database=airpakint;server=208.43.79.122"
        '    consql.Open()
        '    Dim cmd As New SqlCommand(Str, consql)
        '    cmd.ExecuteNonQuery()
        '    consql.Close()
        'Catch ex As Exception
        '    consql.Close()
        'End Try
    End Sub

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
        Try
            airMrkArray = Mrkdt.Select("AirlineCode='" & VC & "'", "")
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

End Class


