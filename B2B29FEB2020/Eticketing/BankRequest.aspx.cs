using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
public partial class Eticketing_BankRequest : System.Web.UI.Page
{
    string workingKey = "";
    public string strAccessCode = "";
    public string strEncRequest = "";
    public string strCCAveUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
                PG.PaymentGateway objPg = new PG.PaymentGateway();
                DataSet ds = new DataSet();

                string ObTid = Request.QueryString["OBTID"];
                string IbTid = Request.QueryString["IBTID"];
                string Ft = Request.QueryString["FT"];
               
                string RDSPGUrl = "http://localhost:53943/Eticketing/RDSPaymentGateway.aspx";
            
            strAccessCode = "";
             
                strEncRequest = "4E051A242D1FC1DDD5A4F3BCEF583C6E556B9464BBB368C52C180DE3555DA30490DB2AA7B2A9C5F07493E16DFF4C963B811AA9707C6B98955208BBB0E4C9BE2962F2FFCE6AEA544FA7F0FE196EC98F6B1703781CF98BABA4F0BF4795B2D7A77358043EC8F74F1838F207170E505A52D4E246F901AA11D5C5D1790BD82F5B4DC2CA56BF9D09D8B952036F80AB052578BDF0E9AE48B4584498F07976727A9BA3345425E712A9E315D5623BC32828398C0728625FE400085BE17DC21069CA4D561576B40D18DC24761F1C0C4DFCE855EA036F6FA71F91022C89BEA54E15C357B677ADC1B25E19D7F669973827D4DFDF589D9C0D607C18BCA9B29AE8055F5CF2136F0489BBD0E60EED439CBA64E49B519614580F4B5626E1248D78D8C2CC699D4098";//Provided By Irctc
                strCCAveUrl = RDSPGUrl;// +strEncRequest;
           
        }
        catch (Exception ex)
        {
            clsErrorLog.LogInfo(ex);
        }
       
    }

   
}