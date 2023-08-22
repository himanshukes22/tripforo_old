Imports Microsoft.VisualBasic
Imports System.Data
Imports LCC
Public Class clsBKGSPLFARE
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Public Function BookTicket(ByVal VC As String, ByVal dsCrd As DataSet, ByVal FltDs As DataSet, ByVal custInfo As Hashtable, ByRef flg As String) As DataTable
        Dim PnrDt As New DataTable
        PnrDt = checkAvailability(VC, dsCrd, FltDs, custInfo, flg)
        Return PnrDt
    End Function
    Private Function checkAvailability(ByVal VC As String, ByVal dsCrd As DataSet, ByVal FltDs As DataSet, ByVal custInfo As Hashtable, ByRef flg As String) As DataTable
        Dim objGoair As New LCC.clsGoAir("", "GoAir")
        Dim GoGuid As String = ""
        Dim GoairDt As DataTable
        Dim netfare As Double = 0
        Dim OriginalTF As Double = 0
        Dim airarray As Array
        Dim FltNumber As String = ""
        Dim FltNo1 As String = ""
        Dim FltNo2 As String = ""
        Dim strXml As String = ""
        Dim sMobile As String = "123456789" '
        Dim sContactType As String = "1"
        Dim sContactNum As String = "0"
        Dim strArray1() As String
        Dim dtotalAmount As Decimal = 0
        Dim PnrDt As New DataTable
        Try
            netfare = FltDs.Tables(0).Rows(0)("NetFare")
            OriginalTF = FltDs.Tables(0).Rows(0)("OriginalTF")
            'netfare += 100
            FltNumber = FltDs.Tables(0).Rows(0)("FlightIdentification")
            For i As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                FltNo1 = FltNo1 & FltDs.Tables(0).Rows(i)("FlightIdentification").ToString().Trim
            Next
            GoGuid = objGoair.GetSecurityGUID(dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"))
            If objGoair.ValidateGUID(GoGuid) Then
                Dim gCabin As String = "" '"ECONOMY"           
                GoairDt = objGoair.GetAvailability(GoGuid, dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), _
                                                   dsCrd.Tables(0).Rows(0)("Password"), "D", "rdbOneWay", FltDs.Tables(0).Rows(0)("OrgDestFrom").ToString().Trim(), _
                                                   FltDs.Tables(0).Rows(0)("OrgDestTo").ToString().Trim(), FltDs.Tables(0).Rows(0)("depdatelcc").ToString().Trim(), _
                                                   "1", FltDs.Tables(0).Rows(0)("Adult").ToString().Trim(), FltDs.Tables(0).Rows(0)("Child").ToString().Trim(), _
     FltDs.Tables(0).Rows(0)("Infant").ToString().Trim(), gCabin, dsCrd.Tables(0).Rows(0)("ServerIP"), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), "SPRING", "", 1, "OutBound", "")
                Try
                    Dim ds As New DataSet
                    ds.Tables.Add(GoairDt.Copy)
                    strXml = ds.GetXml
                    Try
                        If GoairDt.Rows.Count > 0 Then
                            Dim i As Integer = 0
                            Dim LNDT As New DataTable
                            LNDT = GoairDt.DefaultView.ToTable(True, "LineItemNumber")
                            For i = 0 To LNDT.Rows.Count - 1
                                airarray = GoairDt.Select("LineItemNumber='" & LNDT.Rows(i)("LineItemNumber") & "'", "")
                                If InStr((airarray(0)("sno")).ToString.ToUpper.Trim, "GOSPECIAL") > 0 Then
                                    FltNo2 = ""
                                    For j As Integer = 0 To airarray.Length - 1
                                        FltNo2 = FltNo2 & (airarray(j)("FlightIdentification")).ToString().Trim
                                    Next
                                    If FltNo1 = FltNo2 Then
                                        'If netfare > Convert.ToDouble((airarray(0)("OriginalTF")).ToString()) Then
                                        '    flg = "True"
                                        '    dtotalAmount = Convert.ToDecimal((airarray(0)("OriginalTF")).ToString())
                                        '    strArray1 = Split((airarray(0)("sno")), ":")
                                        'End If
                                        If VC = "G8" Then
                                            If netfare > Convert.ToDouble((airarray(0)("OriginalTF")).ToString()) Then
                                                flg = "True"
                                                dtotalAmount = Convert.ToDecimal((airarray(0)("OriginalTF")).ToString())
                                                strArray1 = Split((airarray(0)("sno")), ":")
                                            End If
                                        ElseIf VC = "G8CORP" Then
                                            If OriginalTF = Convert.ToDouble((airarray(0)("OriginalTF")).ToString()) Then
                                                flg = "True"
                                                dtotalAmount = Convert.ToDecimal((airarray(0)("OriginalTF")).ToString())
                                                strArray1 = Split((airarray(0)("sno")), ":")
                                            End If
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        insertLog(FltDs.Tables(0).Rows(0)("Track_id").ToString().Trim(), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), VC, strXml, "", flg)
                    Catch ex As Exception
                        insertLog(FltDs.Tables(0).Rows(0)("Track_id").ToString().Trim(), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), VC, strXml, ex.Message.Replace("'", ""), flg)
                    End Try
                Catch ex As Exception
                    insertLog(FltDs.Tables(0).Rows(0)("Track_id").ToString().Trim(), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), VC, strXml, ex.Message.Replace("'", ""), flg)
                End Try
            End If
            Try
                If flg = "True" Then
                    Dim objG8 As New clsGoAir(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, "GoAir")
                    PnrDt = objG8.GetBookingDetails(strArray1(1).ToString, custInfo("sAddName"), custInfo("sLine1") & ", " & custInfo("sLine2"), custInfo("sCity"), custInfo("sState"), _
                                                       custInfo("sZip"), custInfo("sCountry"), custInfo("sAgencyPhn"), custInfo("sCurrency"), custInfo("sCurrency"), _
                                                       custInfo("sEmailId"), custInfo("sFax"), sMobile, custInfo, sContactType, sContactNum, custInfo("Customeremail"), _
                                                       custInfo("sHomePhn"), custInfo("sAddName"), custInfo("sLine1") & ", " & custInfo("sLine2"), custInfo("sCity"), _
                                                       custInfo("sZip"), custInfo("sState"), custInfo("sCountry"), strArray1(0).ToString, GoairDt.Rows(0)("Adult"), _
                                                       GoairDt.Rows(0)("Child"), GoairDt.Rows(0)("Infant"), dtotalAmount, _
                                                       dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"))
                End If
            Catch ex As Exception
                flg = "False"
                insertLog(FltDs.Tables(0).Rows(0)("Track_id").ToString().Trim(), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), VC, "", ex.Message.Replace("'", ""), flg)
            End Try
        Catch ex As Exception
            flg = "False"
            insertLog(FltDs.Tables(0).Rows(0)("Track_id").ToString().Trim(), FltDs.Tables(0).Rows(0)("user_id").ToString().Trim(), VC, strXml, ex.Message.Replace("'", ""), flg)
        End Try
        Return PnrDt
    End Function
    Private Sub insertLog(ByVal OrderId As String, ByVal Agentid As String, ByVal Airline As String, ByVal data As String, ByVal others As String, ByVal st As String)
        paramHashtable.Clear()
        paramHashtable.Add("@Orderid", OrderId)
        paramHashtable.Add("@Agentid", Agentid)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@data", data)
        paramHashtable.Add("@others", others)
        paramHashtable.Add("@st", st)
        objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_SPLFARELOG", 1)
    End Sub
End Class
