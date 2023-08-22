var SHandlerHotel;
$(document).ready(function() {
    SHandlerHotel = new SearchHelperHotel();
    SHandlerHotel.BindEvents();
});
var SearchHelperHotel = function() {
    this.CheckInDate = $("#htlcheckin");
    this.CheckOutDate = $("#htlcheckout");
    this.HotelCity = $("#htlCity");
    this.HTLCityList = $("#htlcitylist");
    this.HotelName = $("#htlname");
    this.HotelCode = $("#Hotelcode");
    this.CountryCode = $("#contrycode");
    this.Room = $("#rooms");
    this.Child = $("#chds");
    this.buttonSearchHotel = $("#btnHotel");
    this.buttonAddOpt = $("#buttonAddOpt");
}

SearchHelperHotel.prototype.BindEvents = function() {
    var hk = this; var HtlDatePickerOptions;
    var checkin = hk.CheckInDate.val();
    var checkout = hk.CheckOutDate.val();

    HtlDatePickerOptions = { numberOfMonths: 2, dateFormat: "dd/mm/yy", maxDate: "+1y", minDate: "0", showOtherMonths: true, selectOtherMonths: false };
    
    hk.CheckInDate.datepicker(HtlDatePickerOptions).datepicker("option", { onSelect: hk.UpdateCheckoutDate }).datepicker("setDate", checkin.substr(0, 10));
    hk.CheckOutDate.datepicker(HtlDatePickerOptions).datepicker("option", { onSelect: dateDifferences }, checkout.substr(0, 10));
    //Date Picker Bind 

    //Hotel City Search Strat
    var autoCity = UrlBase + "Hotel/HotelSearchs.asmx/HtlCityList";
    hk.HotelCity.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoCity,
                data: "{ 'city': '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function(data) { return data; },
                success: function(data) {
                    response($.map(data.d, function(item) {
                        return {
                            label: item.CityName + ", " + item.Country + " (" + item.CityCode + ")",
                            // label2: item.CityName + "," + item.CityCode + "," + item.Country + ',' + item.CountryCode + ',' + item.SearchType + ',' + item.RegionID,
                            label2: item.CityName + "," + item.CityCode + "," + item.Country + ',' + item.CountryCode + ',' + item.RegionID + ',' + item.InnstantCityID + ',' + item.ExpediaRegionID + ',' + item.GRNCityCode + ',' + item.ZumataRegionID + ',' + item.SearchType,
                            label1: item.CountryCode,
                            label3: item.NoOfHotel,
                            value: item.CityName
                        }
                    }))

                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 3,
        select: function(event, ui) {
            hk.HTLCityList.val(ui.item.label2);
            hk.CountryCode.val(ui.item.label1);
        },
        change: function(event, ui) {
            if (!ui.item) {
                hk.HTLCityList.val('');
            }
        }
    });
    //.data("autocomplete")._renderItem = function(ul, item) {
    //    var inner_html = ''; 
    //    if (parseInt($.trim(item.label3)) > 0)
    //        inner_html = '<a>' + item.label + ' <div style="float:right;">(' + item.label3 + ' Hotels)</div></a>';
    //    else
    //        inner_html = '<a>' + item.label + '</a>';
    //    return $("<li></li>")
    //       .data("item.autocomplete", item)
    //        .append(inner_html)
    //        .appendTo(ul);
    //};
    //Hotel City Search End
    //    .data("autocomplete")._renderItem = function(ul, item) {
    //        var inner_html = '';
    //        if (parseInt($.trim(item.label3)) > 0)
    //            inner_html = '<a>' + item.label + ' <div style="float:right;">('+ item.label3 +' Hotels)</div></a>';
    //        else
    //            inner_html = '<a>' + item.label + '</a>';
    //        return $("<li></li>")
    //            .data("item.autocomplete", item)
    //            .append(inner_html)
    //            .appendTo(ul);

    //        //        if ($.trim(item.label3) == "AREA")
    //        //            inner_html = '<a>' + item.label + ' <div style="float:right;">Area</div></a>';
    //        //        else
    //        //            inner_html = '<a>' + item.label + '</a>';
    //    };
    //Hotel Name Search Strat
    var autoHotel = UrlBase + "Hotel/HotelSearchs.asmx/HotelNameSearch";
    this.HotelName.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoHotel,
                data: "{ 'HotelName': '" + request.term + "', 'city': '" + hk.HotelCity.val() + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function(data) { return data; },
                success: function(data) {
                    response($.map(data.d, function(item) {
                        return {
                            label: item.CityName, value: item.CityName, label1: item.CityCode
                        }
                    }))
                }
            });
        },
        minLength: 3,
        select: function(event, ui) {
            hk.HotelCode.val(ui.item.label1);
        }
    });
    //Hotel Name Search End

    // set effect from select menu value
    hk.buttonAddOpt.click(function() {
        var options = {};
        $("#effect").toggle('blind', options, 500);
    });

    hk.buttonSearchHotel.click(function() {

        if (hk.HotelCity.val() == "") {
            alert('please provide city');
            hk.HotelCity.focus();
            return false;
        }
        if (hk.HTLCityList.val() == "") {
            alert('Invalid City');
            hk.HotelCity.focus();
            return false;
        }

        var strChechin = hk.CheckInDate.val().split("/");
        var strChechout = hk.CheckOutDate.val().split("/");
        var ChechinDate = getNumberOfDays(strChechin[1], strChechin[2]) + strChechin[0];
        var ChechoutDate = getNumberOfDays(strChechout[1], strChechout[2]) + strChechout[0];
        if ((ChechoutDate - ChechinDate) > 0) {
            var rooms = $("#rooms").val();
            var chds = $("#chds").val();
            //Calculate Total pax start
            var AdtTolat = 0, childTotal = 0, infantTotal = 0;
            var SearchQuery = 'HtlResult.aspx?htlcitylist=' + hk.HTLCityList.val() + '&htlcheckin=' + hk.CheckInDate.val() + '&htlcheckout=' + hk.CheckOutDate.val() + '&numRoom=' + hk.Room.val();
            var AdtPerRoom = new Array(); var ChdPerRoom = new Array(); var ChdAge = [];
            $('#hot-search-params').find('select, input').each(function(key, value) {
                for (var i = 0; i < parseInt(rooms); i++) {
                    if (value.name == "room-" + i + "-adult-total") {
                        AdtTolat += parseInt(value.value);
                        SearchQuery += '&rooms[' + i + '].adult=' + value.value;
                        AdtPerRoom.push(value.value);
                    }
                    else if (value.name == "room-" + i + "-child-total") {
                        childTotal += parseInt(value.value);
                        SearchQuery += '&rooms[' + i + '].children=' + value.value;
                        ChdPerRoom.push(value.value);
                    }
                    for (var j = 0; j <= childTotal - 1; j++) {
                        if (value.name == "room-" + i + "-child-" + j + "-age") {
                            if (parseInt(value.value) <= 2) {
                                //childTotal = childTotal - 1
                                infantTotal++;
                            }
                            SearchQuery += '&rooms[' + i + '].child[' + j + '].age=' + value.value;
                            ChdAge.push(['R' + i + 'C' + j, value.value]);
                        }
                    }
                }
            });
            if (AdtTolat + childTotal - infantTotal > 9) {
                alert('Hotel booking will not allow more then 9 people travelling together at a time (that includes children whose age are above 2 year).');
                return false;
            }
         
            var nigtno = $('#nights').text().split(' ');
 
            SearchQuery += '&TotAdt=' + AdtTolat + '&TotChd=' + childTotal + '&htlCity=' + hk.HotelCity.val() + '&Nights=' + nigtno[0];
            //Calculate Total pax end
            if ($('#contrycode').val() != 'IN') {
                for (var r = 0; r < rooms; r++) {
                    var Sadt = "rooms[" + r + "].adult=";
                    var Schd = "rooms[" + r + "].children=";
                    var STotadt = SearchQuery.split(Sadt)[1].split('&');
                    var STotchd = SearchQuery.split(Schd)[1].split('&');

                    if (parseInt(STotadt[0]) > 3 && parseInt(STotchd[0]) > 0) {
                        alert(STotadt[0] + ' Adult and Child can not allowed in One Room. Please select less then 4 adult for child occupancy room.');
                        return false;
                    }
                    else if (parseInt(STotadt[0]) == 1 && parseInt(STotchd[0]) > 2) {
                        alert(STotadt[0] + ' Adult and 3 Child can not allowed in One Room. Please select 2 child occupancy room.');
                        return false;
                    }
                    else if (parseInt(STotadt[0]) == 2 && parseInt(STotchd[0]) > 2) {
                        var childcount = 0;
                        for (var ag = 0; ag < parseInt(STotchd[0]); ag++) {
                            var SchdAge = SearchQuery.split("rooms[" + r + "].child[" + ag + "].age=")[1].split('&');
                            if (parseInt(SchdAge[0]) > 2)
                                childcount++;
                            if (childcount > 2 || childcount == 0) {
                                alert(STotadt[0] + ' Adult and 3 Child can not allowed in One Room. Please select 2 child occupancy room.');
                                return false;
                            }
                        }
                    }
                }
            }
            if ($("#ReqType").val() == 'S') {
                var htlname = "";
                if ($("#contrycode").val() == "IN")
                    htlname = $("#Hotelcode").val();
                else
                    htlname = $("#htlname").val();

                var e = document.getElementById("htlstar");
                var htlstar = e.options[e.selectedIndex].index;

                document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
                window.location.href = UrlBase + 'Hotel/' + SearchQuery + '&htlname=' + htlname + '&htlstar=' + htlstar + '&Guest=' + AdtPerRoom + "_" + ChdPerRoom + "_" + ChdAge;
                //document.forms[0].action = 'Hotel/' + SearchQuery + '&htlname=' + htlname + '&htlstar=' + htlstar ;
                //document.forms[0].submit();
            }

            if ($("#ReqType").val() == 'MS') {
                document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
                window.location.href = UrlBase + 'Hotel/' + SearchQuery + '&Guest=' + AdtPerRoom + "_" + ChdPerRoom + "_" + ChdAge;
                //document.forms[0].action = SearchQuery;
                //document.forms[0].submit();
            }
        }
        else {
            alert('CheckOut Date Should be Greater then CheckIn Date');
            return false;
        }
        return true;

    });
}
//Form Load function End
//update minimum date of return date on the basis of the selected departure date
SearchHelperHotel.prototype.UpdateCheckoutDate = function (dateText, inst) {

    var newdays = SetnextDates(dateText)
    SHandlerHotel.CheckOutDate.datepicker("option", { minDate: newdays });
    dateDifferences();
}

