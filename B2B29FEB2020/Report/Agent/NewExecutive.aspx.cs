using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class NewExecutive : System.Web.UI.Page
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
        //if (Session["User_Type"].ToString().ToUpper() != "ADMIN")
        //{
        //    Response.Redirect("~/Login.aspx");
        //}

        if (!string.IsNullOrEmpty(Convert.ToString(Session["UID"])))
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogInfo(ex);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    public DataTable GetAirline()
    {
        DataTable dt = new DataTable();
        try
        {
            adap = new SqlDataAdapter("SP_GetAirlinenames", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adap.SelectCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 30);
            //adap.SelectCommand.Parameters["@Msg"].Direction = ParameterDirection.Output;
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

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            #region Insert            
            string UserId = "", Password = "", OwnerId = "", AgencyId = "", Name = "", Mobile = "", Email="", Address="", UserType="", RoleType="";
            string Status = "false", Flight = "false", Hotel = "false", Bus = "false", Rail = "false", Cab = "false", Holidays = "false", GiftCard = "false", HomeStay = "false";

            UserId = txtemail.Text.Trim();
            Password = txtpassword.Text;
            OwnerId = Convert.ToString(Session["UID"]);
            AgencyId = Convert.ToString(Session["UID"]);
            Name = txtemail.Text;
            Mobile = txtmobile.Text;
            Email = txtemail.Text.Trim();
            Address = txtAddress.Text.Trim();
            UserType = "STAFF";
            RoleType = "EXEC";

            if (string.IsNullOrEmpty(Email.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Enter Email Id!!');", true);
                return;
            }
            if (string.IsNullOrEmpty(Password.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Enter Password!!');", true);
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Enter Name!!');", true);
                return;
            }
            if (string.IsNullOrEmpty(Mobile.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Enter Mobile No !!');", true);
                return;
            }
            

            for (int i = 0; i < CheckBoxServiceType.Items.Count; i++)
            {
                string str = "";
                if (CheckBoxServiceType.Items[i].Selected == true)// getting selected value from CheckBox List  
                {
                    #region Active Service
                    if (CheckBoxServiceType.Items[i].Text== "Login")
                    {
                        Status = "true";  //CheckBoxServiceType.Items[i].Text
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Flight")
                    {
                        Flight = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Hotel")
                    {
                        Hotel = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Bus")
                    {
                        Bus = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Rail")
                    {
                        Rail = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Cab")
                    {
                        Cab = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "Holidays")
                    {
                        Holidays = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "GiftCard")
                    {
                        GiftCard = "true";
                    }
                    if (CheckBoxServiceType.Items[i].Text == "HomeStay")
                    {
                        HomeStay = "true";
                    }
                    #endregion

                    str += CheckBoxServiceType.Items[i].Text + " ," + "<br/>"; // add selected Item text to the String .  
                }
            }
           string ActionType = "insert";
            int Id = 0;
            string CheckBalance = "false";
            int flag = InsertAndUpdateRecords(UserId,Password,OwnerId,AgencyId,Name,Mobile,Email,Address, UserType, RoleType, Status, Flight,  Hotel, Bus,  Rail,  Cab,  Holidays,  GiftCard, HomeStay, ActionType,Id, CheckBalance);
            if (flag > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Added successfully.');window.location='NewExecutive.aspx'; ", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('try again.');window.location='NewExecutive.aspx'; ", true);
            }
            #endregion

        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex.Message + "');window.location='NewExecutive.aspx'; ", true);
            return;
        }

    }
    private int InsertAndUpdateRecords(string UserId, string Password, string OwnerId, string AgencyId, string Name, string Mobile, string Email, string Address, string UserType, 
        string RoleType, string Status, string Flight, string Hotel, string Bus, string Rail, string Cab, string Holidays, string GiftCard, string HomeStay, string ActionType,int Id, string CheckBalance)
    {
        int flag = 0;
        string CreatedBy = Convert.ToString(Session["UID"]);
        //string ActionType = "insert";
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_AgentStaffMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@OwnerId", OwnerId);
            cmd.Parameters.AddWithValue("@AgencyId", AgencyId);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            cmd.Parameters.AddWithValue("@RoleType", RoleType);
            cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(Status));
            cmd.Parameters.AddWithValue("@Flight", Convert.ToBoolean(Flight));
            cmd.Parameters.AddWithValue("@Hotel", Convert.ToBoolean(Hotel));
            cmd.Parameters.AddWithValue("@Bus", Convert.ToBoolean(Bus));
            cmd.Parameters.AddWithValue("@Rail", Convert.ToBoolean(Rail));
            cmd.Parameters.AddWithValue("@Cab", Convert.ToBoolean(Cab));
            cmd.Parameters.AddWithValue("@Holidays", Convert.ToBoolean(Holidays));
            cmd.Parameters.AddWithValue("@GiftCard", Convert.ToBoolean(GiftCard));
            cmd.Parameters.AddWithValue("@HomeStay", Convert.ToBoolean(HomeStay));
            cmd.Parameters.AddWithValue("@CheckBalance", Convert.ToBoolean(CheckBalance));
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@ActionType", ActionType);
            cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100);
            cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
            if (con.State == ConnectionState.Closed)
                con.Open();
            flag = cmd.ExecuteNonQuery();
            con.Close();
          string  msgout = cmd.Parameters["@Msg"].Value.ToString();
        }
        catch (Exception ex)
        {
            con.Close();
            clsErrorLog.LogInfo(ex);
        }
        return flag;

    }
    public void BindGrid()
    {
        try
        {
            grd_P_IntlDiscount.DataSource = GetRecord();
            grd_P_IntlDiscount.DataBind();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    public DataTable GetRecord()
    {
        DataTable dt = new DataTable();
        try
        {
            string CreatedBy = Convert.ToString(Session["UID"]);

            adap = new SqlDataAdapter("Sp_AgentStaffMaster", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;            
            adap.SelectCommand.Parameters.AddWithValue("@OwnerId", CreatedBy);
            adap.SelectCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            adap.SelectCommand.Parameters.AddWithValue("@ActionType", "select");
            adap.SelectCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 100);
            adap.SelectCommand.Parameters["@Msg"].Direction = ParameterDirection.Output;
            //if (ddl_ptype.SelectedValue == "ALL" && ddl_Pairline.SelectedValue == "ALL")
            //{
            //    adap.SelectCommand.Parameters.AddWithValue("@ActionType", "select");
            //}
            //else
            //{
            //    adap.SelectCommand.Parameters.AddWithValue("@ActionType", "AIRLINEWISE");
            //}
            adap.Fill(dt);
            string msgout = adap.SelectCommand.Parameters["@Msg"].Value.ToString();
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
            string UserId = "", Password = "", OwnerId = "", AgencyId = "", Name = "", Mobile = "", Email = "", Address = "", UserType = "", RoleType = "";
            string Status = "false", Flight = "false", Hotel = "false", Bus = "false", Rail = "false", Cab = "false", Holidays = "false", GiftCard = "false", HomeStay = "false", CheckBalance= "false";


            int result = 0;
            Label lblSNo = (Label)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lblId"));
            int Id = Convert.ToInt32(lblSNo.Text.Trim().ToString());          

            TextBox txtGrdPassword = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txtGrdPassword"));
            Password = Convert.ToString(txtGrdPassword.Text);
            if (string.IsNullOrEmpty(Password))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter Password!');", true);
                return;
            }

            TextBox txtGrdName = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txtGrdName"));
            Name = Convert.ToString(txtGrdName.Text);
            if (string.IsNullOrEmpty(Name))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter name!');", true);
                return;
            }
            TextBox txtGrdMobile = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txtGrdMobile"));
            Mobile = Convert.ToString((txtGrdMobile.Text).Trim());
            if (string.IsNullOrEmpty(Mobile))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter Mobile!');", true);
                return;
            }


            TextBox txtGrdEmail = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txtGrdEmail"));
            Email = Convert.ToString((txtGrdEmail.Text).Trim());
            if (string.IsNullOrEmpty(Email))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter email id!');", true);
                return;
            }

            TextBox txtGrdAddress = (TextBox)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("txtGrdAddress"));
            Address = Convert.ToString(txtGrdAddress.Text);

            DropDownList ddl_Active = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_IsActive");
            Status = ddl_Active.SelectedValue;
            DropDownList ddl_Flight = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_Flight");
            Flight = ddl_Flight.SelectedValue;
            DropDownList ddl_Hotel = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_Hotel");
            Hotel = ddl_Hotel.SelectedValue;
            DropDownList ddl_Bus = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_Bus");
            Bus = ddl_Bus.SelectedValue;
            DropDownList ddl_CheckBalance = (DropDownList)grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("ddl_CheckBalance");
            CheckBalance = ddl_CheckBalance.SelectedValue;

            string ActionType = "GRIDUPDATE";
            result = InsertAndUpdateRecords(UserId, Password, OwnerId, AgencyId, Name, Mobile, Email, Address, UserType, RoleType, Status, Flight, Hotel, Bus, Rail, Cab, Holidays, GiftCard, HomeStay, ActionType,Id, CheckBalance);
            //result = UpdateRecords(Id, CodeType, D_T_Code, AppliedOn, IsActive, FltNo, OrginAirport, DestAirport, OrginCountry, DestCountry, IdType);
            grd_P_IntlDiscount.EditIndex = -1;
            BindGrid();
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(' Record successfully updated.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('try again.');", true);
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('" + ex.Message + "');", true);
        }
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int flag = 0;
            try
            {
                Label lblSNo = (Label)(grd_P_IntlDiscount.Rows[e.RowIndex].FindControl("lblId"));
                int Id = Convert.ToInt32(lblSNo.Text.Trim().ToString());
                SqlCommand cmd = new SqlCommand("Sp_AgentStaffMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["UID"]));
                cmd.Parameters.AddWithValue("@ActionType", "delete");
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
            clsErrorLog.LogInfo(ex);
        }
    }


    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_P_IntlDiscount.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    
}