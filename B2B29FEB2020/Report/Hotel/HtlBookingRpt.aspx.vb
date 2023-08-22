Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports HotelShared
Imports System.Configuration.ConfigurationManager

Partial Class SprReports_Hotel_HtlBookingRpt
    Inherits System.Web.UI.Page
    Private OTPST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private ST As New HotelDAL.HotelDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", True)
            End If

            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    TDAgency.Visible = False
                End If
                Dim GrdDS As New DataSet()
                GrdDS = ST.HotelSearchRpt("", "", "", "", "", "", "", "", "", "Confirm", Session("UID").ToString, Session("User_Type").ToString)
                BindSales(GrdDS)
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            Dim GrdDS As New DataSet()
            GrdDS = GetReportData()
            Bindgrid(GrdDS)
            BindSales(GrdDS)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Private Sub BindSales(ByVal GrdDS As DataSet)
        Try
            Dim dt As DataTable
            Dim Db As String = ""
            Dim sum As Double = 0
            dt = GrdDS.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Db = dr("TotalCost").ToString()
                    If Db Is Nothing OrElse Db = "" Then
                        Db = 0
                    Else
                        sum += Db
                    End If
                Next
            End If
            lbl_Total.Text = "0"
            If sum <> 0 Then
                lbl_Total.Text = sum.ToString
            End If
            lbl_counttkt.Text = dt.Rows.Count
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Private Sub Bindgrid(ByVal ds As DataSet)
        Try
            GrdReport.DataSource = ds
            GrdReport.DataBind()
            If Session("User_Type").ToString = "AGENT" Then
                GrdReport.Columns(10).Visible = True
            Else
                GrdReport.Columns(10).Visible = False
            End If
            If ddlStatus.SelectedIndex > 0 Then
                GrdReport.Columns(10).Visible = False
            End If
            Session("Grdds") = ds
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Function GetReportData() As DataSet
        Dim GrdDS As New DataSet()
        Dim FromDate As String = "", ToDate As String = "", Trip As String = "", Checkin As String = "", status As String = "Confirm"
        Try
            If Not [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Strings.Mid(Request("From").Split(" ")(0), 4, 2) + "/" + Strings.Left(Request("From").Split(" ")(0), 2) + "/" + Strings.Right(Request("From").Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If Not [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            If Not [String].IsNullOrEmpty(Request("Checkin")) Then
                Dim chkdate() As String = Request("Checkin").Split("-")
                Checkin = chkdate(2) + "-" + chkdate(1) + "-" + chkdate(0)
            End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim BookingrID As String = If([String].IsNullOrEmpty(txt_bookingID.Text), "", txt_bookingID.Text.Trim)
            Dim orderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim HtlName As String = If([String].IsNullOrEmpty(txt_htlcode.Text), "", txt_htlcode.Text.Trim)
            Dim RoomName As String = If([String].IsNullOrEmpty(txt_roomcode.Text), "", txt_roomcode.Text.Trim)
            If Triptype.SelectedIndex > 0 Then
                Trip = Triptype.SelectedValue
            End If
            If ddlStatus.SelectedIndex > -1 Then
                status = ddlStatus.SelectedValue
            End If
            GrdDS = ST.HotelSearchRpt(FromDate, ToDate, BookingrID, orderID, HtlName, RoomName, Trip, AgentID, Checkin, status, Session("UID").ToString(), Session("User_Type").ToString())
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return GrdDS
    End Function
    Protected Sub lnkRefund_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lb As LinkButton = CType(sender, LinkButton)
            'Color Selected Grid View row
            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
            gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
            Dim HtlDetailsDs As New DataSet()
            Dim HtlDetails As New DataTable()
            HtlDetailsDs = ST.htlintsummary(lb.CommandArgument, "CancelData")
            If (HtlDetailsDs.Tables.Count > 0) Then
                HtlDetails = HtlDetailsDs.Tables(0)
                ViewState("HtlDetails") = HtlDetailsDs
                If HtlDetails.Rows.Count > 0 Then
                    'Checking in HtlRefund Table it present in table or not
                    Dim status As Integer = ST.CheckHtlRefuStaus(HtlDetails.Rows(0)("BookingID"), HtlDetails.Rows(0)("OrderID").ToString())
                    Select Case status
                        Case 1
                            ShowAlertMessage("This Booking ID Allready Cancelled")
                        Case 2
                            ShowAlertMessage("This Booking ID is Pending for Cancellation")
                        Case 3
                            ShowAlertMessage("This Booking ID is Allready in Cancellation InProcess")
                        Case Else
                            If HtlDetails.Rows(0)("ModifyStatus").ToString() = "PartialCancel" Then
                                amt.InnerText = (Convert.ToDecimal(HtlDetails.Rows(0)("NetCost")) + Convert.ToDecimal(HtlDetails.Rows(0)("PgCharges")) - Convert.ToDecimal(HtlDetails.Rows(0)("RefundAmt"))).ToString()
                            Else
                                amt.InnerText = (Convert.ToDecimal(HtlDetails.Rows(0)("NetCost")) + Convert.ToDecimal(HtlDetails.Rows(0)("PgCharges"))).ToString()
                            End If
                            If HtlDetails.Rows(0)("ModifyStatus").ToString() = "Cancelled" Then
                                ShowAlertMessage("Ticket all ready canceled.")
                            Else
                                HotelName.InnerText = HtlDetails.Rows(0)("HotelName")
                                room.InnerText = HtlDetails.Rows(0)("RoomCount")
                                night.InnerText = HtlDetails.Rows(0)("NightCount")
                                adt.InnerText = HtlDetails.Rows(0)("AdultCount")
                                chd.InnerText = HtlDetails.Rows(0)("ChildCount")
                                policy.InnerHtml = "<span style='font-size:13px;font-weight: bold;'>CANCELLATION POLICIES</span>" & HtlDetails.Rows(0)("CancellationPoli")
                                RemarkTitle.InnerText = "Hotel Cancellation for order id (" & lb.CommandArgument & ")"
                                OrderIDS.Value = lb.CommandArgument
                                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "ShowHide('show','" & HtlDetails.Rows(0)("CheckIN").ToString() & "'); ", True)
                            End If
                    End Select
                End If
            Else
                ShowAlertMessage("We can not process Cancellation.")
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub btn_Refund_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Refund.Click
        Try
            Dim HtlDetailsDs As New DataSet()
            HtlDetailsDs = ViewState("HtlDetails")
            Dim HotelDT As New DataTable()
            HotelDT = HtlDetailsDs.Tables(0)

            If HotelDT.Rows.Count > 0 Then
                'Checking in Hotel Cancellation Table it is present in table or not
                Dim status As Integer = ST.CheckHtlRefuStaus(HotelDT.Rows(0)("BookingID").ToString(), OrderIDS.Value)
                Select Case status
                    Case 0
                        Try
                            Dim AdminMrk As Double = 0, fromcCancelDate As String = "", PreviousRfndAmt As Decimal = 0
                            Dim SearchDetails As New HotelCancellation
                            SearchDetails.BookingID = HotelDT.Rows(0)("BookingID").ToString()
                            SearchDetails.Orderid = OrderIDS.Value
                            SearchDetails.CheckInDate = HotelDT.Rows(0)("CheckIN").ToString()
                            SearchDetails.PGLastName = HtlDetailsDs.Tables(1).Rows(0)("GLName").ToString()
                            SearchDetails.PGEmail = HtlDetailsDs.Tables(1).Rows(0)("GEmail").ToString()
                            SearchDetails.AgentID = HotelDT.Rows(0)("Loginid").ToString()
                            SearchDetails.AgencyName = HotelDT.Rows(0)("Loginid").ToString()
                            SearchDetails.CancelStatus = StatusClass.Pending.ToString()
                            SearchDetails.NoofNight = HotelDT.Rows(0)("NightCount").ToString()
                            SearchDetails.Provider = HotelDT.Rows(0)("Provider").ToString()
                            SearchDetails.AdminMrkAmt = Convert.ToDecimal(HotelDT.Rows(0)("AdminMrkAmt"))
                            SearchDetails.AdminMrkPer = Convert.ToDecimal(HotelDT.Rows(0)("AdminMrkPer"))
                            SearchDetails.AdminMrkType = HotelDT.Rows(0)("AdminMrkType").ToString()
                            SearchDetails.ConfirmationNo = HotelDT.Rows(0)("ConfirmationNo").ToString()
                            SearchDetails.HotelCode = HotelDT.Rows(0)("HotelCode").ToString()
                            SearchDetails.HotelName = HotelDT.Rows(0)("HotelName").ToString()
                            SearchDetails.HtlType = HotelDT.Rows(0)("TripType").ToString()
                            SearchDetails.LoginID = Session("UID").ToString()
                            SearchDetails.CancellationRemark = Request("txtRemarkss").Trim()
                            SearchDetails.Total_Org_Roomrate = Math.Round(Convert.ToDecimal(HotelDT.Rows(0)("BookingAmt")), 2)
                            SearchDetails.CancellationType = "ALL"
                            If SearchDetails.Provider = "TG" Then
                                SearchDetails.Provider = "TGBooking"
                            ElseIf SearchDetails.Provider = "GTA" Then
                                SearchDetails.CurrancyRate = Convert.ToDecimal(HotelDT.Rows(0)("ExchangeRate"))
                            ElseIf SearchDetails.Provider = "SuperShopper" Or SearchDetails.Provider = "GAL" Then
                                SearchDetails.ReferenceNo = HotelDT.Rows(0)("UinversalLocator").ToString()
                            End If

                            Dim orgbookingrate As Decimal = Math.Round(Convert.ToDecimal(HotelDT.Rows(0)("BookingAmt")), 0)
                            'Insert Cancle Data in HotelCancellation Table
                            Dim i As Integer = ST.InsHtlRefund(SearchDetails.Orderid, Session("UID").ToString(), StatusClass.Pending.ToString(), 0, 0, Convert.ToInt32(HotelDT.Rows(0)("NightCount")), "", Request("txtRemarkss").Trim(), HtlDetailsDs.Tables(1).Rows(0)("GTitle").ToString(), HtlDetailsDs.Tables(1).Rows(0)("GFName").ToString(), SearchDetails.PGLastName, SearchDetails.PGEmail)

                            Try
                                If Session("LoginByOTP") IsNot Nothing AndAlso Convert.ToString(Session("LoginByOTP")) <> "" AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                                    Dim UserId As String = Session("UID")
                                    Dim Remark As String = Request("txtRemarkss")
                                    Dim OTPRefNo As String = SearchDetails.Orderid
                                    Dim LoginByOTP As String = Session("LoginByOTP")
                                    Dim OTPId As String = Session("OTPID")
                                    Dim ServiceType As String = "HOTEL-TICKET" + SearchDetails.CancelStatus
                                    Dim flag As Integer = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType)
                                End If
                            Catch ex As Exception
                                HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
                                EXCEPTION_LOG.ErrorLog.FileHandling("HotelReport", "Error_102", ex, "sprreports-hotel-htlbookingrpt")
                            End Try

                            If i = 3 Then
                                If Request("Parcial") = "false" Then
                                    Dim objcancel As New HotelBAL.CommonHotelBL()
                                    SearchDetails = objcancel.CancellationHotelBooking(SearchDetails)
                                    If (SearchDetails.CancelStatus = "Cancelled") Then
                                        Dim objDA As New SqlTransaction
                                        Dim cancelamt As Decimal = 0
                                        If SearchDetails.SupplierRefundAmt > 0 Then
                                            If orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 1) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 2) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 3) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 4) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 5) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 6) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 7) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 8) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 9) Or (orgbookingrate = Math.Round(SearchDetails.SupplierRefundAmt, 0) + 10) Then
                                                SearchDetails.RefundAmt = Convert.ToDecimal(HotelDT.Rows(0)("NetCost"))
                                            Else
                                                If (orgbookingrate + 1 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 2 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 3 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 4 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 5 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 6 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 7 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 8 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 9 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Or (orgbookingrate + 10 = Math.Round(SearchDetails.SupplierRefundAmt, 0)) Then
                                                    SearchDetails.RefundAmt = Convert.ToDecimal(HotelDT.Rows(0)("NetCost"))
                                                Else
                                                    If SearchDetails.AdminMrkType = "Percentage" Then
                                                        Dim mrk As New HotelBAL.HotelMarkups()
                                                        Dim withAdminMrkDesiyaRefundamt As Decimal = mrk.DiscountMarkupCalculation(SearchDetails.AdminMrkPer, 0.0, SearchDetails.AdminMrkType, "Percentage", SearchDetails.SupplierRefundAmt, SearchDetails.servicetax)
                                                        SearchDetails.RefundAmt = withAdminMrkDesiyaRefundamt
                                                    Else
                                                        SearchDetails.RefundAmt = SearchDetails.SupplierRefundAmt
                                                    End If
                                                End If
                                            End If
                                        Else
                                            SearchDetails.RefundAmt = 0
                                            cancelamt = Convert.ToDecimal(HotelDT.Rows(0)("NetCost"))
                                        End If
                                        If SearchDetails.Provider = "TGBooking" Or SearchDetails.Provider = "ROOMXML" Or SearchDetails.Provider = "ZUMATA" Then

                                            Dim j As Integer = ST.SP_HTL_AutoRefund(SearchDetails, cancelamt)
                                            Dim SqlT As New SqlTransaction()
                                            Dim ablBalance As Double = SqlT.UpdateNew_RegsRefund(HotelDT.Rows(0)("LoginId").ToString(), Convert.ToDouble(SearchDetails.RefundAmt))

                                            Dim STDom As New SqlTransactionDom()
                                            'Insert Data in LedgerDetails Table
                                            STDom.insertLedgerDetails(HotelDT.Rows(0)("LoginId").ToString(), HotelDT.Rows(0)("AgencyName").ToString(), OrderIDS.Value, SearchDetails.BookingID, "", "", "", "", Session("UID").ToString(), Request.UserHostAddress, 0, _
                                                                SearchDetails.RefundAmt, Convert.ToDecimal(ablBalance), "HTL RFND", "Hotel refund for " & OrderIDS.Value, 0)
                                            ShowAlertMessage("Hotel Cancelled successfully. Rs " + SearchDetails.RefundAmt.ToString() + " Refunded your Acount.")

                                        Else
                                            ShowAlertMessage("Hotel Cancelled successfully.  Refund amount will process in your acount offline.")
                                            Dim ir As Integer = ST.RejectHoldBooking(StatusClass.Cancelled.ToString(), OrderIDS.Value, 0, Session("UID").ToString(), "")
                                        End If
                                       
                                        'Try
                                        '    Dim objSMSAPI As New SMSAPI.SMS
                                        '    Dim objSqlNew As New SqlTransactionNew
                                        '    Dim smstext As String = ""

                                        '    Dim SmsCrd As DataTable

                                        '    SmsCrd = objDA.SmsCredential(SMS.HOTELCANCEL.ToString()).Tables(0)
                                        '    If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                        '        Dim smsStatus As String = objSMSAPI.SendHotelSms(SearchDetails.Orderid, SearchDetails.BookingID, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), SearchDetails.HotelName + " " + HotelDT.Rows(0)("CityName").ToString().Trim(), SearchDetails.CheckInDate, SearchDetails.CheckOutDate, "Book", smstext, SmsCrd)
                                        '        objSqlNew.SmsLogDetails(SearchDetails.Orderid, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), smstext, smsStatus)
                                        '    End If

                                        'Catch ex As Exception
                                        'End Try

                                    Else
                                        ShowAlertMessage("Hotel Cancelled is under process.")
                                    End If
                                Else
                                    ShowAlertMessage("Hotel Cancelled is under process. for more details, Please contact to customer care.")
                                End If
                            Else
                                ShowAlertMessage("Cancellation request can not be process for this booking. Please contact to customer care.")
                            End If
                            Try
                                ''''Send mail and SMS strat
                                Dim objcancel As New HotelBAL.HotelSendMail_Log()
                                ''objcancel.SendEmailForCancelAndReject(SearchDetails.Orderid, SearchDetails.BookingID, SearchDetails.HotelName + ", " + HotelDT.Rows(0)("CityName").ToString().Trim(), Session("UID").ToString(), HotelStatus.HOTEL_CANCELLATION.ToString(), SearchDetails.CancelStatus)

                                If (HtlDetailsDs.Tables.Count > 1) Then
                                    Dim bookingstaus As String = ""
                                    If SearchDetails.CancelStatus = "Cancelled" Then
                                        bookingstaus = "Cancel"
                                    Else
                                        bookingstaus = "CancelInprocess"
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
                                        smsStatus = objSMSAPI.SendHotelSms(SearchDetails.Orderid, SearchDetails.BookingID, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), SearchDetails.HotelName + ", " + HotelDT.Rows(0)("CityName").ToString().Trim(), SearchDetails.CheckInDate, SearchDetails.CheckOutDate, bookingstaus, smstext, SmsCrd)
                                        objSqlNew.SmsLogDetails(SearchDetails.Orderid, HtlDetailsDs.Tables(1).Rows(0)("GPhoneNo").ToString().Trim(), smstext, smsStatus)
                                    End If
                                End If
                                ''''Send mail and SMS End
                            Catch ex As Exception
                                HotelDAL.HotelDA.InsertHotelErrorLog(ex, " Send SMS Agency Cancellation")
                            End Try
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
                    Case 1
                        ShowAlertMessage("This booking id allready refunded")
                End Select
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            Dim GrdDS As New DataSet()
            GrdDS = GetReportData()
            GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("ModifyStatus"))
            GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("Markup"))
            GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("ConfirmationNo"))
            If Session("User_Type").ToString = "AGENT" Or Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "SALES" Then
                GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("PurchaseCost"))
            End If
            STDom.ExportData(GrdDS)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try

    End Sub
    Public Shared Sub CallScriptFunction(ByVal funName As String, ByVal Parameter1 As String, ByVal Parameter2 As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", funName & "('" & Parameter1 & "', '" & Parameter2 & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub GrdReport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdReport.PageIndexChanging
        Try
            GrdReport.PageIndex = e.NewPageIndex
            Bindgrid(Session("Grdds"))
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
