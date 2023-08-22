
var carLayout1;
var AirportHandler;
var mainArrCity;
var returndate;

$(document).ready(function() {
    AirportHandler = new AirportSearchHelper();

    AirportHandler.BindEvents();
});
var AirportSearchHelper = function() {
    this.Tfrom1 = $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1");
    this.hidTfrom1 = $('#hidTfrom1');
    this.hiddest = $('#hiddest');
    this.HiddenResort = $('#HiddenResort');
    this.Tfrom2 = $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom2");
    this.hidTfrom2 = $("#hidTfrom2");
    this.Tto1 = $("#ctl00_ContentPlaceHolder1_Transearch_Tto1");
    this.hidTto1 = $("#hidTto1");
    this.TDeptdate1 = $("#TDeptdate1");
    this.hidTDeptdate1 = $("#hidTDeptdate1");
    this.TReturn1 = $("#TReturn1");
    this.hidTReturn1 = $("#hidTReturn1");
    this.Tsubmit = $("#ctl00_ContentPlaceHolder1_Transearch_Tsubmit");
    this.ARdatetxt1 = $("#ctl00_ContentPlaceHolder1_Transearch_ARdatetxt1");
    this.depdate = $("#ctl00_ContentPlaceHolder1_Transearch_depdate");
}
AirportSearchHelper.prototype.BindEvents = function() {

    var h = this;
    var t = h.hidTDeptdate1.val();
    var r = {
        numberOfMonths: 1,
        dateFormat: "dd/mm/yy",
        maxDate: "+1y",
        minDate: 0,
        showOtherMonths: true,
        selectOtherMonths: false
    };


    h.TDeptdate1.datepicker(r).datepicker("option", {
        onSelect: h.UpdateRoundTripMininumDate
    }).datepicker("setDate", t.substr(0, 10));
    h.TReturn1.datepicker(r).datepicker("setDate", t.substr(0, 10));
    h.ARdatetxt1.datepicker(r).datepicker("option", {
        onSelect: h.UpdateRoundTripMininumDate
    }).datepicker("setDate", t.substr(0, 10));
    h.depdate.datepicker(r).datepicker("setDate", t.substr(0, 10));

    //   var autoAirportName = UrlBase + "Transfer/A2BService.asmx/FetchAirportList";
    var autoAirportName = "http://b2b.ITZ.com/transfer/a2bservice.asmx/FetchAirportList"; //http://b2b.ITZ.com/transfer/a2bservice.asmx
    var h = this; alert(autoAirportName);
    h.Tfrom1.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoAirportName,
                data: JSON.stringify({ code: request.term }),
                dataType: "jsonp", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var AirportName = item.AirportName + "(" + item.CountryName + ")";
                        var CountryName = item.CountryName;
                        var destID = item.dest;
                        return { label: AirportName, value: AirportName, id: destID }
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
            
            h.hidTfrom1.val(ui.item.value);
            h.hiddest.val(ui.item.id);
        }
    });

    h.Tto1.autocomplete({
        source: function(request, response) {
            //            if (mainArrCity.length == 0)
            getallcityListNew(h);
            var txt = $("#hidTfrom1").val().substring(0, $("#hidTfrom1").val().lastIndexOf('('));
            var desttxt = $("#hiddest").val();

            $.ajax({
                url: UrlBase + "Transfer/A2BService.asmx/FetchResortList",

                data: JSON.stringify({ code: request.term, dest: desttxt, Couna: txt }),
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var ResortName = item.ResortName + "(" + item.CountryName + ")";
                        var CountryName = item.CountryName;
                        return { label: ResortName, value: ResortName, id: CountryName }
                    }))
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {

                    alert(textStatus);
                }
            })
        },
        autoFocus: true,
        minLength: 1,
        select: function(event, ui) {
            h.hidTto1.val(ui.item.id);
        }
    });
   
    h.Tto1.click(function() {
        if (h.Tto1.val() == "") {
            // getallcityList(h);
            getallcityListNew(h);
        }
    });
    
    var autoResortN = UrlBase + "Transfer/A2BService.asmx/FetchResortSearchList";
    h.Tfrom2.autocomplete({
        source: function(request, response) {
            $.ajax({
                url: autoResortN,
                data: JSON.stringify({ code: request.term }),
                dataType: "json", type: "POST",
                contentType: "application/json; charset=utf-8",
                asnyc: true,
                success: function(data) {
                    response($.map(data.d, function(item) {
                        var ResortName = item.ResortName + "(" + item.CountryName + ")";
                        var CountryName = item.CountryName;
                        var ResortID = item.ResortID;
                        return { label: ResortName, value: ResortName, id: ResortID }
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
            h.hidTfrom2.val(ui.item.id);
            h.HiddenResort.val(ui.item.value);
        }
    });
}
$(document).on("click", "#alllservice", function(e) {
    $(".clsservices").toggleClass("hide");
    $(".sh").toggleClass("hide");
});

$("#abcdf").click(function() {

    abcd()
});
function abcd() {
    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton1")[0].checked == true) {
        if (document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom1").value == "") {
            alert('Please Fill Destination airport');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom1").focus();
            return false;

        }
       
            if (document.getElementById("hidTfrom1").value == "") {

                alert('Incorrect Airport Name');
                document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom1").focus();
                return false;
            }
        
        if (document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tto1").value == "") {
            alert('Please Fill Resort Name');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tto1").focus();
            return false;

        }
        if (document.getElementById("hidTto1").value == "") {

            alert('Incorrect Resort Name');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tto1").focus();
            return false;
        }
//        if (document.getElementById("hiddest").value == "") {

//            alert('Incorrect Resort Name');
        //            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tto1").focus();HiddenResort
//            return false;
//        }
    }
    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton2")[0].checked == true) {
    
        if (document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").value == "") {
            alert('Please Fill Destination resort');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").focus();
            return false;

        }
        if (document.getElementById("hidTfrom2").value == "") {

            alert('Incorrect Resort Name');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").focus();
            return false;
        }
        if (document.getElementById("HiddenResort").value == "" || document.getElementById("HiddenResort").value != document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").value) {

            alert('Incorrect Resort Name');
            document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").focus();
            return false;
        }
    }

    if (document.getElementById("TDeptdate1").value == "") {
        document.getElementById("TDeptdate1").focus();
        alert("please select Arrivaldate.");
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4")[0].checked == true) {
        if (document.getElementById("TReturn1").value == "") {
            document.getElementById("TReturn1").focus();
            alert("please select Departdate.");
            return false;
        }
    }

    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4")[0].checked == true) {
        returndate = document.getElementById("TReturn1").value

    }
    else {
        returndate = document.getElementById("TDeptdate1").value
    }
    // AirportSearchHelper.getAvalaibility();
    getAvalaibility();
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

AirportSearchHelper.prototype.UpdateRoundTripMininumDate = function(h, t) {
    AirportHandler.TReturn1.datepicker("option", {
        minDate: h

    });
}
function getAvalaibility(ret) {
    var h = this;
    //    if (h.RHIDDEPARTDATE.val() == "")
    //        h.RUpdateDate(h.HIDDEPARTDATE.val(), "")
    var Triptype;
    var Requesttype;
    var destination;
    var destination2;
    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton1")[0].checked == true) {
        destination = document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom1").value;
        destination2 = document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tto1").value;
    }
    else {
        destination = $("#hidTfrom2").val();
        destination2 = document.getElementById("ctl00_ContentPlaceHolder1_Transearch_Tfrom2").value;
    }

    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4")[0].checked == true) {
        Triptype = "RoundTrip";

    }
    else {
        Triptype = "Oneway";
    }
    if ($("#ctl00_ContentPlaceHolder1_Transearch_RadioButton1")[0].checked == true) {
        Requesttype = "Airport";

    }
    else {
        Requesttype = "Resort";

    }
    var qString = 'Destination=' + destination + '&Arivaldate=' + document.getElementById("TDeptdate1").value + '&returndate=' + returndate + '&Triptype=' + Triptype + '&Requesttype=' + Requesttype + '&Adult=' + parseInt($.trim($("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Adult>option:selected").text())) + '&child=' + parseInt($.trim($("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Child>option:selected").text())) + '&infant=' + parseInt($.trim($("#ctl00_ContentPlaceHolder1_Transearch_Tddl_Infrant>option:selected").text())) + '&To=' + destination2;
    window.location.href = UrlBase + "Transfer/Transfersearch.aspx?" + qString;
}


function Hide(obj) {

    if (obj.checked) {
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb1").hide();
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul1").hide();
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb2").show();
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul2").show();
        $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom2").val("");
        $("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4").attr("disabled", false);
      
    }
}
function Show(obj) {

    if (obj.checked) {
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb1").show();
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul1").show();
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb2").hide();
        $("#ctl00_ContentPlaceHolder1_Transearch_Ttrmul2").hide();
        $("#ctl00_ContentPlaceHolder1_Transearch_Tfrom1").val("");
        $("#ctl00_ContentPlaceHolder1_Transearch_Tto1").val("");
        $("#ctl00_ContentPlaceHolder1_Transearch_RadioButton4").attr("disabled", true);
        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").hide();
        $("#TReturn1").val("");
    }
}
function TripHide(obj) {

    if (obj.checked) {
        //        $("#ctl00_ContentPlaceHolder1_Transearch_Tvb1").hide();
        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").hide();
        $("#TReturn1").val("");

    }
}
function TripShow(obj) {

    if (obj.checked) {

        $("#ctl00_ContentPlaceHolder1_Transearch_rtd").show();


    }
}
function isNumberKey(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode >= 48 && charCode <= 57 || charCode == 08) {
        return true;
    }
    else {
        return false;
    }
}
function isCharKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode == 32 || charCode == 08) {
        return true;
    }
    else {
        return false;
    }
}
function getKeyCode(e) {
    if (window.event)
        return window.event.keyCode;
    else if (e)
        return e.which;
    else
        return null;
}

function keyRestrict(e, validchars) {
    var key = '', keychar = '';
    key = getKeyCode(e);
    if (key == null) return true;
    keychar = String.fromCharCode(key);
    keychar = keychar.toLowerCase();
    validchars = validchars.toLowerCase();
    if (validchars.indexOf(keychar) != -1)
        return true;
    if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
        return true;
    return false;
}
