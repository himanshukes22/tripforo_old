using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
public partial class Report_Proxy_DashboardForOffline : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UID"] == null)
        {
            Response.Redirect("/Login.aspx");
        }


        BindCount();
    }
    protected void BindCount()
    {
        try
        {
            string user_id = Session["UID"].ToString();

            //if (Session["User_Type"].ToString() == "SALES")
            //{
            //    Response.Redirect("SprReports/Admin/Agent_Details.aspx", false);
            //}
            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("usp_Get_Proxy_Satus_wise_Count");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", "2016-06-15 15:15:23.210");
            cmd.Parameters.AddWithValue("@endDate", "2016-06-15 15:15:23.210");
            cmd.Parameters.AddWithValue("@UserID", user_id);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;

            DataTable dt = new DataTable();
            sda.Fill(ds);
            dt = ds.Tables[0];

            open.Text = Convert.ToString(dt.Rows[0]["OpenCount"].ToString());
            quote.Text = Convert.ToString(ds.Tables[0].Rows[0]["Quote"].ToString());
            quoteAccepted.Text = Convert.ToString(dt.Rows[0]["QuoteAccepted"].ToString());
            quoteRejected.Text = Convert.ToString(dt.Rows[0]["QuoteRejected"].ToString());
           // lbltktreq.Text = Convert.ToString(dt.Rows[0]["TKTREQ"].ToString());
            NameGiven.Text = Convert.ToString(dt.Rows[0]["NameGiven"]);
            RquestCancelled.Text = Convert.ToString(dt.Rows[0]["Cancelled"]);

          
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

    }
}