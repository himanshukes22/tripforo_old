<%@ WebHandler Language="C#" Class="AirHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AirHandler : IHttpHandler, IReadOnlySessionState
{
    private System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        //context.Response.AddHeader("Content-Encoding", "gzip");

        STD.BAL.FlightCommonBAL FCBAL = new STD.BAL.FlightCommonBAL(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        List<STD.Shared.UserFlightSearch> FCSHARED = new List<STD.Shared.UserFlightSearch>();
        FCSHARED = FCBAL.GetUserFlightSearch(context.Session["UID"].ToString(), context.Session["User_Type"].ToString());
        bool isCoupon = false;
        STD.Shared.FlightSearch req = new STD.Shared.FlightSearch();
        
        string listProvider="";
        bool isLCC = false;
        string callApi = "";
        if (FCSHARED.Count > 0)
        {
            req.Trip1 = context.Request["Trip1"].ToString();
            req.Trip = req.Trip1 == "D" ? STD.Shared.Trip.D : STD.Shared.Trip.I;
            req.TripType1 = context.Request["TripType1"].ToString();

            if (req.TripType1 == "rdbMultiCity")
            {
                req.TripType = STD.Shared.TripType.MultiCity;
            }
            else
            {
                req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;
            }
            //req.TripType = req.TripType1 == "rdbOneWay" ? STD.Shared.TripType.OneWay : STD.Shared.TripType.RoundTrip;        

            req.Adult = Convert.ToInt16(context.Request["Adult"]);
            req.Child = Convert.ToInt16(context.Request["Child"]);
            req.AgentType = FCSHARED[0].AgentType;
            req.AirLine = context.Request["AirLine"].ToString();
            req.ArrivalCity = context.Request["ArrivalCity"].ToString();
            req.DepartureCity = context.Request["DepartureCity"].ToString();
            req.DepDate = context.Request["DepDate"].ToString();
            req.DISTRID = "SPRING";
            req.GDSRTF = Convert.ToBoolean(context.Request["GDSRTF"]);
            req.HidTxtAirLine = context.Request["HidTxtAirLine"].ToString();
            req.HidTxtArrCity = context.Request["HidTxtArrCity"].ToString();
            req.HidTxtDepCity = context.Request["HidTxtDepCity"].ToString();
            req.Infant = Convert.ToInt16(context.Request["Infant"].ToString());
            req.IsCorp = Convert.ToBoolean(context.Request["IsCorp"]);
            req.NStop = Convert.ToBoolean(context.Request["NStop"]);
            req.OwnerId = FCSHARED[0].UserId;
            req.Provider = context.Request["Provider"].ToString();
            req.RetDate = context.Request["RetDate"].ToString();
            req.RTF = Convert.ToBoolean(context.Request["RTF"]);
            req.TypeId = FCSHARED[0].TypeId;
            req.UserType = FCSHARED[0].UserType;
            isCoupon = Convert.ToBoolean(context.Request["isCoupon"]);
            req.TDS = STD.BAL.Data.Calc_TDS(Con.ConnectionString, req.OwnerId);
            req.UID = req.OwnerId;
            req.Cabin = context.Request["Cabin"].ToString();

            //Add New  for MultiCity
            req.DepartureCity2 = Convert.ToString(context.Request["DepartureCity2"]);
            req.ArrivalCity2 = Convert.ToString(context.Request["ArrivalCity2"]);
            req.HidTxtArrCity2 = Convert.ToString(context.Request["HidTxtArrCity2"]);
            req.HidTxtDepCity2 = Convert.ToString(context.Request["HidTxtDepCity2"]);
            req.DepDate2 = Convert.ToString(context.Request["DepDate2"]);

            req.DepartureCity3 = Convert.ToString(context.Request["DepartureCity3"]);
            req.ArrivalCity3 = Convert.ToString(context.Request["ArrivalCity3"]);
            req.HidTxtArrCity3 = Convert.ToString(context.Request["HidTxtArrCity3"]);
            req.HidTxtDepCity3 = Convert.ToString(context.Request["HidTxtDepCity3"]);
            req.DepDate3 = Convert.ToString(context.Request["DepDate3"]);

            req.DepartureCity4 = Convert.ToString(context.Request["DepartureCity4"]);
            req.ArrivalCity4 = Convert.ToString(context.Request["ArrivalCity4"]);
            req.HidTxtArrCity4 = Convert.ToString(context.Request["HidTxtArrCity4"]);
            req.HidTxtDepCity4 = Convert.ToString(context.Request["HidTxtDepCity4"]);
            req.DepDate4 = Convert.ToString(context.Request["DepDate4"]);

            req.DepartureCity5 = Convert.ToString(context.Request["DepartureCity5"]);
            req.ArrivalCity5 = Convert.ToString(context.Request["ArrivalCity5"]);
            req.HidTxtArrCity5 = Convert.ToString(context.Request["HidTxtArrCity5"]);
            req.HidTxtDepCity5 = Convert.ToString(context.Request["HidTxtDepCity5"]);
            req.DepDate5 = Convert.ToString(context.Request["DepDate5"]);

            req.DepartureCity6 = Convert.ToString(context.Request["DepartureCity6"]);
            req.ArrivalCity6 = Convert.ToString(context.Request["ArrivalCity6"]);
            req.HidTxtArrCity6 = Convert.ToString(context.Request["HidTxtArrCity6"]);
            req.HidTxtDepCity6 = Convert.ToString(context.Request["HidTxtDepCity6"]);
            req.DepDate6 = Convert.ToString(context.Request["DepDate6"]);

            //  DepartureCity2: t.txtDepCity2.val(),
            //ArrivalCity2: t.txtArrCity2.val(),
            //HidTxtDepCity2: t.hidtxtDepCity2.val(),
            //HidTxtArrCity2: t.hidtxtArrCity2.val(),   
            //DepDate2: t.txtDepDate2.val(),
            listProvider =Convert.ToString(context.Request["ListProvider"]);
            isLCC = Convert.ToBoolean(context.Request["isLCC"]);

            callApi = Convert.ToString(context.Request["callApi"]);
        }
        STD.Shared.IFlt objI = new STD.BAL.FltService();
        string resultJson = "";
        string provider = "";

        if (req.Provider == "OF")
        {
            provider = "OF";
        }
        else if (req.Trip == STD.Shared.Trip.I)
        {
            provider = req.Provider + req.HidTxtAirLine.Replace(",", "");
        }
        else
        {
            provider = req.HidTxtAirLine.Replace(",", "");
        }
        
        
        if(req.HidTxtAirLine !="" && req.HidTxtAirLine.Contains(","))
        {
         req.HidTxtAirLine = req.HidTxtAirLine.Split(',')[1];
        }
           


        //if (isCoupon)
        //{
        //    resultJson = "{ \"result\":" + Newtonsoft.Json.JsonConvert.SerializeObject(objI.FltSearchResultCoupon(req)) + ",\"Provider\":\"" + provider + "CPN\"}";
        //}
        //else
        //{
        //    resultJson = "{ \"result\":" + Newtonsoft.Json.JsonConvert.SerializeObject(objI.FltSearchResult(req)) + ",\"Provider\":\"" + provider + "NRML\"}";

        //}
        

        STD.BAL.SearchWrapper obj = new STD.BAL.SearchWrapper();

              List<string> listResult = null;
        if (req.NStop == true)
        {
            if (isCoupon == false)
            {
                STD.BAL.FixedDeparture objfix = new STD.BAL.FixedDeparture();
                listResult = objfix.FltSearchResult(req, isCoupon, listProvider, isLCC, context, callApi);
            }

        }
        else
        {
            listResult = obj.FltSearchResult(req, isCoupon, listProvider, isLCC, context, callApi);
        }
      
        //var compressedStream = new System.IO.MemoryStream();
        //var uncompressedStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(resultJson));
     string result = Newtonsoft.Json.JsonConvert.SerializeObject(listResult);
     var bytes = System.Text.Encoding.UTF8.GetBytes(result);

        using (var msi = new System.IO.MemoryStream(bytes))
        using (var mso = new System.IO.MemoryStream())
        {
            using (var gs = new System.IO.Compression.GZipStream(mso, System.IO.Compression.CompressionMode.Compress))
            {
                //msi.CopyTo(gs);
                CopyTo(msi, gs);
            }

            data1 on = new data1();
            on.data = Convert.ToBase64String( mso.ToArray());
            //System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string serEmployee = javaScriptSerializer.Serialize(on);

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(on));


        }

    }

    public void CopyTo(System.IO.Stream src, System.IO.Stream dest)
    {
        byte[] bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
        {
            dest.Write(bytes, 0, cnt);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}


public class data1
{

    public string data { get; set; }
}