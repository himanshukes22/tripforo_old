'Imports System.Web
'Imports System.Web.Services
'Imports System.Web.Services.Protocols
'Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Collections.Generic
'Imports System.Web.Script.Services
'Imports System.Net
'Imports System.IO
'Imports System.Security.Cryptography.X509Certificates
'Imports YatraBilling
'Imports System.Security.Cryptography


'' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'' <System.Web.Script.Services.ScriptService()> _
'<WebService(Namespace:="http://tempuri.org/")> _
'<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
'<ScriptService()> _
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
'Public Class WebService
'    Inherits System.Web.Services.WebService


'    Private Function SendReceive(ByVal strRequest As String) As String
'        Dim responseFromServer As String = ""
'        Try
'            Dim url As String = "https://osg.oximall.com/transservice.aspx"
'            Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)

'            Dim CertificatePath As String = "C:\Inetpub\spring\OxiCerificate\osg.oximall.com.cer"

'            If Not String.IsNullOrEmpty(CertificatePath.Trim()) Then
'                Dim cert As New X509Certificate()
'                cert = X509Certificate.CreateFromCertFile(CertificatePath)
'                Dim sdf As String = cert.GetSerialNumberString()
'                objRequest.ClientCertificates.Add(cert)
'                objRequest.Credentials = CredentialCache.DefaultCredentials
'                objRequest.KeepAlive = False
'            End If
'            ServicePointManager.CertificatePolicy = New AcceptAllCertificatePolicy()

'            'string authkey = "Z2l0ZWNobm9sb2d5OmdpdEBveGlnZW4xMjM=";
'            Dim authkey As String = "U3ByaW5nVHJ2OlNwcmluZ1RydkBveGkxMjM=" ' "U0JJOm94aWdlbkAxMjM="
'            ' string RequestStr = "Transid=" + DateTime.Now.ToString("ddhhmmss") + "&merchantrefno=" + "ETOP*VODA*DEL,9953704190" + "&amount=" + "100" + "&requestdate=" + DateTime.Now.ToString("yyyyMMddhhmmss") + "&status=" + "0" + "&bankrefno=" + DateTime.Now.ToString("mmhhss");
'            Dim RequestStr As String = strRequest
'            Dim byteArray As Byte() = StrToByteArray(RequestStr)

'            objRequest.Method = "POST"
'            objRequest.Timeout = 3600 * 1000
'            objRequest.ReadWriteTimeout = 3600 * 1000
'            objRequest.ContentLength = byteArray.Length

'            objRequest.Headers.Add("Authorization:Basic " & authkey)
'            'objRequest.KeepAlive = False
'            objRequest.ContentType = "application/x-www-form-urlencoded"
'            objRequest.Credentials = CredentialCache.DefaultCredentials

'            Dim reqStream As Stream = objRequest.GetRequestStream()
'            reqStream.Write(byteArray, 0, byteArray.Length)
'            reqStream.Close()
'            Dim b As [Boolean] = objRequest.HaveResponse

'            Dim StatusResponse As [String]
'            '****************** Get Response *************************//
'            Dim response__1 As HttpWebResponse = DirectCast(objRequest.GetResponse(), HttpWebResponse)
'            Dim dataStream As System.IO.Stream = Nothing
'            dataStream = response__1.GetResponseStream()
'            Dim reader As New StreamReader(dataStream)
'            StatusResponse = response__1.StatusCode.ToString()
'            responseFromServer = reader.ReadToEnd()
'            reader.Close()
'            dataStream.Close()
'            response__1.Close()


'        Catch ex As Exception
'            WriteToEventLog(ex)
'            responseFromServer = ""
'        End Try

'        Return responseFromServer
'    End Function
'    <WebMethod()> _
'  Public Function FetchCommission(ByVal agID As String, ByVal service As String, ByVal amount As String) As String
'        Dim objConn As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim objCmd As New SqlCommand
'        Dim objDS As New DataSet
'        Try
'            objConn.Open()
'            objCmd.Connection = objConn

