using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using ITZLib;


public partial class BS_CancelTicket : System.Web.UI.Page
{
    BS_SHARED.SHARED shared; BS_BAL.SharedBAL sharedbal;
    List<BS_SHARED.SHARED> list; DataColumn col = null;
    //Itz_Trans_Dal objItzT = new Itz_Trans_Dal();
    //ITZ_Trans objIzT = new ITZ_Trans();
    //GetBalanceResponse objBalResp = new GetBalanceResponse();

    //_GetBalance objParamBal = new _GetBalance();
    //ITZGetbalance objItzBal = new ITZGetbalance();
    //ITZcrdb objItzTrans = new ITZcrdb();

    //DebitResponse objDebResp = new DebitResponse();

    //_CrOrDb objParamCrd = new _CrOrDb();
    //RefundResponse objRefnResp = new RefundResponse();

    //ITZcrdb objCrd = new ITZcrdb();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            shared = new BS_SHARED.SHARED(); string pax = "";
            sharedbal = new BS_BAL.SharedBAL();
            if ((Request.QueryString["tin"].ToString() != "" || Request.QueryString["tin"].ToString() != null) && (Request.QueryString["oid"].ToString() != "" || Request.QueryString["oid"].ToString() != null))
            {
                shared.tin = Request.QueryString["tin"].ToString().Trim();
                shared.orderID = Request.QueryString["oid"].ToString().Trim();

                list = sharedbal.getcancellist(shared);
                Session["list"] = list;

                if (list.Count == 0 || list[0].status == "Fail")
                {
                    divcan.Visible = false;
                    divcan_Pax.InnerHtml = "<div align='center' style='color:#203240;font-size:14px;'>The Ticket Has Already been Cancelled</div>";
                }
                else
                {
                    if (list[0].provider_name == "GS")
                    {
                        btncancel.Visible = false;
                        btnChkType.Visible = true;
                        divcan_Pax.InnerHtml = list[0].bookreq;
                    }
                    else
                    {
                        btncancel.Visible = true;
                        btnChkType.Visible = false;
                        pax += "<table cellpadding='0' cellspacing='5' border='0' class='canTbl'>";
                        pax += "<tr>";
                        pax += "<td><b>Bus Operator:</b></td>";
                        pax += "<td>" + list[0].busoperator + "</td>";
                        pax += "<td><b>Date Of Journey:</b></td>";
                        pax += "<td>" + list[0].journeyDate + "</td>";
                        pax += "</tr>";
                        pax += "<tr>";
                        pax += "<td><b>Source City:</b></td>";
                        pax += "<td>" + list[0].src + "</td>";
                        pax += "<td><b>Destination City:</b></td>";
                        pax += "<td>" + list[0].dest + "</td>";
                        pax += "</tr>";
                        pax += "<tr>";
                        pax += "<td><b>Pnr:</b></td>";
                        pax += "<td>" + list[0].pnr + "</td>";
                        pax += "<td><b>Ticket No:</b></td>";
                        pax += "<td>" + list[0].tin + "</td>";
                        pax += "</tr>";

                        pax += "<tr>";
                        pax += "<td><b>Partial Cancellation:</b></td>";
                        if (list[0].partialCanAllowed.ToUpper() == "TRUE")
                        {
                            pax += "<td>Allowed</td>";
                        }
                        else
                        {
                            pax += "<td>Not Allowed</td>";
                        }
                        pax += "<td>&nbsp;&nbsp;</td>";
                        pax += "<td>&nbsp;&nbsp;</td>";
                        pax += "</tr>";

                        pax += "<tr>";
                        pax += "<td colspan='4'><b>Note:</b> if partial cancellation is not allowed that means all tickets will cancelled at one time. </td>";
                        pax += "</tr>";


                        pax += "</table>";
                        divcan_Pax.InnerHtml = pax;
                        BindPax(list);
                        if (list[0].partialCanAllowed.ToUpper() == "FALSE")
                        {
                            foreach (RepeaterItem item in rep_paxCan.Items)
                            {
                                CheckBox chkk = (CheckBox)item.FindControl("chkChild");
                                chkk.Checked = true;
                                chkk.Enabled = false;
                            }
                        }
                    }
                }
            }

        }
    }

    public bool CheckCancelSatus(object val, string controltype)
    {
        bool status = false;
        if (Convert.ToString(val).ToLower().Trim() == "booked")
        {
            if (controltype == "lbl")
            {
                status = false;

            }
            else
            {
                status = true;

            }

        }
        else
        {
            if (controltype == "lbl")
            {
                status = true;

            }
            else
            {
                status = false;

            }

        }
        return status;
    }
    private void BindPax(List<BS_SHARED.SHARED> list)
    {
        DataTable paxdt = new DataTable();
        col = new DataColumn();
        col.ColumnName = "PaxTP";
        paxdt.Columns.Add(col);
        col = new DataColumn();
        col.ColumnName = "PaxName";
        paxdt.Columns.Add(col);
        col = new DataColumn();
        col.ColumnName = "Fare";
        paxdt.Columns.Add(col);
        col = new DataColumn();
        col.ColumnName = "NetFare";
        paxdt.Columns.Add(col);
        col = new DataColumn();
        col.ColumnName = "seat";
        paxdt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "Status";
        paxdt.Columns.Add(col);
        for (int k = 0; k <= list.Count - 1; k++)
        {
            DataRow dr = paxdt.NewRow();
            if (list[0].seat != "" && list[0].fare != "")
            {
                dr["PaxTP"] = "Passenger :" + (k + 1) + "";
                if (list[k].Passengername.IndexOf(",") >= 0)
                {
                    string[] name = list[k].Passengername.Split(',');
                    dr["PaxName"] = name[0].Trim();
                }
                else
                {
                    dr["PaxName"] = list[k].Passengername.Trim();
                }
                dr["Fare"] = list[k].fare.Trim();
                dr["seat"] = list[k].seat.Trim();
                dr["NetFare"] = Convert.ToString(list[k].taNetFare);
                dr["Status"] = Convert.ToString(list[k].status);
                paxdt.Rows.Add(dr);

            }
        }
        rep_paxCan.DataSource = paxdt;
        rep_paxCan.DataBind();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {

            string HiddenValue = HiddenField1.Value;
            string[] resp = null; string netfare = ""; string seat = ""; string fare = "";
            string originalfare = "";
            string[] cancelResponse = null;
            double busfare = 0; bool flagg = false; string[] cancelResp = new string[4];
            BS_DAL.SharedDAL shareddal = new BS_DAL.SharedDAL();
            // BS_BAL.advanceReserv objadv = new BS_BAL.advanceReserv();
            // BS_BAL.advanceReserv objadv1 = new BS_BAL.advanceReserv();
            // BS_BAL.advanceReserv oldresp = new BS_BAL.advanceReserv();
            List<BS_SHARED.SHARED> list = (List<BS_SHARED.SHARED>)Session["list"];
            List<BS_SHARED.SHARED> getlistGS = (List<BS_SHARED.SHARED>)Session["getlistGS"];
            shared = new BS_SHARED.SHARED(); sharedbal = new BS_BAL.SharedBAL();
            //if (list[0].provider_name == "GS")
            //{
            //    string cnstatus = "";
            //    list[0].bookreq = getlistGS[0].bookreq;
            //    GsrtcService objgsrtcService = new GsrtcService();
            //    string arrgs = list[0].bookreq.Trim().Split('_')[0];
            //    string arrgs1 = list[0].bookreq.Trim().Split('_')[1];
            //    string bookResOld = list[0].bookres.Trim();
            //    //cnstatus = objgsrtcService.confirmFullCancellation(arrgs, arrgs1);
            //    if (cnstatus == "success")
            //    {

            //        objadv = objgsrtcService.advanceReservObject(arrgs);
            //        objadv1 = objgsrtcService.advanceReservObject(arrgs1);

            //        string[] spltfare = objadv.seatNosToInActive.Trim().Split(',');
            //        // string[] splitnetfare = shared.netfare.Split(',');

            //        double OtherAmountCalcel = 0;
            //        OtherAmountCalcel = Math.Round(Convert.ToDouble(Convert.ToDouble(objadv1.canclReservationFee) + Convert.ToDouble(objadv1.canclAccidentReliefFund) + Convert.ToDouble(objadv1.canclBridgeFee) + Convert.ToDouble(objadv1.canclTollFee) + Convert.ToDouble(objadv1.canclUserFee) + Convert.ToDouble(objadv1.canclEntryFee) + Convert.ToDouble(objadv1.canclOtherLevies) + Convert.ToDouble(objadv1.cancelOtherDiscount) + Convert.ToDouble(objadv1.canclOtherConcessions)) / list.Count);

            //        for (int b = 0; b < list.Count; b++)
            //        {

            //            if (Convert.ToInt32(objadv.addnlAge[b]) < 12)
            //            {
            //                if (objadv1.refundPrcntOrLumpsum == "P")
            //                {
            //                    busfare = Math.Round(Convert.ToDouble((Convert.ToDouble(objadv.childFare) * Convert.ToDouble(objadv1.refundPrcntOrLumpsumValue)) / 100));
            //                    shared.refundAmt += Convert.ToString(Math.Round(Math.Round(Convert.ToDouble(objadv.childFare) + OtherAmountCalcel) - busfare)) + ",";
            //                }
            //                else
            //                {
            //                    busfare = Math.Round(Convert.ToDouble(Convert.ToInt32(objadv.childFare) + Convert.ToDouble(objadv1.refundPrcntOrLumpsumValue)));
            //                    shared.refundAmt += Convert.ToString(Math.Round(Math.Round(Convert.ToDouble(objadv.childFare) + OtherAmountCalcel) - busfare)) + ",";
            //                }
            //            }
            //            else
            //            {
            //                if (objadv1.refundPrcntOrLumpsum == "P")
            //                {
            //                    busfare = Math.Round(Convert.ToDouble((Convert.ToDouble(objadv.adultFare) * Convert.ToDouble(objadv1.refundPrcntOrLumpsumValue)) / 100));
            //                    shared.refundAmt += Convert.ToString(Math.Round(Math.Round(Convert.ToDouble(objadv.adultFare) + OtherAmountCalcel) - busfare)).Split('.')[0] + ",";
            //                }
            //                else
            //                {
            //                    busfare = Math.Round(Convert.ToDouble(Convert.ToInt32(objadv.adultFare) + Convert.ToDouble(objadv1.refundPrcntOrLumpsumValue)));
            //                    shared.refundAmt += Convert.ToString(Math.Round(Math.Round(Convert.ToDouble(objadv.adultFare) + OtherAmountCalcel) - busfare)).Split('.')[0] + ",";

            //                }
            //            }
            //            shared.cancelRecharge += Convert.ToString(busfare) + ",";
            //        }
            //        shared.pnr = list[0].pnr;
            //        shared.refundAmt = shared.refundAmt.Trim().Substring(0, shared.refundAmt.Trim().Length - 1);
            //        shared.cancelRecharge = shared.cancelRecharge.Trim().Substring(0,shared.cancelRecharge.Trim().Length - 1);
            //        shared.orderID = list[0].orderID;
            //        shared.seat = objadv1.seatNosToInActive;
            //        shared.tin = list[0].tin;
            //        shared.agentID = list[0].agentID;//change here
            //        shared.AgencyName = list[0].AgencyName;
            //        shareddal.updateCanchrgandRefund(shared);
            //        shared.refundAmt = objadv1.netRefundAmount.Trim().Split('.')[0];
            //        shared.addAmt = Convert.ToDecimal(shared.refundAmt);
            //        shared.avalBal = shareddal.deductAndaddfareAmt(shared, "Add");
            //        shareddal.updateTicketandPnr(shared);
            //        shareddal.insertLedgerDetails(shared, "Add");
            //        resp = new string[4];
            //        resp[0] = "suceess";
            //        resp[1] = objadv1.cancellationFee;
            //        resp[2] = objadv1.netRefundAmount;
            //        resp[3] = shared.tin + " Book Refrance Number :" + shared.orderID;
            //    }
            //    else
            //    {
            //        resp[0] = "Fail";
            //    }

            //}
            if (list[0].provider_name == "RB")
            {
                List<string> selectPax = new List<string>();
                foreach (RepeaterItem item in rep_paxCan.Items)
                {
                    CheckBox chkk = (CheckBox)item.FindControl("chkChild");
                    if (chkk.Checked == true)
                    {
                        Label lblnetfare = (Label)item.FindControl("lblnetfare");
                        Label lblseat = (Label)item.FindControl("lblseat");
                        Label lblfare = (Label)item.FindControl("lblfare");
                        Label lblpaxname = (Label)item.FindControl("lblpaxname");
                        netfare += lblnetfare.Text.Trim() + ",";
                        seat += lblseat.Text.Trim() + ",";
                        fare += lblfare.Text.Trim() + ",";
                        selectPax.Add(lblpaxname.Text.Trim());
                        originalfare += list.Where(x => Convert.ToString(x.seat) == Convert.ToString(lblseat.Text.Trim())).ToList()[0].originalfare + ",";
                        //originalfare += list.Where(x => Convert.ToInt16(x.seat) == Convert.ToInt16(lblseat.Text.Trim())).ToList()[0].originalfare + ",";
                    }

                }
                //for (int i = 0; i <= list.Count - 1; i++)
                //{
                //    originalfare += list[i].originalfare + ",";
                //}
                netfare = netfare.Remove(netfare.LastIndexOf(","));
                seat = seat.Remove(seat.LastIndexOf(","));
                fare = fare.Remove(fare.LastIndexOf(","));
                originalfare = originalfare.Remove(originalfare.LastIndexOf(","));
                shared.orderID = list[0].orderID.Trim();
                shared.tin = list[0].tin.Trim();
                shared.journeyDate = list[0].journeyDate.Trim();
                shared.canPolicy = list[0].canPolicy;
                shared.canTime = list[0].canTime;
                shared.agentID = list[0].agentID;
                shared.AgencyName = list[0].AgencyName;
                shared.provider_name = list[0].provider_name;
                shared.Passengername = list[0].Passengername;
                shared.gender = list[0].gender;
                shared.netfare = netfare.Trim();
                shared.seat = seat.Trim();
                shared.fare = originalfare;
                shared.tripId = list[0].tripId;
                shared.paymentmode = list[0].paymentmode;

                //---------------call cancel request insert ---------------- 
                String[] seatarr = shared.seat.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int seatcount = seatarr.Count();
                for (int i = 0; i < seatcount; i++)
                {
                    //shared.seat=seatarr[i];
                    BUSCancellationDetails(Convert.ToString(shared.orderID), Convert.ToString(seatarr[i]), Convert.ToString(selectPax[i]), Convert.ToString(shared.gender), Convert.ToString(shared.agentID), Convert.ToString(shared.tripId), Convert.ToString(shared.paymentmode), Convert.ToString(shared.tin));
                }

                 try
            {
                if (Session["LoginByOTP"] != null && Convert.ToString(Session["LoginByOTP"]) != "" && Convert.ToString(Session["LoginByOTP"]) == "true")
                {
                    string UserId = Convert.ToString(Session["UID"]);
                    string Remark = "BUS TICKET CANCEL";
                    string OTPRefNo = Convert.ToString(shared.orderID);
                    string LoginByOTP = Convert.ToString(Session["LoginByOTP"]);
                    string OTPId = Convert.ToString(Session["OTPID"]);
                    string ServiceType = "BUS-TICKET-CANCEL";
                    int flag = 0;
                    SqlTransaction OTPST = new SqlTransaction();
                    flag = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType);
                }
            }
            catch (Exception ex)
            {
                EXCEPTION_LOG.ErrorLog.FileHandling("BUS", "Error_102", ex, "BS-CancelTicket");
            }


                resp = sharedbal.cancelTicket(ref shared);
                if (resp[0].Trim().ToUpper() != "FAIL")
                {
                    MakeRefund(shared, resp);

                    try
                    {
                        // for Cancelltion SMS
                        DataSet AInfo = AgentIDEMAILINFO(Convert.ToString(shared.orderID));
                        SMSAPI.SMS BusSms = new SMSAPI.SMS();
                        string SeatNo = "", PnrNo = "", MobNo = "", Sector = "", JourneyDate = "";
                        if (AInfo.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < AInfo.Tables[0].Rows.Count; j++)
                            {
                                SeatNo += Convert.ToString(AInfo.Tables[0].Rows[j]["SEATNO"]) + ";";
                            }
                            SeatNo = SeatNo.Substring(0, SeatNo.Length - 1);
                            PnrNo = Convert.ToString(AInfo.Tables[0].Rows[0]["PNR"]);
                            MobNo = Convert.ToString(AInfo.Tables[0].Rows[0]["PRIMARY_PAX_PAXMOB"]);
                            Sector = Convert.ToString(AInfo.Tables[0].Rows[0]["Sector"]);
                            JourneyDate = Convert.ToString(AInfo.Tables[0].Rows[0]["JOURNEYDATE"]);
                            try
                            {




                                DataTable SmsCrd = new DataTable();
                                SqlTransaction objDA = new SqlTransaction();
                                SmsCrd = objDA.SmsCredential(SMS.BUSCANCEL.ToString()).Tables[0];
                                if (SmsCrd.Rows.Count > 0 && Convert.ToBoolean(SmsCrd.Rows[0]["Status"]) == true)
                                {
                                    string Smsstatus = "";
                                    Smsstatus = BusSms.SendBusSms(Convert.ToString(shared.orderID), PnrNo, MobNo, Sector, JourneyDate, SeatNo, "BusBooking", "Cancel", SmsCrd);
                                    SqlTransactionNew obj = new SqlTransactionNew();
                                    obj.SmsLogDetails(Convert.ToString(shared.orderID), MobNo, "", Smsstatus);
                                }
                                //If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                //                                                smsStatus = objSMSAPI.sendSms(FltHdrDs.Tables(0).Rows(0)("OrderId").ToString, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("sector").ToString.Trim, FltHdrDs.Tables(0).Rows(0)("VC").ToString.Trim, FltDs.Tables(0).Rows(0)("FlightIdentification"), FltDs.Tables(0).Rows(0)("Departure_Date"), AirlinePnr, smsMsg, SmsCrd)
                                //                                                objSql.SmsLogDetails(FltHdrDs.Tables(0).Rows(0)("OrderId").ToString, FltHdrDs.Tables(0).Rows(0)("PgMobile").ToString.Trim, smsMsg, smsStatus)
                                //                                            End If
                            }
                            catch (Exception)
                            {
                            }


                        }
                    }
                    catch (System.Exception)
                    {
                    }


                }

            }
            if (resp[0].Trim().ToUpper() == "FAIL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Failed');", true);
            }
            else
            {
                if (resp[0].Trim().ToUpper() != "FAIL")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "func", "close();", true);
                    string mytable = "<table class='table table-bordered' style='    line-height: 29px;' >";
                    mytable += "<tr>";
                    mytable += "<td colspan='4' style='text-align: center; font-size: 14px; font-family: arial;width:100%;  padding: 5px;' ><h2>Refund Details</h2></td>";
                    mytable += "</tr>";
                    mytable += "<tr>";
                    mytable += "<td colspan='4'>Your request for cancellation is successfully executed. Please find below the refund details</td>";
                    mytable += "</tr>";
                    mytable += "<tr>";
                    mytable += "<td>Cancellation Charge:</td>";
                    mytable += "<td>" + resp[1].Trim() + "</td>";
                    mytable += "<td>Refunded Amount: " + shared.addAmt + "</td>";
                    mytable += "<td>Cancelled against Ticket no :" + resp[3].Trim() + "</td>";
                    mytable += "</tr>";
                    mytable += "</table>";
                    mytable += "<table style='text-align: center; font-size: 14px;font-family: arial;width:100%;    line-height: 29px; padding: 5px;'>";
                    foreach (RepeaterItem item in rep_paxCan.Items)
                    {
                        CheckBox chkk = (CheckBox)item.FindControl("chkChild");
                        if (chkk.Checked == true)
                        {
                            mytable += "<tr>";
                            mytable += "<td>Passenger name:</td>";
                            mytable += "<td>" + list[0].Passengername + "</td>";

                            mytable += "<td>Journey Date:</td>";
                            mytable += "<td>" + list[0].journeyDate.Trim() + "</td>";

                            mytable += "<td>Ticket No:</td>";
                            mytable += "<td>" + list[0].tin.Trim() + "</td>";

                            mytable += "<td>Seat No:</td>";
                            mytable += "<td>" + seat.Trim() + "</td>";

                            mytable += "</tr>";
                        }
                    }
                    mytable += "</table>";
                    divcan_Pax.Visible = false;
                    divcan.Visible = false;
                    divrefund.InnerHtml = mytable;
                }


                //if (resp[0].Trim().ToUpper() != "FAIL")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "func", "close();", true);
                //    string mytable = "<table cellpadding='0' cellspacing='4' border='0' align='center' class='divcan'>";
                //    mytable += "<tr>";
                //    mytable += "<td>Cancellation Charge:</td>";
                //    mytable += "<td>" + resp[1].Trim() + "</td>";
                //    mytable += "<td>Refunded Amount:</td>";
                //    mytable += "<td>" + shared.addAmt  + "</td>";
                //    mytable += "<td>Status:</td>";
                //    mytable += "<td>Cancelled against Ticket no :" + resp[3].Trim() + "</td>";
                //    mytable += "</tr>";
                //    mytable += "</table>";
                //    divcan_Pax.Visible = false;
                //    divcan.Visible = false;
                //    divrefund.InnerHtml = mytable;
                //}
            }
        }
        catch (Exception ex)
        {
            string str = ex.Message;
        }
    }

    public static DataSet AgentIDEMAILINFO(string ORDERID)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        DataSet ADS = new DataSet();
        try
        {
            using (SqlCommand sqlcmd = new SqlCommand())
            {
                sqlcmd.Connection = con;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                else
                {
                    con.Open();
                }
                sqlcmd.CommandTimeout = 900;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "USP_BS_AGENTDETAILS";
                sqlcmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = ORDERID;
                sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "Y";
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ADS);
                con.Close();
                ADS.Dispose();
            }
        }
        catch (Exception ex)
        {
        }
        return ADS;
    }

    public void MakeRefund(BS_SHARED.SHARED shared, string[] cancelResp)
    {
        // Session["MchntKeyITZ"] = ConfigurationManager.AppSettings["BUSMerchantKey"].ToString();
        //string MchntKeyITZ = ConfigurationManager.AppSettings["BUSMerchantKey"].ToString();
        //=---------------------------- payment duction code-----------------------------------------

        //if ((PaymentMode == "wallet"))
        //{

        Random rand = new System.Random(1);
        string refundOrderID = "RFNDBUS" + shared.orderID + "_" + DateTime.Now.ToString("HHmmss");
        string rfndStatus = "RefundRequested";
        string easyRefundID = "";
        string easyTransCode = "";
        try
        {

            if (cancelResp.Count() > 2 && shared.addAmt > 0)
            {


                //objParamCrd._DECODE = (shared.agentID != null ? shared.agentID.ToString().Trim() : " ");
                //try
                //{
                //    objParamCrd._MERCHANT_KEY = (MchntKeyITZ != null ? MchntKeyITZ.Trim() : " ");
                //    //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                //    objParamCrd._AMOUNT = Convert.ToString((shared.addAmt != null ? shared.addAmt : 0));
                //    objParamCrd._ORDERID = (refundOrderID != null && !string.IsNullOrEmpty(refundOrderID) ? refundOrderID.Trim() : " ");
                //    objParamCrd._REFUNDORDERID = (shared.orderID != null && !string.IsNullOrEmpty(shared.orderID) ? shared.orderID.Trim() : " ");
                //    objParamCrd._MODE = (Session["ModeTypeITZ"] != null ? Session["ModeTypeITZ"].ToString().Trim() : " ");
                //    //'IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                //    objParamCrd._REFUNDTYPE = "P";
                //    //'objParamCrd._CHECKSUM = " "
                //    string stringtoenc = "MERCHANTKEY=" + objParamCrd._MERCHANT_KEY + "&ORDERID=" + objParamCrd._ORDERID + "&REFUNDTYPE=" + objParamCrd._REFUNDTYPE;
                //    objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc);
                //    //objParamCrd._SERVICE_TYPE = IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                //    objParamCrd._DESCRIPTION = "refund to agent -" + shared.agentID + " against BUS OrderID-" + shared.orderID + " and Seat No(s)-" + shared.seat;
                //    objRefnResp = objCrd.ITZRefund(objParamCrd);
                //}
                //catch (Exception ex)
                //{
                //    clsErrorLog.LogInfo(ex);

                //}

                //try
                //{
                //    objItzT = new Itz_Trans_Dal();
                //    objIzT.AMT_TO_DED = "0";
                //    objIzT.AMT_TO_CRE = Convert.ToString((shared.addAmt != null ? shared.addAmt : 0));
                //    objIzT.B2C_MBLNO_ITZ = " ";
                //    objIzT.COMMI_ITZ = " ";
                //    objIzT.CONVFEE_ITZ = " ";
                //    objIzT.DECODE_ITZ = (shared.agentID != null ? shared.agentID.Trim() : " ");
                //    objIzT.EASY_ORDID_ITZ = (objRefnResp.EASY_ORDER_ID != null ? objRefnResp.EASY_ORDER_ID : " ");
                //    objIzT.EASY_TRANCODE_ITZ = (objRefnResp.EASY_TRAN_CODE != null ? objRefnResp.EASY_TRAN_CODE : " ");
                //    objIzT.ERROR_CODE = (objRefnResp.ERROR_CODE != null ? objRefnResp.ERROR_CODE : " ");
                //    objIzT.MERCHANT_KEY_ITZ = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : " ");
                //    //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                //    objIzT.MESSAGE_ITZ = (objRefnResp.MESSAGE != null ? objRefnResp.MESSAGE : " ");
                //    objIzT.ORDERID = (shared.orderID != null && !string.IsNullOrEmpty(shared.orderID) ? shared.orderID.Trim() : " ");
                //    objIzT.RATE_GROUP_ITZ = " ";
                //    objIzT.REFUND_TYPE_ITZ = (objRefnResp.REFUND_TYPE != null && !string.IsNullOrEmpty(objRefnResp.REFUND_TYPE) && objRefnResp.REFUND_TYPE != " " ? objRefnResp.REFUND_TYPE : " ");
                //    objIzT.SERIAL_NO_FROM = " ";
                //    objIzT.SERIAL_NO_TO = " ";
                //    objIzT.SVC_TAX_ITZ = " ";
                //    objIzT.TDS_ITZ = " ";
                //    objIzT.TOTAL_AMT_DED_ITZ = " ";
                //    objIzT.TRANS_TYPE = "BUSREFUND";
                //    objIzT.USER_NAME_ITZ = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : " ");
                //    try
                //    {
                //        objBalResp = new GetBalanceResponse();
                //        objParamBal._DCODE = (shared.agentID != null ? shared.agentID.Trim() : " ");
                //        objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : " ");
                //        //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                //        objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : " ");
                //        objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : " ");
                //        objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
                //        objIzT.ACCTYPE_NAME_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME : " ");
                //        objIzT.AVAIL_BAL_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : " ");
                //        Session["CL"] = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : " ");
                //        string ablBalance = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : " ");

                //    }
                //    catch (Exception ex)
                //    {
                //        clsErrorLog.LogInfo(ex);
                //    }
                //    bool inst = objItzT.InsertItzTrans(objIzT);
                //}
                //catch (Exception ex)
                //{
                //    clsErrorLog.LogInfo(ex);
                //}

                //if (objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute"))
                //{
                rfndStatus = "Refunded";
                easyRefundID = "";//objRefnResp.EASY_ORDER_ID;
                easyTransCode = "";// objRefnResp.EASY_TRAN_CODE;
                //}
            }
            else
            {
                if (shared.addAmt < 0)
                {
                    shared.addAmt = 0;
                }
                cancelResp[0] = "Failed";
            }
        }
        catch (Exception ex1) { cancelResp[0] = "Failed"; }

        string apicancelstatus = "";
        if (cancelResp[0].ToLower().Trim() == "success")
        {
            apicancelstatus = "Cancelled";

        }
        else
        {
            apicancelstatus = "Failed";
        }


        BS_DAL.SharedDAL shareddal = new BS_DAL.SharedDAL();

        shareddal.updateCanchrgandRefund(shared, shared.serviceID, apicancelstatus, rfndStatus, easyRefundID, refundOrderID, easyTransCode);
        //-------------------------------insert into database----------------------------------//

        shared.refundAmt = shared.refundAmt.Remove(shared.refundAmt.LastIndexOf(","));

        shared.avalBal = shareddal.deductAndaddfareAmt(shared, "Add");
        shareddal.insertLedgerDetails(shared, "Add");




    }


    protected void btnChkType_Click(object sender, EventArgs e)
    {
        GsrtcService objGsrtc = new GsrtcService();
        string HiddenValue = HiddenField1.Value;
        List<BS_SHARED.SHARED> list = (List<BS_SHARED.SHARED>)Session["list"];
        List<BS_SHARED.SHARED> getlistGS = new List<BS_SHARED.SHARED>();
        btncancel.Visible = true;
        btnChkType.Visible = false;
        divcan_Pax.Visible = true;
        divcan.Visible = true;
        try
        {
            //getlistGS = objGsrtc.getFullCancellationDetails(list,HiddenValue);
            Session["getlistGS"] = getlistGS;

            if (getlistGS[0].status == "Fail")
            {
                divcan_Pax.InnerHtml = "<div align='center' style='color:#203240;font-size:14px;'>The Ticket Has Already been Cancelled</div>";
                btncancel.Visible = false;
            }
            else
            {
                divcan_Pax.InnerHtml = getlistGS[0].bookres;
                btncancel.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }
    }

    public int BUSCancellationDetails(string orderid, string seatno, string paxname, string gender, string agentid, string tripid, string paymentmode, string pnr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        int i = 0;

        Random ss = new Random();
        //int rfndn = ss.Next(87756556);
        //string rmdno = "BRFND" + rfndn.ToString();
        try
        {
            using (SqlCommand sqlcmd = new SqlCommand())
            {
                sqlcmd.Connection = con;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                else
                {
                    con.Open();
                }
                //sqlcmd.CommandTimeout = 900;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "SP_BUS_CANCELLATION_DETAILS";
                sqlcmd.Parameters.AddWithValue("@cmd", "insert");
                sqlcmd.Parameters.AddWithValue("@orderid", orderid);
                sqlcmd.Parameters.AddWithValue("@trip", tripid);
                sqlcmd.Parameters.AddWithValue("@seatno", seatno);
                sqlcmd.Parameters.AddWithValue("@paxname", paxname);
                sqlcmd.Parameters.AddWithValue("@gender", "");
                sqlcmd.Parameters.AddWithValue("@agentid", agentid);
                sqlcmd.Parameters.AddWithValue("@cancellationid", "");
                sqlcmd.Parameters.AddWithValue("@refundstatus", "Requested");
                sqlcmd.Parameters.AddWithValue("@apirefundstatus", "");
                sqlcmd.Parameters.AddWithValue("@apicancellationamt", 0);
                sqlcmd.Parameters.AddWithValue("@refundservicechrg", 0);
                sqlcmd.Parameters.AddWithValue("@acceptedby", "");
                sqlcmd.Parameters.AddWithValue("@accepteddate", "");
                sqlcmd.Parameters.AddWithValue("@refundedby", "");
                sqlcmd.Parameters.AddWithValue("@refundeddate", "");
                sqlcmd.Parameters.AddWithValue("@userrefundamt", 0);
                sqlcmd.Parameters.AddWithValue("@paymentmode", paymentmode);
                sqlcmd.Parameters.AddWithValue("@apicancellationstatus", "");
                sqlcmd.Parameters.AddWithValue("@Pnr", pnr);
                i = (Int16)sqlcmd.ExecuteNonQuery();
                con.Close();


            }
        }
        catch (Exception ex)
        {
        }
        return i;
    }
}
