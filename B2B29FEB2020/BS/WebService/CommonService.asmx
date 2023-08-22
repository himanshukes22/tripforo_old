<%@ WebService Language="C#" Class="CommonService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CommonService : System.Web.Services.WebService
{

    BS_BAL.SharedBAL sharedbal; BS_SHARED.SHARED shared;
    [WebMethod]
    #region[Get source autocomplete]
    public List<BS_SHARED.SHARED> getSourceCity(string srcPrefix)
{
        List<BS_SHARED.SHARED> returnsrcList = new List<BS_SHARED.SHARED>();
        try
        {
            sharedbal = new BS_BAL.SharedBAL();
            returnsrcList = sharedbal.getsrcList( HttpUtility.UrlDecode(srcPrefix));
        }
        catch (Exception ex)
        {

        }
        return returnsrcList;
    }
    #endregion
    [WebMethod]
    #region[Get destination autocomplete]
    public List<BS_SHARED.SHARED> getDestCity(string destPrefix,string sourceNmae)
    {
        List<BS_SHARED.SHARED> returndestList = new List<BS_SHARED.SHARED>();
        try
        {
            sharedbal = new BS_BAL.SharedBAL();
            returndestList = sharedbal.getdestList( HttpUtility.UrlDecode(destPrefix),  HttpUtility.UrlDecode(sourceNmae));
        }
        catch (Exception ex)
        {

        }
        return returndestList;
    }
    #endregion
    [WebMethod]
    #region[Insert destination list into DB]
    public void InsertDestination(string srcID)
    {
       
        BS_SHARED.SHARED shared = new BS_SHARED.SHARED();
        try
        {
            sharedbal = new BS_BAL.SharedBAL();

            //-----------------------get src id-----------------------------------//
             shared.src =  HttpUtility.UrlDecode(srcID.Trim());
             sharedbal.InsertDestination(shared);
            //-----------------------end-------------------------------------------//
        }
        catch (Exception ex)
        {

        }
    }
    #endregion
    [WebMethod(EnableSession = true)]
    #region[Get journey Result]
    public List<BS_SHARED.SHARED> getJourneyResult(string src, string dest, string jDate, string noofpax, string seattype)
    {
        List<BS_SHARED.SHARED> resultList = new List<BS_SHARED.SHARED>();
        try
        {
            shared = new BS_SHARED.SHARED();
            sharedbal = new BS_BAL.SharedBAL();
            shared.src =HttpUtility.UrlDecode(src.Trim());
            shared.dest = HttpUtility.UrlDecode(dest.Trim());
            shared.journeyDate =  HttpUtility.UrlDecode(jDate.Trim());
            shared.NoOfPax = HttpUtility.UrlDecode( noofpax.Trim());
            shared.SeatType = HttpUtility.UrlDecode(seattype.Trim());
            shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
            resultList = sharedbal.getMergeJourneyList(shared);
        }
        catch (Exception ex)
        {

        }
        return resultList;
    }
    #endregion
    [WebMethod(EnableSession = true)]
    #region[Seat LayOut]
    public string getSeatLayOut(string jdate, string srcId, string destId, string serviceId, string seattype, string provider, string fare, string traveler, string farewithMarkp)
    {
        shared = new BS_SHARED.SHARED(); sharedbal = new BS_BAL.SharedBAL(); string layout = "";
        try
        {
            shared.journeyDate =  HttpUtility.UrlDecode(jdate);
            shared.src =  HttpUtility.UrlDecode(srcId.Trim());
            shared.dest =  HttpUtility.UrlDecode(destId.Trim());
            shared.SeatType =  HttpUtility.UrlDecode(seattype.Trim());
            shared.serviceID =  HttpUtility.UrlDecode(serviceId.Trim());
            shared.provider_name =  HttpUtility.UrlDecode(provider);
            shared.seatfare =  HttpUtility.UrlDecode(fare.Trim());
            shared.seatfarewithMarkp =  HttpUtility.UrlDecode(farewithMarkp.Trim());
            shared.traveler =  HttpUtility.UrlDecode(traveler.Trim());
            shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
            layout = sharedbal.getBusSeatLayout(shared);
        }
        catch (Exception ex)
        {

        }
        return layout;
    }
    #endregion
    [WebMethod]
    #region[Get order ID]
    public string getOrderID()
    {
        string orderid = "";
        try
        {
            sharedbal = new BS_BAL.SharedBAL();
            orderid = sharedbal.getRand_NUM();
            orderid = (orderid + "O").ToString().Trim();  
            
        }
        catch (Exception ex)
        {

        }
        return orderid;
    }
    #endregion
    [WebMethod(EnableSession = true)]
    #region[Get Commission]
    public List<BS_SHARED.SHARED> getCommissionList(string seatNo, string seatFare, string provider)
    {
        List<BS_SHARED.SHARED> getComm = new List<BS_SHARED.SHARED>();
        sharedbal = new BS_BAL.SharedBAL(); shared = new BS_SHARED.SHARED();
        try
        {
            shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
            shared.seat = HttpUtility.UrlDecode( seatNo.Trim());
            shared.fare =  HttpUtility.UrlDecode(seatFare.Trim());
            shared.provider_name = provider.Trim();
            getComm = sharedbal.getCommissionList(shared);
        }
        catch (Exception ex)
        {

        }
        return getComm;
    }
    #endregion
    [WebMethod(EnableSession = true)]
    #region[Insert selected seat details]
    public string insertSelected_seatDetails(string Oneway, string Return)
    {
        string orderid = "";
        try
        {

            string[] oneWayResult = Oneway.Split('_');

            sharedbal = new BS_BAL.SharedBAL();
            shared = new BS_SHARED.SHARED(); BS_SHARED.SHARED R_shared = new BS_SHARED.SHARED();
            orderid ="BW"+sharedbal.getRand_NUM();
            
            
            if (Return != "")
            {

                shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
                shared.orderID = HttpUtility.UrlDecode(orderid.Trim().Trim() + "O");
                shared.serviceID = HttpUtility.UrlDecode(oneWayResult[0].Trim());
                shared.src = HttpUtility.UrlDecode(oneWayResult[1].Trim());
                shared.dest = HttpUtility.UrlDecode(oneWayResult[2].Trim());
                shared.journeyDate = HttpUtility.UrlDecode(oneWayResult[3].Trim());
                shared.seat = HttpUtility.UrlDecode(oneWayResult[4].Trim()).Remove(HttpUtility.UrlDecode(oneWayResult[4].Trim()).LastIndexOf(",")).Trim();
                shared.ladiesSeat = HttpUtility.UrlDecode(oneWayResult[5].Trim());
                shared.fare = HttpUtility.UrlDecode(oneWayResult[6].Trim()).Remove(HttpUtility.UrlDecode(oneWayResult[6].Trim()).LastIndexOf(",")).Trim();
                shared.traveler = HttpUtility.UrlDecode(oneWayResult[7].Trim());
                shared.busType = HttpUtility.UrlDecode(oneWayResult[8].Trim());
                shared.boardpoint = HttpUtility.UrlDecode(oneWayResult[9].Trim());
                shared.droppoint = HttpUtility.UrlDecode(oneWayResult[10].Trim());
                shared.adcomm = Convert.ToDecimal(oneWayResult[11].ToString().Trim());
                shared.taTds = Convert.ToDecimal(oneWayResult[12].Trim());
                shared.totFare = Convert.ToDecimal(oneWayResult[13].Trim());
                shared.taTotFare = Convert.ToDecimal(oneWayResult[14].Trim());
                shared.taNetFare = Convert.ToDecimal(oneWayResult[15].Trim());
                shared.idproofReq = HttpUtility.UrlDecode(oneWayResult[16].Trim());
                shared.admrkp = Convert.ToDecimal(oneWayResult[17].Trim());
                shared.agmrkp = Convert.ToDecimal(oneWayResult[18].Trim());
                shared.originalfare = oneWayResult[19].Trim().Remove(oneWayResult[19].Trim().LastIndexOf(",")).Trim();
                shared.NoOfPax = HttpUtility.UrlDecode(oneWayResult[20].Trim());
                shared.provider_name = HttpUtility.UrlDecode(oneWayResult[21].Trim());
                shared.canPolicy = HttpUtility.UrlDecode(oneWayResult[22].Trim());
                shared.partialCanAllowed = HttpUtility.UrlDecode(oneWayResult[23].Trim());
                shared.arrTime = HttpUtility.UrlDecode(shared.droppoint).Trim().Substring(HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.droppoint).Trim().Length - HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf(')') - 1);
                shared.departTime = HttpUtility.UrlDecode(shared.boardpoint).Trim().Substring(HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.boardpoint).Trim().Length - HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf(')') - 1);
                if (shared.provider_name == "GS")
                    shared.Dur_Time = HttpUtility.UrlDecode(shared.serviceID).Trim().Split(',')[5];
                else
                    shared.Dur_Time = "00";
                shared.AC_NONAC = HttpUtility.UrlDecode(oneWayResult[24].Trim());
                shared.SEAT_TYPE = HttpUtility.UrlDecode(oneWayResult[25].Trim());
                shared.WithTaxes = HttpUtility.UrlDecode(oneWayResult[26].Trim());
                shared.totWithTaxes = HttpUtility.UrlDecode(oneWayResult[27].Trim());

                string[] ReturnResult = Return.Split('_');
                R_shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
                R_shared.orderID = HttpUtility.UrlDecode(orderid.Trim().Trim() + "R");
                R_shared.serviceID = HttpUtility.UrlDecode(ReturnResult[0].Trim());
                R_shared.src = HttpUtility.UrlDecode(ReturnResult[1].Trim());
                R_shared.dest = HttpUtility.UrlDecode(ReturnResult[2].Trim());
                R_shared.journeyDate = HttpUtility.UrlDecode(ReturnResult[3].Trim());
                R_shared.seat = HttpUtility.UrlDecode(ReturnResult[4].Trim()).Remove(HttpUtility.UrlDecode(ReturnResult[4].Trim()).LastIndexOf(",")).Trim();
                R_shared.ladiesSeat = HttpUtility.UrlDecode(ReturnResult[5].Trim());
                R_shared.fare = HttpUtility.UrlDecode(ReturnResult[6].Trim()).Remove(HttpUtility.UrlDecode(ReturnResult[6].Trim()).LastIndexOf(",")).Trim();
                R_shared.traveler = HttpUtility.UrlDecode(ReturnResult[7].Trim());
                R_shared.busType = HttpUtility.UrlDecode(ReturnResult[8].Trim());
                R_shared.boardpoint = HttpUtility.UrlDecode(ReturnResult[9].Trim());
                R_shared.droppoint = HttpUtility.UrlDecode(ReturnResult[10].Trim());
                R_shared.adcomm = Convert.ToDecimal(ReturnResult[11].ToString().Trim());
                R_shared.taTds = Convert.ToDecimal(ReturnResult[12].Trim());
                R_shared.totFare = Convert.ToDecimal(ReturnResult[13].Trim());
                R_shared.taTotFare = Convert.ToDecimal(ReturnResult[14].Trim());
                R_shared.taNetFare = Convert.ToDecimal(ReturnResult[15].Trim());
                R_shared.idproofReq = HttpUtility.UrlDecode(ReturnResult[16].Trim());
                R_shared.admrkp = Convert.ToDecimal(ReturnResult[17].Trim());
                R_shared.agmrkp = Convert.ToDecimal(ReturnResult[18].Trim());
                R_shared.originalfare = ReturnResult[19].Trim().Remove(ReturnResult[19].Trim().LastIndexOf(",")).Trim();
                R_shared.NoOfPax = HttpUtility.UrlDecode(ReturnResult[20].Trim());
                R_shared.provider_name = HttpUtility.UrlDecode(ReturnResult[21].Trim());
                R_shared.canPolicy = HttpUtility.UrlDecode(ReturnResult[22].Trim());
                R_shared.partialCanAllowed = HttpUtility.UrlDecode(ReturnResult[23].Trim());
                R_shared.arrTime = HttpUtility.UrlDecode(shared.droppoint).Trim().Substring(HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.droppoint).Trim().Length - HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf(')') - 1);
                R_shared.departTime = HttpUtility.UrlDecode(shared.boardpoint).Trim().Substring(HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.boardpoint).Trim().Length - HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf(')') - 1);
                if (R_shared.provider_name == "GS")
                    R_shared.Dur_Time = HttpUtility.UrlDecode(R_shared.serviceID).Trim().Split(',')[5];
                else
                    R_shared.Dur_Time = "00";
                R_shared.AC_NONAC = HttpUtility.UrlDecode(ReturnResult[24].Trim());
                R_shared.SEAT_TYPE = HttpUtility.UrlDecode(ReturnResult[25].Trim());
                R_shared.WithTaxes = HttpUtility.UrlDecode(ReturnResult[26].Trim());
                R_shared.totWithTaxes = HttpUtility.UrlDecode(ReturnResult[27].Trim());

                sharedbal.insertselected_seatDetails(shared);
                sharedbal.insertselected_seatDetails(R_shared);
            }
            else
            {
                shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
                shared.orderID = HttpUtility.UrlDecode(orderid.Trim().Trim() + "O");
                shared.serviceID = HttpUtility.UrlDecode(oneWayResult[0].Trim());
                shared.src = HttpUtility.UrlDecode(oneWayResult[1].Trim());
                shared.dest = HttpUtility.UrlDecode(oneWayResult[2].Trim());
                shared.journeyDate = HttpUtility.UrlDecode(oneWayResult[3].Trim());
                shared.seat = HttpUtility.UrlDecode(oneWayResult[4].Trim()).Remove(HttpUtility.UrlDecode(oneWayResult[4].Trim()).LastIndexOf(",")).Trim();
                shared.ladiesSeat = HttpUtility.UrlDecode(oneWayResult[5].Trim());
                shared.fare = HttpUtility.UrlDecode(oneWayResult[6].Trim()).Remove(HttpUtility.UrlDecode(oneWayResult[6].Trim()).LastIndexOf(",")).Trim();
                shared.traveler = HttpUtility.UrlDecode(oneWayResult[7].Trim());
                shared.busType = HttpUtility.UrlDecode(oneWayResult[8].Trim());
                shared.boardpoint = HttpUtility.UrlDecode(oneWayResult[9].Trim());
                shared.droppoint = HttpUtility.UrlDecode(oneWayResult[10].Trim());
                shared.adcomm = Convert.ToDecimal(oneWayResult[11].ToString().Trim());
                shared.taTds = Convert.ToDecimal(oneWayResult[12].Trim());
                shared.totFare = Convert.ToDecimal(oneWayResult[13].Trim());
                shared.taTotFare = Convert.ToDecimal(oneWayResult[14].Trim());
                shared.taNetFare = Convert.ToDecimal(oneWayResult[15].Trim());
                shared.idproofReq = HttpUtility.UrlDecode(oneWayResult[16].Trim());
                shared.admrkp = Convert.ToDecimal(oneWayResult[17].Trim());
                shared.agmrkp = Convert.ToDecimal(oneWayResult[18].Trim());
                shared.originalfare = oneWayResult[19].Trim().Remove(oneWayResult[19].Trim().LastIndexOf(",")).Trim();
                shared.NoOfPax = HttpUtility.UrlDecode(oneWayResult[20].Trim());
                shared.provider_name = HttpUtility.UrlDecode(oneWayResult[21].Trim());
                shared.canPolicy = HttpUtility.UrlDecode(oneWayResult[22].Trim());
                shared.partialCanAllowed = HttpUtility.UrlDecode(oneWayResult[23].Trim());
                shared.arrTime = HttpUtility.UrlDecode(shared.droppoint).Trim().Substring(HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.droppoint).Trim().Length - HttpUtility.UrlDecode(shared.droppoint).Trim().LastIndexOf(')') - 1);
                shared.departTime = HttpUtility.UrlDecode(shared.boardpoint).Trim().Substring(HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(shared.boardpoint).Trim().Length - HttpUtility.UrlDecode(shared.boardpoint).Trim().LastIndexOf(')') - 1);
                if (shared.provider_name == "GS")
                    shared.Dur_Time = HttpUtility.UrlDecode(shared.serviceID).Trim().Split(',')[5];
                else
                    shared.Dur_Time = "00";
                shared.AC_NONAC = HttpUtility.UrlDecode(oneWayResult[24].Trim());
                shared.SEAT_TYPE = HttpUtility.UrlDecode(oneWayResult[25].Trim());
                shared.WithTaxes = HttpUtility.UrlDecode(oneWayResult[26].Trim());
                shared.totWithTaxes = HttpUtility.UrlDecode(oneWayResult[27].Trim());

                sharedbal.insertselected_seatDetails(shared);
                //shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
                //shared.orderID = HttpUtility.UrlDecode(orderid.Trim().Trim());
                //shared.serviceID = HttpUtility.UrlDecode(tripid.Trim());
                //shared.src = HttpUtility.UrlDecode(src.Trim());
                //shared.dest = HttpUtility.UrlDecode(dest.Trim());
                //shared.journeyDate = HttpUtility.UrlDecode(jrdate.Trim());
                //shared.seat = HttpUtility.UrlDecode(seatno).Remove(HttpUtility.UrlDecode(seatno).LastIndexOf(",")).Trim();
                //shared.ladiesSeat = HttpUtility.UrlDecode(ladiesseat.Trim());
                //shared.fare = HttpUtility.UrlDecode(fare).Remove(HttpUtility.UrlDecode(fare).LastIndexOf(",")).Trim();
                //shared.traveler = HttpUtility.UrlDecode(busoperator.Trim());
                //shared.busType = HttpUtility.UrlDecode(bustype.Trim());
                //shared.boardpoint = HttpUtility.UrlDecode(bdpoint.Trim());
                //shared.droppoint = HttpUtility.UrlDecode(dppoint.Trim());
                //shared.adcomm = Convert.ToDecimal(adcomm.Trim());
                //shared.taTds = Convert.ToDecimal(tatds.Trim());
                //shared.totFare = Convert.ToDecimal(totfare.Trim());
                //shared.taTotFare = Convert.ToDecimal(tatotfare.Trim());
                //shared.taNetFare = Convert.ToDecimal(tanetfare.Trim());
                //shared.idproofReq = HttpUtility.UrlDecode(idproof.Trim());
                //shared.admrkp = Convert.ToDecimal(adminMrkp.Trim());
                //shared.agmrkp = Convert.ToDecimal(agMrkp.Trim());
                //shared.originalfare = originalPrice.Remove(originalPrice.LastIndexOf(",")).Trim();
                //shared.NoOfPax = HttpUtility.UrlDecode(noofPax.Trim());
                //shared.provider_name = HttpUtility.UrlDecode(provider.Trim());
                //shared.canPolicy = HttpUtility.UrlDecode(canpolicy.Trim());
                //shared.partialCanAllowed = HttpUtility.UrlDecode(partialcancel.Trim());
                //shared.arrTime = HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().Substring(HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf(')') - HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') - 1);
                //shared.departTime = HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().Substring(HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') + 1, HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf(')') - HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') - 1);
                //if (shared.provider_name == "GS")
                //    shared.Dur_Time = HttpUtility.UrlDecode(shared.serviceID).Trim().Split(',')[5];
                //else
                //    shared.Dur_Time = "00";

                //  sharedbal.insertselected_seatDetails(shared);
            }
        }
        catch (Exception ex)
        { }
        return orderid.ToString();
    }
    ////public void insertSelected_seatDetails(string orderid, string tripid, string src, string dest, string jrdate, string seatno, string ladiesseat, string fare, string busoperator, string bustype, string bdpoint, string dppoint, string adcomm, string tatds, string totfare, string tatotfare, string tanetfare, string idproof, string adminMrkp, string agMrkp, string originalPrice, string noofPax, string provider, string canpolicy, string partialcancel, string seattype)
    ////{
    ////    try
    ////    {
    ////        sharedbal = new BS_BAL.SharedBAL();
    ////        shared = new BS_SHARED.SHARED();
    ////        shared.agentID = HttpContext.Current.Session["UID"].ToString().Trim();
    ////        shared.orderID =  HttpUtility.UrlDecode(orderid.Trim().Trim());
    ////        shared.serviceID =  HttpUtility.UrlDecode(tripid.Trim());
    ////        shared.src =  HttpUtility.UrlDecode(src.Trim());
    ////        shared.dest =  HttpUtility.UrlDecode(dest.Trim());
    ////        shared.journeyDate =  HttpUtility.UrlDecode(jrdate.Trim());
    ////        shared.seat = HttpUtility.UrlDecode(seatno).Remove( HttpUtility.UrlDecode(seatno).LastIndexOf(",")).Trim();
    ////        shared.ladiesSeat =  HttpUtility.UrlDecode(ladiesseat.Trim());
    ////        shared.fare =  HttpUtility.UrlDecode(fare).Remove( HttpUtility.UrlDecode(fare).LastIndexOf(",")).Trim();
    ////        shared.traveler =  HttpUtility.UrlDecode(busoperator.Trim());
    ////        shared.busType =  HttpUtility.UrlDecode(bustype.Trim());
    ////        shared.boardpoint =  HttpUtility.UrlDecode(bdpoint.Trim());
    ////        shared.droppoint =  HttpUtility.UrlDecode(dppoint.Trim());
    ////        shared.adcomm = Convert.ToDecimal(adcomm.Trim());
    ////        shared.taTds = Convert.ToDecimal(tatds.Trim());
    ////        shared.totFare = Convert.ToDecimal(totfare.Trim());
    ////        shared.taTotFare = Convert.ToDecimal(tatotfare.Trim());
    ////        shared.taNetFare = Convert.ToDecimal(tanetfare.Trim());
    ////        shared.idproofReq =  HttpUtility.UrlDecode(idproof.Trim());
    ////        shared.admrkp = Convert.ToDecimal(adminMrkp.Trim());
    ////        shared.agmrkp = Convert.ToDecimal(agMrkp.Trim());
    ////        shared.originalfare = originalPrice.Remove(originalPrice.LastIndexOf(",")).Trim();
    ////        shared.NoOfPax = HttpUtility.UrlDecode( noofPax.Trim());
    ////        shared.provider_name =  HttpUtility.UrlDecode(provider.Trim());
    ////        shared.canPolicy =  HttpUtility.UrlDecode(canpolicy.Trim());
    ////        shared.partialCanAllowed =  HttpUtility.UrlDecode(partialcancel.Trim());
    ////        shared.arrTime =  HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().Substring( HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') + 1,  HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf(')') - HttpUtility.UrlDecode(bdpoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') - 1);
    ////        shared.departTime =  HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().Substring( HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') + 1,  HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf(')') -  HttpUtility.UrlDecode(dppoint).Trim().Trim().Split('&')[0].Trim().LastIndexOf('(') - 1);
    ////        shared.SeatType = HttpUtility.UrlDecode(seattype.Trim());
           
            
    ////        if (shared.provider_name == "GS")
    ////            shared.Dur_Time =  HttpUtility.UrlDecode(shared.serviceID).Trim().Split(',')[5];
    ////        else
    ////            shared.Dur_Time = "00";
            
    ////        sharedbal.insertselected_seatDetails(shared);
    ////    }
    ////    catch (Exception ex)
    ////    { }
    ////}
    #endregion
    [WebMethod]
    #region[Get Ticket Copy]
    public List<List<BS_SHARED.SHARED>> getTicketCopy(string tin, string orderid)
    {
        List<List<BS_SHARED.SHARED>> list = new List<List<BS_SHARED.SHARED>>();

        sharedbal = new BS_BAL.SharedBAL();
        try
        {
            shared = new BS_SHARED.SHARED();
            if (orderid.Substring(orderid.Length - 1) != "*")
            {
                shared.orderID = HttpUtility.UrlDecode(orderid);
                shared.tin = HttpUtility.UrlDecode(tin);
                list.Add(sharedbal.getticketcopy(shared));
            }
            else
            {
                for (int t = 0; t < HttpUtility.UrlDecode(orderid).Split('*').Length - 1; t++)
                {

                    if (t == 0)
                    {
                        shared.orderID = HttpUtility.UrlDecode(orderid.Split('*')[t]);
                        shared.tin = HttpUtility.UrlDecode(tin.Split('*')[t]);
                        list.Add(sharedbal.getticketcopy(shared));

                    }
                    else
                    {
                        shared.orderID = HttpUtility.UrlDecode(orderid.Split('*')[t]);
                        shared.tin = HttpUtility.UrlDecode(tin.Split('*')[t]);
                        list.Add(sharedbal.getticketcopy(shared));
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        return list;
    }
    ////public List<BS_SHARED.SHARED> getTicketCopy(string tin, string orderid ,string providerNmae)
    ////{
    ////    List<BS_SHARED.SHARED> list = new List<BS_SHARED.SHARED>();
    ////    sharedbal = new BS_BAL.SharedBAL();
    ////    try
    ////    {
    ////        shared = new BS_SHARED.SHARED();
    ////        shared.orderID =  HttpUtility.UrlDecode(orderid);
    ////        shared.tin =  HttpUtility.UrlDecode(tin);
    ////        shared.provider_name = HttpUtility.UrlDecode(providerNmae);
    ////        list = sharedbal.getticketcopy(shared);
    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }
    ////    return list;
    ////}
    #endregion

    [WebMethod]
    #region[Get BoardIngDropping Point]
    public string[][] getBoardIngDropping(string serviceId)
    {
        string [][] BoardIngDropping = new string[1][];
        GsrtcService GsrtcService = new GsrtcService();
        try
        {
            
          //  BoardIngDropping = GsrtcService.GetboardingPoint( HttpUtility.UrlDecode(serviceId),"","","","","","","","","");
        }
        catch (Exception ex)
        {

        }
        return BoardIngDropping;
    }
    #endregion

}






