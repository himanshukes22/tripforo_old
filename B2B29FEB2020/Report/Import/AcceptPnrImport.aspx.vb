Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Reports_Import_AcceptPnrImport
    Inherits System.Web.UI.Page
    Private PI As New pnrinfo()
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            BindDate(ST.ImportPNRDetailsIntl("Pending", "D"))
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
                DirectCast(rowitem.FindControl("lbl_OrderId"), Label).Text = (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("lbl_PnrNo"), Label).Text = (pnrArray(0)("PNRNo")).ToString
                DirectCast(rowitem.FindControl("lbl_AgentID"), Label).Text = (pnrArray(0)("AgentID")).ToString
                DirectCast(rowitem.FindControl("lbl_Ag_Name"), Label).Text = (pnrArray(0)("Ag_Name")).ToString
                DirectCast(rowitem.FindControl("lbl_Depart"), Label).Text = (pnrArray(0)("Departure")).ToString & " - " & (pnrArray(pnrArray.Length - 1)("Destination")).ToString
                DirectCast(rowitem.FindControl("lbl_DDate"), Label).Text = (pnrArray(0)("Departdate")).ToString
                DirectCast(rowitem.FindControl("lbl_SDate"), Label).Text = (pnrArray(0)("RequestDateTime")).ToString

                DirectCast(rowitem.FindControl("lbl_agenttype"), Label).Text = (pnrArray(0)("AgentType")).ToString
                DirectCast(rowitem.FindControl("lbl_SalesExecId"), Label).Text = (pnrArray(0)("SalesExecId")).ToString

                DirectCast(rowitem.FindControl("lbl_Status"), Label).Text = (pnrArray(0)("Status")).ToString
                DirectCast(rowitem.FindControl("ITZ_Accept"), LinkButton).CommandArgument = (pnrArray(0)("OrderId")).ToString
                DirectCast(rowitem.FindControl("ITZ_Reject"), LinkButton).CommandArgument = (pnrArray(0)("OrderId")).ToString
                cnt += 1
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub ITZ_Accept_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim orderid As String = DirectCast(sender, LinkButton).CommandArgument.ToString()
            Dim rUpdate As Integer = 0
            Dim dtstatus As New DataTable
            dtstatus = ST.PnrImportIntlDetails(orderid, "D").Tables(0)
            Dim Status As String = ""
            Status = dtstatus.Rows(0)("Status").ToString()
            Dim ExecID As String = ""
            ExecID = dtstatus.Rows(0)("Exec_ID").ToString()
            If (Status = "Pending" AndAlso ExecID = "") Then
                rUpdate = ST.UpdateImpPnrIntlDetails(orderid, "Processed", Session("UID").ToString, "", "I")
                BindDate(ST.ImportPNRDetailsIntl("Pending", "D"))
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Order Already Allocated');", True)
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub ITZ_Reject_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim orderid As String = DirectCast(sender, LinkButton).CommandArgument.ToString()


            Dim dtstatus As New DataTable
            dtstatus = ST.PnrImportIntlDetails(orderid, "D").Tables(0)
            Dim Status As String = ""
            Status = dtstatus.Rows(0)("Status").ToString()
            Dim ExecID As String = ""
            ExecID = dtstatus.Rows(0)("Exec_ID").ToString()
            If (Status = "Pending" AndAlso ExecID = "") Then
                td_RejectRmk.Visible = True
                ViewState("orderid") = orderid
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Order Already Allocated');", True)
            End If

            
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_AgentRmk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgentRmk.Click
        If txt_RejectRmk.Text IsNot Nothing AndAlso txt_RejectRmk.Text <> "" Then
            Try


                Dim rUpdate As Integer = 0
                rUpdate = ST.UpdateImpPnrIntlDetails(ViewState("orderid"), "Rejected", Session("UID").ToString, txt_RejectRmk.Text, "I")
                BindDate(ST.ImportPNRDetailsIntl("Pending", "D"))
                'txt_RejectRmk.Visible = False
                txt_RejectRmk.Text = ""
                td_RejectRmk.Visible = False

            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        Else
            Response.Write("<script>alert('Please Enter Comment')</script>")
        End If
    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            txt_RejectRmk.Text = ""
            td_RejectRmk.Visible = False
            BindDate(ST.ImportPNRDetailsIntl("Pending", "D"))

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
