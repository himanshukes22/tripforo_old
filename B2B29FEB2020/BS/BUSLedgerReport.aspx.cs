
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class BS_BUSLedgerReport : System.Web.UI.Page
{
    private SqlTransaction ST = new SqlTransaction();
    private SqlTransactionDom STDom = new SqlTransactionDom();
    private clsInsertSelectedFlight CllInsSelectFlt = new clsInsertSelectedFlight();
    DataSet AgencyDDLDS = new DataSet();
    DataSet grdds = new DataSet();
    DataSet fltds = new DataSet();
    private Status sttusobj = new Status();
    SqlConnection con = new SqlConnection();
    ClsCorporate clsCorp = new ClsCorporate();
    public void CheckEmptyValue()
    {
        try
        {
            string FromDate = null;
            string ToDate = null;
            int lenth = 0;
            // string PgStatus = drpPaymentStatus.Visible == true ? drpPaymentStatus.SelectedValue.ToLower() != "select" ? drpPaymentStatus.SelectedValue : null : null;

            if (String.IsNullOrEmpty(Request["From"]))
            {
                FromDate = "";
            }
            else
            {

                FromDate = Request["From"].Substring(3, 2) + "/" + Request["From"].Substring(0, 2) + "/" + Request["From"].Substring(6, 4);
                FromDate = FromDate + " " + "12:00:00 AM";
            }
            if (String.IsNullOrEmpty(Request["To"]))
            {
                ToDate = "";
            }
            else
            {

                ToDate = Request["To"].Substring(3, 2) + "/" + Request["To"].Substring(0, 2) + "/" + Request["To"].Substring(6, 4);
                ToDate = ToDate + " " + "11:59:59 PM";
            }


            string AgentID = String.IsNullOrEmpty(Request["txtAgencyName"]) ? "" : Request["txtAgencyName"];
            string OrderID = String.IsNullOrEmpty(txtOrderID.Text) ? "" : txtOrderID.Text.Trim();
            if (AgentID != "")
            {
                string str = AgentID;
                int pos1 = str.IndexOf("(");
                int pos2 = str.IndexOf(")");
                lenth = pos2 - pos1;
                string AgentID1 = str.Substring(pos1 + 1, lenth - 1);
                grdds.Clear();
                grdds = BUSDetails(Session["UID"].ToString(), Session["User_Type"].ToString(), FromDate, ToDate, OrderID, AgentID1, "");
                ViewState["grdds"] = grdds;
                GrdBusReport.DataSource = grdds;
                GrdBusReport.DataBind();
            }
            else
            {
                grdds.Clear();
                grdds = BUSDetails(Session["UID"].ToString(), Session["User_Type"].ToString(), FromDate, ToDate, OrderID, AgentID, "");
                ViewState["grdds"] = grdds;
                GrdBusReport.DataSource = grdds;
                GrdBusReport.DataBind();

            }


        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    //protected void btn_result_Click(object sender, System.EventArgs e)
    //{
    //    CheckEmptyValue();

    //}
    protected void Page_Load(object sender, System.EventArgs e)
    {

        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (string.IsNullOrEmpty(Session["UID"].ToString()) | Session["UID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    public DataSet BUSDetails(string loginid, string usertype, string fromdate, string todate, string orderid, string agentid, string paymentStatus)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        DataSet DS = new DataSet();
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
                sqlcmd.CommandText = "SP_BUSLedgerReport";
                sqlcmd.Parameters.Add("@usertype", SqlDbType.VarChar).Value = usertype;
                sqlcmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = loginid;
                sqlcmd.Parameters.Add("@FormDate", SqlDbType.VarChar).Value = fromdate;
                sqlcmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = todate;
                sqlcmd.Parameters.Add("@OrderID", SqlDbType.VarChar).Value = orderid;
                sqlcmd.Parameters.Add("@AgentId", SqlDbType.VarChar).Value = agentid;
                sqlcmd.Parameters.Add("@PaymentStatus", SqlDbType.VarChar).Value = paymentStatus;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(DS);
                con.Close();
                DS.Dispose();
                con.Close();
            }
        }
        catch (Exception ex)
        {
        }
        return DS;
    }
    protected void btn_export_Click(object sender, System.EventArgs e)
    {
        try
        {
            string FromDate = null;
            string ToDate = null;
            int lenth = 0;
            // string PgStatus = drpPaymentStatus.Visible == true ? drpPaymentStatus.SelectedValue.ToLower() != "select" ? drpPaymentStatus.SelectedValue : null : null;

            if (String.IsNullOrEmpty(Request["From"]))
            {
                FromDate = "";
            }
            else
            {

                FromDate = Request["From"].Substring(3, 2) + "/" + Request["From"].Substring(0, 2) + "/" + Request["From"].Substring(6, 4);
                FromDate = FromDate + " " + "12:00:00 AM";
            }
            if (String.IsNullOrEmpty(Request["To"]))
            {
                ToDate = "";
            }
            else
            {

                ToDate = Request["To"].Substring(3, 2) + "/" + Request["To"].Substring(0, 2) + "/" + Request["To"].Substring(6, 4);
                ToDate = ToDate + " " + "11:59:59 PM";
            }


            string AgentID = String.IsNullOrEmpty(Request["txtAgencyName"]) ? "" : Request["txtAgencyName"];
            string OrderID = String.IsNullOrEmpty(txtOrderID.Text) ? "" : txtOrderID.Text.Trim();
            if (AgentID != "")
            {
                string str = AgentID;
                int pos1 = str.IndexOf("(");
                int pos2 = str.IndexOf(")");
                lenth = pos2 - pos1;
                string AgentID1 = str.Substring(pos1 + 1, lenth - 1);
                grdds.Clear();
                grdds = BUSDetails(Session["UID"].ToString(), Session["User_Type"].ToString(), FromDate, ToDate, OrderID, AgentID1, "");

                STDom.ExportData(grdds);
            }
            else
            {
                grdds.Clear();
                grdds = BUSDetails(Session["UID"].ToString(), Session["User_Type"].ToString(), FromDate, ToDate, OrderID, AgentID, "");
                //grdds.Tables[0].Columns.Remove(grdds.Tables[0].Columns["RequestJson"]);
                //grdds.Tables[0].Columns.Remove(grdds.Tables[0].Columns["ResponseJson"]);
                //grdds.Tables[0].Columns.Remove(grdds.Tables[0].Columns["EncRequest"]);
                //grdds.Tables[0].Columns.Remove(grdds.Tables[0].Columns["EncResponse"]);
                //grdds.Tables[0].Columns.Remove(grdds.Tables[0].Columns["ErrorCode"]);

                STDom.ExportData(grdds);

            }


        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

    }

    protected void btn_result_Click(object sender, EventArgs e)
    {
        CheckEmptyValue();
    }
}