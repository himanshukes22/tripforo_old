Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Xml

Public Class clsCalcCommAndPlb
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim objSqlTrans As New SqlTransaction
    Public Function getIntFareDetails(ByVal ln As String) As Hashtable
        'Dim objPnrTrans As New PNR_Trans
        Dim strResponse As String = ""
        Dim fltinfo As String = ""
        Dim cls As String = ""
        Dim totp As Integer
        Dim IntAirDt As DataTable
        IntAirDt = HttpContext.Current.Session("IntAirDt")
        Dim AirArray As Array
        Dim IntFareDetails As Hashtable
        AirArray = IntAirDt.Select("LineItemNumber='" & ln & "'", "")
        If (AirArray(0)("ValiDatingCarrier")) <> "IX" And (AirArray(0)("ValiDatingCarrier")) <> "AK" And (AirArray(0)("ValiDatingCarrier")) <> "SG" And (AirArray(0)("ValiDatingCarrier")) <> "6E" And (AirArray(0)("ValiDatingCarrier")) <> "G9" Then
            For i As Integer = 0 To AirArray.Length - 1
                fltinfo = fltinfo & (AirArray(i)("DepartureDate")) & ":" & (AirArray(i)("DepartureTime")) & ":" & (AirArray(i)("ArrivalTime")) & ":" & (AirArray(i)("DepartureLocation")) & ":" & (AirArray(i)("ArrivalLocation")) & ":" & (AirArray(i)("MarketingCarrier")) & ":" & (AirArray(i)("FlightIdentification")) & ":" & (AirArray(i)("RBD")) & "#"
                cls = cls & (AirArray(i)("RBD")) & ":"
            Next
            If (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) = 0 And Val((AirArray(0)("Infant"))) = 0) Then
                totp = 1
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) > 0) And Val((AirArray(0)("Infant"))) = 0 Then
                totp = 2
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) > 0) And Val((AirArray(0)("Infant"))) > 0 Then
                totp = 3
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) = 0) And Val((AirArray(0)("Infant"))) > 0 Then
                totp = 2
            End If

            strResponse = "" 'objPnrTrans.SendandReceiveXML((AirArray(0)("ValiDatingCarrier")), fltinfo, (AirArray(0)("Adult")), (AirArray(0)("Child")), (AirArray(0)("Infant")), cls)
            IntFareDetails = getPrice(strResponse, totp, Convert.ToString((AirArray(0)("Adult"))), Convert.ToString((AirArray(0)("Child"))), Convert.ToString((AirArray(0)("Infant"))), (AirArray(0)("ValiDatingCarrier")), (AirArray(0)("OrgDestFrom")), (AirArray(0)("OrgDestTo")), cls, AirArray)
        Else
            IntFareDetails = getPrice_IntLCC(AirArray)
        End If

        Return IntFareDetails
    End Function

    Private Function getPrice(ByVal fare_res As String, ByVal totp As String, ByVal adult As String, ByVal child As String, ByVal infant As String, ByVal airline As String, ByVal Org As String, ByVal Dst As String, ByVal Cls As String, ByVal AirArray As Array) As Hashtable
        Dim Reader As New XmlDocument()
        Dim tot_b_yq As Double = 0
        Dim adt_yq As Double = 0
        Dim chd_yq As Double = 0
        Dim inf_yq As Double = 0
        Dim fare_info As String = "", agnt_type As String = ""
        Dim Adt_Bfare, Chd_Bfare, Inf_Bfare, Adt_Tot, Chd_Tot, Inf_Tot, Total_fare As String
        Dim Adt_Tax As String = ""
        Dim Chd_Tax As String = ""
        Dim Inf_Tax As String = ""
        Dim Total_Bfare As String = ""
        Dim Tax_Info As String = ""
        Dim adtQtax(), chdQtax() As String
        Dim adtQ As Double = 0
        Dim adtRoe As Double = 0
        Dim chdQ As Double = 0
        Dim chdRoe As Double = 0
        agnt_type = HttpContext.Current.Session("agent_type")

        Dim farepaxxml() As String = Split(fare_res, "PriceAndAddPax")
        Reader.LoadXml(farepaxxml(0))
        Dim GroupOfFare As XmlNodeList = Reader.GetElementsByTagName("fareList")
        For i As Integer = 0 To totp - 1
            fare_info = "<XML>" & GroupOfFare.Item(i).InnerXml & "</XML>"
            Reader.LoadXml(fare_info)
            Dim Fare As XmlNodeList = Reader.GetElementsByTagName("fareAmount")
            If i = 0 And Val(adult) > 0 Then
                ' Adt_Bfare = Adt_Bfare + Fare.Item(0).InnerText
                'Adt_Tot = Adt_Tot + Fare.Item(1).InnerText
                Fare = Reader.GetElementsByTagName("fareDataInformation")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                        For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                                If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                    Adt_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                    Adt_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                    Adt_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                End If
                            End If
                        Next
                    End If
                Next
                'Qtax calc for Adult
                Fare = Reader.GetElementsByTagName("otherPricingInfo")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Left(Fare(0).ChildNodes(ii).InnerText, 3) = "FCA" Then
                        adtQtax = Split(Fare(0).ChildNodes(ii).InnerText.Replace("FCA", "").Replace(Org, "").Replace(Dst, "").Replace(airline, ""), " ")
                    End If
                Next

                For Each t In adtQtax
                    If t.StartsWith("Q") Then
                        If IsNumeric(t.Replace("Q", "")) Then
                            adtQ += Convert.ToDouble(t.Replace("Q", ""))
                        End If
                    ElseIf t.StartsWith("ROE") Then
                        If IsNumeric(t.Replace("ROE", "")) Then
                            adtRoe = Convert.ToDouble(t.Replace("ROE", ""))
                        End If
                    End If
                Next
                adtQ = Math.Round((adtQ * adtRoe), 0)
                'End Qtax Calc for Adult
            ElseIf i = 1 And Val(child) > 0 Then
                'Chd_Bfare = Chd_Bfare + Fare.Item(0).InnerText
                'Chd_Tot = Chd_Tot + Fare.Item(1).InnerText
                Fare = Reader.GetElementsByTagName("fareDataInformation")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                        For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                                If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                    Chd_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                    Chd_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                    Chd_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                End If
                            End If
                        Next
                    End If
                Next
                'Qtax calc for Child
                Fare = Reader.GetElementsByTagName("otherPricingInfo")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Left(Fare(0).ChildNodes(ii).InnerText, 3) = "FCA" Then
                        chdQtax = Split(Fare(0).ChildNodes(ii).InnerText.Replace("FCA", "").Replace(Org, "").Replace(Dst, "").Replace(airline, ""), " ")
                    End If
                Next

                For Each t In chdQtax
                    If t.StartsWith("Q") Then
                        If IsNumeric(t.Replace("Q", "")) Then
                            chdQ += Convert.ToDouble(t.Replace("Q", ""))
                        End If
                    ElseIf t.StartsWith("ROE") Then
                        If IsNumeric(t.Replace("ROE", "")) Then
                            chdRoe = Convert.ToDouble(t.Replace("ROE", ""))
                        End If
                    End If
                Next
                chdQ = Math.Round((chdQ * chdRoe), 0)
                'End Qtax Calc for Child
            ElseIf i = 1 And Val(infant) > 0 Then
                'Inf_Bfare = Inf_Bfare + Fare.Item(0).InnerText
                'Inf_Tot = Inf_Tot + Fare.Item(1).InnerText
                Fare = Reader.GetElementsByTagName("fareDataInformation")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                        For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                                If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                    Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                    Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                    Inf_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                End If
                            End If
                        Next
                    End If
                Next
            ElseIf i = 2 Then
                'Inf_Bfare = Inf_Bfare + Fare.Item(0).InnerText
                'Inf_Tot = Inf_Tot + Fare.Item(1).InnerText
                Fare = Reader.GetElementsByTagName("fareDataInformation")
                For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                    If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                        For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                                If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                    Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                    Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                    Inf_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                                End If
                            End If
                        Next
                    End If
                Next
            End If

            Dim Tot_Tax As XmlNodeList = Reader.GetElementsByTagName("taxInformation")

            'Reader.ChildNodes(0).ChildNodes(11).ChildNodes fpr refundale and non-refundable

            For ii As Integer = 0 To Tot_Tax.Count - 1
                Tax_Info = "<XML>" & Tot_Tax.Item(ii).InnerXml & "</XML>"
                Reader.LoadXml(Tax_Info)
                Dim Tax_typ As XmlNodeList = Reader.GetElementsByTagName("isoCountry")
                Dim Tax_Amt As XmlNodeList = Reader.GetElementsByTagName("fareAmount")


                If i = 0 Then
                    Adt_Tax = Adt_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                    Try
                        If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                            adt_yq = adt_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                        End If
                    Catch ex As Exception
                    End Try
                ElseIf i = 1 And Val(child) > 0 Then
                    Chd_Tax = Chd_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                    Try
                        If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                            chd_yq = chd_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                        End If
                    Catch ex As Exception
                    End Try
                ElseIf i = 1 And Val(infant) > 0 Then
                    Inf_Tax = Inf_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                    Try
                        If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                            inf_yq = inf_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                        End If
                    Catch ex As Exception
                    End Try
                ElseIf i = 2 Then
                    Inf_Tax = Inf_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                    Try
                        If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                            inf_yq = inf_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next
        Next
        Dim IntFareDetails As New Hashtable
        Dim adtComm As Integer = 0, chdComm As Integer = 0, adtTds As Integer = 0, chdTds As Integer = 0, tdsPrcnt As Double = 0
        Dim adtSrvtax As Integer = 0, chdSrvtax As Integer = 0, SrvTaxP As Double = 0
        Dim depdate As String = "", retdate As String = ""
        tdsPrcnt = Convert.ToDouble(geTdsPercentagefromDb(HttpContext.Current.Session("UID")))
        Dim ds As New DataSet
        ds = objSqlTrans.calcServicecharge((AirArray(0)("ValiDatingCarrier")), "I")
        SrvTaxP = ds.Tables(0).Rows(0)("SrvTax")
        For i As Integer = 0 To AirArray.Length - 1
            If (AirArray(i)("TripType")).ToString() = "O" And depdate = "" Then
                depdate = (AirArray(i)("DepartureDate")).ToString()
            End If
            If (AirArray(i)("TripType")).ToString() = "R" And retdate = "" Then
                retdate = (AirArray(i)("DepartureDate")).ToString()
            End If
        Next
        'Total_Bfare = ((Convert.ToDouble(Adt_Bfare) * Val(adult)) + (Convert.ToDouble(Chd_Bfare) * Val(child))).ToString
        'tot_b_yq = ((adt_yq * Val(adult)) + (chd_yq * Val(child))).ToString
        adtComm = calcComm(agnt_type, airline, Convert.ToDouble(Adt_Bfare), adt_yq, Org, Dst, Cls, adtQ, depdate, retdate)
        chdComm = calcComm(agnt_type, airline, Convert.ToDouble(Chd_Bfare), chd_yq, Org, Dst, Cls, chdQ, depdate, retdate)
        adtTds = Math.Round((adtComm * tdsPrcnt / 100), 0)
        chdTds = Math.Round((chdComm * tdsPrcnt / 100), 0)

        adtSrvtax = Math.Round((adtComm * SrvTaxP / 100), 0)
        chdSrvtax = Math.Round((chdComm * SrvTaxP / 100), 0)

        IntFareDetails.Add("AdtBFare", Convert.ToDouble(Adt_Bfare))
        IntFareDetails.Add("AdtTax", Adt_Tax)
        IntFareDetails.Add("AdtTotal", Convert.ToDouble(Adt_Tot))
        If Val(child) > 0 Then
            IntFareDetails.Add("ChdBFare", Convert.ToDouble(Chd_Bfare))
            IntFareDetails.Add("ChdTax", Chd_Tax)
            IntFareDetails.Add("ChdTotal", Convert.ToDouble(Chd_Tot))
        Else
            IntFareDetails.Add("ChdBFare", 0)
            IntFareDetails.Add("ChdTax", "")
            IntFareDetails.Add("ChdTotal", 0)
        End If
        If Val(infant) > 0 Then
            IntFareDetails.Add("InfBFare", Convert.ToDouble(Inf_Bfare))
            IntFareDetails.Add("InfTax", Inf_Tax)
            IntFareDetails.Add("InfTotal", Convert.ToDouble(Inf_Tot))
        Else
            IntFareDetails.Add("InfBFare", 0)
            IntFareDetails.Add("InfTax", "")
            IntFareDetails.Add("InfTotal", 0)
        End If

        IntFareDetails.Add("SrvTax", (adtSrvtax * Val(adult)) + (chdSrvtax * (child)))
        IntFareDetails.Add("TFee", Convert.ToDouble((AirArray(0)("TFee"))))

        IntFareDetails.Add("adtComm", adtComm)
        IntFareDetails.Add("chdComm", chdComm)

        IntFareDetails.Add("adtCB", 0)
        IntFareDetails.Add("chdCB", 0)

        IntFareDetails.Add("adtTds", adtTds)
        IntFareDetails.Add("chdTds", chdTds)

        IntFareDetails.Add("totComm", (adtComm * Val(adult)) + (chdComm * (child)))
        IntFareDetails.Add("totCB", 0)
        IntFareDetails.Add("totTds", (adtTds * Val(adult)) + (chdTds * (child)))

        Dim TC As Integer = 0, AgentTC As Integer = 0
        TC = (Convert.ToInt32((AirArray(0)("ADTAdminMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAdminMrk"))) * (child)) + (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val(child))
        AgentTC = (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val(child))
        IntFareDetails.Add("TC", TC)
        Dim totFare As Integer = 0
        Dim netFare As Integer = 0
        totFare = (IntFareDetails("AdtTotal") * Val(adult)) + (IntFareDetails("ChdTotal") * Val(child)) + (IntFareDetails("InfTotal") * Val(infant))
        totFare = totFare + IntFareDetails("SrvTax") + IntFareDetails("TFee") + IntFareDetails("TC")
        netFare = (totFare + IntFareDetails("totTds")) - (IntFareDetails("totComm") + AgentTC)
        IntFareDetails.Add("totFare", totFare)
        IntFareDetails.Add("netFare", netFare)
        Return IntFareDetails
    End Function
    Public Function calcComm(ByVal GPType As String, ByVal airline As String, ByVal BaseFare As Double, ByVal YQ As Double, ByVal Org As String, ByVal Dst As String, ByVal cls As String, ByVal Qtax As Double, ByVal ddate As String, ByVal rdate As String) As Integer
        Dim comm As Object
        Dim inputParam As New Hashtable
        inputParam.Add("@GPType", GPType)
        inputParam.Add("@AirCode", airline)
        inputParam.Add("@BaseFare", BaseFare)
        inputParam.Add("@YQ", YQ)
        inputParam.Add("@Org", Org)
        inputParam.Add("@Dst", Dst)
        inputParam.Add("@Cls", cls)
        inputParam.Add("@Qtax", Qtax)
        inputParam.Add("@ddate", ddate)
        inputParam.Add("@rdate", rdate)
        comm = objDataAcess.ExecuteData(Of Object)(inputParam, True, "CalcCommPlb", 2)
        Return comm
    End Function
    Public Function calcCommDom(ByVal GPType As String, ByVal airline As String, ByVal BaseFare As Double, ByVal YQ As Double, ByVal PaxCnt As Integer) As DataTable
        Dim comm As DataSet
        Dim inputParam As New Hashtable
        inputParam.Add("@GPType", GPType)
        inputParam.Add("@AirCode", airline)
        inputParam.Add("@BaseFare", BaseFare)
        inputParam.Add("@YQ", YQ)
        inputParam.Add("@PaxCnt", PaxCnt)
        comm = objDataAcess.ExecuteData(Of Object)(inputParam, True, "CalcComm", 3)
        Return comm.Tables(0)
    End Function
    Public Function geTdsPercentagefromDb(ByVal uid As String) As String
        Dim inputParam As New Hashtable
        Dim tds As DataSet
        Try
            inputParam.Add("@UserId", uid)
            tds = objDataAcess.ExecuteData(Of Object)(inputParam, True, "GetTdsPrcnt", 3)
            Return tds.Tables(0).Rows(0)(0)
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    Public Function getDomFareDetails(ByVal Ln As String, ByVal ft As String) As Hashtable
        'Dim PNR As New PNR_Trans
        Dim FareDetails As Hashtable
        Dim totp As Integer
        Dim cls
        Dim PriceTy As New PnrTst
        Dim tds, netamt As String
        Dim DomAirDt As DataTable
        If ft = "OutBound" Then
            DomAirDt = HttpContext.Current.Session("DomAirDt")
        Else
            DomAirDt = HttpContext.Current.Session("DomAirDtR")
        End If

        Dim AirArray As Array
        AirArray = DomAirDt.Select("LineItemNumber='" & Ln & "'", "")
        If (AirArray(0)("ValiDatingCarrier")) <> "G8" And (AirArray(0)("ValiDatingCarrier")) <> "6E" And (AirArray(0)("ValiDatingCarrier")) <> "SG" And (AirArray(0)("ValiDatingCarrier")) <> "IX" Then
            If (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) = 0 And Val((AirArray(0)("Infant"))) = 0) Then
                totp = 1
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) > 0) And Val((AirArray(0)("Infant"))) = 0 Then
                totp = 2
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) > 0) And Val((AirArray(0)("Infant"))) > 0 Then
                totp = 3
            ElseIf (Val((AirArray(0)("Adult"))) > 0 And Val((AirArray(0)("Child"))) = 0) And Val((AirArray(0)("Infant"))) > 0 Then
                totp = 2
            End If

            Dim cls_TY, flt_info_TY As String
            Dim refq As New ArrayList
            Try
                'fltLeg = Split(cls_TY.ToString.Replace("SEGMNT", "#"), "#")
                For i As Integer = 0 To AirArray.Length - 1
                    refq.Add("S")
                    cls_TY = cls_TY + (AirArray(i)("RBD")) + ":" + (AirArray(i)("fareBasis")) + ":" + (AirArray(i)("FBPaxType")) + "SEGMNT"
                    flt_info_TY = flt_info_TY + (AirArray(i)("DepartureDate")) + ":" + (AirArray(i)("DepartureTime")) + ":" + (AirArray(i)("ArrivalTime")) + ":" + (AirArray(i)("DepartureLocation")) + ":" + (AirArray(i)("ArrivalLocation")) + ":" + Left((AirArray(i)("MarketingCarrier")), 2) + ":" + CStr((AirArray(i)("FlightIdentification"))) + "S:E:G"
                Next
            Catch ex As Exception
            End Try

            Try
                Dim fare
                fare = "" 'PNR.SendandReceiveXML_New((AirArray(0)("ValiDatingCarrier")), flt_info_TY.ToString.Replace("S:E:G", "#"), (AirArray(0)("Adult")), (AirArray(0)("Child")), (AirArray(0)("Infant")), cls_TY.ToString.Replace("SEGMNT", "#"), refq)
                Dim adtfarelst As String = ""
                Dim chdfarelst As String = ""
                Dim inffarelst As String = ""
                Dim fare1 As ArrayList
                Dim tkt As Hashtable
                Dim farepaxxml() As String = Split(fare, "PriceAndAddPax")
                fare1 = PriceTy.TST_Read(farepaxxml(0), "")
                tkt = PriceTy.tktdetails(farepaxxml(1))


                Dim tktno As ArrayList = tkt("TktNoArrayList")
                Try
                    For i As Integer = 0 To tktno.Count - 1
                        Dim tktsplit() As String = Split(tktno(i), "::")
                        For ii As Integer = 0 To fare1.Count - 1
                            Dim splitfare() As String = Split(fare1(ii), "#")
                            If Val((AirArray(0)("Adult"))) > 0 Then
                                If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "ADT" And splitfare(2).Split("/")(0) = "PA" Then
                                    If Val(splitfare(1)) = Val((AirArray(0)("AdtFare"))) Then
                                        adtfarelst = Right(fare1(ii), 1)
                                    Else
                                        'adtfarelst = "INVALIDFARE"
                                    End If
                                End If
                            End If
                            If Val((AirArray(0)("Child"))) > 0 Then
                                If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "CHD" And splitfare(2).Split("/")(0) = "PA" Then
                                    If Val(splitfare(1)) = Val((AirArray(0)("ChdFare"))) Then
                                        chdfarelst = Right(fare1(ii), 1)
                                    Else
                                        ' chdfarelst = "INVALIDFARE"
                                    End If
                                End If
                            End If
                            If Val((AirArray(0)("Infant"))) > 0 Then
                                If splitfare(2).Split("/")(1) = tktsplit(0) And tktsplit(1) = "INF" And splitfare(2).Split("/")(0) = "PI" Then
                                    If Val(splitfare(1)) = Val((AirArray(0)("InfFare"))) Then
                                        inffarelst = Right(fare1(ii), 1)
                                    Else
                                        inffarelst = "INVALIDFARE"
                                    End If
                                End If
                            End If
                        Next
                    Next
                Catch ex As Exception
                End Try
                Dim chkfare As Boolean = False
                If Val((AirArray(0)("Adult"))) > 0 Then If adtfarelst = "" Then chkfare = False Else chkfare = True
                If Val((AirArray(0)("Child"))) > 0 Then If chdfarelst = "" Then chkfare = False Else chkfare = True
                If chkfare = True Then
                    FareDetails = getPrice_R95(fare, totp, (AirArray(0)("Adult")), (AirArray(0)("Child")), (AirArray(0)("Infant")), (AirArray(0)("ValiDatingCarrier")), Val(adtfarelst), Val(chdfarelst), Val(inffarelst), AirArray)
                End If
            Catch ex As Exception
            End Try
        Else
            FareDetails = getPrice_LCC(AirArray)
        End If
        Return FareDetails
    End Function

    Public Function getRTFareDetails(ByVal Ln As String, ByVal ft As String) As Hashtable
        Dim lccarray As Array
        Dim lccarrayR As Array
        Dim linarray() As String
        Dim FareDetails As Hashtable
        If ft = "DEP" Then
            lccarray = HttpContext.Current.Session("depDV").select("LineItemNumber='" & Ln.ToString & "'", "")
        ElseIf ft = "RET" Then
            lccarray = HttpContext.Current.Session("retDV").select("LineItemNumber='" & Ln.ToString & "'", "")
        ElseIf ft = "TF" Then
            linarray = Split(Ln, "-")
            lccarray = HttpContext.Current.Session("depDV").select("LineItemNumber='" & linarray(0).ToString & "'", "")
            lccarrayR = HttpContext.Current.Session("retDV").select("LineItemNumber='" & linarray(1).ToString & "'", "")
        End If
        FareDetails = getPrice_LCC(lccarray)
        Return FareDetails
    End Function

    Public Function getPrice_LCC(ByVal AirArray As Array) As Hashtable
        Dim DomFareDetails As New Hashtable
        Dim SrvTaxTFeeHsTbl As Hashtable
        Dim agnt_type As String = ""
        agnt_type = HttpContext.Current.Session("agent_type")
        Dim adtComm As Integer = 0, chdComm As Integer = 0, adtTds As Integer = 0, chdTds As Integer = 0, tdsPrcnt As Double = 0, adtCB As Integer = 0, chdCB As Integer = 0
        Dim adtTF As Integer = 0, chdTF As Integer = 0
        Dim adtSrvtax As Integer = 0, chdSrvtax As Integer = 0, SrvTaxP As Double = 0
        tdsPrcnt = Convert.ToDouble(geTdsPercentagefromDb(HttpContext.Current.Session("UID")))
        Dim ds As New DataSet
        ds = objSqlTrans.calcServicecharge((AirArray(0)("ValiDatingCarrier")), "D")
        SrvTaxP = ds.Tables(0).Rows(0)("SrvTax")
        'Total_Bfare = ((Convert.ToDouble(Adt_Bfare) * Val(adult)) + (Convert.ToDouble(Chd_Bfare) * Val(child))).ToString
        'tot_b_yq = ((adt_yq * Val(adult)) + (chd_yq * Val(child))).ToString
        Dim PaxCnt As Integer = 0
        PaxCnt = 1 '(AirArray(0)("Adult")) + (AirArray(0)("Child"))
        Dim commDt As DataTable
        commDt = calcCommDom(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtFSur"))), PaxCnt)
        adtComm = Math.Round(commDt.Rows(0)("Dis"), 0)
        'adtCB = Math.Round(commDt.Rows(0)("CB"), 0)
        If (AirArray(0)("ValiDatingCarrier")) = "G8" Then
            If (AirArray(0)("Sector")) <> "DEL:SXR" And (AirArray(0)("Sector")) <> "SXR:DEL" And (AirArray(0)("Sector")) <> "DEL:IXL" And (AirArray(0)("Sector")) <> "IXL:DEL" And (AirArray(0)("Sector")) <> "DEL:IXJ" And (AirArray(0)("Sector")) <> "IXJ:DEL" And (AirArray(0)("Sector")) <> "BOM:PAT" And (AirArray(0)("Sector")) <> "PAT:BOM" And (AirArray(0)("Sector")) <> "BOM:IXR" And (AirArray(0)("Sector")) <> "IXR:BOM" And (AirArray(0)("Sector")) <> "BOM:IXC" And (AirArray(0)("Sector")) <> "IXC:BOM" And (AirArray(0)("Sector")) <> "IXJ:SXR" And (AirArray(0)("Sector")) <> "SXR:IXJ" And (AirArray(0)("Sector")) <> "SXR:IXL" And (AirArray(0)("Sector")) <> "IXL:SXR" And (AirArray(0)("Sector")) <> "IXJ:IXL" And (AirArray(0)("Sector")) <> "IXL:IXJ" And (AirArray(0)("Sector")) <> "PAT:IXR" And (AirArray(0)("Sector")) <> "IXR:PAT" Then
                adtCB = Math.Round(commDt.Rows(0)("CB"), 0)
            Else
                adtCB = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
            End If
        Else
            adtCB = Math.Round(commDt.Rows(0)("CB"), 0)
        End If

        commDt = calcCommDom(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdFSur"))), PaxCnt)
        chdComm = Math.Round(commDt.Rows(0)("Dis"), 0)

        If (AirArray(0)("ValiDatingCarrier")) = "G8" Then
            If (AirArray(0)("Sector")) <> "DEL:SXR" And (AirArray(0)("Sector")) <> "SXR:DEL" And (AirArray(0)("Sector")) <> "DEL:IXL" And (AirArray(0)("Sector")) <> "IXL:DEL" And (AirArray(0)("Sector")) <> "DEL:IXJ" And (AirArray(0)("Sector")) <> "IXJ:DEL" And (AirArray(0)("Sector")) <> "BOM:PAT" And (AirArray(0)("Sector")) <> "PAT:BOM" And (AirArray(0)("Sector")) <> "BOM:IXR" And (AirArray(0)("Sector")) <> "IXR:BOM" And (AirArray(0)("Sector")) <> "BOM:IXC" And (AirArray(0)("Sector")) <> "IXC:BOM" And (AirArray(0)("Sector")) <> "IXJ:SXR" And (AirArray(0)("Sector")) <> "SXR:IXJ" And (AirArray(0)("Sector")) <> "SXR:IXL" And (AirArray(0)("Sector")) <> "IXL:SXR" And (AirArray(0)("Sector")) <> "IXJ:IXL" And (AirArray(0)("Sector")) <> "IXL:IXJ" And (AirArray(0)("Sector")) <> "PAT:IXR" And (AirArray(0)("Sector")) <> "IXR:PAT" Then
                chdCB = Math.Round(commDt.Rows(0)("CB"), 0)
            Else
                chdCB = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
            End If
        Else
            chdCB = Math.Round(commDt.Rows(0)("CB"), 0)
        End If

        SrvTaxTFeeHsTbl = FuncSrvTaxTFee((AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtFSur"))), "D")
        adtTF = SrvTaxTFeeHsTbl("TF")
        SrvTaxTFeeHsTbl.Clear()
        SrvTaxTFeeHsTbl = FuncSrvTaxTFee((AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdFSur"))), "D")
        chdTF = SrvTaxTFeeHsTbl("TF")

        adtSrvtax = Math.Round(((adtComm - adtTF) * SrvTaxP / 100), 0)
        chdSrvtax = Math.Round(((chdComm - chdTF) * SrvTaxP / 100), 0)

        adtTds = Math.Round(((adtComm - adtTF) * tdsPrcnt / 100), 0)
        chdTds = Math.Round(((chdComm - chdTF) * tdsPrcnt / 100), 0)

        Dim Adt_Tax, Chd_Tax, Inf_Tax As String

        Adt_Tax = "YQ:" & (AirArray(0)("AdtFSur")).ToString & "#OT:" & ((AirArray(0)("AdtTax")) - (AirArray(0)("AdtFSur"))) & "#"

        DomFareDetails.Add("AdtBFare", Convert.ToDouble((AirArray(0)("AdtBfare"))))
        DomFareDetails.Add("AdtTax", Adt_Tax)
        DomFareDetails.Add("AdtTotal", Convert.ToDouble((AirArray(0)("AdtFare"))))
        If Val((AirArray(0)("Child"))) > 0 Then
            Chd_Tax = "YQ:" & (AirArray(0)("ChdFSur")).ToString & "#OT:" & ((AirArray(0)("ChdTax")) - (AirArray(0)("ChdFSur"))) & "#"
            DomFareDetails.Add("ChdBFare", Convert.ToDouble((AirArray(0)("ChdBfare"))))
            DomFareDetails.Add("ChdTax", Chd_Tax)
            DomFareDetails.Add("ChdTotal", Convert.ToDouble((AirArray(0)("ChdFare"))))
        Else
            DomFareDetails.Add("ChdBFare", 0)
            DomFareDetails.Add("ChdTax", "")
            DomFareDetails.Add("ChdTotal", 0)
        End If
        If Val((AirArray(0)("Infant"))) > 0 Then
            Inf_Tax = "YQ:" & (AirArray(0)("InfFSur")).ToString & "#OT:" & ((AirArray(0)("InfTax")) - (AirArray(0)("InfFSur"))) & "#"
            DomFareDetails.Add("InfBFare", Convert.ToDouble((AirArray(0)("InfBfare"))))
            DomFareDetails.Add("InfTax", Inf_Tax)
            DomFareDetails.Add("InfTotal", Convert.ToDouble((AirArray(0)("InfFare"))))
        Else
            DomFareDetails.Add("InfBFare", 0)
            DomFareDetails.Add("InfTax", "")
            DomFareDetails.Add("InfTotal", 0)
        End If

        DomFareDetails.Add("SrvTax", (adtSrvtax * Val((AirArray(0)("Adult")))) + (chdSrvtax * Val((AirArray(0)("Child"))))) 'Convert.ToDouble((AirArray(0)("STax")))
        DomFareDetails.Add("TFee", Convert.ToDouble((AirArray(0)("TFee"))))

        DomFareDetails.Add("adtComm", adtComm)
        DomFareDetails.Add("chdComm", chdComm)

        DomFareDetails.Add("adtCB", adtCB)
        DomFareDetails.Add("chdCB", chdCB)

        DomFareDetails.Add("adtTds", adtTds)
        DomFareDetails.Add("chdTds", chdTds)

        DomFareDetails.Add("totComm", (adtComm * Val((AirArray(0)("Adult")))) + (chdComm * (Val(AirArray(0)("Child")))))
        DomFareDetails.Add("totCB", (adtCB * Val((AirArray(0)("Adult")))) + (chdCB * (Val(AirArray(0)("Child")))))
        DomFareDetails.Add("totTds", (adtTds * Val((AirArray(0)("Adult")))) + (chdTds * (Val(AirArray(0)("Child")))))

        Dim TC As Integer = 0, AgentTC As Integer = 0
        TC = (Convert.ToInt32((AirArray(0)("ADTAdminMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAdminMrk"))) * ((AirArray(0)("Child")))) + (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
        AgentTC = (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
        DomFareDetails.Add("TC", TC)
        Dim totFare As Integer = 0
        Dim netFare As Integer = 0
        totFare = (DomFareDetails("AdtTotal") * Val((AirArray(0)("Adult")))) + (DomFareDetails("ChdTotal") * Val((AirArray(0)("Child")))) + (DomFareDetails("InfTotal") * Val((AirArray(0)("Infant"))))
        totFare = totFare + DomFareDetails("SrvTax") + DomFareDetails("TFee") + DomFareDetails("TC")
        netFare = (totFare + DomFareDetails("totTds")) - (DomFareDetails("totComm") + DomFareDetails("totCB") + AgentTC)
        DomFareDetails.Add("totFare", totFare)
        DomFareDetails.Add("netFare", netFare)
        Return DomFareDetails
    End Function

    'Public Function getPrice_IntLCC(ByVal AirArray As Array) As Hashtable
    '    Dim IntFareDetails As New Hashtable
    '    Dim agnt_type As String = ""
    '    agnt_type = HttpContext.Current.Session("agent_type")
    '    Dim adtComm As Integer = 0, chdComm As Integer = 0, adtTds As Integer = 0, chdTds As Integer = 0, tdsPrcnt As Double = 0
    '    Dim adtSrvtax As Integer = 0, chdSrvtax As Integer = 0, SrvTaxP As Double = 0
    '    tdsPrcnt = Convert.ToDouble(geTdsPercentagefromDb(HttpContext.Current.Session("UID")))
    '    Dim ds As New DataSet
    '    ds = objSqlTrans.calcServicecharge((AirArray(0)("ValiDatingCarrier")), "I")
    '    SrvTaxP = ds.Tables(0).Rows(0)("SrvTax")
    '    'Total_Bfare = ((Convert.ToDouble(Adt_Bfare) * Val(adult)) + (Convert.ToDouble(Chd_Bfare) * Val(child))).ToString
    '    'tot_b_yq = ((adt_yq * Val(adult)) + (chd_yq * Val(child))).ToString
    '    adtComm = calcComm(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtFSur"))), (AirArray(0)("OrgDestFrom")), (AirArray(0)("OrgDestTo")), "")
    '    chdComm = calcComm(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdFSur"))), (AirArray(0)("OrgDestFrom")), (AirArray(0)("OrgDestTo")), "")
    '    adtTds = Math.Round((adtComm * tdsPrcnt / 100), 0)
    '    chdTds = Math.Round((chdComm * tdsPrcnt / 100), 0)

    '    adtSrvtax = Math.Round((adtComm * SrvTaxP / 100), 0)
    '    chdSrvtax = Math.Round((chdComm * SrvTaxP / 100), 0)

    '    Dim Adt_Tax, Chd_Tax, Inf_Tax As String

    '    Adt_Tax = "YQ:" & (AirArray(0)("AdtFSur")).ToString & "#OT:" & ((AirArray(0)("AdtTax")) - (AirArray(0)("AdtFSur"))) & "#"

    '    IntFareDetails.Add("AdtBFare", Convert.ToDouble((AirArray(0)("AdtBfare"))))
    '    IntFareDetails.Add("AdtTax", Adt_Tax)
    '    IntFareDetails.Add("AdtTotal", Convert.ToDouble((AirArray(0)("AdtFare"))))
    '    If Val((AirArray(0)("Child"))) > 0 Then
    '        Chd_Tax = "YQ:" & (AirArray(0)("ChdFSur")).ToString & "#OT:" & ((AirArray(0)("ChdTax")) - (AirArray(0)("ChdFSur"))) & "#"
    '        IntFareDetails.Add("ChdBFare", Convert.ToDouble((AirArray(0)("ChdBfare"))))
    '        IntFareDetails.Add("ChdTax", Chd_Tax)
    '        IntFareDetails.Add("ChdTotal", Convert.ToDouble((AirArray(0)("ChdFare"))))
    '    Else
    '        IntFareDetails.Add("ChdBFare", 0)
    '        IntFareDetails.Add("ChdTax", "")
    '        IntFareDetails.Add("ChdTotal", 0)
    '    End If
    '    If Val((AirArray(0)("Infant"))) > 0 Then
    '        IntFareDetails.Add("InfBFare", Convert.ToDouble((AirArray(0)("InfFare"))))
    '        IntFareDetails.Add("InfTax", Inf_Tax)
    '        IntFareDetails.Add("InfTotal", Convert.ToDouble((AirArray(0)("InfFare"))))
    '    Else
    '        IntFareDetails.Add("InfBFare", 0)
    '        IntFareDetails.Add("InfTax", "")
    '        IntFareDetails.Add("InfTotal", 0)
    '    End If

    '    IntFareDetails.Add("SrvTax", (adtSrvtax * Val((AirArray(0)("Adult")))) + (chdSrvtax * ((AirArray(0)("Child")))))
    '    IntFareDetails.Add("TFee", Convert.ToDouble((AirArray(0)("TFee"))))

    '    IntFareDetails.Add("adtComm", adtComm)
    '    IntFareDetails.Add("chdComm", chdComm)

    '    IntFareDetails.Add("adtCB", 0)
    '    IntFareDetails.Add("chdCB", 0)

    '    IntFareDetails.Add("adtTds", adtTds)
    '    IntFareDetails.Add("chdTds", chdTds)

    '    IntFareDetails.Add("totComm", (adtComm * Val((AirArray(0)("Adult")))) + (chdComm * ((AirArray(0)("Child")))))
    '    IntFareDetails.Add("totCB", 0)
    '    IntFareDetails.Add("totTds", (adtTds * Val((AirArray(0)("Adult")))) + (chdTds * ((AirArray(0)("Child")))))

    '    Dim TC As Integer = 0, AgentTC As Integer = 0
    '    TC = (Convert.ToInt32((AirArray(0)("ADTAdminMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAdminMrk"))) * ((AirArray(0)("Child")))) + (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
    '    AgentTC = (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
    '    IntFareDetails.Add("TC", TC)
    '    Dim totFare As Integer = 0
    '    Dim netFare As Integer = 0
    '    totFare = (IntFareDetails("AdtTotal") * Val((AirArray(0)("Adult")))) + (IntFareDetails("ChdTotal") * Val((AirArray(0)("Child")))) + (IntFareDetails("InfTotal") * Val((AirArray(0)("Infant"))))
    '    totFare = totFare + IntFareDetails("SrvTax") + IntFareDetails("TFee") + IntFareDetails("TC")
    '    netFare = (totFare + IntFareDetails("totTds")) - (IntFareDetails("totComm") + AgentTC)
    '    IntFareDetails.Add("totFare", totFare)
    '    IntFareDetails.Add("netFare", netFare)
    '    Return IntFareDetails
    'End Function
    Public Function getPrice_IntLCC(ByVal AirArray As Array) As Hashtable
        Dim IntFareDetails As New Hashtable
        Dim SrvTaxTFeeHsTbl As Hashtable
        Dim agnt_type As String = ""
        agnt_type = HttpContext.Current.Session("agent_type")
        Dim adtComm As Integer = 0, chdComm As Integer = 0, adtTds As Integer = 0, chdTds As Integer = 0, tdsPrcnt As Double = 0
        Dim adtTF As Integer = 0, chdTF As Integer = 0
        Dim adtSrvtax As Integer = 0, chdSrvtax As Integer = 0, SrvTaxP As Double = 0
        Dim depdate As String = "", retdate As String = ""

        For i As Integer = 0 To AirArray.Length - 1
            If (AirArray(i)("TripType")).ToString() = "O" And depdate = "" Then
                depdate = (AirArray(i)("DepartureDate")).ToString()
            End If
            If (AirArray(i)("TripType")).ToString() = "R" And retdate = "" Then
                retdate = (AirArray(i)("DepartureDate")).ToString()
            End If
        Next
        tdsPrcnt = Convert.ToDouble(geTdsPercentagefromDb(HttpContext.Current.Session("UID")))
        Dim ds As New DataSet
        ds = objSqlTrans.calcServicecharge((AirArray(0)("ValiDatingCarrier")), "I")
        SrvTaxP = ds.Tables(0).Rows(0)("SrvTax")
        'Total_Bfare = ((Convert.ToDouble(Adt_Bfare) * Val(adult)) + (Convert.ToDouble(Chd_Bfare) * Val(child))).ToString
        'tot_b_yq = ((adt_yq * Val(adult)) + (chd_yq * Val(child))).ToString
        adtComm = calcComm(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtFSur"))), (AirArray(0)("OrgDestFrom")), (AirArray(0)("OrgDestTo")), (AirArray(0)("RBD")), 0, depdate, retdate)
        chdComm = calcComm(agnt_type, (AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdFSur"))), (AirArray(0)("OrgDestFrom")), (AirArray(0)("OrgDestTo")), (AirArray(0)("RBD")), 0, depdate, retdate)

        SrvTaxTFeeHsTbl = FuncSrvTaxTFee((AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtBfare"))), Convert.ToDouble((AirArray(0)("AdtFSur"))), "I")
        adtTF = SrvTaxTFeeHsTbl("TF")
        SrvTaxTFeeHsTbl.Clear()
        SrvTaxTFeeHsTbl = FuncSrvTaxTFee((AirArray(0)("ValiDatingCarrier")), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdBfare"))), Convert.ToDouble((AirArray(0)("ChdFSur"))), "I")
        chdTF = SrvTaxTFeeHsTbl("TF")

        adtTds = Math.Round(((adtComm - adtTF) * tdsPrcnt / 100), 0)
        chdTds = Math.Round(((chdComm - chdTF) * tdsPrcnt / 100), 0)

        adtSrvtax = Math.Round(((adtComm - adtTF) * SrvTaxP / 100), 0)
        chdSrvtax = Math.Round(((chdComm - chdTF) * SrvTaxP / 100), 0)

        Dim Adt_Tax, Chd_Tax, Inf_Tax As String

        Adt_Tax = "YQ:" & (AirArray(0)("AdtFSur")).ToString & "#OT:" & ((AirArray(0)("AdtTax")) - (AirArray(0)("AdtFSur"))) & "#"

        IntFareDetails.Add("AdtBFare", Convert.ToDouble((AirArray(0)("AdtBfare"))))
        IntFareDetails.Add("AdtTax", Adt_Tax)
        IntFareDetails.Add("AdtTotal", Convert.ToDouble((AirArray(0)("AdtFare"))))
        If Val((AirArray(0)("Child"))) > 0 Then
            Chd_Tax = "YQ:" & (AirArray(0)("ChdFSur")).ToString & "#OT:" & ((AirArray(0)("ChdTax")) - (AirArray(0)("ChdFSur"))) & "#"
            IntFareDetails.Add("ChdBFare", Convert.ToDouble((AirArray(0)("ChdBfare"))))
            IntFareDetails.Add("ChdTax", Chd_Tax)
            IntFareDetails.Add("ChdTotal", Convert.ToDouble((AirArray(0)("ChdFare"))))
        Else
            IntFareDetails.Add("ChdBFare", 0)
            IntFareDetails.Add("ChdTax", "")
            IntFareDetails.Add("ChdTotal", 0)
        End If
        If Val((AirArray(0)("Infant"))) > 0 Then
            Inf_Tax = "YQ:" & (AirArray(0)("InfFSur")).ToString & "#OT:" & ((AirArray(0)("InfTax")) - (AirArray(0)("InfFSur"))) & "#"
            IntFareDetails.Add("InfBFare", Convert.ToDouble((AirArray(0)("InfBfare"))))
            IntFareDetails.Add("InfTax", Inf_Tax)
            IntFareDetails.Add("InfTotal", Convert.ToDouble((AirArray(0)("InfFare"))))
        Else
            IntFareDetails.Add("InfBFare", 0)
            IntFareDetails.Add("InfTax", "")
            IntFareDetails.Add("InfTotal", 0)
        End If

        IntFareDetails.Add("SrvTax", (adtSrvtax * Val((AirArray(0)("Adult")))) + (chdSrvtax * ((AirArray(0)("Child")))))
        IntFareDetails.Add("TFee", Convert.ToDouble((AirArray(0)("TFee"))))

        IntFareDetails.Add("adtComm", adtComm)
        IntFareDetails.Add("chdComm", chdComm)

        IntFareDetails.Add("adtCB", 0)
        IntFareDetails.Add("chdCB", 0)

        IntFareDetails.Add("adtTds", adtTds)
        IntFareDetails.Add("chdTds", chdTds)

        IntFareDetails.Add("totComm", (adtComm * Val((AirArray(0)("Adult")))) + (chdComm * ((AirArray(0)("Child")))))
        IntFareDetails.Add("totCB", 0)
        IntFareDetails.Add("totTds", (adtTds * Val((AirArray(0)("Adult")))) + (chdTds * ((AirArray(0)("Child")))))

        Dim TC As Integer = 0, AgentTC As Integer = 0
        TC = (Convert.ToInt32((AirArray(0)("ADTAdminMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAdminMrk"))) * ((AirArray(0)("Child")))) + (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
        AgentTC = (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val((AirArray(0)("Adult")))) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val((AirArray(0)("Child"))))
        IntFareDetails.Add("TC", TC)
        Dim totFare As Integer = 0
        Dim netFare As Integer = 0
        totFare = (IntFareDetails("AdtTotal") * Val((AirArray(0)("Adult")))) + (IntFareDetails("ChdTotal") * Val((AirArray(0)("Child")))) + (IntFareDetails("InfTotal") * Val((AirArray(0)("Infant"))))
        totFare = totFare + IntFareDetails("SrvTax") + IntFareDetails("TFee") + IntFareDetails("TC")
        netFare = (totFare + IntFareDetails("totTds")) - (IntFareDetails("totComm") + AgentTC)
        IntFareDetails.Add("totFare", totFare)
        IntFareDetails.Add("netFare", netFare)
        Return IntFareDetails
    End Function
    Public Function getPrice_R95(ByVal fare_res As String, ByVal totp As Integer, ByVal adult As String, ByVal child As String, ByVal infant As String, ByVal airline As String, ByVal AFL As Integer, ByVal CFL As Integer, ByVal IFL As Integer, ByVal AirArray As Array) As Hashtable
        Dim Reader As New XmlDocument()
        Dim tot_b_yq As Double = 0
        Dim adt_yq As Double = 0
        Dim chd_yq As Double = 0
        Dim inf_yq As Double = 0
        Dim fare_info As String = "", agnt_type As String = ""
        Dim Adt_Bfare, Chd_Bfare, Inf_Bfare, Adt_Tot, Chd_Tot, Inf_Tot, Total_fare As String
        Dim Adt_Tax As String = ""
        Dim Chd_Tax As String = ""
        Dim Inf_Tax As String = ""
        Dim Total_Bfare As String = ""
        Dim Tax_Info As String = ""
        agnt_type = HttpContext.Current.Session("agent_type")

        Dim farepaxxml() As String = Split(fare_res, "PriceAndAddPax")
        Reader.LoadXml(farepaxxml(0))
        Dim GroupOfFare As XmlNodeList = Reader.GetElementsByTagName("fareList")

        Dim Fare As XmlNodeList

        If Val(adult) > 0 Then
            fare_info = "<XML>" & GroupOfFare.Item(AFL).InnerXml & "</XML>"
            Reader.LoadXml(fare_info)
            Fare = Reader.GetElementsByTagName("fareDataInformation")
            For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                    For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                        If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                Adt_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                Adt_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                Adt_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            End If
                        End If
                    Next
                End If
            Next
            'Adt_Bfare = Adt_Bfare + Fare.Item(0).InnerText
            'Adt_Tot = Adt_Tot + Fare.Item(1).InnerText

            Dim Tot_Tax As XmlNodeList
            Tot_Tax = Reader.GetElementsByTagName("taxInformation")
            For ii = 0 To Tot_Tax.Count - 1
                Tax_Info = "<XML>" & Tot_Tax.Item(ii).InnerXml & "</XML>"
                Reader.LoadXml(Tax_Info)
                Dim Tax_typ As XmlNodeList = Reader.GetElementsByTagName("isoCountry")
                Dim Tax_Amt As XmlNodeList = Reader.GetElementsByTagName("fareAmount")
                Adt_Tax = Adt_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                Try
                    If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                        adt_yq = adt_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                    End If
                Catch ex As Exception
                End Try
            Next
        End If
        If Val(child) > 0 Then
            fare_info = "<XML>" & GroupOfFare.Item(CFL).InnerXml & "</XML>"
            Reader.LoadXml(fare_info)
            Fare = Reader.GetElementsByTagName("fareDataInformation")
            For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                    For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                        If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                Chd_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                Chd_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                Chd_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            End If
                        End If
                    Next
                End If
            Next
            'Chd_Bfare = Chd_Bfare + Fare.Item(0).InnerText
            'Chd_Tot = Chd_Tot + Fare.Item(1).InnerText

            Dim Tot_Tax As XmlNodeList
            Tot_Tax = Reader.GetElementsByTagName("taxInformation")
            For ii = 0 To Tot_Tax.Count - 1
                Tax_Info = "<XML>" & Tot_Tax.Item(ii).InnerXml & "</XML>"
                Reader.LoadXml(Tax_Info)
                Dim Tax_typ As XmlNodeList = Reader.GetElementsByTagName("isoCountry")
                Dim Tax_Amt As XmlNodeList = Reader.GetElementsByTagName("fareAmount")
                Chd_Tax = Chd_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                Try
                    If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                        chd_yq = chd_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                    End If
                Catch ex As Exception
                End Try
            Next
        End If
        If Val(infant) > 0 Then
            fare_info = "<XML>" & GroupOfFare.Item(IFL).InnerXml & "</XML>"
            Reader.LoadXml(fare_info)
            Fare = Reader.GetElementsByTagName("fareDataInformation")
            For ii As Integer = 0 To Fare(0).ChildNodes.Count - 1
                If Fare(0).ChildNodes(ii).Name = "fareDataSupInformation" Then
                    For jj As Integer = 0 To Fare(0).ChildNodes(ii).ChildNodes.Count - 1
                        If Fare(0).ChildNodes(ii).ChildNodes(jj).Name = "fareDataQualifier" Then
                            If Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "B" Then
                                Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "E" Then
                                Inf_Bfare = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            ElseIf Fare(0).ChildNodes(ii).ChildNodes(jj).InnerText.Trim = "712" Then
                                Inf_Tot = Fare(0).ChildNodes(ii).ChildNodes(jj).NextSibling.InnerText
                            End If
                        End If
                    Next
                End If
            Next

            'Inf_Bfare = Inf_Bfare + Fare.Item(0).InnerText
            'Inf_Tot = Inf_Tot + Fare.Item(1).InnerText

            Dim Tot_Tax As XmlNodeList
            Tot_Tax = Reader.GetElementsByTagName("taxInformation")
            For ii = 0 To Tot_Tax.Count - 1
                Tax_Info = "<XML>" & Tot_Tax.Item(ii).InnerXml & "</XML>"
                Reader.LoadXml(Tax_Info)
                Dim Tax_typ As XmlNodeList = Reader.GetElementsByTagName("isoCountry")
                Dim Tax_Amt As XmlNodeList = Reader.GetElementsByTagName("fareAmount")
                Inf_Tax = Inf_Tax + Tax_typ.Item(0).InnerText + ":" + Tax_Amt.Item(0).InnerText + "#"
                Try
                    If Tax_typ.Item(0).InnerText.Trim.ToString = "YQ" Then
                        inf_yq = inf_yq + Convert.ToDouble(Tax_Amt.Item(0).InnerText)
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        Dim DomFareDetails As New Hashtable
        Dim adtComm As Integer = 0, chdComm As Integer = 0, adtTds As Integer = 0, chdTds As Integer = 0, tdsPrcnt As Double = 0, adtCB As Integer = 0, chdCB As Integer = 0
        Dim adtSrvtax As Integer = 0, chdSrvtax As Integer = 0, SrvTaxP As Double = 0
        tdsPrcnt = Convert.ToDouble(geTdsPercentagefromDb(HttpContext.Current.Session("UID")))
        Dim ds As New DataSet
        ds = objSqlTrans.calcServicecharge((AirArray(0)("ValiDatingCarrier")), "D")
        SrvTaxP = ds.Tables(0).Rows(0)("SrvTax")
        'Total_Bfare = ((Convert.ToDouble(Adt_Bfare) * Val(adult)) + (Convert.ToDouble(Chd_Bfare) * Val(child))).ToString
        'tot_b_yq = ((adt_yq * Val(adult)) + (chd_yq * Val(child))).ToString
        Dim commDt As DataTable
        commDt = calcCommDom(agnt_type, airline, Convert.ToDouble(Adt_Bfare), adt_yq, 1)
        adtComm = Math.Round(commDt.Rows(0)("Dis"), 0)
        adtCB = Math.Round(commDt.Rows(0)("CB"), 0)

        commDt = calcCommDom(agnt_type, airline, Convert.ToDouble(Chd_Bfare), chd_yq, 1)
        chdComm = Math.Round(commDt.Rows(0)("Dis"), 0)
        chdCB = Math.Round(commDt.Rows(0)("CB"), 0)

        adtTds = Math.Round((adtComm * tdsPrcnt / 100), 0)
        chdTds = Math.Round((chdComm * tdsPrcnt / 100), 0)

        adtSrvtax = Math.Round((adtComm * SrvTaxP / 100), 0)
        chdSrvtax = Math.Round((chdComm * SrvTaxP / 100), 0)



        DomFareDetails.Add("AdtBFare", Convert.ToDouble(Adt_Bfare))
        DomFareDetails.Add("AdtTax", Adt_Tax)
        DomFareDetails.Add("AdtTotal", Convert.ToDouble(Adt_Tot))
        If Val(child) > 0 Then
            DomFareDetails.Add("ChdBFare", Convert.ToDouble(Chd_Bfare))
            DomFareDetails.Add("ChdTax", Chd_Tax)
            DomFareDetails.Add("ChdTotal", Convert.ToDouble(Chd_Tot))
        Else
            DomFareDetails.Add("ChdBFare", 0)
            DomFareDetails.Add("ChdTax", "")
            DomFareDetails.Add("ChdTotal", 0)
        End If
        If Val(infant) > 0 Then
            DomFareDetails.Add("InfBFare", Convert.ToDouble(Inf_Bfare))
            DomFareDetails.Add("InfTax", Inf_Tax)
            DomFareDetails.Add("InfTotal", Convert.ToDouble(Inf_Tot))
        Else
            DomFareDetails.Add("InfBFare", 0)
            DomFareDetails.Add("InfTax", "")
            DomFareDetails.Add("InfTotal", 0)
        End If

        DomFareDetails.Add("SrvTax", (adtSrvtax * Val(adult)) + (chdSrvtax * (child))) ' Convert.ToDouble((AirArray(0)("STax")))
        DomFareDetails.Add("TFee", Convert.ToDouble((AirArray(0)("TFee"))))

        DomFareDetails.Add("adtComm", adtComm)
        DomFareDetails.Add("chdComm", chdComm)

        DomFareDetails.Add("adtCB", adtCB)
        DomFareDetails.Add("chdCB", chdCB)

        DomFareDetails.Add("adtTds", adtTds)
        DomFareDetails.Add("chdTds", chdTds)

        DomFareDetails.Add("totComm", (adtComm * Val(adult)) + (chdComm * (child)))
        DomFareDetails.Add("totCB", (adtCB * Val(adult)) + (chdCB * (child)))
        DomFareDetails.Add("totTds", (adtTds * Val(adult)) + (chdTds * (child)))

        Dim TC As Integer = 0, AgentTC As Integer = 0
        TC = (Convert.ToInt32((AirArray(0)("ADTAdminMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAdminMrk"))) * (child)) + (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val(child))
        AgentTC = (Convert.ToInt32((AirArray(0)("ADTAgentMrk"))) * Val(adult)) + (Convert.ToInt32((AirArray(0)("CHDAgentMrk"))) * Val(child))
        DomFareDetails.Add("TC", TC)
        Dim totFare As Integer = 0
        Dim netFare As Integer = 0
        totFare = (DomFareDetails("AdtTotal") * Val(adult)) + (DomFareDetails("ChdTotal") * Val(child)) + (DomFareDetails("InfTotal") * Val(infant))
        totFare = totFare + DomFareDetails("SrvTax") + DomFareDetails("TFee") + DomFareDetails("TC")
        netFare = (totFare + DomFareDetails("totTds")) - (DomFareDetails("totComm") + DomFareDetails("totCB") + AgentTC)
        DomFareDetails.Add("totFare", totFare)
        DomFareDetails.Add("netFare", netFare)
        Return DomFareDetails
    End Function
    Private Function FuncSrvTaxTFee(ByVal VC As String, ByVal TotBFWI As Double, ByVal TotBFWOI As Double, ByVal FS As Double, ByVal Trip As String) As Hashtable
        Dim dsTax As New DataSet
        Dim AirlineCharges As New Hashtable
        Dim inputParam As New Hashtable
        inputParam.Add("@vc", VC)
        inputParam.Add("@trip", Trip)
        dsTax = objDataAcess.ExecuteData(Of Object)(inputParam, True, "ServiceCharge", 3)

        Try
            If dsTax.Tables(0).Rows.Count > 0 Then
                AirlineCharges.Add("STax", Math.Round(((TotBFWI * dsTax.Tables(0).Rows(0)("SrvTax")) / 100), 0))
                AirlineCharges.Add("TF", Math.Round((((TotBFWOI + FS) * dsTax.Tables(0).Rows(0)("TranFee")) / 100), 0))
                AirlineCharges.Add("IATAComm", dsTax.Tables(0).Rows(0)("IATAComm"))
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
End Class
