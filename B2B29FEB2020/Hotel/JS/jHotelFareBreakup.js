$(document).ready(function() {
    //marquee
    $('marquee').parent().hide();
    //Hotel Cashback Start
    $(".brekups").live("mouseover", function() {
        var thisid = this;
        var chashbackId = thisid.id.split('_');
        $.ajax({
            url: 'HotelSearchs.asmx/HotelCashback',
            contentType: 'application/json; charset=utf-8',
            type: 'POST', dataType: 'json',
            data: "{'HotelPrice': '" + chashbackId[1] + "','Star': '" + this.title + "'}",
            success: function(data) {
                if (data.d != "") {
                    $("#" + $(thisid).attr("id")).qtip({
                        overwrite: false,
                        content: { text: data.d },
                        show: { ready: true },
                        style: { classes: "ui-tooltip-green" },
                        position: { my: "top right", at: "bottom left" }
                    })
                }
            }
        });
    //}).mouseout(function() {
    //    $(".brekups").hide();
    });
    //Hotel Cashback End
});
// Hotel Result Page Cancellation Plicy Start
function polcy(HotelCode, CityCode, HotelName, Provider) {
    $('#map_canvas').hide(); $('#mapss').hide();
    $('#cacellationPolicy').html("")
    $('#basic-modal-content').modal();
    $('#selected_Hotel').html("Hotel Name:  " + HotelName + "  &nbsp;&nbsp;    ");
    $('#cacellationPolicy').html("<img src='../images/loading_bar.gif' style='width:100px; height:20px; margin-left:400px;' />")
    $.ajax({
        url: 'HotelSearchs.asmx/CancPolicy',
        contentType: 'application/json; charset=utf-8',
        type: 'POST', dataType: 'json',
        data: "{'HotelCode': '" + HotelCode + "', 'RatePlaneCode': '" + CityCode + "', 'Provider': '" + Provider + "'}",
       // data: "{'HotelCode': '" + HotelCode + "', 'CityCode': '" + CityCode + "', 'Provider': '" + Provider + "'}",
        success: function(data) {
        if (data.d != "") {
            if (data.d != "SessionExpired")
                $('#cacellationPolicy').html('<br />' + data.d);
            else
                window.location.href = UrlBase + 'Login.aspx';
            }
            else
                $('#cacellationPolicy').html('<br /> Cancellattion policy not available');
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            $("#cacellationPolicy").innerText = "Cancellation Policy not available";
        }
    });
}
// Hotel Result Page Cancellation Plicy end
    

