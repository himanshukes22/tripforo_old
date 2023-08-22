Imports System.Data
Imports PG
Imports System.Web.Services
Imports System.Data.SqlClient
Imports PaytmWall
Imports System.Collections.Generic
Imports STD.BAL

Partial Class FlightIntl_PriceDetails
    Inherits System.Web.UI.Page
    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim objFareBreakup As New clsCalcCommAndPlb
    Dim objDA As New SqlTransaction

    Dim DomAirDt As DataTable
    Dim trackId As String, LIN As String
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
    Dim IntAirDt As DataTable
    Private ObjIntDetails As New IntlDetails()
    Dim objSql As New SqlTransactionNew
    Dim TransTD As String = ""
    Dim objBal As New COMN_BAL.Flight.FlighBal(Variables.Constr)
    Dim objDsO As New DataSet()
    Dim objDsR As New DataSet()
    Dim dtpnr As New DataTable() 'Header
    Dim dtpax As New DataTable() 'Pax
    Dim dtfare As New DataTable()
    Dim OBFltDs, IBFltDs As DataSet
    Dim SSR_LOG As DataSet
    Dim objUMSvc As New FltSearch1()
    Dim objPg As New PG.PaymentGateway()
    Dim objPt As New PaytmWall.PaytmTrans()
    Dim holdBookingChargeO As Decimal = 0
    Dim holdBookingChargeR As Decimal = 0
    Dim SeatListO As List(Of STD.Shared.Seat)
    Dim SeatListR As List(Of STD.Shared.Seat)
    Public Shared UserID As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            SeatListO = New List(Of STD.Shared.Seat)
            Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            Else
                UserID = Session("UID")
                If Not Page.IsPostBack Then


                    If HttpContext.Current.Request.QueryString.Count = 1 Then
                        trackId = HttpContext.Current.Request.QueryString(0).ToString()
                        OBFltDs = objDA.GetFltDtls(trackId, Session("UID"))
                        SeatListO = IFLT.SeatDetails(trackId)
                        IntAirDt = OBFltDs.Tables(0)
                        ViewState("trackid") = trackId
                    End If

                    If IntAirDt.Rows(IntAirDt.Rows.Count - 1)("TripType") = "R" Then
                        FT = "R"
                    Else
                        FT = "O"
                    End If
                    HdnTripType.Value = "OutBound"
                    ViewState("FT") = HdnTripType.Value
                    Session("IntAirDt") = IntAirDt
                    Adult = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                    Child = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                    Infant = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))


                    Dim seatdetails As String = ""
                    Dim seatFareO As Integer = 0
                    Dim seatFareR As Integer = 0

                    Dim dt As DataTable = New DataTable()

                    If SeatListO.Count > 0 Then
                        seatdetails &= "<div class='row'>"
                        seatdetails &= "<div class='large-12 medium-12 small-12  headbgs'><i class='fa fa-wheelchair' aria-hidden='true'></i> Traveller Seat Information</div>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #000!important; font-weight: bold !important; background: #e2e2e2; margin-top: 1px; font-size: 14px;'>Traveller(OutBound)</div>"
                        seatdetails &= "<div class='col-md-3 ' style='color: #000!important; font-weight: bold !important; background: #e2e2e2; margin-top: 1px; font-size: 14px;'>Sector</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #000!important; font-weight: bold !important; background: #e2e2e2; margin-top: 1px; font-size: 14px;'>Seat</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #000!important; font-weight: bold !important; background: #e2e2e2; margin-top: 1px; font-size: 14px;'>Type</div>"
                        seatdetails &= "<div class='col-md-2 ' style='color: #000!important; font-weight: bold !important; background: #e2e2e2; margin-top: 1px; font-size: 14px;'>Amount</div>"
                        seatdetails &= "<div class='col-md-12'>&nbsp</div>"
                        For i As Integer = 0 To SeatListO.Count - 1
                            dt = ObjIntDetails.SelectPaxDetail(ViewState("trackid"), SeatListO(i).PaxId)
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

                    SeatInformation.InnerHtml = seatdetails


                    ' Now Show All Details in Divs
                    ' Flight Details Both in 1 Div
                    Try
                        'SelectedFltArray = IntAirDt.Select("TripType='O'", "")
                        'strFlt = "<div class='w45 lft padding2s'><div class='f18'>OutBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div>"
                        'For i As Integer = 0 To SelectedFltArray.Length - 1
                        '    strFlt = strFlt & "<div>"
                        '    strFlt = strFlt & "<div class='w24 padding1 lft'><div class='f16'>" & (SelectedFltArray(i)("DepartureLocation")) & " - " & (SelectedFltArray(i)("ArrivalLocation")) & "</div><div>" & (SelectedFltArray(i)("Departure_Date")) & "</div></div>"
                        '    strFlt = strFlt & "<div class='w40 padding1 lft'><div class='f16'>" & (SelectedFltArray(i)("MarketingCarrier")) & "-" & (SelectedFltArray(i)("FlightIdentification")) & "</div><div>Class: " & (SelectedFltArray(i)("RBD")) & "</div></div>"
                        '    strFlt = strFlt & "<div class='w28 padding1 lft'><div><span class='lft w30'>Dep:</span>" & (SelectedFltArray(i)("DepartureTime")) & "Hrs.</div><div><span class='lft w30'>Arr:</span> " & (SelectedFltArray(i)("ArrivalTime")) & " Hrs</div>"
                        '    strFlt = strFlt & "</div>"
                        'Next
                        'strFlt = strFlt & "</div>"
                        'If FT = "R" Then
                        '    SelectedFltArray = IntAirDt.Select("TripType='R'", "")
                        '    strFlt = strFlt & "<div class='padding2s w45 rgt'><div class='f18'>InBound</div><div class='clear'></div><div class='hr'></div><div class='clear'></div>"
                        '    For i As Integer = 0 To SelectedFltArray.Length - 1
                        '        strFlt = strFlt & "<div>"
                        '        strFlt = strFlt & "<div class='w24 padding1 lft'><div class='f16'>" & (SelectedFltArray(i)("DepartureLocation")) & " - " & (SelectedFltArray(i)("ArrivalLocation")) & "</div><div>" & (SelectedFltArray(i)("Departure_Date")) & "</div></div>"
                        '        strFlt = strFlt & "<div class='w40 padding1 lft'><div class='f16'>" & (SelectedFltArray(i)("MarketingCarrier")) & "-" & (SelectedFltArray(i)("FlightIdentification")) & "</div><div>Class: " & (SelectedFltArray(i)("RBD")) & "</div></div>"
                        '        strFlt = strFlt & "<div class='w28 padding1 lft'><div><span class='lft w30'>Dep:</span>" & (SelectedFltArray(i)("DepartureTime")) & "Hrs</div><div><span class='lft w30'>Arr:</span> " & (SelectedFltArray(i)("ArrivalTime")) & "Hrs</div></div>"
                        '        strFlt = strFlt & "</div>"
                        '    Next
                        '    strFlt = strFlt & "</div>"
                        'End If
                        'divFltDtls.InnerHtml = strFlt
                        divFltDtls.InnerHtml = "<div class='bor'><div class='' style='padding-left:0px'>" & STDom.CustFltDetails_Intl(OBFltDs) & "</div></div>" 'STDom.CustFltDetails_Intl(OBFltDs)
                    Catch ex As Exception

                    End Try

                    divFareDtls.InnerHtml = "<div class='large-12 medium-12 small-12  headbgs fd-h' style='line-height: 31px;'>&nbsp; Fare Information</div><div class='clear'></div><div class='hr'></div><div class='clear'></div><div>" & fareBreakupfun2(IntAirDt, Adult, Child, Infant, seatFareO) & "</div><div class='clear1'></div>" '"<div class='clear1'></div><div class='padding1 w100 lft'><div class='bld f13'></div><div>" & fareBreakupfun2(IntAirDt, Adult, Child, Infant) & "</div></div><div class='clear1'></div>"
                    divPaxdetails.InnerHtml = showPaxDetails(trackId, FT)


                    VCOB = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    TripOB = OBFltDs.Tables(0).Rows(0)("Trip")
                    'FLT_STAT = OBFltDs.Tables(0).Rows(0)("FlightStatus")
                    objDsO = objBal.GetFltHoldBookingCharge(Session("agent_type"), Session("UID"), VCOB, TripOB)

                    If objDsO.Tables(0).Rows.Count > 0 Then

                        ViewState("IsHoldVisibleO") = Convert.ToBoolean(objDsO.Tables(0).Rows(0)("HoldBooking"))
                        ViewState("holdBookingChargeO") = Convert.ToDecimal(objDsO.Tables(0).Rows(0)("Charges"))
                        '' "Request"


                    End If



                    ''code here for- to visible hold button
                    If Convert.ToBoolean(ViewState("IsHoldVisibleO")) = True Then

                        ButtonHold.Visible = True

                        lblHoldBookingCharge.Text = " Rs. " & (Convert.ToDecimal(ViewState("holdBookingChargeO"))) & "  will be charged to hold the booking."
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

    Private Function fareBreakupfun2(ByVal IntAirDt As DataTable, ByVal Adult As Integer, ByVal Child As Integer, ByVal Inf As Integer, ByVal Searfare As Integer) As String
        Try

            Dim OrderId As String = IntAirDt.Rows(0)("Track_id").ToString()
            Dim VC = IntAirDt.Rows(0)("ValiDatingCarrier")
            Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(OrderId, "")
            Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
            If (MBDT.Tables(0).Rows.Count > 0) Then
                For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                    MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
                    BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                Next
                'IF Header is Updated then Only Allow Booking
                SSR_LOG = objSql.GetSSR_Log_Detail(OrderId)
                If Math.Round((Convert.ToDecimal(SSR_LOG.Tables(0).Rows(0)("Header_TBC")) + MBPR), 2) <= Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("TotFare").ToString()), 2) Then
                    Submit.Visible = True
                Else
                    If Math.Round((Convert.ToDecimal(SSR_LOG.Tables(0).Rows(0)("Header_TBC")) + MBPR), 1) <= Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("TotFare").ToString()), 1) Then
                        Submit.Visible = True
                    Else
                        If Math.Round((Convert.ToDecimal(SSR_LOG.Tables(0).Rows(0)("Header_TBC")) + MBPR), 0) <= Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("TotFare").ToString()), 0) Then
                            Submit.Visible = True
                        Else
                            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                        End If
                    End If

                End If
            End If
            Dim tax(), tax1() As String, yq As Integer = 0, tx As Integer = 0
            tax = IntAirDt.Rows(0)("Adt_Tax").ToString.Split("#")
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                Else
                    tax1 = tax(i).Split(":")
                    tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                End If
            Next

            Dim tcperpax As Integer = Convert.ToInt32(IntAirDt.Rows(0)("TC")) / (Convert.ToInt32(IntAirDt.Rows(0)("Adult")) + Convert.ToInt32(IntAirDt.Rows(0)("Child")))
            'Changed ID 02/04/2014
            strFare = strFare & "<div id='OB_FT' border='0'>"



            'strFare = strFare & "<div class='large-9 medium-9 small-12 columns'>"

            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns bld'>Pax Type</div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns bld'>Base Fare</div>"
            'strFare = strFare & "<div class='large-2 medium-3 small-3 columns bld'>Fuel Charge</div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns bld'>Tax </div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns bld'>Total</div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "<div class='clear'></div>"

            strFare = strFare & "<div class='table'>"
            strFare = strFare & "<div class='theader'>"

            strFare = strFare & "<div class='table_header'>Pax Type</div>"
            strFare = strFare & "<div class='table_header'>Base Fare</div>"
            strFare = strFare & "<div class='table_header'>Fuel Charge</div>"
            strFare = strFare & "<div class='table_header'>Tax </div>"
            strFare = strFare & "<div class='table_header'>Total</div>"
            strFare = strFare & "</div>"



            strFare = strFare & "<div class='table_row'>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Pax Type</div>"
            strFare = strFare & "<div class='table_cell'>ADULT</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Base Fare</div>"
            strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("AdtBFare") & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Fuel Charge</div>"
            strFare = strFare & "<div class='table_cell'>₹ " & yq & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Tax </div>"
            strFare = strFare & "<div class='table_cell'>₹ " & (tx + tcperpax) & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Total</div>"
            strFare = strFare & "<div class='table_cell'>₹ " & (IntAirDt.Rows(0)("AdtFare") + tcperpax) & "&nbsp;<b>x</b>" & Adult & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"


            'strFare = strFare & "<div class='large-9 medium-9 small-12 columns'>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>ADULT</div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & IntAirDt.Rows(0)("AdtBFare") & "</div>"
            'strFare = strFare & "<div class='large-2 medium-3 small-3 columns'>" & yq & "</div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & (tx + tcperpax) & "</div>"
            'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & (IntAirDt.Rows(0)("AdtFare") + tcperpax) & "&nbsp;<b>x</b>" & Adult & "</div>"

            'strFare = strFare & "</div>"
            'strFare = strFare & "<div class='clear'></div>"

            If Child > 0 Then
                Try
                    yq = 0
                    tx = 0
                    'tax = fareHashtbl("ChdTax").ToString.Split("#")
                    tax = IntAirDt.Rows(0)("Chd_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        End If
                    Next
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try

                'strFare = strFare & "<div class='large-9 medium-9 small-12 columns'>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>CHILD</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & IntAirDt.Rows(0)("ChdBFare") & "</div>"
                'strFare = strFare & "<div class='large-2 medium-3 small-3 columns'>" & yq & "</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & tx + tcperpax & "</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & (IntAirDt.Rows(0)("ChdFare") + tcperpax) & "&nbsp;<b>x</b>" & Child & "</div>"
                ''strFare = strFare & "<td><b>x</b>" & Child & "</td>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='clear'></div>"


                strFare = strFare & "<div class='table_row'>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Pax Type</div>"
                strFare = strFare & "<div class='table_cell'>Child</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Base Fare</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("ChdBFare") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Fuel Charge</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & yq & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Tax </div>"
                strFare = strFare & "<div class='table_cell'>₹ " & (tx + tcperpax) & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Total</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & (IntAirDt.Rows(0)("ChdFare") + tcperpax) & "&nbsp;<b>x</b>" & Child & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "</div>"

            End If

            If Infant > 0 Then
                Try
                    yq = 0
                    tx = 0
                    'tax = fareHashtbl("InfTax").ToString.Split("#")
                    tax = IntAirDt.Rows(0)("Inf_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(Math.Round(Convert.ToDecimal(tax1(1).ToString()), 0))
                        End If
                    Next
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try


                'strFare = strFare & "<div class='large-9 medium-9 small-12 columns'>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>INFANT</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & IntAirDt.Rows(0)("InfBFare") & "</div>"
                'strFare = strFare & "<div class='large-2 medium-3 small-3 columns'>" & yq & "</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & tx & "</div>"
                'strFare = strFare & "<div class='large-2 medium-2 small-2 columns'>" & IntAirDt.Rows(0)("InfFare") & "&nbsp;<b>x</b>" & Inf & "</div>"
                ''strFare = strFare & "<td><b>x</b>" & Inf & "</td>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='clear'></div>"


                strFare = strFare & "<div class='table_row'>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Pax Type</div>"
                strFare = strFare & "<div class='table_cell'>Infant</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Base Fare</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("InfBFare") & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Fuel Charge</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & yq & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Tax </div>"
                strFare = strFare & "<div class='table_cell'>₹ " & (tx + tcperpax) & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Total</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("InfFare") & "&nbsp;<b>x</b>" & Infant & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "</div>"

            End If

            strFare = strFare & "</div>"



            'strFare = strFare & "<div class='row'>"
            'strFare = strFare & "<div class='large-3 medium-3 small-12 rgt'>"
            'strFare = strFare & "<div class='large-12 medium-12 small-12 bld blue'>Other Details</div>"
            'strFare = strFare & "<div class='large-12 medium-12 small-12'>"
            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Srv.Tax</div>"
            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>" & IntAirDt.Rows(0)("SrvTax") & "</div>"

            strFare = strFare & "<label style='font-weight: 800;font-size: 14px;position: relative;left: 49px;top: 17px;'>Other Details</label>"
            strFare = strFare & "<div class='table'>"
            strFare = strFare & "<div class='theader'>"
            strFare = strFare & "<div class='table_header'>GST</div>"  'Srv.Tax


            If (IntAirDt.Rows(0)("IsCorp") = True) Then
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Mgnt. Fee</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>" & IntAirDt.Rows(0)("TOTMGTFEE") & "</div>"
                strFare = strFare & "<div class='table_header'>Management Fee</div>"
            Else
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Tran. Fee</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>" & IntAirDt.Rows(0)("TFee") & "</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Tran. Chg</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>0</div>"

                strFare = strFare & "<div class='table_header'>Transaction Fee</div>"
                strFare = strFare & "<div class='table_header'>Transaction Charge</div>"

            End If
            strFare = strFare & "<div class='table_header'>PG Charge</div>"

            If (VC = "SG" Or VC = "6E" Or VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ" Or VC = "G9" Or VC = "IX") Then
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Meal Chg.</div><div class='large-6 medium-6 small-6 columns'>" & MealPr & "</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Bagg Chg.</div><div class='large-6 medium-6 small-6 columns'>" & BgPr & "</div>"

                strFare = strFare & "<div class='table_header'>Meal Charge</div>"
                strFare = strFare & "<div class='table_header'>Baggage Charge</div>"
                'strFare = strFare & "<div class='table_header'>Seat Charge</div>"

            End If
            strFare = strFare & "<div class='table_header'>Gross Total</div>"
            strFare = strFare & "</div>"


            strFare = strFare & "<div class='table_row'>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>GST</div>" 'Srv.Tax
            strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("SrvTax") & "</div>"
            strFare = strFare & "</div>"

            If (IntAirDt.Rows(0)("IsCorp") = True) Then

                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Management Fee</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("TOTMGTFEE") & "</div>"
                strFare = strFare & "</div>"

            Else
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Transaction Fee</div>"
            strFare = strFare & "<div class='table_cell'>₹ " & IntAirDt.Rows(0)("TFee") & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>Transaction Charge</div>"
            strFare = strFare & "<div class='table_cell'>₹ 0</div>"
            strFare = strFare & "</div>"
            End If


            If (VC = "SG" Or VC = "6E" Or VC = "AK" Or VC = "I5" Or VC = "D7" Or VC = "FD" Or VC = "QZ" Or VC = "Z2" Or VC = "XJ" Or VC = "XT" Or VC = "DJ" Or VC = "G9" Or VC = "IX") Then
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Meal Chg.</div><div class='large-6 medium-6 small-6 columns'>" & MealPr & "</div>"
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Bagg Chg.</div><div class='large-6 medium-6 small-6 columns'>" & BgPr & "</div>"

                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Meal Charge</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & MealPr & "</div>"
                strFare = strFare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Baggage Chgarge</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & BgPr & "</div>"
                strFare = strFare & "</div>"

            End If

            If Searfare = 0 Then
            Else
                'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>Seat Chg.</div><div class='large-6 medium-6 small-6 columns'>" & Searfare & "</div>"
                strFare = strFare & "<div class='table_small'>"
                strFare = strFare & "<div class='table_cell'>Seat Chgarge</div>"
                strFare = strFare & "<div class='table_cell'>₹ " & Searfare & "</div>"
                strFare = strFare & "</div>"
            End If
            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns'>PG. Chg. <input type='hidden' name='hdnTotalFareOutBound' id='hdnTotalFareOutBound' value=" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("totFare")), 0) & " > <input type='hidden' name='hdnNetFareOutBound' id='hdnNetFareOutBound' value=" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("netFare")), 0) & " ></div>"
            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns' id='PgChargeOutBound'>0.00</div>"




            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns' id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & FT & "'); onmouseout=funcnetfare('none','trnetfare" & FT & "'); style='cursor:pointer;color: #004b91;font-weight:bold'>Total Fare</div><div class='large-6 medium-6 small-6 columns bld' id='divTotalFareOutBound'>" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("totFare")), 0) & "</div>"
            'strFare = strFare & "<div class='large-6 medium-6 small-6 columns' id='trnetfare" & FT & "' style='display:none'>Net Fare<div class='large-6 medium-6 small-6 columns' id='lblNetFareOutBound'>" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("netFare")), 0) & "</div></div>"
            'strFare = strFare & "</div>"


            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell'>PG. Chg. <input type='hidden' name='hdnTotalFareOutBound' id='hdnTotalFareOutBound' value=" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("totFare")), 0) & " > <input type='hidden' name='hdnNetFareOutBound' id='hdnNetFareOutBound' value=" & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("netFare")), 0) & " ></div>"
            strFare = strFare & "<div class='table_cell'  id='PgChargeOutBound'>₹ 0.00</div>"
            strFare = strFare & "</div>"


            strFare = strFare & "<div class='table_small'>"
            strFare = strFare & "<div class='table_cell' id='trtotfare' onmouseover=funcnetfare('block','trnetfare" & FT & "'); onmouseout=funcnetfare('none','trnetfare" & FT & "'); style='cursor:pointer;color: #004b91;font-weight:bold;'>Total Fare</div>"
            strFare = strFare & "<div class='table_cell' id='trnetfare" & FT & "'>₹ " & Math.Round(Convert.ToDecimal(IntAirDt.Rows(0)("totFare")), 0) & "</div>"
            strFare = strFare & "</div>"

            strFare = strFare & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        Return strFare
    End Function

    Private Function showPaxDetails(ByVal OrderId As String, ByVal FT As String) As String
        Dim my_table As String = ""
        Try
            'dtpnr = ObjIntDetails.SelectHeaderDetail(OrderId) ' PRimary Pax
            'For i As Integer = 0 To dtpnr.Rows.Count - 1
            'Next

            dtpax = ObjIntDetails.SelectPaxDetail(OrderId, TransTD) ' Changes in SelectPaxDetail Added DOB
            'my_table += "<div class='clear1'></div>"
            'my_table += "<div class='row' style='background-color:#fff;'>"

            'my_table = "<div class='large-12 medium-12 small-12  headbgs fd-h' style='line-height: 28px !important;'><i><img alt='user' src='../Images/icons/multi-user-icon.png' style='width: 34px;'></i> Traveller Information</div>"
            'my_table += "<div class='clear'></div>"
            'my_table += "<div class='large-12 medium-12 small-12'>"

            'my_table += "<div class='large-4 medium-4 small-4 columns bld'>Passenger Name</div>"
            'my_table += "<div class='large-4 medium-4 small-4 columns bld'>Type</div>"
            'my_table += "<div class='large-4 medium-4 small-4 columns bld'>DOB</div>"
            'my_table += "</div>"
            'my_table += "<div class='large-12 medium-12 small-12'>"

            my_table += "<div class='row' style='background-color:#fff;' >"
            my_table = "<div class='large-12 medium-12 small-12  headbgs  fd-h' style='line-height: 28px !important;'><i><img alt='user' src='../Images/icons/multi-user-icon.png' style='width: 34px;'></i> Traveller Information</div>"
            my_table += "<div class='clear1'></div>"
            'my_table += "<div class='large-12 medium-12 small-12'>"
            my_table += "<div class='table' style='margin: 0% auto 20px !important;'>"
            my_table += "<div class='theader'>"
            my_table += "<div class='table_header'>Passenger Name</div>"
            my_table += "<div class='table_header'>Type</div>"
            my_table += "<div class='table_header'>DOB</div>"
            my_table += "</div>"

            For Each dr As DataRow In dtpax.Rows
                'my_table += "<div class='large-4 medium-4 small-4 columns'>" & dr("Name").ToString() & "</div>"
                'my_table += "<div class='large-4 medium-4 small-4 columns'>" & dr("PaxType").ToString() & "</div>"
                'my_table += "<div class='large-4 medium-4 small-4 columns'>" & dr("DOB").ToString() & "</div>"
                'my_table += "</tr>"

                my_table += "<div class='table_row'>"
                my_table += "<div class='table_small'>"
                my_table += "<div class='table_cell'>Passenger Name</div>"
                my_table += "<div class='table_cell'>" & dr("Name").ToString() & "</div>"
                my_table += "</div>"
                my_table += "<div class='table_small'>"
                my_table += "<div class='table_cell'>Type</div>"
                my_table += "<div class='table_cell'>" & dr("PaxType").ToString() & "</div>"
                my_table += "</div>"
                my_table += "<div class='table_small'>"
                my_table += "<div class='table_cell'>DOB</div>"
                my_table += "<div class='table_cell'>" & dr("DOB").ToString() & "</div>"
                my_table += "</div>"
                my_table += "</div>"

            Next
            my_table += "</div>"
            'my_table += "<div class='clear1'></div>"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


        Return my_table
    End Function

    Private Function Meal_BagDetails(ByVal OrderId As String, ByVal TransTD As String, ByVal VC As String, ByVal TT As String, ByRef FinalTotal As Double, ByVal HD As String) As String
        Dim my_table As String = ""
        Dim dtfare1 As DataSet = objSql.Get_PAX_MB_Details(OrderId, TransTD, VC, TT)
        Dim DtPxMB As DataTable = dtfare1.Tables(0)

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
        my_table += "<div class='large-1 medium-3 small-3 columns bld' ' id='td_grandtot'    >" & FinalTotal & "</div>"
        my_table += "<div class='large-1 medium-3 small-3 columns bld'></div>"
        my_table += "</div>"
        my_table += "</div>"
        Return my_table

    End Function

    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click

        Try
            Dim AgencyDs As DataSet
            Dim FltDs As DataSet
            Dim totFare As Double = 0
            Dim netFare As Double = 0
            Dim PgMsg As String = ""
            FltDs = objDA.GetFltDtls(ViewState("trackid"), Session("UID"))
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            If AgencyDs.Tables.Count > 0 And FltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And FltDs.Tables(0).Rows.Count > 0 Then
                    totFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("totFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    netFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("netFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("totFare") = totFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("totFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("netFare") = netFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("netFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    ' FltDs.AcceptChanges()
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        Dim agentBal As String = ""

                        '' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                        If rblPaymentMode.SelectedValue = "WL" Then
                            ''agentBal = objUMSvc.GetAgencyBal()
                            ''agentBal = objUMSvc.GetAgencyBal()
                            '' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                            ''If Convert.ToDouble(agentBal) > netFare Then
                            ''Dim um As String = ""
                            ''um = objUMSvc.GetMUForPage("wait.aspx")
                            ''Response.Redirect(um & "?tid=" & ViewState("trackid") & "", False)
                            If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                                Response.Redirect("../wait.aspx?tid=" & ViewState("trackid") & "", False)
                            Else
                                ''Dim um As String = ""
                                ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                ''Response.Redirect(um & "?msg=CL", False)
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
                            'PgMsg = objPg.PaymentGatewayReq(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "INT")
                            ''Use for Payment Option
                            If rblPaymentMode.SelectedValue = "Paytm" Then
                                PgMsg = objPt.PaymentGatewayReqPaytm(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "INT", rblPaymentMode.SelectedValue)
                            Else

                                PgMsg = objPg.PaymentGatewayReqPayU(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "INT", rblPaymentMode.SelectedValue)

                            End If



                            ''                PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType,string IP)
                            If PgMsg.Split("~")(0) = "yes" Then
                                '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
                                    Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
                                Else
                                    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                                End If

                            Else
                                Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                            End If


                            ''redirect payment Gateway Url
                        End If
                    Else
                        ''Dim um As String = ""
                        ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        ''Response.Redirect(um & "?msg=NA", False)
                        Response.Redirect("../International/BookingMsg.aspx?msg=NA", False)
                    End If
                Else
                    ''Dim um As String = ""
                    ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                    ''Response.Redirect(um & "?msg=2", False)
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
            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
        End Try

    End Sub

    Protected Sub ButtonHold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonHold.Click
        Try
            Dim AgencyDs As DataSet
            Dim FltDs As DataSet
            Dim totFare As Double = 0
            Dim netFare As Double = 0
            Dim PgMsg As String = ""
            FltDs = objDA.GetFltDtls(ViewState("trackid"), Session("UID"))
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            If AgencyDs.Tables.Count > 0 And FltDs.Tables.Count > 0 Then
                If AgencyDs.Tables(0).Rows.Count > 0 And FltDs.Tables(0).Rows.Count > 0 Then
                    totFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("totFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    netFare = Convert.ToDouble(FltDs.Tables(0).Rows(0)("netFare")) '+ Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("totFare") = totFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("totFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    FltDs.Tables(0).Rows(0)("netFare") = netFare 'Convert.ToDouble(OBFltDs.Tables(0).Rows(0)("netFare")) + Convert.ToDouble(lbl_OB_TOT.Value)
                    ' FltDs.AcceptChanges()
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        Dim agentBal As String = ""

                        '' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                        If rblPaymentMode.SelectedValue = "WL" Then
                            ''agentBal = objUMSvc.GetAgencyBal()
                            ''agentBal = objUMSvc.GetAgencyBal()
                            '' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                            ''If Convert.ToDouble(agentBal) > netFare Then
                            ''Dim um As String = ""
                            ''um = objUMSvc.GetMUForPage("wait.aspx")
                            ''Response.Redirect(um & "?tid=" & ViewState("trackid") & "", False)


                            If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > (Convert.ToDecimal(ViewState("holdBookingChargeO"))) Then



                                If ViewState("FT") = "InBound" Then

                                    'objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), ViewState("IBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeR")))
                                    ' ''Dim um As String = ""
                                    ' ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ' ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    'Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                Else
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    objBal.UpdateHoldBookingCharge(ViewState("trackid"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), "", False, 0)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("trackid") & "&FT=" & ViewState("FT") & "", False)
                                End If
                            Else
                                ' ''Dim um As String = ""
                                ' ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                ' ''Response.Redirect(um & "?msg=CL", False)
                                Response.Redirect("../International/BookingMsg.aspx?msg=CL", False)
                            End If





                            ''old code 
                            'If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                            '    Response.Redirect("../wait.aspx?tid=" & ViewState("trackid") & "", False)
                            'Else
                            '    ''Dim um As String = ""
                            '    ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                            '    ''Response.Redirect(um & "?msg=CL", False)
                            '    Response.Redirect("../International/BookingMsg.aspx?msg=CL", False)
                            'End If



                        Else
                            ''Dim objPg As New PaymentGateway()
                            Dim ipAddress As String
                            ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                            If ipAddress = "" Or ipAddress Is Nothing Then
                                ipAddress = Request.ServerVariables("REMOTE_ADDR")
                            End If
                            Dim ReferenceNo As String = DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                            Dim Tid As String = ReferenceNo.Substring(4, 16)

                            ''new code
                            If Convert.ToDecimal(ViewState("holdBookingChargeO")) > 0 Then

                                Dim holdChrge As Decimal = Convert.ToDecimal(ViewState("holdBookingChargeO"))

                                ''Use for Payment Option


                                If ViewState("FT") = "InBound" Then

                                    '' objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), ViewState("IBTrackId"), True, Convert.ToDecimal(ViewState("holdBookingChargeR")))

                                Else

                                    objBal.UpdateHoldBookingCharge(ViewState("trackid"), True, Convert.ToDecimal(ViewState("holdBookingChargeO")), "", False, 0)

                                End If



                                PgMsg = objPg.PaymentGatewayReqPayU(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), holdChrge, holdChrge, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight_Hold", ipAddress, "INT", rblPaymentMode.SelectedValue)
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

                                    'objBal.UpdateHoldBookingCharge(ViewState("OBTrackId"), True, 0, ViewState("IBTrackId"), True, 0)
                                    ' ''Dim um As String = ""
                                    ' ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ' ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    'Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                Else
                                    ''Dim um As String = ""
                                    ''um = objUMSvc.GetMUForPage("wait.aspx")
                                    ''Response.Redirect(um & "?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    objBal.UpdateHoldBookingCharge(ViewState("trackid"), True, 0, "", False, 0)
                                    Response.Redirect("../WaitPage.aspx?OBTID=" & ViewState("trackid") & "&FT=" & ViewState("FT") & "", False)
                                End If

                            End If






                            ''OLD CODE
                            ''PgMsg = objPg.PaymentGatewayReq(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "INT")
                            ' ''Use for Payment Option
                            'PgMsg = objPg.PaymentGatewayReqPayU(ViewState("trackid"), Tid, "", Session("UID"), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Agency_Name")), netFare, netFare, Convert.ToString(AgencyDs.Tables(0).Rows(0)("Fname")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Address")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("City")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("State")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("zipcode")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile")), Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), "Flight", ipAddress, "INT", rblPaymentMode.SelectedValue)

                            ' ''                PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType,string IP)
                            'If PgMsg.Split("~")(0) = "yes" Then
                            '    '' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                            '    If Not String.IsNullOrEmpty(PgMsg.Split("~")(1)) Then
                            '        Page.Controls.Add(New LiteralControl(PgMsg.Split("~")(1)))
                            '    Else
                            '        Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                            '    End If

                            'Else
                            '    Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                            'End If


                            ''redirect payment Gateway Url
                        End If
                    Else
                        ''Dim um As String = ""
                        ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        ''Response.Redirect(um & "?msg=NA", False)
                        Response.Redirect("../International/BookingMsg.aspx?msg=NA", False)
                    End If
                Else
                    ''Dim um As String = ""
                    ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                    ''Response.Redirect(um & "?msg=2", False)
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
            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
        End Try

    End Sub

End Class
