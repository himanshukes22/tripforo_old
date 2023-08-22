using IRCTC_RDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Eticketing_EncryptDecrypt : System.Web.UI.Page
{
    EncryptDecryptString ENDE = new EncryptDecryptString();
    protected void Page_Load(object sender, EventArgs e)
    {
        LblMessage.Text = "";

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        TxtString.Text = "";
        LblMessage.Text = "";
    }
    protected void BtnEncrypt_Click(object sender, EventArgs e)
    {
        LblMessage.Text = "";
        if (!string.IsNullOrEmpty(TxtString.Text))
        {
            try
            {
                LblMessage.Text = ENDE.EncryptString(TxtString.Text);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Enter Text ..');", true);
        }
    }
    protected void BtnDecrypt_Click(object sender, EventArgs e)
    {
        LblMessage.Text = "";
        if (!string.IsNullOrEmpty(TxtString.Text))
        {
            try
            {
                LblMessage.Text = ENDE.DecryptString(TxtString.Text);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('"+ex.Message+"');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Enter Text ..');", true);
        }
        
    }
}