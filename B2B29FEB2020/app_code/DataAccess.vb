Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common

Public Class DataAccess
    Private dbCmd As DbCommand
    Private sqlDB As SqlDatabase
    Private SqlCmd As New SqlCommand
    Private connectionStr As String

    Sub New(ByVal conStr As String)
        connectionStr = conStr
    End Sub
    Private Sub MakeConnection()
        sqlDB = New SqlDatabase(connectionStr)
    End Sub

    Public Function ExecuteData(Of T)(ByVal ht As Hashtable, ByVal IsProc As Boolean, ByVal commandText As String, ByVal tparams As Integer) As T
        Dim obj As Object = Nothing
        Try
            MakeConnection()
            If IsProc = True Then
                SqlCmd.CommandType = CommandType.StoredProcedure
                SqlCmd.CommandText = commandText
            Else
                SqlCmd.CommandText = commandText
            End If
            SetCommand(SqlCmd)
            AttachPrameter(ht, sqlDB, dbCmd)
            obj = ReturnValue(Of T)(tparams, sqlDB)
        Catch ex As Exception
            ExcpLogInfo(ex)
        End Try
        Return DirectCast(DirectCast(obj, Object), T)
    End Function


    Private Function ReturnValue(Of T)(ByVal t1 As Integer, ByVal sqlDB As SqlDatabase) As T
        Try
            Dim obj As Object = Nothing
            If t1 = 1 Then
                obj = sqlDB.ExecuteNonQuery(dbCmd)
            ElseIf t1 = 2 Then
                obj = sqlDB.ExecuteScalar(dbCmd)
            ElseIf t1 = 3 Then
                obj = sqlDB.ExecuteDataSet(dbCmd)
            ElseIf t1 = 4 Then
                obj = sqlDB.ExecuteReader(dbCmd)
                'obj = DirectCast(obj, Microsoft.Practices.EnterpriseLibrary.Data.RefCountingDataReader).InnerReader
            End If
            Return DirectCast(DirectCast(obj, Object), T)
        Catch ex As Exception
            ExcpLogInfo(ex)
        End Try

    End Function

    'public int ExecuteNonQuery(Hashtable ht, SqlCommand sqlCmd)
    '{
    '    MakeConnection();
    '    SetCommand(sqlCmd);
    '    AttachPrameter(ht, sqlDB, dbCmd);

    '    object retval = sqlDB.ExecuteNonQuery(dbCmd);

    '    return Convert.ToInt32(retval);
    '}

    'public int ExecuteScalor(Hashtable ht, SqlCommand sqlCmd)
    '{
    '    MakeConnection();
    '    SetCommand(sqlCmd);
    '    AttachPrameter(ht, sqlDB, dbCmd);

    '    object retval = sqlDB.ExecuteScalar(dbCmd);

    '    return Convert.ToInt32(retval);
    '}

    Private Sub SetCommand(ByVal sqlCmd As SqlCommand)
        If sqlCmd.CommandType = CommandType.StoredProcedure Then
            dbCmd = sqlDB.GetStoredProcCommand(sqlCmd.CommandText)
        Else
            dbCmd = sqlDB.GetSqlStringCommand(sqlCmd.CommandText)
        End If
    End Sub

    Private Sub AttachPrameter(ByVal htParams As Hashtable, ByVal sqlDB As SqlDatabase, ByVal db As DbCommand)
        Try
            If htParams IsNot Nothing Then
                For Each o As Object In htParams.Keys
                    Dim paramName As String = o.ToString()
                    Dim paramValue As String '= htParams(o).ToString()
                    Dim parameter As DbParameter = Nothing
                    parameter = db.CreateParameter()
                    parameter.ParameterName = paramName
                    parameter.Value = htParams(o) 'paramValue
                    db.Parameters.Add(parameter)
                Next
            End If
        Catch ex As Exception
            ExcpLogInfo(ex)
        End Try

    End Sub

    Public Function ExcpLogInfo(ByVal ex As Exception) As Integer
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim Temp As Integer = 0
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
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
        Return Temp
    End Function

End Class
