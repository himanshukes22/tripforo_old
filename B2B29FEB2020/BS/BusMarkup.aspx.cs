using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BS_BusMarkup : System.Web.UI.Page
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
                    BindData();
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogInfo(ex);
                }
                mk.Text = "0";
            }
            mk.Attributes.Add("onkeypress", "return phone_vali()");
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
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
                sqlcmd.CommandText = "SP_BUS_Markup";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "GET";
                sqlcmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["UID"].ToString();
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
                    int index = e.RowIndex;
                    GridViewRow row = (GridViewRow)GridView1.Rows[index];
                    Label lbl_userid= (Label)row.FindControl("lbl_userid");
                    TextBox lbl_MARKUP_VALUE = (TextBox)row.FindControl("txt_MARKUP_VALUE");
                    DropDownList lbl_MarkUpType = (DropDownList)row.FindControl("DropDownList1");

                    sqlcmd.CommandTimeout = 900;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "SP_BUS_Markup";
                    sqlcmd.Parameters.AddWithValue("@cmdtype", "UPDATE");
                    sqlcmd.Parameters.AddWithValue("@markupvalue", lbl_MARKUP_VALUE.Text);
                    sqlcmd.Parameters.AddWithValue("@markuptype", lbl_MarkUpType.SelectedValue);
                    sqlcmd.Parameters.AddWithValue("@userid", lbl_userid.Text);
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
                sqlcmd.CommandText = "SP_BUS_Markup";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "DELETE";
                sqlcmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = GridView1.Rows[e.RowIndex].Cells[2].Text.ToString();
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
                sqlcmd.CommandText = "SP_BUS_Markup";
                sqlcmd.Parameters.Add("@cmdtype", SqlDbType.VarChar).Value = "CheckEXISTS";
                sqlcmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["UID"].ToString();
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
                    sqlcmd1.CommandText = "SP_BUS_Markup"; ;
                    sqlcmd1.Parameters.Add("@cmdtype", "INSERT");
                    sqlcmd1.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["UID"].ToString();
                    sqlcmd1.Parameters.Add("@markuptype", SqlDbType.VarChar).Value = ddl_mktyp.SelectedValue;
                    sqlcmd1.Parameters.Add("@markupvalue", SqlDbType.Money).Value = mk.Text.Trim();
                    sqlcmd1.Connection = con;
                    sqlcmd1.ExecuteNonQuery();
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