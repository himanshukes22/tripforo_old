Imports System.Collections.Generic

Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.IO

Partial Class SprReports_Accounts_CreditNoteRail
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
    Dim tRefund As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            IRCTCCompleteInvoiceDetail()
        End If

    End Sub



    Public Sub IRCTCCompleteInvoiceDetail()


        Try

            Dim my_table As String = ""
            lbl_PNR.Text = Request.QueryString("PNRNO")
            Dim dt As New DataTable()
            Dim dt1 As New DataTable()
            Dim dtAAdd As New DataTable()
            Dim dtRefund As New DataTable()

            ds = getData(lbl_PNR.Text)
            If ds.Tables.Count = 4 Then
                Dim Amt As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("TicketFare").ToString())
                Dim Count As Integer = ds.Tables(1).Rows.Count
                dtAAdd = ds.Tables(2)
                dt = ds.Tables(1)
                dt1 = ds.Tables(0)
                dtRefund = ds.Tables(3)
                Dim dtaddress As New DataTable
                Dim STDom As New SqlTransactionDom
                dtaddress = STDom.GetCompanyAddress(ADDRESS.RAIL.ToString().Trim()).Tables(0)
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
                my_table += "</br>"

                my_table += "<table width='100%' border='0' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='100%'>"
                my_table += "<tr>"
                my_table += "<td style='height:30px;padding: 4px 2px; color: #fff; background-color: gray; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; '>&nbsp;&nbsp;  INVOICE NO:  " & dt1.Rows(0)("SLNO").ToString() & "</td>"
                my_table += "<td align='right'; style='height:30px;padding: 4px 2px; color: #fff; background-color: gray; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; '>&nbsp;&nbsp; " & Now() & "</td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='height:30px;font-size: 18px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'>  &nbsp;&nbsp;ADDRESS :<img alt='' src='Images/arrow.jpg'  /> </td>"
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

                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Ticket No.</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>PNR</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>T. Passenger</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Train No</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>From-To</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>DOJ</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Fare</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>IRCTC Sr. Charge</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Agent Charge</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>PG Charge</td>"
                    my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Total Amount</td>"
                    my_table += "</tr>"

                    IRCTCCharge = Convert.ToDouble(dt1.Rows(0)("IRCTCCharge").ToString())
                    Pgatway = Convert.ToDouble((dt1.Rows(0)("PaymentGatewayCharge").ToString()))
                    If (Request.QueryString("CC")) Is Nothing Then
                        AgentCharge = Convert.ToDouble((dt1.Rows(0)("Agent_Charge").ToString()))
                    Else
                        AgentCharge = Convert.ToDouble((dt1.Rows(0)("CAgent_Charge").ToString()))
                    End If
                    GrandTotal = Amt + IRCTCCharge + Pgatway + AgentCharge
                    For Each dr As DataRow In dt.Rows
                        my_table += "<tr>"

                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("TicketNo").ToString() & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("PNR").ToString() & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Count & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("TrainNo").ToString() & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("FromTo").ToString() & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dt1.Rows(0)("DOJ").ToString() & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Amt & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & IRCTCCharge & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & AgentCharge & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & Pgatway & "</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & GrandTotal & "</td>"

                        my_table += "</tr>"
                        Exit For
                    Next
                    If dtRefund.Rows.Count > 0 Then
                        my_table += "<tr><td></td></tr>"
                        my_table += "<tr>"
                        my_table += "<td colspan='10' style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Refund Date</td>"
                        my_table += "<td style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;' align='center'>Refund Amount</td>"
                        my_table += "</tr>"

                        For Each dr As DataRow In dtRefund.Rows
                            my_table += "<tr>"
                            my_table += "<td colspan='10' style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("CreatedDate").ToString() & "</td>"
                            my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #000000;' align='center'>" & dr("Credit").ToString() & "</td>"
                            my_table += "</tr>"
                            tRefund += Convert.ToDouble(dr("Credit").ToString())
                        Next
                        Dim GTInWord As New NumToWord.NumberToWord()
                        my_table += "<tr style='padding: 4px 2px; color: #fff; background-color:#004b91; font-size: 15px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color:#004b91;'>"
                        my_table += "<td align='center'>Amount in word  </td>"
                        my_table += "<td colspan='8' align='left'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(tRefund)) & " </td>"
                        my_table += "<td   align='center'>Grand Total</td>"
                        my_table += "<td style=' font-family: arial, Helvetica, sans-serif;font-size: 13px; line-height: 25px;color: #fff;' align='center'>" & tRefund & "</td>"
                        my_table += "</tr>"

                    End If
                    
                    
                    my_table += "</table>"
                    lbl_Detail.Text = my_table
                End If
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
    Public Sub SendMail()
       

        Try
            Dim sw As New StringWriter()
            Dim w As New HtmlTextWriter(sw)
            div_invoice.RenderControl(w)
            Dim s As String = sw.GetStringBuilder().ToString()
            Dim MailDt As New DataTable
            Dim STDOM As New SqlTransactionDom
            MailDt = STDOM.GetMailingDetails(MAILING.AIR_INVOICE.ToString(), Session("UID").ToString()).Tables(0)
            Dim email As String = Request("txt_email")
            If (MailDt.Rows.Count > 0) Then
                Dim Status As Boolean = False
                Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                Try
                    If Status = True Then
                        Dim i As Integer = STDOM.SendMail(txt_email.Text, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), s, MailDt.Rows(0)("SUBJECT").ToString(), "")
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
    Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SendMail()
    End Sub



    'Code For IRCTC Invoice

    Public Function CountName(ByVal PNR As String) As DataSet

        If con1.State = ConnectionState.Open Then
            con1.Close()
        End If
        con1.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("select (resp.value('@name','varchar(100)')) as count from dbo.TRAIN_BOOKED_DETAILS CROSS APPLY irctc_response.nodes('//PassengerDetail') AS R(resp) where PNR ='" & PNR & "'", con1)
        adap.Fill(ds)
        Return ds


    End Function
    Public Function SelectName(ByVal PNR As String) As DataTable

        If con1.State = ConnectionState.Open Then
            con1.Close()
        End If
        con1.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select (resp.value('@name','varchar(100)')) as Name,(resp.value('@sex','varchar(10)')) as Sex,(resp.value('@age','varchar(3)')) as Age,(resp.value('@bookingStatus','varchar(50)')) as BookingStatus,(resp.value('@currentStatus','varchar(50)')) as CurrentStatus,(resp.value('@coach','varchar(50)')) as Coach,(resp.value('@seat','varchar(50)')) as Seat,(resp.value('@berth','varchar(50)')) as Berth from dbo.TRAIN_BOOKED_DETAILS CROSS APPLY irctc_response.nodes('//PassengerDetail') AS R(resp) where PNR ='" & PNR & "'", con1)
        adap.Fill(dt)
        Return dt


    End Function



    Public Function SelectUID(ByVal PNR As String) As DataTable
        If con1.State = ConnectionState.Open Then
            con1.Close()
        End If
        con1.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select distinct Account_no from dbo.TRAIN_BOOKED_DETAILS  where PNR ='" & PNR & "'", con1)
        adap.Fill(dt)
        Return dt


    End Function


    Public Function SlectIRCTCInvoice(ByVal PNR As String) As DataTable
        If con1.State = ConnectionState.Open Then
            con1.Close()
        End If
        con1.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select  SLNO,TID as TransID,resp.value('@pnrNumber','varchar(10)') as PNR,resp.value('@ticketNumber','varchar(20)') as TicketNo,resp.value('@source','varchar(20)') +'/'+ resp.value('@destination','varchar(20)') as FromTo,(resp.value('@ticketFare','varchar(10)')) as TicketFare,(resp.value('@trainNo','varchar(50)')) as TrainNo,(resp.value('@boardingDate','varchar(50)')) as DOJ,IRCTC_CHARGES as IRCTCCharge,PGI_CHARGE as PaymentGatewayCharge,Agent_Charge,Transaction_amount as TotalAmount,Transaction_Date ,Agent_Name,Agent_mobile_no,Account_no,(resp.value('@ticketFare','varchar(10)')) as TicketFare,(resp.value('@quota','varchar(10)')) as Quota,(CAST((resp.value('@ticketFare','varchar(10)')) as float)+CAST(IRCTC_CHARGES as float)+ CAST(PGI_CHARGE as float)+ CAST(Agent_Charge as float)) as TotalAmount from train_booked_details CROSS APPLY irctc_response.nodes('//PrsReservOutput') as R(resp) where PNR ='" & PNR & "'", con1)
        adap.Fill(dt)
        Return dt


    End Function
    Public Function SelectAgencyDetail(ByVal UID As String) As DataSet
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT TOP (1) Agency_Name, Address, (City +', '+ zipcode +', '+ State) as Add1,Country FROM  New_Regs where User_Id='" & UID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
    Public Function IRCTCINVOICE(ByVal PNR As String, ByVal Type As String) As DataTable

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString
        Dim dtIRCTC As New DataTable

        adp = New SqlDataAdapter("GETIRCTCINVOICEDTL", con)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        adp.SelectCommand.Parameters.AddWithValue("@PNR", PNR)
        adp.SelectCommand.Parameters.AddWithValue("@TYPE", Type)
        adp.Fill(dtIRCTC)
        Return dtIRCTC


    End Function

    Public Function getData(ByVal PNR As String) As DataSet
        Try

            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDBIRCTC").ConnectionString
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            Dim adp As New SqlDataAdapter
            adp = New SqlDataAdapter("SP_IRC_GETINVOICEDETAILS", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@PNR", PNR)
            adp.Fill(ds)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return ds
        Catch
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Function
End Class
