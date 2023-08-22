Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Public Class Reports_Proxy_AgentProxyTicket
    Inherits System.Web.UI.Page
    Private p As New ProxyClass()
    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public pnrds As New DataSet
    Dim result = Nothing
    Private STDom As New SqlTransactionDom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try


            Response.Cache.SetCacheability(HttpCacheability.NoCache)
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
                dtStatus = STDom.GetStatusExecutiveID("Proxy").Tables(0)
                dtExecutive = STDom.GetStatusExecutiveID("Proxy").Tables(1)

                ddl_ExecID.AppendDataBoundItems = True
                ddl_ExecID.Items.Clear()
                ddl_ExecID.Items.Insert(0, "--Select--")
                ddl_ExecID.DataSource = dtExecutive
                ddl_ExecID.DataTextField = "Exec_ID"
                ddl_ExecID.DataValueField = "Exec_ID"
                ddl_ExecID.DataBind()

                ddl_Status.AppendDataBoundItems = True
                ddl_Status.Items.Clear()
                ddl_Status.Items.Insert(0, "--Select Status--")
                ddl_Status.DataSource = dtStatus
                ddl_Status.DataTextField = "Status"
                ddl_Status.DataValueField = "Status"
                ddl_Status.DataBind()

                'Dim ds As New DataSet()
                'ds = SelectAgency()
                'If ds.Tables(0).Rows.Count > 0 Then
                '    ddl_Agency.AppendDataBoundItems = True
                '    ddl_Agency.Items.Clear()
                '    ddl_Agency.Items.Insert(0, "--Select Agency--")
                '    ddl_Agency.DataSource = ds
                '    ddl_Agency.DataTextField = "Agency_Name"
                '    ddl_Agency.DataValueField = "User_Id"
                '    ddl_Agency.DataBind()
                '    CurrentData()

                'End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    'Private Sub CurrentData()
    '    Try
    '        pnrds.Clear()
    '        Dim curr_date = Now.Date() & " " & "12:00:00 AM"
    '        Dim curr_date1 = Now()

    '        Dim AgentID As String
    '        If ddl_Agency.SelectedIndex > 0 Then
    '            AgentID = ddl_Agency.SelectedValue
    '        Else
    '            AgentID = ""
    '        End If

    '        Dim ExecID As String
    '        If ddl_ExecID.SelectedIndex > 0 Then
    '            ExecID = ddl_ExecID.SelectedValue
    '        Else

    '            ExecID = ""
    '        End If

    '        Dim Status As String
    '        If ddl_Status.SelectedIndex > 0 Then
    '            Status = ddl_Status.SelectedValue
    '        Else
    '            Status = ""
    '        End If
    '        pnrds = STDom.GetProxyBookingReport(Session("User_Type").ToString, Session("UID").ToString, curr_date, curr_date1, AgentID, ExecID, Status)
    '        ViewState("pnrds") = pnrds
    '        GridProxyDetail.DataSource = pnrds
    '        GridProxyDetail.DataBind()


    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
    'Private Function SelectAgency() As DataSet
    '    Dim ds As New DataSet()
    '    Try
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    '        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    '        ds = New DataSet()
    '        adp = New SqlDataAdapter("SELECT Distinct Agency_Name,User_Id from New_Regs where User_Id!='' and Agency_Name!='' order by Agency_Name ", con)
    '        adp.Fill(ds)
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '        ex.Message.ToString()
    '    End Try


    '    Return ds
    'End Function
    Protected Sub GridProxyDetail_Changing(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridProxyDetail.PageIndexChanging
        Try
            GridProxyDetail.PageIndex = e.NewPageIndex
            GridProxyDetail.DataSource = Session("pnrds")
            GridProxyDetail.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        pnrds.Clear()
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
            Dim TripType As String
            If ddl_TripType.SelectedIndex > 0 Then
                TripType = ddl_TripType.SelectedValue
            Else
                TripType = ""
            End If

            pnrds = STDom.GetProxyBookingReport(Session("User_Type").ToString, Session("UID").ToString, FromDate, ToDate, AgentID, ExecID, Status, TripType)
            Session("pnrds") = pnrds
            GridProxyDetail.DataSource = pnrds
            GridProxyDetail.DataBind()



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridProxyDetail_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridProxyDetail.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                If DataBinder.Eval(e.Row.DataItem, "status").ToString() = "Ticketed" Then
                    Dim lblmsg As Label = DirectCast(e.Row.FindControl("lbl_UpdateRemark"), Label)
                    lblmsg.Text = ""

                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        pnrds.Clear()
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
            Dim TripType As String
            If ddl_TripType.SelectedIndex > 0 Then
                TripType = ddl_TripType.SelectedValue
            Else
                TripType = ""
            End If

            pnrds = STDom.GetProxyBookingReport(Session("User_Type").ToString, Session("UID").ToString, FromDate, ToDate, AgentID, ExecID, Status, TripType)
            STDom.ExportData(pnrds)
        Catch ex As Exception

        End Try

    End Sub
End Class