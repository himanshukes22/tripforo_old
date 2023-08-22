Imports System.Data
Imports YatraBilling
Partial Class LccRF_LccRTFBooking
    Inherits System.Web.UI.Page
#Region "Variable Declaration:"
    Private OBTrackId As String, IBTrackId As String, FT As String
    Dim objTktCopy As New clsTicketCopy
    Dim objDA As New SqlTransaction
    Dim objOnlineTkt As New clsOnlineTicketing
    Dim objSql As New SqlTransactionNew
    Dim objSqlDom As New SqlTransactionDom
    Dim STYTR As New SqlTransactionYatra
    Dim objSMSAPI As New SMSAPI.SMS
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            Dim FltDsR As DataSet
            Dim PaxDsR As DataSet
            Dim FltHdrDsR As DataSet
            Dim FltFareDsR As DataSet
            Dim FltDataTable As New DataTable
            Dim AgencyDs As DataSet

            Dim BkgStatus As String = ""
            Dim vc As String = ""
            Dim GdsPnr As String = ""
            Dim AirlinePnr As String = ""
            Dim TktNoArray As New ArrayList
            Dim AvlBal As Double = 0
            Dim OrginalTotFare As Double = 0
            Dim smsStatus As String = ""
            Dim smsMsg As String = ""
            If Session("RTFBookIng") = "TRUE" Then
                'strTktCopy = "Please make new search for another booking."
                Response.Redirect("../Login.aspx")
            Else
                Session("RTFBookIng") = "TRUE"
                AgencyDs = objDA.GetAgencyDetails(Session("UID"))

                FltDs = objDA.GetFltDtlsInt(OBTrackId, Session("UID"))
                PaxDs = objDA.GetPaxDetails(OBTrackId)
                FltHdrDs = objDA.GetHdrDetails(OBTrackId)
                FltFareDs = objDA.GetFltFareDtl(OBTrackId)
                FltDataTable = FltDs.Tables(0)
                OrginalTotFare = FltDs.Tables(0).Rows(0)("OriginalTF")

                FltDsR = objDA.GetFltDtlsInt(IBTrackId, Session("UID"))
                PaxDsR = objDA.GetPaxDetails(IBTrackId)
                FltHdrDsR = objDA.GetHdrDetails(IBTrackId)
                FltFareDsR = objDA.GetFltFareDtl(IBTrackId)
                FltDataTable.Merge(FltDsR.Tables(0), False, MissingSchemaAction.Add)
                OrginalTotFare += FltDsR.Tables(0).Rows(0)("OriginalTF")
                vc = FltDs.Tables(0).Rows(0)("ValiDatingCarrier")
                Dim ProjectId As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
                Dim BookedBy As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDs.Tables(0).Rows(0)("BookedBy").ToString())
                Dim BillNoCorp As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDs.Tables(0).Rows(0)("BillNoCorp").ToString())

                If FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "TICKETED" And FltHdrDs.Tables(0).Rows(0)("Status").ToString.Trim.ToUpper <> "CONFIRM" Then
                    If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.Trim <> "NOT ACTIVE" Then
                        If FltHdrDs.Tables(0).Rows(0)("TotalAfterDis") <= Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim) Then
                            GdsPnr = FuncIssueLccPnr(vc, PaxDs, FltHdrDs, FltDataTable, AirlinePnr, OrginalTotFare)
                            If GdsPnr <> "" Then
                                BkgStatus = "Ticketed"
                                '*** OutBound start******
                                AvlBal = FuncDBUpdation(OBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDs.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDs.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                PaxAndLedgerDbUpdation(OBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDs, FltFareDs, ProjectId, BookedBy, BillNoCorp)
                                MessageForInfantPax(GdsPnr, vc, PaxDs)
                                strTktCopy = mailTktCopy(vc, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltHdrDs.Tables(0).Rows(0)("Sector"), FltDs.Tables(0).Rows(0)("Departure_Date"), "OutBound", AirlinePnr, GdsPnr, BkgStatus, OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgEmail"))
                                'YTR Integration
                                'Online Billing
                                Try
                                    'Dim AirObj As New AIR_YATRA
                                    'AirObj.ProcessYatra_Air(OBTrackId, GdsPnr, "B")
                                Catch ex As Exception

                                End Try
                                'Online Billing end
                                'Offline billing
                                'Try
                                '    STYTR.InsertYatra_MIRHEADER(OBTrackId, GdsPnr)
                                '    STYTR.InsertYatra_PAX(OBTrackId, GdsPnr)
                                '    STYTR.InsertYatra_SEGMENT(OBTrackId, GdsPnr)
                                '    STYTR.InsertYatra_FARE(OBTrackId, GdsPnr)
                                '    STYTR.InsertYatra_DIFTLINES(OBTrackId, GdsPnr)
                                Try
                                    Dim SmsCrd As DataTable
                                    SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)
                                    If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                        smsStatus = objSMSAPI.sendSms(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                        objSql.SmsLogDetails(OBTrackId, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                    End If

                                Catch ex As Exception

                                End Try

                                '                        Catch ex As Exception

                                'End Try
                                'Offline billing end
                                '*** OutBound End******
                                '*** InBound Start ******
                                GdsPnr = GdsPnr & "_R"
                                AvlBal = 0
                                AvlBal = FuncDBUpdation(IBTrackId, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Session("UID"), FltHdrDsR.Tables(0).Rows(0)("TotalBookingCost"), FltHdrDsR.Tables(0).Rows(0)("TotalAfterDis"), FltHdrDsR.Tables(0).Rows(0)("Sector"), vc, GdsPnr, AirlinePnr, BkgStatus)
                                Dim ProjectIdR As String = If(IsDBNull(FltHdrDs.Tables(0).Rows(0)("ProjectID")), Nothing, FltHdrDs.Tables(0).Rows(0)("ProjectID").ToString())
                                Dim BookedByR As String = If(IsDBNull(FltHdrDsR.Tables(0).Rows(0)("BookedBy")), Nothing, FltHdrDsR.Tables(0).Rows(0)("BookedBy").ToString())
                                Dim BillNoCorpR As String = If(IsDBNull(FltHdrDsR.Tables(0).Rows(0)("BillNoCorp")), Nothing, FltHdrDsR.Tables(0).Rows(0)("BillNoCorp").ToString())

                                PaxAndLedgerDbUpdation(IBTrackId, vc, GdsPnr, Session("UID"), AgencyDs.Tables(0).Rows(0)("Agency_Name"), FltHdrDsR.Tables(0).Rows(0)("TotalAfterDis"), AvlBal, TktNoArray, PaxDsR, FltFareDsR, ProjectIdR, BookedByR, BillNoCorpR)
                                MessageForInfantPax(GdsPnr, vc, PaxDs)
                                strTktCopy = strTktCopy & "<br/>" & mailTktCopy(vc, FltDsR.Tables(0).Rows(0)("FlightIdentification"), FltHdrDsR.Tables(0).Rows(0)("Sector"), FltDsR.Tables(0).Rows(0)("Departure_Date"), "InBound", AirlinePnr, GdsPnr, BkgStatus, IBTrackId, FltHdrDsR.Tables(0).Rows(0)("PgEmail"))
                                'YTR Integration
                                'Online Billing
                                Try
                                    'Dim AirObj As New AIR_YATRA
                                    'AirObj.ProcessYatra_Air(IBTrackId, GdsPnr, "B")
                                Catch ex As Exception

                                End Try
                                'Online Billing end

                                'Offline billing
                                'Try
                                'STYTR.InsertYatra_MIRHEADER(IBTrackId, GdsPnr)
                                'STYTR.InsertYatra_PAX(IBTrackId, GdsPnr)
                                'STYTR.InsertYatra_SEGMENT(IBTrackId, GdsPnr)
                                'STYTR.InsertYatra_FARE(IBTrackId, GdsPnr)
                                'STYTR.InsertYatra_DIFTLINES(IBTrackId, GdsPnr)
                                Try
                                    Dim SmsCrd As DataTable
                                    SmsCrd = objDA.SmsCredential(SMS.AIRBOOKINGDOM.ToString()).Tables(0)
                                    If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                        smsStatus = objSMSAPI.sendSms(IBTrackId, FltHdrDsR.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDsR.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDsR.Tables(0).Rows(0)("VC").ToString.Trim, FltDsR.Tables(0).Rows(0)("FlightIdentification"), FltDsR.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                        objSql.SmsLogDetails(IBTrackId, FltHdrDsR.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                    End If

                                Catch ex As Exception

                                End Try

                                '                        Catch ex As Exception

                                'End Try
                                'Offline billing end
                                '*** InBound End ******
                            Else
                                strTktCopy = "<strong style='font-size:14px'>Unable to confirm your booking. Please try again.</strong>"
                            End If
                        Else
                            Response.Redirect("../International/BookingMsg.aspx?msg=CL")
                        End If
                    Else
                        Response.Redirect("../International/BookingMsg.aspx?msg=NA")
                    End If
                Else
                    strTktCopy = "<strong style='font-size:14px'>You cann't book ticket using same booking reference number(" & OBTrackId & ")</strong>"
                End If
            End If
        Catch ex As Exception
            strTktCopy = ex.Message
        End Try
        'lblTkt.Text = strTktCopy
        Session("DomRtfStrTktCopy") = strTktCopy
        Response.Redirect("LccBkgConfirmation.aspx", True)
    End Sub

    Private Function FuncIssueLccPnr(ByVal VC As String, ByVal PaxDs As DataSet, ByVal FltHdrDs As DataSet, ByVal FltTable As DataTable, ByRef AirLinePnr As String, ByVal OrginalTotFare As Double) As String
        Dim dsCrd As DataSet = objSql.GetCredentials(VC, "", "")
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

            custinfo.Add("Ttl", FltHdrDs.Tables(0).Rows(0)("PgTitle"))
            custinfo.Add("FName", FltHdrDs.Tables(0).Rows(0)("PgFName"))
            custinfo.Add("LName", FltHdrDs.Tables(0).Rows(0)("PgLName"))
            custinfo.Add("sAddName", ConfigurationManager.AppSettings.Keys("companyname"))
            custinfo.Add("sCity", ConfigurationManager.AppSettings.Keys("companycity"))
            custinfo.Add("sCountry", ConfigurationManager.AppSettings.Keys("companycountry"))
            custinfo.Add("sLine1", ConfigurationManager.AppSettings.Keys("companyaddress1"))
            custinfo.Add("sLine2", ConfigurationManager.AppSettings.Keys("companyaddress2"))
            custinfo.Add("sState", ConfigurationManager.AppSettings.Keys("companystate"))
            custinfo.Add("sZip", ConfigurationManager.AppSettings.Keys("companyzip"))
            custinfo.Add("sHomePhn", FltHdrDs.Tables(0).Rows(0)("PgMobile"))
            custinfo.Add("sEmailId", ConfigurationManager.AppSettings.Keys("companyemail"))
            custinfo.Add("sAgencyPhn", ConfigurationManager.AppSettings.Keys("companyphone"))
            custinfo.Add("sComments", "OnLine Booking(" + ConfigurationManager.AppSettings.Keys("companyname") + ")")
            custinfo.Add("Customeremail", FltHdrDs.Tables(0).Rows(0)("PgEmail"))
            custinfo.Add("pay_type", "CL")
            custinfo.Add("sFax", "1")
            custinfo.Add("sCurrency", "INR")
            Dim sMobile As String = "123456789" '
            Dim sContactType As String = "1"
            Dim sContactNum As String = "0"
            Dim PnrDt As DataTable
            If VC = "6E" Then
                Dim obj6E As New NEWLCCRTF.RTFclsIndigo
                PnrDt = obj6E.GetBookingDT(FltTable, custinfo, OrginalTotFare.ToString, FltHdrDs.Tables(0).Rows(0)("Adult"), FltHdrDs.Tables(0).Rows(0)("Child"), FltHdrDs.Tables(0).Rows(0)("Infant"), "Return")
                pnrno = PnrDt.Rows(0)("PNRId")
                AirLinePnr = PnrDt.Rows(0)("PNRId")
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrDt.Rows(0)("PNRId"), PnrDt.Rows(0)("ReqXml"), PnrDt.Rows(0)("ResXml"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            ElseIf VC = "SG" Then
                Dim objSG As New NEWLCCRTF.RTFclsSpiceJet
                PnrDt = objSG.GetBookingDT(FltTable, custinfo, OrginalTotFare.ToString, FltHdrDs.Tables(0).Rows(0)("Adult"), FltHdrDs.Tables(0).Rows(0)("Child"), FltHdrDs.Tables(0).Rows(0)("Infant"), "Return")
                pnrno = PnrDt.Rows(0)("PNRId")
                AirLinePnr = PnrDt.Rows(0)("PNRId")
                objSql.InsertLccBkgLogs(VC, FltHdrDs.Tables(0).Rows(0)("OrderId"), PnrDt.Rows(0)("PNRId"), PnrDt.Rows(0)("ReqXml"), PnrDt.Rows(0)("ResXml"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            End If
        Catch ex As Exception
        End Try
        Return pnrno
    End Function

    Private Function FuncDBUpdation(ByVal OrderId As String, ByVal AgencyName As String, ByVal UID As String, ByVal TotalFare As Double, ByVal NetFare As Double, ByVal Sector As String, ByVal VC As String, ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal BkgStatus As String) As Double
        Dim AvlBal As Double = 0
        objDA.UpdateFltHeader(OrderId, AgencyName, GdsPnr, AirlinePnr, BkgStatus)
        AvlBal = objDA.UpdateCrdLimit(UID, NetFare)
        objDA.UpdateTransReport(UID, AgencyName, GdsPnr, BkgStatus, AvlBal, TotalFare, NetFare, "RTFLCC Flight Booking", Sector, "CL", VC)
        'strTktCopy = objTktCopy.TicketDetail(trackid, "")
        'strTktCopy = "Unable to confirm your booking. Please try again."
        Return AvlBal
    End Function

    Private Sub PaxAndLedgerDbUpdation(ByVal OrderId As String, ByVal VC As String, ByVal GdsPnr As String, ByVal AgentId As String, ByVal AgencyName As String, ByVal NetFare As Double, ByVal AvlBal As Double, ByVal TktNoArray As ArrayList, ByVal PaxDs As DataSet, ByVal FltFareDs As DataSet, ByVal ProjectId As String, ByVal BookedBy As String, ByVal BillNo As String)
        Dim CurrBal As Double = 0
        CurrBal = AvlBal + NetFare
        For i As Integer = 0 To PaxDs.Tables(0).Rows.Count - 1
            Dim strTktNo As String = ""
            If VC <> "SG" And VC <> "6E" And VC <> "G8" Then
                Dim PNameFromTbl As String = "", PNameFromTktArray As String = ""
                PNameFromTbl = (PaxDs.Tables(0).Rows(i)("Title") & PaxDs.Tables(0).Rows(i)("FName") & PaxDs.Tables(0).Rows(i)("MName") & PaxDs.Tables(0).Rows(i)("LName")).ToString.Replace(" ", "").ToUpper
                For ii As Integer = 0 To TktNoArray.Count - 1
                    strTktNo = ""
                    Dim strtktArray() As String = Split(TktNoArray(ii), "/")
                    PNameFromTktArray = strtktArray(0).ToString.Replace(" ", "").ToUpper
                    strTktNo = strtktArray(1).ToString
                    If PNameFromTbl = PNameFromTktArray Then
                        objSql.UpdateTktNumber(OrderId, PaxDs.Tables(0).Rows(i)("PaxId"), strtktArray(1).ToString)
                        Exit For
                    End If
                Next
            Else
                strTktNo = GdsPnr & (i + 1).ToString
                objSql.UpdateTktNumber(OrderId, PaxDs.Tables(0).Rows(i)("PaxId"), strTktNo)
            End If
            Dim fareArray As Array
            fareArray = FltFareDs.Tables(0).Select("PaxType='" & PaxDs.Tables(0).Rows(i)("PaxType").ToString.Trim & "'", "")
            CurrBal = CurrBal - (fareArray(0)("TotalAfterDis"))
            objSqlDom.insertLedgerDetails(AgentId, AgencyName, OrderId, GdsPnr, strTktNo, VC, "", "", "", Request.UserHostAddress.ToString, (fareArray(0)("TotalAfterDis")), 0, CurrBal, "DomRTFFlt", "", PaxDs.Tables(0).Rows(i)("PaxId"), ProjectId, BookedBy, BillNo)
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
        Dim strTktCopy As String = "", strHTML As String = "", strFileName As String = "", strMailMsg As String = ""
        Dim rightHTML As Boolean = False
        strFileName = "D:\SPR_TicketCopy\" & GdsPnr & "-" & FT & " Flight details-" & DateAndTime.Now.ToString.Replace(":", "").Trim & ".html"
        strFileName = strFileName.Replace("/", "-")
        strTktCopy = objTktCopy.TicketDetail(OrderId, "")
        strHTML = "<html><head><title>Booking Details</title><style type='text/css'> .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}	 .text1{color:#333333; font-weight:bold;}	 .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}	 .pnrdtls1{font-size:12px; color:#333333; text-align:left;}	 .bookdate{font-size:11px; color:#CC6600; text-align:left}	 .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}	 .fltdtls{font-size:11px; color:#333333; text-align:left;}	.text3{font-size:11px; padding:5px;color:#333333; text-align:right}	 .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}	 .hdrtd{background-color:#333333;}	  .lnk{color:#333333;text-decoration:underline;}	  .lnk:hover{color:#333333;text-decoration:none;}	  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}	  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}	 </style></head><body>" & strTktCopy & "</body></html>"
        rightHTML = SaveTextToFile(strHTML, strFileName)
        strMailMsg = "<p style='font-family:verdana; font-size:12px'>Dear Customer<br /><br />"
        strMailMsg = strMailMsg & "Greetings of the day !!!!<br /><br />"
        strMailMsg = strMailMsg & "Please find an attachment for your E-ticket, kindly carry the print out of the same for hassle-free travel. Your onward booking for " & Sector & " is confirmed on " & VC & "-" & FltNo & " for " & DepDate & ". Your airline  booking reference no is " & AirlinePnr & ". <br /><br />"
        strMailMsg = strMailMsg & "Have a nice &amp; wonderful trip.<br /><br />"
        If BkgStatus = "Ticketed" Then
            Dim MailDt As New DataTable
            MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session("UID").ToString()).Tables(0)
            Dim Status As Boolean = False
            Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
            Try
                If (MailDt.Rows.Count > 0) Then
                    If Status = True Then
                        If rightHTML Then
                            objSqlDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), strFileName)
                        Else
                            objSqlDom.SendMail(EmailId, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsg, FT & MailDt.Rows(0)("SUBJECT").ToString(), "")
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End If
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

                    Dim MailDt As New DataTable
                    MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_MAILINGINFANT.ToString(), Session("UID").ToString()).Tables(0)


                    pgstr = pgstr & "<br />Regards,<br />" & MailDt.Rows(0)("REGARDS").ToString() & ""

                    Try
                        'objMail.Credentials = New System.Net.NetworkCredential("b2bticketing", "america")
                        'objMail.Host = "shekhal.ITZ.com"
                        'objMail.Send(msgMail)
                        If (MailDt.Rows.Count > 0) Then
                            Dim Status As Boolean = False
                            Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                            objSqlDom.SendMail(MailDt.Rows(0)("MAILTO").ToString(), MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), pgstr, MailDt.Rows(0)("SUBJECT").ToString(), "")
                            If Status = True Then
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
        Dim objReader As IO.StreamWriter
        Try
            objReader = New IO.StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            Saved = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return Saved
    End Function

End Class
