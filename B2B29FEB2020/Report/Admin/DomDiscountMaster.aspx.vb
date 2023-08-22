Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_DomDiscountMaster
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As SqlCommand
    Private STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Session("User_Type").ToString().ToUpper() <> "ADMIN" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Session("TypeID").ToString() = "AD1" OrElse Session("TypeID").ToString() = "AD2" Then
            tr_Dis.Visible = True
        Else
            tr_Dis.Visible = False
        End If
        If Not Page.IsPostBack Then
            Try

                grade.AppendDataBoundItems = True
                grade.Items.Clear()

                'Dim item As New ListItem("All Type", "0")
                'ddl_ptype.Items.Insert(0, item)
                grade.Items.Insert(0, "--Select--")
                grade.DataSource = STDom.GetAllGroupType().Tables(0)
                grade.DataTextField = "GroupType"
                grade.DataValueField = "GroupType"
                grade.DataBind()


                If con.State = ConnectionState.Open Then con.Close()
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                'adp = New SqlDataAdapter("SELECT user_id FROM New_Regs where Agent_Type='DA' ", con)
                'Dim cmd As New SqlCommand()
                'cmd.CommandText = "USP_DomDiscountMaster"
                'cmd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@DIS", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@CB", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@RBD", 'SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SELECT"
                'cmd.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = ""
                'cmd.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = ""
                'cmd.Connection = con
                'Dim da1 As New SqlDataAdapter(cmd)
                'Dim dt1 As New DataTable()
                'con.Open()
                'adp.Fill(ds)
                'dist.Items.Clear()
                ' If ds.Tables(0).Rows.Count > 0 Then
                'dist.DataSource = ds
                'dist.DataTextField = "user_id"
                'dist.DataValueField = "user_id"
                'dist.DataBind()
                'Else
                'End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
        Try
            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.DISCOUNT_D.ToString()).Tables(0)
            If (dtmodule.Rows.Count > 0) Then
                For Each dr As DataRow In dtmodule.Rows
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.UPDATE.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        UpdateAg.Visible = False
                        tr_Dis.Visible = False
                    End If

                Next
            End If
        Catch ex As Exception
        End Try

        txtPlbBasic.Attributes.Add("onkeypress", "return vali_number();")
        txtPlbYQ.Attributes.Add("onkeypress", "return vali_number();")
        txtPlbBasicYQ.Attributes.Add("onkeypress", "return vali_number();")
        'txtPlbRbd.Attributes.Add("onkeypress", "return vali_number();")

        txtDis.Attributes.Add("onkeypress", "return vali_number();")
        txtDisYQ.Attributes.Add("onkeypress", "return vali_number();")
        txtDisBYQ.Attributes.Add("onkeypress", "return vali_number();")
        UpdateAg.Attributes.Add("OnClick", "return checkvld();")
    End Sub
    Protected Sub grade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grade.SelectedIndexChanged
        Dim Agent As String = ""
        Dim dt As New DataTable()
        Try
            Dim dsagent As New DataSet
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Agent = grade.SelectedItem.Text
            'Dim da As New SqlDataAdapter("SELECT * from Agent_CD where grade='" & Agent & "'", con)
            'Dim dt As New DataTable()
            'da.Fill(dt)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "USP_DomDiscountMaster"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = Agent
            cmd.Parameters.Add("@DIS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@CB", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@RBD", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SelectedIndexChanged"
            cmd.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = ""
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)

            con.Open()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.Airlag.SelectedValue = UCase((dt.Rows(0).Item("Airline")).ToString())
                Me.txtDis.Text = dt.Rows(0).Item("dis")
                Me.txtDisYQ.Text = dt.Rows(0).Item("dis_YQ")
                Me.txtDisBYQ.Text = dt.Rows(0).Item("dis_B_YQ")
                Me.txtcb.Text = dt.Rows(0).Item("cb")


                'Me.txtPlbBasic.Text = dt.Rows(0).Item("PlbBasic")
                'Me.txtPlbYQ.Text = dt.Rows(0).Item("PlbYQ")
                'Me.txtPlbBasicYQ.Text = dt.Rows(0).Item("PlbBYQ")
                'Me.txtPlbRbd.Text = dt.Rows(0).Item("RBD")


                If (IsDBNull(dt.Rows(0).Item("PlbBasic")) = True) OrElse (dt.Rows(0).Item("PlbBasic") = "") Then
                    Me.txtPlbBasic.Text = 0
                Else
                    Me.txtPlbBasic.Text = dt.Rows(0).Item("PlbBasic")
                End If


                If (IsDBNull(dt.Rows(0).Item("PlbYQ")) = True) OrElse (dt.Rows(0).Item("PlbYQ") = "") Then
                    Me.txtPlbYQ.Text = 0
                Else
                    Me.txtPlbYQ.Text = dt.Rows(0).Item("PlbYQ")
                End If



                If (IsDBNull(dt.Rows(0).Item("PlbBYQ")) = True) OrElse (dt.Rows(0).Item("PlbBYQ") = "") Then
                    Me.txtPlbBasicYQ.Text = 0
                Else
                    Me.txtPlbBasicYQ.Text = dt.Rows(0).Item("PlbBYQ")
                End If


                If (IsDBNull(dt.Rows(0).Item("RBD")) = True) OrElse (dt.Rows(0).Item("RBD") = "") Then
                    Me.txtPlbRbd.Text = ""
                Else
                    Me.txtPlbRbd.Text = dt.Rows(0).Item("RBD")
                End If



            Else
                Me.Airlag.SelectedIndex = 0
                Me.txtDis.Text = 0
                Me.txtDisYQ.Text = 0
                Me.txtDisBYQ.Text = 0
                Me.txtcb.Text = 0

                Me.txtPlbBasic.Text = 0
                Me.txtPlbYQ.Text = 0
                Me.txtPlbBasicYQ.Text = 0
                Me.txtPlbRbd.Text = ""

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        Try
            'ds.Clear()
            ' Dim str As String = ""
            'str = "select Grade, Dis, Airline, CB, Dis_YQ, Dis_B_YQ,UpdatedDate,UpdatedBy from Agent_CD where grade='" & Agent & "'"

            'str = "select * from Agent_CD where grade='" & Agent & "'"
            'adp = New SqlDataAdapter(str, con)
            'adp.Fill(ds)
            ' If ds.Tables(0).Rows.Count > 0 Then
            GridView1.DataSource = dt
            GridView1.DataBind()
            ' Else
            'End If
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub dist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dist.SelectedIndexChanged
        Try
            Dim dsagent As New DataSet
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim dis = dist.SelectedItem.Text
            Dim da As New SqlDataAdapter("SELECT * from dist_CD where Distri='" & dis & "'", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.txtDdis.Text = dt.Rows(0).Item("discount")
                Me.Airlinedi.SelectedValue = UCase((dt.Rows(0).Item("Airline")).ToString())
                If dt.Rows(0).Item("TF") = "Y" Then
                    yes.Checked = True
                Else
                    No.Checked = True
                End If
            Else
                Me.txtDdis.Text = 0
            End If
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub UpdateAg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateAg.Click
        Try
            ' Dim str As String
            Dim agent = grade.SelectedItem.Text
            Dim airline = UCase(Me.Airlag.SelectedValue)
            Dim dis = Me.txtDis.Text
            Dim disyq = Me.txtDisYQ.Text
            Dim disbyq = Me.txtDisBYQ.Text
            Dim cashback = Me.txtcb.Text
            Dim PlbBasic = Convert.ToString(Me.txtPlbBasic.Text)
            Dim PlbYQ = Convert.ToString(Me.txtPlbYQ.Text)
            Dim PlbBYQ = Convert.ToString(txtPlbBasicYQ.Text)
            Dim RBD = Convert.ToString(Me.txtPlbRbd.Text).ToUpper()
            Dim cmd1 As New SqlCommand()
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")

            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            'Dim adp As New SqlDataAdapter("SELECT * from Agent_CD where grade='" & agent & "' and Airline='" & airline & "'", con)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "USP_DomDiscountMaster"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = agent
            cmd.Parameters.Add("@DIS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = airline
            cmd.Parameters.Add("@CB", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@RBD", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATESELECT"
            cmd.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = ""
            cmd.Connection = con
            Dim adp As New SqlDataAdapter(cmd)
            con.Open()
            Dim dt As New DataTable
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then


                cmd1.CommandText = "USP_DomDiscountMaster"
                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = agent
                cmd1.Parameters.Add("@DIS", SqlDbType.VarChar).Value = dis
                cmd1.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = airline
                cmd1.Parameters.Add("@CB", SqlDbType.VarChar).Value = cashback
                cmd1.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = disyq
                cmd1.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = disbyq
                cmd1.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = PlbBasic
                cmd1.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = PlbYQ
                cmd1.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = PlbBYQ
                cmd1.Parameters.Add("@RBD", SqlDbType.VarChar).Value = RBD
                cmd1.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress
                cmd1.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATED"
                cmd1.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd1.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = DateAndTime.Now.ToString()
                cmd1.Connection = con
                'str = "update Agent_CD set dis='" & dis & "',cb= '" & cashback & "',dis_YQ='" & disyq & "',dis_B_YQ='" & disbyq & "', PlbBasic='" & PlbBasic & "',PlbYQ= '" & PlbYQ & "',PlbBYQ='" & PlbBYQ & "',RBD='" & RBD & "',  UpdatedDate='" & DateTime.Now.ToString() & "',UpdatedBy='" & Session("UID").ToString() & "' where grade='" & agent & "' and Airline='" & airline & "' "
            Else
                cmd1.CommandText = "USP_DomDiscountMaster"
                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = agent
                cmd1.Parameters.Add("@DIS", SqlDbType.VarChar).Value = dis
                cmd1.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = airline
                cmd1.Parameters.Add("@CB", SqlDbType.VarChar).Value = cashback
                cmd1.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = disyq
                cmd1.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = disbyq
                cmd1.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = PlbBasic
                cmd1.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = PlbYQ
                cmd1.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = PlbBYQ
                cmd1.Parameters.Add("@RBD", SqlDbType.VarChar).Value = RBD
                cmd1.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress
                cmd1.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "INSERTED"
                cmd1.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = Session("UID").ToString
                cmd1.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = DateAndTime.Now.ToString()
                cmd1.Connection = con
                'str = "Insert into Agent_CD(Grade,Dis,Airline,CB,Dis_YQ,Dis_B_YQ,UpdatedDate,UpdatedBy,PlbBasic,PlbYQ,PlbBYQ,RBD)values('" & agent & "','" & dis & "','" & airline & "','" & cashback & "','" & disyq & "','" & disbyq & "','" & DateAndTime.Now.ToString() & "','" & Session("UID").ToString() & "','" & PlbBasic & "','" & PlbYQ & "','" & PlbBYQ & "','" & RBD & "')"
            End If
            'cmd1 = New SqlCommand(str, con)
            'con.Open()
            cmd1.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data has been updated successfully!!');", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "Alert", "alert('Data has been inserted successfully!!');", True)
            End If
            ds.Clear()
            'Dim str1 As String = ""
            ' str1 = "select Grade, Dis, Airline, CB, Dis_YQ, Dis_B_YQ ,UpdatedDate,UpdatedBy from Agent_CD where grade='" & agent & "'"

            'str1 = "select Grade, Dis, Airline, CB, Dis_YQ, Dis_B_YQ ,PlbBasic,PlbYQ,PlbBYQ,RBD ,UpdatedDate,UpdatedBy from Agent_CD where grade='" & agent & "'"
            ' adp = New SqlDataAdapter(str1, con)
            Dim cmd2 As New SqlCommand()
            cmd2.CommandText = "USP_DomDiscountMaster"
            cmd2.CommandType = CommandType.StoredProcedure
            cmd2.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = agent
            cmd2.Parameters.Add("@DIS", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@CB", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@RBD", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "RTVAGENT"
            cmd2.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = ""
            cmd2.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = ""
            cmd2.Connection = con
            Dim adp2 As New SqlDataAdapter(cmd)
            Dim dt2 As New DataTable
            adp2.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                GridView1.DataSource = dt2
                GridView1.DataBind()
            Else
            End If
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub Airlag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Airlag.SelectedIndexChanged
        Try
            'Dim str As String
            Dim agent = grade.SelectedItem.Text
            Dim airline = UCase(Me.Airlag.SelectedValue)
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim cmd As New SqlCommand()
            cmd.CommandText = "USP_DomDiscountMaster"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = agent
            cmd.Parameters.Add("@DIS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = airline
            cmd.Parameters.Add("@CB", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@DIS_B_YQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBASIC", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@PLBBYQ", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@RBD", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATESELECT"
            cmd.Parameters.Add("@UPDATEDBY", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).Value = ""
            cmd.Connection = con
            Dim adp As New SqlDataAdapter(cmd)
            con.Open()
            Dim dt As New DataTable

            ' Dim adp As New SqlDataAdapter("SELECT * from Agent_CD where grade='" & agent & "' and Airline='" & airline & "'", con)
            'Dim dt As New DataTable
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.txtDis.Text = dt.Rows(0).Item("Dis")
                Me.txtDisYQ.Text = dt.Rows(0).Item("Dis_YQ")
                Me.txtDisBYQ.Text = dt.Rows(0).Item("Dis_B_YQ")
                Me.txtcb.Text = dt.Rows(0).Item("CB")

                ' change rakesh 02/05/16
                If (IsDBNull(dt.Rows(0).Item("PlbBasic")) = True) OrElse (dt.Rows(0).Item("PlbBasic") = "") Then
                    Me.txtPlbBasic.Text = 0
                Else
                    Me.txtPlbBasic.Text = dt.Rows(0).Item("PlbBasic")
                End If


                If (IsDBNull(dt.Rows(0).Item("PlbYQ")) = True) OrElse (dt.Rows(0).Item("PlbYQ") = "") Then
                    Me.txtPlbYQ.Text = 0
                Else
                    Me.txtPlbYQ.Text = dt.Rows(0).Item("PlbYQ")
                End If



                If (IsDBNull(dt.Rows(0).Item("PlbBYQ")) = True) OrElse (dt.Rows(0).Item("PlbBYQ") = "") Then
                    Me.txtPlbBasicYQ.Text = 0
                Else
                    Me.txtPlbBasicYQ.Text = dt.Rows(0).Item("PlbBYQ")
                End If


                If (IsDBNull(dt.Rows(0).Item("RBD")) = True) OrElse (dt.Rows(0).Item("RBD") = "") Then
                    Me.txtPlbRbd.Text = ""
                Else
                    Me.txtPlbRbd.Text = dt.Rows(0).Item("RBD")
                End If
                con.Close()
            Else
                Me.txtDis.Text = 0
                Me.txtDisYQ.Text = 0
                Me.txtDisBYQ.Text = 0
                Me.txtcb.Text = 0

                Me.txtPlbBasic.Text = 0
                Me.txtPlbYQ.Text = 0
                Me.txtPlbBasicYQ.Text = 0
                Me.txtPlbRbd.Text = ""
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub UpdateDi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateDi.Click
        Try
            Dim str As String
            Dim TRF
            Dim agent = dist.SelectedItem.Text
            Dim airline = UCase(Me.Airlinedi.SelectedValue)
            Dim dis = Me.txtDdis.Text
            If yes.Checked Then
                TRF = "Y"
            ElseIf No.Checked Then
                TRF = "N"
            End If

            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim adp As New SqlDataAdapter("SELECT * from Dist_CD where distri='" & agent & "' and Airline='" & airline & "'", con)
            Dim dt As New DataTable
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                str = "update Dist_CD set Discount='" & dis & "',TF='" & TRF & "' where distri='" & agent & "' and Airline='" & airline & "' "
            Else
                str = "Insert into Dist_CD(Distri,Airline,Discount,TF)values('" & agent & "','" & airline & "','" & dis & "','" & TRF & "')"
            End If
            cmd = New SqlCommand(str, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub Airlinedi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Airlinedi.SelectedIndexChanged

        Dim str As String
        Dim agent = dist.SelectedItem.Text
        Dim airline = UCase(Me.Airlinedi.SelectedValue)
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim adp As New SqlDataAdapter("SELECT * from Dist_CD where distri='" & agent & "' and Airline='" & airline & "'", con)
            Dim dt As New DataTable
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.Airlinedi.SelectedValue = UCase((dt.Rows(0).Item("Airline")).ToString())
                Me.txtDdis.Text = dt.Rows(0).Item("Discount")
                If dt.Rows(0).Item("TF") = "Y" Then
                    yes.Checked = True
                Else
                    No.Checked = True
                End If
            Else
                Me.txtDdis.Text = 0
            End If
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class

