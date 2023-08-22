using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BS_BusCommsion : System.Web.UI.Page
{
    public DataSet GDS = new DataSet();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
  

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            if (string.IsNullOrEmpty(Session["UID"].ToString()) | Session["UID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!Page.IsPostBack)
            {


                try
                {
                    string msg = "";
                    DropDownListType.DataSource = GroupTypeMGMT("", "", "MultipleSelect", ref msg);
                    DropDownListType.DataTextField = "GroupType";
                    DropDownListType.DataValueField = "GroupType";
                    DropDownListType.DataBind();

                    BindData();
    
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogInfo(ex);
                }

           

            }
           
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }


    public DataTable GroupTypeMGMT(string type, string desc, string cmdType, ref string msg)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_agentTypeMGMT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 200).Value = type;
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 500).Value = desc;
            cmd.Parameters.Add("@cmdType", SqlDbType.VarChar, 50).Value = cmdType;
            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 500);
            cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            msg = cmd.Parameters["@msg"].Value.ToString().Trim();
            con.Close();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
            con.Close();
        }
        return dt;
    }

    public string GetStatusVal(object val)
    {
        string value = "";
        string result = Convert.ToString(val).ToLower();

        if (result == "inactive" )
        {
            value = "Inactive";
        }
        else
        {
            value = "Active";
        }
        return value;
    }

    public DataSet BindData()
    {
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
                sqlcmd.CommandText = "SP_BUS_Commision";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "GET";               
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(GDS);
                GridView1.DataSource = GDS;
                GridView1.DataBind();
                con.Close();
                GDS.Dispose();
            }
        }
        catch (Exception ex)
        {

        }
        return GDS;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

        
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string status = (row.FindControl("ddlstatus") as DropDownList).SelectedItem.ToString();
           // decimal comm = Convert.ToDecimal(row.FindControl("txt_COMMISSION").ToString());
            string comm = (row.FindControl("txt_COMMISSION") as TextBox).Text.ToString();

            if (((LinkButton)GridView1.Rows[0].Cells[0].Controls[0]).Text == "Insert")
            {
            }
            else
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
                    sqlcmd.CommandText = "SP_BUS_Commision";
                    sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "UPDATE";
                    sqlcmd.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = GridView1.Rows[e.RowIndex].Cells[2].Text.ToString();
                   // sqlcmd.Parameters.Add("@Commision", SqlDbType.Decimal).Value = (((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());              
                    //sqlcmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = (((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
                    sqlcmd.Parameters.AddWithValue("@Commision", comm);   
                    sqlcmd.Parameters.AddWithValue("@Status", status);       
                    sqlcmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.EditIndex = -1;
                    BindData();
                }
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
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
                sqlcmd.CommandText = "SP_BUS_Commision";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "DELETE";
                sqlcmd.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = GridView1.Rows[e.RowIndex].Cells[2].Text.ToString();
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                GDS.Dispose();
                BindData();
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
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
                sqlcmd.CommandText = "SP_BUS_Commision";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "CheckEXISTS";
                sqlcmd.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = DropDownListType.SelectedValue;                      
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lbl.Text = "MarkUp Is Already Exist For This User.";
                }
                else
                {
                    SqlCommand sqlcmd1 = new SqlCommand();
                    sqlcmd1.CommandTimeout = 900;
                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                    sqlcmd1.CommandText = "SP_BUS_Commision";
                    sqlcmd1.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "INSERT";
                    sqlcmd1.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = DropDownListType.SelectedValue;
                    sqlcmd1.Parameters.Add("@Commision", SqlDbType.Decimal).Value = Convert.ToDecimal(txtCommision.Text);
                    sqlcmd1.Parameters.Add("@ComType", SqlDbType.VarChar).Value = ddl_commisionType.SelectedValue;
                    sqlcmd1.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddlstatus.SelectedValue;
                    sqlcmd1.Parameters.Add("@ProviderName", SqlDbType.VarChar).Value = ddlprovidername.SelectedValue;           
                    sqlcmd1.Connection = con;
                    int i= sqlcmd1.ExecuteNonQuery();
                    lbl.Text = "";
                }
                con.Close();
                GDS.Dispose();
                 BindData();
 
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }
    }

}