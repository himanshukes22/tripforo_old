using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COMN;
using COMN_SHARED;
using COMN_SHARED.Flight;
using System.Configuration;

using System.Data;


using STD.Shared;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Data.SqlClient;

public partial class FlightInt_Booking : System.Web.UI.Page
{
    SqlTransactionDom objSqlDom = new SqlTransactionDom();
    IntlDetails objTktCopyMail = new IntlDetails();

    SqlTransactionDom objTranDom = new SqlTransactionDom();
    SqlTransaction SqlTrasaction = new SqlTransaction();


    DataTable FltPaxList = new DataTable();

    DataTable FltDetailsList = new DataTable();
    DataTable FltProvider = new DataTable();
    DataTable FltBaggage = new DataTable();
    DataTable dtagentid = new DataTable();
    DataTable FltagentDetail = new DataTable();
    DataTable fltTerminal = new DataTable();
    DataTable fltFare = new DataTable();
    DataTable fltMealAndBag = new DataTable();
    DataTable fltMealAndBag1 = new DataTable();
    DataTable FltHeaderList = new DataTable();
    DataTable fltTerminalDetails = new DataTable();
    DataSet SelectedFltDS = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        string tktCopy = "";
        if (Session["UID"] == "" || Session["UID"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (Convert.ToBoolean(Session["IntBookIng"]) == true || Convert.ToBoolean(Session["BookIng"])==true)
            {
               
                Response.Redirect("../Login.aspx");
            }
            else
            {
                Session["IntBookIng"] = true;
                Session["BookIng"] = true;
                string FT = Convert.ToString(Request.QueryString["FT"]);
                string OBTrackId = Convert.ToString(Request.QueryString["OBTID"]);
                string IBTrackId = Convert.ToString(Request.QueryString["IBTID"]);
                Flt objflt = new Flt(Variables.Constr);

                COMN_SHARED.Flight.FltBookReqShrd objReqBook = new COMN_SHARED.Flight.FltBookReqShrd();
                objReqBook.OrderID = OBTrackId;
                objReqBook.OrderIDR = IBTrackId;
                objReqBook.UserID = Convert.ToString(Session["UID"]);
                FltBookResShrd resp = objflt.CreateBooking(objReqBook);
                FltPaxList = SelectPaxDetail(OBTrackId, "");
                FltHeaderList = objTktCopyMail.SelectHeaderDetail(OBTrackId);
                FltDetailsList = objTktCopyMail.SelectFlightDetail(OBTrackId);
                FltProvider = (objTranDom.GetTicketingProvider(OBTrackId)).Tables[0];
                dtagentid = objTktCopyMail.SelectAgent(OBTrackId);
                SelectedFltDS = SqlTrasaction.GetFltDtls(OBTrackId, dtagentid.Rows[0]["AgentID"].ToString());
                //'Dim Bag As Boolean = False
                //If Not String.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))) Then
                //    Bag = Convert.ToBoolean(SelectedFltDS.Tables(0).Rows(0)("IsBagFare"))
                //End If
                Boolean Bag = false;
                if (!string.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"])))
                {
                    Bag = Convert.ToBoolean(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"]));
                }

                FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows[0]["Trip"]), Convert.ToString(FltHeaderList.Rows[0]["VC"]), Bag)).Tables[0];                
                FltagentDetail = objTktCopyMail.SelectAgencyDetail(dtagentid.Rows[0]["AgentID"].ToString());                
                fltFare = objTktCopyMail.SelectFareDetail(OBTrackId, "");
				//TicketFormate += FltDetailsList.Rows[f]["AirlineCode"] + " " + ;

                tktCopy = tktCopy + mailTktCopy(Convert.ToString(FltHeaderList.Rows[0]["VC"]), FltDetailsList.Rows[0]["FltNumber"].ToString(), FltHeaderList.Rows[0]["Sector"].ToString(), FltDetailsList.Rows[0]["DepDate"].ToString(), "OutBound", resp.PNRO.AirlinePNR, resp.PNRO.GdsPNR, resp.PNRO.Status, OBTrackId, FltHeaderList.Rows[0]["PgEmail"].ToString());

                try { 
                if(!string.IsNullOrEmpty(FT) && FT.ToUpper().Trim()=="INBOUND")
                {

                    FltPaxList = SelectPaxDetail(OBTrackId, "");
                    FltHeaderList = objTktCopyMail.SelectHeaderDetail(IBTrackId);
                    FltDetailsList = objTktCopyMail.SelectFlightDetail(IBTrackId);
                    FltProvider = (objTranDom.GetTicketingProvider(IBTrackId)).Tables[0];
                    dtagentid = objTktCopyMail.SelectAgent(IBTrackId);
                    SelectedFltDS = SqlTrasaction.GetFltDtls(IBTrackId, dtagentid.Rows[0]["AgentID"].ToString());
                    //Boolean Bag = false;
                    Bag = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"])))
                    {
                        Bag = Convert.ToBoolean(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"]));
                    }
                    FltBaggage = (objTranDom.GetBaggageInformation(Convert.ToString(FltHeaderList.Rows[0]["Trip"]), Convert.ToString(FltHeaderList.Rows[0]["VC"]),Bag)).Tables[0];                   
                    FltagentDetail = objTktCopyMail.SelectAgencyDetail(dtagentid.Rows[0]["AgentID"].ToString());                    
                    fltFare = objTktCopyMail.SelectFareDetail(IBTrackId, "");

                    tktCopy = tktCopy + mailTktCopy(Convert.ToString(FltHeaderList.Rows[0]["VC"]), FltDetailsList.Rows[0]["FltNumber"].ToString(), FltHeaderList.Rows[0]["Sector"].ToString(), FltDetailsList.Rows[0]["DepDate"].ToString(), "OutBound", resp.PNRR.AirlinePNR, resp.PNRR.GdsPNR, resp.PNRR.Status, OBTrackId, FltHeaderList.Rows[0]["PgEmail"].ToString());

                }
                }
                catch (Exception ex)
                { }
                Session["IntStrTktCopy"] = Convert.ToString(Session["IntStrTktCopy"]) + tktCopy;

                 Response.Redirect("BookConfirmation.aspx?OrderId=" + OBTrackId, true);


            }

        }





    }





