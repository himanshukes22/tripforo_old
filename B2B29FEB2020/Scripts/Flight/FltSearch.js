//Form Load function start
var SHandler;
$(document).ready(function() {
    SHandler = new SearchHelper();
    SHandler.BindEvents();
});
//Form Load function End

//Search Helper Function Starts
var DATA;
var Trip;
var SearchHelper = function() {
    this.flight = $("flight");
    this.txtDepCity1 = $("#txtDepCity1"); //DepartureCity
    this.txtArrCity1 = $("#txtArrCity1");
    this.btnSearch = $("#btnSearch");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    this.rooms = $("#rooms");
    this.rdbOneWay = $("#chds");
    this.rdbRoundTrip = $("#rdbRoundTrip");
    this.txtDepDate = $("#txtDepDate");
    this.txtRetDate = $("#txtRetDate");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
    this.trRetDateRow = $("#trRetDateRow");
    this.TripType = $('input[name=TripType]');
    this.Adult = $("#Adult");
    this.Child = $("#Child");
    this.Infant = $("#Infant");
    this.Cabin = $("#Cabin");
    this.txtAirline = $("#txtAirline");
    this.hidtxtAirline = $("#hidtxtAirline");
    this.chkNonstop = $("#chkNonstop");
    this.chkAdvSearch = $("#chkAdvSearch");
    this.trAdvSearchRow = $("#trAdvSearchRow");
    this.LCC_RTF = $("#LCC_RTF");
    this.showresult1 = $("#showresult1");
    this.showresult2 = $("#showresult2");
    this.showresult3 = $("#showresult3");
    //For Filter Matrix
    this.AirIndia = $("#AI");
    this.JetAirways = $("#9W");
    this.JetKonnect = $("#S2");
    this.Kingfisher = $("#IT");
    this.Spicejet = $("#SG");

}

//The prototype property allows you to add properties and methods to an object.
//Add BindEvents to SearchHelper
//Start BindEvents 
SearchHelper.prototype.BindEvents = function() {
    // 
    var h = this;
    var Depdate = h.hidtxtDepDate.val();
    var returnDate = h.hidtxtRetDate.val();

    //Date Picker Bind

    var dtPickerOptions = { numberOfMonths: 2, dateFormat: "dd/mm/yy", maxDate: "+1y", minDate: 0, showOtherMonths: true, selectOtherMonths: false };
    h.txtDepDate.datepicker(dtPickerOptions).datepicker("option", { onSelect: h.UpdateRoundTripMininumDate }).datepicker("setDate", Depdate.substr(0, 10));
    h.txtRetDate.datepicker(dtPickerOptions).datepicker("setDate", returnDate.substr(0, 10));

    //Origin AutoComplete

    var autoCity = UrlBase + "AutoComplete.asmx/FetchCityList"; //Url name should be relative
    // 
    h.txtDepCity1.autocomplete({
        source: function(request, response) {//Source of AutoComplete is an Asynchronus Function
            $.ajax({
                url: autoCity, //"AutoComplete.asmx/FetchCityList",
                data: "{ 'city': '" + request.term + "', maxResults: 10 }",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var result = item.CityName + "(" + item.AirportCode + ")";
                        var hidresult = item.AirportCode + "," + item.CountryCode;
                        return { label: result, value: result, id: hidresult }
                    }))
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {alert(textStatus); }
            })
        },
        autoFocus: false, //If set to true the first item will be automatically focused(No change)
        minLength: 3, //The minimum number of characters a user has to type before the Autocomplete activates.
        select: function(event, ui) {// Select Event is Triggered when an item is selected from the menu; ui.item refers to the selected item.
            h.hidtxtDepCity1.val(ui.item.id);
        }
    });


    //Destination AutoComplete
    var autoDest = UrlBase + "AutoComplete.asmx/FetchCityList"
    h.txtArrCity1.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoDest,
                data: "{ 'city': '" + request.term + "', maxResults: 10 }",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var result = item.CityName + "(" + item.AirportCode + ")";
                        var hidresult = item.AirportCode + "," + item.CountryCode;
                        return { label: result, value: result, id: hidresult }
                    }))
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function(event, ui) {
            h.hidtxtArrCity1.val(ui.item.id);
        }
    });

    // Airline AutoComplete

    var autoAirline = UrlBase + "AutoComplete.asmx/FetchAirlineList";
    h.txtAirline.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoAirline,
                data: "{ 'airline': '" + request.term + "', maxResults: 10 }",
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var result = item.ALName + "(" + item.ALCode + ")";
                        var hidresult = item.ALName + "," + item.ALCode;
                        return { label: result, value: result, id: hidresult }
                    }))
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            })
        },
        autoFocus: true,
        minLength: 3,
        select: function(event, ui) {
            h.hidtxtAirline.val(ui.item.id);
        }
    });

    //Check TripType
    h.TripType.click(function() {
        if ($(this).val() == "rdbOneWay" || $(this).val() == "rdbRoundTrip") {
            if ($(this).val() == "rdbRoundTrip") {
                h.UpdateRoundTripMininumDate(h.txtDepDate.val());
                h.trRetDateRow.show();
            }
            else {
                h.trRetDateRow.hide();

            }
        }
        else { }
    })

    //Advance search in case of domestic search
    //Check Trip as Well
    h.chkAdvSearch.click(function() {
        if ($(this).is(":checked")) {
            if ((h.hidtxtDepCity1.val() != '') && (h.hidtxtArrCity1.val() != '')) {
                var hidDep = h.hidtxtDepCity1.val().split(",");
                var hidRet = h.hidtxtArrCity1.val().split(",");
                if ((hidDep[1] == 'IN') && (hidRet[1] == 'IN')) {
                    h.chkAdvSearch.attr("value", true);
                }
                else {
                    h.chkAdvSearch.attr("value", false);
                    alert("Only for Domestic Search");
                    return false;
                }
            }
            else {
                h.chkAdvSearch.attr("value", false);
                alert("Please Select Origin And Destination");
                return false;
            }
        }
        else { h.chkAdvSearch.attr("value", false); }
    })

    //Search Button Click
    h.btnSearch.click(function() { return h.validate(); });

}
//End  of SearchHelper.prototype.BindEvents

