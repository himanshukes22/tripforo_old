using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using GRP_Booking;


public partial class validation : System.Web.UI.Page
{
    string RefRequestNo = "";
    string TripType = "", Trip = "";
    string ProbableDays = "", CfmDate = "", ExpectedFair = "", Remarks = "", BookingName = "",Rbl_Trip = "", Rbl_TripType = "", RefRequestId = "";
    string RequestId = "", OStops = "";
    int RetValue;
    DataSet RequestIDDs = new DataSet();
    DataTable OBFltDs = new DataTable();
    DataTable IBFltDs = new DataTable();
    DataSet AirportInfoDs = new DataSet();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    TextBox Ofrom, ODepDate, ODepTime, OTo, OArvlDate, OArvlTime, OAirline, OFlightNo;
    TextBox Rfrom, RTo, RDepDate, RDepTime, RArvlDate, RArvlTime, RAirline, RFlightNo;
    Label ODepAirportCode, OArvlAirportCode, OGridStops, OTripType, OSector;
    Label RDepAirportCode, RArvlAirportCode, RGridStops, RGridTripType, RSector;
    RandomKeyGenerator RandomKey = new RandomKeyGenerator();
    GroupBooking ObjGB = new GroupBooking();
    string StrOBFlt = "", StrIBFlt = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null || Session["UID"].ToString() == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else if (Page.IsPostBack != true)
            {
                string QueryString = HttpContext.Current.Request.QueryString["RequestId"].ToString();
                if (QueryString != "")
                {
                    RefRequestNo = QueryString;
                }
                RequestIDDs = ObjGB.GetFltDtlsGroupBooking(RefRequestNo, Session["UID"].ToString());
                TripType = RequestIDDs.Tables[0].Rows[0]["TripType"].ToString().Trim();
                Trip = RequestIDDs.Tables[0].Rows[0]["Trip"].ToString().Trim();
                RetValue = Convert.ToInt16(RequestIDDs.Tables[0].Rows[RequestIDDs.Tables[0].Rows.Count - 1]["Flight"]);
                if (RetValue == 2)
                {
                    rbtnRoundTrip.Checked = true;
                }
                else
                {
                    rbtnOneWay.Checked = true;
                }
                if (Trip.ToUpper() == "I")
                {
                    rbtnInternational.Checked = true;
                }
                else
                {
                    rbtnDomestic.Checked = true;
                }
                if (Trip == "D" && RetValue == 2)
                {
                    StrOBFlt = "O";
                    StrIBFlt = "R";
                    DataTable Ods = new DataTable();
                    DataTable Rds = new DataTable();
                    DataTable Saparate_OutBound = new DataTable();
                    DataTable Saparate_InBound = new DataTable();
                    Ods = RequestIDDs.Tables[0].Select(String.Format("TripType = '{0}'", StrOBFlt)).CopyToDataTable();
                    Rds = RequestIDDs.Tables[0].Select(String.Format("TripType = '{0}'", StrIBFlt)).CopyToDataTable();
                    Saparate_OutBound = Ods.DefaultView.ToTable(true, "Track_id", "ValiDatingCarrier");
                    Saparate_InBound = Rds.DefaultView.ToTable(true, "Track_id", "ValiDatingCarrier");
                    for (int i = 0; i < Saparate_OutBound.Rows.Count; i++)
                    {
                        string OTrackId = "", OVC = "";
                        OTrackId = Saparate_OutBound.Rows[i]["Track_id"].ToString();
                        OVC = Saparate_OutBound.Rows[i]["ValiDatingCarrier"].ToString();
                        for (int j = 0; j < Saparate_InBound.Rows.Count; j++)
                        {
                            string ITrackid = "", IVC = "";
                            ITrackid = Saparate_InBound.Rows[j]["Track_id"].ToString();
                            IVC = Saparate_InBound.Rows[j]["ValiDatingCarrier"].ToString();
                            if (OTrackId == ITrackid && OVC != IVC)
                            {
                                foreach (DataRow row in RequestIDDs.Tables[0].Select(String.Format("Track_id = '{0}' and TripType = '{1}'", ITrackid, StrIBFlt)))
                                {
                                    row[86] = ITrackid + "-N";
                                }
                                RequestIDDs.Tables[0].AcceptChanges();
                            }
                        }
                    }
                    OBFltDs = RequestIDDs.Tables[0];
                    if (OBFltDs.Rows.Count > 0)
                    {
                        if (IBFltDs.Rows.Count > 0)
                        {
                            RetValue = 2;
                        }
                        Trip = OBFltDs.Rows[0]["Trip"].ToString().Trim();
                        if (OBFltDs.Rows.Count > 0 && Trip.ToUpper() == "D")
                        {
                            OneWayTrip.DataSource = OBFltDs;
                            OneWayTrip.DataBind();
                        }
                    }
                }
                else
                {
                    if (Trip == "I" && RetValue == 2)
                    {
                        DataTable Saparate_DTRoundTrip = new DataTable();
                        StrOBFlt = "O";
                        StrIBFlt = "R";
                        OBFltDs = RequestIDDs.Tables[0];
                    }
                    else if (Trip == "I" && RetValue == 1)
                    {
                        StrOBFlt = "O";
                        OBFltDs = RequestIDDs.Tables[0].Select(String.Format("TripType = '{0}'", StrOBFlt)).CopyToDataTable();
                    }
                    else if (Trip == "D")
                    {
                        OBFltDs = RequestIDDs.Tables[0];
                    }
                    if (OBFltDs.Rows.Count > 0)
                    {
                        if (IBFltDs.Rows.Count > 0)
                        {
                            RetValue = 2;
                        }
                        Trip = OBFltDs.Rows[0]["Trip"].ToString().Trim();

                        if (OBFltDs.Rows.Count > 0 && Trip.ToUpper() == "I")
                        {
                            OneWayTrip.DataSource = RequestIDDs;
                            OneWayTrip.DataBind();
                        }
                        if (OBFltDs.Rows.Count > 0 && Trip.ToUpper() == "D")
                        {
                            OneWayTrip.DataSource = OBFltDs;
                            OneWayTrip.DataBind();
                            if (RetValue == 2)
                            {
                            }
                        }
                    }
                }
                CfmDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ValidationPageLoad");
        }
    }
    protected void OneWayTrip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataSet Airportinfodep = new DataSet();
                DataSet AirportinfoArvl = new DataSet();
                string depinfo = "", ArvlInfo = "";
                TextBox txt_OFrom = (TextBox)e.Row.FindControl("txt_OFrom");
                TextBox txt_OTo = (TextBox)e.Row.FindControl("txt_OTo");
                TextBox ODepDate = (TextBox)e.Row.FindControl("txt_ODepDate");
                TextBox ODepTime = (TextBox)e.Row.FindControl("txt_ODepTime");
                TextBox OArvlDate = (TextBox)e.Row.FindControl("txt_OArvlDate");
                TextBox OArvlTime = (TextBox)e.Row.FindControl("txt_OArvlTime");
                Label Provider = (Label)e.Row.FindControl("lbl_Provider");
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_odeploction");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_ArrivalLocation");

                if (Provider.Text.Trim() == "1G")
                {
                    ODepDate.Text = ODepDate.Text.Trim().Substring(6, 2) + "/" + ODepDate.Text.Trim().Substring(4, 2) + "/" + ODepDate.Text.Trim().Substring(0, 4);
                    OArvlDate.Text = OArvlDate.Text.Trim().Substring(6, 2) + "/" + OArvlDate.Text.Trim().Substring(4, 2) + "/" + OArvlDate.Text.Trim().Substring(0, 4);
                }
                else
                {
                    ODepDate.Text = ODepDate.Text.Trim().Substring(0, 2) + "/" + ODepDate.Text.Trim().Substring(2, 2) + "/" + "20" + ODepDate.Text.Trim().Substring(4, 2);
                    OArvlDate.Text = OArvlDate.Text.Trim().Substring(0, 2) + "/" + OArvlDate.Text.Trim().Substring(2, 2) + "/" + "20" + OArvlDate.Text.Trim().Substring(4, 2);
                }
                ODepTime.Text = ODepTime.Text.Trim().Substring(0, 2) + ":" + ODepTime.Text.Trim().Substring(2, 2);
                OArvlTime.Text = OArvlTime.Text.Trim().Substring(0, 2) + ":" + OArvlTime.Text.Trim().Substring(2, 2);
                Airportinfodep = ObjGB.AirportInfo(lbl_odeploction.Text.Trim());
                AirportinfoArvl = ObjGB.AirportInfo(lbl_ArrivalLocation.Text.Trim());
                if (Airportinfodep.Tables[0].Rows.Count > 0)
                {
                    depinfo = Airportinfodep.Tables[0].Rows[0]["AirportInfo"].ToString();
                }
                if (AirportinfoArvl.Tables[0].Rows.Count > 0)
                {
                    ArvlInfo = AirportinfoArvl.Tables[0].Rows[0]["AirportInfo"].ToString();
                }
                txt_OFrom.Text = depinfo.Trim().ToString();
                txt_OTo.Text = ArvlInfo.Trim().ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ValidationOWRowBound");
        }

    }
    protected void RoundTrip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Provider = "";
                if (IBFltDs.Rows.Count > 0)
                {
                    Provider = IBFltDs.Rows[0]["Provider"].ToString().Trim();
                }
                else
                {
                    Provider = IBFltDs.Rows[0]["Provider"].ToString().Trim();
                }
                TextBox RDepDate = (TextBox)e.Row.FindControl("txt_RDepDate");
                TextBox RDepTime = (TextBox)e.Row.FindControl("txt_RDepTime");
                TextBox RArvlDate = (TextBox)e.Row.FindControl("txt_RArvlDate");
                TextBox RArvlTime = (TextBox)e.Row.FindControl("txt_RArvlTime");
                if (Provider == "1G")
                {
                    RDepDate.Text = RDepDate.Text.Trim().Substring(6, 2) + "/" + RDepDate.Text.Trim().Substring(4, 2) + "/" + RDepDate.Text.Trim().Substring(0, 4);
                    RArvlDate.Text = RArvlDate.Text.Trim().Substring(6, 2) + "/" + RArvlDate.Text.Trim().Substring(4, 2) + "/" + RArvlDate.Text.Trim().Substring(0, 4);
                }
                else
                {
                    RDepDate.Text = RDepDate.Text.Trim().Substring(0, 2) + "/" + RDepDate.Text.Trim().Substring(2, 2) + "/" + "20" + RDepDate.Text.Trim().Substring(4, 2);
                    RArvlDate.Text = RArvlDate.Text.Trim().Substring(0, 2) + "/" + RArvlDate.Text.Trim().Substring(2, 2) + "/" + "20" + RArvlDate.Text.Trim().Substring(4, 2);
                }
                RDepTime.Text = RDepTime.Text.Trim().Substring(0, 2) + ":" + RDepTime.Text.Trim().Substring(2, 2);
                RArvlTime.Text = RArvlTime.Text.Trim().Substring(0, 2) + ":" + RArvlTime.Text.Trim().Substring(2, 2);
            }

        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ValidationRTRowBound");
        }
    }
    protected void bnt_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            RefRequestId = Request.QueryString["RequestId"];
            if (RefRequestId != "" || RefRequestId == null)
            {
                string DepTime = "", ArvlTime = "";
                foreach (GridViewRow OW in OneWayTrip.Rows)
                {
                    Ofrom = (TextBox)OW.FindControl("txt_OFrom");
                    ODepDate = (TextBox)OW.FindControl("txt_ODepDate");
                    ODepTime = (TextBox)OW.FindControl("txt_ODepTime");
                    DepTime = ODepTime.Text.Substring(0, 5);
                    ODepAirportCode = (Label)OW.FindControl("lbl_ODepAirportName");
                    OTo = (TextBox)OW.FindControl("txt_OTo");
                    OArvlDate = (TextBox)OW.FindControl("txt_OArvlDate");
                    OArvlTime = (TextBox)OW.FindControl("txt_OArvlTime");
                    ArvlTime = OArvlTime.Text.Substring(0, 5);
                    OArvlAirportCode = (Label)OW.FindControl("lbl_OArvlAirportName");
                    OAirline = (TextBox)OW.FindControl("txt_OAirline");
                    OFlightNo = (TextBox)OW.FindControl("txt_OFlightNo");
                    OGridStops = (Label)OW.FindControl("lbl_OStops");
                    OTripType = (Label)OW.FindControl("lbl_OTripType");
                    OSector = (Label)OW.FindControl("lbl_OSector");
                    Label OFlight = (Label)OW.FindControl("lbl_Oflight");
                    Label ORequestID = (Label)OW.FindControl("lbl_ORequestID");
                    Label lbl_Oflt = (Label)OW.FindControl("lbl_Oflt");
                    Label Lbl_sno = (Label)OW.FindControl("lbl_sno");
                    Label lbl_vc = (Label)OW.FindControl("lbl_vc");
                    ObjGB.INSERTFLIGHTDETAILS(ORequestID.Text.Trim(), Ofrom.Text.Trim().ToString(), ODepDate.Text.Trim().ToString(), DepTime, OArvlAirportCode.Text.Trim().ToString(), OTo.Text.Trim().ToString(), OArvlDate.Text.Trim().ToString(),
                        ArvlTime, ODepAirportCode.Text.Trim().ToString(), OAirline.Text.Trim().ToString(), OFlightNo.Text.Trim().ToString(), OGridStops.Text.Trim().ToString(), OTripType.Text.Trim().ToString(),
                        Session["UID"].ToString(), OSector.Text.Trim().ToString(), RefRequestId, lbl_Oflt.Text.Trim(), OFlight.Text.Trim(), Convert.ToInt32(Lbl_sno.Text.Trim().ToString()), lbl_vc.Text.Trim().ToString());
                }

                ProbableDays = "0";
                string TtlAdt = txtNoOfAdult.Value.Trim().ToString();
                string TtlChd = txtNoOfChild.Value.Trim().ToString();
                if (TtlChd == "")
                {
                    TtlChd = "0";
                }
                string TtlInf = txtNoOfInfant.Value.Trim().ToString();
                if (TtlInf == "")
                {
                    TtlInf = "0";
                }
                int TotalPassnger = Convert.ToInt32(TtlAdt) + Convert.ToInt32(TtlChd) + Convert.ToInt32(TtlInf);
                CfmDate = DateTime.Now.ToString("dd/MM/yyyy");
                ExpectedFair = txtExpectedFair.Value.Trim().ToString();
                Remarks = textarea1.Value.Trim().ToString();
                BookingName = txt_Bookingname.Text.Trim().ToString();
                if (rbtnDomestic.Checked == true)
                {
                    Rbl_Trip = "D";
                }
                if (rbtnInternational.Checked == true)
                {
                    Rbl_Trip = "I";
                }
                if (rbtnOneWay.Checked == true)
                {
                    Rbl_TripType = "O";
                }
                if (rbtnRoundTrip.Checked == true)
                {
                    Rbl_TripType = "R";
                }

                ObjGB.INSERTBOOKINGDETAILS(RefRequestId, Rbl_TripType, Rbl_Trip, TotalPassnger, Convert.ToInt32(TtlAdt), Convert.ToInt32(TtlChd), Convert.ToInt32(TtlInf), "Flight", Convert.ToDecimal(ExpectedFair), "Requested", Remarks, Session["UID"].ToString(), ProbableDays, Session["User_Type"].ToString(), BookingName);
               DataSet mailmsg = new DataSet();
               string STRSUB, MAILHEADER, MAILMESSAGE;
               mailmsg= ObjGB.GRP_MAILMSGSUBJECT("REQUESTED", "AGENT");
               STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
               MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
               MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);
               AdminNExecMailSending(RefRequestId, STRSUB, MAILHEADER, MAILMESSAGE);
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your request has been submitted!!');window.location ='../GroupSearch/GroupDetails.aspx?RefRequestID=" + RefRequestId + "';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Some thing went wrong,please try after sometime');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ValidationBtnSubmit");
        }

    }
    protected int AdminNExecMailSending(string RefRequestID,string subject,string header,string message)
    {
        int i = 0;
        DataSet MailDt = new DataSet();
        DataSet MailContent = new DataSet();
        DataSet DSStatus = new DataSet();
        string strMailMsg = "";
        try
        {
            {
                MailContent = ObjGB.GroupRequestDetails(RefRequestID, "MAILCONTENTAGENT", "", RefRequestID);
                strMailMsg = "<table>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9' ><h2> " + header + " </h2>";
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9' > "+message+"";
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Agent ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["CreatedBy"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Booking Name: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Group Booking ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b> Travel Date: </b>" + Convert.ToString(MailContent.Tables[1].Rows[0]["Departure_Date"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Journey Plan: </b>" + Convert.ToString(MailContent.Tables[1].Rows[0]["Sector"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Trip: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Trip"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>TripType: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["TripType"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "<tr>";
                strMailMsg = strMailMsg + "<td><b>Total Pax: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["NoOfPax"]);
                strMailMsg = strMailMsg + "</td>";
                strMailMsg = strMailMsg + "</tr>";
                strMailMsg = strMailMsg + "</table>";
                MailDt = ObjST.GetMailingDetails("GroupBooking", "All");
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
            ErrorLogTrace.WriteErrorLog(ex, "InstantmailAdminExecMail");
        }
        return i;
    }
    [WebMethod]
    public static List<Airlines> GetAllAlNms(string AlName)
    {
        List<Airlines> lstAl = new List<Airlines>();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand("USP_GET_AIRLINES_NAMES", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AlName", AlName);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    lstAl.Add(new Airlines
                    {
                        Alname = dr["AL_NAME"].ToString(),
                        AlCode = dr["AL_Code"].ToString()
                    });
                }
            }
            cmd.Dispose();
            con.Close();
            return lstAl;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return lstAl;
        }
    }
    ////[WebMethod]
    public static string SendMailFltBkng(string OrderNo, string ToMailID)
    {
        string HasSent = "Failure";
        string HtmlBody = "";
        SqlConnection conMail = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            SqlDataAdapter da1 = new SqlDataAdapter("USP_GET_GRPBKG_RECORD", conMail);
            da1.SelectCommand.CommandType = CommandType.StoredProcedure;
            da1.SelectCommand.Parameters.AddWithValue("@BKGNO", OrderNo);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    HtmlBody += "<table cellpadding='2' cellspacting='1' border='1' style='border-collapse:collapse; margin-left:50px;'>";
                    HtmlBody += "<thead>";
                    HtmlBody += "<tr>";
                    HtmlBody += "<th colspan='6' style='background-color: #18184b; color:#fff; font-weight:bold; text-align:left;'>";
                    HtmlBody += "Book A Group";
                    HtmlBody += "</th>";
                    HtmlBody += "</tr>";
                    HtmlBody += "</thead>";
                    HtmlBody += "<tbody>";
                    HtmlBody += "<tr>";
                    HtmlBody += "<td style='font-weight:bold;'>";
                    HtmlBody += "Requested By ";
                    HtmlBody += "</td>";
                    HtmlBody += "<td colspan='5'>";
                    HtmlBody += ds.Tables[0].Rows[0]["requestedby"].ToString();
                    HtmlBody += "</td>";
                    HtmlBody += "</tr>";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        HtmlBody += "<tr>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Service Type ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["servicetype"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Requested Date ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["requesteddate"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Requested Id ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["requestid"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "</tr>";
                        HtmlBody += "<tr>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Airline Name ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["Airline"].ToString(); ;
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Departure From ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["depart_loc"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Arrival To ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["arrival_loc"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "</tr>";
                        HtmlBody += "<tr>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Departure Date ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["departdate"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Departure Time ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["departtime"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Domestic Or International ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["DomOrIntl"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "</tr>";
                        HtmlBody += "<tr>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Flight Number ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["flight_num"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "<td style='font-weight:bold;'>";
                        HtmlBody += "Trip Type ";
                        HtmlBody += "</td>";
                        HtmlBody += "<td>";
                        HtmlBody += ds.Tables[0].Rows[i]["trip_type"].ToString();
                        HtmlBody += "</td>";
                        HtmlBody += "</tr>";
                    }
                    HtmlBody += "<tr>";
                    HtmlBody += "<td style='font-weight:bold;'>";
                    HtmlBody += "Number Of Passenger ";
                    HtmlBody += "</td>";
                    HtmlBody += "<td>";
                    HtmlBody += ds.Tables[0].Rows[0]["no_of_psgr"].ToString();
                    HtmlBody += "</td>";
                    HtmlBody += "<td style='font-weight:bold;'>";
                    HtmlBody += "Remarks ";
                    HtmlBody += "</td>";
                    HtmlBody += "<td>";
                    HtmlBody += ds.Tables[0].Rows[0]["remarks"].ToString();
                    HtmlBody += "</td>";
                    HtmlBody += "<td style='font-weight:bold;'>";
                    HtmlBody += "Probable Days ";
                    HtmlBody += "</td>";
                    HtmlBody += "<td>";
                    HtmlBody += ds.Tables[0].Rows[0]["probable_days"].ToString();
                    HtmlBody += "</td>";
                    HtmlBody += "</tr>";
                    HtmlBody += "<tr>";
                    HtmlBody += "<td style='font-weight:bold;'>";
                    HtmlBody += "Expected Fare ";
                    HtmlBody += "</td>";
                    HtmlBody += "<td colspan='5'>";
                    HtmlBody += ds.Tables[0].Rows[0]["amountpaid"].ToString();
                    HtmlBody += "</td>";
                    HtmlBody += "</tr>";
                    HtmlBody += "</tbody>";
                    HtmlBody += "</table>";
                }
            }

            SqlTransactionDom STDOM = new SqlTransactionDom();
            DataTable dt = new DataTable();
            dt = STDOM.GetMailingDetails("GROUPBOOKING", "").Tables[0];

            if ((dt.Rows.Count > 0))
            {
                bool Status = false;
                Status = Convert.ToBoolean(dt.Rows[0]["Status"].ToString());
                try
                {
                    if (Status == true)
                    {
                        int i = SendMail(ToMailID, dt.Rows[0]["MAILFROM"].ToString(), dt.Rows[0]["BCC"].ToString(), dt.Rows[0]["CC"].ToString(), dt.Rows[0]["SMTPCLIENT"].ToString(), dt.Rows[0]["UserId"].ToString(), dt.Rows[0]["Pass"].ToString(), HtmlBody, dt.Rows[0]["SUBJECT"].ToString(), "");
                        if (i == 1)
                        {
                            HasSent = "Success";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //// mailmsg.Text = "Unable to send mail.Please contact to administrator";
            }
            return HasSent;
        }
        catch (Exception ex)
        {
            return HasSent;
        }
    }
    [WebMethod]
    public static List<AirCityNams> FetchCityList(string city)
    {
        List<AirCityNams> LstArCT = new List<AirCityNams>();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand("CityAutoSearch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@param1", city);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    LstArCT.Add(new AirCityNams
                    {
                        AirportCode = dr["AirportCode"].ToString(),
                        CityName = dr["CityName"].ToString(),
                        CountryCode = dr["CountryCode"].ToString()
                    });
                }
            }
            dr.Close();
            cmd.Dispose();
            con.Close();
            return LstArCT;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return LstArCT;
        }

    }
    [WebMethod]
    ////////public string IsSbmtFltDet(string DepCT,string DepDate,string DepPrefDTime,string ArrCT,string ArrDate,string ArrPrefTime,string ReqBy,string ReqDate,
    public static string IsSbmtFltDet(ArrayList DepCT, ArrayList DepDate, ArrayList DepPrefDTime, ArrayList ArrCT, string ArrDate,
    string ReqBy, string HOD, string PNR, string OpsExec, string PayStts, string BkgStts, string AmtPaid, string SvcType, string IsOffcl,
    string UsrType, string TripType, ArrayList FltName, ArrayList FltNo, string Remks, ArrayList AirLine, string DomOrInt, string NoOfPgr, string ProblDays, string CfmDate)
    {
        string numResent = "";
        string isssaved = "Not Saved";
        int rows = 0;
        string prtime = "";
        string arrdate = "";
        string fltnameopt = "";
        string fltnoopt = "";
        string mailtosend = "";
        if (CfmDate != "")
        {
            var cmfrevdate = CfmDate.Split('/');
            CfmDate = cmfrevdate[2] + "/" + cmfrevdate[1] + "/" + cmfrevdate[0];
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            for (int f = 0; f < DepCT.Count; f++)
            {
                if (DepPrefDTime.Count > 0)
                {
                    if (!string.IsNullOrEmpty(((object[])(DepPrefDTime[f]))[0].ToString()) && DepPrefDTime[f] != null)
                    {
                        prtime = ((object[])(DepPrefDTime[f]))[0].ToString().Split('=')[1].Trim();
                    }
                }

                if (FltName.Count > 0)
                {
                    if (!string.IsNullOrEmpty(((object[])(FltName[f]))[0].ToString()) && FltName[f] != null)
                    {
                        fltnameopt = ((object[])(FltName[f]))[0].ToString().Split(':')[1].Trim();
                    }
                }


                if (FltNo.Count > 0)
                {
                    if (!string.IsNullOrEmpty(((object[])(FltNo[f]))[0].ToString()) && FltNo[f] != null)
                    {
                        fltnoopt = ((object[])(FltNo[f]))[0].ToString().Split(':')[1].Trim();
                    }
                }

                SqlCommand cmd = new SqlCommand("USP_INST_FLTBKG_REQ_DET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DEPCT", ((object[])(DepCT[f]))[0].ToString().Split(':')[1].Trim());
                cmd.Parameters.AddWithValue("@DEPDATE", ((object[])(DepDate[f]))[0].ToString().Split(':')[1].Trim());
                cmd.Parameters.AddWithValue("@DEPPREFTIME", prtime);
                cmd.Parameters.AddWithValue("@ARRCT", ((object[])(ArrCT[f]))[0].ToString().Split(':')[1].Trim());
                cmd.Parameters.AddWithValue("@FLTNAME", fltnameopt);
                cmd.Parameters.AddWithValue("@FLTNO", fltnoopt);
                cmd.Parameters.AddWithValue("@ARRDATE", arrdate);
                cmd.Parameters.AddWithValue("@REQBY", ReqBy);
                cmd.Parameters.AddWithValue("@HOD", HOD);
                cmd.Parameters.AddWithValue("@PNR", PNR);
                cmd.Parameters.AddWithValue("@OPSEXEC", OpsExec);
                cmd.Parameters.AddWithValue("@PAYSTTS", PayStts);
                cmd.Parameters.AddWithValue("@BKGSTTS", BkgStts);
                cmd.Parameters.AddWithValue("@AMTPAID", AmtPaid);
                cmd.Parameters.AddWithValue("@SVCTYPE", SvcType);
                cmd.Parameters.AddWithValue("@ISOFFCL", IsOffcl);
                cmd.Parameters.AddWithValue("@USERTYPE", UsrType);
                cmd.Parameters.AddWithValue("@TRIPTYPE", TripType);
                cmd.Parameters.AddWithValue("@REMKS", Remks);
                cmd.Parameters.AddWithValue("@UNQRESENT", numResent);
                cmd.Parameters.AddWithValue("@AIRLINE", fltnameopt);
                cmd.Parameters.AddWithValue("@DomOrIntl", DomOrInt);
                cmd.Parameters.AddWithValue("@NOOFPSGR", NoOfPgr);
                cmd.Parameters.AddWithValue("@PROBDAYS", ProblDays);
                cmd.Parameters.AddWithValue("@CFMDATE", CfmDate);
                con.Open();

                numResent = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                con.Close();
            }
            if (!string.IsNullOrEmpty(numResent) && numResent.ToString().Trim().Contains("LNBBOOKING"))
            {
                isssaved = "Saved_" + numResent;
                if (DomOrInt.ToString().Trim().ToLower() == "domestic")
                {
                    mailtosend = "dilpreet@looknbook.com";
                }
                else
                {
                    mailtosend = "intlgroups@looknbook.com";
                }
                SendMailFltBkng(numResent, mailtosend);
            }
            return isssaved;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return isssaved + "_" + numResent;
        }
    }
    public static int SendMail(string toEMail, string @from, string bcc, string cc, string smtpClient, string userID, string pass, string body, string subject, string AttachmentFile)
    {
        System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
        System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
        msgMail.To.Clear();
        msgMail.To.Add(new System.Net.Mail.MailAddress(toEMail));
        msgMail.From = new System.Net.Mail.MailAddress(@from);
        if (!string.IsNullOrEmpty(bcc))
        {
            msgMail.Bcc.Add(new System.Net.Mail.MailAddress(bcc));
        }
        if (!string.IsNullOrEmpty(cc))
        {
            msgMail.CC.Add(cc);
        }
        if (!string.IsNullOrEmpty(AttachmentFile))
        {
            msgMail.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFile));
        }

        msgMail.Subject = subject;
        msgMail.IsBodyHtml = true;
        msgMail.Body = body;

        try
        {
            objMail.Credentials = new System.Net.NetworkCredential(userID, pass);
            objMail.Host = smtpClient;
            objMail.Send(msgMail);
            return 1;

        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
            return 0;
        }
    }
}
public class AirCityNams
{
    public string AirportCode { get; set; }
    public string CityName { get; set; }
    public string CountryCode { get; set; }
}
public class FltBkgDA
{
    static SqlConnection con;
    public FltBkgDA(string constring)
    {
        con = new SqlConnection(constring);
    }
    public static List<UserLogin> ValidateUser(string uid, string pwd)
    {
        List<UserLogin> LstUser = new List<UserLogin>();
        try
        {
            SqlCommand cmd = new SqlCommand("USP_VALIDATE_FLTBKG_LOGIN", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@UPWD", pwd);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ////SqlDataAdapter da = new SqlDataAdapter(cmd);
            ////DataSet ds = new DataSet();
            ////con.Close();
            ////da.Fill(ds);
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    LstUser.Add(new UserLogin
                    {
                        Uid = dr["UID"].ToString(),
                        UPwd = dr["UPWD"].ToString(),
                        Uname = dr["UNAME"].ToString(),
                        UType = dr["UTYPE"].ToString()
                    });
                }
            }
            dr.Close();
            cmd.Dispose();
            con.Close();
            return LstUser;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return LstUser;
        }
    }
}
public class Airlines
{
    public string Alname { get; set; }
    public string AlCode { get; set; }
}
public class UserLogin
{
    public string Uid { get; set; }
    public string UPwd { get; set; }
    public string Uname { get; set; }
    public string UType { get; set; }
}

