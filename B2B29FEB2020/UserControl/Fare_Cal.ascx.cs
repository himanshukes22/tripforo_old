using Newtonsoft.Json.Linq;
using STD.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrvCal;

public partial class UserControl_Fare_Cal : System.Web.UI.UserControl
{
    private System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {



       
    }


    
}