'            objCmd.CommandText = "SP_FETCH_COMMISSION"
'            objCmd.CommandType = CommandType.StoredProcedure
'            objCmd.Parameters.Add("@AGENTID", SqlDbType.VarChar).Value = agID
'            objCmd.Parameters.Add("@SERVICE", SqlDbType.VarChar).Value = service
'            objCmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount
'            Dim objDA As New SqlDataAdapter(objCmd)
'            objDA.Fill(objDS)

'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally

'            objConn.Close()
'            objConn.Dispose()
'            objCmd.Dispose()
'        End Try

'        Return objDS.Tables(0).Rows(0)("oxiCOMMISSION").ToString() & "," & objDS.Tables(0).Rows(0)("sprCOMMISSION").ToString()
'    End Function
'    <WebMethod()> _
'    Public Function FetchState(ByVal oprtCOde As String) As List(Of Utl)
'        Dim objConn As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim objCmd As New SqlCommand
'        Dim objDS As New DataSet
'        Try
'            objConn.Open()
'            objCmd.Connection = objConn
'            objCmd.CommandText = "SP_FETCH_STATE"
'            objCmd.CommandType = CommandType.StoredProcedure
'            objCmd.Parameters.Add("@OPERATORALIAS", SqlDbType.VarChar).Value = oprtCOde
'            Dim objDA As New SqlDataAdapter(objCmd)
'            objDA.Fill(objDS)

'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally

'            objConn.Close()
'            objConn.Dispose()
'            objCmd.Dispose()
'        End Try

'        Return GetStateList(objDS.Tables(0))

'    End Function

'    <WebMethod()> _
'   Public Function FetchAmount(ByVal IDVal As String) As List(Of amount)
'        Dim objConn As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim objCmd As New SqlCommand
'        Dim objDS As New DataSet
'        Try
'            objConn.Open()
'            objCmd.Connection = objConn
'            objCmd.CommandText = "SP_FETCH_AMOUNT"
'            objCmd.CommandType = CommandType.StoredProcedure
'            objCmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = IDVal
'            Dim objDA As New SqlDataAdapter(objCmd)
'            objDA.Fill(objDS)

'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally

'            objConn.Close()
'            objConn.Dispose()
'            objCmd.Dispose()
'        End Try

'        Return GetAmountList(objDS.Tables(0))

'    End Function

'    <WebMethod()> _
'    Public Function FetchDthAmount(ByVal DthOperator As String) As List(Of [String])
'        Dim list As New List(Of String)()
'        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim cmd As New SqlCommand("SP_FETCH_DTH_AMOUNT", con)
'        Try
'            Dim dt As DataTable

'            con.Open()

'            cmd.CommandText = "SP_FETCH_DTH_AMOUNT"
'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@operator", DthOperator)
'            Dim da As New SqlDataAdapter(cmd)
'            dt = New DataTable()
'            da.Fill(dt)


'            For i As Integer = 0 To dt.Rows.Count - 1
'                list.Add(dt.Rows(i)("AMOUNT").ToString())
'            Next
'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally
'            con.Close()
'            cmd.Dispose()
'            con.Dispose()
'        End Try


'        Return list
'    End Function
'    Public Function RandomNumber() As Integer
'        Static RandomNumGen As New System.Random
'        Return RandomNumGen.Next()
'    End Function
'    Private Function PostXml(ByVal url As String, ByVal strRequest As String) As String
'        Dim bytes As Byte() = UTF8Encoding.UTF8.GetBytes(strRequest)
'        Dim strResult As String = String.Empty
'        Try
'            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
'            request.Method = "POST"
'            request.ContentLength = bytes.Length
'            request.ContentType = "application/x-www-form-urlencoded"
'            request.KeepAlive = False
'            request.Timeout = 3600 * 1000
'            request.ReadWriteTimeout = 3600 * 1000
'            Using requestStream As Stream = request.GetRequestStream()
'                requestStream.Write(bytes, 0, bytes.Length)
'            End Using
'            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
'                If response.StatusCode <> HttpStatusCode.OK Then
'                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", response.StatusCode)
'                    Throw New ApplicationException(message)
'                Else

