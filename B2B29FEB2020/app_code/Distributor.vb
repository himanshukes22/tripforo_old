Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Distributor

    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    'Dim ds As New DataSet
    'Dim dt As New DataTable
    'Dim sql As New SqlTransaction

    'Private I As New Invoice()
    'Dim ST As New SqlTransaction()
    'Dim Conn As New SqlConnection
    Public Function GetDepositeDetailsById(ByVal AgentId As String, ByVal Status As String, Optional ByVal RequestId As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AGENTID", AgentId)
        paramHashtable.Add("@STATUS", Status)
        paramHashtable.Add("@REQUESTID", RequestId)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETDEPOSITEDETAILSBYID", 3)
    End Function

    Public Function DepositStatusDetails(ByVal Status As String, ByVal DistrId As String, Optional ByVal AccountID As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@DistrId", DistrId)
        paramHashtable.Add("@AccountID", AccountID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GetDepositStatusDetailsDistr", 3)
    End Function
    Public Function AddCrdLimit(ByVal uid As String, ByVal amount As Double, ByVal DistrId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", uid)
        paramHashtable.Add("@DistrId", DistrId)
        paramHashtable.Add("@Amount", amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_DISTR_AddToLimit", 3)
    End Function
    Public Function UpdateCrdLimit(ByVal uid As String, ByVal netFare As Double, ByVal DistrId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", uid)
        paramHashtable.Add("@netFare", netFare)
        paramHashtable.Add("@DistrId", DistrId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_DISTR_UpdateCrdLimit", 3)
    End Function
    Public Function GetDepositeOffice(ByVal AgentId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AGENTID", AgentId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_DEPOSITEOFFICE", 3)
    End Function
    Public Function CheckDistrBalance(ByVal AgentId As String, ByVal Amount As Decimal) As Decimal
        paramHashtable.Clear()
        paramHashtable.Add("@AGENTID", AgentId)
        paramHashtable.Add("@TAAMOUNT", Amount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CHECKDISTRBALANCE", 2)
    End Function
    'NEW
    Public Function GetDistributorSale(ByVal FROMDATE As String, ByVal TODATE As String, ByVal DISTRID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@FROMDATE", FROMDATE)
        paramHashtable.Add("@TODATE", TODATE)
        paramHashtable.Add("@DISTRID", DISTRID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_TOTALSALEREPORTDISTR", 3)
    End Function
    'NEW
    Public Function GetDistinctDistrId() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETDISTINCTDISTRID", 3)
    End Function
    'NEW
    Public Function GetHoldPnrReport(ByVal LOGINID As String, ByVal USERTYPE As String, ByVal TRIP As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@LOGINID", LOGINID)
        paramHashtable.Add("@USERTYPE", USERTYPE)
        paramHashtable.Add("@TRIP", TRIP)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETHOLDPNRREPORT", 3)
    End Function
#Region "Pawan"
    Public Function GetHoldPnrReportNew(ByVal LOGINID As String, ByVal USERTYPE As String, ByVal TRIP As String, ByVal fDt As String, ByVal toDt As String _
                                      , ByVal ordid As String, ByVal pnr As String, ByVal vc As String, ByVal psgrNm As String, ByVal tktno As String _
                                      , ByVal agtid As String, ByVal execid As String, ByVal distrid As String, ByVal Action As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@LOGINID", LOGINID)
        paramHashtable.Add("@USERTYPE", USERTYPE)
        paramHashtable.Add("@TRIP", TRIP)
        paramHashtable.Add("@FormDate", fDt.Trim())
        paramHashtable.Add("@ToDate", toDt.Trim())
        paramHashtable.Add("@OderId", ordid.Trim())
        paramHashtable.Add("@PNR", pnr.Trim())
        paramHashtable.Add("@Airline", vc.Trim())
        paramHashtable.Add("@PaxName", psgrNm.Trim())
        paramHashtable.Add("@TicketNo", tktno.Trim())
        paramHashtable.Add("@AgentId", tktno.Trim())
        paramHashtable.Add("@execid", execid.Trim())
        paramHashtable.Add("@distrid", distrid.Trim())
        paramHashtable.Add("@Action", Action.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETHOLDPNRREPORT", 3)
    End Function

    Public Function GetConfigureMails() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GET_CONFIGURED_MAILS", 3)
    End Function
    Public Function InsertUpdateConfigureMail(ByVal sno As Integer, ByVal moduleMail As String, ByVal mail As String, ByVal creatby As String, ByVal isAct As String) As Integer

        paramHashtable.Clear()
        paramHashtable.Add("@MODULE", moduleMail.Trim())
        paramHashtable.Add("@TOMAIL", mail.Trim())
        paramHashtable.Add("@ISACTIVE", isAct.Trim())
        paramHashtable.Add("@LOGIN", creatby.Trim())
        paramHashtable.Add("@SNO", sno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_INSERTUPDATE_MAILCONFIGURE", 1)
    End Function

    Public Function UpdateDeleteConfigureMail(ByVal sno As Integer, ByVal moduleMail As String, ByVal mail As String, ByVal creatby As String, ByVal isAct As String, ByVal commd As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@MODULE", moduleMail.Trim())
        paramHashtable.Add("@TOMAIL", mail.Trim())
        paramHashtable.Add("@ISACTIVE", isAct.Trim())
        paramHashtable.Add("@LOGIN", creatby.Trim())
        paramHashtable.Add("@SNO", sno)
        paramHashtable.Add("@CMD", commd.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_Update_Delete_MailConfig", 1)
    End Function

    Public Function GetAgentsDetails(ByVal agentID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AGTID", agentID.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GET_AGENTSDETAILS", 3)
    End Function
    Public Function GetCancellationDetails(ByVal CounterNo As Integer) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Cntr", CounterNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_GET_REFUNDDETAILS", 3)
    End Function
    ''Convert.ToInt32(Request.QueryString("counter"))
#End Region

    'NEW
    Public Function GetZeroSalesReport(ByVal FROMDATE As String, ByVal TODATE As String, ByVal AGENTID As String, ByVal USERTYPE As String, ByVal LOGINID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@FROMDATE", FROMDATE)
        paramHashtable.Add("@TODATE", TODATE)
        paramHashtable.Add("@AGENTID", AGENTID)
        paramHashtable.Add("@LOGINID", LOGINID)
        paramHashtable.Add("@USERTYPE", USERTYPE)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETZEROSALESREPORT", 3)
    End Function


    'NEW
    Public Function GetDistributorCB(ByVal DISTRID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@DISTRID", DISTRID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CASHBACK_DISTRIBUTOR", 3)
    End Function
    'NEW
    Public Function InsertCBDetails(ByVal CBOID As String, ByVal DISTRID As String, ByVal AIRTICKET As Decimal, ByVal AIRSALE As Decimal, ByVal AIRCB As Decimal, ByVal AIRCBDISTR As Decimal, ByVal RAILTICKET As Decimal, ByVal RAILSALE As Decimal, ByVal RAILCB As Decimal, ByVal RAILCBDISTR As Decimal, ByVal CBDATE As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@CBOID", CBOID)
        paramHashtable.Add("@DISTRID", DISTRID)
        paramHashtable.Add("@AIRTICKET", AIRTICKET)
        paramHashtable.Add("@AIRSALE", AIRSALE)
        paramHashtable.Add("@AIRCB", AIRCB)
        paramHashtable.Add("@AIRCBDISTR", AIRCBDISTR)
        paramHashtable.Add("@RAILTICKET", RAILTICKET)
        paramHashtable.Add("@RAILSALE", RAILSALE)
        paramHashtable.Add("@RAILCB", RAILCB)
        paramHashtable.Add("@RAILCBDISTR", RAILCBDISTR)
        paramHashtable.Add("@CBDATE", CBDATE)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERTDISTRCBDETAILS", 1)
    End Function


    Public Function InsertBankDetails(ByVal BANKNAME As String, ByVal BRANCHNAME As String, ByVal AREA As String, ByVal ACCNO As String, ByVal IFSCCODE As String, ByVal DISTRID As String, ByVal OPERATION As String, ByVal COUNTER As Integer) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@BANKNAME", BANKNAME)
        paramHashtable.Add("@BRANCHNAME", BRANCHNAME)
        paramHashtable.Add("@AREA", AREA)
        paramHashtable.Add("@ACCNO", ACCNO)
        paramHashtable.Add("@IFSCCODE", IFSCCODE)
        paramHashtable.Add("@DISTRID", DISTRID)
        paramHashtable.Add("@OPERATION", OPERATION)
        paramHashtable.Add("@COUNTER", COUNTER)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_BANKDETIALS", 1)
    End Function
    Public Function GetDistributorBankDetails(ByVal DISTRID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@STOCKIESTID", DISTRID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_DISTR_GETBANKDETAILSBYSTOCKIEST", 3)

    End Function

    Public Function InsertDepositeBranchName(ByVal BRANCHNAME As String, ByVal DISTRID As String, ByVal OPERATION As String, ByVal Counter As Integer) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OFFICE", BRANCHNAME)
        paramHashtable.Add("@DISTRID", DISTRID)
        paramHashtable.Add("@OPERATION", OPERATION)
        paramHashtable.Add("@Counter", Counter)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_DEPOSITEOFFICE", 1)
    End Function
    Public Function GetDistDepositeBranchName(ByVal DISTRID As String) As DataSet
        Try
            paramHashtable.Clear()
            paramHashtable.Add("@OFFICE", "")
            paramHashtable.Add("@DISTRID", DISTRID)
            paramHashtable.Add("@OPERATION", "SELECT")
            paramHashtable.Add("@Counter", 0)
            Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_DEPOSITEOFFICE", 3)
        Catch ex As Exception

        End Try

    End Function
End Class