//Price Breakups start
function farebrekups(RoomTotal, PerNightRate, DisCount, TotalDiscount, Provider, Star, AgtMrk, Taxes, Extraguest, AgentCashBack, GSTAmt, TDSAmt) {
    var strbrekup = "";
    var week = 1; 
    var discounts = parseFloat(DisCount);
    var roomNightlyRate = PerNightRate.split('/');
    var dateformate = $.trim($(".Fcheckin").text()).split('/');
    var newdate = new Date(dateformate[1] + "/" + dateformate[0] + "/" + dateformate[2]);
    var NoofNight = parseInt($('.nightcount').text());
    if (Provider == "GTA") {
        var DiscPernight = 0;
        var GTAPerRoomNight = parseInt(parseInt(RoomTotal) / NoofNight);
        for (var i = 0; i < NoofNight; i++) {
            var day = newdate.getDay();
            var sCheckIn = newdate.getDate() + " " + Datedays(newdate.getMonth());
            var strDayPrice = "";
            if (DiscPernight > 0)
                strDayPrice = "<table style='border:none;'><tr><td style='border:none;'>" + sCheckIn + "</td></tr><tr><td style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> " + GTAPerRoomNight + "</td></tr><tr><td style='border:none;' class='rsprst' ><img src='Images/htlrs.png' class='pagingSize' /> " + DiscPernight + "</td></tr></table>";
            else
                strDayPrice = "<table style='border:none;'><tr><td style='border:none;'>" + sCheckIn + "</td></tr><tr><td style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> " + GTAPerRoomNight + "</td></tr><tr><td style='border:none;' ></td></tr></table>";

            week = SetDailyWisePrice(week, day, strDayPrice);
            newdate.setDate(newdate.getDate() + 1);
        }
    }
    else if (Provider == "TG" || Provider == "ZUMATA") {
       
        var discountRate = TotalDiscount.split("/")
        for (var j = 0; j < roomNightlyRate.length - 1; j++) {
            var Tday = newdate.getDay();
            var TCheckIn = newdate.getDate() + " " + Datedays(newdate.getMonth());
            var strDayPrice = ""
            if (discounts > 0)
                strDayPrice = ("<div style='border:none;'><div class='lft mgrt20' style='border:none;'>" + TCheckIn + "</div><div class='lft mgrt20' style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> ") + roomNightlyRate[j] + "</div><div class='lft discprice1' style='border:none;' class='rsprst' ><img src='Images/htlrs.png' class='pagingSize' /> " + discountRate[j] + "</div></div>";
            else
                strDayPrice = ("<div style='border:none;'><div class='lft' style='border:none;'>" + TCheckIn + "</div><div class='lft mgrt20' style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> ") + roomNightlyRate[j] + "</div><div class='lft' style='border:none;' ></div></div>";

            week = SetDailyWisePrice(week, Tday, strDayPrice);
            newdate.setDate(newdate.getDate() + 1);
        }
    }
    else if (Provider == "ROOMXML" || Provider == "INNSTANT" || Provider == "GAL" || Provider == "SuperShopper") {
        var DiscPernight = 0; 
        var RoomXMLPerNight = parseInt(parseInt(RoomTotal) / NoofNight);
        for (var k = 0; k < NoofNight; k++) {
            var Rday = newdate.getDay();
            var RCheckIn = newdate.getDate() + " " + Datedays(newdate.getMonth());
            var strDayPrice = "";
            if (DiscPernight > 0)
                strDayPrice = "<table class='table table-bordered' style='margin-bottom:0px;'><tr><td>" + RCheckIn + "</td><td><img src='Images/htlrs.png' class='pagingSize' /> " + RoomXMLPerNight + "</td></tr><tr><td class='rsprst' ><img src='Images/htlrs.png' class='pagingSize' /> " + DiscPernight + "</td></tr></table>";
            else
                strDayPrice = "<table class='table table-bordered' style='margin-bottom:0px;'><tr><td>" + RCheckIn + "</td><td><img src='Images/htlrs.png' class='pagingSize' /> " + RoomXMLPerNight + "</td></tr></table>";

            week = SetDailyWisePrice(week, Rday, strDayPrice);
            newdate.setDate(newdate.getDate() + 1);
        }
    }
    else if (Provider == "EX") {
        var DiscPernight = 0;
        var EXPerNight = parseInt(discounts / NoofNight);
        for (var k = 0; k < NoofNight; k++) {
            var Rday = newdate.getDay();
            var RCheckIn = newdate.getDate() + " " + Datedays(newdate.getMonth());
            var strDayPrice = "";
            if (discounts > 0)
                strDayPrice = "<table style='border:none;'><tr><td style='border:none;'>" + RCheckIn + "</td></tr><tr><td style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> " + roomNightlyRate[k] + "</td></tr><tr><td style='border:none;' class='rsprst' ><img src='Images/htlrs.png' class='pagingSize' /> " + EXPerNight + "</td></tr></table>";
            else
                strDayPrice = "<table style='border:none;'><tr><td style='border:none;'>" + RCheckIn + "</td></tr><tr><td style='border:none;'><img src='Images/htlrs.png' class='pagingSize' /> " + roomNightlyRate[k] + "</td></tr><tr><td style='border:none;' ></td></tr></table>";

            week = SetDailyWisePrice(week, Rday, strDayPrice);
            newdate.setDate(newdate.getDate() + 1);
        }
    } 
    var taxses = parseInt(Taxes);
    var ExtGust = parseInt(Extraguest);
    var totattax = parseFloat(Taxes) + parseFloat(Extraguest);
    var AmtBeforetax = parseFloat(RoomTotal) - totattax;
    strbrekup += "<div><div class='lft'>Room Rate for " + NoofNight + " Night: </div><div class='lft'> <img src='Images/htlrs.png' class='pagingSize' /> ";
    strbrekup += AmtBeforetax.toFixed(2) + "</div></div>";
    strbrekup += "<div class='clear'></div>";

if (parseFloat(Taxes) > 0) 
    strbrekup += "<div class='lft'>Tax : </div><div class='lft'>+<img src='Images/htlrs.png' class='pagingSize' /> " + Taxes + "</div>";
strbrekup += "<div class='clear'></div>";
if (parseFloat(Extraguest) > 0)
    strbrekup += "<div class='lft'>Extra Guest Charge : </div><div class='lft'>+<img src='Images/htlrs.png' class='pagingSize' /> " + Extraguest + "</div>";
strbrekup += "<div class='lft bld'>Total Price : </div><div class='lft bld'><img src='Images/htlrs.png' class='pagingSize' /> " + RoomTotal + "</div>";

var cashbakStr = "";
var agtpricess = parseInt(RoomTotal) - parseInt(AgtMrk) - AgentCashBack;
cashbakStr += "<div class='clear'></div>";
cashbakStr += "<div class='lft w100' style='padding:4px;font-size:13px;line-height:130%;'>";
if (AgentCashBack > 0) {
    cashbakStr += "<div class='lft  w50'>Commision : </div><div class='lft  w50'>&nbsp;&nbsp;- <img src='Images/htlrs.png' class='pagingSize' />" + (AgentCashBack - GSTAmt + TDSAmt).toFixed(2) + "</div>";
    cashbakStr += "<div class='clear'></div>";
    // cashbakStr += "<div class='lft  w50'>GST On Commision : </div><div class='lft  w50'>&nbsp;&nbsp;- <img src='Images/htlrs.png' class='pagingSize' />" + GSTAmt + "</div>";
    cashbakStr += "<div class='lft  w50'>TDS On Commision : </div><div class='lft  w50'>&nbsp;&nbsp;+ <img src='Images/htlrs.png' class='pagingSize' />" + TDSAmt.toFixed(2) + "</div>";
    cashbakStr += "<div class='clear'></div>";
}
cashbakStr += "<div class='lft  w50'>Transaction Fee : </div><div class='lft  w50'>- <img src='Images/htlrs.png' class='pagingSize' /> " + AgtMrk + "</div>";
cashbakStr += "<div class='clear'></div>";
cashbakStr += "<div class='lft w100 bld'><div class='lft  w50'>Price To Customer:</div><div class='lft w50'>&nbsp;&nbsp;<img src='Images/htlrs.png' class='pagingSize' /> " + agtpricess.toFixed(2) + "</div></div>";
cashbakStr += "<div class='clear'></div></div>";
$("#PriceBreakups").html(strbrekup + cashbakStr);
 
    $(".HotelFareBrekup").show();
}
function Datedays(checkins) {

    var dayss = "";
    switch (checkins) {
        case 0:
            dayss = "JAN";
            break;
        case 1:
            dayss = "FEB";
            break;
        case 2:
            dayss = "MAR";
            break;
        case 3:
            dayss = "APR";
            break;
        case 4:
            dayss = "MAY";
            break;
        case 5:
            dayss = "JUN";
            break;
        case 6:
            dayss = "JUL";
            break;
        case 7:
            dayss = "AUG";
            break;
        case 8:
            dayss = "SEP";
            break;
        case 9:
            dayss = "OCT";
            break;
        case 10:
            dayss = "NOV";
            break;
        case 11:
            dayss = "DEC";
            break;
    }
    return dayss;
}

