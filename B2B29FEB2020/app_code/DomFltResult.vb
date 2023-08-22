Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Xml
Imports System.Threading

Public Class DomFltResult
    ' Dim objLccAvail As New LccAvailability
    ' Dim objMC As New MC
    Dim objSql As New SqlTransactionNew
    Dim Origin As String, Destination As String, Depdate As String, retDate As String, DepdateLcc As String, retDateLcc As String
    Dim Adult As String, Child As String, Infant As String
    Dim Cabin As String, Airline As String, NStop As String, totPax As String
    Dim SearchValue As String = "", AgentID As String, GoGuid As String
    Dim TripType As String, tCnt As Integer, Ft As String, TrackId As String
    Dim GdsDt As DataTable
    Dim IndigoDt, SpicejetDt, GoairDt, OfflineDt, LCCCpnDt, LCCCpnDtSG, LCCCpnDtIX, LCCCpnDtG8 As DataTable
    Public Function GetResults(ByVal SearchQuery As Hashtable) As DataTable

        Dim OrgDestDate() As String
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
            tCnt = SearchQuery("tCnt")
            Adult = SearchQuery("Adult")
            Child = SearchQuery("Child")
            Infant = SearchQuery("Infant")
            Cabin = SearchQuery("Cabin")
            Ft = SearchQuery("FType")
            TrackId = SearchQuery("TrackId")
            If SearchQuery("Airline") = "" Then
                Airline = ""
            Else
                Airline = Split(SearchQuery("hidAirline"), ",")(1)
            End If
            NStop = SearchQuery("NStop")
            totPax = (Val(Adult) + Val(Child)).ToString
            SearchValue = SearchQuery("SearchValue")
            AgentID = SearchQuery("AgentId")
            Dim ResponseDt As New DataTable
            Dim vlist1 As New ArrayList
            Dim vlist As New ArrayList
            Try
                If Airline = "GDS" Or Airline = "S2" Or Airline = "9W" Or Airline = "AI" Or Airline = "IT" Or Airline = "" Then
                    Dim th As New Thread(New ParameterizedThreadStart(AddressOf GDSAvibilibity))
                    th.Start("1A")
                    vlist.Add(th)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "6E" Or Airline = "" Then
                    Dim th2 As New Thread(New ParameterizedThreadStart(AddressOf IndigoAvibilibity))
                    th2.Start("6E")
                    vlist.Add(th2)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "SG" Or Airline = "" Then
                    Dim th4 As New Thread(New ParameterizedThreadStart(AddressOf SpiceJetAvibilibity))
                    th4.Start("SG")
                    vlist.Add(th4)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "G8" Or Airline = "" Then
                    Dim th1 As New Thread(New ParameterizedThreadStart(AddressOf GoAirAvibilibity))
                    th1.Start("G8")
                    vlist.Add(th1)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "6E" Or Airline = "" Then
                    Dim th3 As New Thread(New ParameterizedThreadStart(AddressOf LccCpnAvibilibity))
                    th3.Start("6E")
                    vlist.Add(th3)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "SG" Or Airline = "" Then
                    Dim th5 As New Thread(New ParameterizedThreadStart(AddressOf SGCpnAvibilibity))
                    th5.Start("SG")
                    vlist.Add(th5)
                    vlist1.Add(Now)
                End If
                If Airline = "LCC" Or Airline = "IX" Or Airline = "" Then
                    If Infant = 0 Then
                        Dim th6 As New Thread(New ParameterizedThreadStart(AddressOf IXCpnAvibilibity))
                        th6.Start("IX")
                        vlist.Add(th6)
                        vlist1.Add(Now)
                    End If
                End If
                If Airline = "LCC" Or Airline = "G8" Or Airline = "" Then
                    Dim th7 As New Thread(New ParameterizedThreadStart(AddressOf GoAirAvibilibityCpn))
                    th7.Start("G8")
                    vlist.Add(th7)
                    vlist1.Add(Now)
                End If

                Dim counter = 0
                While (counter < vlist.Count)
                    Dim TH As Thread = CType(vlist(counter), Thread)
                    If (TH.ThreadState = ThreadState.WaitSleepJoin) Then
                        Dim DIFF = (DateDiff(DateInterval.Second, vlist1(counter), Now))
                        If (DIFF > 75) Then
                            TH.Abort()
                            counter += 1
                        End If
                    ElseIf (TH.ThreadState = ThreadState.Stopped) Then
                        counter += 1
                    ElseIf (TH.IsAlive) Then
                        counter = 0
                    End If
                End While
            Catch ex As Exception

            End Try
            'GoAirAvibilibityCpn("G8CPN")
            If Infant = "0" Then
                OfflineAvibilibity("OffLine")
            End If
            ResponseDt = MrgResult()
            Return ResponseDt
        Catch ex As Exception
        End Try
    End Function

    Private Sub GDSAvibilibity(ByVal Provider As Object)

        Dim dsCrd As DataSet = objSql.GetCredentials(Provider, "", "D")
    End Sub

    Private Sub IndigoAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim obj6E As New clsIndigo(dsCrd2.Tables(0).Rows(0)("UserID"), dsCrd2.Tables(0).Rows(0)("APISource"), dsCrd2.Tables(0).Rows(0)("Password"), dsCrd2.Tables(0).Rows(0)("CarrierAcc"), "Indigo", dsCrd2.Tables(0).Rows(0)("ServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            IndigoDt = obj6E.getAvailabilty("D", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, dsCrd2.Tables(0).Rows(0)("InfBasic"), dsCrd2.Tables(0).Rows(0)("InfTax"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub SpiceJetAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd3 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim objSG As New clsSpiceJet(dsCrd3.Tables(0).Rows(0)("UserID"), dsCrd3.Tables(0).Rows(0)("APISource"), dsCrd3.Tables(0).Rows(0)("Password"), dsCrd3.Tables(0).Rows(0)("CarrierAcc"), "SpiceJet", dsCrd3.Tables(0).Rows(0)("ServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            SpicejetDt = objSG.getAvailabilty("D", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, dsCrd3.Tables(0).Rows(0)("InfBasic"), dsCrd3.Tables(0).Rows(0)("InfTax"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub GoAirAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd4 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim objGoair As New clsGoAir(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, "GoAir")
            GoGuid = objGoair.GetSecurityGUID(dsCrd4.Tables(0).Rows(0)("LoginID"), dsCrd4.Tables(0).Rows(0)("LoginPwd"))
            If objGoair.ValidateGUID(GoGuid) Then
                Dim gCabin As String = "" '"ECONOMY"
                If Cabin = "Y" Then
                    gCabin = "ECONOMY"
                ElseIf Cabin = "C" Then
                    gCabin = "BUSINESS"
                ElseIf Cabin = "F" Then
                    gCabin = "BUSINESS"
                End If
                GoairDt = objGoair.GetAvailability(GoGuid, dsCrd4.Tables(0).Rows(0)("CorporateID"), dsCrd4.Tables(0).Rows(0)("UserID"), dsCrd4.Tables(0).Rows(0)("Password"), "D", "rdbOneWay", Origin, Destination, DepdateLcc, "1", Adult, Child, Infant, gCabin, dsCrd4.Tables(0).Rows(0)("ServerIP"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GoAirAvibilibityCpn(ByVal Provider As Object)
       
        Try
            Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim objLccCpn As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            LCCCpnDtG8 = objLccCpn.GetLccCpnResultGO("D", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, dsCrd2.Tables(0).Rows(0)("InfBasic"), dsCrd2.Tables(0).Rows(0)("InfTax"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId, "G8")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub OfflineAvibilibity(ByVal Provider As Object)
        Try

            Dim objOL As New clsOfflineAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            OfflineDt = objOL.OfflineAvailability(Origin, Destination, Depdate, Airline, "D", Adult, Child, Infant, AgentID, "SPRING", "rdbOneWay", tCnt, Ft, TrackId)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub LccCpnAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim objLccCpn As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            LCCCpnDt = objLccCpn.GetLccCpnResult("D", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, dsCrd2.Tables(0).Rows(0)("InfBasic"), dsCrd2.Tables(0).Rows(0)("InfTax"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId, "6E")
        Catch ex As Exception
        End Try

    End Sub
    Private Sub SGCpnAvibilibity(ByVal Provider As Object)
        Try
            Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider, "", "D")
            Dim objLccCpn As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            LCCCpnDtSG = objLccCpn.GetLccCpnResultSG("D", "rdbOneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, dsCrd2.Tables(0).Rows(0)("InfBasic"), dsCrd2.Tables(0).Rows(0)("InfTax"), AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId, "SG")
        Catch ex As Exception
        End Try

    End Sub
    Private Sub IXCpnAvibilibity(ByVal Provider As Object)
        Try
            'Dim dsCrd2 As DataSet = objSql.GetCredentials(Provider)
            Dim objLccCpn As New clsLccCpnAvailability(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            LCCCpnDtIX = objLccCpn.GetLccCpnResultIX("D", "OneWay", Origin, Destination, DepdateLcc, retDateLcc, Adult, Child, Infant, 0, 0, AgentID, "SPRING", SearchValue, tCnt, Ft, TrackId, "IX")
        Catch ex As Exception
        End Try

    End Sub
    Private Function MrgResult() As DataTable
        Dim AirDataTable As New DataTable
        Dim Lt_No As Integer = 0
        Try
            If GdsDt IsNot Nothing Then
                If GdsDt.Rows.Count > 0 Then
                    AirDataTable.Merge(GdsDt, False, MissingSchemaAction.Add)
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
            If SpicejetDt IsNot Nothing Then
                If SpicejetDt.Rows.Count > 0 Then
                    For i As Integer = 0 To SpicejetDt.Rows.Count - 1
                        SpicejetDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(SpicejetDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(SpicejetDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = SpicejetDt
                    End If
                    Lt_No = SpicejetDt.Rows(SpicejetDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If

        Catch ex As Exception
        End Try
        Try
            If IndigoDt IsNot Nothing Then
                If IndigoDt.Rows.Count > 0 Then
                    For i As Integer = 0 To IndigoDt.Rows.Count - 1
                        IndigoDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(IndigoDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(IndigoDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = IndigoDt
                    End If
                    Lt_No = IndigoDt.Rows(IndigoDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If

        Catch ex As Exception
        End Try
        Try
            If GoairDt IsNot Nothing Then
                If GoairDt.Rows.Count > 0 Then
                    For i As Integer = 0 To GoairDt.Rows.Count - 1
                        GoairDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(GoairDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(GoairDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = GoairDt
                    End If
                    Lt_No = GoairDt.Rows(GoairDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try

        Try
            If OfflineDt IsNot Nothing Then
                If OfflineDt.Rows.Count > 0 Then
                    For i As Integer = 0 To OfflineDt.Rows.Count - 1
                        OfflineDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(OfflineDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(OfflineDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = OfflineDt
                    End If
                    Lt_No = OfflineDt.Rows(OfflineDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If LCCCpnDt IsNot Nothing Then
                If LCCCpnDt.Rows.Count > 0 Then
                    For i As Integer = 0 To LCCCpnDt.Rows.Count - 1
                        LCCCpnDt.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(LCCCpnDt.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(LCCCpnDt, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = LCCCpnDt
                    End If
                    Lt_No = LCCCpnDt.Rows(LCCCpnDt.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try

        Try
            If LCCCpnDtSG IsNot Nothing Then
                If LCCCpnDtSG.Rows.Count > 0 Then
                    For i As Integer = 0 To LCCCpnDtSG.Rows.Count - 1
                        LCCCpnDtSG.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(LCCCpnDtSG.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(LCCCpnDtSG, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = LCCCpnDtSG
                    End If
                    Lt_No = LCCCpnDtSG.Rows(LCCCpnDtSG.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If LCCCpnDtIX IsNot Nothing Then
                If LCCCpnDtIX.Rows.Count > 0 Then
                    For i As Integer = 0 To LCCCpnDtIX.Rows.Count - 1
                        LCCCpnDtIX.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(LCCCpnDtIX.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(LCCCpnDtIX, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = LCCCpnDtIX
                    End If
                    Lt_No = LCCCpnDtIX.Rows(LCCCpnDtIX.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If LCCCpnDtG8 IsNot Nothing Then
                If LCCCpnDtG8.Rows.Count > 0 Then
                    For i As Integer = 0 To LCCCpnDtG8.Rows.Count - 1
                        LCCCpnDtG8.Rows(i)("LineItemNumber") = (Lt_No + Integer.Parse(LCCCpnDtG8.Rows(i)("LineItemNumber"))).ToString
                    Next
                    If AirDataTable IsNot Nothing And AirDataTable.Rows.Count > 0 Then
                        AirDataTable.Merge(LCCCpnDtG8, False, MissingSchemaAction.Add)
                    Else
                        AirDataTable = LCCCpnDtG8
                    End If
                    Lt_No = LCCCpnDtG8.Rows(LCCCpnDtG8.Rows.Count - 1)("LineItemNumber")
                End If
            End If
        Catch ex As Exception
        End Try
        Return AirDataTable
    End Function
End Class
