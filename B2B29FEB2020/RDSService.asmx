<%@ WebService Language="C#" Class="RDSService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RDSService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {        
        return "Hello World";
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true)]
    public string HelloWorld1(string Msg)
    {
        Msg = Msg + " - Hello World";
        return Msg;
    }
    
}