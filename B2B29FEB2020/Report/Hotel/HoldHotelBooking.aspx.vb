Imports System.Data
Imports HotelBAL
Imports HotelShared
Partial Class SprReports_Hotel_HoldHotelBooking
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", True)
            End If

            If Not IsPostBack Then
                Dim grdds As New DataSet()
                'grdds = ST.GetHoldHotel("", "Hold", "")
                grdds = ST.HotelSearchRpt("02/02/2019 12:00:00 AM", "", "", "", "", "", "", "", "", "Hold", Session("UID").ToString, Session("User_Type").ToString())
                Bindgrid(grdds)
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Private Sub Bindgrid(ByVal ds As DataSet)
        Try
            Dim dv1 As DataView = ds.Tables(0).DefaultView
            dv1.RowFilter = "Provider = 'ZUMATA' OR Provider = 'INNSTANT'"
            GrdReport.DataSource = dv1.ToTable
            GrdReport.DataBind()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub lnkCheckHotelStatus_Click(sender As Object, e As EventArgs)
        Try
            Dim lb As LinkButton = CType(sender, LinkButton)
            'Color Selected Grid View row
            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
            gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
            Dim HtlDetailsDs As New DataSet()
            HtlDetailsDs = ST.htlintsummary(lb.CommandArgument, "Ticket")

            If (HtlDetailsDs.Tables.Count > 0) Then
                Dim HotelDT As New DataTable()
                HotelDT = HtlDetailsDs.Tables(0)
                If HotelDT.Rows.Count > 0 Then
                    Dim HotelDetails As New HotelBooking()
                    HotelDetails.ProBookingID = HotelDT.Rows(0)("BookingID").ToString()
                    HotelDetails.BookingID = HotelDT.Rows(0)("BookingID").ToString()
                    HotelDetails.Orderid = HotelDT.Rows(0)("OrderID").ToString()
                    HotelDetails.Provider = HotelDT.Rows(0)("Provider").ToString()
                    If HotelDetails.Provider = "ZUMATA" Then
                        HotelDetails.Provider = "ZUMATABooking"
                    End If
                    Dim objcancel As New HotelBAL.CommonHotelBL()
                    HotelDetails = objcancel.CheckHotelBookingStaus(HotelDetails)
                    Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
                    If page IsNot Nothing Then
                        ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & HotelDetails.Status & "');", True)
                    End If
                    If HotelDetails.Status = "Booking Confirm" Then
                        ST.UpdateHrdHold(HotelDetails.Orderid, HotelDetails.BookingID, HotelDT.Rows(0)("ConfirmationNo").ToString(), "Confirm", Session("UID").ToString(), "Confirm from Check Hotel Status")
                        Dim grdds As New DataSet()
                        grdds = ST.HotelSearchRpt("02/02/2019 12:00:00 AM", "", "", "", "", "", "", "", "", "Hold", Session("UID").ToString(), Session("User_Type").ToString())
                        Bindgrid(grdds)
                        Try
                            ''''Send mail and SMS start
                            Dim objmail As New HotelBAL.HotelSendMail_Log()
                            ''objmail.SendEmailForCancelAndReject(HotelDetails.Orderid, HotelDetails.BookingID, HotelDT.Rows(0)("HotelName").ToString().Trim() + ", " + HotelDT.Rows(0)("CityName").ToString().Trim(), Session("UID").ToString(), HotelStatus.HOTEL_BOOKING.ToString(), HotelStatus.Confirm.ToString())

                            Dim objSMSAPI As New SMSAPI.SMS
                            Dim objSqlNew As New SqlTransactionNew
                            Dim smstext As String = ""
                            Dim SmsCrd As DataTable
                            Dim objDA As New SqlTransaction
                            SmsCrd = objDA.SmsCredential(SMS.HOTELBOOKING.ToString()).Tables(0)
                            Dim smsStatus As String = ""
                            Dim smsMsg As String = ""
                            If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                smsStatus = objSMSAPI.SendHotelSms(HotelDetails.Orderid, HotelDetails.BookingID, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), HotelDT.Rows(0)("HotelName").ToString().Trim() + ", " + HotelDT.Rows(0)("CityName").ToString().Trim(), HotelDT.Rows(0)("CheckIN").ToString().Trim(), HotelDT.Rows(0)("CheckOut").ToString().Trim(), "Book", smstext, SmsCrd)
                                objSqlNew.SmsLogDetails(HotelDetails.Orderid, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), smstext, smsStatus)
                            End If
                            ''''Send mail and SMS End
                        Catch ex As Exception
                            HotelDAL.HotelDA.InsertHotelErrorLog(ex, " Send SMS Agency Hold status")
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
