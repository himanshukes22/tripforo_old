Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Partial Class Reports_TicketReportIntl
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()
    Private sttusobj As New Status()
    Dim con As New SqlConnection()
    Dim PaxType As String
    Dim clsCorp As New ClsCorporate()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try
            Dim AgentID As String = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
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

                Else
                    'AgencyDDLDS = ST.GetAgencyDetailsDDL()
                    'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                    '    ' Bind Agency DDL
                    '    Try
                    '        If (Session("user_type") = "SALES") Then
                    '            Dim dtag As New DataTable
                    '            dtag = STDom.getAgencybySalesRef(Session("UID").ToString).Tables(0)
                    '            ddl_AgencyName.AppendDataBoundItems = True
                    '            ddl_AgencyName.Items.Clear()
                    '            ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    '            ddl_AgencyName.DataSource = dtag
                    '            ddl_AgencyName.DataTextField = "Agency_Name"
                    '            ddl_AgencyName.DataValueField = "user_id"
                    '            ddl_AgencyName.DataBind()
                    '        Else

                    '            ddl_AgencyName.AppendDataBoundItems = True
                    '            ddl_AgencyName.Items.Clear()
                    '            ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    '            ddl_AgencyName.DataSource = AgencyDDLDS
                    '            ddl_AgencyName.DataTextField = "Agency_Name"
                    '            ddl_AgencyName.DataValueField = "user_id"
                    '            ddl_AgencyName.DataBind()
                    '        End If


                    '    Catch ex As Exception

                    '        clsErrorLog.LogInfo(ex)

                    '    End Try
                    'End If
                End If

                Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                Dim curr_date1 = Now()
                grdds.Clear()
                grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, curr_date, curr_date1, "", "", "", "", "", AgentID, "I", StatusClass.Ticketed)
                ' BindGrid(grdds)
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
                    lbl_Total.Text = sum.ToString
                End If
                lbl_counttkt.Text = dt.Rows.Count
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

            If Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "SALES" Then
                ticket_grdview.Columns(14).Visible = False
                ticket_grdview.Columns(15).Visible = False
                '    ticket_grdview.Columns(16).Visible = False
                '    ticket_grdview.Columns(17).Visible = False
                'Else
                '    ticket_grdview.Columns(16).Visible = False
                '    ticket_grdview.Columns(17).Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub lnkreissue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
        gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF275")

        showPoup(lb)
    End Sub

    Protected Sub lnkrefund_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
        gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")

        showPoup(lb)
    End Sub
    Protected Sub btnRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemark.Click
        Try
            Dim fltds As New DataSet()
            'Select requested record for Reissue and Refund
            fltds = ST.GetTicketdIntl(Request("txtPaxid").Trim(), Request("txtPaxType").Trim())
            If fltds.Tables(0).Rows.Count <> 0 Then
                Dim fltTD As DataTable = fltds.Tables(0)
                'Checking Status in ReIssueIntl Table it present in table or not
                Dim Status As Integer = ST.CheckTktNo(Convert.ToInt32(Request("txtPaxid").Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))
                Select Case Status
                    Case 1
                        ShowAlertMessage("This Ticket Number Allready Refunded")
                    Case 2
                        ShowAlertMessage("This Ticket Number is Pending for Refund")
                    Case 3
                        ShowAlertMessage("This Ticket Number is Allready in Refund InProcess")
                    Case 4
                        ShowAlertMessage("This Ticket Number Allready ReIssued")
                    Case 5
                        ShowAlertMessage("This Ticket Number is Pending for ReIssue")
                    Case 6
                        ShowAlertMessage("This Ticket Number is Allready in ReIssue InProcess")
                    Case Else
                        Dim i As Integer = 0

                        Dim txtremark As String = Request("txtRemark").Trim()
                        Dim projectID As String = If(IsDBNull(fltTD.Rows(0).Item("ProjectID")), Nothing, fltTD.Rows(0).Item("ProjectID").ToString().Trim())
                        Dim mgtFee As Decimal = If(IsDBNull(fltTD.Rows(0).Item("MgtFee")), 0, Convert.ToDecimal(fltTD.Rows(0).Item("MgtFee").ToString().Trim()))
                        Dim CancelledBy As String = If(DrpCancelledBy.Visible = True, If(DrpCancelledBy.SelectedValue.ToLower() <> "select", DrpCancelledBy.SelectedValue, Nothing), Nothing)
                        Dim BillNoAir As String = If(IsDBNull(fltTD.Rows(0).Item("BillNoCorp")), Nothing, fltTD.Rows(0).Item("BillNoCorp").ToString().Trim())
                        Dim BillNoCorp As String = Nothing
                        If (Request("RemarksType").Trim() = "Reissue") Then
                            Dim ReIssueID As String = sttusobj.GetID("RISU")
                            'Insert data in ReIssueIntl Table
                            '  i = ST.InsReIssueCancelIntl(fltTD, Convert.ToInt32(Request("txtPaxid").Trim()), ReIssueID, Request("txtRemark").Trim(), "R", StatusClass.Pending)


                            'Insert data in ReIssueIntl Table
                            i = ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), fltTD.Rows(0).Item("Sector"), fltTD.Rows(0).Item("Departure"), _
                                                    fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
                                                    fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalFare"), _
                                                    fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, _
                                                    fltTD.Rows(0).Item("BaseFare"), fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), _
                                                    fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), _
                                                    fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), _
                                                    fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), fltTD.Rows(0).Item("FNo"), txtremark, ReIssueID, _
                                                    Convert.ToInt32(Request("txtPaxid").Trim()), fltTD.Rows(0).Item("Trip"), "R", StatusClass.Pending, projectID, mgtFee, CancelledBy, BillNoCorp, BillNoAir)

                        ElseIf (Request("RemarksType").Trim() = "Refund") Then
                            Dim RefundID As String = sttusobj.GetID("RFND")
                            'Insert data in ReIssueIntl Table
                            ' i = ST.InsReIssueCancelIntl(fltTD, Convert.ToInt32(Request("txtPaxid").Trim()), RefundID, Request("txtRemark").Trim(), "C", StatusClass.Pending)
                            'Insert data in CancellationIntl Table

                            'BillNoCorp = clsCorp.GenerateBillNoCorp("CN")
                            If (Session("IsCorp") = True) Then
                                BillNoCorp = clsCorp.GenerateBillNoCorp("CN")
                            Else
                                CancelledBy = Nothing
                            End If

                            i = ST.InsReIssueCancelIntl(fltTD.Rows(0).Item("PNR"), fltTD.Rows(0).Item("TicketNo"), fltTD.Rows(0).Item("Sector"), fltTD.Rows(0).Item("Departure"), _
                                                    fltTD.Rows(0).Item("Destination"), fltTD.Rows(0).Item("Title").ToString, fltTD.Rows(0).Item("FName"), fltTD.Rows(0).Item("LName"), _
                                                    fltTD.Rows(0).Item("PaxType"), fltTD.Rows(0).Item("BookinfDate"), fltTD.Rows(0).Item("DepartDate"), fltTD.Rows(0).Item("TotalBookingCost"), _
                                                    fltTD.Rows(0).Item("TotalAfterDis"), fltTD.Rows(0).Item("UserID"), fltTD.Rows(0).Item("AgencyName").ToString, fltTD.Rows(0).Item("BaseFare"), _
                                                    fltTD.Rows(0).Item("Tax"), fltTD.Rows(0).Item("YQ"), fltTD.Rows(0).Item("STax"), fltTD.Rows(0).Item("TFee"), fltTD.Rows(0).Item("Dis"), _
                                                    fltTD.Rows(0).Item("CB"), fltTD.Rows(0).Item("TDS"), fltTD.Rows(0).Item("TotalFare"), fltTD.Rows(0).Item("TotalFareAfterDis"), _
                                                    fltTD.Rows(0).Item("VC"), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("DepTime"), fltTD.Rows(0).Item("AirlinePnr"), _
                                                    fltTD.Rows(0).Item("FNo"), txtremark, RefundID, Convert.ToInt32(Request("txtPaxid").Trim()), fltTD.Rows(0).Item("Trip"), "C", StatusClass.Pending, projectID, mgtFee, CancelledBy, BillNoCorp, BillNoAir)


                        End If
                        If i > 0 Then
                            ShowAlertMessage(Request("RemarksType") & " Remark Submitted Succesfully")
                        Else
                            ShowAlertMessage("Try later")
                        End If
                        CallScriptFunction("ResetInputTextvalue", " ", " ", " ", " ", " ", " ")
                End Select
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
    Public Sub showPoup(ByVal lb As LinkButton)
        Try
            Dim gridViewds As New DataSet()
            gridViewds = Session("Grdds")
            Dim filterArray As Array = gridViewds.Tables(0).Select("PaxId ='" & lb.CommandArgument & "'")
            If filterArray.Length > 0 Then

                CallScriptFunction("popupLoad", lb.CommandArgument, lb.CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("TicketNumber").ToString(), filterArray(0)("FName").ToString() & " " & filterArray(0)("LName").ToString(), filterArray(0)("PaxType").ToString())

                ' ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "popupLoad();", True)



            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    'Protected Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ticket_grdview.RowCommand
    '    Try
    '        If e.CommandName = "Reissue" Then
    '            ViewState("PaxId") = e.CommandArgument
    '            ticket_grdview.Columns(16).Visible = True
    '            ticket_grdview.Columns(17).Visible = True
    '            Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
    '            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
    '            Dim RowIndex As Integer = gvr.RowIndex
    '            ViewState("RowIndex") = RowIndex
    '            Dim btnReIssue As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnReIssue"), LinkButton)
    '            Dim btnremark As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnRemark"), LinkButton)
    '            Dim lblReIssue As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblReIssue"), Label)
    '            Dim lblRefund As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblRefund"), Label)
    '            Dim txtremark As TextBox = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '            Dim lnkHides As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
    '            txtremark.Visible = True
    '            lnkHides.Visible = True
    '            btnremark.Visible = False
    '            btnReIssue.Visible = True
    '            lblReIssue.Visible = True
    '            lblRefund.Visible = False
    '            txtremark.Focus()
    '            gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF275")
    '        ElseIf e.CommandName = "Refund" Then
    '            ViewState("PaxId") = e.CommandArgument
    '            ticket_grdview.Columns(16).Visible = True
    '            ticket_grdview.Columns(17).Visible = True
    '            Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
    '            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
    '            Dim RowIndex As Integer = gvr.RowIndex
    '            ViewState("RowIndex") = RowIndex
    '            Dim btnReIssue As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnReIssue"), LinkButton)
    '            Dim btnremark As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnRemark"), LinkButton)
    '            Dim lblReIssue As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblReIssue"), Label)
    '            Dim lblRefund As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblRefund"), Label)
    '            Dim txtremark As TextBox = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '            Dim lnkHides As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
    '            txtremark.Visible = True
    '            lnkHides.Visible = True
    '            btnremark.Visible = True
    '            btnReIssue.Visible = False
    '            lblReIssue.Visible = False
    '            lblRefund.Visible = True
    '            txtremark.Focus()
    '            gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
    '        Else
    '            ticket_grdview.Columns(16).Visible = False
    '            ticket_grdview.Columns(17).Visible = False
    '            Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
    '            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
    '            Dim RowIndex As Integer = gvr.RowIndex
    '            Dim btnReIssue As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnReIssue"), LinkButton)
    '            Dim btnremark As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("btnRemark"), LinkButton)
    '            Dim lblReIssue As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblReIssue"), Label)
    '            Dim lblRefund As Label = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lblRefund"), Label)
    '            Dim txtremark As TextBox = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '            Dim lnkHides As LinkButton = DirectCast(ticket_grdview.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
    '            txtremark.Visible = False
    '            lnkHides.Visible = False
    '            btnremark.Visible = False
    '            btnReIssue.Visible = False
    '            lblReIssue.Visible = False
    '            lblRefund.Visible = False
    '            gvr.BackColor = Nothing
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub

    'Protected Sub btnReIssue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Filtering one record for Reissue
    '    grdds = ViewState("Grdds")
    '    Try
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
    '                    ' Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('This Ticket No is Allready in ReIssue InProcess'); ", True)
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
    '        grdds = ViewState("Grdds")
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

    Protected Sub lnkHides_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ticket_grdview.Columns(16).Visible = False
            ticket_grdview.Columns(17).Visible = False
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Filtering data from Search click
    Public Sub CheckEmptyValue()
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

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, AirPNR, AgentID, "I", StatusClass.Ticketed)
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
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
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

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, AirPNR, AgentID, "I", StatusClass.Ticketed)
            BindGrid(grdds)
            STDom.ExportData(grdds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
End Class
