Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Linq
Imports STD.BAL
Imports STD.Shared

Partial Class FlightDom_CustomerInfo
    Inherits System.Web.UI.Page
    Dim Constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    Dim objbal As New FlightCommonBAL(Constr)
    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim objFareBreakup As New clsCalcCommAndPlb
    Dim objDA As New SqlTransaction

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
    Dim clsCorp As New ClsCorporate()
    'varaibles
    Dim objSql As New SqlTransactionNew
    Dim VCOB As String = "", VCIB As String = ""
    Dim VCOBSPL As String = "", VCIBSPL As String = ""
    Dim TripOB As String = "", TripIB As String = ""
    Dim ATOB As String = "", ATIB As String = ""
    Dim FLT_STAT As String = ""
    Dim Adti As Integer = 0, Chdi As Integer = 0, SERCHVALO As String = "", SERCHVALR As String = ""
    Dim G8SSRListO As New List(Of G8ServiceQuoteResponse)()
    Dim G8SSRListR As New List(Of G8ServiceQuoteResponse)()
    Dim objUMSvc As New FltSearch1()
    Dim Provider As String = "", ProviderIB As String = ""
    Dim TBOSSR As New STD.BAL.TBO.SSR.SSRResponse()
    Dim TBOSSRIB As New STD.BAL.TBO.SSR.SSRResponse()
    Dim YASSR As New List(Of YASSR)
    Dim YASSRIB As New List(Of YASSR)
    Dim AKSSR, AKSSRIB, AKSSRSTF As New List(Of GALWS.AirAsia.AirAsiaSSR)()
    Dim AKSeat, AKSeatIB As New List(Of GALWS.AirAsia.AirAsiaSeat)()
  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim a As Integer = 0
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            Else
                If Not Page.IsPostBack Then
                    'GST Information Star
                    ' BindStateGst("India")
                    Dim AgencyDs As DataSet
                    AgencyDs = objDA.GetAgencyDetails(Session("UID"))
                    If AgencyDs.Tables.Count > 0 Then
                        If AgencyDs.Tables(0).Rows.Count > 0 Then
                            If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile"))) Then
                                txt_MobNo.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("Mobile"))
                            End If

                            If Not String.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email"))) Then
                                txt_Email.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email"))
                            End If
                            If Convert.ToString(AgencyDs.Tables(0).Rows(0)("Is_GST_Apply")).ToLower() = "true" Then
                                txtGstNo.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GSTNO"))
                                txtGstCmpName.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Company_Name"))
                                txtGstAddress.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GSTAddress"))
                                txtPinCode.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Pincode"))
                                txtGstPhone.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_PhoneNo"))
                                txtGstEmail.Text = Convert.ToString(AgencyDs.Tables(0).Rows(0)("GST_Email"))
                            End If
                        End If
                    End If
                    'GST Information END
                    Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
                    If ds IsNot Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            DropDownListProject.Items.Clear()
                            Dim item As New ListItem("Select")
                            DropDownListProject.AppendDataBoundItems = True
                            DropDownListProject.Items.Insert(0, item)
                            DropDownListProject.DataSource = ds.Tables(0)
                            DropDownListProject.DataTextField = "ProjectName"
                            DropDownListProject.DataValueField = "ProjectId"
                            DropDownListProject.DataBind()
                            spn_Projects.Visible = True
                            spn_Projects1.Visible = True
                        Else
                            spn_Projects.Visible = False
                            spn_Projects1.Visible = False
                        End If
                    End If

                    Dim dsbooked As DataSet = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "BB")
                    If dsbooked.Tables(0).Rows.Count > 0 Then
                        DropDownListBookedBy.AppendDataBoundItems = True
                        DropDownListBookedBy.Items.Clear()
                        DropDownListBookedBy.Items.Insert(0, "Select")
                        DropDownListBookedBy.DataSource = dsbooked
                        DropDownListBookedBy.DataTextField = "BOOKEDBY"
                        DropDownListBookedBy.DataValueField = "BOOKEDBY"
                        DropDownListBookedBy.DataBind()
                        Spn_BookedBy.Visible = True
                        Spn_BookedBy1.Visible = True
                    Else
                        Spn_BookedBy.Visible = False
                        Spn_BookedBy1.Visible = False

                    End If

                    Dim query As String = HttpContext.Current.Request.QueryString(0).ToString()
                    Dim Track As String() = query.Split(",")
                    If (Track.Length = 2) Then
                        FT = "InBound"
                    Else
                        FT = "OutBound"
                    End If
                    ViewState("FT") = FT
                    'fareHashtbl = objFareBreakup.getDomFareDetails(LIN, FT)
                    If FT = "OutBound" Then
                        OBTrackId = Track(0) ' objSelectedfltCls.getRndNum
                        ViewState("OBTrackId") = OBTrackId
                        'objSelectedfltCls.InsertFlightData(OBTrackId, LIN, Session("DomAirDt"), "", fareHashtbl("AdtTax").ToString, fareHashtbl("ChdTax").ToString, fareHashtbl("InfTax").ToString, fareHashtbl("SrvTax"), fareHashtbl("TFee"), fareHashtbl("TC"), fareHashtbl("adtTds"), fareHashtbl("chdTds"), fareHashtbl("adtComm"), fareHashtbl("chdComm"), fareHashtbl("adtCB"), fareHashtbl("chdCB"), fareHashtbl("totFare"), fareHashtbl("netFare"), Session("UID"))
                    Else
                        OBTrackId = Track(0) 'Request.QueryString("TID")
                        ViewState("OBTrackId") = OBTrackId
                        IBTrackId = Track(1) ' objSelectedfltCls.getRndNum
                        ViewState("IBTrackId") = IBTrackId
                        ' objSelectedfltCls.InsertFlightData(IBTrackId, LIN, Session("DomAirDtR"), "", fareHashtbl("AdtTax").ToString, fareHashtbl("ChdTax").ToString, fareHashtbl("InfTax").ToString, fareHashtbl("SrvTax"), fareHashtbl("TFee"), fareHashtbl("TC"), fareHashtbl("adtTds"), fareHashtbl("chdTds"), fareHashtbl("adtComm"), fareHashtbl("chdComm"), fareHashtbl("adtCB"), fareHashtbl("chdCB"), fareHashtbl("totFare"), fareHashtbl("netFare"), Session("UID"))
                    End If

                    Dim OBFltDs, IBFltDs As DataSet
                    OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID"))
                    IBFltDs = objDA.GetFltDtls(IBTrackId, Session("UID"))

                    Adult = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                    Child = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                    Infant = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))

                    lbl_adult.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Adult"))
                    lbl_child.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Child"))
                    lbl_infant.Text = Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Infant"))
                    'Check SME FARE
                    If Convert.ToString(OBFltDs.Tables(0).Rows(0)("IsSMEFare")).ToLower() = "true" Or (IBFltDs IsNot Nothing AndAlso IBFltDs.Tables.Count > 0 AndAlso IBFltDs.Tables(0).Rows.Count > 0 AndAlso Convert.ToString(IBFltDs.Tables(0).Rows(0)("IsSMEFare")).ToLower() = "true") Then
                        hdnCheckGST.Value = "true"
                    End If

                    'Code 5/04/2014
                    VCOB = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    VCOBSPL = OBFltDs.Tables(0).Rows(0)("AdtFareType")
                    TripOB = OBFltDs.Tables(0).Rows(0)("Trip")

                    Provider = OBFltDs.Tables(0).Rows(0)("Provider")
                    ''  If (Provider.Trim.ToUpper = "LCC") Then
                    InBoundFTseat_ibSec1.InnerHtml = "<a class='selectbtns topbutton topopup edit button' title='Select Seat' id='btnaddseat'>Seat Map " & OBFltDs.Tables(0).Rows(0)("Sector") & "</a>"

                    OBTrackIds.Value = OBTrackId
                    OBValidatingCarrier.Value = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    If FT = "InBound" Then
                        InBoundFTseat_ibSec2.InnerHtml = "<a class='selectbtns topbutton topopup_ib edit button' title='Select Seat' id='btnaddseat_ib'>Seat Map  " & IBFltDs.Tables(0).Rows(0)("Sector") & "</a> <div class='col-md-12 col-xs-12 bld p10'></div><input type='hidden' id='seatSelect_ib' name='seatSelect_ib' /><div class='clear'></div>"
                        InBoundFTseat_ibSec2.Visible = True
                        InBoundFTSeat_ibDt.Visible = True
                        IBTrackIds.Value = IBTrackId
                        IBValidatingCarrier.Value = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                    End If
                    ''End If
                    If FT = "InBound" Then
                        ProviderIB = IBFltDs.Tables(0).Rows(0)("Provider")

                    End If

                    If Provider.Trim.ToUpper = "TB" Or Provider.Trim.ToUpper = "YA" Or Provider.Trim.ToUpper = "AK"  Or (Provider.Trim.ToUpper = "LCC" And VCOB = "IX") Then
                        If OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("TripType").ToString().Trim() = "R" Then
                            FLT_STAT = "RTF"

                        End If

                    Else : FLT_STAT = OBFltDs.Tables(0).Rows(0)("FlightStatus")
                    End If

                    SERCHVALO = Convert.ToString(OBFltDs.Tables(0).Rows(0)("Searchvalue"))
                    hdn_vc.Value = VCOB
                    Dim Eq As Integer
                    Try
                        If InStr(VCOBSPL, "Special") = 0 And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And Provider.Trim().ToUpper() <> "TB" And Provider.Trim().ToUpper() <> "AK" Then
                            If (VCOB = "SG") Then
                                If (Convert.ToInt16(OBFltDs.Tables(0).Rows(0)("Tot_Dur").ToString().Substring(0, 2)) < 1) Then
                                    ATOB = "Q400"
                                Else
                                    If (OBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim() = "DH8") Then
                                        ATOB = "Q400"
                                    ElseIf (Int32.TryParse(OBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim(), Eq)) Then
                                        If (Eq >= 737 And Eq <= 900) Then
                                            ATOB = "Boeing"
                                        Else
                                            ATOB = ""
                                        End If
                                    End If
                                End If
                            ElseIf (VCOB = "6E") Then
                                ATOB = "ALL"
                            ElseIf (VCOB = "G8") Then
                                ATOB = "ALL"
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Dim exep As String = ""
                    If Provider.Trim().ToUpper() = "TB" Then
                        Dim dsCrd As DataSet = objSql.GetCredentials("TB", "", "D")
                        Dim snoArr As String() = Convert.ToString(OBFltDs.Tables(0).Rows(0)("sno")).Split(":")

                        Dim objBook As New STD.BAL.TBO.TBOBook()

                        Dim log As New Dictionary(Of String, String)

                        objBook.GetFareQuote(dsCrd, snoArr(1), snoArr(0), log, exep)
                        Dim objssr As New STD.BAL.TBO.SSR.TOBSSR()

                        TBOSSR = objssr.GetSSR("", snoArr(1), snoArr(0), dsCrd)
                        If FLT_STAT = "RTF" Then

                            Dim snoArrR As String() = Convert.ToString(OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno")).Split(":")

                            Dim log1 As New Dictionary(Of String, String)
                            objBook.GetFareQuote(dsCrd, snoArrR(1), snoArr(0) & "," & snoArrR(0), log1, exep)
                            TBOSSR = objssr.GetSSR("", snoArr(1), snoArr(0) & "," & snoArrR(0), dsCrd)
                            'TBOSSRIB = objssr.GetSSR("", snoArrR(1), snoArrR(0), dsCrd)

                        End If

                    End If
                    If Provider.Trim().ToUpper() = "YA" Then
                        Dim FltO As DataRow()
                        Dim FltR As DataRow()
                        Dim airPriceResp As String = ""
                        Dim dsCrd As DataSet = objSql.GetCredentials("YA", "", "D")
                        FltO = OBFltDs.Tables(0).Select("TripType='O'", "counter ASC")

                        Dim srchValArr As String = Convert.ToString(FltO(0)("Searchvalue"))
                        Dim objAirPrice As New STD.BAL.YAAirPrice()
                        Dim log As New Dictionary(Of String, String)

                        If FLT_STAT = "RTF" Then
                            FltR = OBFltDs.Tables(0).Select("TripType='R'", "counter ASC")
                            Dim srchValArrR As String = Convert.ToString(FltR(0)("Searchvalue"))
                            Dim log1 As New Dictionary(Of String, String)
                            Dim datasetPrice As DataSet = objbal.InserAndGetYAPricingLog(ViewState("OBTrackId"), "", "", "get")

                            Try
                                airPriceResp = Convert.ToString(datasetPrice.Tables(0)(0)("Price_Res"))
                            Catch ex As Exception

                            End Try
                            Dim ssrM1 As List(Of YASSR) = objAirPrice.GetSSR(airPriceResp)
                            YASSR = objAirPrice.GetSSRTripTypeWise(TripType.O, ssrM1)
                            YASSRIB = objAirPrice.GetSSRTripTypeWise(TripType.R, ssrM1)
                        Else
                            Dim srchRI As String = ""
                            Dim datasetPrice1 As DataSet = objbal.InserAndGetYAPricingLog(ViewState("OBTrackId"), "", "", "get")
                            Try
                                airPriceResp = Convert.ToString(datasetPrice1.Tables(0)(0)("Price_Res"))
                            Catch ex As Exception

                            End Try
                            Dim ssrM As List(Of YASSR) = objAirPrice.GetSSR(airPriceResp)
                            YASSR = objAirPrice.GetSSRTripTypeWise(TripType.O, ssrM)
                            If ProviderIB.Trim().ToUpper() = "YA" Then
                                Dim datasetPrice2 As DataSet = objbal.InserAndGetYAPricingLog(ViewState("IBTrackId"), "", "", "get")
                                Try
                                    airPriceResp = Convert.ToString(datasetPrice2.Tables(0)(0)("Price_Res"))
                                Catch ex As Exception

                                End Try
                                YASSRIB = objAirPrice.GetSSR(airPriceResp) '' objAirPrice.GetSSRTripTypeWise(TripType.R, ssrM1)
                            End If
                        End If
                    End If


                    If VCOB.Trim().ToUpper() = "IX" Then
                        Dim searchvalue As String = OBFltDs.Tables(0).Rows(0)("SearchValue")
                        Dim sno As String = OBFltDs.Tables(0).Rows(0)("sno")
                        Dim FZUrls As New IXSvcAndMethodUrls()
                      
                            Dim objsrvlist As New List(Of G8ServiceQuote)()
                            Dim objsrvQ As New G8ServiceQuote()
                            objsrvQ.LogicalFlightID = searchvalue.Split(":"c)(0)
                            objsrvQ.DepartureDate = OBFltDs.Tables(0).Rows(0)("sno").Split(":")(3).Split("T")(0)
                            objsrvQ.AirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                            objsrvQ.ServiceCode = ""
                            objsrvQ.Cabin = OBFltDs.Tables(0).Rows(0)("AdtCabin")
                            objsrvQ.Category = ""
                            objsrvQ.FareClass = OBFltDs.Tables(0).Rows(0)("AdtRbd")
                            objsrvQ.FareBasisCode = OBFltDs.Tables(0).Rows(0)("AdtFarebasis")
                            objsrvQ.DestinationAirportCode = OBFltDs.Tables(0).Rows(0)("OrgDestTo")
                            objsrvlist.Add(objsrvQ)
                            Dim objFZFQ As New STD.BAL.G8FareQuote(sno.Split(":"c)(1), "", "", "")
                            Dim expsrvQoute As String = ""
                            G8SSRListO = objFZFQ.GetIXServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist, Adult + Child, expsrvQoute)


                            If FT = "InBound" Then
                                Dim FRet As Integer = Convert.ToInt16(IBFltDs.Tables(0).Rows(0)("Flight"))
                                Dim objsrvQ1 As New G8ServiceQuote()
                                Dim searchvalue1 As String = IBFltDs.Tables(0).Rows(0)("SearchValue")
                                Dim sno1 As String = IBFltDs.Tables(0).Rows(0)("sno")
                                Dim objsrvlist1 As New List(Of G8ServiceQuote)()
                                objsrvQ1.LogicalFlightID = searchvalue1.Split(":"c)(0)
                                objsrvQ1.DepartureDate = IBFltDs.Tables(0).Rows(0)("sno").Split(":")(3).Split("T")(0)
                                objsrvQ1.AirportCode = IBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                                objsrvQ1.ServiceCode = ""
                                objsrvQ1.Cabin = IBFltDs.Tables(0).Rows(0)("AdtCabin")
                                objsrvQ1.Category = ""
                                objsrvQ1.FareClass = IBFltDs.Tables(0).Rows(0)("AdtRbd")
                                objsrvQ1.FareBasisCode = IBFltDs.Tables(0).Rows(0)("AdtFarebasis")
                                objsrvQ1.DestinationAirportCode = IBFltDs.Tables(0).Rows(0)("OrgDestTo")
                                objsrvlist1.Add(objsrvQ1)
                                Dim objFZFQ1 As New STD.BAL.G8FareQuote(sno1.Split(":"c)(1), "", "", "")
                                G8SSRListR = objFZFQ1.GetIXServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist1, Adult + Child, expsrvQoute)
                            ElseIf (FLT_STAT = "RTF") Then
                                Dim objsrvQ1 As New G8ServiceQuote()
                                Dim searchvalue1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("SearchValue")
                                Dim objsrvlist1 As New List(Of G8ServiceQuote)()
                                Dim sno1 As String = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno")
                                objsrvQ1.LogicalFlightID = searchvalue1.Split(":"c)(0)
                                objsrvQ1.DepartureDate = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("sno").Split(":")(3).Split("T")(0)
                                objsrvQ1.AirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestTo")
                                objsrvQ1.ServiceCode = ""
                                objsrvQ1.Cabin = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtCabin")
                                objsrvQ1.Category = ""
                                objsrvQ1.FareClass = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtRbd")
                                objsrvQ1.FareBasisCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("AdtFarebasis")
                                objsrvQ1.DestinationAirportCode = OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("OrgDestFrom")
                                objsrvlist1.Add(objsrvQ1)
                                Dim objFZFQ1 As New STD.BAL.FZFareQuote(sno1.Split(":"c)(1), "", "", "")
                                G8SSRListR = objFZFQ.GetIXServiceQuote(FZUrls.SSRUrl, FZUrls.PricingSvcUrl, objsrvlist1, Adult + Child, expsrvQoute)
                            End If

                    End If

                    If Provider.Trim().ToUpper() = "AK" Then
                        Try

                            Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "D")
                            Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                            Dim NewSessionId As String = ""
                            'Call Sell Transaction
                            Dim sellfare As Decimal = Math.Round(objAirAsia.GETSELL(OBFltDs.Tables(0), dsCrd, FLT_STAT, Constr, NewSessionId), 2)
                            Dim orgfare As Decimal = Math.Round(Convert.ToDecimal(OBFltDs.Tables(0).Rows(0)("OriginalTF")), 2)
                            'update table value with new sessionid
                            For sr As Integer = 0 To OBFltDs.Tables(0).Rows.Count - 1
                                OBFltDs.Tables(0).Rows(sr)("Searchvalue") = NewSessionId
                                OBFltDs.AcceptChanges()
                            Next
                            If (sellfare = 0) Then
                                ShowAlertMessage("We can not book this due to Fare has been changed in Sell Journey ")
                                book.Visible = False
                            ElseIf (sellfare > orgfare) Then
                                ShowAlertMessage("OutBond Fare has been changed. Now Rs " & (Convert.ToDecimal(OBFltDs.Tables(0).Rows(0)("totFare")) + (sellfare - orgfare)).ToString() & "")
                                book.Visible = False
                            End If
                            'call SSR Availibity
                            Dim serverpath As String = "D:\\" 'Server.MapPath("~")
                            AKSSR = objAirAsia.GetSSRAvailabilityForBooking(AKSSR, dsCrd, OBFltDs.Tables(0), serverpath, "")
                            '  AKSeat = objAirAsia.GetSeatAvailability(AKSeat, dsCrd, OBFltDs)
                            If FLT_STAT = "RTF" Then
                                'Dim sellfareR As Decimal = Math.Round(objAirAsia.GETSELL(OBFltDs.Tables(0), dsCrd, FLT_STAT), 2)
                                AKSSRSTF = objAirAsia.GetSSRAvailabilityForBooking(AKSSRSTF, dsCrd, OBFltDs.Tables(0), serverpath, FLT_STAT)
                                '  AKSeat = objAirAsia.GetSeatAvailability(AKSeat, dsCrd, OBFltDs)
                            End If

                        Catch ex As Exception
                            ITZERRORLOG.ExecptionLogger.FileHandling("Pagelode_AirAsiaSELL", "Error_001", ex, "Flight")
                            book.Visible = False
                        End Try
                    End If
                    'divFareDtls.InnerHtml = fareBreakupfun_Tot(OBFltDs, IBFltDs, FT)
                    getFarebreakup(OBFltDs, FT, IBFltDs)

                    If FT = "InBound" Then
                        VCIB = IBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                        VCIBSPL = IBFltDs.Tables(0).Rows(0)("AdtFareType")
                        TripIB = IBFltDs.Tables(0).Rows(0)("Trip")
                        SERCHVALR = Convert.ToString(IBFltDs.Tables(0).Rows(0)("Searchvalue"))
                        ProviderIB = IBFltDs.Tables(0).Rows(0)("Provider")
                        Dim EqIB As Integer
                        Try
                            If InStr(VCIBSPL, "Special") = 0 And (VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8") And ProviderIB.Trim().ToUpper() <> "TB" And ProviderIB.Trim().ToUpper() <> "AK" Then
                                If (VCIB = "SG") Then
                                    If (Convert.ToInt16(IBFltDs.Tables(0).Rows(0)("Tot_Dur").ToString().Substring(0, 2)) < 1) Then
                                        ATIB = "Q400"
                                    Else
                                        If (IBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim() = "DH8") Then
                                            ATIB = "Q400"
                                        ElseIf (Int32.TryParse(IBFltDs.Tables(0).Rows(0)("EQ").ToString().Trim(), EqIB)) Then
                                            If (EqIB >= 737 And EqIB <= 900) Then
                                                ATIB = "Boeing"
                                            Else
                                                ATIB = ""
                                            End If
                                        End If
                                    End If
                                ElseIf (VCIB = "6E") Then
                                    ATIB = "ALL"
                                ElseIf (VCIB = "G8") Then
                                    ATIB = "ALL"
                                End If
                            End If
                            If ProviderIB.Trim().ToUpper() = "TB" Then
                                Dim dsCrd As DataSet = objSql.GetCredentials("TB", "", "D")
                                Dim snoArr As String() = Convert.ToString(IBFltDs.Tables(0).Rows(0)("sno")).Split(":")

                                Dim objBook As New STD.BAL.TBO.TBOBook()
                                Dim log As New Dictionary(Of String, String)
                                objBook.GetFareQuote(dsCrd, snoArr(1), snoArr(0), log, exep)
                                Dim objssr As New STD.BAL.TBO.SSR.TOBSSR()
                                TBOSSRIB = objssr.GetSSR("", snoArr(1), snoArr(0), dsCrd)

                            End If
                            If Provider.Trim().ToUpper() = "AK" Or ProviderIB.Trim().ToUpper() = "AK"Then
                                Try
                                    Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "D")
                                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                                    Dim NewSessionId As String = ""
                                    'Call Sell Transaction
                                    Dim sellfare As Decimal = Math.Round(objAirAsia.GETSELL(IBFltDs.Tables(0), dsCrd, "", Constr, NewSessionId), 2)
                                    Dim orgfare As Decimal = Math.Round(Convert.ToDecimal(IBFltDs.Tables(0).Rows(0)("OriginalTF")), 2)
                                    'update table value with new sessionid
                                    For sr As Integer = 0 To IBFltDs.Tables(0).Rows.Count - 1
                                        IBFltDs.Tables(0).Rows(sr)("Searchvalue") = NewSessionId
                                        IBFltDs.AcceptChanges()
                                    Next
                                    If (sellfare = 0) Then
                                        ShowAlertMessage("We can not book this due to Fare has been changed in Sell Journey ")
                                        book.Visible = False
                                    ElseIf (sellfare > orgfare) Then
                                        ShowAlertMessage("Inbond Fare has been changed. Now Rs " & (Convert.ToDecimal(IBFltDs.Tables(0).Rows(0)("totFare")) + (sellfare - orgfare)).ToString() & "")
                                        book.Visible = False
                                    End If

                                    'call SSR Availibity
                                    Dim serverpath As String = "D:\\" 'Server.MapPath("~")
                                    AKSSRIB = objAirAsia.GetSSRAvailabilityForBooking(AKSSRIB, dsCrd, IBFltDs.Tables(0), serverpath, "")
                                Catch ex As Exception
                                    ITZERRORLOG.ExecptionLogger.FileHandling("Pagelode_AirAsiaSELL", "Error_001", ex, "Flight")
                                    book.Visible = False
                                End Try
                            End If

                        Catch ex As Exception

                        End Try

                    End If

                    If FLT_STAT = "RTF" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And Provider.Trim().ToUpper() <> "TB" And Provider.Trim().ToUpper() <> "AK" Then
                        Dim Org As String = OBFltDs.Tables(0).Rows(0)("OrgDestFrom")
                        Dim Dest As String = OBFltDs.Tables(0).Rows(0)("OrgDestTo")
                        Dim Dt As DataRow() = OBFltDs.Tables(0).Select("OrgDestFrom = '" & Dest & "'")
                        Dim row As Integer = OBFltDs.Tables(0).Rows.Count
                        SERCHVALR = Convert.ToString(OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("Searchvalue"))
                        Dim EqIB As Integer
                        If (VCOB = "SG") Then
                            If (Dt(0)("EQ").ToString().Trim() = "DH8") Then
                                ATIB = "Q400"
                            ElseIf (Int32.TryParse(Dt(0)("EQ").ToString().Trim(), EqIB)) Then
                                If (EqIB >= 737 And EqIB <= 900) Then
                                    ATIB = "Boeing"
                                Else
                                    ATIB = ""
                                End If
                            End If
                        ElseIf (VCOB = "6E") Then
                            ATIB = "ALL"
                        ElseIf (VCOB = "G8") Then
                            ATIB = "ALL"
                        End If
                    End If
                    '''Code End
                    Bind_pax(Adult, Child, Infant)
                    divFltDtls1.InnerHtml = showFltDetails(OBFltDs, IBFltDs, FT)
                    'divtotFlightDetails.clea
                    divtotFlightDetails.InnerHtml = STDom.CustFltDetails_Dom(OBFltDs, IBFltDs, FT)


                    Try
                        If (OBFltDs.Tables(0).Rows(0)("Provider") = "FDD") Then
                            For Each rw As RepeaterItem In Repeater_Adult.Items
                                '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                                Dim Fda As HtmlGenericControl = DirectCast(rw.FindControl("divFdAI"), HtmlGenericControl)
                                Fda.Visible = False

                                Dim FdaO As HtmlGenericControl = DirectCast(rw.FindControl("divFdAO"), HtmlGenericControl)
                                FdaO.Visible = False
                            Next
                            For Each rw As RepeaterItem In Repeater_Child.Items
                                '''''''''''''''''''''''' for adlut Possport/and all '''''''''''''''''''''''''''''''''''
                                Dim Fdc As HtmlGenericControl = DirectCast(rw.FindControl("divFdCI"), HtmlGenericControl)
                                Fdc.Visible = False

                                Dim FdcO As HtmlGenericControl = DirectCast(rw.FindControl("divFdCO"), HtmlGenericControl)
                                FdcO.Visible = False
                            Next
                            gstfd.Visible = False
                        End If
                    Catch ex As Exception

                    End Try

                Else
                    'Page Post Back

                End If
                'Book Button Show Hide - Staff
                Try
                    If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true") Then
                        If (Convert.ToString(Session("FlightActive")) <> "True") Then
                            book.Visible = False
                        End If
                    End If
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Private Sub getFarebreakup(ByVal OBDS As DataSet, ByVal Ft As String, ByVal IBDS As DataSet)

        Dim strPax As String = ""

        Try
            strPax = strPax & "<div class='fd-r'>"


            strPax = strPax & "<div class='bor'>"
            strPax = strPax & "<div class='prc-mm'><div class='prc-h'> <i><img src='../Images/icons/rupee.png' style='width: 30px;'/></i><span>  Price Summary</span></div>"
            strPax = strPax & "<div class='prc-h2' style='color:orange;'><img alt='' src='../images/adt.png' style='width:15px;'/>" & OBDS.Tables(0).Rows(0)("Adult") & "</div> &nbsp;"
            strPax = strPax & "<div class='prc-h3' style='color:orange;'><img alt='' src='../images/chd.png' style='width:15px;'/>" & OBDS.Tables(0).Rows(0)("Child") & "</div> &nbsp;"
            strPax = strPax & "<div class='prc-h4' style='color:orange;'><img alt='' src='../images/inf.png' style='width:15px;'/>" & OBDS.Tables(0).Rows(0)("Infant") & "</div>"
            'strPax = strPax & "</div>"
            'strPax = strPax & "
            strPax = strPax & "</div>"
            strPax = strPax & "<div class='prm' style='display:block;'>"
            ''strPax = strPax & "</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Adult</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Child</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Infant</div>"

            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'>"
            'strPax = strPax & "<div class='large-2 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/adt.png'/>(" & OBDS.Tables(0).Rows(0)("Adult") & ")</div>"
            'strPax = strPax & "<div class='large-2 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/chd.png'/>(" & OBDS.Tables(0).Rows(0)("Child") & ")</div>"
            'strPax = strPax & "<div class='large-2 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/inf.png'/>(" & OBDS.Tables(0).Rows(0)("Infant") & ")</div>"


            Dim TotalFare As Double
            Dim NetFare As Double
            Dim commision As Double
            Dim tds As Double
            If Ft = "InBound" Then
                TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("TotFare"))
                NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("NetFare"))
                commision = Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtComm") * OBDS.Tables(0).Rows(0)("Adult")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdComm") * OBDS.Tables(0).Rows(0)("Child"))
                tds = Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtTds") * OBDS.Tables(0).Rows(0)("Adult")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdTds") * OBDS.Tables(0).Rows(0)("Child"))
            Else
                TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare"))
                NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare"))
                commision = Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtComm") * OBDS.Tables(0).Rows(0)("Adult")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdComm") * OBDS.Tables(0).Rows(0)("Child"))
                tds = Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtTds") * OBDS.Tables(0).Rows(0)("Adult")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdTds") * OBDS.Tables(0).Rows(0)("Child"))
            End If





            'strPax = strPax & "<tr id='tr_totnetfare' style='display:none;position:absolute;background:#D1D1D1;padding:5x;'>"
            'strPax = strPax & "<td>Net Fare:" & NetFare & "</td>"
            'strPax = strPax & "</tr>"
            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'>"
            ''strPax = strPax & "<div class='large-5 medium-12 small-6 columns btn' id='ctl00_ContentPlaceHolder1_divtotFlight' onclick='ddshow(this.id);'>Flight Summary</div><div class='large-5 medium-12 small-6 columns btn' id='div_faredd' onclick='ddshow(this.id);'>Fare Summary</div>"







            strPax = strPax & "<div class='pr-l' style='font-weight:600;'>OutBound</div>" & fareBreakupfun(OBDS, "OutBound")
            If Ft = "InBound" Then
                strPax = strPax & "<div class='large-12 medium-12 small-12' style='background-color:#fff;padding-left:7px;line-height: 24px;'> "
                strPax = strPax & "<div  class='pr-l' style='font-weight:600;'>InBound</div>" & fareBreakupfun(IBDS, Ft)

            End If



            strPax = strPax & "<div class='row' style='padding: 0px 0px 0px 9px;background-color: #fff;margin-left: 5px;width: 98%;float: right;' > "
            strPax = strPax & "<div class='large-12 medium-12 small-12 columns' style='display:none;'><div style='font-size:18px; text-align:center;   background-color:#fff;padding-left:7px;line-height: 24px;font-weight:bold;  color:#000042; line-height:40px;border-top: dotted;margin-left: -23px;'>Total Fare: " & TotalFare & "</div><div id='tr_totnetfare' style='display:none;position:absolute;background:#F1F1F1;border: thin solid #D1D1D1;padding:10px; font-size:14px; font-weight:bold; color:#000;'>Net Fare: " & NetFare & "</div></div>"

            'strPax = strPax & "</div style='clear:both;'> </div> "
            strPax = strPax & "</div> "
            'strPax = strPax & "</div style='clear:both;'> </div> "
            strPax = strPax & "</div> "
            strPax = strPax & "</div> "
            'strPax = strPax & "</div style='clear:both;'> </div> "
            ''strPax = strPax & "</div>"
            'strPax = strPax & "</div style='clear:both;'> </div> "
            ''strPax = strPax & "</div>"
            ''strPax = strPax & "</div>"
            'strPax = strPax & "</div> "
            'strPax = strPax & "</div> "
            divtotalpax.InnerHtml = strPax
        Catch ex As Exception

        End Try

    End Sub
    Private Function showFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
        Try

            Dim droneway As DataRow()
            Dim drround As DataRow() = New DataRow(-1) {}

            'If FltHdr.Tables(0).Rows(0)("TripType").ToString().ToUpper() = "O" Then

            'droneway = FltDsGAL.Tables(0).[Select]("flight=1", "counter asc")
            'Else
            droneway = OBDS.Tables(0).[Select]("flight=1", "counter asc")
            drround = OBDS.Tables(0).[Select]("flight=2", "counter asc")
            'End If
            'Dim kk As Integer = VCCount1(droneway)
            strFlt = ""
            Dim Logo As String = ""
            Dim Airline As String = ""
            Dim DepartureTime As String = ""
            Dim ArrivalTime As String = ""
            If (VCCount1(droneway) = 0) Then
                Logo = "../Airlogo/sm" & droneway(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
                'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                Airline = droneway(0)("AirlineName") & "(" & droneway(0)("MarketingCarrier") & "-" & droneway(0)("FlightIdentification") & ")"
            Else
                Logo = "../Airlogo/multiple.png"
                Airline = "Multiple Airline"
            End If
            strFlt = strFlt & "<div class='row'>"
            strFlt = strFlt & "<div class='large-12 medium-12 small-12 headbgs' >Flight Details</div>"

            strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:19px; padding-top:10px;'  >"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='" & Logo & "'/><br />&nbsp;" & Airline & "</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("DepartureLocation") & "(" & droneway(0)("DepartureCityName") & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(droneway.Length - 1)("ArrivalLocation") & "(" & droneway(droneway.Length - 1)("ArrivalCityName") & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("Stops") & "</div>"
            strFlt = strFlt & "</div>"

            strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:19px; padding-top:10px; ' >"
            DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, droneway(0)("DepartureTime"))
            ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, droneway(droneway.Length - 1)("ArrivalTime"))

            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(0)("Departure_Date") & " (" & DepartureTime & ")</div>"
            strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & droneway(droneway.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</div>"
            If VCOB = "SG" And (SERCHVALO.Contains("P1") Or SERCHVALO.Contains("P2")) Then
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(droneway(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "<br/>7kg Hand Bag Only.</div>"
            Else
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(droneway(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "</div>"
            End If

            strFlt = strFlt & "</div>"
            strFlt = strFlt & "</div>"



            If (drround.Length > 0) Then
                Airline = ""
                Logo = ""
                If (VCCount1(drround) = 0) Then
                    Logo = "../Airlogo/sm" & drround(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
                    'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
                    Airline = drround(0)("AirlineName") & "(" & drround(0)("MarketingCarrier") & "-" & drround(0)("FlightIdentification") & ")"
                Else
                    Logo = "../Airlogo/multiple.png"
                    Airline = "Multiple Airline"
                End If


                'Airline = drround(0)("AirlineName") & "(" & drround(0)("MarketingCarrier") & "-" & drround(0)("FlightIdentification") & ")"

                strFlt = strFlt & "<div class='row'>"
                strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:19px; padding-top:10px; ' >"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='" & Logo & "'/><br />" & Airline & "</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("DepartureLocation") & "(" & drround(0)("DepartureCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(drround.Length - 1)("ArrivalLocation") & "(" & drround(drround.Length - 1)("ArrivalCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("Stops") & "</div>"
                strFlt = strFlt & "</div>"

                strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:29px; border-top:1px solid #ccc; padding-top:10px;' >"

                DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, drround(0)("DepartureTime"))
                ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, drround(drround.Length - 1)("ArrivalTime"))

                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(0)("Departure_Date") & " (" & DepartureTime & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & drround(drround.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</div>"
                If VCOB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(drround(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "<br/>7kg Hand Bag Only.</div>"
                Else
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(drround(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "</div>"
                End If
                strFlt = strFlt & "</div>"

                strFlt = strFlt & "</div>"

            End If
            If FT = "InBound" Then
                If (VCCount(IBDS.Tables(0)) = 0) Then
                    Logo = MultiValueFunction(IBDS.Tables(0), "Logo")
                    Airline = MultiValueFunction(IBDS.Tables(0), "Airline")
                Else
                    Logo = "../Airlogo/multiple.png"
                    Airline = "Multiple Airline"
                End If
                strFlt = strFlt & "<div class='row'>"
                strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:19px; padding-top:10px; border-top:1px solid #ccc;' >"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'><img alt='' src='" & Logo & "'/><br />" & Airline & "</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & IBDS.Tables(0).Rows(0)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(0)("DepartureCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalCityName") & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & IBDS.Tables(0).Rows(0)("Stops") & "</div>"
                strFlt = strFlt & "</div>"

                strFlt = strFlt & "<div class='large-12 medium-12 small-12' style='line-height:19px; padding-top:10px; ' >"
                DepartureTime = MultiValueFunction(IBDS.Tables(0), "Dep")
                ArrivalTime = MultiValueFunction(IBDS.Tables(0), "Arr")

                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & IBDS.Tables(0).Rows(0)("Departure_Date") & " (" & DepartureTime & ")</div>"
                strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("Arrival_Date") & " (" & ArrivalTime & ")</div>"
                If VCIB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(IBDS.Tables(0).Rows(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "<br/>7kg Hand Bag Only.</div>"
                Else
                    strFlt = strFlt & "<div class='large-3 medium-3 small-3 columns'>" & If(IBDS.Tables(0).Rows(0)("AdtFareType").ToString().Trim().ToLower() = "refundable", "<img src='../images/refundable.png' title='Refundable Fare' />", "<img src='../images/non-refundable.png' title='Non-Refundable Fare' />") & "</div>"
                End If

                strFlt = strFlt & "</div>"
                strFlt = strFlt & "</div>"


            End If


            Dim strPax As String = ""
            'strPax = strPax & "<div class='row' style='padding:10px; ' >"
            'strPax = strPax & "<div class='large-12 medium-12 small-12 headbgs'>Fare Details</div>"
            'strPax = strPax & "<div class='large-12 medium-12 small-12' style='background-color:#fff;padding:10px;'>"

            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Adult</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Child</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'>Infant</div>"

            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/adt.png'/>(" & OBDS.Tables(0).Rows(0)("Adult") & ")</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/chd.png'/>(" & OBDS.Tables(0).Rows(0)("Child") & ")</div>"
            'strPax = strPax & "<div class='large-4 medium-4 small-4 columns'> &nbsp; <img alt='' src='../images/inf.png'/>(" & OBDS.Tables(0).Rows(0)("Infant") & ")</div>"
            'strPax = strPax & "</div>"

            'strPax = strPax & "<tr id='tr_tottotfare' onmouseover=funcnetfare('block','tr_totnetfare'); onmouseout=funcnetfare('none','tr_totnetfare'); style='cursor:pointer;color: #004b91'>"
            'Dim TotalFare As Double
            'Dim NetFare As Double
            'If FT = "InBound" Then
            '    TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("TotFare"))
            '    NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("NetFare"))
            'Else
            '    TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare"))
            '    NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare"))
            'End If

            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'><span style='font-size:14px; font-weight:bold; color:#000042; line-height:40px;'>Total Fare: " & TotalFare & "</span><div id='tr_totnetfare' style='display:none;position:absolute;background:#F1F1F1;border: thin solid #D1D1D1;padding:10px; font-size:14px; font-weight:bold; color:#000;'>Net Fare: " & NetFare & "</div></div>"


            ''strPax = strPax & "<tr id='tr_totnetfare' style='display:none;position:absolute;background:#D1D1D1;padding:5x;'>"
            ''strPax = strPax & "<td>Net Fare:" & NetFare & "</td>"
            ''strPax = strPax & "</tr>"
            'strPax = strPax & "<div class='large-12 medium-12 small-12 columns'>"
            'strPax = strPax & "<div class='large-5 medium-12 small-6 columns btn' id='ctl00_ContentPlaceHolder1_divtotFlight' onclick='ddshow(this.id);'>Flight Summary</div><div class='large-5 medium-12 small-6 columns btn' id='div_faredd' onclick='ddshow(this.id);'>Fare Summary</div>"
            'strPax = strPax & "</div>"
            'strPax = strPax & "<div class='clear'> </div>"
            'strPax = strPax & "</div>"

            'strPax = strPax & "</div>"

            'divtotalpax.InnerHtml = strPax
            ' divtotFlightDetails.InnerHtml = CustFltDetails(OBDS, IBDS, FT)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


        Return strFlt
    End Function
    'Private Function CustFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
    '    Dim FlightDtlsTotalInfo As String = ""
    '    Dim DepTerminal As String
    '    Dim ArrTerminal As String
    '    Dim FlightType = ""
    '    If FT = "InBound" Then
    '        FlightType = "OutBound"
    '    End If

    '    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<table  width='100%' border='0' cellspacing='0' cellpadding='0'>"
    '    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;' >" & FlightType & " Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '>" & OBDS.Tables(0).Rows(0)("AdtFareType") & "</td><tr>"
    '    For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
    '        DepTerminal = ""
    '        ArrTerminal = ""
    '        'Dim AirportName As String =STDom.GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td ><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & OBDS.Tables(0).Rows(i)("AirlineName") & "</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>(" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Depall", i) & ")</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Arrall", i) & ")</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >Class(" & OBDS.Tables(0).Rows(i)("RBD") & ") </td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("DepartureCityName") & "(" & OBDS.Tables(0).Rows(i)("DepartureLocation") & ")</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("ArrivalCityName") & "(" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & ")</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '        If OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
    '            DepTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
    '        End If
    '        If OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
    '            ArrTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
    '        End If

    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='padding-left: 25px'></td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</td>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    '    Next
    '    If FT = "InBound" Then
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td style='padding-top: 20px'> </td></tr>"
    '        ' FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td colspan='4' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;'>Outbound Flight Details<td><tr>"
    '        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;' >InBound Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "</td><tr>"
    '        For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
    '            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & IBDS.Tables(0).Rows(i)("AirlineName") & "(" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    '            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(i)("DepartureCityName") & ")</td>"
    '            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(i)("ArrivalCityName") & ")</td>"
    '            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td ><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & IBDS.Tables(0).Rows(i)("AirlineName") & "</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>(" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Depall", i) & ")</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Arrall", i) & ")</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >Class(" & IBDS.Tables(0).Rows(i)("RBD") & ") </td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Depall", i) & ")</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Arrall", i) & ")</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    '            DepTerminal = ""
    '            ArrTerminal = ""
    '            If IBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
    '                DepTerminal = "Terminal - " & IBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
    '            End If
    '            If IBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
    '                ArrTerminal = "Terminal - " & IBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
    '            End If
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='padding-left: 25px'></td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(IBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(IBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</td>"
    '            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    '        Next
    '    End If
    '    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</table>"


    '    Return FlightDtlsTotalInfo
    'End Function
    'Private Function fareBreakupfun_Tot(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
    '    Dim fbfstr As String = ""
    '    fbfstr = fbfstr & "<table  width='250px' border='0' cellspacing='2' cellpadding='2'>"
    '    fbfstr = fbfstr & "<tr>"
    '    fbfstr = fbfstr & "<td colspan='2'>Outbound Fare</td>"
    '    fbfstr = fbfstr & "</tr>"
    '    fbfstr = fbfstr & "<tr>"
    '    fbfstr = fbfstr & "<td>Adult Fare(1)</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) & "</td>"
    '    fbfstr = fbfstr & "</tr>"
    '    If Child > 0 Then
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Child Fare(1)</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '    End If
    '    If Infant > 0 Then
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Infant Fare(1)</td><td>" & OBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '    End If
    '    'fbfstr = fbfstr & "<tr>"
    '    'fbfstr = fbfstr & "<td>Total Fare</td><td>" & OBDS.Tables(0).Rows(0)("totFare") & "</td>"
    '    'fbfstr = fbfstr & "</tr>"

    '    If (FT = "InBound") Then
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td colspan='2'>Inbound Fare</td>"
    '        fbfstr = fbfstr & "</tr>"
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Adult Fare(1)</td><td>" & Convert.ToDouble(IBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ADTAgentMrk")) & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '        If Child > 0 Then
    '            fbfstr = fbfstr & "<tr>"
    '            fbfstr = fbfstr & "<td>Child Fare(1)</td><td>" & Convert.ToDouble(IBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("CHDAgentMrk")) & "</td>"
    '            fbfstr = fbfstr & "</tr>"
    '        End If
    '        If Infant > 0 Then
    '            fbfstr = fbfstr & "<tr>"
    '            fbfstr = fbfstr & "<td>Infant Fare(1)</td><td>" & IBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    '            fbfstr = fbfstr & "</tr>"
    '        End If
    '        'fbfstr = fbfstr & "<tr>"
    '        'fbfstr = fbfstr & "<td>Total Fare</td><td>" & IBDS.Tables(0).Rows(0)("totFare") & "</td>"
    '        'fbfstr = fbfstr & "</tr>"

    '    End If
    '    Dim K As String = ""
    '    If (FT <> "InBound") Then
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Total(Per Pax) </td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) + OBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Grand Total</td><td>" & OBDS.Tables(0).Rows(0)("totFare") & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '    Else
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Total(Per Pax) </td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) + OBDS.Tables(0).Rows(0)("InfFare") + Convert.ToDouble(IBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("CHDAgentMrk")) + IBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '        fbfstr = fbfstr & "<tr>"
    '        fbfstr = fbfstr & "<td>Grand Total</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("totFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("totFare")) & "</td>"
    '        fbfstr = fbfstr & "</tr>"
    '        K = "(InBound+OutBound)"
    '    End If
    '    fbfstr = fbfstr & "<tr>"
    '    fbfstr = fbfstr & "<td colspan='2'>" & OBDS.Tables(0).Rows(0)("Adult") & " ADT," & OBDS.Tables(0).Rows(0)("Child") & " CHD," & OBDS.Tables(0).Rows(0)("Infant") & " INF " & K & "</td>"
    '    fbfstr = fbfstr & "</tr>"
    '    fbfstr = fbfstr & "</table>"
    '    Return fbfstr

    'End Function
    'Private Function showFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
    '    Try

    '        Dim droneway As DataRow()
    '        Dim drround As DataRow() = New DataRow(-1) {}

    '        'If FltHdr.Tables(0).Rows(0)("TripType").ToString().ToUpper() = "O" Then

    '        'droneway = FltDsGAL.Tables(0).[Select]("flight=1", "counter asc")
    '        'Else
    '        droneway = OBDS.Tables(0).[Select]("flight=1", "counter asc")
    '        drround = OBDS.Tables(0).[Select]("flight=2", "counter asc")
    '        'End If
    '        'Dim kk As Integer = VCCount1(droneway)
    '        strFlt = ""
    '        Dim Logo As String = ""
    '        Dim Airline As String = ""
    '        Dim DepartureTime As String = ""
    '        Dim ArrivalTime As String = ""
    '        If (VCCount1(droneway) = 0) Then
    '            Logo = "../Airlogo/sm" & droneway(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
    '            'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
    '            Airline = droneway(0)("AirlineName") & "(" & droneway(0)("MarketingCarrier") & "-" & droneway(0)("FlightIdentification") & ")"
    '        Else
    '            Logo = "../Airlogo/multiple.png"
    '            Airline = "Multiple Airline"
    '        End If
    '        strFlt = strFlt & "<table  width='100%' border='0' cellspacing='2' cellpadding='2'>"
    '        strFlt = strFlt & "<tr>"
    '        strFlt = strFlt & "<td  valign='top' >"

    '        strFlt = strFlt & "<table  width='100%' border='0' cellspacing='2' cellpadding='2'>"
    '        strFlt = strFlt & "<tr>"
    '        strFlt = strFlt & "<td style='font-size:14px; font-weight:bold; color:#fff; margin-bottom:15px;' colspan='4'>Flight Details</td>"
    '        strFlt = strFlt & "</tr>"
    '        strFlt = strFlt & "<tr>"
    '        strFlt = strFlt & "<td><img alt='' src='" & Logo & "' class='mtop'/><br />" & Airline & "</td>"
    '        strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & droneway(0)("DepartureLocation") & "(" & droneway(0)("DepartureCityName") & ")</td>"
    '        strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & droneway(droneway.Length - 1)("ArrivalLocation") & "(" & droneway(droneway.Length - 1)("ArrivalCityName") & ")</td>"
    '        strFlt = strFlt & "<td>" & droneway(0)("Stops") & "</td>"
    '        strFlt = strFlt & "</tr>"
    '        strFlt = strFlt & "<tr>"
    '        strFlt = strFlt & "<td></td>"
    '        DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, droneway(0)("DepartureTime"))
    '        ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, droneway(droneway.Length - 1)("ArrivalTime"))
    '        strFlt = strFlt & "<td>" & droneway(0)("Departure_Date") & " (" & DepartureTime & ")</td>"
    '        strFlt = strFlt & "<td>" & droneway(droneway.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</td>"
    '        If VCOB = "SG" And (SERCHVALO.Contains("P1") Or SERCHVALO.Contains("P2")) Then
    '            strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & droneway(0)("AdtFareType") & "<br/>7kg Hand Bag Only.</td>"
    '        Else
    '            strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & droneway(0)("AdtFareType") & "</td>"
    '        End If

    '        strFlt = strFlt & "</tr>"


    '        If (drround.Length > 0) Then
    '            Airline = ""
    '            Logo = ""
    '            If (VCCount1(drround) = 0) Then
    '                Logo = "../Airlogo/sm" & drround(0)("MarketingCarrier") & ".gif" 'MultiValueFunction(OBDS.Tables(0), "Logo")
    '                'Airline = MultiValueFunction(OBDS.Tables(0), "Airline")
    '                Airline = drround(0)("AirlineName") & "(" & drround(0)("MarketingCarrier") & "-" & drround(0)("FlightIdentification") & ")"
    '            Else
    '                Logo = "../Airlogo/multiple.png"
    '                Airline = "Multiple Airline"
    '            End If


    '            'Airline = drround(0)("AirlineName") & "(" & drround(0)("MarketingCarrier") & "-" & drround(0)("FlightIdentification") & ")"
    '            strFlt = strFlt & "<tr>"
    '            strFlt = strFlt & "<td><img alt='' src='" & Logo & "'/><br />" & Airline & "</td>"
    '            strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & drround(0)("DepartureLocation") & "(" & drround(0)("DepartureCityName") & ")</td>"
    '            strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & drround(drround.Length - 1)("ArrivalLocation") & "(" & drround(drround.Length - 1)("ArrivalCityName") & ")</td>"
    '            strFlt = strFlt & "<td>" & drround(0)("Stops") & "</td>"
    '            strFlt = strFlt & "</tr>"
    '            strFlt = strFlt & "<tr>"
    '            strFlt = strFlt & "<td></td>"
    '            DepartureTime = MultiValueFunction(OBDS.Tables(0), "Deprow", 0, drround(0)("DepartureTime"))
    '            ArrivalTime = MultiValueFunction(OBDS.Tables(0), "Arrrow", 0, drround(drround.Length - 1)("ArrivalTime"))
    '            strFlt = strFlt & "<td>" & drround(0)("Departure_Date") & " (" & DepartureTime & ")</td>"
    '            strFlt = strFlt & "<td>" & drround(drround.Length - 1)("Arrival_Date") & " (" & ArrivalTime & ")</td>"
    '            If VCOB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
    '                strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & drround(0)("AdtFareType") & "<br/>7kg Hand Bag Only.</td>"
    '            Else
    '                strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & drround(0)("AdtFareType") & "</td>"
    '            End If

    '            strFlt = strFlt & "</tr>"
    '        End If
    '        If FT = "InBound" Then
    '            If (VCCount(IBDS.Tables(0)) = 0) Then
    '                Logo = MultiValueFunction(IBDS.Tables(0), "Logo")
    '                Airline = MultiValueFunction(IBDS.Tables(0), "Airline")
    '            Else
    '                Logo = "../Airlogo/multiple.png"
    '                Airline = "Multiple Airline"
    '            End If
    '            strFlt = strFlt & "<tr>"
    '            strFlt = strFlt & "<td><img alt='' src='" & Logo & "'/><br />" & Airline & "</td>"
    '            strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(0)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(0)("DepartureCityName") & ")</td>"
    '            strFlt = strFlt & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("ArrivalCityName") & ")</td>"
    '            strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(0)("Stops") & "</td>"
    '            strFlt = strFlt & "</tr>"
    '            strFlt = strFlt & "<tr>"
    '            strFlt = strFlt & "<td></td>"
    '            DepartureTime = MultiValueFunction(IBDS.Tables(0), "Dep")
    '            ArrivalTime = MultiValueFunction(IBDS.Tables(0), "Arr")
    '            strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(0)("Departure_Date") & " (" & DepartureTime & ")</td>"
    '            strFlt = strFlt & "<td>" & IBDS.Tables(0).Rows(IBDS.Tables(0).Rows.Count - 1)("Arrival_Date") & " (" & ArrivalTime & ")</td>"
    '            If VCIB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
    '                strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "<br/>7kg Hand Bag Only.</td>"
    '            Else
    '                strFlt = strFlt & "<td style='font-size:16px; color:#004b91;'>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "</td>"
    '            End If

    '            strFlt = strFlt & "</tr>"

    '        End If

    '        strFlt = strFlt & "</table>"
    '        strFlt = strFlt & "</td>"
    '        strFlt = strFlt & "</tr>"

    '        strFlt = strFlt & "</table>"

    '        Dim strPax As String = ""
    '        strPax = strPax & "<table  width='100%' border='0' cellspacing='2' cellpadding='2'>"
    '        strFlt = strFlt & "<tr>"
    '        strFlt = strFlt & "<td style='font-size:14px; font-weight:bold; color:#fff; margin-bottom:15px;' colspan='3'>Flight Details</td>"
    '        strFlt = strFlt & "</tr>"
    '        strPax = strPax & "<tr style='font-size:13px;font-weight:bold'>"
    '        strPax = strPax & "<td>Adult</td>"
    '        strPax = strPax & "<td>Child</td>"
    '        strPax = strPax & "<td>Infant</td>"
    '        strPax = strPax & "</tr>"
    '        strPax = strPax & "<tr>"
    '        strPax = strPax & "<td valign='top'> &nbsp; <img alt='' src='../images/adt.png'/>(" & OBDS.Tables(0).Rows(0)("Adult") & ")</td>"
    '        strPax = strPax & "<td> &nbsp; <img alt='' src='../images/chd.png'/>(" & OBDS.Tables(0).Rows(0)("Child") & ")</td>"
    '        strPax = strPax & "<td> &nbsp; <img alt='' src='../images/inf.png'/>(" & OBDS.Tables(0).Rows(0)("Infant") & ")</td>"
    '        strPax = strPax & "</tr>"
    '        strPax = strPax & "<tr id='tr_tottotfare' onmouseover=funcnetfare('block','tr_totnetfare'); onmouseout=funcnetfare('none','tr_totnetfare'); style='cursor:pointer;color: #004b91'>"
    '        Dim TotalFare As Double
    '        Dim NetFare As Double
    '        If FT = "InBound" Then
    '            TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("TotFare"))
    '            NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("NetFare"))
    '        Else
    '            TotalFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("TotFare"))
    '            NetFare = Convert.ToDouble(OBDS.Tables(0).Rows(0)("NetFare"))
    '        End If
    '        strPax = strPax & "<td colspan='3'><span style='font-size:14px; font-weight:bold; color:#000042; line-height:40px;'>Total Fare: " & TotalFare & "</span><div id='tr_totnetfare' style='display:none;position:absolute;background:#F1F1F1;border: thin solid #D1D1D1;padding:10px; font-size:14px; font-weight:bold; color:#000;'>Net Fare: " & NetFare & "</div></td>"
    '        strPax = strPax & "</tr>"

    '        'strPax = strPax & "<tr id='tr_totnetfare' style='display:none;position:absolute;background:#D1D1D1;padding:5x;'>"
    '        'strPax = strPax & "<td>Net Fare:" & NetFare & "</td>"
    '        'strPax = strPax & "</tr>"

    '        strPax = strPax & "<tr><td colspan='3'><div style='float:left; cursor:pointer; background:#004b91; padding:5px 10px; color:#fff; font-weight:bold;' id='ctl00_ContentPlaceHolder1_divtotFlight' onclick='ddshow(this.id);'>Flight Summary</div><div style='float:right; font-weight:bold; cursor:pointer; background:#004b91; padding:5px 10px; color:#fff;' id='div_faredd' onclick='ddshow(this.id);'>Fare Summary</div></td></tr>"
    '        strPax = strPax & "</table>"
    '        divtotalpax.InnerHtml = strPax
    '        ' divtotFlightDetails.InnerHtml = CustFltDetails(OBDS, IBDS, FT)
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try


    '    Return strFlt
    'End Function
    ''Private Function CustFltDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
    ''    Dim FlightDtlsTotalInfo As String = ""
    ''    Dim DepTerminal As String
    ''    Dim ArrTerminal As String
    ''    Dim FlightType = ""
    ''    If FT = "InBound" Then
    ''        FlightType = "OutBound"
    ''    End If

    ''    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<table  width='100%' border='0' cellspacing='0' cellpadding='0'>"
    ''    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;' >" & FlightType & " Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '>" & OBDS.Tables(0).Rows(0)("AdtFareType") & "</td><tr>"
    ''    For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
    ''        DepTerminal = ""
    ''        ArrTerminal = ""
    ''        'Dim AirportName As String =STDom.GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td ><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & OBDS.Tables(0).Rows(i)("AirlineName") & "</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>(" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Depall", i) & ")</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(OBDS.Tables(0), "Arrall", i) & ")</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >Class(" & OBDS.Tables(0).Rows(i)("RBD") & ") </td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("DepartureCityName") & "(" & OBDS.Tables(0).Rows(i)("DepartureLocation") & ")</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("ArrivalCityName") & "(" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & ")</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''        If OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
    ''            DepTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
    ''        End If
    ''        If OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
    ''            ArrTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
    ''        End If

    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='padding-left: 25px'></td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(OBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</td>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    ''    Next
    ''    If FT = "InBound" Then
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td style='padding-top: 20px'> </td></tr>"
    ''        ' FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr><td colspan='4' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;'>Outbound Flight Details<td><tr>"
    ''        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:18px; line-height:35px; border-bottom:2px solid #d1d1d1;' >InBound Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '>" & IBDS.Tables(0).Rows(0)("AdtFareType") & "</td><tr>"
    ''        For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
    ''            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & IBDS.Tables(0).Rows(i)("AirlineName") & "(" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    ''            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & "(" & IBDS.Tables(0).Rows(i)("DepartureCityName") & ")</td>"
    ''            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("ArrivalLocation") & "(" & IBDS.Tables(0).Rows(i)("ArrivalCityName") & ")</td>"
    ''            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td ><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & IBDS.Tables(0).Rows(i)("AirlineName") & "</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>(" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Depall", i) & ")</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Arrall", i) & ")</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >Class(" & IBDS.Tables(0).Rows(i)("RBD") & ") </td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Depall", i) & ")</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction(IBDS.Tables(0), "Arrall", i) & ")</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    ''            DepTerminal = ""
    ''            ArrTerminal = ""
    ''            If IBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
    ''                DepTerminal = "Terminal - " & IBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
    ''            End If
    ''            If IBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
    ''                ArrTerminal = "Terminal - " & IBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
    ''            End If
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='padding-left: 25px'></td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(IBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & STDom.GetAirportName(IBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</td>"
    ''            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
    ''        Next
    ''    End If
    ''    FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</table>"


    ''    Return FlightDtlsTotalInfo
    ''End Function
    ''Private Function fareBreakupfun_Tot(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
    ''    Dim fbfstr As String = ""
    ''    fbfstr = fbfstr & "<table  width='250px' border='0' cellspacing='2' cellpadding='2'>"
    ''    fbfstr = fbfstr & "<tr>"
    ''    fbfstr = fbfstr & "<td colspan='2'>Outbound Fare</td>"
    ''    fbfstr = fbfstr & "</tr>"
    ''    fbfstr = fbfstr & "<tr>"
    ''    fbfstr = fbfstr & "<td>Adult Fare(1)</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) & "</td>"
    ''    fbfstr = fbfstr & "</tr>"
    ''    If Child > 0 Then
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Child Fare(1)</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''    End If
    ''    If Infant > 0 Then
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Infant Fare(1)</td><td>" & OBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''    End If
    ''    'fbfstr = fbfstr & "<tr>"
    ''    'fbfstr = fbfstr & "<td>Total Fare</td><td>" & OBDS.Tables(0).Rows(0)("totFare") & "</td>"
    ''    'fbfstr = fbfstr & "</tr>"

    ''    If (FT = "InBound") Then
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td colspan='2'>Inbound Fare</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Adult Fare(1)</td><td>" & Convert.ToDouble(IBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ADTAgentMrk")) & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''        If Child > 0 Then
    ''            fbfstr = fbfstr & "<tr>"
    ''            fbfstr = fbfstr & "<td>Child Fare(1)</td><td>" & Convert.ToDouble(IBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("CHDAgentMrk")) & "</td>"
    ''            fbfstr = fbfstr & "</tr>"
    ''        End If
    ''        If Infant > 0 Then
    ''            fbfstr = fbfstr & "<tr>"
    ''            fbfstr = fbfstr & "<td>Infant Fare(1)</td><td>" & IBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    ''            fbfstr = fbfstr & "</tr>"
    ''        End If
    ''        'fbfstr = fbfstr & "<tr>"
    ''        'fbfstr = fbfstr & "<td>Total Fare</td><td>" & IBDS.Tables(0).Rows(0)("totFare") & "</td>"
    ''        'fbfstr = fbfstr & "</tr>"

    ''    End If
    ''    Dim K As String = ""
    ''    If (FT <> "InBound") Then
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Total(Per Pax) </td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) + OBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Grand Total</td><td>" & OBDS.Tables(0).Rows(0)("totFare") & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''    Else
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Total(Per Pax) </td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(OBDS.Tables(0).Rows(0)("CHDAgentMrk")) + OBDS.Tables(0).Rows(0)("InfFare") + Convert.ToDouble(IBDS.Tables(0).Rows(0)("AdtFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ADTAgentMrk")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("ChdFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("CHDAgentMrk")) + IBDS.Tables(0).Rows(0)("InfFare") & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''        fbfstr = fbfstr & "<tr>"
    ''        fbfstr = fbfstr & "<td>Grand Total</td><td>" & Convert.ToDouble(OBDS.Tables(0).Rows(0)("totFare")) + Convert.ToDouble(IBDS.Tables(0).Rows(0)("totFare")) & "</td>"
    ''        fbfstr = fbfstr & "</tr>"
    ''        K = "(InBound+OutBound)"
    ''    End If
    ''    fbfstr = fbfstr & "<tr>"
    ''    fbfstr = fbfstr & "<td colspan='2'>" & OBDS.Tables(0).Rows(0)("Adult") & " ADT," & OBDS.Tables(0).Rows(0)("Child") & " CHD," & OBDS.Tables(0).Rows(0)("Infant") & " INF " & K & "</td>"
    ''    fbfstr = fbfstr & "</tr>"
    ''    fbfstr = fbfstr & "</table>"
    ''    Return fbfstr

    ''End Function
    Private Function fareBreakupfun(ByVal OFareDS As DataSet, ByVal Ft As String) As String
        Try

            Dim tax(), tax1() As String, yq As Integer = 0, tx As Integer = 0
            tax = OFareDS.Tables(0).Rows(0)("Adt_Tax").ToString.Split("#")
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    yq = yq + Convert.ToInt32(tax1(1))
                Else
                    tax1 = tax(i).Split(":")
                    tx = tx + Convert.ToInt32(tax1(1))
                End If
            Next
            Dim ORGTC As Integer = 0
            If OFareDS.Tables(0).Rows(0)("AdtFareType").ToString() = "Special Fare" Then
                ORGTC = (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("ADTAgentMrk")) * Adult) + (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("CHDAgentMrk")) * Child)
            Else
                ORGTC = OFareDS.Tables(0).Rows(0)("TC")
            End If


            Dim tcperpax As Integer = ORGTC / (Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Adult")) + Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Child")))

            Dim T_ID As String = ""
            If Ft = "OutBound" Then
                T_ID = "OB_FT"
                strFare = "<div id='" + T_ID + "' class='w100'>"
            ElseIf Ft = "InBound" Then
                T_ID = "IB_FT"
                strFare = "<div class='row' style='padding:10px 0px 0px 17px;'>"
                strFare = "<div id='" + T_ID + "' class='w100 lft'>"
            End If
            strFare = strFare & "<div class='lft w100'>"
            'strFare = strFare & "<div class='lft' style='width:100%;border-bottom: 1px solid #a2a2a2;'>"
            strFare = strFare & "<div class='pr ad1'>"
            strFare = strFare & "<div class='pr-l'>Adult x " & OFareDS.Tables(0).Rows(0)("Adult") & "</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & (OFareDS.Tables(0).Rows(0)("AdtBFare")) & "</span></div>"
            'strFare = strFare & "</hr>"
            strFare = strFare & "</div>"
            'strFare = strFare & "<div id='ad' style='display:none;'>"
            'strFare = strFare & "<div class='pr'>"
            'strFare = strFare & "<div class='pr-l'>Fuel Surcharge</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & yq & "</span></div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "<div class='pr'>"
            'strFare = strFare & "<div class='pr-l'>Tax</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & (tx + tcperpax) & "</span></div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "<div class='pr'>"
            'strFare = strFare & "<div class='pr-l'>Total</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & (OFareDS.Tables(0).Rows(0)("AdtFare")) + tcperpax & "</span></div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "</div>"



            If Child > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Chd_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(tax1(1))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(tax1(1))
                        End If
                    Next
                Catch ex As Exception
                End Try

                'strFare = strFare & "<div class='lft w100'>"
                strFare = strFare & "<div class='pr ch'>"
                strFare = strFare & "<div class='pr-l'>Child x " & OFareDS.Tables(0).Rows(0)("Child") & "</div>"
                strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("ChdBFare") & "</span></div>"
                strFare = strFare & "</div>"
                'strFare = strFare & "<div id='ch1' style='display:none;'>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Fuel Surcharge </div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & yq & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Tax</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & (tx + tcperpax) & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Total</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & (OFareDS.Tables(0).Rows(0)("ChdFare") + tcperpax) & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "</div>"
                ''strFare = strFare & "<div class='clear1'></div>"
                'strFare = strFare & "<div class='hr'></div>"
                'strFare = strFare & "</div>"
                ''strFare = strFare & "</div>"

            End If

            If Infant > 0 Then
                Try
                    yq = 0
                    tx = 0
                    tax = OFareDS.Tables(0).Rows(0)("Inf_Tax").ToString.Split("#")
                    For i As Integer = 0 To tax.Length - 2
                        If InStr(tax(i), "YQ") Then
                            tax1 = tax(i).Split(":")
                            yq = yq + Convert.ToInt32(tax1(1))
                        Else
                            tax1 = tax(i).Split(":")
                            tx = tx + Convert.ToInt32(tax1(1))
                        End If
                    Next
                Catch ex As Exception
                End Try
                'strFare = strFare & "<div class='  large-12 medium-12 small-12' style='background-color:#fff;padding-left:7px;line-height: 24px;' >"
                strFare = strFare & "<div class='pr inf'>"
                strFare = strFare & "<div class='pr-l'>Infant x " & OFareDS.Tables(0).Rows(0)("Infant") & "</div>"
                strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("InfBFare") & "</span></div>"
                strFare = strFare & "</div>"
                'strFare = strFare & "<div id='inf1' style='display:none;'>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Tax</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & tx & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Total</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("InfFare") & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='clear1'></div>"
                'strFare = strFare & "<div class='hr'></div>"
                'strFare = strFare & "</div>"
            End If



            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l total' >Total Taxes +</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & (tx + tcperpax) & "</span></div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("TotalTax") & "</span></div>"
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div id='tax' style='display:none;'>"
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>PHF</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>YQ</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("TotalFuelSur") & "</span></div>"
            strFare = strFare & "</div>"


            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>GST</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("SrvTax") & "</span></div>"
            strFare = strFare & "</div>"

    
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"

            strFare = strFare & "</div>"




            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l other' >Other Taxes +</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & (tx + tcperpax) & "</span></div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div id='otax' style='display:none;'>"
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>PHF</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"


            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>ASF</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>PSFF</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

          

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>Agency Mark-up (+)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"
            strFare = strFare & "</div>"

            strFare = strFare & "</div>"
          


            'strFare = strFare & "<div class='pr'>"
            ' ''strFare = strFare & "<div class='pr-l1' style='font-size:9px;'>Gross Total(" & Adult & " Adt," & Child & " Chd," & Infant & " Inf)</div>"
            'strFare = strFare & "<div class='pr-l' style='font-size:16px;color:red;' >Gross Total</div>"
            ''strFare = strFare & "<div style='border-bottom:1px solid #eee;'></div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("totFare") & "</span></div>"
            'strFare = strFare & "</div>"



            'strFare = strFare & "<div class='hr'></div>"
            'strFare = strFare & "</div>"

            'strFare = strFare & "</div>"
            'strFare = strFare & "<div class='clear1'></div>"
            'strFare = strFare & "</div>"
           

            'strFare = strFare & "<div class='large-12 medium-12 small-12' style='background-color:#fff;line-height: 24px;'>"
            'strFare = strFare & "<div class='pr'>"
            'strFare = strFare & "<div class='pr-l'>GST</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("SrvTax") & "</span></div>"
            'strFare = strFare & "</div>"
            If (OFareDS.Tables(0).Rows(0)("IsCorp") = True) Then
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Management Fee</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("TOTMGTFEE") & "</span></div>"
                'strFare = strFare & "</div>"
            Else
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Transaction Fee</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("TFee") & "</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div class='pr'>"
                'strFare = strFare & "<div class='pr-l'>Transaction Charge</div>"
                'strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
                'strFare = strFare & "</div>"
                'strFare = strFare & "<div>" & OFareDS.Tables(0).Rows(0)("TC") & "</div>"
                'If OFareDS.Tables(0).Rows(0)("AdtFareType").ToString() = "Special Fare" Then
                '    strFare = strFare & "<div class='rgt w30'>" & (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("ADTAgentMrk")) * Adult) + (Convert.ToDouble(OFareDS.Tables(0).Rows(0)("CHDAgentMrk")) * Child) & "</div>"
                'Else
                '    strFare = strFare & "<div class='rgt w30'>" & OFareDS.Tables(0).Rows(0)("TC") & "</div>"
                'End If
                'strFare = strFare & "</div>"
            End If

            If Ft = "OutBound" Then
                'strFare = strFare & "<div id='trtotfare' onmouseover=funcnetfare('block','trnetfare'); onmouseout=funcnetfare('none','trnetfare'); style='cursor:pointer;color: #004b91'>"
            ElseIf Ft = "InBound" Then
                'strFare = strFare & "<div id='trtotfareR'onmouseover=funcnetfare('block','trnetfareR'); onmouseout=funcnetfare('none','trnetfareR'); style='cursor:pointer;color: #004b91'>"
            End If
            strFare = strFare & "<div class='pr'>"
            ''strFare = strFare & "<div class='pr-l1' style='font-size:9px;'>Gross Total(" & Adult & " Adt," & Child & " Chd," & Infant & " Inf)</div>"
            strFare = strFare & "<div class='pr-l' style='font-size:16px;color:red;' >Gross Total</div>"
            'strFare = strFare & "<div style='border-bottom:1px solid #eee;'></div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("totFare") & "</span></div>"
            strFare = strFare & "</div>"
            If Ft = "OutBound" Then
                strFare = strFare & "<div id='trnetfare' class=''>"
            ElseIf Ft = "InBound" Then
                strFare = strFare & "<div id='trnetfareR' class=''  style='display:none;'>"
            End If
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l but' style='width: 54% !important;'>Commission Details (+)</div>"
            'strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("DisCount") & "</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div id='content' style='display:none;'>"
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>Commission(-)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("DisCount") & "</span></div>"
            strFare = strFare & "</div>"

            Dim Totaltds As Double = Convert.ToDouble(OFareDS.Tables(0).Rows(0)("AdtTds") * OFareDS.Tables(0).Rows(0)("Adult")) + Convert.ToDouble(OFareDS.Tables(0).Rows(0)("ChdTds") * OFareDS.Tables(0).Rows(0)("Child"))
            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>TDS(+)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & Totaltds & "</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>Company SGST(+)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>CompanyCGST(+)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>Company IGST(+)</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ 0</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l'>Markup Agent (+)</div>"
            Dim totalpax As Integer = Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Adult")) + Convert.ToInt32(OFareDS.Tables(0).Rows(0)("Child"))
            strFare = strFare & "<div class='pr-r'><span>₹ " + (totalpax * Convert.ToInt32(OFareDS.Tables(0).Rows(0)("ADTAgentMrk"))).ToString() + "</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "<div class='pr'>"
            strFare = strFare & "<div class='pr-l' style='font-size:16px;color:red;'>Net Fare</div>"
            strFare = strFare & "<div class='pr-r'><span>₹ " & OFareDS.Tables(0).Rows(0)("netFare") & "</span></div>"
            strFare = strFare & "</div>"

            strFare = strFare & "</div>"

           
            strFare = strFare & "</div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "</div>"
            strFare = strFare & "<div class='clear'></div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "</div>"
            'strFare = strFare & "</div>"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        'strFare = strPax & "</div>"


        Return strFare
    End Function
    Public Sub Bind_pax(ByVal cntAdult As Integer, ByVal cntChild As Integer, ByVal cntInfant As Integer)
        Try
            Dim PaxTbl As New DataTable()
            Dim cntTblColumn As DataColumn = Nothing
            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.Double")
            cntTblColumn.ColumnName = "Counter"
            PaxTbl.Columns.Add(cntTblColumn)

            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.String")
            cntTblColumn.ColumnName = "PaxTP"
            PaxTbl.Columns.Add(cntTblColumn)
            Dim cntrow As DataRow = Nothing
            For i As Integer = 1 To cntAdult
                cntrow = PaxTbl.NewRow()
                cntrow("Counter") = i
                cntrow("PaxTP") = "Passenger " & i.ToString() & " (Adult)"
                PaxTbl.Rows.Add(cntrow)
            Next
            Repeater_Adult.DataSource = PaxTbl
            Repeater_Adult.DataBind()


            PaxTbl.Clear()
            If cntChild > 0 Then

                For i As Integer = 1 To cntChild
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Child)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Child.DataSource = PaxTbl
                Repeater_Child.DataBind()
            End If


            PaxTbl.Clear()

            If cntInfant > 0 Then

                For i As Integer = 1 To cntInfant
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Infant)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Infant.DataSource = PaxTbl
                Repeater_Infant.DataBind()
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try



    End Sub
    Protected Sub book_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles book.Click
        Session("search_type") = "Flt"
        Dim AgencyDs As DataSet
        Dim OBFltDs, IBFltDs As DataSet
        Dim totFare As Double = 0
        Dim netFare As Double = 0
        Dim FltHdrO, FltHdrR As New DataSet
        Dim ProjectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
        Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)
        Dim CorpBillNo As String = Nothing
        Dim Prvdr As String = ""
        Dim PrvdrIB As String = ""

        OBFltDs = objDA.GetFltDtls(ViewState("OBTrackId"), Session("UID"))
        VCOB = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier") 'New Code 
        VCOBSPL = OBFltDs.Tables(0).Rows(0)("AdtFareType") 'New Code
        'TripOB = OBFltDs.Tables(0).Rows(0)("Trip") 'New Code 

        Prvdr = OBFltDs.Tables(0).Rows(0)("Provider")

        If Prvdr.Trim().ToUpper() = "TB" Or Prvdr.Trim().ToUpper() = "YA" Or Prvdr.Trim().ToUpper() = "AK" Then
            If OBFltDs.Tables(0).Rows(OBFltDs.Tables(0).Rows.Count - 1)("TripType").ToString().Trim() = "R" Then
                FLT_STAT = "RTF"
            End If
        Else
            FLT_STAT = OBFltDs.Tables(0).Rows(0)("FlightStatus")
        End If
        'New Code

        Dim BookingRefNo As String = ViewState("OBTrackId")
        If ViewState("FT") = "InBound" Then
            IBFltDs = objDA.GetFltDtls(ViewState("IBTrackId"), Session("UID"))
            VCIB = IBFltDs.Tables(0).Rows(0)("ValiDatingCarrier") 'New Code
            VCIBSPL = IBFltDs.Tables(0).Rows(0)("AdtFareType") 'New Code
            'TripIB = IBFltDs.Tables(0).Rows(0)("Trip") 'New Code
            PrvdrIB = IBFltDs.Tables(0).Rows(0)("Provider")
        End If
        AgencyDs = objDA.GetAgencyDetails(Session("UID"))

        Try
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
                        agentBal = objUMSvc.GetAgencyBal()

                        '' ''If Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) > netFare Then
                        ''If Convert.ToDouble(agentBal) > netFare Then

                        FltHdrO = objDA.GetHdrDetails(ViewState("OBTrackId"))
                        If FltHdrO.Tables.Count > 0 Then
                            If FltHdrO.Tables(0).Rows.Count = 0 Then
                                If Not IsDBNull(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                    If Convert.ToBoolean(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                        CorpBillNo = clsCorp.GenerateBillNoCorp("D")
                                    End If

                                End If
                                objDA.insertFltHdrDetails(OBFltDs, AgencyDs, Session("UID"), ddl_PGTitle.SelectedValue, txt_PGFName.Text, txt_PGLName.Text, txt_MobNo.Text.Trim(), txt_Email.Text, "D", ProjectId, BookedBy, CorpBillNo, "OutBound", BookingRefNo)
                                objDA.insertFlightDetails(OBFltDs)
                                objDA.insertFareDetails(OBFltDs, "D")
                                ''   InsertPaxDetail(ViewState("OBTrackId"), OBFltDs, "OB", VCOB)
                                Dim SeatL As String = ""
                                SeatL = seatSelect.Value
                                InsertPaxDetail(ViewState("OBTrackId"), OBFltDs, "OB", VCOB, SeatL)
                                'Dim gstcity As String = If(ddlCityGst.SelectedItem.Text = "--Select City --", "", ddlCityGst.SelectedItem.Text.Trim())
                                ' Dim gstState As String = If(ddlStateGst.SelectedItem.Text = "--Select State--", "", ddlStateGst.SelectedItem.Text.Trim())
                                Dim gstcity As String = If(GSTCityHid.Value = "Select City", "", GSTCityHid.Value.Trim())
                                Dim gstState As String = If(GSTStateHid.Value = "Select State", "", GSTStateHid.Value.Trim())
                                UpdateGSTDetail(OBFltDs.Tables(0).Rows(0)("Track_id").ToString(), txtGstNo.Text.Trim(), txtGstCmpName.Text.Trim(), txtGstAddress.Text & "/" & gstcity & "/" & gstState.Replace(" ", [String].Empty) & "/" & txtPinCode.Text.Trim(), txtGstPhone.Text.Trim(), txtGstEmail.Text.Trim(), txtRemarks.InnerText.Trim())

                                Try
                                    If Session("LoginByOTP") IsNot Nothing AndAlso Convert.ToString(Session("LoginByOTP")) <> "" AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                                        'Dim OTPRefNo As String = "OTP" + DateTime.Now.ToString("yyyyMMddHHmmssffffff").Substring(7, 13)
                                        Dim UserId As String = Session("UID")
                                        Dim Remark As String = txtRemarks.InnerText.Trim()
                                        Dim OTPRefNo As String = OBFltDs.Tables(0).Rows(0)("Track_id").ToString()
                                        Dim LoginByOTP As String = Session("LoginByOTP")
                                        Dim OTPId As String = Session("OTPID")
                                        Dim ServiceType As String = "FLIGHT-TICKET-DOM"
                                        Dim flag As Integer = 0
                                        Dim OTPST As New SqlTransaction
                                        flag = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType)
                                    End If
                                Catch ex As Exception
                                    'clsErrorLog.LogInfo(ex)
                                    EXCEPTION_LOG.ErrorLog.FileHandling("FLIGHT-DOM", "Error_102", ex, "FlightDom-CustomerInfo")
                                End Try


                                '''' insert farerule
                                Try

                                    Dim ObjFRule As New GALWS.InsertFareRule
                                    ObjFRule.GetFareRule(OBFltDs, ViewState("OBTrackId"))

                                    'Dim obj As New STD.BAL.TBO.TBOFareRule()
                                    'Dim objFareRule As STD.BAL.TBO.FareRuleResponse = obj.GetFareRule("", OBFltDs.Tables(0).Rows(0)("sno"))
                                    'objbal.AddFareRule(ViewState("OBTrackId"), objFareRule.Response.FareRules(0).FareRuleDetail)

                                Catch ex As Exception

                                End Try
                                Dim sellcheck As String = ""
                                If ((VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8"  Or VCOB = "IX") And InStr(VCOBSPL, "Special") = 0) Or Prvdr.Trim().ToUpper() = "TB" Or Prvdr.Trim().ToUpper() = "YA" Or Prvdr.Trim().ToUpper() = "AK" Then

                                    Dim Paxdt As DataSet = objDA.Get_MEAL_BAG_PaxDetails(ViewState("OBTrackId").ToString())
                                    Insert_MEAL_BAG_Detail(ViewState("OBTrackId"), OBFltDs, Paxdt, "OB", "O")

                                    If FLT_STAT = "RTF" Then 'New Code
                                        Insert_MEAL_BAG_Detail(ViewState("OBTrackId"), OBFltDs, Paxdt, "IB", "R")
                                    End If
                                     If Prvdr.Trim().ToUpper() = "AK" And txtGstNo.Text.Trim() <> "" Then
                                        Dim SNNO() As String = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                                        Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "D")
                                        Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                                        Dim sessionvalue As String = objAirAsia.UpdateContactsGST(dsCrd, OBFltDs.Tables(0).Rows(0)("Searchvalue").ToString(), OBFltDs.Tables(0).Rows(0)("Track_id").ToString(), txtGstCmpName.Text.Trim(), txtGstNo.Text.Trim(), txtGstEmail.Text.Trim(), txtGstPhone.Text.Trim(), txtGstAddress.Text & "/" & gstcity & "/" & gstState & "/" & txtPinCode.Text.Trim(), Constr, SNNO(7), SNNO(8))

                                        If (sessionvalue <> OBFltDs.Tables(0).Rows(0)("Searchvalue").ToString()) Then
                                            For Each strow In OBFltDs.Tables(0).Rows
                                                If strow.Item("Track_id").ToString = OBFltDs.Tables(0).Rows(0)("Track_id").ToString() Then
                                                    strow.Item("Searchvalue") = sessionvalue
                                                    OBFltDs.AcceptChanges()
                                                End If
                                            Next
                                        End If
                                    End If

                                    sellcheck = SELL_SSR(ViewState("OBTrackId"))
                                    Dim seatlist As List(Of MySeatDetails) = New List(Of MySeatDetails)()
                                    If String.IsNullOrEmpty(SeatL) = False Then seatlist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of MySeatDetails))(SeatL)
                                    UpadteSeatFare(seatlist, ViewState("OBTrackId"))
                                End If

                                If ViewState("FT") = "InBound" Then
                                    FltHdrR = objDA.GetHdrDetails(ViewState("IBTrackId"))
                                    If FltHdrR.Tables.Count > 0 Then
                                        If FltHdrR.Tables(0).Rows.Count = 0 Then
                                            If Not IsDBNull(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                                If Convert.ToBoolean(AgencyDs.Tables(0).Rows(0)("IsCorp")) Then
                                                    CorpBillNo = clsCorp.GenerateBillNoCorp("D")
                                                End If

                                            End If
                                            objDA.insertFltHdrDetails(IBFltDs, AgencyDs, Session("UID"), ddl_PGTitle.SelectedValue, txt_PGFName.Text, txt_PGLName.Text, txt_MobNo.Text.Trim(), txt_Email.Text, "D", ProjectId, BookedBy, CorpBillNo, "InBound", BookingRefNo)
                                            objDA.insertFlightDetails(IBFltDs)
                                            objDA.insertFareDetails(IBFltDs, "D")
                                            ''   InsertPaxDetail(ViewState("IBTrackId"), IBFltDs, "IB", VCIB)
                                            Dim SeatR As String = ""
                                            SeatR = SeatDetails_ibDtls.Value
                                            InsertPaxDetail(ViewState("IBTrackId"), IBFltDs, "IB", VCIB, SeatR)

                                            ''If h2 > 0 Then
                                            ' Dim gstcity1 As String = If(ddlCityGst.SelectedItem.Text = "Select City", "", ddlCityGst.SelectedItem.Text.Trim())
                                            'Dim gstState1 As String = If(ddlStateGst.SelectedItem.Text = "Select State", "", ddlStateGst.SelectedItem.Text.Trim())
                                            Dim gstcity1 As String = If(GSTCityHid.Value = "Select City", "", GSTCityHid.Value.Trim())
                                            Dim gstState1 As String = If(GSTStateHid.Value = "Select State", "", GSTStateHid.Value.Trim())

                                            UpdateGSTDetail(IBFltDs.Tables(0).Rows(0)("Track_id").ToString(), txtGstNo.Text.Trim(), txtGstCmpName.Text.Trim(), txtGstAddress.Text & "/" & gstcity1 & "/" & gstState1.Replace(" ", [String].Empty) & "/" & txtPinCode.Text.Trim(), txtGstPhone.Text.Trim(), txtGstEmail.Text.Trim(), txtRemarks.InnerText.Trim())


                                            Try
                                                If Session("LoginByOTP") IsNot Nothing AndAlso Convert.ToString(Session("LoginByOTP")) <> "" AndAlso Convert.ToString(Session("LoginByOTP")) = "true" Then
                                                    'Dim OTPRefNo As String = "OTP" + DateTime.Now.ToString("yyyyMMddHHmmssffffff").Substring(7, 13)
                                                    Dim UserId As String = Session("UID")
                                                    Dim Remark As String = txtRemarks.InnerText.Trim()
                                                    Dim OTPRefNo As String = IBFltDs.Tables(0).Rows(0)("Track_id").ToString()
                                                    Dim LoginByOTP As String = Session("LoginByOTP")
                                                    Dim OTPId As String = Session("OTPID")
                                                    Dim ServiceType As String = "FLIGHT-TICKET-DOM"
                                                    Dim flag As Integer = 0
                                                    Dim OTPST As New SqlTransaction
                                                    flag = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType)
                                                End If
                                            Catch ex As Exception
                                                'clsErrorLog.LogInfo(ex)
                                                EXCEPTION_LOG.ErrorLog.FileHandling("FLIGHT-DOM", "Error_102", ex, "FlightDom-CustomerInfo")
                                            End Try

                                            '''' insert farerule
                                            Try

                                                Dim ObjFRule As New GALWS.InsertFareRule
                                                ObjFRule.GetFareRule(IBFltDs, ViewState("IBTrackId"))

                                                'Dim obj As New STD.BAL.TBO.TBOFareRule()
                                                'Dim objFareRule As STD.BAL.TBO.FareRuleResponse = obj.GetFareRule("", IBFltDs.Tables(0).Rows(0)("sno"))
                                                'objbal.AddFareRule(ViewState("IBTrackId"), objFareRule.Response.FareRules(0).FareRuleDetail)

                                            Catch ex As Exception

                                            End Try


                                            If (VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8") And InStr(VCIBSPL, "Special") = 0 Or PrvdrIB.Trim().ToUpper() = "TB" Or PrvdrIB.Trim().ToUpper() = "YA" Or PrvdrIB.Trim().ToUpper() = "AK" Then
                                                Dim PaxdtRT As DataSet = objDA.Get_MEAL_BAG_PaxDetails(ViewState("IBTrackId").ToString())
                                                Insert_MEAL_BAG_Detail(ViewState("IBTrackId"), IBFltDs, PaxdtRT, "IB", "O") 'New Code
                                                If PrvdrIB.Trim().ToUpper() = "AK" And txtGstNo.Text.Trim() <> "" Then
                                                    Dim SNNO() As String = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                                                    Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "D")
                                                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                                                    Dim sessionvalue As String = objAirAsia.UpdateContactsGST(dsCrd, IBFltDs.Tables(0).Rows(0)("Searchvalue").ToString(), IBFltDs.Tables(0).Rows(0)("Track_id").ToString(), txtGstCmpName.Text.Trim(), txtGstNo.Text.Trim(), txtGstEmail.Text.Trim(), txtGstPhone.Text.Trim(), txtGstAddress.Text & "/" & gstcity & "/" & gstState & "/" & txtPinCode.Text.Trim(), Constr, SNNO(7), SNNO(8))

                                                End If
                                                sellcheck = SELL_SSR(ViewState("IBTrackId"))
                                                Dim seatlistR As List(Of MySeatDetails) = New List(Of MySeatDetails)()
                                                If String.IsNullOrEmpty(SeatR) = False Then seatlistR = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of MySeatDetails))(SeatR)
                                                UpadteSeatFare(seatlistR, ViewState("IBTrackId"))
                                            End If
                                            ' Response.Redirect("../Domestic/PriceDetails.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "")
                                            If sellcheck <> "FAILURE" Then
                                                Response.Redirect("../Domestic/PriceDetails.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                            Else
                                                Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                                            End If
                                        Else
                                            ''Dim um As String = ""
                                            ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                            ''Response.Redirect(um & "?msg=1")
                                            Response.Redirect("../International/BookingMsg.aspx?msg=1")
                                        End If
                                    Else
                                        ''Dim um As String = ""
                                        ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                        ''Response.Redirect(um & "?msg=2")
                                        Response.Redirect("../International/BookingMsg.aspx?msg=2")
                                    End If
                                Else
                                    If sellcheck <> "FAILURE" Then
                                        Response.Redirect("../Domestic/PriceDetails.aspx?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    Else
                                        Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                                    End If
                                    ' Response.Redirect("../Domestic/PriceDetails.aspx?OBTID=" & ViewState("OBTrackId") & "&FT=" & ViewState("FT") & "")
                                End If
                            Else
                                ''Dim um As String = ""
                                ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                ''Response.Redirect(um & "?msg=1")
                                Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
                            End If
                        Else
                            ''Dim um As String = ""
                            ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                            ''Response.Redirect(um & "?msg=2")
                            Response.Redirect("../International/BookingMsg.aspx?msg=2", False)
                        End If
                        'Else
                        '    ''Dim um As String = ""
                        '    ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        '    ''Response.Redirect(um & "?msg=CL")
                        '    Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                        'End If
                    Else
                        ''Dim um As String = ""
                        ''um = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                        ''Response.Redirect(um & "?msg=NA")
                        Response.Redirect("../International/BookingMsg.aspx?msg=NA")
                    End If
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Function UpadteG8Fare(ByVal trackId As String) As String

        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(trackId, "")
        Dim BgPr As Decimal = 0
        If (MBDT.Tables(0).Rows.Count > 0) Then
            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("SeatPrice"))
            Next
        End If
        Return objSql.Update_NET_TOT_Fare(trackId, (BgPr).ToString())


    End Function
    Public Sub InsertPaxDetail(ByVal trackid As String, ByVal FltDs As DataSet, ByVal Trip As String, ByVal VCOB As String, ByVal SeatReq As String)
        Try
            Adult = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult"))
            Child = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child"))
            Infant = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant"))

            Dim seatlist As List(Of MySeatDetails) = New List(Of MySeatDetails)()
            If String.IsNullOrEmpty(SeatReq) = False Then seatlist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of MySeatDetails))(SeatReq)

            Dim seat As String = ""
            Dim counter As Integer = 0

            For Each rw As RepeaterItem In Repeater_Adult.Items
                counter += 1
                ''New Code
                Dim ddl_AMealPrefer As DropDownList
                Dim ddl_ASeatPrefer As DropDownList
                Dim txt_AAirline As HtmlInputHidden
                Dim txt_ANumber As TextBox
                '''
                Dim ddl_ATitle As DropDownList = DirectCast(rw.FindControl("ddl_ATitle"), DropDownList)
                Dim ddl_AGender As DropDownList = DirectCast(rw.FindControl("ddl_AGender"), DropDownList)
                Dim txtAFirstName As TextBox = DirectCast(rw.FindControl("txtAFirstName"), TextBox)
                Dim txtAMiddleName As TextBox = DirectCast(rw.FindControl("txtAMiddleName"), TextBox)
                If txtAMiddleName.Text = "Middle Name" Then
                    txtAMiddleName.Text = ""
                End If
                Dim txtALastName As TextBox = DirectCast(rw.FindControl("txtALastName"), TextBox)
                Dim gender As String = "F"

                If ddl_ATitle.SelectedValue.Trim.ToLower = "dr" Or ddl_ATitle.SelectedValue.Trim.ToLower = "prof" Then
                    gender = ddl_AGender.SelectedValue.Trim

                ElseIf ddl_ATitle.SelectedValue.Trim.ToLower = "mr" Then
                    gender = "M"

                End If

                'Dim ddl_ADate As DropDownList = DirectCast(rw.FindControl("ddl_ADate"), DropDownList)
                'Dim ddl_AMonth As DropDownList = DirectCast(rw.FindControl("ddl_AMonth"), DropDownList)
                Dim txtadultDOB As TextBox = DirectCast(rw.FindControl("Txt_AdtDOB"), TextBox)
                Dim DOB As String = ""
                DOB = txtadultDOB.Text.Trim
                If VCOB = "IX" Then
                    If (Not (Trip = "IB")) Then
                        ddl_AMealPrefer = DirectCast(rw.FindControl("Ddl_A_EB_Ob"), DropDownList)
                        ddl_ASeatPrefer = DirectCast(rw.FindControl("ddl_ASeatPrefer"), DropDownList)
                        txt_AAirline = DirectCast(rw.FindControl("hidtxtAirline_Dom"), HtmlInputHidden)
                        txt_ANumber = DirectCast(rw.FindControl("txt_ANumber"), TextBox)
                    Else
                        ddl_AMealPrefer = DirectCast(rw.FindControl("ddl_AMealPrefer_R"), DropDownList)
                        ddl_ASeatPrefer = DirectCast(rw.FindControl("ddl_ASeatPrefer_R"), DropDownList)
                        txt_AAirline = DirectCast(rw.FindControl("hidtxtAirline_R_Dom"), HtmlInputHidden)
                        txt_ANumber = DirectCast(rw.FindControl("txt_ANumber_R"), TextBox)
                    End If
                Else
                    If (Not (Trip = "IB")) Then
                        ddl_AMealPrefer = DirectCast(rw.FindControl("ddl_AMealPrefer"), DropDownList)
                        ddl_ASeatPrefer = DirectCast(rw.FindControl("ddl_ASeatPrefer"), DropDownList)
                        txt_AAirline = DirectCast(rw.FindControl("hidtxtAirline_Dom"), HtmlInputHidden)
                        txt_ANumber = DirectCast(rw.FindControl("txt_ANumber"), TextBox)
                    Else
                        ddl_AMealPrefer = DirectCast(rw.FindControl("ddl_AMealPrefer_R"), DropDownList)
                        ddl_ASeatPrefer = DirectCast(rw.FindControl("ddl_ASeatPrefer_R"), DropDownList)
                        txt_AAirline = DirectCast(rw.FindControl("hidtxtAirline_R_Dom"), HtmlInputHidden)
                        txt_ANumber = DirectCast(rw.FindControl("txt_ANumber_R"), TextBox)
                    End If
                End If



                If txt_AAirline.Value = "Airline" Then
                    txt_AAirline.Value = ""
                End If
                If txt_ANumber.Text = "Number" Then
                    txt_ANumber.Text = ""
                End If
                Dim seatlistO As List(Of MySeatDetails) = New List(Of MySeatDetails)()

                seatlistO = seatlist.Where(Function(s) s.Title = ddl_ATitle.SelectedValue And s.FName = txtAFirstName.Text.Trim() And s.LName = txtALastName.Text.Trim()).ToList()
                For s As Integer = 0 To seatlistO.Count - 1
                    seat = seat & seatlistO(s).Sector & " - " + seatlistO(s).Seat & " - " + seatlistO(s).SeatAlignment & ","
                Next

                If (String.IsNullOrEmpty(seat)) Then
                    If counter <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                         "true", "", gender, "", "", "", "")
                    Else

                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, ddl_ASeatPrefer.SelectedValue, _
                         "false", "", gender, "", "", "", "")

                    End If
                Else
                    If counter <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, seat, _
                         "true", "", gender, "", "", "", "")
                    Else

                        objDA.insertPaxDetails(trackid, ddl_ATitle.SelectedValue, txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), _
                         "ADT", DOB, txt_ANumber.Text.Trim(), txt_AAirline.Value.Trim(), ddl_AMealPrefer.SelectedValue, seat, _
                         "false", "", gender, "", "", "", "")

                    End If
                End If
                Dim objs As STD.BAL.FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                If seatlist.Count > 0 Then
                    Dim Fullname As String = ""
                    For s As Integer = 0 To seatlistO.Count - 1
                        Dim SeatIns As STD.Shared.Seat = New STD.Shared.Seat()
                        '' SeatIns.PaxId = PaxId
                        SeatIns.OrderId = trackid
                        SeatIns.SeatDesignator = seatlistO(s).Seat
                        SeatIns.Amount = Convert.ToInt32(seatlistO(s).Amount)
                        SeatIns.Origin = seatlistO(s).Sector.Split("-"c)(0)
                        SeatIns.Destination = seatlistO(s).Sector.Split("-"c)(1)
                        SeatIns.FlightNumber = seatlistO(s).FlightNumber
                        SeatIns.FlightTime = seatlistO(s).FlightTime
                        SeatIns.VC = VCOB
                        SeatIns.SeatAlignment = seatlistO(s).SeatAlignment
                        SeatIns.OptionalServiceRef = seatlistO(s).OptionalServiceRef
                        SeatIns.Group = seatlistO(s).Group
                        SeatIns.ClassOfService = seatlistO(s).ClassOfService
                        SeatIns.Equipment = seatlistO(s).Equipment
                        SeatIns.Carrier = seatlistO(s).Carrier
                        SeatIns.Paid = seatlistO(s).Paid
                        Fullname = seatlistO(s).Title & seatlistO(s).FName & seatlistO(s).LName
                        objs.insertSeat(SeatIns, Fullname)
                    Next
                End If
            Next

            If Child > 0 Then
                For Each rw As RepeaterItem In Repeater_Child.Items
                    Dim ddl_CMealPrefer As DropDownList
                    Dim ddl_CSeatPrefer As DropDownList

                    Dim ddl_CTitle As DropDownList = DirectCast(rw.FindControl("ddl_CTitle"), DropDownList)
                    Dim txtCFirstName As TextBox = DirectCast(rw.FindControl("txtCFirstName"), TextBox)
                    Dim txtCMiddleName As TextBox = DirectCast(rw.FindControl("txtCMiddleName"), TextBox)
                    If txtCMiddleName.Text = "Middle Name" Then
                        txtCMiddleName.Text = ""
                    End If
                    Dim txtCLastName As TextBox = DirectCast(rw.FindControl("txtCLastName"), TextBox)

                    Dim txtchildDOB As TextBox = DirectCast(rw.FindControl("Txt_chDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtchildDOB.Text.Trim

                    Dim gender As String = "F"

                    If ddl_CTitle.SelectedValue.Trim.ToLower = "mstr" Then
                        gender = "M"

                    End If

                    If (Not (Trip = "IB")) Then
                        ddl_CMealPrefer = DirectCast(rw.FindControl("ddl_CMealPrefer"), DropDownList)
                        ddl_CSeatPrefer = DirectCast(rw.FindControl("ddl_CSeatPrefer"), DropDownList)

                    Else
                        ddl_CMealPrefer = DirectCast(rw.FindControl("ddl_CMealPrefer_R"), DropDownList)
                        ddl_CSeatPrefer = DirectCast(rw.FindControl("ddl_CSeatPrefer_R"), DropDownList)
                    End If
                    Dim seatlistO As List(Of MySeatDetails) = New List(Of MySeatDetails)()
                    seatlistO = seatlist.Where(Function(s) s.Title = ddl_CTitle.SelectedValue And s.FName = txtCFirstName.Text.Trim() And s.LName = txtCLastName.Text.Trim()).ToList()
                    For s As Integer = 0 To seatlistO.Count - 1
                        seat = seat & seatlistO(s).Sector & " - " + seatlistO(s).Seat & " - " + seatlistO(s).SeatAlignment & ","
                    Next

                    If (String.IsNullOrEmpty(seat)) Then

                        objDA.insertPaxDetails(trackid, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), _
                         "CHD", DOB, "", "", ddl_CMealPrefer.SelectedValue, ddl_CSeatPrefer.SelectedValue, _
                         "false", "", gender, "", "", "", "")
                    Else
                        objDA.insertPaxDetails(trackid, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), _
                       "CHD", DOB, "", "", ddl_CMealPrefer.SelectedValue, seat, _
                       "false", "", gender, "", "", "", "")
                    End If
                    Dim objs As STD.BAL.FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                    If seatlist.Count > 0 Then
                        Dim Fullname As String = ""
                        For s As Integer = 0 To seatlistO.Count - 1
                            Dim SeatIns As STD.Shared.Seat = New STD.Shared.Seat()
                            '' SeatIns.PaxId = PaxId
                            SeatIns.OrderId = trackid
                            SeatIns.SeatDesignator = seatlistO(s).Seat
                            SeatIns.Amount = Convert.ToInt32(seatlistO(s).Amount)
                            SeatIns.Origin = seatlistO(s).Sector.Split("-"c)(0)
                            SeatIns.Destination = seatlistO(s).Sector.Split("-"c)(1)
                            SeatIns.FlightNumber = seatlistO(s).FlightNumber
                            SeatIns.FlightTime = seatlistO(s).FlightTime
                            SeatIns.VC = VCOB
                            SeatIns.SeatAlignment = seatlistO(s).SeatAlignment
                            SeatIns.OptionalServiceRef = seatlistO(s).OptionalServiceRef
                            SeatIns.Group = seatlistO(s).Group
                            SeatIns.ClassOfService = seatlistO(s).ClassOfService
                            SeatIns.Equipment = seatlistO(s).Equipment
                            SeatIns.Carrier = seatlistO(s).Carrier
                            SeatIns.Paid = seatlistO(s).Paid
                            Fullname = seatlistO(s).Title & seatlistO(s).FName & seatlistO(s).LName
                            objs.insertSeat(SeatIns, Fullname)
                        Next
                    End If
                Next
            End If

            If Infant > 0 Then
                Dim counter1 As Integer = 0
                For Each rw As RepeaterItem In Repeater_Infant.Items

                    Dim ddl_ITitle As DropDownList = DirectCast(rw.FindControl("ddl_ITitle"), DropDownList)
                    Dim txtIFirstName As TextBox = DirectCast(rw.FindControl("txtIFirstName"), TextBox)
                    Dim txtIMiddleName As TextBox = DirectCast(rw.FindControl("txtIMiddleName"), TextBox)
                    If txtIMiddleName.Text = "Middle Name" Then
                        txtIMiddleName.Text = ""
                    End If
                    Dim txtILastName As TextBox = DirectCast(rw.FindControl("txtILastName"), TextBox)

                    Dim txtinfantDOB As TextBox = DirectCast(rw.FindControl("Txt_InfantDOB"), TextBox)
                    Dim DOB As String = ""
                    DOB = txtinfantDOB.Text.Trim
                    Dim gender As String = "F"
                    If ddl_ITitle.SelectedValue.Trim.ToLower = "mstr" Then
                        gender = "M"

                    End If
                    Dim txtAFirstName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAFirstName"), TextBox)
                    Dim txtAMiddleName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtAMiddleName"), TextBox)
                    Dim txtALastName As TextBox = DirectCast(Repeater_Adult.Items(counter1).FindControl("txtALastName"), TextBox)

                    Dim Name As String = ""
                    Name = txtAFirstName.Text.Trim() + txtAMiddleName.Text.Trim() + txtALastName.Text.Trim()
                    If counter1 <= Infant Then
                        objDA.insertPaxDetails(trackid, ddl_ITitle.SelectedValue, txtIFirstName.Text.Trim(), txtIMiddleName.Text.Trim(), txtILastName.Text.Trim(),
                         "INF", DOB, "", "", "", "",
                         "false", Name, gender, "", "", "", "")
                    End If
                    counter1 += 1
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Protected Sub Repeater_Adult_ItemCreated(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim Flight As Char = "1" 'For RoundTrip SpecialCase'
        Adti = Adti + 1

        If (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And InStr(VCOBSPL, "Special") = 0 Then ''And Provider.Trim.ToUpper <> "TB" And Provider.Trim.ToUpper <> "YA" Then
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)
                If VCOB = "SG" And (SERCHVALO.Contains("P1") Or SERCHVALO.Contains("P2")) Then
                    ddl.Items.Add(New ListItem("Hand Bag Only 7kg--INR0", "HBAG" + Adti.ToString()))
                    ddl.Items.Add(New ListItem("First Check-in Baggage 15kg--INR750", "CBAG" + Adti.ToString()))
                Else
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        ddl.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                End If

                'ddl.AutoPostBack = True
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ds.Clear()
                ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl2.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl2.DataBind()

                div_ADT.Style("Display") = "block"

            Catch ex As Exception

            End Try


        ElseIf Provider.Trim.ToUpper = "TB" Then

            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage) = objmB.GetTBOBaggage(TBOSSR, "O")


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next


                'ddl.AutoPostBack = True
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ds.Clear()
                ' ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)


                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic) = objmB.GetTBOMeals(TBOSSR, "O")

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next
                ddl2.DataBind()

                div_ADT.Style("Display") = "block"

            Catch ex As Exception

            End Try


        ElseIf Provider.Trim.ToUpper = "YA" Then

            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                '' Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)
                Dim objAirPrice As New STD.BAL.YAAirPrice()

                Dim baglist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Baggage, YASSR)


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Desc.ToString() + ")" + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Desc.ToString() + "_" + baglist(i).SSRType.ToString() + "_" + baglist(i).Desc.ToString() + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next


                'ddl.AutoPostBack = True
                ddl.DataBind()

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ''ds.Clear()
                ' ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)


                Dim Meallist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Meal, YASSR)

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).Desc.ToString() + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Desc.ToString() + "_" + Meallist(i).SSRType.ToString() + "_" + Meallist(i).Desc.ToString() + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                Next
                ddl2.DataBind()

                div_ADT.Style("Display") = "block"

            Catch ex As Exception

            End Try

        ElseIf VCOB = "IX" Then

            'Goair Meal
            Dim objG8FQ As New STD.BAL.G8FareQuote("", "", "", "")
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)
                ' Dim div_mealO_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealO_ADT"), HtmlControls.HtmlGenericControl)
                'Dim div_mealOD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealOD_ADT"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                '' Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)
                'div_mealO_ADT.Style("Display") = "none"
                'div_mealOD_ADT.Style("Display") = "none"


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                Dim G8BagListO As New List(Of G8ServiceQuoteResponse)()


                G8BagListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Baggage)
                For i As Integer = 0 To G8BagListO.Count - 1
                    ddl.Items.Add(New ListItem(G8BagListO(i).Description + "--INR" + G8BagListO(i).AmountWithTax.ToString(), G8BagListO(i).CodeType + ":" + G8BagListO(i).SSRCategory + "_" + G8BagListO(i).ServiceID + ":" + G8BagListO(i).Description + ":" + Adti.ToString() + ":" + G8BagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()
                'If G8BagListO.Count > 0 Then
                '    div_ADT.Style("Display") = "block"
                'Else
                '    'div_ADT.Style("Display") = "none"
                'End If

                ''''''''''''''''''''''meal'''''''''''''''''''''''''''

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))

                Dim G8MealListO As New List(Of G8ServiceQuoteResponse)()


                G8MealListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Meal)
                For i As Integer = 0 To G8MealListO.Count - 1
                    ddl2.Items.Add(New ListItem(G8MealListO(i).Description + "--INR" + G8MealListO(i).AmountWithTax.ToString(), G8MealListO(i).CodeType + ":" + G8MealListO(i).SSRCategory + "_" + G8MealListO(i).ServiceID + ":" + G8MealListO(i).Description + ":" + Adti.ToString() + ":" + G8MealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl2.DataBind()
                'If G8MealListO.Count > 0 Then
                '    div_ADT.Style("Display") = "block"
                '    'div_mealO_ADT.Style("Display") = "block"
                '    'div_mealOD_ADT.Style("Display") = "block"
                'Else
                '    div_ADT.Style("Display") = "none"
                'End If

                ''''''''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                'Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_SE_Ob"), DropDownList)
                'ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))

                'Dim G8SeatListO As New List(Of G8ServiceQuoteResponse)()


                'G8SeatListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Seat)
                'For i As Integer = 0 To G8SeatListO.Count - 1
                '    ddl3.Items.Add(New ListItem(G8SeatListO(i).Description + "--INR" + G8SeatListO(i).AmountWithTax.ToString(), G8SeatListO(i).CodeType + ":" + G8SeatListO(i).SSRCategory + "_" + G8SeatListO(i).ServiceID + ":" + G8SeatListO(i).Description + ":" + Adti.ToString() + ":" + G8SeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                'Next

                'ddl3.DataBind()
                'If G8SeatListO.Count > 0 Then
                '    div_ADT.Style("Display") = "block"

                'Else
                '    div_ADT.Style("Display") = "none"
                'End If

                If G8BagListO.Count > 0 Or G8MealListO.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                Else
                    'div_ADT.Style("Display") = "none"
                End If



                If G8SSRListR.Count > 0 Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                        DirectCast(e.Item.FindControl("tranchor1_R"), HtmlGenericControl).Style.Add("display", "none")
                        DirectCast(e.Item.FindControl("A_ALL_R"), HtmlGenericControl).Style.Add("display", "none")
                        'Dim div_mealR_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealR_ADT"), HtmlControls.HtmlGenericControl)
                        'Dim div_mealRD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealRD_ADT"), HtmlControls.HtmlGenericControl)

                        'div_mealR_ADT.Style("Display") = "none"
                        'div_mealRD_ADT.Style("Display") = "none"

                        ' Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                        Dim G8BagListR As New List(Of G8ServiceQuoteResponse)()
                        G8BagListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Baggage)

                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To G8BagListR.Count - 1
                            ddl_Ib.Items.Add(New ListItem(G8BagListR(i).Description + "--INR" + G8BagListR(i).AmountWithTax.ToString(), G8BagListR(i).CodeType + ":" + G8BagListR(i).SSRCategory + "_" + G8BagListR(i).ServiceID + ":" + G8BagListR(i).Description + ":" + Adti.ToString() + ":" + G8BagListR(i).Amount.ToString()))
                        Next
                        ddl_Ib.DataBind()
                        'div_Ib.Style("Display") = "Display:block"

                        ''''''''''''''''''''''''''''''''''''''''''meal'''''''''''''''''''''''''''''''''''''''''''
                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                        Dim G8MealListR As New List(Of G8ServiceQuoteResponse)()
                        G8MealListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Meal)

                        ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                        For i As Integer = 0 To G8MealListR.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(G8MealListR(i).Description + "--INR" + G8MealListR(i).AmountWithTax.ToString(), G8MealListR(i).CodeType + ":" + G8MealListR(i).SSRCategory + "_" + G8MealListR(i).ServiceID + ":" + G8MealListR(i).Description + ":" + Adti.ToString() + ":" + G8MealListR(i).Amount.ToString()))
                        Next
                        ddl2_Ib.DataBind()


                        'If G8MealListR.Count > 0 Then
                        '    div_Ib.Style("Display") = "block"
                        '    'div_mealRD_ADT.Style("Display") = "block"
                        '    ddl2_Ib.Style("Display") = "Display:block"
                        'Else
                        '    'div_mealR_ADT.Style("Display") = "none"
                        '    'div_mealRD_ADT.Style("Display") = "none"
                        '    ddl2_Ib.Style("Display") = "Display:none"


                        'End If


                        ''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''
                        'Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_SE_Ib"), DropDownList)
                        'Dim G8SeatListR As New List(Of G8ServiceQuoteResponse)()
                        'G8SeatListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Seat)

                        'ddl3_Ib.Items.Add(New ListItem("---Select Seat Options---", "select"))
                        'For i As Integer = 0 To G8SeatListR.Count - 1
                        '    ddl3_Ib.Items.Add(New ListItem(G8SeatListR(i).Description + "--INR" + G8SeatListR(i).AmountWithTax.ToString(), G8SeatListR(i).CodeType + ":" + G8SeatListR(i).SSRCategory + "_" + G8SeatListR(i).ServiceID + ":" + G8SeatListR(i).Description + ":" + Adti.ToString() + ":" + G8SeatListR(i).Amount.ToString()))
                        'Next
                        'ddl3_Ib.DataBind()


                        If G8MealListR.Count > 0 Or G8BagListR.Count > 0 Then
                            div_Ib.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            ddl2_Ib.Style("Display") = "Display:block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            ddl2_Ib.Style("Display") = "Display:none"


                        End If

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
            'End Goair Meal
        ElseIf VCOB = "G88" Then

            'Goair Meal
            Dim objG8FQ As New STD.BAL.G8FareQuote("", "", "", "")
            Try
                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)
                ' Dim div_mealO_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealO_ADT"), HtmlControls.HtmlGenericControl)
                'Dim div_mealOD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealOD_ADT"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)
                '' Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)
                'div_mealO_ADT.Style("Display") = "none"
                'div_mealOD_ADT.Style("Display") = "none"


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                Dim G8BagListO As New List(Of G8ServiceQuoteResponse)()


                G8BagListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Baggage)
                For i As Integer = 0 To G8BagListO.Count - 1
                    ddl.Items.Add(New ListItem(G8BagListO(i).Description + "--INR" + G8BagListO(i).AmountWithTax.ToString(), G8BagListO(i).CodeType + ":" + G8BagListO(i).SSRCategory + "_" + G8BagListO(i).ServiceID + ":" + G8BagListO(i).Description + ":" + Adti.ToString() + ":" + G8BagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()
                If G8BagListO.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                Else
                    'div_ADT.Style("Display") = "none"
                End If

                ''''''''''''''''''''''meal'''''''''''''''''''''''''''

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))

                Dim G8MealListO As New List(Of G8ServiceQuoteResponse)()


                G8MealListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Meal)
                For i As Integer = 0 To G8MealListO.Count - 1
                    ddl2.Items.Add(New ListItem(G8MealListO(i).Description + "--INR" + G8MealListO(i).AmountWithTax.ToString(), G8MealListO(i).CodeType + ":" + G8MealListO(i).SSRCategory + "_" + G8MealListO(i).ServiceID + ":" + G8MealListO(i).Description + ":" + Adti.ToString() + ":" + G8MealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl2.DataBind()
                If G8MealListO.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                    'div_mealO_ADT.Style("Display") = "block"
                    'div_mealOD_ADT.Style("Display") = "block"
                Else
                    div_ADT.Style("Display") = "none"
                End If

                ''''''''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                'Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_SE_Ob"), DropDownList)
                'ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))

                'Dim G8SeatListO As New List(Of G8ServiceQuoteResponse)()


                'G8SeatListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Seat)
                'For i As Integer = 0 To G8SeatListO.Count - 1
                '    ddl3.Items.Add(New ListItem(G8SeatListO(i).Description + "--INR" + G8SeatListO(i).AmountWithTax.ToString(), G8SeatListO(i).CodeType + ":" + G8SeatListO(i).SSRCategory + "_" + G8SeatListO(i).ServiceID + ":" + G8SeatListO(i).Description + ":" + Adti.ToString() + ":" + G8SeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                'Next

                'ddl3.DataBind()
                'If G8SeatListO.Count > 0 Then
                '    div_ADT.Style("Display") = "block"

                'Else
                '    div_ADT.Style("Display") = "none"
                'End If




                If G8SSRListR.Count > 0 Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                        DirectCast(e.Item.FindControl("tranchor1_R"), HtmlGenericControl).Style.Add("display", "none")
                        DirectCast(e.Item.FindControl("A_ALL_R"), HtmlGenericControl).Style.Add("display", "none")
                        'Dim div_mealR_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealR_ADT"), HtmlControls.HtmlGenericControl)
                        'Dim div_mealRD_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_mealRD_ADT"), HtmlControls.HtmlGenericControl)

                        'div_mealR_ADT.Style("Display") = "none"
                        'div_mealRD_ADT.Style("Display") = "none"

                        ' Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                        Dim G8BagListR As New List(Of G8ServiceQuoteResponse)()
                        G8BagListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Baggage)

                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To G8BagListR.Count - 1
                            ddl_Ib.Items.Add(New ListItem(G8BagListR(i).Description + "--INR" + G8BagListR(i).AmountWithTax.ToString(), G8BagListR(i).CodeType + ":" + G8BagListR(i).SSRCategory + "_" + G8BagListR(i).ServiceID + ":" + G8BagListR(i).Description + ":" + Adti.ToString() + ":" + G8BagListR(i).Amount.ToString()))
                        Next
                        ddl_Ib.DataBind()
                        div_Ib.Style("Display") = "Display:block"

                        ''''''''''''''''''''''''''''''''''''''''''meal'''''''''''''''''''''''''''''''''''''''''''
                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                        Dim G8MealListR As New List(Of G8ServiceQuoteResponse)()
                        G8MealListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Meal)

                        ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                        For i As Integer = 0 To G8MealListR.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(G8MealListR(i).Description + "--INR" + G8MealListR(i).AmountWithTax.ToString(), G8MealListR(i).CodeType + ":" + G8MealListR(i).SSRCategory + "_" + G8MealListR(i).ServiceID + ":" + G8MealListR(i).Description + ":" + Adti.ToString() + ":" + G8MealListR(i).Amount.ToString()))
                        Next
                        ddl2_Ib.DataBind()


                        If G8MealListR.Count > 0 Then
                            div_Ib.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            ddl2_Ib.Style("Display") = "Display:block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            ddl2_Ib.Style("Display") = "Display:none"


                        End If


                        ''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''
                        'Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_SE_Ib"), DropDownList)
                        'Dim G8SeatListR As New List(Of G8ServiceQuoteResponse)()
                        'G8SeatListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Seat)

                        'ddl3_Ib.Items.Add(New ListItem("---Select Seat Options---", "select"))
                        'For i As Integer = 0 To G8SeatListR.Count - 1
                        '    ddl3_Ib.Items.Add(New ListItem(G8SeatListR(i).Description + "--INR" + G8SeatListR(i).AmountWithTax.ToString(), G8SeatListR(i).CodeType + ":" + G8SeatListR(i).SSRCategory + "_" + G8SeatListR(i).ServiceID + ":" + G8SeatListR(i).Description + ":" + Adti.ToString() + ":" + G8SeatListR(i).Amount.ToString()))
                        'Next
                        'ddl3_Ib.DataBind()


                        If G8MealListR.Count > 0 Then
                            div_Ib.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            ddl2_Ib.Style("Display") = "Display:block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            ddl2_Ib.Style("Display") = "Display:none"


                        End If

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
            'End Goair Meal
        ElseIf Provider.Trim.ToUpper = "AK" Then
            Try
                Dim OBFltDs As DataSet
                OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID").ToString())
                Dim sno As String() = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim segmentcount As Integer = Convert.ToInt32(sno(6))

                DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT"), HtmlControls.HtmlGenericControl)

                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()

                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob"), DropDownList)

                    Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                    If baglistCheck.Count = 0 Then
                        ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    End If

                    For i As Integer = 0 To baglist.Count - 1
                        ' ddl.SelectedIndex = i + 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl.DataBind()
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob"), DropDownList)
                    ' ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                    If MeallistCheck.Count = 0 Then
                        ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    End If
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2.DataBind()
                End If
                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                End If
                If OBFltDs.Tables(0).Rows.Count > 1 And FLT_STAT <> "RTF" Then
                    Dim sector1 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    Dim sector2 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString() + "</div>"
                    DirectCast(e.Item.FindControl("Seg1_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                    DirectCast(e.Item.FindControl("Seg2_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                    DirectCast(e.Item.FindControl("Seg2_Ob"), HtmlGenericControl).Style.Add("display", "block")

                    If (segmentcount > 1) Then
                        baglist = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        If baglist.Count > 0 Then
                            Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob_Seg2"), DropDownList)
                            Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                            If baglistCheck.Count = 0 Then
                                ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                            End If
                            'ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                            For i As Integer = 0 To baglist.Count - 1
                                ddlSeg_Ob.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddlSeg_Ob.DataBind()
                        End If
                        If Meallist.Count > 0 Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                            'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                            If MeallistCheck.Count = 0 Then
                                ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        End If
                    Else
                        Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        If Meallist.Count > 0 Then
                            Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                            ' ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                            If MeallistCheck.Count = 0 Then
                                ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ob.DataBind()
                        End If
                    End If
                ElseIf OBFltDs.Tables(0).Rows.Count > 1 And FLT_STAT = "RTF" Then
                    Dim newFltDsIN() As DataRow = OBFltDs.Tables(0).Select("TripType='R'")
                    Dim newFltDsOut() As DataRow = OBFltDs.Tables(0).Select("TripType='O'")
                    'Dim sector1 As String = "<div class='tablinks'>Sector:" + OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    'Dim sector2 As String = "<div class='tablinks'>Sector:" + newFltDsIN(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(0)("ArrivalLocation").ToString() + "</div>"
                    'DirectCast(e.Item.FindControl("Seg1_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                    'DirectCast(e.Item.FindControl("Seg2_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                    'DirectCast(e.Item.FindControl("Div_ADT_Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")
                    Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                    Dim Srfbaglist As List(Of GALWS.AirAsia.AirAsiaSSR)
                    Dim SrfMeallist As List(Of GALWS.AirAsia.AirAsiaSSR)
                    Srfbaglist = (From ssr In AKSSRSTF Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                    SrfMeallist = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()

                    If Srfbaglist.Count > 0 Then
                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                        'ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Srfbaglist Where ssr.Amount = 0).ToList()
                        If baglistCheck.Count = 0 Then
                            ddl_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                        End If
                        For i As Integer = 0 To baglist.Count - 1
                            ddl_Ib.Items.Add(New ListItem(Srfbaglist(i).Description + "--INR" + Srfbaglist(i).Amount.ToString(), baglist(i).Code + ":" + Srfbaglist(i).Description + "_" + Srfbaglist(i).Origin + "_" + Srfbaglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddl_Ib.DataBind()
                    End If
                    If SrfMeallist.Count > 0 Then
                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                        ' ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                        Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In SrfMeallist Where ssr.Amount = 0).ToList()
                        If MeallistCheck.Count = 0 Then
                            ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                        End If
                        For i As Integer = 0 To Meallist.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(SrfMeallist(i).Description + "--INR" + SrfMeallist(i).Amount.ToString(), SrfMeallist(i).Code + ":" + SrfMeallist(i).Description + "_" + SrfMeallist(i).Origin + "_" + SrfMeallist(i).Destination + ":" + SrfMeallist(i).Amount.ToString() + ":" + Adti.ToString()))
                        Next
                        ddl2_Ib.DataBind()
                    End If
                    If SrfMeallist.Count > 0 Or Srfbaglist.Count > 0 Then
                        div_Ib.Style("Display") = "block"
                    End If

                    If newFltDsIN.Length > 1 Then
                        Dim sector1 As String = "<div class='tablinks'>Sector:" + newFltDsIN(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sector2 As String = "<div class='tablinks'>Sector:" + newFltDsIN(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg1_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                        DirectCast(e.Item.FindControl("Seg2_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                        DirectCast(e.Item.FindControl("Div_ADT_Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")
                        Dim Rsno As String() = newFltDsIN(0)("sno").ToString().Split(":")
                        Dim Rsegmentcount As Integer = Convert.ToInt32(Rsno(6))
                        If (Rsegmentcount > 1) Then
                            Srfbaglist = (From ssr In AKSSRSTF Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            SrfMeallist = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            If baglist.Count > 0 Then
                                Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ob_Seg2"), DropDownList)
                                'ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                                Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                                If baglistCheck.Count = 0 Then
                                    ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                                End If
                                For i As Integer = 0 To baglist.Count - 1
                                    ddlSeg_Ob.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddlSeg_Ob.DataBind()
                            End If
                            If Meallist.Count > 0 Then
                                Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                                'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                                If MeallistCheck.Count = 0 Then
                                    ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                End If
                                For i As Integer = 0 To Meallist.Count - 1
                                    ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddl2Seg2_Ob.DataBind()
                            End If
                        Else
                            SrfMeallist = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            If Meallist.Count > 0 Then
                                Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ob_Seg2"), DropDownList)
                                'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                                If MeallistCheck.Count = 0 Then
                                    ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                End If
                                For i As Integer = 0 To Meallist.Count - 1
                                    ddl2Seg2_Ob.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddl2Seg2_Ob.DataBind()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Adult_ItemCreated", "Error_001", ex, "Flight")
            End Try
            'AirAsia Mael and Baggage
        Else
            DirectCast(e.Item.FindControl("tranchor1"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("A_ALL"), HtmlGenericControl).Style.Add("display", "block")
        End If
        If FLT_STAT = "RTF" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") Then
            VCIB = VCOB
            TripIB = TripOB
            Flight = "2"
        End If

        If (VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8") And InStr(VCIBSPL, "Special") = 0 Then ''And ProviderIB.Trim.ToUpper <> "TB" And ProviderIB.Trim <> "" And ProviderIB.Trim.ToUpper <> "YA" Then
            Try

                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)

                If VCIB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
                    ddl_Ib.Items.Add(New ListItem("Hand Bag Only 7kg--INR0", "HBAG" + Adti.ToString()))
                    ddl_Ib.Items.Add(New ListItem("First Check-in Baggage 15kg--INR750", "CBAG" + Adti.ToString()))
                Else
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                        ddl_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                End If

                ddl_Ib.DataBind()

                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                ds_Ib.Clear()
                If FLT_STAT = "RTF" Then
                    ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("OBTrackId").ToString(), Flight)
                Else
                    ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("IBTrackId").ToString(), Flight)
                End If


                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Adti.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                Next
                ddl2_Ib.DataBind()

                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try

        ElseIf ProviderIB.Trim.ToUpper = "TB" Or (FLT_STAT = "RTF" And Provider.Trim.ToUpper = "TB") Then

            Try

                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage)
                If FLT_STAT = "RTF" Then
                    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                Else
                    baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                End If
                'baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next

                ddl_Ib.DataBind()

                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                ''ds_Ib.Clear()
                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic)
                If FLT_STAT = "RTF" Then
                    Meallist = objmB.GetTBOMeals(TBOSSR, "R")
                Else
                    Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")
                End If

                'Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")

                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next
                ddl2_Ib.DataBind()

                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try


        ElseIf ProviderIB.Trim.ToUpper = "YA" Or (FLT_STAT = "RTF" And Provider.Trim.ToUpper = "YA") Then

            Try

                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)

                ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)

                Dim objAirPrice As New STD.BAL.YAAirPrice()

                Dim baglist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Baggage, YASSRIB)

                'baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Desc.ToString() + ")" + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Desc.ToString() + "_" + baglist(i).SSRType.ToString() + "_" + baglist(i).Desc.ToString() + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next

                ddl_Ib.DataBind()

                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                ''ds_Ib.Clear()
                Dim Meallist As List(Of STD.BAL.YASSR)

                Meallist = objAirPrice.GetYaSSrMealBagList(YASSRType.Meal, YASSRIB)

                'Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")

                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(Meallist(i).Desc.ToString() + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Desc.ToString() + "_" + Meallist(i).SSRType.ToString() + "_" + Meallist(i).Desc.ToString() + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                Next
                ddl2_Ib.DataBind()

                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try
        ElseIf ProviderIB.Trim.ToUpper = "AK" Then
            Try
                Dim IBFltDs As DataSet
                IBFltDs = objDA.GetFltDtls(IBTrackId, Session("UID").ToString())
                Dim sno As String() = IBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim segmentcount As Integer = Convert.ToInt32(sno(6))

                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_ADT_Ib"), HtmlControls.HtmlGenericControl)
                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR)
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR)
                baglist = (From ssr In AKSSRIB Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                Meallist = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()

                If baglist.Count > 0 Then
                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib"), DropDownList)
                    'ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                    If baglistCheck.Count = 0 Then
                        ddl_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                    End If
                    For i As Integer = 0 To baglist.Count - 1
                        ddl_Ib.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl_Ib.DataBind()
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_A_Meal_Ib"), DropDownList)
                    ' ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                    If MeallistCheck.Count = 0 Then
                        ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    End If
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2_Ib.DataBind()
                End If

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_Ib.Style("Display") = "block"
                End If

                If IBFltDs.Tables(0).Rows.Count > 1 Then
                    Dim sector1 As String = "<div class='tablinks'>Sector:" + IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    Dim sector2 As String = "<div class='tablinks'>Sector:" + IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString() + "</div>"
                    DirectCast(e.Item.FindControl("Seg1_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                    DirectCast(e.Item.FindControl("Seg2_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                    DirectCast(e.Item.FindControl("Div_ADT_Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")

                    If (segmentcount > 1) Then
                        Dim baglist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        'If FLT_STAT = "RTF" Then
                        '    baglist2 = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        '    Meallist2 = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim()).ToList()
                        'Else
                        baglist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        Meallist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        'End If  (From h In RoomDetals Order By h.TotalRoomrate Ascending).ToList()
                        If baglist2.Count > 0 Then
                            Dim ddlSeg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_EB_Ib_Seg2"), DropDownList)
                            'ddlSeg_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                            Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0 And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim()).ToList()
                            If baglistCheck.Count > 0 Then
                                ddlSeg_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                            End If
                            For i As Integer = 0 To baglist2.Count - 1
                                ddlSeg_Ib.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddlSeg_Ib.DataBind()
                        End If
                        If Meallist2.Count > 0 Then
                            Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ib_Seg2"), DropDownList)
                            'ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                            If MeallistCheck.Count = 0 Then
                                ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ib.DataBind()
                        End If
                    Else
                        Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        If FLT_STAT = "RTF" Then
                            Meallist2 = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        Else
                            Meallist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        End If

                        If Meallist2.Count > 0 Then
                            Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_A_Meal_Ib_Seg2"), DropDownList)
                            ' ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                            If MeallistCheck.Count = 0 Then
                                ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist2.Count - 1
                                ddl2Seg2_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ib.DataBind()
                        End If
                    End If
                End If

            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("BindSSRL_CustomerInfoDom", "Error_001", ex, "AirAsiaFlight")
            End Try

        ElseIf (VCIB = "") And InStr(VCIBSPL, "Special") = 0 Then

        ElseIf G8SSRListR.Count <= 0 Then
            DirectCast(e.Item.FindControl("tranchor1_R"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("A_ALL_R"), HtmlGenericControl).Style.Add("display", "block")
        End If
    End Sub
    Protected Sub Repeater_Child_ItemCreated(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim Flight As Char = "1" 'For RoundTrip SpecialCase'
        Chdi = Chdi + 1
        If (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") And InStr(VCOBSPL, "Special") = 0 Then ''And Provider.Trim.ToUpper <> "TB" And Provider.Trim.ToUpper <> "YA" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                Dim ds As DataSet = objDA.GetSSR_EB(TripOB, VCOB)

                If VCOB = "SG" And (SERCHVALO.Contains("P1") Or SERCHVALO.Contains("P2")) Then
                    ddl.Items.Add(New ListItem("Hand Bag Only 7kg--INR0", "HBAG" + Chdi.ToString()))
                    ddl.Items.Add(New ListItem("First Check-in Baggage 15kg--INR750", "CBAG" + Chdi.ToString()))
                Else
                    ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        ddl.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                End If

                ds.Clear()
                ds = objDA.GetSSR_Meal(TripOB, VCOB, ATOB, ViewState("OBTrackId").ToString(), Flight)

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ddl2.Items.Add(New ListItem(ds.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds.Tables(0).Rows(i)("PRICE").ToString(), ds.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                div_CHD.Style("Display") = "block"

            Catch ex As Exception

            End Try
        ElseIf Provider.Trim.ToUpper = "TB" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)

                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage) = objmB.GetTBOBaggage(TBOSSR, "O")

                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic) = objmB.GetTBOMeals(TBOSSR, "O")

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next

                div_CHD.Style("Display") = "block"
            Catch ex As Exception

            End Try
        ElseIf Provider.Trim.ToUpper = "YA" Then
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                Dim objAirPrice As New STD.BAL.YAAirPrice()

                Dim baglist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Baggage, YASSR)


                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Desc.ToString() + " )" + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Desc.ToString() + "_" + baglist(i).SSRType.ToString() + "_" + baglist(i).Desc.ToString() + "_" + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                Dim Meallist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Meal, YASSR)

                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2.Items.Add(New ListItem(Meallist(i).Desc.ToString() + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Desc.ToString() + "_" + Meallist(i).SSRType.ToString() + "_" + Meallist(i).Desc.ToString() + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                Next

                div_CHD.Style("Display") = "block"
            Catch ex As Exception

            End Try

        ElseIf VCOB = "IX" Then
            Dim objG8FQ As New STD.BAL.G8FareQuote("", "", "", "")
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                Dim G8BagListO As New List(Of G8ServiceQuoteResponse)()
                G8BagListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Baggage)
                For i As Integer = 0 To G8BagListO.Count - 1
                    ddl.Items.Add(New ListItem(G8BagListO(i).Description + "--INR" + G8BagListO(i).AmountWithTax.ToString(), G8BagListO(i).CodeType + ":" + G8BagListO(i).SSRCategory + "_" + G8BagListO(i).ServiceID + ":" + G8BagListO(i).Description + ":" + Adti.ToString() + ":" + G8BagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()
                'If G8BagListO.Count > 0 Then
                '    div_CHD.Style("Display") = "block"
                'Else
                '    'div_ADT.Style("Display") = "none"
                'End If

                ''''''''''''''''''''''meal'''''''''''''''''''''''''''

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))

                Dim G8MealListO As New List(Of G8ServiceQuoteResponse)()
                G8MealListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Meal)
                For i As Integer = 0 To G8MealListO.Count - 1
                    ddl2.Items.Add(New ListItem(G8MealListO(i).Description + "--INR" + G8MealListO(i).AmountWithTax.ToString(), G8MealListO(i).CodeType + ":" + G8MealListO(i).SSRCategory + "_" + G8MealListO(i).ServiceID + ":" + G8MealListO(i).Description + ":" + Adti.ToString() + ":" + G8MealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl2.DataBind()
                'If G8MealListO.Count > 0 Then
                '    div_CHD.Style("Display") = "block"
                '    'div_mealO_ADT.Style("Display") = "block"
                '    'div_mealOD_ADT.Style("Display") = "block"
                'Else
                '    div_CHD.Style("Display") = "none"
                'End If
                ''''''''''''''''''''''SEAT'''''''''''''''''''''''''''

                'Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_SE_Ob"), DropDownList)
                'ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))

                'Dim G8SeatListO As New List(Of G8ServiceQuoteResponse)()

                'G8SeatListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Seat)
                'For i As Integer = 0 To G8SeatListO.Count - 1
                '    ddl3.Items.Add(New ListItem(G8SeatListO(i).Description + "--INR" + G8SeatListO(i).AmountWithTax.ToString(), G8SeatListO(i).CodeType + ":" + G8SeatListO(i).SSRCategory + "_" + G8SeatListO(i).ServiceID + ":" + G8SeatListO(i).Description + ":" + Adti.ToString() + ":" + G8SeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                'Next
                ''ddl.AutoPostBack = True
                'ddl3.DataBind()
                'If G8SeatListO.Count > 0 Then
                '    div_CHD.Style("Display") = "block"
                '    'div_mealO_ADT.Style("Display") = "block"
                '    'div_mealOD_ADT.Style("Display") = "block"
                'Else
                '    div_CHD.Style("Display") = "none"
                'End If


                If G8BagListO.Count > 0 Or G8MealListO.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                    'div_mealO_ADT.Style("Display") = "block"
                    'div_mealOD_ADT.Style("Display") = "block"
                Else
                    div_CHD.Style("Display") = "none"
                End If
                If G8SSRListR.Count > 0 Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                        DirectCast(e.Item.FindControl("tranchor2_R"), HtmlGenericControl).Style.Add("display", "none")
                        DirectCast(e.Item.FindControl("C_ALL_R"), HtmlGenericControl).Style.Add("display", "none")
                        Dim G8BagListR As New List(Of G8ServiceQuoteResponse)()
                        G8BagListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Baggage)

                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To G8BagListR.Count - 1
                            ddl_Ib.Items.Add(New ListItem(G8BagListR(i).Description + "--INR" + G8BagListR(i).AmountWithTax.ToString(), G8BagListR(i).CodeType + ":" + G8BagListR(i).SSRCategory + "_" + G8BagListR(i).ServiceID + ":" + G8BagListR(i).Description + ":" + Adti.ToString() + ":" + G8BagListR(i).Amount.ToString()))
                        Next
                        ddl_Ib.DataBind()
                        'div_Ib.Style("Display") = "block"

                        ''''''''''''''''''''''''''''''''''''''''''meal'''''''''''''''''''''''''''''''''''''''''''
                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                        Dim G8MealListR As New List(Of G8ServiceQuoteResponse)()
                        G8MealListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Meal)

                        ddl2_Ib.Items.Add(New ListItem("---Select Excess Meal Options---", "select"))
                        For i As Integer = 0 To G8MealListR.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(G8MealListR(i).Description + "--INR" + G8MealListR(i).AmountWithTax.ToString(), G8MealListR(i).CodeType + ":" + G8MealListR(i).SSRCategory + "_" + G8MealListR(i).ServiceID + ":" + G8MealListR(i).Description + ":" + Adti.ToString() + ":" + G8MealListR(i).Amount.ToString()))
                        Next
                        ddl2_Ib.DataBind()

                        'If G8MealListR.Count > 0 Then
                        '    'div_mealR_ADT.Style("Display") = "block"
                        '    'div_mealRD_ADT.Style("Display") = "block"
                        '    ddl2_Ib.Style("Display") = "block"
                        'Else
                        '    'div_mealR_ADT.Style("Display") = "none"
                        '    'div_mealRD_ADT.Style("Display") = "none"
                        '    ddl2_Ib.Style("Display") = "none"
                        'End If
                        ''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''
                        'Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_SE_Ib"), DropDownList)
                        'Dim G8SeatListR As New List(Of G8ServiceQuoteResponse)()
                        'G8SeatListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Seat)

                        'ddl3_Ib.Items.Add(New ListItem("---Select Excess Seat Options---", "select"))
                        'For i As Integer = 0 To G8SeatListR.Count - 1
                        '    ddl3_Ib.Items.Add(New ListItem(G8SeatListR(i).Description + "--INR" + G8SeatListR(i).AmountWithTax.ToString(), G8SeatListR(i).CodeType + ":" + G8SeatListR(i).SSRCategory + "_" + G8SeatListR(i).ServiceID + ":" + G8SeatListR(i).Description + ":" + Adti.ToString() + ":" + G8SeatListR(i).Amount.ToString()))
                        'Next
                        'ddl3_Ib.DataBind()
                        'If G8SeatListR.Count > 0 Then
                        '    'div_mealR_ADT.Style("Display") = "block"
                        '    'div_mealRD_ADT.Style("Display") = "block"
                        '    ddl3_Ib.Style("Display") = "block"
                        'Else
                        '    'div_mealR_ADT.Style("Display") = "none"
                        '    'div_mealRD_ADT.Style("Display") = "none"
                        '    ddl3_Ib.Style("Display") = "none"
                        'End If
                        If G8BagListR.Count > 0 Or G8MealListR.Count > 0 Then
                            'div_mealR_ADT.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            div_Ib.Style("Display") = "block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            div_Ib.Style("Display") = "none"
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        ElseIf VCOB = "G88" Then
            Dim objG8FQ As New STD.BAL.G8FareQuote("", "", "", "")
            Try
                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_CHD As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))

                Dim G8BagListO As New List(Of G8ServiceQuoteResponse)()
                G8BagListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Baggage)
                For i As Integer = 0 To G8BagListO.Count - 1
                    ddl.Items.Add(New ListItem(G8BagListO(i).Description + "--INR" + G8BagListO(i).AmountWithTax.ToString(), G8BagListO(i).CodeType + ":" + G8BagListO(i).SSRCategory + "_" + G8BagListO(i).ServiceID + ":" + G8BagListO(i).Description + ":" + Adti.ToString() + ":" + G8BagListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl.DataBind()
                If G8BagListO.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                Else
                    'div_ADT.Style("Display") = "none"
                End If

                ''''''''''''''''''''''meal'''''''''''''''''''''''''''

                Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))

                Dim G8MealListO As New List(Of G8ServiceQuoteResponse)()
                G8MealListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Meal)
                For i As Integer = 0 To G8MealListO.Count - 1
                    ddl2.Items.Add(New ListItem(G8MealListO(i).Description + "--INR" + G8MealListO(i).AmountWithTax.ToString(), G8MealListO(i).CodeType + ":" + G8MealListO(i).SSRCategory + "_" + G8MealListO(i).ServiceID + ":" + G8MealListO(i).Description + ":" + Adti.ToString() + ":" + G8MealListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl2.DataBind()
                If G8MealListO.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                    'div_mealO_ADT.Style("Display") = "block"
                    'div_mealOD_ADT.Style("Display") = "block"
                Else
                    div_CHD.Style("Display") = "none"
                End If
                ''''''''''''''''''''''SEAT'''''''''''''''''''''''''''

                Dim ddl3 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_SE_Ob"), DropDownList)
                ddl3.Items.Add(New ListItem("---Select Seat Options---", "select"))

                Dim G8SeatListO As New List(Of G8ServiceQuoteResponse)()

                G8SeatListO = objG8FQ.GetSSRListServiceTypeWise(G8SSRListO, ServiceType.Seat)
                For i As Integer = 0 To G8SeatListO.Count - 1
                    ddl3.Items.Add(New ListItem(G8SeatListO(i).Description + "--INR" + G8SeatListO(i).AmountWithTax.ToString(), G8SeatListO(i).CodeType + ":" + G8SeatListO(i).SSRCategory + "_" + G8SeatListO(i).ServiceID + ":" + G8SeatListO(i).Description + ":" + Adti.ToString() + ":" + G8SeatListO(i).Amount.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next
                'ddl.AutoPostBack = True
                ddl3.DataBind()
                If G8SeatListO.Count > 0 Then
                    div_CHD.Style("Display") = "block"
                    'div_mealO_ADT.Style("Display") = "block"
                    'div_mealOD_ADT.Style("Display") = "block"
                Else
                    div_CHD.Style("Display") = "none"
                End If

                If G8SSRListR.Count > 0 Then
                    Try

                        Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                        DirectCast(e.Item.FindControl("tranchor2_R"), HtmlGenericControl).Style.Add("display", "none")
                        DirectCast(e.Item.FindControl("C_ALL_R"), HtmlGenericControl).Style.Add("display", "none")
                        Dim G8BagListR As New List(Of G8ServiceQuoteResponse)()
                        G8BagListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Baggage)

                        ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                        For i As Integer = 0 To G8BagListR.Count - 1
                            ddl_Ib.Items.Add(New ListItem(G8BagListR(i).Description + "--INR" + G8BagListR(i).AmountWithTax.ToString(), G8BagListR(i).CodeType + ":" + G8BagListR(i).SSRCategory + "_" + G8BagListR(i).ServiceID + ":" + G8BagListR(i).Description + ":" + Adti.ToString() + ":" + G8BagListR(i).Amount.ToString()))
                        Next
                        ddl_Ib.DataBind()
                        div_Ib.Style("Display") = "block"

                        ''''''''''''''''''''''''''''''''''''''''''meal'''''''''''''''''''''''''''''''''''''''''''
                        Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                        Dim G8MealListR As New List(Of G8ServiceQuoteResponse)()
                        G8MealListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Meal)

                        ddl2_Ib.Items.Add(New ListItem("---Select Excess Meal Options---", "select"))
                        For i As Integer = 0 To G8MealListR.Count - 1
                            ddl2_Ib.Items.Add(New ListItem(G8MealListR(i).Description + "--INR" + G8MealListR(i).AmountWithTax.ToString(), G8MealListR(i).CodeType + ":" + G8MealListR(i).SSRCategory + "_" + G8MealListR(i).ServiceID + ":" + G8MealListR(i).Description + ":" + Adti.ToString() + ":" + G8MealListR(i).Amount.ToString()))
                        Next
                        ddl2_Ib.DataBind()

                        If G8MealListR.Count > 0 Then
                            'div_mealR_ADT.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            ddl2_Ib.Style("Display") = "block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            ddl2_Ib.Style("Display") = "none"
                        End If
                        ''''''''''''''''''''''''''''''''''''''''''SEAT'''''''''''''''''''''''''''''''''''''''''''
                        Dim ddl3_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_SE_Ib"), DropDownList)
                        Dim G8SeatListR As New List(Of G8ServiceQuoteResponse)()
                        G8SeatListR = objG8FQ.GetSSRListServiceTypeWise(G8SSRListR, ServiceType.Seat)

                        ddl3_Ib.Items.Add(New ListItem("---Select Excess Seat Options---", "select"))
                        For i As Integer = 0 To G8SeatListR.Count - 1
                            ddl3_Ib.Items.Add(New ListItem(G8SeatListR(i).Description + "--INR" + G8SeatListR(i).AmountWithTax.ToString(), G8SeatListR(i).CodeType + ":" + G8SeatListR(i).SSRCategory + "_" + G8SeatListR(i).ServiceID + ":" + G8SeatListR(i).Description + ":" + Adti.ToString() + ":" + G8SeatListR(i).Amount.ToString()))
                        Next
                        ddl3_Ib.DataBind()
                        If G8SeatListR.Count > 0 Then
                            'div_mealR_ADT.Style("Display") = "block"
                            'div_mealRD_ADT.Style("Display") = "block"
                            ddl3_Ib.Style("Display") = "block"
                        Else
                            'div_mealR_ADT.Style("Display") = "none"
                            'div_mealRD_ADT.Style("Display") = "none"
                            ddl3_Ib.Style("Display") = "none"
                        End If

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        ElseIf Provider.Trim.ToUpper = "AK" Then
            Try
                Dim OBFltDs As DataSet
                OBFltDs = objDA.GetFltDtls(OBTrackId, Session("UID").ToString())
                Dim sno As String() = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim segmentcount As Integer = Convert.ToInt32(sno(6))

                DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "none")
                DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "none")
                Dim div_ADT As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD"), HtmlControls.HtmlGenericControl)
                'Or (FLT_STAT = "RTF" 
                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = OBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = OBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()

                If baglist.Count > 0 Then
                    Dim ddl As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob"), DropDownList)
                    'ddl.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                    If baglistCheck.Count = 0 Then
                        ddl.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                    End If
                    For i As Integer = 0 To baglist.Count - 1
                        ddl.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl.DataBind()
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2 As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob"), DropDownList)
                    'ddl2.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                    If meallistCheck.Count = 0 Then
                        ddl2.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                    End If
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2.DataBind()
                End If

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_ADT.Style("Display") = "block"
                End If

                If OBFltDs.Tables(0).Rows.Count > 1 Then
                    Dim newFltDsIN() As DataRow = OBFltDs.Tables(0).Select("TripType='R'")
                    Dim newFltDsOut() As DataRow = OBFltDs.Tables(0).Select("TripType='O'")

                    If FLT_STAT <> "RTF" Then
                        Dim sector1 As String = "<div class='tablinks'>Sector:" + newFltDsOut(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsOut(0)("ArrivalLocation").ToString() + "</div>"
                        Dim sector2 As String = "<div class='tablinks'>Sector:" + newFltDsOut(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsOut(1)("ArrivalLocation").ToString() + "</div>"
                        DirectCast(e.Item.FindControl("Seg1_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                        DirectCast(e.Item.FindControl("Seg2_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                        DirectCast(e.Item.FindControl("Seg2_C_Ob"), HtmlGenericControl).Style.Add("display", "block")

                        If (segmentcount > 1) Then
                            Dim baglist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsOut(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsOut(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsOut(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsOut(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            If baglist2.Count > 0 Then
                                Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob_Seg2"), DropDownList)
                                'ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                                Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist2 Where ssr.Amount = 0).ToList()
                                If baglistCheck.Count = 0 Then
                                    ddlSeg_Ob.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                                End If
                                For i As Integer = 0 To baglist.Count - 1
                                    ddlSeg_Ob.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddlSeg_Ob.DataBind()
                            End If
                            If Meallist2.Count > 0 Then
                                Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                                ' ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                                If meallistCheck.Count = 0 Then
                                    ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                End If
                                For i As Integer = 0 To Meallist.Count - 1
                                    ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddl2Seg2_Ob.DataBind()
                            End If
                        Else
                            Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsOut(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsOut(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            If Meallist2.Count > 0 Then
                                Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                                'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                                If meallistCheck.Count = 0 Then
                                    ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                End If
                                For i As Integer = 0 To Meallist.Count - 1
                                    ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddl2Seg2_Ob.DataBind()
                            End If
                        End If

                    Else

                        Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                        Dim Srfbaglist As List(Of GALWS.AirAsia.AirAsiaSSR)
                        Dim SrfMeallist As List(Of GALWS.AirAsia.AirAsiaSSR)
                        Srfbaglist = (From ssr In AKSSRSTF Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        SrfMeallist = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()

                        If Srfbaglist.Count > 0 Then
                            Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                            'ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                            Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Srfbaglist Where ssr.Amount = 0).ToList()
                            If baglistCheck.Count = 0 Then
                                ddl_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                            End If
                            For i As Integer = 0 To baglist.Count - 1
                                ddl_Ib.Items.Add(New ListItem(Srfbaglist(i).Description + "--INR" + Srfbaglist(i).Amount.ToString(), baglist(i).Code + ":" + Srfbaglist(i).Description + "_" + Srfbaglist(i).Origin + "_" + Srfbaglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl_Ib.DataBind()
                        End If
                        If SrfMeallist.Count > 0 Then
                            Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                            ' ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim MeallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In SrfMeallist Where ssr.Amount = 0).ToList()
                            If MeallistCheck.Count = 0 Then
                                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2_Ib.Items.Add(New ListItem(SrfMeallist(i).Description + "--INR" + SrfMeallist(i).Amount.ToString(), SrfMeallist(i).Code + ":" + SrfMeallist(i).Description + "_" + SrfMeallist(i).Origin + "_" + SrfMeallist(i).Destination + ":" + SrfMeallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2_Ib.DataBind()
                        End If
                        If SrfMeallist.Count > 0 Or Srfbaglist.Count > 0 Then
                            div_Ib.Style("Display") = "block"
                        End If
                        If newFltDsIN.Length > 1 Then
                            Dim sector1 As String = "<div class='tablinks'>Sector:" + newFltDsIN(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(0)("ArrivalLocation").ToString() + "</div>"
                            Dim sector2 As String = "<div class='tablinks'>Sector:" + newFltDsIN(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(1)("ArrivalLocation").ToString() + "</div>"
                            DirectCast(e.Item.FindControl("Seg1_C_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                            DirectCast(e.Item.FindControl("Seg2_C_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                            DirectCast(e.Item.FindControl("Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")

                            Dim Rsno As String() = newFltDsIN(0)("sno").ToString().Split(":")
                            Dim Rsegmentcount As Integer = Convert.ToInt32(sno(6))
                            ' If (Rsegmentcount > 1) Then
                            Dim SrfbaglistIN As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            Dim SrfMeallistIN As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            If SrfbaglistIN.Count > 0 Then
                                Dim ddlSeg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib_Seg2"), DropDownList)
                                'ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                                Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In SrfbaglistIN Where ssr.Amount = 0).ToList()
                                If baglistCheck.Count = 0 Then
                                    ddlSeg_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                                End If
                                For i As Integer = 0 To baglist.Count - 1
                                    ddlSeg_Ib.Items.Add(New ListItem(SrfbaglistIN(i).Description + "--INR" + SrfbaglistIN(i).Amount.ToString(), SrfbaglistIN(i).Code + ":" + SrfbaglistIN(i).Description + "_" + SrfbaglistIN(i).Origin + "_" + SrfbaglistIN(i).Destination + ":" + SrfbaglistIN(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddlSeg_Ib.DataBind()
                            End If
                            If SrfMeallistIN.Count > 0 Then
                                Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib_Seg2"), DropDownList)
                                ' ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In SrfMeallistIN Where ssr.Amount = 0).ToList()
                                If meallistCheck.Count = 0 Then
                                    ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                                End If
                                For i As Integer = 0 To Meallist.Count - 1
                                    ddl2Seg2_Ib.Items.Add(New ListItem(SrfMeallistIN(i).Description + "--INR" + SrfMeallistIN(i).Amount.ToString(), SrfMeallistIN(i).Code + ":" + SrfMeallistIN(i).Description + "_" + SrfMeallistIN(i).Origin + "_" + SrfMeallistIN(i).Destination + ":" + SrfMeallistIN(i).Amount.ToString() + ":" + Adti.ToString()))
                                Next
                                ddl2Seg2_Ib.DataBind()
                            End If
                            'Else
                            '    Dim SrfMeallist As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(1)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            '    If SrfMeallist.Count > 0 Then
                            '        Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                            '        'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '        Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In SrfMeallist Where ssr.Amount = 0).ToList()
                            '        If meallistCheck.Count = 0 Then
                            '            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '        End If
                            '        For i As Integer = 0 To Meallist.Count - 1
                            '            ddl2Seg2_Ob.Items.Add(New ListItem(SrfMeallist(i).Description + "--INR" + SrfMeallist(i).Amount.ToString(), SrfMeallist(i).Code + ":" + SrfMeallist(i).Description + "_" + SrfMeallist(i).Origin + "_" + SrfMeallist(i).Destination + ":" + SrfMeallist(i).Amount.ToString() + ":" + Adti.ToString()))
                            '        Next
                            '        ddl2Seg2_Ob.DataBind()
                            '    End If
                            'End If

                            'If newFltDsIN.Length > 1 Then
                            '    Dim sector1 As String = "<div class='tablinks'>Sector:" + newFltDsIN(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(0)("ArrivalLocation").ToString() + "</div>"
                            '    Dim sector2 As String = "<div class='tablinks'>Sector:" + newFltDsIN(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + newFltDsIN(1)("ArrivalLocation").ToString() + "</div>"
                            '    DirectCast(e.Item.FindControl("Seg1_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                            '    DirectCast(e.Item.FindControl("Seg2_C_Ob_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                            '    DirectCast(e.Item.FindControl("Seg2_C_Ob"), HtmlGenericControl).Style.Add("display", "block")
                            '    Dim Rsno As String() = OBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                            '    Dim Rsegmentcount As Integer = Convert.ToInt32(sno(6))
                            '    ' If (Rsegmentcount > 1) Then
                            '    Dim baglist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Baggage" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            '    Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            '    If baglist2.Count > 0 Then
                            '        Dim ddlSeg_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ob_Seg2"), DropDownList)
                            '        'ddlSeg_Ob.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                            '        Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist2 Where ssr.Amount = 0).ToList()
                            '        If baglistCheck.Count = 0 Then
                            '            ddlSeg_Ob.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                            '        End If
                            '        For i As Integer = 0 To baglist.Count - 1
                            '            ddlSeg_Ob.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            '        Next
                            '        ddlSeg_Ob.DataBind()
                            '    End If
                            '    If Meallist2.Count > 0 Then
                            '        Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                            '        ' ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '        Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                            '        If meallistCheck.Count = 0 Then
                            '            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '        End If
                            '        For i As Integer = 0 To Meallist.Count - 1
                            '            ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            '        Next
                            '        ddl2Seg2_Ob.DataBind()
                            '    End If
                            '    'Else
                            '    '    Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In AKSSRSTF Where ssr.SSRType = "Meal" And ssr.Origin = newFltDsIN(0)("DepartureLocation").ToString().Trim() And ssr.Destination = newFltDsIN(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            '    '    If Meallist2.Count > 0 Then
                            '    '        Dim ddl2Seg2_Ob As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ob_Seg2"), DropDownList)
                            '    '        'ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '    '        Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                            '    '        If meallistCheck.Count = 0 Then
                            '    '            ddl2Seg2_Ob.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            '    '        End If
                            '    '        For i As Integer = 0 To Meallist.Count - 1
                            '    '            ddl2Seg2_Ob.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            '    '        Next
                            '    '        ddl2Seg2_Ob.DataBind()
                            '    '    End If
                            '    'End If
                            'End If

                        End If
                    End If
                End If
            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("Repeater_Child_ItemCreated", "Error_001", ex, "Flight")
            End Try
            'AirAsia Mael and Baggage
        Else
            DirectCast(e.Item.FindControl("tranchor2"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("C_ALL"), HtmlGenericControl).Style.Add("display", "block")
        End If
        If FLT_STAT = "RTF" And (VCOB = "SG" Or VCOB = "6E" Or VCOB = "G8") Then
            VCIB = VCOB
            TripIB = TripOB
            Flight = "2"
        End If


        If (VCIB = "SG" Or VCIB = "6E" Or VCIB = "G8") And InStr(VCIBSPL, "Special") = 0 Then ''And ProviderIB.Trim.ToUpper <> "TB" And ProviderIB.Trim <> "" And ProviderIB.Trim.ToUpper <> "YA" Then
            Try
                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)

                If VCIB = "SG" And (SERCHVALR.Contains("P1") Or SERCHVALR.Contains("P2")) Then
                    ddl_Ib.Items.Add(New ListItem("Hand Bag Only 7kg--INR0", "HBAG" + Chdi.ToString()))
                    ddl_Ib.Items.Add(New ListItem("First Check-in Baggage 15kg--INR750", "CBAG" + Chdi.ToString()))
                Else
                    ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                        ddl_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                    Next
                End If
                ds_Ib.Clear()
                If FLT_STAT = "RTF" Then
                    ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("OBTrackId").ToString(), Flight)
                Else
                    ds_Ib = objDA.GetSSR_Meal(TripIB, VCIB, ATIB, ViewState("IBTrackId").ToString(), Flight)
                End If


                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To ds_Ib.Tables(0).Rows.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(ds_Ib.Tables(0).Rows(i)("DESCRIPTION").ToString() + "--INR" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString(), ds_Ib.Tables(0).Rows(i)("SSR_CODE").ToString() + Chdi.ToString())) '+ "-" + ds_Ib.Tables(0).Rows(i)("PRICE").ToString()
                Next

                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try
        ElseIf ProviderIB.Trim.ToUpper = "TB" Or (FLT_STAT = "RTF" And Provider.Trim.ToUpper = "TB") Then

            Try
                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                Dim objmB As New STD.BAL.TBO.SSR.TOBSSR()
                Dim baglist As List(Of STD.BAL.TBO.SSR.Baggage)
                If FLT_STAT = "RTF" Then
                    baglist = objmB.GetTBOBaggage(TBOSSR, "R")
                Else
                    baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                End If
                'baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Weight.ToString() + " KG)" + "--INR" + baglist(i).Price.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Description.ToString() + "_" + baglist(i).WayType.ToString() + "_" + baglist(i).Weight.ToString() + "_" + baglist(i).Destination.ToString() + "_" + baglist(i).Origin.ToString() + ":" + baglist(i).Price.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next



                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                ''ds_Ib.Clear()
                Dim Meallist As List(Of STD.BAL.TBO.SSR.MealDynamic)
                If FLT_STAT = "RTF" Then
                    Meallist = objmB.GetTBOMeals(TBOSSR, "R")
                Else
                    Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")
                End If

                'Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")

                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(Meallist(i).AirlineDescription.ToString() + "--INR" + Meallist(i).Price.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Description.ToString() + "_" + Meallist(i).WayType.ToString() + "_" + Meallist(i).AirlineDescription.ToString() + "_" + Meallist(i).Destination.ToString() + "_" + Meallist(i).Origin.ToString() + ":" + Meallist(i).Price.ToString() + ":" + Adti.ToString()))
                Next
                ddl2_Ib.DataBind()


                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try

        ElseIf ProviderIB.Trim.ToUpper = "YA" Or (FLT_STAT = "RTF" And Provider.Trim.ToUpper = "YA") Then

            Try
                Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)

                ''Dim ds_Ib As DataSet = objDA.GetSSR_EB(TripIB, VCIB)
                Dim objAirPrice As New STD.BAL.YAAirPrice()

                Dim baglist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Baggage, YASSRIB)
                'baglist = objmB.GetTBOBaggage(TBOSSRIB, "O")
                ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                For i As Integer = 0 To baglist.Count - 1
                    ddl_Ib.Items.Add(New ListItem(baglist(i).Code.ToString() + "( " + baglist(i).Desc.ToString() + ")" + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code.ToString() + ":" + baglist(i).Desc.ToString() + "_" + baglist(i).SSRType.ToString() + "_" + baglist(i).Desc.ToString() + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString())) '+ "-" + ds.Tables(0).Rows(i)("PRICE").ToString()
                Next



                Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib"), DropDownList)
                ''ds_Ib.Clear()
                Dim Meallist As List(Of STD.BAL.YASSR) = objAirPrice.GetYaSSrMealBagList(YASSRType.Meal, YASSRIB)


                'Meallist = objmB.GetTBOMeals(TBOSSRIB, "O")

                ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                For i As Integer = 0 To Meallist.Count - 1
                    ddl2_Ib.Items.Add(New ListItem(Meallist(i).Desc.ToString() + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code.ToString() + ":" + Meallist(i).Desc.ToString() + "_" + Meallist(i).SSRType.ToString() + "_" + Meallist(i).Desc.ToString() + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                Next
                ddl2_Ib.DataBind()


                div_Ib.Style("Display") = "block"
            Catch ex As Exception

            End Try

        ElseIf ProviderIB.Trim.ToUpper = "AK" Then
            Try
                Dim IBFltDs As DataSet
                IBFltDs = objDA.GetFltDtls(IBTrackId, Session("UID").ToString())
                Dim sno As String() = IBFltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim segmentcount As Integer = Convert.ToInt32(sno(6))

                Dim div_Ib As System.Web.UI.HtmlControls.HtmlGenericControl = TryCast(e.Item.FindControl("div_CHILD_Ib"), HtmlControls.HtmlGenericControl)
                Dim baglist As List(Of GALWS.AirAsia.AirAsiaSSR)
                Dim Meallist As List(Of GALWS.AirAsia.AirAsiaSSR)
                If FLT_STAT = "RTF" Then
                    baglist = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                    Meallist = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                Else
                    baglist = (From ssr In AKSSRIB Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                    Meallist = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                End If

                If baglist.Count > 0 Then
                    Dim ddl_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib"), DropDownList)
                    'ddl_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                    Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist Where ssr.Amount = 0).ToList()
                    If baglistCheck.Count = 0 Then
                        ddl_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                    End If
                    For i As Integer = 0 To baglist.Count - 1
                        ddl_Ib.Items.Add(New ListItem(baglist(i).Description + "--INR" + baglist(i).Amount.ToString(), baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Origin + "_" + baglist(i).Destination + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl_Ib.DataBind()
                End If
                If Meallist.Count > 0 Then
                    Dim ddl2_Ib As DropDownList = TryCast(e.Item.FindControl("ddl_C_Meal_Ib"), DropDownList)
                    'ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist Where ssr.Amount = 0).ToList()
                    If meallistCheck.Count = 0 Then
                        ddl2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                    End If
                    For i As Integer = 0 To Meallist.Count - 1
                        ddl2_Ib.Items.Add(New ListItem(Meallist(i).Description + "--INR" + Meallist(i).Amount.ToString(), Meallist(i).Code + ":" + Meallist(i).Description + "_" + Meallist(i).Origin + "_" + Meallist(i).Destination + ":" + Meallist(i).Amount.ToString() + ":" + Adti.ToString()))
                    Next
                    ddl2_Ib.DataBind()
                End If

                If Meallist.Count > 0 Or baglist.Count > 0 Then
                    div_Ib.Style("Display") = "block"
                End If

                If IBFltDs.Tables(0).Rows.Count > 1 Then
                    Dim sector1 As String = "<div class='tablinks'>Sector:" + IBFltDs.Tables(0).Rows(0)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + IBFltDs.Tables(0).Rows(0)("ArrivalLocation").ToString() + "</div>"
                    Dim sector2 As String = "<div class='tablinks'>Sector:" + IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString() + "<img src='/Images/air.png' />" + IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString() + "</div>"
                    DirectCast(e.Item.FindControl("Seg1_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector1
                    DirectCast(e.Item.FindControl("Seg2_Ib_MealBaggage"), HtmlGenericControl).InnerHtml = sector2
                    DirectCast(e.Item.FindControl("Seg2_Ib"), HtmlGenericControl).Style.Add("display", "block")

                    If (segmentcount > 1) Then
                        Dim baglist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        If FLT_STAT = "RTF" Then
                            baglist2 = (From ssr In AKSSR Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            Meallist2 = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        Else
                            baglist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Baggage" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                            Meallist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        End If
                        If baglist2.Count > 0 Then
                            Dim ddlSeg_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_EB_Ib_Seg2"), DropDownList)
                            'ddlSeg_Ib.Items.Add(New ListItem("---Select Excess Bagage Options---", "select"))
                            Dim baglistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In baglist2 Where ssr.Amount = 0).ToList()
                            If baglistCheck.Count = 0 Then
                                ddlSeg_Ib.Items.Add(New ListItem("---Select Bagage Options---", "select"))
                            End If
                            For i As Integer = 0 To baglist2.Count - 1
                                ddlSeg_Ib.Items.Add(New ListItem(baglist2(i).Description + "--INR" + baglist2(i).Amount.ToString(), baglist2(i).Code + ":" + baglist2(i).Description + "_" + baglist2(i).Origin + "_" + baglist2(i).Destination + ":" + baglist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddlSeg_Ib.DataBind()
                        End If
                        If Meallist2.Count > 0 Then
                            Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib_Seg2"), DropDownList)
                            ' ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                            If meallistCheck.Count = 0 Then
                                ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist.Count - 1
                                ddl2Seg2_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ib.DataBind()
                        End If
                    Else
                        Dim Meallist2 As List(Of GALWS.AirAsia.AirAsiaSSR)
                        If FLT_STAT = "RTF" Then
                            Meallist2 = (From ssr In AKSSR Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        Else
                            Meallist2 = (From ssr In AKSSRIB Where ssr.SSRType = "Meal" And ssr.Origin = IBFltDs.Tables(0).Rows(1)("DepartureLocation").ToString().Trim() And ssr.Destination = IBFltDs.Tables(0).Rows(1)("ArrivalLocation").ToString().Trim() Order By ssr.Amount).ToList()
                        End If

                        If Meallist2.Count > 0 Then
                            Dim ddl2Seg2_Ib As DropDownList = TryCast(e.Item.FindControl("Ddl_C_Meal_Ib_Seg2"), DropDownList)
                            'ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            Dim meallistCheck As List(Of GALWS.AirAsia.AirAsiaSSR) = (From ssr In Meallist2 Where ssr.Amount = 0).ToList()
                            If meallistCheck.Count = 0 Then
                                ddl2Seg2_Ib.Items.Add(New ListItem("---Select Meal Options---", "select"))
                            End If
                            For i As Integer = 0 To Meallist2.Count - 1
                                ddl2Seg2_Ib.Items.Add(New ListItem(Meallist2(i).Description + "--INR" + Meallist2(i).Amount.ToString(), Meallist2(i).Code + ":" + Meallist2(i).Description + "_" + Meallist2(i).Origin + "_" + Meallist2(i).Destination + ":" + Meallist2(i).Amount.ToString() + ":" + Adti.ToString()))
                            Next
                            ddl2Seg2_Ib.DataBind()
                        End If
                    End If
                End If

            Catch ex As Exception
                ITZERRORLOG.ExecptionLogger.FileHandling("BindSSRL_CustomerInfoDom", "Error_001", ex, "AirAsiaFlight")
            End Try


        ElseIf (VCIB = "") And InStr(VCIBSPL, "Special") = 0 Then

        Else
            DirectCast(e.Item.FindControl("tranchor2_R"), HtmlGenericControl).Style.Add("display", "block")
            DirectCast(e.Item.FindControl("C_ALL_R"), HtmlGenericControl).Style.Add("display", "block")
        End If
    End Sub
    Private Sub Insert_MEAL_BAG_Detail(ByVal trackid As String, ByVal FltDs As DataSet, ByVal Paxdt As DataSet, ByVal Type As String, ByVal TripType As String)
        Try
            Adult = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult"))
            Child = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child"))
            Infant = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant"))

            'NO Need for this only assign Code in the DDL option value
            Dim MealCd_OB As String = "", BagCd_OB As String = "", MealPr_OB As Decimal = 0, BagPr_OB As Decimal = 0
            Dim MealCd_IB As String = "", BagCd_IB As String = "", MealPr_IB As Decimal = 0, BagPr_IB As Decimal = 0
            Dim counter As Integer = 0
            Dim Dt As New DataTable
            getTableColumn(Dt)

            If (Type = "OB") Then

                Split_MB_Detail(lbl_A_MB_OB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
                If (Child > 0) Then
                    Split_MB_Detail(lbl_C_MB_OB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
                End If
                CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"))
                ''Code To insert in T_Flt_Meal_And_Baggage_Request
            ElseIf (Type = "IB") Then

                Split_MB_Detail(lbl_A_MB_IB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
                If (Child > 0) Then
                    Split_MB_Detail(lbl_C_MB_IB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
                End If
                CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"))
                ''Code To insert in T_Flt_Meal_And_Baggage_Request
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    'Private Sub Insert_MEAL_BAG_Detail(ByVal trackid As String, ByVal FltDs As DataSet, ByVal Paxdt As DataSet, ByVal Type As String, ByVal TripType As String)
    '    Try
    '        Adult = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Adult"))
    '        Child = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Child"))
    '        Infant = Convert.ToInt16(FltDs.Tables(0).Rows(0)("Infant"))

    '        'NO Need for this only assign Code in the DDL option value
    '        Dim MealCd_OB As String = "", BagCd_OB As String = "", MealPr_OB As Decimal = 0, BagPr_OB As Decimal = 0
    '        Dim MealCd_IB As String = "", BagCd_IB As String = "", MealPr_IB As Decimal = 0, BagPr_IB As Decimal = 0
    '        Dim counter As Integer = 0
    '        Dim Dt As New DataTable
    '        getTableColumn(Dt)

    '        If (Type = "OB") Then

    '            Split_MB_Detail(lbl_A_MB_OB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
    '            If (Child > 0) Then
    '                Split_MB_Detail(lbl_C_MB_OB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString().Trim())
    '            End If
    '            CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"))
    '            ' CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), FltDs.Tables(0).Rows(0)("Provider").ToString())
    '            ''Code To insert in T_Flt_Meal_And_Baggage_Request
    '        ElseIf (Type = "IB") Then

    '            Split_MB_Detail(lbl_A_MB_IB.Value, Adult, Dt, "ADT", FltDs.Tables(0).Rows(0)("Provider").ToString())
    '            If (Child > 0) Then
    '                Split_MB_Detail(lbl_C_MB_IB.Value, Child, Dt, "CHD", FltDs.Tables(0).Rows(0)("Provider").ToString())
    '            End If
    '            CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"))
    '            'CreateFinalTable(Dt, Adult, Child, Paxdt.Tables(0), trackid, TripType, FltDs.Tables(0).Rows(0)("ValiDatingCarrier"), FltDs.Tables(0).Rows(0)("Provider").ToString())
    '            ''Code To insert in T_Flt_Meal_And_Baggage_Request
    '        End If

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try


    'End Sub
    'Public Sub Split_MB_Detail(ByVal Text As String, ByVal Pxcnt As Integer, ByRef Dt As DataTable, ByVal PaxType As String, ByVal Provider As String)
    '    Dim MB() As String
    '    Try
    '        MB = Text.Split("#")
    '        Dim tax() As String

    '        If Provider = "LCC" Then
    '            tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    '            For i As Integer = 0 To tax.Length - 1
    '                Dim dr As DataRow = Dt.NewRow()
    '                dr("PaxType") = PaxType
    '                dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
    '                dr("MealCode") = tax(i).Split("@")(0).Split(":")(0)
    '                dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '                dr("MealDesc") = tax(i).Split("@")(0).Split(":")(2)
    '                dr("MealCategory") = tax(i).Split("@")(0).Split(":")(1)
    '                dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
    '                Dt.Rows.Add(dr)
    '            Next
    '            Array.Clear(tax, 0, tax.Length)
    '            tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

    '            For i As Integer = 0 To tax.Length - 1
    '                Dim dr As DataRow = Dt.NewRow()
    '                dr("PaxType") = PaxType
    '                dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
    '                dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
    '                dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '                dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(2)
    '                dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
    '                dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
    '                Dt.Rows.Add(dr)
    '            Next

    '            Array.Clear(tax, 0, tax.Length)
    '            tax = MB(2).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

    '            For i As Integer = 0 To tax.Length - 1
    '                Dim dr As DataRow = Dt.NewRow()
    '                dr("PaxType") = PaxType
    '                dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
    '                dr("SeatCode") = tax(i).Split("@")(0).Split(":")(0)
    '                dr("SeatPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '                dr("SeatDesc") = tax(i).Split("@")(0).Split(":")(2)
    '                dr("SeatCategory") = tax(i).Split("@")(0).Split(":")(1)
    '                dr("SeatPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
    '                Dt.Rows.Add(dr)
    '            Next

    '        Else

    '            tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    '            For i As Integer = 0 To tax.Length - 1
    '                Dim dr As DataRow = Dt.NewRow()
    '                dr("PaxType") = PaxType
    '                dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
    '                dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
    '                dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '                Dt.Rows.Add(dr)
    '            Next
    '            ''Baggage
    '            Array.Clear(tax, 0, tax.Length)
    '            tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    '            For i As Integer = 0 To tax.Length - 1
    '                Dim dr As DataRow = Dt.NewRow()
    '                dr("PaxType") = PaxType
    '                dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
    '                dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
    '                dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '                Dt.Rows.Add(dr)
    '            Next

    '        End If


    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    '    'Dim MB() As String
    '    'Try
    '    '    MB = Text.Split("#")
    '    '    Dim tax() As String
    '    '    tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    '    '    For i As Integer = 0 To tax.Length - 1
    '    '        Dim dr As DataRow = Dt.NewRow()
    '    '        If Provider.Trim() = "TB" Or Provider.Trim() = "YA" Then

    '    '            dr("PaxType") = PaxType
    '    '            dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
    '    '            dr("MealCode") = tax(i).Split("@")(0).Split(":")(0)
    '    '            dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '    '            dr("MealDesc") = tax(i).Split("@")(0).Split(":")(1)
    '    '            '' dr("MealCategory") = tax(i).Split("@")(0).Split(":")(1)
    '    '            ''dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(2)
    '    '            ''VGML:2_2_VEG MEAL_BOM_DEL:250:1@250}#XBPB:2_2_10_BOM_DEL:1750:1@1750}
    '    '        Else

    '    '            dr("PaxType") = PaxType
    '    '            dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
    '    '            dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
    '    '            dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))

    '    '        End If

    '    '        Dt.Rows.Add(dr)
    '    '    Next
    '    '    ''Baggage
    '    '    Array.Clear(tax, 0, tax.Length)
    '    '    tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    '    '    For i As Integer = 0 To tax.Length - 1
    '    '        Dim dr As DataRow = Dt.NewRow()

    '    '        If Provider.Trim() = "TB" Or Provider.Trim() = "YA" Then
    '    '            dr("PaxType") = PaxType
    '    '            dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
    '    '            dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
    '    '            dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
    '    '            dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(1)
    '    '            dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
    '    '            dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(2)

    '    '        Else

    '    '            dr("PaxType") = PaxType
    '    '            dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
    '    '            dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
    '    '            dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))

    '    '        End If

    '    '        Dt.Rows.Add(dr)
    '    '    Next

    '    'Catch ex As Exception
    '    '    clsErrorLog.LogInfo(ex)
    '    'End Try


    'End Sub
    Public Sub Split_MB_Detail(ByVal Text As String, ByVal Pxcnt As Integer, ByRef Dt As DataTable, ByVal PaxType As String, ByVal VC As String)

        Dim MB() As String
        Try
            MB = Text.Split("#")
            Dim tax() As String

            If VC = "G88" Then
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("MealCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next

                Array.Clear(tax, 0, tax.Length)
                tax = MB(2).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("SeatCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("SeatPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("SeatDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("SeatCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("SeatPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next

            ElseIf VC = "IX" Then
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("MealCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("MealPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("BaggageCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("BaggagePriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next

                Array.Clear(tax, 0, tax.Length)
                tax = MB(2).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("SeatCode") = tax(i).Split("@")(0).Split(":")(0)
                    dr("SeatPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("SeatDesc") = tax(i).Split("@")(0).Split(":")(2)
                    dr("SeatCategory") = tax(i).Split("@")(0).Split(":")(1)
                    dr("SeatPriceWithNoTax") = tax(i).Split("@")(0).Split(":")(4)
                    Dt.Rows.Add(dr)
                Next
            ElseIf (VC = "AK") Then
                '"CPML:Complimentary Meal_CCU_DEL:0:1@0"  Meal
                ' baglist(i).Code + ":" + baglist(i).Description + "_" + baglist(i).Destination + "_" + baglist(i).Origin + ":" + baglist(i).Amount.ToString() + ":" + Adti.ToString()))
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = tax(i).Split(":")(1)
                    dr("MealCategory") = ""
                    dr("MealPriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
                ''Baggage
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Split(":")(3)
                    dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = tax(i).Split(":")(1)
                    dr("BaggageCategory") = ""
                    dr("BaggagePriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
            Else
                'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")),
                '                             dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"),
                '                             dr("MealCategory"), dr("MealPriceWithNoTax"), dr("SeatCode"), dr("SeatPrice"), dr("SeatDesc"), dr("SeatCategory"), dr("SeatPriceWithNoTax"))
                tax = MB(0).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
                    dr("MealCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("MealPrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("MealDesc") = ""
                    dr("MealCategory") = ""
                    dr("MealPriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next
                ''Baggage
                Array.Clear(tax, 0, tax.Length)
                tax = MB(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To tax.Length - 1
                    Dim dr As DataRow = Dt.NewRow()
                    dr("PaxType") = PaxType
                    dr("PaxID") = tax(i).Split("@")(0).Substring(4, 1)
                    dr("BaggageCode") = tax(i).Split("@")(0).Substring(0, 4)
                    dr("BaggagePrice") = Convert.ToDecimal(tax(i).Split("@")(1))
                    dr("BaggageDesc") = ""
                    dr("BaggageCategory") = ""
                    dr("BaggagePriceWithNoTax") = 0
                    Dt.Rows.Add(dr)
                Next

            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Public Function getTableColumn(ByRef dt As DataTable) As DataTable

        Try

            Try
                dt.Columns.Add("BookingRefNo", GetType(String))
                dt.Columns.Add("Flt_HeaderID", GetType(String))
                dt.Columns.Add("PaxID", GetType(Integer))
                dt.Columns.Add("PaxType", GetType(String)) 'Extra
                dt.Columns.Add("TripType", GetType(String))
                dt.Columns.Add("MealCode", GetType(String))
                dt.Columns.Add("MealPrice", GetType(Decimal))
                dt.Columns.Add("MealDesc", GetType(String))
                dt.Columns.Add("MealCategory", GetType(String))
                dt.Columns.Add("MealPriceWithNoTax", GetType(Decimal))
                dt.Columns.Add("BaggageCode", GetType(String))
                dt.Columns.Add("BaggagePrice", GetType(Decimal))
                dt.Columns.Add("AirLineCode", GetType(String))
                dt.Columns.Add("BaggageDesc", GetType(String))
                dt.Columns.Add("BaggageCategory", GetType(String))
                dt.Columns.Add("BaggagePriceWithNoTax", GetType(Decimal))
                dt.Columns.Add("SeatCode", GetType(String))
                dt.Columns.Add("SeatPrice", GetType(Decimal))
                dt.Columns.Add("SeatDesc", GetType(String))
                dt.Columns.Add("SeatCategory", GetType(String))
                dt.Columns.Add("SeatPriceWithNoTax", GetType(Decimal))

                Return dt
            Catch ex As Exception
                Return dt
            End Try

            Return dt
        Catch ex As Exception
            Return dt
        End Try
    End Function
    Public Function CreateFinalTable(ByVal Dt As DataTable, ByVal Adult As Integer, ByVal Child As Integer, ByVal Paxdt As DataTable, ByVal OID As String, ByVal Trip As String, ByVal VC As String) As DataTable
        Dim Ft As New DataTable
        getTableColumn(Ft)
        Try
            If (VC = "AK") Then
                For i As Integer = 1 To Adult
                    Dim fltml As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND MealPrice Is Not NULL")
                    Dim fltbg As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND BaggageCode Is Not NULL")
                    Dim fltse As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND SeatPrice Is Not NULL")

                    Dim rownummber As Integer = 0
                    If fltml.Count >= fltbg.Count And fltml.Count >= fltse.Count Then
                        rownummber = fltml.Count
                    ElseIf fltbg.Count >= fltml.Count And fltbg.Count >= fltse.Count Then
                        rownummber = fltbg.Count
                    Else
                        rownummber = fltse.Count
                    End If

                    For j As Integer = 0 To rownummber - 1
                        Dim dr As DataRow = Ft.NewRow()
                        dr("PaxID") = Paxdt.Rows(i - 1)("PaxID")
                        dr("Flt_HeaderID") = Paxdt.Rows(i - 1)("Flt_HeaderID")
                        If (fltml.Length > 0) Then
                            dr("MealCode") = fltml(j)("MealCode")
                            dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                            dr("MealDesc") = fltml(j)("MealDesc")
                            dr("MealCategory") = fltml(j)("MealCategory")
                            dr("MealPriceWithNoTax") = fltml(j)("MealPriceWithNoTax")
                        Else
                            dr("MealCode") = ""
                            dr("MealPrice") = 0
                            dr("MealDesc") = ""
                            dr("MealCategory") = ""
                            dr("MealPriceWithNoTax") = 0
                        End If
                        If (fltbg.Length > 0) Then
                            dr("BaggageCode") = fltbg(j)("BaggageCode")
                            dr("BaggagePrice") = Convert.ToDecimal(fltbg(j)("BaggagePrice"))
                            dr("BaggageDesc") = fltbg(j)("BaggageDesc")
                            dr("BaggageCategory") = fltbg(j)("BaggageCategory")
                            dr("BaggagePriceWithNoTax") = fltbg(j)("BaggagePriceWithNoTax")

                        Else
                            dr("BaggageCode") = ""
                            dr("BaggagePrice") = 0
                            dr("BaggageDesc") = ""
                            dr("BaggageCategory") = ""
                            dr("BaggagePriceWithNoTax") = 0
                        End If

                        If (fltse.Length > 0) Then
                            dr("SeatCode") = fltse(0)("SeatCode")
                            dr("SeatPrice") = Convert.ToDecimal(fltse(j)("SeatPrice"))
                            dr("SeatDesc") = fltse(j)("SeatDesc")
                            dr("SeatCategory") = fltse(j)("SeatCategory")
                            dr("SeatPriceWithNoTax") = fltse(j)("SeatPriceWithNoTax")
                        Else
                            dr("SeatCode") = ""
                            dr("SeatPrice") = 0
                            dr("SeatDesc") = ""
                            dr("SeatCategory") = ""
                            dr("SeatPriceWithNoTax") = 0
                        End If

                        dr("TripType") = Trip
                        Ft.Rows.Add(dr)

                        'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                        If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                            Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LABAK:
                            If (ret > 0) Then
                            Else
                                ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                                GoTo LABAK
                            End If
                        End If
                    Next
                Next
                If (Child > 0) Then
                    For i As Integer = 1 To Child
                        Dim fltml As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND MealCode Is Not NULL")
                        Dim fltbg As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND BaggageCode Is Not NULL")
                        Dim fltse As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND SeatCode Is Not NULL")

                        Dim rownummber As Integer = 0
                        If fltml.Count >= fltbg.Count And fltml.Count >= fltse.Count Then
                            rownummber = fltml.Count
                        ElseIf fltbg.Count >= fltml.Count And fltbg.Count >= fltse.Count Then
                            rownummber = fltbg.Count
                        Else
                            rownummber = fltse.Count
                        End If

                        For j As Integer = 0 To rownummber - 1
                            Dim dr As DataRow = Ft.NewRow()
                            dr("PaxID") = Paxdt.Rows((Adult + i) - 1)("PaxID")
                            dr("Flt_HeaderID") = Paxdt.Rows((Adult + i) - 1)("Flt_HeaderID")
                            If (fltml.Length > 0) Then
                                dr("MealCode") = fltml(j)("MealCode")
                                dr("MealPrice") = Convert.ToDecimal(fltml(j)("MealPrice"))
                                dr("MealDesc") = fltml(j)("MealDesc")
                                dr("MealCategory") = fltml(j)("MealCategory")
                                dr("MealPriceWithNoTax") = fltml(j)("MealPriceWithNoTax")
                            Else
                                dr("MealCode") = ""
                                dr("MealPrice") = 0
                                dr("MealDesc") = ""
                                dr("MealCategory") = ""
                                dr("MealPriceWithNoTax") = 0
                            End If
                            If (fltbg.Length > 0) Then
                                dr("BaggageCode") = fltbg(j)("BaggageCode")
                                dr("BaggagePrice") = Convert.ToDecimal(fltbg(j)("BaggagePrice"))
                                dr("BaggageDesc") = fltbg(j)("BaggageDesc")
                                dr("BaggageCategory") = fltbg(j)("BaggageCategory")
                                dr("BaggagePriceWithNoTax") = fltbg(j)("BaggagePriceWithNoTax")
                            Else
                                dr("BaggageCode") = ""
                                dr("BaggagePrice") = 0
                                dr("BaggageDesc") = ""
                                dr("BaggageCategory") = ""
                                dr("BaggagePriceWithNoTax") = 0
                            End If

                            If (fltse.Length > 0) Then
                                dr("SeatCode") = fltse(0)("SeatCode")
                                dr("SeatPrice") = Convert.ToDecimal(fltse(j)("SeatPrice"))
                                dr("SeatDesc") = fltse(j)("SeatDesc")
                                dr("SeatCategory") = fltse(j)("SeatCategory")
                                dr("SeatPriceWithNoTax") = fltse(j)("SeatPriceWithNoTax")
                            Else
                                dr("SeatCode") = ""
                                dr("SeatPrice") = 0
                                dr("SeatDesc") = ""
                                dr("SeatCategory") = ""
                                dr("SeatPriceWithNoTax") = 0
                            End If
                            dr("TripType") = Trip
                            Ft.Rows.Add(dr)
                            'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                            If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                                Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
LAB2AK:
                                If (ret > 0) Then
                                Else
                                    ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), "", "0", "", "", "0")
                                    GoTo LAB2AK
                                End If
                            End If
                        Next
                    Next

                End If
            Else
                For i As Integer = 1 To Adult

                    Dim fltml As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND MealPrice >0")
                    Dim fltbg As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND BaggagePrice >0")
                    Dim fltse As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND SeatPrice >0")
                    Dim dr As DataRow = Ft.NewRow()
                    dr("PaxID") = Paxdt.Rows(i - 1)("PaxID")
                    dr("Flt_HeaderID") = Paxdt.Rows(i - 1)("Flt_HeaderID")
                    If (fltml.Length > 0) Then
                        dr("MealCode") = fltml(0)("MealCode").ToString()
                        dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                        dr("MealDesc") = fltml(0)("MealDesc").ToString()
                        dr("MealCategory") = fltml(0)("MealCategory").ToString()
                        dr("MealPriceWithNoTax") = fltml(0)("MealPriceWithNoTax")
                    Else
                        dr("MealCode") = ""
                        dr("MealPrice") = 0
                        dr("MealDesc") = ""
                        dr("MealCategory") = ""
                        dr("MealPriceWithNoTax") = 0
                    End If
                    If (fltbg.Length > 0) Then
                        dr("BaggageCode") = fltbg(0)("BaggageCode").ToString()
                        dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
                        dr("BaggageDesc") = fltbg(0)("BaggageDesc").ToString()
                        dr("BaggageCategory") = fltbg(0)("BaggageCategory").ToString()
                        dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")

                    Else
                        dr("BaggageCode") = ""
                        dr("BaggagePrice") = 0
                        dr("BaggageDesc") = ""
                        dr("BaggageCategory") = ""
                        dr("BaggagePriceWithNoTax") = 0
                    End If

                    If (fltse.Length > 0) Then
                        dr("SeatCode") = fltse(0)("SeatCode").ToString()
                        dr("SeatPrice") = Convert.ToDecimal(fltse(0)("SeatPrice"))
                        dr("SeatDesc") = fltse(0)("SeatDesc").ToString()
                        dr("SeatCategory") = fltse(0)("SeatCategory").ToString()
                        dr("SeatPriceWithNoTax") = fltse(0)("SeatPriceWithNoTax")
                    Else
                        dr("SeatCode") = ""
                        dr("SeatPrice") = 0
                        dr("SeatDesc") = ""
                        dr("SeatCategory") = ""
                        dr("SeatPriceWithNoTax") = 0
                    End If

                    dr("TripType") = Trip
                    Ft.Rows.Add(dr)
                    'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                    If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                        Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), dr("SeatCode"), dr("SeatPrice"), dr("SeatDesc"), dr("SeatCategory"), dr("SeatPriceWithNoTax"))
LAB:
                        If (ret > 0) Then
                        Else
                            ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), dr("SeatCode"), dr("SeatPrice"), dr("SeatDesc"), dr("SeatCategory"), dr("SeatPriceWithNoTax"))
                            GoTo LAB
                        End If
                    End If
                Next
                If (Child > 0) Then
                    For i As Integer = 1 To Child
                        Dim fltml As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND MealPrice >0")
                        Dim fltbg As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND BaggagePrice >0")
                        Dim fltse As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND SeatPrice >0")
                        Dim dr As DataRow = Ft.NewRow()
                        dr("PaxID") = Paxdt.Rows((Adult + i) - 1)("PaxID")
                        dr("Flt_HeaderID") = Paxdt.Rows((Adult + i) - 1)("Flt_HeaderID")
                        If (fltml.Length > 0) Then
                            dr("MealCode") = fltml(0)("MealCode")
                            dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
                            dr("MealDesc") = fltml(0)("MealDesc")
                            dr("MealCategory") = fltml(0)("MealCategory")
                            dr("MealPriceWithNoTax") = fltml(0)("MealPriceWithNoTax")
                        Else
                            dr("MealCode") = ""
                            dr("MealPrice") = 0
                            dr("MealDesc") = ""
                            dr("MealCategory") = ""
                            dr("MealPriceWithNoTax") = 0
                        End If
                        If (fltbg.Length > 0) Then
                            dr("BaggageCode") = fltbg(0)("BaggageCode")
                            dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
                            dr("BaggageDesc") = fltbg(0)("BaggageDesc")
                            dr("BaggageCategory") = fltbg(0)("BaggageCategory")
                            dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")
                        Else
                            dr("BaggageCode") = ""
                            dr("BaggagePrice") = 0
                            dr("BaggageDesc") = ""
                            dr("BaggageCategory") = ""
                            dr("BaggagePriceWithNoTax") = 0
                        End If

                        If (fltse.Length > 0) Then
                            dr("SeatCode") = fltse(0)("SeatCode")
                            dr("SeatPrice") = Convert.ToDecimal(fltse(0)("SeatPrice"))
                            dr("SeatDesc") = fltse(0)("SeatDesc")
                            dr("SeatCategory") = fltse(0)("SeatCategory")
                            dr("SeatPriceWithNoTax") = fltse(0)("SeatPriceWithNoTax")
                        Else
                            dr("SeatCode") = ""
                            dr("SeatPrice") = 0
                            dr("SeatDesc") = ""
                            dr("SeatCategory") = ""
                            dr("SeatPriceWithNoTax") = 0
                        End If
                        dr("TripType") = Trip
                        Ft.Rows.Add(dr)
                        'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
                        If (fltbg.Length > 0 Or fltml.Length > 0 Or fltse.Length > 0) Then
                            Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), dr("SeatCode"), dr("SeatPrice"), dr("SeatDesc"), dr("SeatCategory"), dr("SeatPriceWithNoTax"))
LAB2:
                            If (ret > 0) Then
                            Else
                                ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), Convert.ToDecimal(dr("BaggagePriceWithNoTax")), dr("MealDesc"), dr("MealCategory"), dr("MealPriceWithNoTax"), dr("SeatCode"), dr("SeatPrice"), dr("SeatDesc"), dr("SeatCategory"), dr("SeatPriceWithNoTax"))
                                GoTo LAB2
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try

        Return Ft
    End Function
    '    Public Function CreateFinalTable(ByVal Dt As DataTable, ByVal Adult As Integer, ByVal Child As Integer, ByVal Paxdt As DataTable, ByVal OID As String, ByVal Trip As String, ByVal VC As String, ByVal Prvdr As String) As DataTable
    '        Dim Ft As New DataTable
    '        getTableColumn(Ft)
    '        Try

    '            For i As Integer = 1 To Adult

    '                Dim fltml As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND MealPrice >0")
    '                Dim fltbg As DataRow() = Dt.Select("PaxType ='ADT' AND PaxID =" + i.ToString() + "AND BaggagePrice >0") 'AND BaggagePrice >0
    '                Dim dr As DataRow = Ft.NewRow()
    '                dr("PaxID") = Paxdt.Rows(i - 1)("PaxID")
    '                dr("Flt_HeaderID") = Paxdt.Rows(i - 1)("Flt_HeaderID")
    '                If (fltml.Length > 0) Then
    '                    dr("MealCode") = fltml(0)("MealCode")
    '                    dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
    '                    dr("MealDesc") = Convert.ToString(fltml(0)("MealDesc"))
    '                Else
    '                    dr("MealCode") = ""
    '                    dr("MealPrice") = 0
    '                    dr("MealDesc") = ""
    '                End If
    '                If (fltbg.Length > 0) Then
    '                    dr("BaggageCode") = fltbg(0)("BaggageCode")
    '                    dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
    '                    dr("BaggageDesc") = fltbg(0)("BaggageDesc")
    '                    dr("BaggageCategory") = fltbg(0)("BaggageCategory")
    '                    dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")

    '                Else
    '                    dr("BaggageCode") = ""
    '                    dr("BaggagePrice") = 0
    '                    dr("BaggageDesc") = ""
    '                    dr("BaggageCategory") = ""
    '                    dr("BaggagePriceWithNoTax") = 0
    '                End If
    '                dr("TripType") = Trip
    '                Ft.Rows.Add(dr)
    '                'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
    '                Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), dr("BaggagePriceWithNoTax"), dr("MealDesc"), Prvdr)
    'LAB:
    '                If (ret > 0) Then
    '                Else
    '                    ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), dr("BaggagePriceWithNoTax"), dr("MealDesc"), Prvdr)
    '                    GoTo LAB
    '                End If
    '            Next
    '            If (Child > 0) Then
    '                For i As Integer = 1 To Child
    '                    Dim fltml As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND MealPrice >0")
    '                    Dim fltbg As DataRow() = Dt.Select("PaxType ='CHD' AND PaxID =" + i.ToString() + "AND BaggagePrice >0") 'AND BaggagePrice >0
    '                    Dim dr As DataRow = Ft.NewRow()
    '                    dr("PaxID") = Paxdt.Rows((Adult + i) - 1)("PaxID")
    '                    dr("Flt_HeaderID") = Paxdt.Rows((Adult + i) - 1)("Flt_HeaderID")
    '                    If (fltml.Length > 0) Then
    '                        dr("MealCode") = fltml(0)("MealCode")
    '                        dr("MealPrice") = Convert.ToDecimal(fltml(0)("MealPrice"))
    '                        dr("MealDesc") = Convert.ToString(fltml(0)("MealDesc"))
    '                    Else
    '                        dr("MealCode") = ""
    '                        dr("MealPrice") = 0
    '                        dr("MealDesc") = ""
    '                    End If
    '                    If (fltbg.Length > 0) Then
    '                        dr("BaggageCode") = fltbg(0)("BaggageCode")
    '                        dr("BaggagePrice") = Convert.ToDecimal(fltbg(0)("BaggagePrice"))
    '                        dr("BaggageDesc") = fltbg(0)("BaggageDesc")
    '                        dr("BaggageCategory") = fltbg(0)("BaggageCategory")
    '                        dr("BaggagePriceWithNoTax") = fltbg(0)("BaggagePriceWithNoTax")

    '                    Else
    '                        dr("BaggageCode") = ""
    '                        dr("BaggagePrice") = 0
    '                        dr("BaggageDesc") = ""
    '                        dr("BaggageCategory") = ""
    '                        dr("BaggagePriceWithNoTax") = 0
    '                    End If
    '                    dr("TripType") = Trip
    '                    Ft.Rows.Add(dr)
    '                    'objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC)
    '                    Dim ret As Integer = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), dr("BaggagePriceWithNoTax"), dr("MealDesc"), Prvdr)
    'LAB2:
    '                    If (ret > 0) Then
    '                    Else
    '                        ret = objDA.insert_MEAL_BAGDetails(OID, dr("Flt_HeaderID"), Convert.ToInt32(dr("PaxID")), dr("TripType"), dr("MealCode"), Convert.ToDouble(dr("MealPrice")), dr("BaggageCode"), dr("BaggagePrice"), VC, dr("BaggageDesc"), dr("BaggageCategory"), dr("BaggagePriceWithNoTax"), dr("MealDesc"), Prvdr)
    '                        GoTo LAB2
    '                    End If
    '                Next

    '            End If
    '        Catch ex As Exception

    '        End Try

    '        Return Ft
    '    End Function
    'Private Function SELL_SSR(ByVal TrackId As String) As String
    '    Dim SSRPRICE As String = "", SJKAMT As String = ""
    '    Dim Signature As String = ""
    '    Dim Trip As Integer = 1
    '    Dim Diff As Decimal = 0
    '    Try

    '        Dim FltDs As DataSet = objDA.GetFltDtls(TrackId, Session("UID"))
    '        Dim PaxDs As DataSet = objDA.GetPaxDetails(TrackId)
    '        Dim VC As String = FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString.Trim()
    '        Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
    '        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
    '        Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
    '        If (MBDT.Tables(0).Rows.Count > 0) Then
    '            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
    '                MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
    '                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
    '                MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
    '                OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
    '            Next
    '        End If

    '        Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
    '        Dim dsCrd As DataSet = objSql.GetCredentials(VC)
    '        Dim Org As String = "", Dest As String = ""
    '        Dim objInputs As New STD.Shared.FlightSearch
    '        If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
    '        If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
    '        objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
    '        objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
    '        objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
    '        objInputs.HidTxtAirLine = VC
    '        Dim inx As Integer = 0
    '        If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
    '            inx = 1
    '            Trip = 2
    '        End If

    '        Dim seginfo As New ArrayList()

    '        Dim FNO As String = ""
    '        Dim JSK(inx), FSK(inx) As String 'CC(inx), FNO(inx), DD(inx) 

    '        Dim dt = FltDs.Tables(0).DefaultView.ToTable(True, "FlightIdentification") 'Sorted By FNo
    '        For jj As Integer = 0 To dt.Rows.Count - 1
    '            Dim dt1 = FltDs.Tables(0).Select("FlightIdentification='" & dt.Rows(jj)("FlightIdentification") & "'", "")
    '            FNO = dt1(0)("FlightIdentification").Trim()
    '            Dim Seg As New Dictionary(Of String, String)
    '            Seg.Add("FNO", FNO)
    '            Seg.Add("STD", dt1(0)("depdatelcc"))
    '            Seg.Add("Departure", dt1(0)("DepartureLocation"))
    '            Seg.Add("Arrival", dt1(dt1.Length - 1)("ArrivalLocation"))
    '            Seg.Add("VC", VC)
    '            Seg.Add("Flight", dt1(0)("Flight"))
    '            seginfo.Add(Seg)
    '        Next
    '        For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
    '            If (ii = 0) Then
    '                Dim Seg As New Dictionary(Of String, String)
    '                Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
    '                Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
    '                'Changes 8March
    '                'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
    '                objInputs.HidTxtDepCity = Org
    '                objInputs.HidTxtArrCity = Dest
    '            End If
    '            If (Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom").ToString()) Then
    '                JSK(0) = FltDs.Tables(0).Rows(ii)("SNO")
    '                FSK(0) = FltDs.Tables(0).Rows(ii)("Searchvalue")
    '            ElseIf (Org = FltDs.Tables(0).Rows(ii)("OrgDestTo").ToString()) Then
    '                JSK(1) = FltDs.Tables(0).Rows(ii)("SNO")
    '                FSK(1) = FltDs.Tables(0).Rows(ii)("Searchvalue")
    '            End If
    '        Next
    '        objInputs.HidTxtAirLine = VC
    '        Dim Xml As New Dictionary(Of String, String)
    '        If (objInputs.Infant > 0) Then
    '            objInputs.Infant = 0 ' Set Infant to 0
    '        End If
    '        If (MBPR > 0) Then
    '            If (VC = "6E") Then
    '                Dim obj6E As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 340)
    '                Signature = obj6E.Spice_Login()
    '                SJKAMT = obj6E.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml)
    '                SSRPRICE = obj6E.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0))
    '                obj6E.Spice_Logout(Signature)
    '            ElseIf (VC = "SG") Then
    '                Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0)
    '                Signature = objSG.Spice_Login()
    '                SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml)
    '                SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0))
    '                objSG.Spice_Logout(Signature)
    '            End If
    '            Try
    '                objSql.Insert_SSR_Log(TrackId, Signature, Xml("SSR"), SSRPRICE) ' Enter Log
    '            Catch ex As Exception
    '                SSRPRICE = "FAILURE"
    '            End Try
    '            If (SSRPRICE <> "FAILURE") Then
    '                If (Convert.ToDecimal(SSRPRICE) > (OriginalTF)) Then
    '                    ' Logic to Update MB table
    '                    Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
    '                    'Dim DifPerpax As Decimal = Math.Round(Diff / ((objInputs.Adult + objInputs.Child) * Trip), 0)
    '                    objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
    '                End If
    '            Else
    '                'Alert(Code')
    '                If (Xml("SSR").Contains("The requested class of service is sold out")) Then
    '                    Response.Redirect("../International/BookingMsg.aspx?msg=1")
    '                ElseIf (Xml("SSR").Contains("not available on flight")) Then
    '                    Response.Redirect("../International/BookingMsg.aspx?msg=ML")
    '                End If
    '            End If
    '            objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString())
    '        End If
    '    Catch ex As Exception
    '        Response.Redirect("../International/BookingMsg.aspx?msg=1")
    '    End Try
    '    Return SSRPRICE
    'End Function
    Private Function SELL_SSR(ByVal TrackId As String) As String
        Dim SSRPRICE As String = "", SJKAMT As String = "" ', ViaArrv As String = ""
        Dim Signature As String = ""
        Dim Trip As Integer = 1
        Dim Diff As Decimal = 0
        Dim Bag As Boolean = False
        Dim SSRCode As String = ""
        Try

            Dim FltDs As DataSet = objDA.GetFltDtls(TrackId, Session("UID"))
            Dim PaxDs As DataSet = objDA.GetPaxDetails(TrackId)
            Dim VC As String = FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString.Trim()
            Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
            Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
            Dim MBPR As Decimal = 0, MealPr As Decimal = 0, BgPr As Decimal = 0
            If (MBDT.Tables(0).Rows.Count > 0) Then
                For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                    MealPr = MealPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
                    BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    MBPR = MBPR + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                Next
            End If

            Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
            'Dim dsCrd As DataSet = objSql.GetCredentials(VC, "")
            Dim dsCrd As DataSet = objSql.GetCredentials(VC, Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
            Dim Org As String = "", Dest As String = ""
            Dim objInputs As New STD.Shared.FlightSearch
            If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
            objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
            objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
            objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
            objInputs.HidTxtAirLine = VC
            If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
            End If
            SSRCode = Convert.ToString(FltDs.Tables(0).Rows(0)("SSRCode"))

            Dim inx As Integer = 0
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                inx = 1
                Trip = 2
            End If

            Dim seginfo As New ArrayList()
            Dim Utlobj As New SpiceIndigoUTL()

            Dim FNO As String = ""
            Dim JSK(inx), FSK(inx) As String
            Dim ViaArr(inx) As String

            Dim dt = FltDs.Tables(0).DefaultView.ToTable(True, "FlightIdentification") 'Sorted By FNo
            For jj As Integer = 0 To dt.Rows.Count - 1
                Dim dt1 = FltDs.Tables(0).Select("FlightIdentification='" & dt.Rows(jj)("FlightIdentification") & "'", "")
                FNO = dt1(0)("FlightIdentification").Trim()
                Dim Seg As New Dictionary(Of String, String)
                Seg.Add("FNO", FNO)
                Seg.Add("STD", dt1(0)("depdatelcc"))
                Seg.Add("Departure", dt1(0)("DepartureLocation"))
                Seg.Add("Arrival", dt1(dt1.Length - 1)("ArrivalLocation"))
                Seg.Add("VC", VC)
                Seg.Add("Flight", dt1(0)("Flight"))
                seginfo.Add(Seg)

            Next
            For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                If (ii = 0) Then
                    Dim Seg As New Dictionary(Of String, String)
                    Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
                    Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
                    'Changes 8March
                    'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
                    objInputs.HidTxtDepCity = Org
                    objInputs.HidTxtArrCity = Dest
                End If
                If (Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom").ToString()) Then
                    JSK(0) = FltDs.Tables(0).Rows(ii)("SNO")
                    FSK(0) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                    ViaArr(0) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                ElseIf (Org = FltDs.Tables(0).Rows(ii)("OrgDestTo").ToString()) Then
                    JSK(1) = FltDs.Tables(0).Rows(ii)("SNO")
                    FSK(1) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                    ViaArr(1) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                End If
            Next

            objInputs.HidTxtAirLine = VC
            objInputs.Cabin = FltDs.Tables(0).Rows(0)("AdtCabin").ToString().ToUpper()
            ''Devesh add for PROMO CODE
            Dim FareTypeSettingsList As List(Of FareTypeSettings)
            Dim FT As String() = Nothing
            Dim PROMOCODE As String = ""
            Dim AppliedOn As String = ""
            Try
                If [String].IsNullOrEmpty(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString()) = False Then
                    PROMOCODE = Split(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString(), "/")(0)
                    AppliedOn = Split(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString(), "/")(3)
                End If
                If AppliedOn = "BOTH" Then
                    Dim objfltBal As New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                    FareTypeSettingsList = objfltBal.GetFareTypeSettings("", FltDs.Tables(0).Rows(0)("Trip").ToString(), "")
                    FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = VC AndAlso x.Trip = FltDs.Tables(0).Rows(0)("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")).ToUpper().Trim()).ToList()
                    FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                End If
            Catch ex As Exception

            End Try
            ''END Devesh add for PROMO CODE

            Dim Xml As New Dictionary(Of String, String)
            If (objInputs.Infant > 0) Then
                objInputs.Infant = 0 ' Set Infant to 0
            End If
            If (MBPR > 0 Or (VC = "SG" AndAlso objInputs.Cabin = "BUSINESS")) Then
                If (VC = "6E") Then
                    If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                        Dim obj6E As New STD.BAL._6ENAV420._6ENAV(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 420) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = obj6E.Spice_Login()
                        SJKAMT = obj6E.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = obj6E.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        obj6E.Spice_Logout(Signature)
                    Else
                        Dim obj6E As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 340) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = obj6E.Spice_Login()
                        SJKAMT = obj6E.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = obj6E.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        obj6E.Spice_Logout(Signature)
                    End If
                ElseIf (VC = "SG") Then
                    If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                        Dim objSG As New STD.BAL.SGNAV420.SGNAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = objSG.Spice_Login()
                        SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        objSG.Spice_Logout(Signature)
                    Else
                        Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = objSG.Spice_Login()
                        SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                        SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr, Bag, SSRCode)
                        objSG.Spice_Logout(Signature)
                    End If
                ElseIf (VC = "G8") Then
                    Dim objSG As New STD.BAL.G8NAV.G8NAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 411) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    Signature = objSG.Spice_Login()
                    SJKAMT = objSG.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, Xml, FT, PROMOCODE, AppliedOn)
                    SSRPRICE = objSG.Spice_Sell_SSR(Signature, objInputs, seginfo, Xml, PaxDs.Tables(0), MBDT.Tables(0), ViaArr)
                    objSG.Spice_Logout(Signature)
                ElseIf (VC = "AK") Then
                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("AK", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                    SSRPRICE = objAirAsia.SellSSR(CredencialDs, FltDs.Tables(0), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, MBDT.Tables(0), Xml)
                End If
                Try
                    If (VCOB = "IX") Then
                        objSql.Insert_SSR_Log(TrackId, Signature, "", SSRPRICE, Convert.ToDecimal(FltDs.Tables(0).Rows(0)("totFare"))) ' Enter Log

                    Else
                        objSql.Insert_SSR_Log(TrackId, Signature, Xml("SSR"), SSRPRICE, Convert.ToDecimal(FltDs.Tables(0).Rows(0)("totFare"))) ' Enter Log

                    End If
                Catch ex As Exception
                    SSRPRICE = "FAILURE"
                End Try
                If (SSRPRICE <> "FAILURE") Then
                    If (VCOB = "IX") Then
                    Else
                        If (Convert.ToDecimal(SSRPRICE) > (OriginalTF)) Then
                            ' Logic to Update MB table
                            Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
                            'Dim DifPerpax As Decimal = Math.Round(Diff / ((objInputs.Adult + objInputs.Child) * Trip), 0)
                            objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
                        End If
                    End If
                Else
                    SSRPRICE = "FAILURE"
                    If (Xml("SSR").Contains("The requested class of service is sold out")) Then
                        Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
                    ElseIf (Xml("SSR").Contains("not available on flight")) Then
                        Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                    End If
                End If
                If (VCOB = "IX") Then
                    Dim msg As String = UpadteFZFare(TrackId)
                Else
                    objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString())
                End If
            Else
                If (VC = "AK") Then
                    Dim objAirAsia As New GALWS.AirAsia.AirAsiaSellSSR_AssignSeat()
                    Dim CredencialDs As DataSet = objSql.GetCredentials("AK", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                    SSRPRICE = objAirAsia.SellSSR(CredencialDs, FltDs.Tables(0), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, MBDT.Tables(0), Xml)
                    Try
                        objSql.Insert_SSR_Log(TrackId, Signature, Xml("SSR"), SSRPRICE, Convert.ToDecimal(FltDs.Tables(0).Rows(0)("totFare"))) ' Enter Log
                    Catch ex As Exception
                        SSRPRICE = "FAILURE"
                    End Try
                    If (SSRPRICE <> "FAILURE") Then
                        If (Convert.ToDecimal(SSRPRICE) <> (OriginalTF)) Then
                            ' Logic to Update MB table
                            Diff = (Convert.ToDecimal(SSRPRICE) - (OriginalTF))
                            'Dim DifPerpax As Decimal = Math.Round(Diff / ((objInputs.Adult + objInputs.Child) * Trip), 0)
                            objSql.Update_PAX_BG_Price(TrackId, Diff.ToString())
                        End If
                    Else
                        SSRPRICE = "FAILURE"
                        If (Xml("SSR").Contains("The requested class of service is sold out")) Then
                            Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
                        ElseIf (Xml("SSR").Contains("not available on flight")) Then
                            Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                        Else
                            Response.Redirect("../International/BookingMsg.aspx?msg=ML", False)
                        End If
                    End If
                    objSql.Update_NET_TOT_Fare(TrackId, (MBPR + Diff).ToString())
                End If
            End If
        Catch ex As Exception
            Response.Redirect("../International/BookingMsg.aspx?msg=1", False)
            SSRPRICE = "FAILURE"
        End Try
        Return SSRPRICE
    End Function

    Public Function UpadteFZFare(ByVal trackId As String) As String

        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(trackId, "")
        Dim BgPr As Decimal = 0
        If (MBDT.Tables(0).Rows.Count > 0) Then
            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
            Next

        End If
        Return objSql.Update_NET_TOT_Fare(trackId, (BgPr).ToString())


    End Function
    Private Function VCCount(ByVal dt As DataTable) As Integer
        Dim Count As Integer = (From row In dt.Rows Select row Where row("MarketingCarrier").ToString <> dt.Rows(0)("MarketingCarrier")).Count
        Return Count
    End Function
    Private Function VCCount1(ByVal drr As DataRow()) As Integer

        Dim Count1 As Integer = (From row In drr Select row Where row("MarketingCarrier").ToString <> drr(0)("MarketingCarrier")).Count
        Return Count1
    End Function
    Private Function MultiValueFunction(ByVal dt As DataTable, ByVal Type As String, Optional ByVal Position As Int32 = 0, Optional ByVal dtrow As String = "") As String
        Dim OutputString As String = ""
        If (Type = "Logo") Then
            OutputString = "../Airlogo/sm" & dt.Rows(0)("MarketingCarrier") & ".gif"
        ElseIf Type = "Airline" Then
            OutputString = dt.Rows(0)("AirlineName") & "(" & dt.Rows(0)("MarketingCarrier") & "-" & dt.Rows(0)("FlightIdentification") & ")"
        ElseIf Type = "Dep" Then
            If (dt.Rows(0)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(0)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(0)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(0)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Deprow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If

        ElseIf Type = "Arrrow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If
        ElseIf Type = "Arr" Then
            If (dt.Rows(0)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Depall" Then
            If (dt.Rows(Position)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(Position)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Arrall" Then
            If (dt.Rows(Position)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(Position)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("ArrivalTime").ToString().Substring(2, 2)

            End If
        End If
        Return OutputString
    End Function
    Public Function UpadteFare(ByVal trackId As String) As String

        Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(trackId, "")
        Dim BgPr As Decimal = 0
        If (MBDT.Tables(0).Rows.Count > 0) Then
            For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                BgPr = BgPr + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
            Next
        End If
        Return objSql.Update_NET_TOT_Fare(trackId, (BgPr).ToString())


    End Function


    'Protected Sub ddlStateGst_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim code As String = ddlStateGst.SelectedValue
    '        Dim name As String = ddlStateGst.SelectedItem.Text
    '        ''   BindCityGst(code)
    '    Catch ex As Exception
    '        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub

    Protected Function UpdateGSTDetail(ByVal OrderId As String, ByVal GSTNO As String, ByVal CompanyName As String, ByVal CompanyAddress As String, ByVal GSTPHoneNO As String, ByVal GST_Email As String, ByVal GSTRemark As String) As DataSet
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim cmd As New SqlCommand()
        Dim Paxds As New DataSet()
        Dim ii As Integer = 0
        Try
            If String.IsNullOrEmpty(GSTNO) Then
                CompanyName = ""
                CompanyAddress = ""
                GSTPHoneNO = ""
                GST_Email = ""
                'Else
            End If

            cmd.CommandText = "SP_UpdateGSTInHeader" 'OrderID varchar(50), @GSTNO varchar(50), @GST_Company_Name varchar(500), @GST_Company_Address varchar(500), @GST_PhoneNo varchar(20), @GST_Email varchar(200), @GSTRemark
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = OrderId
            cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = GSTNO
            cmd.Parameters.Add("@GST_Company_Name", SqlDbType.VarChar).Value = CompanyName.ToUpper()
            cmd.Parameters.Add("@GST_Company_Address", SqlDbType.VarChar).Value = CompanyAddress.ToUpper()
            cmd.Parameters.Add("@GST_PhoneNo", SqlDbType.VarChar).Value = GSTPHoneNO.ToUpper()
            cmd.Parameters.Add("@GST_Email", SqlDbType.VarChar).Value = GST_Email.ToUpper()
            cmd.Parameters.Add("@GSTRemark", SqlDbType.VarChar).Value = GSTRemark.ToUpper()
            cmd.Connection = con1
            con1.Open()
            ii = cmd.ExecuteNonQuery()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        Finally
            con1.Close()
            cmd.Dispose()
        End Try
        Return Paxds
    End Function

    'Protected Sub ddlStateGst_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim code As String = ddlStateGst.SelectedValue
    '        Dim name As String = ddlStateGst.SelectedItem.Text
    '        BindCityGst(code)
    '    Catch ex As Exception
    '        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub
    'Private Sub BindStateGst(countrycode As String)
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '    Try
    '        Dim dt As New DataTable()
    '        Dim da As New SqlDataAdapter((Convert.ToString("select STATEID as Code,STATE as Name from  [dbo].[Tbl_STATE] where COUNTRY='") & countrycode) + "'order by STATE", con)
    '        da.SelectCommand.CommandType = CommandType.Text
    '        da.Fill(dt)
    '        ddlStateGst.DataSource = dt
    '        ddlStateGst.DataValueField = "Code"
    '        ddlStateGst.DataTextField = "Name"
    '        ddlStateGst.DataBind()
    '        ddlStateGst.Items.Insert(0, New ListItem("Select State", "select"))
    '        BindCityGst(ddlStateGst.SelectedValue)

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub
    'Private Sub BindCityGst(stateCode As String)
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '    Try
    '        Dim dt As New DataTable()
    '        Dim da As New SqlDataAdapter((Convert.ToString("select CITY, STATEID from  [dbo].[TBL_CITY]  where STATEID='") & stateCode) + "'order by CITY", con)
    '        da.SelectCommand.CommandType = CommandType.Text
    '        da.Fill(dt)
    '        ddlCityGst.DataSource = dt
    '        ddlCityGst.DataValueField = "CITY"
    '        ddlCityGst.DataTextField = "CITY"
    '        ddlCityGst.DataBind()
    '        ddlCityGst.Items.Insert(0, New ListItem("Select City", "select"))
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Function UpadteSeatFare(ByRef seatlist As List(Of MySeatDetails), ByRef trackIds As String) As String

        Dim SeatFare As Decimal = 0
        If (seatlist.Count > 0) Then
            For jj As Integer = 0 To seatlist.Count - 1
                SeatFare = SeatFare + Convert.ToDecimal(seatlist(jj).Amount)
            Next
        End If
        Return objbal.Update_NET_TOT_Fare_Seat(trackIds, (SeatFare).ToString())

    End Function
End Class