'                    Dim reader As StreamReader = Nothing
'                    Dim responseStream As Stream = response.GetResponseStream()
'                    reader = New StreamReader(responseStream)
'                    strResult = reader.ReadToEnd()
'                    response.Close()
'                    responseStream.Close()
'                    reader.Close()
'                End If
'            End Using
'        Catch ex As Exception
'        End Try
'        Return strResult
'    End Function

'    'Write by Ravi to Save Dth Transaction Details start
'    Private Function Generate() As String
'        Dim strRndKey As String = ""
'        strRndKey = RNGCharacterMask() ''UsingGuid() &
'        Return strRndKey
'    End Function

'    Private Function UsingGuid() As String
'        Dim result As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
'        Return result
'    End Function

'    Private Function RNGCharacterMask() As String
'        Dim maxSize As Integer = 15
'        Dim minSize As Integer = 15
'        Dim chars As Char() = New Char(61) {}
'        Dim a As String
'        a = "1234567890" ''abcdefghijklmnopqrstuvwxyz
'        chars = a.ToCharArray()
'        Dim size As Integer = maxSize
'        Dim data As Byte() = New Byte(0) {}
'        Dim crypto As New RNGCryptoServiceProvider()
'        crypto.GetNonZeroBytes(data)
'        size = maxSize
'        data = New Byte(size - 1) {}
'        crypto.GetNonZeroBytes(data)
'        Dim result As New StringBuilder(size)
'        For Each b As Byte In data
'            result.Append(chars(b Mod (chars.Length - 1)))
'        Next
'        Return result.ToString()
'    End Function

'    <WebMethod()> _
'Public Function InsertDth(ByVal Operator1 As String, ByVal State As String, ByVal AgentId As String, ByVal Service As String, ByVal Amount As String, _
'  ByVal Smsto As String, ByVal ProductNo As String, ByVal RechargeType As String, ByVal RequestXml As String, ByVal ResponseXml As String) As String
'        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ConnectionString)
'        Dim cmd As New SqlCommand("Sp_InsertDTH_Transaction", con)
'        Dim strMessage As String = ""
'        Try
'            Dim orderid As String = Generate() '(DateTime.Now.Day & DateTime.Now.Month & DateTime.Now.Year & DateTime.Now.Hour & DateTime.Now.Minute & RandomNumber().ToString.Substring(2, 2))
'            Dim bankrefno As String = Generate() '(DateTime.Now.Day & DateTime.Now.Month & DateTime.Now.Year & DateTime.Now.Hour & DateTime.Now.Minute & RandomNumber().ToString.Substring(2, 2))

'            con.Open()

'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@OrderId", orderid)
'            cmd.Parameters.AddWithValue("@State", State)
'            cmd.Parameters.AddWithValue("@Operator", Operator1)
'            cmd.Parameters.AddWithValue("@AgentId", AgentId)
'            cmd.Parameters.AddWithValue("@Service", Service)
'            cmd.Parameters.AddWithValue("@Amount", Amount)
'            cmd.Parameters.AddWithValue("@Bankrefno", bankrefno)
'            cmd.Parameters.AddWithValue("@Smsto", Smsto)
'            cmd.Parameters.AddWithValue("@ProductNo", ProductNo)
'            cmd.Parameters.AddWithValue("@RechargeType", RechargeType)
'            cmd.Parameters.AddWithValue("@RequestXml", "")
'            cmd.Parameters.AddWithValue("@ResponseXml", ResponseXml)
'            cmd.Parameters.AddWithValue("@Status", "PROCCESS")
'            Dim i As Integer = cmd.ExecuteNonQuery()
'            Dim strRequest As String = FetchRequest(orderid)

'            If strRequest <> "insuficient balance" Then
'                'Dim strResponse As String = SendReceive(strRequest)
'                Dim strResponse As String = ""
'                If (strRequest.IndexOf("Transid") >= 0) Then
'                    strResponse = SendReceive(strRequest)
'                Else
'                    strResponse = PostXml("http://123.108.34.245/Integration/RechargeService", strRequest)
'                End If
'                'Ytra billing Object
'                Dim Misobj As New MISCLLENEOUS_YATRA()
'                'Ytra billing Object end

