using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SprReports_ChartReports : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    //Get connection string from web.config
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDBIRCTC"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    [WebMethod]
    public static List<Chart> GetData(string service, string fDate, string toDate, string Airlinecode, string AgentID, string toptype)
    {

        SqlDataAdapter adp = null;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myAmdDBIRCTC"].ConnectionString);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        List<Chart> dataList = new List<Chart>();
        try
        {
            conn.Open();
            adp = new SqlDataAdapter("Sp_GetBookingRecordForGraph", conn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@service", service);
            adp.SelectCommand.Parameters.AddWithValue("@fDate", fDate);
            adp.SelectCommand.Parameters.AddWithValue("@toDate", toDate);
            adp.SelectCommand.Parameters.AddWithValue("@airlinecode", Airlinecode);
            adp.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID);
            adp.SelectCommand.Parameters.AddWithValue("@toptype", toptype);
            adp.Fill(ds);
            dt = ds.Tables[0];


            string cat = "";
            int val = 0;
            string sToolTip = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        cat = dr[0].ToString();
                        val = Convert.ToInt32(dr[1]);
                        if (dr.ItemArray.Count() > 2)
                        {
                            sToolTip = Convert.ToString(dr[2]);
                            // sToolTip = "<table><tr><td>Date : " + cat + "</td><td>Amount :" + val + " Rs.</td><td>Count :" + sToolTip + "</td></tr></table>";

                            if (service == "TopAgent" || service == "AgentFlight" || service == "AgentHotel" || service == "AgentTrain" || service == "AgentBus" || service == "AgentUtility" )
                            {
                                sToolTip = "Agent : " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";

                            }

                            else if (service == "FlightName")
                            {

                                sToolTip = "Airline : " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";
                            }
                            else if (service == "AgentCityFlight" || service == "AgentCityHotel" || service == "AgentCityRail" || service == "AgentCityBus" || service == "AgentCityUtilty")
                            {

                                //sToolTip = " Agent City: " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";
                                sToolTip = " Agent City: " + cat + " Rs. Count :" + sToolTip + " ";
                            }



                            else if (service == "HotelCity" || service == "BusCity" || service == "AgentCityFlight")
                            {

                                sToolTip = "City: " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";
                            }



                            else if (service == "RailClass")
                            {

                                sToolTip = "Class: " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";
                            }

                            else
                            {
                                sToolTip = "Date : " + cat + " Amount :" + val + " Rs. Count :" + sToolTip + " ";
                            }
                        }
                        dataList.Add(new Chart(cat, val, sToolTip));
                    }
                    catch (Exception ex)
                    {
                    }


                }
            }
        }
        catch (Exception ex)
        {
        }

        return dataList;

    }





}
public class Chart
{
    public string ColumnName = "";
    public int Value = 0;
    public string ToolTip = "";

    public Chart(string columnName, int value, string toolTip)
    {
        ColumnName = columnName;
        Value = value;
        ToolTip = toolTip;
    }
}