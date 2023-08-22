<%@ WebService Language="C#" Class="NotificationDisplay" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class NotificationDisplay  : System.Web.Services.WebService {

    [WebMethod]
    public string GetData(string userid, string usertype)
    {       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
        string strReturnJson = "";
        try {
        con.Open();
        SqlCommand cmd = new SqlCommand("SP_Note_Display", con);
        cmd.Parameters.AddWithValue("@usertype", usertype);
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        dt.Columns.Add("Mark", typeof(System.Int32));
        foreach (DataRow row in dt.Rows)
        {
            //need to set value to NewColumn column
            row["Mark"] = 0;             
            // or set it to some other value
        }     
        strReturnJson = DataTableToJSONWithJavaScriptSerializer(dt);      
        con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        } 
        return strReturnJson;
    }
    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }  
      
}