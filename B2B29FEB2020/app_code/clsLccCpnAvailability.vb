Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports LccCouponResult
Public Class clsLccCpnAvailability
    Dim Conn As New SqlConnection
    Public Sub New(ByVal strCon As String)
        Conn.ConnectionString = strCon
    End Sub
    
    Public Function GetLccCpnResult(ByVal Trip As String, ByVal TourType As String, ByVal Origin As String, ByVal Destination As String, ByVal DepDate As String, _
                                   ByVal RetDate As String, ByVal Adult As Integer, _
                                   ByVal Child As Integer, ByVal Infant As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal AgentId As String, ByVal Distr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal Airline As String) As DataTable
        Dim SpiceCpnDt As New DataTable
        Dim IndigoCpnDt As New DataTable
        Dim CouponResTbl As New DataTable
        Dim objCpnRes As New CouponFare
        Dim objclsDT As New clsDbSpiceJet
        Try

            IndigoCpnDt = objCpnRes.getFlightResultTable(Origin, Destination, DepDate.Replace("-", "/"), "", Adult.ToString(), Child.ToString(), Infant.ToString(), Inf_Basic.ToString, Inf_Tax.ToString, "6E", "True", "")
            If IndigoCpnDt.Rows.Count > 0 Then
                CouponResTbl = objclsDT.ResultTable
                Dim row As DataRow
                Conn.Open()
                'Calculation For AgentMarkUp'
                Dim dtAgentMarkup As New DataTable
                dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
                'Calculation For AdminMarkUp'
                Dim dtAdminMarkup As New DataTable
                dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
                For iCtr As Integer = 0 To IndigoCpnDt.Rows.Count - 1
                    row = CouponResTbl.NewRow()

                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = Origin
                    row("OrgDestTo") = Destination
                    row("Sector") = Origin & ":" & Destination
                    If Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureLocation") = Origin Else row("DepartureLocation") = IndigoCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim 'IndigoCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    If Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalLocation") = Destination Else row("ArrivalLocation") = IndigoCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim 'IndigoCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    If Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureCityName") = IndigoCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim Else row("DepartureCityName") = city_name(IndigoCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim)
                    If Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalCityName") = IndigoCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim Else row("ArrivalCityName") = city_name(IndigoCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim)
                    row("depdatelcc") = ""
                    row("arrdatelcc") = ""
                    row("DepartureDate") = Right(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                    row("DepartureTime") = convertTimeFormat(IndigoCpnDt.Rows(iCtr).Item("DEPARTURETIME").ToString.Trim)
                    row("ArrivalDate") = Right(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(IndigoCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                    row("ArrivalTime") = convertTimeFormat(IndigoCpnDt.Rows(iCtr).Item("ARRIVALTIME").ToString.Trim)
                    row("MarketingCarrier") = Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    If Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("AirLineName") = "Indigo" Else row("AirLineName") = "SpiceJet"
                    row("FlightIdentification") = Mid(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 3, IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim.Length - 1)
                    row("RBD") = "" 'IndigoCpnDt.Rows(iCtr).Item("RBD").ToString
                    row("AvailableSeats") = "0"
                    row("ValiDatingCarrier") = Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    row("EQ") = ""

                    row("Stops") = IndigoCpnDt.Select("LINENO='" & IndigoCpnDt.Rows(iCtr).Item("LINENO").ToString & "'", "").Length - 1 & "-Stop"
                    row("fareBasis") = ""
                    row("FBPaxType") = ""
                    row("LineItemNumber") = IndigoCpnDt.Rows(iCtr).Item("LINENO") ' iCtr + 1
                    row("Searchvalue") = ""
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    row("Leg") = iCtr + 1
                    row("Flight") = "1"
                    row("Tot_Dur") = ""
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    If Airline = "6E" Then row("CS") = "INDIGOSPECIAL" Else row("CS") = "SPICEJETSPECIAL"
                    If Airline = "6E" Then row("sno") = "INDIGOSPECIAL/" & IndigoCpnDt.Rows(iCtr).Item("SESSIONID") Else row("sno") = "SPICEJETSPECIAL/" & IndigoCpnDt.Rows(iCtr).Item("SESSIONID")
                    'row("sno") = ""


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (IndigoCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (IndigoCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) + (IndigoCpnDt.Rows(iCtr).Item("INFANTBASEFARE") * Infant)
                    totBFWOInf = (IndigoCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (IndigoCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) '(IndigoCpnDt.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (IndigoCpnDt.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (IndigoCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (IndigoCpnDt.Rows(iCtr).Item("CHILDYQ") * Child)
                    totTax = (IndigoCpnDt.Rows(iCtr).Item("ADULTOTHERTAX") * Adult) + (IndigoCpnDt.Rows(iCtr).Item("CHILDOTHERTAX") * Child) + (IndigoCpnDt.Rows(iCtr).Item("INFANTOTHERTAX") * Infant)
                    'Dim HsTblSTax As Hashtable = ServiceTax(IndigoCpnDt.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    ' Dim HsTblSTax As Hashtable = ServiceTax(IndigoCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, IndigoCpnDt.Rows(iCtr).Item("BaseFare"), IndigoCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    AStax = 0 'HsTblSTax("STax") * Adult
                    ATF = 0 'HsTblSTax("TF") * Adult
                    ' HsTblSTax.Clear()
                    ' HsTblSTax = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    CStax = 0 'HsTblSTax("STax") * Child
                    CTF = 0 ' HsTblSTax("TF") * Child
                    '  HsTblSTax.Clear()
                    'HsTblSTax = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                    IStax = 0 ' HsTblSTax("STax") * Infant
                    ITF = 0 ' HsTblSTax("TF") * Infant




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), IndigoCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), IndigoCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), IndigoCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, Left(IndigoCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), IndigoCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0
                    totMrk = ADTAdminMrk * Adult
                    totMrk = totMrk + ADTAgentMrk * Adult
                    totMrk = totMrk + CHDAdminMrk * Child
                    totMrk = totMrk + CHDAgentMrk * Child
                    'Calculation of Transaction details end


                    row("AdtBfare") = 0 ' IndigoCpnDt.Rows(iCtr).Item("ADULTBASEFARE")
                    row("AdtFSur") = 0 'IndigoCpnDt.Rows(iCtr).Item("ADULTYQ")
                    row("AdtTax") = IndigoCpnDt.Rows(iCtr).Item("ADULTTOTALFARE") ' IndigoCpnDt.Rows(iCtr).Item("ADULTOTHERTAX")
                    row("AdtFare") = IndigoCpnDt.Rows(iCtr).Item("ADULTTOTALFARE")
                    row("ChdBfare") = 0 ' IndigoCpnDt.Rows(iCtr).Item("CHILDBASEFARE")
                    row("ChdFSur") = 0 'IndigoCpnDt.Rows(iCtr).Item("CHILDYQ")
                    row("ChdTax") = IndigoCpnDt.Rows(iCtr).Item("CHILDTOTALFARE") ' IndigoCpnDt.Rows(iCtr).Item("CHILDOTHERTAX")
                    row("ChdFare") = IndigoCpnDt.Rows(iCtr).Item("CHILDTOTALFARE")
                    row("InfBfare") = 0 ' IndigoCpnDt.Rows(iCtr).Item("INFANTBASEFARE")
                    row("InfFSur") = 0 'IndigoCpnDt.Rows(iCtr).Item("INFANTYQ")
                    row("InfTax") = IndigoCpnDt.Rows(iCtr).Item("INFANTTOTALFARE") 'IndigoCpnDt.Rows(iCtr).Item("INFANTOTHERTAX")
                    row("InfFare") = IndigoCpnDt.Rows(iCtr).Item("INFANTTOTALFARE")
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = 0 'HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (IndigoCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (IndigoCpnDt.Rows(iCtr).Item("CHILDYQ") * Child) '+ (IndigoCpnDt.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax + totFS
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk + totFS
                    row("DisCount") = "0"
                    row("OriginalTF") = row("TotalFare") 'IndigoCpnDt.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax + totFS
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    '//
                    'row.Item("Cabin") = IndigoCpnDt.Rows(iCtr).Item("Cabin")
                    CouponResTbl.Rows.Add(row)
                Next

            End If

        Catch ex As Exception

        End Try
        Return CouponResTbl

    End Function
    Public Function GetLccCpnResultSG(ByVal Trip As String, ByVal TourType As String, ByVal Origin As String, ByVal Destination As String, ByVal DepDate As String, _
                                   ByVal RetDate As String, ByVal Adult As Integer, _
                                   ByVal Child As Integer, ByVal Infant As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal AgentId As String, ByVal Distr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal Airline As String) As DataTable
        Dim SpiceCpnDt As New DataTable
        Dim CouponResTbl As New DataTable
        Dim objCpnRes As New CouponFare
        Dim objclsDT As New clsDbIndigo
        Try

            SpiceCpnDt = objCpnRes.getFlightResultTable(Origin, Destination, DepDate.Replace("-", "/"), "", Adult.ToString(), Child.ToString(), Infant.ToString(), Inf_Basic.ToString, Inf_Tax.ToString, "SG", "True", "")
            If SpiceCpnDt.Rows.Count > 0 Then
                CouponResTbl = objclsDT.ResultTable
                Dim row As DataRow
                Conn.Open()
                'Calculation For AgentMarkUp'
                Dim dtAgentMarkup As New DataTable
                dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
                'Calculation For AdminMarkUp'
                Dim dtAdminMarkup As New DataTable
                dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
                For iCtr As Integer = 0 To SpiceCpnDt.Rows.Count - 1
                    row = CouponResTbl.NewRow()

                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = Origin
                    row("OrgDestTo") = Destination
                    row("Sector") = Origin & ":" & Destination
                    If Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureLocation") = Origin Else row("DepartureLocation") = SpiceCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim 'SpiceCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    If Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalLocation") = Destination Else row("ArrivalLocation") = SpiceCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim 'SpiceCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    If Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureCityName") = SpiceCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim Else row("DepartureCityName") = city_name(SpiceCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim)
                    If Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalCityName") = SpiceCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim Else row("ArrivalCityName") = city_name(SpiceCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim)
                    row("depdatelcc") = ""
                    row("arrdatelcc") = ""
                    row("DepartureDate") = Right(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                    row("DepartureTime") = convertTimeFormat(SpiceCpnDt.Rows(iCtr).Item("DEPARTURETIME").ToString.Trim)
                    row("ArrivalDate") = Right(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(SpiceCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                    row("ArrivalTime") = convertTimeFormat(SpiceCpnDt.Rows(iCtr).Item("ARRIVALTIME").ToString.Trim)
                    row("MarketingCarrier") = Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    If Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("AirLineName") = "Indigo" Else row("AirLineName") = "SpiceJet"
                    row("FlightIdentification") = Mid(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 3, SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim.Length - 1)
                    row("RBD") = "" 'SpiceCpnDt.Rows(iCtr).Item("RBD").ToString
                    row("AvailableSeats") = "0"
                    row("ValiDatingCarrier") = Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    row("EQ") = ""

                    row("Stops") = SpiceCpnDt.Select("LINENO='" & SpiceCpnDt.Rows(iCtr).Item("LINENO").ToString & "'", "").Length - 1 & "-Stop"
                    row("fareBasis") = ""
                    row("FBPaxType") = ""
                    row("LineItemNumber") = SpiceCpnDt.Rows(iCtr).Item("LINENO") ' iCtr + 1
                    row("Searchvalue") = ""
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    row("Leg") = iCtr + 1
                    row("Flight") = "1"
                    row("Tot_Dur") = ""
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    If Airline = "6E" Then row("CS") = "INDIGOSPECIAL" Else row("CS") = "SPICEJETSPECIAL"
                    If Airline = "6E" Then row("sno") = "INDIGOSPECIAL/" & SpiceCpnDt.Rows(iCtr).Item("SESSIONID") Else row("sno") = "SPICEJETSPECIAL/" & SpiceCpnDt.Rows(iCtr).Item("SESSIONID")
                    'row("sno") = ""


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (SpiceCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (SpiceCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) + (SpiceCpnDt.Rows(iCtr).Item("INFANTBASEFARE") * Infant)
                    totBFWOInf = (SpiceCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (SpiceCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) '(SpiceCpnDt.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (SpiceCpnDt.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (SpiceCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (SpiceCpnDt.Rows(iCtr).Item("CHILDYQ") * Child)
                    totTax = (SpiceCpnDt.Rows(iCtr).Item("ADULTOTHERTAX") * Adult) + (SpiceCpnDt.Rows(iCtr).Item("CHILDOTHERTAX") * Child) + (SpiceCpnDt.Rows(iCtr).Item("INFANTOTHERTAX") * Infant)
                    'Dim HsTblSTax As Hashtable = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    ' Dim HsTblSTax As Hashtable = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    AStax = 0 'HsTblSTax("STax") * Adult
                    ATF = 0 'HsTblSTax("TF") * Adult
                    ' HsTblSTax.Clear()
                    ' HsTblSTax = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("BaseFare"), SpiceCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    CStax = 0 'HsTblSTax("STax") * Child
                    CTF = 0 ' HsTblSTax("TF") * Child
                    '  HsTblSTax.Clear()
                    'HsTblSTax = ServiceTax(SpiceCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                    IStax = 0 ' HsTblSTax("STax") * Infant
                    ITF = 0 ' HsTblSTax("TF") * Infant




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), SpiceCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), SpiceCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), SpiceCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, Left(SpiceCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), SpiceCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0
                    totMrk = ADTAdminMrk * Adult
                    totMrk = totMrk + ADTAgentMrk * Adult
                    totMrk = totMrk + CHDAdminMrk * Child
                    totMrk = totMrk + CHDAgentMrk * Child
                    'Calculation of Transaction details end


                    row("AdtBfare") = 0 ' SpiceCpnDt.Rows(iCtr).Item("ADULTBASEFARE")
                    row("AdtFSur") = 0 'SpiceCpnDt.Rows(iCtr).Item("ADULTYQ")
                    row("AdtTax") = SpiceCpnDt.Rows(iCtr).Item("ADULTTOTALFARE") ' SpiceCpnDt.Rows(iCtr).Item("ADULTOTHERTAX")
                    row("AdtFare") = SpiceCpnDt.Rows(iCtr).Item("ADULTTOTALFARE")
                    row("ChdBfare") = 0 ' SpiceCpnDt.Rows(iCtr).Item("CHILDBASEFARE")
                    row("ChdFSur") = 0 'SpiceCpnDt.Rows(iCtr).Item("CHILDYQ")
                    row("ChdTax") = SpiceCpnDt.Rows(iCtr).Item("CHILDTOTALFARE") ' SpiceCpnDt.Rows(iCtr).Item("CHILDOTHERTAX")
                    row("ChdFare") = SpiceCpnDt.Rows(iCtr).Item("CHILDTOTALFARE")
                    row("InfBfare") = 0 ' SpiceCpnDt.Rows(iCtr).Item("INFANTBASEFARE")
                    row("InfFSur") = 0 'SpiceCpnDt.Rows(iCtr).Item("INFANTYQ")
                    row("InfTax") = SpiceCpnDt.Rows(iCtr).Item("INFANTTOTALFARE") 'SpiceCpnDt.Rows(iCtr).Item("INFANTOTHERTAX")
                    row("InfFare") = SpiceCpnDt.Rows(iCtr).Item("INFANTTOTALFARE")
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = 0 'HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (SpiceCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (SpiceCpnDt.Rows(iCtr).Item("CHILDYQ") * Child) '+ (SpiceCpnDt.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax + totFS
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk + totFS
                    row("DisCount") = "0"
                    row("OriginalTF") = row("TotalFare") 'SpiceCpnDt.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax + totFS
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    '//
                    'row.Item("Cabin") = SpiceCpnDt.Rows(iCtr).Item("Cabin")
                    CouponResTbl.Rows.Add(row)
                Next

            End If

        Catch ex As Exception

        End Try
        Return CouponResTbl

    End Function

    Public Function GetLccCpnResultGO(ByVal Trip As String, ByVal TourType As String, ByVal Origin As String, ByVal Destination As String, ByVal DepDate As String, _
                                   ByVal RetDate As String, ByVal Adult As Integer, _
                                   ByVal Child As Integer, ByVal Infant As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal AgentId As String, ByVal Distr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal Airline As String) As DataTable
        Dim GoairCpnDt As New DataTable
        Dim CouponResTbl As New DataTable
        Dim objCpnRes As New CouponFare
        Dim objclsDT As New clsDbIndigo
        Try

            GoairCpnDt = objCpnRes.getFlightResultTable(Origin, Destination, DepDate.Replace("-", "/"), "", Adult.ToString(), Child.ToString(), Infant.ToString(), Inf_Basic.ToString, Inf_Tax.ToString, "G8", "True", "")
            If GoairCpnDt.Rows.Count > 0 Then
                CouponResTbl = objclsDT.ResultTable
                Dim row As DataRow
                Conn.Open()
                'Calculation For AgentMarkUp'
                Dim dtAgentMarkup As New DataTable
                dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
                'Calculation For AdminMarkUp'
                Dim dtAdminMarkup As New DataTable
                dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
                For iCtr As Integer = 0 To GoairCpnDt.Rows.Count - 1
                    row = CouponResTbl.NewRow()

                    '//new entry
                    row("TripType") = "O"
                    row("OrgDestFrom") = Origin
                    row("OrgDestTo") = Destination
                    row("Sector") = Origin & ":" & Destination
                    'If Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureLocation") = Origin Else row("DepartureLocation") = GoairCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim 'GoairCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    'If Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalLocation") = Origin Else row("ArrivalLocation") = GoairCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim 'GoairCpnDt.Rows(iCtr).Item("DepartureLocation").ToString.Trim
                    'If Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("DepartureCityName") = GoairCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim Else row("DepartureCityName") = city_name(GoairCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim)
                    'If Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2) = "6E" Then row("ArrivalCityName") = GoairCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim Else row("ArrivalCityName") = city_name(GoairCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim)
                    row("DepartureLocation") = GoairCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim
                    row("ArrivalLocation") = GoairCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim
                    row("DepartureCityName") = city_name(GoairCpnDt.Rows(iCtr).Item("DEPARTURECITY").ToString.Trim)
                    row("ArrivalCityName") = city_name(GoairCpnDt.Rows(iCtr).Item("ARRIVALCITY").ToString.Trim)

                    row("depdatelcc") = ""
                    row("arrdatelcc") = ""
                    row("DepartureDate") = Right(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                    row("DepartureTime") = convertTimeFormat(GoairCpnDt.Rows(iCtr).Item("DEPARTURETIME").ToString.Trim)
                    row("ArrivalDate") = Right(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 2) & Mid(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 6, 2) & Mid(GoairCpnDt.Rows(iCtr).Item("DEPARTUREDATE").ToString.Trim, 3, 2)
                    row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                    row("ArrivalTime") = convertTimeFormat(GoairCpnDt.Rows(iCtr).Item("ARRIVALTIME").ToString.Trim)
                    row("MarketingCarrier") = Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    row("AirLineName") = "GoAir"
                    row("FlightIdentification") = Mid(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 3, GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim.Length - 1)
                    row("RBD") = "" 'GoairCpnDt.Rows(iCtr).Item("RBD").ToString
                    row("AvailableSeats") = "0"
                    row("ValiDatingCarrier") = Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2)
                    row("EQ") = ""

                    row("Stops") = GoairCpnDt.Select("LINENO='" & GoairCpnDt.Rows(iCtr).Item("LINENO").ToString & "'", "").Length - 1 & "-Stop"
                    row("fareBasis") = ""
                    row("FBPaxType") = ""
                    row("LineItemNumber") = GoairCpnDt.Rows(iCtr).Item("LINENO") ' iCtr + 1
                    row("Searchvalue") = ""
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    row("Leg") = iCtr + 1
                    row("Flight") = "1"
                    row("Tot_Dur") = ""
                    row("Trip") = TourType.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = "GOAIRSPECIAL"
                    row("sno") = "GOAIRSPECIAL/" & GoairCpnDt.Rows(iCtr).Item("SESSIONID")
                    'row("sno") = ""


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (GoairCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (GoairCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) + (GoairCpnDt.Rows(iCtr).Item("INFANTBASEFARE") * Infant)
                    totBFWOInf = (GoairCpnDt.Rows(iCtr).Item("ADULTBASEFARE") * Adult) + (GoairCpnDt.Rows(iCtr).Item("CHILDBASEFARE") * Child) '(GoairCpnDt.Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (GoairCpnDt.Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (GoairCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (GoairCpnDt.Rows(iCtr).Item("CHILDYQ") * Child)
                    totTax = (GoairCpnDt.Rows(iCtr).Item("ADULTOTHERTAX") * Adult) + (GoairCpnDt.Rows(iCtr).Item("CHILDOTHERTAX") * Child) + (GoairCpnDt.Rows(iCtr).Item("INFANTOTHERTAX") * Infant)
                    'Dim HsTblSTax As Hashtable = ServiceTax(GoairCpnDt.Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    ' Dim HsTblSTax As Hashtable = ServiceTax(GoairCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, GoairCpnDt.Rows(iCtr).Item("BaseFare"), GoairCpnDt.Rows(iCtr).Item("BaseFare"), GoairCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    AStax = 0 'HsTblSTax("STax") * Adult
                    ATF = 0 'HsTblSTax("TF") * Adult
                    ' HsTblSTax.Clear()
                    ' HsTblSTax = ServiceTax(GoairCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, GoairCpnDt.Rows(iCtr).Item("BaseFare"), GoairCpnDt.Rows(iCtr).Item("BaseFare"), GoairCpnDt.Rows(iCtr).Item("YQ"), Trip)
                    CStax = 0 'HsTblSTax("STax") * Child
                    CTF = 0 ' HsTblSTax("TF") * Child
                    '  HsTblSTax.Clear()
                    'HsTblSTax = ServiceTax(GoairCpnDt.Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                    IStax = 0 ' HsTblSTax("STax") * Infant
                    ITF = 0 ' HsTblSTax("TF") * Infant




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), GoairCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), GoairCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), GoairCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip)
                    If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, Left(GoairCpnDt.Rows(iCtr).Item("FLIGHT").ToString.Trim, 2), GoairCpnDt.Rows(iCtr).Item("TOTALPACKAGECOST"), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0
                    totMrk = ADTAdminMrk * Adult
                    totMrk = totMrk + ADTAgentMrk * Adult
                    totMrk = totMrk + CHDAdminMrk * Child
                    totMrk = totMrk + CHDAgentMrk * Child
                    'Calculation of Transaction details end


                    row("AdtBfare") = 0 ' GoairCpnDt.Rows(iCtr).Item("ADULTBASEFARE")
                    row("AdtFSur") = 0 'GoairCpnDt.Rows(iCtr).Item("ADULTYQ")
                    row("AdtTax") = GoairCpnDt.Rows(iCtr).Item("ADULTTOTALFARE") ' GoairCpnDt.Rows(iCtr).Item("ADULTOTHERTAX")
                    row("AdtFare") = GoairCpnDt.Rows(iCtr).Item("ADULTTOTALFARE")
                    row("ChdBfare") = 0 ' GoairCpnDt.Rows(iCtr).Item("CHILDBASEFARE")
                    row("ChdFSur") = 0 'GoairCpnDt.Rows(iCtr).Item("CHILDYQ")
                    row("ChdTax") = GoairCpnDt.Rows(iCtr).Item("CHILDTOTALFARE") ' GoairCpnDt.Rows(iCtr).Item("CHILDOTHERTAX")
                    row("ChdFare") = GoairCpnDt.Rows(iCtr).Item("CHILDTOTALFARE")
                    row("InfBfare") = 0 ' GoairCpnDt.Rows(iCtr).Item("INFANTBASEFARE")
                    row("InfFSur") = 0 'GoairCpnDt.Rows(iCtr).Item("INFANTYQ")
                    row("InfTax") = GoairCpnDt.Rows(iCtr).Item("INFANTTOTALFARE") 'GoairCpnDt.Rows(iCtr).Item("INFANTOTHERTAX")
                    row("InfFare") = GoairCpnDt.Rows(iCtr).Item("INFANTTOTALFARE")
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = 0 'HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = (GoairCpnDt.Rows(iCtr).Item("ADULTYQ") * Adult) + (GoairCpnDt.Rows(iCtr).Item("CHILDYQ") * Child) '+ (GoairCpnDt.Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax + totFS
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk + totFS
                    row("DisCount") = "0"
                    row("OriginalTF") = row("TotalFare") 'GoairCpnDt.Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax + totFS
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    '//
                    'row.Item("Cabin") = GoairCpnDt.Rows(iCtr).Item("Cabin")
                    CouponResTbl.Rows.Add(row)
                Next
            End If
        Catch ex As Exception

        End Try
        Return CouponResTbl
    End Function

    Public Function GetLccCpnResultIX(ByVal Trip As String, ByVal TourType As String, ByVal Origin As String, ByVal Destination As String, ByVal DepDate As String, _
                                  ByVal RetDate As String, ByVal Adult As Integer, _
                                  ByVal Child As Integer, ByVal Infant As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal AgentId As String, ByVal Distr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal Airline As String) As DataTable
        Dim IXCpnDt As New DataTable
        Dim CouponResTbl As New DataTable
        Dim objCpnRes As New CouponFare
        Dim objclsDT As New clsDbIndigo
        Try
            IXCpnDt = GetIXResult(objCpnRes.getFlightResultTable(Origin, Destination, DepDate.Replace("-", "/"), RetDate.Replace("-", "/"), Adult.ToString(), Child.ToString(), Infant.ToString(), Inf_Basic.ToString, Inf_Tax.ToString, "IX", "True", TourType), TourType)
            CouponResTbl = objclsDT.ResultTable
            Dim row As DataRow
            Conn.Open()
            Dim dtIX As DataTable
            'Calculation For AgentMarkUp'
            Dim dtAgentMarkup As New DataTable
            dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
            'Calculation For AdminMarkUp'
            Dim dtAdminMarkup As New DataTable
            dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
            dtIX = IXCpnDt.DefaultView.ToTable(True, "LineItemNumber")
            For iCtr As Integer = 0 To dtIX.Rows.Count - 1
                Dim IXArray As Array
                IXArray = IXCpnDt.Select("LineItemNumber='" & dtIX.Rows(iCtr)("LineItemNumber") & "'", "")
                Dim IXFareADTHT, IXFareCHDHT As New Hashtable
                IXFareADTHT = GetCalcFareIX(IXArray, "ADULT")
                IXFareCHDHT = GetCalcFareIX(IXArray, "CHILD")
                For ictr1 As Integer = 0 To IXArray.Length - 1
                    row = CouponResTbl.NewRow()
                    If (IXArray(ictr1)("TRIPTYPE")).ToString.Trim = "O" Then
                        row("OrgDestFrom") = Origin
                        row("OrgDestTo") = Destination
                        row("Sector") = Origin & ":" & Destination
                        row("DepartureLocation") = Origin
                        row("ArrivalLocation") = Destination
                    Else
                        row("OrgDestFrom") = Destination
                        row("OrgDestTo") = Origin
                        row("Sector") = Destination & ":" & Origin
                        row("DepartureLocation") = Destination
                        row("ArrivalLocation") = Origin
                    End If
                    row("TripType") = (IXArray(ictr1)("TRIPTYPE"))
                    row("DepartureCityName") = (IXArray(ictr1)("DEPARTURECITY")).ToString.Trim
                    row("ArrivalCityName") = (IXArray(ictr1)("ARRIVALCITY")).ToString.Trim
                    row("depdatelcc") = ""
                    row("arrdatelcc") = ""
                    row("DepartureDate") = Right((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 2) & Mid((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 6, 2) & Mid((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 3, 2)
                    row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                    row("DepartureTime") = convertTimeFormat((IXArray(ictr1)("DEPARTURETIME")).ToString.Trim)
                    row("ArrivalDate") = Right((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 2) & Mid((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 6, 2) & Mid((IXArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 3, 2)
                    row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                    row("ArrivalTime") = convertTimeFormat((IXArray(ictr1)("ARRIVALTIME")).ToString.Trim)
                    row("MarketingCarrier") = Left((IXArray(ictr1)("FLIGHT")).ToString.Trim, 2)
                    row("AirLineName") = "Air IndiaExpress"
                    row("FlightIdentification") = (IXArray(ictr1)("FLIGHT")).ToString.Trim.Replace("IX-", "")
                    row("RBD") = "" '(IXArray(ictr1)("RBD").ToString
                    row("AvailableSeats") = "0"
                    row("ValiDatingCarrier") = "IX"
                    row("EQ") = ""

                    row("Stops") = (IXArray(ictr1)("STOPS")).ToString & "-Stop"
                    row("fareBasis") = (IXArray(ictr1)("TOTALPACKAGECOST")).ToString
                    row("FBPaxType") = ""
                    row("LineItemNumber") = (IXArray(ictr1)("LineItemNumber")) ' iCtr + 1
                    row("Searchvalue") = ""
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    row("Leg") = (IXArray(ictr1)("Leg"))
                    row("Flight") = (IXArray(ictr1)("Flight1"))
                    row("Tot_Dur") = ""
                    row("Trip") = Trip.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = "AIINDIAEXPRESS"
                    row("sno") = "AIINDIAEXPRESS/"
                    'row("sno") = ""


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (IXFareADTHT("BF") * Adult) + (IXFareCHDHT("BF") * Child) '+ ((IXArray(ictr1)("INFANTBASEFARE")) * Infant)
                    totBFWOInf = totBFWInf '((IXArray(ictr1)("ADULTBASEFARE")) * Adult) + ((IXArray(ictr1)("CHILDBASEFARE")) * Child) '((IXArray(ictr1)("BaseFareAmt_Adt") * dTotalNo_Adt) + ((IXArray(ictr1)("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (IXFareADTHT("YQ") * Adult) + (IXFareCHDHT("YQ") * Child)
                    totTax = (IXFareADTHT("OT") * Adult) + (IXFareCHDHT("OT") * Child) '+ ((IXArray(ictr1)("INFANTOTHERTAX")) * Infant)
                    'Dim HsTblSTax As Hashtable = ServiceTax((IXArray(ictr1)("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    ' Dim HsTblSTax As Hashtable = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("YQ"), Trip)
                    AStax = 0 'HsTblSTax("STax") * Adult
                    ATF = 0 'HsTblSTax("TF") * Adult
                    ' HsTblSTax.Clear()
                    ' HsTblSTax = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("YQ"), Trip)
                    CStax = 0 'HsTblSTax("STax") * Child
                    CTF = 0 ' HsTblSTax("TF") * Child
                    '  HsTblSTax.Clear()
                    'HsTblSTax = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                    IStax = 0 ' HsTblSTax("STax") * Infant
                    ITF = 0 ' HsTblSTax("TF") * Infant




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, Left((IXArray(ictr1)("FLIGHT")).ToString.Trim, 2), (IXFareADTHT("BF") + IXFareADTHT("YQ") + IXFareADTHT("OT")), Trip)
                    If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, Left((IXArray(ictr1)("FLIGHT")).ToString.Trim, 2), (IXFareCHDHT("BF") + IXFareCHDHT("YQ") + IXFareCHDHT("OT")), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, Left((IXArray(ictr1)("FLIGHT")).ToString.Trim, 2), (IXFareADTHT("BF") + IXFareADTHT("YQ") + IXFareADTHT("OT")), Trip)
                    If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, Left((IXArray(ictr1)("FLIGHT")).ToString.Trim, 2), (IXFareCHDHT("BF") + IXFareCHDHT("YQ") + IXFareCHDHT("OT")), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0
                    ADTAdminMrk = IXFareADTHT("TC")
                    CHDAdminMrk = IXFareCHDHT("TC")
                    totMrk = ADTAdminMrk * Adult
                    totMrk = totMrk + ADTAgentMrk * Adult
                    totMrk = totMrk + CHDAdminMrk * Child
                    totMrk = totMrk + CHDAgentMrk * Child
                    'Calculation of Transaction details end


                    row("AdtBfare") = IXFareADTHT("BF")
                    row("AdtFSur") = IXFareADTHT("YQ")
                    row("AdtTax") = IXFareADTHT("OT") + IXFareADTHT("YQ") ' (IXArray(ictr1)("ADULTOTHERTAX")
                    row("AdtFare") = IXFareADTHT("TF")
                    row("ChdBfare") = IXFareCHDHT("BF")
                    row("ChdFSur") = IXFareCHDHT("YQ")
                    row("ChdTax") = IXFareCHDHT("OT") + IXFareCHDHT("YQ")
                    row("ChdFare") = IXFareCHDHT("TF")
                    row("InfBfare") = 0
                    row("InfFSur") = 0
                    row("InfTax") = 0
                    row("InfFare") = 0
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = 0 'HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = totFS '((IXArray(ictr1)("ADULTYQ")) * Adult) + ((IXArray(ictr1)("CHILDYQ")) * Child) '+ ((IXArray(ictr1)("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax + totFS
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk + totFS
                    row("DisCount") = "0"
                    row("OriginalTF") = row("TotalFare") '(IXArray(ictr1)("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax + totFS
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    CouponResTbl.Rows.Add(row)
                Next
            Next



        Catch ex As Exception

        End Try
        Return CouponResTbl

    End Function

    Private Function GetIXResult(ByVal IXCpnDt As DataTable, ByVal TourType As String) As DataTable
        Dim IXDt As New DataTable
        Dim IXResArrayO, IXResArrayR As Array
        If IXCpnDt.Rows.Count > 0 Then

            IXResArrayO = IXCpnDt.Select("TRIPTYPE='O' and ADULTTOTALFARE<>'0'", "")
            IXResArrayR = IXCpnDt.Select("TRIPTYPE='R' and ADULTTOTALFARE<>'0'", "")
            IXDt = IXCpnDt.Clone()
            IXDt.Clear()
            Dim AirDataColumn As DataColumn
            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Leg"
            IXDt.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Flight1"
            IXDt.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "LineItemNumber"
            IXDt.Columns.Add(AirDataColumn)
            Dim cnt As Integer = 1
            If TourType = "RoundTrip" Then
                If IXResArrayO.Length > 0 And IXResArrayR.Length > 0 Then
                    For i As Integer = 0 To IXResArrayO.Length - 1
                        For ii As Integer = 0 To IXResArrayR.Length - 1

                            IXDt.ImportRow(IXResArrayO(i))
                            IXDt.Rows(IXDt.Rows.Count - 1)("Leg") = "1"
                            IXDt.Rows(IXDt.Rows.Count - 1)("Flight1") = "1"
                            IXDt.Rows(IXDt.Rows.Count - 1)("LineItemNumber") = cnt.ToString
                            IXDt.ImportRow(IXResArrayR(ii))
                            IXDt.Rows(IXDt.Rows.Count - 1)("Leg") = "1"
                            IXDt.Rows(IXDt.Rows.Count - 1)("Flight1") = "2"
                            IXDt.Rows(IXDt.Rows.Count - 1)("LineItemNumber") = cnt.ToString
                            cnt += 1
                        Next
                    Next
                End If
            Else
                If IXResArrayO.Length > 0 Then
                    For i As Integer = 0 To IXResArrayO.Length - 1
                        IXDt.ImportRow(IXResArrayO(i))
                        IXDt.Rows(IXDt.Rows.Count - 1)("Leg") = "1"
                        IXDt.Rows(IXDt.Rows.Count - 1)("Flight1") = "1"
                        IXDt.Rows(IXDt.Rows.Count - 1)("LineItemNumber") = cnt.ToString
                        cnt += 1
                    Next
                End If
            End If
        End If
        Return IXDt
    End Function

    Private Function GetCalcFareIX(ByVal IXArray As Array, ByVal PaxType As String) As Hashtable
        Dim BF As Double = 0, YQ As Double = 0, OT As Double = 0, TC As Double = 0, TF As Double = 0
        Dim IXFareHT As New Hashtable
        For i As Integer = 0 To IXArray.Length - 1
            BF += Convert.ToDouble((IXArray(i)(PaxType & "BASEFARE")))
            YQ += Convert.ToDouble((IXArray(i)(PaxType & "YQ")))
            OT += Convert.ToDouble((IXArray(i)(PaxType & "OTHERTAX")))
            TC += Convert.ToDouble((IXArray(i)(PaxType & "TC")))
            TF += Convert.ToDouble((IXArray(i)(PaxType & "TOTALFARE")))
        Next
        IXFareHT.Add("BF", BF)
        IXFareHT.Add("YQ", YQ)
        IXFareHT.Add("OT", OT)
        IXFareHT.Add("TC", TC)
        IXFareHT.Add("TF", TF)

        Return IXFareHT
    End Function

    Public Function GetLccCpnResultAirAsia(ByVal Trip As String, ByVal TourType As String, ByVal Origin As String, ByVal Destination As String, ByVal DepDate As String, _
                                   ByVal RetDate As String, ByVal Adult As Integer, _
                                   ByVal Child As Integer, ByVal Infant As Integer, ByVal Inf_Basic As Double, ByVal Inf_Tax As Double, ByVal AgentId As String, ByVal Distr As String, ByVal searchValue As String, ByVal tCnt As Integer, ByVal Ft As String, ByVal TrackId As String, ByVal Airline As String) As DataTable
        Dim AKCpnDt As New DataTable
        Dim CouponResTbl As New DataTable
        Dim objCpnRes As New CouponFare
        Dim objclsDT As New clsDbIndigo
        Try
            AKCpnDt = GetAirAsiaResult(objCpnRes.getFlightResultTable(Origin, Destination, DepDate.Replace("-", "/"), RetDate.Replace("-", "/"), Adult.ToString(), Child.ToString(), Infant.ToString(), Inf_Basic.ToString, Inf_Tax.ToString, "AK", "True", TourType), TourType)
            CouponResTbl = objclsDT.ResultTable
            Dim row As DataRow
            Conn.Open()
            Dim dtAK As DataTable
            'Calculation For AgentMarkUp'
            Dim dtAgentMarkup As New DataTable
            dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
            'Calculation For AdminMarkUp'
            Dim dtAdminMarkup As New DataTable
            dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
            dtAK = AKCpnDt.DefaultView.ToTable(True, "LineItemNumber")
            For iCtr As Integer = 0 To dtAK.Rows.Count - 1
                Dim AKArray As Array
                AKArray = AKCpnDt.Select("LineItemNumber='" & dtAK.Rows(iCtr)("LineItemNumber") & "'", "")
                Dim AKFareADTHT, AKFareCHDHT As New Hashtable
                AKFareADTHT = GetCalcFareAirAsia(AKArray, "ADULT")
                AKFareCHDHT = GetCalcFareAirAsia(AKArray, "CHILD")
                For ictr1 As Integer = 0 To AKArray.Length - 1
                    row = CouponResTbl.NewRow()
                    If (AKArray(ictr1)("TRIPTYPE")).ToString.Trim = "O" Then
                        row("OrgDestFrom") = Origin
                        row("OrgDestTo") = Destination
                        row("Sector") = Origin & ":" & Destination
                        'row("DepartureLocation") = Origin
                        'row("ArrivalLocation") = Destination
                    Else
                        row("OrgDestFrom") = Destination
                        row("OrgDestTo") = Origin
                        row("Sector") = Destination & ":" & Origin
                        'row("DepartureLocation") = Destination
                        'row("ArrivalLocation") = Origin
                    End If
                    row("DepartureLocation") = (AKArray(ictr1)("DEPARTURECITY")).ToString.Trim
                    row("ArrivalLocation") = (AKArray(ictr1)("ARRIVALCITY")).ToString.Trim
                    row("TripType") = (AKArray(ictr1)("TRIPTYPE"))
                    row("DepartureCityName") = city_name((AKArray(ictr1)("DEPARTURECITY")).ToString.Trim)
                    row("ArrivalCityName") = city_name((AKArray(ictr1)("ARRIVALCITY")).ToString.Trim)
                    row("depdatelcc") = ""
                    row("arrdatelcc") = ""
                    row("DepartureDate") = Right((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 2) & Mid((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 6, 2) & Mid((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 3, 2)
                    row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                    row("DepartureTime") = convertTimeFormat((AKArray(ictr1)("DEPARTURETIME")).ToString.Trim)
                    row("ArrivalDate") = Right((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 2) & Mid((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 6, 2) & Mid((AKArray(ictr1)("DEPARTUREDATE")).ToString.Trim, 3, 2)
                    row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                    row("ArrivalTime") = convertTimeFormat((AKArray(ictr1)("ARRIVALTIME")).ToString.Trim)
                    row("MarketingCarrier") = Left((AKArray(ictr1)("FLIGHT")).ToString.Trim, 2)
                    row("AirLineName") = "AirAsia"
                    row("FlightIdentification") = (AKArray(ictr1)("FLIGHT")).ToString.Trim.Replace(Left((AKArray(ictr1)("FLIGHT")).ToString, 2), "")
                    row("RBD") = "" '(IXArray(ictr1)("RBD").ToString
                    row("AvailableSeats") = "0"
                    row("ValiDatingCarrier") = "AK"
                    row("EQ") = ""

                    row("Stops") = (AKArray(ictr1)("STOPS")).ToString & "-Stop"
                    row("fareBasis") = ""
                    row("FBPaxType") = ""
                    row("LineItemNumber") = (AKArray(ictr1)("LineItemNumber")) ' iCtr + 1
                    row("Searchvalue") = ""
                    row("TotPax") = Adult + Child
                    row("Adult") = Adult
                    row("Child") = Child
                    row("Infant") = Infant
                    row("Leg") = (AKArray(ictr1)("Leg"))
                    row("Flight") = (AKArray(ictr1)("Flight1"))
                    row("Tot_Dur") = ""
                    row("Trip") = Trip.ToString
                    row("TripCnt") = tCnt
                    row("Currency") = "INR"
                    row("CS") = "AIRASIA"
                    row("sno") = "AIRASIA"
                    'row("sno") = ""


                    'Calculation of Transaction details
                    Dim totBFWInf As Double = 0
                    Dim totBFWOInf As Double = 0
                    Dim totFS As Double = 0
                    Dim totTax As Double = 0
                    Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                    Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                    Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                    totBFWInf = (AKFareADTHT("BF") * Adult) + (AKFareCHDHT("BF") * Child) '+ ((IXArray(ictr1)("INFANTBASEFARE")) * Infant)
                    totBFWOInf = totBFWInf '((IXArray(ictr1)("ADULTBASEFARE")) * Adult) + ((IXArray(ictr1)("CHILDBASEFARE")) * Child) '((IXArray(ictr1)("BaseFareAmt_Adt") * dTotalNo_Adt) + ((IXArray(ictr1)("BaseFareAmt_Chd") * dTotalNo_Chd)
                    totFS = (AKFareADTHT("YQ") * Adult) + (AKFareCHDHT("YQ") * Child)
                    totTax = (AKFareADTHT("OT") * Adult) + (AKFareCHDHT("OT") * Child) '+ ((IXArray(ictr1)("INFANTOTHERTAX")) * Infant)
                    'Dim HsTblSTax As Hashtable = ServiceTax((IXArray(ictr1)("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                    ' Dim HsTblSTax As Hashtable = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("YQ"), Trip)
                    AStax = 0 'HsTblSTax("STax") * Adult
                    ATF = 0 'HsTblSTax("TF") * Adult
                    ' HsTblSTax.Clear()
                    ' HsTblSTax = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("BaseFare"), (IXArray(ictr1)("YQ"), Trip)
                    CStax = 0 'HsTblSTax("STax") * Child
                    CTF = 0 ' HsTblSTax("TF") * Child
                    '  HsTblSTax.Clear()
                    'HsTblSTax = ServiceTax((IXArray(ictr1)("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                    IStax = 0 ' HsTblSTax("STax") * Infant
                    ITF = 0 ' HsTblSTax("TF") * Infant




                    ADTAgentMrk = CalcMarkup(dtAgentMarkup, "AK", (AKFareADTHT("BF") + AKFareADTHT("YQ") + AKFareADTHT("OT")), Trip)
                    If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, "AK", (AKFareCHDHT("BF") + AKFareCHDHT("YQ") + AKFareCHDHT("OT")), Trip) Else CHDAgentMrk = 0
                    ADTAdminMrk = CalcMarkup(dtAdminMarkup, "AK", (AKFareADTHT("BF") + AKFareADTHT("YQ") + AKFareADTHT("OT")), Trip)
                    If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, "AK", (AKFareCHDHT("BF") + AKFareCHDHT("YQ") + AKFareCHDHT("OT")), Trip) Else CHDAgentMrk = 0
                    Dim totMrk As Double = 0
                    'ADTAdminMrk = AKFareADTHT("TC")
                    'CHDAdminMrk = AKFareCHDHT("TC")
                    totMrk = ADTAdminMrk * Adult
                    totMrk = totMrk + ADTAgentMrk * Adult
                    totMrk = totMrk + CHDAdminMrk * Child
                    totMrk = totMrk + CHDAgentMrk * Child
                    'Calculation of Transaction details end


                    row("AdtBfare") = AKFareADTHT("BF")
                    row("AdtFSur") = AKFareADTHT("YQ")
                    row("AdtTax") = AKFareADTHT("OT") + AKFareADTHT("YQ") ' (IXArray(ictr1)("ADULTOTHERTAX")
                    row("AdtFare") = AKFareADTHT("TF")
                    row("ChdBfare") = AKFareCHDHT("BF")
                    row("ChdFSur") = AKFareCHDHT("YQ")
                    row("ChdTax") = AKFareCHDHT("OT") + AKFareCHDHT("YQ")
                    row("ChdFare") = AKFareCHDHT("TF")
                    row("InfBfare") = 0
                    row("InfFSur") = 0
                    row("InfTax") = 0
                    row("InfFare") = 0
                    row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                    row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                    row("IATAComm") = 0 'HsTblSTax("IATAComm")
                    row("ADTAgentMrk") = ADTAgentMrk
                    row("CHDAgentMrk") = CHDAgentMrk
                    row("ADTAdminMrk") = ADTAdminMrk
                    row("CHDAdminMrk") = CHDAdminMrk
                    row("TotalBfare") = totBFWInf
                    row("TotalFuelSur") = totFS '((IXArray(ictr1)("ADULTYQ")) * Adult) + ((IXArray(ictr1)("CHILDYQ")) * Child) '+ ((IXArray(ictr1)("FUEL_Inf") * dTotalNo_inf)
                    row("TotalTax") = totTax + totFS
                    row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk + totFS
                    row("DisCount") = "0"
                    row("OriginalTF") = row("TotalFare") '(IXArray(ictr1)("TotalPay") 'totBFWInf + totTax
                    row("OriginalTT") = totTax + totFS
                    row("Track_id") = TrackId
                    row("FType") = Ft
                    CouponResTbl.Rows.Add(row)
                Next
            Next



        Catch ex As Exception

        End Try
        Return CouponResTbl

    End Function

    Private Function GetAirAsiaResult(ByVal AKCpnDt As DataTable, ByVal TourType As String) As DataTable
        Dim AKDt As New DataTable
        Dim AKResArrayO, AKResArrayR As Array
        If AKCpnDt.Rows.Count > 0 Then


            AKDt = AKCpnDt.Clone()
            AKDt.Clear()
            Dim AirDataColumn As DataColumn
            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Leg"
            AKDt.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Flight1"
            AKDt.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "LineItemNumber"
            AKDt.Columns.Add(AirDataColumn)
            Dim cnt As Integer = 1
            If TourType = "RoundTrip" Then
                Dim aDt As New DataTable
                aDt = AKCpnDt.DefaultView.ToTable(True, "LINENO")
                For k As Integer = 0 To aDt.Rows.Count - 1
                    AKResArrayO = AKCpnDt.Select("TRIPTYPE='O' and ADULTTOTALFARE<>'0' and LINENO='" & k + 1 & "'", "")
                    AKResArrayR = AKCpnDt.Select("TRIPTYPE='R' and ADULTTOTALFARE<>'0' and LINENO='" & k + 1 & "'", "")
                    If AKResArrayO.Length > 0 Then
                        For i As Integer = 0 To AKResArrayO.Length - 1
                            AKDt.ImportRow(AKResArrayO(i))
                            AKDt.Rows(AKDt.Rows.Count - 1)("Leg") = (i + 1).ToString
                            AKDt.Rows(AKDt.Rows.Count - 1)("Flight1") = "1"
                            AKDt.Rows(AKDt.Rows.Count - 1)("LineItemNumber") = k + 1
                        Next
                    End If
                    If AKResArrayR.Length > 0 Then
                        For ii As Integer = 0 To AKResArrayR.Length - 1
                            AKDt.ImportRow(AKResArrayR(ii))
                            AKDt.Rows(AKDt.Rows.Count - 1)("Leg") = (ii + 1).ToString
                            AKDt.Rows(AKDt.Rows.Count - 1)("Flight1") = "2"
                            AKDt.Rows(AKDt.Rows.Count - 1)("LineItemNumber") = k + 1

                        Next
                    End If
                Next

            Else
                Dim aDt As New DataTable
                aDt = AKCpnDt.DefaultView.ToTable(True, "LINENO")
                For k As Integer = 0 To aDt.Rows.Count - 1
                    AKResArrayO = AKCpnDt.Select("TRIPTYPE='O' and ADULTTOTALFARE<>'0' and LINENO='" & k + 1 & "'", "")
                    If AKResArrayO.Length > 0 Then
                        For i As Integer = 0 To AKResArrayO.Length - 1
                            AKDt.ImportRow(AKResArrayO(i))
                            AKDt.Rows(AKDt.Rows.Count - 1)("Leg") = (i + 1).ToString
                            AKDt.Rows(AKDt.Rows.Count - 1)("Flight1") = "1"
                            AKDt.Rows(AKDt.Rows.Count - 1)("LineItemNumber") = k + 1

                        Next
                    End If
                Next
            End If
        End If
        Return AKDt
    End Function

    Private Function GetCalcFareAirAsia(ByVal AKArray As Array, ByVal PaxType As String) As Hashtable
        Dim BF As Double = 0, YQ As Double = 0, OT As Double = 0, TC As Double = 0, TF As Double = 0
        Dim AKFareHT As New Hashtable
        Dim isOfr As Boolean = False, isRfr As Boolean = False
        For i As Integer = 0 To AKArray.Length - 1
            If (AKArray(i)("TRIPTYPE")) = "O" And isOfr = False Then
                isOfr = True
                BF += Convert.ToDouble((AKArray(i)(PaxType & "BASEFARE")))
                YQ += Convert.ToDouble((AKArray(i)(PaxType & "YQ")))
                OT += Convert.ToDouble((AKArray(i)(PaxType & "OTHERTAX")))
                TC += Convert.ToDouble((AKArray(i)(PaxType & "TC")))
                TF += Convert.ToDouble((AKArray(i)(PaxType & "TOTALFARE")))
            End If
            If (AKArray(i)("TRIPTYPE")) = "R" And isRfr = False Then
                isRfr = True
                BF += Convert.ToDouble((AKArray(i)(PaxType & "BASEFARE")))
                YQ += Convert.ToDouble((AKArray(i)(PaxType & "YQ")))
                OT += Convert.ToDouble((AKArray(i)(PaxType & "OTHERTAX")))
                TC += Convert.ToDouble((AKArray(i)(PaxType & "TC")))
                TF += Convert.ToDouble((AKArray(i)(PaxType & "TOTALFARE")))
            End If
        Next
        AKFareHT.Add("BF", BF)
        AKFareHT.Add("YQ", YQ)
        AKFareHT.Add("OT", OT)
        AKFareHT.Add("TC", TC)
        AKFareHT.Add("TF", TF)

        Return AKFareHT
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

    Private Function convertTimeFormat(ByVal time As String) As String
        Dim mm_str As String
        If Right(time, 2) = "PM" Then
            Select Case Left(time, 2)
                Case "01"
                    mm_str = time.Replace(Left(time, 3), "13:")
                Case "02"
                    mm_str = time.Replace(Left(time, 3), "14:")
                Case "03"
                    mm_str = time.Replace(Left(time, 3), "15:")
                Case "04"
                    mm_str = time.Replace(Left(time, 3), "16:")
                Case "05"
                    mm_str = time.Replace(Left(time, 3), "17:")
                Case "06"
                    mm_str = time.Replace(Left(time, 3), "18:")
                Case "07"
                    mm_str = time.Replace(Left(time, 3), "19:")
                Case "08"
                    mm_str = time.Replace(Left(time, 3), "20:")
                Case "09"
                    mm_str = time.Replace(Left(time, 3), "21:")
                Case "10"
                    mm_str = time.Replace(Left(time, 3), "22:")
                Case "11"
                    mm_str = time.Replace(Left(time, 3), "23:")
                Case "12"
                    mm_str = time.Replace(Left(time, 3), "12:")
                Case Else
                    mm_str = time
            End Select
        Else
            mm_str = time
        End If
        Return Left(mm_str, 5)
    End Function
End Class
