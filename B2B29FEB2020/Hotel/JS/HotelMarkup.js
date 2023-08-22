var DateHandler;
$(document).ready(function() {
    DateHandler = new DateHelper();
    DateHandler.BindEvents();
});
//Form Load function End
var DateHelper = function() {
    //  
    this.txtDepDate = $("#From");
    this.txtRetDate = $("#To");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
}
DateHelper.prototype.BindEvents = function() {
    var h = this;
    // Date for Search Booked Hotel and Item Down Lode  Start
    var Depdate = h.hidtxtDepDate.val();
    var returnDate = h.hidtxtRetDate.val();
    //Date Picker Bind
    var dtPickerOptions = { numberOfMonths: 1, dateFormat: "yy-mm-dd", maxDate: 0, minDate: "-1y", showOtherMonths: true, selectOtherMonths: false };
    h.txtDepDate.datepicker(dtPickerOptions).datepicker("option", { onSelect: h.UpdateRoundTripMininumDate }).datepicker("setDate", Depdate.substr(0, 10));
    h.txtRetDate.datepicker(dtPickerOptions).datepicker("setDate", returnDate.substr(0, 10));
    // Date for Search Booked Hotel and Item DownLode End
    // markup Country Search Autocomplet Start
   
    $("#TR_Country").autocomplete({
        source: function(request, response) {
            $.ajax({
                url: HtlUrlBase + "/Hotel/HotelSearchs.asmx/HtlMrkupCityCountryList",
                data: "{ 'city': '', 'Country': '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function(data) { return data; },
                success: function (data) {
                    response($.map(data.d, function(item) {
                        var result = item.Country;
                        var hidresult = item.Country;
                        return { label: result, value: result, id: hidresult }
                    }))
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        autoFocus: true,
        minLength: 3,
        select: function(event, ui) {
            $("#TR_Country1").val(ui.item.id);
            $("#htlCity").val('ALL');
        }
    });
    // markup Country Search Autocomplet End
    //Markup City Search Autocomplet Start
    $("#htlCity").autocomplete({
        source: function(request, response) {
            $.ajax({

                url: HtlUrlBase + "/Hotel/HotelSearchs.asmx/HtlMrkupCityCountryList",
                // data: "{ 'city': '" + request.term + "','country': '" + $('#TR_Country').val() + "', maxResults: 10 }",
                data: "{ 'city': '" + request.term + "', 'Country': '" + $('#TR_Country').val() + "' }",
                dataType: "json", type: "POST",

                contentType: "application/json; charset=utf-8",
                dataFilter: function(data) { return data; },
                success: function(data) {
                    response($.map(data.d, function (item) {
                        
                        return {
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
            $("#htlcitylist").val(ui.item.label);
        }
    });
    //Markup City Search Autocomplet End 
    //Markup Fixd and Percentage Changes Start
    $("input[name$='mrkupType']").click(function() {
        var radio_value = $(this).val();
        $('#ctl00_ContentPlaceHolder1_txtAmt').val("0");
        if (radio_value == 'Fixed') {
            $("#rdbFixed").show("slow");
            $("#no_box").hide();
            $('#lblMrkAmt').text("Markup Amount");
            $('#mrktype').val("Fixed");
        }
        else if (radio_value == 'Percentage') {
            $('#ctl00_ContentPlaceHolder1_txtAmt').val("0");
            $("#rdbPercentage").show("slow");
            $('#lblMrkAmt').text("Markup Percent");
            $('#mrktype').val("Percentage");
            $("#yes_box").hide();
        }
    });
    //Markup Fixd and Percentage Changes End
    //Cash Back ( discount) Fixd and Percentage Changes Start
    $("input[name$='DiscType']").click(function() {
        var radio_value = $(this).val();
        $('#ctl00_ContentPlaceHolder1_txtAmt').val("0");
        if (radio_value == 'Fixed') {
            $("#rdbFixed").show("slow");
            $("#no_box").hide();
            $('#ctl00_ContentPlaceHolder1_lblMrkAmt').val("Discount Amount");
            $('#DiscType').val("Fixed");
        }
        else if (radio_value == 'Percentage') {
            $('#ctl00_ContentPlaceHolder1_txtAmt').val("0");
            $("#rdbPercentage").show("slow");
            $('#ctl00_ContentPlaceHolder1_lblMrkAmt').val("Discount Percent");
            $('#DiscountType').val("Percentage");
            $("#yes_box").hide();
        }
    });
    //Cash Back ( discount) Fixd and Percentage Changes End 
    //
    $("#ctl00_ContentPlaceHolder1_btn_Search").click(function() {
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlhtl').selectedIndex == 0) {
        alert("Please Select Hotel type");
        $("ctl00_ContentPlaceHolder1_ddlhtl").focus();
        return false;
    }
    if ($("#TR_Country1").val() == "" && $("#TR_Country").val() != "ALL") {
        alert("Invalid Country Name");
        $("TR_Country").focus();
        return false;
    }
    if ($("#htlcitylist").val() == "" && $("#htlCity").val() != "ALL") {
        alert("Invalid City Name");
        $("htlCity").focus();
        return false;
    }
    if ($("#Agency").val() == "AdminAgency") {
        if ($("#hidtxtAgencyName").val() == "" && $("#txtAgencyName").val() != "ALL") {
            alert("Invalid Agency Name");
            $("#txtAgencyName").focus();
            return false;
        }
    }
    });
    $("#ctl00_ContentPlaceHolder1_btn_Submit").click(function() {
        
        if (document.getElementById('ctl00_ContentPlaceHolder1_ddlhtl').selectedIndex == 0) {
            alert("Please Select Hotel type");
            $("ctl00_ContentPlaceHolder1_ddlhtl").focus();
            return false;
        }
        if ($("#TR_Country1").val() == "" && $("#TR_Country").val() != "ALL") {
            alert("Invalid Country Name");
            $("TR_Country").focus();
            return false;
        }
        if ($("#htlcitylist").val() == "" && $("#htlCity").val() != "ALL") {
            alert("Invalid City Name");
            $("htlCity").focus();
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_txtAmt").val() == 0) {
            alert("Please Provide Markup Amount");
            $("#ctl00_ContentPlaceHolder1_txtAmt").focus();
            return false;
        }
        if ($("#Agency").val() == "AdminAgency") {
            if ($("#hidtxtAgencyName").val() == "" && $("#txtAgencyName").val() != "ALL") {
                alert("Invalid Agency Name");
                $("#txtAgencyName").focus();
                return false;
            }
        }
    });
}
DateHelper.prototype.UpdateRoundTripMininumDate = function(dateText, inst) {
    DateHandler.txtRetDate.datepicker("option", { minDate: dateText });
}

$(function acb() {
    $("#ctl00_ContentPlaceHolder1_txtAmt").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_txtAmt").val();
        if ($("#mrktype").val() == "Percentage") {
            if (values.indexOf('.') == -1) {
                if ($("#ctl00_ContentPlaceHolder1_txtAmt").val().length > 2) {
                    str = values.substring(0, 2);
                    str2 = values.substring(2, values.length);
                    str3 = str + "." + str2;
                    $('#ctl00_ContentPlaceHolder1_txtAmt').val(str3)
                }
                else {
                    $('#ctl00_ContentPlaceHolder1_txtAmt').val(values)
                }
            }
            else {
                if (values.indexOf('.') != 1) {
                    str = values.substring(0, 2);

                    var str1 = values.indexOf('.') + 1;
                    str2 = values.substring(str1, values.length);
                    str3 = str + "." + str2;
                    $('#ctl00_ContentPlaceHolder1_txtAmt').val(str3)
                }
            }
        }
        else {
            $('#ctl00_ContentPlaceHolder1_txtAmt').val(values)
        }
    });
});
// Sow and hide date in Item DownLode start
function ShowHidedates(chk) {

    if (chk == 'rdbFullDownLoad' || chk == 'rdbYestDownload') {
        document.getElementById("datestbl").style.display = 'none';
        document.getElementById("douwnloadeType").value = chk;
    }
    else {
        document.getElementById("datestbl").style.display = 'block';
        document.getElementById("douwnloadeType").value = chk;
    }
}

function ShowWaitImage() {
    if (confirm("Are you sure for download!")) {
        document.getElementById("div_Submit").style.display = "none";
        document.getElementById("div_Progress").style.display = "block";
        return true;
    }
    else {
        return false;
    }
}
// Sow and hide Item DownLode end

function ShowHidTGDownlodeBtn(chk) {
    if (chk == 'TGDownLoad') {
        $("#ctl00_ContentPlaceHolder1_DownloadTravelGurudata").show();
        $("#ctl00_ContentPlaceHolder1_InsertTGData").hide();
        $("#txtpath").hide();
    }
    else {
        $("#ctl00_ContentPlaceHolder1_DownloadTravelGurudata").hide();
        $("#ctl00_ContentPlaceHolder1_InsertTGData").show();
        $("#txtpath").show();
    }
}
function ShowWaitImage1() {
    if (confirm("Are you sure for download!")) {
        $("ctl00_ContentPlaceHolder1_DownloadTravelGurudata").hide();
        $("TGRecords").show();
        return true;
    }
    else {
        return false;
    }
}

function ShowWaitImage2() {
    if (confirm("Are you sure for Insert Data!")) {
        $("#ctl00_ContentPlaceHolder1_InsertTGData").hide();
        $("#TGRecords").show();
        return true;
    }
    else {
        return false;
    }
}

function MarkupValidation() {
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlhtl').selectedIndex == 0) {
        alert("Please Select Hotel type");
        $("ctl00_ContentPlaceHolder1_ddlhtl").focus();
        return false;
    }
    if ($("#TR_Country1").val() == "" && $("#TR_Country").val() != "ALL") {
        alert("Invalid Country Name");
        $("TR_Country").focus();
        return false;
    }
    if ($("#htlcitylist").val() == "" && $("#htlCity").val() != "ALL") {
        alert("Invalid City Name");
        $("htlCity").focus();
        return false;
    }

    if ($("#ctl00_ContentPlaceHolder1_txtAmt").val() == 0) {
        alert("Please Provide Markup Amount");
        $("#ctl00_ContentPlaceHolder1_txtAmt").focus();
        return false;
    }
    if ($("#Agency").val() == "AdminAgency") {
        if ($("#hidtxtAgencyName").val() == "" && $("#txtAgencyName").val() != "ALL") {
            alert("Invalid Agency Name");
            $("#txtAgencyName").focus();
            return false;
        }
    }
}

function focusObj(obj) {
    if (obj.value == "ALL") obj.value = "";
}

function blurObj(obj) {
    if (obj.value == "") obj.value = "ALL";
}

function SetCountryAll() {
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlhtl').selectedIndex == 2 && $("#TR_Country").val() != 'India') {
        $("#TR_Country").val('India'); $("#TR_Country1").val('India');
    }
    else if (document.getElementById('ctl00_ContentPlaceHolder1_ddlhtl').selectedIndex == 1 && $("#TR_Country").val() == 'India') {
        $("#TR_Country").val('ALL');
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 48 && charCode <= 57 || charCode == 08 || charCode == 46) {
        return true;
    }
    else {

        return false;
    }
}
function isCharKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
        return true;
    }
    else {

        return false;
    }
}


function checkit(evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8)) {
        return false;
    }
    status = "";
    return true;
}