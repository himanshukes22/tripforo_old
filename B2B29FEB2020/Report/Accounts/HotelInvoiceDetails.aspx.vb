Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.IO
Partial Class SprReports_Accounts_HotelInvoiceDetails
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()
    Dim sql As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '.Tables(0)
            Try
                Dim hotelds As New DataSet()
                hotelds = ST.htlintsummary(Request.QueryString("OrderId"), "Ticket")

                If hotelds.Tables.Count > 0 Then
                    If hotelds.Tables(0).Rows.Count > 0 Then
                        ShowInvoice(hotelds.Tables(0), hotelds.Tables(1))
                        AgentAddress()
                        InvoiceDetail(hotelds.Tables(0))
                        Dim dtaddress As New DataTable
                        Dim STDom As New SqlTransactionDom
                        dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)
                        td_CNAME.InnerText = dtaddress.Rows(0)("COMPANYNAME")
                        td_CADDRESS.InnerText = dtaddress.Rows(0)("COMPANYADDRESS")
                        ' td_CITYZIP.InnerText = dtaddress.Rows(0)("CITY") & "-" & dtaddress.Rows(0)("ZIPCODE")
                        td_PHONE.InnerText = dtaddress.Rows(0)("PHONENO")
                        td_EMAIL.InnerText = dtaddress.Rows(0)("EMAIL")
                    Else
                        lbl_IntInvoice.Text = "No record found."
                    End If
                Else
                    lbl_IntInvoice.Text = "No record found."
                End If
            Catch ex As Exception
                HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
            End Try
        End If
    End Sub
    Public Sub ShowInvoice(ByVal dt As DataTable, ByVal guestDT As DataTable)
        Try

            If (Request.QueryString("OrderId").ToString() <> "" AndAlso Request.QueryString("OrderId").ToString() IsNot Nothing) Then
                Dim id As String = Request.QueryString("OrderId").ToString()
                'guestDT = ST.htlintsummary(Request.QueryString("OrderId"), "GetGuest").Tables(0)
                Dim my_table As String = "" 'background-color: #004b91;background:#eee;
                my_table = "<table border='1' cellspacing='0' cellpadding='0'style='border: thin solid #999999; border=collapse:collapse;width:100%;' >"
                my_table += "<tr>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>Guest&nbsp;Details</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>Hotel&nbsp;Name</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>Room&nbsp;Name</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>Room</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>CheckIN</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>CheckOut</td>"
                my_table += "<td style='padding: 2px 2px;  background-color: #eee;font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;' align='center'>Amount</td>"
                my_table += "</tr>"
                'For Each dr As DataRow In dt.Rows
                my_table += "<tr>"
                my_table += "<td style='padding-left:4px;' > " & guestDT.Rows(0)("GTitle").ToString() & " " & guestDT.Rows(0)("GFName").ToString() & " " & guestDT.Rows(0)("GLName").ToString() & " </br> " & guestDT.Rows(0)("Address").ToString() & " " & guestDT.Rows(0)("City").ToString() & "</td>"
                my_table += "<td style='padding-left:4px;' >" & dt.Rows(0)("HotelName").ToString() & " </br>" & dt.Rows(0)("Address").ToString() & " </br>" & dt.Rows(0)("CityName").ToString() & "</td>"
                my_table += "<td style='padding-left:4px;'>" & dt.Rows(0)("RoomName").ToString() & "</td>"
                my_table += "<td style='padding-left:4px;' align='center'>" & dt.Rows(0)("RoomCount").ToString() & "</td>"
                my_table += "<td style='padding-left:4px;'>" & dt.Rows(0)("CheckIN").ToString().Substring(0, 10) & " </td>"
                my_table += "<td style='padding-left:4px;'>" & dt.Rows(0)("CheckOut").ToString().Substring(0, 10) & " </td>"
                my_table += "<td style='padding-left:4px;'>" & dt.Rows(0)("TotalCost").ToString() & "</td>"
                ' Amount
                my_table += "</tr>"
                Dim GTInWord As New NumToWord.NumberToWord()
                my_table += "<tr  style='padding: 2px 2px; background-color: #eee; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif;'>"
                my_table += "<td colspan='5'>Amount in word:" & GTInWord.AmtInWord(Convert.ToDecimal(dt.Rows(0)("TotalCost").ToString())) & "</td>"
                my_table += "<td>Total Cost</td>"
                my_table += "<td >" & dt.Rows(0)("TotalCost").ToString() & "</td>"
                my_table += "</tr>"
                ' Next
                my_table += "</table>"
                lbl_IntInvoice.Text = my_table
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub btn_PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_PDF.Click
        Dim filename As String = ""
        filename = "PackageReport.pdf"
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
        Response.Charset = ""
        Response.ContentType = "application/pdf"
        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        div_invoice.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.[End]()
    End Sub

    Protected Sub btn_Word_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Word.Click
        Try
            Dim filename As String = ""
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
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Excel.Click
        Try
            Dim filename As String = ""
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
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        Try
            Dim sw As New StringWriter()
            Dim w As New HtmlTextWriter(sw)
            div_invoice.RenderControl(w)
            Dim s As String = sw.GetStringBuilder().ToString()
            Dim STDOM As New SqlTransactionDom
            Dim MailDt As New DataTable
            MailDt = STDOM.GetMailingDetails(MAILING.HOTEL_INVOICE.ToString(), Session("UID").ToString()).Tables(0)
            If (MailDt.Rows.Count > 0) Then
                Dim Status As String
                Status = MailDt.Rows(0)("Status").ToString()
                If Status = True Then
                    Dim i As Integer = STDOM.SendMail(txt_email.Text, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), s, MailDt.Rows(0)("SUBJECT").ToString(), "")
                    If i = 1 Then
                        mailmsg.Text = "Mail sent successfully."
                    Else
                        mailmsg.Text = "Unable to send mail.Please try again"
                    End If
                End If
                txt_email.Text = ""
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
            mailmsg.Text = "Undelivered."
        End Try
    End Sub
    Public Sub AgentAddress()
        Dim dt As New DataTable()
        Try
            dt = sql.GetAgencyDetails(Request("AgentID")).Tables(0)
            td_AgName.InnerText = dt.Rows(0)("Agency_Name").ToString()
            td_Address.InnerText = dt.Rows(0)("Address").ToString()
            td_Add1.InnerText = dt.Rows(0)("City").ToString & "-" & dt.Rows(0)("Zipcode").ToString & "," & dt.Rows(0)("State").ToString
            td_country.InnerText = dt.Rows(0)("Country").ToString()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Public Sub InvoiceDetail(ByVal dt As DataTable)
        Try
            lblinvoiceno.Text = dt.Rows(0)("OrderId").ToString()
            lblInvoiceDate.Text = dt.Rows(0)("BookingDate").ToString()
            lblPartyCode.Text = dt.Rows(0)("LoginID").ToString()
            lblBookingID.Text = dt.Rows(0)("BookingID").ToString()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