//Price Breakups end

//Set Aminites images start for RoomXML
function SetHotelService_RoomXML(Services) {
    var InclImg = "";
    var j = 0, k = 0, l = 0, m = 0, n = 0,o=0, p = 0;
    var incluions = Services.split('#');
    if (incluions.length == 0)
        InclImg="&nbsp;";
    for (var i = 0; i < incluions.length; i++) {
        switch (incluions[i]) {
            case "Parking":
            case "Carparking":
                if (j == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Parking.png' title='Parking' class='IconImageSize' /><span class='hide'>Parking</span>";
                    j = 1;
                }
                 break;
            case "Roomservice":
                if (k == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/lounge.png' title='Room Service' class='IconImageSize' /><span class='hide'>Room Services</span>";
                   k = 1;
                }
                break;
            case "TeaandCoffeemaker":
                InclImg += "<img src='../Hotel/Images/Facility/breakfast.png' title='Tea/Coffee' class='IconImageSize' /><span class='hide'>Tea/Coffee</span>";
                break;
            case "Swimmingpool":
                InclImg += "<img src='../Hotel/Images/Facility/swimming.png' title='Outdoor Swimming Pool' class='IconImageSize' /><span class='hide'>Swimming Pool</span>";
                break;
            case "Sauna":
                InclImg += "<img src='../Hotel/Images/Facility/jacuzzi.png' title='Indoor Swimming Pool' class='IconImageSize' /><span class='hide'>Tub Bath</span>";
                break;
            case "ValetLaundry":
                InclImg += "<img src='../Hotel/Images/Facility/laundary.png' title='Laundry facilities' class='IconImageSize' /><span class='hide'>Laundry Services</span>";
                break;
            case "BanquetFacilities":
            case "ConferenceRoom":
            case "Businesscenter":
                if (l == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Business centre' class='IconImageSize' /><span class='hide'>Business Facilities</span>";
                    l = 1;
                }
                break;
            case "Wheelchairaccess":
                InclImg += "<img src='../Hotel/Images/Facility/handicap.png' title='Disabled facilities' class='IconImageSize' /><span class='hide'>Disabled Facilities</span>";
                break;
            case "Fitnessfacility":
                InclImg += "<img src='../Hotel/Images/Facility/health_club.png' title='Gymnasium' class='IconImageSize' /><span class='hide'>Gym</span>";
                break;
            
            case "InternetAccess":
                InclImg += "<img src='../Hotel/Images/Facility/wifi.gif' title='Internet' class='IconImageSize' /><span class='hide'>Internet/Wi-Fi</span>";
                break;
            case "RestaurantAir-Conditioned":
            case "Restaurant":
            case "Bar":
            case "Mini-bar":
                if (m == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/bar.png' title='Mini bar' class='IconImageSize' /><span class='hide'>Restaurant/Bar</span>";
                    m++;
                }
                break;
            case "Spa":
                InclImg += "<img src='../Hotel/Images/Facility/sauna.png' title='Sauna' class='IconImageSize' /><span class='hide'>Spa/Massage/Wellness/Sauna</span>";
                break;
            case "Balcony":
                InclImg += "<img src='../Hotel/Images/Facility/lobby.png' title='Lobby' class='IconImageSize' /><span class='hide'>Lobby</span>";
                break;
            case "TVinroom":
            case "SatelliteTV":
                if (n == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/TV.png' title='TV' class='IconImageSize' /><span class='hide'>TV</span>";
                    n = 1;
                }
                break;
            case "Airconditioning":
                InclImg += "<img src='../Hotel/Images/Facility/AC.png' title='AC' class='IconImageSize' /><span class='hide'>AC</span>";
                break;
                
            case "Shower":
            case "Hairdryer":
                if (o == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/bath.png' title='Hair Dryer' class='IconImageSize' /><span class='hide'>Hair Dryer</span>";
                    o = 1;
                }
                break;
            case "Lift":
                InclImg += "<img src='../Hotel/Images/Facility/elevator.png' title='Lifts' class='IconImageSize' /><span class='hide'>Lift</span>";
                break;
            case "DirectDialTelephone":
                InclImg += "<img src='../Hotel/Images/Facility/Phone.png' title='Direct dial phone' class='IconImageSize' /><span class='hide'>Phone</span>";
                break;
            case "*CR":
            case "*TA":
                if (p == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Travel agency facilities' class='IconImageSize' /><span class='hide'>Travel & Transfers</span>";
                    p = 1;
                }
                break;
            case "*BS":
                InclImg += "<img src='../Hotel/Images/Facility/babysitting.png' title='Baby' class='IconImageSize' /><span class='hide'>Baby Facilities</span>";
                break;
            case "*BP":
                InclImg += "<img src='../Hotel/Images/Facility/beauty.png' title='Beauty parlour' class='IconImageSize' /><span class='hide'>Beauty Parlour</span>";
                break;
        }
    }
    return InclImg;
}
//Set RoomXML Aminites images End
//Set GTA Aminites images Start
function SetHotelService_GTA(Services) {
    var InclImg = "";
    var j = 0, k = 0, l = 0, m = 0, n = 0, p = 0;
    var incluions = Services.split('#');
    if (incluions.length == 0)
        InclImg="&nbsp;";
    for (var i = 0; i < incluions.length; i++) {
        switch (incluions[i]) {
            case "*CP":
            case "*HP":
                if (k == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Parking.png' title='Parking' class='IconImageSize' /><span class='hide'>Parking</span>";
                    k = 1;
                }
                break;
            case "*PT":
            case "*RS":
                if (j == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/lounge.png' title='Room Service' class='IconImageSize' /><span class='hide'>Room Services</span>";
                    j = 1;
                }
                break;
            case "*DD":
                InclImg += "<img src='../Hotel/Images/Facility/Phone.png' title='Direct dial phone' class='IconImageSize' /><span class='hide'>Phone</span>";
                break;
            case "*OP":
                InclImg += "<img src='../Hotel/Images/Facility/swimming.png' title='Outdoor Swimming Pool' class='IconImageSize' /><span class='hide'>Swimming Pool</span>";
                break;
            case "*IP":
                InclImg += "<img src='../Hotel/Images/Facility/jacuzzi.png' title='Indoor Swimming Pool' class='IconImageSize' /><span class='hide'>Tub Bath</span>";
                break;
            case "*CR":
            case "*TA":
                if (n == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Travel agency facilities' class='IconImageSize' /><span class='hide'>Travel & Transfers</span>";
                    n = 1;
                }
                break;
            case "*LY":
                InclImg += "<img src='../Hotel/Images/Facility/laundary.png' title='Laundry facilities' class='IconImageSize' /><span class='hide'>Laundry Services</span>";
                break;
            //            case "*CR": 
            //                InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Car rental facilities' class='IconImageSize' />"; 
            //                break; 
            case "*BC":
                InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Business centre' class='IconImageSize' /><span class='hide'>Business Facilities</span>";
                break;
            case "*DF":
                InclImg += "<img src='../Hotel/Images/Facility/handicap.png' title='Disabled facilities' class='IconImageSize' /><span class='hide'>Disabled Facilities</span>";
                break;
            case "*GY":
                InclImg += "<img src='../Hotel/Images/Facility/health_club.png' title='Gymnasium' class='IconImageSize' /><span class='hide'>Gym</span>";
                break;
            case "*LF":
                InclImg += "<img src='../Hotel/Images/Facility/elevator.png' title='Lifts' class='IconImageSize' /><span class='hide'>Lift</span>";
                break;
            case "*IN":
                InclImg += "<img src='../Hotel/Images/Facility/wifi.gif' title='Internet' class='IconImageSize' /><span class='hide'>Internet/Wi-Fi</span>";
                break;
            case "*MB":
                InclImg += "<img src='../Hotel/Images/Facility/bar.png' title='Mini bar' class='IconImageSize' /><span class='hide'>Restaurant/Bar</span>";
                break;
            case "*BS":
                InclImg += "<img src='../Hotel/Images/Facility/babysitting.png' title='Baby' class='IconImageSize' /><span class='hide'>Baby Facilities</span>";
                break;
            case "*BP":
                InclImg += "<img src='../Hotel/Images/Facility/beauty.png' title='Beauty parlour' class='IconImageSize' /><span class='hide'>Beauty Parlour</span>";
                break;
            case "*SA":
                InclImg += "<img src='../Hotel/Images/Facility/sauna.png' title='Sauna' class='IconImageSize' /><span class='hide'>Spa/Massage/Wellness/Sauna</span>";
                break;
            case "*LS":
                InclImg += "<img src='../Hotel/Images/Facility/lobby.png' title='Lobby' class='IconImageSize' /><span class='hide'>Lobby</span>";
                break;
            //            case "*TE": 
            //                InclImg += "<img src='../Hotel/Images/Facility/tennis.png' title='Tennis' class='IconImageSize' /><span class='hide'>Tennis</span>"; 
            //                break; 
            case "*GF":
            case "*TE":
                if (p == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/golf.png' title='Golf' class='IconImageSize' /><span class='hide'>Sports</span>";
                    p = 1;
                } break;
            case "*SV":
            case "*TV":
                if (l == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/TV.png' title='TV' class='IconImageSize' /><span class='hide'>TV</span>";
                    l = 1;
                }
                break;
            case "*AC":
                InclImg += "<img src='../Hotel/Images/Facility/AC.png' title='AC' class='IconImageSize' /><span class='hide'>AC</span>";
                break;
            case "*HD":
                InclImg += "<img src='../Hotel/Images/Facility/bath.png' title='Hair Dryer' class='IconImageSize' /><span class='hide'>Hair Dryer</span>";
                break;
        }
    }
    return InclImg;
}
//Set GTA Aminites images End

