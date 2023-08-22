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

public partial class GroupSearch_ExecRequestDetails : System.Web.UI.Page
{
    string PaymentStatus = "", FromDate = "", ToDate = "", RequestID = "", UserType = "", UserID = "";
    GroupBooking ObjGB = new GroupBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DivExec.InnerHtml = "";
            DataSet ds = new DataSet();
            if (Page.IsPostBack != true)
            {
                if (Session["UID"] == null || Session["UID"].ToString() == "")
                {
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    UserType = Session["User_Type"].ToString();
                    UserID = Session["UID"].ToString();
                    PaymentStatus = ddl_status.SelectedValue.Trim().ToString();
                    ds = ObjGB.ShowBookingData("", PaymentStatus, "", "", UserType, UserID);
                    DataTable dt = new DataTable();
                    DataRow[] results = ds.Tables[0].Select("AcceptBy='" + UserID + "' or RejectBy='" + UserID + "'");
                    if (UserType.ToUpper() == "EXEC" && results.Length > 0)
                    {
                        if (results.Length > 0)
                        {
                            dt = results.CopyToDataTable();
                            GrpBookingDetails.DataSource = dt;
                            GrpBookingDetails.DataBind();
                        }
                        else
                        {
                            DivExec.InnerHtml = "No Record found!!";
                        }
                    }
                    else if (ds.Tables[0].Rows.Count <= 0)
                    {
                        DivExec.InnerHtml = "No Record found!!";
                    }
                    else
                    {
                        DivExec.InnerHtml = "No Record found!!";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ExecRequestDetailsPageLoad");
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            DivExec.InnerHtml = "";
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
            ds = ObjGB.ShowBookingData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                DataRow[] results = ds.Tables[0].Select("AcceptBy='" + UserID + "' or RejectBy='" + UserID + "'");
                if (results.Length > 0)
                {
                    dt = results.CopyToDataTable();
                    GrpBookingDetails.DataSource = dt;
                    GrpBookingDetails.DataBind();
                }
                else
                {
                    DivExec.InnerHtml = "No Record found!!";
                }
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
            ErrorLogTrace.WriteErrorLog(ex, "ExecRequestDetailsBtnSubmit");
        }
    }
    protected void GrpBookingDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrpBookingDetails.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ExecRequestDetailsPageIndexChanging");
        }
    }
    protected void link_Refund_Click(object sender, EventArgs e)
    {

    }
    protected void GrpBookingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbSt = (Label)e.Row.FindControl("lblStatus");
            if (lbSt.Text.ToLower() == "ticketed")
                e.Row.Cells[15].Visible = true;
            else
            {
                e.Row.Cells[15].Visible = false;
            }
        }
    }
    protected void link_Invoice_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow GV = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblreq = (Label)GV.FindControl("lblRequestID");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../GroupSearch/InvoiceDetails.aspx?RequestID=" + lblreq.Text.Trim().ToString() + "','_newtab');", true);
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "link_Invoice_Clickexec");
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
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
            ds = ObjGB.ShowBookingData(RequestID, PaymentStatus, FromDate, ToDate, UserType, UserID);
            DataTable StrDataTable = new DataTable();
            STDom.ExportData(ds);
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "ExportDetails");
        }
    }
}