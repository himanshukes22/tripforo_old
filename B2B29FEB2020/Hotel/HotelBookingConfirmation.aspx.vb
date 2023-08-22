Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Xml
Imports System.IO
Imports HotelShared

Partial Class HotelBookingConfirmation
    Inherits System.Web.UI.Page
    Dim SearchDetails As New HotelBooking
    Dim HtlLogObj As New HotelBAL.HotelSendMail_Log()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx", True)
        End If

        If Not Page.IsPostBack Then
            Dim sBookingStatus As String = "", sConfId As String = ""
            SearchDetails = CType(Session("HotelBooking"), HotelBooking)
            Try
                If SearchDetails.BookingID = "" Then
                    HoldBookingTD.Visible = True
                End If
                If SearchDetails.Status = "Hold" Then
                    HoldBookingTD.Visible = True
                End If

                If SearchDetails.HotelErrorMsg <> Nothing Then
                    HoldBookingTD.InnerText = SearchDetails.HotelErrorMsg
                End If
                SearchDetails.HotelName = Session("HtlName").ToString()
                lblStatus.InnerText = SearchDetails.Status
                lblbookingID.InnerText = SearchDetails.BookingID
                lblBookingReference.InnerText = SearchDetails.ProBookingID
                lblBookingDate.Text = Now.Date.ToString("dd-MMM-yyyy")
                lblTotal2.InnerText = "INR " & SearchDetails.TotalRoomrate.ToString() & " /-"
                lblHotelName.Text = SearchDetails.HotelName
                lblRoomType.Text = SearchDetails.RoomName
                htlMealIncluded.InnerHtml = SearchDetails.MealInclude
                lblcheckin.Text = Session("sCheckIn1")
                lblCheckout.Text = Session("sCheckOut1")
                Try
                    If SearchDetails.ExtraRoom = 0 Then
                        lblRoom.Text = SearchDetails.NoofRoom
                    Else
                        lblRoom.Text = (SearchDetails.NoofRoom + SearchDetails.ExtraRoom).ToString()
                    End If
                Catch ex As Exception
                    lblRoom.Text = SearchDetails.NoofRoom
                End Try

                lblAdults.Text = SearchDetails.TotAdt
                lblChilds.Text = SearchDetails.TotChd
                TDGuestName.InnerText = SearchDetails.PGTitle & " " & SearchDetails.PGFirstName & " " & SearchDetails.PGLastName
                TDGuestMobile.InnerText = SearchDetails.PGContact
                lblEmail.Text = SearchDetails.PGEmail
                htlPhone.InnerText = SearchDetails.HotelContactNo
                htlFax.InnerText = SearchDetails.HotelAddress
                TdOrderid.InnerText = SearchDetails.Orderid
                lblnight.Text = SearchDetails.NoofNight.ToString()

                Dim HotelPolicy As String = "<span style='font-size:13px;font-weight: bold;'>HOTEL POLICIES</span><span>"
                HotelPolicy += "<li> As per Government regulations, it is mandatory for all guests above 18 years of age to carry a valid photo identity card & address proof at the time of check-in. Please note that failure to abide by this can result with the hotel denying a check-in. Hotels normally do not provide any refund for such cancellations.</li>"
                HotelPolicy += "<li> The standard check-in and check-out times are 12 noon. Early check-in or late check-out is subject to hotel availability and may also be chargeable by the hotel. Any early check-in or late check-out request must be directed to and reconfirmed with the hotel directly.</li>"
                HotelPolicy += "<li> Failure to check-in to the hotel, will attract the full cost of stay or penalty as per the hotel cancellation policy.</li>"
                HotelPolicy += "<li> All additional charges other than the room charges and inclusions as mentioned in the booking voucher are to be borne and paid separately during check-out. Please make sure that you are aware of all such charges that may comes as extras. Some of them can be WiFi costs, Mini Bar, Laundry Expenses, Telephone calls, Room Service, Snacks etc.</li>"
                HotelPolicy += "<li> Any changes or booking modifications are subject to availability and charges may apply as per the hotel policies.</li></span>"

                lblRules.Text = HttpUtility.HtmlDecode(SearchDetails.HotelPolicy + HotelPolicy)
               
                SearchDetails.HtlCode = "Sorry Back Button Press"
                Try
                    Dim TicketCopy As String = HtlLogObj.TicketSummury(SearchDetails.Orderid, "https://www.RWT.co")
                    BookingCopy.Text = TicketCopy
                    'HtlLogObj.SendMailCopy(SearchDetails, TicketCopy, "https://www.RWT.co")

                    Dim bookingstaus As String = ""
                    If SearchDetails.Status = StatusClass.Confirm.ToString() Then
                        bookingstaus = "Book"
                    ElseIf SearchDetails.Status = StatusClass.Hold.ToString() Then
                        bookingstaus = "Hold"
                    ElseIf SearchDetails.Status = StatusClass.Rejected.ToString() Then
                        bookingstaus = "Reject"
                    End If
                    Dim objSMSAPI As New SMSAPI.SMS
                    Dim objSqlNew As New SqlTransactionNew
                    Dim smstext As String = ""
                    Dim SmsCrd As DataTable
                    Dim objDA As New SqlTransaction
                    SmsCrd = objDA.SmsCredential(SMS.HOTELBOOKING.ToString()).Tables(0)
                    Dim smsStatus As String = ""
                    Dim smsMsg As String = ""
                    If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                        smsStatus = objSMSAPI.SendHotelSms(SearchDetails.Orderid, SearchDetails.BookingID, SearchDetails.PGContact.ToString().Trim(), SearchDetails.HotelName + ", " + SearchDetails.CityName.ToString().Trim(), SearchDetails.CheckInDate, SearchDetails.CheckOutDate, bookingstaus, smstext, SmsCrd)
                        objSqlNew.SmsLogDetails(SearchDetails.Orderid, SearchDetails.PGContact.ToString().Trim(), smstext, smsStatus)
                    End If
                Catch ex As Exception
                    HotelDAL.HotelDA.InsertHotelErrorLog(ex, " Send SMS Agency Booking")
                End Try
            Catch ex As Exception
                HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
            End Try
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
            Response.Cache.SetNoStore()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, Session("UID").ToString())
        End Try
    End Sub
End Class


