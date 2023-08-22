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
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.IO;


public partial class CallingWebCache : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    //private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        // checkedjson();
       // CallingService();

        GDSPSyncInThread(HttpContext.Current);

    }
    private void TraceService(string content)
    {

        //set up a filestream
        FileStream fs = new FileStream(@"C:\Caching\ScheduledService.txt", FileMode.OpenOrCreate, FileAccess.Write);

        //set up a streamwriter for adding text
        StreamWriter sw = new StreamWriter(fs);

        //find the end of the underlying filestream
        sw.BaseStream.Seek(0, SeekOrigin.End);

        //add the text
        sw.WriteLine(content);
        //add the text to the underlying filestream

        sw.Flush();
        //close the writer
        sw.Close();
    }

    protected void CallingService()
    {


        DataSet MidServiceGet = new DataSet();
        MidServiceGet = FetchMidService();

        for (int i = 0; i < MidServiceGet.Tables[0].Rows.Count; i++)
        {

            string strNrm = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["url"]);
            //string ProviderType = "";
            string FromDate_Url = "";
            //string Return_Url = "";
            string adult = "";
            string child = "";
            string inf = "";

            FromDate_Url = strNrm.Substring(8, 10);
            // Return_Url = strNrm.Substring(18, 10);
            adult = strNrm.Substring(30, 1);
            child = strNrm.Substring(32, 1);
            inf = strNrm.Substring(34, 1);

            DateTime FromDate = Convert.ToDateTime(FromDate_Url);
            DateTime dt = FromDate;
            //dt = FromDate.AddDays(j);
            string date_ = String.Format("{0:dd-MM-yyyy}", dt);

            STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
            req.Trip1 = "D";
            req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
            req.TripType1 = "rdbOneWay";
            req.TripType = STD.Shared.TripType.OneWay;
            //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        
            req.Adult = Convert.ToInt32(adult);//Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Adult"]);
            req.Child = Convert.ToInt32(child);
            req.AgentType = "";
            req.AirLine = "Indigo,6E";
            req.ArrivalCity = "";//"Mumbai, India-Chhatrapati Shivaji International(BOM)";
            req.DepartureCity = "";//"New Delhi, India-Indira Gandhi Intl(DEL)";
            req.DepDate = date_.Replace("-", "/");              ///date.Replace("-", "/");
            req.DISTRID = "SPRING";
            req.GDSRTF = false;
            req.HidTxtAirLine = "6E"; //MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString().Split(',')[1];
            req.HidTxtArrCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["Sector"]).Split(':')[1] + ",IN";
            req.HidTxtDepCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["Sector"]).Split(':')[0] + ",IN";
            req.Infant = Convert.ToInt32(child);
            req.IsCorp = false;
            req.NStop = false;
            req.OwnerId = "";
            req.Provider = "LCC";
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

            string exist = deletecache(Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["id"]));

            if (exist == "N")
            {
                if (req.Provider == "LCC")
                {
                    List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                    List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                }
            }
        }
    }
    public DataSet FetchMidService()
    {
        DataSet ds = new DataSet();
        try
        {
            adap = new SqlDataAdapter("SP_MIDSCALLCAHCE", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }

        return ds;
    }
    public string deletecache(int keyno)
    {
        string Value = "";
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_DeleteCacheKey", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", keyno);
            con.Open();
            Value = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
        }
        catch (Exception ex)
        {
            return "N";
        }

        return Value;

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



    public async Task CallGWS(DataRow Row, HttpContext context)
    {
        try
        {
            //   MidServiceGet.Tables[0].Rows[i]["url"]

            string strNrm = Convert.ToString(Row["url"]);
            //string ProviderType = "";
            string FromDate_Url = "";
            //string Return_Url = "";
            string adult = "";
            string child = "";
            string inf = "";

            FromDate_Url = strNrm.Substring(8, 10);
            // Return_Url = strNrm.Substring(18, 10);
            adult = strNrm.Substring(30, 1);
            child = strNrm.Substring(32, 1);
            inf = strNrm.Substring(34, 1);

            DateTime FromDate = Convert.ToDateTime(FromDate_Url);
            DateTime dt = FromDate;
            //dt = FromDate.AddDays(j);
            string date_ = String.Format("{0:dd-MM-yyyy}", dt);

            STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
            req.Trip1 = "D";
            req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
            req.TripType1 = "rdbOneWay";
            req.TripType = STD.Shared.TripType.OneWay;
            //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        
            req.Adult = Convert.ToInt32(adult);//Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Adult"]);
            req.Child = Convert.ToInt32(child);
            req.AgentType = "";
            req.AirLine = "Indigo,6E";
            req.ArrivalCity = "";//"Mumbai, India-Chhatrapati Shivaji International(BOM)";
            req.DepartureCity = "";//"New Delhi, India-Indira Gandhi Intl(DEL)";
            req.DepDate = date_.Replace("-", "/");              ///date.Replace("-", "/");
            req.DISTRID = "SPRING";
            req.GDSRTF = false;
            req.HidTxtAirLine = "6E"; //MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString().Split(',')[1];
            req.HidTxtArrCity = Convert.ToString(Row["Sector"]).Split(':')[1] + ",IN";
            req.HidTxtDepCity = Convert.ToString(Row["Sector"]).Split(':')[0] + ",IN";
            req.Infant = Convert.ToInt32(child);
            req.IsCorp = false;
            req.NStop = false;
            req.OwnerId = "";
            req.Provider = "LCC";
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
          

            string exist = deletecache(Convert.ToInt32(Row["id"]));

            if (exist == "N")
            {
                if (req.Provider == "LCC")
                {
                    try
                    {
                        //TraceService("hiting--Process" + DateTime.Now);
                        List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                        List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                       // TraceService("hitingEnd--Process" + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        TraceService("hiting--ProcessError" + ex.Message + DateTime.Now);

                    }
                }
            }


            await Task.Delay(0);
        }
        catch (Exception ex)
        {

            TraceService("hiting--ProcessErrorfinal" + ex.Message + DateTime.Now);
        }
    }

    public async Task GDSPSyncInThread(HttpContext context)
    {


        try
        {
            DataSet MidServiceGet = new DataSet();
            MidServiceGet = FetchMidService();
            int taskCount = 10;//int.Parse(System.Configuration.ConfigurationManager.AppSettings["TaskCnt"].ToString());

            //List<Task> taskList = new List<Task>();
            int Count = 1;
            ConcurrentBag<Task> bag = new ConcurrentBag<Task>();


            for (int i = 0; i < MidServiceGet.Tables[0].Rows.Count; i++)
            {
                #region Existing Profile
                try
                {
                    bag.Add(Task.Factory.StartNew(async () => await CallGWS(MidServiceGet.Tables[0].Rows[i], context)));
                    Thread.Sleep(200);

                    if (taskCount == Count || (taskCount != Count && i == MidServiceGet.Tables[0].Rows.Count - 1))
                    {
                        //Task.WhenAll(taskList).Wait();
                        try
                        {
                            var continuation = Task.WhenAll(bag.ToArray());
                            try
                            {
                                continuation.ConfigureAwait(true);
                                //continuation.RunSynchronously();
                                continuation.Wait();
                            }
                            catch (AggregateException)
                            { }
                            Thread.Sleep(200);
                            Count = 1;

                            //taskList = new List<Task>();
                            bag = new ConcurrentBag<Task>();
                            Thread.Sleep(200);
                        }
                        catch { }
                    }
                    else
                    {
                        Count++;
                    }

                }
                catch (Exception ex)
                { }
                #endregion


            }

        }
        catch (Exception ex) { }
    }

}