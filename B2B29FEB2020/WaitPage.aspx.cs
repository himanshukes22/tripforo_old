using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WaitPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean( Session["BookIng"]) == true )
            {
            }
            else
            {
                Session["BookIng"] = false;
            }
            if (Convert.ToBoolean(Session["RTFBookIng"]) == true)
            {
            }
            else
            {
                Session["RTFBookIng"] = false;
            }
            if (Convert.ToBoolean(Session["IntBookIng"]) == true)
            {
            }
            else
            {
                Session["IntBookIng"] = false;
            }
        }
        catch (Exception ex)
        {

        }

    }
}