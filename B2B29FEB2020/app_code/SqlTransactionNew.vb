Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Data
Public Class SqlTransactionNew
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable

    Public Function GetAirportList(ByVal code As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@param1", code)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CityAutoSearch", 3)
    End Function
    Public Function GetAirportListFD(ByVal code As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@param1", code)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CityAutoSearchFD", 3)
    End Function
    Public Function checkseatFDD(ByVal referenceNo As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@referenceNo", referenceNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CHECKSEATFDD", 3)
    End Function
    Public Function UPDATESEATFDD(ByVal totalpax As String, ByVal referenceNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@totalpax", totalpax)
        paramHashtable.Add("@referenceNo", referenceNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_updateSeatFDD", 2)
    End Function
    Public Function GetAirlinesList(ByVal airline As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Param1", airline)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AilrineList", 3)
    End Function
    Public Function GetAgencyList(ByVal code As String, ByVal UserType As String, ByVal DistrId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@param1", code)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@DistrId", DistrId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyAutoSearch", 3)
    End Function
    Public Function GetCredentials(ByVal Provider As String, ByVal CrdType As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Provider", Provider)
        paramHashtable.Add("@CrdType", CrdType)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSrvCredentials", 3)
    End Function
    Public Function GetTktCredentials(ByVal VC As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@VC", VC)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetTicketingCrd", 3)
    End Function

    Public Function GetTktCredentials_GAL(ByVal VC As String, ByVal trip As String, ByVal crdType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@trip", trip.Trim())
        paramHashtable.Add("@CrdType", crdType.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetTicketingCrdGAL", 3)
    End Function

    'Public Function InsertLccBkgLogs(ByVal Airline As String, ByVal OrderId As String, ByVal PnrNo As String, ByVal BookReq As String, ByVal BookRes As String, ByVal AddPayReq As String, ByVal AddPayRes As String, ByVal ConfPayRes As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@Airline", Airline)
    '    paramHashtable.Add("@OrderId", OrderId)
    '    paramHashtable.Add("@PnrNo", PnrNo)
    '    paramHashtable.Add("@BookReq", BookReq)
    '    paramHashtable.Add("@BookRes", BookRes)
    '    paramHashtable.Add("@AddPayReq", AddPayReq)
    '    paramHashtable.Add("@AddPayRes", AddPayRes)
    '    paramHashtable.Add("@ConPayRes", ConfPayRes)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertLccBkgLogs", 1)
    'End Function

    Public Function InsertLccBkgLogs(ByVal Airline As String, ByVal OrderId As String, ByVal PnrNo As String, ByVal BookReq As String, ByVal BookRes As String, ByVal AddPayReq As String, ByVal AddPayRes As String, ByVal ConfPayRes As String, ByVal SJKREQ As String, ByVal SJKRES As String, ByVal UPPAXREQ As String, ByVal UPPAXRES As String, ByVal APBREQ As String, ByVal APBRES As String, ByVal EXCEPTION As String, ByVal UCCONREQ As String, ByVal UCCONRES As String, ByVal STATEREQ As String, ByVal STATERES As String, ByVal SBREQ As String, ByVal SBRES As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PnrNo", PnrNo)
        paramHashtable.Add("@BookReq", BookReq)
        paramHashtable.Add("@BookRes", BookRes)
        paramHashtable.Add("@AddPayReq", AddPayReq)
        paramHashtable.Add("@AddPayRes", AddPayRes)
        paramHashtable.Add("@ConPayRes", ConfPayRes)
        paramHashtable.Add("@SJKREQ", SJKREQ)
        paramHashtable.Add("@SJKRES", SJKRES)
        paramHashtable.Add("@UPPAXREQ", UPPAXREQ)
        paramHashtable.Add("@UPPAXRES", UPPAXRES)
        paramHashtable.Add("@APBREQ", APBREQ)
        paramHashtable.Add("@APBRES", APBRES)
        paramHashtable.Add("@OTHER", EXCEPTION)
        paramHashtable.Add("@UCCONREQ", UCCONREQ)
        paramHashtable.Add("@UCCONRES", UCCONRES)
        paramHashtable.Add("@STATEREQ", STATEREQ)
        paramHashtable.Add("@STATERES", STATERES)
        paramHashtable.Add("@SBREQ", SBREQ)
        paramHashtable.Add("@SBRES", SBRES)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertLccBkgLogs_V1", 1)
    End Function
    Public Function InsertLccBkgLogs_Old(ByVal Airline As String, ByVal OrderId As String, ByVal PnrNo As String, ByVal BookReq As String, ByVal BookRes As String, ByVal AddPayReq As String, ByVal AddPayRes As String, ByVal ConfPayRes As String, ByVal SJKREQ As String, ByVal SJKRES As String, ByVal UPPAXREQ As String, ByVal UPPAXRES As String, ByVal APBREQ As String, ByVal APBRES As String, ByVal EXCEPTION As String, ByVal UCCONREQ As String, ByVal UCCONRES As String, ByVal STATEREQ As String, ByVal STATERES As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PnrNo", PnrNo)
        paramHashtable.Add("@BookReq", BookReq)
        paramHashtable.Add("@BookRes", BookRes)
        paramHashtable.Add("@AddPayReq", AddPayReq)
        paramHashtable.Add("@AddPayRes", AddPayRes)
        paramHashtable.Add("@ConPayRes", ConfPayRes)
        paramHashtable.Add("@SJKREQ", SJKREQ)
        paramHashtable.Add("@SJKRES", SJKRES)
        paramHashtable.Add("@UPPAXREQ", UPPAXREQ)
        paramHashtable.Add("@UPPAXRES", UPPAXRES)
        paramHashtable.Add("@APBREQ", APBREQ)
        paramHashtable.Add("@APBRES", APBRES)
        paramHashtable.Add("@OTHER", EXCEPTION)
        paramHashtable.Add("@UCCONREQ", UCCONREQ)
        paramHashtable.Add("@UCCONRES", UCCONRES)
        paramHashtable.Add("@STATEREQ", STATEREQ)
        paramHashtable.Add("@STATERES", STATERES)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertLccBkgLogs", 1)
    End Function
    Public Function InsertGdsBkgLogs(ByVal OrderId As String, ByVal PnrHT As Hashtable) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@Pnr", PnrHT("ADTPNR") & "," & PnrHT("CHDPNR"))
        paramHashtable.Add("@SSReq", PnrHT("SSReq"))
        paramHashtable.Add("@SSRes", PnrHT("SSRes"))
        paramHashtable.Add("@PNBFReq1", PnrHT("PNBF1Req"))
        paramHashtable.Add("@PNBFRes1", PnrHT("PNBF1Res"))
        paramHashtable.Add("@PNBFReq2", PnrHT("PNBF2Req"))
        paramHashtable.Add("@PNBFRes2", PnrHT("PNBF2Res"))
        paramHashtable.Add("@PNBFReq3", PnrHT("PNBF3Req"))
        paramHashtable.Add("@PNBFRes3", PnrHT("PNBF3Res"))
        paramHashtable.Add("@SEReq", PnrHT("SEReq"))
        paramHashtable.Add("@SERes", PnrHT("SERes"))
        paramHashtable.Add("@Other", PnrHT("Other"))
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertGdsBkgLogs", 1)
    End Function

    Public Function InsertGdsTktLogs(ByVal OrderId As String, ByVal TKTHT As Hashtable) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@SSReq", TKTHT("SSReq"))
        paramHashtable.Add("@SSRes", TKTHT("SSRes"))
        paramHashtable.Add("@PNRRTReq", TKTHT("PNRRTReq"))
        paramHashtable.Add("@PNRRTRes", TKTHT("PNRRTRes"))
        paramHashtable.Add("@DOCPRDReq", TKTHT("DOCPRDReq"))
        paramHashtable.Add("@DOCPRDRes", TKTHT("DOCPRDRes"))
        paramHashtable.Add("@PNRRT2Req", TKTHT("PNRRT2Req"))
        paramHashtable.Add("@PNRRT2Res", TKTHT("PNRRT2Res"))
        paramHashtable.Add("@SEReq", TKTHT("SEReq"))
        paramHashtable.Add("@SERes", TKTHT("SERes"))
        'Add more for Re-price XML 
        paramHashtable.Add("@PNRBF1Req", TKTHT("PNRBF1Req"))
        paramHashtable.Add("@PNRBF1Res", TKTHT("PNRBF1Res"))
        paramHashtable.Add("@PNRBF2Req", TKTHT("PNRBF2Req"))
        paramHashtable.Add("@PNRBF2Res", TKTHT("PNRBF2Res"))
        paramHashtable.Add("@PNRBF3Req", TKTHT("PNRBF3Req"))
        paramHashtable.Add("@PNRBF3Res", TKTHT("PNRBF3Res"))
        paramHashtable.Add("@PNRBF4Req", TKTHT("PNRBF4Req"))
        paramHashtable.Add("@PNRBF4Res", TKTHT("PNRBF4Res"))
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertTktLogGal", 1)
    End Function
    Public Function UpdateTktNumber(ByVal OrderId As String, ByVal PaxId As Integer, ByVal Tktno As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@TktNo", Tktno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateTktNo", 1)
    End Function

    Public Function SmsLogDetails(ByVal OrderId As String, ByVal mobno As String, ByVal smstext As String, ByVal status As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@mobno", mobno)
        paramHashtable.Add("@txt", smstext)
        paramHashtable.Add("@statcd", status)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "smscd", 1)
    End Function
    Public Function BlockBookingAirlineWise(ByVal org As String, ByVal Dest As String, ByVal FlightNo As String, ByVal Airline As String, ByVal Trip As String, ByVal Fare As Double, ByVal Adult As Integer, ByVal Child As Integer, ByVal Infant As Integer) As String
        paramHashtable.Clear()
        paramHashtable.Add("@Org", org)
        paramHashtable.Add("@Dest", Dest)
        paramHashtable.Add("@FlightNo", FlightNo)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Fare", Fare)
        paramHashtable.Add("@Adt", Adult)
        paramHashtable.Add("@Chd", Child)
        paramHashtable.Add("@Inf", Infant)
        Dim str As String = objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SpBlockBookingAirlineWise", 2)
        Return str
    End Function
    Function GetRandomNumber(ByVal allowedChars1 As String) As String
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars1.Split(sep)
        Dim rndString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To 4
            temp = arr(rand.[Next](0, arr.Length))
            rndString += temp
        Next
        Return rndString
    End Function

    Public Function GetRndm() As String
        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim rnmd2 As String = GetRandomNumber(allowedChars)
        Return rnmd2
    End Function

    Public Function GetCharteredFlightDetails(ByVal OrgDestFrom As String, ByVal OrgDestTo As String, ByVal DepartureDate As String, ByVal MarketingCarrier As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrgDestFrom", OrgDestFrom)
        paramHashtable.Add("@OrgDestTo", OrgDestTo)
        paramHashtable.Add("@DepartureDate", DepartureDate)
        paramHashtable.Add("@MarketingCarrier", MarketingCarrier)
        paramHashtable.Add("@Trip", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_SearchCharteredFlight", 3)
    End Function

    'Added 05/04/2014 - Manish
    Public Function Get_MEAL_BAG_FareDetails(ByVal OrderId As String, ByVal PaxId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OID", OrderId)
        paramHashtable.Add("@paxid", PaxId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_SELECT_MBAG_DETAILS", 3)
    End Function

    Public Function Get_PAX_MB_Details(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal Trip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OID", OrderId)
        paramHashtable.Add("@paxid", TransTD)
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@TT", Trip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_PX_MB_PRICE", 3)
    End Function

    Public Function Update_PAX_BG_Price(ByVal OrderId As String, ByVal Amount As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@Amount", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Update_ML_BGPrice", 1)
    End Function

    Public Function Update_NET_TOT_Fare(ByVal OrderId As String, ByVal Amount As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@Amount", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Update_Tot_Net_Fare", 2)
    End Function

    Public Function Insert_SSR_Log(ByVal OrderId As String, ByVal Signature As String, ByVal SSRXML As String, ByVal Status As String, ByVal tot As Decimal) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@sign", Signature)
        paramHashtable.Add("@ssr", SSRXML)
        paramHashtable.Add("@stat", Status)
        paramHashtable.Add("@tot", tot)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_InsertSSR_LOG", 1)
    End Function

    'Added 08/05/2014 - Manish
    Public Function GetSSR_Log_Detail(ByVal OrderId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETSSR_LOG", 3)
    End Function
    Public Function GetCountryCode(ByVal country As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Param1", country)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CountryList", 3)
    End Function

    Public Function GetGstTQ(ByVal OrderID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("OrderID", OrderID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetgstTQ", 3)
    End Function
End Class
