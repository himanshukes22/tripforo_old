Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports YatraBilling
Imports System.IO

Partial Class Reports_HoldPNR_UpdateHoldPNRIntl
    Inherits System.Web.UI.Page

    Dim ID As New IntlDetails()
    Dim STDom As New SqlTransactionDom
    Dim objSql As New SqlTransactionNew

    Dim STYTR As New SqlTransactionYatra


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                TicketDetail()

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If

    End Sub
    Private Sub TicketDetail()
        Try
            Dim OrderId As String = Request.QueryString("OrderId")
            'Dim OrderId As String = "1"
            Dim dtagentid As New DataTable()
            dtagentid = ID.SelectAgent(OrderId)


            Dim dtagency As New DataTable()
            dtagency = ID.SelectAgencyDetail(dtagentid.Rows(0)("AgentId").ToString())


            Dim dtpnr As New DataTable()
            dtpnr = STDom.SelectHeaderDetail_HOLD(OrderId).Tables(0)
            Dim PRT As String = ""
            If (dtpnr.Rows(0)("PRT").ToString().ToUpper() = "PART OF ROUNDTRIP") Then
                PRT = " : Part of normal round trip(" & dtpnr.Rows(0)("FareType").ToString() & ") Reference OrderId : " & dtpnr.Rows(0)("PartOfOrderId").ToString() & ""
            End If

            td_agencyinfo.InnerText = "Agency Informaton" & PRT
            td_AgenctName.InnerText = dtagency.Rows(0)("Agency_Name").ToString()
            td_Add.InnerText = dtagency.Rows(0)("Address").ToString()
            td_City.InnerText = dtagency.Rows(0)("Address1").ToString()
            td_Mobile.InnerText = dtagency.Rows(0)("Mobile").ToString()
            td_Email.InnerText = dtagency.Rows(0)("Email").ToString()



            txt_GDSPNR.Text = dtpnr.Rows(0)("GdsPnr").ToString()
            txt_AirlinePNR.Text = dtpnr.Rows(0)("AirlinePnr").ToString()
            td_BookingDate.InnerText = dtpnr.Rows(0)("CreateDate").ToString()
            td_Status.InnerText = dtpnr.Rows(0)("Status").ToString()

            Repeater_Traveller.DataSource = ID.TravellerInfo(OrderId)
            Repeater_Traveller.DataBind()


            Dim dtflight As New DataTable()
            dtflight = ID.SelectFlightDetail(OrderId)


            Dim MgtFeeVisibleStatus As Boolean = False

            If Not IsDBNull(dtagency.Rows(0)("IsCorp")) Then
                If Convert.ToBoolean(dtagency.Rows(0)("IsCorp")) Then
                    MgtFeeVisibleStatus = True

                End If

            End If


            Dim my_table As String = ""
            my_table += "<table width='100%'>"
            my_table += "<tr>"
            my_table += "<td >"
            my_table += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"

            my_table += "<tr>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>From - To</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px; padding-left:10px'></td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Depart Date</td>"


            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>DepTime</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>ArrTime</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>Aircraft</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>AdtRbd</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;'>ChdRbd</td>"

            my_table += "</tr>"
            For Each dr1 As DataRow In dtflight.Rows
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px'> " & dr1("DepAirName").ToString() & " (" & dr1("DFrom").ToString() & " ) - " & dr1("ArrAirName").ToString() & " (" & (dr1("ATo").ToString()) & ")</td>"

                my_table += "<td>"
                my_table += "<table>"
                my_table += "<tr>"
                my_table += "<td> <img src='../../AirLogo/sm" & dr1("AirlineCode").ToString() & ".gif' /></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'> " & dr1("AirlineName").ToString() & "(" & dr1("AirlineCode").ToString() & "-" & dr1("FltNumber").ToString() & ")</td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"

                Dim depDate As String = ""
                depDate = Left(dr1("DepDate").ToString, 2) & " " & datecon(Mid(dr1("DepDate").ToString, 3, 2)) & ", " & Right(dr1("DepDate").ToString, 2)
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & depDate & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'> " & dr1("DepTime").ToString() & "Hrs</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'> " & dr1("ArrTime").ToString() & "Hrs</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("AirCraft").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("AdtRbd").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr1("ChdRbd").ToString() & "</td>"


                my_table += "</tr>"
            Next
            my_table += "</table>"
            my_table += "</td>"
            my_table += "</tr>"
            my_table += "</table>"
            lbl_FlightInfo.Text = my_table


            'Fare Details
            Dim my_table1 As String = ""
            Dim TransTD As String = ""

            Dim dtfare As New DataTable()
            dtfare = ID.SelectFareDetail(OrderId, TransTD)
            ViewState("TFADIS") = dtfare

            Dim dtadtcount As New DataTable()
            dtadtcount = ID.CountADT(OrderId)

            Dim dtchdcount As New DataTable()
            dtchdcount = ID.CountCHD(OrderId)

            Dim dtinfcount As New DataTable()
            dtinfcount = ID.CountINF(OrderId)

            If TransTD = "" OrElse TransTD Is Nothing Then
                Dim GrandTotal As Double = 0
                td_fareinfo.InnerText = "Fare Information " & "(" & dtfare.Rows(0)("FareType") & ")"
                my_table1 += "<table width='100%'>"

                my_table1 += "<tr>"
                my_table1 += "<td>"
                my_table1 += "<table border='0' cellpadding='0' cellspacing='0' width='100%' align='center' >"
                my_table1 += "<tr>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Pax Detail</td>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Base Fare</td>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:100px'>Fuel Surcharge</td>"
                my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>Tax</td>"
                my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:60px'>STax</td>"
                If MgtFeeVisibleStatus = True Then
                    my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Mgt Fee</td>"

                Else
                    my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Fee</td>"
                    my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px'>Trans Charge</td>"

                End If


                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px''>TOTAL</td>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:80px''></td>"
                my_table1 += "</tr>"
                For Each dr2 As DataRow In dtfare.Rows
                    If dr2("PaxType").ToString() = "ADT" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim TCharge As Double = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim mgtfee1 As Double = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())

                        Dim Total As Double = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                        my_table1 += "<tr>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Tax & "</td>"
                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"
                        If MgtFeeVisibleStatus = True Then
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                        Else
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TCharge & "</td>"

                        End If

                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                        my_table1 += "</tr>"
                    End If
                    If dr2("PaxType").ToString() = "CHD" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim TCharge As Double = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        Dim mgtfee1 As Double = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Total As Double = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString()) + mgtfee1
                        my_table1 += "<tr>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Tax & "</td>"
                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"


                        If MgtFeeVisibleStatus = True Then
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                        Else
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TCharge & "</td>"

                        End If
                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += Convert.ToDouble(dr2("Total").ToString()) * Convert.ToDouble(dtchdcount.Rows(0)(0).ToString())
                        my_table1 += "</tr>"
                    End If
                    If dr2("PaxType").ToString() = "INF" Then
                        Dim BaseFare As Double = Convert.ToDouble(dr2("BaseFare").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim Fuel As Double = Convert.ToDouble(dr2("Fuel").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim Tax As Double = Convert.ToDouble(dr2("Tax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim ServiceTax As Double = Convert.ToDouble(dr2("ServiceTax").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim TFee As Double = Convert.ToDouble(dr2("TFee").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim TCharge As Double = Convert.ToDouble(dr2("TCharge").ToString()) * Convert.ToDouble(dtinfcount.Rows(0)(0).ToString())
                        Dim mgtfee1 As Double = Convert.ToDouble(dr2("MgtFee").ToString()) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString())
                        Dim Total As Double = (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                        my_table1 += "<tr>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr2("PaxType").ToString() & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & BaseFare & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Fuel & "</td>"
                        my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & Tax & "</td>"
                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & ServiceTax & "</td>"


                        If MgtFeeVisibleStatus = True Then
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & mgtfee1.ToString() & "</td>"

                        Else
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TFee & "</td>"
                            my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & TCharge & "</td>"

                        End If
                        my_table1 += "<td  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #004b91; height: 20px;font-weight:Bold'>" & Total & "</td>"
                        GrandTotal += (If(MgtFeeVisibleStatus = True, (Convert.ToDouble(dr2("Total").ToString()) - Convert.ToDouble(dr2("TFee").ToString()) - Convert.ToDouble(dr2("TCharge").ToString())), Convert.ToDouble(dr2("Total").ToString()))) * Convert.ToDouble(dtadtcount.Rows(0)(0).ToString()) + mgtfee1
                        my_table1 += "</tr>"

                    End If
                Next
                Dim FinalTotal As Double
                'Dim clstktcopy As New clsTicketCopy
                'my_table1 += "<tr>"
                'my_table1 += "<td colspan='6'>"
                'If (dtpnr.Rows(0)("TripType") = "R") Then
                '    my_table1 += clstktcopy.Meal_BagDetails(OrderId, TransTD, dtflight.Rows(0)("AirlineCode"), "O", FinalTotal, "OutBound")
                '    my_table1 += clstktcopy.Meal_BagDetails(OrderId, TransTD, dtflight.Rows(0)("AirlineCode"), "R", FinalTotal, "InBound")
                'Else
                '    my_table1 += clstktcopy.Meal_BagDetails(OrderId, TransTD, dtflight.Rows(0)("AirlineCode"), "O", FinalTotal, "OutBound")
                'End If
                'my_table1 += "</td>"
                'my_table1 += "</tr>"

                'BAGGAGE

                Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, dtflight.Rows(0)("AirlineCode"), dtpnr.Rows(0)("TripType"))
                Dim DtPxMB As DataTable = dtfare1.Tables(0)
                If DtPxMB.Rows.Count > 0 Then
                    my_table1 += "<tr >"
                    my_table1 += "<td colspan='9' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 25px;padding-left:10px;width:250px;background-color: #CCCCCC;'>Meals Baggage Fare Information</td>"
                    my_table1 += "</td>"
                    my_table1 += "</tr>"

                    my_table1 += "<tr >"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px;width:250px'>Pax Name</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Type</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Meal Detail</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Meal Price</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Bag Detail</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Bag Price</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;width:250px'>Ticket No</td>"
                    my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>TOTAL</td>"
                    my_table += "</tr>"

                    For Each dr As DataRow In DtPxMB.Rows
                        If (Convert.ToDouble(dr("MPRICE").ToString()) > 0 OrElse Convert.ToDouble(dr("BPRICE").ToString()) > 0) Then


                            my_table1 += "<tr>"
                            my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>" & dr("Name").ToString() & "</td>"
                            my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;width:250px' >" & dr("PaxType").ToString() & "</td>"
                            If (Convert.ToDouble(dr("MPRICE").ToString()) > 0) Then
                                my_table1 += "<td  style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>" & dr("MDisc").ToString() & "</td>"
                                my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>" & dr("MPRICE").ToString() & "</td>"
                            Else
                                my_table1 += "<td  style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>N/A</td>"
                                my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>N/A</td>"

                            End If
                            If (Convert.ToDouble(dr("BPRICE").ToString()) > 0) Then
                                my_table1 += "<td align='left' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>" & dr("BDisc").ToString() & "</td>"
                                my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>" & dr("BPRICE").ToString() & "</td>"
                            Else
                                my_table1 += "<td align='left' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>N/A</td>"
                                my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;width:250px'>N/A</td>"
                            End If

                            my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;width:250px'>" & dr("TicketNumber").ToString() & "</td>"
                            my_table1 += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;color: #004b91;font-weight:Bold'>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</td>"
                            my_table1 += "</tr>"
                            FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
                        End If
                    Next

                End If
                'END


                my_table1 += "<tr style='background-color: #FFF;'>"
                If MgtFeeVisibleStatus = True Then
                    my_table1 += "<td colspan='3' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                    my_table1 += "<td colspan='3'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #004b91; height: 20px'>GRAND TOTAL</td>"

                Else
                    my_table1 += "<td colspan='5' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                    my_table1 += "<td colspan='2'  align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #004b91; height: 20px'>GRAND TOTAL</td>"

                End If

                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #000000; height: 20px;'>" & GrandTotal + FinalTotal & "</td>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
                my_table1 += "</tr>"
                my_table1 += "<tr style='background-color: #FFF;'>"
                If MgtFeeVisibleStatus = True Then
                    my_table1 += "<td colspan='3' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                    my_table1 += "<td colspan='3' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #004b91; height: 20px'>NET FARE</td>"

                Else
                    my_table1 += "<td colspan='5' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
                    my_table1 += "<td colspan='2' align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #004b91; height: 20px'>NET FARE</td>"

                End If

                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #000000; height: 20px;'>" & dtpnr.Rows(0)("TotalAfterDis").ToString() & "</td>"
                my_table1 += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
                my_table1 += "</tr>"
                my_table1 += "</table>"
                my_table1 += "</td>"
                my_table1 += "</tr>"

                my_table1 += "</table>"
                lbl_FareInfo.Text = my_table1
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try

            Dim OrderId As String = Request.QueryString("OrderId")
            ID.UpdatePNRIntl(OrderId, txt_GDSPNR.Text.Trim(), txt_AirlinePNR.Text.Trim(), "Ticketed")
            Dim dt As New DataTable
            'dt = ViewState("TFADIS")
            dt = ID.SelectHeaderDetail(OrderId)

            Dim i As Integer = 0
            For Each rw As RepeaterItem In Repeater_Traveller.Items
                i += 1
                Dim TktNo As TextBox = DirectCast(rw.FindControl("txt_TktNo"), TextBox)
                Dim TID As Label = DirectCast(rw.FindControl("lbl_TransID"), Label)
                Dim PaxType As Label = DirectCast(rw.FindControl("lbl_paxtype"), Label)
                Dim Fare As Label = DirectCast(rw.FindControl("lbl_Fare"), Label)
                If dt.Rows(0)("VC").ToString.Trim.ToUpper <> "6E" And dt.Rows(0)("VC").ToString.Trim.ToUpper <> "SG" And dt.Rows(0)("VC").ToString.Trim.ToUpper <> "G8" Then
                    ID.UpdateTicketIntl(OrderId, TID.Text, TktNo.Text.Trim())
                    STDom.UpdateTktDomIntlOnLedger(OrderId, Convert.ToInt32(TID.Text.Trim), TktNo.Text.Trim())
                Else
                    ID.UpdateTicketIntl(OrderId, TID.Text, TktNo.Text.Trim() & i.ToString)
                    STDom.UpdateTktDomIntlOnLedger(OrderId, Convert.ToInt32(TID.Text.Trim), TktNo.Text.Trim() & i.ToString)
                End If

                '

            Next
            'NAV METHOD  CALL START
            Try

                'Dim objNav As New AirService.clsConnection(OrderId, "0", "0")
                'objNav.airBookingNav(OrderId, "", 0)

            Catch ex As Exception

            End Try
            'Nav METHOD END'
            Try

                'STYTR.InsertYatra_MIRHEADER(OrderId, txt_GDSPNR.Text.Trim())
                'STYTR.InsertYatra_PAX(OrderId, txt_GDSPNR.Text.Trim())
                'STYTR.InsertYatra_SEGMENT(OrderId, txt_GDSPNR.Text.Trim())
                'STYTR.InsertYatra_FARE(OrderId, txt_GDSPNR.Text.Trim())
                'STYTR.InsertYatra_DIFTLINES(OrderId, txt_GDSPNR.Text.Trim())

                'Dim AirObj As New AIR_YATRA
                'AirObj.ProcessYatra_Air(OrderId, txt_GDSPNR.Text.Trim(), "B")
            Catch ex As Exception
            End Try
            Try
                'Dim agent_id As String
                'Dim email_id As String
                'Dim dtaid As New DataTable()
                'dtaid = ID.EmailID(OrderId)
                'agent_id = dtaid.Rows(0)("AgentId").ToString()
                'Dim dtemail As New DataTable()
                'dtemail = ID.AgentIDInfo(agent_id)
                'email_id = dtemail.Rows(0)("Email").ToString()
                'Dim s As String = ""
                'Dim objTktCopy As New clsTicketCopy
                's = objTktCopy.TicketDetail(OrderId, "")
                'Dim MailDt As New DataTable
                'MailDt = STDom.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

                'Dim email As String = Request("txt_email")
                Dim dtflight As New DataTable()
                dtflight = ID.SelectFlightDetail(OrderId)
                mailTktCopy(dt.Rows(0)("VC").ToString.Trim, dtflight.Rows(0)("FltNumber").ToString.Trim, dt.Rows(0)("Sector").ToString.Trim, dtflight.Rows(0)("DepDate").ToString.Trim, dt.Rows(0)("FareType").ToString.Trim, dt.Rows(0)("AirlinePnr").ToString.Trim, dt.Rows(0)("GdsPnr").ToString.Trim, dt.Rows(0)("Status").ToString.Trim, OrderId, dt.Rows(0)("PgEmail").ToString.Trim)
            Catch ex As Exception

            End Try
            Try
                Dim smsStatus As String = ""
                Dim smsMsg As String = ""
                Dim objSMSAPI As New SMSAPI.SMS
                Dim dtpnr As New DataTable()
                dtpnr = ID.SelectHeaderDetail(OrderId)
                Dim dtflight As New DataTable()
                dtflight = ID.SelectFlightDetail(OrderId)
                Dim SmsCrd As DataTable
                Dim objDA As New SqlTransaction
                SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGHOLD.ToString()).Tables(0)
                If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                    smsStatus = objSMSAPI.sendSms(OrderId, dtpnr.Rows(0)("PgMobile").ToString.Trim, dtpnr.Rows(0)("sector").ToString.Trim, dtpnr.Rows(0)("VC").ToString.Trim, "", dtflight.Rows(0)("Departure_Date"), txt_GDSPNR.Text, smsMsg, SmsCrd)
                    objSql.SmsLogDetails(OrderId, dtpnr.Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                End If
              
            Catch ex As Exception

            End Try
            Response.Write("<script language='javascript'>alert('Ticket Updated Sucessfully')</script>")
            Response.Write("<script language='javascript'>self.close();</script>")

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
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
    Public Function Meal_BagDetails(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal TT As String, ByRef FinalTotal As Double, ByVal HD As String) As String
        Dim my_table As String = ""
        Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, VC, TT)
        Dim DtPxMB As DataTable = dtfare1.Tables(0)

        If DtPxMB.Rows.Count > 0 Then
            my_table += "<tr>"
            my_table += "<td style='border: thin solid #999999'>"
            my_table += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>"
            my_table += "<tr>"
            my_table += "<td colspan='3'  style='background-color: #135f06; color: #ffffff; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px'>Meals Bagagae Fare Information" + HD + "</td>"
            my_table += "</tr>"
            my_table += "<tr >"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px;padding-left:10px'>Passenger Name</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Type</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>MEAL_DETAIL</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>MEAL_PRICE</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>BAG_DETAIL</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>BAG_PRICE</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>TOTAL</td>"
            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'>Ticket No</td>"
            my_table += "</tr>"
            'If TransTD = "" OrElse TransTD Is Nothing Then
            For Each dr As DataRow In DtPxMB.Rows
                my_table += "<tr>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("Name").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px' >" & dr("PaxType").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("MDisc").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("MPRICE").ToString() & "</td>"
                my_table += "<td  style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("BDisc").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & dr("BPRICE").ToString() & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px;padding-left:10px;'>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</td>"
                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr("TicketNumber").ToString() & "</td>"
                my_table += "</tr>"
                FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
            Next
        End If
        my_table += "<tr style='background-color: gray;'>"
        my_table += "<td   align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: #000000; height: 20px'></td>"
        my_table += "<td   colspan='7' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #FFFF99; height: 20px' >GRAND TOTAL </td>"
        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #FFFF99; height: 20px; ' id='td_grandtot'    >" & FinalTotal & "</td>"
        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
        my_table += "</tr>"
        my_table += "</table>"
        my_table += "</td>"
        my_table += "</tr>"
        Return my_table

    End Function
    Private Sub mailTktCopy(ByVal VC As String, ByVal FltNo As String, ByVal Sector As String, ByVal DepDate As String, ByVal FT As String, ByVal AirlinePnr As String, ByVal GdsPnr As String, ByVal BkgStatus As String, ByVal OrderId As String, ByVal EmailId As String) 'As String
        Try

            Dim objTktCopy As New clsTicketCopy
            Dim strTktCopy As String = "", strHTML As String = "", strFileName As String = "", strMailMsg As String = ""
            Dim rightHTML As Boolean = False
            strFileName = "D:\SPR_TicketCopy\" & GdsPnr & "-" & FT & " Flight details-" & DateAndTime.Now.ToString.Replace(":", "").Trim & ".html"
            strFileName = strFileName.Replace("/", "-")
            strTktCopy = objTktCopy.TicketDetail(OrderId, "")
            strHTML = "<html><head><title>Booking Details</title><style type='text/css'> .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" & strTktCopy & "</body></html>"
            rightHTML = SaveTextToFile(strHTML, strFileName)
            strMailMsg = "<p style='font-family:verdana; font-size:12px'>Dear Customer<br /><br />"
            strMailMsg = strMailMsg & "Greetings of the day !!!!<br /><br />"
            strMailMsg = strMailMsg & "Please find an attachment for your E-ticket, kindly carry the print out of the same for hassle-free travel. Your onward booking for " & Sector & " is confirmed on " & VC & "-" & FltNo & " for " & DepDate & ". Your airline  booking reference no is " & AirlinePnr & ". <br /><br />"
            strMailMsg = strMailMsg & "Have a nice &amp; wonderful trip.<br /><br />"
            If BkgStatus = "Ticketed" Then


                Dim MailDt As New DataTable
                MailDt = STDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                Try
                    If rightHTML Then
                        STDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), strFileName)
                    Else
                        STDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), "")
                    End If
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception

        End Try
        ' Return strTktCopy
    End Sub
    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        Dim Contents As String
        Dim Saved As Boolean = False
        Dim objReader As IO.StreamWriter
        Try
            objReader = New IO.StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            Saved = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return Saved
    End Function
End Class

