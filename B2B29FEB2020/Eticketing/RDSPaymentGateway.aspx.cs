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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using javax.crypto;
using IRCTC_RDS;

public partial class Eticketing_RDSPaymentGateway : System.Web.UI.Page
{
    
    IrctcRdsPayment RDS = new IrctcRdsPayment();
    EncryptDecryptString ENDE = new EncryptDecryptString();

    string IPAddress;
    //string RDSRequest = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        DivLogin.Visible = false;
        DivAfterLogin.Visible = false;
        
        IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (IPAddress == "" || IPAddress == null)
            IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        if (!IsPostBack)
        {
            string EncryptedReq = string.Empty;
            string DecryptedReq = string.Empty;            
            string RDSRequestData = string.Empty;

            try
            { 
               
                //Request.RequestType//"POST"
                //Request.UrlReferrer.AbsolutePath//     /Eticketing/BankRequest.aspx
                //Request.UrlReferrer.AbsoluteUri  //http://localhost:53943/Eticketing/BankRequest.aspx
                //Request.UrlReferrer.Authority       //localhost:53943
                //Request.UrlReferrer.OriginalString      //http://localhost:53943/Eticketing/BankRequest.aspx
              
                
                //int flag = RDS.InsertExceptionLog("RDS-IRCTC", "RDSPaymentGateway.aspx.cs", "Check Response", Convert.ToString(Session["UID"]), Request.Form["encdata"] , Request.Form.ToString());

            EncryptedReq = Request.Form["encdata"];//Request.Form.ToString();
              //  EncryptedReq = "4E051A242D1FC1DDD5A4F3BCEF583C6E556B9464BBB368C52C180DE3555DA304EE6B98E8500E6C5B9810AADA629873EB94E7C9E54A3605326BC6C1222188020C89807F8EB0E64F1095460842855C53A9E80DED378CE49A6A0896092D97FC004D7826D1421AC0E2BB9FF642BF5FAEEB1583EF7BCED3720429CE415BBFBF3BA6968FB6C8120F5BDE5241BC776CED6FE9C07DC4AB6BFC7A7D2A6D15C03720786FD7EBD147A9184BE83574D563A6BF3D2923FF981241B3CA981CC8FEC1B216F847F86DD3093CEA43F508C9C4C464C0086FC7D9B48CC2C83DE6C89112CB955D10C9EF5BA31DC4813FC2699CFBCEFDFAE9D5D39804CCE55B9DE0CE81CB791C70A07675BA4489D851976324A97A466235B8BC0A939BA53826A6F56274B641690E7C8C88";//Request.Form.ToString();
            if (!string.IsNullOrEmpty(EncryptedReq))
             {                 
                 
                 DecryptedReq = ENDE.DecryptString(EncryptedReq);
                 //if (DecryptedReq.Contains("merchantCode=IR_") == false)
                 //{
                 //    DecryptedReq = "merchantCode=IR_" + DecryptedReq;
                 //}
                 //if (DecryptedReq.Contains("merchantCode=FLY") == false)
                 //{
                 //    DecryptedReq = "merchantCode=FLY" + DecryptedReq;
                 //}
                 // merchantCode=IR_CRIS|reservationId=1100012345|txnAmount=458.00|currencyType=INR|appCode=BOOKING|pymtMode=Internet|txnDate=20111206|securityId=CRIS|RU=https://localhost:8080/eticketing/BankResponse|checkSum=
                 if (DecryptedReq.Contains("reservationId=") && DecryptedReq.Contains("txnAmount=") && DecryptedReq.Contains("currencyType=") && DecryptedReq.Contains("appCode=") && DecryptedReq.Contains("pymtMode=") && DecryptedReq.Contains("txnDate=") && DecryptedReq.Contains("securityId=") && DecryptedReq.Contains("RU=") && DecryptedReq.Contains("checkSum="))
                 {
                     #region RDS Request Parse
                     string msg = RDS.ParseRdsRequest(EncryptedReq, DecryptedReq, IPAddress, "");
                     string RU = string.Empty;
                     string rtnMsg = string.Empty;
                     string RefNo = string.Empty;
                     string MercCode = string.Empty;
                     string ReservationId = string.Empty;
                     string CheckSum = string.Empty;
                     string TotalAmount = string.Empty;
                     ViewState["RefNo"] = RefNo;
                     ViewState["MercCode"] = MercCode;
                     ViewState["ReservationId"] = ReservationId;
                     ViewState["CheckSum"] = CheckSum;
                     ViewState["TotalAmount"] = TotalAmount;
                        if (Convert.ToString(ConfigurationManager.AppSettings["RdsReturnURLTest"]) == "true")//RdsReturnURLTest
                        {
                            ViewState["RU"] = Convert.ToString(ConfigurationManager.AppSettings["PostRdsReturnURL"]); //"http://localhost:53943/Eticketing/VerificationResponse.aspx"; //Use for test ,after test Comment line
                        }
                        else
                        {
                            ViewState["RU"] = RU;
                        }
                        
                     if (msg.Split('~').Length > 6)
                     {
                         // yes~FRD80124174006483463~IR_CRIS~1100012345~True~458.00
                         rtnMsg = msg.Split('~')[0];
                         RefNo = msg.Split('~')[1];
                         MercCode = msg.Split('~')[2];
                         ReservationId = msg.Split('~')[3];
                         CheckSum = msg.Split('~')[4];
                         TotalAmount = msg.Split('~')[5];
                         RU = msg.Split('~')[6];

                         lblTranAmount.Text = TotalAmount;
                         lblTransAmtAfterLogin.Text = TotalAmount;

                         lblUserId.Text = "";
                         HdnReferenceNo.Value = RefNo;
                         HdnMerchantCode.Value = MercCode;
                         HdnReservationId.Value = ReservationId;
                         HdnTrnsAmount.Value = TotalAmount;

                         if (rtnMsg == "yes" && !string.IsNullOrEmpty(RefNo) && !string.IsNullOrEmpty(MercCode) && !string.IsNullOrEmpty(ReservationId) && !string.IsNullOrEmpty(TotalAmount) && CheckSum == "True")
                         {
                             DivLogin.Visible = true;
                             DivAfterLogin.Visible = false;

                             ViewState["RefNo"] = RefNo;
                             ViewState["MercCode"] = MercCode;
                             ViewState["ReservationId"] = ReservationId;
                             ViewState["CheckSum"] = CheckSum;
                             ViewState["TotalAmount"] = TotalAmount;
                                if (Convert.ToString(ConfigurationManager.AppSettings["RdsReturnURLTest"]) == "true")//RdsReturnURLTest
                                {
                                    ViewState["RU"] = Convert.ToString(ConfigurationManager.AppSettings["PostRdsReturnURL"]);//Use for test ,after test Comment line
                                }
                                else
                                {
                                    ViewState["RU"] = RU;
                                }
                                
                         }
                         else
                         {
                             DivLogin.Visible = false;
                             DivAfterLogin.Visible = false;
                         }
                     }
                     else
                     {

                     }

                     #endregion
                 }
             }
            else
            {
                foreach (string s in Request.Params.Keys)
                {
                    //Response.Write(s.ToString() + ":" + Request.Params[s] + "<br>");
                    string PgResData = s.ToString() + ":" + Request.Params[s] + "~~";
                    RDSRequestData = RDSRequestData + PgResData;
                }
                int insert = RDS.InsertExceptionLog("RDS-IRCTC", "RDSPaymentGateway.aspx.cs", "RDS Request is null or empty", Convert.ToString(Session["UID"]), Request.Form["encdata"]+"~"+Request.Form.ToString(), RDSRequestData);
            }
            }
            catch(Exception ex)
            {
                //int insert = RDS.InsertExceptionLog("RDS-IRCTC", "RDSPaymentGateway.aspx.cs", "Page_Load", "",Request.Form.ToString(), RDSRequestData);
                int insert = RDS.InsertExceptionLog("IRCTC-RDS", "RDSPaymentGateway.aspx.cs", "Page_Load", "Login", RDSRequestData, ex.Message + "~" + ex.StackTrace);
            }           
           
        }
        
    }
   

    protected void Login(string userid, string pwd)
    {
        DataSet dset = new DataSet();
        try
        {            
            dset = RDS.user_auth(userid, pwd);
            if ((dset.Tables[0].Rows[0][0].ToString() == "Not a Valid ID"))
            {
                lblMsg.Text = "Your UserID Seems to be Incorrect";               
                DivLogin.Visible = true;
                DivAfterLogin.Visible = false;
            }
            else if ((dset.Tables[0].Rows[0][0].ToString() == "incorrect password"))
            {
                lblMsg.Text = "Your Password Seems to be Incorrect";
                DivLogin.Visible = true;
                DivAfterLogin.Visible = false;               
            }
            else
            {
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = "";
                    DivLogin.Visible = false;
                    DivAfterLogin.Visible = true;
                    lblAccountBalance.Text = Convert.ToString(dset.Tables[0].Rows[0]["Crd_Limit"]);
                    lblTransAmtAfterLogin.Text = lblTranAmount.Text;
                    lblUserId.Text = dset.Tables[0].Rows[0]["UID"].ToString();

                    ViewState["UserId"] =Convert.ToString(dset.Tables[0].Rows[0]["UID"]);
                    ViewState["AgencyId"] = Convert.ToString(dset.Tables[0].Rows[0]["AgencyId"]);
                    ViewState["DistrId"] = Convert.ToString(dset.Tables[0].Rows[0]["DistrId"]);
                    ViewState["AgencyName"] = Convert.ToString(dset.Tables[0].Rows[0]["AgencyName"]);
                    //Agency_Name,AgencyId,Distr,User_Id 
                }
            }


            if (lblTransAmtAfterLogin.Text == Convert.ToString(ViewState["TotalAmount"]) && !string.IsNullOrEmpty(Convert.ToString(ViewState["RefNo"])) && !string.IsNullOrEmpty(Convert.ToString(ViewState["MercCode"])) && !string.IsNullOrEmpty(Convert.ToString(ViewState["ReservationId"])) && !string.IsNullOrEmpty(Convert.ToString(ViewState["UserId"])))
            {
                BtnPay.Visible = true;
            }
            else
            {
                DivAfterLogin.Visible = false;
                BtnPay.Visible = false;
            }

        }
        catch (Exception ex)
        {            
            int insert = RDS.InsertExceptionLog("IRCTC-RDS", "RDSPaymentGateway.aspx.cs", "Page_Load", "Login", ex.Message, ex.StackTrace);
        }

    }   
         
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(TxtUserId.Text) && !string.IsNullOrEmpty(TxtPassword.Text))
            {
                string sUserID = TxtUserId.Text;
                string password = TxtPassword.Text;
                Login(sUserID, password);
            }
            else
            {
                DivLogin.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Enter  User Id & password.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            int insert = RDS.InsertExceptionLog("IRCTC-RDS", "RDSPaymentGateway.aspx.cs", "BtnLogin_Click", "Login", ex.Message, ex.StackTrace);
        }

    }
    protected void BtnPay_Click(object sender, EventArgs e)
    {
        try
        {        
        double TxnAmount=Convert.ToDouble(lblTransAmtAfterLogin.Text);
        string UserId=lblUserId.Text;
        string ReferenceNo = HdnReferenceNo.Value;
        string MerchantCode=HdnMerchantCode.Value;
        string ReservationId = HdnReservationId.Value;
        string BankTxnId = string.Empty;
        string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (IPAddress == "" || IPAddress == null)
            IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        string CreatedBy = lblUserId.Text;

        //HdnReferenceNo.Value = RefNo;
        //HdnMerchantCode.Value = MercCode;
        //HdnReservationId.Value = ReservationId;
        //HdnTrnsAmount.Value = TotalAmount;
        //    ViewState["RefNo"] = RefNo;
        //    ViewState["MercCode"] = MercCode;
        //    ViewState["ReservationId"] = ReservationId;
        //    ViewState["CheckSum"] = CheckSum;
        //    ViewState["TotalAmount"] = TotalAmount;
        //    ViewState["RU"] = RU;

            string FailStatus="";
            string ReturnUrl="";
            string FailRemark = "";

        if (lblTransAmtAfterLogin.Text == Convert.ToString(ViewState["TotalAmount"]) && ReferenceNo == Convert.ToString(ViewState["RefNo"]) && MerchantCode == Convert.ToString(ViewState["MercCode"]) && ReservationId == Convert.ToString(ViewState["ReservationId"]) && UserId == Convert.ToString(ViewState["UserId"]))
            {
              #region Check User balance and deduct amount and post data IRCTC
                string msg=RDS.CheckUserBalanceAndDeduct(TxnAmount, UserId, ReferenceNo, MerchantCode, ReservationId, IPAddress, CreatedBy);
                string PgMsg = string.Empty;
                if (msg == "Success")
                {
                    try { 
                    SqlTransaction objDA = new SqlTransaction();
                    DataSet AgencyDs = objDA.GetAgencyDetails(UserId);
                    if (AgencyDs != null && AgencyDs.Tables[0].Rows.Count > 0)
                    {
                        string AgencyId = Convert.ToString(AgencyDs.Tables[0].Rows[0]["AgencyId"]);
                        string AgentName = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Name"]);
                        string AgencyName = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]);
                        string AgentMobile = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]);
                        string AgentEmail = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]);
                        string AgentBalance = Convert.ToString(AgencyDs.Tables[0].Rows[0]["Crd_Limit"]);
                        SendSMS(AgentMobile, UserId, AgencyName, Convert.ToString(TxnAmount), AgentBalance, ReferenceNo);
                        SendMail(AgentEmail, UserId, AgencyId, AgencyName, AgentName, Convert.ToString(TxnAmount), AgentBalance, ReferenceNo);
                    }
                    }
                    catch (Exception ex)
                    {

                    }
                    BankTxnId = ReferenceNo;
                    PgMsg = RDS.CreatePaymentResponse(TxnAmount, UserId, ReferenceNo, MerchantCode, ReservationId, BankTxnId, IPAddress, CreatedBy);
                    if (PgMsg.Contains("~"))
                    {
                        if (PgMsg.Split('~')[0] == "yes" && PgMsg.Split('~').Length>0)
                            {                    
                                if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                                    {
                                        Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                                    }
                                 else
                                    {
                                    //Status Flag – 0 - Success,1 - Fail , 2 - Error
                                        FailStatus = "1";
                                        ReturnUrl = Convert.ToString(ViewState["RU"]);
                                        FailRemark = "Post html form null or empty. " + PgMsg;
                                        string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                                        Page.Controls.Add(new LiteralControl(PostForm));
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 001');", true);
                                    }
                            }
                          else
                            {
                                FailStatus = "1";
                                ReturnUrl = Convert.ToString(ViewState["RU"]);
                                FailRemark = "Getting Error when Create Payment Response-null or empty. " + PgMsg;
                                string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                                Page.Controls.Add(new LiteralControl(PostForm));    
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 002');", true);
                             }
                  }
                 else
                      {
                          FailStatus = "1";
                          ReturnUrl = Convert.ToString(ViewState["RU"]);
                          FailRemark = "CheckUserBalanceAndDeduct" + msg;
                          string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                          Page.Controls.Add(new LiteralControl(PostForm));    
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process busy - 003');", true);
                      }
        }
         else
             {
                    if(msg=="LowBalance" || msg=="Fail")
                    {
                        //Status Flag – 0 - Success,1 - Fail , 2 - Error
                        FailStatus = "1";
                        ReturnUrl = Convert.ToString(ViewState["RU"]);
                        if (msg == "LowBalance")
                        {
                            FailRemark = "Agent Account Balance Low - " + msg;
                        }
                        else
                        {
                            FailRemark = "Transaction amount not match  - " + msg;
                        }                       
                        string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                        Page.Controls.Add(new LiteralControl(PostForm));
                    }
                    else if (msg == "Error")
                    {
                        //set @Msg='Fail'
                        //    set @Status ='Error'
                        FailStatus = "2";
                        ReturnUrl = Convert.ToString(ViewState["RU"]);
                        FailRemark = "Transaction details not found in Ledger- " + msg;
                        string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                        Page.Controls.Add(new LiteralControl(PostForm));
                    }
                    else
                    {
                        FailStatus = "2";
                        ReturnUrl = Convert.ToString(ViewState["RU"]);
                        FailRemark = "Details not found in Ledger- " + msg;
                        string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, MerchantCode, ReferenceNo, ReservationId, BankTxnId, lblTransAmtAfterLogin.Text, FailStatus, ReturnUrl, FailRemark, IPAddress);
                        Page.Controls.Add(new LiteralControl(PostForm));
                    }


               }
        #endregion
            }
            else
                {
                    FailStatus = "2";
                    ReturnUrl = Convert.ToString(ViewState["RU"]);
                    FailRemark = "Not match record hidden field and ViewState.";
                    string PostForm = RDS.MakeFailAndErrorPaymentResponse(UserId, Convert.ToString(ViewState["MercCode"]), Convert.ToString(ViewState["RefNo"]), ReservationId, Convert.ToString(ViewState["RefNo"]), Convert.ToString(ViewState["TotalAmount"]), FailStatus, Convert.ToString(ViewState["RU"]), FailRemark, IPAddress);
                    Page.Controls.Add(new LiteralControl(PostForm));  
                }
        }
        catch (Exception ex)
        {
            int insert = RDS.InsertExceptionLog("IRCTC-RDS", "RDSPaymentGateway.aspx.cs", "BtnPay_Click", "Pay", ex.Message, ex.StackTrace);
        }
        //CheckUserBalanceAndDeduct(double TxnAmount, string UserId, string ReferenceNo, string MerchantCode, string ReservationId, string IPAddress, string CreatedBy, string ActionType)
    }
    protected void BtnLoginCancel_Click(object sender, EventArgs e)
    {
       string FailStatus = "1";
       string ReturnUrl = Convert.ToString(ViewState["RU"]);
       string FailRemark = "User cancel before login.";
       string PostForm = RDS.MakeFailAndErrorPaymentResponse(lblUserId.Text, Convert.ToString(ViewState["MercCode"]), Convert.ToString(ViewState["RefNo"]), Convert.ToString(ViewState["ReservationId"]), Convert.ToString(ViewState["RefNo"]), Convert.ToString(ViewState["TotalAmount"]), FailStatus, Convert.ToString(ViewState["RU"]), FailRemark, IPAddress);
        Page.Controls.Add(new LiteralControl(PostForm));  
    }
    protected void BtnPayCancel_Click(object sender, EventArgs e)
    {
        string FailStatus = "1";
        string ReturnUrl = Convert.ToString(ViewState["RU"]);
        string FailRemark = "User cancel after login.";
        string PostForm = RDS.MakeFailAndErrorPaymentResponse(lblUserId.Text, Convert.ToString(ViewState["MercCode"]), Convert.ToString(ViewState["RefNo"]), Convert.ToString(ViewState["ReservationId"]), Convert.ToString(ViewState["RefNo"]), Convert.ToString(ViewState["TotalAmount"]), FailStatus, Convert.ToString(ViewState["RU"]), FailRemark, IPAddress);
        Page.Controls.Add(new LiteralControl(PostForm));  
    }

    protected void SendSMS(string Mobile, string UserId,string AgencyName, string TxnAmount,string AvilableBalance,string RefNo)
    {
        SqlTransactionDom STDOM = new SqlTransactionDom();
        DataSet M = STDOM.GetMailingDetails("OTP", "");
        try
        {
            string smsMsg = "";
            string smsStatus = "";

            SMSAPI.SMS objSMSAPI = new SMSAPI.SMS();
            SqlTransactionNew objSql = new SqlTransactionNew();
            DataSet SmsCrd = new DataSet();
            SqlTransaction objDA = new SqlTransaction();
            SmsCrd = objDA.SmsCredential(Convert.ToString(SMS.AIRBOOKINGDOM));
            if (SmsCrd != null && SmsCrd.Tables.Count > 0 && SmsCrd.Tables[0].Rows.Count > 0 && Convert.ToBoolean(SmsCrd.Tables[0].Rows[0]["Status"]) == true)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = SmsCrd.Tables[0];//DateTime.Now.ToString("HH:mm:ss tt")
                    smsMsg = "Your Anupam Travel wallet a/c (UserId-" + UserId + ") is used for Rs. " + TxnAmount + " on " + DateTime.Now.ToString() + " at IRCTC and your Reference No  " + RefNo + "  Call Anupam Travel customer care if you have not used it.";
                     //smsMsg = "Dear Customer,Your user id is  " & AgecyId & " and debited Rs. " & CreditAmount & " on " & Convert.ToString(DateAndTime.Now()) & " in your a/c and current available balance  " & CurrentAvlBal & "."
                    string MobileNo = Convert.ToString(Mobile);
                    smsStatus = objSMSAPI.SendSmsForAnyService(ref MobileNo, ref smsMsg, dt);
                    objSql.SmsLogDetails(RefNo, Mobile, smsMsg, smsStatus);
                }
                catch (Exception ex)
                {

                }
            }

        }
        catch (Exception ex)
        {
        }

    }
    protected void SendMail(string EmailId, string UserId, string AgencyId, string AgencyName, string AgentName, string TxnAmount, string AvilableBalance, string RefNo)
    {      
            bool MailSent = false;
            try
            {
                //string AgencyName = "Devesh";
                //string ToEmail = "devesh.mailme@gamil.com";
                int Sent = SendEmail(EmailId,UserId,AgencyId,AgencyName,AgentName,TxnAmount,AvilableBalance, RefNo);
                if (Sent > 0)
                {
                    MailSent = true;
                }
                else
                {
                    MailSent = false;
                }
            }
            catch (Exception ex)
            {

            }
        

    }
    private int SendEmail(string EmailId, string UserId, string AgencyId, string AgencyName, string AgentName, string TxnAmount, string AvilableBalance,string RefNo)
    {
        int SendMail = 0;
        try
        {
            DataSet MailDs = new DataSet();
            DataTable MailDt = new DataTable();
            SqlTransactionDom STDom = new SqlTransactionDom();
            // MailDt = STDom.GetMailingDetails(MAILING.REGISTRATION_AGENT.ToString().Trim(), "");
            MailDs = STDom.GetMailingDetails(MAILING.REGISTRATION_AGENT.ToString().Trim(), "FWS");
            if (MailDs != null && MailDs.Tables.Count > 0 && MailDs.Tables[0].Rows.Count > 0)
            {
                MailDt = MailDs.Tables[0];
            }
            string mailbody = "";
            mailbody += "<table border='0' cellpadding='0' cellspacing='0' width='575' style='border-collapse:collapse;width:431pt'>";
            mailbody += "<tbody>";
            mailbody += "<tr height='102' style='height:76.5pt'>";
            mailbody += "<td height='102' class='m_4924402671878462581xl66' width='575' style='height:76.5pt;width:431pt'>";
            mailbody += "Dear &nbsp;&nbsp; " + AgencyName + " , <br> <br>This is to inform you that an amount of Rs." + TxnAmount + " has been debited from your Anupam Travel wallet account,<br><br> Agenncy Id- " + AgencyId + " on  account of an online payment transaction done using IRCTC Online Booking.<br><br>Your Reference No is " + RefNo + " .<br><br> For more details on the transaction, please call Anupam Travel Customer Care.<br> <br>For any queries E-mail <br>";
            mailbody += "<br> <br>Thank you,";
            mailbody += "<br><br> Team Support";
             mailbody += "<br><br> (This is a system Generated Mail and should not be replied to)";
            mailbody += "</td>";
            mailbody += "</tr>";
            mailbody += "</tbody>";
            mailbody += "</table>";
            try
            {
                if ((MailDt.Rows.Count > 0))
                {
                    bool Status = false;
                    Status = Convert.ToBoolean(MailDt.Rows[0]["Status"].ToString());
                    if (Status == true)
                    {
                        string MailSubject = "Amount debited your Anupam Travel A/C ,Agency Id- " + AgencyId;
                        //(ByVal toEMail As String, ByVal from As String, ByVal bcc As String, ByVal cc As String, ByVal smtpClient As String, ByVal userID As String, ByVal pass As String, ByVal body As String, ByVal subject As String, ByVal AttachmentFile As String) As Integer
                        SendMail = STDom.SendMail(EmailId, Convert.ToString(MailDt.Rows[0]["MAILFROM"]), Convert.ToString(MailDt.Rows[0]["BCC"]), Convert.ToString(MailDt.Rows[0]["CC"]), Convert.ToString(MailDt.Rows[0]["SMTPCLIENT"]), Convert.ToString(MailDt.Rows[0]["UserId"]), Convert.ToString(MailDt.Rows[0]["Pass"]), mailbody, MailSubject, "");
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
        return SendMail;
    }

}