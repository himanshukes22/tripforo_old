Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class SprReports_Accounts_HotelSalesRegister
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()
    Private STDom As New SqlTransactionDom()
    '  Dim HtlLogObj As New HtlLibrary.HtlLog
    Dim GrdDS, AgencyDS As New DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim AgentID As String = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    TDAgency.Visible = False
                    'TDAgency1.Visible = False
                End If
                GrdDS.Clear()
                'GrdDS = ST.HoldHotelSearchRpt(Now.Date() & " " & "12:00:00 AM", Now(), "", "", "", "", "", "", "", "Confirm", Session("UID").ToString, Session("User_Type").ToString)
                GrdDS = ST.HotelSearchRpt("", "", "", "", "", "", "", "", "", "Confirm", Session("UID").ToString, Session("User_Type").ToString)
                BindSales(GrdDS)
		
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    ' code for search on Any value or blank value.........
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try

            Dim FromDate As String = "", ToDate As String = ""
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
            Dim RoomName As String = If([String].IsNullOrEmpty(txt_roomcode.Text), "", txt_roomcode.Text.Trim)
            ' Dim starrating As String = If([String].IsNullOrEmpty(txt_starrating.Text), "", txt_starrating.Text.Trim)
            GrdDS.Clear()
            'GrdDS = ST.HoldHotelSearchRpt(FromDate, ToDate, BookingrID, orderID, HtlName, RoomName, starrating, AgentID, PGName, "Confirm", Session("UID").ToString, Session("User_Type").ToString)
            GrdDS = ST.HotelSearchRpt(FromDate, ToDate, BookingrID, orderID, HtlName, RoomName, "", AgentID, "", "Confirm", Session("UID").ToString(), Session("User_Type").ToString())
            Bindgrid(GrdDS)
            BindSales(GrdDS)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Private Sub BindSales(ByVal ds As DataSet)
        Try
            Dim dt As DataTable
            Dim Db As String = ""
            Dim sum As Double = 0
            dt = GrdDS.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Db = dr("TotalCost").ToString()
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
    Private Sub Bindgrid(ByVal ds As DataSet)
        Try
            Session("Grdds") = ds
            GrdReport.DataSource = ds
            GrdReport.DataBind()
            If Session("User_Type").ToString = "ADMIN" Or Session("User_Type").ToString = "ACC" Then
                GrdReport.Columns(5).Visible = True
            Else
                GrdReport.Columns(5).Visible = False
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Protected Sub GrdReport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdReport.PageIndexChanging
        Try
            GrdReport.PageIndex = e.NewPageIndex
            Bindgrid(Session("Grdds"))
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
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
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Export.Click
        Try
             Dim FromDate As String = "", ToDate As String = ""
            If Not [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Strings.Mid(Request("From").Split(" ")(0), 4, 2) + "/" + Strings.Left(Request("From").Split(" ")(0), 2) + "/" + Strings.Right(Request("From").Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If Not [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            Dim BookingrID As String = If([String].IsNullOrEmpty(txt_bookingID.Text), "", txt_bookingID.Text.Trim)
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim htlname As String = If([String].IsNullOrEmpty(txt_htlcode.Text), "", txt_htlcode.Text.Trim)
            Dim RoomName As String = If([String].IsNullOrEmpty(txt_roomcode.Text), "", txt_roomcode.Text.Trim)
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            GrdDS.Clear()
            GrdDS = ST.HotelSearchRpt(FromDate, ToDate, BookingrID, OrderID, htlname, RoomName, "", AgentID, "", "Confirm", Session("UID").ToString(), Session("User_Type").ToString())
            GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("Markup"))
            GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("ModifyStatus"))

            If Session("User_Type").ToString = "AGENT" Or Session("User_Type").ToString = "EXEC" Or Session("User_Type").ToString = "SALES" Then
                GrdDS.Tables(0).Columns.Remove(GrdDS.Tables(0).Columns("PurchaseCost"))
            End If
            STDom.ExportData(GrdDS)
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
End Class