//Update minimum date of return date on the basis of the selected departure date
SearchHelper.prototype.UpdateRoundTripMininumDate = function(dateText, inst) { SHandler.txtRetDate.datepicker("option", { minDate: dateText }); }


//Start Validate Search Engine - Handles Validation
SearchHelper.prototype.validate = function() {
    // 
    var h = this;
    var TripType = $("input[name='TripType']:checked").val();
    CheckTrip(h.hidtxtDepCity1.val(), h.hidtxtArrCity1.val());
    if (TripType == "rdbOneWay" || TripType == "rdbRoundTrip") {
        if ($.trim(h.txtDepCity1.val()) == "") {
            alert('Origin Required.')
            
            h.txtDepCity1.val("").focus();
            return false;
        }
        else if (h.hidtxtDepCity1.val() == "") {
            //Bind CSS
            alert('Invalid Origin.');
            h.txtDepCity1.val("").focus();
            return false;
        }

        if ($.trim(h.txtArrCity1.val()) == "") {
            //Bind CSS
            alert('Destination Required.');
            h.txtArrCity1.val("").focus();
            return false;
        }
        else if (h.hidtxtArrCity1.val() == "") {
            //Bind CSS
            alert('Invalid Destination.');
            h.txtArrCity1.val("").focus();
            return false;
        }

        if (h.txtDepDate.val() == "" || h.txtDepDate.val() == null) {
            //Bind CSS
            alert('Departure Date Required.');
            return false;
        }
        if (TripType == "rdbRoundTrip") { //|| (Trip == "I")//SAME VALIDATION FOR INTL TRIP
            if (h.txtRetDate.val() == "" || h.txtRetDate.val() == null) {
                //Bind CSS
                alert('Return Date Required.');
                return false;
            }
            var departdate = ChangeDateFormat(h.txtDepDate.val());
            var returnDate = ChangeDateFormat(h.txtRetDate.val());

            if (returnDate < departdate) {
                alert('Return Date Should Be Greater Than Departure Date.');
                return false;
            }
        }
        if ((h.LCC_RTF.attr("checked") == true) && (TripType != "rdbRoundTrip")) {
            h.LCC_RTF.attr("checked", false);
            alert('Please select round trip.');
            return false;
        }
    }
    else {
        return false;
    }


    var numAdt = parseInt(h.Adult.val());
    var numChd = parseInt(h.Child.val());
    var numInf = parseInt(h.Infant.val());

    var totPax = numAdt + numChd;

    if (totPax > 9) {
        alert('Total Number Of Passenger Should Be Less Than 9.');
        return false;
    }
    if (numInf > numAdt) {
        alert('Number Of Infant Should Be Less Than Or Equal To Number Of Adult');
        return false;
    }
    if (h.txtAirline.val() != '') {
        if (h.hidtxtAirline.val() == '') {
            alert('Invalid Airline');
            h.txtAirline.val("").focus();
            return false;
        }
    }
    h.StartSearch();
}
//End Validate Search Engine

SearchHelper.prototype.StartSearch = function() {
    var h = this;
    var TripType = $("input[name='TripType']:checked").val();
    var RTF;
    var Nstop;
    if (h.LCC_RTF.attr("checked") == true) { RTF = 'TRUE'; } else { RTF = 'FALSE'; }
    if (h.chkNonstop.attr("checked") == true) { Nstop = 'TRUE'; } else { Nstop = 'FALSE'; }
    //CheckTrip(h.hidtxtDepCity1.val(), h.hidtxtArrCity1.val());
    //SEND THESE TO QUERY STRING
    var dataString = 'TripType=' + TripType + '&txtDepCity1=' + h.txtDepCity1.val() + '&txtArrCity1=' + h.txtArrCity1.val() + '&hidtxtDepCity1=' + h.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + h.hidtxtArrCity1.val() + '&Adult=' + h.Adult.val();
    dataString += '&Child=' + h.Child.val() + '&Infant=' + h.Infant.val() + '&Cabin=' + h.Cabin.val() + '&txtAirline=' + h.txtAirline.val() + '&hidtxtAirline=' + h.hidtxtAirline.val() + '&txtDepDate=' + h.txtDepDate.val() + '&txtRetDate=' + h.txtRetDate.val();
    dataString += '&Nstop=' + Nstop + '&RTF=' + RTF + '&Trip=' + Trip;
    document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
    if (Trip == "D") {
        document.forms[0].action = UrlBase + 'defaulttest.aspx?' + dataString;
        //window.location.href = UrlBase + 'Result.aspx?' + dataString;
     }
    else if (Trip == "I") { document.forms[0].action = UrlBase + 'Flight/FltResultIntl.aspx?' + dataString; };
    document.forms[0].submit();

}


function CheckTrip(DepCity,ArrCity) {
    if ((DepCity!= '') && (ArrCity!= '')) {
        var hidDep = DepCity.split(",");
        var hidRet = ArrCity.split(",");
        if ((hidDep[1] == 'IN') && (hidRet[1] == 'IN')) {
            Trip ="D";
        }
        else {
            Trip = "I";
        }
    }
}
