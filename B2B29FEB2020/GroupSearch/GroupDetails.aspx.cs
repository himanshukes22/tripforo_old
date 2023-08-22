using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GRP_Booking;
using System.Globalization;

public partial class GroupSearch_GroupDetails : System.Web.UI.Page
{
    string PaymentStatus = "", FromDate = "", ToDate = "", RequestID = "", UserType = "", UserID = "";
    GroupBooking ObjGB = new GroupBooking();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (Page.IsPostBack != true)
            {
                if (Session["UID"] == null || Session["UID"].ToString() == "")
                {
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    UserType = Session["User_Type"].ToString();
                    UserID = Session["UID"].ToString();
                    PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                    ds = ObjGB.ShowBookingData("", PaymentStatus, "", "", UserType, UserID);
                    Session["ds"] = ds;
                    if (UserType.ToUpper() == "ADMIN" && ds.Tables[0].Rows.Count > 0)
                    {
                        GrpBookingDetails.Columns[10].Visible = false;
                        GrpBookingDetails.DataSource = ds;
                        GrpBookingDetails.DataBind();
                    }
                    if (UserType.ToUpper() == "AGENT" && ds.Tables[0].Rows.Count > 0)
                    {
                        GrpBookingDetails.Columns[10].Visible = false;
                        GrpBookingDetails.DataSource = ds;
                        GrpBookingDetails.DataBind();
                    }
                    if (UserType.ToUpper() == "EXEC" && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        DataRow[] results = ds.Tables[0].Select("AcceptBy='N' and RejectBy='N' and Status='Requested'");
                        if (results.Length > 0)
                        {
                            dt = results.CopyToDataTable();
                            GrpBookingDetails.DataSource = dt;
                            GrpBookingDetails.DataBind();
                            Session["dt"] = dt;
                        }
                        else
                        {
                            DivExec.InnerHtml = "No new request found!!";
                        }
                    }
                    else if (ds.Tables[0].Rows.Count <= 0)
                    {
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        GrpBookingDetails.DataSource = ds.Tables[0];
                        GrpBookingDetails.DataBind();
                        int columncount = GrpBookingDetails.Rows[0].Cells.Count;
                        GrpBookingDetails.Rows[0].Cells.Clear();
                        GrpBookingDetails.Rows[0].Cells.Add(new TableCell());
                        GrpBookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                        GrpBookingDetails.Rows[0].Cells[0].Text = "No Request Found";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsPageLoad");
        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_fromDate.Text.ToString() != "" && txt_todate.Text.ToString() != "")
        {
            if (DateTime.ParseExact(txt_fromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_todate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture))
            {
                txt_fromDate.Text = "";
                txt_todate.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", true);
            }
        }
        else
        {
            try
            {
                UserType = Session["User_Type"].ToString();
                UserID = Session["UID"].ToString();
                DataSet ds = new DataSet();
                PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                RequestID = txt_RequestID.Text.Trim().ToString();
                FromDate = txt_fromDate.Text.Trim().ToString();
                ToDate = txt_todate.Text.Trim().ToString();
                if (FromDate != "")
                {
                    FromDate = FromDate.Substring(6, 4) + "-" + FromDate.Substring(3, 2) + "-" + FromDate.Substring(0, 2) + " 00:00:00.001";
                }
                if (ToDate != "")
                {
                    ToDate = ToDate.Substring(6, 4) + "-" + ToDate.Substring(3, 2) + "-" + ToDate.Substring(0, 2) + " 23:59:59.999";
                }
                ds = ObjGB.ShowBookingData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
                Session["ds"] = ds;
                if (UserType.ToUpper() == "EXEC" && ds.Tables[0].Rows.Count > 0)
                {
                    GrpBookingDetails.Columns[10].Visible = false;
                    DataTable dt = new DataTable();
                    DataRow[] results = ds.Tables[0].Select("AcceptBy='N' and RejectBy='N'");
                    if (results.Length > 0)
                    {
                        dt = results.CopyToDataTable();
                        GrpBookingDetails.DataSource = dt;
                        GrpBookingDetails.DataBind();
                        Session["dt"] = dt;
                    }
                    else
                    {
                        DivExec.InnerHtml = "No new request found!!";
                    }
                }
                else if (UserType.ToUpper() == "ADMIN" && ds.Tables[0].Rows.Count > 0)
                {
                    GrpBookingDetails.Columns[10].Visible = false;
                    GrpBookingDetails.DataSource = ds;
                    GrpBookingDetails.DataBind();
                }
                else if (UserType.ToUpper() == "AGENT" && ds.Tables[0].Rows.Count > 0)
                {
                    GrpBookingDetails.Columns[10].Visible = false;
                    GrpBookingDetails.DataSource = ds;
                    GrpBookingDetails.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GrpBookingDetails.DataSource = ds;
                    GrpBookingDetails.DataBind();
                    int columncount = GrpBookingDetails.Rows[0].Cells.Count;
                    GrpBookingDetails.Rows[0].Cells.Clear();
                    GrpBookingDetails.Rows[0].Cells.Add(new TableCell());
                    GrpBookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    GrpBookingDetails.Rows[0].Cells[0].Text = "No Request Found";
                }
                txt_RequestID.Text = "";
                txt_fromDate.Text = "";
                txt_todate.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsBtnSubmit");
            }

        }
    }
    protected void GrpBookingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet mailmsg = new DataSet();
        string STRSUB, MAILHEADER, MAILMESSAGE;

        try
        {
            string status = "";
            if (e.CommandName == "Accept")
            {
                DataSet ds = new DataSet();
                RequestID = e.CommandArgument.ToString();
                UserID = Session["UID"].ToString();
                status = ObjGB.RequestAcceptReject(RequestID, UserID, "Accept", "");
                if (status == "Accepted")
                {
                    mailmsg = ObjGB.GRP_MAILMSGSUBJECT("EXECACCEPT", "EXEC");
                    STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
                    MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
                    MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
                    AdminNExecMailSending(RequestID, STRSUB, MAILHEADER, MAILMESSAGE, "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request has been accepted !!');window.location ='../GroupSearch/GroupDetails.aspx';", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                TextBox txtRemark = (TextBox)GrpBookingDetails.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit = (LinkButton)GrpBookingDetails.Rows[RowIndex].FindControl("lnkSubmit_1");
                LinkButton lnkHides = (LinkButton)GrpBookingDetails.Rows[RowIndex].FindControl("lnkHides_1");
                txtRemark.Visible = true;
                lnkSubmit.Visible = true;
                lnkHides.Visible = true;
            }
            if (e.CommandName == "CancelReqSubmit")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                TextBox txtRemark = (TextBox)GrpBookingDetails.Rows[RowIndex].FindControl("txtRemark");
                if (txtRemark.Text.Trim().ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
                }
                else
                {
                    RequestID = e.CommandArgument.ToString();
                    UserID = Session["UID"].ToString();
                    status = ObjGB.RequestAcceptReject(RequestID, UserID, "Reject", txtRemark.Text.Trim().ToString());
                    if (status == "Rejected")
                    {
                        int i, j;
                        mailmsg = ObjGB.GRP_MAILMSGSUBJECT("EXECREJECT", "EXEC");
                        STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
                        MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
                        MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
                        i = AdminNExecMailSending(RequestID, STRSUB, MAILHEADER, MAILMESSAGE, txtRemark.Text.Trim().ToString());
                        j = AgentMailSending(RequestID, txtRemark.Text.Trim().ToString());
                        if (i == 1 && j == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request has been rejected!!');window.location ='../GroupSearch/GroupDetails.aspx';", true);
                        }
                    }
                }
            }
            if (e.CommandName == "ReqCancel")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                TextBox txtRemark = (TextBox)GrpBookingDetails.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit = (LinkButton)GrpBookingDetails.Rows[RowIndex].FindControl("lnkSubmit_1");
                LinkButton lnkHides = (LinkButton)GrpBookingDetails.Rows[RowIndex].FindControl("lnkHides_1");
                txtRemark.Visible = false;
                lnkSubmit.Visible = false;
                lnkHides.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsRowCommand");
        }
    }
    protected int AdminNExecMailSending(string RefRequestID, string subject, string header, string StrMsgDetls, string RejecteRemarks)
    {
        int i = 0;
        try
        {
            DataSet MailDt = new DataSet();
            DataSet MailContent = new DataSet();
            string strMailMsg = "";
            MailContent = ObjGB.GroupRequestDetails(RefRequestID, "EXECMAILCONTENT", "", "");
            DataSet AgencyDS = new DataSet();
            SqlTransaction ST = new SqlTransaction();
            AgencyDS = ST.GetAgencyDetails(Convert.ToString(MailContent.Tables[0].Rows[0]["CreatedBy"]));
            strMailMsg += "<table style='border: 1px width:100%;'>";
            strMailMsg += "<tr>";
            strMailMsg += "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9' >";
            strMailMsg += "<h2> " + header + " </h2>";
            strMailMsg += "</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr>";
            strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + StrMsgDetls + "";
            strMailMsg += "</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>AGENT ID</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>AGENCY NAME</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
            if (RejecteRemarks != "")
            {
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REJECTED REMARKS</td>";
            }
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
            if (RejecteRemarks != "")
            {
                strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9' >" + RejecteRemarks + "</td>";
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
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>REQUESTID</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>TRIP FROM</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>TRIP TO</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>DEP. DATE</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>DEP TIME</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>ARVL DATE</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 10%; text-align: left; padding: 4px; font-weight: bold;'>ARVL TIME</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>AIRLINE</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>FLIGHT NO.</td>";
                strMailMsg += "</tr>";
                DataTable Saparate_DTOneWay = new DataTable();
                Saparate_DTOneWay = MailContent.Tables[0].DefaultView.ToTable(true, "SNO");
                for (int z = 0; z < Saparate_DTOneWay.Rows.Count; z++)
                {
                    DataTable DT = new DataTable();
                    string ReqID = Convert.ToString(Saparate_DTOneWay.Rows[z]["SNO"]);
                    DT = MailContent.Tables[0].Select(String.Format("SNO = '{0}'", ReqID)).CopyToDataTable();
                    if (DT.Rows.Count > 0)
                    {
                        for (int j = 0; j < DT.Rows.Count; j++)
                        {
                            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                            strMailMsg += "<td>" + Convert.ToString(DT.Rows[j]["RequestID"]) + "</td>";
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
                }
                strMailMsg += "</table>";
                strMailMsg += "<table style='width:100%;'>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td>TRIP TYPE</td>";
                strMailMsg += "<td>TRIP</td>";
                strMailMsg += "<td>TOTAL PASSANGERS</td>";
                strMailMsg += "<td >ADULT</td>";
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["ChildCount"].ToString()) > 0)
                {
                    strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>CHILD</td>";
                }
                if (Convert.ToInt32(MailContent.Tables[2].Rows[0]["InfantCount"].ToString()) > 0)
                {
                    strMailMsg += "<td style='font-size: 10.5px;  width: 25%; text-align: left; padding: 4px; font-weight: bold;'>INFANT</td>";
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
                strMailMsg += "</table>";
                MailDt = ObjST.GetMailingDetails("GroupBooking", Session["UID"].ToString());
                DataSet DSMail = new DataSet();
                DSMail = ObjGB.GroupRequestDetails("GroupBooking", "ConfigMail", "", "");
                if (MailDt.Tables[0].Rows.Count > 0 && DSMail.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < DSMail.Tables[0].Rows.Count; k++)
                    {
                        i = ObjGB.SendMail(DSMail.Tables[0].Rows[k]["ToEmail"].ToString(), MailDt.Tables[0].Rows[0]["MAILFROM"].ToString(), MailDt.Tables[0].Rows[0]["CC"].ToString(), "", MailDt.Tables[0].Rows[0]["SMTPCLIENT"].ToString(), MailDt.Tables[0].Rows[0]["UserID"].ToString(), MailDt.Tables[0].Rows[0]["Pass"].ToString(), strMailMsg, subject, "");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsAdminExecMail");
        }
        return i;
    }
    protected int AgentMailSending(string RefRequestID, string RejectedRemarks)
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
            string REFSNO = Convert.ToString(MailContent.Tables[1].Rows[0]["SNO"]);
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
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REJECTED REMARKS</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
                strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + RejectedRemarks + "</td>";
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
                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                        strMailMsg += "<td>REQUESTID</td>";
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
                            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[k]["RequestID"]) + "</td>";
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
    protected void GrpBookingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["USER_TYPE"].ToString() == "AGENT")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbSt = (Label)e.Row.FindControl("lblStatus");
                if (lbSt.Text.ToLower() == "ticketed")
                    e.Row.Cells[17].Visible = true;
                else
                {
                    e.Row.Cells[17].Visible = false;
                }
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[17].Visible = false;
            }
        }
    }
    protected void GrpBookingDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet strds = new DataSet();
            DataTable strdt = new DataTable();
            UserType = Session["User_Type"].ToString();
            GrpBookingDetails.PageIndex = e.NewPageIndex;
            if (UserType == "EXEC")
            {
                strdt = (DataTable)Session["dt"];
                GrpBookingDetails.DataSource = strdt;
                GrpBookingDetails.DataBind();
            }
            else
            {
                strds = (DataSet)Session["ds"];
                GrpBookingDetails.DataSource = strds;
                GrpBookingDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsPageIndex");
        }
    }
    protected void link_Invoice_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow GV = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblreq = (Label)GV.FindControl("lblRequestID");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../GroupSearch/InvoiceDetails.aspx?RequestID=" + lblreq.Text.Trim().ToString() + "','_newtab');", true);
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "link_Invoice_Clickgrpdtl");
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        if (txt_fromDate.Text.ToString() != "" && txt_todate.Text.ToString() != "")
        {
            if (DateTime.ParseExact(txt_fromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_todate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture))
            {
                txt_fromDate.Text = "";
                txt_todate.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", true);
            }
        }
        else
        {
            SqlTransactionDom STDom = new SqlTransactionDom();
            try
            {
                UserType = Session["User_Type"].ToString();
                UserID = Session["UID"].ToString();
                DataSet ds = new DataSet();
                PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                RequestID = txt_RequestID.Text.Trim().ToString();
                FromDate = txt_fromDate.Text.Trim().ToString();
                ToDate = txt_todate.Text.Trim().ToString();
                if (FromDate != "")
                {
                    FromDate = FromDate.Substring(6, 4) + "-" + FromDate.Substring(3, 2) + "-" + FromDate.Substring(0, 2) + " 00:00:00.001";
                }
                if (ToDate != "")
                {
                    ToDate = ToDate.Substring(6, 4) + "-" + ToDate.Substring(3, 2) + "-" + ToDate.Substring(0, 2) + " 23:59:59.999";
                }
                ds.Clear();
                ds = ObjGB.ShowBookingData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
                DataTable StrDataTable = new DataTable();
                STDom.ExportData(ds);
            }
            catch (Exception ex)
            {
                ErrorLogTrace.WriteErrorLog(ex, "ExportDetails");
            }
        }
    }
}


