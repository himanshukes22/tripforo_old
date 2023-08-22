Imports Microsoft.VisualBasic
Imports System.Data

Public Class CCPaymentGateway
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Public Function CheckAmountCreditCard(ByVal TrackId As String, ByVal Amount As Integer) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", TrackId)
        paramHashtable.Add("@Amount", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckAmountCreditCard", 2)
    End Function
    Public Function UpdateCreditCardDetails(ByVal TrackId As String, ByVal PaymentId As String, ByVal Status As String, ByVal ResMsg As String, ByVal ResCode As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", TrackId)
        paramHashtable.Add("@PaymentId", PaymentId)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@ResponseMesg", ResMsg)
        paramHashtable.Add("@ResponseCode", ResCode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateCreditCardDetails", 1)
    End Function
    Public Function GetAgentIdByCCTrackId(ByVal TrackId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", TrackId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetAgentIdByCCTrackId", 3)
    End Function
    Public Function UpdateCreditCardErrorDetails(ByVal TrackId As String, ByVal ErrorText As String, ByVal PaymentId As String, ByVal RespCode As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@TrackId", TrackId)
        paramHashtable.Add("@ErrorText", ErrorText)
        paramHashtable.Add("@PaymentId", PaymentId)
        paramHashtable.Add("@RespCode", RespCode)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateCreditCardErrorDetails", 1)
    End Function
    Public Function InsertCardDetails(ByVal Name As String, ByVal TrackId As String, ByVal PaymentGateway As String, ByVal Email As String, ByVal Mobile As String, ByVal Address As String, ByVal Amount As Double, ByVal AgentId As String, ByVal AgencyName As String, ByVal Ip As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Name", Name)
        paramHashtable.Add("@TrackId", TrackId)
        paramHashtable.Add("@PaymentGateway", PaymentGateway)
        paramHashtable.Add("@Email", Email)
        paramHashtable.Add("@Mobile", Mobile)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@Amount", Amount)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@Status", "Request")
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@Ip", Ip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertCreditCardDetails", 1)
    End Function
    Public Function CalculateCCPercentage(ByVal Amount As Decimal) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Amount", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CalculateCCPercentage", 2)
    End Function
    Public Function CHECK_UPLOAD_PAYMENT(ByVal AgentId As String, ByVal Status As String, ByVal Amount As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Amount", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CHECK_UPLOAD_PAYMENT", 3)
    End Function
    Public Function GetPgCrd(ByVal Pvd As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Pvd", Pvd)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Sp_Get_PgCredentials", 3)
    End Function

    Function PaymentGatewayReq(p1 As Object, Tid As String, p3 As Object, p4 As Object, p5 As String, netFare As Double, netFare1 As Double, p8 As String, p9 As String, p10 As String, p11 As String, p12 As String, p13 As String, p14 As String, p15 As String, ipAddress As String, p17 As String, p18 As String) As String
        Throw New NotImplementedException
    End Function

    Function GetPgTransChargesByMode(paymode As String, p2 As String) As DataTable
        Throw New NotImplementedException
    End Function

End Class
