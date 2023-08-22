using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;

public partial class OTPValidate : System.Web.UI.Page
{
    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    //private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        string OTPCreatedBy = "";
        string StrAgencyId = Request.QueryString["Param"];
        string OTPId = Request.QueryString["ProductID"];

        if (!string.IsNullOrEmpty(Request.QueryString["ExecID"].ToString()))
        {
            OTPCreatedBy = Request.QueryString["ExecID"];
        }

     
            BtnSubmit.Visible = false;
            SqlTransaction ST = new SqlTransaction();
            DataSet ds =new DataSet(); 
            if (!string.IsNullOrEmpty(StrAgencyId) && !string.IsNullOrEmpty(OTPId) && !string.IsNullOrEmpty(OTPCreatedBy))
            {
                try
                {
                    if (!IsPostBack)
                    {
                        Session["OTP"] = null;
                        hdnAgencyId.Value = "";
                        hdnOTPID.Value ="";
                        hdnCreatedBy.Value ="";  

                        string sUserID = "";
                        string sAgencyId = "";
                        string sOTP = "";                        
                        string OTPStatus = "";
                        

                        string oUserID = "";                       
                        string oOTP = "";
                        string oOTPID = "";                        
                        string oCtreatedBy = "";
                        string SrcCode = "";//(RandomNo.Substring(2, 18)+ OTP.Substring(4, 2)).Substring(18, 2)
                        string sActionType = "GET";
                        ds = GetRecord("", StrAgencyId, OTPId, OTPCreatedBy, sActionType);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if(ds.Tables[0].Rows.Count > 0)
                            {
                                sUserID = Convert.ToString(ds.Tables[0].Rows[0]["USER_ID"]);
                                sAgencyId = Convert.ToString(ds.Tables[0].Rows[0]["AgencyId"]);
                                sOTP = Convert.ToString(ds.Tables[0].Rows[0]["OTP"]);

                                OTPStatus = Convert.ToString(ds.Tables[0].Rows[0]["OTPStatus"]);                                
                                if(sOTP.Length>5)
                                {
                                    SrcCode=sOTP.Substring(4, 2);
                                }
                                Session["OTP"] = sOTP;
                            }
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                //OTPId,UserId,AgencyId,OTP,CreatedBy
                                oUserID = Convert.ToString(ds.Tables[1].Rows[0]["UserId"]);
                                oOTP = Convert.ToString(ds.Tables[1].Rows[0]["OTP"]);
                                oOTPID = Convert.ToString(ds.Tables[1].Rows[0]["OTPId"]);
                                oCtreatedBy = Convert.ToString(ds.Tables[1].Rows[0]["CreatedBy"]);
                            }
                            if (OTPId.Substring(18, 2) == SrcCode && sOTP == oOTP && sAgencyId.ToUpper() == StrAgencyId.ToUpper() && oOTPID == OTPId)
                            {
                                BtnSubmit.Visible = true; 
                                hdnAgencyId.Value=sAgencyId;
                                hdnOTPID.Value=oOTPID;
                                hdnCreatedBy.Value = oCtreatedBy;
                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Status"])) && Convert.ToBoolean(ds.Tables[1].Rows[0]["Status"]) == false)
                                {                                   
                                    sUserID = Convert.ToString(ds.Tables[0].Rows[0]["USER_ID"]);
                                    sAgencyId = Convert.ToString(ds.Tables[0].Rows[0]["AgencyId"]);
                                    sOTP = Convert.ToString(ds.Tables[0].Rows[0]["OTP"]);
                                    string password = Convert.ToString(ds.Tables[0].Rows[0]["PWD"]);
                                    string TimeDuration = Convert.ToString(ds.Tables[0].Rows[0]["OTPStatus"]);
                                    if (sOTP.Length > 5)
                                    {
                                        SrcCode = sOTP.Substring(4, 2);
                                    }
                                    TxtOTP.Text = sOTP;
                                    if (TxtOTP.Text == sOTP && SrcCode == hdnOTPID.Value.Substring(18, 2))
                                    {
                                        BtnSubmit.Visible = true;
                                        TxtOTP.Text = sOTP;
                                        sActionType = "VALIDATE";
                                        DataSet AGENTDS = GetRecord("", hdnAgencyId.Value, hdnOTPID.Value, hdnCreatedBy.Value, sActionType);
                                        Login(sUserID, password, sOTP);
                                    }
                                }


                            }
                            else
                            {
                                Session["OTP"] = null;
                                BtnSubmit.Visible = false;
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                                Response.Redirect("~/Login.aspx?reason=Please try again");                               
                            }                          
                        }
                        else
                        {
                           // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                            Response.Redirect("~/Login.aspx?reason=Please try again");
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogInfo(ex);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                    EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-Page_Load");
                    Response.Redirect("~/Login.aspx?reason=Please try again");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }      
       

    }
    public DataSet GetRecord(string UserId, string AgencyId, string OTPId, string CreatedBy, string ActionType)
    {
        DataSet ds = new DataSet();
        try
        {            
            if (con.State == ConnectionState.Closed)
                con.Open();
            adap = new SqlDataAdapter("SP_INSERT_OTP", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            adap.SelectCommand.Parameters.AddWithValue("@AgencyId", AgencyId);
            adap.SelectCommand.Parameters.AddWithValue("@OTPId", OTPId);
            adap.SelectCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            adap.SelectCommand.Parameters.AddWithValue("@ActionType", ActionType);
            adap.SelectCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 30);
            adap.SelectCommand.Parameters["@Msg"].Direction = ParameterDirection.Output;
            adap.Fill(ds);
            adap.Dispose();
            con.Close();
            
        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-DataSet GetRecord");
            adap.Dispose();
            con.Close();
        }        
        return ds;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try{
        if (!string.IsNullOrEmpty(TxtOTP.Text))
        {
            if (TxtOTP.Text.Length == 6 && !string.IsNullOrEmpty(hdnAgencyId.Value) && !string.IsNullOrEmpty(hdnCreatedBy.Value) && !string.IsNullOrEmpty(hdnOTPID.Value) && Convert.ToString(Session["OTP"]) == TxtOTP.Text)
            {
                //Login()
                string sUserID = "";
                string sAgencyId = "";
                string sOTP = "";
                string OTPStatus = "";               
                string SrcCode = "";//(RandomNo.Substring(2, 18)+ OTP.Substring(4, 2)).Substring(18, 2)
                string sActionType = "VALIDATE";
                string TimeDuration = "";
                DataSet ds = GetRecord("", hdnAgencyId.Value, hdnOTPID.Value, hdnCreatedBy.Value, sActionType);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sUserID = Convert.ToString(ds.Tables[0].Rows[0]["USER_ID"]);
                        sAgencyId = Convert.ToString(ds.Tables[0].Rows[0]["AgencyId"]);
                        sOTP = Convert.ToString(ds.Tables[0].Rows[0]["OTP"]);
                        string password = Convert.ToString(ds.Tables[0].Rows[0]["PWD"]);
                        OTPStatus = Convert.ToString(ds.Tables[0].Rows[0]["OTPStatus"]);
                        TimeDuration = Convert.ToString(ds.Tables[0].Rows[0]["OTPStatus"]);
                        if (sOTP.Length > 5)
                        {
                            SrcCode = sOTP.Substring(4, 2);
                        }
                        if (TxtOTP.Text == sOTP && SrcCode==hdnOTPID.Value.Substring(18, 2))
                        {
                            Login(sUserID, password, sOTP);                            
                        }
                        else
                        {
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                            Response.Redirect("~/Login.aspx?reason=Try again with new OTP.");
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                        Response.Redirect("~/Login.aspx?reason=OTP expired,Try again with new OTP.");
                    }
                }
                else
                {
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please try again!!');", true);
                    Response.Redirect("~/Login.aspx?reason=OTP expired,Try again with new OTP.");
                }
            }
            else
            {
                BtnSubmit.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Enter vailid  OTP!!');", true);
                return;
            }
        }
        else
        {
            BtnSubmit.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Enter  OTP!!');", true);
            return;

        }
        //CAtch
        }
        catch (Exception ex)
        {
            EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-BtnSubmit_Click");
        }



    }

    protected void Login(string userid, string pwd,string strOTP)
    {
        DataSet dset = new DataSet();
        try
        {
            //userid = UserLogin.UserName;
            //pwd = UserLogin.Password;
            dset = this.user_auth(userid, pwd);
            if ((dset.Tables[0].Rows[0][0].ToString() == "Not a Valid ID"))
            {               
                Response.Redirect("~/Login.aspx?reason=Your UserID Seems to be Incorrect");
            }
            else if ((dset.Tables[0].Rows[0][0].ToString() == "incorrect password"))
            {
                Response.Redirect("~/Login.aspx?reason=Your Password Seems to be Incorrect");
            }
            else
            {

             string id = dset.Tables[0].Rows[0]["UID"].ToString();
                string usertype = dset.Tables[0].Rows[0]["UserType"].ToString();
                string typeid = dset.Tables[0].Rows[0]["TypeID"].ToString();
                string User = dset.Tables[0].Rows[0]["Name"].ToString();
                string AgencyName="";
                if ((usertype == "TA"))
                {
                    AgencyName = dset.Tables[0].Rows[0]["AgencyName"].ToString();
                    Session["AgencyId"] = dset.Tables[0].Rows[0]["AgencyId"].ToString();
                    //  Session["User_Type") = "AGENT"
                }


                try
                {
                    DataSet lastLogin = new DataSet();
                    lastLogin = LastLoginTime(id);
                    Session["LastloginTime"] = lastLogin.Tables[0].Rows[0]["LoginTime"].ToString();
                 
                    string ipaddress;
                    ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                       ipaddress = Request.ServerVariables["REMOTE_ADDR"];



                    InsertLoginTime(id, ipaddress);
                }
                catch (Exception ex)
                {
                }



                Session["OTP"] = strOTP;
                Session["OTPCreatedBy"] = hdnCreatedBy.Value;
                Session["LoginByOTP"] = "true";
                Session["OTPID"] = hdnOTPID.Value;


                Session["firstNameITZ"] = userid;
                Session["AgencyName"] = AgencyName;
                Session["UID"] = id;
                // 'dset.Tables(0).Rows(0)["UID"].ToString()
                Session["UserType"] = usertype;
                // ' "TA"
                Session["TypeID"] = typeid;
                // '"TA1"
                Session["IsCorp"] = false;
                Session["AGTY"] = dset.Tables[0].Rows[0]["Agent_Type"];
                // '"TYPE1"
                Session["agent_type"] = dset.Tables[0].Rows[0]["Agent_Type"];
                // '"TYPE1"
                Session["User_Type"] = User;
                FormsAuthentication.RedirectFromLoginPage(userid, false);
                if ((User == "ACCjjjjjjj"))
                {
                    Session["UID"] = dset.Tables[0].Rows[0]["UID"].ToString();
                    Session["UserType"] = "AC";
                    Response.Redirect("Report/Accounts/Ledger.aspx");
                }
                else if ((User == "ADMINiiiiiii"))
                {
                    Session["ADMINLogin"] = userid;
                    Session["UID"] = dset.Tables[0].Rows[0]["UID"].ToString();
                    Session["UserType"] = "AD";
                    Response.Redirect("Search.aspx");
                }
                else if ((User == "EXEC9999999"))
                {
                    Session["User_Type"] = "EXEC";
                    Session["TripExec"] = dset.Tables[0].Rows[0]["Trip"].ToString();
                    Session["UID"] = dset.Tables[0].Rows[0]["UID"].ToString();
                    Session["UserType"] = "EC";
                    Response.Redirect("Report/admin/profile.aspx", false);
                }
                else if (((User == "AGENT") && (typeid == "TA1")))
                {
                    //if (((dset.Tables[0].Rows[0]["IsCorp"].ToString() != "") && dset.Tables[0].Rows[0]["IsCorp"].ToString()))
                    //{                       
                    //    Session["IsCorp"] = Convert.ToBoolean(dset.Tables[0].Rows[0]["IsCorp"]);
                    //}

                    Session["IsCorp"] = false;//Convert.ToBoolean(dset.Tables[0].Rows[0]["IsCorp"]);
                    Response.Redirect("Search.aspx", false);
                }
                else if (((User == "AGENT") && (typeid == "TA2")))
                {
                    //if (((dset.Tables[0].Rows[0]["IsCorp"].ToString() != "")  && dset.Tables[0].Rows[0]["IsCorp"].ToString()))
                    //{
                    //    IsNot;
                    //    null;
                    //    
                    //Session["IsCorp"] = Convert.ToBoolean(dset.Tables[0].Rows[0]["IsCorp"].ToString());
                    //}
                    Session["IsCorp"] = false;
                    Response.Redirect("Report/Accounts/Ledger.aspx", false);
                }
                else if ((User == "SALES--------"))
                {
                    Response.Redirect("Search.aspx");
                }
                else if ((usertype == "DI"))
                {
                    Response.Redirect("Report/Accounts/Ledger.aspx", false);
                    // END CHANGES FOR DISTR
                }

            }

        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-Login(string userid, string pwd)");
        }

    }

    public DataSet user_auth(string uid, string passwd)
    {
        DataSet ds = new DataSet();
        try
        {
            adap = new SqlDataAdapter("UserLoginNew", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@uid", uid);
            adap.SelectCommand.Parameters.AddWithValue("@pwd", passwd);
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            EXCEPTION_LOG.ErrorLog.FileHandling("EmulateAgent", "Error_102", ex, "OTPValidate.aspx.cs-DataSet user_auth");
        }

        return ds;
    }

    public int InsertLoginTime(string UID, string ipaddress)
    {
        DataAccess objDataAcess = new DataAccess(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        Hashtable paramHashtable = new Hashtable();
        paramHashtable.Clear();
        paramHashtable.Add("@userID", UID);
        paramHashtable.Add("@IPAdress", ipaddress);
        return objDataAcess.ExecuteData<int>(paramHashtable, true, "Sp_Tbl_UserLoginTime_Insert", 1);
    }
    public DataSet LastLoginTime(string uid)
    {
        DataSet ds = new DataSet();
        try
        {
            adap = new SqlDataAdapter("Sp_Tbl_UserLoginTime", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@userID", uid);
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

        return ds;
    }


}