    private string mailTktCopy(string VC, string FltNo, string Sector, string DepDate, string FT, string AirlinePnr, string GdsPnr, string BkgStatus, string OrderId, string EmailId)
    {

        string strTktCopy = "";
        string strHTML = "";
        string strFileName = "";
        string strMailMsg = "";
        bool rightHTML = false;
        try
        {
            TktCopyForMail objtkt = new TktCopyForMail();
            strFileName = ConfigurationManager.AppSettings["SPR_TicketCopy"].ToString().Trim() + GdsPnr + "-" + FT + " Flight details-" + DateTime.Now.ToString().Replace(":", "").Trim() + ".html";
            strFileName = strFileName.Replace("/", "-");
            strTktCopy = TicketCopyExportPDF(OrderId, "");
            //'objtkt.TicketDetail(OrderId, "", 0, "")
            strHTML = "<html><head><title>Booking Details</title><style type='text/css'> .maindiv{border: #20313f 1px solid; margin: 10px auto 10px auto; width: 650px; font-size:12px; font-family:tahoma,Arial;}\t .text1{color:#333333; font-weight:bold;}\t .pnrdtls{font-size:12px; color:#333333; text-align:left;font-weight:bold;}\t .pnrdtls1{font-size:12px; color:#333333; text-align:left;}\t .bookdate{font-size:11px; color:#CC6600; text-align:left}\t .flthdr{font-size:11px; color:#CC6600; text-align:left; font-weight:bold}\t .fltdtls{font-size:11px; color:#333333; text-align:left;}\t.text3{font-size:11px; padding:5px;color:#333333; text-align:right}\t .hdrtext{padding-left:5px; font-size:14px; font-weight:bold; color:#FFFFFF;}\t .hdrtd{background-color:#333333;}\t  .lnk{color:#333333;text-decoration:underline;}\t  .lnk:hover{color:#333333;text-decoration:none;}\t  .contdtls{font-size:12px; padding-top:8px; padding-bottom:3px; color:#333333; font-weight:bold}\t  .hrcss{color:#CC6600; height:1px; text-align:left; width:450px;}\t </style></head><body>" + strTktCopy + "</body></html>";
            //Mail Ticket copy to user
            //Dim strFileNmPdf As String
            //strFileNmPdf = ConfigurationManager.AppSettings("HTMLtoPDF").ToString().Trim() + OrderId + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf"
            //Dim pdfDoc As iTextSharp.text.Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
            Session["strFileNmPdf"] = ConfigurationManager.AppSettings["HTMLtoPDF"].ToString().Trim() + OrderId + "-" + DateTime.Now.ToString().Replace(":", "").Replace("/", "-").Replace(" ", "-").Trim() + ".pdf";
            //Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 10f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Session["strFileNmPdf"].ToString(), FileMode.Create, FileAccess.ReadWrite, FileShare.None));
            //pdfDoc.Open();
            //StringReader sr = new StringReader(strHTML);
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();
            //writer.Dispose();
            //sr.Dispose();
            ////' ''''''''''''''
            //rightHTML = SaveTextToFile(strHTML, strFileName);
            //strMailMsg = "<p style='font-family:verdana; font-size:12px'>Dear Customer<br /><br />";
            //strMailMsg = strMailMsg + "Greetings of the day !!!!<br /><br />";
            //strMailMsg = strMailMsg + "Please find an attachment for your E-ticket, kindly carry the print out of the same for hassle-free travel. Your onward booking for " + Sector + " is confirmed on " + VC + "-" + FltNo + " for " + DepDate + ". Your airline  booking reference no is " + AirlinePnr + ". <br /><br />";
            //strMailMsg = strMailMsg + "Have a nice &amp; wonderful trip.<br /><br />";
            strMailMsg = strHTML;
            if (BkgStatus == "Ticketed")
            {
                DataTable MailDt = new DataTable();
                MailDt = objSqlDom.GetMailingDetails(MAILING.AIR_BOOKING.ToString(), Session["UID"].ToString()).Tables[0];
                if (rightHTML)
                {
                    objSqlDom.SendMail(EmailId, MailDt.Rows[0]["MAILFROM"].ToString(), MailDt.Rows[0]["BCC"].ToString(), MailDt.Rows[0]["CC"].ToString(), MailDt.Rows[0]["SMTPCLIENT"].ToString(), MailDt.Rows[0]["UserId"].ToString(), MailDt.Rows[0]["Pass"].ToString(), strMailMsg, FT + MailDt.Rows[0]["SUBJECT"].ToString(), Session["strFileNmPdf"].ToString());
                }
                else
                {
                    objSqlDom.SendMail(EmailId, MailDt.Rows[0]["MAILFROM"].ToString(), MailDt.Rows[0]["BCC"].ToString().ToString(), MailDt.Rows[0]["CC"].ToString(), MailDt.Rows[0]["SMTPCLIENT"].ToString(), MailDt.Rows[0]["UserId"].ToString(), MailDt.Rows[0]["Pass"].ToString(), strMailMsg, FT + MailDt.Rows[0]["SUBJECT"].ToString(), "");
                }

            }

        }
        catch (Exception ex)
        {
        }
        return strTktCopy;
    }


