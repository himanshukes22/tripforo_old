
var A2BLayout = "";
var totresponse;
var totBookingresponse;
var ticketresponse;
var OrderID;
var tttravelaer1 = new Array();
var carLayout1;
var SHandler;
var mainArrCity;
var ttService1 = new Array();
var ttService = new Array();
var Trip;
var dayName = new Array("Sunday", "Monday", "TuesDay", "WednesDay", "ThursDay", "FriDay", "SaturDay");
var month = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var ImgUP = "http://" + window.location.host + UrlBase + "CSS/images/A2Bimg/UP.png";
var ImgDU = "http://" + window.location.host + UrlBase + "CSS/images/A2Bimg/DU.png";
var sorced = "";
//var Omonth = month[jrneyDate.getMonth()].toUpperCase();
//$('#page-content').hide();
var msgsss = "";

$(document).ready(function() {
    SHandler = new SearchHelper();

    SHandler.BindEvents();
});
var SearchHelper = function() {

    this.Tfrom1;
    this.hidTfrom1;
    this.Tfrom2;
//    this.hidTfrom2;
    this.Tto1;
    this.hidTto1;
    this.TDeptdate1;
    this.hidTDeptdate1;
    this.TReturn1;
    this.hidTReturn1;
    this.Tsubmit;
    this.ARdatetxt1;
    this.depdate;
    this.Adult;
    this.child;
    this.infant;


}
SearchHelper.prototype.BindEvents = function() {

    var h = this;
    var query = h.getquerystring();
    h.Tfrom1 = query.split('=')[1].split('&')[0].replace(/%20/g, " ");
    h.TDeptdate1 = query.split('=')[2].split('&')[0].replace(/%20/g, " ");
    h.TReturn1 = query.split('=')[3].split('&')[0].replace(/%20/g, " ");
    h.triptype = query.split('=')[4].split('&')[0].replace(/%20/g, " ");
    Trip = query.split('=')[4].split('&')[0].replace(/%20/g, " ");
    h.requesttype = query.split('=')[5].split('&')[0].replace(/%20/g, " ");
    h.Adult = query.split('=')[6].split('&')[0].replace(/%20/g, " ");
    h.child = query.split('=')[7].split('&')[0].replace(/%20/g, " ");
    h.infant = query.split('=')[8].split('&')[0].replace(/%20/g, " ");
    h.Tto1 = query.split('=')[9].split('&')[0].replace(/%20/g, " ");
    sorced = query.split('=')[9].split('&')[0].replace(/%20/g, " ");
    Aviblity(h);
  
    $(document).on("click", "#alllservice", function(e) {
        $(".clsservices").toggleClass("show");
        $(".sh").toggleClass("show");
    });

}

SearchHelper.prototype.getquerystring = function() {
    return window.location.search.substring(1);
}
function Aviblity(searchrequest) {
    $('#basic-modal-content').show();
    $('#mody').hide();
    $('#page-content').hide();

    if (searchrequest.requesttype == "Resort") {

        var ResortID = searchrequest.Tfrom1;
        var fbUrl = 'resortid=' + ResortID
                    + '&arrivaldate=' + searchrequest.TDeptdate1
                   + '&departuredate=' + searchrequest.TReturn1
                   + '&adults=' + searchrequest.Adult
                      + '&children=' + searchrequest.child
                     + '&infants=' + searchrequest.infant

    }
    if (searchrequest.requesttype == "Airport") {
        var AirportID = searchrequest.Tfrom1.split(')')[0].substring((searchrequest.Tfrom1).split(')')[0].indexOf('(') + 1);
        var fbUrl = 'destination=' + AirportID
                    + '&arrivaldate=' + searchrequest.TDeptdate1
                   + '&departuredate=' + searchrequest.TReturn1
                    + '&adults=' + searchrequest.Adult
                      + '&children=' + searchrequest.child
                     + '&infants=' + searchrequest.infant


    }

    var Url = UrlBase + "Transfer/A2BService.asmx/FetchA2BSearchList";
    $.ajax({
        url: Url,
        contentType: "application/json; charset=utf-8",
        data: "{'Searchreq':'" + fbUrl + "'}",
        dataType: "json",
        type: "POST",
        async: true,
        success: function(data) {
            var dataaaaa = data.d;
            //            var xml = StringToXML(dataaaaa);
            //            var respGS1 = xmlToJson(xml);
            totresponse = data.d;
            GetLayout(data.d, searchrequest);
            $('#basic-modal-content').hide();
            $('#mody').hide();
            $('#page-content').show();
            //            $("#Lay").html(A2BLayout);
        }, error: function(XMLHttpRequest, textStatus, errorThrown) {
             
            $('#basic-modal-content').hide();
            $('#mody').hide();
            $('#page-content').show();
            alert(textStatus);
        }
    });

}