function dateDifferences() {
    // check if both is not empty
    if ($("#htlcheckin").val() == '' || $("#htlcheckout").val() == '') return;
    var diff = ($("#htlcheckout").datepicker("getDate") - $("#htlcheckin").datepicker("getDate")) / 1000 / 60 / 60 / 24; // days

    if (diff > 1)
        $('#nights').html(diff + " Nights");
    else
        $('#nights').html(diff + " Night");
}
function SetnextDates(dateText) {
    var newdate = dateText.split("/"); var newdays = "";
    if (newdate[1] == "01" || newdate[1] == "03" || newdate[1] == "05" || newdate[1] == "07" || newdate[1] == "08" || newdate[1] == "10") {
        if (newdate[0] == "31")
            newdays = 1 + "/" + (parseInt(newdate[1]) + 1) + "/" + newdate[2];
        else
            newdays = (parseInt(newdate[0]) + 1) + "/" + newdate[1] + "/" + newdate[2];
    }
    else if (newdate[1] == "04" || newdate[1] == "06" || newdate[1] == "09" || newdate[1] == "11") {
        if (newdate[0] == "30")
            newdays = 1 + "/" + (parseInt(newdate[1]) + 1) + "/" + newdate[2];
        else
            newdays = (parseInt(newdate[0]) + 1) + "/" + newdate[1] + "/" + newdate[2];
    }
    else if (newdate[1] == "12") {
        if (newdate[0] == "31")
            newdays = 1 + "/" + 1 + "/" + (parseInt(newdate[2]) + 1);
        else
            newdays = (parseInt(newdate[0]) + 1) + "/" + newdate[1] + "/" + newdate[2];
    }
    else if (newdate[1] == "02") {
        if (newdate[2] % 4 == 0) {
            if (newdate[0] == "29")
                newdays = 1 + "/" + (parseInt(newdate[1]) + 1) + "/" + newdate[2];
            else
                newdays = ((parseInt(newdate[0]) + 1) + "/" + newdate[1] + "/" + newdate[2]);
        }
        else {
            if (newdate[0] == "28")
                newdays = 1 + "/" + (parseInt(newdate[1]) + 1) + "/" + newdate[2];
            else
                newdays = (parseInt(newdate[0]) + 1) + "/" + newdate[1] + "/" + newdate[2];
        }
    }
    return newdays;
}

