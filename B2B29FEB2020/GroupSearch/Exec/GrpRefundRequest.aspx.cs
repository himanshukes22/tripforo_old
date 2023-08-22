using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GRP_Booking;

public partial class GroupSearch_Exec_GrpRefundRequest : System.Web.UI.Page
{
    DataSet DSCancel = new DataSet();
    string UserId = "", UserType = "";
    GroupBooking GB = new GroupBooking();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null || Session["UID"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            UserId = Session["UID"].ToString();
            UserType = Session["User_Type"].ToString();
            if (IsPostBack != true)
            {
                lbl_Norecord.Text = "";
                DSCancel = RefundRequest("", "CANCELREQUEST");
                GridRefundRequest.DataSource = DSCancel;
                GridRefundRequest.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex,"Page_Load_refundrequest");
        }
    }
    protected void GridRefundRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                GB.UPdateStatusREFUNDREQUEST(lbl_Reqid.Text.Trim().ToString(), "Accept", "", Session["UID"].ToString());
                GridRefundRequest.DataSource = RefundRequest("", "CANCELREQUEST");
                GridRefundRequest.DataBind();
            }
            if (e.CommandName == "RejectReq")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit");
                LinkButton lnkHides = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides");
                txtRemark.Visible = true;
                lnkSubmit.Visible = true;
                lnkHides.Visible = true;
            }
            if (e.CommandName == "Rejected")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit");
                LinkButton lnkHides = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides");
                txtRemark.Visible = true;
                lnkSubmit.Visible = true;
                lnkHides.Visible = true;
                if (txtRemark.Text.Trim().ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Remark can not be blank,Please Fill Remark');", true);
                }
                else
                {
                    int i, j;
                    GB.UPdateStatusREFUNDREQUEST(lbl_Reqid.Text.Trim().ToString(), "Rejected", txtRemark.Text.Trim().ToString(), Session["UID"].ToString());
                    GridRefundRequest.DataSource = RefundRequest("", "CANCELREQUEST");
                    GridRefundRequest.DataBind();
                    i = AgentMailSending(lbl_Reqid.Text.Trim().ToString(), txtRemark.Text.Trim().ToString());
                    j = AdminNExecMailSending(lbl_Reqid.Text.Trim().ToString(), txtRemark.Text.Trim().ToString());
                    if (i == 1 && j == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Request has been rejected!!');", true);
                    }
                }
            }
            if (e.CommandName == "CanceledReq")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit");
                LinkButton lnkHides = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides");
                txtRemark.Visible = false;
                lnkSubmit.Visible = false;
                lnkHides.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GridRefundRequest_RowCommand");
        }
      
    }
    public DataSet RefundRequest(string Requestid, string CMD_TYPE)
    {
        DataSet ds = new DataSet();
        ds = GB.REFUNDREQUEST("", CMD_TYPE);
        return ds;
    }
    protected void GridRefundRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridRefundRequest.PageIndex = e.NewPageIndex;
            DSCancel = RefundRequest("", "CANCELREQUEST");
            GridRefundRequest.DataSource = DSCancel;
            GridRefundRequest.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GridRefundRequest_PageIndexChanging");
        }
    }
    protected int AgentMailSending(string RefRequestID, string RejectRemarks)
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
            DSStatus = GB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
            _cmdtype = "REJECTEDPAID";
            mailmsg = GB.GRP_MAILMSGSUBJECT(_cmdtype, "AGENT");
            STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
            MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
            MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
            MailContent = GB.GroupRequestDetails(RefRequestID, "MAILCONTENTAGENT", "", "");
            string REFSNO = Convert.ToString(MailContent.Tables[0].Rows[0]["REFSNO"]);
            if (DSStatus.Tables[0].Rows.Count > 0)
            {
                StrMail = GB.GETEMAILID(RefRequestID, "AGENT");
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
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REJECTED REMARKS</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING AMOUNT</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
                strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + RejectRemarks + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookedPrice"]) + "</td>";
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
                            i = GB.SendMail(StrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
                        }
                        else
                        {
                            i = GB.SendMail(EIDStrMail, MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, "Agent Email-ID not found (Group Booking Details)", "");
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
    protected int AdminNExecMailSending(string RefRequestID, string RejectRemarks)
    {
        int i = 0;
        DataSet MailDt = new DataSet();
        DataSet MailContent = new DataSet();
        DataSet DSStatus = new DataSet();
        string strMailMsg = "";
        DSStatus = GB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
        DataSet mailmsg = new DataSet();
        string STRSUB, MAILHEADER, MAILMESSAGE, _cmdtype;
        _cmdtype = "REJECTEDPAID";
        mailmsg = GB.GRP_MAILMSGSUBJECT(_cmdtype, "EXEC");
        STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
        MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
        MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
        try
        {
            MailContent = GB.GroupRequestDetails(RefRequestID, "EXECMAILCONTENT", "", "");
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
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING AMOUNT</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REJECTED REMARKS</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookedPrice"]) + "</td>";
            strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + RejectRemarks + "</td>";
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
                DSMail = GB.GroupRequestDetails("GroupBooking", "ConfigMail", "", "");
                if (MailDt.Tables[0].Rows.Count > 0 && DSMail.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < DSMail.Tables[0].Rows.Count; k++)
                    {
                        i = GB.SendMail(DSMail.Tables[0].Rows[k]["ToEmail"].ToString(), MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, STRSUB, "");
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