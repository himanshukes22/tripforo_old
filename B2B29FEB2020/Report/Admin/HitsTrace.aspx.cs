using System;
using System.Linq;
using System.Web.UI;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

public partial class SprReports_HitsTrace : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            td_agentid.Attributes.Add("style", "dislpay: none;");
        }
    }
    protected void btn_Details_Click(object sender, EventArgs e)
    {
        string sFilePath = string.Empty;
        try
        {
            sFilePath = ConfigurationManager.AppSettings["LOGS_LOC"] +"\\" + From.Value;
            if (Directory.Exists(sFilePath))
            {
                tblresult.Visible = true;
                sFilePath = ConfigurationManager.AppSettings["LOGS_LOC"] + "\\" + From.Value + "\\" + ddlTime.SelectedValue + ".csv";
                var connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",Path.GetDirectoryName(sFilePath));

                using (var conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    var query = "SELECT * FROM [" + Path.GetFileName(sFilePath) + "]";
                    using (var adapter = new OleDbDataAdapter(query, conn))
                    {
                        var ds = new DataSet("CSV File");
                        adapter.Fill(ds);
                        Session["traceds"] = ds;
                        try
                        {
                            lblTotalHits.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                            lblDomFltSearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Search.aspx%'").Count());
                            lblDomFltResult.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Domestic/Result.aspx%'").Count());
                            lblDomFltPax.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Domestic/PaxDetails.aspx%'").Count());
                            lblDomFltBook.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Domestic/BookConfirmation.aspx%'").Count());

                            lblIntFltSearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Search.aspx%'").Count());
                            lblIntFltResult.Text = Convert.ToString(ds.Tables[0].Select("Path like '%International/FltResIntl.aspx%'").Count());
                            lblIntFltPax.Text = Convert.ToString(ds.Tables[0].Select("Path like '%International/PaxDetails.aspx%'").Count());
                            lblIntFltBook.Text = Convert.ToString(ds.Tables[0].Select("Path like '%International/BookConfirmation.aspx%'").Count());

                            lblHtlSearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Search.aspx%'").Count());
                            lblHtlResult.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Hotel/HtlResult.aspx%'").Count());
                            lblHtlPax.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Hotel/HotelCheckOut.aspx%'").Count());
                            lblHtlBook.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Hotel/HotelBookingConfirmation.aspx%'").Count());

                            lblBusSearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%BS/BusSearch.aspx%'").Count());
                            lblBusResult.Text = Convert.ToString(ds.Tables[0].Select("Path like '%BS/BusResult.aspx%'").Count());
                            lblBusPax.Text = Convert.ToString(ds.Tables[0].Select("Path like '%BS/CustomerInfo.aspx%'").Count());
                            lblBusBook.Text = Convert.ToString(ds.Tables[0].Select("Path like '%BS/TicketCopy.aspx%'").Count());

                            lblUtlSearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Utility/UtilityRecharge.aspx%'").Count());


                            lbl_sssearch .Text = Convert.ToString(ds.Tables[0].Select("Path like '%sightSeeing/searchSightseeing.aspx%'").Count());
                            lbl_ssresult .Text = Convert.ToString(ds.Tables[0].Select("Path like '%sightSeeing/SgtSearchResult.aspx%'").Count());
                            lbl_sspax .Text = Convert.ToString(ds.Tables[0].Select("Path like '%sightSeeing/SgtPaxinfo.aspx%'").Count());
                            lbl_ssbook .Text = Convert.ToString(ds.Tables[0].Select("Path like '%sightSeeing/SightseeingConfirmation.aspx%'").Count());


                            lbl_pkgsearch.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Holidays/package_search.aspx%'").Count());
                            lbl_pkgresult.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Holidays/Package_Details.aspx%'").Count());
                            lbl_pkgpax.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Holidays/Package_Checkout.aspx%'").Count());
                            lbl_pkgbook.Text = Convert.ToString(ds.Tables[0].Select("Path like '%Holidays/BookingConfirmation.aspx%'").Count());
                        }
                        catch (Exception ex)
                        {
	lblTotalHits.Text =ex.Message.ToString();
                        }
                    }

                }
            }
            else
            {
                tblresult.Visible = false;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('No file found');", true);
                return;
            }
        }
        catch (Exception ex)
        {
	lblTotalHits.Text =ex.Message.ToString();
        }
    }

    protected void lblDomFltResult_Click(object sender, EventArgs e)

    {
        try{
        DataSet  dscount = (DataSet)Session["traceds"];
        td_module.InnerText = "DOMESTIC FLIGHT RESULT AGENTS";
        //string tbl = "";
        //tbl += "<table>";
        //tbl += "<tr>";
        ////foreach (DataRow dr in dscount.Tables[0].Rows)
        //for(int i=0;i<dscount.Tables[0].Rows.Count;i++)
        //{
        //    tbl += "<td>" + dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'")[i].ItemArray[22] + "</td>";
        //   // tbl += "<tr>";
        //}
        //tbl += "<tr>";
        //tbl += "<table>";
        DataTable dt = new DataTable();
        dt = dscount.Tables[0];//.DefaultView.ToTable(true, "username");//.Rows[0][""].Select("Path like '%Domestic/Result.aspx%'")
        DataRow[] droneway;
        droneway = dt.Select("Path like '%Domestic/Result.aspx%'");
        //var dsdestination = dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");
        DataTable dt1 = droneway.CopyToDataTable();
        //GridView1.DataSource = dt1.DefaultView.ToTable(true, "username");// dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");//.DefaultView.ToTable(true, "username");
        //GridView1.DataBind();
        
        DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");
        DataList1.DataBind();
        DataList1.Visible = true;
        lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }
            
            if (DataList1.Items.Count == 0)
            {
                DataList1.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "No Record Found.";
            }
    }
    protected void lblDomFltPax_Click(object sender, EventArgs e)
    {
        try{
        DataSet dscount = (DataSet)Session["traceds"];
        td_module.InnerText = "DOMESTIC FLIGHT PAX INFO AGENTS";
        //string tbl = "";
        //tbl += "<table>";
        //tbl += "<tr>";
        ////foreach (DataRow dr in dscount.Tables[0].Rows)
        //for(int i=0;i<dscount.Tables[0].Rows.Count;i++)
        //{
        //    tbl += "<td>" + dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'")[i].ItemArray[22] + "</td>";
        //   // tbl += "<tr>";
        //}
        //tbl += "<tr>";
        //tbl += "<table>";
        DataTable dt = new DataTable();
        dt = dscount.Tables[0];//.DefaultView.ToTable(true, "username");//.Rows[0][""].Select("Path like '%Domestic/Result.aspx%'")
        DataRow[] droneway;
        droneway = dt.Select("Path like '%Domestic/PaxDetails.aspx%'");
        //var dsdestination = dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");
        DataTable dt1 = droneway.CopyToDataTable();
        //GridView1.DataSource = dt1.DefaultView.ToTable(true, "username");// dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");//.DefaultView.ToTable(true, "username");
        //GridView1.DataBind();
       
        DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

        DataList1.DataBind();
        DataList1.Visible = true;
        lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblDomFltBook_Click(object sender, EventArgs e)
    {
        try{
        DataSet dscount = (DataSet)Session["traceds"];
        td_module.InnerText = "DOMESTIC FLIGHT BOOKING AGENTS";
        //string tbl = "";
        //tbl += "<table>";
        //tbl += "<tr>";
        ////foreach (DataRow dr in dscount.Tables[0].Rows)
        //for(int i=0;i<dscount.Tables[0].Rows.Count;i++)
        //{
        //    tbl += "<td>" + dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'")[i].ItemArray[22] + "</td>";
        //   // tbl += "<tr>";
        //}
        //tbl += "<tr>";
        //tbl += "<table>";
        DataTable dt = new DataTable();
        dt = dscount.Tables[0];//.DefaultView.ToTable(true, "username");//.Rows[0][""].Select("Path like '%Domestic/Result.aspx%'")
        DataRow[] droneway;
        droneway = dt.Select("Path like '%Domestic/BookConfirmation.aspx%'");
        //var dsdestination = dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");
        DataTable dt1 = droneway.CopyToDataTable();
        //GridView1.DataSource = dt1.DefaultView.ToTable(true, "username");// dscount.Tables[0].Select("Path like '%Domestic/Result.aspx%'");//.DefaultView.ToTable(true, "username");
        //GridView1.DataBind();
      
        DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

        DataList1.DataBind();
        DataList1.Visible = true;
        lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }

    protected void lblIntFltResult_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "INTERNATIONAL FLIGHT RESULT AGENTS";
            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%International/FltResIntl.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblIntFltPax_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "INTERNATIONAL FLIGHT PAX INFO AGENTS";
            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%International/PaxDetails.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            
           
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");
            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblIntFltBook_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "INTERNATIONAL FLIGHT BOOKING AGENTS";
            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            
            try
            {
                dt = dscount.Tables[0];
                DataRow[] droneway = new DataRow[0]; ;
                droneway = dt.Select("Path like '%International/BookConfirmation.aspx%'");
                 dt1 = droneway.CopyToDataTable();

                
                DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

                DataList1.DataBind();
                DataList1.Visible = true;
                lblMessage.Visible = false;

            }
            catch (Exception ex)
            {
                DataTable dt2 = new DataTable();
                DataList1.DataSource = dt2;

                DataList1.DataBind();
            }
            
            if (DataList1.Items.Count == 0)
            {
                DataList1.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "No Record Found.";
            }
        }
        catch (Exception ex)
        { }
    }

    protected void lblHtlResult_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "HOTEL RESULT AGENTS";
            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Hotel/HtlResult.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");
            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblHtlPax_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "HOTEL PAX INFO AGENTS";
            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Hotel/HotelCheckOut.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblHtlBook_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "HOTEL BOOKING AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Hotel/HotelBookingConfirmation.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }

    protected void lblBusResult_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "BUS RESULT AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%BS/BusResult.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }
        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblBusPax_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "BUS PAX INFO AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%BS/CustomerInfo.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }
        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lblBusBook_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "BUS BOOKING AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%BS/TicketCopy.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }

    protected void lbl_ssresult_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "SIGHTSEEING RESULT AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%sightSeeing/SgtSearchResult.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lbl_sspax_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "SIGHTSEEING PAX INFO AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%sightSeeing/SgtPaxinfo.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lbl_ssbook_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "SIGHTSEEING BOOKING AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%sightSeeing/SightseeingConfirmation.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }


    protected void lbl_pkgresult_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "PACKAGES RESULT AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Holidays/Package_Details.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lbl_pkgpax_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "PACKAGES PAX INFO AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Holidays/Package_Checkout.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
    protected void lbl_pkgbook_Click(object sender, EventArgs e)
    {
        try
        {
            td_module.InnerText = "PACKAGES QUERY SENT AGENTS";

            DataSet dscount = (DataSet)Session["traceds"];
            DataTable dt = new DataTable();
            dt = dscount.Tables[0];
            DataRow[] droneway;
            droneway = dt.Select("Path like '%Holidays/BookingConfirmation.aspx%'");
            DataTable dt1 = droneway.CopyToDataTable();
            DataList1.DataSource = dt1.DefaultView.ToTable(true, "username");

            DataList1.DataBind();
            DataList1.Visible = true;
            lblMessage.Visible = false;

        }
        catch (Exception ex)
        {
            DataTable dt2 = new DataTable();
            DataList1.DataSource = dt2;

            DataList1.DataBind();
        }

        if (DataList1.Items.Count == 0)
        {
            DataList1.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "No Record Found.";
        }
    }
}