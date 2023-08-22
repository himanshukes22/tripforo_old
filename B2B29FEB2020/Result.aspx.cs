using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Result : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ViewState["name"] = TextBox1.Text;
            //ViewState["password"] = TextBox2.Text;

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["webresult"])) && Convert.ToString(ViewState["webresult"])== "true")
            {
                div1.Visible = false;
                div2.Visible = true;
            if (!IsPostBack)
            {
                    BindValue();
            }
            }
            else
            {
                div1.Visible = true;
                div2.Visible = false;
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('"+ ex.Message+ "');", true);
        }       

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            string AirlineCode = DropDownAirline.SelectedValue;
            string Status = DropDownStatus.SelectedValue;
            if (!string.IsNullOrEmpty(Convert.ToString(DropDownAirline.SelectedValue)) && Convert.ToString(DropDownAirline.SelectedValue) != "0" && !string.IsNullOrEmpty(Convert.ToString(DropDownStatus.SelectedValue)) && Convert.ToString(DropDownStatus.SelectedValue) != "0" && Convert.ToString(ViewState["webresult"]) == "true")
            {
                int flag = Upadte(Status, AirlineCode);               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Select Value');", true);
            }
        }
        catch(Exception ex)
        {

        }
    }

    public void BindValue()
    {
        try
        {
            DropDownAirline.AppendDataBoundItems = true;
            DropDownAirline.Items.Clear();
            DropDownAirline.DataSource = GetAirline().Tables[0];
            DropDownAirline.DataTextField = "Provider";
            DropDownAirline.DataValueField = "Provider";
            DropDownAirline.DataBind();
            DropDownAirline.Items.Insert(0, new ListItem("-- Select Type --", "0"));

            GridView1.DataSource = GetAirline().Tables[1];
            GridView1.DataBind();           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('" + ex.Message + "');", true);
        }

    }

    public DataSet GetAirline()
    {
        DataSet ds = new DataSet();         
        string ActionType = "SELECT";        
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            adap = new SqlDataAdapter("SP_FLIGHT_RESULT_STATUS", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@ActionType", ActionType);            
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('" + ex.Message + "');", true);
            clsErrorLog.LogInfo(ex);
        }
        finally
        {
            con.Close();
            adap.Dispose();
        }
        return ds;
    }
    private int Upadte(string Status, string AirlineCode)
    {
        string CreatedBy = Convert.ToString(ViewState["UserName"]);
        string IPAddress = GetIPAddress();
        string ActionType = "UPDATE";
        int flag = 0;
      //  cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(Status));
        SqlCommand cmd=null;
        try
        {
            cmd = new SqlCommand("SP_FLIGHT_RESULT_STATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(Status));
            cmd.Parameters.AddWithValue("@AirlineCode", AirlineCode);
            cmd.Parameters.AddWithValue("@Trip", DropDownTrip.SelectedValue);           
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("IPAddress", IPAddress);
            cmd.Parameters.AddWithValue("@ActionType", ActionType);
            if (con.State == ConnectionState.Closed)
                con.Open();
            flag = cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
            if (flag > 0)
            {
                BindValue();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Upadted');", true);
            }
            else
            {
                BindValue();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('try again');", true);
            }
        }
        catch (Exception ex)
        {
            con.Close();
            cmd.Dispose();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('" + ex.Message + "');", true);
        }
        return flag;

    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string IpAddress = string.Empty;
        try
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    IpAddress = addresses[0];
                }
            }
            else
            {
                IpAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        catch (Exception ex)
        {

        }
        return IpAddress;

    }



    protected void btnLogin_Click(object sender, EventArgs e)
    {

        string UserName = txtUserId.Text;
        string Password = txtPassword.Text;
        ViewState["UserName"] = UserName;
        if (UserName=="totaram" && Password== "totaram")
        {
            txtUserId.Text = "";
            txtPassword.Text = "";
            ViewState["webresult"] = "true";
            div1.Visible = false;
            div2.Visible = true;
            BindValue();
        }
        else
        {
            div1.Visible = true;
            div2.Visible = false;
            ViewState["webresult"] = "false";
        }

    }
}