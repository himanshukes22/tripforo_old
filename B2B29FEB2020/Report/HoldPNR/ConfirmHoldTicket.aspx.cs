using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SprReports_HoldPNR_ConfirmHoldTicket : System.Web.UI.Page
{

    string UserId ="";
    string HoldStatus = "";
    decimal AgentCreditLimit = 0;
    decimal TotalAfterDis = 0;
    string userType = "";
    SqlTransaction objDA = new SqlTransaction();
    SqlTransactionDom objSqlDom = new SqlTransactionDom();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            btnSubmit.Visible = false;
            lblMsg.Visible = true;
            if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "")
            {          
                if (!string.IsNullOrEmpty(Request.QueryString["OrderId"]) && !string.IsNullOrEmpty(Request.QueryString["AgentId"]))
                {
                    string TrackId = Request.QueryString["OrderId"];
                    string UserId = Request.QueryString["AgentId"];

                    if (!IsPostBack)
                    {
                        #region GetRecorde
                        DataSet AgencyDs = objDA.GetAgencyDetails(UserId);
                        DataSet FltHdrDs = objDA.GetHdrDetails(TrackId);
                        //DataSet FltDs = objDA.GetFltDtls(OBTrackId, Session["UID"]);
                        //DataSet PaxDs = objDA.GetPaxDetails(OBTrackId);                    
                        //DataSet FltFareDs = objDA.GetFltFareDtl(OBTrackId);
                        //DataSet ds = GetBookingDetails(TrackId, UserId);
                        
                        if (AgencyDs != null && AgencyDs.Tables[0].Rows.Count > 0)
                        {
                            lblAgentId.Text = Convert.ToString(AgencyDs.Tables[0].Rows[0]["User_Id"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(AgencyDs.Tables[0].Rows[0]["Crd_Limit"])))
                            {
                                AgentCreditLimit = Convert.ToDecimal(AgencyDs.Tables[0].Rows[0]["Crd_Limit"]);
                            }
                            userType = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Status"]);
                        }

                        if (FltHdrDs != null && FltHdrDs.Tables[0].Rows.Count > 0)
                        {
                            lblAmount.Text = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"]);
                            lblAgentId.Text = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]);
                            lblSector.Text = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["sector"]);
                            lblOrderId.Text = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["OrderId"]);
                            lblStatus.Text = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Status"]);
                            HoldStatus = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Status"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"])))
                            {
                                TotalAfterDis = Convert.ToDecimal(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"]);//ConfirmByAgent
                            }
                        }
                        if (userType == "TA" && TotalAfterDis > 0 && AgentCreditLimit > 0 && AgentCreditLimit >= TotalAfterDis && HoldStatus.ToLower() == "confirmbyagent" && Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]) == Convert.ToString(AgencyDs.Tables[0].Rows[0]["User_Id"]))
                        {
                            lblMsg.Text = " Amount " + TotalAfterDis + " wil be debited from your wallet to issue the ticket(s)";
                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            btnSubmit.Visible = false;
                            if (HoldStatus.ToLower() == "confirm")
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "The request has already been submitted and executive updated within 45 minutes ,Please wait or contact to customercare team.";
                            }
                        }
                        #endregion
                    }
                }
                else
                {

                }

                
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            //clsErrorLog.LogInfo(ex);
            EXCEPTION_LOG.ErrorLog.FileHandling("HoldBookingConfirmByAgent", "Error_102", ex, "HoldPNR-ConfirmHoldTicket.aspx.cs-Page_Load");
        }

    }    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
        if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "" && Convert.ToString(Session["UID"]) == lblAgentId.Text)
        {
            #region Check Balance and Deduct
            DataSet AgencyDs = objDA.GetAgencyDetails(lblAgentId.Text);
            DataSet FltHdrDs = objDA.GetHdrDetails(lblOrderId.Text);
            if (Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Status"]).Trim().ToLower() == "confirmbyagent")
            {
                //if (Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agent_Status"]).Trim() != "NOT ACTIVE" && Convert.ToString(AgencyDs.Tables[0].Rows[0]["Online_tkt"]).Trim() != "NOT ACTIVE")
                //{
                    if (Convert.ToDouble(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"]) <= Convert.ToDouble(AgencyDs.Tables[0].Rows[0]["Crd_Limit"]))
                    {
                        int Result = 0;
                        Result = objSqlDom.Ledgerandcreditlimit_Transaction(Convert.ToString(Session["UID"]), Convert.ToDouble(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"]), lblOrderId.Text, Convert.ToString(FltHdrDs.Tables[0].Rows[0]["VC"]), Convert.ToString(FltHdrDs.Tables[0].Rows[0]["GdsPnr"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Request.UserHostAddress.ToString(), "", Convert.ToString(Session["UID"]), "",
                        Convert.ToDouble(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"]), "");
                        // GdsPnr,VC
                        //        int Result = 0;
                        //Result = objSqlDom.Ledgerandcreditlimit_Transaction(Session["UID"], FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"), OBTrackId, vc, GdsPnr, AgencyDs.Tables(0).Rows(0)("Agency_Name"), Request.UserHostAddress.ToString(), ProjectId, BookedBy, BillNoCorp,
                        //Convert.ToDouble(AgencyDs.Tables(0).Rows(0)("Crd_Limit").ToString.Trim()), objIzT.EASY_ORDID_ITZ);
                        if (Result == 1)
                        {
                         int flag= UpdateBookingStatus(lblOrderId.Text);
                            try
                            {
                                    if (Session["LoginByOTP"] != null && !string.IsNullOrEmpty(Convert.ToString(Session["LoginByOTP"])) && Convert.ToString(Session["LoginByOTP"])=="true")
                                    {
                                        int otp = OTPTransactionInsert(Convert.ToString(Session["UID"]), "IssueHoldTicket-HoldByAgent", lblOrderId.Text, Convert.ToString(Session["LoginByOTP"]), Convert.ToString(Session["OTPID"]), "Flight-HoldByAgent");
                                    }
                            }

                            catch(Exception ex)
                            {
                                EXCEPTION_LOG.ErrorLog.FileHandling("HoldBookingConfirmByAgent", "Error_102", ex, "HoldPNR-ConfirmHoldTicket.aspx.cs-OTPTransactionInsert");
                            }

                          if( flag>0)
                          {
                              lblMsg.Visible = true;
                              lblMsg.Text = "Please wait your ticket updated or contact to customercare team.";
                              ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc()", true);
                          }
                          else
                          {
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('try again.');", true);
                          }

                        }
                    }
               // }
            }

            #endregion
        }
        }
        catch (Exception ex)
        {
            EXCEPTION_LOG.ErrorLog.FileHandling("UploadRequest", "Error_102", ex, "HoldPNR-ConfirmHoldTicket.aspx.cs-btn_submitt_Click");
        }
    }

    public int UpdateBookingStatus(string OrderId)
    {
        int temp = 0;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("USP_Update_StatusHold", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@orderid", OrderId);
            cmd.Parameters.AddWithValue("@type", "Confirm");           
            temp = cmd.ExecuteNonQuery();
            con.Close();         
        }
        catch (Exception ex)
        {
            EXCEPTION_LOG.ErrorLog.FileHandling("HoldBookingConfirmByAgent", "Error_102", ex, "HoldPNR-ConfirmHoldTicket.aspx.cs- UpdateBookingStatus");
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        return temp;
    }


    public int OTPTransactionInsert(string UserID, string Remark, string OTPRefNo, string LoginByOTP, string OTPId, string ServiceType)
    {
        int temp = 0;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            cmd = new SqlCommand("SP_Insert_Transaction_OTP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Remark", Remark);
            cmd.Parameters.AddWithValue("@OTPRefNo", OTPRefNo);
            cmd.Parameters.AddWithValue("@LoginByOTP", LoginByOTP);
            cmd.Parameters.AddWithValue("@OTPId", OTPId);
            cmd.Parameters.AddWithValue("@ServiceType", ServiceType);            
            temp = cmd.ExecuteNonQuery();
            con.Close(); 
        }
        catch (Exception ex)
        {
            EXCEPTION_LOG.ErrorLog.FileHandling("HoldBookingConfirmByAgent", "Error_102", ex, "HoldPNR-ConfirmHoldTicket.aspx.cs- OTPTransactionInsert");
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        return temp;
    }
}