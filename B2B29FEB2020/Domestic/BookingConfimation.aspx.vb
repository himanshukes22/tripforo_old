Imports System.Data
Imports YatraBilling
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports STD.Shared
#Region "Pawan Kumar"
Imports ITZLib
Imports RedCorona.Cryptography
Imports System.Security.Cryptography
Imports VGCheckSum
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.tool.xml
Imports PG
Imports System.Linq
Imports STD.BAL
Imports NimbusSms

#End Region
Partial Class FlightDom_BookingConfimation
    Inherits System.Web.UI.Page
#Region "Variable Declaration:"
    Private OBTrackId As String, IBTrackId As String, FT As String
    Dim objTktCopy As New clsTicketCopy
    Public BackClor As String
    Dim objDA As New SqlTransaction
    Dim objOnlineTkt As New clsOnlineTicketing
    Dim objSql As New SqlTransactionNew
    Dim objSqlDom As New SqlTransactionDom
    Dim STYTR As New SqlTransactionYatra
    Dim objSMSAPI As New SMSAPI.SMS
    Dim objLccCpn As New LccCouponResult.CouponFare
    Dim objTktCopyMail As New IntlDetails()

    Dim SeatListO As List(Of STD.Shared.Seat)
    Dim SeatListR As List(Of STD.Shared.Seat)
#Region "Pawan Kumar"
    Dim objItzBal As New ITZGetbalance
    Dim objItzTrans As New ITZcrdb
    Dim objParamBal As New _GetBalance
    Dim objDebResp As New DebitResponse
    Dim objBalResp As New GetBalanceResponse
    Dim objParamDeb As New _CrOrDb
    Dim objUMSvc As New FltSearch1()
