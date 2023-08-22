Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Data

Public Class clsOnlineTicketing

    Public Function OnLineTicketing(ByVal airline_pnr As String, ByVal Amd_Pnr As String, ByVal c_Corporate_Id As String, ByVal c_Username As String, ByVal c_Password As String, ByVal VC As String) As ArrayList
        'Dim Factory1 As New APIV2_COMLib.FactoryAPI()
        'Dim Conversation1 As New APIV2_COMLib.AmadeusAPI()
        'Dim OpenType1 As New APIV2_COMLib.OpenType()
        'Dim c_Connection1 As New APIV2_COMLib.ConnectionType()
        Dim Tckt_No As New ArrayList()
        Dim rc As Boolean = False
        'Try
        '    Dim sw As StringWriter = New StringWriter()
        '    Dim xw As XmlTextWriter = New XmlTextWriter(sw)
        '    Dim i As Integer
        '    Dim e_tct_req, e_tct_res, ticket_Req, ticket_Res, ig_trq, ig_trs, tkt_seg(), tct_seg(), result, rtr1, rtr_res1 As String
        '    Dim tckt As New System.Xml.XmlDocument
        '    If airline_pnr <> "" Then
        '        '*** Wait for 5 sec*************
        '        Dim tm As DateTime = DateTime.Now.AddSeconds(5)
        '        While DateTime.Now < tm
        '            '------Do nothing-------- 
        '        End While
        '        '*** Wait for 5 sec*************
        '        If c_Corporate_Id.Trim <> "NA" And c_Username.Trim <> "NA" And c_Password.Trim <> "NA" Then
        '            rc = Factory1.createConversationFactory("apiv2.amadeus.net", 20002, c_Corporate_Id, c_Username, c_Password)
        '            'rc = Factory1.createConversationFactory("195.27.163.89", 20002, c_Corporate_Id, c_Username, c_Password)
        '            If rc Then
        '                rc = Factory1.openConversationFromFactory(OpenType1, Conversation1)
        '                rtr1 = "<Cryptic_GetScreen_Query><Command>RT" & Amd_Pnr & "</Command></Cryptic_GetScreen_Query>"
        '                'rtr1 = "<Cryptic_GetScreen_Query><Command>PV</Command></Cryptic_GetScreen_Query>"
        '                rtr_res1 = Conversation1.SendAndReceiveAsXml(rtr1)
        '                If Left(VC, 2).ToString.Trim.ToUpper = "IT" Then
        '                    e_tct_req = "<Cryptic_GetScreen_Query><Command>TTP/T-IT</Command></Cryptic_GetScreen_Query>"
        '                Else
        '                    e_tct_req = "<Cryptic_GetScreen_Query><Command>TTP/ET</Command></Cryptic_GetScreen_Query>"
        '                End If
        '                e_tct_res = Conversation1.SendAndReceiveAsXml(e_tct_req)
        '                tckt.LoadXml(e_tct_res)
        '                tckt.WriteTo(xw)
        '                result = sw.ToString
        '                Dim seg_info = Split(result, "<Response>")
        '                Dim position = InStr(1, seg_info(1), "OK ETICKET")
        '                If position <> 0 Then
        '                    rtr1 = "<Cryptic_GetScreen_Query><Command>RT" & Amd_Pnr & "</Command></Cryptic_GetScreen_Query>"
        '                    rtr_res1 = Conversation1.SendAndReceiveAsXml(rtr1)
        '                    ticket_Req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>0</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        '                    ticket_Res = Conversation1.SendAndReceiveAsXml(ticket_Req)
        '                    tkt_seg = Split(ticket_Res, "<segmentName>FA</segmentName>")
        '                    Try
        '                        If UBound(tkt_seg) > 0 Then
        '                            Tckt_No = TktNoWithPax(ticket_Res)
        '                            ig_trq = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        '                            ig_trs = Conversation1.SendAndReceiveAsXml(ig_trq)
        '                        Else
        '                            For i = 1 To 3
        '                                ig_trq = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        '                                ig_trs = Conversation1.SendAndReceiveAsXml(ig_trq)
        '                                Dim dt As DateTime = DateTime.Now.AddSeconds(5)
        '                                While DateTime.Now < dt
        '                                    '------Do nothing-------- 
        '                                End While
        '                                rtr1 = "<Cryptic_GetScreen_Query><Command>RT" & Amd_Pnr & "</Command></Cryptic_GetScreen_Query>"
        '                                rtr_res1 = Conversation1.SendAndReceiveAsXml(rtr1)
        '                                ticket_Req = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>0</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        '                                ticket_Res = Conversation1.SendAndReceiveAsXml(ticket_Req)
        '                                tkt_seg = Split(ticket_Res, "<segmentName>FA</segmentName>")
        '                                Dim ii As Integer
        '                                If UBound(tkt_seg) > 0 Then
        '                                    Tckt_No = TktNoWithPax(ticket_Res)
        '                                    ig_trq = "<PoweredPNR_AddMultiElements><pnrActions><optionCode>20</optionCode></pnrActions></PoweredPNR_AddMultiElements>"
        '                                    ig_trs = Conversation1.SendAndReceiveAsXml(ig_trq)
        '                                    Exit For
        '                                ElseIf (i = 3) Then
        '                                    Tckt_No.Add("Ok,E-Ticket,But Airline is not able to Print ticket No." & "!" & "NO")
        '                                End If
        '                            Next
        '                        End If
        '                    Catch ex As Exception
        '                        Tckt_No.Add("AirLine Error")
        '                    End Try
        '                Else
        '                    Tckt_No.Add("AirLine is not able to make ETicket,Please Issue Ticket offline" & "!" & "NO")
        '                End If
        '            Else
        '                Tckt_No.Add("AirLine Server Connection Problem,Please Issue Ticket offline" & "!" & "NO")
        '            End If
        '        Else
        '            Tckt_No.Add("AirLine Server Connection Problem,Please Issue Ticket offline" & "!" & "NO")
        '        End If
        '    Else
        '        Tckt_No.Add("AirLine is not able to make ETicket,Please Issue Ticket offline" & "!" & "NO")
        '    End If
        '    Dim clos = Factory1.closeConversationFromFactory(Conversation1)
        '    Return Tckt_No
        'Catch ex As Exception
        '    Tckt_No.Add("AirLine Error")
        '    Return Tckt_No
        'End Try
        Tckt_No.Add("AirLine Error")
        Return Tckt_No
    End Function

    Private Function TktNoWithPax(ByVal pnrreply As String) As ArrayList
        Dim PaxArrayList As New ArrayList
        Dim tktno As ArrayList = tktdetails(pnrreply)
        Try
            Dim chdpt As String = ""
            For i As Integer = 0 To tktno.Count - 1
                Dim tktsplit() As String = Split(tktno(i), "::")
                If tktsplit(1) = "CHD" Then
                    chdpt = chdpt & tktsplit(0) & "/"
                End If

                Dim fnm() As String = Split(tktsplit(3), " ")
                Dim firstnm As String = ""
                If tktsplit(1) = "INF" Then
                    firstnm = tktsplit(3) & " " & tktsplit(2)
                Else
                    If fnm.Length = 2 Then
                        firstnm = fnm(0).ToString.Trim
                    Else
                        For f As Integer = 0 To fnm.Length - 2
                            firstnm = firstnm & fnm(f) & " "
                        Next
                    End If
                    firstnm = fnm(fnm.Length - 1) & " " & firstnm & " " & tktsplit(2)
                End If
                PaxArrayList.Add(firstnm & "/" & tktsplit(4)) ' & "/" & tktsplit(1) & "/" & "PaxRefNo" & tktsplit(0)
            Next
        Catch ex As Exception
        End Try
        Return PaxArrayList
    End Function

    Private Function tktdetails(ByVal xmlstr As String) As ArrayList
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


        Return tktnoArraylist1
    End Function


End Class