//Set RezNext Aminites images Start
function SetHotelService_RZ(Services) {
    var InclImg = "";
    var j = 0, k = 0, l = 0, m = 0, n = 0, p = 0;
    var incluions = Services.split('#');
    if (incluions.length < 2)
        InclImg = "&nbsp;&nbsp;";
    for (var i = 0; i < incluions.length; i++) {
        switch (incluions[i]) {
            case "43":
                InclImg += "<img src='../Hotel/Images/Facility/Parking.png' title='Parking' class='IconImageSize' /><span class='hide'>Parking</span>";   
                break;
            case "68":
            case "64":
                if (j == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/lounge.png' title='Room Service' class='IconImageSize' /><span class='hide'>Room Services</span>";
                    j = 1;
                }
                break;
            case "65":
                InclImg += "<img src='../Hotel/Images/Facility/Phone.png' title='Direct dial phone' class='IconImageSize' /><span class='hide'>Phone</span>";
                break;
            case "*CR":
            case "2":
                if (n == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Travel agency facilities' class='IconImageSize' /><span class='hide'>Travel & Transfers</span>";
                    n = 1;
                }
                break;
            case "67":
                InclImg += "<img src='../Hotel/Images/Facility/laundary.png' title='Laundry facilities' class='IconImageSize' /><span class='hide'>Laundry Services</span>";
                break; 
            case "5":
                InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Business centre' class='IconImageSize' /><span class='hide'>Business Facilities</span>";
                break;           
            case "26":
                InclImg += "<img src='../Hotel/Images/Facility/health_club.png' title='Gymnasium' class='IconImageSize' /><span class='hide'>Gym</span>";
                break;
            case "10":
                InclImg += "<img src='../Hotel/Images/Facility/elevator.png' title='Lifts' class='IconImageSize' /><span class='hide'>Lift</span>";
                break;
            case "42":
                InclImg += "<img src='../Hotel/Images/Facility/wifi.gif' title='Internet' class='IconImageSize' /><span class='hide'>Internet/Wi-Fi</span>";
                break;
            case "4":
            case "7":
            case "17":
                if (k == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/bar.png' title='Mini bar' class='IconImageSize' /><span class='hide'>Restaurant/Bar</span>";
                    k = 1;
                }
                break;
            case "*BS":
                InclImg += "<img src='../Hotel/Images/Facility/babysitting.png' title='Baby' class='IconImageSize' /><span class='hide'>Baby Facilities</span>";
                break;
            case "*BP":
                InclImg += "<img src='../Hotel/Images/Facility/beauty.png' title='Beauty parlour' class='IconImageSize' /><span class='hide'>Beauty Parlour</span>";
                break;
            case "*SA":
                InclImg += "<img src='../Hotel/Images/Facility/sauna.png' title='Sauna' class='IconImageSize' /><span class='hide'>Spa/Massage/Wellness/Sauna</span>";
                break;
            case "14":
                InclImg += "<img src='../Hotel/Images/Facility/lobby.png' title='Lobby' class='IconImageSize' /><span class='hide'>Lobby</span>";
                break;
            case "*DF":
                InclImg += "<img src='../Hotel/Images/Facility/handicap.png' title='Disabled facilities' class='IconImageSize' /><span class='hide'>Disabled Facilities</span>";
                break;
            case "*GF":
            case "*TE":
                if (p == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/golf.png' title='Golf' class='IconImageSize' /><span class='hide'>Sports</span>";
                    p = 1;
                } break;
            case "*SV":
            case "*TV":
                if (l == 0) {
                    InclImg += "<img src='../Hotel/Images/Facility/TV.png' title='TV' class='IconImageSize' /><span class='hide'>TV</span>";
                    l = 1;
                }
                break;
            case "*AC":
                InclImg += "<img src='../Hotel/Images/Facility/AC.png' title='AC' class='IconImageSize' /><span class='hide'>AC</span>";
                break;
            case "*HD":
                InclImg += "<img src='../Hotel/Images/Facility/bath.png' title='Hair Dryer' class='IconImageSize' /><span class='hide'>Hair Dryer</span>";
                break;
            case "*OP":
                InclImg += "<img src='../Hotel/Images/Facility/swimming.png' title='Outdoor Swimming Pool' class='IconImageSize' /><span class='hide'>Swimming Pool</span>";
                break;
            case "*IP":
                InclImg += "<img src='../Hotel/Images/Facility/jacuzzi.png' title='Indoor Swimming Pool' class='IconImageSize' /><span class='hide'>Tub Bath</span>";
                break;
        }
    }
    return InclImg;
}
//Set RezNext Aminites images End

