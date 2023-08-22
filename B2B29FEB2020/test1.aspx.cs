using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using STD.BAL;
using STD.BAL;
using STD.Shared;
using STD.DAL;
using STD.BAL.TBO;
using Newtonsoft.Json;
using SMSAPI;
using System.Data;
public partial class test1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string Action = "";
        //string agent = "";
        //string Amount = "";
        //if (!string.IsNullOrEmpty(agent) && (!string.IsNullOrEmpty(agent) || !string.IsNullOrEmpty(Amount)))
        //{
        //    PGResponsParse(Request.Form["txnid"]);
        //}

        //if (string.IsNullOrEmpty(agent) && (string.IsNullOrEmpty(agent) || string.IsNullOrEmpty(Amount)))
        //{
        //    PGResponsParse(Request.Form["txnid"]);
        //}





     //   SMSAPI.SMS obj = new SMS();
     //   string test ="";

     //   string orderid = DateTime.Now.ToString("HHmmss");
     //   string result = obj.sendSms(orderid, "9999296937", "del:bom", "AI", "4355", "12 june", "ASDVF4", ref test);

     //Response.Write(result);
        
        
//int a = 0;



        
//        string res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <soapenv:Body> <SubmitXmlOnSessionResponse xmlns=\"http://webservices.galileo.com\"> <SubmitXmlOnSessionResult> <PNRBFManagement_33 xmlns=\"\"> <TransactionErrorCode> <Domain>AppErrorSeverityLevel</Domain> <Code>1</Code> </TransactionErrorCode> <EndTransaction> <ErrorCode>0001</ErrorCode> <EndTransactResponse> <FileAddr/> <RecCodeCheck/> <RecLoc/> <ErrSeverityInd>E</ErrSeverityInd> <LangPref/> </EndTransactResponse> </EndTransaction> </PNRBFManagement_33> </SubmitXmlOnSessionResult> </SubmitXmlOnSessionResponse> </soapenv:Body> </soapenv:Envelope>";

//        string str = res.Replace("xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"", "").Replace("soapenv:", "")
//            .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
//            .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
//        .Replace("xmlns=\"http://webservices.galileo.com\"", "");
//                XDocument xmlDoc = XDocument.Parse(str);
//                if ( xmlDoc.Descendants("SubmitXmlOnSessionResult").Descendants("EndTransactMessage").Any())
//                {
//                    string aa = xmlDoc.Descendants("SubmitXmlOnSessionResult").Descendants("EndTransactMessage").Elements("Text").Where(x => x.Value.ToString().Contains("SIMULTANEOUS CHANGES TO BOOKING FILE")).FirstOrDefault().Value;
                
//                }



