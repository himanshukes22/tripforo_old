using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class PaymentGateway : System.Web.UI.Page
{
    string workingKey = "";
    public string strAccessCode = "";
    public string strEncRequest = "";
    public string strCCAveUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null && Convert.ToString(Session["UID"]) != "")
            {
                PG.PaymentGateway objPg = new PG.PaymentGateway();
                DataSet ds = new DataSet();

                string ObTid = Request.QueryString["OBTID"];
                string IbTid = Request.QueryString["IBTID"];
                string Ft = Request.QueryString["FT"];
                ds = objPg.GetPgRequestAndCredential(ObTid);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    strCCAveUrl = Convert.ToString(ds.Tables[0].Rows[0]["ProviderUrl"]);
                    strAccessCode = Convert.ToString(ds.Tables[0].Rows[0]["MERCHANT_PSWD"]);
                    strEncRequest = Convert.ToString(ds.Tables[1].Rows[0]["EncRequest"]);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("Login.aspx?reason=Session TimeOut", false);
            }
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
        //Response.Redirect("PaymentSucess.aspx?OBTID=" + ObTid + "&IBTID=" + IbTid + "&FT=" + Ft, false);
        //Response.Redirect("PaymentSucess.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
        //Response.Redirect("../wait.aspx?OBTID=" & ViewState("OBTrackId") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
    }

    public DataSet GetPgRequestAndCredential(string TrackId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        DataSet ds = new DataSet();
        string Provider = Convert.ToString(ConfigurationManager.AppSettings["PgProvider"]);
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("SpInsertPaymentDetails", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@TrackId", TrackId);
            adp.SelectCommand.Parameters.AddWithValue("@Provider", Provider);
            //adp.SelectCommand.Parameters.AddWithValue("@Action", "getcrd");
            adp.SelectCommand.Parameters.AddWithValue("@Action", "GetPgCrd");
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
        return ds;
    }

}