using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class BS_BusAdminMarkup : System.Web.UI.Page
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
                sqlcmd.CommandText = "SP_BUS_ADMINMARKUP";
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
    public string GetStatusVal(object val)
    {
        string value = "";
        int result = Convert.ToInt16(val);

        if (result == 0)
        {
            value = "Inactive";
        }
        else
        {
            value = "Active";
        }
        return value;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
             GridViewRow row = GridView1.Rows[e.RowIndex];
             string status = (row.FindControl("ddlstatus") as DropDownList).SelectedItem.ToString();
             Label lbl_Agent_Type = (Label)row.FindControl("lbl_AGENT_TYPE");
             TextBox lbl_MARKUP_VALUE = (TextBox)row.FindControl("txt_MARKUP_VALUE");
             DropDownList lbl_MarkUpType = (DropDownList)row.FindControl("DropDownList1");
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
                    sqlcmd.CommandText = "SP_BUS_ADMINMARKUP";
                    //sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "UPDATE";
                    //sqlcmd.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = GridView1.Rows[e.RowIndex].Cells[2].Text.ToString();
                    //sqlcmd.Parameters.Add("@markupvalue", SqlDbType.Decimal).Value = (((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
                    //sqlcmd.Parameters.Add("@markuptype", SqlDbType.VarChar).Value = (((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString().ToUpper());
                    sqlcmd.Parameters.AddWithValue("@cmdtype", "UPDATE");
                    sqlcmd.Parameters.AddWithValue("@markupvalue", lbl_MARKUP_VALUE.Text);
                    sqlcmd.Parameters.AddWithValue("@markuptype", lbl_MarkUpType.SelectedValue);
                    sqlcmd.Parameters.AddWithValue("@AgentType", lbl_Agent_Type.Text);
                    sqlcmd.Parameters.AddWithValue("@markupon", status.ToUpper() == "ACTIVE" ? 1 : 0);                  
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
            GridViewRow row = GridView1.Rows[e.RowIndex];      
            Label lbl_Agent_Type = (Label)row.FindControl("lbl_AGENT_TYPE");

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
                sqlcmd.CommandText = "SP_BUS_ADMINMARKUP";             
                sqlcmd.Parameters.AddWithValue("@cmdtype", "DELETE");
                sqlcmd.Parameters.AddWithValue("@AgentType", lbl_Agent_Type.Text);
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
                sqlcmd.CommandText = "SP_BUS_ADMINMARKUP";
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
                    sqlcmd1.CommandText = "SP_BUS_ADMINMARKUP";
                    sqlcmd1.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "INSERT";
                    sqlcmd1.Parameters.Add("@AgentType", SqlDbType.VarChar).Value = DropDownListType.SelectedValue;
                    sqlcmd1.Parameters.Add("@markuptype", SqlDbType.VarChar).Value = ddl_mktyp.SelectedValue;
                    sqlcmd1.Parameters.Add("@markupvalue", SqlDbType.Decimal).Value = mkv.Text.ToString();
                    sqlcmd1.Parameters.Add("@markupon", SqlDbType.Int).Value = ddl_status1.SelectedValue;
                    sqlcmd1.Connection = con;
                    int i = sqlcmd1.ExecuteNonQuery();
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