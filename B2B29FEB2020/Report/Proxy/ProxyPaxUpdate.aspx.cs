using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Report_Proxy_ProxyPaxUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UID"] == null)
        {
            Response.Redirect("/Login.aspx");
        }
        string req = Request["status"];
        if((req != "Nothing") && (req == "open"))
        {
            CardView("pending");
        }
        else if((req != "Nothing") && (req == "quote"))
        {
            CardView("IN-PROCESS");
        }
        else if ((req != "Nothing") && (req == "quoteAccepted"))
        {
            CardView("QUOTED ACCEPTED");
        }
        else if ((req != "Nothing") && (req == "quoteRejected"))
        {
            CardView("QUOTED REJCECTED");
            
        }
        else if ((req != "Nothing") && (req == "namegiven"))
        {
            CardView("IN-PROCESS");
        }
        else if ((req != "Nothing") && (req == "cancelled"))
        {
            CardView("REQUEST REJCECTED");
            
        }
        else
        {
            BindData();
        }

        //if (!IsPostBack)
        //{
        //    BindData();
        //}
    }
    private void BindData()
    {
        string user_id = Session["UID"].ToString();
        try
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from ProxyTicket where AgentID='"+ user_id +"'", con);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Update_GV.DataSource = dt;
                Update_GV.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {

            clsErrorLog.LogInfo(ex);
        }
    }

    private void CardView(string status)
    {
        string user_id = Session["UID"].ToString();
        try
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from ProxyTicket where AgentID='" + user_id + "' AND Status='"+ status + "'", con);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Update_GV.DataSource = dt;
                Update_GV.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {

            clsErrorLog.LogInfo(ex);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {

        //string FromDate;
        //string ToDate;
        //if (string.IsNullOrEmpty(Request["From"]))
        //{
        //    FromDate = "";
        //}
        //else
        //{

        //    FromDate = string.Mid(Request["From"].Split(" ")(0), 4, 2) + "/" + String.Left(Request["From"].Split(" ")(0), 2) + "/" + String.Right(Request["From"].Split(" ")(0), 4);
        //    FromDate = FromDate + " " + "12:00:00 AM";
        //}

        //if (string.IsNullOrEmpty(Request["To"]))
        //{
        //    ToDate = "";
        //}
        //else
        //{
        //    ToDate = string.Mid(Request["To"].Split(" ")(0), 4, 2) + "/" + String.Left(Request["To"].Split(" ")(0), 2) + "/" + String.Right(Request["To"].Split(" ")(0), 4);
        //    ToDate = ToDate + " " + "11:59:59 PM";
        //}

        BindData();

    }
}