using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SprReports_Accounts_UploadAmount : System.Web.UI.Page
{
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
                        if (PaymentStatus == "success" && UnmappedStatus == "captured")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount added successfully in your wallet ');window.location ='uploadamount.aspx';", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('In case amount is deducted from your bank a/c and amount not added in your wallet, Please contact to customer care. PG status- " + UnmappedStatus + "');window.location ='uploadamount.aspx';", true);
                        }

                    }
                }
            }
        }
        catch(Exception ex)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("../Login.aspx?reason=Session TimeOut", false);
        }

    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string PgMsg = string.Empty;
            if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "")
            {
                if (!string.IsNullOrEmpty(TxtAmout.Text))
                {
                    int parsedValue;
                    if (!int.TryParse(TxtAmout.Text, out parsedValue))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Plese enter valid amount''); ", true);
                        return;
                    }
                    else
                    {
                        if ( Convert.ToDouble(TxtAmout.Text) > 0)
                        {
                            #region Use for only PaymentGateway
                            PG.PaymentGateway objPg = new PG.PaymentGateway();
                            ZaakPayAPI.MobikwikTrans Mobikwik = new ZaakPayAPI.MobikwikTrans();
                            SqlTransaction objDA = new SqlTransaction();
                            DataSet AgencyDs = objDA.GetAgencyDetails(Convert.ToString(Session["UID"]));
                            string ipAddress = null;
                            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
                            {
                                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                            }
                            string ReferenceNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                            string OrderId = "PUP" + ReferenceNo.Substring(4, 16);
                            string Tid = ReferenceNo.Substring(4, 16);
                            #endregion
                            if (rblPaymentMode.SelectedValue == "Mobikwik" || rblPaymentMode.SelectedValue == "MALL" || rblPaymentMode.SelectedValue == "MCARD" || rblPaymentMode.SelectedValue == "MNETBANKING" || rblPaymentMode.SelectedValue == "MWALLETS" || rblPaymentMode.SelectedValue == "MUPI" || rblPaymentMode.SelectedValue == "MCCONLY" || rblPaymentMode.SelectedValue == "MDCONLY" || rblPaymentMode.SelectedValue == "MEZECLICK" || rblPaymentMode.SelectedValue == "MATMPIN" || rblPaymentMode.SelectedValue == "MMASTERPASS" || rblPaymentMode.SelectedValue == "MEMI")
                            {
                                #region Redirect to Razor gateway

                                string Reference = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                string OrderId_ = "WTRP" + Reference.Substring(4, 16);
                                string Tid_ = Reference.Substring(4, 16);


                                #region redirect to payment gateway  for Razor
                                //'Use for Payment Option
                                //PgMsg = objPg.PaymentGatewayReqPayU(OrderId, Tid, "", Convert.ToString(Session["UID"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(TxtAmout.Text), Convert.ToDouble(TxtAmout.Text), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "WALLET-TOP-UP", ipAddress, "", rblPaymentMode.SelectedValue);
                                try
                                {
                                    PgMsg = Mobikwik.PaymentGatewayReqMobikwik(OrderId_, Tid_, "", Convert.ToString(Session["UID"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(TxtAmout.Text), Convert.ToDouble(TxtAmout.Text), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "INR", "WALLET-TOP-UP", ipAddress, "", rblPaymentMode.SelectedValue);

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('hi" + PgMsg + "'); ", true);
                                }
                                catch (Exception ex)
                                {
                                    //lblerrormsg.Text = ex.Message;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('hi" + PgMsg + "'); ", true);
                                }

                                if (PgMsg.Contains("~"))
                                {
                                    if (PgMsg.Split('~')[0] == "yes")
                                    {
                                        //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                        if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                                        {
                                            Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                                            //Response.Write(PgMsg.Split('~')[1]);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 001');window.location='UploadAmount.aspx'; ", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 002');window.location='UploadAmount.aspx'; ", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 003');window.location='UploadAmount.aspx'; ", true);
                                }
                                #endregion


                                #endregion

                            }
                            else
                            {
                                #region redirect to payment gateway
                                //'Use for Payment Option
                                PgMsg = objPg.PaymentGatewayReqPayU(OrderId, Tid, "", Convert.ToString(Session["UID"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(TxtAmout.Text), Convert.ToDouble(TxtAmout.Text), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
                                Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "WALLET-TOP-UP", ipAddress, "", rblPaymentMode.SelectedValue);
                                if (PgMsg.Contains("~"))
                                {
                                    if (PgMsg.Split('~')[0] == "yes")
                                    {
                                        //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                        if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                                        {
                                            Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 001');window.location='UploadAmount.aspx'; ", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 002');window.location='UploadAmount.aspx'; ", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 003');window.location='UploadAmount.aspx'; ", true);
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Plese enter amount greater than zero'); ", true);
                        }
                    }                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Plese enter upload amount.'); ", true);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("../Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("../Login.aspx?reason=Session TimeOut", false);
        }
    }

    [System.Web.Services.WebMethod()]
    public static string GetPgChargeByMode(string paymode)
    {
        string TransCharge = "0~P";
        string PgCharge = "0";
        string ChargeType = "0";
        PG.PaymentGateway objP = new PG.PaymentGateway();        
        try
        {
            DataTable pgDT = objP.GetPgTransChargesByMode(paymode, "GetPgCharges");
            if (pgDT != null)
            {
                if (pgDT.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["Charges"])))
                    {
                        PgCharge = Convert.ToString(pgDT.Rows[0]["Charges"]).Trim();
                    }
                    else
                    {
                        PgCharge = "0";
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["ChargesType"])))
                    {
                        ChargeType = Convert.ToString(pgDT.Rows[0]["ChargesType"]).Trim();
                    }
                    else
                    {
                        ChargeType = "P";
                    }
                    TransCharge = PgCharge + "~" + ChargeType;
                }
            }
        }
        catch (Exception ex)
        {
            TransCharge = "0~P";
        }
        return TransCharge;
    }
}