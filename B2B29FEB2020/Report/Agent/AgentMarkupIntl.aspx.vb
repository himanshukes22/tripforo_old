Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Agent_AgentMarkupIntl
    Inherits System.Web.UI.Page


    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            Try
                Bind_Data()
                txt_AgentID.Text = Session("UID").ToString()
                If con.State = ConnectionState.Open Then con.Close()
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Dim cmd As New SqlCommand()
                cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
                cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "I"
                cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SELECT"
                cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
                cmd.Connection = con
                Dim da As New SqlDataAdapter(cmd)
                Dim ds1 As New DataSet
                da.Fill(ds1)
                airline_code.AppendDataBoundItems = True
                airline_code.Items.Clear()
                airline_code.Items.Insert(0, "ALL")
                If ds1.Tables(0).Rows.Count > 0 Then
                    airline_code.DataSource = ds1
                    airline_code.DataTextField = "AL_Name"
                    airline_code.DataValueField = "AL_Code"
                    airline_code.DataBind()
                Else
                End If
                lbl.Visible = False
                lbl_msg.Visible = False
                mk.Text = "0"


            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
    End Sub
    Private Sub Bind_Data()
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
                sqlcmd.CommandText = "USP_AgentMarkupIntl"
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

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
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
                sqlcmd.CommandText = "USP_AgentMarkupIntl"
                sqlcmd.Parameters.AddWithValue("@cmd", "delete")
                sqlcmd.Parameters.AddWithValue("@cat_id", Convert.ToInt64(lbl_counter.Text))
                sqlcmd.ExecuteNonQuery()
                con.Close()
                Bind_Data()
            End Using
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        Try
            GridView1.EditIndex = -1
            Bind_Data()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            GridView1.EditIndex = e.NewEditIndex
            Bind_Data()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
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
                        sqlcmd.CommandText = "USP_AgentMarkupIntl"
                        sqlcmd.Parameters.AddWithValue("@cmd", "update")
                        sqlcmd.Parameters.AddWithValue("@markup", txtvalue.Text)
                        sqlcmd.Parameters.AddWithValue("@markuptype", txtmartype.SelectedValue)
                        sqlcmd.Parameters.AddWithValue("@cat_id", Convert.ToInt64(lbl_counter.Text))
                        sqlcmd.Parameters.AddWithValue("@updatedby", Session("UID").ToString.Trim)
                        sqlcmd.ExecuteNonQuery()
                        con.Close()
                        ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(1);", True)
                        GridView1.EditIndex = -1
                        Bind_Data()
                    End Using
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(2);", True)
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
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
                sqlcmd.CommandText = "USP_AgentMarkupIntl"
                sqlcmd.Parameters.AddWithValue("@cmd", "check")
                sqlcmd.Parameters.AddWithValue("@userid", Session("UID").ToString.Trim)
                sqlcmd.Parameters.AddWithValue("@airline", airline_code.SelectedItem.Value.Trim.ToUpper)
                sqlcmd.Parameters.AddWithValue("@trip", "I")
                Dim da As New SqlDataAdapter(sqlcmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                Dim cmd As New SqlCommand()
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(3);", True)
                    mk.Text = "0"
                Else
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "USP_AgentMarkupIntl"
                    cmd.Parameters.AddWithValue("@cmd", "insert")
                    cmd.Parameters.AddWithValue("@userid", Session("UID").ToString())
                    cmd.Parameters.AddWithValue("@airlinecode", airline_code.SelectedItem.Value.Trim.ToUpper)
                    cmd.Parameters.AddWithValue("@airline", airline_code.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@markup", mk.Text.Trim)
                    cmd.Parameters.AddWithValue("@markuptype", ddl_MarkupType.SelectedValue.Trim())
                    cmd.Parameters.AddWithValue("@updatedby", Session("UID").ToString.Trim)
                    cmd.Parameters.AddWithValue("@trip", "I")
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "key", "MyFunc(4);", True)
                    con.Close()
                    Bind_Data()
                End If

            End Using
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub


    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            Bind_Data()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class

