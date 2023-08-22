using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IRCTC_RDS;
using System.Configuration;
public partial class Eticketing_VerificationResponse : System.Web.UI.Page
{
    EncryptDecryptString ENDE = new EncryptDecryptString();
    IrctcRdsPayment RDS = new IrctcRdsPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region PARSE RESPONSE
        lblResponse.Text = "";
        string RDSRequest = string.Empty;
        string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (IPAddress == "" || IPAddress == null)
            IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        if (!IsPostBack)
        {
            string EncryptedReq = "";
            string DecryptedReq = "";
            RDSRequest = Request.Form.ToString();

            if (RDSRequest.Contains("encdata=") && RDSRequest.Split('=').Length > 1)
            {
                EncryptedReq = RDSRequest.Split('=')[1];
                //DecryptedReq = ENDE.DecryptString(EncryptedReq, "abfb7c8d48dfc4f1ce7ed92a44989f25");
                //string EnDecryptKey = Convert.ToString(ConfigurationManager.AppSettings["RDSKEY"]);
                DecryptedReq = ENDE.DecryptString(EncryptedReq);
                //if (DecryptedReq.Contains("merchantCode=IR_") == false)
                //{
                //    DecryptedReq = "merchantCode=IR_" + DecryptedReq;
                //}
                lblResponse.Text = DecryptedReq;

                string PgMsg = "";// RDS.MakeDoublleVerificationReq(EncryptedReq, DecryptedReq, IPAddress, "");

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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 001');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 002');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 003');", true);
                }

            }
        }
        #endregion

    }
}