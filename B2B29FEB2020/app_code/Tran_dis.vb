Imports Microsoft.VisualBasic
Imports System.data
Imports System.Web.HttpContext
Imports System.Web.SessionState
Imports System.Math
Imports System.Data.SqlClient
Imports System.xml

Public Class Tran_dis
    Public F_TFP, TFP, FSP, STP, TFR, STR, Tot_Extra_dep, y
    Public totdisdep = 0
    Dim con As New SqlConnection
    
    Public Function tran_fee(ByVal tot_bafre, ByVal Airlines, ByVal trip) As String
        Dim agnt_type As String = ""
        Dim tot_pas As Integer
        tot_pas = Val(Current.Session("AD")) + Val(Current.Session("CH"))
        TFR = 0
        STR = 0
        agnt_type = Current.Session("agent_type")
        Tot_Extra_dep = 0
        Dim cmd As SqlCommand
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Dim Dsdp1 As New DataSet
        Dim adpdt1 As SqlDataAdapter
        If trip = "inter" Then
            cmd = New SqlCommand("select * from st_int ", con)
        Else
            cmd = New SqlCommand("select FTF,TF,FuelSur,STax,FuelS from Airline_Pa WHERE Code='" & UCase(Left(Airlines.ToString.Trim, 2)) & "'", con)
        End If


        adpdt1 = New SqlDataAdapter(cmd)
        adpdt1.Fill(Dsdp1)
        'FSP->'Calculation Of Fuel Surcharge
        'TFR->Calculation Of Tranasaction Fee
        Dim fs() As String
        Try

            If Dsdp1.Tables(0).Rows.Count > 0 Then
                If trip = "inter" Then
                    TFR = 0
                    STP = Dsdp1.Tables(0).Rows(0)(0)
                    STR = ((tot_bafre) * STP) / 100
                    Tot_Extra_dep = TFR + STR
                Else

                    F_TFP = Dsdp1.Tables(0).Rows(0)(0)
                    If F_TFP > 0 Then
                        TFR = Val(F_TFP) * tot_pas
                        STP = Dsdp1.Tables(0).Rows(0)(3)
                        STR = ((tot_bafre + FSP) * STP) / 100
                    Else
                        TFP = Dsdp1.Tables(0).Rows(0)(1)
                        If Left(Airlines.ToString.Trim, 2) = "6E" Or Left(Airlines.ToString.Trim, 2) = "G8" Or Left(Airlines.ToString.Trim, 2) = "SG" Then
                            fs = Split(Airlines, ":")
                            FSP = Val(fs(1).Trim.ToString)
                        Else
                            If Current.Session("flg_city") = "1" Then
                                FSP = Dsdp1.Tables(0).Rows(0)(4)
                            Else
                                FSP = Dsdp1.Tables(0).Rows(0)(2)
                            End If
                            FSP = "0"
                        End If


                        STP = Dsdp1.Tables(0).Rows(0)(3)         'Service Tax percentage   0.61
                        TFR = ((tot_bafre + FSP) * TFP) / 100   'Calculation of Tranasaction Fee
                        If TFR < 0.5 Then
                            TFR = 0
                        End If
                        If Left(Airlines.ToString.Trim, 2) = "6E" Or Left(Airlines.ToString.Trim, 2) = "G8" Or Left(Airlines.ToString.Trim, 2) = "SG" Then
                            STR = (Val(fs(2).ToString.Trim) * STP) / 100
                        Else
                            STR = ((tot_bafre) * STP) / 100
                        End If
                        'Calculation of Service Tax
                        If STR < 0.5 Then
                            STR = 0
                        End If
                    End If
                    ' If agnt_type = "Type2" Then
                    'Tot_Extra_dep = STR
                    'Else
                    Tot_Extra_dep = TFR + STR
                    ' End If
                End If
            Else
                TFR = 0
                STR = 0
                Tot_Extra_dep = 0
            End If
            Dsdp1.Clear()
        Catch ex As Exception
        End Try
        Return Convert.ToString(Tot_Extra_dep) & ":" & Convert.ToString(TFR) & ":" & Convert.ToString(STR)
    End Function

    Public Function discount(ByVal tot_bafre, ByVal Airlines, ByVal tran_fee) As String
        Dim di, Dbrek, air, dair, dairyq, dairbyq, dcashb, dicnt, discnt, tot, tot_pa, airln
        tot_pa = Val(Current.Session("AD")) + Val(Current.Session("CH"))
        Dim flag As String = "False"
        Dim dis = Current.Session("Discount")
        Dim tran_fee1() As String = tran_fee.Split(":")
        Dim tra = Current.Session("trip")
        Try
            If tra = "inter" Then
                dicnt = 0
                dcashb = 0
            Else
                For di = 0 To UBound(dis) - 1
                    Dbrek = Split(dis(di), "-")
                    air = Dbrek(0)
                    If Airlines = "IT4" Then airln = UCase(Left(Airlines, 3)) Else airln = UCase(Left(Airlines, 2))
                    If air = airln Then
                        dair = Dbrek(1)
                        dairyq = Dbrek(3)
                        dairbyq = Dbrek(4)
                        Dim yq1(), yq As String
                        Try
                            yq1 = Split(Airlines, ":")
                            yq = yq1(1)
                        Catch ex As Exception
                            yq = "0"
                        End Try

                        'Discount on Basic
                        discnt = (Val(tot_bafre) * Val(dair)) / 100

                        'Discount on YQ
                        discnt = discnt + (Val(yq) * Val(dairyq)) / 100

                        'Discount on Basic+YQ
                        discnt = discnt + ((Val(yq) + Val(tot_bafre)) * Val(dairbyq)) / 100

                        dicnt = (Format(Val(discnt), "####")).ToString
                        dcashb = (Val(Dbrek(2)) * Val(tot_pa)).ToString
                        If dcashb = "Y" Or dcashb = "N" Then
                            If dcashb = "N" Then
                                Tot_Extra_dep = Convert.ToDouble(tran_fee1(0)) - Convert.ToDouble(tran_fee1(1))
                                TFR = 0
                                flag = "True"
                            End If
                            dcashb = 0

                        End If
                        Exit For
                    Else
                        dicnt = 0
                        dcashb = 0
                    End If
                Next
            End If

            If dicnt.ToString = "" Then dicnt = "0"
            totdisdep = Val(dicnt) + Val(dcashb)
            If totdisdep = 0 Then
                totdisdep = "0"
            End If
            Current.Session("totdisdep") = totdisdep
        Catch ex As Exception
        End Try
        If flag = "True" Then
            Return Convert.ToString(Tot_Extra_dep) & ":" & Convert.ToString(TFR) & ":" & Convert.ToString(STR) & ":" & Convert.ToString(totdisdep) & ":" & Convert.ToString(dcashb) & ":" & Convert.ToString(dicnt)
        Else
            Return Convert.ToString(tran_fee1(0)) & ":" & Convert.ToString(tran_fee1(1)) & ":" & Convert.ToString(tran_fee1(2)) & ":" & Convert.ToString(totdisdep) & ":" & Convert.ToString(dcashb) & ":" & Convert.ToString(dicnt)
        End If

    End Function

    Public Function RoundAdv(ByVal dVal As Double, Optional ByVal iPrecision As Integer = 0) As Double
        Try

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

        Catch ex As Exception
            RoundAdv = Convert.ToDouble(DateTime.Now.Ticks)
        End Try

    End Function

    Function GetRandomNumber(ByVal allowedChars1 As String) As String
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars1.Split(sep)
        Dim rndString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To 9
            temp = arr(rand.[Next](0, arr.Length))
            rndString += temp
        Next
        Return rndString
    End Function

    Public Function GetTraceIDForIndigo() As String
        Dim allowedChars As String = ""
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,!,@,#,$,%,&,?"
        Dim rnmd1 As String = GetRandomNumber(allowedChars)
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim rnmd2 As String = GetRandomNumber(allowedChars)
        Return rnmd1 & "|" & rnmd2
    End Function

    Public Function GetTraceIDForSpiceJet() As String
        Dim allowedChars As String = ""
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,!,@,#,$,%,&,?"
        Dim rnmd1 As String = GetRandomNumber(allowedChars)
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim rnmd2 As String = GetRandomNumber(allowedChars)
        Return rnmd1 & "|" & rnmd2
    End Function

    Public Sub SaveTID(ByVal al As String, ByVal fltinfo As String, ByVal trackid As String, ByVal pnr As String, ByVal reqxml As String, ByVal resxml As String)
        Dim cmd As SqlCommand
        Try
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()
            cmd = New SqlCommand("insert into LCC_TraceID(Airline,FlightInfo,TraceID,PNR,Book_req,Book_res)values('" & al & "','" & fltinfo & "','" & trackid & "','" & pnr & "','" & reqxml & "','" & resxml & "')", con)
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function CheckAirComp(ByVal str() As String, ByVal flt_ac As String) As Boolean
        Dim xmldocmnt As New XmlDocument
        xmldocmnt.LoadXml(flt_ac)
        Dim farebasis As String = ""
        Dim tamt As String = ""
        Dim ArrivalDateTime_AC As String = ""
        Dim NumberOfStops_AC As String = ""
        Dim DepartureDateTime_AC As String = ""
        Dim Org_AC As String = ""
        Dim Dest_AC As String = ""
        Dim FlightNumber_AC As String = ""
        Dim chkac As Boolean = False
        Try
            For i As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes.Count - 1
                For ii As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes.Count - 1
                    If xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).Name = "SelectedFareBasisCode" Then
                        farebasis = xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).InnerText.Trim
                    ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).Name = "Fares" Then
                        For tf As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes.Count - 1
                            For tf1 As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes.Count - 1
                                If xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "TotalAmount" Then
                                    tamt = xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim.Replace("INR", "")
                                End If
                            Next
                        Next
                    ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).Name = "Flights" Then
                        For tf As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes.Count - 1
                            For tf1 As Integer = 0 To xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes.Count - 1
                                If xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "ArrivalDateTime" Then
                                    ArrivalDateTime_AC = xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim
                                ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "NumberOfStops" Then
                                    NumberOfStops_AC = xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim
                                ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "DepartureDateTime" Then
                                    DepartureDateTime_AC = xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim
                                ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "Origin" Then
                                    Org_AC = Org_AC & xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim & ":"
                                ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "Destination" Then
                                    Dest_AC = Dest_AC & xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim & ":"
                                ElseIf xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).Name = "FlightNumber" Then
                                    FlightNumber_AC = FlightNumber_AC & xmldocmnt.GetElementsByTagName("AirComponents")(0).ChildNodes(i).ChildNodes(ii).ChildNodes(tf).ChildNodes(tf1).InnerText.Trim & ":"
                                End If
                            Next
                        Next
                    End If
                Next
            Next
        Catch ex As Exception
            Return False
        End Try
        Dim fltno() As String = Split(str(0), " ")
        Try
            If (farebasis = str(7).ToString.Trim) And InStr(FlightNumber_AC, fltno(fltno.Length - 1).ToString.Trim) And InStr(Org_AC, str(1).ToString.Trim) Then
                chkac = True
            Else
                chkac = False
            End If
        Catch ex As Exception
        End Try 'And (Convert.ToDouble(tamt) = Convert.ToDouble(str(19).ToString.Trim)
        Return chkac
    End Function
End Class
