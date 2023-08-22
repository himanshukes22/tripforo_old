<%@ WebService Language="C#" Class="FareRuleService" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using STD.BAL.TBO;
using System.Collections.Generic;
using STD.Shared;
using System.Collections;
using STD.BAL;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class FareRuleService  : System.Web.Services.WebService {
    //List<FlightSearchResults>
    string Con = System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString.ToString();
    string strFQCSReq = "", url = "", Userid = "", pcc = "", Pwd = "", strFQCSRes = "", strFQMDRes = "", strFQMDReq = "", StrFareRules = "", Htmllist = "";
    private List<CredentialList> ProviderList;

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    //[WebMethod]
    //public FareRuleResponse GetFareRule(string sno)
    //{

    //    STD.BAL.TBO.TBOFareRule obj = new TBOFareRule();
    //    return obj.GetFareRule("",sno);
    //}

    [WebMethod]
    public FareRuleResponse GetFareRule(ArrayList JsonArr, string sno, string Provider)
    {
        int i = 0, j = 0, k = 0;
        FareRuleResponse tt = new FareRuleResponse();
        object[] ListOW = null;
        ListOW = (object[])JsonArr[0];
        i = ListOW.Length;
        Dictionary<string, object> RecLcc = new Dictionary<string, object>();
        RecLcc = (Dictionary<string, object>)((object[])(ListOW))[0];
        Dictionary<string, object> Rec = new Dictionary<string, object>();
        string DepLocation = "", ArvlLoction = "", Sector = "";
        string baseURL = HttpContext.Current.Request.Url.Host;

        try
        {
            if (ListOW.Length > 0)
            {
                if (Provider != null && Provider == "1G")
                {
                    STD.BAL.FltRequest flt = new STD.BAL.FltRequest();

                    if (i > 0)
                    {
                        for (j = 0; j < i; j++)
                        {
                            Rec = (Dictionary<string, object>)((object[])(ListOW))[j];
                            strFQCSReq = flt.FQCSReq_FareRule(Rec, Con);
                            GetCredentials(Con.ToString(), Convert.ToString(Rec["SearchId"]));
                            FlightCommonBAL objFCB = new FlightCommonBAL();
                            strFQCSRes = objFCB.PostXml(url, strFQCSReq, Userid, Pwd, "http://webservices.galileo.com/SubmitXml");
                            strFQMDReq = flt.FQMD_FullRules_23(strFQCSRes, Con, Convert.ToString(Rec["SearchId"]));
                            strFQMDRes = objFCB.PostXml(url, strFQMDReq, Userid, Pwd, "http://webservices.galileo.com/SubmitXml");

                            DepLocation = Rec["DepartureLocation"].ToString();
                            ArvlLoction = Rec["ArrivalLocation"].ToString();
                            Sector = (DepLocation + "-" + ArvlLoction).Trim();

                            StrFareRules += flt.FQMD_ResponseResult_23(strFQMDRes, Con, Sector);
                        }
                        for (k = 0; k < i; k++)
                        {
                            Rec = (Dictionary<string, object>)((object[])(ListOW))[k];
                            DepLocation = Rec["DepartureLocation"].ToString();
                            ArvlLoction = Rec["ArrivalLocation"].ToString();
                            Sector = (DepLocation + " → " + ArvlLoction).Trim();
                            Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        }
                        Rec = (Dictionary<string, object>)((object[])(ListOW))[0];
                        DepLocation = Rec["DepartureLocation"].ToString();
                        ArvlLoction = Rec["ArrivalLocation"].ToString();
                        Sector = DepLocation + "-" + ArvlLoction;
                        StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();

                    }
                    //else if (i == 1)
                    //{

                    //    Rec = (Dictionary<string, object>)((object[])(ListOW))[0];
                    //    strFQCSReq = flt.FQCSReq_FareRule(Rec, flt.connectionString);
                    //    GetCredentials(flt.connectionString);
                    //    FlightCommonBAL objFCB = new FlightCommonBAL();
                    //    strFQCSRes = objFCB.PostXml(url, strFQCSReq, Userid, Pwd, "http://webservices.galileo.com/SubmitXml");
                    //    strFQMDReq = flt.FQMD_FullRules_23(strFQCSRes, flt.connectionString);
                    //    strFQMDRes = objFCB.PostXml(url, strFQMDReq, Userid, Pwd, "http://webservices.galileo.com/SubmitXml");
                    //    //DepLocation=Rec[""];
                    //    //ArvlLoction=Rec[""];
                    //    Sector = (DepLocation + "-->>" + ArvlLoction).Trim();
                    //    StrFareRules = flt.FQMD_ResponseResult_23(strFQMDRes, flt.connectionString, Sector);
                    //    FareRule1 obj = new FareRule1();
                    //    tt.Response = new ResponseFareRule();
                    //    tt.Response.FareRules = new List<FareRule1>();
                    //    tt.Response.FareRules.Add(obj);
                    //    tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();
                    //}

                }
                else
                {
                    if (Provider == "AK")
                    {
                        if (i > 0)
                        {
                            GALWS.AirAsia.AirAsiaFareRules objrules = new GALWS.AirAsia.AirAsiaFareRules();
                            for (k = 0; k < i; k++)
                            {
                                Rec = (Dictionary<string, object>)((object[])(ListOW))[k];
                                DepLocation = Rec["DepartureLocation"].ToString();
                                ArvlLoction = Rec["ArrivalLocation"].ToString();
                                Sector = (DepLocation + "<img src='/Images/air.png' />" + ArvlLoction).Trim();
                                Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";

                                StrFareRules += objrules.GetFareRule(Con, sno, Rec["fareBasis"].ToString(), Rec["AdtRbd"].ToString(), Rec["Searchvalue"].ToString());
                            }

                            StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";

                            Rec = (Dictionary<string, object>)((object[])(ListOW))[j];
                            FareRule1 obj = new FareRule1();
                            tt.Response = new ResponseFareRule();
                            tt.Response.FareRules = new List<FareRule1>();
                            tt.Response.FareRules.Add(obj);
                            tt.Response.FareRules[0].FareRuleDetail = StrFareRules;
                            // tt.Response.FareRules[0].FareRuleDetail = objrules.GetFareRule(Con, sno, Rec["fareBasis"].ToString(), Rec["AdtRbd"].ToString(), Rec["Searchvalue"].ToString());

                        }
                    }
                    else if (Provider.ToUpper() == "FDD")
                    {

                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        StrFareRules += "<table cellspacing=0 border=1> <tr> <td style=min-width:50px>FIXED DEPARTURE FLIGHT CANCELLATION CHARGES </td></tr> <tr> <td style=min-width:50px>Non Refundable/Non Changeable</td></tr></table>";
                        StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();

                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "SG"  && RecLcc["Trip"].ToString() == "D")
                    {


                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        //Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        //StrFareRules += "<table cellspacing='0' border='0'> <colgroup width='69'></colgroup> <colgroup width='215'></colgroup> <colgroup width='104'></colgroup> <colgroup width='211'></colgroup> <colgroup width='106'></colgroup> <tbody><tr> <td height='21' align='left' valign='bottom'><font color='#000000'><br></font></td> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' colspan='2' align='center' valign='middle'><b><font color='#000000'>Change</font></b></td> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' colspan='2' align='center' valign='middle'><b><font color='#000000'>Cancellation</font></b></td> </tr> <tr> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000' height='21' align='left' valign='middle'><font color='#000000'><br></font></td> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Beyond 4 Days</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Beyond 4 Days</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000' height='21' align='left' valign='middle'><b><font color='#000000'>Domestic</font></b></td> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='3000' sdnum='1033;'><font color='#000000'>3000</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='2500' sdnum='1033;'><font color='#000000'>2500</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='3500' sdnum='1033;'><font color='#000000'>3500</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='3000' sdnum='1033;'><font color='#000000'>3000</font></td> </tr> </tbody></table>";
                        //StrFareRules += "<table style='width: 100%;'> <colgroup></colgroup> <colgroup></colgroup> <colgroup></colgroup> <colgroup></colgroup> <colgroup></colgroup> <tbody><tr> <td><font color='#000000'><br></font></td> <td><b><font color='#000000'>Change</font></b></td><td></td> <td><b><font color='#000000'>Cancellation</font></b></td><td></td> </tr> <tr> <td><font color='#000000'><br></font></td> <td><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td><b><font color='#000000'>Beyond 4 Days</font></b></td> <td><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td><b><font color='#000000'>Beyond 4 Days</font></b></td> </tr> <tr> <td><b><font color='#000000'>Domestic</font></b></td> <td><font color='#000000'>3000</font></td> <td><font color='#000000'>2500</font></td> <td><font color='#000000'>3500</font></td> <td><font color='#000000'>3000</font></td> </tr> </tbody></table>";
                        //StrFareRules += "<div style='width: 100%;float: left;margin: 0px;padding: 0 0 0;'>";

                        //StrFareRules += "<div class='wid_option2'>";
                        //StrFareRules += "<span>Select Fare Type:</span>";
                        //StrFareRules += "<select><option label='Regular Fare' value='' selected='selected'>Regular Fare</option>";
                        //StrFareRules += "<option label=' Special Fare' value=''> Special Fare</option></select>";
                        //StrFareRules += "</div>";

                        StrFareRules += "<div class='can-b2b-tr'>";
                        StrFareRules += "<div class='lef-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Cancellation Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "<div class='rig-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Reschedule Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 2500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "</div>";

                        
                        
                        
                        
                        StrFareRules += "<div style='margin: 0px;padding: 10px 0;font-size: 18px;font-weight: 600;'>Terms &amp; Conditions</div>";
                        StrFareRules += "<div style='font-size: 10px;color: #000;padding: 0;height: 67px;overflow-x: hidden;'>";
                       
                          StrFareRules +=   "<ul>";
                          StrFareRules += "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>";
                          StrFareRules += "<li>The charges will be on per passenger per sector</li>";
                          StrFareRules += "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>";
                          StrFareRules += "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>";
                          StrFareRules += "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>";
                          StrFareRules += "<li>For infants there is no baggage allowance</li>";
                          StrFareRules += "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>";
                          StrFareRules += "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>";
                          StrFareRules += "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>";
                          StrFareRules += "</ul>";
                        StrFareRules += "</div>";
                        StrFareRules += "</div>";
                        //StrFareRules += "Domestic Cancellation, changes and refund </br>";
                        //StrFareRules += "• Spicejet shall provide “Look-in option” for a period of 24 hours after booking ticket. During this period passenger can cancel or amend the ticket without any additional charges, except for the normal prevailing fare for the revised flight for which the ticket is sought to be amended. This facility shall not be available for a flight whose departure is less than 7 days from booking date. The passenger needs to call SpiceJet’s call center to avail this facility. Please note that in all other cases for all domestic sectors any cancellation to the booking will attract a fee of INR 3000/3500 on Economy/Business Class respectively (w.e.f. 03 May‘ 2019) or base fare plus fuel surcharge per passenger per sector per cancellation, whichever is lower.</br>";
                        //StrFareRules += "• For all domestic sectors any changes pertaining to Date for a booking will attract a fee of 2500/1500 INR per passenger per sector per change on Economy/Business Class respectively. If the same fare is not available at the time of change, the difference in fares will be applied in addition to the change fee plus changes to any applicable fees. All fees are subject to change without notice</br>";
                        //StrFareRules += "• c. For all domestic sectors any request for sector change in the booking will attract a cancellation fees of INR 3000/3500 on Economy/Business Class respectively or base fare plus fuel surcharge per passenger per sector per change, whichever is lower. If the same fare is not available at the time of change, the difference in fares will be applied in addition to this change fee plus changes to any applicable fees. All fees are subject to change without notice</br>";

                        StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();


                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "SG" && RecLcc["Trip"].ToString() == "I")
                    {
                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);


                        Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";


                        //StrFareRules += "<table cellspacing='0' border='0'> <colgroup width='98'></colgroup> <colgroup width='215'></colgroup> <colgroup width='104'></colgroup> <colgroup width='211'></colgroup> <colgroup width='106'></colgroup> <tbody><tr> <td height='21' align='left' valign='bottom'><font color='#000000'><br></font></td> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' colspan='2' align='center' valign='middle'><b><font color='#000000'>Change</font></b></td> <td></td><td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' colspan='2' align='center' valign='middle'><b><font color='#000000'>Cancellation</font></b></td> </tr> <tr> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000' height='21' align='left' valign='middle'><font color='#000000'><br></font></td> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Beyond 4 Days</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Within 4Days of Departure Time</font></b></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' bgcolor='#FFFF00'><b><font color='#000000'>Beyond 4 Days</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000' height='21' align='left' valign='middle'><b><font color='#000000'>International</font></b></td> <td style='border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='4000' sdnum='1033;'><font color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='3500' sdnum='1033;'><font color='#000000'>3500</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='5000' sdnum='1033;'><font color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #000000; border-right: 2px solid #000000' align='center' valign='middle' sdval='4500' sdnum='1033;'><font color='#000000'>4500</font></td> </tr> </tbody></table>";



                        //StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";


                        //StrFareRules += "<div class='wid_option2'>";
                        //StrFareRules += "<span>Select Fare Type:</span>";
                        //StrFareRules += "<select><option label='Regular Fare' value='' selected='selected'>Regular Fare</option>";
                        //StrFareRules += "<option label=' Special Fare' value=''> Special Fare</option></select>";
                        //StrFareRules += "</div>";

                        StrFareRules += "<div class='can-b2b-tr'>";
                        StrFareRules += "<div class='lef-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Cancellation Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "<div class='rig-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Reschedule Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 2500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "</div>";

                        StrFareRules += "<div class='trm-had'>Terms &amp; Conditions</div>";
                        StrFareRules += "<div class='terms-b2b2-cancahe'>";
                        StrFareRules += "<ul>";
                        StrFareRules += "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>";
                        StrFareRules += "<li>The charges will be on per passenger per sector</li>";
                        StrFareRules += "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>";
                        StrFareRules += "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>";
                        StrFareRules += "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>";
                        StrFareRules += "<li>For infants there is no baggage allowance</li>";
                        StrFareRules += "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>";
                        StrFareRules += "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>";
                        StrFareRules += "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>";
                        StrFareRules += "</ul>";
                        StrFareRules += "</div>";
                        
                        
                        
                        
                        
                        
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();



                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "6E" && RecLcc["Trip"].ToString() == "D")
                    {


                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        //StrFareRules += "<table cellspacing='0' border='0'> <colgroup width='165'></colgroup> <colgroup width='224'></colgroup> <colgroup width='202'></colgroup> <colgroup width='64'></colgroup> <colgroup width='316'></colgroup> <tbody><tr> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #dee2e6; border-right: 2px solid #000' rowspan='2' height='90' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>Number of days left for departure</font></b></td> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #dee2e6; border-left: 2px solid #000; border-right: 2px solid #000' colspan='2' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>Cancellation fee</font></b></td> <td style='border-top: 2px solid #000000; border-bottom: 2px solid #dee2e6; border-left: 2px solid #000' colspan='2' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>Change Fee</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #dee2e6; border-left: 2px solid #ffffff' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td style='border-bottom: 2px solid #dee2e6; border-right: 2px solid #ffffff' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> <td style='border-bottom: 2px solid #dee2e6' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td style='border-bottom: 2px solid #dee2e6' align='center' valign='middle' bgcolor='#222222'><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='21' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='2500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> </tbody></table>";
                        //StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";
                        
                        //StrFareRules += "<div class='wid_option2'>";
                        //StrFareRules += "<span>Select Fare Type:</span>";
                        //StrFareRules += "<select><option label='Regular Fare' value='' selected='selected'>Regular Fare</option>";
                        //StrFareRules += "<option label=' Special Fare' value=''> Special Fare</option></select>";
                        //StrFareRules += "</div>";
                        
                        StrFareRules += "<div class='can-b2b-tr'>";
                        StrFareRules += "<div class='lef-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Cancellation Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "<div class='rig-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Reschedule Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 2500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "</div>";
                        
                        StrFareRules += "<div class='trm-had'>Terms &amp; Conditions</div>";
                        StrFareRules += "<div class='terms-b2b2-cancahe'>";
                        StrFareRules += "<ul>";
                        StrFareRules += "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>";
                        StrFareRules += "<li>The charges will be on per passenger per sector</li>";
                        StrFareRules += "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>";
                        StrFareRules += "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>";
                        StrFareRules += "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>";
                        StrFareRules += "<li>For infants there is no baggage allowance</li>";
                        StrFareRules += "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>";
                        StrFareRules += "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>";
                        StrFareRules += "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>";
                        StrFareRules += "</ul>";
                        StrFareRules += "</div>";
                        
                        
                        
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();


                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "6E" && RecLcc["Trip"].ToString() == "I")
                    {


                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        StrFareRules += "<table> <colgroup></colgroup> <colgroup ></colgroup> <colgroup ></colgroup> <colgroup ></colgroup> <tbody><tr> <td><b><font face='Open Sans' color='#000'>Number of days left for departure</font></b></td> <td ><b><font face='Open Sans' color='#000'>Currency</font></b></td> <td><b><font face='Open Sans' color='#000'>Cancellation fee</font></b></td> <td><b><font face='Open Sans' color='#000'>Change Fee</font></b></td> </tr> <tr> <td><br></font></b></td> <td><b><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> <td><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> </tr> <tr> <td><b><font face='Open Sans' size='3' color='#000000'>Indian-Sub continent</font></b></td> </tr> <tr> <td>Ex-India to Colombo</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-India to Dhaka</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-India to Kathmandu</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex- Colombo to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>LKR</font></td> <td><font face='Open Sans' size='3' color='#000000'>8910</font></td> <td><font face='Open Sans' size='3' color='#000000'>7700</font></td> <td><font face='Open Sans' size='3' color='#000000'>7700</font></td> <td><font face='Open Sans' size='3' color='#000000'>6370</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-Dhaka to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>BDT</font></td> <td><font face='Open Sans' size='3' color='#000000'>4230</font></td> <td><font face='Open Sans' size='3' color='#000000'>3625</font></td> <td><font face='Open Sans' size='3' color='#000000'>3625</font></td> <td><font face='Open Sans' size='3' color='#000000'>3025</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-Kathmandu to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>NPR</font></td> <td><font face='Open Sans' size='3' color='#000000'>5610</font></td> <td  sdval='4810' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4810</font></td> <td  sdval='4810' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4810</font></td> <td  sdval='4010' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4010</font></td> </tr> <tr> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='9' height='21' align='center' valign='middle'><b><font face='Open Sans' size='3' color='#000000'>South east, middle east and rest of Asia destination&nbsp;</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='77' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Singapore</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='77' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Bangkok/Phuket</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td ><font face='Open Sans' size='3' color='#000000'>Ex-India to Kuala Lumpur</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td ><font face='Open Sans' size='3' color='#000000'>Ex-India to Muscat</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='39' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Male</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td ><font face='Open Sans' size='3' color='#000000'>Ex-India to Kuwait</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='115' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Dubai/Sharjah/Abu Dhabi</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='39' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Doha</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td ><font face='Open Sans' size='3' color='#000000'>Ex-India to Jeddah</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td ><b><font face='Open Sans' size='3' color='#000000'>Europe sector - Istanbul</font></b></td> <td><font face='Times New Roman' size='3' color='#000000'><br></font></td> </tr> <tr> <td ><font face='Open Sans' size='3' color='#000000'>Ex-India to Istanbul</font></td> <td ><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>6500</font></td> <td ><font face='Open Sans' size='3' color='#000000'>6000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td  ><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td ><font face='Times New Roman' size='3' color='#000000'><br></font></td> </tr> </tbody></table>";

                        //StrFareRules += "<table> <colgroup></colgroup> <colgroup ></colgroup> <colgroup ></colgroup> <colgroup ></colgroup> <tbody><tr> <td><b><font face='Open Sans' color='#000'>Number of days left for departure</font></b></td> <td ><b><font face='Open Sans' color='#000'>Currency</font></b></td> <td><b><font face='Open Sans' color='#000'>Cancellation fee</font></b></td> <td><b><font face='Open Sans' color='#000'>Change Fee</font></b></td> </tr> <tr> <td><br></font></b></td> <td><b><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> <td><font face='Open Sans' color='#000'>0-3 (Days)</font></b></td> <td><b><font face='Open Sans' color='#000'>4 Days and above</font></b></td> </tr> <tr> <td><b><font face='Open Sans' size='3' color='#000000'>Indian-Sub continent</font></b></td> </tr> <tr> <td>Ex-India to Colombo</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-India to Dhaka</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-India to Kathmandu</font></td> <td><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td><font face='Open Sans' size='3' color='#000000'>3500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>3000</font></td> <td><font face='Open Sans' size='3' color='#000000'>2500</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex- Colombo to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>LKR</font></td> <td><font face='Open Sans' size='3' color='#000000'>8910</font></td> <td><font face='Open Sans' size='3' color='#000000'>7700</font></td> <td><font face='Open Sans' size='3' color='#000000'>7700</font></td> <td><font face='Open Sans' size='3' color='#000000'>6370</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-Dhaka to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>BDT</font></td> <td><font face='Open Sans' size='3' color='#000000'>4230</font></td> <td><font face='Open Sans' size='3' color='#000000'>3625</font></td> <td><font face='Open Sans' size='3' color='#000000'>3625</font></td> <td><font face='Open Sans' size='3' color='#000000'>3025</font></td> </tr> <tr> <td><font face='Open Sans' size='3' color='#000000'>Ex-Kathmandu to India</font></td> <td><font face='Open Sans' size='3' color='#000000'>NPR</font></td> <td><font face='Open Sans' size='3' color='#000000'>5610</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4810' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4810</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4810' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4810</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4010' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4010</font></td> </tr> <tr> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='9' height='21' align='center' valign='middle'><b><font face='Open Sans' size='3' color='#000000'>South east, middle east and rest of Asia destination&nbsp;</font></b></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='77' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Singapore</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='77' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Bangkok/Phuket</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='58' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Kuala Lumpur</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='58' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Muscat</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='39' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Male</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='58' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Kuwait</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='115' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Dubai/Sharjah/Abu Dhabi</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='39' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Doha</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='58' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Jeddah</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='4' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='3500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>3500</font></td> </tr> <tr> <td style='border-top: 2px solid #dedede; border-bottom: 2px solid #dedede' colspan='6' height='22' align='center' valign='middle'><b><font face='Open Sans' size='3' color='#000000'>Europe sector - Istanbul</font></b></td> <td style='border-top: 2px solid #dedede' colspan='3' align='left' valign='middle'><font face='Times New Roman' size='3' color='#000000'><br></font></td> </tr> <tr> <td style='border-bottom: 2px solid #dedede' height='58' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>Ex-India to Istanbul</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle'><font face='Open Sans' size='3' color='#000000'>INR</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='6500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>6500</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='6000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>6000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='5000' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>5000</font></td> <td style='border-bottom: 2px solid #dedede' align='center' valign='middle' sdval='4500' sdnum='1033;'><font face='Open Sans' size='3' color='#000000'>4500</font></td> <td colspan='3' align='left' valign='middle'><font face='Times New Roman' size='3' color='#000000'><br></font></td> </tr> </tbody></table>";
                        StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();


                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "G8" && RecLcc["Trip"].ToString() == "D")
                    {


                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        //Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        //StrFareRules += "<table cellspacing=0 border=1> <tr> <td style=min-width:50px>DOMESTIC FLIGHT CANCELLATION CHARGES </td> <td style=min-width:50px>DOMESTIC FLIGHT REBOOKING CHARGES </td> <td style=min-width:50px></td> </tr> <tr> <td style=min-width:50px>0-2 hours: 100% except taxes. up to 2 hours before scheduled departure: ₹ 3,000/- or Base Fare + Fuel Surcharge, whichever is lower shall be applicable</td> <td style=min-width:50px>₹2,225/- + difference in base fare</td> <td style=min-width:50px></td> </tr> <tr> <td style=min-width:50px></td> <td style=min-width:50px></td> <td style=min-width:50px></td> </tr></table>";
                        //StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";


                        StrFareRules += "<div class='can-b2b-tr'>";
                        StrFareRules += "<div class='lef-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Cancellation Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "<div class='rig-b2b-cane'>";
                        StrFareRules += "<div class='b2b-ca-char'>Reschedule Charges</div>";
                        StrFareRules += "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>";
                        StrFareRules += "<tbody>";
                        StrFareRules += "<tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 3 Days*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 2500</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr><tr class='ng-scope'>";
                        StrFareRules += "<td scope='row' width='50%'><span class='ng-binding'>Before 6 hours*</span></td>";
                        StrFareRules += "<td width='50%'>";
                        StrFareRules += "<span class='fa'></span>";
                        StrFareRules += "<span class='ng-binding'>₹ 3000</span>";
                        StrFareRules += "</td>";
                        StrFareRules += "</tr>";
                        //StrFareRules += "<tr>";
                        //StrFareRules += "<td scope='row'>SALES EMT Fee</td>";
                        //StrFareRules += "<td>";
                        //StrFareRules += "<span class='fa'></span>";
                        //StrFareRules += "<span class='ng-binding'>₹ 25</span>";
                        //StrFareRules += "</td>";
                        //StrFareRules += "</tr>";
                        StrFareRules += "</tbody>";
                        StrFareRules += "</table>";
                        StrFareRules += "</div>";
                        StrFareRules += "</div>";

                        StrFareRules += "<div class='trm-had'>Terms &amp; Conditions</div>";
                        StrFareRules += "<div class='terms-b2b2-cancahe'>";
                        StrFareRules += "<ul>";
                        StrFareRules += "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>";
                        StrFareRules += "<li>The charges will be on per passenger per sector</li>";
                        StrFareRules += "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>";
                        StrFareRules += "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>";
                        StrFareRules += "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>";
                        StrFareRules += "<li>For infants there is no baggage allowance</li>";
                        StrFareRules += "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>";
                        StrFareRules += "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>";
                        StrFareRules += "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>";
                        StrFareRules += "</ul>";
                        StrFareRules += "</div>";
                        
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();


                    }
                    else if (RecLcc["ValiDatingCarrier"].ToString() == "G8" && RecLcc["Trip"].ToString() == "I")
                    {


                        DepLocation = RecLcc["DepartureLocation"].ToString();
                        ArvlLoction = RecLcc["ArrivalLocation"].ToString();

                        FareRule1 obj = new FareRule1();
                        tt.Response = new ResponseFareRule();
                        tt.Response.FareRules = new List<FareRule1>();
                        tt.Response.FareRules.Add(obj);

                        //Htmllist += "<li><a class='tablinks' title='Click for fare rules' onclick=\"SelectedSector(event,'" + DepLocation + "-" + ArvlLoction + "')\">" + Sector + " </a></li>";
                        StrFareRules += "<table cellspacing=0 border=1> <tr> <td style=min-width:50px> INTERNATIONAL TICKETS CANCELLATION CHARGES </td> <td style=min-width:50px>Change Fee</td> <td style=min-width:50px>Cancellation Fee</td> </tr> <tr> <td style=min-width:50px>Sector</td> <td style=min-width:50px></td> <td style=min-width:50px></td> </tr> <tr> <td style=min-width:50px>Ex India</td> <td style=min-width:50px>INR 4500 + difference in fare</td> <td style=min-width:50px>INR 4500</td> </tr> <tr> <td style=min-width:50px>Ex Male</td> <td style=min-width:50px>USD 64 + difference in fare</td> <td style=min-width:50px>USD 64</td> </tr> <tr> <td style=min-width:50px>Ex Phuket</td> <td style=min-width:50px>THB 2140 + difference in fare</td> <td style=min-width:50px>THB 2140</td> </tr> <tr> <td style=min-width:50px>Ex Muscat</td> <td style=min-width:50px>OMR 25 + difference in fare</td> <td style=min-width:50px>OMR 25</td> </tr> <tr> <td style=min-width:50px>Ex Abu Dhabi</td> <td style=min-width:50px>AED 237 + difference in fare</td> <td style=min-width:50px>AED 237</td> </tr> <tr> <td style=min-width:50px></td> <td style=min-width:50px></td> <td style=min-width:50px></td> </tr> <tr> <td style=min-width:50px></td> <td style=min-width:50px></td> <td style=min-width:50px></td> </tr> </table>";
                        StrFareRules = "<div id='Finalfarerule'><ul class=tab>" + Htmllist + "</ul>" + StrFareRules.Trim() + "</div>";

                        //StrFareRules += "<div class='wid_option2'>";
                        //StrFareRules += "<span>Select Fare Type:</span>";
                        //StrFareRules += "<select><option label='Regular Fare' value='' selected='selected'>Regular Fare</option>";
                        //StrFareRules += "<option label=' Special Fare' value=''> Special Fare</option></select>";
                        //StrFareRules += "</div>";

                        
                        
                        
                        tt.Response.FareRules[0].FareRuleDetail = StrFareRules.ToString();


                    }
                    else
                    {
                        STD.BAL.TBO.TBOFareRule obj = new TBOFareRule();
                        tt = obj.GetFareRule("", sno);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return tt;
    }
    private void GetCredentials(string conn,string ProviderUserId )
    {
        ProviderList = STD.BAL.Data.GetProviderCrd(conn);
        if (!string.IsNullOrEmpty(ProviderUserId))
        {
            url = ((from crd in ProviderList where crd.Provider == "1G" && crd.UserID == ProviderUserId select crd).ToList())[0].AvailabilityURL;
            Userid = ((from crd in ProviderList where crd.Provider == "1G" && crd.UserID == ProviderUserId select crd).ToList())[0].UserID;
            Pwd = ((from crd in ProviderList where crd.Provider == "1G" && crd.UserID == ProviderUserId select crd).ToList())[0].Password;
            pcc = ((from crd in ProviderList where crd.Provider == "1G" && crd.UserID == ProviderUserId select crd).ToList())[0].CarrierAcc;
        }
        else
        {
            url = ((from crd in ProviderList where crd.Provider == "1G" select crd).ToList())[0].AvailabilityURL;
            Userid = ((from crd in ProviderList where crd.Provider == "1G" select crd).ToList())[0].UserID;
            Pwd = ((from crd in ProviderList where crd.Provider == "1G" select crd).ToList())[0].Password;
            pcc = ((from crd in ProviderList where crd.Provider == "1G" select crd).ToList())[0].CarrierAcc;
        }
    }

    [WebMethod]
    public List<City> FetchGSTStateList(string cityCode)
    {
        List<City> objgstcity = new List<City>();
        try
        {
            DataSet CityDS = new DataSet();
            CityDS = GetStateList(cityCode);

            for (int i = 0; (i <= (CityDS.Tables[0].Rows.Count - 1)); i++)
            {
                objgstcity.Add(new City { ID = i, CityName = CityDS.Tables[0].Rows[i]["CITY"].ToString().Trim(), AirportCode = CityDS.Tables[0].Rows[i]["STATEID"].ToString().Trim(), ALCode = CityDS.Tables[0].Rows[i]["StateCode"].ToString().Trim() });
            }
        }
        catch(Exception ex)
        { }
        return objgstcity;

    }
    public DataSet GetStateList(string code) {
        DataSet gstStateDs=new DataSet();
        try{
            SqlDatabase DBHelper = new SqlDatabase(Con);
            DbCommand cmd = new SqlCommand();
            cmd.CommandText = "Usp_GetStateList_V1";
            cmd.CommandType = CommandType.StoredProcedure;
            DBHelper.AddInParameter(cmd, "@param1", DbType.String, code);
            gstStateDs = DBHelper.ExecuteDataSet(cmd);
        }
        catch(Exception ex)
        {}
        return gstStateDs;
    }
        [WebMethod]
    public List<string> SaveGSTDeatil(string agencyid, string gstno, string comname, string comadd, string pin, string state, string city, string phone, string email)
    {
        List<string> result = new List<string>();
        SqlCommand command = new SqlCommand();

        try
        {
            if (!string.IsNullOrEmpty(agencyid))
            {
                SqlTransaction objDA = new SqlTransaction();
                DataSet AgencyDs = objDA.GetAgencyDetails(agencyid);
                if (AgencyDs != null && AgencyDs.Tables[0].Rows.Count > 0)
                {
                    string agencyname = AgencyDs.Tables[0].Rows[0]["Agency_Name"].ToString();

                    SqlConnection connection = new SqlConnection(Con);
                    command = new SqlCommand("sp_InsertAgencyGSTDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@GSTNo", gstno));
                    command.Parameters.Add(new SqlParameter("@CompanyName", comname));
                    command.Parameters.Add(new SqlParameter("@CompanyAddress", comadd));
                    command.Parameters.Add(new SqlParameter("@GSTPinCode", pin));
                    command.Parameters.Add(new SqlParameter("@GSTState", state));
                    command.Parameters.Add(new SqlParameter("@GSTCity", city));
                    command.Parameters.Add(new SqlParameter("@GSTPhone", phone));
                    command.Parameters.Add(new SqlParameter("@GSTEmail", email));
                    command.Parameters.Add(new SqlParameter("@Agentid", agencyid));
                    command.Parameters.Add(new SqlParameter("@AgencyName", agencyname));
                    connection.Open();
                    int isSuccess = command.ExecuteNonQuery();
                    connection.Close();

                    if (isSuccess > 0)
                    {
                        result.Add("true");
                        result.Add(GetAgencyGSTbyAgentId(agencyid, ""));
                    }
                }
            }
            else
            {
                result.Add("error");
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        return result;
    }

    [WebMethod]
    public List<string> GetTravGstByGstId(string agencyid, string gstid)
    {
        List<string> result = new List<string>();

        try
        {
            DataTable dataTable = GetGstDetailByID(agencyid, gstid);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    result.Add(dataTable.Rows[i]["GSTNo"].ToString());
                    result.Add(dataTable.Rows[i]["CompanyName"].ToString());
                    result.Add(dataTable.Rows[i]["CompanyAddress"].ToString());
                    result.Add(dataTable.Rows[i]["GSTPinCode"].ToString());
                    result.Add(dataTable.Rows[i]["GSTState"].ToString());
                    result.Add(dataTable.Rows[i]["GSTCity"].ToString());
                    result.Add(dataTable.Rows[i]["GSTPhone"].ToString());
                    result.Add(dataTable.Rows[i]["GSTEmail"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        return result;
    }

    [WebMethod]
    public string GetAgencyGSTbyAgentId(string agencyid, string gstid)
    {
        StringBuilder sbRecord = new StringBuilder();

        try
        {
            DataTable dataTable = GetGstDetailByID(agencyid, gstid);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sbRecord.Append("<div class='col-sm-12' style='border-bottom: 1px solid #ccc; padding: 3px; font-size: 14px;'>");
                    sbRecord.Append("<div class='col-sm-4'>  <input type='radio' id='GSTid_" + dataTable.Rows[i]["Gstid"].ToString() + "' name='gstCompany' value='" + dataTable.Rows[i]["Gstid"].ToString() + "' /> <label for='male'>" + dataTable.Rows[i]["CompanyName"].ToString() + "</label></div>");
                    sbRecord.Append("<div class='col-sm-4'>  <label>" + dataTable.Rows[i]["GSTNo"].ToString() + "</label></div>");
                    sbRecord.Append("<div class='col-sm-4'><label>" + dataTable.Rows[i]["CompanyAddress"].ToString() + "</label></div>");
                    sbRecord.Append("</div>");
                }
            }
            else
            {
                sbRecord.Append("<div class='col-sm-12 style='border-bottom: 1px solid #ccc; padding: 3px; font-size: 14px;'>");
                sbRecord.Append("<label style='font-size: 12px;text-align: center;color: red;'>No record found !</label></div>");
                sbRecord.Append("</div>");
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        return sbRecord.ToString();
    }

    private DataTable GetGstDetailByID(string agencyid, string gstid)
    {
        DataTable dataTable = new DataTable();
        SqlCommand command = new SqlCommand();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        DataSet dataSet = new DataSet();
        try
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString.ToString());
            command = new SqlCommand("sp_GetAgencyGSTbyAgentId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Agentid", agencyid));
            command.Parameters.Add(new SqlParameter("@GstId", !string.IsNullOrEmpty(gstid) ? gstid : null));
            sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = command;
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "GetAgencyGST");
            dataTable = dataSet.Tables["GetAgencyGST"];

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        return dataTable;
    }

}


