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

public partial class GroupSearch_InvoiceDetails : System.Web.UI.Page
{
    string Invoice = "", Requestno = "";
    GroupBooking ObjGB = new GroupBooking();
    DataSet tktDS = new DataSet();
    string TicketFormate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Requestno = Request.QueryString["RequestID"].ToString();
            if (Requestno != "")
            {
                invoicedetails.Text = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Booking Details</title>";
                invoicedetails.Text += "</head><body>";
                invoicedetails.Text += Invoicedetails(Requestno);
                invoicedetails.Text += "</body></html>";
            }
            else
            {
                invoicedetails.Text = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Booking Details</title>";
                invoicedetails.Text += "</head><body>";
                invoicedetails.Text += "No record found, please try after some time!!";
                invoicedetails.Text += "</body></html>";
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "Invoice");
        }

    }
    public DataTable TerminalDetails(string OrderID, string DepCity, string ArrvlCity)
    {
        DataTable dt1 = new DataTable();
        try
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
            SqlDataAdapter adap = new SqlDataAdapter("USP_GRP_TERMINAL_INFO", con1);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@DEPARTURECITY", DepCity);
            adap.SelectCommand.Parameters.AddWithValue("@ARRIVALCITY", ArrvlCity);
            adap.SelectCommand.Parameters.AddWithValue("@ORDERID", OrderID);
            con1.Open();
            adap.Fill(dt1);
            con1.Close();

        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "InvoiceTerminalDetails");
        }
        return dt1;
    }
    public string Invoicedetails(string GrpRequestno)
    {
        tktDS = ObjGB.TicketReport(GrpRequestno, "INVOICE");
        SqlTransaction ST = new SqlTransaction();
        DataSet AgencyDS = new DataSet();
        AgencyDS = ST.GetAgencyDetails(tktDS.Tables[1].Rows[0]["CreatedBy"].ToString());
        try
        {
            TicketFormate += "<table style='width:100%;'>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='width:50%;text-align:left;'>";
            TicketFormate += "<img src='http://www.RWT.co/images/logo.png' alt='Logo' style='height:54px; width:104px' />";
            TicketFormate += "</td>";
            TicketFormate += "<td style='width: 50%;text-align:right;display:none;'>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='width:50%;text-align:left;'>";
            TicketFormate += "";
            TicketFormate += "</td>";
            TicketFormate += "<td style='width: 50%;text-align:right;'>";
            TicketFormate += "";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='width:100%;height:10px;'></td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='2' style='vertical-align:bottom;color:#f58220;text-align:right;width:100%;font-size:16px;font-weight:bold;'>";
            TicketFormate += "Electronic Ticket";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='2' style='height: 2px; width: 100%; border: 1px solid #0f4da2'></td>";
            TicketFormate += "</tr>";
            TicketFormate += "</table>";
            TicketFormate += "<table style='width: 100%;'>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='width: 100%; text-align: justify; color: #0f4da2; font-size: 11px; padding: 10px;'>";
            TicketFormate += "This is travel itinerary and E-ticket receipt. You may need to show this receipt to enter the airport and/or to show return or onward travel to ";
            TicketFormate += "customs and immigration officials.";
            TicketFormate += "<br />";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "</table>";
            TicketFormate += "<table style='border: 1px solid #0f4da2; font-family: Verdana, Geneva, sans-serif; font-size: 12px;padding:0px !important;width:100%;'>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='text-align: left; background-color: #0f4da2; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;' colspan='7'>";
            TicketFormate += "Passenger & Ticket Information";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>";
            TicketFormate += "<table>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>";
            TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
            if (tktDS.Tables[1].Rows[0]["GDSPNR"].ToString() != tktDS.Tables[1].Rows[0]["GDSPNR_INBOND"].ToString() && tktDS.Tables[1].Rows[0]["GDSPNR_INBOND"].ToString().ToLower() != "null")
            {
                TicketFormate += tktDS.Tables[1].Rows[0]["GDSPNR"].ToString() + "<br/>  Inbound GDSPNR --" + tktDS.Tables[1].Rows[0]["GDSPNR_INBOND"].ToString();
            }
            else
            {
                TicketFormate += tktDS.Tables[1].Rows[0]["GDSPNR"].ToString();
            }
            TicketFormate += "</td>";
            TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>";
            TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
            TicketFormate += AgencyDS.Tables[0].Rows[0]["Agency_Name"].ToString();//agencyname
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>";
            TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
            if (tktDS.Tables[1].Rows[0]["AIRLINEPNR"].ToString() != tktDS.Tables[1].Rows[0]["AIRLINE_INBOND"].ToString() && tktDS.Tables[1].Rows[0]["AIRLINE_INBOND"].ToString().ToLower() != "null")
            {
                TicketFormate += tktDS.Tables[1].Rows[0]["AIRLINEPNR"].ToString() + "<br/>  Inbound AIRLINEPNR --" + tktDS.Tables[1].Rows[0]["AIRLINE_INBOND"].ToString();
            }
            else
            {
                TicketFormate += tktDS.Tables[1].Rows[0]["AIRLINEPNR"].ToString();
            }
            TicketFormate += "</td>";
            TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>";
            TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
            TicketFormate += AgencyDS.Tables[0].Rows[0]["Mobile"].ToString();//mobile
            TicketFormate += "<br/>";
            TicketFormate += AgencyDS.Tables[0].Rows[0]["Email"].ToString();//Email
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>";
            TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
            TicketFormate += tktDS.Tables[1].Rows[0]["Status"].ToString();
            TicketFormate += "</td>";
            TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>";
            TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
            TicketFormate += tktDS.Tables[1].Rows[0]["CreatedDate"].ToString();//createdate
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            for (int i = 0; i < tktDS.Tables[2].Rows.Count; i++)
            {
                TicketFormate += "<tr>";
                TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>";
                TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
                TicketFormate += tktDS.Tables[2].Rows[i]["FName"] + " " + "(" + tktDS.Tables[2].Rows[i]["PaxType"] + ")";
                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; font-weight:bold; width: 35%; text-align: left; padding: 5px;'>";
                TicketFormate += tktDS.Tables[2].Rows[i]["TicketNumber"];
                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; font-weight:bold; width: 35%; text-align: left; padding: 5px;'>";
                TicketFormate += Convert.ToString(tktDS.Tables[2].Rows[i]["TicketNumber_INBOND"]);
                TicketFormate += "</td>";
                TicketFormate += "</tr>";
            }
            TicketFormate += "</table>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='text-align: left; background-color: #0f4da2; color: #fff; width: 100%; padding: 5px;' colspan='7'>";
            TicketFormate += "<table style='width:100%;'>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='text-align: left; color: #f58220; font-size: 11px; width: 25%;font-weight:bold;' colspan='7'>";
            TicketFormate += "Flight Information";
            TicketFormate += "</td>";
            TicketFormate += "<td colspan='5' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>";
            TicketFormate += "</tr>";
            TicketFormate += "</table>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='7' style='height:5px;'>&nbsp;</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='7' style='background-color: #0f4da2;width:100%;'>";
            TicketFormate += "<table style='width:100%;'>";
            TicketFormate += "<tr>";
            TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>FLIGHT</td>";
            TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>DEPART</td>";
            TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE</td>";
            TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>DEPART AIRPORT/TERMINAL</td>";
            TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE AIRPORT/TERMINAL</td>";
            TicketFormate += "</tr>";
            for (int j = 0; j < tktDS.Tables[3].Rows.Count; j++)
            {
                TicketFormate += "</table>";
                TicketFormate += "</td>";
                TicketFormate += "</tr>";
                TicketFormate += "<tr>";
                TicketFormate += "<td colspan='5' style='width:100%;'>";
                TicketFormate += "<table style='width:100%;'>";
                TicketFormate += "<tr>";
                TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>";
                TicketFormate += tktDS.Tables[3].Rows[j]["AirCode"] + " " + tktDS.Tables[3].Rows[j]["FlightNumber"];
                TicketFormate += "<br/>";
                TicketFormate += "<br/>";
                TicketFormate += "<img alt='Logo Not Found' src='http://www.RWT.co/AirLogo/sm" + tktDS.Tables[3].Rows[j]["VC"] + ".gif' ></img>";
                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
                string strDepdt = Convert.ToString(tktDS.Tables[3].Rows[j]["Departure_Date"]);
                strDepdt = (strDepdt.Length == 8 ? STD.BAL.Utility.Left(strDepdt, 2) + "-" + STD.BAL.Utility.Mid(strDepdt, 3, 2) + "-" + STD.BAL.Utility.Right(strDepdt, 2) : "20" + STD.BAL.Utility.Right(strDepdt, 2) + "-" + STD.BAL.Utility.Mid(strDepdt, 3, 2) + "-" + STD.BAL.Utility.Left(strDepdt, 2));
                DateTime deptdt = Convert.ToDateTime(strDepdt);
                strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/");
                string depDay = Convert.ToString(deptdt.DayOfWeek);
                //strDepdt = strDepdt.Split("/")(0).ToString() + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2);
                string strdeptime = Convert.ToString(tktDS.Tables[3].Rows[j]["Departure_Time"]);
                // strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2);
                TicketFormate += strDepdt;
                TicketFormate += "<br/>";
                TicketFormate += "<br/>";
                TicketFormate += strdeptime;
                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
                string strArvdt = Convert.ToString(tktDS.Tables[3].Rows[j]["Arrival_Date"]);

                strArvdt = (strArvdt.Length == 8 ? STD.BAL.Utility.Left(strArvdt, 2) + "-" + STD.BAL.Utility.Mid(strArvdt, 3, 2) + "-" + STD.BAL.Utility.Right(strArvdt, 2) : "20" + STD.BAL.Utility.Right(strArvdt, 2) + "-" + STD.BAL.Utility.Mid(strArvdt, 3, 2) + "-" + STD.BAL.Utility.Left(strArvdt, 2));
                DateTime Arrdt = Convert.ToDateTime(strArvdt);
                strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/");
                string ArrDay = Convert.ToString(Arrdt.DayOfWeek);
                //strArvdt = strArvdt.Split("/")(0) + " " + strArvdt.Split("/")(1) + " " + strArvdt.Split("/")(2);
                string strArrtime = Convert.ToString(tktDS.Tables[3].Rows[j]["Arrival_Time"]);
                // strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2);
                TicketFormate += strArvdt;
                TicketFormate += "<br/>";
                TicketFormate += "<br/>";
                TicketFormate += strArrtime;
                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
                //TicketFormate += FltDetailsList.Rows(f)("DepAirName") + "( " + FltDetailsList.Rows(f)("DFrom") + ")";
                TicketFormate += tktDS.Tables[3].Rows[j]["Departure_Location"];
                TicketFormate += "<br />";
                TicketFormate += "<br />";
                DataTable fltTerminalDetails = new DataTable();
                fltTerminalDetails = TerminalDetails(Request.QueryString["RequestID"], Convert.ToString(tktDS.Tables[3].Rows[j]["Departure_AirportCode"]), "");
                if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["DepTerminal"])))
                {
                    TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml: NA";
                }
                else
                {
                    TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["DepTerminal"];
                }

                TicketFormate += "</td>";
                TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
                TicketFormate += tktDS.Tables[3].Rows[j]["Arrival_Location"];
                TicketFormate += "<br />";
                TicketFormate += "<br />";
                fltTerminalDetails = TerminalDetails(Request.QueryString["RequestID"], "", Convert.ToString(tktDS.Tables[3].Rows[j]["Arrival_AirportCode"]));
                if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["ArrTerminal"])))
                {
                    TicketFormate += fltTerminalDetails.Rows[0]["ArrvAirportName"] + " - Trml: NA";
                }
                else
                {
                    TicketFormate += fltTerminalDetails.Rows[0]["ArrvAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["ArrTerminal"];
                }

                TicketFormate += "</td>";
                TicketFormate += "</tr>";
                TicketFormate += "</table>";
                TicketFormate += "</td>";
                TicketFormate += "</tr>";
                TicketFormate += "<tr>";
                TicketFormate += "<td colspan='7' style='width:100%;'>";
                TicketFormate += "<table style='width:100%;'>";
                TicketFormate += "<tr>";
                TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>";
                TicketFormate += "<br/>";
                TicketFormate += "</td>";
                TicketFormate += "<td style='width: 32%;'></td>";
                TicketFormate += "<td style='width: 18%; font-size:11px;text-align:left;'></td>";
                TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>";
                TicketFormate += "</tr>";
            }
            TicketFormate += "</table>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "<br/><br/>";
            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='7'>";
            TicketFormate += "<ul style='list-style-image: url(http://www.RWT.co/Images/bullet.png);'>";
            TicketFormate += "<li style='font-size:10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.";
            TicketFormate += "For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>";
            TicketFormate += "</ul>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";

            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='7' style='background-color: #0f4da2; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;'>TERMS AND CONDITIONS :</td>";
            TicketFormate += "</tr>";

            TicketFormate += "<tr>";
            TicketFormate += "<td colspan='7'>";
            TicketFormate += "<ul style='list-style-image: url(http://www.RWT.co/Images/bullet.png);'>";
            TicketFormate += "<li style='font-size:10.5px;'>Guests are requested to carry their valid photo identification for all guests, including children.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>We recommend check-in at least 2 hours prior to departure.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>";
            TicketFormate += "Flight schedules are subject to change and approval by authorities.";
            TicketFormate += "<br />";
            TicketFormate += "</li>";
            TicketFormate += "<li style='font-size:10.5px;'>";
            TicketFormate += "Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.";
            TicketFormate += "<br />";
            TicketFormate += " Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.";
            TicketFormate += "</li>";
            TicketFormate += "<li style='font-size:10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>";

            TicketFormate += "<li style='font-size:10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>Cancellation amount will be charged as per airline rule.</li>";
            TicketFormate += "<li style='font-size:10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>";
            TicketFormate += "</ul>";
            TicketFormate += "</td>";
            TicketFormate += "</tr>";
            TicketFormate += "</table>";
            TicketFormate += "</table>";
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "Tktcopy");
        }
        return TicketFormate;
    }
}