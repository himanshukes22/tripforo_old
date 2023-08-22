Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Reports_Refund_HtlRefundRequest
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then
                BindGrid()
            End If
        Catch ex As Exception
            ''HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Dim TripType As String = ""
            If Session("ExecTrip").ToString() = "D" Then
                TripType = "Domestic"
            ElseIf Session("ExecTrip").ToString() = "I" Then
                TripType = "International"
            End If

            Acept_grdview.DataSource = ST.HtlRefundDetail(StatusClass.Pending.ToString(), "", "", "RefundGET", TripType)
            Acept_grdview.DataBind()
        Catch ex As Exception
            ''HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub
    'Protected Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Acept_grdview.RowCommand
    '    Try
    '        If e.CommandName = "Accept" Then
    '            Try
    '                'Update Status in CancellationIntl Table after Accept
    '                ST.HtlRefundDetail("InProcess", e.CommandArgument, Session("UID").ToString, "Modify", "")
    '                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
    '                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
    '                gvr.BackColor = System.Drawing.Color.GreenYellow
    '                BindGrid()
    '            Catch ex As Exception
    '                HtlLibrary.HtlLog.InsertLogDetails(ex)
    '            End Try
    '        End If

    '        If e.CommandName = "Reject" Then
    '            Try
    '                ViewState("OrderId") = e.CommandArgument
    '                Acept_grdview.Columns(17).Visible = True
    '                Acept_grdview.Columns(18).Visible = True
    '                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
    '                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
    '                Dim RowIndex As Integer = gvr.RowIndex
    '                ViewState("RowIndex") = RowIndex
    '                Dim txtRemark As TextBox = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '                Dim lnkSubmit As LinkButton = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("lnkSubmit"), LinkButton)
    '                Dim lnkHides As LinkButton = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("lnkHides"), LinkButton)
    '                lnkHides.Visible = True
    '                txtRemark.Visible = True
    '                lnkSubmit.Visible = True
    '                gvr.BackColor = System.Drawing.Color.Yellow
    '            Catch ex As Exception
    '                HtlLibrary.HtlLog.InsertLogDetails(ex)
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        HtlLibrary.HtlLog.InsertLogDetails(ex)
    '    End Try
    'End Sub
    'Protected Sub btnCanFee_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim RowIndex As Integer = Convert.ToInt32(ViewState("RowIndex"))
    '        Dim txtRemark As TextBox = DirectCast(Acept_grdview.Rows(RowIndex).FindControl("txtRemark"), TextBox)
    '        'Update Status and Remark in CancellationIntl Table after Reject
    '        ST.HtlRefundDetail("Rejected", ViewState("OrderId"), Session("UID").ToString, "RejPending", txtRemark.Text.Trim)
    '        Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
    '        BindGrid()
    '    Catch ex As Exception
    '        HtlLibrary.HtlLog.InsertLogDetails(ex)
    '    End Try
    'End Sub
    'Protected Sub lnkHides_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Acept_grdview.Columns(17).Visible = False
    '    Acept_grdview.Columns(18).Visible = False
    '    BindGrid()
    'End Sub

    Protected Sub btnRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemark.Click
        Try
            'Update Status and Remark in CancellationIntl Table after Reject
            Dim i As Integer = ST.HtlRefundUpdates(StatusClass.Rejected.ToString(), Request("OrderIDS"), Session("UID").ToString, "RejPending", Request("txtRemark").Trim())
            If i > 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
                BindGrid()
            End If
        Catch ex As Exception
            ''HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)   
        Try
            Dim lb As LinkButton = CType(sender, LinkButton)
            Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
            gvr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF")
            'Update Status in CancellationIntl Table after Accept
            Dim i As Integer = ST.HtlRefundUpdates(StatusClass.InProcess.ToString(), lb.CommandArgument, Session("UID").ToString, "Modify", "")
            If i > 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Accept succesfully'); ", True)
                BindGrid()
            End If
        Catch ex As Exception
            ''HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub
End Class
