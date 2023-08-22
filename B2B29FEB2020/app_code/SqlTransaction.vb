Imports Microsoft.VisualBasic
Imports System.Data
Public Class SqlTransaction

    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable

    Public Function insertPaxDetails(ByVal OrderId As String, ByVal Title As String, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal PaxType As String, ByVal DOB As String, ByVal FFNumber As String, ByVal FFAirline As String, ByVal MealType As String, ByVal SeatType As String, ByVal IsPrimary As Boolean, ByVal AssociatedPaxName As String, ByVal Gender As String, ByVal PassportExpireDate As String, ByVal PassportNo As String, ByVal IssueCountryCode As String, ByVal NationalityCode As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", OrderId)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@FName", FName)
        paramHashtable.Add("@MName", MName)
        paramHashtable.Add("@LName", LName)
        paramHashtable.Add("@PaxType", PaxType)
        paramHashtable.Add("@DOB", DOB)
        paramHashtable.Add("@FFNumber", FFNumber)
        paramHashtable.Add("@FFAirline", FFAirline)
        paramHashtable.Add("@MealType", MealType)
        paramHashtable.Add("@SeatType", SeatType)
        paramHashtable.Add("@IsPrimary", IsPrimary)
        paramHashtable.Add("@InfAssociatePaxName", AssociatedPaxName)
        paramHashtable.Add("@Gender", Gender)
        paramHashtable.Add("@PassportExpireDate", PassportExpireDate)
        paramHashtable.Add("@PassportNo", PassportNo)
        paramHashtable.Add("@IssueCountryCode", IssueCountryCode)
        paramHashtable.Add("@NationalityCode", NationalityCode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltPaxDetails", 1)
    End Function

    Public Function insertFlightDetails(ByVal FltDs As DataSet) As Integer
        Try
            For i As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                paramHashtable.Clear()
                paramHashtable.Add("@OrderID", FltDs.Tables(0).Rows(0)("track_id").ToString.Trim)
                paramHashtable.Add("@DCOrAC", FltDs.Tables(0).Rows(i)("DepartureLocation").ToString.Trim)
                paramHashtable.Add("@DCOrAN", FltDs.Tables(0).Rows(i)("DepartureCityName").ToString.Trim)
                paramHashtable.Add("@ACOrAC", FltDs.Tables(0).Rows(i)("ArrivalLocation").ToString.Trim)
                paramHashtable.Add("@ACOrAN", FltDs.Tables(0).Rows(i)("ArrivalCityName").ToString.Trim)
                Dim depdt As String = ""
                Dim arrdt As String = "" 'Right("20121130", 2) & Mid("20110930",5, 2) & Mid("20110930", 3, 2)
                If FltDs.Tables(0).Rows(i)("Provider").ToString.Trim = "1G" Then
                    depdt = Right(FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim, 2) & Mid(FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim, 5, 2) & Mid(FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim, 3, 2)
                    arrdt = Right(FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim, 2) & Mid(FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim, 5, 2) & Mid(FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim, 3, 2)
                Else
                    depdt = FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim
                    arrdt = FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim
                End If
                paramHashtable.Add("@DDate", depdt)
                paramHashtable.Add("@DTime", FltDs.Tables(0).Rows(i)("DepartureTime").ToString.Trim)
                paramHashtable.Add("@ADate", arrdt)
                'paramHashtable.Add("@DDate", FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim)
                'paramHashtable.Add("@DTime", FltDs.Tables(0).Rows(i)("DepartureTime").ToString.Trim)
                'paramHashtable.Add("@ADate", FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim)
                paramHashtable.Add("@ATime", FltDs.Tables(0).Rows(i)("ArrivalTime").ToString.Trim)
                paramHashtable.Add("@AirCode", FltDs.Tables(0).Rows(i)("MarketingCarrier").ToString.Trim)
                paramHashtable.Add("@AirName", FltDs.Tables(0).Rows(i)("AirLineName").ToString.Trim)
                paramHashtable.Add("@FltNo", FltDs.Tables(0).Rows(i)("FlightIdentification").ToString.Trim)
                paramHashtable.Add("@AirCraft", FltDs.Tables(0).Rows(i)("EQ").ToString.Trim)
                paramHashtable.Add("@AdtFB", FltDs.Tables(0).Rows(i)("fareBasis").ToString.Trim)
                paramHashtable.Add("@ChdFB", "")
                paramHashtable.Add("@InfFB", "")
                paramHashtable.Add("@AdtRbd", FltDs.Tables(0).Rows(i)("AdtRbd").ToString.Trim)
                paramHashtable.Add("@ChdRbd", FltDs.Tables(0).Rows(i)("ChdRbd").ToString.Trim)
                paramHashtable.Add("@InfRbd", "")
                paramHashtable.Add("@AvlSeat", 0)
                paramHashtable.Add("@UpdateDate", DateTime.Now)
                objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)
            Next
        Catch ex As Exception

        End Try


    End Function


    Public Function GetCacheStatusForOrderIds(ByVal orderID As String, ByVal OrderIDR As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", orderID.Trim())
        paramHashtable.Add("@OrderIdR", OrderIDR.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "usp_GetCacheStatusForOrderIds ", 3)
    End Function

    Public Function insertFlightDetailsIntl(ByVal FltDs As DataSet) As Integer
        Try
            For i As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                paramHashtable.Clear()
                paramHashtable.Add("@OrderID", FltDs.Tables(0).Rows(0)("track_id").ToString.Trim)
                paramHashtable.Add("@DCOrAC", FltDs.Tables(0).Rows(i)("DepartureLocation").ToString.Trim)
                paramHashtable.Add("@DCOrAN", FltDs.Tables(0).Rows(i)("DepartureCityName").ToString.Trim)
                paramHashtable.Add("@ACOrAC", FltDs.Tables(0).Rows(i)("ArrivalLocation").ToString.Trim)
                paramHashtable.Add("@ACOrAN", FltDs.Tables(0).Rows(i)("ArrivalCityName").ToString.Trim)
                paramHashtable.Add("@DDate", FltDs.Tables(0).Rows(i)("DepartureDate").ToString.Trim)
                paramHashtable.Add("@DTime", FltDs.Tables(0).Rows(i)("DepartureTime").ToString.Trim)
                paramHashtable.Add("@ADate", FltDs.Tables(0).Rows(i)("ArrivalDate").ToString.Trim)
                paramHashtable.Add("@ATime", FltDs.Tables(0).Rows(i)("ArrivalTime").ToString.Trim)
                paramHashtable.Add("@AirCode", FltDs.Tables(0).Rows(i)("MarketingCarrier").ToString.Trim)
                paramHashtable.Add("@AirName", FltDs.Tables(0).Rows(i)("AirLineName").ToString.Trim)
                paramHashtable.Add("@FltNo", FltDs.Tables(0).Rows(i)("FlightIdentification").ToString.Trim)
                paramHashtable.Add("@AirCraft", FltDs.Tables(0).Rows(i)("EQ").ToString.Trim)
                paramHashtable.Add("@AdtFB", FltDs.Tables(0).Rows(i)("fareBasis").ToString.Trim)
                paramHashtable.Add("@ChdFB", "")
                paramHashtable.Add("@InfFB", "")
                paramHashtable.Add("@AdtRbd", "")
                paramHashtable.Add("@ChdRbd", "")
                paramHashtable.Add("@InfRbd", "")
                paramHashtable.Add("@AvlSeat", 0)
                paramHashtable.Add("@UpdateDate", DateTime.Now)
                objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)
            Next
        Catch ex As Exception

        End Try


    End Function

    Public Function insertFareDetails(ByVal FltDs As DataSet, ByVal Trip As String) As Integer
        Dim BaseFare As Integer = 0, FareType As String = FltDs.Tables(0).Rows(0)("AdtFareType")
        Dim AdMrk As Integer = 0, AgMrk As Integer = 0, totDis As Integer = 0, CB As Integer = 0, totDis1 As Integer = 0, totTax As Integer = 0
        Dim tds As Integer = 0, tax() As String
        Dim ClsCorporate As New ClsCorporate()
        If Val(FltDs.Tables(0).Rows(0)("Adult")) > 0 Then
            Try
                BaseFare = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtBFare"))
                tax = FltDs.Tables(0).Rows(0)("Adt_Tax").ToString.Split("#")
                totTax = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtTax"))
                AdMrk = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ADTAdminMrk"))
                AgMrk = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ADTAgentMrk"))
                totDis = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtComm"))
                totDis1 = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtComm1"))
                tds = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtTds"))
                CB = Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtCB"))
                ' calcFare(tax, FltDs.Tables(0).Rows(0)("Track_id"), "ADT", BaseFare, AdMrk, AgMrk, 0, totDis + CB, CB, tds, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip)
                If (Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsCorp")) = True) Then
                    ClsCorporate.calcFareCorp(tax, FltDs.Tables(0).Rows(0)("Track_id"), "ADT", BaseFare, AdMrk, AgMrk, 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtMgtFee")), 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("AdtSrvTax")), 0, 0, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip)

                Else
                    calcFare(tax, FltDs.Tables(0).Rows(0)("Track_id"), "ADT", BaseFare, AdMrk, AgMrk, 0, totDis + CB, CB, tds, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip, totDis1 + CB, FareType, totTax)

                End If
            Catch ex As Exception

            End Try
        End If
        If Val(FltDs.Tables(0).Rows(0)("Child")) > 0 Then
            BaseFare = 0
            AdMrk = 0
            AgMrk = 0
            totDis = 0
            totDis1 = 0
            tds = 0
            CB = 0
            Try
                BaseFare = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdBFare"))
                tax = FltDs.Tables(0).Rows(0)("Chd_Tax").ToString.Split("#")
                totTax = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdTax"))
                AdMrk = Convert.ToInt32(FltDs.Tables(0).Rows(0)("CHDAdminMrk"))
                AgMrk = Convert.ToInt32(FltDs.Tables(0).Rows(0)("CHDAgentMrk"))
                totDis = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdComm"))
                totDis1 = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdComm1"))
                tds = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdTds"))
                CB = Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdCB"))

                'calcFare(tax, FltDs.Tables(0).Rows(0)("Track_id"), "CHD", BaseFare, AdMrk, AgMrk, 0, totDis + CB, CB, tds, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip)

                If (Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsCorp")) = True) Then
                    ClsCorporate.calcFareCorp(tax, FltDs.Tables(0).Rows(0)("Track_id"), "CHD", BaseFare, AdMrk, AgMrk, 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdMgtFee")), 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("ChdSrvTax")), 0, 0, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip)

                Else
                    calcFare(tax, FltDs.Tables(0).Rows(0)("Track_id"), "CHD", BaseFare, AdMrk, AgMrk, 0, totDis + CB, CB, tds, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip, totDis1 + CB, FareType, totTax)

                End If
            Catch ex As Exception
            End Try
        End If
        If Val(FltDs.Tables(0).Rows(0)("Infant")) > 0 Then
            BaseFare = 0
            AdMrk = 0
            AgMrk = 0
            totDis = 0
            totDis1 = 0
            tds = 0
            CB = 0
            Try
                BaseFare = Convert.ToInt32(FltDs.Tables(0).Rows(0)("InfBFare"))
                tax = FltDs.Tables(0).Rows(0)("Inf_Tax").ToString.Split("#")
                totTax = Convert.ToInt32(FltDs.Tables(0).Rows(0)("InfTax"))
                AdMrk = 0
                AgMrk = 0
                totDis = 0
                tds = 0

                If (Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsCorp")) = True) Then
                    ClsCorporate.calcFareCorp(tax, FltDs.Tables(0).Rows(0)("Track_id"), "INF", BaseFare, AdMrk, AgMrk, 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("InfMgtFee")), 0, Convert.ToInt32(FltDs.Tables(0).Rows(0)("InfSrvTax")), 0, 0, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip)

                Else
                    calcFare(tax, FltDs.Tables(0).Rows(0)("Track_id"), "INF", BaseFare, AdMrk, AgMrk, 0, totDis + CB, CB, tds, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), Trip, totDis1 + CB, FareType, totTax)

                End If
            Catch ex As Exception
            End Try
        End If
    End Function

    'Added 05/04/2014 Manish
    Public Function insert_MEAL_BAGDetails(ByVal OrderId As String, ByVal Flt_HeaderID As String, ByVal PaxID As Long, ByVal TripType As String, ByVal MealCode As String, ByVal MealPrice As Double, ByVal BaggageCode As String, ByVal BaggagePrice As String, ByVal VC As String, ByVal BaggageDesc As String, ByVal BaggageCategory As String, ByVal BagPriceWithNoTax As Decimal, ByVal MealDesc As String, ByVal MealCategory As String, ByVal MealPriceWithNoTax As String, ByVal SeatCode As String, ByVal SeatPrice As String, ByVal SeatDesc As String, ByVal SeatCategory As String, ByVal SeatPriceWithNoTax As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@BREF", OrderId)
        paramHashtable.Add("@FltHeader", Flt_HeaderID)
        paramHashtable.Add("@PaxID", PaxID)
        paramHashtable.Add("@TRIP", TripType)
        paramHashtable.Add("@MEAL_SSR", MealCode)
        paramHashtable.Add("@MPRICE", MealPrice)
        paramHashtable.Add("@BAG_SSR", BaggageCode)
        paramHashtable.Add("@BPRICE", BaggagePrice)
        paramHashtable.Add("@BAGDesc", BaggageDesc)
        paramHashtable.Add("@BAGCategory", BaggageCategory)
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@BagPriceWithNoTax", BagPriceWithNoTax)

        paramHashtable.Add("@MealDesc", MealDesc)
        paramHashtable.Add("@MealCategory", MealCategory)
        paramHashtable.Add("@MealPriceWithNoTax", MealPriceWithNoTax)
        paramHashtable.Add("@SEAT_SSR", SeatCode)
        paramHashtable.Add("@SPRICE", SeatPrice)
        paramHashtable.Add("@SeatDesc", SeatDesc)
        paramHashtable.Add("@SeatCategory", SeatCategory)
        paramHashtable.Add("@SeatPriceWithNoTax", SeatPriceWithNoTax)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_MEAL_BAGG", 1)
    End Function
    'Public Function insert_MEAL_BAGDetails(ByVal OrderId As String, ByVal Flt_HeaderID As String, ByVal PaxID As Long, ByVal TripType As String, ByVal MealCode As String, ByVal MealPrice As Double, ByVal BaggageCode As String, ByVal BaggagePrice As String, ByVal VC As String, Optional ByVal BaggageDesc As String = "", Optional ByVal BaggageCategory As String = "", Optional ByVal BagPriceWithNoTax As Decimal = 0, Optional ByVal MealDesc As String = "", Optional ByVal Prvdr As String = "") As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@BREF", OrderId)
    '    paramHashtable.Add("@FltHeader", Flt_HeaderID)
    '    paramHashtable.Add("@PaxID", PaxID)
    '    paramHashtable.Add("@TRIP", TripType)
    '    paramHashtable.Add("@MEAL_SSR", MealCode)
    '    paramHashtable.Add("@MPRICE", MealPrice)
    '    paramHashtable.Add("@BAG_SSR", BaggageCode)
    '    paramHashtable.Add("@BPRICE", BaggagePrice)
    '    paramHashtable.Add("@VC", VC)
    '    paramHashtable.Add("@BAGDesc", BaggageDesc)
    '    paramHashtable.Add("@BAGCategory", BaggageCategory)
    '    paramHashtable.Add("@BagPriceWithNoTax", BagPriceWithNoTax)
    '    paramHashtable.Add("@MealDesc", MealDesc)
    '    paramHashtable.Add("@Provider", Prvdr)


    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_MEAL_BAGG", 1)
    'End Function

    ' GET PAX DETAILS FOR MEAL AND BAGGAGE
    Public Function Get_MEAL_BAG_PaxDetails(ByVal OrderId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OID", OrderId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_SELECT_PAX", 3)
    End Function

    'Public Function GetSSR_Meal(ByVal Trip As String, ByVal VC As String, ByVal AT As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@Trip", Trip)
    '    paramHashtable.Add("@VC", VC)
    '    paramHashtable.Add("@AT", AT)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSSR_MEAL", 3)
    'End Function
    Public Function GetSSR_Meal(ByVal Trip As String, ByVal VC As String, ByVal AT As String, ByVal Orderid As String, ByVal Flight As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Flight", Flight)
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@AT", AT)
        paramHashtable.Add("@oid", Orderid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSSR_MEAL", 3)
    End Function
    Public Function GetSSR_EB(ByVal Trip As String, ByVal VC As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@VC", VC)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSSR_EB", 3)
    End Function

    'Public Sub calcFare(ByVal tax() As String, ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String)
    '    paramHashtable.Clear()
    '    Dim tax1() As String
    '    Dim YQ As Integer = 0, YR As Integer = 0, WO As Integer = 0, OT As Integer = 0, totTax As Integer = 0
    '    Dim totFare As Integer = 0, netFare As Integer = 0, TF As Integer = 0, SrvTax As Double = 0, TF1 As Double = 0, SrvTax1 As Double = 0
    '    Dim ds As DataSet
    '    Try
    '        ds = calcServicecharge(vc, trip)
    '        SrvTax1 = ds.Tables(0).Rows(0)("SrvTax")
    '        TF1 = ds.Tables(0).Rows(0)("TranFee")
    '        SrvTax = Math.Round(((basefare * SrvTax1) / 100), 0)
    '        'TF = 0
    '    Catch ex As Exception
    '    End Try
    '    Try
    '        For i As Integer = 0 To tax.Length - 2
    '            If InStr(tax(i), "YQ") Then
    '                tax1 = tax(i).Split(":")
    '                YQ = YQ + Convert.ToInt32(tax1(1))
    '            ElseIf InStr(tax(i), "YR") Then
    '                tax1 = tax(i).Split(":")
    '                YR = YR + Convert.ToInt32(tax1(1))
    '            ElseIf InStr(tax(i), "WO") Then
    '                tax1 = tax(i).Split(":")
    '                WO = WO + Convert.ToInt32(tax1(1))
    '            Else
    '                tax1 = tax(i).Split(":")
    '                OT = OT + Convert.ToInt32(tax1(1))
    '            End If
    '        Next
    '        If paxtype <> "INF" Then
    '            TF = ((basefare + YQ) * TF1) / 100
    '        End If
    '        totTax = YQ + YR + WO + OT
    '        totFare = basefare + totTax + SrvTax + TF + admrk
    '        netFare = (totFare + tds) - comm
    '    Catch ex As Exception

    '    End Try
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@OrderID", trackid)
    '    paramHashtable.Add("@BaseFare", basefare)
    '    paramHashtable.Add("@YQ", YQ)
    '    paramHashtable.Add("@YR", YR)
    '    paramHashtable.Add("@WO", WO)
    '    paramHashtable.Add("@OT", OT)
    '    paramHashtable.Add("@TotalTax", totTax)
    '    paramHashtable.Add("@TotalFare", totFare)
    '    paramHashtable.Add("@ServiceTax", SrvTax)
    '    paramHashtable.Add("@TranFee", TF)
    '    paramHashtable.Add("@AdminMrk", admrk)
    '    paramHashtable.Add("@AgentMrk", agmrk)
    '    paramHashtable.Add("@DistrMrk", dismrk)
    '    paramHashtable.Add("@TotalDiscount", comm)
    '    paramHashtable.Add("@PLb", 0)
    '    paramHashtable.Add("@Discount", 0)
    '    paramHashtable.Add("@CashBack", 0)
    '    paramHashtable.Add("@Tds", tds)
    '    paramHashtable.Add("@TdsOn", comm)
    '    paramHashtable.Add("@TotalAfterDis", netFare)
    '    paramHashtable.Add("@PaxType", paxtype)
    '    paramHashtable.Add("@UpdateDate", DateTime.Now)
    '    objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    'End Sub
    'Public Sub calcFare(ByVal tax() As String, ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal cb As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String)
    '    paramHashtable.Clear()
    '    Dim tax1() As String
    '    Dim YQ As Integer = 0, YR As Integer = 0, WO As Integer = 0, OT As Integer = 0, QTax As Integer = 0, totTax As Integer = 0
    '    Dim totFare As Integer = 0, netFare As Integer = 0, TF As Integer = 0, SrvTax As Double = 0, TF1 As Double = 0, SrvTax1 As Double = 0

    '    Dim ds As New DataSet
    '    ds = calcServicecharge(vc, trip)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        TF1 = ds.Tables(0).Rows(0)("TranFee")
    '    End If



    '    Try
    '        For i As Integer = 0 To tax.Length - 2
    '            If InStr(tax(i), "YQ") Then
    '                tax1 = tax(i).Split(":")
    '                YQ = YQ + Convert.ToInt32(tax1(1))
    '            ElseIf InStr(tax(i), "YR") Then
    '                tax1 = tax(i).Split(":")
    '                YR = YR + Convert.ToInt32(tax1(1))
    '            ElseIf InStr(tax(i), "WO") Then
    '                tax1 = tax(i).Split(":")
    '                WO = WO + Convert.ToInt32(tax1(1))
    '            ElseIf InStr(tax(i), "Q") Then
    '                tax1 = tax(i).Split(":")
    '                QTax = QTax + Convert.ToInt32(tax1(1))
    '            Else
    '                tax1 = tax(i).Split(":")
    '                OT = OT + Convert.ToInt32(tax1(1))
    '            End If
    '        Next
    '        If paxtype <> "INF" Then
    '            TF = ((basefare + YQ) * TF1) / 100
    '        End If

    '        Try
    '            SrvTax1 = ds.Tables(0).Rows(0)("SrvTax")
    '            If vc.ToUpper.Trim = "6E" OrElse vc.ToUpper.Trim = "SG" OrElse vc.ToUpper.Trim = "G8" Then
    '                SrvTax = Math.Round(((((comm - cb) - TF) * SrvTax1) / 100), 0)
    '            Else
    '                SrvTax = Math.Round((((comm - cb) * SrvTax1) / 100), 0)
    '            End If
    '        Catch ex As Exception
    '        End Try


    '        totTax = YQ + YR + WO + OT
    '        totFare = basefare + totTax + SrvTax + TF + admrk
    '        netFare = (totFare + tds) - comm
    '    Catch ex As Exception

    '    End Try




    '    paramHashtable.Clear()
    '    paramHashtable.Add("@OrderID", trackid)
    '    paramHashtable.Add("@BaseFare", basefare)
    '    paramHashtable.Add("@YQ", YQ)
    '    paramHashtable.Add("@YR", YR)
    '    paramHashtable.Add("@WO", WO)
    '    paramHashtable.Add("@OT", OT)
    '    paramHashtable.Add("@Qtax", QTax)
    '    paramHashtable.Add("@TotalTax", totTax)
    '    paramHashtable.Add("@TotalFare", totFare)
    '    paramHashtable.Add("@ServiceTax", SrvTax)
    '    paramHashtable.Add("@TranFee", TF)
    '    paramHashtable.Add("@AdminMrk", admrk)
    '    paramHashtable.Add("@AgentMrk", agmrk)
    '    paramHashtable.Add("@DistrMrk", dismrk)
    '    paramHashtable.Add("@TotalDiscount", comm)
    '    paramHashtable.Add("@PLb", 0)
    '    paramHashtable.Add("@Discount", comm - cb)
    '    paramHashtable.Add("@CashBack", cb)
    '    paramHashtable.Add("@Tds", tds)
    '    paramHashtable.Add("@TdsOn", comm - cb)
    '    paramHashtable.Add("@TotalAfterDis", netFare)
    '    paramHashtable.Add("@PaxType", paxtype)
    '    paramHashtable.Add("@UpdateDate", DateTime.Now)
    '    objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    'End Sub

    Public Sub calcFare(ByVal tax() As String, ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal cb As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String, ByVal comm1 As Integer, ByVal FareType As String, ByVal totTaxAll As Integer)
        paramHashtable.Clear()
        Dim tax1() As String
        Dim YQ As Integer = 0, YR As Integer = 0, WO As Integer = 0, OT As Integer = 0, QTax As Integer = 0, totTax As Double = 0
        Dim totFare As Integer = 0, netFare As Integer = 0, TF As Integer = 0, SrvTax As Double = 0, TF1 As Double = 0, SrvTax1 As Double = 0
        Dim originalSrvTax As Double = 0
        Dim JN As Double = 0, K3 As Double = 0, F2 As Double = 0, G1 As Double = 0, YM As Double = 0

        Dim ds As New DataSet
        ds = calcServicecharge(vc, trip)
        If ds.Tables(0).Rows.Count > 0 Then
            TF1 = ds.Tables(0).Rows(0)("TranFee")
        End If



        Try
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    YQ = YQ + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "YR") Then
                    tax1 = tax(i).Split(":")
                    YR = YR + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "WO") Then
                    tax1 = tax(i).Split(":")
                    WO = WO + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "JN") Then
                    tax1 = tax(i).Split(":")
                    JN = JN + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "K3") Then
                    tax1 = tax(i).Split(":")
                    K3 = K3 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "F2") Then
                    tax1 = tax(i).Split(":")
                    F2 = F2 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "G1") Then
                    tax1 = tax(i).Split(":")
                    G1 = G1 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "YM") Then
                    tax1 = tax(i).Split(":")
                    YM = YM + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "Q") Then
                    tax1 = tax(i).Split(":")
                    QTax = QTax + Convert.ToInt32(tax1(1))
                Else
                    tax1 = tax(i).Split(":")
                    OT = OT + Convert.ToInt32(tax1(1))
                End If
            Next
            If paxtype <> "INF" Then
                TF = ((basefare + YQ) * TF1) / 100
            End If
            Try
                SrvTax1 = ds.Tables(0).Rows(0)("SrvTax")
                If vc.ToUpper.Trim = "6E" OrElse vc.ToUpper.Trim = "SG" OrElse vc.ToUpper.Trim = "G8" Then
                    ' SrvTax = Math.Round(((((comm - cb) - TF) * SrvTax1) / 100), 0)
                    originalSrvTax = Math.Round(((((comm1 - cb) - TF) * SrvTax1) / 100), 0)
                Else
                    ' SrvTax = Math.Round((((comm - cb) * SrvTax1) / 100), 0)
                    originalSrvTax = Math.Round(((((comm1 - cb) - TF) * SrvTax1) / 100), 0)
                End If
            Catch ex As Exception
            End Try

            SrvTax = 0
            ''totTax = YQ + YR + WO + OT + JN + K3 + F2 + G1 + YM
            totTax = totTaxAll
            totFare = basefare + totTax + SrvTax + TF + admrk
            netFare = (totFare + tds) - comm
        Catch ex As Exception

        End Try




        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", trackid)
        paramHashtable.Add("@BaseFare", basefare)
        paramHashtable.Add("@YQ", YQ)
        paramHashtable.Add("@YR", YR)
        paramHashtable.Add("@WO", WO)
        paramHashtable.Add("@OT", OT)
        paramHashtable.Add("@Qtax", QTax)
        paramHashtable.Add("@TotalTax", totTax)
        paramHashtable.Add("@TotalFare", totFare)
        paramHashtable.Add("@ServiceTax", SrvTax)
        paramHashtable.Add("@TranFee", TF)
        paramHashtable.Add("@AdminMrk", admrk)
        paramHashtable.Add("@AgentMrk", agmrk)
        paramHashtable.Add("@DistrMrk", dismrk)
        paramHashtable.Add("@TotalDiscount", comm)
        paramHashtable.Add("@PLb", 0)
        paramHashtable.Add("@Discount", comm - cb)
        paramHashtable.Add("@CashBack", cb)
        paramHashtable.Add("@Tds", tds)
        paramHashtable.Add("@TdsOn", comm - cb)
        paramHashtable.Add("@TotalAfterDis", netFare)
        paramHashtable.Add("@PaxType", paxtype)
        paramHashtable.Add("@UpdateDate", DateTime.Now)
        paramHashtable.Add("@ServiceTax1", originalSrvTax)
        paramHashtable.Add("@Discount1", comm1)
        paramHashtable.Add("@FareType", FareType)
        paramHashtable.Add("@JN", JN)
        paramHashtable.Add("@K3", K3)
        paramHashtable.Add("@F2", F2)
        paramHashtable.Add("@G1", G1)
        paramHashtable.Add("@YM", YM)
        objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    End Sub



    Public Function calcServicecharge(ByVal vc As String, ByVal trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@vc", vc)
        paramHashtable.Add("@trip", trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ServiceCharge", 3)
    End Function

    Public Function insertFltHdrDetails(ByVal FltDs As DataSet, ByVal AgncyDs As DataSet, ByVal uid As String, ByVal PgTitle As String, ByVal PgFName As String, ByVal PgLName As String, ByVal PgMobile As String, ByVal PgEmail As String, ByVal Trip As String, ByVal projectId As String, ByVal bookedBy As String, ByVal billNoCorp As String, ByVal FareType As String, ByVal BookingRefNo As String) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@OrderID", FltDs.Tables(0).Rows(0)("Track_id"))
            paramHashtable.Add("@sector", FltDs.Tables(0).Rows(0)("Sector"))
            paramHashtable.Add("@Status", "Request")
            paramHashtable.Add("@VC", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString.Trim.ToUpper)
            paramHashtable.Add("@Duration", FltDs.Tables(0).Rows(0)("Tot_Dur"))
            paramHashtable.Add("@TripType", FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType"))
            paramHashtable.Add("@Trip", Trip)
            paramHashtable.Add("@TourCode", "")
            paramHashtable.Add("@TotalBookingCost", FltDs.Tables(0).Rows(0)("totFare"))
            paramHashtable.Add("@TotalAfterDis", FltDs.Tables(0).Rows(0)("netFare"))
            paramHashtable.Add("@AdditionalMarkup", 0)
            paramHashtable.Add("@Adult", Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult")))
            paramHashtable.Add("@Child", Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child")))
            paramHashtable.Add("@Infant", Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant")))
            paramHashtable.Add("@AgentId", uid)
            paramHashtable.Add("@DistrId", AgncyDs.Tables(0).Rows(0)("Distr"))
            paramHashtable.Add("@PaymentType", "CL")
            paramHashtable.Add("@Ttl", PgTitle)
            paramHashtable.Add("@FName", PgFName)
            paramHashtable.Add("@LName", PgLName)
            paramHashtable.Add("@Mobile", PgMobile)
            paramHashtable.Add("@Mail", PgEmail)
            paramHashtable.Add("@ProjectID", projectId)
            paramHashtable.Add("@BillNoCorp", billNoCorp)
            paramHashtable.Add("@BookedBy", bookedBy)
            paramHashtable.Add("@FareType", FareType)
            paramHashtable.Add("@ReferenceNo", BookingRefNo)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltHeader", 1)

        Catch ex As Exception
            Return 0
        End Try

    End Function


    'Public Function GetAgencyDetails(ByVal UserId As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@UserId", UserId)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetails", 3)
    'End Function
    Public Function GetFltDtlsInt(ByVal trackid As String, ByVal UserId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        paramHashtable.Add("@UserId", UserId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSelectedFltDtls", 3)
    End Function

    Public Function GetFDDSectorDetails(Optional ByVal odfrom As String = "", Optional ByVal odto As String = "", Optional ByVal deptime As String = "", Optional ByVal arrtime As String = "", Optional ByVal carrier As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrgDestFrom", odfrom)
        paramHashtable.Add("@OrgDestTo", odto)
        paramHashtable.Add("@DepTime", deptime)
        paramHashtable.Add("@ArrTime", arrtime)
        paramHashtable.Add("@Carrier", carrier)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GetFDDFlightSearchResults", 3)
    End Function


    Public Function GetFDDOfferExpireSoon() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_FDDOfferExpireSoon", 3)
    End Function

    Public Function GetFltDtls(ByVal trackid As String, ByVal UserId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        paramHashtable.Add("@UserId", UserId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSelectedFltDtls_Gal", 3)
    End Function
    Public Function GetDomIntTrip(ByVal Dest As String, ByVal Arr As String, ByVal Action As String, ByVal OrderID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Dest", Dest)
        paramHashtable.Add("@Arr", Arr)
        paramHashtable.Add("@Action", Action)
        paramHashtable.Add("@OrderID", OrderID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSelectedDomInt", 3)
    End Function
    Public Function GetPaxDetails(ByVal trackid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetPaxDetails", 3)
    End Function
    Public Function GetPaxDetailsSeat(ByVal trackid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetPaxDetails_V1", 3)
    End Function
    'Public Function GetHdrDetails(ByVal trackid As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TrackId", trackid)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltHdr", 3)
    'End Function
    Public Function UpdateFltHeader(ByVal trackid As String, ByVal agname As String, ByVal gdspnr As String, ByVal airpnr As String, ByVal status As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", trackid)
        paramHashtable.Add("@Status", status)
        paramHashtable.Add("@AgName", agname)
        paramHashtable.Add("@GdsPnr", gdspnr)
        paramHashtable.Add("@AirlinePnr", airpnr)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateFltHeader", 1)
    End Function
    Public Function UpdateCrdLimit(ByVal uid As String, ByVal netFare As Double) As Double
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", uid)
        paramHashtable.Add("@netFare", netFare)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateCrdLimit", 2)
    End Function
    Public Function UpdateTransReport(ByVal uid As String, ByVal AgName As String, ByVal GdsPnr As String, ByVal Status As String, ByVal AvlBal As Double, ByVal totalFare As Double, ByVal netFare As Double, ByVal Remark As String, ByVal Sector As String, ByVal Paymenttype As String, ByVal vc As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", uid)
        paramHashtable.Add("@AgName", AgName)
        paramHashtable.Add("@GdsPnr", GdsPnr)
        paramHashtable.Add("@AvlBal", AvlBal)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@TotalFare", totalFare)
        paramHashtable.Add("@netFare", netFare)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@vc", vc)
        paramHashtable.Add("@Sector", Sector)
        paramHashtable.Add("@PaymentType", Paymenttype)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateTransReport", 1)
    End Function


    'Public Function GetFltDtlsReissue(ByVal trackid As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TrackId", trackid)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltDtls", 3)
    'End Function
    ' For Cancel Request
    Public Function CancelRequestIntl(ByVal PaxId As Integer, ByVal PaxType As String, ByVal sp As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@PaxType", PaxType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, sp, 3)
    End Function

    Public Function insertCancelationIntl(ByVal pnr As String, ByVal TktNo As String, ByVal sector As String, ByVal deparcher As String, ByVal distance As String, ByVal Title As String, ByVal FName As String, ByVal LName As String, ByVal PaxType As String, ByVal BookinfDate As String, ByVal DepartDate As String, ByVal TotalBookingCost As String, ByVal TotalAfterDis As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BaseFare As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TFee As String, ByVal Dis As String, ByVal CB As String, ByVal TDS As String, ByVal TotalFare As String, ByVal TotalFareAfterDis As String, ByVal VC As String, ByVal OrderID As String, ByVal DepTime As String, ByVal AirlinePNR As String, ByVal FNo As String, ByVal Remark As String, ByVal RefundID As String, ByVal sp As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@PNR", pnr)
            paramHashtable.Add("@Tkt_No", TktNo)
            paramHashtable.Add("@Sector", sector)
            paramHashtable.Add("@departure", deparcher)
            paramHashtable.Add("@destination", distance)
            paramHashtable.Add("@Title", Title)
            paramHashtable.Add("@pax_fname", FName)
            paramHashtable.Add("@pax_lname", LName)
            paramHashtable.Add("@PaxType", PaxType)
            paramHashtable.Add("@Booking_date", BookinfDate)
            paramHashtable.Add("@departure_date", DepartDate)
            paramHashtable.Add("@UserID", UserID)
            paramHashtable.Add("@Agency_Name", AgencyName)
            paramHashtable.Add("@Base_Fare", BaseFare)
            paramHashtable.Add("@Tax", Tax)
            paramHashtable.Add("@YQ", YQ)
            paramHashtable.Add("@Service_Tax", STax)
            paramHashtable.Add("@Tran_Fees", TFee)
            paramHashtable.Add("@Discount", Dis)
            paramHashtable.Add("@CB", CB)
            paramHashtable.Add("@TDS", TDS)
            paramHashtable.Add("@TotalFare", TotalFare)
            paramHashtable.Add("@TotalFareAfterDiscount", TotalAfterDis)
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@DepTime", DepTime)
            paramHashtable.Add("@AirlinePNR", AirlinePNR)
            paramHashtable.Add("@FNo", FNo)
            paramHashtable.Add("@RegardingCancel", Remark)
            paramHashtable.Add("@IDs", RefundID)
            paramHashtable.Add("@Status", Status.ToString)
            paramHashtable.Add("@VC", VC)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, sp, 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Public Function UpdateStatus_Pax_Header(ByVal orderId As String, ByVal sp As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@Orderid", orderId)
    '        paramHashtable.Add("@Status", Status.ToString)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, sp, 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function

    Public Function UpdateNew_RegsIntl(ByVal UserId As String, ByVal Amount As String, ByVal SP As String) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@UserId", UserId)
            paramHashtable.Add("@Amount", Amount)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 2)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function UpdateCancellationIntl(ByVal orderId As String, ByVal Remark As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@Orderid", orderId)
            paramHashtable.Add("@Status", Status.ToString)
            paramHashtable.Add("@Remark", Remark)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateCancellationIntl", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function



    'Public Function UpdateCancelIntlTbl(ByVal TktNo As String, ByVal charge As String, ByVal RefundFair As Integer, ByVal SP As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@TktNo", TktNo)
    '        paramHashtable.Add("@charge", charge)
    '        paramHashtable.Add("@RefundFair", RefundFair)
    '        paramHashtable.Add("@Status", Status.ToString)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function
    'Public Function UpdateReissueIntl(ByVal tktno As String, ByVal orderId As String, ByVal NewTktno As String, ByVal PNR As String, ByVal AirPnr As String, ByVal exec As String, ByVal Deptdate As String, ByVal deptime As String, ByVal ReIssuecharge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@TktNo", tktno)
    '        paramHashtable.Add("@NewTktNo", NewTktno)
    '        paramHashtable.Add("@orderId", orderId)
    '        paramHashtable.Add("@ExecutiveID", exec)
    '        paramHashtable.Add("@deptdate", Deptdate)
    '        paramHashtable.Add("@deptime", deptime)
    '        paramHashtable.Add("@PNR", PNR)
    '        paramHashtable.Add("@airpnr", AirPnr)
    '        paramHashtable.Add("@ReIssueCharge", ReIssuecharge)
    '        paramHashtable.Add("@ServiceCharge", servicecharge)
    '        paramHashtable.Add("@FareDiff", farediff)
    '        paramHashtable.Add("@status", Status.ToString())
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateReIssueRefund", 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function



    Public Function UpdateCancellesionRefundAccept(ByVal counter As Integer, ByVal PaxType As String, ByVal OrderId As String, ByVal Remark As String, ByVal Sp As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@PaxType", PaxType)
            paramHashtable.Add("@OrderId", OrderId)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@Status", Status.ToString)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, Sp, 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function UpdateReIssueReject(ByVal counter As Integer, ByVal UserID As String, ByVal orderId As String, ByVal Remark As String, ByVal SP As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@UserID", UserID)
            paramHashtable.Add("@Orderid", orderId)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@Status", Status.ToString)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    'Public Function GetFltFareDtl(ByVal trackid As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TrackId", trackid)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltFareDtl", 3)
    'End Function
    'Public Function GetCancelletionInt(ByVal tktno As String, ByVal SP As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo", tktno)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 3)
    'End Function

    Public Function UpdateStatus(ByVal tktno As String, ByVal charge As String, ByVal refund As String, ByVal SP As String) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", tktno)
            paramHashtable.Add("@UserID", charge)
            paramHashtable.Add("@Orderid", refund)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Public Function UpdateReissueIntl(ByVal tktno As String, ByVal orderId As String, ByVal NewTktno As String, ByVal PNR As String, ByVal AirPnr As String, ByVal exec As String, ByVal Deptdate As String, ByVal deptime As String, ByVal ReIssuecharge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal SP As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@TktNo", tktno)
    '        paramHashtable.Add("@orderId", tktno)
    '        paramHashtable.Add("@NewTktNo", NewTktno)
    '        paramHashtable.Add("@PNR", PNR)
    '        paramHashtable.Add("@@airpnr", AirPnr)
    '        paramHashtable.Add("@ExecutiveID", exec)
    '        paramHashtable.Add("@deptdate", Deptdate)
    '        paramHashtable.Add("@deptime", deptime)
    '        paramHashtable.Add("@ReIssueCharge", ReIssuecharge)
    '        paramHashtable.Add("@ServiceCharge", servicecharge)
    '        paramHashtable.Add("@FareDiff", farediff)
    '        paramHashtable.Add("@status", Status.ToString)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function

    Public Function GetAgencyDetailsCL(ByVal UserId As String, ByVal SP As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 3)
    End Function

    Public Function InsertTransactionRepot(ByVal pnr As String, ByVal userid As String, ByVal credit As Double, ByVal avalbal As String, ByVal debit As String, ByVal sector As String,
    ByVal paxname As String, ByVal flightno As String, ByVal agencyname As String, ByVal rm As String, ByVal SP As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@pnr", pnr)
        paramHashtable.Add("@userid", userid)
        paramHashtable.Add("@credit", credit)
        paramHashtable.Add("@AvalBal", avalbal)
        paramHashtable.Add("@Debit", debit)
        paramHashtable.Add("@sector", sector)
        paramHashtable.Add("@paxname", paxname)
        paramHashtable.Add("@flightno", flightno)
        paramHashtable.Add("@agencyname", agencyname)
        paramHashtable.Add("@Rm", rm)
        paramHashtable.Add("@status", Status.ToString)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, SP, 1)
    End Function


    Public Function GetReIssueDetail(ByVal UserID As String, ByVal UserType As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal PNR As String, ByVal PaxName As String, ByVal TicketNo As String, ByVal Air As String, ByVal AgentID As String, ByVal ExecID As String, ByVal Status As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@PaxName", PaxName)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@Airline", Air)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@ExecID", ExecID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Trip", Trip)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetReIssueDetail_Intl", 3)

    End Function

    Public Function GetReissueDetailCurrent(ByVal usertype As String, ByVal AgentID As String, ByVal curr_date As String, ByVal curr_date1 As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@curr_date", curr_date)
        paramHashtable.Add("@curr_date1", curr_date1)
        paramHashtable.Add("@Trip", Trip)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetReissueDetailCurrent_Intl", 3)
    End Function
    'Reissue Cancellation Procedure
    'Reissue Cancellation Procedure
    Public Function GetCancellationDetail(ByVal UserID As String, ByVal UserType As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal PNR As String, ByVal PaxName As String, ByVal TicketNo As String, ByVal Air As String, ByVal AgentID As String, ByVal ExecID As String, ByVal Status As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@PaxName", PaxName)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@Airline", Air)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@ExecID", ExecID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Trip", Trip)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetCancellationDetail_Intl", 3)
    End Function
    Public Function GetCancellationDetailCurrent(ByVal usertype As String, ByVal AgentID As String, ByVal curr_date As String, ByVal curr_date1 As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@curr_date", curr_date)
        paramHashtable.Add("@curr_date1", curr_date1)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetCancellationDetailCurrent_Intl", 3)
    End Function
    'Agency Detail
    'Public Function GetAgencyDetailsDDL() As DataSet
    '    paramHashtable.Clear()
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetAgencyDtl", 3)
    'End Function

    'Vipul Vikas

    Public Function Get_RefundRequestGrd(ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetTktRefundPendingGrd", 3)
    End Function

    Public Function UpdateCancelPending(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateCancellesionRefundAccept", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetCancelPendingIntl(ByVal Counter As Integer) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Counter", Counter)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetCancleleIntl", 3)
    End Function

    Public Function Get_RefundInProcessGrd(ByVal ExecId As String, ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@ExecutiveID", ExecId)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_RefundInProcessGrd", 3)
    End Function


    Public Function UpdateCancelPendingReject(ByVal counter As String, ByVal ExecutiveID As String, ByVal Remark As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateRefundReject", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Public Function Get_ReIssueRequestGrd(ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetReIssueGrdview", 3)
    End Function


    Public Function Get_ReIssueInProcessGrd(ByVal ExecId As String, ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@ExecutiveID", ExecId)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetReIssueInProcessGrdview", 3)
    End Function

    Public Function UpdateReIssueRejectPending(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal Remark As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateReIssueReject", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Public Function GetCancelletionInt(ByVal tktno As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo", tktno)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GetCancelletionInt", 3)
    'End Function


    'Public Function GetReIssueedIntl(ByVal tktno As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo", tktno)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetReIssueUpdateGrd", 3)
    'End Function

    Public Function GetAgencyDetails(ByVal UserId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetails", 3)
    End Function

    Public Function GetCancelletionInt(ByVal tktno As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TktNo", tktno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GetCancelletionInt", 3)
    End Function

    ' Update Cancellationintl table after Refund UpdateRemark
    Public Function UpdateCancelIntlTbl(ByVal counter As Integer, ByVal charge As Integer, ByVal ServiceCharge As Integer, ByVal RefundFair As Integer, ByVal UpdateRemark As String, ByVal Status As StatusClass) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@charge", charge)
            paramHashtable.Add("@ServiceCharge", ServiceCharge)
            paramHashtable.Add("@RefundFair", RefundFair)
            paramHashtable.Add("@UpdateRemark", UpdateRemark)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateCancelltion", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Public Function UpdateNew_RegsRefund(ByVal UserId As String, ByVal Amount As Double) As Double
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@UserId", UserId)
    '        paramHashtable.Add("@Amount", Amount)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AddToLimit", 2)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function


    'Public Function GetAgencyDetails(ByVal UserId As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@UserId", UserId)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetails", 3)
    'End Function


    Public Function InsertTransactionRepot(ByVal pnr As String, ByVal userid As String, ByVal credit As String, ByVal avalbal As String, ByVal debit As String, ByVal sector As String,
    ByVal paxname As String, ByVal flightno As String, ByVal agencyname As String, ByVal rm As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@pnr", pnr)
        paramHashtable.Add("@userid", userid)
        paramHashtable.Add("@credit", credit)
        paramHashtable.Add("@AvalBal", avalbal)
        paramHashtable.Add("@Debit", debit)
        paramHashtable.Add("@sector", sector)
        paramHashtable.Add("@paxname", paxname)
        paramHashtable.Add("@flightno", flightno)
        paramHashtable.Add("@agencyname", agencyname)
        paramHashtable.Add("@Rm", rm)
        paramHashtable.Add("@status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertTransReport", 1)
    End Function


    'Public Function UpdateStatus_Pax_Header(ByVal orderId As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@Orderid", orderId)
    '        paramHashtable.Add("@Status", Status.ToString())
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_Update_Pax_Header", 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function


    Public Function GetReIssueedIntl(ByVal tktno As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TktNo", tktno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetReIssueUpdateGrd", 3)
    End Function

    'Public Function UpdateNew_RegsReIssue(ByVal UserId As String, ByVal Amount As Double) As Double
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@UserId", UserId)
    '        paramHashtable.Add("@Amount", Amount)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SubtractFromLimit", 2)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function




    'Public Function insertFltHdrDetailsReissu(ByVal OrderID As String, ByVal GdsPNR As String, ByVal AirPNR As String, ByVal sector As String, ByVal Status As StatusClass, ByVal Duration As String, ByVal TripType As String, ByVal TotalBookingCost As String, ByVal TotalAfterDis As String, ByVal Adult As String, ByVal Child As String, ByVal Infant As String, ByVal AgentId As String, ByVal AgencyName As String, ByVal DistrId As String, ByVal ExecutiveId As String, ByVal PgTitle As String, ByVal PgFName As String, ByVal PgLName As String, ByVal PgMobile As String, ByVal PgEmail As String, ByVal VC As String) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@OrderID", OrderID)
    '        paramHashtable.Add("@GdsPNR", GdsPNR)
    '        paramHashtable.Add("@AirPNR", AirPNR)
    '        paramHashtable.Add("@sector", sector)
    '        paramHashtable.Add("@Status", Status.ToString())
    '        paramHashtable.Add("@Duration", Duration)
    '        paramHashtable.Add("@TripType", TripType)
    '        paramHashtable.Add("@Trip", "I")
    '        paramHashtable.Add("@TourCode", "")
    '        paramHashtable.Add("@TotalBookingCost", TotalBookingCost)
    '        paramHashtable.Add("@TotalAfterDis", TotalAfterDis)
    '        paramHashtable.Add("@AdditionalMarkup", 0)
    '        paramHashtable.Add("@Adult", Adult)
    '        paramHashtable.Add("@Child", Child)
    '        paramHashtable.Add("@Infant", Infant)
    '        paramHashtable.Add("@AgentId", AgentId)
    '        paramHashtable.Add("@AgencyName", AgencyName)
    '        paramHashtable.Add("@DistrId", DistrId)
    '        paramHashtable.Add("@ExecutiveId", ExecutiveId)
    '        paramHashtable.Add("@PaymentType", "CL")
    '        paramHashtable.Add("@Ttl", PgTitle)
    '        paramHashtable.Add("@FName", PgFName)
    '        paramHashtable.Add("@LName", PgLName)
    '        paramHashtable.Add("@Mobile", PgMobile)
    '        paramHashtable.Add("@Mail", PgEmail)
    '        paramHashtable.Add("@VC", VC)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltHeaderReIsue", 1)

    '    Catch ex As Exception
    '        Return 0
    '    End Try

    'End Function



    'Public Function GetFltDtlsReissue(ByVal trackid As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TrackId", trackid)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltDtls", 3)
    'End Function

    'Public Function insertFltDetailsReIssue(ByVal OrderID As String, ByVal DCOrAC As String, ByVal DCOrAN As String, ByVal ACOrAC As String, ByVal ACOrAN As String, ByVal DDate As String, ByVal DTime As String, ByVal ADate As String, ByVal ATime As String, ByVal AirCode As String, ByVal AirName As String, ByVal FltNo As String, ByVal AirCraft As String) As Integer
    '    Try
    '        paramHashtable.Clear()
    '        paramHashtable.Add("@OrderID", OrderID)
    '        paramHashtable.Add("@DCOrAC", DCOrAC)
    '        paramHashtable.Add("@DCOrAN", DCOrAN)
    '        paramHashtable.Add("@ACOrAC", ACOrAC)
    '        paramHashtable.Add("@ACOrAN", ACOrAN)
    '        paramHashtable.Add("@DDate", DDate)
    '        paramHashtable.Add("@DTime", DTime)
    '        paramHashtable.Add("@ADate", ADate)
    '        paramHashtable.Add("@ATime", ATime)
    '        paramHashtable.Add("@AirCode", AirCode)
    '        paramHashtable.Add("@AirName", AirName)
    '        paramHashtable.Add("@FltNo", FltNo)
    '        paramHashtable.Add("@AirCraft", AirCraft)
    '        paramHashtable.Add("@AdtFB", "")
    '        paramHashtable.Add("@ChdFB", "")
    '        paramHashtable.Add("@InfFB", "")
    '        paramHashtable.Add("@AdtRbd", "")
    '        paramHashtable.Add("@ChdRbd", "")
    '        paramHashtable.Add("@InfRbd", "")
    '        paramHashtable.Add("@AvlSeat", 0)
    '        paramHashtable.Add("@UpdateDate", DateTime.Now)
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)
    '    Catch ex As Exception

    '    End Try
    'End Function


    Public Function GetFltFareDtl(ByVal trackid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltFareDtl", 3)
    End Function

    'Public Function insertFareDetailsReIssue(ByVal trackid As String, ByVal basefare As String, ByVal YQ As String, ByVal YR As String, ByVal WO As String, ByVal OT As String, ByVal totTax As String, ByVal totFare As String, ByVal SrvTax As String, ByVal TF As String, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String, ByVal netFare As String, ByVal paxtype As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@OrderID", trackid)
    '    paramHashtable.Add("@BaseFare", basefare)
    '    paramHashtable.Add("@YQ", YQ)
    '    paramHashtable.Add("@YR", YR)
    '    paramHashtable.Add("@WO", WO)
    '    paramHashtable.Add("@OT", OT)
    '    paramHashtable.Add("@TotalTax", totTax)
    '    paramHashtable.Add("@TotalFare", totFare)
    '    paramHashtable.Add("@ServiceTax", SrvTax)
    '    paramHashtable.Add("@TranFee", TF)
    '    paramHashtable.Add("@AdminMrk", admrk)
    '    paramHashtable.Add("@AgentMrk", agmrk)
    '    paramHashtable.Add("@DistrMrk", dismrk)
    '    paramHashtable.Add("@TotalDiscount", comm)
    '    paramHashtable.Add("@PLb", 0)
    '    paramHashtable.Add("@Discount", 0)
    '    paramHashtable.Add("@CashBack", 0)
    '    paramHashtable.Add("@Tds", tds)
    '    paramHashtable.Add("@TdsOn", comm)
    '    paramHashtable.Add("@TotalAfterDis", netFare)
    '    paramHashtable.Add("@PaxType", paxtype)
    '    paramHashtable.Add("@UpdateDate", DateTime.Now)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    'End Function

    'Public Function insertPaxDetailsReIssue(ByVal OrderId As String, ByVal TktNo As String, ByVal Title As String, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal PaxType As String, ByVal DOB As String, ByVal FFNumber As String, ByVal FFAirline As String, ByVal MealType As String, ByVal SeatType As String, ByVal IsPrimary As Boolean, ByVal AssociatedPaxName As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@OrderID", OrderId)
    '    paramHashtable.Add("@TktNo", TktNo)
    '    paramHashtable.Add("@Title", Title)
    '    paramHashtable.Add("@FName", FName)
    '    paramHashtable.Add("@MName", MName)
    '    paramHashtable.Add("@LName", LName)
    '    paramHashtable.Add("@PaxType", PaxType)
    '    paramHashtable.Add("@DOB", DOB)
    '    paramHashtable.Add("@FFNumber", FFNumber)
    '    paramHashtable.Add("@FFAirline", FFAirline)
    '    paramHashtable.Add("@MealType", MealType)
    '    paramHashtable.Add("@SeatType", SeatType)
    '    paramHashtable.Add("@IsPrimary", IsPrimary)
    '    paramHashtable.Add("@InfAssociatePaxName", AssociatedPaxName)
    '    paramHashtable.Add("@Status", Status.ToString())
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltPaxDetailsReIssue", 1)
    'End Function

    'Public Function USP_GetTicketDetail_Intl(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal pnr As String, ByVal paxname As String, ByVal TicketNo As String, ByVal AirPNR As String, ByVal AgentID As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@usertype", usertype)
    '    paramHashtable.Add("@LoginID", LoginId)
    '    paramHashtable.Add("@FormDate", FromDate)
    '    paramHashtable.Add("@ToDate", ToDate)
    '    paramHashtable.Add("@OderId", OrderID)
    '    paramHashtable.Add("@PNR", pnr)
    '    paramHashtable.Add("@AirlinePNR", AirPNR)
    '    paramHashtable.Add("@PaxName", paxname)
    '    paramHashtable.Add("@TicketNo", TicketNo)
    '    paramHashtable.Add("@AgentId", AgentID)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetTicketDetail_Intl", 3)
    'End Function

    Public Function GetAgencyDetailsDDL() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetAllAgencyDtl", 3)
    End Function

    Public Function UpdateReIssuePending(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@Status", Status.ToString)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateReIsuueAccept", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function insertReIssueIntl(ByVal pnr As String, ByVal TktNo As String, ByVal sector As String, ByVal deparcher As String, ByVal distance As String, ByVal Title As String, ByVal FName As String, ByVal LName As String, ByVal PaxType As String, ByVal BookinfDate As String, ByVal DepartDate As String, ByVal TotalBookingCost As String, ByVal TotalAfterDis As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BaseFare As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TFee As String, ByVal Dis As String, ByVal CB As String, ByVal TDS As String, ByVal TotalFare As String, ByVal TotalFareAfterDis As String, ByVal VC As String, ByVal OrderID As String, ByVal DepTime As String, ByVal AirlinePNR As String, ByVal FNo As String, ByVal Remark As String, ByVal RefundID As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@PNR", pnr)
            paramHashtable.Add("@Tkt_No", TktNo)
            paramHashtable.Add("@Sector", sector)
            paramHashtable.Add("@departure", deparcher)
            paramHashtable.Add("@destination", distance)
            paramHashtable.Add("@Title", Title)
            paramHashtable.Add("@pax_fname", FName)
            paramHashtable.Add("@pax_lname", LName)
            paramHashtable.Add("@PaxType", PaxType)
            paramHashtable.Add("@Booking_date", BookinfDate)
            paramHashtable.Add("@departure_date", DepartDate)
            paramHashtable.Add("@UserID", UserID)
            paramHashtable.Add("@Agency_Name", AgencyName)
            paramHashtable.Add("@Base_Fare", BaseFare)
            paramHashtable.Add("@Tax", Tax)
            paramHashtable.Add("@YQ", YQ)
            paramHashtable.Add("@Service_Tax", STax)
            paramHashtable.Add("@Tran_Fees", TFee)
            paramHashtable.Add("@Discount", Dis)
            paramHashtable.Add("@CB", CB)
            paramHashtable.Add("@TDS", TDS)
            paramHashtable.Add("@TotalFare", TotalFare)
            paramHashtable.Add("@TotalFareAfterDiscount", TotalAfterDis)
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@DepTime", DepTime)
            paramHashtable.Add("@AirlinePNR", AirlinePNR)
            paramHashtable.Add("@FNo", FNo)
            paramHashtable.Add("@RegardingIssue", Remark)
            paramHashtable.Add("@IDs", RefundID)
            paramHashtable.Add("@Status", Status.ToString())
            paramHashtable.Add("@VC", VC)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_InsReIssueIntl", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function



    Public Function insertCancelationIntl(ByVal pnr As String, ByVal TktNo As String, ByVal sector As String, ByVal deparcher As String, ByVal distance As String, ByVal Title As String, ByVal FName As String, ByVal LName As String, ByVal PaxType As String, ByVal BookinfDate As String, ByVal DepartDate As String, ByVal TotalBookingCost As String, ByVal TotalAfterDis As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BaseFare As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TFee As String, ByVal Dis As String, ByVal CB As String, ByVal TDS As String, ByVal TotalFare As String, ByVal TotalFareAfterDis As String, ByVal VC As String, ByVal OrderID As String, ByVal DepTime As String, ByVal AirlinePNR As String, ByVal FNo As String, ByVal Remark As String, ByVal RefundID As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@PNR", pnr)
            paramHashtable.Add("@Tkt_No", TktNo)
            paramHashtable.Add("@Sector", sector)
            paramHashtable.Add("@departure", deparcher)
            paramHashtable.Add("@destination", distance)
            paramHashtable.Add("@Title", Title)
            paramHashtable.Add("@pax_fname", FName)
            paramHashtable.Add("@pax_lname", LName)
            paramHashtable.Add("@PaxType", PaxType)
            paramHashtable.Add("@Booking_date", BookinfDate)
            paramHashtable.Add("@departure_date", DepartDate)
            paramHashtable.Add("@UserID", UserID)
            paramHashtable.Add("@Agency_Name", AgencyName)
            paramHashtable.Add("@Base_Fare", BaseFare)
            paramHashtable.Add("@Tax", Tax)
            paramHashtable.Add("@YQ", YQ)
            paramHashtable.Add("@Service_Tax", STax)
            paramHashtable.Add("@Tran_Fees", TFee)
            paramHashtable.Add("@Discount", Dis)
            paramHashtable.Add("@CB", CB)
            paramHashtable.Add("@TDS", TDS)
            paramHashtable.Add("@TotalFare", TotalFare)
            paramHashtable.Add("@TotalFareAfterDiscount", TotalAfterDis)
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@DepTime", DepTime)
            paramHashtable.Add("@AirlinePNR", AirlinePNR)
            paramHashtable.Add("@FNo", FNo)
            paramHashtable.Add("@RegardingCancel", Remark)
            paramHashtable.Add("@IDs", RefundID)
            paramHashtable.Add("@Status", Status.ToString())
            paramHashtable.Add("@VC", VC)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_InsCancelationIntl", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function


    'Public Function Get_TktRptIntlGrdview(ByVal PaxId As String, ByVal PaxType As String, ByVal curr_date As String, ByVal curr_date1 As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@usertype", PaxType)
    '    paramHashtable.Add("@AgentID", PaxId)
    '    paramHashtable.Add("@curr_date", curr_date)
    '    paramHashtable.Add("@curr_date1", curr_date1)

    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetTktRptGrd", 3)
    'End Function

    'Public Function UpdateReissueIntl(ByVal tktno As String, ByVal orderId As String, ByVal NewTktno As String, ByVal PNR As String, ByVal AirPnr As String, ByVal exec As String, ByVal Deptdate As String, ByVal deptime As String, ByVal ReIssuecharge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    Try
    '        paramHashtable.Add("@TktNo", tktno)
    '        paramHashtable.Add("@NewTktNo", NewTktno)
    '        paramHashtable.Add("@orderId", orderId)
    '        paramHashtable.Add("@ExecutiveID", exec)
    '        paramHashtable.Add("@deptdate", Deptdate)
    '        paramHashtable.Add("@deptime", deptime)
    '        paramHashtable.Add("@PNR", PNR)
    '        paramHashtable.Add("@airpnr", AirPnr)
    '        paramHashtable.Add("@ReIssueCharge", ReIssuecharge)
    '        paramHashtable.Add("@ServiceCharge", servicecharge)
    '        paramHashtable.Add("@FareDiff", farediff)
    '        paramHashtable.Add("@status", Status.ToString())
    '        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateReIssueRefund", 1)
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function

    Public Function Get_TktRptIntlGrdview(ByVal PaxId As String, ByVal PaxType As String, ByVal curr_date As String, ByVal curr_date1 As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", PaxType)
        paramHashtable.Add("@AgentID", PaxId)
        paramHashtable.Add("@curr_date", curr_date)
        paramHashtable.Add("@curr_date1", curr_date1)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_GetTktRptGrd", 3)
    End Function

    'Public Function CheckReIssueIntlPNR(ByVal TktNo As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo", TktNo)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckReIssueIntlPNR", 2)
    'End Function
    ' ''
    'Public Function CheckCancelIntlPNR(ByVal TktNo As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo  ", TktNo)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckCancelIntlPNR", 2)
    'End Function

    'Public Function GetTicketdIntl(ByVal PaxId As Integer, ByVal PaxType As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@PaxId", PaxId)
    '    paramHashtable.Add("@PaxType", PaxType)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CancelRequestIntl", 3)
    'End Function


    '********Intl. Invoice and Sale Register start*************

    Public Function IntGetInvoice(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal pnr As String, ByVal TicketNo As String, ByVal AgentID As String, ByVal Trip As String, ByVal Airline As String, ByVal ProjectId As String) As DataSet

        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginId)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", pnr)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@ProjectID", ProjectId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "IntSelectInvoiceDetails", 3)
    End Function
    'Public Function IntGetInvoieDate(ByVal fromdate As String, ByVal todate As String, ByVal userid As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@fromdate", fromdate)
    '    paramHashtable.Add("@todate", todate)
    '    paramHashtable.Add("@userid", userid)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "IntSelectInvoiceDetailsDate", 3)
    'End Function
    Public Function IntGetInvoiceByAgency(ByVal agency As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@agencyname", agency)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "IntGetInvoiceDetailsByagency", 3)

    End Function
    Public Function GetInvoice(ByVal orderid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@orderid", orderid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetInvoiceDetails", 3)

    End Function

    '********Intl. Invoice and Sale Register end*************
    'Add To Limit for rail refund
    Public Function AddCrdLimit(ByVal uid As String, ByVal amount As Double) As Double
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", uid)
        paramHashtable.Add("@Amount", amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AddToLimit", 2)
    End Function
    'Rail Refund Transaction Insert
    Public Function InsertRailRefund(ByVal Agency_ID As String, ByVal Agency_Name As String, ByVal pnr As String, ByVal Train_No As String, ByVal Aval_Bal As String, ByVal Credit As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Agency_ID", Agency_ID)
        paramHashtable.Add("@Agency_Name", Agency_Name)
        paramHashtable.Add("@PNR", pnr)
        paramHashtable.Add("@PNR_Status", "Cancelled")
        paramHashtable.Add("@Booking_Date", System.DateTime.Now)
        paramHashtable.Add("@Booking_Type", "Rail")
        paramHashtable.Add("@Train_No", Train_No)
        paramHashtable.Add("@Aval_Bal", Aval_Bal)
        paramHashtable.Add("@Credit", Credit)
        paramHashtable.Add("@Rm", "Train Cancelling")
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Rail_Trans_Report_Refund", 3)
    End Function
    'Rail Refund Report
    Public Function GetRailRefundReport(ByVal UserType As String, ByVal UserID As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Get_RailRefundReport", 3)
    End Function
    'Public Function CheckTktNo(ByVal TktNo As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@TktNo  ", TktNo)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckTktNo", 2)
    'End Function
    'Reject HoldPNR Intl
    Public Function RejectHoldPNRStatusIntl(ByVal OderId As String, ByVal UID As String, ByVal Status As String, ByVal Remark As String, ByVal PNR As String, ByVal PNRStatus As String, ByVal Credit As Decimal, ByVal Aval_Bal As Decimal, ByVal Rm As String, ByVal AgencyName As String, ByVal AgentID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OderId", OderId)
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@PNRStatus", PNRStatus)
        paramHashtable.Add("@Credit", Credit)
        paramHashtable.Add("@Aval_Bal", Aval_Bal)
        paramHashtable.Add("@Rm", Rm)
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@AgentID", AgentID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "RejectHoldPNR", 3)
    End Function
    'Flt Header Detail
    Public Function GetFltHeaderDetail(ByVal orderid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OderId", orderid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SelectFltHeaderDetail", 3)

    End Function
    'Agent Transaction Search Filter
    Public Function AgentTransSearch(ByVal AgentID As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet
        paramHashtable.Clear()

        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@From", FromDate)
        paramHashtable.Add("@To", ToDate)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgentTransactionDetails", 3)
    End Function

    'Pnr Import Intl Detail
    Public Function PnrImportIntlDetails(ByVal OrderId As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()

        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetPnrImportIntlDetails", 3)
    End Function



    'Insert Intl Import PNR Details
    Public Function InsertImportPNRIntl(ByVal OrderId As String, ByVal PNR As String, ByVal Airline As String, ByVal Dept As String, ByVal dest As String, ByVal DDate As String, ByVal DTime As String, ByVal Adate As String, ByVal ATime As String, ByVal FNo As String, ByVal RBD As String, ByVal Status As String, ByVal BlockPNR As Boolean, ByVal userid As String, ByVal agency As String, ByVal Trip As String, ByVal TripType As String, ByVal Tri As String, ByVal projectID As String, ByVal BookedBy As String, ByVal Provider As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Dept", Dept)
        paramHashtable.Add("@dest", dest)
        paramHashtable.Add("@DDate", DDate)
        paramHashtable.Add("@DTime", DTime)
        paramHashtable.Add("@Adate", Adate)
        paramHashtable.Add("@ATime", ATime)
        paramHashtable.Add("@FNo", FNo)
        paramHashtable.Add("@RBD", RBD)
        paramHashtable.Add("@userid", userid)
        paramHashtable.Add("@agency", agency)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@BlockPNR", False)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@TripType", TripType)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@ProjectID", projectID)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@Provider", Provider)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertImportPNRIntl", 1)
    End Function

    'Insert Intl Import PNR Pax Details
    Public Function InsertPaxInfoIntl(ByVal OrderId As String, ByVal tittle As String, ByVal fname As String, ByVal mname As String, ByVal lname As String, ByVal Tri As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@tittle", tittle)
        paramHashtable.Add("@first_name", fname)
        paramHashtable.Add("@middle_name", mname)
        paramHashtable.Add("@last_name", lname)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@paxtype", "")
        paramHashtable.Add("@ticketno", "")

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPaxIntl", 1)
    End Function


    'intl Import PNR Detais
    Public Function ImportPNRDetailsIntl(ByVal st As String, ByVal Tri As String, Optional ByVal OrderId As String = "", Optional ByVal id As String = "") As DataSet
        paramHashtable.Clear()

        paramHashtable.Add("@st", st)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@id", id)
        paramHashtable.Add("@Tri", Tri)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ImportPNRDetailIntl", 3)
    End Function



    'update intl import pnr details
    Public Function UpdateImpPnrIntlDetails(ByVal orderid As String, ByVal st As String, ByVal id As String, ByVal Remark As String, ByVal Tri As String, Optional ByVal ESCharge As String = "") As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@orderid", orderid)
        paramHashtable.Add("@st", st)
        paramHashtable.Add("@id", id)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@ESCharge", ESCharge)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateImpPnrIntl", 1)
    End Function

    'update Intl Pnr Import Pnr Ticket
    Public Function UpdateTktnoIntl(ByVal paxid As String, ByVal paxtype As String, ByVal tktno As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@PaxID", paxid)
        paramHashtable.Add("@paxtype", paxtype)
        paramHashtable.Add("@tktno", tktno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdatePaxInfofromImportPnrIntl", 1)
    End Function


    'Insert Header Details in Pnr Import
    Public Function insertHeaderDetailsPnrImport(ByVal OrderId As String, ByVal Sector As String, ByVal Status As String, ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal VC As String, ByVal TripType As String, ByVal Trip As String, ByVal TotalBookingCost As Decimal, ByVal TotalAfterDis As Decimal, ByVal AdditionalMarkup As Decimal, ByVal Adult As Integer, ByVal Child As Integer, ByVal Infant As Integer, ByVal AgentId As String, ByVal AgencyName As String, ByVal DistrId As String, ByVal ExecutiveId As String, ByVal PaymentType As String, ByVal PgTitle As String, ByVal PgFName As String, ByVal PgLName As String, ByVal ImportCharge As Decimal, ByVal SFDis As Decimal, ByVal projectId As String, ByVal BookedBy As String, ByVal BillNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@Sector", Sector)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@GdsPnr", GdsPnr)
        paramHashtable.Add("@AirlinePnr", AirlinePnr)
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@TripType", TripType)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@TotalBookingCost", TotalBookingCost)
        paramHashtable.Add("@TotalAfterDis", TotalAfterDis)
        paramHashtable.Add("@AdditionalMarkup", AdditionalMarkup)
        paramHashtable.Add("@Adult", Adult)
        paramHashtable.Add("@Child", Child)
        paramHashtable.Add("@Infant", Infant)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@DistrId", DistrId)
        paramHashtable.Add("@ExecutiveId", ExecutiveId)
        paramHashtable.Add("@PaymentType", PaymentType)
        paramHashtable.Add("@PgTitle", PgTitle)
        paramHashtable.Add("@PgFName", PgFName)
        paramHashtable.Add("@PgLName", PgLName)
        paramHashtable.Add("@ImportCharge", ImportCharge)
        paramHashtable.Add("@SFDis", SFDis)
        paramHashtable.Add("@ProjectID", projectId)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@BillNo", BillNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPnrImportDetails", 1)
    End Function


    'Insert Trans Details in import PNr

    Public Function InsertTransReportPnrImport(ByVal UID As String, ByVal GDSPNR As String, ByVal PNRStatus As String,
     ByVal AvalBal As String, ByVal Debit As String, ByVal Sec As String,
      ByVal Rm As String,
      ByVal FareAFTDIS As String,
     ByVal Ag_Name As String) As Integer

        paramHashtable.Clear()
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@GDSPNR", GDSPNR)
        paramHashtable.Add("@PNRStatus", PNRStatus)
        paramHashtable.Add("@AvalBal", AvalBal)
        paramHashtable.Add("@Debit", Debit)
        paramHashtable.Add("@Sec", Sec)
        paramHashtable.Add("@Rm", Rm)
        paramHashtable.Add("@FareAFTDIS", FareAFTDIS)
        paramHashtable.Add("@Ag_Name", Ag_Name)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPnrImportTransReport", 1)

    End Function


    'Insert Es Charge Trans Details in import PNr
    Public Function InsertEsTransCharge(ByVal UID As String, ByVal GDSPNR As String, ByVal AvalBal As String, ByVal Debit As String, ByVal Rm As String, ByVal Ag_Name As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@GDSPNR", GDSPNR)
        paramHashtable.Add("@AvalBal", AvalBal)
        paramHashtable.Add("@Debit", Debit)
        paramHashtable.Add("@Rm", Rm)
        paramHashtable.Add("@Ag_Name", Ag_Name)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPnrImportTransReportEsCharge", 1)

    End Function

    'Get City Name
    Public Function GetCityNameByCode(ByVal Code As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Code", Code)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetCityName", 3)
    End Function

    'Get Airline Name
    Public Function GetAirlineNameByCode(ByVal AL_Code As String) As DataSet
        paramHashtable.Clear()

        paramHashtable.Add("@AL_Code", AL_Code)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetAirlineName", 3)
    End Function


    'Insert Flight Details in import PNr
    Public Function insertFlightDetailsPnrImport(ByVal OrderID As String, ByVal DCOrAC As String, ByVal DCOrAN As String, ByVal ACOrAC As String,
                ByVal ACOrAN As String, ByVal DDate As String, ByVal DTime As String, ByVal ADate As String, ByVal ATime As String, ByVal AirCode As String,
        ByVal AirName As String, ByVal FltNo As String, ByVal AirCraft As String, ByVal AdtFB As String, ByVal ChdFB As String, ByVal InfFB As String,
        ByVal AdtRbd As String, ByVal ChdRbd As String, ByVal InfRbd As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", OrderID)
        paramHashtable.Add("@DCOrAC", DCOrAC)
        paramHashtable.Add("@DCOrAN", DCOrAN)
        paramHashtable.Add("@ACOrAC", ACOrAC)
        paramHashtable.Add("@ACOrAN", ACOrAN)
        paramHashtable.Add("@DDate", DDate)
        paramHashtable.Add("@DTime", DTime)
        paramHashtable.Add("@ADate", ADate)
        paramHashtable.Add("@ATime", ATime)
        paramHashtable.Add("@AirCode", AirCode)
        paramHashtable.Add("@AirName", AirName)
        paramHashtable.Add("@FltNo", FltNo)
        paramHashtable.Add("@AirCraft", AirCraft)
        paramHashtable.Add("@AdtFB", AdtFB)
        paramHashtable.Add("@ChdFB", ChdFB)
        paramHashtable.Add("@InfFB", InfFB)
        paramHashtable.Add("@AdtRbd", AdtRbd)
        paramHashtable.Add("@ChdRbd", ChdRbd)
        paramHashtable.Add("@InfRbd", InfRbd)
        paramHashtable.Add("@AvlSeat", 0)
        paramHashtable.Add("@UpdateDate", DateTime.Now)
        objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)



    End Function



    Public Function UpdateInltPnrImportTicketed(ByVal OrderId As String, ByVal Status As String, ByVal ESCharge As Integer) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@ESCharge", ESCharge)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdatePnrImportTicketed", 1)

    End Function



    Public Function ImportPnrReport_Intl(ByVal UserType As String, ByVal UserID As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal PNR As String, ByVal Airline As String, ByVal AgentId As String, ByVal Tri As String, ByVal ExecID As String, ByVal Status As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OrderId", OrderID)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@ExecID", ExecID)
        paramHashtable.Add("@Status", Status)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "PnrImportIntlReport", 3)
    End Function

    'Vipul Refund and reissue
    'Filter data on ReIssue and Refund Click from Ticket Report Gridview 
    Public Function GetTicketdIntl(ByVal PaxId As Integer, ByVal PaxType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@PaxType", PaxType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CancelRequestIntl", 3)
    End Function

    'Filtering data from Search click
    Public Function USP_GetTicketDetail_Intl(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String,
        ByVal pnr As String, ByVal paxname As String, ByVal TicketNo As String, ByVal AirPNR As String, ByVal AgentID As String, ByVal Trip As String, ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginId)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", pnr)
        paramHashtable.Add("@AirlinePNR", AirPNR)
        paramHashtable.Add("@PaxName", paxname)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetTicketDetail_Intl", 3)
    End Function

    Public Function USP_GetTicketDetail_IntlStatus(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String,
       ByVal pnr As String, ByVal paxname As String, ByVal TicketNo As String, ByVal AirPNR As String, ByVal AgentID As String, ByVal Trip As String, ByVal Status As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginId)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", pnr)
        paramHashtable.Add("@AirlinePNR", AirPNR)
        paramHashtable.Add("@PaxName", paxname)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetTicketDetail_IntlStatus", 3)
    End Function

    Public Function GetCancellationDetailmass(ByVal UserID As String, ByVal UserType As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, ByVal PNR As String, ByVal PaxName As String, ByVal TicketNo As String, ByVal Air As String, ByVal AgentID As String, ByVal ExecID As String, ByVal Status As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@PaxName", PaxName)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@Airline", Air)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@ExecID", ExecID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Trip", Trip)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetCancellationDetail_IntlMass", 3)
    End Function



    'ChecK PNR Status from ReIssueIntl and Cancellation table
    Public Function CheckTktNo(ByVal PaxId As Integer, ByVal OrderId As String, ByVal PNR As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PNR", PNR)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckTktNo", 2)
    End Function
    'Insert data in ReIssueIntl and CancellationIntl Table
    Public Function InsReIssueCancelIntl(ByVal pnr As String, ByVal TktNo As String, ByVal sector As String, ByVal deparcher As String, ByVal distance As String, ByVal Title As String, ByVal FName As String, ByVal LName As String,
                                       ByVal PaxType As String, ByVal BookinfDate As String, ByVal DepartDate As String, ByVal TotalBookingCost As String, ByVal TotalAfterDis As String, ByVal UserID As String,
                                       ByVal AgencyName As String, ByVal BaseFare As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TFee As String, ByVal Dis As String, ByVal CB As String,
                                       ByVal TDS As String, ByVal TotalFare As String, ByVal TotalFareAfterDis As String, ByVal VC As String, ByVal OrderID As String, ByVal DepTime As String, ByVal AirlinePNR As String,
                                       ByVal FNo As String, ByVal Remark As String, ByVal RefundID As String, ByVal PaxID As Integer, ByVal Trip As String, ByVal ReqTyp As String, ByVal Status As StatusClass, ByVal projectId As String, ByVal mgtFee As Decimal, ByVal CancelledBy As String, ByVal BillNoCorp As String, ByVal BillNoAir As String) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@PaxID", PaxID)
            paramHashtable.Add("@PNR", pnr)
            paramHashtable.Add("@Tkt_No", TktNo)
            paramHashtable.Add("@Sector", sector)
            paramHashtable.Add("@departure", deparcher)
            paramHashtable.Add("@destination", distance)
            paramHashtable.Add("@Title", Title)
            paramHashtable.Add("@pax_fname", FName)
            paramHashtable.Add("@pax_lname", LName)
            paramHashtable.Add("@PaxType", PaxType)
            paramHashtable.Add("@Booking_date", BookinfDate)
            paramHashtable.Add("@departure_date", DepartDate)
            paramHashtable.Add("@UserID", UserID)
            paramHashtable.Add("@Agency_Name", AgencyName)
            paramHashtable.Add("@Base_Fare", BaseFare)
            paramHashtable.Add("@Tax", Tax)
            paramHashtable.Add("@YQ", YQ)
            paramHashtable.Add("@Service_Tax", STax)
            paramHashtable.Add("@Tran_Fees", TFee)
            paramHashtable.Add("@Discount", Dis)
            paramHashtable.Add("@CB", CB)
            paramHashtable.Add("@TDS", TDS)
            paramHashtable.Add("@TotalFare", TotalFare)
            paramHashtable.Add("@TotalFareAfterDiscount", TotalFareAfterDis)
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@DepTime", DepTime)
            paramHashtable.Add("@AirlinePNR", AirlinePNR)
            paramHashtable.Add("@FNo", FNo)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@IDs", RefundID)
            paramHashtable.Add("@Status", Status.ToString())
            paramHashtable.Add("@VC", VC)
            paramHashtable.Add("@ReqTyp", ReqTyp)
            paramHashtable.Add("@Trip", Trip)
            paramHashtable.Add("@ProjectID", projectId)
            paramHashtable.Add("@MgtFee", mgtFee)
            paramHashtable.Add("@CancelledBy", CancelledBy)
            paramHashtable.Add("@BillNoCorp", BillNoCorp)
            paramHashtable.Add("@BillNoAir", BillNoAir)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsReIssueCancelIntl", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function



    'GET Refund and ReIssue Gridview Data whose Status is Pending
    Public Function RefundReIssueRequestGrd(ByVal ReqType As String, ByVal Status As StatusClass, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status.ToString())
        paramHashtable.Add("@ReqType", ReqType)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "RefundReIssueRequestGrd", 3)
    End Function

    'Update Status in CancellationIntl and ReIssueIntl Table after Accepted
    Public Function UpdateReIssueCancleAccept(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal ReqType As String, ByVal Status As StatusClass) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@ReqType", ReqType)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateReIssueCancleAccept", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    'Update Status and Remark in CancellationIntl and ReIssueIntl Table after Reject
    Public Function UpdateReIssueCancelReject(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal Remark As String, ByVal ReqType As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@Remark", Remark)
            paramHashtable.Add("@ReqType", ReqType)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateReIssueCancelReject", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    'GET Refund and ReIssue Gridview Data whose Status is InProcess
    Public Function ReIssueRefundInProcessGrd(ByVal ExecId As String, ByVal ReqType As String, ByVal Status As StatusClass, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@ExecutiveID", ExecId)
        paramHashtable.Add("@ReqType", ReqType)
        paramHashtable.Add("@Status", Status.ToString())
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ReIssueRefundInProcessGrd", 3)
    End Function

    'GET RefundIntl and ReIssueIntl Gridview Data for Update and Fare
    Public Function GetReIssueCancelIntl(ByVal counter As Integer, ByVal ReqType As String, ByRef status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Counter", counter)
        paramHashtable.Add("@ReqType", ReqType)
        paramHashtable.Add("@status", status.ToString)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetReIssueCancelIntl", 3)
    End Function
    ''Insert data in Transaction Report
    'Public Function InsertTransactionRepot(ByVal pnr As String, ByVal userid As String, ByVal credit As String, ByVal avalbal As String, ByVal debit As String, ByVal sector As String, _
    '    ByVal paxname As String, ByVal flightno As String, ByVal agencyname As String, ByVal rm As String, ByVal Status As StatusClass) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@pnr", pnr)
    '    paramHashtable.Add("@userid", userid)
    '    paramHashtable.Add("@credit", credit)
    '    paramHashtable.Add("@AvalBal", avalbal)
    '    paramHashtable.Add("@Debit", debit)
    '    paramHashtable.Add("@sector", sector)
    '    paramHashtable.Add("@paxname", paxname)
    '    paramHashtable.Add("@flightno", flightno)
    '    paramHashtable.Add("@agencyname", agencyname)
    '    paramHashtable.Add("@Rm", rm)
    '    paramHashtable.Add("@status", Status.ToString())
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertTransReport", 1)
    'End Function


    'Update Balance after Refund
    Public Function UpdateNew_RegsRefund(ByVal UserId As String, ByVal Amount As Double) As Double
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@UserId", UserId)
            paramHashtable.Add("@Amount", Amount)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AddToLimit", 2)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' Insert Data in LedgerDetails Table
    Public Function InsLedgerDetails(ByVal AgentID As String, ByVal AgencyName As String, ByVal InvoiceNo As String, ByVal PnrNo As String, ByVal TicketNo As String, ByVal TicketingCarrier As String, ByVal YatraAccountID As String, ByVal AccountID As String, ByVal ExecutiveID As String, ByVal IPAddress As String, ByVal Debit As Decimal, ByVal Credit As Decimal, ByVal Aval_Balance As Decimal, ByVal BookingType As String, ByVal Remark As String) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@AgentID", AgentID)
            paramHashtable.Add("@AgencyName", AgencyName)
            paramHashtable.Add("@InvoiceNo", InvoiceNo)
            paramHashtable.Add("@PnrNo", PnrNo)
            paramHashtable.Add("@TicketNo", TicketNo)
            paramHashtable.Add("@TicketingCarrier", TicketingCarrier)
            paramHashtable.Add("@YatraAccountID", YatraAccountID)
            paramHashtable.Add("@AccountID", AccountID)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@IPAddress", IPAddress)
            paramHashtable.Add("@Debit", Debit)
            paramHashtable.Add("@Credit", Credit)
            paramHashtable.Add("@Aval_Balance", Aval_Balance)
            paramHashtable.Add("@BookingType", BookingType)
            paramHashtable.Add("@Remark", Remark)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsLedgerDetails", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' Update Cancellationintl table after Refund 
    Public Function UpdateCancelIntlTbl(ByVal counter As Integer, ByVal charge As Integer, ByVal ServiceCharge As Integer, ByVal RefundFair As Integer, ByVal Status As StatusClass) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@charge", charge)
            paramHashtable.Add("@ServiceCharge", ServiceCharge)
            paramHashtable.Add("@RefundFair", RefundFair)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateCancelltion", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Update Status=Cancelled or Ticketed in FltHeader and FltPaxDetails 
    Public Function UpdateStatus_Pax_Header(ByVal orderId As String, ByVal PNR As String, ByVal PaxID As Integer, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@Orderid", orderId)
            paramHashtable.Add("@PNR", PNR)
            paramHashtable.Add("@PaxID", PaxID)
            paramHashtable.Add("@Status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_Update_Pax_Header", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    'Check Agent Balance 
    Public Function CheckBalance(ByVal UserId As String, ByVal Amt As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        paramHashtable.Add("@Amt", Amt)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckBalance", 2)
    End Function

    'Update Balance after ReIssue
    Public Function UpdateNew_RegsReIssue(ByVal UserId As String, ByVal Amount As Double) As Double
        paramHashtable.Clear()
        Try
            paramHashtable.Add("@UserId", UserId)
            paramHashtable.Add("@Amount", Amount)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SubtractFromLimit", 2)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' Update ReIssueINTL table after REISSUE 
    Public Function UpdateReissueIntl(ByVal counter As Integer, ByVal ExecutiveID As String, ByVal ReIssuecharge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal Status As StatusClass) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@counter", counter)
            paramHashtable.Add("@ExecutiveID", ExecutiveID)
            paramHashtable.Add("@ReIssueCharge", ReIssuecharge)
            paramHashtable.Add("@ServiceCharge", servicecharge)
            paramHashtable.Add("@FareDiff", farediff)
            paramHashtable.Add("@status", Status.ToString())
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "sp_UpdateReIssueIntl", 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Get PaxDetails Data for Reissue
    Public Function GetPaxDetailsReIssue(ByVal PaxID As String, ByVal TktNo As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@PaxID", PaxID)
        paramHashtable.Add("@TktNo", TktNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetPaxDetailsReIssue", 3)
    End Function


    'Insert Data in PaxDetails After ReIssue
    Public Function insertPaxDetailsReIssue(ByVal OrderId As String, ByVal TktNo As String, ByVal Title As String, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal PaxType As String, ByVal DOB As String, ByVal FFNumber As String, ByVal FFAirline As String, ByVal MealType As String, ByVal SeatType As String, ByVal IsPrimary As Boolean, ByVal AssociatedPaxName As String, ByVal Status As StatusClass) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", OrderId)
        paramHashtable.Add("@TktNo", TktNo)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@FName", FName)
        paramHashtable.Add("@MName", MName)
        paramHashtable.Add("@LName", LName)
        paramHashtable.Add("@PaxType", PaxType)
        paramHashtable.Add("@DOB", DOB)
        paramHashtable.Add("@FFNumber", FFNumber)
        paramHashtable.Add("@FFAirline", FFAirline)
        paramHashtable.Add("@MealType", MealType)
        paramHashtable.Add("@SeatType", SeatType)
        paramHashtable.Add("@IsPrimary", IsPrimary)
        paramHashtable.Add("@InfAssociatePaxName", AssociatedPaxName)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltPaxDetailsReIssue", 1)
    End Function
    ' Get fltHeader data
    Public Function GetHdrDetails(ByVal trackid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltHdr", 3)
    End Function
    'Insert Data in fltHeader Table after ReIssue
    Public Function insertFltHdrDetailsReissu(ByVal OrderID As String, ByVal GdsPNR As String, ByVal AirPNR As String, ByVal sector As String, ByVal Status As StatusClass,
                    ByVal Duration As String, ByVal TripType As String, ByVal TotalBookingCost As Double, ByVal TotalAfterDis As Double, ByVal Adult As Integer,
                    ByVal Child As Integer, ByVal Infant As Integer, ByVal AgentId As String, ByVal AgencyName As String, ByVal DistrId As String, ByVal ExecutiveId As String,
                    ByVal PgTitle As String, ByVal PgFName As String, ByVal PgLName As String, ByVal PgMobile As String, ByVal PgEmail As String, ByVal VC As String,
                    ByVal AdditionalMrk As Double, ByVal Trip As String, ByVal ResuID As String, ByVal ResuCharge As Double, ByVal ResuServiceCharge As Double, ByVal ResuFareDiff As Double, ByVal ProjectId As String, ByVal BillNoCorp As String, ByVal BookedBy As String) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@GdsPNR", GdsPNR)
            paramHashtable.Add("@AirPNR", AirPNR)
            paramHashtable.Add("@sector", sector)
            paramHashtable.Add("@Status", Status.ToString())
            paramHashtable.Add("@Duration", Duration)
            paramHashtable.Add("@TripType", TripType)
            paramHashtable.Add("@Trip", Trip)
            paramHashtable.Add("@TotalBookingCost", TotalBookingCost)
            paramHashtable.Add("@TotalAfterDis", TotalAfterDis)
            paramHashtable.Add("@AdditionalMarkup", AdditionalMrk)
            paramHashtable.Add("@Adult", Adult)
            paramHashtable.Add("@Child", Child)
            paramHashtable.Add("@Infant", Infant)
            paramHashtable.Add("@AgentId", AgentId)
            paramHashtable.Add("@AgencyName", AgencyName)
            paramHashtable.Add("@DistrId", DistrId)
            paramHashtable.Add("@ExecutiveId", ExecutiveId)
            paramHashtable.Add("@PaymentType", "CL")
            paramHashtable.Add("@Ttl", PgTitle)
            paramHashtable.Add("@FName", PgFName)
            paramHashtable.Add("@LName", PgLName)
            paramHashtable.Add("@Mobile", PgMobile)
            paramHashtable.Add("@Mail", PgEmail)
            paramHashtable.Add("@VC", VC)
            paramHashtable.Add("@ResuID", ResuID)
            paramHashtable.Add("@ResuCharge", ResuCharge)
            paramHashtable.Add("@ResuServiceCharge", ResuServiceCharge)
            paramHashtable.Add("@ResuFareDiff", ResuFareDiff)
            paramHashtable.Add("@ProjectId", ProjectId)
            paramHashtable.Add("@BillNoCorp", BillNoCorp)
            paramHashtable.Add("@BookedBy", BookedBy)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltHeaderReIsue", 1)

        Catch ex As Exception
            Return 0
        End Try

    End Function
    'Get FltDetails data
    Public Function GetFltDtlsReissue(ByVal trackid As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", trackid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltDtls", 3)
    End Function

    'insert data in FltDetails Table
    Public Function insertFltDetailsReIssue(ByVal OrderID As String, ByVal DCOrAC As String, ByVal DCOrAN As String, ByVal ACOrAC As String, ByVal ACOrAN As String, ByVal DDate As String, ByVal DTime As String, ByVal ADate As String, ByVal ATime As String, ByVal AirCode As String, ByVal AirName As String, ByVal FltNo As String, ByVal AirCraft As String) As Integer
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@OrderID", OrderID)
            paramHashtable.Add("@DCOrAC", DCOrAC)
            paramHashtable.Add("@DCOrAN", DCOrAN)
            paramHashtable.Add("@ACOrAC", ACOrAC)
            paramHashtable.Add("@ACOrAN", ACOrAN)
            paramHashtable.Add("@DDate", DDate)
            paramHashtable.Add("@DTime", DTime)
            paramHashtable.Add("@ADate", ADate)
            paramHashtable.Add("@ATime", ATime)
            paramHashtable.Add("@AirCode", AirCode)
            paramHashtable.Add("@AirName", AirName)
            paramHashtable.Add("@FltNo", FltNo)
            paramHashtable.Add("@AirCraft", AirCraft)
            paramHashtable.Add("@AdtFB", "")
            paramHashtable.Add("@ChdFB", "")
            paramHashtable.Add("@InfFB", "")
            paramHashtable.Add("@AdtRbd", "")
            paramHashtable.Add("@ChdRbd", "")
            paramHashtable.Add("@InfRbd", "")
            paramHashtable.Add("@AvlSeat", 0)
            paramHashtable.Add("@UpdateDate", DateTime.Now)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltDetails", 1)
        Catch ex As Exception

        End Try
    End Function

    'get flt Fare details
    Public Function GetFltFareDtlReIssue(ByVal OrderID As String, ByVal PaxType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", OrderID)
        paramHashtable.Add("@PaxType", PaxType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltFareDtlReIssue", 3)
    End Function

    'Insert Data in flt Fare Details table
    Public Function insertFareDetailsReIssue(ByVal trackid As String, ByVal basefare As String, ByVal YQ As String, ByVal YR As String, ByVal WO As String, ByVal OT As String, ByVal totTax As String, ByVal totFare As String, ByVal SrvTax As String, ByVal TF As String, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal comm As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String, ByVal netFare As String, ByVal paxtype As String, ByVal mgtfee As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", trackid)
        paramHashtable.Add("@BaseFare", basefare)
        paramHashtable.Add("@YQ", YQ)
        paramHashtable.Add("@YR", YR)
        paramHashtable.Add("@WO", WO)
        paramHashtable.Add("@OT", OT)
        paramHashtable.Add("@Qtax", 0)
        paramHashtable.Add("@TotalTax", totTax)
        paramHashtable.Add("@TotalFare", totFare)
        paramHashtable.Add("@ServiceTax", SrvTax)
        paramHashtable.Add("@TranFee", TF)
        paramHashtable.Add("@AdminMrk", admrk)
        paramHashtable.Add("@AgentMrk", agmrk)
        paramHashtable.Add("@DistrMrk", dismrk)
        paramHashtable.Add("@TotalDiscount", comm)
        paramHashtable.Add("@PLb", 0)
        paramHashtable.Add("@Discount", 0)
        paramHashtable.Add("@CashBack", 0)
        paramHashtable.Add("@Tds", tds)
        paramHashtable.Add("@TdsOn", comm)
        paramHashtable.Add("@TotalAfterDis", netFare)
        paramHashtable.Add("@PaxType", paxtype)
        paramHashtable.Add("@MgtFee", mgtfee)
        paramHashtable.Add("@UpdateDate", DateTime.Now)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    End Function

    'Get WinYatraRpt 
    Public Function GetWinYatraRpt(ByVal FromDate As String, ByVal ToDate As String, ByVal AgentId As String, ByVal ReqType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@ReqType", ReqType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetWinYatraRpt", 3)
    End Function
    '********Net Margin Report*************

    Public Function GetNetMarginReport(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AgentID As String, ByVal Trip As String, ByVal Airline As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginId)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetNetMarginReport", 3)
    End Function
    Public Function GetPendingCreditDetails(ByVal PaymentType As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@PAYMENT_TYPE", PaymentType)
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CREDIT_DETAILS", 3)
    End Function
    '********GET Upload Details By Counter*************
    Public Function GetUploadDetailsByCounter(ByVal Counter As Integer) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Counter", Counter)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GET_UPLOADDETAILS_SALES", 3)
    End Function
    '*****************QC CheckList Report***********************'
    Public Function GetQcChecklist(ByVal FromDate As String, ByVal Todate As String, ByVal OrderId As String, ByVal ExecutiveId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", Todate)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@ExecutiveId", ExecutiveId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Sp_SearchQcChecklist", 3)
    End Function

    '********Update Upload Details Sales*************
    Public Function UpdateDetailsSales(ByVal Counter As Integer, ByVal TransactionIDOrChequeNo As String, ByVal DepositeBankName As String, ByVal RemarkSales As String, ByVal Amount As Double, ByVal DebitPortalBalance As Boolean) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Counter", Counter)
        paramHashtable.Add("@TransactionIDOrChequeNo", TransactionIDOrChequeNo)
        paramHashtable.Add("@DepositeBankName", DepositeBankName)
        paramHashtable.Add("@RemarkSales", RemarkSales)
        paramHashtable.Add("@Amount", Amount)
        paramHashtable.Add("@DebitPortalBalance", DebitPortalBalance)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_UPDATE_UPLOADDETAILS", 1)
    End Function

    '********Check Reissue and Refund Update*************
    Public Function CheckRefundReissueUpdate(ByVal PaxID As Integer, ByVal Status As String, ByVal Type As String) As Boolean
        paramHashtable.Clear()
        paramHashtable.Add("@PaxID", PaxID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Type", Type)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckRefundReissueUpdate", 2)
    End Function




    Public Function USP_GetTicketDetail_Intl_Rejected(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String,
       ByVal pnr As String, ByVal paxname As String, ByVal TicketNo As String, ByVal AirPNR As String, ByVal AgentID As String, ByVal Trip As String, ByVal Status As StatusClass) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginId)
        paramHashtable.Add("@FormDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@OderId", OrderID)
        paramHashtable.Add("@PNR", pnr)
        paramHashtable.Add("@AirlinePNR", AirPNR)
        paramHashtable.Add("@PaxName", paxname)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@AgentId", AgentID)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Status", Status.ToString())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetTicketDetail_Intl_Rejected", 3)
    End Function


    Public Function SmsCredential(ByVal ModuleType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Module", ModuleType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GET_SMS_CREDENTIAL", 3)
    End Function

    Public Function UpdateProviderUserID(ByVal trackid As String, ByVal SearchId As String, ByVal PNRId As String, ByVal TicketId As String, ByVal Provider As String, ByVal CrdType As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", trackid)
        paramHashtable.Add("@Status", PNRId)
        paramHashtable.Add("@AgName", TicketId)
        paramHashtable.Add("@GdsPnr", Provider)
        paramHashtable.Add("@AirlinePnr", CrdType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateFltHeader9999", 1)
    End Function


    Public Function OTPTransactionInsert(ByVal UserID As String, ByVal Remark As String, ByVal OTPRefNo As String, ByVal LoginByOTP As String, ByVal OTPId As String, ByVal ServiceType As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@UserID", UserID)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@OTPRefNo", OTPRefNo)
        paramHashtable.Add("@LoginByOTP", LoginByOTP)
        paramHashtable.Add("@OTPId", OTPId)
        paramHashtable.Add("@ServiceType", ServiceType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_Insert_Transaction_OTP", 1)
    End Function


End Class
