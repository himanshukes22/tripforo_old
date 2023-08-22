using CCA.Util;
using System;
using System.Collections.Generic;
using System.Configuration;// Use for ConfigurationManager
using System.IO;//use for Stream
using System.IO.Compression;// Use for GZipStream and  CompressionMode.Decompress) 
using System.Linq;
using System.Net;// use for ServicePointManager,HttpWebRequest
using System.Text;
//using PG.CCAvenue;
using System.Collections.Specialized;

namespace PG
{
    public class srvPG
    {
        public string GetPostReqResCCAvenue(string ccaRequest, string commandType, string jSessionId)
        {
            StringBuilder sbResult = new StringBuilder();
            PaymentGateway objPg = new PaymentGateway();

            //string url = "https://180.179.175.17/apis/servlet/DoWebTrans";//Convert.ToString(ConfigurationManager.AppSettings["PgUrl"]) + reqUrl;
            //string MerchantID = "103816";// Convert.ToString(ConfigurationManager.AppSettings["PgUserId"]);
            //string AccessCode = "AVZP00DH49AS36PZSA";
            //string workingKey = "73650E87362FEFB7F24D95FAE323F349";            

            string url = Convert.ToString(ConfigurationManager.AppSettings["PGApiUrl"]);
            string MerchantID = Convert.ToString(ConfigurationManager.AppSettings["PGApiMerchantID"]);
            string AccessCode = Convert.ToString(ConfigurationManager.AppSettings["PGApiAccessCode"]);
            string workingKey = Convert.ToString(ConfigurationManager.AppSettings["PGApiWorkingKey"]);

            CCACrypto ccaCrypto = new CCACrypto();
           // string strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            string pgRequest = string.Empty;
            string sResponse = string.Empty;
            string error = string.Empty;
            string encResponse = string.Empty;
            string status = string.Empty;
            string enc_response = string.Empty;
           // pgRequest = "enc_request=" + strEncRequest + "&access_code=" + AccessCode + "&command=" + commandType + "&request_type=JSON&response_type=JSON&version=1.1";
            pgRequest = ccaRequest;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//TLS1.2
                HttpWebRequest Http = (HttpWebRequest)WebRequest.Create(url);
                if (!string.IsNullOrEmpty(pgRequest))
                {
                    Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
                    Http.Method = "POST";
                    byte[] lbPostBuffer = Encoding.UTF8.GetBytes(pgRequest);
                    Http.ContentLength = lbPostBuffer.Length;
                    //Http.ContentType = "application/json";
                    Http.ContentType = "application/x-www-form-urlencoded";
                    //string authInfo = "devesh:pass1234";
                    //string authInfo = userId + ":" + password;
                    // authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    //Http.Headers["Authorization"] = "Basic " + authInfo;
                    Http.PreAuthenticate = true;
                    Http.Timeout = 300000;
                    Http.ReadWriteTimeout = 300000;
                    using (Stream PostStream = Http.GetRequestStream())
                    {
                        PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                    }
                }
                using (HttpWebResponse WebResponse = (HttpWebResponse)Http.GetResponse())
                {
                    if (WebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("POST failed. Received HTTP {0}", WebResponse.StatusCode);
                        throw new ApplicationException(message);
                    }
                    else
                    {
                        Stream responseStream = WebResponse.GetResponseStream();
                        if ((WebResponse.ContentEncoding.ToLower().Contains("gzip")))
                        {
                            responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                        }

                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        sbResult.Append(reader.ReadToEnd());
                        responseStream.Close();
                    }
                    NameValueCollection Params = new NameValueCollection();
                    encResponse = Convert.ToString(sbResult);
                    string[] segments = encResponse.Split('&');
                    foreach (string seg in segments)
                    {
                        string[] parts = seg.Split('=');
                        if (parts.Length > 0)
                        {
                            string Key = parts[0].Trim();
                            string Value = parts[1].Trim();

                            if (Key == "status")
                                status = parts[1].Trim();
                            if (Key == "enc_response")
                                enc_response = parts[1].Trim();

                            //if (Key == "order_status")
                            //    Status = parts[1].Trim();
                            //if (Key == "failure_message")
                            //    ErrorText = parts[1].Trim();
                           
                            //if (PaymentId == "response_code")
                            //    ResponseCode = parts[1].Trim();
                            //if (Key == "amount")
                            //    PgAmount = parts[1].Trim();

                            Params.Add(Key, Value);
                        }
                    }

                    if (!string.IsNullOrEmpty(enc_response) && status == "0")
                    {
                        //CCACrypto ccaCrypto = new CCACrypto();
                        encResponse = ccaCrypto.Decrypt(enc_response, workingKey);
                        //sResponse = encResponse;
                        sResponse = status + "~" + encResponse;
                    }
                    else
                    {
                        sResponse = status + "~" + encResponse;
                    }
                    
                }
                //InsertServiceResponse("POST", url, methodName, reqJson, sResponse, error);               
            }
            catch (WebException ex)
            {
                WebResponse response = ex.Response;
                error = ex.Message;
                int flag = objPg.InsertExceptionLog("PaymentGateway", "PaymentGateway", "srvPG", "ErrorCode", ex.Message, ex.StackTrace);
                status = "1";
                if (response != null)
                {
                    //Stream stream = response.GetResponseStream();
                    //string responseMessage = new StreamReader(stream).ReadToEnd();
                    ////sResponse = "{'errorMessage':'" + responseMessage + "'}";
                    //error = error + "(" + responseMessage + ")";
                    //sResponse = status + "~" + error;
                    //return sResponse;
                    Stream stream = response.GetResponseStream();
                    string responseMessage = new StreamReader(stream).ReadToEnd();                  
                    error = "{\"error_desc\":" + error + ",\"Order List: " + response + "\",\"status\":1,\"error_code\":\"PG101\"}";                   
                    sResponse = status + "~" + error;
                    return sResponse;
                }
                else {
                    error = "{\"error_desc\":" + error + ",\"Order List: " + response + "\",\"status\":1,\"error_code\":\"PG102\"}";                  
                    //  error = error + "(" + responseMessage + ")";
                    sResponse = status + "~" + error;
                    return sResponse;                
                }
            }
            return sResponse;
        }