function SetTripAdvisorRating(strsrt) {
    var InclImg = "";
    try {
        var Rating = strsrt.split('#'); 
        if (strsrt != null) {
            var Ratings = parseFloat(Rating[0]); 
            if (Ratings == 1)
                InclImg = "<span class='lft' title='Trip Advisor Rating 1 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_1.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings > 1 && Ratings < 2)
                InclImg = "<span class='lft' title='Trip Advisor Rating 1.5 out of 5'> <img src='../Hotel/Images/TripAdvisor/TA_1.5.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings == 2)
                InclImg = "<span class='lft' title='Trip Advisor Rating 2 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_2.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings > 2 && Ratings < 3)
                InclImg = "<span class='lft' title='Trip Advisor Rating 2.5 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_2.5.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings == 3)
                InclImg = "<span class='lft' title='Trip Advisor Rating 3 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_3.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings > 3 && Ratings < 4)
                InclImg = "<span class='lft' title='Trip Advisor Rating 3.5 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_3.5.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings == 4)
                InclImg = "<span class='lft' title='Trip Advisor Rating 4 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_4.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings > 4 && Ratings < 5)
                InclImg = "<span class='lft' title='Trip Advisor Rating 4.5 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_4.5.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
            else if (Ratings == 5)
                InclImg = "<span class='lft' title='Trip Advisor Rating 5 out of 5'><img src='../Hotel/Images/TripAdvisor/TA_5.gif' class='hgt13'/>&nbsp; </span><span class='rgt f10'>" + Rating[1] + " Reviews</span>";
        }
    } catch (ex) { }
    return InclImg;
}


