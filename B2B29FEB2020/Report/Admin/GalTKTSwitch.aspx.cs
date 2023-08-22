using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SprReports_Admin_GalTKTSwitch : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
    string Ddl_Value = "", UserID = "", IPAddress="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            Ddl_Value = ddl_gds_tktsearch.SelectedValue;
            BindGridView(Ddl_Value);
        }
    }
    protected void ddl_gds_tktsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Ddl_Value = ddl_gds_tktsearch.SelectedValue.ToString();
        BindGridView(Ddl_Value);
    }
    protected void BindGridView(string DDL_Val)
    {
        try
        {
            using (SqlCommand sqlcmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(constr);
                sqlcmd.Connection = con;
                con.Open();
                sqlcmd.CommandTimeout = 900;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "USP_GDS_TKTSEARCH";
                sqlcmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = 0;
                sqlcmd.Parameters.Add("@TKTSTATUS", SqlDbType.VarChar).Value = "";
                sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = DDL_Val.Trim().ToUpper();
                sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = "";
                sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SELECT";
                sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = "";
                sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV_GDS_TKT_Search.DataSource = ds;
                    GV_GDS_TKT_Search.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GV_GDS_TKT_Search.DataSource = ds;
                    GV_GDS_TKT_Search.DataBind();
                    int columncount = GV_GDS_TKT_Search.Rows[0].Cells.Count;
                    GV_GDS_TKT_Search.Rows[0].Cells.Clear();
                    GV_GDS_TKT_Search.Rows[0].Cells.Add(new TableCell());
                    GV_GDS_TKT_Search.Rows[0].Cells[0].ColumnSpan = columncount;
                    GV_GDS_TKT_Search.Rows[0].Cells[0].Text = "No Records Found";
                }
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }

    protected void GV_GDS_TKT_Search_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_GDS_TKT_Search.PageIndex = e.NewPageIndex;
        BindGridView(ddl_gds_tktsearch.SelectedValue);
    }

    protected void chk_Status_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            UserID = Session["UID"].ToString();
            IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            GridViewRow ChkBox = (GridViewRow)(((CheckBox)sender).NamingContainer);
            CheckBox Chk_STS = (CheckBox)ChkBox.FindControl("chk_Status");
            Label lbl_Counter = (Label)ChkBox.FindControl("lbl_counter");
            Label lbl_Airline = (Label)ChkBox.FindControl("lbl_Airline");
            Ddl_Value = ddl_gds_tktsearch.SelectedValue.Trim().ToString();
            using (SqlCommand sqlcmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(constr);
                sqlcmd.Connection = con;
                con.Open();
                sqlcmd.CommandTimeout = 900;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "USP_GDS_TKTSEARCH";
                sqlcmd.Parameters.Add("@COUNTER", SqlDbType.Int).Value = Convert.ToInt32(lbl_Counter.Text.ToString());
                sqlcmd.Parameters.Add("@TKTSTATUS", SqlDbType.VarChar).Value = Chk_STS.Checked.ToString();
                sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = Ddl_Value.Trim().ToUpper();
                sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = lbl_Airline.Text.Trim().ToUpper();
                sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "UPDATE";
                sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = UserID;
                sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress;
                sqlcmd.ExecuteNonQuery();
                con.Close();
                lbl_status.Text = "Ticketing Status Updated Successfully !!";
            }
            System.Threading.Thread.Sleep(500);
            BindGridView(Ddl_Value);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
}