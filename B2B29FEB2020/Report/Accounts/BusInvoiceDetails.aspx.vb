Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.IO
Partial Class SprReports_Accounts_BusInvoiceDetails
    Inherits System.Web.UI.Page
    Private con As New SqlConnection()
    Private con1 As New SqlConnection()
    Private adp As SqlDataAdapter
    Private adap As SqlDataAdapter
    Private ds As New DataSet()
    Private TotalFare As Double = 0
    Private IRCTCCharge As Double = 0
    Private Pgatway As Double = 0
    Private AgentCharge As Double = 0
    Private GrandTotal As Double = 0
    Dim OrderId As String
    Dim canChrg As Double = 0
    Dim netfare As Double = 0
    Dim serviceChrg As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            OrderId = Request.QueryString("oid")
            BusInvoiceDetail()
        End If
    End Sub

    Public Sub BusInvoiceDetail()
        Try
            If con1.State = ConnectionState.Open Then
                con1.Close()
            End If
            con1.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim ds As New DataSet()
            adap = New SqlDataAdapter("SP_BUS_INVOICE", con1)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@orderid", OrderId)
            adap.Fill(ds)
            Dim dt1 As New DataTable()
            dt1 = ds.Tables(0)
            Dim Count As Integer = dt1.Rows.Count
            Dim ds1 As New DataSet()
            Dim dtAAdd As New DataTable()
            ds1 = SelectAgencyDetail(dt1.Rows(0)("AgentId"))
            dtAAdd = ds1.Tables(0)
            ViewState("AgentEmailId") = dtAAdd.Rows(0)("Email")

            Dim dtaddress As New DataTable
            Dim STDom As New SqlTransactionDom
            dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)

            Dim my_table As String = ""
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


            my_table += "</br>"



            my_table += "</table>"


            my_table += "</br>"



            my_table += "<table width='100%' border='0' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"
            my_table += "<tr>"
            my_table += "<td  style='height:30px;padding: 4px 2px; color: #fff; background-color: gray; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #BBF1FF;'>&nbsp;&nbsp;  INVOICE NO:  " & dt1.Rows(0)("ORDERID").ToString() & "</td>"
            my_table += "</tr>"
            my_table += "<tr>"
            my_table += "<td style='height:30px;font-size: 18px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'>  &nbsp;&nbsp;ADDRESS :<img alt='' src='http://b2b.ITZ.com/Images/arrow.jpg'  /> </td>"
            my_table += "</tr>"

            my_table += "<tr>"
            my_table += "<td style='height:20px;font-size: 15px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Agency_Name").ToString() & " </td>"
            my_table += "</tr>"
            my_table += "<tr>"
            my_table += "<td style='height:20px;font-size: 15px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Address").ToString() & " </td>"
            my_table += "</tr>"

            my_table += "<tr>"
            my_table += "<td style='height:20px;font-size: 15px; font-family: arial, Helvetica, sans-serif;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  " & dtAAdd.Rows(0)("Add1").ToString() & " </td>"
            my_table += "</tr>"

            my_table += "<tr>"
            my_table += "<td style='height:20px;font-size: 15px; font-family: arial, Helvetica, sans-serif;'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & dtAAdd.Rows(0)("Country").ToString() & " </td>"
            my_table += "</tr>"

            my_table += "</table>"
            my_table += "</br>"
            If Count > 0 Then
                my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"

                my_table += "<tr>"

                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Pax Name</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Ticket No.</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>PNR</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>From-To</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Sex</td>"
                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Age</td>"

                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>CurrentStatus</td>"
                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Quota</td>"
                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Coach</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Seat</td>"
                'my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Berth</td>"
                my_table += "<td style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Fare</td>"




                my_table += "</tr>"

                For Each dr As DataRow In dt1.Rows
                    my_table += "<tr>"

                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("PAXNAME").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("TICKETNO").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("PNR").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("SOURCE").ToString() & "-" & dt1.Rows(0)("DESTINATION").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("GENDER").ToString() & "</td>"
                    'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("Age").ToString() & "</td>"

                    'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("CurrentStatus").ToString() & "</td>"
                    'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("Quota").ToString() & "</td>"
                    'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("Coach").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("SEATNO").ToString() & "</td>"
                    'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("Berth").ToString() & "</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("ORIGINAL_FARE").ToString() & "</td>"

                    TotalFare += Convert.ToDouble(dr("ORIGINAL_FARE").ToString())
                    If (dr("CANCELLATIONCHARGE").ToString() <> "" AndAlso dr("REFUNDEDAMOUNT").ToString() <> "") Then
                        canChrg += Convert.ToDouble(dr("CANCELLATIONCHARGE").ToString())
                    End If
                    If (dr("ADMIN_MARKUP").ToString() <> "") Then
                        serviceChrg += Convert.ToDouble(Convert.ToDouble(dr("ADMIN_MARKUP").ToString()))
                    End If
                    my_table += "</tr>"
                Next
                my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>"
                my_table += "<td colspan='6'  align='right'>Total Fare&nbsp;</td>"

                my_table += "<td  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & TotalFare & "</td>"

                my_table += "</tr>"


                'my_table += "<tr  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>"
                'my_table += "<td colspan='11'  align='right'>IRCTC Charge&nbsp;</td>"
                'my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & IRCTCCharge & "</td>"
                'my_table += "</tr>"

                my_table += "<tr  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>"
                my_table += "<td colspan='6'  align='right'>Commission(-)&nbsp;</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Compute("SUM(ADMIN_COMM)", "") & "</td>"
                my_table += "</tr>"


                my_table += "<tr  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>"
                my_table += "<td colspan='6' align='right'>TDS(+)&nbsp;</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Compute("SUM(TA_TDS)", "") & "</td>"
                my_table += "</tr>"

                my_table += "<tr  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>"
                my_table += "<td colspan='6' align='right'>Service Charge&nbsp;</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & serviceChrg & "</td>"
                my_table += "</tr>"

                netfare = (TotalFare + Convert.ToDouble(dt1.Compute("SUM(TA_TDS)", ""))) - dt1.Compute("SUM(ADMIN_COMM)", "")
                netfare = netfare + serviceChrg
                my_table += "<tr  style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>"
                my_table += "<td colspan='6'  align='right'>Net Fare&nbsp;</td>"
                my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & netfare & "</td>"
                my_table += "</tr>"
                If (dt1.Rows(0)("CANCELLATIONCHARGE").ToString() <> "0" AndAlso dt1.Rows(0)("REFUNDEDAMOUNT").ToString() <> "0") Then
                    my_table += "<tr  style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>"
                    my_table += "<td colspan='6'  align='right'>Cancellation Charge(-)&nbsp;</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Compute("SUM(CANCELLATIONCHARGE)", "") & "</td>"
                    my_table += "</tr>"
                    GrandTotal = netfare
                    GrandTotal = GrandTotal - canChrg
                    Dim GTInWord As New NumToWord.NumberToWord()
                    my_table += "<tr style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'>"
                    my_table += "<td align='center'>Amount in word  </td>"
                    my_table += "<td colspan='4' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(GrandTotal)) & " </td>"
                    my_table += "<td   align='center'>Refund Amount</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #fff;' align='center'>" & GrandTotal & "</td>"
                    my_table += "</tr>"
                Else
                    GrandTotal = netfare
                    Dim GTInWord As New NumToWord.NumberToWord()
                    my_table += "<tr style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'>"
                    my_table += "<td align='center'>Amount in word  </td>"
                    my_table += "<td colspan='4' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(GrandTotal)) & " </td>"
                    my_table += "<td   align='center'>Grand Total</td>"
                    my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #fff;' align='center'>" & GrandTotal & "</td>"
                    my_table += "</tr>"
                End If
                my_table += "</table>"


                lbl_Detail.Text = my_table
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Function SelectAgencyDetail(ByVal UID As String) As DataSet
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT TOP (1) Agency_Name, Address, (City +', '+ zipcode +', '+ State) as Add1,Country,Email FROM  New_Regs where User_Id='" & UID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        Try
            Dim sw As New StringWriter()
            Dim w As New HtmlTextWriter(sw)
            div_invoice.RenderControl(w)
            Dim s As String = sw.GetStringBuilder().ToString()

            Dim STDOM As New SqlTransactionDom
            Dim MailDt As New DataTable
            MailDt = STDOM.GetMailingDetails(MAILING.BUS_INVOICE.ToString(), Session("UID").ToString()).Tables(0)

            Dim email As String = Request("txt_email")

            If (MailDt.Rows.Count > 0) Then
                Dim Status As Boolean = False
                Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                Try
                    If Status = True Then
                        Dim i As Integer = STDOM.SendMail(txt_email.Text, ViewState("AgentEmailId"), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), s, MailDt.Rows(0)("SUBJECT").ToString(), "")
                        If i = 1 Then
                            mailmsg.Text = "Mail sent successfully."
                        Else
                            mailmsg.Text = "Unable to send mail.Please try again"
                        End If
                    End If


                    txt_email.Text = ""

                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                    mailmsg.Text = ex.Message.ToString
                End Try
            Else
                mailmsg.Text = "Unable to send mail.Please contact to administrator"
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        
    End Sub
    Protected Sub btn_Word_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filename As String = ""
            'td_hide.Visible = true;
            filename = "InvoiceDetail.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            div_invoice.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_Excel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filename As String = ""
            ' td_hide.Visible = true;
            filename = "InvoiceDetail.xls"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            div_invoice.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub btn_PDF_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filename As String = ""
            'td_hide.Visible = true;
            filename = "PackageReport.pdf"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/pdf"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            lbl_Detail.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
End Class