function SetDailyWisePrice(week, day, strDayPrice) {
    if (week == 1) {
        switch (day) {
            case 0:
                $("#SUNDAY").html(strDayPrice);
                $("#SUNDAY").addClass("bgwight");
                break;
            case 1:
                $("#MONDAY").html(strDayPrice);
                $("#MONDAY").addClass("bgwight");
                break;
            case 2:
                $("#TUESDAY").html(strDayPrice);
                $("#TUESDAY").addClass("bgwight");
                break;
            case 3:
                $("#WEDNESDAY").html(strDayPrice);
                $("#WEDNESDAY").addClass("bgwight");
                break;
            case 4:
                $("#THURSDAY").html(strDayPrice);
                $("#THURSDAY").addClass("bgwight");
                break;
            case 5:
                $("#FRIDAY").html(strDayPrice);
                $("#FRIDAY").addClass("bgwight");
                break;
            case 6:
                $("#SATURDAY").html(strDayPrice);
                $("#SATURDAY").addClass("bgwight");
                week = week + 1;
                $("#weeks2").show();
                day = 7;
                break;
            case 7:
                break;
        }
    }
    if (week == 2) {
        switch (day) {
            case 0:
                $('#SUNDAY2').html(strDayPrice);
                $('#SUNDAY2').addClass("bgwight");
                break;
            case 1:
                $('#MONDAY2').html(strDayPrice);
                $('#MONDAY2').addClass("bgwight");
                break;
            case 2:
                $('#TUESDAY2').html(strDayPrice);
                $('#TUESDAY2').addClass("bgwight");
                break;
            case 3:
                $('#WEDNESDAY2').html(strDayPrice);
                $('#WEDNESDAY2').addClass("bgwight");
                break;
            case 4:

                $('#THURSDAY2').html(strDayPrice);
                $('#THURSDAY2').addClass("bgwight");
                break;
            case 5:
                $('#FRIDAY2').html(strDayPrice);
                $('#FRIDAY2').addClass("bgwight");
                break;
            case 6:
                $('#SATURDAY2').html(strDayPrice);
                $('#SATURDAY2').addClass("bgwight");
                week = week + 1;
                $("#weeks3").show();
                day = 7; ; break;
            case 7:
                break;
        }
    }
    if (week == 3) {
        switch (day) {
            case 0:
                $('#SUNDAY3').html(strDayPrice);
                $('#SUNDAY3').addClass("bgwight");
                break;
            case 1:
                $('#MONDAY3').html(strDayPrice);
                $('#MONDAY3').addClass("bgwight");
                break;
            case 2:
                $('#TUESDAY3').html(strDayPrice);
                $('#TUESDAY3').addClass("bgwight");
                break;
            case 3:
                $('#WEDNESDAY3').html(strDayPrice);
                $('#WEDNESDAY3').addClass("bgwight");
                break;
            case 4:
                $('#THURSDAY3').html(strDayPrice);
                $('#THURSDAY3').addClass("bgwight");
                break;
            case 5:
                $('#FRIDAY3').html(strDayPrice);
                $('#FRIDAY3').addClass("bgwight");
                break;
            case 6:
                $('#SATURDAY3').html(strDayPrice);
                $('#SATURDAY3').addClass("bgwight");
                break;
            case 7:
                break;
        }
    }
    return week;
}