        public string GetPostCCAvenueService(string pgRequest, string commandType, string ReferenceNo)
        {
            StringBuilder sbResult = new StringBuilder();            
            string url = Convert.ToString(ConfigurationManager.AppSettings["PGApiUrl"]);          
            string sResponse = string.Empty;
            string error = string.Empty;
            string status = string.Empty;            
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                HttpWebRequest Http = (HttpWebRequest)WebRequest.Create(url);
                if (!string.IsNullOrEmpty(pgRequest))
                {
                    Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
                    Http.Method = "POST";
                    byte[] lbPostBuffer = Encoding.UTF8.GetBytes(pgRequest);
                    Http.ContentLength = lbPostBuffer.Length;
                    Http.ContentType = "application/json";
                    Http.ContentType = "application/x-www-form-urlencoded";                 
                    Http.PreAuthenticate = true;
                    Http.Timeout = 300000;
                    Http.ReadWriteTimeout = 300000;
                    using (Stream PostStream = Http.GetRequestStream())
                    {
                        PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                    }
                }
                using (HttpWebResponse WebResponse = (HttpWebResponse)Http.GetResponse())
                {
                    if (WebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("POST failed. Received HTTP {0}", WebResponse.StatusCode);
                        throw new ApplicationException(message);
                    }
                    else
                    {
                        Stream responseStream = WebResponse.GetResponseStream();
                        if ((WebResponse.ContentEncoding.ToLower().Contains("gzip")))
                        {
                            responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                        }

                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        sbResult.Append(reader.ReadToEnd());
                        responseStream.Close();
                    }
                  sResponse=  Convert.ToString(sbResult);
                }
                
            }
            catch (WebException ex)
            {
                WebResponse response = ex.Response;
                error = ex.Message;
                PaymentGateway objPg = new PaymentGateway();
                int flag = objPg.InsertExceptionLog("PaymentGateway", "PaymentGateway", "srvPG", "ErrorCode", ex.Message, ex.StackTrace);
                sResponse = "catch";
                return sResponse;
            }
            return sResponse;
        }

        public string GetPostReqResPayU(string PostString, string Url, string ToHash, string Hashed, string MerchantTransId, string CommandType)
        {
            string JsonResponse = string.Empty;
            String status = string.Empty;
            PaymentGateway objPg = new PaymentGateway();
            try
            {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebRequest myWebRequest = WebRequest.Create(Url);
            myWebRequest.Method = "POST";
            myWebRequest.ContentType = "application/x-www-form-urlencoded";
            myWebRequest.Timeout = 180000;
            StreamWriter requestWriter = new StreamWriter(myWebRequest.GetRequestStream());
            requestWriter.Write(PostString);
            requestWriter.Close();
            StreamReader responseReader = new StreamReader(myWebRequest.GetResponse().GetResponseStream());
            WebResponse myWebResponse = myWebRequest.GetResponse();
            Stream ReceiveStream = myWebResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(ReceiveStream, encode);
            JsonResponse = readStream.ReadToEnd();

            Newtonsoft.Json.Linq.JObject account = Newtonsoft.Json.Linq.JObject.Parse(JsonResponse);
            status = (string)account.SelectToken("transaction_details." + MerchantTransId + ".status");
            int i = objPg.InsertWebServiceLog(MerchantTransId, CommandType, ToHash, Hashed, PostString, JsonResponse, status, "", "");

            }
            catch (WebException ex)
            {
                
                int flag = objPg.InsertExceptionLog("PaymentGateway", "srvPG", "GetPostReqResPayU", "Web Service Error", ex.Message, ex.StackTrace);
                int i = objPg.InsertWebServiceLog(MerchantTransId, CommandType, ToHash, Hashed, PostString, JsonResponse, status, ex.Message, ex.StackTrace);
            }
            return JsonResponse;           
        }
    }
}
