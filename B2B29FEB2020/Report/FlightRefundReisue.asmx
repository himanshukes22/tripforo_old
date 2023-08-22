<%@ WebService Language="C#" Class="FlightRefundReisue" %>

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
using System.Data.SqlClient;
using System.Configuration;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class FlightRefundReisue  : System.Web.Services.WebService {

    [WebMethod(EnableSession = true)]
    public string ShowRefundDetails( string orderid) 
    {
        SqlTransactionDom SDOM = new SqlTransactionDom();
        SqlTransaction ST= new SqlTransaction();
        DataSet gridViewds, fltdss = new DataSet();
        string refunddtl = "<div class='large-12 columns' style='line-height:30px;' >";
        try 
        {            
            gridViewds =(DataSet) HttpContext.Current.Session["Grdds"];  
            DataRow[] GridViewFilter = gridViewds.Tables[0].Select("OrderId ='" + orderid + "'");

            fltdss = ST.GetFltDtlsReissue(orderid);
            DataRow[] fltFilter = fltdss.Tables[0].Select("OrderId ='" + orderid + "'");

            refunddtl += "<div class='col-md-11'><b>Select Pax for cancellation</b></div>";
            if (GridViewFilter.Length > 1)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' name='PaxdtlAll' id='AllPax' value='ALLPax'/></span><span class='col-md-3'>ALL Pax</span><span class='col-md-3'> Ticket No</span></div>";
            else
                refunddtl += "<div class='col-md-12'><span class='col-md-1'>&nbsp;</span><span class='col-md-3'> Ticket No</span></div>";

            foreach (DataRow dr in GridViewFilter)
                {
                string Status = CheckTktNo(Convert.ToInt32(dr["PaxId"].ToString().Trim()), orderid, dr["GDSPNR"].ToString().Trim());
                if (Status == "0" || Status == "Reissue request can not be accepted for past departure date." || Status == "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly.")
                    refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' class='Paxcss' name='Paxdtl' id='" + dr["PaxId"].ToString().Trim() + "'  value='" + dr["PaxId"].ToString().Trim() + "'/></span> <span class='col-md-3'>" + dr["PName"].ToString().Trim() + "</span><span class='col-md-2'>" + dr["TicketNumber"].ToString().Trim() + "</span></div>";
                else
                    refunddtl += "<div class='col-md-12'><span class='col-md-1'>&nbsp;</span><span class='col-md-3'>" + dr["PName"].ToString().Trim() + "</span><span class='col-md-2'>" + dr["TicketNumber"].ToString().Trim() + "</span><span class='col-md-6'>" + Status.Replace("Given ticket number is ", "") + "</span><span class='col-md-12'>&nbsp;</span></div>";
            }
            refunddtl += "<div class='col-md-12'><b>Select Sector for cancellation</b> </div>";
            if (fltFilter.Length > 1)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' name='SecterdtlAll' id='AllSector' value='ALL'/></span>  <span class='col-md-9'>ALL Sector</span></div>";

            foreach (DataRow dr in fltFilter)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' class='Sectorcss' name='Secterdtl' id='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'  value='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'/></span> <span class='large-11 columns end'>" + dr["DepCityOrAirportName"].ToString().Trim() + ":" + dr["ArrCityOrAirportName"].ToString().Trim() + "</span></div>";
            //refunddtl += "<div class='large-10 columns'>Select Pax for Cancellation</div>";
            //if (GridViewFilter.Length > 1)                
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' name='PaxdtlAll' id='AllPax' value='ALLPax'/></span><span class='large-6 columns'>ALL Pax</span><span class='large-3 columns'> Ticket No</span></div>";
            //else
            //    refunddtl += "<div><span class='large-3 columns'> Ticket No</span></div>";
            
            //foreach(DataRow dr  in  GridViewFilter)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' class='Paxcss' name='Paxdtl' id='" + dr["PaxId"].ToString() + "'  value='" + dr["PaxId"].ToString() + "'/></span> <span class='large-6 columns'>" + dr["FName"].ToString() + " " + dr["LName"].ToString() + "</span><span class='large-3 columns'>" + dr["TicketNumber"].ToString() + "</span></div>";

            //refunddtl += "<div class='large-10 columns'>Select Sector For Cancellation </div>";
            //if (fltFilter.Length>1)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' name='SecterdtlAll' id='AllSector' value='ALL'/></span>  <span class='large-9 columns'>ALL Sector</span></div>";

            //foreach (DataRow dr in fltFilter)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' class='Sectorcss' name='Secterdtl' id='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'  value='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'/></span> <span class='large-9 columns'>" + dr["DepCityOrAirportName"].ToString().Trim() + ":" + dr["ArrCityOrAirportName"].ToString().Trim() + "</span></div>";
          
        }
        catch(Exception ex)
        { clsErrorLog.LogInfo(ex); }

        return refunddtl + "</div>";
    }

    [WebMethod(EnableSession = true)]
    public string ShowRefundDetailsRNC(string orderid, string RqustType)
    {
        SqlTransactionDom SDOM = new SqlTransactionDom();
        SqlTransaction ST = new SqlTransaction();
        DataSet gridViewds, fltdss = new DataSet();
        string refunddtl = "<div class='large-12 columns' style='line-height:30px;' >";
        try
        {
            //gridViewds =(DataSet) HttpContext.Current.Session["Grdds"];  
            //gridViewds = SDOM.GetALLpaxDetailsbyorderid(orderid);
            gridViewds = GetALLpaxDetailsbyorderid(orderid);
            DataRow[] GridViewFilter = gridViewds.Tables[0].Select("OrderId ='" + orderid + "'");

            fltdss = ST.GetFltDtlsReissue(orderid);
            DataRow[] fltFilter = fltdss.Tables[0].Select("OrderId ='" + orderid + "'");

            if (RqustType == "REFUND")
            {
                refunddtl += "<div class='col-md-11'><b>Select Pax for cancellation</b></div>";
            }
            else
            {
                refunddtl += "<div class='col-md-11'><b>Select Pax for Reissue</b></div>";
            }
            
            if (GridViewFilter.Length > 1)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' name='PaxdtlAll' id='AllPax' value='ALLPax'/></span><span class='col-md-3'>ALL Pax</span><span class='col-md-3'> Ticket No</span></div>";
            else
                refunddtl += "<div class='col-md-12'><span class='col-md-1'>&nbsp;</span><span class='col-md-3'> Ticket No</span></div>";

            foreach (DataRow dr in GridViewFilter)
            {
                string Status = CheckTktNo(Convert.ToInt32(dr["PaxId"].ToString().Trim()), orderid, dr["GDSPNR"].ToString().Trim());
                if (Status == "0" || Status == "Reissue request can not be accepted for past departure date." || Status == "Refund/Reissue request can not be accepted upto 4 hour prior to departure date. Please contact to airline directly.")
                    refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' class='Paxcss' name='Paxdtl' id='" + dr["PaxId"].ToString().Trim() + "'  value='" + dr["PaxId"].ToString().Trim() + "'/></span> <span class='col-md-3'>" + dr["PName"].ToString().Trim() + "</span><span class='col-md-2'>" + dr["TicketNumber"].ToString().Trim() + "</span></div>";
                else
                    refunddtl += "<div class='col-md-12'><span class='col-md-1'>&nbsp;</span><span class='col-md-3'>" + dr["PName"].ToString().Trim() + "</span><span class='col-md-2'>" + dr["TicketNumber"].ToString().Trim() + "</span><span class='col-md-6'>" + Status.Replace("Given ticket number is ", "") + "</span><span class='col-md-12'>&nbsp;</span></div>";
            }

            if (RqustType == "REFUND")
            {
                refunddtl += "<div class='col-md-12'><b>Select Sector for cancellation</b> </div>";
            }
            else
            {
                refunddtl += "<div class='col-md-12'><b>Select Sector for Reissue</b> </div>";
            }
            //refunddtl += "<div class='col-md-12'><b>Select Sector</b> </div>";
            if (fltFilter.Length > 1)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' name='SecterdtlAll' id='AllSector' value='ALL'/></span>  <span class='col-md-9'>ALL Sector</span></div>";

            foreach (DataRow dr in fltFilter)
                refunddtl += "<div class='col-md-12'><span class='col-md-1'><input type='checkbox' class='Sectorcss' name='Secterdtl' id='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'  value='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'/></span> <span class='large-11 columns end'>" + dr["DepCityOrAirportName"].ToString().Trim() + ":" + dr["ArrCityOrAirportName"].ToString().Trim() + "</span></div>";
            //refunddtl += "<div class='large-10 columns'>Select Pax for Cancellation</div>";
            //if (GridViewFilter.Length > 1)                
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' name='PaxdtlAll' id='AllPax' value='ALLPax'/></span><span class='large-6 columns'>ALL Pax</span><span class='large-3 columns'> Ticket No</span></div>";
            //else
            //    refunddtl += "<div><span class='large-3 columns'> Ticket No</span></div>";

            //foreach(DataRow dr  in  GridViewFilter)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' class='Paxcss' name='Paxdtl' id='" + dr["PaxId"].ToString() + "'  value='" + dr["PaxId"].ToString() + "'/></span> <span class='large-6 columns'>" + dr["FName"].ToString() + " " + dr["LName"].ToString() + "</span><span class='large-3 columns'>" + dr["TicketNumber"].ToString() + "</span></div>";

            //refunddtl += "<div class='large-10 columns'>Select Sector For Cancellation </div>";
            //if (fltFilter.Length>1)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' name='SecterdtlAll' id='AllSector' value='ALL'/></span>  <span class='large-9 columns'>ALL Sector</span></div>";

            //foreach (DataRow dr in fltFilter)
            //    refunddtl += "<div class='large-10 columns'><span class='col-md-1'><input type='checkbox' class='Sectorcss' name='Secterdtl' id='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'  value='" + dr["DepCityOrAirportCode"].ToString().Trim() + ":" + dr["ArrCityOrAirportCode"].ToString().Trim() + "'/></span> <span class='large-9 columns'>" + dr["DepCityOrAirportName"].ToString().Trim() + ":" + dr["ArrCityOrAirportName"].ToString().Trim() + "</span></div>";

        }
        catch (Exception ex)
        { clsErrorLog.LogInfo(ex); }

        return refunddtl + "</div>";
    }
    
    [WebMethod(EnableSession = true)]
    public string CheckTktNo(int PaxId, string orderid, string PNR)
    {
        string tktstatus = "";
        System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CheckTktNo_New", con1);
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaxId", SqlDbType.VarChar).Value = PaxId;
            cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = orderid;
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNR;
            con1.Open();
            tktstatus = cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        { }
        finally
        {
            con1.Close();
            cmd.Dispose();
        }
        return tktstatus;
    }

    [WebMethod(EnableSession = true)]
    public ArrayList HotelRefundrequest(string orderid)
    {
        ArrayList StatusAarry =new ArrayList();
        try
        {
             HotelDAL.HotelDA objhtldl=new HotelDAL.HotelDA();
            DataSet HtlDetailsDs = new DataSet();
            HtlDetailsDs = objhtldl.htlintsummary(orderid, "CancelData");
            if (HtlDetailsDs.Tables.Count > 0) {
                Session["HtlDetails"] = HtlDetailsDs;
                int status = objhtldl.CheckHtlRefuStaus(HtlDetailsDs.Tables[0].Rows[0]["BookingID"].ToString(), HtlDetailsDs.Tables[0].Rows[0]["OrderID"].ToString());
                StatusAarry.Add(status);
                StatusAarry.Add(Newtonsoft.Json.JsonConvert.SerializeObject(HtlDetailsDs));  
            }
        }
        catch (Exception ex)
        { }
        return StatusAarry;
    }

    public DataSet GetALLpaxDetailsbyorderid(string ORDERID)
    {
        DataAccess objDataAcess = new DataAccess(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        Hashtable paramHashtable = new Hashtable();
        paramHashtable.Clear();
        //'MOHIT
        paramHashtable.Add("@OderId", ORDERID);
        return objDataAcess.ExecuteData<DataSet>(paramHashtable, true, "USP_GetTicketDetail_Intl_ALLPAX", 3);
    }
    // Public Function CheckTktNo(ByVal PaxId As Integer, ByVal OrderId As String, ByVal PNR As String) As String
    //    Dim cmd As New SqlCommand()
    //    Dim ErrorMsg As String = ""
    //    Try
    //        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    //        cmd.CommandText = "CheckTktNo"
    //        cmd.CommandType = CommandType.StoredProcedure
    //        cmd.Parameters.Add("@PaxId", SqlDbType.VarChar).Value = PaxId
    //        cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = OrderId
    //        cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNR
    //        cmd.Connection = con1
    //        con1.Open()
    //        'If IsDBNull(cmd.ExecuteScalar()) Then
    //        '    ErrorMsg = "0"
    //        'Else
    //        ErrorMsg = cmd.ExecuteScalar()
    //        'End If
    //        cmd.Dispose()
    //        con.Close()

    //    Catch ex As Exception
    //        clsErrorLog.LogInfo(ex)
    //    End Try
    //    Return ErrorMsg
    //End Function
}