function SetHotelService_EX(HService) {
    var InclImg = "";
    var swim = 0, p=0,s=0;
    if (HService != null) {
        var HServ = HService.split('#');
        if (HServ.length < 2)
        { InclImg = "&nbsp;&nbsp;";}
        for (i = 0; i < HServ.length; i++) {
            if (HServ[i] == "16" || HServ[i] == "32" || HServ[i] == "128" || HServ[i] == "65536" || HServ[i] == "131072" || HServ[i] == "1048576" || HServ[i] == "2097152" || HServ[i] == "4194304" || HServ[i] == "67108864")
               InclImg += "&nbsp;";
               else{
                switch (HServ[i]) {
                    case "1":
                        InclImg += "<img src='../Hotel/Images/Facility/Banquet_hall.png' title='Business centre' class='IconImageSize' /><span class='hide'>Business Facilities</span>";
                        break;
                    case "2":
                        { InclImg += "<img src='../Hotel/Images/Facility/health_club.png' title='Gym' class='IconImageSize' /><span class='hide'>Gym</span>"; }
                        break;
                    case "8":
                        { InclImg += "<img src='../Hotel/Images/Facility/wifi.gif' title='Internet/Wi-Fi' class='IconImageSize' /><span class='hide'>Internet/Wi-Fi</span>"; }
                        break;
                    case "64":
                        { InclImg += "<img src='../Hotel/Images/Facility/pets.png' title='Pets Allowed' class='IconImageSize' /><span class='hide'>Pets Allowed</span>"; }
                        break;
                    case "256":
                        { InclImg += "<img src='../Hotel/Images/Facility/bar.png' title='Restaurant/Bar' class='IconImageSize' /><span class='hide'>Restaurant/Bar</span>"; }
                        break;
                    case "512":
                        { InclImg += "<img src='../Hotel/Images/Facility/sauna.png' title='Spa/Massage/Wellness/Sauna' class='IconImageSize' /><span class='hide'>Spa/Massage/Wellness/Sauna</span>"; }
                        break;
                    case "2048":
                        { InclImg += "<img src='../Hotel/Images/Facility/breakfast.png' title='Breakfast' class='IconImageSize' /><span class='hide'>Breakfast</span>"; }
                        break;
                    case "4096":
                        { InclImg += "<img src='../Hotel/Images/Facility/Babysitting.png' title='Baby Facilities' class='IconImageSize' /><span class='hide'>Baby Facilities</span>"; }
                        break;
                    case "8192":
                        { InclImg += "<img src='../Hotel/Images/Facility/jacuzzi.png' title='Jacuzzi' class='IconImageSize' /><span class='hide'>Jacuzzi</span>"; }
                        break;
                    case "32768":
                        InclImg += "<img src='../Hotel/Images/Facility/lounge.png' title='Room Service' class='IconImageSize' /><span class='hide'>Room Services</span>";
                        break;
                    case "262144":
                    case "1024":
                    case "4":
                        if (s == 0) {
                            InclImg += "<img src='../Hotel/Images/Facility/bath.png' title='Shower' class='IconImageSize' /><span class='hide'>Shower</span>"; s++;
                        }
                        break;
                    case "524288":
                        { InclImg += "<img src='../Hotel/Images/Facility/handicap.png' title='Disabled Facilities' class='IconImageSize' /><span class='hide'>Disabled Facilities</span>"; }
                        break;
                    case "8388608":
                        { InclImg += "<img src='../Hotel/Images/Facility/Airport_transfer.png' title='Travel & Transfers' class='IconImageSize' /><span class='hide'>Travel & Transfers</span>"; }
                        break;
                    case "16777216":
                    case "33554432":
                        if (swim == 0) {
                            InclImg += "<img src='../Hotel/Images/Facility/swimming.png' title='Swimming Pool' class='IconImageSize' /><span class='hide'>Swimming Pool</span>";
                            swim++;
                        }
                        break;
                    case "16384":
                    case "134217728":
                        if (p == 0)
                        { InclImg += "<img src='../Hotel/Images/Facility/parking.png' title='Parking' class='IconImageSize' /><span class='hide'>Parking</span>"; p++; }
                        break;
                }
            }
        }
    }
    else
    { InclImg = "&nbsp;&nbsp;"; }

    return InclImg;
}

function SetHotelService_TG(Hotelcode) {
    var InclImg = "";
    try {
        $.ajax({
            url: UrlBase + "Hotel/HotelSearchs.asmx/Get_TG_HotelServices",
            contentType: 'application/json; charset=utf-8',
            type: 'POST', dataType: 'json',
            data: "{'HotelCode': '" + Hotelcode + "','AmenityType': 'property'}",
            success: function (data) {
                if (data.d != "") {


                }
            }
        });

    }
    catch (err) { }
    return InclImg;
}





