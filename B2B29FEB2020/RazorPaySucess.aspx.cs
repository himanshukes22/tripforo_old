using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG;
using paytm;
using PaytmWall;
using Razorpay.Api;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Razorpay.Api.Errors;
using Newtonsoft.Json;

public partial class RazorPaySucess : System.Web.UI.Page
{
    PG.PaymentGateway obPg = new PG.PaymentGateway();
    string pgResponse = string.Empty;
    SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        //pgResponse = Request.Form["encResp"];
        pgResponse = Request.Form.ToString();
        string PgRequestData = string.Empty;
        string ObTid = "";// Request.QueryString["OBTID"];
        string IbTid = "";//Request.QueryString["IBTID"];
        string Ft = "";// Request.QueryString["FT"]; 
        string PgTid = "";
        string ServiceType = "";
        string PaymentStatus = string.Empty;
        string ApiStatus = string.Empty;
        string PayUStatus = "False";
        string UnmappedStatus = string.Empty;
        string CreditLimitUpdate = string.Empty;
        string orderID = "";
        string PaytmCheckSum = "False";
        string pgMessage = "";
        string Trip = "";
        DataSet dsorder = new DataSet();
        #region RAZOR log save
        if (1 == 1)
        {
            try
            {
                int insert = obPg.InsertExceptionLog("RazorPay", "RazorPaySucess.aspx.cs", "Log- RazorPay", Convert.ToString(Session["UID"]), "RAZOR", Request.Form.ToString());
            }
            catch (Exception ex)
            {
                int insert = obPg.InsertExceptionLog("RazorPay", "RazorPaySucess.aspx.cs", "RazorPay Log Save in Database", "All Log Insert", ex.Message, ex.StackTrace);
            }
        }
        #endregion
        try
        {
            try
            {
                dsorder = GetOrderID(Request.Form["razorpay_order_id"].ToString());
                orderID = (Convert.ToString(dsorder.Tables[0].Rows[0][0].ToString()));
                if (dsorder.Tables.Count > 0)
                {
                    PGResponsParse(Convert.ToString(dsorder.Tables[0].Rows[0][0].ToString()));
                }
            }
            catch(Exception ex)
            {
                int insert = obPg.InsertExceptionLog("RazorPayGETORDERid", "RazorPaySucess.aspx.cs", "RazorPay Log Save in Database", "getID", ex.Message, ex.StackTrace);
            
            }

            if (!string.IsNullOrEmpty(pgResponse) && !string.IsNullOrEmpty(Request.Form["razorpay_order_id"]) && string.IsNullOrEmpty(Convert.ToString(Session["UID"])))
            {

             


            }

            if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "")
            {
                if (string.IsNullOrEmpty(pgResponse))
                {
                    foreach (string s in Request.Params.Keys)
                    {
                        //Response.Write(s.ToString() + ":" + Request.Params[s] + "<br>");
                        string PgResData = s.ToString() + ":" + Request.Params[s] + "~~";
                        PgRequestData = PgRequestData + PgResData;
                    }
                    int insert = obPg.InsertExceptionLog("RazorPay", "RazorPaySucess.aspx.cs", "PT Response is null or empty", Convert.ToString(Session["UID"]), Request.Form["CHECKSUMHASH"], PgRequestData);
                }
                if (!string.IsNullOrEmpty(pgResponse))
                {

                    string msg = "no~"+orderID+"";
                    #region Check PayU response hash key
                    try
                    {          
                        if (!string.IsNullOrEmpty(pgResponse))
                        {
                            DataTable dt = new DataTable();
                            dt = GetPgCredential();
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    string MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);
                                    string MERCHANT_PSWD = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                                    string paymentId = Request.Form["razorpay_payment_id"];
                                    string key = MERCHANT_KEY;//"<Enter your Api Key here>";
                                    string secret = MERCHANT_PSWD;// "<Enter your Api Secret here>";

                                    RazorpayClient client = new RazorpayClient(key, secret);

                                    Dictionary<string, string> attributes = new Dictionary<string, string>();

                                    attributes.Add("razorpay_payment_id", paymentId);
                                    attributes.Add("razorpay_order_id", Request.Form["razorpay_order_id"]);
                                    attributes.Add("razorpay_signature", Request.Form["razorpay_signature"]);
                                    bool sign = false;
                                    verifyPaymentSignature(attributes, ref sign);



                                   // bool isValidChecksum = false;//CheckSum.verifyCheckSum(merchantKey, paytmParams, paytmChecksum);

                                    if (sign == true)
                                    {


                                        PayUStatus = "success";

                                         msg = UpdatePaymentResponseDetails(Convert.ToString(Session["UID"]), pgResponse, paymentId, orderID);
                                       // string msg = "yes~" + dsorder.Tables[0].Rows[0][0].ToString();
                                        //Response.Write("Checksum Matched");
                                    }
                                    else
                                    {
                                        PayUStatus = "false";
                                        //Response.Write("Checksum MisMatch");
                                    }

                                    #endregion 
                                }
                                else
                                {
                                    PayUStatus = "False";
                                }
                            }
                            else
                            {
                                PayUStatus = "False";
                            }

                        }
                        else
                        {
                            PayUStatus = "False";
                            int insert = obPg.InsertExceptionLog("RazorPay", "RazorPay.aspx.cs", "Check RAZOR Payment Status", "Payment Status- " + Request.Form["status"], " No Error- Payment Status- " + Request.Form["status"], "Payment Status- " + Request.Form["status"]);
                            // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));
                        }
                    }
                    catch (Exception ex)
                    {
                        int insert = obPg.InsertExceptionLog("RazorPay", "RazorPay.aspx.cs", "Check hash value", "RAZOR hash value decript-incript", ex.Message, ex.StackTrace);
                    }
                   

                    if (msg.Split('~')[0] == "yes")
                    {
                        ObTid = msg.Split('~')[1];
                        if (!string.IsNullOrEmpty(ObTid.Trim()))
                        {
                            #region Get Value After payment details
                            DataSet ds = obPg.GetPaymentDetails(ObTid, Convert.ToString(Session["UID"]));
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    ObTid = Convert.ToString(ds.Tables[0].Rows[0]["TrackId"]);
                                    IbTid = Convert.ToString(ds.Tables[0].Rows[0]["IBTrackId"]);
                                    PaymentStatus = (Convert.ToString(ds.Tables[0].Rows[0]["Status"]));
                                    ServiceType = Convert.ToString(ds.Tables[0].Rows[0]["ServiceType"]);
                                    PgTid = Convert.ToString(ds.Tables[0].Rows[0]["TId"]);
                                    pgMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorText"]);
                                    Trip = Convert.ToString(ds.Tables[0].Rows[0]["Trip"]);
                                    ApiStatus = Convert.ToString(ds.Tables[0].Rows[0]["ApiStatus"]);
                                    UnmappedStatus = Convert.ToString(ds.Tables[0].Rows[0]["UnmappedStatus"]).ToLower();
                                    CreditLimitUpdate = Convert.ToString(ds.Tables[0].Rows[0]["CreditLimitUpdate"]);
                                }
                            }
                            #endregion
                            //if (PaymentStatus == "success" && (ApiStatus == "Successful" || ApiStatus == "Shipped"))
                            if (PaymentStatus.ToLower() == "success" && PayUStatus == "success" && ApiStatus.ToLower() == "success")
                            {
                                string ipAddress = null;
                                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
                                {
                                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                                }
                                if (CreditLimitUpdate.ToLower() == "false")
                                {
                                    int flag = obPg.UpdateCreditLimit(Convert.ToString(Session["UID"]), ObTid, ServiceType, ipAddress);
                                }

                                #region after payment status Success than redirect for flight booking
                                if (ServiceType == "Flight")
                                {
                                    // Session("SearchCriteriaUser") = Request.Url                                        
                                    Session["BookIng"] = "FALSE";
                                    Session["IntBookIng"] = "FALSE";

                                    if (string.IsNullOrEmpty(Convert.ToString(Session["search_type"])) && !string.IsNullOrEmpty(Trip))
                                    {
                                        if (Trip.ToUpper() == "DOM")
                                        {
                                            Session["search_type"] = "Flt";
                                        }
                                        else
                                        {
                                            Session["search_type"] = "FltInt";
                                        }
                                    }
                                    int strlenInbound = 0;
                                    if (!String.IsNullOrEmpty(IbTid))
                                    {
                                        strlenInbound = IbTid.Length;
                                        if (strlenInbound > 6)
                                        {
                                            Ft = "InBound";
                                        }
                                    }
                                    else
                                    {
                                        Ft = "OutBound";
                                    }
                                    if (Trip == "INT")
                                    {
                                        Response.Redirect("wait.aspx?tid=" + ObTid + "", false);
                                    }
                                    else
                                    {
                                        if (Ft == "OutBound")
                                        {
                                            //http://localhost:56359/wait.aspx?OBTID=12df2c01OMiWtCut&FT=OutBound //Dom OneWay
                                            Response.Redirect("wait.aspx?OBTID=" + ObTid + "&FT=" + Ft, false);
                                        }
                                        else
                                        {
                                            //http://localhost:56359/FlightDom/PriceDetails.aspx?OBTID=4eba76541mSy5hlb&IBTID=941f8a608rS4lAEW&FT=InBound    //Dom RoundTrip
                                            Response.Redirect("wait.aspx?OBTID=" + ObTid + "&IBTID=" + IbTid + "&FT=" + Ft, false);
                                        }
                                    }
                                }


                                if (ServiceType == "Flight_Hold")
                                {
                                    Session["BookIng"] = "FALSE";
                                    Session["IntBookIng"] = "FALSE";
                                    int strlenInbound = 0;
                                    if (!String.IsNullOrEmpty(IbTid))
                                    {
                                        strlenInbound = IbTid.Length;
                                        if (strlenInbound > 6)
                                        {
                                            Ft = "InBound";
                                        }
                                    }
                                    else
                                    {
                                        Ft = "OutBound";
                                    }
                                    if (Trip == "INT")
                                    {
                                        Response.Redirect("waitPage.aspx?tid=" + ObTid + "", false);
                                    }
                                    else
                                    {
                                        if (Ft == "OutBound")
                                        {
                                            //http://localhost:56359/wait.aspx?OBTID=12df2c01OMiWtCut&FT=OutBound //Dom OneWay
                                            Response.Redirect("waitPage.aspx?OBTID=" + ObTid + "&FT=" + Ft, false);
                                        }
                                        else
                                        {
                                            //http://localhost:56359/FlightDom/PriceDetails.aspx?OBTID=4eba76541mSy5hlb&IBTID=941f8a608rS4lAEW&FT=InBound    //Dom RoundTrip
                                            Response.Redirect("waitPage.aspx?OBTID=" + ObTid + "&IBTID=" + IbTid + "&FT=" + Ft, false);
                                        }
                                    }
                                }

                                #endregion

                                if (ServiceType == "Hotel")
                                {
                                    Response.Redirect("Hotel/HtlBookwait.aspx", false);
                                }

                                if (ServiceType.ToLower() == "bus")
                                {
                                    Response.Redirect("BS/BusBooking.aspx?OBTID=" + ObTid, false);
                                }

                                if (ServiceType == "GroupBooking")
                                {
                                    if (Trip == "I")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment has been done,please provide the pax details!!');window.location ='/GroupSearch/CustomerInfoIntl.aspx?RefRequestID=" + ObTid + "&PG=Y&Payment=PG&Status=PAID';", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment has been done,please provide the pax details!!');window.location ='/GroupSearch/CustomerInfoDom.aspx?RefRequestID=" + ObTid + "&PG=Y&Payment=PG&Status=PAID';", true);
                                    }
                                }
                                if (ServiceType == "WALLET-TOP-UP")
                                {

                                    Response.Redirect("Report/accounts/uploadamount.aspx?OBTID=" + ObTid + "&PaymentStatus=" + PaymentStatus + "&UnmappedStatus=" + UnmappedStatus, false);
                                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount added successfully in your wallet ');window.location ='SprReports/Accounts/uploadamount.aspx?OBTID=" + ObTid + "&PaymentStatus=" + PaymentStatus + "&UnmappedStatus=" + PaymentStatus + "';", true);
                                }
                            }
                            else
                            {
                                //if (ServiceType == "Flight")
                                if (!string.IsNullOrEmpty(UnmappedStatus))
                                {
                                    Response.Redirect("FlightInt/BookingMsg.aspx?msg=" + UnmappedStatus, false);
                                }
                                else if (!string.IsNullOrEmpty(PaymentStatus))
                                {
                                    Response.Redirect("FlightInt/BookingMsg.aspx?msg=" + PaymentStatus, false);
                                }
                                else
                                {
                                    // Redirect Error  Page and Show error messge
                                    Response.Redirect("FlightInt/BookingMsg.aspx?msg=PG", false);
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("FlightInt/BookingMsg.aspx?msg=2", false);
                        }
                        //
                    }
                    else
                    {
                        //Redirect Error page or Show error message
                        Response.Redirect("Login.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("FlightInt/BookingMsg.aspx?msg=Requested", false);
                }
                //end
            }
            else
            {
                foreach (string s in Request.Params.Keys)
                {
                    // Response.Write(s.ToString() + ":" + Request.Params[s] + "<br>");
                    string PgResData = s.ToString() + ":" + Request.Params[s] + ",";
                    PgRequestData = PgRequestData + PgResData;
                }
                int insert = obPg.InsertExceptionLog("RazorPaySucess", "RazorPaySucess.aspx.cs", "Session is null or empty", "UserId not avilable", Request.Form["encResp"], PgRequestData);

                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            int insert = obPg.InsertExceptionLog("RazorPaySucess", "RazorPaySucess.aspx.cs", "UpdatePaymentResponseDetails- Update PG Response", "insert and select", ex.Message, ex.StackTrace);
            Response.Redirect("Login.aspx", false);
        }
    }

    public DataSet GetPaymentDetails(string TrackId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@TrackId", TrackId);
            adp.SelectCommand.Parameters.AddWithValue("@AgentId", TrackId);
            adp.SelectCommand.Parameters.AddWithValue("@Action", "GetDetails");
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
        return ds;
    }

    public void PGResponsParse(string RefernceNo)
    {
        try
        {
            #region Get details and Login

            if (!string.IsNullOrEmpty(pgResponse) && !string.IsNullOrEmpty(RefernceNo))
            {
                DataSet ds = GetUserIdAndPassword(RefernceNo);
                if (ds != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PWD"])))
                {
                    Login(Convert.ToString(ds.Tables[0].Rows[0]["User_Id"]), Convert.ToString(ds.Tables[0].Rows[0]["PWD"]));
                }
                else
                {
                    int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "PGResponsParse", "UserIdPassword dataset is null or empty", Convert.ToString(Session["UID"]), RefernceNo, pgResponse);
                }

            }
            else
            {
                string PgRequestData = string.Empty;
                if (string.IsNullOrEmpty(pgResponse))
                {
                    foreach (string s in Request.Params.Keys)
                    {
                        //Response.Write(s.ToString() + ":" + Request.Params[s] + "<br>");
                        string PgResData = s.ToString() + ":" + Request.Params[s] + "~~";
                        PgRequestData = PgRequestData + PgResData;
                    }
                    int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "PGResponsParse", "PG Response is null or empty", Convert.ToString(Session["UID"]), Request.Form.ToString(), PgRequestData);
                }
                else
                {
                    int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "PGResponsParse", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), RefernceNo + "~" + pgResponse, PgRequestData);
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "PGResponsParse", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), RefernceNo + "~" + pgResponse + "~" + ex.Message, ex.StackTrace);
        }


    }

    protected void Login(string userid, string pwd)
    {
        DataSet dset = new DataSet();
        try
        {
            //userid = UserLogin.UserName;
            //pwd = UserLogin.Password;
            dset = this.user_auth(userid, pwd);
            if ((dset.Tables[0].Rows[0][0].ToString() == "Not a Valid ID"))
            {
                //Response.Redirect("~/Login.aspx?reason=Your UserID Seems to be Incorrect");
            }
            else if ((dset.Tables[0].Rows[0][0].ToString() == "incorrect password"))
            {
                // Response.Redirect("~/Login.aspx?reason=Your Password Seems to be Incorrect");
            }
            else
            {
                if (dset != null && dset.Tables[0].Rows.Count > 0)
                {
                    string id = dset.Tables[0].Rows[0]["UID"].ToString();
                    string usertype = dset.Tables[0].Rows[0]["UserType"].ToString();
                    string typeid = dset.Tables[0].Rows[0]["TypeID"].ToString();
                    string User = dset.Tables[0].Rows[0]["Name"].ToString();
                    string AgencyName = "";
                    AgencyName = dset.Tables[0].Rows[0]["AgencyName"].ToString();
                    Session["AgencyId"] = dset.Tables[0].Rows[0]["AgencyId"].ToString();
                    Session["OTP"] = "";
                    Session["OTPCreatedBy"] = "";
                    Session["LoginByOTP"] = "";
                    Session["OTPID"] = "";


                    Session["firstNameITZ"] = userid;
                    Session["AgencyName"] = AgencyName;
                    Session["UID"] = id;
                    Session["UserType"] = usertype;
                    Session["TypeID"] = typeid;
                    // '"TA1"
                    Session["IsCorp"] = false;
                    Session["AGTY"] = dset.Tables[0].Rows[0]["Agent_Type"];
                    // '"TYPE1"
                    Session["agent_type"] = dset.Tables[0].Rows[0]["Agent_Type"];
                    // '"TYPE1"
                    Session["User_Type"] = User;
                    if (((User == "AGENT") && (typeid == "TA1" || typeid == "TA2")))
                    {
                        Session["IsCorp"] = false;//Convert.ToBoolean(dset.Tables[0].Rows[0]["IsCorp"]);

                    }
                    //
                }
                else
                {
                    int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "void Login", "login failure,dataset null or empty", Convert.ToString(Session["UID"]), userid, "~login failure,dataset null or empty~" + pwd);
                }

            }

        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            int insert = obPg.InsertExceptionLog("RazorPaySucess.aspx.cs", "void Login", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), ex.Message, ex.StackTrace.ToString());
        }

    }

    public DataSet user_auth(string uid, string passwd)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            adap = new SqlDataAdapter("UserLoginNew", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@uid", uid);
            adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd);
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

    public DataSet GetOrderID(string ReferenceNo)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            adap = new SqlDataAdapter("Sp_RazorID", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@Tid", ReferenceNo);
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

    public string UpdatePaymentResponseDetails(string AgentId, string PgResponse,string PaymentID,string OrderID)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        int temp = 0;
        string OrderId = string.Empty;
        string PaymentId = string.Empty;
        string Status = string.Empty;
         string MID = string.Empty;
        string CURRENCY = string.Empty;
        string MERC_UNQ_REF = string.Empty;
        string BankRefNo = string.Empty;
        string ErrorText = string.Empty;
        string ResponseCode = string.Empty;
        string ResponseMsg = string.Empty;
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
        string Refundamount = "0.0";
        //string ResponseMessage = "";            
        //string workingKey = "";           
        //string OfferType = "";
        //string OfferCode = "";            
        //string pgResponse = string.Empty;
        //string apiStatus = string.Empty;           
        //Status result = new Status();

        try
        {
            #region Cross check of payment status

            try
            {

                DataTable dt = new DataTable();
                dt = GetPgCredential();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       // string Url = Convert.ToString(dt.Rows[0]["WebServiceUrl"]);//"https://test.payu.in/merchant/postservice.php?form=2";
                       // string method = "verify_payment";
                        string MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);//"gtKFFx";
                        string MERCHANT_PSWD = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                       

                        RazorpayClient client = new RazorpayClient(MERCHANT_KEY, MERCHANT_PSWD);
                        Razorpay.Api.Payment payment = client.Payment.Fetch(PaymentID);
                        string response = JsonConvert.SerializeObject(payment);

                         ApiResponse = response;
                         PaymentId = PaymentID;
                         BankRefNo = /// objESRootObject.BANKTXNID;
                         OrderId = OrderID;
                         Status = payment["captured"].ToString();
                         CardType = payment["card_id"].ToString();
                         ResponseCode = "";
                         ResponseMsg = payment["card_id"].ToString();
                         IssuingBank = payment["bank"].ToString();
                         PaymentMode = payment["wallet"].ToString();
                         try
                         {
                             MerAamount = payment["amount"].ToString();
                             MerAamount = MerAamount.Substring(0, MerAamount.Length - 2);
                         }
                         catch(Exception ex)
                         {
                             MerAamount = payment["amount"].ToString();
                         }
                        
          
                             
                     

                        if (Status.ToLower() == "true")
                        {
                            UnmappedStatus = "captured";
                            ApiStatus = "success";
                            Status = "success";
                        }
                        else
                        {

                            if (Status.ToLower() == "true")
                            {
                                UnmappedStatus = "Uncaptured";
                                ApiStatus = "failure";
                                Status = "failure";
                            }
                        }
                      

                    }
                }
            }
            catch (Exception expg)
            {
                int insert = InsertExceptionLog("PaytmWallet", "PaytmSucess", "GetResult", "GetApiRequest", expg.Message, expg.StackTrace);
            }
            #endregion

            #region Update PG Response
            con.Open();
            SqlCommand cmd = new SqlCommand("SpInsertPaymentDetails", con);
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
            cmd.Parameters.AddWithValue("@DiscountValue", Convert.ToDouble(MerAamount));
            cmd.Parameters.AddWithValue("@MerAamount", (Convert.ToDouble(PgAmount) - Convert.ToDouble(MerAamount)));
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
            int insert = InsertExceptionLog("RazorPaySucess", "RazorPaySucess", "UpdatePaymentResponseDetails- Update PG Response", "insert", ex.Message, ex.StackTrace);
            //ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightBooking");
        }
        finally
        {
            con.Close();
       
        }
        if (temp > 0)
        {
            msg = "yes~" + OrderId;
        }
        return msg;
    }


  
    Boolean CheckSumverifyCheckSum(string MerchantKey, Dictionary<String, String> parameters, string check)
    {

        MerchantKey = "QWtcDkr&U@X7Pxla";

        //parameters = new Dictionary<string, string>();


        //check = "";


        foreach (string key in Request.Form.Keys)
        {


            parameters.Add(key.Trim(), Request.Form[key].Trim());
        }


        if (parameters.ContainsKey("CHECKSUMHASH"))
        {

            check = parameters["CHECKSUMHASH"];
            parameters.Remove("CHECKSUMHASH");
        }

        if (CheckSum.verifyCheckSum(MerchantKey, parameters, check))
        {

            Response.Write("Checksum Matched");


            return true;


        }

        else
        {

            Response.Write("Checksum MisMatch");

            return false;
        }
    }
    public class TransectionResponse
    {

        public string TXNID { get; set; }
        public string BANKTXNID { get; set; }
        public string ORDERID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string STATUS { get; set; }
        public string TXNTYPE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string BANKNAME { get; set; }
        public string MID { get; set; }
        public string PAYMENTMODE { get; set; }
        public string REFUNDAMT { get; set; }
        public string TXNDATE { get; set; }
        public string MERC_UNQ_REF { get; set; }
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
            int insert = InsertExceptionLog("Razor", "Razor", "GetPgCredential", "select", ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();

        }
        return dt;
    }



    public string getResult(string ApiRequest, string MerchantID, string MERCHANT_KEY, string OrderId, string Url, Dictionary<String, String> paytmParams)
    {
        string responseData = "";
        //string transactionURL = "https://securegw-stage.paytm.in/merchant-status/getTxnStatus";
        string transactionURL = Url;
        string merchantKey = MERCHANT_KEY;
        string merchantMid = MerchantID;
        try{
        //Dictionary<String, String> paytmParams = new Dictionary<String, String>();
        //paytmParams.Add("MID", merchantMid);
        //paytmParams.Add("ORDERID", OrderId);

        //try
        //{
        //    string paytmChecksum = paytm.CheckSum.generateCheckSum(merchantKey, paytmParams);

        //    paytmParams.Add("CHECKSUMHASH", paytmChecksum);
            string postData = "JsonData=" + new JavaScriptSerializer().Serialize(paytmParams);
            HttpWebRequest connection = (HttpWebRequest)WebRequest.Create(transactionURL);

            connection.Headers.Add("ContentType", "application/json");
            connection.Method = "POST";

            using (StreamWriter requestWriter = new StreamWriter(connection.GetRequestStream()))
            {
                requestWriter.Write(postData);


            }


            responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(connection.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();


            }
        }

        catch (Exception ex)
        {
            Response.Write("Exception message:" + ex.Message.ToString());

        }
        return responseData;
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
            int insert = InsertExceptionLog("PaytmWallet", "PaytmSucess", "GetTotalAmountWithPgCharge", "SELECT", ex.Message, ex.StackTrace);
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

    public static void verifyPaymentSignature(Dictionary<string, string> attributes, ref bool sign)
    {
        string expectedSignature = attributes["razorpay_signature"];
        string orderId = attributes["razorpay_order_id"];
        string paymentId = attributes["razorpay_payment_id"];

        string payload = string.Format("{0}|{1}", orderId, paymentId);

        string secret = RazorpayClient.Secret;

        verifySignature(payload, expectedSignature, secret, ref sign);
    }

    public static void verifyWebhookSignature(string payload, string expectedSignature, string secret, ref bool sign)
    {
        verifySignature(payload, expectedSignature, secret, ref sign);
    }

    public static long ToUnixTimestamp(DateTime inputTime)
    {
        DateTime unixReferenceTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = inputTime - unixReferenceTime;
        return (long)diff.TotalSeconds;
    }

    private static void verifySignature(string payload, string expectedSignature, string secret, ref bool sign)
    {
        string actualSignature = getActualSignature(payload, secret);

        bool verified = actualSignature.Equals(expectedSignature);
        sign = verified;
        if (verified == false)
        {
           // throw new SignatureVerificationError("Invalid signature passed");
        }
    }

    private static string getActualSignature(string payload, string secret)
    {
        byte[] secretBytes = StringEncode(secret);

        HMACSHA256 hashHmac = new HMACSHA256(secretBytes);

        var bytes = StringEncode(payload);

        return HashEncode(hashHmac.ComputeHash(bytes));
    }

    private static byte[] StringEncode(string text)
    {
        var encoding = new ASCIIEncoding();
        return encoding.GetBytes(text);
    }

    private static string HashEncode(byte[] hash)
    {
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }


}