//        int eu = 31;


        ////string res = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <soapenv:Body> <YT_NimbleRS HostName=\"xdist02\"> <Success /> <Supplier AirlineCode=\"G8\" Code=\"G8CP\" Type=\"LCC\" /> <Supplier Code=\"1AWS4\" Type=\"GDS\" /> <Supplier AirlineCode=\"6E\" Code=\"6EAPINavi\" Type=\"LCC\" /> <Supplier AirlineCode=\"SG\" Code=\"SGAPINavitaire\" Type=\"LCC\" /> </YT_NimbleRS> </soapenv:Body> </soapenv:Envelope>";
       
        ////      string str = res.Replace("xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"", "").Replace("soapenv:", "")
        ////               .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
        ////               .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
        ////               .Replace("xmlns=\"http://www.opentravel.org/OTA/2003/05\"", "")
        ////               .Replace("xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\"", "")
        ////               .Replace("ns1:", "");

        ////        XDocument xmlDoc = XDocument.Parse(str);




        ////     string val =  xmlDoc.Descendants("YT_NimbleRS").Descendants("Supplier").Where(x => x.Attributes("AirlineCode").Any() == true).Where(y => y.Attribute("AirlineCode").Value == "G8").Select(z=>z.Attribute("Code")).FirstOrDefault().Value.Trim().ToUpper();
                                          //where el.Attribute("AirlineCode").Value.Trim().ToUpper() == "G8"
                                         // select Convert.ToString(el.Attribute("Code").Value).Trim();

               // res = val.ToList();//[0].Trim();

                string aaf = "";

        //string aa = "{\"Response\":{\"Error\":{\"ErrorCode\":0,\"ErrorMessage\":\"\"},\"IsPriceChanged\":false,\"ResponseStatus\":1,\"Results\":{\"ResultIndex\":\"OB4\",\"Source\":17,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":\"If you are traveling in flights (FZ 7000 to 7999) to and from Jeddah then you will need Umrah visa to board flight\",\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":7252.93,\"Tax\":1707.07,\"TaxBreakup\":[{\"key\":\"YQTax\",\"value\":0},{\"key\":\"YR\",\"value\":0},{\"key\":\"PSF\",\"value\":0},{\"key\":\"UDF\",\"value\":0},{\"key\":\"JNTax\",\"value\":0},{\"key\":\"INTax\",\"value\":0},{\"key\":\"TransactionFee\",\"value\":0}],\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":8975,\"CommissionEarned\":0,\"PLBEarned\":0,\"IncentiveEarned\":0,\"OfferedFare\":8975.00,\"TdsOnCommission\":0,\"TdsOnPLB\":0,\"TdsOnIncentive\":0,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":7252.93,\"Tax\":1707.07,\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"Baggage\":\"0 Kg\",\"CabinBaggage\":\"7 Kg\",\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"FZ\",\"AirlineName\":\"FlyDubai\",\"FlightNumber\":\"432\",\"FareClass\":\"PC\",\"OperatingCarrier\":\"\"},\"Origin\":{\"Airport\":{\"AirportCode\":\"DEL\",\"AirportName\":\"Indira Gandhi Airport\",\"Terminal\":\"\",\"CityCode\":\"DEL\",\"CityName\":\"Delhi\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-09-30T04:00:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"DXB\",\"AirportName\":\"Dubai\",\"Terminal\":\"\",\"CityCode\":\"DXB\",\"CityName\":\"Dubai\",\"CountryCode\":\"AE\",\"CountryName\":\"United Arab Emirates\"},\"ArrTime\":\"2016-09-30T06:15:00\"},\"Duration\":225,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-09-30T06:15:00\",\"StopPointDepartureTime\":\"2016-09-30T04:00:00\",\"Craft\":\"73B\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-09-08T06:55:50\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"DEL\",\"Destination\":\"DXB\",\"Airline\":\"FZ\",\"FareBasisCode\":\"RO6IN2\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"FZ\",\"ValidatingAirline\":\"FZ\"},\"TraceId\":\"50cf399d-bce9-41b6-8e32-0085dbc1b4ad\"}}";
        //      STD.BAL.TBO.FareQuote.FareQuoteResp result = new  STD.BAL.TBO.FareQuote.FareQuoteResp();
        // result = JsonConvert.DeserializeObject<STD.BAL.TBO.FareQuote.FareQuoteResp>(aa);

        //string xmlprice = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <soapenv:Body> <ns1:OTA_AirPriceRS xmlns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\"> <ns1:PricedItineraries> <ns1:PricedItinerary ApplyMarkup=\"Y\" ApplyMarkupMsg=\"\" FareType=\"Refundable\" LFSPRICEDIFF=\"0\" OriginDestinationRPH=\"DELBOM6E1716E20160730\" OriginDestinationRefNumber=\"1\" ProductClass=\"R\" SequenceNumber=\"1.0\" SupplierSystem=\"6ENAV\" TBFValid=\"true\" isTBF=\"false\" xmlns=\"http://www.opentravel.org/OTA/2003/05\"> <ns1:AirItinerary FareType=\"Refundable\" SupplierCode=\"6EAPINavi\" SupplierSystem=\"6ENAV\" TripType=\"\" UniqueIdentifier=\"1\"> <ns1:OriginDestinationOptions WorkFlow=\"PFB\"> <ns1:OriginDestinationOption Cabin=\"\" Duration=\"02:010:00\" FareType=\"\" FlightID=\"DELBOM6E1716E20160730\" ODCabin=\"\" ODKey=\"2016073005\" ReturnOnly=\"false\" Sell=\"true\" StopQuantity=\"0\" SupplierCode=\"6EAPINavi\" UniqueIdentifier=\"1.0\"> <ns1:FlightSegment ArrivalDateTime=\"2016-07-30T07:40:00\" Cabin=\"\" DepartureDateTime=\"2016-07-30T05:30:00\" Duration=\"02:10:00\" FlightNumber=\"171\" OfficeID=\"14363904\" RPH=\"1\" ResBookDesigCode=\"L\" StopQuantity=\"0\"> <ns1:DepartureAirport AirPortName=\"Indira Gandhi\" CityName=\"New Delhi\" CodeContext=\"IATA\" CountryCode=\"IN\" CountryName=\"India\" LocationCode=\"DEL\" Terminal=\"1B\" TerminalID=\"1B\"/> <ns1:ArrivalAirport AirPortName=\"Chatrapati Shivaji\" CityName=\"Mumbai\" CodeContext=\"IATA\" CountryCode=\"IN\" CountryName=\"India\" LocationCode=\"BOM\" Terminal=\"1B\" TerminalID=\"1B\"/> <ns1:Equipment AirEquipType=\"\"/> <ns1:MarketingAirline Code=\"6E\" CodeContext=\"IATA\" MatrixCode=\"6E\" Name=\"Indigo\" YTAirlineCode=\"6E\"/> <ns1:BookingClassAvail FareType=\"\" ResBookDesigCode=\"L\" WebFareName=\"XXXXX\"/> <ns1:BookingClassAvail FareType=\"\" ResBookDesigCode=\"L\"/> </ns1:FlightSegment> <ns1:FormData> <ns1:sellKey>6E~ 171~ ~~DEL~07/30/2016 05:30~BOM~07/30/2016 07:40~</ns1:sellKey> <ns1:fareKey>0~L~~LSPL~4910~~3~X</ns1:fareKey> <ns1:session>7q0ZPf41pRc=|AR3+DpxZs+OsOPhjyGpYGD5g3vA6looM54iVOvXNvJ5NLvniH05YBgftlMaSkCFsRC74pWVpa1vV8sXuV2nowWLmkRY09g7V6q8pHNHKxclYJx9IaiilGHBOT+9kDgrCKQ8ZOvZccgw=</ns1:session> <ns1:PrimaryLangID>Yatra@2015|14363904|14363904</ns1:PrimaryLangID> <ns1:OfficeID>14363904</ns1:OfficeID> <ns1:Cabin/> <ns1:FareDifference> <ns1:TotalFare ADT=\"1\" BaseFare=\"2139-ADT 2139\" CHD=\"0\" Cabin=\"Economy\" HostName=\"qaxdist\" INF=\"0\" Rbd=\" - L - 7\" Tax=\"PSF-146YQ-200UDF-551JN-135WC-0DF-0OC-0CM-75SBC-5KKC-5\">3256</ns1:TotalFare> </ns1:FareDifference> <ns1:HostName>qaxdist</ns1:HostName> <ns1:HostName/> <ns1:FormData> <ns1:sellKey>6E~ 171~ ~~DEL~07/30/2016 05:30~BOM~07/30/2016 07:40~</ns1:sellKey> <ns1:fareKey>0~L~~LSPL~4910~~3~X</ns1:fareKey> <ns1:session>RNO6O3WQ5us=|OaDEe1d1Gz+DrsAQQ7nnpkogPIva3ZH/HELwbqg0ldIpc2/TzEhsQrCXBc80tdlUmrPYGK75MtsCeU1DpoXXmNspE6RHYdESACKw/QJnt+HJHKkv8jEoPnQo3wagNWVM7JF0wiQAiDI=</ns1:session> <ns1:PrimaryLangID>Yatra@2015|14363904|14363904</ns1:PrimaryLangID> <ns1:OfficeID>14363904</ns1:OfficeID> <ns1:Cabin/> <ns1:FareDifference> <ns1:TotalFare ADT=\"1\" BaseFare=\"2139-ADT 2139\" CHD=\"0\" Cabin=\"Economy\" HostName=\"qaxdist\" INF=\"0\" Rbd=\" - L - 7\" Tax=\"PSF-146YQ-200UDF-551JN-135WC-0DF-0OC-0CM-75SBC-5KKC-5\">3256</ns1:TotalFare> </ns1:FareDifference> <ns1:TicketingInfo DeliveryMethod=\"EMAIL\" TicketTimeLimit=\"\" TicketType=\"eTicket\" TicketingStatus=\"\" eTicketNumber=\"\"> <ns1:TicketAdvisory Language=\"English\"/> </ns1:TicketingInfo> <ns1:ApplyMarkup>Y</ns1:ApplyMarkup> <ns1:AgentMarkup> <ns1:Airlines> <ns1:Airline> <ns1:Code>6E</ns1:Code> <ns1:GDS>0</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>3.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>2700.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> <ns1:Classes> <ns1:Class> <ns1:ClassCode>A</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>B</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>C</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>D</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>E</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>F</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> <ns1:Class> <ns1:ClassCode>G</ns1:ClassCode> <ns1:Iata>3.00</ns1:Iata> <ns1:IataOn>BF</ns1:IataOn> <ns1:IataType>P</ns1:IataType> <ns1:Plb>2.00</ns1:Plb> <ns1:PlbType>P</ns1:PlbType> <ns1:PlbOn>BF</ns1:PlbOn> </ns1:Class> </ns1:Classes> </ns1:Airline> <ns1:Airline> <ns1:Code>9W</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>2.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>2.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>AI</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>2.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>0.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>F</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>DN</ns1:Code> <ns1:GDS>0</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>0.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>3250.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>G8</ns1:Code> <ns1:GDS>0</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>2.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>4.5000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>2700.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>IC</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>0.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>B</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>IT</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>3.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>0.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>LB</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>P</ns1:Type> <ns1:DiscountPLB>3.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>S2</ns1:Code> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>F</ns1:Type> <ns1:DiscountPLB>75.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>F</ns1:DiscountPLBType> <ns1:DiscountPLBOn>B</ns1:DiscountPLBOn> <ns1:Surcharge>1100.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Airline> <ns1:Code>SG</ns1:Code> <ns1:GDS>0</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:WholeSalerFeesType>F</ns1:WholeSalerFeesType> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>BF</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> <ns1:Type>F</ns1:Type> <ns1:DiscountPLB>2.0000</ns1:DiscountPLB> <ns1:DiscountPLBType>P</ns1:DiscountPLBType> <ns1:DiscountPLBOn>BF</ns1:DiscountPLBOn> <ns1:Surcharge>2000.0000</ns1:Surcharge> <ns1:AdultMarkup>0</ns1:AdultMarkup> <ns1:ChildMarkup>0</ns1:ChildMarkup> <ns1:InfantMarkup>0</ns1:InfantMarkup> <ns1:MarkupOn/> <ns1:MarkupType>F</ns1:MarkupType> </ns1:Airline> <ns1:Others> <ns1:Code/> <ns1:GDS>1</ns1:GDS> <ns1:WholeSalerFees>0</ns1:WholeSalerFees> <ns1:CommissionType>C</ns1:CommissionType> <ns1:ApplyOn>B</ns1:ApplyOn> <ns1:Amount>0.0000</ns1:Amount> </ns1:Others> </ns1:Airlines> </ns1:AgentMarkup> </ns1:FormData> <ns1:ApplyMarkup>Y</ns1:ApplyMarkup> </ns1:FormData> <ns1:FormData> <ns1:sellKey>6E~ 171~ ~~DEL~07/30/2016 05:30~BOM~07/30/2016 07:40~</ns1:sellKey> <ns1:fareKey>0~L~~LSPL~4910~~3~X</ns1:fareKey> <ns1:session>7q0ZPf41pRc=|AR3+DpxZs+OsOPhjyGpYGD5g3vA6looM54iVOvXNvJ5NLvniH05YBgftlMaSkCFsRC74pWVpa1vV8sXuV2nowWLmkRY09g7V6q8pHNHKxclYJx9IaiilGHBOT+9kDgrCKQ8ZOvZccgw=</ns1:session> <ns1:PrimaryLangID>Yatra@2015|14363904|14363904</ns1:PrimaryLangID> <ns1:OfficeID>14363904</ns1:OfficeID> <ns1:Cabin/> <ns1:FareDifference> <ns1:TotalFare ADT=\"1\" BaseFare=\"2139-ADT 2139\" CHD=\"0\" Cabin=\"Economy\" HostName=\"qaxdist\" INF=\"0\" Rbd=\" - L - 7\" Tax=\"PSF-146YQ-200UDF-551JN-135WC-0DF-0OC-0CM-75SBC-5KKC-5\">3256</ns1:TotalFare> </ns1:FareDifference> </ns1:FormData> <ns1:FormData/> </ns1:OriginDestinationOption> </ns1:OriginDestinationOptions> </ns1:AirItinerary> <ns1:AirItineraryPricingInfo UniqueIdentifier=\"1.0\"> <ns1:ItinTotalFare> <ns1:BaseFare Amount=\"2139\" CurrencyCode=\"INR\"/> <ns1:Taxes> <ns1:Tax Amount=\"146\" CurrencyCode=\"INR\" Description=\"Passenger Service Fee\" FareBasisCode=\"L\" TaxCode=\"PSF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"200\" CurrencyCode=\"INR\" Description=\"Fuel Surcharge\" FareBasisCode=\"L\" TaxCode=\"YQ\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"551\" CurrencyCode=\"INR\" Description=\"User Development Fee\" FareBasisCode=\"L\" TaxCode=\"UDF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"135\" CurrencyCode=\"INR\" Description=\"Govt. Service Tax\" FareBasisCode=\"L\" TaxCode=\"JN\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Web transaction Charges\" FareBasisCode=\"L\" TaxCode=\"WC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Development Fee\" FareBasisCode=\"L\" TaxCode=\"DF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Other Charges\" FareBasisCode=\"L\" TaxCode=\"OC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"75\" CurrencyCode=\"INR\" Description=\"Commission\" FareBasisCode=\"L\" TaxCode=\"CM\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"5\" CurrencyCode=\"INR\" Description=\"Swachh Bharat Cess\" FareBasisCode=\"L\" TaxCode=\"SBC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"5\" CurrencyCode=\"INR\" Description=\"Krishi Kalyan Cess\" FareBasisCode=\"L\" TaxCode=\"KKC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" TaxCode=\"B2BMarkup\"/> </ns1:Taxes> <ns1:TotalFare Amount=\"3266.53\" CurrencyCode=\"INR\"/> <ns1:Fees> <ns1:Fee Amount=\"0\" FeeCode=\"YatraLCCSurcharge\"/> <ns1:Fee Amount=\"0\"/> </ns1:Fees> <ns1:Meal UniqueIdentifier=\"1\"> <ns1:SSRCode Amount=\"250\" Code=\"VGML\" Conversion=\"250\" Currency=\"INR\" Desc=\"Veg Meal\" UniqueIdentifier=\"1\"/> <ns1:SSRCode Amount=\"300\" Code=\"NVML\" Conversion=\"300\" Currency=\"INR\" Desc=\"Non Veg Meal\" UniqueIdentifier=\"1\"/> </ns1:Meal> <ns1:FareBaggage UniqueIdentifier=\"1\"> <ns1:SSRCode Amount=\"1125\" Code=\"XBPA\" Conversion=\"1125\" Currency=\"INR\" Desc=\"5 kg\" UniqueIdentifier=\"1\"/> <ns1:SSRCode Amount=\"2250\" Code=\"XBPB\" Conversion=\"2250\" Currency=\"INR\" Desc=\"10 kg\" UniqueIdentifier=\"1\"/> <ns1:SSRCode Amount=\"3375\" Code=\"XBPC\" Conversion=\"3375\" Currency=\"INR\" Desc=\"15 kg\" UniqueIdentifier=\"1\"/> <ns1:SSRCode Amount=\"6750\" Code=\"XBPD\" Conversion=\"6750\" Currency=\"INR\" Desc=\"30 kg\" UniqueIdentifier=\"1\"/> </ns1:FareBaggage> <ns1:ServiceTax Amount=\"10.53\" Currency=\"INR\" Description=\"Service Tax\" ServiceTaxCode=\"ST\" ServiceTaxRate=\"0\"/> <ns1:FareBaggageAllowance AirlineCode=\"6E\" PaxType=\"ADT\" UnitOfMeasureCode=\"kgs\" UnitOfMeasureQuantity=\"15\"/> <ns1:Markups> <ns1:Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0 wholesalerfees for client 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"W\" Type=\"C\"/> <ns1:Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"C\"/> <ns1:Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0% on fuel surcharge 200 -for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"C\"/> <ns1:Markup Amount=\"64.17\" CurrencyCode=\"INR\" Description=\"3.0% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> <ns1:Markup Amount=\"6\" CurrencyCode=\"INR\" Description=\"3.0% on fuel surcharge 200 -for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> </ns1:Markups> </ns1:ItinTotalFare> <ns1:PTC_FareBreakdowns> <ns1:PTC_FareBreakdown> <ns1:PassengerTypeQuantity Code=\"ADT\" CodeContext=\"IATA\" Quantity=\"1\"/> <ns1:PassengerFare> <ns1:BaseFare Amount=\"2139\" CurrencyCode=\"INR\"/> <ns1:Taxes> <ns1:Tax Amount=\"146\" CurrencyCode=\"INR\" Description=\"Passenger Service Fee\" FareBasisCode=\"L\" TaxCode=\"PSF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"5\" CurrencyCode=\"INR\" Description=\"Swachh Bharat Cess\" FareBasisCode=\"L\" TaxCode=\"SBC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"5\" CurrencyCode=\"INR\" Description=\"Krishi Kalyan Cess\" FareBasisCode=\"L\" TaxCode=\"KKC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"551\" CurrencyCode=\"INR\" Description=\"User Development Fee\" FareBasisCode=\"L\" TaxCode=\"UDF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"200\" CurrencyCode=\"INR\" Description=\"Fuel Surcharge\" FareBasisCode=\"L\" TaxCode=\"YQ\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"135\" CurrencyCode=\"INR\" Description=\"Govt. Service Tax\" FareBasisCode=\"L\" TaxCode=\"JN\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Development Fee\" FareBasisCode=\"L\" TaxCode=\"DF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Web Transaction Charges\" FareBasisCode=\"L\" TaxCode=\"WC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Other Charges\" FareBasisCode=\"L\" TaxCode=\"OC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"75\" CurrencyCode=\"INR\" Description=\"Commission\" FareBasisCode=\"L\" TaxCode=\"CM\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp;amp; Fees\"/> <ns1:Tax Amount=\"0\" TaxCode=\"B2BMarkup\"/> </ns1:Taxes> <ns1:TotalFare Amount=\"3256\" CurrencyCode=\"INR\"/> <ns1:Fees/> <ns1:ServiceTax Amount=\"0\" Currency=\"INR\" Description=\"Service Tax\" ServiceTaxCode=\"ST\" ServiceTaxRate=\"0\"/> <ns1:Markups> <ns1:Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0 wholesalerfees for client 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"W\" Type=\"C\"/> <ns1:Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0 % on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"C\"/> <ns1:Markup Amount=\"0\" Description=\"0.0% on fule surcharge 200.0for client1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"C\"/> <ns1:Markup Amount=\"64.17\" CurrencyCode=\"INR\" Description=\"3.0 % on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> <ns1:Markup Amount=\"6\" Description=\"3.0% on fule surcharge 200.0for client1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> </ns1:Markups> </ns1:PassengerFare> <ns1:FareBasisCodes> <ns1:FareBasisCode BaseFareRefundperPAX=\"-111\" CancelAmtValue=\"2250\" FareType=\"Refundable\" RefundType=\"A\" ResBookDesigCode=\"N\" YatraOfflineFee=\"50\" YatraOnlineFee=\"50\" bfrMonth=\"2000\" bfrWeek=\"1250\" bfrYear=\"1500\">XXXXX</ns1:FareBasisCode> </ns1:FareBasisCodes> <ns1:RefundInfo BaseFareRefundperPAX=\"-111\" CancelAmtValue=\"2250\" FareBasisUsed=\"-\" RefundType=\"A\" ResBookDesigCode=\"-\" YatraOfflineFee=\"50\" YatraOnlineFee=\"50\" bfrMonth=\"2000\" bfrWeek=\"1250\" bfrYear=\"1500\"/> <ns1:RefundInfoSlab> <ns1:Slab> <ns1:StartHour>2</ns1:StartHour> <ns1:EndHour>352</ns1:EndHour> <ns1:CancelAmtValue>2250</ns1:CancelAmtValue> </ns1:Slab> </ns1:RefundInfoSlab> </ns1:PTC_FareBreakdown> </ns1:PTC_FareBreakdowns> </ns1:AirItineraryPricingInfo> <ns1:Notes>6E</ns1:Notes> </ns1:PricedItinerary> </ns1:PricedItineraries> </ns1:OTA_AirPriceRS> </soapenv:Body> </soapenv:Envelope>";
        //xmlprice = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <soapenv:Body> <OTA_AirPriceRS xmlns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\"> <PricedItineraries> <PricedItinerary FareType=\"Refundable\" OriginDestinationRPH=\"DELBOMSG151BOMDELSG152\" OriginDestinationRefNumber=\"2\" SequenceNumber=\"\" SupplierSystem=\"SGNAV\" xmlns=\"http://www.opentravel.org/OTA/2003/05\"> <AirItinerary FareType=\"Refundable\" SupplierCode=\"SGAPINavitaire\" SupplierSystem=\"SGNAV\"> <OriginDestinationOptions WorkFlow=\"PFB\"> <OriginDestinationOption Duration=\"03:20:00\" FareBasisCode=\"XD\" FlightID=\"DELBOMSG151\" FlightKey=\"DELBOMSG151SG20130910\" FromCache=\"true\" ReturnOnly=\"true\" SupplierCode=\"SGAPINavitaire\" SupplierSystem=\"SGNAV\" UniqueIdentifier=\"1.0\"> <FlightSegment ArrivalDateTime=\"2013-09-10T09:20:00\" DateWindow=\"\" DepartureDateTime=\"2013-09-10T06:00:00\" Duration=\"03:20:00\" FlightNumber=\"151\" LTD=\"SGAPINavitaire\" NumberInParty=\"DELBOM\" ResBookDesigCode=\"X\" Status=\"\" StopQuantity=\"\" ValidConnectionInd=\"SGAPINavitaire\"> <BookingClassAvail FareType=\"\" ResBookDesigCode=\"X\"/> <DepartureAirport AirPortName=\"Indira Gandhi\" CityName=\"New Delhi\" CodeContext=\"IATA\" CountryCode=\"IN\" LocationCode=\"DEL\" Terminal=\"1B\" TerminalID=\"1B\"/> <ArrivalAirport AirPortName=\"Chatrapati Shivaji\" CityName=\"Mumbai\" CodeContext=\"IATA\" CountryCode=\"IN\" LocationCode=\"BOM\" Terminal=\"1B\" TerminalID=\"1B\"/> <Equipment AirEquipType=\"737\"/> <MarketingAirline Code=\"SG\" CodeContext=\"IATA\" MatrixCode=\"SG\" Name=\"Spicejet\" YTAirlineCode=\"SG\"/> <StopOver AirPortName=\"Surat\" CityName=\"Surat\" LocationCode=\"STV\"/> <Comment Cancelled=\"\" Delay=\"\" Ontime=\"\" Stars=\"\"/> </FlightSegment> <FormData> <sellKey>SG~ 151~ ~~DEL~09/10/2013 06:00~BOM~09/10/2013 09:20~STV</sellKey> <fareKey>0~X~~XD~0307~~4~X</fareKey> <session>LknU8ySDxF0=|pItFYbHGdLDnINQnCe2TcwLB4RJjs/4NPInVN9WmoIUrAdAQGzf1CMyIwi7zROlsFyBUwHF9FUAy/REk0buQmzCYdz4kCemTQi0fQ2eVZNUjU6e00U2M6CIDnFmN/9Y5cvy5kTLDf1s=</session> <FareDifference> <TotalFare ADT=\"1\" BaseFare=\"610-ADT 1\" CHD=\"0\" Cabin=\"Economy\" HostName=\"railserver\" INF=\"0\" Rbd=\" - X - \" Tax=\"PSF-146YQ-2950UDF-664YR-178.7WC-0TF-50OC-0\">4598.7</TotalFare> </FareDifference> <TicketingInfo DeliveryMethod=\"EMAIL\" TicketTimeLimit=\"\" TicketType=\"eTicket\" TicketingStatus=\"\" eTicketNumber=\"\"> <TicketAdvisory Language=\"English\"/> </TicketingInfo> <AgentMarkup> <Airlines> <Airline> <Code>6E</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>9W</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>0.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>G8</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>IT</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>S2</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>SG</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2000.0000</Surcharge> </Airline> <Others> <Code/> <GDS>1</GDS> <WholeSalerFees>0</WholeSalerFees> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> </Others> </Airlines> </AgentMarkup> <FormData> <sellKey>SG~ 151~ ~~DEL~09/10/2013 06:00~BOM~09/10/2013 09:20~STV</sellKey> <fareKey>0~X~~XD~0307~~4~X</fareKey> <session>LknU8ySDxF0=|pItFYbHGdLDnINQnCe2TcwLB4RJjs/4NPInVN9WmoIUrAdAQGzf1CMyIwi7zROlsFyBUwHF9FUAy/REk0buQmzCYdz4kCemTQi0fQ2eVZNUjU6e00U2M6CIDnFmN/9Y5cvy5kTLDf1s=</session> <FareDifference> <TotalFare ADT=\"1\" BaseFare=\"610-ADT 1\" CHD=\"0\" Cabin=\"Economy\" HostName=\"railserver\" INF=\"0\" Rbd=\" - X - \" Tax=\"PSF-146YQ-2950UDF-664YR-178.7WC-0TF-50OC-0\">4598.7</TotalFare> </FareDifference> <TicketingInfo DeliveryMethod=\"EMAIL\" TicketTimeLimit=\"\" TicketType=\"eTicket\" TicketingStatus=\"\" eTicketNumber=\"\"> <TicketAdvisory Language=\"English\"/> </TicketingInfo> <AgentMarkup> <Airlines> <Airline> <Code>6E</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>9W</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>0.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>G8</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>IT</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>S2</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>SG</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2000.0000</Surcharge> </Airline> <Others> <Code/> <GDS>1</GDS> <WholeSalerFees>0</WholeSalerFees> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> </Others> </Airlines> </AgentMarkup> </FormData> </FormData> <FormData/> <FormData/> </OriginDestinationOption> <OriginDestinationOption Duration=\"03:20:00\" FareBasisCode=\"XD\" FlightID=\"DELBOMSG151BOMDELSG152\" FlightKey=\"BOMDELSG152SG20130915\" FromCache=\"true\" ReturnOnly=\"true\" SupplierCode=\"SGAPINavitaire\" SupplierSystem=\"SGNAV\" UniqueIdentifier=\"1.0\"> <FlightSegment ArrivalDateTime=\"2013-09-15T22:45:00\" DateWindow=\"\" DepartureDateTime=\"2013-09-15T19:20:00\" Duration=\"03:25:00\" FlightNumber=\"152\" LTD=\"SGAPINavitaire\" NumberInParty=\"BOMDEL\" ResBookDesigCode=\"X\" Status=\"\" StopQuantity=\"\" ValidConnectionInd=\"SGAPINavitaire\"> <BookingClassAvail FareType=\"\" ResBookDesigCode=\"X\"/> <DepartureAirport AirPortName=\"Chatrapati Shivaji\" CityName=\"Mumbai\" CodeContext=\"IATA\" CountryCode=\"IN\" LocationCode=\"BOM\" Terminal=\"1B\" TerminalID=\"1B\"/> <ArrivalAirport AirPortName=\"Indira Gandhi\" CityName=\"New Delhi\" CodeContext=\"IATA\" CountryCode=\"IN\" LocationCode=\"DEL\" Terminal=\"1B\" TerminalID=\"1B\"/> <Equipment AirEquipType=\"737\"/> <MarketingAirline Code=\"SG\" CodeContext=\"IATA\" MatrixCode=\"SG\" Name=\"Spicejet\" YTAirlineCode=\"SG\"/> <StopOver AirPortName=\"Surat\" CityName=\"Surat\" LocationCode=\"STV\"/> <Comment Cancelled=\"\" Delay=\"\" Ontime=\"\" Stars=\"\"/> </FlightSegment> <FormData> <sellKey>SG~ 152~ ~~BOM~09/15/2013 19:20~DEL~09/15/2013 22:45~STV</sellKey> <fareKey>0~X~~XD~0307~~4~X</fareKey> <session>LknU8ySDxF0=|pItFYbHGdLDnINQnCe2TcwLB4RJjs/4NPInVN9WmoIUrAdAQGzf1CMyIwi7zROlsFyBUwHF9FUAy/REk0buQmzCYdz4kCemTQi0fQ2eVZNUjU6e00U2M6CIDnFmN/9Y5cvy5kTLDf1s=</session> <FareDifference> <TotalFare ADT=\"1\" BaseFare=\"610-ADT 1\" CHD=\"0\" Cabin=\"Economy\" HostName=\"railserver\" INF=\"0\" Rbd=\" - X - \" Tax=\"PSF-147YQ-2950UDF-421YR-178.7WC-466TF-50OC-0\">4822.7</TotalFare> </FareDifference> <TicketingInfo DeliveryMethod=\"EMAIL\" TicketTimeLimit=\"\" TicketType=\"eTicket\" TicketingStatus=\"\" eTicketNumber=\"\"> <TicketAdvisory Language=\"English\"/> </TicketingInfo> <AgentMarkup> <Airlines> <Airline> <Code>6E</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>9W</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>0.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>G8</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2700.0000</Surcharge> </Airline> <Airline> <Code>IT</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>S2</Code> <GDS>1</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>3.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>1100.0000</Surcharge> </Airline> <Airline> <Code>SG</Code> <GDS>0</GDS> <WholeSalerFees>10.0000</WholeSalerFees> <WholeSalerFeesType>F</WholeSalerFeesType> <CommissionType>M</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> <Type>P</Type> <DiscountPLB>2.0000</DiscountPLB> <DiscountPLBType>P</DiscountPLBType> <DiscountPLBOn>B</DiscountPLBOn> <Surcharge>2000.0000</Surcharge> </Airline> <Others> <Code/> <GDS>1</GDS> <WholeSalerFees>0</WholeSalerFees> <CommissionType>C</CommissionType> <ApplyOn>B</ApplyOn> <Amount>0.0000</Amount> </Others> </Airlines> </AgentMarkup> </FormData> <FormData/> </OriginDestinationOption> </OriginDestinationOptions> </AirItinerary> <AirItineraryPricingInfo UniqueIdentifier=\"1.0\"> <ItinTotalFare> <BaseFare Amount=\"1220\" CurrencyCode=\"INR\"/> <Taxes> <Tax Amount=\"293\" CurrencyCode=\"INR\" Description=\"Passenger Service Fee\" FareBasisCode=\"X\" TaxCode=\"PSF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"5900\" CurrencyCode=\"INR\" Description=\"Fuel Surcharge\" FareBasisCode=\"X\" TaxCode=\"YQ\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"1085\" CurrencyCode=\"INR\" Description=\"User Development Fee\" FareBasisCode=\"X\" TaxCode=\"UDF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"357.4\" CurrencyCode=\"INR\" Description=\"Congestion Fee\" FareBasisCode=\"X\" TaxCode=\"YR\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"0\" CurrencyCode=\"INR\" Description=\"Web transaction Charges\" FareBasisCode=\"X\" TaxCode=\"WC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"100\" CurrencyCode=\"INR\" Description=\"Transaction Fee\" FareBasisCode=\"X\" TaxCode=\"TF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"466\" CurrencyCode=\"INR\" Description=\"Other Charges\" FareBasisCode=\"X\" TaxCode=\"OC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"0\" TaxCode=\"B2BMarkup\"/> </Taxes> <TotalFare Amount=\"9424.33\" CurrencyCode=\"INR\"/> <Fees> <Fee Amount=\"0\"/> <Fee Amount=\"0\" FeeCode=\"YatraLCCSurcharge\"/> </Fees> <ServiceTax Amount=\"2.93\" Currency=\"INR\" Description=\"Service Tax\" ServiceTaxCode=\"ST\" ServiceTaxRate=\"0\"/> <FareBaggageAllowance AirlineCode=\"SG\" PaxType=\"ADT\" UnitOfMeasureCode=\"kgs\" UnitOfMeasureQuantity=\"20\"/> <Markups> <Markup Amount=\"10\" CurrencyCode=\"INR\" Description=\"10.0 wholesalerfees for client 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"W\" Type=\"M\"/> <Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0000% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"M\"/> <Markup Amount=\"24.4\" CurrencyCode=\"INR\" Description=\"2.0% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> </Markups> </ItinTotalFare> <PTC_FareBreakdowns> <PTC_FareBreakdown> <PassengerTypeQuantity Code=\"ADT\" CodeContext=\"IATA\" Quantity=\"1\"/> <PassengerFare> <BaseFare Amount=\"1220\" CurrencyCode=\"INR\"/> <Taxes> <Tax Amount=\"2950\" CurrencyCode=\"INR\" Description=\"Fuel Surcharge\" FareBasisCode=\"X\" TaxCode=\"YQ\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"50\" CurrencyCode=\"INR\" Description=\"User Development Fee\" FareBasisCode=\"X\" TaxCode=\"TF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"146\" CurrencyCode=\"INR\" Description=\"Passenger Service Fee\" FareBasisCode=\"X\" TaxCode=\"PSF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"664\" CurrencyCode=\"INR\" Description=\"Transaction Fee\" FareBasisCode=\"X\" TaxCode=\"UDF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"178.7\" CurrencyCode=\"INR\" Description=\"Congestion Fee\" FareBasisCode=\"X\" TaxCode=\"YR\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"50\" CurrencyCode=\"INR\" Description=\"User Development Fee\" FareBasisCode=\"X\" TaxCode=\"TF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"2950\" CurrencyCode=\"INR\" Description=\"Fuel Surcharge\" FareBasisCode=\"X\" TaxCode=\"YQ\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"466\" CurrencyCode=\"INR\" Description=\"Other Charges\" FareBasisCode=\"X\" TaxCode=\"OC\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"147\" CurrencyCode=\"INR\" Description=\"Passenger Service Fee\" FareBasisCode=\"X\" TaxCode=\"PSF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"421\" CurrencyCode=\"INR\" Description=\"Transaction Fee\" FareBasisCode=\"X\" TaxCode=\"UDF\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"178.7\" CurrencyCode=\"INR\" Description=\"Congestion Fee\" FareBasisCode=\"X\" TaxCode=\"YR\" TaxGroupCode=\"SF\" TaxGroupName=\"Taxes &amp; Fees\"/> <Tax Amount=\"0\" TaxCode=\"B2BMarkup\"/> </Taxes> <TotalFare Amount=\"9421.4\" CurrencyCode=\"INR\"/> <ServiceTax Amount=\"0\" Currency=\"INR\" Description=\"Service Tax\" ServiceTaxCode=\"ST\" ServiceTaxRate=\"0\"/> <Markups> <Markup Amount=\"10.0000\" CurrencyCode=\"INR\" Description=\"10.0 wholesalerfees for client 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"W\" Type=\"M\"/> <Markup Amount=\"0\" CurrencyCode=\"INR\" Description=\"0.0% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"M\"/> <Markup Amount=\"24.4\" CurrencyCode=\"INR\" Description=\"2.0% on base fare for client - 1152\" MarkupCode=\"DisplayMarkup\" MarkupType=\"M\" Type=\"P\"/> </Markups> </PassengerFare> <FareBasisCodes> <FareBasisCode BaseFareRefundperPAX=\"470\" CancelAmtValue=\"750\" FareType=\"Refundable\" RefundType=\"A\" ResBookDesigCode=\"\" YatraOfflineFee=\"310\" YatraOnlineFee=\"200\">XXXXX</FareBasisCode> <FareBasisCode BaseFareRefundperPAX=\"470\" CancelAmtValue=\"750\" FareType=\"Refundable\" RefundType=\"A\" ResBookDesigCode=\"\" YatraOfflineFee=\"310\" YatraOnlineFee=\"200\">XXXXX</FareBasisCode> </FareBasisCodes> <RefundInfo BaseFareRefundperPAX=\"-280\" CancelAmtValue=\"1500\" FareBasisUsed=\"-\" RefundType=\"A\" ResBookDesigCode=\"-\" YatraOfflineFee=\"310\" YatraOnlineFee=\"200\"/> </PTC_FareBreakdown> </PTC_FareBreakdowns> </AirItineraryPricingInfo> <Notes>SG</Notes> </PricedItinerary> </PricedItineraries> </OTA_AirPriceRS> </soapenv:Body> </soapenv:Envelope>";

        ////ParseAirPrice(xmlprice);
        //try
        //{
        //    string fre = "{\"Currency\":\"INR\",\"BaseFare\":3676,\"Tax\":2733.0,\"YQTax\":400.0,\"AdditionalTxnFee\":0.0,\"AdditionalTxnFeeOfrd\":0.0,\"AdditionalTxnFeePub\":0.0,\"OtherCharges\":64.66,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":14.66},{\"key\":\"CONVENIENCECHARGE\",\"value\":0.0},{\"key\":\"OTHERCHARGE\",\"value\":50.0}],\"Discount\":0.0,\"PublishedFare\":6473.66,\"CommissionEarned\":101.09,\"PLBEarned\":0.0,\"IncentiveEarned\":0.0,\"OfferedFare\":6372.57,\"TdsOnCommission\":20.22,\"TdsOnPLB\":0.0,\"TdsOnIncentive\":0.0,\"ServiceFee\":0.0}";
        //    Fare fareMain = JsonConvert.DeserializeObject<STD.BAL.TBO.Fare>(fre);
        //    string fg = "[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":3676.0,\"Tax\":2733.0,\"YQTax\":400.0,\"AdditionalTxnFee\":0.0,\"AdditionalTxnFeeOfrd\":0.0,\"AdditionalTxnFeePub\":0.0}]";
        //    List<FareBreakdown> fareBrkup = JsonConvert.DeserializeObject<List<FareBreakdown>>(fg);
        //}catch (Exception ex)
        //{ 

        //}



        // ArrayList TktNoArray = new ArrayList();

        // string numder = DateTime.Now.ToString("dMyyHms");


        // FlightResult items = new FlightResult();
        // string strJsonResponse = "{\"Response\":{\"ResponseStatus\":1,\"Error\":{\"ErrorCode\":0,\"ErrorMessage\":\"\"},\"TraceId\":\"ab09ed40-5b97-4198-8282-05f02e46a3f5\",\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Results\":[[{\"ResultIndex\":\"OB1\",\"Source\":10,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":null,\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":753,\"Tax\":464.03,\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":1232.03,\"CommissionEarned\":36.60,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":1195.43,\"TdsOnCommission\":1.83,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":753,\"Tax\":464.03,\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"364\",\"FareClass\":\"J\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":9,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T23:55:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1B\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-18T01:15:00\"},\"Duration\":80,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-18T01:15:00\",\"StopPointDepartureTime\":\"2016-08-17T23:55:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"JGOPROMO\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB2\",\"Source\":10,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":null,\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":857,\"Tax\":470.03,\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":1342.03,\"CommissionEarned\":41.65,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":1300.38,\"TdsOnCommission\":2.08,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":857,\"Tax\":470.03,\"YQTax\":0,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"370\",\"FareClass\":\"L\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":4,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T21:50:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1A\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-17T23:15:00\"},\"Duration\":85,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-17T23:15:00\",\"StopPointDepartureTime\":\"2016-08-17T21:50:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"LGOPROMO\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB5\",\"Source\":32,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":\"G8 Pvt Fare \",\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":1415,\"CommissionEarned\":0.00,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":1415.00,\"TdsOnCommission\":0.00,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"364\",\"FareClass\":\"G1\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":9,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T23:55:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1B\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-18T01:15:00\"},\"Duration\":80,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-18T01:15:00\",\"StopPointDepartureTime\":\"2016-08-17T23:55:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"G1TRIP\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB4\",\"Source\":32,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":\"G8 Pvt Fare \",\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":1415,\"CommissionEarned\":0.00,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":1415.00,\"TdsOnCommission\":0.00,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"370\",\"FareClass\":\"G1\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":5,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T21:50:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1A\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-17T23:15:00\"},\"Duration\":85,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-17T23:15:00\",\"StopPointDepartureTime\":\"2016-08-17T21:50:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"G1TRIP\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB3\",\"Source\":32,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":\"G8 Pvt Fare \",\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":1415,\"CommissionEarned\":0.00,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":1415.00,\"TdsOnCommission\":0.00,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":126,\"Tax\":1274,\"YQTax\":800,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"368\",\"FareClass\":\"G1\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":3,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T09:25:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1A\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-17T10:35:00\"},\"Duration\":70,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-17T10:35:00\",\"StopPointDepartureTime\":\"2016-08-17T09:25:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"G1TRIP\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB6\",\"Source\":26,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":\"Special Non Commissionable Fare \",\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":904,\"Tax\":1349.03,\"YQTax\":826,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":0,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":0}],\"Discount\":0,\"PublishedFare\":2253.03,\"CommissionEarned\":0.00,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":2253.03,\"TdsOnCommission\":0.00,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":904,\"Tax\":1349.03,\"YQTax\":826,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"368\",\"FareClass\":\"Y\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":9,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T09:25:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1A\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-17T10:35:00\"},\"Duration\":70,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-17T10:35:00\",\"StopPointDepartureTime\":\"2016-08-17T09:25:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"YGOSPLIP\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"},{\"ResultIndex\":\"OB7\",\"Source\":10,\"IsLCC\":true,\"IsRefundable\":true,\"AirlineRemark\":null,\"Fare\":{\"Currency\":\"INR\",\"BaseFare\":1016,\"Tax\":1355.03,\"YQTax\":826,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0,\"OtherCharges\":15.00,\"ChargeBU\":[{\"key\":\"TBOMARKUP\",\"value\":0},{\"key\":\"CONVENIENCECHARGE\",\"value\":0},{\"key\":\"OTHERCHARGE\",\"value\":15.00}],\"Discount\":0,\"PublishedFare\":2386.03,\"CommissionEarned\":89.52,\"PLBEarned\":0.00,\"IncentiveEarned\":0.00,\"OfferedFare\":2296.51,\"TdsOnCommission\":4.48,\"TdsOnPLB\":0.00,\"TdsOnIncentive\":0.00,\"ServiceFee\":0,\"TotalBaggageCharges\":0,\"TotalMealCharges\":0,\"TotalSeatCharges\":0},\"FareBreakdown\":[{\"Currency\":\"INR\",\"PassengerType\":1,\"PassengerCount\":1,\"BaseFare\":1016,\"Tax\":1355.03,\"YQTax\":826,\"AdditionalTxnFeeOfrd\":0,\"AdditionalTxnFeePub\":0}],\"Segments\":[[{\"TripIndicator\":1,\"SegmentIndicator\":1,\"Airline\":{\"AirlineCode\":\"G8\",\"AirlineName\":\"GoAir\",\"FlightNumber\":\"368\",\"FareClass\":\"Y\",\"OperatingCarrier\":\"\"},\"NoOfSeatAvailable\":9,\"Origin\":{\"Airport\":{\"AirportCode\":\"AMD\",\"AirportName\":\"Ahmedabad\",\"Terminal\":\"\",\"CityCode\":\"AMD\",\"CityName\":\"Ahmedabad\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"DepTime\":\"2016-08-17T09:25:00\"},\"Destination\":{\"Airport\":{\"AirportCode\":\"BOM\",\"AirportName\":\"Mumbai\",\"Terminal\":\"1A\",\"CityCode\":\"BOM\",\"CityName\":\"Mumbai\",\"CountryCode\":\"IN\",\"CountryName\":\"India\"},\"ArrTime\":\"2016-08-17T10:35:00\"},\"Duration\":70,\"GroundTime\":0,\"Mile\":0,\"StopOver\":false,\"StopPoint\":\"\",\"StopPointArrivalTime\":\"2016-08-17T10:35:00\",\"StopPointDepartureTime\":\"2016-08-17T09:25:00\",\"Craft\":\"320\",\"Remark\":null,\"IsETicketEligible\":true,\"FlightStatus\":\"Confirmed\",\"Status\":\"\"}]],\"LastTicketDate\":\"2016-06-30T17:51:07\",\"TicketAdvisory\":null,\"FareRules\":[{\"Origin\":\"AMD\",\"Destination\":\"BOM\",\"Airline\":\"G8\",\"FareBasisCode\":\"YGOSAVE\",\"FareRuleDetail\":\"\",\"FareRestriction\":\"\"}],\"AirlineCode\":\"G8\",\"ValidatingAirline\":\"G8\",\"BaggageAllowance\":\"15kg\"}]]}}";
        // try
        // {
        //     //strJsonResponse = TBOAuthProcess.GetResponse(ServiceUrl.SearchUrl, strJsonData);
        //     // Response items = new Response();

        //     // items = JsonConvert.DeserializeObject<FlightResult>(strJsonResponse);

        //     items = JsonConvert.DeserializeObject<FlightResult>(strJsonResponse, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
        // }
        // catch (Exception ex1)
        // {

        //     strJsonResponse = strJsonResponse + "error: " + ex1.Message + "stack:" + ex1.StackTrace.ToString();// +"" + Convert.ToString(ex1.InnerException.Message);
        // }


        // string AirlineRemark ="6E special non commissionable fare";

        //if( AirlineRemark.Trim().Contains("Special Non Commissionable Fare"))
        //{


        //}

        // string PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?> <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'> <soapenv:Body> <SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'> <SubmitXmlOnSessionResult> <PNRBFManagement_13 xmlns=''> <PNRBFRetrieve> <Control> <KLRCnt>40</KLRCnt> <KlrAry> <Klr> <ID>BP10</ID> <NumOccur>6</NumOccur> </Klr> <Klr> <ID>BP12</ID> <NumOccur>6</NumOccur> </Klr> <Klr> <ID>IT01</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP21</ID> <NumOccur>12</NumOccur> </Klr> <Klr> <ID>BP22</ID> <NumOccur>6</NumOccur> </Klr> <Klr> <ID>BP20</ID> <NumOccur>0</NumOccur> </Klr> <Klr> <ID>BP19</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP16</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>DPP9</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP32</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP26</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP48</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP27</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP08</ID> <NumOccur>1</NumOccur> </Klr> <Klr> <ID>BP40</ID> <NumOccur>1</NumOccur> </Klr> </KlrAry> </Control> <LNameInfo> <LNameNum>1</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>MUNDADA</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>1</AbsNameNum> <FName>RAJKUMARMR</FName> </FNameInfo> <LNameInfo> <LNameNum>2</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>PILLE</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>2</AbsNameNum> <FName>GAJANANMR</FName> </FNameInfo> <LNameInfo> <LNameNum>3</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>PAWAR</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>3</AbsNameNum> <FName>DEVIKANTMR</FName> </FNameInfo> <LNameInfo> <LNameNum>4</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>VERULKAR</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>4</AbsNameNum> <FName>SANTOSHMR</FName> </FNameInfo> <LNameInfo> <LNameNum>5</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>AGRAWAL</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>5</AbsNameNum> <FName>VISHALMR</FName> </FNameInfo> <LNameInfo> <LNameNum>6</LNameNum> <NumPsgrs>1</NumPsgrs> <NameType/> <LName>CHOUDHARY</LName> </LNameInfo> <FNameInfo> <PsgrNum>1</PsgrNum> <AbsNameNum>6</AbsNameNum> <FName>SACHINMR</FName> </FNameInfo> <AirSeg> <SegNum>1</SegNum> <Status>HK</Status> <Dt>20160713</Dt> <DayChg>00</DayChg> <AirV>9W</AirV> <NumPsgrs>6</NumPsgrs> <StartAirp>DEL</StartAirp> <EndAirp>SXR</EndAirp> <StartTm>1220</StartTm> <EndTm>1350</EndTm> <BIC>V</BIC> <FltNum>603</FltNum> <OpSuf/> <COG>N</COG> <TklessInd>Y</TklessInd> <ConxInd>N</ConxInd> <FltFlownInd>N</FltFlownInd> <MarriageNum/> <SellType>O</SellType> <StopoverIgnoreInd/> <TDSValidateInd>N</TDSValidateInd> <NonBillingInd/> <PrevStatusCode>NN</PrevStatusCode> <ScheduleValidationInd/> </AirSeg> <ProgramaticSSR> <GFAXNum>1</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>1</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>1</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669649C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>2</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>1</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>1</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSR> <GFAXNum>3</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>2</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>2</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669650C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>4</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>2</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>2</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSR> <GFAXNum>5</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>3</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>3</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669651C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>6</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>3</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>3</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSR> <GFAXNum>7</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>4</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>4</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669652C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>8</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>4</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>4</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSR> <GFAXNum>9</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>5</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>5</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669653C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>10</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>5</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>5</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSR> <GFAXNum>11</GFAXNum> <SSRCode>TKNE</SSRCode> <Status>HK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>6</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>6</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <ProgramaticSSRText> <Text>5892211669654C1</Text> </ProgramaticSSRText> <ProgramaticSSR> <GFAXNum>12</GFAXNum> <SSRCode>VGML</SSRCode> <Status>KK</Status> <SegNum>1</SegNum> <AppliesToAry> <AppliesTo> <LNameNum>6</LNameNum> <PsgrNum>1</PsgrNum> <AbsNameNum>6</AbsNameNum> </AppliesTo> </AppliesToAry> </ProgramaticSSR> <OSI> <GFAXNum>1</GFAXNum> <OSIV>YY</OSIV> <OSIMsg>MOBILE-9860634340</OSIMsg> </OSI> <PhoneInfo> <PhoneFldNum>1</PhoneFldNum> <Pt>DEL</Pt> <Type>A</Type> <Phone>011 40878787</Phone> </PhoneInfo> <OtherFOP> <ID>1</ID> <Type>2</Type> <Currency/> <Amt/> <AdditionalInfoAry></AdditionalInfoAry> </OtherFOP> <TkArrangement> <Text>BOM 19JUN1014Z WS AG</Text> </TkArrangement> <GenRmkInfo> <GenRmkNum>1</GenRmkNum> <CreationDt>20160619</CreationDt> <CreationTm>1014</CreationTm> <GenlRmkQual/> <GenRmk>ON-LINE BOOKING - D000045960</GenRmk> </GenRmkInfo> <GenPNRInfo> <FileAddr>C3966833</FileAddr> <CodeCheck>E7</CodeCheck> <RecLoc>QDVLF2</RecLoc> <Ver>5</Ver> <OwningCRS>1G</OwningCRS> <OwningAgncyName>ITZ CASH CARD LIMITED</OwningAgncyName> <OwningAgncyPCC>57RS</OwningAgncyPCC> <CreationDt>20160619</CreationDt> <CreatingAgntSignOn>57RSGWS</CreatingAgntSignOn> <CreatingAgntDuty>AG</CreatingAgntDuty> <CreatingAgncyIATANum>14357770</CreatingAgncyIATANum> <OrigBkLocn>BOMOU</OrigBkLocn> <SATONum/> <PTAInd>N</PTAInd> <InUseInd>N</InUseInd> <SimultaneousUpdInd/> <BorrowedInd>N</BorrowedInd> <GlobInd>N</GlobInd> <ReadOnlyInd>N</ReadOnlyInd> <FareDataExistsInd>Y</FareDataExistsInd> <PastDtQuickInd>N</PastDtQuickInd> <CurAgncyPCC>55QN</CurAgncyPCC> <QInd>Y</QInd> <TkNumExistInd>Y</TkNumExistInd> <IMUdataexists>N</IMUdataexists> <ETkDataExistInd>Y</ETkDataExistInd> <CurDtStamp>20160619</CurDtStamp> <CurTmStamp>101422</CurTmStamp> <CurAgntSONID>55QNGWS</CurAgntSONID> <TravInsuranceInd/> <PNRBFTicketedInd>Y</PNRBFTicketedInd> <ZeppelinAgncyInd>N</ZeppelinAgncyInd> <AgncyAutoServiceInd>N</AgncyAutoServiceInd> <AgncyAutoNotifyInd>N</AgncyAutoNotifyInd> <ZeppelinPNRInd>N</ZeppelinPNRInd> <PNRAutoServiceInd>N</PNRAutoServiceInd> <PNRAutoNotifyInd>N</PNRAutoNotifyInd> <SuperPNRInd>N</SuperPNRInd> <PNRBFPurgeDt>20160715</PNRBFPurgeDt> <PNRBFChangeInd>N</PNRBFChangeInd> <MCODataExists>N</MCODataExists> <OrigRcvdField>PASSENGER</OrigRcvdField> <IntContExists>N</IntContExists> </GenPNRInfo> <Email> <ItemNum>1</ItemNum> <Type>T</Type> <Data>raj.bajaj999@yahoo.com</Data> </Email> <VndRecLocs> <RecLocInfoAry> <RecLocInfo> <TmStamp>1014</TmStamp> <DtStamp>20160619</DtStamp> <Vnd>9W</Vnd> <RecLoc>SUFALY</RecLoc> </RecLocInfo> </RecLocInfoAry> </VndRecLocs> </PNRBFRetrieve> </PNRBFManagement_13> </SubmitXmlOnSessionResult> </SubmitXmlOnSessionResponse> </soapenv:Body> </soapenv:Envelope>";

        string PNBF2Res1 = @"<?xml version='1.0' encoding='UTF-8'?>
