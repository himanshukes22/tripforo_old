using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ITZLib;
using GRP_Booking;
using PG;

public partial class GroupSearch_GroupRequestidDetails : System.Web.UI.Page
{
    string RequestID = "", RefRequestID = "";
    string INAirline = "", Ingds = "";
    string TripType = "", Trip = "", AdultCount = "", ChildCount = "", InfantCount = "", Remarks = "", ExpactedFare = "", Jdate = "", ProbDays = "", PaymentLink = "", Url = "";
    TextBox from, DepDate, DepTime, To, ArvlDate, ArvlTime, Airline, FlightNo;
    Label GbdCounter, GfdCounter;
    GroupBooking ObjGB = new GroupBooking();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    DataSet ClientReqDS = new DataSet();
    DataSet PriceQutDS = new DataSet();
    DataSet CustInfoDS = new DataSet();
    DataSet MailContent = new DataSet();
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
    string PaymentStsRst = "";
    string TodayDate = "";
    string validoffer;
    int NoOfDays;
    string CustomerPaymentStatus = "";
    int ResultAgentMail = 0, ResultAdminMail = 0, SNO;
    string IsPaxDtl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            if (Session["UID"] == null || Session["UID"].ToString() == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                try
                {
                    RequestID = Request.QueryString["RequestID"];
                    if (Session["User_Type"].ToString().ToUpper() == "AGENT")
                    {
                        DataSet DSPQuote = new DataSet();
                        DSPQuote = ObjGB.GroupRequestDetails(RequestID, "PRICEQUOTE", "", "");
                        if (DSPQuote.Tables[0].Rows.Count <= 0)
                        {
                            CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "CUSTOMERINFO", "", "");
                            if (CustInfoDS.Tables[0].Rows.Count > 0 && CustInfoDS.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "REQUESTED" || CustInfoDS.Tables[0].Rows.Count > 0 && CustInfoDS.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "ACCEPTED")
                            {
                                Div_Agent_EditBooikgINFO.Visible = true;
                                OneWayTrip.DataSource = CustInfoDS;
                                OneWayTrip.DataBind();
                                IsPaxDtl = CustInfoDS.Tables[0].Rows[0]["IsPaxDeatils"].ToString();
                                if (IsPaxDtl.ToLower() == "true")
                                {
                                    tr_PaxDetails.Visible = true;
                                    txtNoOfAdult.Attributes.Add("readonly", "readonly");
                                    txtNoOfChild.Attributes.Add("readonly", "readonly");
                                    txtNoOfInfant.Attributes.Add("readonly", "readonly");
                                    if (CustInfoDS.Tables[1].Rows.Count > 0)
                                    {
                                        if (CustInfoDS.Tables[0].Rows[0]["Trip"].ToString().ToUpper() == "D")
                                        {
                                            paxdetails.Columns[7].Visible = false;
                                            paxdetails.Columns[8].Visible = false;
                                            paxdetails.Columns[9].Visible = false;
                                            paxdetails.Columns[10].Visible = false;
                                            paxdetails.DataSource = CustInfoDS.Tables[1];
                                            paxdetails.DataBind();
                                        }
                                        else
                                        {
                                            paxdetails.DataSource = CustInfoDS.Tables[1];
                                            paxdetails.DataBind();
                                        }
                                    }
                                }
                                TripType = CustInfoDS.Tables[0].Rows[0]["TripType"].ToString();
                                Trip = CustInfoDS.Tables[0].Rows[0]["Trip"].ToString();
                                AdultCount = CustInfoDS.Tables[0].Rows[0]["AdultCount"].ToString();
                                ChildCount = CustInfoDS.Tables[0].Rows[0]["ChildCount"].ToString();
                                InfantCount = CustInfoDS.Tables[0].Rows[0]["InfantCount"].ToString();
                                Remarks = CustInfoDS.Tables[0].Rows[0]["Remarks"].ToString();
                                ExpactedFare = CustInfoDS.Tables[0].Rows[0]["ExpactedPrice"].ToString();
                                Jdate = CustInfoDS.Tables[0].Rows[0]["CreatedDate"].ToString();
                                ProbDays = CustInfoDS.Tables[0].Rows[0]["ProbableDays"].ToString();
                                txtNoOfAdult.Value = AdultCount;
                                txtNoOfChild.Value = ChildCount;
                                txtNoOfInfant.Value = InfantCount;
                                textarea1.Value = Remarks;
                                txtNoOfAdult.Value = AdultCount;
                                txtExpectedFair.Value = ExpactedFare;
                                if (Trip == "I")
                                {
                                    rbtnInternational.Checked = true;
                                }
                                else
                                {
                                    rbtnDomestic.Checked = true;
                                }
                                if (TripType == "R")
                                {
                                    rbtnRoundTrip.Checked = true;
                                }
                                else
                                {
                                    rbtnOneWay.Checked = true;
                                }
                            }
                            if (CustInfoDS.Tables[0].Rows.Count > 0 && (CustInfoDS.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "FREEZED" || CustInfoDS.Tables[0].Rows[0]["Status"].ToString() == "Cancelled" || CustInfoDS.Tables[0].Rows[0]["Status"].ToString().ToLower() == "paid") || CustInfoDS.Tables[0].Rows[0]["Status"].ToString() == "Rejected" || CustInfoDS.Tables[0].Rows[0]["Status"].ToString() == "Refunded")
                            {
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
                                lbl_BkgSts.Text = CustInfoDS.Tables[0].Rows[0]["Status"].ToString().ToUpper();
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
                        else if (DSPQuote.Tables[0].Rows.Count > 0)
                        {
                            CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "CLIENTREQUEST", Session["UID"].ToString(), Session["User_Type"].ToString());
                            if (CustInfoDS.Tables[1].Rows.Count > 0)
                            {
                                {
                                    string paymentstatus = "";
                                    paymentstatus = CustInfoDS.Tables[1].Rows[0]["Status"].ToString();
                                    if (paymentstatus.ToLower() == "requested")
                                    {
                                        Div_editpaxinfo.Visible = false;
                                        div_paxinfo.Visible = false;
                                        IsPaxDtl = CustInfoDS.Tables[1].Rows[0]["IsPaxDeatils"].ToString();
                                        if (IsPaxDtl.ToLower() == "true")
                                        {
                                            if (CustInfoDS.Tables[1].Rows.Count > 0)
                                            {
                                                if (CustInfoDS.Tables[0].Rows[0]["Trip"].ToString().ToUpper() == "DOMESTIC")
                                                {
                                                    GridView1.Columns[7].Visible = false;
                                                    GridView1.Columns[8].Visible = false;
                                                    GridView1.Columns[9].Visible = false;
                                                    GridView1.Columns[10].Visible = false;
                                                    GridView1.DataSource = CustInfoDS.Tables[3];
                                                    GridView1.DataBind();
                                                }
                                                else
                                                {
                                                    GridView1.DataSource = CustInfoDS.Tables[3];
                                                    GridView1.DataBind();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Div_editpaxinfo.Visible = false;
                                    }
                                    Div_Exec.Visible = true;
                                    DataTable DTOneWay = new DataTable();
                                    DataTable Saparate_DTOneWay = new DataTable();
                                    Saparate_DTOneWay = CustInfoDS.Tables[0].DefaultView.ToTable(true, "SNO");
                                    DataTable DTRoundTrip = new DataTable();
                                    DataTable Saparate_DTRoundTrip = new DataTable();
                                    if (Saparate_DTOneWay.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < Saparate_DTOneWay.Rows.Count; i++)
                                        {
                                            string Oid = "";
                                            Oid = Saparate_DTOneWay.Rows[i]["SNO"].ToString();
                                            DataSet DSOPrrice = new DataSet();
                                            switch (i)
                                            {
                                                case 0:
                                                    OFlightDetails1.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    OFlightDetails1.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails1.Visible = true;
                                                        PriceQuote1.Visible = true;
                                                        GVPriceQuote1.Columns[6].Visible = false;
                                                        GVPriceQuote1.Columns[8].Visible = false;
                                                        GVPriceQuote1.Columns[5].Visible = false;
                                                        GVPriceQuote1.Columns[2].Visible = false;
                                                        GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote1.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails1.Visible = false;
                                                        PriceQuote1.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails1.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote1.Columns[9].Visible = false;
                                                            GVPriceQuote1.Columns[8].Visible = false;
                                                            GVPriceQuote1.Columns[6].Visible = true;
                                                            GVPriceQuote1.Columns[5].Visible = false;
                                                            GVPriceQuote1.Columns[2].Visible = false;

                                                            GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote1.DataBind();
                                                            PriceQuote1.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote1.Columns[9].Visible = true;
                                                                    GVPriceQuote1.Columns[6].Visible = false;
                                                                    GVPriceQuote1.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 1:
                                                    OFlightDetails2.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    OFlightDetails2.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails2.Visible = true;
                                                        PriceQuote2.Visible = true;
                                                        GVPriceQuote2.Columns[6].Visible = false;
                                                        GVPriceQuote2.Columns[8].Visible = false;
                                                        GVPriceQuote2.Columns[5].Visible = false;
                                                        GVPriceQuote2.Columns[2].Visible = false;
                                                        GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote2.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails2.Visible = false;
                                                        PriceQuote2.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails2.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote2.Columns[9].Visible = false;
                                                            GVPriceQuote2.Columns[8].Visible = false;
                                                            GVPriceQuote2.Columns[6].Visible = true;
                                                            GVPriceQuote2.Columns[5].Visible = false;
                                                            GVPriceQuote2.Columns[2].Visible = false;
                                                            GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote2.DataBind();
                                                            PriceQuote2.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote2.Columns[9].Visible = true;
                                                                    GVPriceQuote2.Columns[6].Visible = false;
                                                                    GVPriceQuote2.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    OFlightDetails3.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    OFlightDetails3.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails3.Visible = true;
                                                        PriceQuote3.Visible = true;
                                                        GVPriceQuote3.Columns[6].Visible = false;
                                                        GVPriceQuote3.Columns[8].Visible = false;
                                                        GVPriceQuote3.Columns[5].Visible = false;
                                                        GVPriceQuote3.Columns[2].Visible = false;
                                                        GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote3.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails3.Visible = false;
                                                        PriceQuote3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails3.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote3.Columns[9].Visible = false;
                                                            GVPriceQuote3.Columns[8].Visible = false;
                                                            GVPriceQuote3.Columns[6].Visible = true;
                                                            GVPriceQuote3.Columns[5].Visible = false;
                                                            GVPriceQuote3.Columns[2].Visible = false;
                                                            GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote3.DataBind();
                                                            PriceQuote3.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote3.Columns[9].Visible = true;
                                                                    GVPriceQuote3.Columns[6].Visible = false;
                                                                    GVPriceQuote3.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    OFlightDetails4.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    OFlightDetails4.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails4.Visible = true;
                                                        PriceQuote4.Visible = true;
                                                        GVPriceQuote4.Columns[6].Visible = false;
                                                        GVPriceQuote4.Columns[8].Visible = false;
                                                        GVPriceQuote3.Columns[5].Visible = false;
                                                        GVPriceQuote4.Columns[2].Visible = false;
                                                        GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote4.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails4.Visible = false;
                                                        PriceQuote4.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails4.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote4.Columns[9].Visible = false;
                                                            GVPriceQuote4.Columns[8].Visible = false;
                                                            GVPriceQuote4.Columns[6].Visible = true;
                                                            GVPriceQuote4.Columns[5].Visible = false;
                                                            GVPriceQuote4.Columns[2].Visible = false;
                                                            GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote4.DataBind();
                                                            PriceQuote4.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote4.Columns[9].Visible = true;
                                                                    GVPriceQuote4.Columns[6].Visible = false;
                                                                    GVPriceQuote4.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    OFlightDetails5.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    OFlightDetails5.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails5.Visible = true;
                                                        PriceQuote5.Visible = true;
                                                        GVPriceQuote5.Columns[6].Visible = false;
                                                        GVPriceQuote5.Columns[8].Visible = false;
                                                        GVPriceQuote5.Columns[5].Visible = false;
                                                        GVPriceQuote5.Columns[2].Visible = false;
                                                        GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote5.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails5.Visible = false;
                                                        PriceQuote5.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails5.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote5.Columns[9].Visible = false;
                                                            GVPriceQuote5.Columns[8].Visible = false;
                                                            GVPriceQuote5.Columns[6].Visible = true;
                                                            GVPriceQuote5.Columns[5].Visible = false;
                                                            GVPriceQuote5.Columns[2].Visible = false;
                                                            GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote5.DataBind();
                                                            PriceQuote5.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote5.Columns[9].Visible = true;
                                                                    GVPriceQuote5.Columns[6].Visible = false;
                                                                    GVPriceQuote5.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 5:
                                                    RFlightDetails1.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    RFlightDetails1.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails6.Visible = true;
                                                        PriceQuote6.Visible = true;
                                                        GVPriceQuote6.Columns[6].Visible = false;
                                                        GVPriceQuote6.Columns[8].Visible = false;
                                                        GVPriceQuote6.Columns[5].Visible = false;
                                                        GVPriceQuote6.Columns[2].Visible = false;
                                                        GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote6.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails6.Visible = false;
                                                        PriceQuote6.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails6.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote6.Columns[9].Visible = false;
                                                            GVPriceQuote6.Columns[8].Visible = false;
                                                            GVPriceQuote6.Columns[6].Visible = true;
                                                            GVPriceQuote6.Columns[5].Visible = false;
                                                            GVPriceQuote6.Columns[2].Visible = false;
                                                            GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote6.DataBind();
                                                            PriceQuote6.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote6.Columns[9].Visible = true;
                                                                    GVPriceQuote6.Columns[6].Visible = false;
                                                                    GVPriceQuote6.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 6:
                                                    RFlightDetails2.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    RFlightDetails2.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails7.Visible = true;
                                                        PriceQuote7.Visible = true;
                                                        GVPriceQuote7.Columns[6].Visible = false;
                                                        GVPriceQuote7.Columns[8].Visible = false;
                                                        GVPriceQuote7.Columns[5].Visible = false;
                                                        GVPriceQuote7.Columns[2].Visible = false;
                                                        GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote7.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails7.Visible = false;
                                                        PriceQuote7.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails7.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote7.Columns[9].Visible = false;
                                                            GVPriceQuote7.Columns[8].Visible = false;
                                                            GVPriceQuote7.Columns[6].Visible = true;
                                                            GVPriceQuote7.Columns[5].Visible = false;
                                                            GVPriceQuote7.Columns[2].Visible = false;
                                                            GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote7.DataBind();
                                                            PriceQuote7.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote7.Columns[9].Visible = true;
                                                                    GVPriceQuote7.Columns[6].Visible = false;
                                                                    GVPriceQuote7.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;

                                                case 7:
                                                    RFlightDetails3.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    RFlightDetails3.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails8.Visible = true;
                                                        PriceQuote8.Visible = true;
                                                        GVPriceQuote8.Columns[6].Visible = false;
                                                        GVPriceQuote8.Columns[8].Visible = false;
                                                        GVPriceQuote8.Columns[5].Visible = false;
                                                        GVPriceQuote8.Columns[2].Visible = false;
                                                        GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote8.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails8.Visible = false;
                                                        PriceQuote8.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails8.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote8.Columns[9].Visible = false;
                                                            GVPriceQuote8.Columns[8].Visible = false;
                                                            GVPriceQuote8.Columns[6].Visible = true;
                                                            GVPriceQuote8.Columns[5].Visible = false;
                                                            GVPriceQuote8.Columns[2].Visible = false;
                                                            GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote8.DataBind();
                                                            PriceQuote8.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote8.Columns[9].Visible = true;
                                                                    GVPriceQuote8.Columns[6].Visible = false;
                                                                    GVPriceQuote8.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 8:
                                                    RFlightDetails4.DataSource = CustInfoDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                                    RFlightDetails4.DataBind();
                                                    DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                                    if (DSOPrrice.Tables[0].Rows.Count > 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails9.Visible = true;
                                                        PriceQuote9.Visible = true;
                                                        GVPriceQuote9.Columns[6].Visible = false;
                                                        GVPriceQuote9.Columns[8].Visible = false;
                                                        GVPriceQuote9.Columns[5].Visible = false;
                                                        GVPriceQuote9.Columns[2].Visible = false;
                                                        GVPriceQuote9.DataSource = DSOPrrice.Tables[0];
                                                        GVPriceQuote9.DataBind();
                                                    }
                                                    else if (DSOPrrice.Tables[0].Rows.Count <= 0 && (DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || DSOPrrice.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED"))
                                                    {
                                                        div_flightdetails9.Visible = false;
                                                        PriceQuote9.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        div_flightdetails9.Visible = true;
                                                        if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                        {
                                                            GVPriceQuote9.Columns[9].Visible = false;
                                                            GVPriceQuote9.Columns[8].Visible = false;
                                                            GVPriceQuote9.Columns[6].Visible = true;
                                                            GVPriceQuote9.Columns[5].Visible = false;
                                                            GVPriceQuote9.Columns[2].Visible = false;
                                                            GVPriceQuote9.DataSource = DSOPrrice.Tables[0];
                                                            GVPriceQuote9.DataBind();
                                                            PriceQuote9.Visible = true;
                                                            if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                            {
                                                                CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                                if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                                {
                                                                    GVPriceQuote9.Columns[9].Visible = true;
                                                                    GVPriceQuote9.Columns[6].Visible = false;
                                                                    GVPriceQuote9.Columns[5].Visible = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                if (CustInfoDS.Tables[1].Rows.Count > 0)
                                {
                                    if (CustInfoDS.Tables[1].Rows[0]["Status"].ToString() == "Cancelled" || CustInfoDS.Tables[1].Rows[0]["Status"].ToString() == "Cancellation Requested" || CustInfoDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Refunded" || CustInfoDS.Tables[1].Rows[0]["Status"].ToString() == "Rejected")
                                    {
                                        PriceQuote1.Visible = false;
                                        PriceQuote2.Visible = false;
                                        PriceQuote3.Visible = false;
                                        PriceQuote4.Visible = false;
                                        PriceQuote5.Visible = false;
                                        PriceQuote6.Visible = false;
                                        PriceQuote7.Visible = false;
                                        PriceQuote8.Visible = false;
                                        PriceQuote9.Visible = false;
                                        PriceQuote10.Visible = false;
                                        BookingDetails.Columns[7].Visible = false;
                                        BookingDetails.Columns[8].Visible = false;
                                        BookingDetails.Columns[9].Visible = false;
                                        div_BookingDetails.Visible = true;
                                        BookingDetails.DataSource = CustInfoDS.Tables[1];
                                        BookingDetails.DataBind();
                                    }
                                    else
                                    {
                                        if (CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "freezed")
                                        {
                                            BookingDetails.Columns[9].Visible = true;
                                        }
                                        else if ((CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid" || CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed") && CustInfoDS.Tables[1].Rows[0]["IsPaxDeatils"].ToString().ToLower() == "false")
                                        {
                                            BookingDetails.Columns[9].Visible = false;
                                            BookingDetails.Columns[10].Visible = true;
                                        }
                                        else if ((CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid" || CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed") && CustInfoDS.Tables[1].Rows[0]["IsPaxDeatils"].ToString().ToLower() == "true")
                                        {
                                            BookingDetails.Columns[4].Visible = false;
                                            BookingDetails.Columns[9].Visible = false;
                                            BookingDetails.Columns[13].Visible = true;
                                        }
                                        else
                                        {
                                            BookingDetails.Columns[9].Visible = false;
                                        }
                                        BookingDetails.Columns[7].Visible = false;
                                        div_BookingDetails.Visible = true;
                                        BookingDetails.DataSource = CustInfoDS.Tables[1];
                                        BookingDetails.DataBind();
                                        if (CustInfoDS.Tables[3].Rows.Count > 0)
                                        {
                                            div_paxinfo.Visible = true;
                                            if (CustInfoDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic" && CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                                            {
                                                pax_info.Columns[4].Visible = false;
                                                pax_info.Columns[5].Visible = false;
                                                pax_info.Columns[6].Visible = false;
                                                pax_info.Columns[7].Visible = false;
                                                pax_info.DataSource = CustInfoDS.Tables[3];
                                                pax_info.DataBind();
                                            }
                                            else if (CustInfoDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() != "domestic" && CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                                            {
                                                pax_info.DataSource = CustInfoDS.Tables[3];
                                                pax_info.DataBind();
                                            }
                                            else
                                            {
                                                div_TktInfo.Visible = true;
                                                div_paxinfo.Visible = false;
                                                DataSet DSPax = new DataSet();
                                                DSPax = ObjGB.PAXINFO(CustInfoDS.Tables[3].Rows[0]["RequestID"].ToString());
                                                if (DSPax.Tables[0].Rows[0]["Trip"].ToString() == "D")
                                                {
                                                    tktInfromation.Columns[6].Visible = false;
                                                    tktInfromation.Columns[7].Visible = false;
                                                    tktInfromation.Columns[8].Visible = false;
                                                }
                                                tktInfromation.DataSource = DSPax;
                                                tktInfromation.DataBind();
                                                lbl_gdspnr.Text = DSPax.Tables[0].Rows[0]["GDSPNR"].ToString();
                                                lbl_airlinePnr.Text = DSPax.Tables[0].Rows[0]["AIRLINEPNR"].ToString();
                                                Ingds = DSPax.Tables[0].Rows[0]["INGDSPNR"].ToString();
                                                INAirline = DSPax.Tables[0].Rows[0]["INAIRLINEPNR"].ToString();
                                                if (Ingds != "" && INAirline != "")
                                                {
                                                    tr_Inbond_pnrgds.Visible = true;
                                                    lbl_inairline.Text = INAirline;
                                                    lbl_inbondgdspnr.Text = Ingds;
                                                    tktInfromation.Columns[3].Visible = true;
                                                }
                                                else
                                                {
                                                    tr_Inbond_pnrgds.Visible = false;
                                                    tktInfromation.Columns[3].Visible = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (CustInfoDS.Tables[3].Rows.Count > 0 && CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower().Trim() != "ticketed")
                                {
                                    div_paxinfo.Visible = true;
                                    if (CustInfoDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                                    {
                                        pax_info.Columns[4].Visible = false;
                                        pax_info.Columns[5].Visible = false;
                                        pax_info.Columns[6].Visible = false;
                                        pax_info.Columns[7].Visible = false;
                                        pax_info.DataSource = CustInfoDS.Tables[3];
                                        pax_info.DataBind();
                                    }
                                    else
                                    {
                                        pax_info.DataSource = CustInfoDS.Tables[3];
                                        pax_info.DataBind();
                                    }
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('No record found with your login id, please try after some time !!');", true);
                            }
                        }
                    }
                    if (Session["User_Type"].ToString().ToUpper() == "EXEC")
                    {
                        string Accept = "", Reject = "";
                        Div_Exec.Visible = true;
                        ClientReqDS = ObjGB.GroupRequestDetails(RequestID, "CLIENTREQUEST", Session["UID"].ToString(), Session["User_Type"].ToString());

                        if (ClientReqDS.Tables[0].Rows.Count > 0)
                        {
                            if (ClientReqDS.Tables[1].Rows.Count > 0)
                            {
                                Accept = ClientReqDS.Tables[1].Rows[0]["AcceptBy"].ToString();
                                Reject = ClientReqDS.Tables[1].Rows[0]["RejectBy"].ToString();
                                string StrSts = ClientReqDS.Tables[1].Rows[0]["Status"].ToString();
                                if (StrSts.ToUpper() == "TICKETED")
                                {
                                    BookingDetails.Columns[9].Visible = false;
                                    BookingDetails.Columns[8].Visible = false;
                                }
                                if (StrSts.ToUpper() == "PAID" || StrSts.ToUpper() == "TICKETED")
                                {
                                    BookingDetails.Columns[4].Visible = false;
                                    BookingDetails.Columns[13].Visible = true;
                                }
                            }
                            DataTable DTOneWay = new DataTable();
                            DataTable Saparate_DTOneWay = new DataTable();
                            DataTable DTRoundTrip = new DataTable();
                            DataTable Saparate_DTRoundTrip = new DataTable();
                            Saparate_DTOneWay = ClientReqDS.Tables[0].DefaultView.ToTable(true, "SNO");
                            if (Saparate_DTOneWay.Rows.Count > 0)
                            {
                                for (int i = 0; i < Saparate_DTOneWay.Rows.Count; i++)
                                {
                                    string Oid = "";
                                    Oid = Saparate_DTOneWay.Rows[i]["SNO"].ToString();
                                    DataSet DSOPrrice = new DataSet();
                                    switch (i)
                                    {
                                        case 0:
                                            OFlightDetails1.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails1.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails1.Visible = false;
                                                OWayPrice1.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails1.Visible = true;
                                                PriceQuote1.Visible = true;
                                                GVPriceQuote1.Columns[6].Visible = false;
                                                GVPriceQuote1.Columns[8].Visible = false;
                                                GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote1.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails1.Visible = true;

                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    OWayPrice1.Visible = false;
                                                }
                                                else
                                                {
                                                    OWayPrice1.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote1.Columns[6].Visible = false;
                                                    GVPriceQuote1.Columns[8].Visible = false;
                                                    GVPriceQuote1.Columns[9].Visible = false;
                                                    GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote1.DataBind();
                                                    PriceQuote1.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            OWayPrice1.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["OFlightDetails1"] = Oid;
                                            break;
                                        case 1:
                                            OFlightDetails2.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails2.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails2.Visible = false;
                                                OWayPrice2.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails2.Visible = true;
                                                PriceQuote2.Visible = true;
                                                GVPriceQuote2.Columns[6].Visible = false;
                                                GVPriceQuote2.Columns[8].Visible = false;
                                                GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote2.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails2.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    OWayPrice2.Visible = false;
                                                }
                                                else
                                                {
                                                    OWayPrice2.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote2.Columns[6].Visible = false;
                                                    GVPriceQuote2.Columns[8].Visible = false;
                                                    GVPriceQuote2.Columns[9].Visible = false;
                                                    GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote2.DataBind();
                                                    PriceQuote2.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            OWayPrice2.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["OFlightDetails2"] = Oid;
                                            break;
                                        case 2:
                                            OFlightDetails3.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails3.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails3.Visible = false;
                                                OWayPrice3.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails3.Visible = true;
                                                PriceQuote3.Visible = true;
                                                GVPriceQuote3.Columns[6].Visible = false;
                                                GVPriceQuote3.Columns[8].Visible = false;
                                                GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote3.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails3.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    OWayPrice3.Visible = false;
                                                }
                                                else
                                                {
                                                    OWayPrice3.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote3.Columns[6].Visible = false;
                                                    GVPriceQuote3.Columns[8].Visible = false;
                                                    GVPriceQuote3.Columns[9].Visible = false;
                                                    GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote3.DataBind();
                                                    PriceQuote3.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            OWayPrice3.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["OFlightDetails3"] = Oid;
                                            break;
                                        case 3:
                                            OFlightDetails4.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails4.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails4.Visible = false;
                                                OWayPrice4.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails4.Visible = true;
                                                PriceQuote4.Visible = true;
                                                GVPriceQuote4.Columns[6].Visible = false;
                                                GVPriceQuote4.Columns[8].Visible = false;
                                                GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote4.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails4.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    OWayPrice4.Visible = false;
                                                }
                                                else
                                                {
                                                    OWayPrice4.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote4.Columns[6].Visible = false;
                                                    GVPriceQuote4.Columns[8].Visible = false;
                                                    GVPriceQuote4.Columns[9].Visible = false;
                                                    GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote4.DataBind();
                                                    PriceQuote4.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            OWayPrice4.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["OFlightDetails4"] = Oid;
                                            break;
                                        case 4:
                                            OFlightDetails5.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails5.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails5.Visible = false;
                                                OWayPrice5.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails5.Visible = true;
                                                PriceQuote5.Visible = true;
                                                GVPriceQuote5.Columns[6].Visible = false;
                                                GVPriceQuote5.Columns[8].Visible = false;
                                                GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote5.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails5.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    OWayPrice5.Visible = false;
                                                }
                                                else
                                                {
                                                    OWayPrice5.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote5.Columns[6].Visible = false;
                                                    GVPriceQuote5.Columns[8].Visible = false;
                                                    GVPriceQuote5.Columns[9].Visible = false;
                                                    GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote5.DataBind();
                                                    PriceQuote5.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            OWayPrice5.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["OFlightDetails5"] = Oid;
                                            break;
                                        case 5:
                                            RFlightDetails1.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails1.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails6.Visible = false;
                                                RoundTrip1.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails6.Visible = true;
                                                PriceQuote6.Visible = true;
                                                GVPriceQuote6.Columns[6].Visible = false;
                                                GVPriceQuote6.Columns[8].Visible = false;
                                                GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote6.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails6.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    RoundTrip1.Visible = false;
                                                }
                                                else
                                                {
                                                    RoundTrip1.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote6.Columns[6].Visible = false;
                                                    GVPriceQuote6.Columns[8].Visible = false;
                                                    GVPriceQuote6.Columns[9].Visible = false;
                                                    GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote6.DataBind();
                                                    PriceQuote6.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            RoundTrip1.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["RFlightDetails1"] = Oid;
                                            break;
                                        case 6:
                                            RFlightDetails2.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails2.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails7.Visible = false;
                                                RoundTrip2.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails7.Visible = true;
                                                PriceQuote7.Visible = true;
                                                GVPriceQuote7.Columns[6].Visible = false;
                                                GVPriceQuote7.Columns[8].Visible = false;
                                                GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote7.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails7.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    RoundTrip2.Visible = false;
                                                }
                                                else
                                                {
                                                    RoundTrip2.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote7.Columns[6].Visible = false;
                                                    GVPriceQuote7.Columns[8].Visible = false;
                                                    GVPriceQuote7.Columns[9].Visible = false;
                                                    GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote7.DataBind();
                                                    PriceQuote7.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            RoundTrip2.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["RFlightDetails2"] = Oid;
                                            break;
                                        case 7:
                                            RFlightDetails3.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails3.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails8.Visible = false;
                                                RoundTrip3.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails8.Visible = true;
                                                PriceQuote8.Visible = true;
                                                GVPriceQuote8.Columns[6].Visible = false;
                                                GVPriceQuote8.Columns[8].Visible = false;
                                                GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote8.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails8.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    RoundTrip3.Visible = false;
                                                }
                                                else
                                                {
                                                    RoundTrip3.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote8.Columns[6].Visible = false;
                                                    GVPriceQuote8.Columns[8].Visible = false;
                                                    GVPriceQuote8.Columns[9].Visible = false;
                                                    GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote8.DataBind();
                                                    PriceQuote8.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            RoundTrip3.Visible = false;
                                                        }
                                                    }
                                                }
                                            }
                                            Page.Session["RFlightDetails3"] = Oid;
                                            break;
                                        case 8:
                                            RFlightDetails5.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails5.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails10.Visible = false;
                                                RoundTrip5.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails10.Visible = true;
                                                PriceQuote10.Visible = true;
                                                GVPriceQuote10.Columns[6].Visible = false;
                                                GVPriceQuote10.Columns[8].Visible = false;
                                                GVPriceQuote10.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote10.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails10.Visible = true;
                                                if (Accept == "N" && Reject == "N")
                                                {
                                                    RoundTrip5.Visible = false;
                                                }
                                                else
                                                {
                                                    RoundTrip5.Visible = true;
                                                }
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote10.Columns[6].Visible = false;
                                                    GVPriceQuote10.Columns[8].Visible = false;
                                                    GVPriceQuote10.Columns[9].Visible = false;
                                                    GVPriceQuote10.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote10.DataBind();
                                                    PriceQuote10.Visible = true;
                                                    if (DSOPrrice.Tables[1].Rows.Count > 0)
                                                    {
                                                        CustomerPaymentStatus = DSOPrrice.Tables[1].Rows[0]["Status"].ToString();
                                                        if (CustomerPaymentStatus.ToUpper() == "FREEZED")
                                                        {
                                                            RoundTrip5.Visible = false;
                                                        }
                                                    }
                                                }
                                            }

                                            Page.Session["RFlightDetails5"] = Oid;
                                            break;
                                    }
                                }
                            }
                        }
                        if (ClientReqDS.Tables[1].Rows.Count > 0)
                        {
                            if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Cancelled" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Cancellation Requested" || ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Refunded" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Rejected")
                            {
                                OWayPrice1.Visible = false;
                                OWayPrice2.Visible = false;
                                OWayPrice3.Visible = false;
                                OWayPrice4.Visible = false;
                                OWayPrice5.Visible = false;
                                RoundTrip1.Visible = false;
                                RoundTrip2.Visible = false;
                                RoundTrip3.Visible = false;
                                RoundTrip4.Visible = false;
                                RoundTrip5.Visible = false;
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[8].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                                div_BookingDetails.Visible = true;
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "freezed" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid")
                            {
                                if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid")
                                {
                                    BookingDetails.Columns[7].Visible = true;
                                    BookingDetails.Columns[9].Visible = false;
                                }
                                else
                                {
                                    BookingDetails.Columns[7].Visible = false;
                                    BookingDetails.Columns[9].Visible = false;
                                }
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                if (ClientReqDS.Tables[2].Rows.Count > 0)
                                {
                                    div_paxinfo.Visible = true;
                                    if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                                    {
                                        pax_info.Columns[4].Visible = false;
                                        pax_info.Columns[5].Visible = false;
                                        pax_info.Columns[6].Visible = false;
                                        pax_info.Columns[7].Visible = false;
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                    else
                                    {
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                }
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString().ToLower() == "paid" && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                            {
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                BookingDetails.Columns[7].Visible = true;
                                BookingDetails.Columns[9].Visible = false;
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Requested" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed" || ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Quoted" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Accepted")
                            {
                                if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed")
                                {
                                    div_TktInfo.Visible = true;
                                    div_paxinfo.Visible = false;
                                    DataSet DSPax = new DataSet();
                                    DSPax = ObjGB.PAXINFO(ClientReqDS.Tables[1].Rows[0]["RequestID"].ToString());
                                    if (DSPax.Tables[0].Rows[0]["Trip"].ToString() == "D")
                                    {
                                        tktInfromation.Columns[6].Visible = false;
                                        tktInfromation.Columns[7].Visible = false;
                                        tktInfromation.Columns[8].Visible = false;
                                    }
                                    tktInfromation.DataSource = DSPax;
                                    tktInfromation.DataBind();
                                    lbl_gdspnr.Text = DSPax.Tables[0].Rows[0]["GDSPNR"].ToString();
                                    lbl_airlinePnr.Text = DSPax.Tables[0].Rows[0]["AIRLINEPNR"].ToString();
                                    Ingds = DSPax.Tables[0].Rows[0]["INGDSPNR"].ToString();
                                    INAirline = DSPax.Tables[0].Rows[0]["INAIRLINEPNR"].ToString();
                                    if (Ingds != "" && INAirline != "")
                                    {
                                        tr_Inbond_pnrgds.Visible = true;
                                        lbl_inairline.Text = INAirline;
                                        lbl_inbondgdspnr.Text = Ingds;
                                        tktInfromation.Columns[3].Visible = true;
                                    }
                                    else
                                    {
                                        tr_Inbond_pnrgds.Visible = false;
                                        tktInfromation.Columns[3].Visible = false;
                                    }
                                }
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                if (ClientReqDS.Tables[2].Rows.Count > 0 && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                                {
                                    div_paxinfo.Visible = true;
                                    if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                                    {
                                        pax_info.Columns[4].Visible = false;
                                        pax_info.Columns[5].Visible = false;
                                        pax_info.Columns[6].Visible = false;
                                        pax_info.Columns[7].Visible = false;
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                    else
                                    {
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ClientReqDS.Tables[0].Rows.Add(ClientReqDS.Tables[0].NewRow());
                            BookingDetails.DataSource = ClientReqDS;
                            BookingDetails.DataBind();
                            int columncount = BookingDetails.Rows[0].Cells.Count;
                            BookingDetails.Rows[0].Cells.Clear();
                            BookingDetails.Rows[0].Cells.Add(new TableCell());
                            BookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                            BookingDetails.Rows[0].Cells[0].Text = "No Records Found";
                        }
                        if (ClientReqDS.Tables[2].Rows.Count > 0 && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                        {
                            div_paxinfo.Visible = true;
                            if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                            {
                                pax_info.Columns[4].Visible = false;
                                pax_info.Columns[5].Visible = false;
                                pax_info.Columns[6].Visible = false;
                                pax_info.Columns[7].Visible = false;
                                pax_info.DataSource = ClientReqDS.Tables[2];
                                pax_info.DataBind();
                            }
                            else
                            {
                                pax_info.DataSource = ClientReqDS.Tables[2];
                                pax_info.DataBind();
                            }
                        }
                    }
                    if (Session["User_Type"].ToString().ToUpper() == "ADMIN")
                    {

                        Div_Exec.Visible = true;
                        BookingDetails.Columns[8].Visible = false;

                        ClientReqDS = ObjGB.GroupRequestDetails(RequestID, "CLIENTREQUEST", Session["UID"].ToString(), Session["User_Type"].ToString());
                        if (ClientReqDS.Tables[0].Rows.Count > 0)
                        {
                            DataTable DTOneWay = new DataTable();
                            DataTable Saparate_DTOneWay = new DataTable();
                            DataTable DTRoundTrip = new DataTable();
                            DataTable Saparate_DTRoundTrip = new DataTable();
                            Saparate_DTOneWay = ClientReqDS.Tables[0].DefaultView.ToTable(true, "SNO");
                            if (Saparate_DTOneWay.Rows.Count > 0)
                            {
                                for (int i = 0; i < Saparate_DTOneWay.Rows.Count; i++)
                                {
                                    string Oid = "";
                                    Oid = Saparate_DTOneWay.Rows[i]["SNO"].ToString();
                                    DataSet DSOPrrice = new DataSet();
                                    switch (i)
                                    {
                                        case 0:
                                            OFlightDetails1.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails1.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails1.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails1.Visible = true;
                                                PriceQuote1.Visible = true;
                                                GVPriceQuote1.Columns[6].Visible = false;
                                                GVPriceQuote1.Columns[8].Visible = false;
                                                GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote1.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails1.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote1.Columns[6].Visible = false;
                                                    GVPriceQuote1.Columns[8].Visible = false;
                                                    GVPriceQuote1.Columns[9].Visible = false;
                                                    GVPriceQuote1.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote1.DataBind();
                                                }
                                            }
                                            Page.Session["OFlightDetails1"] = Oid;
                                            break;
                                        case 1:
                                            OFlightDetails2.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails2.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails2.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails2.Visible = true;
                                                PriceQuote2.Visible = true;
                                                GVPriceQuote2.Columns[6].Visible = false;
                                                GVPriceQuote2.Columns[8].Visible = false;
                                                GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote2.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails2.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote2.Columns[6].Visible = false;
                                                    GVPriceQuote2.Columns[8].Visible = false;
                                                    GVPriceQuote2.Columns[9].Visible = false;
                                                    GVPriceQuote2.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote2.DataBind();
                                                }
                                            }
                                            Page.Session["OFlightDetails2"] = Oid;
                                            break;
                                        case 2:
                                            OFlightDetails3.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails3.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails3.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails3.Visible = true;
                                                PriceQuote3.Visible = true;
                                                GVPriceQuote3.Columns[6].Visible = false;
                                                GVPriceQuote3.Columns[8].Visible = false;
                                                GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote3.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails3.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote3.Columns[6].Visible = false;
                                                    GVPriceQuote3.Columns[8].Visible = false;
                                                    GVPriceQuote3.Columns[9].Visible = false;
                                                    GVPriceQuote3.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote3.DataBind();
                                                }
                                            }
                                            Page.Session["OFlightDetails3"] = Oid;
                                            break;
                                        case 3:
                                            OFlightDetails4.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails4.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails4.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails4.Visible = true;
                                                PriceQuote4.Visible = true;
                                                GVPriceQuote4.Columns[6].Visible = false;
                                                GVPriceQuote4.Columns[8].Visible = false;
                                                GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote4.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails4.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote4.Columns[6].Visible = false;
                                                    GVPriceQuote4.Columns[8].Visible = false;
                                                    GVPriceQuote4.Columns[9].Visible = false;
                                                    GVPriceQuote4.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote4.DataBind();
                                                }
                                            }
                                            Page.Session["OFlightDetails4"] = Oid;
                                            break;
                                        case 4:
                                            OFlightDetails5.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            OFlightDetails5.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails5.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails5.Visible = true;
                                                PriceQuote5.Visible = true;
                                                GVPriceQuote5.Columns[6].Visible = false;
                                                GVPriceQuote5.Columns[8].Visible = false;
                                                GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote5.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails5.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote5.Columns[6].Visible = false;
                                                    GVPriceQuote5.Columns[8].Visible = false;
                                                    GVPriceQuote5.Columns[9].Visible = false;
                                                    GVPriceQuote5.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote5.DataBind();
                                                }
                                            }
                                            Page.Session["OFlightDetails5"] = Oid;
                                            break;
                                        case 5:
                                            RFlightDetails1.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails1.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails6.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails6.Visible = true;
                                                PriceQuote6.Visible = true;
                                                GVPriceQuote6.Columns[6].Visible = false;
                                                GVPriceQuote6.Columns[8].Visible = false;
                                                GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote6.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails6.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote6.Columns[6].Visible = false;
                                                    GVPriceQuote6.Columns[8].Visible = false;
                                                    GVPriceQuote6.Columns[9].Visible = false;
                                                    GVPriceQuote6.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote6.DataBind();
                                                }
                                            }
                                            Page.Session["RFlightDetails1"] = Oid;
                                            break;
                                        case 6:
                                            RFlightDetails2.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails2.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");

                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails7.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails7.Visible = true;
                                                GVPriceQuote7.Columns[6].Visible = false;
                                                GVPriceQuote7.Columns[8].Visible = false;
                                                GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote7.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails7.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote7.Columns[6].Visible = false;
                                                    GVPriceQuote7.Columns[8].Visible = false;
                                                    GVPriceQuote7.Columns[9].Visible = false;
                                                    GVPriceQuote7.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote7.DataBind();
                                                }
                                            }
                                            Page.Session["RFlightDetails2"] = Oid;
                                            break;
                                        case 7:
                                            RFlightDetails3.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails3.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails8.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails8.Visible = true;
                                                GVPriceQuote8.Columns[6].Visible = false;
                                                GVPriceQuote8.Columns[8].Visible = false;
                                                GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote8.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails8.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote8.Columns[6].Visible = false;
                                                    GVPriceQuote8.Columns[8].Visible = false;
                                                    GVPriceQuote8.Columns[9].Visible = false;
                                                    GVPriceQuote8.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote8.DataBind();
                                                }
                                            }
                                            Page.Session["RFlightDetails3"] = Oid;
                                            break;
                                        case 8:
                                            RFlightDetails5.DataSource = ClientReqDS.Tables[0].Select(String.Format("SNO = '{0}'", Oid)).CopyToDataTable();
                                            RFlightDetails5.DataBind();
                                            DSOPrrice = ObjGB.GroupRequestDetails(Oid, "EXECPRICEQUOTE", RequestID, "");
                                            if ((ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED") && DSOPrrice.Tables[0].Rows.Count <= 0)
                                            {
                                                div_flightdetails10.Visible = false;
                                            }
                                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "PAID" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToUpper() == "TICKETED" && DSOPrrice.Tables[0].Rows.Count > 0)
                                            {
                                                div_flightdetails10.Visible = true;
                                                GVPriceQuote10.Columns[6].Visible = false;
                                                GVPriceQuote10.Columns[8].Visible = false;
                                                GVPriceQuote10.DataSource = DSOPrrice.Tables[0];
                                                GVPriceQuote10.DataBind();
                                            }
                                            else
                                            {
                                                div_flightdetails10.Visible = true;
                                                if (DSOPrrice.Tables[0].Rows.Count > 0)
                                                {
                                                    GVPriceQuote10.Columns[6].Visible = false;
                                                    GVPriceQuote10.Columns[8].Visible = false;
                                                    GVPriceQuote10.Columns[9].Visible = false;
                                                    GVPriceQuote10.DataSource = DSOPrrice.Tables[0];
                                                    GVPriceQuote10.DataBind();
                                                }
                                            }
                                            Page.Session["RFlightDetails5"] = Oid;
                                            break;
                                    }
                                }
                            }
                        }
                        if (ClientReqDS.Tables[1].Rows.Count > 0)
                        {
                            if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Cancelled" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Cancellation Requested" || ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Refunded" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Rejected")
                            {
                                OWayPrice1.Visible = false;
                                OWayPrice2.Visible = false;
                                OWayPrice3.Visible = false;
                                OWayPrice4.Visible = false;
                                OWayPrice5.Visible = false;
                                RoundTrip1.Visible = false;
                                RoundTrip2.Visible = false;
                                RoundTrip3.Visible = false;
                                RoundTrip4.Visible = false;
                                RoundTrip5.Visible = false;
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[8].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                                div_BookingDetails.Visible = true;
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "freezed" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid")
                            {
                                if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "paid")
                                {
                                    BookingDetails.Columns[7].Visible = false;
                                    BookingDetails.Columns[9].Visible = false;
                                }
                                else
                                {
                                    BookingDetails.Columns[7].Visible = false;
                                    BookingDetails.Columns[9].Visible = false;
                                }
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                if (ClientReqDS.Tables[2].Rows.Count > 0)
                                {
                                    div_paxinfo.Visible = true;
                                    if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                                    {
                                        pax_info.Columns[4].Visible = false;
                                        pax_info.Columns[5].Visible = false;
                                        pax_info.Columns[6].Visible = false;
                                        pax_info.Columns[7].Visible = false;
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                    else
                                    {
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                }
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString().ToLower() == "paid" && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                            {
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString().ToLower() == "paid" && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed")
                            {
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                BookingDetails.Columns[4].Visible = false;
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                                BookingDetails.Columns[13].Visible = true;
                            }
                            else if (ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Requested" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed" || ClientReqDS.Tables[1].Rows[0]["paymentstatus"].ToString() == "Quoted" || ClientReqDS.Tables[1].Rows[0]["Status"].ToString() == "Accepted")
                            {
                                if (ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "ticketed")
                                {
                                    div_TktInfo.Visible = true;
                                    div_paxinfo.Visible = false;
                                    DataSet DSPax = new DataSet();
                                    DSPax = ObjGB.PAXINFO(ClientReqDS.Tables[1].Rows[0]["RequestID"].ToString());
                                    if (DSPax.Tables[0].Rows[0]["Trip"].ToString() == "D")
                                    {
                                        tktInfromation.Columns[6].Visible = false;
                                        tktInfromation.Columns[7].Visible = false;
                                        tktInfromation.Columns[8].Visible = false;
                                    }
                                    tktInfromation.DataSource = DSPax;
                                    tktInfromation.DataBind();
                                    lbl_gdspnr.Text = DSPax.Tables[0].Rows[0]["GDSPNR"].ToString();
                                    lbl_airlinePnr.Text = DSPax.Tables[0].Rows[0]["AIRLINEPNR"].ToString();
                                    Ingds = DSPax.Tables[0].Rows[0]["INGDSPNR"].ToString();
                                    INAirline = DSPax.Tables[0].Rows[0]["INAIRLINEPNR"].ToString();
                                    if (Ingds != "" && INAirline != "")
                                    {
                                        tr_Inbond_pnrgds.Visible = true;
                                        lbl_inairline.Text = INAirline;
                                        lbl_inbondgdspnr.Text = Ingds;
                                        tktInfromation.Columns[3].Visible = true;
                                    }
                                    else
                                    {
                                        tr_Inbond_pnrgds.Visible = false;
                                        tktInfromation.Columns[3].Visible = false;
                                    }
                                }
                                BookingDetails.Columns[7].Visible = false;
                                BookingDetails.Columns[9].Visible = false;
                                BookingDetails.DataSource = ClientReqDS.Tables[1];
                                BookingDetails.DataBind();
                                if (ClientReqDS.Tables[2].Rows.Count > 0 && ClientReqDS.Tables[1].Rows[0]["Status"].ToString().ToLower() != "ticketed")
                                {
                                    div_paxinfo.Visible = true;
                                    if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                                    {
                                        pax_info.Columns[4].Visible = false;
                                        pax_info.Columns[5].Visible = false;
                                        pax_info.Columns[6].Visible = false;
                                        pax_info.Columns[7].Visible = false;
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                    else
                                    {
                                        pax_info.DataSource = ClientReqDS.Tables[2];
                                        pax_info.DataBind();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ClientReqDS.Tables[0].Rows.Add(ClientReqDS.Tables[0].NewRow());
                            BookingDetails.DataSource = ClientReqDS;
                            BookingDetails.DataBind();
                            int columncount = BookingDetails.Rows[0].Cells.Count;
                            BookingDetails.Rows[0].Cells.Clear();
                            BookingDetails.Rows[0].Cells.Add(new TableCell());
                            BookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                            BookingDetails.Rows[0].Cells[0].Text = "No Records Found";
                        }
                        if (ClientReqDS.Tables[2].Rows.Count > 0)
                        {
                            div_paxinfo.Visible = true;
                            if (ClientReqDS.Tables[0].Rows[0]["Trip"].ToString().ToLower() == "domestic")
                            {
                                pax_info.Columns[4].Visible = false;
                                pax_info.Columns[5].Visible = false;
                                pax_info.Columns[6].Visible = false;
                                pax_info.Columns[7].Visible = false;
                                pax_info.DataSource = ClientReqDS.Tables[2];
                                pax_info.DataBind();
                            }
                            else
                            {
                                pax_info.DataSource = ClientReqDS.Tables[2];
                                pax_info.DataBind();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogTrace.WriteErrorLog(ex, "PageLoadGrpRequestIDDetails");
                }
            }
        }
    }
    protected void OWayPrice1_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (OWayPrice1_txt_Quoteprice.Text.Trim() == "")
            {
                OWayPrice1_txt_Quoteprice.Text = "0";
            }
            if (txt_offervalid1.Value == "")
            {
                txt_offervalid1.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["OFlightDetails1"].ToString(), Convert.ToDecimal(OWayPrice1_txt_Quoteprice.Text.Trim()), OWayPrice1_txt_remarks.Text.Trim(), OWayPrice1_ddl_Partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid1.Value.Trim(), Session["UID"].ToString());

            ResultAgentMail = AgentMailSending(OWayPrice1_txt_Quoteprice.Text.Trim(), Session["OFlightDetails1"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["OFlightDetails1"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote1.Columns[6].Visible = false;
                GVPriceQuote1.Columns[8].Visible = false;
                GVPriceQuote1.Columns[9].Visible = false;
                GVPriceQuote1.DataSource = ds1.Tables[0];
                GVPriceQuote1.DataBind();
                PriceQuote1.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid1.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OWayPrice1_btn_submit_Click");
        }
    }
    protected void OWayPrice2_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (OWayPrice2_txt_quoteprice.Text.Trim() == "")
            {
                OWayPrice2_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid2.Value == "")
            {
                txt_offervalid2.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["OFlightDetails2"].ToString(), Convert.ToDecimal(OWayPrice2_txt_quoteprice.Text.Trim()), OWayPrice2_txt_remarks.Text.Trim(), OWayPrice2_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid2.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(OWayPrice2_txt_quoteprice.Text.Trim(), Session["OFlightDetails2"].ToString(), Request.QueryString["RequestID"]);
            ObjGB.GroupRequestDetails(Session["OFlightDetails2"].ToString(), "EXECPRICEQUOTE", RequestID, "");
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["OFlightDetails2"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote2.Columns[6].Visible = false;
                GVPriceQuote2.Columns[8].Visible = false;
                GVPriceQuote2.Columns[9].Visible = false;
                GVPriceQuote2.DataSource = ds1.Tables[0];
                GVPriceQuote2.DataBind();
                PriceQuote2.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid2.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OWayPrice2_btn_submit_Click");
        }
    }
    protected void OWayPrice3_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (OWayPrice3_txt_quoteprice.Text.Trim() == "")
            {
                OWayPrice3_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid3.Value == "")
            {
                txt_offervalid3.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["OFlightDetails3"].ToString(), Convert.ToDecimal(OWayPrice3_txt_quoteprice.Text.Trim()), OWayPrice3_txt_remarks.Text.Trim(), OWayPrice3_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid3.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(OWayPrice3_txt_quoteprice.Text.Trim(), Session["OFlightDetails3"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["OFlightDetails3"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote3.Columns[6].Visible = false;
                GVPriceQuote3.Columns[8].Visible = false;
                GVPriceQuote3.Columns[9].Visible = false;
                GVPriceQuote3.DataSource = ds1.Tables[0];
                GVPriceQuote3.DataBind();
                PriceQuote3.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid3.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OWayPrice3_btn_submit_Click");
        }
    }
    protected void OWayPrice4_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (OWayPrice4_txt_quoteprice.Text.Trim() == "")
            {
                OWayPrice4_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid4.Value == "")
            {
                txt_offervalid4.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["OFlightDetails4"].ToString(), Convert.ToDecimal(OWayPrice4_txt_quoteprice.Text.Trim()), OWayPrice4_txt_remarks.Text.Trim(), OWayPrice4_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid4.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(OWayPrice4_txt_quoteprice.Text.Trim(), Session["OFlightDetails4"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["OFlightDetails4"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote4.Columns[6].Visible = false;
                GVPriceQuote4.Columns[8].Visible = false;
                GVPriceQuote4.Columns[9].Visible = false;
                GVPriceQuote4.DataSource = ds1.Tables[0];
                GVPriceQuote4.DataBind();
                PriceQuote4.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid4.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OWayPrice4_btn_submit_Click");
        }
    }
    protected void OWayPrice5_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (OWayPrice5_txt_quoteprice.Text.Trim() == "")
            {
                OWayPrice5_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid5.Value == "")
            {
                txt_offervalid5.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["OFlightDetails5"].ToString(), Convert.ToDecimal(OWayPrice5_txt_quoteprice.Text.Trim()), OWayPrice5_txt_remarks.Text.Trim(), OWayPrice5_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid5.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(OWayPrice5_txt_quoteprice.Text.Trim(), Session["OFlightDetails5"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["OFlightDetails5"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote5.Columns[6].Visible = false;
                GVPriceQuote5.Columns[8].Visible = false;
                GVPriceQuote5.Columns[9].Visible = false;
                GVPriceQuote5.DataSource = ds1.Tables[0];
                GVPriceQuote5.DataBind();
                PriceQuote5.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid5.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OWayPrice5_btn_submit_Click");
        }
    }
    protected void RoundTrip1_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RoundTrip1_txt_quoteprice.Text.Trim() == "")
            {
                RoundTrip1_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid6.Value == "")
            {
                txt_offervalid6.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["RFlightDetails1"].ToString(), Convert.ToDecimal(RoundTrip1_txt_quoteprice.Text.Trim()), RoundTrip1_txt_remarks.Text.Trim(), RoundTrip1_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid6.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(RoundTrip1_txt_quoteprice.Text.Trim(), Session["RFlightDetails1"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["RFlightDetails1"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote6.Columns[6].Visible = false;
                GVPriceQuote6.Columns[8].Visible = false;
                GVPriceQuote6.Columns[9].Visible = false;
                GVPriceQuote6.DataSource = ds1.Tables[0];
                GVPriceQuote6.DataBind();
                PriceQuote6.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid6.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "RoundTrip1_btn_submit_Click");
        }
    }
    protected void RoundTrip2_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RoundTrip2_txt_quoteprice.Text.Trim() == "")
            {
                RoundTrip2_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid7.Value == "")
            {
                txt_offervalid7.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["RFlightDetails2"].ToString(), Convert.ToDecimal(RoundTrip2_txt_quoteprice.Text.Trim()), RoundTrip2_txt_remarks.Text.Trim(), RoundTrip2_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid7.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(RoundTrip2_txt_quoteprice.Text.Trim(), Session["RFlightDetails2"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["RFlightDetails2"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote7.Columns[6].Visible = false;
                GVPriceQuote7.Columns[8].Visible = false;
                GVPriceQuote7.Columns[9].Visible = false;
                GVPriceQuote7.DataSource = ds1.Tables[0];
                GVPriceQuote7.DataBind();
                PriceQuote7.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid7.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "RoundTrip2_btn_submit_Click");
        }
    }
    protected void RoundTrip3_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RoundTrip3_txt_quoteprice.Text.Trim() == "")
            {
                RoundTrip3_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid8.Value == "")
            {
                txt_offervalid8.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["RFlightDetails3"].ToString(), Convert.ToDecimal(RoundTrip3_txt_quoteprice.Text.Trim()), RoundTrip3_txt_remarks.Text.Trim(), RoundTrip3_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid8.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(RoundTrip3_txt_quoteprice.Text.Trim(), Session["RFlightDetails3"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["RFlightDetails3"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote8.Columns[6].Visible = false;
                GVPriceQuote8.Columns[8].Visible = false;
                GVPriceQuote8.Columns[9].Visible = false;
                GVPriceQuote8.DataSource = ds1.Tables[0];
                GVPriceQuote8.DataBind();
                PriceQuote8.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid8.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "RoundTrip3_btn_submit_Click");
        }
    }
    protected void RoundTrip4_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RoundTrip4_txt_quoteprice.Text.Trim() == "")
            {
                RoundTrip4_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid9.Value == "")
            {
                txt_offervalid9.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["RFlightDetails4"].ToString(), Convert.ToDecimal(RoundTrip4_txt_quoteprice.Text.Trim()), RoundTrip4_txt_remarks.Text.Trim(), RoundTrip4_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid9.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(RoundTrip4_txt_quoteprice.Text.Trim(), Session["RFlightDetails4"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["RFlightDetails4"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote9.Columns[6].Visible = false;
                GVPriceQuote9.Columns[8].Visible = false;
                GVPriceQuote9.Columns[9].Visible = false;
                GVPriceQuote9.DataSource = ds1.Tables[0];
                GVPriceQuote9.DataBind();
                PriceQuote9.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid9.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "RoundTrip4_btn_submit_Click");
        }
    }
    protected void RoundTrip5_btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RoundTrip5_txt_quoteprice.Text.Trim() == "")
            {
                RoundTrip5_txt_quoteprice.Text = "0";
            }
            if (txt_offervalid10.Value == "")
            {
                txt_offervalid10.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
            ObjGB.PARTNERPRICES(Session["RFlightDetails5"].ToString(), Convert.ToDecimal(RoundTrip5_txt_quoteprice.Text.Trim()), RoundTrip5_txt_remarks.Text.Trim(), RoundTrip5_ddl_partner.SelectedValue.ToString(), "INSERTED", Request.QueryString["RequestID"].ToString(), txt_offervalid10.Value.Trim(), Session["UID"].ToString());
            ResultAgentMail = AgentMailSending(RoundTrip5_txt_quoteprice.Text.Trim(), Session["RFlightDetails5"].ToString(), Request.QueryString["RequestID"]);
            DataSet ds1 = new DataSet();
            ds1 = ObjGB.GroupRequestDetails(Session["RFlightDetails5"].ToString(), "EXECPRICEQUOTE", Request.QueryString["RequestID"], "");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                GVPriceQuote10.Columns[6].Visible = false;
                GVPriceQuote10.Columns[8].Visible = false;
                GVPriceQuote10.Columns[9].Visible = false;
                GVPriceQuote10.DataSource = ds1.Tables[0];
                GVPriceQuote10.DataBind();
                PriceQuote10.Visible = true;
            }
            if (ResultAgentMail == 1)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(9);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
            }
            ClearInputs(Page.Controls);
            txt_offervalid10.Value = "";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "RoundTrip5_btn_submit_Click");
        }
    }
    protected void GVPriceQuote1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote1.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "O");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote1_RowCommand");
        }
    }
    protected void GVPriceQuote2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote2.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "O");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }

            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote2_RowCommand");
        }
    }
    protected void GVPriceQuote3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote3.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "O");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote3_RowCommand");
        }
    }
    protected void GVPriceQuote4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote4.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "O");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote4_RowCommand");
        }
    }
    protected void GVPriceQuote5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote5.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "O");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote5_RowCommand");
        }

    }
    protected void GVPriceQuote6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote6.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "R");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote6_RowCommand");
        }
    }
    protected void GVPriceQuote7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote7.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "R");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }

            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote7_RowCommand");
        }
    }
    protected void GVPriceQuote8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote8.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "R");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote8_RowCommand");
        }
    }
    protected void GVPriceQuote9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote9.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "R");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote9_RowCommand");
        }
    }
    protected void GVPriceQuote10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label bkg = (Label)GVPriceQuote10.Rows[RowIndex].FindControl("lblBKG_PartnerName");
                RefRequestID = Request.QueryString["RequestID"];
                SNO = Convert.ToInt32(e.CommandArgument.ToString());
                validoffer = ObjGB.PAYMENTSTATUS(RefRequestID, "OFFERDATE", SNO, bkg.Text.Trim().ToString());
                TodayDate = CurrentTime();
                NoOfDays = ObjGB.DateDiff(TodayDate, validoffer);
                if (NoOfDays >= 0)
                {
                    PaymentStsRst = ObjGB.PAYMENTSTATUS(RefRequestID, "STATUS", 0, bkg.Text.Trim().ToString());
                    if (PaymentStsRst == "Quoted")
                    {
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblPName = (Label)row.FindControl("lblBKG_PName");
                        Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                        ObjGB.FREEZEPPRICE(e.CommandArgument.ToString(), Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATED", RefRequestID, "R");
                        ResultAgentMail = AgentMailSending(lblQuotePrice.Text.Trim(), e.CommandArgument.ToString(), RefRequestID);
                        ResultAdminMail = AdminNExecMailSending("Price Quote has been freezed!!", e.CommandArgument.ToString(), RefRequestID);
                        ObjGB.GroupRequestDetails(Request.QueryString["RequestID"], "PaymentLink", Session["PaymentLink"].ToString(), "");
                        if (ResultAgentMail == 1 && ResultAdminMail == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(2);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                        }
                    }
                    else if (PaymentStsRst == "freezed" || PaymentStsRst == "Paid")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(7);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(8);", true);
                }
            }
            if (e.CommandName == "Reject")
            {
                bool Result = false;
                RequestID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPName = (Label)row.FindControl("lblBKG_PName");
                Label lblQuotePrice = (Label)row.FindControl("lblQuotePrice");
                Result = AutoRefund(RequestID, "Reject", Convert.ToDouble(lblQuotePrice.Text.Trim().ToString()));
                if (Result == true)
                {
                    ObjGB.CancelByAdmin(RequestID, Convert.ToDecimal(lblQuotePrice.Text.Trim().ToString()), lblPName.Text.Trim().ToString(), Session["UID"].ToString(), "UPDATEDBYADMIN");
                    AdminNExecMailSending("Your Price Quote Request has been Cancelled!!", "", "");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Payment has been refunded againest requestid" + RequestID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Somthing went wrong, please try aftersome time');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GVPriceQuote10_RowCommand");
        }
    }
    protected void lb_reqcancl_Click(object sender, EventArgs e)
    {
        try
        {
            CancelRemarks.Visible = true;
            lb_reqcancl.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "lb_reqcancl_Click");
        }
    }
    protected void lb_submit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow OW in OneWayTrip.Rows)
            {
                from = (TextBox)OW.FindControl("txt_From");
                To = (TextBox)OW.FindControl("txt_To");
                DepDate = (TextBox)OW.FindControl("txt_DepDate");
                DepTime = (TextBox)OW.FindControl("txt_DepTime");
                ArvlDate = (TextBox)OW.FindControl("txt_ArvlDate");
                ArvlTime = (TextBox)OW.FindControl("txt_ArvlTime");
                Airline = (TextBox)OW.FindControl("txt_Airline");
                FlightNo = (TextBox)OW.FindControl("txt_FlightNo");
                GbdCounter = (Label)OW.FindControl("lbl_gbdcounter");
                GfdCounter = (Label)OW.FindControl("lbl_fgdcounter");
                ObjGB.INSERTFINALFLIGHTDETAILS(from.Text.Trim(), To.Text.Trim(), DepDate.Text.Trim(), DepTime.Text.Trim(), ArvlDate.Text.Trim(), ArvlTime.Text.Trim(), Airline.Text.Trim(), FlightNo.Text.Trim(), Convert.ToInt32(GfdCounter.Text.Trim()), Session["UID"].ToString());
            }
            foreach (GridViewRow RT in RoundTrip.Rows)
            {
                from = (TextBox)RT.FindControl("txt_From");
                To = (TextBox)RT.FindControl("txt_To");
                DepDate = (TextBox)RT.FindControl("txt_DepDate");
                DepTime = (TextBox)RT.FindControl("txt_DepTime");
                ArvlDate = (TextBox)RT.FindControl("txt_ArvlDate");
                ArvlTime = (TextBox)RT.FindControl("txt_ArvlTime");
                Airline = (TextBox)RT.FindControl("txt_Airline");
                FlightNo = (TextBox)RT.FindControl("txt_FlightNo");
                GbdCounter = (Label)RT.FindControl("lbl_gbdcounter");
                GfdCounter = (Label)RT.FindControl("lbl_fgdcounter");
                ObjGB.INSERTFINALFLIGHTDETAILS(from.Text.Trim(), To.Text.Trim(), DepDate.Text.Trim(), DepTime.Text.Trim(), ArvlDate.Text.Trim(), ArvlTime.Text.Trim(), Airline.Text.Trim(), FlightNo.Text.Trim(), Convert.ToInt32(GfdCounter.Text.Trim()), Session["UID"].ToString());
            }
            foreach (GridViewRow PD in paxdetails.Rows)
            {
                TextBox Title = (TextBox)PD.FindControl("txt_title");
                TextBox FName = (TextBox)PD.FindControl("txt_fname");
                TextBox MName = (TextBox)PD.FindControl("txt_mname");
                TextBox LName = (TextBox)PD.FindControl("txt_lNAME");
                TextBox PaxType = (TextBox)PD.FindControl("txt_PaxType");
                TextBox DOB = (TextBox)PD.FindControl("txt_DOB");
                DropDownList Gender = (DropDownList)PD.FindControl("dll_gender");
                TextBox PassportNo = (TextBox)PD.FindControl("txt_passport");
                TextBox PassportExpireDate = (TextBox)PD.FindControl("txt_passportex");
                TextBox NationalityCode = (TextBox)PD.FindControl("txt_NationalityCode");
                TextBox IssueCountryCode = (TextBox)PD.FindControl("txt_IssueCountryCode");
                Label lbl_paxcounter = (Label)PD.FindControl("lbl_paxcounter");
                ObjGB.UPDATEPAXINFOR(Title.Text.Trim(), FName.Text.Trim(), MName.Text.Trim(), LName.Text.Trim(), PaxType.Text.Trim(), DOB.Text.Trim(), Gender.SelectedValue, PassportExpireDate.Text.Trim(), PassportNo.Text.Trim(), IssueCountryCode.Text.Trim(), NationalityCode.Text.Trim(), Convert.ToInt32(lbl_paxcounter.Text.Trim()), "UPDATED");
            }
            ProbDays = Request["sltProbableDays"];
            AdultCount = Request["txtNoOfAdult"];
            ChildCount = Request["txtNoOfChild"];
            if (ChildCount == "")
            {
                ChildCount = "0";
            }
            InfantCount = Request["txtNoOfInfant"];
            if (InfantCount == "")
            {
                InfantCount = "0";
            }
            Remarks = Request["textarea1"];
            ExpactedFare = Request["txtExpectedFair"];
            ObjGB.INSERTFINALBOOKINGDETAILS(Convert.ToInt32(AdultCount), Convert.ToInt32(ChildCount), Convert.ToInt32(InfantCount), Remarks, ProbDays, Convert.ToDecimal(ExpactedFare), Convert.ToInt32(GbdCounter.Text.Trim()), Session["UID"].ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Data updated successfully!!');", true);
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "lb_submit_Click");
        }
    }
    protected void link_UpdateTktPnr_Click(object sender, EventArgs e)
    {
        try
        {
            dynamic ReqID = ((LinkButton)sender).CommandArgument.ToString();
            if (ReqID != "")
            {
                Response.Redirect("UpdateTicket_PNR.aspx?RequestID=" + ReqID);
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "link_UpdateTktPnr_Click");
        }
    }
    protected int AgentMailSending(string VendorQuote, string RequestID, string RefRequestID)
    {
        int i = 0;
        DataSet MailDt = new DataSet();
        DataSet MailContent = new DataSet();
        DataSet DSStatus = new DataSet();
        string StrMail = "";
        string strMailMsg = "";

        try
        {
            MailContent = ObjGB.GroupRequestDetails(RequestID, "MAILCONTENT", "", RefRequestID);
            DSStatus = ObjGB.SENDPAYMENTLINK(RefRequestID, Session["UID"].ToString());
            DataSet mailmsg = new DataSet();
            string STRSUB, MAILHEADER, MAILMESSAGE, _cmdtype;
            _cmdtype = Convert.ToString(DSStatus.Tables[0].Rows[0]["Status"]).ToUpper();
            mailmsg = ObjGB.GRP_MAILMSGSUBJECT(_cmdtype, "AGENT");
            STRSUB = Convert.ToString(mailmsg.Tables[0].Rows[0]["STRSUB"]);
            MAILHEADER = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILHEADER"]);
            MAILMESSAGE = Convert.ToString(mailmsg.Tables[0].Rows[0]["MAILMESSAGE"]);

            if (DSStatus.Tables[0].Rows.Count > 0)
            {
                if (DSStatus.Tables[0].Rows[0]["Status"].ToString().ToLower() == "freezed")
                {
                    StrMail = ObjGB.GETEMAILID(RefRequestID, "AGENT");
                    strMailMsg = "<table>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'><h2> " + MAILHEADER + " </h2>";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg += "<tr>";
                    strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
                    strMailMsg += "</td>";
                    strMailMsg += "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td> ";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Group Booking ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["GrpID"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Freezed Request ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Booking Name: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Travel Date: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Departure_Date"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>AirLine : </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["aircode"]) + "(" + Convert.ToString(MailContent.Tables[0].Rows[0]["flightnumber"]) + ")";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Journey Plan: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Sector"]);
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
                    strMailMsg = strMailMsg + "<td><b>Numbers of Adult: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["AdultCount"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Child: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ChildCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Infant: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["InfantCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Your Expacted Fare: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ExpactedPrice"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Booking Fare: </b>" + Convert.ToString(DSStatus.Tables[0].Rows[0]["BookedPrice"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    Url = HttpContext.Current.Request.Url.Authority;
                    PaymentLink = "http://" + Url + "/ITZNEW/GroupBookingLogin.aspx?userid=" + MailContent.Tables[0].Rows[0]["userid"].ToString() + "&fare=" + DSStatus.Tables[0].Rows[0]["BookedPrice"].ToString() + "&RequestID=" + RefRequestID + "&Trip=" + MailContent.Tables[0].Rows[0]["TripType"].ToString() + "&PD=" + DSStatus.Tables[0].Rows[0]["IsPaxDeatils"].ToString().ToLower() + "&Status=" + DSStatus.Tables[0].Rows[0]["Status"].ToString().ToLower() + "&TT=" + DSStatus.Tables[0].Rows[0]["Trip"].ToString() + "&Payment=off";
                    Session["PaymentLink"] = PaymentLink.Trim().ToString();
                    strMailMsg = strMailMsg + "<td><b><a href='" + PaymentLink.Trim().ToString() + "'>For book your request, please follow the link </b>";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "</table>";
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
                else if (DSStatus.Tables[0].Rows[0]["Status"].ToString().ToLower() == "cancellation requested" || DSStatus.Tables[0].Rows[0]["Status"].ToString().ToLower() == "cancelled")
                {

                    StrMail = ObjGB.GETEMAILID(RefRequestID, "AGENT");
                    strMailMsg = "<table>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'><h2> " + MAILHEADER + " </h2>";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg += "<tr>";
                    strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
                    strMailMsg += "</td>";
                    strMailMsg += "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td> ";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Group Booking ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["GrpID"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Booking Name: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Travel Date: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Departure_Date"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>AirLine : </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["aircode"]) + "(" + Convert.ToString(MailContent.Tables[0].Rows[0]["flightnumber"]) + ")";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Journey Plan: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Sector"]);
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
                    strMailMsg = strMailMsg + "<td><b>Numbers of Adult: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["AdultCount"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Child: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ChildCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Infant: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["InfantCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Cancellation Remarks: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["CancellationRemarks"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Booking Fare: </b>" + Convert.ToString(DSStatus.Tables[0].Rows[0]["BookedPrice"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "</table>";

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
                else
                {
                    StrMail = ObjGB.GETEMAILID(RefRequestID, "AGENT");
                    strMailMsg = "<table>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td style='text-align: center; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'><h2> " + MAILHEADER + "</h2>";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg += "<tr>";
                    strMailMsg += "<td style='text-align: left; font-size: 11px; font-weight: bold; padding: 5px;' colspan='9'>" + MAILMESSAGE + "";
                    strMailMsg += "</td>";
                    strMailMsg += "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td> ";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Group Booking ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["GrpID"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Request ID: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Booking Name: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Travel Date: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Departure_Date"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>AirLine : </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["aircode"]) + "(" + Convert.ToString(MailContent.Tables[0].Rows[0]["flightnumber"]) + ")";
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b>Journey Plan: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["Sector"]);
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
                    strMailMsg = strMailMsg + "<td><b>Numbers of Adult: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["AdultCount"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["ChildCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Child: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ChildCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    if (Convert.ToInt32(MailContent.Tables[0].Rows[0]["InfantCount"].ToString()) > 0)
                    {
                        strMailMsg = strMailMsg + "<tr>";
                        strMailMsg = strMailMsg + "<td><b>Numbers of Infant: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["InfantCount"]);
                        strMailMsg = strMailMsg + "</td>";
                        strMailMsg = strMailMsg + "</tr>";
                    }
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Your Expacted Fare: </b>" + Convert.ToString(MailContent.Tables[0].Rows[0]["ExpactedPrice"]);
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "<tr>";
                    strMailMsg = strMailMsg + "<td><b> Quoted Fare: </b>" + VendorQuote;
                    strMailMsg = strMailMsg + "</td>";
                    strMailMsg = strMailMsg + "</tr>";
                    strMailMsg = strMailMsg + "</table>";
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
            ErrorLogTrace.WriteErrorLog(ex, "AgentMailSending");
        }
        return i;
    }
    protected int AdminNExecMailSending(string MailMSG, string RequestID, string RefRequestID)
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
            DataSet AgencyDS = new DataSet();
            SqlTransaction ST = new SqlTransaction();
            AgencyDS = ST.GetAgencyDetails(Convert.ToString(MailContent.Tables[0].Rows[0]["CreatedBy"]));
            MailContent.Tables[0].Select("");
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
            if (_cmdtype.ToUpper() == "CANCELLATION REQUESTED" || _cmdtype.ToUpper() == "CANCELLED")
            {
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>CANCELLATION REMARKS</td>";
            }
            else
            {
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>EXPACTED PRICE</td>";
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKED PRICE</td>";
            }
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
            if (_cmdtype.ToUpper() == "CANCELLATION REQUESTED" || _cmdtype.ToUpper() == "CANCELLED")
            {
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CancellationRemarks"]) + "</td>";
            }
            else
            {
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["ExpactedPrice"]) + "</td>";
                strMailMsg += "<td style='font-size: 11px; font-weight: bold;'>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookedPrice"]) + "</td>";
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
                strMailMsg += "<td>REQUESTID</td>";
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
                if (_cmdtype.ToUpper() == "CANCELLED")
                {
                    DT = MailContent.Tables[0];
                }
                else
                {
                    DT = MailContent.Tables[0].Select(String.Format("SNO = '{0}'", RequestID)).CopyToDataTable();
                }
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
                //}
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
                strMailMsg += "</table>";
                if (MailContent.Tables[1].Rows.Count > 0)
                {
                    strMailMsg += "<table style='width:100%;'>";
                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;'>";
                    strMailMsg += "<td>REQUESTID</td>";
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
                        strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[1].Rows[k]["RequestID"]) + "</td>";
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
    protected bool AutoRefund(string RequestID, string Status, double Amount)
    {
        bool Rfndstatus = false;
        RefundResponse objRefnResp = new RefundResponse();
        _CrOrDb objParamCrd = new _CrOrDb();
        ITZcrdb objCrd = new ITZcrdb();
        ITZGetbalance objItzBal = new ITZGetbalance();
        _GetBalance objParamBal = new _GetBalance();
        GetBalanceResponse objBalResp = new GetBalanceResponse();
        SqlTransaction ST = new SqlTransaction();
        SqlTransactionDom STDom = new SqlTransactionDom();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        try
        {
            DataSet ds = new DataSet();
            ds = ST.GetFltHeaderDetail(RequestID);
            DataTable dtID = new DataTable();
            dtID = ds.Tables[0];
            string ChecksSatus = Status;
            Itz_Trans_Dal objItzT = new Itz_Trans_Dal();
            bool inst = false;
            ITZ_Trans objIzT = new ITZ_Trans();
            double ablBalance = 0;
            if ((ChecksSatus == "Rejected"))
            {
                double Aval_Bal = ST.AddCrdLimit(dtID.Rows[0]["AgentId"].ToString(), Amount);
                //Adding Refund Amount in Agent balance
                //objParamCrd._DECODE = IIf(Session["_DCODE"] <> Nothing, Session["_DCODE"].ToString().Trim(), " ")
                RandomKeyGenerator rndnum = new RandomKeyGenerator();
                string numRand = rndnum.Generate();
                try
                {
                    objParamCrd._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
                    //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    objParamCrd._AMOUNT = (Convert.ToString(Amount) != null ? Convert.ToString(Amount) : "0");
                    objParamCrd._ORDERID = (numRand != null && !string.IsNullOrEmpty(numRand) ? numRand.Trim() : " ");
                    objParamCrd._REFUNDORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID.Trim() : "");
                    objParamCrd._MODE = (Session["ModeTypeITZ"] != null ? Session["ModeTypeITZ"].ToString().Trim() : " ");
                    //'IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                    objParamCrd._REFUNDTYPE = "F";
                    //'objParamCrd._CHECKSUM = " "
                    string stringtoenc = "MERCHANTKEY=" + objParamCrd._MERCHANT_KEY + "&ORDERID=" + objParamCrd._ORDERID + "&REFUNDTYPE=" + objParamCrd._REFUNDTYPE;
                    objParamCrd._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc);
                    //objParamCrd._SERVICE_TYPE = IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                    objParamCrd._DESCRIPTION = "Refund to agent -" + dtID.Rows[0]["AgentId"].ToString() + " against requestID-" + RequestID.ToString();
                    objRefnResp = objCrd.ITZRefund(objParamCrd);
                    if (objRefnResp.MESSAGE.Trim().ToLower().Contains("successfully execute"))
                    {
                        Rfndstatus = true;
                        // ST.RejectHoldPNRStatusIntl(OrderID, "Äuto", "Rejected", "Auto Rejected on failure.", dtID.Rows(0)("GdsPnr").ToString(), "Rejected", dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Dom. PNR Auto Rejected Against  OrderID=" + OrderID, dtID.Rows(0)("AgencyName").ToString(),
                        // dtID.Rows(0)("AgentId").ToString());
                        STDom.insertLedgerDetails(dtID.Rows[0]["AgentId"].ToString(), dtID.Rows[0]["AgencyName"].ToString(), RequestID, dtID.Rows[0]["GdsPnr"].ToString(), "", "", (objRefnResp.EASY_ORDER_ID != null ? objRefnResp.EASY_ORDER_ID : ""), "", Session["UID"].ToString(), Request.UserHostAddress,
                        0, 0, Aval_Bal, "Group booking Auto Rejection", "Auto Refund Against  RequestID=" + RequestID, 0);
                    }
                }
                catch (Exception ex)
                {
                }
                objItzT = new Itz_Trans_Dal();
                try
                {
                    objIzT.AMT_TO_DED = "0";
                    objIzT.AMT_TO_CRE = (dtID.Rows[0]["TotalAfterDis"] != null ? dtID.Rows[0]["TotalAfterDis"].ToString() : "0");
                    objIzT.B2C_MBLNO_ITZ = " ";
                    objIzT.COMMI_ITZ = " ";
                    objIzT.CONVFEE_ITZ = " ";
                    objIzT.DECODE_ITZ = (dtID.Rows[0]["AgentId"].ToString() != null ? dtID.Rows[0]["AgentId"].ToString().Trim() : "");
                    objIzT.EASY_ORDID_ITZ = (objRefnResp.EASY_ORDER_ID != null ? objRefnResp.EASY_ORDER_ID : "");
                    objIzT.EASY_TRANCODE_ITZ = (objRefnResp.EASY_TRAN_CODE != null ? objRefnResp.EASY_TRAN_CODE : "");
                    objIzT.ERROR_CODE = (objRefnResp.ERROR_CODE != null ? objRefnResp.ERROR_CODE : "");
                    objIzT.MERCHANT_KEY_ITZ = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : "");
                    //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    objIzT.MESSAGE_ITZ = (objRefnResp.MESSAGE != null ? objRefnResp.MESSAGE : "");
                    objIzT.ORDERID = (RequestID != null && !string.IsNullOrEmpty(RequestID) ? RequestID.Trim() : "");
                    objIzT.RATE_GROUP_ITZ = "";
                    objIzT.REFUND_TYPE_ITZ = (objRefnResp.REFUND_TYPE != null && !string.IsNullOrEmpty(objRefnResp.REFUND_TYPE) && objRefnResp.REFUND_TYPE != "" ? objRefnResp.REFUND_TYPE : "");
                    objIzT.SERIAL_NO_FROM = "";
                    objIzT.SERIAL_NO_TO = "";
                    objIzT.SVC_TAX_ITZ = "";
                    objIzT.TDS_ITZ = "";
                    objIzT.TOTAL_AMT_DED_ITZ = "";
                    objIzT.TRANS_TYPE = "REFUND";
                    objIzT.USER_NAME_ITZ = (dtID.Rows[0]["AgentId"].ToString() != null ? dtID.Rows[0]["AgentId"].ToString().Trim() : "");
                    try
                    {
                        objBalResp = new GetBalanceResponse();
                        objParamBal._DCODE = (dtID.Rows[0]["AgentId"].ToString() != null ? dtID.Rows[0]["AgentId"].ToString().Trim() : "");
                        objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : " ");
                        //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : "");
                        objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : "");
                        objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
                        objIzT.ACCTYPE_NAME_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME : "");
                        objIzT.AVAIL_BAL_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
                        Session["CL"] = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "");
                        ablBalance = Convert.ToDouble((objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : "0"));
                    }
                    catch (Exception ex)
                    {
                        ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
                    }
                    inst = objItzT.InsertItzTrans(objIzT);
                }
                catch (Exception ex)
                {
                    ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
                }

                //'STDom.insertLedgerDetails(dtID.Rows(0)("AgentId").ToString(), dtID.Rows(0)("AgencyName").ToString(), OrderID, dtID.Rows(0)("GdsPnr").ToString(), "", "", objIzT.EASY_ORDID_ITZ, "", "Auto", Request.UserHostAddress, 0, dtID.Rows(0)("TotalAfterDis").ToString(), Aval_Bal, "Dom. Rejection", "Dom. PNR Auto Rejected Against  OrderID=" & OrderID, 0)
                //' Response.Write("<script>alert('PNR Rejected Sucessfully')</script>")

            }
            else
            {
                //'Response.Write("<script>alert('PNR Already Rejected')</script>")

            }

        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "AutoRefund");
        }
        return Rfndstatus;
    }
    protected void OFlightDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_flightdetails1_Departure_Location");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_flightdetails1_Arrival_Location");
                if (lbl_odeploction != null)
                {
                    lbl_odeploction.Text = lbl_odeploction.Text.Substring(0, lbl_odeploction.Text.IndexOf(","));
                }
                if (lbl_ArrivalLocation != null)
                {
                    lbl_ArrivalLocation.Text = lbl_ArrivalLocation.Text.Substring(0, lbl_ArrivalLocation.Text.IndexOf(","));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OFlightDetails1_RowDataBound");
        }
    }
    protected void OFlightDetails2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_flightdetails2_Departure_Location");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_flightdetails2_Arrival_Location");
                if (lbl_odeploction != null)
                {
                    lbl_odeploction.Text = lbl_odeploction.Text.Substring(0, lbl_odeploction.Text.IndexOf(","));
                }
                if (lbl_ArrivalLocation != null)
                {
                    lbl_ArrivalLocation.Text = lbl_ArrivalLocation.Text.Substring(0, lbl_ArrivalLocation.Text.IndexOf(","));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OFlightDetails2_RowDataBound");
        }
    }
    protected void OFlightDetails3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_flightdetails3_Departure_Location");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_flightdetails3_Arrival_Location");
                if (lbl_odeploction != null)
                {
                    lbl_odeploction.Text = lbl_odeploction.Text.Substring(0, lbl_odeploction.Text.IndexOf(","));
                }
                if (lbl_ArrivalLocation != null)
                {
                    lbl_ArrivalLocation.Text = lbl_ArrivalLocation.Text.Substring(0, lbl_ArrivalLocation.Text.IndexOf(","));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OFlightDetails3_RowDataBound");
        }
    }
    protected void OFlightDetails4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_flightdetails4_Departure_Location");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_flightdetails4_Arrival_Location");
                if (lbl_odeploction != null)
                {
                    lbl_odeploction.Text = lbl_odeploction.Text.Substring(0, lbl_odeploction.Text.IndexOf(","));
                }
                if (lbl_ArrivalLocation != null)
                {
                    lbl_ArrivalLocation.Text = lbl_ArrivalLocation.Text.Substring(0, lbl_ArrivalLocation.Text.IndexOf(","));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OFlightDetails4_RowDataBound");
        }
    }
    protected void OFlightDetails5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_odeploction = (Label)e.Row.FindControl("lbl_flightdetails5_Departure_Location");
                Label lbl_ArrivalLocation = (Label)e.Row.FindControl("lbl_flightdetails5_Arrival_Location");
                if (lbl_odeploction != null)
                {
                    lbl_odeploction.Text = lbl_odeploction.Text.Substring(0, lbl_odeploction.Text.IndexOf(","));
                }
                if (lbl_ArrivalLocation != null)
                {
                    lbl_ArrivalLocation.Text = lbl_ArrivalLocation.Text.Substring(0, lbl_ArrivalLocation.Text.IndexOf(","));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OFlightDetails5_RowDataBound");
        }
    }
    void ClearInputs(ControlCollection ctrls)
    {
        try
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                ClearInputs(ctrl.Controls);
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ClearInputs");
        }

    }
    public string CurrentTime()
    {
        string CurrentTime = "";
        try
        {
            CurrentTime = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "CurrentTime");
        }
        return CurrentTime;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow PD in GridView1.Rows)
            {
                TextBox Title = (TextBox)PD.FindControl("txt_title");
                TextBox FName = (TextBox)PD.FindControl("txt_fname");
                TextBox MName = (TextBox)PD.FindControl("txt_mname");
                TextBox LName = (TextBox)PD.FindControl("txt_lNAME");
                TextBox PaxType = (TextBox)PD.FindControl("txt_PaxType");
                TextBox DOB = (TextBox)PD.FindControl("txt_DOB");
                DropDownList Gender = (DropDownList)PD.FindControl("dll_gender");
                TextBox PassportNo = (TextBox)PD.FindControl("txt_passport");
                TextBox PassportExpireDate = (TextBox)PD.FindControl("txt_passportex");
                TextBox NationalityCode = (TextBox)PD.FindControl("txt_NationalityCode");
                TextBox IssueCountryCode = (TextBox)PD.FindControl("txt_IssueCountryCode");
                Label lbl_paxcounter = (Label)PD.FindControl("lbl_paxcounter");
                ObjGB.UPDATEPAXINFOR(Title.Text.Trim(), FName.Text.Trim(), MName.Text.Trim(), LName.Text.Trim(), PaxType.Text.Trim(), DOB.Text.Trim(), Gender.SelectedValue, PassportExpireDate.Text.Trim(), PassportNo.Text.Trim(), IssueCountryCode.Text.Trim(), NationalityCode.Text.Trim(), Convert.ToInt32(lbl_paxcounter.Text.Trim()), "UPDATED");
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Pax information has been updated!!');", true);
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "Button1_Click");
        }
    }
    protected void link_Payment_Click(object sender, EventArgs e)
    {
        try
        {
            dynamic ReqID = ((LinkButton)sender).CommandArgument.ToString();
            if (ReqID != "")
            {
                CustInfoDS = ObjGB.GroupRequestDetails(ReqID, "CLIENTREQUEST", "", "AGENT");
                {
                    if (CustInfoDS.Tables[1].Rows.Count > 0 && CustInfoDS.Tables[1].Rows[0]["Status"].ToString().ToLower() == "freezed")
                    {
                        string PayemtLinkVal = "", GRPUserID = "", Fare = "", Trip = "", ISPaxDtls = "", Status = "";
                        PayemtLinkVal = CustInfoDS.Tables[1].Rows[0]["PaymentLink"].ToString();
                        string StrPGVal = rblPaymentMode.SelectedValue;
                        if (PayemtLinkVal != "")
                        {
                            GRPUserID = PayemtLinkVal.Substring(PayemtLinkVal.IndexOf("userid="), PayemtLinkVal.IndexOf("&fare") - PayemtLinkVal.IndexOf("userid="));
                            GRPUserID = GRPUserID.Substring(7);
                            Fare = PayemtLinkVal.Substring(PayemtLinkVal.IndexOf("fare="), PayemtLinkVal.IndexOf("&RequestID") - PayemtLinkVal.IndexOf("fare="));
                            Fare = Fare.Substring(5);
                            ISPaxDtls = PayemtLinkVal.Substring(PayemtLinkVal.IndexOf("PD="), PayemtLinkVal.IndexOf("&Status") - PayemtLinkVal.IndexOf("PD="));
                            ISPaxDtls = ISPaxDtls.Substring(3);
                            Status = PayemtLinkVal.Substring(PayemtLinkVal.IndexOf("Status="), PayemtLinkVal.IndexOf("&TT") - PayemtLinkVal.IndexOf("Status="));
                            Status = Status.Substring(7);
                            TripType = CustInfoDS.Tables[1].Rows[0]["TripType"].ToString();
                            Trip = PayemtLinkVal.Substring(PayemtLinkVal.IndexOf("TT="), PayemtLinkVal.IndexOf("&Payment") - PayemtLinkVal.IndexOf("TT="));
                            Trip = Trip.Substring(3);
                            Response.Redirect("../GroupSearch/GroupPayment.aspx?RequestID=" + ReqID + "&Fare=" + Fare + "&GRPUserID=" + GRPUserID + "&TripType=" + TripType + "&ISPaxDtls=" + ISPaxDtls + "&Status=" + Status + "&Payment=on&TT=" + Trip + "&PGVAL=" + StrPGVal + "");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "link_Payment_Click");
        }
    }
    protected void OneWayTrip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            RequestID = Request.QueryString["RequestID"].ToString();
            CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "CUSTOMERINFO", "", "");
            DataTable Saparate_DTOneWay = new DataTable();
            Saparate_DTOneWay = CustInfoDS.Tables[0].DefaultView.ToTable(true, "SNO");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_lbl_Reqnoid = (Label)e.Row.FindControl("lbl_SNO");
                TextBox txt_Request_no = (TextBox)e.Row.FindControl("txt_Request_no");
                for (int z = 0; z < Saparate_DTOneWay.Rows.Count; z++)
                {
                    string reqno = "";
                    reqno = Saparate_DTOneWay.Rows[z]["SNO"].ToString();
                    if (lbl_lbl_Reqnoid.Text.Trim() == reqno)
                    {
                        txt_Request_no.Text = "Request No:" + (z + 1) + "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "OneWayTrip_RowDataBound");
        }
    }
    protected void BookingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "CancelBooking")
        {
            LinkButton lb1 = e.CommandSource as LinkButton;
            GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
            int RowIndex = gvr1.RowIndex;
            ViewState["RowIndex"] = RowIndex;
            TextBox txtRemark = (TextBox)BookingDetails.Rows[RowIndex].FindControl("txtRemark");
            LinkButton lnkSubmit = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkSubmit_1");
            LinkButton lnkHides = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkHides_1");
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
            TextBox txtRemark = (TextBox)BookingDetails.Rows[RowIndex].FindControl("txtRemark");
            LinkButton lnkSubmit = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkSubmit_1");
            LinkButton lnkHides = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkHides_1");
            txtRemark.Visible = true;
            lnkSubmit.Visible = true;
            lnkHides.Visible = true;
            if (txtRemark.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(10);", true);
            }
            else
            {
                RequestID = Request.QueryString["RequestID"];
                string User = Session["UID"].ToString();
                string User_Type = Session["User_Type"].ToString();
                string PaymentStatus = "";
                try
                {
                    ObjGB.RequestStatus(RequestID, User, User_Type, txtRemark.Text.Trim().ToString());
                    CustInfoDS = ObjGB.GroupRequestDetails(RequestID, "CLIENTREQUEST", Session["UID"].ToString(), Session["User_Type"].ToString());
                    if (CustInfoDS.Tables[1].Rows.Count > 0)
                    {
                        PaymentStatus = Convert.ToString(CustInfoDS.Tables[1].Rows[0]["Status"]);
                        if (PaymentStatus.ToLower() == "cancellation requested")
                        {
                            ObjGB.InsertRedundRequest(Convert.ToString(CustInfoDS.Tables[1].Rows[0]["RequestID"]), Convert.ToString(CustInfoDS.Tables[0].Rows[0]["RequestID"]), User,
                                Convert.ToString(CustInfoDS.Tables[1].Rows[0]["Remarks"]), txtRemark.Text.ToString(), Convert.ToString(CustInfoDS.Tables[0].Rows[0]["Sector"]), Convert.ToString(CustInfoDS.Tables[1].Rows[0]["BookedPrice"]), Convert.ToString(CustInfoDS.Tables[0].Rows[0]["Departure_Date"]), Convert.ToString(CustInfoDS.Tables[0].Rows[0]["Departure_Location"]), Convert.ToString(CustInfoDS.Tables[0].Rows[0]["Arrival_Location"]));

                        }

                        //AgentMailSending("0", Convert.ToString(CustInfoDS.Tables[0].Rows[0]["SNO"]), Convert.ToString(CustInfoDS.Tables[1].Rows[0]["RequestID"]));
                        ResultAgentMail = CancellationAgentMailSending(Convert.ToString(CustInfoDS.Tables[1].Rows[0]["RequestID"]));
                        ResultAdminMail = AdminNExecMailSending("Booking has been Cancelled", Convert.ToString(CustInfoDS.Tables[0].Rows[0]["SNO"]), Convert.ToString(CustInfoDS.Tables[1].Rows[0]["RequestID"]));
                        BookingDetails.Columns[7].Visible = false;
                        BookingDetails.Columns[8].Visible = false;
                        BookingDetails.Columns[9].Visible = false;
                        div_BookingDetails.Visible = true;
                        BookingDetails.DataSource = CustInfoDS.Tables[1];
                        BookingDetails.DataBind();
                    }
                    if ((User_Type == "AGENT" || User_Type == "ADMIN") && (ResultAgentMail == 1 && ResultAdminMail == 1))
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(5);", true);
                    }
                    else if (User_Type == "EXEC" && (ResultAgentMail == 1 && ResultAdminMail == 1))
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(4);", true);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogTrace.WriteErrorLog(ex, "link_Reject_RefRequestID_Click");
                    if (User_Type == "AGENT" || User_Type == "ADMIN")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(3);", true);
                    }
                    else if (User_Type == "EXEC")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(6);", true);
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
            TextBox txtRemark = (TextBox)BookingDetails.Rows[RowIndex].FindControl("txtRemark");
            LinkButton lnkSubmit = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkSubmit_1");
            LinkButton lnkHides = (LinkButton)BookingDetails.Rows[RowIndex].FindControl("lnkHides_1");
            txtRemark.Visible = false;
            lnkSubmit.Visible = false;
            lnkHides.Visible = false;
        }
        if (e.CommandName == "PaxInformation")
        {
            LinkButton lb1 = e.CommandSource as LinkButton;
            string ReqID = e.CommandArgument.ToString();
            GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
            int RowIndex = gvr1.RowIndex;
            ViewState["RowIndex"] = RowIndex;
            Label trip = (Label)BookingDetails.Rows[RowIndex].FindControl("lbltrip");
            if (trip.Text.Trim().ToString() == "International")
            {
                Response.Redirect("CustomerInfoIntl.aspx?RefRequestID=" + ReqID + "&Status=PAID&Payment=pax");
            }
            else
            {
                Response.Redirect("CustomerInfoDom.aspx?RefRequestID=" + ReqID + "&Status=PAID&Payment=pax");
            }
        }
    }
    protected void lnksubmitreq_Click(object sender, EventArgs e)
    {
        try
        {
            CancelRemarks.Visible = true;
            if (txt_can_remarks.Value == "")
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(10);", true);
            }
            else
            {
                RequestID = Request.QueryString["RequestID"];
                string User = Session["UID"].ToString();
                string User_Type = Session["User_Type"].ToString();
                ObjGB.RequestStatus(RequestID, User, User_Type, txt_can_remarks.Value.Trim().ToString());
                int i = EXECADMINMAIL(RequestID, txt_can_remarks.Value.Trim().ToString());
                if (i == 1)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "MyFunc(1);", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "lnksubmitreq_Click");
        }
    }
    protected void lnkcancelreq_Click(object sender, EventArgs e)
    {
        CancelRemarks.Visible = false;
        lb_reqcancl.Visible = true;
    }
    protected int EXECADMINMAIL(string RefRequestID, string RejectedRemarks)
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
            string REFSNO = Convert.ToString(MailContent.Tables[0].Rows[0]["SNO"]);
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
            strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>REJECTED REMARKS</td>";
            strMailMsg += "</tr>";
            strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["CreatedBy"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(AgencyDS.Tables[0].Rows[0]["Agency_Name"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["BookingName"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["RequestID"]) + "</td>";
            strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[2].Rows[0]["ExpactedPrice"]) + "</td>";
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
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
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
                    strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
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
    protected int CancellationAgentMailSending(string RefRequestID)
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
                strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; color: #4f6b72; border-right: 1px solid #C1DAD7; border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px; text-transform: uppercase;	text-align: left; padding: 6px 6px 6px 12px;'>";
                strMailMsg += "<td style='font-size: 10.5px; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKING NAME</td>";
                strMailMsg += "<td style='font-size: 10.5px; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>GROUP BOOKING ID</td>";
                if (_cmdtype.ToUpper() == "CANCELLATION REQUESTED")
                {
                    strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>BOOKED PRICE</td>";
                }
                strMailMsg += "<td style='font-size: 10.5px;  width: 20%; text-align: left; padding: 4px; font-weight: bold;'>CANCELLATION REMARKS</td>";
                strMailMsg += "</tr>";
                strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7;	background: #fff;	padding: 6px 6px 6px 12px;	color: #4f6b72;'>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookingName"]) + "</td>";
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["RequestID"]) + "</td>";
                if (_cmdtype.ToUpper() == "CANCELLATION REQUESTED")
                {
                    strMailMsg += "<td style='font-size: 15px; font-weight: bold;'>" + Convert.ToString(MailContent.Tables[0].Rows[0]["BookedPrice"]) + "</td>";
                }
                strMailMsg += "<td>" + Convert.ToString(MailContent.Tables[0].Rows[0]["CancellationRemarks"]) + "</td>";
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
                    strMailMsg += "<tr style='font: bold 11px 'Trebuchet MS', Verdana, Arial, Helvetica, sans-serif; border-right: 1px solid #C1DAD7; border-bottom: 1px solid #C1DAD7;	border-top: 1px solid #C1DAD7;	letter-spacing: 2px; text-transform: uppercase;	text-align: left; padding: 6px 6px 6px 12px;'>";
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
                    if (_cmdtype.ToUpper() == "CANCELLED")
                    {
                        DT = MailContent.Tables[1];
                    }
                    else
                    {
                        DT = MailContent.Tables[1].Select(String.Format("SNO = '{0}'", REFSNO)).CopyToDataTable();
                    }
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
                        strMailMsg += "<tr style='border-right: 1px solid #C1DAD7;	border-bottom: 1px solid #C1DAD7; background: #fff;	padding: 6px 6px 6px 12px;'>";
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
}