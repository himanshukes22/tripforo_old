


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITZLib;
using System.Data.SqlClient;
using System.Data;
//using GRP_Booking;
using PG;
using PaytmWall;

public partial class BS_BUSPayment : System.Web.UI.Page
{
    string RequestID = "", UserID = "", Fare = "", QSTrip = "", amtbeforded = "";
    _GetBalance objParamBal = new _GetBalance();
    GetBalanceResponse objBalResp = new GetBalanceResponse();
    ITZGetbalance objItzBal = new ITZGetbalance();
    _CrOrDb objParamDeb = new _CrOrDb();
    DebitResponse objDebResp = new DebitResponse();
    ITZcrdb objItzTrans = new ITZcrdb();
    Itz_Trans_Dal objItzT = new Itz_Trans_Dal();
    ITZ_Trans objIzT = new ITZ_Trans();
    SqlTransactionDom objSqlDom = new SqlTransactionDom();
    DataSet AgencyDs = new DataSet();
    SqlTransaction objDA = new SqlTransaction();
    bool inst = false;
    string mrchntKey = ConfigurationManager.AppSettings["MerchantKey"].ToString();
    DataSet CustInfoDS = new DataSet();
   // GroupBooking ObjGB = new GroupBooking();


    string TripType = "", Trip = "", AdultCount = "", ChildCount = "", InfantCount = "", Remarks = "", ExpactedFare = "", Jdate = "", ProbDays = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack != true)
            {
                string userid = Request.QueryString["GRPUserID"];
                if (Session["UID"] == null || Session["UID"].ToString() == "" || userid != Session["UID"].ToString())
                {
                    if (userid != Session["UID"].ToString())
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
                    }
                    else
                    {
                        Response.Redirect("../GroupBookingLogin.aspx?userid=" + Request.QueryString["GRPUserID"] + "&fare=" + Request.QueryString["Fare"] + "&RequestID=" + Request.QueryString["RequestID"] + "&Trip=" + Request.QueryString["Trip"] + "");
                    }
                }
                else
                {
                    RequestID = Request.QueryString["RequestID"];
                    UserID = Request.QueryString["GRPUserID"];
                    Fare = Request.QueryString["Fare"];
                    QSTrip = Request.QueryString["TT"];
                    lbl_requestid.Text = RequestID;
                    lbl_userid.Text = UserID;
                    lbl_bookingfare.Text = Fare;
                    hdnbookprice.Value = Fare;
                   // CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "PAYBLECUSTOMERINFO", "", "");
                    Div_Agent_FinalBookingDetails.Visible = true;
                    finalBookingGrid.DataSource = CustInfoDS.Tables[0];
                    finalBookingGrid.DataBind();
                    TripType = CustInfoDS.Tables[0].Rows[0]["TripType"].ToString();
                    Trip = CustInfoDS.Tables[0].Rows[0]["Trip"].ToString();
                    AdultCount = CustInfoDS.Tables[0].Rows[0]["AdultCount"].ToString();
                    ChildCount = CustInfoDS.Tables[0].Rows[0]["ChildCount"].ToString();
                    InfantCount = CustInfoDS.Tables[0].Rows[0]["InfantCount"].ToString();
                    Remarks = CustInfoDS.Tables[0].Rows[0]["Remarks"].ToString();
                    ExpactedFare = CustInfoDS.Tables[0].Rows[0]["ExpactedPrice"].ToString();
                    Jdate = CustInfoDS.Tables[0].Rows[0]["CreatedDate"].ToString();
                    ProbDays = CustInfoDS.Tables[0].Rows[0]["ProbableDays"].ToString();
                    FBtxt_totAdt.Text = AdultCount;
                    FBtxt_totchd.Text = ChildCount;
                    FBtxt_totinf.Text = InfantCount;
                    FBtxt_remarks.Text = Remarks;
                    FBtxt_fare.Text = ExpactedFare;
                    if (Trip.ToUpper() == "D")
                    {
                        lbltrip.Text = "Domestic";
                    }
                    else
                    {
                        lbltrip.Text = "International";
                    }
                    if (TripType.ToUpper() == "O")
                    {
                        lbltriptyp.Text = "Oneway Trip";
                    }
                    else
                    {
                        lbltriptyp.Text = "Round Trip";
                    }
                }
            }
        }
        catch (Exception ex)
        {
           // ErrorLogTrace.WriteErrorLog(ex, "GroupPaymentPageLoad");
        }
    }
    protected void btn_payment_Click(object sender, EventArgs e)
    {
        RequestID = Request.QueryString["RequestID"];
        Fare = hdntotal.Value.Trim().ToString();
        if (Fare == "")
        {
            Fare = Request.QueryString["Fare"].ToString();
        }
        Trip = Request.QueryString["TT"];
        UserID = Request.QueryString["GRPUserID"];
        AgencyDs = objDA.GetAgencyDetails(Session["UID"].ToString());
        string PGVAL = rblPaymentMode.SelectedValue.ToString();
        if (PGVAL == "WL")
        {
            try
            {
                objParamBal._DCODE = (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : "");
                objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
                objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : "");
                objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : "");
                objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
            }
            catch (Exception ex)
            {
               // ErrorLogTrace.WriteErrorLog(ex, "GroupPaymentGetBalanceCustomer");
            }
            try
            {
                if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL != null)
                {
                    if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient balance,please contact with admin!!');window.opener.location.reload(true);self.close();", true);
                    }
                    else
                    {
                        if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL.Length > 0)
                        {
                            if (Convert.ToDouble(Fare) <= Convert.ToDouble(objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE.Trim()))
                            {
                                amtbeforded = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
                                try
                                {
                                    objParamDeb._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
                                    objParamDeb._PASSWORD = ((Session["_PASSWORD"] != null) ? Session["_PASSWORD"].ToString().Trim() : "");
                                    objParamDeb._DECODE = ((Session["_DCODE"] != null) ? Session["_DCODE"].ToString().Trim() : "");
                                    objParamDeb._AMOUNT = Fare;
                                    objParamDeb._MODE = (Session["ModeTypeITZ"] != null ? Session["ModeTypeITZ"].ToString().Trim() : "");
                                    objParamDeb._ORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID : "");
                                    objParamDeb._DESCRIPTION = "Amount deducted against booking Group RequestID-" + RequestID;
                                    string stringtoenc = "MERCHANTKEY=" + objParamDeb._MERCHANT_KEY + "&ORDERID=" + objParamDeb._ORDERID + "&AMOUNT=" + objParamDeb._AMOUNT + "&MODE=" + objParamDeb._MODE;
                                    objParamDeb._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc);
                                    objParamDeb._SERVICE_TYPE = (Session["_SvcTypeITZ"] != null ? Session["_SvcTypeITZ"].ToString().Trim() : "");
                                    objDebResp = objItzTrans.ITZDebit(objParamDeb);
                                }
                                catch (Exception ex)
                                {
                                    //ErrorLogTrace.WriteErrorLog(ex, "ITZDebit");
                                }
                                try
                                {
                                    objItzT = new Itz_Trans_Dal();
                                    objIzT.AMT_TO_DED = Fare;
                                    objIzT.AMT_TO_CRE = "0";
                                    objIzT.B2C_MBLNO_ITZ = (objDebResp.B2C_MOBILENO != null ? objDebResp.B2C_MOBILENO : "");
                                    objIzT.COMMI_ITZ = (objDebResp.COMMISSION != null ? objDebResp.COMMISSION : "");
                                    objIzT.CONVFEE_ITZ = (objDebResp.CONVENIENCEFEE != null ? objDebResp.CONVENIENCEFEE : "");
                                    objIzT.DECODE_ITZ = (objDebResp.DECODE != null && !string.IsNullOrEmpty(objDebResp.DECODE) && objDebResp.DECODE != "" ? objDebResp.DECODE : (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : ""));
                                    objIzT.EASY_ORDID_ITZ = (objDebResp.EASY_ORDER_ID != null ? objDebResp.EASY_ORDER_ID : "");
                                    objIzT.EASY_TRANCODE_ITZ = (objDebResp.EASY_TRAN_CODE != null ? objDebResp.EASY_TRAN_CODE : "");
                                    objIzT.ERROR_CODE = (objDebResp.ERROR_CODE != null ? objDebResp.ERROR_CODE : "");
                                    objIzT.MERCHANT_KEY_ITZ = (objDebResp.MERCHANT_KEY != null && !string.IsNullOrEmpty(objDebResp.MERCHANT_KEY) && objDebResp.MERCHANT_KEY != "" ? objDebResp.MERCHANT_KEY : (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : ""));
                                    objIzT.MESSAGE_ITZ = (objDebResp.MESSAGE != null ? objDebResp.MESSAGE : "");
                                    objIzT.ORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID : "");
                                    objIzT.RATE_GROUP_ITZ = (objDebResp.RATEGROUP != null ? objDebResp.RATEGROUP : "");
                                    objIzT.REFUND_TYPE_ITZ = "";
                                    objIzT.SERIAL_NO_FROM = (objDebResp.SERIALNO_FROM != null ? objDebResp.SERIALNO_FROM : "");
                                    objIzT.SERIAL_NO_TO = (objDebResp.SERIALNO_TO != null ? objDebResp.SERIALNO_TO : "");
                                    objIzT.SVC_TAX_ITZ = (objDebResp.SERVICETAX != null ? objDebResp.SERVICETAX : "");
                                    objIzT.TDS_ITZ = (objDebResp.TDS != null ? objDebResp.TDS : "");
                                    objIzT.TOTAL_AMT_DED_ITZ = (objDebResp.TOTALAMOUNT != null ? objDebResp.TOTALAMOUNT : "");
                                    objIzT.TRANS_TYPE = "GRP";
                                    objIzT.USER_NAME_ITZ = (objDebResp.USERNAME != null && !string.IsNullOrEmpty(objDebResp.USERNAME) && objDebResp.USERNAME != "" ? objDebResp.USERNAME : (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : ""));
                                    try
                                    {
                                        objBalResp = new GetBalanceResponse();
                                        objParamBal._DCODE = (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : "");
                                        objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
                                        objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : "");
                                        objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : "");
                                        objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
                                        objIzT.ACCTYPE_NAME_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME : "");
                                        objIzT.AVAIL_BAL_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
                                    }
                                    catch (Exception ex)
                                    {
                                       // ErrorLogTrace.WriteErrorLog(ex, "ITZDebit");
                                    }
                                    inst = objItzT.InsertItzTrans(objIzT);
                                }
                                catch (Exception ex)
                                {
                                    //ErrorLogTrace.WriteErrorLog(ex, "ITZDebit");
                                }
                                if (objDebResp != null)
                                {
                                    if (objDebResp.MESSAGE.Trim().ToLower().Contains("successfully execute"))
                                    {
                                        int Result = 0;
                                        Result = objSqlDom.insertLedgerDetails(Session["UID"].ToString(), AgencyDs.Tables[0].Rows[0]["Agency_Name"].ToString(), RequestID, RequestID, "", "", "", "", "", Request.UserHostAddress.ToString(), Convert.ToDouble(Fare), 0, Convert.ToDouble(amtbeforded.Trim()), "GroupBooking", "GroupBooking", 0, "", "", "");
                                      //  ObjGB.UPDATEPAYMENTSTATUS(RequestID, Session["UID"].ToString(), "UPDATED", "Wallet");
                                        string Payment = Request.QueryString["Payment"].ToString();
                                        if (Request.QueryString["TT"].ToString() == "I")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment has been done,please provide the pax details!!');window.location ='../GroupSearch/CustomerInfoIntl.aspx?RefRequestID=" + RequestID + "&Fare=" + Fare + "&GRPUserID=" + Session["UID"].ToString() + "&Trip=" + Trip + "&Status=PAID&Payment=" + Payment + "&PG=N';", true);
                                        }
                                        else if (Request.QueryString["TT"].ToString() == "D")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment has been done,please provide the pax details!!');window.location ='../GroupSearch/CustomerInfoDom.aspx?RefRequestID=" + RequestID + "&Fare=" + Fare + "&GRPUserID=" + Session["UID"].ToString() + "&Trip=" + Trip + "&Status=PAID&Payment=" + Payment + "&PG=N';", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment has been done succesfully,will get back to you soon!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + RequestID + "';", true);
                                        }
                                    }
                                    else
                                    {
                                        if (objDebResp.MESSAGE.ToString() == "Order Id already exist  ")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment already done!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + RequestID + "';", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + objDebResp.MESSAGE.ToString() + "');", true);
                                           // CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "CUSTOMERINFO", "", "");
                                            Div_Agent_FinalBookingDetails.Visible = true;
                                            finalBookingGrid.DataSource = CustInfoDS.Tables[0];
                                            finalBookingGrid.DataBind();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(Somthing went worng please try after sometime!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + RequestID + "';", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // ErrorLogTrace.WriteErrorLog(ex, "VAL_ACCOUNT_TYPE_DETAIL");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Somthing went worng please try after sometime!!');window.opener.location.reload(true);self.close();", true);
            }
        }
        else
        {
            try { 
            string ipAddress = null;
            string PgMsg = "";
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            string ReferenceNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            string Tid = ReferenceNo.Substring(4, 16);
            PG.PaymentGateway objPg = new PG.PaymentGateway();
            PaytmWall.PaytmTrans objpt = new PaytmWall.PaytmTrans();


            if (rblPaymentMode.SelectedValue.ToString() == "Paytm")
            {
                PgMsg = objpt.PaymentGatewayReqPaytm(RequestID, Tid, "", Session["UID"].ToString(), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(Request.QueryString["Fare"]), Convert.ToDouble(Request.QueryString["Fare"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]),
                     Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
                   Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "GroupBooking", ipAddress, Request.QueryString["TT"].ToString(), PGVAL);
           
            }
            else
            {
                PgMsg = objPg.PaymentGatewayReqPayU(RequestID, Tid, "", Session["UID"].ToString(), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(Request.QueryString["Fare"]), Convert.ToDouble(Request.QueryString["Fare"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]),
                 Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
               Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "GroupBooking", ipAddress, Request.QueryString["TT"].ToString(), PGVAL);
           
            }
           
                if (PgMsg.Split('~')[0] == "yes")
            {
                //Response.Redirect("../PaymentGateway.aspx?OBTID=" + RequestID + "", false);
                if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                {
                    string strForm = PgMsg.Split('~')[1];
                    Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Somthing went worng please try after sometime   -PGH001!!!!');window.opener.location.reload(true);self.close();", true);                    
                }               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Somthing went worng please try after sometime!!');window.opener.location.reload(true);self.close();", true);                
            }
            }
            catch (Exception ex)
            {
                 GRP_Booking.ErrorLogTrace.WriteErrorLog(ex, "Redirect on PayU");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Somthing went worng please try after sometime!!');window.opener.location.reload(true);self.close();", true);
            }
        }
    }
    [System.Web.Services.WebMethod()]
    public static string GetPgChargeByMode(string paymode)
    {
        string TransCharge = "0~P";
        //string PgCharge = "0";
        //string ChargeType = "0";
        //PaymentGateway objP = new PaymentGateway();
        //try
        //{
        //    DataTable pgDT = objP.GetPgTransChargesByMode(paymode, "GetPgCharges");
        //    if (pgDT != null)
        //    {
        //        if (pgDT.Rows.Count > 0)
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["Charges"])))
        //            {
        //                PgCharge = Convert.ToString(pgDT.Rows[0]["Charges"]).Trim();
        //            }
        //            else
        //            {
        //                PgCharge = "0";
        //            }
        //            if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["ChargesType"])))
        //            {
        //                ChargeType = Convert.ToString(pgDT.Rows[0]["ChargesType"]).Trim();
        //            }
        //            else
        //            {
        //                ChargeType = "P";
        //            }
        //            TransCharge = PgCharge + "~" + ChargeType;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //   // ErrorLogTrace.WriteErrorLog(ex, "GetPgChargeByMode");
        //}
        return TransCharge;
    }
}
