using System;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class CallingMidServiceAuto : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    //private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkedjson();
        CallingService();

    }

    protected void CallingService()
    {


        DataSet MidServiceGet = new DataSet();
        MidServiceGet = FetchMidService();

        for (int i = 0; i < MidServiceGet.Tables[0].Rows.Count; i++)
        {

            if (Convert.ToBoolean(MidServiceGet.Tables[0].Rows[i]["Active"]) == true)
            {
                for (int j = 0; j < Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["NoOfHowManyDays"].ToString()); j++)
                {

                    DateTime FromDate = Convert.ToDateTime(MidServiceGet.Tables[0].Rows[i]["FromDate_"].ToString());
                    DateTime dt = FromDate;
                    dt = FromDate.AddDays(j);
                    string date_ = String.Format("{0:dd-MM-yyyy}", dt);

                    STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
                    req.Trip1 = "D";
                    req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
                    req.TripType1 = "rdbOneWay";
                    req.TripType = STD.Shared.TripType.OneWay;
                    //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        
                    req.Adult = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Adult"]);
                    req.Child = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Child"]);
                    req.AgentType = "";
                    req.AirLine = MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString();    //"Indigo,6E";
                    req.ArrivalCity = "";//"Mumbai, India-Chhatrapati Shivaji International(BOM)";
                    req.DepartureCity = "";//"New Delhi, India-Indira Gandhi Intl(DEL)";
                    req.DepDate = date_.Replace("-", "/");              ///date.Replace("-", "/");
                    req.DISTRID = "SPRING";
                    req.GDSRTF = false;
                    req.HidTxtAirLine = MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString().Split(',')[1];
                    req.HidTxtArrCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["To"]) +",IN";
                    req.HidTxtDepCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["From"])+ ",IN";
                    req.Infant = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Infrant"]);
                    req.IsCorp = false;
                    req.NStop = false;
                    req.OwnerId = "";
                    req.Provider = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["ProviderType"]);
                    req.RetDate = date_.Replace("-", "/");   //date.Replace("-", "/");  //"08/06/2018";
                    req.RTF = false;
                    req.TypeId = "";
                    req.UserType = "";

                    req.TDS = "0";
                    req.UID = "";
                    req.Cabin = "";

                    //Add New  for MultiCity
                    req.DepartureCity2 = "";
                    req.ArrivalCity2 = "";
                    req.HidTxtArrCity2 = "";
                    req.HidTxtDepCity2 = "";
                    req.DepDate2 = "";

                    req.DepartureCity3 = "";
                    req.ArrivalCity3 = "";
                    req.HidTxtArrCity3 = "";
                    req.HidTxtDepCity3 = "";
                    req.DepDate3 = "";

                    req.DepartureCity4 = "";
                    req.ArrivalCity4 = "";
                    req.HidTxtArrCity4 = "";
                    req.HidTxtDepCity4 = "";
                    req.DepDate4 = "";
                    req.DepartureCity5 = "";
                    req.ArrivalCity5 = "";
                    req.HidTxtArrCity5 = "";
                    req.HidTxtDepCity5 = "";
                    req.DepDate5 = "";

                    req.DepartureCity6 = "";
                    req.ArrivalCity6 = "";
                    req.HidTxtArrCity6 = "";
                    req.HidTxtDepCity6 = "";
                    req.DepDate6 = "";

                    STD.Shared.IFlt objI = new STD.BAL.FltService();
                    STD.BAL.SearchWrapper obj = new STD.BAL.SearchWrapper();
                    HttpContext context = HttpContext.Current;


                    if (req.Provider == "LCC")
                    {
                        List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                        List<string> listResult2 = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                    }
                    else
                    {
                        List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
                        List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
                    }
                }
            }
        }
     

    }
    public DataSet FetchMidService()
    {
        DataSet ds = new DataSet();
        try
        {
            adap = new SqlDataAdapter("SP_Tbl_MidServiceCall", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

        return ds;
    }
   

    public DataSet checkedjson()
    {
        DataSet ds = new DataSet();
        try
        {
            adap = new SqlDataAdapter("select * from Tbl_CacheMidSearvice", con);
            adap.SelectCommand.CommandType = CommandType.Text;
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            string str = ds.Tables[0].Rows[0]["Response"].ToString();
        }
           return ds;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CallingService();
    }
}