function GetLayout(index, hres) {


    var ImgUP = "<img src='" + UrlBase + "CSS/images/A2Bimg/UP.png'/>";
    var ImgDU = "<img src='" + UrlBase + "CSS/images/A2Bimg/DU.png'/>";
    var HeaderLayout = "";
    var strdtlsLayout = "";
    ttService1 = new Array();
    tttravelaer1 = new Array();

    $("#Air").hide();
    if (index == "") {
        strdtlsLayout += "<div>";
        HeaderLayout += "<div>";
        HeaderLayout += "<div class='w100 lft bgtc lh30 bld'>";
        HeaderLayout += "<div class='w30 bgtc1 padding1s lft'><div class='w50 lft'>Currency</div><div class='w50 lft'> Destination Name</div><br/><div class='w50 lft transprice'>INR </div><div class='w50 lft'> " + hres.Tfrom1 + "</div></div><div class='w30 bgtc1 padding1s lft'><div class='w50 lft'> Departure Date</div>  Arrival Date <br/><div class='w50 lft'> " + hres.TDeptdate1 + "</div>" + hres.TReturn1 + "</div><div class='w33 bgtc1 padding1s lft'><div class='w30 lft'> Adults </div><div class='w30 lft'> Children</div>  Infants <br/><div class='w30 lft'> " + hres.Adult + "</div><div class='w30 lft'> " + hres.child + "</div>" + hres.infant + "</div>";
        HeaderLayout += "</div>";
        HeaderLayout += "</div>";
        strdtlsLayout += HeaderLayout;
    }
    else {
        if (index[0].ArrivalDate != "") {
            

            strdtlsLayout += "<div>";
            if (index.ErrorMsg == undefined) {
                HeaderLayout += "<div>";

                HeaderLayout += "<div class='w100 lft bgtc lh30 bld'>";
                HeaderLayout += "<div class='w30 bgtc1 padding1s lft'><div class='w50 lft'>Currency</div><div class='w50 lft'> Destination Name</div><br/><div class='w50 lft transprice'>INR </div><div class='w50 lft'> " + index[0].Destination_Name + "</div></div><div class='w30 bgtc1 padding1s lft'><div class='w50 lft'> Departure Date</div>  Arrival Date <br/><div class='w50 lft'> " + index[0].DepartureDate + "</div>" + index[0].ArrivalDate + "</div><div class='w33 bgtc1 padding1s lft'><div class='w30 lft'> Adults </div><div class='w30 lft'> Children</div>  Infants <br/><div class='w30 lft'> " + index[0].Adults + "</div><div class='w30 lft'> " + index[0].Children + "</div>" + index[0].Infants + "</div>";
                HeaderLayout += "</div>";


                HeaderLayout += "</div>";
                strdtlsLayout += HeaderLayout;
                strdtlsLayout += "<div class='w100'><hr/></div>";
                strdtlsLayout += "<div class='clear1'></div>";
                strdtlsLayout += "<div>";
                strdtlsLayout += "<div>";
                strdtlsLayout += "<div class='w100  boxshadow lft bgf1 lh30 bld hideheader jplist-panel'>";
                strdtlsLayout += "<div class='hidden w30 pk lft cursorpointer' data-control-type='sortdistance1' data-control-name='sortdistance1' data-control-action='sort' data-path='.dis' data-order='asc' data-type='number' title='Sort by Distance'>" + ImgDU + "Distance</div><div class='hidden w30 wh lft cursorpointer' data-control-type='sortduration' data-control-name='sortduration' data-control-action='sort' data-path='.dur' data-order='asc' data-type='number' title='Sort by Duration'>" + ImgDU + "Duration</div><div class='hidden w30 bl lft cursorpointer ' data-control-type='sortrate' data-control-name='sortrate' data-control-action='sort' data-path='.Rate' data-order='asc' data-type='number' title='Sort by Rate'>" + ImgDU + "Rates</div>";
                strdtlsLayout += "</div>";
                strdtlsLayout += "<div  class='list box text-shadow'>";
                if (index.length != undefined) {

                    for (var nt = 0; nt < index.length; nt++) {

                        ttService1.push(index[nt].LocationTo);
                        strdtlsLayout += "<div class ='w100 boxshadow lft bgtc list-item box totdata1 rtgrid1'>";

                        //   strdtlsLayout += "<div class='w30 padding1s lft' id=" + index[nt].TransferID + "K" + nt + " class='w20 padding1s lft' ><br/><img style='width:250px;height:108px;' src='" + UrlBase + "CSS/images/A2Bimg/" + index[nt].Transfer_Type + ".jpg' /></div><div class='hide'></div>";
                        strdtlsLayout += "<div class='w30 padding1s lft' id=" + index[nt].TransferID + "K" + nt + " class='w20 padding1s lft'><br/><img style='width:250px;height:108px;' src='" + UrlBase + "CSS/images/A2Bimg/" + index[nt].Transfer_Type + ".jpg' /></div><div class='hide'></div>";
                        strdtlsLayout += "<div class='w40 padding1s lft transfrom Transfer_Type'>" + "<span class='pvttrans'>" + index[nt].Transfer_Type + "</span><br/>" + "<span class='transto'>From: </span>" + index[nt].LocationFrom + "<br/>" + "<span class='transto'>To: </span><span class=' servicetyO'>" + index[nt].LocationTo + "</span><br/><div class='dis hide'>" + index[nt].Distance + "</div><span class='transto'>Distance: </span>" + index[nt].Distance + " KM <div class='dur hide'>" + index[nt].Duration + "</div><span class='transto'>Duration: </span>" + index[nt].Duration + " Minutes </div>";


                        if (Trip == "Oneway") {
                            strdtlsLayout += "<div class='w20 padding1s lft'><div class='Rate hide'>" + index[nt].TotalIRate + "</div><span class='pvttrans1'>Total Price </span><br/><span class='transprice'>INR " + index[nt].TotalIRate + " " + index[nt].Price_Policy + "</span><br/><div id=" + nt + " class='bookindiv bld padding1s w40 lft transbutton' onclick='insertTransferBookingRequest(this.id)'>Book Now</div><div class='clear'></div><div  id='" + index[nt].TransferID + "K" + nt + "K' class='w20 padding1 lft TrDetails' >Detail</div><div class='hide ABtooltip'></div></div>"
                        } else {

                            strdtlsLayout += "<div class='w20 padding1s lft'><div class='Rate hide'>" + index[nt].TotalBRate + "</div><span class='pvttrans1'>Total Price </span><br/><span class='transprice'>INR " + index[nt].TotalBRate + " " + index[nt].Price_Policy + "</span><br/><div id=" + nt + " class='bookindiv bld padding1s w40 lft transbutton' onclick='insertTransferBookingRequest(this.id)'>Book Now</div><div class='clear'></div><div  id='" + index[nt].TransferID + "K" + nt + "K' class='w20 lft TrDetails' >Detail</div><div class='hide ABtooltip'></div></div>"

                        }

                        strdtlsLayout += "</div>";

                    }
                }
                else {
                    strdtlsLayout += "<div class ='w100 boxshadow lft bgf1 list-item box totdata1 rtgrid1'>";

                    //   strdtlsLayout += "<div class ='w100 boxshadow lft bgf1 list-item box totdata1 rtgrid1'>";
                    strdtlsLayout += "<div>";
                    strdtlsLayout += "<div class='w30 padding1s lft' id=" + index.TrSearchRq.transfer.Transfer_Type_ID["#text"] + "K" + 0 + " class='w20 padding1s lft'><br/><img style='width:250px;height:108px;' src='" + UrlBase + "CSS/images/A2Bimg/private_transfer_standard.jpg' /></div><div class='hide '></div>";
                    strdtlsLayout += "<div class='w40 padding1s lft transfrom Transfer_Type'>" + "<span class='pvttrans'>" + index.TrSearchRq.transfer.Transfer_Type["#text"] + "</span><br/>" + "<span class='transto'>From: </span>" + index.TrSearchRq.transfer.LocationFrom["#text"] + "<br/>" + "<span class='transto'>To:</span><span class=' servicetyO'>" + index.TrSearchRq.transfer.LocationTo["#text"] + "</span><br/><div class='dis hide'>" + index.TrSearchRq.transfer.Distance["#text"] + "</div><span class='transto'>Distance: </span>" + index.TrSearchRq.transfer.Distance["#text"] + " KM <div class='dur hide'>" + index.TrSearchRq.transfer.Duration["#text"] + "</div><span class='transto'>Duration: </span>" + index.TrSearchRq.transfer.Duration["#text"] + " Minutes </div>";
                    if (Trip == "Oneway") {
                        strdtlsLayout += "<div class='w20 padding1s lft'><div class='Rate hide'>" + index.TrSearchRq.transfer.Rates.TotalIRate.Rate["#text"] + "</div><span class='pvttrans1'>Total Price </span><br/><span class='transprice'>INR " + index.TrSearchRq.transfer.Rates.TotalIRate.Rate["#text"] + " " + index.TrSearchRq.transfer.Price_Policy["#text"] + "</span><br/><div id=" + 0 + " class='bookindiv bld padding1s w40 lft transbutton' onclick='insertTransferBookingRequest(this.id)'>Book Now</div><div class='clear'></div><div  id='" + index.TrSearchRq.transfer.Transfer_Type_ID["#text"] + "K" + 0 + "K' class='w20  lft TrDetails' >Detail</div><div class='hide ABtooltip'></div></div>"
                    } else {

                        strdtlsLayout += "<div class='w20 padding1s lft'><div class='Rate hide'>" + index.TrSearchRq.transfer.Rates.TotalBRate.Rate["#text"] + "</div><span class='pvttrans1'>Total Price </span><br/><span class='transprice'>INR " + index.TrSearchRq.transfer.Rates.TotalBRate.Rate["#text"] + " " + index.TrSearchRq.transfer.Price_Policy["#text"] + "</span><br/><div id=" + 0 + " class='bookindiv bld padding1s w40 lft transbutton' onclick='insertTransferBookingRequest(this.id)'>Book Now</div><div class='clear'></div><div  id='" + index.TrSearchRq.transfer.Transfer_Type_ID["#text"] + "K" + 0 + "K' class='w20  lft TrDetails' >Detail</div><div class='hide ABtooltip'></div></div>"
                    }
                    //                
                    strdtlsLayout += "</div>";

                }
                strdtlsLayout += "</div>";

                strdtlsLayout += "</div>";

            }
            else {
                strdtlsLayout += "<div>" + index.TrSearchRq.ErrorMsg["#text"] + "</div>";

            }
        }
    }
    strdtlsLayout += "</div>";
    strdtlsLayout += "<div class='noRes1 boxshadow bgf1'><p>No results found .Please try to modify your search.</p></div>";
    //  strdtlsLayout += "</div> <div class='jplist-no-results'><p>No results found</p></div>";
    A2BLayout = strdtlsLayout;
    $("#a2bresult").html("");
    $("#a2bresult").html(strdtlsLayout);
    $('#Load1').hide();
    $("#a2bresult").show();
    ttService.push(ttService1);
    var no = 0
    $("#ServiceSort1").html(createNewserviceTypeFliter(no, ttService1));

    $("#ServiceSort1").show();
    $("#modfy").show();
    setjplist();

}



