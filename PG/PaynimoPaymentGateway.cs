//using CCA.Util;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace PG
{
    public class PaynimoPaymentGateway
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adap;
        PG.PaymentGateway objPg = new PG.PaymentGateway();
        public string PaymentGatewayReqPaynimo(string TrackId, string TId, string IBTrackId, string AgentId, string AgencyName, double TotalAmount, double OrignalAmount, string BillingName, string BillingAddress, string BillingCity, string BillingState, string BillingZip, string BillingTel, string BillingEmail, string ServiceType, string IP, string Trip, string PaymentOption)
        {
            string Reference = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            TId = "TP" + Reference.Substring(4, 16);
            bool IsValid = false;
            string MerchantID = "";
            string ProviderUrl = "";
            string SuccessUrl = "";
            string CancelUrl = "";
            string FailureUrl = "";
            string postHtml = "";
            int flag = 0;
            string ccaRequest = "";
            string strEncRequest = "";
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
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
                DataTable dt = new DataTable();
                dt = GetPgCredential(Provider, PaymentOption);
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
                        #region Paynimo Date:09-04-2021
                        //Session["Merchant_Code"] = "T625110";
                        //Session["IsKey"] = "8912431315HRPQEF";
                        //Session["IsIv"] = "1410217746QVVYWQ";
                        string requesttype = "T";
                        string merchantcode = MerchantID;// "T625110 ";//MerchantID                        
                        string IsKey = MERCHANT_KEY;// "8912431315HRPQEF";//MERCHANT_KEY
                        string IsIv = SALT;// "1410217746QVVYWQ";//SALT
                        string Bankcode = "470";
                        string MerchantTxnRefNo = TrackId;// "09042021175926";
                        string ITC = AgencyName;//"Saleel_K";//More Discription 
                        string Amount = Convert.ToString(TotalAmount);//"1";
                        string Currencycode = "INR";
                        string uniqueCustomerID = AgentId;//TrackId;//"09042021175926";
                        string returnURL = SuccessUrl;// "http://localhost:51684/ResponsePage.aspx";//SuccessUrl
                        string StoSreturnURL = "";
                        string TPSLTXNID = "";
                        string Shoppingcartdetails = "FIRST_" + Convert.ToString(TotalAmount).Trim() + "_0.0";// "Test_1.00_0.0";
                        string TxnDate = DateTime.Now.ToString("dd-MM-yyyy"); //"09-04-2021";
                        string Email = BillingEmail;// "devesh.singh@itq.in";
                        string mobileNo = BillingTel;//"9871186224";
                        // TrackId,  TId,  IBTrackId,  AgentId,  AgencyName, double TotalAmount, double OrignalAmount, 
                        // BillingName,  BillingAddress,  BillingCity,  BillingState,  BillingZip,  BillingTel,  
                        //BillingEmail,  ServiceType,  IP,  Trip,  PaymentOption
                        string customerName = BillingName;// "Devesh";
                        string CardID = "";
                        string AccountNo = "";
                        String response = "";
                        try
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            RequestURL objRequestURL = new RequestURL();
                            if (requesttype == "T") //TXT_requesttype.Text.ToUpper() == "T" || TXT_requesttype.Text.ToUpper() == "S" || TXT_requesttype.Text.ToUpper() == "O" || TXT_requesttype.Text.ToUpper() == "R")
                            {
                                //if (TXT_requesttype.Text.ToUpper() == "T" || TXT_requesttype.Text.ToUpper() == "S" || TXT_requesttype.Text.ToUpper() == "O" || TXT_requesttype.Text.ToUpper() == "R")
                                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                response = objRequestURL.SendRequest
                                          (
                                          requesttype
                                          , merchantcode
                                          , MerchantTxnRefNo
                                          , ITC
                                          , Amount
                                          , Currencycode
                                          , uniqueCustomerID
                                          , returnURL
                                          , StoSreturnURL
                                          , TPSLTXNID
                                          , Shoppingcartdetails
                                          , TxnDate
                                          , Email
                                          , mobileNo
                                          , Bankcode
                                          , customerName
                                          , CardID
                                          , AccountNo
                                          , IsKey
                                          , IsIv
                                          );
                            }
                            String strResponse = response.ToUpper();
                            //bool IsValid = false;
                            if (strResponse.StartsWith("ERROR"))
                            {
                                if (strResponse == "ERROR073")
                                {
                                    IsValid = false;
                                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    response = objRequestURL.SendRequest
                                               (
                                                requesttype
                                                , merchantcode
                                                , MerchantTxnRefNo
                                                , ITC, Amount
                                                , Currencycode
                                                , uniqueCustomerID
                                                , returnURL
                                                , StoSreturnURL
                                                , TPSLTXNID
                                                , Shoppingcartdetails
                                                , TxnDate
                                                , Email
                                                , mobileNo
                                                , Bankcode
                                                , customerName
                                                , CardID
                                                , AccountNo
                                                , IsKey
                                                , IsIv
                                               );
                                    strResponse = response.ToUpper();
                                }
                                else
                                {
                                    // lblResponse.Text = response;
                                }
                            }
                            else
                            {
                                IsValid = true;
                            }
                            if (requesttype.Trim() == "T" && IsValid == true)
                            {
                                //Session["Merchant_Code"] = TXT_merchantcode.Text;
                                //Session["IsKey"] = TXT_IsKey.Text;
                                //Session["IsIv"] = TXT_IsIv.Text;


                                StringBuilder strForm = new StringBuilder();
                                strForm.Append("<form name='s1_2' id='s1_2' action='" + response + "' method='post'> ");
                                strForm.Append("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
                                strForm.Append("</script>");
                                strForm.Append("<script language='javascript' >");
                                strForm.Append("</script>");
                                strForm.Append("</form> ");
                                postHtml = Convert.ToString(strForm);
                                //postHtml = response;                                
                            }
                            else
                            {
                                if (response == "")
                                {
                                    //lblResponse.Text = "Transaction Fail " + "ERROR:";
                                }
                                else
                                {
                                    //lblResponse.Text = response;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            IsValid = false;
                            int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaynimoPaymentGateway.cs", "Paynimo dll error,When making request", "insert", ex.Message, ex.StackTrace);
                            //throw;
                        }
                        #endregion
                        // postHtml = PreparePOSTForm(action1, data);  

                        string req1 = "FlywidusPaymentMode=" + PaymentOption + ",requesttype=" + requesttype + ",merchantcode=" + merchantcode + ",IsKey=" + IsKey + ",IsIv=" + IsIv + ",Bankcode=" + Bankcode + ",MerchantTxnRefNo=" + MerchantTxnRefNo;
                        string req2 = ",ITC=" + ITC + ",Amount=" + Amount + ",Currencycode=" + Currencycode + ",uniqueCustomerID=" + uniqueCustomerID + ",returnURL=" + returnURL;
                        string req3 = ",StoSreturnURL=" + StoSreturnURL + ",TPSLTXNID=" + TPSLTXNID + ",Shoppingcartdetails=" + Shoppingcartdetails + ",TxnDate=" + TxnDate + ",Email=" + Email + ",mobileNo=" + mobileNo;
                        ccaRequest = req1 + req2 + req3;
                        strEncRequest = response;

                        #region T Type Request
                        try
                        {
                            int reqFlag = InsertPaymentRequestDetails(TrackId, TId, AgentId, requesttype, merchantcode, MerchantTxnRefNo, ITC, Amount, Currencycode, uniqueCustomerID, returnURL, StoSreturnURL, TPSLTXNID, Shoppingcartdetails, TxnDate, Email, mobileNo, Bankcode, customerName, CardID, AccountNo, IsKey, IsIv, 0);
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion
                    }
                    else
                    {
                        postHtml = "no^" + "PaymentGateway Credential not found";
                        int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaymentGatewayReqPaynimo", TrackId, "insert", postHtml, "No row found in GetPgCredential data table");
                        //return postHtml;
                    }
                }
                else
                {
                    postHtml = "no^" + "PaymentGateway Credential not found";
                    int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaymentGatewayReqPaynimo", TrackId, "insert", postHtml, "GetPgCredential data table is null or empty");
                }
                flag = objPg.InsertPaymentRequestDetails(TrackId, TId, IBTrackId, BillingName, "Paynimo", BillingEmail, BillingTel, BillingAddress, TotalAmount, OrignalAmount, AgentId, AgencyName, IP, ccaRequest, ServiceType, strEncRequest, Trip, TotalPgCharges, TransCharges, ChargesType, postHtml);
                #endregion -End PG Credential and Making Request
                if (flag > 0 && IsValid == true)
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
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaynimoPaymentGateway", "PaymentGatewayReqPaynimo", "insert", ex.Message, ex.StackTrace);
                return postHtml;
            }

            return postHtml;
        }
        public DataTable GetPgCredential(string provider,string PaymentMode)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            DataTable dt = new DataTable();
            // string provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
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
                //int insert = InsertExceptionLog("PaymentGateway", "PaynimoPaymentGateway", "PaymentGatewayReqPaynimo", "insert", ex.Message, ex.StackTrace);
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaynimoPaymentGateway", "GetPgCredential", "select", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return dt;
        }
                
        public string UpdatePaymentResponsePaynimo(string SeesionAgentId, string PGEncryptRes)
        {
            string AgentId = "";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
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
            string MerchantCode = string.Empty;
            string IsIv = string.Empty;
            string IsKey = string.Empty;
            string PropertyFile = Convert.ToString(ConfigurationManager.AppSettings["ErrorFile"]);
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
            //string CrdPaymentMode = string.Empty;
            #endregion
            try
            {
                #region Paynimo Response Parse
                if (!String.IsNullOrEmpty(PGEncryptRes))//(PgResponse))
                {
                    DataTable dt = new DataTable();
                    dt = GetPgCredential(Provider, "Paynimo");
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MerchantCode = Convert.ToString(dt.Rows[0]["MerchantID"]);// "T625110";
                            IsIv = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"1410217746QVVYWQ";
                            IsKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"8912431315HRPQEF";
                            //ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);                            
                        }
                    }
                    RequestURL objRequestURL = new RequestURL();    //Creating Object of Class DotNetIntegration_1_1.RequestURL
                    /*string strDecryptedVal = null; */                 //Decrypting the PG response
                    PropertyFile = "";
                    if (!String.IsNullOrEmpty(PropertyFile))
                    {
                        string strFilePath = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);
                        string[] FilePath = strFilePath.Split('\\');
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, strFilePath);
                        PgResponse = PgResponse + strDecryptedVal;
                    }
                    else
                    {
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, IsKey, IsIv);
                        PgResponse = PgResponse + strDecryptedVal;
                    }


                    if (strDecryptedVal.StartsWith("ERROR"))
                    {
                        //return Error Message
                        // lblValidate.Text = strDecryptedVal;
                    }
                    else
                    {
                        strSplitDecryptedResponse = strDecryptedVal.Split('|');
                        string[] parameters = strSplitDecryptedResponse;
                        // GetPGRespnseData(strSplitDecryptedResponse);

                        #region Get Value from Response
                        //GetPGRespnseData(string[] parameters)
                        string[] strGetMerchantParamForCompare;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                            if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                            {
                                strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                                ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_REF")
                            {
                                //strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                OrderId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_BANK_CD")
                            {
                                //strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                CardType = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_ID")
                            {
                                //strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                                BankRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PaymentId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                            {
                                //strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PgAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                MerAamount = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_msg")
                            {
                                // ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PGStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_err_msg")
                            {
                                ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
                            //{
                            //    try
                            //    {
                            //        strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                            //        strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                            //        strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                            //        strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                            //    }
                            //    catch (Exception ex)
                            //    { }
                            //}
                        }
                        //
                        #endregion
                        //PaymentId,Status,ResponseCode,BankRefNo,PgAmount,MerAamount,PaymentMode,CardName,DiscountValue,CardType,UnmappedStatus,ApiStatus                        
                        if (strPG_TxnStatus == "0300" || strPG_TxnStatus == "0200")
                        {
                            try
                            {
                                Status = PGStatus.ToLower().Trim();// "success";
                                ApiStatus = "success";
                                UnmappedStatus = "captured";
                                // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                            }
                            catch (Exception ex)
                            { }
                        }
                        else
                        {
                            Status = "failure";
                            if (strPG_TxnStatus != "0300" && strPG_TxnStatus != "0200")
                            {
                                Status = ResponseCode;
                                ApiStatus = "failure";
                                UnmappedStatus = "failure";
                                //lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                            }
                            try
                            {
                                strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                                ApiStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                                UnmappedStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                            }
                            catch (Exception ex)
                            { }

                            //lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                        }
                    }
                }
                #endregion
                #region Get UserId/AgentId from database 

                DataSet ds = GetAgentIdByOrderid(OrderId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                {
                    AgentId = Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]);
                }
                msg = msg + OrderId + "~" + AgentId;
                //Check Db AgentId and Session AgentId              
                #endregion
                #region Cross Verification  by API
                PaymentMode = GetPaymentMode(OrderId, AgentId);
                ApiResponse = PaymentVerificationRequestPaynimo("S", OrderId, BankRefNo, AgentId, strDecryptedVal, PaymentMode);
                string TxnStatusAPI = string.Empty;
                string ResponseCodeAPI = string.Empty;
                string API_txn_msg = string.Empty;
                if (!string.IsNullOrEmpty(ApiResponse))
                {
                    //string[] strSplitApiResponse;
                    //strSplitApiResponse = ApiResponse.Split('|');
                    string[] APIparameters = ApiResponse.Split('|');
                    // GetPGRespnseData(strSplitDecryptedResponse);                    
                    #region Get Value from Response
                    //GetPGRespnseData(string[] parameters)
                    string[] strGetMerchantParamForCompare;
                    for (int i = 0; i < APIparameters.Length; i++)
                    {
                        strGetMerchantParamForCompare = APIparameters[i].ToString().Split('=');
                        if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                        {
                            TxnStatusAPI = Convert.ToString(strGetMerchantParamForCompare[1]);
                            ResponseCodeAPI = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_msg")
                        {
                            API_txn_msg = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_err_msg")
                        {
                            ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                        //{
                        //    //strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //    PgAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //    MerAamount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //}

                    }
                }
                //
                if (TxnStatusAPI == "0300")
                {
                    try
                    {
                        //Status = PGStatus.ToLower().Trim();// "success";
                        ApiStatus = API_txn_msg.ToLower().Trim();// "success";
                        UnmappedStatus = "captured";
                        // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                    }
                    catch (Exception ex)
                    { }
                }
                else
                {
                    // ApiStatus = PGApiStatus;
                    ApiStatus = "failure";
                    UnmappedStatus = "failure";
                }
                #endregion

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
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
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
                cmd.Parameters.AddWithValue("@ApiStatus", ApiStatus);// "");
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
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "UpdatePaymentResponsePaynimo", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
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
                    //    int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PGResponsParse", "UserIdPassword dataset is null or empty", Convert.ToString(Session["UID"]), RefernceNo, pgResponse);
                    //}
                }
                else
                {
                    int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "GetAgentIdByOrderid", RefernceNo, RefernceNo, RefernceNo, RefernceNo);
                }
                #endregion
            }
            catch (Exception ex)
            {
                int insert1 = objPg.InsertExceptionLog("PaynimoPaymentGateway", "GetAgentIdByOrderid", RefernceNo, RefernceNo, ex.Message, ex.StackTrace);
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
                adap.SelectCommand.Parameters.AddWithValue("@Action", "GetUserId");
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
        #region Payment Verification Request
        
        public string PaymentVerificationRequestPaynimo(string RequestType, string TrackId, string TPSLTxnID, string AgentId, string PGResponse ,string PaymentMode)
        {
            //PaymentMakingRequestPaynimo
            //PaymentVerificationRequestPaynimo
            //RequestType,MerchantCode,MerchantTxnRefNumber,TPSLTxnID,txnDate,Key,Iv
            //MerchantTxnRefNumber or TPSLTxnID should be provided
            //Either PropertyPath or Key,IV pair should be provided. This is mandatory.
            string TxnDate = "";
            bool IsValid = false;
            string MerchantID = "";
            string ProviderUrl = "";
            string SuccessUrl = "";
            string CancelUrl = "";
            string FailureUrl = "";
            string postHtml = "";
            //string ccaRequest = "";
            //string strEncRequest = "";
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
            string SALT = "";
            string MERCHANT_KEY = "";
            string HashSequence = "";
            String response = "";
            string strReq = string.Empty;
            try
            {
                //Devesh
                #region Get PG Credential and Making Request- New Code
                
                DataTable dt = new DataTable();
                dt = GetPgCredential(Provider, PaymentMode);
                if (dt != null)
                {
                    DataTable REQDT = new DataTable();
                    REQDT = Get_T_TypeRequestDetails(TrackId, AgentId);
                    if (dt.Rows.Count > 0 && REQDT != null && REQDT.Rows.Count > 0)
                    {
                        MerchantID = Convert.ToString(dt.Rows[0]["MerchantID"]);
                        SALT = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                        MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                        ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);
                        //SuccessUrl = Convert.ToString(dt.Rows[0]["SuccessUrl"]);
                        SuccessUrl = Convert.ToString(dt.Rows[0]["FailureUrl"]);
                        CancelUrl = Convert.ToString(dt.Rows[0]["CancelUrl"]);
                        HashSequence = Convert.ToString(dt.Rows[0]["HashSequence"]);
                        #region Paynimo Date:09-04-2021
                        //Session["Merchant_Code"] = "T625110";
                        //Session["IsKey"] = "8912431315HRPQEF";
                        //Session["IsIv"] = "1410217746QVVYWQ";
                        string requesttype = "S";
                        string merchantcode = MerchantID;// "T625110 ";//MerchantID                        
                        string IsKey = MERCHANT_KEY;// "8912431315HRPQEF";//MERCHANT_KEY
                        string IsIv = SALT;// "1410217746QVVYWQ";//SALT
                        string Bankcode = "";// "470";
                        string MerchantTxnRefNo = TrackId;// "09042021175926";
                        string ITC = Convert.ToString(REQDT.Rows[0]["ITC"]); //"Saleel_K";//More Discription 
                        string Amount = Convert.ToString(REQDT.Rows[0]["Amount"]); //"1";
                        string Currencycode = "INR";
                        string uniqueCustomerID = "";// AgentId;
                        string returnURL = SuccessUrl;// "http://localhost:51684/ResponsePage.aspx";//SuccessUrl
                        string StoSreturnURL = "";
                        string TPSLTXNID = "";
                        string Shoppingcartdetails = "";//"FIRST_" + Convert.ToString(Amount).Trim() + "_0.0";// "Test_1.00_0.0";
                        TxnDate = Convert.ToString(REQDT.Rows[0]["TxnDate"]); //DateTime.Now.ToString("dd-MM-yyyy"); //"09-04-2021";
                        string Email = "";// "devesh.singh@itq.in";
                        string mobileNo = "";//"9871186224";                        
                        string customerName = "";// "Devesh";
                        string CardID = "";
                        string AccountNo = "";
                        //String response = "";
                        try
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            RequestURL objRequestURL = new RequestURL();                            
                            response = objRequestURL.SendRequest
                                      (
                                      requesttype
                                      , merchantcode
                                      , MerchantTxnRefNo
                                      , ITC
                                      , Amount
                                      , Currencycode
                                      , uniqueCustomerID
                                      , returnURL
                                      , StoSreturnURL
                                      , TPSLTXNID
                                      , Shoppingcartdetails
                                      , TxnDate
                                      , Email
                                      , mobileNo
                                      , Bankcode
                                      , customerName
                                      , CardID
                                      , AccountNo
                                      , IsKey
                                      , IsIv
                                      );
                            if (!String.IsNullOrEmpty(response))
                            {
                                String strResponse = response.ToUpper();
                                //bool IsValid = false;
                                if (strResponse.StartsWith("ERROR") || strResponse.ToUpper().Contains("THE UNDERLYING CONNECTION WAS CLOSED") || strResponse.ToUpper().Contains("THE REQUEST WAS ABORTED") || strResponse.ToUpper().Contains("COULD NOT CREATE SSL/TLS"))
                                {
                                    IsValid = false;
                                    if (strResponse == "ERROR073")
                                    {
                                        IsValid = false;
                                        // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; 
                                        response = objRequestURL.SendRequest
                                                   (
                                                    requesttype
                                                    , merchantcode
                                                    , MerchantTxnRefNo
                                                    , ITC, Amount
                                                    , Currencycode
                                                    , uniqueCustomerID
                                                    , returnURL
                                                    , StoSreturnURL
                                                    , TPSLTXNID
                                                    , Shoppingcartdetails
                                                    , TxnDate
                                                    , Email
                                                    , mobileNo
                                                    , Bankcode
                                                    , customerName
                                                    , CardID
                                                    , AccountNo
                                                    , IsKey
                                                    , IsIv
                                                   );
                                        strResponse = response.ToUpper();
                                    }
                                    else
                                    {
                                        // lblResponse.Text = response;
                                    }
                                }
                                else
                                {
                                    IsValid = true;
                                }
                            }
                            else
                            {
                                IsValid = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            IsValid = false;
                            int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaynimoPaymentGateway", "Paynimo dll error,When making request", "insert", ex.Message, ex.StackTrace);
                            //throw;
                        }
                        #endregion
                        // postHtml = PreparePOSTForm(action1, data);  

                        string req1 = "requesttype=" + requesttype + ",merchantcode=" + merchantcode + ",IsKey=" + IsKey + ",IsIv=" + IsIv + ",Bankcode=" + Bankcode + ",MerchantTxnRefNo=" + MerchantTxnRefNo;
                        string req2 = ",ITC=" + ITC + ",Amount=" + Amount + ",Currencycode=" + Currencycode + ",uniqueCustomerID=" + uniqueCustomerID + ",returnURL=" + returnURL;
                        string req3 = ",StoSreturnURL=" + StoSreturnURL + ",TPSLTXNID=" + TPSLTXNID + ",Shoppingcartdetails=" + Shoppingcartdetails + ",TxnDate=" + TxnDate + ",Email=" + Email + ",mobileNo=" + mobileNo;
                        strReq = req1 + req2 + req3;
                        //strEncRequest = response;
                    }
                    else
                    {
                        postHtml = "PaymentGateway Credential not found";
                        int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaymentMakingRequestPaynimo", TrackId, RequestType + "," + TrackId + "," + TPSLTxnID + "," + TxnDate + "," + AgentId, postHtml, "No row found in GetPgCredential data table");
                        //return postHtml;
                    }
                }
                else
                {
                    postHtml = "PaymentGateway Credential not found";
                    int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaymentMakingRequestPaynimo", TrackId, RequestType + "," + TrackId + "," + TPSLTxnID + "," + TxnDate + "," + AgentId, postHtml, "GetPgCredential data table is null or empty");
                }
                #endregion -End PG Credential and Making Request
                //End Devesh
            }
            catch (Exception ex)
            {
                postHtml =  ex.Message;
                int insert = objPg.InsertExceptionLog("PaymentMakingRequestPaynimo", "PaymentMakingRequestPaynimo", TrackId, RequestType + "," + TrackId + "," + TPSLTxnID + "," + TxnDate + "," + AgentId, ex.Message, ex.StackTrace);
               // return postHtml;
            }

            try
            {
                //InsertVerificationRequest(AgentId, OrderId, PaymentId, InsertMessage,PgResponse, ApiRequest, ApiPostFormReq,ApiResponse)
                //InsertVerificationRequest(AgentId, TrackId, TrackId,   IsValid,       PGResponse, strReq,          postHtml, response);
                //(string AgentId, string OrderId, string PaymentId, string InsertMessage, string PgResponse, string ApiRequest, string ApiPostFormReq, string ApiResponse)
                string insert = InsertVerificationRequest(AgentId, TrackId, TrackId, Convert.ToString(IsValid), PGResponse, strReq, postHtml, response);
            }
            catch (Exception ex)
            { }
            return response;
        }

        public string UpdatePaymentResponsePaynimoCrossCheck(string SeesionAgentId, string PGEncryptRes, string PaymentMode)
        {
            string AgentId = "";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
            int temp = 0;
            string PaymentId = string.Empty;
            string Status = string.Empty;
            string BankRefNo = string.Empty;
            string ErrorText = string.Empty;
            string ResponseCode = string.Empty;
            //string PaymentMode = "Paynimo"; //string.Empty;
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
            string MerchantCode = string.Empty;
            string IsIv = string.Empty;
            string IsKey = string.Empty;
            string PropertyFile = Convert.ToString(ConfigurationManager.AppSettings["ErrorFile"]);


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
                #region Paynimo Response Parse
                if (!String.IsNullOrEmpty(PGEncryptRes))//(PgResponse))
                {
                    DataTable dt = new DataTable();
                    dt = GetPgCredential(Provider, PaymentMode);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MerchantCode = Convert.ToString(dt.Rows[0]["MerchantID"]);// "T625110";
                            IsIv = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"1410217746QVVYWQ";
                            IsKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"8912431315HRPQEF";
                            //ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);                            
                        }
                    }
                    RequestURL objRequestURL = new RequestURL();    //Creating Object of Class DotNetIntegration_1_1.RequestURL
                    /*string strDecryptedVal = null; */                 //Decrypting the PG response
                    PropertyFile = "";
                    if (!String.IsNullOrEmpty(PropertyFile))
                    {
                        string strFilePath = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);
                        string[] FilePath = strFilePath.Split('\\');
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, strFilePath);
                        PgResponse = PgResponse + strDecryptedVal;
                    }
                    else
                    {
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, IsKey, IsIv);
                        PgResponse = PgResponse + strDecryptedVal;
                    }


                    if (strDecryptedVal.StartsWith("ERROR"))
                    {
                        //return Error Message
                        // lblValidate.Text = strDecryptedVal;
                    }
                    else
                    {
                        strSplitDecryptedResponse = strDecryptedVal.Split('|');
                        string[] parameters = strSplitDecryptedResponse;
                        // GetPGRespnseData(strSplitDecryptedResponse);

                        #region Get Value from Response
                        //GetPGRespnseData(string[] parameters)
                        string[] strGetMerchantParamForCompare;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                            if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                            {
                                strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                                ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_REF")
                            {
                                //strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                OrderId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_BANK_CD")
                            {
                                //strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                CardType = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_ID")
                            {
                                //strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                                BankRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PaymentId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                            {
                                //strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PgAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                MerAamount = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_msg")
                            {
                                ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_err_msg")
                            {
                                ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
                            //{
                            //    try
                            //    {
                            //        strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                            //        strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                            //        strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                            //        strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                            //    }
                            //    catch (Exception ex)
                            //    { }
                            //}
                        }
                        //
                        #endregion
                        //PaymentId,Status,ResponseCode,BankRefNo,PgAmount,MerAamount,PaymentMode,CardName,DiscountValue,CardType,UnmappedStatus,ApiStatus                        
                        if (strPG_TxnStatus == "0300")
                        {
                            Status = "success";
                            ApiStatus = "success";
                            UnmappedStatus = "captured";
                            // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                            // ApiStatus == "success")
                            //lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                        }
                        else if (strPG_TxnStatus == "0200")
                        {
                            Status = "success";
                            ApiStatus = "success";
                            UnmappedStatus = "captured";
                            //lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                        }
                        else
                        {
                            Status = "failure";
                            if (strPG_TxnStatus != "0300" && strPG_TxnStatus != "0200")
                            {
                                Status = ResponseCode;
                                ApiStatus = "failure";
                                UnmappedStatus = "failure";
                                //lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                            }
                            try
                            {
                                strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                                ApiStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                                UnmappedStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                            }
                            catch (Exception ex)
                            { }

                            //lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                        }
                    }
                }
                #endregion

                #region Get UserId/AgentId from database 

                DataSet ds = GetAgentIdByOrderid(OrderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                {
                    AgentId = Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]);
                }
                msg = msg + OrderId + "~" + AgentId;
                //Check Db AgentId and Session AgentId              
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
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
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

                cmd.Parameters.AddWithValue("@Action", "paynimoupdate");
                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    msg = "yes~" + OrderId + "~" + AgentId;
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
                //}
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaymentGateway", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return msg;
        }
        public string InsertVerificationRequest(string AgentId, string OrderId, string PaymentId, string InsertMessage, string PgResponse, string ApiRequest, string ApiPostFormReq, string ApiResponse)
        {

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            int temp = 0;
            string msg = "no";
            try
            {
                //InsertVerificationRequest(AgentId, OrderId, PaymentId, InsertMessage,PgResponse, ApiRequest, ApiPostFormReq,ApiResponse)
                //InsertVerificationRequest(AgentId, TrackId, TrackId,   IsValid,       PGResponse, strReq,          postHtml, response);
                #region Insert Verification Request
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("SpInsertPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgentId", AgentId);
                cmd.Parameters.AddWithValue("@TrackId", OrderId);
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);
                cmd.Parameters.AddWithValue("@ErrorText", InsertMessage);
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
                cmd.Parameters.AddWithValue("@ApiRequest", ApiRequest + " " + ApiPostFormReq+ " "+ InsertMessage);
                cmd.Parameters.AddWithValue("@ApiResponse", ApiResponse);                
                cmd.Parameters.AddWithValue("@ApiEncryptRequest", ApiRequest + " " + ApiPostFormReq + " " + InsertMessage);
                cmd.Parameters.AddWithValue("@Action", "PDVRInsert");
                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    msg = "yes";
                }
                #endregion
            }
            catch (Exception ex)
            {
                msg = "no";
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "InsertVerificationRequest", "InsertVerificationRequest", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return msg;
        }

        #endregion

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
                //int insert = InsertExceptionLog("PaymentGateway", "PaynimoPaymentGateway", "PaymentGatewayReqPaynimo", "insert", ex.Message, ex.StackTrace);
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "PaynimoPaymentGateway", "Get_T_TypeRequestDetails", "select SP SP_Insert_Tbl_PaynimoReq", ex.Message, ex.StackTrace);
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
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "InsertPaymentRequestDetails", "SP_Insert_Tbl_PaynimoReq", "insert", ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return temp;
        }

        #region Get Payment mode Value
        public string GetPaymentMode(string RefernceNo,string AgentId)
        {
            string PaymentMode = string.Empty;
            try
            {
                #region Get details and Login
                if (!string.IsNullOrEmpty(RefernceNo))
                {
                    DataSet ds = GetPGPaymentMode(RefernceNo, AgentId);
                    if (ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                    {
                        PaymentMode = Convert.ToString(ds.Tables[0].Rows[0]["PaymentMode"]);                        
                    }
                    else
                    {
                        int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "GetPaymentMode", "GetPGPaymentMode dataset is null or empty", AgentId, RefernceNo, "Not Getting Payment mode");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "GetPaymentMode", RefernceNo, AgentId, ex.Message, ex.StackTrace);
            }
            return PaymentMode;

        }
        public DataSet GetPGPaymentMode(string ReferenceNo,string AgentId)
        {
            DataSet ds = new DataSet();
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
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
                //int insert = objPg.InsertExceptionLog("PaymentGateway", "PaynimoPaymentGateway", "Paynimo dll error,When making request", "insert", ex.Message, ex.StackTrace);
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "GetPGPaymentMode", ReferenceNo, AgentId, ex.Message, ex.StackTrace);
            }
            finally
            {
                con.Close();
                adap.Dispose();
            }
            return ds;
        }
        #endregion


        public string UpdatePaymentResponsePaynimoByPaymentMode(string SeesionAgentId, string PGEncryptRes,string PaymentMode)
        {
            string AgentId = "";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProviderPaynimo"]);
            int temp = 0;
            string PGStatus = string.Empty;
            string PaymentId = string.Empty;
            string Status = string.Empty;
            string BankRefNo = string.Empty;
            string ErrorText = string.Empty;
            string ResponseCode = string.Empty;
            //string PaymentMode = string.Empty;
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
            string MerchantCode = string.Empty;
            string IsIv = string.Empty;
            string IsKey = string.Empty;
            string PropertyFile = Convert.ToString(ConfigurationManager.AppSettings["ErrorFile"]);
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
                #region Paynimo Response Parse
                if (!String.IsNullOrEmpty(PGEncryptRes))//(PgResponse))
                {
                    DataTable dt = new DataTable();
                    dt = GetPgCredential(Provider, PaymentMode);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MerchantCode = Convert.ToString(dt.Rows[0]["MerchantID"]);// "T625110";
                            IsIv = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);//"1410217746QVVYWQ";
                            IsKey = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"8912431315HRPQEF";
                            //ProviderUrl = Convert.ToString(dt.Rows[0]["ProviderUrl"]);                            
                        }
                    }
                    RequestURL objRequestURL = new RequestURL();    //Creating Object of Class DotNetIntegration_1_1.RequestURL
                    /*string strDecryptedVal = null; */                 //Decrypting the PG response
                    PropertyFile = "";
                    if (!String.IsNullOrEmpty(PropertyFile))
                    {
                        string strFilePath = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);
                        string[] FilePath = strFilePath.Split('\\');
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, strFilePath);
                        PgResponse = PgResponse + strDecryptedVal;
                    }
                    else
                    {
                        strDecryptedVal = objRequestURL.VerifyPGResponse(PGEncryptRes, IsKey, IsIv);
                        PgResponse = PgResponse + strDecryptedVal;
                    }


                    if (strDecryptedVal.StartsWith("ERROR"))
                    {
                        //return Error Message
                        // lblValidate.Text = strDecryptedVal;
                    }
                    else
                    {
                        strSplitDecryptedResponse = strDecryptedVal.Split('|');
                        string[] parameters = strSplitDecryptedResponse;
                        // GetPGRespnseData(strSplitDecryptedResponse);

                        #region Get Value from Response
                        //GetPGRespnseData(string[] parameters)
                        string[] strGetMerchantParamForCompare;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                            if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                            {
                                strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                                ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_REF")
                            {
                                //strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                OrderId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_BANK_CD")
                            {
                                //strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                CardType = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_ID")
                            {
                                //strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                                BankRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PaymentId = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                            {
                                //strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PgAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                                MerAamount = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_msg")
                            {
                                // ResponseCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                                PGStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_err_msg")
                            {
                                ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                            }
                            //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
                            //{
                            //    try
                            //    {
                            //        strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                            //        strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                            //        strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                            //        strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                            //    }
                            //    catch (Exception ex)
                            //    { }
                            //}
                        }
                        //
                        #endregion
                        //PaymentId,Status,ResponseCode,BankRefNo,PgAmount,MerAamount,PaymentMode,CardName,DiscountValue,CardType,UnmappedStatus,ApiStatus                        
                        if (strPG_TxnStatus == "0300" || strPG_TxnStatus == "0200")
                        {
                            try
                            {
                                Status = PGStatus.ToLower().Trim();// "success";
                                ApiStatus = "success";
                                UnmappedStatus = "captured";
                                // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                            }
                            catch (Exception ex)
                            { }
                        }
                        else
                        {
                            Status = "failure";
                            if (strPG_TxnStatus != "0300" && strPG_TxnStatus != "0200")
                            {
                                Status = ResponseCode;
                                ApiStatus = "failure";
                                UnmappedStatus = "failure";
                                //lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                            }
                            try
                            {
                                strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                                ApiStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                                UnmappedStatus = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                            }
                            catch (Exception ex)
                            { }

                            //lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                        }
                    }
                }
                #endregion
                #region Get UserId/AgentId from database 

                DataSet ds = GetAgentIdByOrderid(OrderId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                {
                    AgentId = Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]);
                }
                msg = msg + OrderId + "~" + AgentId;
                //Check Db AgentId and Session AgentId              
                #endregion
                #region Cross Verification  by API
                ApiResponse = PaymentVerificationRequestPaynimo("S", OrderId, BankRefNo, AgentId, strDecryptedVal, PaymentMode);
                string TxnStatusAPI = string.Empty;
                string ResponseCodeAPI = string.Empty;
                string API_txn_msg = string.Empty;
                if (!string.IsNullOrEmpty(ApiResponse))
                {
                    //string[] strSplitApiResponse;
                    //strSplitApiResponse = ApiResponse.Split('|');
                    string[] APIparameters = ApiResponse.Split('|');
                    // GetPGRespnseData(strSplitDecryptedResponse);                    
                    #region Get Value from Response
                    //GetPGRespnseData(string[] parameters)
                    string[] strGetMerchantParamForCompare;
                    for (int i = 0; i < APIparameters.Length; i++)
                    {
                        strGetMerchantParamForCompare = APIparameters[i].ToString().Split('=');
                        if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                        {
                            TxnStatusAPI = Convert.ToString(strGetMerchantParamForCompare[1]);
                            ResponseCodeAPI = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_msg")
                        {
                            API_txn_msg = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToLower().Trim() == "txn_err_msg")
                        {
                            ErrorText = Convert.ToString(strGetMerchantParamForCompare[1]);
                        }
                        //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                        //{
                        //    //strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //    PgAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //    MerAamount = Convert.ToString(strGetMerchantParamForCompare[1]);
                        //}

                    }
                }
                //
                if (TxnStatusAPI == "0300")
                {
                    try
                    {
                        //Status = PGStatus.ToLower().Trim();// "success";
                        ApiStatus = API_txn_msg.ToLower().Trim();// "success";
                        UnmappedStatus = "captured";
                        // PaymentStatus == "success" && PayUStatus == "success" && UnmappedStatus == "captured" 
                    }
                    catch (Exception ex)
                    { }
                }
                else
                {
                    // ApiStatus = PGApiStatus;
                    ApiStatus = "failure";
                    UnmappedStatus = "failure";
                }
                #endregion

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
                cmd.Parameters.AddWithValue("@PgResponse", PgResponse);
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
                cmd.Parameters.AddWithValue("@ApiStatus", ApiStatus);// "");
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
                msg = "no~" + OrderId + "~" + AgentId;                
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGateway", "UpdatePaymentResponsePaynimo", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
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
