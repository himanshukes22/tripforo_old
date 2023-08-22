<%@ WebService Language="C#" Class="HotelSearchs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using HotelShared;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class HotelSearchs : System.Web.Services.WebService
{
    //Hotel City Search
    [WebMethod()]
    public List<HotelShared.CitySearch> HtlCityList(string city)
    {
        HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
        List<HotelShared.CitySearch> citylist = new List<HotelShared.CitySearch>();
        try
        {
            citylist = HTLST.GetCityList(city);
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, ex.StackTrace);
        }
        return citylist;
    }
    //Hotel Name Search
    [WebMethod()]
    public List<HotelShared.CitySearch> HotelNameSearch(string HotelName, string city)
    {
        List<HotelShared.CitySearch> citylist = new List<HotelShared.CitySearch>();
        try
        {
            HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
            citylist = HTLST.GetCityList2(city, HotelName);
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, ex.StackTrace);
        }
        return citylist;
    }
    //List<CitySearch> GetMarkupCityCountryList(string TripType, string city, string Country, List<CitySearch> CityList
    [WebMethod()]
    public List<HotelShared.CitySearch> HtlMrkupCityCountryList(string city, string Country)
    {
        HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
        List <HotelShared.CitySearch > citylist = new List<HotelShared.CitySearch>();
        try
        {
            citylist = HTLST.GetMarkupCityCountryList( city, Country, citylist);
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, ex.StackTrace);
        }
        return citylist;
    }

    [WebMethod(EnableSession = true)]
  // public string GetHtlResult(string CheckInDate, string CheckOutDate, string HTLCity, string NoOfRooms, string TotalAdult, string TotalChild, string TotalAdultperrooms, string Totalchildperrooms, string childage, string HotelName, string starratings)
   public List<HotelResult> GetHtlResult(string CheckInDate, string CheckOutDate, string HTLCity, string NoOfRooms, string TotalAdult, string TotalChild, string TotalAdultperrooms, string Totalchildperrooms, string childage, string HotelName, string starratings)
    {
        HotelSearch search = new HotelSearch(); HotelComposite HotelCompositResult = new HotelComposite();
        ArrayList adultsperroom = new ArrayList(); ArrayList childsperroom = new ArrayList();
        string Hotellistdata="";
        try
        {
            if (HttpContext.Current.Session["UID"] == null)
            {
                List<HotelResult> objHotellist = new List<HotelResult>();
                objHotellist.Add(new HotelResult { HtlError = "SessionExpired" });
                HotelCompositResult.Hotelresults = objHotellist;
            }
            else
            {
                search.HTLCityList = HTLCity.Trim();
                string[] CheckInsDate = CheckInDate.Split('/');
                string[] CheckOutsDate = CheckOutDate.Split('/');
                search.CheckInDate = CheckInsDate[2] + "-" + CheckInsDate[1] + "-" + CheckInsDate[0];
                search.CheckOutDate = CheckOutsDate[2] + "-" + CheckOutsDate[1] + "-" + CheckOutsDate[0];
                search.NoofRoom = Convert.ToInt32(NoOfRooms);
                for (int aa = 0; aa < search.NoofRoom; aa++)
                {
                    adultsperroom.Add(TotalAdultperrooms.Split('_')[aa]);
                    childsperroom.Add(Totalchildperrooms.Split('_')[aa]);
                }
                search.AdtPerRoom = adultsperroom;
                search.ChdPerRoom = childsperroom;
                search.TotAdt = Convert.ToInt32(TotalAdult);
                search.TotChd = Convert.ToInt32(TotalChild);

                int[,] chld_age = new int[search.NoofRoom + 1, 4];
                try
                {
                    string[] RoomCount = childage.Split('#');
                    for (int ii = 0; ii < search.NoofRoom; ii++)
                    {
                        if (childsperroom[ii] == null)
                            childsperroom[ii] = 0;

                        string[] agesArray1 = new string[4];

                        if (Convert.ToInt32(childsperroom[ii]) != 0)
                        {
                            for (int tc = 0; tc <= Convert.ToInt32(childsperroom[ii]) - 1; tc++)
                            {
                                if (childsperroom[0] == null)
                                {
                                    break; // TODO: might not be correct. Was : Exit For rooms[0].child[0].age=3,rooms[0].child[1].age=4,rooms[1].child[0].age=5,rooms[1].child[1].age=6,
                                }
                                else
                                {
                                    for (int ag = 1; ag <= RoomCount.Length - 1; ag++)
                                    {
                                        string rn = RoomCount[ag].Substring(0, 1);

                                        if (ii == Convert.ToInt32(rn))
                                        {
                                            string[] strarray = RoomCount[ag].Split('_');
                                            agesArray1[tc] = strarray[tc];
                                            chld_age[ii, tc] = Convert.ToInt32(agesArray1[tc].Substring(1, agesArray1[tc].Length - 1));
                                            break; 
                                        }
                                    }                                  
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HotelDAL.HotelDA.InsertHotelErrorLog(ex, "GetHtlResult");
                    HotelDAL.HotelDA.InsertHotelErrorLog(ex, search.AgentID);
                     List<HotelResult> objHotellist = new List<HotelResult>();
                objHotellist.Add(new HotelResult { HtlError = "EXCEPTION" });
                HotelCompositResult.Hotelresults = objHotellist;
                }
                search.ChdAge = chld_age;
                if (HotelName != null & !string.IsNullOrEmpty(HotelName))
                    search.HotelName = HotelName;
                else
                    search.HotelName = "";
                if ((starratings) != null & !string.IsNullOrEmpty(starratings))
                    search.StarRating = Convert.ToInt32(starratings).ToString();
                else
                    search.StarRating = "0";
                if (HttpContext.Current.Session["User_Type"].ToString() == "AGENT")
                    search.AgencyGroup = HttpContext.Current.Session["AGTY"].ToString();
                else
                    search.AgencyGroup = HttpContext.Current.Session["User_Type"].ToString();
                search.Sorting = "";
                search.AgentID = HttpContext.Current.Session["UID"].ToString();
                search.BaseURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                search.CorporateID = "RWT";
                //SortOrder can have following values:TG_RANKING, PRICE, GUEST_RATING, STAR_RATING_ASCENDING, STAR_RATING_DESCENDING, DEALS
                HotelBAL.HotelAvailabilitySearch objHtlReq = new HotelBAL.HotelAvailabilitySearch();
                HotelCompositResult = objHtlReq.HotelAvailability(search);
                HttpContext.Current.Session["HotelCompositSearch"] = HotelCompositResult;
                HttpContext.Current.Session["HotelSearch"] = search;
                //HttpContext.Current.Session["SearchResult"] = HotelCompositResult.Hotelresults;
                //HttpContext.Current.Session["OrgList"] = HotelCompositResult.HotelOrgList;         
              //  HotelDAL.HotelDA objhtlDaa = new HotelDAL.HotelDA();
                //objhtlDaa.SP_Htl_InsUpdBookingLog("After result", objHtlReq.GetXMLFromObject(HotelCompositResult), objHtlReq.GetXMLFromObject(HotelCompositResult.Hotelresults), "Innstant", "HotelInsert");

                //string result = Newtonsoft.Json.JsonConvert.SerializeObject(HotelCompositResult.Hotelresults);
                //var bytes = System.Text.Encoding.UTF8.GetBytes(result);

                //using (var msi = new System.IO.MemoryStream(bytes))
                //using (var mso = new System.IO.MemoryStream())
                //{
                //    using (var gs = new System.IO.Compression.GZipStream(mso, System.IO.Compression.CompressionMode.Compress))
                //    {
                //        //msi.CopyTo(gs);
                //        CopyTo(msi, gs);
                //    }

                //    data1 on = new data1();
                //    on.data = Convert.ToBase64String(mso.ToArray());
                //    //System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //    //string serEmployee = javaScriptSerializer.Serialize(on);
                //    Hotellistdata += Newtonsoft.Json.JsonConvert.SerializeObject(on);
                //}
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "GetHtlResult");
             List<HotelResult> objHotellist = new List<HotelResult>();
             objHotellist.Add(new HotelResult { HtlError = "EXCEPTION" });
             HotelCompositResult.Hotelresults = objHotellist;
            
        }
       // HotelBAL.HotelAvailabilitySearch objHtlReq2 = new HotelBAL.HotelAvailabilitySearch();
       //return objHtlReq2.GetJsonStringFromListObject(HotelCompositResult.Hotelresults); 
       return HotelCompositResult.Hotelresults;
        //return Hotellistdata;
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
    public class data1
    {

        public string data { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public RoomComposite SelectedHotelSearch(string HotelName, string HotelCode)
    {
        RoomComposite objRoonCombind = new RoomComposite();
        try
        {
            if (Session["UID"] == null || Session["HotelSearch"] == null)
            {
                List<RoomList> objRoomList = new List<RoomList>();
                objRoomList.Add(new RoomList { Room_Error = "SessionExpired" });
                objRoonCombind.RoomDetails = objRoomList;
            }
            else
            {
                HotelComposite HotelCompositResult = new HotelComposite(); HotelSearch SearchDetails = new HotelSearch();
                HotelCompositResult = (HotelComposite)System.Web.HttpContext.Current.Session["HotelCompositSearch"];
                SearchDetails = (HotelShared.HotelSearch)System.Web.HttpContext.Current.Session["HotelSearch"];
                string[] strhtl = HotelCode.Split('_');
                SearchDetails.HtlCode = strhtl[0];
                SearchDetails.HotelCityCode = strhtl[1];
                SearchDetails.CorporateID = "";
                if (HttpContext.Current.Session["User_Type"].ToString() == "AGENT")
                    SearchDetails.AgencyGroup = HttpContext.Current.Session["AGTY"].ToString();
                else
                    SearchDetails.AgencyGroup = HttpContext.Current.Session["User_Type"].ToString();
                SearchDetails.BaseURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                HotelBAL.HotelAvailabilitySearch htlresp = new HotelBAL.HotelAvailabilitySearch();
                objRoonCombind = htlresp.SelectedHotelResponse(SearchDetails, HotelCompositResult, objRoonCombind, HotelName);
                // HotelBAL.CirtificationHotelAvailabilitySearch htlresp = new HotelBAL.CirtificationHotelAvailabilitySearch();
                //objRoonCombind = htlresp.SelectedHotelResponse(SearchDetails, HotelCompositResult, objRoonCombind, HotelName);
                SearchDetails.HotelName = HotelName;
                Session["SelectedHotelDetail"] = objRoonCombind;
                Session["HotelSearcDetailss"] = SearchDetails;

            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "SelectedHotelSearch_" + HotelName + "_" + HotelCode);
        }
        return objRoonCombind;
    }

    [WebMethod(EnableSession = true)]
    public string CancPolicy(string HotelCode, string RatePlaneCode, string Provider)
    {
        string Policys = "";
        HotelSearch HotelDetails = new HotelSearch();
        try
        {
            if (Session["UID"] == null || Session["HotelSearch"] == null)
                Policys = "SessionExpired";
            else
            {
                HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearch"];
                if (Session["UID"] == null || Session["HotelSearch"] == null)
                    Policys = "SessionExpired";
                else
                {
                    HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearch"];
                    HotelBAL.CommonHotelBL objcancel = new HotelBAL.CommonHotelBL();
                    Policys += objcancel.GetCancelationPolicy(HotelDetails, Provider, RatePlaneCode, HotelCode, Policys);
                }
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HtlResult_Cancpolicy");
            Policys = ex.Message;
        }
        return HttpUtility.HtmlDecode("<h1>Cancellation Policy</h1><ul>" + Policys + "</ul>");
    }
    [WebMethod(EnableSession = true)]
    public string HotelCashback_Room(string HotelPrice, string Star, string AgentMarkup)
    {
        StringBuilder str = new StringBuilder("");
        HotelSearch HotelDetails = new HotelSearch();
        try
        {
            if (Session["UID"] == null || Session["HotelSearch"] == null)
                str.Append("SessionExpired");
            else
            {
                HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearch"];
                string AgencyType = "";
                if (HttpContext.Current.Session["User_Type"].ToString() == "AGENT")
                    AgencyType = HttpContext.Current.Session["AGTY"].ToString();
                else
                    AgencyType = HttpContext.Current.Session["User_Type"].ToString();
                decimal NetPrices = Convert.ToDecimal(HotelPrice) - Convert.ToDecimal(AgentMarkup);
                HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
                DataSet commisionds = new DataSet();
                DataTable commisiondt = new DataTable();
                //commisionds = HTLST.GetDetailsPageCommision(HotelDetails.Country, HotelDetails.SearchCity, Star, HotelDetails.AgentID, AgencyType, NetPrices);
                commisiondt = commisionds.Tables[0];
                str.Append("<div align='center' style='font-size:13px;line-height:130%;width:250px;'>");
                str.Append("<div class='clear'></div>");
                str.Append("<div class='lft'>Commision : </div><div class='lft'>&nbsp;&nbsp;- <img src='Images/htlrs.png' class='pagingSize' /> " + commisiondt.Rows[0]["CommisionAmt"].ToString() + "</div>");
                str.Append("<div class='clear'></div>");
                str.Append("<div class='lft'>Markups : </div><div class='lft'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; - <img src='Images/htlrs.png' class='pagingSize' /> " + AgentMarkup + "</div>");
                str.Append("<div class='clear'></div>");
                str.Append("<div class='lft bld'>Price To Agent:</div><div class='lft bld'>&nbsp;&nbsp;<img src='Images/htlrs.png' class='pagingSize' /> " + commisiondt.Rows[0]["TotalAmount"].ToString() + "</div>");
                str.Append("<div class='clear'></div>");
                str.Append("</div>");
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HtlResult_HotelCashback");
        }
        return str.ToString();
    }

    [WebMethod()]
    public string RecentBookedHotel()
    {
        StringBuilder strhtl = new StringBuilder("");
        try
        {
            HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
            DataSet HotelDT = new DataSet();
            HotelDT = HTLST.RecentBookedHotel();
            if (HotelDT != null)
            {
                DataTable htldt = HotelDT.Tables[0];
                if (htldt.Rows.Count > 0)
                {
                    strhtl.Append("<div>");
                    strhtl.Append("<div class='f16 bld'>Recently Booked Hotels</div><div class='clear1'></div>");
                    for (int i = 0; i < htldt.Rows.Count; i++)
                    {
                        string redirctURL = "", starImg = "";
                        string[] checkin = htldt.Rows[i]["CheckIN"].ToString().Split('-');
                        string[] checkOut = htldt.Rows[i]["CheckOut"].ToString().Split('-');
                       if (htldt.Rows[i]["Provider"].ToString() != "RoomXML")
                            redirctURL = "HtlResult.aspx?htlcitylist=" + htldt.Rows[i]["City"].ToString() + "," + htldt.Rows[i]["cityCode"].ToString() + "," + htldt.Rows[i]["Country"].ToString() + "," + htldt.Rows[i]["CountryCode"].ToString() + ",CITY," + htldt.Rows[i]["cityCode"].ToString() + "&htlcheckin=" + checkin[2] + "/" + checkin[1] + "/" + checkin[0] + "&htlcheckout=" + checkOut[2] + "/" + checkOut[1] + "/" + checkOut[0] + "&numRoom=1&rooms[0].adult=1&rooms[0].children=0&TotAdt=1&TotChd=0&htlCity=" + htldt.Rows[i]["City"].ToString() + "&Nights=1&htlname=" + htldt.Rows[i]["HtlCode"].ToString() + "&htlstar=0&Guest=1_0_";
                       else if (htldt.Rows[i]["Provider"].ToString() != "GTA")
                           redirctURL = "HtlResult.aspx?htlcitylist=" + htldt.Rows[i]["City"].ToString() + "," + htldt.Rows[i]["cityCode"].ToString() + "," + htldt.Rows[i]["Country"].ToString() + "," + htldt.Rows[i]["CountryCode"].ToString() + ",CITY,&htlcheckin=" + checkin[2] + "/" + checkin[1] + "/" + checkin[0] + "&htlcheckout=" + checkOut[2] + "/" + checkOut[1] + "/" + checkOut[0] + "&numRoom=1&rooms[0].adult=1&rooms[0].children=0&TotAdt=1&TotChd=0&htlCity=" + htldt.Rows[i]["City"].ToString() + "&Nights=1&htlname=" + htldt.Rows[i]["HtlName"].ToString() + "&htlstar=0&Guest=1_0_"; 
                       else 
                           redirctURL = "HtlResult.aspx?htlcitylist=" + htldt.Rows[i]["City"].ToString() + "," + htldt.Rows[i]["cityCode"].ToString() + "," + htldt.Rows[i]["Country"].ToString() + "," + htldt.Rows[i]["CountryCode"].ToString() + ",CITY,&htlcheckin=" + checkin[2] + "/" + checkin[1] + "/" + checkin[0] + "&htlcheckout=" + checkOut[2] + "/" + checkOut[1] + "/" + checkOut[0] + "&numRoom=1&rooms[0].adult=1&rooms[0].children=0&TotAdt=1&TotChd=0&htlCity=" + htldt.Rows[i]["City"].ToString() + "&Nights=1&htlname=" + htldt.Rows[i]["HtlCode"].ToString() + "&htlstar=0&Guest=1_0_";
                       //else if (htldt.Rows[i]["Provider"].ToString() != "EX")
                       //     redirctURL = "HtlResult.aspx?htlcitylist=" + htldt.Rows[i]["City"].ToString() + "," + htldt.Rows[i]["cityCode"].ToString() + "," + htldt.Rows[i]["Country"].ToString() + "," + htldt.Rows[i]["CountryCode"].ToString() + ",CITY,&htlcheckin=" + checkin[2] + "/" + checkin[1] + "/" + checkin[0] + "&htlcheckout=" + checkOut[2] + "/" + checkOut[1] + "/" + checkOut[0] + "&numRoom=1&rooms[0].adult=1&rooms[0].children=0&TotAdt=1&TotChd=0&htlCity=" + htldt.Rows[i]["City"].ToString() + "&Nights=1&htlname=" + htldt.Rows[i]["HtlCode"].ToString() + "&htlstar=0&Guest=1_0_";
                       // else if (htldt.Rows[i]["Provider"].ToString() != "RZ")
                       //     redirctURL = "HtlResult.aspx?htlcitylist=" + htldt.Rows[i]["City"].ToString() + "," + htldt.Rows[i]["cityCode"].ToString() + "," + htldt.Rows[i]["Country"].ToString() + "," + htldt.Rows[i]["CountryCode"].ToString() + ",CITY,&htlcheckin=" + checkin[2] + "/" + checkin[1] + "/" + checkin[0] + "&htlcheckout=" + checkOut[2] + "/" + checkOut[1] + "/" + checkOut[0] + "&numRoom=1&rooms[0].adult=1&rooms[0].children=0&TotAdt=1&TotChd=0&htlCity=" + htldt.Rows[i]["City"].ToString() + "&Nights=1&htlname=" + htldt.Rows[i]["HtlCode"].ToString() + "&htlstar=0&Guest=1_0_";
                        
                        string[] starratings = htldt.Rows[i]["StarRating"].ToString().Split('.');
                        for (var st = 0; st < Convert.ToInt32(starratings[0]); st++)
                        {
                            starImg += " <img src='Images/star.png' alt='Hotel Rating'/> ";
                        }
                        if (starratings.Length > 1)
                        {
                            if (starratings[1] == "5")
                                starImg += "<img src='Images/star_cut.png' alt='Hotel Rating'/>";
                        }
                        
                        strhtl.Append("<div class='clear1'></div>");
                        strhtl.Append("<div><a href='" + redirctURL + "' class='' >");
                        strhtl.Append("<div class='lft w24'><img src='" + htldt.Rows[i]["HtlImage"].ToString() + "' class='hights60 w98'/></div>");
                        strhtl.Append("<div class='w75 rgt'>");
                        strhtl.Append("<div class='lft bld'>" + htldt.Rows[i]["HtlName"].ToString() + "</div>");
                        strhtl.Append("<div class='clear'></div>");
                        strhtl.Append("<div class='lft'>" + htldt.Rows[i]["City"].ToString() + ", " + htldt.Rows[i]["Country"].ToString() + "</div>");
                        strhtl.Append("<div class='clear'></div>");
                        strhtl.Append("<div class='lft'>" + starImg + "</div>");
                        strhtl.Append("<div class='clear'></div>");
                        strhtl.Append("</div></a></div>");
                        strhtl.Append("<div class='clear'></div></div><hr />");
                    }
                    strhtl.Append("</div>");
                }
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "RecentBookedHotel");
        }
        return strhtl.ToString();
    }
    [WebMethod(EnableSession = true)]
    public string RoomCancelationPolicy(string RatePlaneCode, string HotelCode, string Provider)
    {
        string Policys = "";
        HotelSearch HotelDetails = new HotelSearch();
        try
        {
            if (Session["UID"] == null || Session["HotelSearcDetailss"] == null)
                Policys = "SessionExpired";
            else
            {
                HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearcDetailss"];

                CancelationPolicy objcan = new CancelationPolicy();
                HotelBAL.CommonHotelBL objcancel = new HotelBAL.CommonHotelBL();
                objcan = objcancel.RoomCancelationPolicy(HotelDetails, Provider, RatePlaneCode, HotelCode, objcan);
                Policys = objcan.HotelPolicy;
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "RoomXMLCancellationPolicy");
        }
        return Policys;
    }
    [WebMethod(EnableSession = true)]
    public string HotelCashback(string HotelPrice, string Star)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            if (Session["UID"] == null)
                str.Append("SessionExpired");
            else
            {
                HotelSearch HotelDetails = new HotelSearch();
                HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearch"];
                string AgencyType = "";
                if (HttpContext.Current.Session["User_Type"].ToString() == "AGENT")
                    AgencyType = HttpContext.Current.Session["AGTY"].ToString();
                else
                    AgencyType = HttpContext.Current.Session["User_Type"].ToString();

                decimal hotelrate = Convert.ToDecimal(HotelPrice.Trim());

                List<HotelResult> cashbak = new List<HotelResult>();
                cashbak = (List<HotelResult>)HttpContext.Current.Session["SearchResult"];
                cashbak = cashbak.Where(x => x.hotelPrice == hotelrate).ToList();
                //Commision Calculation
                HotelDAL.HotelDA HTLST = new HotelDAL.HotelDA();
               // DataTable commisiondt = HTLST.GetDetailsPageCommision(HotelDetails.Country, HotelDetails.SearchCity, Star, HotelDetails.AgentID, AgencyType, cashbak[0].hotelPrice - cashbak[0].AgtMrk).Tables[0];
               // str.Append("<table style='background-color:#fff;float:right;z-index:1;padding:11px;font-size:12px; overflow:visible;border-radius: 4px; line-height:130%;border:thin solid #333;width:200px;'>");
                str.Append("<tr><td class='bld'>Total : </td><td> <img src='Images/htlrs.png' class='pagingSize' /> " + hotelrate.ToString() + "</td></tr>");
               // str.Append("<tr><td class='bld'>Commision : </td><td>- <img src='Images/htlrs.png' class='pagingSize' /> " + commisiondt.Rows[0]["CommisionAmt"].ToString() + "</td></tr>");
                //str.Append("<tr><td class='bld'>Markups : </td><td>- <img src='Images/htlrs.png' class='pagingSize' /> " + cashbak[0].AgtMrk.ToString() + "</td></tr>");
                //str.Append("<tr><td class='bld'>Net Cost : </td><td> <img src='Images/htlrs.png' class='pagingSize' /> " + commisiondt.Rows[0]["TotalAmount"].ToString() + "</td></tr>");
                str.Append("</table>");
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HtlResult_HotelCashback");
            str.Append("");
        }
        return str.ToString();
    }


    [WebMethod()]
    public string Get_TG_HotelServices(string HotelCode, string AmenityType)
    {
        string Hotelinclution = "";
        try
        {
            HotelDAL.HotelDA objDa = new HotelDAL.HotelDA();
            Hotelinclution = SetHotelService_TG(objDa.GetHotelServices(HotelCode, "property"));
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "Get_TG_HotelServices");
        }
        return Hotelinclution;
    }
    
    protected string SetHotelService_TG(DataTable HtlServices)
    {
        string InclImg = ""; int i = 0, j = 0, k = 0, l = 0, m = 0, n = 0;
        try
        {
            foreach (DataRow Services in HtlServices.Rows)
            {
                switch (Services["Amenity_id"].ToString().Trim())
                {
                    case "01":
                    case "344":
                        if (n == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/travel_desk.png' title='Tagency Help Desk' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                            n = 0;
                        }
                        break;
                    case "03":
                    case "04":
                        if (i == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Car rental facilities' class='IconImageSize' /><span class='hide'>Car rental facilities</span>";
                            i = 1;
                        }
                        break;
                    case "08":
                        InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Banquet_hall' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "09":
                        InclImg += "<img src='../Hotel/Images/Facility/bar.png' title='Mini bar' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "11":
                        InclImg += "<img src='../Hotel/Images/Facility/beauty.png' title='Beauty parlour' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "22":
                        InclImg += "<img src='../Hotel/Images/Facility/babysitting.png' title='Baby sitting' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "24":
                        InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Business centre' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "26":
                        InclImg += "<img src='../Hotel/Images/Facility/Phone.png' title='Direct dial phone' style='margin:0 2px' />";
                        break;
                    case "30":
                    case "32":
                        InclImg += "<img src='../Hotel/Images/Facility/breakfast.png' title='Tea/Coffee' style='margin:0 2px' />";
                        break;
                    case "31":
                    case "44":
                    case "65":
                        if (j == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/lobby.png' title='Lobby' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                            j = 1;
                        }
                        break;
                    case "40":
                        InclImg += "<img src='../Hotel/Images/Facility/elevator.png' title='Lifts' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "52":
                        InclImg += "<img src='../Hotel/Images/Facility/health_club.png' title='Gymnasium' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "54":
                    case "55":
                    case "56":
                    case "57":
                    case "58":
                        if (k == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/Internet.png' title='Internet' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                            k = 1;
                        }
                        break;
                    case "59":
                        InclImg += "<img src='../Hotel/Images/Facility/laundary.png' title='Laundry facilities' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "71":
                    case "72":
                    case "73":
                    case "74":
                    case "75":
                        if (l == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/Parking.png' title='Parking' style='margin:2px;' />";
                            l = 1;
                        }
                        break;
                    case "88":
                        InclImg += "<img src='../Hotel/Images/Facility/sauna.png' title='Sauna' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "122":
                    case "360":
                        InclImg += "<img src='../Hotel/Images/Facility/AC.png' title='AC' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "126":
                    case "325":
                    case "131":
                        if (m == 0)
                        {
                            InclImg += "<img src='../Hotel/Images/Facility/TV.png' title='TV' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                            m = 1;
                        }
                        break;
                    case "334":
                        InclImg += "<img src='../Hotel/Images/Facility/handicap.png' title='Disabled facilities' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "338":
                        InclImg += "<img src='../Hotel/Images/Facility/golf.png' title='Golf' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "345":
                        InclImg = "<img src='../Hotel/Images/Facility/swimming.png' title='Outdoor Swimming Pool' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                    case "355":
                        InclImg += "<img src='../Hotel/Images/Facility/jacuzzi.png' title='Indoor Swimming Pool' class='IconImageSize' /><span class='hide'>Tagency Help Desk</span>";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "SetHotelService_TG");
        }
        return InclImg;
    }
    [WebMethod(EnableSession = true)]
    public int HotelPriceEnquiry(string HotelName)
    {
        HotelSearch HotelDetails = new HotelSearch(); int i = 0;
        try
        {
            if (Session["UID"] != null && Session["HotelSearch"] != null)
            {
                HotelDetails = (HotelSearch)HttpContext.Current.Session["HotelSearch"];
                HotelBAL.HotelSendMail_Log objHtlEnquery = new HotelBAL.HotelSendMail_Log();
                HotelDetails.HotelName = HotelName;
                i = objHtlEnquery.HotelEnquiryEmail(HotelDetails);
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "HotelPriceEnquiry");
        }
        return i;
    }
    [WebMethod(EnableSession = true)]
    public RoomComposite SelectedHotelSearchOld(string HotelName, string HotelCode)
    {
        RoomComposite objRoonCombind = new RoomComposite();
        try
        {
            if (Session["UID"] == null || Session["HotelSearch"] == null)
            {
                List<RoomList> objRoomList = new List<RoomList>();
                objRoomList.Add(new RoomList { Room_Error = "SessionExpired" });
                objRoonCombind.RoomDetails = objRoomList;
            }
            else
            {
                HotelComposite HotelCompositResult = new HotelComposite(); HotelSearch SearchDetails = new HotelSearch();
                HotelCompositResult = (HotelComposite)System.Web.HttpContext.Current.Session["HotelCompositSearch"];
                SearchDetails = (HotelShared.HotelSearch)System.Web.HttpContext.Current.Session["HotelSearch"];
                string[] strhtl = HotelCode.Split('_');
                SearchDetails.HtlCode = strhtl[0];
                SearchDetails.HotelCityCode = strhtl[1];

                List<HotelShared.HotelResult> HtlList = new List<HotelShared.HotelResult>();
                HtlList = HotelCompositResult.Hotelresults;
                //HtlList = (List<HotelShared.HotelResult>)HttpContext.Current.Session["SearchResult"];
                HtlList = HtlList.Where(x => x.HotelCode == SearchDetails.HtlCode && x.HotelCityCode == SearchDetails.HotelCityCode).ToList();
                if ((HtlList.Count > 0))
                {
                    SearchDetails.StarRating = HtlList[0].StarRating;
                    SearchDetails.HotelName = HtlList[0].HotelName;
                    SearchDetails.ThumbImage = HtlList[0].HotelThumbnailImg;
                    SearchDetails.Provider = HtlList[0].Provider;
                }

                HotelBAL.HotelAvailabilitySearch htlresp = new HotelBAL.HotelAvailabilitySearch();
               // objRoonCombind = htlresp.SelectedHotelResponse(SearchDetails, HotelCompositResult, objRoonCombind, HotelName);

                List<HotelShared.HotelResult> HtlList1 = new List<HotelShared.HotelResult>();
                if (HotelCompositResult.HotelOrgList != null)
                {
                    if ((HotelCompositResult.Hotelresults.Count < (HotelCompositResult.HotelOrgList).Count))
                    {
                        HtlList1 = HotelCompositResult.HotelOrgList;
                        HtlList1 = HtlList1.Where(x => x.HotelName.ToUpper().Replace(" HOTEL ", " ").Replace("HOTEL ", " ").Replace(" HOTEL", " ").Replace("  ", " ").Trim() == HotelName.ToUpper().Trim() && x.Provider != SearchDetails.Provider).ToList();
                    }
                }

                List<RoomList> RoomDetals = null;
                if (HtlList1.Count > 0)
                {
                    RoomComposite RommCompositResultTG = new RoomComposite(); RoomComposite RommCompositResultInnstant = new RoomComposite(); RoomComposite RommCompositResultROOMXML = new RoomComposite();
                    RoomComposite RommCompositResultMTS = new RoomComposite(); RoomComposite RommCompositResultGRN = new RoomComposite(); RoomComposite RommCompositResultSuperShopper = new RoomComposite();
                    RoomComposite RommCompositResultExpedia = new RoomComposite();
                    for (int z = 0; z < HtlList1.Count; z++)
                    {
                        switch (HtlList1[z].Provider)
                        {
                            case "TG":
                                SearchDetails.Provider = "TG";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                //RommCompositResultTG = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultTG.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultTG.RoomDetails).ToList();
                                break;
                            case "INNSTANT":
                                SearchDetails.Provider = "INNSTANT";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                //RommCompositResultInnstant = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultInnstant.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultInnstant.RoomDetails).ToList();
                                break;
                            case "ROOMXML":
                                SearchDetails.Provider = "ROOMXML";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                SearchDetails.HotelCityCode = HtlList1[z].HotelCityCode;
                                //RommCompositResultROOMXML = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultROOMXML.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultROOMXML.RoomDetails).ToList();
                                break;
                            case "MTS":
                                SearchDetails.Provider = "MTS";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                SearchDetails.HotelCityCode = HtlList1[z].HotelCityCode;
                               // RommCompositResultMTS = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultMTS.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultMTS.RoomDetails).ToList();
                                break;
                            case "SuperShopper":
                                SearchDetails.Provider = "SuperShopper";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                SearchDetails.HotelCityCode = HtlList1[z].HotelCityCode;
                               // RommCompositResultSuperShopper = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultSuperShopper.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultSuperShopper.RoomDetails).ToList();
                                break;
                            case "GRN":
                                SearchDetails.Provider = "GRN";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                SearchDetails.HotelCityCode = HtlList1[z].HotelCityCode;
                               // RommCompositResultGRN = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultGRN.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultGRN.RoomDetails).ToList();
                                break;
                            case "EXPEDIA":
                                SearchDetails.Provider = "EXPEDIA";
                                SearchDetails.HtlCode = HtlList1[z].HotelCode;
                                SearchDetails.HotelCityCode = HtlList1[z].HotelCityCode;
                               // RommCompositResultExpedia = htlresp.SelectedHotelResponse(SearchDetails);
                                if (RommCompositResultExpedia.RoomDetails.Count > 0)
                                    RoomDetals = objRoonCombind.RoomDetails.Union(RommCompositResultExpedia.RoomDetails).ToList();
                                break;
                        }
                    }
                    RoomDetals = RoomDetals.OrderBy(h => h.TotalRoomrate).ToList();
                    objRoonCombind.RoomDetails = RoomDetals;
                    Session["SelectedHotelDetail"] = objRoonCombind;

                    if (RommCompositResultTG != null)
                    {
                        if (RommCompositResultTG.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultTG.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultTG.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultTG.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultTG.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultTG.SelectedHotelDetail.HotelAmenities;
                        }
                    }
                    if (RommCompositResultInnstant != null)
                    {
                        if (RommCompositResultInnstant.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelSessionID = RommCompositResultInnstant.SelectedHotelDetail.HotelSessionID;
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultInnstant.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultInnstant.SelectedHotelDetail.HotelImage;
                            //objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultInnstant.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultInnstant.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultInnstant.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultInnstant.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultInnstant.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                    if (RommCompositResultROOMXML != null)
                    {
                        if (RommCompositResultROOMXML.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultROOMXML.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultROOMXML.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultROOMXML.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultROOMXML.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultROOMXML.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultROOMXML.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultROOMXML.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                    if (RommCompositResultMTS != null)
                    {
                        if (RommCompositResultMTS.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultMTS.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultMTS.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultMTS.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultMTS.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultMTS.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultMTS.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultMTS.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                    if (RommCompositResultSuperShopper != null)
                    {
                        if (RommCompositResultSuperShopper.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultSuperShopper.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultSuperShopper.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultSuperShopper.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultSuperShopper.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultSuperShopper.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultSuperShopper.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultSuperShopper.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                    if (RommCompositResultGRN != null)
                    {
                        if (RommCompositResultGRN.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultGRN.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultGRN.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultGRN.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultGRN.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultGRN.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultGRN.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultGRN.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                    if (RommCompositResultExpedia != null)
                    {
                        if (RommCompositResultExpedia.SelectedHotelDetail != null)
                        {
                            objRoonCombind.SelectedHotelDetail.HotelDescription += RommCompositResultExpedia.SelectedHotelDetail.HotelDescription;
                            objRoonCombind.SelectedHotelDetail.HotelImage += RommCompositResultExpedia.SelectedHotelDetail.HotelImage;
                            objRoonCombind.SelectedHotelDetail.Attraction += RommCompositResultExpedia.SelectedHotelDetail.Attraction;
                            objRoonCombind.SelectedHotelDetail.RoomAmenities += RommCompositResultExpedia.SelectedHotelDetail.RoomAmenities;
                            objRoonCombind.SelectedHotelDetail.HotelAmenities += RommCompositResultExpedia.SelectedHotelDetail.HotelAmenities;
                            if (RommCompositResultExpedia.SelectedHotelDetail.HotelContactNo != "")
                                SearchDetails.HotelContactNo = RommCompositResultExpedia.SelectedHotelDetail.HotelContactNo;
                        }
                    }
                }
                else
                {
                    RoomDetals = objRoonCombind.RoomDetails.OrderBy(x => x.TotalRoomrate).ToList();
                    objRoonCombind.RoomDetails = RoomDetals;

                    Session["SelectedHotelDetail"] = objRoonCombind;
                    if (SearchDetails.Provider == "ROOMXML" || SearchDetails.Provider == "INNSTANT")
                    {
                        SearchDetails.HotelContactNo = objRoonCombind.SelectedHotelDetail.HotelContactNo;
                    }
                    if (SearchDetails.Provider == "GTA")
                    {
                        SearchDetails.HotelContactNo = objRoonCombind.SelectedHotelDetail.HotelContactNo;
                        //Show No of Room Updated
                        if (SearchDetails.ExtraRoom > 0)
                        {
                            //   ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "ShowRoomAlertMassage('" + Htlsearch.HotelName + " can not alowed 4 Adult and Child  in One Room, So we added one more " + Htlsearch.ExtraRoomType + ".');", true);
                            //  htlrmslbl.Text = (Htlsearch.NoofRoom + Htlsearch.ExtraRoom).ToString();
                        }
                        //Show No of Cot requested
                        if (SearchDetails.ExtraCot != "0")
                        {
                            // cots.Visible = true;
                            // cots1.Text = SearchDetails.ExtraCot;
                            // ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "ShowAlertMassage();", true);

                        }
                    }
                }
                SearchDetails.HotelName = HotelName;
                Session["HotelSearcDetailss"] = SearchDetails;
            }
        }
        catch (Exception ex)
        {
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "SelectedHotelSearch_" + HotelName + "_" + HotelCode);
        }
        return objRoonCombind;
    }
   

    public void exporttexttoexel()
    {
        List<string> lstColumnNames = new List<string>();
        List<List<string>> lstRowData = new List<List<string>>();
        try
        {
            System.IO.StreamReader file =
             new System.IO.StreamReader("D:\\ActivePropertyList.txt");
            string line = "";
            int counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    lstColumnNames.AddRange(line.Split('|'));
                }
                else
                {
                    List<string> tempRowData = new List<string>();
                    tempRowData.AddRange(line.Split('|'));
                    lstRowData.Add(tempRowData);
                }
                counter++;
            }
            System.IO.TextWriter tw = new System.IO.StreamWriter("D:\\Expedia.xlsx");

            if (lstColumnNames.Count != 0)
            {
                string temp = "";
                foreach (string str in lstColumnNames)
                {
                    if (temp != "")
                    {
                        temp += ";";
                    }
                    temp += str;
                }
                tw.WriteLine(temp);


                foreach (List<string> lstRow in lstRowData)
                {
                    temp = "";

                    foreach (string str in lstRow)
                    {
                        if (temp != "")
                        {
                            temp += ";";
                        }
                        temp += str;
                    }
                    tw.WriteLine(temp);
                }
                tw.Close();
            }
        }
        catch (Exception ex)
        {
            string a = "";
        
        }
    }
}

