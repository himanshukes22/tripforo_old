var SeatMapHandler;
var seatslArray = new Array();
var seatslArray_ib = new Array();
var popupStatus = 0; // set value
var ft = "";
var seatArr;
$(document).ready(function () {
    SeatMapHandler = new SeatMapHelper();
    SeatMapHandler.BindEvents();

  
});
var SeatMapHelper = function () {
    this.orderid = $("#OBTrackIds");
    this.airline = $("#OBValidatingCarrier");
    this.btnaddseat = $("#btnaddseat");
    this.btnaddseat_ib = $("#btnaddseat_ib");
    this.clsseat = $(".clsseat");
    this.changeseat = $("#btnchangeseat");
    this.orderid_ib = $("#IBTrackIds");
    this.airline_ib = $("#IBValidatingCarrier");
    //this.aclass = $(".clsseat");
}


var seatSelect = function (paxid, sector, seat, amount, htmlid, seatalignment, title, fname, lname, tpcode, flightnumber, flighttime,
    optionalserviceref, group, classofservice, equipment, carrier, paid
    ) {
    this.Pax = paxid;
    this.Sector = sector;
    this.Seat = seat;
    this.Amount = amount;
    this.HtmlID = htmlid;
    this.SeatAlignment = seatalignment;
    this.FName = fname;
    this.LName = lname;
    this.Title = title;
    this.Tpcode = tpcode;
    this.FlightNumber = flightnumber;
    this.FlightTime = flighttime;

    this.OptionalServiceRef = optionalserviceref;
    this.Group = group;
    this.ClassOfService = classofservice;
    this.Equipment = equipment;
    this.Carrier = carrier;
    this.Paid = paid;
}
SeatMapHelper.prototype.BindEvents = function () {
    var h = this;
    //var seatslArray = new Array();
    if (!('contains' in String.prototype)) {
        String.prototype.contains = function (str, startIndex) {
            return ''.indexOf.call(this, str, startIndex) !== -1;
        };
    }
  
    $("#SeatSource").delegate(".clspaxdtls", "click", function () {

        $("#hdn_selected").val('');
        $(".clspaxdtls").removeClass("paxselected");
        $(this).addClass("paxselected");
        $("#hdn_selected").val($(this).attr('Traveller'));
        $("#hdn_selected_fname").val($(this).attr('FName'));
        $("#hdn_selected_lname").val($(this).attr('LName'));
        $("#hdn_selected_title").val($(this).attr('Title'));
        $("#hdn_selected_tpcode").val($(this).attr('Tpcode'));
        $("#selectedtrvl").html($(this).attr('FName'));
    });
    $("#SeatSource").delegate(".clsorgdest", "click", function () {
        $(".clsorgdest").removeClass("fltselected");
        $(this).addClass("fltselected");
        $(".seatlayout").hide();
        var fltid = $(this).attr('origin') + '-' + $(this).attr('destination');
        $('#' + fltid).show();
        var selectedpax;
        if (ft == "outbound") {
            selectedpax = JSLINQ(seatslArray)
                       .Where(function (item) { return item.Sector == fltid })
                       .Select(function (item) { return item });
        }
        else {
            selectedpax = JSLINQ(seatslArray_ib)
                               .Where(function (item) { return item.Sector == fltid })
                               .Select(function (item) { return item });
        }
        $(".spntrvl").empty();

        if (selectedpax.items.length > 0) {

            for (var i = 0; i < selectedpax.items.length; i++) {

                $('#spn' + selectedpax.items[i].Pax).html('Seat: ' + selectedpax.items[i].Seat + '(' + selectedpax.items[i].SeatAlignment + ') Amount: ' + selectedpax.items[i].Amount);
            }

        }

    });
    $("#SeatSource").delegate(".clsseat", "click", function () {
        var seatstatus = $(this).attr('seatstatus');
        if ($(this)[0].className.indexOf("SELECTED") >= 0 || seatstatus == "OCCUPIED" || seatstatus == "BLANK") {
        }
        else {
            var Traveller = $("#hdn_selected").val();
            var FName = $("#hdn_selected_fname").val();
            var LName = $("#hdn_selected_lname").val();
            var Title = $("#hdn_selected_title").val();
            var Tpcode = $("#hdn_selected_tpcode").val();
            var selectcls = 'SELECTED';
            $(".clsseat").removeClass(selectcls);
            $(".clsseat").attr('original-title', 'Pick This');
            var seatname = $(this).attr('seatname');
            var sectorname = $(this).attr('sectorname');
            var seatfee = $(this).attr('seatfee');
            var seatalignment = $(this).attr('seatalignment');
            var FlightNumber = $(this).attr('FlightNumber');
            var FlightTime = $(this).attr('FlightTime');


            var ref = "", group = "", carrier = "", classofservice = "", equipment = "", paid = "";;

            ref = $(this).attr('ref');
            group = $(this).attr('group');
            carrier = $(this).attr('carrier');
            classofservice = $(this).attr('classofservice');
            equipment = $(this).attr('equipment');
            paid = $(this).attr('paid');


            var selectedpax;
            if (ft == "outbound") {
                selectedpax = JSLINQ(seatslArray)
                            .Where(function (item) { return item.Pax == Traveller && item.Sector == sectorname })
                            .Select(function (item) { return item });
            }
            else {
                selectedpax = JSLINQ(seatslArray_ib)
                             .Where(function (item) { return item.Pax == Traveller && item.Sector == sectorname })
                             .Select(function (item) { return item });
            }
            if (selectedpax.items.length > 0) {
                selectedpax.items[0].Seat = seatname;
                selectedpax.items[0].Amount = seatfee;
                selectedpax.items[0].SeatAlignment = seatalignment;
                selectedpax.items[0].HtmlID = $(this).attr('id');

                selectedpax.items[0].OptionalServiceRef = ref;
                selectedpax.items[0].Group = group;
                selectedpax.items[0].Carrier = carrier;
                selectedpax.items[0].ClassOfService = classofservice;
                selectedpax.items[0].Equipment = equipment;
                selectedpax.items[0].Paid = paid;
            }
            else {

                var sl = new seatSelect(Traveller, sectorname, seatname, seatfee, $(this).attr('id'), seatalignment, Title, FName, LName, Tpcode, FlightNumber, FlightTime, ref, group, classofservice, equipment, carrier, paid);
                if (ft == "outbound")
                    seatslArray.push(sl);
                else
                    seatslArray_ib.push(sl);
            }

            if (ft == "outbound") {
                var total = 0;
                if (seatslArray.length > 0) {
                    for (var i = 0; i < seatslArray.length; i++) {
                        var fullname = "";
                        fullname = Title + " " + FName + "" + LName;
                      
                        $('#spn' + Traveller).html('<br/>Seat: ' + seatname + '(' + seatalignment + ') Amount: ' + seatslArray[i].Amount);
                        $('#' + seatslArray[i].HtmlID).addClass(selectcls);

                        total = parseInt(total) + parseInt(seatslArray[i].Amount);
                        $('#spnamount').removeClass("hide");
                        $('#spnamount').show();
                        $('#spnamount').html('Seat total amount :   ' + total);
                    }

                }

            }
            else {
                var total_ib = 0;
                if (seatslArray_ib.length > 0) {
                    for (var i = 0; i < seatslArray_ib.length; i++) {
                        var fullname = "";
                        fullname = Title + " " + FName + "" + LName;
                        $('#spn' + Traveller).html('<br/>Seat: '+seatname + '(' + seatalignment + ') Amount: ' + seatslArray[i].Amount);
                        $('#' + seatslArray_ib[i].HtmlID).addClass(selectcls);
                        total_ib = parseInt(total_ib) + parseInt(seatslArray_ib[i].Amount);
                        $('#spnamount_ib').removeClass("hide");
                        $('#spnamount_ib').show();
                        $('#spnamount_ib').html('Seat total amount :   ' + total_ib);
                    }
                }
            }

            $(this).attr('original-title', seatname + sectorname + Traveller);
        }

    });
    $("#SeatSource").delegate("#btn_confirm", "click", function () {
        if (seatslArray.length > 0 || seatslArray_ib.length > 0) {
            if (ft == "outbound") {
                $("#SeatDetails").empty();
                $("#ctl00_ContentPlaceHolder1_seatSelect").val('');
                if (seatslArray.length > 0) {
                    var seatdetails = "";
                    seatdetails += '<div class="w100 lft">'
                    seatdetails += '<div class="w30 lft bld">Traveller(OutBound)</div>'
                    seatdetails += '<div class="w20 lft bld">Sector</div>'
                    seatdetails += '<div class="w10 lft bld">Seat</div>'
                    seatdetails += '<div class="w10 lft bld">Type</div>'
                    seatdetails += '<div class="w10 lft bld">Amount</div>'
                    seatdetails += '<div class="clear"></div>';
                    for (var i = 0; i < seatslArray.length; i++) {
                        seatdetails += '<div class="w30 lft">' + seatslArray[i].Title + ' ' + seatslArray[i].FName+' ' + seatslArray[i].LName + '</div>'
                        seatdetails += '<div class="w20 lft">' + seatslArray[i].Sector + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray[i].Seat + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray[i].SeatAlignment + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray[i].Amount + '</div>'
                        seatdetails += '<div class="clear"></div>';
                    }
                }
                else {

                }
                seatdetails += '</div>';
                disablePopup();
                var json = JSON.stringify(seatslArray);
                $("#ctl00_ContentPlaceHolder1_seatSelect").val(json);
                $("#SeatDetails").append(seatdetails);


            }
            else {
                $("#ctl00_ContentPlaceHolder1_SeatDetails_ibDtls").empty();
                $("#ctl00_ContentPlaceHolder1_seatSelect_ib").val('');
                if (seatslArray_ib.length > 0) {
                    var seatdetails = "";
                    seatdetails += '<div class="w100 lft">'
                    seatdetails += '<div class="w30 lft bld">Traveller(InBound)</div>'
                    seatdetails += '<div class="w20 lft bld">Sector</div>'
                    seatdetails += '<div class="w10 lft bld">Seat</div>'
                    seatdetails += '<div class="w10 lft bld">Type</div>'
                    seatdetails += '<div class="w10 lft bld">Amount</div>'
                    seatdetails += '<div class="clear"></div>';
                    for (var i = 0; i < seatslArray_ib.length; i++) {
                        seatdetails += '<div class="w30 lft">' + seatslArray_ib[i].Title +' '+ seatslArray_ib[i].FName + ' '+ seatslArray_ib[i].LName +  '</div>'
                        seatdetails += '<div class="w20 lft">' + seatslArray_ib[i].Sector + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray_ib[i].Seat + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray_ib[i].SeatAlignment + '</div>'
                        seatdetails += '<div class="w10 lft">' + seatslArray_ib[i].Amount + '</div>'
                        seatdetails += '<div class="clear"></div>';
                    }
                }
                else {

                }
                seatdetails += '</div>';
                disablePopup();
                var json = JSON.stringify(seatslArray_ib);
                $("#ctl00_ContentPlaceHolder1_SeatDetails_ibDtls").val(json);
                $("#ctl00_ContentPlaceHolder1_SeatDetails_ib").append(seatdetails);
            }
        }

    });
    function BlockUIFC() {
        $.blockUI({
            message: $("#waitMessagefc")
        });
    }
    //function SeatLayout(d, seatslArray) {
    //    for (var l = 0; l < d.PaxListDetails.length; l++) {
    //        d.PaxListDetails[l].FName = d.PaxListDetails[l].FName == "" ? ($.trim($("#PaxListDetails_" + l + "__FName").val()) == "" ? ("Pax " + (l + 1)) : $.trim($("#PaxListDetails_" + l + "__FName").val())) : d.PaxListDetails[l].FName;
    //    }
    //    var res = "";
    //    try {

    //        $("#SeatSource").empty();
    //        if (d.Error != "" && d.Error == null && d.SeatMapAll[0].SeatMapDetails_Final[0].Error != "" && d.SeatMapAll[0].SeatMapDetails_Final[0].Error == null) {
    //            res += '<div class="w40 lft textheads">Select Your Seats </div> '
    //            res += '<div class="w70 rgt" style="height: 22px;padding-top: 11px;" >'

    //            res += '<div class="w100 rgt">'
    //            for (var sec = 0; sec < d.SeatMapAll.length; sec++) {
    //                res += '<div class="w15 lft">'
    //                res += '<div class="w100 lft">'
    //                if (sec == 0) {
    //                    res += '<a class="clsorgdest fltselected" id="sector' + sec + '"  origin="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + '"  destination="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + '">' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + ' - ' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + ' </a>';
    //                }
    //                else {
    //                    res += '<a class="clsorgdest" id="sector' + sec + '"   origin="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + '"  destination="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + '">' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + ' - ' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + ' </a>';
    //                }
    //                res += '</div>'
    //                res += '</div>'
    //            }
    //            res += '</div>'
    //            res += '</div>'
    //            res += '<div class="rgt vListContainer"  style="border-top: 1px solid #e9e9e9; width:100%;  ">';
    //            res += '<div class="w20 lft" style="border-right: 1px solid #e9e9e9;height:500px;">';
    //            for (var pax = 0; pax < d.PaxListDetails.length ; pax++) {
    //                res += '<div class="w100 lftgreen lft"  >';
    //                if (pax == 0) {
    //                    res += '<div class="clspaxdtls paxselected" id="Traveller' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d.PaxListDetails[pax].FName + '" Lname="' + d.PaxListDetails[pax].LName + '"   Title="' + d.PaxListDetails[pax].Title + '" Tpcode="' + d.PaxListDetails[pax].ProfileCode + '">' + d.PaxListDetails[pax].Title + ' ' + d.PaxListDetails[pax].FName + ' ' + d.PaxListDetails[pax].LName + ' <span class="spntrvl" style="color:#000;font-size:9px;" id="spnTraveller' + pax + '"></div>';
    //                    res += '<input type="hidden" id="hdn_selected" value="Traveller' + pax + '"/>'
    //                }

    //                else
    //                    res += '<div class="clspaxdtls" id="trvl' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d.PaxListDetails[pax].FName + '" Lname="' + d.PaxListDetails[pax].LName + '"   Title="' + d.PaxListDetails[pax].Title + '" Tpcode="' + d.PaxListDetails[pax].ProfileCode + '">' + d.PaxListDetails[pax].Title + ' ' + d.PaxListDetails[pax].FName + ' ' + d.PaxListDetails[pax].LName + '  <span class="spntrvl" style="color:#000;font-size:9px;" id="spnTraveller' + pax + '"></span></div>';
    //                res += '<input type="hidden" id="hdn_selected_title" value="' + d.PaxListDetails[pax].Title + '"/>'
    //                res += '<input type="hidden" id="hdn_selected_fname" value="' + d.PaxListDetails[pax].FName + '"/>'
    //                res += '<input type="hidden" id="hdn_selected_lname" value="' + d.PaxListDetails[pax].LName + '"/>'
    //                res += '<input type="hidden" id="hdn_selected_tpcode" value="' + d.PaxListDetails[pax].ProfileCode + '"/>'
    //                res += '</div>';
    //                res += '<div class="clear"></div>';
    //            }
    //            res += '</div>';
    //            res += '<div class="w75 lft p10">';
    //            res += '<div class="w100 lft texthead ">Seat selection for <span id="selectedtrvl"></span> </div>'
    //            res += '<div class="clear"></div>';
    //            var tot = 0;
    //            var blank = 1;
    //            for (var is = 0; is < d.SeatMapAll.length; is++) {
    //                res += '<div style="height: 297px; width: 100%; overflow-x: scroll; overflow-y: hidden;display:none" class="seatlayout" id="' + d.SeatMapAll[is].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[is].SeatMapDetails_Final[0].ArrivalStation + '">';
    //                res += '<div class="w20 lft flightSeatMap" style="height: 256px; " >' + ' <img class="front" src="' + UrlBase + 'images/Seat/front.png"  />' + '</div>';
    //                res += '<div class="w80 lft" >';
    //                res += '<table width="100%" style="height: 230px; width: auto; overflow-x: scroll;border-top: 2px solid #fff;border-bottom: 2px solid #fff; overflow-y: hidden;" id="seat_div">';
    //                res += '<tr>';
    //                var fl = 0;
    //                for (var ir = 0; ir < d.SeatMapAll[is].Rows; ir++) {
    //                    res += '<td>';
    //                    res += '<table width="100%">';
    //                    //if (d.Provider == "GDS") {
    //                    //    if (fl == 0) {
    //                    //        for (var ap = 0; ap < d.SeatMapAll[is].Alpha.length; ap++) {

    //                    //            res += '<tr>';
    //                    //            res += '<td style="padding:1px;padding-bottom:5px;text-align: center;vertical-align: top; color:#ccc;font-size: 12px; ">';
    //                    //            res += '<a class="clsseat">' + d.SeatMapAll[is].Alpha[ap].Value + '</a>';
    //                    //            res += '</td>';
    //                    //            res += ' </tr>';


    //                    //        }
    //                    //    }
    //                    //}
    //                    for (var ic = 0; ic < d.SeatMapAll[is].Columns; ic++) {
    //                        var ExitSeats = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].ExitSeats;
    //                        var Message = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Message;//GDS
    //                        var seatrow = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].RowNo;//GDS
    //                        if (ic == 0 && d.Provider == "LCC") {
    //                            if (ExitSeats == "EXIT")
    //                                res += '<tr><td style="padding:1px"><div>  <img src="' + UrlBase + 'images/Seat/exit.png" /> </div></td></tr>';
    //                            else
    //                                res += '<tr><td style="padding:1px">&nbsp;</td></tr>';
    //                        }
    //                        if (Message != "hide") {
    //                            res += '<tr>';
    //                            res += '<td style="padding:1px;padding-bottom:5px;text-align: center;vertical-align: top; color:#ccc;font-size: 12px; ">';
    //                            var seatname = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator;
    //                            var sectorname = d.SeatMapAll[is].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[is].SeatMapDetails_Final[0].ArrivalStation;
    //                            var seatstatus = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatStatus;
    //                            var seatfee = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatFee;
    //                            var SeatAlignment = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatAlignment;
    //                            var FlightNumber = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].FlightNumber;
    //                            var FlightTime = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].FlightTime;
    //                            var id = sectorname + ir.toString() + ic.toString();

    //                            var ref = "", group = "", carrier = "", classofservice = "", equipment = "", paid = "false";
    //                            if (d.Provider == "GDS") {
    //                                ref = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SegmentRef;
    //                                group = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Group;
    //                                carrier = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Carrier;
    //                                classofservice = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].ClassOfService;
    //                                equipment = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Equipment;
    //                                paid = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Paid;
    //                            }


    //                            if (seatstatus == "BLANK") {//seatstatus == "BLANK" || seatstatus == "BLANK"Message == "WAY"
    //                                if (d.Provider == "LCC") {
    //                                    res += '<a class="clsseat" id="' + id + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  title="' + d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator + '">' + blank + '</a>';
    //                                    blank = blank + 1;
    //                                }
    //                                else {
    //                                    res += '<a class="clsseat" id="' + id + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  title="' + d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator + '">' + seatrow + '</a>';
    //                                    blank = blank + 1;
    //                                }
    //                            }
    //                            else {
    //                                if (seatstatus == "NoSeat") {
    //                                    res += '<a class="" title="OCCUPIED" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '" sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '">&nbsp;</a>';//<img src="' + UrlBase + 'images/Seat/seat_occupied.png" />
    //                                }
    //                                else if (seatstatus == "OCCUPIED") {
    //                                    res += '<a class="clsseat OCCUPIED" title="OCCUPIED" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '" sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_occupied.png" />
    //                                }
    //                                else {
    //                                    if (d.Provider == "LCC")
    //                                        res += '<a class="clsseat PRICE_' + seatfee + '" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />
    //                                    else {
    //                                        if (paid == true)

    //                                            res += '<a class="clsseat PRICE_PAID" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />
    //                                        else
    //                                            res += '<a class="clsseat PRICE_' + seatfee + '" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />

    //                                    }
    //                                }
    //                            }
    //                            res += '</td>';
    //                            res += ' </tr>';

    //                            if (ic == d.Columns - 1 && d.Provider == "LCC") {
    //                                if (ExitSeats == "EXIT")
    //                                    res += '<tr><td style="padding:1px"><div>  <img src="' + UrlBase + 'images/Seat/Exit1.png" /> </div></td></tr>';
    //                                else
    //                                    res += '<tr><td style="padding:1px">&nbsp;</td></tr>';
    //                            }
    //                        }

    //                        tot = tot + 1;
    //                    }
    //                    res += '</table>';
    //                    res += '</td>';
    //                    fl = fl + 1;
    //                }
    //                //}
    //                res += '<td style="float:right;height: 210px;padding: -3px;width: 60px;" >' + ' <img class="rear" src="' + UrlBase + 'images/Seat/rear.png" />' + '</td>';
    //                res += ' </tr>';
    //                res += '</table>';
    //                res += '</div>';
    //                res += '</div>';
    //                blank = 1;
    //                tot = 0;
    //            }
    //            res += '</div>';
    //            try {
    //                var SeatFareArray = JSLINQ(d.SeatMapAll[0].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails)
    //              .Select(function (item) { return item });
    //                var UniqueArray = (JSLINQ(SeatFareArray.items).Select(function (item) { return $.trim(item.SeatFee) }).OrderByDescending(function (item) { return $.trim(item.SeatFee) })).items.unique();
    //                var SortedArray2 = UniqueArray.sort(sortNumber);
    //                var SortedArray = UniqueArray.sort(sortNumber);
    //                var prcstring = "";
    //                if (d.Provider == 'GDS') {
    //                    prcstring = prcstring + "<li> <li> <a class='PRICE_0'></a>Free Seat</li></li>";//<a class='PRICE_PAID'></a>Paid Seat
    //                }
    //                else {
    //                    for (var i = 0; i < SortedArray.length; i++) {
    //                        prcstring = prcstring + "<li> <a class='PRICE_" + SortedArray[i] + "'></a>Rs " + SortedArray[i] + "</li>";
    //                    }
    //                }
    //                if (d.Provider == 'GDS'){
    //                    res += '<div class="w75 rgt"><ul class="pricetbs"> ' + prcstring + ' <li> <a class="OCCUPIED"></a>Occupied</li><li> <a class="SELECTED"></a>Selected</li> <li> <a class=""></a></li>   </ul> </div>'
    //                }
    //                else {
    //                    res += '<div class="w75 rgt"><ul class="pricetbs"> ' + prcstring + ' <li> <a class="OCCUPIED"></a>Occupied</li><li> <a class="SELECTED"></a>Selected</li> <li> <a class=""></a><img src="' + UrlBase + 'images/Seat/Exitb.png"  />   Exit</li>   </ul> </div>'
    //                }

    //            }
    //            catch (er)
    //            {
                    
    //            }
    //            res += '<div class="w100">'

    //            res += '<div class="w30 lft" style="padding-left:20px;">'
    //            res += '<span class="hide" id="spnamount"></span> <span class="hide" id="spnamount_ib"></span>';
    //            res += '</div>'

    //            res += '<div class="w45 rgt" style="padding-right:20px;">'
    //            res += '<input type="button" class="btn_close rgt " id="btn_confirm" value="Continue"/>'
    //            res += '</div>'
    //            res += '</div>';

    //            res += '</div>';
    //            res += '';
    //            $("#SeatSource").append(res);
    //            $('#' + d.SeatMapAll[0].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[0].SeatMapDetails_Final[0].ArrivalStation).show();
    //            var Name = d.PaxListDetails[0].Title + ' ' + d.PaxListDetails[0].FName + ' ' + d.PaxListDetails[0].LName;
    //            $("#selectedtrvl").html(Name);
    //            $.unblockUI();
    //        }
    //        else {
    //            $("#SeatSource").empty();
    //            res += '<div class="w60 lft text-center" style="width:100%; text-align:center; padding-top:40px " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
    //            res += '<div class="w60 lft texthe" style="width:100%; text-align:center; padding-top:70px">Sorry! Seat map is not available right now.</div> '
    //            $("#SeatSource").append(res);
    //            $.unblockUI();
    //        }
    //    }
    //    catch (er) {
    //        $("#SeatSource").empty();
    //        res += '<div class="w60 lft text-center" style="width:100%; text-align:center;padding-top:40px " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
    //        res += '<div class="w60 lft texthe" style="width:100%; text-align:center; padding-top:70px ">Sorry! Seat map is not available right now..</div> '
    //        $("#SeatSource").append(res);
    //        $.unblockUI();
    //    }
    //}
  
    function disablePopup() {
        if (popupStatus == 1) { // if value is 1, close popup
            $("#toPopup").fadeOut("normal");
            $("#toPopup1").hide();
            $("#backgroundPopup").fadeOut("normal");
            popupStatus = 0;  // and set value to 0
        }
    }
   
}

