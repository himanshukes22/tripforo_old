using System;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;




public partial class Report_OrderDeatils : System.Web.UI.Page

{
    string imagevc = "";
    public string imgurlvc;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ToString());


    //booking details
    public string passorderid;
    StringBuilder srn = new StringBuilder();

    StringBuilder htmlTable = new StringBuilder();
    StringBuilder paxHtmlTable = new StringBuilder();
    StringBuilder bse = new StringBuilder();
    StringBuilder t = new StringBuilder();
    StringBuilder ap = new StringBuilder();
    StringBuilder gp = new StringBuilder();
    StringBuilder tno = new StringBuilder();
    StringBuilder tStatus = new StringBuilder();
    StringBuilder cm = new StringBuilder();
    StringBuilder nf = new StringBuilder();
    StringBuilder gf = new StringBuilder();
    StringBuilder bcls = new StringBuilder();
    StringBuilder rf = new StringBuilder();


    StringBuilder fltpanel = new StringBuilder();
    StringBuilder fltnoo = new StringBuilder();
    StringBuilder acode = new StringBuilder();
    StringBuilder atimedate = new StringBuilder();
    StringBuilder deairportcode = new StringBuilder();
    StringBuilder dtimedate = new StringBuilder();
    // booking details end

    //cart infromation
    StringBuilder cf1 = new StringBuilder();
    StringBuilder cf2 = new StringBuilder();
    StringBuilder cf3 = new StringBuilder();
    StringBuilder cf4 = new StringBuilder();
    StringBuilder cf5 = new StringBuilder();
    StringBuilder cf6 = new StringBuilder();
    StringBuilder cf7 = new StringBuilder();
    StringBuilder cf8 = new StringBuilder();

    //cart head Info
    StringBuilder chf1 = new StringBuilder();
    StringBuilder chf2 = new StringBuilder();

    //paymentprocess
    StringBuilder payment = new StringBuilder();

    bool IsAllPaxCancelled = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UID"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {

            passorderid = Request.QueryString["OrderId"].ToString();
            Cart_Head_Info(passorderid);
            BookingDetails(passorderid);
            Cart_Information(passorderid);
            PyamentProcess(passorderid);

            this.imgurlvc = imagevc;

            //string exword = Request.QueryString("email");
            //if (exword != "Nothing" & exword == "true")
            //{
            //    btn_exporttoword_Click(sender, e);
            //}

        }
    }
    public void Cart_Head_Info(string ordid)
    {

        chf1.Append(ordid);
        CartInformation.Controls.Add(new Literal { Text = chf1.ToString() });
        BkId.Controls.Add(new Literal { Text = chf1.ToString() });

    }
    public void Cart_Information(string ordid)
    {
        try
        {
            string ord = "";
            string amt = "";
            string st = "";
            string mst = "";
            string cd = "";
            string ag = "";
            string eml = "";
            string mob = "";
            con.Open();
            String sp = "GetCartInformation";
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ordid", ordid);
            SqlDataReader ci = cmd.ExecuteReader();
            if (ci.HasRows)
            {

                while (ci.Read())
                {
                    ord = ci["OrderId"].ToString();
                    amt = ci["amount"].ToString();
                    st = ci["status"].ToString();
                    mst = ci["MordifyStatus"].ToString();
                    cd = ci["cdate"].ToString();
                    ag = ci["agent"].ToString();
                    eml = ci["email"].ToString();
                    mob = ci["mob"].ToString();
                }
                cf1.Append(" <label>Booking ID : <strong>" + " " + ord + "</strong></label>");
                cf2.Append(" <label>Amount : <strong>&#8377; " + "" + amt + "</strong></label>");

                if (!string.IsNullOrEmpty(mst) && IsAllPaxCancelled == false)
                {
                    if (mst.Equals("Cancelled"))
                    {
                        cf3.Append(" <label>Status : <strong style='color:red;'>" + " " + mst.ToUpper() + "</strong></label>");
                    }
                    else
                    {
                        cf3.Append(" <label>Status : <strong style='color:#2a9240;'>" + " " + mst.ToUpper() + "</strong></label>");
                    }
                }
                else if (st.Equals("Ticketed"))
                {
                    cf3.Append(" <label>Status : <strong style='color:#2a9240;'>" + " " + st.ToUpper() + "</strong></label>");
                }
                else
                {
                    cf3.Append(" <label>Status : <strong style='color:red;'>" + " " + st.ToUpper() + "</strong></label>");

                }
                cf4.Append(" <label>Created On : <strong>" + " " + cd + "</strong></label>");
                cf8.Append(" <label>Booking Date : <strong>" + " " + cd + "</strong></label>");
                cf5.Append(" <label>Agent : <strong>" + " " + ag + "</strong></label>");
                cf6.Append(" <label>Agent Email : <strong>" + " " + eml + "</strong></label>");
                cf7.Append(" <label>Agent Mobile : <strong>" + " " + mob + "</strong></label>");

                BookingId.Controls.Add(new Literal { Text = cf1.ToString() });
                Amount.Controls.Add(new Literal { Text = cf2.ToString() });
                Status.Controls.Add(new Literal { Text = cf3.ToString() });
                CreateDate.Controls.Add(new Literal { Text = cf4.ToString() });
                // AgentID.Controls.Add(new Literal { Text = cf5.ToString() });
                AgentName.Controls.Add(new Literal { Text = cf5.ToString() });
                BookingDate.Controls.Add(new Literal { Text = cf8.ToString() });
                AgentEmail.Controls.Add(new Literal { Text = cf6.ToString() });
                AgentMobile.Controls.Add(new Literal { Text = cf7.ToString() });


                ci.Close();
                ci.Dispose();
            }

            con.Close();
        }
        catch (Exception ex)
        {

            clsErrorLog.LogInfo(ex);
        }
    }

    public void BookingDetails(string OrderId)
    {
        try
        {
            int i = 0;
            string basefare = "";
            decimal tax = 0;
            string airlinepnr = "";
            string gdspnr = "";
            string tcktno = "";
            decimal commsn = 0;
            decimal netfare = 0;
            decimal gfare = 0;
            string bclass = "";
            string refndble = "";
            string baname = "";
            string airlinecode = "";
            string fltno = "";
            string arrairportcode = "";
            string arrtime = "";
            string arrdate = "";
            string depairportcode = "";
            string deptime = "";
            string depdate = "";
            string arairportname = "";
            string arairportcity = "";
            string arairportcounty = "";
            string depairportname = "";
            string depairportcity = "";
            string depairportcounty = "";
            //string imagevc = "";

            con.Open();
            String sp = "GetBookingDetails";

            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ordid", OrderId);
            SqlDataReader Booking = cmd.ExecuteReader();
            if (Booking.HasRows)
            {

                while (Booking.Read())
                {
                    i++;
                    string fn = Booking["FName"].ToString();
                    string ln = Booking["LName"].ToString();
                    string tl = Booking["Title"].ToString();
                    string fname = fn.ToUpper();
                    string lname = ln.ToUpper();
                    string title = tl.ToUpper();
                    srn.Append("<p><strong>" + i + "</strong></p>");
                    htmlTable.Append(" <p>" + title + " " + fname + " " + lname + "</p>");
                    if (Booking["PaxType"].ToString().ToLower().Trim() == "adt")
                    {
                        paxHtmlTable.Append("<p>ADULT</p>");
                    }
                    else if (Booking["PaxType"].ToString().ToLower().Trim() == "chd")
                    {
                        paxHtmlTable.Append("<p>CHILD</p>");
                    }
                    else
                    {
                        paxHtmlTable.Append("<p>INFANT</p>");
                    }

                    //basefare = Booking["BaseFare"].ToString();
                    bse.Append(" <p><strong>&#8377; " + Booking["BaseFare"] + "</strong></p>");


                    tax = tax + (decimal)Booking["totaltax"];
                    airlinepnr = Booking["Airlinepnr"].ToString();
                    gdspnr = Booking["gdspnr"].ToString();
                    // tcktno = Booking["TicketNumber"].ToString();
                    tno.Append(" <p><strong>" + Booking["TicketNumber"] + "</strong></p>");
                    if (!string.IsNullOrEmpty(Booking["PaxStatus"].ToString()) && Booking["PaxStatus"].ToString() == "Cancelled")
                    {
                        tStatus.Append("<p><strong style='color:red;font-weight: bold;'>" + Booking["PaxStatus"] + "</strong></p>");
                    }
                    else if (!string.IsNullOrEmpty(Booking["PaxStatus"].ToString()) && Booking["PaxStatus"].ToString() != "Cancelled")
                    {
                        tStatus.Append("<p><strong>" + Booking["PaxStatus"] + "</strong></p>");
                    }
                    else
                    {
                        IsAllPaxCancelled = true;
                        tStatus.Append("<p><strong style='color:green;font-weight: bold;'>Ticketed</strong></p>");
                    }

                    commsn = commsn + (decimal)Booking["commission"];
                    netfare = netfare + (decimal)Booking["BaseFare"];
                    gfare = gfare + (decimal)Booking["grossfare"];
                    bclass = Booking["bookingclass"].ToString();
                    refndble = Booking["refundable"].ToString();
                    baname = Booking["airlinename"].ToString();
                    airlinecode = Booking["airlinecode"].ToString();
                    fltno = Booking["fltno"].ToString();

                    arrairportcode = Booking["arrairportcode"].ToString();
                    arrtime = Booking["arrtime"].ToString();
                    arrdate = Booking["arrdate"].ToString();
                    arairportname = Booking["arrivalairportname"].ToString();
                    arairportcity = Booking["acityname"].ToString();
                    arairportcounty = Booking["acountyname"].ToString();

                    depairportcode = Booking["depairportcode"].ToString();
                    deptime = Booking["deptime"].ToString();
                    depdate = Booking["depdate"].ToString();
                    depairportname = Booking["depairportname"].ToString();
                    depairportcity = Booking["depcityname"].ToString();
                    depairportcounty = Booking["depcountyname"].ToString();

                    imagevc = Booking["VC"].ToString();



                }

                fltpanel.Append(baname);
                fltnoo.Append(airlinecode + "-" + fltno);
                //acode.Append(arairportcity + " " + arairportcounty + "(" + arairportname + ")" + "-" + arrairportcode);
                acode.Append(arairportname + " [" + arrairportcode + "]");
                //atimedate.Append(arrtime + ", " + FlightDateFormat(arrdate, arrtime));
                atimedate.Append(FlightDateFormat(arrdate, arrtime));
                //deairportcode.Append(depairportcity + " " + depairportcounty + "(" + depairportname + ")" + "-" + depairportcode);
                deairportcode.Append(depairportname + " [" + depairportcode + "]");
                //dtimedate.Append(deptime + ", " + FlightDateFormat(depdate, deptime));
                dtimedate.Append(FlightDateFormat(depdate, deptime));



                // bse.Append(" <p><strong>" + basefare + "</strong></p>");
                t.Append(" <p>Total Taxes : <strong>&#8377; " + tax + "</strong></p>");
                ap.Append(" <p>Airline PNR : <strong>" + airlinepnr + "</strong></p>");
                // gp.Append(" <p><strong>" + gdspnr + "</strong></p>");
                // tno.Append(" <p>" + tcktno + "</p>");
                cm.Append(" <p>Total Commission : <strong>&#8377; " + commsn + "</strong></p>");
                nf.Append(" <p>Net Fare : <strong>&#8377; " + netfare + "</strong></p>");
                gf.Append(" <p>Gross Fare : <strong>&#8377; " + gfare + "</strong></p>");
                bcls.Append(" <p>Booking Class : <strong>" + bclass + "</strong></p>");
                rf.Append(" <p>Refundable : <strong>" + refndble + "</strong></p>");

                Srno.Controls.Add(new Literal { Text = srn.ToString() });
                PassengerName.Controls.Add(new Literal { Text = htmlTable.ToString() });
                Paxtype.Controls.Add(new Literal { Text = paxHtmlTable.ToString() });
                BaseFare.Controls.Add(new Literal { Text = bse.ToString() });
                TotalTax.Controls.Add(new Literal { Text = t.ToString() });
                AirLinePNR.Controls.Add(new Literal { Text = ap.ToString() });
                //GDSPNR.Controls.Add(new Literal { Text = gp.ToString() });
                TicketNo.Controls.Add(new Literal { Text = tno.ToString() });
                PaxStatus.Controls.Add(new Literal { Text = tStatus.ToString() });
                TotalComm.Controls.Add(new Literal { Text = cm.ToString() });
                NetFare.Controls.Add(new Literal { Text = nf.ToString() });
                GrossFare.Controls.Add(new Literal { Text = gf.ToString() });
                BookingClass.Controls.Add(new Literal { Text = bcls.ToString() });
                Refundable.Controls.Add(new Literal { Text = rf.ToString() });
                AirLineName.Controls.Add(new Literal { Text = fltpanel.ToString() });
                FltNo.Controls.Add(new Literal { Text = fltnoo.ToString() });
                ArrivalDetailsAirport.Controls.Add(new Literal { Text = acode.ToString() });
                ArrivalDateTime.Controls.Add(new Literal { Text = atimedate.ToString() });
                DepAirportCode.Controls.Add(new Literal { Text = deairportcode.ToString() });
                DepDateTime.Controls.Add(new Literal { Text = dtimedate.ToString() });

                Booking.Close();
                Booking.Dispose();
            }
            con.Close();
        }
        catch (Exception ex)
        {

            clsErrorLog.LogInfo(ex);
        }

    }

    public void PyamentProcess(string OrderId)
    {
        try
        {
            int i = 0;
            decimal cr = 0;
            decimal db = 0;
            decimal ab = 0;
            con.Open();
            String sp = "GetPayementProcess";

            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ordid", OrderId);
            SqlDataReader pp = cmd.ExecuteReader();
            if (pp.HasRows)
            {
                while (pp.Read())
                {
                    i++;
                    payment.Append("<tr>");
                    payment.Append("<td>" + i + "</td>");
                    payment.Append("<td>" + pp["CreatedDate"] + "</td>");
                    payment.Append("<td>" + pp["InvoiceNo"] + "</td>");

                    string bookingType = string.Empty;
                    if (pp["BookingType"].ToString().ToLower().Trim() == "domflt")
                    {
                        bookingType = "Domestic";
                    }
                    else if (pp["BookingType"].ToString().ToLower().Trim() == "intflt")
                    {
                        bookingType = "International";
                    }
                    else
                    {
                        bookingType = pp["BookingType"].ToString();
                    }
                    payment.Append("<td>" + bookingType + "</td>");
                    payment.Append("<td style='text-align: right;'>&#8377; " + pp["Credit"] + "</td>");
                    cr += (decimal)pp["Credit"];
                    payment.Append("<td style='text-align: right;'>&#8377; " + pp["Debit"] + "</td>");
                    db += (decimal)pp["Debit"];
                    payment.Append("<td style='text-align: right;'>&#8377; " + pp["Aval_Balance"] + "</td>");
                    ab = (decimal)pp["Aval_Balance"];

                    payment.Append("<td>" + pp["status"] + "</td>");

                    payment.Append("</tr>");
                }
                //payment.Append("<tr>");

                payment.Append("<td>" + "" + "</td>");
                payment.Append("<td>" + "" + "</td>");
                payment.Append("<td>" + "" + "</td>");
                payment.Append("<td>" + "<h5>Total</h5>" + "</td>");
                payment.Append("<td>" + "<h5 style='text-align: right;'>&#8377; " + cr + "</h5>" + "</td>");
                payment.Append("<td>" + "<h5 style='text-align: right;'>&#8377; " + db + "</h5>" + "</td>");
                payment.Append("<td>" + "<h5 style='text-align: right;'>&#8377; " + ab + "</h5>" + "</td>");
                //payment.Append("</tr>");
                payment.Append("</table>");
                PaymentProcess.Controls.Add(new Literal { Text = payment.ToString() });
                pp.Close();
                pp.Dispose();
            }
            con.Close();
        }
        catch (Exception ex)
        {

            clsErrorLog.LogInfo(ex);
        }
    }
    protected void SendEmail_Click(object sender, EventArgs e)
    {

    }

    //protected void SendSMS_Click(object sender, EventArgs e)
    //{
    //    var client = new RestClient("https://www.fast2sms.com/dev/bulkV2");
    //    var request = new RestRequest(Method.POST);
    //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
    //    request.AddHeader("authorization", "YOUR_API_KEY");
    //    request.AddParameter("sender_id", "DLT_SENDER_ID");
    //    request.AddParameter(message", "YOUR_MESSAGE_ID");
    //    request.AddParameter("variables_values", "12345|asdaswdx");
    //    request.AddParameter("route", "dlt");
    //    request.AddParameter("numbers", "9999999999,8888888888,7777777777");
    //    IRestResponse response = client.Execute(request);
    //}

    private string FlightDateFormat(string fltDate, string flttime)
    {
        if (!string.IsNullOrEmpty(fltDate))
        {
            if (fltDate.Length == 6 && flttime.Length == 5)
            {
                string fltDateSplit = fltDate.Substring(2, 2) + "/" + fltDate.Substring(0, 2) + "/20" + fltDate.Substring(4, 2) + " " + flttime;
                DateTime tempDate = new DateTime();
                tempDate = Convert.ToDateTime(fltDateSplit);
                return tempDate.ToString("dd MMMM yyyy, h:mm tt");
            }
            else if (fltDate.Length == 6 && flttime.Length == 4)
            {
                string fltDateSplit = fltDate.Substring(2, 2) + "/" + fltDate.Substring(0, 2) + "/20" + fltDate.Substring(4, 2) + " " + (flttime.Substring(0, 2) + ":" + flttime.Substring(2, 2));
                DateTime tempDate = new DateTime();
                tempDate = Convert.ToDateTime(fltDateSplit);
                return tempDate.ToString("dd MMMM yyyy, h:mm tt");
            }
        }

        return fltDate;
    }
}
