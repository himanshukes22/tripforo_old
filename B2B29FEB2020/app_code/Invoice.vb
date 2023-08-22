Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Web.UI
''' <summary>
''' Summary description for Invoice
''' </summary>
Public Class Invoice
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private ds As DataSet
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    'public DataSet Ledger()
    '{
    '    DataSet ds = new DataSet();
    '    adap = new SqlDataAdapter("SELECT * from Trans_Report where user_id='MML1'  order by booking_date desc", con);
    '    adap.Fill(ds);
    '    return ds;


    '}
    Public Function InvoiceLedger() As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT * from Trans_Report where user_id='MML1'  AND (pnr_status = 'Ticketed') order by booking_date desc", con)
        adap.Fill(ds)
        Return ds


    End Function
    Public Function InvoiceDetail(ByVal PNR As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SelectInvoiceDetail", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@PNR", PNR)
        adap.Fill(dt)
        Return dt


    End Function
    Public Function InvoiceDetailTrans(ByVal PNR As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select * from Trans_Report where pnr_locator='" & PNR & "'", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function InvoiceDetailTrans1(ByVal From As String, ByVal [To] As String, ByVal ID As String, ByVal usertype As String) As DataSet

        Dim ds As New DataSet()
        If usertype = "ADMIN" Or usertype = "ACC" Then
            adap = New SqlDataAdapter("SELECT * from Trans_Report where pnr_status='Ticketed' and (booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')) order by counter desc ", con)
        ElseIf usertype = "AGENT" Then
            adap = New SqlDataAdapter("SELECT * from Trans_Report where pnr_status='Ticketed' and user_id='" & ID & "' and (booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')) order by counter desc ", con)

        ElseIf usertype = "EXEC" Then
        ElseIf usertype = "SALES" Then


        End If

        adap.Fill(ds)
        Return ds



    End Function
    Public Function InvoiceDetailTrans2(ByVal PNR As String, ByVal ID As String, ByVal usertype As String) As DataSet

        Dim ds As New DataSet()
        If usertype = "ADMIN" Or usertype = "ACC" Then
            adap = New SqlDataAdapter("SELECT * from Trans_Report where pnr_status='Ticketed' and pnr_locator='" & PNR & "' order by counter desc ", con)
        ElseIf usertype = "AGENT" Then
            adap = New SqlDataAdapter("SELECT * from Trans_Report where pnr_status='Ticketed' and user_id='" & ID & "' and  pnr_locator='" & PNR & "'  order by counter desc ", con)

        ElseIf usertype = "EXEC" Then
        ElseIf usertype = "SALES" Then


        End If

        adap.Fill(ds)
        Return ds



    End Function

    Public Function AgentAddress(ByVal PNR As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SELECT TOP (1) New_Regs.Agency_Name, New_Regs.Address, (New_Regs.City +', '+ New_Regs.zipcode +', '+ New_Regs.State) as Add1,New_Regs.Country,Trans_Report.pnr_locator FROM   Trans_Report INNER JOIN New_Regs ON Trans_Report.Ag_Name = New_Regs.Agency_Name WHERE(Trans_Report.pnr_locator = '" & PNR & "')", con)
        adap.Fill(dt)
        Return dt
    End Function



End Class
