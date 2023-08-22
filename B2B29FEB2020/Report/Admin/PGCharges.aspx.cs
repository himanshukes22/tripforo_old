using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class SprReports_Accounts_PGCharges : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null || string.IsNullOrEmpty(Convert.ToString(Session["UID"])))
            {
                Response.Redirect("~/Login.aspx");
            }
            if (IsPostBack == false)
            {
                GetAllPaymentMode();
                BindGridView();
            }
        }
        catch (Exception ex)
        { 
        
        
        }
    }
    private void GetAllPaymentMode()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SP_PGTransCharge", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@action", "All");
            da.Fill(dt);            
            ddlCardType.DataSource = dt;
            ddlCardType.DataValueField = "Id";
            ddlCardType.DataTextField = "PaymentMode";
            ddlCardType.DataBind();
            //Adding "select" option in dropdownlist 
            ddlCardType.Items.Insert(0, new ListItem("Select Payment Mode", "0"));
        }
        catch (Exception ex)
        {
           
            //con.Close();

        }
    }

    private void BindGridView()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SP_PGTransCharge", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@action", "All");
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();            
        }
        catch (Exception ex)
        {
            //con.Close();
        }
    }
    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Id = ddlCardType.SelectedValue;
            string PaymentMode = ddlCardType.SelectedItem.Text;
            GetPgTransDetailsByPgMode(Id, PaymentMode);
            //btnUpdate.Visible = false;            
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('" + Convert.ToString(ex.Message) + "');", true);
        }

    }
    private void GetPgTransDetailsByPgMode(string Id, string PaymentMode)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SP_PGTransCharge", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@PaymentMode", PaymentMode);
            da.SelectCommand.Parameters.AddWithValue("@action", "GetDetails");
            da.Fill(dt);           
            if (dt.Rows.Count > 0)
            {
                ddlChargeType.SelectedValue = Convert.ToString(dt.Rows[0]["ChargesType"]).Trim();
                //txtPgCharges.Text = (Convert.ToDouble(dt.Rows[0]["Charges"])).ToString("0.00");
                txtPgCharges.Text = Convert.ToString(dt.Rows[0]["Charges"]).Trim();
            }
            else
            {                
                txtPgCharges.Text = "";
            }
        }
        catch (Exception ex)
        {            
            //con.Close();
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    { 
        if (ddlCardType.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please select payment mode');", true);
            return;
        }
        if (txtPgCharges.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please enter transaction charges');", true);           
            return;
        }
        if (!String.IsNullOrEmpty(ddlCardType.SelectedValue) && ddlCardType.SelectedValue != "0" && !String.IsNullOrEmpty(txtPgCharges.Text))       
        {
            SqlCommand cmd = new SqlCommand("SP_PGTransCharge", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@id", ddlCardType.SelectedValue);                
                cmd.Parameters.AddWithValue("@Charges",Convert.ToDouble(txtPgCharges.Text));
                cmd.Parameters.AddWithValue("@ChargesType", ddlChargeType.SelectedValue);
                cmd.Parameters.AddWithValue("@ActionBy", Convert.ToString(Session["UID"]));
                cmd.Parameters.AddWithValue("@action", "update");
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    GetPgTransDetailsByPgMode(ddlCardType.SelectedValue, ddlCardType.SelectedItem.Text);
                    BindGridView();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Payment gateway transaction charges updated ');", true);
                }
                else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Try again);", true);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('" + Convert.ToString(ex.Message) + "');", true);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please again select payment mode,than update');", true);
        }
    }
}