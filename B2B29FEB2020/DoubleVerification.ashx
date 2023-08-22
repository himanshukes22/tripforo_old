<%@ WebHandler Language="C#" Class="DoubleVerification" %>

using System;
using System.Web;
using IRCTC_RDS;
using System.Configuration;
using System.IO;

public class DoubleVerification : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string EncryptedReq = string.Empty;
        string DecryptedReq = string.Empty;
        string ReturnUrl = string.Empty;
        string RequestForm = string.Empty;
        string IPAddress =  string.Empty;//
        string Response=  string.Empty;
        IrctcRdsPayment RDS = new IrctcRdsPayment();

        try
        {
            RequestForm = context.Request.Form.ToString();
            EncryptedReq = context.Request.Form["encdata"];
        }
        catch(Exception ex)
        {
            int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX1", ex.Message, ex.StackTrace.ToString() + " ,RequestForm-" + RequestForm + "  ,EncryptedReq" + EncryptedReq);
        }

        if (!string.IsNullOrEmpty(EncryptedReq))
        {

            try
            {
                IPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (IPAddress == "" || IPAddress == null)
                    IPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX2", ex.Message, ex.StackTrace.ToString() + ",IPAddress" + IPAddress);
            }

            #region Get URL From  Request.UrlReferrer
            try
            {
                if (string.IsNullOrEmpty(ReturnUrl.Trim()))
                {
                    ReturnUrl = Convert.ToString(context.Request.UrlReferrer.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX3", ex.Message, ex.StackTrace.ToString() + ",ReturnUrl" + ReturnUrl);
            }

            try
            {
                if (string.IsNullOrEmpty(ReturnUrl.Trim()))
                {
                    ReturnUrl = Convert.ToString(context.Request.UrlReferrer.OriginalString);
                }
            }
            catch (Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX4", ex.Message, ex.StackTrace.ToString() + ",ReturnUrl2" + ReturnUrl);
            }

            #endregion Get URL From  Request.UrlReferrer

            try
            {
                InsertDoubleVerificationLog(EncryptedReq, RequestForm, ReturnUrl);
            }
            catch (Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX5", ex.Message, ex.StackTrace.ToString() + ",EncryptedReq-" + EncryptedReq + ",RequestForm-" + RequestForm);
            }
            try
            {
                if (!string.IsNullOrEmpty(EncryptedReq))
                {
                    Response = ReturnDoubleVerificationResponse(EncryptedReq, ReturnUrl, IPAddress);
                }
                else
                {
                    int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "encdata blank", EncryptedReq, RequestForm);
                }
            }
            catch (Exception ex)
            {
                int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EX6", ex.Message, ex.StackTrace.ToString());
            }
        }
        else
        {
             RequestForm = context.Request.Form.ToString();
              EncryptedReq = context.Request.Form["encdata"];
         int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ProcessRequest", "EncData is Empty.", EncryptedReq, RequestForm);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(Response);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public string ReturnDoubleVerificationResponse(string EncryptedReq,string ReturnUrl,string IPAddress)
    {
        EncryptDecryptString ENDE = new EncryptDecryptString();
        IrctcRdsPayment RDS = new IrctcRdsPayment();
        string DecryptedReq = string.Empty;
        string Response = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(EncryptedReq))
            {
                DecryptedReq = ENDE.DecryptString(EncryptedReq);
                string PgMsg = RDS.ParseDoubleVerificationReq(EncryptedReq, DecryptedReq, IPAddress, "false", ReturnUrl);
                try
                {
                    int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ReturnDoubleVerificationResponse", "INSERT-LOG", "PgMsg-" + PgMsg + "~~" + EncryptedReq, "IRCTC_ReturnUrl-" + ReturnUrl + "~~" + DecryptedReq);
                    if (PgMsg.Contains("~") && PgMsg.Split('~').Length > 1)
                    {
                        Response = PgMsg.Split('~')[1];
                    }
                }
                catch(Exception ex)
                {
                    int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "ReturnDoubleVerificationResponse", "RDSVERIFICATION1_EXC", ex.Message + "Request_Form" + DecryptedReq, "IRCTC_ReturnUrl-" + ReturnUrl +",Encdata: "+ EncryptedReq + "~~" + ex.StackTrace.ToString());
                }
            }
            else
            {

            }

        }
        catch(Exception ex)
        {
            int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "Check Response", "RDSVERIFICATION1_EXC", ex.Message + "Request_Form" + DecryptedReq, "IRCTC_ReturnUrl-" + ReturnUrl +",Encdata: "+ EncryptedReq + "~~" + ex.StackTrace.ToString());
            //int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "Check Response", "RDSVERIFICATION1", "PgMsg-" + PgMsg + "~~" + EncryptedReq, "IRCTC_ReturnUrl-" + ReturnUrl + "~~" + DecryptedReq);
        }
        return Response;

    }

    public void InsertDoubleVerificationLog(string Encdata,string RequestForm,string ReturnUrl)
    {
        IrctcRdsPayment RDS = new IrctcRdsPayment();
        try
        {
            //G:\TFS Projects\ITQ_B2B_FWS\SourceCode\Staging\FWS\localhost_56359\DoubleVerification.ashx
            int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "InsertDoubleVerificationLog", "RDSVERIFICATION1", Encdata, "IRCTC_ReturnUrl-" + ReturnUrl+"~~"+ RequestForm);
        }
        catch(Exception ex)
        {
            int flag = RDS.InsertExceptionLog("RDS-IRCTC", "DoubleVerification.ashx", "InsertDoubleVerificationLog", "RDSVERIFICATION1", ex.Message, ex.StackTrace.ToString());
        }

    }
}