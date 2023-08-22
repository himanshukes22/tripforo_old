Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Web

Partial Class contact_us
    Inherits System.Web.UI.Page
    Private result As String = "NOT IN USE"
    Private Request As WebRequest
    Private Response As HttpWebResponse
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''sendSms("UJHgdgsdhsjss", "9999590749", "DEL:BOM", "BDHSGJ", "625", "28-04-2017", "nhgdft", "Please find ticket")
    End Sub
    Public Function sendSms(ByVal orderid As String, ByVal mobno As String, ByVal sector As String, ByVal Airno As String, ByVal FltNo As String, ByVal depdt As String, _
 ByVal pnr As String, ByRef smstext As String) As String
        Dim sendToPhoneNumber As String = mobno

        Dim m As String = "Dear Customer,Your booking for " & sector & " is confirmed on " & Airno & "-" & FltNo & " for " & " departure date " & depdt & ". Your airline PNR No. is " & pnr & "."
        smstext = m
        Dim m1 As String = HttpUtility.UrlEncode(m)

        Dim FeedId As String = "339159"
        Dim username As String = "9911333666"
        Dim password As String = "jgjpd"
        Dim ToMob As String = mobno
        Dim Text As String = m1
        Dim time As String = System.DateTime.Now.ToString("yyyyMMddhhmm")
        Dim senderid As String = "testSenderID"

        'Dim url As String = "http://sms.itzcash.com/servlet/PushSMSServlet?mobileno=" & mobno & "&entity=TRAVEL&message=" & m1 & "&orderid =" & orderid
        Dim url As String = "http://bulkpush.mytoday.com/BulkSms/SingleMsgApi?feedid=" & FeedId & "&username=" & username & "&password =" & password & "&To=" & ToMob & "&Text =" & Text & "&time=" & time & "&senderid =" & senderid

        Dim client As New WebClient()
        'Dim baseurl As String = "" + url + "username=" + UserId + "&password=" + password + "&sendername=" + senderid + "&mobileno=" + MobileNo + "&message=" + Message + ""
        Dim data As Stream = client.OpenRead(url)
        Dim reader As New StreamReader(data)
        Dim s As String = reader.ReadToEnd()
        data.Close()
        reader.Close()
        'Try
        '    Request = DirectCast(WebRequest.Create(url), HttpWebRequest)
        '    'request.Method = "POST"
        '    Request.Method = "POST"
        '    Request.ContentType = "application/x-www-form-urlencoded"
        '    Response = DirectCast(Request.GetResponse(), HttpWebResponse)
        '    Dim stream As Stream = Response.GetResponseStream()
        '    Dim ec As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '    Dim reader As StreamReader = New System.IO.StreamReader(stream, ec)
        '    Result = reader.ReadToEnd()
        '    reader.Close()
        '    stream.Close()
        'Catch ex As Exception
        '    Result = ex.InnerException.Message
        'End Try
        'Dim sendToPhoneNumber As [String] = mobno
        'Dim userid As [String] = "2000029630"
        'Dim passwd As [String] = "charles1"
        'Dim m As [String] = "Dear Customer,Your booking for " & sector & " is confirmed on " & Airno & "-" & FltNo & " for " & " departure date" & depdt & ". Your airline PNR No. is " & pnr & " ."
        'smstext = m
        'Dim m1 As String = HttpUtility.UrlEncode(m)

        'Dim url As [String] = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" & sendToPhoneNumber & "&msg=" & m1 & "&userid=" & userid & "&password=" & passwd & "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN"
        ''Try
        ''    request = WebRequest.Create(url)
        ''    response = DirectCast(request.GetResponse(), HttpWebResponse)
        ''    Dim stream As Stream = response.GetResponseStream()
        ''    Dim ec As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        ''    Dim reader As StreamReader = New System.IO.StreamReader(stream, ec)
        ''    result = reader.ReadToEnd()
        ''    reader.Close()
        ''    stream.Close()
        ''Catch ex As Exception
        ''    result = ex.InnerException.Message
        ''End Try

        Result = Result & url

        Return Result
    End Function
End Class
