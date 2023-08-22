Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Public Class GDS_Amadeus

#Region "Variable Declaration"
    Dim Conn As New SqlConnection()
    Dim Comm As SqlCommand
    Dim Request_XML, Response_XML As String
    'Private MPRequest As New XMLRequest()
    Dim Url As String = ""
    Dim Port As String = ""
    '***Live Credentials******
    Dim c_Corporate_Id As String = ""
    Dim c_Username As String = "" '"DELVS384Q"
    Dim c_Password As String = "" '"499YXQG4"
    '***Live Credentials******

    '***Test Credentials******
    'Public c_Corporate_Id As String = "BIS-IBE-PT"
    'Public c_Username As String = "DEL1A0980"
    'Public c_Password As String = "ME5DL7SW"
    '***Test Credentials******

    'Dim Factory As New APIV2_COMLib.FactoryAPI()
    'Dim Conversation As New APIV2_COMLib.AmadeusAPI()
    'Dim OpenType As New APIV2_COMLib.OpenType()
    'Dim c_Connection As New APIV2_COMLib.ConnectionType()
    Dim rc As Boolean
    Dim Spliter, Dept, Recomm, XML_aval
    Dim AirDataTable As New DataTable
    Dim Flt_List As String
    Dim i As Integer
    Dim flt_stat As String
    Dim clos As Boolean
    Dim Amd_PNR As String
    Dim PNR_DATA_XML As String
    Dim tstRequest As New PnrTst
