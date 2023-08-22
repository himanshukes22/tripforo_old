Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_IntlAirlineMarkup
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom
    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet
    'Public Agent
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
                'Bind_Data()
                If con.State = ConnectionState.Open Then con.Close()
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                'adp = New SqlDataAdapter("SELECT user_id,Agency_Name FROM new_regs order by Agency_Name", con)
                'adp.Fill(ds)
                'If ds.Tables(0).Rows.Count > 0 Then
                '    uid.DataSource = ds
                '    uid.DataTextField = "Agency_Name"
                '    uid.DataValueField = "user_id"
                '    uid.DataBind()
                'Else
                'End If
                Dim cmd As New SqlCommand()
                cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
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
                'adp = New SqlDataAdapter("SELECT AL_Code,AL_Name FROM AirlineNames order by AL_Name", con)
                da.Fill(ds1)
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
                        GridView1.Columns(7).Visible = False
                    End If
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        'mk.Attributes.Add("onkeypress", "return phone_vali()")
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
    Private Sub Bind_Data()
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
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "I"
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "BINDDATA"
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()

            Dim AgentType As String = GridView1.Rows(e.RowIndex).Cells(2).Text
            Dim Airline As String = GridView1.Rows(e.RowIndex).Cells(3).Text
            Dim Trip As String = GridView1.Rows(e.RowIndex).Cells(4).Text
            Dim Mrkup As String = GridView1.Rows(e.RowIndex).Cells(5).Text
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")

            Dim lblSrNo As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_ID"), Label)
            'cmd.CommandText = "DELETE FROM AdminMarkup WHERE Counter=@cat_id"
            'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = lblSrNo.Text
            cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = AgentType
            cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = DirectCast(GridView1.Rows(e.RowIndex).Cells(6).FindControl("LabelMrkType"), Label).Text
            cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = Mrkup
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = Airline
            cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
            cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = IPAddress
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "I"
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "DELETE"
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = lblSrNo.Text
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data  has been delete successfully!!!');", True)
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

            If DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert" Then

            Else
                Dim AgentType As String = GridView1.Rows(e.RowIndex).Cells(2).Text
                Dim Airline As String = GridView1.Rows(e.RowIndex).Cells(3).Text
                Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
                'Dim cmd1 As New SqlCommand("AirlineMarkup_log", con)
                'cmd1.CommandType = CommandType.StoredProcedure
                'cmd1.Parameters.AddWithValue("@MARKUP", UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text.ToUpper))
                'cmd1.Parameters.AddWithValue("@MARKUPTYPE", UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddl_MarkupTypeE"), DropDownList).SelectedValue.ToUpper))
                'cmd1.Parameters.AddWithValue("@AGENT_TYPE", AgentType)
                'cmd1.Parameters.AddWithValue("@AIRLINE", Airline)
                'cmd1.Parameters.AddWithValue("@UPDATEDBY", Session("UID").ToString())
                'cmd1.Parameters.AddWithValue("@IPADDRESS", IPAddress)
                'cmd1.Parameters.AddWithValue("@CMD_TYPE", "EDIT_INT_AirlineMarkup")
                'cmd1.Parameters.AddWithValue("@TRIPTYPE", "")
                'cmd1.Parameters.AddWithValue("@IATACOMM", "")
                'con.Open()
                'cmd1.ExecuteNonQuery()
                'con.Close()

                Dim lblSrNo As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_ID"), Label)
                'Dim mrkvalue As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_MrkValue"), Label)
                'cmd.CommandText = "UPDATE AdminMarkup SET MarkupValue=@MRK, MarkupType=@MrkType WHERE Counter=@cat_id"
                'cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text.ToUpper)
                'cmd.Parameters.Add("@MrkType", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddl_MarkupTypeE"), DropDownList).SelectedValue.ToUpper)
                'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = lblSrNo.Text
                cmd.CommandText = "USP_AIRLINEMARKUP_DOMINT"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = AgentType
                cmd.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddl_MarkupTypeE"), DropDownList).SelectedValue.ToUpper)
                cmd.Parameters.Add("@MRK", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text.ToUpper)
                cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = Airline
                cmd.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = IPAddress
                cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "I"
                cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATE"
                cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = lblSrNo.Text
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data  has been updated successfully!!!');", True)
            End If
            GridView1.EditIndex = -1
            Bind_Data()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    '    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    '        Try
    '            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '            'Dim da As New SqlDataAdapter("Select user_id from agent_register where user_id='" & DropDownListType.SelectedValue & "' ", con)
    '            'Dim dt As New DataTable()
    '            'con.Open()
    '            'da.Fill(dt)

    '            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    '            Dim da1 As New SqlDataAdapter("Select * from AdminMarkup where (UserId='" & DropDownListType.SelectedValue & "') and (AirlineCode='" & airline_code.SelectedValue & "') ", con)
    '            Dim dt1 As New DataTable()
    '            con1.Open()
    '            da1.Fill(dt1)
    '            con1.Close()
    '            If (dt1.Rows.Count > 0) Then
    '                lbl.Visible = True
    '                lbl.Text = "MarkUp For This AirLine And Agent Is Already Exist."
    '                lbl_msg.Text = ""
    '            Else
    '                Dim txtlenght As Integer = mk.Text.Length
    '                If (RadioButtonList1.SelectedValue = "D" AndAlso txtlenght > 3 OrElse RadioButtonList1.SelectedValue = "I" AndAlso txtlenght > 4) Then
    '                    Response.Write("<script>alert('Markup is not valid')</script>")
    '                    GoTo finish
    '                End If
    '                Dim adap As New SqlDataAdapter("SP_InsertAdminMarkup", con)
    '                adap.SelectCommand.CommandType = CommandType.StoredProcedure
    '                adap.SelectCommand.Parameters.AddWithValue("@uid", DropDownListType.SelectedValue)
    '                adap.SelectCommand.Parameters.AddWithValue("@AL_Code", airline_code.SelectedValue)
    '                adap.SelectCommand.Parameters.AddWithValue("@AL_Name", airline_code.SelectedItem.Text)
    '                adap.SelectCommand.Parameters.AddWithValue("@Mrk_Type", ddl_MarkupType.SelectedValue)
    '                adap.SelectCommand.Parameters.AddWithValue("@Trip", RadioButtonList1.SelectedValue)
    '                adap.SelectCommand.Parameters.AddWithValue("@Mrk_Value", mk.Text.Trim)
    '                adap.SelectCommand.Parameters.AddWithValue("@Dis", "ITZ")
    '                adap.SelectCommand.Parameters.AddWithValue("@Show", "TRN")
    '                adap.SelectCommand.Parameters.AddWithValue("@Type", "AGENT")
    '                Dim dt2 As New DataTable()
    '                adap.Fill(dt2)
    '                lbl_msg.Visible = True
    '                lbl_msg.Text = "Admin Markup Submitted Sucessfully"
    '                lbl.Text = ""
    '                Bind_Data()

    'finish:
    '                Response.Write("")

    '                mk.Text = 0
    '                lbl.Text = ""


    '            End If

    '        Catch ex As Exception
    '            clsErrorLog.LogInfo(ex)

    '        End Try
    '    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd1 As New SqlCommand()
            cmd1.CommandText = "USP_AIRLINEMARKUP_DOMINT"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add("@AGENTTYPE", SqlDbType.VarChar).Value = DropDownListType.SelectedValue
            cmd1.Parameters.Add("@MARKUPTYPE", SqlDbType.VarChar).Value = ""
            cmd1.Parameters.Add("@MRK", SqlDbType.VarChar).Value = ""
            cmd1.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = airline_code.SelectedValue
            cmd1.Parameters.Add("@UPDATEEDBY", SqlDbType.VarChar).Value = ""
            cmd1.Parameters.Add("@IPADDERSS", SqlDbType.VarChar).Value = ""
            cmd1.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = "I"
            cmd1.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "ADDDATA"
            cmd1.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
            cmd1.Connection = con
            Dim da1 As New SqlDataAdapter(cmd1)
            Dim dt1 As New DataTable()
            con.Open()
            da1.Fill(dt1)
            con.Close()
            If (dt1.Rows.Count > 0) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('MarkUp For This AirLine And Agent Is Already Exist.');", True)
            Else
                Dim txtlenght As Integer = mk.Text.Length
                If (RadioButtonList1.SelectedValue = "D" AndAlso txtlenght > 3 OrElse RadioButtonList1.SelectedValue = "I" AndAlso txtlenght > 4) Then
                    Response.Write("<script>alert('Markup is not valid')</script>")
                    GoTo finish
                End If
                Dim adap As New SqlDataAdapter("SP_InsertAdminMarkup", con)
                adap.SelectCommand.CommandType = CommandType.StoredProcedure
                adap.SelectCommand.Parameters.AddWithValue("@uid", DropDownListType.SelectedValue)
                adap.SelectCommand.Parameters.AddWithValue("@AL_Code", airline_code.SelectedValue)
                adap.SelectCommand.Parameters.AddWithValue("@AL_Name", airline_code.SelectedItem.Text)
                adap.SelectCommand.Parameters.AddWithValue("@Mrk_Type", ddl_MarkupType.SelectedValue)
                adap.SelectCommand.Parameters.AddWithValue("@Trip", RadioButtonList1.SelectedValue)
                adap.SelectCommand.Parameters.AddWithValue("@Mrk_Value", mk.Text.Trim)
                adap.SelectCommand.Parameters.AddWithValue("@Dis", "ITZ")
                adap.SelectCommand.Parameters.AddWithValue("@Show", "TRN")
                adap.SelectCommand.Parameters.AddWithValue("@Type", "AGENT")
                Dim dt2 As New DataTable()
                adap.Fill(dt2)
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Admin Markup Submitted successfully');", True)
                Bind_Data()
finish:
                Response.Write("")
                mk.Text = 0
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            'Dim da1 As New SqlDataAdapter("Select * from AdminMarkup where (UserId='" & DropDownListType.SelectedValue & "') and (Trip='" & RadioButtonList1.SelectedValue & "') ", con1)
            'Dim dt1 As New DataTable()
            'con1.Open()
            'da1.Fill(dt1)
            'con1.Close()
            'ViewState("AgentId") = Request("hidtxtAgencyName")
            'GridView1.DataSource = dt1
            'GridView1.DataBind()
            Bind_Data()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class