jQuery(function ($) {

    $("body").on("click", "a.topopup", function () {
  
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup(); // function show popup 
            $("#SeatSource").empty();
            $("#SeatDetails").empty();
            seatslArray = new Array();

        }, 500); // .5 second
        return false;
    });
    $("body").on("click", "a.topopup_ib", function () {
        ////////debugger;
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup(); // function show popup 
            $("#SeatSource").empty();
            $("#ctl00_ContentPlaceHolder1_SeatDetails_ib").empty();
            seatslArray_ib = new Array();

        }, 500); // .5 second
        return false;
    });
    $("body").on("click", "#btnchangeseat", function (e) {
        $("#toPopup").fadeIn(0500); // fadein popup div
        $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
        $("#backgroundPopup").fadeIn(0001);
    });
    $("body").on("click", "#btnaddseat", function (e) {
        // h.btnaddseat.on("click", function () {
       
        var PaxInfolist = GetPaxInfoList(GetPaxInfo());
        BlockUIFC();
        if (PaxInfolist.length != 0)
        {
            $("#toPopup1").show();
            
            $("#toPopup").removeClass("hidepo");
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/ViewSeatDetails",
                //url: UrlBase + "/Flight/ViewSeatDetails",
                //////data: "{'txtPCCImpPnr':'" + $.trim(h.txtPCCImpPnr.val()) + "','txtPNRImpPnr':'" + $.trim(h.txtPNRImpPnr.val()) + "','txtQueueNoImpPnr':'" + $.trim(h.txtQueueNoImpPnr.val()) + "','sltCopIDImpPnr':'" + $.trim(h.sltCopIDImpPnr.val()) + "','sltTripImpPnr':'" + $.trim(h.sltTripImpPnr.val()) + "','sltTripTypeImpPnr':'" + $.trim(h.sltTripTypeImpPnr.val()) + "'}",
                data: "{'Airline':'" + $.trim($("#ctl00_ContentPlaceHolder1_OBValidatingCarrier").val()) + "','OrderId':'" + $.trim($("#ctl00_ContentPlaceHolder1_OBTrackIds").val()) + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    var Error = "";
                    var PaxD = JSON.parse(data.d);
                    ft = "outbound";
                    //$("#SeatSource").empty();
                    //if (d.Error != "" && d.SeatMapAll[0].Error !="")
                    //    Error = d.SeatMapAll.Error;
                    //else

                    if (PaxD.PaxListDetails != null) {
                        //#region MultiPax
                        debugger;
                        //  var o = $.parseJSON($("#hdnPaxListforSeat").val());
                        var PaxInfolist = GetPaxInfoList(GetPaxInfo());
                        var PaxListDetails = JSLINQ(PaxD.PaxListDetails)
                                        .Where(function (item) { return item.PaxType != "INF"; })
                                        .Select(function (item) { return item; });
                        PaxD.PaxListDetails = PaxListDetails.items;
                        //#endregion
                        SeatLayout(PaxD, seatslArray, PaxInfolist);
                    }
                    else if (1 == 1) {
                        seatslArray = new Array();
                        var o = $.parseJSON($("#ctl00_ContentPlaceHolder1_hdnPaxListforSeat").val());
                        var PaxListDetails = JSLINQ(o)
                                        .Where(function (item) { return item.PaxType != "INF"; })
                                        .Select(function (item) { return item; });
                        PaxD.PaxListDetails = PaxListDetails.items;
                        SeatLayout(PaxD, seatslArray);
                    }
                    else {
                        alert("Seat map is not available.");
                        $.unblockUI();
                        $("#toPopup").addClass("hidepo");
                        $("#toPopup1").hide();
                    }


                },
                error: function (e, t, n) {
                    alert("Seat map not available please try again later");
                    $.unblockUI();
                    $("#toPopup").addClass("hidepo");
                    $("#toPopup1").hide();
                }
            });
        }
        else {
            alert("Please fill all passenger informaton");          
            $.unblockUI();
            $("#toPopup").addClass("hidepo");
            $("#toPopup1").hide();
                 }
       

    });
    $("body").on("click", "#btnaddseat_ib", function (e) {
       
        var PaxInfolist = GetPaxInfoList(GetPaxInfo());
        BlockUIFC();
        if (PaxInfolist.length != 0) {
            $("#toPopup").removeClass("hidepo");
            $("#toPopup1").show();
            $.ajax({
                url: UrlBase + "FltSearch1.asmx/ViewSeatDetails",
             //   url: UrlBase + "/Flight/ViewSeatDetails",
                //////data: "{'txtPCCImpPnr':'" + $.trim(h.txtPCCImpPnr.val()) + "','txtPNRImpPnr':'" + $.trim(h.txtPNRImpPnr.val()) + "','txtQueueNoImpPnr':'" + $.trim(h.txtQueueNoImpPnr.val()) + "','sltCopIDImpPnr':'" + $.trim(h.sltCopIDImpPnr.val()) + "','sltTripImpPnr':'" + $.trim(h.sltTripImpPnr.val()) + "','sltTripTypeImpPnr':'" + $.trim(h.sltTripTypeImpPnr.val()) + "'}",
                data: "{'Airline':'" + $.trim($("#ctl00_ContentPlaceHolder1_IBValidatingCarrier").val()) + "','OrderId':'" + $.trim($("#ctl00_ContentPlaceHolder1_IBTrackIds").val()) + "'}",
             //   data: "{'Airline':'" + $.trim(h.airline_ib.val()) + "','OrderId':'" + $.trim(h.orderid_ib.val()) + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    var htmlStr = "";
                    var Error = "";
                    var PaxD = JSON.parse(data.d);
                 
                    ft = "inbound";
                    if (PaxD.PaxListDetails != null) {
                      //  //#region MultiPax
                      ////  var o = $.parseJSON($("#hdnPaxListforSeat").val());
                      //  var PaxListDetails = JSLINQ(d.PaxListDetails)
                      //                  .Where(function (item) { return item.PaxType != "INF"; })
                      //                  .Select(function (item) { return item; });
                      //  d.PaxListDetails = PaxListDetails.items;
                      //  //#endregion
                        //  SeatLayout(d, seatslArray_ib);
                        seatslArray_ib = new Array();
                        var PaxInfolist = GetPaxInfoList(GetPaxInfo());
                        var PaxListDetails = JSLINQ(PaxD.PaxListDetails)
                                        .Where(function (item) { return item.PaxType != "INF"; })
                                        .Select(function (item) { return item; });
                        PaxD.PaxListDetails = PaxListDetails.items;
                        //#endregion
                        SeatLayout(PaxD, seatslArray_ib, PaxInfolist);
                    }
                    else {
                        alert("Seat map is not available.");
                        $.unblockUI();
                    }


                },
                error: function (e, t, n) {
                    alert("Seat map not available please try again later");
                    $.unblockUI();
                }
            });
        }
        else {
            alert("Please fill all passenger informaton");
            $.unblockUI();
            $("#toPopup").addClass("hidepo");
            $("#toPopup1").hide();
        }
    });
    /* event for close the popup */
    $("div.close").hover(
					function () {
					    $('span.ecs_tooltip').show();
					},
					function () {
					    $('span.ecs_tooltip').hide();
					}
				);

    $("body").on("click", "div.close", function () {
        disablePopup();  // function close pop up
    });

    $("body").on("click", "div.close", function () {
        disablePopup();  // function close pop up
    });
    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup();  // function close pop up
        }
    });

    $('a.livebox').click(function () {
        alert('Hello World!');
        return false;
    });
  
    /************** start: functions. **************/
    function loading() {
        $("div.loader").show();
    }
    function closeloading() {
        $("div.loader").fadeOut('normal');
    }



    function loadPopup() {
        if (popupStatus == 0) { // if value is 0, show popup
            closeloading(); // fadeout loading
            $("#toPopup").fadeIn(0500); // fadein popup div
            $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
            $("#backgroundPopup").fadeIn(0001);
            popupStatus = 1; // and set value to 1
        }
    }

    function disablePopup() {
        if (popupStatus == 1) { // if value is 1, close popup
            $("#toPopup").fadeOut("normal");
            $("#backgroundPopup").fadeOut("normal");
            $("#toPopup1").hide();
            popupStatus = 0;  // and set value to 0
        }
    }
    /************** end: functions. **************/
});
function sortNumber(a, b) {
    return a - b;
}
Array.prototype.unique =
  function () {
      var a = [];
      var l = this.length;
      for (var i = 0; i < l; i++) {
          for (var j = i + 1; j < l; j++) {
              // If this[i] is found later in the array
              if (this[i] === this[j])
                  j = ++i;
          }
          a.push(this[i]);
      }
      return a;
  };

