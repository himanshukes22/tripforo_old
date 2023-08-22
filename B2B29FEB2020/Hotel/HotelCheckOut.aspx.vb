Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Xml
Imports System.Linq
Imports System.IO
Imports System.Web.UI.Page
Imports System.Configuration
Imports HotelDAL
Imports HotelShared
Imports System.Configuration.ConfigurationManager
Imports System.Collections.Generic
Imports PG
Imports PaytmWall

Partial Class HotelCheckOut
    Inherits System.Web.UI.Page
    Dim HTLST As New HotelDA
    Dim Htlsearch As New HotelSearch()
    Dim RommCompositResult As New RoomComposite
    Public Shared UserID As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx", False)
        Else
            UserID = Session("UID")
            Htlsearch = CType(Session("HotelSearcDetailss"), HotelSearch)
            If Not IsPostBack Then
                Try
                    RommCompositResult = CType(Session("SelectedHotelDetail"), RoomComposite)

                    Dim ObjBooking As New HotelBooking
                    ObjBooking.RoomPlanCode = Request.QueryString("RoomRatePlan")
                    ObjBooking.RoomTypeCode = Request.QueryString("RoomCode")
                    ObjBooking.AgentID = Session("UID").ToString()
                    ObjBooking.CityCode = Htlsearch.HotelCityCode
                    ObjBooking.CityName = Htlsearch.SearchCity
                    ObjBooking.Country = Htlsearch.Country
                    ObjBooking.CountryCode = Htlsearch.CountryCode
                    ObjBooking.HtlType = Htlsearch.HtlType
                    ObjBooking.AdtPerRoom = Htlsearch.AdtPerRoom
                    ObjBooking.ChdPerRoom = Htlsearch.ChdPerRoom
                    ObjBooking.ChdAge = Htlsearch.ChdAge
                    ObjBooking.TotAdt = Htlsearch.TotAdt
                    ObjBooking.TotChd = Htlsearch.TotChd
                    ObjBooking.NoofRoom = Htlsearch.NoofRoom
                    ObjBooking.CheckInDate = Htlsearch.CheckInDate
                    ObjBooking.CheckOutDate = Htlsearch.CheckOutDate
                    ObjBooking.HtlCode = Request.QueryString("RoomRPH")
                    ObjBooking.HotelName = Htlsearch.HotelName
                    ObjBooking.Currency = "INR"
                    ObjBooking.StarRating = RommCompositResult.SelectedHotelDetail.StarRating
                    ObjBooking.ThumbImg = RommCompositResult.SelectedHotelDetail.ThumbnailUrl
                    ObjBooking.HotelAddress = RommCompositResult.SelectedHotelDetail.HotelAddress
                    ObjBooking.NoofNight = Htlsearch.NoofNight
                    'Generate OrderID
                    Dim HtlLogObj As New HotelBAL.HotelSendMail_Log()
                    ObjBooking.Orderid = HtlLogObj.GetID("HTL")
                    Session("HtlName") = ObjBooking.HotelName

                    Dim Hotelcredencials As New List(Of HotelCredencials)
                    Hotelcredencials = (From v In Htlsearch.CredencialsList Where v.HotelProvider = Request.QueryString("Provider")).ToList()
                    If (Hotelcredencials.Count > 0) Then
                        ObjBooking.HotelUrl = Hotelcredencials(0).HotelUrl
                        ObjBooking.HotelUsername = Hotelcredencials(0).HotelUsername
                        ObjBooking.HotelPassword = Hotelcredencials(0).HotelPassword
                        ObjBooking.HotelCompanyID = Hotelcredencials(0).HotelCompanyID
                    End If

                    Dim RoomDetals As List(Of RoomList)
                    RoomDetals = (From rooms In RommCompositResult.RoomDetails Where rooms.RatePlanCode = ObjBooking.RoomPlanCode And rooms.RoomTypeCode = ObjBooking.RoomTypeCode).ToList()

                    If RoomDetals.Count > 0 Then
                        Session("RoomDetals") = RoomDetals
                        ObjBooking.Provider = RoomDetals(0).Provider
                        ObjBooking.TotalRoomrate = RoomDetals(0).TotalRoomrate
                        ObjBooking.Total_Org_Roomrate = RoomDetals(0).Total_Org_Roomrate
                        ObjBooking.DiscountAMT = RoomDetals(0).DiscountAMT
                        ObjBooking.AgentMrkAmt = RoomDetals(0).AgentMarkupAmt
                        ObjBooking.RoomName = RoomDetals(0).RoomName.Replace("H/T/Lamp;", ", ")
                        ObjBooking.AmountBeforeTax = RoomDetals(0).AmountBeforeTax
                        ObjBooking.Taxes = RoomDetals(0).Taxes
                        pernight.InnerText = Math.Round(ObjBooking.TotalRoomrate / (Htlsearch.NoofRoom * Htlsearch.NoofNight), 2).ToString()

                        If RoomDetals(0).inclusions <> "" Then
                            RoomInclusionTD.InnerHtml = RoomDetals(0).inclusions
                            RoomInclusionTDHead.Visible = True
                        Else
                            RoomInclusionTDHead.Visible = False
                        End If
                        ObjBooking.MealInclude = RoomDetals(0).inclusions
                        ObjBooking.SessionID = RommCompositResult.SelectedHotelDetail.HotelSessionID
                        If RommCompositResult.SelectedHotelDetail.HotelContactNo <> Nothing Then
                            ObjBooking.HotelContactNo = RommCompositResult.SelectedHotelDetail.HotelContactNo
                        ElseIf Htlsearch.HotelContactNo <> Nothing Then
                            ObjBooking.HotelContactNo = Htlsearch.HotelContactNo
                        Else
                            ObjBooking.HotelContactNo = ""
                        End If

                        Dim objhtlcom As New HotelBAL.CommonHotelBL()
                        'Insert HotelSearch Request and Response XML Log
                        If Request.QueryString("Provider") = "TG" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.TG_HotelSearchReq, Htlsearch.TG_HotelSearchRes, "TG", "HotelInsert")
                            ObjBooking.HotelPolicy = RoomDetals(0).CancelationPolicy
                        ElseIf Request.QueryString("Provider") = "ZUMATA" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.Zumata_HotelSearchReq, Htlsearch.Zumata_HotelSearchRes, "ZUMATA", "HotelInsert")
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.ZUMATAHotelDetailReq, Htlsearch.ZUMATAHotelDetailRes, Session("UID").ToString(), "HtlUpdateDetails")
                            Dim objhtlcan As New CancelationPolicy()
                            objhtlcan = objhtlcom.RoomCancelationPolicy(Htlsearch, "ZUMATA", RoomDetals(0).RatePlanCode, ObjBooking.HtlCode, objhtlcan)
                            If objhtlcan.PolicyID IsNot Nothing Then
                                ObjBooking.HotelPolicy = objhtlcan.HotelPolicy + objhtlcan.CancelationPolicyInfo
                                ObjBooking.ReferenceNo = objhtlcan.PolicyID
                            Else
                                ObjBooking.HotelPolicy = ""
                                btnPayment.Visible = False
                                lblError.Text = "Booking Policy Not available from API. Please Refresh the Page or contact to admin"
                            End If

                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, objhtlcan.CancelationPolicyReq, objhtlcan.CancelationPolicyResp, Session("UID").ToString(), "HtlUpdateAgreement")

                        ElseIf Request.QueryString("Provider") = "INNSTANT" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.Innstant_HotelSearchReq, Htlsearch.Innstant_HotelSearchRes, "INNSTANT", "HotelInsert")
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.INNSTANTHotelDetailReq, Htlsearch.INNSTANTHotelDetailRes, Session("UID").ToString(), "HtlUpdateDetails")
                            Dim objhtlcan As New CancelationPolicy()
                            objhtlcan = objhtlcom.RoomCancelationPolicy(Htlsearch, ObjBooking.Provider, ObjBooking.RoomPlanCode, ObjBooking.SessionID, objhtlcan)

                            ObjBooking.HotelPolicy = objhtlcan.HotelPolicy & objhtlcan.CancelationPolicyInfo

                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, objhtlcan.CancelationPolicyReq, objhtlcan.CancelationPolicyResp, Session("UID").ToString(), "HtlUpdateAgreement")
                            If objhtlcan.Bookable = False Then
                                btnPayment.Visible = False
                                lblError.Text = objhtlcan.AlertInfo
                            End If
                        ElseIf Request.QueryString("Provider") = "ROOMXML" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.RoomXML_HotelSearchReq, Htlsearch.RoomXML_HotelSearchRes, "ROOMXML", "HotelInsert")
                            Dim objhtlcan As New CancelationPolicy()
                            objhtlcan = objhtlcom.RoomCancelationPolicy(Htlsearch, ObjBooking.Provider, Request.QueryString("RoomRatePlan"), "", objhtlcan)
                            ObjBooking.HotelPolicy = System.Web.HttpUtility.HtmlDecode(objhtlcan.HotelPolicy & objhtlcan.CancelationPolicyInfo)

                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, objhtlcan.CancelationPolicyReq, objhtlcan.CancelationPolicyResp, Session("UID").ToString(), "HtlUpdateAgreement")
                        ElseIf Request.QueryString("Provider") = "GRN" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.GRN_HotelSearchReq, Htlsearch.GRN_HotelSearchRes, "GRN", "HotelInsert")
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.GRNHotelDetailReq, Htlsearch.GRNHotelDetailRes, Session("UID").ToString(), "HtlUpdateDetails")
                            ObjBooking.HotelPolicy = RoomDetals(0).CancelationPolicy
                            ObjBooking.RacCode = RoomDetals(0).RoomRateKey

                        ElseIf Request.QueryString("Provider") = "MTS" Then
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.MTS_HotelSearchReq, Htlsearch.MTS_HotelSearchRes, "MTS", "HotelInsert")
                        ElseIf Request.QueryString("Provider") = "SuperShopper" Then
                            'RoomRateKey
                            ObjBooking.HotelChain = RommCompositResult.SelectedHotelDetail.HotelChain
                            ObjBooking.HotelPolicy = RoomDetals(0).CancelationPolicy
                            ObjBooking.SelectedRoomsData = RoomDetals(0).RoomRateKey
                            ObjBooking.CurrancyRate = RoomDetals(0).exchangerate
                            ObjBooking.Currency = RoomDetals(0).CurrencyCode
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.SuperShopper_HotelSearchReq, Htlsearch.SuperShopper_HotelSearchRes, "SuperShopper", "HotelInsert")
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, RommCompositResult.SelectedHotelDetail.HotelRequest, RommCompositResult.SelectedHotelDetail.HotelResponse, Session("UID").ToString(), "HtlUpdateAgreement")
                        ElseIf Request.QueryString("Provider") = "GAL" Then
                            'RoomRateKey
                            Htlsearch.HotelchainCode = ObjBooking.RoomTypeCode
                            ObjBooking.HotelChain = RommCompositResult.SelectedHotelDetail.HotelChain
                            ObjBooking.HotelPolicy = RoomDetals(0).CancelationPolicy
                            ObjBooking.SelectedRoomsData = RoomDetals(0).RoomRateKey
                            ObjBooking.CurrancyRate = RoomDetals(0).exchangerate
                            ObjBooking.Currency = RoomDetals(0).CurrencyCode
                            ObjBooking.GuaranteeType = RoomDetals(0).GuaranteeType
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, Htlsearch.GAL_HotelSearchReq, Htlsearch.GAL_HotelSearchRes, "GAL", "HotelInsert")
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, RommCompositResult.SelectedHotelDetail.HotelRequest, RommCompositResult.SelectedHotelDetail.HotelResponse, Session("UID").ToString(), "HtlUpdateAgreement")

                            Dim objhtlcan As New CancelationPolicy()
                            objhtlcan = objhtlcom.RoomCancelationPolicy(Htlsearch, ObjBooking.Provider, ObjBooking.RoomPlanCode, ObjBooking.Currency + ObjBooking.Total_Org_Roomrate.ToString(), objhtlcan)
                            HTLST.SP_Htl_InsUpdBookingLog(ObjBooking.Orderid, objhtlcan.CancelationPolicyReq, objhtlcan.CancelationPolicyResp, Session("UID").ToString(), "HtlUpdateAgreement")

                            If (ObjBooking.GuaranteeType = "Deposit" Or ObjBooking.GuaranteeType = "Prepayment" Or ObjBooking.GuaranteeType = "Guarantee") Then
                                btnPayment.Visible = True
                            Else
                                ObjBooking.GuaranteeType = objhtlcan.CancelationPolicyInfo
                            End If

                            If (ObjBooking.GuaranteeType = "Guarantee") Then
                                btnPayment.Visible = False
                                lblError.Text = "Guarantee(Pay at Hotel) Payment Type Booking is not posible. Please select other payment type room."
                            End If
                            ObjBooking.HotelPolicy = objhtlcan.HotelPolicy & "<ul><li> Payment Requirement: " & ObjBooking.GuaranteeType & "</li></ul>"

                            If objhtlcan.Bookable = False Then
                                btnPayment.Visible = False
                                lblError.Text = "Booking Policy Not available from API. Please Refresh the Page or contact to admin"
                            End If
                        End If

                        rptItems.DataSource = getPaxList(Htlsearch)
                        rptItems.DataBind()

                        Dim AgencyType As String = ""
                        If Session("User_Type").ToString() = "AGENT" Then
                            AgencyType = Session("AGTY").ToString()
                            ObjBooking.AgencyName = Session("AgencyName").ToString()
                        Else
                            ObjBooking.AgencyName = Session("User_Type").ToString()
                            AgencyType = ObjBooking.AgencyName
                            btnPayment.Visible = False
                        End If
                        'Commision Calculation
                        'ObjBooking = HTLST.GetHtlCommision(ObjBooking, AgencyType, ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt)
                        'Save Booking details
                        ObjBooking.CommisionAmt = 0
                        ObjBooking.CommisionPer = 0
                        ObjBooking.CommisionType = ""
                        ObjBooking.servicetax = 0
                        ' ObjBooking.ServiseTaxAmt = 0
                        ' ObjBooking.V_ServiseTaxAmt = 0

                        ObjBooking.HotelLocation = ""
                        ObjBooking.SharingBedding = RoomDetals(0).Smoking
                        ObjBooking.Refundable = ""
                        ObjBooking.RoomRPH = ""
                        'Save data in table
                        SaveBookingInfoNew(ObjBooking, RoomDetals)

                    Else
                        btnPayment.Visible = False
                        lblError.Text = "The requested rooms are no longer available. Please search again for different room type or hotel"
                    End If

                        Session("HotelBookingDetails") = ObjBooking
                        Session("CheckInOutDetails") = Htlsearch

                        Dim sCheckIn() As String = Split(ObjBooking.CheckInDate, "-")
                        Dim sCheckOut() As String = Split(ObjBooking.CheckOutDate, "-")
                        Session("sCheckIn1") = sCheckIn(2) & " " & Left(MonthName(sCheckIn(1)), 3) & " " & sCheckIn(0)
                        Session("sCheckOut1") = sCheckOut(2) & " " & Left(MonthName(sCheckOut(1)), 3) & " " & sCheckOut(0)

                        htlcheckinlbl.Text = Session("sCheckIn1").ToString()
                        htlcheckoutlbl.Text = Session("sCheckOut1").ToString()
                        'lblTotalCharge.Text = ObjBooking.TotalRoomrate.ToString()
                        lblTotalCharge.Text = (ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt - ObjBooking.CommisionAmt).ToString()
                        lblAmount.InnerText = ObjBooking.TotalRoomrate.ToString()
                        AgtMrk.InnerText = "- " & ObjBooking.AgentMrkAmt.ToString()
                        AgtComm.InnerText = "- " & ObjBooking.CommisionAmt.ToString()
                        TdNetCost.InnerText = (ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt - ObjBooking.CommisionAmt).ToString()

                        'Set value in hidden field for PG Date:13-12-2016
                        DivPgCharge.InnerText = "0"
                        HdnTotalPrice.Value = ObjBooking.TotalRoomrate.ToString()
                        HdnCommision.Value = ObjBooking.CommisionAmt.ToString()
                        HdnMarkups.Value = ObjBooking.AgentMrkAmt.ToString()
                        HdnPgCharge.Value = "0"
                        HdnPriceToAgent.Value = (ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt - ObjBooking.CommisionAmt).ToString()
                        HdnOrignalPriceAgent.Value = (ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt - ObjBooking.CommisionAmt).ToString()


                        'lblAvgCharge.InnerText = "INR " & SearchDetails.PerNight
                        lblNights.InnerText = Htlsearch.NoofNight
                        TotAdt.InnerText = Htlsearch.TotAdt
                        Totchd.InnerText = Htlsearch.TotChd
                        htlrmslbl.InnerText = Htlsearch.NoofRoom.ToString()
                        'Set Star Rating
                        HtlStrImg.Text = HtlLogObj.SetStars(RommCompositResult.SelectedHotelDetail.StarRating, Request.Url.Scheme & "://" & Request.Url.Authority)

                        lblRules.Text = HttpUtility.HtmlDecode(ObjBooking.HotelPolicy)
                        HtlImg.ImageUrl = RommCompositResult.SelectedHotelDetail.ThumbnailUrl
                        HtlNameLbl.Text = RommCompositResult.SelectedHotelDetail.HotelName
                        HtlLoc.Text = RommCompositResult.SelectedHotelDetail.HotelAddress
                        SuiteNm.Text = ObjBooking.RoomName
                        ObjBooking.AgentDebitAmt = ObjBooking.TotalRoomrate - ObjBooking.AgentMrkAmt
                Catch ex As Exception
                    HotelDA.InsertHotelErrorLog(ex, "HotelCheckOut_Page_Load")
                    btnPayment.Visible = False
                    lblError.Text = ex.Message
                End Try
            End If
            'Book Button Show Hide - Staff
            Try
                If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true") Then
                    If (Convert.ToString(Session("FlightActive")) <> "True") Then
                        btnPayment.Visible = False
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub SaveBookingInfoNew(ByVal BookingDetails As HotelBooking, ByVal roomdtl As List(Of RoomList))
        Try
            Dim OrderIdExsit As Integer = HTLST.InsHtlHrdDetails(BookingDetails.Orderid, "REQUEST", BookingDetails.HtlCode, BookingDetails.RoomPlanCode, BookingDetails.StarRating, _
                               roomdtl(0).TotalRoomrate, BookingDetails.Total_Org_Roomrate, BookingDetails.Taxes, BookingDetails.ExtraGuestTax, BookingDetails.AmountBeforeTax, BookingDetails.CurrancyRate, _
                                roomdtl(0).AdminMarkupAmt, BookingDetails.AgentMrkAmt, roomdtl(0).AdminMarkupType, roomdtl(0).AgentMarkupType, roomdtl(0).AdminMarkupPer, roomdtl(0).AgentMarkupPer, _
                               roomdtl(0).discountMsg, roomdtl(0).DiscountAMT, BookingDetails.HtlType, BookingDetails.Currency, BookingDetails.Country, BookingDetails.CityName, RommCompositResult.SelectedHotelDetail.HotelAddress, "", _
                                BookingDetails.CheckInDate, BookingDetails.CheckOutDate, BookingDetails.NoofRoom, BookingDetails.NoofNight, BookingDetails.TotAdt, BookingDetails.TotChd, Request.UserHostAddress, _
                                Session("UID").ToString(), BookingDetails.AgencyName, Htlsearch.servicetax, roomdtl(0).AgentServiseTaxAmt, roomdtl(0).V_ServiseTaxAmt, roomdtl(0).Provider, 0, 0, "")
            'BookingDetails.CommisionPer, BookingDetails.CommisionAmt, BookingDetails.CommisionType)

            If OrderIdExsit < 1 Then
                btnPayment.Visible = False
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Due To Some Problem we are Unable to Book Your Hotel. Please Search Another Hotel.');", True)
            Else
                'Insert Hotel Details
                HTLST.InsHotelDetails(BookingDetails.Orderid, BookingDetails.HtlCode, BookingDetails.HotelName, BookingDetails.RoomName, BookingDetails.RoomTypeCode, BookingDetails.RoomPlanCode, BookingDetails.RoomRPH, _
                                   BookingDetails.HotelLocation, RommCompositResult.SelectedHotelDetail.HotelAddress, BookingDetails.CountryCode, BookingDetails.CityCode, BookingDetails.CityName, BookingDetails.Country, _
                   BookingDetails.HotelContactNo, BookingDetails.ThumbImg, roomdtl(0).inclusions, roomdtl(0).EssentialInformation, BookingDetails.SharingBedding, roomdtl(0).OrgRateBreakups, roomdtl(0).MrkRateBreakups, "", _
                     BookingDetails.HotelPolicy, Htlsearch.SearchType, roomdtl(0).Refundable, roomdtl(0).Smoking)
                '      string OrderID, string RoomTypeCode, string RoomPlanCode, string RoomName,  string RoomRPH, int AdultCount, int ChildCount, int RoomNo, string ChildAge
                'Insert Room Details  
                ' Dim roomname As String() = Regex.Split(BookingDetails.RoomName, "and")
                For rn As Integer = 1 To Htlsearch.NoofRoom
                    HTLST.InsRoomDetails(BookingDetails.Orderid, roomdtl(0).RoomTypeCode, roomdtl(0).RatePlanCode, BookingDetails.RoomName, "", _
                                           Htlsearch.AdtPerRoom(rn - 1), Htlsearch.ChdPerRoom(rn - 1), rn, "")
                Next
                HTLST.UpdateHotelDiscountInCheckout(BookingDetails.Orderid, roomdtl(0).MinCapvalue, roomdtl(0).RetaintionPer, roomdtl(0).TDSPercentage, roomdtl(0).TDSAmt, roomdtl(0).AgentCommissionAmt, _
                  roomdtl(0).AdminCommissionAmt, roomdtl(0).GSTPercentage, roomdtl(0).GSTAmt, roomdtl(0).SupplierCommissionAmt, roomdtl(0).SupplierCommissionPer, roomdtl(0).SupplierCommisionTaxIncluded)
            End If
        Catch ex As Exception
            HotelDA.InsertHotelErrorLog(ex, "HotelCheckOut_SaveBookingInfoNew")
            btnPayment.Visible = False
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayment.Click
        Dim ObjBooking As New HotelBooking
        Try
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            Else
                ObjBooking = CType(Session("HotelBookingDetails"), HotelBooking)
                ObjBooking.PGTitle = TitleDDL.SelectedValue
                ObjBooking.PGFirstName = Fname.Text
                ObjBooking.PGLastName = Lname.Text
                ObjBooking.PGEmail = txt_email.Text
                ObjBooking.PGAddress = TB_AddLine.Text
                ObjBooking.PGCity = TB_City.Text
                ObjBooking.PGState = DDL_State.Text
                ObjBooking.PGPin = TB_PinCode.Text
                ObjBooking.PGCountry = txtCountry.Text
                ObjBooking.PGContact = txtCIPhoneNo.Text
                ObjBooking.PGNationality = Request("CountryCode")
                ObjBooking.PgCharges = HdnPgCharge.Value
                ObjBooking.PaymentMode = rblPaymentMode.SelectedValue

                Session("CheckInOutDetails") = Htlsearch
                Session("HotelBookingDetails") = ObjBooking
                'Insert Gest Info
                HTLST.insGuestDetails(ObjBooking.Orderid, "Primary", TitleDDL.SelectedValue, Fname.Text.Trim, Lname.Text.Trim, txtCIPhoneNo.Text.Trim, txt_email.Text.Trim, _
                                      txtCountry.Text.Trim, DDL_State.Text.Trim, TB_City.Text.Trim, TB_AddLine.Text.Trim, TB_PinCode.Text.Trim, 1, 0, Session("UID").ToString(), "")
                Dim objpaxDetails As New List(Of PaxInfo)()

                objpaxDetails.Add(New PaxInfo() With {.Title = ObjBooking.PGTitle, .FName = ObjBooking.PGFirstName, .LName = ObjBooking.PGLastName, .PaxType = "Adult", .DOB = "", .RoomNo = 1})

                If ObjBooking.TotAdt + ObjBooking.TotChd > 1 Then
                    Dim PaxID As Integer = 1
                    Dim list As New ArrayList
                    For Each guest As RepeaterItem In rptItems.Items
                        Dim surName As String = DirectCast(guest.FindControl("ATitleDDL"), DropDownList).SelectedValue
                        Dim nameF As String = DirectCast(guest.FindControl("txtFName"), TextBox).Text.Trim()
                        Dim nameL As String = DirectCast(guest.FindControl("txtLName"), TextBox).Text.Trim()
                        Dim rn As String = DirectCast(guest.FindControl("lblRNO"), Label).Text
                        Dim paxType As String = (DirectCast(guest.FindControl("lblPaxType"), Label).Text)
                        PaxID = PaxID + 1
                        If (DirectCast(guest.FindControl("lblPaxType"), Label).Text).Contains("Child Age of") Then
                            paxType = "Child"
                        Else
                            paxType = "Adult"
                        End If
                        Dim addiGuest As New ArrayList
                        addiGuest.Add(surName)
                        addiGuest.Add(nameF)
                        addiGuest.Add(nameL)
                        addiGuest.Add(paxType)
                        addiGuest.Add(DirectCast(guest.FindControl("CAge"), Label).Text)
                        addiGuest.Add(rn)
                        list.Add(addiGuest)
                        objpaxDetails.Add(New PaxInfo() With {.Title = surName, .FName = nameF, .LName = nameL, .PaxType = paxType, .DOB = DirectCast(guest.FindControl("CAge"), Label).Text, .RoomNo = rn})
                    Next
                    ObjBooking.AdditinalGuest = list
                End If
                ObjBooking.PaxInformation = objpaxDetails
                Session("CheckInOutDetails") = Htlsearch
                Session("HotelBookingDetails") = ObjBooking
                'Wallet Amount Check
                If rblPaymentMode.SelectedValue = "WL" Then
                    Try
                        'Update Payment Modein HotelbookingHeader table
                        HTLST.UpdateHtlHeader(ObjBooking.Orderid, rblPaymentMode.SelectedValue, "", "", "")
                    Catch ex As Exception
                        HotelDA.InsertHotelErrorLog(ex, "HotelCheckOut_Getbalance")
                    End Try
                    Dim BalanceCheck As Integer = HTLST.CheckBalance(Session("UID").ToString(), ObjBooking.AgentDebitAmt)
                    If (BalanceCheck = 0) Then
                        Response.Redirect("HtlBookwait.aspx", False)
                    Else
                        lblError.Text = "InSufficient Credit Limit. Please Contact Administrator."
                        btnPayment.Visible = False
                    End If
                Else
                    'Update Payment Modein HotelbookingHeader table
                    HTLST.UpdateHtlHeader(ObjBooking.Orderid, "PG", "", "", "")


                    Dim objPg As New PG.PaymentGateway()
                    Dim objpt As New PaytmWall.PaytmTrans()
                    Dim ipAddress As String
                    Dim PgMsg As String = ""
                    Dim FT As String = "Hotel"
                    ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                    If ipAddress = "" Or ipAddress Is Nothing Then
                        ipAddress = Request.ServerVariables("REMOTE_ADDR")
                    End If
                    Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                    Dim Tid As String = ReferenceNo.Substring(4, 16)

                    Dim objDA As New SqlTransaction
                    Dim AgencyDs As DataSet
                    AgencyDs = objDA.GetAgencyDetails(Session("UID"))
                    ''Use for Payment Option
                    If rblPaymentMode.SelectedValue = "Paytm" Then
                        PgMsg = objpt.PaymentGatewayReqPaytm(ObjBooking.Orderid, Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), ObjBooking.AgentDebitAmt, ObjBooking.AgentDebitAmt, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Hotel", ipAddress, "DOM", rblPaymentMode.SelectedValue)
                    Else
                        PgMsg = objPg.PaymentGatewayReqPayU(ObjBooking.Orderid, Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), ObjBooking.AgentDebitAmt, ObjBooking.AgentDebitAmt, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Hotel", ipAddress, "DOM", rblPaymentMode.SelectedValue)
                    End If


                    'PgMsg = objPg.PaymentGatewayReq(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Hotel", ipAddress, "DOM", rblPaymentMode.SelectedValue)

                    If PgMsg.Split("~")(0) = "yes" Then
                        '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ObjBooking.Orderid & "&FT=" & FT & "", False)
                        If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
                            Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
                        Else
                            lblError.Text = "Somthing wrong with payment gatway. Please try again."
                            ''ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(Somthing went worng please try after sometime  -PGH001!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + RequestID + "';", true);
                        End If

                    Else
                        ''Change Page Name for Message 
                        ' Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                        lblError.Text = "Somthing wrong with payment gatway. Please try again."
                    End If
                End If
                'Wallet Amount Check End
            End If
        Catch ex As Exception
            HotelDA.InsertHotelErrorLog(ex, "HotelCheckOut_btnPayment_Click")
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Function getPaxList(ByVal Htlsearch As HotelSearch) As DataTable
        Dim HtlDtlsDT As DataTable = CreategustDT()
        Try
            Dim DtRow As DataRow
            For i As Integer = 0 To Htlsearch.NoofRoom - 1
                If i = 0 Then
                    For j As Integer = 1 To CInt(Htlsearch.AdtPerRoom(i)) - 1
                        DtRow = HtlDtlsDT.NewRow
                        DtRow("RoomNO") = i + 1
                        DtRow("PaxType") = ""
                        DtRow("ChildAge") = ""
                        DtRow("NextRoom") = ""
                        HtlDtlsDT.Rows.Add(DtRow)
                    Next
                Else
                    DtRow = HtlDtlsDT.NewRow
                    DtRow("NextRoom") = "<strong style='color: #083E68;'>Room " + (i + 1).ToString() + " Guest Name</strong><hr />"
                    For j As Integer = 0 To CInt(Htlsearch.AdtPerRoom(i)) - 1
                        If (j > 0) Then
                            DtRow = HtlDtlsDT.NewRow
                            DtRow("NextRoom") = ""
                        End If

                        DtRow("RoomNO") = i + 1
                        DtRow("PaxType") = ""
                        DtRow("ChildAge") = ""
                        HtlDtlsDT.Rows.Add(DtRow)
                    Next
                End If
                If Request.QueryString("Provider") = "GTA" Then
                    For j As Integer = 0 To CInt(Htlsearch.ChdPerRoom(i)) - 1
                        If Htlsearch.ChdAge(i, j) > 2 Then
                            DtRow = HtlDtlsDT.NewRow
                            DtRow("RoomNO") = i + 1
                            DtRow("PaxType") = "Child Age of "
                            DtRow("ChildAge") = Htlsearch.ChdAge(i, j)
                            DtRow("NextRoom") = ""
                            HtlDtlsDT.Rows.Add(DtRow)
                        End If
                    Next
                Else
                    For j As Integer = 0 To CInt(Htlsearch.ChdPerRoom(i)) - 1
                        DtRow = HtlDtlsDT.NewRow
                        DtRow("RoomNO") = i + 1
                        DtRow("PaxType") = "Child Age of "
                        DtRow("ChildAge") = Htlsearch.ChdAge(i, j)
                        DtRow("NextRoom") = ""
                        HtlDtlsDT.Rows.Add(DtRow)
                    Next
                End If
            Next
        Catch ex As Exception
            HotelDA.InsertHotelErrorLog(ex, "getPaxList")
            btnPayment.Visible = False
        End Try
        Return HtlDtlsDT
    End Function

    Protected Function CreategustDT() As DataTable
        Dim HotelDT As New DataTable
        Dim HotelDataColumn As DataColumn
        HotelDataColumn = New DataColumn()
        HotelDataColumn.DataType = Type.GetType("System.String")
        HotelDataColumn.ColumnName = "RoomNO"
        HotelDT.Columns.Add(HotelDataColumn)

        HotelDataColumn = New DataColumn()
        HotelDataColumn.DataType = Type.GetType("System.String")
        HotelDataColumn.ColumnName = "PaxType"
        HotelDT.Columns.Add(HotelDataColumn)

        HotelDataColumn = New DataColumn()
        HotelDataColumn.DataType = Type.GetType("System.String")
        HotelDataColumn.ColumnName = "ChildAge"
        HotelDT.Columns.Add(HotelDataColumn)

        HotelDataColumn = New DataColumn()
        HotelDataColumn.DataType = Type.GetType("System.String")
        HotelDataColumn.ColumnName = "NextRoom"
        HotelDT.Columns.Add(HotelDataColumn)
        Return HotelDT
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPgChargeByMode(paymode As String) As String
        Dim TransCharge As String = "0~P"
        Dim PgCharge As String = "0"
        Dim ChargeType As String = "0"
        Dim objP As New PG.PaymentGateway()
        '' Dim PaymentMode As String = rblPaymentMode.SelectedValue
        ''PaymentMode = rblPaymentMode.SelectedValue
        Try
            'Dim pgDT As DataTable = objP.GetPgTransChargesByMode(paymode, "GetPgCharges")
            Dim pgDT As DataTable = objP.GetPgTransChargesByModeByAgentWise(UserID, paymode, "GetPgCharges")
            If pgDT IsNot Nothing Then
                If pgDT.Rows.Count > 0 Then
                    If Not String.IsNullOrEmpty(Convert.ToString(pgDT.Rows(0)("Charges"))) Then
                        PgCharge = Convert.ToString(pgDT.Rows(0)("Charges")).Trim()
                    Else
                        PgCharge = "0"
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(pgDT.Rows(0)("ChargesType"))) Then
                        ChargeType = Convert.ToString(pgDT.Rows(0)("ChargesType")).Trim()
                    Else
                        ChargeType = "P"
                    End If
                    TransCharge = PgCharge + "~" + ChargeType
                End If
            End If
        Catch ex As Exception
            TransCharge = "0~P"
        End Try
        Return TransCharge
    End Function
End Class
