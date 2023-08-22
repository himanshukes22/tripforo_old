Imports System.Data
Imports PG
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports PaytmWall
Imports STD.BAL

Partial Class FlightDom_PriceDetails
    Inherits System.Web.UI.Page
    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim objFareBreakup As New clsCalcCommAndPlb
    Dim objDA As New SqlTransaction
    '    Dim objSqlTrans As New SqlTransaction
    Dim DomAirDt As DataTable
    Dim trackId As String, LIN As String
    Dim OBTrackId As String, IBTrackId As String
    Dim FT As String = ""
    Dim Adult As Integer
    Dim Child As Integer
    Dim Infant As Integer
    Dim SelectedFltArray As Array
    Dim strFlt As String = "", strFare As String = ""
    Dim fareHashtbl As Hashtable
    Dim STDom As New SqlTransactionDom()
    'varaibles
    Dim VCOB As String = "", VCIB As String = ""
    Dim TripOB As String = "", TripIB As String = ""
    Dim ATOB As String = "", ATIB As String = ""
    Dim FLT_STAT As String = ""

    Private ObjIntDetails As New IntlDetails()
    Dim objSql As New SqlTransactionNew
    Dim TransTD As String = ""
    Dim dtpnr As New DataTable() 'Header
    Dim dtpax As New DataTable() 'Pax
    Dim Flthear As New DataTable()
    Dim dtfare As New DataTable()
    Dim OBFltDs, IBFltDs As DataSet
    Dim SSR_LOG As DataSet
    Dim objUMSvc As New FltSearch1()
    Dim objPg As New PG.PaymentGateway()
    Dim objPt As New PaytmWall.PaytmTrans()
    Dim objBal As New COMN_BAL.Flight.FlighBal(Variables.Constr)
    Dim objDsO As New DataSet()
    Dim objDsR As New DataSet()
    Dim IsHoldVisibleO As Boolean = False
    Dim IsHoldVisibleR As Boolean = False
    Dim holdBookingChargeO As Decimal = 0
    Dim holdBookingChargeR As Decimal = 0
    Dim SeatListO As List(Of STD.Shared.Seat)
    Dim SeatListR As List(Of STD.Shared.Seat)
    Public Shared UserID As String = String.Empty
    Private det As New Details()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            Else
                UserID = Session("UID")
                PaxGrd_div.Visible = False
                If Not Page.IsPostBack Then

                    ViewState("holdBookingChargeO") = 0
                    ViewState("holdBookingChargeR") = 0
                    SeatListO = New List(Of STD.Shared.Seat)
                    SeatListR = New List(Of STD.Shared.Seat)
                    Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                    If HttpContext.Current.Request.QueryString.Count >= 3 Then
                        OBTrackId = HttpContext.Current.Request.QueryString("OBTID").ToString()
                        IBTrackId = HttpContext.Current.Request.QueryString("IBTID").ToString()
                        FT = "InBound"
                        HdnTripType.Value = FT
                        OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID"))
                        IBFltDs = objDA.GetFltDtls(IBTrackId, Session("UID"))
                        SeatListO = IFLT.SeatDetails(OBTrackId)
                        SeatListR = IFLT.SeatDetails(IBTrackId)
                    ElseIf HttpContext.Current.Request.QueryString.Count = 2 Then
                        OBTrackId = HttpContext.Current.Request.QueryString("OBTID").ToString()
                        FT = "OutBound"
                        HdnTripType.Value = FT
                        OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID"))
                        SeatListO = IFLT.SeatDetails(OBTrackId)
                    End If
                    ViewState("FT") = FT
                    Session("FT") = FT
                    If FT = "OutBound" Then
                        ViewState("OBTrackId") = OBTrackId
                        Session("OBTrackId") = OBTrackId
                    Else
                        ViewState("OBTrackId") = OBTrackId
                        ViewState("IBTrackId") = IBTrackId

                        Session("OBTrackId") = OBTrackId
                        Session("IBTrackId") = IBTrackId
                    End If

                    Adult = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                    Child = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                    Infant = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))
                    ''Seat
                    Dim seatdetails As String = ""
                    Dim seatFareO As Integer = 0
                    Dim seatFareR As Integer = 0

                    Dim dt As DataTable = New DataTable()
                    dt = det.AgencyInfo(UserID).Tables(0)
                    If dt.Rows.Count > 0 Then
                        'lblagency.Text = ds.Tables(0).Rows(0)("Cred_Limit").ToString()
                        'lblCamt.Visible = True
                        hdnAvlBal.Value = "₹ " + dt.Rows(0)("Crd_Limit").ToString().Replace(".0000", ".00")
                        'td_AgencyID.InnerText = ds.Tables(0).Rows(0)("user_id").ToString()
                        'lblagency.Text = Session("AgencyName")
                        ''Session("AGTY") = ds.Tables(0).Rows(0)("Agent_Type").ToString()
                    End If

                    If SeatListO.Count > 0 Then
                        seatdetails &= "<div class='row'>"
                        seatdetails &= "<div class='large-12 medium-12 small-12  headbgs'><i class='fa fa-wheelchair' aria-hidden='true'></i> Traveller Seat Information</div>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Traveller(OutBound)</div>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Sector</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Seat</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Type</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Amount</div>"
                        seatdetails &= "<div class='col-md-12'>&nbsp</div>"
                        For i As Integer = 0 To SeatListO.Count - 1
                            dt = ObjIntDetails.SelectPaxDetail(OBTrackId, SeatListO(i).PaxId)
                            seatdetails &= "<div class='col-md-3'>" & dt.Rows(0)("Name") & "</div>"
                            seatdetails &= "<div class='col-md-3'>" & SeatListO(i).Origin & "-" & SeatListO(i).Destination & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListO(i).SeatDesignator & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListO(i).SeatAlignment & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListO(i).Amount & "</div>"
                            seatFareO = seatFareO + Convert.ToInt32(SeatListO(i).Amount)

                            seatdetails &= "<div class='col-md-12'>&nbsp</div>"
                        Next
                        seatdetails &= "</div>"
                    End If
                    If SeatListR.Count > 0 Then
                        seatdetails &= "<div class='row'>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Traveller(InBound)</div>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Sector</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Seat</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Type</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #fff!important; font-weight: bold !important; background: #f3dc56; margin-top: 1px; font-size: 14px;'>Amount</div>"
                        seatdetails &= "<div class='col-md-12'>&nbsp</div>"
                        For i As Integer = 0 To SeatListR.Count - 1
                            dt = ObjIntDetails.SelectPaxDetail(IBTrackId, SeatListR(i).PaxId)
                            seatdetails &= "<div class='col-md-3'>" & dt.Rows(0)("Name") & "</div>"
                            seatdetails &= "<div class='col-md-3'>" & SeatListR(i).Origin & "-" & SeatListR(i).Destination & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListR(i).SeatDesignator & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListR(i).SeatAlignment & "</div>"
                            seatdetails &= "<div class='col-md-2'>" & SeatListR(i).Amount & "</div>"
                            seatFareR = seatFareR + Convert.ToInt32(SeatListR(i).Amount)
                            seatdetails &= "<div class='col-md-12'>&nbsp</div>"
                        Next

                        seatdetails &= "</div>"
                    End If
                    SeatInformation.InnerHtml = seatdetails

                    divFltDtls.InnerHtml = "<div><div class='bld' style='padding-left:0px;'>" & STDom.CustFltDetails_Dom(OBFltDs, IBFltDs, FT) & "</div></div>" 'STDom.CustFltDetails_Dom(OBFltDs, IBFltDs, FT) 'showFltDetails(OBFltDs, IBFltDs, FT) ' Flight Details Both in 1 
                    'divFareDtls.InnerHtml = "<b>OutBound :-</b><br/> <br/>" & fareBreakupfun(OBFltDs, "OutBound", "O")
                    If FT = "OutBound" Then
                        divFareDtls.InnerHtml = "<div class=''><div class='large-12 medium-12 small-12  headbgs' style='line-height: 33px !important;font-weight:600;'>&nbsp;  OutBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div><div>" & fareBreakupfun2(OBFltDs, "OutBound", "O", Adult, Child, Infant, seatFareO) & "</div></div>"
                    Else
                        divFareDtls.InnerHtml = "<div class='bor'><div class='f16 bgf1 bld lh30' style='color:#424242'>&nbsp; OutBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div><div>" & fareBreakupfun2(OBFltDs, "OutBound", "O", Adult, Child, Infant, seatFareR) & "</div></div>"
                    End If


                    divPaxdetails.InnerHtml = showPaxDetails(OBTrackId, FT)

                    div_FlightDel.InnerHtml = STDom.Bind_Flight_PaxDetails(OBFltDs, IBFltDs, FT, ObjIntDetails.SelectPaxDetail(OBTrackId, TransTD))

                    VCOB = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    TripOB = OBFltDs.Tables(0).Rows(0)("Trip")
                    'FLT_STAT = OBFltDs.Tables(0).Rows(0)("FlightStatus")
                    objDsO = objBal.GetFltHoldBookingCharge(Session("agent_type"), Session("UID"), VCOB, TripOB)

                    If objDsO.Tables(0).Rows.Count > 0 Then

                        ViewState("IsHoldVisibleO") = Convert.ToBoolean(objDsO.Tables(0).Rows(0)("HoldBooking"))
                        ViewState("holdBookingChargeO") = Convert.ToDecimal(objDsO.Tables(0).Rows(0)("Charges"))
                        '' "Request"



                    End If

                    If FT = "InBound" Then
                        'divFareDtlsR.InnerHtml = "<b>InBound :-</b><br/> <br/>" & fareBreakupfun(IBFltDs, FT, "O")
                        divFareDtlsR.InnerHtml = "<div class='brdr bor'><div class='f16 bgf1 bld lh30' style='color:#424242'>&nbsp; InBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div><div>" & fareBreakupfun2(IBFltDs, FT, "O", Adult, Child, Infant, seatFareR) & "</div></div>"
                        VCIB = IBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                        TripIB = IBFltDs.Tables(0).Rows(0)("Trip")
                        objDsR = objBal.GetFltHoldBookingCharge(Session("agent_type"), Session("UID"), VCIB, TripIB)

                        If objDsR.Tables(0).Rows.Count > 0 Then

                            ViewState("IsHoldVisibleR") = Convert.ToBoolean(objDsR.Tables(0).Rows(0)("HoldBooking"))
                            ViewState("holdBookingChargeR") = Convert.ToDecimal(objDsR.Tables(0).Rows(0)("Charges"))

                        End If


                    Else
                        ViewState("IsHoldVisibleR") = True

                    End If

                    ''code here for- to visible hold button
                    If Convert.ToBoolean(ViewState("IsHoldVisibleO")) = True And Convert.ToBoolean(ViewState("IsHoldVisibleR")) = True Then

                        ButtonHold.Visible = True

                        lblHoldBookingCharge.Text = " Rs. " & (Convert.ToDecimal(ViewState("holdBookingChargeO")) + Convert.ToDecimal(ViewState("holdBookingChargeR"))) & "  will be charged to hold the booking."
                    Else
                        ButtonHold.Visible = False
                        lblHoldBookingCharge.Text = ""
                    End If


                Else
                    'Page Post Back

                End If

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    <System.Web.Services.WebMethod()>
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
                    ''PgCharge = Convert.ToString(pgDT.Rows(0)("Charges")).Trim()
                    ''ChargeType = Convert.ToString(pgDT.Rows(0)("ChargesType"))
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
    Private Function showFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
        Try
            strFlt = "<div class='w45 lft padding2s'><div class='f18' style='color:#004b91'>OutBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div>"
            For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
                strFlt = strFlt & "<div>"
                strFlt = strFlt & "<div class='w24 padding1 lft'><div class='f16'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & OBDS.Tables(0).Rows(i)("ArrivalLocation") & "</div><div>" & OBDS.Tables(0).Rows(i)("Departure_Date") & "</div></div>"
                strFlt = strFlt & "<div class='w40 padding1 lft'><div class='f16'>" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & " - " & OBDS.Tables(0).Rows(i)("FlightIdentification") & "</div><div>Class: " & OBDS.Tables(0).Rows(i)("RBD") & "</div></div>"
                strFlt = strFlt & "<div class='w28 padding1 lft'><div><span class='lft w30'>Dep:</span>" & OBDS.Tables(0).Rows(i)("DepartureTime") & " Hrs</div><div><span class='lft w30'>Arr:</span> " & OBDS.Tables(0).Rows(i)("ArrivalTime") & " Hrs</div></div>"
                strFlt = strFlt & "</div>"
            Next
            strFlt = strFlt & "</div>"
            If FT = "InBound" Then
                strFlt = strFlt & "<div class='padding2s w45 rgt'><div class='f18' style='color:#004b91'>InBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div>"
                For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
                    strFlt = strFlt & "<div>"
                    strFlt = strFlt & "<div class='w24 padding1 lft'><div class='f16'>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & IBDS.Tables(0).Rows(i)("ArrivalLocation") & "</div><div>" & IBDS.Tables(0).Rows(i)("Departure_Date") & "</div></div>"
                    strFlt = strFlt & "<div class='w40 padding1 lft'><div class='f16'>" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & " - " & IBDS.Tables(0).Rows(i)("FlightIdentification") & "</div><div>Class: " & IBDS.Tables(0).Rows(i)("RBD") & "</div></div>"
                    strFlt = strFlt & "<div class='w28 padding1 lft'><div><span class='lft w30'>Dep:</span>" & IBDS.Tables(0).Rows(i)("DepartureTime") & "Hrs</div><div><span class='lft w30'>Arr:</span> " & IBDS.Tables(0).Rows(i)("ArrivalTime") & "Hrs</div></div>"
                    strFlt = strFlt & "</div>"
                Next
                strFlt = strFlt & "</div><div class='clear1'></div>"
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


        Return strFlt
    End Function
    'Private Function fareBreakupfun2(ByVal OFareDS As DataSet, ByVal Ft As String, ByVal Trip As String, ByVal Adult As Integer, ByVal Child As Integer, ByVal Inf As Integer) As String
    '    Try
    '        Dim dtpnr As New DataTable()
    '        Dim OrderId As String = OFareDS.Tables(0).Rows(0)("Track_id").ToString()
    '        dtpnr = ObjIntDetails.SelectHeaderDetail(OrderId)
    '        Dim VC As String = OFareDS.Tables(0).Rows(0)("ValiDatingCarrier")
    '        Dim VCSPL As String = OFareDS.Tables(0).Rows(0)("AdtFareType")
    '        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(OFareDS.Tables(0).Rows(0)("Track_id").ToString(), "")
    '        Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
    '        If (MBDT.Tables(0).Rows.Count > 0) Then
    '            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
    '                MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
    '                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
    '                MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
    '            Next
    '            'IF Header is Updated then Only Allow Booking
    '            SSR_LOG = objSql.GetSSR_Log_Detail(OrderId)
    '            If (Convert.ToDecimal(SSR_LOG.Tables(0).Rows(0)("Header_TBC")) + MBPR) <= Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("TotFare").ToString()) Then
    '                Submit.Visible = True
    '            Else
    '                'Submit.Visible = False
    '                Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '            End If
    '        End If

    '        'OTHER CALCULATION
    '        Dim TC As Decimal = 0
    '        If OFareDS.Tables(0).Rows(0)("AdtFareType").ToString() = "Special Fare" Then
    '            TC = (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("ADTAgentMrk")) * Adult) + (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("CHDAgentMrk")) * Child)
    '        Else
    '            TC = OFareDS.Tables(0).Rows(0)("TC")
    '        End If


    '        'END CALCULATION

    '        Dim tax(), tax1() As String, yq As Integer = 0, tx As Integer = 0
    '        tax = OFareDS.Tables(0).Rows(0)("Adt_Tax").ToString.Split("#")
    '        For i As Integer = 0 To tax.Length - 2
    '            If InStr(tax(i), "YQ") Then
    '                tax1 = tax(i).Split(":")
    '                yq = yq + Convert.ToInt32(tax1(1))
    '            Else
    '                tax1 = tax(i).Split(":")
    '                tx = tx + Convert.ToInt32(tax1(1))
    '            End If
    '        Next
    '        Dim T_ID As String = ""
    '        If Ft = "OutBound" Then
    '            T_ID = "OB_FT"
    '            strFare = "<table id='" + T_ID + "' border='0' class='TTBBL w98 padding1' width='100%' >"
    '        ElseIf Ft = "InBound" Then
    '            T_ID = "IB_FT"
    '            strFare = "<table id='" + T_ID + "' border='0' class='TTBBL w98 padding1' width='100%'>"
    '        End If

    '        'strFare = strFare & "<tr>"
    '        'strFare = strFare & "<th>Pax Type</th>"
    '        'strFare = strFare & "<th>Base Fare</th>"
    '        'strFare = strFare & "<th>Fuel Charge</th>"
    '        'strFare = strFare & "<th>Tax </th>"
    '        'strFare = strFare & "<th>Total</th>"
    '        ''strFare = strFare & "<th>Qty</th>"
    '        'strFare = strFare & "<th>Other Details</th>"

    '        'strFare = strFare & "</tr>"

    '        strFare = strFare & "<tr>"
    '        strFare = strFare & "<td valign='top'>"
    '        strFare = strFare & "<table border='0' width='100%'>"

    '        strFare = strFare & "<tr>"
    '        strFare = strFare & "<td class='bld'>Pax Type</td>"
    '        strFare = strFare & "<td class='bld'>Base Fare</td>"
    '        strFare = strFare & "<td class='bld'>Fuel Charge</td>"
    '        strFare = strFare & "<td class='bld'>Tax </td>"
    '        strFare = strFare & "<td class='bld'>Total</td>"
    '        strFare = strFare & "</tr>"


    '        strFare = strFare & "<tr>"
    '        strFare = strFare & "<td>ADULT</td>"
    '        strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("AdtBFare") & "</td>"
    '        strFare = strFare & "<td>" & yq & "</td>"
    '        strFare = strFare & "<td>" & tx & "</td>"
    '        strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("AdtFare") & "&nbsp;<b>x</b>" & Adult & "</td>"
    '        'strFare = strFare & "<td><b>x</b>" & Adult & "</td>"

    '        strFare = strFare & "</tr>"

    '        If Child > 0 Then
    '            Try
    '                yq = 0
    '                tx = 0
    '                tax = OFareDS.Tables(0).Rows(0)("Chd_Tax").ToString.Split("#")
    '                For i As Integer = 0 To tax.Length - 2
    '                    If InStr(tax(i), "YQ") Then
    '                        tax1 = tax(i).Split(":")
    '                        yq = yq + Convert.ToInt32(tax1(1))
    '                    Else
    '                        tax1 = tax(i).Split(":")
    '                        tx = tx + Convert.ToInt32(tax1(1))
    '                    End If
    '                Next
    '            Catch ex As Exception
    '            End Try

    '            strFare = strFare & "<tr>"
    '            strFare = strFare & "<td>CHILD</td>"
    '            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("ChdBFare") & "</td>"
    '            strFare = strFare & "<td>" & yq & "</td>"
    '            strFare = strFare & "<td>" & tx & "</td>"
    '            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("ChdFare") & "&nbsp;<b>x</b>" & Child & "</td>"
    '            'strFare = strFare & "<td><b>x</b>" & Child & "</td>"
    '            strFare = strFare & "</tr>"
    '        End If

    '        If Infant > 0 Then
    '            Try
    '                yq = 0
    '                tx = 0
    '                tax = OFareDS.Tables(0).Rows(0)("Inf_Tax").ToString.Split("#")
    '                For i As Integer = 0 To tax.Length - 2
    '                    If InStr(tax(i), "YQ") Then
    '                        tax1 = tax(i).Split(":")
    '                        yq = yq + Convert.ToInt32(tax1(1))
    '                    Else
    '                        tax1 = tax(i).Split(":")
    '                        tx = tx + Convert.ToInt32(tax1(1))
    '                    End If
    '                Next
    '            Catch ex As Exception
    '            End Try
    '            strFare = strFare & "<tr>"
    '            strFare = strFare & "<td>INFANT</td>"
    '            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("InfBFare") & "</td>"
    '            strFare = strFare & "<td>" & yq & "</td>"
    '            strFare = strFare & "<td>" & tx & "</td>"
    '            strFare = strFare & "<td>" & OFareDS.Tables(0).Rows(0)("InfFare") & "&nbsp;<b>x</b>" & Infant & "</td>"
    '            'strFare = strFare & "<td><b>x</b>" & Infant & "</td>"
    '            strFare = strFare & "</tr>"
    '        End If


    '        strFare = strFare & "</table>"
    '        strFare = strFare & "</td>"


    '        strFare = strFare & "<td>"
    '        strFare = strFare & "<table border='0' width='100%'>"
    '        strFare = strFare & "<tr>"
    '        strFare = strFare & "<td colspan='2' class='bld'>Other Details</td>"
    '        strFare = strFare & "</tr>"
    '        strFare = strFare & "<tr><td width='100px'>Srv.Tax</td><td>" & OFareDS.Tables(0).Rows(0)("SrvTax") & "</td></tr>"
    '        If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
    '            strFare = strFare & "<tr><td>Mgnt. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</td></tr>"
    '        Else
    '            strFare = strFare & "<tr><td>Tran. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TFee") & "</td></tr>"
    '            strFare = strFare & "<tr><td>Tran. Chg</td><td>" & TC & "</td></tr>"
    '        End If

    '        If (VC = "SG" Or VC = "6E") And InStr(VCSPL, "Special") = 0 Then
    '            strFare = strFare & "<tr><td>Meal Chg.</td><td>" & MealPr & "</td></tr>"
    '            strFare = strFare & "<tr><td>Bagg Chg.</td><td>" & BgPr & "</td></tr>"
    '        End If
    '        strFare = strFare & "<tr><td id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #004b91;;font-weight:bold'>Total Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</td></tr>"
    '        strFare = strFare & "<tr id='trnetfare" & Ft & "' style='display:none'><td>Net Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & "</td></tr>"
    '        strFare = strFare & "</table>"
    '        strFare = strFare & "</td>"

    '        strFare = strFare & "</tr>"
    '        'strFare = strFare & "<tr>"
    '        'strFare = strFare & "<td>INFANT</td>"

    '        'strFare = strFare & "</tr>"

    '        'strFare = strFare & "<tr><td colspan='3' rowspan='7'></td><td>Srv.Tax</td><td>" & OFareDS.Tables(0).Rows(0)("SrvTax") & "</td></tr>"
    '        'If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
    '        '    strFare = strFare & "<tr><td>Mgnt. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</td></tr>"
    '        'Else
    '        '    strFare = strFare & "<tr><td>Tran. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TFee") & "</td></tr>"
    '        '    strFare = strFare & "<tr><td>Tran. Chg</td><td>" & TC & "</td></tr>"
    '        'End If

    '        'If (VC = "SG" Or VC = "6E") And InStr(VCSPL, "Special") = 0 Then
    '        '    strFare = strFare & "<tr><td>Meal Chg.</td><td>" & MealPr & "</td></tr>"
    '        '    strFare = strFare & "<tr><td>Bagg Chg.</td><td>" & BgPr & "</td></tr>"
    '        'End If

    '        'strFare = strFare & "<tr><td id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #006600'>Total Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</td></tr>"
    '        'strFare = strFare & "<tr id='trnetfare" & Ft & "' style='display:none'><td>Net Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & "</td></tr>"
    '        strFare = strFare & "</table>"
    '    Catch ex As Exception
    '        'clsErrorLog.LogInfo(ex)
    '    End Try


    '    Return strFare
    'End Function
    Private Function fareBreakupfun2(ByVal OFareDS As DataSet, ByVal Ft As String, ByVal Trip As String, ByVal Adult As Integer, ByVal Child As Integer, ByVal Inf As Integer, ByVal Searfare As Integer) As String
        Try
            Dim dtpnr As New DataTable()
            Dim OrderId As String = OFareDS.Tables(0).Rows(0)("Track_id").ToString()
            dtpnr = ObjIntDetails.SelectHeaderDetail(OrderId)
            Dim VC As String = OFareDS.Tables(0).Rows(0)("ValiDatingCarrier")
            Dim VCSPL As String = OFareDS.Tables(0).Rows(0)("AdtFareType")
            Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(OFareDS.Tables(0).Rows(0)("Track_id").ToString(), "")
            Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
            If (MBDT.Tables(0).Rows.Count > 0) Then
                For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                    MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
                    BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                Next
                'IF Header is Updated then Only Allow Booking
                SSR_LOG = objSql.GetSSR_Log_Detail(OrderId)
                If (Convert.ToDecimal(SSR_LOG.Tables(0).Rows(0)("Header_TBC")) + MBPR) <= Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("TotFare").ToString()) Then
                    Submit.Visible = True
                Else
                    'Submit.Visible = False
                    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                End If
            End If

            'OTHER CALCULATION
            Dim TC As Decimal = 0
            If OFareDS.Tables(0).Rows(0)("AdtFareType").ToString() = "Special Fare" Then
                TC = (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("ADTAgentMrk")) * Adult) + (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("CHDAgentMrk")) * Child)
            Else
                TC = OFareDS.Tables(0).Rows(0)("TC")
            End If

            Dim tcperpax As Integer = TC / (Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Adult")) + Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Child")))
            'END CALCULATION

            Dim tax(), tax1() As String, yq As Decimal = 0, tx As Decimal = 0
            Dim charsplit As Char() = {"#"c}

            tax = OFareDS.Tables(0).Rows(0)("Adt_Tax").ToString.Split("#") ''Split(charsplit, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    yq = yq + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                Else
                    tax1 = tax(i).Split(":")
                    tx = tx + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                End If
            Next
            Dim T_ID As String = ""
            If Ft = "OutBound" Then
                T_ID = "OB_FT"
                strFare = "<div id='" + T_ID + "' border='0' class='large-12 medium-12 small-12'>"
            ElseIf Ft = "InBound" Then
                T_ID = "IB_FT"
                strFare = "<div id='" + T_ID + "' border='0' class='large-12 medium-12 small-12'>"
            End If


            strFare = strFare & "<div class='theme-sidebar-section _mb-10'>"

            strFare = strFare & "<div class='theme-sidebar-section-charges'>"
            strFare = strFare & "<ul class='theme-sidebar-section-charges-list'>"
            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<div class='li'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Passenger Type</h5>"
            'strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>Adult</p>"
            strFare = strFare & "</li>"
            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Base Fare</h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ ₹ " & OFareDS.Tables(0).Rows(0)("AdtBFare") & "</span></p>"
            strFare = strFare & "</li>"
            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Fuel Charge</h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & yq & "</p>"
            strFare = strFare & "</li>"
            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Tax</h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & (tx + tcperpax) & "</p>"
            strFare = strFare & "</li>"
            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Total</h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & (OFareDS.Tables(0).Rows(0)("AdtFare") + tcperpax) & "&nbsp;<b>x</b>" & Adult & "</p>"
            strFare = strFare & "</li>"
            strFare = strFare & "</ul>"
            strFare = strFare & "</div>"




            If Child > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Chd_Tax").ToString.Split("#") ''Split(charsplit, System.StringSplitOptions.RemoveEmptyEntries)
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                        End If
                    Next
                Catch ex As Exception
                End Try

                strFare = strFare & "<div class='theme-sidebar-section _mb-10'>"
                strFare = strFare & "<div class='theme-sidebar-section-charges'>"
                strFare = strFare & "<ul class='theme-sidebar-section-charges-list'>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<div class='li'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Passenger Type</h5>"
                'strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>Child</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Base Fare</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ ₹ " & OFareDS.Tables(0).Rows(0)("ChdBFare") & "</span></p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Fuel Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & yq & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Tax</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & (tx + tcperpax) & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Total</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & (OFareDS.Tables(0).Rows(0)("ChdFare") + tcperpax) & "&nbsp;<b>x</b>" & Child & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "</ul>"
                strFare = strFare & "</div>"
                strFare = strFare & "</div>"


            End If

            If Infant > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Inf_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToDecimal(Math.Ceiling(Convert.ToDecimal(tax1(1))))
                        End If
                    Next
                Catch ex As Exception
                End Try

                strFare = strFare & "<div class='theme-sidebar-section-charges'>"
                strFare = strFare & "<ul class='theme-sidebar-section-charges-list'>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<div class='li'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Passenger Type</h5>"
                'strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>Infant</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Base Fare</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ ₹ " & OFareDS.Tables(0).Rows(0)("InfBFare") & "</span></p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Fuel Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & yq & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Tax</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & (tx + tcperpax) & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Total</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & OFareDS.Tables(0).Rows(0)("InfFare") & "&nbsp;<b>x</b>" & Infant & "</p>"
                strFare = strFare & "</li>"
                strFare = strFare & "</ul>"
                strFare = strFare & "</div>"



            End If
            strFare = strFare & "</div>"


            strFare = strFare & "<label style='font-weight: 800;font-size: 14px;position: relative;'>Other Details</label>"
            strFare = strFare & "<div class='theme-sidebar-section _mb-10'>"
            strFare = strFare & "<div class='theme-sidebar-section-charges'>"
            strFare = strFare & "<ul class='theme-sidebar-section-charges-list'>"
            'strFare = strFare & "<div class='table'>"
            'strFare = strFare & "<div class='theader'>"
            'strFare = strFare & "<div class='table_header'>GST</div>"  'Srv.Tax
            If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
                'strFare = strFare & "<div class='table_header'>Management Fee</div>"
            Else
                'strFare = strFare & "<div class='table_header'>Transaction Fee</div>"
                'strFare = strFare & "<div class='table_header'>Transaction Charge</div>"
            End If
            'strFare = strFare & "<div class='table_header'>PG Charge</div>"
            If (VC = "SG" Or VC = "6E" Or VC = "G8" Or VC = "IX" Or OFareDS.Tables(0).Rows(0)("Provider") = "AK") And InStr(VCSPL, "Special") = 0 Then
                'strFare = strFare & "<div class='table_header'>Meal Charge</div>"
                'strFare = strFare & "<div class='table_header'>Baggage Charge</div>"
                'strFare = strFare & "<div class='table_header'>Seat Charge</div>"
            End If
            'strFare = strFare & "<div class='table_header'>Gross Total</div>"
            'strFare = strFare & "</div>"



            'strFare = strFare & "<div class='large-3 medium-3 small-12 columns'>"











            'strFare = strFare & "<div class='table_row'>"
            'strFare = strFare & "<div class='table_small'>"
            'strFare = strFare & "<div class='table_cell'>GST</div>" 'Srv.Tax
            'strFare = strFare & "<div class='table_cell'>₹ " & OFareDS.Tables(0).Rows(0)("SrvTax") & "</div>"
            'strFare = strFare & "</div>"

            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<div class='li'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>GST</h5>"
            'strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & OFareDS.Tables(0).Rows(0)("SrvTax") & "</p>"
            strFare = strFare & "</li>"
            If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Management Fee</div>"
                'strFare = strFare & "<div class='table_cell'>₹ " & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</div>"
                'strFare = strFare & "</div>"

                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Management Fee</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</span></p>"
                strFare = strFare & "</li>"
            Else
                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Transaction Fee</div>"
                'strFare = strFare & "<div class='table_cell'>₹ " & OFareDS.Tables(0).Rows(0)("TFee") & "</div>"
                'strFare = strFare & "</div>"

                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Transaction Fee</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & OFareDS.Tables(0).Rows(0)("TFee") & "</span></p>"
                strFare = strFare & "</li>"

                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Transaction Charge</div>"
                'strFare = strFare & "<div class='table_cell'>₹ 0</div>"
                'strFare = strFare & "</div>"

                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Transaction Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ 0</span></p>"
                strFare = strFare & "</li>"

            End If
            'strFare = strFare & "<div class='table_small'>"
            'strFare = strFare & "<div class='table_cell'>PG. Chg. <input type='hidden' name='hdnTotalFare" & Ft & "' id='hdnTotalFare" & Ft & "' value=" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & " ></div>"
            'strFare = strFare & "<div class='table_cell'  id='PgCharge" & Ft & "'>₹ 0.00</div>"
            'strFare = strFare & "</div>"

            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>PG. Chg. <input type='hidden' name='hdnTotalFare" & Ft & "' id='hdnTotalFare" & Ft & "' value=" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & " ></h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price' id='PgCharge" & Ft & "'>₹ 0.00</span></p>"
            strFare = strFare & "</li>"


            If (VC = "SG" Or VC = "6E" Or VC = "G8" Or VC = "IX" Or OFareDS.Tables(0).Rows(0)("Provider") = "AK") And InStr(VCSPL, "Special") = 0 Then
                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Meal Charge</div>"
                'strFare = strFare & "<div class='table_cell'>₹ " & MealPr & "</div>"
                'strFare = strFare & "</div>"

                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Meal Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & MealPr & "</span></p>"
                strFare = strFare & "</li>"

                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Baggage Charge</div>"
                'strFare = strFare & "<div class='table_cell'>₹ " & BgPr & "</div>"
                'strFare = strFare & "</div>"

                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Baggage Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & BgPr & "</span></p>"
                strFare = strFare & "</li>"

                'strFare = strFare & "<div class='table_small'>"
                'strFare = strFare & "<div class='table_cell'>Seat Charge</div>"
                'strFare = strFare & "<div class='table_cell'>₹ " & Searfare & "</div>"
                'strFare = strFare & "</div>"


                strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
                strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title'>Seat Charge</h5>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
                strFare = strFare & "<p class='theme-sidebar-section-charges-item-price'>₹ " & Searfare & "</span></p>"
                strFare = strFare & "</li>"
            End If


            'strFare = strFare & "<div class='table_small'>"
            'strFare = strFare & "<div class='table_cell' id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #004b91;font-weight:bold;'>Total Fare</div>"
            'strFare = strFare & "<div class='table_cell' id='divTotalFare" & Ft & "'>₹ " & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</div>"
            'strFare = strFare & "</div>"


            strFare = strFare & "<li class='theme-sidebar-section-charges-item'>"
            strFare = strFare & "<h5 class='theme-sidebar-section-charges-item-title' id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #004b91;font-weight:bold;'>Total Fare</h5>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-subtitle'></p>"
            strFare = strFare & "<p class='theme-sidebar-section-charges-item-price' id='divTotalFare" & Ft & "'>₹ " & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</span></p>"
            strFare = strFare & "</li>"

            'strFare = strFare & "<div class='table_small'>"
            'strFare = strFare & "<div class='table_cell' id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #004b91;font-weight:bold;'>Total Fare</div>"
            'strFare = strFare & "<div class='table_cell' id='divTotalFare" & Ft & "'>₹ " & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</div>"
            'strFare = strFare & "</div>"
            '' strFare = strFare & "<div class='large-6 medium-6 small-6 columns' id='trnetfare" & Ft & "' style='display:none'>Net Fare<div class='large-6 medium-6 small-6 columns'>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & "</div></div>"
            'strFare = strFare & "<div class='table_small'>"
            'strFare = strFare & "<div class='table_cell' id='trnetfare" & Ft & "' style='display:none'><div class='table_cell'> Net Fare </div><div class='table_cell' id='lblNetFare" & Ft & "'>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & "</div> <input type='hidden' name='hdnNetFare" & Ft & "' id='hdnNetFare" & Ft & "' value=" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & " ></div></div>"
            'strFare = strFare & "</div>"
            strFare = strFare & "</ul>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"


            'strFare = strFare & "<tr>"
            'strFare = strFare & "<td>INFANT</td>"

            'strFare = strFare & "</tr>"

            'strFare = strFare & "<tr><td colspan='3' rowspan='7'></td><td>Srv.Tax</td><td>" & OFareDS.Tables(0).Rows(0)("SrvTax") & "</td></tr>"
            'If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
            '    strFare = strFare & "<tr><td>Mgnt. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</td></tr>"
            'Else
            '    strFare = strFare & "<tr><td>Tran. Fee</td><td>" & OFareDS.Tables(0).Rows(0)("TFee") & "</td></tr>"
            '    strFare = strFare & "<tr><td>Tran. Chg</td><td>" & TC & "</td></tr>"
            'End If

            'If (VC = "SG" Or VC = "6E") And InStr(VCSPL, "Special") = 0 Then
            '    strFare = strFare & "<tr><td>Meal Chg.</td><td>" & MealPr & "</td></tr>"
            '    strFare = strFare & "<tr><td>Bagg Chg.</td><td>" & BgPr & "</td></tr>"
            'End If

            'strFare = strFare & "<tr><td id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & Ft & "'); onmouseout=funcnetfare('none','trnetfare" & Ft & "'); style='cursor:pointer;color: #006600'>Total Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("totFare")), 0) & "</td></tr>"
            'strFare = strFare & "<tr id='trnetfare" & Ft & "' style='display:none'><td>Net Fare</td><td>" & Math.Round(Convert.ToDecimal(OFareDS.Tables(0).Rows(0)("netFare")), 0) & "</td></tr>"
            strFare = strFare & "</div>"
        Catch ex As Exception
            'clsErrorLog.LogInfo(ex)
        End Try


        Return strFare
    End Function
    'Private Function showPaxDetails(ByVal OrderId As String, ByVal FT As String) As String
    '    Dim my_table As String = ""
    '    Try

    '        dtpax = ObjIntDetails.SelectPaxDetail(OrderId, TransTD) ' Changes in SelectPaxDetail Added DOB
    '        my_table = "<div class='brdr'><div class='f16 bgf1 bld lh30' style='color:#fff'>&nbsp; Traveller Information</div>"
    '        my_table += "<div class='clear1'></div>"
    '        my_table += "<table class='TTBBL w98 mauto'>"
    '        my_table += "<tr>"
    '        my_table += "<th class='lft mright10'>Passenger Name</th>"
    '        my_table += "<th class='lft w25'>Type</th>"
    '        my_table += "<th class='lft'>DOB</th>"
    '        my_table += "</tr>"
    '        For Each dr As DataRow In dtpax.Rows
    '            my_table += "<tr>"
    '            my_table += "<td class='lft w22'>" & dr("Name").ToString() & "</td>"
    '            my_table += "<td class='lft w30'>" & dr("PaxType").ToString() & "</td>"
    '            my_table += "<td class='lft'>" & dr("DOB").ToString() & "</td>"
    '            my_table += "</tr>"
    '        Next
    '        my_table += "</table></div>"
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '        my_table += "</table></div>"
    '    End Try

    '    Return my_table
    'End Function
    Private Function showPaxDetails(ByVal OrderId As String, ByVal FT As String) As String
        Dim my_table As String = ""
        Try

            dtpax = ObjIntDetails.SelectPaxDetail(OrderId, TransTD) ' Changes in SelectPaxDetail Added DOB
            my_table += "<div Class='datatable-container'>"
            my_table += "<table Class='datatable'>"
            my_table += "<thead class='pass-head hidden'>"
            my_table += "<tr>"

            my_table += "<th></th>"
            my_table += "<th>Passenger Name</th>"

            my_table += "<th>Date of Birth</th>"

            my_table += "</tr>"
            my_table += "</thead>"
            my_table += "<tbody>"
            For Each dr As DataRow In dtpax.Rows





                my_table += "<tr>"



                If dr("PaxType").ToString() = "ADT" Then
                    If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                        my_table += "<td style='width: 5%;'><i class='icofont-businesswoman icofont-2x'></i></td>"
                    Else
                        my_table += "<td style='width: 5%;'><i class='icofont-business-man-alt-1 icofont-2x'></i></td>"
                    End If

                ElseIf dr("PaxType").ToString() = "CHD" Then
                    If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                        my_table += "<td style='width: 5%;'><i class='icofont-girl-alt icofont-2x'></i></td>"
                    Else
                        my_table += "<td style='width: 5%;'><i class='icofont-boy icofont-2x'></i></td>"
                    End If

                Else
                    If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                        my_table += "<td style='width: 5%;'><i class='icofont-baby icofont-2x'></i></td>"
                    Else
                        my_table += "<td style='width: 5%;'><i class='icofont-baby icofont-2x'></i></td>"
                    End If

                End If
                my_table += "<td style='width: 79%;'>" & dr("Name").ToString() & "</td>"
                If dr("DOB").ToString() <> "" And dr("DOB").ToString() <> "DOB" Then
                    'Dim startDate As DateTime = DateTime.Parse(dr("DOB").ToString())
                    'Dim strDate As String = startDate.ToString("dd MMM yyyy")

                    my_table += "<td> " & dr("DOB").ToString() & "</td>"
                Else
                    my_table += "<td>NA</td>"
                End If


                my_table += "</tr>"




            Next


            my_table += "</tbody>"
            my_table += "</table>"

            my_table += "</div>"



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

        Return my_table
    End Function
    'Private Function Meal_BagDetails(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal TT As String, ByRef FinalTotal As Double, ByVal HD As String) As String
    '    Dim my_table As String = ""
    '    Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, VC, TT)
    '    Dim DtPxMB As DataTable = dtfare1.Tables(0)
    '    Try

    '        If DtPxMB.Rows.Count > 0 Then
    '            my_table += "<tr>"
    '            my_table += "<td style='border: thin solid #999999'>"
    '            my_table += "<table width='100%' border='0' cellspacing='10' cellpadding='0'>"
    '            my_table += "<tr>"
    '            my_table += "<td colspan='3'  style='background-color: #135f06; color:#004b91; font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; height: 25px;padding-left:10px;'>Meals Bagagae Fare Information" + HD + "</td>"
    '            my_table += "</tr>"
    '            my_table += "<tr >"
    '            my_table += "<td>Passenger Name</td>"
    '            my_table += "<td>Type</td>"
    '            my_table += "<td>MEAL_DETAIL</td>"
    '            my_table += "<td>MEAL_PRICE</td>"
    '            my_table += "<td>BAG_DETAIL</td>"
    '            my_table += "<td>BAG_PRICE</td>"
    '            my_table += "<td>TOTAL</td>"
    '            my_table += "<td>Ticket No</td>"
    '            my_table += "</tr>"
    '            'If TransTD = "" OrElse TransTD Is Nothing Then
    '            For Each dr As DataRow In DtPxMB.Rows
    '                my_table += "<tr>"
    '                my_table += "<td>" & dr("Name").ToString() & "</td>"
    '                my_table += "<td>" & dr("PaxType").ToString() & "</td>"
    '                my_table += "<td>" & dr("MDisc").ToString() & "</td>"
    '                my_table += "<td>" & dr("MPRICE").ToString() & "</td>"
    '                my_table += "<td>" & dr("BDisc").ToString() & "</td>"
    '                my_table += "<td>" & dr("BPRICE").ToString() & "</td>"
    '                my_table += "<td>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</td>"
    '                my_table += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'>" & dr("TicketNumber").ToString() & "</td>"
    '                my_table += "</tr>"
    '                FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
    '            Next
    '        End If
    '        my_table += "<tr style='background-color:#004b91;'>"
    '        my_table += "<td   align='right'></td>"
    '        my_table += "<td   colspan='7' align='right' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #FFFFFF; height: 20px' >GRAND TOTAL </td>"
    '        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; color: #FFFFFF; height: 20px; ' id='td_grandtot'    >" & FinalTotal & "</td>"
    '        my_table += "<td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 12px; color: #000000; height: 20px'></td>"
    '        my_table += "</tr>"
    '        my_table += "</table>"
    '        my_table += "</td>"
    '        my_table += "</tr>"
    '    Catch ex As Exception

    '    End Try
    '    Return my_table

    'End Function
    Private Function Meal_BagDetails(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal TT As String, ByRef FinalTotal As Double, ByVal HD As String) As String
        Dim my_table As String = ""
        Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, VC, TT)
        Dim DtPxMB As DataTable = dtfare1.Tables(0)
        Try

            If DtPxMB.Rows.Count > 0 Then

                my_table += "<div class='row'>"

                my_table += "<div class='large-12 medium-12 small-12 bld'>Meals Bagagae Fare Information" + HD + "</div>"

                my_table += "<div class='large-12 medium-12 small-12'>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>Passenger Name</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>Type</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>MEAL_DETAIL</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>MEAL_PRICE</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>BAG_DETAIL</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>BAG_PRICE</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>TOTAL</div>"
                my_table += "<div class='large-1 medium-3 small-3 columns bld'>Ticket No</div>"
                my_table += "</div>"
                'If TransTD = "" OrElse TransTD Is Nothing Then
                For Each dr As DataRow In DtPxMB.Rows
                    my_table += "<div class='large-12 medium-12 small-12'>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("Name").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("PaxType").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("MDisc").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("MPRICE").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("BDisc").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("BPRICE").ToString() & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString())) & "</div>"
                    my_table += "<div class='large-1 medium-3 small-3 columns'>" & dr("TicketNumber").ToString() & "</div>"
                    my_table += "</div>"
                    FinalTotal += Convert.ToDouble(Convert.ToDouble(dr("MPRICE").ToString()) + Convert.ToDouble(dr("BPRICE").ToString()))
                Next
            End If
            my_table += "<div class='large-12 medium-12 small-12'>"

            my_table += "<div class='large-1 medium-3 small-3 large-push-10 medium-push-6 small-push-6 columns bld'>GRAND TOTAL </div>"
            my_table += "<div class='large-1 medium-3 small-3 columns bld' id='td_grandtot'>" & FinalTotal & "</td>"
            my_table += "<div class='large-1 medium-3 small-3 columns bld'></div>"
            my_table += "</div>"
            my_table += "</div>"

        Catch ex As Exception

        End Try
        Return my_table

    End Function
    'Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
    '    Try
    '        Dim AgencyDs As DataSet
    '        Dim OBFltDs, IBFltDs As DataSet
    '        Dim totFare As Double = 0
    '        Dim netFare As Double = 0

    '        Dim PgMsg As String = ""
    '        OBFltDs = objDA.GetFltDtls(ViewState("OBTrackId"), Session("UID"))
    '        If ViewState("FT") = "InBound" Then
    '            IBFltDs = objDA.GetFltDtls(ViewState("IBTrackId"), Session("UID"))
    '        End If
    '        AgencyDs = objDA.GetAgencyDetails(Session("UID"))
    '        If AgencyDs.Tables.Count > 0 And OBFltDs.Tables.Count > 0 Then
    '            If AgencyDs.Tables(0).Rows.Count > 0 And OBFltDs.Tables(0).Rows.Count > 0 Then
    '                If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
    '                    totFare = OBFltDs.Tables(0).Rows(0)("totFare")
    '                    netFare = OBFltDs.Tables(0).Rows(0)("netFare")
    '                    If ViewState("FT") = "InBound" Then
    '                        totFare = totFare + IBFltDs.Tables(0).Rows(0)("totFare")
    '                        netFare = netFare + IBFltDs.Tables(0).Rows(0)("netFare")
    '                    End If
    '                    Dim agentBal As String = ""
    '                    If rblPaymentMode.SelectedValue = "WL" Then

    '                        '' agentBal = objUMSvc.GetAgencyBal()
    '                        ' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then


    '                        'If Convert.ToDouble(agentBal) > netFare Then
    '                        If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
    '                            If ViewState("FT") = "InBound" Then
    '                                ''Dim um As String = ""
    '                                ''um = objUMSvc.GetMUForPage("wait.aspx")
    '                                ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
    '                                Response.Redirect("../wait.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
    '                            Else
    '                                ''Dim um As String = ""
    '                                ''um = objUMSvc.GetMUForPage("wait.aspx")
    '                                ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
    '                                Response.Redirect("../wait.aspx?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
    '                            End If
    '                        Else
    '                            ' ''Dim um As String = ""
    '                            ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
    '                            ' ''Response.Redirect(um & "?msg=CL", False)
    '                            Response.Redirect("../International/BookingMsg.aspx?msg=CL", False)
    '                        End If

    '                    Else
    '                        ''Dim objPg As New PaymentGateway()
    '                        Dim ipAddress As String
    '                        ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
    '                        If ipAddress = "" Or ipAddress Is Nothing Then
    '                            ipAddress = Request.ServerVariables("REMOTE_ADDR")
    '                        End If
    '                        Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
    '                        Dim Tid As String = ReferenceNo.Substring(4, 16)
    '                        'PgMsg = objPg.PaymentGatewayReq(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM")
    '                        ''Use for Payment Option

    '                        If rblPaymentMode.SelectedValue = "Paytm" Then
    '                            PgMsg = objPt.PaymentGatewayReqPaytm(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM", rblPaymentMode.SelectedValue)
    '                        Else

    '                            PgMsg = objPg.PaymentGatewayReqPayU(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM", rblPaymentMode.SelectedValue)
    '                        End If

    '                        ''        PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType,string IP)
    '                        If PgMsg.Split("~")(0) = "yes" Then
    '                            '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
    '                            If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
    '                                Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
    '                            Else
    '                                Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '                            End If
    '                        Else
    '                            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '                        End If


    '                        ''redirect payment Gateway Url
    '                    End If



    '                Else
    '                    ' ''Dim um As String = ""
    '                    ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
    '                    ' ''Response.Redirect(um & "?msg=NA", False)
    '                    Response.Redirect("../International/BookingMsg.aspx?msg=NA", False)
    '                End If
    '            Else
    '                ' ''Dim um As String = ""
    '                ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
    '                ' ''Response.Redirect(um & "?msg=2", False)
    '                Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '            End If
    '        Else
    '            ''Dim um As String = ""
    '            ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
    '            ''Response.Redirect(um & "?msg=2", False)
    '            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '        End If
    '    Catch ex As Exception
    '        ''Dim um As String = ""
    '        ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
    '        ''Response.Redirect(um & "?msg=2", False)
    '        '' Response.Write(ex.Message & ex.StackTrace.ToString())
    '        Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
    '    End Try
    'End Sub


    Protected Sub ButtonHold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonHold.Click
        Try
            Dim AgencyDs As DataSet
            Dim OBFltDs, IBFltDs As DataSet
            Dim totFare As Double = 0
            Dim netFare As Double = 0

            Dim PgMsg As String = ""
            OBFltDs = objDA.GetFltDtls(ViewState("OBTrackId"), Session("UID"))
            If ViewState("FT") = "InBound" Then
                IBFltDs = objDA.GetFltDtls(ViewState("IBTrackId"), Session("UID"))
            End If
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            If AgencyDs.Tables.Count > 0 And OBFltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And OBFltDs.Tables(0).Rows.Count > 0 Then
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        totFare = OBFltDs.Tables(0).Rows(0)("totFare")
                        netFare = OBFltDs.Tables(0).Rows(0)("netFare")
                        If ViewState("FT") = "InBound" Then
                            totFare = totFare + IBFltDs.Tables(0).Rows(0)("totFare")
                            netFare = netFare + IBFltDs.Tables(0).Rows(0)("netFare")
                        End If
                        Dim agentBal As String = ""
                        If rblPaymentMode.SelectedValue = "WL" Then

                            '' agentBal = objUMSvc.GetAgencyBal()
                            ' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then


                            'If Convert.ToDouble(agentBal) > netFare Then
                            If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > (Convert.ToDecimal(ViewState("holdBookingChargeO")) + Convert.ToDecimal(ViewState("holdBookingChargeR"))) Then



                                If ViewState("FT") = "InBound" Then

                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), ViewState("IBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeR")))
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                Else
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), "", False, 0)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                End If
                            Else
                                ' ''Dim um As String = ""
                                ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                ' ''Response.Redirect(um & "?msg=CL", False)
                                Response.Redirect("../International/BookingMsg.aspx?msg=CL", False)
                            End If

                        Else
                            ''Dim objPg As New PaymentGateway()
                            Dim ipAddress As String
                            ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                            If ipAddress = "" Or ipAddress Is Nothing Then
                                ipAddress = Request.ServerVariables("REMOTE_ADDR")
                            End If
                            Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                            Dim Tid As String = ReferenceNo.Substring(4, 16)
                            'PgMsg = objPg.PaymentGatewayReq(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM")

                            If Convert.ToDecimal(ViewState("holdBookingChargeO")) + Convert.ToDecimal(ViewState("holdBookingChargeR")) > 0 Then

                                Dim holdChrge As Decimal = Convert.ToDecimal(ViewState("holdBookingChargeO")) + Convert.ToDecimal(ViewState("holdBookingChargeR"))

                                ''Use for Payment Option


                                If ViewState("FT") = "InBound" Then

                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), ViewState("IBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeR")))

                                Else

                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), "", False, 0)

                                End If



                                PgMsg = objPg.PaymentGatewayReqPayU(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), holdChrge, holdChrge, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight_Hold", ipAddress, "DOM", rblPaymentMode.SelectedValue)
                                ''                PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType,string IP)
                                If PgMsg.Split("~")(0) = "yes" Then
                                    '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
                                        Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
                                    Else
                                        Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                                    End If
                                Else
                                    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                                End If

                            Else



                                If ViewState("FT") = "InBound" Then

                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, 0, ViewState("IBTrackId"), True, 0)
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                Else
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, 0, "", False, 0)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                End If

                            End If

                            ''redirect payment Gateway Url
                        End If



                    Else
                        ' ''Dim um As String = ""
                        ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        ' ''Response.Redirect(um & "?msg=NA", False)
                        Response.Redirect("../International/BookingMsg.aspx?msg=NA", False)
                    End If
                Else
                    ' ''Dim um As String = ""
                    ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                    ' ''Response.Redirect(um & "?msg=2", False)
                    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                End If
            Else
                ''Dim um As String = ""
                ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                ''Response.Redirect(um & "?msg=2", False)
                Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
            End If
        Catch ex As Exception
            ''Dim um As String = ""
            ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
            ''Response.Redirect(um & "?msg=2", False)
            '' Response.Write(ex.Message & ex.StackTrace.ToString())
            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
        End Try
    End Sub

    Protected Sub LinkEdit_Click(sender As Object, e As EventArgs) Handles LinkEdit.Click

        Try

            If (LinkEdit.Text = "Edit") Then
                Submit.Attributes.Add("disabled", True)
                ButtonHold.Attributes.Add("disabled", True)
                PaxGrd_div.Visible = True
                divPaxdetails.Visible = False
                LinkEdit.Text = "Update"

                Dim OBTrackId_Str1 As String = ""
                Dim IBTrackId_Str1 As String = ""



                If HttpContext.Current.Request.QueryString.Count >= 3 Then
                    OBTrackId_Str1 = HttpContext.Current.Request.QueryString("OBTID").ToString()
                    IBTrackId_Str1 = HttpContext.Current.Request.QueryString("IBTID").ToString()
                    PAXGRD.DataSource = ObjIntDetails.SelectPaxDetailBOTH(OBTrackId_Str1, IBTrackId_Str1, "")
                    PAXGRD.DataBind()


                ElseIf HttpContext.Current.Request.QueryString.Count = 2 Then
                    OBTrackId_Str1 = HttpContext.Current.Request.QueryString("OBTID").ToString()
                    PAXGRD.DataSource = ObjIntDetails.SelectPaxDetail(OBTrackId_Str1, "")
                    PAXGRD.DataBind()

                End If

                Flthear = ObjIntDetails.SelectFltheaderDetls(OBTrackId_Str1)
                If (Flthear.Rows.Count > 0) Then
                    Mob_txt.Text = Flthear.Rows(0)("PgMobile").ToString()
                    Email_txt.Text = Flthear.Rows(0)("PgEmail").ToString()
                End If

            Else
                If (LinkEdit.Text = "Update") Then
                    PaxGrd_div.Visible = False
                    divPaxdetails.Visible = True
                    LinkEdit.Text = "Edit"

                    Dim OBTrackId_Str As String = ""
                    Dim IBTrackId_Str As String = ""

                    If HttpContext.Current.Request.QueryString.Count >= 3 Then
                        OBTrackId_Str = HttpContext.Current.Request.QueryString("OBTID").ToString()
                        IBTrackId_Str = HttpContext.Current.Request.QueryString("IBTID").ToString()

                    ElseIf HttpContext.Current.Request.QueryString.Count = 2 Then
                        OBTrackId_Str = HttpContext.Current.Request.QueryString("OBTID").ToString()

                    End If



                    For i As Integer = 0 To PAXGRD.Rows.Count - 1
                        ''Dim sid As String = PAXGRD.Rows(i).Cells(1).Text
                        Dim row As GridViewRow = PAXGRD.Rows(i)
                        Dim GGDD_DISTYPE As DropDownList = CType(row.FindControl("GGDD_DISTYPE"), DropDownList)
                        Dim lbl_PaxID As String = CType(row.FindControl("lbl_PaxID"), TextBox).Text
                        Dim lbl_FName As String = CType(row.FindControl("lbl_FName"), TextBox).Text
                        Dim lbl_MName As String = CType(row.FindControl("lbl_MName"), TextBox).Text
                        Dim lbl_LName As String = CType(row.FindControl("lbl_LName"), TextBox).Text
                        Dim Gtxt_DOB As String = CType(row.FindControl("Gtxt_DOB"), TextBox).Text

                        InsertData(OBTrackId_Str, IBTrackId_Str, GGDD_DISTYPE.SelectedValue, Convert.ToInt32(lbl_PaxID), lbl_FName, lbl_MName, lbl_LName, Gtxt_DOB)

                    Next

                    divPaxdetails.InnerHtml = ""
                    divPaxdetails.InnerHtml = showPaxDetails(OBTrackId_Str, "")
                    Submit.Attributes.Remove("disabled")
                    ButtonHold.Attributes.Remove("disabled")

                End If
            End If
        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try


    End Sub

    Private Sub InsertData(ByVal OBTrackIdStr As String, ByVal IBTrackId_Str As String, ByVal title As String, ByVal PaxID As Integer, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal DOB As String)

        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand("SP_UPDATEPAXDETAILSONLINE", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@OBTrackIdStr", OBTrackIdStr)
            cmd.Parameters.AddWithValue("@IBTrackId_Str", IBTrackId_Str)
            cmd.Parameters.AddWithValue("@PaxID", PaxID)
            cmd.Parameters.AddWithValue("@FName", FName)
            cmd.Parameters.AddWithValue("@MName", MName)
            cmd.Parameters.AddWithValue("@LName", LName)
            cmd.Parameters.AddWithValue("@DOB", DOB)
            cmd.Parameters.AddWithValue("@Title", title)
            cmd.Parameters.AddWithValue("@Email", Email_txt.Text.Trim)
            cmd.Parameters.AddWithValue("@Mobile", Mob_txt.Text.Trim)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim GGDD_DISTYPE As DropDownList = CType(e.Row.FindControl("GGDD_DISTYPE"), DropDownList)
            GGDD_DISTYPE.DataBind()
            GGDD_DISTYPE.Items.FindByValue((TryCast(e.Row.FindControl("lbl_Tittle"), Label)).Text).Selected = True
        End If
    End Sub

    'Protected Sub gvDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    If (e.Row.RowType = DataControlRowType.DataRow) Then

    '        'Find the DropDownList in the Row.
    '        Dim ddlCountries As DropDownList = CType(e.Row.FindControl("ddlCountries"), DropDownList)
    '        ddlCountries.DataSource = GetData("SELECT DISTINCT Country FROM Customers")
    '        ddlCountries.DataTextField = "Country"
    '        ddlCountries.DataValueField = "Country"
    '        ddlCountries.DataBind()

    '        'Add Default Item in the DropDownList.
    '        ddlCountries.Items.Insert(0, New ListItem("Please select"))

    '        'Select the Country of Customer in DropDownList.
    '        Dim country As String = CType(e.Row.FindControl("lblCountry"), Label).Text
    '        ddlCountries.Items.FindByValue(country).Selected = True
    '    End If
    'End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function Submit_Button_Click() As String
        Dim krishna As New FlightDom_PriceDetails
        Return krishna.CallSubmit_Click
    End Function

    Public Function CallSubmit_Click() As String
        Dim rutStr As String = ""
        Try
            Dim AgencyDs As DataSet
            Dim OBFltDs, IBFltDs As DataSet
            Dim totFare As Double = 0
            Dim netFare As Double = 0
            Dim PaymentMode As String = "WL" 'rblPaymentMode.SelectedValue

            Dim OBTrackId As String = ""
            Dim IBTrackId As String = ""
            Dim FT As String = ""

            If Session("OBTrackId") IsNot Nothing Then
                OBTrackId = Session("OBTrackId").ToString()
            End If

            If Session("IBTrackId") IsNot Nothing Then
                IBTrackId = Session("IBTrackId").ToString()
            End If

            If Session("FT") IsNot Nothing Then
                FT = Session("FT").ToString()
            End If

            Dim PgMsg As String = ""
            OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID"))
            If FT = "InBound" Then
                IBFltDs = objDA.GetFltDtls(IBTrackId, Session("UID"))
            End If
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            If AgencyDs.Tables.Count > 0 And OBFltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And OBFltDs.Tables(0).Rows.Count > 0 Then
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        totFare = OBFltDs.Tables(0).Rows(0)("totFare")
                        netFare = OBFltDs.Tables(0).Rows(0)("netFare")
                        If FT = "InBound" Then
                            totFare = totFare + IBFltDs.Tables(0).Rows(0)("totFare")
                            netFare = netFare + IBFltDs.Tables(0).Rows(0)("netFare")
                        End If
                        Dim agentBal As String = ""
                        If PaymentMode = "WL" Then

                            '' agentBal = objUMSvc.GetAgencyBal()
                            ' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then


                            'If Convert.ToDouble(agentBal) > netFare Then
                            If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                                If FT = "InBound" Then
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    rutStr = "/wait.aspx?OBTID=" & OBTrackId & "&IBTID=" & IBTrackId & "&FT=" & FT & ""
                                Else
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    rutStr = "/wait.aspx?OBTID=" & OBTrackId & "&FT=" & FT & ""
                                End If
                            Else
                                ' ''Dim um As String = ""
                                ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                ' ''Response.Redirect(um & "?msg=CL", False)
                                rutStr = "/International/BookingMsg.aspx?msg=CL"
                            End If

                        Else
                            ''Dim objPg As New PaymentGateway()
                            Dim ipAddress As String
                            ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                            If ipAddress = "" Or ipAddress Is Nothing Then
                                ipAddress = Request.ServerVariables("REMOTE_ADDR")
                            End If
                            Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                            Dim Tid As String = ReferenceNo.Substring(4, 16)
                            'PgMsg = objPg.PaymentGatewayReq(ViewState("OBTrackId"), Tid, ViewState("IBTrackId"), Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM")
                            ''Use for Payment Option

                            If PaymentMode = "Paytm" Then
                                PgMsg = objPt.PaymentGatewayReqPaytm(OBTrackId, Tid, IBTrackId, Session("UID"),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")),
                                                                     netFare, netFare,
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")),
                                                                     Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM", rblPaymentMode.SelectedValue)
                            Else

                                PgMsg = objPg.PaymentGatewayReqPayU(OBTrackId, Tid, IBTrackId, Session("UID"),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")),
                                                                    netFare, netFare,
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")),
                                                                    Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "DOM", rblPaymentMode.SelectedValue)
                            End If

                            ''        PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType,string IP)
                            If PgMsg.Split("~")(0) = "yes" Then
                                '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
                                    Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
                                Else
                                    rutStr = "/International/BookingMsg.aspx?msg=2"
                                End If
                            Else
                                rutStr = "/International/BookingMsg.aspx?msg=2"
                            End If


                            ''redirect payment Gateway Url
                        End If



                    Else
                        ' ''Dim um As String = ""
                        ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        ' ''Response.Redirect(um & "?msg=NA", False)
                        rutStr = "/International/BookingMsg.aspx?msg=NA"
                    End If
                Else
                    ' ''Dim um As String = ""
                    ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                    ' ''Response.Redirect(um & "?msg=2", False)
                    rutStr = "/International/BookingMsg.aspx?msg=2"
                End If
            Else
                ''Dim um As String = ""
                ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                ''Response.Redirect(um & "?msg=2", False)
                rutStr = "/International/BookingMsg.aspx?msg=2"
            End If
        Catch ex As Exception
            ''Dim um As String = ""
            ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
            ''Response.Redirect(um & "?msg=2", False)
            '' Response.Write(ex.Message & ex.StackTrace.ToString())
            rutStr = "/International/BookingMsg.aspx?msg=2"
        End Try
        Return rutStr
    End Function
End Class
