Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.IO
Partial Class CreditNodeDomDetails
    Inherits System.Web.UI.Page
    Dim STDom As New SqlTransactionDom
    Dim ST As New SqlTransaction

    Public Sub CreditNodeDetail()
        Try
            Dim projectIdShow As String = ""
            Dim BillNoCorp As String = ""
            Dim cancelledBy As String = ""
            Dim BillNoAir As String = ""
            Dim dt As New DataTable
            Dim dtt As New DataTable
            dtt = STDom.GetCreditNodeDetails(Request("RefundID"), "D", "Cancelled").Tables(0)

            Try
                If (dtt.Rows(0)("OnlineRefAmount") = 0) Then
                    dt = dtt
                Else
                    dt = dtt.Select("Tkt_No = '" & Request("TicketID") & "' ").CopyToDataTable()
                End If
            Catch ex As Exception
                dt = dtt
            End Try

            If (dt.Rows.Count > 0) Then
                Dim mgtFee As Double = 0
                If Not IsDBNull(dt.Rows(0)("ProjectID")) Then
                    projectIdShow = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >PROJECT ID&nbsp;:&nbsp;" & dt.Rows(0)("ProjectID") & "  </span>"

                End If

                If Not IsDBNull(dt.Rows(0)("CancelledBy")) Then
                    cancelledBy = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >CANCELLED BY&nbsp;:&nbsp;" & dt.Rows(0)("CancelledBy") & "  </span>"

                End If
                If Not IsDBNull(dt.Rows(0)("BillNoAir")) Then
                    BillNoAir = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >BILL NO&nbsp;:&nbsp;" & dt.Rows(0)("BillNoAir") & "  </span>"

                End If
                If Not IsDBNull(dt.Rows(0)("BillNoCorp")) Then
                    BillNoCorp = dt.Rows(0)("BillNoCorp").ToString()

                End If

                Dim dtAAdd As DataTable
                dtAAdd = ST.GetAgencyDetails(dt.Rows(0)("UserID").ToString()).Tables(0)


                Dim MgtFeeVisibleStatus As Boolean = False
                Dim IsCorp As Boolean = False
                If Not IsDBNull(dtAAdd.Rows(0)("IsCorp")) Then
                    If Convert.ToBoolean(dtAAdd.Rows(0)("IsCorp")) Then
                        MgtFeeVisibleStatus = True
                        mgtFee = If(IsDBNull(dt.Rows(0)("MgtFee")), 0, Convert.ToDouble(dt.Rows(0)("MgtFee")))
                        IsCorp = True

                    End If

                End If
                Dim dtaddress As New DataTable
                Dim my_table As String = ""

                my_table += "<table width='100%' border='0' cellspacing='0'  cellpadding='0' align='center'>"
                If (IsCorp = True) Then
                    dtaddress = STDom.GetCompanyAddress(ADDRESS.CORP.ToString().Trim()).Tables(0)
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000' align='center'>" & dtaddress.Rows(0)("COMPANYNAME") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>" & dtaddress.Rows(0)("COMPANYADDRESS") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'> Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>Email :" & dtaddress.Rows(0)("EMAIL") & "</td>"
                    my_table += "</tr>"
                Else
                    dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)
                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000' align='center'>" & dtaddress.Rows(0)("COMPANYNAME") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>" & dtaddress.Rows(0)("COMPANYADDRESS") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'> Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>Email :" & dtaddress.Rows(0)("EMAIL") & "</td>"
                    my_table += "</tr>"
                End If



                my_table += "</br>"



                my_table += "</table>"


                my_table += "</br>"



                my_table += "<table width='100%' border='0' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td  style='height:30px;padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #BBF1FF;'>&nbsp;&nbsp;  CREDIT NOTE NO :  " & If(BillNoCorp = "", Request("RefundID"), BillNoCorp) & "" & BillNoAir & projectIdShow & cancelledBy & "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='height:30px;font-size: 18px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'>  &nbsp;&nbsp;ADDRESS :<img alt='' src='Images/arrow.jpg'  /> </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Agency_Name").ToString() & " </td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Address").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("City").ToString() & ",&nbsp;" & dtAAdd.Rows(0)("State").ToString() & ",&nbsp;" & dtAAdd.Rows(0)("zipcode").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & dtAAdd.Rows(0)("Country").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "</table>"
                my_table += "</br>"

                my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"

                my_table += "<tr>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>PNR</td>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Ticket</td>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Airline</td>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Passenger</td>"

                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Sector</td>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>DDate</td>"

                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>BaseFare</td>"
                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Tax</td>"
                If (IsCorp = False) Then

                    my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Srv Tax</td>"
                    my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>TF</td>"
                End If

                my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Total</td>"
                ' my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Fare</td>"




                my_table += "</tr>"


                my_table += "<tr>"
                Dim Total As Double = 0
                If MgtFeeVisibleStatus = True Then
                    Total = Convert.ToDouble(dt.Rows(0)("Base_Fare").ToString()) + Convert.ToDouble(dt.Rows(0)("Tax").ToString()) '+ Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString())
                Else
                    Total = Convert.ToDouble(dt.Rows(0)("Base_Fare").ToString()) + Convert.ToDouble(dt.Rows(0)("Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString())
                End If
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("pnr_locator").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Tkt_No").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("VC").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("pax_fname").ToString() & " " & dt.Rows(0)("pax_lname").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Sector").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("departure_date").ToString() & "</td>"

                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Base_Fare").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Tax").ToString() & "</td>"

                If (IsCorp = False) Then
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>0</td>"
                End If

                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Total & "</td>"


                Dim NetFare As Double = 0
                Dim servicecharge As Double

                If dt.Rows(0)("ServiceCharge").ToString <> "" AndAlso dt.Rows(0)("ServiceCharge").ToString IsNot Nothing Then
                    servicecharge = Convert.ToDouble(dt.Rows(0)("ServiceCharge").ToString)
                Else
                    servicecharge = 0
                End If
                If (IsCorp = True) Then
                    my_table += "</tr>"
                    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>MANAGEMENT FEE(+)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & mgtFee & "</td>"
                    my_table += "</tr>"

                    my_table += "</tr>"
                    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE TAX(+)&nbsp;&nbsp;</td>"
                    my_table += "<td  style='font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
                    my_table += "</tr>"
                    NetFare = (Total + Convert.ToDouble(mgtFee) + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
                    my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee; line-height: 20px;'>"
                    my_table += "<td colspan='8'  align='right'>Net Fare&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & NetFare & "</td>"
                    my_table += "</tr>"

                    Dim cancellationcharge As Double = 0
                    cancellationcharge = Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + mgtFee + Convert.ToDouble(dt.Rows(0)("CancellationCharge")) + servicecharge

                    my_table += "</tr>"
                    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>CANCELLATION CHARGE(-)&nbsp;&nbsp;</td>"
                    'my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("CancellationCharge").ToString() & "</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & cancellationcharge.ToString() & "</td>"
                    my_table += "</tr>"

                    'my_table += "</tr>"
                    'my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE CHARGE(-)&nbsp;&nbsp;</td>"
                    'my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & servicecharge & "</td>"
                    'my_table += "</tr>"
                Else

                    my_table += "</tr>"
                    my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>DISCOUNT(-)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Convert.ToDouble(dt.Rows(0)("Discount").ToString()) - Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString()) & "</td>"
                    my_table += "</tr>"

                    my_table += "</tr>"
                    my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>TDS(+)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("TDS").ToString() & "</td>"
                    my_table += "</tr>"



                    NetFare = (Total + Convert.ToDouble(dt.Rows(0)("TDS").ToString())) - Convert.ToDouble(dt.Rows(0)("Discount").ToString())
                    my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee; line-height: 20px;'>"
                    my_table += "<td colspan='10'  align='right'>Net Fare&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & NetFare & "</td>"
                    my_table += "</tr>"


                    my_table += "</tr>"
                    my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>CANCELLATION CHARGE(-)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("CancellationCharge").ToString() & "</td>"
                    my_table += "</tr>"

                    my_table += "</tr>"
                    my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE CHARGE(-)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & servicecharge & "</td>"
                    my_table += "</tr>"
                End If

                'If IsCorp = True Then
                '    my_table += "</tr>"
                '    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE TAX(-)&nbsp;&nbsp;</td>"
                '    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
                '    my_table += "</tr>"
                '    my_table += "</tr>"
                '    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>MANAGEMENT FEE(-)&nbsp;&nbsp;</td>"
                '    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & mgtFee.ToString() & "</td>"
                '    my_table += "</tr>"

                'End If '



                'Dim RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)

                Dim RefAmt As Double
                If IsCorp = True Then
                    RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge + mgtFee + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
                Else
                    RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)
                End If

                'If (String.IsNullOrEmpty(Convert.ToDouble(dt.Rows(0)("OnlineRefAmount").ToString()))) Then

                '    If IsCorp = True Then
                '        RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge + mgtFee + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
                '    Else
                '        RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)
                '    End If

                'Else
                '    If IsCorp = True Then
                '        RefAmt = Total - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge + mgtFee + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
                '    Else
                '        RefAmt = Total - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)
                '    End If

                'End If

                '' Dim m1 As DateTime = Convert.ToDateTime(dt.Rows(0)("AcceptDate").ToString()) ''Refund date
                '' Dim m() As String = dt.Rows(0)("Booking_date").ToString().Replace("-", "/").Split("/")  ''bookingDate
                '' Dim m2 As String = m(1)

                'If m1.Month <> Convert.ToInt32(m2) Then

                'If (String.IsNullOrEmpty(Convert.ToDouble(dt.Rows(0)("OnlineRefAmount").ToString()))) Then MK
                'Else
                '    my_table += "</tr>"
                '    my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>Dis(-)&nbsp;&nbsp;</td>"
                '    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Convert.ToDouble(dt.Rows(0)("Discount").ToString()) - Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString()) & "</td>"
                '    my_table += "</tr>"

                '    Dim tt As Decimal = Convert.ToDouble(dt.Rows(0)("Discount").ToString()) - Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString())

                '    RefAmt = RefAmt - tt

                'End If




                my_table += "</tr>"
                my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>TDS(-)&nbsp;&nbsp;</td>"
                    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("TDS").ToString() & "</td>"
                    my_table += "</tr>"
                    RefAmt = RefAmt - Convert.ToDouble(dt.Rows(0)("TDS").ToString())
                    ' End If


                    Dim GTInWord As New NumToWord.NumberToWord()
                    my_table += "<tr style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;'>"
                    my_table += "<td align='center'>Amount in word  </td>"
                    If (IsCorp = True) Then
                        my_table += "<td colspan='6' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(RefAmt)) & " </td>"
                    Else
                        my_table += "<td colspan='8' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(RefAmt)) & " </td>"
                    End If

                    my_table += "<td   align='center'>Refund Amount</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #424242;' align='center'>" & RefAmt & "</td>"
                    my_table += "</tr>"
                    my_table += "</table>"

                    lbl_Detail.Text = my_table
                Else


                    Dim my_table As String = ""

                my_table += "<table width='100%' border='0' cellspacing='0'  cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td align='center'>Credit Node is Not Genereted For This Ticket </td>"
                my_table += "</tr>"
                my_table += "</table>"

                lbl_Detail.Text = my_table
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try


    End Sub


    'Public Sub CreditNodeDetail()
    '    Try

    '        Dim projectIdShow As String = ""
    '        Dim BillNoCorp As String = ""
    '        Dim cancelledBy As String = ""
    '        Dim BillNoAir As String = ""
    '        Dim dt As New DataTable
    '        dt = STDom.GetCreditNodeDetails(Request("RefundID"), "D", "Cancelled").Tables(0)

    '        Dim mgtFee As Double = 0
    '        If Not IsDBNull(dt.Rows(0)("ProjectID")) Then
    '            projectIdShow = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >PROJECT ID&nbsp;:&nbsp;" & dt.Rows(0)("ProjectID") & "  </span>"

    '        End If

    '        If Not IsDBNull(dt.Rows(0)("CancelledBy")) Then
    '            cancelledBy = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >CANCELLED BY&nbsp;:&nbsp;" & dt.Rows(0)("CancelledBy") & "  </span>"

    '        End If
    '        If Not IsDBNull(dt.Rows(0)("BillNoAir")) Then
    '            BillNoAir = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=""text-align:right;"" >BILL NO&nbsp;:&nbsp;" & dt.Rows(0)("BillNoAir") & "  </span>"

    '        End If
    '        If Not IsDBNull(dt.Rows(0)("BillNoCorp")) Then
    '            BillNoCorp = dt.Rows(0)("BillNoCorp").ToString()

    '        End If




    '        Dim dtAAdd As DataTable
    '        dtAAdd = ST.GetAgencyDetails(dt.Rows(0)("UserID").ToString()).Tables(0)


    '        Dim MgtFeeVisibleStatus As Boolean = False
    '        Dim IsCorp As Boolean = False
    '        If Not IsDBNull(dtAAdd.Rows(0)("IsCorp")) Then
    '            If Convert.ToBoolean(dtAAdd.Rows(0)("IsCorp")) Then
    '                MgtFeeVisibleStatus = True
    '                mgtFee = If(IsDBNull(dt.Rows(0)("MgtFee")), 0, Convert.ToDouble(dt.Rows(0)("MgtFee")))
    '                IsCorp = True

    '            End If

    '        End If
    '        Dim dtaddress As New DataTable
    '        Dim my_table As String = ""

    '        my_table += "<table width='100%' border='0' cellspacing='0'  cellpadding='0' align='center'>"
    '        If (IsCorp = True) Then
    '            dtaddress = STDom.GetCompanyAddress(ADDRESS.CORP.ToString().Trim()).Tables(0)
    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000' align='center'>" & dtaddress.Rows(0)("COMPANYNAME") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>" & dtaddress.Rows(0)("COMPANYADDRESS") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'> Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>Email :" & dtaddress.Rows(0)("EMAIL") & "</td>"
    '            my_table += "</tr>"
    '        Else
    '            dtaddress = STDom.GetCompanyAddress(ADDRESS.ITZ.ToString().Trim()).Tables(0)
    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000' align='center'>" & dtaddress.Rows(0)("COMPANYNAME") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>" & dtaddress.Rows(0)("COMPANYADDRESS") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'> Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "</td>"
    '            my_table += "</tr>"

    '            my_table += "<tr>"
    '            my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666' align='center'>Email :" & dtaddress.Rows(0)("EMAIL") & "</td>"
    '            my_table += "</tr>"
    '        End If



    '        my_table += "</br>"



    '        my_table += "</table>"


    '        my_table += "</br>"



    '        my_table += "<table width='100%' border='0' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"
    '        my_table += "<tr>"
    '        my_table += "<td  style='height:30px;padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #BBF1FF;'>&nbsp;&nbsp;  CREDIT NOTE NO :  " & If(BillNoCorp = "", Request("RefundID"), BillNoCorp) & "" & BillNoAir & projectIdShow & cancelledBy & "</td>"
    '        my_table += "</tr>"
    '        my_table += "<tr>"
    '        my_table += "<td style='height:30px;font-size: 18px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'>  &nbsp;&nbsp;ADDRESS :<img alt='' src='Images/arrow.jpg'  /> </td>"
    '        my_table += "</tr>"

    '        my_table += "<tr>"
    '        my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Agency_Name").ToString() & " </td>"
    '        my_table += "</tr>"
    '        my_table += "<tr>"
    '        my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Address").ToString() & " </td>"
    '        my_table += "</tr>"

    '        my_table += "<tr>"
    '        my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("City").ToString() & ",&nbsp;" & dtAAdd.Rows(0)("State").ToString() & ",&nbsp;" & dtAAdd.Rows(0)("zipcode").ToString() & " </td>"
    '        my_table += "</tr>"

    '        my_table += "<tr>"
    '        my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & dtAAdd.Rows(0)("Country").ToString() & " </td>"
    '        my_table += "</tr>"

    '        my_table += "</table>"
    '        my_table += "</br>"

    '        my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"

    '        my_table += "<tr>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>PNR</td>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Ticket</td>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Airline</td>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Passenger</td>"

    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Sector</td>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>DDate</td>"

    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>BaseFare</td>"
    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Tax</td>"
    '        If (IsCorp = False) Then

    '            my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Srv Tax</td>"
    '            my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>TF</td>"
    '        End If

    '        my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Total</td>"
    '        ' my_table += "<td style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;' align='center'>Fare</td>"




    '        my_table += "</tr>"


    '        my_table += "<tr>"
    '        Dim Total As Double = 0
    '        If MgtFeeVisibleStatus = True Then
    '            Total = Convert.ToDouble(dt.Rows(0)("Base_Fare").ToString()) + Convert.ToDouble(dt.Rows(0)("Tax").ToString()) '+ Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString())
    '        Else
    '            Total = Convert.ToDouble(dt.Rows(0)("Base_Fare").ToString()) + Convert.ToDouble(dt.Rows(0)("Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString())
    '        End If
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("pnr_locator").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Tkt_No").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("VC").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("pax_fname").ToString() & " " & dt.Rows(0)("pax_lname").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Sector").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("departure_date").ToString() & "</td>"

    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Base_Fare").ToString() & "</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Tax").ToString() & "</td>"

    '        If (IsCorp = False) Then
    '            my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
    '            my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>0</td>"
    '        End If

    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Total & "</td>"


    '        Dim NetFare As Double = 0
    '        Dim servicecharge As Double

    '        If dt.Rows(0)("ServiceCharge").ToString <> "" AndAlso dt.Rows(0)("ServiceCharge").ToString IsNot Nothing Then
    '            servicecharge = Convert.ToDouble(dt.Rows(0)("ServiceCharge").ToString)
    '        Else
    '            servicecharge = 0
    '        End If
    '        If (IsCorp = True) Then
    '            my_table += "</tr>"
    '            my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>MANAGEMENT FEE(+)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & mgtFee & "</td>"
    '            my_table += "</tr>"

    '            my_table += "</tr>"
    '            my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE TAX(+)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style='font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
    '            my_table += "</tr>"
    '            NetFare = (Total + Convert.ToDouble(mgtFee) + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
    '            my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee; line-height: 20px;'>"
    '            my_table += "<td colspan='8'  align='right'>Net Fare&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & NetFare & "</td>"
    '            my_table += "</tr>"

    '            Dim cancellationcharge As Double = 0
    '            cancellationcharge = Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()) + mgtFee + Convert.ToDouble(dt.Rows(0)("CancellationCharge")) + servicecharge

    '            my_table += "</tr>"
    '            my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>CANCELLATION CHARGE(-)&nbsp;&nbsp;</td>"
    '            'my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("CancellationCharge").ToString() & "</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & cancellationcharge.ToString() & "</td>"
    '            my_table += "</tr>"

    '            'my_table += "</tr>"
    '            'my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE CHARGE(-)&nbsp;&nbsp;</td>"
    '            'my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & servicecharge & "</td>"
    '            'my_table += "</tr>"
    '        Else

    '            my_table += "</tr>"
    '            my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>DISCOUNT(-)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Convert.ToDouble(dt.Rows(0)("Discount").ToString()) - Convert.ToDouble(dt.Rows(0)("Tran_Fees").ToString()) & "</td>"
    '            my_table += "</tr>"

    '            my_table += "</tr>"
    '            my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>TDS(+)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("TDS").ToString() & "</td>"
    '            my_table += "</tr>"
    '            NetFare = (Total + Convert.ToDouble(dt.Rows(0)("TDS").ToString())) - Convert.ToDouble(dt.Rows(0)("Discount").ToString())
    '            my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee; line-height: 20px;'>"
    '            my_table += "<td colspan='10'  align='right'>Net Fare&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & NetFare & "</td>"
    '            my_table += "</tr>"


    '            my_table += "</tr>"
    '            my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>CANCELLATION CHARGE(-)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("CancellationCharge").ToString() & "</td>"
    '            my_table += "</tr>"

    '            my_table += "</tr>"
    '            my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE CHARGE(-)&nbsp;&nbsp;</td>"
    '            my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & servicecharge & "</td>"
    '            my_table += "</tr>"
    '        End If






    '        'If IsCorp = True Then
    '        '    my_table += "</tr>"
    '        '    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE TAX(-)&nbsp;&nbsp;</td>"
    '        '    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt.Rows(0)("Service_Tax").ToString() & "</td>"
    '        '    my_table += "</tr>"
    '        '    my_table += "</tr>"
    '        '    my_table += "<td colspan='8' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>MANAGEMENT FEE(-)&nbsp;&nbsp;</td>"
    '        '    my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & mgtFee.ToString() & "</td>"
    '        '    my_table += "</tr>"

    '        'End If '



    '        'Dim RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)
    '        Dim RefAmt As Double
    '        If IsCorp = True Then
    '            RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge + mgtFee + Convert.ToDouble(dt.Rows(0)("Service_Tax").ToString()))
    '        Else
    '            RefAmt = NetFare - (Convert.ToDouble(dt.Rows(0)("CancellationCharge").ToString()) + servicecharge)
    '        End If


    '        Dim GTInWord As New NumToWord.NumberToWord()
    '        my_table += "<tr style='padding: 4px 2px; color: #424242; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #eee;'>"
    '        my_table += "<td align='center'>Amount in word  </td>"
    '        If (IsCorp = True) Then
    '            my_table += "<td colspan='6' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(RefAmt)) & " </td>"
    '        Else
    '            my_table += "<td colspan='8' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(RefAmt)) & " </td>"
    '        End If

    '        my_table += "<td   align='center'>Refund Amount</td>"
    '        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #424242;' align='center'>" & RefAmt & "</td>"
    '        my_table += "</tr>"

    '        my_table += "</table>"


    '        lbl_Detail.Text = my_table
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreditNodeDetail()
    End Sub
End Class
