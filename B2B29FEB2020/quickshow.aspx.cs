using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class quickshow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UID"] == null)
        {
            Response.Redirect("/Login.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                GetCityFrom("cityfrom");
                GetCityFrom("tocity");
                GetCityFrom("airline");
                BindQuickShowDetails();
            }
        }
    }

    private void BindQuickShowDetails()
    {
        try
        {
            gvQuickShow.DataSource = GetQuickShow();
            gvQuickShow.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private DataTable GetQuickShow()
    {
        DataTable dataTable = new DataTable();
        try
        {
            string fromdate = ""; string todate = "";
            if (!string.IsNullOrEmpty(Request["From"]))
            {
                fromdate = String.Format(Request["From"].Split('-')[2], 4) + "-" + String.Format(Request["From"].Split('-')[1], 2) + "-" + String.Format(Request["From"].Split('-')[0], 2) + " 00:00:00";
            }
            if (!string.IsNullOrEmpty(Request["To"]))
            {
                todate = String.Format(Request["To"].Split('-')[2], 4) + "-" + String.Format(Request["To"].Split('-')[1], 2) + "-" + String.Format(Request["To"].Split('-')[0], 2);
            }

            SqlCommand sqlCommand = new SqlCommand("sp_QuickShow", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@type", "agent");
            sqlCommand.Parameters.AddWithValue("@fromdate", fromdate);
            sqlCommand.Parameters.AddWithValue("@todate", todate);
            sqlCommand.Parameters.AddWithValue("@cityfrom", ddlFromCity.SelectedItem.Value);
            sqlCommand.Parameters.AddWithValue("@cityto", ddlToCity.SelectedItem.Value);
            sqlCommand.Parameters.AddWithValue("@airline", ddlAirline.SelectedItem.Value);
            sqlCommand.Parameters.AddWithValue("@pnr", "");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(dataTable);
            Session["Record"] = dataTable;
            lblTotalRecord.Text = "Total Record : " + dataTable.Rows.Count;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return dataTable;
    }

    private DataTable GetCityFrom(string city)
    {
        DataTable dataTable = new DataTable();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("sp_QuickShow", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@type", city);
            sqlCommand.Parameters.AddWithValue("@fromdate", "");
            sqlCommand.Parameters.AddWithValue("@todate", "");
            sqlCommand.Parameters.AddWithValue("@cityfrom", "");
            sqlCommand.Parameters.AddWithValue("@cityto", "");
            sqlCommand.Parameters.AddWithValue("@airline", "");
            sqlCommand.Parameters.AddWithValue("@pnr", "");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            if (city == "cityfrom")
            {
                ddlFromCity.DataSource = dataTable;
                ddlFromCity.DataTextField = "OrgDestFrom";
                ddlFromCity.DataValueField = "DepAirportCode";
                ddlFromCity.DataBind();
                ddlFromCity.Items.Insert(0, new ListItem("Select From City", string.Empty));
            }
            else if (city == "tocity")
            {
                ddlToCity.DataSource = dataTable;
                ddlToCity.DataTextField = "OrgDestTo";
                ddlToCity.DataValueField = "ArrAirportCode";
                ddlToCity.DataBind();
                ddlToCity.Items.Insert(0, new ListItem("Select To City", string.Empty));
            }
            else
            {
                ddlAirline.DataSource = dataTable;
                ddlAirline.DataTextField = "AirLineName";
                ddlAirline.DataValueField = "AirLineName";
                ddlAirline.DataBind();
                ddlAirline.Items.Insert(0, new ListItem("Select Airline", string.Empty));
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return dataTable;
    }

    protected void gvQuickShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvQuickShow.PageIndex = e.NewPageIndex;

        gvQuickShow.DataSource = Session["Record"];
        gvQuickShow.DataBind();
    }

    protected void btn_result_Click(object sender, EventArgs e)
    {
        gvQuickShow.DataSource = GetQuickShow();
        gvQuickShow.DataBind();
    }
}