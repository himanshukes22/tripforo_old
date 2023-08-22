Imports Microsoft.VisualBasic

Public Class XMLRequest
    Public Function XML_MP(ByVal DestFrom As String, ByVal DestTo As String, ByVal DepDate As String, ByVal ADT As Int16, ByVal CHD As Int16, ByVal INF As Int16, ByVal Cabin As String, ByVal AirLine As String, ByVal Full_eco As String, ByVal Ntrip As String, ByVal TimeD As String) As String
        Dim j As Integer, k As Integer
        Dim Fare_Type As String, Trip_XML As String
        Dim Adult As String, Child As String, Infant As String
        Dim NS As String
        Dim Itinerary As String
        Dim Req_aval As New FltRequest()
        Req_aval.DestFrom = DestFrom
        Req_aval.DestTo = DestTo
        Req_aval.Deptdate = DepDate
        Req_aval.Adult = ADT
        Req_aval.Child = CHD
        Req_aval.Infant = INF
        Req_aval.Cabin = Cabin
        Req_aval.Airline = AirLine
        Req_aval.Currency = ""
        Req_aval.NoOfPax = DirectCast((Req_aval.Adult + Req_aval.Child), Int16)
        Req_aval.NoNStop = Ntrip
        Req_aval.TimeD = TimeD

    

        If Req_aval.Cabin Is Nothing Then
            Req_aval.Cabin = "Y"
        End If

        If Full_eco = "full" Then
            Fare_Type = "<fareOptions><pricingTickInfo><pricingTicketing><priceType>NR</priceType><priceType>RP</priceType><priceType>RU</priceType><priceType>TAC</priceType><priceType>ET</priceType></pricingTicketing></pricingTickInfo></fareOptions><travelFlightInfo><cabinId><cabin>" & Req_aval.Cabin & "</cabin></cabinId>"
        Else
            Fare_Type = "<fareOptions><pricingTickInfo><pricingTicketing><priceType>RP</priceType><priceType>RU</priceType><priceType>TAC</priceType><priceType>ET</priceType></pricingTicketing></pricingTickInfo></fareOptions><travelFlightInfo><cabinId><cabin>" & Req_aval.Cabin & "</cabin></cabinId>"
        End If
        Dim Request_Header As String = "<PoweredLowestFare_Search><numberOfUnit><unitNumberDetail><numberOfUnits>" & Req_aval.NoOfPax & "</numberOfUnits><typeOfUnit>PX</typeOfUnit></unitNumberDetail><unitNumberDetail><numberOfUnits>200</numberOfUnits><typeOfUnit>RC</typeOfUnit></unitNumberDetail></numberOfUnit>"

        If Req_aval.TimeD = "AnyTime" Then
            Itinerary = "<itinerary><requestedSegmentRef><segmentRef>1</segmentRef></requestedSegmentRef><departureLocalization><depMultiCity><locationId>" & Req_aval.DestFrom & "</locationId></depMultiCity>" & "</departureLocalization><arrivalLocalization><arrivalMultiCity><locationId>" & Req_aval.DestTo & "</locationId></arrivalMultiCity></arrivalLocalization><timeDetails><firstDateTimeDetail><date>" & Req_aval.Deptdate & "</date></firstDateTimeDetail></timeDetails></itinerary></PoweredLowestFare_Search>"
        Else
            Itinerary = "<itinerary><requestedSegmentRef><segmentRef>1</segmentRef></requestedSegmentRef><departureLocalization><depMultiCity><locationId>" & Req_aval.DestFrom & "</locationId></depMultiCity>" & "</departureLocalization><arrivalLocalization><arrivalMultiCity><locationId>" & Req_aval.DestTo & "</locationId></arrivalMultiCity></arrivalLocalization><timeDetails><firstDateTimeDetail><timeQualifier>TD</timeQualifier><date>" & Req_aval.Deptdate & "</date><time>" & Req_aval.TimeD & "</time><timeWindow>4</timeWindow></firstDateTimeDetail></timeDetails></itinerary></PoweredLowestFare_Search>"
        End If

        Dim Flight_Info As String = Nothing
        If Req_aval.Airline <> "" And Ntrip <> "Y" Then
            Req_aval.Preference = "M"
            Flight_Info = "<companyIdentity><carrierQualifier>" & Req_aval.Preference & "</carrierQualifier><carrierId>" & Req_aval.Airline & "</carrierId></companyIdentity></travelFlightInfo>"
        ElseIf Req_aval.Airline <> "" And Ntrip = "Y" Then
            Req_aval.Preference = "M"
            Flight_Info = "<companyIdentity><carrierQualifier>" & Req_aval.Preference & "</carrierQualifier><carrierId>" & Req_aval.Airline & "</carrierId></companyIdentity><flightDetail><flightType>N</flightType><flightType>D</flightType></flightDetail></travelFlightInfo>"
        ElseIf Ntrip = "Y" Then
            Flight_Info = "<flightDetail><flightType>N</flightType><flightType>D</flightType></flightDetail></travelFlightInfo>"
        Else
            Flight_Info = "</travelFlightInfo>"
        End If
        Fare_Type = Fare_Type + Flight_Info

        'If Ntrip = "Y" Then
        '    Trip_XML = "<flightDetail><flightType>N</flightType><flightType>D</flightType></flightDetail>"
        'Else
        'End If

        Adult = ""
        Child = ""
        Infant = ""
        For j = 1 To Req_aval.NoOfPax
            If Req_aval.Adult > 0 Then
                Adult = "<paxReference><ptc>ADT</ptc>"
                For k = 1 To Req_aval.Adult
                    Adult = Adult & " <traveller>  <ref>" & j & "</ref>   </traveller>"
                    j = j + 1
                Next
                Adult = Adult & "</paxReference>"
            Else
                Adult = ""
            End If

            If Req_aval.Child > 0 Then
                Child = "<paxReference> <ptc>CH</ptc> "
                For k = 1 To Req_aval.Child
                    Child = Child & " <traveller>  <ref>" & j & "</ref>  </traveller> "
                    j = j + 1
                Next
                Child = Child & "</paxReference>"
            Else

                Child = ""
            End If

            If Req_aval.Infant > 0 Then
                Infant = Infant & "<paxReference> <ptc>INF</ptc>"
                For k = 1 To Req_aval.Infant
                    Infant = Infant & "<traveller>  <ref>" & k & "</ref>   <infantIndicator>1</infantIndicator>   </traveller>  "
                    j = j + 1
                Next
                Infant = Infant & "</paxReference>"
            Else

                Infant = ""
            End If
        Next
        Dim Request_XML As String
        Dim Pax As String = Adult + Child + Infant
        Request_XML = Request_Header + Pax + Fare_Type + Itinerary
        Return Request_XML

    End Function
End Class
