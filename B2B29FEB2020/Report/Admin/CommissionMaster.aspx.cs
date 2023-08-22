using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections;
public partial class SprReports_Admin_CommissionMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (Session["UID"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (Session["User_Type"].ToString().ToUpper() != "DI")
        {
            Response.Redirect("~/Login.aspx");
        }
        try
        {
            if (!IsPostBack)
            {
                ddl_ptype.AppendDataBoundItems = true;
                ddl_ptype.Items.Clear();

                //Dim item As New ListItem("All Type", "0")
                //ddl_ptype.Items.Insert(0, item)
                ddl_ptype.DataSource = GetAllGroupType().Tables[0];
                ddl_ptype.DataTextField = "GroupType";
                ddl_ptype.DataValueField = "GroupType";
                ddl_ptype.DataBind();
                ddl_ptype.Items.Insert(0, new ListItem("-- Select Type --", "ALL"));

                ddl_Pairline.AppendDataBoundItems = true;
                ddl_Pairline.Items.Clear();
               // ddl_Pairline.Items.Insert(0, "--Select Airline--");
                ddl_Pairline.Items.Insert(0, new ListItem("-- Select Airline --", "ALL"));
                ddl_Pairline.DataSource = GetAirline();
                ddl_Pairline.DataTextField = "AL_Name";
                ddl_Pairline.DataValueField = "AL_Code";
                ddl_Pairline.DataBind();
                //ddl_Pairline.Items.Add(new ListItem("ALL_TYPE", "All Type"));
                
                BindGrid();
            }
           

        }
        catch (Exception ex)
        {
        }

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_ptype.SelectedValue == "ALL" && ddl_Pairline.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please select group type and airline!!');", true);
            }
            else if (ddl_ptype.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please select group type!!');", true);                 
            }
            else if (ddl_Pairline.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please select airline!!');", true);                 
            }
            else {             
         #region Insert
            string BookingFromDate = Convert.ToString(Request["From"]);
        string BookingToDate = Convert.ToString(Request["To"]);
        string OnwardTravelFromDate = Convert.ToString(Request["OTFromDate"]);
        string OnwardTravelToDate = Convert.ToString(Request["OTToDate"]);
        string ReturnTravelFromDate = Convert.ToString(Request["RTFromDate"]);
        string ReturnTravelToDate = Convert.ToString(Request["RTToDate"]);
        string BookingClassInclude = Convert.ToString(TxtBookingClassIn.Text);
        string BookingClassExclude = Convert.ToString(TxtBookingClassEx.Text);
        string FareBasisInclude = Convert.ToString(TxtFareBasisIn.Text);
        string FareBasisExclude = Convert.ToString(TxtFareBasisEx.Text);
        string OrginAirportInclude = Convert.ToString(TxtOrginAirportIn.Text);
        string OrginAirportExclude = Convert.ToString(TxtOrginAirportEx.Text);
        string DestinationAirportInclude = Convert.ToString(TxtDestAirportIn.Text);
        string DestinationAirportExclude = Convert.ToString(TxtDestAirportEx.Text);
        string FlightNoInclude = Convert.ToString(TxtFlightNoIn.Text);
        string FlightNoExclude = Convert.ToString(TxtFlightNoEx.Text);
        string OperatingCarrierInclude = Convert.ToString(TxtOperatingCarrierIn.Text);
        string OperatingCarrierExclude = Convert.ToString(TxtOperatingCarrierEx.Text);
        string MarketingCarrierInclude = Convert.ToString(TxtMarketingCarrierIn.Text);
        string MarketingCarrierExclude = Convert.ToString(TxtMarketingCarrierEx.Text);

        string AirlineCode = Convert.ToString(ddl_Pairline.SelectedValue);
        string TripType = Convert.ToString(DdlTripType.SelectedValue);

        string AirlineName = Convert.ToString(ddl_Pairline.SelectedItem.Text);
        string TripTypeName = Convert.ToString(DdlTripType.SelectedItem.Text);
        string FareTypeName = Convert.ToString(DdlFareType.SelectedItem.Text);
            //AirlineName,TripTypeName,FareTypeName

        foreach (int i in ListCabinClassIn.GetSelectedIndices())
        {
            // ListBox1.Items[i] ...
        }

        System.Collections.Generic.List<string> selectedItemsList = new System.Collections.Generic.List<string>();
        foreach (ListItem item in ListCabinClassIn.Items)
        {
            if (item.Selected)
            {
                selectedItemsList.Add(item.Value);
            }
        }
        string CabinClassInclude = string.Join(",", selectedItemsList.ToArray());
        //string CabinClassInclude = Convert.ToString(ListCabinClassIn.SelectedValue);

        System.Collections.Generic.List<string> selectedItemsList2 = new System.Collections.Generic.List<string>();
        //foreach (ListItem item in ListCabinClassEx.Items)
        //{
        //    if (item.Selected)
        //    {
        //        selectedItemsList2.Add(item.Value);
        //    }
        //}
        string CabinClassExclude = ""; //string.Join(",", selectedItemsList2.ToArray());
        //string CabinClassExclude = Convert.ToString(ListCabinClassEx.SelectedValue);
        string FareType = Convert.ToString(DdlFareType.SelectedValue);
        string BookingChannel = Convert.ToString(DdlBookingChannel.SelectedValue);
        string Active = "";
        string Status = "";
        string CreatedBy = Convert.ToString(Session["UID"]);
        string ActionType = "insert";

        decimal CommisionOnBasic = 0;       
        decimal CommissionOnYq = 0;
        decimal CommisionOnBasicYq = 0;
        decimal PlbOnBasic = 0;
        decimal PlbOnBasicYq = 0;            
        string GroupType = ddl_ptype.SelectedValue;//HiddenAlltype.Value == "All Type" ? "0" : ddl_ptype.SelectedValue;
        if (!string.IsNullOrEmpty(Convert.ToString(txt_basic.Text)))
        {
                CommisionOnBasic =Convert.ToDecimal(txt_basic.Text);
        }
        if (!string.IsNullOrEmpty(Convert.ToString(txt_CYQ.Text)))
        {
            CommissionOnYq = Convert.ToDecimal(txt_CYQ.Text);
        }
        if (!string.IsNullOrEmpty(Convert.ToString(txt_CYB.Text)))
        {
            CommisionOnBasicYq = Convert.ToDecimal(txt_CYB.Text);
        }
        if (!string.IsNullOrEmpty(Convert.ToString(txt_Pbasic.Text)))
        {
            PlbOnBasic = Convert.ToDecimal(txt_Pbasic.Text);
        }
        if (!string.IsNullOrEmpty(Convert.ToString(txt_Pyqb.Text)))
        {
            PlbOnBasicYq = Convert.ToDecimal(txt_Pyqb.Text);
        }
        decimal CashBackAmt = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(TxtCashBack.Text)))
        {
            CashBackAmt = Convert.ToDecimal(TxtCashBack.Text);
        }
        string OriginCountryInclude = Convert.ToString(TxtOriginCountryIn.Text);
        string OriginCountryExclude = Convert.ToString(TxtOriginCountryEx.Text);
        string DestCountryInclude = Convert.ToString(TxtDestCountryIn.Text);
        string DestCountryExclude = Convert.ToString(TxtDestCountryEx.Text);
        string RestrictionOn = Convert.ToString(DdlRestriction.SelectedValue);
        string PPPType = Convert.ToString(DdlPPPType.SelectedValue);
        int flag = InsertCommissionMaster(BookingFromDate, BookingToDate, OnwardTravelFromDate, OnwardTravelToDate, ReturnTravelFromDate, ReturnTravelToDate, BookingClassInclude,
                            BookingClassExclude, FareBasisInclude, FareBasisExclude, OrginAirportInclude, OrginAirportExclude, DestinationAirportInclude, DestinationAirportExclude,
                            FlightNoInclude, FlightNoExclude, OperatingCarrierInclude, OperatingCarrierExclude, MarketingCarrierInclude, MarketingCarrierExclude, CabinClassInclude,
                            CabinClassExclude, FareType, BookingChannel, Active, Status, CreatedBy, ActionType, GroupType, CommisionOnBasic, CommisionOnBasicYq, CommissionOnYq,
                            PlbOnBasic, PlbOnBasicYq, AirlineCode, TripType, OriginCountryInclude, OriginCountryExclude, DestCountryInclude, DestCountryExclude, AirlineName, TripTypeName, FareTypeName, CashBackAmt, RestrictionOn, PPPType);
       

        if (flag > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Added successfully.');window.location='CommissionMaster.aspx'; ", true);
        }
        else {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('try again.');window.location='CommissionMaster.aspx'; ", true);
        }
            #endregion
            }
            }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex .Message+ "');window.location='CommissionMaster.aspx'; ", true);
        }

    }

    private int InsertCommissionMaster(string BookingFromDate,string BookingToDate,string OnwardTravelFromDate,string OnwardTravelToDate,string ReturnTravelFromDate,string ReturnTravelToDate,string BookingClassInclude,string 
                         BookingClassExclude,string FareBasisInclude,string FareBasisExclude,string OrginAirportInclude,string OrginAirportExclude,string DestinationAirportInclude,string DestinationAirportExclude,string 
                         FlightNoInclude,string FlightNoExclude,string OperatingCarrierInclude,string OperatingCarrierExclude,string MarketingCarrierInclude,string MarketingCarrierExclude,string CabinClassInclude,string 
                         CabinClassExclude,string FareType,string BookingChannel,string Active,string Status,string CreatedBy,string ActionType,string GroupType,decimal CommisionOnBasic,decimal CommisionOnBasicYq,decimal CommissionOnYq,decimal PlbOnBasic,decimal PlbOnBasicYq,string AirlineCode,string TripType,
       string OriginCountryInclude, string OriginCountryExclude, string DestCountryInclude, string DestCountryExclude, string AirlineName, string TripTypeName, string FareTypeName, decimal CashBackAmt, string RestrictionOn, string PPPType)
    {
        int flag = 0;
        try
        {
            //SqlCommand cmd = new SqlCommand("SpFlightNewCommissionFilter", con);
            SqlCommand cmd = new SqlCommand("UspFlightCommissionFilter_forStockist_v1_NM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookingFromDate", BookingFromDate);
	        cmd.Parameters.AddWithValue("@BookingToDate", BookingToDate);
	        cmd.Parameters.AddWithValue("@OnwardTravelFromDate", OnwardTravelFromDate);
            cmd.Parameters.AddWithValue("@OnwardTravelToDate", OnwardTravelToDate);
            cmd.Parameters.AddWithValue("@ReturnTravelFromDate", ReturnTravelFromDate);
            cmd.Parameters.AddWithValue("@ReturnTravelToDate", ReturnTravelToDate);
            cmd.Parameters.AddWithValue("@BookingClassInclude", BookingClassInclude);
            cmd.Parameters.AddWithValue("@BookingClassExclude", BookingClassExclude);
            cmd.Parameters.AddWithValue("@FareBasisInclude", FareBasisInclude);
            cmd.Parameters.AddWithValue("@FareBasisExclude", FareBasisExclude);
            cmd.Parameters.AddWithValue("@OrginAirportInclude", OrginAirportInclude);
            cmd.Parameters.AddWithValue("@OrginAirportExclude", OrginAirportExclude);
            cmd.Parameters.AddWithValue("@DestinationAirportInclude", DestinationAirportInclude);
            cmd.Parameters.AddWithValue("@DestinationAirportExclude", DestinationAirportExclude);
            cmd.Parameters.AddWithValue("@FlightNoInclude", FlightNoInclude);
            cmd.Parameters.AddWithValue("@FlightNoExclude", FlightNoExclude);
            cmd.Parameters.AddWithValue("@OperatingCarrierInclude", OperatingCarrierInclude);
            cmd.Parameters.AddWithValue("@OperatingCarrierExclude", OperatingCarrierExclude);
            cmd.Parameters.AddWithValue("@MarketingCarrierInclude", MarketingCarrierInclude);
            cmd.Parameters.AddWithValue("@MarketingCarrierExclude", MarketingCarrierExclude);
            cmd.Parameters.AddWithValue("@CabinClassInclude", CabinClassInclude);
            cmd.Parameters.AddWithValue("@CabinClassExclude", CabinClassExclude);
            cmd.Parameters.AddWithValue("@FareType", FareType);
            cmd.Parameters.AddWithValue("@BookingChannel", BookingChannel);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@ActionType", ActionType);
            
            cmd.Parameters.AddWithValue("@GroupType", GroupType);
            cmd.Parameters.AddWithValue("@CommisionOnBasic", CommisionOnBasic);
            cmd.Parameters.AddWithValue("@CommisionOnBasicYq", CommisionOnBasicYq);
            cmd.Parameters.AddWithValue("@CommissionOnYq", CommissionOnYq);
            cmd.Parameters.AddWithValue("@PlbOnBasic", PlbOnBasic);
            cmd.Parameters.AddWithValue("@PlbOnBasicYq", PlbOnBasicYq);           
            cmd.Parameters.AddWithValue("@AirlineCode", AirlineCode);
            cmd.Parameters.AddWithValue("@TripType", TripType);

            cmd.Parameters.AddWithValue("@OriginCountryInclude", OriginCountryInclude);
            cmd.Parameters.AddWithValue("@OriginCountryExclude", OriginCountryExclude);
            cmd.Parameters.AddWithValue("@DestCountryInclude", DestCountryInclude);
            cmd.Parameters.AddWithValue("@DestCountryExclude", DestCountryExclude);
            
            cmd.Parameters.AddWithValue("@AirlineName", AirlineName);
            cmd.Parameters.AddWithValue("@TripTypeName", TripTypeName);
            cmd.Parameters.AddWithValue("@FareTypeName", FareTypeName);
            cmd.Parameters.AddWithValue("@CashBackAmt", CashBackAmt);
            cmd.Parameters.AddWithValue("@RestrictionOn", RestrictionOn);
            cmd.Parameters.AddWithValue("@PPPType", PPPType);
            cmd.Parameters.AddWithValue("@UserId", Session["UID"].ToString());
           
            if (con.State == ConnectionState.Closed)
                con.Open();
            flag = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch(Exception ex)
        {
            con.Close();            
        }
        return flag;

    }



    public DataTable GetAirline()
    {
        DataTable dt = new DataTable();       
        try
        {
            adap = new SqlDataAdapter("SP_GetAirlinenames", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.Fill(dt);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
        finally
        {
            adap.Dispose();
        }
        return dt;
    }
    
    protected void grd_P_IntlDiscount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd_P_IntlDiscount.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

    }

    protected void grd_P_IntlDiscount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd_P_IntlDiscount.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    
    protected void grd_P_IntlDiscount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {           
            //Label AirlineName = (Label)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lbl_airline");
            //Label AirlineCode = (Label)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lbl_airlinecode");
            //DropDownList CommClass = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_CommClass");
            //DropDownList PLBClass = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_PLBClass");
           

            int result = 0;
            Label lblSNo = (Label)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lblId"));
            int Id = Convert.ToInt16(lblSNo.Text.Trim().ToString());            
            TextBox TxtPlbOnBasic = (TextBox)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_PBasic");
            TextBox TxtPlbOnBasicYq = (TextBox)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_PBasicYQ");           
            TextBox TxtCommisionOnBasic = (TextBox)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_CBasic");
            TextBox TxtCommissionOnYq = (TextBox)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_CYQ");
            TextBox TxtCommisionOnBasicYQ = (TextBox)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_CBasicYQ");

            decimal CommisionOnBasic = 0;
            decimal CommissionOnYq = 0;
            decimal CommisionOnBasicYq = 0;
            decimal PlbOnBasic = 0;
            decimal PlbOnBasicYq = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(TxtCommisionOnBasic.Text)))
            {
                CommisionOnBasic = Convert.ToDecimal(TxtCommisionOnBasic.Text);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(TxtCommissionOnYq.Text)))
            {
                CommissionOnYq = Convert.ToDecimal(TxtCommissionOnYq.Text);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(TxtCommisionOnBasicYQ.Text)))
            {
                CommisionOnBasicYq = Convert.ToDecimal(TxtCommisionOnBasicYQ.Text);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(TxtPlbOnBasic.Text)))
            {
                PlbOnBasic = Convert.ToDecimal(TxtPlbOnBasic.Text);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(TxtPlbOnBasicYq.Text)))
            {
                PlbOnBasicYq = Convert.ToDecimal(TxtPlbOnBasicYq.Text);
            }
            TextBox TxtBookingFromDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_BookingFromDate"));
            string BookingFromDate= Convert.ToString(TxtBookingFromDate.Text);           
            TextBox TxtBookingToDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_BookingToDate"));
            string BookingToDate = Convert.ToString(TxtBookingToDate.Text);
            TextBox TxtOnwardTravelFromDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_OnwardTravelFromDate"));
            string OnwardTravelFromDate = Convert.ToString(TxtOnwardTravelFromDate.Text);
            TextBox TxtOnwardTravelToDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_OnwardTravelToDate"));
            string OnwardTravelToDate = Convert.ToString(TxtOnwardTravelToDate.Text);
            TextBox TxtReturnTravelFromDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_ReturnTravelFromDate"));
            string ReturnTravelFromDate = Convert.ToString(TxtReturnTravelFromDate.Text);
            TextBox TxtReturnTravelToDate = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_ReturnTravelToDate"));
            string ReturnTravelToDate = Convert.ToString(TxtReturnTravelToDate.Text);

            TextBox Txt_CashBackAmt = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txt_CashBackAmt"));
            decimal CashBackAmt = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(Txt_CashBackAmt.Text)))
            {
                CashBackAmt = Convert.ToDecimal(Txt_CashBackAmt.Text);
            }
            DropDownList DDLRestrictionOn = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_RestrictionOn");
            string RestrictionOn = DDLRestrictionOn.SelectedValue;

            string PPPType = "";
            DropDownList Ddl_PPPType = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("Ddl_PPPType");
            PPPType = Ddl_PPPType.SelectedValue;            

            result = UpdateCommissionMaster(Id, BookingFromDate, BookingToDate, OnwardTravelFromDate, OnwardTravelToDate, ReturnTravelFromDate, ReturnTravelToDate,
                         CommisionOnBasic, CommisionOnBasicYq, CommissionOnYq, PlbOnBasic, PlbOnBasicYq, CashBackAmt, RestrictionOn, PPPType);

            grd_P_IntlDiscount.EditIndex = -1;
            BindGrid();

            if (result >0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(' Record successfully updated.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('tryagain.');", true);
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('"+ex.Message+"');", true);
        }
    }


    public void BindGrid()
    {
        try
        {
            //if (ddl_ptype.SelectedValue != "Select Type")
            //{
            grd_P_IntlDiscount.DataSource = GetCommissionRecord(ddl_ptype.SelectedValue);
            grd_P_IntlDiscount.DataBind();
            //}

        }
        catch (Exception ex)
        {
        }

    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }


    public DataTable GetCommissionRecord(string groupType)
    {
        DataTable dt = new DataTable();
        try
        {    
            // ddl_ptype.SelectedValue == "ALL" && ddl_Pairline.SelectedValue == "ALL"
            adap = new SqlDataAdapter("UspFlightCommissionFilter_forStockist_v1_NM", con);
            //adap = new SqlDataAdapter("SpFlightNewCommissionFilter", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@GroupType", ddl_ptype.SelectedValue);
            adap.SelectCommand.Parameters.AddWithValue("@AirlineCode", ddl_Pairline.SelectedValue);
            adap.SelectCommand.Parameters.AddWithValue("@UserId", Session["UID"].ToString());
            if (ddl_ptype.SelectedValue == "ALL" && ddl_Pairline.SelectedValue == "ALL")
            {
                adap.SelectCommand.Parameters.AddWithValue("@ActionType", "select");
            }
            else
            {
                adap.SelectCommand.Parameters.AddWithValue("@ActionType", "AIRLINEWISE");
            }
                       
            adap.Fill(dt);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
        finally
        {
            adap.Dispose();
        }
        return dt;
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int flag = 0;
            try
            {
                Label lblSNo = (Label)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lblId"));
                int Id = Convert.ToInt16(lblSNo.Text.Trim().ToString());
                //string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                SqlCommand cmd = new SqlCommand("UspFlightCommissionFilter_forStockist_v1_NM", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ActionType", "delete");
                cmd.Parameters.AddWithValue("@UserId", Session["UID"].ToString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                flag = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (SqlException ex)
            {
                con.Close();
                clsErrorLog.LogInfo(ex);
            }

            BindGrid();
            if (flag > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(' Record successfully deleted.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(' Problen in deleting record.');", true);
            }
        }
        catch (Exception ex)
        {
            //ErrorLogTrace.WriteErrorLog(ex, "Flight")
            clsErrorLog.LogInfo(ex);
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                {
                    button.Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
                }
            }
        }
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_P_IntlDiscount.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    private int UpdateCommissionMaster(int Id, string BookingFromDate, string BookingToDate, string OnwardTravelFromDate, string OnwardTravelToDate, string ReturnTravelFromDate, string ReturnTravelToDate,
                         decimal CommisionOnBasic, decimal CommisionOnBasicYq, decimal CommissionOnYq, decimal PlbOnBasic, decimal PlbOnBasicYq, decimal CashBackAmt, string RestrictionOn, string PPPType)
    {
        int flag = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("UspFlightCommissionFilter_forStockist_v1_NM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@BookingFromDate", BookingFromDate);
            cmd.Parameters.AddWithValue("@BookingToDate", BookingToDate);
            cmd.Parameters.AddWithValue("@OnwardTravelFromDate", OnwardTravelFromDate);
            cmd.Parameters.AddWithValue("@OnwardTravelToDate", OnwardTravelToDate);
            cmd.Parameters.AddWithValue("@ReturnTravelFromDate", ReturnTravelFromDate);
            cmd.Parameters.AddWithValue("@ReturnTravelToDate", ReturnTravelToDate);           
            cmd.Parameters.AddWithValue("@CommisionOnBasic", CommisionOnBasic);
            cmd.Parameters.AddWithValue("@CommissionOnYq", CommissionOnYq);
            cmd.Parameters.AddWithValue("@CommisionOnBasicYq", CommisionOnBasicYq);            
            cmd.Parameters.AddWithValue("@PlbOnBasic", PlbOnBasic);
            cmd.Parameters.AddWithValue("@PlbOnBasicYq", PlbOnBasicYq);
            cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["UID"]));
            cmd.Parameters.AddWithValue("@ActionType", "GRIDUPDATE");
            cmd.Parameters.AddWithValue("@CashBackAmt", CashBackAmt);
            cmd.Parameters.AddWithValue("@RestrictionOn", RestrictionOn);
            cmd.Parameters.AddWithValue("@PPPType", PPPType);
            cmd.Parameters.AddWithValue("@UserId", Session["UID"].ToString());
            //cmd.Parameters.AddWithValue("@BookingClassExclude", BookingClassExclude);
            //cmd.Parameters.AddWithValue("@FareBasisInclude", FareBasisInclude);
            //cmd.Parameters.AddWithValue("@FareBasisExclude", FareBasisExclude);
            //cmd.Parameters.AddWithValue("@OrginAirportInclude", OrginAirportInclude);
            //cmd.Parameters.AddWithValue("@OrginAirportExclude", OrginAirportExclude);
            //cmd.Parameters.AddWithValue("@DestinationAirportInclude", DestinationAirportInclude);
            //cmd.Parameters.AddWithValue("@DestinationAirportExclude", DestinationAirportExclude);
            //cmd.Parameters.AddWithValue("@FlightNoInclude", FlightNoInclude);
            //cmd.Parameters.AddWithValue("@FlightNoExclude", FlightNoExclude);
            //cmd.Parameters.AddWithValue("@OperatingCarrierInclude", OperatingCarrierInclude);
            //cmd.Parameters.AddWithValue("@OperatingCarrierExclude", OperatingCarrierExclude);
            //cmd.Parameters.AddWithValue("@MarketingCarrierInclude", MarketingCarrierInclude);
            //cmd.Parameters.AddWithValue("@MarketingCarrierExclude", MarketingCarrierExclude);
            //cmd.Parameters.AddWithValue("@CabinClassInclude", CabinClassInclude);
            //cmd.Parameters.AddWithValue("@CabinClassExclude", CabinClassExclude);
            //cmd.Parameters.AddWithValue("@FareType", FareType);
            //cmd.Parameters.AddWithValue("@BookingChannel", BookingChannel);
            //cmd.Parameters.AddWithValue("@Active", Active);
            //cmd.Parameters.AddWithValue("@Status", Status);            
            //cmd.Parameters.AddWithValue("@GroupType", GroupType);
            //cmd.Parameters.AddWithValue("@AirlineCode", AirlineCode);
            //cmd.Parameters.AddWithValue("@TripType", TripType);

            if (con.State == ConnectionState.Closed)
                con.Open();
            flag = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }
        return flag;

    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
        //DataSet ds = new DataSet();
        //ds.Tables.Add(dt);
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
       
        try
        {
            DataTable dt = new DataTable();
            dt = GetCommissionRecord(ddl_ptype.SelectedValue);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            STDom.ExportData(ds);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);

        }
    }
 public DataSet GetAllGroupType()
     {
    DataAccess objDataAcess = new DataAccess(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    Hashtable paramHashtable = new Hashtable();
    paramHashtable.Add("@distr", Session["UID"].ToString().Trim());
    return objDataAcess.ExecuteData<DataSet>(paramHashtable, true, "Sp_GetAllGroupType_stockist", 3);
}
}