
using PG;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace RazorGateways
{
    public class RazorPay_Gateway
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adap;
        public string PaymentGatewayReqRazorPay(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
        {

            string MerchantID = "";
            string ProviderUrl = "";
            string SuccessUrl = "";
            string CancelUrl = "";
            string MERC_UNQ_PASD = "";
            string CHANNEL_ID = "";
            string INDUSTRY_TYPE_ID = "";
            string WEBSITE = "";
            string FailureUrl = "";
            string outputHTML = "";
            int flag = 0;
            string ccaRequest = "";
            string strEncRequest = "";
            string SALT = "";
            string MERCHANT_KEY = "";
            string HashSequence = "";

            double TransCharges = 0;
            string ChargesType = "";
            double TotalPgCharges = 0;
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["RazorProvider"]);

            try
            {
               
                DataTable dt = new DataTable();
                dt = GetPgCredential();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //B2B (GUNEET SETHI) 9911333666
                        //ID- HR@FLYWIDUS.COM    merchant id  : GZ2wA09jaMb9LF  Deactivate credit card
                        //B2C (VIKAS JAIN) 9911222666
                        //ID- ADMIN@SEVENSEAZ.COM   Merchant Id: GZopnEK1TKGxfY  Deactivate debit card
                        if (PaymentOption == "razorpaydebit")
                        {
                            //MERCHANT_KEY= MERC_UNQ_REF, MERCHANT_PSWD= CHANNEL_ID, 
                            MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERC_UNQ_REF"]);
                            MERC_UNQ_PASD = Convert.ToString(dt.Rows[0]["CHANNEL_ID"]);
                        }
                        else
                        {
                            MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                            MERC_UNQ_PASD = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                        }
                        CHANNEL_ID = Convert.ToString(dt.Rows[0]["CHANNEL_ID"]);
                        INDUSTRY_TYPE_ID = Convert.ToString(dt.Rows[0]["INDUSTRY_TYPE_ID"]);
                        WEBSITE = Convert.ToString(dt.Rows[0]["WEBSITE"]);
                        ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                        SuccessUrl = Convert.ToString(dt.Rows[0]["SuccessUrl"]); ;

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
                        //string OAMT = Convert.ToString(TotalAmount.ToString().Split('.')[0]) + "00";                        
                        //string Parameters = "amount=" + TotalAmount * 100 + "&buyerEmail=" + buyerEmail + "&currency=" + currency + "&merchantIdentifier=" + merchantIdentifier + "&merchantIpAddress=" + IP + "&mode=" + mode + "&orderId=" + orderId + "&purpose=" + purpose + "&returnUrl=" + returnUrl + "&txnType=" + txnType + "&zpPayOption=" + zpPayOption + "&";

                        string method = string.Empty;

                        Dictionary<string, object> input = new Dictionary<string, object>();
                        //input.Add("amount", Convert.ToDecimal(OAMT)); // this amount should be same as transaction amount
                        input.Add("amount", TotalAmount * 100); // this amount should be same as transaction amount
                        input.Add("currency", "INR");
                        input.Add("receipt", TrackId);
                        #region Payment Mode
                        if (!string.IsNullOrEmpty(PaymentOption))
                        {
                            if (PaymentOption == "razorpaycard")
                            {
                                method = "card";
                            }
                            else if (PaymentOption == "razorpaydebit")
                            {
                                method = "card";
                            }
                            else if (PaymentOption == "razorpaycredit")
                            {
                                method = "card";
                            }
                            else if (PaymentOption == "razorpaynetbanking")
                            {
                                method = "netbanking";
                            }
                            else if (PaymentOption == "razorpaywallet")
                            {
                                method = "wallet";
                            }
                            else if (PaymentOption == "razorpayemi")
                            {
                                method = "emi";
                            }
                            else if (PaymentOption == "razorpayupi")
                            {
                                method = "upi";
                            }
                            //input.Add("method", method);                            
                        }
                        #endregion
                        input.Add("payment_capture", 1);
                        string key = MERCHANT_KEY;//"<Enter your Api Key here>";
                        string secret = MERC_UNQ_PASD;// "<Enter your Api Secret here>";

                        RazorpayClient client = new RazorpayClient(key, secret);
                        Order order = client.Order.Create(input);
                        //Razorpay.Api.Order order = client.Order.Create(input);
                        string orderId = order["id"].ToString();
                        string CustName = "";
                        if (!string.IsNullOrEmpty(BillingName))
                        {
                            CustName = BillingName;
                        }
                        else { CustName = AgentId; }
                        outputHTML = "<html>";
                        outputHTML += "<head>";
                        outputHTML += "<title>Merchant Check Out Page</title>";
                        outputHTML += "</head>";
                        outputHTML += "<body>";
                        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
                        outputHTML += "<form method='post' action='" + ProviderUrl + "' name ='f1'>";//" + orderId + " name='f1'
                        outputHTML += "<input type='hidden' name='key_id' value='"+ key + "'/>";
                        outputHTML += "<input type='hidden' name='order_id' value='" + orderId + "'/>";
                        outputHTML += "<input type='hidden' name='name' value='" + CustName + "'/>";
                        outputHTML += "<input type='hidden' name='description' value='"+ AgencyName + "'/>";
                        // outputHTML += "<input type='hidden' name='image' value='https://cdn.razorpay.com/logos/BUVwvgaqVByGp2_large.png'/>";
                        if (!string.IsNullOrEmpty(method))
                        {
                            outputHTML += "<input type='hidden' name='method' value='" + method + "'/>";
                        }                            
                        outputHTML += "<input type='hidden' name='prefill[name]' value='"+AgentId+"'/>";
                        outputHTML += "<input type='hidden' name='prefill[contact]' value='"+ BillingTel + "'/>";
                        outputHTML += "<input type='hidden' name='prefill[email]' value='"+ BillingEmail + "'/>";
                        outputHTML += "<input type='hidden' name='notes[shipping address]' value='"+ BillingAddress + "'/>";
                        outputHTML += "<input type='hidden' name='callback_url' value='"+ SuccessUrl + "'/>";
                        outputHTML += "<input type='hidden' name='cancel_url' value='"+SuccessUrl+"'/>";//CancelUrl
                        outputHTML += "<script type='text/javascript'>";
                        outputHTML += "document.f1.submit();";
                        outputHTML += "</script>";
                        outputHTML += "</form>";
                        outputHTML += "</body>";
                        outputHTML += "</html>";

                        //Response.Write(outputHTML);


                        // data.Add("enforce_paymethod", PaymentOption.Trim());  //working fine for all payment mode type -FLYWIDUS
                        // data.Add("hash_debug",ccaRequest);
                        //postHtml = PreparePOSTForm(action1, data);
                        //Page.Controls.Add(new LiteralControl(strForm));                            
                        flag = InsertPaymentRequestDetails(TrackId, orderId, IBTrackId, BillingName, "Razor", BillingEmail, BillingTel, BillingAddress, TotalAmount, OrignalAmount, AgentId, AgencyName, IP, ccaRequest, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, outputHTML, PaymentOption);
                    }
                    else
                    {
                    }
                }
                if (flag > 0)
                {
                    outputHTML = "yes~" + outputHTML;
                }
                else
                {
                    outputHTML = "no~" + outputHTML;
                }
            }
            catch (Exception ex)
            {
                outputHTML = "no~" + ex.Message;
                int insert = InsertExceptionLog("Razor_Pay", "Razor_Pay", "PaymentGatewayReqRazorPay", "insert", ex.Message, ex.StackTrace);
                return outputHTML;
            }

            return outputHTML;
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
                int insert = InsertExceptionLog("Razor_Pay", "Razor_Pay", "GetTotalAmountWithPgCharge", "SELECT", ex.Message, ex.StackTrace);
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
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["RazorProvider"]);

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
                int insert = InsertExceptionLog("Razor_Pay", "Razor_Pay", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public int InsertPaymentRequestDetails(string TrackId, string TId, string IBTrackId, string Name, string PaymentGateway, string Email, string Mobile, string Address, double TotalAmount, double OriginalAmount, string AgentId, string AgencyName, string Ip, string PgRequest, string ServiceType, string EncRequest, string Trip, double TotalPgCharges, double TransCharges, string ChargesType, string PostHtml,string PaymentOption)
        {
            int temp = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
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
                // add new param 23 March 2021
                cmd.Parameters.AddWithValue("@PaymentMode", PaymentOption);
                temp = cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                int insert = InsertExceptionLog("Razor_Pay", "Razor_Pay", "InsertPaymentRequestDetails", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();

            }
            return temp;
        }
    }

}
