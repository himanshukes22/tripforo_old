

Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration


Partial Class SprReports_Admin_PreHoldDetails
    Inherits System.Web.UI.Page

    Private ID As New IntlDetails
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim adap As SqlDataAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        Try
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub BindGrid()

        'Dim OrderId As String = Request.QueryString("ProxyID")
        Try


            GridHoldPNRAccept.DataSource = USP_PreHoldMGMT("", "", "Select", "")
            GridHoldPNRAccept.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub GridHoldPNRAccept_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridHoldPNRAccept.RowCommand

        Try


            

            If e.CommandName = "Reject" Then

                Dim Orderid As String = e.CommandArgument.ToString()
                Dim ds As New DataSet()

                Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                gvr.BackColor = System.Drawing.Color.Yellow

                td_Reject.Visible = True
                Dim ID As String = e.CommandArgument.ToString()
                ViewState("ID") = ID
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

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

    Protected Sub btn_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Comment.Click
        Try
            Dim OrderID As String = ""
            OrderID = ViewState("ID").ToString()



            If txt_Reject.Text IsNot Nothing AndAlso txt_Reject.Text <> "" Then

                Dim ds As New DataSet()

                Dim dtID As New DataTable()
               
                ds = USP_PreHoldMGMT(OrderID, txt_Reject.Text, "Update", Convert.ToString(Session("UID")))
                If ds IsNot Nothing And ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0)(0).ToString() = "1" Then
                        Response.Write("<script>alert('Status Succesfully Updated')</script>")
                    End If

                    BindGrid()
                    txt_Reject.Text = ""

                    td_Reject.Visible = False

                    BindGrid()

                End If

               

            Else
                Response.Write("<script>alert('Remark cannot be blank.')</script>")


            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridHoldPNRAccept_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridHoldPNRAccept.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim l As LinkButton = DirectCast(e.Row.FindControl("LB_Reject"), LinkButton)
        '    l.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to Reject Proxy ID " & DataBinder.Eval(e.Row.DataItem, "ProxyID") & "')")
        'End If
        Try
            If Session("User_Type") = "ADMIN" Then
                If (e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate) AndAlso (e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header) Then

                    e.Row.Cells(15).Visible = False
                End If
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub GridHoldPNRAccept_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridHoldPNRAccept.PageIndexChanging
        Try
            GridHoldPNRAccept.PageIndex = e.NewPageIndex
            BindGrid()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

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


