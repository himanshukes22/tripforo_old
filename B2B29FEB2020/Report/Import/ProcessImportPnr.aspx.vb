Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Reports_Import_ProcessImportPnr
    Inherits System.Web.UI.Page
    Private PI As New pnrinfo()
    Private ST As New SqlTransaction()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            'If Not IsPostBack Then
            BindDate(ST.ImportPNRDetailsIntl("Processed", "D", "", Session("UID").ToString))

            'End If

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
                pnrArray = ImportPnrDs.Tables(0).Select("OrderId='" & ImportPnrDs.Tables(1).Rows(cnt)("OrderId").ToString & "'", "")
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).HRef = "ImportPnrDetails.aspx?OrderId=" & (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("ancher"), HtmlAnchor).InnerHtml = "View"

                DirectCast(rowitem.FindControl("ancherupdate"), HtmlAnchor).HRef = "UpdateImportPnr.aspx?OrderId=" & (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("ancherupdate"), HtmlAnchor).InnerHtml = "Update"
                DirectCast(rowitem.FindControl("lbl_OrderId"), Label).Text = (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("lbl_PnrNo"), Label).Text = (pnrArray(0)("PNRNo")).ToString
                DirectCast(rowitem.FindControl("lbl_AgentID"), Label).Text = (pnrArray(0)("AgentID")).ToString
                DirectCast(rowitem.FindControl("lbl_Ag_Name"), Label).Text = (pnrArray(0)("Ag_Name")).ToString
                DirectCast(rowitem.FindControl("lbl_Depart"), Label).Text = (pnrArray(0)("Departure")).ToString & " - " & (pnrArray(pnrArray.Length - 1)("Destination")).ToString
                DirectCast(rowitem.FindControl("lbl_DDate"), Label).Text = (pnrArray(0)("Departdate")).ToString
                DirectCast(rowitem.FindControl("lbl_Status"), Label).Text = (pnrArray(0)("Status")).ToString
                DirectCast(rowitem.FindControl("lbl_agenttype"), Label).Text = (pnrArray(0)("AgentType")).ToString
                DirectCast(rowitem.FindControl("lbl_SalesExecId"), Label).Text = (pnrArray(0)("SalesExecId")).ToString

                DirectCast(rowitem.FindControl("lbl_SDate"), Label).Text = (pnrArray(0)("RequestDateTime")).ToString
                DirectCast(rowitem.FindControl("lbl_AcceptedDate"), Label).Text = (pnrArray(0)("AcceptedDate")).ToString
                ' DirectCast(rowitem.FindControl("ITZ_Reject"), LinkButton).CommandArgument = (pnrArray(0)("orderid")).ToString
                cnt += 1
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    

    Protected Sub GridImportProxyDetail_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridImportProxyDetail.RowCommand
        Try
            If e.CommandName = "AgentRemark" Then

                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.Yellow

                td_AgentRmk.Visible = True
                td_RejectRmk.Visible = False
                Dim OrderId As String = e.CommandArgument.ToString()
                Session("OrderId") = OrderId




            End If
            If e.CommandName = "Reject" Then
                Dim orderid As String = e.CommandArgument.ToString()
               
                Dim lbr As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvrej As GridViewRow = TryCast(lbr.Parent.Parent, GridViewRow)
                gvrej.BackColor = System.Drawing.Color.Yellow
                td_AgentRmk.Visible = False
                td_RejectRmk.Visible = True
                ViewState("orderid") = orderid
          

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub



    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            td_RejectRmk.Visible = False
            td_AgentRmk.Visible = False
            BindDate(ST.ImportPNRDetailsIntl("Processed", "D", "", Session("UID").ToString))
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_AgentRmk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgentRmk.Click
        Try
            Dim OrderId As String = ""
            OrderId = Session("OrderId").ToString()
            If txt_AgentRmk.Text IsNot Nothing AndAlso txt_AgentRmk.Text <> "" Then

                PI.AgentCommentIntl(OrderId, txt_AgentRmk.Text)
                td_AgentRmk.Visible = False
                td_RejectRmk.Visible = False

                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Agent Remark Submitted Sucessfully');", True)
                BindDate(ST.ImportPNRDetailsIntl("Processed", "D", "", Session("UID").ToString))
            Else

                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Enter Remark');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If txt_RejectRmk.Text IsNot Nothing AndAlso txt_RejectRmk.Text <> "" Then

                Dim rUpdate As Integer = 0
                

                rUpdate = ST.UpdateImpPnrIntlDetails(ViewState("orderid"), "Rejected", Session("UID").ToString, txt_RejectRmk.Text, "I")
                td_AgentRmk.Visible = False
                td_RejectRmk.Visible = False
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Reject Remark Submitted Sucessfully');", True)
                BindDate(ST.ImportPNRDetailsIntl("Processed", "D", "", Session("UID").ToString))
          


            Else

                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Enter Comment');", True)
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            td_RejectRmk.Visible = False
            td_AgentRmk.Visible = False
            BindDate(ST.ImportPNRDetailsIntl("Processed", "D", "", Session("UID").ToString))
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

End Class
