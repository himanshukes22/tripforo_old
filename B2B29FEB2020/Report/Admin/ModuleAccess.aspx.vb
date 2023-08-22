
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Partial Class SprReports_Admin_ModuleAccess
    Inherits System.Web.UI.Page
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Private dt As New DataTable()
    Private da As New SqlDataAdapter()


    Public Sub GetdropdownlistItems()
        Try
            da = New SqlDataAdapter("sp_ModuleAccess", con)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    For i As Integer = 0 To dt.Rows.Count - 1
            '        DropDownList1.Items.Add(dt.Rows(i)("USERID").ToString())
            '    Next
            'End If
            DropDownList1.AppendDataBoundItems = True
            DropDownList1.Items.Clear()
            DropDownList1.Items.Insert(0, "--Select--")
            DropDownList1.DataSource = dt
            DropDownList1.DataTextField = "USERID"
            DropDownList1.DataValueField = "USERID"
            DropDownList1.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        'Dim userID As String = DropDownList1.SelectedValue
        'da = New SqlDataAdapter("sp_GetModuleAcess", con)
        'da.SelectCommand.CommandType = CommandType.StoredProcedure
        'da.SelectCommand.Parameters.AddWithValue("@USERID", userID)
        'da.Fill(dt)

        'GridView1.Visible = True
        'GridView1.DataSource = dt
        'GridView1.DataBind()

        BindGrid(DropDownList1.SelectedValue)

    End Sub
    Private Sub BindGrid(ByVal UserID As String)
        da = New SqlDataAdapter("sp_GetModuleAcess", con)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.Parameters.AddWithValue("@USERID", UserID)
        da.Fill(dt)

        GridView1.Visible = True
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_Command(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim i As Integer = 0
        Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim lbl_COUNTER As Label = DirectCast(rw.FindControl("lbl_COUNTER"), Label)
        'Dim lbl_SEVTYPE As Label = DirectCast(rw.FindControl("lbl_AIRLINECODE"), Label)
        Dim FLAGE As Boolean
        If e.CommandName = "ON" Then
            FLAGE = True
            i = UPDATESTATUS(FLAGE, Convert.ToInt32(lbl_COUNTER.Text.Trim()))
            ' lbtn.Text = "False"
        End If
        If e.CommandName = "OFF" Then
            FLAGE = False
            i = UPDATESTATUS(FLAGE, Convert.ToInt32(lbl_COUNTER.Text.Trim()))
        End If
        BindGrid(DropDownList1.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetdropdownlistItems()
        End If
    End Sub
    Protected Sub GridRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            'If (e.Row.RowType = DataControlRowType.DataRow) Then

            '    If DataBinder.Eval(e.Row.DataItem, "TBLNAME").ToString() = "SSW" Then
            '        e.Row.CssClass = "GridViewStyle ONGRID"
            '    Else
            '        ' e.Row.CssClass = "OFFGRID GridViewStyle"
            '        'e.Row.Enabled = False
            '        e.Row.CssClass = "GridViewStyle OFFGRID"
            '        e.Row.Attributes.Add("Style", "background: url(../../images/coupon.png)  5px center;")
            '    End If
            'End If
            'e.Row.CssClass = "GridViewStyle"
            e.Row.Height = 20
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' Dim l As LinkButton = DirectCast(e.Row.FindControl("LB_Reject"), LinkButton)
                'Dim TravelType As Label = DirectCast(e.Row.FindControl("lbl_TravelType"), Label)
                Dim l1 As LinkButton = DirectCast(e.Row.FindControl("lnk_ON"), LinkButton)
                Dim l2 As LinkButton = DirectCast(e.Row.FindControl("lnk_OFF"), LinkButton)


                'Dim lon As ImageButton = DirectCast(e.Row.FindControl("img_ON"), ImageButton)
                'Dim loff As ImageButton = DirectCast(e.Row.FindControl("img_OFF"), ImageButton)


                If DataBinder.Eval(e.Row.DataItem, "STATUS").ToString() = "True" Then
                    'l2.BackColor = Drawing.Color.Gray
                    'l1.Text = "ON"
                    'l1.ForeColor = Drawing.Color.GhostWhite
                    'l1.Font.Bold = True
                    'l1.Enabled = False
                    'l1.Font.Underline = False
                    'l2.Text = "OFF"
                    'l2.ForeColor = Drawing.Color.Tan


                    l1.Visible = True
                    l2.Visible = False

                Else
                    'l1.BackColor = Drawing.Color.Gray
                    'l1.Text = "ON"
                    'l1.ForeColor = Drawing.Color.Tan
                    'l2.Text = "OFF"
                    'l2.ForeColor = Drawing.Color.GhostWhite
                    'l2.Font.Bold = True
                    'l2.Enabled = False
                    'l2.Font.Underline = False

                    l2.Visible = True
                    l1.Visible = False
                End If

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Function UPDATESTATUS(ByVal STATUS As Boolean, ByVal COUNTER As Integer) As Integer
        Dim flag As Integer = 0
        Try

            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()
            Dim cmd As SqlCommand
            cmd = New SqlCommand("SP_UPDATEMODULEACCESS", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@STATUS", STATUS)
            cmd.Parameters.AddWithValue("@COUNTER", COUNTER)
            flag = cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
        Return flag
    End Function
End Class
