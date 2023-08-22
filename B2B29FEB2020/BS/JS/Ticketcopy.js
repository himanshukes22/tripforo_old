
var Thandler;
$(document).ready(function () {
    Thandler = new TesHelper();
    Thandler.BindEvents();

});
var TesHelper = function () {
}
TesHelper.prototype.BindEvents = function () {
    var h = this;
    var query = h.getquerystring();
    h.tinid = query[0];
    h.orderid = query[1];
    h.getTicketcopyDetails();
}
TesHelper.prototype.getquerystring = function () {
    var pgUrl = window.location.search.substring(1);
    var qarray = pgUrl.split('&');
    var splt = qarray[0].split('=');
    var splt1 = qarray[1].split('=');
    var collect = new Array();
    collect.push(splt[1]);
    collect.push(splt1[1]);
    return collect;
}
TesHelper.prototype.getTicketcopyDetails = function () {
    var h = this; var htmlResponse = "";
    var url = UrlBase + "BS/WebService/CommonService.asmx/getTicketCopy";
    if ((h.tinid != "") && (h.tinid != undefined)) {
        $.ajax({
            url: url,
            data: "{'tin':'" + h.tinid + "','orderid':'" + h.orderid + "'}",
            dataType: "json", type: "POST",
            contentType: "application/json; charset=utf-8",
            asnyc: true,
            success: function (data) {

                var dd = data.d[0];
                var rrResult;
                var dd1 = data.d[1];
                if (data.d.length != 0 && data.d != "")
                    rrResult = data.d;
                if (rrResult != undefined) {
                    if (rrResult.length > 1)
                        htmlResponse += "<div style='float:left;margin:10px;width:20%;' onclick='showTktCopy(this.id)' id='OneWayTkts' class='tripbutton1'>Onway</div><div id='returntkts' onclick='showTktCopy(this.id)' style='float:left;margin:10px;width:20%;' class='tripbutton2'>Return</div>";
                    if (rrResult.length != 0 && rrResult != "") {
                        for (var z = 0; z < rrResult.length; z++) {

                            if (z == 0) {
                                htmlResponse += "<div id='OnewayTkt'>"
                                htmlResponse += "<div style='clear:both;'></div>"
                                if (rrResult[0].length != 0) {
                                    if (rrResult[z][0].provider_name == "GS" || rrResult[z][0].provider_name == "TY" || rrResult[z][0].provider_name == "ES")
                                        htmlResponse += Fun_CreateTicketCopy(rrResult[z]);
                                    else
                                        htmlResponse += CreateTicketCopy(rrResult[z]);
                                }
                                htmlResponse += "</div>";
                            }
                            else {
                                htmlResponse += "<div id='ReturnTkt' style='display:none;'>"
                                htmlResponse += "<div style='clear:both;'></div>"
                                if (rrResult[0].length != 0) {
                                    if (rrResult[z][0].provider_name == "GS" || rrResult[z][0].provider_name == "TY" || rrResult[z][0].provider_name == "ES")
                                        htmlResponse += Fun_CreateTicketCopy(rrResult[z]);
                                    else
                                        htmlResponse += CreateTicketCopy(rrResult[z]);
                                }
                                htmlResponse += "</div>";
                            }

                        }

                        $("#divticketcopy").html(htmlResponse);
                        var url = window.location.href;
                        if (url.indexOf('TicketCopy') > -1) {
                            h.SendingMail(escape(htmlResponse));
                        }
                    }
                }
                else
                    $("#divticketcopy").html("<div>please contact Administrator : Ticket No=  " + h.tinid + "  OrderId:  " + h.orderid + " </div>");

            }

        });

    }
    else {
        $("#divexport").hide();
        $("#divmail").hide();
        var mytable = "";
        mytable += "<table width='50%'  style='border: 1px solid #ccc; margin-top:10px; box-shadow: 1px 1px 5px #333; padding: 10px; border-radius: 10px; -o-border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px; width:100%; margin:auto; text-align:left;'>";
        mytable += "<tr>";
        mytable += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #000; height: 25px;'>Currently we are unable to book your ticket.</td>";
        mytable += "</tr>";
        mytable += "<tr>";
        mytable += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #000; height: 25px;'>Please contact our help desk before another try.</td>";
        mytable += "</tr>";
        mytable += "<tr>";
        mytable += "<td style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #000; height: 25px;'>In case of Booking Failure &nbsp;:&nbsp;<br />Your amount will be credited to your account within few minutes.</td>";
        mytable += "</tr>";
        mytable += "</table>";

        $("#divticketcopy").html(mytable);

    }
}
TesHelper.prototype.SendingMail = function (STRResponse) {
    var h = this; var htmlResponse = "";
    var url = UrlBase + "BS/TicketCopy.aspx/MailSend";
    if ((h.tinid != "") && (h.tinid != undefined)) {
        $.ajax({
            url: url,
            data: "{'Ticket':'" + STRResponse + "','OrderID':'" + h.orderid + "'}",
            dataType: "json", type: "POST",
            contentType: "application/json; charset=utf-8",
            asnyc: true,
            success: function (data) {
            }
        })
    }
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
                if (dhr == 0) {
                    durationTime = dhr + "0" + +':' + reminder + "0" + " " + "PM";
                }
                else {
                    durationTime = dhr + ':' + reminder + "0" + " " + "PM";
                }
            }
            else {
                durationTime = dhr + ':' + reminder + "0" + " " + "AM";
            }
        }
        else {
            if (dhr >= 12) {
                dhr = parseInt(dhr) - 12;
                if (dhr == 0) {
                    durationTime = dhr + "0" + +':' + reminder + "0" + " " + "PM";
                }
                else {
                    durationTime = dhr + ':' + reminder + " " + "PM";
                }
            }
            else {
                durationTime = dhr + ':' + reminder + " " + "AM";
            }
        }
        return durationTime;
    }
}
xmlToJson = function (xml) {
    var obj = {};
    if (xml.nodeType == 1) {
        if (xml.attributes.length > 0) {
            obj["@attributes"] = {};
            for (var j = 0; j < xml.attributes.length; j++) {
                var attribute = xml.attributes.item(j);
                obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
            }
        }
    } else if (xml.nodeType == 3) {
        obj = xml.nodeValue;
    }
    if (xml.hasChildNodes()) {
        for (var i = 0; i < xml.childNodes.length; i++) {
            var item = xml.childNodes.item(i);
            var nodeName = item.nodeName;
            if (typeof (obj[nodeName]) == "undefined") {
                obj[nodeName] = xmlToJson(item);
            } else {
                if (typeof (obj[nodeName].push) == "undefined") {
                    var old = obj[nodeName];
                    obj[nodeName] = [];
                    obj[nodeName].push(old);
                }
                obj[nodeName].push(xmlToJson(item));
            }
        }
    }
    return obj;
}
function StringToXML(oString) {
    //code for IE
    if (window.ActiveXObject) {
        var oXML = new ActiveXObject("Microsoft.XMLDOM"); oXML.loadXML(oString);
        return oXML;
    }
        // code for Chrome, Safari, Firefox, Opera, etc. 
    else {
        return (new DOMParser()).parseFromString(oString, "text/xml");
    }
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
function CreateTicketCopy(resultsss) {
    var result = resultsss[0];
    var NotAvlMsg = "Not available";
    if (result != undefined) {
        if (result.length != 0) {
            var f = 0; var cancelpolicy = " ";// "<div class='abc'>";
            if (result.provider_name == "RB") {
                var resp = $.parseJSON(result.bookres);
            }
            if (result.provider_name == "GS") {
                var xml = StringToXML(result.bookres);
                var respGS1 = xmlToJson(xml);
                // var respGS = JSON.stringify(respGS1);
                //resp1.advanceReserv.addnlAge["#text"]
            }
            var d = result.journeyDate;
            var ddd = d.split("-");
            var year = ddd[0];
            var mon = ddd[1];
            var date = ddd[2];
            var mytable = "";

            var mytable = "<table style='border:2px solid #203240; font-family: arial; font-size: 10px; color: #000; line-height:18px; text-align:left; width:100%;'>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%'>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%'>";
            mytable += "<tr>";
            if (result.provider_name == "RB") {
                mytable += "<td align='left' style='width:600px'><img src='../images/logo.png' width='204' height='107' /></td>";
            }
            // mytable += "<td align='right'>&nbsp;</td>";
            // <img src='http://itztravel..com/AgentLogo/" + result[0].agentID + ".jpg' style='height:45px; border:2px solid #ccc; border-radius:3px; -webkit-border-radius:3px;  -o-border-radius:3px;  -moz-border-radius:3px; ' />
            // mytable += "</tr>";
            // mytable += "<tr>";
            mytable += "<td align='right'  style='width:200px; font-family:arial; font-size:12px'><ul style='list-style-type:none; line-height:20px;'><li><h3 style='margin:0;'>" + result.AgencyName + "</h3></li><li>" + result.AgencyAddress + "," + result.City + "</li><li>" + result.State + "," + result.Country + "</li><li>" + result.Phone + "/" + result.Mobile + "</li></ul></td>";
            mytable += "</tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            mytable += "<td style='color:#000; font-family:arial; font-size:12px'><h4 style='margin:0;color:#000;'>" + result.src + " - " + result.dest + "</h4> <span>(" + date + " /" + mon + "/ " + year + ")</span></td>";
            mytable += "<td style='font-family:arial; font-size:12px'><b>Bus Operator:</b></td>";
            if (result.provider_name == "RB") {
                mytable += " <td style='font-family:arial; font-size:12px'>" + resp.travels + "</td>";
            }
            else if (result.provider_name == "GS" || result.provider_name == "TY" || result.provider_name == "ES" || result.provider_name == "AB") {
                mytable += " <td>" + result.traveler + "</td>";
            }
            mytable += "<td style='font-family:arial; font-size:12px'><b>Bus Operator Contact No. :</b></td>";
            if (result.provider_name == "RB") {
                mytable += "<td style='font-family:arial; font-size:12px'>" + resp.pickUpContactNo + "</td>";
            }
            else if (result.provider_name == "AB" || result.provider_name == "GS" || result.provider_name == "TY" || result.provider_name == "ES") {
                mytable += "<td style='font-family:arial; font-size:12px'>" + NotAvlMsg + "</td>";
            }
            mytable += " </tr>";
            mytable += "<tr>";
            mytable += " <td colspan='3' style='font-family:arial; font-size:12px'><b>PNR :</b> " + resp.pnr + "</td>";
            if (result.provider_name == "RB") {
                mytable += "<td style='font-family:arial; font-size:12px'>&nbsp;</td>";
            }
            else if (result.provider_name == "GS" || result.provider_name == "TY" || result.provider_name == "AB" || result.provider_name == "ES") {
                mytable += "<td style='font-family:arial; font-size:12px'>" + result.pnr + "</td>";
            }
            if (result.provider_name == "RB") {
                mytable += "<td style='font-family:arial; font-size:12px'>" + resp.tin + "</td>";
            }
            else if (result.provider_name == "GS" || result.provider_name == "TY" || result.provider_name == "AB" || result.provider_name == "ES") {
                mytable += "<td style='font-family:arial; font-size:12px'>" + result.tin + "</td>";
            }
            mytable += "</tr>";
            mytable += " </table>";
            mytable += " </td>";
            mytable += "</tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td style='font-family:arial; font-size:12px'>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr style='background: #203240; color: #fff; font-weight: bold;'>";
            mytable += "<td Height='20px' style='font-family:arial; font-size:12px'>Passenger Name</td>";
            mytable += "<td style='font-family:arial; font-size:12px; padding:5px;'>Seat Name</td>";
            mytable += "<td style='font-family:arial; font-size:12px; padding:5px;'>Contact Number</td>";
            mytable += "</tr>";
            if (result.provider_name == "RB") {
                if (resp.inventoryItems.length == undefined) {//.passenger
                    mytable += "<tr>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems.passenger.name + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems.seatName + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems.passenger.mobile + "</td>";
                    mytable += "</tr>";
                    f += parseInt(resultsss[0].fare);
                }
                else {
                    for (var i = 0; i <= resp.inventoryItems.length - 1; i++) {//.passenger
                        if (resp.inventoryItems[i].passenger.primary == "true") {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems[i].passenger.name + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems[i].seatName + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems[i].passenger.mobile + "</td>";
                            mytable += "</tr>";
                            f += parseInt(resultsss[i].fare);
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems[i].passenger.name + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + resp.inventoryItems[i].seatName + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                            f += parseInt(resultsss[i].fare);
                        }
                    }
                }
            }
            else if (result.provider_name == "AB") {
                if (result.length == 1) {
                    var pn = result.Passengername.split(',');
                    mytable += "<tr>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + pn[0] + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + result.seat + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + result.paxmob + "</td>";
                    mytable += "</tr>";
                    f += parseInt(result.fare);
                }
                else {
                    for (var i = 0; i <= result.length - 1; i++) {//.passenger
                        if (result[i].Isprimary == "primary") {
                            mytable += "<tr>";
                            var pn = result[i].Passengername.split(',');
                            mytable += "<td style='font-family:arial; font-size:12px'>" + pn[0] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].seat + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].paxmob + "</td>";
                            mytable += "</tr>";
                            f += parseInt(result[i].fare);
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].Passengername + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].seat + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                            f += parseInt(result[i].fare);
                        }
                    }
                }
            }
            else if (result.provider_name == "GS") {
                for (var Gs = 0; Gs < parseInt(result.length) ; Gs++) {
                    if (result.length == 1) {
                        mytable += "<tr>";
                        mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.addnlPasngrName["#text"].split(',')[0] + "</td>";
                        mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.seatNumber["#text"] + "</td>";
                        mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.phoneNumber["#text"] + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                        mytable += "</tr>";
                    }
                    else {
                        //  if (respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[1] == "primary") {
                        if (Gs == 0) {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[0] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.seatNumber[Gs]["#text"] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.phoneNumber["#text"] + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[0] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + respGS1.advanceReserv.seatNumber[Gs]["#text"] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                    }
                    f += parseInt(result[Gs].fare);
                }
            }
            else if (result.provider_name == "ES") {
                if (result.length == 1) {
                    var pn = result.Passengername.split(',');
                    mytable += "<tr>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + pn[0] + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + result.seat + "</td>";
                    mytable += "<td style='font-family:arial; font-size:12px'>" + result.paxmob + "</td>";
                    mytable += "</tr>";
                    f += parseInt(result.fare);
                }
                else {
                    for (var i = 0; i <= result.length - 1; i++) {//.passenger
                        if (result[i].Isprimary == "primary") {
                            mytable += "<tr>";
                            var pn = result[i].Passengername.split(',');
                            mytable += "<td style='font-family:arial; font-size:12px'>" + pn[0] + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].seat + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].paxmob + "</td>";
                            mytable += "</tr>";
                            f += parseInt(result[i].fare);
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].Passengername + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>" + result[i].seat + "</td>";
                            mytable += "<td style='font-family:arial; font-size:12px'>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                            f += parseInt(result[i].fare);
                        }
                    }
                }
            }
            mytable += " </table>";
            mytable += " </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr style='font-weight: bold; background: #203240;color: #fff'>";
            mytable += "<td style='font-family:arial; font-size:12px,width:150px; height:20px'>Bus Type</td>";
            mytable += "<td  style='font-family:arial; font-size:12px; width:150px;'>Reporting Time:</td>";
            mytable += "<td colspan='3' style='font-family:arial; font-size:12px,'>Boarding Point Address: </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            if (result.provider_name == "RB") {
                mytable += "<td valign='top' style='font-family:arial; font-size:12px'> " + resp.busType + "</td>";
                mytable += "<td valign='top'  style='font-family:arial; font-size:12px'>" + getTimeDuration(resp.pickupTime, "", "") + "</td>";
                mytable += "<td valign='top' style='padding-right:5px; width:200px;font-family:arial; font-size:12px'><b>Location : </b> " + resp.pickupLocation + "</td>";
                mytable += "<td valign='top' style='padding-right:5px; width:200px;font-family:arial; font-size:12px'><b>Landmark : </b>" + resp.pickupLocationLandmark + "</td>";
                mytable += " <td valign='top' style='font-family:arial; font-size:12px'><b>Address :</b> " + resp.pickUpLocationAddress + "</td>";
            }
            else if (result.provider_name == "AB") {
                var location = result.boardpoint.split('&');
                var rt = location[0].split('(');
                var lnd = location[1].replace(':', '@').split('@');
                mytable += "<td valign='top'> " + result.busType + "</td>";
                if (rt[1].indexOf(",") > 0) {
                    var tt = rt[1].split(',');
                    var lndd = tt[1].replace(':', '@').split('@');
                    mytable += "<td valign='top' style='font-family:arial; font-size:12px'>" + tt[0] + "</td>";
                    mytable += "<td valign='top' style='padding-right:5px; width:200px; font-family:arial; font-size:12px'><b>Location : </b> " + rt[0] + "</td>";
                    mytable += "<td valign='top' style='padding-right:5px; width:200px; font-family:arial; font-size:12px'><b>Landmark : </b>" + lndd[1].substring(0, lndd[1].length - 1) + "</td>";
                }
                else {
                    mytable += "<td valign='top' style='font-family:arial; font-size:12px'>" + rt[1].substring(0, rt[1].length - 1) + "</td>";
                    mytable += "<td valign='top' style='padding-right:5px; width:200px; font-family:arial; font-size:12px'><b>Location : </b> " + rt[0] + "</td>";
                    mytable += "<td valign='top' style='padding-right:5px; width:200px;font-family:arial; font-size:12px'><b>Landmark : </b>" + lnd[1] + "</td>";
                }
                mytable += " <td valign='top' style='font-family:arial; font-size:12px'><b>Address :</b></td>";
            }
            else if (result.provider_name == "GS") {
                mytable += "<td valign='top'  style='font-family:arial; font-size:12px'> " + result.busType + "</td>";
                mytable += "<td valign='top'  style='font-family:arial; font-size:12px'>" + Show_Time_12(respGS1.advanceReserv.pickupPointTime["#text"].split(':')[0], respGS1.advanceReserv.pickupPointTime["#text"].split(':')[1]) + "</td>";
                mytable += "<td valign='top' style='padding-right:5px; width:200px; font-family:arial; font-size:12px'><b>Location : </b> " + result.boardpoint.substring(0, result.boardpoint.indexOf('(')) + "</td>";
                mytable += "<td valign='top' style='padding-right:5px; width:200px; font-family:arial; font-size:12px'><b>Landmark : </b>" + result.boardpoint.substring(0, result.boardpoint.indexOf('(')) + "</td>";
                mytable += " <td valign='top'style='font-family:arial; font-size:12px'><b>Address :</b> " + result.boardpoint.substring(0, result.boardpoint.indexOf('(')) + "</td>";
            }
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td colspan='4'  style='font-family:arial; font-size:12px'><b>Total Fare :</b>&nbsp; " + f + "/-</td>";
            mytable += "<td  style='font-family:arial; font-size:12px'>&nbsp;</td>";
            mytable += "</tr>";
            mytable += "</table>";

            mytable += "  </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td  style='font-family:arial; font-size:12px'>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            mytable += "<td  style='font-family:arial; font-size:12px'><b>Terms & Conditions :</b></td>";
            mytable += "</tr>";
            mytable += "<tr>";
            if (result.provider_name == "RB") {
                mytable += "<td style='text-wrap:suppress; font-family:arial; font-size:12px'>RWT is only a bus ticket booking agent and it doestn't operate bus services of it's own.<br/> In order to provide a comprehensive choice of bus operators departure times and prices to customers, it has tied up with many bus operators.<br /> RWT advice to customers is to choose bus operator they are aware of and whose service they are comfortable with.</td>";
            }
            else if (result.provider_name == "AB") {
                mytable += "<td style='text-wrap:suppress;font-family:arial; font-size:12px'><p>1. The arrival and departure times mentioned on the ticket copy are only tentative timings.<br /> Buses may be delayed due to some unavoidable reason like(traffic jams etc)<br />How ever the bus will not leave the source before the time is mentioned on the ticket.<br /> 2. Passengers are requested to arrive at the boarding point at least 15 min. beforethe schedule timeof departure.<br /> 3. Passengers are required to furnish the follwing at the time of boarding the bus<br />failing to do so they may not be allowed to board the bus <ul><li>A copy of the ticket.</li><li>Identity proof(Driving lincese,Pan card,Passport,Voter Id).</li></ul></p><p> 4. The company is not resposible for any loss of goods or property of the passengers and accident.<br /> 5. The company shall not be responsible for any delay or inconvenience during the journey due to break down of the vehicle or other reason beyond the control of company<br /> 6. The trips are subjected to cancellation or postponement due to breakdown of vehicle or insufficient passenger for th trip.<br /> 7. Video and air conditioning in the bus is not guaranteed.</p></td>";
            }
            mytable += "</tr>";

            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td  style='font-family:arial; font-size:12px'>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            if (result.provider_name == "RB") {
                mytable += "<td valign='top' style='padding-right:5px; Width:600px;text-wrap:suppress;font-family:arial; font-size:12px;' ><b>RWT responsible  for :</b><p>1. Issuing a valid eticket(a ticket that wiil be accepted by the bus operator) for<br /> it's network of bus operator.<br /> 2. Providing refund &amp; support in the event of cancellation.<br /> 3. Providing customer support and information in case of any delays/inconvenience.</p><p ><b>RWT not responsible for :</b></p><p> 1. The bus operator's bus not departing/reaching on time.<br /> 2. The bus operator's employees being rude<br /> 3. The bus operator's bus seats etc not being up to the customer's expectation.<br /> 4. The bus operator canceling the trip due to unavoidable reasons.</p></td>";
            }
            mytable += " <td style='float:right; padding-right:10px; vertical-align:top; width:300px; font-family:arial; font-size:12px'>";
            mytable += "<table  style='width:100%; padding-right:10px'>";
            mytable += "<tr>";
            mytable += "<td valign='top' style='padding-right:10px; font-family:arial; font-size:12px'><b>Cancellation Policy :</b> </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td  style='font-family:arial; font-size:12px'>";
            if (result.provider_name == "RB") {
                var policy = resp.cancellationPolicy.split(';');

                for (var k = 0; k <= policy.length - 1; k++) {
                    if (policy[k] != "") {
                        var canstr = policy[k].split(':');
                        if (canstr[1] != "-1") {
                            cancelpolicy += "Between" + " " + canstr[0] + " " + "Hrs" + " " + "to" + " " + canstr[1] + " " + "Hrs" + "&nbsp;&nbsp;" + canstr[2] + "%<br />";
                        }
                        else {
                            cancelpolicy += "Before" + " " + canstr[0] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + canstr[2] + "%<br />";
                        }
                    }
                }
            }
            else {
                if (result.canPolicy != "null")
                    cancelpolicy = result.canPolicy;
                else
                    cancelpolicy = " 0-1 Day Cancellation Charges 25 % of Basic Fare%<br >2-5 Day Cancellation Charges 20 % of Basic Fare%<br />6-10 Day Cancellation Charges 15 % of Basic Fare%<br />11-20 Day Cancellation Charges 10 % of Basic Fare%<br />21-60 Day Cancellation Charges 5 % of Basic Fare%<br />"

            }
            mytable += "</td>";
            mytable += " </tr>";
            mytable += " <tr>";
            mytable += "<td align='left' valign='top' style='font-family:arial; font-size:12px;'>";
            mytable += cancelpolicy;//+ "<br /></div>";
            mytable += " </td>";
            mytable += " </tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += " </table>";
            mytable += " </td>";
            mytable += " </tr>";
            mytable += " </table>";

            mytable += "</table>";

        }
    }
    else {
        mytable += "please contact Administrator";
    }
    return mytable;
}
function Fun_CreateTicketCopy(S_Result) {
    var strundefined = "Not available";
    var SS_Result = S_Result;
    var result = S_Result;
    if (result != undefined) {
        if (result.length != 0) {
            var f = 0; var cancelpolicy = "";// "<div class='abc'>";
            if (result[0].provider_name != "ES")
                var xml = StringToXML(result[0].bookres);

            var respGS1;
            if (result[0].provider_name == "ES")
                respGS1 = $.parseJSON(result[0].bookres);
            else
                respGS1 = xmlToJson(xml);

            // var respGS1 = xmlToJson(xml);
            var d = result[0].journeyDate;
            var ddd = d.split("-");
            var year = ddd[0];
            var mon = ddd[1];
            var date = ddd[2];
            var mytable = "";

            var mytable = "<table width='100%' border='0' cellpadding='0' cellspacing='0'  style='border:2px solid #203240; padding:10px; font-family: Verdana; font-size: 13px; color: #000; line-height:25px; text-align:left;'>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            mytable += "<tr>";
            mytable += "<td colspan='3'>";
            mytable += "<table width='100%'>";
            mytable += "<tr>";
            mytable += "<td align='right'><img src='http://Springtravels.com/AgentLogo/" + result.agentID + ".jpg' style='height:45px; border:2px solid #ccc; border-radius:3px; -webkit-border-radius:3px;  -o-border-radius:3px;  -moz-border-radius:3px; ' /></td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td align='right' colspan='2'><ul style='list-style-type:none; line-height:20px;'><li><h3 style='margin:0;'>" + result[0].AgencyName + "</h3></li><li>" + result[0].AgencyAddress + "," + result[0].City + "</li><li>" + result[0].State + "," + result[0].Country + "</li><li>" + result[0].Phone + "/" + result[0].Mobile + "</li></ul></td>";
            mytable += "</tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            mytable += "<td rowspan='2' style='color:#000'><h3 style='margin:0;color:#000;'>" + result[0].src + " - " + result[0].dest + "</h3> </div><div>(" + date + " /" + mon + "/ " + year + ")</div></td>";
            mytable += "<td><b>Bus Operator:</b></td>";
            mytable += " <td>" + result[0].traveler + "</td>";
            mytable += "<td><b>Bus Operator Contact No. :</b></td>";

            if (result[0].provider_name == "TY") {
                if (respGS1.clsBookingDetail.Pickup.Phone["#text"] != undefined)
                    mytable += "<td>" + respGS1.clsBookingDetail.Pickup.Phone["#text"] + "</td>";
                else
                    mytable += "<td>" + strundefined + "</td>";
            }
            else if (result[0].provider_name == "GS") {
                mytable += "<td>" + strundefined + "</td>";
            }
            else if (result[0].provider_name == "ES") {
                if (respGS1.serviceProviderContact != undefined)
                    mytable += "<td>" + respGS1.serviceProviderContact + "</td>";
                else
                    mytable += "<td>" + strundefined + "</td>";
            }




            //            if (result[0].provider_name == "TY") {
            //                if (respGS1.clsBookingDetail.Pickup.Phone["#text"] != undefined)
            //                    mytable += "<td>" + respGS1.clsBookingDetail.Pickup.Phone["#text"] + "</td>";
            //                else
            //                    mytable += "<td>" + strundefined + "</td>";
            //            }
            //            else
            //                mytable += "<td>" + strundefined + "</td>";
            mytable += " </tr>";
            mytable += "<tr>";
            mytable += " <td colspan='3'><b>PNR :</b> " + result[0].pnr + "</td>";
            mytable += "<td>&nbsp;</td>";
            mytable += " <td><b>Ticket No :</b></td>";
            mytable += "<td>" + result[0].tin + "</td>";
            mytable += "</tr>";
            mytable += " </table>";
            mytable += " </td>";
            mytable += "</tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr style='background: #203240; color: #fff; font-weight: bold;'>";
            mytable += "<td Height='20px'>Passenger Name</td>";
            mytable += "<td style='padding:5px'>Seat Name</td>";
            mytable += "<td style='padding:5px'>Contact Number</td>";
            mytable += "</tr>";
            for (var Gs = 0; Gs < parseInt(result.length) ; Gs++) {
                if (result.length == 1) {
                    if (result[0].provider_name == "TY" || result[0].provider_name == "ES") {

                        mytable += "<tr>";
                        mytable += "<td>" + result[Gs].Passengername.split(',')[0] + "</td>";
                        mytable += "<td>" + result[Gs].seat + "</td>";
                        mytable += "<td>" + result[Gs].paxmob + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                        mytable += "</tr>";
                    }
                    else {
                        mytable += "<tr>";
                        mytable += "<td>" + respGS1.advanceReserv.addnlPasngrName["#text"].split(',')[0] + "</td>";
                        mytable += "<td>" + respGS1.advanceReserv.seatNumber["#text"] + "</td>";
                        mytable += "<td>" + respGS1.advanceReserv.phoneNumber["#text"] + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                        mytable += "</tr>";
                    }
                }
                else {
                    //  if (respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[1] == "primary") {
                    if (result[0].provider_name == "TY" || result[0].provider_name == "ES") {
                        if (Gs == 0) {
                            mytable += "<tr>";
                            mytable += "<td>" + result[Gs].Passengername.split(',')[0] + "</td>";
                            mytable += "<td>" + result[Gs].seat + "</td>";
                            mytable += "<td>" + result[Gs].paxmob + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td>" + result[Gs].Passengername.split(',')[0] + "</td>";
                            mytable += "<td>" + result[Gs].seat + "</td>";
                            mytable += "<td>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                    }
                    else {
                        if (Gs == 0) {
                            mytable += "<tr>";
                            mytable += "<td>" + respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[0] + "</td>";
                            mytable += "<td>" + respGS1.advanceReserv.seatNumber[Gs]["#text"] + "</td>";
                            mytable += "<td>" + respGS1.advanceReserv.phoneNumber["#text"] + "</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                        else {
                            mytable += "<tr>";
                            mytable += "<td>" + respGS1.advanceReserv.addnlPasngrName[Gs]["#text"].split(',')[0] + "</td>";
                            mytable += "<td>" + respGS1.advanceReserv.seatNumber[Gs]["#text"] + "</td>";
                            mytable += "<td>&nbsp;</td>"; // + resp.inventoryItems[i].passenger.mobile + 
                            mytable += "</tr>";
                        }
                    }
                }
                f += parseInt(result[Gs].fare);
            }

            mytable += " </table>";
            mytable += " </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr style='font-weight: bold; background: #203240;color: #fff'>";
            mytable += "<td Width='150px' Height='20px'>Bus Type</td>";
            mytable += "<td Width='150px'>Reporting Time:</td>";
            mytable += "<td colspan='3'>Boarding Point Address: </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td valign='top'> " + result[0].busType + "</td>";
            if (result[0].provider_name == "TY") {
                mytable += "<td valign='top'>" + result[0].boardpoint.substring(result[0].boardpoint.lastIndexOf('(') + 1, result[0].boardpoint.lastIndexOf(')')) + "</td>";
                if (respGS1.clsBookingDetail.Pickup.PickupName["#text"] != undefined)
                    mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Location : </b> " + respGS1.clsBookingDetail.Pickup.PickupName["#text"] + "</td>";
                else
                    mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Location : </b> " + strundefined + "</td>";
                if (respGS1.clsBookingDetail.Pickup.Landmark["#text"] != undefined)
                    mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Landmark : </b>" + respGS1.clsBookingDetail.Pickup.Landmark["#text"] + "</td>";
                else
                    mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Landmark : </b>" + strundefined + "</td>";
                if (respGS1.clsBookingDetail.Pickup.Address["#text"] != undefined)
                    mytable += " <td valign='top' ><b>Address :</b> " + respGS1.clsBookingDetail.Pickup.Address["#text"] + "</td>";
                else
                    mytable += " <td valign='top' ><b>Address :</b> " + strundefined + "</td>";
            } else if (result[0].provider_name == "GS") {
                mytable += "<td valign='top'>" + Show_Time_12(respGS1.advanceReserv.pickupPointTime["#text"].split(':')[0], respGS1.advanceReserv.pickupPointTime["#text"].split(':')[1]) + "</td>";
                mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Location : </b> " + result[0].boardpoint.substring(0, result[0].boardpoint.indexOf('(')) + "</td>";
                mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Landmark : </b>" + result[0].boardpoint.substring(0, result[0].boardpoint.indexOf('(')) + "</td>";
                mytable += " <td valign='top' ><b>Address :</b> " + result[0].boardpoint.substring(0, result[0].boardpoint.indexOf('(')) + "</td>";
            }
            else if (result[0].provider_name == "ES") {
                mytable += "<td valign='top'>" + result[0].boardpoint.substring(result[0].boardpoint.lastIndexOf('(') + 1, result[0].boardpoint.lastIndexOf(')')) + "</td>";
                mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Location : </b>-</td>";
                mytable += "<td valign='top' Width='200px' style='padding-right:5px'><b>Landmark : </b>-</td>";
                mytable += " <td valign='top' ><b>Address :</b> " + result[0].boardpoint.substring(0, result[0].boardpoint.lastIndexOf('(')) + "</td>";
            }
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td colspan='4'><b>Total Fare :</b>&nbsp; " + f + "/-</td>";
            mytable += "<td></td>";
            mytable += "</tr>";
            mytable += "</table>";

            mytable += "  </td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            mytable += "<td ><b>Terms & Conditions :</b></td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td><p>1. The arrival and departure times mentioned on the ticket copy are only tentative timings.<br /> Buses may be delayed due to some unavoidable reason like(traffic jams etc)<br />How ever the bus will not leave the source before the time is mentioned on the ticket.<br /> 2. Passengers are requested to arrive at the boarding point at least 15 min. beforethe schedule timeof departure.<br /> 3. Passengers are required to furnish the follwing at the time of boarding the bus<br />failing to do so they may not be allowed to board the bus <ul><li>A copy of the ticket.</li><li>Identity proof(Driving lincese,Pan card,Passport,Voter Id).</li></ul></p><p> 4. The company is not resposible for any loss of goods or property of the passengers and accident.<br /> 5. The company shall not be responsible for any delay or inconvenience during the journey due to break down of the vehicle or other reason beyond the control of company<br /> 6. The trips are subjected to cancellation or postponement due to breakdown of vehicle or insufficient passenger for th trip.<br /> 7. Video and air conditioning in the bus is not guaranteed.</p></td>";
            mytable += "</tr>";

            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += "<tr>";
            mytable += "<td>";
            mytable += "<table style='width:100%;'>";
            mytable += "<tr>";
            mytable += "<td><b>responsible for :</b><p>1. Issuing a valid eticket(a ticket that wiil be accepted by the bus operator) for<br /> it's network of bus operator.<br /> 2. Providing refund &amp; support in the event of cancellation.<br /> 3. Providing customer support and information in case of any delays/inconvenience.</p><p ><b>Not responsible for :</b></p><p> 1. The bus operator's bus not departing/reaching on time.<br /> 2. The bus operator's employees being rude<br /> 3. The bus operator's bus seats etc not being up to the customer's expectation.<br /> 4. The bus operator canceling the trip due to unavoidable reasons.</p></td>";
            mytable += " <td align='left' valign='top'>";
            mytable += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            mytable += "<tr>";
            mytable += "<td ><b>Cancellation Policy :</b> </td>";
            mytable += " </tr>";
            //if (result[0].canPolicy != "null") {
            //  if (result[0].provider_name == "TY")
            //    cancelpolicy = createPolicy(result[0].canPolicy, respGS1.clsBookingDetail.JourneyDate["#text"], f)
            // else if (result[0].provider_name == "ES")
            //   cancelpolicy = EScreatePolicy(respGS1.cancellationPolicy, result[0].journeyDate + " " + result[0].boardpoint.substring(result[0].boardpoint.lastIndexOf('(') + 1, result[0].boardpoint.lastIndexOf(')')), f)
            //else if (result[0].provider_name == "RB")
            // cancelpolicy = result[0].canPolicy;
            //}
            if (result[0].canPolicy != "null") {
                if (result[0].provider_name == "TY")
                    cancelpolicy = createPolicy(result[0].canPolicy, respGS1.clsBookingDetail.JourneyDate["#text"], f)
                else if (result[0].provider_name == "ES") {
                    if (respGS1.cancellationPolicy == null || respGS1.cancellationPolicy == "" || respGS1.cancellationPolicy == "null")
                        cancelpolicy = "Cancellation not allowed for this ticket";
                    else
                        cancelpolicy = EScreatePolicy(respGS1.cancellationPolicy, result[0].journeyDate + " " + result[0].boardpoint.substring(result[0].boardpoint.lastIndexOf('(') + 1, result[0].boardpoint.lastIndexOf(')')), f)
                }
                else if (result[0].provider_name == "RB")
                    cancelpolicy = result[0].canPolicy;
            }


            else
                cancelpolicy = " 0-1 Day Cancellation Charges 25 % of Basic Fare%<br ></div>2-5 Day Cancellation Charges 20 % of Basic Fare%<br />6-10 Day Cancellation Charges 15 % of Basic Fare%<br />11-20 Day Cancellation Charges 10 % of Basic Fare%<br />21-60 Day Cancellation Charges 5 % of Basic Fare%<br />"
            mytable += " <tr>";
            mytable += "<td align='left' valign='top'>";
            // mytable += " <div class='abc'>" + cancelpolicy + "<br /></div>";
            mytable += cancelpolicy;
            mytable += " </td>";
            mytable += " <td align='left' valign='top'>";
            mytable += " </td>";
            mytable += " </tr>";
            mytable += "</table>";
            mytable += "</td>";
            mytable += "</tr>";
            mytable += " </table>";

            mytable += " </td>";
            mytable += " </tr>";
            mytable += " </table>";

            mytable += "</table>";
        }
    }
    else {
        mytable += "please contact Administrator";
    }
    return mytable;

}
function showTktCopy(id) {

    if (id == "OneWayTkts") {
        $("#returntkts").removeClass("tripbutton1");
        $("#returntkts").addClass("tripbutton2");
        $("#OneWayTkts").removeClass("tripbutton2");
        $("#OneWayTkts").addClass("tripbutton1");
        $("#OnewayTkt").show(); $("#ReturnTkt").hide();
    }
    else {
        $("#OneWayTkts").removeClass("tripbutton1");
        $("#OneWayTkts").addClass("tripbutton2");
        $("#returntkts").removeClass("tripbutton2");
        $("#returntkts").addClass("tripbutton1");
        $("#OnewayTkt").hide(); $("#ReturnTkt").show();
    }
}
function createPolicy(polic, DddTTT, rs) {
    var policisss = "[" + polic + "]"; var stm = 0; var stmSum = 0; var startTime = 0; var endTime = 0;
    policy = $.parseJSON(policisss);
    var Ndate = ""; var layoutOfCancelationPolicy = "";
    for (var ts = 0; ts < policy.length; ts++) {
        stm = parseInt(stmSum);
        startTime = parseInt(stm);
        endTime = parseInt(policy[ts].MinsBeforeDeparture) / 60;
        Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
        if (startTime == endTime) {
            if (policy[ts].ChargeFixed == "0")
                layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, parseInt(100 - parseInt(policy[ts].ChargePercentage))) + "</div>";
            else
                layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.split('@')[1] + "</div><div class='ClCharge'>RS " + policy[ts].ChargeFixed + "</div>";
        }
        else {
            if (policy[ts].ChargeFixed == "0")
                layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.split('@')[0] + " to " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, parseInt(100 - parseInt(policy[ts].ChargePercentage))) + "</div>";
            else
                layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.split('@')[0] + " to " + Ndate.split('@')[1] + "</div><div class='ClCharge'>RS " + policy[ts].ChargeFixed + "</div>";
        }
        layoutOfCancelationPolicy += "<div class='clear'></div>";

        stmSum = parseInt(policy[ts].MinsBeforeDeparture) / 60;
        startTime = 0;
        endTime = 0;

        //stm =parseInt(policy[ts].MinsBeforeDeparture);
        //var startTime = parseInt(stm / 60);
        //var endTime = parseInt(policy[ts].MinsBeforeDeparture) / 60;
        //if (ts == 0) {
        //    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
        //    if (policy[ts].ChargeFixed == "0")
        //        layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, policy[ts].ChargePercentage) + "</div>";
        //    else
        //        layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + policy[ts].ChargeFixed + "</div>";
        //    layoutOfCancelationPolicy += "<div class='clear'></div>";
        //}
        //else {
        //    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
        //    if (policy[ts].ChargeFixed == "0")
        //        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.split('@')[1] + " to " + Ndate.split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, policy[ts].ChargePercentage) + "</div>";
        //    else
        //        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.split('@')[1] + " to " + Ndate.split('@')[0] + "</div><div class='ClCharge'>" + policy[ts].ChargeFixed + "</div>";
        //    layoutOfCancelationPolicy += "<div class='clear'></div>";
        //}
    }
    return layoutOfCancelationPolicy;

}
function EScreatePolicy(polic, DddTTT, rs) {
    var stm = 0; var stmSum = 0; var startTime = 0; var endTime = 0;
    policy = $.parseJSON(polic);
    var Ndate = ""; var layoutOfCancelationPolicy = "";
    for (var ts1 = 0; ts1 < policy.length; ts1++) {
        if (policy[ts1].cutoffTime.indexOf('-') == -1) {
            endTime = policy[ts1].cutoffTime.split('-')[0];
            percentRs = policy[ts1].refundInPercentage;
            Ndate = ESGetNewDateTimeFormate(DddTTT, startTime, endTime);
            layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
            layoutOfCancelationPolicy += "<div class='clear'></div>";
        }
        else {
            startTime = policy[ts1].cutoffTime.split('-')[0];
            endTime = policy[ts1].cutoffTime.split('-')[1];
            percentRs = policy[ts1].refundInPercentage;
            Ndate = ESGetNewDateTimeFormate(DddTTT, startTime, endTime);
            layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.split('@')[0] + " to " + Ndate.split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
            layoutOfCancelationPolicy += "<div class='clear'></div>";
        }
    }
    return layoutOfCancelationPolicy;

}


