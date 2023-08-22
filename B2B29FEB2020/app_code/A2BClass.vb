Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
'using Newtonsoft.Json;
Imports System.Web.Script.Serialization
Imports System.Configuration
Imports System.Data
'using SharedBAL;
Imports System.Xml
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Namespace ITZA2B
    Public Class A2BClass
        Dim A2BPassword As String = ConfigurationManager.AppSettings("A2BPassword").ToString()
        Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
        'using ITZShared;
        'using Newtonsoft.Json.Linq;

        '/ <summary>
        '/ Summary description for Class1
        '/ </summary>

        'Public Class A2BClass
      
     
        Public Function getEnquiryRequest(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "search_request.asp?" + Url + "&login=" + A2BPassword)
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
                Dim i As Integer = Updatelogs("search_request", A2BHostUrl + "search_request.asp?" + Url + "&login=" + A2BPassword, sResponse)
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getEnquiryResponse(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "booking_request.asp?Session_ID=" + Url + "&login=" + A2BPassword)
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
                Dim i As Integer = Updatelogs("booking_request", A2BHostUrl + "booking_request.asp?" + Url + "&login=" + A2BPassword, sResponse)
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getEnquirycancelResponse(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "cancel_request.asp?BookRef=" + Url + "&login=" + A2BPassword)
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
                Dim i As Integer = Updatelogs("cancel_request", A2BHostUrl + "cancel_request.asp?" + Url + "&login=" + A2BPassword, sResponse)
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getEnquirypickupResponse(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "pickup_time_request.asp?BookRef=" + Url + "&login=" + A2BPassword)
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
                Dim i As Integer = Updatelogs("pickup_time_request", A2BHostUrl + "pickup_time_request.asp?" + Url + "&login=" + A2BPassword, sResponse)
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getEnquiryamendResponse(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "booking_amend.asp?BookRef=" + Url + "&login=" + A2BPassword)
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
                Dim i As Integer = Updatelogs("booking_amend", A2BHostUrl + "booking_amend.asp?" + Url + "&login=" + A2BPassword, sResponse)
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getEnquirytransfertypeResponse(ByVal Url As String) As String
            Dim A2BHostUrl As String = ConfigurationManager.AppSettings("A2BHostUrl").ToString()
            Dim sResponse As String = String.Empty

            'Dim _StrmR As StreamReader
            Try
                sResponse = (A2BHostUrl + "type_request.asp?" + Url + "&login=" + A2BPassword)
                'Dim _BeamReq As WebRequest = getFinalizedObject(A2BHostUrl + "type_request.asp?" + Url + "&login=" + A2BPassword)
                'Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                '_StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                ' sResponse = _BeamReq
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        Public Function getCurrencyEnquiryResponse(ByVal Rate As String) As String
            ''   Dim A2BCurrencyUrl As String = ("http://rate-exchange.appspot.com/currency?from=USD&to=INR&q=")
            Dim sResponse As String = String.Empty
            Dim _StrmR As StreamReader
            Try
                ''  Dim _BeamReq As WebRequest = getFinalizedObject(A2BCurrencyUrl + Rate)

                Dim _BeamReq As WebRequest = getFinalizedObject("http://www.google.com/finance/converter?a=" + Rate + "&from=USD&to=INR")
                Dim _A2B_resp As WebResponse = _BeamReq.GetResponse()
                _StrmR = New StreamReader(_A2B_resp.GetResponseStream())
                sResponse = _StrmR.ReadToEnd()
            Catch ex As WebException

            End Try
            Return sResponse
        End Function
        'Public Shared Function getFinalizedObject(ByVal _ServiceURL As String) As WebRequest
        '    Dim _MyReq As WebRequest =  WebRequest.CreateCType(as HttpWebRequest, _ServiceURL)
        '    _MyReq.Method = "GET"
        '    _MyReq.Timeout = 3600 * 1000
        '    _MyReq.Headers.Add("SOAPAction", _ServiceURL + "?wsdl")
        '    Return _MyReq
        'End Function
        Public Shared Function getFinalizedObject(ByVal _ServiceURL As String) As WebRequest
            Dim _MyReq As WebRequest = CType(WebRequest.Create(_ServiceURL), HttpWebRequest)
            _MyReq.Method = "GET"
            _MyReq.Timeout = (3600 * 1000)
            _MyReq.Headers.Add("SOAPAction", (_ServiceURL + "?wsdl"))
            Return _MyReq
        End Function


