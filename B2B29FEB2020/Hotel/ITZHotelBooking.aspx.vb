Imports System.Data
Imports System.Data.SqlClient
Imports HotelShared
Imports System.Configuration.ConfigurationManager


Partial Class Hotel_ITZHotelBooking
    Inherits System.Web.UI.Page
    Dim HtlLogObj As New HotelBAL.HotelSendMail_Log()
    Dim HTLST As New HotelDAL.HotelDA
    Dim objPg As New PG.PaymentGateway()

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx", True)
        End If

        Dim ObjBooking As New HotelBooking
        Dim objDA As New SqlTransaction
        ObjBooking = CType(Session("HotelBookingDetails"), HotelBooking)
        Try
            Dim AgencyDs As DataSet
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                Dim BalanceCheck As Integer = HTLST.CheckBalance(Session("UID").ToString, ObjBooking.AgentDebitAmt)
                If BalanceCheck = 0 Then
                    If ObjBooking.Total_Org_Roomrate <= ObjBooking.AgentDebitAmt Then
                        Dim HtlRes As New HotelBAL.CommonHotelBL()
                        ObjBooking = HtlRes.PreBookingHotelRequest(ObjBooking)
                        Session("HotelBooking") = ObjBooking
                        If ObjBooking.ProBookingID <> "false" Then
                            If DeductbookingAmount(ObjBooking, AgencyDs) Then
                                ObjBooking = HtlRes.HotelBookingRequest(ObjBooking)
                                UpdateBooking(ObjBooking)
                            Else
                                ObjBooking.Status = StatusClass.Rejected.ToString()
                                HTLST.UpdateHtlBooking(ObjBooking.Orderid, "", "Balance Issue", ObjBooking.Status, "", "", "")
                                Session("HotelBooking") = ObjBooking
                            End If
                        Else
                            ObjBooking.Status = StatusClass.Rejected.ToString()
                            HTLST.UpdateHtlBooking(ObjBooking.Orderid, "", "Fare has been changed. Please try another room", ObjBooking.Status, "", "", "")
                            Session("HotelBooking") = ObjBooking
                        End If
                    Else
                        ObjBooking.Status = StatusClass.Rejected.ToString()
                        ObjBooking.BookingID = ""
                        ObjBooking.ProBookingID = ""
                        HTLST.UpdateHtlBooking(ObjBooking.Orderid, "", "Agent Debit amount and Booking amount not matched", ObjBooking.Status, "", "", "")
                        Session("HotelBooking") = ObjBooking
                    End If
                Else
                    ObjBooking.Status = StatusClass.Rejected.ToString()
                    ObjBooking.BookingID = ""
                    ObjBooking.ProBookingID = ""
                    HTLST.UpdateHtlBooking(ObjBooking.Orderid, "", "InSufficient Credit Limit. Please Contact Administrator.", ObjBooking.Status, "", "", "")
                    Session("HotelBooking") = ObjBooking
                End If
            Else
                ObjBooking.Status = StatusClass.Rejected.ToString()
                ObjBooking.BookingID = ""
                ObjBooking.ProBookingID = ""
                HTLST.UpdateHtlBooking(ObjBooking.Orderid, "", "Agent Not alow to Book Hotel. Please Contact Administrator.", ObjBooking.Status, "", "", "")
                Session("HotelBooking") = ObjBooking
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "ITZHtlBooking_PageLoad")
        End Try

    End Sub
    Protected Sub UpdateBooking(ByVal SearchDetails As HotelBooking)
        Try
            If SearchDetails.BookingID <> "" Then
                If SearchDetails.Provider = "ROOMXML" And SearchDetails.Status.ToLower = "confirmed" Then
                    SearchDetails.Status = StatusClass.Confirm.ToString()
                ElseIf (SearchDetails.Provider = "GAL" Or SearchDetails.Provider = "SuperShopper") And SearchDetails.Status.ToLower = "confirm" Then
                    If SearchDetails.GuaranteeType = "Guarantee" Then
                        SearchDetails.Status = StatusClass.Hold.ToString()
                        SearchDetails.HotelErrorMsg = "Hotel Booking is Confirm. Please contact Customer care for detail."
                    End If
                End If
            Else
                SearchDetails.Status = StatusClass.Hold.ToString()
                IIf(SearchDetails.BookingID Is Nothing, "", SearchDetails.BookingID)
            End If
            'If SearchDetails.Status = StatusClass.Confirm.ToString() Then
            '    Try
            '        Dim objSMSAPI As New SMSAPI.SMS
            '        Dim objSqlNew As New SqlTransactionNew
            '        Dim smstext As String = ""
            '        Dim SmsCrd As DataTable
            '        Dim objDA As New SqlTransaction
            '        SmsCrd = objDA.SmsCredential(SMS.HOTELBOOKING.ToString()).Tables(0)
            '        Dim smsStatus As String = ""
            '        Dim smsMsg As String = ""
            '        If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
            '            smsStatus = objSMSAPI.SendHotelSms(SearchDetails.Orderid, SearchDetails.BookingID, SearchDetails.PGContact.ToString().Trim(), SearchDetails.HotelName + " " + SearchDetails.CityName.ToString().Trim(), SearchDetails.CheckInDate, SearchDetails.CheckOutDate, "Book", smstext, SmsCrd)
            '            objSqlNew.SmsLogDetails(SearchDetails.Orderid, SearchDetails.PGContact.ToString().Trim(), smstext, smsStatus)
            '        End If
            '    Catch ex As Exception
            '    End Try
            'End If

            Session("HotelBooking") = SearchDetails
            'Update Booking Info 
            HTLST.UpdateHtlBooking(SearchDetails.Orderid, SearchDetails.BookingID, SearchDetails.ProBookingID, SearchDetails.Status, SearchDetails.HotelContactNo, "", "")

        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HtlBooking_UpdateBooking")
            Response.Redirect("NoResult.aspx?HtlError=your booking on Hold due to some exception on booking process", False)
        End Try
    End Sub
    Protected Function DeductbookingAmount(ByVal SearchDetails As HotelBooking, ByVal AgencyDs As DataSet) As Boolean
        Try

            'Balance Check and deduct and Transaction Log - Staff Login
            Dim DebitSataus As String = ""
            Dim CreditSataus As String = ""
            Dim CheckBalance As String = ""
            Dim AgentStatus As String = ""
            Dim StaffBalCheck As String = ""
            Dim StaffBalCheckStatus As String = ""
            Dim CurrentTotAmt As String = ""
            Dim TransAmount As String = ""
            Dim BookTicket As String = "true"
            Try
                If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")).ToUpper() = "TRUE" AndAlso Convert.ToString(Session("LoginType")).ToUpper() = "STAFF") Then
                    BookTicket = "false"
                    If (Convert.ToString(Session("FlightActive")) = "True") Then
                        Dim BoookingByStaff As String = "True"
                        Dim sOrderId As String = SearchDetails.Orderid
                        Dim sTransAmount As String = SearchDetails.AgentDebitAmt
                        Dim sStaffUserId As String = Session("StaffUserId")
                        Dim sOwnerId As String = Session("UID")
                        Dim sIPAddress As String = Request.UserHostAddress.ToString()
                        Dim sRemark As String = Session("LoginType") + "_" + sOrderId + "_Hotel_" + SearchDetails.Provider + "_" + SearchDetails.HotelName + "_" + sTransAmount
                        Dim sCreatedBy As String = Session("StaffUserId")
                        Dim ModuleType As String = "HOTEL BOOKING"
                        Dim sServiceType As String = "HOTEL"
                        Dim DebitCredit As String = "DEBIT"
                        Dim ActionType As String = "CHECKBAL-DEDUCT"
                        Dim StaffDs As DataSet
                        Dim objSqlDom As New SqlTransactionDom
                        StaffDs = objSqlDom.CheckStaffBalanceAndBalanceDeduct(sOrderId, sServiceType, sTransAmount, sStaffUserId, sOwnerId, sIPAddress, sRemark, sCreatedBy, DebitCredit, ModuleType, ActionType)
                        If (StaffDs IsNot Nothing AndAlso StaffDs.Tables.Count > 0 AndAlso StaffDs.Tables(0).Rows.Count > 0) Then
                            'DebitSataus ,CreditSataus,CheckBalance,AgentStatus,StaffBalCheck,StaffBalCheckStatus,CurrentTotAmt,TransAmount		
                            DebitSataus = Convert.ToString(StaffDs.Tables(0).Rows(0)("DebitSataus"))
                            CreditSataus = Convert.ToString(StaffDs.Tables(0).Rows(0)("CreditSataus"))
                            CheckBalance = Convert.ToString(StaffDs.Tables(0).Rows(0)("CheckBalance"))
                            AgentStatus = Convert.ToString(StaffDs.Tables(0).Rows(0)("AgentStatus"))
                            StaffBalCheck = Convert.ToString(StaffDs.Tables(0).Rows(0)("StaffBalCheck"))
                            StaffBalCheckStatus = Convert.ToString(StaffDs.Tables(0).Rows(0)("StaffBalCheckStatus"))
                            CurrentTotAmt = Convert.ToString(StaffDs.Tables(0).Rows(0)("CurrentTotAmt"))
                            TransAmount = Convert.ToString(StaffDs.Tables(0).Rows(0)("TransAmount"))
                            BookTicket = "false"
                            If (Convert.ToString(StaffDs.Tables(0).Rows(0)("LoginStatus")) = "True" AndAlso Convert.ToString(StaffDs.Tables(0).Rows(0)("Hotel")) = "True" AndAlso (DebitSataus.ToLower() = "true" OrElse StaffBalCheckStatus.ToLower() = "false")) Then
                                BookTicket = "true"
                            End If
                        Else
                            BookTicket = "false"
                        End If
                    Else
                        BookTicket = "false"
                    End If

                End If
            Catch ex As Exception

            End Try

            'END: Balance Check and deduct and Transaction Log - Staff

            'Check Balance
            Dim BalanceCheck As Integer = HTLST.CheckBalance(Session("UID").ToString, SearchDetails.AgentDebitAmt)
            If (BalanceCheck = 0 AndAlso BookTicket = "true") Then
                Dim ablBalance As Double = HTLST.UpdateBalance(Session("UID").ToString, SearchDetails.AgentDebitAmt)
                Dim objSqlDom As New SqlTransactionDom
                Dim Result As Integer = objSqlDom.LedgerEntry_Common(SearchDetails.Orderid, SearchDetails.AgentDebitAmt, 0, Convert.ToDouble(ablBalance), SearchDetails.Provider, SearchDetails.HotelName, "", _
                                                     "HTLBook", Session("UID").ToString(), AgencyDs.Tables(0).Rows(0)("Agency_Name").ToString(), Session("UID").ToString(), Request.UserHostAddress.ToString(), _
                                                     "", "", "", "Hotel booking agence " + SearchDetails.Orderid, SearchDetails.HtlType.Substring(0, 1), 0)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HtlBooking_DeductbookingAmount")
            Return False
        End Try

    End Function


    Protected Sub HotelBookByPaymentGateway(ByVal AgencyDs As DataSet, ByVal OrderId As String, ByVal PaymentMode As String)
        Try
            Dim objDA As New SqlTransaction
            Dim objSqlDom As New SqlTransactionDom
            Dim amtbeforded As String = ""
            Dim inst As Boolean = False
            Dim ObjBooking As New HotelBooking
            ObjBooking = CType(Session("HotelBookingDetails"), HotelBooking)

            Dim PaymentStatus As String = ""
            Dim ApiStatus As String = ""
            Dim PgTid As String = ""
            Dim PaymentId As String = ""
            Dim BankRefNo As String = ""
            Dim PgAmount As String = ""
            Dim OriginalAmount As String = ""
            Dim TotalCharges As String = ""
            Dim Amount As String = ""
            Dim ds As DataSet = objPg.GetPgPaymentDetailsHotel(ObjBooking.Orderid, "Hotel", "PaymentDetails", Session("UID"))
            If ds IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    PaymentStatus = Convert.ToString(ds.Tables(0).Rows(0)("Status"))
                    ApiStatus = Convert.ToString(ds.Tables(0).Rows(0)("ApiStatus"))
                    PgTid = Convert.ToString(ds.Tables(0).Rows(0)("TId"))
                    PaymentId = Convert.ToString(ds.Tables(0).Rows(0)("PaymentId"))
                    BankRefNo = Convert.ToString(ds.Tables(0).Rows(0)("BankRefNo"))

                    If Not String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(0)("PgAmount"))) Then
                        PgAmount = Convert.ToString(ds.Tables(0).Rows(0)("PgAmount"))
                    Else
                        PgAmount = "0"
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(0)("OriginalAmount"))) Then
                        OriginalAmount = Convert.ToString(ds.Tables(0).Rows(0)("OriginalAmount"))
                    Else
                        OriginalAmount = "0"
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(0)("TotalCharges"))) Then
                        TotalCharges = Convert.ToString(ds.Tables(0).Rows(0)("TotalCharges"))
                    Else
                        TotalCharges = "0"
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(0)("Amount"))) Then
                        Amount = Convert.ToString(ds.Tables(0).Rows(0)("Amount")).Trim()
                    Else
                        Amount = "0"
                    End If
                    If ObjBooking.AgentDebitAmt <= Convert.ToDouble(Amount) AndAlso PaymentMode = "PG" AndAlso PaymentStatus = "Success" Then
                        If ObjBooking.Total_Org_Roomrate <= ObjBooking.AgentDebitAmt Then
                            Dim HtlRes As New HotelBAL.CommonHotelBL()
                            ObjBooking = HtlRes.PreBookingHotelRequest(ObjBooking)
                            Session("HotelBooking") = ObjBooking
                            If ObjBooking.ProBookingID <> "false" Then
                                amtbeforded = 0
                                Dim Result As Integer = objSqlDom.LedgerEntry_Common(ObjBooking.Orderid, ObjBooking.AgentDebitAmt, 0, Convert.ToDouble(amtbeforded.Trim()), ObjBooking.Provider, ObjBooking.HotelName, "", _
                                                        "HTLBook", Session("UID").ToString(), AgencyDs.Tables(0).Rows(0)("Agency_Name").ToString(), Session("UID").ToString(), Request.UserHostAddress.ToString(), _
                                                        "", "", "", "Hotel booking agence " + ObjBooking.Orderid, ObjBooking.HtlType.Substring(0, 1), 0)

                                ObjBooking = HtlRes.HotelBookingRequest(ObjBooking)
                                UpdateBooking(ObjBooking)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HotelBookByPaymentGateway")
        End Try

    End Sub

End Class


