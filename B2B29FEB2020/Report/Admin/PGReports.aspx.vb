Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Partial Class SprReports_Accounts_PGReports
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Private CllInsSelectFlt As New clsInsertSelectedFlight()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()
    Private sttusobj As New Status()
    Dim con As New SqlConnection()
    Dim PaxType As String
    Dim clsCorp As New ClsCorporate()

    Public Sub CheckEmptyValue()
        Try
            Dim FromDate As String
            Dim ToDate As String
            Dim PgStatus As String = If(drpPaymentStatus.Visible = True, If(drpPaymentStatus.SelectedValue.ToLower() <> "select", drpPaymentStatus.SelectedValue, Nothing), Nothing)

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
            'Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            'Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            'Dim Airline As String = If([String].IsNullOrEmpty(txt_Airline.Text), "", txt_Airline.Text.Trim)
            grdds.Clear()
            ''loginid ,usertype ,fromdate ,todate ,orderid ,agentid ,paymentStatus 
            grdds = GetPgTransactionDetails(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, AgentID, PgStatus)
            'BindGrid(grdds)
            ViewState("grdds") = grdds
            'If (grdds.Tables(0).Rows.Count > 0) Then
            '    DivPrint.InnerHtml = ""
            '    If spn_Projects.Visible = True Then
            '        PrintVisible.Visible = True
            '    Else : PrintVisible.Visible = False

            '    End If
            'End If
            grd_IntsaleRegis.DataSource = grdds
            grd_IntsaleRegis.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        CheckEmptyValue()
        'If (Session("IsCorp") = True AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
        '    grd_IntsaleRegis.Columns(15).Visible = False
        '    grd_IntsaleRegis.Columns(17).Visible = False
        '    grd_IntsaleRegis.Columns(18).Visible = False
        'ElseIf (Session("IsCorp") = False AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
        '    grd_IntsaleRegis.Columns(16).Visible = False

        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                Bindstatus()
                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID"), Session("User_Type"))
                If ds IsNot Nothing Then
                    '    If ds.Tables.Count > 0 Then
                    '        DropDownListProject.Items.Clear()
                    '        Dim item As New ListItem("Select")
                    '        DropDownListProject.AppendDataBoundItems = True
                    '        DropDownListProject.Items.Insert(0, item)
                    '        DropDownListProject.DataSource = ds.Tables(0)
                    '        DropDownListProject.DataTextField = "ProjectName"
                    '        DropDownListProject.DataValueField = "ProjectId"
                    '        DropDownListProject.DataBind()
                    '        spn_Projects.Visible = True
                    '        spn_Projects1.Visible = True
                    '    Else
                    '        spn_Projects.Visible = False
                    '        spn_Projects1.Visible = False

                    '    End If

                End If
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False

                Else
                End If

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
            'Dim PNR As String = If([String].IsNullOrEmpty(txt_PNR.Text), "", txt_PNR.Text.Trim)
            'Dim TicketNo As String = If([String].IsNullOrEmpty(txt_TktNo.Text), "", txt_TktNo.Text.Trim)
            'Dim Airline As String = If([String].IsNullOrEmpty(txt_Airline.Text), "", txt_Airline.Text.Trim)
            Dim PgStatus As String = If(drpPaymentStatus.Visible = True, If(drpPaymentStatus.SelectedValue.ToLower() <> "select", drpPaymentStatus.SelectedValue, Nothing), Nothing)

            grdds.Clear()
            grdds = GetPgTransactionDetails(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, OrderID, AgentID, PgStatus)
            'If (Session("IsCorp") = True AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
            '    grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("TranFee"))
            '    grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("TotalDiscount"))
            '    grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("Tds"))
            'ElseIf (Session("IsCorp") = False AndAlso Session("User_Type").ToString.ToUpper() = "AGENT") Then
            '    grdds.Tables(0).Columns.Remove(grdds.Tables(0).Columns("MgtFee"))
            'End If
            If ToDate = "" Then

                ToDate = DateTime.Now.ToString("dd-MM-yyyy")

            End If

            STDom.ExportData(grdds, "Dom._Sale_Register-From " & FromDate & "-To-" & ToDate)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

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

    Public Function GetPgTransactionDetails(ByVal loginid As String, ByVal usertype As String, ByVal fromdate As String, ByVal todate As String, ByVal orderid As String, ByVal agentid As String, ByVal paymentStatus As String) As DataSet
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear() ''@usertype,@LoginID,@FormDate,@ToDate,@OderId,@AgentId ,@PaymentStatus
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", loginid)
        paramHashtable.Add("@FormDate", fromdate)
        paramHashtable.Add("@ToDate", todate)
        paramHashtable.Add("@OderId", orderid)
        'paramHashtable.Add("@pnr", pnr)
        'paramHashtable.Add("@ticketno", ticketno)
        paramHashtable.Add("@AgentId", agentid)
        paramHashtable.Add("@PaymentStatus", paymentStatus)
        'paramHashtable.Add("@trip", trip)
        'paramHashtable.Add("@projectid", projectid)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "spGetPgReport", 3)
    End Function

    Public Sub Bindstatus()
        Try

            Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("BindStatus_PP")

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = con
            con.Open()
            drpPaymentStatus.DataSource = cmd.ExecuteReader()
            drpPaymentStatus.DataTextField = "status"

            drpPaymentStatus.DataBind()
            con.Close()
            drpPaymentStatus.Items.Insert(0, New ListItem("--Select status--", "0"))
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try


    End Sub




End Class
