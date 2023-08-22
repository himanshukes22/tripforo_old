Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Agent_agent_markup
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet   
    Public dst As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not Page.IsPostBack Then
                Try
                    BindData()
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
                mk.Text = "0"
            End If
            uid.Text = Session("UID").ToString.Trim
            mk.Attributes.Add("onkeypress", "return phone_vali()")
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Private Sub BindData()
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim dst As New DataSet()
            Using sqlcmd As New SqlCommand()
                sqlcmd.Connection = con
                If con.State = ConnectionState.Open Then
                    con.Close()
                Else
                    con.Open()
                End If
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.CommandText = "USP_AgentMarkup"
                sqlcmd.Parameters.AddWithValue("@cmd", "select")
                sqlcmd.Parameters.AddWithValue("@userid", Session("UID").ToString.Trim)
                con.Close()
                Dim da As New SqlDataAdapter(sqlcmd)
                da.Fill(dst)
                GridView1.DataSource = dst
                GridView1.DataBind()
            End Using

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
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            Dim txtvalue As TextBox = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txt_markupvalue"), TextBox)
            Dim lbl_counter As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_counter"), Label)
            Dim txtmartype As DropDownList = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddl_mktyp"), DropDownList)
            If txtvalue.Text <> "" Then
                If DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert" Then
                Else
                    Using sqlcmd As New SqlCommand()
                        sqlcmd.Connection = con
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        Else
                            con.Open()
                        End If
                        sqlcmd.CommandType = CommandType.StoredProcedure
                        sqlcmd.CommandText = "USP_AgentMarkup"
                        sqlcmd.Parameters.AddWithValue("@cmd", "update")
                        sqlcmd.Parameters.AddWithValue("@markup", txtvalue.Text)
                        sqlcmd.Parameters.AddWithValue("@markuptype", txtmartype.SelectedValue)
                        sqlcmd.Parameters.AddWithValue("@cat_id", Convert.ToInt64(lbl_counter.Text))
                        sqlcmd.Parameters.AddWithValue("@updatedby", Session("UID").ToString.Trim)
                        sqlcmd.ExecuteNonQuery()
                        con.Close()
                        ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(1);", True)
                        GridView1.EditIndex = -1
                        BindData()
                    End Using
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(2);", True)
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub


    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try
            Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
            Dim lbl_counter As Label = DirectCast(row.FindControl("lbl_counter"), Label)
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Using sqlcmd As New SqlCommand()
                sqlcmd.Connection = con
                If con.State = ConnectionState.Open Then
                    con.Close()
                Else
                    con.Open()
                End If
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.CommandText = "USP_AgentMarkup"
                sqlcmd.Parameters.AddWithValue("@cmd", "delete")
                sqlcmd.Parameters.AddWithValue("@cat_id", Convert.ToInt64(lbl_counter.Text))
                sqlcmd.ExecuteNonQuery()
                con.Close()
                BindData()
            End Using
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Using sqlcmd As New SqlCommand()
                sqlcmd.Connection = con
                If con.State = ConnectionState.Open Then
                    con.Close()
                Else
                    con.Open()
                End If
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.CommandText = "USP_AgentMarkup"
                sqlcmd.Parameters.AddWithValue("@cmd", "check")
                sqlcmd.Parameters.AddWithValue("@userid", Session("UID").ToString.Trim)
                sqlcmd.Parameters.AddWithValue("@airline", air.SelectedItem.Value.Trim.ToUpper)
                Dim da As New SqlDataAdapter(sqlcmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                Dim cmd As New SqlCommand()
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(3);", True)
                    mk.Text = "0"
                Else
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "USP_AgentMarkup"
                    cmd.Parameters.AddWithValue("@cmd", "insert")
                    cmd.Parameters.AddWithValue("@userid", uid.Text.Trim)
                    cmd.Parameters.AddWithValue("@airline", air.SelectedItem.Value.Trim.ToUpper)
                    cmd.Parameters.AddWithValue("@markup", mk.Text.Trim)
                    cmd.Parameters.AddWithValue("@markuptype", ddl_MarkupType.SelectedValue.Trim())
                    cmd.Parameters.AddWithValue("@updatedby", Session("UID").ToString.Trim)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(4);", True)
                    con.Close()
                    BindData()
                End If
               
            End Using
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub


End Class