function getallcityListNew(id) {


    var autoResortName = UrlBase + "Transfer/A2BService.asmx/FetchResortList";
    var txt = $("#hidTfrom1").val().substring(0, $("#hidTfrom1").val().lastIndexOf('('));

    $.ajax({
        url: autoResortName,
        data: "{'code': '" + txt + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function(data) {
            mainArrCity = new Array();
            if (mainArrCity.length == 0)
                flagC = true;
            else
                flagC = false;
            var cityLayout = "<table cellpadding='0' cellspacing='11' border='0px' style='color:#888;' width='100%'>";
            if (data.d.length > 0) {
                for (var i = 0; i < data.d.length; i++) {
                    //   if (i % 4 != 0) {
                    cityLayout += "<tr><td style='color:black; font-weight:bold; id='" + data.d[i].ResortName + "_" + data.d[i].CountryName + "'>" + data.d[i].ResortName + "_" + data.d[i].CountryName + "</td></tr>";
                    //                    }
                    //                    else {
                    //                        cityLayout += "</tr><tr><td style='color:black; font-weight:bold;' id='" + data.d[i].ResortName + "_" + data.d[i].CountryName + "'>" + data.d[i].ResortName + "_" + data.d[i].CountryName + "</td>";
                    //                    }
                    if (flagC == true)
                        mainArrCity.push({ id: data.d[i].ResortName + "_" + data.d[i].CountryName });
                }
                cityLayout += "</table>";


                cityLayout1 = cityLayout;

                //                $("#resortto").html(cityLayout);
                // $("#dep").html(cityLayout);

                //return {cityLayout1 }
            }
        }
    });
}

