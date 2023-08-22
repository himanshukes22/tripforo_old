using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
//using System.Web.UI.HtmlControls;
using ITZLib;


public partial class BS_CustomerInfo : System.Web.UI.Page
{
    Itz_Trans_Dal objItzT = new Itz_Trans_Dal();
    ITZ_Trans objIzT = new ITZ_Trans();
    GetBalanceResponse objBalResp = new GetBalanceResponse();
    BS_SHARED.SHARED shared; BS_BAL.SharedBAL sharedbal; GsrtcService gsrtcservice;
    EXCEPTION_LOG.ErrorLog erlog = new EXCEPTION_LOG.ErrorLog();
    DataColumn col = null; string amount = ""; decimal totfare = 0; string amount1 = ""; decimal totfare1 = 0; decimal srvChrg1 = 0; decimal ACsvcCharge = 0;
    string seat = ""; decimal srvChrg = 0; decimal tatotfare = 0; decimal tatotfare1 = 0;
    decimal tanetfare = 0; decimal tds = 0; decimal com = 0; decimal tanetfare1 = 0; decimal tds1 = 0; decimal com1 = 0;
    public static string UserID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        hideGs.Visible = false; idehide.Visible = false;
        string mytable = "";
        List<BS_SHARED.SHARED> list = new List<BS_SHARED.SHARED>();
        List<BS_SHARED.SHARED> breakupList = new List<BS_SHARED.SHARED>();
        try
        {
            UserID = Convert.ToString(Session["UID"]);
            if (!IsPostBack)
            {
                shared = new BS_SHARED.SHARED(); sharedbal = new BS_BAL.SharedBAL();
                if (Request.QueryString["ID"] != null || Request.QueryString["ID"] != "")
                {
                    shared.orderID = Request.QueryString["ID"].ToString();
                    list = sharedbal.getSelected_SeatDetails(shared);
                    Session["PaxList"] = list;
                    List<BS_SHARED.SHARED> catogarylist = new List<BS_SHARED.SHARED>();
                    //gsrtcservice = new GsrtcService();


                    BindPax(list);
                    string farebreakup = "";
                    string farebreakup1 = ""; string farebreakup2 = "";
                    farebreakup += "<div class='f16 colorp bld'></div><div class='clear1'></div><div class='clear1'></div>";
                    for (int g = 0; g < list.Count; g++)
                    {
                        shared.traveler = list[g].traveler;
                        hidprovider.Value = list[g].provider_name;
                        if (list[g].provider_name == "GS")
                            pPaxId.Visible = false;
                        else
                            pPaxId.Visible = true;
                        if (list.Count > 0)
                        {
                            string[] strsrc = list[g].src.Split('(');
                            string[] strdest = list[g].dest.Split('(');
                            mytable += "<div class='w100 lft brdr'><div class='f16 colorp lft w45 p3'><b>" + ConvertTo_ProperCase(strsrc[0]) + " <img src='Images/arrow.png' />&nbsp;" + ConvertTo_ProperCase(strdest[0]) + "</b></div>";
                            string[] strbdpoint = list[g].boardpoint.Split('&');
                            string[] strdrpoint = list[g].droppoint.Split('&');
                            if (g == 0)
                                mytable += "<div class='lft w25 p3' style=''><div><b>Journey Date:</b>" + list[g].journeyDate.Trim().Split('-')[2] + "-" + list[g].journeyDate.Trim().Split('-')[1] + "-" + list[g].journeyDate.Trim().Split('-')[0] + " " + strbdpoint[0].Trim().Substring(strbdpoint[0].Trim().LastIndexOf('(') + 1).Replace(')', ' ') + "</div></div>";
                            else
                                mytable += "<div class='rgt w25' style=''><div><b>Return Date:</b>" + list[g].journeyDate.Trim().Split('-')[2] + "-" + list[g].journeyDate.Trim().Split('-')[1] + "-" + list[g].journeyDate.Trim().Split('-')[0] + " " + strbdpoint[0].Trim().Substring(strbdpoint[0].Trim().LastIndexOf('(') + 1).Replace(')', ' ') + "</div></div>";
                            mytable += "<div class='clear1'></div>";
                            mytable += "<div class='lft w45 lft padding1s' style=''><div class='bld'><b>Boarding Point:</b>" + strbdpoint[0] + "</div></div>";
                            if (strdrpoint[0].ToString() != "Not available")
                            {
                                mytable += "<div class='lft w45 padding1s' style=''><div class='bld'><b>Dropping Point:</b>" + strdrpoint[0] + "</div></div>";
                            }

                            mytable += "<div class='clear1'></div>";
                            mytable += "</div>";
                            divUpper.InnerHtml = mytable;
                            if (g == 0)
                            {
                                for (int a = 0; a < list[0].originalfare.Split(',').Length; a++)
                                {
                                    amount += list[0].originalfare.Split(',')[a].Trim() + ",";
                                    totfare += Convert.ToDecimal(list[g].originalfare.Split(',')[a].Trim());
                                    if (list[0].provider_name == "ES")
                                        ACsvcCharge += Convert.ToDecimal(list[0].WithTaxes.Split('*')[a]);
                                    //seat += list[g].originalfare.Split(',')[a].Trim() + ",";
                                }
                                amount = amount.Remove(amount.LastIndexOf(","));
                                //   seat = seat.Remove(seat.LastIndexOf(","));
                                shared.fare = Convert.ToString(amount);
                                shared.totWithTaxes = list[0].totWithTaxes;
                                shared.agentID = list[0].agentID.Trim();
                                shared.provider_name = list[0].provider_name.Trim();
                                if (shared.provider_name == "GS")
                                {
                                    for (int i = 0; i < shared.fare.Split(',').Length; i++)
                                    {
                                        srvChrg = 0;
                                        tatotfare += Convert.ToDecimal(shared.fare.Split(',')[i]);
                                        tanetfare += Convert.ToDecimal(shared.fare.Split(',')[i]);
                                        tds = 0;
                                        com = 0;
                                    }
                                }
                                else
                                {
                                    breakupList = sharedbal.getCommissionList(shared);
                                    for (int i = 0; i <= breakupList.Count - 1; i++)
                                    {
                                        srvChrg += breakupList[i].serviceChrg;
                                        tatotfare += breakupList[i].taTotFare;
                                        tanetfare += breakupList[i].taNetFare;
                                        tds += breakupList[i].taTds;
                                        com += breakupList[i].adcomm;
                                    }
                                    tatotfare += ACsvcCharge;
                                    tanetfare += ACsvcCharge;
                                }

                                //breakupList = sharedbal.getCommissionList(shared);
                                //for (int i = 0; i <= breakupList.Count - 1; i++)
                                //{
                                //    srvChrg += breakupList[i].serviceChrg;
                                //    tatotfare += breakupList[i].taTotFare;
                                //    tanetfare += breakupList[i].taNetFare;
                                //    tds += breakupList[i].taTds;
                                //    com += breakupList[i].adcomm;
                                //}
                                //farebreakup1 += "<div class='bld colorp textunderline'>OneWay Breakup</div>";
                                //farebreakup1 += "<div class='clear1'></div>";
                                farebreakup1 += "<div class='lft'>Fare</div><div class='rgt'>" + Math.Round(totfare, 0) + "</div>";
                                farebreakup1 += "<div class='clear'></div>";
                                farebreakup1 += "<div class='lft'>Service Charge</div><div  class='rgt'>" + srvChrg + "</div>";
                                farebreakup1 += "<div class='clear'></div>";
                                farebreakup1 += "<div class='lft'>A/C Service Tax</div><div  class='rgt'>" + ACsvcCharge + "</div>";
                                farebreakup1 += "<div class='clear'></div>";
                                farebreakup1 += "<div class='lft'>PG Charge</div><div class='rgt' id='PgCharge'> 0.00</div>";
                                farebreakup1 += "<div class='clear'></div>";
                                HdnOrgTotalFare.Value = Convert.ToString(tatotfare);
                                HdnOrgPayAmount.Value = Convert.ToString(tatotfare);
                                HdnOrgNetFare.Value = Convert.ToString(tanetfare);
                                farebreakup1 += "<div class='lft'><a href='#' class='brk' rel='" + com + "," + tds + "," + tanetfare + "'><span class='bld blue'><b>Total Fare</b></span></a></div><div class='rgt f16 bld' id='divTotalFare'>" + tatotfare + "</div><div class='clear1'></div>";
                                farebreakup += farebreakup1;
                            }
                            else
                            {
                                for (int a = 0; a < list[1].originalfare.Split(',').Length; a++)
                                {
                                    amount1 += list[1].originalfare.Split(',')[a].Trim() + ",";
                                    totfare1 += Convert.ToDecimal(list[g].originalfare.Split(',')[a].Trim());
                                    if (list[1].provider_name == "ES")
                                        ACsvcCharge += Convert.ToDecimal(list[1].WithTaxes.Split('*')[a]);
                                    //seat += list[g].originalfare.Split(',')[a].Trim() + ",";
                                }
                                amount1 = amount1.Remove(amount1.LastIndexOf(","));
                                shared.fare = Convert.ToString(amount1);
                                shared.totWithTaxes = list[1].totWithTaxes;
                                shared.agentID = list[1].agentID.Trim();
                                shared.provider_name = list[1].provider_name.Trim();
                                if (shared.provider_name == "GS")
                                {
                                    for (int i = 0; i < shared.fare.Split(',').Length; i++)
                                    {
                                        srvChrg1 = 0;
                                        tatotfare1 += Convert.ToDecimal(shared.fare.Split(',')[i]);
                                        tanetfare1 += Convert.ToDecimal(shared.fare.Split(',')[i]);
                                        tds1 = 0;
                                        com1 = 0;
                                    }
                                }
                                else
                                {
                                    breakupList = sharedbal.getCommissionList(shared);
                                    for (int i = 0; i <= breakupList.Count - 1; i++)
                                    {
                                        srvChrg1 += breakupList[i].serviceChrg;
                                        tatotfare1 += breakupList[i].taTotFare;
                                        tanetfare1 += breakupList[i].taNetFare;
                                        tds1 += breakupList[i].taTds;
                                        com1 += breakupList[i].adcomm;
                                    }
                                }
                                //breakupList = sharedbal.getCommissionList(shared);
                                //for (int i = 0; i <= breakupList.Count - 1; i++)
                                //{
                                //    srvChrg1 += breakupList[i].serviceChrg;
                                //    tatotfare1 += breakupList[i].taTotFare;
                                //    tanetfare1 += breakupList[i].taNetFare;
                                //    tds1 += breakupList[i].taTds;
                                //    com1 += breakupList[i].adcomm;
                                //}

                                farebreakup2 += "<div>";
                                farebreakup2 += "<div class='bld colorp textunderline'>Return Breakup</div>";
                                farebreakup2 += "<div class='clear1'></div>";
                                farebreakup2 += "<div class='lft'>Fare</div><div class='rgt'>" + Math.Round(totfare1, 0) + "</div>";
                                farebreakup2 += "<div class='clear'></div>";
                                farebreakup2 += "<div class='lft'>Service Charge</div><div  class='rgt'>" + srvChrg1 + "</div>";
                                farebreakup2 += "<div class='clear'></div>";
                                farebreakup2 += "<div class='lft'><a href='#' class='brk' rel='" + com1 + "," + tds1 + "," + tanetfare1 + "'><span class='colorp'><b>Totel Fare</b></span></a></div><div class='rgt f16 bld'>" + tatotfare1 + "</div>";
                                farebreakup2 += "</div>";
                                farebreakup += farebreakup2;
                            }

                        }
                        if (list[g].idproofReq.ToUpper() == "TRUE")
                        {
                            tdid1.Visible = true;
                            tdid2.Visible = true;
                        }
                        var lseets = list[g].ladiesSeat.Split(',');
                        int st = 0;
                        foreach (RepeaterItem rpt in rep_Pax.Items)
                        {
                            DropDownList dpgender = (DropDownList)rpt.FindControl("dpgender");
                            DropDownList dptitle = (DropDownList)rpt.FindControl("dptitle");
                            if (list.Count > 1)
                            {
                                if (list[0].ladiesSeat.Split(',')[st] == "true" || list[1].ladiesSeat.Split(',')[st] == "true")
                                    lseets[st] = "true";
                            }

                            if (lseets[st] == "true")
                            {
                                dpgender.SelectedValue = "F";
                                dptitle.SelectedValue = "Ms";
                                dpgender.SelectedItem.Text = "Female";
                                dptitle.SelectedItem.Text = "Ms";
                                dptitle.Enabled = false;
                                dpgender.Enabled = false;
                            }
                            else
                            {
                                dpgender.SelectedValue = "M";
                                dptitle.SelectedValue = "select";
                                dpgender.SelectedItem.Text = "Male";
                                dptitle.SelectedItem.Text = "Title";
                            }
                            st = st + 1;
                        }
                    }
                    farebreakup += "<div class='clear1'></div><hr /><div class='clear1'></div>";
                    HdnOrgPayAmount.Value = Convert.ToString(Convert.ToDecimal(tatotfare + tatotfare1));
                    HdnOrgNetFare2.Value = Convert.ToString(Convert.ToDecimal(tanetfare + tanetfare1));
                    //<img src='../images/rsp.png' />
                    farebreakup += "<div class='colorp f16'><a href='#' class='brknet' rel='" + Convert.ToDecimal(tanetfare + tanetfare1) + "' class='lft colorp'><b>Pay Amount</b></a><span class='rgt f20' id='SpanPayAmount'>" + Convert.ToDecimal(tatotfare + tatotfare1) + "</span></div>";
                    farebreakup += "</div>";
                    divfarebrk.InnerHtml = farebreakup;
                }

            }
            #region Check Staff Service Enable or Not
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["LoginByStaff"])) && Convert.ToString(Session["LoginByStaff"]).ToUpper() == "TRUE")
                {
                    btnbook.Visible = false;
                    if (Convert.ToString(Session["BUS"]) == "True")
                    {
                        btnbook.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            { }
            #endregion
        }
        catch (Exception ex)
        {
            erlog = new EXCEPTION_LOG.ErrorLog();
            erlog.writeErrorLog(ex, "CustomerInfo.aspx.cs");
        }
    }



    private void BindPax(List<BS_SHARED.SHARED> list)
    {
        DataTable paxdt = new DataTable();
        col = new DataColumn();
        col.ColumnName = "PaxTP";
        paxdt.Columns.Add(col);
        if (list.Count > 1)
        {
            ddddd.InnerText = "R";
            col = new DataColumn();
            col.ColumnName = "seat";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "fare";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "seat1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "fare1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Originalfare";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ladiesSeat";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Originalfare1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ladiesSeat1";
            paxdt.Columns.Add(col);
        }
        else
        {
            ddddd.InnerText = "O";
            col = new DataColumn();
            col.ColumnName = "seat";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "fare";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "seat1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "fare1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Originalfare";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ladiesSeat";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Originalfare1";
            paxdt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ladiesSeat1";
            paxdt.Columns.Add(col);
        }


        for (int k = 0; k <= Convert.ToInt32(list[0].NoOfPax) - 1; k++)
        {
            DataRow dr = paxdt.NewRow(); string[] strSeat = null; string[] strFare = null; string[] strladies = null;
            string[] strSeat1 = null; string[] strFare1 = null;
            //if (list[0].seat.IndexOf(",") > 0 && list[0].fare.IndexOf(",") > 0)
            //{
            strSeat = list[0].seat.Split(',');
            strFare = list[0].fare.Split(',');
            strladies = list[0].ladiesSeat.Split(',');
            if (list.Count > 1)
            {
                dr["PaxTP"] = "Passenger : " + Convert.ToInt32(k + 1) + "<span class='span1'>Return</span><span class='span2'>Oneway</span>";
                strSeat1 = list[1].seat.Split(',');
                strFare1 = list[1].fare.Split(',');
                for (int ll = 0; ll < list.Count; ll++)
                {
                    if (list[ll].ladiesSeat.Split(',')[k].Trim() == "true")
                    {
                        if (ll == 0)
                        {
                            dr["seat"] = strSeat[k].Trim();
                            dr["fare"] = strFare[k].Trim();
                            dr["ladiesSeat"] = "Images/3.png";
                        }
                        else
                        {
                            dr["seat1"] = strSeat1[k].Trim();
                            dr["fare1"] = strFare1[k].Trim();
                            dr["ladiesSeat1"] = "Images/3.png";
                        }
                    }
                    else
                    {
                        if (ll == 0)
                        {
                            dr["seat"] = strSeat[k].Trim();
                            dr["fare"] = strFare[k].Trim();
                            dr["ladiesSeat"] = "Images/2.png";
                        }
                        else
                        {
                            dr["seat1"] = strSeat1[k].Trim();
                            dr["fare1"] = strFare1[k].Trim();
                            dr["ladiesSeat1"] = "Images/2.png";
                        }
                    }
                }
            }
            else
            {
                dr["PaxTP"] = "Passenger : " + Convert.ToInt32(k + 1);

                if (strladies[k].Trim() == "true")
                {
                    dr["seat"] = strSeat[k].Trim();
                    dr["fare"] = strFare[k].Trim();
                    dr["ladiesSeat"] = "Images/3.png";

                }
                else
                {
                    dr["seat"] = strSeat[k].Trim();
                    dr["fare"] = strFare[k].Trim();
                    dr["ladiesSeat"] = "Images/2.png";
                }

            }

            paxdt.Rows.Add(dr);
        }

        rep_Pax.DataSource = paxdt;
        rep_Pax.DataBind();
    }
    protected void btnbook_Click(object sender, EventArgs e)
    {
        string strExistsMessage = ""; string strmessage = ""; string strTinNo = ""; string strOrderId = ""; string GsResponse1 = ""; string GsResponse2 = "";
        // for check duplicate orderid
        strExistsMessage = Check_Exists_OrderId(Request.QueryString["ID"].ToString());
        // return empity or message  "please try again........."

        if (strExistsMessage == "")
        {
            sharedbal = new BS_BAL.SharedBAL();
            List<BS_SHARED.SHARED> inventroyList = new List<BS_SHARED.SHARED>();
            inventroyList = (List<BS_SHARED.SHARED>)Session["PaxList"];
            bool flag = false;
            try
            {
                #region[Set property value]
                for (int p = 0; p < inventroyList.Count; p++)
                {
                    shared = new BS_SHARED.SHARED(); shared.paxname = new List<string>(); shared.paxage = new List<string>();
                    shared.title = new List<string>(); shared.gender = new List<string>();
                    shared.paxseat = new List<string>(); shared.perFare = new List<string>();
                    shared.boardingId = new List<string>(); shared.boardinglocation = new List<string>();
                    shared.perOriginalFare = new List<string>();
                    List<BS_SHARED.SHARED> farelist = new List<BS_SHARED.SHARED>();
                    List<BS_SHARED.SHARED> finallist = new List<BS_SHARED.SHARED>();
                    shared.agentID = inventroyList[p].agentID;
                    shared.AgencyName = inventroyList[p].AgencyName.Trim();
                    shared.provider_name = inventroyList[p].provider_name;
                    shared.traveler = inventroyList[p].traveler;
                    shared.totWithTaxes = inventroyList[p].totWithTaxes;
                    foreach (RepeaterItem item in rep_Pax.Items)
                    {
                        DropDownList dptitle = (DropDownList)item.FindControl("dptitle");
                        TextBox txtname = (TextBox)item.FindControl("txtpaxname");
                        TextBox txtage = (TextBox)item.FindControl("txtpaxage");
                        DropDownList dpgender = (DropDownList)item.FindControl("dpgender");

                        if (inventroyList[p].orderID.Substring(inventroyList[p].orderID.Length - 1) == "R")
                        {
                            Label lblseat1 = (Label)item.FindControl("lblseat1");
                            Label lblfare1 = (Label)item.FindControl("lblfare1");
                            shared.paxseat.Add(lblseat1.Text.Trim());
                            shared.perFare.Add(lblfare1.Text.Trim());
                        }
                        else
                        {
                            Label lblseat = (Label)item.FindControl("lblseat");
                            Label lblfare = (Label)item.FindControl("lblfare");
                            shared.paxseat.Add(lblseat.Text.Trim());
                            shared.perFare.Add(lblfare.Text.Trim());
                        }
                        shared.title.Add(dptitle.SelectedValue);
                        shared.paxname.Add(txtname.Text.Trim());
                        shared.paxage.Add(txtage.Text.Trim());
                        if (dpgender.SelectedValue == "F")
                        {
                            shared.gender.Add("FEMALE");
                        }
                        else
                        {
                            shared.gender.Add("MALE");
                        }
                        //if (shared.provider_name == "RB")
                        //{
                        //    if (dpgender.SelectedValue == "F")
                        //    {
                        //        shared.gender.Add("FEMALE");
                        //    }
                        //    else
                        //    {
                        //        shared.gender.Add("MALE");
                        //    }
                        //}
                        //else
                        //{
                        //    shared.gender.Add(dpgender.SelectedValue);
                        //}
                    }


                    string[] strfare = inventroyList[p].originalfare.Split(',');
                    string[] strSeat = inventroyList[p].seat.Split(',');
                    for (int f = 0; f <= strfare.Length - 1; f++)
                    {
                        if (strfare[f].Trim() != "")
                        {
                            shared.fare = strfare[f].Trim();
                            shared.perOriginalFare.Add(strfare[f].Trim());
                            shared.seat = strSeat[f].Trim();
                            farelist = sharedbal.getCommissionList(shared);
                            decimal withtax = 0;
                            if (inventroyList[p].provider_name == "ES")
                            {
                                withtax = Convert.ToDecimal(inventroyList[p].WithTaxes.Split('*')[f]);
                                shared.subAmt += Convert.ToDecimal(farelist[0].taNetFare.ToString().Trim()) + withtax;
                                farelist[0].taTotFare += withtax;
                                farelist[0].taNetFare += withtax;
                                farelist[0].totFare += withtax;
                            }
                            else
                            {
                                shared.WithTaxes = inventroyList[p].WithTaxes.Split('*')[f];
                                shared.subAmt += Convert.ToDecimal(farelist[0].taNetFare.ToString().Trim());
                            }

                            // shared.subAmt += Convert.ToDecimal(farelist[0].taNetFare.ToString().Trim());
                            // finallist.Add(new BS_SHARED.SHARED { adcomm = farelist[0].adcomm, taTds = farelist[0].taTds, admrkp = farelist[0].admrkp, agmrkp = farelist[0].agmrkp, taTotFare = farelist[0].taTotFare, taNetFare = farelist[0].taNetFare });
                            finallist.Add(new BS_SHARED.SHARED { adcomm = farelist[0].adcomm, taTds = farelist[0].taTds, admrkp = farelist[0].admrkp, agmrkp = farelist[0].agmrkp, taTotFare = farelist[0].taTotFare, taNetFare = farelist[0].taNetFare, totFare = farelist[0].totFare });
                        }
                    }
                    shared.orderID = inventroyList[p].orderID;
                    shared.serviceID = inventroyList[p].serviceID;
                    //  string[] srcid = inventroyList[p].src.Split('(');
                    shared.src = inventroyList[p].src.Substring(0, inventroyList[p].src.LastIndexOf("("));// srcid[0].Trim();
                    //     srcid = inventroyList[p].src.Substring(inventroyList[p].src.LastIndexOf("(") + 1).Replace(")", "");// srcid[1].Split(')');
                    //  string[] destid = inventroyList[p].dest.Split('(');
                    shared.dest = inventroyList[p].dest.Substring(0, inventroyList[p].dest.LastIndexOf("("));//destid[0].Trim();
                    // destid = inventroyList[p].dest.Substring(inventroyList[p].dest.LastIndexOf("("));//destid[1].Split(')');
                    shared.srcID = inventroyList[p].src.Substring(inventroyList[p].src.LastIndexOf("(") + 1).Replace(")", "");// srcid[0].Trim();
                    shared.destID = inventroyList[p].dest.Substring(inventroyList[p].dest.LastIndexOf("(") + 1).Replace(")", "");
                    shared.traveler = inventroyList[p].traveler;
                    shared.ladiesSeat = inventroyList[p].ladiesSeat;
                    string[] board = inventroyList[p].boardpoint.Split('&');
                    string[] drop = inventroyList[p].droppoint.Split('&');
                    shared.boardpoint = board[0].Trim();
                    shared.droppoint = drop[0].Trim();
                    string[] bpointid = board[1].Trim().Split('(');
                    string[] dpointid = drop[1].Trim().Split('(');
                    shared.boardpointid = bpointid[0].Trim();
                    shared.droppointid = dpointid[0].Trim();
                    shared.journeyDate = inventroyList[p].journeyDate;
                    shared.partialCanAllowed = inventroyList[p].partialCanAllowed;
                    flag = false;
                    for (int n = 0; n <= shared.paxname.Count - 1; n++)
                    {
                        if (shared.provider_name == "GS")
                        {
                            if (flag == false)
                            {
                                shared.paxname[0] = shared.paxname[0].Trim() + ",primary";
                                shared.Isprimary = "true";
                                flag = true;
                            }
                        }
                        else
                        {
                            if (shared.paxname[n].ToString().Trim() == Request["txtprimarypax"].ToString().Trim())
                            {
                                shared.paxname[n] = shared.paxname[n].Trim() + ",primary";
                                shared.Isprimary = "true";
                                flag = true;
                            }
                            else
                            {
                                if (flag == false)
                                {
                                    shared.paxname[0] = shared.paxname[0].Trim() + ",primary";
                                    shared.Isprimary = "true";
                                }
                            }
                        }
                    }
                    shared.NoOfPax = Convert.ToString(shared.paxname.Count);
                    shared.paxmob = Request["txtmob"].ToString().Trim();
                    shared.paxemail = Request["txtemail"].ToString().Trim();
                    shared.paxaddress = Request["txtaddress"].ToString().Trim();
                    shared.provider_name = inventroyList[p].provider_name.Trim();
                    shared.traveler = inventroyList[p].traveler.Trim();
                    shared.canPolicy = inventroyList[p].canPolicy.Trim();
                    shared.AC_NONAC = inventroyList[p].AC_NONAC.Trim();
                    shared.SEAT_TYPE = inventroyList[p].SEAT_TYPE.Trim();
                    if (inventroyList[p].idproofReq == "true")
                    {
                        shared.idnumber = Request["txtcard"].ToString().Trim();
                        shared.idtype = Request["idproof"].ToString().Trim();
                    }
                    else
                    {
                        shared.idproofReq = "false";
                    }
                    #region[Check Credit Limit and Final Book]
                    decimal crdlimit = 0;
                    crdlimit = sharedbal.getCrdLimit(shared.agentID.Trim());


                    //_GetBalance objParamBal = new _GetBalance();
                    //ITZGetbalance objItzBal = new ITZGetbalance();
                    //ITZcrdb objItzTrans = new ITZcrdb();

                    //DebitResponse objDebResp = new DebitResponse();

                    //_CrOrDb objParamDeb = new _CrOrDb();

                    //try
                    //{
                    //objParamBal._DCODE = (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : " ");
                    //objParamBal._MERCHANT_KEY = (Session["MchntKeyITZ"] != null ? Session["MchntKeyITZ"].ToString().Trim() : " ");
                    ////'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                    //objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : " ");
                    //objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : " ");
                    //objBalResp = objItzBal.GetBalanceCustomer(objParamBal);

                    //if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL != null)
                    //{
                    //    crdlimit = Convert.ToDecimal(objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE);
                    //}
                    //else { crdlimit = 0; }

                    //}
                    //catch (Exception ex)
                    //{
                    //    crdlimit = 0;
                    //}
                    shared.paymentmode = rblPaymentMode.SelectedValue;

                    try
                    {
                        if (Session["LoginByOTP"] != null && Convert.ToString(Session["LoginByOTP"]) != "" && Convert.ToString(Session["LoginByOTP"]) == "true")
                        {
                            string UserId = Convert.ToString(Session["UID"]);
                            string Remark = "BUS TICKET BOOK";
                            string OTPRefNo = Convert.ToString(shared.orderID);
                            string LoginByOTP = Convert.ToString(Session["LoginByOTP"]);
                            string OTPId = Convert.ToString(Session["OTPID"]);
                            string ServiceType = "BUS-TICKET-BOOK";
                            int f = 0;
                            SqlTransaction OTPST = new SqlTransaction();
                            f = OTPST.OTPTransactionInsert(UserId, Remark, OTPRefNo, LoginByOTP, OTPId, ServiceType);
                        }
                    }
                    catch (Exception ex)
                    {
                        EXCEPTION_LOG.ErrorLog.FileHandling("BUS", "Error_102", ex, "BS-CustomerInfo");
                    }


                    if (rblPaymentMode.SelectedValue == "WL")
                    {
                        #region Bus Booking from Wallet

                        if (crdlimit >= shared.subAmt)
                        {
                            strmessage = Booking(shared, finallist);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('you have insufficient balance');window.location='../Search.aspx'; ", true);
                        }
                        if (strmessage.Split('_')[0] == "Error")
                        {
                            if (strTinNo == "")
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + strmessage + "');window.location='../Search.aspx'; ", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + strmessage + " please Contect Administrator for refund yor return trip');window.location='TicketCopy.aspx?tin=" + strTinNo + "&oid=" + strOrderId + "'; ", true);

                        }
                        else
                        {
                            if (shared.provider_name == "GS")
                            {
                                if (shared.orderID.Substring(shared.orderID.Length - 1) == "O")
                                    GsResponse1 = strmessage;
                                else
                                    GsResponse2 = strmessage;
                            }
                            else
                            {
                                strTinNo += strmessage + "*";
                                strOrderId += shared.orderID + "*";
                            }
                        }

                        if (strmessage.Split('_')[0] == "Error")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + strmessage + "');window.location='../Search.aspx'; ", true);
                        }
                        else
                        {
                            if (shared.provider_name == "GS")
                            {
                                if (GsResponse2 == "")
                                {
                                    ghxsgh.InnerHtml = "<div class='lft w50 f20'>One Way</div><div class='clear1'></div><div>" + GsResponse1 + "</div>";
                                    ordrId.InnerHtml = "";
                                    ordrId.InnerHtml = shared.orderID;
                                }
                                else
                                {
                                    ghxsgh.InnerHtml = "<div class='lft w50 f20'>One Way</div><div class='lft w50 f20'>Return</div><div class='clear1'></div><div class='lft w50'>" + GsResponse1 + "</div><div class='w50 rgt'>" + GsResponse2 + "</div>";
                                    ordrId1.InnerHtml = "";
                                    ordrId1.InnerHtml = shared.orderID.Trim().Substring(0, shared.orderID.Trim().Length - 1) + "R";
                                    ordrId.InnerHtml = "";
                                    ordrId.InnerHtml = shared.orderID.Trim().Substring(0, shared.orderID.Trim().Length - 1) + "O";
                                }
                                idehide.Visible = true;
                                ghxsgh.Visible = true;
                                hideGs.Visible = true;
                                ordrId.Visible = false;
                                ordrId1.Visible = false;
                                tblhidefalse.Visible = false;
                            }
                            else
                            {

                                if (strTinNo.IndexOf("*") > -1)
                                {
                                    strTinNo = strTinNo.Substring(0, strTinNo.Length - 1);
                                }
                                if (strOrderId.IndexOf("*") > -1)
                                {
                                    strOrderId = strOrderId.Substring(0, strOrderId.Length - 1);
                                }


                                Response.Redirect("TicketCopy.aspx?tin=" + strTinNo + "&oid=" + strOrderId + "");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //Booking by PaymentGateway
                        BookingByPaymentGateway(shared, finallist);
                    }



                }

                #endregion
            }

            catch (Exception ex)
            {
                erlog = new EXCEPTION_LOG.ErrorLog();
                erlog.writeErrorLog(ex, "CustomerInfo.aspx.cs");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + strExistsMessage + "');window.location='../Search.aspx'; ", true);
        }
    }

    protected void btnbookGs_Click(object sender, EventArgs e)
    {
        List<BS_SHARED.SHARED> list_Pnr1 = new List<BS_SHARED.SHARED>();
        string TinNo = ""; string OrderIds = "";
        BS_SHARED.SHARED shareddata = new BS_SHARED.SHARED();
        BS_BAL.SharedBAL sharedbal1 = new BS_BAL.SharedBAL();
        try
        {
            if (ordrId.InnerText.Trim().Substring(ordrId.InnerText.Trim().Length - 1) == "O")
            {
                shareddata = sharedbal1.getSelectedSeatPaxInfo(ordrId.InnerText.Trim().Substring(0, ordrId.InnerText.Trim().Length - 1), ordrId.InnerText.Trim());
                list_Pnr1 = sharedbal1.getSelectedSeat_TicketNo(shareddata);

                //  OrderIds = shareddata.orderID.Substring(0, shareddata.orderID.Length - 1);

                if (list_Pnr1[0].status == "Fail" || list_Pnr1[0].status == "Error" || list_Pnr1[0].status == "FAIL")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr1[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
                }
                else
                {
                    TinNo += list_Pnr1[0].tin.Trim() + "*";
                    OrderIds += shareddata.orderID + "*";
                }
            }
            if (ordrId1.InnerText.Trim().Substring(ordrId1.InnerText.Trim().Length - 1) == "R")
            {
                shareddata = sharedbal1.getSelectedSeatPaxInfo(ordrId1.InnerText.Trim().Substring(0, ordrId1.InnerText.Trim().Length - 1), ordrId1.InnerText.Trim());
                list_Pnr1 = sharedbal1.getSelectedSeat_TicketNo(shareddata);
                if (list_Pnr1[0].status == "Fail" || list_Pnr1[0].status == "Error" || list_Pnr1[0].status == "FAIL")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please Contect Administrator for refund yor return trip');window.location='TicketCopy.aspx?tin=" + TinNo + "&oid=" + OrderIds + "'; ", true);
                }
                else
                {
                    TinNo += list_Pnr1[0].tin.Trim() + "*";
                    OrderIds += shareddata.orderID + "*";
                }
            }
            if (list_Pnr1[0].status == "Fail" || list_Pnr1[0].status == "Error" || list_Pnr1[0].status == "FAIL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr1[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
            }
            else
            {
                Response.Redirect("TicketCopy.aspx?tin=" + TinNo + "&oid=" + OrderIds + "");
            }
        }
        catch (Exception ex)
        {
            erlog = new EXCEPTION_LOG.ErrorLog();
            erlog.writeErrorLog(ex, "CustomerInfo.aspx.cs");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('" + ex.Message + "');", true);
        }
    }
    public string Booking(BS_SHARED.SHARED sharedddd, List<BS_SHARED.SHARED> finallist)
    {
        List<BS_SHARED.SHARED> farelist = new List<BS_SHARED.SHARED>();
        List<BS_SHARED.SHARED> list_Pnr = new List<BS_SHARED.SHARED>();
        string message = "";
        try
        {
            #region Balance Check and deduct and Transaction Log - Staff Login

            string DebitSataus = "";
            string CreditSataus = "";
            string CheckBalance = "";
            string AgentStatus = "";
            string StaffBalCheck = "";
            string StaffBalCheckStatus = "";
            string CurrentTotAmt = "";
            string TransAmount = "";
            string BookTicket = "true";
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["LoginByStaff"])) && Convert.ToString(Session["LoginByStaff"]).ToUpper() == "TRUE" && Convert.ToString(Session["LoginType"]).ToUpper() == "STAFF")
                {
                    BookTicket = "false";
                    if (Convert.ToString(Session["BUS"]) == "True")
                    {
                        string BoookingByStaff = "True";
                        string sOrderId = shared.orderID;
                        string sTransAmount = Convert.ToString(shared.subAmt); //FltHdrDs.Tables(0).Rows(0)["TotalAfterDis"];
                        string sStaffUserId = Convert.ToString(Session["StaffUserId"]);
                        string sOwnerId = Convert.ToString(Session["UID"]);
                        string sIPAddress = Request.UserHostAddress;
                        string sRemark = Convert.ToString(Session["LoginType"]) + "_" + Convert.ToString(Session["StaffUserId"]) + "_" + sOrderId + "_BUS_" + shared.provider_name + "_" + shared.journeyDate + "_" + Convert.ToString(shared.subAmt);
                        string sCreatedBy = Convert.ToString(Session["StaffUserId"]);
                        string ModuleType = "BUS BOOKING";
                        string sServiceType = "BUS";
                        string DebitCredit = "DEBIT";
                        string ActionType = "CHECKBAL-DEDUCT";
                        DataSet StaffDs;
                        //Dim objSqlDom As New SqlTransactionDom
                        SqlTransactionDom objSqlDom = new SqlTransactionDom();
                        StaffDs = objSqlDom.CheckStaffBalanceAndBalanceDeduct(sOrderId, sServiceType, Convert.ToDouble(sTransAmount), sStaffUserId, sOwnerId, sIPAddress, sRemark, sCreatedBy, DebitCredit, ModuleType, ActionType);
                        if ((StaffDs != null && StaffDs.Tables.Count > 0 && StaffDs.Tables[0].Rows.Count > 0))
                        {
                            // DebitSataus ,CreditSataus,CheckBalance,AgentStatus,StaffBalCheck,StaffBalCheckStatus,CurrentTotAmt,TransAmount		
                            DebitSataus = Convert.ToString(StaffDs.Tables[0].Rows[0]["DebitSataus"]);
                            CreditSataus = Convert.ToString(StaffDs.Tables[0].Rows[0]["CreditSataus"]);
                            CheckBalance = Convert.ToString(StaffDs.Tables[0].Rows[0]["CheckBalance"]);
                            AgentStatus = Convert.ToString(StaffDs.Tables[0].Rows[0]["AgentStatus"]);
                            StaffBalCheck = Convert.ToString(StaffDs.Tables[0].Rows[0]["StaffBalCheck"]);
                            StaffBalCheckStatus = Convert.ToString(StaffDs.Tables[0].Rows[0]["StaffBalCheckStatus"]);
                            CurrentTotAmt = Convert.ToString(StaffDs.Tables[0].Rows[0]["CurrentTotAmt"]);
                            TransAmount = Convert.ToString(StaffDs.Tables[0].Rows[0]["TransAmount"]);
                            BookTicket = "false";
                            if (Convert.ToString(StaffDs.Tables[0].Rows[0]["LoginStatus"]) == "True" && Convert.ToString(StaffDs.Tables[0].Rows[0]["BUS"]) == "True" && (DebitSataus.ToLower() == "true" || StaffBalCheckStatus.ToLower() == "false"))
                                BookTicket = "true";
                        }
                        else
                            BookTicket = "false";
                    }
                    else
                        BookTicket = "false";
                }
            }
            catch (Exception ex)
            {
                BookTicket = "false";
            }

            // END: Balance Check and deduct and Transaction Log - Staff
            #endregion Staff 

            if (BookTicket.ToLower() == "true")
            {
                if (shared.provider_name == "GS")
                {
                    shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                    if (shared.blockKey.Trim() == "Error" || shared.blockKey.Trim() == "")
                    {
                        message = "Error_The seat has already been booked..please try after some time";
                    }
                    else
                    {
                        finallist = sharedbal.SetPaxInformationGS(shared);
                        if (finallist[0].orderID.Contains("Error") == true)
                        {
                            message = finallist[0].orderID + "   please contact administrator";
                        }
                        else
                        {
                            //------------------------insert into database------------------------//
                            sharedbal.insertselected_seatforbook(shared, finallist);
                            message = @finallist[0].bookres;
                        }
                    }

                }
                else if (shared.provider_name == "RB" || shared.provider_name == "ES")
                {
                    ghxsgh.Visible = false;
                    tblhidefalse.Visible = true;
                    ordrId.Visible = false;
                    hideGs.Visible = false;
                    sharedbal.insertselected_seatforbook(shared, finallist);
                    shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);


                    //_GetBalance objParamBal = new _GetBalance();
                    //ITZGetbalance objItzBal = new ITZGetbalance();
                    //ITZcrdb objItzTrans = new ITZcrdb();

                    //DebitResponse objDebResp = new DebitResponse();
                    // GetBalanceResponse objBalResp = new GetBalanceResponse();
                    //_CrOrDb objParamDeb = new _CrOrDb();

                    if (shared.blockKey.ToString().Contains("Error") == true)
                    {
                        if (shared.blockKey.ToString().Contains("Insufficient balance") == true)
                        {
                            message = "Error_The seat has already been booked.please try after some time";
                        }
                        else
                        {
                            message = shared.blockKey.ToString().Trim().Replace("Error:", "");
                        }

                    }
                    else
                    {

                        BS_DAL.SharedDAL shareddal = new BS_DAL.SharedDAL();
                        //shareddal.updateTicketno(shared);
                        shared.avalBal = shareddal.deductAndaddfareAmt(shared, "subtract");
                        shareddal.insertLedgerDetails(shared, "subtract");

                        //if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL != null)
                        //{
                        //if (objBalResp.VAL_ACCOUNT_TYPE_DETAIL.Length > 0)
                        //{
                        //if (Convert.ToDouble(shared.subAmt) <= Convert.ToDouble(objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE.Trim()))
                        //{
                        //AvlBal = objDA.UpdateCrdLimit(Session["UID"], FltHdrDs.Tables(0).Rows(0)("TotalAfterDis"))
                        //If AvlBal > 0 Then
                        // amtbeforded = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE : " ");
                        //string MchntKeyITZ = ConfigurationManager.AppSettings["BUSMerchantKey"].ToString();
                        //try
                        //{
                        //    objParamDeb._MERCHANT_KEY = (MchntKeyITZ != null ? MchntKeyITZ.Trim() : " ");
                        //    //'IIf(Not ConfigurationManager.AppSettings("MerchantKey") Is Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        //    objParamDeb._PASSWORD = ((Session["_PASSWORD"] != null) ? Session["_PASSWORD"].ToString().Trim() : " ");
                        //    objParamDeb._DECODE = ((Session["_DCODE"] != null) ? Session["_DCODE"].ToString().Trim() : " ");
                        //    //objParamDeb._AMOUNT = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString();
                        //    objParamDeb._AMOUNT = shared.subAmt.ToString();
                        //    objParamDeb._MODE = (Session["ModeTypeITZ"] != null ? Session["ModeTypeITZ"].ToString().Trim() : " ");
                        //    //'IIf(Not ConfigurationManager.AppSettings("ITZMode") Is Nothing, ConfigurationManager.AppSettings("ITZMode").Trim(), " ")
                        //    objParamDeb._ORDERID = (shared.orderID != null && !string.IsNullOrEmpty(shared.orderID) ? shared.orderID : " ");
                        //    objParamDeb._DESCRIPTION = " Amount deducted against booking BUS-" + shared.orderID;
                        //    //objParamDeb._CHECKSUM = " "
                        //    string stringtoenc = "MERCHANTKEY=" + objParamDeb._MERCHANT_KEY + "&ORDERID=" + objParamDeb._ORDERID + "&AMOUNT=" + objParamDeb._AMOUNT + "&MODE=" + objParamDeb._MODE;
                        //    objParamDeb._CHECKSUM = VGCheckSum.calculateEASYChecksum(stringtoenc);
                        //    objParamDeb._SERVICE_TYPE = (Session["_SvcTypeITZ"] != null ? Session["_SvcTypeITZ"].ToString().Trim() : " ");
                        //    //'IIf(Not ConfigurationManager.AppSettings("ITZSvcType") Is Nothing, ConfigurationManager.AppSettings("ITZSvcType").Trim(), " ")
                        //    objDebResp = objItzTrans.ITZDebit(objParamDeb);
                        //}
                        //catch (Exception ex)
                        //{
                        //}

                        //try
                        //{
                        //    objItzT = new Itz_Trans_Dal();
                        //    //objIzT.AMT_TO_DED = FltHdrDs.Tables(0).Rows(0)("TotalAfterDis").ToString();
                        //    objIzT.AMT_TO_DED = shared.subAmt.ToString();
                        //    objIzT.AMT_TO_CRE = "0";
                        //    objIzT.B2C_MBLNO_ITZ = (objDebResp.B2C_MOBILENO != null ? objDebResp.B2C_MOBILENO : " ");
                        //    objIzT.COMMI_ITZ = (objDebResp.COMMISSION != null ? objDebResp.COMMISSION : " ");
                        //    objIzT.CONVFEE_ITZ = (objDebResp.CONVENIENCEFEE != null ? objDebResp.CONVENIENCEFEE : " ");
                        //    objIzT.DECODE_ITZ = (objDebResp.DECODE != null && !string.IsNullOrEmpty(objDebResp.DECODE) && objDebResp.DECODE != " " ? objDebResp.DECODE : (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : " "));
                        //    objIzT.EASY_ORDID_ITZ = (objDebResp.EASY_ORDER_ID != null ? objDebResp.EASY_ORDER_ID : " ");
                        //    objIzT.EASY_TRANCODE_ITZ = (objDebResp.EASY_TRAN_CODE != null ? objDebResp.EASY_TRAN_CODE : " ");
                        //    objIzT.ERROR_CODE = (objDebResp.ERROR_CODE != null ? objDebResp.ERROR_CODE : " ");
                        //    objIzT.MERCHANT_KEY_ITZ = (objDebResp.MERCHANT_KEY != null && !string.IsNullOrEmpty(objDebResp.MERCHANT_KEY) && objDebResp.MERCHANT_KEY != " " ? objDebResp.MERCHANT_KEY : (MchntKeyITZ != null ? MchntKeyITZ.Trim() : " "));
                        //    objIzT.MESSAGE_ITZ = (objDebResp.MESSAGE != null ? objDebResp.MESSAGE : " ");
                        //    objIzT.ORDERID = (shared.orderID != null && !string.IsNullOrEmpty(shared.orderID) ? shared.orderID : " ");
                        //    objIzT.RATE_GROUP_ITZ = (objDebResp.RATEGROUP != null ? objDebResp.RATEGROUP : " ");
                        //    objIzT.REFUND_TYPE_ITZ = " ";
                        //    objIzT.SERIAL_NO_FROM = (objDebResp.SERIALNO_FROM != null ? objDebResp.SERIALNO_FROM : " ");
                        //    objIzT.SERIAL_NO_TO = (objDebResp.SERIALNO_TO != null ? objDebResp.SERIALNO_TO : " ");
                        //    objIzT.SVC_TAX_ITZ = (objDebResp.SERVICETAX != null ? objDebResp.SERVICETAX : " ");
                        //    objIzT.TDS_ITZ = (objDebResp.TDS != null ? objDebResp.TDS : " ");
                        //    objIzT.TOTAL_AMT_DED_ITZ = (objDebResp.TOTALAMOUNT != null ? objDebResp.TOTALAMOUNT : " ");
                        //    objIzT.TRANS_TYPE = "BUS";
                        //    objIzT.USER_NAME_ITZ = (objDebResp.USERNAME != null && !string.IsNullOrEmpty(objDebResp.USERNAME) && objDebResp.USERNAME != " " ? objDebResp.USERNAME : (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : " "));
                        //    try
                        //    {
                        //        objBalResp = new GetBalanceResponse();
                        //        objParamBal._DCODE = (Session["_DCODE"] != null ? Session["_DCODE"].ToString().Trim() : " ");
                        //        objParamBal._MERCHANT_KEY = (MchntKeyITZ != null ? MchntKeyITZ : " ");
                        //        //'IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
                        //        objParamBal._PASSWORD = (Session["_PASSWORD"] != null ? Session["_PASSWORD"].ToString().Trim() : " ");
                        //        objParamBal._USERNAME = (Session["_USERNAME"] != null ? Session["_USERNAME"].ToString().Trim() : " ");
                        //        objBalResp = objItzBal.GetBalanceCustomer(objParamBal);
                        //        objIzT.ACCTYPE_NAME_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_TYPE_NAME : " ");
                        //        objIzT.AVAIL_BAL_ITZ = (objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE != null ? objBalResp.VAL_ACCOUNT_TYPE_DETAIL[0].VAL_ACCOUNT_BALANCE : " ");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //    }
                        //    objItzT.InsertItzTrans(objIzT);
                        //}
                        //catch (Exception ex)
                        //{
                        //}

                        //if (objDebResp != null)
                        //{

                        //    if (objDebResp.MESSAGE.Trim().ToLower().Contains("request successfully execute"))
                        //    {

                        if ((shared.blockKey.Trim() != "" && shared.blockKey.Trim().ToString().Contains("Error") == false && shared.blockKey.Trim().ToString().Contains("error") == false))
                        {
                            if (shared.provider_name != "GS")
                            {

                                //----------------------final book--------------------//
                                //list_Pnr.Add(new BS_SHARED.SHARED { status = "Error", bookres="Check"});





                                list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                                if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                                {
                                    message = list_Pnr[0].bookres.Trim();
                                }
                                else
                                {
                                    message = list_Pnr[0].tin.Trim();
                                }


                            }
                        }
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have insufficient balance to perform this transaction. Please keep sufficient balance in the linked wallet.'); window.location.href='../Search.aspx'; ", true);

                        //    }

                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have insufficient balance to perform this transaction. Please keep sufficient balance in the linked wallet.'); window.location.href='~/Search.aspx'; ", true);

                        //}
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have insufficient balance to perform this transaction. Please keep sufficient balance in the linked wallet.'); window.location.href='../Search.aspx'; ", true);

                        //}
                        //}

                        //}
                        //else
                        //{

                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + objBalResp.MESSAGE + "'); window.location.href='../Search.aspx'; ", true);
                        //}
















                    }

                }
                else if (shared.provider_name == "AB")
                {
                    ghxsgh.Visible = false;
                    tblhidefalse.Visible = true;
                    ordrId.Visible = false;
                    hideGs.Visible = false;
                    sharedbal.insertselected_seatforbook(shared, finallist);
                    shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                    if (shared.blockKey.Trim() == "Error")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The seat has already been booked..please try after some time');window.location='../Search.aspx'; ", true);
                    }
                    else
                    {
                        //----------------------final book--------------------//
                        list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                        if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
                        }
                        else
                        {
                            Response.Redirect("TicketCopy.aspx?tin=" + list_Pnr[0].tin.Trim() + "&oid=" + shared.orderID.Trim() + "");
                        }
                    }
                }
                else if (shared.provider_name == "TY")
                {
                    ghxsgh.Visible = false;
                    tblhidefalse.Visible = true;
                    ordrId.Visible = false;
                    hideGs.Visible = false;
                    sharedbal.insertselected_seatforbook(shared, finallist);
                    shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                    if (shared.blockKey.Contains("Error") == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The seat has already been booked..please try after some time');window.location='../Search.aspx'; ", true);
                    }
                    else
                    {
                        //----------------------final book--------------------//
                        list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                        if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                        {
                            message = list_Pnr[0].tin.Trim();
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
                        }
                        else
                        {
                            message = list_Pnr[0].tin.Trim();
                            //Response.Redirect("TicketCopy.aspx?tin=" + list_Pnr[0].tin.Trim() + "&oid=" + shared.orderID.Trim() + "");
                        }
                    }
                }
                #endregion
                //
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Booking is not allowed.');window.location='../Search.aspx'; ", true);
                //message = "Booking not allowed";
            }
        }
        catch (Exception ex)
        {
        }
        return message;
    }
    public string Check_Exists_OrderId(string orderID)
    {
        string strMessage = "";
        SqlConnection con;
        DataTable dt = new DataTable();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        try
        {

            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("SP_RB_CHECK_EXISTS_ORDERID", con);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@ORDERID", orderID);
            adap.Fill(dt);

            if (dt.Rows[0]["returnvalue"].ToString() != "0")
                strMessage = "";
            else
                strMessage = "please try again ....";

            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }
        return strMessage;
    }
    public static string ConvertTo_ProperCase(string text)
    {
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        return myTI.ToTitleCase(text.ToLower());
    }

    public void BookingByPaymentGateway(BS_SHARED.SHARED sharedddd, List<BS_SHARED.SHARED> finallist)
    {
        List<BS_SHARED.SHARED> farelist = new List<BS_SHARED.SHARED>();
        List<BS_SHARED.SHARED> list_Pnr = new List<BS_SHARED.SHARED>();



        #region Use for only PaymentGateway
        string PgMsg = string.Empty;
        PG.PaymentGateway objPg = new PG.PaymentGateway();
        SqlTransaction objDA = new SqlTransaction();
        DataSet AgencyDs = objDA.GetAgencyDetails(shared.agentID);

        string ipAddress = null;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ipAddress) | ipAddress == null)
        {
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        string ReferenceNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        string Tid = ReferenceNo.Substring(4, 16);
        #endregion
        string message = "";
        try
        {
            if (shared.provider_name == "GS")
            {
                shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                if (shared.blockKey.Trim() == "Error" || shared.blockKey.Trim() == "")
                {
                    message = "Error_The seat has already been booked..please try after some time";
                }
                else
                {
                    finallist = sharedbal.SetPaxInformationGS(shared);
                    if (finallist[0].orderID.Contains("Error") == true)
                    {
                        message = finallist[0].orderID + "   please contact administrator";
                    }
                    else
                    {
                        //------------------------insert into database------------------------//
                        sharedbal.insertselected_seatforbook(shared, finallist);
                        message = @finallist[0].bookres;
                    }
                }

            }
            else if (shared.provider_name == "RB" || shared.provider_name == "ES")
            {
                ghxsgh.Visible = false;
                tblhidefalse.Visible = true;
                ordrId.Visible = false;
                hideGs.Visible = false;
                sharedbal.insertselected_seatforbook(shared, finallist);
                shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                if (shared.blockKey.ToString().Contains("Error") == true || shared.blockKey.Contains("<HTML>"))
                {
                    if (shared.blockKey.ToString().Contains("Insufficient balance") == true)
                    {
                        message = "Error_The seat has already been booked.please try after some time";
                    }
                    else if (shared.blockKey.Contains("<HTML>"))
                    {
                        message = shared.blockKey;
                    }
                    else
                    {
                        message = shared.blockKey.ToString().Trim().Replace("Error:", "");
                    }

                }
                else
                {

                    BS_DAL.SharedDAL shareddal = new BS_DAL.SharedDAL();
                    //shareddal.updateTicketno(shared);

                    //shared.avalBal = shareddal.deductAndaddfareAmt(shared, "subtract");
                    //shareddal.insertLedgerDetails(shared, "subtract");               

                    if ((shared.blockKey.Trim() != "" && shared.blockKey.Trim().ToString().Contains("Error") == false && shared.blockKey.Trim().ToString().Contains("error") == false))
                    {
                        if (shared.provider_name != "GS")
                        {
                            #region redirect to payment gateway
                            //'Use for Payment Option
                            PgMsg = objPg.PaymentGatewayReqPayU(shared.orderID, Tid, "", shared.agentID, Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(shared.subAmt), Convert.ToDouble(shared.subAmt), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
                            Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "BUS", ipAddress, "", rblPaymentMode.SelectedValue);
                            if (PgMsg.Contains("~"))
                            {
                                if (PgMsg.Split('~')[0] == "yes")
                                {
                                    //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                                    if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                                    {
                                        Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                            }
                            #endregion
                            //----------------------final book--------------------//


                            //list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                            //if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                            //{
                            //    message = list_Pnr[0].bookres.Trim();
                            //}
                            //else
                            //{
                            //    message = list_Pnr[0].tin.Trim();
                            //}                           
                        }
                    }
                }

            }
            else if (shared.provider_name == "AB")
            {
                ghxsgh.Visible = false;
                tblhidefalse.Visible = true;
                ordrId.Visible = false;
                hideGs.Visible = false;
                sharedbal.insertselected_seatforbook(shared, finallist);
                shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                if (shared.blockKey.Trim() == "Error")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The seat has already been booked..please try after some time');window.location='../Search.aspx'; ", true);
                }
                else
                {

                    #region redirect to payment gateway
                    //'Use for Payment Option
                    PgMsg = objPg.PaymentGatewayReqPayU(shared.orderID, Tid, "", shared.agentID, Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(shared.subAmt), Convert.ToDouble(shared.subAmt), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
                    Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "BUS", ipAddress, "", rblPaymentMode.SelectedValue);
                    if (PgMsg.Contains("~"))
                    {
                        if (PgMsg.Split('~')[0] == "yes")
                        {
                            //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                            if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                            {
                                Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                    }
                    #endregion

                    //----------------------final book--------------------//


                    //list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                    //if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + list_Pnr[0].bookres.Trim() + "');window.location='../Search.aspx'; ", true);
                    //}
                    //else
                    //{
                    //    Response.Redirect("TicketCopy.aspx?tin=" + list_Pnr[0].tin.Trim() + "&oid=" + shared.orderID.Trim() + "");
                    //}
                }
            }
            else if (shared.provider_name == "TY")
            {
                ghxsgh.Visible = false;
                tblhidefalse.Visible = true;
                ordrId.Visible = false;
                hideGs.Visible = false;
                sharedbal.insertselected_seatforbook(shared, finallist);
                shared.blockKey = sharedbal.getSelectedSeat_BlockKey(shared);
                if (shared.blockKey.Contains("Error") == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The seat has already been booked..please try after some time');window.location='../Search.aspx'; ", true);
                }
                else
                {

                    #region redirect to payment gateway
                    //'Use for Payment Option
                    PgMsg = objPg.PaymentGatewayReqPayU(shared.orderID, Tid, "", shared.agentID, Convert.ToString(AgencyDs.Tables[0].Rows[0]["Agency_Name"]), Convert.ToDouble(shared.subAmt), Convert.ToDouble(shared.subAmt), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Fname"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Address"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["City"]),
                    Convert.ToString(AgencyDs.Tables[0].Rows[0]["State"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["zipcode"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Mobile"]), Convert.ToString(AgencyDs.Tables[0].Rows[0]["Email"]), "BUS", ipAddress, "", rblPaymentMode.SelectedValue);
                    if (PgMsg.Contains("~"))
                    {
                        if (PgMsg.Split('~')[0] == "yes")
                        {
                            //' Response.Redirect("../PaymentGateway.aspx?OBTID=" & ViewState("trackid") & "&IBTID=" & ViewState("IBTrackId") & "&FT=" & ViewState("FT") & "", False)
                            if (!string.IsNullOrEmpty(PgMsg.Split('~')[1]))
                            {
                                Page.Controls.Add(new LiteralControl(PgMsg.Split('~')[1]));
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('please try after some time because payment gateway process to be busy');window.location='../Search.aspx'; ", true);
                    }
                    #endregion
                    //----------------------final book--------------------//
                    //list_Pnr = sharedbal.getSelectedSeat_TicketNo(shared);
                    //if (list_Pnr[0].status == "Fail" || list_Pnr[0].status == "Error")
                    //{
                    //    message = list_Pnr[0].tin.Trim();

                    //}
                    //else
                    //{
                    //    message = list_Pnr[0].tin.Trim();                        
                    //}
                }
            }

        }
        catch (Exception ex)
        {
        }
        //return message;
    }


    [System.Web.Services.WebMethod()]
    public static string GetPgChargeByMode(string paymode)
    {
        string TransCharge = "0~P";
        string PgCharge = "0";
        string ChargeType = "0";
        PG.PaymentGateway objP = new PG.PaymentGateway();
        //' Dim PaymentMode As String = rblPaymentMode.SelectedValue
        //'PaymentMode = rblPaymentMode.SelectedValue
        try
        {
            //DataTable pgDT = objP.GetPgTransChargesByMode(paymode, "GetPgCharges");
            DataTable pgDT = objP.GetPgTransChargesByModeByAgentWise(UserID, paymode, "GetPgCharges");
            if (pgDT != null)
            {
                if (pgDT.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["Charges"])))
                    {
                        PgCharge = Convert.ToString(pgDT.Rows[0]["Charges"]).Trim();
                    }
                    else
                    {
                        PgCharge = "0";
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(pgDT.Rows[0]["ChargesType"])))
                    {
                        ChargeType = Convert.ToString(pgDT.Rows[0]["ChargesType"]).Trim();
                    }
                    else
                    {
                        ChargeType = "P";
                    }
                    TransCharge = PgCharge + "~" + ChargeType;
                }
            }
        }
        catch (Exception ex)
        {
            TransCharge = "0~P";
        }
        return TransCharge;
    }
}