<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <soapenv:Body>
    <SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'>
      <SubmitXmlOnSessionResult>
        <PNRBFManagement_13 xmlns=''>
          <PNRBFRetrieve>
            <Control>
              <KLRCnt>39</KLRCnt>
              <KlrAry>
                <Klr>
                  <ID>BP10</ID>
                  <NumOccur>4</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP12</ID>
                  <NumOccur>4</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP13</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>IT01</ID>
                  <NumOccur>2</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP21</ID>
                  <NumOccur>8</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP22</ID>
                  <NumOccur>8</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP20</ID>
                  <NumOccur>2</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP19</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP16</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP32</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP28</ID>
                  <NumOccur>5</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP27</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
                <Klr>
                  <ID>BP08</ID>
                  <NumOccur>1</NumOccur>
                </Klr>
              </KlrAry>
            </Control>
            <LNameInfo>
              <LNameNum>1</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>AHUJA</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>1</AbsNameNum>
              <FName>NANCYMRS</FName>
            </FNameInfo>
            <LNameInfo>
              <LNameNum>2</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>AHUJA</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>2</AbsNameNum>
              <FName>RAKSHITMR</FName>
            </FNameInfo>
            <LNameInfo>
              <LNameNum>3</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>AHUJA</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>3</AbsNameNum>
              <FName>ARNAVMR</FName>
            </FNameInfo>
            <LNameInfo>
              <LNameNum>4</LNameNum>
              <NumPsgrs>1</NumPsgrs>
              <NameType/>
              <LName>AHUJA</LName>
            </LNameInfo>
            <FNameInfo>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>4</AbsNameNum>
              <FName>RAJVEERMSTR</FName>
            </FNameInfo>
            <NameRmkInfo>
              <LNameNum>4</LNameNum>
              <PsgrNum>1</PsgrNum>
              <AbsNameNum>4</AbsNameNum>
              <NameRmk>P-C06</NameRmk>
            </NameRmkInfo>
            <AirSeg>
              <SegNum>1</SegNum>
              <Status>HK</Status>
              <Dt>20180612</Dt>
              <DayChg>00</DayChg>
              <AirV>SU</AirV>
              <NumPsgrs>4</NumPsgrs>
              <StartAirp>DEL</StartAirp>
              <EndAirp>SVO</EndAirp>
              <StartTm>500</StartTm>
              <EndTm>855</EndTm>
              <BIC>T</BIC>
              <FltNum>233</FltNum>
              <OpSuf/>
              <COG>N</COG>
              <TklessInd>Y</TklessInd>
              <ConxInd>N</ConxInd>
              <FltFlownInd>Y</FltFlownInd>
              <MarriageNum>01</MarriageNum>
              <SellType>O</SellType>
              <StopoverIgnoreInd/>
              <TDSValidateInd>N</TDSValidateInd>
              <NonBillingInd/>
              <PrevStatusCode>NN</PrevStatusCode>
              <ScheduleValidationInd/>
            </AirSeg>
            <AirSeg>
              <SegNum>2</SegNum>
              <Status>HK</Status>
              <Dt>20180612</Dt>
              <DayChg>00</DayChg>
              <AirV>SU</AirV>
              <NumPsgrs>4</NumPsgrs>
              <StartAirp>SVO</StartAirp>
              <EndAirp>BUD</EndAirp>
              <StartTm>1020</StartTm>
              <EndTm>1205</EndTm>
              <BIC>T</BIC>
              <FltNum>2030</FltNum>
              <OpSuf/>
              <COG>N</COG>
              <TklessInd>Y</TklessInd>
              <ConxInd>Y</ConxInd>
              <FltFlownInd>Y</FltFlownInd>
              <MarriageNum>01</MarriageNum>
              <SellType>O</SellType>
              <StopoverIgnoreInd/>
              <TDSValidateInd>N</TDSValidateInd>
              <NonBillingInd/>
              <PrevStatusCode>NN</PrevStatusCode>
              <ScheduleValidationInd/>
            </AirSeg>
            <ProgramaticSSR>
              <GFAXNum>1</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>1</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>1</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342842C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>2</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>2</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>2</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342843C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>3</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>3</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>3</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342844C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>4</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>1</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>4</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>4</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342845C1</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>5</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>2</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>1</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>1</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342842C2</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>6</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>2</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>2</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>2</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342843C2</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>7</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>2</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>3</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>3</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342844C2</Text>
            </ProgramaticSSRText>
            <ProgramaticSSR>
              <GFAXNum>8</GFAXNum>
              <SSRCode>TKNE</SSRCode>
              <Status>HK</Status>
              <SegNum>2</SegNum>
              <AppliesToAry>
                <AppliesTo>
                  <LNameNum>4</LNameNum>
                  <PsgrNum>1</PsgrNum>
                  <AbsNameNum>4</AbsNameNum>
                </AppliesTo>
              </AppliesToAry>
            </ProgramaticSSR>
            <ProgramaticSSRText>
              <Text>5552680342845C2</Text>
            </ProgramaticSSRText>
            <NonProgramaticSSR>
              <GFAXNum>9</GFAXNum>
              <SSRCode>CHLD</SSRCode>
              <Vnd>UK</Vnd>
              <Status>HK</Status>
              <NumRequired>1</NumRequired>
              <SSRText>/-1AHUJA/RAJVEERMSTR</SSRText>
            </NonProgramaticSSR>
            <NonProgramaticSSR>
              <GFAXNum>10</GFAXNum>
              <SSRCode>CHLD</SSRCode>
              <Vnd>SU</Vnd>
              <Status>HK</Status>
              <NumRequired>1</NumRequired>
              <SSRText>/-1AHUJA/RAJVEERMSTR</SSRText>
            </NonProgramaticSSR>
            <OSI>
              <GFAXNum>1</GFAXNum>
              <OSIV>1G</OSIV>
              <OSIMsg>SNC RLOC SU UGPXAD</OSIMsg>
            </OSI>
            <PhoneInfo>
              <PhoneFldNum>1</PhoneFldNum>
              <Pt>DEL</Pt>
              <Type>A</Type>
              <Phone>RWT 01147677777 REF ROHIT</Phone>
            </PhoneInfo>
            <TkArrangement>
              <Text>QSB 08JUN0735Z 02 AG</Text>
            </TkArrangement>
            <VndRmk>
              <RmkNum>1</RmkNum>
              <TmStamp>640</TmStamp>
              <DtStamp>20180608</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>SU</Vnd>
              <Rmk>ATTN TKT MUST BE COMPLETED WITHIN 24 HOURS AFTER RES</Rmk>
            </VndRmk>
            <VndRmk>
              <RmkNum>2</RmkNum>
              <TmStamp>740</TmStamp>
              <DtStamp>20180608</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>SU</Vnd>
              <Rmk>PLS ADV CORRECT PAX MOBILE PHONE</Rmk>
            </VndRmk>
            <VndRmk>
              <RmkNum>3</RmkNum>
              <TmStamp>848</TmStamp>
              <DtStamp>20180608</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>SU</Vnd>
              <Rmk>PLEASE CHECK AND VERIFY PAX DOCS DATA/NUMBER</Rmk>
            </VndRmk>
            <VndRmk>
              <RmkNum>4</RmkNum>
              <TmStamp>1001</TmStamp>
              <DtStamp>20180611</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>SU</Vnd>
              <Rmk>PLEASE CHECK AND VERIFY PAX DOCS DATA/NUMBER</Rmk>
            </VndRmk>
            <VndRmk>
              <RmkNum>5</RmkNum>
              <TmStamp>2009</TmStamp>
              <DtStamp>20180607</DtStamp>
              <RmkType>I</RmkType>
              <VType>A</VType>
              <Vnd>UK</Vnd>
              <Rmk>ADTK1GTOUK BY 08JUN 1400 OTHERWISE WILL BE XLD</Rmk>
            </VndRmk>
            <GenPNRInfo>
              <FileAddr>E41029B7</FileAddr>
              <CodeCheck>47</CodeCheck>
              <RecLoc>725MBE</RecLoc>
              <Ver>25</Ver>
              <OwningCRS>1G</OwningCRS>
              <OwningAgncyName>RWT COM</OwningAgncyName>
              <OwningAgncyPCC>000V</OwningAgncyPCC>
              <CreationDt>20180607</CreationDt>
              <CreatingAgntSignOn>000VSM</CreatingAgntSignOn>
              <CreatingAgntDuty>AG</CreatingAgntDuty>
              <CreatingAgncyIATANum>14319126</CreatingAgncyIATANum>
              <OrigBkLocn>BOMOU</OrigBkLocn>
              <SATONum/>
              <PTAInd>N</PTAInd>
              <InUseInd>N</InUseInd>
              <SimultaneousUpdInd/>
              <BorrowedInd>N</BorrowedInd>
              <GlobInd>N</GlobInd>
              <ReadOnlyInd>N</ReadOnlyInd>
              <FareDataExistsInd>Y</FareDataExistsInd>
              <PastDtQuickInd>N</PastDtQuickInd>
              <CurAgncyPCC>57UW</CurAgncyPCC>
              <QInd>Y</QInd>
              <TkNumExistInd>Y</TkNumExistInd>
              <IMUdataexists>N</IMUdataexists>
              <ETkDataExistInd>Y</ETkDataExistInd>
              <CurDtStamp>20181010</CurDtStamp>
              <CurTmStamp>032536</CurTmStamp>
              <CurAgntSONID>57UWGWS</CurAgntSONID>
              <TravInsuranceInd/>
              <PNRBFTicketedInd>Y</PNRBFTicketedInd>
              <ZeppelinAgncyInd>N</ZeppelinAgncyInd>
              <AgncyAutoServiceInd>N</AgncyAutoServiceInd>
              <AgncyAutoNotifyInd>N</AgncyAutoNotifyInd>
              <ZeppelinPNRInd>N</ZeppelinPNRInd>
              <PNRAutoServiceInd>N</PNRAutoServiceInd>
              <PNRAutoNotifyInd>N</PNRAutoNotifyInd>
              <SuperPNRInd>N</SuperPNRInd>
              <PNRBFPurgeDt>20181114</PNRBFPurgeDt>
              <PNRBFChangeInd>N</PNRBFChangeInd>
              <MCODataExists>N</MCODataExists>
              <OrigRcvdField>C</OrigRcvdField>
              <IntContExists>N</IntContExists>
            </GenPNRInfo>
            <VndRecLocs>
              <RecLocInfoAry>
                <RecLocInfo>
                  <TmStamp>2009</TmStamp>
                  <DtStamp>20180607</DtStamp>
                  <Vnd>1A</Vnd>
                  <RecLoc>LOKCAX</RecLoc>
                </RecLocInfo>
                <RecLocInfo>
                  <TmStamp>2010</TmStamp>
                  <DtStamp>20180607</DtStamp>
                  <Vnd>SU</Vnd>
                  <RecLoc>UGPXAD</RecLoc>
                </RecLocInfo>
              </RecLocInfoAry>
            </VndRecLocs>
          </PNRBFRetrieve>
        </PNRBFManagement_13>
      </SubmitXmlOnSessionResult>
    </SubmitXmlOnSessionResponse>
  </soapenv:Body>
