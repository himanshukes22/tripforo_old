using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG
{
    public class CCAveRequest
    {
        public string GetCCAvenPaymentStatus(string ReferenceNo, string OrderNo, string AgentId)
        {
            string message = "not";
            string pgResponse = string.Empty;
            Status result = new Status();
            try
            {
                //orderStatusTracker
                //order_no= TrackId  //database
                //reference_no= PaymentId //database
                //{"reference_no": "305002833041","order_no": "cddf223bzdrMT5oW"}
                string pgReq = "{\"reference_no\": \"" + ReferenceNo + "\",\"order_no\": \"" + OrderNo + "\"}";                
                srvPG objPg = new srvPG();
                pgResponse = objPg.GetPostReqResCCAvenue(pgReq, "orderStatusTracker", "");
                if (!string.IsNullOrEmpty(pgResponse))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(pgResponse);
                    if (result.error_code != null)
                    {
                        message =result.error_desc+ " - "+ result.error_code;
                    }
                    else {
                        message = result.order_status;
                    }
                }            
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message; 
        }


        //public int InsertPgApi(string TrackId, string TId, string IBTrackId, string Name, string PaymentGateway, string Email, string Mobile, string Address, double Amount, double OriginalAmount, string AgentId, string AgencyName, string Ip, string PgRequest, string ServiceType, string EncRequest, string Trip)
        //{
        //    int temp = 0;
        //    con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        //    try
        //    {
        //        //Name, TrackId, PaymentGateway, Email, Mobile, Address, Amount, OriginalAmount, AgentId, AgencyName, Status, Ip
        //        con.Open();
        //        cmd = new SqlCommand("SpInsertPaymentDetails", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@TrackId", TrackId);
        //        cmd.Parameters.AddWithValue("@Name", Name);
        //        cmd.Parameters.AddWithValue("@PaymentGateway", PaymentGateway);
        //        cmd.Parameters.AddWithValue("@Email", Email);
        //        cmd.Parameters.AddWithValue("@Mobile", Mobile);
        //        cmd.Parameters.AddWithValue("@Address", Address);
        //        cmd.Parameters.AddWithValue("@Amount", Amount);
        //        cmd.Parameters.AddWithValue("@OriginalAmount", OriginalAmount);
        //        cmd.Parameters.AddWithValue("@AgentId", AgentId);
        //        cmd.Parameters.AddWithValue("@AgencyName", AgencyName);
        //        cmd.Parameters.AddWithValue("@Status", "Requested");
        //        cmd.Parameters.AddWithValue("@Ip", Ip);
        //        cmd.Parameters.AddWithValue("@Action", "insert");
        //        cmd.Parameters.AddWithValue("@PgRequest", PgRequest);
        //        cmd.Parameters.AddWithValue("@EncRequest", EncRequest);
        //        cmd.Parameters.AddWithValue("@TId", TId);
        //        cmd.Parameters.AddWithValue("@IBTrackId", IBTrackId);
        //        cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
        //        cmd.Parameters.AddWithValue("@Trip", Trip);
        //        temp = cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightBooking");
        //    }
        //    finally
        //    {
        //        con.Close();
        //        cmd.Dispose();
        //    }
        //    return temp;
        //}
    }
}
