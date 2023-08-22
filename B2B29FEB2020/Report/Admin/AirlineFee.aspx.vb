Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_AirlineFee
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet
    Private STDom As New SqlTransactionDom


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            Try
                Bind_Data()
                If con.State = ConnectionState.Open Then con.Close()
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Dim ds1 As New DataSet
                Dim cmd As New SqlCommand()
                Dim da As New SqlDataAdapter(cmd)
                cmd.CommandText = "USP_AIRLINEFEE"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@AIRLINE_CODE", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@SERVICETAX", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@TRANSFEE", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@IATTACOMM", SqlDbType.VarChar).Value = ""
                cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0
                cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "AIRLINE"
                cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
                cmd.Connection = con
                da.Fill(ds1)
                'da = New SqlDataAdapter("SELECT AL_Code,AL_Name FROM AirlineNames", con)
                'da.Fill(ds1)
                If ds1.Tables(0).Rows.Count > 0 Then
                    airline_code.DataSource = ds1
                    airline_code.DataTextField = "AL_Name"
                    airline_code.DataValueField = "AL_Code"
                    airline_code.DataBind()
                Else
                End If
                txt_SeviceTax.Text = "0"
                txt_TrasFee.Text = "0"
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
            
        End If
        Try
            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.SERVICETAX.ToString()).Tables(0)
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
    End Sub
    Private Sub Bind_Data()
        Try
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            ' Dim da As New SqlDataAdapter("SELECT Counter,AirlineCode,SrvTax,TranFee,IATAComm,Trip FROM ServiceTaxInt order by AirlineCode ", con)
            'Dim ds1 As New DataSet

            Dim adap As New SqlDataAdapter("USP_AIRLINEFEE", con1)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@DOMINT", RadioButtonList1.SelectedValue.Trim)
            adap.SelectCommand.Parameters.AddWithValue("@AIRLINE_CODE", "")
            adap.SelectCommand.Parameters.AddWithValue("SERVICETAX", "")
            adap.SelectCommand.Parameters.AddWithValue("@TRANSFEE", "")
            adap.SelectCommand.Parameters.AddWithValue("@IATTACOMM", "")
            adap.SelectCommand.Parameters.AddWithValue("@COUNTER", 0)
            adap.SelectCommand.Parameters.AddWithValue("@CMD_TYPE", "BINDDATA")
            adap.SelectCommand.Parameters.AddWithValue("@USERID", Session("UID").ToString)
            adap.SelectCommand.Parameters.AddWithValue("@IPADDRESS", "")
            Dim dt2 As New DataTable()
            adap.Fill(dt2)
            GridView1.DataSource = dt2
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
            Dim lblSrNo As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_ID"), Label)

            Dim AirCode As String = GridView1.Rows(e.RowIndex).Cells(2).Text
            Dim ServTax As String = GridView1.Rows(e.RowIndex).Cells(3).Text
            Dim TrnsFee As String = GridView1.Rows(e.RowIndex).Cells(4).Text
            Dim IATA As String = GridView1.Rows(e.RowIndex).Cells(5).Text
            Dim Trip As String = GridView1.Rows(e.RowIndex).Cells(6).Text

            Dim da As New SqlDataAdapter(cmd)
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
            cmd.CommandText = "USP_AIRLINEFEE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = Trip
            cmd.Parameters.Add("@AIRLINE_CODE", SqlDbType.VarChar).Value = AirCode
            cmd.Parameters.Add("@SERVICETAX", SqlDbType.VarChar).Value = ServTax
            cmd.Parameters.Add("@TRANSFEE", SqlDbType.VarChar).Value = TrnsFee
            cmd.Parameters.Add("@IATTACOMM", SqlDbType.VarChar).Value = IATA
            cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = lblSrNo.Text
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "DELETE"
            cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Session("UID").ToString
            cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress
            'cmd.CommandText = "DELETE FROM ServiceTaxInt WHERE Counter=@cat_id"
            'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = lblSrNo.Text
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Bind_Data()
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data deleted successfully !!');", True)
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

                Dim lblSrNo As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_ID"), Label)
                Dim AirlineCode As String = GridView1.Rows(e.RowIndex).Cells(2).Text
                'Dim mrkvalue As Label = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbl_MrkValue"), Label)
                'cmd.CommandText = "UPDATE ServiceTaxInt SET SrvTax=@STax,TranFee=@TFee,IATAComm=@IATAComm WHERE Counter=@cat_id"
                'cmd.Parameters.Add("@STax", SqlDbType.Decimal).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text.ToUpper)
                'cmd.Parameters.Add("@TFee", SqlDbType.Decimal).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text.ToUpper)
                'cmd.Parameters.Add("@IATAComm", SqlDbType.Int).Value = Convert.ToInt16(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text)
                'cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt16(lblSrNo.Text)
                'Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
                Dim da As New SqlDataAdapter(cmd)
                Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
                cmd.CommandText = "USP_AIRLINEFEE"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DOMINT", SqlDbType.VarChar).Value = RadioButtonList1.SelectedValue
                cmd.Parameters.Add("@AIRLINE_CODE", SqlDbType.VarChar).Value = AirlineCode
                cmd.Parameters.Add("@SERVICETAX", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text.ToUpper)
                cmd.Parameters.Add("@TRANSFEE", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text.ToUpper)
                cmd.Parameters.Add("@IATTACOMM", SqlDbType.VarChar).Value = Convert.ToInt16(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text)
                cmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = lblSrNo.Text
                cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATED"
                cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
            GridView1.EditIndex = -1

        Catch ex As Exception

            clsErrorLog.LogInfo(ex)

        End Try

        Bind_Data()
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data updated successfully !!');", True)
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click


        Try
            'Dim da As New SqlDataAdapter("Select user_id from new_regs where user_id='" & uid.SelectedItem.Text.Trim & "' ", con)
            'Dim dt As New DataTable()
            'con.Open()
            'da.Fill(dt)
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            'Dim da1 As New SqlDataAdapter("Select * from ServiceTaxInt where  (AirlineCode='" & airline_code.SelectedValue & "') and Trip='" & RadioButtonList1.SelectedValue & "'  ", con1)
            'Dim dt1 As New DataTable()
            Dim adp As New SqlDataAdapter("USP_AIRLINEFEE", con1)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@DOMINT", RadioButtonList1.SelectedValue.Trim)
            adp.SelectCommand.Parameters.AddWithValue("@AIRLINE_CODE", airline_code.SelectedValue.Trim)
            adp.SelectCommand.Parameters.AddWithValue("SERVICETAX", "")
            adp.SelectCommand.Parameters.AddWithValue("@TRANSFEE", "")
            adp.SelectCommand.Parameters.AddWithValue("@IATTACOMM", "")
            adp.SelectCommand.Parameters.AddWithValue("@COUNTER", 0)
            adp.SelectCommand.Parameters.AddWithValue("@CMD_TYPE", "SEARCH")
            adp.SelectCommand.Parameters.AddWithValue("@USERID", Session("UID").ToString)
            adp.SelectCommand.Parameters.AddWithValue("@IPADDRESS", "")
            Dim dt1 As New DataTable()
            con1.Open()
            adp.Fill(dt1)
            con1.Close()
            If (dt1.Rows.Count > 0) Then
                'lbl.Text = "ServiceTax & Transactin Fee For This AirLine And Agent Is Already Exist."
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('ServiceTax & Transactin Fee For This AirLine And Agent Is Already Exist.');", True)
            Else
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                Dim adap As New SqlDataAdapter("SP_InsertServiceTaxIntl", con)
                adap.SelectCommand.CommandType = CommandType.StoredProcedure
                adap.SelectCommand.Parameters.AddWithValue("@AL_Name", airline_code.SelectedItem.Text.Trim)
                adap.SelectCommand.Parameters.AddWithValue("@AL_Code", airline_code.SelectedValue)
                adap.SelectCommand.Parameters.AddWithValue("@STax", txt_SeviceTax.Text)
                adap.SelectCommand.Parameters.AddWithValue("@TFee", txt_TrasFee.Text)
                adap.SelectCommand.Parameters.AddWithValue("@IATAComm", Convert.ToInt16(txtIataComm.Text))
                adap.SelectCommand.Parameters.AddWithValue("@Trip", RadioButtonList1.SelectedValue)
                Dim dt2 As New DataTable()
                adap.Fill(dt2)
                Bind_Data()
                ' txt_SeviceTax.Text = ""
                'txt_TrasFee.Text = ""
                'lbl.Text = ""
                'uid.SelectedIndex = 0
                'mk.Text = 0
                'lbl.Text = "Submitted Sucessfully"
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('data Submitted successfully');", True)


            End If

        Catch ex As Exception

            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
        Try
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            ' Dim da1 As New SqlDataAdapter("Select * from ServiceTaxInt where  (AirlineCode='" & airline_code.SelectedValue & "') and Trip='" & RadioButtonList1.SelectedValue & "'  ", con1)
            ' Dim dt1 As New DataTable()
            Dim adp As New SqlDataAdapter("USP_AIRLINEFEE", con1)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@DOMINT", RadioButtonList1.SelectedValue.Trim)
            adp.SelectCommand.Parameters.AddWithValue("@AIRLINE_CODE", "")
            adp.SelectCommand.Parameters.AddWithValue("SERVICETAX", "")
            adp.SelectCommand.Parameters.AddWithValue("@TRANSFEE", "")
            adp.SelectCommand.Parameters.AddWithValue("@IATTACOMM", "")
            adp.SelectCommand.Parameters.AddWithValue("@COUNTER", 0)
            adp.SelectCommand.Parameters.AddWithValue("@CMD_TYPE", "SEARCH")
            adp.SelectCommand.Parameters.AddWithValue("@USERID", Session("UID").ToString)
            adp.SelectCommand.Parameters.AddWithValue("@IPADDRESS", "")
            Dim dt2 As New DataTable()
            con1.Open()
            adp.Fill(dt2)
            GridView1.DataSource = dt2
            GridView1.DataBind()
            con1.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Bind_Data()
    End Sub
End Class

