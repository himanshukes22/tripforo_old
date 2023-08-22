Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class SqlTransactionDom
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Public Function insertProxyDetails(ByVal BookingType As String, ByVal TravelType As String, ByVal ProxyFrom As String, ByVal ProxyTo As String, ByVal DepartDate As String, ByVal ReturnDate As String, ByVal DepartTime As String, ByVal ReturnTime As String, ByVal Adult As Integer, ByVal Child As Integer, ByVal Infrant As Integer, ByVal ClassT As String, ByVal Airline As String, ByVal Classes As String, ByVal PaymentMode As String, ByVal Remark As String, ByVal AgentID As String, ByVal Ag_Name As String, ByVal Status As String, ByVal Trip As String, ByVal Ptype As String, ByVal projectId As String, ByVal BookedBy As String,ByVal ExpectedAmount As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@TravelType", TravelType)
        paramHashtable.Add("@ProxyFrom", ProxyFrom)
        paramHashtable.Add("@ProxyTo", ProxyTo)
        paramHashtable.Add("@DepartDate", DepartDate)
        paramHashtable.Add("@ReturnDate", ReturnDate)
        paramHashtable.Add("@DepartTime", DepartTime)
        paramHashtable.Add("@ReturnTime", ReturnTime)
        paramHashtable.Add("@Adult", Adult)
        paramHashtable.Add("@Child", Child)
        paramHashtable.Add("@Infrant", Infrant)
        paramHashtable.Add("@Class", ClassT)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Classes", Classes)
        paramHashtable.Add("@PaymentMode", PaymentMode)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@Ag_Name", Ag_Name)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Ptype", Ptype) 'Added ProxyType Domestic or International
        paramHashtable.Add("@ProjectID", projectId)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@ExpectedAmt", ExpectedAmount)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertProxy", 2)
    End Function
    Public Function InsertProxyPaxDetail(ByVal ProxyID As Integer, ByVal Title As String, ByVal FirstName As String, ByVal LastName As String, ByVal Age As String, ByVal AgentID As String, ByVal PaxType As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@ProxyID", ProxyID)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@FirstName", FirstName)
        paramHashtable.Add("@LastName", LastName)
        paramHashtable.Add("@Age", Age)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@PaxType", PaxType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertProxyPax", 1)
    End Function

    Public Function ProxyDetails(ByVal st As String, Optional ByVal Ptype As String = "", Optional ByVal id As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@st", st)
        paramHashtable.Add("@Ptype", Ptype)
        paramHashtable.Add("@id", id)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ProxyDetail", 3)
    End Function
    Public Function GetSalesB2C() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSalesB2C", 3)
    End Function
    Public Function UpdateProxyDetails(ByVal st As String, ByVal id As String, ByVal ProxyID As String, ByVal Rm As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@st", st)
        paramHashtable.Add("@id", id)
        paramHashtable.Add("@Proxyid", ProxyID)
        paramHashtable.Add("@Remark", Rm)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateProxy", 1)
    End Function
    Public Function AgentCommentProxy(ByVal PID As String, ByVal RM As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Remark", RM)
        paramHashtable.Add("@Proxyid", PID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgentCommentProxy", 3)
    End Function
    Public Function ProxyPaxDetails(ByVal PID As String, ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@ProxyID", PID)
        paramHashtable.Add("@Type", Type)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ProxyPaxDetails", 3)
    End Function
    Public Function GetAgencyDetails(ByVal UserId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetails", 3)
    End Function

    Public Function LedgerWithDetailSingleOrderID(ByVal usertype As String, ByVal LoginID As String, ByVal FormDate As String, ByVal ToDate As String, ByVal AgentId As String, ByVal BookingType As String, ByVal SearchType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginID)
        paramHashtable.Add("@FormDate", FormDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@SearchType", SearchType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "LedgerWithDetailSingleOrderID", 3)
    End Function
    Public Function Servicecharge(ByVal AirCode As String, ByVal SelectType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AirCode", AirCode)
        paramHashtable.Add("@SelectType", SelectType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "ProxyServiceTax", 3)
    End Function
    Public Function InsertProxyPaxInfoIntl(ByVal OrderId As String, ByVal tittle As String, ByVal fname As String, ByVal mname As String, ByVal lname As String, ByVal Paxtype As String, ByVal TktNo As String, ByVal Tri As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@tittle", tittle)
        paramHashtable.Add("@first_name", fname)
        paramHashtable.Add("@middle_name", mname)
        paramHashtable.Add("@last_name", lname)
        paramHashtable.Add("@paxtype", Paxtype)
        paramHashtable.Add("@ticketno", TktNo)
        paramHashtable.Add("@Tri", Tri)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPaxIntl", 1)
    End Function
    Public Function UpdateProxyDate(ByVal Status As String, ByVal ProxyID As String, ByVal SrvChgOneWay As Double, ByVal SrvChgTwoWay As Double, ByVal OrderIdOneWay As String, ByVal OrderIdTwoWay As String, ByVal RBDOneWay As String, ByVal RBDTwoWay As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@ProxyID", ProxyID)
        paramHashtable.Add("@SrvChgOneWay", SrvChgOneWay)
        paramHashtable.Add("@SrvChgTwoWay", SrvChgTwoWay)
        paramHashtable.Add("@OrderIdOneWay", OrderIdOneWay)
        paramHashtable.Add("@OrderIdTwoWay", OrderIdTwoWay)
        paramHashtable.Add("@RBDOneWay", RBDOneWay)
        paramHashtable.Add("@RBDTwoWay", RBDTwoWay)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateProxyDate", 1)
    End Function
    Public Function insertLedgerDetails(ByVal AgentID As String, ByVal AgencyName As String, ByVal InvoiceNo As String, ByVal PnrNo As String, ByVal TicketNo As String, ByVal TicketingCarrier As String, ByVal YatraAccountID As String, ByVal AccountID As String, ByVal ExecutiveID As String, ByVal IPAddress As String, ByVal Debit As Double, ByVal Credit As Double, ByVal Aval_Balance As Double, ByVal BookingType As String, ByVal Remark As String, ByVal PaxId As Integer, Optional ByVal ProjectId As String = Nothing, Optional ByVal BookedBy As String = Nothing, Optional ByVal BillNo As String = Nothing) As Integer ', ) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@InvoiceNo", InvoiceNo)
        paramHashtable.Add("@PnrNo", PnrNo)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@TicketingCarrier", TicketingCarrier)
        paramHashtable.Add("@YatraAccountID", YatraAccountID)

        paramHashtable.Add("@AccountID", AccountID)
        paramHashtable.Add("@ExecutiveID", ExecutiveID)
        paramHashtable.Add("@IPAddress", IPAddress)

        paramHashtable.Add("@Debit", Debit)
        paramHashtable.Add("@Credit", Credit)
        paramHashtable.Add("@Aval_Balance", Aval_Balance)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@ProjectId", ProjectId)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@BillNo", BillNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "usp_insert_LedgerDetails", 1)
    End Function

    Public Function GetCreditNodeDetailsMass(ByVal PnrNO As String, ByVal Trip As String, ByVal Status As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@PnrNO", PnrNO)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Status", Status)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetCreditNodeDetailsMass", 3)
    End Function
    Public Function GetFltFareDtlForLedger(ByVal TrackID As String, ByVal PaxType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Trackid", TrackID)
        paramHashtable.Add("@PaxType", PaxType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetFltFareDtlForLedger", 3)
    End Function
    'Upload Statement
    Public Function GetUploadType() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetUploadType", 3)
    End Function
    Public Function GetCategory(ByVal UType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UType", UType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetCategory", 3)
    End Function
    Public Function GetAgencyByType(ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Type", Type)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetAgencyByType", 3)
    End Function
    Public Function insertUploadLedgerDetails(ByVal AgentID As String, ByVal AgencyName As String, ByVal AccountID As String, ByVal IPAddress As String, ByVal Debit As Double, ByVal Credit As Double, ByVal Aval_Balance As Double, ByVal BookingType As String, ByVal Remark As String, ByVal UploadType As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@AccountID", AccountID)
        paramHashtable.Add("@IPAddress", IPAddress)
        paramHashtable.Add("@Debit", Debit)
        paramHashtable.Add("@Credit", Credit)
        paramHashtable.Add("@Aval_Balance", Aval_Balance)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@UploadType", UploadType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertUploadAmount", 1)
    End Function
    Public Function insertUploadDetails(ByVal AgentID As String, ByVal AgencyName As String, ByVal AccountID As String, ByVal IPAddress As String, ByVal Debit As Double, ByVal Credit As Double, ByVal Remark As String, ByVal UploadType As String, ByVal LastAval_Balance As Double, ByVal CurrentAval_Balance As Double, ByVal YtrRcptNo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@agentid", AgentID)
        paramHashtable.Add("@agencyname", AgencyName)
        paramHashtable.Add("@accid", AccountID)
        paramHashtable.Add("@IPaddress", IPAddress)
        paramHashtable.Add("@debit", Debit)
        paramHashtable.Add("@credit", Credit)

        paramHashtable.Add("@remark", Remark)
        paramHashtable.Add("@Uploadtype", UploadType)
        paramHashtable.Add("@lastavlbal", LastAval_Balance)
        paramHashtable.Add("@curravlbal", CurrentAval_Balance)
        paramHashtable.Add("@YtrRcptNo", YtrRcptNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertUploadDetails", 2)
    End Function
    Public Function GetUploadTypeByType(ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Type", Type)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetUploadTypeByType", 3)
    End Function

    'Agent Panel
    Public Function BankInformation(ByVal AgentId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AgentId", AgentId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "BankInformation", 3)
    End Function
    Public Function GetBranchAccount(ByVal Bank As String, ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Bank", Bank)
        paramHashtable.Add("@Type", Type)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetBranchAccount", 3)
    End Function
    Public Function insertDeposite(ByVal AgencyName As String, ByVal AgencyID As String, ByVal Amount As Double, ByVal ModeOfPayment As String, ByVal BankName As String, ByVal BranchName As String, ByVal AccountNo As String, ByVal ChequeNo As String, ByVal ChequeDate As String, ByVal TransactionID As String, ByVal BankAreaCode As String, ByVal DepositCity As String, ByVal DepositeDate As String, ByVal Remark As String, ByVal Status As String, ByVal UploadType As String, ByVal DepositeOffice As String, ByVal ConcernPerson As String, ByVal RecieptNo As String, ByVal BranchCode As String, ByVal AgentBankName As String, ByVal SalesExecID As String, ByVal AgentType As String, ByVal OTPRefNo As String, ByVal LoginByOTP As String, ByVal OTPId As String, ByVal file_pan As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@AgencyID", AgencyID)
        paramHashtable.Add("@Amount", Amount)

        paramHashtable.Add("@ModeOfPayment", ModeOfPayment)
        paramHashtable.Add("@BankName", BankName)
        paramHashtable.Add("@BranchName", BranchName)

        paramHashtable.Add("@AccountNo", AccountNo)
        paramHashtable.Add("@ChequeNo", ChequeNo)
        paramHashtable.Add("@ChequeDate", ChequeDate)

        paramHashtable.Add("@TransactionID", TransactionID)
        paramHashtable.Add("@BankAreaCode", BankAreaCode)
        paramHashtable.Add("@DepositCity", DepositCity)

        paramHashtable.Add("@DepositeDate", DepositeDate)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@Status", Status)

        paramHashtable.Add("@UploadType", UploadType)
        paramHashtable.Add("@DepositeOffice", DepositeOffice)
        paramHashtable.Add("@ConcernPerson", ConcernPerson)

        paramHashtable.Add("@RecieptNo", RecieptNo)
        paramHashtable.Add("@BranchCode", BranchCode)
        paramHashtable.Add("@AgentBankName", AgentBankName)

        paramHashtable.Add("@SalesExecID", SalesExecID)
        paramHashtable.Add("@AgentType", AgentType)

        paramHashtable.Add("@OTPRefNo", OTPRefNo)
        paramHashtable.Add("@LoginByOTP", LoginByOTP)
        paramHashtable.Add("@OTPId", OTPId)
        paramHashtable.Add("@FileUpName", file_pan)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertDepositDetails", 1)
    End Function
    Public Function GetDepositDetails(ByVal FromDate As String, ByVal ToDate As String, ByVal UserType As String, ByVal LoginID As String, ByVal PaymentMode As String, ByVal AgentID As String, ByVal Status As String, ByVal SearchType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@LoginID", LoginID)
        paramHashtable.Add("@PaymentMode", PaymentMode)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@SearchType", SearchType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetDepositDetailsWithdate", 3)
    End Function



    'Account Panel
    Public Function DepositProcessDetails(ByVal Status As String, ByVal Type As String, Optional ByVal AccountID As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@AccountID", AccountID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetDepositProcessDetails", 3)
    End Function
    Public Function UpdateDepositDetails(ByVal ID As String, ByVal AgentID As String, ByVal Status As String, ByVal Type As String, ByVal AccountID As String, ByVal Rmk As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@ID", ID)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@AccountID", AccountID)
        paramHashtable.Add("@Rmk", Rmk)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateDepositDetails", 1)
    End Function
    Public Function GetDepositDetailsByID(ByVal ID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@ID", ID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetDepositeDetailsByID", 3)
    End Function
    Public Function DepositStatusDetails(ByVal Status As String, Optional ByVal AccountID As String = "") As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@AccountID", AccountID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetDepositStatusDetails", 3)
    End Function
    Public Function GetTypeByID() As DataSet
        paramHashtable.Clear()
        ' paramHashtable.Add("@UID", ID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetTypeByID", 3)
    End Function
    Public Function GetAgentType() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetAgentType", 3)
    End Function
    'Public Function UpdateAgentTypeSalesRef(ByVal ID As String, ByVal SalesRef As String, ByVal Type As String, ByVal AgentStatus As String, ByVal Online_Tkt As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Country As String, ByVal Zipcode As String, ByVal FName As String, ByVal LName As String, ByVal Mobile As String, ByVal Email As String, ByVal Fax As String, ByVal Pan As String, ByVal Title As String, ByVal AgencyName As String, ByVal pwd As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@UID", ID)
    '    paramHashtable.Add("@SalesRef", SalesRef)
    '    paramHashtable.Add("@Type", Type)
    '    paramHashtable.Add("@AgentStatus", AgentStatus)
    '    paramHashtable.Add("@Online_Tkt", Online_Tkt)
    '    paramHashtable.Add("@Address", Address)
    '    paramHashtable.Add("@City", City)
    '    paramHashtable.Add("@State", State)
    '    paramHashtable.Add("@Country", Country)
    '    paramHashtable.Add("@Zipcode", Zipcode)
    '    paramHashtable.Add("@FName", FName)
    '    paramHashtable.Add("@LName", LName)
    '    paramHashtable.Add("@Mobile", Mobile)
    '    paramHashtable.Add("@Email", Email)
    '    paramHashtable.Add("@Fax", Fax)
    '    paramHashtable.Add("@Pan", Pan)
    '    paramHashtable.Add("@Title", Title)
    '    paramHashtable.Add("@AgencyName", AgencyName)
    '    paramHashtable.Add("@pwd", pwd)
    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateAgentTypeSalesRef", 1)
    'End Function
    Public Function UpdateAgentTypeSalesRef(ByVal ID As String, ByVal SalesRef As String, ByVal Type As String, ByVal AgentStatus As String, ByVal Online_Tkt As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Country As String, ByVal Zipcode As String, ByVal FName As String, ByVal LName As String, ByVal Mobile As String, ByVal Email As String, ByVal Fax As String, ByVal Pan As String, ByVal Title As String, ByVal AgencyName As String, ByVal pwd As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@UID", ID)
        paramHashtable.Add("@SalesRef", SalesRef)
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@AgentStatus", AgentStatus)
        paramHashtable.Add("@Online_Tkt", Online_Tkt)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@City", City)
        paramHashtable.Add("@State", State)
        paramHashtable.Add("@Country", Country)
        paramHashtable.Add("@Zipcode", Zipcode)
        paramHashtable.Add("@FName", FName)
        paramHashtable.Add("@LName", LName)
        paramHashtable.Add("@Mobile", Mobile)
        paramHashtable.Add("@Email", Email)
        paramHashtable.Add("@Fax", Fax)
        paramHashtable.Add("@Pan", Pan)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@AgencyName", AgencyName)
        paramHashtable.Add("@pwd", pwd)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateAgentTypeSalesRef_Dist", 1)
    End Function
    Public Function GetLedgerDetails(ByVal usertype As String, ByVal LoginID As String, ByVal FormDate As String, ByVal ToDate As String, ByVal AgentId As String, ByVal BookingType As String, ByVal SearchType As String, ByVal Price As String, ByVal OrderNo As String, ByVal AirCode As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginID)
        paramHashtable.Add("@FormDate", FormDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@SearchType", SearchType)
        paramHashtable.Add("@Price", Price)
        paramHashtable.Add("@OrderNo", OrderNo)
        paramHashtable.Add("@AirCode", AirCode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetLedgerDetail", 3)
    End Function
    Public Function GetLedgerDetailsSingleOrderID(ByVal usertype As String, ByVal LoginID As String, ByVal FormDate As String, ByVal ToDate As String, ByVal AgentId As String, ByVal BookingType As String, ByVal SearchType As String, ByVal Price As String, ByVal OrderNo As String, ByVal AirCode As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginID)
        paramHashtable.Add("@FormDate", FormDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@SearchType", SearchType)
        paramHashtable.Add("@Price", Price)
        paramHashtable.Add("@OrderNo", OrderNo)
        paramHashtable.Add("@AirCode", AirCode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetLedgerDetailSingleOrderID", 3)
    End Function

    'InsertRegistration
    'Public Function InsertRegistration(ByVal user_id As String, ByVal Title As String, ByVal Fname As String, ByVal Lname As String, ByVal Address As String, ByVal city As String, ByVal state As String, ByVal country As String, ByVal zipcode As String, ByVal Phone As String, ByVal Mobile As String, ByVal email As String, ByVal Alt_Email As String, ByVal Fax_no As String, ByVal Agency_Name As String, ByVal Website As String, ByVal PanNo As String, ByVal Status As String, ByVal Stax_no As String, ByVal Remark As String, ByVal Sec_Qes As String, ByVal Sec_Ans As String, ByVal PWD As String, ByVal Agent_Type As String, ByVal Distr As String, ByVal SalesExecID As String, ByVal ag_logo As String) As Integer
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@user_id", user_id)
    '    paramHashtable.Add("@Title", Title)
    '    paramHashtable.Add("@Fname", Fname)

    '    paramHashtable.Add("@Lname", Lname)
    '    paramHashtable.Add("@Address", Address)
    '    paramHashtable.Add("@city", city)

    '    paramHashtable.Add("@state", state)
    '    paramHashtable.Add("@country", country)
    '    paramHashtable.Add("@zipcode", zipcode)

    '    paramHashtable.Add("@Phone", Phone)
    '    paramHashtable.Add("@Mobile", Mobile)
    '    paramHashtable.Add("@email", email)

    '    paramHashtable.Add("@Alt_Email", Alt_Email)
    '    paramHashtable.Add("@Fax_no", Fax_no)
    '    paramHashtable.Add("@Agency_Name", Agency_Name)
    '    paramHashtable.Add("@Website", Website)

    '    paramHashtable.Add("@PanNo", PanNo)
    '    paramHashtable.Add("@Status", Status)
    '    paramHashtable.Add("@Stax_no", Stax_no)

    '    paramHashtable.Add("@Remark", Remark)
    '    paramHashtable.Add("@Sec_Qes", Sec_Qes)
    '    paramHashtable.Add("@Sec_Ans", Sec_Ans)

    '    paramHashtable.Add("@PWD", PWD)
    '    paramHashtable.Add("@Agent_Type", Agent_Type)
    '    paramHashtable.Add("@Distr", Distr)
    '    paramHashtable.Add("@SalesExecID", SalesExecID)

    '    paramHashtable.Add("@ag_logo", ag_logo)



    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertRegistrationDetails", 1)
    'End Function

    Public Function InsertRegistration_AgentReg(ByVal user_id As String, ByVal Title As String, ByVal Fname As String, ByVal Lname As String, ByVal Address As String, ByVal city As String, ByVal state As String, ByVal country As String, ByVal Area As String, ByVal zipcode As String, ByVal Phone As String, ByVal Mobile As String, ByVal WhMobile As String, ByVal email As String, ByVal Alt_Email As String, ByVal Fax_no As String, ByVal Agency_Name As String, ByVal Website As String, ByVal NameOnPan As String, ByVal PanNo As String, ByVal Status As String, ByVal Stax_no As String, ByVal Remark As String, ByVal Sec_Qes As String, ByVal Sec_Ans As String, ByVal PWD As String, ByVal Agent_Type As String, ByVal Distr As String, ByVal SalesExecID As String, ByVal ag_logo As String, ByVal Branch As String,
                                                ByVal gstNo As String, ByVal gstCmpName As String, ByVal gstCmpAdd As String, ByVal gstPhone As String, ByVal gstEmail As String,
                                                ByVal gstApply As Boolean, ByVal gstCity As String, ByVal gstState As String, ByVal gstStateCode As String, ByVal gstPinCode As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@user_id", user_id)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@Fname", Fname)

        paramHashtable.Add("@Lname", Lname)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@city", city)

        paramHashtable.Add("@state", state)
        paramHashtable.Add("@country", country)
        'Bipin
        paramHashtable.Add("@Area", Area)
        paramHashtable.Add("@zipcode", zipcode)

        paramHashtable.Add("@Phone", Phone)
        paramHashtable.Add("@Mobile", Mobile)
        paramHashtable.Add("@email", email)
        paramHashtable.Add("@WhMobile", WhMobile)

        paramHashtable.Add("@Alt_Email", Alt_Email)
        paramHashtable.Add("@Fax_no", Fax_no)
        paramHashtable.Add("@Agency_Name", Agency_Name)
        paramHashtable.Add("@Website", Website)
        paramHashtable.Add("@NameOnPan", NameOnPan)
        paramHashtable.Add("@PanNo", PanNo)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Stax_no", Stax_no)

        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@Sec_Qes", Sec_Qes)
        paramHashtable.Add("@Sec_Ans", Sec_Ans)

        paramHashtable.Add("@PWD", PWD)
        paramHashtable.Add("@Agent_Type", Agent_Type)
        paramHashtable.Add("@Distr", Distr)
        paramHashtable.Add("@SalesExecID", SalesExecID)

        paramHashtable.Add("@ag_logo", ag_logo)
        paramHashtable.Add("@Branch", Branch)

        paramHashtable.Add("@GSTNO", gstNo)
        paramHashtable.Add("@GSTCompanyName", gstCmpName)
        paramHashtable.Add("@GSTCompanyAddress", gstCmpAdd)
        paramHashtable.Add("@GSTPhoneNo", gstPhone)
        paramHashtable.Add("@GSTEmail", gstEmail)
        paramHashtable.Add("@IsGSTApply", gstApply)
        paramHashtable.Add("@GSTCity", gstCity)
        paramHashtable.Add("@GSTState", gstState)
        paramHashtable.Add("@GSTStateCode", gstStateCode)
        paramHashtable.Add("@GSTPinCode", gstPinCode)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertRegistrationDetails_AgentReg_New", 1)
    End Function

    Public Function InsertRegistration(ByVal user_id As String, ByVal Title As String, ByVal Fname As String, ByVal Lname As String, ByVal Address As String, ByVal city As String, ByVal state As String, ByVal country As String, ByVal Area As String, ByVal zipcode As String, ByVal Phone As String, ByVal Mobile As String, ByVal email As String, ByVal Alt_Email As String, ByVal Fax_no As String, ByVal Agency_Name As String, ByVal Website As String, ByVal NameOnPan As String, ByVal PanNo As String, ByVal Status As String, ByVal Stax_no As String, ByVal Remark As String, ByVal Sec_Qes As String, ByVal Sec_Ans As String, ByVal PWD As String, ByVal Agent_Type As String, ByVal Distr As String, ByVal SalesExecID As String, ByVal ag_logo As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@user_id", user_id)
        paramHashtable.Add("@Title", Title)
        paramHashtable.Add("@Fname", Fname)

        paramHashtable.Add("@Lname", Lname)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@city", city)

        paramHashtable.Add("@state", state)
        paramHashtable.Add("@country", country)
        'Bipin
        paramHashtable.Add("@Area", Area)
        paramHashtable.Add("@zipcode", zipcode)

        paramHashtable.Add("@Phone", Phone)
        paramHashtable.Add("@Mobile", Mobile)
        paramHashtable.Add("@email", email)

        paramHashtable.Add("@Alt_Email", Alt_Email)
        paramHashtable.Add("@Fax_no", Fax_no)
        paramHashtable.Add("@Agency_Name", Agency_Name)
        paramHashtable.Add("@Website", Website)
        paramHashtable.Add("@NameOnPan", NameOnPan)
        paramHashtable.Add("@PanNo", PanNo)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@Stax_no", Stax_no)

        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@Sec_Qes", Sec_Qes)
        paramHashtable.Add("@Sec_Ans", Sec_Ans)

        paramHashtable.Add("@PWD", PWD)
        paramHashtable.Add("@Agent_Type", Agent_Type)
        paramHashtable.Add("@Distr", Distr)
        paramHashtable.Add("@SalesExecID", SalesExecID)

        paramHashtable.Add("@ag_logo", ag_logo)



        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertRegistrationDetails", 1)
    End Function


    Public Function GetDetailByEmailMobile(ByVal Email As String, ByVal Mobile As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Email", Email)

        paramHashtable.Add("@Mobile", Mobile)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetDetailByEmailMobile", 3)
    End Function
    Public Function getmaxcount() As Double
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetNewRegsMaxCount", 2)
    End Function
    Public Function GetSalesRef() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetSalesRef", 3)
    End Function
    Public Function GetCreditNodeDetails(ByVal RefundID As String, ByVal Trip As String, ByVal Status As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@RefundID", RefundID)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@Status", Status)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetCreditNodeDetails", 3)
    End Function
    Public Function InvoiceNoRail(ByVal PNR As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@PNR", PNR)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InvoiceNoRail", 2)
    End Function
    Public Function getAgencybySalesRef(ByVal SalesRef As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@SalesRef", SalesRef)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "getAgencybySalesRef", 3)
    End Function
    Public Function UpdateTktDomIntlOnLedger(ByVal OrderId As String, ByVal PaxId As Integer, ByVal TktNo As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PaxId", PaxId)
        paramHashtable.Add("@TktNo", TktNo)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateTktDomIntl", 3)
    End Function
    'Select Status and Executive
    Public Function GetStatusExecutiveID(ByVal ModuleName As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Module", ModuleName)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetStatusExecutiveID", 3)
    End Function
    'Get Proxy Booking Report
    Public Function GetProxyBookingReport(ByVal usertype As String, ByVal LoginID As String, ByVal FormDate As String, ByVal ToDate As String, ByVal AgentID As String, ByVal ExecID As String, ByVal Status As String, ByVal ProxyTrip As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@usertype", usertype)
        paramHashtable.Add("@LoginID", LoginID)
        paramHashtable.Add("@FormDate", FormDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentID", AgentID)
        paramHashtable.Add("@ExecID", ExecID)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@ProxyTrip", ProxyTrip)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetProxyBookingReport", 3)
    End Function
    'Update Agent Profile
    Public Function UpdateAgentProfile(ByVal Type As String, ByVal UID As String, ByVal pwd As String, ByVal AEmail As String, ByVal Landline As String, ByVal Fax As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Country As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@pwd", pwd)
        paramHashtable.Add("@AEmail", AEmail)
        paramHashtable.Add("@Landline", Landline)
        paramHashtable.Add("@Fax", Fax)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@City", City)
        paramHashtable.Add("@State", State)
        paramHashtable.Add("@Country", Country)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateAgentProfile", 1)
    End Function
    'Ckeck Forgot  Password
    Public Function CheckForgotPassword(ByVal UID As String, ByVal Email As String, ByVal Mobile As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@Email", Email)
        paramHashtable.Add("@Mobile", Mobile)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckForgotPassword", 3)
    End Function
    'Public Sub ExportData(ByVal dset As DataSet)
    '    Dim response As HttpResponse = HttpContext.Current.Response
    '    ' first let's clean up the response.object   
    '    response.Clear()
    '    response.Charset = ""
    '    ' set the response mime type for excel  
    '    'Dim filename As String = "Record.xls"
    '    Dim filename As String = "Report(" & "" & DateTime.Now.ToString("dd-MM-yyyy") & ").xls"

    '    response.ContentType = "application/vnd.ms-excel"
    '    response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & """")

    '    ' create a string writer   
    '    Using sw As New StringWriter()
    '        Using htw As New HtmlTextWriter(sw)
    '            ' instantiate a datagrid   
    '            Dim dg As New DataGrid()
    '            dg.DataSource = dset.Tables(0)
    '            dg.DataBind()
    '            dg.RenderControl(htw)
    '            response.Write(sw.ToString())
    '            response.[End]()
    '        End Using
    '    End Using

    'End Sub

    Public Sub ExportData(ByVal ds As DataSet)
        'Dim response As HttpResponse = HttpContext.Current.Response
        '' first let's clean up the response.object   
        'response.Clear()
        'response.Charset = ""
        '' set the response mime type for excel  
        ''Dim filename As String = "Record.xls"
        'Dim filename As String = "Report(" & "" & DateTime.Now.ToString("dd-MM-yyyy") & ").xls"

        'response.ContentType = "application/vnd.ms-excel"
        'response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & """")

        '' create a string writer   
        'Using sw As New StringWriter()
        '    Using htw As New HtmlTextWriter(sw)
        '        ' instantiate a datagrid   
        '        Dim dg As New DataGrid()
        '        dg.DataSource = dset.Tables(0)
        '        dg.DataBind()
        '        dg.RenderControl(htw)
        '        response.Write(sw.ToString())
        '        response.[End]()
        '    End Using
        'End Using
        Dim FilterList As String() = New String() {"Red", "Blue"}
        Dim Response As HttpResponse = HttpContext.Current.Response
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()

        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Write("<!DOCTYPE HTML  PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">")
        Dim filename As String = ""
        If filename <> "" Then
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ").xls")

        Else
            Response.AddHeader("content-disposition", "attachment;filename=Report(" & "" & DateTime.Now.ToString("dd-MM-yyyyHHmmss") & ").xls")

        End If


        Response.ContentEncoding = Encoding.UTF8
        Response.Charset = ""
        'EnableViewState = False

        'Set Fonts
        Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>")
        Response.Write("<BR><BR><BR>")

        'Sets the table border, cell spacing, border color, font of the text, background,
        'foreground, font height
        Response.Write("<Table border='2' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:#F4F4F4;'> <TR>")

        ' Check not to increase number of records more than 65k according to excel,03
        If ds.Tables(0).Rows.Count <= 65536 Then
            ' Get DataTable Column's Header
            For Each column As DataColumn In ds.Tables(0).Columns
                'Write in new column
                Response.Write("<Td align='center'    style='padding-top: 7px;font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; background-color: #004b91; color: #FFFFFF;'>")

                'Get column headers  and make it as bold in excel columns
                Response.Write("<B >")
                Response.Write(column)
                Response.Write("</B>")
                Response.Write("</Td>")

            Next

            Response.Write("</TR>")

            ' Get DataTable Column's Row
            For Each dtRow As DataRow In ds.Tables(0).Rows
                'Write in new row
                Response.Write("<TR>")

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    Response.Write("<Td>")
                    Response.Write(dtRow(i).ToString())
                    Response.Write("</Td>")
                Next

                Response.Write("</TR>")
            Next
        End If

        Response.Write("</Table>")
        Response.Write("</font>")

        Response.Flush()
        Response.[End]()
    End Sub
    Public Sub ExportData(ByVal ds As DataSet, Optional ByVal fileName As String = "")
        'Dim response As HttpResponse = HttpContext.Current.Response
        '' first let's clean up the response.object   
        'response.Clear()
        'response.Charset = ""
        '' set the response mime type for excel  
        ''Dim filename As String = "Record.xls"
        'Dim filename As String = "Report(" & "" & DateTime.Now.ToString("dd-MM-yyyy") & ").xls"

        'response.ContentType = "application/vnd.ms-excel"
        'response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & """")

        '' create a string writer   
        'Using sw As New StringWriter()
        '    Using htw As New HtmlTextWriter(sw)
        '        ' instantiate a datagrid   
        '        Dim dg As New DataGrid()
        '        dg.DataSource = dset.Tables(0)
        '        dg.DataBind()
        '        dg.RenderControl(htw)
        '        response.Write(sw.ToString())
        '        response.[End]()
        '    End Using
        'End Using
        Dim FilterList As String() = New String() {"Red", "Blue"}
        Dim Response As HttpResponse = HttpContext.Current.Response
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()

        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Write("<!DOCTYPE HTML  PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">")

        If fileName <> "" Then
            Response.AddHeader("content-disposition", "attachment;filename=" & fileName & ").xls")

        Else
            Response.AddHeader("content-disposition", "attachment;filename=Report(" & "" & DateTime.Now.ToString("dd-MM-yyyyHHmmss") & ").xls")

        End If


        Response.ContentEncoding = Encoding.UTF8
        Response.Charset = ""
        'EnableViewState = False

        'Set Fonts
        Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>")
        Response.Write("<BR><BR><BR>")

        'Sets the table border, cell spacing, border color, font of the text, background,
        'foreground, font height
        Response.Write("<Table border='2' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:#F4F4F4;'> <TR>")

        ' Check not to increase number of records more than 65k according to excel,03
        If ds.Tables(0).Rows.Count <= 65536 Then
            ' Get DataTable Column's Header
            For Each column As DataColumn In ds.Tables(0).Columns
                'Write in new column
                Response.Write("<Td align='center'    style='padding-top: 7px;font-family: arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; background-color: #004b91; color: #FFFFFF;'>")

                'Get column headers  and make it as bold in excel columns
                Response.Write("<B >")
                Response.Write(column)
                Response.Write("</B>")
                Response.Write("</Td>")

            Next

            Response.Write("</TR>")

            ' Get DataTable Column's Row
            For Each dtRow As DataRow In ds.Tables(0).Rows
                'Write in new row
                Response.Write("<TR>")

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    Response.Write("<Td>")
                    Response.Write(dtRow(i).ToString())
                    Response.Write("</Td>")
                Next

                Response.Write("</TR>")
            Next
        End If

        Response.Write("</Table>")
        Response.Write("</font>")

        Response.Flush()
        Response.[End]()
    End Sub
    'AirlineStatusTrueFalse
    Public Function AirlineEnableTrueFalse(ByVal org As String, ByVal Dest As String, ByVal FlightNo As String, ByVal Airline As String, ByVal Trip As String) As String
        paramHashtable.Clear()
        paramHashtable.Add("@Org", org)
        paramHashtable.Add("@Dest", Dest)
        paramHashtable.Add("@FlightNo", FlightNo)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Trip", Trip)
        Dim i As String = objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SpBlockBookingAirlineWise", 2)
        Return i
    End Function
    'For Ledger Details BookingType
    Public Function GetLedgerBookingType() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GET_LEDGER_BOOKINGTYPE", 3)
    End Function
    'Public Function GetAgencyDetailsDynamic(ByVal UserId As String, ByVal AgentType As String, ByVal SalesPersonID As String, ByVal FromDate As String, ByVal ToDate As String, ByVal DistrId As String, ByVal UserType As String) As DataSet
    '    paramHashtable.Clear()
    '    paramHashtable.Add("@UserId", UserId)
    '    paramHashtable.Add("@AgentType", AgentType)
    '    paramHashtable.Add("@SalesExecID", SalesPersonID)
    '    paramHashtable.Add("@FromDate", FromDate)
    '    paramHashtable.Add("@ToDate", ToDate)
    '    paramHashtable.Add("@DistrId", DistrId)
    '    paramHashtable.Add("@UserType", UserType)

    '    Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetailsDynamic", 3)
    'End Function
    Public Function GetAgencyDetailsDynamic(ByVal UserId As String, ByVal AgentType As String, ByVal SalesPersonID As String, ByVal FromDate As String, ByVal ToDate As String, ByVal DistrId As String, ByVal UserType As String, ByVal DiSearch As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        paramHashtable.Add("@AgentType", AgentType)
        paramHashtable.Add("@SalesExecID", SalesPersonID)
        paramHashtable.Add("@FromDate", FromDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@DistrId", DistrId)
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@DiSearch", DiSearch)

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "AgencyDetailsDynamic", 3)
    End Function
    Public Function GetAllGroupType() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "Sp_GetAllGroupType", 3)
    End Function

    'baggage allowance of domestic airlines
    Public Function GetBaggageInformation(ByVal Trip As String, ByVal airCode As String, ByVal IsBagFare As Boolean) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@AIRLINE", airCode)
        paramHashtable.Add("@IsBagFare", IsBagFare)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETBAGGAGE_INFO", 3)
    End Function

    Public Function GetFlightDetailsByAgentId(ByVal AgentID As String, ByVal date1 As String) As List(Of FltDetails)


        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Dim DateFarmate() As String
        DateFarmate = date1.Split("-")
        If (Convert.ToInt16(DateFarmate(1)) = 0) Then
            date1 = 1
            date1 = DateFarmate(0) + "-" + (Convert.ToInt16(DateFarmate(1)) + 1).ToString() + "-" + DateFarmate(2)
            date1 = Convert.ToDateTime(date1).ToString("ddMMyy")
            '   var validdate = validDate.split('-');
            '  if (parseInt(validdate[1]) == 0) {

            '      validDate = validdate[0] + "-" + (parseInt(validdate[1]) + 1).toString() + "-" + validdate[2];
            '}


        Else
            date1 = Convert.ToDateTime(date1).AddMonths(1).ToString("ddMMyy")
        End If
        Dim fltDList As New List(Of FltDetails)
        Try

            Dim cmd As New SqlCommand("Sp_GetFlightDetailsForTravelCal", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgentID", AgentID.Trim())
            'cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date1).AddMonths(1).ToString("ddMMyy"))
            cmd.Parameters.AddWithValue("@date", date1)
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()


            While dr.Read()
                fltDList.Add(New FltDetails() With {.DepDate = dr("DepDate").ToString().Trim(), .Pnr = dr("GdsPnr").ToString().Trim(), .DepTime = dr("DepTime").ToString().Trim(), .Sector = dr("sector").ToString().Trim(), .OrderID = dr("OrderId").ToString().Trim()})

            End While


            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
            con.Dispose()
        End Try


        Return fltDList




    End Function



    Public Function GetFlightDetailsByDateForCal(ByVal AgentID As String, ByVal date1 As String) As List(Of FltDetails)


        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Dim fltDList As New List(Of FltDetails)
        Try

            Dim cmd As New SqlCommand("Sp_GetFlightDetailsByDateForCal", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgentID", AgentID.Trim())
            cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date1).ToString("ddMMyy"))
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()


            While dr.Read()
                fltDList.Add(New FltDetails() With {.DepDate = dr("DepDate").ToString().Trim(), .Pnr = dr("GdsPnr").ToString().Trim(), .DepTime = dr("DepTime").ToString().Trim(), .Sector = dr("sector").ToString().Trim(), .OrderID = dr("OrderId").ToString().Trim()})

            End While


            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
            con.Dispose()
        End Try


        Return fltDList




    End Function
    'MAILING
    Public Function SendMail(ByVal toEMail As String, ByVal from As String, ByVal bcc As String, ByVal cc As String, ByVal smtpClient As String, ByVal userID As String, ByVal pass As String, ByVal body As String, ByVal subject As String, ByVal AttachmentFile As String) As Integer

        Dim objMail As New System.Net.Mail.SmtpClient
        Dim msgMail As New System.Net.Mail.MailMessage
        msgMail.To.Clear()
        msgMail.To.Add(New System.Net.Mail.MailAddress(toEMail))
        msgMail.From = New System.Net.Mail.MailAddress(from)
        If bcc <> "" Then
            msgMail.Bcc.Add(New System.Net.Mail.MailAddress(bcc))
        End If
        If cc <> "" Then
            msgMail.CC.Add(New System.Net.Mail.MailAddress(cc))
        End If
        If AttachmentFile <> "" Then
            msgMail.Attachments.Add(New System.Net.Mail.Attachment(AttachmentFile))
        End If

        msgMail.Subject = subject
        msgMail.IsBodyHtml = True
        msgMail.Body = body


        Try
            objMail.Credentials = New System.Net.NetworkCredential(userID, pass)
            objMail.EnableSsl = True
            objMail.Host = smtpClient
            objMail.Send(msgMail)
            Return 1

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            Return 0

        End Try
    End Function
    Public Function GetMailingDetails(ByVal department As String, ByVal UserID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@Department", department)
        paramHashtable.Add("@UserID", UserID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETMAILINGCREDENTIAL_ITZ", 3)
    End Function
    Public Function GetCompanyAddress(ByVal AddressType As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TYPE", AddressType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETADDRESS", 3)
    End Function
    'Ledger and Credit Limit With Transaction
    Public Function Ledgerandcreditlimit_Transaction(ByVal AGENTID As String, ByVal TOTALAFETRDIS As Double, ByVal TRACKID As String, ByVal VC As String, ByVal GDSPNR As String, ByVal AGENCYNAME As String, ByVal IP As String, ByVal ProjectId As String, ByVal BookedBy As String, ByVal BillNo As String, ByVal AvailBal As Double, ByVal EasyID As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@AGENTID", AGENTID)
        paramHashtable.Add("@TOTALAFETRDIS", TOTALAFETRDIS)
        paramHashtable.Add("@TRACKID", TRACKID)
        paramHashtable.Add("@VC", VC)
        paramHashtable.Add("@GDSPNR", GDSPNR)
        paramHashtable.Add("@AGENCYNAME", AGENCYNAME)
        paramHashtable.Add("@IP", IP)
        paramHashtable.Add("@ProjectId", ProjectId)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@BillNo", BillNo)
        paramHashtable.Add("@AVAILBAL", AvailBal)
        paramHashtable.Add("@EasyID", EasyID)

        Dim i As Integer = objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_LEDGERNEWREGES_TRANSACTION_V1", 2)
        '' Dim i As Integer = objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_INSERT_LEDGERNEWREGES_TRANSACTION_NEW", 2)
        Return i
        'End Ledger and Credit Limit With Transaction
    End Function
    ''Public Function Ledgerandcreditlimit_Transaction(ByVal AGENTID As String, ByVal TOTALAFETRDIS As Double, ByVal TRACKID As String, ByVal VC As String, ByVal GDSPNR As String, ByVal AGENCYNAME As String, ByVal IP As String, ByVal ProjectId As String, ByVal BookedBy As String, ByVal BillNo As String) As Integer
    ''    paramHashtable.Clear()
    ''    paramHashtable.Add("@AGENTID", AGENTID)
    ''    paramHashtable.Add("@TOTALAFETRDIS", TOTALAFETRDIS)
    ''    paramHashtable.Add("@TRACKID", TRACKID)
    ''    paramHashtable.Add("@VC", VC)
    ''    paramHashtable.Add("@GDSPNR", GDSPNR)
    ''    paramHashtable.Add("@AGENCYNAME", AGENCYNAME)
    ''    paramHashtable.Add("@IP", IP)
    ''    paramHashtable.Add("@ProjectId", ProjectId)
    ''    paramHashtable.Add("@BookedBy", BookedBy)
    ''    paramHashtable.Add("@BillNo", BillNo)
    ''    Dim i As Integer = objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_LEDGERNEWREGES_TRANSACTION", 2)
    ''    Return i
    ''    'End Ledger and Credit Limit With Transaction
    ''End Function

    'Update Ledger by paxId
    Public Function UpdateLedger_PaxId(ByVal PAXID As Integer, ByVal TICKETNO As String, ByVal PNR As String) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@PAXID", PAXID)
        paramHashtable.Add("@TICKETNO", TICKETNO)
        paramHashtable.Add("@PNR", PNR)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_UPDATE_LEDGERBYPAXID", 1)
    End Function
    'End Update Ledger by paxId

    'Get Module Details 
    Public Function GetModuleAccessDetails(ByVal UID As String, ByVal MODULENAME As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@USERID", UID)
        paramHashtable.Add("@MODULE", MODULENAME)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETMODULEACCESS_DETAILS", 3)
    End Function
    'New FLayout
    Public Function SelectHeaderDetail_HOLD(ByVal OrderId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GETHRDDETAILS_HOLD", 3)
    End Function
    Public Function GetAirportName(ByVal AirCode As String) As String
        paramHashtable.Clear()
        paramHashtable.Add("@AIRCODE", AirCode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_GET_AIRPORTNAME", 2)
    End Function
    ' Public Function Bind_Flight_PaxDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String, ByVal dtPax As DataTable) As String
        ' Dim Result As String = ""
        ' Dim div_FlightDel As String = ""
        ' Dim div_PaxDel As String = ""

        ' div_FlightDel = "<br /><div id='dv_FlightDetail'>"
        ' For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
            ' div_FlightDel += "<div class='row'> <div class='col-sm-6'> <p style='text-align: left;'> <img alt='flight-icon' class='flightImagesNew' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif' />   " & OBDS.Tables(0).Rows(i)("AirlineName") & " (" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & " </p> </div> <div class='col-sm-6'> <p style='text-align: right;'> " & OBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & OBDS.Tables(0).Rows(i)("ArrivalLocation") & " , " & OBDS.Tables(0).Rows(i)("Departure_Date") & " </p> </div> </div>"
        ' Next
        ' div_PaxDel = "<div id='dv_PaxDetail'><div class='row' style='border-bottom: 1px dotted #ccc;'> <div class='col-sm-6'> <h5>Passenger's Name</h5> </div> <div class='col-sm-6 text-right'> <h5>Passenger's D.O.B</h5> </div> </div>"

        ' For Each dr As DataRow In dtPax.Rows
            ' div_PaxDel += "<div class='row' style='padding-top: 5px;'> <div class='col-sm-6'> <p style='text-align: left;'> <i class='icofont-business-man-alt-1 icofont-1x'></i> " & dr("Name").ToString() & " </p> </div> <div class='col-sm-6'> <p style='text-align: right;'> " & dr("DOB").ToString() & " </p> </div> </div>"
        ' Next

        ' Result = div_FlightDel + "</div>" + div_PaxDel + "</div>"

        ' Return Result
    ' End Function
	Public Function Bind_Flight_PaxDetails(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String, ByVal dtPax As DataTable) As String
        Dim Result As String = ""
        Dim div_FlightDel As String = ""
        Dim div_PaxDel As String = ""

        div_FlightDel = "<br /><div id='dv_FlightDetail'>"
        For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
            div_FlightDel += "<div class='row'> <div class='col-sm-6'> <p style='text-align: left;'> <img alt='flight-icon' class='flightImagesNew' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif' />" & OBDS.Tables(0).Rows(i)("AirlineName") & "<br/><span style='margin-left: 16%;'>" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & "</span>" & " </p> </div> <div class='col-sm-6'> <p style='text-align: right;'> " & OBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & OBDS.Tables(0).Rows(i)("ArrivalLocation") & ", " & OBDS.Tables(0).Rows(i)("Departure_Date") & " </p><p style='text-align: right;'> DeptTime : " & OBDS.Tables(0).Rows(i)("DepartureTime") & " - ArrTime : " & OBDS.Tables(0).Rows(i)("ArrivalTime") & " </p></div> </div>"
        Next
        div_PaxDel = "<div id='dv_PaxDetail'><div class='row' style='border-bottom: 1px dotted #ccc;'> <div class='col-sm-6'> <h5>Passenger's Name</h5> </div> <div class='col-sm-6 text-right'> <h5>Passenger's D.O.B</h5> </div> </div>"

        For Each dr As DataRow In dtPax.Rows
            div_PaxDel += "<div class='row' style='padding-top: 5px;'> <div class='col-sm-6'> <p style='text-align: left;'> "
            If dr("PaxType").ToString() = "ADT" Then
                If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                    div_PaxDel += "<i class='icofont-businesswoman icofont-1x'></i>"
                Else
                    div_PaxDel += "<i class='icofont-business-man-alt-1 icofont-1x'></i>"
                End If
            ElseIf dr("PaxType").ToString() = "CHD" Then
                If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                    div_PaxDel += "<i class='icofont-girl-alt icofont-1x'></i>"
                Else
                    div_PaxDel += "<i class='icofont-boy icofont-1x'></i>"
                End If
            Else
                If dr("title").ToString().ToLower().Contains("s") = True And dr("title").ToString().ToLower() <> "mstr" Then
                    div_PaxDel += "<i class='icofont-baby icofont-1x'></i>"
                Else
                    div_PaxDel += "<i class='icofont-baby icofont-1x'></i>"
                End If
            End If
            div_PaxDel += dr("Name").ToString() & " </p> </div> <div class='col-sm-6'> <p style='text-align: right;'> " & dr("DOB").ToString() & " </p> </div> </div>"
        Next

        Result = div_FlightDel + "</div>" + div_PaxDel + "</div>"

        Return Result
    End Function
    Public Function CustFltDetails_Dom(ByVal OBDS As DataSet, ByVal IBDS As DataSet, ByVal FT As String) As String
        Dim FlightDtlsTotalInfo As String = ""
        Dim DepTerminal As String
        Dim ArrTerminal As String
        Dim RBD As String
        'Dim IBRBD As String

        'Dim OBCABIN As String
        Dim CABIN As String
        Dim FlightType = ""
        If FT = "InBound" Then
            FlightType = "OutBound"
        End If

        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div>"

        For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1
            DepTerminal = ""
            ArrTerminal = ""
            RBD = ""
            CABIN = ""
            If OBDS.Tables(0).Rows(i)("RBD").ToString().Trim() <> "" Then
                RBD = "Class (" & OBDS.Tables(0).Rows(i)("RBD") & ")"
            End If
            If OBDS.Tables(0).Rows(i)("AdtCabin").ToString().Trim() <> "" Then
                CABIN = "Cabin (" & CabinName(OBDS.Tables(0).Rows(i)("AdtCabin")) & ")"
            End If


            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='' style='padding:5px;'><span style='font-weight:600;font-size:17px;'>" & FlightType & "</span></div>" '" & OBDS.Tables(0).Rows(0)("AdtFareType") & "


            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='flt-details'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fl width100 padTB10'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fl width100 padLR10 padTB10'><i class='icofont-airplane icofont-rotate-90 fl icon-flight_line1 ico18 mobdn blue '></i><span class='fl mobdn ico18 padL10'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & " - " & OBDS.Tables(0).Rows(i)("ArrivalLocation") & " , " & OBDS.Tables(0).Rows(i)("Departure_Date") & "</span><span class='fr txtTransUpper white ico11 brRadius2 pad5 greenBgLt'>" & OBDS.Tables(0).Rows(i)("AdtFareType") & "</span></div>"



            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='borderAll posRel whiteBg brRadius5 fl width100 fl padTB10'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div Class='bkHalfCircleTop mobdn bkHalfCirctop posAbs'></div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-2'><span class='db padB5'><img alt='flight-icon' class='flightImagesNew' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/></span><span class='db greyLt ico12'>" & OBDS.Tables(0).Rows(i)("AirlineName") & " (" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</span><span class='db greyLter padT2 ico11'>" & RBD & "</span><span class='db greyLter padT2 ico11'>" & CABIN & "</span></div>"




            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3 col-sm-3 col-xs-4 padL20'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='co12 db grey'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='ico20 db padT5 padB10'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "<span class='fb mobdb'>" & MultiValueFunction_Dom(OBDS.Tables(0), "Depall", i) & "</span></span>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='ico13 db greyLter lh1-2 mobdn'> " & OBDS.Tables(0).Rows(i)("DepartureTerminal") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"




            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-4 col-sm-4 col-xs-4 mobdn txtCenter padLR20 padT20'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span Class='db ico20 backgroundLn'><i class='oval-2 fl'></i><span class='padLR10 whiteBg'>" & OBDS.Tables(0).Rows(i)("Tot_Dur") & "h</span><i class='icofont-airplane icofont-rotate-90 fr ico22 blue icon-flight_line1'></i></span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span Class='ico12 greyLter padT5 txtCap'>" & OBDS.Tables(0).Rows(i)("Stops") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"




            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3 col-sm-3 col-xs-4 padL10 mobTxtRight'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='co12 db grey'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='ico20 db padT5 padB10'>" & MultiValueFunction_Dom(OBDS.Tables(0), "Arrall", i) & "<span class='fb mobdb'>" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & "</span></span>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='ico13 db greyLter lh1-2 mobdn'>" & OBDS.Tables(0).Rows(i)("ArrivalTerminal") & "</span>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div Class='bkHalfCircle bkHalfCircbot mobdn posAbs'></div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"

            ''''''''''''''''''''''''1st flight''''''''''''''''''''''''''''''''''''''''''''''''''
        Next
        If FT = "InBound" Then




            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='' style='padding:5px;'><span style='font-weight:600;font-size:17px;'>InBound</span></div>" '" & OBDS.Tables(0).Rows(0)("AdtFareType") & "

            For i As Integer = 0 To IBDS.Tables(0).Rows.Count - 1
                RBD = ""
                CABIN = ""
                If IBDS.Tables(0).Rows(i)("RBD").ToString().Trim() <> "" Then
                    RBD = "Class (" & IBDS.Tables(0).Rows(i)("RBD") & ")"
                End If
                If IBDS.Tables(0).Rows(i)("AdtCabin").ToString().Trim() <> "" Then
                    CABIN = "Cabin (" & CabinName(IBDS.Tables(0).Rows(i)("AdtCabin")) & ")"
                End If



                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div style='width: 100%;float: left;font-size: 15px;color: #000;'><i><img src='../Images/icons/flight_dep.png' style='width: 40px;'/></i>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & "-" & IBDS.Tables(0).Rows(i)("ArrivalLocation") & " | <span style='font-size:10px;'>" & IBDS.Tables(0).Rows(i)("Departure_Date") & "</span></div>"



                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row' style='margin: 32px;'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-5'><div class='col-md-2'><img alt='' src='../Airlogo/sm" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif' style='width: 50px;margin-left: -35px;'/></div> <div class='col-md-3' style='width: 80%;'><div class='row'>" & IBDS.Tables(0).Rows(i)("AirlineName") & " (" & IBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & IBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</div><div class='row'>" & RBD & "</div><div class='row'>" & CABIN & "</div></div></div>"



                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-2 col-xs-3'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='theme-search-results-item-flight-section-meta>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-time'>" & IBDS.Tables(0).Rows(i)("DepartureLocation") & "<span>" & MultiValueFunction_Dom(IBDS.Tables(0), "Depall", i) & "</span></p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-date'>" & IBDS.Tables(0).Rows(i)("Departure_Date") & "</p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-city'>" & IBDS.Tables(0).Rows(i)("DepartureTerminal") & "</p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"


                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3 col-xs-2'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly1'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span Class='jtm'>" & IBDS.Tables(0).Rows(i)("Tot_Dur") & "|" & IBDS.Tables(0).Rows(i)("Stops") & "</span>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly2'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='fart fart3'>" & IBDS.Tables(0).Rows(i)("AdtFareType") & "</span>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-2 col-xs-3' style='text-align: End;'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div Class='theme-search-results-item-flight-section-meta'>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-time'><span>" & MultiValueFunction_Dom(IBDS.Tables(0), "Arrall", i) & "</span>&nbsp;" & IBDS.Tables(0).Rows(i)("ArrivalLocation") & "</p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-date'>" & IBDS.Tables(0).Rows(i)("Arrival_Date") & "</p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-city'> " & IBDS.Tables(0).Rows(i)("ArrivalTerminal") & "</p>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"

                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

                'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
                'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"



                DepTerminal = ""
                ArrTerminal = ""

            Next
        End If
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


        Return FlightDtlsTotalInfo
    End Function
    Public Function CustFltDetails_Intl(ByVal OBDS As DataSet) As String
        Dim FlightDtlsTotalInfo As String = ""
        'Dim FlightType = ""
        'If FT = "InBound" Then
        '    FlightType = "OutBound"
        'End If
        Dim DepTerminal As String
        Dim ArrTerminal As String


        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fd-h'><i><img src='../Images/icons/plane-airport.png' style='width:34px;'/></i><span> Flight Details</span></div>" '" & OBDS.Tables(0).Rows(0)("AdtFareType") & "
        ''FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class=''><i><img src='../Images/icons/plane-airport.png' style='width:34px;'/></i><span style='font-weight:600;font-size:17px;'>Flight Details</span></div>" '" & OBDS.Tables(0).Rows(0)("AdtFareType") & "

        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<table  width='100%' border='0' cellspacing='0' cellpadding='0'>"
        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr ><td colspan='2' style='font-size:16px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91' >Flight Details</td><td align='left' style='font-size:14px; line-height:35px; border-bottom:2px solid #d1d1d1;color:#004b91; '></td><tr>" '" & OBDS.Tables(0).Rows(0)("AdtFareType") & "
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div>"

        For i As Integer = 0 To OBDS.Tables(0).Rows.Count - 1

            Dim RBD As String = ""
            Dim CABIN As String = ""
            If OBDS.Tables(0).Rows(i)("RBD").ToString().Trim() <> "" Then
                RBD = "Class (" & OBDS.Tables(0).Rows(i)("RBD") & ")"
            End If
            If OBDS.Tables(0).Rows(i)("AdtCabin").ToString().Trim() <> "" Then
                CABIN = "Cabin (" & CabinName(OBDS.Tables(0).Rows(i)("AdtCabin")) & ")"

            End If



            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row' style='margin: 7px;'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div style='width: 100%;float: left;font-size: 15px;color: #000;'><i><img src='../Images/icons/flight_dep.png' style='width: 40px;'/></i>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "-" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & " | <span style='font-size:10px;'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & "</span></div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row' style='margin: 32px;'>"
            If OBDS.Tables(0).Rows(i)("depdatelcc").ToString.Trim() <> "" Then
                'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td colspan='2'><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/> " & OBDS.Tables(0).Rows(i)("AirlineName") & "</td><td align='left' style='font-size:14px; line-height:35px;color:#004b91; '>" & OBDS.Tables(0).Rows(i)("AdtFareType") & "</td>"
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3'><div class='row'><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif'/></div> <div class='row'>" & OBDS.Tables(0).Rows(i)("AirlineName") & " (" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</div><div class='row'>" & RBD & "</div><div class='row'>" & CABIN & "</div></div>"

            Else
                FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-5'><div class='col-md-2'><img alt='' src='../Airlogo/sm" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & ".gif' style='width: 50px;margin-left: -35px;'/></div> <div class='col-md-3' style='width:80%;'><div class='row'>" & OBDS.Tables(0).Rows(i)("AirlineName") & " (" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</div><div class='row'>" & RBD & "</div><div class='row'>" & CABIN & "</div></div></div>"

            End If
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3'><div class='row' style='font-size: 22px;'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "&nbsp;<span style='font-weight:600;'>" & MultiValueFunction_Dom(OBDS.Tables(0), "Depall", i) & "</span></div><div class='row' style='width: 77%;height: 3px;border-bottom: 1px dotted #b0aeae;float: left;position: absolute;margin-left: -17px;'></div><div class='row' style='padding: 7px 0px 0px 0px;'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & " </div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row'>Terminal - " & OBDS.Tables(0).Rows(i)("DepartureTerminal") & "</div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-2 col-xs-3'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='theme-search-results-item-flight-section-meta>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-time'>" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "<span>" & MultiValueFunction_Dom(OBDS.Tables(0), "Depall", i) & "</span></p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-date'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & "</p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-city'>" & OBDS.Tables(0).Rows(i)("DepartureTerminal") & "</p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"


            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3'><div class='row'>" & OBDS.Tables(0).Rows(i)("Tot_Dur") & "H|" & OBDS.Tables(0).Rows(i)("Stops") & "</div><div class='row' style='width: 100%;height: 3px;border-bottom: 1px dotted #b0aeae;float: left;position: relative;margin-top: 15px;margin-left: -60px;'><div class='fli-i'><i><img src='../Images/icons/plane_img.png' style='width: 40px;position: relative;margin: auto;left: 66px;right: 0;top: -18px;    background-position: -265px -3px;'/></i></div></div> <div class='row'><span style='text-align: center;border: 1px solid #2dca1c;margin: 46px -23px auto;border-radius: 23px;color: #2dca1c;padding: 3px 0;font-size: 11px;display: block;width: 60%;'>" & OBDS.Tables(0).Rows(i)("AdtFareType") & "</span></div></div>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3 col-xs-2'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly1'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span Class='jtm'>" & OBDS.Tables(0).Rows(i)("Tot_Dur") & "H|" & OBDS.Tables(0).Rows(i)("Stops") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='fly2'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<span class='fart fart3'>" & OBDS.Tables(0).Rows(i)("AdtFareType") & "</span>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-3'><div class='row' style='font-size: 22px;'><span style='font-weight:600;'>" & MultiValueFunction_Dom(OBDS.Tables(0), "Arrall", i) & "</span>&nbsp;" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & " </div><div class='row' style='width: 100%;height: 3px;border-bottom: 1px dotted #b0aeae;float: left;position: absolute;margin-left: -70px;'></div><div class='row' style='padding: 7px 0px 0px 0px;'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & "</div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='row'>Terminal - " & OBDS.Tables(0).Rows(i)("ArrivalTerminal") & "</div></div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div class='col-md-2 col-xs-3' style='text-align: End;'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<div Class='theme-search-results-item-flight-section-meta'>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-time'><span>" & MultiValueFunction_Dom(OBDS.Tables(0), "Arrall", i) & "</span>&nbsp;" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & "</p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-date'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & "</p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<p class='theme-search-results-item-flight-section-meta-city'> " & OBDS.Tables(0).Rows(i)("ArrivalTerminal") & "</p>"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"
            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div >"



            FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>(" & OBDS.Tables(0).Rows(i)("MarketingCarrier") & "-" & OBDS.Tables(0).Rows(i)("FlightIdentification") & ")" & "</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Departure_Date") & " (" & MultiValueFunction_Intl(OBDS.Tables(0), "Depall", i) & ")</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td style='font-size:14px; font-weight:bold;'>" & OBDS.Tables(0).Rows(i)("Arrival_Date") & " (" & MultiValueFunction_Intl(OBDS.Tables(0), "Arrall", i) & ")</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"

            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & RBD & "</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("DepartureLocation") & "(" & OBDS.Tables(0).Rows(i)("DepartureCityName") & ")</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td >" & OBDS.Tables(0).Rows(i)("ArrivalLocation") & "(" & OBDS.Tables(0).Rows(i)("ArrivalCityName") & ")</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
            DepTerminal = ""
            ArrTerminal = ""
            If OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim() <> "" Then
                'DepTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("DepartureTerminal").ToString().Trim()
            End If
            If OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim() <> "" Then
                'ArrTerminal = "Terminal - " & OBDS.Tables(0).Rows(i)("ArrivalTerminal").ToString().Trim()
            End If
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<tr>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & CABIN & "</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & GetAirportName(OBDS.Tables(0).Rows(i)("DepartureLocation")) & " " & DepTerminal & "</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "<td>" & GetAirportName(OBDS.Tables(0).Rows(i)("ArrivalLocation")) & " " & ArrTerminal & "</td>"
            'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</tr>"
        Next
        FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"

        'FlightDtlsTotalInfo = FlightDtlsTotalInfo & "</div>"


        Return FlightDtlsTotalInfo
    End Function
    Private Function MultiValueFunction_Dom(ByVal dt As DataTable, ByVal Type As String, Optional ByVal Position As Int32 = 0, Optional ByVal dtrow As String = "") As String
        Dim OutputString As String = ""
        If (Type = "Logo") Then
            OutputString = "../Airlogo/sm" & dt.Rows(0)("MarketingCarrier") & ".gif"
        ElseIf Type = "Airline" Then
            OutputString = dt.Rows(0)("AirlineName") & "(" & dt.Rows(0)("MarketingCarrier") & "-" & dt.Rows(0)("FlightIdentification") & ")"
        ElseIf Type = "Dep" Then
            If (dt.Rows(0)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(0)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(0)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(0)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Deprow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If

        ElseIf Type = "Arrrow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If
        ElseIf Type = "Arr" Then
            If (dt.Rows(0)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Depall" Then
            If (dt.Rows(Position)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(Position)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Arrall" Then
            If (dt.Rows(Position)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(Position)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("ArrivalTime").ToString().Substring(2, 2)

            End If
        End If
        Return OutputString
    End Function
    Private Function MultiValueFunction_Intl(ByVal dt As DataTable, ByVal Type As String, Optional ByVal Position As Int32 = 0, Optional ByVal dtrow As String = "") As String
        Dim OutputString As String = ""
        If (Type = "Logo") Then
            OutputString = "../Airlogo/sm" & dt.Rows(0)("MarketingCarrier") & ".gif"
        ElseIf Type = "Airline" Then
            OutputString = dt.Rows(0)("AirlineName") & "(" & dt.Rows(0)("MarketingCarrier") & "-" & dt.Rows(0)("FlightIdentification") & ")"
        ElseIf Type = "Dep" Then
            If (dt.Rows(0)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(0)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(0)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(0)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Deprow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If

        ElseIf Type = "Arrrow" Then
            If (dtrow.Contains(":") = True) Then
                OutputString = dtrow

            Else
                OutputString = dtrow.Substring(0, 2) & ":" & dtrow.Substring(2, 2)

            End If
        ElseIf Type = "Arr" Then
            If (dt.Rows(0)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(dt.Rows.Count - 1)("ArrivalTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Depall" Then
            If (dt.Rows(Position)("DepartureTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("DepartureTime").ToString()

            Else
                OutputString = dt.Rows(Position)("DepartureTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("DepartureTime").ToString().Substring(2, 2)

            End If
        ElseIf Type = "Arrall" Then
            If (dt.Rows(Position)("ArrivalTime").ToString().Contains(":") = True) Then
                OutputString = dt.Rows(Position)("ArrivalTime").ToString()

            Else
                OutputString = dt.Rows(Position)("ArrivalTime").ToString().Substring(0, 2) & ":" & dt.Rows(Position)("ArrivalTime").ToString().Substring(2, 2)

            End If
        End If
        Return OutputString
    End Function
    Private Function CabinName(ByVal Cabin As String) As String
        Dim C As String = ""
        If Cabin.ToUpper().Trim() = "E" Or Cabin.ToUpper().Trim() = "Y" Then
            C = "ECONOMY"
        ElseIf Cabin.ToUpper().Trim() = "F" Then
            C = "FIRST"
        ElseIf Cabin.ToUpper().Trim() = "B" Then
            C = "BUSINESS"
        ElseIf Cabin.ToUpper().Trim() = "W" Then
            C = "Premium Economy"
        Else
            C = Cabin.ToUpper().Trim()
        End If
        Return C
    End Function

    'Ledger Entry for Hotel and other module
    Public Function LedgerEntry_Common(ByVal TRACKID As String, ByVal DebitAmount As Double, ByVal CreditAmount As Double, ByVal AvailBal As Double, ByVal VC As String, ByVal GDSPNR As String, ByVal TicketNo As String,
                                                     ByVal BookingType As String, ByVal AGENTID As String, ByVal AGENCYNAME As String, ByVal BookedBy As String, ByVal IP As String, ByVal ProjectId As String,
                                                     ByVal BillNo As String, ByVal EasyID As String, ByVal Remark As String, ByVal TRIP As String, ByVal PaxID As Integer) As Integer
        paramHashtable.Clear()
        paramHashtable.Add("@TRACKID", TRACKID)
        paramHashtable.Add("@DebitAmount", DebitAmount)
        paramHashtable.Add("@CreditAmount", CreditAmount)
        paramHashtable.Add("@Provider", VC)
        paramHashtable.Add("@PNRNO", GDSPNR)
        paramHashtable.Add("@TicketNo", TicketNo)
        paramHashtable.Add("@BookingType", BookingType)
        paramHashtable.Add("@AGENTID", AGENTID)
        paramHashtable.Add("@AGENCYNAME", AGENCYNAME)
        paramHashtable.Add("@IP", IP)
        paramHashtable.Add("@ProjectId", ProjectId)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@BillNo", BillNo)
        paramHashtable.Add("@AVAILBAL", AvailBal)
        paramHashtable.Add("@EasyID", EasyID)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@TRIP", TRIP)
        paramHashtable.Add("@PaxID", PaxID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_INSERT_LEDGER_CommonModule", 1)
    End Function
    'Update Ledger by paxId

    Public Function GetTicketingProvider(ByVal orderID As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", orderID)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "usp_Get_TicketingProvider", 3)
    End Function


    Public Function CheckAgentUserIdEmailMobile(ByVal UserId As String, ByVal email As String, ByVal mobile As String, ByVal panno As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        paramHashtable.Add("@EmailId", email)
        paramHashtable.Add("@Mobile", mobile)
        paramHashtable.Add("@PanNo", panno)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckAgentUserIdEmailMobile", 3)
    End Function
    Public Function CheckAgentUserId(ByVal UserId As String, ByVal Action As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@UserId", UserId)
        paramHashtable.Add("@Action", Action)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "CheckAgentUserId", 3)
    End Function

    Public Function StaffTransactionHistory(ByVal OrderId As String, ByVal ServiceType As String, ByVal TransAmount As Double, ByVal StaffUserId As String, ByVal OwnerId As String, ByVal IPAddress As String, ByVal Remark As String, ByVal CreatedBy As String, ByVal DebitCredit As String, ByVal ModuleType As String, ByVal ActionType As String) As Integer
        paramHashtable.Clear()
        'Dim IPAddress As String = ""
        'IPAddress = Request.UserHostAddress
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@ServiceType", ServiceType)
        paramHashtable.Add("@TransAmount", TransAmount)
        paramHashtable.Add("@StaffUserId", StaffUserId)
        paramHashtable.Add("@OwnerId", OwnerId)
        paramHashtable.Add("@IPAddress", IPAddress)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@CreatedBy", CreatedBy)
        paramHashtable.Add("@DebitCredit", DebitCredit)
        paramHashtable.Add("@Module", ModuleType)
        paramHashtable.Add("@ActionType", ActionType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_Insert_StaffTransaction", 1)
    End Function

    Public Function CheckStaffBalanceAndBalanceDeduct(ByVal OrderId As String, ByVal ServiceType As String, ByVal TransAmount As Double, ByVal StaffUserId As String, ByVal OwnerId As String, ByVal IPAddress As String, ByVal Remark As String, ByVal CreatedBy As String, ByVal DebitCredit As String, ByVal ModuleType As String, ByVal ActionType As String) As DataSet
        paramHashtable.Clear()
        '@ActionType='CHECKBAL-DEDUCT'),@ServiceType='FLIGHT',@DebitCredit='DEBIT',@DebitCredit='CREDIT'
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@ServiceType", ServiceType)
        paramHashtable.Add("@TransAmount", TransAmount)
        paramHashtable.Add("@StaffUserId", StaffUserId)
        paramHashtable.Add("@OwnerId", OwnerId)
        paramHashtable.Add("@IPAddress", IPAddress)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@CreatedBy", CreatedBy)
        paramHashtable.Add("@DebitCredit", DebitCredit)
        paramHashtable.Add("@Module", ModuleType)
        paramHashtable.Add("@ActionType", ActionType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_Insert_StaffTransaction", 3)
    End Function

    Public Function GetStaffTransaction(ByVal UserType As String, ByVal StaffUserId As String, ByVal FormDate As String, ByVal ToDate As String, ByVal AgentId As String, ByVal sModule As String, ByVal ServiceType As String, ByVal SearchType As String, ByVal OrderId As String) As DataSet
        paramHashtable.Clear()
        'UserType,StaffUserId,FormDate,ToDate,AgentId,sModule,ServiceType,OrderId,SearchType
        paramHashtable.Add("@UserType", UserType)
        paramHashtable.Add("@StaffUserId", StaffUserId)
        paramHashtable.Add("@FormDate", FormDate)
        paramHashtable.Add("@ToDate", ToDate)
        paramHashtable.Add("@AgentId", AgentId)
        paramHashtable.Add("@Module", sModule)
        paramHashtable.Add("@ServiceType", ServiceType)
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@SearchType", SearchType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetStaffTransaction", 3)
    End Function

    Public Function StaffTransaction(ByVal OrderId As String, ByVal ServiceType As String, ByVal TransAmount As Double, ByVal StaffUserId As String, ByVal OwnerId As String, ByVal IPAddress As String, ByVal Remark As String, ByVal CreatedBy As String, ByVal DebitCredit As String, ByVal ModuleType As String, ByVal ActionType As String) As Integer
        paramHashtable.Clear()
        'Dim IPAddress As String = ""
        'IPAddress = Request.UserHostAddress
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@ServiceType", ServiceType)
        paramHashtable.Add("@TransAmount", TransAmount)
        paramHashtable.Add("@StaffUserId", StaffUserId)
        paramHashtable.Add("@OwnerId", OwnerId)
        paramHashtable.Add("@IPAddress", IPAddress)
        paramHashtable.Add("@Remark", Remark)
        paramHashtable.Add("@CreatedBy", CreatedBy)
        paramHashtable.Add("@DebitCredit", DebitCredit)
        paramHashtable.Add("@Module", ModuleType)
        paramHashtable.Add("@ActionType", ActionType)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_StaffAmountDebitCredit", 1)
    End Function
End Class



