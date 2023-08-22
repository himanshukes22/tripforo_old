Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data

Partial Class SprReports_Distr_InProcessUploadDetails
    Inherits System.Web.UI.Page


    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Private Distr As New Distributor
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Or Session("User_Type").ToString() <> "DI" Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then
                Try
                    If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then
                        grd_accdeposit.DataSource = Distr.DepositStatusDetails("DIInProcess", Session("UID"), Session("UID"))
                        grd_accdeposit.DataBind()
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)

                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub grd_accdeposit_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)


        Try
            If e.CommandName = "reject" Then

                id = e.CommandArgument.ToString()
                Dim DtDDetails As New DataTable
                DtDDetails = STDom.GetDepositDetailsByID(id).Tables(0)
                Dim Status As String = DtDDetails.Rows(0)("Status").ToString
                Dim IDS As String = DtDDetails.Rows(0)("Counter").ToString
                If IDS = id AndAlso Status = "Confirm" Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Deposite amount is already confirm');", True)
                    grd_accdeposit.DataSource = Distr.DepositStatusDetails("DIInProcess", Session("UID"), Session("UID"))
                    grd_accdeposit.DataBind()

                Else
                    Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                    Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                    gvr.BackColor = System.Drawing.Color.YellowGreen
                    td_Reject.Visible = True

                    Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                    Dim AgentID As Label = DirectCast(rw.FindControl("lbl_uid"), Label)
                    ViewState("ID") = id
                    ViewState("AgentID") = AgentID.Text
                End If

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Try
            td_Reject.Visible = False
            grd_accdeposit.DataSource = Distr.DepositStatusDetails("DIInProcess", Session("UID"), Session("UID"))
            grd_accdeposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub grd_accdeposit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)


    End Sub
    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
        Try
            STDom.UpdateDepositDetails(ViewState("ID"), ViewState("AgentID"), "Rejected", "Rej", Session("UID"), txt_Reject.Text.Replace("'", ""))
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Sucessfully');", True)
            td_Reject.Visible = False
            grd_accdeposit.DataSource = Distr.DepositStatusDetails("DIInProcess", Session("UID"), Session("UID"))
            grd_accdeposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_searchag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_searchag.Click
        Dim dsag As New DataSet
        dsag = Distr.GetDepositeDetailsById(Request("hidtxtAgencyName"), "DIInProcess", Session("UID"))
        grd_accdeposit.DataSource = dsag
        grd_accdeposit.DataBind()
    End Sub
End Class

