Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class Reports_Accounts_IntlSaleRegister
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()
    Private sttusobj As New Status()
    Dim con As New SqlConnection()
    Dim PaxType As String
    Dim clsCorp As New ClsCorporate()
    Dim FromDate1 As String
    Dim ToDate1 As String
    Public Sub CheckEmptyValue()
        Try
            Dim FromDate As String
            Dim ToDate As String
            Dim ProjectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)

            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If


            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))



            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Airline As String = If([String].IsNullOrEmpty(txt_Airline.Text), "", txt_Airline.Text.Trim)
            grdds.Clear()

            grdds = ST.IntGetInvoice(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, TicketNo, AgentID, "I", Airline, ProjectId)
            ViewState("grdds") = grdds
            If (grdds.Tables(0).Rows.Count > 0) Then
                DivPrint.InnerHtml = ""


                If spn_Projects.Visible = True Then
                    PrintVisible.Visible = True
                Else : PrintVisible.Visible = False

                End If
            End If
            grd_IntsaleRegis.DataSource = grdds
            grd_IntsaleRegis.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        FromDate1 = Request("From")
        ToDate1 = Request("To")
        If FromDate1.ToString <> Nothing And ToDate1 <> Nothing Then
            If DateTime.ParseExact(FromDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(ToDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", True)
            Else
                CheckEmptyValue()
                If (Session("IsCorp") = True AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                    grd_IntsaleRegis.Columns(15).Visible = False
                    grd_IntsaleRegis.Columns(17).Visible = False
                    grd_IntsaleRegis.Columns(18).Visible = False
                ElseIf (Session("IsCorp") = False AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                    grd_IntsaleRegis.Columns(16).Visible = False

                End If
            End If
        Else
            CheckEmptyValue()
            If (Session("IsCorp") = True AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                grd_IntsaleRegis.Columns(15).Visible = False
                grd_IntsaleRegis.Columns(17).Visible = False
                grd_IntsaleRegis.Columns(18).Visible = False
            ElseIf (Session("IsCorp") = False AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                grd_IntsaleRegis.Columns(16).Visible = False

            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim AgentID As String = Nothing
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
                If ds IsNot Nothing Then
                    If ds.Tables.Count > 0 Then
                        DropDownListProject.Items.Clear()
                        Dim item As New ListItem("Select")
                        DropDownListProject.AppendDataBoundItems = True
                        DropDownListProject.Items.Insert(0, item)
                        DropDownListProject.DataSource = ds.Tables(0)
                        DropDownListProject.DataTextField = "ProjectName"
                        DropDownListProject.DataValueField = "ProjectId"
                        DropDownListProject.DataBind()
                        spn_Projects.Visible = True
                        'spn_Projects1.Visible = True
                    Else
                        spn_Projects.Visible = False
                        'spn_Projects1.Visible = False

                    End If
                    If Session("User_Type") = "AGENT" Then
                        td_Agency.Visible = False
                        AgentID = Session("UID").ToString()




                    Else


                    End If
                    'Else
                    'AgencyDDLDS = ST.GetAgencyDetailsDDL()
                    'If AgencyDDLDS.Tables(0).Rows.Count > 0 Then
                    '    ' Bind Agency DDL
                    '    Try
                    '        ddl_AgencyName.AppendDataBoundItems = True
                    '        ddl_AgencyName.Items.Clear()
                    '        ddl_AgencyName.Items.Insert(0, "--Select Agency Name--")
                    '        ddl_AgencyName.DataSource = AgencyDDLDS
                    '        ddl_AgencyName.DataTextField = "Agency_Name"
                    '        ddl_AgencyName.DataValueField = "user_id"
                    '        ddl_AgencyName.DataBind()

                    '    Catch ex As Exception
                    '        clsErrorLog.LogInfo(ex)

                    '    End Try
                    'End If
                End If

                'Dim curr_date = Now.Date() & " " & "00:00:00 AM"
                'Dim curr_date1 = Now()
                'grdds.Clear()
                'grdds = ST.IntGetInvoice(Session("UID").ToString, Session("User_Type").ToString, curr_date, curr_date1, Nothing, Nothing, Nothing, AgentID, "I", Nothing)
                'ViewState("grdds") = grdds
                'grd_IntsaleRegis.DataSource = grdds
                'grd_IntsaleRegis.DataBind()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub grd_IntsaleRegis_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd_IntsaleRegis.PageIndexChanging
        grd_IntsaleRegis.PageIndex = e.NewPageIndex
        grd_IntsaleRegis.DataSource = ViewState("grdds")
        grd_IntsaleRegis.DataBind()
    End Sub
    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
         FromDate1 = Request("From")
        ToDate1 = Request("To")
        If FromDate1.ToString <> Nothing And ToDate1 <> Nothing Then
            If DateTime.ParseExact(FromDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(ToDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture) Then
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", True)
            Else
                ExportResult()
            End If
        Else
            ExportResult()
        End If

    End Sub
    Public Sub ExportResult()
        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If


            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))



            Dim OrderID As String = If([String].IsNullOrEmpty(txt_OrderId.Text), "", txt_OrderId.Text.Trim)
            Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            Dim Airline As String = If([String].IsNullOrEmpty(txt_Airline.Text), "", txt_Airline.Text.Trim)
            Dim ProjectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)

            grdds.Clear()

            grdds = ST.IntGetInvoice(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, PNR, TicketNo, AgentID, "I", Airline, ProjectId)

            grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("AgentType"))
            grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("CreateDate"))
            grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("AgentID"))
            If (Session("IsCorp") = True AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("TranFee"))
                grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("TotalDiscount"))
                grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("Tds"))
            ElseIf (Session("IsCorp") = False AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
                grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("MgtFee"))
            End If
            If ToDate = "" Then

                ToDate = DateTime.Now.ToString("dd-MM-yyyy")

            End If
            STDom.ExportData(grdds, "Intl._Sale_Register-From " & FromDate & "-To-" & ToDate)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Protected Sub ButtonPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrint.Click

        'Try
        '    Dim ds As DataSet = DirectCast(ViewState("grdds"), DataSet)
        '    If (TextBoxPrintNo.Text <> "") Then
        '        If ds.Tables(0).Rows.Count > 0 Then


        '            Dim pageNo As String() = TextBoxPrintNo.Text.Split("-")
        '            If Convert.ToInt16(pageNo(1)) <= ds.Tables(0).Rows.Count Then
        '                If Convert.ToInt16(pageNo(1)) <= 50 Then
        '                    DivPrint.InnerHtml = GetInvoicePrintHtml(Convert.ToInt16(pageNo(0)), Convert.ToInt16(pageNo(1)), ds)
        '                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "callprint('" & DivPrint.ClientID.ToString() & "');", True)
        '                Else
        '                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('You can not print more than 50 invoice at a time.');", True)
        '                End If
        '            Else
        '                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('page limit must me less than search result.');", True)
        '            End If




        '        End If
        '    Else
        '        ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('Please enter page range');", True)
        '    End If
        'Catch ex As Exception
        '    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('Please enter page range');", True)
        'End Try
        Try
            Dim ds As DataSet = DirectCast(ViewState("grdds"), DataSet)
            If (TextBoxPrintNo.Text <> "") Then

                Dim dsCount As Integer = ds.Tables(0).Rows.Count

                Dim dtUniqRecordsByOerderID As New DataTable()
                dtUniqRecordsByOerderID = ds.Tables(0).DefaultView.ToTable(True, "OrderId")
                If dtUniqRecordsByOerderID.Rows.Count > 0 Then


                    Dim pageNo As String() = TextBoxPrintNo.Text.Split("-")
                    ' If Convert.ToInt16(pageNo(1)) <= dtUniqRecordsByOerderID.Rows.Count Then
                    If Convert.ToInt16(pageNo(1)) > Convert.ToInt16(pageNo(0)) Then


                        If (Convert.ToInt16(pageNo(1)) - Convert.ToInt16(pageNo(0)) + 1) <= 50 Then
                            DivPrint.InnerHtml = GetInvoicePrintHtml(Convert.ToInt16(pageNo(0)), Convert.ToInt16(pageNo(1)), dtUniqRecordsByOerderID)
                            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "callprint('" & DivPrint.ClientID.ToString() & "');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('You can not print more than 50 invoice at a time.');", True)
                        End If

                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('Please Provide Page Range in proper Format.');", True)

                    End If

                    'Else
                    '   ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('page limit must be less than search result.');", True)
                    ' End If




                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('Please enter page range');", True)

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "print", "alert('Please enter page range');", True)
        End Try

       

    End Sub

    'Public Function GetInvoicePrintHtml(ByVal FromPage As Integer, ByVal ToPage As Integer, ByVal ds As DataSet) As String

    '    Dim resultHtml As String = ""


    '    If ds.Tables(0).Rows.Count > 0 Then

    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


    '            If i >= FromPage - 1 AndAlso i < ToPage Then

    '                Dim ri As Integer = i
    '                resultHtml += "<div style='page-break-after:always;'>"
    '                resultHtml += clsCorp.ShowInvoice(ds.Tables(0).Rows(i)("OrderId"))
    '                resultHtml += "</div>"




    '            End If

    '        Next


    '        'For Each row As DataRow In ds.Tables(0).Rows

    '        '    If row. >= FromPage - 1 AndAlso row.RowIndex < ToPage Then
    '        '        Dim ri As Integer = row.RowIndex

    '        '        resultHtml += ShowInvoice("e563935bc4BOhNLO")




    '        '    End If


    '        'Next

    '    End If

    '    Return resultHtml
    'End Function
    Public Function GetInvoicePrintHtml(ByVal FromPage As Integer, ByVal ToPage As Integer, ByVal dt As DataTable) As String

        Dim resultHtml As String = ""


        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1


                If i >= FromPage - 1 AndAlso i < ToPage Then

                    Dim ri As Integer = i
                    resultHtml += "<div style='page-break-after:always;'>"
                    resultHtml += clsCorp.ShowInvoice(dt.Rows(i)("OrderId"))
                    resultHtml += "</div>"




                End If

            Next



        End If

        Return resultHtml
    End Function


End Class