SearchHelper.prototype.UpdateRoundTripMininumDate = function(h, t) {
    SHandler.TReturn1.datepicker("option", {
        minDate: h

    });
}
xmlToJson = function(xml) {
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
function insertTransferBookingRequest(id) {
    // var objTxtTr = new Array();
    //var objTxtTr = "sujeet";
    var bookindiv = $(".bookindiv");
    //if (bookindiv.length > 0) {
    var slectedItem = $(bookindiv[parseInt(this.id)]);
    // var item = totresponse;
    // var rateinbound = $(".Rates").val();
    var rateinbound;
    var rateinboundID; var netfare; var totalfare; var originalfare;var EXCHANGERATE;
    if (Trip == "Oneway") {
        if (totresponse.length != undefined) {
            rateinboundID = totresponse[id].IRate_ID
            rateinbound = totresponse[id].OriginalIRate
            netfare = totresponse[id].NetIRate
            totalfare = totresponse[id].TotalIRate
            originalfare = totresponse[id].OriginalIRate
            EXCHANGERATE = totresponse[id].EXCHANGEIRate;
        }
        else {
            rateinboundID = totresponse.TrSearchRq.transfer.Rates.OriginalIRate.Rate_Id["#text"]
            rateinbound = totresponse.TrSearchRq.transfer.Rates.OriginalIRate.Rate["#text"]
        }
    } else {
        if (totresponse.length != undefined) {
            rateinboundID = totresponse[id].BRate_ID
            rateinbound = totresponse[id].OriginalBRate
            netfare = totresponse[id].NetBRate
            totalfare = totresponse[id].TotalBRate
            originalfare = totresponse[id].OriginalBRate
            EXCHANGERATE = totresponse[id].EXCHANGEBRate;
        } else {
            rateinboundID = totresponse.TrSearchRq.transfer.Rates.Both_Rate.Rate_Id["#text"]
            rateinbound = totresponse.TrSearchRq.transfer.Rates.OriginalIRate.Rate["#text"]
        }
    }
    var item = new Array();
    var objTxtTr;
    if (totresponse.length != undefined) {
        objTxtTr = [totresponse[0].Currency, //Currency
    totresponse[0].Session_ID, //session_id
         totresponse[0].Destination_Name, //Destination_Name
    totresponse[0].DepartureDate, //DepartureDate
      totresponse[0].ArrivalDate, //ArrivalDate
     totresponse[0].Adults, //Adults
        totresponse[0].Children, //Children
     totresponse[0].Infants, //Infants
        totresponse[id].LocationFrom, //LocationFrom
  totresponse[id].LocationTo, //LocationTo
  totresponse[id].Distance, //Distance
  totresponse[id].Duration, //Duration
     totresponse[id].Price_Policy, //Price_Policy
        //       totresponse[id].Num_Transfers["#text"], //Num_Transfers
         Trip,
  rateinbound, //OriginalIRate
    totresponse[id].LocID, //LocID
    rateinboundID,
        //totresponse.TrSearchRq.transfer[id].Rates.OriginalIRate.Rate_Id["#text"], //Rate_ID
totresponse[id].TransferID, //Transfer_Type_ID
totresponse[id].Transfer_Type, //Transfer_Type_ID
 originalfare,
 netfare,
 totresponse[id].AdminMrk,
 totresponse[id].AgentMrk,
totalfare,
totresponse[id].Provider,
totresponse[id].AgentID,
EXCHANGERATE,
totresponse[id].RateOFEX,
totresponse[id].Num_Transfers


    ];
    }
    else {
        objTxtTr = [totresponse.TrSearchRq.Currency["#text"], //Currency
    totresponse.TrSearchRq.session_id["#text"], //session_id
         totresponse.TrSearchRq.Destination_Name["#text"], //Destination_Name
    totresponse.TrSearchRq.Search_Criteria.DepartureDate["#text"], //DepartureDate
      totresponse.TrSearchRq.Search_Criteria.ArrivalDate["#text"], //ArrivalDate
     totresponse.TrSearchRq.Search_Criteria.Adults["#text"], //Adults
        totresponse.TrSearchRq.Search_Criteria.Children["#text"], //Children
     totresponse.TrSearchRq.Search_Criteria.Infants["#text"], //Infants
        totresponse.TrSearchRq.transfer.LocationFrom["#text"], //LocationFrom
  totresponse.TrSearchRq.transfer.LocationTo["#text"], //LocationTo
  totresponse.TrSearchRq.transfer.Distance["#text"], //Distance
  totresponse.TrSearchRq.transfer.Duration["#text"], //Duration
     totresponse.TrSearchRq.transfer.Price_Policy["#text"], //Price_Policy
       totresponse.TrSearchRq.transfer.Num_Transfers["#text"], //Num_Transfers
    rateinbound, //OriginalIRate
      totresponse.TrSearchRq.transfer.LocID["#text"], //LocID
      rateinboundID,
        //totresponse.TrSearchRq.transfer.Rates.OriginalIRate.Rate_Id["#text"], //Rate_ID
totresponse.TrSearchRq.transfer.Transfer_Type_ID["#text"], //Transfer_Type_ID
totresponse.TrSearchRq.transfer.Transfer_Type["#text"]
    ];
    }
    item.push(objTxtTr);
    $.ajax({
        //url: 'user.aspx/InsertPaxDetails',

        url: UrlBase + "Transfer/A2BService.asmx/InsertTransferBookingRequest",
        //           url: "http://localhost:64784/Transfer1/A2BService.asmx",
        data: JSON.stringify({ 'arrPax': item }),
        //data: ({ 'arrPax': objTxtTr }),
        // data: "{'arrPax': '" + item + "', maxResults: 10 }",
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        //url: UrlBase + 'user.aspx/InsertPaxDetails',
        async: false,
        success: function(data) {
            OrderID = data.d;
            window.location.href = UrlBase + "Transfer/Paxinfo.aspx?ID=" + OrderID;

        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
             
            //alert(textStatus);
        }
    });
    //}
    // $("#Lay").hide();
    $("#a2bresult").hide();
    $("#modfy").hide();
    $("#finaldiv").show();
    if (Trip == "Oneway") {
        $("#finaldivA2").hide();
    }
    else {
        $("#finaldivA2").show();
    }

}



function funcheckboxcheckvalidation(id) {
    if ($(id).is(':checked') == true) {
        $('input.' + id.className).not(id)[0].checked = false;
        $('input.' + id.className).not(id)[1].checked = false
    }
    else
        id.checked = true;
}
function fun_SetMinMaxFare(fareList) {
    var priceList = ""; priceList = fareList; var priceArr = new Array();
    if (priceList.length != 0) {
        var arrPrice = new Array();
        for (var pri = 0; pri < priceList.length; pri++) {
            if ($(priceList[pri]).length != 0)

                arrPrice.push({ id: pri, price: $(priceList[pri]).text() });
        }
        var sortprice = arrPrice.sort(function(a, b) {
            return parseFloat(a.price) - parseFloat(b.price)
        });

        priceArr.push(parseFloat(sortprice[0].price));
        priceArr.push(parseFloat(sortprice[parseInt(sortprice.length) - 1].price));
    }
    else {
        priceArr.push(0);
        priceArr.push(0);
    }
    return priceArr;
}
function setjplist() {
    var Aprice;
    var Adistance;
    var Aduration;
    var Priceimage = "<img src='" + UrlBase + "image/images.jpg' style='height:10px;' />";
    Aprice = fun_SetMinMaxFare($(".Rate"));
    Adistance = fun_SetMinMaxFare($(".dis"));
    Aduration = fun_SetMinMaxFare($(".dur"));
    $('#demo').jplist({
        itemsBox: '.list'
      , itemPath: '.list-item'
      , panelPath: '.jplist-panel'
         , noResults: '.noRes1'
        , storage: '' //'', 'cookies', 'localstorage'			
        , storageName: 'jplist-list-grid'
            , controlTypes: {
                'sortdistance1': {
                    className: 'DefaultSort1'
                    , options: {}
                }
                , 'sortduration': {
                    className: 'DefaultSort1'
                    , options: {}
                }
                 , 'sortrate': {
                     className: 'DefaultSort1'
                    , options: {}
                 }
                ,
                'Service0': {
                    className: 'CheckboxGroupFilter'
                 , options: {}
                }, 'range-slider-price1': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider 
                     ui_slider: function($slider, $prev, $next) {
                         $slider.slider({
                             min: Aprice[0]
                             , max: Aprice[1]
                             , range: true
                             , values: [Aprice[0], Aprice[1]]
                             , slide: function(event, ui) {
                                 $prev.html(Priceimage + ui.values[0]);
                                 $next.html(Priceimage + ui.values[1]);
                             }
                         });
                     },
                     set_values: function($slider, $prev, $next) {
                         $prev.html("INR " + $slider.slider('values', 0));
                         $next.html("INR " + $slider.slider('values', 1));

                     }
                 }
                }
                 , 'range-slider-distance': {
                     className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider 
                     ui_slider: function($slider, $prev, $next) {
                         $slider.slider({
                             min: Adistance[0]
                             , max: Adistance[1]
                             , range: true
                             , values: [Adistance[0], Adistance[1]]
                             , slide: function(event, ui) {
                                 $prev.html(ui.values[0] + " KM");
                                 $next.html(ui.values[1] + " KM");
                             }
                         });
                     },
                     set_values: function($slider, $prev, $next) {
                         $prev.html($slider.slider('values', 0) + " KM");
                         $next.html($slider.slider('values', 1) + " KM");

                     }
                 }
                 }
                   , 'range-slider-duration': {
                       className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider 
                     ui_slider: function($slider, $prev, $next) {
                         $slider.slider({
                             min: Aduration[0]
                             , max: Aduration[1]
                             , range: true
                             , values: [Aduration[0], Aduration[1]]
                             , slide: function(event, ui) {
                                 $prev.html(ui.values[0] + " Minutes");
                                 $next.html(ui.values[1] + " Minutes");
                             }
                         });
                     },
                     set_values: function($slider, $prev, $next) {
                     $prev.html($slider.slider('values', 0) + " Minutes");
                     $next.html($slider.slider('values', 1) + " Minutes");

                     }
                 }
                   }
                    , 'checkbox-text-filter0T': {
                        className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+ checkbox-text-filter
                    }
                    }
            , 'checkbox-text-filter0S': {
                className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                    }

            }

            }



    });
}




