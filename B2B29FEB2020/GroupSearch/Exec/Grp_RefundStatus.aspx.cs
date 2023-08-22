using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GRP_Booking;
using System.Globalization;

public partial class GroupSearch_Exec_Grp_RefundStatus : System.Web.UI.Page
{
    string PaymentStatus = "", FromDate = "", ToDate = "", RequestID = "", UserType = "", UserID = "";
    GroupBooking ObjGB = new GroupBooking();
    SqlTransactionDom ObjST = new SqlTransactionDom();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (Page.IsPostBack != true)
            {
                if (Session["UID"] == null || Session["UID"].ToString() == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UserType = Session["User_Type"].ToString();
                    UserID = Session["UID"].ToString();
                    PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                    Session["ds"] = "";
                    ds = ObjGB.ShowRefundData("", PaymentStatus, "", "", UserType, UserID);
                    Session["ds"] = ds;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GrpBookingDetails.DataSource = ds;
                        GrpBookingDetails.DataBind();
                    }
                    else if (ds.Tables[0].Rows.Count <= 0)
                    {
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        GrpBookingDetails.DataSource = ds.Tables[0];
                        GrpBookingDetails.DataBind();
                        int columncount = GrpBookingDetails.Rows[0].Cells.Count;
                        GrpBookingDetails.Rows[0].Cells.Clear();
                        GrpBookingDetails.Rows[0].Cells.Add(new TableCell());
                        GrpBookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                        GrpBookingDetails.Rows[0].Cells[0].Text = "No Request Found";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsPageLoad");
        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_fromDate.Text.ToString() != "" && txt_todate.Text.ToString() != "")
        {
            if (DateTime.ParseExact(txt_fromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_todate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture))
            {
                txt_fromDate.Text = "";
                txt_todate.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", true);
            }
        }
        else
        {
            try
            {
                UserType = Session["User_Type"].ToString();
                UserID = Session["UID"].ToString();
                DataSet ds = new DataSet();
                PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                RequestID = txt_RequestID.Text.Trim().ToString();
                FromDate = txt_fromDate.Text.Trim().ToString();
                ToDate = txt_todate.Text.Trim().ToString();
                if (FromDate != "")
                {
                    FromDate = FromDate.Substring(6, 4) + "-" + FromDate.Substring(3, 2) + "-" + FromDate.Substring(0, 2) + " 00:00:00.001";
                }
                if (ToDate != "")
                {
                    ToDate = ToDate.Substring(6, 4) + "-" + ToDate.Substring(3, 2) + "-" + ToDate.Substring(0, 2) + " 23:59:59.999";
                }
                Session["ds"] = "";
                ds = ObjGB.ShowRefundData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
                Session["ds"] = ds;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrpBookingDetails.DataSource = ds;
                    GrpBookingDetails.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GrpBookingDetails.DataSource = ds;
                    GrpBookingDetails.DataBind();
                    int columncount = GrpBookingDetails.Rows[0].Cells.Count;
                    GrpBookingDetails.Rows[0].Cells.Clear();
                    GrpBookingDetails.Rows[0].Cells.Add(new TableCell());
                    GrpBookingDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    GrpBookingDetails.Rows[0].Cells[0].Text = "No Request Found";
                }
                txt_RequestID.Text = "";
                txt_fromDate.Text = "";
                txt_todate.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogTrace.WriteErrorLog(ex, "GroupDetailsBtnSubmit");
            }
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        if (txt_fromDate.Text.ToString() != "" && txt_todate.Text.ToString() != "")
        {
            if (DateTime.ParseExact(txt_fromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_todate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture))
            {
                txt_fromDate.Text = "";
                txt_todate.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('To date cannot be less than from date!!');", true);
            }
        }
        else
        {
            SqlTransactionDom STDom = new SqlTransactionDom();
            try
            {
                UserType = Session["User_Type"].ToString();
                UserID = Session["UID"].ToString();
                DataSet ds = new DataSet();
                PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                RequestID = txt_RequestID.Text.Trim().ToString();
                FromDate = txt_fromDate.Text.Trim().ToString();
                ToDate = txt_todate.Text.Trim().ToString();
                if (FromDate != "")
                {
                    FromDate = FromDate.Substring(6, 4) + "-" + FromDate.Substring(3, 2) + "-" + FromDate.Substring(0, 2) + " 00:00:00.001";
                }
                if (ToDate != "")
                {
                    ToDate = ToDate.Substring(6, 4) + "-" + ToDate.Substring(3, 2) + "-" + ToDate.Substring(0, 2) + " 23:59:59.999";
                }
                ds.Clear();
                ds = ObjGB.ShowRefundData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);

                DataTable StrDataTable = new DataTable();
                STDom.ExportData(ds);
            }
            catch (Exception ex)
            {
                ErrorLogTrace.WriteErrorLog(ex, "ExportRefund");
            }
        }
    }
    protected void GrpBookingDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrpBookingDetails.PageIndex = e.NewPageIndex;
        DataSet strds = (DataSet)Session["ds"];
        GrpBookingDetails.DataSource = strds;
        GrpBookingDetails.DataBind();
    }
}