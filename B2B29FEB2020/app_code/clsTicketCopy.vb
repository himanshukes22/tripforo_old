Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Linq
Public Class clsTicketCopy

    Private ObjIntDetails As New IntlDetails()
    Private STDom As New SqlTransactionDom()
    Dim objSql As New SqlTransactionNew
    Public Function TicketDetail(ByVal OrderId As String, ByVal TransTD As String) As String
        Try

            'Added Meal-Bag 04/05/2014

            Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransTD)

            Dim dtpnr As New DataTable()
            dtpnr = ObjIntDetails.SelectHeaderDetail(OrderId)

            Dim dtpax As New DataTable()
            dtpax = ObjIntDetails.SelectPaxDetail(OrderId, TransTD)

            Dim dtagentid As New DataTable()
            dtagentid = ObjIntDetails.SelectAgent(OrderId)

            Dim dtagency As New DataTable()
            dtagency = ObjIntDetails.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())

            Dim dtflight As New DataTable()
            dtflight = ObjIntDetails.SelectFlightDetail(OrderId)

            Dim dtfare As New DataTable()
            dtfare = ObjIntDetails.SelectFareDetail(OrderId, TransTD)
            Dim FareType As String = ""
            If (dtfare.Rows(0)("FareType") <> "") Then
                FareType = "(" & dtfare.Rows(0)("FareType") & ")"
            End If
            Dim dtadtcount As New DataTable()
            dtadtcount = ObjIntDetails.CountADT(OrderId)

            Dim dtchdcount As New DataTable()
            dtchdcount = ObjIntDetails.CountCHD(OrderId)

            Dim dtinfcount As New DataTable()
            dtinfcount = ObjIntDetails.CountINF(OrderId)

            '            Dim AdtRbd As String = ""
            '            Dim ChdRbd As String = ""
            '            If (Convert.ToInt16(dtinfcount.Rows(0)(0).ToString()) > 0) Then
            'AdtRbd=dtflight.
            '            End If
            Dim objDA As New SqlTransaction
            'Added Meal-Bag 04/05/2014
            Dim FltDs As DataSet = objDA.GetFltDtls(OrderId, dtagentid.Rows(0)("AgentID").ToString())

            Dim mgtFee As Double = 0
            Dim MgtFeeVisibleStatus As Boolean = False

            Dim IsCorp As Boolean = HttpContext.Current.Session("IsCorp")

            If HttpContext.Current.Session("IsCorp") = False Then
                If Not IsDBNull(dtagency.Rows(0)("IsCorp")) Then
                    If Convert.ToBoolean(dtagency.Rows(0)("IsCorp")) Then
                        MgtFeeVisibleStatus = True

                        mgtFee = If(IsDBNull(dtfare.Rows(0)("MgtFee")), 0, Convert.ToDouble(dtfare.Rows(0)("MgtFee")))


                    End If

                End If
            End If


            Dim my_table As String = ""
            'my_table = "<table width='100%' border='0'  cellspacing='0'  cellpadding='0'>"
            'my_table += "<tr>"
            If dtpnr.Rows(0)("Status").ToString().Trim().ToLower() = "rejected" Then

                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-8 medium-8 small-12 large-push-2 medium-push-2  bld blue center'><u>Booking Reference No. " & OrderId & "</u></div>"
                my_table += "<div class='clear1'></div>"

                my_table += "<div class='large-12 medium-12 small-12'>Booking Failure</div>"

                my_table += "<div class='large-12 medium-12 small-12'>Please re-try the booking.Your booking has been rejected due to some technical issuse in airline. We have credited the booking amount to your account. </div>"

                my_table += "</div>"

            Else

                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-12 medium-12 small-12' style='display: none'>"
                my_table += "<table><tr><td id='td_adtcnt' class='hide'>" & dtadtcount.Rows(0)("Counting") & "</td>"
                my_table += "<td id='td_chdcnt'  class='hide'>" & dtchdcount.Rows(0)("Counting") & "</td>"
                my_table += "</tr></table></div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div class='large-8 medium-8 small-12 large-push-2 medium-push-2  bld blue center'><u>Booking Reference No. " & OrderId & "</u></div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                'my_table += "<tr>"
                'my_table += "<td colspan='2'   style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Agency Information</td>"
                'my_table += "</tr>"

                my_table += "<div class='large-2 medium-2 small-2 columns' style='background='http://RWT.co/images/nologo.png'; background-repeat: no-repeat;'></div> " 'for agency logo
                my_table += "<div class='large-10 medium-10 small-10 columns'>"
                my_table += "<div class='large-12 medium-12 small-12 bld'>Agency Information</div>"

                my_table += "<div class='large-12 medium-12 small-12'>" & dtagency.Rows(0)("Agency_Name").ToString() & "</div>"
                'my_table += "<td> <img src='CSS/images/logo.png' /></td>";


                'my_table += "<div class='large-12 medium-12 small-12'>" & dtagency.Rows(0)("Address").ToString() & "</div>"
                'my_table += "<div class='large-12 medium-12 small-12'>" & dtagency.Rows(0)("Address1").ToString() & "</div>"

                my_table += "<div class='large-12 medium-12 small-12'>" & dtagency.Rows(0)("Mobile").ToString() & "</div>"

                my_table += "<div class='large-12 medium-12 small-12'>" & dtagency.Rows(0)("Email").ToString() & "</div>"
                my_table += "</div>"
                my_table += "</div>"
                my_table += "<div class='clear1'></div>"

                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-3 medium-3 small-6 columns  bld'>GDSPNR</div>"
                my_table += "<div class='large-3 medium-3 small-6 columns  '>" & dtpnr.Rows(0)("GdsPnr").ToString() & "</div>"
                my_table += "<div class='large-3 medium-3 small-6 columns  bld'>Booking Date</div>"
                my_table += "<div class='large-3 medium-3 small-6 columns  '>" & dtpnr.Rows(0)("CreateDate").ToString() & "</div>"
                my_table += "<div class=''clear1></div>"

                my_table += "<div class='large-3 medium-3 small-6 columns  bld'>AirlinePNR</div>"
                my_table += "<div class='large-3 medium-3 small-6 columns  '>" & dtpnr.Rows(0)("AirlinePnr").ToString() & "&nbsp; </div>"
                my_table += "<div class='large-3 medium-3 small-6 columns  bld'>Status</div>"
                If dtpnr.Rows(0)("status").ToString().Trim.ToUpper = "CONFIRM" Then
                    my_table += "<div class='large-3 medium-3 small-6 columns  bld'>PNR ON HOLD</div>"
                Else
                    my_table += "<div class='large-3 medium-3 small-6  columns '>" & dtpnr.Rows(0)("status").ToString() & "</div>"
                End If

                my_table += "</div>"
                my_table += "<div class='clear1'></div>"


                my_table += "<div class='large-12 medium-12 small-12 bld'>Traveller Information</div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class=' large-4 medium-4 small-4   columns  bld'>Passenger Name</div>"
                my_table += "<div class=' large-4 medium-4 small-4   columns  bld'>Type</div>"
                my_table += "<div class=' large-4 medium-4 small-4   columns  bld'>Ticket No</div>"

                If TransTD = "" OrElse TransTD Is Nothing Then
                    For Each dr As DataRow In dtpax.Rows
                        my_table += "<div class='clear1'></div>"
                        my_table += "<div class='large-4 medium-4 small-4  columns '>" & dr("Name").ToString() & "</div>"
                        my_table += "<div class='large-4 medium-4 small-4  columns '>" & dr("PaxType").ToString() & "</div>"
                        If dtpnr.Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                            my_table += "<div class='large-4 medium-4 small-4  columns '>" & dr("TicketNumber").ToString() & "</div>"
                        Else
                            If InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-FQ") > 0 Then
                                my_table += "<div class='large-12 medium-12 small-12'>Pnr On Hold, Due to dynamic updation of fare and inventory class at airline’s end, the fare may have changed. Pls contact our call centre.</div>"
                            ElseIf InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-BLOCK") > 0 Then
                                my_table += "<div class='large-12 medium-12 small-12'>Pnr On Hold, Due to dynamic updation of fare and inventory class at airline’s end, the fare may have changed. Pls contact our call centre.</div>"
                            ElseIf InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-SPR") > 0 Then
                                my_table += "<div class='large-12 medium-12 small-12'>Pnr On Hold, Pls contact our call centre.</div>"
                            Else
                                my_table += "<div class='large-12 medium-12 small-12'>Pnr On Hold, We are processing your ticket details.</div>"
                            End If
                        End If
                        my_table += "<div class='clear1'></div>"
                    Next
                Else
                    For Each dr As DataRow In dtpax.Rows

                        my_table += "<div class='large-4 medium-4 small-4  columns '>" & dr("Name").ToString() & "</div>"
                        my_table += "<div class='large-4 medium-4 small-4  columns ' id='td_perpaxtype'>" & dr("PaxType").ToString() & "</div>"
                        my_table += "<div class='large-4 medium-4 small-4  columns '>" & dr("TicketNumber").ToString() & "</div>"

                    Next
                End If
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'><hr/></div>"
                my_table += "<div class='clear1'></div>"


                my_table += "<div  class='large-12 medium-12 small-12 bld'><u>Flight Information</u></div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div  class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld'>From - To</div>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld passenger'>&nbsp;</div>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld'>Depart Date</div>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld'>DepTime</div>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld'>ArrTime</div>"
                my_table += "<div class='large-2 medium-2 small-3 columns  bld passenger'>Aircraft</div>"
                my_table += "</div>"
                my_table += "<div class='clear1'></div>"
                For Each dr1 As DataRow In dtflight.Rows

                    '' Dim slctedDataRow As DataRow() = FltDs.Tables(0).Select("DepAirportCode='" & dr1("DFrom").ToString().Trim() & "' and  ArrAirportCode='" & dr1("ATo").ToString().Trim() & "' ")

                    my_table += "<div  class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-2 medium-2 small-3  columns '>" & dr1("DepAirName").ToString().Trim() & " (" & dr1("DFrom").ToString().Trim() & " ) - " & dr1("ArrAirName").ToString().Trim() & " (" & (dr1("ATo").ToString().Trim()) & ")</div>"
                    my_table += "<div class='large-2 medium-2 small-3 columns  passenger'>"
                    my_table += "<div  class='large-12 medium-12 small-12'> <img src='http://RWT.co/AirLogo/sm" & dr1("AirlineCode").ToString() & ".gif' /></div>"
                    my_table += "<div  class='large-12 medium-12 small-12'>" & dr1("AirlineName").ToString().Trim() & "(" & dr1("AirlineCode").ToString().Trim() & "-" & dr1("FltNumber").ToString() & ")</div>"
                    my_table += "</div>"
                    Dim depDate As String = ""
                    depDate = Left(dr1("DepDate").ToString, 2) & " " & datecon(Mid(dr1("DepDate").ToString, 3, 2)) & ", " & Right(dr1("DepDate").ToString, 2)
                    my_table += "<div class='large-2 medium-2 small-3  columns '>" & depDate & "</div>"
                    my_table += "<div class='large-2 medium-2 small-3  columns '>" & dr1("DepTime").ToString().Trim() & "Hrs</div>"
                    my_table += "<div class='large-2 medium-2 small-3  columns '>" & dr1("ArrTime").ToString().Trim() & "Hrs</div>"
                    my_table += "<div class='large-2 medium-2 small-3  columns passenger'>" & dr1("AirCraft").ToString().Trim() & "</div>"
                    my_table += "</div>"
                    my_table += "<div class='clear1'></div>"
                Next

                ''my_table += "<div class='clear1'></div>"
                'Added05/04/2014  'Start Adult/Child/Infant Fare Details
                Dim FinalTotal As Double = 0
                If TransTD = "" OrElse TransTD Is Nothing Then
                    Dim GrandTotal As Double = 0

                    my_table += "<div  class='large-12 medium-12 small-12'>Fare Information " & FareType & "</div>"
                    my_table += "<div  class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Pax Detail</div>"
                    my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Base Fare</div>"
                    my_table += "<div class='large-1 medium-2 small-2 columns  bld'>Fuel Surcharge</div>"
                    my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Tax</div>"

                    my_table += "<div class='large-1 medium-1 small-1 columns  bld'>STax</div>"
                    If MgtFeeVisibleStatus = True Then
                        my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Mgt Fee</div>"

                    Else
                        my_table += "<div class='large-1 medium-2 small-2 columns  bld'>Trans Fee</div>"
                        my_table += "<div class='large-1 medium-2 small-2 columns  bld'>Trans Charge</div>"
                    End If


                    my_table += "<div class='large-1 medium-2 small-2 columns  bld'>TOTAL</div>"

                    my_table += "</div>"
                    my_table += "<div class='clear1'></div>"
                    For Each dr2 As DataRow In dtfare.Rows
                        If dr2("PaxType").ToString() = "ADT" Then
                            Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                            Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                            Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                            Dim ServiceTax As Double = 0
                            Dim TCharge As Double = 0
                            If IsCorp = True Then
                                ServiceTax = 0

                            Else

                                ServiceTax = (Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()))
                                TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                            End If
                            Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())

                            Dim mgtfee1 As Double = 0
                            If MgtFeeVisibleStatus = True Then mgtfee1 = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())

                            Dim Total As Double = 0
                            If (IsCorp = True) Then
                                Total = (BaseFare + Fuel + Tax)
                            Else

                                Total = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1

                            End If
                            my_table += "<div  class='large-12 medium-12 small-12'>"
                            my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_paxtype'>" & dr2("PaxType").ToString() & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                            my_table += "<div class='large-1 medium-2 small-2  columns '>" & Fuel & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_taxadt'>" & Tax & "</div>"

                            'If (IsCorp = True) Then
                            '    my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>0</td>"
                            'Else
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"
                            ' End If

                            If MgtFeeVisibleStatus = True Then
                                my_table += "<div class='large-2 medium-2 small-2  columns '>" & mgtfee1.ToString() & "</div>"

                            Else
                                my_table += "<div class='large-1 medium-2 small-2  columns '>" & TFee & "</div>"
                                my_table += "<div class='large-1 medium-2 small-2  columns ' id='td_tcadt'>" & TCharge & "</div>"

                            End If


                            my_table += "<div class='large-1 medium-2 small-2  columns ' id='td_adttot'>" & Total & "</div>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                            End If



                            my_table += "</div>"
                            my_table += "<div class='clear1'></div>"
                        End If
                        If dr2("PaxType").ToString() = "CHD" Then
                            Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                            Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                            Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                            Dim ServiceTax As Double = 0
                            Dim TCharge As Double = 0
                            If IsCorp = True Then
                                ServiceTax = 0
                            Else

                                ServiceTax = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                                TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                            End If

                            Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                            Dim mgtfee1 As Double = 0
                            If MgtFeeVisibleStatus = True Then mgtfee1 = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())


                            Dim Total As Double = 0
                            If (IsCorp = True) Then
                                Total = (BaseFare + Fuel + Tax)
                            Else

                                Total = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1

                            End If
                            my_table += "<div  class='large-12 medium-12 small-12'>"
                            my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_paxtype'>" & dr2("PaxType").ToString() & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                            my_table += "<div class='large-1 medium-2 small-2  columns '>" & Fuel & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_taxchd'>" & Tax & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"
                            If MgtFeeVisibleStatus = True Then
                                my_table += "<div class='large-2 medium-2 small-2  columns '>" & mgtfee1.ToString() & "</div>"

                            Else
                                my_table += "<div class='large-1 medium-2 small-2  columns '>" & TFee & "</div>"
                                my_table += "<div class='large-1 medium-2 small-2  columns ' id='td_tcchd'>" & TCharge & "</div>"

                            End If



                            my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_chdtot'>" & Total & "</div>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                'Added 05/04/2014
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1
                            End If

                            my_table += "</div>"
                            my_table += "<div class='clear1'></div>"
                        End If
                        If dr2("PaxType").ToString() = "INF" Then
                            Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            Dim ServiceTax As Double = 0

                            If IsCorp = True Then
                                ServiceTax = 0
                            Else
                                ServiceTax = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            End If

                            Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            Dim mgtfee1 As Double = 0
                            Dim TCharge As Double = 0
                            If MgtFeeVisibleStatus = True Then mgtfee1 = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())



                            Dim Total As Double = 0
                            If (IsCorp = True) Then
                                Total = (BaseFare + Fuel + Tax)
                            Else

                                Total = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString()) + mgtfee1
                                TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                            End If
                            my_table += "<div class='large-12 medium-12 small-12'>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr2("PaxType").ToString() & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                            my_table += "<div class='large-1 medium-2 small-2  columns '>" & Fuel & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & Tax & "</div>"
                            my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"

                            If MgtFeeVisibleStatus = True Then
                                my_table += "<div class='large-2 medium-2 small-2  columns '>" & mgtfee1.ToString() & "</div>"

                            Else
                                my_table += "<div class='large-1 medium-2 small-2  columns '>" & TFee & "</div>"
                                my_table += "<div class='large-1 medium-2 small-2  columns '>" & TCharge & "</div>"

                            End If


                            my_table += "<div class='large-1 medium-2 small-2  columns '>" & Total & "</div>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString()) + mgtfee1
                            End If


                            my_table += "</div>"
                            my_table += "<div class='clear1'></div>"

                        End If
                    Next
                    my_table += "<div class='large-12 medium-12 small-12'>"

                    my_table += "<div class='large-1 medium-2 small-2 large-push-8 medium-push-8 small-push-8 columns  bld blue'>GRAND TOTAL </div>"
                    my_table += "<div class='large-1 medium-2 small-2 columns  bld' id='td_grandtot'>" & GrandTotal & "</div>"

                    my_table += "</div>"
                    my_table += "</div>"
                    my_table += "<div class='clear1'></div>"
                    'my_table += "</table>"
                    'my_table += "</td>"

                    'my_table += "</tr>"
                Else

                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-12 medium-12 small-12 bld blue'>Fare Information " & FareType & "</div>"

                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Base Fare</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("BaseFare").ToString() & "</div>"
                    my_table += "</div>"
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Fuel Surcharge</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("Fuel").ToString() & "</div>"
                    my_table += "</div>"
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Tax</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtax'>" & dtfare.Rows(0)("Tax").ToString() & "</div>"
                    my_table += "</div>"


                    Dim BFare As Double = Convert.ToDouble(dtfare.Rows(0)("BaseFare").ToString())
                    Dim Fuel As Double = Convert.ToDouble(dtfare.Rows(0)("Fuel").ToString())
                    Dim Tax As Double = Convert.ToDouble(dtfare.Rows(0)("Tax").ToString())
                    Dim Total As Double = BFare + Fuel + Tax
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>TOTAL</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtot'>" & Total & "</div>"
                    my_table += "</div>"
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Service Tax</div>"
                    If (IsCorp = True) Then
                        my_table += "<div class='large-6 medium-6 small-6  columns '>0</div>"
                    Else
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("ServiceTax").ToString() & "</div>"
                    End If

                    my_table += "</div>"


                    If MgtFeeVisibleStatus = True Then


                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Management Fee</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtc'>" & mgtFee.ToString() & "</div>"
                        my_table += "</div>"

                    Else

                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Transaction Fee</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("TFee").ToString() & "</div>"
                        my_table += "</div>"
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Transaction Charge</div>"
                        If (IsCorp = True) Then
                            my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtc'>0</div>"
                        Else
                            my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtc'>" & dtfare.Rows(0)("TCharge").ToString() & "</div>"
                        End If

                        my_table += "</div>"

                    End If

                    Dim MealBg As Double = 0, MC = 0, BC = 0
                    If MBDT.Tables(0).Rows.Count > 0 Then
                        MC = Convert.ToDouble(MBDT.Tables(0).Rows(0)("MealPrice").ToString())
                        BC = Convert.ToDouble(MBDT.Tables(0).Rows(0)("BaggagePrice").ToString())
                        MealBg = MC + BC

                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Meal Charges</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtax'>" & MC.ToString() & "</div>"
                        my_table += "</div>"

                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Baggage Charges</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtax'>" & BC.ToString() & "</div>"
                        my_table += "</div>"

                    End If




                    Dim ResuCharge As Double, ResuServiseCharge As Double, ResuFareDiff As Double = 0
                    If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

                        ResuCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuCharge").ToString())
                        ResuServiseCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuServiseCharge").ToString())
                        ResuFareDiff = Convert.ToDouble(dtpnr.Rows(0)("ResuFareDiff").ToString())
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Charge</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuCharge & "</div>"
                        my_table += "</div>"

                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Srv. Charge</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuServiseCharge & "</div>"
                        my_table += "</div>"

                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Fare Diff.</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuFareDiff & "</div>"
                        my_table += "</div>"

                    End If




                    Dim STax As Double = Convert.ToDouble(dtfare.Rows(0)("ServiceTax").ToString())
                    Dim TFee As Double = Convert.ToDouble(dtfare.Rows(0)("TFee").ToString())
                    Dim TCharge As Double = Convert.ToDouble(dtfare.Rows(0)("TCharge").ToString())
                    Dim GrandTotal As Double = 0
                    If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then
                        GrandTotal = Total + STax + If(MgtFeeVisibleStatus = False, (TFee + TCharge), 0) + ResuCharge + ResuServiseCharge + ResuFareDiff + mgtFee + MealBg

                    Else
                        GrandTotal = Total + STax + If(MgtFeeVisibleStatus = False, (TFee + TCharge), 0) + mgtFee + MealBg

                    End If

                    If (IsCorp = True) Then
                        GrandTotal = Total
                    End If
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6 columns  bld'>GRAND TOTAL</div>"
                    my_table += "<div class='large-6 medium-6 small-6 columns  bld' id='td_perpaxgrandtot'>" & GrandTotal & "</div>"
                    my_table += "</div>"
                    my_table += "</div>"
                    my_table += "<div class='clear1'></div>"
                End If
                'Fare of Adt/child/Infant Complete

                ' Added 05/04/2014 ''Begin Code For Meal And bagage Detail
                If TransTD = "" OrElse TransTD Is Nothing Then
                    If FltDs.Tables(0).Rows(0)("Provider").ToString().Trim().ToUpper() = "TB" Then
                        If (dtpnr.Rows(0)("TripType") = "R") Then
                            my_table += Meal_BagDetails(OrderId, "", FltDs.Tables(0).Rows(0)("Provider").ToString().Trim().ToUpper(), "O", FinalTotal, "OutBound")
                            my_table += Meal_BagDetails(OrderId, "", FltDs.Tables(0).Rows(0)("Provider").ToString().Trim().ToUpper(), "R", FinalTotal, "InBound")
                        Else
                            my_table += Meal_BagDetails(OrderId, "", FltDs.Tables(0).Rows(0)("Provider").ToString().Trim().ToUpper(), "O", FinalTotal, "OutBound")
                        End If

                    Else

                        If (dtflight.Rows(0)("AirlineCode") = "SG" Or dtflight.Rows(0)("AirlineCode") = "6E") Then
                            If (dtpnr.Rows(0)("TripType") = "R") Then
                                my_table += Meal_BagDetails(OrderId, "", dtflight.Rows(0)("AirlineCode"), "O", FinalTotal, "OutBound")
                                my_table += Meal_BagDetails(OrderId, "", dtflight.Rows(0)("AirlineCode"), "R", FinalTotal, "InBound")
                            Else
                                my_table += Meal_BagDetails(OrderId, "", dtflight.Rows(0)("AirlineCode"), "O", FinalTotal, "OutBound")
                            End If
                        End If

                    End If

                Else

                End If


                'my_table += "<tr>"
                'If TransTD = "" OrElse TransTD Is Nothing Then
                '    my_table += "<td>"
                'Else
                '    my_table += "<td>"
                'End If

                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-12 medium-12 small-12'> Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.</br>For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div class='large-12 medium-12 small-12 bld blue'><u>TERMS AND CONDITIONS :</u></div>"

                my_table += "<div class='clear'></div>"

                my_table += "<div class='large-12 medium-12 small-12'> <b>1.</b> Guests are requested to carry their valid photo identification for all guests, including children</div>"



                my_table += "<div class='large-12 medium-12 small-12'><b>2.</b> We recommend check-in at least 2 hours prior to departure.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>3.</b>  Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>4.</b> Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>5.</b>  Flight schedules are subject to change and approval by authorities.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>6.</b> In case of ticket Cancellation the transaction fee will not be refunded.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>7.</b> Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>8.</b>  Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>9.</b> Bookings made under the Armed Forces quota are non cancelable and non- changeable.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>10.</b> Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>11.</b> Cancellation amount will be charged as per airline rule.</div>"


                my_table += "<div class='large-12 medium-12 small-12'> <b>12.</b> Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</div>"

                my_table += "</div>"
                my_table += "<div class='clear1'></div>"

                'For Baggage Information
                If dtpnr.Rows(0)("Trip").ToString.ToUpper = "D" Then
                    my_table += "<div class='large-12 medium-12 small-12 bld blue'><u>BAGGAGE INFORMATION :</u></div>"

                    my_table += "<div class='clear1'></div>"

                    'For Domestic

                    'my_table += "<tr>"
                    'my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91;font-weight:bold;padding-top:10px'>Baggage allowance of domestic airlines :</div>"
                    'my_table += "</tr>"



                    my_table += "<div class='large-12 medium-12 small-12'>"


                    my_table += "<div class='large-6 medium-6 small-6 columns  bld'>Airline</div>"
                    my_table += "<div class='large-6 medium-6 small-6 columns  bld'>Weight</div>"
                    my_table += "<div class='clear'></div>"

                    Dim Bag As Boolean = False
                    If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                        Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
                    End If
                    Dim dtbaggage As New DataTable
                    dtbaggage = STDom.GetBaggageInformation("D", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), Bag).Tables(0)
                    For Each drbg In dtbaggage.Rows

                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & drbg("BaggageName") & "</div>"
                        my_table += "<div class='large-6 medium-6 small-6  columns '>" & drbg("Weight") & "</div>"

                        my_table += "<div class='clear'></div>"


                    Next



                    my_table += "</div>"

                End If
                'End For Baggage Information

                my_table += "</div>"
                my_table += "<div class='clear1'></div>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</table>"
            End If

            Return my_table

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try



    End Function
    Public Function datecon(ByVal MM As String)
        Dim mm_str As String = ""
        Select Case MM
            Case "01"
                mm_str = "JAN"
            Case "02"
                mm_str = "FEB"
            Case "03"
                mm_str = "MAR"
            Case "04"
                mm_str = "APR"
            Case "05"
                mm_str = "MAY"
            Case "06"
                mm_str = "JUN"
            Case "07"
                mm_str = "JUL"
            Case "08"
                mm_str = "AUG"
            Case "09"
                mm_str = "SEP"
            Case "10"
                mm_str = "OCT"
            Case "11"
                mm_str = "NOV"
            Case "12"
                mm_str = "DEC"
            Case Else

        End Select

        Return mm_str

    End Function
    Public Function FareDetail(ByVal OrderId As String, ByVal TransTD As String) As String
        Try

            Dim dtpnr As New DataTable()
            dtpnr = ObjIntDetails.SelectHeaderDetail(OrderId)

            Dim dtpax As New DataTable()
            dtpax = ObjIntDetails.SelectPaxDetail(OrderId, TransTD)

            Dim dtagentid As New DataTable()
            dtagentid = ObjIntDetails.SelectAgent(OrderId)

            Dim dtagency As New DataTable()
            dtagency = ObjIntDetails.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())

            Dim dtflight As New DataTable()
            dtflight = ObjIntDetails.SelectFlightDetail(OrderId)

            Dim dtfare As New DataTable()
            dtfare = ObjIntDetails.SelectFareDetail(OrderId, TransTD)

            Dim dtadtcount As New DataTable()
            dtadtcount = ObjIntDetails.CountADT(OrderId)

            Dim dtchdcount As New DataTable()
            dtchdcount = ObjIntDetails.CountCHD(OrderId)

            Dim dtinfcount As New DataTable()
            dtinfcount = ObjIntDetails.CountINF(OrderId)
            Dim IsCorp As Boolean = HttpContext.Current.Session("IsCorp")
            Dim my_table As String = ""
            Dim FareType As String = ""
            If (dtfare.Rows(0)("FareType") <> "") Then
                FareType = "(" & dtfare.Rows(0)("FareType") & ")"
            End If

            If TransTD = "" OrElse TransTD Is Nothing Then
                Dim GrandTotal As Double = 0

                my_table += "<tr>"
                my_table += "<td colspan='3' class='bld'>Fare Information " & FareType & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Pax Detail</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Base Fare</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Fuel Surcharge</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Tax</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>STax</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Trans Fee</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Trans Charge</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>TOTAL</div>"
                my_table += "</div>"
                my_table += "<div class='clear1'></div>"
                For Each dr2 As DataRow In dtfare.Rows
                    If dr2("PaxType").ToString() = "ADT" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim TCharge As Double = 0
                        If (IsCorp = False) Then
                            TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())

                        End If
                        Dim Total As Double = Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr2("PaxType").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Fuel & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_taxadt'>" & Tax & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & TFee & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns ' id='td_tcadt'>" & TCharge & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Total & "</div>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        my_table += "</div>"
                        my_table += "<div class='clear1'></div>"
                    End If
                    If dr2("PaxType").ToString() = "CHD" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())

                        Dim TCharge As Double = 0
                        If (IsCorp = False) Then
                            TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())

                        End If
                        Dim Total As Double = Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr2("PaxType").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Fuel & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns ' id='td_taxchd'>" & Tax & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & TFee & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns ' id='td_tcchd'>" & TCharge & "</div>"

                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Total & "</div>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        my_table += "</div>"
                        my_table += "<div class='clear1'></div>"
                    End If
                    If dr2("PaxType").ToString() = "INF" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())

                        Dim TCharge As Double = 0
                        If (IsCorp = False) Then
                            TCharge = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())

                        End If
                        Dim Total As Double = (Convert.ToDouble(dr2("Total").ToString())) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr2("PaxType").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & BaseFare & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Fuel & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & Tax & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & ServiceTax & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & TFee & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & TCharge & "</div>"

                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & Total & "</div>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        my_table += "</div>"

                    End If
                Next
                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-1 medium-1 small-1 large-push-8 medium-push-8 small-push-8 columns  bld blue'>GRAND TOTAL</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld' id='td_grandtot'>" & GrandTotal & "</div>"

                my_table += "<div>"
                my_table += "</div>"
                my_table += "<div class='clear1'></div>"
                'my_table += "</td>"
                'my_table += "</tr>"


                'my_table += "</table>"
                'my_table += "</td>"

                'my_table += "</tr>"
            Else
                'my_table += "<tr>"
                'my_table += "<td>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-12 medium-12 small-12 bld blue'>Fare Information " & FareType & "</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Base Fare</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("BaseFare").ToString() & "</div>"
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Fuel Surcharge</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("Fuel").ToString() & "</div>"
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Tax</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtax'>" & dtfare.Rows(0)("Tax").ToString() & "</div>"
                my_table += "</div>"

                Dim BFare As Double = Convert.ToDouble(dtfare.Rows(0)("BaseFare").ToString())
                Dim Fuel As Double = Convert.ToDouble(dtfare.Rows(0)("Fuel").ToString())
                Dim Tax As Double = Convert.ToDouble(dtfare.Rows(0)("Tax").ToString())
                Dim Total As Double = BFare + Fuel + Tax
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>TOTAL</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>" & Total & "</div>"
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Service Tax</div>"
                If (IsCorp = True) Then
                    my_table += "<div class='large-6 medium-6 small-6  columns '>0</div>"

                Else
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("ServiceTax").ToString() & "</div>"

                End If
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Transaction Fee</div>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>" & dtfare.Rows(0)("TFee").ToString() & "</div>"
                my_table += "</div>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6  columns '>Transaction Charge</div>"
                If (IsCorp = True) Then
                    my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtc'>0</div>"
                Else
                    my_table += "<div class='large-6 medium-6 small-6  columns ' id='td_perpaxtc'>" & dtfare.Rows(0)("TCharge").ToString() & "</div>"
                End If

                my_table += "</div>"


                Dim ResuCharge As Double, ResuServiseCharge As Double, ResuFareDiff As Double = 0
                If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

                    ResuCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuCharge").ToString())
                    ResuServiseCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuServiseCharge").ToString())
                    ResuFareDiff = Convert.ToDouble(dtpnr.Rows(0)("ResuFareDiff").ToString())
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Charge</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuCharge & "</div>"
                    my_table += "</div>"

                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Srv. Charge</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuServiseCharge & "</div>"
                    my_table += "</div>"

                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>Reissue Fare Diff.</div>"
                    my_table += "<div class='large-6 medium-6 small-6  columns '>" & ResuFareDiff & "</div>"
                    my_table += "</div>"

                End If




                Dim STax As Double = Convert.ToDouble(dtfare.Rows(0)("ServiceTax").ToString())
                Dim TFee As Double = Convert.ToDouble(dtfare.Rows(0)("TFee").ToString())
                Dim TCharge As Double = Convert.ToDouble(dtfare.Rows(0)("TCharge").ToString())
                Dim GrandTotal As Double = 0
                If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then
                    GrandTotal = Total + STax + TFee + TCharge + ResuCharge + ResuServiseCharge + ResuFareDiff
                Else
                    GrandTotal = Total + STax + TFee + TCharge
                End If
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-6 medium-6 small-6 columns  bld'>GRAND TOTAL</div>"
                my_table += "<div class='large-6 medium-6 small-6 columns  bld' id='td_perpaxgrandtot'>" & GrandTotal & "</div>"
                my_table += "</div>"
                my_table += "</div>"
                my_table += "<div class='clear1'></div>"
                'my_table += "</td>"

                'my_table += "</tr>"

            End If
            Return my_table



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Function

    Public Function Meal_BagDetails(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal TT As String, ByRef FinalTotal As Double, ByVal HD As String) As String
        Dim my_table As String = ""
        Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, VC, TT)
        Dim DtPxMB As DataTable = dtfare1.Tables(0)
        Dim mbfare As Array = dtfare1.Tables(0).Select("MPRICE>0 or BPRICE>0", "")
        If DtPxMB.Rows.Count > 0 Then
            If mbfare.Length > 0 Then
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-12 medium-12 small-12 bld blue'>Meals Bagagae Fare Information" + HD + "</div>"
                my_table += "<div class='clear1'></div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Pax Name</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Type</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Meal Details</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Meal Price</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Bag Details</div>"
                my_table += "<div class='large-2 medium-2 small-2 columns  bld'>Bag Price</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Ticket No</div>"
                my_table += "<div class='large-1 medium-1 small-1 columns  bld'>Total</div>"
                my_table += "</div>"
                my_table += "<div class='clear'></div>"
                'If TransTD = "" OrElse TransTD Is Nothing Then
                For Each dr As DataRow In DtPxMB.Rows
                    If (Convert.ToDouble(dr("MPRICE").ToString()) > 0 OrElse Convert.ToDouble(dr("BPRICE").ToString()) > 0) Then
                        my_table += "<div class='large-12 medium-12 small-12'>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr("Name").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr("PaxType").ToString() & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & If(VC = "TB", If(String.IsNullOrEmpty(dr("MDisc")) <> True, dr("MDisc").ToString().Split("_")(2), ""), dr("MDisc").ToString()) & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & dr("MPRICE").ToString() & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & If(VC = "TB", If(String.IsNullOrEmpty(dr("BDisc")) <> True, dr("BDisc").ToString().Split("_")(2) & " KG", ""), dr("BDisc").ToString()) & "</div>"
                        my_table += "<div class='large-2 medium-2 small-2  columns '>" & dr("BPRICE").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & dr("TicketNumber").ToString() & "</div>"
                        my_table += "<div class='large-1 medium-1 small-1  columns '>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</div>"

                        my_table += "</div>"
                        my_table += "<div class='clear1'></div>"
                        FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
                    End If
                Next
                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-2 medium-2 small-2  columns '>FINAL TOTAL </div>"
                my_table += "<div class='large-2 medium-2 small-2  columns ' id='td_finaltot'>" & FinalTotal & "</div>"
                my_table += "</div>"
                my_table += "</div>"
                my_table += "</td>"
                my_table += "</tr>"
            End If
        End If

        Return my_table

    End Function

End Class
