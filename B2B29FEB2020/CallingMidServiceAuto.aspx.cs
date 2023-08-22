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
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;


public partial class CallingMidServiceAuto : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    //private SqlTransactionDom STDom = new SqlTransactionDom();
    private SqlDataAdapter adap;
    protected void Page_Load(object sender, EventArgs e)
    {
        GDSPSyncInThread(HttpContext.Current);
        //CallingService();

    }

    //protected void CallingService()
    //{


    //    DataSet MidServiceGet = new DataSet();
    //    MidServiceGet = FetchMidService();

    //    for (int i = 0; i < MidServiceGet.Tables[0].Rows.Count; i++)
    //    {

    //        if (Convert.ToBoolean(MidServiceGet.Tables[0].Rows[i]["Active"]) == true)
    //        {
    //            for (int j = 0; j < Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["NoOfHowManyDays"].ToString()); j++)
    //            {

    //                DateTime FromDate = Convert.ToDateTime(MidServiceGet.Tables[0].Rows[i]["FromDate_"].ToString());
    //                DateTime dt = FromDate;
    //                dt = FromDate.AddDays(j);
    //                string date_ = String.Format("{0:dd-MM-yyyy}", dt);

    //                STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
    //                req.Trip1 = "D";
    //                req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
    //                req.TripType1 = "rdbOneWay";
    //                req.TripType = STD.Shared.TripType.OneWay;
    //                //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        
    //                req.Adult = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Adult"]);
    //                req.Child = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Child"]);
    //                req.AgentType = "";
    //                req.AirLine = MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString();    //"Indigo,6E";
    //                req.ArrivalCity = "";//"Mumbai, India-Chhatrapati Shivaji International(BOM)";
    //                req.DepartureCity = "";//"New Delhi, India-Indira Gandhi Intl(DEL)";
    //                req.DepDate = date_.Replace("-", "/");              ///date.Replace("-", "/");
    //                req.DISTRID = "SPRING";
    //                req.GDSRTF = false;
    //                req.HidTxtAirLine = MidServiceGet.Tables[0].Rows[i]["AirlineCode"].ToString().Split(',')[1];
    //                req.HidTxtArrCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["To"]) +",IN";
    //                req.HidTxtDepCity = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["From"])+ ",IN";
    //                req.Infant = Convert.ToInt32(MidServiceGet.Tables[0].Rows[i]["Infrant"]);
    //                req.IsCorp = false;
    //                req.NStop = false;
    //                req.OwnerId = "";
    //                req.Provider = Convert.ToString(MidServiceGet.Tables[0].Rows[i]["ProviderType"]);
    //                req.RetDate = date_.Replace("-", "/");   //date.Replace("-", "/");  //"08/06/2018";
    //                req.RTF = false;
    //                req.TypeId = "";
    //                req.UserType = "";

    //                req.TDS = "0";
    //                req.UID = "";
    //                req.Cabin = "";

    //                //Add New  for MultiCity
    //                req.DepartureCity2 = "";
    //                req.ArrivalCity2 = "";
    //                req.HidTxtArrCity2 = "";
    //                req.HidTxtDepCity2 = "";
    //                req.DepDate2 = "";

    //                req.DepartureCity3 = "";
    //                req.ArrivalCity3 = "";
    //                req.HidTxtArrCity3 = "";
    //                req.HidTxtDepCity3 = "";
    //                req.DepDate3 = "";

    //                req.DepartureCity4 = "";
    //                req.ArrivalCity4 = "";
    //                req.HidTxtArrCity4 = "";
    //                req.HidTxtDepCity4 = "";
    //                req.DepDate4 = "";
    //                req.DepartureCity5 = "";
    //                req.ArrivalCity5 = "";
    //                req.HidTxtArrCity5 = "";
    //                req.HidTxtDepCity5 = "";
    //                req.DepDate5 = "";

    //                req.DepartureCity6 = "";
    //                req.ArrivalCity6 = "";
    //                req.HidTxtArrCity6 = "";
    //                req.HidTxtDepCity6 = "";
    //                req.DepDate6 = "";

    //                STD.Shared.IFlt objI = new STD.BAL.FltService();
    //                STD.BAL.SearchWrapper obj = new STD.BAL.SearchWrapper();
    //                HttpContext context = HttpContext.Current;


    //                if (req.Provider == "LCC")
    //                {
    //                    List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
    //                    List<string> listResult2 = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
    //                }
    //                //else
    //                //{
    //                //    List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
    //                //    List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
    //                //}
    //            }
    //        }
    //    }
     

    //}
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
                {
                    TraceService("WindowService2:----" + ex.Message + ex.StackTrace + DateTime.Now);
                }
                #endregion


            }

        }
        catch (Exception ex) {

            TraceService("WindowService2:----" + ex.Message + ex.StackTrace + DateTime.Now);
        
        }
    }

    public async Task CallGWS(DataRow Row, HttpContext context)
    {
        try
        {
            if (Convert.ToBoolean(Row["Active"]) == true)
            {
                for (int j = 0; j < Convert.ToInt32(Row["NoOfHowManyDays"].ToString()); j++)
                {

                    DateTime FromDate = Convert.ToDateTime(Row["FromDate_"].ToString());
                    DateTime dt = FromDate;
                    dt = FromDate.AddDays(j);
                    string date_ = String.Format("{0:dd-MM-yyyy}", dt);

                    STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
                    req.Trip1 = "D";
                    req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
                    req.TripType1 = "rdbOneWay";
                    req.TripType = STD.Shared.TripType.OneWay;
                    //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        
                    req.Adult = Convert.ToInt32(Row["Adult"]);
                    req.Child = Convert.ToInt32(Row["Child"]);
                    req.AgentType = "";
                    req.AirLine = Row["AirlineCode"].ToString();    //"Indigo,6E";
                    req.ArrivalCity = "";//"Mumbai, India-Chhatrapati Shivaji International(BOM)";
                    req.DepartureCity = "";//"New Delhi, India-Indira Gandhi Intl(DEL)";
                    req.DepDate = date_.Replace("-", "/");              ///date.Replace("-", "/");
                    req.DISTRID = "SPRING";
                    req.GDSRTF = false;
                    req.HidTxtAirLine = Row["AirlineCode"].ToString().Split(',')[1];
                    req.HidTxtArrCity = Convert.ToString(Row["To"]) + ",IN";
                    req.HidTxtDepCity = Convert.ToString(Row["From"]) + ",IN";
                    req.Infant = Convert.ToInt32(Row["Infrant"]);
                    req.IsCorp = false;
                    req.NStop = false;
                    req.OwnerId = "";
                    req.Provider = Convert.ToString(Row["ProviderType"]);
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

                    //DEL/BOM/26/07/2018/26/07/2018/1/0/0/False/False/
                    string str = Checkcache(Convert.ToString(Convert.ToString(Row["From"]) + "/" + Convert.ToString(Row["To"]) + "/" + req.DepDate + "/" + req.DepDate + "/"+ Convert.ToString(req.Adult)+"/"+Convert.ToString(req.Child)+"/"+Convert.ToString(req.Infant)+"/False/False/"), req.HidTxtAirLine);

                    if (str == "NO")
                    {
                        if (req.Provider == "LCC")
                        {
                            TraceService("WindowService2:----" + Convert.ToString(j) + req.DepDate + req.HidTxtAirLine);
                            List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                            List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, true, context, req.HidTxtAirLine);
                        }
                        //else
                        //{
                        //    List<string> listResult = obj.FltSearchResult(req, false, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
                        //    List<string> listResult2 = obj.FltSearchResult(req, true, req.HidTxtAirLine + ":" + req.Provider, false, context, req.HidTxtAirLine);
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {

            TraceService("WindowService2:----" + ex.Message + DateTime.Now);
        }
    }
    public string Checkcache(string skeyvalue, string AirCode)
    {
        try
        {
            string val = "";
            SqlCommand cmd = new SqlCommand("Sp_Sector_check", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@skeyvalue", skeyvalue.Trim());
            cmd.Parameters.AddWithValue("@AirCode", AirCode.ToUpper().Trim());
            con.Open();
            val = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return val;
        }       
        catch (Exception ex)
        {
            return "YES";
        }

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
}