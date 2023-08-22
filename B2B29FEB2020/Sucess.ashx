<%@ WebHandler Language="C#" Class="Sucess" %>

using System;
using System.Web;
using Paynimo;
using System.Data;
public class Sucess : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        PaynimoPaymentGatway objPg = new PaynimoPaymentGatway();
        string newUrl = string.Empty;
        string ReferenceNo = "HXPG" + DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        try
        {
            
            string EncryptedResponse = string.Empty;
            string strPG_MerchantCode = string.Empty;
            string ResponseForm = string.Empty;
            string OrderId = string.Empty;            
            try
            {
                string ipAddress = null;
                try
                {
                    ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
                    {
                        ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                catch (Exception ex)
                { 
                }
                
                 EncryptedResponse = context.Request.Form["msg"];
                 strPG_MerchantCode = HttpContext.Current.Request["tpsl_mrct_cd"];
                
                 int insert = objPg.PGLogTrack(ReferenceNo, "", EncryptedResponse, ipAddress,"INSERTLOG");
                 if (!string.IsNullOrEmpty(EncryptedResponse))
                 {
                     OrderId = ParsePaymentResponse(EncryptedResponse, ReferenceNo, ipAddress);
                 }
                 else {
                     OrderId = "ERROR";
                 }
            }
            catch (Exception ex)
            {
                ResponseForm = context.Request.Form.ToString();
                int insert = objPg.InsertExceptionLog("PaynimoPaymentGatway", "PGLogTrack", "Sucess.ashx_Handler", "ProcessRequest", ex.Message + ",Response:" + EncryptedResponse, ex.StackTrace + ",ResponseForm:" + ResponseForm);
            }
             // http://182.71.94.67/SwiftTravel/UIDVerification/UIDVerification.asmx/UIDVerify?DistCode=SWI00000000016&UID=58330df0df&AgencyCode= QUICKSUN2019
            //newUrl = "http://localhost:53943/ResponsePage.aspx?OrderId=" + OrderId + "&ReferenceNo=" + ReferenceNo;
            //newUrl = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"])+ "PaymentSucess.aspx?OrderId=" + OrderId + "&ReferenceNo=" + ReferenceNo;
            //newUrl = "http://localhost:53943/PaymentSucess.aspx?OrderId=" + OrderId + "&ReferenceNo=" + ReferenceNo;
            newUrl = "http://anupamtravelonline.com/PaymentSucess.aspx?OrderId=" + OrderId + "&ReferenceNo=" + ReferenceNo;
            context.Response.Redirect(newUrl,false);
        }
        catch (Exception ex)
        {
            int insert = objPg.InsertExceptionLog("PaynimoPaymentGatway", "Sucess.ashx_Handler1",ReferenceNo, newUrl, ex.Message, ex.StackTrace);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    //
    public string ParsePaymentResponse(string pgResponse, string ReferenceNo,string ipAddress)
    {
        
        #region Parse Response
        PaynimoPaymentGatway obPg = new PaynimoPaymentGatway();
        string ObTid = "";// Request.QueryString["OBTID"];
        string IbTid = "";//Request.QueryString["IBTID"];
        string Ft = "";// Request.QueryString["FT"]; 
        string PgTid = "";
        string ServiceType = "";
        string PaymentStatus = string.Empty;
        string ApiStatus = string.Empty;       
        string UnmappedStatus = string.Empty;
        string CreditLimitUpdate = string.Empty;
        string AgentId = string.Empty;
        string pgMessage = string.Empty;
        string Trip = string.Empty;
        try
        {            
                if (!string.IsNullOrEmpty(pgResponse))
                {
                   // string ReferenceNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                    string msg = obPg.UpdatePaymentResponseDetailsPaynimo("", pgResponse, ReferenceNo);
                    // string msg = objPg.UpdatePaymentResponseDetailsPaynimo("devesh", strPGResponse, strPG_MerchantCode);
                    if (!string.IsNullOrEmpty(msg) && msg.Split('~')[0] == "yes")
                    {
                        
                        ObTid = msg.Split('~')[1];
                        if (!string.IsNullOrEmpty(ObTid.Trim()))
                        {
                            #region Get Value After payment details
                            DataSet ds = obPg.GetPaymentDetailsPaynimo(ReferenceNo, ObTid, "GetDetails");
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
                                    AgentId = Convert.ToString(ds.Tables[0].Rows[0]["AgentId"]);
                                }
                            }
                            #endregion                            
                            if (PaymentStatus == "success" && UnmappedStatus == "0300" && ApiStatus == "success")
                            {
                                if (CreditLimitUpdate.ToLower() == "false" && !string.IsNullOrEmpty(AgentId))
                                {
                                    int flag = obPg.UpdateCreditLimit(AgentId, ObTid, ServiceType, ipAddress);
                                }
                            }                            
                        }
                        else
                        {
                            ObTid = "ERROR";
                            //Response.Redirect("FlightInt/BookingMsg.aspx?msg=2", false);
                        }                        
                    }
                    else
                    {                        
                        //Redirect Error page or Show error message
                        ObTid = "ERROR";
                    }
                }
                else
                {
                    //Redirect Error page or Show error message
                    ObTid = "ERROR";
                }
                //end          
        }
        catch (Exception ex)
        {
            //Redirect Error page or Show error message
            ObTid = "ERROR";        
            int insert = obPg.InsertExceptionLog("PaynimoPaymentGatway", "Sucess.ashx", "ParsePaymentResponse", "ParsePaymentResponse", ex.Message, ex.StackTrace);
        }
        #endregion

        return ObTid;
    }
    //
}