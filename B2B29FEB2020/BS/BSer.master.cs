using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System;
using IPTracker;

public partial class BS_BSer : System.Web.UI.MasterPage
{
    private string id;
    private string usertype;
    private string servtype;
    private string typeid;
    private DataSet ds;
    private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    private SqlDataAdapter adap;
    private DataSet dsm;
    private Details det = new Details();
    protected void  // ERROR: Handles clauses are not supported in C#
    Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            //if (!IsPostBack)
            //{
            //if (!string.IsNullOrEmpty(Session["UID"].ToString()) && Session["UID"] != null && !string.IsNullOrEmpty(Session["UserType"].ToString()) && Session["UserType"] != null && !string.IsNullOrEmpty(Session["TypeID"].ToString()) && Session["TypeID"] != null)
            if (Session["UID"] != null && Session["UserType"] != null && Session["TypeID"] != null)
            {
                try
                {
                    StateCollection State = new StateCollection();
                    IPDetails objIP = new IPDetails();
                    State.SessionID = Session.SessionID;
                    State.Path = Request.CurrentExecutionFilePath;
                    State.Username = Session["UID"].ToString(); //Page.User.Identity.Name;
                    State.VISTING_TIME = DateTime.Now.ToString();
                    SessionTrack objST = new SessionTrack();
                    objST.Add(State, Request.CurrentExecutionFilePath);
                }
                catch (Exception ex)
                {
                }
                id = Session["UID"].ToString();
                servtype = "Bus";
                usertype = Session["UserType"].ToString();
                typeid = Session["TypeID"].ToString();
                if (usertype == "AD")
                {
                    lblagency.Text = Session["ADMINLogin"].ToString();
                    crdrow.Visible = false;
                    tr_AgencyID.Visible = false;
                }
                else if (usertype == "AC")
                {
                    lblagency.Text = "Accounts";
                    crdrow.Visible = false;
                    tr_AgencyID.Visible = false;
                }
                else if (usertype == "EC")
                {
                    lblagency.Text = Session["UID"].ToString();
                    crdrow.Visible = false;
                    tr_AgencyID.Visible = false;
                }
                else if (usertype == "SE")
                {
                    lblagency.Text = Session["UID"].ToString();
                    crdrow.Visible = false;
                    tr_AgencyID.Visible = false;
                }
                else if (usertype == "TA")
                {
                    ds = det.AgencyInfo(id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblagency.Text = ds.Tables[0].Rows[0]["Agency_Name"].ToString();
                        lblCamt.Visible = true;
                        lblCamt.Text = " INR " + Convert.ToDouble(ds.Tables[0].Rows[0]["crd_limit"].ToString());
                        td_AgencyID.InnerText = ds.Tables[0].Rows[0]["user_id"].ToString();
                        Session["AgencyName"] = lblagency.Text;
                        Session["AGTY"] = ds.Tables[0].Rows[0]["Agent_Type"].ToString();
                    }
                }

                if (Session["User_Type"] == "ACC" | Session["User_Type"] == "SALES")
                {
                    div_menu.Visible = false;
                    hypdeal.Visible = false;
                }
                if (Session["User_Type"] == "EXEC")
                {
                    div_Rail.Visible = false;
                    div_Utility.Visible = false;
                    div_Series.Visible = false;
                }

                if (Session["User_Type"] == "ADMIN")
                {
                    hypdeal.Visible = false;
                }
                if (typeid == "TA2")
                {
                    div_Rail.Visible = false;
                    divflt.Visible = false;
                    divhtl.Visible = false;
                    div_Series.Visible = false;
                    div_Utility.Visible = false;
                    hypdeal.Visible = false;
                }

                //If Session["User_Type"] = "A" Or Session["User_Type"] = "EXEC" Then
                //    div_menu.Visible = False
                //End If






            }
            else if (Session["UID"] == null && Session["UserType"] == null && Session["TypeID"] == null)
            {
                Response.Redirect("~/Login.aspx?reason=Session TimeOut");
            }
            ShowMenu();
            //RowMenu.Visible = False

            if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
            {
                Request.Browser.Adapters.Clear();
            }

            try
            {
                dsm = det.GetMarquueemsg(servtype);

                if (dsm.Tables[0].Rows.Count > 0)
                {

                    tdmarquee.InnerText = dsm.Tables[0].Rows[0]["Message"].ToString();


                }

            }
            catch (Exception ex)
            {
            }

        }
        //}
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    protected void  // ERROR: Handles clauses are not supported in C#
    lnklogout_Click1(object sender, EventArgs e)
    {
        try
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }
    public void ShowMenu()
    {
        try
        {
            adap = new SqlDataAdapter("GetURL", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@typeid", typeid);
            DataSet dset = new DataSet();
            adap.Fill(dset);
            adap.Dispose();
            XmlDataSource xmld = new XmlDataSource();
            xmld.ID = "XmlDataSource1";
            xmld.EnableCaching = false;
            dset.DataSetName = "Menus";
            dset.Tables[0].TableName = "abc";
            DataRelation relation = new DataRelation("ParentChild", dset.Tables["abc"].Columns["Page_ID"], dset.Tables["abc"].Columns["PageParent_ID"], true);
            relation.Nested = true;
            dset.Relations.Add(relation);
            xmld.Data = dset.GetXml();
            xmld.TransformFile = Server.MapPath("~/Transform.xslt");
            xmld.XPath = "MenuItems/MenuItem";
            Menu1.DataSource = xmld;
            Menu1.DataBind();
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
    }



}
