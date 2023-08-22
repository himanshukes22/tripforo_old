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


public partial class Report_Accounts_PayConfirmation : System.Web.UI.Page
{
    PG.PaymentGateway obPg = new PG.PaymentGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //TxtAmout.Attributes.Add("autocomplete", "off");
            if (Session["UID"] == null && Convert.ToString(Session["UID"]) == "")
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("../Login.aspx?reason=Session TimeOut", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ObTid"]) && !string.IsNullOrEmpty(Request.QueryString["PaymentStatus"]) && !string.IsNullOrEmpty(Request.QueryString["UnmappedStatus"]))
                    {
                        string orderID = Convert.ToString(Request.QueryString["ObTid"]);
                        string PaymentStatus = Convert.ToString(Request.QueryString["PaymentStatus"]);
                        string UnmappedStatus = Convert.ToString(Request.QueryString["UnmappedStatus"]);
                        string pgMessage = "";
                        string PgTid = "";
                        string ApiStatus = "";
                        // string PgTid = "";
                        lblOrderNumber.Text = Request.QueryString["ObTid"];
                        lblStatus.Text = "Failed";
                        #region Get Value After payment details
                        DataSet ds = obPg.GetPaymentDetails(orderID, Convert.ToString(Session["UID"]));
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                lblOrderNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["TrackId"]);
                                lblAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                                PaymentStatus = (Convert.ToString(ds.Tables[0].Rows[0]["Status"])).ToLower();
                                PgTid = Convert.ToString(ds.Tables[0].Rows[0]["TId"]);
                                pgMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorText"]);
                                ApiStatus = Convert.ToString(ds.Tables[0].Rows[0]["ApiStatus"]).ToLower();
                                UnmappedStatus = Convert.ToString(ds.Tables[0].Rows[0]["UnmappedStatus"]).ToLower();

                                if (PaymentStatus == "success" && UnmappedStatus == "captured" && ApiStatus == "success")
                                {
                                    //lblAmount.Text = ApiStatus;
                                    lblStatus.Text = ApiStatus;
                                }
                                else if (PaymentStatus != "success" && ApiStatus != "success")
                                {
                                    //lblAmount.Text = ApiStatus;
                                    lblStatus.Text = PaymentStatus;
                                }
                                else
                                {
                                    lblStatus.Text = "Pending";
                                }
                            }
                        }
                        else
                        {
                            lblOrderNumber.Text = Request.QueryString["ObTid"];
                            lblStatus.Text = "Failed";
                            //lblAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                        }
                        #endregion

                        //if (PaymentStatus == "success" && UnmappedStatus == "captured")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount added successfully in your wallet ');window.location ='uploadamount.aspx';", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('In case amount is deducted from your bank a/c and amount not added in your wallet, Please contact to customer care. PG status- " + UnmappedStatus + "');window.location ='uploadamount.aspx';", true);
                        //}

                    }
                }
                else
                {
                    lblOrderNumber.Text = "N.A";
                    lblStatus.Text = "N.A";
                    lblAmount.Text = "N.A";
                }
            }
        }
        catch (Exception ex)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("../Login.aspx?reason=Session TimeOut", false);
        }
    }
}