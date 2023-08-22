Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class Reports_Proxy_ProxyTicketDetail
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
                'Binding of Gridview Data from ProxyTicket table records based on Session Id and ProxyType
                'to populate dtaa on ProxyTicket Detail page
                GridProxyDetail.DataSource = STDom.ProxyDetails("InProcess", Session("ExecTrip"), Session("UID").ToString)
                GridProxyDetail.DataBind()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Response.Clear()

            Response.AddHeader("content-disposition", "attachment; filename=FileName.xls")

            Response.Charset = ""

            Response.ContentType = "application/vnd.xls"

            Dim stringWrite As New System.IO.StringWriter()

            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

            GridProxyDetail.RenderControl(htmlWrite)

            Response.Write(stringWrite.ToString())

            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that a Form control was rendered 


    End Sub
    Protected Sub GridProxyDetail_Changing(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridProxyDetail.PageIndex = e.NewPageIndex
    End Sub
    Protected Sub GridRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridProxyDetail.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim l As LinkButton = DirectCast(e.Row.FindControl("LB_Reject"), LinkButton)
                l.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to Reject this Proxy ID " & DataBinder.Eval(e.Row.DataItem, "ProxyID") & "')")
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridProxyDetail_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridProxyDetail.RowCommand
        Try
            If e.CommandName = "Reject" Then
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.Yellow
                td_Reject.Visible = True
                td_AgentRmk.Visible = False
                Dim ID As String = e.CommandArgument.ToString()
                Session("ID") = ID
            End If
            If e.CommandName = "AgentComment" Then
                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.Yellow
                td_AgentRmk.Visible = True
                td_Reject.Visible = False
                Dim ID As String = e.CommandArgument.ToString()
                Session("ID") = ID
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Try
            td_Reject.Visible = False
            GridProxyDetail.DataSource = STDom.ProxyDetails("InProcess", Session("ExecTrip"), Session("UID").ToString)
            GridProxyDetail.DataBind()
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
                GridProxyDetail.DataSource = STDom.ProxyDetails("InProcess", Session("ExecTrip"), Session("UID").ToString)
                GridProxyDetail.DataBind()
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Enter Comment');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            td_AgentRmk.Visible = False
            GridProxyDetail.DataSource = STDom.ProxyDetails("InProcess", Session("ExecTrip"), Session("UID").ToString)
            GridProxyDetail.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_AgentRmk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgentRmk.Click
        Try
            Dim PID As String = ""
            PID = Session("ID").ToString()
            If txt_AgentRmk.Text IsNot Nothing AndAlso txt_AgentRmk.Text <> "" Then
                STDom.AgentCommentProxy(PID, txt_AgentRmk.Text.Trim)
                td_AgentRmk.Visible = False
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Agent Remark Submitted Sucessfully');", True)
                GridProxyDetail.DataSource = STDom.ProxyDetails("InProcess", Session("ExecTrip"), Session("UID").ToString)
                GridProxyDetail.DataBind()
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Enter Remark');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class