function SeatLayout(d, seatslArray, PaxInfolist) {
  
    for (var l = 0; l < PaxInfolist.length; l++) {
        d.PaxListDetails[l].FName = PaxInfolist[l].FirstName;//d.PaxListDetails[l].FName == "" ? ($.trim($("#PaxListDetails_" + l + "__FName").val()) == "" ? ("Pax " + (l + 1)) : $.trim($("#PaxListDetails_" + l + "__FName").val())) : d.PaxListDetails[l].FName;
        d.PaxListDetails[l].LName = PaxInfolist[l].LastName;
        d.PaxListDetails[l].Title = PaxInfolist[l].Title;
        d.PaxListDetails[l].Gender = PaxInfolist[l].Gender;
        d.PaxListDetails[l].DOB = PaxInfolist[l].DOB;
    }
    var res = "";
    try {

        $("#SeatSource").empty();
        if (d.Error != "" && d.Error == null && d.SeatMapAll[0].SeatMapDetails_Final[0].Error != "" && d.SeatMapAll[0].SeatMapDetails_Final[0].Error == null) {
            res += '<div class="w40 lft textheads">Select Your Seats </div> '
            res += '<div class="w70 rgt" style="height: 22px;padding-top: 11px;" >'
            res += '<div class="w100 rgt">'
            for (var sec = 0; sec < d.SeatMapAll.length; sec++) {
                res += '<div class="w15 lft">'
                res += '<div class="w100 lft">'
                if (sec == 0) {
                    res += '<a class="clsorgdest fltselected" id="sector' + sec + '"  origin="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + '"  destination="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + '">' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + ' - ' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + ' </a>';
                }
                else {
                    res += '<a class="clsorgdest" id="sector' + sec + '"   origin="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + '"  destination="' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + '">' + d.SeatMapAll[sec].SeatMapDetails_Final[0].DepartureStation + ' - ' + d.SeatMapAll[sec].SeatMapDetails_Final[0].ArrivalStation + ' </a>';
                }
                res += '</div>'
                res += '</div>'
            }
            res += '</div>'
            res += '</div>'
            res += '<div class="rgt vListContainer"  style="border-top: 1px solid #e9e9e9; width:100%;margin-top:20px;  ">';
            res += '<div class="w30 lft" style="border-right: 1px solid #e9e9e9;height:500px;">';
            for (var pax = 0; pax < PaxInfolist.length ; pax++) {
                res += '<div class="w100 lftgreen lft"  >';
                if (pax == 0) {
                    res += '<div class="clspaxdtls paxselected" id="Traveller' + pax + '" Traveller="Traveller' + pax + '" Fname="' + PaxInfolist[pax].FirstName + '" Lname="' + PaxInfolist[pax].LastName + '"   Title="' + PaxInfolist[pax].Title + '" Tpcode="' + PaxInfolist[pax].PAxID + '">' + d.PaxListDetails[pax].Title + ' ' + d.PaxListDetails[pax].FName + ' ' + d.PaxListDetails[pax].LName + '  <span class="spntrvl" style="color:#000;font-size:12px;"  id="spnTraveller' + pax + '"></div>';
                    res += '<input type="hidden" id="hdn_selected" value="Traveller' + pax + '"/>'
                }

                else
                    res += '<div class="clspaxdtls" id="trvl' + pax + '" Traveller="Traveller' + pax + '" Fname="' + PaxInfolist[pax].FirstName + '" Lname="' + PaxInfolist[pax].LastName + '"   Title="' + PaxInfolist[pax].Title + '" Tpcode="' + PaxInfolist[pax].PAxID + '">' + d.PaxListDetails[pax].Title + ' ' + d.PaxListDetails[pax].FName + ' ' + d.PaxListDetails[pax].LName + ' <span class="spntrvl" style="color:#000;font-size:12px;" id="spnTraveller' + pax + '"></span></div>';
                res += '<input type="hidden" id="hdn_selected_title" value="' + PaxInfolist[pax].Title + '"/>'
                res += '<input type="hidden" id="hdn_selected_fname" value="' + PaxInfolist[pax].FirstName + '"/>'
                res += '<input type="hidden" id="hdn_selected_lname" value="' + PaxInfolist[pax].LastName + '"/>'
                res += '<input type="hidden" id="hdn_selected_tpcode" value="' + PaxInfolist[pax].PAxID + '"/>'
                res += '</div>';
                res += '<div class="clear"></div>';
            }
            //for (var pax = 0; pax < d.PaxListDetails.length ; pax++) {
            //    res += '<div class="w100 lftgreen lft"  >';
            //    if (pax == 0) {
            //        res += '<div class="clspaxdtls paxselected" id="Traveller' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d.PaxListDetails[pax].FName + '" Lname="' + d.PaxListDetails[pax].LName + '"   Title="' + d.PaxListDetails[pax].Title + '" Tpcode="' + d.PaxListDetails[pax].ProfileCode + '">' + d.PaxListDetails[pax].FName + ' <span class="spntrvl" style="color:#000;font-size:9px;" id="spnTraveller' + pax + '"></div>';
            //        res += '<input type="hidden" id="hdn_selected" value="Traveller' + pax + '"/>'
            //    }

            //    else
            //        res += '<div class="clspaxdtls" id="trvl' + pax + '" Traveller="Traveller' + pax + '" Fname="' + d.PaxListDetails[pax].FName + '" Lname="' + d.PaxListDetails[pax].LName + '"   Title="' + d.PaxListDetails[pax].Title + '" Tpcode="' + d.PaxListDetails[pax].ProfileCode + '">' + d.PaxListDetails[pax].FName + ' <span class="spntrvl" style="color:#000;font-size:9px;" id="spnTraveller' + pax + '"></span></div>';
            //    res += '<input type="hidden" id="hdn_selected_title" value="' + d.PaxListDetails[pax].Title + '"/>'
            //    res += '<input type="hidden" id="hdn_selected_fname" value="' + d.PaxListDetails[pax].FName + '"/>'
            //    res += '<input type="hidden" id="hdn_selected_lname" value="' + d.PaxListDetails[pax].LName + '"/>'
            //    res += '<input type="hidden" id="hdn_selected_tpcode" value="' + d.PaxListDetails[pax].ProfileCode + '"/>'
            //    res += '</div>';
            //    res += '<div class="clear"></div>';
            //}
            res += '</div>';
            res += '<div class="w70 lft p10">';
            res += '<div class="w100 lft texthead ">Seat selection for <span id="selectedtrvl"></span> </div>'
            res += '<div class="clear"></div>';
            var tot = 0;
            var blank = 1;
            for (var is = 0; is < d.SeatMapAll.length; is++) {
                res += '<div style="height: 297px; width: 100%; overflow-x: scroll; overflow-y: hidden;display:none" class="seatlayout" id="' + d.SeatMapAll[is].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[is].SeatMapDetails_Final[0].ArrivalStation + '">';
                res += '<div class="w20 lft flightSeatMap" style="height: 256px; " >' + ' <img class="front" src="' + UrlBase + 'images/Seat/front.png"  />' + '</div>';
                res += '<div class="w80 lft" >';
                res += '<table width="100%" style="height: 230px; width: auto; overflow-x: scroll;border-top: 2px solid #fff;border-bottom: 2px solid #fff; overflow-y: hidden;" id="seat_div">';
                res += '<tr>';
                var fl = 0;
                for (var ir = 0; ir < d.SeatMapAll[is].Rows; ir++) {
                    res += '<td>';
                    res += '<table width="100%">';
                    //if (d.Provider == "GDS") {
                    //    if (fl == 0) {
                    //        for (var ap = 0; ap < d.SeatMapAll[is].Alpha.length; ap++) {

                    //            res += '<tr>';
                    //            res += '<td style="padding:1px;padding-bottom:5px;text-align: center;vertical-align: top; color:#ccc;font-size: 12px; ">';
                    //            res += '<a class="clsseat">' + d.SeatMapAll[is].Alpha[ap].Value + '</a>';
                    //            res += '</td>';
                    //            res += ' </tr>';


                    //        }
                    //    }
                    //}
                    for (var ic = 0; ic < d.SeatMapAll[is].Columns; ic++) {
                        var ExitSeats = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].ExitSeats;
                        var Message = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Message;//GDS
                        var seatrow = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].RowNo;//GDS
                        if (ic == 0 && d.Provider == "LCC") {
                            if (ExitSeats == "EXIT")
                                res += '<tr><td style="padding:1px"><div>  <img src="' + UrlBase + 'images/Seat/exit.png" /> </div></td></tr>';
                            else
                                res += '<tr><td style="padding:1px">&nbsp;</td></tr>';
                        }
                        if (Message != "hide") {
                            res += '<tr>';
                            res += '<td style="padding:1px;padding-bottom:5px;text-align: center;vertical-align: top; color:#ccc;font-size: 12px; ">';
                            var seatname = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator;
                            var sectorname = d.SeatMapAll[is].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[is].SeatMapDetails_Final[0].ArrivalStation;
                            var seatstatus = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatStatus;
                            var seatfee = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatFee;
                            var SeatAlignment = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatAlignment;
                            var FlightNumber = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].FlightNumber;
                            var FlightTime = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].FlightTime;
                            var id = sectorname + ir.toString() + ic.toString();

                            var ref = "", group = "", carrier = "", classofservice = "", equipment = "", paid = "false";
                            if (d.Provider == "GDS") {
                                ref = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SegmentRef;
                                group = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Group;
                                carrier = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Carrier;
                                classofservice = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].ClassOfService;
                                equipment = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Equipment;
                                paid = d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].Paid;
                            }


                            if (seatstatus == "BLANK") {//seatstatus == "BLANK" || seatstatus == "BLANK"Message == "WAY"
                                if (d.Provider == "LCC") {
                                    res += '<a class="clsseat" id="' + id + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  title="' + d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator + '">' + blank + '</a>';
                                    blank = blank + 1;
                                }
                                else {
                                    res += '<a class="clsseat" id="' + id + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  title="' + d.SeatMapAll[is].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails[tot].SeatDesignator + '">' + seatrow + '</a>';
                                    blank = blank + 1;
                                }
                            }
                            else {
                                if (seatstatus == "NoSeat") {
                                    res += '<a class="" title="OCCUPIED" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '" sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '">&nbsp;</a>';//<img src="' + UrlBase + 'images/Seat/seat_occupied.png" />
                                }
                                else if (seatstatus == "OCCUPIED") {
                                    res += '<a class="clsseat OCCUPIED" title="OCCUPIED" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '" sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_occupied.png" />
                                }
                                else {
                                    if (d.Provider == "LCC")
                                        res += '<a class="clsseat PRICE_' + seatfee + '" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />
                                    else {
                                        if (paid == true)

                                            res += '<a class="clsseat PRICE_PAID" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />
                                        else
                                            res += '<a class="clsseat PRICE_' + seatfee + '" id="' + id + '" FlightTime="' + FlightTime + '" FlightNumber="' + FlightNumber + '" seatalignment=' + SeatAlignment + ' seatstatus="' + seatstatus + '"  original-title="Pick this"  sectorname="' + sectorname + '" original-title="Pick this"  seatname="' + seatname + '"  seatfee="' + seatfee + '" ref="' + ref + '" group="' + group + '" carrier="' + carrier + '" classofservice="' + classofservice + '" equipment="' + equipment + '" paid="' + paid + '"></a>';//<img src="' + UrlBase + 'images/Seat/seat_0.png" />

                                    }
                                }
                            }
                            res += '</td>';
                            res += ' </tr>';

                            if (ic == d.Columns - 1 && d.Provider == "LCC") {
                                if (ExitSeats == "EXIT")
                                    res += '<tr><td style="padding:1px"><div>  <img src="' + UrlBase + 'images/Seat/Exit1.png" /> </div></td></tr>';
                                else
                                    res += '<tr><td style="padding:1px">&nbsp;</td></tr>';
                            }
                        }

                        tot = tot + 1;
                    }
                    res += '</table>';
                    res += '</td>';
                    fl = fl + 1;
                }
                //}
                res += '<td style="float:right;height: 210px;padding: -3px;width: 60px;" >' + ' <img class="rear" src="' + UrlBase + 'images/Seat/rear.png" />' + '</td>';
                res += ' </tr>';
                res += '</table>';
                res += '</div>';
                res += '</div>';
                blank = 1;
                tot = 0;
            }
            res += '</div>';
            try {
                var SeatFareArray = JSLINQ(d.SeatMapAll[0].SeatMapDetails_Final[0].SeatMapDetails[0].SeatListDetails)
              .Select(function (item) { return item });
                var UniqueArray = (JSLINQ(SeatFareArray.items).Select(function (item) {
                    return $.trim(item.SeatFee)
                }).OrderByDescending(function (item) {
                    return $.trim(item.SeatFee)
                })).items.unique();
                var SortedArray = UniqueArray.sort(sortNumber);
                var prcstring = "";
                if (d.Provider == 'GDS') {
                    prcstring = prcstring + "<li> <li> <a class='PRICE_0'></a>Free Seat</li></li>";//<a class='PRICE_PAID'></a>Paid Seat
                }
                else {
                    for (var i = 0; i < SortedArray.length; i++) {
                        prcstring = prcstring + "<li> <a class='PRICE_" + SortedArray[i] + "'></a>Rs " + SortedArray[i] + "</li>";
                    }
                }
                if (d.Provider == 'GDS') {
                    res += '<div class="w75 rgt"><ul class="pricetbs"> ' + prcstring + ' <li> <a class="OCCUPIED"></a>Occupied</li><li> <a class="SELECTED"></a>Selected</li> <li> <a class=""></a></li>   </ul> </div>'
                }
                else {
                    res += '<div class="w75 rgt"><ul class="pricetbs"> ' + prcstring + ' <li> <a class="OCCUPIED"></a>Occupied</li><li> <a class="SELECTED"></a>Selected</li> <li> <a class=""></a><img src="' + UrlBase + 'images/Seat/Exitb.png"  />   Exit</li>   </ul> </div>'
                }

            }
            catch (er) {
            }
            res += '<div class="w100">'

            res += '<div class="w30 lft" style="padding-left:20px;">'
            res += '<span class="hide" id="spnamount"></span> <span class="hide" id="spnamount_ib"></span>';
            res += '</div>'

            res += '<div class="w45 rgt" style="padding-right:20px;">'
            res += '<input type="button" class="btn_close rgt " id="btn_confirm" value="Continue"/>'
            res += '</div>'
            res += '</div>';

            res += '</div>';
            res += '';
            $("#SeatSource").append(res);
            $('#' + d.SeatMapAll[0].SeatMapDetails_Final[0].DepartureStation + '-' + d.SeatMapAll[0].SeatMapDetails_Final[0].ArrivalStation).show();
            var Name = d.PaxListDetails[0].Title + ' ' + d.PaxListDetails[0].FName + ' ' + d.PaxListDetails[0].LName;
            $("#selectedtrvl").html(Name);
            $.unblockUI();
        }
        else {
            $("#SeatSource").empty();
            res += '<div class="w60 lft text-center" style="width:100%; text-align:center; padding-top:40px " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
            res += '<div class="w60 lft texthe" style="width:100%; text-align:center; padding-top:70px">Sorry! Seat map is not available right now.</div> '
            $("#SeatSource").append(res);
            $.unblockUI();
        }
    }
    catch (er) {
        $("#SeatSource").empty();
        res += '<div class="w60 lft text-center" style="width:100%; text-align:center;padding-top:40px " > <img height="200" width="200" src="../images/Seat/oops.jpg" /> </div> '
        res += '<div class="w60 lft texthe" style="width:100%; text-align:center; padding-top:70px ">Sorry! Seat map is not available right now..</div> '
        $("#SeatSource").append(res);
        $.unblockUI();
    }
}

