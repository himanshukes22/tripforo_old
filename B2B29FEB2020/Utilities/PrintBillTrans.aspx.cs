using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SWIFTMoneyBillPayments;

public partial class DMT_Manager_PrintBillTrans : System.Web.UI.Page
{
    private static string UserId { get; set; }
    private static string TransId { get; set; }
    private static string AgentId { get; set; }
    private static string TrackId { get; set; }
    public static string HtmlContent { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        TransId = Request.QueryString["transid"] != null ? Request.QueryString["transid"].ToString() : string.Empty;
        AgentId = Request.QueryString["agentid"] != null ? Request.QueryString["agentid"].ToString() : string.Empty;
        TrackId = Request.QueryString["trackid"] != null ? Request.QueryString["trackid"].ToString() : string.Empty;
        UserId = AgentId;

        if (string.IsNullOrEmpty(TransId) && string.IsNullOrEmpty(AgentId) && string.IsNullOrEmpty(TrackId))
        {
            Response.Redirect("/");
        }
        else
        {
            HtmlContent = BindTransPrint();
        }
    }

    public string BindTransPrint()
    {
        StringBuilder sbResult = new StringBuilder();

        if (!string.IsNullOrEmpty(UserId))
        {
            if (!string.IsNullOrEmpty(TrackId) && !string.IsNullOrEmpty(AgentId))
            {
                DataTable dtAgency = SMBPApiService.GetAgencyDetailById(UserId);
                string agencyName = dtAgency.Rows[0]["Agency_Name"].ToString();
                string agencyAddress = dtAgency.Rows[0]["Address"].ToString() + ",<br/>" + dtAgency.Rows[0]["City"].ToString() + ", " + dtAgency.Rows[0]["State"].ToString() + ", " + dtAgency.Rows[0]["Country"].ToString() + ", " + dtAgency.Rows[0]["zipcode"].ToString();
                string agencyMobile = dtAgency.Rows[0]["Mobile"].ToString();
                string agencyEmail = dtAgency.Rows[0]["Email"].ToString();

                DataTable dtTrans = SMBPApiService.GetBillTransactionHistory(UserId, null, null, TrackId, null, null);
                string transType = !string.IsNullOrEmpty(dtTrans.Rows[0]["ServiceType"].ToString()) ? dtTrans.Rows[0]["ServiceType"].ToString().ToLower().Trim() : string.Empty;

                sbResult.Append("<div style='border: 1px solid #ff8d3c; width: 800px; border-collapse: initial;' class='main'>");
                sbResult.Append("<div class='col-sm-12'>");
                sbResult.Append("<table data-toggle='table' style='width: 100%; border-collapse: initial; font-family: Verdana,Geneva,sans-serif; font-size: 12px; border-spacing: 0px; padding: 0px;'><tbody>");
                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid #ff8d3c; font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<td style='text-align: left;'><img src='/Advance_CSS/Icons/logo(ft).png' style='max-width: 200px;' /></td>");
                if (!string.IsNullOrEmpty(transType) && (transType != "prepaid" && transType != "mobile postpaid"))
                {
                    sbResult.Append("<td style='text-align: center;'><img src='/utilities/bbps_Logo.png' style='max-width: 200px;height:30px;' /></td>");
                }
                sbResult.Append("<td style='text-align: right;padding: 7px;'>");
                sbResult.Append("<span style='font-size: 15px;'>" + agencyName + "</span><br />");
                sbResult.Append("<span>" + agencyAddress + "</span><br />");
                sbResult.Append("<span>Mobile: " + agencyMobile + "</span><br />");
                sbResult.Append("<span>Email: " + agencyEmail + "</span>");
                sbResult.Append("</td>");
                sbResult.Append("</tr>");
                sbResult.Append("</tbody></table>");
                //sbResult.Append("<a href='#'><img src='../Images/gallery/logo(ft).png' style='max-width: 200px;' /></a>");
                //sbResult.Append("<span style='float: right!important; text-align: right; padding: 7px;'>");
                ////sbResult.Append("<span>" + dtRemitter.Rows[0]["FirstName"].ToString() + " " + dtRemitter.Rows[0]["LastName"].ToString() + "</span><br />");
                ////sbResult.Append("<span>" + dtRemitter.Rows[0]["Address"].ToString() + ", " + dtRemitter.Rows[0]["Pincode"].ToString() + "</span>");
                //sbResult.Append("<span style='font-size: 20px;'>" + agencyName + "</span><br />");
                //sbResult.Append("<span>" + agencyAddress + "</span><br />");
                //sbResult.Append("<span>Mobile: " + agencyMobile + "</span><br />");
                //sbResult.Append("<span>Email: " + agencyEmail + "</span>");
                //sbResult.Append("</span>");
                sbResult.Append("</div>");
                sbResult.Append("<br/>");
                sbResult.Append("<div class='col-sm-12'>");
                sbResult.Append("<hr style='text-align: center; border: 1px solid #ff8d3c; background-color: #ff8d3c; margin-top: 0px; margin-bottom: 0;' />");
                sbResult.Append("<h4 class='heading'>Customer Transaction Receipt <div class='sendmail form-validation'><input type='text' placeholder='enter email id' id='ReceiptSendMail' style='width: 200px;' /> <span style='cursor:pointer;' id='btnMailSend'>Send Mail</span></div></h4>");
                sbResult.Append("<table data-toggle='table' style='width: 800px; border-collapse: initial; border: 1px solid #ff8d3c; font-family: Verdana,Geneva,sans-serif; font-size: 12px; border-spacing: 0px; padding: 0px;'>");



                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Number</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Number"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Customer Name</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["CustomerName"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Order ID</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["TransactionId"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Client Ref. Id</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["ClientRefId"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Amount</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>₹ " + dtTrans.Rows[0]["Amount"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Operator</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Operator"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Service Type</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["ServiceType"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Status</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Status"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Trans. Date</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + ConvertStringDateToStringDateFormate(dtTrans.Rows[0]["UpdatedDate"].ToString()) + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("</table>");
                sbResult.Append("<h4 style='padding-left: 5px;'>Note :-</h4>");
                //sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>1. Customer transaction charge is minimum of Rs. 10/- and Maximum 1% of the transaction amount.</p><br />");
                //sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>2. In case of non-payment to the beneficiary, the customer will receive an SMS with an OTP that he/she needs to present at the agent location where the transaction was initiated.</p><br />");
                sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>1. In case the Agent charges the Customer in excess of the fee/charges as mentioned in the receipt, he/she should lodge complaint about the same with our Customer Care on Tel. No. (+91)-8902231371 or email us at www.anupamtravelonline.com .</p><br />");
                sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>2. The receipt is subject to terms and conditions, privacy policy and terms of use detailed in the website www.anupamtravelonline.com  and shall be binding on the Customer for each transaction.</p><br /><br />");

                sbResult.Append("<table data-toggle='table' style='width: 800px; border-collapse: initial; font-family: Verdana,Geneva,sans-serif; font-size: 12px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<tr>");
                sbResult.Append("<td><p>Date..............................</p></td>");
                sbResult.Append("<td><p style='float: right!important;'>Signature Of Customer's......................................................</p></td>");
                sbResult.Append("</tr>");
                sbResult.Append("</table>");
                sbResult.Append("<br /><br />");
                sbResult.Append("</div>");
                sbResult.Append("</div>");
            }
        }

        return sbResult.ToString();
    }

    public static string ConvertStringDateToStringDateFormate(string date)
    {
        DateTime dtDate = new DateTime();

        if (!string.IsNullOrEmpty(date))
        {
            dtDate = DateTime.Parse(date);
            return dtDate.ToString("dd MMM yyyy hh:mm tt");
        }

        return string.Empty;
    }

    [WebMethod]
    public static string SendReceiptInMail(string emailid)
    {
        if (!string.IsNullOrEmpty(emailid.Trim()))
        {
            string mailBody = ReceiptSend();

            DataTable dtAgency = SMBPApiService.GetAgencyDetailById(UserId);

            if (dtAgency != null && dtAgency.Rows.Count > 0)
            {
                SqlTransactionDom STDOM = new SqlTransactionDom();
                DataTable MailDt = new DataTable();
                MailDt = STDOM.GetMailingDetails("RECHARGE", UserId).Tables[0];
                if (MailDt != null && MailDt.Rows.Count > 0)
                {
                    bool Status = Convert.ToBoolean(MailDt.Rows[0]["Status"].ToString());
                    string subject = "Payment Transaction Receipt [Client Ref# " + TrackId + "]";
                    if (Status)
                    {
                        int isSuccess = STDOM.SendMail(emailid, MailDt.Rows[0]["MAILFROM"].ToString(), MailDt.Rows[0]["BCC"].ToString(), MailDt.Rows[0]["CC"].ToString(), MailDt.Rows[0]["SMTPCLIENT"].ToString(), MailDt.Rows[0]["UserId"].ToString(), MailDt.Rows[0]["Pass"].ToString(), mailBody, subject, "");
                        if (isSuccess > 0)
                        {
                            return "sent";
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                }
            }
        }

        return string.Empty;
    }

    private static string ReceiptSend()
    {
        StringBuilder sbResult = new StringBuilder();

        if (!string.IsNullOrEmpty(UserId))
        {
            if (!string.IsNullOrEmpty(TrackId) && !string.IsNullOrEmpty(AgentId))
            {
                DataTable dtAgency = SMBPApiService.GetAgencyDetailById(UserId);
                string agencyName = dtAgency.Rows[0]["Agency_Name"].ToString();
                string agencyAddress = dtAgency.Rows[0]["Address"].ToString() + ",<br/>" + dtAgency.Rows[0]["City"].ToString() + ", " + dtAgency.Rows[0]["State"].ToString() + ", " + dtAgency.Rows[0]["Country"].ToString() + ", " + dtAgency.Rows[0]["zipcode"].ToString();
                string agencyMobile = dtAgency.Rows[0]["Mobile"].ToString();
                string agencyEmail = dtAgency.Rows[0]["Email"].ToString();

                DataTable dtTrans = SMBPApiService.GetBillTransactionHistory(UserId, null, null, TrackId, null, null);
                string transType = !string.IsNullOrEmpty(dtTrans.Rows[0]["ServiceType"].ToString()) ? dtTrans.Rows[0]["ServiceType"].ToString().ToLower().Trim() : string.Empty;

                sbResult.Append("<div style='border: 1px solid #ff8d3c; width: 800px; border-collapse: initial;' class='main'>");
                sbResult.Append("<div class='col-sm-12'>");
                sbResult.Append("<table data-toggle='table' style='width: 100%; border-collapse: initial; font-family: Verdana,Geneva,sans-serif; font-size: 12px; border-spacing: 0px; padding: 0px;'><tbody>");
                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid #ff8d3c; font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<td style='text-align: left;'><img src='/Advance_CSS/Icons/logo(ft).png' style='max-width: 200px;' /></td>");
                if (!string.IsNullOrEmpty(transType) && (transType != "prepaid" && transType != "mobile postpaid"))
                {
                    sbResult.Append("<td style='text-align: center;'><img src='/utilities/bbps_Logo.png' style='max-width: 200px;height:30px;' /></td>");
                }
                sbResult.Append("<td style='text-align: right;padding: 7px;'>");
                sbResult.Append("<span style='font-size: 15px;'>" + agencyName + "</span><br />");
                sbResult.Append("<span>" + agencyAddress + "</span><br />");
                sbResult.Append("<span>Mobile: " + agencyMobile + "</span><br />");
                sbResult.Append("<span>Email: " + agencyEmail + "</span>");
                sbResult.Append("</td>");
                sbResult.Append("</tr>");
                sbResult.Append("</tbody></table>");
                sbResult.Append("</div>");
                sbResult.Append("<br/>");
                sbResult.Append("<div class='col-sm-12'>");
                sbResult.Append("<br/>");
                sbResult.Append("<hr style='text-align: center; border: 1px solid #ff8d3c; background-color: #ff8d3c; margin-top: 0px; margin-bottom: 0;' />");
                sbResult.Append("<h4 style='text-align: center;'>Customer Transaction Receipt</h4>");
                sbResult.Append("<table data-toggle='table' style='width: 800px; border-collapse: initial; border: 1px solid #ff8d3c; font-family: Verdana,Geneva,sans-serif; font-size: 12px; border-spacing: 0px; padding: 0px;'>");



                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Number</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Number"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Customer Name</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["CustomerName"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Order ID</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["TransactionId"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Client Ref. Id</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["ClientRefId"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Amount</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>₹ " + dtTrans.Rows[0]["Amount"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Operator</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Operator"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Service Type</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["ServiceType"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Status</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + dtTrans.Rows[0]["Status"].ToString() + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("<tr style='border-collapse: initial; border: 1px solid rgb(204,204,224); font-size: 11px; border-spacing: 0px; padding: 0px;'>");
                sbResult.Append("<th colspan='2' style='text-align: left; border-right: 1px solid #b3b3b3; border-bottom: 1px solid #b3b3b3;width: 200px; background-color: #f1f1f1; color: #484848; padding: 10px;'>Trans. Date</th>");
                sbResult.Append("<td colspan='3' style='text-align: left; border-bottom: 1px solid #b3b3b3; padding: 10px;'>" + ConvertStringDateToStringDateFormate(dtTrans.Rows[0]["UpdatedDate"].ToString()) + "</td>");
                sbResult.Append("</tr>");

                sbResult.Append("</table>");
                sbResult.Append("<h4 style='padding-left: 5px;'>Note :-</h4>");
                //sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>1. Customer transaction charge is minimum of Rs. 10/- and Maximum 1% of the transaction amount.</p><br />");
                //sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>2. In case of non-payment to the beneficiary, the customer will receive an SMS with an OTP that he/she needs to present at the agent location where the transaction was initiated.</p><br />");
                sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>1. In case the Agent charges the Customer in excess of the fee/ charges as mentioned in the receipt, he/she should lodge complaint about the same with our Customer Care on Tel. No. (+91)-8902231371 or email us at www.anupamtravelonline.com .</p><br />");
                sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>2. The receipt is subject to terms and conditions, privacy policy and terms of use detailed in the website www.anupamtravelonline.com and shall be binding on the Customer for each transaction.</p><br /><br />");
                sbResult.Append("<p style='margin: 0px; padding-left: 5px;'>3. This is a system generated receipt hence does not require any signature.</p><br /><br />");
                sbResult.Append("</div>");
                sbResult.Append("</div>");
            }
        }

        return sbResult.ToString();
    }
}