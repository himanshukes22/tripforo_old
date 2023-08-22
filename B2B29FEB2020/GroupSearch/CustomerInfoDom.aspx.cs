using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using GRP_Booking;
public partial class GroupSearch_CustomerInfoDom : System.Web.UI.Page
{
    DataSet PaxCountDS = new DataSet();
    int AdultCount, ChildCount, InfantCount, i = 0;
    GroupBooking ObjGB = new GroupBooking();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    DataSet MailDt = new DataSet();
    DataSet MailContent = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null || Session["UID"].ToString() == "")
            {
                Response.Redirect("../GroupBookingLogin.aspx");
            }
            else if (Page.IsPostBack != true)
            {
                if (Request.QueryString["RefRequestID"] != null || Request.QueryString["RefRequestID"] != "")
                {
                    PaxCountDS = ObjGB.GroupRequestDetails(Request.QueryString["RefRequestID"], "PAXCOUNTINFO", "", "");
                    AdultCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["AdultCount"].ToString());
                    ChildCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["ChildCount"].ToString());
                    InfantCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["InfantCount"].ToString());
                    Bind_pax(AdultCount, ChildCount, InfantCount);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "CustomerInfoDomPageLoad");
        }
    }
    public void Bind_pax(int cntAdult, int cntChild, int cntInfant)
    {
        try
        {
            DataTable PaxTbl = new DataTable();
            DataColumn cntTblColumn = null;
            cntTblColumn = new DataColumn();
            cntTblColumn.DataType = Type.GetType("System.Double");
            cntTblColumn.ColumnName = "Counter";
            PaxTbl.Columns.Add(cntTblColumn);
            cntTblColumn = new DataColumn();
            cntTblColumn.DataType = Type.GetType("System.String");
            cntTblColumn.ColumnName = "PaxTP";
            PaxTbl.Columns.Add(cntTblColumn);
            DataRow cntrow = null;
            for (int i = 1; i <= cntAdult; i++)
            {
                cntrow = PaxTbl.NewRow();
                cntrow["Counter"] = i;
                cntrow["PaxTP"] = "Passenger " + i.ToString() + " (Adult)";
                PaxTbl.Rows.Add(cntrow);
            }
            Repeater_Adult.DataSource = PaxTbl;
            Repeater_Adult.DataBind();
            PaxTbl.Clear();
            if (cntChild > 0)
            {
                for (int i = 1; i <= cntChild; i++)
                {
                    cntrow = PaxTbl.NewRow();
                    cntrow["Counter"] = i;
                    cntrow["PaxTP"] = "Passenger " + i.ToString() + " (Child)";
                    PaxTbl.Rows.Add(cntrow);
                }
                Repeater_Child.DataSource = PaxTbl;
                Repeater_Child.DataBind();
            }
            PaxTbl.Clear();
            if (cntInfant > 0)
            {
                for (int i = 1; i <= cntInfant; i++)
                {
                    cntrow = PaxTbl.NewRow();
                    cntrow["Counter"] = i;
                    cntrow["PaxTP"] = "Passenger " + i.ToString() + " (Infant)";
                    PaxTbl.Rows.Add(cntrow);
                }
                Repeater_Infant.DataSource = PaxTbl;
                Repeater_Infant.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "CustomerInfoDomBindPax");
        }
    }
    protected void book_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Request.QueryString["RefRequestID"] != null || Request.QueryString["RefRequestID"] != "") && Request.QueryString["Status"] == "PAID")
            {
                InsertPaxDetail(Request.QueryString["RefRequestID"]);
                int j = 0, k = 0;
                j = AgentMailSending(Request.QueryString["RefRequestID"]);
                k = AdminNExecMailSending(Request.QueryString["RefRequestID"]);
                if (j == 1 && k == 1)
                {
                    string linktype = Request.QueryString["Payment"].ToString();
                    if (linktype.ToLower() == "off")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Thanks for pax information, will get back to you soon!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + Request.QueryString["RefRequestID"] + "';", true);
                    }
                    else if (linktype.ToLower() == "on" || linktype.ToLower() == "pg")
                    {
                        if (Request.QueryString["PG"].ToString() == "Y")
                        {
                            ObjGB.UPDATEPAYMENTSTATUS(Request.QueryString["RefRequestID"].ToString(), Session["UID"].ToString(), "UPDATED", "PG");
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Thanks for pax information, will get back to you soon!!');window.opener.location.reload(true);self.close();", true);
                    }
                    else if (linktype.ToLower() == "pax")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Thanks for pax information, will get back to you soon!!');window.opener.location.reload(true);self.close();", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "CustomerInfoDomBookClick");
        }
    }
    public void InsertPaxDetail(string trackid)
    {
        try
        {
            PaxCountDS = ObjGB.GroupRequestDetails(trackid, "PAXCOUNTINFO", "", "");
            if (PaxCountDS.Tables[0].Rows.Count > 0)
            {
                AdultCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["AdultCount"].ToString());
                ChildCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["ChildCount"].ToString());
                InfantCount = Convert.ToInt32(PaxCountDS.Tables[0].Rows[0]["InfantCount"].ToString());
                int counter = 0;
                foreach (RepeaterItem rw in Repeater_Adult.Items)
                {
                    counter += 1;
                    DropDownList ddl_ATitle = (DropDownList)rw.FindControl("ddl_ATitle");
                    DropDownList ddl_AGender = (DropDownList)rw.FindControl("ddl_AGender");
                    TextBox txtAFirstName = (TextBox)rw.FindControl("txtAFirstName");
                    TextBox txtAMiddleName = (TextBox)rw.FindControl("txtAMiddleName");
                    if (txtAMiddleName.Text == "Middle Name")
                    {
                        txtAMiddleName.Text = "";
                    }
                    TextBox txtALastName = (TextBox)rw.FindControl("txtALastName");
                    string gender = "F";
                    if (ddl_ATitle.SelectedValue.Trim().ToLower() == "dr" || ddl_ATitle.SelectedValue.Trim().ToLower() == "prof")
                    {
                        gender = ddl_AGender.SelectedValue.Trim();
                    }
                    else if (ddl_ATitle.SelectedValue.Trim().ToLower() == "mr")
                    {
                        gender = "M";
                    }
                    TextBox txtadultDOB = (TextBox)rw.FindControl("Txt_AdtDOB");
                    string DOB = "";
                    DOB = txtadultDOB.Text.Trim();
                    if (counter <= InfantCount)
                    {
                        ObjGB.GRP_InsertPaxDetails(trackid, ddl_ATitle.SelectedValue.Trim(), txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), "ADT", DOB, gender, "", "", "", "", "DOM", "Y");
                    }
                    else
                    {
                        ObjGB.GRP_InsertPaxDetails(trackid, ddl_ATitle.SelectedValue.Trim(), txtAFirstName.Text.Trim(), txtAMiddleName.Text.Trim(), txtALastName.Text.Trim(), "ADT", DOB, gender, "", "", "", "", "DOM", "Y");
                    }
                }
                if (ChildCount > 0)
                {
                    foreach (RepeaterItem rw in Repeater_Child.Items)
                    {
                        DropDownList ddl_CTitle = (DropDownList)rw.FindControl("ddl_CTitle");
                        TextBox txtCFirstName = (TextBox)rw.FindControl("txtCFirstName");
                        TextBox txtCMiddleName = (TextBox)rw.FindControl("txtCMiddleName");
                        if (txtCMiddleName.Text == "Middle Name")
                        {
                            txtCMiddleName.Text = "";
                        }
                        TextBox txtCLastName = (TextBox)rw.FindControl("txtCLastName");
                        TextBox txtchildDOB = (TextBox)rw.FindControl("Txt_chDOB");
                        string DOB = "";
                        DOB = txtchildDOB.Text.Trim();
                        string gender = "F";
                        if (ddl_CTitle.SelectedValue.Trim().ToLower() == "mstr")
                        {
                            gender = "M";
                        }
                        ObjGB.GRP_InsertPaxDetails(trackid, ddl_CTitle.SelectedValue.Trim(), txtCFirstName.Text.Trim(), txtCMiddleName.Text.Trim(), txtCLastName.Text.Trim(), "CHD", DOB, gender, "", "", "", "", "DOM", "Y");
                    }
                }
                if (InfantCount > 0)
                {
                    int counter1 = 0;
                    foreach (RepeaterItem rw in Repeater_Infant.Items)
                    {
                        DropDownList ddl_ITitle = (DropDownList)rw.FindControl("ddl_ITitle");
                        TextBox txtIFirstName = (TextBox)rw.FindControl("txtIFirstName");
                        TextBox txtIMiddleName = (TextBox)rw.FindControl("txtIMiddleName");
                        if (txtIMiddleName.Text == "Middle Name")
                        {
                            txtIMiddleName.Text = "";
                        }
                        TextBox txtILastName = (TextBox)rw.FindControl("txtILastName");
                        TextBox txtinfantDOB = (TextBox)rw.FindControl("Txt_InfantDOB");
                        string DOB = "";
                        DOB = txtinfantDOB.Text.Trim();
                        string gender = "F";
                        if (ddl_ITitle.SelectedValue.Trim().ToLower() == "mstr")
                        {
                            gender = "M";
                        }
                        TextBox txtAFirstName = (TextBox)Repeater_Adult.Items[counter1].FindControl("txtAFirstName");
                        TextBox txtAMiddleName = (TextBox)Repeater_Adult.Items[counter1].FindControl("txtAMiddleName");
                        TextBox txtALastName = (TextBox)Repeater_Adult.Items[counter1].FindControl("txtALastName");
                        string Name = "";
                        Name = txtAFirstName.Text.Trim() + txtAMiddleName.Text.Trim() + txtALastName.Text.Trim();
                        if (counter1 <= InfantCount)
                        {
                            ObjGB.GRP_InsertPaxDetails(trackid, ddl_ITitle.SelectedValue.Trim(), txtIFirstName.Text.Trim(), txtIMiddleName.Text.Trim(), txtILastName.Text.Trim(), "INF", DOB, gender, "", "", "", "", "DOM", "Y");
                        }
                        counter1 += 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "CustomerInfoDomInsertPax");
        }
    }
    protected int AgentMailSending(string RefRequestID)
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
            if (_cmdtype=="FREEZED")
            {
                _cmdtype = "PAID";
            }
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
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKED PRICE</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
                strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookedPrice"]) + "</td>";
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

                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px; color: #4f6b72;'>";
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
                        if (MailContent.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "international" && MailContent.Tables[2].Rows.Count>0)
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
    protected int AdminNExecMailSending(string RefRequestID)
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
        if (_cmdtype == "FREEZED")
        {
            _cmdtype = "PAID";
        }
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
            strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
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
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif;	color: #4f6b72;	border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px;	text-transform: uppercase;	text-align: left;	padding: 6px 6px 6px 12px;'>";
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

                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px; color: #4f6b72;'>";
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