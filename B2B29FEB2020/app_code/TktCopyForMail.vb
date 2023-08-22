Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Linq

Public Class TktCopyForMail

    Private ObjIntDetails As New IntlDetails()
    Private STDom As New SqlTransactionDom()
    Dim objSql As New SqlTransactionNew
    Public Function TicketDetail(ByVal OrderId As String, ByVal TransTD As String, ByVal charge As Integer, ByVal Chargetype As String) As String
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

            'Added Meal-Bag 04/05/2014
            Dim objDA As New SqlTransaction
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

            If dtpnr.Rows(0)("Status").ToString().Trim().ToLower() = "rejected" Then

                my_table += "<div class='large-12 medium-12 small-12'>"

                my_table += "<div class='large-8 medium-8 small-12 large-push-2 medium-push-2  bld blue center'><u>Booking Reference No. " & OrderId & "</u></div>"
                my_table += "<div class='clear1'></div>"

                my_table += "<div class='large-12 medium-12 small-12'>Booking Failure</div>"

                my_table += "<div class='large-12 medium-12 small-12'>Please re-try the booking.Your booking has been rejected due to some technical issuse in airline. We have credited the booking amount to your account. </div>"

                my_table += "</div>"

            Else



                my_table = "<table width='100%' border='0'  cellspacing='0'  cellpadding='0'>"

                my_table += "<tr style='display:none;'>"
                my_table += "<td id='td_adtcnt'>" & dtadtcount.Rows(0)("Counting") & "</td>"
                my_table += "<td id='td_chdcnt'>" & dtchdcount.Rows(0)("Counting") & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 18px; font-weight: bold; color: #0000cc; height: 20px; padding-right: 10px; padding-left: 20px'>Electronic Ticket</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td colspan='2' align='center'   style='background-color: #004b91;  color: #fff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px;border: thin solid #000000'>Booking Reference No. " & OrderId & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='border: thin solid #999999;padding-left:20px; width:100%'>"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%'  >"
                'my_table += "<tr>"
                'my_table += "<td colspan='2'   style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Agency Information</td>"
                'my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td valign='top' background='http://RWT.co/images/nologo.png' style='background-repeat: no-repeat;width:100px;height:70px;'></td> " 'for agency logo
                my_table += "<td style='padding:10px' align='right' >"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' >"

                my_table += "<tr>"
                my_table += "<td  align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 14px;font-weight:bold; color: #000000; height: 20px;padding-left:20px'>Agency Information</td>"
                'my_table += "<td> <img src='CSS/images/logo.png' /></td>";
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:20px'>" & dtagency.Rows(0)("Agency_Name").ToString() & "</td>"
                'my_table += "<td> <img src='CSS/images/logo.png' /></td>";
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:20px'>" & dtagency.Rows(0)("Address").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:20px'>" & dtagency.Rows(0)("Address1").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:20px'>" & dtagency.Rows(0)("Mobile").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:20px'>" & dtagency.Rows(0)("Email").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='padding: 10px;border: thin solid #999999'>"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td  style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>GDSPNR</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtpnr.Rows(0)("GdsPnr").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Booking Date</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtpnr.Rows(0)("CreateDate").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>AirlinePNR</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtpnr.Rows(0)("AirlinePnr").ToString() & " </td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Status</td>"
                If dtpnr.Rows(0)("status").ToString().Trim.ToUpper = "CONFIRM" Then
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>PNR ON HOLD</td>"
                Else
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtpnr.Rows(0)("status").ToString() & "</td>"
                End If
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='border: thin solid #999999;' >"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td colspan='3'  style='background-color: ##004b91; color: #000; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;'>Traveller Information</td>"
                my_table += "</tr>"
                my_table += "<tr >"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Passenger Name</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Type</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Ticket No</td>"
                my_table += "</tr>"
                If TransTD = "" OrElse TransTD Is Nothing Then
                    For Each dr As DataRow In dtpax.Rows
                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("Name").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' >" & dr("PaxType").ToString() & "</td>"
                        If dtpnr.Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr("TicketNumber").ToString() & "</td>"
                        Else
                            If InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-FQ") > 0 Then
                                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>Pnr On Hold, Due to dynamic updation of fare and inventory class at airline’s end, the fare may have changed. Pls contact our call centre.</td>"
                            ElseIf InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-BLOCK") > 0 Then
                                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>Pnr On Hold, Due to dynamic updation of fare and inventory class at airline’s end, the fare may have changed. Pls contact our call centre.</td>"
                            ElseIf InStr(dtpnr.Rows(0)("GdsPnr").ToString(), "-SPR") > 0 Then
                                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>Pnr On Hold, Pls contact our call centre.</td>"
                            Else
                                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>Pnr On Hold, We are processing your ticket details.</td>"
                            End If
                        End If
                        my_table += "</tr>"
                    Next
                Else
                    For Each dr As DataRow In dtpax.Rows
                        my_table += "<tr >"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("Name").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_perpaxtype'>" & dr("PaxType").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr("TicketNumber").ToString() & "</td>"
                        my_table += "</tr>"
                    Next
                End If
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='border: thin solid #999999' >"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td colspan='6'  style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Flight Information</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>From - To</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px; '></td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Depart Date</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>DepTime</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>ArrTime</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>Aircraft</td>"



                my_table += "</tr>"
                For Each dr1 As DataRow In dtflight.Rows
                    my_table += "<tr>"
                    my_table += "<td valign='bottom' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px'>" & dr1("DepAirName").ToString() & " (" & dr1("DFrom").ToString() & " ) - " & dr1("ArrAirName").ToString() & " (" & (dr1("ATo").ToString()) & ")</td>"
                    my_table += "<td valign='bottom'>"
                    my_table += "<table>"
                    my_table += "<tr>"
                    my_table += "<td> <img src='http://RWT.co/AirLogo/sm" & dr1("AirlineCode").ToString() & ".gif' /></td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("AirlineName").ToString() & "(" & dr1("AirlineCode").ToString() & "-" & dr1("FltNumber").ToString() & ")</td>"
                    my_table += "</tr>"
                    my_table += "</table>"
                    my_table += "</td>"
                    Dim depDate As String = ""
                    depDate = Left(dr1("DepDate").ToString, 2) & " " & datecon(Mid(dr1("DepDate").ToString, 3, 2)) & ", " & Right(dr1("DepDate").ToString, 2)
                    my_table += "<td valign='bottom' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & depDate & "</td>"
                    my_table += "<td valign='bottom' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("DepTime").ToString() & "Hrs</td>"
                    my_table += "<td valign='bottom' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("ArrTime").ToString() & "Hrs</td>"
                    my_table += "<td valign='bottom' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("AirCraft").ToString() & "</td>"
                    my_table += "</tr>"
                Next
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                'Added05/04/2014  'Start Adult/Child/Infant Fare Details
                Dim FinalTotal As Double = 0
                If TransTD = "" OrElse TransTD Is Nothing Then
                    Dim GrandTotal As Double = 0
                    my_table += "<tr>"
                    my_table += "<td colspan='3' style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Fare Information " & FareType & "</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='border: thin solid #999999'>"
                    my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                    my_table += "<tr>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Pax Detail</td>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Base Fare</td>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Fuel Surcharge</td>"
                    my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>Tax</td>"

                    my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>STax</td>"
                    If MgtFeeVisibleStatus = True Then
                        my_table += "<td colspan='3'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>Mgt Fee</td>"

                    Else
                        my_table += "<td colspan='2' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Fee</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Charge</td>"
                    End If


                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>TOTAL</td>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px''></td>"
                    my_table += "</tr>"
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
                            my_table += "<tr>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px ' id='td_paxtype'>" & dr2("PaxType").ToString() & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                            my_table += "<td id='tdadttax' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_taxadt'>" & Tax & "</td>"

                            'If (IsCorp = True) Then
                            '    my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>0</td>"
                            'Else
                            my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                            ' End If

                            If MgtFeeVisibleStatus = True Then
                                my_table += "<td  colspan='3'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                            Else
                                my_table += "<td colspan='2' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                                my_table += "<td id='tdadttc' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_tcadt'>" & TCharge & "</td>"

                            End If


                            my_table += "<td id='tdadttl' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold' id='td_adttot'>" & Total & "</td>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                            End If



                            my_table += "</tr>"
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
                            my_table += "<tr>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_paxtype'>" & dr2("PaxType").ToString() & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                            my_table += "<td id='tdchdtax' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_taxchd'>" & Tax & "</td>"
                            my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                            If MgtFeeVisibleStatus = True Then
                                my_table += "<td colspan='3'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                            Else
                                my_table += "<td colspan='2' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                                my_table += "<td id='tdchdtc' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_tcchd'>" & TCharge & "</td>"

                            End If



                            my_table += "<td id='tdchdtl'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold' id='td_chdtot'>" & Total & "</td>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                'Added 05/04/2014
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1
                            End If

                            my_table += "</tr>"
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
                            my_table += "<tr>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                            my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Tax & "</td>"
                            my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"

                            If MgtFeeVisibleStatus = True Then
                                my_table += "<td colspan='3'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                            Else
                                my_table += "<td colspan='2' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TCharge & "</td>"

                            End If


                            my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                            If (IsCorp = True) Then
                                GrandTotal += Total
                                FinalTotal += Total 'Added 05/04/2014
                            Else
                                GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString()) + mgtfee1
                                FinalTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString()) + mgtfee1
                            End If


                            my_table += "</tr>"

                        End If
                    Next
                    my_table += "<tr style='background-color: #004b91;'>"
                    my_table += "<td   align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                    my_table += "<td   colspan='7' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #FFFF99; height: 20px' >GRAND TOTAL </td>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #FFFFFF; height: 20px; ' id='tdgrandtot'    >" & GrandTotal & "</td>"
                    my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
                    my_table += "</tr>"
                    my_table += "</table>"
                    my_table += "</td>"
                    my_table += "</tr>"


                    'my_table += "</table>"
                    'my_table += "</td>"

                    'my_table += "</tr>"
                Else
                    my_table += "<tr>"
                    my_table += "<td style='border: thin solid #999999' >"
                    my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                    my_table += "<tr>"
                    my_table += "<td colspan='3' style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Fare Information " & FareType & "</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px;padding-left:10px'>Base Fare</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("BaseFare").ToString() & "</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Fuel Surcharge</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("Fuel").ToString() & "</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td id='tdtax' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Tax</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='tdperpaxtax'>" & IIf(Chargetype = "TAX", Convert.ToDecimal(Convert.ToDecimal(dtfare.Rows(0)("Tax").ToString()) + charge), dtfare.Rows(0)("Tax").ToString()) & "</td>"
                    my_table += "</tr>"


                    Dim BFare As Double = Convert.ToDouble(dtfare.Rows(0)("BaseFare").ToString())
                    Dim Fuel As Double = Convert.ToDouble(dtfare.Rows(0)("Fuel").ToString())
                    Dim Tax As Double = Convert.ToDouble(dtfare.Rows(0)("Tax").ToString())
                    Dim Total As Double = BFare + Fuel + Tax + charge
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;padding-left:10px'>TOTAL</td>"
                    my_table += "<td id='tdTL' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Service Tax</td>"
                    If (IsCorp = True) Then
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>0</td>"
                    Else
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("ServiceTax").ToString() & "</td>"
                    End If

                    my_table += "</tr>"


                    If MgtFeeVisibleStatus = True Then


                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Management Fee</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='tdperpaxtcmgmt'>" & mgtFee.ToString() & "</td>"
                        my_table += "</tr>"

                    Else

                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Transaction Fee</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("TFee").ToString() & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Transaction Charge</td>"
                        If (IsCorp = True) Then
                            my_table += "<td id='tdtc' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>0</td>"
                        Else
                            my_table += "<td id='tdtc' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & IIf(Chargetype = "TC", Convert.ToDecimal(Convert.ToDecimal(dtfare.Rows(0)("TCharge").ToString()) + charge), dtfare.Rows(0)("TCharge").ToString()) & "</td>"
                        End If

                        my_table += "</tr>"

                    End If

                    Dim MealBg As Double = 0, MC = 0, BC = 0
                    If MBDT.Tables(0).Rows.Count > 0 Then
                        MC = Convert.ToDouble(MBDT.Tables(0).Rows(0)("MealPrice").ToString())
                        BC = Convert.ToDouble(MBDT.Tables(0).Rows(0)("BaggagePrice").ToString())
                        MealBg = MC + BC

                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Meal Charges</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='tdperpaxtaxmc'>" & MC.ToString() & "</td>"
                        my_table += "</tr>"

                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Baggage Charges</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='tdperpaxtaxbc'>" & BC.ToString() & "</td>"
                        my_table += "</tr>"

                    End If




                    Dim ResuCharge As Double, ResuServiseCharge As Double, ResuFareDiff As Double = 0
                    If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

                        ResuCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuCharge").ToString())
                        ResuServiseCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuServiseCharge").ToString())
                        ResuFareDiff = Convert.ToDouble(dtpnr.Rows(0)("ResuFareDiff").ToString())
                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Charge</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuCharge & "</td>"
                        my_table += "</tr>"

                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Srv. Charge</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuServiseCharge & "</td>"
                        my_table += "</tr>"

                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Fare Diff.</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuFareDiff & "</td>"
                        my_table += "</tr>"

                    End If




                    Dim STax As Double = Convert.ToDouble(dtfare.Rows(0)("ServiceTax").ToString())
                    Dim TFee As Double = Convert.ToDouble(dtfare.Rows(0)("TFee").ToString())
                    Dim TCharge As Double = Convert.ToDouble(dtfare.Rows(0)("TCharge").ToString())
                    Dim GrandTotal As Double = 0
                    If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then
                        GrandTotal = Total + STax + If(MgtFeeVisibleStatus = False, (TFee + TCharge), 0) + ResuCharge + ResuServiseCharge + ResuFareDiff + mgtFee + MealBg + charge


                    Else
                        GrandTotal = Total + STax + If(MgtFeeVisibleStatus = False, (TFee + TCharge), 0) + mgtFee + MealBg + charge

                    End If

                    If (IsCorp = True) Then
                        GrandTotal = Total
                    End If
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;padding-left:10px'>GRAND TOTAL</td>"
                    my_table += "<td id='tdGT' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & GrandTotal & "</td>"
                    my_table += "</tr>"
                   
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
                my_table += "</table>"
                my_table += "</td>"

                my_table += "</tr>"

                my_table += "<tr>"
                If TransTD = "" OrElse TransTD Is Nothing Then
                    my_table += "<td style='padding: 10px;border: thin solid #999999'>"
                Else
                    my_table += "<td colspan='2' style='padding: 10px;border: thin solid #999999'>"
                End If

                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'> Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.<br/>For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</td>"

                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #004b91;font-weight:bold;padding-top:10px'>TERMS AND CONDITIONS :</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>1.</b> Guests are requested to carry their valid photo identification for all guests, including children</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>2.</b> We recommend check-in at least 2 hours prior to departure.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>3.</b>  Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>4.</b> Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>5.</b>  Flight schedules are subject to change and approval by authorities.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>6.</b> In case of ticket Cancellation the transaction fee will not be refunded.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>7.</b> Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>8.</b>  Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>9.</b> Bookings made under the Armed Forces quota are non cancelable and non- changeable.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>10.</b> Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>11.</b> Cancellation amount will be charged as per airline rule.</td>"

                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px;color: #000000; height: 20px'><b>12.</b> Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</td>"

                my_table += "</tr>"

                'For Baggage Information
                If dtpnr.Rows(0)("Trip").ToString.ToUpper = "D" Then
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #004b91;font-weight:bold;padding-top:10px'>BAGGAGE INFORMATION :</td>"
                    my_table += "</tr>"

                    'For Domestic

                    'my_table += "<tr>"
                    'my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91;font-weight:bold;padding-top:10px'>Baggage allowance of domestic airlines :</td>"
                    'my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td >"
                    my_table += "<table border='1' cellpadding='2' cellspacing='0' Width='99%' >"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91;font-weight:bold;'>Airline</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91;font-weight:bold;'>Weight</td>"
                    my_table += "</tr>"

                    Dim Bag As Boolean = False
                    If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                        Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
                    End If
                    Dim dtbaggage As New DataTable
                    dtbaggage = STDom.GetBaggageInformation("D", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), Bag).Tables(0)
                    For Each drbg In dtbaggage.Rows
                        my_table += "<tr>"
                        my_table += "<td>" & drbg("BaggageName") & "</td>"
                        my_table += "<td>" & drbg("Weight") & "</td>"
                        my_table += "</tr>"
                    Next



                    my_table += "</table>"
                    my_table += "</td >"
                    my_table += "</tr>"
                End If
                'End For Baggage Information

                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</table>"
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
                my_table += "<td colspan='3' style='background-color: ##004b91; color: #000; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Fare Information " & FareType & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='border: thin solid #999999'>"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Pax Detail</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Base Fare</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Fuel Surcharge</td>"
                my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>Tax</td>"
                my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>STax</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Fee</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Charge</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px''>TOTAL</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px''></td>"
                my_table += "</tr>"
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
                        my_table += "<tr>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_taxadt'>" & Tax & "</td>"
                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_tcadt'>" & TCharge & "</td>"

                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        my_table += "</tr>"
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
                        my_table += "<tr>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_taxchd'>" & Tax & "</td>"
                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_tcchd'>" & TCharge & "</td>"

                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        my_table += "</tr>"
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
                        my_table += "<tr>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Tax & "</td>"
                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TCharge & "</td>"

                        my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        my_table += "</tr>"

                    End If
                Next
                my_table += "<tr style='background-color:#004b91;'>"
                my_table += "<td colspan='6' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                my_table += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #FFFF99; height: 20px'>GRAND TOTAL</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #FFFFFF; height: 20px' id='td_grandtot'>" & GrandTotal & "</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"


                'my_table += "</table>"
                'my_table += "</td>"

                'my_table += "</tr>"
            Else
                my_table += "<tr>"
                my_table += "<td style='border: thin solid #999999' >"
                my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table += "<tr>"
                my_table += "<td colspan='3' style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Fare Information " & FareType & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px;padding-left:10px'>Base Fare</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("BaseFare").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Fuel Surcharge</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("Fuel").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Tax</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_perpaxtax'>" & dtfare.Rows(0)("Tax").ToString() & "</td>"
                my_table += "</tr>"

                Dim BFare As Double = Convert.ToDouble(dtfare.Rows(0)("BaseFare").ToString())
                Dim Fuel As Double = Convert.ToDouble(dtfare.Rows(0)("Fuel").ToString())
                Dim Tax As Double = Convert.ToDouble(dtfare.Rows(0)("Tax").ToString())
                Dim Total As Double = BFare + Fuel + Tax
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;padding-left:10px'>TOTAL</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Service Tax</td>"
                If (IsCorp = True) Then
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>0</td>"

                Else
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("ServiceTax").ToString() & "</td>"

                End If
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Transaction Fee</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dtfare.Rows(0)("TFee").ToString() & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Transaction Charge</td>"
                If (IsCorp = True) Then
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_perpaxtc'>0</td>"
                Else
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' id='td_perpaxtc'>" & dtfare.Rows(0)("TCharge").ToString() & "</td>"
                End If

                my_table += "</tr>"


                Dim ResuCharge As Double, ResuServiseCharge As Double, ResuFareDiff As Double = 0
                If dtpnr.Rows(0)("ResuID").ToString() <> "" AndAlso dtpnr.Rows(0)("ResuID").ToString() IsNot Nothing Then

                    ResuCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuCharge").ToString())
                    ResuServiseCharge = Convert.ToDouble(dtpnr.Rows(0)("ResuServiseCharge").ToString())
                    ResuFareDiff = Convert.ToDouble(dtpnr.Rows(0)("ResuFareDiff").ToString())
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Charge</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuCharge & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Srv. Charge</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuServiseCharge & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Reissue Fare Diff.</td>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ResuFareDiff & "</td>"
                    my_table += "</tr>"

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
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;padding-left:10px'>GRAND TOTAL</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold' id='td_perpaxgrandtot'>" & GrandTotal & "</td>"
                my_table += "</tr>"
                'my_table += "</table>"
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
                my_table += "<td style='border: thin solid #999999'>"
                my_table += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>"
                my_table += "<tr>"
                my_table += "<td colspan='9'  style='background-color: ##004b91; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Meals Bagagae Fare Information" + HD + "</td>"
                my_table += "</tr>"
                my_table += "<tr >"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold'>Pax Name</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Type</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Meal Details</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Meal Price</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Bag Details</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Bag Price</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px'>Ticket No</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #004b91; height: 20px;font-weight:Bold''>Total</td>"
                my_table += "</tr>"
                'If TransTD = "" OrElse TransTD Is Nothing Then
                For Each dr As DataRow In DtPxMB.Rows
                    If (Convert.ToDouble(dr("MPRICE").ToString()) > 0 OrElse Convert.ToDouble(dr("BPRICE").ToString()) > 0) Then
                        my_table += "<tr>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("Name").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' >" & dr("PaxType").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & If(VC = "TB", If(String.IsNullOrEmpty(dr("MDisc")) <> True, dr("MDisc").ToString().Split("_")(2), ""), dr("MDisc").ToString()) & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("MPRICE").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & If(VC = "TB", If(String.IsNullOrEmpty(dr("BDisc")) <> True, dr("BDisc").ToString().Split("_")(2) & " KG", ""), dr("BDisc").ToString()) & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("BPRICE").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr("TicketNumber").ToString() & "</td>"
                        my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</td>"

                        my_table += "</tr>"
                        FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
                    End If
                Next
                my_table += "<tr style='background-color:#004b91;'>"
                my_table += "<td   align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                my_table += "<td   colspan='7' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #FFFF99; height: 20px' >FINAL TOTAL </td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #FFFFFF; height: 20px; ' id='tdfinaltot'    >" & FinalTotal & "</td>"
                my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
            End If
        End If

        Return my_table

    End Function

End Class
