Imports System.Net
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Linq
Imports System.Xml.Linq
Partial Class Hotel_HotelTesting
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            TextBox2.Text = GTAPostXml(TextBox3.Text.Trim(), TextBox1.Text.Trim())
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
            TextBox2.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TextBox3.Text = "http://stage-api.travelguru.com/services-2.0/tg-services/TGServiceEndPoint"
            'Dim ReqXml As StringBuilder = New StringBuilder()
            'Try
            '    ReqXml.Append("<soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            '    ReqXml.Append("<soap:Body>")
            '    ReqXml.Append("<OTA_HotelAvailRQ xmlns='http://www.opentravel.org/OTA/2003/05' RequestedCurrency='INR' SortOrder='DEALS' Version='0.0' PrimaryLangID='en' SearchCacheLevel='VeryRecent'>")
            '    ReqXml.Append("<AvailRequestSegments>")
            '    ReqXml.Append("<AvailRequestSegment>")
            '    ReqXml.Append("<HotelSearchCriteria>")
            '    ReqXml.Append("<Criterion>")
            '    ReqXml.Append("<Address>")
            '    ReqXml.Append("<CityName>New Delhi</CityName>")
            '    ReqXml.Append("<CountryName Code='India'></CountryName>")
            '    ReqXml.Append("</Address>")
            '    ReqXml.Append("<HotelRef/>")
            '    ReqXml.Append("<StayDateRange End='2013-12-30' Start='2013-12-27'/>")
            '    ReqXml.Append("<RoomStayCandidates>")
            '    ReqXml.Append("<RoomStayCandidate>")
            '    ReqXml.Append("<GuestCounts>")
            '    ReqXml.Append("<GuestCount AgeQualifyingCode='10' Count='1'/>")
            '    ReqXml.Append("</GuestCounts>")
            '    ReqXml.Append("</RoomStayCandidate>")
            '    ReqXml.Append("</RoomStayCandidates>")
            '    ReqXml.Append("<TPA_Extensions>")
            '    ReqXml.Append("<Pagination enabled='false' />")
            '    ReqXml.Append("<HotelBasicInformation>")
            '    ReqXml.Append("<Reviews/>")
            '    ReqXml.Append("</HotelBasicInformation>")
            '    ReqXml.Append("<UserAuthentication password='test' propertyId='1300000141' username='testnet'/>")
            '    ReqXml.Append("<Promotion Type='BOTH' Name='ALLPromotions' />")
            '    ReqXml.Append("</TPA_Extensions>")
            '    ReqXml.Append("</Criterion>")
            '    ReqXml.Append("</HotelSearchCriteria>")
            '    ReqXml.Append("</AvailRequestSegment>")
            '    ReqXml.Append("</AvailRequestSegments>")
            '    ReqXml.Append("</OTA_HotelAvailRQ>")
            '    ReqXml.Append("</soap:Body>")
            '    ReqXml.Append("</soap:Envelope>")
            '    TextBox1.Text = ReqXml.ToString()
            'Catch ex As Exception
            '    HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
            '    TextBox2.Text = ex.Message
            'End Try
        End If
    End Sub

    Protected Function GTAPostXml(ByVal url As String, ByVal xml As String) As String
        Dim sbResult As New StringBuilder
        Try
            Dim Http As HttpWebRequest = WebRequest.Create(url)
            If Not String.IsNullOrEmpty(xml) Then
                Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                Http.Method = "POST"
                Dim lbPostBuffer As Byte() = Encoding.UTF8.GetBytes(xml)
                Http.ContentLength = lbPostBuffer.Length
                Http.ContentType = "text/xml"
                'Http.Timeout = 300000
                Using PostStream As Stream = Http.GetRequestStream()
                    PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length)
                End Using
            End If

            Using WebResponse As HttpWebResponse = Http.GetResponse()
                If WebResponse.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", WebResponse.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim responseStream As Stream = WebResponse.GetResponseStream()
                    If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                        responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                    ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                        responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                    End If
                    Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)
                    sbResult.Append(reader.ReadToEnd())
                    responseStream.Close()
                End If
            End Using
        Catch WebEx As WebException
            TextBox2.Text = WebEx.Message
            Dim response As WebResponse = WebEx.Response
            Dim stream As Stream = response.GetResponseStream()
            Dim responseMessage As [String] = New StreamReader(stream).ReadToEnd()
            sbResult.Append(responseMessage)
        Catch ex As Exception
            sbResult.Append(ex.Message)
            TextBox2.Text = ex.Message
        End Try
        Return sbResult.ToString()
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sbResult As New StringBuilder
        Try
            Dim Http As HttpWebRequest = WebRequest.Create(TextBox3.Text.Trim())
            If Not String.IsNullOrEmpty(TextBox3.Text.Trim()) Then
                Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                Http.Method = "GET"
                Http.ContentType = "application/json; encoding=utf-8"
            End If

            Using WebResponse As HttpWebResponse = Http.GetResponse()
                If WebResponse.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", WebResponse.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim responseStream As Stream = WebResponse.GetResponseStream()
                    If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                        responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                    ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                        responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                    End If
                    Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)
                    sbResult.Append(reader.ReadToEnd())
                    responseStream.Close()
                End If
            End Using
        Catch WebEx As WebException
            TextBox2.Text = WebEx.Message
            Dim response As WebResponse = WebEx.Response
            Dim stream As Stream = response.GetResponseStream()
            Dim responseMessage As [String] = New StreamReader(stream).ReadToEnd()
            sbResult.Append(responseMessage)
        Catch ex As Exception
            sbResult.Append(ex.Message)
            TextBox2.Text = ex.Message
        End Try
        TextBox2.Text = sbResult.ToString()
        'StringBuilder sbResult = new StringBuilder();
        'Try
        '    {
        '        HttpWebRequest Httprequest = (HttpWebRequest)WebRequest.Create(url);
        '        Httprequest = (HttpWebRequest)HttpWebRequest.Create(url);
        '        Httprequest.Method = "GET";
        '        Httprequest.ContentType = "application/json; encoding=utf-8";
        '        Httprequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
        '        using (HttpWebResponse WebResponse = (HttpWebResponse)Httprequest.GetResponse())
        '        {
        '            if (WebResponse.StatusCode != HttpStatusCode.OK)
        '            {
        '                string message = String.Format("POST failed. Received HTTP {0}", WebResponse.StatusCode);
        '                throw new ApplicationException(message);
        '            }
        '        Else
        '            {
        '                Stream responseStream = WebResponse.GetResponseStream();
        '            If ((WebResponse.ContentEncoding.ToLower().Contains("gzip"))) Then
        '                {
        '                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
        '                }
        '            ElseIf ((WebResponse.ContentEncoding.ToLower().Contains("deflate"))) Then
        '                {
        '                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
        '                }
        '                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
        '                sbResult.Append(reader.ReadToEnd());
        '                responseStream.Close();
        '            }

        '        }
        '    }
        '    catch (WebException WebEx)
        '    {
        '        HotelDA objhtlDa = new HotelDA();
        '        HotelDA.InsertHotelErrorLog(WebEx, "ZumataGetXml");
        '        WebResponse response = WebEx.Response;
        '        if (response != null)
        '        {
        '            Stream stream = response.GetResponseStream();
        '            string responseMessage = new StreamReader(stream).ReadToEnd();
        '            sbResult.Append("<Errors>" + responseMessage + "</Errors>");
        '            int m = objhtlDa.SP_Htl_InsUpdBookingLog("EXCEPTION", url, responseMessage, "Zumata", "HotelInsert");
        '        }
        '    Else
        '        {
        '            int n = objhtlDa.SP_Htl_InsUpdBookingLog("EXCEPTION", url, WebEx.Message, "Zumata", "HotelInsert");
        '            sbResult.Append("<Errors>" + WebEx.Message + "</Errors>");

        '        }
        '    }
        '    catch (Exception ex)
        '    {
        '        sbResult.Append("<Errors>" + ex.Message + "</Errors>");
        '        HotelDA.InsertHotelErrorLog(ex, "ZumataGetXml");
        '        HotelDA objhtlDa = new HotelDA();
        '        int m = objhtlDa.SP_Htl_InsUpdBookingLog("EXCEPTION", url, ex.Message, "Zumata", "HotelInsert");
        '    }
        '    return sbResult.ToString();
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sbResult As New StringBuilder
        Try
            Dim Http As HttpWebRequest = WebRequest.Create(TextBox3.Text)
            If Not String.IsNullOrEmpty(TextBox1.Text) Then
                Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                Http.Method = "POST"
                Dim lbPostBuffer As Byte() = Encoding.UTF8.GetBytes(TextBox1.Text)
                Http.ContentLength = lbPostBuffer.Length
                Http.ContentType = "application/xml"
                'Http.Timeout = 300000
                Using PostStream As Stream = Http.GetRequestStream()
                    PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length)
                End Using
            End If

            Using WebResponse As HttpWebResponse = Http.GetResponse()
                If WebResponse.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", WebResponse.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim responseStream As Stream = WebResponse.GetResponseStream()
                    If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                        responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                    ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                        responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                    End If
                    Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)
                    sbResult.Append(reader.ReadToEnd())
                    responseStream.Close()
                End If
            End Using
        Catch WebEx As WebException
            TextBox2.Text = WebEx.Message
            Dim response As WebResponse = WebEx.Response
            Dim stream As Stream = response.GetResponseStream()
            Dim responseMessage As [String] = New StreamReader(stream).ReadToEnd()
            sbResult.Append(responseMessage)
        Catch ex As Exception
            sbResult.Append(ex.Message)
            TextBox2.Text = ex.Message
        End Try
        TextBox2.Text = sbResult.ToString()

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sbResult As New StringBuilder
        Try

            Dim Http As HttpWebRequest = WebRequest.Create(TextBox3.Text)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            If Not String.IsNullOrEmpty(TextBox1.Text) Then
                Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip")
                Http.Headers.Add("SOAPAction", TextBox4.Text)
                Http.Method = "POST"
                Dim lbPostBuffer As Byte() = Encoding.UTF8.GetBytes(TextBox1.Text)
                Http.ContentLength = lbPostBuffer.Length
                Http.ContentType = "text/xml"
                Using PostStream As Stream = Http.GetRequestStream()
                    PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length)
                End Using
            End If

            Using WebResponse As HttpWebResponse = Http.GetResponse()
                If WebResponse.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", WebResponse.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim responseStream As Stream = WebResponse.GetResponseStream()
                    If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                        responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                    ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                        responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                    End If
                    Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)
                    sbResult.Append(reader.ReadToEnd())
                    responseStream.Close()
                End If
            End Using
        Catch WebEx As WebException
            TextBox2.Text = WebEx.Message
            Dim response As WebResponse = WebEx.Response
            Dim stream As Stream = response.GetResponseStream()
            Dim responseMessage As [String] = New StreamReader(stream).ReadToEnd()
            sbResult.Append(responseMessage)
        Catch ex As Exception
            sbResult.Append(ex.Message)
            TextBox2.Text = ex.Message
        End Try
        TextBox2.Text = sbResult.ToString()

    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim sbResult As New StringBuilder
        Try
            Dim Http As HttpWebRequest = WebRequest.Create(TextBox3.Text.Trim())
            If Not String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
                ' Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                Http.Headers.Add("X-Payment-Key", TextBox4.Text)
                Http.Method = "POST"
                Http.ContentType = "application/json; charset=UTF-8"
                ' Http.KeepAlive = True
                Dim lbPostBuffer As Byte() = Encoding.UTF8.GetBytes(TextBox1.Text)
                Http.ContentLength = lbPostBuffer.Length
                Using PostStream As Stream = Http.GetRequestStream()
                    PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length)
                End Using
            End If

            Using WebResponse As HttpWebResponse = Http.GetResponse()

                If WebResponse.StatusCode = HttpStatusCode.OK Or WebResponse.StatusCode = HttpStatusCode.Created Or WebResponse.StatusCode = HttpStatusCode.Accepted Then
                    Dim responseStream As Stream = WebResponse.GetResponseStream()
                    If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                        responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                    ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                        responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                    End If
                    Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)
                    sbResult.Append(reader.ReadToEnd())
                    responseStream.Close()
                Else
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", WebResponse.StatusCode)
                    Throw New ApplicationException(message)
                End If
            End Using
        Catch WebEx As WebException
            TextBox2.Text = WebEx.Message
            Dim response As WebResponse = WebEx.Response
            Dim stream As Stream = response.GetResponseStream()
            Dim responseMessage As [String] = New StreamReader(stream).ReadToEnd()
            sbResult.Append(responseMessage)
        Catch ex As Exception
            sbResult.Append(ex.Message)
            TextBox2.Text = ex.Message
        End Try
        TextBox2.Text = sbResult.ToString()
    End Sub
End Class
