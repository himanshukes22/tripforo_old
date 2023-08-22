Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Public Class InFlowDetails
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private cmd As SqlCommand
    Dim ds As New DataSet
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    Public Function GetAgency() As DataSet
        adap = New SqlDataAdapter("GetAgency", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetUploadedDetails(ByVal fromdate As String, ByVal todate As String) As DataSet
        adap = New SqlDataAdapter("select trans_report.user_id,trans_report.ag_name,(trans_report.credit) as amount,trans_report.booking_date,trans_report.rm from trans_report where (trans_report.booking_date BETWEEN CONVERT(datetime, '" & fromdate & "') AND CONVERT(datetime, '" & todate & "')) and trans_report.pnr_locator is null and trans_report.credit!='Null' order by trans_report.booking_date asc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetUploadedDetailsWithAgency(ByVal fromdate As String, ByVal todate As String, ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("select trans_report.user_id,trans_report.ag_name,(trans_report.credit) as amount,trans_report.booking_date,trans_report.rm from trans_report where (trans_report.booking_date BETWEEN CONVERT(datetime, '" & fromdate & "') AND CONVERT(datetime, '" & todate & "')) and trans_report.pnr_locator is null and trans_report.user_id='" & userid & "' and trans_report.credit!='Null' order by trans_report.booking_date asc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds

    End Function
    Public Function GetDeductedDetails(ByVal fromdate As String, ByVal todate As String) As DataSet
        adap = New SqlDataAdapter("select trans_report.user_id,trans_report.ag_name,(trans_report.debit) as amount,trans_report.booking_date,trans_report.rm from trans_report where convert(varchar(10),trans_report.booking_date,105)>='" & fromdate & "' and convert(varchar(10),trans_report.booking_date,105)<='" & todate & "' and trans_report.pnr_locator is null and trans_report.debit!='Null' order by trans_report.booking_date asc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds

    End Function
    Public Function GetDeductedDetailsWithAgency(ByVal fromdate As String, ByVal todate As String, ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("select trans_report.user_id,trans_report.ag_name,(trans_report.debit) as amount,trans_report.booking_date,trans_report.rm from trans_report where convert(varchar(10),trans_report.booking_date,105)>='" & fromdate & "' and convert(varchar(10),trans_report.booking_date,105)<='" & todate & "' and trans_report.pnr_locator is null and trans_report.user_id='" & userid & "' and trans_report.debit!='Null' order by trans_report.booking_date asc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds

    End Function
    Public Function GetCashInflow(ByVal agencyname As String, ByVal type As String) As DataSet
        adap = New SqlDataAdapter("GetCashDetailsAgency", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@agencyname", agencyname)
        adap.SelectCommand.Parameters.AddWithValue("@flowtype", type)
        adap.Fill(ds)
        Return ds
    End Function
    Public Function Update(ByVal type As String, ByVal remark As String, ByVal YtrRcptNo As String, ByVal id As String, ByVal flowtype As String, ByVal counter As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateCashInFlow", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@type", type)
        cmd.Parameters.AddWithValue("@remark", remark)
        cmd.Parameters.AddWithValue("@YtrRcptNo", YtrRcptNo)
        cmd.Parameters.AddWithValue("@accid", id)
        cmd.Parameters.AddWithValue("@flowtype", flowtype)
        cmd.Parameters.AddWithValue("@counter", counter)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function GetCashInflowDetails(ByVal fdate As String, ByVal tdate As String, ByVal agency As String, ByVal uploadtype As String, ByVal flowtype As String, ByVal id As String, ByVal usertype As String, ByVal searchtype As String) As DataSet
        adap = New SqlDataAdapter("NewCashInflowDetails", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@Fromdate", fdate)
        adap.SelectCommand.Parameters.AddWithValue("@Todate", tdate)
        adap.SelectCommand.Parameters.AddWithValue("@agencyname", agency)
        adap.SelectCommand.Parameters.AddWithValue("@uploadtype", uploadtype)
        adap.SelectCommand.Parameters.AddWithValue("@flowtype", flowtype)
        adap.SelectCommand.Parameters.AddWithValue("@accid", id)
        adap.SelectCommand.Parameters.AddWithValue("@usertype", usertype)

        adap.SelectCommand.Parameters.AddWithValue("@searchtype", searchtype)
        adap.Fill(ds)
        Return ds

    End Function
End Class