Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Configuration
Imports System.Data


Partial Public Class Reports_Proxy_ProxyEmp
    Inherits System.Web.UI.Page
    Dim STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                Try
                    'lbl_Adult.Text = Session("Adult").ToString()
                    'lbl_Child.Text = Session("Child").ToString()
                    'lbl_Infrant.Text = Session("Infrant").ToString()
                    lbl_Adult.Text = Request.QueryString("Adult")
                    lbl_Child.Text = Request.QueryString("Child")
                    lbl_Infrant.Text = Request.QueryString("Infrant")
                    Bind_pax(lbl_Adult.Text, lbl_Child.Text, lbl_Infrant.Text)
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub Bind_pax(ByVal cntAdult As Integer, ByVal cntChild As Integer, ByVal cntInfant As Integer)
        Try
            Dim PaxTbl As New DataTable()
            Dim cntTblColumn As DataColumn = Nothing
            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.Double")
            cntTblColumn.ColumnName = "Counter"
            PaxTbl.Columns.Add(cntTblColumn)
            cntTblColumn = New DataColumn()
            cntTblColumn.DataType = Type.[GetType]("System.String")
            cntTblColumn.ColumnName = "PaxTP"
            PaxTbl.Columns.Add(cntTblColumn)
            Dim cntrow As DataRow = Nothing
            For i As Integer = 1 To cntAdult
                cntrow = PaxTbl.NewRow()
                cntrow("Counter") = i
                cntrow("PaxTP") = "Passenger " & i.ToString() & " (Adult)"
                PaxTbl.Rows.Add(cntrow)
            Next
            Repeater_Adult.DataSource = PaxTbl
            Repeater_Adult.DataBind()
            PaxTbl.Clear()
            If cntChild > 0 Then
                For i As Integer = 1 To cntChild
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Child)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Child.DataSource = PaxTbl
                Repeater_Child.DataBind()
            Else
                td_Child.Visible = False
            End If
            PaxTbl.Clear()
            If cntInfant > 0 Then
                For i As Integer = 1 To cntInfant
                    cntrow = PaxTbl.NewRow()
                    cntrow("Counter") = i
                    cntrow("PaxTP") = "Passenger " & i.ToString() & " (Infant)"
                    PaxTbl.Rows.Add(cntrow)
                Next
                Repeater_Infant.DataSource = PaxTbl
                Repeater_Infant.DataBind()
            Else
                td_Infant.Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub Button2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try
            ' Dim [True] As String = Session("A").ToString()
            Dim TrvlTyp As String = Request.QueryString("TravelType").ToString()

            ' If [True] = "True" Then
            If TrvlTyp = "OneWay" Then
                InsertProxyTicketOneWay()
                Repeater_Adult.Visible = False
                Repeater_Child.Visible = False
                Repeater_Infant.Visible = False
                Button2.Visible = False

                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Proxy Request Sent Successfully.');window.location='Proxy.aspx';", True)
            Else
                InsertProxyTicketRoundTrip()
                Repeater_Adult.Visible = False
                Repeater_Child.Visible = False
                Repeater_Infant.Visible = False
                Button2.Visible = False
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Proxy Request Sent Successfully.');window.location='Proxy.aspx';", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub 'Final Submit Button Calls Insert proxy 
    Public Sub InsertPaxDetail(ByVal ProxyID As Integer, ByVal Title As String, ByVal FirstName As String, ByVal LastName As String, ByVal Age As String, ByVal AgentID As String, ByVal PaxType As String, ByVal FFNO As String, ByVal PASSNO As String, ByVal PPEXP As String, ByVal VISADET As String)
        Try
            InsertProxyPaxDetail(ProxyID, Title, FirstName, LastName, Age, AgentID, PaxType, FFNO, PASSNO, PPEXP, VISADET)
            '' STDom.InsertProxyPaxDetail(ProxyID, Title, FirstName, LastName, Age, AgentID, PaxType)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Public Sub InsertProxyTicketOneWay() 'Insert Data 4 One way proxy by Calling InsertPaxDetail Function of SqlTransactionDom CLASS
        Try
            'Dim Adult As String = Session("Adult").ToString()
            'Dim Child As String = Session("Child").ToString()
            'Dim Infrant As String = Session("Infrant").ToString()
            'Dim BookingType As String = Session("BookingType").ToString()
            'Dim OneWay As String = Session("OneWay").ToString()
            'Dim From As String = Session("From").ToString() 'Need to Change
            'Dim [To] As String = Session("To").ToString()   'Need to Change 
            'Dim DepartMM As String = Session("DepartMM").ToString()
            'Dim DepartDD As String = Session("DepartDD").ToString()
            'If DepartMM.Length = 1 Then DepartMM = "0" & DepartMM
            'If DepartDD.Length = 1 Then DepartDD = "0" & DepartDD
            'Dim DepartYYYY As String = Session("DepartYYYY").ToString()
            'Dim DepartAnyTime As String = Session("DepartAnyTime").ToString()
            'Dim [Class] As String = Session("Class").ToString()
            'Dim Airline As String = Session("Airline").ToString()
            'Dim Classes As String = Session("Classes").ToString()
            'Dim PaymentMode As String = Session("PaymentMode").ToString()
            'Dim Remark As String = Session("Remark").ToString()
            'Dim DepartDate As String = DepartDD & "/" & DepartMM & "/" & Right(DepartYYYY, 2)
            'Dim AgentID As String = Session("UID").ToString()
            'Dim Ag_Name As String = Session("AgencyName").ToString
            'Dim FromCtry As String = Session("FromCtry").ToString 'Retrived Country Details
            'Dim ToCtry As String = Session("ToCtry").ToString 'Retreived Country Details
            'Dim Ptype As String
            'Dim projectId As String = If(Session("ProjectId") Is Nothing, Nothing, Session("ProjectId").ToString())
            'Dim BookedBy As String = If(Session("BookedBy") Is Nothing, Nothing, Session("BookedBy").ToString())

            'If FromCtry = "IN" And ToCtry = "IN" Then
            '    Ptype = "D"
            'Else
            '    Ptype = "I"
            'End If
            ''Insert Details into Proxy Ticket Table with Trip = oneway 'O'
            'Dim PID As Integer = STDom.insertProxyDetails(BookingType, OneWay, From, [To], DepartDate, "", DepartAnyTime, "", Adult, Child, Infrant, [Class], Airline, Classes, PaymentMode, Remark, AgentID, Ag_Name, "Pending", "O", Ptype, projectId, BookedBy, "")
            'Above Code commented By sonu kumar on 20-09-2021

            Dim pro_id As String = Request.QueryString("Proxyid").ToString()
            Dim AgentID As String = Request.QueryString("AgentID").ToString()
            Dim PID As Integer = Convert.ToInt32(pro_id)
            For Each rw As RepeaterItem In Repeater_Adult.Items
                Dim ddl_ATitle As DropDownList = DirectCast(rw.FindControl("ddl_ATitle"), DropDownList)
                Dim txtAFirstName As TextBox = DirectCast(rw.FindControl("txtAFirstName"), TextBox)
                Dim txtALastName As TextBox = DirectCast(rw.FindControl("txtALastName"), TextBox)
                Dim ddl_AAge As DropDownList = DirectCast(rw.FindControl("ddl_Age"), DropDownList)
                Dim txtAFFno As TextBox = DirectCast(rw.FindControl("ID_AFreqFlyerNO"), TextBox)
                Dim txtApassno As TextBox = DirectCast(rw.FindControl("ID_ApassNo"), TextBox)
                Dim txtAppexp As TextBox = DirectCast(rw.FindControl("ID_APPEXP"), TextBox)
                Dim ID_AVisaDet As TextBox = DirectCast(rw.FindControl("ID_AVisaDet"), TextBox)
                InsertPaxDetail(PID, ddl_ATitle.SelectedValue.ToString, txtAFirstName.Text.Trim, txtALastName.Text.Trim, ddl_AAge.SelectedValue.ToString(), AgentID, "ADT", txtAFFno.Text.Trim, txtApassno.Text.Trim, txtAppexp.Text.Trim, ID_AVisaDet.Text.Trim)
            Next
            If lbl_Child.Text > 0 Then
                For Each rw As RepeaterItem In Repeater_Child.Items
                    Dim ddl_CTitle As DropDownList = DirectCast(rw.FindControl("ddl_CTitle"), DropDownList)
                    Dim txtCFirstName As TextBox = DirectCast(rw.FindControl("txtCFirstName"), TextBox)
                    Dim txtCLastName As TextBox = DirectCast(rw.FindControl("txtCLastName"), TextBox)
                    Dim ddl_CAge As DropDownList = DirectCast(rw.FindControl("ddl_AgeChild"), DropDownList)
                    Dim txtCFFno As TextBox = DirectCast(rw.FindControl("ID_CFreqFlyerNO"), TextBox)
                    Dim txtCpassno As TextBox = DirectCast(rw.FindControl("ID_CpassNo"), TextBox)
                    Dim txtCppexp As TextBox = DirectCast(rw.FindControl("ID_CPPEXP"), TextBox)
                    Dim ID_CVisaDet As TextBox = DirectCast(rw.FindControl("ID_CVisaDet"), TextBox)
                    InsertPaxDetail(PID, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim, txtCLastName.Text.Trim, ddl_CAge.SelectedValue, AgentID, "CHD", txtCFFno.Text.Trim, txtCpassno.Text.Trim, txtCppexp.Text.Trim, ID_CVisaDet.Text.Trim)
                Next
            End If
            If lbl_Infrant.Text > 0 Then

                For Each rw As RepeaterItem In Repeater_Infant.Items

                    Dim ddl_ITitle As DropDownList = DirectCast(rw.FindControl("ddl_ITitle"), DropDownList)
                    Dim txtIFirstName As TextBox = DirectCast(rw.FindControl("txtIFirstName"), TextBox)
                    Dim txtILastName As TextBox = DirectCast(rw.FindControl("txtILastName"), TextBox)
                    Dim ddl_IAge As DropDownList = DirectCast(rw.FindControl("ddl_AgeInfant"), DropDownList)
                    Dim txtIFFno As TextBox = DirectCast(rw.FindControl("ID_IFreqFlyerNO"), TextBox)
                    Dim txtIpassno As TextBox = DirectCast(rw.FindControl("ID_IpassNo"), TextBox)
                    Dim txtIppexp As TextBox = DirectCast(rw.FindControl("ID_IPPEXP"), TextBox)
                    Dim ID_IVisaDet As TextBox = DirectCast(rw.FindControl("ID_IVisaDet"), TextBox)
                    InsertPaxDetail(PID, ddl_ITitle.SelectedValue, txtIFirstName.Text.Trim, txtILastName.Text.Trim, ddl_IAge.SelectedValue, AgentID, "INF", txtIFFno.Text.Trim, txtIpassno.Text.Trim, txtIppexp.Text.Trim, ID_IVisaDet.Text.Trim)
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub InsertProxyTicketRoundTrip()
        Try
            'Dim Adult As String = Session("Adult").ToString()
            'Dim Child As String = Session("Child").ToString()
            'Dim Infrant As String = Session("Infrant").ToString()
            'Dim BookingType As String = Session("BookingType").ToString()
            'Dim RoundTrip As String = Session("RoundTrip").ToString()
            'Dim From As String = Session("From").ToString()
            'Dim [To] As String = Session("To").ToString()
            'Dim DepartMM As String = Session("DepartMM").ToString()
            'Dim DepartDD As String = Session("DepartDD").ToString()
            'If DepartMM.Length = 1 Then DepartMM = "0" & DepartMM
            'If DepartDD.Length = 1 Then DepartDD = "0" & DepartDD
            'Dim DepartYYYY As String = Session("DepartYYYY").ToString()
            'Dim DepartAnyTime As String = Session("DepartAnyTime").ToString()
            'Dim ToMM As String = Session("ToMM").ToString()
            'Dim ToDD As String = Session("ToDD").ToString()
            'If ToMM.Length = 1 Then ToMM = "0" & ToMM
            'If ToDD.Length = 1 Then ToDD = "0" & ToDD
            'Dim ToYYYY As String = Session("ToYYYY").ToString()
            'Dim ToAnyTime As String = Session("ReturnAnyTime").ToString()
            'Dim [Class] As String = Session("Class").ToString()
            'Dim Airline As String = Session("Airline").ToString()
            'Dim Classes As String = Session("Classes").ToString()
            'Dim PaymentMode As String = Session("PaymentMode").ToString()
            'Dim Remark As String = Session("Remark").ToString()
            'Dim AgentID As String = Session("UID").ToString()
            'Dim DepartDate As String = DepartDD & "/" & DepartMM & "/" & Right(DepartYYYY, 2)
            'Dim ReturnDate As String = ToDD & "/" & ToMM & "/" & Right(ToYYYY, 2)
            'Dim Ag_Name As String = Session("AgencyName").ToString
            'Dim FromCtry As String = Session("FromCtry").ToString 'Retrived Country Details
            'Dim ToCtry As String = Session("ToCtry").ToString 'Retreived Country Details
            'Dim Ptype As String 'Set valueof proxy Type 
            'Dim RprojectId As String = If(Session("ProjectId") Is Nothing, Nothing, Session("ProjectId").ToString())
            'Dim RBookedBy As String = If(Session("BookedBy") Is Nothing, Nothing, Session("BookedBy").ToString())

            'If FromCtry = "IN" And ToCtry = "IN" Then
            '    Ptype = "D"
            'Else
            '    Ptype = "I"
            'End If
            'Dim PID As Integer = STDom.insertProxyDetails(BookingType, RoundTrip, From, [To], DepartDate, ReturnDate, DepartAnyTime, ToAnyTime, Adult, Child, Infrant, [Class], Airline, Classes, PaymentMode, Remark, AgentID, Ag_Name, "Pending", "R", Ptype, RprojectId, RBookedBy, "")
            Dim pro_id As String = Request.QueryString("Proxyid").ToString()
            Dim AgentID As String = Request.QueryString("AgentID").ToString()
            Dim PID As Integer = Convert.ToInt32(pro_id)
            For Each rw As RepeaterItem In Repeater_Adult.Items
                Dim ddl_ATitle As DropDownList = DirectCast(rw.FindControl("ddl_ATitle"), DropDownList)
                Dim txtAFirstName As TextBox = DirectCast(rw.FindControl("txtAFirstName"), TextBox)
                Dim txtALastName As TextBox = DirectCast(rw.FindControl("txtALastName"), TextBox)
                Dim ddl_AAge As DropDownList = DirectCast(rw.FindControl("ddl_Age"), DropDownList)
                Dim txtAFFno As TextBox = DirectCast(rw.FindControl("ID_AFreqFlyerNO"), TextBox)
                Dim txtApassno As TextBox = DirectCast(rw.FindControl("ID_ApassNo"), TextBox)
                Dim txtAppexp As TextBox = DirectCast(rw.FindControl("ID_APPEXP"), TextBox)
                Dim ID_AVisaDet As TextBox = DirectCast(rw.FindControl("ID_AVisaDet"), TextBox)
                InsertPaxDetail(PID, ddl_ATitle.SelectedValue.ToString, txtAFirstName.Text.Trim, txtALastName.Text.Trim, ddl_AAge.SelectedValue.ToString(), AgentID, "ADT", txtAFFno.Text.Trim, txtApassno.Text.Trim, txtAppexp.Text.Trim, ID_AVisaDet.Text.Trim)
            Next
            If lbl_Child.Text > 0 Then
                For Each rw As RepeaterItem In Repeater_Child.Items
                    Dim ddl_CTitle As DropDownList = DirectCast(rw.FindControl("ddl_CTitle"), DropDownList)
                    Dim txtCFirstName As TextBox = DirectCast(rw.FindControl("txtCFirstName"), TextBox)
                    Dim txtCLastName As TextBox = DirectCast(rw.FindControl("txtCLastName"), TextBox)
                    Dim ddl_CAge As DropDownList = DirectCast(rw.FindControl("ddl_AgeChild"), DropDownList)
                    Dim txtCFFno As TextBox = DirectCast(rw.FindControl("ID_CFreqFlyerNO"), TextBox)
                    Dim txtCpassno As TextBox = DirectCast(rw.FindControl("ID_CpassNo"), TextBox)
                    Dim txtCppexp As TextBox = DirectCast(rw.FindControl("ID_CPPEXP"), TextBox)
                    Dim ID_CVisaDet As TextBox = DirectCast(rw.FindControl("ID_CVisaDet"), TextBox)
                    InsertPaxDetail(PID, ddl_CTitle.SelectedValue, txtCFirstName.Text.Trim, txtCLastName.Text.Trim, ddl_CAge.SelectedValue, AgentID, "CHD", txtCFFno.Text.Trim, txtCpassno.Text.Trim, txtCppexp.Text.Trim, ID_CVisaDet.Text.Trim)
                Next
            End If
            If lbl_Infrant.Text > 0 Then

                For Each rw As RepeaterItem In Repeater_Infant.Items
                    Dim ddl_ITitle As DropDownList = DirectCast(rw.FindControl("ddl_ITitle"), DropDownList)
                    Dim txtIFirstName As TextBox = DirectCast(rw.FindControl("txtIFirstName"), TextBox)
                    Dim txtILastName As TextBox = DirectCast(rw.FindControl("txtILastName"), TextBox)
                    Dim ddl_IAge As DropDownList = DirectCast(rw.FindControl("ddl_AgeInfant"), DropDownList)
                    Dim txtIFFno As TextBox = DirectCast(rw.FindControl("ID_IFreqFlyerNO"), TextBox)
                    Dim txtIpassno As TextBox = DirectCast(rw.FindControl("ID_IpassNo"), TextBox)
                    Dim txtIppexp As TextBox = DirectCast(rw.FindControl("ID_IPPEXP"), TextBox)
                    Dim ID_IVisaDet As TextBox = DirectCast(rw.FindControl("ID_IVisaDet"), TextBox)
                    InsertPaxDetail(PID, ddl_ITitle.SelectedValue, txtIFirstName.Text.Trim, txtILastName.Text.Trim, ddl_IAge.SelectedValue, AgentID, "INF", txtIFFno.Text.Trim, txtIpassno.Text.Trim, txtIppexp.Text.Trim, ID_IVisaDet.Text.Trim)
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function InsertProxyPaxDetail(ByVal ProxyID As Integer, ByVal Title As String, ByVal FirstName As String, ByVal LastName As String, ByVal Age As String, ByVal AgentID As String, ByVal PaxType As String, ByVal FFNO As String, ByVal PASSNO As String, ByVal PPEXP As String, ByVal VISADET As String) As Integer
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable

        If (FFNO = "FreqFlyerNO") Then
            FFNO = ""
        End If
        If (PASSNO = "PassportNo") Then
            PASSNO = ""
        End If
        If (PPEXP = "PPExp") Then
            PPEXP = ""
        End If
        If (VISADET = "VisaDet") Then
            VISADET = ""
        End If

        paramHashtable.Clear()
        paramHashtable.Add("@ProxyID", ProxyID)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@FirstName", FirstName)
        paramHashtable.Add("@LastName", LastName)
        paramHashtable.Add("@Age", Age)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@PaxType", PaxType)
        paramHashtable.Add("@FFNO", FFNO)
        paramHashtable.Add("@PASSNO", PASSNO)
        paramHashtable.Add("@PPEXP", PPEXP)
        paramHashtable.Add("@VISADET", VISADET)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertProxyPax_NM", 1)
    End Function
End Class