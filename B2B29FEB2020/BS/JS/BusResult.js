var RHandler;
var Original_Fare = 0; var title;
var ta_Netfare = 0; var ta_Totfare = 0;
var ta_totComm = 0; var ta_totTds = 0;
var admrkpAmt = 0; var agmrkpAmt = 0;
var totSeat = ""; var totseatFare = "";
var rel; var original_Fare = "";
var FarewithMarkup1 = ""; var OriginalSeat_fare = "";
$(document).ready(function() {
    RHandler = new ResHelper;
    RHandler.BindEvents();  
});
var TotTrips;
var OW_SL = 0; var RT_SL = 0;
var BusAr;
var ResHelper = function() {
    this.SOURCE = $("#txtsrc");
    this.HID_SOURCE = $("#txthidsrc");
    this.DESTINATION = $("#txtdest");
    this.HID_DESTINATION = $("#txthiddest");
    this.DEPARTDATE = $("#txtdate");
    this.HIDDEPARTDATE = $("#hiddepart");
    this.BtnSearch = $("#btnsearch");
    this.PASSENGER = $("#ddlpax");
    this.SEATTYPE = $("#ddlseat");
    this.CS = $("#CS");
    this.Filter = $("#Filter");
    this.BusMatrix1 = $("#BusMatrix1");
    this.response;
    this.serviceID;
    this.seatType;
    this.provider;
    this.seatfare;
    this.bPoint;
    this.dPoint;
    this.traveler;
    this.totPrice = 0;
    this.ladiesSeat = "";
    this.boardpoint;
    this.droppoint;
    this.boardpointId;
    this.droppointId;
    this.markup;
    this.markpFare;
    this.IdProof;
    this.bustype;
    this.fareArr;
    this.canPolicy;
    this.partialcancel;
    this.ArrtimeAB;
    this.DEptTimeAB;
    this.DUrTimeAB;
    this.DGetArrrTimeAB = new Array();
}
ResHelper.prototype.BindEvents = function() {
    var h = this;
    h.getAvalaibility(h);
}
ResHelper.prototype.getCommandTds = function(a, b, c) {
    var h = this;
    $(".farebreakup").html("<img src='" + UrlBase + "BS/images/load.gif' alt='' />");
    var comUrl = UrlBase + "BS/WebService/CommonService.asmx/getCommissionList";
    $.ajax({
        url: comUrl,
        data: "{'seatNo':'" + a + "','seatFare':'" + b + "','provider':'" + c + "'}",
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function(data) {
            var comResult = data.d;
            var farebreakup = "<table cellpadding='0' cellspacing='0'>";
            if (b.indexOf(',') > 0) {
                farebreakup += "<tr>";
                var f = b.split(',');
                farebreakup += "<td class='bg'><b>Lowest Fare:</b>&nbsp; " + f[0] + "</td>";
            }
            else {
                farebreakup += "<tr>";
                farebreakup += "<td class='bg'><b>Fare:</b>&nbsp;" + b + "</td>";
            }
            farebreakup += "<td class='bg'><b>Srv.Charge:</b>&nbsp;" + comResult[0].serviceChrg + "</td>";
            farebreakup += "<td class='bg'><b>Total Fare:</b>&nbsp;" + comResult[0].taTotFare + "</td>";
            farebreakup += "</tr>";
            farebreakup += "<tr>";
            farebreakup += "<td class='bg'><b>Commission(-):</b>&nbsp;" + comResult[0].adcomm + "</td>";
            farebreakup += "<td class='bg'><b>TDS(+):</b>&nbsp;" + comResult[0].taTds + "</td>";
            farebreakup += "<td class='bg'><b>Net Fare:</b>&nbsp; " + comResult[0].taNetFare + "</td>";
            farebreakup += "</tr>";
            farebreakup += "</table>";
            $(".farebreakup").html(farebreakup);

        },
        error: function(XMLHttpRequest, textStatus, errorThrown)
        { return textStatus; }


    });
}
ResHelper.prototype.getAvalaibility = function(b) {
    var h = b;
    $.blockUI({ message: $('#basic-modal-content') });
    var oTable = ""; var oTable1 = ""; var OnlyOWTable = "";
    var MinPriceOW; var MaxPriceOW; var MinPriceRT; var MaxPriceRT; var TotRecRT;
    var MinDepTimeOW; var MaxDepTimeOW; var MinDepTimeRT; var MaxDepTimeRT;
    h.CS.html("<strong><br/>" + h.SOURCE.val().replace(/%20/g, " ") + " To " + h.DESTINATION.val().replace(/%20/g, " ") + " On " + h.HIDDEPARTDATE.val().replace(/%20/g, " ") + "<br/></strong>");
    var jrneyDate = new Date(h.HIDDEPARTDATE.val().replace("-", "/"));
    var dayName = new Array("Sunday", "Monday", "TuesDay", "WednesDay", "ThursDay", "FriDay", "SaturDay");
    $('#source').html("<br/>From:" + h.SOURCE.val().replace(/%20/g, " ") + "<br />" + "To:" + "<br />" + h.DESTINATION.val().replace(/%20/g, " ") + "<br />" + "Travel On:" + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + jrneyDate.getFullYear() + "<br/>");
    //-----------------------Result List---------------------------------------------------------------------------//
    var Url = UrlBase + "BS/WebService/CommonService.asmx/getJourneyResult";
    $.ajax({
        url: Url,
        data: "{'src':'" + h.SOURCE.val().replace(/%20/g, " ") + "','dest':'" + h.DESTINATION.val().replace(/%20/g, " ") + "','jDate':'" + h.HIDDEPARTDATE.val().replace(/%20/g, " ") + "','noofpax':'" + h.PASSENGER.val().replace(/%20/g, " ") + "','seattype':'" + h.SEATTYPE.val().replace(/%20/g, " ") + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function(data) {
            h.response = data.d;
            TotTrips = 0;
            if (h.response != 0) {
                var bp; var dp; var board; var drop; var time;
                var cancelpolicy; var can_policy;
                var mytable = "<br /><table cellpadding='0' cellspacing='0' class='mygrid' border='0' id='availabilitytbl'>";
                mytable += "<thead><tr>";
                mytable += "<th style='width:25%; padding-left:2%;'><img src='Images/bus.png' />TRAVELS</th>";
                mytable += "<th style='width:25%;'><img src='Images/bus-o.png' />BUS TYPE</th>";
                mytable += "<th style='width:10%;'>DEPARTS</th><th style='width:10%;'><img src='Images/watch-d.png' />DEPARTS</th>";
                mytable += "<th style='width:10%;'>ARRIVES</th><th style='width:10%;'><img src='Images/watch-a.png' />ARRIVES</th>";
                // mytable += "<th style='width:10%;'><img src='Images/watch.png' />DURATION</th>";
                mytable += "<th style='width:10%;'><img src='Images/seats.png' />SEATS</th>";
                mytable += "<th style='width:10%;'>FARE</th><th style='width:10%;' class='point'><img src='Images/fare.png'/>FARE</th></tr></thead>";
                mytable += "<tbody>";
                if (h.response.length > 1) {
                    BusAr = new Array(h.response.length - 1);
                    for (var i = 0; i <= parseInt(h.response.length) - 1; i++) {
                     
                        board = ""; drop = ""; cancelpolicy = ""; can_policy = "";
                        FarewithMarkup1 = ""; OriginalSeat_fare = "";
                        board += "<div id='divbp" + i + "' style='line-height:16px;'><span style='color:#175d80; font-size:12px;'>Departure Time<span> (boarding point)</span><br /><span style='color:#175d80; font-size:12px;'>Expected Duration<span>" + h.response[i].Dur_Time + "</span></span><br />"; drop += "<div id='divdp" + i + "' style='line-height:16px;'><span style='color:#175d80; font-size:12px;'>Arrival Time<span> (droping point)</span></span><br /><span style='color:#175d80; font-size:12px;'>Expected Duration<span>" + h.response[i].Dur_Time + "</span></span><br />";
                        cancelpolicy += "<div id='divcancel" + i + "' style='line-height:16px;'><span style='color:#175d80; font-size:14px;'>Cancellation Policy</span><br /><span style='color:#175d80; font-size:12px;'>Departure Hours</span>&nbsp;&nbsp;<span style='color:#175d80; font-size:12px;'>Can.Charge</span><br />";
                        if (jQuery.type(h.response[i].seatfare) != "string") {
                            bp = new Array(); dp = new Array();
                            BusAr[i] = h.response[i].serviceType;
                            mytable += "<tr id='" + i + "' custom='" + BusAr[i] + "' style='vertical-align:top;' >";
                            if (h.response[i].provider_name == "AB") {
                                if (h.response[i].bdPoint.length > 0) {
                                    for (var j = 0; j <= h.response[i].bdPoint.length - 1; j++) {
                                        var bdRes = h.response[i].bdPoint[j].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                                        var bdRes1 = bdRes[1].replace("|", "").split('@');
                                        if ($.trim(bdRes1[1]) != "") {
                                            board += $.trim(bdRes1[0]) + "               " + $.trim(bdRes1[2]) + "(landmark:" + $.trim(bdRes1[1]) + ")<br />";
                                        }
                                        else {
                                            board += $.trim(bdRes1[0]) + "               " + $.trim(bdRes1[2]) + "<br />";
                                        }
                                    }
                                }
                                if (h.response[i].drPoint.length > 0) {
                                    for (var k = 0; k <= h.response[i].drPoint.length - 1; k++) {
                                        var drRes = h.response[i].drPoint[k].replace('^^', '@').replace('#', '@').replace('!', '@').replace("-", "@").split('~');
                                        if (drRes.length > 1) {
                                            var drRes1 = drRes[1].split('@');
                                        }
                                        else {
                                            var drRes1 = drRes[0].split('@');
                                        }
                                        drop += drRes1[1] + "               " + drRes1[2] + "<br />";
                                    }
                                }
                                var policy = h.response[i].canPolicy_AB;
                                var cutTime = policy[1].split(',');
                                var refund = policy[2].split(',');
                                if (($.trim(policy[3])).toUpperCase() == "P") {
                                    for (var c = 0; c <= cutTime.length - 1; c++) {
                                        for (var t = 0; t <= refund.length - 1; t++) {
                                            if (c == t) {
                                                if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                                    cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                                    can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (($.trim(policy[3])).toUpperCase() == "F") {
                                    for (var c = 0; c <= cutTime.length - 1; c++) {
                                        for (var t = 0; t <= refund.length - 1; t++) {
                                            if (c == t) {
                                                if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                                    cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                                    can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (h.response[i].provider_name == "RB") {
                                time = getTimeDuration(h.response[i].arrTime, h.response[i].departTime, h.HIDDEPARTDATE.val());
                                if (h.response[i].bdPoint.length > 1) {
                                    for (var j = 0; j <= h.response[i].bdPoint.length - 1; j++) {
                                        var bdRes = $.parseJSON(h.response[i].bdPoint[j]);
                                        board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                                    }
                                }
                                else {
                                    var bdRes = $.parseJSON(h.response[i].bdPoint[0]);
                                    board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                                }
                                board += "</div>";
                                if (h.response[i].drPoint.length > 1) {
                                    for (var k = 0; k <= h.response[i].drPoint.length - 1; k++) {
                                        var drRes = $.parseJSON(h.response[i].drPoint[k]);
                                        drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                                    }
                                }
                                else {
                                    var drRes = $.parseJSON(h.response[i].drPoint[0]);
                                    drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                                }
                                drop += "</div>";
                                var policy = h.response[i].canPolicy_RB.split(';');
                                for (var k = 0; k < policy.length - 1; k++) {
                                    var canstr = policy[k].split(':');
                                    if (canstr[1] != "-1") {
                                        cancelpolicy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                        can_policy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                    }
                                    else {
                                        cancelpolicy += "Before" + " " + canstr[0] + " " + "  " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                        can_policy += "Before" + " " + canstr[0] + " " + "  " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                    }
                                }
                            }

                            else if (h.response[i].provider_name == "GS") {
                                time = GetNewDate(h.HIDDEPARTDATE.val(), h.response[i].arrTime, h.response[i].departTime, h.response[i].provider_name, h.response[i].Dur_Time);
                                //if (h.response[i].bdPoint.length > 1) {
                                //    for (var j = 0; j <= h.response[i].bdPoint.length - 1; j++) {
                                //        var bdRes = $.parseJSON(h.response[i].bdPoint[j]);
                                //        board += bdRes.location + "     " + Show_Time_12(bdRes.time.trim().split(':')[0], bdRes.time.trim().split(':')[1]) + "<br />";
                                //    }
                                //}
                                //else {
                                //    var bdRes = $.parseJSON(h.response[i].bdPoint[0]);
                                //    board += bdRes.location + "     " + Show_Time_12(bdRes.time.trim().split(':')[0], bdRes.time.trim().split(':')[1]) + "<br />";
                                //}
                                board += "</div>";
                                //if (h.response[i].drPoint.length > 1) {
                                //    //for (var k = 0; k <= h.response[i].drPoint.length - 1; k++) {
                                //    //    var drRes = $.parseJSON(h.response[i].drPoint[k]);
                                //    //    drop += drRes.location + "     " + Show_Time_12(drRes.time.trim().split(':')[0], drRes.time.trim().split(':')[1]) + "<br />";
                                //    //}
                                //}
                                //else {
                                //    var drRes = $.parseJSON(h.response[i].drPoint[0]);
                                //    drop += drRes.location + "     " + Show_Time_12(drRes.time.trim().split(':')[0], drRes.time.trim().split(':')[1]) + "<br />";
                                //}
                                drop += "</div>";
                                cancelpolicy += "0-1 Day Cancellation Charges 25 % of Basic Fare" + "%<br />";
                                can_policy += "0-1 Day Cancellation Charges 25 % of Basic Fare" + "%<br />";
                                cancelpolicy += "2-5 Day Cancellation Charges 20 % of Basic Fare" + "%<br />";
                                can_policy += "2-5 Day Cancellation Charges 20 % of Basic Fare" + "%<br />";
                                cancelpolicy += "6-10 Day Cancellation Charges 15 % of Basic Fare" + "%<br />";
                                can_policy += "6-10 Day Cancellation Charges 15 % of Basic Fare" + "%<br />";
                                cancelpolicy += "11-20 Day Cancellation Charges 10 % of Basic Fare" + "%<br />";
                                can_policy += "11-20 Day Cancellation Charges 10 % of Basic Fare" + "%<br />";
                                cancelpolicy += "21-60 Day Cancellation Charges 5 % of Basic Fare" + "%<br />";
                                can_policy += "21-60 Day Cancellation Charges 5 % of Basic Fare" + "%<br />";
                            }
                            mytable += "<td style='width:250px; padding-left:20px;'>" + h.response[i].traveler.trim().replace('*', '') + "<br /><div class='canpolicy'><div class='cannan'>Can. Policy</div><br /><div class='can' style='border: 1px solid #175d80; z-index:999;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + cancelpolicy + "</div></div></td>";
                            if (h.response[i].busType != null) {
                                mytable += "<td>" + h.response[i].serviceType + "(" + h.response[i].busType + ")</td>";
                            }
                            else {
                                mytable += "<td>" + h.response[i].serviceType + "</td>";
                            }
                            if (h.response[i].provider_name == "RB") {
                                mytable += "<td >" + ConvertTime(time[0]) + "</td>";
                                mytable += "<td class='board'>" + time[0] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                                mytable += "<td >" + ConvertTime(time[1]) + "</td>";
                                mytable += "<td class='drop'>" + time[1] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";

                            }
                            else if (h.response[i].provider_name == "AB") {
                                mytable += "<td >" + ConvertTime(h.response[i].departTime) + "</td>";
                                mytable += "<td class='board'>" + h.response[i].departTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                                mytable += "<td >" + ConvertTime(h.response[i].arrTime) + "</td>";
                                mytable += "<td class='drop'>" + h.response[i].arrTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";
                            }

                            else if (h.response[i].provider_name == "GS") {
                                mytable += "<td >" + Show_Time_12(h.response[i].departTime.trim().split(':')[0], h.response[i].departTime.trim().split(':')[1]) + "</td>";
                                mytable += "<td class='board'>" + Show_Time_12(h.response[i].departTime.trim().split(':')[0], h.response[i].departTime.trim().split(':')[1]) + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                                mytable += "<td >" + Show_Time_12(h.response[i].arrTime.trim().split(':')[0], h.response[i].arrTime.trim().split(':')[1]) + "</td>";
                                mytable += "<td class='drop'>" + Show_Time_12(h.response[i].arrTime.trim().split(':')[0], h.response[i].arrTime.trim().split(':')[1]) + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";
                            }

                            mytable += "<td>" + h.response[i].remainingSeat + "</td>";

                            for (var t = 0; t <= h.response[i].seat_Originalfare.length - 1; t++) {
                                OriginalSeat_fare += $.trim(h.response[i].seat_Originalfare[t]) + ",";
                                FarewithMarkup1 += $.trim(h.response[i].seat_farewithMarkp[t]) + ",";
                            }

                            OriginalSeat_fare = OriginalSeat_fare.substring(0, OriginalSeat_fare.length - 1);
                            FarewithMarkup1 = FarewithMarkup1.substring(0, FarewithMarkup1.length - 1);

                            mytable += "<td visible='false'>" + h.response[i].seat_farewithMarkp[0] + "</td>";
                            if (h.response[i].provider_name=="GS")
                                mytable += "<td align='center'><div class='divfare' rel='" + h.response[i].serviceID + "#" + h.response[i].SeatType + "#" + h.response[i].provider_name + "#" + OriginalSeat_fare + "#" + h.response[i].traveler.replace(/\*|'/, "") + "#" + FarewithMarkup1 + "#" + h.response[i].idproofReq + "#" + h.response[i].serviceType + "#" + can_policy + "#" + h.response[i].partialCanAllowed + "'><img src='Images/rupee.png'/><span style='font-size:14px;'>" + FarewithMarkup1 + "</span><div id='strimg_" + i + "' style='display:none; position:absolute; background:#f9f9f9; border:1px solid #d1d1d1; padding: 5px 10px; border-radius:3px; box-shadow:0px 1px 5px #000; color:#000;'>* Basic Fare : " + h.response[i].seat_farewithMarkp[0] + "(Adult) / " + h.response[i].seat_farewithMarkp[1] + "(Child)<br/>  Total fare and fare breakup wiil be display after confirmation of passenger. </div><img id='strimg" + i + "' onmouseover='funstrImghidefalse(this.id)'  onmouseout='funmouseout(this.id)'  src='Images/star_red1.png' class='loader' align='right'/><img src='Images/smload.gif' class='loader' style='display:none;' align='right' /></div><div class='breakup' rel='" + h.response[i].seat_Originalfare + "," + h.response[i].provider_name + "' >Fare Breakup<div class='farebreakup'></div></div></td>";
                            else
                                mytable += "<td align='center'><div class='divfare' rel='" + h.response[i].serviceID + "#" + h.response[i].SeatType + "#" + h.response[i].provider_name + "#" + OriginalSeat_fare + "#" + h.response[i].traveler.replace(/\*|'/, "") + "#" + FarewithMarkup1 + "#" + h.response[i].idproofReq + "#" + h.response[i].serviceType + "#" + can_policy + "#" + h.response[i].partialCanAllowed + "'><img src='Images/rupee.png'/>" + FarewithMarkup1 + "<img src='Images/smload.gif' class='loader' style='display:none;' align='right' /></div><div class='breakup' rel='" + h.response[i].seat_Originalfare + "," + h.response[i].provider_name + "' >Fare Breakup<div class='farebreakup'></div></div></td>";

                            mytable += "</tr>";
                        }
                        else {
                            bp = new Array(); dp = new Array();
                            BusAr[i] = h.response[i].serviceType;
                            mytable += "<tr id='" + i + "' custom='" + BusAr[i] + "' style='vertical-align:top;'>";

                            if (h.response[i].provider_name == "AB") {
                                if (h.response[i].bdPoint.length > 0) {
                                    for (var j = 0; j <= h.response[i].bdPoint.length - 1; j++) {
                                        var bdRes = h.response[i].bdPoint[j].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                                        if (bdRes[3] != "") {
                                            board += $.trim(bdRes[3]) + "     ( landmark: " + $.trim(bdRes[1]) + " )<br />";
                                           // h.DGetArrrTimeAB.push(GetNewDateTimeF(h.HIDDEPARTDATE.val(), $.trim(bdRes[3]), h.response[i].Dur_Time))
                                        }
                                        else {
                                            board += $.trim(h.response[i].departTime) + "    ( landmark: " + $.trim(bdRes[1]) + " )<br />";
                                         //   h.DGetArrrTimeAB.push(GetNewDateTimeF(h.HIDDEPARTDATE.val(), h.response[i].departTime, h.response[i].Dur_Time))
                                        }
                                        //                                        var bdRes1 = bdRes[1].replace("|", "").split('@');
                                        //                                        if ($.trim(bdRes1[1]) != "") {
                                        //                                            board += $.trim(bdRes1[0]) + "               " + $.trim(bdRes1[2]) + "(landmark:" + $.trim(bdRes1[1]) + ")<br />";
                                        //                                        }
                                        //                                        else {
                                        //                                            board += $.trim(bdRes1[0]) + "               " + $.trim(bdRes1[2]) + "<br />";
                                        //                                        }
                                    }
                                }
                                if (h.response[i].drPoint.length > 0) {
                                    var drRes1 = "";
                                    for (var k = 0; k < h.response[i].drPoint.length; k++) {
                                        var drRes = h.response[i].drPoint[k].replace('^^', '@').replace('#', '@').replace('!', '@').replace("-", "@").split('~');

                                        if (drRes[3] != "") {
                                            drop += drRes[3] + "               " + drRes[1] + "<br />";
                                        }
                                        else {
                                            drop += drRes[1] + "<br />";
                                        }


                                    }
                                    //                                  //  for (var k = 0; k <= h.response[i].drPoint.length - 1; k++) {
                                    //                                        var drRes = h.response[i].drPoint[k].replace('^^', '@').replace('#', '@').replace('!', '@').replace("-", "@").split('~');
                                    //                                        if (drRes.length > 1) {
                                    //                                            var drRes1 = drRes[1].split('@');
                                    //                                        }
                                    //                                        else {
                                    //                                            var drRes1 = drRes[0].split('@');
                                    //                                        }
                                    //                                        drop += drRes1[1] + "               " + drRes1[2] + "<br />";
                                    //                                    }
                                }
                                var policy = h.response[i].canPolicy_AB;
                                var cutTime = policy[1].split(',');
                                var refund = policy[2].split(',');
                                if (($.trim(policy[3])).toUpperCase() == "P") {
                                    for (var c = 0; c <= cutTime.length - 1; c++) {
                                        for (var t = 0; t <= refund.length - 1; t++) {
                                            if (c == t) {
                                                if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                                    cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                                    can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (($.trim(policy[3])).toUpperCase() == "F") {
                                    for (var c = 0; c <= cutTime.length - 1; c++) {
                                        for (var t = 0; t <= refund.length - 1; t++) {
                                            if (c == t) {
                                                if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                                    cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                                    can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (h.response[i].provider_name == "RB") {
                                time = getTimeDuration(h.response[i].arrTime, h.response[i].departTime, h.HIDDEPARTDATE.val());
                                if (h.response[i].bdPoint.length > 1) {
                                    for (var j = 0; j <= h.response[i].bdPoint.length - 1; j++) {
                                        var bdRes = $.parseJSON(h.response[i].bdPoint[j]);
                                        board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                                    }
                                }
                                else {
                                    var bdRes = $.parseJSON(h.response[i].bdPoint[0]);
                                    board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                                }
                                board += "</div>";
                                if (h.response[i].drPoint.length > 1) {
                                    for (var k = 0; k <= h.response[i].drPoint.length - 1; k++) {
                                        var drRes = $.parseJSON(h.response[i].drPoint[k]);
                                        drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                                    }
                                }
                                else {
                                    var drRes = $.parseJSON(h.response[i].drPoint[0]);
                                    drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                                }
                                drop += "</div>";
                                var policy = h.response[i].canPolicy_RB.split(';');
                                for (var k = 0; k < policy.length - 1; k++) {
                                    var canstr = policy[k].split(':');
                                    if (canstr[1] != "-1") {
                                        cancelpolicy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                        can_policy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                    }
                                    else {
                                        cancelpolicy += "Before" + " " + canstr[0] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                        can_policy += "Before" + " " + canstr[0] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                    }
                                }
                            }
                            mytable += "<td style='width:250px; padding-left:20px;'>" + h.response[i].traveler + "<br /><div class='canpolicy'><div class='cannan'>Can. Policy</div><br /><div class='can' style='border: 1px solid #175d80; z-index:999;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + cancelpolicy + "</div></div></td>";
                            if (h.response[i].busType != null) {
                                mytable += "<td>" + h.response[i].serviceType + "(" + h.response[i].busType + ")</td>";
                            }
                            else {
                                mytable += "<td>" + h.response[i].serviceType + "</td>";
                            }
                            if (h.response[i].provider_name == "RB") {
                                mytable += "<td >" + ConvertTime(time[0]) + "</td>";
                                mytable += "<td class='board'>" + time[0] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                                mytable += "<td >" + ConvertTime(time[1]) + "</td>";
                                mytable += "<td class='drop'>" + time[1] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";

                            }
                            else if (h.response[i].provider_name == "AB") {
                                mytable += "<td >" + ConvertTime(h.response[i].departTime) + "</td>";
                                mytable += "<td class='board'>" + h.response[i].departTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                                mytable += "<td >" + ConvertTime(h.response[i].arrTime) + "</td>";
                                mytable += "<td class='drop'>" + h.response[i].arrTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";
                            }

                            mytable += "<td>" + h.response[i].remainingSeat + "</td>";

                            for (var t = 0; t <= h.response[i].seat_Originalfare.length - 1; t++) {
                                OriginalSeat_fare += $.trim(h.response[i].seat_Originalfare[t]) + ",";
                                FarewithMarkup1 += $.trim(h.response[i].seat_farewithMarkp[t]) + ",";
                            }

                            OriginalSeat_fare = OriginalSeat_fare.substring(0, OriginalSeat_fare.length - 1);
                            FarewithMarkup1 = FarewithMarkup1.substring(0, FarewithMarkup1.length - 1);

                            mytable += "<td visible='false'>" + h.response[i].seat_farewithMarkp[0] + "</td>";
                            mytable += "<td align='center'><div class='divfare' rel='" + h.response[i].serviceID + "#" + h.response[i].SeatType + "#" + h.response[i].provider_name + "#" + OriginalSeat_fare + "#" + h.response[i].traveler.replace("'", "") + "#" + FarewithMarkup1 + "#" + h.response[i].idproofReq + "#" + h.response[i].serviceType + "#" + can_policy + "#" + h.response[i].partialCanAllowed + "'><img src='Images/rupee.png'/>" + FarewithMarkup1 + "<img src='Images/smload.gif' class='loader' style='display:none;' align='right' /></div><div class='breakup' rel='" + h.response[i].seat_Originalfare + "," + h.response[i].provider_name + "' >Fare Breakup<div class='farebreakup'></div></div></td>";
                            mytable += "</tr>";
                        }
                    }
                }
                else {

                    bp = new Array(); dp = new Array(); board = ""; drop = ""; cancelpolicy = ""; can_policy = "";
                    FarewithMarkup1 = ""; OriginalSeat_fare = "";
                    BusAr = new Array(1);
                    BusAr[0] = h.response[0].serviceType;
                    mytable += "<tr id='0' custom='" + BusAr[0] + "' style='vertical-align:top;'>";
                    board += "<div id='divbp0' style='line-height:16px;'><span style='color:#175d80; font-size:12px;'>Departure<span> (boarding point)</span></span><br />"; drop += "<div id='divdp0' style='line-height:16px;'><span style='color:#175d80; font-size:12px;'>Arrival<span> (droping point)</span></span><br />";
                    cancelpolicy += "<div id='divcancel' style='line-height:16px;'><span style='color:#175d80; font-size:14px;'>Cancellation Policy</span><br /><span style='color:#175d80; font-size:12px;'>Departure Hours</span>&nbsp;&nbsp;<span style='color:#175d80; font-size:12px;'>Can.Charge</span><br />";

                    if (h.response[0].provider_name == "AB") {
                        if (h.response[0].bdPoint.length > 0) {
                            for (var j = 0; j <= h.response[0].bdPoint.length - 1; j++) {
                                var arrB = h.response[0].bdPoint[j].split('~');
                                arrB = arrB[1].replace("^^", "").split('!');
                                board += arrB[0] + "           " + arrB[1];
                            }
                        }
                        if (h.response[0].drPoint.length > 0) {
                            for (var k = 0; k <= h.response[0].drPoint.length - 1; k++) {
                                var arrD = h.response[0].drPoint[k].split('~');
                                arrD = arrD[0].split('-');
                                drop += arrD[1].replace("^^", "") + "             " + arrD[2];
                            }
                        }
                        var policy = h.response[0].canPolicy_AB;
                        var cutTime = policy[1].split(',');
                        var refund = policy[2].split(',');
                        if (($.trim(policy[3])).toUpperCase() == "P") {
                            for (var c = 0; c <= cutTime.length - 1; c++) {
                                for (var t = 0; t <= refund.length - 1; t++) {
                                    if (c == t) {
                                        if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                            cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                            can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                                        }
                                    }
                                }
                            }
                        }
                        else if (($.trim(policy[3])).toUpperCase() == "F") {
                            for (var c = 0; c <= cutTime.length - 1; c++) {
                                for (var t = 0; t <= refund.length - 1; t++) {
                                    if (c == t) {
                                        if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                                            cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                            can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (h.response[0].provider_name == "RB") {
                        time = getTimeDuration(h.response[0].arrTime, h.response[0].departTime, h.HIDDEPARTDATE.val());
                        if (h.response[0].bdPoint.length > 1) {
                            for (var j = 0; j <= h.response[0].bdPoint.length - 1; j++) {
                                var bdRes = $.parseJSON(h.response[0].bdPoint[j]);
                                board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                            }
                        }
                        else {
                            var bdRes = $.parseJSON(h.response[0].bdPoint[0]);
                            board += bdRes.location + "     " + getTimeDuration(bdRes.time, '', '') + "<br />";
                        }
                        board += "</div>";
                        if (h.response[0].drPoint.length > 1) {
                            for (var k = 0; k <= h.response[0].drPoint.length - 1; k++) {
                                var drRes = $.parseJSON(h.response[0].drPoint[k]);
                                drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                            }
                        }
                        else {
                            var drRes = $.parseJSON(h.response[0].drPoint[0]);
                            drop += drRes.location + "     " + getTimeDuration(drRes.time, '', '') + "<br />";
                        }
                        drop += "</div>";
                        var policy = h.response[0].canPolicy_RB.split(';');
                        for (var k = 0; k <policy.length - 1; k++) {
                            var canstr = policy[k].split(':');
                            if (canstr[1] != "-1") {
                                cancelpolicy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                can_policy += "Between" + " " + canstr[0] + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                            }
                            else {
                                cancelpolicy += "Before" + " " + canstr[0] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                                can_policy += "Before" + " " + canstr[0] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                            }
                        }
                    }
                    mytable += "<td style='width:250px; padding-left:20px;'>" + h.response[0].traveler + "<br /><div class='canpolicy'><div class='cannan'>Can. Policy</div><br /><div class='can' style='border: 1px solid #175d80; z-index:999;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + cancelpolicy + "</div></div></td>";
                    if (h.response[0].busType != null) {
                        mytable += "<td>" + h.response[0].serviceType + "(" + h.response[0].busType + ")</td>";
                    }
                    else {
                        mytable += "<td>" + h.response[0].serviceType + "</td>";
                    }
                    if (h.response[0].provider_name == "RB") {
                        mytable += "<td >" + ConvertTime(time[0]) + "</td>";
                        mytable += "<td class='board'>" + time[0] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                        mytable += "<td >" + ConvertTime(time[1]) + "</td>";
                        mytable += "<td class='drop'>" + time[1] + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";
                    }
                    else if (h.response[0].provider_name == "AB") {
                        mytable += "<td >" + ConvertTime(h.response[0].departTime) + "</td>";
                        mytable += "<td class='board'>" + h.response[0].departTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + board + "<div style='clear:both;'></div></div></td>";
                        mytable += "<td >" + ConvertTime(h.response[0].arrTime) + "</td>";
                        mytable += "<td class='drop'>" + h.response[0].arrTime + "<div class='abc' style='border: 1px solid #20313F;'><div style='position:absolute; left:-19px;'><img src='Images/arrw.png'/></div>" + drop + "</div></td>";
                    }

                    mytable += "<td>" + h.response[0].remainingSeat + "</td>";

                    for (var t = 0; t <= h.response[0].seat_Originalfare.length - 1; t++) {
                        OriginalSeat_fare += $.trim(h.response[0].seat_Originalfare[t]) + ",";
                        FarewithMarkup1 += $.trim(h.response[0].seat_farewithMarkp[t]) + ",";
                    }
                    OriginalSeat_fare = OriginalSeat_fare.substring(0, OriginalSeat_fare.length - 1);
                    FarewithMarkup1 = FarewithMarkup1.substring(0, FarewithMarkup1.length - 1);

                    mytable += "<td visible='false'>" + h.response[0].seat_farewithMarkp[0] + "</td>";
                    mytable += "<td align='center'><div class='divfare' rel='" + h.response[0].serviceID + "#" + h.response[0].SeatType + "#" + h.response[0].provider_name + "#" + OriginalSeat_fare + "#" + h.response[0].traveler.replace("'", "") + "#" + FarewithMarkup1 + "#" + h.response[0].idproofReq + "#" + h.response[0].serviceType + "#" + can_policy + "#" + h.response[0].partialCanAllowed + "'><img src='Images/rupee.png'/>" + FarewithMarkup1 + "<img src='Images/smload.gif' class='loader' style='display:none;' align='right' /></div><div class='breakup' rel='" + h.response[0].seat_Originalfare + "," + h.response[0].provider_name + "' >Fare Breakup<div class='farebreakup'></div></div></td>";
                    mytable += "</tr>";

                }
                mytable += "</tbody>";
                mytable += "</table>";
                $("#divresult").html(mytable);
                $(document).ajaxStop($.unblockUI);
                var resultTable = $("#availabilitytbl").dataTable({
                    "sDom": '<"top"i>rt',
                    "aaSorting": [[7, 'asc']], //During Initialization DataTable is sorted according to Price.
                    "aoColumnDefs": [//{ "aDataSort": [2, 3, 4], "aTargets": [5] }, //Allows multiple columns TO BE SORTED ACCRODING TO TARGET COLUMN
                                                                {"bSortable": false, "aTargets": [0, 1, 2] },
                                                              { "aTargets": [2, 4, 7], "bVisible": false },
                                                                                         { "iDataSort": 2, "aTargets": [3] },
                                                                                          { "iDataSort": 4, "aTargets": [5] },
                                                              { "iDataSort": 7, "aTargets": [8] }
                    //{ "iDataSort": 9, "aTargets": [10]} //Sort 10th According to 8th
                                                            ],
                    "bPaginate": false, //"bFilter": false, //Enable/Disable Filtering
                    "bInfo": false,
                    "bAutoWidth": false

                });
                var B;
                if (BusAr.length > 1) {
                    B = removeDuplicates(BusAr);
                }
                else { B = BusAr; }
                create_BusFilter(B);
                h.Filter.show(); //By Default Filter is Hidden
                BusFilter(B); //Filter ON Click
                ////////////////////////////////////////Functions For Filtering Data//////////////////////////////////////////
                //Find Min-Max Value for Slider Range AND Slider-Time
                if (TotTrips == 0) {
                    //Slider Price
                    MinPriceOW = $("#availabilitytbl").find("tr:nth-child(1) td:eq(5)").text();
                    MaxPriceOW = $("#availabilitytbl").find("tr:last td:eq(5)").text();
                    Min = MinPriceOW; var ArrMin = MinPriceOW.split("/"); Min = ArrMin[0];
                    Max = MaxPriceOW; var ArrMax = MaxPriceOW.split("/"); Max = ArrMax[0];
                    //Slider Dep Time
                }
                //RT
                if (TotTrips == 1) {
                    //Slider Price
                    MinPriceOW = $("#availabilitytbl").find("tr:nth-child(1) td:eq(5)").text();
                    var ArrMin = MinPriceOW.split("/");
                    MinPriceOW = ArrMin[0];
                    MaxPriceOW = $("#availabilitytbl").find("tr:last td:eq(6)").text();
                    var ArrMax = MaxPriceOW.split("/");
                    MaxPriceOW = ArrMax[0];
                    MinPriceRT = $("#availabilitytbl").find("tr:nth-child(1) td:eq(6)").text();
                    var ArrMin = MinPriceRT.split("/");
                    MinPriceRT = ArrMin[0];
                    MaxPriceRT = $("#availabilitytbl").find("tr:last td:eq(6)").text();
                    var ArrMax = MaxPriceRT.split("/");
                    MaxPriceRT = ArrMax[0];

                    if (MinPriceOW <= MinPriceRT) { Min = MinPriceOW; } else { Min = MinPriceRT; }
                    if (MaxPriceOW > MaxPriceRT) { Max = MaxPriceOW; } else { Max = MaxPriceRT; }
                    //Slider Dep Time
                    MinDepTimeOW = Math.min.apply(Math, DepTimeOW).toString(); if (MinDepTimeOW.length == 1) { var str1 = "000"; MinDepTimeOW = str1.concat(MinDepTimeOW) } else if (MinDepTimeOW.length == 2) { var str1 = "00"; MinDepTimeOW = str1.concat(MinDepTimeOW) } else if (MinDepTimeOW.length == 3) { var str1 = "0"; MinDepTimeOW = str1.concat(MinDepTimeOW) };
                    MaxDepTimeOW = Math.max.apply(Math, DepTimeOW).toString();
                    MinDepTimeRT = Math.min.apply(Math, DepTimeRT).toString(); if (MinDepTimeRT.length == 1) { var str1 = "000"; MinDepTimeRT = str1.concat(MinDepTimeRT) } else if (MinDepTimeRT.length == 2) { var str1 = "00"; MinDepTimeRT = str1.concat(MinDepTimeRT) } else if (MinDepTimeRT.length == 3) { var str1 = "0"; MinDepTimeRT = str1.concat(MinDepTimeRT) };
                    MaxDepTimeRT = Math.max.apply(Math, DepTimeRT).toString();
                }
                $("#slider-range").slider("enable");
                //$("#slider-Deptime").slider("enable"); //For OutBound Results Only
                //For InBound Results Only
                //                AirLineFilter(AirSelector); // Hide Unhide Airlines on Checkbox click
                //                StopFilter(StopSelector); //Hide Unhide Airlines on Stop click
                if (TotTrips == 0) { SLIDER(Min, Max, resultTable, "", B); SLIDER_DEPTIME(resultTable, "", B); }
                else if (TotTrips == 1) { SLIDER(Min, Max, resultTable, oTable1, B); SLIDER_DEPTIME(MinDepTimeOW, MaxDepTimeOW, resultTable, oTable1, B); /*SLIDER_ARRTIME(MinArrTimeOW, MaxArrTimeOW, oTable, oTable1);*/ }

                //                ////////////////////////////////////////FUNCTIONS FOR FILTERING DATA ENDS //////////////////////////////////////////

                $(".divfare").click(function() {
                    totSeat = ""; totseatFare = "";
                    original_Fare = ""; ta_totComm = 0; ta_totTds = 0; ta_Netfare = 0; ta_Totfare = 0;
                    admrkpAmt = 0; agmrkpAmt = 0;
                    var arr = $(this).attr("rel").split('#');
                    h.serviceID = arr[0];
                    h.seatType = arr[1];
                    h.provider = arr[2];
                    h.seatfare = arr[3];
                    h.traveler = arr[4];
                    h.markpFare = arr[5];
                    h.IdProof = arr[6];
                    h.bustype = arr[7];
                    h.canPolicy = arr[8];
                    h.partialcancel = arr[9];
                    h.getTripDetails(h);
                });
                $('.board').mouseover(function() {
                    $(this).addClass("board1");
                    $(this).removeClass("board");
                });
                $('.board').mouseout(function() {
                    $(this).addClass("board");
                    $(this).removeClass("board1");
                });

                $('.drop').mouseover(function() {
                    $(this).addClass("drop1");
                    $(this).removeClass("drop");

                });
                $('.drop').mouseout(function() {
                    $(this).addClass("drop");
                    $(this).removeClass("drop1");

                });
                $(".breakup").mouseout(function() {
                    $(this).addClass("breakup");
                    $(this).removeClass("breakup1");

                });
                $(".breakup").mouseover(function() {
                    var strsplt = $(this).attr("rel").split(',');
                    h.seatfare = "";
                    if (strsplt.length == 2) {
                        h.seatfare = strsplt[0];
                        h.provider = strsplt[1];
                    }
                    else {
                        for (var f = 0; f <= strsplt.length - 1; f++) {
                            if ($.trim(strsplt[f]) == "RB" || $.trim(strsplt[f]) == "AB") {

                                h.provider = strsplt[f];
                            }
                            else {
                                h.seatfare += strsplt[f] + ",";
                            }
                        }
                        h.seatfare = h.seatfare.substring(0, h.seatfare.length - 1);
                    }
                    $(this).addClass("breakup1");
                    h.getCommandTds("", h.seatfare, h.provider);
                });
                $('.canpolicy').mouseover(function() {
                    $(this).addClass("canpolicy1");
                    $(this).removeClass("canpolicy");
                });
                $('.canpolicy').mouseout(function() {
                    $(this).addClass("canpolicy");
                    $(this).removeClass("canpolicy1");
                });
            }
            else {
                $(document).ajaxStop($.unblockUI);
                $("#divresult").html("<b>Sorry No result Found for this date</b>");
            }
        }

    });

    // }
    //});

    //---------------------------------------------------------------------------------------------------------------//

}
ResHelper.prototype.getTripDetails = function(b) {
    var h = b;
    
    var ladiesseattxt = "";
    var res = h.response; var seatArrangement = "";
    var type = h.bustype; h.bPoint = new Array(); h.dPoint = new Array();
    var Url = UrlBase + "BS/WebService/CommonService.asmx/getSeatLayOut";
  

    if (res.length != undefined) {
        for (var i = 0; i <= res.length - 1; i++) {
            if (h.serviceID == res[i].serviceID) {
                h.bPoint = res[i].bdPoint;
                h.dPoint = res[i].drPoint;
                h.traveler = res[i].traveler;
                h.ArrtimeAB = res[i].arrTime;
                h.DEptTimeAB = res[i].departTime;
                h.DUrTimeAB = res[i].Dur_Time;

            }
        }
    }
    else {
        if (h.serviceID == res[0].serviceID) {
            h.bPoint = res[0].bdPoint;
            h.dPoint = res[0].drPoint;
            h.traveler = res[0].traveler;
            h.ArrtimeAB = res[0];
            h.DEptTimeAB = res[0];
            h.DUrTimeAB = res[0];
        }
    }
    $.ajax({
        url: Url,
        contentType: "application/json; charset=utf-8",
        data: "{'jdate':'" + h.HIDDEPARTDATE.val() + "','srcId':'" + h.SOURCE.val() + "','destId':'" + h.DESTINATION.val() + "','serviceId':'" + h.serviceID + "','seattype':'" + h.seatType + "','provider':'" + h.provider + "','fare':'" + h.seatfare + "','traveler':'" + h.traveler.replace("'", "") + "','farewithMarkp':'" + h.markpFare + "'}",
        dataType: "json",
        type: "POST",
        async: true,
        success: function (data) {
             
            var seatLayout = data.d;
         
            seatArrangement += "<table cellpadding='0' cellspacing='10' border='0' style='color:#888;' width='100%'>";
            seatArrangement += "<tr>";
            seatArrangement += "<td style='width:500px;'>";
            seatArrangement += seatLayout;
            seatArrangement += "</td>";
            seatArrangement += "<td valign='middle'>";

            seatArrangement += "<table cellspacing='0' cellpadding='5' border='0'>";
            seatArrangement += "<tr>";
            seatArrangement += "<td><img src='Images/4.png'/> Available Seat<br><img src='Images/3.png'/> Reserved For Ladies<br><img src='Images/2.png'/> Selected Seat<br><img src='Images/1.png'/> Blocked Seat <p><img src='Images/s1.png'/> Available Seat<br><img src='Images/s2.png'/> Reserved For Ladies<br><img src='Images/s3.png'/> Selected Seat<br><img src='Images/s4.png'/> Blocked Seat</p>";
            seatArrangement += "</td>";
            seatArrangement += "</tr>";
            seatArrangement += "<tr><td>";



            seatArrangement += "</td></tr></table>";

            seatArrangement += "</td></tr>";
            seatArrangement += "<tr><td valign='top'>";

            seatArrangement += "<table style='width:100%;' border='0'><tr><td style='font-weight:bold; width:30%; color:#20313F; font-size:14px;'>Boarding Point:</td>";
            seatArrangement += "<td style='font-weight:bold; width:25%; color:#20313F; font-size:14px;'>Dropping Point:</td><td  style='font-weight:bold; color:#20313F; font-size:12px;'>Expected Duration : " + h.DUrTimeAB + " </td>";
            seatArrangement += "</tr>";
            seatArrangement += "<tr>";
            seatArrangement += "<td><select name='board' id='board' style='width:150px;' class='drpBox'>";
            if (h.provider == "AB") {
                if (h.bPoint.length > 1) {
                    for (var a = 0; a <= h.bPoint.length - 1; a++) {
                        var bdRes = h.bPoint[a].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                        var bdRes1 = bdRes[1].split('@');
                        if (bdRes1.length > 3) {
                            if ($.trim(bdRes1[1]) != "") {
                                seatArrangement += "<option value='" + bdRes[0] + "(landmark:" + $.trim(bdRes1[1]) + ")'>" + bdRes1[0] + "(" + bdRes1[3] + ")" + "</option>";
                            }
                            else {
                                seatArrangement += "<option value='" + bdRes[0] + "'>" + bdRes1[0] + "(" + bdRes1[3] + ")" + "</option>";
                            }
                        }
                        else {
                            if ($.trim(bdRes1[1]) != "") {
                                seatArrangement += "<option value='" + bdRes[0] + "(landmark:" + $.trim(bdRes1[1]) + ")'>" + bdRes1[0] + "(" + bdRes1[2] + ")" + "</option>";
                            }
                            else {
                                if (bdRes[3] != "") {
                                    seatArrangement += "<option value='" + bdRes[0] + "(landmark:" + $.trim(bdRes[1]) + ")'>" + bdRes[1] + "(" + bdRes[3] + ")" + "</option>";

                                }
                                else {
                                    seatArrangement += "<option value='" + bdRes[0] + "(landmark:" + $.trim(bdRes[1]) + ")'>" + bdRes[1] + "(" + bdRes1[3] + ")" + "</option>";
                                }
                            }
                        }
                    }

                }
                else {
                    var bdRes = h.bPoint[0].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                    var bdRes1 = bdRes[1].split('@');
                    if ($.trim(bdRes1[1]) != "") {
                        seatArrangement += "<option value='" + bdRes[0] + "'>" + bdRes1[0] + "(" + bdRes1[2] + "," + "landmark:" + bdRes1[1] + ")" + "</option>";
                    }
                    else {
                        if ($.trim(bdRes1[1]) != "") {
                            seatArrangement += "<option value='" + bdRes[0] + "'>" + bdRes1[0] + "(" + bdRes1[1] + ")" + "</option>";
                        }
                        else {
                            seatArrangement += "<option value='" + bdRes[0] + "'>" + bdRes1[0] + "(" + h.DEptTimeAB + ")" + "</option>";
                        }
                    }
                }

                seatArrangement += "</select>";
                seatArrangement += "</td>";
            }
            else if (h.provider == "RB") {
                if (h.bPoint.length != undefined) {
                    for (var n = 0; n <= h.bPoint.length - 1; n++) {
                        var bdRes = $.parseJSON(h.bPoint[n]);
                        bTime = getTimeDuration(bdRes.time, "", "");
                        seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "(" + bTime[0] + ")" + "</option>";
                    }
                }
                else {
                    var bdRes = $.parseJSON(h.bPoint);
                    bTime = getTimeDuration(bdRes.time, "", "");
                    seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                }

                seatArrangement += "</select>";
                seatArrangement += "</td>";
            }
            else if (h.provider == "GS") {
            //    if (h.bPoint != null) {

                    var seatlrt;
                    var Url2 = UrlBase + "BS/WebService/CommonService.asmx/getBoardIngDropping";
                    $.ajax({
                        url: Url2,
                        contentType: "application/json; charset=utf-8",
                        data: "{'serviceId':'" + h.serviceID + "'}",
                        dataType: "json",
                        type: "POST",
                        async: true,
                        success: function (data) {
                            seatlrt = data.d;
                            h.bPoint = seatlrt[0];
                            h.dPoint = seatlrt[1];
                            if (h.bPoint.length != undefined) {
                                for (var k = 0; k <= h.bPoint.length - 1; k++) {
                                    var bdRes = $.parseJSON(h.bPoint[k]);
                                    bTime = GetNewDate(h.HIDDEPARTDATE.val(), bdRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                                    seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "(" + bTime[0] + ")" + "</option>";
                                }
                            }
                            else {
                                var bdRes = $.parseJSON(h.bPoint);
                                bTime = GetNewDate(h.HIDDEPARTDATE.val(), bdRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                                seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                            }

                            seatArrangement += "</select>";
                            seatArrangement += "</td>";
                            seatArrangement += "<td><select name='drop' id='drop' style='width:150px;' class='drpBox'>";

                            if (h.dPoint.length != undefined) {
                                for (var nl = 0; nl <= h.dPoint.length - 1; nl++) {
                                    var drRes = $.parseJSON(h.dPoint[nl]);
                                    bTime = GetNewDate(h.HIDDEPARTDATE.val(), drRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                                    seatArrangement += "<option value='" + drRes.dpId + "'>" + drRes.location + "(" + bTime[0] + ")" + "</option>";
                                }
                            }
                            else {
                                var drRes = $.parseJSON(h.bPoint);

                                bTime = GetNewDate(h.HIDDEPARTDATE.val(), drRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                                seatArrangement += "<option value='" + drRes.dpId + "'>" + drRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                            }

                            seatArrangement += "</select>";
                            seatArrangement += "</td>";


                            seatArrangement += "<td style=' width:30%;'><input type='button' id='btnContinue' name='Procced' value='Continue' class='button33' /></td>";

                            seatArrangement += "</tr></table></td><td>";
                            seatArrangement += "<table id='fare' border='0'><tr>";
                            seatArrangement += "<td id='s' style='color:#000;font-size:12px;'></td>";
                            seatArrangement += "<td id='f' style='color:#000;font-size:12px;'></td>";
                            seatArrangement += "</tr></table></td></tr></table>";

                            $("#divseat").html(seatArrangement);
                            //$('#divseat').bPopup({
                            //    speed: 650,
                            //    transition: 'slideIn'
                            //});
                            
                            $('#divseat').show();
                            funseatselectoption(h);


                        }


                    });



                    //if (h.bPoint.length != undefined) {
                    //    for (var k = 0; k <= h.bPoint.length - 1; k++) {
                    //        var bdRes = $.parseJSON(h.bPoint[k]);
                    //        bTime = GetNewDate(h.HIDDEPARTDATE.val(), bdRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                    //        seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "(" + bTime[0] + ")" + "</option>";
                    //    }
                    //}
                    //else {
                    //    var bdRes = $.parseJSON(h.bPoint);
                    //    bTime = GetNewDate(h.HIDDEPARTDATE.val(), bdRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
                    //    seatArrangement += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                    //}
             //   }

            }

            //bTime.pop();
           
            if (h.provider == "AB") {
               
                seatArrangement += "<td><select name='drop' id='drop' style='width:150px;' class='drpBox'>";
                if (h.dPoint.length > 1) {
                    for (var z = 0; z <= h.dPoint.length - 1; z++) {

                        var drRes = h.dPoint[z].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                        if (drRes[3] != "") {
                            seatArrangement += "<option value='" + drRes[0] + "'>" + drRes[1] + "(" + drRes[3] + ")" + "</option>";
                        }
                        else {
                            seatArrangement += "<option value='" + drRes[0] + "'>" + drRes[1] + "</option>";
                        }
                        //                        if (drRes.length > 1) {
                        //                            var drRes1 = drRes[1].split('@');
                        //                            seatArrangement += "<option value='" + drRes[0] + "'>" + drRes1[0] + "(" + drRes1[2] + ")" + "</option>";
                        //                        }
                        //                        else {
                        //                            var drRes1 = drRes[0].split('@');
                        //                            var drRes2 = drRes1[0].split('-');
                        //                            seatArrangement += "<option value='" + drRes2[0] + "'>" + drRes2[1] + "</option>";
                        //                        }

                    }
                }
                else {
                    if (h.dPoint.length != 0) {
                        if (h.dPoint[0].indexOf("AM") > 0 || h.dPoint[0].indexOf("PM") > 0) {
                            var drRes = h.dPoint[0].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                            var drRes1 = drRes[1].split('@');
                            seatArrangement += "<option value='" + drRes[0] + "'>" + drRes1[0] + "(" + drRes1[1] + ")" + "</option>";
                        }
                        else {
                            var drRes = h.dPoint[0].replace('^^', '@').replace('#', '@').replace('!', '@').split('~');
                            var drRes1 = drRes[0].split('@');
                            var drRes2 = drRes1[0].split('-');
                            seatArrangement += "<option value='" + drRes2[0] + "'>" + drRes2[1] + "</option>";
                        }
                    }
                    else {
                        seatArrangement += "<option value=''>Not available</option>";
                    }
                }


                seatArrangement += "</select>";
                seatArrangement += "</td>";
            }
            else if (h.provider == "RB") {
              
                seatArrangement += "<td><select name='drop' id='drop' style='width:150px;' class='drpBox'>";
                if (h.dPoint.length != undefined) {
                    for (var n = 0; n <= h.dPoint.length - 1; n++) {
                        var drRes = $.parseJSON(h.dPoint[n]);
                        bTime = getTimeDuration(drRes.time, "", "");
                        seatArrangement += "<option value='" + drRes.bpId + "'>" + drRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                    }
                }
                else {
                    var drRes = $.parseJSON(h.dPoint);
                    bTime = getTimeDuration(drRes.time, "", "");
                    seatArrangement += "<option value='" + drRes.bpId + "'>" + drRes.location + "" + "(" + bTime[0] + ")" + "</option>";
                }

                seatArrangement += "</select>";
                seatArrangement += "</td>";

            }

            if (h.provider != "GS") {
                seatArrangement += "<td style=' width:30%;'><input type='button' id='btnContinue' name='Procced' value='Continue' class='button33' /></td>";

                seatArrangement += "</tr></table></td><td>";
                seatArrangement += "<table id='fare' border='0'><tr>";
                seatArrangement += "<td id='s' style='color:#000;font-size:12px;'></td>";
                seatArrangement += "<td id='f' style='color:#000;font-size:12px;'></td>";
                seatArrangement += "</tr></table></td></tr></table>";
                $("#divseat").html(seatArrangement);
                //$('#divseat').bPopup({
                //    speed: 650,
                //    transition: 'slideIn'
                //});
                $('#divseat').show();
                funseatselectoption(h);
            }
            //else if (h.provider == "GS") {
            //    if (h.bPoint != null) {
            //        if (h.dPoint.length != undefined) {
            //            for (var nl = 0; nl <= h.dPoint.length - 1; nl++) {
            //                var drRes = $.parseJSON(h.dPoint[nl]);
            //                bTime = GetNewDate(h.HIDDEPARTDATE.val(), drRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
            //                seatArrangement += "<option value='" + drRes.dpId + "'>" + drRes.location + "(" + bTime[0] + ")" + "</option>";
            //            }
            //        }
            //        else {
            //            var drRes = $.parseJSON(h.bPoint);
            //            bTime = GetNewDate(h.HIDDEPARTDATE.val(), drRes.time, "", h.provider, ""); //getTimeDuration(bdRes.time, "", "");
            //            seatArrangement += "<option value='" + drRes.dpId + "'>" + drRes.location + "" + "(" + bTime[0] + ")" + "</option>";
            //        }
            //    }
            //}
            
//                if (h.dPoint.length != undefined) {
//                    for (var nl = 0; nl <= h.dPoint.length - 1; nl++) {
//                        var drRes = $.parseJSON(h.dPoint[nl]);
//                        bTime = GetNewDate(h.HIDDEPARTDATE.val(), "", drRes.time, h.provider, ""); // getTimeDuration(drRes.time, "", "");
//                        seatArrangement += "<option value='" + drRes.bpId + "'>" + drRes.location + "" + "(" + bTime[1] + ")" + "</option>";
//                    }
//                }
//                else {
//                    var drRes = $.parseJSON(h.dPoint);
//                    bTime = GetNewDate(h.HIDDEPARTDATE.val(), "", drRes.time, h.provider, "")//; getTimeDuration(drRes.time, "", "");
//                    seatArrangement += "<option value='" + drRes.bpId + "'>" + drRes.location + "" + "(" + bTime[1] + ")" + "</option>";
//                }
            
            //bTime.pop();
          
            //seatArrangement += "<td style=' width:30%;'><input type='button' id='btnContinue' name='Procced' value='Continue' class='button33' /></td>";

            //seatArrangement += "</tr></table></td><td>";
            //seatArrangement += "<table id='fare' border='0'><tr>";
            //seatArrangement += "<td id='s' style='color:#000;font-size:12px;'></td>";
            //seatArrangement += "<td id='f' style='color:#000;font-size:12px;'></td>";
            //seatArrangement += "</tr></table></td></tr></table>";
            //$("#divseat").html(seatArrangement);
            //$('#divseat').bPopup({
            //    speed: 650,
            //    transition: 'slideIn'
            //});
           
           
            function funseatselectoption(h) {


                var cnt = 0;
                var sseat = "";

                h.totPrice = 0;
                $('#divseat div[title]' || 'GStbl div[title]')
                                .click(function () {
                                    var comUrl = UrlBase + "BS/WebService/CommonService.asmx/getCommissionList";
                                    if ($(this).attr("class") == "divAval") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "false,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------//
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            //                                        $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";
                                            $(this).removeClass();
                                            $(this).addClass("divSelect");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                        }
                                    }
                                    else if ($(this).attr("class") == "divHoriSleperAval") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "false,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------// 
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            // $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";
                                            $(this).removeClass();
                                            $(this).addClass("divHoriSleperSelect");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                        }
                                    }
                                    else if ($(this).attr("class") == "divVertiSleperAval") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "false,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------// 
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            // $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";
                                            $(this).removeClass();
                                            $(this).addClass("divVertiSleperSelect");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                        }
                                    }
                                    else if ($(this).attr("class") == "divLadies") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "true,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------// 
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            // $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";
                                            $(this).removeClass();
                                            $(this).addClass("divSelectLadies");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                        }
                                    }
                                    else if ($(this).attr("class") == "divHoriSleperLadies") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "true,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------// 
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            // $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";

                                            $(this).removeClass();
                                            $(this).addClass("divHoriSleperSelectLadies");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                            //                                    $(this).removeClass();
                                            //                                    $(this).addClass("divHoriSleperSelect");
                                        }
                                    }
                                    else if ($(this).attr("class") == "divVertiSleperLadies") {
                                        if (cnt < parseInt(h.PASSENGER.val())) {

                                            title = $(this).attr("title").split('|');
                                            rel = $(this).attr("rel").split('|');
                                            var seat = title[0].split(':');
                                            var fare = title[1].split(':');
                                            var fareArr = rel[1].split(':');
                                            ladiesseattxt += "true,";
                                            cnt += 1;
                                            //-----------------------calculate commision and tds-----------------------------------//
                                            $.ajax({
                                                url: comUrl,
                                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                                dataType: "json", type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                asnyc: true,
                                                success: function (data) {
                                                    var com = data.d;
                                                    if (com.length != 0) {
                                                        ta_totComm += parseInt(com[0].adcomm);
                                                        ta_totTds += parseInt(com[0].taTds);
                                                        ta_Totfare += parseInt(com[0].taTotFare);
                                                        ta_Netfare += parseInt(com[0].taNetFare);
                                                        admrkpAmt += parseInt(com[0].admrkp);
                                                        agmrkpAmt += parseInt(com[0].agmrkp);
                                                    }
                                                }
                                            });
                                            //--------------------------------------------------------------------------------------// 
                                            h.totPrice += parseInt($.trim(fare[1]));
                                            sseat += $.trim(seat[1]) + ",";
                                            $("#s").html("Seat:" + sseat); //cnt
                                            // $("#s").html("Seat:" + cnt);
                                            $("#f").html("Fare:" + h.totPrice);
                                            totSeat += $.trim(seat[1]) + ",";
                                            totseatFare += parseInt($.trim(fare[1])) + ",";
                                            original_Fare += parseInt($.trim(fareArr[1])) + ",";
                                            $(this).removeClass();
                                            $(this).addClass("divVertiSleperSelectladies");
                                        }
                                        else {
                                            alert('you have searched for "' + h.PASSENGER.val() + '" passenger .if you want to book more seat please search again.');
                                        }
                                        //                                 
                                        //                                    $(this).removeClass();
                                        //                                    $(this).addClass("divVertiSleperSelectladies");
                                    }
                                    else if ($(this).attr("class") == "divBlock" || $(this).attr("class") == "divHoriSleperBlock" || $(this).attr("class") == "divVertiSleperBlock" || $(this).attr("class") == "divBlockladies" || $(this).attr("class") == "divHoriSleperBlockladies" || $(this).attr("class") == "divVertiSleperBlockladies") {
                                        alert('This Seat is already Blocked');
                                        return false;
                                    }
                                    else if ($(this).attr("class") == "divSelect") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("false,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divAval");
                                    }
                                    else if ($(this).attr("class") == "divSelectLadies") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("true,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divLadies");
                                    }



                                    else if ($(this).attr("class") == "divHoriSleperSelect") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("false,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divHoriSleperAval");
                                    }

                                    else if ($(this).attr("class") == "divHoriSleperSelectLadies") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("true,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divHoriSleperLadies");
                                    }
                                    else if ($(this).attr("class") == "divVertiSleperSelect") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("false,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divVertiSleperAval");
                                    }

                                    else if ($(this).attr("class") == "divVertiSleperSelectladies") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        ladiesseattxt = ladiesseattxt.replace("true,", "");
                                        sseat = sseat.replace($.trim(seat[1]) + ",", "");
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divVertiSleperLadies");
                                    }

                                    else if ($(this).attr("class") == "divVertiSleperLadies") {
                                        title = $(this).attr("title").split('|');
                                        rel = $(this).attr("rel").split('|');
                                        var seat = title[0].split(':');
                                        var fare = title[1].split(':');
                                        var fareArr = rel[1].split(':');
                                        ladiesseattxt += "true,";
                                        cnt -= 1;
                                        //-----------------------calculate commision and tds-----------------------------------//
                                        $.ajax({
                                            url: comUrl,
                                            data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "'}",
                                            dataType: "json", type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            asnyc: false,
                                            success: function (data) {
                                                var com = data.d;
                                                if (com.length != 0) {
                                                    ta_totComm -= parseInt(com[0].adcomm);
                                                    ta_totTds -= parseInt(com[0].taTds);
                                                    ta_Totfare -= parseInt(com[0].taTotFare);
                                                    ta_Netfare -= parseInt(com[0].taNetFare);
                                                    admrkpAmt -= parseInt(com[0].admrkp);
                                                    agmrkpAmt -= parseInt(com[0].agmrkp);
                                                }
                                            }
                                        });
                                        //--------------------------------------------------------------------------------------//    
                                        h.totPrice -= parseInt($.trim(fare[1]));
                                        sseat += $.trim(seat[1]) + ",";
                                        $("#s").html("Seat:" + sseat); //cnt
                                        //$("#s").html("Seat:" + cnt);
                                        $("#f").html("Fare:" + h.totPrice);
                                        if (cnt == 0) {
                                            totSeat = "";
                                            totseatFare = "";
                                            original_Fare = "";
                                        }
                                        else {
                                            totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                            totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                            original_Fare = original_Fare.replace(parseInt($.trim(fareArr[1])) + ",", "");
                                        }
                                        $(this).removeClass();
                                        $(this).addClass("divVertiSleperSelectladies");
                                    }
                                });

                $('#btnContinue').click(function () {
                    if (totSeat == "" && totseatFare == "") {
                        alert('please select your seat');
                        return false;
                    }

                    else {
                        if (cnt < parseInt(h.PASSENGER.val())) {
                            alert('You have searched for ' + parseInt(h.PASSENGER.val()) + ' passenger ..please select !');
                            return false;
                        }
                        else {
                            h.boardpoint = $("#board>option:selected").text();
                            h.droppoint = $("#drop>option:selected").text();
                            h.boardpointId = $("#board>option:selected").val();
                            h.droppointId = $("#drop>option:selected").val();
                            h.ladiesSeat = ladiesseattxt.substring(0, ladiesseattxt.length - 1);
                            var dpointk = h.droppoint.trim().substring(h.droppoint.trim().lastIndexOf('(') + 1, h.droppoint.trim().lastIndexOf(')'));

                            var bpointk = h.boardpoint.trim().substring(h.boardpoint.trim().lastIndexOf('(') + 1, h.boardpoint.trim().lastIndexOf(')'));
                            if (dpointk == "" || dpointk == undefined)
                                h.droppoint = h.droppoint + "(" + GetNewDateTimeF(h.HIDDEPARTDATE.val(), bpointk, h.DUrTimeAB) + ")";
                            h.insertSelected_SeatDetails(h);
                        }
                    }
                });
            }

           
        }
    });

}

function GetNewDate(Cdate, ArrTime, DeptTime, providerName, DurT) {

    var CurDate = "";
    var arrT = "";
    var DeptT = "";
    var ProV = "";
    var DurTime = "";
    var myArray = new Array();
    CurDate = Cdate.trim().split('-');
    arrT = ArrTime;
    DeptT = DeptTime;
    ProV = providerName;
    DurTime = DurT;
    var durationTime = "";
    var adurationTime = "";
    var resultDiff = "";

    if (providerName == "GS") {
        if (arrT != "") {
            var Enddate = new Date(parseFloat(CurDate[0]), (parseFloat(CurDate[1]) - 1), (parseFloat(CurDate[2])), parseFloat(arrT.trim().split(':')[0]), parseFloat(arrT.trim().split(':')[1]));
            durationTime = formatAMPM(Enddate);
        }
        if (DeptT != "") {
            var startDate = new Date(parseFloat(CurDate[0]), (parseFloat(CurDate[1]) - 1), (parseFloat(CurDate[2])), parseFloat(DeptT.trim().split(':')[0]), parseFloat(DeptT.trim().split(':')[1]));
            adurationTime = formatAMPM(startDate);
        }
        if (DurTime != "") {
            //  var DurarionD = new Date(parseFloat(CurDate[0]), (parseFloat(CurDate[1]) - 1), (parseFloat(CurDate[2])), parseFloat(DurTime.trim().split(':')[0]), parseFloat(DurTime.trim().split(':')[1]));
            resultDiff = DurTime; //formatAMPM(DurarionD);
        }

    }
    else if (providerName == "AB") {
    }
    else if (providerName == "RB") {
    }


    myArray.push(durationTime, adurationTime, resultDiff + " " + "Hrs:mns", resultDiff);
    return myArray;
}


function getTimeDuration(arrival, departure, departureDate) {
    var adurationTime; var durationTime; var journeyDate;
    var myArray = new Array();
    if (arrival != "" && departure == "" && departureDate == "") {
        var hours = parseInt(parseInt(arrival) / 60);
        var reminder = parseInt(arrival) % 60;
        var djourneyDay = parseInt(parseInt(hours) / 24);
        var dhr = parseInt(hours) % 24;
        if (reminder == 0) {
            if (dhr >= 12) {
                dhr = parseInt(dhr) - 12;
                durationTime = dhr + ':' + reminder + "0" + " " + "PM";
            }
            else {
                durationTime = dhr + ':' + reminder + "0" + " " + "AM";
            }
        }
        else {
            if (dhr >= 12) {
                dhr = parseInt(dhr) - 12;
                durationTime = dhr + ':' + reminder + " " + "PM";
            }
            else {
                durationTime = dhr + ':' + reminder + " " + "AM";
            }
        }
        myArray.push(durationTime);
    }
    else {
        //-----------------------departure-------------------------//
        journeyDate = departureDate.split('-');
        var hours = parseInt(parseInt(departure) / 60);
        var reminder = parseInt(departure) % 60;
        var djourneyDay = parseInt(parseInt(hours) / 24);
        var dhr = parseInt(hours) % 24;
        if (reminder == 0) {
            if (dhr >= 12) {
                dhr = parseInt(dhr) - 12;
                durationTime = dhr + ':' + reminder + "0" + " " + "PM";
            }
            else {
                durationTime = dhr + ':' + reminder + "0" + " " + "AM";
            }
        }
        else {
            if (dhr >= 12) {
                dhr = parseInt(dhr) - 12;
                durationTime = dhr + ':' + reminder + " " + "PM";
            }
            else {
                durationTime = dhr + ':' + reminder + " " + "AM";
            }
        }
        //--------------------------------------------//
        var d = new Date(journeyDate[1] + "/" + journeyDate[2] + "/" + journeyDate[0] + " " + durationTime);
        var date = d.getDate();
        //------------------arrival------------------//
        var hours1 = parseInt(parseInt(arrival) / 60);
        var reminder1 = parseInt(arrival) % 60;
        var aArrivalDay = parseInt(parseInt(hours1) / 24);
        var ahr = parseInt(hours1) % 24;
        if (aArrivalDay == 0) {
            if (reminder1 == 0) {
                if (ahr >= 12) {
                    ahr = parseInt(ahr) - 12;
                    adurationTime = ahr + ':' + reminder1 + "0" + " " + "PM";
                }
                else {
                    adurationTime = ahr + ':' + reminder1 + "0" + " " + "AM";
                }
                var arrD = date;
            }
            else {
                if (ahr >= 12) {
                    ahr = parseInt(ahr) - 12;
                    adurationTime = ahr + ':' + reminder1 + " " + "PM";
                }
                else {
                    adurationTime = ahr + ':' + reminder1 + " " + "AM";
                }
                var arrD = date;
            }
        }
        else {
            if (reminder1 == 0) {
                if (ahr >= 12) {
                    ahr = parseInt(ahr) - 12;
                    adurationTime = ahr + ':' + reminder1 + "0" + " " + "PM";
                }
                else {
                    adurationTime = ahr + ':' + reminder1 + "0" + " " + "AM";
                }
                var arrD = d.getDate() + 1;
            }
            else {
                if (ahr >= 12) {
                    ahr = parseInt(ahr) - 12;
                    adurationTime = ahr + ':' + reminder1 + " " + "PM";
                }
                else {
                    adurationTime = ahr + ':' + reminder1 + " " + "AM";
                }
                var arrD = d.getDate() + 1;
            }
        }
        //------------------------------------------//
        var d1 = new Date(journeyDate[1] + "/" + arrD + "/" + journeyDate[0] + " " + adurationTime);
        //-------------------------------------------//
        var timeduration = d1 - d;
        var resultDiff = timeduration / 60 / 60 / 1000;
        var t = resultDiff.toString();
        var tt = t.split('.');
        if (tt[1] > 60) {
            var min = Math.round(parseInt(tt[1].substring(0, 2)) / 60);
            var rem = parseInt(tt[1].substring(0, 2)) % 60;
            resultDiff = parseInt(parseInt(tt[0]) + parseInt(min)) + "." + parseInt(rem);
        }
        myArray.push(durationTime, adurationTime, resultDiff + " " + "Hrs:mns", resultDiff);
    }
    return myArray;

}
ResHelper.prototype.insertSelected_SeatDetails = function(b) {
    var h = b; var orderID = "";
    var url = UrlBase + "BS/WebService/CommonService.asmx/getOrderID";
    $.ajax({
        url: url,
        data: "{}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: function(data) {
            orderID = data.d;
            var seatUrl = UrlBase + "BS/WebService/CommonService.asmx/insertSelected_seatDetails";
            $.ajax({
                url: seatUrl,
                data: "{'orderid':'" + orderID + "','tripid':'" + h.serviceID + "','src':'" + h.SOURCE.val() + "','dest':'" + h.DESTINATION.val() + "','jrdate':'" + h.HIDDEPARTDATE.val() + "','seatno':'" + totSeat + "','ladiesseat':'" + h.ladiesSeat + "','fare':'" + totseatFare + "','busoperator':'" + h.traveler + "','bustype':'" + h.bustype + "','bdpoint':'" + h.boardpoint.replace('&',' and ') + "&" + h.boardpointId + "','dppoint':'" + h.droppoint.replace('&',' and ') + "&" + h.droppointId + "','adcomm':'" + ta_totComm + "','tatds':'" + ta_totTds + "','totfare':'" + h.totPrice + "','tatotfare':'" + ta_Totfare + "','tanetfare':'" + ta_Netfare + "','idproof':'" + h.IdProof + "','adminMrkp':'" + admrkpAmt + "','agMrkp':'" + agmrkpAmt + "','originalPrice':'" + original_Fare + "','noofPax':'" + h.PASSENGER.val() + "','provider':'" + h.provider + "','canpolicy':'" + h.canPolicy + "','partialcancel':'" + h.partialcancel + "'}",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                success: function(data) {
                    window.location.href = UrlBase + "BS/CustomerInfo.aspx?ID=" + orderID + "";
                },
                error: function(XMLHttpRequest, textStatus, errorThrown)
                { return textStatus; }

            });
        },
        error: function(XMLHttpRequest, textStatus, errorThrown)
        { return textStatus; }

    });

}
/////////////////////////////////////////FILTER////////////////////////////////////////////
function create_BusFilter(BTYPE) {

    var table1 = "<span style='font-size:15px; font-weight:bold; text-decoration:underline;'>Filter Bus Results</span>  :<br/><table id='BusFilter' cellspacing='0' cellpadding='0' width='100%'>";
    var length = BTYPE.length; var c = parseInt(parseInt(length) / 2);

    //ADD THIS ONLY IF LENGTH > 6
    if (c >= 6) {
        for (var k = 0; k < c; k++) {
            table1 = table1 + "<tr><td><input type='checkbox' name='" + BTYPE[k] + "' id='B" + k + "' checked='checked' value='True'/> " + BTYPE[k] + "</td></tr>";
        }
        table1 = table1 + "</table><br/>";
        $('#BusMatrix1').html(table1);
        var table2 = "<table id='AirLineFilter2' cellspacing='0' cellpadding='0'>";
        for (var k = c; k < length; k++) { table2 = table2 + "<tr><td><input type='checkbox' name='" + BTYPE[k] + "' id='" + k + "' checked='checked' value='True'/> " + BTYPE[k] + "</td></tr>"; }
        table2 = table2 + "</table><br/>";
        $('#BusMatrix2').html(table2);
        $('#BusMatrix2').show();
    }
    else {
        for (var k = 0; k < length; k++) {
            table1 = table1 + "<tr><td><input type='checkbox' name='" + BTYPE[k] + "' id='B" + k + "' checked='checked' value='True'/>" + BTYPE[k] + "</td></tr>";
        }
        table1 = table1 + "</table><br/>";
        $('#BusMatrix1').html(table1);
        $('#BusMatrix2').hide();
    }


    $('#BusFilter').show();
}


function BusFilter(B) {
    for (var i = 0; i < B.length; i++) {
        var AIR = "#" + "B" + i;
        $(AIR).click(function () { Logic(B); });
    }
}

function Logic(B) {
    for (var i = 0; i < B.length; i++) {

        var AIR = "#" + "B" + i;
        var BSEL = $(AIR);

        // Custom Value
        var Title = "[custom=" + B[i] + "]"; //VOLVOA/C
        var TITLESEL = $(Title);
        if ((BSEL.is(":checked") == true)) {
            // ONLY That Title will Show which is Perfect Combination 
            TITLESEL.show();
        }
        else if ((BSEL.is(":checked") == false)) {
            TITLESEL.hide();
        }
    }
}


////Filtering Results Through Slider
$.fn.dataTableExt.afnFiltering.push(
    function(oSettings, aData, iDataIndex) {
        //Note Filtering Done On the Basis of 24 Hour Format
        var iMin = document.getElementById('amount1').value * 1; var iMax = document.getElementById('amount2').value * 1;
        var TimeMin = "0000"; var TimeMax = "2400"; var DepTime;

        //Code for 24 Hour Format:-
        if (OW_SL == 1 || RT_SL == 1) {
            TimeMin = ConvertTime(document.getElementById('Time1').value);
            TimeMax = ConvertTime(document.getElementById('Time2').value);
        }
        //        else if (RT_SL == 1) {
        //            TimeMin = ConvertTime(document.getElementById('Time3').value);
        //            TimeMax = ConvertTime(document.getElementById('Time4').value);
        //        }
        //Code for 1 Hour Format:-
        var Price = aData[7].split(','); //oSettings.aoData[7].nTr.id
        var SelectPrice = Price[0];
        DepTime = aData[3]; DepTime = ConvertTime(DepTime);
        // New Code - Added Condition for DepTime
        if (iMin == "" && iMax == "" && TimeMin == 0 && TimeMax == 2400) { //Considering 0 
            return true;
        }
        else if ((iMin <= SelectPrice && iMax == "" && TimeMin == 0 && TimeMax == 2400) || (iMin == "" && SelectPrice <= iMax && TimeMin == 0 && TimeMax == 2400)) { //Considering only 1 ->Greater than Min , Less than Max 
            return true;
        }
        else if ((iMin == "" && iMax == "" && TimeMin <= DepTime) || (iMin == "" && iMax == "" && DepTime <= TimeMax)) {  //Considering only 1 
            return true;
        }
        //Considering 2
        else if ((iMin <= SelectPrice && SelectPrice <= iMax && TimeMin == 0 && TimeMax == 2400) || (iMin <= SelectPrice && iMax == "" && DepTime >= TimeMin && TimeMax == 2400)) {
            return true;
        }
        else if ((iMin <= SelectPrice && iMax == "" && TimeMin == 0 && DepTime <= TimeMax) || (iMin == "" && SelectPrice <= iMax && DepTime >= TimeMin && TimeMax == 2400)) {
            return true;
        }

        //Considering 3
        else if ((iMin <= SelectPrice && SelectPrice <= iMax && DepTime >= TimeMin && TimeMax == 2400) || (iMin <= SelectPrice && iMax == "" && DepTime >= TimeMin && DepTime <= TimeMax)) {
            return true;
        }
        else if ((iMin <= SelectPrice && SelectPrice <= iMax && DepTime >= TimeMin && DepTime <= TimeMax) || (iMin == "" && SelectPrice <= iMax && DepTime >= TimeMin && DepTime <= TimeMax)) {
            return true;
        }
        else
            return false;
    }
);

/////////////////////////////////////////////////// FILTERING END//////////////////////////////////////
//////////////////////////////////////////////////////SLIDERS ////////////////////////////////////////////////////

//PRICE SLIDER STARTS
function SLIDER(MinPrice, MaxPrice, oTable, oTable1, B) {
    // 
    //Get Min-Max Range
    //Note:-Min Max Values are excluded for Searching
    var minrtrng = parseInt(MinPrice);  //- 1000;
    var maxrtrng = parseInt(MaxPrice);  //+ 1000;
    $("#slider-range").slider({
        range: true,
        animate: true,
        step: 25,
        min: parseInt(minrtrng),
        max: parseInt(maxrtrng),
        values: [parseInt(minrtrng), parseInt(maxrtrng)],
        slide: function(event, ui) {
            $("#amount1").val(ui.values[0]); // To Show Value of Range Selected by USER ON PAGE
            $("#amount2").val(ui.values[1]); // To Show Value of Range Selected by USER ON PAGE
        },
        change: function(event, ui) { },
        stop: function(event, ui) {//This event is triggered when the user stops sliding.
            // 
            //Draws the table According to the Filtered Range
            if (TotTrips == 0) { OW_SL = 1; oTable.fnDraw(); OW_SL = 0; }
            else { OW_SL = 1; oTable.fnDraw(); OW_SL = 0; RT_SL = 1; oTable1.fnDraw(); RT_SL = 0; }
            //And Condition wud be checked
            //            BusFilter();
            //            Logic(BusSel, BTypeSel);
            BusFilter(B); Logic(B);
        }
    });
    $("#amount1").val(minrtrng); //Giving Slider Intial Value on Load
    $("#amount2").val(maxrtrng); //Giving Slider Intial Value on Load

}
//DEPARTURE TIME SLIDER FOR OUTBOUND
function SLIDER_DEPTIME(oTable, oTable1, B) {
    // 
    //Get Min-Max Range
    //Note:-Min Max Values are excluded for Searching
    var minrtrng = "0000";
    var maxrtrng = "1439";
    $("#slider-Deptime").slider({
        range: true,
        animate: true,
        step: 10, //Time difference //5
        min: 0, //    min: minrtrng,
        max: 1439, // max: maxrtrng, //Starting From MidNight Upto 12.00 AM (720 Minutes), 11:59 PM(=1439) = 11*60 + 720 + 59
        values: [0, 1439], // values: [minrtrng, maxrtrng],[0, 1439]
        slide: function(event, ui) {
            //  //This Function is Just Showing Values on Page when User Slides
            var val0 = $("#slider-Deptime").slider("values", 0).toString();
            var val1 = $("#slider-Deptime").slider("values", 1).toString();
            var minutes0 = parseInt(val0 % 60, 10), hours0 = parseInt(val0 / 60 % 24, 10);
            var minutes1 = parseInt(val1 % 60, 10), hours1 = parseInt(val1 / 60 % 24, 10);
            var startTime = Show_Time_12(hours0, minutes0), endTime = Show_Time_12(hours1, minutes1);
            $("#Time1").val(startTime); // To Show Value of Range Selected by USER ON PAGE
            $("#Time2").val(endTime); // To Show Value of Range Selected by USER ON PAGE
        },
        change: function(event, ui) { },
        stop: function(event, ui) {//This event is triggered when the user stops sliding.
            // 
            //Draws the table According to the Filtered Range
            if (TotTrips == 0) { OW_SL = 1; oTable.fnDraw(); OW_SL = 0; }
            else { OW_SL = 1; oTable.fnDraw(); OW_SL = 0; RT_SL = 1; oTable1.fnDraw(); RT_SL = 0; }
            //And Condition wud be checked
            //            BusFilter();
            //            Logic(BusSel, BTypeSel);
            BusFilter(B); Logic(B);
        }
    });
    var minutes0 = parseInt(minrtrng % 60, 10), hours0 = parseInt(minrtrng / 60 % 24, 10);
    var minutes1 = parseInt(maxrtrng % 60, 10), hours1 = parseInt(maxrtrng / 60 % 24, 10);
    $("#Time1").val(Show_Time_12(hours0, minutes0)); //Giving Slider Intial Value on Load
    $("#Time2").val(Show_Time_12(hours1, minutes1)); //Giving Slider Intial Value on Load //val(MaxTime)

}

function ConvertTime(Time) {
    // Convert to 4 Length from HR:Min AM Format
    var A = Time.toString();
    A = Time.split(':');
    var Hr = A[0];
    A = A[1].split(' ');
    var min = A[0];
    if ((A[1] == "AM") & (Hr == 12)) { Hr = "00"; }
    if (A[1] == "PM") { Hr = parseInt(Hr, 10) + 12 }
    if (Hr.length == 1) { var str1 = "0"; Hr = str1.concat(Hr) }
    if (min.length == 1) { var str1 = "0"; min = str1.concat(min) }
    var Ctime = Hr + min;
    return Ctime;
}
function Show_Time_12(hours, minutes) {
    var time = null;
    minutes = minutes + "";
    if (hours < 12) { time = "AM"; }
    else { time = "PM"; }
    if (hours == 0) { hours = 12; }
    if (hours > 12) { hours = hours - 12; }
    if (minutes.length == 1) { minutes = "0" + minutes; }
    return hours + ":" + minutes + " " + time;
}

////////////////////////////////////////// SLIDERS END ////////////////////////////////////////////////
function removeDuplicates(inputArray) {
    var i;
    var len = inputArray.length;
    var outputArray = [];
    var temp = {};
    for (i = 1; i < len; i++) {
        temp[inputArray[i]] = 0;
    }
    for (i in temp) {
        outputArray.push(i);
    }
    return outputArray;
}



function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}


function GetNewDateTimeF(DDDD, StartTime, EndTime) {
    var nh = "";
    DDDDArr = DDDD.trim().split('-');
    if (StartTime.trim().split(' ')[1 == "PM"]) {
        nh = parseFloat(parseFloat(StartTime.trim().split(' ')[0].trim().split(':')[0]) + parseFloat(EndTime.trim().split(':')[0])) + 12;
    }
    else {
        nh = parseFloat(parseFloat(StartTime.trim().split(' ')[0].trim().split(':')[0]) + parseFloat(EndTime.trim().split(':')[0]));
    }
    var nmin = parseFloat(parseFloat(StartTime.trim().split(' ')[0].trim().split(':')[1]) + parseFloat(EndTime.trim().split(':')[1]));
    var Startate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), parseFloat(nh) , parseFloat(nmin));
    //var ndatetime =  parseFloat(nh) + parseFloat(nmin)
    var OrigDS = Startate.toLocaleTimeString();
    return DDDD = OrigDS; 

}

function funstrImghidefalse(id) {
    $("#strimg_" + id.trim().split('g')[1]).show();

}
function funmouseout(id) {
    $("#strimg_" + id.trim().split('g')[1]).hide();
}