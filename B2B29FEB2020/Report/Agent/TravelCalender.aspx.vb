Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class SprReports_Agent_TravelCalender
    Inherits System.Web.UI.Page

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
            End If
            'Dim dscancelled As DataSet = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "CB")
            'If dscancelled.Tables(0).Rows.Count > 0 Then
            '    DrpCancelledBy.AppendDataBoundItems = True
            '    DrpCancelledBy.Items.Clear()
            '    DrpCancelledBy.Items.Insert(0, "Select")
            '    DrpCancelledBy.DataSource = dscancelled
            '    DrpCancelledBy.DataTextField = "BOOKEDBY"
            '    DrpCancelledBy.DataValueField = "BOOKEDBY"
            '    DrpCancelledBy.DataBind()

            'End If

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
            'End If
            'If Session("User_Type") = "EXEC" Then
            '    ''tr_ExecID.Visible = False
            '    ''tdTripNonExec1.Visible = False
            '    tdTripNonExec2.Visible = False
            'End If
            'Dim curr_date = Now.Date() & " " & "12:00:00 AM"
            'Dim curr_date1 = Now()
            'Dim trip As String = "" ''''IIf(Session("User_Type") = "EXEC", IIf([String].IsNullOrEmpty(Session("TripExec")), "", Session("TripExec").ToString().Trim()), If([String].IsNullOrEmpty(ddlTripRefunDomIntl.SelectedItem.Value), "", ddlTripRefunDomIntl.SelectedItem.Value.Trim()))
            'If Session("User_Type") = "EXEC" Then
            '    If [String].IsNullOrEmpty(Session("TripExec")) Then
            '        trip = ""
            '    Else
            '        trip = Session("TripExec").ToString().Trim()
            '    End If
            'Else
            '    trip = If([String].IsNullOrEmpty(ddlTripDomIntl.SelectedItem.Value), "", ddlTripDomIntl.SelectedItem.Value.Trim())
            'End If
            'grdds.Clear()
            'grdds = ST.USP_GetTicketDetail_Intl(Session("UID").ToString, Session("User_Type").ToString, curr_date, curr_date1, "", "", "", "", "", AgentID, trip.Trim(), StatusClass.Ticketed)
            ''BindGrid(grdds)
            'Dim dt As DataTable
            'Dim Db As String = ""
            'Dim sum As Double = 0
            'dt = grdds.Tables(0)
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        Db = dr("TotalAfterDis").ToString()
            '        If Db Is Nothing OrElse Db = "" Then
            '            Db = 0
            '        Else
            '            sum += Db
            '        End If
            '    Next

            'End If
            'lbl_Total.Text = "0"
            'If sum <> 0 Then
            '    lbl_Total.Text = sum.ToString '("C", New CultureInfo("en-IN"))
            'End If
            'lbl_counttkt.Text = dt.Rows.Count
            'End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class

