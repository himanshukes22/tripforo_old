Imports System.Data
Partial Class SprReports_Refund_HotelRefundReport
    Inherits System.Web.UI.Page

    Private HtlST As New HotelDAL.HotelDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            End If
            If Session("User_Type") = "AGENT" Then
                TDAgency.Visible = False
            End If
            If Session("User_Type") = "EXEC" Then
                tr_ExecID.Visible = False
            End If

            If Not IsPostBack Then
                If Session("User_Type") <> "AGENT" And Session("User_Type") <> "EXEC" Then
                    Dim STDom As New SqlTransactionDom()
                    ddl_ExecID.AppendDataBoundItems = True
                    ddl_ExecID.Items.Clear()
                    ddl_ExecID.Items.Insert(0, "--Select--")
                    ddl_ExecID.DataSource = STDom.GetStatusExecutiveID("HotelCancellation").Tables(0)
                    ddl_ExecID.DataTextField = "ExecutiveID"
                    ddl_ExecID.DataValueField = "ExecutiveID"
                    ddl_ExecID.DataBind()
                Else
                    ddl_ExecID.Visible = False
                    tr_ExecID.Visible = False


                End If
                BindSales(HtlST.HotelSearchRpt("", "", "", "", "", "", "", "", "", "", Session("UID").ToString, Session("User_Type").ToString))
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

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Dim STDom As New SqlTransactionDom()
        Dim GrdDS As New DataSet()
        Try
            GrdDS = GetReportData()
            STDom.ExportData(GrdDS)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Function GetReportData() As DataSet
        Dim GrdDS As New DataSet()
        Dim FromDate As String = "", ToDate As String = "", Trip As String = "", ExecID As String = "", Status As String = "", checkin As String = ""
        Try
            If Not [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Strings.Mid(Request("From").Split(" ")(0), 4, 2) + "/" + Strings.Left(Request("From").Split(" ")(0), 2) + "/" + Strings.Right(Request("From").Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If Not [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim BookingrID As String = If([String].IsNullOrEmpty(txt_bookingID.Text), "", txt_bookingID.Text.Trim)
            Dim orderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim HtlName As String = If([String].IsNullOrEmpty(txt_htlcode.Text), "", txt_htlcode.Text.Trim)
            Dim RoomName As String = If([String].IsNullOrEmpty(txt_roomcode.Text), "", txt_htlcode.Text.Trim)
            If Not [String].IsNullOrEmpty(Request("Checkin")) Then
                Dim chkdate() As String = Request("Checkin").Split("-")
                Checkin = chkdate(2) + "-" + chkdate(1) + "-" + chkdate(0)
            End If

            If Triptype.SelectedIndex > 0 Then
                Trip = Triptype.SelectedValue
            End If
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else
                ExecID = Session("UID").ToString()
            End If
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue           
            End If
            ' GrdDS = ST.HotelSearchRpt(FromDate, ToDate, BookingrID, orderID, HtlName, RoomName, Trip, AgentID, Checkin, "Confirm", Session("UID").ToString(), Session("User_Type").ToString())
            GrdDS = HtlST.HotelSearchRpt(FromDate, ToDate, BookingrID, orderID, HtlName, RoomName, Trip, AgentID, checkin, Status, ExecID, Session("User_Type").ToString())
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
        Return GrdDS
    End Function

    Private Sub BindSales(ByVal GrdRfnDS As DataSet)
        Try
            Dim dt As DataTable
            Dim Db As String = ""
            Dim sum As Double = 0
            dt = GrdRfnDS.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Db = dr("RefundFare").ToString()
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
    Private Sub Bindgrid(ByVal GrdRfnDS As DataSet)
        Try
            grd_RefundReport.DataSource = GrdRfnDS
            grd_RefundReport.DataBind()
            Session("GrdRfndds") = GrdRfnDS
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub grd_RefundReport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd_RefundReport.PageIndexChanging
        Try
            grd_RefundReport.PageIndex = e.NewPageIndex
            Bindgrid(Session("GrdRfndds"))
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