#End Region
#End Region
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language='javascript'>" & vbLf
        strDisAbleBackButton += "window.history.forward(1);" & vbLf
        strDisAbleBackButton += vbLf & "</script>"
        ClientScript.RegisterClientScriptBlock(Me.Page.[GetType](), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
    End Sub
    Public Function ToXML(objDeb As DebitResponse) As String
        Dim stringwriter As New System.IO.StringWriter()
        Dim serializer As New System.Xml.Serialization.XmlSerializer(objDeb.GetType())
        serializer.Serialize(stringwriter, objDeb)
        Return stringwriter.ToString()
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim amtbeforded As String = ""
        Dim objItzT As New Itz_Trans_Dal
        Dim inst As Boolean = False
        Dim objIzT As New ITZ_Trans
        Session("DomStrTktCopy") = ""
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        Dim strTktCopy As String = ""
        Try
            FT = Request.QueryString("FT")
            OBTrackId = Request.QueryString("OBTID")
            IBTrackId = Request.QueryString("IBTID")
            Dim FltDs As DataSet
            Dim PaxDs As DataSet
            Dim FltHdrDs As DataSet
            Dim FltFareDs As DataSet
            Dim AgencyDs As DataSet

            Dim BkgStatus As String = ""
            Dim vc As String = ""
            Dim GdsPnr As String = ""
            Dim AirlinePnr As String = ""
            Dim smsStatus As String = ""
            Dim smsStatustwo As String = ""
            Dim smsMsg As String = ""
            Dim TktNoArray As New ArrayList
            Dim AvlBal As Double = 0

            If Session("BookIng") = "TRUE" Then
                'strTktCopy = "Please make new search for another booking."
                Response.Redirect("../Login.aspx")
            Else
                Session("BookIng") = "TRUE"
                AgencyDs = objDA.GetAgencyDetails(Session("UID"))
                Try
                    FltDs = objDA.GetFltDtls(OBTrackId, Session("UID"))
                    PaxDs = objDA.GetPaxDetails(OBTrackId)
                    FltHdrDs = objDA.GetHdrDetails(OBTrackId)
                    FltFareDs = objDA.GetFltFareDtl(OBTrackId)
                    vc = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")

                    'If Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")) = "PG" Then
                    '    strTktCopy = FunBookedFlightByPaumentGateway(FltDs, PaxDs, FltHdrDs, FltFareDs, AgencyDs, vc, OBTrackId, IBTrackId)
                    'Else

                    ''to Get Mechant key
                    'Comment by Devesh
                    'GetMerchantKey(OBTrackId)

                    Dim ProjectId As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
                    Dim BookedBy As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDs.Tables(0).Rows(0)("BookedBy").ToString())
                    Dim BillNoCorp As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDs.Tables(0).Rows(0)("BillNoCorp").ToString())
                    'Comment by Devesh
                    'Try
                    '    objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                    '    objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    '    objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                    '    objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                    '    objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                    'Catch ex As Exception
                    'End Try


                    If FltDs.Tables(0).Rows.Count > 0 AndAlso PaxDs.Tables(0).Rows.Count > 0 AndAlso FltHdrDs.Tables(0).Rows.Count > 0 AndAlso FltFareDs.Tables(0).Rows.Count > 0 Then
                        ''''''''
                        If FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "TICKETED" And FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                            If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                                If FltHdrDs.Tables(0).Rows(0)("TotalAfterDis") <= Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) Then
                                    ''''
                                    ''If objBalResp.VAL_ACCOUNT_TYPE_DETAIL IsNot Nothing Then
                                    'If objBalResp.VAL_ACCOUNT_TYPE_DETAIL.Length > 0 Then
                                    'If Convert.ToDouble(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")) <= Convert.ToDouble(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE.Trim()) Then
                                    ' amtbeforded = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")

                                    'Try
                                    '    objParamDeb._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("MerchantKey") Is Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                    '    objParamDeb._PASSWORD = IIf(Not Session("_PASSWORD") Is Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                    '    objParamDeb._DECODE = IIf(Not Session("_DCODE") Is Nothing, Session("_DCODE").ToString().Trim(), " ")
                                    '    objParamDeb._AMOUNT = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                    '    objParamDeb._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                                    '    objParamDeb._ORDERID = IIf(OBTrackId <> Nothing AndAlso OBTrackId <> "", OBTrackId, " ")
                                    '    objParamDeb._DESCRIPTION = "Amount deducted against booking flight-" & FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString() & "-" & FltDs.Tables(0).Rows(0)("FlightIdentification").ToString()
                                    '    Dim stringtoenc As String = "MERCHANTKEY=" & objParamDeb._MERCHANT_KEY & "&ORDERID=" & objParamDeb._ORDERID & "&AMOUNT=" & objParamDeb._AMOUNT & "&MODE=" & objParamDeb._MODE
                                    '    objParamDeb._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                                    '    objParamDeb._SERVICE_TYPE = IIf(Session("_SvcTypeITZ") <> Nothing, Session("_SvcTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                                    '    objDebResp = objItzTrans.ITZDebit(objParamDeb)
                                    'Catch ex As Exception
                                    'End Try

                                    'Try
                                    '    objItzT = New Itz_Trans_Dal()
                                    '    objIzT.AMT_TO_DED = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                    '    objIzT.AMT_TO_CRE = "0"
                                    '    objIzT.B2C_MBLNO_ITZ = IIf(objDebResp.B2C_MOBILENO IsNot Nothing, objDebResp.B2C_MOBILENO, " ")
                                    '    objIzT.COMMI_ITZ = IIf(objDebResp.COMMISSION IsNot Nothing, objDebResp.COMMISSION, " ")
                                    '    objIzT.CONVFEE_ITZ = IIf(objDebResp.CONVENIENCEFEE IsNot Nothing, objDebResp.CONVENIENCEFEE, " ")
                                    '    objIzT.DECODE_ITZ = IIf(objDebResp.DECODE IsNot Nothing AndAlso objDebResp.DECODE <> "" AndAlso objDebResp.DECODE <> " ", objDebResp.DECODE, IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " "))
                                    '    objIzT.EASY_ORDID_ITZ = IIf(objDebResp.EASY_ORDER_ID IsNot Nothing, objDebResp.EASY_ORDER_ID, " ")
                                    '    objIzT.EASY_TRANCODE_ITZ = IIf(objDebResp.EASY_TRAN_CODE IsNot Nothing, objDebResp.EASY_TRAN_CODE, " ")
                                    '    objIzT.ERROR_CODE = IIf(objDebResp.ERROR_CODE IsNot Nothing, objDebResp.ERROR_CODE, " ")
                                    '    objIzT.MERCHANT_KEY_ITZ = IIf(objDebResp.MERCHANT_KEY IsNot Nothing AndAlso objDebResp.MERCHANT_KEY <> "" AndAlso objDebResp.MERCHANT_KEY <> " ", objDebResp.MERCHANT_KEY, IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " "))
                                    '    objIzT.MESSAGE_ITZ = IIf(objDebResp.MESSAGE IsNot Nothing, objDebResp.MESSAGE, " ")
                                    '    objIzT.ORDERID = IIf(OBTrackId <> Nothing AndAlso OBTrackId <> "", OBTrackId, " ")
                                    '    objIzT.RATE_GROUP_ITZ = IIf(objDebResp.RATEGROUP IsNot Nothing, objDebResp.RATEGROUP, " ")
                                    '    objIzT.REFUND_TYPE_ITZ = " "
                                    '    objIzT.SERIAL_NO_FROM = IIf(objDebResp.SERIALNO_FROM IsNot Nothing, objDebResp.SERIALNO_FROM, " ")
                                    '    objIzT.SERIAL_NO_TO = IIf(objDebResp.SERIALNO_TO IsNot Nothing, objDebResp.SERIALNO_TO, " ")
                                    '    objIzT.SVC_TAX_ITZ = IIf(objDebResp.SERVICETAX IsNot Nothing, objDebResp.SERVICETAX, " ")
                                    '    objIzT.TDS_ITZ = IIf(objDebResp.TDS IsNot Nothing, objDebResp.TDS, " ")
                                    '    objIzT.TOTAL_AMT_DED_ITZ = IIf(objDebResp.TOTALAMOUNT IsNot Nothing, objDebResp.TOTALAMOUNT, " ")
                                    '    objIzT.TRANS_TYPE = "AIR"
                                    '    objIzT.USER_NAME_ITZ = IIf(objDebResp.USERNAME IsNot Nothing AndAlso objDebResp.USERNAME <> "" AndAlso objDebResp.USERNAME <> " ", objDebResp.USERNAME, IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " "))
                                    '    Try
                                    '        objBalResp = New GetBalanceResponse()
                                    '        objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                    '        objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                    '        objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                    '        objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                    '        objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                    '        objIzT.ACCTYPE_NAME_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME, " ")
                                    '        objIzT.AVAIL_BAL_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                                    '    Catch ex As Exception
                                    '    End Try
                                    '    inst = objItzT.InsertItzTrans(objIzT)
                                    'Catch ex As Exception
                                    'End Try
                                    'Dim abc = ToXML(objDebResp)
                                    'AvlBal = FuncAmountDeduction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))
                                    ' If objDebResp IsNot Nothing Then
                                    'If objDebResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then

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
                                                Dim sOrderId As String = OBTrackId
                                                Dim sTransAmount As String = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")
                                                Dim sStaffUserId As String = Session("StaffUserId")
                                                Dim sOwnerId As String = Session("UID")
                                                Dim sIPAddress As String = Request.UserHostAddress
                                                Dim sRemark As String = Session("LoginType") + "_" + OBTrackId + "_Flight_" + vc + "_" + FltHdrDs.Tables(0).Rows(0)("Sector") + "_" + Convert.ToString(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))
                                                Dim sCreatedBy As String = Session("StaffUserId")
                                                Dim ModuleType As String = "FLIGHT BOOKING-DOM"
                                                Dim sServiceType As String = "FLIGHT"
                                                Dim DebitCredit As String = "DEBIT"
                                                Dim ActionType As String = "CHECKBAL-DEDUCT"
                                                Dim StaffDs As DataSet
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
                                                    If (Convert.ToString(StaffDs.Tables(0).Rows(0)("LoginStatus")) = "True" AndAlso Convert.ToString(StaffDs.Tables(0).Rows(0)("Flight")) = "True" AndAlso (DebitSataus.ToLower() = "true" OrElse StaffBalCheckStatus.ToLower() = "false")) Then
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


                                    Dim Result As Integer = 0
                                    Result = objSqlDom.Ledgerandcreditlimit_Transaction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), OBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Request.UserHostAddress.ToString(), ProjectId, BookedBy, BillNoCorp, Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim()), objIzT.EASY_ORDID_ITZ)
                                    If Result = 1 AndAlso BookTicket = "true" AndAlso objDebResp IsNot Nothing Then 'Result = 1
                                        BkgStatus = PnrCreation(vc, PaxDs, FltHdrDs, FltDs, TktNoArray, GdsPnr, AirlinePnr, AgencyDs, Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")))

                                        If String.IsNullOrEmpty(GdsPnr) Then GdsPnr = objSql.GetRndm() & "-FQ"

                                        If GdsPnr <> "" Then 'And InStr(GdsPnr, "-FQ") <= 0
                                            FuncDBUpdation(OBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                            'PaxAndLedgerDbUpdation(OBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDs, FltFareDs, ProjectId, BookedBy, BillNoCorp)
                                            PaxAndLedgerDbUpdation(OBTrackId, vc, GdsPnr, TktNoArray, PaxDs)
                                            MessageForInfantPax(GdsPnr, vc, PaxDs)
                                            strTktCopy = mailTktCopy(vc, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltHdrDs.Tables(0).Rows(0)("Sector"), FltDs.Tables(0).Rows(0)("Departure_Date"), "OutBound", AirlinePnr, GdsPnr, BkgStatus, OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgEmail"))

                                            If (BkgStatus = "Ticketed") Then
                                                'YTR Integration
                                                'Online Billing
                                                Try
                                                    'Dim AirObj As New AIR_YATRA
                                                    'AirObj.ProcessYatra_Air(OBTrackId, GdsPnr, "B")
                                                Catch ex As Exception

                                                End Try
                                                'NAV METHOD CALL START
                                                Try

                                                    'Dim objNav As New AirService.clsConnection(OBTrackId, "0", "0")
                                                    'objNav.airBookingNav(OBTrackId, "", 0)

                                                Catch ex As Exception

                                                End Try
                                                'Nav METHOD END'
                                                'Online Billing end

                                                Try
                                                    ' Dim SmsCrd As DataTable
                                                    ' SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)

                                                    ' If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                                        ' smsStatus = objSMSAPI.sendSms(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                                        ' objSql.SmsLogDetails(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                                    ' End If


														Dim smsStatus_New As Boolean = nimbus.ConfirmBooking(FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("PgLName").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("AirlinePnr").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("DepCityOrAirportCode").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("ArrCityOrAirportCode").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("DepTime").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("DepDate").ToString.Trim)

                                                        smsStatus = "Failed"
                                                        If smsStatus_New = True Then
                                                            smsStatus = "Success"
                                                        End If

                                                        objSql.SmsLogDetails(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)

                                                Catch ex As Exception
                                                End Try
                                            End If

                                            If Not (BkgStatus.Trim().ToLower().Contains("ticketed")) Or GdsPnr.Trim().Contains("-FQ") Then
                                                Try
                                                    Dim objDS As New Distributor()
                                                    Dim dsConfigMail As New DataSet()
                                                    Dim MailDt As New DataTable
                                                    Dim subHdr As String = ""
                                                    Dim bodyHdr As String = ""
                                                    Dim tomail As String = ""
                                                    Dim isAct As String = ""
                                                    Try
                                                        dsConfigMail = objDS.GetConfigureMails()
                                                    Catch ex As Exception

                                                    End Try

                                                    If GdsPnr.Trim().Contains("-FQ") Then
                                                        subHdr = "Domestic Air Booking Failed"
                                                        bodyHdr = "Domestic Failed Air Booking Details"
                                                        ''Try
                                                        ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("ToEmail").ToString()
                                                        ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("IsActive").ToString()
                                                        ''Catch ex As Exception

                                                        ''End Try
                                                    Else
                                                        subHdr = "Domestic Air Booking On Hold"
                                                        bodyHdr = "Domestic Hold Air Booking Details"
                                                        ''Try
                                                        ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("ToEmail").ToString()
                                                        ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("IsActive").ToString()
                                                        ''Catch ex As Exception

                                                        ''End Try
                                                    End If

                                                    Try
                                                        MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                                                    Catch ex As Exception

                                                    End Try
                                                    ''If Convert.ToBoolean(isAct) = True AndAlso MailDt IsNot Nothing Then
                                                    If MailDt IsNot Nothing Then
                                                        If MailDt.Rows.Count > 0 Then
                                                            Try
                                                                Dim strMailMsgHold As String
                                                                Dim newDepDate As String = ""
                                                                newDepDate = FltDs.Tables(0).Rows(0)("DepartureDate").ToString()
                                                                newDepDate = newDepDate.Insert(4, "/")
                                                                newDepDate = newDepDate.Insert(7, "/")
                                                                strMailMsgHold = "<table>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><h2>" & bodyHdr & "</h2>"
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><b>Customer ID: </b>" + Session("UID").ToString
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><b>Departure Date: </b>" + Convert.ToDateTime(newDepDate).ToString("dd/MM/yyyy")
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><b>Pnr No: </b>" + GdsPnr
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OBTrackId
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "<tr>"
                                                                strMailMsgHold = strMailMsgHold & "<td><b>Fare: </b>" + FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                                strMailMsgHold = strMailMsgHold & "</td>"
                                                                strMailMsgHold = strMailMsgHold & "</tr>"
                                                                strMailMsgHold = strMailMsgHold & "</table>"
                                                                Try
                                                                    Dim mailRow = If(GdsPnr.Trim().Contains("-FQ"), dsConfigMail.Tables(0).Select("ModuleType='Failed'"), dsConfigMail.Tables(0).Select("ModuleType='Hold'"))
                                                                    If mailRow.Length > 0 Then
                                                                        For ml As Integer = 0 To mailRow.Length - 1
                                                                            If Convert.ToBoolean(mailRow(ml)("IsActive").ToString()) Then
                                                                                objSqlDom.SendMail(mailRow(ml)("ToEmail").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Catch ex As Exception

                                                                End Try
                                                                ' ''If Convert.ToBoolean(isAct) Then
                                                                ' ''    objSqlDom.SendMail(tomail, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                ' ''End If

                                                            Catch ex As Exception

                                                            End Try

                                                        End If

                                                    End If

                                                Catch ex As Exception

                                                End Try
                                            End If


                                            If FT.ToUpper.Trim = "INBOUND" And InStr(GdsPnr, "-FQ") <= 0 Then
                                                objDebResp = Nothing
                                                FltDs.Clear()
                                                PaxDs.Clear()
                                                FltHdrDs.Clear()
                                                FltFareDs.Clear()
                                                GdsPnr = ""
                                                AirlinePnr = ""
                                                smsStatus = ""
                                                smsMsg = ""
                                                TktNoArray.Clear()
                                                vc = ""
                                                AvlBal = 0
                                                FltDs = objDA.GetFltDtls(IBTrackId, Session("UID"))
                                                PaxDs = objDA.GetPaxDetails(IBTrackId)
                                                FltHdrDs = objDA.GetHdrDetails(IBTrackId)
                                                FltFareDs = objDA.GetFltFareDtl(IBTrackId)
                                                vc = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")

                                                '' to get merchant key
                                                'Comment by Devesh
                                                ''GetMerchantKey(IBTrackId)

                                                Dim ProjectIdIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
                                                Dim BookedByIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDs.Tables(0).Rows(0)("BookedBy").ToString())
                                                Dim BillNoCorpIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDs.Tables(0).Rows(0)("BillNoCorp").ToString())

                                                'Try
                                                '    objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                                '    objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                                '    objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                                '    objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                                '    objBalResp = Nothing
                                                '    objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                                'Catch ex As Exception
                                                'End Try

                                                If FltDs.Tables(0).Rows.Count > 0 AndAlso PaxDs.Tables(0).Rows.Count > 0 AndAlso FltHdrDs.Tables(0).Rows.Count > 0 AndAlso FltFareDs.Tables(0).Rows.Count > 0 Then
                                                    ''''''
                                                    If FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "TICKETED" And FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                                                        If FltHdrDs.Tables(0).Rows(0)("TotalAfterDis") <= Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) Then
                                                            '''''
                                                            '' If Convert.ToDouble(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")) <= Convert.ToDouble(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE.Trim()) Then
                                                            'AvlBal = FuncAmountDeduction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))

                                                            'amtbeforded = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")

                                                            'Try
                                                            '    objParamDeb._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("MerchantKey") Is Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                                            '    objParamDeb._PASSWORD = IIf(Not Session("_PASSWORD") Is Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                                            '    objParamDeb._DECODE = IIf(Not Session("_DCODE") Is Nothing, Session("_DCODE").ToString().Trim(), " ")
                                                            '    objParamDeb._AMOUNT = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                            '    objParamDeb._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                                                            '    objParamDeb._ORDERID = IIf(IBTrackId <> Nothing AndAlso IBTrackId <> "", IBTrackId, " ")
                                                            '    objParamDeb._DESCRIPTION = " Amount deducted against booking flight-" & FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString() & "-" & FltDs.Tables(0).Rows(0)("FlightIdentification").ToString()
                                                            '    ''objParamDeb._CHECKSUM = " "
                                                            '    Dim stringtoenc As String = "MERCHANTKEY=" & objParamDeb._MERCHANT_KEY & "&ORDERID=" & objParamDeb._ORDERID & "&AMOUNT=" & objParamDeb._AMOUNT & "&MODE=" & objParamDeb._MODE
                                                            '    objParamDeb._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                                                            '    objParamDeb._SERVICE_TYPE = IIf(Session("_SvcTypeITZ") <> Nothing, Session("_SvcTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                                                            '    objDebResp = Nothing
                                                            '    objDebResp = objItzTrans.ITZDebit(objParamDeb)
                                                            'Catch ex As Exception
                                                            'End Try

                                                            'Try
                                                            '    objBalResp = New GetBalanceResponse()
                                                            '    objIzT = New ITZ_Trans()
                                                            '    objIzT.AMT_TO_DED = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                            '    objIzT.AMT_TO_CRE = "0"
                                                            '    objIzT.B2C_MBLNO_ITZ = IIf(objDebResp.B2C_MOBILENO IsNot Nothing, objDebResp.B2C_MOBILENO, " ")
                                                            '    objIzT.COMMI_ITZ = IIf(objDebResp.COMMISSION IsNot Nothing, objDebResp.COMMISSION, " ")
                                                            '    objIzT.CONVFEE_ITZ = IIf(objDebResp.CONVENIENCEFEE IsNot Nothing, objDebResp.CONVENIENCEFEE, " ")
                                                            '    objIzT.DECODE_ITZ = IIf(objDebResp.DECODE IsNot Nothing AndAlso objDebResp.DECODE <> "" AndAlso objDebResp.DECODE <> " ", objDebResp.DECODE, IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " "))
                                                            '    objIzT.EASY_ORDID_ITZ = IIf(objDebResp.EASY_ORDER_ID IsNot Nothing, objDebResp.EASY_ORDER_ID, " ")
                                                            '    objIzT.EASY_TRANCODE_ITZ = IIf(objDebResp.EASY_TRAN_CODE IsNot Nothing, objDebResp.EASY_TRAN_CODE, " ")
                                                            '    objIzT.ERROR_CODE = IIf(objDebResp.ERROR_CODE IsNot Nothing, objDebResp.ERROR_CODE, " ")
                                                            '    objIzT.MERCHANT_KEY_ITZ = IIf(objDebResp.MERCHANT_KEY IsNot Nothing AndAlso objDebResp.MERCHANT_KEY <> "" AndAlso objDebResp.MERCHANT_KEY <> " ", objDebResp.MERCHANT_KEY, IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " "))
                                                            '    objIzT.MESSAGE_ITZ = IIf(objDebResp.MESSAGE IsNot Nothing, objDebResp.MESSAGE, " ")
                                                            '    objIzT.ORDERID = IIf(IBTrackId <> Nothing AndAlso IBTrackId <> "", IBTrackId, " ")
                                                            '    objIzT.RATE_GROUP_ITZ = IIf(objDebResp.RATEGROUP IsNot Nothing, objDebResp.RATEGROUP, " ")
                                                            '    objIzT.REFUND_TYPE_ITZ = " "
                                                            '    objIzT.SERIAL_NO_FROM = IIf(objDebResp.SERIALNO_FROM IsNot Nothing, objDebResp.SERIALNO_FROM, " ")
                                                            '    objIzT.SERIAL_NO_TO = IIf(objDebResp.SERIALNO_TO IsNot Nothing, objDebResp.SERIALNO_TO, " ")
                                                            '    objIzT.SVC_TAX_ITZ = IIf(objDebResp.SERVICETAX IsNot Nothing, objDebResp.SERVICETAX, " ")
                                                            '    objIzT.TDS_ITZ = IIf(objDebResp.TDS IsNot Nothing, objDebResp.TDS, " ")
                                                            '    objIzT.TOTAL_AMT_DED_ITZ = IIf(objDebResp.TOTALAMOUNT IsNot Nothing, objDebResp.TOTALAMOUNT, " ")
                                                            '    objIzT.TRANS_TYPE = "AIR"
                                                            '    objIzT.USER_NAME_ITZ = IIf(objDebResp.USERNAME IsNot Nothing AndAlso objDebResp.USERNAME <> "" AndAlso objDebResp.USERNAME <> " ", objDebResp.USERNAME, IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " "))
                                                            '    Try
                                                            '        objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                                                            '        objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                                                            '        objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                                                            '        objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                                                            '        objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                                                            '        objIzT.ACCTYPE_NAME_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME, " ")
                                                            '        objIzT.AVAIL_BAL_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                                                            '    Catch ex As Exception
                                                            '    End Try
                                                            '    inst = objItzT.InsertItzTrans(objIzT)
                                                            'Catch ex As Exception
                                                            'End Try

                                                            'If objDebResp IsNot Nothing Then
                                                            ' If objDebResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then

                                                            'Balance Check and deduct and Transaction Log - Staff Login -Inbound
                                                            Dim DebitSatausR As String = ""
                                                            Dim CreditSatausR As String = ""
                                                            Dim CheckBalanceR As String = ""
                                                            Dim AgentStatusR As String = ""
                                                            Dim StaffBalCheckR As String = ""
                                                            Dim StaffBalCheckStatusR As String = ""
                                                            Dim CurrentTotAmtR As String = ""
                                                            Dim TransAmountR As String = ""
                                                            Dim BookTicketR As String = "true"
                                                            Try
                                                                If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")).ToUpper() = "TRUE" AndAlso Convert.ToString(Session("LoginType")).ToUpper() = "STAFF") Then
                                                                    BookTicketR = "false"
                                                                    If (Convert.ToString(Session("FlightActive")) = "True") Then
                                                                        'Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), IBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), 
                                                                        Dim BoookingByStaff As String = "True"
                                                                        Dim sOrderId As String = IBTrackId
                                                                        Dim sTransAmount As String = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")
                                                                        Dim sStaffUserId As String = Session("StaffUserId")
                                                                        Dim sOwnerId As String = Session("UID")
                                                                        Dim sIPAddress As String = Request.UserHostAddress.ToString()
                                                                        Dim sRemark As String = Session("LoginType") + "_" + IBTrackId + "_Flight_" + vc + "_" + FltHdrDs.Tables(0).Rows(0)("Sector") + "_" + Convert.ToString(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))
                                                                        Dim sCreatedBy As String = Session("StaffUserId")
                                                                        Dim ModuleType As String = "FLIGHT BOOKING-DOM"
                                                                        Dim sServiceType As String = "FLIGHT"
                                                                        Dim DebitCredit As String = "DEBIT"
                                                                        Dim ActionType As String = "CHECKBAL-DEDUCT"
                                                                        Dim StaffDs As DataSet
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
                                                                            BookTicketR = "false"
                                                                            If (Convert.ToString(StaffDs.Tables(0).Rows(0)("LoginStatus")) = "True" AndAlso Convert.ToString(StaffDs.Tables(0).Rows(0)("Flight")) = "True" AndAlso (DebitSataus.ToLower() = "true" OrElse StaffBalCheckStatus.ToLower() = "false")) Then
                                                                                BookTicketR = "true"
                                                                            End If
                                                                        Else
                                                                            BookTicketR = "false"
                                                                        End If
                                                                    Else
                                                                        BookTicketR = "false"
                                                                    End If

                                                                End If
                                                            Catch ex As Exception

                                                            End Try

                                                            'END: Balance Check and deduct and Transaction Log - Staff

                                                            Dim Result1 As Integer = 0
                                                            Result1 = objSqlDom.Ledgerandcreditlimit_Transaction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), IBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Request.UserHostAddress.ToString(), ProjectId, BookedBy, BillNoCorp, Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim), objIzT.EASY_ORDID_ITZ)
                                                            If Result1 = 1 AndAlso BookTicketR = "true" Then
                                                                BkgStatus = PnrCreation(vc, PaxDs, FltHdrDs, FltDs, TktNoArray, GdsPnr, AirlinePnr, AgencyDs, Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")))
                                                                If String.IsNullOrEmpty(GdsPnr) Then GdsPnr = objSql.GetRndm() & "-FQ"
                                                                If GdsPnr <> "" Then
                                                                    FuncDBUpdation(IBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                                                    'PaxAndLedgerDbUpdation(IBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDs, FltFareDs, ProjectIdIN, BookedByIN, BillNoCorpIN)
                                                                    PaxAndLedgerDbUpdation(IBTrackId, vc, GdsPnr, TktNoArray, PaxDs)
                                                                    MessageForInfantPax(GdsPnr, vc, PaxDs)
                                                                    strTktCopy = strTktCopy & "<br/>" & mailTktCopy(vc, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltHdrDs.Tables(0).Rows(0)("Sector"), FltDs.Tables(0).Rows(0)("Departure_Date"), "InBound", AirlinePnr, GdsPnr, BkgStatus, IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgEmail"))
                                                                    If (BkgStatus = "Ticketed") Then
                                                                        'YTR Integration
                                                                        'Online Billing
                                                                        Try
                                                                            'Dim AirObj As New AIR_YATRA
                                                                            'AirObj.ProcessYatra_Air(IBTrackId, GdsPnr, "B")
                                                                        Catch ex As Exception

                                                                        End Try
                                                                        'NAV METHOD  CALL START
                                                                        Try

                                                                            'Dim objNav As New AirService.clsConnection(IBTrackId, "0", "0")
                                                                            'objNav.airBookingNav(IBTrackId, "", 0)

                                                                        Catch ex As Exception

                                                                        End Try
                                                                        'Nav METHOD END'
                                                                        'Online Billing end

                                                                        Try
                                                                            Dim SmsCrd As DataTable
                                                                            SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)

                                                                            If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                                                                smsStatus = objSMSAPI.sendSms(IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                                                                objSql.SmsLogDetails(IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                                                            End If

                                                                        Catch ex As Exception
                                                                        End Try
                                                                    End If

                                                                    If Not (BkgStatus.Trim().ToLower().Contains("ticketed")) Or GdsPnr.Trim().Contains("-FQ") Then
                                                                        Try
                                                                            Dim objDS As New Distributor()
                                                                            Dim dsConfigMail As New DataSet()
                                                                            Dim MailDt As New DataTable
                                                                            Dim subHdr As String = ""
                                                                            Dim bodyHdr As String = ""
                                                                            Dim tomail As String = ""
                                                                            Dim isAct As String = ""
                                                                            Try
                                                                                dsConfigMail = objDS.GetConfigureMails()
                                                                            Catch ex As Exception

                                                                            End Try
                                                                            If GdsPnr.Trim().Contains("-FQ") Then
                                                                                subHdr = "Domestic Air Booking Failed"
                                                                                bodyHdr = "Domestic Failed Air Booking Details"
                                                                                ''Try
                                                                                ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("ToEmail").ToString()
                                                                                ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("IsActive").ToString()
                                                                                ''Catch ex As Exception

                                                                                ''End Try

                                                                            Else
                                                                                subHdr = "Domestic Air Booking On Hold"
                                                                                bodyHdr = "Domestic Hold Air Booking Details"
                                                                                ''Try
                                                                                ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("ToEmail").ToString()
                                                                                ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("IsActive").ToString()
                                                                                ''Catch ex As Exception

                                                                                ''End Try

                                                                            End If

                                                                            Try
                                                                                MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                                                                            Catch ex As Exception

                                                                            End Try
                                                                            ''If Convert.ToBoolean(isAct) = True AndAlso MailDt IsNot Nothing Then
                                                                            If MailDt IsNot Nothing Then
                                                                                If MailDt.Rows.Count > 0 Then
                                                                                    Try
                                                                                        Dim strMailMsgHold As String
                                                                                        Dim newDepDate As String = ""
                                                                                        newDepDate = FltDs.Tables(0).Rows(0)("DepartureDate").ToString()
                                                                                        newDepDate = newDepDate.Insert(4, "/")
                                                                                        newDepDate = newDepDate.Insert(7, "/")
                                                                                        strMailMsgHold = "<table>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><h2>" & bodyHdr & "</h2>"
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><b>Customer ID: </b>" + Session("UID").ToString
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><b>Departure Date: </b>" + Convert.ToDateTime(newDepDate).ToString("dd/MM/yyyy")
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><b>Pnr No: </b>" + GdsPnr
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OBTrackId
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "<td><b>Fare: </b>" + FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                                                        strMailMsgHold = strMailMsgHold & "</td>"
                                                                                        strMailMsgHold = strMailMsgHold & "</tr>"
                                                                                        strMailMsgHold = strMailMsgHold & "</table>"
                                                                                        Try
                                                                                            Dim mailRow = If(GdsPnr.Trim().Contains("-FQ"), dsConfigMail.Tables(0).Select("ModuleType='Failed'"), dsConfigMail.Tables(0).Select("ModuleType='Hold'"))
                                                                                            If mailRow.Length > 0 Then
                                                                                                For ml As Integer = 0 To mailRow.Length - 1
                                                                                                    If Convert.ToBoolean(mailRow(ml)("IsActive").ToString()) Then
                                                                                                        objSqlDom.SendMail(mailRow(ml)("ToEmail").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                                                    End If
                                                                                                Next
                                                                                            End If
                                                                                        Catch ex As Exception

                                                                                        End Try
                                                                                        ''If Convert.ToBoolean(isAct) Then
                                                                                        ''    objSqlDom.SendMail(tomail, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                                        ''End If
                                                                                    Catch ex As Exception

                                                                                    End Try

                                                                                End If

                                                                            End If

                                                                        Catch ex As Exception

                                                                        End Try
                                                                    End If


                                                                Else
                                                                    strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                                                End If
                                                            Else
                                                                strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                                            End If
                                                            'Else
                                                            '    strTktCopy = objDebResp.MESSAGE.Trim()
                                                            'End If
                                                            'Else
                                                            '    strTktCopy = "<strong style='font-size:14px'>Could not execute the transaction, please try after some time</strong>"
                                                            'End If
                                                        Else
                                                            strTktCopy = "<strong style='font-size:14px'>You cann't book ticket becaues of low balance</strong>"
                                                        End If
                                                        ''Else
                                                        ''    Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                                                        ''End If
                                                    Else
                                                        strTktCopy = "<strong style='font-size:14px'>You cann't book ticket using same booking reference number(" & IBTrackId & ")</strong>"
                                                    End If
                                                    ''''''
                                                Else
                                                    strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                                End If
                                            End If
                                        Else
                                            strTktCopy = "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                        End If
                                    Else
                                        'strTktCopy = "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                        strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                    End If
                                    'Else
                                    '    Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                                    'End If
                                    'Else
                                    '    'strTktCopy = "<strong style='font-size:14px'>Could not execute the transaction, please try after some time</strong>"
                                    '    strTktCopy = objDebResp.MESSAGE.Trim()
                                    'End If
                                    'Else
                                    '    strTktCopy = "<strong style='font-size:14px'>Could not execute the transaction, please try after some time</strong>"
                                    'End If
                                    'Else
                                    '    strTktCopy = "<strong style='font-size:14px'>You cann't book ticket becaues of low balance</strong>"
                                    'End If
                                    'Else
                                    '    strTktCopy = "<strong style='font-size:14px'>Could not get agent's balance details, please try after some time.</strong>"
                                    'End If
                                Else
                                    strTktCopy = "<strong style='font-size:14px'>Insufficient credit limit.</strong>"
                                End If
                            Else
                                'Dim um1 As String = ""
                                'um1 = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                                'Response.Redirect(um1 & "?msg=NA")
                                Response.Redirect("../International/BookingMsg.aspx?msg=NA")
                            End If
                        Else
                            strTktCopy = "<strong style='font-size:14px'>You cann't book ticket using same booking reference number(" & OBTrackId & ")</strong>"
                        End If
                        ''''''''                           
                    Else
                        strTktCopy = "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                    End If
                    'End If    
                    'Payment Gateway Endif

                Catch ex As Exception

                    strTktCopy = "Something went wrong. Please Contact our Customer care to know the actual status of the booking." ''ex.Message
                End Try
            End If
        Catch ex As Exception

            strTktCopy = "Something went wrong. Please Contact our Customer care to know the actual status of the booking." ''ex.Message
        End Try
        'lblTkt.Text = strTktCopy
        Session("DomStrTktCopy") = Convert.ToString(Session("DomStrTktCopy")) + strTktCopy
        'Dim um As String = ""
        'um = objUMSvc.GetMUForPage("Domestic/BookConfirmation.aspx")
        'Response.Redirect(um, True)
        Response.Redirect("../Report/PnrSummaryIntl.aspx?OrderId=" & OBTrackId, True)
    End Sub
    Private Function PnrCreation(ByVal VC As String, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef TktNoArray As ArrayList, ByRef GdsPnr As String, ByRef AirlinePnr As String, ByVal AgencyDs As DataSet, ByVal PaymentMode As String) As String
        Dim TktAirlineCrdDS As DataSet
        Dim BkgStatus As String = ""
        Dim blockBkg As String = ""
        Dim TKTHT As New Hashtable
        'blockBkg = objSql.BlockBookingAirlineWise(FltDs.Tables(0).Rows(0)("OrgDestFrom").ToString.Trim.ToUpper, FltDs.Tables(0).Rows(0)("OrgDestTo").ToString.Trim.ToUpper, FltDs.Tables(0).Rows(0)("FlightIdentification").ToString.Trim.ToUpper, VC.Trim.ToUpper, "D")



        '       SeatListO = IFlt.SeatDetails(OBTrackId)
        '#region Seat
        '               SeatList = objFleSBal.SeatDetails(orderId);
        '               #endregion

        Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        SeatListO = IFLT.SeatDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString.Trim.ToUpper)
        '' SeatListR = IFLT.SeatDetails(IBTrackId)

        blockBkg = objSql.BlockBookingAirlineWise(FltDs.Tables(0).Rows(0)("Track_id").ToString.Trim.ToUpper, FltDs.Tables(0).Rows(0)("OrgDestTo").ToString.Trim.ToUpper, FltDs.Tables(0).Rows(0)("FlightIdentification").ToString.Trim.ToUpper, VC.Trim.ToUpper, "D", FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Adult"), FltHdrDs.Tables(0).Rows(0)("Child"), FltHdrDs.Tables(0).Rows(0)("Infant"))
        If FltDs.Tables(0).Rows(0)("sno").ToString().ToUpper().Trim() = "OFFLINE" Then
            blockBkg = objSql.GetRndm() & "-BLOCK"
        End If
        Try
            If FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "SP1" Then

                Dim objSPBook As New STD.BAL.ScrapBookBAL()
                Dim crddatadset As New DataSet()
                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Dim ctx As HttpContext = HttpContext.Current
                GdsPnr = objSPBook.ScrapFightBook(FltDs.Tables(0), PaxDs, VC, crddatadset, FltHdrDs, TktNoArray, constr, ctx)

                If GdsPnr.Length > 6 Then
                    GdsPnr = Utility.GetRndm() + "-FQ"
                Else
                    AirlinePnr = GdsPnr
                End If

                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 And GdsPnr.Length <= 6 Then

                    BkgStatus = "Ticketed"

                Else

                    BkgStatus = "Confirm"
                    'If GdsPnr.Contains("-FRM") And PaymentMode <> "PG" Then
                    '    ''"Confirm"
                    '    Dim rfndstatus As Boolean = AutoRefund(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "Rejected")

                    '    If rfndstatus = True Then
                    '        BkgStatus = "Rejected"

                    '    Else
                    '        BkgStatus = "Confirm"

                    '    End If

                    'Else
                    '    BkgStatus = "Confirm"
                    'End If



                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = VC & xxP & "-FQ"
                    AirlinePnr = VC & xxP & "-FQ"
                End If

            ElseIf (FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "FDD") Then

                Dim crddatadset As New DataSet()
                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Dim ctx As HttpContext = HttpContext.Current

                Try
                    Dim totpax As Integer = 0
                    totpax = (Convert.ToInt32(FltDs.Tables(0).Rows(0)("Adult").ToString()) + Convert.ToInt32(FltDs.Tables(0).Rows(0)("Child").ToString()))
                    ''GdsPnr = objSPBook.ScrapFightBook(FltDs.Tables(0), PaxDs, VC, crddatadset, FltHdrDs, TktNoArray, constr, ctx)
                    Dim Seatav As New DataSet()
                    Seatav = objSql.checkseatFDD(FltDs.Tables(0).Rows(0)("sno").ToString())
                    If (Convert.ToInt32(Seatav.Tables(0).Rows(0)("SeatRn").ToString()) >= totpax And Convert.ToInt32(Seatav.Tables(0).Rows(0)("SeatRn").ToString()) > 0) Then
                        GdsPnr = FltDs.Tables(0).Rows(0)("FareBasis").ToString().Trim()

                        If (Not String.IsNullOrEmpty(GdsPnr) And GdsPnr.Length <= 6) Then
                            Dim ST As New SqlTransaction()
                            Dim STDom As New SqlTransactionDom()
                            Dim Aval_Bal As Double = ST.AddCrdLimit(FltDs.Tables(0).Rows(0)("Searchvalue").ToString().Trim(), FltDs.Tables(0).Rows(0)("NetFare").ToString().Trim())
                            STDom.insertLedgerDetails(FltDs.Tables(0).Rows(0)("Searchvalue").ToString().Trim(), "", FltDs.Tables(0).Rows(0)("Track_id").ToString.Trim.ToUpper(), GdsPnr, "", "", "", "", Session("UID").ToString(), "1111", 0, FltDs.Tables(0).Rows(0)("NetFare").ToString(), Aval_Bal, "Amount Credit to Your Account from fixed departure", "Successfully added Refno=" & FltDs.Tables(0).Rows(0)("sno").ToString(), 0)
                            objSql.UPDATESEATFDD(Convert.ToString(totpax), FltDs.Tables(0).Rows(0)("sno").ToString())
                        Else
                            GdsPnr = "FD" & Utility.GetRndm() + "-FQ"
                        End If
                    Else
                        GdsPnr = "FD" & Utility.GetRndm() + "-FQ"
                    End If
                Catch ex As Exception
                End Try
                AirlinePnr = GdsPnr
                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                    BkgStatus = "Ticketed"
                Else
                    BkgStatus = "Confirm"
                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = "FD" & xxP & "-FQ"
                    AirlinePnr = "FD" & xxP & "-FQ"
                End If

            ElseIf (VC = "IX") And FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "LCC" Then
                ''  GdsPnr = FuncIXIssueLccPnr(vc, PaxDs, FltHdrDs, FltDs, AirlinePnr)
                GdsPnr = FuncIssueIXPnr(VC, PaxDs, FltHdrDs, FltDs, AirlinePnr)
                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                    BkgStatus = "Ticketed"
                Else
                    BkgStatus = "Confirm"
                End If
            ElseIf FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "YA" And FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper <> "TQ" Then

                Dim objBook As New STD.BAL.YAAirBook()
                Dim dsCrdVA As DataSet = objSql.GetCredentials("YA", "", "D")

                'Dim islcc As Boolean = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("sno").ToString().Split(":")(2))
                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString

                Dim MBTbl As New DataTable
                If MBDT.Tables.Count > 0 Then
                    MBTbl = MBDT.Tables(0)
                End If

                GdsPnr = objBook.YAFlightBook(FltDs.Tables(0), PaxDs, VC, dsCrdVA, FltHdrDs, MBTbl, TktNoArray, constr)
                AirlinePnr = GdsPnr
                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then

                    BkgStatus = "Ticketed"

                Else


                    If GdsPnr.Contains("-FRM") And PaymentMode <> "PG" And FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper <> "TQ" Then
                        ''"Confirm"
                        'Dim rfndstatus As Boolean = AutoRefund(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "Rejected")
                        Dim rfndstatus As Boolean = False
                        If rfndstatus = True Then
                            BkgStatus = "Rejected"

                        Else
                            BkgStatus = "Confirm"

                        End If

                    Else
                        BkgStatus = "Confirm"
                    End If



                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = VC & xxP & "-FQ"
                    AirlinePnr = VC & xxP & "-FQ"
                End If




            ElseIf FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "TB" Then

                Dim objBook As New STD.BAL.TBO.TBOBook()
                Dim dsCrdVA As DataSet = objSql.GetCredentials("TB", "", "D")

                Dim islcc As Boolean = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("sno").ToString().Split(":")(2))
                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString

                Dim MBTbl As New DataTable
                If MBDT.Tables.Count > 0 Then
                    MBTbl = MBDT.Tables(0)
                End If

                If islcc = True Then

                    If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType").ToString().Trim() = "R" Then

                        Dim FltO As DataRow()
                        Dim FltR As DataRow()
                        FltO = FltDs.Tables(0).Select("TripType='O'", "counter ASC")
                        FltR = FltDs.Tables(0).Select("TripType='R'", "counter ASC")

                        Dim MBDTO As DataRow()
                        Dim MBDTR As DataRow()
                        MBDTO = MBTbl.Select("TripType='O'", "MBID ASC")
                        MBDTR = MBTbl.Select("TripType='R'", "MBID ASC")

                        GdsPnr = objBook.TBOFightBookLCC(FltDs.Tables(0), PaxDs, VC, dsCrdVA, FltHdrDs, MBTbl, TktNoArray, constr)
                        'GdsPnr = objBook.TBOFightBookLCC(FltO.CopyToDataTable(), PaxDs, VC, dsCrdVA, FltHdrDs, MBDTO.CopyToDataTable(), TktNoArray, constr)
                        If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then

                            BkgStatus = "Ticketed"
                            'Dim GdsPnr1 As String = objBook.TBOFightBookLCC(FltR.CopyToDataTable(), PaxDs, VC, dsCrdVA, FltHdrDs, MBDTR.CopyToDataTable(), TktNoArray, constr)
                            'If GdsPnr1 <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr1, "-BLOCK") <= 0 And InStr(GdsPnr1, "-SPR") <= 0 And InStr(GdsPnr1, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                            '    BkgStatus = "Ticketed"
                            'Else
                            '    BkgStatus = "Confirm"
                            'End If
                        Else
                            If GdsPnr.Contains("-FRM") And PaymentMode <> "PG" Then
                                'Dim rfndstatus As Boolean = AutoRefund(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "Rejected")
                                Dim rfndstatus As Boolean = False
                                If rfndstatus = True Then
                                    BkgStatus = "Rejected"

                                Else
                                    BkgStatus = "Confirm"

                                End If

                            Else
                                BkgStatus = "Confirm"
                            End If
                            Dim xxP As String
                            xxP = objSql.GetRndm()
                            GdsPnr = VC & xxP & "-FQ"
                            AirlinePnr = VC & xxP & "-FQ"
                        End If



                    Else

                        GdsPnr = objBook.TBOFightBookLCC(FltDs.Tables(0), PaxDs, VC, dsCrdVA, FltHdrDs, MBTbl, TktNoArray, constr)
                        AirlinePnr = GdsPnr
                        If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                            BkgStatus = "Ticketed"
                        Else
                            If GdsPnr.Contains("-FRM") And PaymentMode <> "PG" Then
                                'Dim rfndstatus As Boolean = AutoRefund(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "Rejected")
                                Dim rfndstatus As Boolean = False
                                If rfndstatus = True Then
                                    BkgStatus = "Rejected"

                                Else
                                    BkgStatus = "Confirm"

                                End If

                            Else
                                BkgStatus = "Confirm"
                            End If
                            Dim xxP As String
                            xxP = objSql.GetRndm()
                            GdsPnr = VC & xxP & "-FQ"
                            AirlinePnr = VC & xxP & "-FQ"

                        End If


                    End If







                Else
                    Dim bookingId As String = ""
                    GdsPnr = objBook.TBOFightBook(FltDs.Tables(0), PaxDs, VC, dsCrdVA, FltHdrDs, TktNoArray, constr, bookingId)
                    AirlinePnr = GdsPnr

                    If InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 Then
                        ' TktNoArray = objOnlineTkt.OnLineTicketing(AirlinePnr, GdsPnr, TktAirlineCrdDS.Tables(0).Rows(0)("Corporate_ID"), TktAirlineCrdDS.Tables(0).Rows(0)("Office_ID"), TktAirlineCrdDS.Tables(0).Rows(0)("Password"), VC)
                        '*******1G Ticketing****

                        Dim sno1 As String() = Convert.ToString(FltDs.Tables(0).Rows(0)("sno")).Split(":")

                        objBook.NonLCCTIcket(dsCrdVA, "", sno1(1), GdsPnr, bookingId, TktNoArray, constr)


                        If InStr(TktNoArray(0).ToString.ToUpper, "AIRLINE") > 0 Or TktNoArray.Count = 0 Then
                            BkgStatus = "Confirm"
                        Else
                            BkgStatus = "Ticketed"
                        End If
                    Else
                        BkgStatus = "Confirm"
                        Dim xxP As String
                        xxP = objSql.GetRndm()
                        GdsPnr = VC & xxP & "-FQ"
                        AirlinePnr = VC & xxP & "-FQ"
                    End If



                End If





            ElseIf VC.ToUpper() = "VA" Then
                Dim dsCrdVA As DataSet = objSql.GetCredentials(VC, "", "D")
                Dim AV As New STD.BAL.Booking_AV
                Dim HSBooking As New Hashtable
                Try
                    HSBooking = AV.AirVentura_Booking(dsCrdVA.Tables(0).Rows(0)("UserID"), dsCrdVA.Tables(0).Rows(0)("Password"), dsCrdVA.Tables(0).Rows(0)("CorporateID"), FltDs, FltHdrDs, PaxDs)
                    GdsPnr = HSBooking("VAPNR").ToString().Trim()
                    AirlinePnr = HSBooking("AIRLINEPNR").ToString().Trim()
                    TktNoArray = HSBooking("TKTNOARRAY")
                Catch ex As Exception
                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = VC & xxP & "-FQ"
                    AirlinePnr = VC & xxP & "-FQ"
                    If TktNoArray.Count = 0 Then
                        TktNoArray.Add("Airline")
                    End If
                End Try

                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And AirlinePnr <> "" Then
                    BkgStatus = "Ticketed"
                Else
                    BkgStatus = "Confirm"
                    If TktNoArray.Count = 0 Then
                        TktNoArray.Add("Airline")
                    End If
                End If
            ElseIf VC.ToUpper() = "LB" Then
                Dim dsCrdVA As DataSet = objSql.GetCredentials(VC, "", "D")
                Dim AC As New STD.BAL.Booking_AC
                Dim HSBooking As New Hashtable
                Try
                    HSBooking = AC.AirCosta_Booking(dsCrdVA.Tables(0).Rows(0)("UserID"), dsCrdVA.Tables(0).Rows(0)("Password"), dsCrdVA.Tables(0).Rows(0)("CorporateID"), FltDs, FltHdrDs, PaxDs)
                    GdsPnr = HSBooking("VAPNR").ToString().Trim()
                    AirlinePnr = HSBooking("AIRLINEPNR").ToString().Trim()
                    TktNoArray = HSBooking("TKTNOARRAY")
                Catch ex As Exception
                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = VC & xxP & "-FQ"
                    AirlinePnr = VC & xxP & "-FQ"
                    If TktNoArray.Count = 0 Then
                        TktNoArray.Add("Airline")
                    End If
                End Try

                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And AirlinePnr <> "" Then
                    BkgStatus = "Ticketed"
                Else
                    BkgStatus = "Confirm"
                    If TktNoArray.Count = 0 Then
                        TktNoArray.Add("Airline")
                    End If
                End If
            ElseIf VC.ToUpper.Trim <> "G8" And VC.ToUpper.Trim <> "6E" And VC.ToUpper.Trim <> "SG" And VC.ToUpper.Trim <> "IX" And VC.ToUpper.Trim <> "VA" And VC.ToUpper.Trim <> "LB" And VC.ToUpper.Trim <> "AK" And FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper <> "TQ" Then
                Dim ServiceCode As String = ""
                Dim con As New SqlConnection
                Try
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                    con.Open()
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand("SP_SERVICEPROVIDER_ENABLE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Trip", "D")
                    cmd.Parameters.AddWithValue("@VC", VC)
                    ServiceCode = cmd.ExecuteScalar()
                    con.Close()
                Catch ex As Exception
                    ServiceCode = "1A"
                End Try

                '''''''''''''''''1G start'''''''''''''''''''''''''''''''''''''''''''''''''''
                If ServiceCode.Trim().ToUpper() = "1G" Then
                    If blockBkg = "FALSE" Then GdsPnr = FuncIssueGdsPnr_GAL(PaxDs, FltHdrDs, FltDs, AirlinePnr) Else GdsPnr = blockBkg
                    If GdsPnr <> "" Then 'And InStr(GdsPnr, "-FQ") <= 0 And AirlinePnr <> ""
                        TktAirlineCrdDS = objSql.GetTktCredentials_GAL(VC, FltHdrDs.Tables(0).Rows(0)("Trip").ToString().Trim(), FltDs.Tables(0).Rows(0)("RESULTTYPE"))
                        Dim tktthrough As String = TktAirlineCrdDS.Tables(0).Rows(0)("TicketThrough")
                        Dim forceToHold As Boolean = TktAirlineCrdDS.Tables(0).Rows(0)("ForceToHold")
                        If forceToHold = True Then
                            TktNoArray.Add("Airline")
                            BkgStatus = "Confirm"

                        Else
                            'FuncIssueGdsPnr_GAL(PaxDs , FltHdrDs, FltDs,AirLinePnr) As String
                            'objGALGWS.CreateGdsPnrGAL(FltDs, FltHdrDs, PaxDs, TktDs)
                            If TktAirlineCrdDS.Tables(0).Rows(0)("OnlineTkt") = True And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 Then
                                ' TktNoArray = objOnlineTkt.OnLineTicketing(AirlinePnr, GdsPnr, TktAirlineCrdDS.Tables(0).Rows(0)("Corporate_ID"), TktAirlineCrdDS.Tables(0).Rows(0)("Office_ID"), TktAirlineCrdDS.Tables(0).Rows(0)("Password"), VC)
                                '*******1G Ticketing****
                                Dim objTktGal As New STD.BAL.GALTransanctions()
                                TKTHT = objTktGal.GetTicketNumberUsingOLTKT(GdsPnr, TktAirlineCrdDS, FltHdrDs.Tables(0).Rows(0)("OrderId").ToString().Trim(), FltDs, FltHdrDs, PaxDs)
                                TktNoArray = TKTHT("TktNoArray")
                                Try
                                    If AirlinePnr = "" Then
                                        AirlinePnr = TKTHT("AirPnr")
                                    End If
                                Catch ex As Exception

                                End Try
                                objSql.InsertGdsTktLogs(FltHdrDs.Tables(0).Rows(0)("OrderId").ToString, TKTHT)
                                If InStr(TktNoArray(0).ToString.ToUpper, "AIRLINE") > 0 Then
                                    BkgStatus = "Confirm"
                                Else
                                    BkgStatus = "Ticketed"
                                End If
                            Else
                                TktNoArray.Add("Airline")
                                BkgStatus = "Confirm"
                            End If
                        End If
                    Else
                        TktNoArray.Add("Airline")
                        BkgStatus = "Confirm"
                    End If
                End If

                '''''''''''''''''1G end'''''''''''''''''''''''''''''''''''''''''''''''''''

                '''''''''''''''''1A start'''''''''''''''''''''''''''''''''''''''''''''''''''
                'If ServiceCode.Trim().ToUpper() = "1A" Then
                '    If blockBkg = "FALSE" Then
                '        Dim objPnrCreate_1A As New STD.BAL.PNRCreation()
                '        Dim HSPNR As New Hashtable
                '        Try
                '            HSPNR = objPnrCreate_1A.PNRCreate(FltDs, FltHdrDs, PaxDs)
                '            GdsPnr = HSPNR("GDSPNR").ToString()
                '            AirlinePnr = HSPNR("AirlinePNR").ToString()
                '        Catch ex As Exception
                '            If GdsPnr <> "" Then GdsPnr = ""
                '        End Try
                '    Else
                '        GdsPnr = blockBkg
                '    End If
                '    If GdsPnr <> "" Then
                '        TktAirlineCrdDS = objSql.GetTktCredentials(VC)
                '        If TktAirlineCrdDS.Tables(0).Rows(0)("OnlineTkt") = True And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 Then
                '            Try
                '                Dim objOnlineTkt As New STD.BAL.OnlineTicketing()
                '            ''  TktNoArray = objOnlineTkt.GDSOnlineTicketing(GdsPnr, TktAirlineCrdDS, FltHdrDs.Tables(0).Rows(0)("OrderId").ToString().Trim())
                '            Catch ex As Exception
                '                If TktNoArray.Count = 0 Then TktNoArray.Add("Airline")
                '            End Try
                '            If InStr(TktNoArray(0).ToString.ToUpper, "AIRLINE") > 0 Then
                '                BkgStatus = "Confirm"
                '            Else
                '                BkgStatus = "Ticketed"
                '            End If
                '        Else
                '            TktNoArray.Add("Airline")
                '            BkgStatus = "Confirm"
                '        End If
                '    Else
                '        TktNoArray.Add("Airline")
                '        BkgStatus = "Confirm"
                '    End If
                'End If

                '''''''''''''''''1A end'''''''''''''''''''''''''''''''''''''''''''''''''''

                '''''''''''''''''1B start'''''''''''''''''''''''''''''''''''''''''''''''''''



                'If ServiceCode.Trim().ToUpper() = "1B" Then
                '    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                '    If blockBkg = "FALSE" Then
                '        GetAbacusPNR(FltDs.Tables(0), PaxDs.Tables(0), VC, "1B", GdsPnr, AirlinePnr, FltHdrDs.Tables(0).Rows(0)("PgMobile"), FltHdrDs.Tables(0).Rows(0)("PgEmail"), ConnStr)
                '        If GdsPnr <> "" And AirlinePnr <> "" Then
                '            Dim xmlTicketing As New Dictionary(Of String, String)()
                '            Dim fltBkgDa As STD.BAL.Credentials1 = New STD.BAL.Credentials1(ConnStr)
                '            Dim dsCrd As DataSet = fltBkgDa.Get_Abacus_PNR_TKT_CRD(VC, "D", "TKT", "1B")


                '            Dim absTrasac As New STD.BAL.AbacusTransaction(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), "Default", dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(1))

                '            If dsCrd.Tables(0).Rows(0)("OnlineTkt") = True And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 Then
                '                '*******1B Ticketing****
                '                Try

                '                    TktNoArray = absTrasac.PnrRetrieve(GdsPnr, FltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), xmlTicketing, FltDs.Tables(0).Rows(0)("IATAComm").ToString())

                '                    Try
                '                        fltBkgDa.InsertAbacusTicketing_Log(xmlTicketing("SetChangeAAARQ"), xmlTicketing("SetChangeAAARS"), xmlTicketing("setDesignatePrinterRQ"), xmlTicketing("setDesignatePrinterRS"), xmlTicketing("DesignatePrinter_1_TicketingRQ"), xmlTicketing("DesignatePrinter_1_TicketingRS"), _
                '                        xmlTicketing("DesignatePrinter_2_TicketingRQ"), xmlTicketing("DesignatePrinter_2_TicketingRS"), xmlTicketing("TravelTicktingLLSTicketingRQ"), xmlTicketing("TravelTicktingLLSTicketingRS"), xmlTicketing("setAirTicketRQ"), xmlTicketing("setAirTicketRS"), _
                '                        xmlTicketing("TicketNumberRQ"), xmlTicketing("TicketNumberRS"), FltDs.Tables(0).Rows(0)("Track_id").ToString(), GdsPnr)
                '                    Catch ex As Exception

                '                    End Try
                '                Catch ex As Exception
                '                    If TktNoArray.Count = 0 Then TktNoArray.Add("Airline")
                '                End Try
                '                If TktNoArray.Count > 0 And InStr(TktNoArray(0).ToString.ToUpper, "AIRLINE") > 0 Then
                '                    BkgStatus = "Confirm"
                '                Else
                '                    BkgStatus = "Ticketed"
                '                End If
                '            Else
                '                BkgStatus = "Confirm"
                '            End If
                '        Else
                '            BkgStatus = "Confirm"
                '        End If


                '    Else : GdsPnr = blockBkg
                '    End If
                'End If

                '''''''''''''''''1B end'''''''''''''''''''''''''''''''''''''''''''''''''''
            ElseIf FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "TQ" Then

                Dim SnoSplit As String() = New String() {"Ref!"}
                Dim SnoSplitDetails As String() = FltDs.Tables(0).Rows(0)("sno").ToString().Split(SnoSplit, StringSplitOptions.RemoveEmptyEntries)

                Dim objItqApi As New GALWS.ITQAirApi.BookingAndAddPassanger()
                Dim dsCrd As DataSet = objSql.GetCredentials("TQ", "", "D")
                GdsPnr = objItqApi.ITQairApiBooking(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP").ToString().Replace("Pricing", "Booking"), FltDs.Tables(0).Rows(0)("Searchvalue").ToString(), FltDs.Tables(0).Rows(0)("Track_id").ToString(), Session("UID").ToString(), SnoSplitDetails, AirlinePnr)

                If (SnoSplitDetails(4).ToUpper() <> "True".ToUpper() And GdsPnr.Contains("-FQ") = False And GdsPnr.Contains("FQ-") = False) Then
                    TKTHT = objItqApi.ITQairApiTiketting(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP").ToString().Replace("Pricing", "Ticket"), GdsPnr, FltDs.Tables(0).Rows(0)("Searchvalue").ToString(), FltDs.Tables(0).Rows(0)("Track_id").ToString(), Session("UID").ToString(), SnoSplitDetails)
                    TktNoArray = TKTHT("TktNoArray")
                    'Try
                    '    If AirlinePnr = "" Then
                    '        AirlinePnr = TKTHT("AirPnr")
                    '    End If
                    'Catch ex As Exception

                    'End Try
                End If
                If GdsPnr <> "" And InStr(GdsPnr, "FQ-") <= 0 And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then

                    BkgStatus = "Ticketed"
                Else
                    If GdsPnr.Contains("-FRM") Then
                        'Dim rfndstatus As Boolean = AutoRefund(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "Rejected")
                        Dim rfndstatus As Boolean = False
                        If rfndstatus = True Then
                            BkgStatus = "Rejected"

                        Else
                            BkgStatus = "Confirm"

                        End If

                    Else
                        BkgStatus = "Confirm"
                    End If
                    Dim xxP As String
                    xxP = objSql.GetRndm()
                    GdsPnr = VC & xxP & "-FQ"
                    AirlinePnr = VC & xxP & "-FQ"
                End If
            ElseIf FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "AK" Then
                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString()
                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")

                Dim SSRPrice As Decimal = 0
                For i As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                    SSRPrice += Convert.ToDecimal(MBDT.Tables(0).Rows(i)("MealPrice"))
                    SSRPrice += Convert.ToDecimal(MBDT.Tables(0).Rows(i)("BaggagePrice"))
                Next

                Dim SNNO() As String = FltDs.Tables(0).Rows(0)("sno").ToString().Split(":")
                Dim Originalrate As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF"))
                If SNNO(7) <> "INR" Then
                    Originalrate = Math.Round(Originalrate / Convert.ToDecimal(SNNO(8)), 4)
                    SSRPrice = Math.Round((SSRPrice / Convert.ToDecimal(SNNO(8))), 4)
                End If

                Dim dsCrd As DataSet = objSql.GetCredentials("AK", "", "D")
                Dim objAirAsia As New GALWS.AirAsia.AirAsiaBookings()
                If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType").ToString().Trim() = "R" Then
                    'Dim FltO As DataRow()
                    'Dim FltR As DataRow()
                    'FltO = FltDs.Tables(0).Select("TripType='O'", "counter ASC")
                    'FltR = FltDs.Tables(0).Select("TripType='R'", "counter ASC")

                    GdsPnr = objAirAsia.AirAsiaBookingDetails(dsCrd, FltDs.Tables(0), PaxDs, FltHdrDs, TktNoArray, AgencyDs, constr, SSRPrice, SNNO(7), Originalrate)
                    AirlinePnr = GdsPnr

                    If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                        BkgStatus = "Ticketed"
                    Else
                        BkgStatus = "Confirm"
                        If GdsPnr.Contains("-FQ") = False Then
                            Dim xxP As String
                            xxP = objSql.GetRndm()
                            GdsPnr = VC & xxP & "-FQ"
                            AirlinePnr = VC & xxP & "-FQ"
                        End If
                    End If
                Else


                    GdsPnr = objAirAsia.AirAsiaBookingDetails(dsCrd, FltDs.Tables(0), PaxDs, FltHdrDs, TktNoArray, AgencyDs, constr, SSRPrice, SNNO(7), Originalrate)
                    AirlinePnr = GdsPnr
                    If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                        BkgStatus = "Ticketed"
                    Else
                        BkgStatus = "Confirm"
                        If GdsPnr.Contains("-FQ") = False Then
                            Dim xxP As String
                            xxP = objSql.GetRndm()
                            GdsPnr = VC & xxP & "-FQ"
                            AirlinePnr = VC & xxP & "-FQ"
                        End If
                    End If
                End If
            Else
                'Dim stsCpn As String = ""
                'stsCpn = FltDs.Tables(0).Rows(0)("sno")  'objLccCpn.CheckCouponFare(FltHdrDs.Tables(0).Rows(0)("OrderId"), VC)
                'If stsCpn.ToUpper.Trim <> "INDIGOSPECIAL" And stsCpn.ToUpper.Trim <> "SPICEJETSPECIAL" Then
                '    GdsPnr = FuncIssueLccPnr(VC, PaxDs, FltHdrDs, FltDs, AirlinePnr)
                'Else
                '    GdsPnr = VC & objSql.GetRndm() & "-SPL"
                'End If
                Dim stsCpn As String = ""
                Dim stCpn() As String, sessionid As String = ""

                stsCpn = "" ' FltDs.Tables(0).Rows(0)("sno")  'objLccCpn.CheckCouponFare(FltHdrDs.Tables(0).Rows(0)("OrderId"), VC)
                If InStr(FltDs.Tables(0).Rows(0)("sno"), "/") > 0 Then
                    stCpn = Split(FltDs.Tables(0).Rows(0)("sno"), "/")
                    stsCpn = stCpn(0).ToString.Trim()
                    sessionid = stCpn(1).ToString.Trim()
                ElseIf InStr(FltDs.Tables(0).Rows(0)("sno"), "(ROUNDTRIP)") > 0 Then
                    stsCpn = "GOAIRPROMO(ROUNDTRIP)"
                End If
                'If stsCpn.ToUpper.Trim = "SPICEJETAPI" Then
                '    GdsPnr = FuncIssueLccCpnBooking(sessionid, "SGAPI", PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                If stsCpn.ToUpper.Trim = "INDIGOCORP" Then
                    GdsPnr = FuncIssueLccCpnBooking(sessionid, "6ECORP", PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                ElseIf stsCpn.ToUpper.Trim = "INDIGOTBF" Then
                    GdsPnr = FuncIssueLccCpnBooking(sessionid, "6ETBF", PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                ElseIf stsCpn.ToUpper.Trim = "GOAIRCORP" Then
                    GdsPnr = FuncIssueLccCpnBooking(sessionid, "G8CORP", PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                ElseIf stsCpn.ToUpper.Trim = "GOAIRPROMO(ROUNDTRIP)" Then
                    GdsPnr = FuncIssueLccPnr("G8PROMO", PaxDs, FltHdrDs, FltDs, AirlinePnr)
                ElseIf stsCpn.ToUpper.Trim = "AIRASIA" Then
                    GdsPnr = FuncIssueLccCpnBooking(sessionid, "AKCPN", PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                ElseIf stsCpn.ToUpper.Trim <> "INDIGOSPECIAL" And stsCpn.ToUpper.Trim <> "SPICEJETSPECIAL" And stsCpn.ToUpper.Trim <> "GOAIRSPECIAL" And stsCpn.ToUpper.Trim <> "AIINDIAEXPRESS" Then
                    If blockBkg = "FALSE" Then
                        GdsPnr = FuncIssueLccPnr(VC, PaxDs, FltHdrDs, FltDs, AirlinePnr)
                    Else
                        GdsPnr = blockBkg
                    End If
                Else
                    GdsPnr = FuncIssueLccCpnBooking(sessionid, VC, PaxDs, AgencyDs, FltHdrDs, FltDs, AirlinePnr)
                End If

                If GdsPnr <> "" And InStr(GdsPnr, "-FQ") <= 0 And InStr(GdsPnr, "-BLOCK") <= 0 And InStr(GdsPnr, "-SPR") <= 0 And InStr(GdsPnr, "-SPL") <= 0 And InStr(GdsPnr, "-DOMSPR") <= 0 Then
                    BkgStatus = "Ticketed"
                Else
                    BkgStatus = "Confirm"
                End If
            End If

        Catch ex As Exception
            BkgStatus = "Confirm"
        End Try
        Return BkgStatus
    End Function
    Private Function AutoRefund(ByVal OrderID As String, ByVal Status As String) As Boolean

        Dim Rfndstatus As Boolean = False
        Dim objRefnResp As New RefundResponse
        Dim objParamCrd As New _CrOrDb
        Dim objCrd As New ITZcrdb
        Dim objItzBal As New ITZGetbalance
        Dim objParamBal As New _GetBalance
        Dim objBalResp As New GetBalanceResponse
        Dim ST As New SqlTransaction()
        Dim STDom As New SqlTransactionDom()
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            ''Dim OrderID As String = ""
            ''OrderID = ViewState("ID").ToString()

            Dim ds As New DataSet()
            ds = ST.GetFltHeaderDetail(OrderID)
            Dim dtID As New DataTable()
            dtID = ds.Tables(0)
            Dim ChecksSatus As String = Status

            Dim objItzT As New Itz_Trans_Dal
            Dim inst As Boolean = False
            Dim objIzT As New ITZ_Trans
            Dim ablBalance As Double = 0

            If (ChecksSatus = "Rejected") Then
                Dim Aval_Bal As Double = ST.AddCrdLimit(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("TotalAfterDis").ToString())

                'Ledger

                'Adding Refund Amount in Agent balance
                'objParamCrd._DECODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
                Dim rndnum As New RandomKeyGenerator()

                Dim numRand As String = rndnum.Generate()
                Try
                    objParamCrd._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    objParamCrd._AMOUNT = IIf(dtID.Rows(0)("TotalAfterDis").ToString() <> Nothing, dtID.Rows(0)("TotalAfterDis").ToString(), 0)
                    objParamCrd._ORDERID = IIf(numRand <> Nothing AndAlso numRand <> "", numRand.Trim(), " ")
                    objParamCrd._REFUNDORDERID = IIf(OrderID <> Nothing AndAlso OrderID <> "", OrderID.Trim(), " ")
                    objParamCrd._MODE = IIf(Session("ModeTypeITZ") <> Nothing, Session("ModeTypeITZ").ToString().Trim(), " ") ''IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                    objParamCrd._REFUNDTYPE = "F"
                    ''objParamCrd._CHECKSUM = " "
                    Dim stringtoenc As String = "MERCHANTKEY=" & objParamCrd._MERCHANT_KEY & "&ORDERID=" & objParamCrd._ORDERID & "&REFUNDTYPE=" & objParamCrd._REFUNDTYPE
                    objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc)
                    'objParamCrd._SERVICE_TYPE = IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                    objParamCrd._DESCRIPTION = "refund to agent -" & dtID.Rows(0)("AgentId").ToString() & " against orderId-" & OrderID.ToString()
                    objRefnResp = objCrd.ITZRefund(objParamCrd)

                    If objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute") Then
                        Rfndstatus = True
                        ST.RejectHoldPNRStatusIntl(OrderID, "Äuto", "Rejected", "Auto Rejected on failure.", dtID.Rows(0)("GdsPnr").ToString(), "Rejected", dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Dom. PNR Auto Rejected Against  OrderID=" & OrderID, dtID.Rows(0)("AgencyName").ToString(), dtID.Rows(0)("AgentId").ToString())
                        STDom.insertLedgerDetails(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("AgencyName").ToString(), OrderID, dtID.Rows(0)("GdsPnr").ToString(), "", "", IIf(objRefnResp.EASY_ORDER_ID IsNot Nothing, objRefnResp.EASY_ORDER_ID, " "), "", Session("UID").ToString(), Request.UserHostAddress, 0, dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Dom. Auto Rejection", "Dom. PNR Auto Refund Against  OrderID=" & OrderID, 0)
                    End If
                Catch ex As Exception
                End Try
                objItzT = New Itz_Trans_Dal()
                Try

                    objIzT.AMT_TO_DED = "0"
                    objIzT.AMT_TO_CRE = IIf(dtID.Rows(0)("TotalAfterDis") <> Nothing, dtID.Rows(0)("TotalAfterDis").ToString(), 0)
                    objIzT.B2C_MBLNO_ITZ = " "
                    objIzT.COMMI_ITZ = " "
                    objIzT.CONVFEE_ITZ = " "
                    objIzT.DECODE_ITZ = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
                    objIzT.EASY_ORDID_ITZ = IIf(objRefnResp.EASY_ORDER_ID IsNot Nothing, objRefnResp.EASY_ORDER_ID, " ")
                    objIzT.EASY_TRANCODE_ITZ = IIf(objRefnResp.EASY_TRAN_CODE IsNot Nothing, objRefnResp.EASY_TRAN_CODE, " ")
                    objIzT.ERROR_CODE = IIf(objRefnResp.ERROR_CODE IsNot Nothing, objRefnResp.ERROR_CODE, " ")
                    objIzT.MERCHANT_KEY_ITZ = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    objIzT.MESSAGE_ITZ = IIf(objRefnResp.MESSAGE IsNot Nothing, objRefnResp.MESSAGE, " ")
                    objIzT.ORDERID = IIf(OrderID <> Nothing AndAlso OrderID <> "", OrderID.Trim(), " ")
                    objIzT.RATE_GROUP_ITZ = " "
                    objIzT.REFUND_TYPE_ITZ = IIf(objRefnResp.REFUND_TYPE IsNot Nothing AndAlso objRefnResp.REFUND_TYPE <> "" AndAlso objRefnResp.REFUND_TYPE <> " ", objRefnResp.REFUND_TYPE, " ")
                    objIzT.SERIAL_NO_FROM = " "
                    objIzT.SERIAL_NO_TO = " "
                    objIzT.SVC_TAX_ITZ = " "
                    objIzT.TDS_ITZ = " "
                    objIzT.TOTAL_AMT_DED_ITZ = " "
                    objIzT.TRANS_TYPE = "REFUND"
                    objIzT.USER_NAME_ITZ = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
                    Try
                        objBalResp = New GetBalanceResponse()
                        objParamBal._DCODE = IIf(dtID.Rows(0)("AgentId").ToString() <> Nothing, dtID.Rows(0)("AgentId").ToString().Trim(), " ")
                        objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
                        objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
                        objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
                        objIzT.ACCTYPE_NAME_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_TYPE_NAME, " ")
                        objIzT.AVAIL_BAL_ITZ = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                        Session("CL") = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                        ablBalance = IIf(objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE IsNot Nothing, objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE, " ")
                    Catch ex As Exception
                    End Try
                    inst = objItzT.InsertItzTrans(objIzT)
                Catch ex As Exception
                End Try

                ''STDom.insertLedgerDetails(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("AgencyName").ToString(), OrderID, dtID.Rows(0)("GdsPnr").ToString(), "", "", objIzT.EASY_ORDID_ITZ, "", "Auto", Request.UserHostAddress, 0, dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Dom. Rejection", "Dom. PNR Auto Rejected Against  OrderID=" & OrderID, 0)
                '' Response.Write("<script>alert('PNR Rejected Sucessfully')</script>")

            Else
                ''Response.Write("<script>alert('PNR Already Rejected')</script>")

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
        Return Rfndstatus
    End Function

    Private Function FuncIssueIXPnr(ByVal VC As String, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String

        Dim custinfo As New Hashtable
        Dim pnrno As String = ""
        Dim objdict As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        Dim objdictPaxID As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
        Try
            Dim PaxArray As Array
            Dim cnt As Integer = 1
            PaxArray = PaxDs.Tables(0).Select("PaxType='ADT'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_ADT" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameADT" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameADT" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameADT" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("ADTAge" & i + 1, "30")
                If (PaxArray(i)("DOB").ToString().Trim() = "DOB") Then
                    custinfo.Add("BirthDate_ADT" & i + 1, "1975-01-01")
                Else
                    custinfo.Add("BirthDate_ADT" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                End If
                custinfo.Add("PaxTypeCode_ADT" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_ADT" & i + 1, PaxArray(i)("PaxId"))
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='CHD'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_CHD" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameCHD" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameCHD" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameCHD" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("CHDAge" & i + 1, "10")
                If (PaxArray(i)("DOB").ToString().Trim() = "DOB") Then
                    custinfo.Add("BirthDate_CHD" & i + 1, "2005-01-01")
                Else
                    custinfo.Add("BirthDate_CHD" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                End If
                custinfo.Add("PaxTypeCode_CHD" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_CHD" & i + 1, PaxArray(i)("PaxId"))
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='INF'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_INF" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameINF" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameINF" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameINF" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("INFAge" & i + 1, "2")
                If (PaxArray(i)("DOB").ToString().Trim() = "DOB") Then
                    custinfo.Add("BirthDate_INF" & i + 1, "2018-01-01")
                Else
                    custinfo.Add("BirthDate_INF" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                End If

                custinfo.Add("PaxTypeCode_INF" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_INF" & i + 1, PaxArray(i)("PaxId"))
            Next

            custinfo.Add("Ttl", FltHdrDs.Tables(0).Rows(0)("PgTitle"))
            custinfo.Add("FName", FltHdrDs.Tables(0).Rows(0)("PgFName"))
            custinfo.Add("LName", FltHdrDs.Tables(0).Rows(0)("PgLName"))
            'custinfo.Add("sAddName", ConfigurationManager.AppSettings.Keys("companyname"))
            'custinfo.Add("sCity", ConfigurationManager.AppSettings.Keys("companycity"))
            'custinfo.Add("sCountry", ConfigurationManager.AppSettings.Keys("companycountry"))
            'custinfo.Add("sLine1", ConfigurationManager.AppSettings.Keys("companyaddress1"))
            'custinfo.Add("sLine2", ConfigurationManager.AppSettings.Keys("companyaddress2"))
            'custinfo.Add("sState", ConfigurationManager.AppSettings.Keys("companystate"))
            'custinfo.Add("sZip", ConfigurationManager.AppSettings.Keys("companyzip"))
            'custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            'custinfo.Add("sEmailId", ConfigurationManager.AppSettings.Keys("companyemail"))
            'custinfo.Add("sAgencyPhn", ConfigurationManager.AppSettings.Keys("companyphone"))
            'custinfo.Add("sComments", "OnLine Booking(" + ConfigurationManager.AppSettings.Keys("companyname") + ")")
            custinfo.Add("sAddName", Resources.Address.CompanyName)
            custinfo.Add("sCity", Resources.Address.City)
            custinfo.Add("sCountry", Resources.Address.Country)
            custinfo.Add("sLine1", Resources.Address.AddLine1)
            custinfo.Add("sLine2", Resources.Address.AddLine2)
            custinfo.Add("sState", Resources.Address.State)
            custinfo.Add("sZip", Resources.Address.Zip)
            custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            custinfo.Add("sEmailId", Resources.Address.Email)
            custinfo.Add("sAgencyPhn", Resources.Address.PhoneNo)
            custinfo.Add("sComments", Resources.Address.Comments)
            custinfo.Add("Customeremail", FltHdrDs.Tables(0).Rows(0)("PgEmail"))
            custinfo.Add("pay_type", "CL")
            custinfo.Add("sFax", "1")
            custinfo.Add("sCurrency", "INR")
            custinfo.Add("sCountryAccCode", "91")
            custinfo.Add("sAreaCityCode", "11")
            custinfo.Add("sCountryCode", "91")
            custinfo.Add("Nationality", "IN")
            custinfo.Add("CountryNameCode", "IN")


            Dim sMobile As String = "123456789"
            Dim sContactType As String = "1"
            Dim sContactNum As String = "0"
            Dim PnrDt As DataTable

            If VC = "IX" Then
                Dim dsCrd As DataSet
                Dim strArray() As String
                strArray = Split(FltDs.Tables(0).Rows(0)("sno"), ":")
                dsCrd = objSql.GetCredentials(strArray(6), "", "I")
                Try

                    Dim MBArrO As Array
                    Dim MBArrR As Array

                    Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
                    Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")

                    If (MBDT.Tables(0).Rows.Count > 0) Then
                        For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1

                            Dim MealPrice As Decimal = 0
                            Dim BagPrice As Decimal = 0
                            Dim SeatPrice As Decimal = 0
                            If Convert.ToString(MBDT.Tables(0).Rows(jj)("MealCategory")) = "" Then
                            Else
                                MealPrice = Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice"))
                            End If

                            If Convert.ToString(MBDT.Tables(0).Rows(jj)("BaggageCategory")) = "" Then
                            Else
                                BagPrice = Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                            End If

                            If Convert.ToString(MBDT.Tables(0).Rows(jj)("SeatCategory")) = "" Then
                            Else
                                SeatPrice = Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("SeatPrice"))
                            End If
                            OriginalTF = OriginalTF + MealPrice + BagPrice + SeatPrice
                        Next

                    End If



                    Dim Adt_No As Integer = FltDs.Tables(0).Rows(0)("Adult")
                    Dim Chd_No As Integer = FltDs.Tables(0).Rows(0)("Child")
                    Dim Inf_No As Integer = FltDs.Tables(0).Rows(0)("Infant")
                    Dim bookFlightInput As STD.Shared.FZBookFlightRequest = New STD.Shared.FZBookFlightRequest()
                    Dim custlist As New List(Of STD.Shared.FZPerson)()
                    Dim paxid As Integer = 214

                    Dim iCtr As Integer = 1
                    If Adt_No > 0 Then
                        Do While iCtr <= Adt_No
                            Dim pasnger As New STD.Shared.FZPerson()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameADT" & iCtr)
                            pasnger.MiddleName = custinfo("MnameADT" & iCtr)
                            pasnger.LastName = custinfo("LnameADT" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_ADT" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 1
                            pasnger.Title = custinfo("Title_ADT" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Trim() = "MR" Or pasnger.Title.ToUpper().Contains("DR") Or pasnger.Title.ToUpper().Contains("PROF"), "Male", "Female") ' "Male"
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_ADT" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            iCtr = iCtr + 1
                            paxid = paxid + 1
                        Loop
                        iCtr = 1
                    End If
                    If Chd_No > 0 Then
                        Do While iCtr <= Chd_No
                            Dim pasnger As New STD.Shared.FZPerson()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameCHD" & iCtr)
                            pasnger.MiddleName = custinfo("MnameCHD" & iCtr)
                            pasnger.LastName = custinfo("LnameCHD" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_CHD" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 6
                            pasnger.Title = custinfo("Title_CHD" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Contains("MR"), "Male", "Female")
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_CHD" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            iCtr = iCtr + 1
                            paxid = paxid + 1
                        Loop
                        iCtr = 1
                    End If
                    If Inf_No > 0 Then
                        Do While iCtr <= Inf_No
                            Dim pasnger As New STD.Shared.FZPerson()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameINF" & iCtr)
                            pasnger.MiddleName = custinfo("MnameINF" & iCtr)
                            pasnger.LastName = custinfo("LnameINF" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_INF" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 5
                            pasnger.Title = custinfo("Title_INF" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Contains("MR"), "Male", "Female")
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_INF" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            iCtr = iCtr + 1
                            paxid = paxid + 1
                        Loop
                        iCtr = 1
                    End If
                    bookFlightInput.CustomerList = custlist


                    Dim segmentList As New List(Of STD.Shared.FZSegment)
                    Dim datarowOweWay As DataRow() = FltDs.Tables(0).Select("Flight=1")
                    Dim datarowR As DataRow() = FltDs.Tables(0).Select("Flight=2")
                    If datarowOweWay.Length > 0 Then
                        Dim seg As New STD.Shared.FZSegment()
                        seg.FareInformationID = Convert.ToInt16(Split(datarowOweWay(0)("sno"), ":")(0))
                        seg.MarketingCode = Nothing
                        Dim splSrvlistO As New List(Of STD.Shared.FZServiceQuoteResponse)()
                        MBArrO = MBDT.Tables(0).Select("TripType='O'", "MBID ASC")
                        For i As Integer = 0 To MBArrO.Length - 1

                            Dim splO As New STD.Shared.FZServiceQuoteResponse()

                            If Convert.ToString(MBArrO(i)("BaggageCategory")) = "" Then
                            Else
                                splO.CodeType = MBArrO(i)("BaggageCode")
                                splO.DepartureDate = Split(Split(datarowOweWay(0)("sno"), ":")(3), "T")(0)
                                splO.LogicalFlightID = Split(datarowOweWay(0)("Searchvalue"), ":")(0)
                                splO.Amount = MBArrO(i)("BaggagePriceWithNoTax")
                                splO.SSRCategory = Split(MBArrO(i)("BaggageCategory"), "_")(0)
                                splO.ServiceID = Split(MBArrO(i)("BaggageCategory"), "_")(1)
                                splO.PersonOrgID = objdictPaxID(MBArrO(i)("PaxID").ToString().Trim())
                                splSrvlistO.Add(splO)
                            End If

                            If Convert.ToString(MBArrO(i)("MealCategory")) = "" Then
                            Else
                                splO = New STD.Shared.FZServiceQuoteResponse()
                                splO.CodeType = MBArrO(i)("MealCode")
                                splO.DepartureDate = Split(Split(datarowOweWay(0)("sno"), ":")(3), "T")(0)
                                splO.LogicalFlightID = Split(datarowOweWay(0)("Searchvalue"), ":")(0)
                                splO.Amount = MBArrO(i)("MealPriceWithNoTax")
                                splO.SSRCategory = Split(MBArrO(i)("MealCategory"), "_")(0)
                                splO.ServiceID = Split(MBArrO(i)("MealCategory"), "_")(1)
                                splO.PersonOrgID = objdictPaxID(MBArrO(i)("PaxID").ToString().Trim())
                                splSrvlistO.Add(splO)
                            End If
                        Next
                        seg.SpecialServices = splSrvlistO
                        segmentList.Add(seg)
                    End If
                    If datarowR.Length > 0 Then
                        Dim seg1 As New STD.Shared.FZSegment()
                        seg1.FareInformationID = Convert.ToInt16(Split(datarowR(0)("sno"), ":")(0))
                        seg1.MarketingCode = Nothing

                        Dim splSrvlistR As New List(Of STD.Shared.FZServiceQuoteResponse)
                        MBArrR = MBDT.Tables(0).Select("TripType='R'", "MBID ASC")
                        For j As Integer = 0 To MBArrR.Length - 1
                            Dim splR As New STD.Shared.FZServiceQuoteResponse()
                            If Convert.ToString(MBArrR(j)("BaggageCategory")) = "" Then
                            Else

                                splR.CodeType = MBArrR(j)("BaggageCode")
                                splR.DepartureDate = Split(Split(datarowR(0)("sno"), ":")(3), "T")(0)
                                splR.LogicalFlightID = Split(datarowR(0)("Searchvalue"), ":")(0)
                                splR.Amount = MBArrR(j)("BaggagePriceWithNoTax")
                                splR.SSRCategory = Split(MBArrR(j)("BaggageCategory"), "_")(0)
                                splR.ServiceID = Split(MBArrR(j)("BaggageCategory"), "_")(1)
                                splR.PersonOrgID = objdictPaxID(MBArrR(j)("PaxID").ToString().Trim())
                                splSrvlistR.Add(splR)
                            End If

                            If Convert.ToString(MBArrR(j)("MealCategory")) = "" Then
                            Else
                                splR = New STD.Shared.FZServiceQuoteResponse()
                                splR.CodeType = MBArrR(j)("MealCode")
                                splR.DepartureDate = Split(Split(datarowR(0)("sno"), ":")(3), "T")(0)
                                splR.LogicalFlightID = Split(datarowR(0)("Searchvalue"), ":")(0)
                                splR.Amount = MBArrR(j)("MealPriceWithNoTax")
                                splR.SSRCategory = Split(MBArrR(j)("MealCategory"), "_")(0)
                                splR.ServiceID = Split(MBArrR(j)("MealCategory"), "_")(1)
                                splR.PersonOrgID = objdictPaxID(MBArrR(j)("PaxID").ToString().Trim())
                                splSrvlistR.Add(splR)
                            End If


                        Next
                        seg1.SpecialServices = splSrvlistR

                        segmentList.Add(seg1)
                    End If

                    bookFlightInput.Address = custinfo("sLine1")
                    bookFlightInput.Address2 = custinfo("sLine2")
                    bookFlightInput.CarrierCurrency = custinfo("sCurrency")
                    bookFlightInput.City = custinfo("sCity")
                    bookFlightInput.ContactValue = custinfo("sAgencyPhn")
                    bookFlightInput.Country = custinfo("sCountry")
                    bookFlightInput.CountryCode = custinfo("sCountryCode")
                    bookFlightInput.AreaCode = custinfo("sAreaCityCode")
                    bookFlightInput.DisplayCurrency = custinfo("sCurrency")
                    bookFlightInput.Email = custinfo("Customeremail")
                    bookFlightInput.Fax = custinfo("sFax")
                    bookFlightInput.IATANum = dsCrd.Tables(0).Rows(0)("CorporateID").ToString()
                    bookFlightInput.Mobile = custinfo("sHomePhn")
                    bookFlightInput.Postal = custinfo("sZip")
                    bookFlightInput.ProfileID = 0
                    bookFlightInput.PromoCode = Nothing
                    bookFlightInput.SecurityGUID = strArray(1)
                    bookFlightInput.State = custinfo("sState")
                    bookFlightInput.WebBookingID = strArray(1)
                    bookFlightInput.paymentDetails = New STD.Shared.FZPayment()
                    bookFlightInput.paymentDetails.CompanyName = custinfo("sAddName")
                    bookFlightInput.paymentDetails.ExchangeRate = 1
                    bookFlightInput.paymentDetails.ExchangeRateDate = DateTime.Now
                    bookFlightInput.paymentDetails.FirstName = custinfo("FName")
                    bookFlightInput.paymentDetails.ISOCurrency = 1
                    bookFlightInput.paymentDetails.LastName = custinfo("LName")
                    bookFlightInput.paymentDetails.OriginalAmount = Convert.ToDecimal(OriginalTF)
                    bookFlightInput.paymentDetails.OriginalCurrency = custinfo("sCurrency")
                    bookFlightInput.paymentDetails.PaymentAmount = Convert.ToDecimal(OriginalTF)
                    bookFlightInput.paymentDetails.PaymentComment = custinfo("sComments")
                    bookFlightInput.paymentDetails.PaymentCurrency = custinfo("sCurrency")
                    bookFlightInput.paymentDetails.PaymentDate = DateTime.Now
                    bookFlightInput.paymentDetails.PaymentNum = 1
                    bookFlightInput.paymentDetails.VoucherNum = Nothing
                    bookFlightInput.segment = segmentList

                    Dim bookFlight As New STD.BAL.IXBooking(Split(datarowOweWay(0)("sno"), ":")(1), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("ServerIP"))

                    objdict = bookFlight.BookFlight(bookFlightInput)

                    'PnrDt = bookFlight.BookReservation(bookFlightInput)
                    ' pnrno = PnrDt.Rows(0)("ConfirmationNo")
                    AirLinePnr = objdict("PNR")
                    pnrno = AirLinePnr

                    objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), If(objdict.ContainsKey("PNR"), objdict("PNR"), ""), If(objdict.ContainsKey("SummaryPnrReq"), objdict("SummaryPnrReq"), ""), _
                                          If(objdict.ContainsKey("SummaryPnrRes"), objdict("SummaryPnrRes"), ""), If(objdict.ContainsKey("ProcessPNRPaymentReq"), objdict("ProcessPNRPaymentReq"), ""), _
                                        If(objdict.ContainsKey("ProcessPNRPaymentRes"), objdict("ProcessPNRPaymentRes"), ""), If(objdict.ContainsKey("CreatePNRRes"), objdict("CreatePNRRes"), ""), _
                                      If(objdict.ContainsKey("CreatePNRReq"), objdict("CreatePNRReq"), ""), "", "", "", "", "", _
                                     If(objdict.ContainsKey("EXEP"), objdict("EXEP"), ""), "", "", "", "", "SeatReq", "SeatRes")
                Catch ex As Exception
                    objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), If(objdict.ContainsKey("PNR"), objdict("PNR"), ""), If(objdict.ContainsKey("SummaryPnrReq"), objdict("SummaryPnrReq"), ""), _
                                          If(objdict.ContainsKey("SummaryPnrRes"), objdict("SummaryPnrRes"), ""), If(objdict.ContainsKey("ProcessPNRPaymentReq"), objdict("ProcessPNRPaymentReq"), ""), _
                                        If(objdict.ContainsKey("ProcessPNRPaymentRes"), objdict("ProcessPNRPaymentRes"), ""), If(objdict.ContainsKey("CreatePNRRes"), objdict("CreatePNRRes"), ""), _
                                      If(objdict.ContainsKey("CreatePNRReq"), objdict("CreatePNRReq"), ""), "", "", "", "", "", _
                                     If(objdict.ContainsKey("EXEP"), objdict("EXEP"), ""), "", "", "", "", "SeatReq", "SeatRes")
                End Try
                Try
                    If pnrno <> "" And pnrno IsNot Nothing Then
                    Else
                        Dim xx As String
                        xx = objSql.GetRndm()
                        pnrno = VC & xx & "-FQ"
                        AirLinePnr = VC & xx & "-FQ"
                    End If
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception


        End Try
        Return pnrno
    End Function
    Private Function FuncIssueGdsPnr(ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String

        Dim AdultFirstName() As String, AdultLastName() As String, AdultTitle() As String, ChildFirstName() As String, ChildLastName() As String
        Dim ChildTitle() As String, InfantFirstName() As String, InfantLastName() As String, InfantTitle() As String, ChildDOB() As String, InfantDOB() As String
        Dim requiredTadult1 As String = "", requiredFadult1 As String = "", requiredLadtult1 As String = "", requiredTchild1 As String = "", requiredFchild1 As String = ""
        Dim requiredLchild1 As String = "", requiredTinfant1 As String = "", requiredFinfant1 As String = "", requiredLinfant1 As String = ""
        Dim dob_chd As String = ""
        Dim dob_inf As String = ""
        Dim ff_air As String = "", seat_ty_adt As String = "", meal_ty_adt As String = "", seat_ty_chd As String = "", meal_ty_chd As String = ""
        Dim strPnr As String = "", GdsPnr As String = ""
        Dim fltArray As Array
        Dim Mobile, Email, Trip, sector, vc As String
        Dim Tot_seat, Adult, Child, infant As Integer
        fltArray = FltDs.Tables(0).Select
        Adult = FltHdrDs.Tables(0).Rows(0)("Adult")
        Child = FltHdrDs.Tables(0).Rows(0)("Child")
        infant = FltHdrDs.Tables(0).Rows(0)("Infant")
        sector = FltHdrDs.Tables(0).Rows(0)("sector")
        Mobile = FltHdrDs.Tables(0).Rows(0)("PgMobile")
        Email = FltHdrDs.Tables(0).Rows(0)("PgEmail")
        vc = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")
        Trip = FltDs.Tables(0).Rows(0)("Trip")
        Tot_seat = Adult + Child


        For i As Integer = 0 To PaxDs.Tables(0).Rows.Count - 1
            If PaxDs.Tables(0).Rows(i)("PaxType") = "ADT" Then
                requiredTadult1 = requiredTadult1 & PaxDs.Tables(0).Rows(i)("Title") & " " & "<BR>"
                requiredFadult1 = requiredFadult1 & PaxDs.Tables(0).Rows(i)("FName") & " " & PaxDs.Tables(0).Rows(i)("MName") & " " & "<BR>"
                requiredLadtult1 = requiredLadtult1 & PaxDs.Tables(0).Rows(i)("LName") & " " & "<BR>"
                ff_air = ff_air & PaxDs.Tables(0).Rows(i)("FFAirline") & ":" & PaxDs.Tables(0).Rows(i)("FFNumber") & "<BR>"
                seat_ty_adt = seat_ty_adt & PaxDs.Tables(0).Rows(i)("SeatType") & "<BR>"
                meal_ty_adt = meal_ty_adt & PaxDs.Tables(0).Rows(i)("MealType") & "<BR>"
            ElseIf PaxDs.Tables(0).Rows(i)("PaxType") = "CHD" Then
                requiredTchild1 = requiredTchild1 & PaxDs.Tables(0).Rows(i)("Title") & " " & "<BR>"
                requiredFchild1 = requiredFchild1 & PaxDs.Tables(0).Rows(i)("FName") & " " & PaxDs.Tables(0).Rows(i)("MName") & " " & "<BR>"
                requiredLchild1 = requiredLchild1 & PaxDs.Tables(0).Rows(i)("LName") & " " & "<BR>"
                Dim yr1 = Right(PaxDs.Tables(0).Rows(i)("DOB"), 2)
                dob_chd = dob_chd & Left(PaxDs.Tables(0).Rows(i)("DOB"), 2) & datecon(Mid(PaxDs.Tables(0).Rows(i)("DOB"), 4, 2)) & yr1 & "<BR>"
                seat_ty_chd = seat_ty_chd & PaxDs.Tables(0).Rows(i)("SeatType") & "<BR>"
                meal_ty_chd = meal_ty_chd & PaxDs.Tables(0).Rows(i)("MealType") & "<BR>"
            ElseIf PaxDs.Tables(0).Rows(i)("PaxType") = "INF" Then
                requiredTinfant1 = requiredTinfant1 & PaxDs.Tables(0).Rows(i)("Title") & " " & "<BR>"
                requiredFinfant1 = requiredFinfant1 & PaxDs.Tables(0).Rows(i)("FName") & PaxDs.Tables(0).Rows(i)("MName") & " " & "<BR>"
                requiredLinfant1 = requiredLinfant1 & PaxDs.Tables(0).Rows(i)("LName") & " " & "<BR>"
                Dim yr1 = Right(PaxDs.Tables(0).Rows(i)("DOB"), 2)
                dob_inf = dob_inf & Left(PaxDs.Tables(0).Rows(i)("DOB"), 2) & datecon(Mid(PaxDs.Tables(0).Rows(i)("DOB"), 4, 2)) & yr1 & "<BR>"
            End If
        Next
        ChildDOB = Split(dob_chd, "<BR>")
        InfantDOB = Split(dob_inf, "<BR>")
        AdultTitle = Split(requiredTadult1, "<BR>")
        ChildTitle = Split(requiredTchild1, "<BR>")
        InfantTitle = Split(requiredTinfant1, "<BR>")
        AdultFirstName = Split(requiredFadult1, "<BR>")
        AdultLastName = Split(requiredLadtult1, "<BR>")
        ChildFirstName = Split(requiredFchild1, "<BR>")
        ChildLastName = Split(requiredLchild1, "<BR>")
        InfantFirstName = Split(requiredFinfant1, "<BR>")
        InfantLastName = Split(requiredLinfant1, "<BR>")
        Dim dsCrd As New DataSet
        Dim TktAirlineCrdDS As New DataSet
        TktAirlineCrdDS.Clear()
        Dim OT As Boolean = False, TKTOID As String = ""
        TktAirlineCrdDS = objSql.GetTktCredentials(vc)
        If TktAirlineCrdDS.Tables(0).Rows(0)("OnlineTkt") = True Then OT = True
        TKTOID = TktAirlineCrdDS.Tables(0).Rows(0)("Office_ID").ToString.Trim
        dsCrd.Clear()
        dsCrd = objSql.GetCredentials("1A", "", "D")
        Dim objAmadeusBook As New GDS_Amadeus(dsCrd.Tables(0).Rows(0)("ServerUrlOrIP"), dsCrd.Tables(0).Rows(0)("Port"), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"))
        Try
            strPnr = objAmadeusBook.Segnment_Sell(fltArray, Tot_seat, Adult, Child, infant, AdultTitle, AdultFirstName, AdultLastName, ChildTitle, ChildFirstName, ChildLastName, ChildDOB, InfantFirstName, InfantDOB, Mobile, Email, vc, Trip, sector, ff_air, seat_ty_adt, meal_ty_adt, seat_ty_chd, meal_ty_chd, "Dom", OT, TKTOID)
            Dim Ref_Str = Split(strPnr, "<AmadeusPnr>")
            Dim T_REF_S = Split(Ref_Str(1), "</AmadeusPnr>")
            GdsPnr = T_REF_S(0)
            Ref_Str = Split(strPnr, "<AirLinePnr>")
            T_REF_S = Split(Ref_Str(1), "</AirLinePnr>")
            AirLinePnr = T_REF_S(0)
        Catch ex As Exception
            Dim xx As String

            xx = objAmadeusBook.GetRndm()
            GdsPnr = xx & "-FQ"
            AirLinePnr = xx & "-FQ"
        End Try
        Return GdsPnr
    End Function
    Private Function FuncIssueGdsPnr_GAL(ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String
        Dim objGALGWS As New STD.BAL.GALTransanctions()
        objGALGWS.connectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Dim GdsPnr As String
        Dim PnrHT As Hashtable
        Dim TktDs As New DataSet
        TktDs = objSql.GetTktCredentials_GAL(FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim(), FltHdrDs.Tables(0).Rows(0)("Trip").ToString().Trim(), FltDs.Tables(0).Rows(0)("RESULTTYPE"))
        PnrHT = objGALGWS.CreateGdsPnrGAL(FltDs, FltHdrDs, PaxDs, TktDs)
        GdsPnr = PnrHT("ADTPNR")
        AirLinePnr = PnrHT("ADTAIRPNR") ' PnrHT("ADTPNR")
        objSql.InsertGdsBkgLogs(FltHdrDs.Tables(0).Rows(0)("OrderId").ToString, PnrHT)
        Return GdsPnr
    End Function
    Private Function FuncIssueLccPnr(ByVal VC As String, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String
        Dim FareTypeSettingsList As List(Of FareTypeSettings)
        Dim FT As String() = Nothing
        Dim PROMOCODE As String = ""
        Dim Bag As Boolean = False
        Dim SSRCode As String = ""
        Try
            If [String].IsNullOrEmpty(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString()) = False Then
                PROMOCODE = Split(FltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString(), "/")(0)
            End If
            Dim objfltBal As New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            FareTypeSettingsList = objfltBal.GetFareTypeSettings("", FltDs.Tables(0).Rows(0)("Trip").ToString(), "")
            FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = VC AndAlso x.Trip = FltDs.Tables(0).Rows(0)("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")).ToUpper().Trim()).ToList()
            FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
        Catch ex As Exception

        End Try

        Dim custinfo As New Hashtable
        Dim pnrno As String = ""
        Try
            Dim PaxArray As Array
            Dim cnt As Integer = 1
            PaxArray = PaxDs.Tables(0).Select("PaxType='ADT'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_ADT" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameADT" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameADT" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("ADTAge" & i + 1, "30")
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='CHD'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_CHD" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameCHD" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameCHD" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("CHDAge" & i + 1, "10")
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='INF'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_INF" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameINF" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameINF" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("INFAge" & i + 1, "2")
            Next
            '' Dim Resources.Address() As String = 


            custinfo.Add("Ttl", FltHdrDs.Tables(0).Rows(0)("PgTitle"))
            custinfo.Add("FName", FltHdrDs.Tables(0).Rows(0)("PgFName"))
            custinfo.Add("LName", FltHdrDs.Tables(0).Rows(0)("PgLName"))
            custinfo.Add("sAddName", Resources.Address.CompanyName)
            custinfo.Add("sCity", Resources.Address.City)
            custinfo.Add("sCountry", Resources.Address.Country)
            custinfo.Add("sLine1", Resources.Address.AddLine1)
            custinfo.Add("sLine2", Resources.Address.AddLine2)
            custinfo.Add("sState", Resources.Address.State)
            custinfo.Add("sZip", Resources.Address.Zip)
            custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            custinfo.Add("sEmailId", Resources.Address.Email)
            custinfo.Add("sAgencyPhn", Resources.Address.PhoneNo)
            custinfo.Add("sComments", Resources.Address.Comments)
            'custinfo.Add("sComments", "OnLine Booking(" + ConfigurationManager.AppSettings.Keys("companyname") + ")")
            'custinfo.Add("sAddName", ConfigurationManager.AppSettings.Keys("companyname"))
            'custinfo.Add("sCity", ConfigurationManager.AppSettings.Keys("companycity"))
            'custinfo.Add("sCountry", ConfigurationManager.AppSettings.Keys("companyindia"))
            'custinfo.Add("sLine1", ConfigurationManager.AppSettings.Keys("companyaddress1"))
            'custinfo.Add("sLine2", ConfigurationManager.AppSettings.Keys("companyaddress2"))
            'custinfo.Add("sState", ConfigurationManager.AppSettings.Keys("companycity"))
            'custinfo.Add("sZip", ConfigurationManager.AppSettings.Keys("companyzip"))
            'custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            'custinfo.Add("sEmailId", ConfigurationManager.AppSettings.Keys("companyemail"))
            'custinfo.Add("sAgencyPhn", ConfigurationManager.AppSettings.Keys("companyphone"))
            'custinfo.Add("sComments", "OnLine Booking(" + ConfigurationManager.AppSettings.Keys("companyname") + ")")
            custinfo.Add("Customeremail", FltHdrDs.Tables(0).Rows(0)("PgEmail"))
            custinfo.Add("pay_type", "CL")
            custinfo.Add("sFax", "1")
            custinfo.Add("sCurrency", "INR")
            Dim sMobile As String = "123456789" '
            Dim sContactType As String = "1"
            Dim sContactNum As String = "0"
            Dim PnrDt As DataTable
            If VC = "6E" Then
                '''new code'''''
                Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())

                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")

                If (MBDT.Tables(0).Rows.Count > 0) Then
                    For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                        OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    Next
                End If

                Dim FareType As String = ""
                Try
                    If FltDs.Tables(0).Rows(0)("Provider").ToString.Trim.ToUpper = "SP" Then
                        FareType = "NRM"
                    Else
                        FareType = Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE"))
                    End If
                Catch ex As Exception
                    FareType = Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE"))
                End Try

                Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
                'Dim dsCrd As DataSet = objSql.GetCredentials(VC & FltDs.Tables(0).Rows(0)("AvailableSeats").ToString(), Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                Dim dsCrd As DataSet = objSql.GetCredentials(VC, FareType, "D")
                Dim Org As String = "", Dest As String = "", ViaArrv As String = ""
                Dim objInputs As New STD.Shared.FlightSearch
                If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
                If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
                objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
                objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
                objInputs.Cabin = FltDs.Tables(0).Rows(0)("AdtCabin").ToString().ToUpper()
                objInputs.HidTxtAirLine = VC
                If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                    Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
                End If
                SSRCode = Convert.ToString(FltDs.Tables(0).Rows(0)("SSRCode"))
                Dim inx As Integer = 0
                If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                    inx = 1
                End If

                Dim seginfo As New ArrayList()

                Dim Utlobj As New SpiceIndigoUTL()
                'Dim Ftype As String = Utlobj.Check_Via_Connecting(FltDs.Tables(0))
                'If (Ftype = "Via" And VC = "6E") Then
                '    ViaArrv = FltDs.Tables(0).Rows(0)("ArrivalLocation")
                'End If

                Dim FNO As String = ""
                Dim JSK(inx), FSK(inx) As String 'CC(inx), FNO(inx), DD(inx) 
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
                    Seg.Add("VC", "6E")
                    Seg.Add("Flight", dt1(0)("Flight"))
                    seginfo.Add(Seg)
                Next
                For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                    If (ii = 0) Then
                        Dim Seg As New Dictionary(Of String, String)
                        Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
                        Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
                        'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
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
                Dim Xml As New Dictionary(Of String, String)
                If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                    Dim obj6E As New STD.BAL._6ENAV420._6ENAV(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 420) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    pnrno = obj6E.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", Bag, SSRCode, SeatListO)
                    AirLinePnr = pnrno
                Else
                    Dim obj6E As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 340) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    ' Dim Xml As New Dictionary(Of String, String)
                    pnrno = obj6E.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", Bag, SSRCode, SeatListO)
                    AirLinePnr = pnrno
                End If
                ''' new code end'''
                Try
                    If pnrno <> "" And pnrno IsNot Nothing And InStr(pnrno, "-FQ") <= 0 Then
                    Else
                        Dim xx As String
                        xx = objSql.GetRndm()
                        pnrno = VC & xx & "-FQ"
                        AirLinePnr = VC & xx & "-FQ"
                    End If
                Catch ex As Exception

                End Try
                'objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrDt.Rows(0)("PNRId"), PnrDt.Rows(0)("ReqXml"), PnrDt.Rows(0)("ResXml"), "", "", "", "", "", "", "", "", "", "")
                'objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, Xml("BC-REQ"), Xml("BC-RES"), Xml("APBREQ"), Xml("APBRES"), Xml("SSR"), Xml.Item("SJKREQ"), Xml("SJKRES"), Xml("UPPAXREQ"), Xml("UPPAXRES"), Xml("APBREQ"), Xml("APBRES"), Xml("OTHER"), Xml("UCCONREQ"), Xml("UCCONRES"), Xml("STATEREQ"), Xml("STATERES"))
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, If(Xml.ContainsKey("BC-REQ"), Xml("BC-REQ"), ""), If(Xml.ContainsKey("BC-RES"), Xml("BC-RES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("SSR"), Xml("SSR"), ""), If(Xml.ContainsKey("SJKREQ"), Xml("SJKREQ"), ""), If(Xml.ContainsKey("SJKRES"), Xml("SJKRES"), ""), If(Xml.ContainsKey("UPPAXREQ"), Xml("UPPAXREQ"), ""), If(Xml.ContainsKey("UPPAXRES"), Xml("UPPAXRES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("OTHER"), Xml("OTHER"), ""), If(Xml.ContainsKey("UCCONREQ"), Xml("UCCONREQ"), ""), If(Xml.ContainsKey("UCCONRES"), Xml("UCCONRES"), ""), If(Xml.ContainsKey("STATEREQ"), Xml("STATEREQ"), ""), If(Xml.ContainsKey("STATERES"), Xml("STATERES"), ""), If(Xml.ContainsKey("SBREQ"), Xml("SBREQ"), ""), If(Xml.ContainsKey("SBRES"), Xml("SBRES"), ""))
            ElseIf VC = "G8" Then
                '''new code'''''
                Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")
                If (MBDT.Tables(0).Rows.Count > 0) Then
                    For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                        OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    Next
                End If
                Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
                Dim dsCrd As DataSet = objSql.GetCredentials(VC & FltDs.Tables(0).Rows(0)("AvailableSeats").ToString(), Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                Dim Org As String = "", Dest As String = "", ViaArrv As String = ""
                Dim objInputs As New STD.Shared.FlightSearch
                If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
                If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
                objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
                objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
                objInputs.HidTxtAirLine = VC
                Dim inx As Integer = 0
                If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                    inx = 1
                End If
                Dim seginfo As New ArrayList()
                Dim Utlobj As New SpiceIndigoUTL()
                'Dim Ftype As String = Utlobj.Check_Via_Connecting(FltDs.Tables(0))
                'If (Ftype = "Via" And VC = "6E") Then
                '    ViaArrv = FltDs.Tables(0).Rows(0)("ArrivalLocation")
                'End If

                Dim FNO As String = ""
                Dim JSK(inx), FSK(inx) As String 'CC(inx), FNO(inx), DD(inx) 
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
                    Seg.Add("VC", "G8")
                    Seg.Add("Flight", dt1(0)("Flight"))
                    seginfo.Add(Seg)
                Next
                For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                    If (ii = 0) Then
                        Dim Seg As New Dictionary(Of String, String)
                        Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
                        Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
                        'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
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
                Dim objG8 As New STD.BAL.G8NAV.G8NAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 411) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                Dim Xml As New Dictionary(Of String, String)
                pnrno = objG8.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", SeatListO)
                AirLinePnr = pnrno
                ''' new code end'''
                Try
                    If pnrno <> "" And pnrno IsNot Nothing And InStr(pnrno, "-FQ") <= 0 Then
                    Else
                        Dim xx As String
                        xx = objSql.GetRndm()
                        pnrno = VC & xx & "-FQ"
                        AirLinePnr = VC & xx & "-FQ"
                    End If
                Catch ex As Exception

                End Try
                'objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, If(Xml.ContainsKey("BC-REQ"), Xml("BC-REQ"), ""), If(Xml.ContainsKey("BC-RES"), Xml("BC-RES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("SSR"), Xml("SSR"), ""), If(Xml.ContainsKey("SJKREQ"), Xml("SJKREQ"), ""), If(Xml.ContainsKey("SJKRES"), Xml("SJKRES"), ""), If(Xml.ContainsKey("UPPAXREQ"), Xml("UPPAXREQ"), ""), If(Xml.ContainsKey("UPPAXRES"), Xml("UPPAXRES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("OTHER"), Xml("OTHER"), ""), If(Xml.ContainsKey("UCCONREQ"), Xml("UCCONREQ"), ""), If(Xml.ContainsKey("UCCONRES"), Xml("UCCONRES"), ""), If(Xml.ContainsKey("STATEREQ"), Xml("STATEREQ"), ""), If(Xml.ContainsKey("STATERES"), Xml("STATERES"), ""))
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, If(Xml.ContainsKey("BC-REQ"), Xml("BC-REQ"), ""), If(Xml.ContainsKey("BC-RES"), Xml("BC-RES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("SSR"), Xml("SSR"), ""), If(Xml.ContainsKey("SJKREQ"), Xml.Item("SJKREQ"), ""), If(Xml.ContainsKey("SJKRES"), Xml("SJKRES"), ""), If(Xml.ContainsKey("UPPAXREQ"), Xml("UPPAXREQ"), ""), If(Xml.ContainsKey("UPPAXRES"), Xml("UPPAXRES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("OTHER"), Xml("OTHER"), ""), If(Xml.ContainsKey("UCCONREQ"), Xml("UCCONREQ"), ""), If(Xml.ContainsKey("UCCONRES"), Xml("UCCONRES"), ""), If(Xml.ContainsKey("STATEREQ"), Xml("STATEREQ"), ""), If(Xml.ContainsKey("STATERES"), Xml("STATERES"), ""), If(Xml.ContainsKey("SBREQ"), Xml("SBREQ"), ""), If(Xml.ContainsKey("SBRES"), Xml("SBRES"), ""))
            ElseIf VC = "SG" Then
                'Dim dsCrd As DataSet = objSql.GetCredentials(VC)
                'Dim objSG As New clsSpiceJet(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("APISource"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("CarrierAcc"), "Indigo", dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                'PnrDt = objSG.GetBookingDT(FltDs.Tables(0), custinfo, FltDs.Tables(0).Rows(0)("OriginalTF"), FltHdrDs.Tables(0).Rows(0)("Adult"), FltHdrDs.Tables(0).Rows(0)("Child"), FltHdrDs.Tables(0).Rows(0)("Infant"), FltDs.Tables(0).Rows(0)("Trip"))
                'pnrno = PnrDt.Rows(0)("PNRId")
                'AirLinePnr = PnrDt.Rows(0)("PNRId")
                Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
                Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")

                If (MBDT.Tables(0).Rows.Count > 0) Then
                    For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                        OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice"))
                    Next
                End If

                Dim InfFare As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("InfFare").ToString())
                'Dim dsCrd As DataSet = objSql.GetCredentials(VC)
                Dim dsCrd As DataSet
                If FltDs.Tables(0).Rows(0)("AvailableSeats").ToString().Trim() = "SGTBF" Then
                    dsCrd = objSql.GetCredentials("SGTBF", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                Else
                    dsCrd = objSql.GetCredentials(VC & FltDs.Tables(0).Rows(0)("AvailableSeats").ToString(), Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                End If
                Dim Org As String = "", Dest As String = "", ViaArrv As String = ""
                Dim objInputs As New STD.Shared.FlightSearch
                If FltDs.Tables(0).Rows(FltDs.Tables(0).Rows.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
                If FltDs.Tables(0).Rows(0)("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                objInputs.Adult = FltDs.Tables(0).Rows(0)("Adult")
                objInputs.Child = FltDs.Tables(0).Rows(0)("Child")
                objInputs.Infant = FltDs.Tables(0).Rows(0)("Infant")
                objInputs.Cabin = FltDs.Tables(0).Rows(0)("AdtCabin").ToString().ToUpper()
                objInputs.HidTxtAirLine = VC
                If Not String.IsNullOrEmpty(Convert.ToString(FltDs.Tables(0).Rows(0)("IsBagFare"))) Then
                    Bag = Convert.ToBoolean(FltDs.Tables(0).Rows(0)("IsBagFare"))
                End If
                SSRCode = Convert.ToString(FltDs.Tables(0).Rows(0)("SSRCode"))
                Dim inx As Integer = 0
                If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                    inx = 1
                End If

                Dim seginfo As New ArrayList()

                Dim Utlobj As New SpiceIndigoUTL()
                'Dim Ftype As String = Utlobj.Check_Via_Connecting(FltDs.Tables(0))
                'If (Ftype = "Via" And VC = "6E") Then
                '    ViaArrv = FltDs.Tables(0).Rows(0)("ArrivalLocation")
                'End If

                Dim FNO As String = ""
                Dim JSK(inx), FSK(inx) As String 'CC(inx), FNO(inx), DD(inx) 
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
                    Seg.Add("VC", "SG")
                    Seg.Add("Flight", dt1(0)("Flight"))
                    seginfo.Add(Seg)
                Next


                For ii As Integer = 0 To FltDs.Tables(0).Rows.Count - 1
                    If (ii = 0) Then
                        Dim Seg As New Dictionary(Of String, String)
                        Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom")
                        Dest = FltDs.Tables(0).Rows(ii)("OrgDestTo")
                        'OriginalTF = Convert.ToDecimal(FltDs.Tables(0).Rows(ii)("OriginalTF").ToString())
                    End If
                    If (Org = FltDs.Tables(0).Rows(ii)("OrgDestFrom").ToString()) Then
                        JSK(0) = FltDs.Tables(0).Rows(ii)("sno")
                        FSK(0) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                        ViaArr(0) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                    ElseIf (Org = FltDs.Tables(0).Rows(ii)("OrgDestTo").ToString()) Then
                        JSK(1) = FltDs.Tables(0).Rows(ii)("sno")
                        FSK(1) = FltDs.Tables(0).Rows(ii)("Searchvalue")
                        ViaArr(1) = Utlobj.Check_Via_Connecting(FltDs.Tables(0), FltDs.Tables(0).Rows(ii)("Flight"), VC)
                    End If
                Next
                'Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId)

                'Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                'Dim Xml As New Dictionary(Of String, String)
                'pnrno = objSG.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", Bag, SSRCode)
                'AirLinePnr = pnrno

                Dim Xml As New Dictionary(Of String, String)
                If (dsCrd.Tables(0).Rows(0)("ServerIP") = "V4") Then
                    Dim objSG As New STD.BAL.SGNAV420.SGNAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    'Dim Xml As New Dictionary(Of String, String)
                    pnrno = objSG.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", Bag, SSRCode, SeatListO)
                    AirLinePnr = pnrno
                Else
                    Dim objSG As New STD.BAL.SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    'Dim Xml As New Dictionary(Of String, String)
                    pnrno = objSG.Spice_GetPnr(objInputs, JSK, FSK, seginfo, PaxDs.Tables(0), OriginalTF, InfFare, custinfo, Xml, MBDT.Tables(0), ViaArr, FT, PROMOCODE, "BOTH", Bag, SSRCode, SeatListO)
                    AirLinePnr = pnrno
                End If
                Try
                    If pnrno <> "" And pnrno IsNot Nothing And InStr(pnrno, "-FQ") <= 0 Then
                    Else
                        Dim xx As String
                        xx = objSql.GetRndm()
                        pnrno = VC & xx & "-FQ"
                        AirLinePnr = VC & xx & "-FQ"
                    End If
                Catch ex As Exception

                End Try
                'objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, Xml("BC-REQ"), Xml("BC-RES"), Xml("APBREQ"), Xml("APBRES"), Xml("SSR"), Xml.Item("SJKREQ"), Xml("SJKRES"), Xml("UPPAXREQ"), Xml("UPPAXRES"), Xml("APBREQ"), Xml("APBRES"), Xml("OTHER"), Xml("UCCONREQ"), Xml("UCCONRES"), Xml("STATEREQ"), Xml("STATERES"))
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), pnrno, If(Xml.ContainsKey("BC-REQ"), Xml("BC-REQ"), ""), If(Xml.ContainsKey("BC-RES"), Xml("BC-RES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("SSR"), Xml("SSR"), ""), If(Xml.ContainsKey("SJKREQ"), Xml("SJKREQ"), ""), If(Xml.ContainsKey("SJKRES"), Xml("SJKRES"), ""), If(Xml.ContainsKey("UPPAXREQ"), Xml("UPPAXREQ"), ""), If(Xml.ContainsKey("UPPAXRES"), Xml("UPPAXRES"), ""), If(Xml.ContainsKey("APBREQ"), Xml("APBREQ"), ""), If(Xml.ContainsKey("APBRES"), Xml("APBRES"), ""), If(Xml.ContainsKey("OTHER"), Xml("OTHER"), ""), If(Xml.ContainsKey("UCCONREQ"), Xml("UCCONREQ"), ""), If(Xml.ContainsKey("UCCONRES"), Xml("UCCONRES"), ""), If(Xml.ContainsKey("STATEREQ"), Xml("STATEREQ"), ""), If(Xml.ContainsKey("STATERES"), Xml("STATERES"), ""), If(Xml.ContainsKey("SBREQ"), Xml("SBREQ"), ""), If(Xml.ContainsKey("SBRES"), Xml("SBRES"), ""))
            ElseIf VC = "IX" Then
                Dim xx As String
                xx = objSql.GetRndm()
                pnrno = VC & xx & "-DOMSPR"
                AirLinePnr = VC & xx & "-DOMSPR"
            End If
        Catch ex As Exception

        End Try
        Return pnrno
    End Function
    Private Function FuncIssueLccCpnBooking(ByVal sessionid As String, ByVal VC As String, ByVal PaxDs As DataSet, ByVal AgencyDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String
        'Dim dsCrd As DataSet = objSql.GetCredentials(VC)
        Dim CpnDt As New DataTable
        Dim PnrNo As String = ""
        Dim custinfo As New Hashtable
        Dim PaxArray As Array
        Dim cnt As Integer = 1

        Try
            PaxArray = PaxDs.Tables(0).Select("PaxType='ADT'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_ADT" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameADT" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameADT" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("ADTAge" & i + 1, "30")
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='CHD'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_CHD" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameCHD" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameCHD" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("CHDAge" & i + 1, "10")
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='INF'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_INF" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameINF" & i + 1, (PaxArray(i)("FName")) & " " & (PaxArray(i)("MName")))
                custinfo.Add("LnameINF" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("INFAge" & i + 1, "2")
            Next

            custinfo.Add("Ttl", FltHdrDs.Tables(0).Rows(0)("PgTitle"))
            custinfo.Add("FName", FltHdrDs.Tables(0).Rows(0)("PgFName"))
            custinfo.Add("LName", FltHdrDs.Tables(0).Rows(0)("PgLName"))
            'custinfo.Add("sAddName", ConfigurationManager.AppSettings.Keys("companyname"))
            'custinfo.Add("sCity", ConfigurationManager.AppSettings.Keys("companycity"))
            'custinfo.Add("sCountry", ConfigurationManager.AppSettings.Keys("companycountry"))
            'custinfo.Add("sLine1", ConfigurationManager.AppSettings.Keys("companyaddress1"))
            'custinfo.Add("sLine2", ConfigurationManager.AppSettings.Keys("companyaddress2"))
            'custinfo.Add("sState", ConfigurationManager.AppSettings.Keys("companystate"))
            'custinfo.Add("sZip", ConfigurationManager.AppSettings.Keys("companyzip"))
            'custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            'custinfo.Add("sEmailId", ConfigurationManager.AppSettings.Keys("companyemail"))
            'custinfo.Add("sAgencyPhn", ConfigurationManager.AppSettings.Keys("companyphone"))
            'custinfo.Add("sComments", "OnLine Booking(" + ConfigurationManager.AppSettings.Keys("companyname") + ")")
            custinfo.Add("sAddName", Resources.Address.CompanyName)
            custinfo.Add("sCity", Resources.Address.City)
            custinfo.Add("sCountry", Resources.Address.Country)
            custinfo.Add("sLine1", Resources.Address.AddLine1)
            custinfo.Add("sLine2", Resources.Address.AddLine2)
            custinfo.Add("sState", Resources.Address.State)
            custinfo.Add("sZip", Resources.Address.Zip)
            custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            custinfo.Add("sEmailId", Resources.Address.Email)
            custinfo.Add("sAgencyPhn", Resources.Address.PhoneNo)
            custinfo.Add("sComments", Resources.Address.Comments)
            custinfo.Add("Customeremail", FltHdrDs.Tables(0).Rows(0)("PgEmail"))
            custinfo.Add("pay_type", "CL")
            custinfo.Add("sFax", "1")
            custinfo.Add("sCurrency", "INR")
            Dim flg As String = "False"
            Try
                Dim dsCrd As DataSet
                Dim objBKGSPLFARE As New clsBKGSPLFARE()
                Dim PnrDt As New DataTable
                If VC.ToUpper.Trim = "G8" AndAlso PnrNo = "" Then
                    dsCrd = objSql.GetCredentials("G8CPN", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                    If dsCrd.Tables(0).Rows(0)("SearchType").ToString().Trim() = "BKG" Then
                        PnrDt = objBKGSPLFARE.BookTicket(VC, dsCrd, FltDs, custinfo, flg)
                        If PnrDt.Rows.Count > 0 Then
                            PnrNo = PnrDt.Rows(0)("ConfirmationNo")
                            AirLinePnr = PnrDt.Rows(0)("ConfirmationNo")
                            objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrDt.Rows(0)("ConfirmationNo"), PnrDt.Rows(0)("BookReq"), PnrDt.Rows(0)("BookRes"), PnrDt.Rows(0)("AddPayReq"), PnrDt.Rows(0)("AddPayRes"), PnrDt.Rows(0)("ConfirmPayRes"), "", "", "", "", "", "", "", "", "", "", "", "", "")
                        End If
                    End If
                ElseIf VC.ToUpper.Trim = "G8CORP" AndAlso PnrNo = "" Then
                    dsCrd = objSql.GetCredentials("G8CORP", Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                    If dsCrd.Tables(0).Rows(0)("SearchType").ToString().Trim() = "BKG" Then
                        PnrDt = objBKGSPLFARE.BookTicket(VC, dsCrd, FltDs, custinfo, flg)
                        If PnrDt.Rows.Count > 0 Then
                            PnrNo = PnrDt.Rows(0)("ConfirmationNo")
                            AirLinePnr = PnrDt.Rows(0)("ConfirmationNo")
                            objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrDt.Rows(0)("ConfirmationNo"), PnrDt.Rows(0)("BookReq"), PnrDt.Rows(0)("BookRes"), PnrDt.Rows(0)("AddPayReq"), PnrDt.Rows(0)("AddPayRes"), PnrDt.Rows(0)("ConfirmPayRes"), "", "", "", "", "", "", "", "", "", "", "", "", "")
                        End If
                    End If
                End If
            Catch ex As Exception
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrNo, ex.ToString().Replace("'", ""), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "Seat", "")
            End Try

            If flg = "False" AndAlso PnrNo = "" Then
                CpnDt = objLccCpn.CheckCouponFare(FltHdrDs.Tables(0).Rows(0)("OrderId"), VC, sessionid, "Spring", FltHdrDs.Tables(0).Rows(0)("AgentId"), AgencyDs.Tables(0).Rows(0)("Mobile"), "", FltHdrDs.Tables(0).Rows(0)("AgentId"))
                If CpnDt.Rows.Count > 0 Then
                    If CpnDt.Rows(0)("STATUS").ToString().ToUpper().Trim = "FAILED" Then
                        If VC = "IX" Then
                            PnrNo = VC & objSql.GetRndm() & "-DOMSPR"
                        Else
                            PnrNo = VC & objSql.GetRndm() & "-SPL"
                        End If
                        AirLinePnr = PnrNo
                    Else
                        PnrNo = CpnDt.Rows(0)("PNR").ToString().ToUpper().Trim
                        AirLinePnr = PnrNo
                    End If
                Else
                    If VC = "IX" Then
                        PnrNo = VC & objSql.GetRndm() & "-DOMSPR"
                    ElseIf VC = "SGAPI" Then
                        PnrNo = VC & objSql.GetRndm() & "-SPR"
                    Else
                        PnrNo = VC & objSql.GetRndm() & "-SPL"
                    End If
                    AirLinePnr = PnrNo
                End If
            End If
        Catch ex As Exception
            If VC = "IX" Then
                PnrNo = VC & objSql.GetRndm() & "-DOMSPR"
            ElseIf VC = "SGAPI" Then
                PnrNo = VC & objSql.GetRndm() & "-SPR"
            Else
                PnrNo = VC & objSql.GetRndm() & "-SPL"
            End If
            AirLinePnr = PnrNo
        End Try
        Return PnrNo
    End Function

    Private Sub FuncDBUpdation(ByVal OrderId As String, ByVal AgencyName As String, ByVal UID As String, ByVal TotalFare As Double, ByVal NetFare As Double, ByVal Sector As String, ByVal VC As String, ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal BkgStatus As String)
        'Dim AvlBal As Double = 0
        objDA.UpdateFltHeader(OrderId, AgencyName, GdsPnr, AirlinePnr, BkgStatus)
        'AvlBal = objDA.UpdateCrdLimit(UID, NetFare)
        'objDA.UpdateTransReport(UID, AgencyName, GdsPnr, BkgStatus, AvlBal, TotalFare, NetFare, "Dom. Flight Booking", Sector, "CL", VC)
        'strTktCopy = objTktCopy.TicketDetail(trackid, "")
        'strTktCopy = "Unable to confirm your booking. Please try again."
        'Return AvlBal
    End Sub
    Private Function FuncAmountDeduction(ByVal UID As String, ByVal NetFare As Double) As Double
        Dim AvlBal As Double = 0
        'objDA.UpdateFltHeader(OrderId, AgencyName, GdsPnr, AirlinePnr, BkgStatus)
        AvlBal = objDA.UpdateCrdLimit(UID, NetFare)
        'objDA.UpdateTransReport(UID, AgencyName, GdsPnr, BkgStatus, AvlBal, TotalFare, NetFare, "Dom. Flight Booking", Sector, "CL", VC)
        'strTktCopy = objTktCopy.TicketDetail(trackid, "")
        'strTktCopy = "Unable to confirm your booking. Please try again."
        Return AvlBal
    End Function
    Private Sub PaxAndLedgerDbUpdation(ByVal OrderId As String, ByVal VC As String, ByVal GdsPnr As String, ByVal TktNoArray As ArrayList, ByVal PaxDs As DataSet)
        'Dim CurrBal As Double = 0
        'CurrBal = AvlBal + NetFare
        For i As Integer = 0 To PaxDs.Tables(0).Rows.Count - 1
            Dim strTktNo As String = ""
            If VC <> "SG" And VC <> "6E" And VC <> "G8" And VC <> "IX" And VC <> "AK" And VC <> "I5" And VC <> "LB" And VC <> "VA" And VC <> "2T" Then
                Dim PNameFromTbl As String = "", PNameFromTktArray As String = ""
                If PaxDs.Tables(0).Rows(i)("PaxType").ToString.Trim.ToUpper = "INF" Then
                    PNameFromTbl = (PaxDs.Tables(0).Rows(i)("FName") & PaxDs.Tables(0).Rows(i)("MName") & PaxDs.Tables(0).Rows(i)("LName")).ToString.Replace(" ", "").ToUpper.Trim
                Else
                    PNameFromTbl = (PaxDs.Tables(0).Rows(i)("FName") & PaxDs.Tables(0).Rows(i)("MName") & PaxDs.Tables(0).Rows(i)("Title") & PaxDs.Tables(0).Rows(i)("LName")).ToString.Replace(" ", "").ToUpper.Trim
                End If
                Try
                    If InStr(TktNoArray(0).ToString.ToUpper, "AIRLINE") > 0 Then
                        objSqlDom.UpdateLedger_PaxId(Convert.ToInt32(PaxDs.Tables(0).Rows(i)("PaxId")), "", GdsPnr)
                    Else
                        For ii As Integer = 0 To TktNoArray.Count - 1
                            strTktNo = ""
                            Dim strtktArray() As String = Split(TktNoArray(ii), "/")
                            PNameFromTktArray = strtktArray(0).ToString.Replace(" ", "").ToUpper.Trim
                            strTktNo = strtktArray(1).ToString
                            'If InStr(PNameFromTbl, PNameFromTktArray) > 0 Then
                            '    objSql.UpdateTktNumber(OrderId, PaxDs.Tables(0).Rows(i)("PaxId"), strtktArray(1).ToString)
                            '    Exit For
                            'End If
                            If PNameFromTbl = PNameFromTktArray Then
                                'objSql.UpdateTktNumber(OrderId, PaxDs.Tables(0).Rows(i)("PaxId"), strtktArray(1).ToString)
                                objSqlDom.UpdateLedger_PaxId(Convert.ToInt32(PaxDs.Tables(0).Rows(i)("PaxId")), strtktArray(1).ToString, GdsPnr)
                                Exit For
                            End If
                        Next
                    End If
                Catch ex As Exception

                End Try
            Else
                strTktNo = GdsPnr & (i + 1).ToString
                'objSql.UpdateTktNumber(OrderId, PaxDs.Tables(0).Rows(i)("PaxId"), strTktNo)
                objSqlDom.UpdateLedger_PaxId(Convert.ToInt32(PaxDs.Tables(0).Rows(i)("PaxId")), strTktNo, GdsPnr)
            End If
            'Dim fareArray As Array
            'fareArray = FltFareDs.Tables(0).Select("PaxType='" & PaxDs.Tables(0).Rows(i)("PaxType").ToString.Trim & "'", "")
            'CurrBal = CurrBal - (fareArray(0)("TotalAfterDis"))
            'objSqlDom.insertLedgerDetails(AgentId, AgencyName, OrderId, GdsPnr, strTktNo, VC, "", "", "", Request.UserHostAddress.ToString, (fareArray(0)("TotalAfterDis")), 0, CurrBal, "DomFlt", "", PaxDs.Tables(0).Rows(i)("PaxId"), ProjectId, BookedBy, BillNo)
            'objSqlDom.UpdateLedger_PaxId(Convert.ToInt32(PaxDs.Tables(0).Rows(i)("PaxId")), strTktNo, GdsPnr)
        Next
    End Sub
    Public Function datecon(ByVal MM As String) As String
        Dim mm_str As String = ""
        Select Case MM
            Case "01"
                mm_str = "JAN"
            Case "02"
                mm_str = "FEB"
            Case "03"
                mm_str = "MAR"
            Case "04"
                mm_str = "APR"
            Case "05"
                mm_str = "MAY"
            Case "06"
                mm_str = "JUN"
            Case "07"
                mm_str = "JUL"
            Case "08"
                mm_str = "AUG"
            Case "09"
                mm_str = "SEP"
            Case "10"
                mm_str = "OCT"
            Case "11"
                mm_str = "NOV"
            Case "12"
                mm_str = "DEC"
            Case Else

        End Select

        Return mm_str

    End Function
    Private Function mailTktCopy(ByVal VC As String, ByVal FltNo As String, ByVal Sector As String, ByVal DepDate As String, ByVal FT As String, ByVal AirlinePnr As String, ByVal GdsPnr As String, ByVal BkgStatus As String, ByVal OrderId As String, ByVal EmailId As String) As String
        Dim strFileNmPdf As String = ""
        Dim strTktCopy As String = "", strHTML As String = "", strFileName As String = "", strMailMsg As String = ""
        Dim rightHTML As Boolean = False
        Try
            Dim objtkt As New TktCopyForMail()
            'strFileName = ConfigurationManager.AppSettings("SPR_TicketCopy").ToString().Trim() & GdsPnr & "-" & FT & " Flight details-" & DateAndTime.Now.ToString.Replace(":", "").Trim & ".html"
            ' strFileName = strFileName.Replace("/", "-")
            strTktCopy = TicketCopyExportPDF(OrderId, "") ''objtkt.TicketDetail(OrderId, "", 0, "")
            strHTML = "<html><head><title>Booking Details</title><style type='text/css'> .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" & strTktCopy & "</body></html>"
            Try
                ''Dim s As String = ""
                Dim Body As String = ""

                Dim TicketFormate As String
                Dim writePDF As Boolean = False
                Dim status1 As Integer = 0
                TicketFormate = strTktCopy.Trim.ToString
                strFileNmPdf = ConfigurationManager.AppSettings("HTMLtoPDF").ToString().Trim() + OrderId + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf"
                Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4, 2, 2, 0, 0)
                Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                pdfDoc.Open()
                Dim sr As New StringReader(TicketFormate.ToString)
                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
                pdfDoc.Close()
                writer.Dispose()
                sr.Dispose()
                pdfDoc.Dispose()
                writePDF = True

            Catch ex As Exception

                Dim stt As String = ex.Message
            End Try
            strMailMsg = strHTML ''strMailMsg & strTktCopy

            If BkgStatus = "Ticketed" Then
                Dim MailDt As New DataTable
                MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                If rightHTML Then
                    objSqlDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), "sales@tripforo.com", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), strFileNmPdf)
                Else
                    objSqlDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(),"sales@tripforo.com", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), strFileNmPdf)
                End If

            End If
        Catch ex As Exception

        End Try
        Return strTktCopy
    End Function
    Private Sub MessageForInfantPax(ByVal Pnr As String, ByVal VC As String, ByVal PaxDs As DataSet)
        If VC = "SG" Or VC = "6E" Then
            Dim pgstr As String = ""
            Dim PaxArray As Array
            PaxArray = PaxDs.Tables(0).Select("PaxType='INF'", "")
            Try
                If PaxArray.Length > 0 Then
                    pgstr = "Dear Team,<br /><br />"
                    pgstr = pgstr & "Please Contact LCC Airline For Infant Passenger(PNR : " & Pnr & " And Airline : " & VC & ").<br /><br />"
                    Try
                        For k As Integer = 0 To PaxArray.Length - 1
                            Dim name As String = (PaxArray(k)("Title")) & " " & (PaxArray(k)("FName")) & " " & (PaxArray(k)("MName")) & " " & (PaxArray(k)("LName"))
                            pgstr = pgstr & "Infant Name : " & name.Trim & " dateOfBirth(DD/MM/YYYY) : " & (PaxArray(k)("DOB")).ToString
                        Next
                    Catch ex As Exception

                    End Try

                    ' Dim STDom As New SqlTransactionDom
                    Dim MailDt As New DataTable
                    MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_MAILINGINFANT.ToString(), Session("UID").ToString()).Tables(0)

                    pgstr = pgstr & "<br />Regards,<br />" & MailDt.Rows(0)("REGARDS").ToString() & ""

                    Try
                        If (MailDt.Rows.Count > 0) Then
                            Dim Status As Boolean = False
                            Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                            If Status = True Then
                                Dim i As Integer = objSqlDom.SendMail(MailDt.Rows(0)("MAILTO").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), pgstr, MailDt.Rows(0)("SUBJECT").ToString(), "")
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        Dim Contents As String
        Dim Saved As Boolean = False
        Dim objReader As System.IO.StreamWriter
        Try
            objReader = New System.IO.StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            Saved = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return Saved
    End Function
    Public Function GetMerchantKey(ByVal orderID As String) As String


        Dim mrchntKey As String = ConfigurationManager.AppSettings("MerchantKey").ToString()

        Try


            Dim provider12 As String = ""
            Dim sqldom As New SqlTransactionDom()

            Dim dsp As New DataSet()

            dsp = sqldom.GetTicketingProvider(orderID)




            If (dsp.Tables.Count > 0) Then
                If (dsp.Tables(0).Rows.Count > 0) Then

                    provider12 = dsp.Tables(0).Rows(0)(0)

                End If


            End If


            If provider12.ToLower().Trim() = "yatra" Then

                mrchntKey = ConfigurationManager.AppSettings("YatraITZMerchantKey").ToString()
            End If

        Catch ex As Exception
            mrchntKey = ConfigurationManager.AppSettings("MerchantKey").ToString()
        End Try
        Session("MchntKeyITZ") = mrchntKey
        Return mrchntKey

    End Function
    Private Function FunBookedFlightByPaumentGateway(ByVal FltDs As DataSet, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltFareDs As DataSet, ByVal AgencyDs As DataSet, ByVal vc As String, ByVal OBTrackId As String, ByVal IBTrackId As String) As String

        Dim amtbeforded As String = ""
        Dim objItzT As New Itz_Trans_Dal
        Dim inst As Boolean = False
        Dim objIzT As New ITZ_Trans
        Dim strTktCopy As String = ""
        FT = Request.QueryString("FT")
        OBTrackId = Request.QueryString("OBTID")
        IBTrackId = Request.QueryString("IBTID")

        Dim strlenInbound As Integer = 0
        If Not [String].IsNullOrEmpty(IBTrackId) Then
            strlenInbound = IBTrackId.Length
            If strlenInbound > 6 Then
                FT = "INBOUND"
            End If
        End If


        'Dim FltDs As DataSet
        'Dim PaxDs As DataSet
        'Dim FltHdrDs As DataSet
        'Dim FltFareDs As DataSet
        'Dim AgencyDs As DataSet
        Dim PgDs As DataSet
        Dim BkgStatus As String = ""
        'Dim vc As String = ""
        Dim GdsPnr As String = ""
        Dim AirlinePnr As String = ""
        Dim smsStatus As String = ""
        Dim smsMsg As String = ""
        Dim TktNoArray As New ArrayList
        Dim AvlBal As Double = 0
        Dim NetFareOutBound As Double = Convert.ToDouble(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))

        ''to Get Mechant key
        'Comment by Devesh
        'GetMerchantKey(OBTrackId)
        Dim ProjectId As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
        Dim BookedBy As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDs.Tables(0).Rows(0)("BookedBy").ToString())
        Dim BillNoCorp As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDs.Tables(0).Rows(0)("BillNoCorp").ToString())


        Dim objPg As New PG.PaymentGateway()
        PgDs = objPg.GetPaymentDetails(OBTrackId, Session("UID"))


        If FltDs.Tables(0).Rows.Count > 0 AndAlso PaxDs.Tables(0).Rows.Count > 0 AndAlso FltHdrDs.Tables(0).Rows.Count > 0 AndAlso FltFareDs.Tables(0).Rows.Count > 0 AndAlso Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")) = "PG" AndAlso Convert.ToString(PgDs.Tables(0).Rows(0)("Status")) = "Success" Then
            ''''''''
            If FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "TICKETED" And FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                    If Convert.ToDouble(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")) <= Convert.ToDouble(PgDs.Tables(0).Rows(0)("PgAmount")) Then
                        'Dim abc = ToXML(objDebResp)
                        'AvlBal = FuncAmountDeduction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))                       
                        Dim Result As Integer = 0
                        Result = objSqlDom.Ledgerandcreditlimit_Transaction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), OBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Request.UserHostAddress.ToString(), ProjectId, BookedBy, BillNoCorp, 0, Convert.ToString(PgDs.Tables(0).Rows(0)("PaymentId")))
                        If Result = 1 AndAlso objDebResp IsNot Nothing Then 'Result = 1
                            BkgStatus = PnrCreation(vc, PaxDs, FltHdrDs, FltDs, TktNoArray, GdsPnr, AirlinePnr, AgencyDs, Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")))

                            If String.IsNullOrEmpty(GdsPnr) Then GdsPnr = objSql.GetRndm() & "-FQ"

                            If GdsPnr <> "" Then 'And InStr(GdsPnr, "-FQ") <= 0
                                FuncDBUpdation(OBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                'PaxAndLedgerDbUpdation(OBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDs, FltFareDs, ProjectId, BookedBy, BillNoCorp)
                                PaxAndLedgerDbUpdation(OBTrackId, vc, GdsPnr, TktNoArray, PaxDs)
                                MessageForInfantPax(GdsPnr, vc, PaxDs)
                                strTktCopy = mailTktCopy(vc, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltHdrDs.Tables(0).Rows(0)("Sector"), FltDs.Tables(0).Rows(0)("Departure_Date"), "OutBound", AirlinePnr, GdsPnr, BkgStatus, OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgEmail"))

                                If (BkgStatus = "Ticketed") Then
                                    'YTR Integration
                                    'Online Billing
                                    Try
                                        'Dim AirObj As New AIR_YATRA
                                        'AirObj.ProcessYatra_Air(OBTrackId, GdsPnr, "B")
                                    Catch ex As Exception

                                    End Try
                                    'NAV METHOD CALL START
                                    Try

                                        'Dim objNav As New AirService.clsConnection(OBTrackId, "0", "0")
                                        'objNav.airBookingNav(OBTrackId, "", 0)

                                    Catch ex As Exception

                                    End Try
                                    'Nav METHOD END'
                                    'Online Billing end

                                    Try
                                        Dim SmsCrd As DataTable
                                        SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)

                                        If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                            smsStatus = objSMSAPI.sendSms(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                            objSql.SmsLogDetails(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                        End If

                                    Catch ex As Exception
                                    End Try
                                End If

                                If Not (BkgStatus.Trim().ToLower().Contains("ticketed")) Or GdsPnr.Trim().Contains("-FQ") Then
                                    Try
                                        Dim objDS As New Distributor()
                                        Dim dsConfigMail As New DataSet()
                                        Dim MailDt As New DataTable
                                        Dim subHdr As String = ""
                                        Dim bodyHdr As String = ""
                                        Dim tomail As String = ""
                                        Dim isAct As String = ""
                                        Try
                                            dsConfigMail = objDS.GetConfigureMails()
                                        Catch ex As Exception

                                        End Try

                                        If GdsPnr.Trim().Contains("-FQ") Then
                                            subHdr = "Domestic Air Booking Failed"
                                            bodyHdr = "Domestic Failed Air Booking Details"
                                            ''Try
                                            ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("ToEmail").ToString()
                                            ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("IsActive").ToString()
                                            ''Catch ex As Exception

                                            ''End Try
                                        Else
                                            subHdr = "Domestic Air Booking On Hold"
                                            bodyHdr = "Domestic Hold Air Booking Details"
                                            ''Try
                                            ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("ToEmail").ToString()
                                            ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("IsActive").ToString()
                                            ''Catch ex As Exception

                                            ''End Try
                                        End If

                                        Try
                                            MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                                        Catch ex As Exception

                                        End Try
                                        ''If Convert.ToBoolean(isAct) = True AndAlso MailDt IsNot Nothing Then
                                        If MailDt IsNot Nothing Then
                                            If MailDt.Rows.Count > 0 Then
                                                Try
                                                    Dim strMailMsgHold As String
                                                    Dim newDepDate As String = ""
                                                    newDepDate = FltDs.Tables(0).Rows(0)("DepartureDate").ToString()
                                                    newDepDate = newDepDate.Insert(4, "/")
                                                    newDepDate = newDepDate.Insert(7, "/")
                                                    strMailMsgHold = "<table>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><h2>" & bodyHdr & "</h2>"
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><b>Customer ID: </b>" + Session("UID").ToString
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><b>Departure Date: </b>" + Convert.ToDateTime(newDepDate).ToString("dd/MM/yyyy")
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><b>Pnr No: </b>" + GdsPnr
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OBTrackId
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                                    strMailMsgHold = strMailMsgHold & "<td><b>Fare: </b>" + FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                    strMailMsgHold = strMailMsgHold & "</td>"
                                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                                    strMailMsgHold = strMailMsgHold & "</table>"
                                                    Try
                                                        Dim mailRow = If(GdsPnr.Trim().Contains("-FQ"), dsConfigMail.Tables(0).Select("ModuleType='Failed'"), dsConfigMail.Tables(0).Select("ModuleType='Hold'"))
                                                        If mailRow.Length > 0 Then
                                                            For ml As Integer = 0 To mailRow.Length - 1
                                                                If Convert.ToBoolean(mailRow(ml)("IsActive").ToString()) Then
                                                                    objSqlDom.SendMail(mailRow(ml)("ToEmail").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                End If
                                                            Next
                                                        End If
                                                    Catch ex As Exception

                                                    End Try
                                                    ' ''If Convert.ToBoolean(isAct) Then
                                                    ' ''    objSqlDom.SendMail(tomail, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                    ' ''End If

                                                Catch ex As Exception

                                                End Try

                                            End If

                                        End If

                                    Catch ex As Exception

                                    End Try
                                End If


                                If FT.ToUpper.Trim = "INBOUND" And InStr(GdsPnr, "-FQ") <= 0 Then
                                    objDebResp = Nothing
                                    FltDs.Clear()
                                    PaxDs.Clear()
                                    FltHdrDs.Clear()
                                    FltFareDs.Clear()
                                    GdsPnr = ""
                                    AirlinePnr = ""
                                    smsStatus = ""
                                    smsMsg = ""
                                    TktNoArray.Clear()
                                    vc = ""
                                    AvlBal = 0
                                    FltDs = objDA.GetFltDtls(IBTrackId, Session("UID"))
                                    PaxDs = objDA.GetPaxDetails(IBTrackId)
                                    FltHdrDs = objDA.GetHdrDetails(IBTrackId)
                                    FltFareDs = objDA.GetFltFareDtl(IBTrackId)
                                    vc = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")

                                    '' to get merchant key
                                    'Comment by Devesh
                                    'GetMerchantKey(IBTrackId)

                                    Dim ProjectIdIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
                                    Dim BookedByIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDs.Tables(0).Rows(0)("BookedBy").ToString())
                                    Dim BillNoCorpIN As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDs.Tables(0).Rows(0)("BillNoCorp").ToString())



                                    If FltDs.Tables(0).Rows.Count > 0 AndAlso PaxDs.Tables(0).Rows.Count > 0 AndAlso FltHdrDs.Tables(0).Rows.Count > 0 AndAlso FltFareDs.Tables(0).Rows.Count > 0 Then
                                        ''''''
                                        If FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "TICKETED" And FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                                            ''If FltHdrDs.Tables(0).Rows(0)("TotalAfterDis") <= Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) Then
                                            '''''
                                            If Convert.ToDouble(FltHdrDs.Tables(0).Rows(0)("TotalAfterDis")) + NetFareOutBound <= Convert.ToDouble(PgDs.Tables(0).Rows(0)("PgAmount")) Then
                                                'AvlBal = FuncAmountDeduction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))   
                                                Dim Result1 As Integer = 0
                                                Result1 = objSqlDom.Ledgerandcreditlimit_Transaction(Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), IBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Request.UserHostAddress.ToString(), ProjectId, BookedBy, BillNoCorp, 0, Convert.ToString(PgDs.Tables(0).Rows(0)("PaymentId")))
                                                If Result1 = 1 Then
                                                    BkgStatus = PnrCreation(vc, PaxDs, FltHdrDs, FltDs, TktNoArray, GdsPnr, AirlinePnr, AgencyDs, Convert.ToString(FltHdrDs.Tables(0).Rows(0)("PaymentMode")))
                                                    If String.IsNullOrEmpty(GdsPnr) Then GdsPnr = objSql.GetRndm() & "-FQ"
                                                    If GdsPnr <> "" Then
                                                        FuncDBUpdation(IBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                                        'PaxAndLedgerDbUpdation(IBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDs, FltFareDs, ProjectIdIN, BookedByIN, BillNoCorpIN)
                                                        PaxAndLedgerDbUpdation(IBTrackId, vc, GdsPnr, TktNoArray, PaxDs)
                                                        MessageForInfantPax(GdsPnr, vc, PaxDs)
                                                        strTktCopy = strTktCopy & "<br/>" & mailTktCopy(vc, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltHdrDs.Tables(0).Rows(0)("Sector"), FltDs.Tables(0).Rows(0)("Departure_Date"), "InBound", AirlinePnr, GdsPnr, BkgStatus, IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgEmail"))
                                                        If (BkgStatus = "Ticketed") Then
                                                            'YTR Integration
                                                            'Online Billing
                                                            Try
                                                                'Dim AirObj As New AIR_YATRA
                                                                'AirObj.ProcessYatra_Air(IBTrackId, GdsPnr, "B")
                                                            Catch ex As Exception

                                                            End Try
                                                            'NAV METHOD  CALL START
                                                            Try

                                                                'Dim objNav As New AirService.clsConnection(IBTrackId, "0", "0")
                                                                'objNav.airBookingNav(IBTrackId, "", 0)

                                                            Catch ex As Exception

                                                            End Try
                                                            'Nav METHOD END'
                                                            'Online Billing end

                                                            Try
                                                                Dim SmsCrd As DataTable
                                                                SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)

                                                                If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                                                    smsStatus = objSMSAPI.sendSms(IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                                                    objSql.SmsLogDetails(IBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                                                End If

                                                            Catch ex As Exception
                                                            End Try
                                                        End If

                                                        If Not (BkgStatus.Trim().ToLower().Contains("ticketed")) Or GdsPnr.Trim().Contains("-FQ") Then
                                                            Try
                                                                Dim objDS As New Distributor()
                                                                Dim dsConfigMail As New DataSet()
                                                                Dim MailDt As New DataTable
                                                                Dim subHdr As String = ""
                                                                Dim bodyHdr As String = ""
                                                                Dim tomail As String = ""
                                                                Dim isAct As String = ""
                                                                Try
                                                                    dsConfigMail = objDS.GetConfigureMails()
                                                                Catch ex As Exception

                                                                End Try
                                                                If GdsPnr.Trim().Contains("-FQ") Then
                                                                    subHdr = "Domestic Air Booking Failed"
                                                                    bodyHdr = "Domestic Failed Air Booking Details"
                                                                    ''Try
                                                                    ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("ToEmail").ToString()
                                                                    ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Failed'")(0)("IsActive").ToString()
                                                                    ''Catch ex As Exception

                                                                    ''End Try

                                                                Else
                                                                    subHdr = "Domestic Air Booking On Hold"
                                                                    bodyHdr = "Domestic Hold Air Booking Details"
                                                                    ''Try
                                                                    ''    tomail = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("ToEmail").ToString()
                                                                    ''    isAct = dsConfigMail.Tables(0).Select("ModuleType='Hold'")(0)("IsActive").ToString()
                                                                    ''Catch ex As Exception

                                                                    ''End Try

                                                                End If

                                                                Try
                                                                    MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
                                                                Catch ex As Exception

                                                                End Try
                                                                ''If Convert.ToBoolean(isAct) = True AndAlso MailDt IsNot Nothing Then
                                                                If MailDt IsNot Nothing Then
                                                                    If MailDt.Rows.Count > 0 Then
                                                                        Try
                                                                            Dim strMailMsgHold As String
                                                                            Dim newDepDate As String = ""
                                                                            newDepDate = FltDs.Tables(0).Rows(0)("DepartureDate").ToString()
                                                                            newDepDate = newDepDate.Insert(4, "/")
                                                                            newDepDate = newDepDate.Insert(7, "/")
                                                                            strMailMsgHold = "<table>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><h2>" & bodyHdr & "</h2>"
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><b>Customer ID: </b>" + Session("UID").ToString
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><b>Departure Date: </b>" + Convert.ToDateTime(newDepDate).ToString("dd/MM/yyyy")
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><b>Pnr No: </b>" + GdsPnr
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OBTrackId
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<tr>"
                                                                            strMailMsgHold = strMailMsgHold & "<td><b>Fare: </b>" + FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString()
                                                                            strMailMsgHold = strMailMsgHold & "</td>"
                                                                            strMailMsgHold = strMailMsgHold & "</tr>"
                                                                            strMailMsgHold = strMailMsgHold & "</table>"
                                                                            Try
                                                                                Dim mailRow = If(GdsPnr.Trim().Contains("-FQ"), dsConfigMail.Tables(0).Select("ModuleType='Failed'"), dsConfigMail.Tables(0).Select("ModuleType='Hold'"))
                                                                                If mailRow.Length > 0 Then
                                                                                    For ml As Integer = 0 To mailRow.Length - 1
                                                                                        If Convert.ToBoolean(mailRow(ml)("IsActive").ToString()) Then
                                                                                            objSqlDom.SendMail(mailRow(ml)("ToEmail").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                                        End If
                                                                                    Next
                                                                                End If
                                                                            Catch ex As Exception

                                                                            End Try
                                                                            ''If Convert.ToBoolean(isAct) Then
                                                                            ''    objSqlDom.SendMail(tomail, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, subHdr, "")
                                                                            ''End If
                                                                        Catch ex As Exception

                                                                        End Try

                                                                    End If

                                                                End If

                                                            Catch ex As Exception

                                                            End Try
                                                        End If


                                                    Else
                                                        strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                                    End If
                                                Else
                                                    strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                                End If

                                            Else
                                                strTktCopy = strTktCopy & "<strong style='font-size:14px'>You cann't book ticket becaues of low balance</strong>"
                                            End If
                                            ''Else
                                            ''    Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                                            ''End If
                                        Else
                                            strTktCopy = strTktCopy & "<strong style='font-size:14px'>You cann't book ticket using same booking reference number(" & IBTrackId & ")</strong>"
                                        End If
                                        ''''''
                                    Else
                                        strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                                    End If
                                End If
                            Else
                                strTktCopy = strTktCopy & "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                            End If
                        Else
                            'strTktCopy = "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                            strTktCopy = strTktCopy & "<br/> <strong style='font-size:14px'>Unable to confirm your inbound flight booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
                        End If
                        'Else
                        '    Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                        'End If


                    Else
                        strTktCopy = strTktCopy & "<strong style='font-size:14px'>You cann't book ticket becaues of low balance</strong>"
                    End If


                Else
                    'Dim um1 As String = ""
                    'um1 = objUMSvc.GetMUForPage("International/BookingMsg.aspx")
                    'Response.Redirect(um1 & "?msg=NA")
                    Response.Redirect("../International/BookingMsg.aspx?msg=NA")
                End If
            Else
                strTktCopy = strTktCopy & "<strong style='font-size:14px'>You cann't book ticket using same booking reference number(" & OBTrackId & ")</strong>"
            End If
            ''''''''                           
        Else
            strTktCopy = strTktCopy & "<strong style='font-size:14px'>We are unable to confirm your booking at the moment.<br/> Instead of trying again, please contact our call centre to avoid any inconvenience.</strong>"
        End If
        Return strTktCopy
    End Function
    Public Function GetCabin(ByVal Provider As String, ByVal cabin As String, ByVal VC As String) As String
        Dim cabininfo As String = ""
        Try


            If Provider = "TB" And VC = "G8" Then

            ElseIf Provider = "TB" Then

                cabininfo = "Economy"


            Else

                If cabin.ToUpper().Trim() = "Y" Then

                    cabininfo = "Economy"
                ElseIf cabin.ToUpper().Trim() = "C" Then
                    cabininfo = "Business"
                ElseIf cabin.ToUpper().Trim() = "F" Then
                    cabininfo = "First"
                ElseIf cabin.ToUpper().Trim() = "W" Then
                    cabininfo = "Premium Economy"

                Else

                    cabininfo = cabin

                End If



            End If

        Catch ex As Exception

        End Try
        Return cabininfo
    End Function

    Public Function TicketCopyExportPDF(OrderId As String, TransID As String) As String

        Dim strFileNmPdf As String = ""
        Dim writePDF As Boolean = False
        Dim TktCopy As String = ""
        Dim Gtotal As Integer = 0
        Dim initialAdt As Integer = 0
        Dim initalChld As Integer = 0
        Dim initialift As Integer = 0
        Dim MealBagTotalPrice As Decimal = 0
        Dim AdtTtlFare As Decimal = 0
        Dim ChdTtlFare As Decimal = 0
        Dim INFTtlFare As Decimal = 0
        Dim fare As Decimal = 0
        Dim TBasefare As Decimal = 0
        Dim ABasefare As Decimal = 0
        Dim CBasefare As Decimal = 0
        Dim IBasefare As Decimal = 0

        Dim Tfuel As Decimal = 0
        Dim Afuel As Decimal = 0
        Dim Cfuel As Decimal = 0
        Dim Ifuel As Decimal = 0

        Dim Ttax As Decimal = 0
        Dim Atax As Decimal = 0
        Dim Ctax As Decimal = 0
        Dim Itax As Decimal = 0

        Dim Tgst As Decimal = 0
        Dim Agst As Decimal = 0
        Dim Cgst As Decimal = 0
        Dim Igst As Decimal = 0

        Dim TTransfee As Decimal = 0
        Dim ATransfee As Decimal = 0
        Dim CTransfee As Decimal = 0
        Dim ITransfee As Decimal = 0

        Dim TTranscharge As Decimal = 0
        Dim ATranscharge As Decimal = 0
        Dim CTranscharge As Decimal = 0
        Dim ITranscharge As Decimal = 0

        'Dim OrderId As String = "1c2019deXCP9cVSU"
        'Dim TransID As String = ""


        Dim objTranDom As New SqlTransactionDom()
        Dim SqlTrasaction As New SqlTransaction()
        Dim objSql As New SqlTransactionNew()
        Dim FltPaxList As New DataTable()

        Dim FltDetailsList As New DataTable()
        Dim FltProvider As New DataTable()
        Dim FltBaggage As New DataTable()
        Dim dtagentid As New DataTable()
        Dim FltagentDetail As New DataTable()
        Dim fltTerminal As New DataTable()
        Dim fltFare As New DataTable()
        Dim fltMealAndBag As New DataTable()
        Dim fltMealAndBag1 As New DataTable()
        Dim FltHeaderList As New DataTable()
        Dim fltTerminalDetails As New DataTable()
        Dim SelectedFltDS As New DataSet()

        Dim SeatListO As List(Of STD.Shared.Seat)

        Dim bgdetail As String = ""
        FltPaxList = SelectPaxDetail(OrderId, TransID)
        FltHeaderList = objTktCopyMail.SelectHeaderDetail(OrderId)
        FltDetailsList = objTktCopyMail.SelectFlightDetail(OrderId)
        FltProvider = (objTranDom.GetTicketingProvider(OrderId)).Tables(0)
        dtagentid = objTktCopyMail.SelectAgent(OrderId)
        SelectedFltDS = SqlTrasaction.GetFltDtls(OrderId, dtagentid.Rows(0)("AgentID").ToString())
        Dim Bag As Boolean = False
        If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
            Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
        End If
        FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows(0)("Trip")), Convert.ToString(FltHeaderList.Rows(0)("VC")), Bag)).Tables(0)
        FltagentDetail = objTktCopyMail.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())
        fltFare = objTktCopyMail.SelectFareDetail(OrderId, TransID)
        Dim dt As DateTime = Convert.ToDateTime(Convert.ToString(FltHeaderList.Rows(0)("CreateDate")))
        Dim [date] As String = dt.ToString("dd/MMM/yyyy").Replace("-", "/")

        Dim Createddate As String = [date].Split("/")(0) + " " + [date].Split("/")(1) + " " + [date].Split("/")(2)

        Dim fltmealbag As DataRow() = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0).Select("MealPrice>0 or BaggagePrice>0 ")
        fltMealAndBag1 = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0) '.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        If fltmealbag.Length > 0 Then

            fltMealAndBag = fltMealAndBag1.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        End If
        ''Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        ''SeatListO = IFLT.SeatDetails(OrderId)
        Try
            Dim TicketFormate As String = ""
            If ((Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" OrElse Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirmbyagent")) Then
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>"
                ''TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b> and contact customer care for issuance."
                TicketFormate += "PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>.Functioning team is working on it and may take 15 minutes to resolve. Please contact our customer care representative at <b>+91 00 0000 0000</b> for any further assistance"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
            End If
            'Dim strAirline As String = "SG6EG8"
            Dim dd As String = ""
            Dim AgentAddress As String = ""
            Try
                Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
                If (System.IO.File.Exists(filepath)) Then

                    dd = "http://tripforo.com/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
                    AgentAddress = "<p style='font-weight: bolder; font-size: 20px;height: 8px;margin: 0px 0 20px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email")) + "</p>"
                Else
                    dd = "http://tripforo.com/Advance_CSS/Icons/logo(ft).png"
                    AgentAddress = "<p style='font-weight: bolder; font-size: 20px;height: 8px;margin: 0px 0 20px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "</p><p style='font-size: 13px;height: 8px;margin: 0px 0 13px !important;text-align:right;'>Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email")) + "</p>"
                End If
            Catch ex As Exception
                'clsErrorLog.LogInfo(ex)
                dd = "../Advance_CSS/Icons/logo(ft).png"
            End Try

            If (FltDetailsList.Rows(0)("AirlineCode") = "SG") Then
                BackClor = "#9c1304"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "6E") Then
                BackClor = "#2d3c9a"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "G8") Then
                BackClor = "#172169"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "UK") Then
                BackClor = "#47143d"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "AK") Then
                BackClor = "#e32525"
            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "I5") Then
                BackClor = "#e32525"

            ElseIf (FltDetailsList.Rows(0)("AirlineCode") = "AI") Then
                BackClor = "#e32525"
            Else

                BackClor = "#000"
            End If


            TicketFormate += "<div class='large-12 medium-12 small-12' style='border:1px solid " + BackClor + "'>"
            TicketFormate += "<table style='width: 100%;'>"
            TicketFormate += "<tbody>"
            TicketFormate += "<tr id='trLogoWithAgencySection'>"
            TicketFormate += "<td colspan='4'> <img id='agencylogo' src='" + dd + "' alt='Logo' width='244' height='87'/></td>"
            TicketFormate += "<td style='background-color: white;width:55%' colspan='4'>"
            TicketFormate += AgentAddress
            'TicketFormate += "<img src='/prepod/Images/icons/plane.png' style='width: 40px; position: relative; bottom: -25px; bottom: 56px; bottom: -35px;' />"
            'TicketFormate += "<font face='Arial' style='font-weight: 900; font-size: 30px; position: relative; left: 8px; bottom: -38px;'>E-Ticket</font><font style='position: relative; left: 176px; bottom: 54px; left: 21px; bottom: -36px;'>Reference No." + OrderId + " </font>" ''ttt
            TicketFormate += "</td>"
            'TicketFormate += "</tr>"
            'TicketFormate += "<tr><td class='pri' class='pri' style='background-color:#fff; color: #000; font-size: 12px; font-weight: bold; padding: 5px;' colspan='8'><font face='Arial' style='font-weight: 100;font-size: 25px;position: relative;left: 8px;bottom: 0px;'><img alt='Logo Not Found' style='width: 35px;margin-left: 20px;' src='http://richatravels.in/AirLogo/sm" + FltDetailsList.Rows(0)("AirlineCode") + ".gif'/><span style='font-weight: bold;font-size: 15px;margin-left: 15px;'>" + FltDetailsList.Rows(0)("AirlineName") + "</span></td></tr>"
            'TicketFormate += "<tr>"
            'TicketFormate += "</tr>"
            'TicketFormate += "<tr style='display:none;'><td class='pri' class='pri' style='background-color:" + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='8'><font face='Arial' style='font-weight: 100;font-size: 25px;position: relative;left: 8px;bottom: 0px;'>E-Ticket</font><font style='position: relative; left: 176px; bottom: 54px; left: 21px; bottom: 0px;z-index:1011'>Reference No." + OrderId + "  </font><input type='hidden' id='hdnReferenceNo' value='" + OrderId + "' /></td></tr>"
            'TicketFormate += "<tr>"
            TicketFormate += "</tr>"
            TicketFormate += "<tr>"


            TicketFormate += "<td  colspan='4' style='background-color: white;border: 1px solid #ccc;'>"
            TicketFormate += "<p style='font-size: 15px;height: 8px;'>Booking Date:" + FltHeaderList.Rows(0)("Createdate") + "</p>"
            TicketFormate += "<p style='font-size: 15px;height: 8px;'>Ref No:" + OrderId + "</p>"
            TicketFormate += "<p style='font-size: 15px;height: 8px;'>Status:" + FltHeaderList.Rows(0)("Status") + "</p>"

            TicketFormate += "</td>"
            ' TicketFormate += "<td style='background-color: white;border: 1px solid #ccc;'><div id='barcodeTarget' style='float:right;' class='barcodeTarget'></div><canvas id='canvasTarget' style='width:150px; height:150px;'></canvas> "'
            TicketFormate += "<td colspan='2' style='background-color: white;border: 1px solid #ccc;width: 270px;border-right: 0px solid #fff;'><img alt='Logo Not Found' style='width: 35px;margin-left: 20px;' src='https://tripforo.com/AirLogo/sm" + FltDetailsList.Rows(0)("AirlineCode") + ".gif'/><i style='font-weight: bold;font-size: 15px;margin-left: 15px;margin: 0px 0 0px !important;'>" + FltDetailsList.Rows(0)("AirlineName") + "</i>"
            TicketFormate += "</td>"
            TicketFormate += "<td colspan='2' style='background-color: white;border: 1px solid #ccc;width: 176px;border-left: 0px solid;'>  <p style='font-weight: bold;font-size: 15px;margin: 0px 0 0px !important;text-transform:uppercase;'>" + FltHeaderList.Rows(0)("GdsPnr") + " </p><br /><p style='margin: 0px 0 0px !important;'>Airline PNR</p>"
            TicketFormate += "</td>"
            TicketFormate += "</tr>"
            TicketFormate += "</tbody>"
            TicketFormate += "</table>"


            If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "ticketed") Then
                For f As Integer = 0 To FltDetailsList.Rows.Count - 1
                    TicketFormate += "<table style='font-size: 12px; padding: 0px !important; width: 100%;'>"
                    TicketFormate += " <tbody>"
                    TicketFormate += "<tr><td class='pri' style='background-color:" + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;width: 40%;text-transform:uppercase;' colspan='4'>Airline Pnr :  " + FltHeaderList.Rows(0)("GdsPnr") + "/" + FltHeaderList.Rows(0)("AirlinePnr") + "</td><td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;text-align: end;width: 60%' colspan='4'>Confirmed Booked On:" + FltHeaderList.Rows(0)("Createdate") + "</td></tr>"
                    TicketFormate += " <tr>"
                    TicketFormate += "<td colspan='8' style='font-size: 12px; padding: 5px; width: 100%'>"
                    TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%;'>"
                    TicketFormate += " <tbody>"
                    TicketFormate += " <tr>"

                    'Try
                    '    Dim dtbaggage As New DataTable
                    '    dtbaggage = objTranDom.GetBaggageInformation("D", FltHeaderList.Rows(0)("VC"), Bag).Tables(0)
                    '    Dim bginfo As String = GetBagInfo(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AirlineRemark")))
                    '    If bginfo = "" Then

                    '        For Each drbg In dtbaggage.Rows

                    '            bgdetail += drbg("BaggageName") & "|" & drbg("Weight")
                    '        Next
                    '    Else
                    '        bgdetail = bginfo

                    '    End If


                    'Catch ex As Exception

                    'End Try
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    'Response.Write(strDepdt)

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime")).Replace(":", "")
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; vertical-align: top;'><img alt='Logo Not Found' style='width: 50px;' src='http://tripforo.com/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif'/><br />" + FltDetailsList.Rows(f)("AirlineName") + "<br />" + FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber") + "<br/>" + bgdetail + "</td>"
                    'TicketFormate += "<td colspan='2' width='100'></td><td colspan='2' width='100'></td><td colspan='2' width='100'></td>"
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; padding: 2px; text-align: right;'>"
                    TicketFormate += "<p style='font-size: 16px;font-weight:600;text-align:right;'>" + FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")" + " " + strdeptime + "</p>" ''tttt

                    TicketFormate += "<br />" + strDepdt + "<br />" ''tttt
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime")).Replace(":", "")
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    Dim totdur As String = ""
                    Dim stops As String = ""
                    Try
                        totdur = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Tot_Dur"))
                        stops = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Stops"))
                    Catch ex As Exception
                        totdur = Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Duration"))
                        stops = Convert.ToString(FltDetailsList.Rows.Count)

                    End Try
                    TicketFormate += "<td colspan='2' style='text-align: center;'><i style='font-weight: 600'>" + totdur + "</i><br />" ''tttt
                    TicketFormate += "------------------<br />"
                    TicketFormate += "" + stops + "<br />" '''tttt
                    TicketFormate += "" + Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtFareType")) + "</td>" '''ttt
                    TicketFormate += "<td colspan='2' style='font-size: 12px; text-align: left; padding: 2px;'>"
                    TicketFormate += "<p style='font-size: 16px; font-weight: 600;'>" + strArrtime + " " + FltDetailsList.Rows(f)("ArrAirName").ToString().Trim() + " (" + FltDetailsList.Rows(f)("ATo").ToString().Trim() + ")</p>" ''ttt



                    TicketFormate += "<br />" + strArvdt + "<br />" ''''tttttt

                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If
                    TicketFormate += "</td>" '''tttt
                    TicketFormate += "</tr>"
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += " </tbody>"
                    TicketFormate += "</table>"
                Next

                '''''''''''''''''end''''''''''''''''''''''
                ''''''''''''''''''''Passenger &amp; Ticket Information'''''''''''''''''''''''''''

                TicketFormate += " <table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='6'>Passenger &amp; Ticket Information</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='6' style='font-size: 12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000'>"
                TicketFormate += "<tbody style='border: 1px solid #000;'>"
                TicketFormate += "<tr style='border: 1px solid #000;'>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Passenger Information</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Sector</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Ticket No</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Seat</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal</th>"
                ''TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Class</th>"
                ''TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px;'>Status</th>"
                TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px;'>Extra</th>"
                TicketFormate += "</tr>"

                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;'>" + FltPaxList.Rows(p)("Name") + "</td>" ''ttt
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-collapse:collapse !important; padding: 0px 0px !important; margin:0 !important; text-indent:10px;'>"
                    TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000;margin-bottom: 0px;'>" ''ttt
                    TicketFormate += "<tbody>"
                    For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                        TicketFormate += "<tr  >"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px;'>" + FltDetailsList.Rows(f)("DFrom") + "-" + FltDetailsList.Rows(f)("ATo") + "</td>" ''ttt
                        TicketFormate += "</tr>"



                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>" ''ttt
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;text-transform:uppercase;'>" + Convert.ToString(FltPaxList.Rows(p)("TicketNumber")) + "</td>" ''ttt
                    If (Not SeatListO Is Nothing) Then


                        For i As Integer = 0 To SeatListO.Count - 1
                            Dim dts As DataTable = New DataTable()
                            dts = SelectPaxDetail(OrderId, SeatListO(i).PaxId)
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" + SeatListO(i).SeatDesignator + "</td>" ''Seat Number
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" & dts.Rows(0)("MealType") & "</td>" ''ttt meal
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" & SeatListO(i).Amount & "</td>" ''ttt amount
                        Next
                    Else
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                        TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>None</td>"
                    End If



                    ''TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>" + GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier"))) + "</td>" ''ttt
                    ''TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px;'>CONFIRMED</td>" ''ttt
                    ''ttt
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                ''''''''''''''''''''End''''''''''''''
                ''''''''''''''''''''Fare Breakup''''''''''
                If TransID = "" OrElse TransID Is Nothing Then
                    TicketFormate += "<table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                    TicketFormate += "<tbody>"
                    TicketFormate += "<tr class='pri2'>"
                    TicketFormate += "<td colspan='6' style= 'background-color:#fff;width:100%;border: 1px solid #000;'>"
                    TicketFormate += "<table style='width:100%;' id='fareinfo'>"
                    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td class='pri' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Fare Information</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Type</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Count</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Base fare</th>"
                    TicketFormate += "<th class='pri2' style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Fuel Surcharge</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Tax</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>STax</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trans Fee</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trans Charge</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Total</th>"
                    TicketFormate += "</tr>"
                    For fd As Integer = 0 To fltFare.Rows.Count - 1

                        If fltFare.Rows(fd)("PaxType").ToString() = "ADT" AndAlso initialAdt = 0 Then
                            Dim numberOfADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;' id='td_adtcnt'>" & numberOfADT & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT).ToString
                            ABasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT).ToString
                            Afuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'id='td_taxadt'>"
                            'TicketFormate += ((Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT)).ToString
                            'TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT).ToString
                            Atax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT) + (Convert.ToDecimal(fltFare.Rows(fd)("ticketcopymarkupforTAX")) * numberOfADT)
                            Atax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT)
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT).ToString
                            Agst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT).ToString
                            TTransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'id='td_tcadt'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT).ToString
                            ATranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;' id='td_adttot'>"
                            AdtTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfADT).ToString
                            TicketFormate += AdtTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initialAdt += 1
                        End If

                        If fltFare.Rows(fd)("PaxType").ToString() = "CHD" AndAlso initalChld = 0 Then
                            Dim numberOfCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;' id='td_chdcnt'>" & numberOfCHD & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD).ToString
                            CBasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD).ToString
                            Cfuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_taxchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfCHD).ToString
                            Ctax = Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD).ToString
                            Cgst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD).ToString
                            CTransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_tcchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD).ToString
                            CTranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_chdtot'>"
                            ChdTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfCHD).ToString
                            TicketFormate += ChdTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initalChld += 1
                        End If
                        If fltFare.Rows(fd)("PaxType").ToString() = "INF" AndAlso initialift = 0 Then
                            Dim numberOfINF As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "INF").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;' id='td_infcnt'>" & numberOfINF & "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF).ToString
                            IBasefare = Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF).ToString
                            Ifuel = Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            Itax = (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF).ToString
                            Igst = Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF).ToString
                            ITransfee = Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF).ToString
                            ITranscharge = Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;border-top: 1px solid #000;background: #fff;'id='td_Inftot'>"
                            INFTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfINF).ToString
                            TicketFormate += INFTtlFare.ToString
                            TicketFormate += "</td>"
                            TicketFormate += "</tr>"
                            initialift += 1

                        End If
                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    'Remark--Commented line
                    Dim ticketcopymarkup As Decimal = 0

                    If (fltFare.Rows.Count > 0) Then

                        If (Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTax")) <> "0") Then
                            ticketcopymarkup = Convert.ToDecimal(fltFare.Rows(0)("ticketcopymarkupforTax"))
                        End If
                        If (Convert.ToString(fltFare.Rows(0)("ticketcopymarkupforTC")) <> "0") Then
                            ticketcopymarkup = Convert.ToDecimal(fltFare.Rows(0)("ticketcopymarkupforTC"))
                        End If
                    End If
                    Dim numADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                    Dim numCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                    Dim tpaxcount As Integer = numADT + numCHD
                    ticketcopymarkup = ticketcopymarkup * tpaxcount
                    TBasefare = ABasefare + CBasefare + IBasefare
                    TTransfee = ATransfee + CTransfee + ITransfee
                    TTranscharge = ATranscharge + CTranscharge + ITranscharge
                    Tfuel = Afuel + Cfuel + Ifuel
                    Ttax = Atax + Ctax + Itax + TTransfee + TTranscharge
                    Tgst = Agst + Cgst + Igst

                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare
                    basetaxfarenrm.Value = Atax + Ctax + Itax + TTransfee + TTranscharge
                Else
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='2' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top; display:none;'id='td_perpaxtype'>" + FltPaxList.Rows(0)("PaxType") + "</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Base Fare</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("BaseFare").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("Fuel").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Tax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtax'>"
                    TicketFormate += fltFare.Rows(0)("Tax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>STax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("ServiceTax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr >"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("TFee").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'id='td_perpaxtc'>"
                    TicketFormate += fltFare.Rows(0)("TCharge").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"

                    Dim ResuCharge As Decimal = 0
                    Dim ResuServiseCharge As Decimal = 0
                    Dim ResuFareDiff As Decimal = 0
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuCharge").ToString
                        ResuCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Srv. Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuServiseCharge").ToString
                        ResuServiseCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuServiseCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Fare Diff</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuFareDiff").ToString
                        ResuFareDiff = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuFareDiff"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;'>TOTAL</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;' id='td_totalfare'>"
                    fare = (Convert.ToDecimal(fltFare.Rows(0)("BaseFare")) + Convert.ToDecimal(fltFare.Rows(0)("Fuel")) + Convert.ToDecimal(fltFare.Rows(0)("Tax")) + Convert.ToDecimal(fltFare.Rows(0)("ServiceTax")) + Convert.ToDecimal(fltFare.Rows(0)("TCharge")) + Convert.ToDecimal(fltFare.Rows(0)("TFee")) + ResuCharge + ResuServiseCharge + ResuFareDiff).ToString
                    TicketFormate += fare.ToString
                    TicketFormate += "</td>"

                    'fare = Convert.ToDecimal(fltFare.Rows[0]["Total"]) + ResuCharge + ResuServiseCharge + ResuFareDiff;
                    TicketFormate += "</tr>"
                End If
                ''''''''''''''''''''End'''''''''''''''''''

                ''''''''''''''''''''Meal & Baggege'''''''''''
                If fltMealAndBag.Rows.Count > 0 Then
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Meal & Baggage Information</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr style='border: 1px solid #000;'>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Pax Name</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Trip Type</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal Code</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Meal Price</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Baggage Code</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Baggage Price</th>"
                    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Total</th>"
                    TicketFormate += "</tr>"



                    For i As Integer = 0 To fltMealAndBag.Rows.Count - 1
                        'If Convert.ToString(fltMealAndBag.Rows(i)("MealPrice")) <> "0.00" AndAlso Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice")) <> "0.00" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("Name"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TripType"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealPrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggageCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        MealBagTotalPrice += Convert.ToDecimal(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += "</td>"

                        TicketFormate += "</tr>"
                        'End If
                    Next
                    TicketFormate += "</tbody>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare + MealBagTotalPrice
                End If

                '''''''''''''''''''''End'''''''''''''''''''''
                '''''''''''''''''''Seat Details''''''''''''''
                Dim seatdetails As String = ""
                Dim seatFareO As Integer = 0
                'If SeatListO.Count > 0 Then
                '    TicketFormate += "<tr>"
                '    TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>"
                '    TicketFormate += "<table style='width:100%;'>"
                '    TicketFormate += "<tbody style='border: 1px solid #000;'>"
                '    TicketFormate += "<tr>"
                '    TicketFormate += "<td style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;' colspan='9'>Traveller Seat Information:</td>"
                '    TicketFormate += "</tr>"

                '    TicketFormate += "<tr style='border: 1px solid #000;'>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Traveller</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Sector</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Seat</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Type</th>"
                '    TicketFormate += "<th style='font-size: 12px; text-align: left; padding: 5px; border-right: 1px solid #000;'>Amount</th>"
                '    TicketFormate += "</tr>"

                '    For i As Integer = 0 To SeatListO.Count - 1
                '        Dim dts As DataTable = New DataTable()
                '        dts = SelectPaxDetail(OrderId, SeatListO(i).PaxId)
                '        TicketFormate += "<tr>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & dts.Rows(0)("Name") & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).Origin & "-" & SeatListO(i).Destination & "</div>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatDesignator & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatAlignment & "</td>"
                '        TicketFormate &= "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>" & SeatListO(i).Amount & "</td>"
                '        seatFareO = seatFareO + Convert.ToInt32(SeatListO(i).Amount)
                '        TicketFormate += "</tr>"
                '    Next
                '    fare = AdtTtlFare + ChdTtlFare + INFTtlFare + MealBagTotalPrice + seatFareO
                '    TicketFormate += "</tbody>"
                '    TicketFormate += "</table>"
                '    TicketFormate += "</td>"
                '    TicketFormate += "</tr>"
                'End If

                '''''''''''''''''''''End''''''''''''''''''''''
                '''''''''''''''Fare Information'''''''''''''''''''
                TicketFormate += "<table id='disfareinfoheader'  style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;' >"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr id='TR_FareInformation1'>"
                TicketFormate += "<td class='pri' colspan='9' style='background-color: " + BackClor + "; color: #fff; font-size: 12px; font-weight: bold; padding: 5px;'>Redemption Details</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "<table id='disfareinfo' style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='9' style='font-size: 12px; padding: 5px; width: 30%'>"
                TicketFormate += "<table style='font-size: 12px; padding: 5px; width: 100%; border: 1px solid #000'>"
                TicketFormate += "<tbody style='border: 1px solid #000;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Base fare</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;'>" + TBasefare.ToString() + "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Fuel Surcharge</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;'>" + Tfuel.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Tax</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;' id='td_taxadt' class='taxclass'>" + Ttax.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Supp.SGST</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;'>" + Tgst.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Supp.CGST</th>"
                TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;'>0</td>" ''ttt
                TicketFormate += "</tr>"
                'TicketFormate += "<tr style='display:none' id='trtransfee'>"
                'TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Trans Fee</th>"
                'TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;' id='lbltransfee'>" + TTransfee.ToString() + "</td>" ''ttt
                'TicketFormate += "</tr>"
                'TicketFormate += "<tr style='display:none'>"
                'TicketFormate += "<th style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Trans Charge</th>"
                'TicketFormate += "<td style='font-size: 12px; text-align: left; vertical-align: top;' id='td_allcharge'>" + TTranscharge.ToString() + "</td>" ''ttt
                'TicketFormate += "</tr>"
                TicketFormate += "<tr style='border: 1px solid #000;'>"
                TicketFormate += "<th class='pri2' style='font-size: 12px; color: #424242; text-align: left; padding: 5px; font-weight: bold;'>Grand Total</th>"
                TicketFormate += "<td class='pri2' style='font-size: 12px; text-align: left; vertical-align: top;' id='td_grandtot'>" + fare.ToString() + "</td>" ''ttt
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='4' style='font-size: 12px; padding: 5px; width: 100%'>"
                TicketFormate += " <table style='width:50%;margin-top: -82px;float: right;border: 1px solid #000;'>"
                TicketFormate += " <tbody style='border: 1px solid #000;'>"
                TicketFormate += " <tr style='border: 1px solid #000;'><td style='font-weight: bold;font-size: 12px;'>Passenger Information:</td><td style=''></td> </tr>"
                TicketFormate += " <tr><td class='pri2' style='font-weight: bold;font-size: 12px;'>Email:</td><td class='pri2' style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgEmail") + "</td></tr>"
                TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Contact No:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgMobile") + "</td></tr>"

                TicketFormate += " </tbody>"
                TicketFormate += " </table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr >"
                TicketFormate += " </tbody>"
                TicketFormate += " </table>"


                'TicketFormate += "<table id='hdpaxinfo' style='display:none;border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;' >"
                'TicketFormate += "<tbody>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 12px; padding: 5px; width: 100%'>"
                'TicketFormate += " <table style='float:right'>"
                'TicketFormate += " <tbody style='border: 1px solid #000;'>"
                'TicketFormate += " <tr style='border: 1px solid #000;'><td style='font-weight: bold;font-size: 12px;'>Passenger Information:</td><td style=''></td> </tr>"
                'TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Email:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgEmail") + "</td></tr>"
                'TicketFormate += " <tr><td style='font-weight: bold;font-size: 12px;'>Contact No:</td><td style='font-weight: bold;font-size: 12px;'>" + FltHeaderList.Rows(0)("PgMobile") + "</td></tr>"
                'TicketFormate += " </tbody>"
                'TicketFormate += " </table>"
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"
                'TicketFormate += " </tbody>"
                'TicketFormate += " </table>"
                ''''''''''''''''''''''''''''End'''''''''''''''''''''''''''''''

                '''''''''''''''''''''''''Terms & Conditions'''''''''''''''''''''''
                TicketFormate += "<table style='border: 1px solid #0b2759; font-size: 12px; padding: 0px !important; width: 100%;'>"
                TicketFormate += "<tbody>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td class='pri' colspan='8' style='background-color: " + BackClor + "; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'>IMPORTANT INFORMATION :</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='8'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>For any queries or communication with tripforo regarding this booking, please use the Booking ID as a reference.</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>Please note that for all domestic flights, check-in counters close 60 minutes prior to flight departure.</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>It is mandatory for the passenger to carry a valid photo ID proof in order to enter the airport and show at the time of check-in. Permissible ID proofs include - Aadhaar Card, Passport or any other government recognized ID proof. For infant travellers (0-2 yrs), it is mandatory to carry the birth certificate as a proof</li>"
                TicketFormate += "<li style='font-size: 10.5px;margin-left: 15px !important;list-style: disc !important;'>Kindly carry a copy of your e-ticket on a tablet/ mobile/ laptop or a printed copy of the ticket to enter the airport and show at the time of check-in.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Flight schedules are subject to change and approval by authorities.<br>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.<br>"
                'TicketFormate += "Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Cancellation amount will be charged as per airline rule.</li>"
                'TicketFormate += "<li style='font-size: 10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>"
                TicketFormate += "</ul>"
                TicketFormate += " </td>"
                TicketFormate += "</tr>"
                TicketFormate += "</tbody>"
                TicketFormate += "</table>"
                TicketFormate += "</div>"



                'TicketFormate += " <script type='text/javascript'>"
                'TicketFormate += " decodeURI(window.location.search).substring(1).split('&');"
                'TicketFormate += "  var btype = 'code128';"
                'TicketFormate += "  var renderer = 'css';"
                'TicketFormate += "  var quietZone = false;"
                'TicketFormate += " if ($('#quietzone').is(':checked') || $('#quietzone').attr('checked'))"
                'TicketFormate += "  { quietZone = true; }"
                'TicketFormate += "  var settings = { output: renderer, bgColor: '#FFFFFF', color: '#000000', barWidth: '1', barHeight: '110', moduleSize: '5', posX: '10', posY: '20', addQuietZone: '1' };"
                'TicketFormate += "   $('#canvasTarget').hide();"
                'TicketFormate += "  $('#barcodeTarget').html('').show().barcode('" + OrderId + "', btype, settings);"
                'TicketFormate += "  </script>"

            End If

            Return TicketFormate
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''change
    End Function
    Public Function TicketCopyExportPDFold(OrderId As String, TransID As String) As String

        Dim strFileNmPdf As String = ""
        Dim writePDF As Boolean = False
        Dim TktCopy As String = ""
        Dim Gtotal As Integer = 0
        Dim initialAdt As Integer = 0
        Dim initalChld As Integer = 0
        Dim initialift As Integer = 0
        Dim MealBagTotalPrice As Decimal = 0
        Dim AdtTtlFare As Decimal = 0
        Dim ChdTtlFare As Decimal = 0
        Dim INFTtlFare As Decimal = 0
        Dim fare As Decimal = 0

        'Dim OrderId As String = "1c2019deXCP9cVSU"
        'Dim TransID As String = ""


        Dim objTranDom As New SqlTransactionDom()
        Dim SqlTrasaction As New SqlTransaction()
        Dim objSql As New SqlTransactionNew()
        Dim FltPaxList As New DataTable()

        Dim FltDetailsList As New DataTable()
        Dim FltProvider As New DataTable()
        Dim FltBaggage As New DataTable()
        Dim dtagentid As New DataTable()
        Dim FltagentDetail As New DataTable()
        Dim fltTerminal As New DataTable()
        Dim fltFare As New DataTable()
        Dim fltMealAndBag As New DataTable()
        Dim fltMealAndBag1 As New DataTable()
        Dim FltHeaderList As New DataTable()
        Dim fltTerminalDetails As New DataTable()
        Dim SelectedFltDS As New DataSet()

        Dim SeatListO As List(Of STD.Shared.Seat)

        FltPaxList = SelectPaxDetail(OrderId, TransID)
        FltHeaderList = objTktCopyMail.SelectHeaderDetail(OrderId)
        FltDetailsList = objTktCopyMail.SelectFlightDetail(OrderId)
        FltProvider = (objTranDom.GetTicketingProvider(OrderId)).Tables(0)
        dtagentid = objTktCopyMail.SelectAgent(OrderId)
        SelectedFltDS = SqlTrasaction.GetFltDtls(OrderId, dtagentid.Rows(0)("AgentID").ToString())
        Dim Bag As Boolean = False
        If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
            Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
        End If
        FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows(0)("Trip")), Convert.ToString(FltHeaderList.Rows(0)("VC")), Bag)).Tables(0)
        FltagentDetail = objTktCopyMail.SelectAgencyDetail(dtagentid.Rows(0)("AgentID").ToString())
        fltFare = objTktCopyMail.SelectFareDetail(OrderId, TransID)
        Dim dt As DateTime = Convert.ToDateTime(Convert.ToString(FltHeaderList.Rows(0)("CreateDate")))
        Dim [date] As String = dt.ToString("dd/MMM/yyyy").Replace("-", "/")

        Dim Createddate As String = [date].Split("/")(0) + " " + [date].Split("/")(1) + " " + [date].Split("/")(2)

        Dim fltmealbag As DataRow() = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0).Select("MealPrice>0 or BaggagePrice>0 ")
        fltMealAndBag1 = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables(0) '.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        If fltmealbag.Length > 0 Then

            fltMealAndBag = fltMealAndBag1.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()
        End If
        Dim IFLT As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        SeatListO = IFLT.SeatDetails(OrderId)
        Try
            'Dim strAirline As String = "SG6EG8"

            Dim TicketFormate As String = ""


            If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" And Session("UserType") = "TA") Then

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>"
                TicketFormate += "<b>" & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>"
                If FltHeaderList.Rows(0)("GdsPnr").Contains("-FQ") And Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm" Then
                    TicketFormate += "Your booking is <b>Pending</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b>+ 91-11-47 677 777</b> for any further assistance"
                Else
                    TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b> +91-11-47 677 777  </b> for any further assistance"
                End If

                'TicketFormate += "The PNR-<b>" & FltHeaderList.Rows(0)("GdsPnr") & " </b>is on <b>HOLD</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b> +91-11-47 677 777  </b> for any further assistance"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>" ''Devesh
                TicketFormate += "<td>" ''Devesh
                TicketFormate += "<table style='border: 1px solid #0b2759; font-family: Verdana, Geneva, sans-serif; font-size: 12px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>"
                TicketFormate += "Passenger Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table>"

                TicketFormate += "<tr>"
                'TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
                ' TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                'TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                ' TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                ' TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
                ' TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                ' TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                ' TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                'TicketFormate += "<tr>"
                ''TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
                ''TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                ''TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows(0)("Status"))
                ''TicketFormate += "</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                'TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                'TicketFormate += Createddate
                'TicketFormate += "</td>"

                'TicketFormate += "</tr>"


                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"
                TicketFormate += "</tr>"



                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("TicketNumber")
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #fff; width: 100%; padding: 5px;' colspan='4'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; color: #fff; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                'TicketFormate += "<tr>"
                'TicketFormate += "<td colspan='4' style='height:1px;'>&nbsp;</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='5' style='background-color: #0b2759;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Arrive Airport/Terminal</td>"
                TicketFormate += "</tr>"

                For f As Integer = 0 To FltDetailsList.Rows.Count - 1

                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='5' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"

                    TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='http://tripforo.com/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif' ></img>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")

                    'Response.Write(strDepdt)

                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime")).Replace(":", "")
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime")).Replace(":", "")
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"

                    TicketFormate += "<br />"
                    TicketFormate += "<br />"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br />"
                    TicketFormate += "<br />"
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If

                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='4' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif' ></img>"
                    TicketFormate += "<br/>"

                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:11px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"

                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>" ''Add new tr close Devesh
                TicketFormate += "</table>"
                TicketFormate += "</td>" ''Add new tr td Devesh
                TicketFormate += "</tr>" ''Add new tr close Devesh
                TicketFormate += "</table>" ''Add new table close Devesh
            ElseIf (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "rejected" And Session("UserType") = "TA") Then

                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:left;font-size:15px;'>"
                TicketFormate += "<b>Booking Reference No. " & OrderId & "</b>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align:left;font-size:14px;'>"
                TicketFormate += "Please re-try the booking.Your booking has been rejected due to some technical issue at airline end."
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"

            Else


                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:50%;text-align:left;'>"
                'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:104px' />"
                'TicketFormate += "</td>"

                Dim dd As String = ""
                Dim AgentAddress As String = ""
                Try
                    Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
                    If (System.IO.File.Exists(filepath)) Then
                        dd = "http://tripforo.com/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg"
                        'dd = ResolveClientUrl("~/AgentLogo/" + Convert.ToString(dtagentid.Rows(0)("AgentID")) + ".jpg")
                        AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br />" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br />" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br />Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br />Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                    Else
                        dd = "/Custom_Design/images/logo_new.png"
                        'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:70px; width:110px' />"
                        AgentAddress = Convert.ToString(FltagentDetail.Rows(0)("Agency_Name")) + "<br />" + Convert.ToString(FltagentDetail.Rows(0)("Address")) + "<br />" + Convert.ToString(FltagentDetail.Rows(0)("Address1")) + "<br />Mobile:" + Convert.ToString(FltagentDetail.Rows(0)("Mobile")) + "<br />Email:" + Convert.ToString(FltagentDetail.Rows(0)("Email"))
                    End If
                Catch ex As Exception
                    'clsErrorLog.LogInfo(ex)
                    'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:110px' />"
                    ''dd = "https://RWT.co/images/logo.png"
                End Try


                TicketFormate += "<img src='" + dd + "' alt='Logo' style='height:70px; width:150px' />"
                'TicketFormate += "<img src='https://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:110px' />"
                TicketFormate += "</td>"
                TicketFormate += "<td style='text-align:right;width:50%;font-size:12px;font-weight:bold;'>"
                'TicketFormate += "<span style='font-size:16px;font-weight:bold;'>Electronic Ticket</span><br />"
                TicketFormate += "<div style='font-size:12px;font-weight:bold;'>Electronic Ticket</div><br/>"
                If String.IsNullOrEmpty(AgentAddress) Then
                    AgentAddress = ""
                Else
                    TicketFormate += "<br />"
                    TicketFormate += "<div style='color:#424242;font-size:12px;font-weight:bold;'>" + AgentAddress + "</div><br />"
                    ''TicketFormate += AgentAddress + "<br />"
                End If


                TicketFormate += "</td>"

                TicketFormate += "<td style='width: 50%;text-align:right;display:none;'>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:50%;text-align:left;'>"
                TicketFormate += ""
                TicketFormate += "</td>"
                TicketFormate += "<td style='width: 50%;text-align:right;'>"
                TicketFormate += ""
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width:100%;height:10px;'></td>"
                TicketFormate += "</tr>"
                'TicketFormate += "<tr>"
                'TicketFormate += "<td colspan='2' style='vertical-align:bottom;color:#f58220;text-align:right;width:100%;font-size:16px;font-weight:bold;'>"
                'TicketFormate += "Electronic Ticket"
                'TicketFormate += "</td>"
                'TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='height: 2px; width: 100%; border: 1px solid #0b2759'></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"


                TicketFormate += "<table style='width: 100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='width: 100%; text-align: justify; color: #0b2759; font-size: 11px; padding: 10px;'>"
                TicketFormate += "This is travel itinerary and E-ticket receipt. You may need to show this receipt to enter the airport and/or to show return or onward travel to "
                TicketFormate += "customs and immigration officials."
                TicketFormate += "<br />"

                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "<table style='border: 1px solid #0b2759; font-size: 12px;padding:0px !important;width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left;  background-color: #0b2759; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>"
                TicketFormate += "Passenger & Ticket Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>"
                TicketFormate += "<table>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("GdsPnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AgencyName")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += FltHeaderList.Rows(0)("AirlinePnr")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
                TicketFormate += FltagentDetail.Rows(0)("Mobile")
                TicketFormate += "<br/>"
                TicketFormate += FltagentDetail.Rows(0)("Email")
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
                TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"

                If (Convert.ToString(FltHeaderList.Rows(0)("Status")).ToLower().Trim() = "ticketed") Then
                    TicketFormate += "Confirmed"
                Else
                    TicketFormate += FltHeaderList.Rows(0)("Status")

                End If


                ''TicketFormate += FltHeaderList.Rows(0)("Status")
                TicketFormate += "</td>"
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>"
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                TicketFormate += Createddate
                TicketFormate += "</td>"
                TicketFormate += "</tr>"


                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>Class</td>"
                TicketFormate += "<td style='font-size: 12px; width: 25%; text-align: left; padding: 5px;'>"
                TicketFormate += GetCabin(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AdtCabin")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("ValiDatingCarrier")))
                TicketFormate += "</td>"
                TicketFormate += "</tr>"



                For p As Integer = 0 To FltPaxList.Rows.Count - 1
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
                    TicketFormate += FltPaxList.Rows(p)("Name") + " " + "(" + FltPaxList.Rows(p)("PaxType") + ")"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                Next

                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #fff; width: 100%; padding: 5px;' colspan='4'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='text-align: left; color: #fff; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>"
                TicketFormate += "Flight Information"
                TicketFormate += "</td>"
                TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                'TicketFormate += "<tr>"
                'TicketFormate += "<td colspan='4' style='height:1px;'>&nbsp;</td>"
                'TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='5' style='background-color: #0b2759;width:100%;'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Flight</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Depart</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>Arrive</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Depart Airport/Terminal</td>"
                TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>Arrive Airport/Terminal</td>"
                TicketFormate += "</tr>"

                For f As Integer = 0 To FltDetailsList.Rows.Count - 1


                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='5' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"


                    TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineCode") + " " + FltDetailsList.Rows(f)("FltNumber")
                    TicketFormate += "<br/>"
                    TicketFormate += FltDetailsList.Rows(f)("AirlineName")   '' chge mk
                    TicketFormate += "<br/>"
                    TicketFormate += "<img alt='Logo Not Found' src='http://tripforo.com/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif' ></img>"
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strDepdt As String = Convert.ToString(FltDetailsList.Rows(f)("DepDate"))
                    'strDepdt = strDepdt.Substring(0, 2) + "-" + strDepdt.Substring(2, 2) + "-" + strDepdt.Substring(4, 2)
                    strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                    Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                    strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                    strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                    Dim strdeptime As String = Convert.ToString(FltDetailsList.Rows(f)("DepTime"))
                    Try
                        If strdeptime.Length > 4 Then
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(3, 2)
                        Else
                            strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strDepdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strdeptime
                    TicketFormate += "</td>"

                    TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>"
                    Dim strArvdt As String = Convert.ToString(FltDetailsList.Rows(f)("ArrDate"))
                    'strArvdt = strArvdt.Substring(0, 2) + "-" + strArvdt.Substring(2, 2) + "-" + strArvdt.Substring(4, 2)
                    strArvdt = IIf(strArvdt.Length = 8, STD.BAL.Utility.Left(strArvdt, 4) & "-" & STD.BAL.Utility.Mid(strArvdt, 4, 2) & "-" & STD.BAL.Utility.Right(strArvdt, 2), "20" & STD.BAL.Utility.Right(strArvdt, 2) & "-" & STD.BAL.Utility.Mid(strArvdt, 2, 2) & "-" & STD.BAL.Utility.Left(strArvdt, 2))
                    Dim Arrdt As DateTime = Convert.ToDateTime(strArvdt)
                    strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/")
                    Dim ArrDay As String = Convert.ToString(Arrdt.DayOfWeek)
                    strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2)
                    Dim strArrtime As String = Convert.ToString(FltDetailsList.Rows(f)("ArrTime"))
                    Try
                        If strArrtime.Length > 4 Then
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(3, 2)
                        Else
                            strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                    TicketFormate += strArvdt
                    TicketFormate += "<br/>"
                    TicketFormate += "<br/>"
                    TicketFormate += strArrtime
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>"
                    TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")"

                    TicketFormate += "<br />"
                    TicketFormate += "<br />"
                    fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows(f)("DFrom"), "")
                    'if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[0]["DepartureTerminal"])))
                    '    TicketFormate += "Terminal:" + fltTerminal.Rows[0]["DepartureTerminal"];
                    'else
                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("DepartureTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("DepAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("DepartureTerminal")
                    End If
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>"
                    TicketFormate += FltDetailsList.Rows(f)("ArrAirName") + " (" + FltDetailsList.Rows(f)("ATo") + ")"
                    TicketFormate += "<br />"
                    TicketFormate += "<br />"
                    'if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[f]["ArrivalTerminal"])))
                    '    TicketFormate += "Terminal:" + fltTerminal.Rows[f]["ArrivalTerminal"];
                    'else
                    fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows(f)("ATo"))

                    If String.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows(0)("ArrivalTerminal"))) Then
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml: NA"
                    Else
                        TicketFormate += fltTerminalDetails.Rows(0)("ArrvlAirportName") + " - Trml:" + fltTerminalDetails.Rows(0)("ArrivalTerminal")
                    End If
                    TicketFormate += "</td>"

                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='4' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>"
                    'TicketFormate += "<img alt='Logo Not Found' src='https://RWT.co/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode") + ".gif' ></img>"
                    TicketFormate += "<br/>"
                    'TicketFormate += FltDetailsList.Rows(f)("AirlineName")
                    TicketFormate += "</td>"
                    TicketFormate += "<td style='width: 32%;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size:11px;text-align:left;'></td>"
                    TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>"
                    TicketFormate += "</tr>"

                Next
                TicketFormate += "</table>"
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style=' background-color: #0b2759; color: #fff;font-size:11px;font-weight:bold; padding: 5px;'>"
                TicketFormate += "Fare Information"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                If TransID = "" OrElse TransID Is Nothing Then
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Type</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Count</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Base fare</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Tax</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>STax</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Total</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='8' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    For fd As Integer = 0 To fltFare.Rows.Count - 1

                        If fltFare.Rows(fd)("PaxType").ToString() = "ADT" AndAlso initialAdt = 0 Then
                            Dim numberOfADT As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "ADT").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_adtcnt'>" & numberOfADT & "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'id='td_taxadt'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_tcadt'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfADT).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;' id='td_adttot'>"
                            AdtTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfADT).ToString
                            TicketFormate += AdtTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initialAdt += 1
                        End If

                        If fltFare.Rows(fd)("PaxType").ToString() = "CHD" AndAlso initalChld = 0 Then
                            Dim numberOfCHD As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "CHD").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_chdcnt'>" & numberOfCHD & "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'id='td_taxchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_tcchd'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfCHD).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_chdtot'>"
                            ChdTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfCHD).ToString
                            TicketFormate += ChdTtlFare.ToString
                            TicketFormate += "</td>"

                            TicketFormate += "</tr>"

                            initalChld += 1
                        End If
                        If fltFare.Rows(fd)("PaxType").ToString() = "INF" AndAlso initialift = 0 Then
                            Dim numberOfINF As Integer = FltPaxList.AsEnumerable().Where(Function(x) x("PaxType").ToString() = "INF").ToList().Count
                            TicketFormate += "<tr>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += fltFare.Rows(fd)("PaxType")
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_infcnt'>" & numberOfINF & "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("BaseFare")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Fuel")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("Tax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("ServiceTax")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TFee")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                            TicketFormate += (Convert.ToDecimal(fltFare.Rows(fd)("TCharge")) * numberOfINF).ToString
                            TicketFormate += "</td>"
                            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_Inftot'>"
                            INFTtlFare = (Convert.ToDecimal(fltFare.Rows(fd)("Total")) * numberOfINF).ToString
                            TicketFormate += INFTtlFare.ToString
                            TicketFormate += "</td>"
                            TicketFormate += "</tr>"
                            initialift += 1

                        End If
                    Next
                    fare = AdtTtlFare + ChdTtlFare + INFTtlFare
                Else
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='2' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top; display:none;'id='td_perpaxtype'>" + FltPaxList.Rows(0)("PaxType") + "</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Base Fare</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("BaseFare").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Fuel Surcharge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("Fuel").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Tax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtax'>"
                    TicketFormate += fltFare.Rows(0)("Tax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>STax</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("ServiceTax").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Fee</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                    TicketFormate += fltFare.Rows(0)("TFee").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Charge</td>"
                    TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'id='td_perpaxtc'>"
                    TicketFormate += fltFare.Rows(0)("TCharge").ToString
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"

                    Dim ResuCharge As Decimal = 0
                    Dim ResuServiseCharge As Decimal = 0
                    Dim ResuFareDiff As Decimal = 0
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuCharge").ToString
                        ResuCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuServiseCharge")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Srv. Charge</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuServiseCharge").ToString
                        ResuServiseCharge = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuServiseCharge"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    If Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) IsNot Nothing AndAlso Convert.ToString(FltHeaderList.Rows(0)("ResuFareDiff")) <> "" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Fare Diff</td>"
                        TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>"
                        TicketFormate += FltHeaderList.Rows(0)("ResuFareDiff").ToString
                        ResuFareDiff = (Convert.ToDecimal(FltHeaderList.Rows(0)("ResuFareDiff"))).ToString
                        TicketFormate += "</td>"
                        TicketFormate += "</tr>"
                    End If
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;'>TOTAL</td>"
                    TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;' id='td_totalfare'>"
                    fare = (Convert.ToDecimal(fltFare.Rows(0)("BaseFare")) + Convert.ToDecimal(fltFare.Rows(0)("Fuel")) + Convert.ToDecimal(fltFare.Rows(0)("Tax")) + Convert.ToDecimal(fltFare.Rows(0)("ServiceTax")) + Convert.ToDecimal(fltFare.Rows(0)("TCharge")) + Convert.ToDecimal(fltFare.Rows(0)("TFee")) + ResuCharge + ResuServiseCharge + ResuFareDiff).ToString
                    TicketFormate += fare.ToString
                    TicketFormate += "</td>"

                    'fare = Convert.ToDecimal(fltFare.Rows[0]["Total"]) + ResuCharge + ResuServiseCharge + ResuFareDiff;
                    TicketFormate += "</tr>"
                End If
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                If fltMealAndBag.Rows.Count > 0 Then
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='7' style= 'background-color: #0b2759;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Pax Name</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trip Type</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Code</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Price</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Code</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Price</td>"
                    TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Total</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"

                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='7' style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"


                    For i As Integer = 0 To fltMealAndBag.Rows.Count - 1
                        'If Convert.ToString(fltMealAndBag.Rows(i)("MealPrice")) <> "0.00" AndAlso Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice")) <> "0.00" Then
                        TicketFormate += "<tr>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("Name"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TripType"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("MealPrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggageCode"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>"
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("BaggagePrice"))
                        TicketFormate += "</td>"
                        TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>"
                        MealBagTotalPrice += Convert.ToDecimal(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += Convert.ToString(fltMealAndBag.Rows(i)("TotalPrice"))
                        TicketFormate += "</td>"

                        TicketFormate += "</tr>"
                        'End If
                    Next
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                End If
                Dim seatdetails As String = ""
                Dim seatFareO As Integer = 0
                If SeatListO.Count > 0 Then
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style= 'background-color: #eee;width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td   style='background-color: #28394e; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'> Traveller Seat Information:</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate &= "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Traveller</td>"
                    TicketFormate &= "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Sector</td>"
                    TicketFormate &= "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Seat</td>"
                    TicketFormate &= "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Type</td>"
                    TicketFormate &= "<td style='font-size:12px; color: #424242; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Amount</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                    TicketFormate += "<tr>"
                    TicketFormate += "<td style='width:100%;'>"
                    TicketFormate += "<table style='width:100%;'>"

                    For i As Integer = 0 To SeatListO.Count - 1
                        Dim dts As DataTable = New DataTable()
                        dts = SelectPaxDetail(OrderId, SeatListO(i).PaxId)
                        TicketFormate += "<tr>"
                        TicketFormate &= "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>" & dts.Rows(0)("Name") & "</td>"
                        TicketFormate &= "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>" & SeatListO(i).Origin & "-" & SeatListO(i).Destination & "</div>"
                        TicketFormate &= "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatDesignator & "</td>"
                        TicketFormate &= "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>" & SeatListO(i).SeatAlignment & "</td>"
                        TicketFormate &= "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>" & SeatListO(i).Amount & "</td>"
                        seatFareO = seatFareO + Convert.ToInt32(SeatListO(i).Amount)
                        TicketFormate += "</tr>"
                    Next
                    TicketFormate += "</table>"
                    TicketFormate += "</td>"
                    TicketFormate += "</tr>"
                End If

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='background-color: #0b2759; color:#fdc42c;font-size:11px;font-weight:bold; padding: 5px;'>"
                TicketFormate += "<table style='width:100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='font-size: 10px; width: 15%; text-align: left; vertical-align: top;'></td>"
                TicketFormate += "<td style='color: #fff; font-size: 10px; width: 15%; text-align: left; vertical-align: top;'>Grand Total</td>"
                TicketFormate += "<td style='color: #fff; font-size: 10px; width: 10%; text-align: left; vertical-align: top;'id='td_grandtot'>"
                TicketFormate += (fare + MealBagTotalPrice + seatFareO).ToString
                TicketFormate += "</td>"

                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<br/><br/>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size:10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control."
                TicketFormate += "For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='background-color: #0b2759; color: #fff; font-size: 11px; font-weight: bold; padding: 5px;'>Terms & Conditions :</td>"
                TicketFormate += "</tr>"

                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4'>"
                TicketFormate += "<ul style='list-style-image: url(https://RWT.co/Images/bullet.png);'>"
                TicketFormate += "<li style='font-size:10.5px;'>Guests are requested to carry their valid photo identification for all guests, including children.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>We recommend check-in at least 2 hours prior to departure.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>"
                TicketFormate += "Flight schedules are subject to change and approval by authorities."
                TicketFormate += "<br />"
                TicketFormate += "</li>"
                TicketFormate += "<li style='font-size:10.5px;'>"
                TicketFormate += "Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof."
                TicketFormate += "<br />"
                TicketFormate += " Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation."
                TicketFormate += "</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>"

                TicketFormate += "<li style='font-size:10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Cancellation amount will be charged as per airline rule.</li>"
                TicketFormate += "<li style='font-size:10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>"
                TicketFormate += "</ul>"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                TicketFormate += "</table>"
                TicketFormate += "<table style='width: 100%;'>"
                TicketFormate += "<tr>"
                TicketFormate += "<td colspan='4' style='background-color: #0b2759; color:#fff; font-size: 11px; font-weight: bold; padding: 5px;'>Baggage Information :"
                TicketFormate += "</td>"
                TicketFormate += "</tr>"
                'Dim Bag As Boolean = False
                If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
                    Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
                End If


                Dim dtbaggage As New DataTable
                dtbaggage = objTranDom.GetBaggageInformation("D", FltHeaderList.Rows(0)("VC"), Bag).Tables(0)
                Dim bginfo As String = GetBagInfo(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("Provider")), Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("AirlineRemark")))

                If bginfo = "" Then

                    For Each drbg In dtbaggage.Rows

                        TicketFormate += "<tr>"
                        TicketFormate += "<td colspan='2' style='font-size:10.5px;'>" & drbg("BaggageName") & "</td>"
                        TicketFormate += "<td colspan='2' style='font-size:10.5px;'>" & drbg("Weight") & "</td>"
                        TicketFormate += "</tr>"
                    Next


                Else
                    TicketFormate += "<tr>"
                    TicketFormate += "<td colspan='2'></td>"
                    TicketFormate += "<td colspan='2' style='font-size:10.5px;'>" & bginfo & "</td>"
                    TicketFormate += "</tr>"

                End If




                TicketFormate += "</table>"

            End If

            'TicketFormate += "<table style='width: 100%;'>"
            'TicketFormate += "<tr>"
            'TicketFormate += "<td style='width: 100%; text-align: justify; color: #0b2759; font-size: 11px; padding: 10px; font-size:10.5px;'>"
            ''TicketFormate += "For any assistance contact: ATPI International Pvt. Ltd. | Tel: 00-91-2240095555 | Fax: 00-91-2240095556 | "

            'TicketFormate += "</td>"
            'TicketFormate += "</tr>"
            'TicketFormate += "</table>"
            '#End Region
            'Dim Body As String = ""

            'Dim status As Integer = 0
            'Try

            '    strFileNmPdf = ConfigurationManager.AppSettings("HTMLtoPDF").ToString().Trim() + FltHeaderList.Rows(0)("GdsPnr") + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf"
            '    Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4)
            '    Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            '    pdfDoc.Open()
            '    Dim sr As New StringReader(TicketFormate)
            '    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
            '    pdfDoc.Close()
            '    writer.Dispose()
            '    sr.Dispose()
            '    writePDF = True
            '    Return TicketFormate
            'Catch ex As Exception
            'End Try
            Return TicketFormate
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Function

    Public Function GetBagInfo(ByVal Provider As String, ByVal Remark As String) As String

        Dim baginfo As String = ""
        If Provider = "TB" Then

            If Remark.Contains("Hand") Then
                baginfo = Remark

            End If
        ElseIf Provider = "YA" Then

            If Remark.Contains("Hand") Then
                baginfo = Remark

            ElseIf Not String.IsNullOrEmpty(Remark) Then

                baginfo = Remark & " Baggage allowance"

            End If


        ElseIf Provider = "1G" Then

            If Remark.Contains("PC") Then

                baginfo = Remark.Replace("PC", " Piece(s) Baggage allowance")
            ElseIf Remark.Contains("K") Then

                baginfo = Remark.Replace("K", " Kg Baggage allowance")

            End If



        End If
        Return baginfo

    End Function
    Public Function SelectPaxDetail(ByVal OrderId As String, ByVal TID As String) As DataTable
        Dim adap As New SqlDataAdapter()
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        If String.IsNullOrEmpty(TID) Then
            Dim dt As New DataTable()

            adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            'adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            adap.Fill(dt)

            Return dt
        Else
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            'adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            adap.Fill(dt)
            Return dt
        End If
    End Function
    Public Function TerminalDetails(ByVal OrderID As String, ByVal DepCity As String, ByVal ArrvlCity As String) As DataTable
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim adap As New SqlDataAdapter("USP_TERMINAL_INFO", con1)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@DEPARTURECITY", DepCity)
        adap.SelectCommand.Parameters.AddWithValue("@ARRIVALCITY", ArrvlCity)
        adap.SelectCommand.Parameters.AddWithValue("@ORDERID", OrderID)
        Dim dt1 As New DataTable()
        con1.Open()
        adap.Fill(dt1)
        con1.Close()
        Return dt1
    End Function
    Private Function FuncIssueG8Pnr(ByVal VC As String, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltDs As DataSet, ByRef AirLinePnr As String) As String

        Dim custinfo As New Hashtable
        Dim pnrno As String = ""
        Dim objdict As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        Dim objdictPaxID As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
        Try
            Dim PaxArray As Array
            Dim cnt As Integer = 1
            PaxArray = PaxDs.Tables(0).Select("PaxType='ADT'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_ADT" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameADT" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameADT" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameADT" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("ADTAge" & i + 1, "30")
                custinfo.Add("BirthDate_ADT" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                custinfo.Add("PaxTypeCode_ADT" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_ADT" & i + 1, PaxArray(i)("PaxId"))
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='CHD'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_CHD" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameCHD" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameCHD" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameCHD" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("CHDAge" & i + 1, "10")
                custinfo.Add("BirthDate_CHD" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                custinfo.Add("PaxTypeCode_CHD" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_CHD" & i + 1, PaxArray(i)("PaxId"))
            Next
            PaxArray = PaxDs.Tables(0).Select("PaxType='INF'", "PaxId ASC")
            For i As Integer = 0 To PaxArray.Length - 1
                custinfo.Add("Title_INF" & i + 1, (PaxArray(i)("Title")))
                custinfo.Add("FNameINF" & i + 1, (PaxArray(i)("FName")))
                custinfo.Add("MnameINF" & i + 1, (PaxArray(i)("MName")))
                custinfo.Add("LnameINF" & i + 1, (PaxArray(i)("LName")))
                custinfo.Add("INFAge" & i + 1, "2")
                custinfo.Add("BirthDate_INF" & i + 1, PaxArray(i)("DOB").ToString().Split("/")(2) + "-" + PaxArray(i)("DOB").ToString().Split("/")(1) + "-" + PaxArray(i)("DOB").ToString().Split("/")(0))
                custinfo.Add("PaxTypeCode_INF" & i + 1, PaxArray(i)("PaxType"))
                custinfo.Add("PaxID_INF" & i + 1, PaxArray(i)("PaxId"))
            Next

            custinfo.Add("Ttl", FltHdrDs.Tables(0).Rows(0)("PgTitle"))
            custinfo.Add("FName", FltHdrDs.Tables(0).Rows(0)("PgFName"))
            custinfo.Add("LName", FltHdrDs.Tables(0).Rows(0)("PgLName"))
            custinfo.Add("sAddName", Resources.Address.CompanyName)
            custinfo.Add("sCity", Resources.Address.City)
            custinfo.Add("sCountry", Resources.Address.Country)
            custinfo.Add("sLine1", Resources.Address.AddLine1)
            custinfo.Add("sLine2", Resources.Address.AddLine2)
            custinfo.Add("sState", Resources.Address.State)
            custinfo.Add("sZip", Resources.Address.Zip)
            custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            custinfo.Add("sEmailId", Resources.Address.Email)
            custinfo.Add("sAgencyPhn", Resources.Address.PhoneNo)
            custinfo.Add("sComments", Resources.Address.Comments)
            custinfo.Add("Customeremail", FltHdrDs.Tables(0).Rows(0)("PgEmail"))
            custinfo.Add("pay_type", "CL")
            custinfo.Add("sFax", "1")
            custinfo.Add("sCurrency", "INR")
            custinfo.Add("sCountryAccCode", "91")
            custinfo.Add("sAreaCityCode", "11")
            custinfo.Add("sCountryCode", "91")
            custinfo.Add("Nationality", "IN")
            custinfo.Add("CountryNameCode", "IN")


            Dim sMobile As String = "123456789"
            Dim sContactType As String = "1"
            Dim sContactNum As String = "0"
            Dim PnrDt As DataTable

            If VC = "G8" Then
                Dim dsCrd As DataSet
                Dim strArray() As String
                strArray = Split(FltDs.Tables(0).Rows(0)("sno"), ":")
                dsCrd = objSql.GetCredentials(strArray(6), Convert.ToString(FltDs.Tables(0).Rows(0)("RESULTTYPE")), "D")
                Try

                    Dim MBArrO As Array
                    Dim MBArrR As Array

                    Dim OriginalTF As Decimal = Convert.ToDecimal(FltDs.Tables(0).Rows(0)("OriginalTF").ToString())
                    Dim MBDT As DataSet = objSql.Get_MEAL_BAG_FareDetails(FltDs.Tables(0).Rows(0)("Track_id").ToString(), "")

                    If (MBDT.Tables(0).Rows.Count > 0) Then
                        For jj As Integer = 0 To MBDT.Tables(0).Rows.Count - 1
                            OriginalTF = OriginalTF + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("BaggagePrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("MealPrice")) + Convert.ToDecimal(MBDT.Tables(0).Rows(jj)("SeatPrice"))
                        Next

                    End If



                    Dim Adt_No As Integer = FltDs.Tables(0).Rows(0)("Adult")
                    Dim Chd_No As Integer = FltDs.Tables(0).Rows(0)("Child")
                    Dim Inf_No As Integer = FltDs.Tables(0).Rows(0)("Infant")
                    Dim bookFlightInput As STD.Shared.G8BookFlightRequest = New STD.Shared.G8BookFlightRequest()
                    Dim custlist As New List(Of STD.Shared.G8Person)()
                    Dim paxid As Integer = 2141
                    Dim TravelWithpaxid As Integer = 2141
                    Dim iCtr As Integer = 1
                    If Adt_No > 0 Then
                        Do While iCtr <= Adt_No
                            Dim pasnger As New STD.Shared.G8Person()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameADT" & iCtr)
                            pasnger.LastName = custinfo("LnameADT" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_ADT" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 1
                            pasnger.Title = custinfo("Title_ADT" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Trim() = "MR" Or pasnger.Title.ToUpper().Contains("DR") Or pasnger.Title.ToUpper().Contains("PROF"), "Male", "Female") ' "Male"
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_ADT" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            iCtr = iCtr + 1
                            paxid = paxid + 1
                        Loop
                        iCtr = 1
                    End If
                    If Chd_No > 0 Then
                        Do While iCtr <= Chd_No
                            Dim pasnger As New STD.Shared.G8Person()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameCHD" & iCtr)
                            pasnger.LastName = custinfo("LnameCHD" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_CHD" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 6
                            pasnger.Title = custinfo("Title_CHD" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Contains("MR"), "Male", "Female")
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_CHD" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            iCtr = iCtr + 1
                            paxid = paxid + 1
                        Loop
                        iCtr = 1
                    End If
                    If Inf_No > 0 Then
                        Do While iCtr <= Inf_No
                            Dim pasnger As New STD.Shared.G8Person()
                            pasnger.ContactNum = "123456789"
                            pasnger.ContactType = 2
                            pasnger.FirstName = custinfo("FNameINF" & iCtr)
                            pasnger.LastName = custinfo("LnameINF" & iCtr)
                            pasnger.DOB = custinfo("BirthDate_INF" & iCtr)
                            pasnger.ProfileID = 0
                            pasnger.PTCID = 5
                            pasnger.Title = custinfo("Title_INF" & iCtr)
                            pasnger.Gender = If(pasnger.Title.ToUpper().Contains("MSTR") Or pasnger.Title.ToUpper().Contains("MR"), "Male", "Female")
                            pasnger.PersonOrgID = paxid
                            objdictPaxID.Add(custinfo("PaxID_INF" & iCtr).ToString().Trim(), paxid)
                            custlist.Add(pasnger)
                            pasnger.TravelWithPersonOrgID = TravelWithpaxid

                            iCtr = iCtr + 1
                            paxid = paxid + 1
                            TravelWithpaxid = TravelWithpaxid + 1
                        Loop
                        iCtr = 1
                    End If
                    bookFlightInput.CustomerList = custlist


                    Dim segmentList As New List(Of STD.Shared.G8Segment)
                    Dim datarowOweWay As DataRow() = FltDs.Tables(0).Select("Flight=1")
                    Dim datarowR As DataRow() = FltDs.Tables(0).Select("Flight=2")
                    If datarowOweWay.Length > 0 Then
                        Dim seg As New STD.Shared.G8Segment()
                        seg.FareInformationID = Convert.ToInt16(Split(datarowOweWay(0)("sno"), ":")(0))
                        seg.MarketingCode = Nothing
                        Dim splSrvlistO As New List(Of STD.Shared.G8ServiceQuoteResponse)()
                        MBArrO = MBDT.Tables(0).Select("TripType='O'", "MBID ASC")
                        For i As Integer = 0 To MBArrO.Length - 1
                            If Convert.ToDecimal(MBArrO(i)("BaggagePriceWithNoTax")) > 0 Then


                                Dim splBGO As New STD.Shared.G8ServiceQuoteResponse()
                                splBGO.CodeType = MBArrO(i)("BaggageCode").ToString().Trim()
                                splBGO.DepartureDate = Split(Split(datarowOweWay(0)("sno"), ":")(3), "T")(0)
                                splBGO.LogicalFlightID = Split(datarowOweWay(0)("Searchvalue"), ":")(0)
                                splBGO.Amount = MBArrO(i)("BaggagePriceWithNoTax")
                                splBGO.SSRCategory = Split(MBArrO(i)("BaggageCategory"), "_")(0)
                                splBGO.ServiceID = Split(MBArrO(i)("BaggageCategory"), "_")(1)
                                splBGO.PersonOrgID = objdictPaxID(MBArrO(i)("PaxID").ToString().Trim())
                                splSrvlistO.Add(splBGO)

                            End If


                            If Convert.ToDecimal(MBArrO(i)("MealPriceWithNoTax")) > 0 Then


                                Dim splMealO As New STD.Shared.G8ServiceQuoteResponse()
                                splMealO.CodeType = MBArrO(i)("MealCode").ToString().Trim()
                                splMealO.DepartureDate = Split(Split(datarowOweWay(0)("sno"), ":")(3), "T")(0)
                                splMealO.LogicalFlightID = Split(datarowOweWay(0)("Searchvalue"), ":")(0)
                                splMealO.Amount = MBArrO(i)("MealPriceWithNoTax")
                                splMealO.SSRCategory = Split(MBArrO(i)("MealCategory"), "_")(0)
                                splMealO.ServiceID = Split(MBArrO(i)("MealCategory"), "_")(1)
                                splMealO.PersonOrgID = objdictPaxID(MBArrO(i)("PaxID").ToString().Trim())
                                splSrvlistO.Add(splMealO)

                            End If

                            If Convert.ToDecimal(MBArrO(i)("SeatPriceWithNoTax")) > 0 Then


                                Dim splSeatO As New STD.Shared.G8ServiceQuoteResponse()
                                splSeatO.CodeType = MBArrO(i)("SeatCode").ToString().Trim()
                                splSeatO.DepartureDate = Split(Split(datarowOweWay(0)("sno"), ":")(3), "T")(0)
                                splSeatO.LogicalFlightID = Split(datarowOweWay(0)("Searchvalue"), ":")(0)
                                splSeatO.Amount = MBArrO(i)("SeatPriceWithNoTax")
                                splSeatO.SSRCategory = Split(MBArrO(i)("SeatCategory"), "_")(0)
                                splSeatO.ServiceID = Split(MBArrO(i)("SeatCategory"), "_")(1)
                                splSeatO.PersonOrgID = objdictPaxID(MBArrO(i)("PaxID").ToString().Trim())
                                splSrvlistO.Add(splSeatO)

                            End If




                        Next
                        seg.SpecialServices = splSrvlistO
                        segmentList.Add(seg)
                    End If
                    If datarowR.Length > 0 Then
                        Dim seg1 As New STD.Shared.G8Segment()
                        seg1.FareInformationID = Convert.ToInt16(Split(datarowR(0)("sno"), ":")(0))
                        seg1.MarketingCode = Nothing

                        Dim splSrvlistR As New List(Of STD.Shared.G8ServiceQuoteResponse)
                        MBArrR = MBDT.Tables(0).Select("TripType='R'", "MBID ASC")
                        For j As Integer = 0 To MBArrR.Length - 1

                            If Convert.ToDecimal(MBArrR(j)("BaggagePriceWithNoTax")) > 0 Then

                                Dim splR As New STD.Shared.G8ServiceQuoteResponse()
                                splR.CodeType = MBArrR(j)("BaggageCode").ToString().Trim()

                                splR.DepartureDate = Split(Split(datarowR(0)("sno"), ":")(3), "T")(0)
                                splR.LogicalFlightID = Split(datarowR(0)("Searchvalue"), ":")(0)
                                splR.Amount = MBArrR(j)("BaggagePriceWithNoTax")
                                splR.SSRCategory = Split(MBArrR(j)("BaggageCategory"), "_")(0)
                                splR.ServiceID = Split(MBArrR(j)("BaggageCategory"), "_")(1)
                                splR.PersonOrgID = objdictPaxID(MBArrR(j)("PaxID").ToString().Trim())
                                splSrvlistR.Add(splR)
                            End If



                            If Convert.ToDecimal(MBArrR(j)("MealPriceWithNoTax")) > 0 Then

                                Dim splMealR As New STD.Shared.G8ServiceQuoteResponse()
                                splMealR.CodeType = MBArrR(j)("MealCode").ToString().Trim()
                                splMealR.DepartureDate = Split(Split(datarowR(0)("sno"), ":")(3), "T")(0)
                                splMealR.LogicalFlightID = Split(datarowR(0)("Searchvalue"), ":")(0)
                                splMealR.Amount = MBArrR(j)("MealPriceWithNoTax")
                                splMealR.SSRCategory = Split(MBArrR(j)("MealCategory"), "_")(0)
                                splMealR.ServiceID = Split(MBArrR(j)("MealCategory"), "_")(1)
                                splMealR.PersonOrgID = objdictPaxID(MBArrR(j)("PaxID").ToString().Trim())
                                splSrvlistR.Add(splMealR)
                            End If


                            If Convert.ToDecimal(MBArrR(j)("SeatPriceWithNoTax")) > 0 Then

                                Dim splSeatR As New STD.Shared.G8ServiceQuoteResponse()
                                splSeatR.CodeType = MBArrR(j)("SeatCode").ToString().Trim()
                                splSeatR.DepartureDate = Split(Split(datarowR(0)("sno"), ":")(3), "T")(0)
                                splSeatR.LogicalFlightID = Split(datarowR(0)("Searchvalue"), ":")(0)
                                splSeatR.Amount = MBArrR(j)("SeatPriceWithNoTax")
                                splSeatR.SSRCategory = Split(MBArrR(j)("SeatCategory"), "_")(0)
                                splSeatR.ServiceID = Split(MBArrR(j)("SeatCategory"), "_")(1)
                                splSeatR.PersonOrgID = objdictPaxID(MBArrR(j)("PaxID").ToString().Trim())
                                splSrvlistR.Add(splSeatR)
                            End If


                        Next
                        seg1.SpecialServices = splSrvlistR

                        segmentList.Add(seg1)
                    End If

                    bookFlightInput.Address = custinfo("sLine1")
                    bookFlightInput.Address2 = custinfo("sLine2")
                    bookFlightInput.CarrierCurrency = custinfo("sCurrency")
                    bookFlightInput.City = custinfo("sCity")
                    bookFlightInput.ContactValue = custinfo("sAgencyPhn")
                    bookFlightInput.Country = custinfo("sCountry")
                    bookFlightInput.CountryCode = custinfo("sCountryCode")
                    bookFlightInput.AreaCode = custinfo("sAreaCityCode")
                    bookFlightInput.DisplayCurrency = custinfo("sCurrency")
                    bookFlightInput.Email = custinfo("Customeremail")
                    bookFlightInput.Fax = custinfo("sFax")
                    bookFlightInput.IATANum = dsCrd.Tables(0).Rows(0)("CorporateID").ToString()
                    bookFlightInput.Mobile = custinfo("sHomePhn")
                    bookFlightInput.Postal = custinfo("sZip")
                    bookFlightInput.ProfileID = 0
                    bookFlightInput.PromoCode = Nothing
                    bookFlightInput.SecurityGUID = strArray(1)
                    bookFlightInput.State = custinfo("sState")
                    bookFlightInput.WebBookingID = strArray(1)
                    bookFlightInput.paymentDetails = New STD.Shared.G8Payment()
                    bookFlightInput.paymentDetails.CompanyName = custinfo("sAddName")
                    bookFlightInput.paymentDetails.ExchangeRate = 1
                    bookFlightInput.paymentDetails.ExchangeRateDate = DateTime.Now
                    bookFlightInput.paymentDetails.FirstName = custinfo("FName")
                    bookFlightInput.paymentDetails.ISOCurrency = 1
                    bookFlightInput.paymentDetails.LastName = custinfo("LName")
                    bookFlightInput.paymentDetails.OriginalAmount = Convert.ToDecimal(OriginalTF)
                    bookFlightInput.paymentDetails.OriginalCurrency = custinfo("sCurrency")
                    bookFlightInput.paymentDetails.PaymentAmount = Convert.ToDecimal(OriginalTF)
                    bookFlightInput.paymentDetails.PaymentComment = custinfo("sComments")
                    bookFlightInput.paymentDetails.PaymentCurrency = custinfo("sCurrency")
                    bookFlightInput.paymentDetails.PaymentDate = DateTime.Now
                    bookFlightInput.paymentDetails.PaymentNum = 1
                    bookFlightInput.paymentDetails.VoucherNum = Nothing
                    bookFlightInput.segment = segmentList

                    Dim bookFlight As New STD.BAL.G8BookBAL(Split(datarowOweWay(0)("sno"), ":")(1), dsCrd.Tables(0).Rows(0)("CorporateID"), dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("ServerIP"))

                    objdict = bookFlight.BookFlight(bookFlightInput)

                    'PnrDt = bookFlight.BookReservation(bookFlightInput)
                    ' pnrno = PnrDt.Rows(0)("ConfirmationNo")
                    AirLinePnr = objdict("PNR")
                    pnrno = AirLinePnr

                    objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), If(objdict.ContainsKey("PNR"), objdict("PNR"), ""), If(objdict.ContainsKey("SummaryPnrReq"), objdict("SummaryPnrReq"), ""), _
                                          If(objdict.ContainsKey("SummaryPnrRes"), objdict("SummaryPnrRes"), ""), If(objdict.ContainsKey("ProcessPNRPaymentReq"), objdict("ProcessPNRPaymentReq"), ""), _
                                        If(objdict.ContainsKey("ProcessPNRPaymentRes"), objdict("ProcessPNRPaymentRes"), ""), If(objdict.ContainsKey("CreatePNRRes"), objdict("CreatePNRRes"), ""), _
                                      If(objdict.ContainsKey("CreatePNRReq"), objdict("CreatePNRReq"), ""), "", "", "", "", "", _
                                     If(objdict.ContainsKey("EXEP"), objdict("EXEP"), ""), "", "", "", "", "", "")
                Catch ex As Exception
                    objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), If(objdict.ContainsKey("PNR"), objdict("PNR"), ""), If(objdict.ContainsKey("SummaryPnrReq"), objdict("SummaryPnrReq"), ""), _
                                          If(objdict.ContainsKey("SummaryPnrRes"), objdict("SummaryPnrRes"), ""), If(objdict.ContainsKey("ProcessPNRPaymentReq"), objdict("ProcessPNRPaymentReq"), ""), _
                                        If(objdict.ContainsKey("ProcessPNRPaymentRes"), objdict("ProcessPNRPaymentRes"), ""), If(objdict.ContainsKey("CreatePNRRes"), objdict("CreatePNRRes"), ""), _
                                      If(objdict.ContainsKey("CreatePNRReq"), objdict("CreatePNRReq"), ""), "", "", "", "", "", _
                                     If(objdict.ContainsKey("EXEP"), objdict("EXEP"), ""), "", "", "", "", "Seat", "")
                End Try
                Try
                    If pnrno <> "" And pnrno IsNot Nothing Then
                    Else
                        Dim xx As String
                        xx = objSql.GetRndm()
                        pnrno = VC & xx & "-FQ"
                        AirLinePnr = VC & xx & "-FQ"
                    End If
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception


        End Try
        Return pnrno
    End Function
End Class


