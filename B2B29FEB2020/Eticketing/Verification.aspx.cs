using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IRCTC_RDS;
using System.Configuration;

public partial class Eticketing_Verification : System.Web.UI.Page
{
    EncryptDecryptString ENDE = new EncryptDecryptString();
    IrctcRdsPayment RDS = new IrctcRdsPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        string RDSRequest = string.Empty;
        string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (IPAddress == "" || IPAddress == null)
            IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        if (!IsPostBack)
        {
            string EncryptedReq = string.Empty;
            string DecryptedReq = string.Empty;
            string ReturnUrl = string.Empty;
            try
            {
                RDSRequest = Request.Form.ToString();
                EncryptedReq = Request.Form["encdata"];//Request.Form.ToString();
                ReturnUrl = Request.UrlReferrer.AbsoluteUri;           
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDSVERIFICATION1", Request.Form["encdata"], "IRCTC_ReturnUrl-" + ReturnUrl+"~~"+ Request.Form.ToString());
            }
            catch(Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDSVERIFICATION1_EXC", ex.Message, "IRCTC_ReturnUrl-" + ReturnUrl + "~~" + ex.StackTrace.ToString());
            }

            try
            {
            

            //if (RDSRequest.Contains("encdata=") && RDSRequest.Split('=').Length > 1)
            //{
            if (!string.IsNullOrEmpty(EncryptedReq))
            {  
                //EncryptedReq = RDSRequest.Split('=')[1];
                //DecryptedReq = ENDE.DecryptString(EncryptedReq, "abfb7c8d48dfc4f1ce7ed92a44989f25");
                //string EnDecryptKey = Convert.ToString(ConfigurationManager.AppSettings["RDSKEY"]);
                DecryptedReq = ENDE.DecryptString(EncryptedReq);

                //if (DecryptedReq.Contains("merchantCode=IR_") == false)
                //{
                //    DecryptedReq = "merchantCode=IR_" + DecryptedReq;
                //}

                string PgMsg = RDS.ParseDoubleVerificationReq(EncryptedReq, DecryptedReq, IPAddress, "Anupam Travel", ReturnUrl);
                if (PgMsg.Contains("~"))
                {
                    if (PgMsg.Split('~').Length > 1 && PgMsg.Split('~')[0] == "yes")
                    {
                        //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                        if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                        {
                            Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                        }
                        //else
                        //{
                        //    Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                        //}
                    }                    
                }
                else
                {
                        int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDSVERIFICATION1","PgMsg-"+ PgMsg+"~~"+ EncryptedReq, "IRCTC_ReturnUrl-" + ReturnUrl + "~~" + DecryptedReq);
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 003');", true);
                    }


            }
            }
            catch(Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "Verification.aspx.cs", "Check Response", "RDS", ex.Message, ex.StackTrace.ToString());
            }
        }

    }
}