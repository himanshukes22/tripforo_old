Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Threading
Public Class IntFltResult
    Dim objSql As New SqlTransactionNew
    Dim IXDt, AKDt As DataTable
    Dim IntResponseDt, ResponseDt As New DataTable
    Dim TripType As String
    Dim OrgDestDate() As String
    Dim Origin As String, Destination As String, Depdate As String, retDate As String, DepdateLcc As String, retDateLcc As String
    Dim Adult As String, Child As String, Infant As String
    Dim Cabin As String, Airline As String, NStop As String, totPax As String, SearchValue As String, UID As String
    Dim IndigoDt, SpicejetDt, AirArabiaDt As DataTable
    Dim DirectSectorsForIndigo As New Hashtable
    Public Function GetResults(ByVal SearchQuery As Hashtable) As DataTable
        DirectSectorsForIndigo.Add("DXB", "DEL,BOM,HYD,MAA,COK")
        DirectSectorsForIndigo.Add("MCT", "BOM")
        DirectSectorsForIndigo.Add("KTM", "DEL")
        DirectSectorsForIndigo.Add("BKK", "DEL,BOM,CCU")
        DirectSectorsForIndigo.Add("SIN", "DEL,BOM")
        DirectSectorsForIndigo.Add("DEL", "DXB,SIN,BKK,KTM")
        DirectSectorsForIndigo.Add("BOM", "DXB,SIN,BKK,MCT")
        DirectSectorsForIndigo.Add("HYD", "DXB")
        DirectSectorsForIndigo.Add("MAA", "DXB")
        DirectSectorsForIndigo.Add("COK", "DXB")
        DirectSectorsForIndigo.Add("CCU", "BKK")

        totPax = (Val(Adult) + Val(Child)).ToString
        Try
            OrgDestDate = Split(SearchQuery("Origin"), "(")
            Origin = Left(OrgDestDate(1), 3)
            OrgDestDate = Split(SearchQuery("Destination"), "(")
            Destination = Left(OrgDestDate(1), 3)
            OrgDestDate = Split(SearchQuery("Depdate"), "/")
            Depdate = OrgDestDate(0) & OrgDestDate(1) & Right(OrgDestDate(2), 2)
            DepdateLcc = OrgDestDate(2) & "-" & OrgDestDate(1) & "-" & OrgDestDate(0)
            OrgDestDate = Split(SearchQuery("retDate"), "/")
            retDate = OrgDestDate(0) & OrgDestDate(1) & Right(OrgDestDate(2), 2)
            retDateLcc = OrgDestDate(2) & "-" & OrgDestDate(1) & "-" & OrgDestDate(0)
            TripType = SearchQuery("TripType")
            Adult = SearchQuery("Adult")
            Child = SearchQuery("Child")
            Infant = SearchQuery("Infant")
            Cabin = SearchQuery("Cabin")
            'Airline = SearchQuery("Airline")
           
            If SearchQuery("Airline") = "" Then
                Airline = ""
            Else
                Airline = Split(SearchQuery("hidAirline"), ",")(1)
            End If
            NStop = SearchQuery("NStop")
            SearchValue = SearchQuery("SearchValue")
            UID = HttpContext.Current.Session("UID")
            Dim vlist1 As New ArrayList
            Dim vlist As New ArrayList

            If Airline <> "IX" And Airline <> "SG" And Airline <> "6E" And Airline <> "AK" And Airline <> "G9" Then
                Dim th As New Thread(New ParameterizedThreadStart(AddressOf GetGDSAvailability))
                th.Start("1A")
                vlist.Add(th)
                vlist1.Add(Now)
            End If
            If Airline = "LCC" Or Airline = "IX" Or Airline = "" Then
                Dim th1 As New Thread(New ParameterizedThreadStart(AddressOf GetIXAvailability))
                th1.Start("IX")
                vlist.Add(th1)
                vlist1.Add(Now)
            End If
            If Airline = "LCC" Or Airline = "AK" Or Airline = "" Then
                Dim th2 As New Thread(New ParameterizedThreadStart(AddressOf GetAirAsiaAvailability))
                th2.Start("AK")
                vlist.Add(th2)
                vlist1.Add(Now)
            End If
            If Airline = "LCC" Or Airline = "6E" Or Airline = "" Then
                If InStr(DirectSectorsForIndigo(Origin.Trim.ToUpper), Destination.Trim.ToUpper) > 0 Then
                    Dim th3 As New Thread(New ParameterizedThreadStart(AddressOf GetIndigoAvibilibity))
                    th3.Start("6E")
                    vlist.Add(th3)
                    vlist1.Add(Now)
                End If

            End If
            If Airline = "LCC" Or Airline = "SG" Or Airline = "" Then
                Dim th4 As New Thread(New ParameterizedThreadStart(AddressOf GetSpiceJetAvibilibity))
                th4.Start("SG")
                vlist.Add(th4)
                vlist1.Add(Now)
            End If
            If Airline = "LCC" Or Airline = "G9" Or Airline = "" Then
                Dim th5 As New Thread(New ParameterizedThreadStart(AddressOf GetAirArabiaAvailability))
                th5.Start("G9")
                vlist.Add(th5)
                vlist1.Add(Now)
            End If

            Dim counter = 0
            While (counter < vlist.Count)
                Dim TH As Thread = CType(vlist(counter), Thread)
                If (TH.ThreadState = ThreadState.WaitSleepJoin) Then
                    Dim DIFF = (DateDiff(DateInterval.Second, vlist1(counter), Now))
                    If (DIFF > 45) Then
                        TH.Abort()
                        counter += 1
                    End If
                ElseIf (TH.ThreadState = ThreadState.Stopped) Then
                    counter += 1
                ElseIf (TH.IsAlive) Then
                    counter = 0
                End If
            End While
            IntResponseDt = MrgResult()
            Return IntResponseDt
        Catch ex As Exception
        End Try
    End Function
    Private Sub GetGDSAvailability(ByVal Provider As Object)
        'Dim ObjMC As New MC
        'Dim dsCrd As DataSet = objSql.GetCredentials("1A")
        'If TripType = "rdbOneWay" Then
        '    ResponseDt = ObjMC.SendandReceiveXML(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("Port"), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), "INR", TripType, Origin, Destination, "", "", "", "", Adult, Child, Infant, totPax, Depdate, retDate, Cabin, "", "", NStop, "", SearchValue, "I", UID, "SPRING", 1, "", "")
        'Else
        '    ResponseDt = ObjMC.SendandReceiveXML(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("Port"), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), "INR", TripType, Origin, Destination, Destination, Origin, "", "", Adult, Child, Infant, totPax, Depdate, retDate, Cabin, "", "", NStop, "", SearchValue, "I", UID, "SPRING", 2, "", "")
        'End If
    End Sub
    Private Sub GetIXAvailability(ByVal Provider As Object)
        Dim objLccResult As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        If TripType = "rdbOneWay" Then
            If Infant = 0 Then
                IXDt = objLccResult.GetLccCpnResultIX("I", TripType.Replace("rdb", ""), Origin, Destination, DepdateLcc, retDateLcc, Adult.ToString, Child.ToString, Infant.ToString, 0, 0, UID, "SPRING", SearchValue, 1, "", "", "IX")
            End If
        Else
            If Infant = 0 Then
                IXDt = objLccResult.GetLccCpnResultIX("I", TripType.Replace("rdb", ""), Origin, Destination, DepdateLcc, retDateLcc, Adult.ToString, Child.ToString, Infant.ToString, 0, 0, UID, "SPRING", SearchValue, 2, "", "", "IX")
            End If
        End If
    End Sub
    Private Sub GetIndigoAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider, "", "I")
            Dim obj6E As New clsIndigo(dsCrd2.Tables(0).Rows(0)("UserID"), dsCrd2.Tables(0).Rows(0)("APISource"), dsCrd2.Tables(0).Rows(0)("Password"), dsCrd2.Tables(0).Rows(0)("CarrierAcc"), "Indigo", dsCrd2.Tables(0).Rows(0)("ServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            If TripType = "rdbOneWay" Then
                IndigoDt = obj6E.getAvailabilty("I", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, 1000, 1000, UID, "SPRING", SearchValue, 1, "", "")
            Else
                IndigoDt = obj6E.getAvailabilty("I", "Return", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, 1000, 1000, UID, "SPRING", SearchValue, 2, "", "")
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub GetSpiceJetAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd3 As DataSet = objSql.GetCredentials(Provider, "", "I")
            Dim objSG As New clsSpiceJet(dsCrd3.Tables(0).Rows(0)("UserID"), dsCrd3.Tables(0).Rows(0)("APISource"), dsCrd3.Tables(0).Rows(0)("Password"), dsCrd3.Tables(0).Rows(0)("CarrierAcc"), "SpiceJet", dsCrd3.Tables(0).Rows(0)("ServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            If TripType = "rdbOneWay" Then
                SpicejetDt = objSG.getAvailabilty("I", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, 1000, 1000, UID, "SPRING", SearchValue, 1, "", "")
            Else
                SpicejetDt = objSG.getAvailabilty("I", "Return", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, 1000, 1000, UID, "SPRING", SearchValue, 2, "", "")
            End If

        Catch ex As Exception
        End Try

    End Sub
    Private Sub GetAirAsiaAvailability(ByVal Provider As Object)
        Dim objLccResult As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        If TripType = "rdbOneWay" Then
            If Infant = 0 Then
                AKDt = objLccResult.GetLccCpnResultAirAsia("I", TripType.Replace("rdb", ""), Origin, Destination, DepdateLcc, retDateLcc, Adult.ToString, Child.ToString, Infant.ToString, 0, 0, UID, "SPRING", SearchValue, 1, "", "", "AK")
            End If
        Else
            If Infant = 0 Then
                AKDt = objLccResult.GetLccCpnResultAirAsia("I", TripType.Replace("rdb", ""), Origin, Destination, DepdateLcc, retDateLcc, Adult.ToString, Child.ToString, Infant.ToString, 0, 0, UID, "SPRING", SearchValue, 2, "", "", "AK")
            End If
        End If
    End Sub
    Private Sub GetAirArabiaAvailability(ByVal Provider As Object)
        Dim ObjMC As New AirArabia.GetFinalResult
        Dim dsCrd As DataSet = objSql.GetCredentials(Provider, "", "")
        If TripType = "rdbOneWay" Then
            '' AirArabiaDt = ObjMC.GetResponse(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, "OneWay", UID, "SPRING")
        Else
            ''AirArabiaDt = ObjMC.GetResponse(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, "Return", UID, "SPRING")
        End If
    End Sub
    Private Function MrgResult() As DataTable
        Dim AirDataTable As New DataTable
        Dim Lt_No As Integer = 0
        Try
            If ResponseDt IsNot Nothing Then
                If ResponseDt.Rows.Count > 0 Then
                    AirDataTable.Merge(ResponseDt, False, MissingSchemaAction.Add)
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If AirDataTable IsNot Nothing Then
                If AirDataTable.Rows.Count > 0 Then
                    Lt_No = AirDataTable.Rows(AirDataTable.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If IXDt IsNot Nothing Then
                If IXDt.Rows.Count > 0 Then
                    For i As Integer = 0 To IXDt.Rows.Count - 1
                        IXDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(IXDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(IXDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = IXDt
                    End If
                    Lt_No = IXDt.Rows(IXDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If

        Catch ex As Exception
        End Try
        Try
            If AKDt IsNot Nothing Then
                If AKDt.Rows.Count > 0 Then
                    For i As Integer = 0 To AKDt.Rows.Count - 1
                        AKDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(AKDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(AKDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = AKDt
                    End If
                    Lt_No = AKDt.Rows(AKDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If

        Catch ex As Exception
        End Try
        Try
            If SpicejetDt IsNot Nothing Then
                Dim SGDT As DataTable
                If TripType = "rdbOneWay" Then
                    SGDT = SpicejetDt
                Else
                    SGDT = LccResultFormatForIntl(SpicejetDt)
                End If
                If SGDT IsNot Nothing Then
                    If SGDT.Rows.Count > 0 Then
                        For i As Integer = 0 To SGDT.Rows.Count - 1
                            SGDT.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(SGDT.Rows(i)("LineItemNumber"))).ToString
                        Next
                        If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                            AirDataTable.Merge(SGDT, False, MissingSchemaAction.Add)
                        Else
                            AirDataTable = SGDT
                        End If
                        Lt_No = SGDT.Rows(SGDT.Rows.Count - 1)("LineItemNumber")
                    End If
                End If
            End If


        Catch ex As Exception
        End Try

        Try
            If IndigoDt IsNot Nothing Then
                Dim INDGDT As DataTable
                If TripType = "rdbOneWay" Then
                    INDGDT = IndigoDt
                Else
                    INDGDT = LccResultFormatForIntl(IndigoDt)
                End If
                If INDGDT IsNot Nothing Then
                    If INDGDT.Rows.Count > 0 Then
                        For i As Integer = 0 To INDGDT.Rows.Count - 1
                            INDGDT.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(INDGDT.Rows(i)("LineItemNumber"))).ToString
                        Next
                        If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                            AirDataTable.Merge(INDGDT, False, MissingSchemaAction.Add)
                        Else
                            AirDataTable = INDGDT
                        End If
                        Lt_No = INDGDT.Rows(INDGDT.Rows.Count - 1)("LineItemNumber")
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        Try
            If AirArabiaDt IsNot Nothing Then
                If AirArabiaDt.Rows.Count > 0 Then
                    For i As Integer = 0 To AirArabiaDt.Rows.Count - 1
                        AirArabiaDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(AirArabiaDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(AirArabiaDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = AirArabiaDt
                    End If
                    Lt_No = AirArabiaDt.Rows(AirArabiaDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Return AirDataTable
    End Function
    Private Function LccResultFormatForIntl(ByVal dt As DataTable) As DataTable
        Dim tblSourceO As DataTable = CType(dt, DataTable)
        Dim tblSourceR As DataTable = CType(dt, DataTable)
        tblSourceO.DefaultView.RowFilter = "TripType='O'"
        tblSourceO = tblSourceO.DefaultView.ToTable
        tblSourceR.DefaultView.RowFilter = "TripType='R'"
        tblSourceR = tblSourceR.DefaultView.ToTable
        Dim mergeDt As New DataTable
        If tblSourceO.Rows.Count > 0 And tblSourceR.Rows.Count > 0 Then
            Dim depDV As DataTable = tblSourceO.DefaultView.ToTable(True, "LineItemNumber")
            Dim retDV As DataTable = tblSourceR.DefaultView.ToTable(True, "LineItemNumber")
            Dim k = 0, l = 1
            mergeDt = tblSourceO.Clone
            For i As Integer = 0 To depDV.Rows.Count - 1
                Dim lccArrayO As Array = tblSourceO.Select("LineItemNumber='" & (i + 1).ToString & "'", "")
                For ii As Integer = 0 To retDV.Rows.Count - 1
                    Dim lccArrayR As Array = tblSourceR.Select("LineItemNumber='" & (ii + 1).ToString & "'", "")
                    For o As Integer = 0 To lccArrayO.Length - 1
                        mergeDt.ImportRow(lccArrayO(o))
                    Next
                    For o As Integer = 0 To lccArrayR.Length - 1
                        mergeDt.ImportRow(lccArrayR(o))
                    Next
                    For j = k To mergeDt.Rows.Count - 1
                        mergeDt.Rows(j)("LineItemNumber") = (l).ToString
                        mergeDt.Rows(j)("AdtFare") = (lccArrayO(0)("AdtFare")) + (lccArrayR(0)("AdtFare"))
                        mergeDt.Rows(j)("AdtBfare") = (lccArrayO(0)("AdtBfare")) + (lccArrayR(0)("AdtBfare"))
                        mergeDt.Rows(j)("AdtTax") = (lccArrayO(0)("AdtTax")) + (lccArrayR(0)("AdtTax"))
                        mergeDt.Rows(j)("ChdFare") = (lccArrayO(0)("ChdFare")) + (lccArrayR(0)("ChdFare"))
                        mergeDt.Rows(j)("ChdBfare") = (lccArrayO(0)("ChdBfare")) + (lccArrayR(0)("ChdBfare"))
                        mergeDt.Rows(j)("ChdTax") = (lccArrayO(0)("ChdTax")) + (lccArrayR(0)("ChdTax"))
                        mergeDt.Rows(j)("InfFare") = (Val((lccArrayO(0)("InfFare"))) + Val((lccArrayR(0)("InfFare")))).ToString
                        mergeDt.Rows(j)("infBfare") = (lccArrayO(0)("infBfare")) + (lccArrayR(0)("infBfare"))
                        mergeDt.Rows(j)("InfTax") = (lccArrayO(0)("InfTax")) + (lccArrayR(0)("InfTax"))
                        mergeDt.Rows(j)("TotalBfare") = (lccArrayO(0)("TotalBfare")) + (lccArrayR(0)("TotalBfare"))
                        mergeDt.Rows(j)("TotalTax") = (lccArrayO(0)("TotalTax")) + (lccArrayR(0)("TotalTax"))
                        mergeDt.Rows(j)("TotalFare") = (lccArrayO(0)("TotalFare")) + (lccArrayR(0)("TotalFare"))
                        mergeDt.Rows(j)("STax") = (lccArrayO(0)("STax")) + (lccArrayR(0)("STax"))
                        mergeDt.Rows(j)("TFee") = (lccArrayO(0)("TFee")) + (lccArrayR(0)("TFee"))
                        mergeDt.Rows(j)("DisCount") = (lccArrayO(0)("DisCount")) + (lccArrayR(0)("DisCount"))
                        mergeDt.Rows(j)("ADTAdminMrk") = (lccArrayO(0)("ADTAdminMrk")) + (lccArrayR(0)("ADTAdminMrk"))
                        mergeDt.Rows(j)("ADTAgentMrk") = (lccArrayO(0)("ADTAgentMrk")) + (lccArrayR(0)("ADTAgentMrk"))
                        mergeDt.Rows(j)("CHDAdminMrk") = (lccArrayO(0)("CHDAdminMrk")) + (lccArrayR(0)("CHDAdminMrk"))
                        mergeDt.Rows(j)("CHDAgentMrk") = (lccArrayO(0)("CHDAgentMrk")) + (lccArrayR(0)("CHDAgentMrk"))
                        mergeDt.Rows(j)("AdtFSur") = (lccArrayO(0)("AdtFSur")) + (lccArrayR(0)("AdtFSur"))
                        mergeDt.Rows(j)("ChdFSur") = (lccArrayO(0)("ChdFSur")) + (lccArrayR(0)("ChdFSur"))
                        mergeDt.Rows(j)("InfFSur") = (lccArrayO(0)("InfFSur")) + (lccArrayR(0)("InfFSur"))
                        mergeDt.Rows(j)("OriginalTF") = (lccArrayO(0)("OriginalTF")) + (lccArrayR(0)("OriginalTF"))
                        mergeDt.Rows(j)("OriginalTT") = (lccArrayO(0)("OriginalTT")) + (lccArrayR(0)("OriginalTT"))
                        mergeDt.Rows(j)("TotalFuelSur") = (lccArrayO(0)("TotalFuelSur")) + (lccArrayR(0)("TotalFuelSur"))
                    Next
                    'mergeDt.AcceptChanges()
                    l = l + 1
                    k = mergeDt.Rows.Count
                Next
            Next
        End If
        Return mergeDt
    End Function
End Class
