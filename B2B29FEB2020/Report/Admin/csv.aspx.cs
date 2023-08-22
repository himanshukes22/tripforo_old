using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class csv : System.Web.UI.Page
{
    string datetofind = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == true)
            {
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(ctrlname))
                {
                    if (ctrlname.Contains("lbtn"))
                    {
                        SetFiles();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["1stdata"] = null;
        List<loginhistory> objlogin1 = new List<loginhistory>();
        //objlogin1 = (List<loginhistory>)Session["lstdata"];
        try
        {
            ////DataTable dt = ((DataSet)Session["CSVDataSet"]).Tables[0];
            DataTable dt = ((DataSet)Session["CSVDataSet"]).Tables[0];

            var query = dt.AsEnumerable().Where(p => p.Field<string>("Path") == ((DropDownList)gvDetails.HeaderRow.FindControl("dd2FilterTypeLine")).SelectedItem.Text).Distinct();
            gvDetails.DataSource = query;
            gvDetails.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void xyz_TextChanged(object sender, EventArgs e)
    {
        SetFiles();
    }

    public void lbtn_Command(object sender, CommandEventArgs e)
    {
        string server = "", fileexns = "", sfileNmae = "";
        int index;
        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        sfileNmae = milliseconds + e.CommandArgument.ToString();  //.csv file name modify            
        var filename = System.Configuration.ConfigurationManager.AppSettings["LOGS_LOC"].ToString() + "\\" + datetofind + "\\" + e.CommandArgument.ToString();

        var connString = string.Format(
            @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
            Path.GetDirectoryName(filename)
        );

        try
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();
                //var query = "SELECT * FROM [" + Path.GetFileName(filename) + "] where Path='/login.aspx'";
                var query = "SELECT * FROM [" + Path.GetFileName(filename) + "]";
                using (var adapter = new OleDbDataAdapter(query, conn))
                {
                    var ds = new DataSet("CSV File");
                    adapter.Fill(ds);

                    List<loginhistory> objlogin = new List<loginhistory>();

                    Label2.Text = "Total no of hits: " + Convert.ToString(ds.Tables[0].Rows.Count);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objlogin.Add(new loginhistory
                        {
                            AUTH_TYPE = Convert.ToString(ds.Tables[0].Rows[i]["AUTH_TYPE"]),
                            AUTH_USER = Convert.ToString(ds.Tables[0].Rows[i]["AUTH_USER"]),
                            CITY = Convert.ToString(ds.Tables[0].Rows[i]["CITY"]),
                            COUNTRY_CODE = Convert.ToString(ds.Tables[0].Rows[i]["COUNTRY_CODE"]),
                            COUNTRY_NAME = Convert.ToString(ds.Tables[0].Rows[i]["COUNTRY_NAME"]),
                            DNS = Convert.ToString(ds.Tables[0].Rows[i]["DNS"]),
                            DOMAIN = Convert.ToString(ds.Tables[0].Rows[i]["DOMAIN"]),
                            DOMAIN_NAME = Convert.ToString(ds.Tables[0].Rows[i]["DOMAIN_NAME"]),
                            HTTP_HOST = Convert.ToString(ds.Tables[0].Rows[i]["HTTP_HOST"]),
                            ISP_NAME = Convert.ToString(ds.Tables[0].Rows[i]["ISP_NAME"]),
                            LATITUDE = Convert.ToString(ds.Tables[0].Rows[i]["LATITUDE"]),
                            LONGITUDE = Convert.ToString(ds.Tables[0].Rows[i]["LONGITUDE"]),
                            PageView = Convert.ToString(ds.Tables[0].Rows[i]["PageView"]),
                            Path = Convert.ToString(ds.Tables[0].Rows[i]["Path"]),
                            REGION = Convert.ToString(ds.Tables[0].Rows[i]["REGION"]),
                            REQUEST_METHOD = Convert.ToString(ds.Tables[0].Rows[i]["REQUEST_METHOD"]),
                            SCRIPT_NAME = Convert.ToString(ds.Tables[0].Rows[i]["SCRIPT_NAME"]),
                            SERVER_NAME = Convert.ToString(ds.Tables[0].Rows[i]["SERVER_NAME"]),
                            SERVER_PORT = Convert.ToString(ds.Tables[0].Rows[i]["SERVER_PORT"]),
                            SessionID = Convert.ToString(ds.Tables[0].Rows[i]["SessionID"]),
                            SOURCE = Convert.ToString(ds.Tables[0].Rows[i]["SOURCE"]),
                            TIME_ZONE = Convert.ToString(ds.Tables[0].Rows[i]["TIME_ZONE"]),
                            Username = Convert.ToString(ds.Tables[0].Rows[i]["Username"]),
                            VISITOR_IPADDR = Convert.ToString(ds.Tables[0].Rows[i]["VISITOR_IPADDR"]),
                            VISTING_TIME = Convert.ToString(ds.Tables[0].Rows[i]["VISTING_TIME"]),
                            ZIP_CODE = Convert.ToString(ds.Tables[0].Rows[i]["ZIP_CODE"]),
                        });
                    }
                    //Session["lstdata"] = objlogin;

                    var noOfvisitors = objlogin.Select(x => x.VISITOR_IPADDR).Distinct().Count();  //no of unique hits
                    Label1.Text = "Total no of unique IP hits : " + noOfvisitors.ToString();             //no of unique hits   

                    //var noOfTotalvisitors = objlogin.Select(x => x.VISITOR_IPADDR).Count();  //no of unique hits
                    //Label4.Text = "Total unique hits : " + noOfTotalvisitors.ToString();            //no of unique hits   


                    var noOfsource = objlogin.Select(x => x.SOURCE).Distinct().Count();
                    label3.Text = "total no of unique source hits: " + noOfsource.ToString();

                    var noofpath = objlogin.Select(x => x.Path).Distinct().Count();
                    label4.Text = "total no of unique path :" + noofpath.ToString();


                    //Label2.Text = "Total no of hits: " + (objlogin.Count);

                    //ddlpath.DataSource = objlogin.Select(o => o.Path).Distinct();
                    //ddlpath.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    public void SetFiles()
    {
        string selecteddate = xyz.Text.ToString(); string[] abc = { };
        string server = "", fileexns = "", sfileNmae = "";
        int index;
        maindiv.Visible = true;
        string strfile = "";
        datetofind = selecteddate;
        try
        {
            if (Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["LOGS_LOC"].ToString() + "\\" + selecteddate))
            {
                var file = Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["LOGS_LOC"].ToString() + "\\" + selecteddate);
                abc = new string[file.Length];
                pnlNewFiles.Controls.Clear();
                for (int j = 0; j < file.Length; j++)
                {
                    int len = file[j].Split('\\').Length;
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtn" + j;
                    lbtn.Text = file[j].Split('\\')[len - 1];
                    lbtn.EnableViewState = true;
                    lbtn.CommandName = "Update";
                    lbtn.CommandArgument = lbtn.Text;
                    lbtn.EnableViewState = true;
                    lbtn.Attributes.Add("onchange", "javascript:setTimeout('__doPostBack(\'btnUpload\',\'\')', 0)");
                    lbtn.Attributes.Add("OnCommand", "lbtn_Command");
                    lbtn.Command += new CommandEventHandler(lbtn_Command);
                    pnlNewFiles.Controls.Add(lbtn);

                    Label lblBr = new Label();
                    lblBr.Text = "<br/>";
                    pnlNewFiles.Controls.Add(lblBr);
                    pnlNewFiles.EnableViewState = true;
                    Label lbl = new Label();
                    lbl.Text = "<br/>";
                    maindiv.Controls.Add(lbl);
                };
            }
        }
        catch (Exception ex)
        {
        }
    }
}
public class loginhistory
{

    public string AUTH_TYPE { set; get; }
    public string AUTH_USER { set; get; }
    public string CITY { set; get; }
    public string COUNTRY_CODE { set; get; }
    public string COUNTRY_NAME { set; get; }
    public string DNS { set; get; }
    public string DOMAIN { set; get; }
    public string DOMAIN_NAME { set; get; }
    public string HTTP_HOST { set; get; }
    public string ISP_NAME { set; get; }
    public string LATITUDE { set; get; }
    public string LONGITUDE { set; get; }
    public string PageView { set; get; }
    public string Path { set; get; }
    public string REGION { set; get; }
    public string REQUEST_METHOD { set; get; }
    public string SCRIPT_NAME { set; get; }
    public string SERVER_NAME { set; get; }
    public string SERVER_PORT { set; get; }
    public string SessionID { set; get; }
    public string SOURCE { set; get; }
    public string TIME_ZONE { set; get; }
    public string Username { set; get; }
    public string VISITOR_IPADDR { set; get; }
    public string VISTING_TIME { set; get; }
    public string ZIP_CODE { set; get; }





    public loginhistory()
    {
        //
        // TODO: Add constructor logic here
        //




    }
}