$(document).on("mouseover", ".TrDetails", function(e) {
    var Cartype = this.id.split('K')[0];

    var nextdiv = $("#" + this.id).next();
    var carUrl = 'Transfer_Type_ID=' + Cartype
    var Url = UrlBase + "Transfer/A2BService.asmx/getresponse";
    $.ajax({
        url: Url,
        contentType: "application/json; charset=utf-8",
        data: "{'Searchreq':'" + carUrl + "'}",
        dataType: "json",
        type: "POST",
        async: true,
        success: function(data) {
            var dataaaaa = data.d;
            var xml = StringToXML(dataaaaa);
            var respcar = xmlToJson(xml);
            var carLayout = "<div  style=' background:#fff; padding:2%;'>";
            carLayout = "<div>";


            carLayout += "<div  class=' w40 lft'><div class='w15 lft'><b>Min:</b>" + respcar.TrSearchRq.Occupancy_From["#text"] + "</div>"
            carLayout += "<div class='w15 lft'><b>  Max: </b>" + respcar.TrSearchRq.Occupancy_To["#text"] + "</div>"
            carLayout += "<div class='w70 lft'><b> Tranfer_Type:</b> " + respcar.TrSearchRq.Tranfer_Type["#text"] + "</div></div>"
            if (dataaaaa.split('[')[2].split(']')[0] == "") {
                carLayout += "<div class='w60 rgt'><b> Description:</b>  " + " Not Available" + "</div>"
            }
            else {

                carLayout += "<div class='w60 rgt'><b> Description:</b> " + dataaaaa.split('[')[2].split(']')[0] + "</div>"
            }
            carLayout += "</div>";
            $(nextdiv).html(carLayout);
            $(nextdiv).toggleClass("hide");
            //$(nextdiv).show();
        }, error: function(XMLHttpRequest, textStatus, errorThrown) {
             
            alert(textStatus);
        }
    });
});
$(document).on("mouseout", ".TrDetails", function(e) {
   
    var nextdiv = $("#" + this.id).next();
    $(nextdiv).toggleClass("hide");
   // $(nextdiv).hide();

});