'                Select Case RechargeType
'                    Case "TopUp"
'                        If Left(strResponse, 1) = 0 And InStr(strResponse.ToLower.Trim.ToString, "successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = " Recharge Succefully....<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end

'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Recharge failed....<br/>Your transanction order no is:" & orderid
'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Recharge status is pending...<br/>Your tranction order no is:" & orderid
'                            End If
'                        End If

'                    Case "Voucher"
'                        If Left(strResponse, 1) = 0 And strResponse.IndexOf("Transaction Successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = " Recharge successfully..Please check<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end
'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Recharge failed....<br/>Your transanction order no is:" & orderid

'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Recharge status is pending...<br/>Your tranction order no is:" & orderid

'                            End If
'                        End If

'                    Case "Bill"
'                        If Left(strResponse, 1) = 0 And strResponse.IndexOf("Transaction Successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = "Bill payment successfully..Please check<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end
'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Bill payment failed....<br/>Your transanction order no is:" & orderid

'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Bill payment is pending...<br/>Your tranction order no is:" & orderid

'                            End If
'                        End If


'                End Select
'            Else
'                strMessage = strRequest
'            End If


'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally
'            con.Close()
'            con.Dispose()
'            cmd.Dispose()
'        End Try

'        Return strMessage
'        'Write by Ravi to Save Dth Transaction Details end

'    End Function

'    <WebMethod()> _
'Public Function InsertBill(ByVal sprCom As String, ByVal oxiCom As String, ByVal Operator1 As String, ByVal State As String, ByVal AgentId As String, ByVal Service As String, ByVal Amount As String, _
' ByVal Smsto As String, ByVal ProductNo As String, ByVal RechargeType As String, ByVal RequestXml As String, ByVal ResponseXml As String) As String
'        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ConnectionString)
'        Dim cmd As New SqlCommand("Sp_InsertBill_Transaction", con)
'        Dim strMessage As String = ""
'        Try
'            Dim orderid As String = Generate() '(DateTime.Now.Day & DateTime.Now.Month & DateTime.Now.Year & DateTime.Now.Hour & DateTime.Now.Minute & RandomNumber().ToString.Substring(2, 2))
'            Dim bankrefno As String = Generate() '(DateTime.Now.Day & DateTime.Now.Month & DateTime.Now.Year & DateTime.Now.Hour & DateTime.Now.Minute & RandomNumber().ToString.Substring(2, 2))

'            con.Open()

'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@sprComm", sprCom)
'            cmd.Parameters.AddWithValue("@oxiComm", oxiCom)
'            cmd.Parameters.AddWithValue("@OrderId", orderid)
'            cmd.Parameters.AddWithValue("@State", State)
'            cmd.Parameters.AddWithValue("@Operator", Operator1)
'            cmd.Parameters.AddWithValue("@AgentId", AgentId)
'            cmd.Parameters.AddWithValue("@Service", Service)
'            cmd.Parameters.AddWithValue("@Amount", Amount)
'            cmd.Parameters.AddWithValue("@Bankrefno", bankrefno)
'            cmd.Parameters.AddWithValue("@Smsto", Smsto)
'            cmd.Parameters.AddWithValue("@ProductNo", ProductNo)
'            cmd.Parameters.AddWithValue("@RechargeType", RechargeType)
'            cmd.Parameters.AddWithValue("@RequestXml", "")
'            cmd.Parameters.AddWithValue("@ResponseXml", ResponseXml)
'            cmd.Parameters.AddWithValue("@Status", "PROCCESS")
'            Dim i As Integer = cmd.ExecuteNonQuery()

'            Dim strRequest As String = FetchRequest(orderid)

'            If strRequest <> "insuficient balance" Then
'                Dim strResponse As String = SendReceive(strRequest)
'                'Ytra billing Object
'                Dim Misobj As New MISCLLENEOUS_YATRA()
'                'Ytra billing Object end
'                Select Case RechargeType
'                    Case "TopUp"
'                        If Left(strResponse, 1) = 0 And strResponse.IndexOf("Transaction Successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = " Recharge Succefully....<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end
'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Recharge failed....<br/>Your transanction order no is:" & orderid
'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Recharge status is pending...<br/>Your tranction order no is:" & orderid
'                            End If
'                        End If

