using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SprReports_LogTracker : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
    string OrderID = "", PNRNo = "", CMDTYPE = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            OrderID = txt_OrderId.Text.Trim().ToString();
            PNRNo = txt_PNR.Text.Trim().ToString();
            CMDTYPE = ddl_PTYPE.SelectedValue.Trim().ToString();
            if (OrderID == "" || PNRNo == "")
            {
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection con = new SqlConnection(constr);
                sqlcmd.Connection = con;
                con.Open();
                sqlcmd.CommandTimeout = 900;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "USP_LOGTRACKER_DETAILS";
                sqlcmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = OrderID;
                sqlcmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNRNo;
                sqlcmd.Parameters.Add("@CMDTYPE", SqlDbType.VarChar).Value = CMDTYPE;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                if (CMDTYPE == "F")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divSelectedFlightDetails_Gal.Visible = true;
                        SelectedFlightDetails_Gal.DataSource = ds.Tables[0];
                        SelectedFlightDetails_Gal.DataBind();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        divfltheader.Visible = true;
                        FltHeader.DataSource = ds.Tables[1];
                        FltHeader.DataBind();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        divFltDetails.Visible = true;
                        FltDetails.DataSource = ds.Tables[2];
                        FltDetails.DataBind();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        divFltFareDetails.Visible = true;
                        FltFareDetails.DataSource = ds.Tables[3];
                        FltFareDetails.DataBind();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        divFltPaxDetails.Visible = true;
                        FltPaxDetails.DataSource = ds.Tables[4];
                        FltPaxDetails.DataBind();
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        divT_Flt_Meal_And_Baggage_Request.Visible = true;
                        T_Flt_Meal_And_Baggage_Request.DataSource = ds.Tables[5];
                        T_Flt_Meal_And_Baggage_Request.DataBind();
                    }
                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        divTBL_ITZ_TRANSACTIONS.Visible = true;
                        TBL_ITZ_TRANSACTIONS.DataSource = ds.Tables[6];
                        TBL_ITZ_TRANSACTIONS.DataBind();
                    }
                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        divReIssueIntl.Visible = true;
                        ReIssueIntl.DataSource = ds.Tables[7];
                        ReIssueIntl.DataBind();

                    }
                    if (ds.Tables[8].Rows.Count > 0)
                    {
                        divCancellationIntl.Visible = true;
                        CancellationIntl.DataSource = ds.Tables[8];
                        CancellationIntl.DataBind();
                    }
                    if (ds.Tables[9].Rows.Count > 0)
                    {
                        divTBL_YABookingLogs.Visible = true;
                        TBL_YABookingLogs.DataSource = ds.Tables[9];
                        TBL_YABookingLogs.DataBind();
                    }
                    if (ds.Tables[10].Rows.Count > 0)
                    {
                        divTBL_TBOBookingLogs.Visible = true;
                        TBL_TBOBookingLogs.DataSource = ds.Tables[10];
                        TBL_TBOBookingLogs.DataBind();
                    }
                    if (ds.Tables[11].Rows.Count > 0)
                    {
                        divGDSBookingLogs.Visible = true;
                        GDSBookingLogs.DataSource = ds.Tables[11];
                        GDSBookingLogs.DataBind();

                    }
                    if (ds.Tables[12].Rows.Count > 0)
                    {
                        divTicketingLog_GAL.Visible = true;
                        TicketingLog_GAL.DataSource = ds.Tables[12];
                        TicketingLog_GAL.DataBind();
                    }
                    if (ds.Tables[13].Rows.Count > 0)
                    {
                        divPGLog.Visible = true;
                        PGLog.DataSource = ds.Tables[13];
                        PGLog.DataBind();
                    }
                }
                if (CMDTYPE == "B")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divTBL_RB_SEATBOOKINGDETAILS.Visible = true;
                        TBL_RB_SEATBOOKINGDETAILS.DataSource = ds.Tables[0];
                        TBL_RB_SEATBOOKINGDETAILS.DataBind();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        divTBL_RB_SeatDetails.Visible = true;
                        TBL_RB_SeatDetails.DataSource = ds.Tables[1];
                        TBL_RB_SeatDetails.DataBind();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        divTBL_RB_SEATBOOKREQUESTRESPONSE.Visible = true;
                        TBL_RB_SEATBOOKREQUESTRESPONSE.DataSource = ds.Tables[2];
                        TBL_RB_SEATBOOKREQUESTRESPONSE.DataBind();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        divTBL_RB_CANCELLATIONDETAIL.Visible = true;
                        TBL_RB_CANCELLATIONDETAIL.DataSource = ds.Tables[3];
                        TBL_RB_CANCELLATIONDETAIL.DataBind();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        divTBL_RB_CANCELREQUESTRESPONSE.Visible = true;
                        TBL_RB_CANCELREQUESTRESPONSE.DataSource = ds.Tables[4];
                        TBL_RB_CANCELREQUESTRESPONSE.DataBind();
                    }
                }
                if (CMDTYPE == "H")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divT_HTL_HotelBookingHeader.Visible = true;
                        T_HTL_HotelBookingHeader.DataSource = ds.Tables[0];
                        T_HTL_HotelBookingHeader.DataBind();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        divT_HTL_HotelDetail.Visible = true;
                        T_HTL_HotelDetail.DataSource = ds.Tables[1];
                        T_HTL_HotelDetail.DataBind();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        divT_HTL_GuestDetail.Visible = true;
                        T_HTL_GuestDetail.DataSource = ds.Tables[2];
                        T_HTL_GuestDetail.DataBind();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        divT_HTL_RoomDetail.Visible = true;
                        T_HTL_RoomDetail.DataSource = ds.Tables[3];
                        T_HTL_RoomDetail.DataBind();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        divT_HTL_Cancellation.Visible = true;
                        T_HTL_Cancellation.DataSource = ds.Tables[4];
                        T_HTL_Cancellation.DataBind();
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        divT_HTL_HotelBookingLog.Visible = true;
                        T_HTL_HotelBookingLog.DataSource = ds.Tables[5];
                        T_HTL_HotelBookingLog.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        txt_OrderId.Text = "";
        txt_PNR.Text = "";
    }
}