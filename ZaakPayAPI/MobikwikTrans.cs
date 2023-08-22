using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ZaakPayAPI
{
    public class ParamSanitizer
    {
        public static string pram = "";
        public static string sanitizeParam(string param)
        {

            String ret = null;
            if (param == null)
                return null;

            ret = param.Replace("[>><>(){}?&* ~`!#$%^=+|\\:'\";,\\x5D\\x5B]+", " ");

            return ret;
        }
        public static String SanitizeURLParam(String url)
        {

            if (url == null)
                return "";

            Match match = Regex.Match(url, "^(https?)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]", RegexOptions.IgnoreCase);

            if (match.Success)

                return url;
            else
                return "";

        }
    }

    public class ChecksumCalculator
    {
        public static string toHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();


        }

        public static string calculateChecksum(string secretkey, string allparamvalues)
        {

            byte[] dataToEncryptByte = Encoding.UTF8.GetBytes(allparamvalues);
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretkey);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);
            byte[] checksumByte = hmacsha256.ComputeHash(dataToEncryptByte);
            String checksum = toHex(checksumByte);

            return checksum;
        }

        public static Boolean verifyChecksum(String secretKey, String allParamVauleExceptChecksum, String checksumReceived)
        {

            byte[] dataToEncryptByte = Encoding.UTF8.GetBytes(allParamVauleExceptChecksum);
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);
            byte[] checksumCalculatedByte = hmacsha256.ComputeHash(dataToEncryptByte); ;
            String checksumCalculated = toHex(checksumCalculatedByte);

            if (checksumReceived.Equals(checksumCalculated))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string getAllNotEmptyParamValue(HttpRequest Request)
        {
            String allNonEmptyParamValue = "";
            NameValueCollection postedValues = Request.Form;
            //string merchantIdentifier = ViewState["merchantIdentifier"];
            String[] paramSeq = { "amount","bankid","buyerAddress",
                "buyerCity","buyerCountry","buyerEmail","buyerFirstName","buyerLastName","buyerPhoneNumber","buyerPincode",
                "buyerState","currency","merchantIdentifier","merchantIpAddress","mode","orderId",
                "product1Description","product2Description","product3Description","product4Description",
                "productDescription","productInfo","purpose","returnUrl","shipToAddress","shipToCity","shipToCountry",
                "shipToFirstname","shipToLastname","shipToPhoneNumber","shipToPincode","shipToState","showMobile","txnDate",
                "txnType","zpPayOption"};

            foreach (String i in paramSeq)
            {

                try
                {
                    String paramInArray = postedValues[i];

                    if (!paramInArray.Equals(""))
                    {
                        //paramName = postedValues.AllKeys[i];

                        String paramValue = ParamSanitizer.sanitizeParam(paramInArray);


                        if (paramValue != null)
                        {
                            allNonEmptyParamValue = allNonEmptyParamValue + i + "=" + paramValue + "&";

                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught: {0}", e);
                }
            }

            return allNonEmptyParamValue;

        }
    }

    public class MobikwikTrans
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);//TotalAmount
        SqlCommand cmd;
        SqlDataAdapter adap;    
        public string PaymentGatewayReqMobikwik(string orderId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double amount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string buyerEmail, string currency, string ServiceType, string IP, string Trip, string PaymentOption)
        {
            if (IP == "::1")
            {
                IP = "125.19.186.190";
            }
            string merchantIdentifier = "";// merchantIdentifier.Text;
            String secretKey = "";           
            string ProviderUrl = "";
            string returnUrl = "";//returnUrl.Text;
            //string FailureUrl = "";
            //string CancelUrl = "";

            string postHTML = "";

            //string MERC_UNQ_REF = "";
            //string CHANNEL_ID = "";
            //string INDUSTRY_TYPE_ID = "";
            //string WEBSITE = "";
            //string SALT = "";
            //string HashSequence = "";
            string zpPayOption = "";// zpPayOption.Text;
            string txnType = "";//txnType.Text;            
            int flag = 0;
            string mode = "1";// mode.Text;           
            string ccaRequest = "";
            string strEncRequest = "";
            string purpose = "1";//purpose.Text;            
            //DateTime Datetime = "";//Convert.ToDateTime(txnDate.Text);
            
            double TransCharges = 0;
            string ChargesType = "";
            double TotalPgCharges = 0;
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["Mobikwik"]);

            try
            {
                DataTable pgDT = GetTotalAmountWithPgCharge(PaymentOption, amount, AgentId, orderId);
                if (pgDT != null)
                {
                    if (pgDT.Rows.Count > 0)
                    {
                        TotalAmount = Convert.ToDouble(pgDT.Rows[0]["TotalAmount"]);
                        TotalPgCharges = Convert.ToDouble(pgDT.Rows[0]["TotalPgCharges"]);
                        TransCharges = Convert.ToDouble(pgDT.Rows[0]["Charges"]);
                        ChargesType = Convert.ToString(pgDT.Rows[0]["ChargesType"]);
                    }
                }
                DataTable dt = new DataTable();
                dt = GetPgCredential();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        merchantIdentifier = Convert.ToString(dt.Rows[0]["MerchantID"]);
                        secretKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                        ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                        returnUrl = Convert.ToString(dt.Rows[0]["SuccessUrl"]);
                        zpPayOption = Convert.ToString(dt.Rows[0]["Zppayoption"]);
                        //Below code use for credit card Enable Option only
                        //FailureUrl='e39a69458ecf4cbcbcc83119a1e93637' ,CancelUrl='17fdb33802c9493d9b3cc7fb1cdb0191'
                        //Merchant Identifier :	e39a69458ecf4cbcbcc83119a1e93637=  FailureUrl =merchantIdentifier
                        //Key :17fdb33802c9493d9b3cc7fb1cdb0191=CancelUrl =secretKey
                        
                        //if (PaymentOption.ToUpper() == "MCCONLY")
                        //{
                        //    merchantIdentifier = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                        //    secretKey = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                        //}
                        try
                        {
                            if (PaymentOption.ToUpper() == "MCCONLY")
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["FailureUrl"])) && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CancelUrl"])))
                                {
                                    merchantIdentifier = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                                    secretKey = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            merchantIdentifier = Convert.ToString(dt.Rows[0]["MerchantID"]);
                            secretKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                        }

                        //MERC_UNQ_REF = Convert.ToString(dt.Rows[0]["MERC_UNQ_REF"]);
                        //CHANNEL_ID = Convert.ToString(dt.Rows[0]["CHANNEL_ID"]);
                        //INDUSTRY_TYPE_ID = Convert.ToString(dt.Rows[0]["INDUSTRY_TYPE_ID"]);
                        //WEBSITE = Convert.ToString(dt.Rows[0]["WEBSITE"]);                        
                        //FailureUrl = Convert.ToString(dt.Rows[0]["FailureUrl"]);                        
                        //CancelUrl = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                        //txnType = Convert.ToString(dt.Rows[0]["Txntype"]);                        
                        //HashSequence = Convert.ToString(dt.Rows[0]["HashSequence"]);
                        //"amount=10000&buyerEmail=admin@flywidus.com&currency=INR&merchantIdentifier=a4f55c7c338d41d9aaefc1fc66b8e934&merchantIpAddress=127.1.0.1&mode=1&orderId=ZPLive1547729564413&purpose=1&txnType=1&zpPayOption=1&"
                        //1 - ALL 11 - CARD 12 - NETBANKING 13 - WALLETS 14 - UPI 15 - CCONLY - Only allow CreditCards
                        // 16 - DCONLY - Only allow DebitCards 17 - EZEClick 18 - ATM + PIN 19 - MasterPass 20 - EMI
                        if (PaymentOption.ToUpper() == "MALL")
                            txnType = "1";
                        else if (PaymentOption.ToUpper() == "MCARD")
                            txnType = "11";
                        else if (PaymentOption.ToUpper() == "MNETBANKING")
                            txnType = "12";
                        else if (PaymentOption.ToUpper() == "MWALLETS")
                            txnType = "13";
                        else if (PaymentOption.ToUpper() == "MUPI")
                            txnType = "14";
                        else if (PaymentOption.ToUpper() == "MCCONLY")
                            txnType = "15";
                        else if (PaymentOption.ToUpper() == "MDCONLY")
                            txnType = "16";
                        else if (PaymentOption.ToUpper() == "MEZECLICK")
                            txnType = "17";
                        else if (PaymentOption.ToUpper() == "MATMPIN")
                            txnType = "18";
                        else if (PaymentOption.ToLower() == "MMASTERPASS")
                            txnType = "19";
                        else if (PaymentOption.ToUpper() == "MEMI")
                            txnType = "20";

                        string Parameters = "amount=" + TotalAmount * 100 + "&buyerEmail=" + buyerEmail + "&currency=" + currency + "&merchantIdentifier=" + merchantIdentifier + "&merchantIpAddress=" + IP + "&mode=" + mode + "&orderId=" + orderId + "&purpose=" + purpose + "&returnUrl=" + returnUrl + "&txnType=" + txnType + "&zpPayOption=" + zpPayOption + "&";
                        ccaRequest = Parameters;
                         // ccaRequest = "" + merchantIdentifier + "|" + MERC_UNQ_REF + "|" + secretKey + "|" + CHANNEL_ID + "|" + INDUSTRY_TYPE_ID + "|" + WEBSITE + "|" + orderId + "|" + Convert.ToDecimal(TotalAmount).ToString("g29") + "|" + ServiceType + "|" + BillingName + "|" + buyerEmail + "|||||||||||" + SALT + "";
                         strEncRequest = ChecksumCalculator.calculateChecksum(secretKey, Parameters);


                        postHTML = "<html>";
                        postHTML += "<head>";
                        postHTML += "<title>Merchant Check Out Page</title>";
                        postHTML += "</head>";
                        postHTML += "<body>";
                        postHTML += "<center><h1>Please do not refresh this page...</h1></center>";
                        postHTML += "<form method='post' action='" + ProviderUrl + "'" + orderId + " name='f1'>";//" + orderId + " name='f1'
                        postHTML += "<table border='1'>";
                        postHTML += "<tbody>";
                        postHTML += "<input type='hidden' name= 'merchantIdentifier' value='" + merchantIdentifier + "'>";
                        postHTML += "<input type='hidden' name= 'orderId' value='" + orderId + "'>";
                        postHTML += "<input type='hidden' name= 'amount' value='" + TotalAmount * 100 + "'>";
                        postHTML += "<input type='hidden' name= 'buyerEmail' value='" + buyerEmail + "'>";
                        postHTML += "<input type='hidden' name= 'txnType' value='" + txnType + "'>";
                        postHTML += "<input type='hidden' name= 'zpPayOption' value='" + zpPayOption + "'>";
                        postHTML += "<input type='hidden' name= 'mode' value='" + mode + "'>";
                        postHTML += "<input type='hidden' name= 'currency' value='" + currency + "'>";
                        postHTML += "<input type='hidden' name= 'merchantIpAddress' value='" + IP + "'>";
                        postHTML += "<input type='hidden' name= 'purpose' value='" + purpose + "'>";//CancelUrl
                        postHTML += "<input type='hidden' name= 'returnUrl' value='" + returnUrl + "'>";
                        postHTML += "<input type='hidden' name='checksum' value='" + strEncRequest + "'>";
                        postHTML += "</tbody>";
                        postHTML += "</table>";
                        postHTML += "<script type='text/javascript'>";
                        postHTML += "document.f1.submit();";
                        postHTML += "</script>";
                        postHTML += "</form>";
                        postHTML += "</body>";
                        postHTML += "</html>";

                        flag = InsertPaymentRequestDetails(orderId, TId, IBTrackId, BillingName, "Mobikwik", buyerEmail, BillingTel, BillingAddress, TotalAmount, amount, AgentId, AgencyName, IP, ccaRequest, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, postHTML);
                    }
                    else
                    {
                    }
                }
                if (flag > 0)
                {
                    postHTML = "yes~" + postHTML;
                }
                else
                {
                    postHTML = "no~" + postHTML;
                }
            }
            catch (Exception ex)
            {
                postHTML = "no~" + ex.Message;
                int insert = InsertExceptionLog("ZaakPayWallet", "MobikwikTrans", "ZaakpayGatewayReqPaytm", "insert", ex.Message, ex.StackTrace);
                return postHTML;
            }

            return postHTML;
        }

        public DataTable GetTotalAmountWithPgCharge(string PaymentMode, double OriginalAmount, string UserId, string TrackId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@PaymentMode", PaymentMode);
                adp.SelectCommand.Parameters.AddWithValue("@OriginalAmount", OriginalAmount);
                adp.SelectCommand.Parameters.AddWithValue("@AgentId", UserId);
                adp.SelectCommand.Parameters.AddWithValue("@TrackId", TrackId);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "PgTotalAmount");
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaytmWallet", "PaytmTrans", "GetTotalAmountWithPgCharge", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public int InsertExceptionLog(string Module, string ClassName, string MethodName, string ErrorCode, string ExMessage, string ExStackTrace)
        {
            int temp = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SpInsertExceptionLog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Module", Module);
                cmd.Parameters.AddWithValue("@ClassName", ClassName);
                cmd.Parameters.AddWithValue("@MethodName", MethodName);
                cmd.Parameters.AddWithValue("@ErrorCode", ErrorCode);
                cmd.Parameters.AddWithValue("@ExMessage", ExMessage);
                cmd.Parameters.AddWithValue("@ExStackTrace ", ExStackTrace);
                temp = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();

            }
            return temp;
        }
        public DataTable GetPgCredential()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["Mobikwik"]);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Get_PgCredentials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pvd", Provider);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                //adap.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("ZaakPayWallet", "MobikwikTrans", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public int InsertPaymentRequestDetails(string TrackId, string TId, string IBTrackId, string Name, string PaymentGateway, string Email, string Mobile, string Address, double TotalAmount, double OriginalAmount, string AgentId, string AgencyName, string Ip, string PgRequest, string ServiceType, string EncRequest, string Trip, double TotalPgCharges, double TransCharges, string ChargesType, string PostHtml)
        {
            int temp = 0;
            try
            {
                //Name, TrackId, PaymentGateway, Email, Mobile, Address, Amount, OriginalAmount, AgentId, AgencyName, Status, Ip
                con.Open();
                SqlCommand cmd = new SqlCommand("SpInsertPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrackId", TrackId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@PaymentGateway", PaymentGateway);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Mobile", Mobile);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Amount", TotalAmount);
                cmd.Parameters.AddWithValue("@OriginalAmount", OriginalAmount);
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@AgencyName", AgencyName);
                cmd.Parameters.AddWithValue("@Status", "Requested");
                cmd.Parameters.AddWithValue("@Ip", Ip);
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@PgRequest", PgRequest);
                cmd.Parameters.AddWithValue("@EncRequest", EncRequest);
                cmd.Parameters.AddWithValue("@TId", TId);
                cmd.Parameters.AddWithValue("@IBTrackId", IBTrackId);
                cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
                cmd.Parameters.AddWithValue("@Trip", Trip);
                // add new param 07 sept 2016
                cmd.Parameters.AddWithValue("@PgTotalCharges", TotalPgCharges);
                cmd.Parameters.AddWithValue("@PgTransCharges", TransCharges);
                cmd.Parameters.AddWithValue("@PgChargesType", ChargesType);
                cmd.Parameters.AddWithValue("@PostForm", PostHtml);

                temp = cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("ZaakPaymWallet", "MobikwikTrans", "InsertPaymentRequestDetails", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();

            }
            return temp;
        }
        public int UpdateCreditLimit(string AgentId, string TrackId, string BookingType, string IpAddress)
        {
            int temp = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                con.Open();
                cmd = new SqlCommand("AddCreditLimitByPaymentGateway", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgentID", AgentId);
                cmd.Parameters.AddWithValue("@InvoiceNo", TrackId);
                cmd.Parameters.AddWithValue("@BookingType", BookingType);
                cmd.Parameters.AddWithValue("@IPAddress", IpAddress);
                cmd.Parameters.AddWithValue("@ActionType", "PGCREDITLIMIT");
                temp = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "UpdateCreditLimit", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }
        public DataSet GetPaymentDetails(string TrackId, string AgentID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@TrackId", TrackId);
                adp.SelectCommand.Parameters.AddWithValue("@AgentId", AgentID);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "GetDetails");
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPaymentDetails", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public string UpdatePaymentResponseDetails(string AgentId, string PgResponse)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            int temp = 0;
            string OrderId = string.Empty;
            string PaymentId = string.Empty;
            string StatusCode = string.Empty;
            string BankRefNo = string.Empty;
            string ErrorText = string.Empty;
            string ResponseCode = string.Empty;
            string PaymentMode = string.Empty;
            string CardType = string.Empty;
            string IssuingBank = string.Empty;
            string CardName = string.Empty;
            string DoRedirect = string.Empty;
            string CardNumber = string.Empty;
            string PaymentMethod=string.Empty;
            string UnmappedStatus = string.Empty;
            string PgAmount = "0.0";
            string DiscountValue = "0.0";
            string MerAamount = "0.0";
            string msg = "no~" + OrderId;
            string ApiRequest = string.Empty;
            string ApiResponse = string.Empty;
            string ApiStatus = string.Empty;
            string ApiEncryptRequest = string.Empty;
            string CardHashedId = "";            
            string ReturnCheckSum = "";
            string ApiStatusCode = string.Empty;
            string ApiDescription = string.Empty;
            //string OfferType = "";
            //string OfferCode = "";            
            //string pgResponse = string.Empty;
            //string apiStatus = string.Empty;           
            //Status result = new Status();

            try
            {

                //DataTable dt = new DataTable();
                //dt = GetPgCredential();
                //if (dt != null)
                //{
                //    workingKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                //}

                #region parse ZaakPay Response

                // string Parameters = "amount=" + amount + "&buyerEmail=" + buyerEmail + "&currency=" + currency + "&merchantIdentifier=" + merchantIdentifier + "&merchantIpAddress=" + IP + "&mode=" + mode + "&orderId=" + orderId + "&purpose=" + purpose + "&returnUrl=" + returnUrl + "&txnType=" + txnType + "&zpPayOption=" + zpPayOption + "&";

                ////ccaRequest = "" + merchantIdentifier + "|" + MERC_UNQ_REF + "|" + secretKey + "|" + CHANNEL_ID + "|" + INDUSTRY_TYPE_ID + "|" + WEBSITE + "|" + orderId + "|" + Convert.ToDecimal(TotalAmount).ToString("g29") + "|" + ServiceType + "|" + BillingName + "|" + buyerEmail + "|||||||||||" + SALT + "";
                // strEncRequest = ChecksumCalculator.calculateChecksum(secretKey, Parameters);

                System.Collections.Specialized.NameValueCollection Params = new System.Collections.Specialized.NameValueCollection();
                string[] segments = PgResponse.Split('&');

                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        if (Key == "orderId")
                            OrderId = parts[1].Trim();

                        if (Key == "responseCode")
                            StatusCode = parts[1].Trim();
                        if (Key == "responseDescription")
                            ErrorText = parts[1].Trim();

                        if (Key == "amount")
                            PgAmount = parts[1].Trim();

                        if (Key == "doRedirect")
                            DoRedirect = parts[1].Trim();

                        if (Key == "paymentMode")
                            PaymentMode = parts[1].Trim();

                        if (Key == "paymentMethod")
                            PaymentMethod = parts[1].Trim();

                        if (Key == "cardhashid")
                            CardHashedId = parts[1].Trim();
                        if (Key == "checksum")
                            ReturnCheckSum = parts[1].Trim();


                        Params.Add(Key, Value);
                    }
                }
                #endregion
                #region Check Return Checksum of Momiwik
                string checksumNew = "";
                DataTable dt = new DataTable();
                dt = GetPgCredential();                
                #endregion

                //Below Condition Removed When cross check payment status working  fine
                if (1 == 2)
                { 
                #region Cross check of payment status
                try
                {

                    //DataTable dt = new DataTable();
                    //dt = GetPgCredential();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string Url = Convert.ToString(dt.Rows[0]["WebServiceUrl"]);
                            string MID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                            string salt = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"eCwWELxi";
                            string key = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"gtKFFx";//"2932d5d5c8b845d9a249a5fde033899e"; 
                            string mode = "0";
                            string var1 = OrderId;

                                string PaymentOption = GetPaymentMode(OrderId,AgentId);
                                if (PaymentOption.ToUpper() == "MCCONLY")
                                {
                                    Url = Convert.ToString(dt.Rows[0]["WebServiceUrl"]);
                                    MID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                                    salt = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"eCwWELxi";
                                    key = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                                }
                                #region 02-12-2020 18:37 Devesh
                                //        If you are posting in order like merchantIdentifier, orderId & mode
                                //        Input Parameter string :
                                //'b19e8f103bce406cbd3476431b6b7973''ZPK12345''0'

                                //string allParamValue = "'b19e8f103bce406cbd3476431b6b7973''ZPK12345''0'";
                                //string secretKey = "0678056d96914a8583fb518caf42828a";
                                //string oldCheckSum = "94681259256bc24e3c6881fe85e0fd61cf1b41a0e650c9ca6b7b7fe7ae510af4";
                                //string checksum = ChecksumCalculatorResponse.calculateChecksum(secretKey, allParamValue);
                                //if (oldCheckSum == checksum)
                                //{

                                //}
                                string key1 = "a";
                            string Key2 = "a";
                            if (key1 == Key2)
                            {
                                key = "2932d5d5c8b845d9a249a5fde033899e";
                            }

                            string allParamValue = "'" + MID + "'" + "'" + OrderId + "'" + "'" + mode + "'";
                            Url = "https://api.zaakpay.com/checktransaction?v=5";
                            string CalChecksum1 = ChecksumCalculator.calculateChecksum(key, allParamValue);
                            ApiRequest += "<form method='post' action='https://api.zaakpay.com/checktransaction?v=5'>";// name='f1'>";//" + orderId + " name='f1'
                            //ApiRequest += "<table border='1'>";
                            //ApiRequest += "<tbody>";
                            ApiRequest += "<input type='hidden' name= 'merchantIdentifier' value='" + MID + "'>";
                            ApiRequest += "<input type='hidden' name= 'orderId' value='" + OrderId + "'>";
                            ApiRequest += "<input type='hidden' name= 'mode' value='0'>";
                            //ApiRequest += "<input type='hidden' name= 'mode' value='" + mode + "'>";
                            ApiRequest += "<input type='hidden' name='checksum' value='" + CalChecksum1 + "'>";
                            //ApiRequest += "</tbody>";
                            //ApiRequest += "</table>";
                            //ApiRequest += "<script type='text/javascript'>";
                            //ApiRequest += "document.f1.submit();";
                            //ApiRequest += "</script>";
                            ApiRequest += "</form>";

                            srvMPG objPgtt = new srvMPG();
                            //string response = objPg.GetPostReqRes(ApiRequest, Url, ToHash, Hashed, OrderId, method);
                            string response99 = objPgtt.GetPostReqRes(ApiRequest, Url, "", "", OrderId, "");
                            #endregion


                            //string method = "checktransaction?v=5&";
                            string method = MID + OrderId + mode;
                            // string allParamValue = "merchantIdentifier=" + MID + "&orderId=" + OrderId + "mode=" + mode+ "&"; //+ "&buyerEmail=" + buyerEmail + "&currency=" + currency + "&merchantIpAddress=" + IP + "&mode=" + mode + "&purpose=" + purpose + "&returnUrl=" + returnUrl + "&txnType=" + txnType + "&zpPayOption=" + zpPayOption + "&";
                            string CalChecksum = ChecksumCalculator.calculateChecksum(key, allParamValue);
                            allParamValue = allParamValue + "&checksum=" + CalChecksum;
                            Url = "https://api.zaakpay.com/checktransaction?v=5&" + allParamValue;

                            //https://api.zaakpay.com/checktransaction?v=5&merchantIdentifier=a4f55c7c338d41d9aaefc1fc66b8e934&orderId=MWK1126143935733594&mode=0&checksum=c8a23cd2b54975506d667ac9da83def60e6599c1e76789269af5c99df2fa0f17';

                            //
                            //string Parameters = "amount=" + TotalAmount * 100 + "&buyerEmail=" + buyerEmail + "&currency=" + currency + "&merchantIdentifier=" + merchantIdentifier + "&merchantIpAddress=" + IP + "&mode=" + mode + "&orderId=" + orderId + "&purpose=" + purpose + "&returnUrl=" + returnUrl + "&txnType=" + txnType + "&zpPayOption=" + zpPayOption + "&";
                            //ccaRequest = Parameters;
                            //// ccaRequest = "" + merchantIdentifier + "|" + MERC_UNQ_REF + "|" + secretKey + "|" + CHANNEL_ID + "|" + INDUSTRY_TYPE_ID + "|" + WEBSITE + "|" + orderId + "|" + Convert.ToDecimal(TotalAmount).ToString("g29") + "|" + ServiceType + "|" + BillingName + "|" + buyerEmail + "|||||||||||" + SALT + "";
                            //strEncRequest = ChecksumCalculator.calculateChecksum(secretKey, Parameters);

                            //
                            //Add Devesh - 01-12-2020
                            string jsonReq = "{\"merchantIdentifier\":\"" + MID + "\",\"mode\":\"" + mode + "\",\"orderDetail\":{\"orderId\":\"" + OrderId + "\"}}";

                            string CalChecksumJson = ChecksumCalculator.calculateChecksum(key, allParamValue);
                            string UrlJson = "https://api.zaakpay.com/checktransaction?v=5";
                            srvMPG objPgm = new srvMPG();
                            string response1 = objPgm.GetFromApiServiceHttpClient(UrlJson, jsonReq, CalChecksumJson);


                            #region XMLRequestResponse
                            ApiRequest += "<form method='post' action='https://api.zaakpay.com/checktransaction?v=5' name='f1'>";//" + orderId + " name='f1'
                            ApiRequest += "<table border='1'>";
                            ApiRequest += "<tbody>";
                            ApiRequest += "<input type='hidden' name= 'merchantIdentifier' value='" + MID + "'>";
                            ApiRequest += "<input type='hidden' name= 'orderId' value='" + OrderId + "'>";
                            ApiRequest += "<input type='hidden' name= 'mode' value='0'>";
                            //ApiRequest += "<input type='hidden' name= 'mode' value='" + mode + "'>";
                            ApiRequest += "<input type='hidden' name='checksum' value='" + CalChecksum + "'>";
                            ApiRequest += "</tbody>";
                            ApiRequest += "</table>";
                            ApiRequest += "<script type='text/javascript'>";
                            ApiRequest += "document.f1.submit();";
                            ApiRequest += "</script>";
                            ApiRequest += "</form>";

                            #endregion
                            string ToHash = MID + "|" + OrderId + "|" + mode + "|" + salt + "|" + method + "|" + key;
                            string Hashed = Generatehash512(ToHash);
                            //toDO
                            srvMPG objPg = new srvMPG();
                            //string response = objPg.GetPostReqRes(ApiRequest, Url, ToHash, Hashed, OrderId, method);
                            string response = objPg.GetPostReqRes(ApiRequest, Url + method, ToHash, Hashed, OrderId, method);
                            ApiEncryptRequest = Hashed;
                            ApiResponse = response;


                            ApiDescription = objPg.GetValueFromXml(response, "response_element");
                            ApiStatusCode = objPg.GetValueFromXml(response, "responsecode");




                        }
                    }
                }
                catch (Exception expg)
                {
                    int insert = InsertExceptionLog("ZaakPaymentGateway", "ZaakPaymentGateway", "UpdatePaymentResponseDetails-GetPostReqResPayU", "GetApiRequest", expg.Message, expg.StackTrace);
                }
                #endregion
                }
                #region Update PG Response
                //responseCode=100&responseDescription=The+transaction+was+completed+successfully.
                string CheckSumStatus = "Checksum match failure";
                string Status = "Failure";
                string ResponseMessage = "Failure";
                ApiStatus = "failure";
                if (!string.IsNullOrEmpty(ReturnCheckSum) && !string.IsNullOrEmpty(checksumNew) && ReturnCheckSum == checksumNew)
                {
                    CheckSumStatus= "success";
                }
                CheckSumStatus = "success";
                if (StatusCode=="100")
                {
                    Status = "success";
                    ResponseMessage= "Post-" + ErrorText;
                }
                //if (ApiStatusCode == "100")
                //{
                    ApiStatus = "success";
                //    ResponseMessage =  ErrorText + " Api-" + ApiDescription;
                //}
                if (Status == "success" && ApiStatus == "success" && CheckSumStatus == "success")
                {
                    UnmappedStatus = "captured";
                }
                else
                {
                    ErrorText = "Post-" + ErrorText + " Api-" + ApiDescription;
                }
                PaymentId = OrderId;
                BankRefNo = OrderId;
                if (con.State != ConnectionState.Open)                   
                    con.Open();
                                
                cmd = new SqlCommand("SpInsertPaymentDetails", con);//spResponseData
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrackId", OrderId);
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@ResponseMessage", ResponseMessage);
                cmd.Parameters.AddWithValue("@ResponseCode", StatusCode);
                cmd.Parameters.AddWithValue("@ErrorText", ErrorText);
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
                cmd.Parameters.AddWithValue("@BankRefNo", BankRefNo);
                //cmd.Parameters.AddWithValue("@PgAmount", Convert.ToDouble(PgAmount));
                cmd.Parameters.AddWithValue("@PgAmount", Convert.ToDouble(PgAmount) / 100);// As per Mobiwike logic  amount divided by 100
                cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode);
                cmd.Parameters.AddWithValue("@CardName", CardName);
                cmd.Parameters.AddWithValue("@DiscountValue", Convert.ToDouble(string.IsNullOrEmpty(DiscountValue) ? "0.0" : DiscountValue));
                cmd.Parameters.AddWithValue("@MerAamount", Convert.ToDouble(string.IsNullOrEmpty(MerAamount) ? "0.0" : MerAamount));
                cmd.Parameters.AddWithValue("@CardType", CardType);
                cmd.Parameters.AddWithValue("@IssuingBank", IssuingBank);
                cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
                cmd.Parameters.AddWithValue("@UnmappedStatus", UnmappedStatus);
                //cmd.Parameters.AddWithValue("@TId", );
                //cmd.Parameters.AddWithValue("@OfferCode", OfferCode);
                cmd.Parameters.AddWithValue("@ApiRequest", ApiRequest);
                cmd.Parameters.AddWithValue("@ApiResponse", ApiResponse);
                cmd.Parameters.AddWithValue("@ApiStatus", ApiStatus);
                cmd.Parameters.AddWithValue("@ApiEncryptRequest", ApiEncryptRequest);
                cmd.Parameters.AddWithValue("@Action", "update");
                temp = cmd.ExecuteNonQuery();
                #endregion 
                
            }
                
            catch (Exception ex)
            {
                if (temp > 0)
                {
                    msg = "yes~" + OrderId;
                }
                else
                {
                    msg = "no~" + OrderId;
                }
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
                //ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightBooking");
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            if (temp > 0)
            {
                msg = "yes~" + OrderId;
            }
            return msg;
        }
        public int InsertWebServiceLog(string OrderId, string ApiName, string ToHash, string Hashed, string Request, string Response, string Status, string ExMessage, string ExStackTrace)
        {
            int temp = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                con.Open();
                cmd = new SqlCommand("SpInsertPgWebserviceLog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                cmd.Parameters.AddWithValue("@ApiName", ApiName);
                cmd.Parameters.AddWithValue("@ToHash", ToHash);
                cmd.Parameters.AddWithValue("@Hashed", Hashed);
                cmd.Parameters.AddWithValue("@Request", Request);
                cmd.Parameters.AddWithValue("@Response", Response);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@ExMessage", ExMessage);
                cmd.Parameters.AddWithValue("@ExStackTrace", ExStackTrace);
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                temp = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "InsertWebServiceLog", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        public string GetPaymentMode(string ReferenceNo, string AgentId)
        {
            string PaymentMode = string.Empty;
            try
            {
                #region Get details and Login
                if (!string.IsNullOrEmpty(ReferenceNo))
                {
                    DataSet ds = GetPGPaymentMode(ReferenceNo,AgentId);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)//&& !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                    {
                        PaymentMode = Convert.ToString(ds.Tables[0].Rows[0]["PaymentMode"]);
                        //Login(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]), Convert.ToString(ds.Tables[0].Rows[0]["PWD"]));
                    }
                    else
                    {
                        int insert = InsertExceptionLog("Mobikwik", "GetPaymentMode", "", AgentId, ReferenceNo, "");
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("Mobikwik", "PGResponsParse", "PG Response is null or RefernceNo not found", AgentId, ReferenceNo  + "~" + ex.Message, ex.StackTrace);
            }
            return PaymentMode;

        }

        public DataSet GetPGPaymentMode(string ReferenceNo,string AgentId)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                adap = new SqlDataAdapter("SpInsertPaymentDetails", con);
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@TrackId", ReferenceNo);
                adap.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
                adap.SelectCommand.Parameters.AddWithValue("@Action", "GETPGMODE");
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("Mobikwik", "GetPGPaymentMode", "PG Response is null or RefernceNo not found", AgentId, ReferenceNo  + "~" + ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                adap.Dispose();
            }
            return ds;
        }


        //public bool Check()
        //{
        //    String secretKey = "Insert your secret key here";
        //    String allParamValue = null;
        //    Boolean isChecksumValid = false;
        //    allParamValue = ChecksumResponse.ChecksumCalculatorResponse.getAllNotEmptyParamValue(Request).Trim();
        //    String checksum = ChecksumResponse.ChecksumCalculatorResponse.calculateChecksum(secretKey, allParamValue);
        //    System.Diagnostics.Debug.WriteLine("allParamValue Response : " + allParamValue);
        //    System.Diagnostics.Debug.WriteLine("secretKey Response : " + secretKey);
        //    if (checksum != null)
        //    {

        //        isChecksumValid = ChecksumResponse.ChecksumCalculatorResponse.verifyChecksum(secretKey, allParamValue, checksum);

        //    }
        //}

    }

    public class ResponseData
    {
        public double Amount { get; set; }
        public string bank { get; set; }
        public string bankid { get; set; }
        public string cardId { get; set; }
        public string cardScheme { get; set; }
        public string cardToken { get; set; }
        public string cardhashId { get; set; }
        public string doRedirect { get; set; }
        public string orderId { get; set; }
        public string paymentmethod { get; set; }
        public string paymentMode { get; set; }
        public int responseCode { get; set; }
        public string responseDescription { get; set; }
        public string checksum { get; set; }
    }
    public class ResponseData1
    {
        public double merchantIdentifier { get; set; }
        public string orderId { get; set; }
        public string returnUrl { get; set; }
        public string buyerEmail { get; set; }
        public BuyerDetails buyerDetails { get; set; }
        public string txnType { get; set; }
        public string zpPayOption { get; set; }

        public string mode { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string merchantIpAddress { get; set; }
        public string purpose { get; set; }
        public string productDescription { get; set; }
        public DateTime txnDate { get; set; }
    }
    public class BuyerDetails
    {
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
        public string buyerAddress { get; set; }
        public string buyerCity { get; set; }
        public string buyerState { get; set; }
        public string buyerCountry { get; set; }
        public string buyerPincode { get; set; }
        public string buyerPhoneNumber { get; set; }
        public string buyerEmail { get; set; }
    }
}
