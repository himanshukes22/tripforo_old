var SHandlerBus;
$(document).ready(function() {
    SHandlerBus = new SearchHelperBus();
    SHandlerBus.BindEvents();

});
var SearchHelperBus = function () {

    this.SOURCE = $("#txtsrc");
    this.HID_SOURCE = $("#txthidsrc");
    this.DESTINATION = $("#txtdest");
    this.HID_DESTINATION = $("#txthiddest");
    this.DEPARTDATE = $("#cal");
    this.DEPARTDATE = $("#txtdate");
    this.HIDDEPARTDATE = $("#hiddepart");
    this.RHIDDEPARTDATE = $("#Rhiddepart");
    this.SPAN = $("#date");
    this.RSPAN = $("#Rdate");
    this.BtnSearch = $("#btnsearch");
    this.PASSENGER = $("#ddlpax");
    this.SEATTYPE = $("#ddlseat");
    this.TripTypeValue = $(".tripbutton1").attr("id");
}
SearchHelperBus.prototype.BindEvents = function () {
    var h = this; var source; var dest;
    //------auto complete for source------//
    if (h.SOURCE.length != 0) {
         
        var autoCity = UrlBase + "BS/WebService/CommonService.asmx/getSourceCity";
        h.SOURCE.autocomplete({
            source: function(request, response) {
                $.ajax({
                    //Absolute Url name
                    url: autoCity,
                    data: "{'srcPrefix': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    asnyc: true,
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            var result = item.src.replace(/%20/g, " ");
                            var hidresult = item.srcID.replace(/%20/g, " ");
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
                h.HID_SOURCE.val(ui.item.id.replace(/%20/g, " "));
                source = ui.item.id.replace(/%20/g, " ");
                h.InsertDest(ui.item.label.replace(/%20/g, " "));

            }
        });
    }
    //-------------------auto search for destination-------------------//

    if (h.DESTINATION.length != 0) {
        var autoCity1 = UrlBase + "BS/WebService/CommonService.asmx/getDestCity";
        h.DESTINATION.autocomplete({
            source: function(request, response) {
                $.ajax({
                    //Absolute Url name
                    url: autoCity1,
                    data: "{'destPrefix': '" + request.term + "', maxResults: 10,'sourceNmae':'" + $("#txtsrc").val() + "' }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    asnyc: true,
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            var result = item.dest.replace(/%20/g, " ");
                            var hidresult = item.destID.replace(/%20/g, " ");
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
                h.HID_DESTINATION.val(ui.item.id.replace(/%20/g, " "));
                dest = ui.item.id.replace(/%20/g, " ");
            }
        });
    }
    SearchHelperBus.prototype.InsertDest = function (id) {
        var h = this;
        var url = UrlBase + "BS/WebService/CommonService.asmx/InsertDestination";
        $.ajax({
            url: url,
            data: "{'srcID':'" + id + "'}",
            dataType: "json", type: "POST",
            contentType: "application/json; charset=utf-8",
            asnyc: true,
            success: function(data) {

            }

        });
    }
    //------------------------------Calender--------------------------------------//

    var date = h.HIDDEPARTDATE.val();
    h.HIDDEPARTDATE.datepicker({
        
        numberOfMonths: 2, dateFormat: "dd/mm/yy", maxDate: "+1y", minDate: 0, showOtherMonths: true, selectOtherMonths: false
    }).datepicker("option", { onSelect: h.UpdateDate }).datepicker("setDate", date.substr(0, 10));

    ////var date = h.RHIDDEPARTDATE.val();
    ////h.RHIDDEPARTDATE.datepicker({
    ////    buttonImage: "images/Calender.png", buttonImageOnly: true,
    ////    showOn: 'button', numberOfMonths: 2, dateFormat: "yy-mm-dd", maxDate: "+1y", minDate: 0, showOtherMonths: true, selectOtherMonths: false
    ////}).datepicker("option", { onSelect: h.RUpdateDate }).datepicker("setDate", date.substr(0, 10));
    //----------------------------------------------------------------------//

    h.BtnSearch.click(function() { return h.validate(); });
}
SearchHelperBus.prototype.validate = function () {
    var h = this;

    if (h.SOURCE.val() == "" && h.HID_SOURCE.val() == "") {
        $("#divtxtsrc").show();
        setTimeout(function() {
            $("#divtxtsrc").fadeOut("slow");
        }, 2000);
        $("#txtsrc").focus();
        return false;
    }
    if (h.SOURCE.val() != "" && h.HID_SOURCE.val() == "") {
        $("#divtxtsrc").show();
        setTimeout(function() {
            $("#divtxtsrc").fadeOut("slow");
        }, 2000);
        $("#txtsrc").focus();
        return false;
    }
    if (h.SOURCE.val() == "" && h.HID_SOURCE.val() != "") {
        $("#divtxtsrc").show();
        setTimeout(function() {
            $("#divtxtsrc").fadeOut("slow");
        }, 2000);
        $("#txtsrc").focus();
        return false;
    }
    if (h.DESTINATION.val() == "" && h.HID_DESTINATION.val() == "") {
        $("#divtxtdest").show();
        setTimeout(function() {
            $("#divtxtdest").fadeOut("slow");
        }, 2000);
        $("#txtdest").focus();
        return false;
    }
    if (h.DESTINATION.val() != "" && h.HID_DESTINATION.val() == "") {
        $("#divtxtdest").show();
        setTimeout(function() {
            $("#divtxtdest").fadeOut("slow");
        }, 2000);
        $("#txtdest").focus();
        return false;
    }
    if (h.DESTINATION.val() == "" && h.HID_DESTINATION.val() != "") {
        $("#divtxtdest").show();
        setTimeout(function() {
            $("#divtxtdest").fadeOut("slow");
        }, 2000);
        $("#txtdest").focus();
        return false;
    }
    if (h.HIDDEPARTDATE.val() == "") {
        var currDate = new Date;
        var searchdate = currDate.getFullYear() + '-' + (currDate.getMonth() + 1) + '-' + (currDate.getDate());
        h.HIDDEPARTDATE.val(searchdate);
    }
    if ($("#ddlseat>option:selected").val() == "") {
        $("#divddlseat").show();
        setTimeout(function() {
            $("#divddlseat").fadeOut("slow");
        }, 2000);
        $("#divddlseat").focus();
        return false;
    }
    if ($("#txtsrc").val() == $("#txtdest").val()) {       
        alert("Source - destination are same please enter valid destinaton");
        $("#txtdest").focus();
        return;
    }

    h.getAvalaibility();
}

///-------------------------searching for journey starts--------------------------//
SearchHelperBus.prototype.getAvalaibility = function () {
    var h = this;
    if (h.RHIDDEPARTDATE.val() == "")
        h.RUpdateDate(h.HIDDEPARTDATE.val(), "")
    var datefmt = h.HIDDEPARTDATE.val();
    var year, month, day, finaldate;
    if (datefmt.indexOf('-') > -1) {
        var year, month, day, finaldate;
        year = datefmt.substr(0, 4);
        month = datefmt.substr(5, 2);
        day = datefmt.substr(8, 2);
        finaldate = year + "-" + month + "-" + day;
    }
    if (datefmt.indexOf('/') > -1) {
        year = datefmt.substr(6, 4);
        month = datefmt.substr(3, 2);
        day = datefmt.substr(0, 2);
        finaldate = year + "-" + month + "-" + day;
    }
    var qString = 'CityId=' + h.HID_SOURCE.val() + '&CityName=' + h.SOURCE.val() + '&DestCityId=' + h.HID_DESTINATION.val() + '&DestCityName=' + h.DESTINATION.val() + '&JourneyDate=' + finaldate + '&Pax=' + '6' + '&Type=' + 'Seat' + '&TripType=' + 'O' + '&ReturnDate=' + finaldate;
    window.location.href = UrlBase + "BS/newBusResultaspx.aspx?" + qString;
}

//----------------------------end--------------------------------------------//
SearchHelperBus.prototype.UpdateDate = function (dateText, inst) {
    var dd = dateText.split('-');
    var selected_date = new Date(dd[1] + '/' + dd[2] + '/' + dd[0]);
    $("#month").html(month[selected_date.getMonth()]);
    $("#day").html(dayName[selected_date.getDay()]);
    $("#date").html($.trim(dd[2]));
    $("#year").html(selected_date.getFullYear());
    $("#hiddepart").val(dateText);
    var newdays = SetnextDate(dateText);
    var rdate = parseFloat(newdays.trim().split('/')[0]);
    var Rmonth = parseFloat(newdays.trim().split('/')[1]);
    if (rdate < 10) { rdate = "0" + rdate; } else { rdate = rdate; }
    if (Rmonth < 10) { Rmonth = "0" + Rmonth; } else { Rmonth = Rmonth; }
    $("#Rmonth").html(month[parseFloat(parseFloat(newdays.trim().split('/')[1])-1)]);
    $("#Rday").html(dayName[parseFloat(parseFloat(newdays.trim().split('/')[0])-1)]);
    $("#Rdate").html(rdate);
    $("#Ryear").html(parseFloat(newdays.trim().split('/')[2]));
    $("#Rhiddepart").val(newdays.trim().split('/')[2] + '-' + Rmonth + '-' + rdate);
    
}
SearchHelperBus.prototype.RUpdateDate = function (dateText, inst) {
    var dd = dateText.split('-');
    var cdat = dateDifference()
    if (cdat < 0) {return false; }
    var selected_date = new Date(dd[1] + '/' + dd[2] + '/' + dd[0]);  
    $("#Rmonth").html(month[selected_date.getMonth()]);
    $("#Rday").html(dayName[selected_date.getDay()]);
    $("#Rdate").html($.trim(dd[2]));
    $("#Ryear").html(selected_date.getFullYear());
    $("#Rhiddepart").val(dateText);
}

function dateDifference() {
    // check if both is not empty
    if ($("#hiddepart").val() == '' || $("#Rhiddepart").val() == '') {
        return;
    }
    else {
        var diff = ($("#Rhiddepart").datepicker("getDate") - $("#hiddepart").datepicker("getDate")) / 1000 / 60 / 60 / 24; // days
        return diff;
    }
  
}

function SetnextDate(dateText) {
    var dddddd = dateText.trim().split('-')[2] + "/" + dateText.trim().split('-')[1] + "/" + dateText.trim().split('-')[0];

    var newdate = dddddd.split("/"); var newdays = "";
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

function getTripType(id) {
  
    if (id == "oneway") {
        $("#return").removeClass("tripbutton1");
        $("#return").addClass("tripbutton2");
        $("#oneway").removeClass("tripbutton2");
        $("#oneway").addClass("tripbutton1");
        $("#RtripId").hide(); $("#RRtripId").hide();    
    }
    else {
        $("#oneway").removeClass("tripbutton1");
        $("#oneway").addClass("tripbutton2");
        $("#return").removeClass("tripbutton2");
        $("#return").addClass("tripbutton1");
        $("#RtripId").show(); $("#RRtripId").show();
    }
}
