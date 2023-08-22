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

public partial class GroupSearch_Exec_QCGroupBookingReport : System.Web.UI.Page
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
                    UserType = "LOOKUP";
                    UserID = Session["UID"].ToString();
                    PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                    ds = ObjGB.ShowBookingData("", PaymentStatus, "", "", UserType, UserID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvEmployee.DataSource = ds;
                        gvEmployee.DataBind();
                    }
                    else if (ds.Tables[0].Rows.Count <= 0)
                    {
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        gvEmployee.DataSource = ds.Tables[0];
                        gvEmployee.DataBind();
                        int columncount = gvEmployee.Rows[0].Cells.Count;
                        gvEmployee.Rows[0].Cells.Clear();
                        gvEmployee.Rows[0].Cells.Add(new TableCell());
                        gvEmployee.Rows[0].Cells[0].ColumnSpan = columncount;
                        gvEmployee.Rows[0].Cells[0].Text = "No Request Found";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "lookupreport");
        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        {
            try
            {
                UserType = "LOOKUP";
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
                ds = ObjGB.ShowBookingData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvEmployee.DataSource = ds;
                    gvEmployee.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvEmployee.DataSource = ds;
                    gvEmployee.DataBind();
                    int columncount = gvEmployee.Rows[0].Cells.Count;
                    gvEmployee.Rows[0].Cells.Clear();
                    gvEmployee.Rows[0].Cells.Add(new TableCell());
                    gvEmployee.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvEmployee.Rows[0].Cells[0].Text = "No Request Found";
                }
                txt_RequestID.Text = "";
                txt_fromDate.Text = "";
                txt_todate.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogTrace.WriteErrorLog(ex, "lookupreport");
            }
        }
    }
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
            ds = ObjGB.ShowBookingData("", PaymentStatus, "", "", "LOOKUP", UserID);
            gvEmployee.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "lookupreportpaging");
        }
    }
}