<%@ WebService Language="C#" Class="PNRCancellation" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using GALWS;
using OnlineCancellationSHARED;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PNRCancellation : System.Web.Services.WebService
{
    string con = System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString;
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

    [WebMethod]
    public string GetCancellationSegmentDetails(string OrderID, string PaxID, string Opr)
    {



        List<OnlineCancellationSHARED.SegmentDetail> ObjS = new List<OnlineCancellationSHARED.SegmentDetail>();
        List<OnlineCancellationSHARED.PaxSegments> Obj = new List<OnlineCancellationSHARED.PaxSegments>();
        try
        {




            OnlineCancellationBAL.PNRCancellationBAL objBal = new OnlineCancellationBAL.PNRCancellationBAL(con);
            ObjS = objBal.GetCancellationSegmentDetails(OrderID, PaxID, Opr);


            if (ObjS.Count() > 0 && ObjS[0].Error != "" && ObjS[0].Error != null)
            {
                Obj.Add(new OnlineCancellationSHARED.PaxSegments
                {
                    Error = ObjS[0].Error,
                });
            }
            else
            {
                var Paxids = ObjS.Select(x => x.PaxId).Distinct().ToList();
                for (int i = 0; i < Paxids.Count; i++)
                {
                    Obj.Add(new OnlineCancellationSHARED.PaxSegments
                    {
                        PaxId = Paxids[i],
                        Sector = ObjS.Where(x => x.PaxId == Paxids[i]).Select(x => x.Sector).Distinct().FirstOrDefault(),
                        lstPaxSeg = ObjS.Where(x => x.PaxId == Paxids[i]).ToList(),

                    });
                }
            }

        }
        catch (Exception ex)
        {
            //ExecptionLogger.FileHandling("PNRCancellation(GetTicketStatus)", "Error_009", ex, "PNRCancellation");
        }

        return jsSerializer.Serialize(Obj);

    }

    [WebMethod]
    public string GetTicketStatusBySegment(string orderid, string segment, string paxid)
    {

        string jsonSt = "";
        try
        {
            OnlineCancellationBAL.PNRCancellationBAL objBal = new OnlineCancellationBAL.PNRCancellationBAL(con);
            jsonSt = objBal.CheckTicketStatusBySegment(orderid, paxid, segment);
        }
        catch (Exception ex)
        {

        }
        return jsSerializer.Serialize(jsonSt);
    }
    [WebMethod]
    public string CancelPNR(string objPCancDetails, string selectedSegment, string CancelRemark = "")
    {

        string jsonSt = "";
        OnlineCancellationBAL.PNRCancellationBAL objBal = new OnlineCancellationBAL.PNRCancellationBAL(con);
        OnlineCancellationSHARED.PaxList objPCanc = new OnlineCancellationSHARED.PaxList();
        string Provider = "";

        List<OnlineCancellationSHARED.SegmentDetail> totalPaxSegList = null;
        List<OnlineCancellationSHARED.SegmentDetail> selectedPaxSegList = null;
        string Result = "";
        string Refund = "";
        DataTable dtPaxSeg = null;
        OnlineCancellationSHARED.ObjCancelPNR CanObj = new OnlineCancellationSHARED.ObjCancelPNR();
        try
        {
            List<OnlineCancellationSHARED.PaxList> objPCancList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OnlineCancellationSHARED.PaxList>>(objPCancDetails);
            objPCanc = objPCancList[0];
            totalPaxSegList = objBal.GetCancellationSegmentDetails(objPCanc.OrderID.Trim(), "", "GET");
            if (String.IsNullOrEmpty(selectedSegment) == false)
                selectedPaxSegList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OnlineCancellationSHARED.SegmentDetail>>(selectedSegment);

            Provider = selectedPaxSegList[0].Provider.Trim();
            OnlineCancellationDAL.PNRCancellationDAL fltSDal = new OnlineCancellationDAL.PNRCancellationDAL(con);
            string RefNo = fltSDal.GetFlightTrackID();
            CanObj.OrderID = objPCanc.OrderID.Trim();
            CanObj.IPAdd = "";//Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]).Trim();
            CanObj.ReqRemarks = CancelRemark;
            dtPaxSeg = new DataTable();
            dtPaxSeg.Columns.Add("SNO"); dtPaxSeg.Columns.Add("PaxID"); dtPaxSeg.Columns.Add("SECTOR"); dtPaxSeg.Columns.Add("SEGMENT"); dtPaxSeg.Columns.Add("FLTID"); dtPaxSeg.Columns.Add("REFNO"); dtPaxSeg.Columns.Add("TKTNO");
            for (int i = 0; i < selectedPaxSegList.Count; i++)
            {
                DataRow drKeys = dtPaxSeg.NewRow();
                drKeys["SNO"] = (i + 1);
                drKeys["PaxID"] = selectedPaxSegList[i].PaxId.Trim();
                drKeys["SECTOR"] = selectedPaxSegList[i].Sector.Replace("-", ":").Trim();
                drKeys["SEGMENT"] = selectedPaxSegList[i].Segment.Trim();
                drKeys["FLTID"] = selectedPaxSegList[i].FltId.Trim();
                drKeys["REFNO"] = RefNo.Trim();
                drKeys["TKTNO"] = selectedPaxSegList[i].TicketNumber.Trim();
                dtPaxSeg.Rows.Add(drKeys);
            }

            CanObj.PAXSEG = dtPaxSeg;
            CanObj.PAXSEG.TableName = "T_AllKeys";

            Result = objBal.CancelBooking(totalPaxSegList, selectedPaxSegList, objPCanc.OrderID.Trim(), CanObj, RefNo);
            try
            {
                #region CancellationRefund
                if (Result.Split('_').Count() == 3 && Result.Split('_')[0] == "Success" && Provider == "LCC")
                {
                    CanRefund canRefnd = new CanRefund();

                    OnlineCancellationDAL.PNRCancellationDAL objDA = new OnlineCancellationDAL.PNRCancellationDAL(con);
                    DataSet FltHdrDs = objDA.GetHdrDetails(CanObj.OrderID);

                    OnlineCancellationBAL.PNRCancellationBAL objBA = new OnlineCancellationBAL.PNRCancellationBAL(con);


                    CancellationCharge canCharge = new CancellationCharge();
                    canCharge.AgentID = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]);
                    canCharge.AirlineCode = totalPaxSegList[0].VC;
                    canCharge.Mode = "ONLINE";
                    canCharge.Trip = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Trip"]).ToString();
                    string ResultFare = "";
                    string   CanChargeByAdmin = objBal.GetCancellationCharge(canCharge);
                    string tds = "0"; //Convert.ToString(Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["tds"].ToString()) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));
                    string disc =  Convert.ToString(Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["Disc"].ToString()) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));


                    CanChargeByAdmin = Convert.ToString(float.Parse(CanChargeByAdmin) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));

                    string Totalcharge = Convert.ToString(float.Parse(CanChargeByAdmin) + float.Parse(tds)+ float.Parse(disc));

                    string result = "";


                    if (float.Parse(Result.Split('_')[1]) <= 0)
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) - float.Parse(Totalcharge));
                        if (float.Parse(ResultFare) >= 0)
                        {
                            result = objBA.AddToLimit(Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]), float.Parse(ResultFare));
                        }
                        else
                        {
                            result = objBA.UpdateCrdLimit(Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]), float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare)))));
                        }
                    }
                    else
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) + float.Parse(Totalcharge));
                        result = objBA.UpdateCrdLimit(Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]), float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare)))));
                    }





                    canRefnd.TRACKID = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["OrderId"]);
                    canRefnd.PNRNO = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["GDSPnr"]);
                    canRefnd.AGENTID = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]);
                    canRefnd.AGENCYNAME = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AGencyName"]);
                    canRefnd.Provider = totalPaxSegList[0].VC; //"LCC";
                    canRefnd.AVAILBAL = Convert.ToSingle(result);


                    //if (float.Parse(Result.Split('_')[1]) <= 0)
                    //    canRefnd.CreditAmount = float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) -float.Parse( CanChargeByAdmin);
                    //else
                    //    canRefnd.DebitAmount = float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) + float.Parse(CanChargeByAdmin); //float.Parse(Result.Split('_')[1]);

                    if (float.Parse(Result.Split('_')[1]) <= 0)
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) - float.Parse(Totalcharge));
                        if (float.Parse(ResultFare) >= 0)
                        {
                            canRefnd.CreditAmount = float.Parse(ResultFare);
                        }
                        else
                        {
                            canRefnd.DebitAmount = float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare))));

                        }
                    }
                    else
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) + float.Parse(Totalcharge));
                        canRefnd.DebitAmount =  float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare))));
                    }

                    canRefnd.TRIP = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Trip"]);
                    canRefnd.RefNo = RefNo;
                    decimal totalrefund =  (Math.Abs(Convert.ToDecimal(Result.Split('_')[1])));

                    //  TotalBookingCost
                    // canRefnd.CancellationChg = Convert.ToString(Convert.ToDecimal(FltHdrDs.Tables[0].Rows[0]["TotalAfterDis"].ToString()) - totalrefund);
                    canRefnd.CancellationChg = Convert.ToString((Math.Abs(Convert.ToDecimal(Result.Split('_')[2]))) - totalrefund);
                    canRefnd.ServiceChg = CanChargeByAdmin;



                    if (float.Parse(Result.Split('_')[1]) <= 0)
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) - float.Parse(Totalcharge));

                        if (float.Parse(ResultFare) >= 0)
                        {
                            canRefnd.RefundAmt = Convert.ToString(ResultFare);
                        }
                        else
                        {
                            canRefnd.RefundAmt = Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare)));
                        }
                    }
                    else
                    {
                        ResultFare = Convert.ToString(float.Parse(Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1])))) + float.Parse(Totalcharge));
                        canRefnd.RefundAmt = Convert.ToString(Math.Abs(Convert.ToDecimal(ResultFare)));
                    }



                    //if (float.Parse(Result.Split('_')[1]) <= 0)
                    //{
                    //    canRefnd.RefundAmt = Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1]))- Convert.ToDecimal( CanChargeByAdmin));
                    //}
                    //else
                    //{
                    //    canRefnd.RefundAmt = Convert.ToString(Math.Abs(Convert.ToDecimal(Result.Split('_')[1]))+ Convert.ToDecimal( CanChargeByAdmin));
                    //}


                    Refund = objBA.UpdateRefundToLedger(canRefnd);

                    if (Refund.Trim().ToUpper().Contains("SUCCESSFULL"))
                    {
                        Result = "Ticket cancelled successfully."; ;
                        //TempData["CancelQueueMsg"] = "Ticket cancelled successfully.";
                        // return RedirectToAction("GetReportImportPNRCancellation", "PNRCancellation");
                    }
                    else
                    {
                        Result = "Ticket cancelled successfully."; ;
                        //TempData["CancelQueueMsg"] = "Ticket cancelled successfully.";
                        // return RedirectToAction("GetReportImportPNRCancellation", "PNRCancellation");
                    }

                }
                #endregion
            }
            catch (Exception)
            {
                //ExecptionLogger.FileHandling("PNRCancellation(Refund)", "Error_009", ex, "PNRCancellation");
                //TempData["CancelQueueMsg"] = ex.Message.ToString();
            }

        }
        catch (Exception ex)
        {


        }

        return jsSerializer.Serialize(Result);
    }

    [WebMethod]
    public string CheckCancelAmount(string objPCancDetails, string selectedSegment, string CancelRemark = "")
    {

        string jsonSt = "";
        OnlineCancellationBAL.PNRCancellationBAL objBal = new OnlineCancellationBAL.PNRCancellationBAL(con);
        OnlineCancellationSHARED.PaxList objPCanc = new OnlineCancellationSHARED.PaxList();
        string Provider = "";

        List<OnlineCancellationSHARED.SegmentDetail> totalPaxSegList = null;
        List<OnlineCancellationSHARED.SegmentDetail> selectedPaxSegList = null;
        string Result = "";
        string Refund = "";
        DataTable dtPaxSeg = null;
        OnlineCancellationSHARED.ObjCancelPNR CanObj = new OnlineCancellationSHARED.ObjCancelPNR();
        try
        {
            List<OnlineCancellationSHARED.PaxList> objPCancList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OnlineCancellationSHARED.PaxList>>(objPCancDetails);
            objPCanc = objPCancList[0];
            totalPaxSegList = objBal.GetCancellationSegmentDetails(objPCanc.OrderID.Trim(), "", "GET");
            if (String.IsNullOrEmpty(selectedSegment) == false)
                selectedPaxSegList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OnlineCancellationSHARED.SegmentDetail>>(selectedSegment);

            Provider = selectedPaxSegList[0].Provider.Trim();
            OnlineCancellationDAL.PNRCancellationDAL fltSDal = new OnlineCancellationDAL.PNRCancellationDAL(con);
            string RefNo = fltSDal.GetFlightTrackID();
            CanObj.OrderID = objPCanc.OrderID.Trim();
            CanObj.ExecID = "";//WebSecurity.CurrentUserName.Trim();
            CanObj.IPAdd = "";//Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]).Trim();
            CanObj.ReqRemarks = CancelRemark;
            dtPaxSeg = new DataTable();
            dtPaxSeg.Columns.Add("SNO"); dtPaxSeg.Columns.Add("PaxID"); dtPaxSeg.Columns.Add("SECTOR"); dtPaxSeg.Columns.Add("SEGMENT"); dtPaxSeg.Columns.Add("FLTID"); dtPaxSeg.Columns.Add("REFNO"); dtPaxSeg.Columns.Add("TKTNO");
            for (int i = 0; i < selectedPaxSegList.Count; i++)
            {
                DataRow drKeys = dtPaxSeg.NewRow();
                drKeys["SNO"] = (i + 1);
                drKeys["PaxID"] = selectedPaxSegList[i].PaxId.Trim();
                drKeys["SECTOR"] = selectedPaxSegList[i].Sector.Replace("-", ":").Trim();
                drKeys["SEGMENT"] = selectedPaxSegList[i].Segment.Trim();
                drKeys["FLTID"] = selectedPaxSegList[i].FltId.Trim();
                drKeys["REFNO"] = RefNo.Trim();
                drKeys["TKTNO"] = selectedPaxSegList[i].TicketNumber.Trim();
                dtPaxSeg.Rows.Add(drKeys);
            }

            CanObj.PAXSEG = dtPaxSeg;
            CanObj.PAXSEG.TableName = "T_AllKeys";

            Result = objBal.CheckCancelBookingAmount(totalPaxSegList, selectedPaxSegList, objPCanc.OrderID.Trim(), CanObj, RefNo);

            OnlineCancellationDAL.PNRCancellationDAL ObjDAL = new OnlineCancellationDAL.PNRCancellationDAL(con);
            ObjDAL.CheckUpdateCancelTicket(objPCanc.OrderID.Trim(), RefNo, "0", "PRECAN", "", "");

            if (Result.Split('_').Count() == 3 && Result.Split('_')[0] == "Success" && Provider == "LCC")
            {
                string CanCharge = "0";
                OnlineCancellationDAL.PNRCancellationDAL objDA = new OnlineCancellationDAL.PNRCancellationDAL(con);
                DataSet FltHdrDs = objDA.GetHdrDetails(CanObj.OrderID);
                CancellationCharge canCharge = new CancellationCharge();
                canCharge.AgentID = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["AgentId"]);
                canCharge.AirlineCode = totalPaxSegList[0].VC;
                canCharge.Mode = "ONLINE";
                canCharge.Trip = Convert.ToString(FltHdrDs.Tables[0].Rows[0]["Trip"]).ToString();
                string   CanChargeByAdmin = objBal.GetCancellationCharge(canCharge);
                string tds = "0";  //Convert.ToString(Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["tds"].ToString()) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));
                string disc =  Convert.ToString(Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["Disc"].ToString()) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));

                CanChargeByAdmin = Convert.ToString(float.Parse(CanChargeByAdmin) * (Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["adult"]) + Convert.ToInt32(FltHdrDs.Tables[0].Rows[0]["child"])));
                string Totalcharge = Convert.ToString(float.Parse(CanChargeByAdmin) + float.Parse(tds) + float.Parse(disc));

                string RefundAmt = "Refunded amount is " + ((-float.Parse(Result.Split('_')[1])) - (float.Parse(Totalcharge)));
                Result = RefundAmt;
            }
        }
        catch (Exception ex)
        {


        }

        return jsSerializer.Serialize(Result);
    }


}