function ESGetNewDateTimeFormate(DDDD, StartTime, EndTime) {
    var now = new Date();
    DDDDArr = DDDD.split('-');
    var Dttt = parseFloat(DDDD.split(' ')[1].split(':')[0]);
    var DtMM = parseFloat(DDDD.split(' ')[1].split(':')[1]);
    if (DDDD.split(' ')[2] == "PM")
        Dttt = parseFloat(Dttt) + 12;
    else
        Dttt = Dttt;
    var Startate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(StartTime)), parseFloat(DtMM));
    var Enddate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(EndTime)), parseFloat(DtMM));
    var OrigDS = Startate.toLocaleString();
    var OrigDE = Enddate.toLocaleString();
    return DDDD = OrigDS + "@" + OrigDE;
}

function GetNewDateTimeFormate(DDDD, StartTime, EndTime) {
    var ndate = DDDD.split('T');
    DDDDArr = ndate[0].split('-');
    var Dttt = parseFloat(DDDD.split('T')[1].split(':')[0]);
    var DtMM = parseFloat(DDDD.split('T')[1].split(':')[1]);
    var Startate = new Date(); var Enddate = new Date();
    Startate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(StartTime)), parseFloat(DtMM));
    Enddate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(EndTime)), parseFloat(DtMM));
    var OrigDS = Startate.toLocaleString();
    var OrigDE = Enddate.toLocaleString();
    return DDDD = OrigDS + "@" + OrigDE;
}
function getPercentage(RS, PerCent) {
    var rsF = RS;
    var toFar = "";

    toFar = toFar + "RS " + Math.round(parseFloat(parseFloat(rsF) * parseFloat(PerCent) / 100), 0) + " /";

    toFar = toFar.substring(0, toFar.length - 1);
    return RS = toFar;
}