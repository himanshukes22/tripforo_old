Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Public Class IntDiscount
    Private con As SqlConnection
    Private cmd As SqlCommand
    Private adap As SqlDataAdapter
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    
    Public Function PLB(ByVal type As String, ByVal airline As String, ByVal airlinecode As String, ByVal rbd As String, ByVal Sector As String, _
     ByVal plbbasic As String, ByVal plbyqb As String, ByVal StartDate As String, ByVal EndDate As String, ByVal Remark As String) As Integer
        Try
            con.Open()
            cmd = New SqlCommand("InsertPLB", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@GroupType", type)
            cmd.Parameters.AddWithValue("@Airline", airline)
            cmd.Parameters.AddWithValue("@Airlinecode", airlinecode)
            cmd.Parameters.AddWithValue("@rbd", rbd)
            cmd.Parameters.AddWithValue("@Sector", Sector)

            cmd.Parameters.AddWithValue("@plbBasic", plbbasic)
            cmd.Parameters.AddWithValue("@plbbasicyq", plbyqb)
            cmd.Parameters.AddWithValue("@StartDate", StartDate)
            cmd.Parameters.AddWithValue("@EndDate", EndDate)
            cmd.Parameters.AddWithValue("@Remark", Remark)
            Dim i As Integer = cmd.ExecuteNonQuery()
            con.Close()
            Return i

        Catch ex As Exception
        End Try
        Return 0
    End Function

    Public Function Commission(ByVal type As String, ByVal airline As String, ByVal airlinecode As String, ByVal Cbasic As String, ByVal Cyq As String, ByVal Cbyq As String) As Integer
        Try
            con.Open()
            cmd = New SqlCommand("InsertCommission", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@GroupType", type)
            cmd.Parameters.AddWithValue("@Airline", airline)
            cmd.Parameters.AddWithValue("@Airlinecode", airlinecode)
            cmd.Parameters.AddWithValue("@CommissionBasic", Cbasic)
            cmd.Parameters.AddWithValue("@Commissionyq", Cyq)
            cmd.Parameters.AddWithValue("@Commissionbasicyq", Cbyq)
            Dim i As Integer = cmd.ExecuteNonQuery()
            con.Close()
            Return i

        Catch ex As Exception
        End Try
        Return 0
    End Function

    Public Function GetPLB(ByVal type As String) As DataTable
        Dim msg As String
        Dim dt As New DataTable()
        Try

            adap = New SqlDataAdapter("GetPLBDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@type", type)
            adap.Fill(dt)

        Catch ex As Exception
            msg = ex.Message
        End Try

        Return dt
    End Function
    Public Function GetCommision(ByVal type As String) As DataTable
        Dim msg As String
        Dim dt As New DataTable()
        Try

            adap = New SqlDataAdapter("GetCommDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@type", type)
            adap.Fill(dt)

        Catch ex As Exception
            msg = ex.Message
        End Try

        Return dt
    End Function

    Public Function DeleteCommissionDetails(ByVal code As String, ByVal type As String, ByVal counter As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("delete from Commission where airlinecode='" & code & "' and grouptype='" & type & "'and Counter='" & counter & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function DeletePLBDetails(ByVal code As String, ByVal type As String, ByVal counter As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("delete from IntPLB where airlinecode='" & code & "' and grouptype='" & type & "' and Counter='" & counter & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function UpdateCommissionDetails(ByVal airlinecode As String, ByVal grouptype As String, ByVal Cobasic As String, ByVal Coyq As String, ByVal Cobyq As String, ByVal counter As Integer) As Integer
        Try
            con.Open()
            cmd = New SqlCommand("UpdateCommission", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@airlinecode", airlinecode)
            cmd.Parameters.AddWithValue("@grouptype", grouptype)
            cmd.Parameters.AddWithValue("@Cbasic", Cobasic)
            cmd.Parameters.AddWithValue("@Cyq", Coyq)
            cmd.Parameters.AddWithValue("@Cbyq", Cobyq)
            cmd.Parameters.AddWithValue("@counter", counter)
            Dim i As Integer = cmd.ExecuteNonQuery()
            con.Close()
            Return i

        Catch ex As Exception
        End Try

        Return 0

    End Function


    'Public Function UpdatePLBDetails(ByVal grouptype As String, ByVal Aircode As String, ByVal rbd As String, ByVal Sector As String, ByVal Plbasic As String, _
    ' ByVal Plbyq As String, ByVal counter As Integer) As Integer
    '    Dim msg As String
    '    Try
    '        con.Open()
    '        cmd = New SqlCommand("UpdatePLB", con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("@grouptype", grouptype)
    '        cmd.Parameters.AddWithValue("@airlinecode", Aircode)
    '        cmd.Parameters.AddWithValue("@rbd", rbd)
    '        cmd.Parameters.AddWithValue("@Sector", Sector)
    '        cmd.Parameters.AddWithValue("@Pbasic", Plbasic)
    '        cmd.Parameters.AddWithValue("@Pbyq", Plbyq)
    '        cmd.Parameters.AddWithValue("@counter", counter)
    '        Dim i As Integer = cmd.ExecuteNonQuery()
    '        con.Close()
    '        Return i
    '    Catch ex As Exception

    '        msg = ex.Message
    '    End Try

    '    Return 0
    'End Function

    Public Function UpdatePLBDetails(ByVal grouptype As String, ByVal Aircode As String, ByVal rbd As String, ByVal Sector As String, ByVal Plbasic As String, _
     ByVal Plbyq As String, ByVal counter As Integer, ByVal StartDate As String, ByVal EndDate As String, ByVal Remark As String) As Integer
        Dim msg As String
        Try
            con.Open()
            cmd = New SqlCommand("UpdatePLB", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@grouptype", grouptype)
            cmd.Parameters.AddWithValue("@airlinecode", Aircode)
            cmd.Parameters.AddWithValue("@rbd", rbd)
            cmd.Parameters.AddWithValue("@Sector", Sector)
            cmd.Parameters.AddWithValue("@Pbasic", Plbasic)
            cmd.Parameters.AddWithValue("@Pbyq", Plbyq)
            cmd.Parameters.AddWithValue("@counter", counter)
            cmd.Parameters.AddWithValue("@StartDate", StartDate)
            cmd.Parameters.AddWithValue("@EndDate", EndDate)
            cmd.Parameters.AddWithValue("@Remark", Remark)
            Dim i As Integer = cmd.ExecuteNonQuery()
            con.Close()
            Return i
        Catch ex As Exception

            msg = ex.Message
        End Try

        Return 0
    End Function

    Public Function GetAirline() As DataSet
        adap = New SqlDataAdapter("select al_code,al_name from dbo.AirLineNames", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function

End Class

