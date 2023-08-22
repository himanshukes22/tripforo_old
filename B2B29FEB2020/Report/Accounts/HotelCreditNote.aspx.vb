Imports System.Data
Partial Class SprReports_Accounts_HotelCreditNote
    Inherits System.Web.UI.Page

    Private HtlST As New HotelDAL.HotelDA()
    Dim ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
          
            Dim AgencyDT As DataTable
            AgencyDT = ST.GetAgencyDetails(Request("AgentID")).Tables(0)
            Dim dtaddress As New DataTable
            Dim STDom As New SqlTransactionDom
            dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)
            Dim my_table As String = ""
            'spring address start
            my_table += "<table width='100%' border='0' cellspacing='0'  cellpadding='0' align='center'>"
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
            my_table += "</table>"
            'spring address End
            'my_table += "</br>"

            Dim RefundDT As New DataTable
            RefundDT = HtlST.HtlRefundDetail(StatusClass.Cancelled.ToString(), Request("OrderId"), "", "CreditNote", "").Tables(0)
            If RefundDT.Rows.Count > 0 Then
                'Agent address start
                my_table += "<table width='100%' border='0' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td  style='height:30px;padding: 2px 2px; color: #fff; background-color: gray; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #BBF1FF;'> &nbsp; CREDIT NODE NO :  " & RefundDT.Rows(0)("RefundID").ToString() & " </td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='height:30px;font-size: 18px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;ADDRESS : </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;  " & AgencyDT.Rows(0)("Agency_Name").ToString() & " </td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;  " & AgencyDT.Rows(0)("Address").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;  " & AgencyDT.Rows(0)("City").ToString() & ",&nbsp;" & AgencyDT.Rows(0)("State").ToString() & ",&nbsp;" & AgencyDT.Rows(0)("zipcode").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "<tr>"
                my_table += "<td style='height:20px;font-size: 13px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp; " & AgencyDT.Rows(0)("Country").ToString() & " </td>"
                my_table += "</tr>"

                my_table += "</table>"
                my_table += "</br>"
                'Agent address End

                'Credit Note Start
                my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"

                my_table += "<tr>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Order ID</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Booking ID</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Hotel Name</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Room Name</td>"
                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Star rating</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Booking Date</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>CheckIn Date</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>CheckOut Date</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Total</td>"
                my_table += "</tr>"

                my_table += "<tr>"
                ' Dim Total As Double = Convert.ToDouble(RefundDT.Rows(0)("Base_Fare").ToString()) + Convert.ToDouble(RefundDT.Rows(0)("Tax").ToString()) + Convert.ToDouble(RefundDT.Rows(0)("Service_Tax").ToString()) + Convert.ToDouble(RefundDT.Rows(0)("Tran_Fees").ToString())
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("OrderId").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("BookingID").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("HotelName").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("RoomName").ToString() & "</td>"
                'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("StarRating").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("BookingDate").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("CheckIN").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("CheckOut").ToString() & "</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("TotalCost").ToString() & "</td>"

                my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>"
                my_table += "<td colspan='7'  align='right'>Net Fare&nbsp;&nbsp;</td>"
                my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & RefundDT.Rows(0)("NetCost").ToString() & "</td>"
                my_table += "</tr>"
                Dim CancelCharge As Integer = RefundDT.Rows(0)("CancelCharge")
                my_table += "</tr>"
                my_table += "<td colspan='7' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>CANCELLATION CHARGE(-)&nbsp;&nbsp;</td>"
                my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='right'>" & CancelCharge.ToString() & "&nbsp;</td>"
                my_table += "</tr>"

                my_table += "</tr>"
                my_table += "<td colspan='7' style=' font-family: arial, Helvetica, sans-serif;font-size: 11px; line-height: 25px;color: #000000;' align='right'>SERVICE CHARGE(-)&nbsp;&nbsp;</td>"
                my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='right'>" & RefundDT.Rows(0)("ServiceCharge").ToString() & "&nbsp;</td>"
                my_table += "</tr>"

                Dim RefAmt As Integer = Convert.ToInt32(RefundDT.Rows(0)("NetCost")) - (CancelCharge + Convert.ToInt32(RefundDT.Rows(0)("ServiceCharge")))
                Dim GTInWord As New NumToWord.NumberToWord()
                my_table += "<tr style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'>"
                my_table += "<td align='center'>Amount in word  </td>"
                my_table += "<td colspan='5' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(RefAmt)) & " </td>"
                my_table += "<td   align='center'>Refund Amount</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #fff;' align='center'>" & RefAmt & "</td>"
                my_table += "</tr>"
                'Credit Note End
                my_table += "</table>"
            Else
                my_table += "<table width='100%' border='0'><tr>"
                my_table += "<td style='font-size: 13px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>"
                my_table += "Record not found </td></tr></table>"
            End If
            lbl_Detail.Text = my_table
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
