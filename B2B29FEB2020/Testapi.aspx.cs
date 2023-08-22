using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Testapi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Session["agent_type"]);
        //SqlTransactionNew objSql = new SqlTransactionNew();
        //DataSet dsCrd = objSql.GetCredentials("6E", "NRM", "D");
        string Signature = "";
        //STD.BAL._6ENAV420._6ENAV obj6E = new STD.BAL._6ENAV420._6ENAV(dsCrd.Tables[0].Rows[0]["UserID"], dsCrd.Tables[0].Rows[0]["Password"], dsCrd.Tables[0].Rows[0]["BkgServerUrlOrIP"], ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables[0].Rows[0]["CorporateID"], objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables[0].Rows[0]["LoginID"], dsCrd.Tables[0].Rows[0]["LoginPwd"], 420); // , Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))

        //STD.BAL._6ENAV420._6ENAV obj6E = new STD.BAL._6ENAV420._6ENAV("OTI132C", "Gaurav@123", "www", ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString, "OTI132", "DEL", "BOM", "FWU", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://6eprodr4xapi.navitaire.com/sessionmanager.svc", "https://6eprodr4xapi.navitaire.com/bookingmanager.svc", 420); // , Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
        //string Signature = obj6E.Spice_Login();         
        //    obj6E.Spice_Logout(Signature);

        // STD.BAL.SGNAV420.SGNAV4 objSG = new STD.BAL.SGNAV420.SGNAV4("OTI132C", "Gaurav@123", "www", ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString, "OTI132", "DEL", "BOM", "FWU", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://6eprodr4xapi.navitaire.com/sessionmanager.svc", "https://6eprodr4xapi.navitaire.com/bookingmanager.svc", 420); // , Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
        //Dim objSG As New STD.BAL.SGNAV420.SGNAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), objInputs.HidTxtDepCity, objInputs.HidTxtArrCity, objInputs.OwnerId, "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
        //STD.BAL.SGNAV420.SGNAV4 objSG = new STD.BAL.SGNAV420.SGNAV4("DELSSV1599", "Sspicecorp@188", "www", ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString, "DELSSV1599", "DEL", "BOM", "FWU", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://SGprodr4xapi.navitaire.com/SessionManager.svc", "https://SGprodr4xapi.navitaire.com/BookingManager.svc", 0);
        STD.BAL.SGNAV420.SGNAV4 objSG = new STD.BAL.SGNAV420.SGNAV4("DELSSA8750", "Class@4344", "www", ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString, "DELSSA8750", "DEL", "BOM", "FWU", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://SGprodr4xapi.navitaire.com/SessionManager.svc", "https://SGprodr4xapi.navitaire.com/BookingManager.svc", 0);
        Signature = objSG.Spice_Login();
        objSG.Spice_Logout(Signature);

//Username:
//        -DELSSV1599
//Password: -Sspicecorp@188

        //UserID,Password,BkgServerUrlOrIP,LoginID,LoginPwd
        //
        //            UserID  Password BkgServerUrlOrIP    LoginID LoginPwd
        //OTI132 Fyt4@kqdi7 www https://6eprodr4xapi.navitaire.com/sessionmanager.svc	https://6eprodr4xapi.navitaire.com/bookingmanager.svc
    }
}