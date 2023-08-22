using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration; 

/// <summary>
/// Summary description for DynamicMenuDB
/// </summary>
public class DynamicMenuDB
{     
    
    SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);

     SqlCommand cmd;
    
    
	public DynamicMenuDB()
	{
	}

    public DataTable  TypeIdMGMT(string DeptName, string TypeID, string TypeName, string UserType,string queryType)
    {
      
       DataSet ds = new DataSet();
       DataTable dt = new DataTable();

        try
        {
            conn.Open();
            cmd = new SqlCommand("usp_typeId_Mgmt", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deptName", DeptName);
            cmd.Parameters.AddWithValue("@TypeID", TypeID);
            cmd.Parameters.AddWithValue("@TypeName", TypeName);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            cmd.Parameters.AddWithValue("@queryType", queryType);

            if (queryType.ToLower().Trim() == "insert" || queryType.ToLower().Trim() == "update" || queryType.ToLower().Trim() == "delete")
            {
                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
            }
            else  if (queryType.ToLower().Trim() == "select" )
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            if(ds.Tables.Count>0)
            {
                dt = ds.Tables[0];

            }
           
        }
        catch (Exception ex)
        {
             //ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightSearch");
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }
}