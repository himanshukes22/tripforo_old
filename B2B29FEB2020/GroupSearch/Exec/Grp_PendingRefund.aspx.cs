using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GRP_Booking;

public partial class GroupSearch_Exec_Grp_PendingRefund : System.Web.UI.Page
{
    DataSet DSCancel = new DataSet();
    string UserId = "", UserType = "";
    GroupBooking GB = new GroupBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null || Session["UID"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            UserId = Session["UID"].ToString();
            UserType = Session["User_Type"].ToString();
            if (IsPostBack != true)
            {
                lbl_Norecord.Text = "";
                DSCancel = RefundRequest("", "Pending");
                GridRefundRequest.DataSource = DSCancel;
                GridRefundRequest.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "Pending refund");
        }
    }
    protected void GridRefundRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ReqUpdate")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit_1");
                LinkButton lnkHides_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides_1");
                txtRemark.Visible = true;
                lnkSubmit_1.Visible = true;
                lnkHides_1.Visible = true;
                GridRefundRequest.Columns[14].Visible = false;
            }
            if (e.CommandName == "GRPREFUNDUPDATE")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_Group_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit_1");
                LinkButton lnkHides_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides_1");
                txtRemark.Visible = true;
                lnkSubmit_1.Visible = true;
                lnkHides_1.Visible = true;
                if (txtRemark.Text.Trim().ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Remark can not be blank,Please Fill Remark');", true);
                }
                else
                {
                    GB.UPdateStatusREFUNDREQUEST(lbl_Reqid.Text.Trim().ToString(), "Refunded", txtRemark.Text.Trim().ToString(), Session["UID"].ToString());
                    GridRefundRequest.DataSource = RefundRequest("", "InProcess");
                    GridRefundRequest.DataBind();
                    GridRefundRequest.Columns[14].Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Request has been rejected!!');", true);
                }
            }
            if (e.CommandName == "GRP_CANCEL")
            {
                LinkButton lb1 = e.CommandSource as LinkButton;
                GridViewRow gvr1 = lb1.Parent.Parent as GridViewRow;
                int RowIndex = gvr1.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                Label lbl_Reqid = (Label)GridRefundRequest.Rows[RowIndex].FindControl("lbl_RequestID");
                TextBox txtRemark = (TextBox)GridRefundRequest.Rows[RowIndex].FindControl("txtRemark");
                LinkButton lnkSubmit_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkSubmit_1");
                LinkButton lnkHides_1 = (LinkButton)GridRefundRequest.Rows[RowIndex].FindControl("lnkHides_1");
                txtRemark.Visible = false;
                lnkSubmit_1.Visible = false;
                lnkHides_1.Visible = false;
                GridRefundRequest.Columns[14].Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "_RowCommand");
        }

    }
    public DataSet RefundRequest(string Requestid, string CMD_TYPE)
    {
        DataSet ds = new DataSet();
        ds = GB.REFUNDREQUEST("", CMD_TYPE);
        return ds;
    }
    protected void GridRefundRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridRefundRequest.PageIndex = e.NewPageIndex;
            DSCancel = RefundRequest("", "Pending");
            GridRefundRequest.DataSource = DSCancel;
            GridRefundRequest.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogTrace.WriteErrorLog(ex, "GridRefundRequest_PageIndexChanging");
        }
    }
}