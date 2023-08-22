using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using PayWithFC;


public partial class FreechargeSucess : System.Web.UI.Page
{
    //PG.PaymentGateway obPg = new PG.PaymentGateway();
    FCTrans obPg = new FCTrans();
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
       

        string pgMessage = "";
        string Trip = "";
        try
        {
            //pgResponse = "txnId=OAWaTMk3evwR7q_FC0314130900800201_1&merchantTxnId=FC0314130900800201&amount=1&metadata=&status=COMPLETED&authCode=EJHoR51ATszcA5FiaqWt_PjMaXKh4Mg54mrdG20lN3VYQ6iWmlhb_yGLoQvoLVtioo32MS6CzTZl1cZEvRrpf2YHNFKcJh22wTjsNaR26FgqFdr1hSlelSlPSU77kOLC&checksum=335e7f59ae875f65f2fb1ec945f722701192905e8bca9fed1c3a199defdb7098";
            if (!string.IsNullOrEmpty(pgResponse) && !string.IsNullOrEmpty(Request.Form["merchantTxnId"]) && string.IsNullOrEmpty(Convert.ToString(Session["UID"])))
                {
                    PGResponsParse(Request.Form["merchantTxnId"]);
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
                    int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "PG Response is null or empty", Convert.ToString(Session["UID"]), Request.Form["encResp"], PgRequestData);
                }
                if (!string.IsNullOrEmpty(pgResponse))
                {
                    string msg = obPg.UpdatePaymentResponseDetails(Convert.ToString(Session["UID"]), pgResponse);


                    #region Check PayU response hash key
                    try
                    {
                        string[] merc_hash_vars_seq;
                        string merc_hash_string = string.Empty;
                        string merc_hash = string.Empty;
                        string order_id = string.Empty;
                        string SALT = string.Empty;
                        //string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                        string hash_seq = string.Empty;
                        if (Request.Form["status"] == "COMPLETED")//"success")
                        {

                            DataTable dt = new DataTable();
                            dt = obPg.GetPgCredential();
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    SALT = Convert.ToString(dt.Rows[0]["MERCHANT_PSWD"]);
                                    //MERCHANT_KEY = Convert.ToString(dt.Rows[0]["MERCHANT_KEY"]);                       
                                    hash_seq = Convert.ToString(dt.Rows[0]["HashSequence"]);

                                    merc_hash_vars_seq = hash_seq.Split('|');
                                    Array.Reverse(merc_hash_vars_seq);
                                    //merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];
                                    merc_hash_string = SALT + "|" + Request.Form["status"];
                                    foreach (string merc_hash_var in merc_hash_vars_seq)
                                    {
                                        merc_hash_string += "|";
                                        merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
                                    }
                                    merc_hash = obPg.Generatehash512(merc_hash_string).ToLower();
                                    if (merc_hash != Request.Form["hash"])
                                    {
                                        PayUStatus = "False";
                                        int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "Check hash value", "Hash value did not matched", "No Error-Hash value did not matched", "Payment Status- " + Request.Form["status"]);
                                        // Response.Write("Hash value did not matched");
                                    }
                                    else
                                    {
                                        order_id = Request.Form["txnid"];
                                        PayUStatus = "success";
                                        //Response.Write("value matched");                              
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
                            }
                            
                        }
                        else
                        {
                            PayUStatus = "False";
                            int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "Check PayU Payment Status", "Payment Status- " + Request.Form["status"], " No Error- Payment Status- " + Request.Form["status"], "Payment Status- " + Request.Form["status"]);
                            // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));
                        }
                    }
                    catch (Exception ex)
                    {
                        int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "Check hash value", "payu hash value decript-incript", ex.Message, ex.StackTrace);
                    }
                    #endregion
                    

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
                                    PaymentStatus = (Convert.ToString(ds.Tables[0].Rows[0]["Status"])).ToLower();
                                    ServiceType = Convert.ToString(ds.Tables[0].Rows[0]["ServiceType"]);
                                    PgTid = Convert.ToString(ds.Tables[0].Rows[0]["TId"]);
                                    pgMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorText"]);
                                    Trip = Convert.ToString(ds.Tables[0].Rows[0]["Trip"]);
                                    ApiStatus = Convert.ToString(ds.Tables[0].Rows[0]["ApiStatus"]).ToLower();
                                    UnmappedStatus = Convert.ToString(ds.Tables[0].Rows[0]["UnmappedStatus"]).ToLower();
                                    CreditLimitUpdate = Convert.ToString(ds.Tables[0].Rows[0]["CreditLimitUpdate"]);
                                }
                            }
                            #endregion
                            //if (PaymentStatus == "success" && (ApiStatus == "Successful" || ApiStatus == "Shipped"))
                            //Request.Form["status"] == "COMPLETED"
                            //if (PaymentStatus == "success" && PayUStatus == "success" && Request.Form["status"] == "success" && UnmappedStatus == "captured" && ApiStatus == "success")
                                if (PaymentStatus == "success" && Convert.ToString(Request.Form["status"]) == "COMPLETED")
                                {
                                string ipAddress = null;
                                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
                                {
                                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                                }
                                if (CreditLimitUpdate.ToLower() =="false")
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
                                        if(Trip.ToUpper()== "DOM")
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
                                            //http://localhost:56359/Domestic/PriceDetails.aspx?OBTID=4eba76541mSy5hlb&IBTID=941f8a608rS4lAEW&FT=InBound    //Dom RoundTrip
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
                                            //http://localhost:56359/Domestic/PriceDetails.aspx?OBTID=4eba76541mSy5hlb&IBTID=941f8a608rS4lAEW&FT=InBound    //Dom RoundTrip
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
                                    if (Trip=="I")
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

                                    Response.Redirect("Report/Accounts/uploadamount.aspx?OBTID=" + ObTid + "&PaymentStatus=" + PaymentStatus + "&UnmappedStatus=" + UnmappedStatus, false);
                                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount added successfully in your wallet ');window.location ='Report/Accounts/uploadamount.aspx?OBTID=" + ObTid + "&PaymentStatus=" + PaymentStatus + "&UnmappedStatus=" + PaymentStatus + "';", true);
                                }
                            }
                            else
                            {
                                //if (ServiceType == "Flight")
                                if (!string.IsNullOrEmpty(UnmappedStatus))
                                {
                                    Response.Redirect("International/BookingMsg.aspx?msg=" + UnmappedStatus, false);
                                }
                                else if (!string.IsNullOrEmpty(PaymentStatus))
                                {
                                    Response.Redirect("International/BookingMsg.aspx?msg=" + PaymentStatus, false);
                                }
                                else
                                {
                                    // Redirect Error  Page and Show error messge
                                    Response.Redirect("International/BookingMsg.aspx?msg=PG", false);
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("International/BookingMsg.aspx?msg=2", false);
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
                    Response.Redirect("International/BookingMsg.aspx?msg=Requested", false);
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
                int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "Session is null or empty", "UserId not avilable", Request.Form["encResp"], PgRequestData);

                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            int insert = obPg.InsertExceptionLog("FreechargeSucess", "FreechargeSucess.aspx.cs", "UpdatePaymentResponseDetails- Update PG Response", "insert and select", ex.Message, ex.StackTrace);
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
                    int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "PGResponsParse", "UserIdPassword dataset is null or empty", Convert.ToString(Session["UID"]), RefernceNo, pgResponse);
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
                    int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "PGResponsParse", "PG Response is null or empty", Convert.ToString(Session["UID"]), Request.Form.ToString(), PgRequestData);
                }
                else
                {
                    int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "PGResponsParse", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), RefernceNo + "~" + pgResponse, PgRequestData);
                }
            }
            #endregion
        }
        catch(Exception ex)
        {
            int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "PGResponsParse", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), RefernceNo + "~" + pgResponse+"~"+ex.Message , ex.StackTrace);
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
                    int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "void Login", "login failure,dataset null or empty", Convert.ToString(Session["UID"]), userid, "~login failure,dataset null or empty~"+pwd);
                }

            }

        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            int insert = obPg.InsertExceptionLog("FreechargeSucess.aspx.cs", "void Login", "PG Response is null or RefernceNo not found", Convert.ToString(Session["UID"]), ex.Message, ex.StackTrace.ToString());
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



}