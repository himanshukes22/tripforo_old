using CCA.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace PG
{
    public class PaymentGateway
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adap;
        public string PaymentGatewayReq(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
        {
            //string pgReq = "tid=" + TId + "&merchant_id=103816&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://localhost:1044/ccavResponseHandler.aspx&cancel_url=http://localhost:1044/ccavResponseHandler.aspx&billing_name=Charli&billing_address=Room no 1101, near Railway station Ambad&billing_city=Indore&billing_state=MP&billing_zip=425001&billing_country=India&billing_tel=9899622605&billing_email=test@gmail.com&delivery_name=Chaplin&delivery_address=room no.701 near bus stand&delivery_city=Hyderabad&delivery_state=Andhra&delivery_zip=425001&delivery_country=India&delivery_tel=9871186224&merchant_param1=additional Info.&merchant_param2=additional Info.&merchant_param3=additional Info.&merchant_param4=additional Info.&merchant_param5=additional Info.&promo_code=&customer_identifier=ITZ101&";
            //IBTrackId=order_id
            string MerchantID = "";
            string AccessCode = "";
            string WorkingKey = "";
            string PgURL = "";
            string RedirectURL = "";
            string CancelUrl = "";
            string postHtml = "";
            int flag = 0;
            string ccaRequest = "";
            string strEncRequest = "";
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);

            // select  @TotalAmount as TotalAmount,@TransCharges as Charges,@ChargesType as ChargesType,@PgCharges as PgCharges
            double TransCharges = 0;
            string ChargesType = "";
            double TotalPgCharges = 0;
            try
            {
                //PaymentGateway objPg = new PaymentGateway();
                CCACrypto ccaCrypto = new CCACrypto();
                DataTable pgDT = GetTotalAmountWithPgCharge(PaymentOption, OrignalAmount, AgentId, TrackId);
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
                        MerchantID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                        AccessCode = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                        WorkingKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                        PgURL = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                        RedirectURL = Convert.ToString(dt.Rows[0]["SuccessUrl"]);
                        CancelUrl = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                        //ccaRequest = "tid=" + TId + "&merchant_id=" + MerchantID + "&order_id=" + TrackId + "&amount=" + Convert.ToString(TotalAmount) + "&currency=INR&redirect_url=" + RedirectURL + "&cancel_url=" + CancelUrl + "&billing_name=" + BillingName + "&billing_address=" + BillingAddress + "&billing_city=" + BillingCity + "&billing_state=" + BillingState + "&billing_zip=" + BillingZip + "&billing_country=India&billing_tel=" + BillingTel + "&billing_email=" + BillingEmail + "&delivery_name=" + BillingName + "&delivery_address=" + BillingAddress + "&delivery_city=" + BillingCity + "&delivery_state=" + BillingState + "&delivery_zip=" + BillingZip + "&delivery_country=India&delivery_tel=" + BillingTel + "&merchant_param1=additional Info.&merchant_param2=additional Info.&merchant_param3=additional Info.&merchant_param4=additional Info.&merchant_param5=additional Info.&promo_code=&customer_identifier=" + AgentId + "&";
                        ccaRequest = "tid=" + TId + "&merchant_id=" + MerchantID + "&order_id=" + TrackId + "&amount=" + Convert.ToString(TotalAmount) + "&currency=INR&redirect_url=" + RedirectURL + "&cancel_url=" + CancelUrl + "&billing_name=" + BillingName + "&billing_address=" + BillingAddress + "&billing_city=" + BillingCity + "&billing_state=" + BillingState + "&billing_zip=" + BillingZip + "&billing_country=India&billing_tel=" + BillingTel + "&billing_email=" + BillingEmail + "&delivery_name=" + BillingName + "&delivery_address=" + BillingAddress + "&delivery_city=" + BillingCity + "&delivery_state=" + BillingState + "&delivery_zip=" + BillingZip + "&delivery_country=India&delivery_tel=" + BillingTel + "&merchant_param1=additional Info.&merchant_param2=additional Info.&merchant_param3=additional Info.&merchant_param4=additional Info.&merchant_param5=additional Info.&payment_option=" + PaymentOption + "&promo_code=&customer_identifier=" + AgentId + "&";
                        strEncRequest = ccaCrypto.Encrypt(ccaRequest, WorkingKey);
                        flag = InsertPaymentRequestDetails(TrackId, TId, IBTrackId, BillingName, "CCAvenue", BillingEmail, BillingTel, BillingAddress, TotalAmount, OrignalAmount, AgentId, AgencyName, IP, ccaRequest, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, "");
                        //tid=1469616295300&merchant_id=103816&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://localhost:1044/ccavResponseHandler.aspx&cancel_url=http://localhost:1044/ccavResponseHandler.aspx&billing_name=Charli&billing_address=Room no 1101, near Railway station Ambad&billing_city=Indore&billing_state=MP&billing_zip=425001&billing_country=India&billing_tel=9899622605&billing_email=test@gmail.com&delivery_name=Chaplin&delivery_address=room no.701 near bus stand&delivery_city=Hyderabad&delivery_state=Andhra&delivery_zip=425001&delivery_country=India&delivery_tel=9896426054&merchant_param1=additional Info.&merchant_param2=additional Info.&merchant_param3=additional Info.&merchant_param4=additional Info.&merchant_param5=additional Info.&promo_code=&customer_identifier=AGENT1234&
                        //    </html>"; //  action='https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction'
                        postHtml = @"<form id='nonseamless' method='post' name='redirect' action='" + PgURL + "'>";
                        postHtml += @"<input type='hidden' id='encRequest' name='encRequest' value='" + strEncRequest + "'/>";
                        postHtml += @"<input type='hidden' name='access_code' id='Hidden1' value='" + AccessCode + "'/>";
                        postHtml += @"</form>";
                    }
                }
                if (flag > 0)
                {
                    postHtml = "yes~" + postHtml;
                }
                else
                {
                    postHtml = "no~" + postHtml;
                }
            }
            catch (Exception ex)
            {
                postHtml = "no~" + ex.Message;
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PaymentGatewayReq", "insert", ex.Message, ex.StackTrace);
                return postHtml;
            }

            return postHtml;
        }
        public DataTable GetPgCredential()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            string provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("Sp_Get_PgCredentials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pvd", provider);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                //adap.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return dt;
        }
        public int InsertPaymentRequestDetails(string TrackId, string TId, string IBTrackId, string Name, string PaymentGateway, string Email, string Mobile, string Address, double TotalAmount, double OriginalAmount, string AgentId, string AgencyName, string Ip, string PgRequest, string ServiceType, string EncRequest, string Trip, double TotalPgCharges, double TransCharges, string ChargesType, string PostHtml)
        {
            int temp = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                //Name, TrackId, PaymentGateway, Email, Mobile, Address, Amount, OriginalAmount, AgentId, AgencyName, Status, Ip
                con.Open();
                cmd = new SqlCommand("SpInsertPaymentDetails", con);
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
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "InsertPaymentRequestDetails", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }
        public string UpdatePaymentResponseDetails(string AgentId, string PgResponse)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            int temp = 0;
            string OrderId = string.Empty;
            string PaymentId = string.Empty;
            string Status = string.Empty;
            string BankRefNo = string.Empty;
            string ErrorText = string.Empty;
            string ResponseCode = string.Empty;
            string PaymentMode = string.Empty;
            string CardType = string.Empty;
            string IssuingBank = string.Empty;
            string CardName = string.Empty;
            //string name_on_card = "";
            string CardNumber = string.Empty;
            string UnmappedStatus = string.Empty;
            string PgAmount = "0.0";
            string DiscountValue = "0.0";
            string MerAamount = "0.0";
            string msg = "no~" + OrderId;
            string ApiRequest = string.Empty;
            string ApiResponse = string.Empty;
            string ApiStatus = string.Empty;
            string ApiEncryptRequest = string.Empty;
            //string ResponseMessage = "";            
            //string workingKey = "";           
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

                #region parse PayU Response
                System.Collections.Specialized.NameValueCollection Params = new System.Collections.Specialized.NameValueCollection();
                string[] segments = PgResponse.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        if (Key == "txnid")
                            OrderId = parts[1].Trim();

                        if (Key == "mihpayid")
                            PaymentId = parts[1].Trim();

                        if (Key == "bank_ref_num")
                            BankRefNo = parts[1].Trim();

                        if (Key == "status")
                            Status = parts[1].Trim();

                        if (Key == "error_Message")
                            ErrorText = parts[1].Trim();

                        if (Key == "error")
                            ResponseCode = parts[1].Trim();

                        if (Key == "mode")
                            PaymentMode = parts[1].Trim();

                        if (Key == "card_type")
                            CardType = parts[1].Trim();

                        if (Key == "issuing_bank")
                            IssuingBank = parts[1].Trim();

                        if (Key == "name_on_card")
                            CardName = parts[1].Trim();

                        if (Key == "cardnum")
                            CardNumber = parts[1].Trim();

                        if (Key == "unmappedstatus")
                            UnmappedStatus = parts[1].Trim();

                        if (Key == "amount")
                            PgAmount = parts[1].Trim();
                        if (Key == "discount")
                            DiscountValue = parts[1].Trim();
                        if (Key == "net_amount_debit")
                            MerAamount = parts[1].Trim();

                        Params.Add(Key, Value);
                    }
                }
                #endregion

                #region Cross check of payment status

                try
                {

                    DataTable dt = new DataTable();
                    dt = GetPgCredential();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string Url = Convert.ToString(dt.Rows[0]["WebServiceUrl"]);//"https://test.payu.in/merchant/postservice.php?form=2";
                            string method = "verify_payment";
                            string salt = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"eCwWELxi";
                            string key = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"gtKFFx";
                            string var1 = OrderId;//Transaction ID of the merchant
                            // gtKFFx|verify_payment|e30478dzpd1Ut4d|eCwWELxi
                            string ToHash = key + "|" + method + "|" + var1 + "|" + salt;
                            string Hashed = Generatehash512(ToHash);
                            ApiRequest = "key=" + key +
                                "&command=" + method +
                                "&hash=" + Hashed +
                                "&var1=" + var1;// +
                            //"&var2=" + var2 +
                            //"&var3=" + var3;
                            srvPG objPg = new srvPG();
                            string response = objPg.GetPostReqResPayU(ApiRequest, Url, ToHash, Hashed, OrderId, method);
                            ApiEncryptRequest = Hashed;
                            ApiResponse = response;
                            Newtonsoft.Json.Linq.JObject account = Newtonsoft.Json.Linq.JObject.Parse(response);
                            ApiStatus = (string)account.SelectToken("transaction_details." + var1 + ".status");

                        }
                    }
                }
                catch (Exception expg)
                {
                    int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "UpdatePaymentResponseDetails-GetPostReqResPayU", "GetApiRequest", expg.Message, expg.StackTrace);
                }
                #endregion                

                #region Update PG Response
                con.Open();
                cmd = new SqlCommand("SpInsertPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrackId", OrderId);
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@ResponseMessage", PgResponse);
                cmd.Parameters.AddWithValue("@ResponseCode", ResponseCode);
                cmd.Parameters.AddWithValue("@ErrorText", ErrorText);
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
                cmd.Parameters.AddWithValue("@BankRefNo", BankRefNo);
                cmd.Parameters.AddWithValue("@PgAmount", Convert.ToDouble(PgAmount));
                cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode);
                cmd.Parameters.AddWithValue("@CardName", CardName);
                cmd.Parameters.AddWithValue("@DiscountValue", Convert.ToDouble(DiscountValue));
                cmd.Parameters.AddWithValue("@MerAamount", Convert.ToDouble(MerAamount));
                cmd.Parameters.AddWithValue("@CardType", CardType);
                cmd.Parameters.AddWithValue("@IssuingBank", IssuingBank);
                cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
                cmd.Parameters.AddWithValue("@UnmappedStatus", UnmappedStatus);
                //cmd.Parameters.AddWithValue("@OfferType", OfferType);
                //cmd.Parameters.AddWithValue("@OfferCode", OfferCode);
                cmd.Parameters.AddWithValue("@ApiRequest", ApiRequest);
                cmd.Parameters.AddWithValue("@ApiResponse", ApiResponse);
                cmd.Parameters.AddWithValue("@ApiStatus", ApiStatus);
                cmd.Parameters.AddWithValue("@ApiEncryptRequest", ApiEncryptRequest);

                cmd.Parameters.AddWithValue("@Action", "update");
                temp = cmd.ExecuteNonQuery();
            }
            #endregion
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
        public DataSet GetPgRequestAndCredential(string TrackId)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataSet ds = new DataSet();
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@TrackId", TrackId);
                adp.SelectCommand.Parameters.AddWithValue("@Provider", Provider);
                // adp.SelectCommand.Parameters.AddWithValue("@Action", "getcrd");
                adp.SelectCommand.Parameters.AddWithValue("@Action", "GetPgCrd");
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgRequestAndCredential", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                //cmd.Dispose();
            }
            return ds;
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
        public string GetCCAvenPaymentStatus(string ReferenceNo, string OrderNo, string AgentId)
        {
            string message = "not";
            string pgResponse = string.Empty;
            Status result = new Status();
            try
            {
                //orderStatusTracker
                //order_no= TrackId  //database
                //reference_no= PaymentId //database
                //{"reference_no": "305002833041","order_no": "cddf223bzdrMT5oW"}
                string pgReq = "{\"reference_no\": \"" + ReferenceNo + "\",\"order_no\": \"" + OrderNo + "\"}";
                srvPG objPg = new srvPG();
                pgResponse = objPg.GetPostReqResCCAvenue(pgReq, "orderStatusTracker", "");
                string[] pgApiRes = pgResponse.Split('~');
                string pgApiStatus = pgApiRes[0];
                string CCAvRes = pgApiRes[1];
                if (!string.IsNullOrEmpty(pgResponse) && pgApiStatus == "0")
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(pgResponse);
                    if (result.error_code != null)
                    {
                        message = result.error_desc + " - " + result.error_code;
                    }
                    else
                    {
                        message = result.order_status;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetCCAvenPaymentStatus", "SELECT", ex.Message, ex.StackTrace);
            }
            return message;
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
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetTotalAmountWithPgCharge", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetPgTransCharges()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_PGTransCharge", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Action", "GetPgCharges");
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgTransCharges", "SELECT", ex.Message, ex.StackTrace);
            }
            return dt;
        }

        public DataTable GetPgTransChargesByMode(string PaymentMode, string ActionType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SP_PGTransCharge", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@PaymentCode", PaymentMode);
                adp.SelectCommand.Parameters.AddWithValue("@Action", ActionType);
                //adp.SelectCommand.Parameters.AddWithValue("@Action", "PriceDetails");
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgTransChargesByMode", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetPgTransChargesByModeByAgentWise(string UserId, string PaymentMode, string ActionType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SP_PGTransCharge", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@PaymentCode", PaymentMode);
                adp.SelectCommand.Parameters.AddWithValue("@AgentId", UserId);
                adp.SelectCommand.Parameters.AddWithValue("@Action", ActionType);
                //adp.SelectCommand.Parameters.AddWithValue("@Action", "PriceDetails");
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgTransChargesByMode", "SELECT", ex.Message, ex.StackTrace);
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
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                con.Open();
                cmd = new SqlCommand("SpInsertExceptionLog", con);
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
                cmd.Dispose();
            }
            return temp;
        }
        #region Payment API Method Added Date 15 Nov 2016

        public string PgRefundAmount(string OrderId, string ServiceType, string TripType, double RefundAmount, string AgentId, string RefundRemark, string RefundedBy, string RefundFor, double PgCharges, string PaxId, double TDS, string AgencyName)
        {
            string msg = string.Empty;
            string ApiEncryptRequest = string.Empty;
            string ApiDecryptRespone = string.Empty;
            string ApiEncryptRespone = string.Empty;
            string pgApiStatus = string.Empty;
            string CommandType = string.Empty;
            double TotalDebitAmount = 0;

            string RefundRefNo = "";
            Refund result = new Refund();
            string RefundStatus = "InProcess";
            string FailureReason = string.Empty;
            string ErrorCode = string.Empty;
            string OrderStatus = string.Empty;
            string ReferenceNo = string.Empty;
            string PgStatus = string.Empty;
            string PgApi = string.Empty;


            try
            {
                if (!string.IsNullOrEmpty(OrderId))
                {
                    DataSet ds = new DataSet();
                    ds = PgPaymentDetails(OrderId, "", "");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ReferenceNo = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                        PgStatus = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                        PgApi = Convert.ToString(ds.Tables[0].Rows[0]["ApiStatus"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PgAmount"])))
                        {
                            TotalDebitAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["PgAmount"]);
                        }
                        if (PgStatus == "Success")
                        {
                            if (TotalDebitAmount >= RefundAmount)
                            {
                                #region Pg Amount Refund
                                string RandomNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                RefundRefNo = "ITZR" + RandomNo.Substring(4, 16);
                                string RequestJson = "{\"reference_no\":\"" + ReferenceNo + "\",\"refund_amount\":\"" + Convert.ToString(RefundAmount) + "\",\"refund_ref_no\":\"" + RefundRefNo + "\"}";
                                srvPG objPg = new srvPG();
                                try
                                {
                                    OrderStatus = PgRefundStatus(Convert.ToString(ds.Tables[0].Rows[0]["TrackId"]), ReferenceNo, AgentId);
                                    //if (!string.IsNullOrEmpty(OrderStatus) && OrderStatus != "Refunded" && OrderStatus == "Successful")
                                    if (!string.IsNullOrEmpty(OrderStatus) && (OrderStatus == "Refunded" || OrderStatus == "Successful" || OrderStatus == "Shipped"))
                                    {
                                        CommandType = "refundOrder";
                                        ApiEncryptRequest = GetEncryptRequest(RequestJson, CommandType);//json  Encrypt
                                        ApiEncryptRespone = objPg.GetPostCCAvenueService(ApiEncryptRequest, CommandType, OrderId);//Request for Refund to CCAvenue
                                                                                                                                  //OrderStatus = PgRefundStatus(RefundRefNo, ReferenceNo, AgentId);
                                        if (!string.IsNullOrEmpty(ApiEncryptRespone) && ApiEncryptRespone != "catch")
                                        {
                                            ApiDecryptRespone = GetDecryptResponse(ApiEncryptRespone);
                                            string[] pgApiRes = ApiDecryptRespone.Split('~');
                                            pgApiStatus = pgApiRes[0];
                                            if (!string.IsNullOrEmpty(ApiDecryptRespone) && pgApiStatus == "0")
                                            {
                                                string CCAvRes = pgApiRes[1];
                                                result = Newtonsoft.Json.JsonConvert.DeserializeObject<Refund>(CCAvRes);
                                                //if (!string.IsNullOrEmpty(result.error_code))
                                                //{                              
                                                //  FailureReason=result.reason;
                                                //  ErrorCode ="refund_status: "+ result.refund_status+ "refund_status: "+  result.error_code;
                                                //}                           
                                                if (result.refund_status == 0)
                                                {
                                                    RefundStatus = "Refunded";
                                                    msg = "Refunded";
                                                }
                                                else if (result.refund_status == 1)
                                                {
                                                    RefundStatus = "Failure";
                                                    msg = "Failure";
                                                }
                                                else
                                                {
                                                    RefundStatus = "InProcess";
                                                    msg = "InProcess";
                                                }

                                                FailureReason = result.reason;
                                                ErrorCode = "refund_status: " + Convert.ToString(result.refund_status) + ", error_code: " + result.error_code;
                                            }
                                            int ins = InsertPgRefundDetails(OrderId, ReferenceNo, RefundRefNo, AgentId, AgencyName, RefundAmount, RefundRemark, pgApiStatus, RefundStatus, FailureReason, ErrorCode, RequestJson, ApiEncryptRequest, ApiEncryptRespone, ApiDecryptRespone, RefundedBy, OrderStatus, RefundFor, ServiceType, "insert", PgCharges, PaxId, TDS);
                                        }
                                        else
                                        {
                                            RefundStatus = "InProcess";
                                            msg = "InProcess";
                                            if (ApiEncryptRespone == "catch")
                                            {
                                                int ins = InsertPgRefundDetails(OrderId, ReferenceNo, RefundRefNo, AgentId, "", RefundAmount, RefundRemark, pgApiStatus, RefundStatus, FailureReason, "Exception in GetPostCCAvenueService,check Table- ExceptionLog ", RequestJson, ApiEncryptRequest, ApiEncryptRespone, ApiDecryptRespone, RefundedBy, OrderStatus, RefundFor, ServiceType, "insert", PgCharges, PaxId, TDS);
                                            }
                                            else
                                            {
                                                int ins = InsertPgRefundDetails(OrderId, ReferenceNo, RefundRefNo, AgentId, "", RefundAmount, RefundRemark, pgApiStatus, RefundStatus, FailureReason, ErrorCode, RequestJson, ApiEncryptRequest, ApiEncryptRespone, ApiDecryptRespone, RefundedBy, OrderStatus, RefundFor, ServiceType, "insert", PgCharges, PaxId, TDS);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (OrderStatus == "Refunded")
                                        {
                                            msg = "Already refunded";
                                        }
                                        else
                                        {
                                            msg = "Please Try Again!!";
                                            try
                                            {
                                                int ins = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PgRefundAmount-orderstatus", "Status", OrderStatus, OrderStatus);
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch (Exception expg)
                                {
                                    msg = "InProcess";
                                    int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PgRefundAmount-GetPostReqResCCAvenue", "EncryptAndPost", expg.Message, expg.StackTrace);
                                    int ins = InsertPgRefundDetails(OrderId, ReferenceNo, RefundRefNo, AgentId, "", RefundAmount, RefundRemark, pgApiStatus, RefundStatus, FailureReason, ErrorCode, RequestJson, ApiEncryptRequest, ApiEncryptRespone, ApiDecryptRespone, RefundedBy, OrderStatus, RefundFor, ServiceType, "insert", PgCharges, PaxId, TDS);
                                }
                                #endregion
                            }
                            else
                            {
                                msg = "Refund amount cannot exceeds the debit amount!";
                            }
                        }
                        else
                        {
                            msg = "Refund cann't be processed,please do refund from CCAvenue panel!";
                        }
                    }
                    else
                    {
                        msg = "Record not found!";
                    }
                }
                else
                {
                    msg = "Order Id could not be blank!";
                }
            }
            catch (Exception ex)
            {
                msg = "try again --TC101";
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PgRefundAmount", "Refund", ex.Message, ex.StackTrace);
            }
            return msg;
        }
        public string PgRefundStatus(string OrderId, String ReferenceNo, string AgentId)
        {
            #region Cross check of refund status
            string ApiEncryptRequest = string.Empty;
            string pgResponse = string.Empty;
            string apiStatus = string.Empty;
            Status result = new Status();
            //string pgReq = "{\"reference_no\": \"" + PaymentId + "\",\"order_no\": \"" + OrderId + "\"}";
            string JsonReq = "{\"reference_no\": \"" + ReferenceNo + "\",\"order_no\": \"" + OrderId + "\"}";
            srvPG objPg = new srvPG();
            try
            {
                string CommandType = "orderStatusTracker";
                ApiEncryptRequest = GetEncryptRequest(JsonReq, CommandType);
                pgResponse = objPg.GetPostReqResCCAvenue(ApiEncryptRequest, CommandType, AgentId);
                string[] pgApiRes = pgResponse.Split('~');
                string pgApiStatus = pgApiRes[0];
                if (!string.IsNullOrEmpty(pgResponse) && pgApiStatus == "0")
                {
                    string CCAvRes = pgApiRes[1];
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(CCAvRes);
                    if (!string.IsNullOrEmpty(result.error_code))
                    {
                        apiStatus = result.error_desc + " - " + result.error_code;
                    }
                    else
                    {
                        apiStatus = result.order_status;
                    }
                }
            }
            catch (Exception expg)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PgRefundStatus", "Status", expg.Message, expg.StackTrace);
            }
            #endregion
            return apiStatus;
        }

        public DataSet PgPaymentDetails(string TrackId, string AgentID, string ServiceType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SpPgRefundAmount", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@OrderId", TrackId);
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

        public int InsertPgRefundDetails(string OrderId, string ReferenceNo, string RefundRefNo, string AgentId, string AgencyName, double RefundAmount, string RefundRemark,
            string ApiStatus, string RefundStatus, string FailureReason, string ErrorCode, string RequestJson, string EncRequest, string EncResponse, string ApiDecryptRespone, string RefundedBy, string OrderStatus, string RefundFor, string ServiceType, string Action, double PgCharges, string PaxId, double TDS)
        {
            int temp = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                con.Open();
                cmd = new SqlCommand("SpPgRefundAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                cmd.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                cmd.Parameters.AddWithValue("@RefundRefNo", RefundRefNo);
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@AgencyName", AgencyName);
                cmd.Parameters.AddWithValue("@RefundAmount", RefundAmount);
                cmd.Parameters.AddWithValue("@RefundRemark", RefundRemark);
                cmd.Parameters.AddWithValue("@ApiStatus", ApiStatus);
                cmd.Parameters.AddWithValue("@RefundStatus", RefundStatus);
                cmd.Parameters.AddWithValue("@FailureReason", FailureReason);
                cmd.Parameters.AddWithValue("@ErrorCode", ErrorCode);
                cmd.Parameters.AddWithValue("@RequestJson", RequestJson);
                cmd.Parameters.AddWithValue("@EncRequest", EncRequest);
                cmd.Parameters.AddWithValue("@EncResponse", EncResponse);
                cmd.Parameters.AddWithValue("@ResponseJson", ApiDecryptRespone);
                cmd.Parameters.AddWithValue("@RefundedBy", RefundedBy);
                cmd.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                cmd.Parameters.AddWithValue("@RefundFor", RefundFor);
                cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@PgCharge", PgCharges);
                cmd.Parameters.AddWithValue("@PaxId", string.IsNullOrEmpty(PaxId) ? 0 : Convert.ToInt32(PaxId));
                cmd.Parameters.AddWithValue("@TDS", TDS);
                temp = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "InsertPgRefundDetails", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }

        public string GetEncryptRequest(string JsonReq, string commandType)
        {

            string ApiEncryptRequest = string.Empty;
            CCACrypto ccaCrypto = new CCACrypto();
            try
            {
                #region Create Encrypt Api Request

                string AccessCodeApi = Convert.ToString(ConfigurationManager.AppSettings["PGApiAccessCode"]);
                string workingKeyApi = Convert.ToString(ConfigurationManager.AppSettings["PGApiWorkingKey"]);
                string strEncRequest = ccaCrypto.Encrypt(JsonReq, workingKeyApi);
                ApiEncryptRequest = "enc_request=" + strEncRequest + "&access_code=" + AccessCodeApi + "&command=" + commandType + "&request_type=JSON&response_type=JSON&version=1.1";

                #endregion
            }
            catch (Exception ex)
            {
                ApiEncryptRequest = string.Empty;
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetEncryptRequest", "Encrypting", ex.Message, ex.StackTrace);
            }
            return ApiEncryptRequest;
        }

        public string GetDecryptResponse(string EncryptResponse)
        {
            string DecryptResponse = string.Empty;
            string encResponse = string.Empty;
            string status = string.Empty;
            string enc_response = string.Empty;
            string sResponse = string.Empty;
            CCACrypto ccaCrypto = new CCACrypto();
            string AccessCodeApi = Convert.ToString(ConfigurationManager.AppSettings["PGApiAccessCode"]);
            string WorkingKeyApi = Convert.ToString(ConfigurationManager.AppSettings["PGApiWorkingKey"]);
            try
            {
                #region Create Decrypt Api Response
                NameValueCollection Params = new NameValueCollection();
                encResponse = Convert.ToString(EncryptResponse);
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
                        Params.Add(Key, Value);
                    }
                }

                if (!string.IsNullOrEmpty(enc_response) && status == "0")
                {
                    encResponse = ccaCrypto.Decrypt(enc_response, WorkingKeyApi);
                    DecryptResponse = status + "~" + encResponse;
                }
                else
                {
                    DecryptResponse = status + "~" + encResponse;
                }

                #endregion
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetDecryptResponse", "Decrypt", ex.Message, ex.StackTrace);
                return DecryptResponse;
            }
            return DecryptResponse;
        }

        #endregion
        #region Hotel PaymentGateway(13 Dec 2016)
        public string GetPaymentMode(string OrderId, string ServiceType)
        {
            string PaymentMode = "0";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HTLConnStr"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                //cmd = new SqlCommand("Sp_GetPaymentMode", con);
                cmd = new SqlCommand("Sp_GetPgPaymentDetailsHotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
                cmd.Parameters.AddWithValue("@Action", "PayMode");
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        PaymentMode = Convert.ToString(dt.Rows[0]["PaymentMode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPaymentMode", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return PaymentMode;
        }
        public DataSet GetPgPaymentDetailsHotel(string OrderId, string ServiceType, string Action, string AgentId)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HTLConnStr"].ConnectionString);
            DataSet ds = new DataSet();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlDataAdapter adp = new SqlDataAdapter("Sp_GetPgPaymentDetailsHotel", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@OrderId", OrderId);
                adp.SelectCommand.Parameters.AddWithValue("@ServiceType", ServiceType);
                adp.SelectCommand.Parameters.AddWithValue("@Action", Action);
                adp.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                con.Close();
                cmd.Dispose();
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetPgPaymentDetailsHotel", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return ds;
        }
        #endregion        
        #region PayU Payment Gateway
        public string PaymentGatewayReqPayU(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
        {
            string MerchantID = "";
            string ProviderUrl = "";
            string SuccessUrl = "";
            string CancelUrl = "";
            string FailureUrl = "";
            string postHtml = "";
            int flag = 0;
            string ccaRequest = "";
            string strEncRequest = "";
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);

            string SALT = "";
            string MERCHANT_KEY = "";
            string HashSequence = "";


            double TransCharges = 0;
            string ChargesType = "";
            double TotalPgCharges = 0;
            try
            {
                DataTable pgDT = GetTotalAmountWithPgCharge(PaymentOption, OrignalAmount, AgentId, TrackId);
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
                        MerchantID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                        SALT = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                        MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                        ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                        SuccessUrl = Convert.ToString(dt.Rows[0]["SuccessUrl"]);
                        FailureUrl = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                        CancelUrl = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                        HashSequence = Convert.ToString(dt.Rows[0]["HashSequence"]);

                        //Set PayU Credential 
                        //MERCHANT_KEY="gtKFFx";
                        //SALT="eCwWELxi";
                        //ProviderUrl="https://test.payu.in";
                        //HashSequence = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                        //BillingName = "Devesh";
                        //BillingEmail = "devesh.mailme@gmail.com";

                        //<add key="hashSequence" value="key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10"/>
                        // ccaRequest = "gtKFFx|9583898168031386e84b|100|Hotel|Devesh|sumit2love4@gmail.com|||||||||||eCwWELxi";
                        ccaRequest = "" + MERCHANT_KEY + "|" + TrackId + "|" + Convert.ToDecimal(TotalAmount).ToString("g29") + "|" + ServiceType + "|" + BillingName + "|" + BillingEmail + "|||||||||||" + SALT + "";
                        strEncRequest = Generatehash512(ccaRequest).ToLower();         //generating hash
                        string action1 = ProviderUrl + "/_payment";// setting URL
                        if (!string.IsNullOrEmpty(strEncRequest))
                        {
                            string hash = strEncRequest;
                            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                            data.Add("hash", strEncRequest);
                            data.Add("txnid", TrackId);
                            data.Add("key", MERCHANT_KEY);
                            string AmountForm = Convert.ToDecimal(TotalAmount).ToString("g29");// eliminating trailing zeros                            
                            data.Add("amount", AmountForm);
                            data.Add("firstname", BillingName.Trim());
                            data.Add("email", BillingEmail.Trim());
                            data.Add("phone", BillingTel.Trim());
                            data.Add("productinfo", ServiceType.Trim());
                            data.Add("surl", SuccessUrl.Trim());
                            data.Add("furl", FailureUrl.Trim());
                            data.Add("lastname", "");
                            data.Add("curl", CancelUrl.Trim());
                            data.Add("address1", "");
                            data.Add("address2", "");
                            data.Add("city", "");
                            data.Add("state", "");
                            data.Add("country", "");
                            data.Add("zipcode", "");
                            data.Add("udf1", "");
                            data.Add("udf2", "");
                            data.Add("udf3", "");
                            data.Add("udf4", "");
                            data.Add("udf5", "");
                            //data.Add("pg", "");
                            if (PaymentOption.ToUpper() == "AMZPAY" || PaymentOption.ToUpper() == "PHONEPE" || PaymentOption.ToUpper() == "ITZC" || PaymentOption.ToUpper() == "AMON" || PaymentOption.ToUpper() == "FREC" || PaymentOption.ToUpper() == "OXICASH"
                                || PaymentOption.ToUpper() == "PAYZ" || PaymentOption.ToUpper() == "AMEXZ" || PaymentOption.ToUpper() == "YESW" || PaymentOption.ToUpper() == "OLAM"
                                || PaymentOption.ToUpper() == "PAYCASH" || PaymentOption.ToUpper() == "JIOM" || PaymentOption.ToUpper() == "CPMC")
                            {
                                data.Add("pg", "CASH");
                                data.Add("bancode", PaymentOption.ToUpper());
                                data.Add("enforce_paymethod", PaymentOption.Trim());
                                if (PaymentOption.ToUpper() != "AMEX" && PaymentOption.ToUpper() != "DINR")
                                {                                    
                                    data.Add("drop_category", "CC,DC,NB,UPI,CC|AMEX|DINR");
                                }
                            }
                            else if (PaymentOption.ToUpper() == "AMEX" || PaymentOption.ToUpper() == "DINR")
                            {
                                data.Add("pg", "CC");
                                data.Add("bancode", PaymentOption.ToUpper());
                                data.Add("enforce_paymethod", PaymentOption.Trim());
                            }
                            else if (PaymentOption.ToLower() == "creditcard")
                            {
                                data.Add("pg", "CC");
                                data.Add("bancode", "CC");                                
                                data.Add("drop_category", "DC,NB,UPI,CC|AMEX|DINR,CASH");
                            }
                            else if (PaymentOption.ToLower() == "cashcard")
                            {                               
                                data.Add("drop_category", "CC,DC,NB,UPI");
                            }
                            else if (PaymentOption.ToLower() == "netbanking")
                            {                                
                                data.Add("drop_category", "CC,DC,NB|CITNB,CASH,UPI");
                            }
                            else
                            {
                                data.Add("pg", "");
                                data.Add("enforce_paymethod", PaymentOption.Trim());
                            }                            
                            // data.Add("hash_debug",ccaRequest);
                            postHtml = PreparePOSTForm(action1, data);                                                       
                            flag = InsertPaymentRequestDetails(TrackId, TId, IBTrackId, BillingName, "PayU", BillingEmail, BillingTel, BillingAddress, TotalAmount, OrignalAmount, AgentId, AgencyName, IP, ccaRequest, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, postHtml);
                        }
                        else
                        {
                            //no hash

                        }
                    }
                }
                if (flag > 0)
                {
                    postHtml = "yes~" + postHtml;
                }
                else
                {
                    postHtml = "no~" + postHtml;
                }
            }
            catch (Exception ex)
            {
                postHtml = "no~" + ex.Message;
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "PaymentGatewayReqPayU", "insert", ex.Message, ex.StackTrace);
                return postHtml;
            }

            return postHtml;
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
        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
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

        public string PostWebService(string MerchantTransId, string CommandType)
        {

            #region Cross check of payment status
            string msg = "";
            try
            {
                string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
                DataTable dt = new DataTable();
                dt = GetPgCredential();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string Url = Convert.ToString(dt.Rows[0]["WebServiceUrl"]);//"https://test.payu.in/merchant/postservice.php?form=2";
                        string method = "verify_payment";
                        string salt = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"eCwWELxi";
                        string key = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"gtKFFx";
                        string var1 = MerchantTransId;//Transaction ID of the merchant
                        // gtKFFx|verify_payment|e30478dzpd1Ut4d|eCwWELxi
                        string ToHash = key + "|" + method + "|" + var1 + "|" + salt;
                        string Hashed = Generatehash512(ToHash);
                        string PostString = "key=" + key +
                            "&command=" + method +
                            "&hash=" + Hashed +
                            "&var1=" + var1;// +
                        //"&var2=" + var2 +
                        //"&var3=" + var3;
                        srvPG objPg = new srvPG();
                        string response = objPg.GetPostReqResPayU(PostString, Url, ToHash, Hashed, MerchantTransId, CommandType);
                        Newtonsoft.Json.Linq.JObject account = Newtonsoft.Json.Linq.JObject.Parse(response);
                        String status = (string)account.SelectToken("transaction_details." + var1 + ".status");
                        msg = status;
                    }
                }
            }
            catch (Exception expg)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "UpdatePaymentResponseDetails-GetPostReqResCCAvenue", "insert", expg.Message, expg.StackTrace);
            }
            #endregion
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

        #endregion
        public DataSet GetBusBookingDetails(string OrderId, string AgentID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@TrackId", OrderId);
                adp.SelectCommand.Parameters.AddWithValue("@AgentId", AgentID);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "GetBusDetails");
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("PaymentGateway", "PaymentGateway", "GetBusBookingDetails", "SELECT", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
    }

}
