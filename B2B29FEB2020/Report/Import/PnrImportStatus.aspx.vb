Imports System.Data
Imports System.Data.SqlClient

Partial Class Reports_Import_PnrImportStatus
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim ds As New DataSet()
    Dim AgencyDDLDS As New DataSet()
    Dim grdds As New DataSet
    Private sttusobj As New Status()
    Public pnrds As New DataSet
    Dim con As New SqlConnection()
    Dim PaxType As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Session("User_Type") = "AGENT" Then
            tr_ExecID.Visible = False
            td_Agency.Visible = False
        End If
        If Session("User_Type") = "EXEC" Then
            tr_ExecID.Visible = False
        End If
        If Not IsPostBack Then

            Try
                Dim dtExecutive As New DataTable
                Dim dtStatus As New DataTable
                dtStatus = STDom.GetStatusExecutiveID("Import").Tables(0)
                dtExecutive = STDom.GetStatusExecutiveID("Import").Tables(1)

                ddl_ExecID.AppendDataBoundItems = True
                ddl_ExecID.Items.Clear()
                ddl_ExecID.Items.Insert(0, "Select Agent Type")
                ddl_ExecID.DataSource = dtExecutive
                ddl_ExecID.DataTextField = "Exec_ID"
                ddl_ExecID.DataValueField = "Exec_ID"
                ddl_ExecID.DataBind()

                ddl_Status.AppendDataBoundItems = True
                ddl_Status.Items.Clear()
                ddl_Status.Items.Insert(0, "--Select--")
                ddl_Status.DataSource = dtStatus
                ddl_Status.DataTextField = "Status"
                ddl_Status.DataValueField = "Status"
                ddl_Status.DataBind()


                'AgencyDDLDS = ST.GetAgencyDetailsDDL()
                'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                '    Try
                'ddl_AgencyName.AppendDataBoundItems = True
                'ddl_AgencyName.Items.Clear()
                'ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                'ddl_AgencyName.DataSource = AgencyDDLDS
                'ddl_AgencyName.DataTextField = "Agency_Name"
                'ddl_AgencyName.DataValueField = "user_id"
                'ddl_AgencyName.DataBind()

                'Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                'Dim curr_date1 = Now()
                'Dim AgentID As String
                'If ddl_AgencyName.SelectedItem.Text = "--Select Agency Name--" Then
                '    AgentID = Nothing
                'Else
                '    AgentID = ddl_AgencyName.SelectedValue
                'End If
                'Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), Nothing, txt_OrderId.Text.Trim)
                'Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), Nothing, txt_PNR.Text.Trim)
                'Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), Nothing, txt_AirPNR.Text.Trim)
                'Dim ExecID As String
                'If ddl_ExecID.SelectedIndex > 0 Then
                '    ExecID = ddl_ExecID.SelectedValue
                'Else

                '    ExecID = Nothing
                'End If

                'Dim Status As String
                'If ddl_Status.SelectedIndex > 0 Then
                '    Status = ddl_Status.SelectedValue
                'Else
                '    Status = Nothing
                'End If
                'pnrds.Clear()
                'pnrds = ST.ImportPnrReport_Intl(Session("User_Type"), Session("UID").ToString, curr_date, curr_date1, OrderID, PNR, AirPNR, AgentID, "D", ExecID, Status)
                'ViewState("pnrds") = pnrds
                'BindDate(pnrds)

                '    Catch ex As Exception
                '        clsErrorLog.LogInfo(ex)

                '    End Try
                'End If
            Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
        End If
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Nothing
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Nothing
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))


            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), Nothing, txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), Nothing, txt_PNR.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), Nothing, txt_AirPNR.Text.Trim)

            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = Nothing
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = Nothing
            End If


            pnrds.Clear()
            pnrds = ST.ImportPnrReport_Intl(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, OrderID, PNR, AirPNR, AgentID, "D", ExecID, Status)
            Session("pnrds") = pnrds
            BindDate(pnrds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Sub BindDate(ByVal ImportPnrDs As DataSet)
        Try
            GridImportProxyDetail.DataSource = ImportPnrDs.Tables(1)
            GridImportProxyDetail.DataBind()
            Dim cnt As Integer = 0
            Dim pnrArray As Array
            For Each rowitem As GridViewRow In GridImportProxyDetail.Rows
                pnrArray = ImportPnrDs.Tables(0).Select("PNRNo='" & ImportPnrDs.Tables(1).Rows(cnt)("PNRNo").ToString & "'", "")
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).HRef = "ImportPnrDetails.aspx?OrderId=" & (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).InnerHtml = "View"
                DirectCast(rowitem.FindControl("lbl_PnrNo"), Label).Text = (pnrArray(0)("PNRNo")).ToString
                DirectCast(rowitem.FindControl("lbl_OrderId"), Label).Text = (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("lbl_AgentID"), Label).Text = (pnrArray(0)("AgentID")).ToString
                DirectCast(rowitem.FindControl("lbl_Ag_Name"), Label).Text = (pnrArray(0)("Ag_Name")).ToString
                DirectCast(rowitem.FindControl("lbl_Depart"), Label).Text = (pnrArray(0)("Departure")).ToString & " - " & (pnrArray(pnrArray.Length - 1)("Destination")).ToString
                DirectCast(rowitem.FindControl("lbl_DDate"), Label).Text = (pnrArray(0)("Departdate")).ToString
                DirectCast(rowitem.FindControl("lbl_Status"), Label).Text = (pnrArray(0)("Status")).ToString
                DirectCast(rowitem.FindControl("lbl_AlertMsg"), Label).Text = (pnrArray(0)("UpdRemark")).ToString
                DirectCast(rowitem.FindControl("lbl_Exec"), Label).Text = (pnrArray(0)("Exec_ID")).ToString
                DirectCast(rowitem.FindControl("lbl_ReqTime"), Label).Text = (pnrArray(0)("RequestDateTime")).ToString
                DirectCast(rowitem.FindControl("lbl_AcceptedDate"), Label).Text = (pnrArray(0)("AcceptedDate")).ToString
                DirectCast(rowitem.FindControl("lbl_UpTime"), Label).Text = (pnrArray(0)("UpdatedDate")).ToString
                cnt += 1
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub GridImportProxyDetail_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridImportProxyDetail.PageIndexChanging
        Try
            GridImportProxyDetail.PageIndex = e.NewPageIndex
            Dim i As Integer = GridImportProxyDetail.PageIndex.ToString
            ViewState("Count") = i
            BindDatePaging(Session("pnrds"))
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Sub BindDatePaging(ByVal ImportPnrDs As DataSet)
        Try
            Dim cnt As Integer = 0
            If (ViewState("Count") = 0) Then
                cnt = 0
            Else
                cnt = Convert.ToInt32(ViewState("Count") * 30)
            End If
            GridImportProxyDetail.DataSource = ImportPnrDs.Tables(1)
            GridImportProxyDetail.DataBind()

            Dim pnrArray As Array
            For Each rowitem As GridViewRow In GridImportProxyDetail.Rows
                pnrArray = ImportPnrDs.Tables(0).Select("PNRNo='" & ImportPnrDs.Tables(1).Rows(cnt)("PNRNo").ToString & "'", "")
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).HRef = "ImportPnrDetails.aspx?OrderId=" & (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).InnerHtml = "View"
                DirectCast(rowitem.FindControl("lbl_PnrNo"), Label).Text = (pnrArray(0)("PNRNo")).ToString
                DirectCast(rowitem.FindControl("lbl_OrderId"), Label).Text = (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("lbl_AgentID"), Label).Text = (pnrArray(0)("AgentID")).ToString
                DirectCast(rowitem.FindControl("lbl_Ag_Name"), Label).Text = (pnrArray(0)("Ag_Name")).ToString
                DirectCast(rowitem.FindControl("lbl_Depart"), Label).Text = (pnrArray(0)("Departure")).ToString & " - " & (pnrArray(pnrArray.Length - 1)("Destination")).ToString
                DirectCast(rowitem.FindControl("lbl_DDate"), Label).Text = (pnrArray(0)("Departdate")).ToString
                DirectCast(rowitem.FindControl("lbl_Status"), Label).Text = (pnrArray(0)("Status")).ToString
                DirectCast(rowitem.FindControl("lbl_AlertMsg"), Label).Text = (pnrArray(0)("UpdRemark")).ToString
                DirectCast(rowitem.FindControl("lbl_Exec"), Label).Text = (pnrArray(0)("Exec_ID")).ToString
                DirectCast(rowitem.FindControl("lbl_ReqTime"), Label).Text = (pnrArray(0)("RequestDateTime")).ToString
                DirectCast(rowitem.FindControl("lbl_AcceptedDate"), Label).Text = (pnrArray(0)("AcceptedDate")).ToString
                DirectCast(rowitem.FindControl("lbl_UpTime"), Label).Text = (pnrArray(0)("UpdatedDate")).ToString
                cnt += 1
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = Nothing
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = Nothing
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", Nothing, Request("hidtxtAgencyName"))


            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), Nothing, txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), Nothing, txt_PNR.Text.Trim)
            Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), Nothing, txt_AirPNR.Text.Trim)

            Dim ExecID As String
            If ddl_ExecID.SelectedIndex > 0 Then
                ExecID = ddl_ExecID.SelectedValue
            Else

                ExecID = Nothing
            End If

            Dim Status As String
            If ddl_Status.SelectedIndex > 0 Then
                Status = ddl_Status.SelectedValue
            Else
                Status = Nothing
            End If


            pnrds.Clear()
            pnrds = ST.ImportPnrReport_Intl(Session("User_Type"), Session("UID").ToString, FromDate, ToDate, OrderID, PNR, AirPNR, AgentID, "D", ExecID, Status)
            STDom.ExportData(pnrds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