//stop;


$(".closemody").click(function() {
    $(".mody").fadeOut();
    //$(".mody").hide();
    //$(".mody").bPopup().close();
    //$("#a2bresult").show();
    //$("#modfy").show();
});

function modifysearch() {

    var newquery = window.location.search.substring(1);
    
    var rettype = newquery.split('=')[4].split('&')[0];
    var reqtype = newquery.split('=')[5].split('&')[0];
    if (reqtype == "Airport") {
        document.getElementById("ctl00_ContentPlaceHolder1_Transearch_RadioButton1").checked = true;
        $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1").val(newquery.split('=')[1].split('&')[0].replace(/%20/g, " "));
        $("#hidTfrom1").val(newquery.split('=')[1].split('&')[0].replace(/%20/g, " "));
        $("#ctl00_ContentPlaceHolder1_Transearch_Tto1").val(newquery.split('=')[9].split('&')[0].replace(/%20/g, " "));
        $("#hidTto1").val(newquery.split('=')[9].split('&')[0].replace(/%20/g, " "));
        $("#TDeptdate1").val(newquery.split('=')[2].split('&')[0].replace(/%20/g, " "));
        $("#TReturn1").val(newquery.split('=')[3].split('&')[0].replace(/%20/g, " "));
        $("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4").attr("disabled", false);
//        $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom2").val("");
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul1").hide();
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb2").show();
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul2").show();
        $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom2").val("");
    }

    if (rettype == "RoundTrip") {
        document.getElementById("ctl00_ContentPlaceHolder1_Transearch_RadioButton4").checked = true;
        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").show();
    }
    if (rettype == "Oneway") {
        document.getElementById("ctl00_ContentPlaceHolder1_Transearch_RadioButton3").checked = true;        
        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").hide();
        $("#TReturn1").val("");
    }

    if (reqtype == "Resort") {
        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").hide();
        $("#TReturn1").val("");
    document.getElementById("ctl00_ContentPlaceHolder1_Transearch_RadioButton2").checked = true;
    $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom2").val(newquery.split('=')[9].split('&')[0].replace(/%20/g, " "));
    // document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").value (newquery.split('=')[9].split('&')[0].replace(/%20/g, " ");HiddenResort
    $("#HiddenResort").val(newquery.split('=')[9].split('&')[0].replace(/%20/g, " "));
    $("#hidTfrom2").val(newquery.split('=')[1].split('&')[0].replace(/%20/g, " "));
    $("#TDeptdate1").val(newquery.split('=')[2].split('&')[0].replace(/%20/g, " "));
    $("#TReturn1").val(newquery.split('=')[3].split('&')[0].replace(/%20/g, " "));
    $("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4").attr("disabled", true);
    $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1").val("");
//    $("#ctl00_ContentPlaceHolder1_Transearch_Tto1").val("");
//    $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul1").show();
    //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb2").hide();
    $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul1").show();
    $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul2").hide();
    $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1").val("");
    $("#ctl00_ContentPlaceHolder1_Transearch_Tto1").val("");
   
}

    //h.triptype = newquery.split('=')[4].split('&')[0];
    // Trip = newquery.split('=')[4].split('&')[0];
  
   // h.requesttype = newquery.split('=')[5].split('&')[0];
$("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Adult").val(newquery.split('=')[6].split('&')[0].replace(/%20/g, " "));
$("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Child").val(newquery.split('=')[7].split('&')[0].replace(/%20/g, " "));
$("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Infrant").val(newquery.split('=')[8].split('&')[0].replace(/%20/g, " "));
   
//    $("#a2bresult").hide();
//    $("#modfy").hide();
//    $(".mody").show();
    $('.mody').fadeIn(); 
    }
