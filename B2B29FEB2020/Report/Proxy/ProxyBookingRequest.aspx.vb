Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class Reports_Proxy_ProxyBookingRequest
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom()
    'Need to Add code to Retrive D or I code so that it can be sent to STDom.ProxyDetails'
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                GridProxyDetail.DataSource = STDom.ProxyDetails("Pending", Session("ExecTrip"))
                GridProxyDetail.DataBind()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridProxyDetail_Changing(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridProxyDetail.PageIndex = e.NewPageIndex
    End Sub
    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "Accept" Then
                Dim id As String = e.CommandArgument.ToString()
                Dim dsProxy As New DataSet
                Dim dtProxy As New DataTable
                dsProxy = STDom.ProxyDetails("", Session("ExecTrip"), id)
                dtProxy = dsProxy.Tables(0)
                Dim ExecID As String = dtProxy.Rows(0)("Exec_ID").ToString()
                Dim Status As String = dtProxy.Rows(0)("Status").ToString()
                If ExecID = "" AndAlso Status = "Pending" Then
                    STDom.UpdateProxyDetails("InProcess", Session("UID").ToString, id, "")
                    GridProxyDetail.DataSource = STDom.ProxyDetails("Pending", Session("ExecTrip"))
                    GridProxyDetail.DataBind()
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Already Allocated');", True)
                    GridProxyDetail.DataSource = STDom.ProxyDetails("Pending", Session("ExecTrip"))
                    GridProxyDetail.DataBind()
                End If
            End If
            If e.CommandName = "Reject" Then

                Dim id1 As String = e.CommandArgument.ToString()
                Dim dsProxy As New DataSet
                Dim dtProxy As New DataTable
                dsProxy = STDom.ProxyDetails("", Session("ExecTrip"), id1)
                dtProxy = dsProxy.Tables(0)
                Dim ExecID1 As String = dtProxy.Rows(0)("Exec_ID").ToString()
                Dim Status1 As String = dtProxy.Rows(0)("Status").ToString()
                If ExecID1 = "" AndAlso Status1 = "Pending" Then
                    Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                    Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                    gvr.BackColor = System.Drawing.Color.Yellow
                    td_Reject.Visible = True
                    Dim ID As String = e.CommandArgument.ToString()
                    Session("ID") = ID
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim l As LinkButton = DirectCast(e.Row.FindControl("LB_Reject"), LinkButton)
                l.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to Reject Proxy ID " & DataBinder.Eval(e.Row.DataItem, "ProxyID") & "')")
                If e.Row.RowType = DataControlRowType.DataRow Then
                End If

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Comment.Click
        Try
            Dim PID As String = ""
            PID = Session("ID").ToString()

            If txt_Reject.Text IsNot Nothing AndAlso txt_Reject.Text <> "" Then
                STDom.UpdateProxyDetails("Rejected", Session("UID").ToString, PID, txt_Reject.Text.Trim)
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Comment Submitted Sucessfully');", True)
                td_Reject.Visible = False
                txt_Reject.Text = ""
                GridProxyDetail.DataSource = STDom.ProxyDetails("Pending", Session("ExecTrip"))
                GridProxyDetail.DataBind()
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Enter Comment');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Try
            td_Reject.Visible = False
            GridProxyDetail.DataSource = STDom.ProxyDetails("Pending", Session("ExecTrip"))
            GridProxyDetail.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class