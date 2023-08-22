Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.IO
Imports System.Net.Mail

Partial Class Hotel_BookingSummaryHtl
    Inherits System.Web.UI.Page
    Dim HtlLogObj As New HotelBAL.HotelSendMail_Log()
    Dim BookingCopy As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' BookingCopy = HtlLogObj.TicketSummury(Request("OrderId"), Request.Url.Scheme & "://" & Request.Url.Authority & "/TEST")
            BookingCopy = HtlLogObj.TicketSummury(Request("OrderId"), Request.Url.Scheme & "://" & Request.Url.Authority)
            lbl_summary.Text = BookingCopy
            Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "generateBarcode('" & Request("OrderId") & "'); ", True)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ' Dim i As Integer = HtlLogObj.SendEmail(txt_email.Text, BookingCopy, Session("UID").ToString(), Request.Url.Scheme & "://" & Request.Url.Authority & "/TEST")
            Dim i As Integer = HtlLogObj.SendEmail(txt_email.Text, BookingCopy, Session("UID").ToString(), Request.Url.Scheme & "://" & Request.Url.Authority)
            If i = 1 Then

                mailmsg.Text = " Send Successfully"
                txt_email.Text = " "

                txt_email.Focus()
            Else
                mailmsg.Text = " Undelivered"
                txt_email.Focus()
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
