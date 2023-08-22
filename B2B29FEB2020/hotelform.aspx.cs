using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;
//using System.Net.Http;
//using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
using System.Diagnostics;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;


public partial class hotelform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jsonval = "{\"hotelIds\": [\"60936\",\"60934\",\"60988\",\"61070\",\"61098\",\"61099\",\"61112\",\"61125\",\"61128\",\"61191\",\"61219\",\"61256\",\"61323\",\"61458\",\"61693\",\"62081\",\"62695\",\"62705\",\"73125\",\"73228\",\"73553\",\"73748\",\"74129\",\"74166\",\"74180\",\"74173\",\"74184\",\"74198\",\"74208\",\"74254\",\"74256\",\"74346\",\"74356\",\"74383\",\"74423\",\"74401\",\"74435\",\"74669\",\"74864\",\"75157\",\"75227\",\"75286\",\"75414\",\"75687\",\"7589\",\"75908\",\"76634\",\"76672\",\"76777\",\"77070\"],  \"checkin\": 1592945996, \"checkout\": 1593032396, \"adults\": 1, \"children\": 0, \"rooms\":1}";
        string response = OYOHotelPostJSONBooking("http://api.oyorooms.com/b2b_reseller/api/v2/hotels/availability", jsonval, "SkNzYkh5WUFZLWhOX3Itb0FzOWg6cFpvOUx2WVB5czhaczlQMm5ieUM=", "POST");
      
}
    private string OYOHotelPostJSONBooking(string url, string json, string AccessToken, string MethodType)
    {
        StringBuilder sbResult = new StringBuilder();
        try
        {
            if (!string.IsNullOrEmpty(json))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = Encoding.ASCII.GetBytes(json);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                request.ContentLength = requestBytes.Length;
                request.Method = MethodType;
                //request.ContentType = "application/json";
                request.Headers["x-access-token"] = AccessToken;
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();

                using (HttpWebResponse WebResponse = (HttpWebResponse)request.GetResponse())
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
                        else if ((WebResponse.ContentEncoding.ToLower().Contains("deflate")))
                        {
                            responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
                        }
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        sbResult.Append(reader.ReadToEnd());
                        responseStream.Close();
                    }
                }
            }

        }
        catch (WebException WebEx)
        {
            WebResponse response = WebEx.Response;
            if (response != null)
            {
                Stream stream = response.GetResponseStream();
                string responseMessage = new StreamReader(stream).ReadToEnd();
                sbResult.Append(responseMessage);
                if (!responseMessage.Contains("error"))
                    sbResult.Append("<error>" + WebEx.Message + "</error>");
            }
            else
            {
                sbResult.Append(WebEx.Message);
                if (!WebEx.Message.Contains("error"))
                    sbResult.Append("<error>" + WebEx.Message + "</error>");
            }
        }
        catch (Exception ex)
        {
            sbResult.Append(ex.Message + "<error>" + ex.Message + "</error>");
        }
Response.Write(sbResult.ToString());
        return sbResult.ToString();
    }
}