</soapenv:Envelope>";
        string PNBF2Res = @"<?xml version='1.0' encoding='UTF-8'?>
<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
 <soapenv:Body><SubmitXmlOnSessionResponse xmlns='http://webservices.galileo.com'><SubmitXmlOnSessionResult><PNRBFManagement_13 xmlns=''><PNRBFRetrieve><Control><KLRCnt>31</KLRCnt><KlrAry><Klr><ID>BP10</ID><NumOccur>4</NumOccur></Klr><Klr><ID>BP12</ID><NumOccur>4</NumOccur></Klr><Klr><ID>BP13</ID><NumOccur>1</NumOccur></Klr><Klr><ID>IT01</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP21</ID><NumOccur>5</NumOccur></Klr><Klr><ID>BP22</ID><NumOccur>5</NumOccur></Klr><Klr><ID>BP20</ID><NumOccur>0</NumOccur></Klr><Klr><ID>BP19</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP16</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP52</ID><NumOccur>1</NumOccur></Klr><Klr><ID>DPP0</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP32</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP26</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP48</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP28</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP27</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP08</ID><NumOccur>1</NumOccur></Klr><Klr><ID>BP40</ID><NumOccur>1</NumOccur></Klr></KlrAry></Control><LNameInfo><LNameNum>1</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>DEBNATH</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum><FName>JYOTIRMOYMR</FName></FNameInfo><LNameInfo><LNameNum>2</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>DEBNATH</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum><FName>MUNNAMS</FName></FNameInfo><LNameInfo><LNameNum>3</LNameNum><NumPsgrs>1</NumPsgrs><NameType/><LName>DEBNATH</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum><FName>MUKTAMS</FName></FNameInfo><LNameInfo><LNameNum>4</LNameNum><NumPsgrs>1</NumPsgrs><NameType>I</NameType><LName>DEBNATH</LName></LNameInfo><FNameInfo><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum><FName>DEBRAJMSTR</FName></FNameInfo><NameRmkInfo><LNameNum>4</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum><NameRmk>16JUL17</NameRmk></NameRmkInfo><AirSeg><SegNum>1</SegNum><Status>HK</Status><Dt>20190220</Dt><DayChg>00</DayChg><AirV>AI</AirV><NumPsgrs>3</NumPsgrs><StartAirp>IXA</StartAirp><EndAirp>CCU</EndAirp><StartTm>1125</StartTm><EndTm>1225</EndTm><BIC>U</BIC><FltNum>744</FltNum><OpSuf/><COG>N</COG><TklessInd>Y</TklessInd><ConxInd>N</ConxInd><FltFlownInd>N</FltFlownInd><MarriageNum/><SellType>O</SellType><StopoverIgnoreInd/><TDSValidateInd>N</TDSValidateInd><NonBillingInd/><PrevStatusCode>NN</PrevStatusCode><ScheduleValidationInd/></AirSeg><ProgramaticSSR><GFAXNum>1</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0983472703046C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>2</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>2</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>2</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0983472703047C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>3</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>3</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>3</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0983472703048C1</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>4</GFAXNum><SSRCode>INFT</SSRCode><Status>KK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>1</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>1</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>DEBNATH/DEBRAJMSTR 16JUL17</Text></ProgramaticSSRText><ProgramaticSSR><GFAXNum>5</GFAXNum><SSRCode>TKNE</SSRCode><Status>HK</Status><SegNum>1</SegNum><AppliesToAry><AppliesTo><LNameNum>4</LNameNum><PsgrNum>1</PsgrNum><AbsNameNum>4</AbsNameNum></AppliesTo></AppliesToAry></ProgramaticSSR><ProgramaticSSRText><Text>0983472703049C1</Text></ProgramaticSSRText><OSI><GFAXNum>1</GFAXNum><OSIV>YY</OSIV><OSIMsg>MOBILE-9436137496</OSIMsg></OSI><PhoneInfo><PhoneFldNum>1</PhoneFldNum><Pt>DEL</Pt><Type>A</Type><Phone>01147677777</Phone></PhoneInfo><CreditCardFOP><ID>6</ID><Type>1</Type><Currency/><Amt/><ExpDt>420</ExpDt><TransType/><ApprovalInd/><AcceptOverride/><ValidationBypassReq/><Vnd>AX</Vnd><Acct><![CDATA[300000000001007     ]]></Acct><AdditionalInfoAry></AdditionalInfoAry></CreditCardFOP><TkArrangement><Text>BOM 16JAN0642Z WS AG</Text></TkArrangement><GenRmkInfo><GenRmkNum>1</GenRmkNum><CreationDt>20190116</CreationDt><CreationTm>642</CreationTm><GenlRmkQual/><GenRmk>ON-LINE BOOKING - 9436137496</GenRmk></GenRmkInfo><VndRmk><RmkNum>1</RmkNum><TmStamp>642</TmStamp><DtStamp>20190116</DtStamp><RmkType>I</RmkType><VType>A</VType><Vnd>AI</Vnd><Rmk>AUTO XX IF SSR TKNA/E/M/C NOT RCVD BY AI BY 1212/24JAN/ DEL LT</Rmk></VndRmk><GenPNRInfo><FileAddr>E6018070</FileAddr><CodeCheck>9B</CodeCheck><RecLoc>997KTK</RecLoc><Ver>5</Ver><OwningCRS>1G</OwningCRS><OwningAgncyName>RWT COM</OwningAgncyName><OwningAgncyPCC>000V</OwningAgncyPCC><CreationDt>20190116</CreationDt><CreatingAgntSignOn>000VGWS</CreatingAgntSignOn><CreatingAgntDuty>AG</CreatingAgntDuty><CreatingAgncyIATANum>14319126</CreatingAgncyIATANum><OrigBkLocn>BOMOU</OrigBkLocn><SATONum/><PTAInd>N</PTAInd><InUseInd>N</InUseInd><SimultaneousUpdInd/><BorrowedInd>N</BorrowedInd><GlobInd>N</GlobInd><ReadOnlyInd>N</ReadOnlyInd><FareDataExistsInd>Y</FareDataExistsInd><PastDtQuickInd>N</PastDtQuickInd><CurAgncyPCC>0JO7</CurAgncyPCC><QInd>Y</QInd><TkNumExistInd>Y</TkNumExistInd><IMUdataexists>N</IMUdataexists><ETkDataExistInd>Y</ETkDataExistInd><CurDtStamp>20190116</CurDtStamp><CurTmStamp>064242</CurTmStamp><CurAgntSONID>JO7GWS</CurAgntSONID><TravInsuranceInd/><PNRBFTicketedInd>Y</PNRBFTicketedInd><ZeppelinAgncyInd>N</ZeppelinAgncyInd><AgncyAutoServiceInd>N</AgncyAutoServiceInd><AgncyAutoNotifyInd>N</AgncyAutoNotifyInd><ZeppelinPNRInd>N</ZeppelinPNRInd><PNRAutoServiceInd>N</PNRAutoServiceInd><PNRAutoNotifyInd>N</PNRAutoNotifyInd><SuperPNRInd>N</SuperPNRInd><PNRBFPurgeDt>20190222</PNRBFPurgeDt><PNRBFChangeInd>N</PNRBFChangeInd><MCODataExists>N</MCODataExists><OrigRcvdField>PASSENGER</OrigRcvdField><IntContExists>N</IntContExists></GenPNRInfo><Email><ItemNum>1</ItemNum><Type>T</Type><Data>binac3@gmail.com</Data></Email><VndRecLocs><RecLocInfoAry><RecLocInfo><TmStamp>642</TmStamp><DtStamp>20190116</DtStamp><Vnd>AI</Vnd><RecLoc>YNMZ1</RecLoc></RecLocInfo></RecLocInfoAry></VndRecLocs></PNRBFRetrieve></PNRBFManagement_13></SubmitXmlOnSessionResult></SubmitXmlOnSessionResponse></soapenv:Body></soapenv:Envelope>";

        if (1==2)
        {
            string ggg = "0";
            PNBF2Res = PNBF2Res1;

        }


        ArrayList TktNoArray = new ArrayList();
        try
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(PNBF2Res);
            XmlNode xxd = xd.SelectSingleNode("//PNRBFRetrieve");
            XmlNodeList xt = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSRText");
            //XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR");
            XmlNodeList xt1 = xd.SelectNodes("//PNRBFRetrieve/ProgramaticSSR[starts-with(SSRCode,'TKNE')]");
            int aa = xt.Count - 1;
            for (int ii = 0; ii <= xt1.Count - 1; ii++)
            {
                XmlNode xt2 = xt1[ii];
                xt1[ii].AppendChild(xt[ii]);
                xxd.ReplaceChild(xt2, xt1[ii]);
            }
            int i = 0;
            XDocument xdo = XDocument.Load(new XmlNodeReader(xxd));

            var tkt = from t in xdo.Descendants("PNRBFRetrieve").Descendants("ProgramaticSSR")
                      from p in xdo.Descendants("PNRBFRetrieve").Descendants("FNameInfo")
                      from l in xdo.Descendants("PNRBFRetrieve").Descendants("LNameInfo")
                      where p.Element("AbsNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value &&
                      l.Element("LNameNum").Value == t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value &&
                      t.Element("Status").Value == "HK"
                      select new
                      {
                          indx = i,
                          fn = p.Element("FName").Value,
                          ln = l.Element("LName").Value,
                          tk = t.Element("ProgramaticSSRText").Element("Text").Value,
                          abs = t.Element("AppliesToAry").Element("AppliesTo").Element("AbsNameNum").Value

                      };

            foreach (var a in tkt)
            {
                i++;
                string[] tkt1 = Utility.Split(a.fn, " ");
                string nm = "", tkno = "";
                for (int n = tkt1.Length - 1; n >= 0; n--)
                {
                    nm = nm + tkt1[n].ToString();
                }
                tkno = Utility.Left(Utility.Left(a.tk, 13), 3) + "-" + Utility.Right(Utility.Left(a.tk, 13), 10);
                TktNoArray.Add(nm + a.ln + "/" + tkno);
            }
        }
        catch (Exception ex1)
        {
            TktNoArray.Add("AirLine is not able to make ETicket");
        }

    }


    private bool CheckPnrCreationStatus(string strRes, DataSet SlcFltDs, int Cnt, bool WOCHD)
    {
        try
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(strRes);
            XDocument xdo = XDocument.Load(new XmlNodeReader(xd));
            XmlNamespaceManager mgr = new XmlNamespaceManager(xd.NameTable);

            mgr.AddNamespace("SubmitXmlOnSessionResponse", "http://webservices.galileo.com");

            XmlNode temp = xd.SelectSingleNode("//Status", mgr);
            XmlNode temp1 = xd.SelectSingleNode("//FareFiledOKInd", mgr);
            XmlNodeList temp2 = xd.SelectNodes("//TotAmt", mgr);
            XmlNodeList temp3 = xd.SelectNodes("//GenQuoteDetails/UniqueKey", mgr);
            XmlNodeList temp4 = xd.SelectNodes("//PsgrTypes/PICReq", mgr);
            string st = "", FiledInd = "";
            double TF = 0, TotFare = 0;
            st = temp.InnerText;
            FiledInd = temp1.InnerText;
            if (SlcFltDs.Tables[0].Rows[0]["Searchvalue"].ToString().Trim() == "PROMOFARE")
            {
                for (int Q = 0; Q <= temp2.Count - 1; Q++)
                {
                    if (Q == 0)
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Adult"].ToString().Trim()));
                    else if ((Q == 1) && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()) > 0))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()));
                    else if ((Q == 1) && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()) > 0))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()));
                    else if (Q == 2)
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()));
                }
            }
            else
            {
                for (int Q = 0; Q <= temp2.Count - 1; Q++)
                {
                    if ((temp3[Q].InnerText == "1") && (temp4[Q].InnerText == "ADT"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Adult"].ToString().Trim()));
                    else if ((temp3[Q].InnerText == "1") && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()) > 0) && (temp4[Q].InnerText == "INF"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()));
                    else if ((temp3[Q].InnerText == "1") && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()) > 0) && (temp4[Q].InnerText == "CNN"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()));
                    else if ((temp3[Q].InnerText == "2") && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()) > 0) && (temp4[Q].InnerText == "CNN"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()));
                    else if ((temp3[Q].InnerText == "2") && (double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()) > 0) && (temp4[Q].InnerText == "INF"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()));
                    else if (temp3[Q].InnerText == "3" && (temp4[Q].InnerText == "INF"))
                        TF = TF + (double.Parse(temp2[Q].InnerText) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim()));

                }
            }
            //SMS charge calc
            double SMSChg = 0;
            try
            {
                SMSChg = double.Parse(SlcFltDs.Tables[0].Rows[0]["OriginalTT"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Adult"].ToString().Trim());
                SMSChg = SMSChg + (double.Parse(SlcFltDs.Tables[0].Rows[0]["OriginalTT"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()));
            }
            catch
            { }

            TF = TF + SMSChg;
            //SMS charge calc end

            if (WOCHD == false)
            {
                TotFare = double.Parse(SlcFltDs.Tables[0].Rows[0]["AdtFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Adult"].ToString().Trim());
                TotFare += double.Parse(SlcFltDs.Tables[0].Rows[0]["ChdFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim());
                TotFare += double.Parse(SlcFltDs.Tables[0].Rows[0]["InfFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim());
            }
            else
            {
                if (Cnt == 0)
                {
                    TotFare = double.Parse(SlcFltDs.Tables[0].Rows[0]["AdtFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Adult"].ToString().Trim());
                    // TotFare += double.Parse(SlcFltDs.Tables[0].Rows[0]["ChdFare"].ToString().Trim());
                    TotFare += double.Parse(SlcFltDs.Tables[0].Rows[0]["InfFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Infant"].ToString().Trim());
                }
                else
                { TotFare = double.Parse(SlcFltDs.Tables[0].Rows[0]["ChdFare"].ToString().Trim()) * double.Parse(SlcFltDs.Tables[0].Rows[0]["Child"].ToString().Trim()); }

            }

            if (((st.Trim() == "SS") || (st.Trim() == "HS")) && (FiledInd.Trim() == "Y") && (TF == TotFare))
            { return true; }
            else { return false; }
        }
        catch
        {
            return false;
        }
    }
    public List<FlightSearchResults> ParseAirPrice(string response)
    {
        string str = response.Replace("xmlns:soapenv='\"http://schemas.xmlsoap.org/soap/envelope/\"", "").Replace("soapenv:", "")
             .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
             .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
             .Replace("xmlns=\"http://www.opentravel.org/OTA/2003/05\"", "")
             .Replace("xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\"", "")
             .Replace("ns1:", "");

        List<FlightSearchResults> resultList = new List<FlightSearchResults>();

        XDocument xmlDoc = XDocument.Parse(str);

        IEnumerable<XElement> xlFPricedItinerary = from el in xmlDoc.Descendants("OTA_AirPriceRS").Descendants("PricedItineraries").Descendants("PricedItinerary")
                                                   select el;

        decimal totalfare = 0;


        for (int ptn = 0; ptn < xlFPricedItinerary.Count(); ptn++)
        {

            IEnumerable<XElement> xlFAirODItnry = xlFPricedItinerary.ElementAt(ptn).Descendants("AirItinerary").Descendants("OriginDestinationOptions").Descendants("OriginDestinationOption");

            IEnumerable<XElement> xlFAirPriceItnry = xlFPricedItinerary.ElementAt(ptn).Descendants("AirItineraryPricingInfo");

            IEnumerable<XElement> xlFAirODItnries = xlFPricedItinerary.ElementAt(ptn).Descendants("AirItinerary");//.Descendants("OriginDestinationOptions").Descendants("OriginDestinationOption");

            #region Origin Destination

            for (int odn = 0; odn < xlFAirODItnry.Count(); odn++)
            {


                IEnumerable<XElement> segmnt = xlFAirODItnry.ElementAt(odn).Descendants("FlightSegment");


                for (int s = 0; s < segmnt.Count(); s++)
                {

                    XElement seg = segmnt.ElementAt(s);

                    FlightSearchResults fsr = new FlightSearchResults();


                    fsr.Leg = s + 1;
                    fsr.LineNumber = ptn + 1;
                    fsr.TotDur = xlFAirODItnry.ElementAt(odn).Elements().FirstOrDefault().Attribute("Duration").Value.ToString();


                    xlFAirODItnries.ToList().ForEach(x => { fsr.Searchvalue = fsr.Searchvalue + x.ToString(); });

                    xlFAirPriceItnry.ToList().ForEach(x => { fsr.sno = fsr.sno + x.ToString(); });

                    fsr.Flight = (odn + 1).ToString();
                   


                    fsr.Stops = (segmnt.Count() - 1).ToString() + "-Stop";

                    //fsr.Adult = searchInput.Adult;
                    //fsr.Child = searchInput.Child;
                    //fsr.Infant = searchInput.Infant;


                    fsr.Adult = 1;
                    fsr.Child = 1;
                    fsr.Infant =1;


                    fsr.depdatelcc = seg.Attribute("DepartureDateTime").Value.Trim();
                    fsr.arrdatelcc = seg.Attribute("ArrivalDateTime").Value.Trim();
                    fsr.Departure_Date = Convert.ToDateTime(fsr.depdatelcc).ToString("dd MMM"); ;// legdetailsM.DepartureDate[8].ToString() + legdetailsM.DepartureDate[9].ToString() + " " + GetMonthName(Convert.ToInt16(legdetailsM.DepartureDate[5].ToString() + legdetailsM.DepartureDate[6].ToString()));
                    fsr.Arrival_Date = Convert.ToDateTime(fsr.arrdatelcc).ToString("dd MMM");// legdetailsM.ArrivalDate[8].ToString() + legdetailsM.ArrivalDate[9].ToString() + " " + GetMonthName(Convert.ToInt16(legdetailsM.ArrivalDate[5].ToString() + legdetailsM.ArrivalDate[6].ToString()));
                    fsr.DepartureDate = Convert.ToDateTime(fsr.depdatelcc).ToString("ddMMyy");
                    fsr.ArrivalDate = Convert.ToDateTime(fsr.arrdatelcc).ToString("ddMMyy");


                    fsr.FlightIdentification = seg.Attribute("FlightNumber").Value.Trim();
                    fsr.AirLineName = seg.Element("MarketingAirline").Attribute("Name").Value.Trim();

                    try { fsr.ValiDatingCarrier = seg.Element("ValidatingCarrier").Attribute("Code").Value.Trim(); }
                    catch { fsr.ValiDatingCarrier = seg.Element("MarketingAirline").Attribute("Code").Value.Trim(); }

                    fsr.OperatingCarrier = fsr.ValiDatingCarrier;
                    fsr.MarketingCarrier = seg.Element("MarketingAirline").Attribute("Code").Value.Trim();//seg.Airline.OperatingCarrier.Trim();
                    fsr.fareBasis = seg.Attribute("ResBookDesigCode").Value.Trim();

                    //if (searchInput.HidTxtAirLine.Contains("G8"))
                    //{
                    //    fsr.AdtFar = seg.Element("BookingClassAvail").Attribute("WebFareName").Value.ToString();
                    //}
                    //else
                    //{ fsr.AdtFar = ""; }


                    fsr.DepartureLocation = seg.Element("DepartureAirport").Attribute("LocationCode").Value.Trim();
                    fsr.DepartureCityName = seg.Element("DepartureAirport").Attribute("CityName").Value.Trim();
                    fsr.DepartureTime = Convert.ToDateTime(fsr.depdatelcc).ToString("HHmm");
                    fsr.DepartureAirportName = seg.Element("DepartureAirport").Attribute("AirPortName").Value.Trim();
                    try { fsr.DepartureTerminal = seg.Element("DepartureAirport").Attribute("Terminal").Value.Trim(); }
                    catch { }

                    fsr.DepAirportCode = seg.Element("DepartureAirport").Attribute("LocationCode").Value.Trim();

                    fsr.ArrivalLocation = seg.Element("ArrivalAirport").Attribute("LocationCode").Value.Trim();
                    fsr.ArrivalCityName = seg.Element("ArrivalAirport").Attribute("CityName").Value.Trim();
                    fsr.ArrivalTime = Convert.ToDateTime(fsr.arrdatelcc).ToString("HHmm");
                    fsr.ArrivalAirportName = seg.Element("ArrivalAirport").Attribute("AirPortName").Value.Trim();

                    try { fsr.ArrivalTerminal = seg.Element("ArrivalAirport").Attribute("Terminal").Value.Trim(); }
                    catch { }

                    fsr.ArrAirportCode = seg.Element("ArrivalAirport").Attribute("LocationCode").Value.Trim();
                   // fsr.Trip = searchInput.Trip.ToString();
                    fsr.Trip = "D";
                    fsr.EQ = seg.Element("Equipment").Attribute("AirEquipType").Value.Trim();
                    fsr.Provider = "YA";
                    fsr.AdtFareType = xlFPricedItinerary.ElementAt(ptn).Attribute("FareType").Value.Trim();
                    



                    IEnumerable<XElement> farebrkup = xlFAirPriceItnry.Descendants("PTC_FareBreakdowns").Descendants("PTC_FareBreakdown");


                    fsr.AdtCabin = xlFAirODItnry.ElementAt(odn).Descendants("FormData").Descendants("FareDifference").Descendants("TotalFare").FirstOrDefault().Attribute("Cabin").Value.Trim();
                    fsr.ChdCabin = fsr.AdtCabin;
                    fsr.InfCabin = fsr.AdtCabin;
                    if (!(fsr.MarketingCarrier == "6E" || fsr.MarketingCarrier == "G8" || fsr.MarketingCarrier == "SG"))
                    {

                        fsr.AdtAvlStatus = xlFAirODItnry.ElementAt(odn).Descendants("FormData").Descendants("FBC").FirstOrDefault().Attribute("SeatToSell").Value.Trim();
                        fsr.ChdAvlStatus = fsr.AdtAvlStatus;
                        fsr.InfAvlStatus = fsr.AdtAvlStatus;
                    }
                    else
                    {
                        try { fsr.AdtAvlStatus = xlFPricedItinerary.ElementAt(ptn).Attribute("SeatToSell").Value.Trim(); }
                        catch { fsr.AdtAvlStatus = "9"; }

                        fsr.ChdAvlStatus = fsr.AdtAvlStatus;
                        fsr.InfAvlStatus = fsr.AdtAvlStatus;
                    }



                    #region fare calculation


                    IEnumerable<XElement> xItinTotalFare = xlFAirPriceItnry.Descendants("ItinTotalFare");

                    decimal tFeess = 0;
                 

                    try
                    {
                        fsr.BagInfo = xItinTotalFare.Descendants("FareBaggageAllowance").FirstOrDefault().Attribute("UnitOfMeasureQuantity").Value + " " + xItinTotalFare.Descendants("FareBaggageAllowance").FirstOrDefault().Attribute("UnitOfMeasure").Value;
                    }
                    catch { }
                    fsr.OriginalTF = float.Parse(xItinTotalFare.Descendants("TotalFare").FirstOrDefault().Attribute("Amount").Value);

                   


                    float totalfareItn = 0;
                    for (int frb = 0; frb < farebrkup.Count(); frb++)
                    {

                        XElement objfrb = farebrkup.ElementAt(frb);

                        #region Adult
                        if (objfrb.Element("PassengerTypeQuantity").Attribute("Code").Value.Trim().ToUpper() == "ADT")
                        {

                            
                            fsr.AdtBfare = float.Parse(objfrb.Descendants("PassengerFare").Descendants("BaseFare").FirstOrDefault().Attribute("Amount").Value);//(float)Math.Ceiling(Convert.ToDecimal(objfrb.Element("DisplayFareAmt").Value.Trim()));
                            totalfareItn = totalfareItn + float.Parse(objfrb.Descendants("PassengerFare").Descendants("TotalFare").FirstOrDefault().Attribute("Amount").Value) * fsr.Adult;

                           
                            float AdtTax = 0;


                            if (!(fsr.MarketingCarrier == "6E" || fsr.MarketingCarrier == "G8" || fsr.MarketingCarrier == "SG"))
                            {
                                fsr.AdtRbd = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();
                                fsr.AdtFarebasis = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ActualFareBase").Value.Trim();

                            }
                            else
                            {
                                fsr.AdtRbd = seg.Descendants("BookingClassAvail").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();
                                try
                                {
                                    fsr.AdtFarebasis = xlFAirODItnry.ElementAt(odn).Descendants("OriginDestinationOption").FirstOrDefault().Attribute("FareBasisCode").Value.Trim();
                                }
                                catch { }

                            }


                           
                            fsr.FBPaxType = "ADT";
                          



                            IEnumerable<XElement> TaxDetails = objfrb.Descendants("PassengerFare").Descendants("Taxes").Descendants("Tax");

                            for (int td = 0; td < TaxDetails.Count(); td++)
                            {
                                XElement taxel = TaxDetails.ElementAt(td);

                                if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "WO")
                                {
                                    fsr.AdtWO = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    AdtTax = AdtTax + fsr.AdtWO;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "JN")
                                {
                                    fsr.AdtJN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    AdtTax = AdtTax + fsr.AdtJN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "IN")
                                {
                                    fsr.AdtIN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    AdtTax = AdtTax + fsr.AdtIN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "YQ")
                                {
                                    fsr.AdtFSur = fsr.AdtFSur + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    AdtTax = AdtTax + fsr.AdtFSur;
                                }
                                else
                                {
                                    fsr.AdtOT = fsr.AdtOT + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));

                                }

                            }


                         


                            fsr.AdtOT = fsr.AdtOT + (float.Parse(objfrb.Descendants("PassengerFare").Descendants("ServiceTax").FirstOrDefault().Attribute("Amount").Value));// / searchInput.Adult) + (float.Parse(tFeess.ToString()) / searchInput.Adult);
                            AdtTax = AdtTax + (float)Math.Ceiling(fsr.AdtOT);

                            fsr.AdtTax = AdtTax;

                            fsr.AdtFare = fsr.AdtBfare + fsr.AdtTax;


                        }
                        #endregion


                        #region Child
                        if (objfrb.Element("PassengerTypeQuantity").Attribute("Code").Value.Trim().ToUpper() == "CHD")
                        {

                            
                            fsr.ChdBFare = float.Parse(objfrb.Descendants("PassengerFare").Descendants("BaseFare").FirstOrDefault().Attribute("Amount").Value);//(float)Math.Ceiling(Convert.ToDecimal(objfrb.Element("DisplayFareAmt").Value.Trim()));
                            totalfareItn = totalfareItn + float.Parse(objfrb.Descendants("PassengerFare").Descendants("TotalFare").FirstOrDefault().Attribute("Amount").Value) * fsr.Child;

                            
                            float ChdTax = 0;
                            if (!(fsr.MarketingCarrier == "6E" || fsr.MarketingCarrier == "G8" || fsr.MarketingCarrier == "SG"))
                            {
                                fsr.ChdRbd = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();
                                fsr.ChdFarebasis = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ActualFareBase").Value.Trim();

                            }
                            else
                            {
                                fsr.ChdRbd = seg.Descendants("BookingClassAvail").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();

                                try
                                {
                                    fsr.ChdFarebasis = xlFAirODItnry.ElementAt(odn).Descendants("OriginDestinationOption").FirstOrDefault().Attribute("FareBasisCode").Value.Trim();
                                }
                                catch { }

                            }
                            fsr.FBPaxType = "Chd";
                          


                            IEnumerable<XElement> TaxDetails = objfrb.Descendants("PassengerFare").Descendants("Taxes").Descendants("Tax");

                            for (int td = 0; td < TaxDetails.Count(); td++)
                            {
                                XElement taxel = TaxDetails.ElementAt(td);

                                if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "WO")
                                {
                                    fsr.ChdWO = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    ChdTax = ChdTax + fsr.ChdWO;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "JN")
                                {
                                    fsr.ChdJN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    ChdTax = ChdTax + fsr.ChdJN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "IN")
                                {
                                    fsr.ChdIN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    ChdTax = ChdTax + fsr.ChdIN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "YQ")
                                {
                                    fsr.ChdFSur = fsr.ChdFSur + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    ChdTax = ChdTax + fsr.ChdFSur;
                                }
                                else
                                {
                                    fsr.ChdOT = fsr.ChdOT + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));

                                }

                            }


                           


                            fsr.ChdOT = fsr.ChdOT + float.Parse(objfrb.Descendants("PassengerFare").Descendants("ServiceTax").FirstOrDefault().Attribute("Amount").Value);// / searchInput.Adult;
                            ChdTax = ChdTax + (float)Math.Ceiling(fsr.ChdOT);

                            fsr.ChdTax = ChdTax;

                            fsr.ChdFare = fsr.ChdBFare + fsr.ChdTax;


                        }



                        #endregion


                        #region Infant
                        if (objfrb.Element("PassengerTypeQuantity").Attribute("Code").Value.Trim().ToUpper() == "INF")
                        {


                           
                            fsr.InfBfare = float.Parse(objfrb.Descendants("PassengerFare").Descendants("BaseFare").FirstOrDefault().Attribute("Amount").Value);//(float)Math.Ceiling(Convert.ToDecimal(objfrb.Element("DisplayFareAmt").Value.Trim()));
                            totalfareItn = totalfareItn + float.Parse(objfrb.Descendants("PassengerFare").Descendants("TotalFare").FirstOrDefault().Attribute("Amount").Value) * fsr.Infant;
                           
                            float InfTax = 0;

                            if (!(fsr.MarketingCarrier == "6E" || fsr.MarketingCarrier == "G8" || fsr.MarketingCarrier == "SG"))
                            {
                                fsr.InfRbd = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();
                                fsr.InfFarebasis = objfrb.Descendants("FareBasisCodes").Descendants("FareBasisCode").FirstOrDefault().Attribute("ActualFareBase").Value.Trim();

                            }
                            else
                            {
                                fsr.InfRbd = seg.Descendants("BookingClassAvail").FirstOrDefault().Attribute("ResBookDesigCode").Value.Trim();

                                try
                                {
                                    fsr.InfFarebasis = xlFAirODItnry.ElementAt(odn).Descendants("OriginDestinationOption").FirstOrDefault().Attribute("FareBasisCode").Value.Trim();

                                }
                                catch { }
                            }

                         


                            IEnumerable<XElement> TaxDetails = objfrb.Descendants("PassengerFare").Descendants("Taxes").Descendants("Tax");

                            for (int td = 0; td < TaxDetails.Count(); td++)
                            {
                                XElement taxel = TaxDetails.ElementAt(td);

                                if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "WO")
                                {
                                    fsr.InfWO = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    InfTax = InfTax + fsr.InfWO;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "JN")
                                {
                                    fsr.InfJN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    InfTax = InfTax + fsr.InfJN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "IN")
                                {
                                    fsr.InfIN = (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    InfTax = InfTax + fsr.InfIN;
                                }
                                else if (taxel.Attribute("TaxCode").Value.Trim().ToUpper() == "YQ")
                                {
                                    fsr.InfFSur = fsr.InfFSur + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));
                                    InfTax = InfTax + fsr.InfFSur;
                                }
                                else
                                {
                                    fsr.InfOT = fsr.InfOT + (float)Math.Ceiling(Convert.ToDecimal(taxel.Attribute("Amount").Value));

                                }

                            }




                            fsr.InfOT = fsr.InfOT + float.Parse(objfrb.Descendants("PassengerFare").Descendants("ServiceTax").FirstOrDefault().Attribute("Amount").Value);// / searchInput.Adult;
                            InfTax = InfTax + (float)Math.Ceiling(fsr.InfOT);

                            fsr.InfTax = InfTax;

                            fsr.InfFare = fsr.InfBfare + fsr.InfTax;


                        }
                        #endregion

                    }
                    float fareDiff = (float)Math.Ceiling(Convert.ToDecimal((fsr.OriginalTF - totalfareItn) / fsr.Adult));
                    fsr.AdtOT = fsr.AdtOT + fareDiff;
                    fsr.AdtTax = fsr.AdtTax + fareDiff;
                    fsr.AdtFare = fsr.AdtFare + fareDiff;

                    #endregion


                    //if (isSplFare)
                    //{
                    //    fsr.Sector = searchInput.HidTxtDepCity.Split(',')[0] + ":" + searchInput.HidTxtArrCity.Split(',')[0] + ":" + searchInput.HidTxtDepCity.Split(',')[0];

                    //}
                    //else
                    //{
                    //    fsr.Sector = searchInput.HidTxtDepCity.Split(',')[0] + ":" + searchInput.HidTxtArrCity.Split(',')[0];

                    //}

                    fsr.AvailableSeats = fsr.AdtAvlStatus + ":" + fsr.ChdAvlStatus + ":" + fsr.InfAvlStatus;
                    fsr.TotalFare = (fsr.Adult * fsr.AdtFare) + (fsr.Child * fsr.ChdFare) + (fsr.Infant * fsr.InfFare);
                    fsr.TotalFuelSur = (fsr.Adult * fsr.AdtFSur) + (fsr.Child * fsr.ChdFSur) + (fsr.Infant * fsr.InfFSur);
                    fsr.TotalTax = (fsr.Adult * fsr.AdtTax) + (fsr.Child * fsr.ChdTax) + (fsr.Infant * fsr.InfTax);
                    fsr.TotBfare = (fsr.Adult * fsr.AdtBfare) + (fsr.Child * fsr.ChdBFare) + (fsr.Infant * fsr.InfBfare);
                    fsr.AvailableSeats = fsr.AdtAvlStatus + ":" + fsr.ChdAvlStatus + ":" + fsr.InfAvlStatus;
                    fsr.TotMrkUp = (fsr.ADTAdminMrk * fsr.Adult) + (fsr.ADTAgentMrk * fsr.Adult) + (fsr.CHDAdminMrk * fsr.Child) + (fsr.CHDAgentMrk * fsr.Child);
                    fsr.TotalFare = fsr.TotalFare + fsr.TotMrkUp + fsr.STax + fsr.TFee + fsr.TotMgtFee;
                    fsr.NetFare = (fsr.TotalFare + fsr.TotTds) - (fsr.TotDis + fsr.TotCB + (fsr.ADTAgentMrk * fsr.Adult) + (fsr.CHDAgentMrk * fsr.Child));

                    //if (Convert.ToInt32(fsr.Flight) == 1)
                    //{
                    //    fsr.OrgDestFrom = searchInput.HidTxtDepCity.Split(',')[0];
                    //    fsr.OrgDestTo = fsr.OrgDestFrom = searchInput.HidTxtArrCity.Split(',')[0];
                    //    fsr.TripType = TripType.O.ToString();
                    //    //fsrList.Add(fsr);
                    //}
                    //else if (Convert.ToInt32(fsr.Flight) == 2)
                    //{
                    //    fsr.OrgDestFrom = fsr.OrgDestFrom = searchInput.HidTxtArrCity.Split(',')[0];
                    //    fsr.OrgDestTo = fsr.OrgDestFrom = searchInput.HidTxtDepCity.Split(',')[0];
                    //    fsr.TripType = TripType.R.ToString();
                    //   // RfsrList.Add(fsr);
                    //}

                    resultList.Add(fsr);
                }


            }




            #endregion





        }


        return resultList;
    }

}