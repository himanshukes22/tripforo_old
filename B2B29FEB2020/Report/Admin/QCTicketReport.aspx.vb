Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Partial Class SprReports_Admin_QCTicketReport
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    Private ST As New SqlTransaction()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()
    Private sttusobj As New Status()
    Dim con As New SqlConnection()
    Dim PaxType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim AgentID As String = ""

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False
                    AgentID = Session("UID").ToString()

                End If

                Dim curr_date = Now.Date() & " " & "12:00:00 AM"
                Dim curr_date1 = Now()
                grdds.Clear()
                grdds = USP_GetTicketDetail_QCTKTLOOKUP(Session("UID").ToString, Session("User_Type").ToString, curr_date, curr_date1, "", "", "", "")
                'BindGrid(grdds)
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
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub CheckEmptyValue()
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                'FromDate = Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = Strings.Right((Request("From")).Split(" ")(0), 4) + "-" + Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "-" + Strings.Left((Request("From")).Split(" ")(0), 2)
                FromDate = FromDate + " " + "00:00:01 000"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Right((Request("To")).Split(" ")(0), 4) & "-" & Left((Request("To")).Split(" ")(0), 2) & "-" & Mid((Request("To")).Split(" ")(0), 4, 2)
                ToDate = ToDate & " " & "23:59:59 000"
            End If


            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim PaxName As String = If([String].IsNullOrEmpty(txt_PaxName.Text), "", txt_PaxName.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            'Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = USP_GetTicketDetail_QCTKTLOOKUP(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo)
            BindGrid(grdds)

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
            If Session("User_Type").ToString = "ADMIN" Then

                ticket_grdview.Columns(17).Visible = True
               
            Else
                ticket_grdview.Columns(17).Visible = False

            End If
            ticket_grdview.DataBind()
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
                FromDate = Strings.Right((Request("From")).Split(" ")(0), 4) + "-" + Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "-" + Strings.Left((Request("From")).Split(" ")(0), 2)
                FromDate = FromDate + " " + "00:00:01 000"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Right((Request("To")).Split(" ")(0), 4) & "-" & Left((Request("To")).Split(" ")(0), 2) & "-" & Mid((Request("To")).Split(" ")(0), 4, 2)
                ToDate = ToDate & " " & "23:59:59 000"
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
            ' Dim AirPNR As String = If([String].IsNullOrEmpty(txt_AirPNR.Text), "", txt_AirPNR.Text.Trim)
            grdds.Clear()
            grdds = USP_GetTicketDetail_QCTKTLOOKUP(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, PaxName, TicketNo)

            STDom.ExportData(grdds)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        CheckEmptyValue()
        txt_PNR.Text = ""
        txt_PaxName.Text = ""
        txt_OrderId.Text = ""
        txt_TktNo.Text = ""
    End Sub

    Protected Sub ticket_grdview_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ticket_grdview.RowCommand

        Try

            If e.CommandName = "ChangeStatus" Then

                Dim Orderid As String = e.CommandArgument.ToString()
                Dim ds As New DataSet()

                'Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                'Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                'gvr.BackColor = System.Drawing.Color.Yellow


                Dim ID As String = e.CommandArgument.ToString()
               
                Dim dtID As New DataTable()

                ds = USP_PreHoldMGMT(ID, "", "Update", Convert.ToString(Session("UID")))
                If ds IsNot Nothing And ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0)(0).ToString() = "1" Then
                        ShowAlertMessage("Status Succesfully Updated")  ''Response.Write("<script>alert('Status Succesfully Updated')</script>")
                    End If
                    ShowAlertMessage("Status not Succesfully Updated, Please retry.")
                   

                End If

                Dim ds11 = New DataSet

               
                ticket_grdview.DataBind()
            Else



            End If
           
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Function IsVisible(ByVal status As Object) As Boolean

        If status.ToString().ToUpper().Trim() = "PNR ON PREHOLD" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function USP_GetTicketDetail_QCTKTLOOKUP(ByVal LoginId As String, ByVal usertype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal OrderID As String, _
                                                     ByVal pnr As String, ByVal paxname As String, ByVal TicketNo As String) As DataSet
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim ds1 As New DataSet
        Dim cmd As New SqlCommand()
        Dim da As New SqlDataAdapter(cmd)
        cmd.CommandText = "USP_GetTicketDetail_QCTKTLOOKUP"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@usertype", SqlDbType.VarChar).Value = usertype
        cmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = LoginId
        cmd.Parameters.Add("@FormDate", SqlDbType.VarChar).Value = FromDate
        cmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = ToDate
        cmd.Parameters.Add("@OderId", SqlDbType.VarChar).Value = OrderID
        cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = pnr
        cmd.Parameters.Add("@PaxName", SqlDbType.VarChar).Value = paxname
        cmd.Parameters.Add("@TicketNo", SqlDbType.VarChar).Value = TicketNo
        cmd.Connection = con1
        da.Fill(ds1)
        Return ds1
    End Function


    Public Function USP_PreHoldMGMT(ByVal orderID As String, ByVal Remark As String, ByVal queryType As String, ByVal updatedBy As String) As DataSet
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim ds1 As New DataSet
        Dim cmd As New SqlCommand()
        Dim da As New SqlDataAdapter(cmd)
        cmd.CommandText = "Usp_Prehold_MGMT"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@orderID", SqlDbType.VarChar).Value = orderID
        cmd.Parameters.Add("@PreholdRemark", SqlDbType.VarChar).Value = Remark
        cmd.Parameters.Add("@queryType", SqlDbType.VarChar).Value = queryType
        cmd.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = updatedBy


        cmd.Connection = con1
        da.Fill(ds1)
        Return ds1
    End Function

End Class
