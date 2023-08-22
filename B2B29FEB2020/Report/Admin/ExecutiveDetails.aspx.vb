Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Partial Class Reports_Admin_ExecutiveDetails
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type").ToString().ToUpper() <> "ADMIN" Then
                Response.Redirect("~/Login.aspx")
            End If
            LabelMsg.Visible = False
            If Not IsPostBack Then
                BindData()
                ''trip.Visible = False
            End If
            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.EXECDETAILS.ToString()).Tables(0)
            If (dtmodule.Rows.Count > 0) Then
                For Each dr As DataRow In dtmodule.Rows
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.INSERT.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        btnAdd.Visible = False
                    End If
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.UPDATE.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        GridView1.Columns(0).Visible = False
                    End If
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.DELETE.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        GridView1.Columns(5).Visible = False
                    End If
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindData()
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim da As New SqlDataAdapter("SELECT * from Execu  where status=1", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        Try
            GridView1.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            GridView1.EditIndex = -1
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        If DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert" Then
            Try
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                Dim cmd As New SqlCommand()
                cmd.CommandText = "INSERT INTO Execu(user_id,user_pwd,Dept) VALUES(@cat_name,@curr,@val1)"
                cmd.Parameters.Add("@cat_name", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(2).Controls(0), TextBox).Text
                cmd.Parameters.Add("@curr", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text
                cmd.Parameters.Add("@val1", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        Else
            Try
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                Dim cmd As New SqlCommand()
                cmd.CommandText = "UPDATE Execu SET user_pwd=@curr WHERE counter=@cat_id"
                ''cmd.Parameters.Add("@cat_name", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).FindControl("labeluserId"), Label).Text
                cmd.Parameters.Add("@curr", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text
                ''cmd.Parameters.Add("@val1", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).), DropDownList).SelectedValue
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).FindControl("labelsrno"), Label).Text)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If
        Try
            GridView1.EditIndex = -1
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try


            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim da As New SqlDataAdapter("Select* from Execu where status=1", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            Dim dr As DataRow = dt.NewRow()
            dt.Rows.InsertAt(dr, 0)
            GridView1.EditIndex = 0
            GridView1.DataSource = dt
            GridView1.DataBind()
            DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert"
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_Create_Executive"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = TextBoxEmail.Text.Trim()
            cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 500).Value = TextBoxMobileNo.Text.Trim()
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = TextBoxName.Text.Trim()
            cmd.Parameters.Add("@Dept", SqlDbType.VarChar, 1).Value = DropDownListStaffType.SelectedValue.Trim()
            cmd.Parameters.Add("@Trip", SqlDbType.VarChar, 1).Value = DropDownListTrip.SelectedValue.Trim()
            cmd.Parameters.Add("@Pass", SqlDbType.NVarChar, 500).Value = TextBoxPass.Text.Trim()
            cmd.Parameters.Add("@cmdType", SqlDbType.NVarChar, 500).Value = "delete"
            cmd.Parameters.Add("@userCounter", SqlDbType.BigInt, 500).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).FindControl("labelsrno"), Label).Text)

            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    '    Try
    'Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    'Dim cmd As New SqlCommand()
    '        cmd.CommandText = "DELETE FROM Execu WHERE counter=@cat_id"
    '        cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(GridView1.Rows(e.RowIndex).Cells(1).Text)
    '        cmd.Connection = con
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()
    '        BindData()
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    ''End Sub


    Protected Sub DropDownListStaffType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListStaffType.SelectedIndexChanged

        'If DropDownListStaffType.SelectedValue.ToUpper().Trim() = "E" Then
        '    trip.Visible = True
        'Else
        '    trip.Visible = False

        'End If


    End Sub

    Public Function GetDeptname(dept As String) As String
        If dept.ToUpper.Trim() = "E" Then
            Return "Operational Staff"
        Else
            Return "Accounts Staff"

        End If

    End Function
    'zczxc'

    Protected Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_Create_Executive"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = TextBoxEmail.Text.Trim()
            cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 500).Value = TextBoxMobileNo.Text.Trim()
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = TextBoxName.Text.Trim()
            cmd.Parameters.Add("@Dept", SqlDbType.VarChar, 1).Value = DropDownListStaffType.SelectedValue.Trim()
            cmd.Parameters.Add("@Trip", SqlDbType.VarChar, 1).Value = DropDownListTrip.SelectedValue.Trim()
            cmd.Parameters.Add("@Pass", SqlDbType.NVarChar, 500).Value = TextBoxPass.Text.Trim()
            cmd.Parameters.Add("@cmdType", SqlDbType.NVarChar, 500).Value = "insert"
            cmd.Parameters.Add("@userCounter", SqlDbType.BigInt, 500).Value = 0
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            BindData()
            LabelMsg.Visible = True
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class

