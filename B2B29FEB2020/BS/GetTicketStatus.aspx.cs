using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class BS_GetTicketStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GsrtcService obj = new GsrtcService();
     
       // BS_BAL.trackTicketHistoryDetails objtrackTicketHistoryDetails = new BS_BAL.trackTicketHistoryDetails();
       // objtrackTicketHistoryDetails= obj.GetTicketStatus(txtorderidbase.Text);
    }
}
