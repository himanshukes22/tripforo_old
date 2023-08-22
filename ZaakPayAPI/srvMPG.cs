using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http.Headers;

namespace ZaakPayAPI
{
    public class srvMPG
    {
        public string GetPostReqRes(string PostString, string Url, string ToHash, string Hashed, string MerchantTransId, string CommandType)
        {
            string JsonXMLResponse = string.Empty;
            String status = string.Empty;
           // string ss = GetFromApiService(Url);

            //string ss1 = GetFromApiServiceHttpClient(Url);
            MobikwikTrans objMPg = new MobikwikTrans();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                WebRequest myWebRequest = WebRequest.Create(Url);
                myWebRequest.Method = "POST";
                myWebRequest.ContentType = "application/x-www-form-urlencoded";               
                //myWebRequest.Headers.Add("Cache-Control", "no-cache");                
                //myWebRequest.Headers.Add("Postman-Token", "2932d5d5c8b845d9a249a5fde033899e"); //API KEY


               // myWebRequest.Headers.Add("Postman-Token", "c1741438-a818-4d95-9a79-37d0aa24ca47");
                //myWebRequest.Headers.TryAddWithoutValidation("Postman-Token", "c1741438-a818-4d95-9a79-37d0aa24ca47");
                //myWebRequest.ContentType = "application/json; charset=utf-8";
                myWebRequest.Timeout = 180000;
                StreamWriter requestWriter = new StreamWriter(myWebRequest.GetRequestStream());
                requestWriter.Write(PostString);
                requestWriter.Close();
                StreamReader responseReader = new StreamReader(myWebRequest.GetResponse().GetResponseStream());
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                JsonXMLResponse = readStream.ReadToEnd();
                if (JsonXMLResponse.Contains("xml"))
                {
                    status = GetValueFromXml(JsonXMLResponse, "response_element");
                }
                else
                {
                    Newtonsoft.Json.Linq.JObject account = Newtonsoft.Json.Linq.JObject.Parse(JsonXMLResponse);
                    status = (string)account.SelectToken("transaction_details." + MerchantTransId + ".status");
                }
                int i = objMPg.InsertWebServiceLog(MerchantTransId, CommandType, ToHash, Hashed, PostString, JsonXMLResponse, status, "", "");

            }
            catch (WebException ex)
            {

                int flag = objMPg.InsertExceptionLog("PaymentGateway", "srvPG", "GetPostReqResPayU", "Web Service Error", ex.Message, ex.StackTrace);
                int i = objMPg.InsertWebServiceLog(MerchantTransId, CommandType, ToHash, Hashed, PostString, JsonXMLResponse, status, ex.Message, ex.StackTrace);
            }
            return JsonXMLResponse;
        }
        public string GetValueFromXml(string strRes, string ResType)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(strRes);
            XDocument xdo = XDocument.Load(new XmlNodeReader(xd));
            string str = "";
            try
            {
                if (ResType.ToLower() == "response_element")
                {
                    //str = xdo.Descendants("responsecode").First().Value + "&";
                    str = xdo.Descendants("description").First().Value;
                }
                else
                { str = xdo.Descendants("responsecode").First().Value; }
            }
            catch (Exception ex)
            { }
            return str;
        }

        public string GetFromApiService(string Url)
        {
            string JsonXMLResponse = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    JsonXMLResponse = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            { }           
            return JsonXMLResponse;
        }
        public string GetFromApiServiceHttpClient(string Url,string Request,string checkSum)
        {
            string JsonXMLResponse = "";
            //var response =null ;
            //ConcurrentBag<Task> taskList = new ConcurrentBag<Task>();
            //try
            //{
            //    using (var httpClient = new HttpClient())
            //    {
            //        using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.zaakpay.com/checktransaction?v=5&merchantIdentifier=a4f55c7c338d41d9aaefc1fc66b8e934&orderId=MWK1126143935733594&mode=0&checksum=c8a23cd2b54975506d667ac9da83def60e6599c1e76789269af5c99df2fa0f17"))
            //        {
            //            request.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
            //            request.Headers.TryAddWithoutValidation("Postman-Token", "c1741438-a818-4d95-9a79-37d0aa24ca47");

            //            //var response = await httpClient.SendAsync(request);
            //            taskList.Add(Task.Run(async () => await httpClient.SendAsync(request)));

            //            //ConcurrentBag<Task> taskList = new ConcurrentBag<Task>()
            //        }
            //    }
            //    Task.WhenAll(taskList).Wait();

            //}
            //catch (Exception ex)
            //{ }

            try
            {
                string a = "1";
                string b = "2";
                var handler = new HttpClientHandler();
                handler.UseCookies = false;
                if(a==b)
                {
                    Url = "https://zaakstaging.zaakpay.com/checkTxn?v=5";
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                using (var httpClient = new HttpClient(handler))
                {
                    //using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://zaakstaging.zaakpay.com/checkTxn?v=5"))
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), Url))
                    {
                        request.Headers.TryAddWithoutValidation("Cookie", "JSESSIONID=A4C211B0FC1F713C69DD79521963AAD2");

                        var contentList = new List<string>();
                        //contentList.Add($"data={Uri.EscapeDataString("{\"merchantIdentifier\":\"b19e8f103bce406cbd3476431b6b7973\",\"mode\":\"0\",\"orderDetail\":{\"orderId\":\"ZPLive1602500556069\"}}")}");
                        //contentList.Add($"checksum={Uri.EscapeDataString("e0a8a4080fc661dbd23119309d9a45817bd01cc9264b1fde9b26a1dac7e1da50")}");

                        //uncoment devesh
                        //contentList.Add($"data={Uri.EscapeDataString(Request)}");
                        //contentList.Add($"checksum={Uri.EscapeDataString(checkSum)}");

                        request.Content = new StringContent(string.Join("&", contentList));
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                        //Task.Run(async () => await CPNAvailability_O(context, CrdListCPN[e]))
                       // HttpResponseMessage result = await client.SendAsync(request);
                       // var response = Task.Run(async () => await httpClient.SendAsync(request));

                        var t = Task.Run(() => httpClient.SendAsync(request));
                        t.Wait();
                     //string ff=   t.Result;
                        //  response = await httpClient.SendAsync(request);
                        // Task.WhenAll(taskList).Wait();
                        string hhh = "";
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return JsonXMLResponse;
        }

    }
}
