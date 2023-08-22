var RHandler;
var resultG = new Array();
var Obook = null;
var Rbook = null;

var ObookSFM = null;
var RbookSFM = null;
var divclspmatrix = 0;
var rtfResult = null;
var lccRtfResult = null;
var gdsRtfResult = null;
var rtfResultArray = null;
var lccRtfResultArray = null;
var gdsRtfResultArray = null;
var CommonResultArray = new Array();
var CommanRTFArray = new Array();
var rStatus = 0;
var isSRF = false;
var totprevFare = 0;
var totcurrentFare = 0;
var totcurrentFareSFM = 0;

var ORTFFare = 0;
var RRTFFare = 0;
var ORTFLineNo = '';
var RRTFLineNo = '';
var ORTFVC = '';
var RRTFVC = '';
var roundtripNrml1 = false;


var applyfilterStatus = false;

//Devesh SRF
var gdsJsonRTF = new Array();
var RTFFirstTime = 0;
var SRFResultAfterReprice = new Array();
var SRFReprice = false;

//Devesh SRF END

$(document).ready(function () {
    RHandler = new ResHelper;
    RHandler.BindEvents()

    // Book Button Show Hide 12-02-2020
    //$(document).on("click", "input[name='radioPrice']", function (e) {
    //    debugger;
    //    $('.falsebookbutton').show();
    //    $('.buttonfltbk').hide();
    //    var test = $(this).val();
    //    var titlearray = this.title.split('_');
    //    var targetbox = $(this).attr('class');
    //    // _faredetailmasterall
    //    // _faredetailmaster
    //    // _master
    //    //$('.' + this.title + '_faredetailmasterall').hide();
    //    //$('#' + this.id + '_faredetailmaster').hide();
    //    $('#' + this.value + '_master').show();
    //    //$('#' + this.value + '_faredetailmaster').hide();
    //    // $('#' + this.id.replace("_radio", "")).show()
    //    $('#' + this.value).show()
    //    $('#' + this.value + '_falsebookbutton').hide()
    //    if (titlearray[1] == "R") {
    //        $('.' + titlearray[0] + '_faredetailmasterall').hide();
    //        $('.' + titlearray[0] + '_R').hide();
    //        $('#' + this.value + '_master_R').show();
    //    }
    //    $('.' + this.title).hide();

    //    SearchRbtnLineNo = "";
    //    //alert(this.id.replace("_radio", "Det"));
    //    //alert(this.value + '-Title- ' + this.title);

    //    //1apiSGNRMLITZNRMLDet
    //});
    //End Book Button Show Hide 12-02-2020
});

$(document).ready(function () {
    $("#ModifySearch").click(function () {
        $("#Modsearch").slideDown();
    });
    $("#mdclose").click(function () {
        $("#Modsearch").slideUp();
    });
    //$(document).on('click', "[data-target]", function () {
    //    var ids = this.id;
    //    var cls = $(this).attr("class");
    //    if (cls == "gridViewToolTip") {


    //        setfare(ids.replace("_ALL", "_flt"), ids.replace("_ALL", ""));

    //    }

    //    //#3apiAKNRMLITZNRML_Fare
    //    //3apiAKNRMLITZNRML_ALL class=
    //});
});


function DiplayMsearch1(id) {

    $("#" + id).show();
}
function DiplayMsearch(obj) {

    //$(obj).parent().parent().parent().fadeToggle(1000);



    $('.fade').each(function () {

        $(this).hide();

    });

    //if (obj.id != null) {
    //    //$("#" + obj.id).fadeToggle(1000);
    //    $("#" + obj.id).html("");
    //    //var bb = $("#" + obj.id);
    //    //for (var i = 0; i < bb.length; i++) {

    //    //    $($(obj[i])[0]).hide();
    //    //}
    //    //$("#" + obj.id).each(function () {
    //    //    $(this).hide();

    //    //});
    //}
    //else {

    //    for (var i = 0; i < obj.length; i++) {

    //       $($(obj[i])[0]).hide();
    //    }

    //$("#" + obj).each(function () {
    //    $(this).fadeToggle(1000)

    //});
    // $("#" + obj).fadeToggle(1000);
    //}
}

function mtrxx() {



    if (Obook != null) {
        $('.list-item').each(function () {

            $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
            $(this).find('.mrgbtmG').removeClass("fltbox02");

        });


        var linenums = Obook[0].LineNumber.split('api')[0];
        $('#main_' + linenums + '_O').removeClass("fltbox").addClass("fltbox01");
        $('#main1_' + linenums + '_O').addClass("fltbox02");




        if ($('#main_' + linenums + '_O').find("input[type='radio']").attr('checked') != "checked") {
            $('#main_' + linenums + '_O').find("input[type='radio']").attr('checked', 'checked');
        }
        if ($('#main1_' + linenums + '_O').find("input[type='radio']").attr('checked') != "checked") {
            $('#main1_' + linenums + '_O').find("input[type='radio']").attr('checked', 'checked');
        }
    }


    if (Rbook != null) {
        $('.list-itemR').each(function () {

            $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
            $(this).find('.mrgbtmG').removeClass("fltbox02");
        });
        var linenumsR = Rbook[0].LineNumber.split('api1')[0];
        $('#main_' + linenumsR + '_R').removeClass("fltbox").addClass("fltbox01");
        $('#main_' + linenumsR + '_R').find("input[type='radio']").attr('checked', 'checked');
        $('#main1_' + linenumsR + '_R').addClass("fltbox02");
        $('#main1_' + linenumsR + '_R').find("input[type='radio']").attr('checked', 'checked');
    }









}

var ResHelper = function () {
    this.flight = $("flight");
    this.txtDepCity1 = $("#txtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.btnSearch = $("#btnSearch");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    this.rooms = $("#rooms");
    this.rdbOneWay = $("#rdbOneWay");
    this.rdbRoundTrip = $("#rdbRoundTrip");
    this.txtDepDate = $("#txtDepDate");
    this.txtRetDate = $("#txtRetDate");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
    this.trRetDateRow = $("#trRetDateRow");
    this.TripType = $("input[name=TripType]:checked");
    this.Adult = $("#Adult");
    this.Child = $("#Child");
    this.Infant = $("#Infant");
    $("#sapnTotPax").val(parseInt($(this.Adult).val()) + parseInt($(this.Child).val()) + parseInt($(this.Infant).val()) + " Traveller");
    this.Cabin = $("#Cabin");
    this.txtAirline = $("#txtAirline");
    this.hidtxtAirline = $("#hidtxtAirline");
    this.chkNonstop = $("#chkNonstop");
    this.chkAdvSearch = $("#chkAdvSearch");
    this.trAdvSearchRow = $("#trAdvSearchRow");
    this.LCC_RTF = $("#LCC_RTF");
    this.GDS_RTF = $("#GDS_RTF");
    this.DivResult = $("#divResult");
    this.searchquery = $("#searchquery");
    this.airlineFilter = $("#airlineFilter");
    this.stopFilter = $("#stopFlter");
    this.airlineFilterR = $("#airlineFilterR");
    this.stopFilterR = $("#stopFlterR");
    this.divFromR = $("#divFrom1");
    this.divToR = $("#divTo1");
    this.RoundTripH = $("#RoundTripH");
    this.OnewayH = $("#onewayH");
    this.MainSF = $("#MainSF");
    this.MainSFR = $("#MainSFR");
    this.flterO = $(".fo");
    this.flterR = $(".fr");
    this.flterTab = $("#flterTab");
    this.flterTabO = $("#flterTabO");
    this.flterTabR = $("#flterTabR");
    this.RadioSelect = $("input:radio");
    this.RtfFltSelectDiv = $("#fltselct");
    this.RtfFltSelectDivO = $("#fltgo");
    this.RtfFltSelectDivR = $("#fltbk");
    this.RtfTotalPayDiv = $("#totalPay");
    this.RtfBookBtn = $("#FinalBook");
    this.PrevDaySrch = $("#PrevDay");
    this.NextDaySrch = $("#NextDay");
    this.SearchTextDiv = $("#divSearchText");
    this.RTFTextFrom = $("#RTFTextFrom");
    this.RTFTextTo = $("#RTFTextTo");
    this.DivMatrix = $("#divMatrix");
    this.DivMatrixRtfO = $("#divMatrixRtfO");
    this.DivMatrixRtfR = $("#divMatrixRtfR");
    this.DivRefinetitle = $("#refinetitle");
    this.SearchTextDiv1 = $("#divSearchText1");
    //this.matrix1 = $('.matrix');
    this.fltbox12 = $('.fltbox');
    this.clspMatrix = $('#clspMatrix');
    this.divMatrix = $('.divMatrix');
    this.divMatrixO = $('#divMatrix');
    this.hdnOnewayOrRound = $("#hdnOnewayOrRound");
    this.RtfFromPrevDay = $("#RtfFromPrevDay");
    this.RtfFromNextDay = $("#RtfFromNextDay");
    this.RtfToPrevDay = $("#RtfToPrevDay");
    this.RtfToNextDay = $("#RtfToNextDay");
    this.prexnt = $("#prexnt");
    this.rtfResultDiv = $("#rtfResultDiv");
    this.lccRTFDiv = $("#lccRTFDiv");
    this.gdsRTFDiv = $("#gdsRTFDiv");
    this.divFromResult = $("#divFrom");
    this.fltrDiv = $("#lftdv1");
    this.DivLoadP = $("#DivLoadP");
    this.DivColExpnd = $("#lftdv");
    this.DisplaySearchinput = $('#displaySearchinput');
    this.nextdate = $('#next-date');
    this.prevdate = $('#prev-date');
    this.ondt = $('#on-dt');
    this.rndt = $('#rn-dt');
    this.rpdt = $('#rp-dt');
    this.opdt = $('#op-dt');

    //For MultiCity 27-12-2016


    //this.txtDepDate1 = $("#txtDepDate1");
    //this.hidtxtDepDate1 = $("#hidtxtDepDate1");

    this.txtDepCity2 = $("#txtDepCity2");
    this.hidtxtDepCity2 = $("#hidtxtDepCity2");
    this.txtArrCity2 = $("#txtArrCity2");
    this.hidtxtArrCity2 = $("#hidtxtArrCity2");
    this.txtDepDate2 = $("#txtDepDate2");
    //this.hidtxtDepDate2 = $("#hidtxtDepDate2");


    this.txtDepCity3 = $("#txtDepCity3");
    this.hidtxtDepCity3 = $("#hidtxtDepCity3");
    this.txtArrCity3 = $("#txtArrCity3");
    this.hidtxtArrCity3 = $("#hidtxtArrCity3");
    this.txtDepDate3 = $("#txtDepDate3");
    //this.hidtxtDepDate3 = $("#hidtxtDepDate3");

    this.txtDepCity4 = $("#txtDepCity4");
    this.hidtxtDepCity4 = $("#hidtxtDepCity4");
    this.txtArrCity4 = $("#txtArrCity4");
    this.hidtxtArrCity4 = $("#hidtxtArrCity4");
    this.txtDepDate4 = $("#txtDepDate4");
    //this.hidtxtDepDate4 = $("#hidtxtDepDate4");

    this.txtDepCity5 = $("#txtDepCity5");
    this.hidtxtDepCity5 = $("#hidtxtDepCity5");
    this.txtArrCity5 = $("#txtArrCity5");
    this.hidtxtArrCity5 = $("#hidtxtArrCity5");
    this.txtDepDate5 = $("#txtDepDate5");
    //this.hidtxtDepDate5 = $("#hidtxtDepDate5");

    this.txtDepCity6 = $("#txtDepCity6");
    this.hidtxtDepCity6 = $("#hidtxtDepCity6");
    this.txtArrCity6 = $("#txtArrCity6");
    this.hidtxtArrCity6 = $("#hidtxtArrCity6");
    this.txtDepDate6 = $("#txtDepDate6");
    //this.hidtxtDepDate6 = $("#hidtxtDepDate6");

    this.AirlineFareType = $("#AirlineFareType");
    this.AirlineFareTypeR = $("#AirlineFareTypeR");


};
ResHelper.prototype.BindEvents = function () {
    var e = this;
    e.GetResult(e)
    e.eventFn();
    e.NextPrevSearch(e);
};

ResHelper.prototype.GetCommomTimeForFilter = function (time) {

    var timestr = "";
    var h = parseInt(time.substring(0, 2));
    var m = parseInt(time.substring(2, 4));
    if (h >= 0 && (h <= 6)) {
        if (h == 6 && m > 0) {
            timestr = "6_12";
        }
        else {
            timestr = "0_6";
        }

    }
    else if ((h >= 6) && (h <= 12)) {
        if (h == 12 && m > 0) {
            timestr = "12_18";
        }
        else {
            timestr = "6_12";
        }
    }
    if ((h >= 12 && m > 0) && (h <= 18)) {


        if (h == 18 && m > 0) {
            timestr = "18_0";
        }
        else {
            timestr = "12_18";
        }
    }
    if (h >= 18) {



        if (h == 18 && m == 0) {
            timestr = "12_18";
        }
        else {

            timestr = "18_0";
        }
    }
    return timestr;
}
ResHelper.prototype.NextPrevSearch = function (e) {


    e.RtfToNextDay.click(function (event) {
        event.preventDefault();

        var QSHandler = new QSHelper();

        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');
        var arrDate = new Date(parseFloat(ArrivaldateArr[2]), (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) + 1));
        var arrDay;
        if (arrDate.getDate() < 10) {
            arrDay = "0" + arrDate.getDate();
        }
        else {
            arrDay = arrDate.getDate();
        }
        var arrM = parseInt(arrDate.getMonth() + 1);
        if (arrM < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }
        //if (arrDate.getMonth() < 10) {
        //   arrM = "0" + arrM;
        // }
        //  else {
        //      arrM = arrM;
        //  }
        e.txtRetDate.val(arrDay + '/' + arrM + '/' + arrDate.getFullYear());
        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + qarray.txtDepDate + '&txtRetDate=' + e.txtRetDate.val();
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") { window.location.href = UrlBase + 'Flightdom/FltResult.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'FlightInt/FltResIntl.aspx?' + dataString; };


    });

    e.RtfToPrevDay.click(function (event) {
        event.preventDefault();

        var QSHandler = new QSHelper();
        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');

        var arrDate = new Date(parseFloat(ArrivaldateArr[2]), (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) - 1));
        var arrDay;

        if (arrDate.getDate() < 10) {
            arrDay = "0" + arrDate.getDate();
        }
        else {
            arrDay = arrDate.getDate();
        }


        var arrM = parseInt(arrDate.getMonth() + 1);
        if (arrM < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }

        //if (arrDate.getMonth() < 10) {
        //    arrM = "0" + arrM;
        // }
        // else {
        //     arrM = arrM;
        // }

        e.txtRetDate.val(arrDay + '/' + arrM + '/' + arrDate.getFullYear());
        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + qarray.txtDepDate + '&txtRetDate=' + e.txtRetDate.val();
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") { window.location.href = UrlBase + 'Flightdom/FltResult.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'FlightInt/FltResIntl.aspx?' + dataString; };

    });



    e.RtfFromNextDay.click(function (event) {
        event.preventDefault();

        var QSHandler = new QSHelper();

        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');
        var depdate = new Date(parseFloat(depdateArr[2]), (parseFloat(depdateArr[1]) - 1), (parseFloat(depdateArr[0]) + 1));
        var depday;
        if (depdate.getDate() < 10) {
            depday = "0" + depdate.getDate();
        }
        else {
            depday = depdate.getDate();
        }

        var depM = parseInt(depdate.getMonth() + 1);

        if (depM < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }

        // if (depdate.getMonth() < 10) {
        //     depM = "0" + depM;
        // }
        // else {
        //    depM = depM;
        //}


        e.txtDepDate.val(depday + '/' + depM + '/' + depdate.getFullYear());

        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + e.txtDepDate.val() + '&txtRetDate=' + qarray.txtRetDate;
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };

    });

    e.RtfFromPrevDay.click(function (event) {
        event.preventDefault();

        var QSHandler = new QSHelper();

        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');
        var depdate = new Date(parseFloat(depdateArr[2]), (parseFloat(depdateArr[1]) - 1), (parseFloat(depdateArr[0]) - 1));

        var depday;

        if (depdate.getDate() < 10) {
            depday = "0" + depdate.getDate();
        }
        else {
            depday = depdate.getDate();
        }

        var depM = parseInt(depdate.getMonth() + 1);

        if (depM < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }

        // if (depdate.getMonth() < 10) {
        //     depM = "0" + depM;
        // }
        // else {
        //    depM = depM;
        // }


        e.txtDepDate.val(depday + '/' + depM + '/' + depdate.getFullYear());

        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + e.txtDepDate.val() + '&txtRetDate=' + qarray.txtRetDate;
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };

    });






    e.PrevDaySrch.click(function (event) {
        event.preventDefault();

        var QSHandler = new QSHelper();

        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');
        var depdate = new Date(parseFloat(depdateArr[2]), (parseFloat(depdateArr[1]) - 1), (parseFloat(depdateArr[0]) - 1));
        var arrDate = new Date(parseFloat(ArrivaldateArr[2]), (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) - 1));
        var arrDay;
        var depday;

        if (depdate.getDate() < 10) {
            depday = "0" + depdate.getDate();
        }
        else {
            depday = depdate.getDate();
        }

        if (arrDate.getDate() < 10) {
            arrDay = "0" + arrDate.getDate();
        }
        else {
            arrDay = arrDate.getDate();
        }

        var depM = parseInt(depdate.getMonth() + 1);
        var arrM = parseInt(arrDate.getMonth() + 1);
        //if (depdate.getMonth() < 10) {
        //    depM = "0" + depM;
        //}
        //else {
        //    depM = depM;
        //}
        //if (arrDate.getMonth() < 10) {
        //    arrM = "0" + arrM;
        //}
        //else {
        //    arrM = arrM;
        //}

        if (depM < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }
        if (arrM < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }

        e.txtDepDate.val(depday + '/' + depM + '/' + depdate.getFullYear());
        e.txtRetDate.val(arrDay + '/' + arrM + '/' + arrDate.getFullYear());
        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }
        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + e.txtDepDate.val() + '&txtRetDate=' + e.txtRetDate.val();
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") {
            ////window.location.href = UrlBase + 'Flightdom/FltResult.aspx?' + dataString;
            try {
                $.ajax({
                    url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                    data: "{ 'name': 'Domestic/Result.aspx'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var resUm = data.d;
                        if (resUm != null && resUm != "") {
                            window.location.href = UrlBase + resUm + "?" + s
                        }
                        else {
                            window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                        }
                    },
                    error: function (e, t, n) {
                        // alert(t)
                        window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
            }
        }
        else if (Trip == "I") {
            ////window.location.href = UrlBase + 'FlightInt/FltResIntl.aspx?' + dataString;
            try {
                $.ajax({
                    url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                    data: "{ 'name': 'International/FltResIntl.aspx'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var resUm = data.d;
                        if (resUm != null && resUm != "") {
                            window.location.href = UrlBase + resUm + "?" + dataString
                        }
                        else {
                            window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
                        }
                    },
                    error: function (e, t, n) {
                        // alert(t)
                        window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
            }
        };
    });

    e.NextDaySrch.click(function (event) {
        event.preventDefault();

        var qarray = QSHandler.queryStr();
        var depdateArr = qarray.txtDepDate.split('/');
        var ArrivaldateArr = qarray.txtRetDate.split('/');
        var depdate = new Date(parseFloat(depdateArr[2]), (parseFloat(depdateArr[1]) - 1), (parseFloat(depdateArr[0]) + 1));

        var arrDate = new Date(parseFloat(ArrivaldateArr[2]), (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) + 1));

        var arrDay;
        var depday;

        if (depdate.getDate() < 10) {
            depday = "0" + depdate.getDate();
        }
        else {
            depday = depdate.getDate();
        }

        if (arrDate.getDate() < 10) {
            arrDay = "0" + arrDate.getDate();
        }
        else {
            arrDay = arrDate.getDate();
        }
        var depM = parseInt(depdate.getMonth() + 1);
        var arrM = parseInt(arrDate.getMonth() + 1);
        //if (depdate.getMonth() < 10) {
        //    depM = "0" + depM;
        //}
        //else {
        //    depM = depM;
        //}

        //if (arrDate.getMonth() < 10) {
        //    arrM = "0" + arrM;
        //}
        //else {
        //    arrM = arrM;
        //}


        if (depM < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }

        if (arrM < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }

        e.txtDepDate.val(depday + '/' + depM + '/' + depdate.getFullYear());
        e.txtRetDate.val(arrDay + '/' + arrM + '/' + arrDate.getFullYear());
        var Trip;
        var i = e.hidtxtDepCity1.val().split(",");
        var s = e.hidtxtArrCity1.val().split(",");
        if (i[1] == "IN" && s[1] == "IN") {
            Trip = "D"
        } else {
            Trip = "I"
        }

        var o = new Boolean;
        if (e.chkNonstop.is(":checked") == true) {
            o = true
        } else {
            o = false
        }
        var u = new Boolean;
        if (e.LCC_RTF.is(":checked") == true) {
            u = true
        } else {
            u = false
        }
        var a = new Boolean;
        if (e.GDS_RTF.is(":checked") == true) {
            a = true
        } else {
            a = false
        }
        var r = $("input[name='TripType']:checked").val();

        var dataString = 'TripType=' + r + '&txtDepCity1=' + e.txtDepCity1.val() + '&txtArrCity1=' + e.txtArrCity1.val() + '&hidtxtDepCity1=' + e.hidtxtDepCity1.val() + '&hidtxtArrCity1=' + e.hidtxtArrCity1.val() + '&Adult=' + e.Adult.val();
        dataString += '&Child=' + e.Child.val() + '&Infant=' + e.Infant.val() + '&Cabin=' + e.Cabin.val() + '&txtAirline=' + e.txtAirline.val() + '&hidtxtAirline=' + e.hidtxtAirline.val() + '&txtDepDate=' + e.txtDepDate.val() + '&txtRetDate=' + e.txtRetDate.val();
        dataString += '&Nstop=' + o + '&RTF=' + u + '&Trip=' + Trip + '&GRTF=' + a

        document.getElementById('__VIEWSTATE').name = 'NOVIEWSTATE';
        if (Trip == "D") {
            ////window.location.href = UrlBase + 'Flightdom/FltResult.aspx?' + dataString;
            try {
                $.ajax({
                    url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                    data: "{ 'name': 'Domestic/Result.aspx'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var resUm = data.d;
                        if (resUm != null && resUm != "") {
                            window.location.href = UrlBase + resUm + "?" + s
                        }
                        else {
                            window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                        }
                    },
                    error: function (e, t, n) {
                        // alert(t)
                        window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
            }
        }
        else if (Trip == "I") {
            ////window.location.href = UrlBase + 'FlightInt/FltResIntl.aspx?' + dataString;
            try {
                $.ajax({
                    url: UrlBase + "FltSearch1.asmx/GetMUForPage",
                    data: "{ 'name': 'International/FltResIntl.aspx'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var resUm = data.d;
                        if (resUm != null && resUm != "") {
                            window.location.href = UrlBase + resUm + "?" + dataString
                        }
                        else {
                            window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
                        }
                    },
                    error: function (e, t, n) {
                        // alert(t)
                        window.location.href = UrlBase + "FlightInt/FltResIntl.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
            }
        };

    });
};

ResHelper.prototype.eventFn = function () {
    var e = this;
    e.flterTabO.click(function () {
        divclspmatrix = 0;
        e.flterR.hide();
        e.flterO.show();
        e.flterTabO.removeClass('spn');
        //$('.matrix').addClass('brdrred');
        $(".clspMatrix").switchClass("clspMatrix1", "clspMatrix");
        e.flterTabO.removeClass('spn1');
        e.flterTabO.addClass('spn1');
        e.flterTabR.addClass('spn');
        e.flterTabO.addClass('');


        $('.list-itemR .fltbox').removeClass('bdrblue');
        $('.list-itemR .fltboxnew').removeClass('bdrblue');
        $('.list-item .fltbox').addClass('bdrblue');
        $('.list-item .fltboxnew').addClass('bdrblue');

        e.DivRefinetitle.html(e.txtDepCity1.val() + " to " + e.txtArrCity1.val());
        e.DivMatrixRtfO.show();
        e.DivMatrixRtfR.hide();
    });

    e.flterTabR.click(function () {
        divclspmatrix = 1;
        e.flterO.hide();
        e.flterR.show();
        e.flterTabR.removeClass('spn');
        e.flterTabR.removeClass('spn1');


        $(".clspMatrix").switchClass("clspMatrix1", "clspMatrix");


        $('.list-item .fltbox').removeClass('bdrblue');
        $('.list-item .fltboxnew').removeClass('bdrblue');
        $('.list-itemR .fltbox').addClass('bdrblue');
        $('.list-itemR .fltboxnew').addClass('bdrblue');

        // $('.matrix').addClass('brdrred');
        e.flterTabR.addClass('spn1');
        e.flterTabO.addClass('spn');
        e.DivRefinetitle.html(e.txtArrCity1.val() + " to " + e.txtDepCity1.val());
        e.DivMatrixRtfO.hide();
        e.DivMatrixRtfR.show();

    });

    e.clspMatrix.click(function () {

        if (divclspmatrix == 0) {
            try {
                e.DivMatrixRtfO.slideToggle(300);
            } catch (exx) { }
            e.divMatrixO.slideToggle(300);
        }
        else {
            e.DivMatrixRtfR.slideToggle(300);
        }
        e.clspMatrix.toggleClass("clspMatrix1");
    });


    e.rtfResultDiv.click(function () {
        e.hdnOnewayOrRound.val("RoundTrip");
        e.flterR.show();
        e.RoundTripH.show();
        e.OnewayH.hide();
        e.DivMatrixRtfO.show();
        e.DivMatrixRtfR.show();
        e.DivMatrix.hide();
        if (rtfResult != null) {
            e.RoundTripH.html(rtfResult);
        }



    });
    e.lccRTFDiv.click(function () {

        e.hdnOnewayOrRound.val("OneWay");
        e.flterR.hide();
        if (rtfResult == null) {
            rtfResult = e.RoundTripH.html();
        }
        e.RoundTripH.html("");
        e.RoundTripH.hide();
        e.DivMatrixRtfO.hide();
        e.DivMatrixRtfR.hide();

        if (lccRtfResult == null) {

            e.GetResultSplRTFTrip(e, "lcc");
        }
        else {

            $.blockUI({
                message: $("#waitMessage")
            });
            e.DivResult.html(lccRtfResult);
            e.Book(lccRtfResultArray);
            e.GetFltDetails(lccRtfResultArray);
            e.ShowFareBreakUp(lccRtfResultArray);
            // e.DivMatrix.html(t.GetMatrix(lccRtfResultArray[0], 'O'));
            e.FltrSort(lccRtfResultArray);
            e.GetFltDetailsR(lccRtfResultArray);
            e.MainSF.show();

            FlightHandler = new FlightResult();
            FlightHandler.BindEvents();
            $(document).ajaxStop($.unblockUI)


        }
        e.OnewayH.show();
        e.DivMatrix.show();
    });
    e.gdsRTFDiv.click(function () {

        e.hdnOnewayOrRound.val("OneWay");
        e.flterR.hide();
        if (rtfResult == null) {
            rtfResult = e.RoundTripH.html();
        }
        e.RoundTripH.html("");
        e.RoundTripH.hide();
        e.DivMatrixRtfO.hide();
        e.DivMatrixRtfR.hide();
        if (gdsRtfResult == null) {

            e.GetResultSplRTFTrip(e, "gds");
        }
        else {
            $.blockUI({
                message: $("#waitMessage")
            });

            e.DivResult.html(gdsRtfResult);
            e.Book(gdsRtfResultArray);
            e.GetFltDetails(gdsRtfResultArray);
            e.ShowFareBreakUp(gdsRtfResultArray);
            // e.DivMatrix.html(e.GetMatrix(gdsRtfResultArray[0], 'O'));
            e.FltrSort(gdsRtfResultArray);
            e.GetFltDetailsR(gdsRtfResultArray);
            e.MainSF.show();
            FlightHandler = new FlightResult();
            FlightHandler.BindEvents();
            $(document).ajaxStop($.unblockUI)


        }
        e.OnewayH.show();
        e.DivMatrix.show();


    });
}


ResHelper.prototype.Book = function (result) {
    var e = this;
    var t = this;

    this.bookO = $(".buttonfltbk");
    $(document).on("click", ".buttonfltbk1", function (e) {


        ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

        $("#searchquery").hide();

        var lineNum = $.trim($(this).attr("title"));
        var lineNo = $.trim(lineNum.split('api')[0])
        var fltSelectedArray = JSLINQ(result[0])
            .Where(function (item) { return item.LineNumber == lineNum || item.LineNumber == lineNo; })
            .Select(function (item) { return item });
        var arr = new Array(fltSelectedArray.items);
        var iscahem = arr[0][0].LineNumber;
        for (var i = 0; i < arr[0].length; i++) {
            arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber.split('ITZ')[1];
            arr[0][i].LineNumber = arr[0][i].LineNumber.split('api')[0];


        }
        if (arr[0][0].Trip == "I") {
            var urlParams = new URLSearchParams(location.search);
            var NStop = urlParams.get('NStop');
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails_LZCmp";
            $.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {


                    if (e.d.ChangeFareO.TrackId == "0") {
                        alert("Fare Changed Please Try again");

                    } else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'I');
                    }

                    else if (NStop == "TRUE") {
                        window.location = UrlBase + "FlightInt/CustomerInfoFixDep.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                    else {
                        window.location = UrlBase + "International/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                },
                error: function (e, t, n) {

                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
        else {
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
            $.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }




                    if (e.d.ChangeFareO.TrackId == "0") {
                        var r = confirm("Fare changed Please Try again.");
                        if (r == true) {
                            location.reload();
                        }
                        $('#ConfmingFlight').hide();
                        $(document).ajaxStop($.unblockUI)
                    }
                    else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
                    }
                    else {

                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                    }



                },
                error: function (e, t, n) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }
                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
    });

    e.bookO.click(function () {

        ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

        $("#searchquery").hide();

        var lineNum = $.trim($(this).attr("title"));
        var lineNo = $.trim(lineNum.split('api')[0])
        var fltSelectedArray = JSLINQ(result[0])
            .Where(function (item) { return item.LineNumber == lineNum || item.LineNumber == lineNo; })
            .Select(function (item) { return item });
        var arr = new Array(fltSelectedArray.items);
        var iscahem = arr[0][0].LineNumber;
        for (var i = 0; i < arr[0].length; i++) {
            arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber.split('ITZ')[1];
            arr[0][i].LineNumber = arr[0][i].LineNumber.split('api')[0];


        }
        if (arr[0][0].Trip == "I") {
            var urlParams = new URLSearchParams(location.search);
            var NStop = urlParams.get('NStop');
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails_LZCmp";
            $.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {


                    if (e.d.ChangeFareO.TrackId == "0") {
                        alert("Fare Changed Please Try again");

                    } else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'I');
                    }

                    else if (NStop == "TRUE") {
                        window.location = UrlBase + "FlightInt/CustomerInfoFixDep.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                    else {
                        window.location = UrlBase + "International/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId + ",I";
                    }
                },
                error: function (e, t, n) {

                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
        else {
            var compressedArr = (JSON.stringify(arr));
            var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
            $.ajax({
                url: t,
                type: "POST",

                data: JSON.stringify({
                    a: compressedArr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }




                    if (e.d.ChangeFareO.TrackId == "0") {

                        var r = confirm("Fare changed Please Try again.");
                        if (r == true) {
                            location.reload();
                        }
                        $('#ConfmingFlight').hide();
                        $(document).ajaxStop($.unblockUI)
                    }
                    else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
                    }
                    else {

                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                    }




                },
                error: function (e, t, n) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }
                    alert(t)
                    window.location = UrlBase + "Search.aspx";
                }
            })
        }
    });



    this.bookfalse = $(".falsebookbutton");
    e.bookfalse.click(function () {
        alert("Please select Fare !!");

    });

};


ResHelper.prototype.ShowFareBreakUp = function (result) {

    var t = this;
    //this.gridViewToolTip = $(".gridViewToolTip");
    $('.gridViewToolTip').click(function (event) {
        event.preventDefault();
        var th = this;
        // var   ag  = $(th).next().attr('title');

        var lineNum = this.id.split('_');
        //$('#' + this.rel + '_').slideUp();

        var lineup = lineNum[0]; //this.rel;
        if (this.rel == "") { lineup = lineNum[0]; }



        var fltSelectedArray;
        if (lineNum[1] == "R") {
            fltSelectedArray = JSLINQ(result[1])
                .Where(function (item) { return item.LineNumber == lineNum[0]; })
                .Select(function (item) { return item });
        }
        else {

            fltSelectedArray = JSLINQ(result[0])
                .Where(function (item) { return item.LineNumber == lineNum[0]; })
                .Select(function (item) { return item });
        }

        var compressedData = (JSON.stringify(fltSelectedArray.items));
        //var compressedData = LZString.compressToUTF16(JSON.stringify(fltSelectedArray.items));
        //if (fltSelectedArray.items[0].Trip == "I" ) {
        var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL1";
        $.ajax({
            url: n,
            type: "POST",
            //data: JSON.stringify({
            //    AirArray: fltSelectedArray.items,
            //    Trip: fltSelectedArray.items[0].Trip
            //}),
            data: JSON.stringify({
                AirArray: compressedData,
                Trip: fltSelectedArray.items[0].Trip
            }),
            dataType: "json",
            type: "POST",
            //async: false,
            contentType: "application/json; charset=utf-8",
            success: function (e) {
                // $("#FareBreakupHeder").addClass("show");
                //$("#FareBreakupHederId").html("");
                // $("#FareBreakupHederId").html("Loding Breakup ...");
                // $("#FareBreakupHederId").html(t.CreateFareBreakUp(e.d[0]));
                $('#' + lineup + '_Fare').html(t.CreateFareBreakUp(e.d[0]));


                //$('#' + this.rel + '_').show();
                //$('#' + lineup + '_Fare').show();
                //$('#' + lineup).hide();
                //$(th).next().html(t.CreateFareBreakUp(e.d[0]));
                //t.gridViewToolTip.tooltip({

                //    track: true,
                //    delay: 0,
                //    showURL: false,
                //    fade: 100,
                //    bodyHandler: function () {
                //        return $($(th).next().html());
                //    },
                //    showURL: false

                //});
            },
            error: function (e, t, n) {
                //alert(t)
            }
        })

        //}
        //else {
        //    $(th).next().html(t.CreateFareBreakUp(fltSelectedArray.items[0]));
        //    t.gridViewToolTip.tooltip({

        //        track: true,
        //        delay: 0,
        //        showURL: false,
        //        fade: 100,
        //        bodyHandler: function () {
        //            return $($(th).next().html());

        //        },
        //        showURL: false

        //    });
        //}


    });



    this.SRFTooltipFare = $(".gridViewToolTipSRF");

    t.SRFTooltipFare.click(function (event) {
        var SFMfare = "";
        var th = this;
        if ($("input[name=RadioGroup1]").length == 0) {
            SFMfare = "";
        }
        else {
            SFMfare = $("input[name=RadioGroup1]:checked").val();

        }
        if (SFMfare == "2") {
            var Mkey = Obook[0].SubKey + Rbook[0].SubKey;
            //}

            //if (Obook[0].LineNumber.search('SFM') > 0) {
            /// SPL
            //Devesh SRFFF
            var fltReturnArrayM = null;
            var arr = new Array();
            var compressedData;
            var sTrip = "D";
            if (SRFReprice == true && SRFResultAfterReprice != null && SRFResultAfterReprice.length > 0 && SRFResultAfterReprice[0].d.length > 0 && SRFResultAfterReprice[0].d[0].ValiDatingCarrier.toUpperCase() == "6E") {
                fltReturnArrayM = SRFResultAfterReprice[0].d;
                arr = new Array(fltReturnArrayM);
                // compressedData = LZString.compressToUTF16(JSON.stringify(fltReturnArrayM));
                compressedData = (JSON.stringify(fltReturnArrayM));
                //sTrip= fltReturnArrayM.items[0].Trip
                sTrip = fltReturnArrayM[0].Trip
            }
            else {
                fltReturnArrayM = JSLINQ(CommanRTFArray)
                    .Where(function (item) { return item.MainKey == Mkey; })
                    .Select(function (item) { return item });
                //var fltReturnArrayM = JSLINQ(CommanRTFArray)
                //       .Where(function (item) { return item.LineNumber == Obook[0].LineNumber; })
                //       .Select(function (item) { return item });
                //var arr = new Array(fltReturnArrayM.items);
                arr = new Array(fltReturnArrayM.items);
                //compressedData = LZString.compressToUTF16(JSON.stringify(fltReturnArrayM.items));
                compressedData = (JSON.stringify(fltReturnArrayM.items));
                sTrip = fltReturnArrayM.items[0].Trip
            }
            //var compressedData = LZString.compressToUTF16(JSON.stringify(fltReturnArrayM.items));
            //var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
            var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL1";
            $.ajax({
                url: n,
                type: "POST",
                //data: JSON.stringify({
                //    AirArray: fltReturnArrayM.items,
                //    Trip: fltReturnArrayM.items[0].Trip
                //}),
                data: JSON.stringify({
                    AirArray: compressedData,
                    Trip: sTrip
                }),
                dataType: "json",
                type: "POST",
                //async: false,
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    $("#FareBreakupHeder").addClass("show");
                    $("#FareBreakupHederId").html("");
                    $("#FareBreakupHederId").html("Loding Breakup ...");
                    $("#FareBreakupHederId").html(t.CreateFareBreakUp(e.d[0]));
                    //$(th).next().html(t.CreateFareBreakUp(e.d[0]));

                    //$('#fareBrkup').html(t.CreateFareBreakUp(e.d[0]));
                    //t.SRFTooltipFare.tooltip({

                    //    track: true,
                    //    delay: 0,
                    //    showURL: false,
                    //    fade: 100,
                    //    bodyHandler: function () {
                    //        return $('#fareBrkup').html();

                    //    },
                    //    showURL: false

                    //});
                },
                error: function (e, t, n) {
                    //alert(t)
                }
            })



        }
        else {



            //var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
            var compressedDataO = (JSON.stringify(Obook));
            //var compressedDataO = LZString.compressToUTF16(JSON.stringify(Obook));
            var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL1";
            $.ajax({
                url: n,
                type: "POST",
                //data: JSON.stringify({
                //    AirArray: Obook,
                //    Trip: Obook[0].Trip
                //}),
                data: JSON.stringify({
                    AirArray: compressedDataO,
                    Trip: Obook[0].Trip
                }),
                dataType: "json",
                type: "POST",
                //async: false,
                contentType: "application/json; charset=utf-8",
                success: function (e) {

                    var compressedDataR = (JSON.stringify(Rbook));
                    //  var compressedDataR = LZString.compressToUTF16(JSON.stringify(Rbook));
                    var bookO = t.CreateFareBreakUp(e.d[0]);

                    $.ajax({
                        url: n,
                        type: "POST",
                        //data: JSON.stringify({
                        //    AirArray: Rbook,
                        //    Trip: Rbook[0].Trip
                        //}),
                        data: JSON.stringify({
                            AirArray: compressedDataR,
                            Trip: Rbook[0].Trip
                        }),
                        dataType: "json",
                        type: "POST",
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (e) {


                            var bookR = t.CreateFareBreakUp(e.d[0]);
                            var htmlfare = '<div class="row">';

                            htmlfare += '<div class="col-md-12"><div><h4>Outbound</h4></div>' + bookO + '</div>'
                            htmlfare += '<div class="col-md-12"><div><h4>Inbound</h4></div>' + bookR + '</div>'
                            htmlfare += '<div class="clear"></div>';
                            htmlfare += '</div>';

                            $("#FareBreakupHeder").addClass("show");
                            $("#FareBreakupHederId").html("");
                            $("#FareBreakupHederId").html("Loding Breakup ...");
                            $("#FareBreakupHederId").html(htmlfare);
                            //var htmlfare = '<div class="large-12 medium-12 small-12 fareres2">';

                            //htmlfare += '<div class="large-6 medium-6 small-12 columns"><div><h4>Outbound</h4></div>' + bookO + '</div>'
                            //htmlfare += '<div class="large-6 medium-6 small-12 columns"><div><h4>Inbound</h4></div>' + bookR + '</div>'
                            //htmlfare += '<div class="clear"></div>';
                            //htmlfare += '</div>';


                            //$('#fareBrkup').html(htmlfare);

                            //t.SRFTooltipFare.tooltip({

                            //    track: true,
                            //    delay: 0,
                            //    showURL: false,
                            //    fade: 100,
                            //    bodyHandler: function () {
                            //        return $('#fareBrkup').html();

                            //    },
                            //    showURL: false

                            //});




                        },
                        error: function (e, t, n) {
                            alert(t)
                        }
                    })




                },
                error: function (e, t, n) {
                    alert(t)
                }
            })







        }





    });


    this.fareRuleToolTip = $(".fareRuleToolTip");
    $('.fareRuleToolTip').click(function (event) {
        event.preventDefault();
        var th = this;
        var lineNum = $(th).next().attr('title').split('_');
        //$('#' + lineNum[0] + '_').show();
        var Divhide = '<div>';
        //Divhide += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + lineNum[0] + '_\');" title="Click to close Details"><i class="fa fa-times-circle"></i></span><div></div>';
        $('#' + lineNum[0] + '_').html("<div align='center'><img alt='loading' width='50px' height='50px' src='../images/loadingAnim.gif'/></div>");
        Divhide += '</div>';
        if (lineNum[0] != null) {
            if (lineNum[1] == "O") {
                //$('#' + lineNum[0] + '_').slideDown();
            }
            if (lineNum[1] == "R") {
                //$('#' + lineNum[0] + '_RO').slideDown();
            }
            var fltSelectedArray;
            if (lineNum[1] == "R") {
                fltSelectedArray = JSLINQ(result[1])
                    .Where(function (item) { return item.LineNumber == lineNum[0]; })
                    .Select(function (item) { return item });
            }
            else {
                fltSelectedArray = JSLINQ(result[0])
                    .Where(function (item) { return item.LineNumber == lineNum[0]; })
                    .Select(function (item) { return item });
            }


            //var newArry=  jQuery.extend(true, [], fltSelectedArray.items);


            //for (var u = 0; u < newArry.length; u++)
            //{
            //    newArry[u].LineNumber = 1;

            //}
            var NewArr = new Array(fltSelectedArray.items);

            if (fltSelectedArray.items.length > 0) {
                var n = UrlBase + "FareRuleService.asmx/GetFareRule";
                $.ajax({
                    url: n,
                    type: "POST",
                    data: JSON.stringify({
                        JsonArr: NewArr,
                        sno: fltSelectedArray.items[0].sno,
                        Provider: fltSelectedArray.items[0].Provider
                    }),
                    dataType: "json",
                    type: "POST",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {
                        var str1 = '<div>';
                        if (e.d.Response == null) {
                            str1 += '<div class=""><div></div>';
                            str1 += '<div>fare rule not available.</div>';
                            str1 += '</div>';
                        }

                        else {
                            str1 += '<div class=""><div></div>';
                            str1 += '<div>' + e.d.Response.FareRules[0].FareRuleDetail + '</div>'
                            str1 += '</div>';
                        }
                        if (lineNum[1] == "R") {
                            $('#' + lineNum[0] + '_RO').html(str1);
                            //$('#' + lineNum[0] + '_RO').show();
                        }
                        else {
                            $('#' + lineNum[0] + '_canc').html(str1);
                            //$('#' + lineNum[0] + '_canc').show();
                        }
                        //$('#' + lineNum[0] + '_').slideToggle();
                        // $('#' + lineNum[0]).hide();
                    },
                    error: function (e, t, n) {
                        alert("Please try after sometime!")
                        $('#' + lineNum[0] + '_').hide();
                    }
                })

            }
            else {
                $(th).next().html("fare rule not available.");
                $('#' + lineNum[0] + '_').hide();
                if (lineNum[1] == "R") {
                    $('#' + lineNum[0] + '_R').hide();
                }
                //t.fareRuleToolTip.tooltip({

                //    track: true,
                //    delay: 0,
                //    showURL: false,
                //    fade: 100,
                //    bodyHandler: function () {
                //        return $(th).next().html();

                //    },
                //    showURL: false

                //});
            }
        }
    });



    $('.fltfareDetailsR').click(function (event) {


        if ($(this).attr("rel") != null) {
            var th = this;
            $(th).next().html("Loading.., Please wait.");
            $(th).next().fadeToggle(1000);
            var lineNums = $(this).attr("rel").split('_');
            // var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");
            $('#' + lineNums[1] + '_' + lineNums[2]).show();
            //$('#' + this.idr + '_').slideUp();
            //$('#' + this.rel + '_').slideUp();
            var OB;
            if ($.trim(lineNums[2]) == "O") {
                OB = JSLINQ(result[0])
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });
            }
            else if ($.trim(lineNums[2]) == "R") {
                OB = JSLINQ(result[1])
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });
            }
            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });
            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });
            var str1 = '<div>';
            if (O.items.length > 0 || R.items.length > 0) {
                var NewArr;
                var SNo, Prvdr;
                if (O.items.length > 0) {
                    NewArr = new Array(O.items);
                    SNo = O.items[0].sno;
                    Prvdr = O.items[0].Provider;
                }
                else if (R.items.length > 0) {
                    NewArr = new Array(R.items);
                    SNo = R.items[0].sno;
                    Prvdr = R.items[0].Provider;
                }

                //var newArry = jQuery.extend(true, [], NewArr);


                //for (var u = 0; u < newArry.length; u++) {
                //    newArry[u].LineNumber = 1;

                //}
                //var NewArrO = new Array(newArry);


                var n = UrlBase + "FareRuleService.asmx/GetFareRule";
                $.ajax({
                    url: n,
                    type: "POST",
                    data: JSON.stringify({
                        JsonArr: NewArr,
                        sno: SNo,
                        Provider: Prvdr
                    }),
                    dataType: "json",
                    type: "POST",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {
                        // 
                        var str1 = "";
                        str1 += '<div class="depcity">';
                        str1 += '<div style="width: 60%; margin:10% 0 0 20% !important; padding: 10px;background: #ffffff;box-shadow:0px 0px 4px #333;margin: auto;border: 6px solid #eee;left: 34%; top: 182px;">';
                        str1 += '<div style="cursor:pointer; float:right; position:relative; top:-12px; right:3px;font-size:20px" onclick="DiplayMsearch(' + $.trim($(th).next()[0].id) + ');">X</div>';
                        str1 += '<div class="large-12 medium-12 small-12 bld">Fare Rule</div>';
                        if (e.d.Response == null) {
                            str1 += '<div>fare rule not available.</div>';
                        }
                        else {
                            str1 += '<div style="overflow-y:scroll;height:250px;">' + e.d.Response.FareRules[0].FareRuleDetail + '</div>'
                        }
                        str1 += '</div>'
                        str1 += '</div>'
                        $(th).next().html(str1);
                    },
                    error: function (e, t, n) {
                        alert("Please try after sometime!!")
                    }
                })
            }
            else {
                $(th).next().html("fare rule not available.");
                t.fareRuleToolTip.tooltip({

                    track: true,
                    delay: 0,
                    showURL: false,
                    fade: 100,
                    bodyHandler: function () {
                        return $(th).next().html();

                    },
                    showURL: false

                });
            }
        }
    });

    $(".FareBreakupHederClose").click(function () {
        $("#FareBreakupHeder").removeClass("show");
        $("#FareBreakupHederId").html("");
    });


};
ResHelper.prototype.CreateFareBreakUp = function (e) {
    var t = this;
    var n = e.AdtTax - e.AdtFSur;
    var r = e.AdtFare;
    var i = e.ChdTax - e.ChdFSur;
    var s = e.ChdFare;
    var o = e.InfTax - e.InfFSur;
    var u = e.InfFare;
    var pmf = t.DisplayPromotionalFare(e);
    //var a = e.TotMrkUp;
    var a;
    if (e.AdtFareType == 'Special Fare') { a = (e.ADTAgentMrk * e.Adult) + (e.CHDAgentMrk * e.Child); }
    else { a = e.TotMrkUp; }
    var f = e.TotCB;
    var l = e.TotTds;
    var aa = parseInt(a / (e.Adult + e.Child));
    strResult = '<div><span onclick="Close(\'' + e.LineNumber + '_\');" title="Click to close Details"></span></div>'
    strResult = strResult + "<div class='new-fare-details ' id='FareBreak'>"
    strResult = strResult + "<div class='new-fare-d-1'>"
    strResult = strResult + "<div class='nw-b2b-fared'>"
    strResult = strResult + "<div class='nw-pad-b2'>"
    strResult = strResult + "<div class='nw-far-1'><span>" + e.Adult + " </span>x Adult</div>"
    strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.AdtBfare + "</span></div>"
    strResult = strResult + "</div>";
    strResult = strResult + "</div>";

    //strResult = strResult + "<table border='0' cellpadding='0' cellspacing='0' id='FareBreak' class='breakup w100'>";
    //strResult = strResult + "<thead>";//<tr><th colspan='5' class='hd'>Fare Summary</th></tr>";
    //strResult = strResult + "<tr>";
    //strResult = strResult + "<th></th>";
    //strResult = strResult + "<th>Base Fares</th>";
    //strResult = strResult + "<th>Fuel Surcharge</th>";
    //strResult = strResult + "<th>Other Tax</td>";
    //strResult = strResult + "<th>Total Fare</th></tr></thead>";
    //strResult = strResult + "<tbody><tr><td class='bld'>ADT</td>";
    //strResult = strResult + "<td>" + e.AdtBfare + "</td>";
    //strResult = strResult + "<td>" + e.AdtFSur + "</td>";
    //strResult = strResult + "<td>" + (n + aa) + "</td>";
    //strResult = strResult + "<td>" + (r + aa) + "</td></tr>";
    if (e.Child > 0) {
        //strResult = strResult + "<tr><td class='bld'>CHD</td>";
        //strResult = strResult + "<td>" + e.ChdBFare + "</td>";
        //strResult = strResult + "<td>" + e.ChdFSur + "</td>";
        //strResult = strResult + "<td>" + (i + aa) + "</td>";
        //strResult = strResult + "<td>" + (s + aa) + "</td></tr>"


        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>" + e.Child + " </span>x Child</div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.ChdBFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";


    }
    if (e.Infant > 0) {
        //strResult = strResult + "<tr><td class='bld'>INF</td>";
        //strResult = strResult + "<td>" + e.InfBfare + "</td>";
        //strResult = strResult + "<td>" + e.InfFSur + "</td>";
        //strResult = strResult + "<td>" + o + "</td>";
        //strResult = strResult + "<td>" + u + "</td></tr>"


        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>" + e.Infant + " </span>x Child</div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.InfBfare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";


    }
    //strResult = strResult + "<tr>";
    //strResult = strResult + "<td><span class='bld'>GST</span><br />" + e.STax + "</td>";
    if (e.IsCorp == true) {
        //strResult = strResult + "<td><span class='bld'>Management Fee:</span><br />" + e.TotMgtFee + "</td>";
        //strResult = strResult + "<td>(" + e.Adult + " ADT<br />" + e.Child + " CHD<br />" + e.Infant + " INF)</td>";
        //strResult = strResult + "<td><span class='bld'>Total Fare:</span><br />" + e.TotalFare + "</td>";
        //strResult = strResult + "<td><span class='bld'>Net Fare:</span><br />" + e.NetFare + "</td>";

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Total Taxes & Fees</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Gross Total</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Net Fare</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.NetFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



    }
    else {



        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Total Taxes & Fees</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalTax + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";
        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Gross Total</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.TotalFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";

        strResult = strResult + "<div class='nw-b2b-fared'>"
        strResult = strResult + "<div class='nw-pad-b2'>"
        strResult = strResult + "<div class='nw-far-1'><span>Net Fare</span></div>"
        strResult = strResult + "<div class='nw-far-2'>₹ <span>" + e.NetFare + "</span></div>"
        strResult = strResult + "</div>";
        strResult = strResult + "</div>";



        //strResult = strResult + "<td><span class='bld'>Transaction Fee:</span><br />" + e.TFee + "</td>";
        //strResult = strResult + "<td><span class='bld'>Transaction Charge:</span><br />0</td>";//
        //strResult = strResult + "<td colspan='2' class='bld cursorpointer'>" + e.Adult + " ADT, " + e.Child + " CHD, " + e.Infant + " INF<br /><span class='f16'>Total Fare: " + e.TotalFare + "</span></td>";
        //strResult = strResult + "</tr>";
        //strResult = strResult + "<tr>";
        //strResult = strResult + "<td><span class='bld'>Commission:</span><br />" + e.TotDis + "</td>";
        //strResult = strResult + "<td><span class='bld'>Cash Back:</span><br />" + f + "</td>";
        //strResult = strResult + "<td><span class='bld'>TDS:</span><br />" + l + "</td>";
        //strResult = strResult + "<td class='bld'>NetFare:<br />" + e.NetFare + "</td><td>&nbsp;</td>";


    }
    //strResult = strResult + "</tr>";
    if (pmf != '') {
        //strResult = strResult + "<tr><td class='colorp f16 italic' colspan='5'><div class='clear1'></div>" + pmf + " </td></tr>";
    }
    //strResult = strResult + "</tbody>";
    //strResult = strResult + "</table>";



    strResult = strResult + "</div>";

    //Tax
    strResult = strResult + "<div class='can-bnew-rg'>";
    strResult = strResult + "<div class='lef-b2b-cane'>"
    strResult = strResult + "<div class='b2b-ca-char'>Tax & Other Fees</div>"
    strResult = strResult + " <table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>"
    strResult = strResult + "<tbody>"


    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>SGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>CGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>IGST Airline</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>Fuel Surcharge</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ " + e.TotalFuelSur + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + " <td scope='row' width='50%'><span>Transaction Charge</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"




    //strResult = strResult + "<tr class='ng-scope'>"
    //strResult = strResult + "<td scope='row' width='50%'><span>GST</span></td>"
    //strResult = strResult + "<td width='50%'>"
    //strResult = strResult + "<span>₹ " + e.STax + "</span>"
    //strResult = strResult + "</td>"
    //strResult = strResult + "</tr>"
    //strResult = strResult + " <td scope='row' width='50%'><span>Management Fee</span></td>"
    //strResult = strResult + "<td width='50%'>"
    //strResult = strResult + "<span>₹ " + e.TotMgtFee + "</span>"
    //strResult = strResult + "</td>"
    //strResult = strResult + "</tr>"
    //strResult = strResult + "<tr>"
    //strResult = strResult + "<td scope='row'>Transaction Fee</td>"
    //strResult = strResult + "<td>"
    //strResult = strResult + "<span>₹ " + e.TFee + "</span>"
    //strResult = strResult + "</td>"
    //strResult = strResult + "</tr>"
    //strResult = strResult + "<tr>"
    //strResult = strResult + " <td scope='row'>Transaction Charge</td>"
    //strResult = strResult + "<td>"
    //strResult = strResult + "<span>₹ 0</span>"
    //strResult = strResult + "</td>"
    //strResult = strResult + "</tr>"
    //strResult = strResult + ""
    //strResult = strResult + ""
    //strResult = strResult + ""
    strResult = strResult + "</tbody>"
    strResult = strResult + "</table>"
    strResult = strResult + "</div>"

    //Other Charges
    strResult = strResult + "<div class='rig-b2b-cane'>"
    strResult = strResult + "<div class='b2b-ca-char'>Commission</div>"
    strResult = strResult + "<table rules='all' border='1' class='b2b-can-tabe' style='border:1px solid #ddd;'>"
    strResult = strResult + "<tbody>"
    strResult = strResult + "<td scope='row' width='50%'><span>Commission</span></td>"
    strResult = strResult + " <td width='50%'>"
    strResult = strResult + "<span>₹ " + e.TotDis + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"
    strResult = strResult + "<td scope='row' width='50%'><span>TDS</span></td>"
    strResult = strResult + "<td width='50%'>"
    strResult = strResult + "<span>₹ " + l + "</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"
    //strResult = strResult + "<tr>"
    //strResult = strResult + "<td scope='row'>Cash Back</td>"
    //strResult = strResult + "<td>"
    //strResult = strResult + "<span>₹ " + f + "</span>"
    //strResult = strResult + "</td>"
    //strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>CGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>SGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>IGST Company</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"

    strResult = strResult + "<tr>"
    strResult = strResult + "<td scope='row'>Transaction Charge</td>"
    strResult = strResult + "<td>"
    strResult = strResult + "<span>₹ 0</span>"
    strResult = strResult + "</td>"
    strResult = strResult + "</tr>"
    strResult = strResult + "</tbody>"
    strResult = strResult + "</table>"
    strResult = strResult + "</div>"

    strResult = strResult + "<div class='tr-b2b-cancgr'>"
    strResult = strResult + "<div class='trm-had'>Terms &amp; Conditions</div>"
    strResult = strResult + "<div class='terms-b2b2-cancahe'>"
    strResult = strResult + "<ul>"
    strResult = strResult + "<li>Penalty is subject to 4 hours prior to departure and no changes are allowed after that.</li>"
    strResult = strResult + "<li>The charges will be on per passenger per sector</li>"
    strResult = strResult + "<li>Rescheduling Charges = Rescheduling/Change Penalty + Fare Difference (if applicable)</li>"
    strResult = strResult + "<li>Partial cancellation is not allowed on the flight tickets which are book under special discounted fares</li>"
    strResult = strResult + "<li>In case, the customer have not cancelled the ticket within the stipulated time or no show then only statutory taxes are refundable from the respective airlines</li>"
    strResult = strResult + "<li>For infants there is no baggage allowance</li>"
    strResult = strResult + "<li>In certain situations of restricted cases, no amendments and cancellation is allowed</li>"
    strResult = strResult + "<li>Penalty from airlines needs to be reconfirmed before any cancellation or amendments</li>"
    strResult = strResult + "<li>Penalty changes in airline are indicative and can be changes without any prior notice</li>"
    strResult = strResult + " </ul>"
    strResult = strResult + "</div>"
    strResult = strResult + "</div>"
    //strResult = strResult + ""
    //strResult = strResult + ""


    strResult = strResult + "</div>";
    strResult = strResult + "</div>";
    strResult = strResult + "<div class='clear'></div>";
    strResult = strResult + "<div class='clear'></div>";
    return strResult
};
ResHelper.prototype.DisplayPromotionalFare = function (e) {

    var pmf = '';
    try {
        //if ((e.fareBasis.substring(0, 1) == "P") && (e.ValiDatingCarrier == "6E")) {
        //    pmf = "Promotional Fare";
        // }
        //if ((e.AdtFareType == "Special Fare") && (e.ValiDatingCarrier == "6E")) {
        //  pmf = "*T&C Apply.";
        //}
        //if ((e.AvailableSeats == "SGNRML") && ((e.RBD == "E") || (e.RBD == "F") || (e.RBD == "H") || (e.RBD == "J") || (e.RBD == "K")) && (parseInt(e.Adult) + parseInt(e.Child) >= 2)) {
        // pmf = "Friend and Family special fares (*T&C Apply.) ";
        // }

        if ((e.ProductClass == "A") && (e.AdtFar == "NRM" || e.AdtFar == "CRP") && (e.ValiDatingCarrier == "6E") && (parseInt(e.Adult) + parseInt(e.Child) >= 4)) {
            pmf = "Friend and Family special fares (*T&C Apply.)";
        }
        else if ((e.AdtFar == "NRM" || e.AdtFar == "CRP") && (e.ValiDatingCarrier == "SG") && ((e.RBD == "E") || (e.RBD == "F") || (e.RBD == "H") || (e.RBD == "J") || (e.RBD == "K")) && (parseInt(e.Adult) + parseInt(e.Child) >= 4)) {
            pmf = "Friend and Family special fares (*T&C Apply.) ";
        }

    }
    catch (fberr) { }
    return pmf;
};

ResHelper.prototype.MakeupTotDur = function (totDur) {

    var tdur = "";
    if ($.trim(totDur) != "") {
        if ($.trim(totDur).search("hrs") > 0) {
            var hrsmin;

            tdur = $.trim(totDur).replace(" hrs", ":").replace(" min", "");
            hrsmin = tdur.split(":");
            tdur = ($.trim(hrsmin[0]).length > 1 ? hrsmin[0] : "0" + hrsmin[0]) + ":" + ($.trim(hrsmin[1]).length > 1 ? $.trim(hrsmin[1]) : "0" + $.trim(hrsmin[1]))

        }
        else if ($.trim(totDur).search(":") > 0) {
            tdur = $.trim(totDur); //.replace(/\s/g, '');
        }
        else {


            if (parseInt(totDur) < 60) {

                tdur = "00:" + ($.trim(totDur).length > 1 ? $.trim(totDur) : "0" + $.trim(totDur));
            }
            else {
                var hrs = $.trim(parseInt(parseInt(totDur) / 60).toString());
                var rmin = $.trim(parseInt(parseInt(totDur) % 60).toString());

                tdur = (hrs.length > 1 ? hrs : "0" + hrs) + ":" + ($.trim(rmin).length > 1 ? rmin : "0" + rmin);
            }


        }
    }


    return tdur;
};

ResHelper.prototype.MakeupAdTime = function (adTime) {
    if (adTime.length < 4) {
        if (adTime.length == 1) { adTime = "000" + adTime; }
        else if (adTime.length == 2) { adTime = "00" + adTime; }
        else if (adTime.length == 3) { adTime = "0" + adTime; }
    }
    if ($.trim(adTime).search(":") <= 0) {
        return $.trim(adTime).slice(0, 2) + ":" + $.trim(adTime).slice(2, 4)
    }
    else {
        return adTime;
    }

};

ResHelper.prototype.GetMinMaxPrice = function (result) {

    var marray = new Array();
    var OF = JSLINQ(result)
        .Select(function (item) { return item.TotalFare });

    marray.push(Math.min.apply(Math, OF.items));
    marray.push(Math.max.apply(Math, OF.items));
    return marray;

};

ResHelper.prototype.GetMinMaxTime = function (result) {

    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
        .Select(function (item) { return e.MakeupAdTime(item.DepartureTime).replace(':', '') });
    marray.push(Math.min.apply(Math, OF.items));
    marray.push(Math.max.apply(Math, OF.items));
    return marray;

};

ResHelper.prototype.GetUniqueAirline = function (result, cls, type) {
    var e = this;
    var marray = new Array();
    var marrayalcode = new Array();
    var OF = JSLINQ(result)
        .OrderBy(function (item) { return item.AirLineName })
        .Select(function (item) { return item.AirLineName });

    var OAC = JSLINQ(result)
        .OrderBy(function (item) { return item.AirLineName })
        .Select(function (item) { return item.ValiDatingCarrier });

    marray = OF.items.unique1();
    marrayalcode = OAC.items.unique1();

    var str = '<div class="jplist-group" data-control-type="Airlinefilter' + type + '" data-control-action="filter"  data-control-name="Airlinefilter' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'

    for (var i = 0; i < marray.length; i++) {


        str += '<label for="' + marray[i] + '" style="width: 100%;"><input value="' + marray[i] + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  />  ' + marray[i] + '<span><img alt="" src="../Airlogo/sm' + marrayalcode[i] + '.gif" style="width:17px;float: right;margin-top: 9px;"></span></label>';

    }
    str += '</div>';

    if (type == 'O') {
        e.airlineFilter.html(str);
    }
    else {
        e.airlineFilterR.html(str);
    }


};


ResHelper.prototype.GetStopFilter = function (result, cls, type) {

    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
        .OrderBy(function (item) { return item.Stops })
        .Select(function (item) { return item.Stops });

    marray = OF.items.unique1();

    var str = '<div class="jplist-group" data-control-type="Stopfilter' + type + '" data-control-action="filter"  data-control-name="Stopfilter' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'

    for (var i = 0; i < marray.length; i++) {

        str += '<label for="' + marray[i] + '" style="width:100%;"><input value="' + marray[i] + '"  id="CheckboxS' + type + i + 1 + '"  type="checkbox"  />&nbsp;' + marray[i] + '</label>';

    }
    str += '</div>';
    if (type == 'O') {
        e.stopFilter.html(str);
    }
    else {
        e.stopFilterR.html(str);
    }

};

ResHelper.prototype.GetMatrix = function (result, type) {

    var e = this;
    var stopArray = new Array();
    var OF1 = JSLINQ(result)
        .OrderBy(function (item) { return item.Stops })
        .Select(function (item) { return item.Stops });
    stopArray = OF1.items.unique();

    var AirArray = new Array();
    var OF = JSLINQ(result)
        .OrderBy(function (item) { return item.TotalFare })
        .Select(function (item) { return $.trim(item.MarketingCarrier) + "_" + $.trim(item.AirLineName) });

    AirArray = OF.items.unique1();

    for (var i = 0; i < AirArray.length; i++) {

        if ($.trim(AirArray[i]).search('null') >= 0) {
            AirArray.splice(i, 1);
        }
    }

    var cls;

    if (type == 'O') {
        cls = '.airstopO'
    }
    else {
        cls = '.airstopR'
    }
    var str = '';
    var str = '<div class="jplist-group w100" style="overflow-x:scroll;"  data-control-type="button-text-filter-group' + type + '"  data-control-action="filter"  data-control-name="button-text-filter-group-' + type + '">';

    str += '<table class="matrix passenger brdblue" cellpadding="0" cellspacing="0" >';


    for (var i = 0; i < stopArray.length + 1; i++) {
        str += '<tr>';
        for (var j = 0; j < AirArray.length + 1; j++) {

            if (i == 0) {

                if (i == 0 && j == 0) {

                    str += '<td class="f16 bld bgmn1" style="min-width:70px;" onclick="mtrxx()" id="' + i + '">';
                    str += '<button type="button" class="jplist-reset-btn colorwht" data-control-type="reset" data-control-name="reset1" data-control-action="reset" style="border: none; background: none; cursor:pointer;">ALL</button>';
                    str += '</td>';
                }
                else {
                    var airL = AirArray[j - 1].split('_');
                    str += '<td id="' + j + '"><div data-path=".airlineImage" data-button="true"  data-text="' + airL[1] + '" data-fltr="" > <img alt="" src="../Airlogo/sm' + airL[0] + '.gif"  title="' + airL[1] + '" /></div></td>';
                }
            }
            else if (i != 0 && j == 0) {
                str += '<td  class="colorwht bgmn1" id="' + j + '"><div data-path=".stops" data-button="true"  data-text="' + stopArray[i - 1] + '" data-fltr="" > ' + stopArray[i - 1] + '</div> </td>';
            }
            else {
                str += '<td id="' + j + '" style="min-width:50px;">' + e.GetFareForMatrix(AirArray[j - 1], stopArray[i - 1], result, cls);
                str += '</td>';
            }
        }
        str += '</tr>';
    }
    str += '</table>';
    str += '</div>';
    return str;
};

ResHelper.prototype.GetFareForMatrix = function (airNameCode, stop, result, datapath) {
    var airL = airNameCode.split('_');

    var OB = JSLINQ(result)
        .Where(function (item) { return $.trim(item.AirLineName).toLowerCase() == $.trim(airL[1]).toLowerCase() && $.trim(item.Stops).toLowerCase() == $.trim(stop).toLowerCase(); })
        .Select(function (item) { return item.TotalFare })
    //.OrderByDescending(function (item) { return item.TotalFare })
    var minval = Math.min.apply(Math, OB.items);

    var str = '';

    if (OB.items.length > 0) {
        var datafltr = $.trim(stop).toString().toLowerCase() + '_' + $.trim(airL[1]).toString().toLowerCase().replace(' ', '_');
        str += '<div  data-path="' + datapath + '" data-button="true"  data-text="' + datafltr + '"  data-fltr="' + datafltr + '" >';
        str += '<img src="' + UrlBase + 'Images/rs.png" style="" />&nbsp;' + minval;
        str += '</div>';
    }
    else {
        str = '-';
    }
    return str;
};



Array.prototype.unique = function () {
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
Array.prototype.unique1 = function () {
    var a = [];
    var l = this.length;
    for (var i = 0; i < l; i++) {
        if (i == 0) { a.push(this[i]); }
        else {
            var flg = false;
            for (var j = 0; j < a.length; j++) {
                if ($.trim(a[j]).toLowerCase() === $.trim(this[i]).toLowerCase()) {
                    flg = true;
                }
            }
            if (flg == false)
                a.push(this[i]);

        }
    }
    return a;
};


ResHelper.prototype.calFlightDur = function (departTime, arrivalTime) {
    var durH = parseInt(arrivalTime.slice(0, 2), 10) - parseInt(departTime.slice(0, 2), 10)
    if (durH < 0) {

        durH = durH + 24;
    }
    var durT = parseInt(arrivalTime.slice(2), 10) - parseInt(departTime.slice(2), 10)
    if (durT < 0) {
        durT = 60 + durT;
        durH = durH - 1;
    }

    var maxH = this.getFourDigitTime($.trim(durH.toString()));
    var maxT = this.getFourDigitTime($.trim(durT.toString()));
    return [maxH.slice(2), ":", maxT.slice(2)].join('');
};

ResHelper.prototype.getFourDigitTime = function (val) {

    var val1;
    if (val.toString().length == 1) { val1 = "000" + val.toString(); }
    else if (val.toString().length == 2) { val1 = "00" + val.toString(); }
    else if (val.toString().length == 3) { val1 = "0" + val.toString(); }
    else { val1 = val; }

    return val1;

};

ResHelper.prototype.GetFltDetails = function (result) {
    var e = this;
    $('.fltDetailslink').click(function (event) {

        event.preventDefault();
        // var main = $.parseJSON($('#' + this.rel + 'M').html());
        //   
        debugger;
        if (this.rel != null) {
            $('#' + this.rel + '_').slideUp();
            var lineNums = this.rel; //.split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };


            // var airc = lineNums[1] + "_" + lineNums[2];
            var OB = JSLINQ(result[0])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });

            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });

            var R = JSLINQ(OB.items)
            //.Where(function (item) { return item.Flight == "2"; })
            //.Select(function (item) { return item });


            var fta = JSLINQ(OB.items)
                .Select(function (item) { return item.Flight });

            var ft = Math.max.apply(Math, fta.items);

            var str1 = '<div class="bdr-fltdet">';
            if (O.items.length > 0) {

                var totDur = "";

                if (O.items[0].Provider == "TB") {
                    totDur = e.GetTotalDuration(O.items);
                }
                else {
                    totDur = e.MakeupTotDur(O.items[0].TotDur)
                }
                //str1 += '<div class="large-12 medium-12 small-12 bld" style="position: relative;right: 20px;top: -10px;float: left;width: 112%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;"><span class="f20">' + O.items[0].DepartureCityName + ' → ' + O.items[O.items.length - 1].ArrivalCityName + '</span> | <span class="f16" style="color: #979797;font-size: 11px;">' + totDur + '</span></div><div class="clear1"></div>';

                //str1 += '<div class=""><div class="large-12 medium-12 small-12"><span class="f16">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span></div><div class="clear"></div>';
                for (var i = 0; i < O.items.length; i++) {
                    if (i >= 1 && O.items.length > 1 && i < O.items.length) {
                        str1 += e.GetLayOver(O.items, i);
                    }

                    if ((O.items[i].MarketingCarrier == '6E') && ($.trim(O.items[i].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" />' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>'
                    }

                    var ftme = e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + '';
                    if (O.items[0].Trip == "I" && O.items[0].Provider == "1G") {
                        ftme = O.items[i].TripCnt + ' HRS';
                    }
                    //else if (O.items[0].Trip == "I") {
                    //    ftme = "&nbsp;";
                    //}

                    //sanjeet//
                    //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="col-md-3 col-xs-3">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time">' + O.items[i].DepartureCityName + '<span>' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span></p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Departure_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].DepartureTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';

                    str1 += '<div class="col-md-4 col-xs-2">';
                    str1 += '<div class="fly">';
                    str1 += '<div class="fly1">';
                    str1 += '<span class="jtm">' + ftme + '</span>';
                    str1 += '</div>';
                    str1 += '<div class="fly2">';
                    str1 += '<span class="fart fart3">' + O.items[i].AdtFareType + '</span>';
                    str1 += '</div>';
                    str1 += '</div>';
                    str1 += '</div>';


                    //ftme = O.items[i].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';

                    str1 += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '</span> ' + O.items[i].ArrivalCityName + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Arrival_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].ArrivalTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';


                }
            }
            for (var ff = 2; ff <= ft; ff++) {
                var R = JSLINQ(OB.items)
                    .Where(function (item) { return item.Flight == ff.toString(); })
                    .Select(function (item) { return item });

                if (R.items.length > 0) {

                    var totDur = "";

                    if (R.items[0].Provider == "TB") {
                        totDur = e.GetTotalDuration(R.items);
                    }
                    else {
                        totDur = e.MakeupTotDur(R.items[0].TotDur)
                    }

                    str1 += '</div><div class="" style="position:relative;top:12px;"><div class="large-12 medium-12 small-12 bld" style="position: relative;top: -10px;float: left;width: 100%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + totDur + '</div><div class="clear"></div>';
                    for (var j = 0; j < R.items.length; j++) {

                        if (j >= 1 && R.items.length > 1 && j < R.items.length) {
                            str1 += e.GetLayOver(R.items, j);
                        }

                        if ((R.items[j].MarketingCarrier == '6E') && ($.trim(R.items[j].sno).search("INDIGOCORP") >= 0)) {
                            str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        else {
                            str1 += '<div class="col-md-2 col-xs-3"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        var Ftmer = e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS';
                        debugger;
                        if (R.items[0].Trip == "I" && R.items[0].Provider == "1G") {
                            Ftmer = R.items[j].TripCnt + ' HRS';


                        }
                        //else if (R.items[0].Trip == "I") {
                        //    Ftmer = "&nbsp;";
                        //}
                        //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + Ftmer + '</div>'
                        //Kunal Work
                        //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'

                        str1 += '<div class="col-md-3 col-xs-3">';
                        str1 += '<div class="theme-search-results-item-flight-section-meta">';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-time">' + R.items[j].DepartureLocation + '<span>' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span></p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + R.items[j].Departure_Date + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + R.items[j].DepartureTerminal + '</p>';
                        str1 += '</div > ';
                        str1 += '</div > ';

                        str1 += '<div class="col-md-4 col-xs-2">';
                        str1 += '<div class="fly">';
                        str1 += '<div class="fly1">';
                        str1 += '<span class="jtm">' + ftme + '</span>';
                        str1 += '</div>';
                        str1 += '<div class="fly2">';
                        str1 += '<span class="fart fart3">' + O.items[j].AdtFareType + '</span>';
                        //str1 += '<span class="fmeal">Free Meal</span>';
                        str1 += '</div>';
                        str1 += '</div>';
                        str1 += '</div>';

                        str1 += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
                        str1 += '<div class="theme-search-results-item-flight-section-meta">';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-time"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '</span> ' + R.items[j].ArrivalLocation + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + R.items[j].Arrival_Date + '</p>';
                        str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + R.items[j].ArrivalTerminal + '</p>';
                        str1 += '</div > ';
                        str1 += '</div > ';
                    }
                }
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
            //    }
            //} catch (ex) { }
            str1 += '<div class="clear"></div>';
            str1 += '</div></div><div class="clear"></div>';
            str1 += '<div class="clear"></div>';
            $('#' + this.rel + '_fltdt').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + this.rel + '_fltdt').show();
            //$('#' + this).hide();
        }
    });




    $('.fltBagDetails').click(function (event) {

        event.preventDefault();
        // var main = $.parseJSON($('#' + this.rel + 'M').html());
        //   
        if (this.rel != null) {
            $('#' + this.rel + '_').slideUp();
            var lineNums = this.rel; //.split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };


            // var airc = lineNums[1] + "_" + lineNums[2];
            var OB = JSLINQ(result[0])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });

            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });

            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });
            var str1 = '<div>';
            if (O.items.length > 0) {
                str1 += '<div class=""><div></div>';
                str1 += '<table class="rtable"><tr><td  class="w50 f16 bld">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
                for (var i = 0; i < O.items.length; i++) {
                    str1 += '<tr><td>' + O.items[i].DepartureCityName + '-' + O.items[i].ArrivalCityName + '(' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(O.items[i].BagInfo) + '</td></tr>';
                }
                str1 += '</table>';
                str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';

                str1 += '</div>';
            }
            if (R.items.length > 0) {
                str1 += '<div class="depcity1">';
                str1 += '<table class="w100 f12">';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<tr><td class="w50">' + R.items[j].DepartureCityName + '-' + R.items[j].ArrivalCityName + '(' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(R.items[j].BagInfo) + '</td></tr>';
                }
                str1 += '</table>';

                str1 += '</div>';
            }
            //str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            //str1 += ' <div class"rgt" onclick="Close(\'' + this.rel + '_\');" >X</div
            $('#' + this.rel + '_bag').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + this.rel + '_bag').show();
            // $('#' + this.id).toggleClass("fltDetailslink1");
            //$('#' + this).hide();
        }
    });




};

ResHelper.prototype.GetLayOver = function (items, position) {
    var t = this;
    var str1 = "";

    var layoverTime = t.calFlightDur(items[position - 1].ArrivalTime.replace(":", ""), items[position].DepartureTime.replace(":", ""));

    //str1 = '<div class="layover"> + <span> <i class="ico ico-time">&nbsp;</i> Layover: <strong> </strong> </span> <span class="pipe"> | </span> <time> Time: <strong></strong> </time>  </div>';
    str1 = '<div class="stm"><span class="lay">Layover:' + items[position].DepartureCityName + ' (' + items[position].DepartureLocation + ') - ' + layoverTime + '</span></div>';
    return str1;
};

ResHelper.prototype.GetTotalDuration = function (items) {
    var t = this;
    var hr = 0;
    var mn = 0;
    for (var i = 0; i < items.length; i++) {


        if (i > 0) {


            var layoverTime = t.calFlightDur(items[i - 1].ArrivalTime.replace(":", ""), items[i].DepartureTime.replace(":", ""));

            hr = hr + parseInt(layoverTime.split(':')[0]);
            mn = mn + parseInt(layoverTime.split(':')[1]);


        }

        hr = hr + parseInt(items[i].TripCnt.split(':')[0]);
        mn = mn + parseInt(items[i].TripCnt.split(':')[1]);



    }



    if (mn > 60) {
        var hrs = parseInt(mn / 60);
        var rmin = mn % 60;
        hr = hr + hrs;
        mn = rmin;
    }


    var maxH = this.getFourDigitTime($.trim(hr.toString()));
    var maxT = this.getFourDigitTime($.trim(mn.toString()));
    return [maxH.slice(2), ":", maxT.slice(2)].join('');
}


function Close(id) {
    $('#' + id).hide();
}


ResHelper.prototype.FormatedDate = function (txtDate) {
    var dd = "";

    var weekday = new Array(7);
    weekday[0] = "Sun";
    weekday[1] = "Mon";
    weekday[2] = "Tue";
    weekday[3] = "Wed";
    weekday[4] = "Thu";
    weekday[5] = "Fri";
    weekday[6] = "Sat";

    var m_names = new Array("Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul", "Aug", "Sep",
        "Oct", "Nov", "Dec");

    var ArrivaldateArr = txtDate.split('/');
    //var arrDate = new Date(parseFloat(ArrivaldateArr[2]) - 1, (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) + 1));

    //dd = weekday[arrDate.getDay()] + ", " + ArrivaldateArr[0] + " " + m_names[parseFloat(ArrivaldateArr[1]) - 1]

    //return dd;

    let strDatefor = (parseInt(ArrivaldateArr[2]) + "-" + parseInt(ArrivaldateArr[1]) + "-" + parseInt(ArrivaldateArr[0]));
    let datedel = new Date(strDatefor);
    let weekdayname = weekday[datedel.getDay()];
    let yearname = m_names[parseInt(ArrivaldateArr[1]) - 1];

    dd = weekdayname + ", " + ArrivaldateArr[0] + " " + yearname;

    return dd;

}

function getTheDays(currdate) {
    var dt = currdate;
    var month = dt.getMonth(),
        year = dt.getFullYear(),
        day = dt.getDate();

    // GET THE FIRST AND LAST DATE OF THE MONTH.
    var FirstDay = new Date(year, month, day);
    //var LastDay = new Date(year, month + 1, day);

    // FINALLY, GET THE DAY.
    var weekday = new Array();
    weekday[0] = "Sun";
    weekday[1] = "Mon";
    weekday[2] = "Tue";
    weekday[3] = "Wed";
    weekday[4] = "Thu";
    weekday[5] = "Fri";
    weekday[6] = "Sat";

    var m_names = new Array("Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul", "Aug", "Sep",
        "Oct", "Nov", "Dec");

    //if (typeof weekday[FirstDay.getDay()] != 'undefined') {     // CHECK FOR 'undefined'.
    //    document.getElementById('fday').innerHTML = weekday[FirstDay.getDay()] +
    //        ' (' + FirstDay.toDateString('dd/mon/yyyy') + ')';
    //    document.getElementById('lday').innerHTML = weekday[LastDay.getDay()] +
    //        ' (' + LastDay.toDateString('dd/mon/yyyy') + ')';;
    //}
    //else {
    //    document.getElementById('fday').innerHTML = '';
    //    document.getElementById('lday').innerHTML = '';
    //}

    var hggh = weekday[FirstDay.getDay()] + ", " + day + " " + m_names[month];

    return hggh;
}

ResHelper.prototype.GetSearchDisplay = function (c, triptype) {
    var t = this;
    var st = "";
    var nxtdt = "";
    var prvdt = "";
    var ondt = "";
    var opdt = "";
    var rndt = "";
    var rpdt = "";

    var cd_next = "";
    var cd_prev = "";

    //Shri kant
    let currdate = c.DepDate.split('/');
    var cd_prev_date = new Date(currdate[2] + "-" + currdate[1] + "-" + currdate[0]);
    cd_prev_date.setDate(cd_prev_date.getDate() - 1);
    var prev_dd = String(cd_prev_date.getDate()).padStart(2, '0');
    var prev_mm = String(cd_prev_date.getMonth() + 1).padStart(2, '0');
    var prev_yyyy = cd_prev_date.getFullYear();
    cd_prev = prev_dd + "/" + prev_mm + "/" + prev_yyyy;

    var cd_next_date = new Date(currdate[2] + "-" + currdate[1] + "-" + currdate[0]);
    cd_next_date.setDate(cd_next_date.getDate() + 1);
    var next_dd = String(cd_next_date.getDate()).padStart(2, '0');
    var next_mm = String(cd_next_date.getMonth() + 1).padStart(2, '0');
    var next_yyyy = cd_next_date.getFullYear();
    cd_next = next_dd + "/" + next_mm + "/" + next_yyyy;


    //cd_next = parseInt(c.DepDate.split("/")[0]) + 1;

    //cd_prev = parseInt(c.DepDate.split("/")[0]) - 1;

    //var cdep = cd_prev + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    //var cden = cd_next + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    //nxtdt += '<span>' + t.FormatedDate(cden) + '</span>';
    //prvdt += '<span>' + t.FormatedDate(cdep) + '</span>';

    nxtdt += '<span>' + getTheDays(cd_next_date) + '</span>';
    prvdt += '<span>' + getTheDays(cd_prev_date) + '</span>';

    var o_next = "";
    var o_prev = "";
    o_next = parseInt(c.DepDate.split("/")[0]) + 1;

    o_prev = parseInt(c.DepDate.split("/")[0]) - 1;

    var odep = o_prev + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    var oden = o_next + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    ondt += '<span style="font-size:10px;">' + t.FormatedDate(oden) + '</span>';
    opdt += '<span style="font-size:10px;">' + t.FormatedDate(odep) + '</span>';

    var r_next = "";
    var r_prev = "";
    r_next = parseInt(c.DepDate.split("/")[0]) + 1;

    r_prev = parseInt(c.DepDate.split("/")[0]) - 1;

    var rdep = r_prev + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    var rden = r_next + "/" + c.DepDate.split("/")[1] + "/" + c.DepDate.split("/")[2];
    rndt += '<span style="font-size:10px;">' + t.FormatedDate(rden) + '</span>';
    rpdt += '<span style="font-size:10px;">' + t.FormatedDate(rdep) + '</span>';


    t.nextdate.html(nxtdt);
    t.prevdate.html(prvdt);

    t.ondt.html(ondt);
    t.opdt.html(opdt);

    t.rndt.html(rndt);
    t.rpdt.html(rpdt);



    if (triptype == "rdbOneWay") {

        st += '<div class="col-md-4 col-xs-3">'
        st += '<div class="WID theme-search-area-section first theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">'
        st += '<i class="fa fa-plane p-n" style="background: #fff; color: #000; padding: 5px 9px; border-radius: 50% 50% 50% 50%; margin: 0.1em; font-size: 2em;"></i>'
        st += '<p class="theme-login-terms" style="color: white; position: relative; margin-top: -37px; margin-left: 28px; font-weight: 800;">'
        st += t.hidtxtDepCity1.val().split(', ')[0] + '';
        st += '<br>'
        st += '<a style="color: #fff; font-weight: initial;">' + t.FormatedDate(c.DepDate) + ' <span class="mob-pax" >| Adult ' + c.Adult + '  Child ' + c.Child + '  Infant ' + c.Infant + '</span></a>'
        st += '</p>'
        st += '</div>'
        st += '</div>'

        st += '<div class="col-md-2 col-xs-2"><div class="pln"><img class="r-plane" src="../Images/transporteplane.png" style="width:73px;"/></div></div>'


        st += '<div class="col-md-4 col-xs-3">'
        st += '<div class="WID theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">'
        st += '<i class="fa fa-plane p-n" style="background: #fff; color: #000; padding: 5px 9px; border-radius: 50% 50% 50% 50%; margin: 0.1em; transform: rotate(90deg); font-size: 2em;"></i>'
        st += '<p class="theme-login-terms" style="color: white; position: relative; margin-top: -37px; margin-left: 28px; font-weight: 800;">'
        st += t.hidtxtArrCity1.val().split(',')[0] + '';
        st += '<br>'
        st += '<a style="color: #fff; font-weight: initial;">' + t.FormatedDate(c.DepDate) + ' <span class="mob-pax" >| Adult ' + c.Adult + '  Child ' + c.Child + '  Infant ' + c.Infant + '</span></a>'
        st += '</p>'
        st += '</div>'
        st += '</div>'



        //st += ' <div class="lft plft10">';
        //st += '  <div class="lft ">';
        //st += t.hidtxtDepCity1.val().split(',')[0] + '<br />';
        ////st +=  c.DepartureCity.split('(')[1].replace(')', '')

        //st += ' </div>';

        //st += '  <div class="lft plft10">';
        //st += t.hidtxtArrCity1.val().split(',')[0] + '<br />';
        //// st += '  <span class="f10 txtgray">Fri 7 Mar </span>' ;
        //st += '  </div>&nbsp;&nbsp;| <span class="f10 txtgray" style="font-size: 13px !important;">' + t.FormatedDate(c.DepDate) + '</span>';
        //st += ' </div>';
        // st += '<div class="bdrdot lft">&nbsp;</div>';
        t.DisplaySearchinput.html(st);
        // t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        //t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        /// f = "<b>" + f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant </b>";
        // t.CS.html(f)
        //c.RetDate = c.DepDate;
    }
    else if (triptype == "rdbMultiCity") {




        st += ' <div class="lft plft10">';
        st += '  <div class="lft ">';
        st += t.hidtxtDepCity1.val().split(',')[0] + '<br />';
        st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate) + '</span>';
        st += ' </div>';
        st += ' <div class="lft">';
        st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
        st += '  <div class="lft plft10">';
        st += t.hidtxtArrCity1.val().split(',')[0] + '<br />';
        st += '  </div>';
        st += ' </div>';
        st += '<div class="bdrdot lft">&nbsp;</div>';
        if (c.DepartureCity2 != "" && c.ArrivalCity2 != "" && c.DepDate2 != "") {
            // f = f + c.DepartureCity2 + " To " + c.ArrivalCity2 + " On  " + c.DepDate2 + "  <br />";


            st += ' <div class="lft plft10">';
            st += '  <div class="lft ">';
            st += t.hidtxtDepCity2.val().split(',')[0] + '<br />';
            st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate2) + '</span>';
            st += ' </div>';
            st += ' <div class="lft">';
            st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity2.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        if (c.DepartureCity3 != "" && c.ArrivalCity3 != "" && c.DepDate3 != "") {
            // f = f + c.DepartureCity3 + " To " + c.ArrivalCity3 + " On  " + c.DepDate3 + "  <br />";

            st += ' <div class="lft plft10">';
            st += '  <div class="lft ">';
            st += t.hidtxtDepCity3.val().split(',')[0] + '<br />';
            st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate3) + '</span>';
            st += ' </div>';
            st += ' <div class="lft">';
            st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity3.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        if (c.DepartureCity4 != "" && c.ArrivalCity4 != "" && c.DepDate4 != "") {
            // f = f + c.DepartureCity4 + " To " + c.ArrivalCity4 + " On  " + c.DepDate4 + "  <br />";
            st += ' <div class="lft plft10">';
            st += '  <div class="lft ">';
            st += t.hidtxtDepCity4.val().split(',')[0] + '<br />';
            st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate4) + '</span>';
            st += ' </div>';
            st += ' <div class="lft">';
            st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity3.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        if (c.DepartureCity5 != "" && c.ArrivalCity5 != "" && c.DepDate5 != "") {
            //f = f + c.DepartureCity5 + " To " + c.ArrivalCity5 + " On  " + c.DepDate5 + "  <br />";
            st += ' <div class="lft plft10">';
            st += '  <div class="lft ">';
            st += t.hidtxtDepCity5.val().split(',')[0] + '<br />';
            st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate5) + '</span>';
            st += ' </div>';
            st += ' <div class="lft">';
            st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity5.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        if (c.DepartureCity6 != "" && c.ArrivalCity6 != "" && c.DepDate6 != "") {
            //f = f + c.DepartureCity6 + " To " + c.ArrivalCity6 + " On  " + c.DepDate6 + "  <br />";
            st += ' <div class="lft plft10">';
            st += '  <div class="lft ">';
            st += t.hidtxtDepCity6.val().split(',')[0] + '<br />';
            st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate6) + '</span>';
            st += ' </div>';
            st += ' <div class="lft">';
            st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity5.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            // st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        t.DisplaySearchinput.html(st);

    }
    else if (triptype == "rdbRoundTrip" || triptype == "rdbRoundTripF") {


        st += '<div class="col-md-4 col-xs-3">'
        st += '<div class="WID theme-search-area-section first theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">'
        st += '<i class="fa fa-plane p-n" style="background: #fff; color: #000; padding: 5px 9px; border-radius: 50% 50% 50% 50%; margin: 0.1em; font-size: 2em;"></i>'
        st += '<p class="theme-login-terms" style="color: white; position: relative; margin-top: -37px; margin-left: 28px; font-weight: 800;">'
        st += t.hidtxtDepCity1.val().split(', ')[0] + '';
        st += '<br>'
        st += '<a style="color: #fff; font-weight: initial;">' + t.FormatedDate(c.DepDate) + ' <span class="mob-pax" >| Adult ' + c.Adult + '  Child ' + c.Child + '  Infant ' + c.Infant + '</span></a>'
        st += '</p>'
        st += '</div>'
        st += '</div>'

        st += '<div class="col-md-2 col-xs-2"><div class="pln"><img class="r-plane" src="../Images/returnplane.png" /></div></div>'


        st += '<div class="col-md-4 col-xs-3">'
        st += '<div class="WID theme-search-area-section theme-search-area-section-curved theme-search-area-section-sm theme-search-area-section-fade-white theme-search-area-section-no-border">'
        st += '<i class="fa fa-plane p-n" style="background: #fff; color: #000; padding: 5px 9px; border-radius: 50% 50% 50% 50%; margin: 0.1em; transform: rotate(90deg); font-size: 2em;"></i>'
        st += '<p class="theme-login-terms" style="color: white; position: relative; margin-top: -37px; margin-left: 28px; font-weight: 800;">'
        st += t.hidtxtArrCity1.val().split(',')[0] + '';
        st += '<br>'
        //st += '<a style="color: white; font-weight: initial;">' + t.FormatedDate(c.RetDate) + ' | <img src="../Advance_CSS/Icons/adult.png" title="Adult" style="width:15px;"/> ' + c.Adult + '  <img src="../Advance_CSS/Icons/child.png" title="Child" style="width:15px;"/> ' + c.Child + '  <img src="../Advance_CSS/Icons/infant.png" title="Infant" style="width:15px;"/> ' + c.Infant + '</a>'
        st += '<a style="color: #fff; font-weight: initial;">' + t.FormatedDate(c.DepDate) + ' <span class="mob-pax" >| Adult ' + c.Adult + '  Child ' + c.Child + '  Infant ' + c.Infant + '</span></a>'

        st += '</p>'
        st += '</div>'
        st += '</div>'





        //st += ' <div class="lft plft10">';
        //st += '  <div class="lft ">';
        //st += t.hidtxtDepCity1.val().split(',')[0] + '<br />';
        //st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate) + '</span>';
        //st += ' </div>';
        //st += ' <div class="lft">';
        //st += '  <i aria-hidden="true" style="color:#fff;">⟶</i></div>';
        //st += '  <div class="lft plft10">';
        //st += t.hidtxtArrCity1.val().split(',')[0] + '<br />';
        //st += '  <span class="f10 txtgray">' + t.FormatedDate(c.RetDate) + '</span>';
        //st += '  </div>';
        //st += ' </div>';
        //st += '<div class="bdrdot lft">&nbsp;</div>';
        t.DisplaySearchinput.html(st);
        //if (n == "D") {
        //    t.prexnt.hide();
        //}
        //f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        //l = c.ArrivalCity + " To " + c.DepartureCity + " On  " + c.RetDate;
        //var h = f + "<br/>" + l;
        //t.searchquery.html(h);
        //f = f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        //l = l + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        //// t.SearchTextDiv.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());
        //t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());

        //t.RTFTextFrom.html(t.hidtxtDepCity1.val().split(',')[0] + " - " + t.hidtxtArrCity1.val().split(',')[0] + " " + t.txtDepDate.val());
        //t.RTFTextTo.html(t.hidtxtArrCity1.val().split(',')[0] + " - " + t.hidtxtDepCity1.val().split(',')[0] + " " + t.txtRetDate.val());
        //t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        //t.flterTabO.html(t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0]);
        //t.flterTabR.html(t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0]);
        // h = "<b>" + h + "</b>";
        //t.CS.html(h)
    }


}


ResHelper.prototype.BagInfo = function (bag) {

    var e = this;
    if (bag != null && bag.toString() != '') {

        var rs = '';
        if ($.trim(bag).toUpperCase().search('PC') > 0) {
            rs = $.trim(bag).replace('PC', ' Piece(s) Baggage included.');
        }
        else if (($.trim(bag).toUpperCase().search('K') > 0) && ($.trim(bag).toString().length < 4)) {
            rs = $.trim(bag).replace('K', ' Kg Baggage included.');
        }
        else {
            rs = bag;
        }
    }
    else {
        rs = "Baggage information not available."
    }
    return rs;
}

ResHelper.prototype.TerminalAirportInfo = function (terminal, airport) {
    var str = '';

    if (terminal != null && terminal != '' && airport != null && airport != '') {
        str = airport.toString().replace("Airport", "").replace("airport", "") + ' Airport, Terminal  ' + terminal;
    }
    else if (terminal != null && terminal != '' && (airport == null || airport == '')) {
        str = 'Terminal  ' + terminal;
    }
    else if ((terminal == null || terminal == '') && airport != null && airport != '') {
        str = airport.toString().replace("Airport", "").replace("airport", "") + ' Airport';
    }
    return str;
};

ResHelper.prototype.GetFltDetailsR = function (result) {
    var e = this;

    $('.fltDetailslinkR').click(function (event) {
        var lineNumshd;
        if ($(this).attr("rel") != null) {
            lineNumshd = $(this).attr("rel").split('_');
            lineNums = $(this).attr("id").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            //var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");
            //$('#' + lineNumshd[0] + '_').slideUp();

            // var airc = lineNums[1] + "_" + lineNums[2];

            var OB;
            if ($.trim(lineNums[1]) == "O") {
                OB = JSLINQ(result[0])
                    .Where(function (item) { return item.LineNumber == lineNums[0]; })
                    .Select(function (item) { return item });
            }
            else if ($.trim(lineNums[1]) == "R") {
                OB = JSLINQ(result[1])
                    .Where(function (item) { return item.LineNumber == lineNums[0]; })
                    .Select(function (item) { return item });
            }
            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });
            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });
            var str1 = '';
            str1 += '<div class="">';
            //str1 += '<div>';
            //str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; right:3px;font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');">X</div>';
            //str1 += '<div class="large-12 medium-12 small-12 bld">Flight Details</div><div>';

            if (O.items.length > 0) {
                try {
                    if ((parseInt(O.items[0].AvailableSeats1) <= 5) && (O.items[0].ValiDatingCarrier != 'SG')) {

                        //seats-kunal
                        //str1 += '<div class="colorwht lft" style="background:#004b91; padding:2px 5px; border-radius:4px; color:#fff; position:relative; top:6px;">' + O.items[0].AvailableSeats1 + ' Seat(s) Left!</div><div class="clear1"></div>';
                    }
                } catch (ex) { }


                for (var i = 0; i < O.items.length; i++) {
                    if (i == 0) {
                        var totDur = "";

                        if (O.items[0].Provider == "TB") {
                            totDur = e.GetTotalDuration(O.items);
                        }
                        else {
                            totDur = e.MakeupTotDur(O.items[0].TotDur)
                        }

                        //str1 += '<div class="large-12 medium-12 small-12 bld" style="position: relative;right: 10px;top: -5px;float: left;width: 112%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;"><span class="f20">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span> | <span class="f16" style="color: #979797;font-size: 11px;">' + totDur + '</span></div><div class="clear1"></div>';
                    }
                    else {
                        str1 += '<div class="clear1"></div><hr /><div class="clear1"></div><div class="large-12 medium-12 small-12 bld">';
                    }
                    if ((O.items[i].MarketingCarrier == '6E') && ($.trim(O.items[i].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="col-md-2"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="col-md-2"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }

                    var ftme = e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS';
                    if (O.items[0].Trip == "I") {
                        ftme = O.items[i].TripCnt + ' HRS';
                    }
                    //else if (O.items[0].Trip == "I") {
                    //    ftme = "&nbsp;";
                    //}
                    //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + ftme + '</div>';
                    //str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '</div>';
                    //str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>';
                    //str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '</div><div class="clear"></div></div>';

                    //str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].Departure_Date + '</div>';

                    str1 += '<div class="col-md-3 col-xs-3">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time">' + O.items[i].DepartureLocation + '<span>' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span></p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Departure_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].DepartureTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';


                    //str1 += '<div class="large-2 medium-2 small-2 columns bld" style="margin-left: 30px;"><span style="position: relative;">' + ftme + '</span><br><div class="flt-dur"><div class="dot"></div><div class="dot-plan"><i class="fa fa-plane" style="background: #fff;color: #b8b8b8;"></i></div><div class="dot2"></div></div><span style="text-align: center;border: 1px solid #bcbcbc;margin: 16px -23px auto;border-radius: 23px;color: orange;padding: 3px 0;font-size: 11px;display: block;">' + O.items[0].AdtFareType + '</span></div>';

                    str1 += '<div class="col-md-4 col-xs-2">';
                    str1 += '<div class="fly">';
                    str1 += '<div class="fly1">';
                    str1 += '<span class="jtm">' + ftme + '</span>';
                    str1 += '</div>';
                    str1 += '<div class="fly2">';
                    str1 += '<span class="fart fart3">' + O.items[i].AdtFareType + '</span>';
                    str1 += '</div>';
                    str1 += '</div>';
                    str1 += '</div>';


                    //ftme = O.items[i].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';

                    //str1 += '<div class="large-3 medium-3 small-3 columns" style="position: relative;right: -77px;"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].Arrival_Date + '</div><div class="clear"></div>';


                    str1 += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
                    str1 += '<div class="theme-search-results-item-flight-section-meta">';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-time"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '</span> ' + O.items[i].ArrivalLocation + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-date">' + O.items[i].Arrival_Date + '</p>';
                    str1 += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - ' + O.items[i].ArrivalTerminal + '</p>';
                    str1 += '</div > ';
                    str1 += '</div > ';

                    str1 += '</br> ';
                    if (i >= 1 && O.items.length > 1 && i < O.items.length - 1) {
                        str1 += e.GetLayOver(O.items, i);
                    }
                }
            }
            if (R.items.length > 0) {


                var totDur = "";

                if (R.items[0].Provider == "TB") {
                    totDur = e.GetTotalDuration(R.items);
                }
                else {
                    totDur = e.MakeupTotDur(R.items[0].TotDur)
                }

                str1 += '</div><div class="depcity1"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + totDur + '<div class="clear"></div>';
                for (var j = 0; j < R.items.length; j++) {
                    if ((R.items[j].MarketingCarrier == '6E') && ($.trim(R.items[j].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>';
                    }

                    var Ftmer = e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS';

                    if (R.items[0].Trip == "I") {
                        Ftmer = R.items[j].TripCnt + ' HRS';
                    }
                    //else if (R.items[0].Trip == "I") {
                    //    Ftmer = "&nbsp;";
                    //}
                    str1 += '<div class="large-2 medium-2 small-2 columns bld">' + Ftmer + '</div>'
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + e.TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                    //str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R.items[j].ArrivalLocation + '</span><br />' + R.items[j].ArrivalCityName + '<br />' + R.items[j].Arrival_Date + '<br />' + e.TerminalAirportInfo(R.items[j].ArrivalTerminal, R.items[j].ArrivalAirportName) + '</div><div class="clear"></div>';
                    if (j >= 1 && R.items.length > 1 && j < R.items.length - 1) {
                        str1 += e.GetLayOver(R.items, j);
                    }
                }
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
            //    }
            //} catch (ex) { }
            str1 += '<div class="clear"></div>';
            str1 += '</div></div>';

            //str1 += '</div></div>';
            str1 += '<div class="clear"></div>';
        }
        if (lineNumshd[1] == "O") {
            $('#' + lineNums[0] + '_Obdfltdt').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + lineNumshd[0] + '_').slideToggle();
        }
        else {
            $('#' + lineNums[0] + '_RO').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + lineNumshd[0] + '_RO').slideToggle();
        }
        //$('#' + this).hide();
        //$(this).next()[0].innerHTML = str1;
        //$(this).next().fadeToggle(1000);
    });



    $('.fltBagDetailsR').click(function (event) {
        var lineNumsbghd;
        if ($(this).attr("rel") != null) {
            lineNumsbghd = $(this).attr("rel").split('_');
            var lineNums = $(this).attr("id").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            // var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");

            // var airc = lineNums[1] + "_" + lineNums[2];

            var OB;
            if ($.trim(lineNums[1]) == "O") {
                OB = JSLINQ(result[0])
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });
            }
            else if ($.trim(lineNums[1]) == "R") {
                OB = JSLINQ(result[1])
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });
            }
            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });
            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });
            var str1 = '';
            str1 += '<div>';
            //str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');" title="Click to close Details"><i class="fa fa-times-circle"></i></div>';
            //str1 += '<div style="position: relative;right: 10px;top: -5px;float: left;width: 112%;padding: 2px 0px 3px 3px;background: #eeeeee;border-bottom: 1px solid #a2a2a2;font-weight: 600;">Baggage Details</div>';

            if (O.items.length > 0) {
                str1 += '<div class="depcity">';
                str1 += '<table class="w100 f12" style="padding: 10px; background: #ffffff; box-shadow:0px 0px 15px #333; border: 6px solid #eee;"><tr><td class="f16 bld w50">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
                for (var i = 0; i < O.items.length; i++) {
                    str1 += '<tr><td>' + O.items[i].DepartureCityName + '-' + O.items[i].ArrivalCityName + '(' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(O.items[i].BagInfo) + '</td></tr>';

                }
                str1 += '</table></div>';
            }
            if (R.items.length > 0) {
                str1 += '<div class="depcity1">';
                str1 += '<table class="w100 f12" style="padding: 10px; background: #ffffff; box-shadow:0px 0px 15px #333; border: 6px solid #eee;">';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<tr><td class="w50">' + R.items[j].DepartureCityName + '-' + R.items[j].ArrivalCityName + '(' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(R.items[j].BagInfo) + '</td></tr>';
                }
                str1 += '</table></div>';
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
            //    }
            //} catch (ex) { }
            str1 += '<p>The information presented above is as obtained from the airline reservation system. Faretripbox does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</p>';
            str1 += '<div class="clear1"></div>';
            str1 += '</div></div>';

        }

        if (lineNumsbghd[1] == "O") {
            $('#' + lineNums[0] + '_Obdbag').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + lineNumsbghd[0] + '_').slideToggle();
        }
        if (lineNumsbghd[1] == "R") {
            $('#' + lineNums[0] + '_RO').html(str1);
            //$('#' + this.rel + '_').show();
            //$('#' + lineNumsbghd[0] + '_RO').slideToggle();
        }
        //$('#' + idr).html(str1);
        //$('#' + idr).fadeToggle(1000);
    });



};

ResHelper.prototype.GetResult = function (e) {
    var Obook = null;
    var Rbook = null;
    applyfilterStatus = false;

    var t = e;
    t.MainSF.hide();
    var n;
    var r = $("input[name='TripType']:checked").val();
    var i = t.hidtxtDepCity1.val().split(",");
    var s = t.hidtxtArrCity1.val().split(",");

    var d2 = "";
    if (t.hidtxtDepCity2.val().split(",").length > 1) {
        d2 = t.hidtxtDepCity2.val().split(",")[1].trim();
    }
    else {
        d2 = "IN";
    }
    var a2 = "";
    if (t.hidtxtArrCity2.val().split(",").length > 1) {
        a2 = t.hidtxtArrCity2.val().split(",")[1].trim();
    }
    else {
        a2 = "IN";
    }
    var d3 = "";
    if (t.hidtxtDepCity3.val().split(",").length > 1) {
        d3 = t.hidtxtDepCity3.val().split(",")[1].trim();
    }
    else {
        d3 = "IN";
    }
    var a3 = "";
    if (t.hidtxtArrCity3.val().split(",").length > 1) {
        a3 = t.hidtxtArrCity3.val().split(",")[1].trim();
    }
    else {
        a3 = "IN";
    }
    var d4 = "";
    if (t.hidtxtDepCity4.val().split(",").length > 1) {
        d4 = t.hidtxtDepCity4.val().split(",")[1].trim();
    }
    else {
        d4 = "IN";
    }
    var a4 = "";
    if (t.hidtxtArrCity4.val().split(",").length > 1) {
        a4 = t.hidtxtArrCity4.val().split(",")[1].trim();
    }
    else {
        a4 = "IN";
    }
    var d5 = "";
    if (t.hidtxtDepCity5.val().split(",").length > 1) {
        d5 = t.hidtxtDepCity5.val().split(",")[1].trim();
    }
    else {
        d5 = "IN";
    }
    var a5 = "";
    if (t.hidtxtArrCity5.val().split(",").length > 1) {
        a5 = t.hidtxtArrCity5.val().split(",")[1].trim();
    }
    else {
        a5 = "IN";
    }
    var d6 = "";
    if (t.hidtxtDepCity6.val().split(",").length > 1) {
        d6 = t.hidtxtDepCity6.val().split(",")[1].trim();
    }
    else {
        d6 = "IN";
    }
    var a6 = "";
    if (t.hidtxtArrCity6.val().split(",").length > 1) {
        a6 = t.hidtxtArrCity6.val().split(",")[1].trim();
    }
    else {
        a6 = "IN";
    }




    if (i[1] == "IN" && s[1] == "IN" && a2 == "IN" && a3 == "IN" && a4 == "IN" && a5 == "IN" && a6 == "IN" && d2 == "IN" && d3 == "IN" && d4 == "IN" && d5 == "IN" && d6 == "IN") {
        n = "D"
    } else {
        n = "I"
    }
    var o = new Boolean; $(this)
    if (t.chkNonstop.is(":checked") == true) {
        o = true
    } else {
        o = false
    }
    var u = new Boolean;
    if (t.LCC_RTF.is(":checked") == true) {
        u = true
    } else {
        u = false
    }
    var a = new Boolean;
    if (t.GDS_RTF.is(":checked") == true) {
        a = true
    } else {
        a = false
    }
    var f;
    var l;
    var c = {
        Trip1: n,
        TripType1: r,
        DepartureCity: t.txtDepCity1.val(),
        ArrivalCity: t.txtArrCity1.val(),
        HidTxtDepCity: t.hidtxtDepCity1.val(),
        HidTxtArrCity: t.hidtxtArrCity1.val(),
        Adult: t.Adult.val(),
        Child: t.Child.val(),
        Infant: t.Infant.val(),
        Cabin: t.Cabin.val(),
        AirLine: t.txtAirline.val(),
        HidTxtAirLine: t.hidtxtAirline.val(),
        DepDate: t.txtDepDate.val(),
        RetDate: t.txtRetDate.val(),
        RTF: u,
        NStop: o,
        GDSRTF: a,

        //Add for Multicity  02-01-2017 Devesh

        DepartureCity2: t.txtDepCity2.val(),
        ArrivalCity2: t.txtArrCity2.val(),
        HidTxtDepCity2: t.hidtxtDepCity2.val(),
        HidTxtArrCity2: t.hidtxtArrCity2.val(),
        DepDate2: t.txtDepDate2.val(),

        DepartureCity3: t.txtDepCity3.val(),
        ArrivalCity3: t.txtArrCity3.val(),
        HidTxtDepCity3: t.hidtxtDepCity3.val(),
        HidTxtArrCity3: t.hidtxtArrCity3.val(),
        DepDate3: t.txtDepDate3.val(),

        DepartureCity4: t.txtDepCity4.val(),
        ArrivalCity4: t.txtArrCity4.val(),
        HidTxtDepCity4: t.hidtxtDepCity4.val(),
        HidTxtArrCity4: t.hidtxtArrCity4.val(),
        DepDate4: t.txtDepDate4.val(),

        DepartureCity5: t.txtDepCity5.val(),
        ArrivalCity5: t.txtArrCity5.val(),
        HidTxtDepCity5: t.hidtxtDepCity5.val(),
        HidTxtArrCity5: t.hidtxtArrCity5.val(),
        DepDate5: t.txtDepDate5.val(),

        DepartureCity6: t.txtDepCity6.val(),
        ArrivalCity6: t.txtArrCity6.val(),
        HidTxtDepCity6: t.hidtxtDepCity6.val(),
        HidTxtArrCity6: t.hidtxtArrCity6.val(),
        DepDate6: t.txtDepDate6.val()

    };

    t.GetSearchDisplay(c, r);
    if (r == "rdbOneWay") {
        t.prexnt.show();

        f = c.DepartureCity.split('(')[1].replace(')', '') + " → " + c.ArrivalCity.split('(')[1].replace(')', '') + "<i><image url='https://www.iconfinder.com/data/icons/small-n-flat/24/calendar-512.png' alt=''/></i> " + c.DepDate;
        //f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        t.searchquery.html(f);
        // t.SearchTextDiv.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        // t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        /// f = "<b>" + f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant </b>";
        // t.CS.html(f)
        c.RetDate = c.DepDate;

    }
    else if (r == "rdbMultiCity") {
        t.prexnt.show();
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate + "  <br />";
        if (c.DepartureCity2 != "" && c.ArrivalCity2 != "" && c.DepDate2 != "") {
            f = f + c.DepartureCity2 + " To " + c.ArrivalCity2 + " On  " + c.DepDate2 + "  <br />";
        }

        if (c.DepartureCity3 != "" && c.ArrivalCity3 != "" && c.DepDate3 != "") {
            f = f + c.DepartureCity3 + " To " + c.ArrivalCity3 + " On  " + c.DepDate3 + "  <br />";
        }

        if (c.DepartureCity4 != "" && c.ArrivalCity4 != "" && c.DepDate4 != "") {
            f = f + c.DepartureCity4 + " To " + c.ArrivalCity4 + " On  " + c.DepDate4 + "  <br />";
        }

        if (c.DepartureCity5 != "" && c.ArrivalCity5 != "" && c.DepDate5 != "") {
            f = f + c.DepartureCity5 + " To " + c.ArrivalCity5 + " On  " + c.DepDate5 + "  <br />";
        }

        if (c.DepartureCity6 != "" && c.ArrivalCity6 != "" && c.DepDate6 != "") {
            f = f + c.DepartureCity6 + " To " + c.ArrivalCity6 + " On  " + c.DepDate6 + "  <br />";
        }
        t.searchquery.html(f);
        t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        c.RetDate = c.DepDate;
        e.TripType.click();



    }
    else if (r == "rdbRoundTrip") {

        if (n == "D") {
            t.prexnt.hide();
        }
        f = c.DepartureCity.split('(')[1].replace(')', '') + " → " + c.ArrivalCity.split('(')[1].replace(')', '') + " On  " + c.DepDate;
        l = c.ArrivalCity.split('(')[1].replace(')', '') + " → " + c.DepartureCity.split('(')[1].replace(')', '') + " On  " + c.RetDate;
        //f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        //l = c.ArrivalCity + " To " + c.DepartureCity + " On  " + c.RetDate;
        var h = f + "<br/>" + l;
        t.searchquery.html(h);
        f = f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        l = l + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        // t.SearchTextDiv.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());
        t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());

        t.RTFTextFrom.html(t.hidtxtDepCity1.val().split(',')[0] + " → " + t.hidtxtArrCity1.val().split(',')[0] + " | " + t.txtDepDate.val());
        t.RTFTextTo.html(t.hidtxtArrCity1.val().split(',')[0] + " → " + t.hidtxtDepCity1.val().split(',')[0] + " | " + t.txtRetDate.val());
        t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        t.flterTabO.html(t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0]);
        t.flterTabR.html(t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0]);
        // h = "<b>" + h + "</b>";
        //t.CS.html(h)
    }

    $.blockUI({
        message: $("#waitMessage")
    });

    var IsNRMLRoundTrip = false;
    if (n == "D" && u == false && a == false && r == "rdbRoundTrip") {
        IsNRMLRoundTrip = true;
        roundtripNrml1 = true;
        CommonResultArray.push([]);
        CommonResultArray.push([]);

    }
    else {
        CommonResultArray.push([]);
        roundtripNrml1 = false;
    }

    var rtfStarus1 = ""
    if (n == "D" && u == true && r == "rdbRoundTrip") {
        rtfStarus1 = "LCC"
    }
    else if (n == "D" && a == true && r == "rdbRoundTrip") {
        rtfStarus1 = "GDS"
    }

    var airlineCode = "";
    if ($.trim(c.HidTxtAirLine) != '') {
        airlineCode = c.HidTxtAirLine.split(',')[1];
    }


    var trip = $("input[name='TripType']:checked").val();
    if (trip == "rdbMultiCity") {


        $("#two").show();
        $("#three").show();
        $("#add").show();
        $("#minus").hide();
    }
    else {
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();
        $("#five").hide();
        $("#six").hide();
        //$("#minus").hide();
        //$("#plus").hide();
        $("#add").hide();


    }


    var fixd = "/GetActiveAirlineProviders"
    if (o == true) {
        fixd = "/GetActiveAirlineProvidersF"
    }

    $.ajax({
        url: UrlBase + 'FltSearch1.asmx' + fixd,
        type: "POST",
        data: JSON.stringify({
            org: c.HidTxtDepCity.split(',')[0], dest: c.HidTxtArrCity.split(',')[0], airline: airlineCode, rtfStatus: rtfStarus1, trip: c.Trip1, DepDate: c.DepDate, RetDate: c.RetDate
        }),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (e, t, n) {
            // alert("Sorry, we could not find a match for the destination you have entered..Kindly modify your search.");
            // window.location = UrlBase + 'IBEHome.aspx';
            return false
        },
        success: function (data) {

            var providers = data.d;


            $.ajax({
                url: UrlBase + 'FltSearch1.asmx/GetFlightChacheDataListNoth',
                type: "POST",
                data: JSON.stringify({
                    obj: c
                }),
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (e, t, n) {
                    // alert("Sorry, we could not find a match for the destination you have entered..Kindly modify your search.");
                    // window.location = UrlBase + 'IBEHome.aspx';
                    return false
                },
                success: function (data) {

                    var cacheData = data.d;

                    var cacheAirline = "";

                    if (cacheData != null) {
                        for (var i = 0; i < cacheData.length; i++) {
                            cacheAirline += ',' + cacheData[i].Airline;
                        }
                    }
                    cacheAirline = cacheAirline + ','
                    t.ParallelCalling(c, IsNRMLRoundTrip, n, providers, cacheData, cacheAirline, airlineCode, rtfStarus1);
                }

            });






        }
    });


};


ResHelper.prototype.ParallelCalling = function (c, IsNRMLRoundTrip, trip, providers, cacheData, cacheAirline, hidTextAirCode, rtfStatus) {
    var t = this;
    var mc = $("input[name='TripType']:checked").val()
    async.parallel({
        One: function (callback) {
            if ((trip == "I" || trip == "D") && mc != "rdbMultiCity") {

                if ($.trim(cacheAirline).search("G8,") == -1) {
                    //var prov = $.trim($.trim(providers).split('G8')[1].split('-')[0]);

                    t.postData(t, c.HidTxtAirLine + ":LCC", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false, true, $.trim(providers), '');
                    //t.Displayflter("G8", c, IsNRMLRoundTrip);
                }
                else {

                    t.DisplayCacheData("G8", IsNRMLRoundTrip, cacheData)

                }
            }
            else {
                t.Displayflter("G8", c, IsNRMLRoundTrip);
            }

        },
        Two: function (callback) {
            if (IsNRMLRoundTrip == true && mc != "rdbMultiCity") {
                if (true) {
                    //var prov = $.trim($.trim(providers).split('G8')[1].split('-')[0]);
                    t.postData(t, c.HidTxtAirLine + ":LCC", c, false, UrlBase + "AirHandler.ashx", false, true, true, $.trim(providers), '');
                    //t.Displayflter("G8", c, IsNRMLRoundTrip);
                }
                else {
                    t.DisplayCacheData("G8", IsNRMLRoundTrip, cacheData)
                }
            }
            else {

                t.Displayflter("G8", c, IsNRMLRoundTrip);
            }
        },

        Three: function (callback) {
            if (trip == "I" || mc == "rdbMultiCity" || trip == "D") {
                var prov = $.trim(providers).split(':')[1].split('-')[0].replace('-', '');

                t.postData(t, c.HidTxtAirLine + ':' + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false, false, $.trim(providers), '');
                // t.Displayflter("1G", c, IsNRMLRoundTrip);
            }
            else {
                t.Displayflter("1G", c, IsNRMLRoundTrip);
            }
        },
        Four: function (callback) {
            if ($.trim(providers).search("CPN") >= 0 && mc != "rdbMultiCity" && trip == "D") {
                if ($.trim(cacheAirline).search("CPN,") == -1 || trip == "I") {

                    var prov = ":CPN";

                    t.postData(t, c.HidTxtAirLine + ":SG", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false, true, $.trim(providers), '');
                    //t.Displayflter("CPN", c, IsNRMLRoundTrip);
                }
                else {
                    t.DisplayCacheData("CPN", IsNRMLRoundTrip, cacheData)
                }
            } else {
                t.Displayflter("CPN", c, IsNRMLRoundTrip);
            }
        },
        Five: function (callback) {
            if (IsNRMLRoundTrip == true && mc != "rdbMultiCity" && trip != "I") {

                var prov = ":1G"//$.trim($.trim(providers).split('SG')[1].split('-')[0]);
                t.postData(t, c.HidTxtAirLine + prov, c, false, UrlBase + "AirHandler.ashx", false, true, false, $.trim(providers), '');
                //t.Displayflter("SG", c, IsNRMLRoundTrip);

            }
            else {

                t.Displayflter("1G", c, IsNRMLRoundTrip);
            }
        },

        Six: function (callback) {
            if ((trip == "I" || trip == "D") && mc != "rdbMultiCity") {

                if ($.trim(cacheAirline).search("G8,") == -1) {
                    //var prov = $.trim($.trim(providers).split('G8')[1].split('-')[0]);

                    t.postData(t, c.HidTxtAirLine + ":LCC", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false, true, $.trim(providers), '6E');
                    // t.Displayflter("G8", c, IsNRMLRoundTrip);
                }
                else {

                    t.DisplayCacheData("G8", IsNRMLRoundTrip, cacheData)

                }
            }
            else {
                t.Displayflter("G8", c, IsNRMLRoundTrip);
            }

        },
        Seven: function (callback) {
            if (IsNRMLRoundTrip == true && mc != "rdbMultiCity") {
                if (true) {
                    //var prov = $.trim($.trim(providers).split('G8')[1].split('-')[0]);
                    t.postData(t, c.HidTxtAirLine + ":LCC", c, false, UrlBase + "AirHandler.ashx", false, true, true, $.trim(providers), '6E');
                    //t.Displayflter("G8", c, IsNRMLRoundTrip);
                }
                else {
                    t.DisplayCacheData("G8", IsNRMLRoundTrip, cacheData)
                }
            }
            else {

                t.Displayflter("G8", c, IsNRMLRoundTrip);
            }
        },
        Eight: function (callback) {
            if ($.trim(providers).search("CPN") >= 0 && mc != "rdbMultiCity" && trip == "D") {
                if ($.trim(cacheAirline).search("CPN,") == -1 || trip == "I") {

                    var prov = ":CPN";

                    t.postData(t, c.HidTxtAirLine + ":SG", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false, true, $.trim(providers), '6E');
                    // t.Displayflter("CPN", c, IsNRMLRoundTrip);
                }
                else {
                    t.DisplayCacheData("CPN", IsNRMLRoundTrip, cacheData)
                }
            } else {
                t.Displayflter("CPN", c, IsNRMLRoundTrip);
            }
        },


        //Six: function (callback) {
        //    if ($.trim(providers).search("6E") >= 0 && mc != "rdbMultiCity") {
        //        if ($.trim(cacheAirline).search("6E,") == -1 || trip == "I") {
        //            var prov = $.trim($.trim(providers).split('6E')[1].split('-')[0]);
        //            // t.postData(t, "6E" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false);
        //            t.Displayflter("6E", c, IsNRMLRoundTrip);
        //        }
        //        else {
        //            t.DisplayCacheData("6E", IsNRMLRoundTrip, cacheData)
        //        }
        //    }
        //    else {

        //        t.Displayflter("6E", c, IsNRMLRoundTrip);
        //    }
        //},

        //Seven: function (callback) {
        //    if ($.trim(providers).search("6E") >= 0 && IsNRMLRoundTrip == true && mc != "rdbMultiCity") {
        //        if ($.trim(cacheAirline).search("6E,") == -1 || trip == "I") {
        //            var prov = $.trim($.trim(providers).split('6E')[1].split('-')[0]);
        //            //t.postData(t, "6E" + prov, c, false, UrlBase + "AirHandler.ashx", false, true);
        //            t.Displayflter("6E", c, IsNRMLRoundTrip);
        //        }
        //        else {
        //            t.DisplayCacheData("6E", IsNRMLRoundTrip, cacheData)
        //        }
        //    }
        //    else {

        //        t.Displayflter("6E", c, IsNRMLRoundTrip);
        //    }
        //},

        //Eight: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("AI") >= 0 && hidTextAirCode == "AI") || (trip == "D" && $.trim(providers).search("AI") >= 0 && hidTextAirCode == "")) {
        //            if ($.trim(cacheAirline).search("AI,") == -1 || trip == "I") {
        //                var prov = $.trim($.trim(providers).split('AI')[1].split('-')[0]);
        //                //t.postData(t, "AI" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false);
        //                t.Displayflter("AI", c, IsNRMLRoundTrip);
        //            }
        //            else {
        //                t.DisplayCacheData("AI", IsNRMLRoundTrip, cacheData)
        //            }
        //        }
        //        else {
        //            t.Displayflter("AI", c, IsNRMLRoundTrip);
        //        }
        //    }
        //    else {
        //        t.Displayflter("AI", c, IsNRMLRoundTrip);
        //    }
        //},
        //Nine: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("AI") >= 0 && hidTextAirCode == "AI" && IsNRMLRoundTrip == true) || (trip == "D" && $.trim(providers).search("AI") >= 0 && hidTextAirCode == "" && IsNRMLRoundTrip == true)) {
        //            if ($.trim(cacheAirline).search("AI,") == -1) {
        //                var prov = $.trim($.trim(providers).split('AI')[1].split('-')[0]);
        //                //t.postData(t, "AI" + prov, c, false, UrlBase + "AirHandler.ashx", false, true);

        //                t.Displayflter("AI", c, IsNRMLRoundTrip);
        //            }
        //            else {
        //                t.DisplayCacheData("AI", IsNRMLRoundTrip, cacheData)
        //            }
        //        }
        //        else {
        //            t.Displayflter("AI", c, IsNRMLRoundTrip);
        //        }
        //    } else {
        //        t.Displayflter("AI", c, IsNRMLRoundTrip);
        //    }
        //},


        //Ten: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("9W") >= 0 && hidTextAirCode == "9W") || (trip == "D" && $.trim(providers).search("9W") >= 0 && hidTextAirCode == "")) {
        //            if ($.trim(cacheAirline).search("9W,") == -1 || trip == "I") {
        //                var prov = $.trim($.trim(providers).split('9W')[1].split('-')[0]);
        //                // t.postData(t, "9W" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false);
        //                t.Displayflter("9W", c, IsNRMLRoundTrip);
        //            }
        //            else {

        //                t.DisplayCacheData("9W", IsNRMLRoundTrip, cacheData)
        //            }
        //        } else {
        //            t.Displayflter("9W", c, IsNRMLRoundTrip);
        //        }
        //    } else {
        //        t.Displayflter("9W", c, IsNRMLRoundTrip);
        //    }
        //},
        //Eleven: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("9W") >= 0 && hidTextAirCode == "9W" && IsNRMLRoundTrip == true) || (trip == "D" && $.trim(providers).search("9W") >= 0 && hidTextAirCode == "" && IsNRMLRoundTrip == true)) {
        //            if ($.trim(cacheAirline).search("9W,") == -1 || trip == "I") {
        //                var prov = $.trim($.trim(providers).split('9W')[1].split('-')[0]);
        //                // t.postData(t, "9W" + prov, c, false, UrlBase + "AirHandler.ashx", false, true);
        //                t.Displayflter("9W", c, IsNRMLRoundTrip);
        //            }
        //            else {

        //                t.DisplayCacheData("9W", IsNRMLRoundTrip, cacheData)
        //            }
        //        } else {
        //            t.Displayflter("9W", c, IsNRMLRoundTrip);
        //        }
        //    } else {
        //        t.Displayflter("9W", c, IsNRMLRoundTrip);
        //    }
        //},


        //Twelve: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("UK") >= 0 && hidTextAirCode == "UK") || (trip == "D" && $.trim(providers).search("UK") >= 0 && hidTextAirCode == "")) {
        //            if ($.trim(cacheAirline).search("UK,") == -1 || trip == "I") {
        //                var prov = $.trim($.trim(providers).split('UK')[1].split('-')[0]);
        //                // t.postData(t, "UK" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false);
        //                t.Displayflter("UK", c, IsNRMLRoundTrip);
        //            }
        //            else {
        //                t.DisplayCacheData("UK", IsNRMLRoundTrip, cacheData)
        //            }
        //        } else {
        //            t.Displayflter("UK", c, IsNRMLRoundTrip);
        //        }
        //    } else {
        //        t.Displayflter("UK", c, IsNRMLRoundTrip);
        //    }
        //},


        //Thirteen: function (callback) {
        //    if (mc != "rdbMultiCity") {
        //        if ((trip == "D" && $.trim(providers).search("UK") >= 0 && hidTextAirCode == "UK" && IsNRMLRoundTrip == true) || (trip == "D" && $.trim(providers).search("UK") >= 0 && hidTextAirCode == "" && IsNRMLRoundTrip == true)) {
        //            if ($.trim(cacheAirline).search("UK,") == -1 || trip == "I") {
        //                var prov = $.trim($.trim(providers).split('UK')[1].split('-')[0]);
        //                // t.postData(t, "UK" + prov, c, false, UrlBase + "AirHandler.ashx", false, true);
        //                t.Displayflter("UK", c, IsNRMLRoundTrip);
        //            }
        //            else {
        //                t.DisplayCacheData("UK", IsNRMLRoundTrip, cacheData)
        //            }
        //        } else {
        //            t.Displayflter("UK", c, IsNRMLRoundTrip);
        //        }
        //    } else {
        //        t.Displayflter("UK", c, IsNRMLRoundTrip);
        //    }
        //}
        //,
        //Fourteen: function (callback) {

        //    if ($.trim(providers).search("OF") >= 0 && mc != "rdbMultiCity" && IsNRMLRoundTrip != true) {

        //        if ($.trim(cacheAirline).search("OF,") == -1) {
        //            var prov = $.trim($.trim(providers).split('OF')[1].split('-')[0]);

        //            // t.postData(t, "OF" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false);
        //            t.Displayflter("OF", c, IsNRMLRoundTrip);
        //        }
        //        else {

        //            t.DisplayCacheData("OF", IsNRMLRoundTrip, cacheData)

        //        }
        //    }
        //    else {
        //        t.Displayflter("OF", c, IsNRMLRoundTrip);
        //    }



        //},
        //Fifteen: function (callback) {

        //    if ($.trim(providers).search("OF") >= 0 && trip == "D" && mc != "rdbMultiCity") {

        //        if ($.trim(cacheAirline).search("OF,") == -1) {
        //            var prov = $.trim($.trim(providers).split('OF')[1].split('-')[0]);

        //            // t.postData(t, "OF" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, true);
        //            t.Displayflter("OF", c, IsNRMLRoundTrip);
        //        }
        //        else {

        //            t.DisplayCacheData("OF", IsNRMLRoundTrip, cacheData)

        //        }
        //    }
        //    else {
        //        t.Displayflter("OF", c, IsNRMLRoundTrip);
        //    }

        //},

        //Sixteen: function (callback) {
        //    if ($.trim(providers).search("GSCPN") >= 0 && mc != "rdbMultiCity" && trip == "D") {
        //        if ($.trim(cacheAirline).search("SGCPN,") == -1 || trip == "I") {

        //            var prov = ":CPN";

        //           // t.postData(t, "SG:SG", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false);
        //             t.Displayflter("SGCPN", c, IsNRMLRoundTrip);
        //        }
        //        else {
        //            t.DisplayCacheData("SGCPN", IsNRMLRoundTrip, cacheData)
        //        }
        //    } else {
        //        t.Displayflter("SGCPN", c, IsNRMLRoundTrip);
        //    }
        //    // rStatus = rStatus + 1;
        //},
        //Seventeen: function (callback) {
        //    if ($.trim(providers).search("E6CPN") >= 0 && mc != "rdbMultiCity" && trip == "D") {
        //        if ($.trim(cacheAirline).search("6ECPN,") == -1 || trip == "I") {
        //            var prov = ":CPN";
        //            // t.postData(t, "6E" + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true);
        //            //t.postData(t, "6E:6E", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false);
        //            t.Displayflter("6ECPN", c, IsNRMLRoundTrip);
        //        }
        //        else {
        //            t.DisplayCacheData("6ECPN", IsNRMLRoundTrip, cacheData)
        //        }
        //    } else {
        //        t.Displayflter("6ECPN", c, IsNRMLRoundTrip);
        //    }
        //},
        //Eighteen: function (callback) {
        //    if ($.trim(providers).search("8GCPN") >= 0 && mc != "rdbMultiCity" && trip == "D") {
        //        if ($.trim(cacheAirline).search("G8CPN,") == -1 || trip == "I") {
        //            var prov = ":CPN";
        //            // t.postData(t, "G8"+prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true);

        //           // t.postData(t, "G8:G8", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false);

        //             t.Displayflter("G8CPN", c, IsNRMLRoundTrip);
        //        }
        //        else {
        //            t.DisplayCacheData("G8CPN", IsNRMLRoundTrip, cacheData)
        //        }
        //    } else {
        //        t.Displayflter("G8CPN", c, IsNRMLRoundTrip);
        //    }

        //}



    },
        function (err, results) {


            // results is now equals to: {one: 1, two: 2} AirArabiaSearchListPartialO
        });


}

ResHelper.prototype.Displayflter = function (Airline, c, IsNRMLRoundTrip) {
    var t = this;
    rStatus = rStatus + 1;
    t.ProgressBar(rStatus);
    if (rStatus == 8) {
        t.ApplyFilters(t, Airline, IsNRMLRoundTrip);
    }

}

ResHelper.prototype.DisplayCacheData = function (Airline, IsNRMLRoundTrip, cacheResult) {
    var t = this;
    rStatus = rStatus + 1;
    t.ProgressBar(rStatus);
    var OB = JSLINQ(cacheResult)
        .Where(function (item) { return $.trim(item.Airline).toLowerCase() == $.trim(Airline).toLowerCase(); })
        .Select(function (item) { return item });

    var resultG1 = $.parseJSON(OB.items[0].Json);
    var result = "";

    if (IsNRMLRoundTrip == true) {
        var maxLineNumO = 0;
        var maxLineNumR = 0;
        if (resultG1[0].length > 0) {
            maxLineNumO = resultG1[0][resultG1[0].length - 1].LineNumber;
            for (var i = 0; i < resultG1[0].length; i++) {
                resultG1[0][i].LineNumber = resultG1[0][i].LineNumber + "api" + Airline + "ITZCACHE";
            }

            CommonResultArray[0] = $.merge(CommonResultArray[0], resultG1[0]);

        }

        if (resultG1.length > 1 && resultG1[1].length > 0) {
            maxLineNumR = resultG1[1][resultG1[1].length - 1].LineNumber;
            for (var j = 0; j < resultG1[1].length; j++) {
                resultG1[1][j].LineNumber = resultG1[1][j].LineNumber + "api1" + Airline + "ITZCACHE";
            }

            CommonResultArray[1] = $.merge(CommonResultArray[1], resultG1[1]);

        }



        t.GetResultR(resultG1, maxLineNumO, maxLineNumR, Airline + "ITZCACHE");
        t.OnewayH.hide();
        t.RoundTripH.show();
        if (rStatus == 8) {
            t.ApplyFilters(t, Airline, IsNRMLRoundTrip);
        }



    }
    else {



        var maxLineNum = 0;
        if (resultG1[0][0].length > 0) {
            maxLineNum = resultG1[0][0][resultG1[0][0].length - 1].LineNumber;
            for (var i = 0; i < resultG1[0][0].length; i++) {
                resultG1[0][0][i].LineNumber = resultG1[0][0][i].LineNumber + "api" + Airline + "ITZCACHE";
            }

            CommonResultArray[0] = $.merge(CommonResultArray[0], resultG1[0][0]);
            $(document).ajaxStop($.unblockUI);
        }


        t.hdnOnewayOrRound.val("OneWay");
        t.flterR.hide();
        t.OnewayH.show();
        t.divFromResult.prepend(t.GetResultO(resultG1[0], maxLineNum, Airline + "ITZCACHE"));
        t.DivResult.show();
        if (rStatus == 8) {
            t.ApplyFilters(t, Airline, IsNRMLRoundTrip);
        }

    }

    //t.MainSF.show();
    // $.unblockUI();

};

ResHelper.prototype.ApplyFilters = function (t, provider, IsNRMLRoundTrip) {


    if (applyfilterStatus == false) {
        if (roundtripNrml1 == true) {

            if (CommonResultArray[0].length > 0 && CommonResultArray[0].length > 0) {

                t.OnewayH.hide();
                t.RoundTripH.show();
                //t.DivMatrixRtfO.html(t.GetMatrix(CommonResultArray[0], 'O'));
                //t.DivMatrixRtfR.html(t.GetMatrix(CommonResultArray[1], 'R'));


                t.FltrSortR(CommonResultArray);
                t.flterR.hide();
                t.flterTab.show();
               t.GetSelectedRoundtripFlight(CommonResultArray);
                t.RTFFinalBook();
                t.ShowFareBreakUp(CommonResultArray);
                t.hdnOnewayOrRound.val("RoundTrip");
            }
            else {

                alert("Sorry, we could not find a match for the destination you have entered.Kindly modify your search.");
                $(document).ajaxStop($.unblockUI)
                window.location = UrlBase + 'Search.aspx';
            }

        }
        else {

            if (CommonResultArray[0].length > 0) {

                t.hdnOnewayOrRound.val("OneWay");
                t.flterR.hide();
                t.Book(CommonResultArray);
                t.GetFltDetails(CommonResultArray);
                t.ShowFareBreakUp(CommonResultArray);
                // t.DivMatrix.html(t.GetMatrix(CommonResultArray[0], 'O'));
                t.FltrSort(CommonResultArray);
                t.GetSelectedOneWayFlight(CommonResultArray);
            }
            else {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly modify your search.");
                $(document).ajaxStop($.unblockUI)
                window.location = UrlBase + 'Search.aspx';
            }

        }
        t.GetFltDetailsR(CommonResultArray);
        t.MainSF.show();
        t.fltrDiv.show();
        t.DivColExpnd.show();
        t.DivLoadP.hide();
        FlightHandler = new FlightResult();
        FlightHandler.BindEvents();
        $(document).ajaxStop($.unblockUI)

        applyfilterStatus = true;
    }

};

function lzw_decode(s) {
    var dict = {};
    var data = (s + "").split("");
    var currChar = data[0];
    var oldPhrase = currChar;
    var out = [currChar];
    var code = 256;
    var phrase;
    for (var i = 1; i < data.length; i++) {
        var currCode = data[i].charCodeAt(0);
        if (currCode < 256) {
            phrase = data[i];
        }
        else {
            phrase = dict[currCode] ? dict[currCode] : (oldPhrase + currChar);
        }
        out.push(phrase);
        currChar = phrase.charAt(0);
        dict[code] = oldPhrase + currChar;
        code++;
        oldPhrase = phrase;
    }
    return out.join("");
}


function str2ab(str) {
    var buf = new ArrayBuffer(str.length * 2); // 2 bytes for each char
    var bufView = new Uint16Array(buf);
    for (var i = 0, strLen = str.length; i < strLen; i++) {
        bufView[i] = str.charCodeAt(i);
    }
    return buf;
}
ResHelper.prototype.postData = function (t, provider, c, IsNRMLRoundTrip, url1, isCoupon, isRTF, isLCC, providerLIst, Is6E) {


    //var q = UrlBase + "FLTSearch1.asmx/Search_Flight";
    //    if (c.Trip1 == "I" && provider == "1G") {
    //    }
    //    else {
    //        c.AirLine = provider;
    //        c.HidTxtAirLine = provider;
    //        var prov = provider;
    //        if (provider == "AI" || provider == "9W" || provider == "UK") {
    //            prov = "1G";
    //        }
    //    }


    var prov = provider.split(':')[1];
    var oairline = "";
    if (provider == "OF") {
        prov = "OF";
        oairline = c.HidTxtAirLine;
    }
    else {
        oairline = provider.split(':')[0];


        //if (provider == "AI" || provider == "9W" || provider == "UK") {
        //    prov = "1G";
        //}
    }



    //var qstr = "?Trip1=" + c.Trip1 + "&TripType1=" + c.TripType1 + "&DepartureCity=" + c.DepartureCity + "&ArrivalCity=" + c.ArrivalCity + "&HidTxtDepCity=" + c.HidTxtDepCity + "&HidTxtArrCity=" + c.HidTxtArrCity + "&Adult=" + c.Adult + "&Child=" + c.Child + "&Infant=" + c.Infant + "&Cabin=" + c.Cabin + "&AirLine=" + c.AirLine + "&HidTxtAirLine=" + c.HidTxtAirLine + "&DepDate=" + c.DepDate + "&RetDate=" + c.RetDate + "&RTF=" + c.RTF + "&NStop=" + c.NStop + "&GDSRTF=" + c.GDSRTF + "&Provider=" + prov + "&isCoupon=" + isCoupon;
    // var qstr = "?Trip1=" + c.Trip1 + "&TripType1=" + c.TripType1 + "&DepartureCity=" + c.DepartureCity + "&ArrivalCity=" + c.ArrivalCity + "&HidTxtDepCity=" + c.HidTxtDepCity + "&HidTxtArrCity=" + c.HidTxtArrCity + "&Adult=" + c.Adult + "&Child=" + c.Child + "&Infant=" + c.Infant + "&Cabin=" + c.Cabin + "&AirLine=" + oairline + "&HidTxtAirLine=" + oairline + "&DepDate=" + c.DepDate + "&RetDate=" + c.RetDate + "&RTF=" + isRTF + "&NStop=" + c.NStop + "&GDSRTF=" + isRTF + "&Provider=" + prov + "&isCoupon=" + isCoupon;
    var qstr = "?Trip1=" + c.Trip1 + "&TripType1=" + c.TripType1 + "&DepartureCity=" + c.DepartureCity + "&ArrivalCity=" + c.ArrivalCity + "&HidTxtDepCity=" + c.HidTxtDepCity + "&HidTxtArrCity=" + c.HidTxtArrCity + "&Adult=" + c.Adult + "&Child=" + c.Child + "&Infant=" + c.Infant + "&Cabin=" + c.Cabin + "&AirLine=" + oairline + "&HidTxtAirLine=" + oairline + "&DepDate=" + c.DepDate + "&RetDate=" + c.RetDate + "&RTF=" + isRTF + "&NStop=" + c.NStop + "&GDSRTF=" + isRTF + "&Provider=" + prov + "&isCoupon=" + isCoupon + "&DepartureCity2=" + c.DepartureCity2 + "&ArrivalCity2=" + c.ArrivalCity2 + "&HidTxtDepCity2=" + c.HidTxtDepCity2 + "&HidTxtArrCity2=" + c.HidTxtArrCity2 + "&DepDate2=" + c.DepDate2 + "&DepartureCity3=" + c.DepartureCity3 + "&ArrivalCity3=" + c.ArrivalCity3 + "&HidTxtDepCity3=" + c.HidTxtDepCity3 + "&HidTxtArrCity3=" + c.HidTxtArrCity3 + "&DepDate3=" + c.DepDate3 + "&DepartureCity4=" + c.DepartureCity4 + "&ArrivalCity4=" + c.ArrivalCity4 + "&HidTxtDepCity4=" + c.HidTxtDepCity4 + "&HidTxtArrCity4=" + c.HidTxtArrCity4 + "&DepDate4=" + c.DepDate4 + "&DepartureCity5=" + c.DepartureCity5 + "&ArrivalCity5=" + c.ArrivalCity5 + "&HidTxtDepCity5=" + c.HidTxtDepCity5 + "&HidTxtArrCity5=" + c.HidTxtArrCity5 + "&DepDate5=" + c.DepDate5 + "&DepartureCity6=" + c.DepartureCity6 + "&ArrivalCity6=" + c.ArrivalCity6 + "&HidTxtDepCity6=" + c.HidTxtDepCity6 + "&HidTxtArrCity6=" + c.HidTxtArrCity6 + "&DepDate6=" + c.DepDate6 + "&isLCC=" + isLCC + "&ListProvider=" + providerLIst + "&callApi=" + Is6E;
    $.ajax({
        url: url1 + qstr,
        // type: "POST",
        //data: JSON.stringify({
        //    obj:  c, Provider: provider
        //}),
        //data: JSON.stringify({
        //    milliseconds: 10
        //    }),
        //data: '{"obj": {"Trip1": "D", "TripType1": "rdbOneWay", "DepartureCity": "New Delhi(DEL)", "ArrivalCity": "Mumbai(BOM)", "HidTxtDepCity": "DEL,IN", "HidTxtArrCity": "BOM,IN", "Adult": "1", "Child": "0", "Infant": "0", "Cabin": "", "AirLine": "", "HidTxtAirLine": "", "DepDate": "16/01/2015", "RetDate": "16/01/2015", "RTF": false, "NStop": false, "GDSRTF": false}, "Provider": "G8"}',
        data: {},
        async: true,
        // crossDomain: true,
        contentType: "application/json; charset=utf-8",//"text/html charset=utf-8",//
        dataType: "json",
        //headers: { "Accept-Encoding": "gzip" },
        success: function (data1) {

            rStatus = rStatus + 1;
            t.ProgressBar(rStatus);
            //debugger;

            var b64Data = data1.data;//'H4sIAAAAAAAAAwXB2w0AEBAEwFbWl2Y0IW4jQmziPNo3k6TuGK0Tj/ESVRs6yzkuHRnGIqPB92qzhg8yp62UMAAAAA==';

            // Decode base64 (convert ascii to binary)
            var strData = atob(b64Data);

            // Convert binary string to character-number array
            var charData = strData.split('').map(function (x) { return x.charCodeAt(0); });

            // Turn number array into byte-array
            var binData = new Uint16Array(charData);

            // Pako magic
            var datanew = pako.inflate(binData);

            // Convert gunzipped byteArray back to ascii string:
            var strData = "";
            //try {

            for (var i = 0; i < datanew.length; i++) {
                strData = strData + String.fromCharCode(datanew[i]);
            }
            //var strData = String.fromCharCode(//.apply(null, new Uint16Array(datanew));
            //} catch (ex) { }



            //var decoded = lzw_decode(data.data);
            var data11 = new Array();
            data11 = JSON.parse(strData);

            ///////
            //  debugger;
            try {
                for (var l = 0; l < data11.length; l++) {

                    var data = JSON.parse(data11[l]);
                    var resultG1 = data.result;//data.result;
                    var result = "";

                    if (resultG1.length > 1 && resultG1[1].length > 0 && resultG1[0].length > 0 && IsNRMLRoundTrip == true) {
                        //$.unblockUI();
                    }
                    else if (resultG1.length > 0 && resultG1[0].length > 0) {
                        // $.unblockUI();
                    }


                    if (IsNRMLRoundTrip == true) {
                        if (resultG1.length > 1) {
                            var maxLineNumO = 0;
                            var maxLineNumR = 0;
                            if (resultG1[0].length > 0) {
                                maxLineNumO = resultG1[0][resultG1[0].length - 1].LineNumber;
                                for (var i = 0; i < resultG1[0].length; i++) {
                                    resultG1[0][i].LineNumber = resultG1[0][i].LineNumber + "api" + data.Provider + "ITZNRML";
                                }

                                CommonResultArray[0] = $.merge(CommonResultArray[0], resultG1[0]);

                            }

                            if (resultG1.length > 1 && resultG1[1].length > 0) {
                                maxLineNumR = resultG1[1][resultG1[1].length - 1].LineNumber;
                                for (var j = 0; j < resultG1[1].length; j++) {
                                    resultG1[1][j].LineNumber = resultG1[1][j].LineNumber + "api1" + data.Provider + "ITZNRML";
                                }

                                CommonResultArray[1] = $.merge(CommonResultArray[1], resultG1[1]);

                            }
                            //if (resultG1.length > 1 && resultG1[1].length > 0 && resultG1[0].length > 0) {
                            //    $.unblockUI();
                            //}
                            t.GetResultR(resultG1, maxLineNumO, maxLineNumR, data.Provider + "ITZNRML");
                            t.OnewayH.hide();
                            t.RoundTripH.show();
                        }
                        if (c.NStop == true) {
                            if (rStatus == 8 || rStatus == 6 && l == (parseInt(data11.length) - 1)) {
                                rStatus = 8;
                                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
                            }

                        }
                        else {
                            if (rStatus == 8 && l == (parseInt(data11.length) - 1)) {
                                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
                            }

                        }

                    }
                    else {

                        var maxLineNum = 0;
                        if (resultG1.length > 0 && resultG1[0].length > 0) {
                            //$.unblockUI();
                            maxLineNum = resultG1[0][resultG1[0].length - 1].LineNumber;

                            if ($.trim(resultG1[0][0].Sector).length > 8 && c.TripType1 != "rdbMultiCity" && $.trim(resultG1[0][0].Trip).toUpperCase() == "D") {


                                var mrslt = resultG1[0];

                                for (var i = 0; i < mrslt.length; i++) {
                                    mrslt[i].LineNumber = mrslt[i].LineNumber + "api" + data.Provider + "ITZNRML95SFM";
                                }
                                CommanRTFArray = $.merge(CommanRTFArray, mrslt);

                                var ODKey = JSLINQ(mrslt)
                                    .Where(function (item) { return item.Flight == "1"; })
                                    .Distinct(function (item) { return item.SubKey })
                                    .Select(function (item) { return item }).ToArray();
                                //var O = OO.items.slice(true);
                                var O = new Array();

                                for (var i = 0; i < ODKey.length; i++) {
                                    var ODL = JSLINQ(mrslt)
                                        .Where(function (item) { return item.Flight == "1" & item.SubKey == ODKey[i]; })
                                        .Distinct(function (item) { return item.LineNumber })

                                    var ODM = JSLINQ(mrslt)
                                        .Where(function (item) { return item.Flight == "1" & item.LineNumber == ODL.items[0]; })

                                    for (var k = 0; k < ODM.items.length; k++) {
                                        O.push(ODM.items[k]);

                                    }


                                }

                                var NewO = new Array();
                                for (var j = 0; j < O.length; j++) {
                                    var yy = jQuery.extend(true, [], O[j]);
                                    yy.LineNumber = yy.LineNumber + "1";
                                    yy.TotalFare = Math.ceil(parseFloat(yy.TotalFare) / 2);
                                    NewO.push(yy);
                                }

                                CommonResultArray[0] = $.merge(CommonResultArray[0], NewO);



                                var RDKey = JSLINQ(mrslt)
                                    .Where(function (item) { return item.Flight == "2"; })
                                    .Distinct(function (item) { return item.SubKey })
                                    .Select(function (item) { return item }).ToArray();


                                var R = new Array();

                                for (var i = 0; i < RDKey.length; i++) {
                                    var RDL = JSLINQ(mrslt)
                                        .Where(function (item) { return item.Flight == "2" & item.SubKey == RDKey[i]; })
                                        .Distinct(function (item) { return item.LineNumber })

                                    var RDM = JSLINQ(mrslt)
                                        .Where(function (item) { return item.Flight == "2" & item.LineNumber == RDL.items[0]; })

                                    for (var k = 0; k < RDM.items.length; k++) {
                                        R.push(RDM.items[k]);

                                    }


                                }

                                var NewR = new Array();
                                for (var i = 0; i < R.length; i++) {

                                    var yy = jQuery.extend(true, [], R[i]);
                                    yy.LineNumber = yy.LineNumber.replace("api", "api1") + "2";

                                    yy.TotalFare = Math.ceil(parseFloat(yy.TotalFare) / 2);

                                    NewR.push(yy);
                                }


                                var joinArray = new Array();
                                joinArray.push(NewO);

                                CommonResultArray[1] = $.merge(CommonResultArray[1], NewR);

                                joinArray.push(NewR);

                                //t.GetResultR(joinArray, maxLineNum, maxLineNum, data.Provider + "ITZNRML95SFM");
                                t.OnewayH.hide();
                                t.RoundTripH.show();
                                //Add By Devesh 09-05-2018
                                //$('#RTFSAirMain').show();
                                var fltname;
                                try {
                                    var airItemRTF = { "fltName": resultG1[0][0].AirLineName, "fltJson": resultG1[0] };
                                    //gdsJsonRTF.push(airItemRTF);                                    
                                    //GetUniqueAirlineRtf($.trim(resultG1[0][0].AirLineName), 0, $.trim(resultG1[0][0].AirLineName).replace(/\s/g, '').split('_')[0])
                                }
                                catch (error) {
                                }
                                //End Add By Devesh 09-05-2018	

                                // (RTFFirstTime == 2) 
                                //{
                                //$('#RTFSAirMain').show();
                                //('#RTFSAir').show();    
                                //$('#splLoading').hide();
                                //$('#RTFSAir').html('<div id="DivNormalfare" onclick="RTFResultShowHide(\'DivNormalfare\');" class="curve rtfpanel1 bld lft w30">Normal Fare</div>&nbsp;&nbsp<div style="margin-left:10px;" id="DivTabRTF" onclick="RTFResultShowHide(\'DivTabRTF\');" class="curve lft rtfpanel bld w30">SRF Fare</div>');
                                //RTFFirstTime = 1;
                                //} 

                            }
                            else {
                                for (var i = 0; i < resultG1[0].length; i++) {
                                    resultG1[0][i].LineNumber = resultG1[0][i].LineNumber + "api" + data.Provider + "ITZNRML";
                                }
                                CommonResultArray[0] = $.merge(CommonResultArray[0], resultG1[0]);


                                t.hdnOnewayOrRound.val("OneWay");
                                t.flterR.hide();
                                t.OnewayH.show();
                                t.divFromResult.prepend(t.GetResultO(resultG1, maxLineNum, data.Provider + "ITZNRML"));
                                t.DivResult.show();


                            }



                        }

                        if (c.NStop == true) {
                            if (rStatus == 8 || rStatus == 6 && l == (parseInt(data11.length) - 1)) {
                                rStatus = 8;
                                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
                            }

                        }
                        else {
                            if (rStatus == 8 && l == (parseInt(data11.length) - 1)) {
                                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
                            }

                        }


                    }

                }
            }
            catch (ex) {
                $.unblockUI();
                //dddd
            }

            if (rStatus == 8 && parseInt(data11.length) == 0) {
                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
            }
            t.MainSF.show();


        },
        error: function (e, p, n) {

            rStatus = rStatus + 1;
            t.ProgressBar(rStatus);
            if (rStatus == 8) {
                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
            }
        }
    });
}

ResHelper.prototype.ProgressBar = function (stage) {
    var val = 10;
    //var remainder = stage % 3;
    //if (remainder == 0) {
    val = (stage * 14);
    $("#DivLoadP").progressbar({
        value: val
    });

    // }



};

ResHelper.prototype.GetResultSplRTFTrip = function (e, type) {
    // var Obook = null;
    // var Rbook = null;

    var t = e;
    t.MainSF.hide();

    var n;
    var r = $("input[name='TripType']:checked").val();
    var i = t.hidtxtDepCity1.val().split(",");
    var s = t.hidtxtArrCity1.val().split(",");
    if (i[1] == "IN" && s[1] == "IN") {
        n = "D"
    } else {
        n = "I"
    }
    var o = new Boolean; $(this)
    if (t.chkNonstop.is(":checked") == true) {
        o = true
    } else {
        o = false
    }
    var u = new Boolean;

    if (type == "lcc") {
        u = true;
        a = false;
    }
    else if (type == "gds") {
        u = false;
        a = true;
    }

    var QSHandler = new QSHelper();
    var qarray = QSHandler.queryStr();

    var f;
    var l;
    var c = {
        Trip1: n,
        TripType1: r,
        DepartureCity: t.txtDepCity1.val(),
        ArrivalCity: t.txtArrCity1.val(),
        HidTxtDepCity: t.hidtxtDepCity1.val(),
        HidTxtArrCity: t.hidtxtArrCity1.val(),
        Adult: t.Adult.val(),
        Child: t.Child.val(),
        Infant: t.Infant.val(),
        Cabin: t.Cabin.val(),
        AirLine: t.txtAirline.val(),
        HidTxtAirLine: t.hidtxtAirline.val(),
        DepDate: qarray.txtDepDate,
        RetDate: qarray.txtRetDate,
        RTF: u,
        NStop: o,
        GDSRTF: a
    };


    if (r == "rdbOneWay") {
        t.prexnt.show();
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        t.searchquery.html(f);
        // t.SearchTextDiv.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        // t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        /// f = "<b>" + f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant </b>";
        // t.CS.html(f)
    } else if (r == "rdbRoundTrip") {

        t.prexnt.hide();
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        l = c.ArrivalCity + " To " + c.DepartureCity + " On  " + c.RetDate;
        var h = f + "<br/>" + l;
        t.searchquery.html(h);
        f = f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        l = l + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        // t.SearchTextDiv.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());
        t.SearchTextDiv1.html("One Way from " + t.txtDepCity1.val() + " to " + t.txtArrCity1.val() + " | return " + t.txtArrCity1.val() + " to " + t.txtDepCity1.val());

        t.RTFTextFrom.html(t.hidtxtDepCity1.val().split(',')[0] + " - " + t.hidtxtArrCity1.val().split(',')[0] + " " + t.txtDepDate.val());
        t.RTFTextTo.html(t.hidtxtArrCity1.val().split(',')[0] + " - " + t.hidtxtDepCity1.val().split(',')[0] + " " + t.txtRetDate.val());
        t.DivRefinetitle.html(t.txtDepCity1.val() + " to " + t.txtArrCity1.val());
        //t.flterTabO.html(t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0]);
        //  t.flterTabR.html(t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0]);
        t.flterTabO.hide();
        t.flterTabR.hide();
        // h = "<b>" + h + "</b>";
        //t.CS.html(h)
    }

    $.blockUI({
        message: $("#waitMessage")
    });

    var q = UrlBase + "FLTSearch1.asmx/Search_Flight";

    $.ajax({
        url: q,
        type: "POST",
        data: JSON.stringify({
            obj: c
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (e, t, n) {

            alert("Sorry, we could not find a match for the destination you have entered..Kindly mordify your search.");
            // window.location = UrlBase + 'IBEHome.aspx';

            return false
        },
        success: function (data) {
            var resultRTFSp = data.d;
            var result = "";
            if (resultRTFSp == "" || resultRTFSp == null) {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                $(document).ajaxStop($.unblockUI)
                // window.location = UrlBase + 'IBEHome.aspx';

            } else {
                if (resultRTFSp[0] == "" || resultRTFSp[0] == null) {
                    alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                    $(document).ajaxStop($.unblockUI)
                    // window.location = UrlBase + 'IBEHome.aspx';

                } else {

                    t.hdnOnewayOrRound.val("OneWay");
                    t.flterR.hide();

                    if (type == "lcc") {
                        lccRtfResultArray = resultRTFSp;
                        lccRtfResult = t.GetResultO(resultRTFSp);
                        t.DivResult.html(lccRtfResult);
                    }
                    else if (type == "gds") {
                        gdsRtfResultArray = resultRTFSp;
                        gdsRtfResult = t.GetResultO(resultRTFSp);
                        t.DivResult.html(gdsRtfResult);
                    }

                    t.Book(resultRTFSp);
                    t.GetFltDetails(resultRTFSp);
                    t.ShowFareBreakUp(resultRTFSp);
                    //t.DivMatrix.html(t.GetMatrix(resultRTFSp[0], 'O'));
                    t.FltrSort(resultRTFSp);

                    t.GetFltDetailsR(resultRTFSp);
                    t.MainSF.show();
                    FlightHandler = new FlightResult();
                    FlightHandler.BindEvents();
                    $(document).ajaxStop($.unblockUI)
                }
            }

        }
    });
};

ResHelper.prototype.GetResultR = function (resultArray, maxLineNumO, maxLineNumR, Provider) {
    var chunk = 40;
    var t = this;
    var result = '';
    var Provider1 = '';
    var Provider2 = '';
    if (resultArray[0].length > 0) {
        var i = 1;
        var obsLNItems = JSLINQ(resultArray[0]).Select(function (item) {
            return item.SLineNumber
        });
        var LnOb = Math.max.apply(Math, obsLNItems.items);

        function doChunkO() {
            if ($.trim(Provider).search("SFM") > 0) {
                Provider1 = Provider + "1";
            } else {
                Provider1 = Provider;
            }
            var cnt = chunk;
            for (; i <= LnOb; i++) {
                cnt--;
                var obs = JSLINQ(resultArray[0]).Where(function (item) {
                    return item.SLineNumber == i;
                }).Select(function (item) {
                    return item
                });
                var NewItemArray = obs.items.sort(function (a, b) {
                    return (a.AdtFare * a.Adult + a.ChdFare * a.Child + a.InfFare * a.Infant) - (b.AdtFare * b.Adult + b.ChdFare * b.Child + b.InfFare * b.Infant)
                });
                //var Ln = JSLINQ(obs.items)
                var Ln = JSLINQ(NewItemArray).Select(function (item) {
                    return item.LineNumber
                });
                var OB = JSLINQ(obs.items).Where(function (item) {
                    return item.LineNumber == Ln.items[0] && item.Flight == "1";
                }).Select(function (item) {
                    return item
                });
                var k = 0;
                var O = "O";
                var unds = "_";
                if (OB.items.length > 0) {
                    if ($.trim(Provider).search("SFM") > 0) {
                        var SrfFlightClass = OB.items[0].AirLineName.replace(/\s/g, '') + "_" + OB.items[0].AirLineName.replace(/\s/g, '');
                        result += '<div class="list-item resR srtfResult ' + SrfFlightClass + '" style="display:none;">';
                    } else {
                        result += '<div class="list-item resR nrmResult">';
                    }
                    result += '<div id="main_' + i + "api" + Provider + '_O" class="fltbox mrgbtm bdrblue">';
                    for (var obi = 0; obi < 1; obi++) {
                        result += '<span>';
                        result += '<span id="ret">';
                        if (t.CheckMultipleCarrier(OB.items) == true) {
                            result += '<img alt="" class="air-img-r"   src="' + UrlBase + 'Airlogo/multiple.png"/>';
                            result += '</div>';
                            result += '<div>Multiple Carriers</div>';
                            result += '<div class="airlineImage2 hide">' + OB.items[obi].AirLineName + '</div>';
                        } else {
                            if ((OB.items[obi].MarketingCarrier == '6E') && ($.trim(OB.items[obi].sno).search("INDIGOCORP") >= 0)) {
                                result += '<img alt="" class="air-img-r"  src="../Airlogo/smITZ.gif"/>';
                                result += '</div>';
                                result += '<div class="gray">' + OB.items[obi].FlightIdentification + '</div>';
                                result += '<div class="airlineImage2">RWT Fare</div>';
                            } else {
                                result += '<img alt="" class="air-img-r"  src="../Airlogo/sm' + OB.items[obi].MarketingCarrier + '.gif"/>';
                                result += '</span>';
                                result += ' <span class="airlineInfo-sctn" ><span class="font12 inlineB append_bottom7 insertSep" style="font-size: 12px !important;">' + OB.items[obi].AirLineName + ' | <span class="font10 prepend_left5" style="font-size: 12px !important;">' + OB.items[obi].MarketingCarrier + '-' + OB.items[obi].FlightIdentification + '</span></span></span>';
                            }
                        }
                        result += '</span>';
                        result += '<div class="">';
                        result += '<div class="row">';
                        result += '<div class="col-md-4 col-xs-4" style="text-align: left;">';
                        result += '<div class="f16">' + t.MakeupAdTime(OB.items[obi].DepartureTime) + '</div> ';
                        result += '<div class="">' + OB.items[obi].Departure_Date + '</div> ';
                        result += '<div class="ter">' + OB.items[obi].DepartureTerminal + '</div> ';
                        result += '<div class="dur">' + totDur + '</div>';
                        result += '</div>';
                        result += '<div class="col-md-4 col-xs-4">';
                        if (k == 0) {
                            var totDur = "";
                            if (OB.items[obi].Provider == "TB") {
                                totDur = t.GetTotalDuration(OB.items);
                            } else {
                                totDur = t.MakeupTotDur(OB.items[obi].TotDur)
                            }
                            result += '<div class="theme-search-results-item-flight-section-path stp-dur">';
                            result += '<div class="theme-search-results-item-flight-section-path-fly-time"><p style="font-size:10px !important;">' + totDur + ' | ' + OB.items[obi].Stops + '</p></div>';
                            result += '<div class="row path">';
                            result += '<div class="theme-search-results-item-flight-section-path-line"></div>';
                            result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OB.items[obi].DepartureLocation + '</div></div>';
                            result += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OB.items[obi].ArrivalLocation + '</div></div>';
                            result += '</div>';
                            result += '</div>';
                            var freeMeal = "";
                            result += '<div class="airlineImage hide">' + OB.items[obi].AirLineName + '</div>';
                            result += '<div class="stops hide">' + OB.items[obi].Stops + '</div>';
                            result += '<div class="totdur hide">' + t.MakeupTotDur(OB.items[obi].TotDur).replace(':', '') + '</div>';
                            result += '<div class="dtime2 hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '')) + '</div>';
                            if ($.trim(Provider).search("SFM") > 0) {
                                result += '<div class="srf hide">SRF</div>';
                            } else {
                                result += '<div class="srf hide">NRMLF</div>'
                            }
                            result += '<div class="clear"></div>';
                            result += '</div>';
                            result += '<div class="col-md-4 col-xs-4" style="text-align: end;">';
                            result += '<div class="f16"> ' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime);
                            result += '<div class="arrtime hide">' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                            result += '</div>';
                            result += '<div class="">' + OB.items[obi].Arrival_Date + '</div> ';
                            result += '<div class="ter">' + OB.items[obi].ArrivalTerminal + '</div>';
                            result += '<div class="stp">' + OB.items[obi].Stops + '</div>';
                            result += '</div>';
                            result += '</div>';
                            result += '<div class="row">';
                            result += '<div class="large-12 medium-12 small-12 columns passenger">';
                            var rrfndO = 'n';
                            if (t.DisplayPromotionalFare(OB.items[obi]) == '') { } else {
                                result += '<div class="lft"><img src="' + UrlBase + 'images/icons/FamilyFriends.png" alt="F" title="Family and Friends" />&nbsp;</div>';
                            }
                            result += '<div class="gridViewToolTip1 hide" title="' + OB.items[obi].LineNumber + '_O" >ss</div>';
                        }
                        result += '</div>';
                        result += '</div>';
                        if (k == 0) {
                            result += '<div class="">';
                            if ($.trim(Provider).search("SFM") > 0 && (OB.items[obi].MarketingCarrier == "SG" || OB.items[obi].MarketingCarrier == "6E" || OB.items[obi].MarketingCarrier == "G8" || OB.items[obi].MarketingCarrier == "UK888") && OB.items[obi].Trip == "D") {
                                result += '<div class="f20 rgt colorp" style="display:none;"><i class="fa fa-inr" aria-hidden="true"></i>' + OB.items[obi].NetFareSRTF + '</div>';
                            } else {
                                result += '<div class="f20 rgt colorp" style="display:none;"><i class="fa fa-inr" aria-hidden="true"></i>' + OB.items[obi].TotalFare + '</div>';
                            }
                            for (var rf1 = 0; rf1 < OB.items.length; rf1++) {
                                result += '<div class="airstopO hide  gray">' + $.trim(OB.items[rf1].Stops).toString().toLowerCase() + '_' + $.trim(OB.items[rf1].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                            }
                            result += '<div class="lft">';
                            if (k == 0) {
                                var mulfare = DisplayMultipleFares(obs, Ln.items, 0, OB.items[obi].AirLineName, true, false, i, "O", OB.items[obi].LineNumber);
                                if (mulfare[0] != "" && mulfare[0] != ",") {
                                    result += mulfare[0];
                                }
                                if (mulfare[1] != "" && mulfare[1] != ",") {
                                    result += mulfare[1];
                                }
                                result += '<div class="deptime hide passenger">' + t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '') + '</div>';
                                result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '')) + '</div>';
                                result += '<div class="rfnd hide passenger">' + rrfndO + '</div> ';
                                result += '<div class="price hide passenger">' + OB.items[obi].TotalFare + '</div>';
                            }
                            result += '</div>';
                            result += '</div>';
                            result += '</div>';
                            result += '<div class="clear"></div>';
                            result += '<div class="shadborder"></div>';
                            result += '<div class="row darkbg" style="display:none;">';
                            result += '<div class="col-md-4">';
                            if (k == 0) {
                                var rnr = '<div class="text-center">Non-Refundable </div>';
                                if ($.trim(OB.items[obi].AdtFareType).toLowerCase() == "refundable") {
                                    rnr = '<div class="text-center"> Refundable   </div>';
                                    rrfndO = 'r';
                                }
                                if ($.trim(Provider).search("SFM") > 0) {
                                    rnr = '<div class="text-center">  Special Return </div>';
                                }
                                result += '<div class="lft passenger" style="white-space:nowrap;">' + rnr + '</div>';
                            }
                            result += '</div>';
                            result += '<div class="col-md-4">' + " Delhi" + '</div>';
                            result += '<div class="col-md-4">';
                            result += '<div id="ABC" style="top: 0px;" onclick="return toggleFD(\'FD_' + OB.items[obi].LineNumber + unds + OB.items[obi].AirLineName.replace(' ', '-') + '_O' + '\');"> <a href="javascript:void(0);" class="bld1">Flight Details <i class="fa fa-chevron-down"></i></a>   </div>';
                            result += '</div>';
                            result += '<div class="clear"></div>';
                            result += '</div>';
                            result += '<div class="">';
                            result += '<div class=""><div class="summary" id="FS_' + OB.items[obi].LineNumber + unds + OB.items[obi].AirLineName.replace(' ', '-') + '_O' + '"> </div>';
                            result += '</div>';
                            result += '<div class=""><div  class="summary" id="FDt_' + OB.items[obi].LineNumber + unds + OB.items[obi].AirLineName.replace(' ', '-') + '_O' + '"></div>';
                            result += '</div>';
                            result += '<div class=""><div  class="summary" id="BIn_' + OB.items[obi].LineNumber + unds + OB.items[obi].AirLineName.replace(' ', '-') + '_O' + '"></div>';
                            result += '</div>';
                        }
                        k++;
                        break;
                    }

                  
                    result += '</div>';
                    result += ' </div>';
                    result += ' </div>';
                }
                if ((i < LnOb && cnt == 0) || (i == LnOb)) {
                    result += ' </div>';
                    result += ' </div>';
                    var isfilter = false;
                    if (i == LnOb) {
                        isfilter = true;
                        i++;
                    }
                    setTimeout(doChunkO, 10);
                    break;
                }
            }
        }
        doChunkO();
    }
    t.divFromR.prepend(result);
    var result1 = '';
    if (resultArray.length > 1) {
        var a = 1;
        if (resultArray[1].length > 0) {
            if ($.trim(Provider).search("SFM") > 0) {
                Provider2 = Provider + "2";
            } else {
                Provider2 = Provider;
            }
            var LnIb = 0;
            var obsLNItems;
            obsLNItems = JSLINQ(resultArray[1]).Select(function (item) {
                return item.SLineNumber
            });
            LnIb = Math.max.apply(Math, obsLNItems.items);
            a = Math.min.apply(Math, obsLNItems.items);

            function doChunkR() {
                var cntR = chunk;
                for (; a <= LnIb; a++) {
                    cntR--;
                    var OR = JSLINQ(resultArray[1]).Where(function (item) {
                        return item.SLineNumber == a;
                    }).Select(function (item) {
                        return item
                    });
                    var NewItemArray = OR.items.sort(function (a, b) {
                        return (a.AdtFare * a.Adult + a.ChdFare * a.Child + a.InfFare * a.Infant) - (b.AdtFare * b.Adult + b.ChdFare * b.Child + b.InfFare * b.Infant)
                    });
                    //var Ln = JSLINQ(OR.items)
                    var Ln = JSLINQ(NewItemArray).Select(function (item) {
                        return item.LineNumber
                    });
                    var k = 0;
                    var R = "R";
                    var unds = "_";
                    if (OR.items.length > 0) {
                        if ($.trim(Provider).search("SFM") > 0) {
                            var SrfFlightClass = OR.items[0].AirLineName.replace(/\s/g, '') + "_" + OR.items[0].AirLineName.replace(/\s/g, '');
                            result1 += '<div class="list-itemR srtfResult ' + SrfFlightClass + '" style="display:none;">';
                        } else {
                            result1 += '<div class="list-itemR nrmResult">';
                        }
                        if (t.CheckMultipleCarrier(OR.items) == true) { } else {
                            if ((OR.items[0].MarketingCarrier == '6E') && ($.trim(OR.items[0].sno).search("INDIGOCORP") >= 0)) { } else { }
                        }
                        if ((OR.items[0].ValiDatingCarrier == 'SG') && (($.trim(OR.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OR.items[0].Searchvalue).search("AP14") >= 0))) {
                            result1 += '<div class="clear"></div><div class="bld colorp italic">Non Refundable</div>';
                        }
                        result1 += '<div id="main_' + i + "api1" + Provider + '_R" class="fltbox mrgbtm">';
                        for (var obr = 0; obr < 1; obr++) {
                            result1 += '<span>';
                            result1 += '<span>';
                            if (t.CheckMultipleCarrier(OR.items) == true) {
                                result1 += '<img alt="" class="air-img-r" src="' + UrlBase + 'Airlogo/multiple.png"/>';
                                result1 += '</div>';
                                result1 += '<div>Multiple Carriers</div>';
                            } else {
                                if ((OR.items[obr].MarketingCarrier == '6E') && ($.trim(OR.items[obr].sno).search("INDIGOCORP") >= 0)) {
                                    result1 += '<img alt="" class="air-img-r" src="../Airlogo/smITZ.gif"/>';
                                    result1 += '</div>';
                                    result1 += '<div class="gray">' + OR.items[obr].FlightIdentification + '</div>';
                                } else {
                                    result1 += '<img alt="" class="air-img-r" src="../Airlogo/sm' + OR.items[obr].MarketingCarrier + '.gif"/>';
                                    result1 += '</span>';
                                    result1 += ' <span class="airlineInfo-sctn" ><span class="font12 inlineB append_bottom7 insertSep" style="font-size: 12px !important;">' + OR.items[obr].AirLineName + ' | <span class="font10 prepend_left5" style="font-size: 12px !important;">' + OR.items[obr].MarketingCarrier + '-' + OR.items[obr].FlightIdentification + '</span></span></span>';
                                }
                            }
                            result1 += '</span>';
                            result1 += '<div class="row">';
                            result1 += '<div class="col-md-4 col-xs-4" style="text-align: left;">';
                            result1 += '<div class="f16">' + t.MakeupAdTime(OR.items[obr].DepartureTime) + '</div>';
                            result1 += '<div>' + OR.items[obr].Departure_Date + '</div>'
                            result1 += '<div class="ter">' + OR.items[obr].DepartureTerminal + '</div>'
                            result1 += '<div class="dur">' + totDur + '</div>';
                            result1 += '</div>';
                            result1 += '<div class="col-md-4 col-xs-4 ">';
                            if (k == 0) {
                                var totDur = "";
                                if (OR.items[obr].Provider == "TB") {
                                    totDur = t.GetTotalDuration(OR.items);
                                } else {
                                    totDur = t.MakeupTotDur(OR.items[obr].TotDur)
                                }
                                result1 += '<div class="theme-search-results-item-flight-section-path stp-dur">';
                                result1 += '<div class="theme-search-results-item-flight-section-path-fly-time"><p style="font-size:10px !important;">' + totDur + ' | ' + OR.items[obr].Stops + '</p></div>';
                                result1 += '<div class="row path">';
                                result1 += '<div class="theme-search-results-item-flight-section-path-line"></div>';
                                result1 += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OR.items[obr].DepartureLocation + '</div></div>';
                                result1 += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OR.items[obr].ArrivalLocation + '</div></div>';
                                result1 += '</div>';
                                result1 += '</div>';
                                var freeMeal = "";
                                result1 += '<div class="totdur hide">' + t.MakeupTotDur(OR.items[obr].TotDur).replace(':', '') + '</div>';
                                result1 += '<div class="atime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OR.items[obr].DepartureTime).replace(':', '')) + '</div>';
                                result1 += '<div class="airlineImage hide">' + OR.items[obr].AirLineName + '</div>';
                                result += '</div>';
                                result += '<div class="clear"></div>';
                                result1 += '</div>';
                                result1 += '<div class="col-md-4 col-xs-4" style="text-align: end;">';
                                result1 += '<div class="f16"> ' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime) + '</div>';
                                result1 += '<div class="arrtime hide">' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                                result1 += '<div >' + OR.items[obr].Arrival_Date + '</div>'
                                result1 += '<div class="ter">' + OR.items[obr].ArrivalTerminal + '</div>'
                                result1 += '<div class="stp">' + OR.items[obr].Stops + '</div>'
                                result1 += '</div>';
                                result += '<div class="large-12 medium-12 small-12 columns passenger">';
                                var rrfndR = 'n';
                                if (k == 0) {
                                    if ($.trim(OR.items[obr].AdtFareType).toLowerCase() == "refundable") {
                                        rrfndR = 'r';
                                    }
                                    if ($.trim(Provider).search("SFM") > 0) { }
                                    if ($.trim(Provider).search("SFM") > 0) {
                                        result1 += '<div class="srf hide passenger">SRF</div>';
                                    } else {
                                        result1 += '<div class="srf hide passenger">NRMLF</div>'
                                    }
                                }
                            }
                            result1 += '</div>';
                            if (k == 0) {
                                result1 += '<div class="">';
                                if ($.trim(Provider).search("SFM") > 0 && (OR.items[obr].MarketingCarrier == "SG" || OR.items[obr].MarketingCarrier == "6E" || OR.items[obr].MarketingCarrier == "G8" || OR.items[obr].MarketingCarrier == "UK888") && OR.items[obr].Trip == "D") {
                                    result1 += '<div class="price f20 rgt colorp" style="display:none;"><i class="fa fa-inr" aria-hidden="true"></i>' + OR.items[obr].NetFareSRTF + '</div>';
                                } else {
                                    result1 += '<div class="price f20 rgt colorp" style="display:none;"><i class="fa fa-inr" aria-hidden="true"></i>' + OR.items[obr].TotalFare + '</div>';
                                }
                                result1 += '<div class="clear"></div>';
                                for (var rfo = 0; rfo < OR.items.length; rfo++) {
                                    result1 += '<div class="airstopR hide  gray">' + $.trim(OR.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OR.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                                }
                                result1 += '<div class="lft">';
                                if (k == 0) {
                                    var mulfare2 = DisplayMultipleFares(OR, Ln.items, 1, OR.items[obr].AirLineName, true, false, a, "R", OR.items[obr].LineNumber);
                                    if (mulfare2[0] != "" && mulfare2[0] != ",") {
                                        result1 += mulfare2[0];
                                    }
                                    if (mulfare2[1] != "" && mulfare2[1] != ",") {
                                        result1 += mulfare2[1];
                                    }
                                    result1 += '<div class="deptime hide">' + t.MakeupAdTime(OR.items[obr].DepartureTime).replace(':', '') + '</div>';
                                    result1 += '<div class="rfnd bld hide">' + rrfndR + '</div>';
                                }
                                result1 += '</div>';
                                result1 += '</div>';
                                result1 += '<div class="clear"></div>';
                                result1 += '<div class="shadborder"></div>';
                                result1 += '<div class="row darkbg" style="display:none;">';
                                result1 += '<div class="col-md-4">' + " Delhi" + '</div>';
                                result1 += '<div class="col-md-4">' + " Delhi" + '</div>';
                                result1 += '<div class="col-md-4">';
                                result1 += '</div>';
                                result1 += '</div>';
                                result1 += '<div class="clear"></div>';
                                result1 += '<div class="">';
                                result1 += '<div class=""><div class="summary" id="FS_' + OR.items[obr].LineNumber + unds + OR.items[obr].AirLineName.replace(' ', '-') + '_R' + '"> </div>';
                                result1 += '</div>';
                                result1 += '<div class=""><div  class="summary" id="FDt_' + OR.items[obr].LineNumber + unds + OR.items[obr].AirLineName.replace(' ', '-') + '_R' + '"></div>';
                                result1 += '</div>';
                                result1 += '<div class=""><div  class="summary" id="BIn_' + OR.items[obr].LineNumber + unds + OR.items[obr].AirLineName.replace(' ', '-') + '_R' + '"></div>';
                                result1 += '</div>';
                                result1 += '</div>';
                            }
                            k++;
                            break;
                            result1 += '<div class="clear"></div>';
                        }
                        result1 += '</div>';
                        result1 += '</div>';
                    }
                    if ((a < LnIb && cntR == 0) || (a == LnIb)) {
                        result1 += '</div>';
                        result1 += '</div>';
                        var isfilter = false;
                        setTimeout(doChunkR, 10);
                        break;
                    }
                }
            }
            doChunkR();
        }
    }
    t.divToR.prepend(result1);
};

ResHelper.prototype.CheckMultipleCarrier = function (objFlt) {
    var fltArray = new Array();

    for (var i = 0; i < objFlt.length; i++) {

        fltArray.push(objFlt[i].MarketingCarrier);
    }

    var fltArray1 = fltArray.unique();
    var img1 = "";
    if (fltArray1.length > 1) {
        return true;
    }
    else {
        return false;
    }
}

ResHelper.prototype.GetResultO = function (resultArray, maxLineNumber, Provider) {
    var t = this;
    var result = '';
    var introundfare = '';
    if (resultArray[0].length > 0) {
        var obsLNItems = JSLINQ(resultArray[0]).Select(function (item) {
            return item.SLineNumber
        });
        var LnOb = Math.max.apply(Math, obsLNItems.items);
        i = Math.min.apply(Math, obsLNItems.items);
        let div_md_9 = 1;
        for (i = 1; i <= LnOb; i++) {
            var obs = JSLINQ(resultArray[0]).Where(function (item) {
                return item.SLineNumber == i;
            }).Select(function (item) {
                return item
            });
            var NewItemArray = obs.items.sort(function (a, b) {
                return (a.AdtFare * a.Adult + a.ChdFare * a.Child + a.InfFare * a.Infant) - (b.AdtFare * b.Adult + b.ChdFare * b.Child + b.InfFare * b.Infant)
            });
            //var Ln = JSLINQ(obs.items)
            var Ln = JSLINQ(NewItemArray).Select(function (item) {
                return item.LineNumber
            });
            var OB = JSLINQ(obs.items).Where(function (item) {
                return item.LineNumber == Ln.items[0];
            }).Select(function (item) {
                return item
            });
            var k = 0;
            var O1 = "O";
            var R1 = "R";
            var M1 = "M";
            var D1 = "D";
            var unds = "_";
            var rint = 0;
            var OF;
            var RF;
            var ft;
            if (OB.items.length > 0) {
                result += '<div class="list-item resO">';
                OF = JSLINQ(OB.items).Where(function (item) {
                    return item.Flight == "1";
                }).Select(function (item) {
                    return item
                });
                var fta = JSLINQ(OB.items).Select(function (item) {
                    return item.Flight
                });
                ft = Math.max.apply(Math, fta.items);
                RF = JSLINQ(OB.items).Where(function (item) {
                    return item.Flight == "2";
                }).Select(function (item) {
                    return item
                });
                rint = RF.items.length;
                result += '<div class="theme-search-results" >';
                result += '<div class="theme-search-results-item theme-search-results-item-">';
                result += '<div class="theme-search-results-item-preview">';
                result += '<div id="OneWay">';
                if (OF.items.length > 0) {
                    result += '<div class="col-md-2 col-xs-5" >';
                    result += '<div  class="logoimg col-xs-6">';
                    if (t.CheckMultipleCarrier(OF.items) == true) {
                        result += '<img class="air-img" alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                        result += '</div>';
                        result += '<div>Multiple Carriers</div>';
                        result += '<div class="airlineImage hide">' + OF.items[0].AirLineName + '</div>';
                    } else {
                        if ((OF.items[0].MarketingCarrier == '6E') && ($.trim(OF.items[0].sno).search("INDIGOCORP") >= 0)) {
                            result += '<img class="air-img" alt="" src="../Airlogo/smITZ.gif"/>';
                            result += '</div>';
                            result += '<div class="gray"><span>' + OF.items[0].FlightIdentification + '</div>';
                            result += '<div class="airlineImage">RWT Fare</div>';
                        } else {
                            result += '<img class="air-img" alt="" src="../Airlogo/sm' + OF.items[0].MarketingCarrier + '.gif"/>';
                            result += '</div>';
                            result += '<div class="col-xs-6">';
                            result += '<div class="row"><span>' + OF.items[0].MarketingCarrier + '</span> - ' + OF.items[0].FlightIdentification + '</div>';
                            result += '<div class="row">' + OF.items[0].AirLineName + '</div>';
                            result += '<div class="row">Class - ' + OF.items[0].AdtRbd + '</div>';
                            result += '</div>';
                        }
                    }
                    result += '</div>';
                    result += '<div class="col-md-2 col-xs-3">';
                    result += '<div class="theme-search-results-item-flight-section-meta">';
                    result += '<p class="theme-search-results-item-flight-section-meta-time">' + t.MakeupAdTime(OF.items[0].DepartureTime) + '</p>';
                    result += '<p class="theme-search-results-item-flight-section-meta-date">' + OF.items[0].Departure_Date + '</p>';
                    result += '<p class="theme-search-results-item-flight-section-meta-city ter">' + OF.items[0].DepartureTerminal + '</p>';
                    result += '<p class="theme-search-results-item-flight-section-meta-city stp">' + OF.items[0].Stops + '</p>';
                    result += '<div class="deptime hide">' + t.MakeupAdTime(OF.items[0].DepartureTime).replace(':', '') + '</div>';
                    result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OF.items[0].DepartureTime).replace(':', '')) + '</div>';
                    result += '</div>';
                    result += '</div>';
                    result += '<div class="col-md-2 col-xs-3 tot-dur">';
                    var totDur = "";
                    if (OF.items[0].Provider == "TB") {
                        totDur = t.GetTotalDuration(OF.items);
                    } else {
                        totDur = t.MakeupTotDur(OF.items[0].TotDur)
                    }
                    result += '<div class="theme-search-results-item-flight-section-path">'
                    result += '<div class="theme-search-results-item-flight-section-path-fly-time"><p>' + totDur + ' | ' + OF.items[0].Stops + '</p></div>'
                    result += '<div class="row">'
                    result += '<div class="theme-search-results-item-flight-section-path-line"></div>';
                    if (OF.items[0].Stops == "0-Stop") {
                        result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[0].DepartureLocation + '</div></div>'
                        result += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[OF.items.length - 1].ArrivalLocation + '</div></div>'
                    }
                    if (OF.items[0].Stops == "2-Stop") {
                        result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[0].DepartureLocation + '</div></div>'
                        result += '<div class="theme-search-results-item-flight-section-path-line-middle-1"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[0].ArrAirportCode + '</div></div>'
                        result += '<div class="theme-search-results-item-flight-section-path-line-middle-2"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[1].ArrivalLocation + '</div></div>'
                        result += ' <div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[OF.items.length - 1].ArrivalLocation + '</div></div>'
                    }
                    if (OF.items[0].Stops == "1-Stop") {
                        result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[0].DepartureLocation + '</div></div>'
                        result += '<div class="theme-search-results-item-flight-section-path-line-middle-1" style="left: 50% !important;"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[0].ArrAirportCode + '</div></div>'
                        result += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + OF.items[OF.items.length - 1].ArrivalLocation + '</div></div>'
                    }
                    result += '</div>'
                    var freeMeal = "";
                    result += '<div class="airlineImage hide">' + OF.items[0].AirLineName + '</div>';
                    result += '<div class="stops hide">' + OF.items[0].Stops + '</div>';
                    result += '<div class="totdur hide">' + t.MakeupTotDur(OF.items[0].TotDur).replace(':', '') + '</div>';
                    for (var rfo = 0; rfo < OF.items.length; rfo++) {
                        result += '<div class="airstopO hide  gray">' + $.trim(OF.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OF.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                    }
                    result += '</div>';
                    result += '</div>';
                    result += '<div class="col-md-2 col-xs-4">';
                    result += '<div class="theme-search-results-item-flight-section-meta" style="text-align:right;">';
                    result += '<p class="theme-search-results-item-flight-section-meta-time">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime) + '</p>';
                    result += '<p class="theme-search-results-item-flight-section-meta-date">' + OF.items[OF.items.length - 1].Arrival_Date + '</p>';
                    result += '<p class="theme-search-results-item-flight-section-meta-city ter">' + OF.items[OF.items.length - 1].ArrivalTerminal + '</p>';
                    result += '<div class="arrtime hide">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                    result += '<p class="theme-search-results-item-flight-section-meta-city dur">' + totDur + '</p>';
                    result += '</div>';
                    result += '</div>';
                    result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                    result += '<div class="col-md-4">';
                    result += '<div class="">'
                    if ($.trim(Provider).search("SFM") > 0) {
                        result += '<div class="price f20 lft colorp"> <i clawaitMessagess="fa fa-inr" aria-hidden="true"></i>' + Math.ceil(OF.items[0].TotalFare / 2) + '</div>';
                    } else {
                        result += '<div class="price f20 lft colorp" style="display:none;"> <i class="fa fa-inr" aria-hidden="true"></i>' + OF.items[0].TotalFare + '</div>';
                    }
                    if ((parseInt(OB.items[0].AvailableSeats1) <= 5) && (OB.items[0].ValiDatingCarrier != 'SG')) { }
                    result += '<div class="row">';
                    var mulfare = DisplayMultipleFares(obs, Ln.items, 0, OB.items[0].AirLineName, false, OB.items[0].RTF, i, "O", OB.items[0].LineNumber, rint);


                    if (mulfare[0] != "" && mulfare[0] != ",") {
                        result += mulfare[0];
                    }
                    if (mulfare[1] != "" && mulfare[1] != ",") {
                        result += mulfare[1];
                    }



                  //  result += displayfare[0];
                    //if (RF.items.length > 0) {
                    //    introundfare = displayfare[1];
                    //}
                    result += '</div>';
                    result += '</div>'
                    result += '</div>';
                    result += '<div class="clear"></div>';
                }
                result += '<div class="w100 bggray" style="margin-left:-10px;width:963px;" id="DIV_' + OB.items[0].LineNumber.toString() + unds + OB.items[0].AirLineName.replace(' ', '-') + '">';
                result += '<div class="active in" id="' + OB.items[0].LineNumber + '_O_flth"><div class="summary" id="FS_' + OB.items[0].LineNumber.toString() + unds + OB.items[0].AirLineName.replace(' ', '-') + '" style="background:#fff;"> ';
                result += '</div>';
                result += '</div>';
                result += '<div class="active in" id="' + OB.items[0].LineNumber + '_O_Det"><div  class="summary" id="FDt_' + OB.items[0].LineNumber.toString() + unds + OB.items[0].AirLineName.replace(' ', '-') + '" style="background:#fff;"></div>';
                result += '</div>';
                result += '<div class="active in" id="' + OB.items[0].LineNumber + '_O_BagD"><div  class="summary" id="BIn_' + OB.items[0].LineNumber.toString() + unds + OB.items[0].AirLineName.replace(' ', '-') + '_O" style="background:#fff;"></div>';
                result += '</div>';
                result += '</div>';
                result += '</div>';
                result += '</div>';
                for (var ff = 2; ff <= ft; ff++) {
                    var RF = JSLINQ(OB.items).Where(function (item) {
                        return item.Flight == ff.toString();
                    }).Select(function (item) {
                        return item
                    });
                    result += '<hr>';
                    result += '<i><img alt="" src="/Custom_Design/img/round_trip-512.png" style="width: 30px;position: absolute;left: 50%;right: 50%;top: 101px;"/></i>'
                    result += '<div class="theme-search-results-item-preview" style="/* padding: 0px 0px; */">';
                    result += '<div id="Return" style="">';
                    if (RF.items.length > 0) {
                        result += '<hr class="w80 mauto" style="border:none; border-top:1px solid #eee; float:left; " /> ';
                        result += '<div class="clear1"> </div> ';
                        //result += '<div class="col-md-2 col-xs-2">'
                        //result += '<div>';
                        //if (t.CheckMultipleCarrier(RF.items) == true) {
                        //    result += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                        //    result += '</div>';
                        //    result += '<div>Multiple Carriers</div>';
                        //    result += '<div class="airlineImage hide">' + RF.items[0].AirLineName + '</div>';
                        //} else {
                        //    if ((RF.items[0].MarketingCarrier == '6E') && ($.trim(RF.items[0].sno).search("INDIGOCORP") >= 0)) {
                        //        result += '<img alt="" src="../Airlogo/smITZ.gif"/>';
                        //        result += '</div>';
                        //        result += '<div class="gray"><span>' + RF.items[0].FlightIdentification + '</div>';
                        //        result += '<div class="airlineImage">RWT Fare</div>';
                        //    } else {
                        //        result += '<img alt="" src="../Airlogo/sm' + RF.items[0].MarketingCarrier + '.gif"/>';
                        //        result += '</div>';
                        //        result += '<div class="gray"><span>' + RF.items[0].MarketingCarrier + '</span> - ' + RF.items[0].FlightIdentification + '</div>';
                        //        result += '<div class="airlineImage">' + RF.items[0].AirLineName + '</div>';
                        //    }
                        //}

                        result += '<div class="col-md-2 col-xs-5" >';
                        result += '<div  class="logoimg col-xs-6">';
                        if (t.CheckMultipleCarrier(RF.items) == true) {
                            result += '<img class="air-img" alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                            result += '</div>';
                            result += '<div>Multiple Carriers</div>';
                            result += '<div class="airlineImage hide">' + RF.items[0].AirLineName + '</div>';
                        } else {
                            if ((RF.items[0].MarketingCarrier == '6E') && ($.trim(RF.items[0].sno).search("INDIGOCORP") >= 0)) {
                                result += '<img class="air-img" alt="" src="../AChangedFarePopupShowirlogo/smITZ.gif"/>';
                                result += '</div>';
                                result += '<div class="gray"><span>' + RF.items[0].FlightIdentification + '</div>';
                                result += '<div class="airlineImage">RWT Fare</div>';
                            } else {
                                result += '<img class="air-img" alt="" src="../Airlogo/sm' + RF.items[0].MarketingCarrier + '.gif"/>';
                                result += '</div>';
                                result += '<div class="col-xs-6">';
                                result += '<div class="row"><span>' + RF.items[0].MarketingCarrier + '</span> - ' + RF.items[0].FlightIdentification + '</div>';
                                result += '<div class="row">' + RF.items[0].AirLineName + '</div>';
                                result += '<div class="row">Class - ' + RF.items[0].AdtRbd + '</div>';
                                result += '</div>';
                            }
                        }


                        result += '</div>';
                        result += '<div class="col-md-2 col-xs-3">';
                        result += '<div class="theme-search-results-item-flight-section-meta">';
                        result += '<p class="theme-search-results-item-flight-section-meta-time">' + t.MakeupAdTime(RF.items[0].DepartureTime) + '</p>';
                        result += '<p class="theme-search-results-item-flight-section-meta-date">' + RF.items[0].Departure_Date + '</p>';
                        result += '<p class="theme-search-results-item-flight-section-meta-city">' + RF.items[0].DepartureTerminal + '</p>';
                        result += '<div class="deptime hide">' + t.MakeupAdTime(RF.items[0].DepartureTime).replace(':', '') + '</div>';
                        result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(RF.items[0].DepartureTime).replace(':', '')) + '</div>';
                        result += '</div>';
                        result += '</div>';
                        result += '<div class="col-md-2">';
                        var totDur = "";
                        if (RF.items[0].Provider == "TB") {
                            totDur = t.GetTotalDuration(RF.items);
                        } else {
                            totDur = t.MakeupTotDur(RF.items[0].TotDur)
                        }
                        result += '<div class="theme-search-results-item-flight-section-path">'
                        result += '<div class="theme-search-results-item-flight-section-path-fly-time"><p>' + totDur + ' | ' + RF.items[0].Stops + '</p></div>'
                        result += '<div class="row">'
                        result += '<div class="theme-search-results-item-flight-section-path-line"></div>';
                        if (RF.items[0].Stops == "0-Stop") {
                            result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[0].DepartureLocation + '</div></div>'
                            result += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[RF.items.length - 1].ArrivalLocation + '</div></div>'
                        }
                        if (RF.items[0].Stops == "2-Stop") {
                            result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[0].DepartureLocation + '</div></div>'
                            result += '<div class="theme-search-results-item-flight-section-path-line-middle-1"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[0].ArrAirportCode + '</div></div>'
                            result += '<div class="theme-search-results-item-flight-section-path-line-middle-2"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[1].ArrivalLocation + '</div></div>'
                            result += ' <div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[RF.items.length - 1].ArrivalLocation + '</div></div>'
                        }
                        if (RF.items[0].Stops == "1-Stop") {
                            result += '<div class="theme-search-results-item-flight-section-path-line-start"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[0].DepartureLocation + '</div></div>'
                            result += '<div class="theme-search-results-item-flight-section-path-line-middle-1" style="left: 50% !important;"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[0].ArrAirportCode + '</div></div>'
                            result += '<div class="theme-search-results-item-flight-section-path-line-end"><div class="theme-search-results-item-flight-section-path-line-dot"></div><div class="theme-search-results-item-flight-section-path-line-title">' + RF.items[RF.items.length - 1].ArrivalLocation + '</div></div>'
                        }
                        result += '</div>'
                        var freeMeal = "";
                        result += '<div class="stops hide">' + RF.items[0].Stops + '</div>';
                        result += '<div class="totdur hide">' + t.MakeupTotDur(RF.items[0].TotDur).replace(':', '') + '</div>';
                        for (var rfi = 0; rfo < RF.items.length; rfo++) {
                            result += '<div class="airstopO hide  gray">' + $.trim(RF.items[rfi].Stops).toString().toLowerCase() + '_' + $.trim(RF.items[rfi].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                        }
                        result += '</div>';
                        result += '</div>';
                        result += '<div class="col-md-2 col-xs-3">';
                        result += '<div class="theme-search-results-item-flight-section-meta" style="text-align:right;">';
                        result += '<p class="theme-search-results-item-flight-section-meta-time">' + t.MakeupAdTime(RF.items[RF.items.length - 1].ArrivalTime) + '</p>';
                        result += '<p class="theme-search-results-item-flight-section-meta-date">' + RF.items[RF.items.length - 1].Arrival_Date + '</p>';
                        result += '<p class="theme-search-results-item-flight-section-meta-city">' + RF.items[RF.items.length - 1].ArrivalTerminal + '</p>';
                        result += '<div class="arrtime hide">' + t.MakeupAdTime(RF.items[RF.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                        result += '</div>';
                        result += '</div>';
                        result += '<div class="clear1"></div>';
                    }
                    result += '</div>';
                    result += '</div>';
                }
                result += '<div class="clear"></div>';
                result += '</div>';
                if (OB.items[0].Trip == "I")
                {
                    result += '<div id="' + OB.items[0].LineNumber.toString() + unds + '_Fare" class="hide"></div>';
                    result += '<div id="' + OB.items[0].LineNumber.toString() + unds + '_radio" class="hide"></div>';
                }
                result += '<div id="' + OB.items[0].LineNumber.toString() + unds + OB.items[0].AirLineName.replace(' ', '-') + '" class="hide"></div>';
                result += '</div>';
                result += '</div>';
                result += '</div>';
                result += '</div>';
            }
        }
    }
    return result;
};

ResHelper.prototype.GetSelectedRoundtripFlight = function (resultArray) {
    var e = this;
    $("input:radio").click(function () {
        $('#msg1').html("");
        e.RtfTotalPayDiv.html("");
        $('#showfare').hide();
        //debugger;
        SRFReprice = false;
        if (this.name == "O" || this.name == "RO") {
            //  Obook = null;// $.parseJSON($('#' + this.value).html());

            var lineNums = this.value;
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            ////var fltArray = JSLINQ(gdsJson)
            ////           .Where(function (item) { return item.fltName == lineNums[1]; })
            ////           .Select(function (item) { return item.fltJson; });

            var fltOneWayArray = JSLINQ(resultArray[0])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });

            Obook = fltOneWayArray.items;
            ORTFFare = fltOneWayArray.items[0].TotalFare;
            ORTFLineNo = fltOneWayArray.items[0].LineNumber;
            ORTFVC = fltOneWayArray.items[0].ValiDatingCarrier;
            if (Obook[0].LineNumber.search('SFM') > 0) {
                e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, "SRF"));
            }
            else {
                e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, ""));
            }

            $('.list-item').each(function () {
                $(this).find('.rw').css("background", "#ECEFF1");
                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");

            });

            //var lineNumsO = lineNums.split('api')[0];

            //$('#main_' + lineNums + '_O').removeClass("fltbox").addClass("fltbox01");
            //$('#main1_' + lineNums + '_O').addClass("fltbox02");

            //if ($('#main_' + lineNums + '_O').find("input[type='radio']").attr('checked') != "checked") {
            //    $('#main_' + lineNums + '_O').find("input[type='radio']").attr('checked', 'checked');
            //}
            //if ($('#main1_' + lineNums + '_O').find("input[type='radio']").attr('checked') != "checked") {
            //    $('#main1_' + lineNums + '_O').find("input[type='radio']").attr('checked', 'checked');
            //}

            $(this).attr('checked', 'checked');
            $(this).closest('div.r').closest('div.rw').css("background", "#e40e2b");

            e.RtfFltSelectDiv.css("display", "block");
        }
        else if (this.name == "R" || this.name == "RR") {
            // Rbook = null;// $.parseJSON($('#' + this.value).html());

            var lineNums = this.value;
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            //var fltArrayR = JSLINQ(gdsJson)
            //           .Where(function (item) { return item.fltName == lineNums[1]; })
            //           .Select(function (item) { return item.fltJson; });

            var fltReturnArray = JSLINQ(resultArray[1])
                .Where(function (item) { return item.LineNumber == lineNums; })
                .Select(function (item) { return item });
            Rbook = fltReturnArray.items;
            RRTFFare = fltReturnArray.items[0].TotalFare;
            RRTFLineNo = fltReturnArray.items[0].LineNumber;
            RRTFVC = fltReturnArray.items[0].ValiDatingCarrier;
            $('#hdnSRFPriceLineNoR').val(RRTFLineNo + RRTFVC);
            $('#hdnSRFAircodeR').val(RRTFVC);

            if (Rbook[0].LineNumber.search('SFM') > 0) {
                e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, "SRF"));
            } else {
                e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, ""));
            }
            $('.list-itemR').each(function () {
                $(this).find('.rw').css("background", "#ECEFF1");
                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");
            });
            var lineNumsR = lineNums.split('api1')[0];

            $(this).attr('checked', 'checked');
            $(this).closest('div.r').closest('div.rw').css("background", "#e40e2b");
            //$('#main_' + lineNums + '_R').removeClass("fltbox").addClass("fltbox01");
            //$('#main_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
            //$('#main1_' + lineNums + '_R').addClass("fltbox02");
            //$('#main1_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
            e.RtfFltSelectDiv.css("display", "block");
        }


        try {

            if (Obook != null && Rbook != null) {

                totprevFare = ORTFFare + RRTFFare; //Obook[0].TotalFare + Rbook[0].TotalFare;

                var olnum = $.trim(ORTFLineNo);
                var rlnum = $.trim(RRTFLineNo);
                //if (olnum.search('SFM') > 0 && rlnum.search('SFM') > 0) {
                //    totprevFare =Math.ceil( Obook[0].TotalFare + Rbook[0].TotalFare);
                //    totcurrentFare =Math.ceil( Obook[0].TotalFare + Rbook[0].TotalFare);
                //}
                //else if (olnum.search('SFM') > 0 && rlnum.search('SFM') < 0) {
                //    totprevFare = Math.ceil( Obook[0].TotalFare  )+ Rbook[0].TotalFare ;
                //    totcurrentFare =Math.ceil( Obook[0].TotalFare) + Rbook[0].TotalFare;
                //}
                //else if (olnum.search('SFM') < 0 && rlnum.search('SFM') > 0) {
                //    totprevFare = Obook[0].TotalFare  +Math.ceil( Rbook[0].TotalFare );
                //    totcurrentFare = Obook[0].TotalFare  +Math.ceil( Rbook[0].TotalFare);
                //}
                var rtfBoolStatus = false;


                var ovc = $.trim(ORTFVC);
                var rvc = $.trim(RRTFVC);
                if (olnum.search('SFM') < 0 && rlnum.search('SFM') < 0) {
                    var fltOneWayArray = JSLINQ(resultArray[0])
                        .Where(function (item) { return item.LineNumber == $.trim(ORTFLineNo); })
                        .Select(function (item) { return item });

                    Obook = fltOneWayArray.items;

                    var fltReturnArray = JSLINQ(resultArray[1])
                        .Where(function (item) { return item.LineNumber == $.trim(RRTFLineNo); })
                        .Select(function (item) { return item });
                    Rbook = fltReturnArray.items;




                    var Mkey = Obook[0].SubKey + Rbook[0].SubKey;

                    var fltReturnArrayM = JSLINQ(CommanRTFArray)
                        .Where(function (item) {

                            return item.MainKey == Mkey;
                        })
                        .Select(function (item) { return item });

                    if (fltReturnArrayM.items.length > 0) {
                        var fltReturnArrayO = JSLINQ(fltReturnArrayM.items)
                            .Where(function (item) { return item.MainKey == Mkey && item.Flight == "1"; })
                            .Select(function (item) { return item });

                        var fltReturnArrayR = JSLINQ(fltReturnArrayM.items)
                            .Where(function (item) { return item.MainKey == Mkey && item.Flight == "2"; })
                            .Select(function (item) { return item });

                        ObookSFM = new Array();
                        RbookSFM = new Array();

                        for (var b = 0; b < fltReturnArrayO.items.length; b++) {
                            var obb = jQuery.extend(true, [], fltReturnArrayO.items[b]);;
                            //obb.push( fltReturnArrayO.items[b]);
                            obb.TotalFare = (obb.TotalFare / 2);

                            ObookSFM.push(obb);
                        }


                        for (var b = 0; b < fltReturnArrayR.items.length; b++) {
                            var obb = jQuery.extend(true, [], fltReturnArrayR.items[b]);
                            //obb.push(fltReturnArrayR.items[b]);
                            obb.TotalFare = (obb.TotalFare / 2);
                            RbookSFM.push(obb);
                        }
                        rtfBoolStatus = true;
                    }
                    //   var Mkey = Obook[0].SubKey + Rbook[0].SubKey;

                    //   var fltReturnArrayMSFMO = JSLINQ(resultArray[0])
                    //   .Where(function (item) { return item.MainKey == Mkey; })
                    //   .Select(function (item) { return item });
                    //   var fltReturnArraySFMR = JSLINQ(resultArray[1])
                    //.Where(function (item) { return item.MainKey == Mkey; })
                    //.Select(function (item) { return item });

                    //   ObookSFM = fltReturnArrayMSFMO.items;
                    //   RbookSFM = fltReturnArraySFMR.items;
                    //if (Obook[0].AirLineName == Rbook[0].AirLineName && ObookSFM.length != 0 && RbookSFM.length != 0 && Obook[0].AdtFar == "NRM" && Rbook[0].AdtFar == "NRM" && rtfBoolStatus==true) {

                    if (Obook[0].AirLineName == Rbook[0].AirLineName && ObookSFM != null && RbookSFM != null && ObookSFM.length != 0 && RbookSFM.length != 0 && Obook[0].AdtFar == "NRM" && Rbook[0].AdtFar == "NRM" && rtfBoolStatus == true) {
                        //e.RtfTotalPayDiv.html('<input type="radio" id="radio1" name="RadioGroup1" value="1" checked="true"/>Normal Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-  <input type="radio" id="radio2" name="RadioGroup1" value="2" />SRF Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(ObookSFM[0].TotalFare + RbookSFM[0].TotalFare) + '/- <div class="clear1"></div>');
                        //totcurrentFareSFM = parseInt(ObookSFM[0].TotalFare + RbookSFM[0].TotalFare);
                        SRFResultReprice = new Array();
                        //De	
                        if (ObookSFM[0].ValiDatingCarrier.toUpperCase() == "6E" && RbookSFM[0].ValiDatingCarrier.toUpperCase() == "6E") {
                            BlockUI();
                            SRFResultAfterReprice = SRFPriceItinReq(fltReturnArrayM);
                            $.unblockUI();
                            //debugger;
                            SRFResultReprice = SRFResultAfterReprice;
                            //
                            if (SRFResultAfterReprice != null && SRFResultAfterReprice.length > 0 && SRFResultAfterReprice[0].d.length > 0 && SRFResultAfterReprice[0].d[0].TotalFare > 0) {
                                //if 2
                                SRFReprice = true
                                e.RtfTotalPayDiv.html('<label class="radio inline"><input type="radio" id="radio1" name="RadioGroup1" value="1" checked="true"/><span style="color:#fff">Selected Fare: ₹ ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span>  <span style="color:#fff;"><input type="radio" id="radio2" name="RadioGroup1" value="2" />SRF: ₹ ' + SRFResultAfterReprice[0].d[0].TotalFare + '/-</span>');
                                // e.RtfTotalPayDiv.html('<input type="radio" id="radio1" name="RadioGroup1" value="1" checked="true"/>Normal Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-  <input type="radio" id="radio2" name="RadioGroup1" value="2" />SRF Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(SRFResultAfterReprice[0].d[0].TotalFare) + '/- <div class="clear1"></div>');
                                totcurrentFareSFM = parseInt(SRFResultAfterReprice[0].d[0].TotalFare);
                            }
                            else {
                                //if else 2
                                e.RtfTotalPayDiv.html('<div><input type="radio" id="radio1" name="RadioGroup1" value="1" checked="true"/>Selected Fare: ₹ ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</div>');
                                // totcurrentFareSFM = parseInt(ObookSFM[0].TotalFare + RbookSFM[0].TotalFare);
                                totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
                            }
                            //							
                        }
                        else {
                            //if else 1
                            e.RtfTotalPayDiv.html('<div><input type="radio" id="radio1" name="RadioGroup1" value="1" checked="true"/>Selected Fare: ₹ ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</div>  <div><input type="radio" id="radio2" name="RadioGroup1" value="2" />SRF: ₹ ' + Math.ceil(ObookSFM[0].TotalFare + RbookSFM[0].TotalFare) + '/-</div>');
                            totcurrentFareSFM = parseInt(ObookSFM[0].TotalFare + RbookSFM[0].TotalFare);
                        }
                        //						
                    }
                    else {
                        e.RtfTotalPayDiv.html('<span style="color:#fff;">Selected Fare- ₹ ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span>');
                        totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
                    }

                    e.RtfTotalPayDiv.css("display", "block");
                    e.RtfBookBtn.css("display", "block");
                    $('#showfare').show();
                }
                else {

                    if (olnum.search('SFM') > 0 && rlnum.search('SFM') > 0) {
                        if (ovc.toLowerCase() != rvc.toLowerCase()) {

                            // popup for fare cannot be selected

                            $('#msg1').html("Spacial return fare can be availed on same airline only.");
                            e.RtfBookBtn.css("display", "none");
                            totcurrentFare = 0;
                        }
                        else {

                            var Mkey = Obook[0].SubKey + Rbook[0].SubKey;

                            var fltReturnArrayM = JSLINQ(CommanRTFArray)
                                .Where(function (item) { return item.MainKey == Mkey; })
                                .Select(function (item) { return item });

                            if (fltReturnArrayM.items.length > 0) {
                                var fltReturnArrayO = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.MainKey == Mkey && item.Flight == "1"; })
                                    .Select(function (item) { return item });

                                var fltReturnArrayR = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.MainKey == Mkey && item.Flight == "2"; })
                                    .Select(function (item) { return item });


                                //Obook = fltReturnArrayO.items;
                                //Rbook = fltReturnArrayR.items;



                                //e.RtfFltSelectDivO.html("");
                                //e.RtfFltSelectDivR.html("");

                                //e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, "SRF"));
                                //e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, "SRF"));
                                Obook = new Array();
                                Rbook = new Array();

                                for (var b = 0; b < fltReturnArrayO.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayO.items[b]);;
                                    //obb.push( fltReturnArrayO.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);

                                    Obook.push(obb);
                                }


                                for (var b = 0; b < fltReturnArrayR.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayR.items[b]);
                                    //obb.push(fltReturnArrayR.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);
                                    Rbook.push(obb);
                                }

                                e.RtfTotalPayDiv.html('<span style="color:#fff;">Current Fare- <i class="fa fa-inr" aria-hidden="true"></i> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span><div class="clear1"></div>');
                                e.RtfTotalPayDiv.css("display", "block");
                                e.RtfBookBtn.css("display", "block");
                                isSRF = true;
                                totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
                                //$('#hdnSRFPriceLineNoR').val(RRTFLineNo+RRTFVC);
                                // $('#hdnSRFAircodeR').val(RRTFVC);
                                //if($('#hdnSRFPriceLineNoO').val() !="6E" && Obook[0].ValiDatingCarrier !="SG" && Obook[0].ValiDatingCarrier !="G8" && Rbook[0].ValiDatingCarrier !="6E" && Rbook[0].ValiDatingCarrier !="SG" && Rbook[0].ValiDatingCarrier !="G8")
                                if ($('#hdnSRFAircodeO').val() != "6E" && $('#hdnSRFAircodeO').val() != "SG" && $('#hdnSRFAircodeO').val() != "G8" && $('#hdnSRFAircodeR').val() != "6E" && $('#hdnSRFAircodeR').val() != "SG" && $('#hdnSRFAircodeR').val() != "G8") {
                                    var finalSrfFare = parseInt(totcurrentFare / 2);
                                    var ORTFLineNoO = $('#hdnSRFPriceLineNoO').val();
                                    var RRTFLineNoR = $('#hdnSRFPriceLineNoR').val();
                                    var OldSrfPriceO = $('#Left_' + ORTFLineNoO).html();
                                    var OldSrfPriceR = $('#Right_' + RRTFLineNoR).html();
                                    if (OldSrfPriceO == finalSrfFare) {
                                        $('#OLDSRFO_' + ORTFLineNoO).html("");
                                    }
                                    else {
                                        $('#OLDSRFO_' + ORTFLineNoO).html("&nbsp;" + OldSrfPriceO.strike());
                                    }

                                    if (OldSrfPriceR == finalSrfFare) {
                                        $('#OLDSRFR_' + RRTFLineNoR).html("");
                                    }
                                    else {
                                        $('#OLDSRFR_' + RRTFLineNoR).html("&nbsp;" + OldSrfPriceR.strike());
                                    }

                                    //var OldSrfPriceO=($('#Left_' + ORTFLineNoO).html()).strike();
                                    //var OldSrfPriceR=($('#Right_' +RRTFLineNoR).html()).strike();	
                                    var NewSrfPriceO = OldSrfPriceO + ' ' + finalSrfFare;
                                    var NewSrfPriceR = OldSrfPriceR + ' ' + finalSrfFare;
                                    //$('#Left_' + ORTFLineNoO).html(NewSrfPriceO);
                                    //$('#Right_' +RRTFLineNoR).html(NewSrfPriceR);
                                    $('#Left_' + ORTFLineNoO).html(finalSrfFare);
                                    $('#Right_' + RRTFLineNoR).html(finalSrfFare);

                                    //var RRTFVCR = fltReturnArray.items[0].ValiDatingCarrier;
                                    //var ORTFLineNoO = fltOneWayArray.items[0].LineNumber;
                                    //var ORTFVCO = fltOneWayArray.items[0].ValiDatingCarrier;								
                                    //var RRTFLineNoR = Rbook[0].LineNumber;
                                    //var RRTFVCR = Rbook[0].ValiDatingCarrier;
                                    //var ORTFLineNoO = Obook[0].LineNumber;
                                    //var ORTFVCO = Obook[0].ValiDatingCarrier;												
                                    //var finalSrfFare=parseInt(totcurrentFare/2);
                                    //$('#Left_' + ORTFLineNoO+'1'+ORTFVC).html(finalSrfFare);
                                    //$('#Right_' +RRTFLineNoR+'2'+RRTFVC).html(finalSrfFare);

                                }

                                $('#showfare').show();



                            }
                            else {
                                // popup  to select another fare
                                $('#msg1').html("Fare changed Please Try again.");
                                e.RtfBookBtn.css("display", "none");
                                totcurrentFare = 0;
                            }

                        }


                    }
                    else if (olnum.search('SFM') > 0 && rlnum.search('SFM') < 0) {

                        var Mkey = Obook[0].SubKey + Rbook[0].SubKey;

                        var fltReturnArrayM = JSLINQ(CommanRTFArray)
                            .Where(function (item) { return item.MainKey == Mkey && item.ValiDatingCarrier == ovc; })
                            .Select(function (item) { return item });
                        var splfare = 0;
                        var nrmlFare = Rbook[0].TotalFare;

                        if (fltReturnArrayM.items.length > 0) {
                            splfare = fltReturnArrayM.items[0].TotalFare;

                        }

                        // to get outbound normal fare


                        var fltOutboundArray = JSLINQ(resultArray[0])
                            .Where(function (item) { return item.Subkey == Obook[0].SubKey && item.ValiDatingCarrier == ovc; })
                            .Select(function (item) { return item });
                        if (fltOutboundArray.items.length > 0) {
                            nrmlFare = nrmlFare + fltOutboundArray.items[0].TotalFare;

                        }

                        if (splfare >= 0 && nrmlFare > 0) {
                            if (nrmlFare > splfare && splfare > 0 || (nrmlFare < splfare && fltOutboundArray.items.length <= 0)) {

                                // for srf fare
                                var fltReturnArrayO = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.LineNumber == fltReturnArrayM.items[0].LineNumber && item.Flight == "1"; })
                                    .Select(function (item) { return item });

                                var fltReturnArrayR = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.LineNumber == fltReturnArrayM.items[0].LineNumber && item.Flight == "2"; })
                                    .Select(function (item) { return item });

                                //Obook = fltReturnArrayO.items.slice(true);
                                //Rbook = fltReturnArrayR.items.slice(true);

                                //e.RtfFltSelectDivO.html("");
                                //e.RtfFltSelectDivR.html("");

                                //e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, "SRF"));
                                //e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, "SRF"));
                                Obook = new Array();
                                Rbook = new Array();


                                for (var b = 0; b < fltReturnArrayO.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayO.items[b]);;
                                    //obb.push( fltReturnArrayO.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);

                                    Obook.push(obb);
                                }


                                for (var b = 0; b < fltReturnArrayR.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayR.items[b]);
                                    //obb.push(fltReturnArrayR.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);
                                    Rbook.push(obb);
                                }
                                //for (var b = 0; b < fltReturnArrayO.items.length; b++) {
                                //    var obb = new Array();
                                //    obb.push(fltReturnArrayO.items[b]);
                                //    obb[0].TotalFare =Math.ceil( obb[0].TotalFare / 2);

                                //    Obook.push(obb[0]);
                                //}


                                //for (var b = 0; b < fltReturnArrayR.items.length; b++) {
                                //    var obb = new Array();
                                //    obb.push(fltReturnArrayR.items[b]);
                                //    obb[0].TotalFare =Math.ceil( obb[0].TotalFare / 2);
                                //    Rbook.push(obb[0]);
                                //}

                                e.RtfTotalPayDiv.html('<span style="color:#fff;">Current Fare- <i class="fa fa-inr" aria-hidden="true"></i> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span><div class="clear1"></div>');
                                e.RtfTotalPayDiv.css("display", "block");
                                e.RtfBookBtn.css("display", "block");
                                $('#showfare').show();
                                isSRF = true;

                                totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);



                            }
                            else {
                                // for outbound and inbound normal fare
                                Obook = new Array(); //fltOutboundArray.items;

                                if (fltOutboundArray.items.length > 0) {

                                    for (var b = 0; b < fltOutboundArray.items.length; b++) {
                                        var obb = jQuery.extend(true, [], fltOutboundArray.items[b]);;
                                        //obb.push( fltReturnArrayO.items[b]);
                                        obb.TotalFare = Math.ceil(obb.TotalFare / 2);

                                        Obook.push(obb);
                                    }

                                }
                                else {

                                    $('#msg1').html("Fare changed Please Try again.");
                                    e.RtfBookBtn.css("display", "none");
                                    totcurrentFare = 0;
                                }



                                //e.RtfFltSelectDivO.html("");
                                //e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, ""));
                                if (Obook.length > 0 && Rbook.length > 0) {

                                    e.RtfTotalPayDiv.html('<span style="color:#fff;">Current Fare- <i class="fa fa-inr" aria-hidden="true"></i> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span><div class="clear1"></div>');
                                    e.RtfTotalPayDiv.css("display", "block");
                                    e.RtfBookBtn.css("display", "block");
                                    $('#showfare').show();
                                    totcurrentFare = Obook[0].TotalFare + Rbook[0].TotalFare;
                                }

                            }


                        }

                    }

                    else if (olnum.search('SFM') < 0 && rlnum.search('SFM') > 0) {

                        var Mkey = Obook[0].SubKey + Rbook[0].SubKey;

                        var fltReturnArrayM = JSLINQ(CommanRTFArray)
                            .Where(function (item) { return item.MainKey == Mkey && item.ValiDatingCarrier == ovc; })
                            .Select(function (item) { return item });
                        var splfare = 0;
                        var nrmlFare = Obook[0].TotalFare;

                        if (fltReturnArrayM.items.length > 0) {
                            splfare = fltReturnArrayM.items[0].TotalFare;

                        }

                        // to get inbound normal fare
                        var fltInboundArray = JSLINQ(resultArray[1])
                            .Where(function (item) { return item.Subkey == Rbook[0].SubKey && item.ValiDatingCarrier == rvc; })
                            .Select(function (item) { return item });
                        if (fltInboundArray.items.length > 0) {
                            nrmlFare = nrmlFare + fltInboundArray.items[0].TotalFare;

                        }

                        if (splfare >= 0 && nrmlFare > 0) {
                            if (nrmlFare > splfare && splfare > 0 || (nrmlFare < splfare && fltInboundArray.items.length <= 0)) {

                                // for srf fare
                                var fltReturnArrayO = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.LineNumber == fltReturnArrayM.items[0].LineNumber && item.Flight == "1"; })
                                    .Select(function (item) { return item });

                                var fltReturnArrayR = JSLINQ(fltReturnArrayM.items)
                                    .Where(function (item) { return item.LineNumber == fltReturnArrayM.items[0].LineNumber && item.Flight == "2"; })
                                    .Select(function (item) { return item });

                                //Obook = fltReturnArrayO.items.slice(true);
                                //Rbook = fltReturnArrayR.items.slice(true);

                                //e.RtfFltSelectDivO.html("");
                                //e.RtfFltSelectDivR.html("");

                                //e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, "SRF"));
                                //e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, "SRF"));
                                Obook = new Array();
                                Rbook = new Array();
                                for (var b = 0; b < fltReturnArrayO.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayO.items[b]);;
                                    //obb.push( fltReturnArrayO.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);

                                    Obook.push(obb);
                                }


                                for (var b = 0; b < fltReturnArrayR.items.length; b++) {
                                    var obb = jQuery.extend(true, [], fltReturnArrayR.items[b]);
                                    //obb.push(fltReturnArrayR.items[b]);
                                    obb.TotalFare = Math.ceil(obb.TotalFare / 2);
                                    Rbook.push(obb);
                                }
                                e.RtfTotalPayDiv.html('<span style="color:#fff;">Current Fare- <i class="fa fa-inr" aria-hidden="true"></i> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span><div class="clear1"></div>');
                                e.RtfTotalPayDiv.css("display", "block");
                                e.RtfBookBtn.css("display", "block");
                                isSRF = true;
                                totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
                                $('#showfare').show();

                            }
                            else {
                                // for outbound and inbound normal fare
                                // Obook = fltInboundArray.items;

                                Rbook = new Array();
                                if (fltInboundArray.items.length > 0) {

                                    for (var b = 0; b < fltInboundArray.items.length; b++) {
                                        var obb = jQuery.extend(true, [], fltInboundArray.items[b]);
                                        //obb.push(fltReturnArrayR.items[b]);
                                        obb.TotalFare = Math.ceil(obb.TotalFare / 2);
                                        Rbook.push(obb);
                                    }
                                }
                                else {

                                    $('#msg1').html("Fare changed Please Try again.");
                                    e.RtfBookBtn.css("display", "none");
                                    totcurrentFare = 0;
                                }



                                //e.RtfFltSelectDivR.html("");
                                //e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, ""));
                                if (Obook.length > 0 && Rbook.length > 0) {
                                    e.RtfTotalPayDiv.html('<span style="color:#fff;">Current Fare- <i class="fa fa-inr" aria-hidden="true"></i> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-</span><div class="clear1"></div>');
                                    e.RtfTotalPayDiv.css("display", "block");
                                    e.RtfBookBtn.css("display", "block");
                                    totcurrentFare = Obook[0].TotalFare + Rbook[0].TotalFare;
                                    $('#showfare').show();
                                }
                            }


                        }



                    }




                }

                $('#prevfare').html('');
                $('#FareDiff').html('');
                if (totprevFare != totcurrentFare && totcurrentFare > 0 && totprevFare > 0) {
                    // ChangedFarePopupShow(totprevFare, totcurrentFare, "", 'show');

                    var diff = 'Fare Diff.: ';
                    var prevFare = 'Previous Fare: ';
                    if (parseFloat(totcurrentFare) > parseFloat(totprevFare)) {
                        diff += '(+) <img src="../Images/drs.png"  /> ' + (parseFloat(totcurrentFare) - parseFloat(totprevFare)) + '/-</div> '

                    }
                    else {
                        diff += '(-) <img src="../Images/drs.png" /> ' + (parseFloat(totprevFare) - parseFloat(totcurrentFare)) + '/-</div> '
                    }

                    prevFare += '<img src="../Images/prs.png" /> ' + totprevFare + '/-</div> '

                    // $('#prevfare').html(prevFare);
                    // $('#FareDiff').html(diff);

                    // $('#msg1').html("The fare of the selected flight has changed.");

                }
                //else
                //{
                //    var prevFare = 'Previous Fare: ';
                //    prevFare += '<img src="../Images/prs.png" /> ' + totprevFare + '</div> '
                //    $('#prevfare').html(prevFare);
                //}
            }
        }
        catch (ex) {
            $('#msg1').html("Fare changed Please Try again.");
            e.RtfBookBtn.css("display", "none");
            totcurrentFare = 0;
        }
    });
};
ResHelper.prototype.GetSelectedOneWayFlight = function (resultArray) {
    var e = this;
    $("input:radio").click(function () {
        var lineNums = this.value;

        var fltOneWayArray = JSLINQ(resultArray[0])
            .Where(function (item) { return item.LineNumber == lineNums; })
            .Select(function (item) { return item });

        Obook = fltOneWayArray.items;
        ORTFFare = fltOneWayArray.items[0].TotalFare;
        ORTFLineNo = fltOneWayArray.items[0].LineNumber;
        ORTFVC = fltOneWayArray.items[0].ValiDatingCarrier;

        $("#hdvhgvhgvfh").html(e.DisplaySelectedFlightNew(Obook, ""));
        $(".one-way-select").removeClass("hide");

    });
};
ResHelper.prototype.DisplaySelectedFlightNew = function (objFlt, type) {
    var string;
    var FAirType0 = "";
    if (objFlt.length > 1) {

        var fltArray = new Array();

        for (var i = 0; i < objFlt.length; i++) {

            fltArray.push(objFlt[i].MarketingCarrier);
        }
        var fltArray1 = fltArray.unique();


        var fltone = JSLINQ(objFlt)
            .Where(function (item) { return item.Flight == "1"; })
            .Select(function (item) { return item });

        var fltTwo = JSLINQ(objFlt)
            .Where(function (item) { return item.Flight == "2"; })
            .Select(function (item) { return item });

        var img1 = "";

        if (fltTwo.items.length != 0) {
            string = "<div class='col-md-4'>"
            if (fltone.items.length > 1) {
                img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
            }

            string += '<div class="col-md-3 col-xs-3"><img src="../AirLogo/sm' + fltone.items[0].MarketingCarrier + '.gif" style="width:40px;"/><br>' + fltone.items[0].MarketingCarrier + '-' + fltone.items[0].FlightIdentification + '<br>' + FAirType0 + '' + fltone.items[0].AirLineName + '</div>';

            string += '<div class="col-md-3 col-xs-3">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + fltone.items[0].DepartureLocation + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + fltone.items[0].Departure_Date + '  ' + fltone.items[0].DepartureTime + '</p>';

            string += '</div>';
            string += '</div>';


            string += '<div class="col-md-3 col-xs-2">';
            string += '<div class="fly">';
            string += '<div class="fly1">';
            string += '<span class="jtm">' + fltone.items[0].Departure_Date + '</span>';
            string += '</div><div class="fly2">';
            string += '</div>';
            string += '</div>';
            string += '</div>';
            string += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + fltone.items[fltone.items.length - 1].ArrivalLocation + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + fltone.items[fltone.items.length - 1].Arrival_Date + '  ' + fltone.items[fltone.items.length - 1].ArrivalTime + '</p>';
            //string += '<p class="theme-search-results-item-flight-section-meta-city ter">' + fltone[fltone.length - 1].ArrivalTerminal + '</p>';

            string += '</div>';
            string += '</div>';

            string += '</div>';
            string += '</div>';
            string += "<div class='col-md-4'>"

            var img2 = "";

            if (fltTwo.length > 1) {
                img2 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
            }

            string += '<div class="col-md-3 col-xs-3"><img src="../AirLogo/sm' + fltTwo.items[0].MarketingCarrier + '.gif" style="width:40px;"/><br>' + fltTwo.items[0].MarketingCarrier + '-' + fltTwo.items[0].FlightIdentification + '<br>' + FAirType0 + '' + fltTwo.items[0].AirLineName + '</div>';

            string += '<div class="col-md-3 col-xs-3">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + fltTwo.items[0].DepartureLocation + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + fltTwo.items[0].Departure_Date + '  ' + fltTwo.items[0].DepartureTime + '</p>';

            string += '</div>';
            string += '</div>';


            string += '<div class="col-md-3 col-xs-2">';
            string += '<div class="fly">';
            string += '<div class="fly1">';
            string += '<span class="jtm">' + fltTwo.items[0].Departure_Date + '</span>';
            string += '</div><div class="fly2">';
            string += '</div>';
            string += '</div>';
            string += '</div>';
            string += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + fltTwo.items[fltTwo.items.length - 1].ArrivalLocation + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + fltTwo.items[fltTwo.items.length - 1].Arrival_Date + '  ' + fltTwo.items[fltTwo.items.length - 1].ArrivalTime + '</p>';
            //string += '<p class="theme-search-results-item-flight-section-meta-city ter">' + fltTwo[fltTwo.length - 1].ArrivalTerminal + '</p>';

            string += '</div>';
            string += '</div>';

            string += '</div>';


            string += '</div>';


            string += '<div class="col-md-3" style="border-right: 1px solid #000;border-left: 1px solid #000;"><label style="font-size: 30px; 400 !important; color: #e20000;">₹ ' + objFlt[objFlt.length - 1].TotalFare + '</label><span style="font-size: 30px; font-weight: 600 !important;color: #313131;"> ' + objFlt[objFlt.length - 1].DisplayFareType + '</span><br><p> Net Fare ₹ ' + objFlt[objFlt.length - 1].NetFare + ' | Incentive ₹ ' + objFlt[objFlt.length - 1].TotDis + '</p></div>';

            string += '<div class="col-md-1"> <input type="button" class="buttonfltbk1 btn btn-danger" id = "' + objFlt[objFlt.length - 1].LineNumber + '" title = "' + objFlt[objFlt.length - 1].LineNumber + '" value="Book Now" id="FinalBook1" style="Float: left;" /> </div>';

            
        }
        else {
            if (fltArray1.length > 1) {
                img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
            }

            string = '<div class="col-md-1 col-xs-3"><img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif" style="width:40px;"/><br>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '<br>' + FAirType0 + '' + objFlt[0].AirLineName + '</div>';

            string += '<div class="col-md-2 col-xs-3">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].DepartureCityName + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + objFlt[0].Departure_Date + '  ' + objFlt[0].DepartureTime + '</p>';

            string += '</div>';
            string += '</div>';


            string += '<div class="col-md-2 col-xs-2">';
            string += '<div class="fly">';
            string += '<div class="fly1">';
            string += '<span class="jtm">' + objFlt[0].Departure_Date + '</span>';
            string += '</div><div class="fly2">';
            string += '</div>';
            string += '</div>';
            string += '</div>';
            string += '<div class="col-md-2 col-xs-3" style="text-align: end;">';
            string += '<div class="theme-search-results-item-flight-section-meta">';
            string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[objFlt.length - 1].ArrivalCityName + '</p>';
            string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + objFlt[objFlt.length - 1].Arrival_Date + '  ' + objFlt[objFlt.length - 1].ArrivalTime + '</p>';
            //string += '<p class="theme-search-results-item-flight-section-meta-city ter">' + objFlt[objFlt.length - 1].ArrivalTerminal + '</p>';

            string += '</div>';
            string += '</div>';

            string += '</div>';







            string += '<div class="col-md-3" style="border-right: 1px solid #000;border-left: 1px solid #000;"><label style="font-size: 30px; 400 !important; color: #e20000;">₹ ' + objFlt[objFlt.length - 1].TotalFare + '</label><span style="font-size: 30px; font-weight: 600 !important;color: #313131;"> ' + objFlt[objFlt.length - 1].DisplayFareType + '</span><br><p> Net Fare ₹ ' + objFlt[objFlt.length - 1].NetFare + ' | Incentive ₹ ' + objFlt[objFlt.length - 1].TotDis + '</p></div>';

            string += '<div class="col-md-2"> <input type="button" class="buttonfltbk1 btn btn-danger" id = "' + objFlt[objFlt.length - 1].LineNumber + '" title = "' + objFlt[objFlt.length - 1].LineNumber + '" value="Book Now" id="FinalBook1" style="Float: left;" /> </div>';
        }
        
    }


    else {

        if ((objFlt[0].MarketingCarrier == '6E') && ($.trim(objFlt[0].sno).search("INDIGOCORP") >= 0)) {

            string = '<div class="large-2 medium-2 small-3 columns"><div> <img src="../AirLogo/smITZ.gif"  /></div> <div>' + objFlt[0].FlightIdentification + '</br><span class="restext">' + objFlt[0].DisplayFareType + '</span></div></div>';
        }
        else {

            string = '<div class="col-md-1 col-xs-3"><img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  style="width:40px;"/><br>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '<br>' + FAirType0 + '' + objFlt[0].AirLineName + '</div>';
        }
        string += '<div class="col-md-2 col-xs-3">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].DepartureCityName + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + objFlt[0].Departure_Date + ' ' + objFlt[0].DepartureTime + '</p>';

        string += '</div>';
        string += '</div>';

        string += '<div class="col-md-2 col-xs-2">';
        string += '<div class="fly">';
        string += '<div class="fly1">';
        string += '<span class="jtm">' + objFlt[0].Departure_Date + '</span>';
        string += '</div><div class="fly2">';
        string += '</div>';
        string += '</div>';
        string += '</div>';

        string += '<div class="col-md-2 col-xs-3" style="text-align: end;">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].ArrivalCityName + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date" style="font-size: 15px;">' + objFlt[objFlt.length - 1].Arrival_Date + ' ' + objFlt[0].ArrivalTime + '</p>';
        string += '</div>';
        string += '</div>';



        string += '<div class="col-md-3" style="border-right: 1px solid #000;border-left: 1px solid #000;"><label style="font-size: 30px; 400 !important; color: #e20000;">₹ ' + objFlt[0].TotalFare + '</label><span style="font-size: 30px; font-weight: 600 !important;color: #313131;"> ' + objFlt[0].DisplayFareType + '</span><br><p> Net Fare ₹ ' + objFlt[0].NetFare + ' | Incentive ₹ ' + objFlt[0].TotDis + '</p></div>';


        string += '<div class="col-md-2"> <input type="button" class="buttonfltbk1 btn btn-danger" id = "' + objFlt[0].LineNumber + '" title = "' + objFlt[0].LineNumber + '" value="Book Now" id="FinalBook1" style="Float: left;" /> </div>';

    }

    return string;
};
ResHelper.prototype.DisplaySelectedFlight = function (objFlt, type) {
    var string;
    var FAirType0 = "";
    if (objFlt.length > 1) {

        var fltArray = new Array();

        for (var i = 0; i < objFlt.length; i++) {

            fltArray.push(objFlt[i].MarketingCarrier);
        }

        //if (objFlt[0].AdtFar == "CPN" || objFlt[0].AdtFar == "Coupon Fare") {
        //    FAirType0 = "Coupon Fare";
        //}
        //else if (objFlt[0].AdtFar == "CRP") {
        //    FAirType0 = "Special Fare";
        //}
        //else {
        //    FAirType0 = "";
        //}
        //result += '<div class="AirlineFareType bld colorp italic hide">' + FAirType0 + '</div>';



        var fltArray1 = fltArray.unique();
        var img1 = "";
        if (fltArray1.length > 1) {
            img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
        }
        //else {
        //    if ((objFlt[0].MarketingCarrier == '6E') && ($.trim(objFlt[0].sno).search("INDIGOCORP") >= 0)) {
        //        img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/smITZ.gif"  /></div> <div>' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div>';
        //    }
        //    else {
        //        img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div> <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div>';
        //    }

        //}




        //Kunal Work
        //string = '<div class="large-2 medium-3 small-3 columns"> ' + img1 + '</div>';
        string = '<div class="col-md-3 col-xs-3"><img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif" /><br>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '<br>' + FAirType0 + '</div>';

        string += '<div class="col-md-3 col-xs-3">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].DepartureCityName + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date">' + objFlt[0].DepartureTime + '</p>';
        //string += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - 3</p>';
        string += '</div>';
        string += '</div>';


        string += '<div class="col-md-3 col-xs-2">';
        string += '<div class="fly">';
        string += '<div class="fly1">';
        string += '<span class="jtm">' + objFlt[0].Departure_Date + '</span>';
        string += '</div><div class="fly2">';
        //string += '<span class="fart fart3">Refundable</span>';
        string += '</div>';
        string += '</div>';
        string += '</div>';

        string += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[objFlt.length - 1].ArrivalCityName + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date">' + objFlt[objFlt.length - 1].ArrivalTime + '</p>';
        //string += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - 2</p>';
        string += '</div>';
        string += '</div>';

        //string += '<div class="large-6 medium-6 small-6 columns"><div>' + objFlt[0].DepartureCityName + ' - ' + objFlt[objFlt.length - 1].ArrivalCityName + '</div>';;
        //string += '<div class="passenger lft ">' + objFlt[0].DepartureTime + ' - ' + objFlt[objFlt.length - 1].ArrivalTime + '</div>';
        //string += '<div class="passenger lft f12" style="padding:2px 10px;font-weight:bold">' + '&nbsp;&nbsp;' + objFlt[0].Departure_Date + '</div>';
        string += '</div>';





        if (type == "SRF") {
            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld hide">INR ' + Math.ceil(parseInt(objFlt[0].TotalFare)) + '/-</div>';

        } else {

            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld hide">INR ' + objFlt[0].TotalFare + '/-</div>';
        }
    }
    else {
        //string = '<div class="lft w18"><div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div>  <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</div></div>';

        //if (objFlt[0].AdtFar == "CPN" || objFlt[0].AdtFar == "Coupon Fare") {
        //    FAirType0 = "Coupon Fare";
        //}
        //else if (objFlt[0].AdtFar == "CRP") {
        //    FAirType0 = "Special Fare";
        //}
        //else {
        //    FAirType0 = "";
        //}
        if ((objFlt[0].MarketingCarrier == '6E') && ($.trim(objFlt[0].sno).search("INDIGOCORP") >= 0)) {
            string = '<div class="large-2 medium-2 small-3 columns"><div> <img src="../AirLogo/smITZ.gif"  /></div> <div>' + objFlt[0].FlightIdentification + '</br><span class="restext">' + objFlt[0].DisplayFareType + '</span></div></div>';
        }
        else {
            //string = '<div class="large-3 medium-3 small-3 columns"><div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div> <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div></div>';

            string = '<div class="col-md-3 col-xs-3"><img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif" /><br>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '<br>' + FAirType0 + '</div>';
        }

        //string += '<div class="large-6 medium-6 small-6 columns"><div>' + objFlt[0].DepartureLocation + ' - ' + objFlt[0].ArrivalLocation + '</div><div class="passenger lft ">' + objFlt[0].DepartureTime + ' - ' + objFlt[0].ArrivalTime + '</div><div class="passenger f12 bld" style="padding:2px 10px;">' + '&nbsp;&nbsp;' + objFlt[0].Departure_Date + '</div></div>';

        string += '<div class="col-md-3 col-xs-3">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].DepartureLocation + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date">' + objFlt[0].DepartureTime + '</p>';
        //string += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - 3</p>';
        string += '</div>';
        string += '</div>';

        string += '<div class="col-md-3 col-xs-2">';
        string += '<div class="fly">';
        string += '<div class="fly1">';
        string += '<span class="jtm">' + objFlt[0].Departure_Date + '</span>';
        string += '</div><div class="fly2">';
        //string += '<span class="fart fart3">Refundable</span>';
        string += '</div>';
        string += '</div>';
        string += '</div>';

        string += '<div class="col-md-3 col-xs-3" style="text-align: end;">';
        string += '<div class="theme-search-results-item-flight-section-meta">';
        string += '<p class="theme-search-results-item-flight-section-meta-time">' + objFlt[0].ArrivalLocation + '</p>';
        string += '<p class="theme-search-results-item-flight-section-meta-date">' + objFlt[0].ArrivalTime + '</p>';
        //string += '<p class="theme-search-results-item-flight-section-meta-city">Terminal - 2</p>';
        string += '</div>';
        string += '</div>';

        //fare-fun
        //string = '<div class="large-6 medium-6 small-6 columns">';
        //string = '<div class="row">';
        //string = '<div class="large-2 medium-2 small-2 columns">';
        //string = '<div class="f16">' + objFlt[0].DepartureTime + '</div>';
        //string = '<div>' + objFlt[0].DepartureLocation + '</div>';
        //string = '</div>';
        //string = '<div class="large-2 medium-2 small-2 columns"> → </div>';
        //string = '<div class="f16">' + objFlt[0].ArrivalTime + '</div>';
        //string = '<div>' + objFlt[0].ArrivalLocation + '</div>';
        //string = '</div>';
        //string = '</div>';



        if (type == "SRF") {
            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld hide">INR ' + Math.ceil(parseInt(objFlt[0].TotalFare)) + '/-</div>';

            string += '<div class="large-3 medium-3 small-3 columns"><img src="' + UrlBase + 'images/srf.png" title="Special Return Fare" /></div>';
        } else { string += '<div class="large-3 medium-3 small-3 columns f16 blue bld hide">INR ' + objFlt[0].TotalFare + '/-</div>'; }
    }

    return string;
};
ResHelper.prototype.RTFFinalBook = function () {

    var e = this;
    e.RtfBookBtn.click(function () {
        debugger;
        if (Obook != null && Rbook != null) {
            $("#searchquery").hide();
            //            $("#div_Progress").show();
            //            $.blockUI({
            //                message: $("#waitMessage")
            //            });

            var obookline = Obook[0].LineNumber;
            var rbookline = Rbook[0].LineNumber;

            ChangedFarePopupShow(0, 0, 0, 'hide', 'D');
            if ($("input[name=RadioGroup1]").length == 0) {
                SFMfare = "";
            }
            else {
                SFMfare = $("input[name=RadioGroup1]:checked").val();

            }
            if (SFMfare == "2") {
                var Mkey = Obook[0].SubKey + Rbook[0].SubKey;
                //}

                //if (Obook[0].LineNumber.search('SFM') > 0) {
                /// SPL

                //    var fltReturnArrayM = JSLINQ(CommanRTFArray)
                //          .Where(function (item) {  return item.MainKey == Mkey;  })
                //          .Select(function (item) { return item });
                //if (Obook[0].LineNumber.search('SFM') > 0) {
                /// SPL

                //SRF FARE FIND 6E

                var fltReturnArrayM = null;
                var arr = new Array();
                var compressedData;
                var sTrip = "D";
                if (SRFReprice == true && SRFResultAfterReprice != null && SRFResultAfterReprice.length > 0 && SRFResultAfterReprice[0].d.length > 0 && SRFResultAfterReprice[0].d[0].ValiDatingCarrier.toUpperCase() == "6E") {
                    fltReturnArrayM = SRFResultAfterReprice[0].d;
                    arr = new Array(fltReturnArrayM);
                    //compressedData = LZString.compressToUTF16(JSON.stringify(fltReturnArrayM));
                    //sTrip= fltReturnArrayM.items[0].Trip
                    sTrip = fltReturnArrayM[0].Trip
                }
                else {
                    //END SRF FARE FIND 6E  
                    fltReturnArrayM = JSLINQ(CommanRTFArray)
                        .Where(function (item) { return item.MainKey == Mkey; })
                        .Select(function (item) { return item });
                    //var fltReturnArrayM = JSLINQ(CommanRTFArray)
                    //       .Where(function (item) { return item.LineNumber == obookline; })
                    //       .Select(function (item) { return item });
                    arr = new Array(fltReturnArrayM.items);
                }

                var iscahem = arr[0][0].LineNumber;
                for (var i = 0; i < arr[0].length; i++) {
                    arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber.split('ITZ')[1];
                    arr[0][i].LineNumber = arr[0][i].LineNumber.split('api')[0];

                }
                var compressedArr = (JSON.stringify(arr));
                // var compressedArr = LZString.compressToUTF16(JSON.stringify(arr));
                var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
                //var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
                $.ajax({
                    url: t,
                    type: "POST",
                    //data: JSON.stringify({
                    //    a: arr
                    //}),
                    data: JSON.stringify({
                        a: compressedArr
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {
                        for (var i = 0; i < arr[0].length; i++) {
                            arr[0][i].LineNumber = iscahem;
                        }


                        if (e.d.ChangeFareO.TrackId == "0") {
                            var r = confirm("Fare changed Please Try again.");
                            if (r == true) {
                                location.reload();
                            }
                            $('#ConfmingFlight').hide();
                            $(document).ajaxStop($.unblockUI)

                            //alert("Selected fare has been changed.Please select another flight.");
                            // window.location = UrlBase + "IBEHome.aspx";
                            // $(document).ajaxStop($.unblockUI)
                        }
                        else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                            ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
                        }
                        else {

                            window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                        }



                    },
                    error: function (e, t, n) {
                        for (var i = 0; i < arr[0].length; i++) {
                            arr[0][i].LineNumber = iscahem;
                        }
                        //alert(t)
                        window.location = UrlBase + "Search.aspx";
                    }
                });

            }
            else {
                // For nornal Round trip
                var rtfArray = new Array(Obook, Rbook);

                for (var i = 0; i < rtfArray.length; i++) {

                    for (var j = 0; j < rtfArray[i].length; j++) {

                        rtfArray[i][j].ProductDetailQualifier = rtfArray[i][j].LineNumber.split('ITZ')[1];
                        rtfArray[i][j].LineNumber = rtfArray[i][j].LineNumber.split('api')[0];
                    }

                }

                var compressedRtfArray = (JSON.stringify(rtfArray));
                //var compressedRtfArray = LZString.compressToUTF16(JSON.stringify(rtfArray));
                var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails_LZCmp";
                // var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
                $.ajax({
                    url: t,
                    type: "POST",
                    //data: JSON.stringify({
                    //    a: rtfArray
                    //}),
                    data: JSON.stringify({
                        a: compressedRtfArray
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {

                        //InsertedTID = e.d;

                        for (var obs = 0; obs < Obook.length; obs++) {
                            Obook[obs].LineNumber = obookline;
                        }

                        for (var rbs = 0; rbs < Rbook.length; rbs++) {
                            Rbook[rbs].LineNumber = rbookline;
                        }

                        var tackid = e.d.ChangeFareO.TrackId + ',' + e.d.ChangeFareR.TrackId;
                        //if ($.trim(Obook[0].ProductDetailQualifier) == "CACHE" || $.trim(Rbook[0].ProductDetailQualifier) == "CACHE") {
                        if (e.d.ChangeFareO.TrackId == "0") {
                            var r = confirm("Fare changed Please Try again.");
                            if (r == true) {
                                location.reload();
                            }
                            $('#ConfmingFlight').hide();
                            $(document).ajaxStop($.unblockUI)
                            // alert("Selected fare has been changed.Please select another flight.");
                            //$("#searchquery").show();
                            //$(document).ajaxStop($.unblockUI);
                            //  window.location = UrlBase + "IBEHome.aspx";
                        } else {


                            var totfareOrigunal = parseFloat(Obook[0].TotalFare) + parseFloat(Rbook[0].TotalFare);
                            var totfareNew;
                            var netfareNew;

                            totfareNew = parseFloat(e.d.ChangeFareO.NewTotFare) + parseFloat(e.d.ChangeFareR.NewTotFare);
                            netfareNew = parseFloat(e.d.ChangeFareO.NewNetFare) + parseFloat(e.d.ChangeFareR.NewNetFare);

                            if (totfareOrigunal != totfareNew) {
                                ChangedFarePopupShow(totfareOrigunal, totfareNew, tackid, 'show', netfareNew, 'D');
                            }
                            else {
                                if (e.d.ChangeFareO.TrackId == "0") {
                                    alert("Selected fare has been changed.Please select another flight.");
                                    // $("#searchquery").show();
                                    //$(document).ajaxStop($.unblockUI);
                                    window.location = UrlBase + "Search.aspx";
                                } else {
                                    window.location = UrlBase + "Domestic/PaxDetails.aspx?" + tackid;
                                }

                            }
                        }

                    },
                    error: function (e, t, n) {
                        for (var obs = 0; obs < Obook.length; obs++) {
                            Obook[obs].LineNumber = obookline;
                        }

                        for (var rbs = 0; rbs < Rbook.length; rbs++) {
                            Rbook[rbs].LineNumber = rbookline;
                        }
                        //alert(t)
                        window.location = UrlBase + "Search.aspx";
                    }
                });
            }



        }


    });
};

ResHelper.prototype.FltrSortR = function (resultG) {
    $('#IdFareType').show();
    this.GetStopFilter(resultG[0], 'stops', 'O');
    this.GetUniqueAirline(resultG[0], 'airlineImage', 'O');
    this.GetUniqueAirlineFareType(resultG[0], 'AirlineFareType', 'O');

    var mPr = this.GetMinMaxPrice(resultG[0]);
    var mT = this.GetMinMaxTime(resultG[0]);
    var e = this;
    $(".closeopen").next().slideDown();
    $(".closeopen").toggleClass("closeopen1");

    //if (mPr[0] <= mPr[1] + 100) {
    //    mPr[1] = mPr[1] + 100;
    //}
    e.MainSF.jplist({
        debug: false,
        itemsBox: '.listO'
        , itemPath: '.list-item'
        , panelPath: '.jplist-panel'
        , storage: ''
        // ,noResults: '.jplist-no-results'
        //panel controls
        , controlTypes: {

            'range-slider': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                            , max: mPr[1]
                            , range: true
                            , values: [mPr[0], mPr[1]]
                            , slide: function (event, ui) {
                                $prev.text(ui.values[0]);
                                $next.text(ui.values[1]);
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text($slider.slider('values', 0));
                        $next.text($slider.slider('values', 1));
                    }
                }
            }

            //,'default-sort':{
            //   className: 'DefaultSort'
            //   ,options: {isDefault:false}
            //}
            //prices range slider
            , 'range-slider-Time': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mT[0]
                            , max: mT[1]
                            , range: true
                            , values: [mT[0], mT[1]]
                            , slide: function (event, ui) {
                                $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
                                $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text(e.getFourDigitTime($slider.slider('values', 0)) + " Hrs");
                        $next.text(e.getFourDigitTime($slider.slider('values', 1)) + " Hrs");
                    }
                }
            }


            , 'AirlinefilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'StopfilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'RfndfilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }
            , 'FareTypefilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'reset': {
                className: 'Reset'
                , options: {}
            }

            , 'button-text-filter-groupO': {
                className: 'TextFilterGroup'
                , options: {
                    ignore: '[~!@#$%^&*()+=`\'"\/\\_]+' //[^a-zA-Z0-9]+ not letters/numbers: [~!@#$%^&*\(\)+=`\'"\/\\_]+
                }
            }
            , 'reset1': {
                className: 'Reset'
                , options: {}
            }

            , 'sortCITZ1': {
                className: 'DefaultSort1'
                , options: {}
            }

            , 'sortAirline': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortDeptime': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortArrtime': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortTotdur': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'DTimefilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'AirlineFareTypeO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

        }
    });




    /// for return filter

    this.GetStopFilter(resultG[1], 'stops', 'R');
    this.GetUniqueAirline(resultG[1], 'airlineImage', 'R');
    this.GetUniqueAirlineFareType(resultG[1], 'AirlineFareType', 'R');
    var mPr = this.GetMinMaxPrice(resultG[1]);
    var mT = this.GetMinMaxTime(resultG[1]);
    var e = this;
    //if (mPr[0] <= mPr[1] + 100) {
    //    mPr[1] = mPr[1] + 100;
    //}
    e.MainSFR.jplist({
        debug: false,
        itemsBox: '.listR'
        , itemPath: '.list-itemR'
        , panelPath: '.jplist-panel'
        , storage: ''
        // ,noResults: '.jplist-no-results'
        //panel controls
        , controlTypes: {

            'range-sliderR': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                            , max: mPr[1] + 1
                            , range: true
                            , values: [mPr[0], mPr[1] + 1]
                            , slide: function (event, ui) {
                                $prev.text(ui.values[0]);
                                $next.text(ui.values[1]);
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text($slider.slider('values', 0));
                        $next.text($slider.slider('values', 1));
                    }
                }
            }

            //,'default-sort':{
            //   className: 'DefaultSort'
            //   ,options: {isDefault:false}
            //}
            //prices range slider
            , 'range-slider-TimeR': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mT[0]
                            , max: mT[1]
                            , range: true
                            , values: [mT[0], mT[1]]
                            , slide: function (event, ui) {
                                $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
                                $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text(e.getFourDigitTime($slider.slider('values', 0)) + " Hrs");
                        $next.text(e.getFourDigitTime($slider.slider('values', 1)) + " Hrs");
                    }
                }
            }


            , 'AirlinefilterR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'StopfilterR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'RfndfilterR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }
            , 'FareTypefilterR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }
            , 'reset': {
                className: 'Reset'
                , options: {}
            }
            , 'button-text-filter-groupR': {
                className: 'TextFilterGroup'
                , options: {
                    ignore: '[~!@#$%^&*()+=`\'"\/\\_]+' //[^a-zA-Z0-9]+ not letters/numbers: [~!@#$%^&*\(\)+=`\'"\/\\_]+
                }
            }
            , 'reset1': {
                className: 'Reset'
                , options: {}
            }
            , 'sortCITZR': {
                className: 'DefaultSort1'
                , options: {}
            }

            , 'sortAirlineR': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortDeptimeR': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortArrtimeR': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortTotdurR': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'DTimefilterR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }
            , 'AirlineFareTypeR': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

        }
    });






};

ResHelper.prototype.FltrSort = function (result) {
    $('#IdFareType').hide();
    this.GetStopFilter(result[0], 'stops', 'O');
    this.GetUniqueAirline(result[0], 'airlineImage', 'O');
    this.GetUniqueAirlineFareType(result[0], 'AirlineFareType', 'O');
    var mPr = this.GetMinMaxPrice(result[0]);
    var mT = this.GetMinMaxTime(result[0]);
    var e = this;
    $(".closeopen").next().slideDown();
    $(".closeopen").toggleClass("closeopen1");

    //if (mPr[0] <= mPr[1]+100) {
    //    mPr[1] = mPr[1] + 100;
    //}
    e.MainSF.jplist({
        debug: false,
        itemsBox: '.list'
        , itemPath: '.list-item'
        , panelPath: '.jplist-panel'
        , storage: '' //'', 'cookies', 'localstorage'			
        //, storageName: 'jplist-list-grid'
        // ,noResults: '.jplist-no-results'
        //panel controls
        , controlTypes: {

            'range-slider': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                            , max: mPr[1]
                            , range: true
                            , values: [mPr[0], mPr[1]]
                            , slide: function (event, ui) {
                                $prev.text(ui.values[0]);
                                $next.text(ui.values[1]);
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text($slider.slider('values', 0));
                        $next.text($slider.slider('values', 1));
                    }
                }
            }

            , 'default-sort': {
                className: 'DefaultSort'
                , options: { isDefault: false }
            }
            //prices range slider
            , 'range-slider-Time': {
                className: 'RangeSlider'
                , options: {

                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: mT[0]
                            , max: mT[1]
                            , range: true
                            , values: [mT[0], mT[1]]
                            , slide: function (event, ui) {
                                $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
                                $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
                            }
                        });
                    }

                    , set_values: function ($slider, $prev, $next) {

                        $prev.text(e.getFourDigitTime($slider.slider('values', 0)) + " Hrs");
                        $next.text(e.getFourDigitTime($slider.slider('values', 1)) + " Hrs");
                    }
                }
            }

            , 'AirlinefilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'StopfilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'RfndfilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }
            , 'reset': {
                className: 'Reset'
                , options: {}
            }
            , 'reset1': {
                className: 'Reset'
                , options: {}
            }


            , 'sortCITZ': {
                className: 'DefaultSort1'
                , options: {}
            }

            , 'sortAirline': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortDeptime': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortArrtime': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'sortTotdur': {
                className: 'DefaultSort1'
                , options: {}
            }
            , 'button-text-filter-groupO': {
                className: 'TextFilterGroup'
                , options: {
                    ignore: '[~!@#$%^&*()+=`\'"\/\\_]+' //[^a-zA-Z0-9]+ not letters/numbers: [~!@#$%^&*\(\)+=`\'"\/\\_]+
                }
            }

            , 'views': {
                className: 'Views'
                , options: {}
            }
            , 'DTimefilterO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

            , 'AirlineFareTypeO': {
                className: 'CheckboxTextFilter'
                , options: {
                    ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                }
            }

        }
    });



};


//ResHelper.prototype.GetUniqueAirline = function (result, cls, type) {
ResHelper.prototype.GetUniqueAirlineFareType = function (result, cls, type) {
    //debugger;
    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
        .OrderBy(function (item) { return item.AdtFar })
        .Where(function (item) { return item.AdtFar.length < 5 })
        .Select(function (item) { return item.AdtFar });

    marray = OF.items.unique1();

    var str = '<div class="jplist-group" data-control-type="AirlineFareType' + type + '" data-control-action="filter"  data-control-name="AirlineFareType' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'
    var CouponFareF = true;
    var SpecialFareF = true;
    var NormalFareF = true;
    for (var i = 0; i < marray.length; i++) {
        var FareTypeS1 = "";
        var FilterFareTypeShow = "";
        if (marray[i] == "CPN" || marray[i] == "Coupon Fare") {
            FilterFareTypeShow = "Coupon";
            FareTypeS1 = "CPN";
        }
        else if (marray[i] == "CRP") {
            FilterFareTypeShow = "RWT Fare";
            FareTypeS1 = "CRP";
        }
        else if (marray[i] == "HBAG" || marray[i] == "HBG") {
            FilterFareTypeShow = "Hand Baggage";
            FareTypeS1 = "HBAG";
        }
        else if (marray[i] == "PKG") {
            FilterFareTypeShow = "Package Fare";
            FareTypeS1 = "PKG";
        }
        else if (marray[i] == "SME") {
            FilterFareTypeShow = "SME Fare";
            FareTypeS1 = "SME";
        }
        else if (marray[i] != "NRM" && marray[i] != "") {
            FilterFareTypeShow = marray[i];
            FareTypeS1 = marray[i];
        }
        else {
            FilterFareTypeShow = "Published";
            //FareTypeS1 = marray[i];
            FareTypeS1 = "NRM";
        }
        if (FareTypeS1 != "") {
            //CouponFare = false;
            //SpecialFare = false;
            //NormalFareF == false;
            str += '<label for="' + FareTypeS1 + '" style="width:100%;"><input value="' + FareTypeS1 + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  />&nbsp;' + FilterFareTypeShow + '</label>';
            //str += '<div class="lft w8"> <input value="' + marray[i] + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft w80" style="padding-top:3px;"><label for="' + marray[i] + '">' + FilterFareTypeShow + '</label> </div> <div class="clear"> </div>';
        }
    }
    str += '</div>';

    if (type == 'O') {
        //e.airlineFilter.html(str);
        e.AirlineFareType.html(str);
    }
    else {
        //e.airlineFilterR.html(str);
        e.AirlineFareTypeR.html(str);

    }


};


//function mtrxshhd() {
//    $(".matrix").toggelClass("hide");
//}

function toggleFD(id) {


    if ($("#" + id).is(":visible")) {
        $("#BIn_" + id).hide();
        $("#FD_" + id).hide();

        //$("#" + id).hide();
    }
    else {
        //$(".ABC").hide();
        //$(".summary").hide();
        $("#" + id).slideToggle();
        // $("#CL_" + id).trigger("click");
    }

}
function displayFS(id, d) {

    $(".selectbtn").removeClass("selectbtn");
    $(d).addClass("selectbtn");
    $(".summary").hide();
    if ($.trim(id).search("RTF") > 0) {

        var lineNums = id.split('_');

        var airc = lineNums[2] + "_" + lineNums[3];
        var fltArray = JSLINQ(CommanRTFArray)
            .Where(function (item) { return item.AirLineName == airc.replace('-', ' '); })
            .Select(function (item) { return item.fltJson; });

        var fltSelectedArray = JSLINQ(fltArray.items[0][0])
            .Where(function (item) { return item.LineNumber == lineNums[1]; })
            .Select(function (item) { return item });
    }
    else {
        var lineNums = id.split('_');
        var fltArray = JSLINQ(CommonResultArray[0])
            .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' '); })
            .Select(function (item) { return item; });


        if (lineNums.length == 4 && lineNums[3] == "R") {

            var fltArray = JSLINQ(CommonResultArray[1])
                .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' '); })
                .Select(function (item) { return item; });
            var fltSelectedArray = JSLINQ(fltArray.items)
                .Where(function (item) { return item.LineNumber == lineNums[1]; })
                .Select(function (item) { return item });
        }
        else {
            var fltSelectedArray = JSLINQ(fltArray.items)
                .Where(function (item) { return item.LineNumber == lineNums[1]; })
                .Select(function (item) { return item });
        }
    }

    var str1 = "";
    var gTotal = 0;

    strResult = "<table border='0' cellpadding='0' cellspacing='0' id='FareBreak' class='table table-striped breakup w100'>";
    // strResult = strResult + "<thead><tr><th colspan='5' class='hd'>Fare Summary</th></tr>";
    //var totalPax = fltSelectedArray.items[0].Adult + fltSelectedArray.items[0].Child + fltSelectedArray.items[0].Infant
    strResult = strResult + "<tr>";
    strResult = strResult + "<th>Pax Type</th>";
    strResult = strResult + "<th>Base Fares</th>";
    strResult = strResult + "<th>Fuel Surcharge</th>";
    strResult = strResult + "<th>Other Tax</td>";
    strResult = strResult + "<th>Total Fare</th></tr></thead>";
    strResult = strResult + "<tbody><tr><td class='bld'>ADT</td>";
    strResult = strResult + "<td>" + fltSelectedArray.items[0].AdtBfare + "</td>";
    strResult = strResult + "<td>" + fltSelectedArray.items[0].AdtFSur + "</td>";
    strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].AdtTax - fltSelectedArray.items[0].AdtFSur) + "</td>";  //fltSelectedArray.items[0].TotMrkUp / totalPax
    strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].AdtFare) + "</td></tr>";


    if (fltSelectedArray.items[0].Child > 0) {
        strResult = strResult + "<tr><td class='bld'>CHD</td>";
        strResult = strResult + "<td>" + fltSelectedArray.items[0].ChdBFare + "</td>";
        strResult = strResult + "<td>" + fltSelectedArray.items[0].ChdFSur + "</td>";

        strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].ChdTax - fltSelectedArray.items[0].ChdFSur) + "</td>";
        strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].ChdFare) + "</td></tr>"
        //strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].ChdTax - fltSelectedArray.items[0].ChdFSur + (fltSelectedArray.items[0].TotMrkUp / totalPax)) + "</td>";
        //strResult = strResult + "<td>" + +parseFloat((fltSelectedArray.items[0].TotMrkUp / totalPax) + fltSelectedArray.items[0].ChdFare) + "</td></tr>"
    }
    if (fltSelectedArray.items[0].Infant > 0) {
        strResult = strResult + "<tr><td class='bld'>INF</td>";
        strResult = strResult + "<td>" + fltSelectedArray.items[0].InfBfare + "</td>";
        strResult = strResult + "<td>" + fltSelectedArray.items[0].InfFSur + "</td>";
        strResult = strResult + "<td>" + parseFloat(fltSelectedArray.items[0].InfTax - fltSelectedArray.items[0].InfFSur) + "</td>";
        strResult = strResult + "<td>" + (fltSelectedArray.items[0].InfFare) + "</td></tr>"
    }
    //strResult = strResult + "<tr>";
    //strResult = strResult + "<td colspan='5' class='bdrtop'>&nbsp; </td>";
    //strResult = strResult + "</tr>";
    strResult = strResult + "<tr Style='display:none'>";
    strResult = strResult + "<td><span class='bld1'>Service Tax:</span><br />" + fltSelectedArray.items[0].STax + "</td>";
    //strResult = strResult + "<td><span class='bld1'>Tran. Fee:</span><br />" + fltSelectedArray.items[0].TotMrkUp + "</td>";
    strResult = strResult + "<td><span class='bld1'>Tran. Fee:</span><br />0</td>";
    strResult = strResult + "<td><span class='bld1'>Commission:</span><br />" + fltSelectedArray.items[0].TotDis + "</td>";
    strResult = strResult + "<td><span class='bld1'>Cash Back:</span><br />" + fltSelectedArray.items[0].TotCB + "</td>";
    strResult = strResult + "<td><span class='bld1'>TDS:</span><br />" + fltSelectedArray.items[0].TotTds + "</td>";
    strResult = strResult + "</tr>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td colspan='4' class='bld1'>" + fltSelectedArray.items[0].Adult + " ADT,&nbsp;&nbsp;&nbsp; " + fltSelectedArray.items[0].Child + " CHD,&nbsp;&nbsp;&nbsp; " + fltSelectedArray.items[0].Infant + " INF &nbsp;&nbsp;&nbsp;</td>";
    strResult = strResult + "<td colspan='' class='text-right blue'>Total Fare: " + fltSelectedArray.items[0].TotalFare + "</td>";
    strResult = strResult + "</tr>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td colspan='4' class='bld1'>&nbsp;</td>";
    strResult = strResult + "<td colspan='' class='text-right blue'>Net Fare: " + fltSelectedArray.items[0].NetFare + "</td>";
    strResult = strResult + "</tr>";
    //strResult = strResult + "<td colspan='5' class='bdrtop'>&nbsp; </td>";
    //if (fltSelectedArray.items[0].AdtFar != null && fltSelectedArray.items[0].AdtFar != "") {
    //    strResult = strResult + "<tr>";
    //    strResult = strResult + "<td class='bld1 '>" + fltSelectedArray.items[0].AdtFar.split('/')[0].split('-')[0] + ": <br/>" + "<span class='blue'>" + fltSelectedArray.items[0].AdtFar.split('/')[0].split('-')[1] + "</span>" + "</td>";
    //    strResult = strResult + "<td colspan='2' class='bld1 '>" + fltSelectedArray.items[0].AdtFar.split('/')[1].split('-')[0] + ": <br/>" + "<span class='redd'>" + fltSelectedArray.items[0].AdtFar.split('/')[1].split('-')[1] + "</span>" + "</td>";
    //    strResult = strResult + "<td colspan='2' class='bld1'>" + fltSelectedArray.items[0].AdtFar.split('/')[2].split('-')[0] + ": <br/>" + "<span class='green'>" + fltSelectedArray.items[0].AdtFar.split('/')[2].split('-')[1] + "</span>" + "</td>";
    //    // strResult = strResult + "<td><span class='bld1'>FareCondition:</span><br />" + Refundable + "</td>";
    //    // strResult = strResult + "<td><span class='bld1'>CancelPenalty:</span><br />" + 2000 + "</td>";
    //    // strResult = strResult + "<td><span class='bld1'>ChangePenalty :</span><br />" + 2000 + "</td>";
    //    // strResult = strResult + "<td colspan='2' >&nbsp;</td>";
    //    strResult = strResult + "</tr>";
    //}
    strResult = strResult + "</tbody>";
    strResult = strResult + "</table>";

    $("#" + id).html(strResult);
    $("#" + id).show();
    return false;
}
function displayFDt(id, d) {

    $(".selectbtn").removeClass("selectbtn");
    $(d).addClass("selectbtn");
    $(".summary").hide();
    var lineNums = id.split('_');
    var str1 = '';
    if (lineNums.length == 4) {
        if (lineNums[3] == "RTF") {
            var lineNums = id.split('_');

            var airc = lineNums[2] + "_" + lineNums[3];
            var fltArray = JSLINQ(gdsJsonRTF)
                .Where(function (item) { return item.fltName == airc; })
                .Select(function (item) { return item; });

            var fltSelectedArray = JSLINQ(fltArray.items[0][0])
                .Where(function (item) { return item.LineNumber == lineNums[1]; })
                .Select(function (item) { return item });
            var O = (JSLINQ(fltSelectedArray.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item })).items; //$.parseJSON($('#' + this.rel + 'O').html());
            var R = (JSLINQ(fltSelectedArray.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item })).items;

            str1 = '<div>';
            if (O.length > 0) {
                str1 += '<div class="depcity">';
                for (var i = 0; i < O.length; i++) {
                    str1 += '<div class="lft w25"><img alt="" src="' + UrlBase + '/Images/AirLogo/sm' + O[i].MarketingCarrier + '.gif" /><br />' + O[i].MarketingCarrier + ' - ' + O[i].FlightIdentification + '</div>'
                    //str1 += '<div class="lft w10 bld textaligncenter"><p>' + calFlightDur(O[i].DepartureTime.replace(":", ""), O[i].ArrivalTime.replace(":", "")) + ' HRS</p></div>'
                    str1 += '<div class="lft w25"><span>' + O[i].DepartureLocation + '&nbsp;' + [O[i].DepartureTime.replace(":", "").slice(0, 2), ":", O[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O[i].DepartureCityName + '<br />' + O[i].Departure_Date + '<br />' + O[i].DepartureAirportName + ' Airport, Terminal ' + O[i].DepartureTerminal + '</div>';
                    str1 += '<div class="lft w25 dvsrc"><i class="fa fa-plane-departure"></i><br/> ' + O[0].TotDur + '</div>'
                    str1 += '<div class="lft w25"><span>' + [O[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O[i].ArrivalLocation + '</span><br />' + O[i].ArrivalCityName + '<br />' + O[i].Arrival_Date + '<br />' + O[i].ArrivalAirportName + ' Airport, Terminal  ' + O[i].ArrivalTerminal + '</div><div class="clear"></div>';

                }
                str1 += '</div>';
            }

            if (R.length > 0) {
                str1 += '<div class="depcity">';
                for (var j = 0; j < R.length; j++) {
                    str1 += '<div class="lft w25"><img alt="" src="' + UrlBase + '/Images/AirLogo/sm' + R[j].MarketingCarrier + '.gif" /><br />' + R[j].MarketingCarrier + ' - ' + R[j].FlightIdentification + '</div>'
                    //str1 += '<div class="lft w10 f20 bld textaligncenter"><p>' + calFlightDur(R[j].DepartureTime.replace(":", ""), R[j].ArrivalTime.replace(":", "")) + ' HRS</p></div>'
                    str1 += '<div class="lft w25"><span>' + R[j].DepartureLocation + '&nbsp;' + [R[j].DepartureTime.replace(":", "").slice(0, 2), ":", R[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R[j].DepartureCityName + '<br />' + R[j].Departure_Date + '<br />' + R[j].DepartureAirportName + ' Airport, Terminal ' + R[j].DepartureTerminal + '</div>';
                    str1 += '<div class="lft w25 dvsrc"><i class="fa fa-plane-departure"></i><br/> ' + R[0].TotDur + '</div>'
                    str1 += '<div class="lft w25"><span>' + [R[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R[j].ArrivalLocation + '</span><br />' + R[j].ArrivalCityName + '<br />' + R[j].Arrival_Date + '<br />' + R[j].ArrivalAirportName + ' Airport, Terminal  ' + R[j].ArrivalTerminal + '</div><div class="clear"></div>';
                }
                str1 += '</div>';
            }
            str1 += '</div><div class="clear"></div>';
        }
        else {

            var OB;
            if ($.trim(lineNums[3]) == "O") {

                var fltArray = JSLINQ(CommonResultArray[0])
                    .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' '); })
                    .Select(function (item) { return item; });
                OB = JSLINQ(fltArray.items)
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });
                //if ($('.hidtriptype').val().toLowerCase() == "r" && $('#FltSearch_hidFrom').val().split(',')[1] == "IN" && $('#FltSearch_hidTo').val().split(',')[1] == "IN") {
                //    tripsatus = 'R'
                //}
                //else {
                //    tripsatus = 'O'
                //}
            }
            else if ($.trim(lineNums[3]) == "R") {
                var fltArray = JSLINQ(CommonResultArray[1])
                    .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' '); })
                    .Select(function (item) { return item; });
                OB = JSLINQ(fltArray.items)
                    .Where(function (item) { return item.LineNumber == lineNums[1]; })
                    .Select(function (item) { return item });

                tripsatus = 'R'
            }
            var O = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "1"; })
                .Select(function (item) { return item });
            var R = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == "2"; })
                .Select(function (item) { return item });

            str1 += '<div style="">';
            //str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; right:3px;font-size:20px"  class="cls"></div>';
            // str1 += '<div class="mtop20"> &nbsp;</div><div>';
            if (O.items.length > 0) {
                try {
                    if (parseInt(O.items[0].AvailableSeats1) <= 5) {

                        str1 += '<div class="colorwht lft" style="background:#43566f; padding:2px 5px; border-radius:4px; position:relative; top:6px;">' + O.items[0].AvailableSeats1 + ' Seat(s) Left!</div><div class="clear1"></div>';
                    }
                } catch (ex) { }

                str1 += '<div class="depcity">';
                for (var i = 0; i < O.items.length; i++) {
                    if (i == 0) {
                        str1 += '<div class="lft w25"><div><span class="">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span>&nbsp;<span class="">' + MakeupTotDur(O.items[0].TotDur) + '</span></div><div class="clear1"></div>';
                    }
                    else {
                        str1 += '<div class="clear1"></div><hr /><div class="clear1"></div><div class="lft w24">';
                    }
                    str1 += '<div class="lft w50"><img alt="" src="' + UrlBase + '/Images/AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '</div>';
                    str1 += '<div class="lft w50 bld textaligncenter"><p>' + calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS</p></div></div>';
                    str1 += '<div class="lft w25" style="text-align:right;"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '<br />' + TerminalAirportInfo(O.items[i].DepartureTerminal, O.items[i].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w25 dvsrc"><i class="fa fa-plane-departure"></i></div>';
                    str1 += '<div class="lft w25"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '<br />' + TerminalAirportInfo(O.items[i].ArrivalTerminal, O.items[i].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }
            if (R.items.length > 0) {
                str1 += '</div><div class="depcity1"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + R.items[0].TotDur + '<div class="clear"></div>';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<div class="lft w25"><img alt="" src="' + UrlBase + '/Images/AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '</div>'
                    str1 += '<div class="lft w10 f20 bld textaligncenter"><p>' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS</p></div>'
                    str1 += '<div class="lft w25" style="text-align:right;"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w10 dvsrc"><i class="fa fa-plane-departure"></i></div>'
                    str1 += '<div class="lft w25"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R.items[j].ArrivalLocation + '</span><br />' + R.items[j].ArrivalCityName + '<br />' + R.items[j].Arrival_Date + '<br />' + TerminalAirportInfo(R.items[j].ArrivalTerminal, R.items[j].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }

            str1 += '<div class="clear"></div>';
            str1 += '</div></div>';
            str1 += '</div>';
            str1 += '</div>';
            // main div ended
            str1 += '<div class="clear"></div>';
        }
    }
    else {
        var lineNums = id.split('_');

        var airc = lineNums[2] + "_" + lineNums[3];


        var lineNums = id.split('_');
        var airc = lineNums[2] + "_" + lineNums[3];
        var fltArray = JSLINQ(CommonResultArray[0])
            .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' '); })
            .Select(function (item) { return item });
        var fltSelectedArray = JSLINQ(fltArray.items)
            .Where(function (item) { return item.LineNumber == lineNums[1]; })
            .Select(function (item) { return item });
        var ftcnt = ((JSLINQ(fltSelectedArray.items)
            //.Where(function (item) { return item.Flight == "1"; })
            .Select(function (item) { return item.Flight })).items).unique();
        var str1 = '<div>';
        for (var k = 1; k <= ftcnt.length; k++) {
            var O = (JSLINQ(fltSelectedArray.items)
                .Where(function (item) { return item.Flight == k.toString(); })
                .Select(function (item) { return item })).items; //$.parseJSON($('#' + this.rel + 'O').html());
            if (O.length > 0) {
                str1 += '<div class="depcity">';
                for (var i = 0; i < O.length; i++) {
                    str1 += '<div class="w25 lft"><img alt="" src="' + UrlBase + '/Images/AirLogo/sm' + O[i].MarketingCarrier + '.gif" /><br />' + O[i].MarketingCarrier + ' - ' + O[i].FlightIdentification + '</div>'
                    str1 += '<div class="lft w25"><span>' + O[i].DepartureLocation + '&nbsp;' + [O[i].DepartureTime.replace(":", "").slice(0, 2), ":", O[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O[i].DepartureCityName + '<br />' + O[i].Departure_Date + '<br />' + O[i].DepartureAirportName + ' Airport, Terminal ' + O[i].DepartureTerminal + '</div>';
                    str1 += '<div class="lft w25 dvsrc"><i class="fa fa-plane-departure"></i><br/>' + O[0].TotDur + '</div>'
                    str1 += '<div class="lft w25"><span>' + [O[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O[i].ArrivalLocation + '</span><br />' + O[i].ArrivalCityName + '<br />' + O[i].Arrival_Date + '<br />' + O[i].ArrivalAirportName + ' Airport, Terminal  ' + O[i].ArrivalTerminal + '</div><div class="clear"></div>';
                }
                str1 += '</div>';
            }
        }
        str1 += '</div><div class="clear"></div>';
    }
    $("#" + id).html(str1);
    $("#" + id).show();
    return false;
}

function displayBIn(id, d) {

    event.preventDefault();
    var e = this;
    $(".selectbtn").removeClass("selectbtn");
    $(d).addClass("selectbtn");
    var RHandler = new ResHelper;

    $(".summary").hide();
    // var main = $.parseJSON($('#' + this.rel + 'M').html());
    //   
    // if (this.rel != null) {
    $('#' + id).show();
    // var lineNums = this.rel; //.split('_');
    //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };

    var lineNums = id.split('_')
    // // var airc = lineNums[1] + "_" + lineNums[2];
    // var OB = JSLINQ(result[0])
    //                      .Where(function (item) { return item.LineNumber == lineNums[0]; })
    //                      .Select(function (item) { return item });

    // var O = JSLINQ(OB.items)
    //                  .Where(function (item) { return item.Flight == "1"; })
    //                  .Select(function (item) { return item });

    // var R = JSLINQ(OB.items)
    //.Where(function (item) { return item.Flight == "2"; })
    //.Select(function (item) { return item });

    var OB;
    if ($.trim(lineNums[3]) == "O") {

        var fltArray = JSLINQ(CommonResultArray[0])
            .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' ');; })
            .Select(function (item) { return item; });
        OB = JSLINQ(fltArray.items)
            .Where(function (item) { return item.LineNumber == lineNums[1]; })
            .Select(function (item) { return item });
        //if ($('.hidtriptype').val().toLowerCase() == "r" && $('#FltSearch_hidFrom').val().split(',')[1] == "IN" && $('#FltSearch_hidTo').val().split(',')[1] == "IN") {
        //    tripsatus = 'R'
        //}
        //else {
        //    tripsatus = 'O'
        //}
    }
    else if ($.trim(lineNums[3]) == "R") {
        var fltArray = JSLINQ(CommonResultArray[1])
            .Where(function (item) { return item.AirLineName == lineNums[2].replace('-', ' ');; })
            .Select(function (item) { return item; });
        OB = JSLINQ(fltArray.items)
            .Where(function (item) { return item.LineNumber == lineNums[1]; })
            .Select(function (item) { return item });

        tripsatus = 'R'
    }
    var O = JSLINQ(OB.items)
        .Where(function (item) { return item.Flight == "1"; })
        .Select(function (item) { return item });
    var R = JSLINQ(OB.items)
        .Where(function (item) { return item.Flight == "2"; })
        .Select(function (item) { return item });




    var str1 = '<div>';
    if (O.items.length > 0) {
        str1 += '<div class=""><span style="font-size:20px;display:none; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + this.rel + '_\');" title="Click to close Details"><i class="fa fa-times-circle"></i></span><div></div>';
        str1 += '<table class="w100 f12"><tr><td  class="w50 f16 bld">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
        for (var i = 0; i < O.items.length; i++) {
            str1 += '<tr><td>' + O.items[i].DepartureCityName + '-' + O.items[i].ArrivalCityName + '(' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + ')</td>'
            str1 += '<td>' + RHandler.BagInfo(O.items[i].BagInfo) + '</td></tr>';
        }
        str1 += '</table></div>';
    }
    if (R.items.length > 0) {
        str1 += '<div class="depcity1">';
        str1 += '<table class="w100 f12">';
        for (var j = 0; j < R.items.length; j++) {
            str1 += '<tr><td class="w50">' + R.items[j].DepartureCityName + '-' + R.items[j].ArrivalCityName + '(' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + ')</td>'
            str1 += '<td>' + RHandler.BagInfo(R.items[j].BagInfo) + '</td></tr>';
        }
        str1 += '</table></div>';
    }
    str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
    str1 += '<div class="clear1"></div>';
    //str1 += ' <div class"rgt" onclick="Close(\'' + this.rel + '_\');" >X</div
    $('#' + id).html(str1);
    //$('#' + this.rel + '_').show();
    //$('#' + this.rel + '_').slideToggle();
    // $('#' + this.id).toggleClass("fltDetailslink1");
    // $('#' + this).hide();
    // }
};
ChangedFarePopupShow = function (originalFare, updatedFare, TrackId, type, netFare, trip) {


    if (type == 'show') {
        var diff = '';
        if (parseFloat(updatedFare) > parseFloat(originalFare)) {
            diff = '(+) <img src="../Images/rsp.png" style="height: 10px;" /> ' + (parseFloat(updatedFare) - parseFloat(originalFare)) + '</div> '

        }
        else {
            diff = '(-) <img src="../Images/rsp.png" style="height: 10px;" /> ' + (parseFloat(originalFare) - parseFloat(updatedFare)) + '</div> '
        }

        var str = '<div class="f14 bld colorp">Oops! your selected flight fare has been changed';
        str += '</div><hr /> <div class="clear1"> </div><div class="w95 padding1 mauto bgpp f16">Updated fare is';
        str += ' <img src="../Images/rs.png" style="height: 12px; position:relative; top:1px;" /><span class="bld" id="spnupfare">' + updatedFare + '</span><span style="position:absolute; background:#f9f9f9; padding:5px; box-shadow:1px 2px 4px #888; margin-top:-5px;margin-left:10px;display:none;" id="divnetfare">NetFare: <img src="../Images/rs.png" style="height: 12px; position:relative; top:1px;" />' + netFare + '</span>';
        str += '</div><div class="clear1"></div><div class="clear1"></div><div class="bld w90 mauto">';
        str += ' <div class="w33 lft"> <div> Updated Fare</div> <div class="f14"> <img src="../Images/rs.png" style="height: 10px;" /> ' + updatedFare + '</div></div>';
        str += ' <div class="w33 lft"><div> Previous Fare</div><div class="f14"><img src="../Images/rs.png" style="height: 10px;" /> ' + originalFare + '</div>';
        str += '</div><div class="w33 lft colorp"><div> Fare Difference</div> <div class="f14">';
        str += diff + '</div> </div></div> <div class="clear1">';
        str += '</div><div class="clear1"> </div><div class="w95 rgt"> <ul><span class="bld">Reasons:</span>';
        str += '   <li>Airfares are dynamic and subject to change. This change is beyond our control.</li>';
        str += ' <li>The updated fare is the cheapest fare currently available on your selected flight.</li>';
        str += '</ul> </div> <div class="clear1"> </div><div class="textaligncenter">';
        str += '<input type="button" id="Cancelflt" name="Choose another flight" value="Choose another flight" class="button1" />';
        str += ' <input type="button" id="ContinueCflt" name="Continue" value="Continue" class="button1" style="margin-right:2px;" />';
        str += ' </div> <div class="clear1"></div>'
        $('#divLoadcf').html('');
        $('#divLoadcf').html(str);
        $('#divLoadcf').show();
        $('#Cancelflt').click(function () {
            $('#ConfmingFlight').hide();

        });
        $('#ContinueCflt').click(function () {
            debugger;
            if (TrackId == "") {
                $('#ConfmingFlight').hide();
            } else {

                if ($.trim(trip).toUpperCase() == "I") {
                    window.location = UrlBase + "International/PaxDetails.aspx?" + TrackId + ",I";
                }
                else {
                    window.location = UrlBase + "Domestic/PaxDetails.aspx?" + TrackId;
                }
            }

        });


        $('#spnupfare').mouseover(function () {
            $('#divnetfare').show();

        });


        $('#spnupfare').mouseout(function () {
            $('#divnetfare').hide();

        });
    }
    else if (type == 'hide') {


        var str1 = '';

        //str1 += ' <div class="clear1"> </div> ';
        //str1 += '<div class="w30 auto"  style="text-align: center; "><img alt="loading" width="50px" src="../images/loadingAnim.gif"/> </div> ';
        str1 += '<svg xmlns="http://www.w3.org/2000/svg" version="1.1">';
        str1 += '        <defs>';
        str1 += '            <filter id="gooey">';
        str1 += '                <feGaussianBlur in="SourceGraphic" stdDeviation="10" result="blur"></feGaussianBlur>';
        str1 += '                <feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 18 -7" result="goo"></feColorMatrix>';
        str1 += '                <feBlend in="SourceGraphic" in2="goo"></feBlend>';
        str1 += '            </filter>';
        str1 += '        </defs>';
        str1 += '</svg>';


        str1 += '<div class="loading" >';
        str1 += '   <p style="position: absolute;left: 0;bottom: 220px;text-align: center;width: 100%;color:#fff;">Please wait</p>';
        str1 += '   <span><i></i><i></i></span>';
        str1 += '</div>';

        $('#divLoadcf').html('');
        $('#divLoadcf').html(str1);
        $('#divLoadcf').show();
    }
    $("#ConfmingFlight").show();


};



function SelectedSector(evt, sctor) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(sctor).style.display = "block";
    evt.currentTarget.className += " active";
}

$(".abdd div").click(function (event) {

    event.preventDefault();
    {
        $(this).next().toggle('fast');
        $(this).children('i:last-child').toggleClass('fa-caret-down fa-caret-up');
    }
});

function MakeTabNrmAndSrf() {
    $('#RTFSAirMain').show();
    $('#RTFSAir').show();

    $('#splLoading').hide();
    if (RTFFirstTime == 0) {
        $('#RTFSAir').html('<div id="DivNormalfare" onclick="RTFResultShowHide(\'DivNormalfare\');" class="curve rtfpanel1 bld w15">Normal Fare</div>&nbsp;&nbsp;<div id="DivTabRTF" onclick="RTFResultShowHide(\'DivTabRTF\');" class="curve rtfpanel bld w15">SRF Fare</div>');
        RTFFirstTime = 1;
    }
    try {

    }
    catch (error) {
    }

}

function RTFResultShowHide(id) {
    BlockUI();
    // $('#divResult').html("");
    // $('#divFrom').html("");    
    try {

        $('.rtfpanel1').addClass('rtfpanel');
        //$('*').removeClass('rtfpanel1');
        // $('.curve').addClass('rtfpanel');
        $('.curve').removeClass('rtfpanel1');
        $('#' + $.trim(id)).addClass("rtfpanel1");
        setTimeout(function () {
            if ($.trim(id) == "DivNormalfare") {
                $('.nrmResult').show();
                $('.srtfResult').hide();
                //$('#fltselct').hide();
                //$('#fltbk').hide();
                //$('#fltgo').hide();
                //$('#fltbtn').hide();


            }
            else {
                $('.nrmResult').hide();
                $('.srtfResult').hide();
                $('.' + id).show();
                //$('#fltselct').hide();
                //$('#fltbk').hide();
                //$('#fltgo').hide();
                //$('#fltbtn').hide();
            }
            $.unblockUI();

        }, 100);
    }
    catch (error) {
        $.unblockUI();
    }
}
function BlockUI() {
    $.blockUI({
        message: $("#waitMessage")
    });
}

function GetUniqueAirlineRtf(result, st, svcName) {
    $('#RTFSAirMain').show();
    $('#RTFSAir').show();

    $('#splLoading').hide();
    if (RTFFirstTime == 0) {
        $('#RTFSAir').html('<div id="DivNormalfare" onclick="RTFResultShowHide(\'DivNormalfare\');" class="curve rtfpanel1 bld w15">Normal Fare</div>');
        RTFFirstTime = 1;
    }
    try {
        //RTFResultShowHide
        //var str = "<div  id='" + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + "' class='curve rtfpanel'  onclick='RTFResultShowHide(\"" + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + "\");' ><img src='" + getAirImagePath(result.replace(/\s/g, '').split('_')[0]) + "' title='" + result.split('_')[0] + "' class='w29 lft' alt='" + result.split('_')[0] + "' /><span class='lft f16'> " + getMinPriceRTF(gdsJsonRTF, result.replace(/\s/g, '').split('_')[0] + "_" + svcName) + "</span></div>";
        var str = "<div  id='" + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + "' class='curve rtfpanel'  onclick='RTFResultShowHide(\"" + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + "\");' ><img src='" + getAirImagePath(result.replace(/\s/g, '').split('_')[0]) + "' title='" + result.split('_')[0] + "' class='w29 lft' alt='" + result.split('_')[0] + "' /></div>";

        $('#RTFSAir').append(str);
        // RTF RESULT BIND -DEVESH
        //var divSRTF = '<div id="divResultRTF_' + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + '" class="listRTF list' + result.replace(/\s/g, '').split('_')[0] + "_" + svcName + '" style="display:none;">';
        // $('#mainDiv').append(divSRTF);           
        // RTF RESULT BIND -DEVESH
    }
    catch (error) {
    }
    //if ($('.curve').length > 1) {
    //    GetRTFResult(airlineNameRTFAll, $('.curve')[1].id);
    //}
}



function getMinPriceRTF(ViewResult, airName) {
    var totData = "";
    var airlineNameRTF1 = "";


    var fltArray = JSLINQ(ViewResult)
        .Where(function (item) { return $.trim(item.fltName).replace(/\s/g, '') == airName.split('_')[1]; })
        .Select(function (item) { return item.fltJson; });

    //debugger;
    var fltArrayPrice = JSLINQ(fltArray.items[0])
        .Select(function (item) { return parseFloat(parseFloat(item.AdtFare) * parseFloat(item.Adult) + parseFloat(item.ChdFare) * parseFloat(item.Child) + parseFloat(item.InfFare) * parseFloat(item.Infant)); });


    var arr = new Array();
    arr = fltArrayPrice.items;
    var sorted = arr.slice(0).sort(function (a, b) {
        return a - b;
    });
    var minLnNO = JSLINQ(fltArray.items[0])
        .Where(function (item) { return parseFloat(parseFloat(item.AdtFare) * parseFloat(item.Adult) + parseFloat(item.ChdFare) * parseFloat(item.Child) + parseFloat(item.InfFare) * parseFloat(item.Infant)) == sorted[0]; })
        .Select(function (item) { return item.LineNumber; });

    var maxLnNO = JSLINQ(fltArray.items[0])
        .Where(function (item) { return parseFloat(parseFloat(item.AdtFare) * parseFloat(item.Adult) + parseFloat(item.ChdFare) * parseFloat(item.Child) + parseFloat(item.InfFare) * parseFloat(item.Infant)) == sorted[sorted.length - 1]; })
        .Select(function (item) { return item.LineNumber; });

    if (sorted.length > 0) {
        airlineNameRTFAll.push({ id: airName.split('_')[1], html: '', minprice: parseFloat(sorted[0]), maxprice: parseFloat(sorted[sorted.length - 1]), minLn: minLnNO.items[0], maxLn: maxLnNO.items[0] });
        //return $.trim(sorted[0]);
        $('#RTFSAirMain').show();
        // $('#RTFSAirMain').hide()
        return sorted[0];
    }
    else {
        return "0";
    }
}

function getAirImagePath(AirName) {

    var aircode;
    if ($.trim(AirName).toLocaleLowerCase() == $.trim('GoAir').toLocaleLowerCase()) {

        aircode = "G8";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('JetAirways').toLocaleLowerCase()) {

        aircode = "9W";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('AirIndia').toLocaleLowerCase()) {

        aircode = "AI";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('Vistara').toLocaleLowerCase()) {

        aircode = "UK";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('Jetlite').toLocaleLowerCase()) {

        aircode = "S2";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('SpiceJet').toLocaleLowerCase()) {

        aircode = "SG";
    }
    else if ($.trim(AirName).toLocaleLowerCase() == $.trim('indigo').toLocaleLowerCase()) {

        aircode = "6E";
    }
    return "../Airlogo/sm" + aircode + ".gif";

    //return "http://RWT.co/AirLogo/sm" + aircode + ".gif";    
    //return UrlBase + "../AirLogo/sm" + aircode + ".gif";


}


function SRFPriceItinReq(fltReturnArraySRF) {

    var ArraySRF = new Array();
    // BlockUI();
    try {

        var arr = new Array(fltReturnArraySRF.items);
        var compressedData = (JSON.stringify(fltReturnArraySRF.items));
        // var compressedData = LZString.compressToUTF16(JSON.stringify(fltReturnArraySRF.items));
        var n = UrlBase + "FLTSearch1.asmx/SRFPriceItinReq";
        $.ajax({
            url: n,
            type: "POST",

            data: JSON.stringify({
                AirArray: compressedData,
                Trip: fltReturnArraySRF.items[0].Trip
            }),
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (e) {
                ArraySRF.push(e);
                // $.unblockUI();
            },
            error: function (e, t, n) {
                //alert(t)
                // $.unblockUI();
            }
        })
    }
    catch (error) {
        // $.unblockUI();
    }
    return ArraySRF;
}
function ShowHideDiscount(id) {
    if ($.trim(id) == "show") {
        $('.spnDiscountShowHide').show();
        $('.spnBtnHide').show();
        $('.spnBtnShow').hide();
    }
    else {
        $('.spnDiscountShowHide').hide();
        $('.spnBtnHide').hide();
        $('.spnBtnShow').show();
    }
}

function tabs() {
    var tab = '';


}


function DisplayMultipleFares(list, lnNoArray, secNum, fltName, IsRTrip, IsRTF, Index, tripF, mainLineNo, rint) {

    var res = '';
    var faredet = '';
    var IntRfaredet = '';
    var arry = new Array(4);
    var lnn = lnNoArray.unique();
    var sector;
    var finalelasesec = false;

    if (IsRTrip == true) {

        if (tripF == "O") {
            res += '<div class="">';
        }
        else { res += '<div class="" style="">'; }
    }

    else {
        res += '<div class="" style="">';
        res += '<div class="col-md-12">';
        res += '<div class="row">';
    }

    //let totalpricecount = lnNoArray.length;
    let div_md_9 = 1;

    for (var i = 0; i < lnn.length; i++) {

        var OF = JSLINQ(list.items)
            .Where(function (item) { return item.LineNumber == lnn[i]; })
            .Select(function (item) { return item });
        sector = $.trim(OF.items[0].Sector).replace(':', '-');
        if (IsRTrip == true) {
            if (tripF == "O") {

                //res += '<label class="radio inline" style="font-size: 17px;margin-bottom: 3px;"><input type="radio" name="O" class="' + mainLineNo + '" title="' + mainLineNo + '_R" value="' + OF.items[0].LineNumber + '"/>₹ ' + OF.items[0].TotalFare + '</label><sapn style="line-height: 0px;font-size: 8px;text-transform:uppercase;color: #929292;">' + OF.items[0].DisplayFareType + '</span>';
                //res += '<a href="#" data-toggle="modal" data-target="#' + OF.items[0].LineNumber + i + '_radio"><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 15px;margin-top: 6px !important;"></i></a>'

                res += '<div  class="disp_o0" style="display:block;">';
                res += '<div class="pro DG_0">';
                res += '<div class="rw">';
                res += '<div class="r"><input type="radio" name="O" class="' + mainLineNo + '" title="' + mainLineNo + '_R" value="' + OF.items[0].LineNumber + '" /></div>';
                res += '</div>';
                res += '<a class="fd gridViewToolTip" title="View Flight Details" data-toggle="modal" data-target="#' + OF.items[0].LineNumber + i + '_radio" id="' + OF.items[0].LineNumber + '_O" ><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 15px;margin-top: 6px !important;"></i></a>';
                //res += '<a href="#" data-toggle="modal"  data-target="#' + OF.items[0].LineNumber + i + '_radio" id="' + OF.items[0].LineNumber + '_ALL" class="gridViewToolTip"><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 15px;margin-top: 6px !important;"></i></a>' 
                res += '<div class="pw">';
                res += '<div class="p" data-container=".sc-result.air .a_sr_wrap" title="" >₹ ' + OF.items[0].TotalFare + '</div>';

                res += '</div>';
                res += '<div class="nw">';
                res += '<div class="dg" title="Published Fare">' + OF.items[0].DisplayFareType + '</div>';
                res += '<div class="indi">';
                var rrfnd = 'Not-Refundable';
                var rnr = '';
                if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0))) {
                    //rnr = '<span>Refundable</span>';
                    rrfnd = 'Refundable';
                }
                else {
                    //var rnr = '<span>Non-Refundable</span>';
                    rrfnd = 'Not-Refundable';
                    if ($.trim(OF.items[0].AdtFareType).toLowerCase() == "refundable") {
                        //rnr = '<span>Refundable</span>';
                        rrfnd = 'Refundable';
                    }
                }


                res += '<div class="a_rf" title="Click for Rules">' + rrfnd + '</div>';
                //res += '<div class="a_nb">Free Meal</div>';
                res += '<div class="a_nb spnDiscountShowHide" style="display:none;">Net-: ₹ ' + OF.items[0].NetFare + ' </div>';
                res += '<div class="a_nb spnDiscountShowHide" style="display:none;">Inv-:' + OF.items[0].TotDis + ' </div>';
                res += ' </div>';
                res += '</div>';
                res += '</div>';
                res += '</div>';






                res += '<div class="AirlineFareType hide">' + OF.items[0].AdtFar + '</div>'


               
            }
            else if (tripF == "R") {

                res += '<div class="AirlineFareType hide">' + OF.items[0].AdtFar + '</div>';


                res += '<div  class="disp_o0" style="display:block;">';
                res += '<div class="pro DG_0">';
                res += '<div class="rw">';
                res += '<div class="r"><input type="radio" name="R" class="' + mainLineNo + '" title="' + mainLineNo + '_R" value="' + OF.items[0].LineNumber + '" /></div>';
                res += '</div>';
                res += '<a href="#" data-toggle="modal" data-target="#' + OF.items[0].LineNumber + i + 'R_radio" id="' + OF.items[0].LineNumber + '_R" class="fd gridViewToolTip" title="View Flight Details"><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 15px;margin-top: 6px !important;"></i></a>';

                res += '<div class="pw">';
                res += '<div class="p" data-container=".sc-result.air .a_sr_wrap" title="" >₹ ' + OF.items[0].TotalFare + '</div>';

                res += '</div>';
                res += '<div class="nw">';
                res += '<div class="dg" title="Published Fare">' + OF.items[0].DisplayFareType + '</div>';
                res += '<div class="indi">';

                var rrfnd = 'Not-Refundable';
                var rnr = '';
                if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0))) {
                    //rnr = '<span>Refundable</span>';
                    rrfnd = 'Refundable';
                }
                else {
                    //var rnr = '<span>Non-Refundable</span>';
                    rrfnd = 'Not-Refundable';
                    if ($.trim(OF.items[0].AdtFareType).toLowerCase() == "refundable") {
                        //rnr = '<span>Refundable</span>';
                        rrfnd = 'Refundable';
                    }
                }


                res += '<div class="a_rf" title="Click for Rules">' + rrfnd + '</div>';
                //res += '<div class="a_nb">Free Meal</div>';
                res += '<div class="a_nb spnDiscountShowHide" style="display:none;">Net-: ₹ ' + OF.items[0].NetFare + '</div>';
                res += '<div class="a_nb spnDiscountShowHide" style="display:none;">Inv-:' + OF.items[0].TotDis + '</div>';
                res += ' </div>';
                res += '</div>';
                res += '</div>';
                res += '</div>';



            }
        }
        else {
            finalelasesec = true;

            if (div_md_9 == 1) {
                res += '<div class="col-md-12 chk-fare">';
            }

            res += '<div class="AirlineFareType hide">' + OF.items[0].AdtFar + '</div>';
            res += '<div class="row">';
            res += '<div class="col-md-10">';
            res += '<label style="font-size: 13px; 400 !important; color: #e20000;">';
            res += '<input type="radio" name="radioPrice" title="' + mainLineNo + '" id="' + OF.items[0].LineNumber + '_radio"  value="' + OF.items[0].LineNumber + '">';

            res += ' ₹ ' + Math.ceil(OF.items[0].TotalFare) + '&nbsp;&nbsp;<span style="font-size: 11px; font-weight: 600 !important;color: #313131;">' + OF.items[0].DisplayFareType + '</span>';

            res += '</label>';
            res += '</div>';

            res += '<div class="col-md-1 flt-det">';
            res += '<a href="#" data-toggle="modal"  data-target="#' + OF.items[0].LineNumber + i + '_radio" id="' + OF.items[0].LineNumber + '_ALL" class="gridViewToolTip"><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 15px;margin-top: 6px !important;"></i></a>'
            res += '</div>';

            res += '</div>';

            var rrfnd = 'Not-Refundable';
            var rnr = '';
            if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0))) {
                //rnr = '<span>Refundable</span>';
                rrfnd = 'Refundable';
            }
            else {
                //var rnr = '<span>Non-Refundable</span>';
                rrfnd = 'Not-Refundable';
                if ($.trim(OF.items[0].AdtFareType).toLowerCase() == "refundable") {
                    //rnr = '<span>Refundable</span>';
                    rrfnd = 'Refundable';
                }
            }
            res += '<div class="rfnd">' + rrfnd + '</div>';
            res += '<div class="spnDiscountShowHide rfnd1" style="display:none;">Net ₹ ' + OF.items[0].NetFare + ' </div>';

            res += '<div class="spnDiscountShowHide rfnd1" style="display:none;">Inv ₹' + OF.items[0].TotDis + '</div>';
        }

        div_md_9 = div_md_9 + 1;


        //if (div_md_9 == 1) {
        //    lastdiv= '</div>';
        //}
        //else {
        //    if (div_md_9 == (totalpricecount + 1)) {               
        //        lastdiv='</div>';
        //    }
        //}
    }
    //let lastdiv = "";
    //if (div_md_9 == (totalpricecount + 1)) {
    //    lastdiv = '</div>';
    //}

    //res = res + lastdiv;

    res += '</div><div class="col-md-3 book-btn" style="position: relative;display:none;">';
    for (var i = 0; i < lnn.length; i++) {

        var OF = JSLINQ(list.items)
            .Where(function (item) { return item.LineNumber == lnn[i]; })
            .Select(function (item) { return item });
        sector = $.trim(OF.items[0].Sector).replace(':', '-');



        if (IsRTrip == false) {
            if (i == 0) {
                res += '<div class=""><input type="button"  value="Book" class="falsebookbutton ' + mainLineNo + '" id="' + mainLineNo + '_falsebookbutton" style=""/></div>';
            }
            res += '<div class=""><input type="button"  value="→"  class="buttonfltbk" style="display: none;" title="' + OF.items[0].LineNumber + '"  id="' + OF.items[0].LineNumber + '" style="font-size: 37px;"/></div>';
        }


        if (IsRTrip == true) {
            ////Fare Details 12-02-2020 Outbound

            debugger;
            if (tripF == "O") {
                var rnr = '';



                faredet += ' <div class="modal fade" id="' + OF.items[0].LineNumber + i + '_radio" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">';
                faredet += ' <div class="modal-dialog" role="document">';
                faredet += '<div class="modal-content">';
                faredet += ' <div class="modal-header">';
                faredet += ' <h5 class="modal-title" id="exampleModalLabel">Flight Details</h5>';
                faredet += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">';
                faredet += '<span aria-hidden="true">&times;</span>';
                faredet += '</button>';
                faredet += ' </div>';
                faredet += '<div class="modal-body">';

                faredet += '<div class="tabs" >';


                faredet += '<div  value="' + mainLineNo + '" class="fare-groups-wrap ' + mainLineNo + '_faredetailmasterall" id="' + OF.items[0].LineNumber + '_faredetailmaster" style="margin-bottom:40px;">';
                faredet += '<div class="gridViewToolTip1 lft"  title="' + OF.items[0].LineNumber + '_O" ></div>';
                faredet += '<ul class="fare-groups nav navbar-nav" role="tablist" style="border-bottom:1px solid #CCC;">';
                faredet += '<li class="fgf sel nav-item active">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_Fare" class="d div_cls gridViewToolTipSRF nav-link collapsible" data-toggle="tab" role="tab" style="padding:0px;" id="' + OF.items[0].LineNumber + '_O" title="' + mainLineNo + '" rel="' + mainLineNo + '">Fare Details</a>';
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_Obdfltdt" class="d div_cls fltDetailslinkR nav-link collapsible" data-toggle="tab" role="tab" id="' + OF.items[0].LineNumber + '_O" title="' + mainLineNo + '" rel="' + mainLineNo + '_O" style="padding:0px;" >Flight Details</a>';

                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_Obdbag" class="d div_cls fltBagDetailsR nav-link collapsible" data-toggle="tab" role="tab" id="' + OF.items[0].LineNumber + '_O"  title="' + mainLineNo + '" rel="' + mainLineNo + '_O"  style="padding:0px;">Baggage</a>'
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_Obdcanc" class="d div_cls fareRuleToolTip cursorpointer nav-link collapsible" data-toggle="tab" role="tab" rel="FareRule_' + mainLineNo + '_O" title="' + mainLineNo + '" style="padding:0px;" >Cancellation</a><div class="fade" title="' + mainLineNo + '_O" ></div>'
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<div class="ui_block clearfix"></div>';
                faredet += '</ul>';
                faredet += '</div>';
                faredet += '<div class="tabs-stage tab-content">';//a
                faredet += '<div class="depcity tab-pane active" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Fare" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Obdfltdt" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Obdbag" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Obdcanc" style="margin-top:-11px;"></div>';
                faredet += '</div>';//a
                faredet += '</div>';//b
                faredet += '</div>';//c
                faredet += '</div>';//e
                faredet += '</div>';//f
                faredet += '</div>';//g






                ////Fare Details 12-02-2020 Outbound
            }
            else {
                ////Fare Details 12-02-2020 Inbound
                var rnr = '';



                faredet += ' <div class="modal fade" id="' + OF.items[0].LineNumber + i + 'R_radio" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">';
                faredet += ' <div class="modal-dialog" role="document">';
                faredet += '<div class="modal-content">';
                faredet += ' <div class="modal-header">';
                faredet += ' <h5 class="modal-title" id="exampleModalLabel">Flight Details</h5>';
                faredet += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">';
                faredet += '<span aria-hidden="true">&times;</span>';
                faredet += '</button>';
                faredet += ' </div>';
                faredet += '<div class="modal-body">';

                faredet += '<div class="tabs" >';


                faredet += '<div  value="' + mainLineNo + '" class="fare-groups-wrap ' + mainLineNo + '_faredetailmasterall " id="' + OF.items[0].LineNumber + '_faredetailmaster" style="margin-bottom:40px;">';
                faredet += '<div class="gridViewToolTip1 lft"  title="' + OF.items[0].LineNumber + '_R" ></div>';
                faredet += '<ul class="fare-groups nav navbar-nav" role="tablist" style="border-bottom:1px solid #CCC;">';
                faredet += '<li class="fgf sel nav-item active">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_Fare" class="d div_cls gridViewToolTipSRF nav-link collapsible" data-toggle="tab" role="tab" style="padding:0px;" id="' + OF.items[0].LineNumber + '_R" title="' + mainLineNo + '" rel="' + mainLineNo + '">Fare Details</a>';
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_RO" class="d div_cls fltDetailslinkR nav-link collapsible" data-toggle="tab" role="tab" id="' + OF.items[0].LineNumber + '_R" title="' + mainLineNo + '" rel="' + mainLineNo + '_R" style="padding:0px;" >Flight Details</a>';

                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_bag" class="d div_cls fltBagDetailsR nav-link collapsible" data-toggle="tab " role="tab" id="' + OF.items[0].LineNumber + '_R"  title="' + mainLineNo + '" rel="' + mainLineNo + '_R"  style="padding:0px;">Baggage</a>'
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<li class="fgf nav-item">';
                faredet += '<a href="#' + OF.items[0].LineNumber + '_canc" class="d div_cls fareRuleToolTip cursorpointer nav-link collapsible" data-toggle="tab" role="tab" rel="FareRule_' + mainLineNo + '_R" title="' + mainLineNo + '" style="padding:0px;" >Cancellation</a><div class="fade" title="' + mainLineNo + '_R" ></div>'
                faredet += '<div class="p"></div>';
                faredet += '</li>';
                faredet += '<div class="ui_block clearfix"></div>';
                faredet += '</ul>';
                faredet += '</div>';
                faredet += '<div class="tabs-stage tab-content">';//a
                faredet += '<div class="depcity tab-pane active" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Fare" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_RO" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_bag" style="margin-top:-11px;"></div>';
                faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_canc" style="margin-top:-11px;"></div>';
                faredet += '</div>';//a
                faredet += '</div>';//b
                faredet += '</div>';//c
                faredet += '</div>';//e
                faredet += '</div>';//f
                faredet += '</div>';//g



                ////Fare Details 12-02-2020 Inbound
            }

            ////Fare Details 12-02-2020
        }

        ////Fare Details 12-02-2020
        else {



            faredet += ' <div class="modal fade" id="' + OF.items[0].LineNumber + i + '_radio" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">';
            faredet += ' <div class="modal-dialog" role="document">';
            faredet += '<div class="modal-content">';
            faredet += ' <div class="modal-header">';
            faredet += ' <h5 class="modal-title" id="exampleModalLabel">Flight Details</h5>';
            faredet += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">';
            faredet += '<span aria-hidden="true">&times;</span>';
            faredet += '</button>';
            faredet += ' </div>';
            faredet += '<div class="modal-body">';

            faredet += '<div class="tabs" >';


            faredet += '<div  value="' + mainLineNo + '" class="fare-groups-wrap ' + mainLineNo + '_faredetailmasterall " id="' + OF.items[0].LineNumber + '_faredetailmaster" style="margin-bottom:40px;">';
            faredet += '<div class="gridViewToolTip1 lft"  title="' + OF.items[0].LineNumber + '_O" ></div>';
            faredet += '<ul class="fare-groups nav navbar-nav" role="tablist" style="border-bottom:1px solid #CCC;">';
            faredet += '<li class="fgf sel nav-item active"  id="' + OF.items[0].LineNumber + '_Allll" >';
            faredet += '<a href="#' + OF.items[0].LineNumber + '_Fare" data-toggle="tab" role="tab" class="div_cls d collapsible gridViewToolTip nav-link active"  style="padding:0px;" id="' + OF.items[0].LineNumber + '_flt" title="' + mainLineNo + '" rel="' + mainLineNo + '">Fare Details</a>';
            faredet += '<div class="p"></div>';
            faredet += '</li>';
            faredet += '<li class="fgf nav-item">';
            faredet += '<a href="#' + OF.items[0].LineNumber + '_fltdt" data-toggle="tab" role="tab"  class="div_cls d collapsible fltDetailslink nav-link"  id="' + OF.items[0].LineNumber + 'Det" title="' + mainLineNo + '" rel="' + mainLineNo + '" style="padding:0px;" >Flight Details</a>';
            faredet += '<div class="p"></div>';
            faredet += '</li>';
            faredet += '<li class="fgf nav-item">';
            faredet += '<a href="#' + OF.items[0].LineNumber + '_bag" data-toggle="tab" role="tab" class="div_cls d collapsible fltBagDetails nav-link"  id="' + OF.items[0].LineNumber + 'BagDet"  title="' + mainLineNo + '" rel="' + mainLineNo + '" style="padding:0px;">Baggage</a>';
            faredet += '<div class="p"></div>';
            faredet += '</li>';
            faredet += '<li  class="fgf nav-item">';
            faredet += '<a href="#' + OF.items[0].LineNumber + '_canc" data-toggle="tab" role="tab"  class="div_cls d collapsible fareRuleToolTip cursorpointer nav-link" rel="FareRule_' + mainLineNo + '_O" title="' + mainLineNo + '" style="padding:0px;" >Cancellation</a><div class="fade" title="' + mainLineNo + '_O" ></div>';

            faredet += '<div class="p"></div>';
            faredet += '</li>';
            faredet += '<div class="ui_block clearfix"></div>';
            faredet += '</ul>';
            faredet += '</div>';
            faredet += '<div class="tabs-stage tab-content">';//a
            faredet += '<div class="depcity tab-pane active" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_Fare" style="margin-top:-11px;"></div>';
            faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_fltdt" style="margin-top:-11px;"></div>';
            faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_bag" style="margin-top:-11px;"></div>';
            faredet += '<div class="depcity tab-pane" role="tabpanel" id="' + OF.items[0].LineNumber.toString() + '_canc" style="margin-top:-11px;"></div>';
            faredet += '</div>';//a
            faredet += '</div>';//b
            faredet += '</div>';//c
            faredet += '</div>';//e
            faredet += '</div>';//f
            faredet += '</div>';//g



            ////Fare Details 12-02-2020
        }

    }


    res += '</div>';
    res += '</div>';
    if (finalelasesec == true) {

        res += '</div>';
        res += '</div>';
    }
    res += '<div class="row">';
    if (rint == 0) {
        res += faredet;
    }
    else {
        IntRfaredet = faredet;
    }

    res += '</div>';
    return [res, IntRfaredet];
}

function SelectFareAlert() {
    alert("Please select Fare !!");
}

function show_sidebar(xt) {
    var ty = xt.id.split("_")[0];
    document.getElementById('' + ty + '_sidebar').style.visibility = "visible";
}

function hide_sidebar(yt) {
    var zy = yt.id.split("_")[0];
    document.getElementById('' + zy + '_sidebar').style.visibility = "hidden";
}

//$(document).on("click", ".div_cls", function (e) {
//    var hhhrr = $(this).attr("href");

//    var hhtttt = $(hhhrr).attr("style").indexOf("none");
//    var hhttttR = $(hhhrr).attr("style").indexOf("block");
//    if (hhtttt == -1 || hhttttR == 1) {
//        //$(hhhrr).addClass("active in");
//        $(hhhrr).attr('style', 'display:none');
//    }
//    else {
//        $(hhhrr).attr('style', 'display:block');
//        //$(hhhrr).removeClass("active");
//    }

//});
//$(document).on("click", ".div_close", function (e) {
//    var hhrr = $(this).attr("href");

//    var hhtt = $(hhrr).attr("class").indexOf("active");
//    if (hhtt == -1) {
//        $(hhrr).addClass("active in");
//    }
//    else {
//        $(hhrr).removeClass("active in");
//    }

//});



//$(document).on("click", ".div_cls", function (e) {
//    debugger;
//    var hhhrr = $(this).attr("href");

//    var hhtttt = $(hhhrr).attr("class").indexOf("active");
//    if (hhtttt == -1) {
//        $(hhhrr).addClass("active in");
//    }
//    else {
//        $(hhhrr).removeClass("active in");
//    }

//});

function setfare(idsc, rel) {
    event.preventDefault();
    var th = this;


    var lineNum = idsc.split('_');
    //$('#' + this.rel + '_').slideUp();
    var lineup = rel;



    var fltSelectedArray;
    if (lineNum[1] == "R") {
        fltSelectedArray = JSLINQ(result[1])
            .Where(function (item) { return item.LineNumber == lineNum[0]; })
            .Select(function (item) { return item });
    }
    else {

        fltSelectedArray = JSLINQ(result[0])
            .Where(function (item) { return item.LineNumber == lineNum[0]; })
            .Select(function (item) { return item });
    }

    var compressedData = (JSON.stringify(fltSelectedArray.items));
    //var compressedData = LZString.compressToUTF16(JSON.stringify(fltSelectedArray.items));
    //if (fltSelectedArray.items[0].Trip == "I" ) {
    var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL1";
    $.ajax({
        url: n,
        type: "POST",
        //data: JSON.stringify({
        //    AirArray: fltSelectedArray.items,
        //    Trip: fltSelectedArray.items[0].Trip
        //}),
        data: JSON.stringify({
            AirArray: compressedData,
            Trip: fltSelectedArray.items[0].Trip
        }),
        dataType: "json",
        type: "POST",
        //async: false,
        contentType: "application/json; charset=utf-8",
        success: function (e) {

            $('#' + lineup + '_Fare').html(t.CreateFareBreakUp(e.d[0]));
            $('#SomeModal').on('show.bs.modal', function (e) {
                // do something...
            })

        },
        error: function (e, t, n) {
            //alert(t)
        }
    })
}