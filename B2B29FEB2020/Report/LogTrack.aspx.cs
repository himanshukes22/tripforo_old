using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SprReports_LogTrack : System.Web.UI.Page
{
   
        string constr = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
        string OrderID = "", PNRNo = "";


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_logtrack_Click(object sender, EventArgs e)
    {
        OrderID = txt_orderid.Text.Trim().ToString();
        PNRNo = txt_pnrno.Text.Trim().ToString();
        if (OrderID == "" || PNRNo == "")
        {
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection con = new SqlConnection(constr);
            sqlcmd.Connection = con;
            con.Open();
            sqlcmd.CommandTimeout = 900;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "USP_LOGTRACKER";
            sqlcmd.Parameters.Add("@PNRNO", SqlDbType.VarChar).Value = PNRNo;
            sqlcmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = OrderID;
            sqlcmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "RETRIVE";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GetXML_Details.Visible = true;
                Panel1.Visible = true;
                Panel2.Visible = false;
                DataList1.DataSource = ds.Tables[0];
                DataList1.DataBind();

            }
            else if (ds.Tables[1].Rows.Count > 0)
            {
                //JsonDiv.Visible = true;
                Panel2.Visible = true;
                Panel1.Visible = false;
                DataList2.DataSource = ds.Tables[1];
                DataList2.DataBind();
            }
        }
        else
        {
            lbl_error.Text = "Please fill the right value";
        }
    }
    public string GetXML(string GetXML_Details, string val)
    {
        val = val.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Trim().Replace("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">", "").Trim().Replace("<soapenv:Body><SubmitXmlOnSessionResponse xmlns=\"http://webservices.galileo.com\">", "").Trim().Replace("</SubmitXmlOnSessionResponse>", "").Trim().Replace("</soapenv:Body>", "").Trim().Replace("</soapenv:Envelope>", "").Trim().Replace("xmlns=\"\"", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\" ,\"" + val + "\")";
    }
    public string SSRes(string val)
    {
        val = val.Replace("<?xml version=1.0 encoding=utf-8?>", "").Trim().Replace("<soap:Envelope xmlns:soap=http://schemas.xmlsoap.org/soap/envelope/ xmlns:xsi=http://www.w3.org/2001/XMLSchema-instance xmlns:xsd=http://www.w3.org/2001/XMLSchema>", "").Trim().Replace("<soap:Body>", "").Trim().Replace("<BeginSession xmlns=http://webservices.galileo.com>", "<BeginSession>").Trim().Replace("</soap:Body>", "").Trim().Replace("</soap:Envelope>", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\" ,\"" + val + "\")";
    }
    public string SSReq(string val)
    {
        val = val.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Trim().Replace("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">", "").Trim().Replace("<soapenv:Body>", "").Trim().Replace("<BeginSessionResponse xmlns=\"http://webservices.galileo.com\">", "").Trim().Replace("</BeginSessionResponse>", "").Trim().Replace("</soapenv:Body>", "").Trim().Replace("</soapenv:Envelope>", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\" ,\"" + val + "\")";
    }

    public string DOCPRDResponse(string val)
    {
        val = val.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Trim().Replace("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">", "").Trim().Replace("<soapenv:Body>", "").Trim().Replace("<SubmitXmlOnSessionResponse xmlns=\"http://webservices.galileo.com\">", "").Trim().Replace("</SubmitXmlOnSessionResponse>", "").Trim().Replace("xmlns=\"\"", "").Trim().Replace("</soapenv:Body>", "").Trim().Replace("</soapenv:Envelope>", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\" ,\"" + val + "\")";
    }
    public string SERes(string val)
    {
        val = val.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\" ,\"" + val + "\")";
    }
    public string PNBFRequest1(string GetXML_Details, string val)
    {
        val = val.Replace("<?xml version=1.0 encoding=utf-8?>", "").Trim().Replace("<soap:Envelope xmlns:soap=http://schemas.xmlsoap.org/soap/envelope/ xmlns:xsi=http://www.w3.org/2001/XMLSchema-instance xmlns:xsd=http://www.w3.org/2001/XMLSchema><soap:Body><SubmitXmlOnSession xmlns=http://webservices.galileo.com>", "<SubmitXmlOnSession>").Trim().Replace("xmlns=", "").Trim().Replace("</soap:Body>", "").Trim().Replace("</soap:Envelope>", "").Trim().Replace("_ xmlns= ", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\",\"" + val + "\")";
    }
    public string PNRRTReq(string val)
    {
        val = val.Replace("<?xml version='1.0' encoding='utf-8'?>", "").Trim().Replace("<soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>", "").Trim().Replace("<soap:Body>", "").Trim().Replace("<SubmitXmlOnSession xmlns='http://webservices.galileo.com'>", "<SubmitXmlOnSession>").Trim().Replace("<_ xmlns='' />", "").Replace("</soap:Body>", "").Trim().Replace("</soap:Envelope>", "").Trim().Replace("<LDVOverride />", "").Trim();
        return "return GetXMLDetails(\"GetXML_Details\",\"" + val + "\")";
    }
    public string ChangeVal(string val)
    {
        return "return getjson('" + val + "')";
    }

}