Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.VisualBasic

Partial Class SprReports_TicketReport
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Private ST As New SqlTransaction()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()
    Private sttusobj As New Status()
    Dim con As New SqlConnection()
    Dim PaxType As String
    Dim clsCorp As New ClsCorporate()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim AgentID As String = ""

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "ADMIN" Then
                    btnRemark.Visible = False
                End If
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False
                    AgentID = Session("UID").ToString()
                    Dim dscancelled As DataSet = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "CB")
                    If dscancelled.Tables(0).Rows.Count > 0 Then
                        DrpCancelledBy.AppendDataBoundItems = True
                        DrpCancelledBy.Items.Clear()
                        DrpCancelledBy.Items.Insert(0, "Select")
                        DrpCancelledBy.DataSource = dscancelled
                        DrpCancelledBy.DataTextField = "BOOKEDBY"
                        DrpCancelledBy.DataValueField = "BOOKEDBY"
                        DrpCancelledBy.DataBind()

                    End If

                    'Else
                    'AgencyDDLDS = ST.GetAgencyDetailsDDL()
                    'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                    ' Bind Agency DDL
                    'Try
                    'If (Session("user_type") = "SALES") Then
                    '    Dim dtag As New DataTable
                    '    dtag = STDom.getAgencybySalesRef(Session("UID").ToString).Tables(0)
                    'ddl_AgencyName.AppendDataBoundItems = True
                    'ddl_AgencyName.Items.Clear()
                    'ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    'ddl_AgencyName.DataSource = dtag
                    'ddl_AgencyName.DataTextField = "Agency_Name"
                    'ddl_AgencyName.DataValueField = "user_id"
                    'ddl_AgencyName.DataBind()
                    'Else
                    'ddl_AgencyName.AppendDataBoundItems = True
                    'ddl_AgencyName.Items.Clear()
                    'ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    'ddl_AgencyName.DataSource = AgencyDDLDS
                    'ddl_AgencyName.DataTextField = "Agency_Name"
                    'ddl_AgencyName.DataValueField = "user_id"
                    'ddl_AgencyName.DataBind()
                    'End If


                    ' Catch ex As Exception
                    'clsErrorLog.LogInfo(ex)

                    'End Try
                    'End If
                End If

                Dim ddate = Request.QueryString("DepDate").ToString()
                Dim ddmonth = Request.QueryString("ddmonth").ToString()
                Dim ddyear = Request.QueryString("ddyear").ToString()
                Dim VPNR = Request.QueryString("VPNR").ToString()
                Dim VPAXNAME = Request.QueryString("VPAXNAME").ToString()
                Dim DEPCITY = Request.QueryString("DEPCITY").ToString()
                Dim ARRCITY = Request.QueryString("ARRCITY").ToString()
                Dim AIRLINE = Request.QueryString("AIRLINE").ToString()
                Dim loginid = Request.QueryString("loginid").ToString()
                Dim user_Type = Request.QueryString("user_Type").ToString()
                Dim agentIDD = Request.QueryString("Agentid").ToString()


                Dim vc As String = ""
                Dim SDEPCITY As String = ""
                Dim SARRCITY As String = ""
                Dim Agent_id As String = ""



                If (Not String.IsNullOrEmpty(agentIDD.Trim())) Then
                    Agent_id = agentIDD.Split("(")(1).ToString().Replace(")", "")
                End If

                If (Not String.IsNullOrEmpty(AIRLINE.Trim())) Then
                    vc = AIRLINE.Split(",")(1)
                End If
                If (Not String.IsNullOrEmpty(DEPCITY.Trim())) Then
                    SDEPCITY = DEPCITY.Split(",")(0)
                End If
                If (Not String.IsNullOrEmpty(ARRCITY.Trim())) Then
                    SARRCITY = ARRCITY.Split(",")(0)
                End If


                If Session("User_Type") = "EXEC" Then
                    ''tr_ExecID.Visible = False
                    ''tdTripNonExec1.Visible = False
                    tdTripNonExec2.Visible = False
                End If
                Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                Dim curr_date1 = Now()
                Dim trip As String = "" ''''IIf(Session("User_Type") = "EXEC", IIf([String].IsNullOrEmpty(Session("TripExec")), "", Session("TripExec").ToString().Trim()), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))
                If Session("User_Type") = "EXEC" Then
                    If [String].IsNullOrEmpty(Session("TripExec")) Then
                        trip = ""
                    Else
                        trip = Session("TripExec").ToString().Trim()
                    End If
                Else
                    trip = If([String].IsNullOrEmpty(ddlTripDomIntl.SelectedItem.Value), "", ddlTripDomIntl.SelectedItem.Value.Trim())
                End If

                grdds.Clear()
                grdds = USP_GetTicketDetail_Intl_calender(ddate, Agent_id, loginid, user_Type, ddmonth, ddyear, VPNR, VPAXNAME, SDEPCITY, SARRCITY, vc)
                BindGrid(grdds)
                Dim dt As DataTable
                Dim Db As String = ""
                Dim sum As Double = 0
                dt = grdds.Tables(0)
                '    If dt.Rows.Count > 0 Then
                '        For Each dr As DataRow In dt.Rows
                '            Db = dr("TotalAfterDis").ToString()
                '            If Db Is Nothing OrElse Db = "" Then
                '                Db = 0
                '            Else
                '                sum += Db
                '            End If
                '        Next

                '    End If
                '    lbl_Total.Text = "0"
                '    If sum <> 0 Then
                '        lbl_Total.Text = sum.ToString '("C", New CultureInfo("en-IN"))
                '    End If
                '    lbl_counttkt.Text = dt.Rows.Count
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid(ByVal Gds As DataSet)
        Try
            Dim dt As DataTable
            Dim Db As String = ""
            Dim sum As Double = 0
            dt = grdds.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Db = dr("TotalAfterDis").ToString()
                    If Db Is Nothing OrElse Db = "" Then
                        Db = 0
                    Else
                        sum += Db
                    End If
                Next

            End If
            lbl_Total.Text = "0"
            If sum <> 0 Then
                lbl_Total.Text = sum.ToString '("C", New CultureInfo("en-IN"))
            End If
            lbl_counttkt.Text = dt.Rows.Count
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

        Try

            Session("Grdds") = Gds
            ticket_grdview.DataSource = Gds
            ticket_grdview.DataBind()
            If Gds.Tables(0).Rows.Count > 0 Then
                divReport.Visible = True
            Else
                divReport.Visible = False
            End If
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then

                ticket_grdview.Columns(16).Visible = False
                ticket_grdview.Columns(17).Visible = False
                ticket_grdview.Columns(19).Visible = True
                ticket_grdview.Columns(5).Visible = True
                'ticket_grdview.Columns(17).Visible = False
            Else
                ticket_grdview.Columns(19).Visible = False
                ticket_grdview.Columns(5).Visible = False
                ''ticket_grdview.Columns(6).Visible = False
                ticket_grdview.Columns(16).Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub lnkreissue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
        gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF275")
        RemarkRefundReissue(lb)
    End Sub

    Protected Sub lnkrefund_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
        gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
        RemarkRefundReissue(lb)
    End Sub
    Public Sub RemarkRefundReissue(ByVal lb As LinkButton)
        Try
            Dim FirstStatus, SecondStatus As String
            Dim gridViewds As New DataSet()
            Dim Paxds As New DataSet()
            gridViewds = Session("Grdds")
            Dim filterArray As Array = gridViewds.Tables(0).Select("PaxId ='" & lb.CommandArgument & "'")
            If filterArray.Length > 0 Then
                FirstStatus = CheckTktNo(Convert.ToInt32(lb.CommandArgument), filterArray(0)("Orderid").ToString(), filterArray(0)("GdsPnr").ToString())

                Dim fltds As New DataSet()
                fltds = ST.GetTicketdIntl(Convert.ToInt32(lb.CommandArgument), filterArray(0)("PaxType").ToString())
                Dim newpaxid As String = lb.CommandArgument.Trim()
                If fltds.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso fltds.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
                    Paxds = OldPaxInfo(fltds.Tables(0).Rows(0)("ResuID").ToString(), fltds.Tables(0).Rows(0)("Title").ToString(), fltds.Tables(0).Rows(0)("FName").ToString(), fltds.Tables(0).Rows(0)("MName").ToString(), fltds.Tables(0).Rows(0)("LName").ToString(), fltds.Tables(0).Rows(0)("PaxType").ToString())
                    newpaxid = Paxds.Tables(0).Rows(0)("PaxId").ToString()

                End If


                fltds = ST.GetTicketdIntl(newpaxid, filterArray(0)("PaxType").ToString())
                Dim fltTD As DataTable = fltds.Tables(0)

                SecondStatus = CheckTktNo(Convert.ToInt32(newpaxid.Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))



                If SecondStatus = "0" Then
                    showPoup(lb)
                ElseIf SecondStatus = "Reissue request can not be accepted for past departure date." And lb.CommandName = "REFUND" Then
                    showPoup(lb)
                ElseIf (SecondStatus = "0" Or SecondStatus = "Reissue request can not be accepted for past departure date." Or SecondStatus = "Given ticket number is already ReIssued") Or FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And lb.CommandName = "Refund" Then
                    If FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And lb.CommandName = "REFUND" Then
                        Try
                            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
                            If page IsNot Nothing Then
                                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "HourDeparturePopup(" & lb.CommandArgument & ", '" & lb.CommandName & "', '" & filterArray(0)("GdsPnr").ToString() & "', '" & filterArray(0)("OrderId").ToString() & "');", True)
                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                        End Try
                    Else
                        showPoup(lb)
                    End If

                ElseIf FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And lb.CommandName = "REFUND" Then
                    Try
                        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
                        If page IsNot Nothing Then
                            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "HourDeparturePopup(" & lb.CommandArgument & ", '" & lb.CommandName & "', '" & filterArray(0)("GdsPnr").ToString() & "', '" & filterArray(0)("OrderId").ToString() & "');", True)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                Else

                    ''new change mt
                    '' If (SecondStatus = "Given ticket number is under process for Reissuance." Or SecondStatus = "Given ticket number is under process for Cancellation/Refund") Then
                    If (SecondStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly.") Then
                        ShowAlertMessage(SecondStatus)

                    Else
                        showPoup(lb)
                    End If
                End If


                'Select Case Status
                '    Case 1

                '    Case 2
                '        ShowAlertMessage("This Ticket Number is Pending for Refund")
                '    Case 3
                '        ShowAlertMessage("This Ticket Number is Allready in Refund InProcess")
                '    Case 4
                '        ShowAlertMessage("This Ticket Number Allready ReIssued")
                '    Case 5
                '        ShowAlertMessage("This Ticket Number is Pending for ReIssue")
                '    Case 6
                '        ShowAlertMessage("This Ticket Number is Allready in ReIssue InProcess")
                '    Case 0

                'End Select
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

  
    Public Sub showPoup(ByVal lb As LinkButton)
        Try
            Dim gridViewds As New DataSet()
            gridViewds = Session("Grdds")
            Dim filterArray As Array = gridViewds.Tables(0).Select("PaxId ='" & lb.CommandArgument & "'")
            If filterArray.Length > 0 Then
                If lb.CommandName = "REFUND" Then
                    CallScriptFunction_Refund("RefundPopUpLoadReport", lb.CommandArgument, lb.CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("OrderId").ToString())
                Else
                    CallScriptFunction_Refund("RefundPopUpLoadReport", lb.CommandArgument, lb.CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("OrderId").ToString())
                    ''CallScriptFunction("popupLoadReport", lb.CommandArgument, lb.CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("TicketNumber").ToString(), filterArray(0)("FName").ToString() & " " & filterArray(0)("LName").ToString(), filterArray(0)("PaxType").ToString())
                End If
                ' CallScriptFunction("popupLoad", lb.CommandArgument, lb.CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("TicketNumber").ToString(), filterArray(0)("FName").ToString() & " " & filterArray(0)("LName").ToString(), filterArray(0)("PaxType").ToString())

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btnRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemark.Click
        Try
            If (Request("RemarksType").Trim() = "REFUND") Then
                CancellationRemark(Request("txtPaxid").Trim(), Request("txtSectorid").Trim(), Request("txtOrderid").Trim(), Request("txtPNRNO").Trim())
            ElseIf (Request("RemarksType").Trim() = "REISSUE") Then
                Dim fltds As New DataSet()
                'Dim ObjIntDetails As New IntlDetails()
                'Select requested record for Reissue and Refund
                Dim OrderId As String = Request("txtOrderid").Trim()
                Dim PNR As String = Request("txtPNRNO").Trim()
                Dim Status As String = "0"
                Dim checkInsert As Boolean = False
                ''Dim i As Integer = 0
                Dim paxiddetails() As String = Request("txtPaxid").Trim().Split(",")
                Dim ObjIntDetails As New IntlDetails()
                Dim cancelstatus As String = "", TickeNo As String = ""
                'Checking Status in ReIssueIntl Table it present in table or not  txtSectorid
                'Status = "Reissue request can not be accepted for past departure date."
                For j As Integer = 0 To paxiddetails.Length - 1
                    Status = CheckTktNo(Convert.ToInt32(paxiddetails(j).Trim()), OrderId, PNR)
                    If (Status <> "0") Then
                        TickeNo = paxiddetails(j).Trim()
                        Exit For
                    End If
                Next

                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString()
                Dim objcacelDAL As New GALWS.FlightCancellationDAL(constr)
                ' paxdt = objcacelDAL.PaxDetailsDt(Request("txtPaxid").Trim(), paxdt)
                Dim Paxds As DataSet = ST.GetPaxDetails(OrderId)
                Dim PaxDtRow() As DataRow = Paxds.Tables(0).Select("PaxId=" + Request("txtPaxid").Replace(",", " or PaxID ="))
                'Select requested record for Reissue and Refund
                ''Dim fltds As New DataSet()
                fltds = ST.GetTicketdIntl(paxiddetails(0).Trim(), PaxDtRow(0)("PaxType").ToString())
                'fltds = ST.GetTicketdIntl(Request("txtPaxid").Trim(), Request("txtPaxType").Trim()) reissue cmt

                For j As Integer = 0 To paxiddetails.Length - 1
                    ''  If Status = "0" Or Status = "Reissue request can not be accepted for past departure date." Or Status = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." Then
                    If fltds.Tables(0).Rows.Count <> 0 Then
                        ''Dim fltTD As DataTable = fltds.Tables(0)
                        'Checking Status in ReIssueIntl Table it present in table or not
                        Status = CheckTktNo(Convert.ToInt32(paxiddetails(j).Trim()), OrderId, PNR)
                        Dim newpaxid As String = paxiddetails(j).Trim()

                        fltds = ST.GetTicketdIntl(newpaxid, PaxDtRow(j)("PaxType").ToString())
                        Dim fltTD As DataTable = fltds.Tables(0)

                        Status = CheckTktNo(Convert.ToInt32(newpaxid.Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))
                        '' Status = CheckTktNo(Convert.ToInt32(Request("txtPaxid").Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR")) reissue cmt
                        If Status = "0" Then
                            Dim i As Integer = 0
                            Dim txtremark As String = Request("txtRemark").Trim()
                            Dim projectID As String = If(IsDBNull(fltTD.Rows(0).Item("ProjectID")), Nothing, fltTD.Rows(0).Item("ProjectID").ToString().Trim())
                            Dim mgtFee As Decimal = If(IsDBNull(fltTD.Rows(0).Item("MgtFee")), 0, Convert.ToDecimal(fltTD.Rows(0).Item("MgtFee").ToString().Trim()))
                            Dim CancelledBy As String = If(DrpCancelledBy.Visible = True, If(DrpCancelledBy.SelectedValue.ToLower() <> "select", DrpCancelledBy.SelectedValue, Nothing), Nothing)
                            Dim BillNoAir As String = If(IsDBNull(fltTD.Rows(0).Item("BillNoCorp")), Nothing, fltTD.Rows(0).Item("BillNoCorp").ToString().Trim())
                            Dim BillNoCorp As String = Nothing
                            If (Request("RemarksType").Trim() = "REISSUE") Then
                                ''Insert data in ReIssueIntl Table 
                                i = ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), fltTD.Rows(0).Item("Sector"), fltTD.Rows(0).Item("Departure"), _
                                                        fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
                                                        fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalFare"), _
                                                        fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, _
                                                        fltTD.Rows(0).Item("BaseFare"), fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), _
                                                        fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), _
                                                        fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), _
                                                        fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), fltTD.Rows(0).Item("FNo"), txtremark, sttusobj.GetID("RISU"), _
                                                        Convert.ToInt32(newpaxid), fltTD.Rows(0).Item("Trip"), "R", StatusClass.Pending, projectID, mgtFee, CancelledBy, BillNoCorp, BillNoAir)
                                Try
                                    Dim dt As New DataTable()
                                    Dim strMailMsgHold As String
                                    strMailMsgHold = "<table>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><h2> Reissue Request </h2>"
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + fltTD.Rows(0).Item("OrderId")
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>PnrNo: </b>" + fltTD.Rows(0).Item("PNR")
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>Pax Name: </b>" + fltTD.Rows(0).Item("Title").ToString + " " + fltTD.Rows(0).Item("FName") + " " + fltTD.Rows(0).Item("LName")
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>Pax Type: </b>" + fltTD.Rows(0).Item("PaxType")
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>Net Amount: </b>" + Convert.ToString(fltTD.Rows(0).Item("TotalFareAfterDis"))
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "<tr>"
                                    strMailMsgHold = strMailMsgHold & "<td><b>Remark: </b>" + Request("txtRemark").Trim()
                                    strMailMsgHold = strMailMsgHold & "</td>"
                                    strMailMsgHold = strMailMsgHold & "</tr>"
                                    strMailMsgHold = strMailMsgHold & "</table>"
                                    dt = ObjIntDetails.Email_Credentilas(fltTD.Rows(0).Item("OrderId"), "ReIssue_REJECTED", newpaxid)
                                    Dim STDOM As New SqlTransactionDom
                                    Dim MailDt As New DataTable
                                    MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)
                                    Dim AgencyDs As DataSet = ST.GetAgencyDetails(Session("UID"))
                                    Try
                                        If (MailDt.Rows.Count > 0) Then

                                            Try
                                                'Send Mail Agent
                                                If (AgencyDs.Tables(0).Rows.Count > 0) Then
                                                    STDOM.SendMail(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Reissue Request", "")
                                                End If
                                            Catch ex As Exception
                                                clsErrorLog.LogInfo(ex)
                                            End Try
                                            For v As Integer = 0 To dt.Rows.Count - 1
                                                STDOM.SendMail(dt.Rows(j)(1).ToString(), Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Reissue Request", "")
                                                '' STDOM.SendMail(dt.Rows(v)(1).ToString(), "info@itztravel.itzcash.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Reissue Request", "")
                                            Next
                                        End If
                                    Catch ex As Exception
                                        clsErrorLog.LogInfo(ex)
                                    End Try
                                Catch ex As Exception
                                End Try
                            End If
                            If i > 0 Then
                                checkInsert = True
                                ''ShowAlertMessage(Request("RemarksType") & " Remark Submitted Succesfully")
                            Else
                                '' ShowAlertMessage("Try later")
                            End If
                            ''CallScriptFunction("ResetInputTextvalue", " ", " ", " ", " ", " ", " ")
                            'End Select
                        End If
                    End If
                Next
                If checkInsert = True Then
                    ShowAlertMessage(Request("RemarksType") & " Remark Submitted Succesfully")
                Else
                    ShowAlertMessage("Try later")
                End If

                If Status = "0" Then
                    CallScriptFunction("ResetInputTextvalue", " ", " ", " ", " ", " ", " ")
                    ''End Select
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Shared Sub CallScriptFunction(ByVal funName As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String, ByVal Parameter5 As String, ByVal Parameter6 As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", funName & "('" & Parameter1 & "', '" & Parameter2 & "', '" & Parameter3 & "', '" & Parameter4 & "', '" & Parameter5 & "', '" & Parameter6 & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ticket_grdview.RowCommand
        Try
            If e.CommandName = "REISSUE" Then
                ViewState("PaxId") = e.CommandArgument
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                ViewState("RowIndex") = RowIndex

                gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF275")
            ElseIf e.CommandName = "REFUND" Then
                ViewState("PaxId") = e.CommandArgument
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                Dim RowIndex As Integer = gvr.RowIndex
                ViewState("RowIndex") = RowIndex
                gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
            Else

                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)

                gvr.BackColor = Nothing
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Protected Sub btnReIssue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Filtering one record for Reissue 
    '    Try
    '        grdds = Session("Grdds")
    '        Dim filterArray As Array = grdds.Tables(0).Select("PaxId ='" & ViewState("PaxId").ToString & "'")
    '        Dim PaxType As String = filterArray(0)("PaxType").ToString()
    '        fltds.Clear()
    '        fltds = ST.GetTicketdIntl(ViewState("PaxId"), PaxType)

    '        If fltds.Tables(0).Rows.Count <> 0 Then
    '            Dim fltTD As DataTable = fltds.Tables(0)
    '            'Checking Status in ReIssueIntl Table it present in table or not
    '            Dim Status As Integer = ST.CheckTktNo(Convert.ToInt32(ViewState("PaxId")), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))                              'ST.CheckReIssueIntlPNR(ds.Tables(0).Rows(0).Item("TicketNo"))
    '            Select Case Status
    '                Case 1
    '                    ShowAlertMessage("This Ticket Number Allready Refunded")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No Allready Refunded'); ", True)
    '                Case 2
    '                    ShowAlertMessage("This Ticket Number is Pending for Refund")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Pending for Refund'); ", True)
    '                Case 3
    '                    ShowAlertMessage("This Ticket Number is Allready in Refund InProcess")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Allready in Cancelletion InProcess'); ", True)
    '                Case 4
    '                    ShowAlertMessage("This Ticket Number Allready ReIssued")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No Allready ReIssued'); ", True)
    '                Case 5
    '                    ShowAlertMessage("This Ticket Number is Pending for ReIssue")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Pending for ReIssue'); ", True)
    '                Case 6
    '                    ShowAlertMessage("This Ticket Number is Allready in ReIssue InProcess")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Allready in ReIssue InProcess'); ", True)
    '                Case Else
    '                    Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
    '                    Dim ReIssueID As String = sttusobj.GetID("RISU")
    '                    Dim txtremark As TextBox = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '                    Dim projectID As String = If(IsDBNull(fltTD.Rows(0).Item("ProjectID")), Nothing, fltTD.Rows(0).Item("ProjectID").ToString().Trim())
    '                    Dim mgtFee As Decimal = If(IsDBNull(fltTD.Rows(0).Item("MgtFee")), 0, Convert.ToDecimal(fltTD.Rows(0).Item("MgtFee").ToString().Trim()))

    '                    'Insert data in ReIssueIntl Table
    '                    ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), fltTD.Rows(0).Item("Sector"), fltTD.Rows(0).Item("Departure"), _
    '                                            fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
    '                                            fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalFare"), _
    '                                            fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, _
    '                                            fltTD.Rows(0).Item("BaseFare"), fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), _
    '                                            fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), _
    '                                            fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), _
    '                                            fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), fltTD.Rows(0).Item("FNo"), txtremark.Text, ReIssueID, _
    '                                            Convert.ToInt32(ViewState("PaxId")), fltTD.Rows(0).Item("Trip"), "R", StatusClass.Pending, projectID, mgtFee)
    '                    ShowAlertMessage("ReIssue Remark Submitted Succesfully")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('ReIssue Remark Submitted Succesfully'); ", True)
    '            End Select
    '        End If

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
    'Protected Sub btnRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Filtering one record for Refund
    '    Try
    '        grdds = Session("Grdds")
    '        Dim filterArray As Array = grdds.Tables(0).Select("PaxId ='" & ViewState("PaxId").ToString & "'")
    '        Dim PaxType As String = filterArray(0)("PaxType").ToString()
    '        fltds.Clear()
    '        fltds = ST.GetTicketdIntl(ViewState("PaxId"), PaxType)

    '        If fltds.Tables(0).Rows.Count <> 0 Then
    '            Dim fltTD As DataTable = fltds.Tables(0)
    '            'Checking in CancelletionIntl Table it present in table or not
    '            Dim status As Integer = ST.CheckTktNo(Convert.ToInt32(ViewState("PaxId")), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))                                      'ST.CheckCancelIntlPNR(ds.Tables(0).Rows(0).Item("TicketNo"))
    '            Select Case status
    '                Case 1
    '                    ShowAlertMessage("This Ticket Number Allready Refunded")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No Allready Refunded'); ", True)
    '                Case 2
    '                    ShowAlertMessage("This Ticket Number is Pending for Refund")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Pending for Refund'); ", True)
    '                Case 3
    '                    ShowAlertMessage("This Ticket Number is Allready in Cancelletion InProcess")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Allready in Cancelletion InProcess'); ", True)
    '                Case 4
    '                    ShowAlertMessage("This Ticket Number Allready ReIssued")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No Allready ReIssued'); ", True)
    '                Case 5
    '                    ShowAlertMessage("This Ticket Number is Pending for ReIssue")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Pending for ReIssue'); ", True)
    '                Case 6
    '                    ShowAlertMessage("This Ticket Number is Allready in ReIssue InProcess")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Allready in ReIssue InProcess'); ", True)
    '                Case Else
    '                    Dim RefundID As String = sttusobj.GetID("RFND")
    '                    Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
    '                    Dim txtremark As TextBox = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '                    Dim projectID As String = If(IsDBNull(fltTD.Rows(0).Item("ProjectID")), Nothing, fltTD.Rows(0).Item("ProjectID").ToString().Trim())
    '                    Dim mgtFee As Decimal = If(IsDBNull(fltTD.Rows(0).Item("MgtFee")), 0, Convert.ToDecimal(fltTD.Rows(0).Item("MgtFee").ToString().Trim()))

    '                    'Insert data in CancellationIntl Table
    '                    ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), fltTD.Rows(0).Item("Sector"), fltTD.Rows(0).Item("Departure"), _
    '                                            fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
    '                                            fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalBookingCost"), _
    '                                            fltTD.Rows(0).Item("TotalAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, fltTD.Rows(0).Item("BaseFare"), _
    '                                            fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), _
    '                                            fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), _
    '                                            fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), _
    '                                            fltTD.Rows(0).Item("FNo"), txtremark.Text, RefundID, Convert.ToInt32(ViewState("PaxId")), fltTD.Rows(0).Item("Trip"), "C", StatusClass.Pending, projectID, mgtFee)
    '                    ShowAlertMessage("Refund Remark Submitted Succesfully")
    '                    'Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Refund Remark Submitted Succesfully'); ", True)
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
    'Protected Sub lnkHides_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        ticket_grdview.Columns(16).Visible = False
    '        ticket_grdview.Columns(17).Visible = False

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
    'Filtering data from Search click
    Public Sub CheckEmptyValue()
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If


            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim trip As String = "" ''''IIf(Session("User_Type") = "EXEC", IIf([String].IsNullOrEmpty(Session("TripExec")), "", Session("TripExec").ToString().Trim()), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))
            If Session("User_Type") = "EXEC" Then
                If [String].IsNullOrEmpty(Session("TripExec")) Then
                    trip = ""
                Else
                    trip = Session("TripExec").ToString().Trim()
                End If
            Else
                trip = If([String].IsNullOrEmpty(ddlTripDomIntl.SelectedItem.Value), "", ddlTripDomIntl.SelectedItem.Value.Trim())
            End If
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName & "_" & DropDownListDate.SelectedItem.Value.Trim, TicketNo, AirPNR, AgentID, trip.Trim(), StatusClass.Ticketed)
            BindGrid(grdds)

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            CheckEmptyValue()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub ticket_grdview_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ticket_grdview.PageIndexChanging
        Try
            ticket_grdview.PageIndex = e.NewPageIndex
            BindGrid(Session("Grdds"))

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "jAlert('" & [error] & "', 'Alert');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            'Dim AgentID As String
            'If ddl_AgencyName.SelectedIndex > 0 Then
            '    AgentID = ddl_AgencyName.SelectedValue
            'Else
            '    AgentID = Nothing
            'End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")), "", Request("hidtxtAgencyName"))
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim trip As String = "" ''''IIf(Session("User_Type") = "EXEC", IIf([String].IsNullOrEmpty(Session("TripExec")), "", Session("TripExec").ToString().Trim()), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))
            If Session("User_Type") = "EXEC" Then
                If [String].IsNullOrEmpty(Session("TripExec")) Then
                    trip = ""
                Else
                    trip = Session("TripExec").ToString().Trim()
                End If
            Else
                trip = If([String].IsNullOrEmpty(ddlTripDomIntl.SelectedItem.Value), "", ddlTripDomIntl.SelectedItem.Value.Trim())
            End If
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, AirPNR, AgentID, trip.Trim(), StatusClass.Ticketed)
            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then

                STDom.ExportData(grdds)
            Else
                Dim StrDataTable As New DataTable
                If grdds.Tables(0).Columns.Contains("PartnerName") = True Then

                    StrDataTable = grdds.Tables(0)
                    StrDataTable.Columns.Remove("PartnerName")
                    StrDataTable.Columns.Remove("Provider")
                    STDom.ExportData(grdds)
                End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Function GetVisibleStatus() As String

        If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Or Session("User_Type").ToString = "ACC" Then

            Return True

        Else
            Return False

        End If
    End Function
    Public Function CheckTktNo(ByVal PaxId As Integer, ByVal OrderId As String, ByVal PNR As String) As String
        Dim cmd As New SqlCommand()
        Dim ErrorMsg As String = ""
        Try
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            cmd.CommandText = "CheckTktNo_New"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@PaxId", SqlDbType.VarChar).Value = PaxId
            cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = OrderId
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNR
            cmd.Connection = con1
            con1.Open()
            ErrorMsg = cmd.ExecuteScalar()
            cmd.Dispose()
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        Return ErrorMsg
    End Function

    Protected Sub CancellationRemark(ByVal txtPaxid As String, ByVal Sector As String, ByVal OrderId As String, ByVal PNR As String)
        Try
            Dim i As Integer = 0
            Dim paxiddetails() As String = Request("txtPaxid").Trim().Split(",")
            Dim ObjIntDetails As New IntlDetails()
            Dim cancelstatus As String = "", TickeNo As String = "", Status As String = "0"
            'Checking Status in ReIssueIntl Table it present in table or not  txtSectorid
            'Status = "Reissue request can not be accepted for past departure date."
            For j As Integer = 0 To paxiddetails.Length - 1
                Status = CheckTktNo(Convert.ToInt32(paxiddetails(j).Trim()), OrderId, PNR)
                If (Status <> "0") Then
                    TickeNo = paxiddetails(j).Trim()
                    Exit For
                End If
            Next

            '  If Status = "0" Then
            Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString()
            Dim objcacelDAL As New GALWS.FlightCancellationDAL(constr)
            ' paxdt = objcacelDAL.PaxDetailsDt(Request("txtPaxid").Trim(), paxdt)
            Dim Paxds As DataSet = ST.GetPaxDetails(OrderId)
            Dim PaxDtRow() As DataRow = Paxds.Tables(0).Select("PaxId=" + Request("txtPaxid").Replace(",", " or PaxID ="))
            'Select requested record for Reissue and Refund
            Dim fltds As New DataSet()
            fltds = ST.GetTicketdIntl(paxiddetails(0).Trim(), PaxDtRow(0)("PaxType").ToString())
            For j As Integer = 0 To paxiddetails.Length - 1
                Status = CheckTktNo(Convert.ToInt32(paxiddetails(j).Trim()), OrderId, PNR)
                If Status = "0" Or Status = "Reissue request can not be accepted for past departure date." Or Status = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." Then
                    If fltds.Tables(0).Rows.Count <> 0 Then
                        ''new code
                        Dim BillNoAir As String = ""
                        Dim newpaxid As String = paxiddetails(j).Trim()
                        If fltds.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso fltds.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
                            Paxds = OldPaxInfo(fltds.Tables(0).Rows(0)("ResuID").ToString(), PaxDtRow(j)("Title").ToString(), PaxDtRow(j)("FName").ToString(), PaxDtRow(j)("MName").ToString(), PaxDtRow(j)("LName").ToString(), PaxDtRow(j)("PaxType").ToString())
                            newpaxid = Paxds.Tables(0).Rows(0)("PaxId").ToString()
                            BillNoAir = fltds.Tables(0).Rows(0)("ResuID").ToString()
                        End If
                        '' newcode end
                        'Select requested record for Reissue and Refund 
                        fltds = ST.GetTicketdIntl(newpaxid, PaxDtRow(j)("PaxType").ToString())
                        Dim fltTD As DataTable = fltds.Tables(0)

                        Status = CheckTktNo(Convert.ToInt32(newpaxid.Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))



                        If Status = "0" Or Status = "Reissue request can not be accepted for past departure date." Or Status = "Given ticket number is already ReIssued" Or Status = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." Then

                            Dim projectID As String = If(IsDBNull(fltTD.Rows(0).Item("ProjectID")), Nothing, fltTD.Rows(0).Item("ProjectID").ToString().Trim())
                            Dim mgtFee As Decimal = If(IsDBNull(fltTD.Rows(0).Item("MgtFee")), 0, Convert.ToDecimal(fltTD.Rows(0).Item("MgtFee").ToString().Trim()))
                            Dim CancelledBy As String = If(DrpCancelledBy.Visible = True, If(DrpCancelledBy.SelectedValue.ToLower() <> "select", DrpCancelledBy.SelectedValue, Nothing), Nothing)
                            '' Dim BillNoAir As String = If(IsDBNull(fltTD.Rows(0).Item("BillNoCorp")), Nothing, fltTD.Rows(0).Item("BillNoCorp").ToString().Trim())
                            Dim BillNoCorp As String = Nothing
                            Dim RefundID As String = sttusobj.GetID("RFND")
                            If (Session("IsCorp") = True) Then
                                BillNoCorp = clsCorp.GenerateBillNoCorp("CN")
                            Else
                                CancelledBy = Nothing
                            End If



                            i = ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), Sector, fltTD.Rows(0).Item("Departure"), _
                                                    fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
                                                    fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalBookingCost"), _
                                                    fltTD.Rows(0).Item("TotalAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, fltTD.Rows(0).Item("BaseFare"), _
                                                    fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), _
                                                    fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), _
                                                    fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), _
                                                  fltTD.Rows(0).Item("FNo"), Request("txtRemark").Trim(), RefundID, Convert.ToInt32(newpaxid.Trim()), fltTD.Rows(0).Item("Trip"), "C", StatusClass.Pending, projectID, mgtFee, CancelledBy, BillNoCorp, BillNoAir)
                        End If
                    End If
                End If
            Next
            Try
                If Status = "0" Then
                    If fltds.Tables(0).Rows(0).Item("PartnerName").ToString() = "TB" Then
                        Dim logds As New DataSet()
                        logds = objcacelDAL.Fltlogds(OrderId, logds)

                        Dim objSql As New SqlTransactionNew
                        ''Dim dsCrd As DataSet = objSql.GetCredentials("TB")
                        Dim dsCrd As DataSet = objSql.GetCredentials("TB", "", "")
                        Dim objcacellation As New GALWS.TBO.TBOCancellation()
                        cancelstatus = objcacellation.TBOFightCancellation(dsCrd, logds.Tables(1).Rows(0)("BookRes").ToString(), fltds.Tables(0).Rows(0).Item("TicketNo").ToString(), Request("txtRemark").Trim(), "Cancel", constr, PaxDtRow, Sector)

                    ElseIf fltds.Tables(0).Rows(0).Item("PartnerName").ToString() = "1G" Then
                        Dim objgdscancel As New GALWS.GDSCancellation()
                        'cancelstatus = objgdscancel.GDSFightCancellation(fltds.Tables(0).Rows(0).Item("PNR").ToString(), fltds.Tables(0).Rows(0).Item("TicketNo").ToString(), constr, PaxDtRow, Sector)
                        cancelstatus = "Failed"
                    End If

                    'SendMail_Refund("Refund Request", fltds.Tables(0), cancelstatus, PaxDtRow, Sector)
                End If
            Catch ex As Exception
                LogInfo(ex)
            End Try
            'Travel journey is passed, we process no show refund.
            If i > 0 And cancelstatus = "Cancelled" Then
                SendMail_Refund("Refund Request", fltds.Tables(0), cancelstatus, PaxDtRow, Sector)
                ShowAlertMessage("Cancellation/Refund request is submitted successfully. The refund will process up to 7 working days.")
            ElseIf i > 0 And cancelstatus = "Faild" Then
                SendMail_Refund("Refund Request", fltds.Tables(0), cancelstatus, PaxDtRow, Sector)
                ShowAlertMessage("Cancellation/Refund request is submitted successfully.")
            ElseIf i > 0 And cancelstatus = "" Then
                SendMail_Refund("Refund Request", fltds.Tables(0), cancelstatus, PaxDtRow, Sector)
                If Status = "Reissue request can not be accepted for past departure date." Then
                    ShowAlertMessage("Cancellation/Refund request is submitted successfully. Please note that journey date is already passed and hence NO SHOW refund will be processed. NO SHOW refund may take 7 working days to get processed.")
                Else
                    ShowAlertMessage("Cancellation/Refund request is submitted successfully.")
                End If
            Else
                ShowAlertMessage("Please contact help desk regarding Cancellation/Refund.")
            End If
            CallScriptFunction("ResetInputTextvalue", " ", " ", " ", " ", " ", " ")
            'End Select
            'Else
            '    ShowAlertMessage(TickeNo + " " + Status + ". Please select valid ticket nubmer  for ReIssue or Refund.")
            ' End If
        Catch ex As Exception
            LogInfo(ex)
        End Try
    End Sub
    Protected Sub SendMail_Refund(ByVal Modules As String, ByVal fltTD As DataTable, ByVal caclestatus As String, ByVal paxdt() As DataRow, ByVal Sector As String)
        Dim ObjIntDetails As New IntlDetails()
        Try
            Dim mailds As New DataSet()
            Dim strMailMsgHold As String
            strMailMsgHold = "<table>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><h2> Cancellation/Refund Request </h2>"
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + fltTD.Rows(0).Item("OrderId")
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>PnrNo: </b>" + fltTD.Rows(0).Item("PNR")
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"

            For j As Integer = 0 To paxdt.Length - 1
                strMailMsgHold = strMailMsgHold & "<tr>"
                strMailMsgHold = strMailMsgHold & "<td><b>Pax Name: </b>" + paxdt(j).Item("Title").ToString + " " + paxdt(j).Item("FName") + " " + paxdt(j).Item("LName")
                strMailMsgHold = strMailMsgHold & "</td>"
                strMailMsgHold = strMailMsgHold & "</tr>"
                strMailMsgHold = strMailMsgHold & "<tr>"
                strMailMsgHold = strMailMsgHold & "<td><b>Pax Type: </b>" + paxdt(j).Item("PaxType")
                strMailMsgHold = strMailMsgHold & "</td>"
                strMailMsgHold = strMailMsgHold & "</tr>"

            Next

            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Sector: </b>" + Sector
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Net Amount: </b>" + Convert.ToString(fltTD.Rows(0).Item("TotalFareAfterDis"))
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Remark: </b>" + Request("txtRemark").Trim()
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            'Cancellation/Refund request is submitted successfully. The refund will process up to 7 working days.
            strMailMsgHold = strMailMsgHold & "<tr>"
            If caclestatus = "Cancelled" Then
                strMailMsgHold = strMailMsgHold & "<td><b>Cancel Status: </b> Flight booking has been canceled successfully. The refund will process up to 7 working days.</td>"
            Else
                strMailMsgHold = strMailMsgHold & "<td><b>Cancel Status: </b> Cancellation/Refund request is submitted successfully. The refund will process up to 7 working days.</td>"
            End If
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "</table>"

            Dim STDOM As New SqlTransactionDom
            Dim EDt As DataTable = ObjIntDetails.Email_Credentilas(fltTD.Rows(0).Item("OrderId"), "Refund_REJECTED", paxdt(0).Item("PaxId").ToString())
            Dim MailDt As DataTable = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)
            Dim AgencyDs As DataSet = ST.GetAgencyDetails(Session("UID"))
            Try
                If (MailDt.Rows.Count > 0) Then


                    Try
                        'Send Mail Agent
                        If (AgencyDs.Tables(0).Rows.Count > 0) Then
                            STDOM.SendMail(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Refund Request", "")
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try

                    For k As Integer = 0 To EDt.Rows.Count - 1
                        STDOM.SendMail(EDt.Rows(k)(1).ToString(), Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Refund Request", "")
                    Next
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Shared Sub CallScriptFunction_Refund(ByVal funName As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", funName & "('" & Parameter1 & "', '" & Parameter2 & "', '" & Parameter3 & "', '" & Parameter4 & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub LogInfo(ByVal ex As Exception)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim cmd As SqlCommand
        Dim Temp As Integer = 0
        Try
            Dim trace As New System.Diagnostics.StackTrace(ex, True)

            Dim linenumber As Integer = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
            Dim ErrorMsg As String = ex.Message
            Dim fileNames As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
            con.Open()
            cmd = New SqlCommand("InsertErrorLog", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PageName", fileNames)
            cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMsg)
            cmd.Parameters.AddWithValue("@LineNumber", linenumber)
            Temp = cmd.ExecuteNonQuery()
        Catch ex1 As Exception
        Finally
            con.Close()
        End Try
    End Sub

    Protected Function OldPaxInfo(ByVal reissueid As String, ByVal Title As String, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal PaxType As String) As DataSet
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)

        Dim Paxds As New DataSet()
        ''SP_GetOldPaxDetails(@reissueid varchar(50), @Title varchar(20), @FName varchar(50), @MName varchar(50), @LName varchar(50), @PaxType varchar(20))
        Try
            con.Open()
            Dim adap As New SqlDataAdapter("SP_GetOldPaxDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@reissueid", reissueid)
            adap.SelectCommand.Parameters.AddWithValue("@Title", Title)
            adap.SelectCommand.Parameters.AddWithValue("@FName", FName)
            adap.SelectCommand.Parameters.AddWithValue("@MName", MName)
            adap.SelectCommand.Parameters.AddWithValue("@LName", LName)
            adap.SelectCommand.Parameters.AddWithValue("@PaxType", PaxType)
            adap.Fill(Paxds)

        Catch ex1 As Exception
            clsErrorLog.LogInfo(ex1)
        Finally
            con.Close()
        End Try
        Return Paxds
    End Function

    Public Function USP_GetTicketDetail_Intl_calender(ByVal ddate As String, ByVal Agent_id As String, ByVal loginid As String, ByVal user_Type As String, ByVal ddmonth As String, ByVal ddyear As String, ByVal VPNR As String, ByVal VPAXNAME As String, ByVal SDEPCITY As String, ByVal SARRCITY As String, ByVal vc As String) As DataSet
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@Calender_Date", ddate)
        paramHashtable.Add("@AgentId", Agent_id)
        paramHashtable.Add("@LoginID", loginid)
        paramHashtable.Add("@usertype", user_Type)
        paramHashtable.Add("@month", ddmonth)
        paramHashtable.Add("@year", ddyear)
        paramHashtable.Add("@PNR", VPNR)
        paramHashtable.Add("@PaxName", VPAXNAME)
        paramHashtable.Add("@Source", SDEPCITY)
        paramHashtable.Add("@destination", SARRCITY)
        paramHashtable.Add("@AirlinePNR", vc)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GetTicketDetail_Intl_CalenderN", 3)
    End Function

End Class