function createNewserviceTypeFliter(Sindexid, servicet) {
    var loclist = "";
    if (servicet.length == 1)
        loclist = servicet;
    else
        loclist = removeDuplicates(servicet);
    var strservicelayout = "";
    strservicelayout += " <div class='clear'> </div><div><img class='lft'src='../CSS/images/A2Bimg/next1.png' style='position: relative; top: 3px;' /><div class='f14 bld colorb closeopenss1 cursorpointer'>Filter By Place </div></div>";
    if (Sindexid == 0)
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Sindexid + "S' data-control-action='filter' data-control-name='checkbox-text-filter0S' data-path='.servicetyO' data-logic='or'>";
    else
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Sindexid + "S' data-control-action='filter' data-control-name='checkbox-text-filter1S' data-path='.Rservicety' data-logic='or'>";
    strservicelayout += "<div>";
    for (var sr = 0; sr < loclist.length; sr++) {
        if (sr < 3) {
            if (loclist[sr].toUpperCase() == sorced.split('(')[0].toUpperCase())
                strservicelayout += '<div class="lft w10" > <input checked="true" value="' + loclist[sr] + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w88 textnowrap cursorpointer"> <label for="' + loclist[sr] + '" title="' + loclist[sr] + '">' + loclist[sr] + '</label></div><div class="clear"> </div>';
            else
                strservicelayout += '<div class="lft w10" > <input value="' + loclist[sr] + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w88 textnowrap cursorpointer"> <label for="' + loclist[sr] + '" title="' + loclist[sr] + '">' + loclist[sr] + '</label></div><div class="clear"> </div>';
        }
        else {
            if (loclist[sr].toUpperCase() == sorced.split('(')[0].toUpperCase())
                strservicelayout += '<div class="lft w10 hide clsservices"> <input checked="true" value="' + loclist[sr] + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w88 textnowrap cursorpointer hide clsservices"> <label for="' + loclist[sr] + '" title="' + loclist[sr] + '">' + loclist[sr] + '</label></div><div class="clear hide clsservices"> </div>';
        else
            strservicelayout += '<div class="lft w10 hide clsservices"> <input value="' + loclist[sr] + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w88 textnowrap cursorpointer hide clsservices"> <label for="' + loclist[sr] + '" title="' + loclist[sr] + '">' + loclist[sr] + '</label></div><div class="clear hide clsservices"> </div>';
        }
    }
    if (loclist.length > 3)
        strservicelayout += "<div id='alllservice' class='f14 cursorpointer padding2s bld colorb closeopenss1 '>  <span class='sh'>Show all " + loclist.length + " Place</span> <span class='sh hide'>Hide all " + loclist.length + " Place</span></div><div class='clear'> </div>";
    strservicelayout += "</div></div>";
    return strservicelayout;

}
function removeDuplicates(inputArray) {
    var i;
    var len = inputArray.length;
    var outputArray = [];
    var temp = {};
    for (i = 0; i < len; i++) {
        temp[inputArray[i]] = 0;
    }
    for (i in temp) {
        outputArray.push(i);
    }
    return outputArray;
}


