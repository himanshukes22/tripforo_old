using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace PG
{
    public class PGRefund
    {
        public void MethoodPGRefund(decimal RefundAmount, string OrderID, string RefundFor, string AgentID, string RefundStatus, string CreatedBy, string CreatedDate, string RefundBy, string RefundDate, string RefundMode, string CMD_TYPE)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
                SqlCommand cmd = new SqlCommand("USP_INSERTPGDETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RefundAmount", RefundAmount);
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.Parameters.AddWithValue("@RefundFor", RefundFor);
                cmd.Parameters.AddWithValue("@AgentID", AgentID);
                cmd.Parameters.AddWithValue("@RefundStatus", RefundStatus);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                cmd.Parameters.AddWithValue("@RefundBy", RefundBy);
                cmd.Parameters.AddWithValue("@RefundDate", RefundDate);
                cmd.Parameters.AddWithValue("@RefundMode", RefundMode);
               // cmd.Parameters.AddWithValue("@IniativeBy", IniativeBy);
                cmd.Parameters.AddWithValue("@CMD_TYPE", CMD_TYPE);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                else
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                //throw ex;
                //ErrorLogTrace.WriteErrorLog(ex, "INSERTFLIGHTDETAILS");
            }
        }
    }
}
