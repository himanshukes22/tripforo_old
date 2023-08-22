using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BS_BusBooking : System.Web.UI.Page
{

    BS_SHARED.SHARED shared = new BS_SHARED.SHARED();
    EXCEPTION_LOG.ErrorLog erlog = new EXCEPTION_LOG.ErrorLog();
    PG.PaymentGateway obPg = new PG.PaymentGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!IsPostBack)
            //{ 
            //}
            List<BS_SHARED.SHARED> farelist = new List<BS_SHARED.SHARED>();
            List<BS_SHARED.SHARED> list_Pnr = new List<BS_SHARED.SHARED>();
            string OrderId = Request.QueryString["OBTID"];
            string TotalAmount = "0";
            string OrignalAmount = "0";
            string PgStatus = "0";
            string ApiStatus = "0";
            string PgCharges = "0";
            string CreditLimitUpdate = string.Empty;
            string UnmappedStatus = string.Empty;
            string ServiceType = string.Empty;
            string BookingStatus = string.Empty;
            string message = "";
            string PaymentMode = string.Empty;
            string PgBooking = string.Empty;

            if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "")
            {
                if (!string.IsNullOrEmpty(OrderId))
                {
                    DataSet ds = obPg.GetBusBookingDetails(OrderId, Convert.ToString(Session["UID"]));
                    if (ds != null)
                    {
                        #region Bind Bus Booking details
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            shared.agentID = Convert.ToString(ds.Tables[0].Rows[0]["AGENTID"]);
                            shared.blockKey = Convert.ToString(ds.Tables[0].Rows[0]["BLOCK_KEY"]);
                            shared.orderID = Convert.ToString(ds.Tables[0].Rows[0]["ORDERID"]);
                            shared.paxemail = Convert.ToString(ds.Tables[0].Rows[0]["Emaill"]);
                            shared.paxmob = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
                            shared.provider_name = (Convert.ToString(ds.Tables[0].Rows[0]["PROVIDER_NAME"]));
                            shared.paymentmode = "";// Convert.ToString(ds.Tables[0].Rows[0]["Trip"]);
                            shared.NoOfPax = Convert.ToString(ds.Tables[0].Rows[0]["NOOF_PAX"]);
                            BookingStatus = Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]);
                            shared.tripId = Convert.ToString(ds.Tables[0].Rows[0]["TRIPID"]);
                            PaymentMode = Convert.ToString(ds.Tables[0].Rows[0]["PaymentMode"]);
                            PgBooking = Convert.ToString(ds.Tables[0].Rows[0]["PgBooking"]);
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            shared.paymentmode = Convert.ToString(ds.Tables[1].Rows[0]["PaymentMode"]);
                            shared.AgencyName = Convert.ToString(ds.Tables[1].Rows[0]["AgencyName"]);
                            shared.subAmt = Convert.ToDecimal(ds.Tables[1].Rows[0]["OriginalAmount"]);
                            TotalAmount = (Convert.ToString(ds.Tables[1].Rows[0]["Amount"])).ToLower();
                            OrignalAmount = Convert.ToString(ds.Tables[1].Rows[0]["OriginalAmount"]);
                            PgCharges = Convert.ToString(ds.Tables[1].Rows[0]["TotalCharges"]);
                            PgStatus = Convert.ToString(ds.Tables[1].Rows[0]["Status"]);
                            ApiStatus = Convert.ToString(ds.Tables[1].Rows[0]["ApiStatus"]).ToLower();
                            UnmappedStatus = Convert.ToString(ds.Tables[1].Rows[0]["UnmappedStatus"]).ToLower();
                            CreditLimitUpdate = Convert.ToString(ds.Tables[1].Rows[0]["CreditLimitUpdate"]).ToLower();
                            ServiceType = Convert.ToString(ds.Tables[1].Rows[0]["ServiceType"]);

                        }
                        #endregion
                        if (PgStatus == "success" && UnmappedStatus == "captured" && ApiStatus == "success" && CreditLimitUpdate == "true" && BookingStatus.ToLower() == "request" && ServiceType.ToLower() == "bus")
                        {
                            #region Balance Check and deduct and Transaction Log - Staff Login

                            string DebitSataus = "";
                            string CreditSataus = "";
                            string CheckBalance = "";
                            string AgentStatus = "";
                            string StaffBalCheck = "";
                            string StaffBalCheckStatus = "";
                            string CurrentTotAmt = "";
                            string TransAmount = "";
                            string BookTicket = "true";
                            try
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(Session["LoginByStaff"])) && Convert.ToString(Session["LoginByStaff"]).ToUpper() == "TRUE" && Convert.ToString(Session["LoginType"]).ToUpper() == "STAFF")
                                {
                                    BookTicket = "false";
                                    if (Convert.ToString(Session["BUS"]) == "True")
                                    {
                                        string BoookingByStaff = "True";
                                        string sOrderId = shared.orderID;
                                        string sTransAmount = Convert.ToString(shared.subAmt); //FltHdrDs.Tables(0).Rows(0)["TotalAfterDis"];
                                        string sStaffUserId = Convert.ToString(Session["StaffUserId"]);
                                        string sOwnerId = Convert.ToString(Session["UID"]);
                                        string sIPAddress = Request.UserHostAddress;
                                        string sRemark = Convert.ToString(Session["LoginType"]) + "_" + Convert.ToString(Session["StaffUserId"]) + "_" + sOrderId + "_BUS_" + shared.provider_name + "_" + shared.journeyDate + "_" + Convert.ToString(shared.subAmt);
                                        string sCreatedBy = Convert.ToString(Session["StaffUserId"]);
                                        string ModuleType = "BUS BOOKING";
                                        string sServiceType = "BUS";
                                        string DebitCredit = "DEBIT";
                                        string ActionType = "CHECKBAL-DEDUCT";
                                        DataSet StaffDs;
                                        //Dim objSqlDom As New SqlTransactionDom
                                        SqlTransactionDom objSqlDom = new SqlTransactionDom();
                                        StaffDs = objSqlDom.CheckStaffBalanceAndBalanceDeduct(sOrderId, sServiceType, Convert.ToDouble(sTransAmount), sStaffUserId, sOwnerId, sIPAddress, sRemark, sCreatedBy, DebitCredit, ModuleType, ActionType);
                                        if ((StaffDs != null && StaffDs.Tables.Count > 0 && StaffDs.Tables[0].Rows.Count > 0))
                                        {
                                            // DebitSataus ,CreditSataus,CheckBalance,AgentStatus,StaffBalCheck,StaffBalCheckStatus,CurrentTotAmt,TransAmount		
                                            DebitSataus = Convert.ToString(StaffDs.Tables[0].Rows[0]["DebitSataus"]);
                                            CreditSataus = Convert.ToString(StaffDs.Tables[0].Rows[0]["CreditSataus"]);
                                            CheckBalance = Convert.ToString(StaffDs.Tables[0].Rows[0]["CheckBalance"]);
                                            AgentStatus = Convert.ToString(StaffDs.Tables[0].Rows[0]["AgentStatus"]);
                                            StaffBalCheck = Convert.ToString(StaffDs.Tables[0].Rows[0]["StaffBalCheck"]);
                                            StaffBalCheckStatus = Convert.ToString(StaffDs.Tables[0].Rows[0]["StaffBalCheckStatus"]);
                                            CurrentTotAmt = Convert.ToString(StaffDs.Tables[0].Rows[0]["CurrentTotAmt"]);
                                            TransAmount = Convert.ToString(StaffDs.Tables[0].Rows[0]["TransAmount"]);
                                            BookTicket = "false";
                                            if (Convert.ToString(StaffDs.Tables[0].Rows[0]["LoginStatus"]) == "True" && Convert.ToString(StaffDs.Tables[0].Rows[0]["BUS"]) == "True" && (DebitSataus.ToLower() == "true" || StaffBalCheckStatus.ToLower() == "false"))
                                                BookTicket = "true";
                                        }
                                        else
                                            BookTicket = "false";
                                    }
                                    else
                                        BookTicket = "false";
                                }
                            }
                            catch (Exception ex)
                            {
                                BookTicket = "false";
                            }

                            // END: Balance Check and deduct and Transaction Log - Staff
                            #endregion Staff 

                            if (PaymentMode == "PG" && PgBooking == "true" && BookTicket == "true")
                            {
                                #region check credit limit and Final Bus Booking
                                decimal crdlimit = 0;
                                BS_BAL.SharedBAL sharedbal = new BS_BAL.SharedBAL();
                                crdlimit = sharedbal.getCrdLimit(shared.agentID.Trim());
                                if (crdlimit >= shared.subAmt)
                                {
                                    if (Convert.ToString(Session["UID"]) == shared.agentID)
                                    {
                                        //strmessage = Booking(shared, finallist);
                                        BS_DAL.SharedDAL shareddal = new BS_DAL.SharedDAL();
                                        shared.avalBal = shareddal.deductAndaddfareAmt(shared, "subtract");
                                        shareddal.insertLedgerDetails(shared, "subtract");
                                        //----------------------final book--------------------//

                                        try
                                        {
                                            list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                                            if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                                            {
                                                // message = list_Pnr[0].TicketNoRes + ":" + list_Pnr[0].bookres;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact administrator and try again - 008');window.location='../Search.aspx'; ", true);
                                                // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + message + "');window.location='../Search.aspx'; ", true);
                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
                                            }
                                            else
                                            {
                                                message = list_Pnr[0].tin;
                                                Response.Redirect("TicketCopy.aspx?tin=" + list_Pnr[0].tin.Trim() + "&oid=" + shared.orderID.Trim() + "");
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            erlog = new EXCEPTION_LOG.ErrorLog();
                                            erlog.writeErrorLog(ex, "BusBooking.aspx.cs");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Your booking on hold,please contact to administrator - 001');window.location='../Search.aspx'; ", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact to administrator and try again - 002');window.location='../Search.aspx'; ", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('you have insufficient balance');window.location='../Search.aspx'; ", true);
                                }
                                #endregion
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You are all ready processed to booking,Please contact administrator and try again - 003');window.location='../Search.aspx'; ", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact to administrator and try again - 004');window.location='../Search.aspx'; ", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact to administrator and try again - 005');window.location='../Search.aspx'; ", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact to administrator and try again - 006');window.location='../Search.aspx'; ", true);
                }

            }
            else
            {
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            //System.Web.Security.FormsAuthentication.SignOut();
            //Session.Abandon();
            //Response.Redirect("Login.aspx?reason=Session TimeOut", false);
            erlog = new EXCEPTION_LOG.ErrorLog();
            erlog.writeErrorLog(ex, "BusBooking.aspx.cs");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please contact to administrator and try again - 007');window.location='../Search.aspx'; ", true);
        }

    }
}