#Region "Corency Converter from Google API"
        Public Function CurrancyConvert_USD_To_INR(ByVal currencytype As String) As Decimal
            ''Dim objhtlDa As New HotelDA()
            Try
                Dim web As New WebClient()
                Dim url As String = String.Format("http://www.google.com/finance/converter?a=1&from=" + currencytype + "&to=INR")
                Dim databuffer As Byte() = Encoding.ASCII.GetBytes("test=postvar&test2=another")
                Dim _webreqquest As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
                _webreqquest.Method = "POST"
                _webreqquest.ContentType = "application/x-www-form-urlencoded"
                _webreqquest.Timeout = 11000
                _webreqquest.ContentLength = databuffer.Length
                Dim PostData As Stream = _webreqquest.GetRequestStream()
                PostData.Write(databuffer, 0, databuffer.Length)
                PostData.Close()
                Dim WebResp As HttpWebResponse = DirectCast(_webreqquest.GetResponse(), HttpWebResponse)
                Dim finalanswer As Stream = WebResp.GetResponseStream()
                Dim _answer As New StreamReader(finalanswer)
                Dim value As String() = Regex.Split(_answer.ReadToEnd(), "&nbsp;")
                Dim a = Regex.Split(value(1), "<div id=currency_converter_result>1 " + currencytype + " = <span class=bld>")
                Dim rate As Decimal = 0
                Try
                    rate = Convert.ToDecimal(Regex.Split(a(1), " INR</span>")(0).Trim())
                    Dim i As Integer = UpdateCurrancyValue(currencytype, rate)
                Catch ex As Exception

                End Try
                Return rate
            Catch ex As Exception
                Dim curval As Decimal = 0
                curval = SelectCurrancyValue(currencytype)
                If curval > 0 Then
                    Return curval
                Else
                    Return CurrancyConvert_USD_To_INR(currencytype)
                End If
            End Try
        End Function
#End Region



        Public Function Updatelogs(ByVal RequestTYPE As String, ByVal REQUEST As String, ByVal RESPONSE As String)
            Try
                Dim cmd As SqlCommand
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
                cmd = New SqlCommand("SP_INSERTLOGS", con)
                con.Open()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@RequestTYPE", RequestTYPE.ToString())
                cmd.Parameters.AddWithValue("@REQUEST", REQUEST)
                cmd.Parameters.AddWithValue("@RESPONSE", RESPONSE)
                cmd.Parameters.AddWithValue("@provider", "A2B")
                cmd.ExecuteNonQuery()
                con.Close()
                cmd.Dispose()
            Catch ex As Exception
            End Try

        End Function

        Public Function UpdateCurrancyValue(ByVal currencytype As String, ByVal rate As Decimal)
            Try
                Dim cmd As SqlCommand
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
                cmd = New SqlCommand("SP_CurreccyConvert", con)
                con.Open()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@currencytype", currencytype.ToString())
                cmd.Parameters.AddWithValue("@rate", rate)
                cmd.ExecuteNonQuery()
                con.Close()
                cmd.Dispose()
            Catch ex As Exception
            End Try

        End Function
        Public Function SelectCurrancyValue(ByVal currencytype As String) As Decimal
            'Dim DT As New DataTable
            Try
                Dim fare As Decimal = 0
                Dim cmd As SqlCommand
                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
                cmd = New SqlCommand("SP_Curreccyselect", con)
                con.Open()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@currencytype", currencytype.ToString())
                fare = Convert.ToDecimal(cmd.ExecuteScalar())
                con.Close()
                cmd.Dispose()
                'Dim adap As SqlDataAdapter
                'adap = New SqlDataAdapter("SP_Curreccyselect", con)
                'adap.SelectCommand.CommandType = CommandType.StoredProcedure
                'adap.SelectCommand.Parameters.AddWithValue("@currencytype", currencytype.ToString())
                'adap.Fill(DT)
                Return fare

            Catch ex As Exception
            End Try

        End Function
        Public Function ExcpLogInfo(ByVal ex As Exception) As Integer
            Dim con As SqlConnection
            Dim cmd As SqlCommand
            Dim Temp As Integer = 0
            Try
                con = New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
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


End Namespace


