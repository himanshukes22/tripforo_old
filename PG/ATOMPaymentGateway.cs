//using DotNetIntegrationKit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PG
{
    public class ATOMPaymentGateway
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adap;
        PG.PaymentGateway objPg = new PG.PaymentGateway();
        public string PaymentGatewayReq_Atom(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
        {
            #region Use IN ATOM 18-05-2021: DEVEH
           
            string signature = "";
            string strsignature = "";
            string plaintextReq = "";
            //string Encryptval = "";
            //string strURL = "";            
            //string TransactionAmount = Convert.ToString(TotalAmount);
            //string TransactionID = TrackId;
            #endregion
            string Reference = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            TId = "AT" + Reference;//Reference.Substring(4, 16);            
            string MerchantID = "";
            string ProviderUrl = "";
            string SuccessUrl = "";
            string CancelUrl = "";
            string FailureUrl = "";
            string postHtml = "";
            int flag = 0;
            //string ccaRequest = "";
            string strEncRequest = "";
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderATOM"]);
            string SALT = "";
            string MERCHANT_KEY = "";
            string HashSequence = "";
            double TransCharges = 0;
            string ChargesType = "";
            double TotalPgCharges = 0;
            try
            {
                #region Get and Calculate Payment Charge
                DataTable pgDT = objPg.GetTotalAmountWithPgCharge(PaymentOption, OrignalAmount, AgentId, TrackId);
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
                #endregion
                #region Get PG Credential and Making Request
                string AtomMMID = "MMID_1ST";
                if (PaymentOption.ToUpper() == "ATOMCC_2ND" || PaymentOption.ToUpper().Contains("_2ND") == true)
                {
                    AtomMMID = "MMID_2ND";
                }
                DataTable dt = new DataTable();
                dt = GetPgCredential(Provider, AtomMMID);
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
                        //ProductID, HashReqKey, AESReqKey, AESReqSaltIVKey, HashResKey, AESResKey, AESResSaltIVKey
                        string ProductID = Convert.ToString(dt.Rows[0]["ProductID"]);// "NSE";
                        string ClientCode = AgentId; //Convert.ToString(dt.Rows[0]["MerchantID"]);//"9132";
                        string MerchantLogin = Convert.ToString(dt.Rows[0]["MerchantID"]);// "9132";
                        string MerchantPass = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"Test@123";
                        string reqHashKey = Convert.ToString(dt.Rows[0]["HashReqKey"]);// "KEY123657234";
                        string passphrase = Convert.ToString(dt.Rows[0]["AESReqKey"]);//"A4476C2062FFA58980DC8F79EB6A799E";
                        string salt = Convert.ToString(dt.Rows[0]["AESReqSaltIVKey"]);//"A4476C2062FFA58980DC8F79EB6A799E";
                        string ru = Convert.ToString(dt.Rows[0]["SuccessUrl"]); //"http://localhost:53943/ATOMSuccess.aspx";
                                               
                        string TransactionServiceCharge = "0";
                        string BankID = "2001";
                        string CustomerAccountNo = "20069686727";
                        string MerchantDiscretionaryData = "";

                        string TransactionType = "NBFundtransfer";
                        string TransactionCurrency = "INR";                        
                        string TransactionDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//System.DateTime.Now.ToString();// DateTime.Now.ToString("dd-MM-yyyy"); //"09-04-2021";DD/MM/YYYY HH:MM:SS
                        

                        // postHtml = PreparePOSTForm(action1, data);

                        //Payment Url-https://paynetzuat.atomtech.in/paynetz/epi/fts 
                        //Dashboard Url-	https://paynetzuat.atomtech.in/PaynetzWebMerchant 
                        //User ID	9132
                        //DashBoard Login Password	Test@123
                        //Transaction Password	Test@123
                        //Product ID	NSE
                        //Hash Request Key	KEY123657234
                        //AES Request Key	A4476C2062FFA58980DC8F79EB6A799E
                        //AES Request Salt/IV Key	A4476C2062FFA58980DC8F79EB6A799E

                        //Hash Response Key	KEYRESP123657234
                        //AES Response Key	75AEF0FA1B94B3C10D4F5B268F757F11
                        //AES Response Salt/IV Key	75AEF0FA1B94B3C10D4F5B268F757F11

                        #region ATOM PAYMENT GATEWAY MAKING REQUEST Date  17-05-2021
                        string strClientCode, strClientCodeEncoded = "";
                        byte[] b;

                        //string ClientCode = "9132";
                        //string MerchantLogin = "9132";//"197";
                        //string MerchantPass = "Test@123";//"Test@123";  
                        //string reqHashKey = "KEY123657234"; //"KEY123657234";
                        //string passphrase = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";
                        //string salt = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";

                        ClientCode = AgentId;
                        b = Encoding.UTF8.GetBytes(ClientCode);
                        strClientCode = Convert.ToBase64String(b);
                        strClientCodeEncoded = HttpUtility.UrlEncode(strClientCode);                        
                        strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TrackId + Convert.ToString(TotalAmount) + TransactionCurrency;
                        byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
                        byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                        // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
                        signature = byteToHexString(bt).ToLower();
                        byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                        int iterations = 65536;
                        int keysize = 256;
                        string hashAlgorithm = "SHA1";
                        //mdd:If no values passed, then all banks /credit card options enabled for merchant will be displayed to the customer on Atom payment option page. See Table below for all Possible values
                        //NB In case the merchant wishes to show only Net Banking as payment option to customer.
                        //CC In case the merchant wishes to show only Credit Card as payment option to customer.
                        //DC In case the merchant wishes to show only Debit Card as payment option to customer.
                        //AMEX In case the merchant wishes to show only American Express as payment option to customer.
                        //SI In case the merchant wishes to show only STANDING INSTRUCTION as payment option to customer.
                        //EMI In case the merchant wishes to show only EMI as payment option to customer.
                        //CH In case the merchant wishes to show only Challan as payment option to customer.
                        //MV In case the merchant wishes to show only BharatQR as payment option to customer.
                        //UP In case the merchant wishes to show only Unified Payment Interface (UPI) as payment option to customer.
                        //MV In case the merchant wishes to show only BharatQR as payment option to customer.
                        //MW In case the merchant wishes to show only wallet as payment option to customer.
                        //Kindly use mdd=MW for the only wallet payment option to be displayed on atom payment page.  as Gaurav Atom mail
                        string mdd = "";
                        if (PaymentOption.ToUpper()== "ATOMNB" || PaymentOption.ToUpper() == "ATOMNB_2ND")
                        { mdd = "NB"; }
                       else if (PaymentOption.ToUpper() == "ATOMCC" || PaymentOption.ToUpper() == "ATOMCC_2ND")
                        { mdd = "CC"; }
                        else if (PaymentOption.ToUpper() == "ATOMDC" || PaymentOption.ToUpper() == "ATOMDC_2ND")
                        { mdd = "DC"; }
                        else if (PaymentOption.ToUpper() == "ATOMAMEX" || PaymentOption.ToUpper() == "ATOMAMEX_2ND")
                        { mdd = "AMEX"; }
                        else if (PaymentOption.ToUpper() == "ATOMSI" || PaymentOption.ToUpper() == "ATOMSI_2ND")
                        { mdd = "SI"; }
                        else if (PaymentOption.ToUpper() == "ATOMEMI" || PaymentOption.ToUpper() == "ATOMEMI_2ND")
                        { mdd = "EMI"; }
                        else if (PaymentOption.ToUpper() == "ATOMCH" || PaymentOption.ToUpper() == "ATOMCH_2ND")
                        { mdd = "CH"; }
                        else if (PaymentOption.ToUpper() == "ATOMMV" || PaymentOption.ToUpper() == "ATOMMV_2ND")
                        { mdd = "MV"; }
                        else if (PaymentOption.ToUpper() == "ATOMUP" || PaymentOption.ToUpper() == "ATOMUP_2ND")
                        { mdd = "UP"; }
                        else if (PaymentOption.ToUpper() == "ATOMMW" || PaymentOption.ToUpper() == "ATOMMW_2ND")
                        { mdd = "MW"; }                        
                        // BillingName.Remove(45)
                        // working fine below code
                        //string plaintext = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&prodid=NSE&amt=" + TransactionAmount + "&txncurr=INR&txnscamt=0&clientcode=" + strClientCodeEncoded + "&txnid=" + TransactionID + "&date=" + TransactionDateTime + "&custacc=20069766737&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=" + ru + "&udf3=9999999999&signature=" + signature;
                        //(string TId,AgentId, string AgencyName,BillingName,BillingAddress,BillingCity,BillingState, string BillingZip, BillingTel, BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
                        if (PaymentOption.ToUpper()=="ATOM")
                        {
                            plaintextReq = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&prodid=" + ProductID + "&amt=" + Convert.ToString(TotalAmount) + "&txncurr=INR&txnscamt=0&clientcode=" + strClientCodeEncoded + "&txnid=" + TrackId + "&date=" + TransactionDateTime + "&custacc=" + TId + "&udf1=" + BillingName + "&udf9=" + AgentId + "&udf10=" + PaymentOption + "&udf11=" + ServiceType + "&ru=" + ru + "&udf3=" + BillingTel + "&signature=" + signature;
                        }
                        else
                        {
                            plaintextReq = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&prodid=" + ProductID + "&amt=" + Convert.ToString(TotalAmount) + "&txncurr=INR&txnscamt=0&clientcode=" + strClientCodeEncoded + "&txnid=" + TrackId + "&date=" + TransactionDateTime + "&custacc=" + TId + "&udf1=" + BillingName + "&udf9=" + AgentId + "&udf10=" + PaymentOption + "&mdd=" + mdd + "&ru=" + ru + "&udf3=" + BillingTel + "&signature=" + signature;
                        }
                        
                        strEncRequest = Encrypt(plaintextReq, passphrase, salt, iv, iterations);
                        //postHtml = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + MerchantLogin + "&encdata=" + strEncRequest;
                        //string FormActionUrl = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + MerchantLogin + "&encdata=" + strEncRequest;                        
                        string FormActionUrl = ProviderUrl.Trim()+"login=" + MerchantLogin + "&encdata=" + strEncRequest;
                        StringBuilder strForm = new StringBuilder();
                        strForm.Append("<form name='s1_2' id='s1_2' action='" + FormActionUrl + "' method='post'> ");
                        strForm.Append("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
                        strForm.Append("</script>");
                        strForm.Append("<script language='javascript' >");
                        strForm.Append("</script>");
                        strForm.Append("</form> ");
                        postHtml = Convert.ToString(strForm);


                        //strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + MerchantLogin + "&encdata=" + Encryptval;
                        //string req = "login=9132&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=100.00&txncurr=INR&txnscamt=0&clientcode=TkFWSU4=&txnid=M123&date=03/04/2020&custacc=100000036600&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=https://pgtest.atomtech.in/paynetzclient/ResponseParam.jsp&udf3=9999999999&signature=60be76a00eae711826095afe80874822c1e261cb7150cfe4828ba83721c416b0dcb92af439a63f27d875a4d1d5e53f4a3e028acfe52504952776d507386253f3";
                        //string finalreq = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=9132&encdata=EE09C765D78EB9AA8406378CAA3E230917A126B5D52A15A1D953D5E15411C9561A8860BE1AC5716B9D21275D6B01E506439927C78DC6CA3C345C0CCA2344109DB6317F2349294480A7411B0EEE0E536EAD507F1AE1B4BADDC7F84F2E3364A5F768E7D81CD6F1AE32A4C75E2F5887DD9D00F9CD232EFE8B99263A70A107E75EA778E6FDE63771099AD7C3461EA68B13F346D410FD3F6489382A9857EC495CE5B4D9308F33E18E6F36DA86E66F4B8E56AA74CE98EA7FDAA62E49CDB3FCD6E2D0B112650B36E6E4971C7A48F93B51E239857E3CCD23DAD904BBF321182083E02CB1FC635A459637FB9F033DC7C1F93D1A276B3F5FAB7EF334D1A6EA5B8DA93915FC4BF9AFB118F3E9A64BFD1029BC936C267CCBF01B4A85E907E7D292332F4819A42B612CE1D01A9DDFCCE19D5DDAF27FA8EAAA46DB9A2DD4EB11369D5B64F2A9BC75208B0AC66F55260DC797A99469C5CE3980AE917F047D38D4A7D7C87594BDC0C79B4CEBDA0B8BBD79156862E6562C49981C1858FABF62D0689B704715B709ABD4FEB48FBD5A506042E7FA74305A2D18E7CFEF28E75AA4A04A3ECFB482B307E8363CBAE0C88ACC2BA7F51BA7AB4F4465";

                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; // comparable to modern browsers

                        // HttpContext.Current.Response.Redirect(strURL, false);

                        #endregion

                    }
                    else
                    {
                        postHtml = "no^" + "PaymentGateway Credential not found";
                        int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "PaymentGatewayReq_Atom", TrackId, "insert", postHtml, "No row found in GetPgCredential data table");
                        //return postHtml;
                    }
                }
                else
                {
                    postHtml = "no^" + "PaymentGateway Credential not found";
                    int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "PaymentGatewayReq_Atom", TrackId, "insert", postHtml, "GetPgCredential data table is null or empty");
                }
                flag = objPg.InsertPaymentRequestDetails(TrackId, TId, IBTrackId, BillingName, "ATOM", BillingEmail, BillingTel, BillingAddress, TotalAmount, OrignalAmount, AgentId, AgencyName, IP, plaintextReq, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, postHtml);
                #endregion -End PG Credential and Making Request
                if (flag > 0)
                {
                    postHtml = "yes^" + postHtml;
                }
                else
                {
                    postHtml = "no^" + postHtml;
                }
            }
            catch (Exception ex)
            {
                postHtml = "no^" + ex.Message;
                int insert = objPg.InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "PaymentGatewayReq_Atom", "insert", ex.Message, ex.StackTrace);
                return postHtml;
            }

            return postHtml;
        }
        public DataTable GetPgCredential_OLD(string provider)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            // string provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
            try
            {
                if (con.State != ConnectionState.Open)
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
                //int insert = InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "PaymentGatewayReq_Atom", "insert", ex.Message, ex.StackTrace);
                int insert = objPg.InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return dt;
        }
        public DataTable GetPgCredential(string provider, string PaymentMode)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                #region Get PG Credential Details  with Sp_Get_PgCredentials
                //if (con.State != ConnectionState.Open)
                //    con.Open();
                //cmd = new SqlCommand("Sp_Get_PgCredentials", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Pvd", provider);
                //SqlDataReader reader = cmd.ExecuteReader();
                //dt.Load(reader);
                #endregion
                #region Get PG Credential Details  using SpInsertPaymentDetails
                if (string.IsNullOrEmpty(PaymentMode))
                {
                    PaymentMode = "MMID_1ST";
                }
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SpInsertPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Provider", provider);
                cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode);
                cmd.Parameters.AddWithValue("@Action", "GETCRD");
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                #endregion

            }
            catch (Exception ex)
            {
                int insert = objPg.InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return dt;
        }
        public string UpdatePaymentResponse_Atom(string SeesionAgentId, string PGEncryptRes, string PaymentOption, string Response)
        {
            string AgentId = "";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);            
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderATOM"]);
            int temp = 0;
            string PGStatus = string.Empty;
            string PaymentId = string.Empty;
            string Status = string.Empty;
            string BankRefNo = string.Empty;
            string ErrorText = string.Empty;
            string ResponseCode = string.Empty;
            string PaymentMode = string.Empty;
            string CardType = string.Empty;
            string IssuingBank = string.Empty;
            string CardName = string.Empty;
            string CardNumber = string.Empty;
            string UnmappedStatus = string.Empty;
            string PgAmount = "0.0";
            string DiscountValue = "0.0";
            string MerAamount = "0.0";
            string msg = "no~";
            string ApiRequest = string.Empty;
            string ApiResponse = string.Empty;
            string ApiStatus = string.Empty;
            string ApiEncryptRequest = string.Empty;
            string OrderId = string.Empty;
            //RequestURL objRequestURL = new RequestURL();
            #region Date 12-04-2021
            //string MerchantCode = string.Empty;
            //string IsIv = string.Empty;
            //string IsKey = string.Empty;
            //string PropertyFile = Convert.ToString(ConfigurationManager.AppSettings["ErrorFile"]);
            //string strFilePath = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);

            string strPG_TxnStatus = string.Empty,
            // strPG_ClintTxnRefNo = string.Empty,
            //strPG_TPSLTxnBankCode = string.Empty,
            // strPG_TPSLTxnID = string.Empty,
            //strPG_TxnAmount = string.Empty,
            strPG_TxnDateTime = string.Empty,
            strPG_TxnDate = string.Empty,
            strPG_TxnTime = string.Empty;
            //string strPGResponse;
            string[] strSplitDecryptedResponse;
            string[] strArrPG_TxnDateTime;
            string[] strPGChecksum, strPGTxnString;
            string PgResponse = PGEncryptRes + "~~";
            string strDecryptedVal = null;
            #endregion
            try
            {
                #region ATOM Response Parse
                if (!String.IsNullOrEmpty(PGEncryptRes) || !String.IsNullOrEmpty(Response))//(PgResponse))
                {
                    string AtomMMID = "MMID_1ST";
                    if (PaymentOption.ToUpper() == "MMID_2ND" || PaymentOption.ToUpper() == "ATOMCC_2ND" || PaymentOption.ToUpper().Contains("_2ND") == true)
                    {
                        AtomMMID = "MMID_2ND";
                    }
                    DataTable dt = new DataTable();
                    dt = GetPgCredential(Provider, AtomMMID);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {                             
                            string MerchantID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                            string SALT = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                            string MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                            string ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                            string SuccessUrl = Convert.ToString(dt.Rows[0]["SuccessUrl"]);
                            string FailureUrl = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                            string CancelUrl = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                            string HashSequence = Convert.ToString(dt.Rows[0]["HashSequence"]);
                            //ProductID, HashReqKey, AESReqKey, AESReqSaltIVKey, HashResKey, AESResKey, AESResSaltIVKey
                            string ProductID = Convert.ToString(dt.Rows[0]["ProductID"]);// "NSE";
                            string ClientCode = Convert.ToString(dt.Rows[0]["MerchantID"]);//"9132";
                            string MerchantLogin = Convert.ToString(dt.Rows[0]["MerchantID"]);// "9132";
                            string MerchantPass = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"Test@123";
                            string reqHashKey = Convert.ToString(dt.Rows[0]["HashResKey"]);// "KEY123657234";
                            string passphrase = Convert.ToString(dt.Rows[0]["AESResKey"]);//"A4476C2062FFA58980DC8F79EB6A799E";
                            string salt = Convert.ToString(dt.Rows[0]["AESResSaltIVKey"]);//"A4476C2062FFA58980DC8F79EB6A799E";
                            string ru = Convert.ToString(dt.Rows[0]["SuccessUrl"]); //"http://localhost:53943/ATOMSuccess.aspx";
                            #region ATOM RESPONSE PARSE

                            //string passphrase = "75AEF0FA1B94B3C10D4F5B268F757F11";// "8E41C78439831010F81F61C344B7BFC7";
                            //string salt = "75AEF0FA1B94B3C10D4F5B268F757F11";// "8E41C78439831010F81F61C344B7BFC7";
                            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                            int iterations = 65536;
                            int keysize = 256;
                            // string plaintext = "ABC123";
                            string hashAlgorithm = "SHA1";
                            //string Encryptval = Encrypt(plaintext, passphrase, salt, iv, iterations);
                            if (!string.IsNullOrEmpty(PGEncryptRes))// || !string.IsNullOrEmpty(Convert.ToString(Request.Form)))
                            {
                                strDecryptedVal = decrypt(PGEncryptRes, passphrase, salt, iv, iterations);
                            }
                            else
                            {
                                strDecryptedVal = Response;
                            }                                
                            strSplitDecryptedResponse = strDecryptedVal.Split('&');
                            string[] parameters = strSplitDecryptedResponse;

                            //date=Tue May 18 12:28:18 IST 2021&CardNumber=401288XXXXXX1881&surcharge=0.00&clientcode=9132&udf15=null&udf14=null
                            //&signature=fe5afd2b1e9d526e239fb5017cbb93d9a49f9e8479bf97ea264ee31b5e4c18af066abc6b7b32e763dfd4bf178b24560784379c4a31438136f2619a6a2ccb9fa1
                            //&udf13=null&udf12=null&udf11=9953725487&amt=1.00&udf10=ATOM&merchant_id =9132&
                            //mer_txn =WTC0518122534391437&f_code=Ok&bank_txn=0011000000096996418&udf9=WALLET-TOP-UP&ipg_txn_id=11000000096996&bank_name=Hdfc Bank&prod=NSE
                            //&mmp_txn=11000000096996&udf5=null&udf6=null&udf3=Manish&udf4=null&udf1=PMPTRAVEL&udf2=null&discriminator=CC
                            //&auth_code=00000&desc=SUCCESS

                            #region Get Value from Response
                            string mmp_txn=string.Empty, mer_txn = string.Empty, f_code = string.Empty, prod = string.Empty, discriminator = string.Empty, amt = string.Empty, bank_txn = string.Empty;
                            string signatureResponse = "";
                            //GetPGRespnseData(string[] parameters)
                            string[] strGetMerchantParamForCompare;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                                if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "f_code")
                                {
                                    f_code = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);// f_code = Ok                                    
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "signature")
                                {
                                    signatureResponse = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "clientcode")
                                {
                                    AgentId = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "prod")
                                {
                                    prod = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "amt")
                                {
                                    amt = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    PgAmount= Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "mer_txn")
                                {
                                    mer_txn = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    OrderId = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "bank_txn")
                                {
                                    bank_txn = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    BankRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);                                    
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "ipg_txn_id")
                                {                                    
                                    PaymentId = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "mmp_txn")
                                {
                                    mmp_txn = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    CardName = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "cardnumber")
                                {                                    
                                    CardNumber = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }                               
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "discriminator")
                                {
                                    discriminator = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    CardType = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }                               
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "bank_name")
                                {
                                    IssuingBank = Convert.ToString(strGetMerchantParamForCompare[1]);
                                    //PgAmount,IssuingBank,CardName
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "auth_code")
                                {                                   
                                    ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }
                                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "desc")
                                {
                                    ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                                }                               
                            }
                            #endregion

                            #region Signature Hash Match

                            //Signature will contain the hash values of the mmp_txn, mer_txn,f_code, prod, discriminator, amt, bank_txn parameters generated using HMACSHA512 algorithm.
                            //string strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TrackId + Convert.ToString(TotalAmount) + TransactionCurrency;
                            string strsignature = mmp_txn + mer_txn + f_code + prod + discriminator + amt + bank_txn;
                            byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
                            byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                            // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
                            string signature = byteToHexString(bt).ToLower();
                            bool signatureHashMatch = false;
                            if (signature.ToLower()== signatureResponse.ToLower())
                            {
                                signatureHashMatch = true;
                            }
                            #endregion

                            //PaymentId,Status,ResponseCode,BankRefNo,PgAmount,MerAamount,PaymentMode,CardName,DiscountValue,CardType,UnmappedStatus,ApiStatus                        
                            if (strPG_TxnStatus.ToUpper() == "OK" && ErrorText.ToUpper().Contains("SUCCESS") == true)
                            {
                                Status = "success";                               
                                ApiStatus = "signature not match";
                                UnmappedStatus = "signature not match";
                                if (signatureHashMatch==true)
                                {
                                    Status = "success";
                                    ApiStatus = "success";
                                    UnmappedStatus = "captured";
                                    // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                                }
                                
                                // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                            }
                            else
                            {
                                Status = "failure";
                                ApiStatus = "TxnStatus not OK";
                                UnmappedStatus = "TxnStatus not OK";
                            }
                            if (string.IsNullOrEmpty(PGEncryptRes))// || !string.IsNullOrEmpty(Convert.ToString(Request.Form)))
                            {
                                Status = "failure";//PGEncryptRes
                                ApiStatus = "PG response is not encrypted";
                            }
                            #endregion
                        }
                    }
                }
                #endregion
                #region Get UserId/AgentId from database 

                //DataSet ds = GetAgentIdByOrderid(OrderId);
                //if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                //{
                //    AgentId = Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]);
                //}
                msg = msg + OrderId + "~" + AgentId;
                //EncryptedVal Response of ATOM POST
                ApiResponse = Response;//PgResponse;
                //Check Db AgentId and Session AgentId              
                #endregion
                #region Cross Verification  by API

                #endregion


                #region Update PG Response
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SpInsertPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrackId", OrderId);
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@ResponseMessage", strDecryptedVal);//PgResponse
                cmd.Parameters.AddWithValue("@ResponseCode", ResponseCode);
                cmd.Parameters.AddWithValue("@ErrorText", ErrorText);
                cmd.Parameters.AddWithValue("@PgResponse", strDecryptedVal);// PgResponse);
                cmd.Parameters.AddWithValue("@BankRefNo", BankRefNo);
                cmd.Parameters.AddWithValue("@PgAmount", Convert.ToDouble(PgAmount));//PgAmount
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
                if (temp > 0)
                {
                    msg = "yes~" + OrderId + "~" + AgentId;
                    //msg = "yes^" + OrderId + "^" + AgentId;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //if (temp > 0)
                //{

                //         msg = "yes~" + OrderId + "~" + AgentId;
                //}
                //else
                //{
                msg = "no~" + OrderId + "~" + AgentId;
                //msg = "no^" + OrderId + "^" + AgentId;
                //}
                int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "UpdatePaymentResponse_ATOM", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return msg;
        }
        public DataSet GetAgentIdByOrderid(string RefernceNo)
        {
            DataSet ds = new DataSet();
            try
            {
                #region Get details and Login

                if (!string.IsNullOrEmpty(RefernceNo))
                {
                    ds = GetUserIdAndPassword(RefernceNo);
                    //if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                    //{
                    //    Login(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]), Convert.ToString(ds.Tables[0].Rows[0]["PWD"]));
                    //}
                    //else
                    //{
                    //    int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "PGResponsParse", "UserIdPassword dataset is null or empty", Convert.ToString(Session["UID"]), RefernceNo, pgResponse);
                    //}
                }
                else
                {
                    int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "GetAgentIdByOrderid", RefernceNo, RefernceNo, RefernceNo, RefernceNo);
                }
                #endregion
            }
            catch (Exception ex)
            {
                int insert1 = objPg.InsertExceptionLog("ATOMPaymentGateway", "GetAgentIdByOrderid", RefernceNo, RefernceNo, ex.Message, ex.StackTrace);
            }
            return ds;


        }

        public DataSet GetUserIdAndPassword(string ReferenceNo)
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
                adap.SelectCommand.Parameters.AddWithValue("@Action", "GetUsrId");
                //cmd.Parameters.AddWithValue("@TrackId", TrackId);
                // cmd.Parameters.AddWithValue("@Action", "insert");
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
                //clsErrorLog.LogInfo(ex);
                // EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-DataSet user_auth");
            }
            finally
            {
                con.Close();
                adap.Dispose();
            }
            return ds;
        }
        
        public DataTable Get_T_TypeRequestDetails(string OrderId, string AgentId)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SP_Insert_Tbl_PaynimoReq", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@MerchantTxnRefNumber", OrderId);
                cmd.Parameters.AddWithValue("@Provider", Provider);
                cmd.Parameters.AddWithValue("@Action", "GET");
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                //adap.Fill(dt);
            }
            catch (Exception ex)
            {
                //int insert = InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "PaymentGatewayReq_Atom", "insert", ex.Message, ex.StackTrace);
                int insert = objPg.InsertExceptionLog("PGDLL", "ATOMPaymentGateway", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return dt;
        }

        public int InsertPaymentRequestDetails(string TrackId, string TId, string AgentId, string RequestType, string MerchantCode, string MerchantTxnRefNo, string ITC, string Amount, string Currencycode
                                          , string uniqueCustomerID, string returnURL, string StoSreturnURL
                                          , string TPSLTXNID, string Shoppingcartdetails, string TxnDate
                                          , string Email, string mobileNo, string Bankcode, string customerName
                                          , string CardID, string AccountNo, string IsKey, string IsIv, int Counter)
        {
            int temp = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            try
            {
                string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SP_Insert_Tbl_PaynimoReq", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Counter", Counter);
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@RequestType", RequestType);
                cmd.Parameters.AddWithValue("@MerchantCode", MerchantCode);
                cmd.Parameters.AddWithValue("@MerchantTxnRefNumber", TrackId);
                cmd.Parameters.AddWithValue("@ITC", ITC);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@CurrencyCode", Currencycode);
                cmd.Parameters.AddWithValue("@UniqueCustomerId", uniqueCustomerID);
                cmd.Parameters.AddWithValue("@ReturnURL", returnURL);
                cmd.Parameters.AddWithValue("@S2SReturnURL", StoSreturnURL);
                cmd.Parameters.AddWithValue("@TPSLTxnID", TPSLTXNID);
                cmd.Parameters.AddWithValue("@ShoppingCartDetails", Shoppingcartdetails);
                cmd.Parameters.AddWithValue("@TxnDate", TxnDate);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@MobileNumber", mobileNo);
                cmd.Parameters.AddWithValue("@BankCode", Bankcode);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@CardID", CardID);
                cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
                cmd.Parameters.AddWithValue("@IsKey", IsKey);
                cmd.Parameters.AddWithValue("@IsIv", IsIv);
                cmd.Parameters.AddWithValue("@Provider", Provider);
                cmd.Parameters.AddWithValue("@Action", "insert");
                temp = cmd.ExecuteNonQuery();
                //con.Close();           
            }
            catch (Exception ex)
            {
                //int insert = InsertExceptionLog("PGDLL", "PGDLL", "InsertPaymentRequestDetails", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }

        public void TransferFund()
        {
            //string MerchantLogin, string MerchantPass, string MerchantDiscretionaryData, string ProductID, 
            string ClientCode = "9132";
            //string CustomerAccountNo, string TransactionType, string TransactionAmount, string TransactionCurrency, string TransactionServiceCharge, string TransactionID, string TransactionDateTime, string BankID

            string strURL, strClientCode, strClientCodeEncoded = "";
            byte[] b;
            string strResponse = "";
            //Payment Url-https://paynetzuat.atomtech.in/paynetz/epi/fts 
            //Dashboard Url-	https://paynetzuat.atomtech.in/PaynetzWebMerchant 
            //User ID	9132
            //DashBoard Login Password	Test@123
            //Transaction Password	Test@123
            //Product ID	NSE
            //Hash Request Key	KEY123657234
            //Hash Response Key	KEYRESP123657234
            //AES Request Key	A4476C2062FFA58980DC8F79EB6A799E
            //AES Request Salt/IV Key	A4476C2062FFA58980DC8F79EB6A799E
            //AES Response Key	75AEF0FA1B94B3C10D4F5B268F757F11
            //AES Response Salt/IV Key	75AEF0FA1B94B3C10D4F5B268F757F11

            string MerchantLogin = "9132";//"197";
            string MerchantPass = "Test@123";//"Test@123";        
            string TransactionType = "NBFundtransfer";
            string ProductID = "NSE";
            string TransactionID = "DE" + System.DateTime.Now.ToString("yyyyMMddHHmmssffffff"); //System.DateTime.Now.Date.ToString("ddMMyyyyhhhmmsstt");// "123";
            string TransactionAmount = "1";
            string TransactionServiceCharge = "0";
            string TransactionCurrency = "INR";
            string BankID = "2001";
            string ru = "http://localhost:53943/ATOMSuccess.aspx";
            //string ru = "http://localhost:258252/Pages/FundTransferFailed.aspx";
            string TransactionDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//System.DateTime.Now.ToString();// DateTime.Now.ToString("dd-MM-yyyy"); //"09-04-2021";DD/MM/YYYY HH:MM:SS
            string CustomerAccountNo = "20069686727";
            string MerchantDiscretionaryData = "";

            try
            {
                b = Encoding.UTF8.GetBytes(ClientCode);
                strClientCode = Convert.ToBase64String(b);
                strClientCodeEncoded = HttpUtility.UrlEncode(strClientCode);
                strURL = "" + ConfigurationManager.AppSettings["TransferURL"].ToString();///
                strURL = strURL.Replace("[MerchantLogin]", MerchantLogin + "&");
                strURL = strURL.Replace("[MerchantPass]", MerchantPass + "&");
                strURL = strURL.Replace("[TransactionType]", TransactionType + "&");
                strURL = strURL.Replace("[ProductID]", ProductID + "&");
                strURL = strURL.Replace("[TransactionAmount]", TransactionAmount + "&");
                strURL = strURL.Replace("[TransactionCurrency]", TransactionCurrency + "&");
                strURL = strURL.Replace("[TransactionServiceCharge]", TransactionServiceCharge + "&");
                strURL = strURL.Replace("[ClientCode]", strClientCodeEncoded + "&");
                strURL = strURL.Replace("[TransactionID]", TransactionID + "&");
                strURL = strURL.Replace("[TransactionDateTime]", TransactionDateTime + "&");
                strURL = strURL.Replace("[CustomerAccountNo]", CustomerAccountNo + "&");
                strURL = strURL.Replace("[MerchantDiscretionaryData]", MerchantDiscretionaryData + "&");
                strURL = strURL.Replace("[BankID]", BankID + "&");
                strURL = strURL.Replace("[ru]", ru + "&");// Remove on Production
                TransactionAmount = "100"; //txtAmt.Text;
                //  string reqHashKey = requestkey;
                string reqHashKey = "KEY123657234"; //"KEY123657234";
                string signature = "";
                string strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TransactionID + TransactionAmount + TransactionCurrency;
                byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
                byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
                signature = byteToHexString(bt).ToLower();
                strURL = strURL.Replace("[signature]", signature);


                string passphrase = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";
                string salt = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;
                string plaintext = "ABC123";
                string hashAlgorithm = "SHA1";
                //string Encryptval = Encrypt(plaintext, passphrase, salt, iv, iterations);

                //Payment Url-https://paynetzuat.atomtech.in/paynetz/epi/fts 
                //Dashboard Url-	https://paynetzuat.atomtech.in/PaynetzWebMerchant 
                //User ID	9132
                //DashBoard Login Password	Test@123
                //Transaction Password	Test@123
                //Product ID	NSE
                //Hash Request Key	KEY123657234
                //Hash Response Key	KEYRESP123657234
                //AES Request Key	A4476C2062FFA58980DC8F79EB6A799E
                //AES Request Salt/IV Key	A4476C2062FFA58980DC8F79EB6A799E
                //AES Response Key	75AEF0FA1B94B3C10D4F5B268F757F11
                //AES Response Salt/IV Key	75AEF0FA1B94B3C10D4F5B268F757F11


                plaintext = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&prodid=NSE&amt=" + TransactionAmount + "&txncurr=INR&txnscamt=0&clientcode=" + strClientCodeEncoded + "&txnid=" + TransactionID + "&date=" + TransactionDateTime + "&custacc=20069766737&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=" + ru + "&udf3=9999999999&signature=" + signature;
                string Encryptval = Encrypt(plaintext, passphrase, salt, iv, iterations);
                strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + MerchantLogin + "&encdata=" + Encryptval;
                //string req = "login=9132&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=100.00&txncurr=INR&txnscamt=0&clientcode=TkFWSU4=&txnid=M123&date=03/04/2020&custacc=100000036600&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=https://pgtest.atomtech.in/paynetzclient/ResponseParam.jsp&udf3=9999999999&signature=60be76a00eae711826095afe80874822c1e261cb7150cfe4828ba83721c416b0dcb92af439a63f27d875a4d1d5e53f4a3e028acfe52504952776d507386253f3";
                //string finalreq = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=9132&encdata=EE09C765D78EB9AA8406378CAA3E230917A126B5D52A15A1D953D5E15411C9561A8860BE1AC5716B9D21275D6B01E506439927C78DC6CA3C345C0CCA2344109DB6317F2349294480A7411B0EEE0E536EAD507F1AE1B4BADDC7F84F2E3364A5F768E7D81CD6F1AE32A4C75E2F5887DD9D00F9CD232EFE8B99263A70A107E75EA778E6FDE63771099AD7C3461EA68B13F346D410FD3F6489382A9857EC495CE5B4D9308F33E18E6F36DA86E66F4B8E56AA74CE98EA7FDAA62E49CDB3FCD6E2D0B112650B36E6E4971C7A48F93B51E239857E3CCD23DAD904BBF321182083E02CB1FC635A459637FB9F033DC7C1F93D1A276B3F5FAB7EF334D1A6EA5B8DA93915FC4BF9AFB118F3E9A64BFD1029BC936C267CCBF01B4A85E907E7D292332F4819A42B612CE1D01A9DDFCCE19D5DDAF27FA8EAAA46DB9A2DD4EB11369D5B64F2A9BC75208B0AC66F55260DC797A99469C5CE3980AE917F047D38D4A7D7C87594BDC0C79B4CEBDA0B8BBD79156862E6562C49981C1858FABF62D0689B704715B709ABD4FEB48FBD5A506042E7FA74305A2D18E7CFEF28E75AA4A04A3ECFB482B307E8363CBAE0C88ACC2BA7F51BA7AB4F4465";


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; // comparable to modern browsers

                // HttpContext.Current.Response.Redirect(strURL, false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #region ATOM PAYMENT GATEWAY

        public void TransferFundNew()
        {

            string strURL = "", strClientCode, strClientCodeEncoded = "";
            byte[] b;
            string ClientCode = "9132";
            string MerchantLogin = "9132";//"197";
            string MerchantPass = "Test@123";//"Test@123";        
            string TransactionType = "NBFundtransfer";
            string ProductID = "NSE";
            string TransactionID = "DE" + System.DateTime.Now.ToString("yyyyMMddHHmmssffffff"); //System.DateTime.Now.Date.ToString("ddMMyyyyhhhmmsstt");// "123";
            string TransactionAmount = "1";
            string TransactionServiceCharge = "0";
            string TransactionCurrency = "INR";
            string BankID = "2001";
            string ru = "http://localhost:53943/ATOMSuccess.aspx";
            string TransactionDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//System.DateTime.Now.ToString();// DateTime.Now.ToString("dd-MM-yyyy"); //"09-04-2021";DD/MM/YYYY HH:MM:SS
            string CustomerAccountNo = "20069686727";
            string MerchantDiscretionaryData = "";

            try
            {
                b = Encoding.UTF8.GetBytes(ClientCode);
                strClientCode = Convert.ToBase64String(b);
                strClientCodeEncoded = HttpUtility.UrlEncode(strClientCode);
                TransactionAmount = "100"; //txtAmt.Text;
                //  string reqHashKey = requestkey;
                string reqHashKey = "KEY123657234"; //"KEY123657234";
                string signature = "";
                string strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TransactionID + TransactionAmount + TransactionCurrency;
                byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
                byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
                signature = byteToHexString(bt).ToLower();
                strURL = strURL.Replace("[signature]", signature);


                string passphrase = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";
                string salt = "A4476C2062FFA58980DC8F79EB6A799E"; //"8E41C78439831010F81F61C344B7BFC7";
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;
                string plaintext = "ABC123";
                string hashAlgorithm = "SHA1";
                plaintext = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&prodid=NSE&amt=" + TransactionAmount + "&txncurr=INR&txnscamt=0&clientcode=" + strClientCodeEncoded + "&txnid=" + TransactionID + "&date=" + TransactionDateTime + "&custacc=20069766737&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=" + ru + "&udf3=9999999999&signature=" + signature;
                string Encryptval = Encrypt(plaintext, passphrase, salt, iv, iterations);
                strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + MerchantLogin + "&encdata=" + Encryptval;
                //string req = "login=9132&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=100.00&txncurr=INR&txnscamt=0&clientcode=TkFWSU4=&txnid=M123&date=03/04/2020&custacc=100000036600&udf1=smithbob&udf9=ABCD&udf10=ABCD123&udf11=DUMMY1313&ru=https://pgtest.atomtech.in/paynetzclient/ResponseParam.jsp&udf3=9999999999&signature=60be76a00eae711826095afe80874822c1e261cb7150cfe4828ba83721c416b0dcb92af439a63f27d875a4d1d5e53f4a3e028acfe52504952776d507386253f3";
                //string finalreq = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=9132&encdata=EE09C765D78EB9AA8406378CAA3E230917A126B5D52A15A1D953D5E15411C9561A8860BE1AC5716B9D21275D6B01E506439927C78DC6CA3C345C0CCA2344109DB6317F2349294480A7411B0EEE0E536EAD507F1AE1B4BADDC7F84F2E3364A5F768E7D81CD6F1AE32A4C75E2F5887DD9D00F9CD232EFE8B99263A70A107E75EA778E6FDE63771099AD7C3461EA68B13F346D410FD3F6489382A9857EC495CE5B4D9308F33E18E6F36DA86E66F4B8E56AA74CE98EA7FDAA62E49CDB3FCD6E2D0B112650B36E6E4971C7A48F93B51E239857E3CCD23DAD904BBF321182083E02CB1FC635A459637FB9F033DC7C1F93D1A276B3F5FAB7EF334D1A6EA5B8DA93915FC4BF9AFB118F3E9A64BFD1029BC936C267CCBF01B4A85E907E7D292332F4819A42B612CE1D01A9DDFCCE19D5DDAF27FA8EAAA46DB9A2DD4EB11369D5B64F2A9BC75208B0AC66F55260DC797A99469C5CE3980AE917F047D38D4A7D7C87594BDC0C79B4CEBDA0B8BBD79156862E6562C49981C1858FABF62D0689B704715B709ABD4FEB48FBD5A506042E7FA74305A2D18E7CFEF28E75AA4A04A3ECFB482B307E8363CBAE0C88ACC2BA7F51BA7AB4F4465";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; // comparable to modern browsers

                // HttpContext.Current.Response.Redirect(strURL, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string byteToHexString(byte[] byData)
        {
            StringBuilder sb = new StringBuilder((byData.Length * 2));
            for (int i = 0; (i < byData.Length); i++)
            {
                int v = (byData[i] & 255);
                if ((v < 16))
                {
                    sb.Append('0');
                }

                sb.Append(v.ToString("X"));

            }

            return sb.ToString();
        }

        public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


            return data;
        }
        public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            byte[] str = HexStringToByte(plainText);

            string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
            return data1;
        }
        public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        }
        public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
        {
            var saltBytes = new byte[16];
            var ivBytes = new byte[16];
            Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations);
            saltBytes = rfcdb.GetBytes(32);
            var tempBytes = iv;
            Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
            var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
            rij.Mode = CipherMode.CBC;
            rij.Padding = PaddingMode.PKCS7;
            rij.FeedbackSize = 128;
            rij.KeySize = 128;

            rij.BlockSize = 128;
            rij.Key = saltBytes;
            rij.IV = ivBytes;
            return rij;
        }
        protected static byte[] HexStringToByte(string hexString)
        {
            try
            {
                int bytesCount = (hexString.Length) / 2;
                byte[] bytes = new byte[bytesCount];
                for (int x = 0; x < bytesCount; ++x)
                {
                    bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
                }
                return bytes;
            }
            catch
            {
                throw;
            }
        }
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        #endregion
        public string InsertCallBackAPIRespose_ATOM(string IPAddress, string Response)
        {            
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderATOM"]);
            string msg = "";
            int temp = 0;
           string[] strSplitDecryptedResponse;
            string MerchantID="", MerchantTxnID = "", AMT = "", VERIFIED = "", BID = "", BankName = "", AtomTxnId = "", Discriminator = "", Surcharge = "";
            string CardNumber = "", TxnDate = "", CustomerAccNo = "", Clientcode = "", AgentId = "", Status = "", CreatedBy = "";
            try
            {
                #region ATOM Response Parse
                if (!String.IsNullOrEmpty(Response))
                {
                    #region Get Value from Response
                    string strDecryptedVal = Response;// Response.ToLower();                    
                    strSplitDecryptedResponse = strDecryptedVal.Split('&');
                    string[] parameters = strSplitDecryptedResponse;
                    string[] strGetMerchantParamForCompare;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                        if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("MerchantID").ToLower())
                        {
                            MerchantID = Convert.ToString(strGetMerchantParamForCompare[1]);                            
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("MerchantTxnID").ToLower())
                        {
                            MerchantTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("AMT").ToLower())
                        {
                            AMT = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("VERIFIED").ToLower())
                        {
                            VERIFIED = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("BID").ToLower())
                        {
                            BID = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("BankName").ToLower())
                        {
                            BankName = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("AtomTxnId").ToLower())
                        {
                            AtomTxnId = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("Discriminator").ToLower())
                        {
                            Discriminator = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("Surcharge").ToLower())
                        {
                            Surcharge = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("CardNumber").ToLower())
                        {
                            CardNumber = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("TxnDate").ToLower())
                        {
                            TxnDate = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("CustomerAccNo").ToLower())
                        {
                            CustomerAccNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == ("Clientcode").ToLower())
                        {
                            Clientcode = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                    }
                    #endregion
                }
                #endregion
                #region INSERT ATOM CALL BACK API PG Response
                Status = VERIFIED;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SP_InsertCallBackAPIRespose_ATOM", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MerchantID", MerchantID);
                cmd.Parameters.AddWithValue("@MerchantTxnID", MerchantTxnID);
                cmd.Parameters.AddWithValue("@AMT", AMT);
                cmd.Parameters.AddWithValue("@VERIFIED", VERIFIED);
                cmd.Parameters.AddWithValue("@BID", BID);
                cmd.Parameters.AddWithValue("@BankName", BankName);
                cmd.Parameters.AddWithValue("@AtomTxnId", AtomTxnId);
                cmd.Parameters.AddWithValue("@Discriminator", Discriminator);
                cmd.Parameters.AddWithValue("@Surcharge", Surcharge);
                cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
                cmd.Parameters.AddWithValue("@TxnDate", TxnDate);
                cmd.Parameters.AddWithValue("@CustomerAccNo", CustomerAccNo);
                cmd.Parameters.AddWithValue("@Clientcode", Clientcode);
                cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Response", Response);                
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);                
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    msg = "yes";
                    //msg = "yes^" + OrderId + "^" + AgentId;
                }
                #endregion
            }
            catch (Exception ex)
            {                
                int insert = objPg.InsertExceptionLog("ATOMPaymentGateway", "UpdatePaymentResponse_ATOM", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return msg;
        }
    }
}
