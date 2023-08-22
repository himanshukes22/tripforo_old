Imports System.Data
Imports System.Data.SqlClient
Imports HotelShared
Imports System.Configuration.ConfigurationManager
Imports ITZLib

Partial Class Search
    Inherits System.Web.UI.Page
    Dim Email As String
    Dim Password As String
    Dim FirstName As String
    Dim LastName As String
    Dim AgencyName As String
    Dim Address As String
    Dim City As String
    Dim State As String
    Dim Country As String
    Dim agnttype As String
    Dim Zip As String
    Dim Phone As String
    Public Distr As String
    Public User_Type As String
    Dim ag_type As String
    Public dis_str
    Public Dis_Air
    Dim conn As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    Dim Dist As String
    Dim credit_limit
    Dim Ex_Id As String
    Dim exe_dept As String
    Dim agent_type As String
    Dim agent_status As String
    Private STDom As New SqlTransactionDom()
    Private ST As New SqlTransaction()
    Private sttusobj As New Status()
    Dim clsCorp As New ClsCorporate()
    Private objHTLDAL As New HotelDAL.HotelDA()
    Public NotificationContent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        Try

            If Session("User_Type") = "DI" Then
                Response.Redirect("SprReports/Accounts/Ledger.aspx")
            End If

            If Not IsPostBack Then
                GetDomCommission()
                BindNotifications()
                If Session("User_Type") = "AGENT" Or Session("User_Type") = "DIST" Then
                    Dim dtAg As New DataTable
                    Dim ST As New SqlTransaction
                    dtAg = ST.GetAgencyDetails(Session("UID")).Tables(0)
                    'If dtAg.Rows(0) Then
                    'userid.InnerText = Session("UID")
                    Dim IsPWD As Boolean = If(String.IsNullOrEmpty(dtAg.Rows(0)("IsPWD").ToString()), False, Convert.ToBoolean(dtAg.Rows(0)("IsPWD")))
                    If IsPWD = False Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tmp", "<script type='text/javascript'>passchange();</script>", False)
                    End If

                End If



            End If
        Catch ex As Exception

        End Try


        Session("PNR") = "False"
        Session("Flag") = "0"
        Try
            User_Type = Session("User_Type").ToString.ToUpper
            If User_Type = "AGENT" Then
                AuthenticationMethod(Session("UID"), Session("User_Type"))
            ElseIf User_Type = "ADMIN" Then

            ElseIf User_Type = "DIST" Then
                AuthenticationMethod(Session("UID"), Session("User_Type"))
            ElseIf User_Type = "EXEC" Or User_Type = "ACC" Then

            End If

            If Session("UID") = "" Then
                Response.Redirect("Login.aspx")
            End If
        Catch ex As Exception

        End Try
        If (Request("Htl") <> "") Then

            'If Request("Htl") = "H" Then
            '    IBE_CP.Visible = False
            '    HotelSearch.Visible = True
            'Else
            '    HotelSearch.Visible = False
            '    IBE_CP.Visible = True
            'End If
        Else
            ''HotelSearch.Visible = False
            ''IBE_CP.Visible = True
        End If


        Try
            Distr = Dist
            If Session("User_Type") = "DIST" Then
                di_dis()
            ElseIf Session("User_Type") = "AGENT" And Distr = UCase("SPRING") Then
                ag_dis(Distr)
            ElseIf Session("User_Type") = "AGENT" And Distr <> "SPRING" Then
                ag_dis(Distr)
            End If
        Catch ex As Exception

        End Try

        Session("SearchCriteriaUser") = Request.Url

        ' ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "<script type='text/javascript'>openDialog();</script>", False)
    End Sub
    'Protected Sub btn_resetpwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_resetpwd.Click
    '    Try


    '        Dim dtAg As New DataTable
    '        Dim ST As New SqlTransaction
    '        dtAg = ST.GetAgencyDetails(Session("UID")).Tables(0)
    '        If (dtAg.Rows(0)("PWD").ToString().ToUpper() = oldpwd.Text.Trim().ToUpper()) Then
    '            If dtAg.Rows(0)("PWD").ToString().ToUpper() = newpwd.Text.Trim().ToUpper() Then
    '                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Old and new password are same. Please try another password.');", True)
    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tmp", "<script type='text/javascript'>passchange();</script>", False)
    '            Else
    '                Dim STDom As New SqlTransactionDom()
    '                STDom.UpdateAgentProfile("PasswordChange", dtAg.Rows(0)("User_Id").ToString(), newpwd.Text.Trim(), "", "", "", "", "", "", "")
    '                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Your password has been changed sucessfully.');", True)
    '            End If

    '            'Try
    '            '    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Your password has been changed sucessfully.');", True)
    '            '    'FormsAuthentication.SignOut()
    '            '    'Session.Abandon()
    '            '    'Response.Redirect("~/Login.aspx")

    '            'Catch ex As Exception

    '            'End Try
    '            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tmp", "<script type='text/javascript'>passchangeclose();</script>", False)
    '        Else
    '            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Old password is incorrect');", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tmp", "<script type='text/javascript'>passchange();</script>", False)
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Public Sub GetDomCommission()
        Dim dt As New DataTable
        Dim Connection As New SqlConnection(conn)
        Try
            Dim str
            ' Dim Connection As New SqlConnection(conn)
            Connection.Open()
            str = "select * from CommissionNew where  TripType='D' and GroupType='" & Session("agent_type") & "'"
            Dim adp As SqlDataAdapter
            adp = New SqlDataAdapter(str, Connection)
            adp.Fill(dt)
            Session("CommissionNew") = dt
            'Catch ex As Exception
            '    Session("CommissionNew") = dt
            'End Try
        Catch ex As Exception
            Session("CommissionNew") = dt

        Finally
            Connection.Close()
        End Try

    End Sub

    Public Sub ag_dis(ByVal Distr)
        Dim i As Integer
        'Dim dis_str As String

        Dim str
        dis_str = Nothing
        Dim Connection As New SqlConnection(conn)
        Connection.Open()
        If Session("User_Type") = "AGENT" And Distr <> UCase("SPRING") Then
            str = "select * from DAgent_CD where grade='" & Session("AGTY") & "' and distr='" & Replace(Distr, ",", "") & "'"
        Else
            str = "select * from Agent_CD where grade='" & Session("AGTY") & "'"
        End If

        Dim adp As SqlDataAdapter
        adp = New SqlDataAdapter(str, Connection)
        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                dis_str = dis_str + CStr(dt.Rows(i)(2)) & "-" & CStr(dt.Rows(i)(1)) & "-" & CStr(dt.Rows(i)(3)) & "-" & CStr(dt.Rows(i)(4)) & "-" & CStr(dt.Rows(i)(5)) & "AD-"
            Next
            Dis_Air = Split(dis_str, "AD-")
            Session("Discount") = Dis_Air

        End If
    End Sub
    Public Sub di_dis()
        Dim i As Integer
        'Dim dis_str As String

        dis_str = Nothing
        Dim Connection As New SqlConnection(conn)
        Connection.Open()
        Dim str = "select * from Dist_CD where distri='" & Session("UID") & "'"
        Dim adp As SqlDataAdapter
        adp = New SqlDataAdapter(str, Connection)
        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                dis_str = dis_str + CStr(dt.Rows(i)(1)) & "-" & CStr(dt.Rows(i)(2)) & "-" & CStr(dt.Rows(i)(3)) & "AD-"
            Next
            Dis_Air = Split(dis_str, "AD-")
            Session("Discount") = Dis_Air

        End If
    End Sub
    Private Sub Authentication_exe(ByVal UserName As String)
        Try
            Dim boolReturnValue As Boolean = False
            Dim Connection As New SqlConnection(conn)
            Connection.Open()
            Dim strSQL As String = "SELECT * FROM Execu"
            UserName = UserName
            Password = Password
            Dim adp As SqlDataAdapter
            adp = New SqlDataAdapter(strSQL, Connection)
            Dim dt As New DataTable
            adp.Fill(dt)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) Then
                        Ex_Id = UCase((dt.Rows(i).Item("user_id")).ToString())
                        exe_dept = UCase((dt.Rows(i).Item("dept")).ToString())
                        boolReturnValue = True
                        Exit For
                    End If
                Next
            End If

        Catch
        End Try
    End Sub
    Private Sub AuthenticationMethod_admin(ByVal UserName As String)
        Try
            Dim boolReturnValue As Boolean = False
            Dim Connection As New SqlConnection(conn)
            Connection.Open()
            Dim strSQL As String = "SELECT * FROM ADMIN_b2b"
            UserName = UserName
            Password = Password
            Dim adp As SqlDataAdapter
            adp = New SqlDataAdapter(strSQL, Connection)
            Dim dt As New DataTable
            adp.Fill(dt)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If (LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString())) Then
                        Email = UCase((dt.Rows(i).Item("user_id")).ToString())
                        FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
                        LastName = UCase((dt.Rows(i).Item("lname")).ToString())
                        Address = UCase((dt.Rows(i).Item("address")).ToString())
                        City = UCase((dt.Rows(i).Item("city")).ToString())
                        Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
                        Country = UCase((dt.Rows(i).Item("country")).ToString())
                        Phone = UCase((dt.Rows(i).Item("Mobileno")).ToString())
                        'Agnttype = UCase((dt.Rows(i).Item("AgentType")).ToString())
                        'AgencyName = UCase((dt.Rows(i).Item("company")).ToString())
                        'Dist = UCase((dt.Rows(i).Item("Distributor")).ToString())
                        boolReturnValue = True
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub AuthenticationMethod(ByVal UserName As String, ByVal User_Type As String)
        Dim Connection As New SqlConnection(conn)
        Try
            Dim strSQL
            Dim uid = UserName
            Dim pwd = Password
            Dim boolReturnValue As Boolean = False
            'Dim Connection As New SqlConnection(conn)
            Connection.Open()
            If User_Type = "DIST" Then
                strSQL = "SELECT * FROM New_Regs where agent_Type='DA'"
            Else
                strSQL = "SELECT * FROM New_Regs where User_Id='" & UserName & "' and (agent_Type <> 'DA')"
            End If
            Dim adp As SqlDataAdapter
            adp = New SqlDataAdapter(strSQL, Connection)
            Dim dt As New DataTable
            adp.Fill(dt)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If LCase(UserName) = LCase((dt.Rows(i).Item("user_id")).ToString()) Then
                        Email = UCase((dt.Rows(i).Item("user_id")).ToString())
                        FirstName = UCase((dt.Rows(i).Item("Fname")).ToString())
                        LastName = UCase((dt.Rows(i).Item("lname")).ToString())
                        Address = UCase((dt.Rows(i).Item("address")).ToString())
                        City = UCase((dt.Rows(i).Item("city")).ToString())
                        Zip = UCase((dt.Rows(i).Item("zipcode")).ToString())
                        Country = UCase((dt.Rows(i).Item("country")).ToString())
                        Phone = UCase((dt.Rows(i).Item("Mobile")).ToString())
                        AgencyName = UCase((dt.Rows(i).Item("Agency_Name")).ToString())
                        Dist = UCase((dt.Rows(i).Item("Distr")).ToString())
                        credit_limit = UCase((dt.Rows(i).Item("Crd_Limit")).ToString())
                        agent_type = dt.Rows(i).Item("Agent_Type").ToString()
                        Session("agent_type") = agent_type
                        agent_status = dt.Rows(i).Item("Agent_Status").ToString()
                        Session("AgencyName") = AgencyName
                        Session("email_Agent") = Email
                        Session("Mobile") = Phone
                        Session("Address") = Address
                        Session("CL") = credit_limit
                        boolReturnValue = True
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            'clsErrorLog.LogInfo(ex)
            LogInfo(ex)
        Finally
            Connection.Close()
        End Try
    End Sub


    'Public Sub RemarkRefundReissue(ByVal PaxId As String, ByVal CommandName As String, ByVal PaxType As String)
    '    Dim ST As New SqlTransaction()
    '    Try
    '        Dim FirstStatus, SecondStatus As String
    '        Dim gridViewds As New DataSet()
    '        Dim Paxds As New DataSet()
    '        gridViewds = ST.GetTicketdIntl(Convert.ToInt32(PaxId), PaxType)
    '        '  Dim filterArray As Array = gridViewds.Tables(0).Select("PaxId ='" & PaxId & "'")
    '        If gridViewds.Tables(0).Rows.Count > 0 Then
    '            FirstStatus = CheckTktNo(Convert.ToInt32(PaxId), gridViewds.Tables(0).Rows(0)("Orderid").ToString(), gridViewds.Tables(0).Rows(0)("GdsPnr").ToString())

    '            Dim fltds As New DataSet()
    '            fltds = ST.GetTicketdIntl(Convert.ToInt32(PaxId), PaxType)
    '            Dim newpaxid As String = PaxId.Trim()
    '            If fltds.Tables(0).Rows(0)("ResuID").ToString() <> "" AndAlso fltds.Tables(0).Rows(0)("ResuID").ToString() IsNot Nothing Then
    '                Paxds = OldPaxInfo(fltds.Tables(0).Rows(0)("ResuID").ToString(), fltds.Tables(0).Rows(0)("Title").ToString(), fltds.Tables(0).Rows(0)("FName").ToString(), fltds.Tables(0).Rows(0)("MName").ToString(), fltds.Tables(0).Rows(0)("LName").ToString(), fltds.Tables(0).Rows(0)("PaxType").ToString())
    '                newpaxid = Paxds.Tables(0).Rows(0)("PaxId").ToString()

    '            End If

    '            fltds = ST.GetTicketdIntl(newpaxid, PaxType)
    '            Dim fltTD As DataTable = fltds.Tables(0)

    '            SecondStatus = CheckTktNo(Convert.ToInt32(newpaxid.Trim()), fltTD.Rows(0).Item("OrderId"), fltTD.Rows(0).Item("PNR"))

    '            If SecondStatus = "0" Then
    '                showPoup(PaxId, CommandName)
    '            ElseIf SecondStatus = "Reissue request can not be accepted for past departure date." And CommandName = "Refund" Then
    '                showPoup(PaxId, CommandName)
    '            ElseIf (SecondStatus = "0" Or SecondStatus = "Reissue request can not be accepted for past departure date." Or SecondStatus = "Given ticket number is already ReIssued") Or FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And CommandName = "Refund" Then
    '                If FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And CommandName = "Refund" Then
    '                    Try
    '                        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
    '                        If page IsNot Nothing Then
    '                            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "HourDeparturePopup(" & PaxId & ", '" & CommandName & "', '" & gridViewds.Tables(0).Rows(0)("GdsPnr").ToString() & "', '" & gridViewds.Tables(0).Rows(0)("OrderId").ToString() & "');", True)
    '                        End If
    '                    Catch ex As Exception
    '                        clsErrorLog.LogInfo(ex)
    '                    End Try
    '                Else
    '                    showPoup(PaxId, CommandName)
    '                End If

    '            ElseIf FirstStatus = "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly." And CommandName = "Refund" Then
    '                Try
    '                    Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
    '                    If page IsNot Nothing Then
    '                        ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "HourDeparturePopup(" & PaxId & ", '" & CommandName & "', '" & gridViewds.Tables(0).Rows(0)("GdsPnr").ToString() & "', '" & gridViewds.Tables(0).Rows(0)("OrderId").ToString() & "');", True)
    '                    End If
    '                Catch ex As Exception
    '                    clsErrorLog.LogInfo(ex)
    '                End Try
    '            Else
    '                ShowAlertMessage(SecondStatus)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try

    'End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "jAlert('" & [error] & "', 'Alert');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    'Public Sub showPoup(ByVal PaxId As String, ByVal CommandName As String)
    '    Try
    '        Dim gridViewds As New DataSet()
    '        gridViewds = Session("Grdds")
    '        Dim filterArray As Array = gridViewds.Tables(0).Select("PaxId ='" & PaxId & "'")
    '        If filterArray.Length > 0 Then
    '            If CommandName = "Refund" Then
    '                CallScriptFunction_Refund("RefundPopUpLoad", PaxId, CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("OrderId").ToString())
    '            Else
    '                CallScriptFunction("popupLoad", PaxId, CommandName, filterArray(0)("GdsPnr").ToString(), filterArray(0)("TicketNumber").ToString(), filterArray(0)("FName").ToString() & " " & filterArray(0)("LName").ToString(), filterArray(0)("PaxType").ToString())
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)
    '    End Try
    'End Sub

    Public Shared Sub CallScriptFunction(ByVal funName As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String, ByVal Parameter5 As String, ByVal Parameter6 As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", funName & "('" & Parameter1 & "', '" & Parameter2 & "', '" & Parameter3 & "', '" & Parameter4 & "', '" & Parameter5 & "', '" & Parameter6 & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Shared Sub CallScriptFunction_Refund(ByVal funName As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", funName & "('" & Parameter1 & "', '" & Parameter2 & "', '" & Parameter3 & "', '" & Parameter4 & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function CheckTktNo(ByVal PaxId As Integer, ByVal OrderId As String, ByVal PNR As String) As String
        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim cmd As New SqlCommand("CheckTktNo_New", con1)
        Dim ErrorMsg As String = ""
        Try
            cmd.CommandText = "CheckTktNo_New"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@PaxId", SqlDbType.VarChar).Value = PaxId
            cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = OrderId
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNR
            con1.Open()
            ErrorMsg = cmd.ExecuteScalar()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        Finally
            cmd.Dispose()
            con1.Close()
        End Try
        Return ErrorMsg
    End Function

    Protected Function OldPaxInfo(ByVal reissueid As String, ByVal Title As String, ByVal FName As String, ByVal MName As String, ByVal LName As String, ByVal PaxType As String) As DataSet
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim adap As New SqlDataAdapter("SP_GetOldPaxDetails", con)
        Dim Paxds As New DataSet()
        ''SP_GetOldPaxDetails(@reissueid varchar(50), @Title varchar(20), @FName varchar(50), @MName varchar(50), @LName varchar(50), @PaxType varchar(20))
        Try
            con.Open()
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@reissueid", reissueid)
            adap.SelectCommand.Parameters.AddWithValue("@Title", Title)
            adap.SelectCommand.Parameters.AddWithValue("@FName", FName)
            adap.SelectCommand.Parameters.AddWithValue("@MName", MName)
            adap.SelectCommand.Parameters.AddWithValue("@LName", LName)
            adap.SelectCommand.Parameters.AddWithValue("@PaxType", PaxType)
            adap.Fill(Paxds)

        Catch ex1 As Exception
            clsErrorLog.LogInfo(ex1)
        Finally
            con.Close()
        End Try
        Return Paxds
    End Function





    Protected Sub SendMail_Refund(ByVal Modules As String, ByVal fltTD As DataTable, ByVal caclestatus As String, ByVal paxdt() As DataRow, ByVal Sector As String)
        Dim ObjIntDetails As New IntlDetails()
        Try
            Dim mailds As New DataSet()
            Dim strMailMsgHold As String
            strMailMsgHold = "<table>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><h2> Cancellation/Refund Request </h2>"
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + fltTD.Rows(0).Item("OrderId")
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>PnrNo: </b>" + fltTD.Rows(0).Item("PNR")
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"

            For j As Integer = 0 To paxdt.Length - 1
                strMailMsgHold = strMailMsgHold & "<tr>"
                strMailMsgHold = strMailMsgHold & "<td><b>Pax Name: </b>" + paxdt(j).Item("Title").ToString + " " + paxdt(j).Item("FName") + " " + paxdt(j).Item("LName")
                strMailMsgHold = strMailMsgHold & "</td>"
                strMailMsgHold = strMailMsgHold & "</tr>"
                strMailMsgHold = strMailMsgHold & "<tr>"
                strMailMsgHold = strMailMsgHold & "<td><b>Pax Type: </b>" + paxdt(j).Item("PaxType")
                strMailMsgHold = strMailMsgHold & "</td>"
                strMailMsgHold = strMailMsgHold & "</tr>"

            Next

            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Sector: </b>" + Sector
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Net Amount: </b>" + Convert.ToString(fltTD.Rows(0).Item("TotalFareAfterDis"))
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "<tr>"
            strMailMsgHold = strMailMsgHold & "<td><b>Remark: </b>" + Request("txtRemark").Trim()
            strMailMsgHold = strMailMsgHold & "</td>"
            strMailMsgHold = strMailMsgHold & "</tr>"
            'Cancellation/Refund request is submitted successfully. The refund will process up to 7 working days.
            strMailMsgHold = strMailMsgHold & "<tr>"
            If caclestatus = "Cancelled" Then
                strMailMsgHold = strMailMsgHold & "<td><b>Cancel Status: </b> Flight booking has been canceled successfully. The refund will process up to 7 working days.</td>"
            Else
                strMailMsgHold = strMailMsgHold & "<td><b>Cancel Status: </b> Cancellation/Refund request is submitted successfully. The refund will process up to 7 working days.</td>"
            End If
            strMailMsgHold = strMailMsgHold & "</tr>"
            strMailMsgHold = strMailMsgHold & "</table>"

            Dim STDOM As New SqlTransactionDom
            Dim EDt As DataTable = ObjIntDetails.Email_Credentilas(fltTD.Rows(0).Item("OrderId"), "Refund_REJECTED", paxdt(0).Item("PaxId").ToString())
            Dim MailDt As DataTable = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

            Try
                If (MailDt.Rows.Count > 0) Then
                    For k As Integer = 0 To EDt.Rows.Count - 1
                        STDOM.SendMail(EDt.Rows(k)(1).ToString(), "info@Richa Travel.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, "Ticket Refund Request", "")
                    Next
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub LogInfo(ByVal ex As Exception)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim cmd As SqlCommand
        Dim Temp As Integer = 0
        Try
            Dim trace As New System.Diagnostics.StackTrace(ex, True)

            Dim linenumber As Integer = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
            Dim ErrorMsg As String = ex.Message
            Dim fileNames As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
            con.Open()
            cmd = New SqlCommand("InsertErrorLog", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PageName", fileNames)
            cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMsg)
            cmd.Parameters.AddWithValue("@LineNumber", linenumber)
            Temp = cmd.ExecuteNonQuery()
        Catch ex1 As Exception
        Finally
            con.Close()
        End Try
    End Sub


    Public Sub BindNotifications()
        Try
            Dim adap As SqlDataAdapter
            Dim dt As New DataTable
            adap = New SqlDataAdapter("sp_GetHomeNotification", conn)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.Fill(dt)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    NotificationContent = NotificationContent + "<div class='mySlides'>" + dt.Rows(i)("Content").ToString() + "</div>"
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub



End Class