'                    Case "Voucher"
'                        If Left(strResponse, 1) = 0 And strResponse.IndexOf("Transaction Successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = " Recharge successfully..Please check<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end
'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Recharge failed....<br/>Your transanction order no is:" & orderid

'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Recharge status is pending...<br/>Your tranction order no is:" & orderid

'                            End If
'                        End If

'                    Case "Bill"
'                        If Left(strResponse, 1) = 0 And strResponse.IndexOf("Transaction Successful") > 0 Then
'                            Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                            cmd1.CommandType = CommandType.StoredProcedure
'                            cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                            cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                            cmd1.Parameters.AddWithValue("@STATUS", "COMPLETED")
'                            Dim k As Integer = cmd1.ExecuteNonQuery()
'                            strMessage = "Bill payment successfully..Please check<br/>Your transanction order no is:" & orderid
'                            'Ytra billing 
'                            Try
'                                Misobj.ProcessYatra_WyInsurance(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'Ytra billing end
'                            'NAV METHOD  CALL START
'                            Try
'                                Dim objNavoxi As New UtilityService.clsRecharge(orderid, "recharge")
'                                objNavoxi.pushRechargeUtilityToNav(orderid, "")
'                            Catch ex As Exception

'                            End Try
'                            'NAV METHOD  CALL END
'                        Else
'                            If (strResponse <> "") Then
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "FAILED")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                Dim strResponseArr = strResponse.Split("|")
'                                strMessage = ".....Bill payment failed....<br/>Your transanction order no is:" & orderid

'                            Else
'                                Dim cmd1 As New SqlCommand("SP_UPDATE_RESPONSE", con)
'                                cmd1.CommandType = CommandType.StoredProcedure
'                                cmd1.Parameters.AddWithValue("@OrderId", orderid)
'                                cmd1.Parameters.AddWithValue("@ResponseXml", strResponse)
'                                cmd1.Parameters.AddWithValue("@STATUS", "PENDING")
'                                Dim k As Integer = cmd1.ExecuteNonQuery()
'                                strMessage = "Bill payment is pending...<br/>Your tranction order no is:" & orderid

'                            End If
'                        End If


'                End Select
'            Else
'                strMessage = strRequest
'            End If


'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally
'            con.Close()
'            con.Dispose()
'            cmd.Dispose()
'        End Try

'        Return strMessage
'        'Write by Ravi to Save Dth Transaction Details end

'    End Function

'    Public Shared Function StrToByteArray(ByVal str As String) As Byte()
'        Dim encoding As New System.Text.ASCIIEncoding()
'        Return encoding.GetBytes(str)
'    End Function

'    Public Shared Function GetStateList(ByVal dt As DataTable) As List(Of Utl)
'        Dim StateList As New List(Of Utl)()
'        For i As Integer = 0 To dt.Rows.Count - 1
'            StateList.Add(New Utl() With {.StateName = dt.Rows(i)("STATENAME").ToString().Trim(), .StateCode = dt.Rows(i)("STATEID").ToString().Trim()})
'        Next
'        Return StateList
'    End Function

'    Public Shared Function GetAmountList(ByVal dt As DataTable) As List(Of amount)
'        Dim AmountList As New List(Of amount)()
'        For i As Integer = 0 To dt.Rows.Count - 1
'            AmountList.Add(New amount() With {.ID = dt.Rows(i)("ID").ToString().Trim(), .Amount = dt.Rows(i)("AMOUNT").ToString().Trim()})
'        Next
'        Return AmountList
'    End Function


'    Public Function FetchRequest(ByVal orderID As String) As String

'        Dim objConn As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim objCmd As New SqlCommand
'        Dim objDS As New DataSet
'        Try
'            objConn.Open()
'            objCmd.Connection = objConn
'            objCmd.CommandText = "SP_FETCH_REQUEST"
'            objCmd.CommandType = CommandType.StoredProcedure
'            objCmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = orderID
'            Dim objDA As New SqlDataAdapter(objCmd)
'            objDA.Fill(objDS)

