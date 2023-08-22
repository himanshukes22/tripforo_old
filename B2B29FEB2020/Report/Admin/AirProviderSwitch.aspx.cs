
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class SprReports_Admin_AirProviderSwitch : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
        string Ddl_Value = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack == true)
            {
                string DDL_Val = ddl_triptype.SelectedValue.ToString();
                BindGridView(DDL_Val);
            }
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
                    sqlcmd.CommandText = "USP_AIRLINE_SERVICE_SWITCH";
                    sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@PROVIDER", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@RTF", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = DDL_Val;
                    sqlcmd.Parameters.Add("@CounterID", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "SELECT";
                    sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = "";
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                    else
                    {
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                        int columncount = GridView1.Rows[0].Cells.Count;
                        GridView1.Rows[0].Cells.Clear();
                        GridView1.Rows[0].Cells.Add(new TableCell());
                        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                        GridView1.Rows[0].Cells[0].Text = "No Records Found";
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(ddl_triptype.SelectedValue);
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView(ddl_triptype.SelectedValue.ToString());
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView(ddl_triptype.SelectedValue.ToString());
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                string aa = GridView1.DataKeys[e.RowIndex].Value.ToString();
                GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                bool BolRTF = (row.Cells[0].Controls[0] as CheckBox).Checked;
                bool BOlStatus = (row.Cells[2].Controls[0] as CheckBox).Checked;
                GridView1.EditIndex = -1;
                BindGridView(ddl_triptype.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    RadioButtonList Rbl_provdr = (RadioButtonList)e.Row.FindControl("rbl_Provider");
                    Label airlinetype = (Label)e.Row.FindControl("lbl_ProviderType");
                    if (airlinetype.Text.Trim().ToUpper() == "LCC")
                    {
                        Rbl_provdr.DataSource = GetData().Select("lcc=1").CopyToDataTable();
                    }
                    if (airlinetype.Text.Trim().ToUpper() == "GDS")
                    {
                        Rbl_provdr.DataSource = GetData().Select("gds=1").CopyToDataTable();
                    }
                    Rbl_provdr.DataTextField = "ProviderName";
                    Rbl_provdr.DataBind();
                    if (Rbl_provdr.Items.FindByText((e.Row.FindControl("lbl_Provider") as Label).Text) != null)
                    {
                        Rbl_provdr.Items.FindByText((e.Row.FindControl("lbl_Provider") as Label).Text).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        private DataTable GetData()
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
                    sqlcmd.CommandText = "USP_GetData_AirProviderSwitch";
                    sqlcmd.Parameters.Add("@cmd_type", SqlDbType.VarChar).Value = "RETRIVE";
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    con.Close();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddl_triptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ddl_Value = ddl_triptype.SelectedValue.ToString();
            BindGridView(Ddl_Value);
        }
        protected void chk_RTF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string  UserID = Session["UID"].ToString();
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                GridViewRow ChkBox = (GridViewRow)(((CheckBox)sender).NamingContainer);
                CheckBox chk_RTF = (CheckBox)ChkBox.FindControl("chk_RTF");
                Label lbl_Counter = (Label)ChkBox.FindControl("lbl_counter");
                Ddl_Value = ddl_triptype.SelectedValue.Trim().ToString();
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    SqlConnection con = new SqlConnection(constr);
                    sqlcmd.Connection = con;
                    con.Open();
                    sqlcmd.CommandTimeout = 900;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "USP_AIRLINE_SERVICE_SWITCH";
                    sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@PROVIDER", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@RTF", SqlDbType.VarChar).Value = chk_RTF.Checked.ToString();
                    sqlcmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = Ddl_Value;
                    sqlcmd.Parameters.Add("@CounterID", SqlDbType.Int).Value = Convert.ToInt32(lbl_Counter.Text.ToString());
                    sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "RTFUPDATED";
                    sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = UserID;
                    sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress;
                    sqlcmd.ExecuteNonQuery();
                    con.Close();
                    lbl_status.Text = "RTF Updated Successfully !!";
                }
                System.Threading.Thread.Sleep(1000);
                BindGridView(Ddl_Value);
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        protected void chk_Status_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string UserID = Session["UID"].ToString();
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                GridViewRow CHK_Sts = (GridViewRow)(((CheckBox)sender).NamingContainer);
                CheckBox chk_Status = (CheckBox)CHK_Sts.FindControl("chk_Status");
                Label lbl_Counter = (Label)CHK_Sts.FindControl("lbl_counter");
                Ddl_Value = ddl_triptype.SelectedValue.Trim().ToString();
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    SqlConnection con = new SqlConnection(constr);
                    sqlcmd.Connection = con;
                    con.Open();
                    sqlcmd.CommandTimeout = 900;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "USP_AIRLINE_SERVICE_SWITCH";
                    sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@PROVIDER", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@RTF", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = chk_Status.Checked.ToString();
                    sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = Ddl_Value;
                    sqlcmd.Parameters.Add("@CounterID", SqlDbType.Int).Value = Convert.ToInt32(lbl_Counter.Text.ToString());
                    sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "STSUPDATED";
                    sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = UserID;
                    sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress;
                    sqlcmd.ExecuteNonQuery();
                    con.Close();
                    lbl_status.Text = "Status Updated Successfully !!";
                }
                System.Threading.Thread.Sleep(1000);
                BindGridView(Ddl_Value);
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        protected void rbl_Provider_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string UserID = Session["UID"].ToString();
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                GridViewRow RBL_Pvd = (GridViewRow)(((RadioButtonList)sender).NamingContainer);
                RadioButtonList rbl_Provider = (RadioButtonList)RBL_Pvd.FindControl("rbl_Provider");
                Label lbl_Counter = (Label)RBL_Pvd.FindControl("lbl_counter");
                Ddl_Value = ddl_triptype.SelectedValue.Trim().ToString();
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    SqlConnection con = new SqlConnection(constr);
                    sqlcmd.Connection = con;
                    con.Open();
                    sqlcmd.CommandTimeout = 900;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "USP_AIRLINE_SERVICE_SWITCH";
                    sqlcmd.Parameters.Add("@AIRLINE", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@PROVIDER", SqlDbType.VarChar).Value = rbl_Provider.SelectedValue;
                    sqlcmd.Parameters.Add("@RTF", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@TRIPTYPE", SqlDbType.VarChar).Value = Ddl_Value;
                    sqlcmd.Parameters.Add("@CounterID", SqlDbType.Int).Value = Convert.ToInt32(lbl_Counter.Text.ToString());
                    sqlcmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = "PROVIDERUPDATED";
                    sqlcmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = UserID;
                    sqlcmd.Parameters.Add("@IPADDRESS", SqlDbType.VarChar).Value = IPAddress;
                    sqlcmd.ExecuteNonQuery();
                    lbl_status.Text = "Provider Updated Successfully !!";
                    con.Close();
                }
                System.Threading.Thread.Sleep(1000);
                BindGridView(Ddl_Value);
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
    }
