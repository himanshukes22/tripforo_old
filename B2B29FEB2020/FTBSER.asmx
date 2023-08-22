<%@ WebService Language="C#" Class="FTBSER" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Web.SessionState;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class FTBSER : System.Web.Services.WebService
{
    private System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);


    [WebMethod(EnableSession = true)]
    public string FTBFARE(string objreq)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        JObject array = JObject.Parse(objreq);
        STD.Shared.FlightSearch req = serializer.Deserialize<STD.Shared.FlightSearch>(array.ToString());
        STD.BAL.FlightCommonBAL FCBAL = new STD.BAL.FlightCommonBAL(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        List<STD.Shared.UserFlightSearch> FCSHARED = new List<STD.Shared.UserFlightSearch>();
        FCSHARED = FCBAL.GetUserFlightSearch(Session["UID"].ToString(), Session["User_Type"].ToString());
        bool isCoupon = false;


        string listProvider = "";
        bool isLCC = true;
        string callApi = "";
        if (FCSHARED.Count > 0)
        {
            if (req.HidTxtArrCity.Substring(req.HidTxtArrCity.LastIndexOf(",") + 1).Trim() != "IN" || req.HidTxtDepCity.Substring(req.HidTxtDepCity.LastIndexOf(",") + 1).Trim() != "IN")
            {
                req.Trip1 = "I";
                req.Trip = STD.Shared.Trip.I;
            }
            else
            {
                req.Trip1 = "D";
                req.Trip = STD.Shared.Trip.D;
            }
            req.TripType1 = "rdbOneWay";
            req.OwnerId = FCSHARED[0].UserId;
            req.TypeId = FCSHARED[0].TypeId;
            req.UserType = FCSHARED[0].UserType;
            req.TDS = STD.BAL.Data.Calc_TDS(Con.ConnectionString, req.OwnerId);
            req.UID = req.OwnerId;
            listProvider = "SS:FDD";

            callApi = "";
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


        if (req.HidTxtAirLine != "" && req.HidTxtAirLine.Contains(","))
        {
            req.HidTxtAirLine = req.HidTxtAirLine.Split(',')[1];
        }



        STD.BAL.SearchWrapper obj = new STD.BAL.SearchWrapper();

        List<string> listResult = null;

        HttpContext htt = HttpContext.Current; ;
        STD.BAL.FixedDeparture objfix = new STD.BAL.FixedDeparture();
        listResult = objfix.FltSearchResult(req, isCoupon, listProvider, isLCC, htt, callApi);


        string result = Newtonsoft.Json.JsonConvert.SerializeObject(listResult);

        return result;
    }


}