'        Catch ex As Exception
'            WriteToEventLog(ex)
'        Finally

'            objConn.Close()
'            objConn.Dispose()
'            objCmd.Dispose()
'        End Try

'        Return objDS.Tables(0).Rows(0)("REQUESTXML").ToString()

'    End Function
'    Public Shared Sub WriteToEventLog(ByVal excep As Exception)
'        Dim connectionString As String = ConfigurationManager.ConnectionStrings("oxicase").ConnectionString
'        Dim ErrorMessgage As String = excep.Message

'        Dim trace As Diagnostics.StackTrace = New Diagnostics.StackTrace(excep, True)
'        Dim pagename As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
'        Dim method As String = trace.GetFrame((trace.FrameCount - 1)).GetMethod().ToString()
'        Dim lineNumber As Int32 = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
'        Dim con As New SqlConnection(connectionString)
'        con.Open()
'        Dim cmd As New SqlCommand("sp_InsertException_Oxicash", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@pagename", pagename)
'        cmd.Parameters.AddWithValue("@method", method)
'        cmd.Parameters.AddWithValue("@Line_number", lineNumber)
'        cmd.Parameters.AddWithValue("@error_msg", ErrorMessgage)
'        cmd.ExecuteNonQuery()
'        con.Close()

'    End Sub
'End Class
'Public Class Utl
'    Public Property StateName() As String
'        Get
'            Return m_StateName
'        End Get
'        Set(ByVal value As String)
'            m_StateName = value
'        End Set
'    End Property
'    Private m_StateName As String
'    Public Property StateCode() As String
'        Get
'            Return m_StateCode
'        End Get
'        Set(ByVal value As String)
'            m_StateCode = value
'        End Set
'    End Property
'    Private m_StateCode As String



'    '
'    ' TODO: Add constructor logic here
'    '
'    Public Sub New()
'    End Sub
'    Public Shared Function FetchState(ByVal oprtCOde As String) As List(Of Utl)
'        Dim objConn As New SqlConnection(ConfigurationManager.ConnectionStrings("oxicase").ToString())
'        Dim objCmd As New SqlCommand
'        Dim objDS As New DataSet
'        Try
'            objConn.Open()
'            objCmd.Connection = objConn
'            objCmd.CommandText = "SP_FETCH_STATE"
'            objCmd.CommandType = CommandType.StoredProcedure
'            objCmd.Parameters.Add("@OPERATORALIAS", SqlDbType.VarChar).Value = oprtCOde
'            Dim objDA As New SqlDataAdapter(objCmd)
'            objDA.Fill(objDS)

'        Catch ex As Exception
'        Finally

'            objConn.Close()
'            objConn.Dispose()
'            objCmd.Dispose()
'        End Try

'        Return GetStateList(objDS.Tables(0))

'    End Function


'    Public Shared Function GetStateList(ByVal dt As DataTable) As List(Of Utl)
'        Dim StateList As New List(Of Utl)()
'        For i As Integer = 0 To dt.Rows.Count - 1
'            StateList.Add(New Utl() With {.StateName = dt.Rows(i)("STATENAME").ToString().Trim(), .StateCode = dt.Rows(i)("STATEID").ToString().Trim()})
'        Next
'        Return StateList
'    End Function


'End Class
'Public Class AcceptAllCertificatePolicy
'    Implements ICertificatePolicy
'    Public Sub New()
'    End Sub
'    Public Function CheckValidationResult(ByVal sPoint As ServicePoint, ByVal cert As X509Certificate, ByVal wRequest As WebRequest, ByVal certProb As Integer) As Boolean Implements ICertificatePolicy.CheckValidationResult
'        Return True
'    End Function

'End Class
'Public Class amount
'    Public Property ID() As String
'        Get
'            Return m_id
'        End Get
'        Set(ByVal value As String)
'            m_id = value
'        End Set
'    End Property
'    Private m_id As String
'    Public Property Amount() As String
'        Get
'            Return m_Amount
'        End Get
'        Set(ByVal value As String)
'            m_Amount = value
'        End Set
'    End Property
'    Private m_Amount As String

'End Class