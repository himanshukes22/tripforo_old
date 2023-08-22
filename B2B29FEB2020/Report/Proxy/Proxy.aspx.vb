Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Object
Imports System.Data

Partial Public Class Report_Proxy_Proxy

    Inherits System.Web.UI.Page
    Dim STDom As New SqlTransactionDom()
    Dim clsCorp As New ClsCorporate()
    Dim objDA As New SqlTransaction


    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Submit.Click

        Try
            Dim AgencyDs As DataSet
            AgencyDs = objDA.GetAgencyDetails(Session("UID"))
            Dim EmlTmp As New SendEmailTemp
            Dim EmailBody As String = EmlTmp.GetEmptyEmailTemp(Session("UID").ToString())

            Dim MailDt As DataTable = STDom.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)

            If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.ToUpper.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.ToUpper.Trim <> "NOT ACTIVE" Then
                Dim FromDate As String = Request("txtDepDate").ToString

                Dim ToDate As String = Request("txtRetDate").ToString
                Dim split_str1 As String() = FromDate.Split("-"c)
                Dim split_str2 As String() = ToDate.Split("-"c)
                Dim FromDEST As String = Request("hidtxtDepCity1").ToString
                Dim ToDEST As String = Request("hidtxtArrCity1").ToString
                Dim split_str3 As String() = FromDEST.Split(","c)
                Dim split_str4 As String() = ToDEST.Split(","c)
                Dim FromCtry As String = split_str3(1)
                Dim ToCtry As String = split_str4(1)
                Dim Adult As String
                Dim Child As String
                Dim Infrant As String
                Dim AgentID As String = Session("UID").ToString()
                Dim Ag_Name As String = Session("AgencyName").ToString
                Dim ExpectedAmount As String = TxtExpectedFare.Text

                Session("ProjectId") = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
                Dim projectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
                Session("BookedBy") = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)
                Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)


                If RB_OneWay.Text = "One Way" AndAlso RB_OneWay.Checked = True Then
                    'Session("Adult") = ddl_Adult.SelectedItem.Text
                    Session("Adult") = ddl_Adult.Text
                    Adult = ddl_Adult.Text
                    'Session("Child") = ddl_Child.SelectedItem.Text
                    If ddl_Child.Text = "" Or ddl_Child.Text = "0" Then
                        Session("Child") = "0"
                        Child = "0"
                    Else
                        Session("Child") = ddl_Child.Text
                        Child = ddl_Child.Text
                    End If
                    'Session("Child") = ddl_Child.Text
                    'Session("Infrant") = ddl_Infrant.SelectedItem.Text
                    If ddl_Infrant.Text = "" Or ddl_Infrant.Text = "0" Then
                        Session("Infrant") = "0"
                        Infrant = "0"
                    Else
                        Session("Infrant") = ddl_Infrant.Text
                        Infrant = ddl_Infrant.Text
                    End If

                    Session("A") = True
                    Session("BookingType") = RBL_Booking.SelectedItem.Text
                    Session("OneWay") = RB_OneWay.Text
                    'Add only City Code
                    Session("From") = split_str3(0)
                    Session("To") = split_str4(0)
                    'Added only Country Codes to Session'
                    Session("FromCtry") = FromCtry
                    Session("ToCtry") = ToCtry
                    Session("DepartDD") = split_str1(0) 'Splitting Departure Date in one way
                    Session("DepartMM") = split_str1(1)
                    Session("DepartYYYY") = split_str1(2).Substring(2, 2) 'ddldepart_YYYY.SelectedValue
                    Session("DepartAnyTime") = ddl_DepartAnytime.SelectedItem.Text
                    Session("Class") = ddl_Class.SelectedItem.Text
                    Session("Airline") = Right((Request("hidtxtAirline")), 2)
                    Session("Classes") = ddl_Classes.SelectedItem.Text
                    Session("PaymentMode") = ddl_PaymentMode.SelectedValue
                    Session("Remark") = txt_Remark.Text
                    Dim Ptype As String
                    If FromCtry = "IN" And ToCtry = "IN" Then
                        Ptype = "D"
                    Else
                        Ptype = "I"
                    End If
                    Dim DepartDD As String = split_str1(0)
                    Dim DepartMM As String = split_str1(1)
                    Dim DepartYYYY As String = split_str1(2).Substring(2, 2)
                    Dim DepartDate As String = DepartDD & "/" & DepartMM & "/" & Right(DepartYYYY, 2)
                    'Response.Redirect("ProxyEmp.aspx")
                    Dim PID As Integer = STDom.insertProxyDetails("", "OneWay", split_str3(0), split_str4(0), DepartDate, "", ddl_DepartAnytime.SelectedItem.Text, "", Adult, Child, Infrant, ddl_Class.SelectedItem.Text, Right((Request("hidtxtAirline")), 2), "", "", txt_Remark.Text, AgentID, Ag_Name, "Pending", "O", Ptype, projectId, BookedBy, ExpectedAmount)
                    If PID > 0 Then
                        Try
                            If (MailDt.Rows.Count > 0) Then

                                Try
                                    'Send Mail Agent
                                    If (AgencyDs.Tables(0).Rows.Count > 0) Then
                                        EmailBody = EmailBody.Replace("#bodyheader", "Offline Request").Replace("#body", "Your offline request has been successfully sent.We will update you as soon a posiible.")
                                        STDom.SendMail(Convert.ToString(AgencyDs.Tables(0).Rows(0)("Email")), Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), "test hai", " Payment Upload Request", "")
                                        EmailBody = EmailBody.Replace("Dear", "Dear Admin,<br>new offline request from").Replace("Your offline request has been successfully sent.We will update you as soon a posiible.", "")
                                        STDom.SendMail("sales@tripforo.com", Convert.ToString(MailDt.Rows(0)("MAILFROM")), "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), EmailBody, "Offline Request", "")
                                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Offline Request Sent Successfully.');window.location='Proxy.aspx';", True)

                                    End If
                                Catch ex As Exception
                                    clsErrorLog.LogInfo(ex)
                                End Try

                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                        End Try

                    End If

                Else

                    ' Session("Adult") = ddl_Adult.SelectedItem.Text
                    Session("Adult") = ddl_Adult.Text
                    Adult = ddl_Adult.Text
                    If ddl_Child.Text = "" Or ddl_Child.Text = "0" Then
                        Session("Child") = "0"
                        Child = "0"
                    Else
                        Session("Child") = ddl_Child.Text
                        Child = ddl_Child.Text
                    End If
                    'Session("Child") = ddl_Child.Text
                    'Session("Infrant") = ddl_Infrant.SelectedItem.Text
                    If ddl_Infrant.Text = "" Or ddl_Infrant.Text = "0" Then
                        Session("Infrant") = "0"
                        Infrant = "0"
                    Else
                        Session("Infrant") = ddl_Infrant.Text
                        Infrant = ddl_Infrant.Text
                    End If
                    Session("A") = False
                    Session("BookingType") = RBL_Booking.SelectedItem.Text
                    Session("RoundTrip") = RB_RoundTrip.Text
                    'Splitting Departure Date and Return in 2 way
                    Session("From") = split_str3(0)                 'ddl_From.SelectedValue
                    Session("To") = split_str4(0)                   'ddl_To.SelectedValue
                    Session("DepartDD") = split_str1(0)             'ddldepart_DD.SelectedValue
                    Session("DepartMM") = split_str1(1)             'ddldepart_MM.SelectedValue
                    Session("DepartYYYY") = split_str1(2).Substring(2, 2) 'ddldepart_YYYY.SelectedValue
                    Session("DepartAnyTime") = ddl_DepartAnytime.SelectedItem.Text
                    'Splitting Return Date
                    Session("ToDD") = split_str2(0)                     'ddlto_DD.SelectedValue
                    Session("ToMM") = split_str2(1)                     'ddlto_MM.SelectedValue
                    Session("ToYYYY") = split_str2(2).Substring(2, 2)   'ddlto_YYYY.SelectedValue
                    Session("ReturnAnyTime") = ddl_ReturnAnytime.SelectedItem.Text
                    Session("Class") = ddl_Class.SelectedItem.Text
                    Session("Airline") = Right((Request("hidtxtAirline")), 2)        'ddl_Airlines.SelectedValue
                    Session("Classes") = ddl_Classes.SelectedItem.Text
                    Session("PaymentMode") = ddl_PaymentMode.SelectedValue
                    Session("Remark") = txt_Remark.Text
                    'Added only Country Codes to Session'
                    Session("FromCtry") = FromCtry
                    Session("ToCtry") = ToCtry
                    'Response.Redirect("ProxyEmp.aspx")
                    Dim Ptype As String
                    If FromCtry = "IN" And ToCtry = "IN" Then
                        Ptype = "D"
                    Else
                        Ptype = "I"
                    End If
                    Dim DepartDD As String = split_str1(0)
                    Dim DepartMM As String = split_str1(1)
                    Dim DepartYYYY As String = split_str1(2).Substring(2, 2)
                    Dim DepartDate As String = DepartDD & "/" & DepartMM & "/" & Right(DepartYYYY, 2)

                    Dim ToDD As String = split_str2(0)
                    Dim ToMM As String = split_str2(1)
                    Dim ToYYYY As String = split_str2(2).Substring(2, 2)
                    Dim ReturnDate As String = ToDD & "/" & ToMM & "/" & Right(ToYYYY, 2)
                    Dim PID As Integer = STDom.insertProxyDetails("", "Round Trip", split_str3(0), split_str4(0), DepartDate, ReturnDate, ddl_DepartAnytime.SelectedItem.Text, "", Adult, Child, Infrant, ddl_Class.SelectedItem.Text, Right((Request("hidtxtAirline")), 2), "", "", txt_Remark.Text, AgentID, Ag_Name, "Pending", "O", Ptype, projectId, BookedBy, ExpectedAmount)
                    If PID > 0 Then

                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Proxy Request Sent Successfully.');window.location='Proxy.aspx';", True)

                    End If
                End If

            Else
                ShowAlertMessage("You are not authorized. Please contact to your sales person")
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');window.location='Proxy.aspx';", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then
                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
                If ds Is Nothing Then

                Else
                    If ds.Tables(0).Rows.Count > 0 Then
                        DropDownListProject.Items.Clear()
                        Dim item As New ListItem("Select")
                        DropDownListProject.AppendDataBoundItems = True
                        DropDownListProject.Items.Insert(0, item)
                        DropDownListProject.DataSource = ds.Tables(0)
                        DropDownListProject.DataTextField = "ProjectName"
                        DropDownListProject.DataValueField = "ProjectId"
                        DropDownListProject.DataBind()
                        Dim dtbooked As New DataTable
                        dtbooked = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "BB").Tables(0)
                        If ds.Tables(0).Rows.Count > 0 Then
                            DropDownListBookedBy.AppendDataBoundItems = True
                            DropDownListBookedBy.Items.Clear()
                            DropDownListBookedBy.Items.Insert(0, "Select")
                            DropDownListBookedBy.DataSource = dtbooked
                            DropDownListBookedBy.DataTextField = "BOOKEDBY"
                            DropDownListBookedBy.DataValueField = "BOOKEDBY"
                            DropDownListBookedBy.DataBind()
                        End If

                        TBL_Projects.Visible = True
                    Else
                        TBL_Projects.Visible = False

                    End If

                End If
                'Me.ddldepart_MM.SelectedValue = DateTime.Now.Month.ToString()
                'Me.ddldepart_DD.SelectedValue = DateTime.Now.Day.ToString()
                'Me.ddldepart_YYYY.SelectedValue = DateTime.Now.Year.ToString()
                'Me.ddlto_MM.SelectedValue = DateTime.Now.Month.ToString()
                'Me.ddlto_DD.SelectedValue = DateTime.Now.Day.ToString()
                'Me.ddlto_YYYY.SelectedValue = DateTime.Now.Year.ToString()
                'ddlto_MM.Enabled = False
                'ddlto_DD.Enabled = False
                'ddlto_YYYY.Enabled = False
                'ddl_ReturnAnytime.Enabled = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try


    End Sub
End Class