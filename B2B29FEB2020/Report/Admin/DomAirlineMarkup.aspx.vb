Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_DomAirlineMarkup
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet
    Public Agent
    Public dst As New DataSet
    Private STDom As New SqlTransactionDom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            Try
                Dim msg As String = ""
                DropDownListType.DataSource = GroupTypeMGMT("", "", "MultipleSelect", msg)
                DropDownListType.DataTextField = "GroupType"
                DropDownListType.DataValueField = "GroupType"
                DropDownListType.DataBind()

                'BindData()
                'If con.State = ConnectionState.Open Then con.Close()
                'con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                'adp = New SqlDataAdapter("SELECT user_id,Agency_Name FROM new_regs order by Agency_Name", con)
                'adp.Fill(ds)
                'If ds.Tables(0).Rows.Count > 0 Then
                '    cbo.DataSource = ds
                '    cbo.DataTextField = "Agency_Name"
                '    cbo.DataValueField = "user_id"
                '    cbo.DataBind()

                '    uid.DataSource = ds
                '    uid.DataTextField = "Agency_Name"
                '    uid.DataValueField = "user_id"
                '    uid.DataBind()
                'Else
                'End If
                mk.Text = "0"
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
        Try
            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.DISCOUNT_I.ToString()).Tables(0)
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
        mk.Attributes.Add("onkeypress", "return phone_vali()")
    End Sub

    Public Function GroupTypeMGMT(ByVal type As String, ByVal desc As String, ByVal cmdType As String, ByRef msg As String) As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable()
        Try
            con.Open()
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_agentTypeMGMT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 200).Value = type
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 500).Value = desc
            cmd.Parameters.Add("@cmdType", SqlDbType.VarChar, 50).Value = cmdType
            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 500)
            cmd.Parameters("@msg").Direction = ParameterDirection.Output
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            msg = cmd.Parameters("@msg").Value.ToString().Trim()
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()
        End Try
        Return dt
    End Function
    Private Sub BindData()
        'Try
        '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        '    Dim da As New SqlDataAdapter("SELECT counter,user_id,Airline,MarkUp,MarkupType FROM Mrk_admin where user_id='" & DropDownListType.SelectedValue & "'", con)
        '    da.Fill(dst)
        '    GridView1.DataSource = dst
        '    GridView1.DataBind()
        'Catch ex As Exception
        '    clsErrorLog.LogInfo(ex)
        'End Try
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
            cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "D"
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "BINDDATA"
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dst)
            GridView1.DataSource = dst
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
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            If DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert" Then
            Else
                Dim AgentType As String = GridView1.Rows(e.RowIndex).Cells(2).Text
                Dim Airline As String = GridView1.Rows(e.RowIndex).Cells(3).Text
                Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")

                cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = AgentType
                cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).FindControl("ddl_MarkupTypeE"), DropDownList).SelectedValue.Trim.ToUpper)
                cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text.ToUpper)
                cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = Airline
                cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = IPAddress
                cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "D"
                cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATE"
                cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblSRNO"), Label).Text)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data has been updated successfully!!');", True)
                'cmd.CommandText = "UPDATE Mrk_admin SET MarkUp=@MRK,MarkupType=@MarkupType WHERE counter=@cat_id"
                'cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text.ToUpper)
                'cmd.Parameters.Add("@MarkupType", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).FindControl("ddl_MarkupTypeE"), DropDownList).SelectedValue.Trim.ToUpper)
                'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblSRNO"), Label).Text)
                'cmd.Connection = con
                'con.Open()
                'cmd.ExecuteNonQuery()
                'con.Close()
            End If
            GridView1.EditIndex = -1
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()

            Dim AgentType As String = GridView1.Rows(e.RowIndex).Cells(2).Text
            Dim Airline As String = GridView1.Rows(e.RowIndex).Cells(3).Text
            Dim MARKUPTYPE As String = GridView1.Rows(e.RowIndex).Cells(4).Text
            Dim MRK As String = GridView1.Rows(e.RowIndex).Cells(5).Text
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
            cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = AgentType
            cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = MARKUPTYPE
            cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(5).FindControl("LabelMrkType"), Label).Text
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = Airline
            cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
            cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = IPAddress
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "D"
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "DELETE"
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblSRNO"), Label).Text)
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data has been deleted successfully !!!');", True)
            'cmd.CommandText = "DELETE FROM Mrk_admin WHERE counter=@cat_id"
            'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(DirectCast(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblSRNO"), Label).Text)
            'cmd.Connection = con
            'con.Open()
            'cmd.ExecuteNonQuery()
            'con.Close()
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    'Private Sub Bind_Data()
    '    Try
    '        Agent = Request("hidtxtAgencyName")
    '        ViewState("AgentId") = Agent
    '        Session("Agent") = Agent
    '        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '        Dim da As New SqlDataAdapter("SELECT counter,user_id,Airline,MarkUp,MarkupType FROM Mrk_admin where user_id='" & DropDownListType.SelectedValue & "'", con)
    '        Dim dt As New DataTable()
    '        da.Fill(dt)
    '        GridView1.DataSource = dt
    '        GridView1.DataBind()
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub

    'Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click

    '    Try
    '        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '        'Dim da As New SqlDataAdapter("Select user_id from agent_register where user_id='" & Request("hidtxtAgencyName") & "' ", con)
    '        'Dim dt As New DataTable()
    '        con.Open()
    '        'da.Fill(dt)

    '        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '        Dim da1 As New SqlDataAdapter("Select * from Mrk_admin where (user_id='" & DropDownListType.SelectedValue & "') and (Airline='" & air.SelectedItem.Value.Trim.ToUpper & "') ", con)
    '        Dim dt1 As New DataTable()
    '        con1.Open()
    '        da1.Fill(dt1)
    '        con1.Close()

    '        Dim cmd As New SqlCommand()
    '        ''If dt.Rows.Count > 0 Then
    '        If dt1.Rows.Count > 0 Then
    '            lbl.Text = "MarkUp For This AirLine And Agent Type Is Already Exist."
    '        Else
    '            cmd.CommandText = "INSERT INTO Mrk_admin(user_id,Airline,MarkUp,MarkupType) VALUES(@UID,@AL,@MRK,@MarkupType)"
    '            cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = DropDownListType.SelectedValue '' Request("hidtxtAgencyName")
    '            cmd.Parameters.Add("@AL", SqlDbType.VarChar).Value = air.SelectedItem.Value.Trim.ToUpper
    '            cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = mk.Text.Trim
    '            cmd.Parameters.Add("@MarkupType", SqlDbType.VarChar).Value = ddl_MarkupType.SelectedValue.Trim()
    '            cmd.Connection = con
    '            cmd.ExecuteNonQuery()
    '            lbl.Text = ""
    '        End If

    '        'Else
    '        'lbl.Text = "InCorrect User ID"
    '        'End If
    '        con.Close()
    '        BindData()
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click

        Try
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
            cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = air.SelectedItem.Value.Trim.ToUpper
            cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
            cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "D"
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SELECT"
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
            cmd.Connection = con1
            Dim da1 As New SqlDataAdapter(cmd)
            Dim dt1 As New DataTable()
            con1.Open()
            da1.Fill(dt1)
            con1.Close()

            If dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('MarkUp For This AirLine And Agent Type Is Already Exist.!!!');", True)
            Else
                Dim cmd1 As New SqlCommand()
                cmd1.CommandText = "USP_AIRLINEMARKUP_DOMINT"
                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
                cmd1.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = ddl_MarkupType.SelectedValue.Trim()
                cmd1.Parameters.Add("@MRK", SqlDbType.VarChar).Value = mk.Text.Trim
                cmd1.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = air.SelectedItem.Value.Trim.ToUpper
                cmd1.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd1.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = ""
                cmd1.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "D"
                cmd1.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "INSERT"
                cmd1.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
                cmd1.Connection = con1
                con1.Open()
                cmd1.ExecuteNonQuery()
                con1.Close()
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data  has been updated successfully!!!');", True)
            End If
            con.Close()
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Try
            BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class

