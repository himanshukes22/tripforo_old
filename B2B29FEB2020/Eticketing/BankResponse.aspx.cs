using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IRCTC_RDS;
using System.Configuration;
using System.Data;
public partial class Eticketing_BankResponse : System.Web.UI.Page
{

    string workingKey = "";
    public string strAccessCode = "";
    public string strEncRequest = "";
    public string strCCAveUrl = "";
    EncryptDecryptString ENDE = new EncryptDecryptString();
    IrctcRdsPayment RDS = new IrctcRdsPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        #region PARSE RESPONSE
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

                string PgMsg = RDS.MakeDoublleVerificationReq(EncryptedReq, DecryptedReq, IPAddress, "");

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


       
            
            DataSet ds = new DataSet();
            string ObTid = Request.QueryString["OBTID"];
            string IbTid = Request.QueryString["IBTID"];
            string Ft = Request.QueryString["FT"];
           
            string RDSPGUrl = "http://localhost:53943/Eticketing/Verification.aspx";
            strAccessCode = "";
            //strEncRequest = "encdata=D5620509499F1C240C7FBBDDA4D4C1C22EB36C2C5C3D74B0AC919C18AD3A13EEBC1A968F9DC8EEB45F16BC59D256D02A7A48724E8291C015FCBA0BF3974AB3BD0515B648EC60A0DA893DBBA2C2935CFBBBD2E94ED4C5E0B2D094DF3861DB3903F5300EBFD0F2D502FCEE25F55DE66272CE50302585BD00CBBF66A390A623AF579830FA93549C5170CDDF64098E89A65C442C40E0FE10B711FCFA324AD3BF3CEAC166E51997D084A6DAB66E57A2A3B4843A4A2546183EDBAD7F5C40DF0B105013010FDF70E9C794D71A34C3C6C6F9BA965286208EEA5F92D041DE90CE9D677BB8356C583529151F647B1B380C68466212840F147A314485FD009239D6401AFC7F90B22686919BAECFDB44BD852AE1A189";
            strEncRequest = "D5620509499F1C240C7FBBDDA4D4C1C22EB36C2C5C3D74B0AC919C18AD3A13EEBC1A968F9DC8EEB45F16BC59D256D02A7A48724E8291C015FCBA0BF3974AB3BD0515B648EC60A0DA893DBBA2C2935CFBBBD2E94ED4C5E0B2D094DF3861DB3903F5300EBFD0F2D502FCEE25F55DE66272CE50302585BD00CBBF66A390A623AF579830FA93549C5170CDDF64098E89A65C442C40E0FE10B711FCFA324AD3BF3CEAC166E51997D084A6DAB66E57A2A3B4843A4A2546183EDBAD7F5C40DF0B105013010FDF70E9C794D71A34C3C6C6F9BA965286208EEA5F92D041DE90CE9D677BB8356C583529151F647B1B380C68466212840F147A314485FD009239D6401AFC7F90B22686919BAECFDB44BD852AE1A189";
            strCCAveUrl = RDSPGUrl;// +strEncRequest;

        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }


    }
}