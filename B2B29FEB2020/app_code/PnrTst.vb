Imports Microsoft.VisualBasic
Imports System.Xml
Public Class PnrTst
    Public airmrkup As New markup

    Public Function getTstRequest(ByVal Adt As Integer, ByVal Chd As Integer, ByVal Inf As Integer, ByVal AdtFare As String, ByVal ChdFare As String, ByVal InfFare As String, ByVal PriceRes As String, ByVal addmultiRes As String, ByVal airline As String) As String
        Dim tkt As Hashtable
        Dim fare As ArrayList
        Dim adttstqulf As String = ""
        Dim chdtstqulf As String = ""
        Dim inftstqulf As String = ""
        Dim TSTReq As String = ""
        tkt = tktdetails(addmultiRes)
        fare = TST_Read(PriceRes, "")

        Dim admin_mrk As String = ""
        Dim agent_mrk As String = ""
        Dim perpax_mrk As String = ""
        Dim totnoofpax As Integer = Adt + Chd
        Try
            admin_mrk = airmrkup.AirlineMarkUp("ADMIN", airline.ToString.Trim, totnoofpax, System.Web.HttpContext.Current.Session("UID").ToString.Trim)
            agent_mrk = airmrkup.AirlineMarkUp("AGENT", airline.ToString.Trim, totnoofpax, System.Web.HttpContext.Current.Session("UID").ToString.Trim)
            perpax_mrk = (((Val(admin_mrk) + Val(agent_mrk)) / totnoofpax)).ToString 
            
        Catch ex As Exception
        End Try

        Dim tktno As ArrayList = tkt("TktNoArrayList")
        Try
            For i As Integer = 0 To tktno.Count - 1
                Dim tktsplit() As String = Split(tktno(i), "::")
                For ii As Integer = 0 To fare.Count - 1
                    Dim splitfare() As String = Split(fare(ii), "#")
                    If Adt > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "ADT" And splitfare(2).Split("/")(0) = "PA" Then
                            If Val(splitfare(1)) = ((Val(AdtFare) / Adt) - Val(perpax_mrk)) Then
                                adttstqulf = Left(fare(ii), 1)
                            Else
                                'adttstqulf = "INVALIDFARE"
                            End If
                        End If
                    End If
                    If Chd > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "CHD" And splitfare(2).Split("/")(0) = "PA" Then
                            If Val(splitfare(1)) = ((Val(ChdFare) / Chd) - Val(perpax_mrk)) Then
                                chdtstqulf = Left(fare(ii), 1)
                            Else
                                'chdtstqulf = "INVALIDFARE"
                            End If
                        End If
                    End If
                    If Inf > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "INF" And splitfare(2).Split("/")(0) = "PI" Then
                            If Val(splitfare(1)) = (Val(InfFare) / Inf) Then
                                inftstqulf = Left(fare(ii), 1)
                            Else
                                inftstqulf = "INVALIDFARE"
                            End If
                        End If
                    End If
                Next
            Next
            Dim chkfare As Boolean = False
            If Adt > 0 Then If adttstqulf = "" Then chkfare = False Else chkfare = True
            If Chd > 0 Then If chdtstqulf = "" Then chkfare = False Else chkfare = True

            ' If adttstqulf = "" Or chdtstqulf = "" Then
            If chkfare = False Then
                TSTReq = ""
            Else
                If inftstqulf = "INVALIDFARE" Then
                    TSTReq = inftstqulf
                Else
                    TSTReq = PNR_TST(Adt, Chd, Inf, adttstqulf, chdtstqulf, inftstqulf)
                End If
            End If

        Catch ex As Exception
        End Try
        Return TSTReq
    End Function

    Public Function tktdetails(ByVal xmlstr As String) As Hashtable
        Dim xmldoc As New XmlDocument
        xmldoc.LoadXml(xmlstr)
        Dim xnode As XmlNodeList
        Dim tktno1() As String
        Dim tktno As String = String.Empty
        Dim ptno As String = String.Empty
        Dim tktnoArraylist As New ArrayList
        Dim tktnoArraylist1 As New ArrayList
        Dim FAElement As Boolean = False
        xnode = xmldoc.SelectNodes("/PoweredPNR_PNRReply/dataElementsMaster/dataElementsIndiv")
        For i As Integer = 0 To xnode.Count - 1
            For ii As Integer = 0 To xnode.Item(i).ChildNodes.Count - 1
                If xnode.Item(i).ChildNodes(ii).Name = "elementManagementData" Then
                    For fv As Integer = 0 To xnode.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                        If xnode.Item(i).ChildNodes(ii).ChildNodes(fv).Name = "segmentName" Then
                            If xnode.Item(i).ChildNodes(ii).ChildNodes(fv).InnerText = "FA" Then
                                FAElement = True
                            End If
                        End If
                    Next
                End If
                If FAElement = True Then
                    If xnode.Item(i).ChildNodes(ii).Name = "otherDataFreetext" Then
                        For tkt As Integer = 0 To xnode.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                            If xnode.Item(i).ChildNodes(ii).ChildNodes(tkt).Name = "longFreetext" Then
                                tktno1 = Split(xnode.Item(i).ChildNodes(ii).ChildNodes(tkt).InnerText, "/")
                                tktno = tktno1(0).ToString
                            End If
                        Next
                    ElseIf xnode.Item(i).ChildNodes(ii).Name = "referenceForDataElement" Then
                        For pt As Integer = 0 To xnode.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                            If xnode.Item(i).ChildNodes(ii).ChildNodes(pt).Name = "reference" Then
                                For qulf As Integer = 0 To xnode.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes.Count - 1
                                    If xnode.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(qulf).InnerText = "PT" Then
                                        ptno = xnode.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(qulf).NextSibling.InnerText
                                        FAElement = False
                                        tktnoArraylist.Add(ptno & "::" & tktno)
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            Next
        Next
        '***************** Ticket No. with Passenger Details Start******************
        ptno = String.Empty
        Dim srnm As String = String.Empty
        Dim fstnm As String = String.Empty

        Dim travinfo As XmlNodeList = xmldoc.SelectNodes("/PoweredPNR_PNRReply/travellerInfo")
        For i As Integer = 0 To travinfo.Count - 1
            For ii As Integer = 0 To travinfo.Item(i).ChildNodes.Count - 1
                If travinfo.Item(i).ChildNodes(ii).Name = "elementManagementPassenger" Then
                    For pt As Integer = 0 To travinfo.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                        If travinfo.Item(i).ChildNodes(ii).ChildNodes(pt).Name = "reference" Then
                            For qulf As Integer = 0 To travinfo.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes.Count - 1
                                If travinfo.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(qulf).InnerText = "PT" Then
                                    ptno = travinfo.Item(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(qulf).NextSibling.InnerText
                                    If tktnoArraylist.Count > 0 Then
                                        For t As Integer = 0 To tktnoArraylist.Count - 1
                                            Dim typ As String = String.Empty
                                            Dim infindc As String = String.Empty
                                            Dim pttkt() As String = Split(tktnoArraylist(t), "::")
                                            If pttkt(0).ToString.Trim = ptno.ToString.Trim Then
                                                Dim travnm As XmlNode = travinfo.Item(i).ChildNodes(ii).NextSibling
                                                For trvnm As Integer = 0 To travnm.ChildNodes.Count - 1
                                                    If travnm.ChildNodes(trvnm).Name = "travellerInformation" Then
                                                        For trvnm1 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes.Count - 1
                                                            If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).Name = "traveller" Then
                                                                For trvnm2 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes.Count - 1
                                                                    If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "surname" Then
                                                                        srnm = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                                    End If
                                                                Next
                                                            ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).Name = "passenger" Then
                                                                For trvnm2 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes.Count - 1
                                                                    If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "firstName" Then
                                                                        fstnm = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                                    ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "type" Then
                                                                        typ = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                                    ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "infantIndicator" Then
                                                                        infindc = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                                    End If
                                                                Next
                                                                Dim tktarray() As String = Split(pttkt(1), " ")
                                                                If typ = tktarray(0) Then
                                                                    tktnoArraylist1.Add(pttkt(0) & "::" & typ & "::" & srnm & "::" & fstnm & "::" & tktarray(1))
                                                                ElseIf (typ = "" Or typ = String.Empty) And infindc <> "" And tktarray(0) <> "INF" Then
                                                                    tktnoArraylist1.Add(pttkt(0) & "::" & "ADT" & "::" & srnm & "::" & fstnm & "::" & tktarray(1))
                                                                ElseIf (typ = "" Or typ = String.Empty) And infindc = "" And tktarray(0) <> "INF" Then
                                                                    tktnoArraylist1.Add(pttkt(0) & "::" & "NA" & "::" & srnm & "::" & fstnm & "::" & tktarray(1))
                                                                ElseIf (typ = "ADT" Or typ = "CHD") And tktarray(0) = "PAX" Then
                                                                    tktnoArraylist1.Add(pttkt(0) & "::" & typ & "::" & srnm & "::" & fstnm & "::" & tktarray(1))
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    Else
                                        Dim typ As String = String.Empty
                                        Dim infindc As String = String.Empty
                                        Dim travnm As XmlNode = travinfo.Item(i).ChildNodes(ii).NextSibling
                                        For trvnm As Integer = 0 To travnm.ChildNodes.Count - 1
                                            If travnm.ChildNodes(trvnm).Name = "travellerInformation" Then
                                                For trvnm1 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes.Count - 1
                                                    If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).Name = "traveller" Then
                                                        For trvnm2 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes.Count - 1
                                                            If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "surname" Then
                                                                srnm = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                            End If
                                                        Next
                                                    ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).Name = "passenger" Then
                                                        For trvnm2 As Integer = 0 To travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes.Count - 1
                                                            If travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "firstName" Then
                                                                fstnm = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                            ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "type" Then
                                                                typ = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                            ElseIf travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).Name = "infantIndicator" Then
                                                                infindc = travnm.ChildNodes(trvnm).ChildNodes(trvnm1).ChildNodes(trvnm2).InnerText
                                                            End If
                                                        Next
                                                        tktnoArraylist1.Add(ptno & "::" & typ & "::" & srnm & "::" & fstnm & "::" & "NOT TICKETED")
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If


                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Next
        '***************** Ticket No. with Passenger Details End******************

        '***************** OriginDestinationDetails Details Start******************
        Dim originDestination As New Hashtable
        Dim OrgDest As XmlNodeList = xmldoc.SelectNodes("/PoweredPNR_PNRReply/originDestinationDetails/itineraryInfo")
        For i As Integer = 0 To OrgDest.Count - 1
            Try
                Dim itineraryInfo As New Hashtable
                For ii As Integer = 0 To OrgDest.Item(i).ChildNodes.Count - 1
                    If OrgDest.Item(i).ChildNodes(ii).Name = "travelProduct" Then
                        For tp As Integer = 0 To OrgDest.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                            If OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).Name = "product" Then
                                For tp1 As Integer = 0 To OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes.Count - 1
                                    If OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).Name = "depDate" Then
                                        itineraryInfo.Add("depDate", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).InnerText)
                                    ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).Name = "depTime" Then
                                        itineraryInfo.Add("depTime", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).InnerText)
                                    ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).Name = "arrDate" Then
                                        itineraryInfo.Add("arrDate", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).InnerText)
                                    ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).Name = "arrTime" Then
                                        itineraryInfo.Add("arrTime", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).ChildNodes(tp1).InnerText)
                                    End If
                                Next
                            ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).Name = "boardpointDetail" Then
                                itineraryInfo.Add("boardpointDetail", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).InnerText)
                            ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).Name = "offpointDetail" Then
                                itineraryInfo.Add("offpointDetail", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).InnerText)
                            ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).Name = "companyDetail" Then
                                itineraryInfo.Add("companyDetail", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).InnerText)
                            ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).Name = "productDetails" Then
                                itineraryInfo.Add("productDetails", OrgDest.Item(i).ChildNodes(ii).ChildNodes(tp).InnerText)
                            End If
                        Next
                    ElseIf OrgDest.Item(i).ChildNodes(ii).Name = "itineraryReservationInfo" Then
                        For ir As Integer = 0 To OrgDest.Item(i).ChildNodes(ii).ChildNodes.Count - 1
                            If OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).Name = "reservation" Then
                                For ir1 As Integer = 0 To OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).ChildNodes.Count - 1
                                    If OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).ChildNodes(ir1).Name = "companyId" Then
                                        itineraryInfo.Add("companyId", OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).ChildNodes(ir1).InnerText)
                                    ElseIf OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).ChildNodes(ir1).Name = "controlNumber" Then
                                        itineraryInfo.Add("controlNumber", OrgDest.Item(i).ChildNodes(ii).ChildNodes(ir).ChildNodes(ir1).InnerText)
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
                originDestination.Add(i, itineraryInfo)
            Catch ex As Exception
            End Try
        Next
        '***************** OriginDestinationDetails Details End******************

        '***********All Details Start***********
        Dim Tkt_OrgDest_Details As New Hashtable
        Tkt_OrgDest_Details.Add("originDestination", originDestination)
        Tkt_OrgDest_Details.Add("TktNoArrayList", tktnoArraylist1)
        '***********All Details End***********

        Return Tkt_OrgDest_Details
    End Function

    Public Function TST_Read(ByVal Fare_Info As String, ByVal ChdPt As String) As ArrayList
        Dim Fare_Reader As New XmlDocument()
        Dim FareList As String = Nothing
        Dim TaxList As String = Nothing
        Dim cpt As String = ""
        Dim FareArray As New ArrayList
        Dim FareArray1 As New ArrayList
        Dim paxref As String = ""
        Dim Pax As String = ""
        If InStr(Fare_Info, "<errorFreeText1>") Then

        Else
            Fare_Reader.LoadXml(Fare_Info)
            Dim Fare_Node As XmlNodeList = Fare_Reader.GetElementsByTagName("fareList")
            For i As Integer = 0 To Fare_Node.Count - 1
                Dim uqlfr As String = ""
                For ii As Integer = 0 To Fare_Node(i).ChildNodes.Count - 1
                    If Fare_Node(i).ChildNodes(ii).Name = "fareReference" Then
                        For iii As Integer = 0 To Fare_Node(i).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare_Node(i).ChildNodes(ii).ChildNodes(iii).Name = "uniqueReference" Then
                                uqlfr = Fare_Node(i).ChildNodes(ii).ChildNodes(iii).InnerText
                            End If
                        Next
                    ElseIf Fare_Node(i).ChildNodes(ii).Name = "fareDataInformation" Then
                        For iii As Integer = 0 To Fare_Node(i).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare_Node(i).ChildNodes(ii).ChildNodes(iii).Name = "fareDataSupInformation" Then
                                For FR As Integer = 0 To Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes.Count - 1
                                    If Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).Name = "fareDataQualifier" Then
                                        If Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).InnerText = "B" Then
                                            ' FareList = FareList & "BasicFare;"
                                        ElseIf Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).InnerText = "712" Then
                                            FareList = FareList & "TotalFare;"
                                        ElseIf Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).InnerText = "TFT" Then
                                            'FareList = FareList & "GrandTotalFare;"
                                        End If
                                    ElseIf Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).Name = "fareAmount" Then
                                        If FareList = "TotalFare;" Then
                                            FareList = Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).InnerText '& ";"

                                        End If
                                    ElseIf Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).Name = "fareCurrency" Then
                                        '  FareList = FareList & Fare_Node(i).ChildNodes(ii).ChildNodes(iii).ChildNodes(FR).InnerText & "::"
                                    End If
                                Next
                            End If
                        Next
                    ElseIf Fare_Node(i).ChildNodes(ii).Name = "paxSegReference" Then
                        paxref = ""
                        For pt As Integer = 0 To Fare_Node(i).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare_Node(i).ChildNodes(ii).ChildNodes(pt).Name = "refDetails" Then
                                For rfc As Integer = 0 To Fare_Node(i).ChildNodes(ii).ChildNodes(pt).ChildNodes.Count - 1
                                    If Fare_Node(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(rfc).Name = "refNumber" Then
                                        cpt = Fare_Node(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(rfc).InnerText
                                        If pt = Fare_Node(i).ChildNodes(ii).ChildNodes.Count - 1 Then
                                            paxref = cpt 'paxref & "PaxRefNo" & cpt
                                        Else
                                            paxref = cpt 'paxref & "PaxRefNo" & cpt & "/"
                                        End If
                                    ElseIf Fare_Node(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(rfc).Name = "refQualifier" Then
                                        Pax = Fare_Node(i).ChildNodes(ii).ChildNodes(pt).ChildNodes(rfc).InnerText
                                    End If
                                Next
                                FareArray1.Add(Pax & "/" & paxref)
                            End If
                        Next
                    End If
                Next
                For ii As Integer = 0 To FareArray1.Count - 1
                    FareArray.Add(uqlfr & "Q" & "#" & FareList & "#" & FareArray1(ii) & "#" & i.ToString)
                Next
                FareArray1.Clear()
                FareList = ""
            Next
        End If
        Return FareArray
    End Function

    Private Function PNR_TST(ByVal Adult, ByVal Child, ByVal Infant, ByVal adttstqlf, ByVal chdtstqlf, ByVal inftstqlf)
        Dim tkt As String = ""
        If (Val(Adult) > 0 And Val(Child) = 0 And Val(Infant) = 0) Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & adttstqlf & "</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) > 0) And Val(Infant) = 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & adttstqlf & "</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & chdtstqlf & "</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) > 0) And Val(Infant) > 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & adttstqlf & "</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & chdtstqlf & "</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & inftstqlf & "</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        ElseIf (Val(Adult) > 0 And Val(Child) = 0) And Val(Infant) > 0 Then
            tkt = "<PoweredTicket_CreateTSTFromPricing><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & adttstqlf & "</uniqueReference></itemReference></psaList><psaList><itemReference><referenceType>TST</referenceType><uniqueReference>" & inftstqlf & "</uniqueReference></itemReference></psaList></PoweredTicket_CreateTSTFromPricing>"
        End If
        Return tkt
    End Function

    Public Function getFareListID(ByVal fare As String, ByVal Adt As Integer, ByVal Chd As Integer, ByVal Inf As Integer, ByVal AdtFare As String, ByVal ChdFare As String, ByVal InfFare As String, ByVal airline As String) As Hashtable
        Dim adtfarelst As String = ""
        Dim chdfarelst As String = ""
        Dim inffarelst As String = ""
        Dim fare1 As ArrayList
        Dim tkt As Hashtable
        Dim flid As New Hashtable
        Dim farepaxxml() As String = Split(fare, "PriceAndAddPax")
        fare1 = TST_Read(farepaxxml(0), "")
        tkt = tktdetails(farepaxxml(1))
        Dim admin_mrk As String = ""
        Dim agent_mrk As String = ""
        Dim perpax_mrk As String = ""
        Dim totnoofpax As Integer = Adt + Chd
        Try
            admin_mrk = airmrkup.AirlineMarkUp("ADMIN", airline.ToString.Trim, totnoofpax, System.Web.HttpContext.Current.Session("UID").ToString.Trim)
            agent_mrk = airmrkup.AirlineMarkUp("AGENT", airline.ToString.Trim, totnoofpax, System.Web.HttpContext.Current.Session("UID").ToString.Trim)
            perpax_mrk = (((Val(admin_mrk) + Val(agent_mrk)) / totnoofpax)).ToString

        Catch ex As Exception
        End Try

        Dim tktno As ArrayList = tkt("TktNoArrayList")
        Try
            For i As Integer = 0 To tktno.Count - 1
                Dim tktsplit() As String = Split(tktno(i), "::")
                For ii As Integer = 0 To fare1.Count - 1
                    Dim splitfare() As String = Split(fare1(ii), "#")
                    If Val(Adt) > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "ADT" And splitfare(2).Split("/")(0) = "PA" Then
                            If Val(splitfare(1)) = ((Val(AdtFare) / Adt) - Val(perpax_mrk)) Then
                                adtfarelst = Right(fare1(ii), 1)
                            End If
                        End If
                    End If
                    If Val(Chd) > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "CHD" And splitfare(2).Split("/")(0) = "PA" Then
                            If Val(splitfare(1)) = ((Val(ChdFare) / Chd) - Val(perpax_mrk)) Then
                                chdfarelst = Right(fare1(ii), 1)
                            End If
                        End If
                    End If
                    If Val(Inf) > 0 Then
                        If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "INF" And splitfare(2).Split("/")(0) = "PI" Then
                            If Val(splitfare(1)) = (Val(InfFare) / Inf) Then
                                inffarelst = Right(fare1(ii), 1)
                            End If
                        End If
                    End If
                Next
            Next

            flid.Add("adtfarelst", adtfarelst)
            flid.Add("chdfarelst", chdfarelst)
            flid.Add("inffarelst", inffarelst)

        Catch ex As Exception
        End Try
        Return flid
    End Function

    
End Class
