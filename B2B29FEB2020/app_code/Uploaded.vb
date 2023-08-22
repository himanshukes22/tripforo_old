Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

''' <summary>
''' Summary description for Uploaded
''' </summary>
Public Class Uploaded
    Private con As SqlConnection
    Private cmd As SqlCommand
    Private adap As SqlDataAdapter
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
   
    Public Function GetAgency(ByVal id As String) As DataSet
        adap = New SqlDataAdapter("select agency_name,user_id from dbo.New_Regs where user_id='" + id + "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function InsertDeposit(ByVal agencyname As String, ByVal agencyid As String, ByVal amount As Integer, ByVal modeofpayment As String, ByVal bankname As String, ByVal chequeno As String, _
     ByVal chequedate As String, ByVal tranid As String, ByVal areacode As String, ByVal city As String, ByVal remark As String, ByVal status As String) As Integer
        con.Open()
        cmd = New SqlCommand("InsertDepositDetails", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@agencyname", agencyname)
        cmd.Parameters.AddWithValue("@userid", agencyid)
        cmd.Parameters.AddWithValue("@amount", amount)
        cmd.Parameters.AddWithValue("@modeofpayment", modeofpayment)
        cmd.Parameters.AddWithValue("@bankname", bankname)
        cmd.Parameters.AddWithValue("@ChequeNo", chequeno)
        cmd.Parameters.AddWithValue("@ChequeDate", chequedate)
        cmd.Parameters.AddWithValue("@TransactionID", tranid)
        cmd.Parameters.AddWithValue("@BankAreaCode", areacode)
        cmd.Parameters.AddWithValue("@DepositCity", city)
        cmd.Parameters.AddWithValue("@Remark", remark)
        cmd.Parameters.AddWithValue("@status", status)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function GetDepositDetails(ByVal currentdate As String, Optional ByVal userid As String = "") As DataSet
        If userid = "" Then
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where convert(varchar(10),Date,101)='" + currentdate + "'", con)
        Else
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where convert(varchar(10),Date,101)='" + currentdate + "' and AgencyID='" & userid & "'", con)
        End If
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetDepositDetailsWithdate(ByVal fromdate As String, ByVal todate As String, Optional ByVal userid As String = "") As DataSet
        If userid = "" Then
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where convert(varchar(10),Date,105)<='" + fromdate + "' and convert(varchar(10),Date,105)>='" + todate + "'", con)
        Else
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where convert(varchar(10),Date,105)<='" + fromdate + "' and convert(varchar(10),Date,105)>='" + todate + "' and AgencyID='" & userid & "'", con)
        End If
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateAcceptDeposit(ByVal id As String, ByVal accid As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.DepositDetails set Status='process',AccountID='" + accid + "' where AgencyID='" + id + "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateRejectDeposit(ByVal id As String, ByVal accid As String, ByVal remark As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.DepositDetails set Status='Rejected',AccountID='" + accid + "',RemarkByAccounts='" + remark + "' where AgencyID='" + id + "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateProcessDeposit(ByVal id As String, ByVal accid As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.DepositDetails set Status='Uploaded',AccountID='" + accid + "' where AgencyID='" + id + "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function GetDepositProcessDetails(ByVal status As String, Optional ByVal accountsid As String = "") As DataSet
        If accountsid = "" Then
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where Status='" & status & "'", con)
        Else
            adap = New SqlDataAdapter("select Date,AgencyName,AgencyID,Amount,ModeOfPayment,BankName,ChequeNo,ChequeDate,TransactionID,BankAreaCode,DepositCity,Remark,Status,RemarkByAccounts,AccountID from dbo.DepositDetails where Status='" & status & "' and AccountID='" & accountsid & "'", con)
        End If
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function

End Class