//Date Calculation for Validation
function getNumberOfDays(month, year) {
    var noOfLpYear = parseInt((year - 1) / 4) - parseInt((year - 1) / 100) + parseInt((year - 1) / 400);
    var noOfYear = (year - 1) - noOfLpYear;
    var daysInYear = (noOfLpYear * 366) + (noOfYear * 365);
    var lpYear = 0;
    if ((year % 400 == 0 && year % 100 == 0) || (year % 4 == 0 && year % 100 != 0)) {
        lpYear = 1;
    }
    switch (month) {
        case '1': return daysInYear + 0;
        case '01': return daysInYear + 0;
        case '2': return daysInYear + 31;
        case '02': return daysInYear + 31;
        case '3': return daysInYear + lpYear + 59;
        case '03': return daysInYear + lpYear + 59;
        case '4': return daysInYear + lpYear + 89
        case '04': return daysInYear + lpYear + 89
        case '5': return daysInYear + lpYear + 120;
        case '05': return daysInYear + lpYear + 120;
        case '6': return daysInYear + lpYear + 150;
        case '06': return daysInYear + lpYear + 150;
        case '7': return daysInYear + lpYear + 181;
        case '07': return daysInYear + lpYear + 181;
        case '8': return daysInYear + lpYear + 212;
        case '08': return daysInYear + lpYear + 212;
        case '9': return daysInYear + lpYear + 242;
        case '09': return daysInYear + lpYear + 242;
        case '10': return daysInYear + lpYear + 273;
        case '11': return daysInYear + lpYear + 303;
        case '12': return daysInYear + lpYear + 334;
    }
}

