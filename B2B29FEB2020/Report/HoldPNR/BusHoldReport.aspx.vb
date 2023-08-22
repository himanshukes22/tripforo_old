Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class SprReports_HoldPNR_BusHoldReport
    Inherits System.Web.UI.Page
    Private ID As New IntlDetails
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As SqlDataAdapter
    Protected Sub GrdBusReport_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdBusReport.RowCommand
        Try


            If e.CommandName = "Accept" Then
                Dim AOrderid As String = e.CommandArgument.ToString()
                con.Open()
                Dim cmd As SqlCommand
                cmd = New SqlCommand("SP_RB_GET_HOLD_BOOKING", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@OrderId", AOrderid)
                cmd.Parameters.AddWithValue("@ExecId", Session("UID").ToString())
                cmd.Parameters.AddWithValue("@Type", "StatusConfirm")
                cmd.Parameters.AddWithValue("@RejectRemark", "")
                Dim i As Integer = cmd.ExecuteNonQuery()
                con.Close()
                If (i >= 1) Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "<script type='text/javascript'>alert('OrderId " & AOrderid & " Booked successfully');</script>", False)
                    BindGrid()
                End If

            End If
            If e.CommandName = "Reject" Then

                td_Reject.Visible = True

                Dim ROrderid As String = e.CommandArgument.ToString()
                Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim lblFare As Label = DirectCast(rw.FindControl("lblFare"), Label)

                ViewState("Fare") = lblFare.Text
                ViewState("ID") = ROrderid

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
            If Session("UserType") = "AD" Then
                GrdBusReport.Columns(11).Visible = False


            End If


        End If

    End Sub
    Public Sub BindGrid()
        Try
            Dim ds As New DataSet()
            adap = New SqlDataAdapter("SP_RB_GET_HOLD_BOOKING", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@Type", "All")
            adap.SelectCommand.Parameters.AddWithValue("@ExecId", "")
            adap.SelectCommand.Parameters.AddWithValue("@OrderId", "")
            adap.SelectCommand.Parameters.AddWithValue("@RejectRemark", "")

            adap.Fill(ds)

            GrdBusReport.DataSource = ds
            GrdBusReport.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub btn_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Comment.Click
        Try
            con.Open()
            Dim cmd As SqlCommand
            cmd = New SqlCommand("SP_RB_GET_HOLD_BOOKING", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@OrderId", ViewState("ID").ToString())
            cmd.Parameters.AddWithValue("@ExecId", Session("UID").ToString())
            cmd.Parameters.AddWithValue("@RejectRemark", txt_Reject.Text.Trim())

            cmd.Parameters.AddWithValue("@Type", "StatusRejected")
            Dim str As String = cmd.ExecuteScalar()
            con.Close()
            If str IsNot Nothing Then

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "<script type='text/javascript'>alert('OrderId " & ViewState("ID").ToString() & " Rejected successfully');</script>", False)

                Dim dtID As New DataSet()
                dtID = STDom.GetAgencyDetails(str)
                Dim Aval_Bal As Double = ST.AddCrdLimit(dtID.Tables(0).Rows(0)("User_Id").ToString(), Convert.ToDecimal(ViewState("Fare").ToString()))
                Dim y As Integer = STDom.insertLedgerDetails(dtID.Tables(0).Rows(0)("User_Id").ToString(), dtID.Tables(0).Rows(0)("Agency_Name").ToString(), ViewState("ID").ToString(), ViewState("ID").ToString(), "", "", "", "", Session("UID").ToString(), Request.UserHostAddress, 0, Convert.ToDouble(ViewState("Fare").ToString()), Aval_Bal, "Bus Booking Rejection", "Bus PNR Rejected Against OrderID=" & ViewState("ID").ToString(), 0)
                txt_Reject.Text = ""
                td_Reject.Visible = False
                BindGrid()
            End If




        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Try
            td_Reject.Visible = False
            BindGrid()
            txt_Reject.Text = ""

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