#End Region
    Sub New(ByVal surl As String, ByVal PortNo As String, ByVal CorpId As String, ByVal UserId As String, ByVal Password As String)
        Url = surl
        Port = PortNo
        c_Corporate_Id = CorpId
        c_Username = UserId
        c_Password = Password
    End Sub

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

    Public Function RoundAdv(ByVal dVal As Double, Optional ByVal iPrecision As Integer = 0) As Double
        Dim roundStr As String
        Dim WholeNumberPart As String
        Dim DecimalPart As String
        Dim i As Integer
        Dim RoundUpValue As Double
        roundStr = CStr(dVal)

        If InStr(1, roundStr, ".") = -1 Then
            RoundAdv = dVal
            Exit Function
        End If
        WholeNumberPart = Mid$(roundStr, 1, InStr(1, roundStr, ".") - 1)
        DecimalPart = Mid$(roundStr, (InStr(1, roundStr, ".")))
        If Len(DecimalPart) > iPrecision + 1 Then
            Select Case Mid$(DecimalPart, iPrecision + 2, 1)
                Case "0", "1", "2", "3", "4"
                    DecimalPart = Mid$(DecimalPart, 1, iPrecision + 1)
                Case "5", "6", "7", "8", "9"
                    RoundUpValue = 0.1
                    For i = 1 To iPrecision - 1
                        RoundUpValue = RoundUpValue * 0.1
                    Next
                    DecimalPart = CStr(Val(Mid$(DecimalPart, 1, iPrecision + 1)) + RoundUpValue)
                    If Mid$(DecimalPart, 1, 1) <> "1" Then
                        DecimalPart = Mid$(DecimalPart, 2)
                    Else
                        WholeNumberPart = CStr(Val(WholeNumberPart) + 1)
                        DecimalPart = ""
                    End If
            End Select
        End If
        RoundAdv = Val(WholeNumberPart & DecimalPart)
    End Function

    Public Function PNRAddElements(ByVal Adult, ByVal Child, ByVal infant, ByVal AdultTitle, ByVal AdultFirstName, ByVal AdultLastName, ByVal ChildTitle, ByVal ChildFirstName, ByVal ChildLastName, ByVal ChildDOB, ByVal InfantFirstName, ByVal InfantDOB, ByVal Mobile, ByVal Email, ByVal vali_carrier, ByVal ff_air, ByVal seat_ty_adt, ByVal meal_ty_adt, ByVal seat_ty_chd, ByVal meal_ty_chd, ByVal IATAComm, ByVal OT, ByVal TktOID) As String

        Dim ff_n = Split(ff_air, "<BR>")
        Dim ff, ff1 As Integer
        Dim ff_spl
        Dim ff_xml As String = Nothing
        Try
            If ff_air.ToString.Trim.Replace(":", "").Replace("<BR>", "") <> "" Then
                For ff = 0 To UBound(ff_n) - 1
                    ff_spl = Split(ff_n(ff), ":")
                    If ff_spl(1) = "" Then

                    Else
                        ff_xml = ff_xml + "<dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>2</number></reference><segmentName>SSR</segmentName></elementManagementData><serviceRequest><ssr> <type>FQTV</type><companyId>" & ff_spl(0) & "</companyId><indicator>P01</indicator></ssr></serviceRequest><frequentTravellerData><frequentTraveller><companyId>" & ff_spl(0) & "</companyId><membershipNumber>" & ff_spl(0) + ff_spl(1) & "</membershipNumber>        </frequentTraveller></frequentTravellerData><referenceForDataElement><reference><qualifier>PR</qualifier><number>" & ff + 1 & "</number>        </reference></referenceForDataElement></dataElementsIndiv>"
                    End If
                Next
            End If
        Catch ex As Exception
            ff_xml = ""
        End Try
        Dim s_ty() As String = Split(seat_ty_adt, "<BR>")
        Dim seat_req_adt As String = ""
        Try
            If seat_ty_adt.ToString.Trim.Replace(":", "").Replace("<BR>", "") <> "" Then
                For ff = 0 To s_ty.Length - 2
                    seat_req_adt = seat_req_adt + "<dataElementsIndiv><elementManagementData><segmentName>STR</segmentName> </elementManagementData><seatGroup><seatRequest><special><seatType>" & s_ty(ff).ToString.Trim & "</seatType></special></seatRequest></seatGroup><referenceForDataElement><reference><qualifier>PR</qualifier> <number>" & ff + 1 & "</number></reference><reference><qualifier>ST</qualifier> <number>1</number> </reference></referenceForDataElement></dataElementsIndiv>"
                    ff1 = ff + 1
                Next
            End If
        Catch ex As Exception
            seat_req_adt = ""
            ff1 = 0
        End Try
        s_ty = Split(seat_ty_chd, "<BR>")
        Dim seat_req_chd As String = ""
        Try
            If Val(Child) > 0 Then
                If seat_ty_chd.ToString.Trim.Replace(":", "").Replace("<BR>", "") <> "" Then
                    For ff = 0 To s_ty.Length - 2
                        seat_req_chd = seat_req_chd + "<dataElementsIndiv><elementManagementData><segmentName>STR</segmentName> </elementManagementData><seatGroup><seatRequest><special><seatType>" & s_ty(ff).ToString.Trim & "</seatType></special></seatRequest></seatGroup><referenceForDataElement><reference><qualifier>PR</qualifier> <number>" & ff1 + 1 & "</number></reference><reference><qualifier>ST</qualifier> <number>1</number> </reference></referenceForDataElement></dataElementsIndiv>"
                        ff1 = ff1 + 1
                    Next
                End If
            End If
        Catch ex As Exception
            seat_req_chd = ""
            ff1 = 0
        End Try
        ff1 = 0
        Dim m_ty() As String = Split(meal_ty_adt, "<BR>")
        Dim meal_req_adt As String = ""
        Try
            If meal_ty_adt.ToString.Trim.Replace(":", "").Replace("<BR>", "") <> "" Then
                For ff = 0 To m_ty.Length - 2
                    meal_req_adt = meal_req_adt + "<dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>280</number></reference><segmentName>SSR</segmentName></elementManagementData><serviceRequest><ssr><type>" & m_ty(ff).ToString.Trim & "</type><status>NN</status><quantity>1</quantity><companyId>YY</companyId><indicator>P01</indicator></ssr></serviceRequest><referenceForDataElement><reference><qualifier>PR</qualifier><number>" & ff + 1 & "</number></reference></referenceForDataElement></dataElementsIndiv>"
                    ff1 = ff + 1
                Next
            End If
        Catch ex As Exception
            meal_req_adt = ""
            ff1 = 0
        End Try
        m_ty = Split(meal_ty_chd, "<BR>")
        Dim meal_req_chd As String = ""
        Try
            If Val(Child) > 0 Then
                If meal_ty_chd.ToString.Trim.Replace(":", "").Replace("<BR>", "") <> "" Then
                    For ff = 0 To m_ty.Length - 2
                        meal_req_chd = meal_req_chd + "<dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>280</number></reference><segmentName>SSR</segmentName></elementManagementData><serviceRequest><ssr><type>" & m_ty(ff).ToString.Trim & "</type><status>NN</status><quantity>1</quantity><companyId>YY</companyId><indicator>P01</indicator></ssr></serviceRequest><referenceForDataElement><reference><qualifier>PR</qualifier><number>" & ff1 + 1 & "</number></reference></referenceForDataElement></dataElementsIndiv>"
                        ff1 = ff1 + 1
                    Next
                End If
            End If
        Catch ex As Exception
            meal_req_chd = ""
            ff1 = 0
        End Try
        Dim travel1, travelend1, travel2, travelend3
        Dim start, Adl, j, k, travel, travelend, powerend, adu, chd, inf
        start = "<PoweredPNR_AddMultiElements><pnrActions> <optionCode>0</optionCode> </pnrActions>"
        Dim total1, total, total2, total4, tra, tra1, tra2, pax
        Dim adult3 = Val(Adult)
        Dim child3 = Val(Child)
        Dim infant3 = Val(infant)
        adu = ""
        chd = ""
        inf = ""
        Dim tot_no_seat = Val(Adult) + Val(Child)
        i = Val(tot_no_seat)
        For j = 1 To i
            If adult3 = infant3 Then
                For k = 1 To Val(adult3) 'Val(Adult)
                    travel1 = "<travellerInfo><elementManagementPassenger><reference><qualifier>PR</qualifier> <number>" & j & "</number> </reference><segmentName>NM</segmentName> </elementManagementPassenger><passengerData><travellerInformation><traveller> <surname>" & AdultLastName(k - 1) & "</surname> <quantity>2</quantity></traveller><passenger><firstName>" & AdultFirstName(k - 1) & " " & AdultTitle(k - 1) & "</firstName><type>ADT</type><infantIndicator>2</infantIndicator> </passenger><passenger><firstName>" & InfantFirstName(k - 1) & "</firstName><type>INF</type></passenger></travellerInformation><dateOfBirth><dateAndTimeDetails><qualifier>706</qualifier><date>" & InfantDOB(k - 1) & "</date></dateAndTimeDetails></dateOfBirth></passengerData></travellerInfo>"
                    tra = (tra + travel1)
                    travel1 = ""
                    j = j + 1
                Next
            ElseIf adult3 > Val(infant3) And Val(infant3) > 0 Then
                For k = 1 To adult3 'Val(Adult)
                    If infant3 > 0 Then
                        travel1 = "<travellerInfo><elementManagementPassenger><reference><qualifier>PR</qualifier> <number>" & j & "</number> </reference><segmentName>NM</segmentName> </elementManagementPassenger><passengerData><travellerInformation><traveller> <surname>" & AdultLastName(k - 1) & "</surname> <quantity>2</quantity></traveller><passenger><firstName>" & AdultFirstName(k - 1) & " " & AdultTitle(k - 1) & "</firstName><type>ADT</type><infantIndicator>2</infantIndicator> </passenger><passenger><firstName>" & InfantFirstName(k - 1) & "</firstName><type>INF</type></passenger></travellerInformation><dateOfBirth><dateAndTimeDetails><qualifier>706</qualifier><date>" & InfantDOB(k - 1) & "</date></dateAndTimeDetails></dateOfBirth></passengerData></travellerInfo>"
                    Else
                        travel1 = "<travellerInfo><elementManagementPassenger><reference><qualifier>PR</qualifier> <number>" & j & "</number> </reference><segmentName>NM</segmentName> </elementManagementPassenger><passengerData><travellerInformation><traveller> <surname>" & AdultLastName(k - 1) & "</surname> <quantity>1</quantity></traveller><passenger><firstName>" & AdultFirstName(k - 1) & " " & AdultTitle(k - 1) & "</firstName><type>ADT</type></passenger></travellerInformation></passengerData></travellerInfo>"
                    End If
                    tra = (tra + travel1)
                    travel1 = ""
                    infant3 = infant3 - 1
                    j = j + 1
                Next
            Else
                For k = 1 To adult3 'Val(Adult)
                    travel1 = "<travellerInfo><elementManagementPassenger><reference><qualifier>PR</qualifier> <number>" & j & "</number> </reference><segmentName>NM</segmentName> </elementManagementPassenger><passengerData><travellerInformation><traveller> <surname>" & AdultLastName(k - 1) & "</surname> <quantity>1</quantity></traveller><passenger><firstName>" & AdultFirstName(k - 1) & " " & AdultTitle(k - 1) & "</firstName><type>ADT</type></passenger></travellerInformation></passengerData></travellerInfo>"
                    tra = (tra + travel1)
                    travel1 = ""
                    j = j + 1
                Next
            End If
            If (child3 > 0) Then
                For k = 1 To child3 'Val(child)
                    travel1 = "<travellerInfo><elementManagementPassenger><reference><qualifier>PR</qualifier> <number>" & j & "</number> </reference><segmentName>NM</segmentName> </elementManagementPassenger> <passengerData><travellerInformation><traveller> <surname>" & ChildLastName(k - 1) & "</surname> <quantity>1</quantity></traveller><passenger><firstName>" & ChildFirstName(k - 1) & " " & ChildTitle(k - 1) & "</firstName> <type>CHD</type> </passenger></travellerInformation><dateOfBirth><dateAndTimeDetails><qualifier>706</qualifier><date>" & ChildDOB(k - 1) & "</date></dateAndTimeDetails></dateOfBirth></passengerData> </travellerInfo>"
                    tra1 = (tra1 + travel1)
                    j = j + 1
                    child3 = child3 - 1
                Next
            End If
            travel = (travel + tra + tra1 + tra2)
            travel1 = ""
        Next
        '******** ES For OT ****** 
        Dim es As String = ""
        Try
            If OT = True Then
                Dim aa As DateTime = Now
                Dim todaydate As String = Format(aa, "MMddyy").ToString
                If c_Username.Trim <> TktOID.ToString.Trim Then
                    es = "<dataElementsIndiv><elementManagementData><segmentName>ES</segmentName></elementManagementData><pnrSecurity><security><identification>" & TktOID.ToString.Trim & "</identification><accessMode>B</accessMode></security><securityInfo><creationDate>" & todaydate & "</creationDate><agentCode>AASU</agentCode></securityInfo></pnrSecurity></dataElementsIndiv>"
                End If
            End If
        Catch ex As Exception
        End Try
        '******** ES For OT ******
        powerend = "<dataElementsMaster><marker1>0</marker1><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>1</number></reference><segmentName>RF</segmentName> </elementManagementData><freetextData><freetextDetail><subjectQualifier>3</subjectQualifier> <type>P22</type> </freetextDetail><longFreetext>Online Booking By </longFreetext> </freetextData></dataElementsIndiv><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>2</number></reference><segmentName>AP</segmentName> </elementManagementData><freetextData><freetextDetail><subjectQualifier>3</subjectQualifier> <type>6</type></freetextDetail><longFreetext>011-46464140</longFreetext></freetextData></dataElementsIndiv><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier> <number>3</number> </reference><segmentName>RM</segmentName> </elementManagementData><miscellaneousRemark> <remarks><type>RM</type> <freetext>" & Email & "</freetext> </remarks></miscellaneousRemark></dataElementsIndiv><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier> <number>3</number> </reference><segmentName>RM</segmentName> </elementManagementData><miscellaneousRemark> <remarks><type>RM</type> <freetext>Online Booking</freetext> </remarks></miscellaneousRemark></dataElementsIndiv><dataElementsIndiv> <elementManagementData><reference><qualifier>OT</qualifier> <number>2</number></reference><segmentName>FM</segmentName></elementManagementData><commission><passengerType>PAX</passengerType><indicator>FM</indicator><commissionInfo><percentage>" & IATAComm & "</percentage></commissionInfo></commission> </dataElementsIndiv><dataElementsIndiv><elementManagementData><segmentName>OS</segmentName> </elementManagementData><freetextData><freetextDetail><subjectQualifier>3</subjectQualifier><companyId>YY</companyId></freetextDetail><longFreetext>Mobile-" & Mobile & "</longFreetext></freetextData></dataElementsIndiv><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier><number>1</number></reference><segmentName>FV</segmentName></elementManagementData><ticketingCarrier><carrier><airlineCode>" & vali_carrier & "</airlineCode></carrier></ticketingCarrier></dataElementsIndiv><dataElementsIndiv><elementManagementData><reference><qualifier>OT</qualifier> <number>5</number></reference> <segmentName>TK</segmentName> </elementManagementData> <ticketElement> <passengerType>PAX</passengerType> <ticket><indicator>OK</indicator> </ticket> </ticketElement> </dataElementsIndiv>"
        Dim fop = "<dataElementsIndiv><elementManagementData><segmentName>FP</segmentName></elementManagementData><formOfPayment><fop><identification>CA</identification></fop></formOfPayment></dataElementsIndiv>"
        Dim en = "</dataElementsMaster></PoweredPNR_AddMultiElements>"
        'pax = (start + travel + powerend + fop + en)
        pax = (start + travel + powerend + ff_xml + seat_req_adt + seat_req_chd + meal_req_adt + meal_req_chd + fop + es + en)
        Return pax
    End Function

    Public Function Segnment_Sell(ByVal Sell_Req, ByVal Tot_Seat, ByVal Adult, ByVal Child, ByVal infant, ByVal AdultTitle, ByVal AdultFirstName, ByVal AdultLastName, ByVal ChildTitle, ByVal ChildFirstName, ByVal ChildLastName, ByVal ChildDOB, ByVal InfantFirstName, ByVal InfantDOB, ByVal Mobile, ByVal Email, ByVal vali_carrier, ByVal Trip, ByVal sector, ByVal ff_air, ByVal seat_ty_adt, ByVal meal_ty_adt, ByVal seat_ty_chd, ByVal meal_ty_chd, ByVal Tripp, ByVal OT, ByVal TktOID) As String
        Dim Sell_Header, Sell_Footer, SegmentInfo, Sell_Res As String
        Dim sel_seg1, sel_seg2, sel_seg3, Sell_Req_t, Sell_R As String

        rc = False ' Factory.createConversationFactory(Url, Port, c_Corporate_Id, c_Username, c_Password)
        'rc = Factory.createConversationFactory("195.27.163.89", 20002, c_Corporate_Id, c_Username, c_Password)
        If rc Then
            'rc = Factory.openConversationFromFactory(OpenType, Conversation)
            Sell_Header = "<PoweredLowestFare_SellFromRecommendation><messageActionDetails><messageFunctionDetails><messageFunction>183</messageFunction><additionalMessageFunction>M1</additionalMessageFunction></messageFunctionDetails></messageActionDetails>"
            If Trip = "rdbOneWay" Then
                sel_seg1 = "<itineraryDetails><originDestinationDetails><origin>" & (Sell_Req(0)).ItemArray(0) & "</origin><destination>" & (Sell_Req(0)).ItemArray(1) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                Sell_Header = Sell_Header + sel_seg1
                Sell_Footer = "</itineraryDetails></PoweredLowestFare_SellFromRecommendation>"
                For i = 0 To Sell_Req.Length - 1
                    SegmentInfo = SegmentInfo + "<segmentInformation><travelProductInformation><flightDate><departureDate>" & (Sell_Req(i)).ItemArray(10) & "</departureDate></flightDate><boardPointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(6) & "</trueLocationId></boardPointDetails><offpointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(8) & "</trueLocationId></offpointDetails><companyDetails><marketingCompany>" & (Sell_Req(i)).ItemArray(16) & "</marketingCompany></companyDetails><flightIdentification><flightNumber>" & (Sell_Req(i)).ItemArray(17) & "</flightNumber><bookingClass>" & (Sell_Req(i)).ItemArray(18) & "</bookingClass></flightIdentification></travelProductInformation><relatedproductInformation><quantity>" & Tot_Seat & "</quantity><statusCode>NN</statusCode></relatedproductInformation></segmentInformation>"
                Next
                Sell_Footer = "</itineraryDetails></PoweredLowestFare_SellFromRecommendation>"
                Sell_R = Sell_Header & SegmentInfo & Sell_Footer
            ElseIf Trip = "rdbRoundTrip" Then
                Dim Sec_Det = Split(sector, ":")
                sel_seg1 = "<itineraryDetails><originDestinationDetails><origin>" & Sec_Det(0) & "</origin><destination>" & Sec_Det(1) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                sel_seg2 = "<itineraryDetails><originDestinationDetails><origin>" & Sec_Det(2) & "</origin><destination>" & Sec_Det(3) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                'Sell_Header = Sell_Header + sel_seg1 + sel_seg2
                For i = 0 To Sell_Req.Length - 1
                    SegmentInfo = SegmentInfo + "<segmentInformation><travelProductInformation><flightDate><departureDate>" & (Sell_Req(i)).ItemArray(10) & "</departureDate></flightDate><boardPointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(6) & "</trueLocationId></boardPointDetails><offpointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(8) & "</trueLocationId></offpointDetails><companyDetails><marketingCompany>" & (Sell_Req(i)).ItemArray(16) & "</marketingCompany></companyDetails><flightIdentification><flightNumber>" & (Sell_Req(i)).ItemArray(17) & "</flightNumber><bookingClass>" & (Sell_Req(i)).ItemArray(18) & "</bookingClass></flightIdentification></travelProductInformation><relatedproductInformation><quantity>" & Tot_Seat & "</quantity><statusCode>NN</statusCode></relatedproductInformation></segmentInformation>"
                    If UCase((Sell_Req(i)).ItemArray(41)) = "O" Then
                        If (Sell_Req(i)("Leg")) = "1" Then
                            Sell_Req_t = sel_seg1
                        End If
                        Sell_Req_t = Sell_Req_t + SegmentInfo
                    End If
                    If UCase((Sell_Req(i)).ItemArray(41)) = "R" Then
                        If (Sell_Req(i)("Leg")) = "1" Then
                            Sell_Req_t = Sell_Req_t + "</itineraryDetails>"
                            Sell_Req_t = Sell_Req_t + sel_seg2
                        End If
                        Sell_Req_t = Sell_Req_t + SegmentInfo

                    End If
                    SegmentInfo = ""
                Next
                Sell_R = Sell_Header & Sell_Req_t & "</itineraryDetails></PoweredLowestFare_SellFromRecommendation>"
            ElseIf Trip.ToString.Trim.ToUpper = "rdbMultiCity".ToString.Trim.ToUpper Then
                Dim Sec_Det = Split(sector, ":")
                sel_seg1 = "<itineraryDetails><originDestinationDetails><origin>" & Sec_Det(0) & "</origin><destination>" & Sec_Det(1) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                sel_seg2 = "<itineraryDetails><originDestinationDetails><origin>" & Sec_Det(2) & "</origin><destination>" & Sec_Det(3) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                sel_seg3 = "<itineraryDetails><originDestinationDetails><origin>" & Sec_Det(4) & "</origin><destination>" & Sec_Det(5) & "</destination></originDestinationDetails><message><messageFunctionDetails><messageFunction>183</messageFunction></messageFunctionDetails></message>"
                For i = 0 To Sell_Req.Length - 1
                    SegmentInfo = SegmentInfo + "<segmentInformation><travelProductInformation><flightDate><departureDate>" & (Sell_Req(i)).ItemArray(10) & "</departureDate></flightDate><boardPointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(6) & "</trueLocationId></boardPointDetails><offpointDetails><trueLocationId>" & (Sell_Req(i)).ItemArray(8) & "</trueLocationId></offpointDetails><companyDetails><marketingCompany>" & (Sell_Req(i)).ItemArray(16) & "</marketingCompany></companyDetails><flightIdentification><flightNumber>" & (Sell_Req(i)).ItemArray(17) & "</flightNumber><bookingClass>" & (Sell_Req(i)).ItemArray(18) & "</bookingClass></flightIdentification></travelProductInformation><relatedproductInformation><quantity>" & Tot_Seat & "</quantity><statusCode>NN</statusCode></relatedproductInformation></segmentInformation>"
                    If UCase((Sell_Req(i)).ItemArray(41)) = "O" Then
                        If (Sell_Req(i)("Leg")) = "1" Then
                            Sell_Req_t = sel_seg1
                        End If
                        Sell_Req_t = Sell_Req_t + SegmentInfo
                    End If
                    If UCase((Sell_Req(i)).ItemArray(41)) = "R" Then
                        If (Sell_Req(i)("Leg")) = "1" Then
                            Sell_Req_t = Sell_Req_t + "</itineraryDetails>"
                            Sell_Req_t = Sell_Req_t + sel_seg2
                        End If
                        Sell_Req_t = Sell_Req_t + SegmentInfo
                    End If
                    If UCase((Sell_Req(i)).ItemArray(41)) = "M" Then
                        If (Sell_Req(i)("Leg")) = "1" Then
                            Sell_Req_t = Sell_Req_t + "</itineraryDetails>"
                            Sell_Req_t = Sell_Req_t + sel_seg3
                        End If
                        Sell_Req_t = Sell_Req_t + SegmentInfo
                    End If
                    SegmentInfo = ""
                Next
                Sell_R = Sell_Header & Sell_Req_t & "</itineraryDetails></PoweredLowestFare_SellFromRecommendation>"
            End If
            Sell_Res = "" ' Conversation.SendAndReceiveAsXml(Sell_R)
            flt_stat = sell_recom_chk(Sell_Res)
            If flt_stat = "False" Then
                Dim Req_Trns = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
                Dim Res_Trns = "" 'Conversation.SendAndReceiveAsXml(Req_Trns)
                clos = False 'Factory.closeConversationFromFactory(Conversation)
                Dim xx As String
                xx = GetRndm()
                Amd_PNR = "" '"Sell" & Convert.ToString(xx) & "-FQ"
                PNR_DATA_XML = "<XML><AmadeusPnr>" & Amd_PNR & "</AmadeusPnr><AirLinePnr>" & Amd_PNR & "</AirLinePnr></XML>" 'CStr(controlno) & ":" & CStr(air_pnr)
                '*********Logs********
                BookingLogs(Sell_Req(0).ItemArray(37), Sell_R, Sell_Res, "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL")
                '*********Logs End********
            Else
                Dim IATAComm As String = "0"
                Try
                    IATAComm = (Sell_Req(0)).ItemArray(53).ToString
                Catch ex As Exception

                End Try
                Dim AddElementsReq = PNRAddElements(Adult, Child, infant, AdultTitle, AdultFirstName, AdultLastName, ChildTitle, ChildFirstName, ChildLastName, ChildDOB, InfantFirstName, InfantDOB, Mobile, Email, vali_carrier, ff_air, seat_ty_adt, meal_ty_adt, seat_ty_chd, meal_ty_chd, IATAComm, OT, TktOID)
                Dim AddElementsRes = "" 'Conversation.SendAndReceiveAsXml(AddElementsReq)
                If InStr(AddElementsRes, "<errorText>") Then
                    Dim Ign_Req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
                    Dim Ign_Res = "" 'Conversation.SendAndReceiveAsXml(Ign_Req)
                    Dim clss1 = False 'Factory.closeConversationFromFactory(Conversation)
                    Dim xx As String
                    xx = GetRndm()
                    Amd_PNR = "ADDMLT" & Convert.ToString(xx) & "-FQ"
                    PNR_DATA_XML = "<XML><AmadeusPnr>" & Amd_PNR & "</AmadeusPnr><AirLinePnr>" & Amd_PNR & "</AirLinePnr></XML>" 'CStr(controlno) & ":" & CStr(air_pnr)
                    '*********Logs********
                    BookingLogs(Sell_Req(0).ItemArray(37), Sell_R, Sell_Res, AddElementsReq, AddElementsRes, "NULL", "NULL", "NULL", "NULL", "NULL", "NULL")
                    '*********Logs End********
                Else
                    Dim PriceSegReq = ""
                    Dim PriceSegRes = ""
                    '*****New Pricing With FBA Start****
                    If Tripp.ToString.Trim.ToUpper = "INT" Then
                        PriceSegReq = Price_PNR(vali_carrier)
                        PriceSegRes = "" 'Conversation.SendAndReceiveAsXml(PriceSegReq)
                    Else
                        If Sell_Req.Length >= 3 Then
                            PriceSegReq = "<PoweredFare_PricePNRWithBookingClass><overrideInformation><attributeDetails><attributeType>RP</attributeType></attributeDetails><attributeDetails><attributeType>RU</attributeType></attributeDetails><attributeDetails><attributeType>RLO</attributeType></attributeDetails></overrideInformation><validatingCarrier><carrierInformation><carrierCode>" & vali_carrier & "</carrierCode></carrierInformation></validatingCarrier></PoweredFare_PricePNRWithBookingClass>"
                            PriceSegRes = "" ' Conversation.SendAndReceiveAsXml(PriceSegReq)
                        Else
                            Dim Price As Hashtable '= PricingWithFBA.Pricing_FBA(Conversation, vali_carrier, Sell_Req, AddElementsRes)
                            PriceSegReq = Price("fare_req") ' Price_PNR(vali_carrier) '"<PoweredFare_PricePNRWithBookingClass><overrideInformation><attributeDetails><attributeType>RP</attributeType></attributeDetails><attributeDetails><attributeType>RU</attributeType></attributeDetails></overrideInformation><validatingCarrier><carrierInformation><carrierCode>" & vali_carrier & "</carrierCode></carrierInformation></validatingCarrier></PoweredFare_PricePNRWithBookingClass>"
                            PriceSegRes = Price("fare_res") ' Conversation.SendAndReceiveAsXml(PriceSegReq)
                        End If

                    End If

                    '*****New Pricing With FBA End****
                    If InStr(PriceSegRes, "<errorText>") Then
                        Dim Ign_Req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
                        Dim Ign_Res = "" 'Conversation.SendAndReceiveAsXml(Ign_Req)
                        Dim clss1 = False ' Factory.closeConversationFromFactory(Conversation)
                        Dim xx As String
                        xx = GetRndm()
                        Amd_PNR = "PRC" & Convert.ToString(xx) & "-FQ"
                        PNR_DATA_XML = "<XML><AmadeusPnr>" & Amd_PNR & "</AmadeusPnr><AirLinePnr>" & Amd_PNR & "</AirLinePnr></XML>" 'CStr(controlno) & ":" & CStr(air_pnr)
                        '*********Logs********
                        BookingLogs(Sell_Req(0).ItemArray(37), Sell_R, Sell_Res, AddElementsReq, AddElementsRes, PriceSegReq, PriceSegRes, "NULL", "NULL", "NULL", "NULL")
                        '*********Logs End********
                    Else

                        Dim TstSegReq = PNR_TST(Adult, Child, infant) ' tstRequest.getTstRequest(Val(Adult), Val(Child), Val(infant), Sell_Req, PriceSegRes, AddElementsRes, vali_carrier, Tripp) 'PNR_TST(Adult, Child, infant)
                        Dim TstSegRes = "" ' Conversation.SendAndReceiveAsXml(TstSegReq)
                        If InStr(TstSegRes, "<errorText>") Then
                            Dim Ign_Req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
                            Dim Ign_Res = "" ' Conversation.SendAndReceiveAsXml(Ign_Req)
                            Dim clss1 = False ' Factory.closeConversationFromFactory(Conversation)
                            Dim xx As String
                            xx = GetRndm()
                            Amd_PNR = "TST" & Convert.ToString(xx) & "-FQ"
                            PNR_DATA_XML = "<XML><AmadeusPnr>" & Amd_PNR & "</AmadeusPnr><AirLinePnr>" & Amd_PNR & "</AirLinePnr></XML>" 'CStr(controlno) & ":" & CStr(air_pnr)
                            '*********Logs********
                            BookingLogs(Sell_Req(0).ItemArray(37), Sell_R, Sell_Res, AddElementsReq, AddElementsRes, PriceSegReq, PriceSegRes, TstSegReq, TstSegRes, "NULL", "NULL")
                            '*********Logs End********
                        Else
                            PNR_DATA_XML = PNR_Create(Sell_Req(0).ItemArray(37), Sell_R, Sell_Res, AddElementsReq, AddElementsRes, PriceSegReq, PriceSegRes, TstSegReq, TstSegRes)
                        End If
                    End If
                End If
            End If
        End If
        clos = False 'Factory.closeConversationFromFactory(Conversation)
        Return PNR_DATA_XML
    End Function

    Public Function Price_PNR(ByVal val_car As String) As String
        Dim fare = "<PoweredFare_PricePNRWithBookingClass><overrideInformation><attributeDetails><attributeType>RP</attributeType></attributeDetails><attributeDetails><attributeType>RU</attributeType></attributeDetails><attributeDetails><attributeType>RLO</attributeType></attributeDetails></overrideInformation><validatingCarrier><carrierInformation><carrierCode>" & val_car & "</carrierCode></carrierInformation></validatingCarrier></PoweredFare_PricePNRWithBookingClass>"
        Return fare
    End Function

    Public Function PNR_TST(ByVal Adult As Int16, ByVal Child As Int16, ByVal Infant As Int16) As String
        Dim tkt As String = Nothing
        If (Val(Adult) > 0 And Val(Child) = 0 And Val(Infant) = 0) Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>1</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) > 0) And Val(Infant) = 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>1</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>2</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) > 0) And Val(Infant) > 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>1</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>2</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>3</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) = 0) And Val(Infant) > 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>1</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>2</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        End If
        Return tkt
    End Function

    Public Function PNR_Create(ByVal SearchValue As String, ByVal ReqSell As String, ByVal ResSell As String, ByVal ReqAddmulti As String, ByVal ResAddmulti As String, ByVal ReqPrice As String, ByVal ResPrice As String, ByVal ReqTst As String, ByVal ResTst As String) As String
        Dim i, ig_rtr, ig_rtr_res, PNR_DET, pnr_res
        Dim controlno, air_pnr As String
        Dim Pnr_req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>11</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        pnr_res = "" 'Conversation.SendAndReceiveAsXml(Pnr_req)
        'AirLine PNR
        Try
            Dim aa = Split(pnr_res, "<securityInformation>")
            Dim DD = aa(0)
            Dim x, xx, xxx
            x = "<xml>"
            xx = "</PoweredPNR_PNRReply></xml>"
            xxx = x & DD & xx
            Dim reader As New System.Xml.XmlDocument
            reader.LoadXml(xxx)
            Dim dsn As New DataSet
            dsn.ReadXml(New XmlNodeReader(reader))
            controlno = dsn.Tables(3).Rows(0).Item(1)
            'Retrive Airline PNR
            Dim sw As StringWriter = New StringWriter()
            Dim xw As XmlTextWriter = New XmlTextWriter(sw)
            Dim tckt As New System.Xml.XmlDocument
            air_pnr = airline_locator(pnr_res)
            If air_pnr = "" Then
                For i = 1 To 2
                    Dim dt As DateTime = DateTime.Now.AddSeconds(6)
                    While DateTime.Now < dt
                        '------Do nothing-------- 
                    End While
                    ig_rtr = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>21</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
                    ig_rtr_res = "" ' Conversation.SendAndReceiveAsXml(ig_rtr)
                    air_pnr = airline_locator(ig_rtr_res)
                    If air_pnr <> "" Then
                        Exit For
                    End If
                Next
                '*********Logs********
                BookingLogs(SearchValue, ReqSell, ResSell, ReqAddmulti, ResAddmulti, ReqPrice, ResPrice, ReqTst, ResTst, Pnr_req, pnr_res)
                '*********Logs End********
            End If
            PNR_DET = "<XML><AmadeusPnr>" & CStr(controlno) & "</AmadeusPnr><AirLinePnr>" & air_pnr & "</AirLinePnr></XML>" 'CStr(controlno) & ":" & CStr(air_pnr)
            'AirLine PNR End
            Return PNR_DET
        Catch ex As Exception
            Dim xx As String
            xx = GetRndm()
            PNR_DET = "<XML><AmadeusPnr>PNRS" & Convert.ToString(xx) & "-FQ</AmadeusPnr>" & "<AirLinePnr>" & "PNRS" & Convert.ToString(xx) & "-FQ</AirLinePnr></XML>"
            '*********Logs********
            BookingLogs(SearchValue, ReqSell, ResSell, ReqAddmulti, ResAddmulti, ReqPrice, ResPrice, ReqTst, ResTst, Pnr_req, pnr_res)
            '*********Logs End********
            Return PNR_DET
        End Try
    End Function

    Private Function airline_locator(ByVal pnr As String) As String
        Dim air_rloc, result
        Try
            Dim xmldoc As New System.Xml.XmlDocument
            xmldoc.LoadXml(pnr)
            Dim sw As StringWriter = New StringWriter()
            Dim xw As XmlTextWriter = New XmlTextWriter(sw)
            xmldoc.WriteTo(xw)
            result = sw.ToString
            Dim split_pnr = Split(result, "<reservation>")
            split_pnr = Split(split_pnr(2), "<controlNumber>")
            split_pnr = Split(split_pnr(1), "</controlNumber>")
            air_rloc = split_pnr(0)
        Catch ex As Exception
            air_rloc = ""
            Return air_rloc
        End Try
        Return air_rloc
    End Function

    Public Function sell_recom_chk(ByVal Sell_Res As String) As String
        Dim flg As String = "True"
        Dim x, result1, tot
        Try
            Dim xmldoc As New System.Xml.XmlDocument
            xmldoc.LoadXml(Sell_Res)

            Dim sw As StringWriter = New StringWriter()
            Dim xw As XmlTextWriter = New XmlTextWriter(sw)
            xmldoc.WriteTo(xw)
            x = sw.ToString
            result1 = x
            result1 = result1.Replace("<", "")
            result1 = result1.Replace(">", "")
            result1 = result1.Replace("/", "")

            Dim seg_info = Split(result1, "segmentInformation")
            tot = UBound(seg_info)
            Dim i As Integer
            Dim status
            Dim code As New ArrayList
            Dim ii
            ii = 0
            For i = 1 To tot - 1 Step 2
                status = Split(seg_info(i), "statusCode")
                If UCase(status(1)) <> "OK" Then
                    flg = "False"
                End If
            Next
        Catch ex As Exception
            Return flg = "False"
        End Try
        Return flg
    End Function

    Private Sub Connection()

        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If
        Try
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Conn.Open()
        Catch ex As Exception
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Conn.Open()
        End Try

    End Sub

    Public Sub BookingLogs(ByVal SearchValue As String, ByVal ReqSell As String, ByVal ResSell As String, ByVal ReqAddmulti As String, ByVal ResAddmulti As String, ByVal ReqPrice As String, ByVal ResPrice As String, ByVal ReqTst As String, ByVal ResTst As String, ByVal ReqPnr As String, ByVal ResPnr As String)
        Connection()
        Dim str As String = ""
        Dim cmd As SqlCommand
        Try
            str = "insert into bookinglogs (SearchValue,Sell_Req,Sell_Res,Addmulti_Req,Addmulti_Res,Price_Req,Price_Res,Tst_Req,Tst_Res,Pnr_Req,Pnr_Res)values('" & SearchValue & "','" & ReqSell.Replace("'", "") & "','" & ResSell.Replace("'", "") & "','" & ReqAddmulti.Replace("'", "") & "','" & ResAddmulti.Replace("'", "") & "','" & ReqPrice.Replace("'", "") & "','" & ResPrice.Replace("'", "") & "','" & ReqTst.Replace("'", "") & "','" & ResTst.Replace("'", "") & "','" & ReqPnr.Replace("'", "") & "','" & ResPnr.Replace("'", "") & "')"
            cmd = New SqlCommand(str, Conn)
            cmd.ExecuteNonQuery()
            Conn.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class
