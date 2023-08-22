using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class SprReports_CItyManagementPanel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    SqlDataAdapter adp = null;
    DataTable dt = null;
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select State", "0");
        if(!IsPostBack)
        {
            ddlState.Items.Insert(0, li);
            getStates();
            txtCity.Enabled = false;
        }
        if(IsPostBack)
        {
            if(ddlState.SelectedValue=="0")
            {
                txtCity.Enabled = false;
                cityGridview.DataSource = "";
                cityGridview.DataBind();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkIfCityExist()==false)
            {
                cmd = new SqlCommand("insert into TBL_CITY (CITY,STATEID) values ('" + txtCity.Text.Trim() + "','" + ddlState.SelectedValue + "')", con);
                cmd.CommandType = CommandType.Text;
                if (con.State == ConnectionState.Open){
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('City added to the city table.')</script>");
                getCities();
                ddlState.SelectedValue = "0";
                txtCity.Text = "";
                txtCity.Enabled = false;
            }
            else{
                Response.Write("<script>alert('This city is already exist.')</script>");
                txtCity.Text = "";
                txtCity.Focus();
            }
        }
        catch(Exception)
        {
            //
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
    }
    public void getStates()
    {
        adp = new SqlDataAdapter("select STATE,STATEID from TBL_STATE order by STATE", con);
        adp.SelectCommand.CommandType = CommandType.Text;
        dt = new DataTable();
        adp.Fill(dt);
        ddlState.DataSource = dt;
        ddlState.DataTextField = "STATE";
        ddlState.DataValueField = "STATEID";
        ddlState.DataBind();
    }
    public void getCities()
    {
        adp = new SqlDataAdapter("select c.COUNTER,c.CITY,s.STATE,c.CREATEDDATE from TBL_CITY c,TBL_STATE s where c.STATEID='" + ddlState.SelectedValue + "' and c.STATEID=s.STATEID order by c.CREATEDDATE desc", con);
        adp.SelectCommand.CommandType = CommandType.Text;
        dt = new DataTable();
        adp.Fill(dt);
        cityGridview.DataSource = dt;
        cityGridview.DataBind();
    }
    public bool checkIfCityExist()
    {
        try
        {
            cmd = new SqlCommand("select COUNT(*) from TBL_CITY where STATEID='" + ddlState.SelectedValue + "' and CITY='" + txtCity.Text.Trim() + "'", con);
            cmd.CommandType = CommandType.Text;
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            int effect = (int)cmd.ExecuteScalar();
            if(effect > 0){
               return true;
            }
            else{
                return false;
            }
        }
       catch(Exception)
        {
            return true;
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlState.SelectedValue!="0"){ 
        getCities();
        txtCity.Enabled = true;
        }
        else{
            ddlState.SelectedValue = "0";
            txtCity.Enabled = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string Query = "select c.COUNTER,c.CITY,s.STATE,c.CREATEDDATE from TBL_CITY c,TBL_STATE s where c.STATEID='" + ddlState.SelectedValue + "' and c.STATEID=s.STATEID and c.CITY='" + txtCity.Text.Trim() + "' order by c.CREATEDDATE desc";
            adp = new SqlDataAdapter(Query, con);
            adp.SelectCommand.CommandType = CommandType.Text;
            dt = new DataTable();
            adp.Fill(dt);
            cityGridview.DataSource = dt;
            cityGridview.DataBind();
        }
        catch (Exception ex)
        {
            string messsage = "<script>alert('" + ex + "')</script>";
            Response.Write(messsage);
        }
        finally
        {
            adp.Dispose();
        }
    }
    protected void cityGridview_RowEditing(object sender, GridViewEditEventArgs e)
    {
        cityGridview.EditIndex = e.NewEditIndex;
        getCities();
    }
    protected void cityGridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        cityGridview.EditIndex = -1;
        getCities();
    }
    protected void cityGridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string counterValue = ((HiddenField)cityGridview.Rows[e.RowIndex].FindControl("counterHiddenField")).Value;
            string cityName = ((TextBox)cityGridview.Rows[e.RowIndex].FindControl("txtEditCity")).Text;
            cmd = new SqlCommand("update TBL_CITY set CITY='" + cityName + "' where COUNTER='" + counterValue + "'", con);
            cmd.CommandType = CommandType.Text;
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('City updated successfully.')</script>");
        }
        catch(Exception)
        {
            //
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        cityGridview.EditIndex = -1;
        getCities();
    }
    protected void cityGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string counterValue = ((HiddenField)cityGridview.Rows[e.RowIndex].FindControl("counterHiddenField")).Value;
            cmd = new SqlCommand("  delete from TBL_CITY where COUNTER='"+counterValue+"'",con);
            cmd.CommandType = CommandType.Text;
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch(Exception)
        {
            //
        }
        finally
        {

        }
        cityGridview.EditIndex = -1;
        getCities();
    }
}