function BlockUIFC() {
    $.blockUI({
        message: $("#waitMessagefc")
    });
}


function GetPaxInfo()
{
    var PaxListId = new Array();
    var PaxList = new Array();//'ctl00$ContentPlaceHolder1$Repeater_Child$ctl01$txtCFirstName'
    var ADTCount = $("#ctl00_ContentPlaceHolder1_td_Adult .row div div input");
    var CHDCount = $("#ctl00_ContentPlaceHolder1_td_Child .row div div input");
    var listAdt = ADTCount.filter(function (a, b) {
        if ($(b).attr("id").indexOf("txtAFirstName")!=-1)
            PaxListId.push({ PAXIndex: a, Type: "ADT", PAxID: $(b).attr("id").replace("txtAFirstName", "") });
    });
    var listChd = CHDCount.filter(function (a, b) {
        if ($(b).attr("id").indexOf("txtCFirstName") != -1)
            PaxListId.push({ PAXIndex: a, Type: "CHD", PAxID: $(b).attr("id").replace("txtCFirstName", "") })
    });    
    return PaxListId;
}
function GetPaxInfoList(infolist) {
  
    var PaxList = new Array();
    var listAdt = infolist.filter(function (b, a) {
        if (b.Type == "ADT" && $("#" + b.PAxID + "ddl_ATitle").val() != "" && $("#" + b.PAxID + "txtAFirstName").val() != "" && $("#" + b.PAxID + "txtAFirstName").val() != "First Name" && $("#" + b.PAxID + "txtALastName").val() != "Last Name" && $("#" + b.PAxID + "txtALastName").val() != "")
        {
            PaxList.push({
                PAXIndex: a,
                Type: b.Type,
                PAxID: (a + 1),
                Gender: $("#" + b.PAxID + "ddl_AGender").val(),
                Title: $("#" + b.PAxID + "ddl_ATitle").val(),
                FirstName: $("#" + b.PAxID + "txtAFirstName").val(),
                MiddleName: "",
                LastName: $("#" + b.PAxID + "txtALastName").val(),
                DOB: $("#" + b.PAxID + "Txt_AdtDOB").val(),
            });
        }
        else if (b.Type == "CHD" && $("#" + b.PAxID + "ddl_CTitle").val() != "" && $("#" + b.PAxID + "txtCFirstName").val() != "" && $("#" + b.PAxID + "txtCFirstName").val() != "First Name" && $("#" + b.PAxID + "txtCLastName").val() != "Last Name" && $("#" + b.PAxID + "txtCLastName").val() != "" && $("#" + b.PAxID + "Txt_chDOB").val() != "DOB" && $("#" + b.PAxID + "Txt_chDOB").val() != "")
        {
            var gen = $("#" + b.PAxID + "ddl_CTitle").val() == "Mstr" ? "M" : "F";
            PaxList.push({
                PAXIndex: a,
                Type: b.Type,
                PAxID: (a + 1),
                Gender: gen,
                Title: $("#" + b.PAxID + "ddl_CTitle").val(),
                FirstName: $("#" + b.PAxID + "txtCFirstName").val(),
                MiddleName: "",
                LastName: $("#" + b.PAxID + "txtCLastName").val(),
                DOB: $("#" + b.PAxID + "Txt_chDOB").val(),
            });
        }  
    });

    if (infolist.length != PaxList.length)
    {
        PaxList = new Array();
    }
    return PaxList;
}