$(document).on("click", "[data-control-name='sortdistance1'],[data-control-name='sortduration'],[data-control-name='sortrate']", function(e) {

    if ($(".list .list-item").length != 1 && $(".list .list-item").length != 0 && $(".noRes1")[0].className == "noRes1 boxshadow bgf1 jplist-hidden") {
        if ($(this).attr("data-control-name") == "sortdistance1") {
            if (ImgUP != $(this).find("img")[0].src)
                $(this).find("img")[0].src = ImgUP;
            else
                $(this).find("img")[0].src = ImgDU;
            $("[data-control-name='sortduration']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortrate']").find("img")[0].src = ImgDU;
        }
        else if ($(this).attr("data-control-name") == "sortduration") {
            if (ImgUP != $(this).find("img")[0].src)
                $(this).find("img")[0].src = ImgUP;
            else
                $(this).find("img")[0].src = ImgDU;
            $("[data-control-name='sortdistance1']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortrate']").find("img")[0].src = ImgDU;
        }
        else if ($(this).attr("data-control-name") == "sortrate") {
            if (ImgUP != $(this).find("img")[0].src)
                $(this).find("img")[0].src = ImgUP;
            else
                $(this).find("img")[0].src = ImgDU;
            $("[data-control-name='sortduration']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortdistance1']").find("img")[0].src = ImgDU;
        }
    }
});

  $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1").click(function() {
    $("#ctl00_ContentPlaceHolder1_Transearch_Tto1").val("");

});
$(document).on("click", ".jplist-reset-btn", function(e) {
var ImgUP = "http://" + window.location.host + UrlBase + "BS/Images/UP.png";
var ImgDU = "http://" + window.location.host + UrlBase + "BS/Images/DU.png";
   
    $(".list-view")[0].style.backgroundImage = "url('../BS/Images/list-btn.png')";
   
  
        $("[data-control-name='sortMinFare']").find("img")[0].src = ImgDU;
        $("[data-control-name='sortArrival']").find("img")[0].src = ImgDU;
        $("[data-control-name='sortDept']").find("img")[0].src = ImgDU;
        $("[data-control-name='sortMinFare1']").find("img")[0].src = ImgDU;
        $("[data-control-name='sortArrival1']").find("img")[0].src = ImgDU;
        $("[data-control-name='sortDept1']").find("img")[0].src = ImgDU;
   
   
});