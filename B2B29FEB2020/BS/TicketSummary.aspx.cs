using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using HtmlAgilityPack;
using System.Linq;
using iTextSharp.text.html.simpleparser;
public partial class BS_TiketSummary : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection();
    private SqlConnection con1 = new SqlConnection();
    private SqlDataAdapter adp;
    private SqlDataAdapter adap;
    private DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    public DataTable AgentIDInfo(string AgentID)
    {

        SqlConnection con = default(SqlConnection);
        SqlDataAdapter adap = default(SqlDataAdapter);
        // Dim ds As DataSet
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        DataTable dt = new DataTable();
        adap = new SqlDataAdapter("select * from agent_register where user_id='" + AgentID + "'", con);
        adap.Fill(dt);
        return dt;
    }

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik
    //Facebook: facebook.com/telerik
    //=======================================================

    protected void btn_export_Click(object sender, EventArgs e)
    {
        try
        {
            string sword = "";
            string filename = "";
            filename = "BusTicketReport.doc";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
            Response.Charset = "";
            Response.ContentType = "application/doc";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divticketcopy.RenderControl(htmlWrite);
            //if ((!string.IsNullOrEmpty(Request("Hidden1"))))
            //{
                sword =Request["Hidden1"];
            //}
            //else
            //{
            //    sword = stringWrite.GetStringBuilder().ToString();
            //}

            Response.Write(sword);
            Response.End();
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["myCon"].ConnectionString;
            DataTable dt = new DataTable();
            adap = new SqlDataAdapter("select  AgentId from dbo.TBL_RB_SEATBOOKINGDETAILS  where OrderId ='" + Request.QueryString["oid"].ToString() + "'", con1);
            adap.Fill(dt);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }






            con.ConnectionString = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
            //DataTable dtag = new DataTable();
            //adap = new SqlDataAdapter("SELECT TOP (1) Agency_Name, Address, (City +', '+ zipcode +', '+ State) as Add1,Country,Email FROM  New_Regs where User_Id='" + dt.Rows[0]["AgentId"].ToString() + "'  ", con);
            //adap.Fill(dtag);
        
            string email_id ="";



            try
            {
              
               string agent_id = dt.Rows[0]["AgentId"].ToString();
               
                
                DataTable dtemail = new DataTable();
                dtemail = AgentIDInfo(agent_id);
                email_id = dtemail.Rows[0]["Email"].ToString();
            }
            catch (Exception ex)
            {
                email_id = "info@RWT.com";
            }


            string strMailMsg = "";
            string strFileNmPdf = "";
            try
            {
                string s = "";
                //Dim str As String = 
                StringWriter sw = new StringWriter();
                HtmlTextWriter w = new HtmlTextWriter(sw);
                divticketcopy.RenderControl(w);




                if ((!string.IsNullOrEmpty(Request["Hidden1"])))
                {
                    s = Request["Hidden1"].Trim();
                    s = s.Replace("<img width=\"198\" height=\"45\" src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\">", "<img src=\"http://www.RWT.co/images/logo.png\" alt=\"Logo\" style=\"height:54px; width:104px\"/>").Replace("<br>", "<br/>").Replace("<img src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\" width=\"198\" height=\"45\">", "<img src=\"http://www.RWT.co/BS/images/seatseller_logo.jpg\" width=\"198\" height=\"45\" />");//.Replace("<img alt=\"Logo Not Found\" src=\"http://www.RWT.co/AirLogo/sm" + FltHeaderList.Rows(0)("VC") + ".gif\">", "<img alt=\"Logo Not Found\" src=\"http://www.RWT.co/AirLogo/sm" + FltHeaderList.Rows(0)("VC") + ".gif\"/>").Replace("<span id=\"LabelTkt\">", "").Replace("<title>ticket details</title>", "").Replace("</span>", "").Replace("<br>", "<br/>");
                }
                else
                {
                    s = sw.GetStringBuilder().ToString();
                }
                ///' Ticketcopy Convert into PDF 
                
                try
                {
                    string Body = "";
                    
                    string TicketFormate = null;
                   
                    bool writePDF = false;
                    int status1 = 0;
                    try
                    {
                        TicketFormate = s.Trim().ToString();
                        strFileNmPdf = ConfigurationManager.AppSettings["HTMLtoPDF"].ToString().Trim() + Request.QueryString["oid"].ToString() + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf";
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFileNmPdf, FileMode.Create, FileAccess.ReadWrite, FileShare.None));
                        pdfDoc.Open();
                        StringReader sr = new StringReader(TicketFormate);
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        writer.Dispose();
                        sr.Dispose();
                        pdfDoc.Dispose();
                        writePDF = true;


                    }
                    catch (Exception ex)
                    {
                        string stt = ex.Message;
                    }
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
                MailDt = STDOM.GetMailingDetails("BUS_BOOKING", Session["UID"].ToString()).Tables[0];

                string email = Request["txt_email"];

                if ((MailDt.Rows.Count > 0))
                {
                    bool Status = false;
                    Status = Convert.ToBoolean(MailDt.Rows[0]["Status"].ToString());
                    try
                    {
                        if (Status == true)
                        {
                            int i = STDOM.SendMail(email, email_id, email_id, MailDt.Rows[0]["CC"].ToString(), MailDt.Rows[0]["SMTPCLIENT"].ToString(), MailDt.Rows[0]["UserId"].ToString(), MailDt.Rows[0]["Pass"].ToString(), strMailMsg, MailDt.Rows[0]["SUBJECT"].ToString(), strFileNmPdf);
                            if (i == 1)
                            {
                                ShowAlertMessage("Mail sent successfully.");
                                //mailmsg.Text = "Mail sent successfully."
                            }
                            else
                            {
                                ShowAlertMessage("Unable to send mail.Please try again");
                                //mailmsg.Text = "Unable to send mail.Please try again"
                            }
                        }


                        txt_email.Text = "";
                       // ddl_srvtype.SelectedIndex = 0;

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                       // LogInfo(ex);
                        mailmsg.Text = ex.Message.ToString();
                    }
                }
                else
                {
                    mailmsg.Text = "Unable to send mail.Please contact to administrator";
                }

              


                //if ((!string.IsNullOrEmpty(Request("Hidden1"))))
                //{
                    s = Request["Hidden1"].ToString();
                //}
                //else
                //{
                //    s = sw.GetStringBuilder().ToString();
                //}

                //Dim f As String = lbl_Summary.Text
                ////string email = Request["txt_email"].ToString();
                ////System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
                ////System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
                ////msgMail.To.Clear();
                ////msgMail.To.Add(new System.Net.Mail.MailAddress(email));
                ////msgMail.Bcc.Add(new System.Net.Mail.MailAddress(email_id));
                ////msgMail.From = new System.Net.Mail.MailAddress(email_id);
                ////msgMail.Subject = "Bus Ticket Copy";
                ////msgMail.IsBodyHtml = true;
                ////msgMail.Body = s;

                ////try
                ////{
                ////    objMail.Credentials = new System.Net.NetworkCredential("b2bticketing", "america");
                ////    objMail.Host = "shekhal.springtravels.com";
                ////    objMail.Send(msgMail);
                ////    mailmsg.Text = "Mail sent successfully.";
                ////    txt_email.Text = "";
               
                ////}
                ////catch (Exception ex)
                ////{
                ////    clsErrorLog.LogInfo(ex);
                ////    mailmsg.Text = ex.Message.ToString();
                ////}
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }

        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }
    }


    public static void ShowAlertMessage(string error)
    {

        try
        {

            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                error = error.Replace("'", "'");
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }

    }

   
    //public DataTable SelectUID(string PNR)
    //{
    //    if (con1.State == ConnectionState.Open)
    //    {
    //        con1.Close();
    //    }
    //    con1.ConnectionString = ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString;
    //    DataTable dt = new DataTable();
    //    adap = new SqlDataAdapter("select distinct Account_no from dbo.TRAIN_BOOKED_DETAILS  where PNR ='" + PNR + "'", con1);
    //    adap.Fill(dt);
    //    return dt;


    //}
    //public DataSet SelectAgencyDetail(string UID)
    //{
    //    if (con.State == ConnectionState.Open)
    //    {
    //        con.Close();
    //    }
    //    con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString;
    //    DataSet ds = new DataSet();
    //    adap = new SqlDataAdapter("SELECT TOP (1) Agency_Name, Address, (City +', '+ zipcode +', '+ State) as Add1,Country FROM  New_Regs where User_Id='" + UID + "'  ", con);
    //    adap.Fill(ds);
    //    return ds;


    //}
}
