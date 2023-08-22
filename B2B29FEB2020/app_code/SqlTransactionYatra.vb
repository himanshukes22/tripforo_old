Imports Microsoft.VisualBasic

Public Class SqlTransactionYatra
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Public Function InsertYatra_MIRHEADER(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId) '@oid'
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "MH", 1)

    End Function

    Public Function InsertYatra_PAX(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "PX", 1)

    End Function
    Public Function InsertYatra_SEGMENT(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SEG", 1)
    End Function

    Public Function InsertYatra_FARE(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "FX", 1)
    End Function
    Public Function InsertYatra_DIFTLINES(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "DFL", 1)
    End Function
    Public Function InsertYatra_RESUFARE(ByVal OrderId As String, ByVal PnrNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@oid", OrderId)
        paramHashtable.Add("@PNR", PnrNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "RIS_FX", 1)
    End Function
End Class
