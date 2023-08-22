using System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services;

public partial class BS_TicketCopy : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [WebMethod]
    public static void MailSend(string Ticket, string OrderID)
    {
        HttpContext.Current.Response.Write("OrderID_ch");
        DataSet AgentDS = new DataSet();
        AgentDS = AgentIDEMAILINFO(OrderID);
        if (AgentDS.Tables[0].Rows.Count > 0)
        {
            string BookingStatus = "";
            BookingStatus = Convert.ToString(AgentDS.Tables[0].Rows[0]["BOOKINGSTATUS"]);
            if (BookingStatus.ToLower() == "booked")
            {
                PDFEmailSending(AgentDS, OrderID, Ticket);
            }
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
    public static void PDFEmailSending(DataSet AInfo, string OrderID, string Ticket)
    {
      
        string strMailMsg = "", strFileNmPdf = "", s = "";
        string TicketFormate = null;
        StringWriter sw = new StringWriter();
        //HtmlTextWriter w = new HtmlTextWriter(sw);
        //divticketcopy.RenderControl(w);
        string a = HttpUtility.UrlDecode(Ticket, System.Text.Encoding.Default);
        if ((!string.IsNullOrEmpty(a)))
        {
            s = a.Trim();
            s = s.Replace("<img width=\"198\" height=\"45\" src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\">", "<img src=\"../images/logo.png\" alt=\"Logo\" style=\"height:54px; width:104px\"/>").Replace("<br>", "<br/>").Replace("<img src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\" width=\"198\" height=\"45\">", "<img src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\" width=\"198\" height=\"45\" />");
            s = s.Replace("</table></table>", "</table>");
        }
        else
        {
            s = sw.GetStringBuilder().ToString();
        }
        try
        {
            TicketFormate = s.Trim().ToString();
            strFileNmPdf = ConfigurationManager.AppSettings["HTMLtoPDF"].ToString().Trim() + OrderID + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf";
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None));
            pdfDoc.Open();
            StringReader sr = new StringReader(TicketFormate);
            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            writer.Dispose();
            sr.Dispose();
            pdfDoc.Dispose();
            strMailMsg = "<p style='font-family:verdana; font-size:12px'>Dear Customer<br /><br />";
            strMailMsg = strMailMsg + "Greetings of the day !!!!<br /><br />";
            strMailMsg = strMailMsg + "Please find an attachment for your E-ticket, kindly carry the print out of the same for hassle-free travel. Your onward booking for is confirmed on for .<br /><br />";
            strMailMsg = strMailMsg + "Have a nice &amp; wonderful trip.<br /><br />";
        }
        catch (Exception ex)
        {
            string stt = ex.Message;
        }
        SqlTransactionDom STDOM = new SqlTransactionDom();
        DataTable MailDt = new DataTable();
        MailDt = STDOM.GetMailingDetails("BUS_BOOKING", Convert.ToString(AInfo.Tables[0].Rows[0]["AgentId"])).Tables[0];
        string ToEmail = Convert.ToString(AInfo.Tables[0].Rows[0]["Email"]);
        string FromEmail = Convert.ToString(AInfo.Tables[0].Rows[0]["AgentEmail"]);
        if ((MailDt.Rows.Count > 0))
        {
            bool Status = false;
            Status = Convert.ToBoolean(MailDt.Rows[0]["Status"].ToString());
            try
            {
                if (Status == true)
                {
                    //for booking SMS
                    SMSAPI.SMS BusSms = new SMSAPI.SMS();
                    string SeatNo = "", PnrNo = "", MobNo = "", Sector = "", JourneyDate = "";
                    if (AInfo.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < AInfo.Tables[0].Rows.Count; j++)
                        {
                            SeatNo += Convert.ToString(AInfo.Tables[0].Rows[j]["SEATNO"]) + ";";
                        }
                        SeatNo = SeatNo.Substring(0, SeatNo.Length - 1);
                        PnrNo = Convert.ToString(AInfo.Tables[0].Rows[0]["TICKETNO"]);
                        MobNo = Convert.ToString(AInfo.Tables[0].Rows[0]["PRIMARY_PAX_PAXMOB"]);
                        Sector = Convert.ToString(AInfo.Tables[0].Rows[0]["Sector"]);
                        JourneyDate = Convert.ToString(AInfo.Tables[0].Rows[0]["JOURNEYDATE"]);
                        try
                        {
                            string Smsstatus = "";
                            
                            DataTable SmsCrd = new DataTable();
                            SqlTransaction objDA = new SqlTransaction();
                            SmsCrd = objDA.SmsCredential(SMS.BUSBOOKING.ToString()).Tables[0];
                            if (SmsCrd.Rows.Count > 0 && Convert.ToBoolean(SmsCrd.Rows[0]["Status"]) == true)
                            {
                                Smsstatus = BusSms.SendBusSms(OrderID, PnrNo, MobNo, Sector, JourneyDate, SeatNo, "BusBooking", "Book", SmsCrd);
                                SqlTransactionNew obj = new SqlTransactionNew();
                                obj.SmsLogDetails(OrderID, MobNo, "", Smsstatus);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    int i = STDOM.SendMail(ToEmail, FromEmail, MailDt.Rows[0]["BCC"].ToString(), MailDt.Rows[0]["CC"].ToString(), MailDt.Rows[0]["SMTPCLIENT"].ToString(), MailDt.Rows[0]["UserId"].ToString(), MailDt.Rows[0]["Pass"].ToString(), strMailMsg, MailDt.Rows[0]["SUBJECT"].ToString(), strFileNmPdf);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
