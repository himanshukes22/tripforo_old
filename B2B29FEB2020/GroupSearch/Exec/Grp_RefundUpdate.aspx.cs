using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITZLib;
using GRP_Booking;
using PG;
public partial class GroupSearch_Exec_Grp_RefundUpdate : System.Web.UI.Page
{
    DataSet DSCancel = new DataSet();
    GroupBooking GB = new GroupBooking();
    SqlTransaction ST = new SqlTransaction();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    PG.PaymentGateway pgrfnd = new PG.PaymentGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Session["UID"] == null || Session["UID"].ToString() == "")
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //    Request.QueryString["RequestID"].ToString();
        //    Request.QueryString["AgentID"].ToString();
        //    if (IsPostBack != true)
        //    {
        //        DataSet AgencyDS = new DataSet();
        //        AgencyDS = ST.GetAgencyDetails(Request.QueryString["AgentID"].ToString());
        //        if (AgencyDS.Tables[0].Rows.Count != 0)
        //        {
        //            DataTable dt1 = new DataTable();
        //            dt1 = AgencyDS.Tables[0];
        //            td_AgentID.InnerText = Request.QueryString["AgentID"].ToString();
        //            td_AgentAddress.InnerText = dt1.Rows[0]["Address"].ToString() + ", " + dt1.Rows[0]["city"].ToString() + ", " + dt1.Rows[0]["State"].ToString() + ", " + dt1.Rows[0]["country"].ToString() + ", " + dt1.Rows[0]["zipcode"].ToString();
        //            td_AgentMobNo.InnerText = dt1.Rows[0]["Mobile"].ToString();
        //            td_Email.InnerText = dt1.Rows[0]["Email"].ToString();
        //            td_AgencyName.InnerText = dt1.Rows[0]["Agency_Name"].ToString();
        //            td_CardLimit.InnerText = dt1.Rows[0]["Crd_Limit"].ToString();
        //        }

        //        DSCancel = RefundRequest(Request.QueryString["RequestID"].ToString(), "Refund");
        //        GridRefundRequest.DataSource = DSCancel;
        //        GridRefundRequest.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ErrorLogTrace.WriteErrorLog(ex, "LoadRefundUpdate");
        //}
    }
    //public DataSet RefundRequest(string Requestid, string CMD_TYPE)
    //{
    //    DataSet ds = new DataSet();
    //   // ds = GB.REFUNDREQUEST(Requestid, CMD_TYPE);
    //    return ds;
    //}
    //protected void btn_Update_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string PaymentMode = "";
    //        double CnlAmt = 0.00, CnlChg = 0.00, BkgAmt = 0.00;
    //        double RefundAmount = 0.00;
    //        bool Result = false;
    //        string PGResult = "";
    //        string refid = Request.QueryString["REFNDID"].ToString();
    //        DSCancel = RefundRequest(Request.QueryString["RequestID"].ToString(), "Refund");
    //        CnlAmt = Convert.ToDouble(txt_Reissue_charge.Value.Trim().ToString());
    //        CnlChg = Convert.ToDouble(txt_Service_charge.Value.Trim().ToString());
    //        BkgAmt = Convert.ToDouble(DSCancel.Tables[0].Rows[0]["BookingFare"].ToString());
    //        if (BkgAmt > (CnlAmt + CnlChg))
    //        {
    //            RefundAmount = BkgAmt - (CnlAmt + CnlChg);
    //            if (DSCancel.Tables[0].Rows.Count > 0)
    //            {
    //                PaymentMode = DSCancel.Tables[0].Rows[0]["PaymentMode"].ToString();
    //                if (PaymentMode == "PG")
    //                {
    //                    try
    //                    {
    //                        double pgcharge = 0.00;
    //                        pgcharge = Convert.ToDouble(DSCancel.Tables[0].Rows[0]["PGCharges"].ToString());
    //                        PGResult = pgrfnd.PgRefundAmount(Request.QueryString["RequestID"].ToString(), "GroupBooking", DSCancel.Tables[0].Rows[0]["Trip"].ToString(), RefundAmount, Request.QueryString["AgentID"].ToString(), txtRemark.Value.Trim().ToString(), Session["UID"].ToString(), "GroupBookingRefund", pgcharge, "");
    //                        if (PGResult == "Refunded")
    //                        {

    //                            GB.UpdateRefundAmt(Request.QueryString["RequestID"].ToString(), txtRemark.Value.Trim().ToString(), Session["UID"].ToString(), "Refunded", Convert.ToDecimal(txt_Reissue_charge.Value.Trim().ToString()), Convert.ToDecimal(txt_Service_charge.Value.Trim().ToString()), Convert.ToDecimal(RefundAmount));
    //                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" + PGResult + "');javascript:window.close();window.location='GrpRefundRequestInprocess.aspx';", true);
    //                        }
    //                        else if (PGResult != "Already refunded" || PGResult != "Refunded")
    //                        {
    //                            GB.UPdateStatusREFUNDREQUEST(Request.QueryString["RequestID"].ToString(), "RefundRequested", txtRemark.Value.Trim().ToString(), Session["UID"].ToString());
    //                        }
    //                        else if (PGResult == "Already refunded")
    //                        {
    //                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('" + PGResult + "');javascript:window.close();window.location='GrpRefundRequestInprocess.aspx';", true);
    //                        }
    //                        else
    //                        {
    //                            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('something went wrong ,please try after sometime!!');javascript:window.close();window.location='GrpRefundRequestInprocess.aspx';", true);
    //                        }
    //                        //DSCancel = RefundRequest(Request.QueryString["RequestID"].ToString(), "Refund");
    //                        //GridRefundRequest.DataSource = DSCancel;
    //                        //GridRefundRequest.DataBind();
    //                        int i, j;
    //                        i = AgentMailSending(Request.QueryString["RequestID"].ToString(), Convert.ToString(RefundAmount), refid);
    //                        j = AdminNExecMailSending(Request.QueryString["RequestID"].ToString(), Convert.ToString(RefundAmount), refid);
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        ErrorLogTrace.WriteErrorLog(ex, "RefundPaymentMode");
    //                        Response.Write("<script>javascript:window.close();</script>");
    //                    }
    //                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
    //                }
    //                else
    //                {
    //                    Result = AutoRefund(Request.QueryString["RequestID"].ToString(), "Refunded", RefundAmount, Request.QueryString["AgentID"].ToString(), DSCancel.Tables[0].Rows[0]["BookingFare"].ToString(), refid);
    //                    if (Result == true)
    //                    {
    //                        int i, j;
    //                        i = AgentMailSending(Request.QueryString["RequestID"].ToString(), Convert.ToString(RefundAmount), refid);
    //                        j = AdminNExecMailSending(Request.QueryString["RequestID"].ToString(), Convert.ToString(RefundAmount), refid);
    //                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
    //                    }
    //                    else
    //                    {
    //                        ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please try after sometime'!!);javascript:window.close();", true);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cancellation amount should be less then payable amount!!');", true);
    //            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
    //            txt_Reissue_charge.Value = "";
    //            txt_Service_charge.Value = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogTrace.WriteErrorLog(ex, "UpdateRedunfUpdate_Click");
    //    }
    //}
    //protected bool AutoRefund(string RequestID, string Status, double RefundAmount, string AgentID, string BookingAmount,string refid)
    //{
    //    bool Rfndstatus = false;
    //    RefundResponse objRefnResp = new RefundResponse();
    //    _CrOrDb objParamCrd = new _CrOrDb();
    //    ITZcrdb objCrd = new ITZcrdb();
    //    ITZGetbalance objItzBal = new ITZGetbalance();
    //    _GetBalance objParamBal = new _GetBalance();
    //    GetBalanceResponse objBalResp = new GetBalanceResponse();
    //    SqlTransaction ST = new SqlTransaction();
    //    SqlTransactionDom STDom = new SqlTransactionDom();
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    //    try
    //    {
    //        DataSet AgencyDS = new DataSet();
    //        AgencyDS = ST.GetAgencyDetails(Request.QueryString["AgentID"].ToString());
    //        string ChecksSatus = Status;
    //        Itz_Trans_Dal objItzT = new Itz_Trans_Dal();
    //        bool inst = false;
    //        ITZ_Trans objIzT = new ITZ_Trans();
    //        double ablBalance = 0;
    //        if ((ChecksSatus == "Refunded"))
    //        {
    //            double Aval_Bal = ST.AddCrdLimit(AgentID, RefundAmount);
    //            RandomKeyGenerator rndnum = new RandomKeyGenerator();
    //            string numRand = rndnum.Generate();
    //            try
    //            {
    //                objParamCrd._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
    //                objParamCrd._AMOUNT = (Convert.ToString(RefundAmount) != null ? Convert.ToString(RefundAmount) : "0");
    //                objParamCrd._ORDERID = (numRand != null && !string.IsNullOrEmpty(numRand) ? numRand.Trim() : " ");
    //                objParamCrd._REFUNDORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID.Trim() : "");
    //                objParamCrd._MODE = (Session["ModeTypeITZ"] != null ? Session["ModeTypeITZ"].ToString().Trim() : " ");
    //                objParamCrd._REFUNDTYPE = "P";
    //                string stringtoenc = "MERCHANTKEY=" + objParamCrd._MERCHANT_KEY + "&ORDERID=" + objParamCrd._ORDERID + "&REFUNDTYPE=" + objParamCrd._REFUNDTYPE;
    //                objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc);
    //                objParamCrd._DESCRIPTION = "Refund to agent -" + AgentID + " against requestID-" + RequestID.ToString();
    //                objRefnResp = objCrd.ITZRefund(objParamCrd);
    //                if (objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute"))
    //                {
    //                    Rfndstatus = true;
    //                    STDom.insertLedgerDetails(AgentID, AgencyDS.Tables[0].Rows[0]["Agency_Name"].ToString(), RequestID, refid, "", "", (objRefnResp.EASY_ORDER_ID != null ? objRefnResp.EASY_ORDER_ID : ""), "", Session["UID"].ToString(), Request.UserHostAddress,
    //                    0, RefundAmount, Aval_Bal, "Group booking Auto Rejection", "Refund Against  RequestID=" + RequestID, 0);
    //                    GB.UpdateRefundAmt(RequestID, txtRemark.Value.Trim().ToString(), Session["UID"].ToString(), "Refunded", Convert.ToDecimal(txt_Reissue_charge.Value.Trim().ToString()), Convert.ToDecimal(txt_Service_charge.Value.Trim().ToString()), Convert.ToDecimal(RefundAmount));
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                ErrorLogTrace.WriteErrorLog(ex, "RefundAmount");
    //            }
    //            objItzT = new Itz_Trans_Dal();
    //            try
    //            {
    //                objIzT.AMT_TO_DED = "0";
    //                objIzT.AMT_TO_CRE = (Convert.ToString(RefundAmount) != null ? Convert.ToString(RefundAmount) : "0");
    //                objIzT.B2C_MBLNO_ITZ = " ";
    //                objIzT.COMMI_ITZ = " ";
    //                objIzT.CONVFEE_ITZ = " ";
    //                objIzT.DECODE_ITZ = (AgentID != null ? AgentID.Trim() : "");
    //                objIzT.EASY_ORDID_ITZ = (objRefnResp.EASY_ORDER_ID != null ? objRefnResp.EASY_ORDER_ID : "");
    //                objIzT.EASY_TRANCODE_ITZ = (objRefnResp.EASY_TRAN_CODE != null ? objRefnResp.EASY_TRAN_CODE : "");
    //                objIzT.ERROR_CODE = (objRefnResp.ERROR_CODE != null ? objRefnResp.ERROR_CODE : "");
    //                objIzT.MERCHANT_KEY_ITZ = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
    //                objIzT.MESSAGE_ITZ = (objRefnResp.MESSAGE != null ? objRefnResp.MESSAGE : "");
    //                objIzT.ORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID.Trim() : "");
    //                objIzT.RATE_GROUP_ITZ = "";
    //                objIzT.REFUND_TYPE_ITZ = (objRefnResp.REFUND_TYPE != null && !string.IsNullOrEmpty(objRefnResp.REFUND_TYPE) && objRefnResp.REFUND_TYPE != "" ? objRefnResp.REFUND_TYPE : "");
    //                objIzT.SERIAL_NO_FROM = "";
    //                objIzT.SERIAL_NO_TO = "";
    //                objIzT.SVC_TAX_ITZ = "";
    //                objIzT.TDS_ITZ = "";
    //                objIzT.TOTAL_AMT_DED_ITZ = "";
    //                objIzT.TRANS_TYPE = "REFUND";
    //                objIzT.USER_NAME_ITZ = (AgentID != null ? AgentID : "");
    //                try
    //                {
    //                    objBalResp = new GetBalanceResponse();
    //                    objParamBal._DCODE = (AgentID != null ? AgentID : "");
    //                    objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : " ");
    //                    objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : "");
    //                    objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : "");
    //                    objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
    //                    objIzT.ACCTYPE_NAME_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME : "");
    //                    objIzT.AVAIL_BAL_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
    //                    Session["CL"] = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
    //                    ablBalance = Convert.ToDouble((objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "0"));
    //                }
    //                catch (Exception ex)
    //                {
    //                    ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
    //                }
    //                inst = objItzT.InsertItzTrans(objIzT);
    //            }
    //            catch (Exception ex)
    //            {
    //                ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
    //    }
    //    return Rfndstatus;
    //}
    //protected int AgentMailSending(string RefRequestID, string refundamount, string refid)
    //{
    //    int i = 0;
    //    try
    //    {
    //        DataSet MailDt = new DataSet();
    //        DataSet MailContent = new DataSet();
    //        DataSet DSStatus = new DataSet();
    //        DataSet mailmsg = new DataSet();
    //        string strMailMsg = "", STRSUB = "", MAILHEADER = "", MAILMESSAGE = "", _cmdtype = "", StrMail = "";
    //        DataSet AgencyDS = new DataSet();
    //        SqlTransaction ST = new SqlTransaction();
    //        DSStatus = GB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
    //        _cmdtype = Convert.ToString(DSStatus.Tables[0].Rows[0]["Status"]).ToUpper();
    //        mailmsg = GB.GRP_MAILMSGSUBJECT("RefundDone", "AGENT");
    //        STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
    //        MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
    //        MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
    //        MailContent = GB.GroupRequestDetails(RefRequestID, "MAILCONTENTAGENT", "", "");
    //        string REFSNO = Convert.ToString(MailContent.Tables[0].Rows[0]["REFSNO"]);
    //        if (DSStatus.Tables[0].Rows.Count > 0)
    //        {
    //            StrMail = GB.GETEMAILID(RefRequestID, "AGENT");
    //            strMailMsg += "<table style='border: 1px width:100%;'>";
    //            strMailMsg += "<tr>";
    //            strMailMsg += "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
    //            strMailMsg += "<h2> " + MAILHEADER + " </h2>";
    //            strMailMsg += "</td>";
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr>";
    //            strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
    //            strMailMsg += "</td>";
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
    //            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
    //            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
    //            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REFUND ID</td>";
    //            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REFUND AMOUNT</td>";
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
    //            strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + refid + "</td>";
    //            strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + refundamount + "</td>";
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //            if (MailContent.Tables[0].Rows.Count > 0)
    //            {
    //                strMailMsg += "<table style='border: 1px width:100%;'>";
    //                strMailMsg += "<tr>";
    //                strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
    //                strMailMsg += "";
    //                strMailMsg += "</td>";
    //                strMailMsg += "</tr>";
    //                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
    //                //strMailMsg += "<td>REQUESTID</td>";
    //                strMailMsg += "<td>TRIP FROM</td>";
    //                strMailMsg += "<td>TRIP TO</td>";
    //                strMailMsg += "<td>DEP. DATE</td>";
    //                strMailMsg += "<td>DEP TIME</td>";
    //                strMailMsg += "<td>ARVL DATE</td>";
    //                strMailMsg += "<td>ARVL TIME</td>";
    //                strMailMsg += "<td>AIRLINE</td>";
    //                strMailMsg += "<td>FLIGHT NO.</td>";
    //                strMailMsg += "</tr>";
    //                DataTable DT = new DataTable();
    //                DT = MailContent.Tables[1].Select(String.Format("SNO = '{0}'", REFSNO)).CopyToDataTable();
    //                if (DT.Rows.Count > 0)
    //                {
    //                    for (int j = 0; j < DT.Rows.Count; j++)
    //                    {
    //                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //                        //strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["RequestID"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Location"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Location"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Date"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Time"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Date"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Time"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Aircode"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["FlightNumber"]) + "</td>";
    //                        strMailMsg += "</tr>";
    //                    }
    //                    strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //                }
    //                strMailMsg += "</table>";
    //                strMailMsg += "<table style='width:100%;'>";
    //                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
    //                strMailMsg += "<td>TRIP TYPE</td>";
    //                strMailMsg += "<td>TRIP</td>";
    //                strMailMsg += "<td>TOTAL PASSANGERS</td>";
    //                strMailMsg += "<td>ADULT</td>";
    //                if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
    //                {
    //                    strMailMsg += "<td>CHILD</td>";
    //                }
    //                if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
    //                {
    //                    strMailMsg += "<td>INFANT</td>";
    //                }
    //                strMailMsg += "</tr>";

    //                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["TripType"]) + "</td>";
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Trip"]) + "</td>";
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["NoOfPax"]) + "</td>";
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["AdultCount"]) + "</td>";
    //                if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
    //                {
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ChildCount"]) + "</td>";
    //                }
    //                if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
    //                {
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["InfantCount"]) + "</td>";
    //                }
    //                strMailMsg += "</tr>";
    //                strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //                strMailMsg += "</table>";
    //                if (MailContent.Tables[2].Rows.Count > 0)
    //                {
    //                    strMailMsg += "<table style='width:100%;'>";
    //                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
    //                    //strMailMsg += "<td>REQUESTID</td>";
    //                    strMailMsg += "<td>NAME</td>";
    //                    strMailMsg += "<td>PAX TYPE</td>";
    //                    strMailMsg += "<td>DOB</td>";
    //                    strMailMsg += "<td>GENDER</td>";
    //                    if (MailContent.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "international" && MailContent.Tables[2].Rows.Count > 0)
    //                    {
    //                        strMailMsg += "<td>PASSPORT NO.</td>";
    //                        strMailMsg += "<td>ISSUE COUNTRY</td>";
    //                        strMailMsg += "<td>NATIONALITY</td>";
    //                        strMailMsg += "<td>EX.DATE</td>";
    //                    }
    //                    strMailMsg += "</tr>";
    //                    int countpax = MailContent.Tables[2].Rows.Count;
    //                    for (int k = 0; k < countpax; k++)
    //                    {
    //                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //                        //strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["RequestID"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["Title"].ToString() + " " + MailContent.Tables[2].Rows[k]["FName"].ToString() + " " + MailContent.Tables[2].Rows[k]["LName"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PaxType"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["DOB"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["Gender"]) + "</td>";
    //                        if (MailContent.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "international")
    //                        {
    //                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PassportNo"]) + "</td>";
    //                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["IssueCountryCode"]) + "</td>";
    //                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["NationalityCode"]) + "</td>";
    //                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PassportExpireDate"]) + "</td>";
    //                        }
    //                        strMailMsg += "</tr>";
    //                    }
    //                    strMailMsg += "</table>";
    //                }
    //                MailDt = ObjST.GetMailingDetails("GroupBooking", "All");
    //                if (MailDt.Tables[0].Rows.Count > 0)
    //                {
    //                    string EIDStrMail = ConfigurationManager.AppSettings["EmailId"].ToString();
    //                    if (StrMail != "")
    //                    {
    //                        i = GB.SendMail(StrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
    //                    }
    //                    else
    //                    {
    //                        i = GB.SendMail(EIDStrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, "Agent Email-ID not found (Group Booking Details)", "");
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogTrace.WriteErrorLog(ex, "AdminNExecMailSending");
    //    }
    //    return i;
    //}
    //protected int AdminNExecMailSending(string RefRequestID, string Refundamount, string refid)
    //{
    //    int i = 0;
    //    DataSet MailDt = new DataSet();
    //    DataSet MailContent = new DataSet();
    //    DataSet DSStatus = new DataSet();
    //    string strMailMsg = "";
    //    DSStatus = GB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
    //    DataSet mailmsg = new DataSet();
    //    string STRSUB, MAILHEADER, MAILMESSAGE, _cmdtype;
    //    _cmdtype = Convert.ToString(DSStatus.Tables[0].Rows[0]["Status"]).ToUpper();
    //    mailmsg = GB.GRP_MAILMSGSUBJECT("RefundDone", "EXEC");
    //    STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
    //    MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
    //    MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
    //    try
    //    {
    //        MailContent = GB.GroupRequestDetails(RefRequestID, "EXECMAILCONTENT", "", "");
    //        string REFSNO = Convert.ToString(MailContent.Tables[2].Rows[0]["REFSNO"]);
    //        DataSet AgencyDS = new DataSet();
    //        SqlTransaction ST = new SqlTransaction();
    //        AgencyDS = ST.GetAgencyDetails(Convert.ToString(MailContent.Tables[0].Rows[0]["CreatedBy"]));

    //        strMailMsg += "<table style='border: 1px width:100%;'>";
    //        strMailMsg += "<tr>";
    //        strMailMsg += "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
    //        strMailMsg += "<h2> " + MAILHEADER + " </h2>";
    //        strMailMsg += "</td>";
    //        strMailMsg += "</tr>";
    //        strMailMsg += "<tr>";
    //        strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
    //        strMailMsg += "</td>";
    //        strMailMsg += "</tr>";
    //        strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>AGENT ID</td>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>AGENCY NAME</td>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REFUND ID</td>";
    //        strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REFUND AMOUNT</td>";
    //        strMailMsg += "</tr>";
    //        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
    //        strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
    //        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
    //        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
    //        strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + refid + "</td>";
    //        strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + Refundamount + "</td>";
    //        strMailMsg += "</tr>";
    //        strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //        if (MailContent.Tables[0].Rows.Count > 0)
    //        {
    //            strMailMsg += "<table style='border: 1px width:100%;'>";
    //            strMailMsg += "<tr>";
    //            strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
    //            strMailMsg += "";
    //            strMailMsg += "</td>";
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
    //            //strMailMsg += "<td>REQUESTID</td>";
    //            strMailMsg += "<td>TRIP FROM</td>";
    //            strMailMsg += "<td>TRIP TO</td>";
    //            strMailMsg += "<td>DEP. DATE</td>";
    //            strMailMsg += "<td>DEP TIME</td>";
    //            strMailMsg += "<td>ARVL DATE</td>";
    //            strMailMsg += "<td>ARVL TIME</td>";
    //            strMailMsg += "<td>AIRLINE</td>";
    //            strMailMsg += "<td>FLIGHT NO.</td>";
    //            strMailMsg += "</tr>";
    //            DataTable DT = new DataTable();
    //            DT = MailContent.Tables[0].Select(String.Format("SNO = '{0}'", REFSNO)).CopyToDataTable();
    //            if (DT.Rows.Count > 0)
    //            {
    //                for (int j = 0; j < DT.Rows.Count; j++)
    //                {
    //                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //                    //strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["RequestID"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Location"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Location"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Date"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Time"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Date"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Time"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Aircode"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["FlightNumber"]) + "</td>";
    //                    strMailMsg += "</tr>";
    //                }
    //                strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //            }
    //            strMailMsg += "</table>";
    //            strMailMsg += "<table style='width:100%;'>";
    //            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
    //            strMailMsg += "<td>TRIP TYPE</td>";
    //            strMailMsg += "<td>TRIP</td>";
    //            strMailMsg += "<td>TOTAL PASSANGERS</td>";
    //            strMailMsg += "<td>ADULT</td>";
    //            if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["ChildCount"].ToString()) > 0)
    //            {
    //                strMailMsg += "<td>CHILD</td>";
    //            }
    //            if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["InfantCount"].ToString()) > 0)
    //            {
    //                strMailMsg += "<td>INFANT</td>";
    //            }
    //            strMailMsg += "</tr>";

    //            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["TripType"]) + "</td>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["Trip"]) + "</td>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["NoOfPax"]) + "</td>";
    //            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["AdultCount"]) + "</td>";
    //            if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["ChildCount"].ToString()) > 0)
    //            {
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["ChildCount"]) + "</td>";
    //            }
    //            if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["InfantCount"].ToString()) > 0)
    //            {
    //                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["InfantCount"]) + "</td>";
    //            }
    //            strMailMsg += "</tr>";
    //            strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
    //            strMailMsg += "</table>";
    //            if (MailContent.Tables[1].Rows.Count > 0)
    //            {
    //                strMailMsg += "<table style='width:100%;'>";
    //                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
    //                //strMailMsg += "<td>REQUESTID</td>";
    //                strMailMsg += "<td>NAME</td>";
    //                strMailMsg += "<td>PAX TYPE</td>";
    //                strMailMsg += "<td>DOB</td>";
    //                strMailMsg += "<td>GENDER</td>";
    //                if (MailContent.Tables[2].Rows[0]["Trip"].ToString() == "I")
    //                {
    //                    strMailMsg += "<td>PASSPORT NO.</td>";
    //                    strMailMsg += "<td>ISSUE COUNTRY</td>";
    //                    strMailMsg += "<td>NATIONALITY</td>";
    //                    strMailMsg += "<td>EX.DATE</td>";
    //                }
    //                strMailMsg += "</tr>";
    //                int countpax = MailContent.Tables[1].Rows.Count;
    //                for (int k = 0; k < countpax; k++)
    //                {
    //                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
    //                    //strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["RequestID"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["Title"].ToString() + " " + MailContent.Tables[1].Rows[k]["FName"].ToString() + " " + MailContent.Tables[1].Rows[k]["LName"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PaxType"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["DOB"]) + "</td>";
    //                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["Gender"]) + "</td>";
    //                    if (MailContent.Tables[2].Rows[0]["Trip"].ToString() == "I")
    //                    {
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PassportNo"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["IssueCountryCode"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["NationalityCode"]) + "</td>";
    //                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PassportExpireDate"]) + "</td>";
    //                    }
    //                    strMailMsg += "</tr>";
    //                }
    //                strMailMsg += "</table>";
    //            }
    //            MailDt = ObjST.GetMailingDetails("GroupBooking", Session["UID"].ToString());
    //            DataSet DSMail = new DataSet();
    //            DSMail = GB.GroupRequestDetails("GroupBooking", "ConfigMail", "", "");
    //            if (MailDt.Tables[0].Rows.Count > 0 && DSMail.Tables[0].Rows.Count > 0)
    //            {
    //                for (int k = 0; k < DSMail.Tables[0].Rows.Count; k++)
    //                {
    //                    i = GB.SendMail(DSMail.Tables[0].Rows[k]["ToEmail"].ToString(), MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogTrace.WriteErrorLog(ex, "AdminNExecMailSending");
    //    }
    //    return i;
    //}
}