    public bool SaveTextToFile(string strData, string FullPath, string ErrInfo = "")
    {
        string Contents = null;
        bool Saved = false;
        System.IO.StreamWriter objReader = default(System.IO.StreamWriter);
        try
        {
            objReader = new System.IO.StreamWriter(FullPath);
            objReader.Write(strData);
            objReader.Close();
            Saved = true;
        }
        catch (Exception Ex)
        {
            ErrInfo = Ex.Message;
        }
        return Saved;
    }


public string TicketCopyExportPDF(string OrderId, string TransID)
{

	string strFileNmPdf = "";
	bool writePDF = false;
	string TktCopy = "";
	int Gtotal = 0;
	int initialAdt = 0;
	int initalChld = 0;
	int initialift = 0;
	decimal MealBagTotalPrice = 0;
	decimal AdtTtlFare = 0;
	decimal ChdTtlFare = 0;
	decimal INFTtlFare = 0;
	decimal fare = 0;

	//Dim OrderId As String = "1c2019deXCP9cVSU"
	//Dim TransID As String = ""


	SqlTransactionDom objTranDom = new SqlTransactionDom();
	SqlTransaction SqlTrasaction = new SqlTransaction();
	SqlTransactionNew objSql = new SqlTransactionNew();
	
	
	DateTime dt = Convert.ToDateTime(Convert.ToString(FltHeaderList.Rows[0]["CreateDate"]));
	string date = dt.ToString("dd/MMM/yyyy").Replace("-", "/");

	string Createddate = date.Split('/')[0] + " " + date.Split('/')[1] + " " + date.Split('/')[2];

	DataRow[] fltmealbag = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables[0].Select("MealPrice>0 or BaggagePrice>0 ");
	fltMealAndBag1 = objSql.Get_MEAL_BAG_FareDetails(OrderId, TransID).Tables[0];
	//.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable()

	if (fltmealbag.Length > 0) {
		fltMealAndBag = fltMealAndBag1.Select("MealPrice>0 or BaggagePrice>0 ").CopyToDataTable();
	}
    string TicketFormate = "";
	try {
		//Dim strAirline As String = "SG6EG8"





        if ((Convert.ToString(FltHeaderList.Rows[0]["Status"]).ToLower().Trim() == "confirm" || Convert.ToString(FltHeaderList.Rows[0]["Status"]).ToLower().Trim() == "confirmbyagent") && Convert.ToString(Session["UserType"]) == "TA")
        {
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 15px; width: 15%; text-align: left; padding: 5px;'>";
			TicketFormate += "<b>Booking Reference No. " + OrderId + "</b>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 14px; width: 15%; text-align: left; padding: 5px;'>";
            if(Convert.ToString(FltHeaderList.Rows[0]["Status"]).ToLower().Trim() == "confirmbyagent")
            {
                TicketFormate += "The PNR-<b>" + FltHeaderList.Rows[0]["GdsPnr"] + " </b>is on <b>HOLD (Hold By Agent)</b> because booking hold by you,Please check transaction in  Hold PNR report and click on  issue hold ticket.After issue hold ticket by you, Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b>+ 91-11-47 677 777 </b> for any further assistance";
            }
            else{
                TicketFormate += "The PNR-<b>" + FltHeaderList.Rows[0]["GdsPnr"] + " </b>is on <b>HOLD</b>. Our operation team is working on it and may take 20 minutes to resolve. Please contact our customer care representative at <b>+ 91-11-47 677 777  </b> for any further assistance";
            }
			
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			//'Devesh
			TicketFormate += "<td>";
			//'Devesh
			TicketFormate += "<table style='border: 1px solid #0b2759; font-family: Verdana, Geneva, sans-serif; font-size: 12px;padding:0px !important;width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>";
			TicketFormate += "Passenger Information";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>";
			TicketFormate += "<table>";

			TicketFormate += "<tr>";
			//TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>"
			// TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
			//TicketFormate += FltHeaderList.Rows[0]["GdsPnr"]
			// TicketFormate += "</td>"
			TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>";
			TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
			TicketFormate += FltHeaderList.Rows[0]["AgencyName"];
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			// TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>"
			// TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>"
			// TicketFormate += FltHeaderList.Rows[0]["AirlinePnr"]
			// TicketFormate += "</td>"
			TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>";
			TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
			TicketFormate += FltagentDetail.Rows[0]["Mobile"];
			TicketFormate += "<br/>";
			TicketFormate += FltagentDetail.Rows[0]["Email"];
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			//TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>"
			//TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>"
			//TicketFormate += IIf(Convert.ToString(FltHeaderList.Rows[0]["Status"]).ToLower().Trim() = "confirm", "Hold", FltHeaderList.Rows[0]["Status"])
			//TicketFormate += "</td>"
			TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>";
			TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
			TicketFormate += Createddate;
			TicketFormate += "</td>";

			TicketFormate += "</tr>";
			for (int p = 0; p <= FltPaxList.Rows.Count - 1; p++) {
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>";
				TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
				TicketFormate += FltPaxList.Rows[p]["Name"] + " " + "(" + FltPaxList.Rows[p]["PaxType"] + ")";
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
				TicketFormate += FltPaxList.Rows[p]["TicketNumber"];
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
			}

			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #fff; width: 100%; padding: 5px;' colspan='4'>";
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; color: #f58220; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>";
			TicketFormate += "Flight Information";
			TicketFormate += "</td>";
			TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='height:5px;'>&nbsp;</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='5' style='background-color: #0b2759;width:100%;'>";
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>FLIGHT</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>DEPART</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>DEPART AIRPORT/TERMINAL</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE AIRPORT/TERMINAL</td>";
			TicketFormate += "</tr>";


			for (int f = 0; f <= FltDetailsList.Rows.Count - 1; f++) {
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";

				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='5' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";

				TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>";
				TicketFormate += FltDetailsList.Rows[f]["AirlineCode"] + " " + FltDetailsList.Rows[f]["FltNumber"];
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += "<img alt='Logo Not Found' src='http://RWT.co/AirLogo/sm" + FltDetailsList.Rows[f]["AirlineCode"] + ".gif' ></img>";
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
				string strDepdt = Convert.ToString(FltDetailsList.Rows[f]["DepDate"]);
				strDepdt = (strDepdt.Length == 8 ? STD.BAL.Utility.Left(strDepdt, 4) + "-" + STD.BAL.Utility.Mid(strDepdt, 4, 2) + "-" + STD.BAL.Utility.Right(strDepdt, 2) : "20" + STD.BAL.Utility.Right(strDepdt, 2) + "-" + STD.BAL.Utility.Mid(strDepdt, 2, 2) + "-" + STD.BAL.Utility.Left(strDepdt, 2));
				DateTime deptdt = Convert.ToDateTime(strDepdt);
				strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/");

				//Response.Write(strDepdt)

				string depDay = Convert.ToString(deptdt.DayOfWeek);
				strDepdt = strDepdt.Split('/')[0] + " " + strDepdt.Split('/')[1] + " " + strDepdt.Split('/')[2];
				string strdeptime = Convert.ToString(FltDetailsList.Rows[f]["DepTime"]).Replace(":", "");
				strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2);
				TicketFormate += strDepdt;
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += strdeptime;
				TicketFormate += "</td>";

				TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
				string strArvdt = Convert.ToString(FltDetailsList.Rows[f]["ArrDate"]);
				strArvdt = (strArvdt.Length == 8 ? STD.BAL.Utility.Left(strArvdt, 4) + "-" + STD.BAL.Utility.Mid(strArvdt, 4, 2) + "-" + STD.BAL.Utility.Right(strArvdt, 2) : "20" + STD.BAL.Utility.Right(strArvdt, 2) + "-" + STD.BAL.Utility.Mid(strArvdt, 2, 2) + "-" + STD.BAL.Utility.Left(strArvdt, 2));
				DateTime Arrdt = Convert.ToDateTime(strArvdt);
				strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/");
				string ArrDay = Convert.ToString(Arrdt.DayOfWeek);
				strArvdt = strArvdt.Split('/')[0] + " " + strArvdt.Split('/')[1] + " " + strArvdt.Split('/')[2];
				string strArrtime = Convert.ToString(FltDetailsList.Rows[f]["ArrTime"]).Replace(":", "");
				strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2);
				TicketFormate += strArvdt;
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += strArrtime;
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
				TicketFormate += FltDetailsList.Rows[f]["DepAirName"] + "( " + FltDetailsList.Rows[f]["DFrom"] + ")";

				TicketFormate += "<br />";
				TicketFormate += "<br />";
				fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows[f]["DFrom"].ToString(), "");
				if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["DepartureTerminal"]))) {
					TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml: NA";
				} else {
					TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["DepartureTerminal"];
				}
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
				TicketFormate += FltDetailsList.Rows[f]["ArrAirName"] + " (" + FltDetailsList.Rows[f]["ATo"] + ")";
				TicketFormate += "<br />";
				TicketFormate += "<br />";
				fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows[f]["ATo"].ToString());
				if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["ArrivalTerminal"]))) {
					TicketFormate += fltTerminalDetails.Rows[0]["ArrvlAirportName"] + " - Trml: NA";
				} else {
					TicketFormate += fltTerminalDetails.Rows[0]["ArrvlAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["ArrivalTerminal"];
				}

				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";

				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='4' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>";
				//TicketFormate += "<img alt='Logo Not Found' src='http://RWT.co/AirLogo/sm" + FltDetailsList.Rows[f]["AirlineCode"] + ".gif' ></img>"
				TicketFormate += "<br/>";
				//TicketFormate += FltDetailsList.Rows[f]["AirlineName"]
				TicketFormate += "</td>";
				TicketFormate += "<td style='width: 32%;'></td>";
				TicketFormate += "<td style='width: 18%; font-size:11px;text-align:left;'></td>";
				TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>";
				TicketFormate += "</tr>";

			}
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			//'Add new tr close Devesh
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			//'Add new tr td Devesh
			TicketFormate += "</tr>";
			//'Add new tr close Devesh
			TicketFormate += "</table>";
			//'Add new table close Devesh

		} else if ((Convert.ToString(FltHeaderList.Rows[0]["Status"]).ToLower().Trim() == "rejected" && Convert.ToString(Session["UserType"]) == "TA")) {
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align:left;font-size:15px;'>";
			TicketFormate += "<b>Booking Reference No. " + OrderId + "</b>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align:left;font-size:14px;'>";
			TicketFormate += "Please re-try the booking.Your booking has been rejected due to some technical issue at airline end.";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";


		} else {
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='width:50%;text-align:left;'>";
			TicketFormate += "<img src='http://RWT.co/images/logo.png' alt='Logo' style='height:54px; width:104px' />";
			TicketFormate += "</td>";
			TicketFormate += "<td style='width: 50%;text-align:right;display:none;'>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td style='width:50%;text-align:left;'>";
			TicketFormate += "";
			TicketFormate += "</td>";
			TicketFormate += "<td style='width: 50%;text-align:right;'>";
			TicketFormate += "";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='width:100%;height:10px;'></td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='2' style='vertical-align:bottom;color:#f58220;text-align:right;width:100%;font-size:16px;font-weight:bold;'>";
			TicketFormate += "Electronic Ticket";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='2' style='height: 2px; width: 100%; border: 1px solid #0b2759'></td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";


			TicketFormate += "<table style='width: 100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='width: 100%; text-align: justify; color: #0b2759; font-size: 11px; padding: 10px;'>";
			TicketFormate += "This is travel itinerary and E-ticket receipt. You may need to show this receipt to enter the airport and/or to show return or onward travel to ";
			TicketFormate += "customs and immigration officials.";
			TicketFormate += "<br />";

			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";
			TicketFormate += "<table style='border: 1px solid #0b2759; font-family: Verdana, Geneva, sans-serif; font-size: 12px;padding:0px !important;width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;' colspan='4'>";
			TicketFormate += "Passenger & Ticket Information";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='font-size:12px; padding: 5px; width: 100%'>";
			TicketFormate += "<table>";

			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>GDS PNR</td>";
			TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
			TicketFormate += FltHeaderList.Rows[0]["GdsPnr"];
			TicketFormate += "</td>";
			TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Issued By</td>";
			TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
			TicketFormate += FltHeaderList.Rows[0]["AgencyName"];
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Airline PNR</td>";
			TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
			TicketFormate += FltHeaderList.Rows[0]["AirlinePnr"];
			TicketFormate += "</td>";
			TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Agency Info</td>";
			TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
			TicketFormate += FltagentDetail.Rows[0]["Mobile"];
			TicketFormate += "<br/>";
			TicketFormate += FltagentDetail.Rows[0]["Email"];
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; padding: 5px;'>Status</td>";
			TicketFormate += "<td style='font-size: 11px; width: 30%; text-align: left; padding: 5px;'>";
			TicketFormate += FltHeaderList.Rows[0]["Status"];
			TicketFormate += "</td>";
			TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Date Of Issue</td>";
			TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
			TicketFormate += Createddate;
			TicketFormate += "</td>";

			TicketFormate += "</tr>";
			for (int p = 0; p <= FltPaxList.Rows.Count - 1; p++) {
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; padding: 5px;'>Passenger Name</td>";
				TicketFormate += "<td style='font-size: 11px; width: 35%; text-align: left; padding: 5px;'>";
				TicketFormate += FltPaxList.Rows[p]["Name"] + " " + "(" + FltPaxList.Rows[p]["PaxType"] + ")";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
			}

			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; background-color: #0b2759; color: #fff; width: 100%; padding: 5px;' colspan='4'>";
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='text-align: left; color: #f58220; font-size: 11px; width: 25%;font-weight:bold;' colspan='1'>";
			TicketFormate += "Flight Information";
			TicketFormate += "</td>";
			TicketFormate += "<td colspan='3' style='font-size: 11px; color: black; font-weight: bold; width: 75%; text-align: left; '></td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='height:5px;'>&nbsp;</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='5' style='background-color: #0b2759;width:100%;'>";
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>FLIGHT</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>DEPART</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 20%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>DEPART AIRPORT/TERMINAL</td>";
			TicketFormate += "<td style='font-size: 10.5px; color: #fff; width: 25%; text-align: left; padding: 4px; font-weight: bold;'>ARRIVE AIRPORT/TERMINAL</td>";
			TicketFormate += "</tr>";


			for (int f = 0; f <= FltDetailsList.Rows.Count - 1; f++) {

				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";

				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='5' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";


				TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>";
				TicketFormate += FltDetailsList.Rows[f]["AirlineCode"] + " " + FltDetailsList.Rows[f]["FltNumber"];
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += "<img alt='Logo Not Found' src='http://RWT.co/AirLogo/sm" + FltDetailsList.Rows[f]["AirlineCode"] + ".gif' ></img>";
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
				string strDepdt = Convert.ToString(FltDetailsList.Rows[f]["DepDate"]);
				//strDepdt = strDepdt.Substring(0, 2) + "-" + strDepdt.Substring(2, 2) + "-" + strDepdt.Substring(4, 2)
				strDepdt = (strDepdt.Length == 8 ? STD.BAL.Utility.Left(strDepdt, 4) + "-" + STD.BAL.Utility.Mid(strDepdt, 4, 2) + "-" + STD.BAL.Utility.Right(strDepdt, 2) : "20" + STD.BAL.Utility.Right(strDepdt, 2) + "-" + STD.BAL.Utility.Mid(strDepdt, 2, 2) + "-" + STD.BAL.Utility.Left(strDepdt, 2));
				DateTime deptdt = Convert.ToDateTime(strDepdt);
				strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/");
				string depDay = Convert.ToString(deptdt.DayOfWeek);
				strDepdt = strDepdt.Split('/')[0] + " " + strDepdt.Split('/')[1] + " " + strDepdt.Split('/')[2];
				string strdeptime = Convert.ToString(FltDetailsList.Rows[f]["DepTime"]);
				strdeptime = strdeptime.Substring(0, 2) + " : " + strdeptime.Substring(2, 2);
				TicketFormate += strDepdt;
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += strdeptime;
				TicketFormate += "</td>";

				TicketFormate += "<td style='font-size: 11px; width: 20%; text-align: left; vertical-align: top;'>";
				string strArvdt = Convert.ToString(FltDetailsList.Rows[f]["ArrDate"]);
				//strArvdt = strArvdt.Substring(0, 2) + "-" + strArvdt.Substring(2, 2) + "-" + strArvdt.Substring(4, 2)
				strArvdt = (strArvdt.Length == 8 ? STD.BAL.Utility.Left(strArvdt, 4) + "-" + STD.BAL.Utility.Mid(strArvdt, 4, 2) + "-" + STD.BAL.Utility.Right(strArvdt, 2) : "20" + STD.BAL.Utility.Right(strArvdt, 2) + "-" + STD.BAL.Utility.Mid(strArvdt, 2, 2) + "-" + STD.BAL.Utility.Left(strArvdt, 2));
				DateTime Arrdt = Convert.ToDateTime(strArvdt);
				strArvdt = Arrdt.ToString("dd/MMM/yy").Replace("-", "/");
				string ArrDay = Convert.ToString(Arrdt.DayOfWeek);
				strArvdt = strArvdt.Split('/')[0] + " " + strArvdt.Split('/')[1] + " " + strArvdt.Split('/')[2];
				string strArrtime = Convert.ToString(FltDetailsList.Rows[f]["ArrTime"]);
				strArrtime = strArrtime.Substring(0, 2) + " : " + strArrtime.Substring(2, 2);
				TicketFormate += strArvdt;
				TicketFormate += "<br/>";
				TicketFormate += "<br/>";
				TicketFormate += strArrtime;
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
				TicketFormate += FltDetailsList.Rows[f]["DepAirName"] + "( " + FltDetailsList.Rows[f]["DFrom"] + ")";

				TicketFormate += "<br />";
				TicketFormate += "<br />";
				fltTerminalDetails = TerminalDetails(OrderId, FltDetailsList.Rows[f]["DFrom"].ToString(), "");
				//if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[0]["DepartureTerminal"])))
				//    TicketFormate += "Terminal:" + fltTerminal.Rows[0]["DepartureTerminal"];
				//else
				if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["DepartureTerminal"]))) {
					TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml: NA";
				} else {
					TicketFormate += fltTerminalDetails.Rows[0]["DepAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["DepartureTerminal"];
				}
				TicketFormate += "</td>";
				TicketFormate += "<td style='font-size: 11px; width: 25%; text-align: left; padding: 2px; font-weight: bold;'>";
				TicketFormate += FltDetailsList.Rows[f]["ArrAirName"] + " (" + FltDetailsList.Rows[f]["ATo"] + ")";
				TicketFormate += "<br />";
				TicketFormate += "<br />";
				//if (!String.IsNullOrEmpty(Convert.ToString(fltTerminal.Rows[f]["ArrivalTerminal"])))
				//    TicketFormate += "Terminal:" + fltTerminal.Rows[f]["ArrivalTerminal"];
				//else
				fltTerminalDetails = TerminalDetails(OrderId, "", FltDetailsList.Rows[f]["ATo"].ToString());

				if (string.IsNullOrEmpty(Convert.ToString(fltTerminalDetails.Rows[0]["ArrivalTerminal"]))) {
					TicketFormate += fltTerminalDetails.Rows[0]["ArrvlAirportName"] + " - Trml: NA";
				} else {
					TicketFormate += fltTerminalDetails.Rows[0]["ArrvlAirportName"] + " - Trml:" + fltTerminalDetails.Rows[0]["ArrivalTerminal"];
				}
				TicketFormate += "</td>";

				TicketFormate += "</tr>";
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";

				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='4' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 11px; width: 322%; text-align: left; font-weight:bold;'>";
				//TicketFormate += "<img alt='Logo Not Found' src='http://RWT.co/AirLogo/sm" + FltDetailsList.Rows[f]["AirlineCode"] + ".gif' ></img>"
				TicketFormate += "<br/>";
				//TicketFormate += FltDetailsList.Rows[f]["AirlineName"]
				TicketFormate += "</td>";
				TicketFormate += "<td style='width: 32%;'></td>";
				TicketFormate += "<td style='width: 18%; font-size:11px;text-align:left;'></td>";
				TicketFormate += "<td style='width: 18%; font-size: 11px; text-align: left; font-weight: bold;'></td>";
				TicketFormate += "</tr>";

			}
			TicketFormate += "</table>";
			TicketFormate += "</td>";

			TicketFormate += "</tr>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='background-color: #0b2759; color: #f58220;font-size:11px;font-weight:bold; padding: 5px;'>";
			TicketFormate += "Fare Information";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			if (string.IsNullOrEmpty(TransID) || TransID == null) {
				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='8' style= 'background-color: #0b2759;width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Type</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Pax Count</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Base fare</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Fuel Surcharge</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Tax</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>STax</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>Trans Fee</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trans Charge</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>TOTAL</td>";
				TicketFormate += "</tr>";
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";


				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='8' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";

				for (int fd = 0; fd <= fltFare.Rows.Count - 1; fd++) {
					if (fltFare.Rows[fd]["PaxType"].ToString() == "ADT" && initialAdt == 0) {
						int numberOfADT = FltPaxList.AsEnumerable().Where(x => x["PaxType"].ToString() == "ADT").ToList().Count;
						TicketFormate += "<tr>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += fltFare.Rows[fd]["PaxType"];
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_adtcnt'>" + numberOfADT + "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["BaseFare"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Fuel"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'id='td_taxadt'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Tax"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["ServiceTax"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TFee"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_tcadt'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TCharge"]) * numberOfADT).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;' id='td_adttot'>";
						AdtTtlFare = Convert.ToDecimal(fltFare.Rows[fd]["Total"]) * numberOfADT;
						TicketFormate += AdtTtlFare.ToString();
						TicketFormate += "</td>";

						TicketFormate += "</tr>";

						initialAdt += 1;
					}

					if (fltFare.Rows[fd]["PaxType"].ToString() == "CHD" && initalChld == 0) {
						int numberOfCHD = FltPaxList.AsEnumerable().Where(x => x["PaxType"].ToString() == "CHD").ToList().Count;
						TicketFormate += "<tr>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += fltFare.Rows[fd]["PaxType"];
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_chdcnt'>" + numberOfCHD + "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["BaseFare"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Fuel"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'id='td_taxchd'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Tax"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["ServiceTax"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TFee"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_tcchd'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TCharge"]) * numberOfCHD).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_chdtot'>";
						ChdTtlFare = Convert.ToDecimal(fltFare.Rows[fd]["Total"]) * numberOfCHD;
						TicketFormate += ChdTtlFare.ToString();
						TicketFormate += "</td>";

						TicketFormate += "</tr>";

						initalChld += 1;
					}
					if (fltFare.Rows[fd]["PaxType"].ToString() == "INF" && initialift == 0) {
						int numberOfINF = FltPaxList.AsEnumerable().Where(x => x["PaxType"].ToString() == "INF").ToList().Count;
						TicketFormate += "<tr>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += fltFare.Rows[fd]["PaxType"];
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;' id='td_infcnt'>" + numberOfINF + "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["BaseFare"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Fuel"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["Tax"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["ServiceTax"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TFee"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
						TicketFormate += (Convert.ToDecimal(fltFare.Rows[fd]["TCharge"]) * numberOfINF).ToString();
						TicketFormate += "</td>";
						TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'id='td_Inftot'>";
						INFTtlFare = Convert.ToDecimal(fltFare.Rows[fd]["Total"]) * numberOfINF;
						TicketFormate += INFTtlFare.ToString();
						TicketFormate += "</td>";
						TicketFormate += "</tr>";
						initialift += 1;

					}
				}
				fare = AdtTtlFare + ChdTtlFare + INFTtlFare;
			} else {
				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='2' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";


				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top; display:none;'id='td_perpaxtype'>" + FltPaxList.Rows[0]["PaxType"] + "</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Base Fare</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
				TicketFormate += fltFare.Rows[0]["BaseFare"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Fuel Surcharge</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
				TicketFormate += fltFare.Rows[0]["Fuel"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Tax</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;' id='td_perpaxtax'>";
				TicketFormate += fltFare.Rows[0]["Tax"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>STax</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
				TicketFormate += fltFare.Rows[0]["ServiceTax"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Fee</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
				TicketFormate += fltFare.Rows[0]["TFee"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Trans Charge</td>";
				TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'id='td_perpaxtc'>";
				TicketFormate += fltFare.Rows[0]["TCharge"].ToString();
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
				TicketFormate += "<tr>";

				decimal ResuCharge = 0;
				decimal ResuServiseCharge = 0;
				decimal ResuFareDiff = 0;
				if (Convert.ToString(FltHeaderList.Rows[0]["ResuCharge"]) != null && !string.IsNullOrEmpty(Convert.ToString(FltHeaderList.Rows[0]["ResuCharge"]))) {
					TicketFormate += "<tr>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Charge</td>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
					TicketFormate += FltHeaderList.Rows[0]["ResuCharge"].ToString();
					ResuCharge = Convert.ToDecimal(FltHeaderList.Rows[0]["ResuCharge"]);
					TicketFormate += "</td>";
					TicketFormate += "</tr>";
				}
				if (Convert.ToString(FltHeaderList.Rows[0]["ResuServiseCharge"]) != null && !string.IsNullOrEmpty(Convert.ToString(FltHeaderList.Rows[0]["ResuServiseCharge"]))) {
					TicketFormate += "<tr>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Srv. Charge</td>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
					TicketFormate += FltHeaderList.Rows[0]["ResuServiseCharge"].ToString();
					ResuServiseCharge = (Convert.ToDecimal(FltHeaderList.Rows[0]["ResuServiseCharge"].ToString()));
					TicketFormate += "</td>";
					TicketFormate += "</tr>";
				}
				if (Convert.ToString(FltHeaderList.Rows[0]["ResuFareDiff"]) != null && !string.IsNullOrEmpty(Convert.ToString(FltHeaderList.Rows[0]["ResuFareDiff"]))) {
					TicketFormate += "<tr>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>Reissue Fare Diff</td>";
					TicketFormate += "<td style='font-size: 10px; width: 50%; text-align: left; vertical-align: top;'>";
					TicketFormate += FltHeaderList.Rows[0]["ResuFareDiff"].ToString();
					ResuFareDiff = (Convert.ToDecimal(FltHeaderList.Rows[0]["ResuFareDiff"].ToString()));
					TicketFormate += "</td>";
					TicketFormate += "</tr>";
				}
				TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;'>TOTAL</td>";
				TicketFormate += "<td style='font-size: 11px; width: 50%; text-align: left; vertical-align: top;' id='td_totalfare'>";
				fare = Convert.ToDecimal(fltFare.Rows[0]["BaseFare"].ToString()) + Convert.ToDecimal(fltFare.Rows[0]["Fuel"].ToString()) + Convert.ToDecimal(fltFare.Rows[0]["Tax"].ToString()) + Convert.ToDecimal(fltFare.Rows[0]["ServiceTax"].ToString()) + Convert.ToDecimal(fltFare.Rows[0]["TCharge"].ToString()) + Convert.ToDecimal(fltFare.Rows[0]["TFee"].ToString()) + ResuCharge + ResuServiseCharge + ResuFareDiff;
				TicketFormate += fare.ToString();
				TicketFormate += "</td>";

				//fare = Convert.ToDecimal(fltFare.Rows[0]["Total"]) + ResuCharge + ResuServiseCharge + ResuFareDiff;
				TicketFormate += "</tr>";
			}
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			if (fltMealAndBag.Rows.Count > 0) {
				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='7' style= 'background-color: #0b2759;width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";
				TicketFormate += "<tr>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Pax Name</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Trip Type</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Code</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Meal Price</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Code</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 15%; text-align: left; padding: 4px; font-weight: bold;'>Baggage Price</td>";
				TicketFormate += "<td style='font-size: 10px; color: #fff; width: 10%; text-align: left; padding: 4px; font-weight: bold;'>TOTAL</td>";
				TicketFormate += "</tr>";
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";

				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='7' style='width:100%;'>";
				TicketFormate += "<table style='width:100%;'>";


				for (int i = 0; i <= fltMealAndBag.Rows.Count - 1; i++) {
					//If Convert.ToString(fltMealAndBag.Rows[i]["MealPrice"]) <> "0.00" AndAlso Convert.ToString(fltMealAndBag.Rows[i]["BaggagePrice"]) <> "0.00" Then
					TicketFormate += "<tr>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["Name"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["TripType"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["MealCode"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["MealPrice"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["BaggageCode"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 15%; text-align: left; vertical-align: top;'>";
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["BaggagePrice"]);
					TicketFormate += "</td>";
					TicketFormate += "<td style='font-size: 11px; width: 10%; text-align: left; vertical-align: top;'>";
					MealBagTotalPrice += Convert.ToDecimal(fltMealAndBag.Rows[i]["TotalPrice"]);
					TicketFormate += Convert.ToString(fltMealAndBag.Rows[i]["TotalPrice"]);
					TicketFormate += "</td>";

					TicketFormate += "</tr>";
					//End If
				}
				TicketFormate += "</table>";
				TicketFormate += "</td>";
				TicketFormate += "</tr>";
			}



			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='background-color: #0b2759; color:#fdc42c;font-size:11px;font-weight:bold; padding: 5px;'>";
			TicketFormate += "<table style='width:100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>";
			TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>";
			TicketFormate += "<td style='font-size: 10px; width: 20%; text-align: left; vertical-align: top;'></td>";
			TicketFormate += "<td style='font-size: 10px; width: 15%; text-align: left; vertical-align: top;'></td>";
			TicketFormate += "<td style='color: #fff; font-size: 10px; width: 15%; text-align: left; vertical-align: top;'>GRAND TOTAL</td>";
			TicketFormate += "<td style='color: #fff; font-size: 10px; width: 10%; text-align: left; vertical-align: top;'id='td_grandtot'>";
			TicketFormate += (fare + MealBagTotalPrice).ToString();
			TicketFormate += "</td>";

			TicketFormate += "</tr>";
			TicketFormate += "</table>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<br/><br/>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4'>";
			TicketFormate += "<ul style='list-style-image: url(http://RWT.co/Images/bullet.png);'>";
			TicketFormate += "<li style='font-size:10.5px;'>Kindly confirm the status of your PNR within 24 hrs of booking, as at times the same may fail on account of payment failure, internet connectivity, booking engine or due to any other reason beyond our control.";
			TicketFormate += "For Customers who book their flights well in advance of the scheduled departure date it is necessary that you re-confirm the departure time of your flight between 72 and 24 hours before the Scheduled Departure Time.</li>";
			TicketFormate += "</ul>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='background-color: #0b2759; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;'>TERMS AND CONDITIONS :</td>";
			TicketFormate += "</tr>";

			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4'>";
			TicketFormate += "<ul style='list-style-image: url(http://RWT.co/Images/bullet.png);'>";
			TicketFormate += "<li style='font-size:10.5px;'>Guests are requested to carry their valid photo identification for all guests, including children.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>We recommend check-in at least 2 hours prior to departure.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>Boarding gates close 45 minutes prior to the scheduled time of departure. Please report at your departure gate at the indicated boarding time. Any passenger failing to report in time may be refused boarding privileges.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>Cancellations and Changes permitted more than two (2) hours prior to departure with payment of change fee and difference in fare if applicable only in working hours (10:00 am to 06:00 pm) except Sundays and Holidays.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>";
			TicketFormate += "Flight schedules are subject to change and approval by authorities.";
			TicketFormate += "<br />";
			TicketFormate += "</li>";
			TicketFormate += "<li style='font-size:10.5px;'>";
			TicketFormate += "Name Changes on a confirmed booking are strictly prohibited. Please ensure that the name given at the time of booking matches as mentioned on the traveling Guests valid photo ID Proof.";
			TicketFormate += "<br />";
			TicketFormate += " Travel Agent does not provide compensation for travel on other airlines, meals, lodging or ground transportation.";
			TicketFormate += "</li>";
			TicketFormate += "<li style='font-size:10.5px;'>Bookings made under the Armed Forces quota are non cancelable and non- changeable.</li>";

			TicketFormate += "<li style='font-size:10.5px;'>Guests are advised to check their all flight details (including their Name, Flight numbers, Date of Departure, Sectors) before leaving the Agent Counter.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>Cancellation amount will be charged as per airline rule.</li>";
			TicketFormate += "<li style='font-size:10.5px;'>Guests requiring wheelchair assistance, stretcher, Guests traveling with infants and unaccompanied minors need to be booked in advance since the inventory for these special service requests are limited per flight.</li>";
			TicketFormate += "</ul>";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
			TicketFormate += "</table>";
			TicketFormate += "<table style='width: 100%;'>";
			TicketFormate += "<tr>";
			TicketFormate += "<td colspan='4' style='background-color: #0b2759; color: #f58220; font-size: 11px; font-weight: bold; padding: 5px;'>BAGGAGE INFORMATION :";
			TicketFormate += "</td>";
			TicketFormate += "</tr>";
            Boolean Bag = false;
            if (!string.IsNullOrEmpty(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"])))
            {
                Bag = Convert.ToBoolean(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["IsBagFare"]));
            }

			DataTable dtbaggage = new DataTable();
            dtbaggage = objTranDom.GetBaggageInformation("D", FltHeaderList.Rows[0]["VC"].ToString(), Bag).Tables[0];
			string bginfo = GetBagInfo(Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["Provider"]), Convert.ToString(SelectedFltDS.Tables[0].Rows[0]["AirlineRemark"]));


			if (string.IsNullOrEmpty(bginfo)) {

				foreach (DataRow  drbg_loopVariable in dtbaggage.Rows) {
					var drbg = drbg_loopVariable;
					TicketFormate += "<tr>";
					TicketFormate += "<td colspan='2'>" + drbg_loopVariable["BaggageName"] + "</td>";
					TicketFormate += "<td colspan='2'>" + drbg_loopVariable["Weight"] + "</td>";
					TicketFormate += "</tr>";
				}


			} else {
				TicketFormate += "<tr>";
				TicketFormate += "<td colspan='2'></td>";
				TicketFormate += "<td colspan='2'>" + bginfo + "</td>";
				TicketFormate += "</tr>";

			}




			TicketFormate += "</table>";

		}

		
		
	} catch (Exception ex) {
		clsErrorLog.LogInfo(ex);
	}

    return TicketFormate; 


}


    public string GetBagInfo(string Provider, string Remark)
    {

        string baginfo = "";

        if (Provider == "TB")
        {
            if (Remark.Contains("Hand"))
            {
                baginfo = Remark;

            }

        }
        else if (Provider == "YA")
        {
            if (Remark.Contains("Hand"))
            {
                baginfo = Remark;


            }
            else if (!string.IsNullOrEmpty(Remark))
            {
                baginfo = Remark + " Baggage allowance";

            }



        }
        else if (Provider == "1G")
        {

            if (Remark.Contains("PC"))
            {
                baginfo = Remark.Replace("PC", " Piece(s) Baggage allowance");

            }
            else if (Remark.Contains("K"))
            {
                baginfo = Remark.Replace("K", " Kg Baggage allowance");

            }



        }
        return baginfo;

    }

    
    public DataTable SelectPaxDetail(string OrderId, string TID)
    {
        SqlDataAdapter adap = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(Variables.Constr);
        if (string.IsNullOrEmpty(TID))
        {
            DataTable dt = new DataTable();

            adap = new SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" + OrderId + "' ", con);
            //adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            adap.Fill(dt);

            return dt;
        }
        else
        {
            DataTable dt = new DataTable();
            adap = new SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB,FFNumber,FFAirline,MealType,SeatType FROM   FltPaxDetails WHERE OrderId = '" + OrderId + "' and PaxId= '" + TID + "' ", con);
            //adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            adap.Fill(dt);
            return dt;
        }
    }



    public DataTable TerminalDetails(string OrderID, string DepCity, string ArrvlCity)
    {
        SqlConnection con1 = new SqlConnection(Variables.Constr);
        SqlDataAdapter adap = new SqlDataAdapter("USP_TERMINAL_INFO", con1);
        adap.SelectCommand.CommandType = CommandType.StoredProcedure;
        adap.SelectCommand.Parameters.AddWithValue("@DEPARTURECITY", DepCity);
        adap.SelectCommand.Parameters.AddWithValue("@ARRIVALCITY", ArrvlCity);
        adap.SelectCommand.Parameters.AddWithValue("@ORDERID", OrderID);
        DataTable dt1 = new DataTable();
        con1.Open();
        adap.Fill(dt1);
        con1.Close();
        return dt1;
    }

   



}