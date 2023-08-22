using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GRP_Booking;
using System.Configuration;

public partial class GroupSearch_UpdateTicket_PNR : System.Web.UI.Page
{
    GroupBooking ObjGB = new GroupBooking();
    string RequestID = "";
    DataSet DS = new DataSet();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    string O_ARCOD = "", I_ARCOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack != true)
            {
                RequestID = Request.QueryString["RequestID"];
                DS = ObjGB.GROUPTICKETINFO(RequestID, "", "", "", "0", "SELECT", "");

                if (DS.Tables[0].Rows.Count > 0)
                {
                    ViewState["O_ARCOD"] = DS.Tables[0].Rows[0]["Aircode"].ToString();
                    ViewState["I_ARCOD"] = DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["Aircode"].ToString();
                    O_ARCOD = ViewState["O_ARCOD"].ToString();
                    I_ARCOD = ViewState["I_ARCOD"].ToString();
                    if (O_ARCOD != I_ARCOD && DS.Tables[2].Rows.Count > 0)
                    {
                        tbl_Inboundpaxinfo.Visible = true;
                        tbl_inboundpnr.Visible = true;
                        InbounTicketUpdate.DataSource = DS.Tables[2];
                        InbounTicketUpdate.DataBind();
                    }
                    else
                    {
                        tbl_Inboundpaxinfo.Visible = false;
                        tbl_inboundpnr.Visible = false;
                    }
                    FlightDetails.DataSource = DS.Tables[0];
                    FlightDetails.DataBind();
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        BookingDetails.DataSource = DS.Tables[1];
                        BookingDetails.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        UpdateTicket.DataSource = DS.Tables[2];
                        UpdateTicket.DataBind();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "UpdateTicketPageLoad");
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        TextBox TicketNo;
        Label Counter;
        int i = 0, j = 0;
        string AirlinePNR = "", GDSPNR = "";
        string INAirlinePNR = "", INGDSPNR = "";
        O_ARCOD = ViewState["O_ARCOD"].ToString();
        I_ARCOD = ViewState["I_ARCOD"].ToString();
        try
        {
            RequestID = Request.QueryString["RequestID"];
            AirlinePNR = txt_airlinepnr.Text.Trim().ToString();
            GDSPNR = txt_gdspnr.Text.Trim().ToString();
            foreach (GridViewRow UPTKT in UpdateTicket.Rows)
            {
                TicketNo = (TextBox)UPTKT.FindControl("txt_Ticket");
                Counter = (Label)UPTKT.FindControl("lbl_counter");
                ObjGB.UPDATEGROUPTICKETINFO("", "", "", TicketNo.Text.Trim().ToString(), Counter.Text.Trim(), "UPDATE", "PAX", Session["UID"].ToString());
            }
            if (O_ARCOD != I_ARCOD)
            {
                foreach (GridViewRow INUPTKT in InbounTicketUpdate.Rows)
                {
                    TicketNo = (TextBox)INUPTKT.FindControl("txt_Ticket");
                    Counter = (Label)INUPTKT.FindControl("lbl_counter");
                    ObjGB.UPDATEGROUPTICKETINFO("", "", "", TicketNo.Text.Trim().ToString(), Counter.Text.Trim(), "UPDATE", "INPAX", Session["UID"].ToString());
                }
                INAirlinePNR = txt_INAirLine.Text.Trim().ToString();
                INGDSPNR = txt_INGDS.Text.Trim().ToString();
                ObjGB.UPDATEGROUPTICKETINFO(RequestID, INGDSPNR, INAirlinePNR, "", "0", "UPDATE", "INBOOKING", Session["UID"].ToString());
            }

            ObjGB.UPDATEGROUPTICKETINFO(RequestID, GDSPNR, AirlinePNR, "", "0", "UPDATE", "BOOKING", Session["UID"].ToString());

            i = AgentMailSending(RequestID, AirlinePNR, GDSPNR, INAirlinePNR, INGDSPNR);
            j = AdminNExecMailSending(RequestID, AirlinePNR, GDSPNR, INAirlinePNR, INGDSPNR);
            if (i == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "UpdateTicketBtnClk");
        }
    }
    protected int AgentMailSending(string RefRequestID, string AirlinePNR, string GDSPNR, string Inboundairline, string IndoundGDSPNR)
    {
        int i = 0;
        try
        {
            DataSet MailDt = new DataSet();
            DataSet MailContent = new DataSet();
            DataSet DSStatus = new DataSet();
            DataSet mailmsg = new DataSet();
            string strMailMsg = "", STRSUB = "", MAILHEADER = "", MAILMESSAGE = "", _cmdtype = "", StrMail = "";
            DataSet AgencyDS = new DataSet();
            SqlTransaction ST = new SqlTransaction();
            DSStatus = ObjGB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
            _cmdtype = Convert.ToString(DSStatus.Tables[0].Rows[0]["Status"]).ToUpper();
            mailmsg = ObjGB.GRP_MAILMSGSUBJECT(_cmdtype, "AGENT");
            STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
            MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
            MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
            MailContent = ObjGB.GroupRequestDetails(RefRequestID, "MAILCONTENTAGENT", "", "");
            string REFSNO = Convert.ToString(MailContent.Tables[0].Rows[0]["REFSNO"]);
            if (DSStatus.Tables[0].Rows.Count > 0)
            {
                StrMail = ObjGB.GETEMAILID(RefRequestID, "AGENT");
                strMailMsg += "<table style='border: 1px width:100%;'>";
                strMailMsg += "<tr>";
                strMailMsg += "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
                strMailMsg += "<h2> " + MAILHEADER + " </h2>";
                strMailMsg += "</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr>";
                strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
                strMailMsg += "</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKED PRICE</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>AIRLINE PNR</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GDS PNR</td>";
                if (Inboundairline != "" && IndoundGDSPNR!="")
                {
                    strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>INBOUND AIRLINE PNR</td>";
                    strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>INBOUND GDS PNR</td>";
                }
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
                strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookedPrice"]) + "</td>";
                strMailMsg += "<td>" + AirlinePNR + "</td>";
                strMailMsg += "<td>" + GDSPNR + "</td>";
                if (Inboundairline != "" && IndoundGDSPNR != "")
                {
                    strMailMsg += "<td>" + Inboundairline + "</td>";
                    strMailMsg += "<td>" + IndoundGDSPNR + "</td>";
                }
                strMailMsg += "</tr>";
                strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
                if (MailContent.Tables[0].Rows.Count > 0)
                {
                    strMailMsg += "<table style='border: 1px width:100%;'>";
                    strMailMsg += "<tr>";
                    strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
                    strMailMsg += "";
                    strMailMsg += "</td>";
                    strMailMsg += "</tr>";
                    strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                    //strMailMsg += "<td>REQUESTID</td>";
                    strMailMsg += "<td>TRIP FROM</td>";
                    strMailMsg += "<td>TRIP TO</td>";
                    strMailMsg += "<td>DEP. DATE</td>";
                    strMailMsg += "<td>DEP TIME</td>";
                    strMailMsg += "<td>ARVL DATE</td>";
                    strMailMsg += "<td>ARVL TIME</td>";
                    strMailMsg += "<td>AIRLINE</td>";
                    strMailMsg += "<td>FLIGHT NO.</td>";
                    strMailMsg += "</tr>";
                    DataTable DT = new DataTable();
                    DT = MailContent.Tables[1].Select(String.Format("SNO = '{0}'", REFSNO)).CopyToDataTable();
                    if (DT.Rows.Count > 0)
                    {
                        for (int j = 0; j < DT.Rows.Count; j++)
                        {
                            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                            //strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["RequestID"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Location"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Location"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Date"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Time"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Date"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Time"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Aircode"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["FlightNumber"]) + "</td>";
                            strMailMsg += "</tr>";
                        }
                        strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
                    }
                    strMailMsg += "</table>";
                    strMailMsg += "<table style='width:100%;'>";
                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                    strMailMsg += "<td>TRIP TYPE</td>";
                    strMailMsg += "<td>TRIP</td>";
                    strMailMsg += "<td>TOTAL PASSANGERS</td>";
                    strMailMsg += "<td>ADULT</td>";
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
                    {
                        strMailMsg += "<td>CHILD</td>";
                    }
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
                    {
                        strMailMsg += "<td>INFANT</td>";
                    }
                    strMailMsg += "</tr>";

                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["TripType"]) + "</td>";
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Trip"]) + "</td>";
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["NoOfPax"]) + "</td>";
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["AdultCount"]) + "</td>";
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
                    {
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ChildCount"]) + "</td>";
                    }
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
                    {
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["InfantCount"]) + "</td>";
                    }
                    strMailMsg += "</tr>";
                    strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
                    strMailMsg += "</table>";
                    if (MailContent.Tables[2].Rows.Count > 0)
                    {
                        strMailMsg += "<table style='width:100%;'>";
                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                        //strMailMsg += "<td>REQUESTID</td>";
                        strMailMsg += "<td>NAME</td>";
                        strMailMsg += "<td>PAX TYPE</td>";
                        strMailMsg += "<td>DOB</td>";
                        strMailMsg += "<td>GENDER</td>";
                        strMailMsg += "<td>TICKET NO.</td>";
                        if (MailContent.Tables[2].Rows[0]["TicketNumber_INBOND"].ToString() != "")
                        {
                            strMailMsg += "<td>INBOUND TICKET NO.</td>";
                        }
                        if (MailContent.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "international" && MailContent.Tables[2].Rows.Count > 0)
                        {
                            strMailMsg += "<td>PASSPORT NO.</td>";
                            strMailMsg += "<td>ISSUE COUNTRY</td>";
                            strMailMsg += "<td>NATIONALITY</td>";
                            strMailMsg += "<td>EX.DATE</td>";
                        }
                        strMailMsg += "</tr>";
                        int countpax = MailContent.Tables[2].Rows.Count;
                        for (int k = 0; k < countpax; k++)
                        {
                            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                            //strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["RequestID"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["Title"].ToString() + " " + MailContent.Tables[2].Rows[k]["FName"].ToString() + " " + MailContent.Tables[2].Rows[k]["LName"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PaxType"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["DOB"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["Gender"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["TicketNumber"]) + "</td>";
                            if (MailContent.Tables[2].Rows[k]["TicketNumber_INBOND"].ToString() != "")
                            {
                                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["TicketNumber_INBOND"]) + "</td>";
                            }
                            if (MailContent.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "international")
                            {
                                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PassportNo"]) + "</td>";
                                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["IssueCountryCode"]) + "</td>";
                                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["NationalityCode"]) + "</td>";
                                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["PassportExpireDate"]) + "</td>";
                            }
                            strMailMsg += "</tr>";
                        }
                        strMailMsg += "</table>";
                    }
                    MailDt = ObjST.GetMailingDetails("GroupBooking", "All");
                    if (MailDt.Tables[0].Rows.Count > 0)
                    {
                        string EIDStrMail = ConfigurationManager.AppSettings["EmailId"].ToString();
                        if (StrMail != "")
                        {
                            i = ObjGB.SendMail(StrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
                        }
                        else
                        {
                            i = ObjGB.SendMail(EIDStrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, "Agent Email-ID not found (Group Booking Details)", "");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "AdminNExecMailSending");
        }
        return i;
    }
    protected int AdminNExecMailSending(string RefRequestID, string AirlinePNR, string GDSPNR, string Inboundairline, string IndoundGDSPNR)
    {
        int i = 0;
        DataSet MailDt = new DataSet();
        DataSet MailContent = new DataSet();
        DataSet DSStatus = new DataSet();
        string strMailMsg = "";
        DSStatus = ObjGB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
        DataSet mailmsg = new DataSet();
        string STRSUB, MAILHEADER, MAILMESSAGE, _cmdtype;
        _cmdtype = Convert.ToString(DSStatus.Tables[0].Rows[0]["Status"]).ToUpper();
        mailmsg = ObjGB.GRP_MAILMSGSUBJECT(_cmdtype, "EXEC");
        STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
        MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
        MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
        try
        {
            MailContent = ObjGB.GroupRequestDetails(RefRequestID, "EXECMAILCONTENT", "", "");
            string REFSNO = Convert.ToString(MailContent.Tables[2].Rows[0]["REFSNO"]);
            DataSet AgencyDS = new DataSet();
            SqlTransaction ST = new SqlTransaction();
            AgencyDS = ST.GetAgencyDetails(Convert.ToString(MailContent.Tables[0].Rows[0]["CreatedBy"]));

            strMailMsg += "<table style='border: 1px width:100%;'>";
            strMailMsg += "<tr>";
            strMailMsg += "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
            strMailMsg += "<h2> " + MAILHEADER + " </h2>";
            strMailMsg += "</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr>";
            strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
            strMailMsg += "</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>AGENT ID</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>AGENCY NAME</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>EXPACTED PRICE</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKED PRICE</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["ExpactedPrice"]) + "</td>";
            strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookedPrice"]) + "</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
            if (MailContent.Tables[0].Rows.Count > 0)
            {
                strMailMsg += "<table style='border: 1px width:100%;'>";
                strMailMsg += "<tr>";
                strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>";
                strMailMsg += "";
                strMailMsg += "</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                //strMailMsg += "<td>REQUESTID</td>";
                strMailMsg += "<td>TRIP FROM</td>";
                strMailMsg += "<td>TRIP TO</td>";
                strMailMsg += "<td>DEP. DATE</td>";
                strMailMsg += "<td>DEP TIME</td>";
                strMailMsg += "<td>ARVL DATE</td>";
                strMailMsg += "<td>ARVL TIME</td>";
                strMailMsg += "<td>AIRLINE</td>";
                strMailMsg += "<td>FLIGHT NO.</td>";
                strMailMsg += "</tr>";
                DataTable DT = new DataTable();
                DT = MailContent.Tables[0].Select(String.Format("SNO = '{0}'", REFSNO)).CopyToDataTable();
                if (DT.Rows.Count > 0)
                {
                    for (int j = 0; j < DT.Rows.Count; j++)
                    {
                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                        //strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["RequestID"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Location"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Location"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Date"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Departure_Time"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Date"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Arrival_Time"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["Aircode"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["FlightNumber"]) + "</td>";
                        strMailMsg += "</tr>";
                    }
                    strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
                }
                strMailMsg += "</table>";
                strMailMsg += "<table style='width:100%;'>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td>TRIP TYPE</td>";
                strMailMsg += "<td>TRIP</td>";
                strMailMsg += "<td>TOTAL PASSANGERS</td>";
                strMailMsg += "<td>ADULT</td>";
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["ChildCount"].ToString()) > 0)
                {
                    strMailMsg += "<td>CHILD</td>";
                }
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["InfantCount"].ToString()) > 0)
                {
                    strMailMsg += "<td>INFANT</td>";
                }
                strMailMsg += "</tr>";

                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["TripType"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["Trip"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["NoOfPax"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["AdultCount"]) + "</td>";
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["ChildCount"].ToString()) > 0)
                {
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["ChildCount"]) + "</td>";
                }
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["InfantCount"].ToString()) > 0)
                {
                    strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["InfantCount"]) + "</td>";
                }
                strMailMsg += "</tr>";
                strMailMsg += "<tr><td colspan='9'><hr></td></tr>";
                strMailMsg += "</table>";
                if (MailContent.Tables[1].Rows.Count > 0)
                {
                    strMailMsg += "<table style='width:100%;'>";
                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                    //strMailMsg += "<td>REQUESTID</td>";
                    strMailMsg += "<td>NAME</td>";
                    strMailMsg += "<td>PAX TYPE</td>";
                    strMailMsg += "<td>DOB</td>";
                    strMailMsg += "<td>GENDER</td>";
                    if (MailContent.Tables[2].Rows[0]["Trip"].ToString() == "I")
                    {
                        strMailMsg += "<td>PASSPORT NO.</td>";
                        strMailMsg += "<td>ISSUE COUNTRY</td>";
                        strMailMsg += "<td>NATIONALITY</td>";
                        strMailMsg += "<td>EX.DATE</td>";
                    }
                    strMailMsg += "</tr>";
                    int countpax = MailContent.Tables[1].Rows.Count;
                    for (int k = 0; k < countpax; k++)
                    {
                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                        //strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["RequestID"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["Title"].ToString() + " " + MailContent.Tables[1].Rows[k]["FName"].ToString() + " " + MailContent.Tables[1].Rows[k]["LName"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PaxType"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["DOB"]) + "</td>";
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["Gender"]) + "</td>";
                        if (MailContent.Tables[2].Rows[0]["Trip"].ToString() == "I")
                        {
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PassportNo"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["IssueCountryCode"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["NationalityCode"]) + "</td>";
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["PassportExpireDate"]) + "</td>";
                        }
                        strMailMsg += "</tr>";
                    }
                    strMailMsg += "</table>";
                }
                MailDt = ObjST.GetMailingDetails("GroupBooking", Session["UID"].ToString());
                DataSet DSMail = new DataSet();
                DSMail = ObjGB.GroupRequestDetails("GroupBooking", "ConfigMail", "", "");
                if (MailDt.Tables[0].Rows.Count > 0 && DSMail.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < DSMail.Tables[0].Rows.Count; k++)
                    {
                        i = ObjGB.SendMail(DSMail.Tables[0].Rows[k]["ToEmail"].ToString(), MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "AdminNExecMailSending");
        }
        return i;
    }
}