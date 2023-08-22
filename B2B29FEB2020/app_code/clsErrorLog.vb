Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
'<Serializable()> _
Public Class clsErrorLog
    Public Shared Function LogInfo(ByVal ex As Exception) As Integer

        ITZERRORLOG.ExecptionLogger.FileHandling("Error_log", "Error_001", ex, "Flight")
        ''Dim con As SqlConnection
        ''Dim cmd As SqlCommand
        ''Dim Temp As Integer = 0
        ''Try
        ''    con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        ''    Dim trace As New System.Diagnostics.StackTrace(ex, True)


        ''    Dim linenumber As Integer = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
        ''    Dim ErrorMsg As String = ex.Message
        ''    Dim fileNames As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
        ''    con.Open()
        ''    cmd = New SqlCommand("InsertErrorLog", con)
        ''    cmd.CommandType = CommandType.StoredProcedure
        ''    cmd.Parameters.AddWithValue("@PageName", fileNames)
        ''    cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMsg)
        ''    cmd.Parameters.AddWithValue("@LineNumber", linenumber)
        ''    Temp = cmd.ExecuteNonQuery()
        ''Catch ex1 As Exception
        ''Finally
        ''    con.Close()
        ''End Try
        Return 0
    End Function
End Class
