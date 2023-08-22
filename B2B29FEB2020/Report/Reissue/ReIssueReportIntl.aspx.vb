Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Partial Class Reports_Reissue_ReIssueReportIntl
    Inherits System.Web.UI.Page
    Dim AgencyDDLDS As New DataSet()
    Dim objDA As New SqlTransaction
    Dim con As New SqlConnection()
    Dim con1 As New SqlConnection()
    Dim adp As SqlDataAdapter
    Public pnrds As New DataSet
    Dim ds As New DataSet()
    Private STDom As New SqlTransactionDom()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type") = "AGENT" Then
                td_Agency.Visible = False
            End If
            If Session("User_Type") = "EXEC" Then
                tr_ExecID.Visible = False
            End If
            If Not IsPostBack Then

                Dim dtExecutive As New DataTable
                Dim dtStatus As New DataTable
                dtStatus = STDom.GetStatusExecutiveID("Reissue").Tables(0)
                dtExecutive = STDom.GetStatusExecutiveID("Reissue").Tables(1)

                ddl_ExecID.AppendDataBoundItems = True
                ddl_ExecID.Items.Clear()
                ddl_ExecID.Items.Insert(0, "--Select--")
                ddl_ExecID.DataSource = dtExecutive
                ddl_ExecID.DataTextField = "ExecutiveID"
                ddl_ExecID.DataValueField = "ExecutiveID"
                ddl_ExecID.DataBind()

                ddl_Status.AppendDataBoundItems = True
                ddl_Status.Items.Clear()
                ddl_Status.Items.Insert(0, "--Select--")
                ddl_Status.DataSource = dtStatus
                ddl_Status.DataTextField = "Status"
                ddl_Status.DataValueField = "Status"
                ddl_Status.DataBind()


                'AgencyDDLDS = objDA.GetAgencyDetailsDDL()
                'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                '    ddl_AgencyName.AppendDataBoundItems = True
                '    ddl_AgencyName.Items.Clear()
                '    ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                '    ddl_AgencyName.DataSource = AgencyDDLDS
                '    ddl_AgencyName.DataTextField = "Agency_Name"
                '    ddl_AgencyName.DataValueField = "user_id"
                '    ddl_AgencyName.DataBind()
                '    'BindGrid()
                'End If
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub CheckEmptyValue()
        Try
            pnrds.Clear()
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


            Dim UserID As String = Session("UID").ToString
            Dim UserType As String = Session("User_Type")
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Air As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = ""
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = ""
            End If
            pnrds = objDA.GetReIssueDetail(UserID, UserType, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, Air, AgentID, ExecID, Status, "I")

            ViewState("pnrds") = pnrds
            grd_paxstatusinfo.DataSource = pnrds
            grd_paxstatusinfo.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindGrid()

        Try
            pnrds.Clear()
            Dim curr_date = Now.Date() & " " & "00:00:00 AM"
            Dim curr_date1 = Now()
            pnrds = objDA.GetReissueDetailCurrent(Session("User_Type"), Session("UID"), curr_date, curr_date1, "I")
            ViewState("pnrds") = pnrds
            grd_paxstatusinfo.DataSource = pnrds
            grd_paxstatusinfo.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        CheckEmptyValue()
    End Sub

    Protected Sub grd_paxstatusinfo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd_paxstatusinfo.PageIndexChanging
        Try
            grd_paxstatusinfo.PageIndex = e.NewPageIndex
            grd_paxstatusinfo.DataSource = ViewState("pnrds")
            grd_paxstatusinfo.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            pnrds.Clear()
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



            Dim UserID As String = Session("UID").ToString
            Dim UserType As String = Session("User_Type")
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Air As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = ""
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = ""
            End If
            pnrds = objDA.GetReIssueDetail(UserID, UserType, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo, Air, AgentID, ExecID, Status, "I")
            STDom.ExportData(pnrds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
End Class
