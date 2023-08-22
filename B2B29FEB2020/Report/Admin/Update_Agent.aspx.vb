Imports System.Data
Imports System.Data.SqlClient
Partial Class Update_Agent
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Public Property AgentDt() As DataTable
        Get
            Return DirectCast(Session("AgentDt"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("AgentDt") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type").ToString().ToUpper() <> "ADMIN" And Session("User_Type").ToString().ToUpper() <> "ACC" And Session("User_Type").ToString().ToUpper() <> "DI" Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("TypeID").ToString() = "AD2" Then
                'btn_update.Style(HtmlTextWriterStyle.Display) = "none"
                txt_saleref.Enabled = True
                ddl_type.Enabled = True
                ddl_activation.Enabled = True
                ddl_TicketingActiv.Enabled = True
                txt_pwd.Enabled = True
                txt_AgencyName.Enabled = True
                btn_update.Visible = True
            ElseIf Session("TypeID").ToString() = "AD1" Then
                txt_saleref.Enabled = False
                ddl_type.Enabled = True
                ddl_activation.Enabled = True
                ddl_TicketingActiv.Enabled = True
                btn_update.Visible = True
            ElseIf Session("TypeID").ToString() = "AC1" Then
                txt_saleref.Enabled = False
                ddl_type.Enabled = False
                ddl_activation.Enabled = True
                ddl_TicketingActiv.Enabled = True
                btn_update.Visible = True
            ElseIf Session("TypeID").ToString() <> "AD1" OrElse Session("TypeID").ToString() = "AD2" OrElse Session("TypeID").ToString() = "AC1" Then
                txt_saleref.Enabled = False
                ddl_type.Enabled = False
                ddl_activation.Enabled = False
                btn_update.Visible = False
                ddl_TicketingActiv.Enabled = False
            End If
            If Session("TypeID").ToString() = "AD1" OrElse Session("TypeID").ToString() = "AD2" Then
                txt_Address.Enabled = True
                txt_City.Enabled = True
                txt_Country.Enabled = True
                txt_State.Enabled = True
                txt_title.Enabled = True
                txt_Fname.Enabled = True
                txt_Lname.Enabled = True
                txt_Fax.Enabled = True
                txt_zip.Enabled = True
                txt_Mobile.Enabled = True
                txt_Email.Enabled = True
                txt_Pan.Enabled = True

            End If

            If Not IsPostBack Then
                Dim dttype As New DataTable

                dttype = GetAllGroupType().Tables(0)
                ''dttype = STDom.GetAgentType().Tables(0)
                If dttype.Rows.Count > 0 Then
                    ddl_type.AppendDataBoundItems = True
                    ddl_type.Items.Clear()
                    ddl_type.Items.Insert(0, "--Select Type--")
                    ddl_type.DataSource = dttype
                    ddl_type.DataTextField = "GroupType"
                    ddl_type.DataValueField = "GroupType"
                    ddl_type.DataBind()
                End If
                Dim dt As New DataTable
                dt = ST.GetAgencyDetails(Request("AgentID")).Tables(0)
                AgentDt = dt
                txt_AgencyName.Text = dt.Rows(0)("Agency_name").ToString
                td_AgentID.InnerText = dt.Rows(0)("User_Id").ToString
                td_AgencyID.InnerText = dt.Rows(0)("AgencyId").ToString
                txt_Address.Text = dt.Rows(0)("Address").ToString
                txt_City.Text = dt.Rows(0)("city").ToString
                txt_Country.Text = dt.Rows(0)("country").ToString
                txt_State.Text = dt.Rows(0)("State").ToString
                td_CrLimit.InnerText = dt.Rows(0)("Crd_Limit").ToString
                td_LTDate.InnerText = dt.Rows(0)("Crd_Trns_Date").ToString

                txt_pwd.Text = dt.Rows(0)("pwd").ToString
                txt_title.Text = dt.Rows(0)("Title").ToString

                txt_Fname.Text = dt.Rows(0)("FName").ToString
                txt_Lname.Text = dt.Rows(0)("LName").ToString
                txt_Fax.Text = dt.Rows(0)("Fax_no").ToString
                txt_zip.Text = dt.Rows(0)("zipcode").ToString
                txt_Mobile.Text = dt.Rows(0)("mobile").ToString
                txt_Email.Text = dt.Rows(0)("email").ToString
                txt_Pan.Text = dt.Rows(0)("PanNo").ToString

                td_tds.InnerText = dt.Rows(0)("TDS").ToString
                txt_saleref.Text = dt.Rows(0)("SalesExecID").ToString
                ddl_type.SelectedValue = dt.Rows(0)("Agent_Type").ToString
                ddl_activation.SelectedValue = dt.Rows(0)("Agent_Status").ToString
                ddl_TicketingActiv.SelectedValue = dt.Rows(0)("Online_Tkt").ToString

            End If
            If Session("User_Type").ToString() = "DI" Then
                ddl_activation.Enabled = True
                ddl_TicketingActiv.Enabled = True
                btn_update.Visible = True
                td_pwd.Visible = False
                txt_pwd.Visible = False
                ddl_type.Enabled = True

            End If

            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.AGENCYDETAILS.ToString()).Tables(0)
            If (dtmodule.Rows.Count > 0) Then
                For Each dr As DataRow In dtmodule.Rows
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.UPDATE.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        btn_update.Visible = False
                        'End If
                    ElseIf (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.PASSWORD.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        td_pwd.Visible = False
                        txt_pwd.Visible = False
                    ElseIf (dr("MODULETYPE").ToString().ToUpper() = "TYPE" AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        ddl_type.Enabled = False
                    ElseIf (dr("MODULETYPE").ToString().ToUpper() = "SALESREF" AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        txt_saleref.Enabled = False
                    ElseIf (dr("MODULETYPE").ToString().ToUpper() = "ACTIVATION" AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        ddl_activation.Enabled = False
                    ElseIf (dr("MODULETYPE").ToString().ToUpper() = "TKTACTIVATION" AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        ddl_TicketingActiv.Enabled = False
                    End If
                Next

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            STDom.UpdateAgentTypeSalesRef(Request("AgentID"), txt_saleref.Text.Trim, ddl_type.SelectedValue, ddl_activation.SelectedValue, ddl_TicketingActiv.SelectedValue, txt_Address.Text.Trim, txt_City.Text.Trim, txt_State.Text.Trim, txt_Country.Text.Trim, txt_zip.Text.Trim, txt_Fname.Text.Trim, txt_Lname.Text.Trim, txt_Mobile.Text.Trim, txt_Email.Text.Trim, txt_Fax.Text.Trim, txt_Pan.Text.Trim, txt_title.Text.Trim, txt_AgencyName.Text.Trim, txt_pwd.Text.Trim)
            Dim upXml As String = UpdationXml()
            If upXml <> "<Changes></Changes>" Then
                InsertAgencyDetailsUpdationLog(Request("AgentID"), Session("UID"), Request.ServerVariables("REMOTE_ADDR"), upXml)

            End If

            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Updated Sucessfully');", True)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function UpdationXml() As String

        Dim Changesxml As String = "<Changes>"


        If txt_AgencyName.Text <> AgentDt.Rows(0)("Agency_name").ToString Then
            Changesxml = Changesxml & "<Agency_name><New>" & txt_AgencyName.Text & "</New><Old>" & AgentDt.Rows(0)("Agency_name").ToString & "</Old></Agency_name>"

        End If




        If td_AgentID.InnerText <> AgentDt.Rows(0)("User_Id").ToString Then
            Changesxml = Changesxml & "<User_Id><New>" & td_AgentID.InnerText & "</New><Old>" & AgentDt.Rows(0)("User_Id").ToString & "</Old></User_Id>"

        End If


        If txt_Address.Text <> AgentDt.Rows(0)("Address").ToString Then
            Changesxml = Changesxml & "<Address><New>" & txt_Address.Text & "</New><Old>" & AgentDt.Rows(0)("Address").ToString & "</Old></Address>"

        End If

        If txt_City.Text <> AgentDt.Rows(0)("city").ToString Then
            Changesxml = Changesxml & "<City><New>" & txt_City.Text & "</New><Old>" & AgentDt.Rows(0)("city").ToString & "</Old></City>"

        End If

        If txt_Country.Text <> AgentDt.Rows(0)("country").ToString Then
            Changesxml = Changesxml & "<Country><New>" & txt_Country.Text & "</New><Old>" & AgentDt.Rows(0)("country").ToString & "</Old></Country>"

        End If

        If txt_State.Text <> AgentDt.Rows(0)("State").ToString Then
            Changesxml = Changesxml & "<State><New>" & txt_State.Text & "</New><Old>" & AgentDt.Rows(0)("State").ToString & "</Old></State>"

        End If

        If td_CrLimit.InnerText <> AgentDt.Rows(0)("Crd_Limit").ToString Then
            Changesxml = Changesxml & "<Crd_Limit><New>" & td_CrLimit.InnerText & "</New><Old>" & AgentDt.Rows(0)("Crd_Limit").ToString & "</Old></Crd_Limit>"

        End If

        If td_LTDate.InnerText <> AgentDt.Rows(0)("Crd_Trns_Date").ToString Then
            Changesxml = Changesxml & "<Crd_Trns_Date><New>" & td_LTDate.InnerText & "</New><Old>" & AgentDt.Rows(0)("Crd_Trns_Date").ToStringThen & "</Old></Crd_Trns_Date>"

        End If

        If txt_pwd.Text <> AgentDt.Rows(0)("pwd").ToString Then
            Changesxml = Changesxml & "<Pwd><New>" & txt_pwd.Text & "</New><Old>" & AgentDt.Rows(0)("pwd").ToString & "</Old></Pwd>"

        End If

        If txt_title.Text <> AgentDt.Rows(0)("Title").ToString Then
            Changesxml = Changesxml & "<Title><New>" & txt_title.Text & "</New><Old>" & AgentDt.Rows(0)("Title").ToString & "</Old></Title>"

        End If


        If txt_Fname.Text <> AgentDt.Rows(0)("FName").ToString Then
            Changesxml = Changesxml & "<FName><New>" & txt_Fname.Text & "</New><Old>" & AgentDt.Rows(0)("FName").ToString & "</Old></FName>"

        End If

        If txt_Lname.Text <> AgentDt.Rows(0)("LName").ToString Then
            Changesxml = Changesxml & "<LName><New>" & txt_Lname.Text & "</New><Old>" & AgentDt.Rows(0)("LName").ToString & "</Old></LName>"

        End If

        If txt_Fax.Text <> AgentDt.Rows(0)("Fax_no").ToString Then
            Changesxml = Changesxml & "<Fax_no><New>" & txt_Fax.Text & "</New><Old>" & AgentDt.Rows(0)("Fax_no").ToString & "</Old></Fax_no>"

        End If

        If txt_zip.Text <> AgentDt.Rows(0)("zipcode").ToString Then
            Changesxml = Changesxml & "<Zipcode><New>" & txt_zip.Text & "</New><Old>" & AgentDt.Rows(0)("zipcode").ToString & "</Old></Zipcode>"

        End If

        If txt_Mobile.Text <> AgentDt.Rows(0)("mobile").ToString Then
            Changesxml = Changesxml & "<Mobile><New>" & txt_Mobile.Text & "</New><Old>" & AgentDt.Rows(0)("mobile").ToString & "</Old></Mobile>"

        End If

        If txt_Email.Text <> AgentDt.Rows(0)("email").ToString Then
            Changesxml = Changesxml & "<Email><New>" & txt_Email.Text & "</New><Old>" & AgentDt.Rows(0)("email").ToString & "</Old></Email>"

        End If

        If txt_Pan.Text <> AgentDt.Rows(0)("PanNo").ToString Then
            Changesxml = Changesxml & "<PanNo><New>" & txt_Pan.Text & "</New><Old>" & AgentDt.Rows(0)("PanNo").ToString & "</Old></PanNo>"

        End If


        If td_tds.InnerText <> AgentDt.Rows(0)("TDS").ToString Then
            Changesxml = Changesxml & "<TDS><New>" & td_tds.InnerText & "</New><Old>" & AgentDt.Rows(0)("TDS").ToString & "</Old></TDS>"

        End If

        If txt_saleref.Text <> AgentDt.Rows(0)("SalesExecID").ToString Then
            Changesxml = Changesxml & "<SalesExecID><New>" & txt_saleref.Text & "</New><Old>" & AgentDt.Rows(0)("SalesExecID").ToString & "</Old></SalesExecID>"

        End If

        If ddl_type.SelectedValue <> AgentDt.Rows(0)("Agent_Type").ToString Then
            Changesxml = Changesxml & "<Agent_Type><New>" & ddl_type.SelectedValue & "</New><Old>" & AgentDt.Rows(0)("Agent_Type").ToString & "</Old></Agent_Type>"

        End If

        If ddl_activation.SelectedValue <> AgentDt.Rows(0)("Agent_Status").ToString Then
            Changesxml = Changesxml & "<Agent_Status><New>" & ddl_activation.SelectedValue & "</New><Old>" & AgentDt.Rows(0)("Agent_Status").ToString & "</Old></Agent_Status>"

        End If

        If ddl_TicketingActiv.SelectedValue <> AgentDt.Rows(0)("Online_Tkt").ToString Then
            Changesxml = Changesxml & "<Online_Tkt><New>" & ddl_TicketingActiv.SelectedValue & "</New><Old>" & AgentDt.Rows(0)("Online_Tkt").ToString & "</Old></Online_Tkt>"

        End If
        Changesxml = Changesxml & "</Changes>"
        UpdationXml = Changesxml

    End Function


    Public Function InsertAgencyDetailsUpdationLog(ByVal agencyId As String, ByVal UpdatedBy As String, ByVal ip As String, ByVal updatedFieldLog As String) As Integer

        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Try
            Dim cmd As New SqlCommand("Sp_Insert_Agency_Details_Updation_Log", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgencyID", agencyId)
            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy)
            cmd.Parameters.AddWithValue("@IP", ip)
            cmd.Parameters.AddWithValue("@UpdatedFieldLog", updatedFieldLog)
            con.Open()
            cmd.ExecuteNonQuery()
            i = 1
            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
        End Try
        Return i
    End Function

    Public Function GetAllGroupType() As DataSet
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@distr", Session("UID").ToString().Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Sp_GetAllGroupType_stockist_updateType", 3)
    End Function
End Class
