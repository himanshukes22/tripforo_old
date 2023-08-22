var RHandler;
var resultG = new Array();
var Obook = null;
var Rbook = null;
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



var ORTFFare = 0;
var RRTFFare = 0;
var ORTFLineNo = '';
var RRTFLineNo = '';
var ORTFVC = '';
var RRTFVC = '';
var roundtripNrml1 = false;


var applyfilterStatus = false;

$(document).ready(function () {
    RHandler = new ResHelper;
    RHandler.BindEvents()
});

$(document).ready(function () {
    $("#ModifySearch").click(function () {
        $("#Modsearch").slideDown();
    });
    $("#mdclose").click(function () {
        $("#Modsearch").slideUp();
    });
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

        if (arrDate.getMonth() < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }
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
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };


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

        if (arrDate.getMonth() < 10) {
            arrM = "0" + arrM;
        }
        else {
            arrM = arrM;
        }

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
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };

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

        if (depdate.getMonth() < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }


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

        if (depdate.getMonth() < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }


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
            ////window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString;
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
                        alert(t)
                        window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
            }
        }
        else if (Trip == "I") {
            ////window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString;
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
                        alert(t)
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
            ////window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString;
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
                        alert(t)
                        window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
                    }
                });
            }
            catch (err) {
                window.location.href = UrlBase + "Domestic/Result.aspx?" + dataString;
            }
        }
        else if (Trip == "I") {
            ////window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString;
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
                        alert(t)
                        window.location.href = UrlBase + "International/FltResIntl.aspx?" + dataString;
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
    e.bookO.click(function () {

        ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

        $("#searchquery").hide();
        // $("#div_Progress").show();
        //        $.blockUI({
        //            message: $("#waitMessage")
        //        });
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
            var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails";
            $.ajax({
                url: t,
                type: "POST",
                data: JSON.stringify({
                    a: arr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    //InsertedTID = e.d;

                    if (e.d.ChangeFareO.TrackId == "0") {
                        alert("Selected fare has been changed.Please select another flight.");
                        // $("#searchquery").show();
                        // $(document).ajaxStop($.unblockUI);
                        //window.location = UrlBase + "Search.aspx";
                    } else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'I');
                    } else {
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
            var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
            $.ajax({
                url: t,
                type: "POST",
                data: JSON.stringify({
                    a: arr
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    for (var i = 0; i < arr[0].length; i++) {
                        arr[0][i].LineNumber = iscahem;
                    }

                    //InsertedTID = e.d;
                    //if ($.trim(arr[0][0].ProductDetailQualifier) == "CACHE") {

                    if (e.d.ChangeFareO.TrackId == "0") {
                        alert("Selected fare has been changed.Please select another flight.");
                        window.location = UrlBase + "Search.aspx";
                        $(document).ajaxStop($.unblockUI)
                    }
                    else if (parseFloat(e.d.ChangeFareO.CacheTotFare) != parseFloat(e.d.ChangeFareO.NewTotFare)) {
                        ChangedFarePopupShow(e.d.ChangeFareO.CacheTotFare, e.d.ChangeFareO.NewTotFare, e.d.ChangeFareO.TrackId, 'show', e.d.ChangeFareO.NewNetFare, 'D');
                    }
                    else {

                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                    }



                    //}
                    //else {

                    //    if (e.d.ChangeFareO.TrackId == "0") {
                    //        alert("Selected fare has been changed.Please select another flight.");
                    //        $("#searchquery").show();
                    //        //$("#BTNDIV").show();
                    //        // $("#div_Progress").hide();
                    //        $(document).ajaxStop($.unblockUI)
                    //        window.location = UrlBase + "Search.aspx";
                    //    } else {
                    //        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e.d.ChangeFareO.TrackId;
                    //    }
                    //}
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



};


ResHelper.prototype.ShowFareBreakUp = function (result) {

    var t = this;
    this.gridViewToolTip = $(".gridViewToolTip");
    t.gridViewToolTip.mouseover(function (event) {

        var th = this;
        var lineNum = $(th).next().attr('title').split('_');

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

        //if (fltSelectedArray.items[0].Trip == "I" ) {
        var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
        $.ajax({
            url: n,
            type: "POST",
            data: JSON.stringify({
                AirArray: fltSelectedArray.items,
                Trip: fltSelectedArray.items[0].Trip
            }),
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (e) {
                $(th).next().html(t.CreateFareBreakUp(e.d[0]));
                t.gridViewToolTip.tooltip({

                    track: true,
                    delay: 0,
                    showURL: false,
                    fade: 100,
                    bodyHandler: function () {
                        return $($(th).next().html());
                    },
                    showURL: false

                });
            },
            error: function (e, t, n) {
                alert(t)
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

    t.SRFTooltipFare.mouseover(function (event) {

        var th = this;



        if (Obook[0].LineNumber.search('SFM') > 0) {
            /// SPL
            var fltReturnArrayM = JSLINQ(CommanRTFArray)
                   .Where(function (item) { return item.LineNumber == Obook[0].LineNumber; })
                   .Select(function (item) { return item });




            var arr = new Array(fltReturnArrayM.items);

            var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
            $.ajax({
                url: n,
                type: "POST",
                data: JSON.stringify({
                    AirArray: fltReturnArrayM.items,
                    Trip: fltReturnArrayM.items[0].Trip
                }),
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    //$(th).next().html(t.CreateFareBreakUp(e.d[0]));

                    $('#fareBrkup').html(t.CreateFareBreakUp(e.d[0]));
                    t.SRFTooltipFare.tooltip({

                        track: true,
                        delay: 0,
                        showURL: false,
                        fade: 100,
                        bodyHandler: function () {
                            return $('#fareBrkup').html();

                        },
                        showURL: false

                    });
                },
                error: function (e, t, n) {
                    alert(t)
                }
            })



        }
        else {



            var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
            $.ajax({
                url: n,
                type: "POST",
                data: JSON.stringify({
                    AirArray: Obook,
                    Trip: Obook[0].Trip
                }),
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (e) {


                    var bookO = t.CreateFareBreakUp(e.d[0]);

                    $.ajax({
                        url: n,
                        type: "POST",
                        data: JSON.stringify({
                            AirArray: Rbook,
                            Trip: Rbook[0].Trip
                        }),
                        dataType: "json",
                        type: "POST",
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (e) {


                            var bookR = t.CreateFareBreakUp(e.d[0]);

                            var htmlfare = '<div class="large-12 medium-12 small-12 fareres2">';

                            htmlfare += '<div class="large-6 medium-6 small-12 columns"><div><h4>Outbound</h4></div>' + bookO + '</div>'
                            htmlfare += '<div class="large-6 medium-6 small-12 columns"><div><h4>Inbound</h4></div>' + bookR + '</div>'
                            htmlfare += '<div class="clear"></div>';
                            htmlfare += '</div>';


                            $('#fareBrkup').html(htmlfare);

                            t.SRFTooltipFare.tooltip({

                                track: true,
                                delay: 0,
                                showURL: false,
                                fade: 100,
                                bodyHandler: function () {
                                    return $('#fareBrkup').html();

                                },
                                showURL: false

                            });




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
        $('#' + lineNum[0] + '_').show();
        var Divhide = '<div>';
        Divhide += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + lineNum[0] + '_\');" title="Click to close Details">X</span><div></div>';
        $('#' + lineNum[0] + '_').html("Loading.., Please wait.<div align='center'><img alt='loading' width='50px' height='50px' src='../images/loadingAnim.gif'/></div>");
        Divhide += '</div>';
        if (lineNum[0] != null) {
            $('#' + lineNum[0] + '_').slideDown();
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
                            str1 += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + lineNum[0] + '_\');" title="Click to close Details">X</span><div></div>';
                            str1 += '<div>fare rule not available.</div>';
                            str1 += '</div>';
                        }

                        else {
                            str1 += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + lineNum[0] + '_\');" title="Click to close Details">X</span><div></div>';
                            str1 += '<div style="overflow-y:scroll;height:250px;">' + e.d.Response.FareRules[0].FareRuleDetail + '</div>'
                            str1 += '</div>';
                        }
                        $('#' + lineNum[0] + '_').html(str1);
                        // $('#' + lineNum[0] + '_').slideToggle();
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
    if (e.AdtFareType == 'Special Fare')
    { a = (e.ADTAgentMrk * e.Adult) + (e.CHDAgentMrk * e.Child); }
    else
    { a = e.TotMrkUp; }
    var f = e.TotCB;
    var l = e.TotTds;
    var aa = parseInt(a / (e.Adult + e.Child));

    strResult = "<table border='0' cellpadding='0' cellspacing='0' id='FareBreak' class='breakup w100'>";
    strResult = strResult + "<thead><tr><th colspan='5' class='hd'>Fare Summary</th></tr>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<th>&nbsp;</th>";
    strResult = strResult + "<th>Base Fares</th>";
    strResult = strResult + "<th>Fuel Surcharge</th>";
    strResult = strResult + "<th>Other Tax</td>";
    strResult = strResult + "<th>Total Fare</th></tr></thead>";
    strResult = strResult + "<tbody><tr><td class='bld'>ADT</td>";
    strResult = strResult + "<td>" + e.AdtBfare + "</td>";
    strResult = strResult + "<td>" + e.AdtFSur + "</td>";
    strResult = strResult + "<td>" + (n + aa) + "</td>";
    strResult = strResult + "<td>" + (r + aa) + "</td></tr>";
    if (e.Child > 0) {
        strResult = strResult + "<tr><td class='bld'>CHD</td>";
        strResult = strResult + "<td>" + e.ChdBFare + "</td>";
        strResult = strResult + "<td>" + e.ChdFSur + "</td>";
        strResult = strResult + "<td>" + (i + aa) + "</td>";
        strResult = strResult + "<td>" + (s + aa) + "</td></tr>"
    }
    if (e.Infant > 0) {
        strResult = strResult + "<tr><td class='bld'>INF</td>";
        strResult = strResult + "<td>" + e.InfBfare + "</td>";
        strResult = strResult + "<td>" + e.InfFSur + "</td>";
        strResult = strResult + "<td>" + o + "</td>";
        strResult = strResult + "<td>" + u + "</td></tr>"
    }
    strResult = strResult + "<tr>";
    strResult = strResult + "<td><span class='bld'>SrvTax:</span><br />" + e.STax + "</td>";
    if (e.IsCorp == true) {
        strResult = strResult + "<td><span class='bld'>Mgnt. Fee:</span><br />" + e.TotMgtFee + "</td>";
        strResult = strResult + "<td>(" + e.Adult + " ADT<br />" + e.Child + " CHD<br />" + e.Infant + " INF)</td>";
        strResult = strResult + "<td><span class='bld'>Total Fare:</span><br />" + e.TotalFare + "</td>";
        strResult = strResult + "<td><span class='bld'>Net Fare:</span><br />" + e.NetFare + "</td>";
    }
    else {
        strResult = strResult + "<td><span class='bld'>Tran. Fee:</span><br />" + e.TFee + "</td>";
        strResult = strResult + "<td><span class='bld'>Tran. Charge:</span><br />0</td>";//
        strResult = strResult + "<td colspan='2' class='bld cursorpointer'>" + e.Adult + " ADT, " + e.Child + " CHD, " + e.Infant + " INF<br /><span class='f16'>Total Fare: " + e.TotalFare + "</span></td>";
        strResult = strResult + "</tr>";
        strResult = strResult + "<tr>";
        strResult = strResult + "<td><span class='bld'>Commission:</span><br />" + e.TotDis + "</td>";
        strResult = strResult + "<td><span class='bld'>Cash Back:</span><br />" + f + "</td>";
        strResult = strResult + "<td><span class='bld'>TDS:</span><br />" + l + "</td>";
        strResult = strResult + "<td class='bld'>NetFare:<br />" + e.NetFare + "</td><td>&nbsp;</td>";
    }
    strResult = strResult + "</tr>";
    if (pmf != '') {
        strResult = strResult + "<tr><td class='colorp f16 italic' colspan='5'><div class='clear1'></div>" + pmf + " </td></tr>";
    }
    strResult = strResult + "</tbody>";
    strResult = strResult + "</table>";
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
        if ((e.AvailableSeats == "SGNRML") && ((e.RBD == "E") || (e.RBD == "F") || (e.RBD == "H") || (e.RBD == "J") || (e.RBD == "K")) && (parseInt(e.Adult) + parseInt(e.Child) >= 2)) {
            pmf = "Friend and Family special fares (*T&C Apply.) ";
        }
    }
    catch (fberr)
    { }
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
        if (adTime.length == 1)
        { adTime = "000" + adTime; }
        else if (adTime.length == 2)
        { adTime = "00" + adTime; }
        else if (adTime.length == 3)
        { adTime = "0" + adTime; }
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
    var OF = JSLINQ(result)
          .OrderBy(function (item) { return item.AirLineName })
           .Select(function (item) { return item.AirLineName });

    marray = OF.items.unique1();

    var str = '<div class="jplist-group" data-control-type="Airlinefilter' + type + '" data-control-action="filter"  data-control-name="Airlinefilter' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'

    for (var i = 0; i < marray.length; i++) {


        str += '<div class="lft w8"> <input value="' + marray[i] + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft w80" style="padding-top:3px;"><label for="' + marray[i] + '">' + marray[i] + '</label> </div> <div class="clear"> </div>';

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

        str += '<div class="clear"> </div><div class="lft w8"> <input value="' + marray[i] + '"  id="CheckboxS' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft w80" style="padding-top:3px"> <label for="' + marray[i] + '">' + marray[i] + '</label></div>';

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
        if (i == 0)
        { a.push(this[i]); }
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
    if (val.toString().length == 1)
    { val1 = "000" + val.toString(); }
    else if (val.toString().length == 2)
    { val1 = "00" + val.toString(); }
    else if (val.toString().length == 3)
    { val1 = "0" + val.toString(); }
    else
    { val1 = val; }

    return val1;

};

ResHelper.prototype.GetFltDetails = function (result) {
    var e = this;
    $('.fltDetailslink').click(function (event) {

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
            //.Where(function (item) { return item.Flight == "2"; })
            //.Select(function (item) { return item });


            var fta = JSLINQ(OB.items)
                  .Select(function (item) { return item.Flight });

            var ft = Math.max.apply(Math, fta.items);

            var str1 = '<div>';
            if (O.items.length > 0) {

                var totDur = "";

                if (O.items[0].Provider == "TB") {
                    totDur = e.GetTotalDuration(O.items);
                }
                else {
                    totDur = e.MakeupTotDur(O.items[0].TotDur)
                }

                str1 += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-5px; cursor:pointer;" onclick="Close(\'' + this.rel + '_\');" title="Click to close Details">X</span><div class="large-12 medium-12 small-12"><span class="f16">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span>&nbsp;' + totDur + '</div><div class="clear"></div>';
                for (var i = 0; i < O.items.length; i++) {
                    if (i >= 1 && O.items.length > 1 && i < O.items.length) {
                        str1 += e.GetLayOver(O.items, i);
                    }

                    if ((O.items[i].MarketingCarrier == '6E') && ($.trim(O.items[i].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>'
                    }

                    var ftme = e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';
                    if (O.items[0].Trip == "I") {
                        ftme = O.items[i].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';
                    }
                    //else if (O.items[0].Trip == "I") {
                    //    ftme = "&nbsp;";
                    //}
                    str1 += '<div class="large-2 medium-2 small-2 columns bld">' + ftme + '</div>';

                    //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '<br />' + e.TerminalAirportInfo(O.items[i].DepartureTerminal, O.items[i].DepartureAirportName) + '</div>';
                    str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '<br />' + e.TerminalAirportInfo(O.items[i].ArrivalTerminal, O.items[i].ArrivalAirportName) + '</div><div class="clear"></div>';

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

                    str1 += '</div><div class="depcity"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + totDur + '<div class="clear"></div>';
                    for (var j = 0; j < R.items.length; j++) {

                        if (j >= 1 && R.items.length > 1 && j < R.items.length) {
                            str1 += e.GetLayOver(R.items, j);
                        }

                        if ((R.items[j].MarketingCarrier == '6E') && ($.trim(R.items[j].sno).search("INDIGOCORP") >= 0)) {
                            str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        else {
                            str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '<br/>Class(' + R.items[j].AdtRbd + ')</div>'
                        }
                        var Ftmer = e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';

                        if (R.items[0].Trip == "I") {
                            Ftmer = R.items[j].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';;
                        }
                        //else if (R.items[0].Trip == "I") {
                        //    Ftmer = "&nbsp;";
                        //}
                        str1 += '<div class="large-2 medium-2 small-2 columns bld">' + Ftmer + '</div>'

                        //str1 += '<div class="large-2 medium-2 small-2 columns bld">' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" /></div>'
                        str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + e.TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                        str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                        str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R.items[j].ArrivalLocation + '</span><br />' + R.items[j].ArrivalCityName + '<br />' + R.items[j].Arrival_Date + '<br />' + e.TerminalAirportInfo(R.items[j].ArrivalTerminal, R.items[j].ArrivalAirportName) + '</div><div class="clear"></div>';

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
            $('#' + this.rel + '_').html(str1);
            //$('#' + this.rel + '_').show();
            $('#' + this.rel + '_').slideToggle();
            $('#' + this).hide();
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
                str1 += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-15px; cursor:pointer; height:1px;" onclick="Close(\'' + this.rel + '_\');" title="Click to close Details">X</span><div></div>';
                str1 += '<table class="w100 f12"><tr><td  class="w50 f16 bld">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
                for (var i = 0; i < O.items.length; i++) {
                    str1 += '<tr><td>' + O.items[i].DepartureCityName + '-' + O.items[i].ArrivalCityName + '(' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(O.items[i].BagInfo) + '</td></tr>';
                }
                str1 += '</table></div>';
            }
            if (R.items.length > 0) {
                str1 += '<div class="depcity1">';
                str1 += '<table class="w100 f12">';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<tr><td class="w50">' + R.items[j].DepartureCityName + '-' + R.items[j].ArrivalCityName + '(' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + ')</td>'
                    str1 += '<td>' + e.BagInfo(R.items[j].BagInfo) + '</td></tr>';
                }
                str1 += '</table></div>';
            }
            str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            //str1 += ' <div class"rgt" onclick="Close(\'' + this.rel + '_\');" >X</div
            $('#' + this.rel + '_').html(str1);
            //$('#' + this.rel + '_').show();
            $('#' + this.rel + '_').slideToggle();
            // $('#' + this.id).toggleClass("fltDetailslink1");
            $('#' + this).hide();
        }
    });




};

ResHelper.prototype.GetLayOver = function (items, position) {
    var t = this;
    var str1 = "";

    var layoverTime = t.calFlightDur(items[position - 1].ArrivalTime.replace(":", ""), items[position].DepartureTime.replace(":", ""));

    str1 = '<div class="layover"> + <span> <i class="ico ico-time">&nbsp;</i> Layover: <strong>' + items[position].DepartureCityName + ' (' + items[position].DepartureLocation + ') </strong> </span> <span class="pipe"> | </span> <time> Time: <strong>' + layoverTime + '</strong> </time>  </div>';

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
    var arrDate = new Date(parseFloat(ArrivaldateArr[2]) - 1, (parseFloat(ArrivaldateArr[1]) - 1), (parseFloat(ArrivaldateArr[0]) + 1));

    dd = weekday[arrDate.getDay()] + ", " + ArrivaldateArr[0] + " " + m_names[parseFloat(ArrivaldateArr[1]) - 1]

    return dd;

}

ResHelper.prototype.GetSearchDisplay = function (c, triptype) {
    var t = this;
    var st = "";
    st += '<div class="lft">';
    st += '<div class="lft">';
    st += ' <img src="../Images/adt.png" />&nbsp;&nbsp;&nbsp;&nbsp;<br />';
    st += ' <span class="">&nbsp;' + c.Adult + '</span>';
    st += '</div>';
    st += ' <div class="lft">';
    st += '<img src="../Images/chd.png" />&nbsp;&nbsp;&nbsp;&nbsp;';
    st += '<br />';
    st += '<span class="">&nbsp;' + c.Child + '</span>';
    st += ' </div>';
    st += ' <div class="lft">';
    st += ' <img src="../Images/inf.png" />&nbsp;&nbsp;&nbsp;&nbsp;';
    st += '<br />';
    st += ' <span>&nbsp;' + c.Infant + '</span>';
    st += '</div>';
    st += '</div>';
    st += '<div class="bdrdot lft">&nbsp;</div>';


    if (triptype == "rdbOneWay") {

        st += ' <div class="lft plft10">';
        st += '  <div class="lft ">';
        st += t.hidtxtDepCity1.val().split(',')[0] + '<br />';
        st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate) + '</span>';
        st += ' </div>';
        st += ' <div class="lft">';
        st += '  <img src="../Images/arrow.png" /></div>';
        st += '  <div class="lft plft10">';
        st += t.hidtxtArrCity1.val().split(',')[0] + '<br />';
        // st += '  <span class="f10 txtgray">Fri 7 Mar </span>' ;
        st += '  </div>';
        st += ' </div>';
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
        st += '  <img src="../Images/arrow.png" /></div>';
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
            st += '  <img src="../Images/arrow.png" /></div>';
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
            st += '  <img src="../Images/arrow.png" /></div>';
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
            st += '  <img src="../Images/arrow.png" /></div>';
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
            st += '  <img src="../Images/arrow.png" /></div>';
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
            st += '  <img src="../Images/arrow.png" /></div>';
            st += '  <div class="lft plft10">';
            st += t.hidtxtArrCity5.val().split(',')[0] + '<br />';
            st += '  </div>';
            st += ' </div>';
            // st += '<div class="bdrdot lft">&nbsp;</div>';
        }

        t.DisplaySearchinput.html(st);

    }
    else if (triptype == "rdbRoundTrip") {

        st += ' <div class="lft plft10">';
        st += '  <div class="lft ">';
        st += t.hidtxtDepCity1.val().split(',')[0] + '<br />';
        st += ' <span class="f10 txtgray">' + t.FormatedDate(c.DepDate) + '</span>';
        st += ' </div>';
        st += ' <div class="lft">';
        st += '  <img src="../Images/rarrow.png" /></div>';
        st += '  <div class="lft plft10">';
        st += t.hidtxtArrCity1.val().split(',')[0] + '<br />';
        st += '  <span class="f10 txtgray">' + t.FormatedDate(c.RetDate) + '</span>';
        st += '  </div>';
        st += ' </div>';
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

        if ($(this).attr("rel") != null) {
            var lineNums = $(this).attr("rel").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            //var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");

            // var airc = lineNums[1] + "_" + lineNums[2];

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
            var str1 = '';
            str1 += '<div class="depcity">';
            str1 += '<div style="width: 60%; margin:10% 0 0 20% !important; padding: 10px;background: #ffffff;box-shadow:0px 0px 4px #333;margin: auto;border: 6px solid #eee;">';
            str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; right:3px;font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');">X</div>';
            str1 += '<div class="large-12 medium-12 small-12 bld">Flight Details</div><div>';

            if (O.items.length > 0) {
                try {
                    if ((parseInt(O.items[0].AvailableSeats1) <= 5) && (O.items[0].ValiDatingCarrier != 'SG')) {

                        str1 += '<div class="colorwht lft" style="background:#004b91; padding:2px 5px; border-radius:4px; color:#fff; position:relative; top:6px;">' + O.items[0].AvailableSeats1 + ' Seat(s) Left!</div><div class="clear1"></div>';
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

                        str1 += '<div class="large-12 medium-12 small-12 bld"><span class="f20">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span>&nbsp;<span class="f16">' + totDur + '</span></div><div class="clear1"></div>';
                    }
                    else {
                        str1 += '<div class="clear1"></div><hr /><div class="clear1"></div><div class="large-12 medium-12 small-12 bld">';
                    }
                    if ((O.items[i].MarketingCarrier == '6E') && ($.trim(O.items[i].sno).search("INDIGOCORP") >= 0)) {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/smITZ.gif" /><br />' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }
                    else {
                        str1 += '<div class="large-2 medium-2 small-2 columns"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '<br/>Class(' + O.items[i].AdtRbd + ')</div>';
                    }

                    var ftme = e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';
                    if (O.items[0].Trip == "I") {
                        ftme = O.items[i].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';
                    }
                    //else if (O.items[0].Trip == "I") {
                    //    ftme = "&nbsp;";
                    //}
                    str1 += '<div class="large-2 medium-2 small-2 columns bld">' + ftme + '</div>';
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '<br />' + e.TerminalAirportInfo(O.items[i].DepartureTerminal, O.items[i].DepartureAirportName) + '</div>';
                    str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>';
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '<br />' + e.TerminalAirportInfo(O.items[i].ArrivalTerminal, O.items[i].ArrivalAirportName) + '</div><div class="clear"></div></div>';

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

                    var Ftmer = e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';

                    if (R.items[0].Trip == "I") {
                        Ftmer = R.items[j].TripCnt + ' HRS<br/><img src="' + UrlBase + 'Images/duration.png" />';;
                    }
                    //else if (R.items[0].Trip == "I") {
                    //    Ftmer = "&nbsp;";
                    //}
                    str1 += '<div class="large-2 medium-2 small-2 columns bld">' + Ftmer + '</div>'
                    str1 += '<div class="large-3 medium-3 small-3 columns"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + e.TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                    str1 += '<div class="large-2 medium-2 small-2 columns dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
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

            str1 += '</div></div>';
            str1 += '<div class="clear"></div>';
        }
        $(this).next()[0].innerHTML = str1;
        $(this).next().fadeToggle(1000);
    });



    $('.fltBagDetailsR').click(function (event) {

        if ($(this).attr("rel") != null) {
            var lineNums = $(this).attr("rel").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            // var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");

            // var airc = lineNums[1] + "_" + lineNums[2];

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
            var str1 = '';
            str1 += '<div style="width: 60%; margin: 10% 0 0 20% !important; padding: 10px; background: #ffffff; box-shadow:0px 0px 15px #333; border: 6px solid #eee;">';
            str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');" title="Click to close Details">X</div>';
            str1 += '<div class="f20 bld colormn padding1 lft">Baggage Details</div>';

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
            str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. RWT does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            str1 += '</div></div>';

        }
        $('#' + idr).html(str1);
        $('#' + idr).fadeToggle(1000);
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
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
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


    $.ajax({
        url: UrlBase + 'FltSearch1.asmx/GetActiveAirlineProviders',
        type: "POST",
        data: JSON.stringify({
            org: c.HidTxtDepCity.split(',')[0], dest: c.HidTxtArrCity.split(',')[0], airline: airlineCode, rtfStatus: rtfStarus1, trip: c.Trip1, DepDate: c.DepDate, RetDate: c.RetDate
        }),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (e, t, n) {
            // alert("Sorry, we could not find a match for the destination you have entered..Kindly modify your search.");
            // window.location = UrlBase + 'Search.aspx';
            return false
        },
        success: function (data) {

            var providers = data.d;


            $.ajax({
                url: UrlBase + 'FltSearch1.asmx/GetFlightChacheDataList',
                type: "POST",
                data: JSON.stringify({
                    obj: c
                }),
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (e, t, n) {
                    // alert("Sorry, we could not find a match for the destination you have entered..Kindly modify your search.");
                    // window.location = UrlBase + 'Search.aspx';
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

                    t.postData(t, c.HidTxtAirLine + ":LCC", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false, true, $.trim(providers));
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
        Two: function (callback) {
            if (IsNRMLRoundTrip == true && mc != "rdbMultiCity") {
                if (true) {
                    //var prov = $.trim($.trim(providers).split('G8')[1].split('-')[0]);
                    t.postData(t, c.HidTxtAirLine + ":LCC", c, false, UrlBase + "AirHandler.ashx", false, true, true, $.trim(providers));
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

                t.postData(t, c.HidTxtAirLine + ':' + prov, c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", false, false, false, $.trim(providers));
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

                    t.postData(t, c.HidTxtAirLine + ":SG", c, IsNRMLRoundTrip, UrlBase + "AirHandler.ashx", true, false, true, $.trim(providers));
                    // t.Displayflter("CPN", c, IsNRMLRoundTrip);
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
                t.postData(t, c.HidTxtAirLine + prov, c, false, UrlBase + "AirHandler.ashx", false, true, false, $.trim(providers));
                //t.Displayflter("SG", c, IsNRMLRoundTrip);

            }
            else {

                t.Displayflter("1G", c, IsNRMLRoundTrip);
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
    if (rStatus == 5) {
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
        if (rStatus == 5) {
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
        if (rStatus == 5) {
            t.ApplyFilters(t, Airline, IsNRMLRoundTrip);
        }

    }

    t.MainSF.show();
    $.unblockUI();

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
ResHelper.prototype.postData = function (t, provider, c, IsNRMLRoundTrip, url1, isCoupon, isRTF, isLCC, providerLIst) {


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
    var qstr = "?Trip1=" + c.Trip1 + "&TripType1=" + c.TripType1 + "&DepartureCity=" + c.DepartureCity + "&ArrivalCity=" + c.ArrivalCity + "&HidTxtDepCity=" + c.HidTxtDepCity + "&HidTxtArrCity=" + c.HidTxtArrCity + "&Adult=" + c.Adult + "&Child=" + c.Child + "&Infant=" + c.Infant + "&Cabin=" + c.Cabin + "&AirLine=" + oairline + "&HidTxtAirLine=" + oairline + "&DepDate=" + c.DepDate + "&RetDate=" + c.RetDate + "&RTF=" + isRTF + "&NStop=" + c.NStop + "&GDSRTF=" + isRTF + "&Provider=" + prov + "&isCoupon=" + isCoupon + "&DepartureCity2=" + c.DepartureCity2 + "&ArrivalCity2=" + c.ArrivalCity2 + "&HidTxtDepCity2=" + c.HidTxtDepCity2 + "&HidTxtArrCity2=" + c.HidTxtArrCity2 + "&DepDate2=" + c.DepDate2 + "&DepartureCity3=" + c.DepartureCity3 + "&ArrivalCity3=" + c.ArrivalCity3 + "&HidTxtDepCity3=" + c.HidTxtDepCity3 + "&HidTxtArrCity3=" + c.HidTxtArrCity3 + "&DepDate3=" + c.DepDate3 + "&DepartureCity4=" + c.DepartureCity4 + "&ArrivalCity4=" + c.ArrivalCity4 + "&HidTxtDepCity4=" + c.HidTxtDepCity4 + "&HidTxtArrCity4=" + c.HidTxtArrCity4 + "&DepDate4=" + c.DepDate4 + "&DepartureCity5=" + c.DepartureCity5 + "&ArrivalCity5=" + c.ArrivalCity5 + "&HidTxtDepCity5=" + c.HidTxtDepCity5 + "&HidTxtArrCity5=" + c.HidTxtArrCity5 + "&DepDate5=" + c.DepDate5 + "&DepartureCity6=" + c.DepartureCity6 + "&ArrivalCity6=" + c.ArrivalCity6 + "&HidTxtDepCity6=" + c.HidTxtDepCity6 + "&HidTxtArrCity6=" + c.HidTxtArrCity6 + "&DepDate6=" + c.DepDate6 + "&isLCC=" + isLCC + "&ListProvider=" + providerLIst;
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
            for (var l = 0; l < data11.length; l++) {

                var data = JSON.parse(data11[l]);
                var resultG1 = data.result;//data.result;
                var result = "";

                if (resultG1.length > 1 && resultG1[1].length > 0 && resultG1[0].length > 0 && IsNRMLRoundTrip == true) {
                    $.unblockUI();
                }
                else if (resultG1.length > 0 && resultG1[0].length > 0) {
                    $.unblockUI();
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
                    if (rStatus == 5 && l == (parseInt(data11.length) - 1)) {
                        t.ApplyFilters(t, provider, IsNRMLRoundTrip);
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

                            t.GetResultR(joinArray, maxLineNum, maxLineNum, data.Provider + "ITZNRML95SFM");
                            t.OnewayH.hide();
                            t.RoundTripH.show();



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

                    if (rStatus == 5 && l == (parseInt(data11.length) - 1)) {
                        t.ApplyFilters(t, provider, IsNRMLRoundTrip);
                    }


                }

            }
            t.MainSF.show();


        },
        error: function (e, p, n) {
            //debugger;
            rStatus = rStatus + 1;
            t.ProgressBar(rStatus);
            if (rStatus == 5) {
                t.ApplyFilters(t, provider, IsNRMLRoundTrip);
            }
        }
    });
}

ResHelper.prototype.ProgressBar = function (stage) {
    var val = 10;
    //var remainder = stage % 3;
    //if (remainder == 0) {
    val = (stage * 20);
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
            // window.location = UrlBase + 'Search.aspx';

            return false
        },
        success: function (data) {
            var resultRTFSp = data.d;
            var result = "";
            if (resultRTFSp == "" || resultRTFSp == null) {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                $(document).ajaxStop($.unblockUI)
                // window.location = UrlBase + 'Search.aspx';

            } else {
                if (resultRTFSp[0] == "" || resultRTFSp[0] == null) {
                    alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                    $(document).ajaxStop($.unblockUI)
                    // window.location = UrlBase + 'Search.aspx';

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

    var t = this;
    var result = '';
    //  result += '<div class="lft w50">';
    // result += '<div  class="list w100">';

    var Provider1 = '';
    var Provider2 = '';
    if (resultArray[0].length > 0) {

        if ($.trim(Provider).search("SFM") > 0) {
            Provider1 = Provider + "1";
        }
        else { Provider1 = Provider; }
        var LnOb = maxLineNumO; //resultArray[0][resultArray[0].length - 1].LineNumber;
        for (var i = 1; i <= LnOb; i++) {
            //var OB = (from ct in Model.fltsearch1[0] where ct.LineNumber == i select ct).ToList();

            var OB = JSLINQ(resultArray[0])
                     .Where(function (item) { return item.LineNumber == i + "api" + Provider1; })
                     .Select(function (item) { return item });
            var k = 0;
            var O = "O";
            var unds = "_";
            if (OB.items.length > 0) {
                result += '<div class="list-item resR">';

                // grid layout

                result += '<div id="main1_' + i + "api" + Provider + '_O" class="fltboxnew mrgbtmG hide">';
                if (t.CheckMultipleCarrier(OB.items) == true) {

                    result += '<div><div class="w33 lft"><img alt=""   src="' + UrlBase + 'Airlogo/multiple.png" title=">Multiple Carriers"  /></div>';
                    result += '<div class="rgt w66 textalignright f16">Multiple Carriers</div></div>';

                }
                else {
                    if ((OB.items[0].MarketingCarrier == '6E') && ($.trim(OB.items[0].sno).search("INDIGOCORP") >= 0)) {
                        result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/smITZ.gif"   title="' + OB.items[0].AirLineName + '" /></div>';
                        result += '<div class="rgt w66 f16 textalignright">' + OB.items[0].FlightIdentification + '</div></div>';
                    }
                    else {
                        result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OB.items[0].MarketingCarrier + '.gif"   title="' + OB.items[0].AirLineName + '" /></div>';
                        result += '<div class="rgt w66 f16 textalignright">' + OB.items[0].MarketingCarrier + ' - ' + OB.items[0].FlightIdentification + '</div></div>';
                    }
                }
                result += '<div class="clear1"><hr /></div>';
                result += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OB.items[0].DepartureTime) + '</span></div>';
                result += '<div class="rgt textalignright w50"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime) + '</span></div>';
                result += '<div class="clear1"></div>';
                //if (t.DisplayPromotionalFare(OB.items[0]) == '') {
                //    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png"   /> &nbsp;</div>';
                //}
                //else {
                //    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png"   /> &nbsp;</div>';
                //}
                result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                result += '<span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + OB.items[0].LineNumber + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /> &nbsp;</span><div class="fade" id="fltdtls_' + OB.items[0].LineNumber + '_O" > &nbsp;</div>';
                result += '<div class="fltBagDetailsR lft" rel="fltdtls_' + OB.items[0].LineNumber + '_O"><img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                result += '<div class="f20 rgt img5t bld colorp">' + OB.items[0].TotalFare + '</div>';
                result += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                //result += '<div class="clear1"><hr /></div>';
                //result += '<div class="w100"><span  class="f16 bld lft">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + '</span></div>';
                result += '<div class="clear"></div>';
                result += '<div><span class="rgt  gray">' + OB.items[0].Stops + '</span></div>';
                result += '<div class="clear1"></div>';
                //result += '<div class="lft w50"><input type="checkbox" value="' + OB.items[0].LineNumber.toString() + '"  rel="' + OB.items[0].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                result += '<div class="rgt w50 textalignright"><input type="radio"  class="rgt" name="RO" value="' + OB.items[0].LineNumber + '" /></div>';
                //result += '<div class="clear"></div><div class="bld colorp italic">' + OB.items[0].AdtFareType + '</div>';
                if ((OB.items[0].ValiDatingCarrier == 'SG') && (($.trim(OB.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OB.items[0].Searchvalue).search("AP14") >= 0)))
                { result += '<div class="clear"></div><div class="bld colorp italic">Non Refundable</div>'; }
                else if (OB.items[0].Trip == 'I')
                { result += '<div class="clear"></div><div class="bld colorp italic">&nbsp;</div>'; }
                else {
                    result += '<div class="clear"></div><div class="bld colorp italic">' + OB.items[0].AdtFareType + '</div>';
                }
                result += '</div>';



                result += '<div id="main_' + i + "api" + Provider + '_O" class="fltbox mrgbtm bdrblue">';
                for (var obi = 0; obi < 1; obi++) { //OB.items.length; obi++) {
                    result += '<div class="large-2 medium-2 small-3 columns gray">';
                    result += '<div id="ret">';
                    if (t.CheckMultipleCarrier(OB.items) == true) {

                        result += '<img alt=""   src="' + UrlBase + 'Airlogo/multiple.png"/>';
                        result += '</div>';
                        result += '<div>Multiple Carriers</div>';
                        result += '<div class="airlineImage hide">' + OB.items[obi].AirLineName + '</div>';


                    }
                    else {
                        if ((OB.items[obi].MarketingCarrier == '6E') && ($.trim(OB.items[obi].sno).search("INDIGOCORP") >= 0)) {
                            result += '<img alt=""  src="../Airlogo/smITZ.gif"/>';
                            result += '</div>';
                            result += '<div class="gray">' + OB.items[obi].FlightIdentification + '</div>';
                            result += '<div class="airlineImage">RWT Fare</div>';
                        }
                        else {
                            result += '<img alt=""  src="../Airlogo/sm' + OB.items[obi].MarketingCarrier + '.gif"/>';
                            result += '</div>';
                            result += '<div class="gray">' + OB.items[obi].MarketingCarrier + '-' + OB.items[obi].FlightIdentification + '</div>';
                            result += '<div class="airlineImage">' + OB.items[obi].AirLineName + '</div>';
                        }

                    }


                    result += '</div>';
                    result += '<div class="large-8 medium-8 small-8 columns">';
                    result += '<div class="large-4 medium-4 small-4 columns">';
                    result += '<div class="f16">' + t.MakeupAdTime(OB.items[obi].DepartureTime) + '</div> ';
                    //  result += '<div>' + OB.items[obi].DepartureCityName + '</div>';

                    result += '</div>';
                    result += '<div class="large-4 medium-4 small-4 columns">';
                    result += '<div class="f16"> ' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime);
                    result += '<div class="arrtime hide">' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                    result += '</div>';
                    //  result += '<div>' + OB.items[obi].ArrivalCityName + '</div>';
                    result += '</div>';
                    result += '<div class="large-4 medium-4 small-4 columns passenger">';
                    if (k == 0) {
                        var totDur = "";

                        if (OB.items[obi].Provider == "TB") {
                            totDur = t.GetTotalDuration(OB.items);
                        }
                        else {
                            totDur = t.MakeupTotDur(OB.items[obi].TotDur)
                        }


                        result += '<div class="f16 bld blue">' + totDur + '</div>';

                        var freeMeal = "";
                        //if (OB.items[obi].Provider == "1G")
                        //{ freeMeal = "Free Meals"; }

                        result += '<div class="stops">' + freeMeal + '</div>';// ' + "Free Meals" + '
                        result += '<div class="totdur hide">' + t.MakeupTotDur(OB.items[obi].TotDur).replace(':', '') + '</div>';
                        result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '')) + '</div>';

                        if ($.trim(Provider).search("SFM") > 0) {
                            result += '<div class="srf hide">SRF</div>';
                        }
                        else { result += '<div class="srf hide">NRMLF</div>' }


                        //if (t.DisplayPromotionalFare(OB.items[obi]) == '') {
                        //    result += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails.png"   /></div>';
                        //}
                        //else {
                        //    result += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails1.png"   /></div>';
                        //}
                        result += '</div>';
                        result += '<div class="clear"></div>';
                        result += '<div class="large-12 medium-12 small-12 columns passenger">';

                        var rrfndO = 'n';
                        if (k == 0) {

                            var rnr = '<img src="' + UrlBase + 'images/non-refundable.png" title="Non-Refundable Fare" />';
                            if ($.trim(OB.items[obi].AdtFareType).toLowerCase() == "refundable") {
                                rnr = '<img src="' + UrlBase + 'images/refundable.png" title="Refundable Fare" />';
                                rrfndO = 'r';
                            }
                            if ($.trim(Provider).search("SFM") > 0) {

                                rnr = rnr + '<img src="' + UrlBase + 'images/srf.png" title="Special Return Fare" />'
                            }


                            result += '<div class="bld colorp italic lft passenger" style="white-space:nowrap;">' + rnr + '</div>'; //+ OB.items[obi].AdtFareType
                            //result += '<div><input type="checkbox"  value="' + OB.items[obi].LineNumber.toString() + '" rel="' + OB.items[obi].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a> </div>';

                            var FAirType0 = "";
                            if (OB.items[obi].AdtFar == "CPN" || OB.items[obi].AdtFar == "Coupon Fare") {
                                FAirType0 = "CPN";
                            }
                            else if (OB.items[obi].AdtFar == "CRP") {
                                FAirType0 = "CRP";
                            }
                            else {
                                FAirType0 = "NRM";
                            }
                            result += '<div class="AirlineFareType bld colorp italic hide">' + FAirType0 + '</div>';
                        }
                        result += '<div class="gridViewToolTip1 hide" title="' + OB.items[obi].LineNumber + '_O" >ss</div>';
                        result += '<div class="fltDetailslinkR cursorpointer lft" rel="fltdtlsR_' + OB.items[obi].LineNumber + '_O" >&nbsp; <img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /> &nbsp;</div>';
                        result += '<span class="fade" id="fltdtlsR_' + OB.items[obi].LineNumber + '_O" >&nbsp; </span>';
                        result += '<div class="lft"   ><a href="#"  class="fltBagDetailsR" rel="fltdtlsR_' + OB.items[obi].LineNumber + '_O"> <img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></a></div>';
                        result += '<span class="fltfareDetailsR cursorpointer lft" rel="FareRule_' + OB.items[obi].LineNumber + '_O" ><img src="' + UrlBase + 'images/fare-rules.png"  class="cursorpointer" alt="Fare Rule" title="Fare Rule" style="cursor:pointer;" /> &nbsp;</span><div class="fade"  title="' + OB.items[obi].LineNumber + '_O" > &nbsp;</div>';
                        result += '<div class="clear"></div>';
                    }
                    result += '</div>';
                    result += '</div>';
                    if (k == 0) {
                        result += '<div class="large-2 medium-2 small-2 columns">';
                        result += '<div class="f20 rgt colorp"><img src="' + UrlBase + 'Images/rsp.png"/>' + OB.items[obi].TotalFare + '</div>';
                        //result += '<img src="' + UrlBase + 'Images/rsp.png"  class="" style="padding-top:-13px;"/>';
                        result += '<div class="clear"></div>';

                        result += '<div class="rgt passenger gray"><span class="stops">' + OB.items[obi].Stops + '</span></div>';
                        for (var rf1 = 0; rf1 < OB.items.length; rf1++) {
                            result += '<div class="airstopO hide  gray">' + $.trim(OB.items[rf1].Stops).toString().toLowerCase() + '_' + $.trim(OB.items[rf1].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                        }




                        result += '<div class="lft">';
                        if (k == 0) {

                            result += '<input type="radio" name="O" value="' + OB.items[obi].LineNumber + '" />';
                            result += '<div class="deptime hide passenger">' + t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '') + '</div>';
                            result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '')) + '</div>';
                            result += '<div class="rfnd hide passenger">' + rrfndO + '</div> ';
                            result += '<div class="price hide passenger">' + OB.items[obi].TotalFare + '</div>';
                            if (OB.items[obi].AdtFar != "NRM") {
                                var FareTypeS1 = OB.items[obi].AdtFar;
                                var FareTypeShow1 = "";
                                if (FareTypeS1 == "CPN") {
                                    FareTypeShow1 = "Coupon Fare";
                                }
                                else if (FareTypeS1 == "CRP") {
                                    FareTypeShow1 = "Special Fare";
                                }
                                else {
                                    FareTypeShow1 = OB.items[obi].AdtFar;
                                }
                                //result += '<div class="passenger" style="color:red">' + FareTypeShow1 + '</div>';
                            }

                        }
                        result += '</div>';
                        result += '</div>';

                        if (OB.items[obi].AdtFar != "NRM") {
                            var FareTypeS = OB.items[obi].AdtFar;
                            var FareTypeShow = "";
                            if (FareTypeS == "CPN") {
                                FareTypeShow = "Coupon Fare";
                            }
                            else if (FareTypeS == "CRP") {
                                FareTypeShow = "Special Fare";
                            }
                            else if (FareTypeS == "SGNRML" || FareTypeS == "G8NRML" || FareTypeS == "6ENRML") {
                                FareTypeShow = "";
                            }
                            else {
                                FareTypeShow = OB.items[obi].AdtFar;
                            }
                            result += '<div class="passenger w100 restext">' + FareTypeShow + '</div>';
                        }
                        if (OB.items[obi].IsBagFare)
                            result += '<div class="w100 restext B2">Hand baggage fare only.<a href="#" onclick="displayBaggageDetails(\'' + OB.items[obi].MarketingCarrier + '\')"><u>more details..</u></a></div>';

                    }
                    k++;

                    result += '<div class="clear"></div>';
                }
                result += '</div>';
                result += '</div>';
            }
        }
    }
    // result += '</div>';
    // result += '</div>';
    t.divFromR.prepend(result);
    var result1 = '';


    // result += '<div class="rgt w50">';
    // result1 += '<div  class="listR w100">';
    if (resultArray.length > 1) {
        if (resultArray[1].length > 0) {
            if ($.trim(Provider).search("SFM") > 0) {
                Provider2 = Provider + "2";
            }
            else { Provider2 = Provider; }
            //int LnOb = Model.fltsearch1[1][Model.fltsearch1[1].Count - 1].LineNumber;
            var LnOb = maxLineNumR; //resultArray[1][resultArray[1].length - 1].LineNumber;
            for (var i = 1; i <= LnOb; i++) {
                //  var OR = (from ct in Model.fltsearch1[1] where ct.LineNumber == i select ct).ToList();
                var OR = JSLINQ(resultArray[1])
                    .Where(function (item) { return item.LineNumber == i + "api1" + Provider2; })
                    .Select(function (item) { return item });
                var k = 0;
                var R = "R";
                var unds = "_";
                if (OR.items.length > 0) {
                    result1 += '<div class="list-itemR">';
                    result1 += '<div id="main1_' + i + "api1" + Provider + '_R" class="fltboxnew mrgbtmG hide">';
                    if (t.CheckMultipleCarrier(OR.items) == true) {
                        result1 += '<div><div class="w33 lft"><img alt="" src="' + UrlBase + 'Airlogo/multiple.png"  /></div>';
                        result1 += '<div class="rgt w66 textaligncenter f16">Multiple Carriers</div></div>';
                    }
                    else {
                        if ((OR.items[0].MarketingCarrier == '6E') && ($.trim(OR.items[0].sno).search("INDIGOCORP") >= 0)) {
                            result1 += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/smITZ.gif"  title="' + OR.items[0].AirLineName + '" /></div>';
                            result1 += '<div class="rgt w66 textaligncenter f16">' + OR.items[0].FlightIdentification + '</div></div>';
                        }
                        else {
                            result1 += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OR.items[0].MarketingCarrier + '.gif"  title="' + OR.items[0].AirLineName + '" /></div>';
                            result1 += '<div class="rgt w66 textaligncenter f16">' + OR.items[0].MarketingCarrier + ' - ' + OR.items[0].FlightIdentification + '</div></div>';
                        }
                    }
                    result1 += '<div class="clear1"><hr /></div>';
                    result1 += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OR.items[0].DepartureTime) + '</span></div>';
                    result1 += '<div class="rgt w50 textalignright"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime) + '</span></div>';
                    result1 += '<div class="clear1"></div>';
                    //if (t.DisplayPromotionalFare(OR.items[0]) == '') {
                    //    result1 += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png"   /> &nbsp;</div>';
                    //}
                    //else {
                    //    result1 += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png"   /> &nbsp;</div>';
                    //}
                    result1 += '<div class="gridViewToolTip1 hide"  title="' + OR.items[0].LineNumber + '_R" >ss</div>';
                    result1 += '<span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + OR.items[0].LineNumber + '_R" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /> &nbsp;</span><div class="fade" id="fltdtls_' + OR.items[0].LineNumber + '_R" > &nbsp;</div>';
                    result1 += '<div class="fltBagDetailsR lft" rel="fltdtls_' + OR.items[0].LineNumber + '_R" ><img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                    result1 += '<div class="f20 rgt img5t bld colorp">' + OR.items[0].TotalFare + '</div>';
                    result1 += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                    //result1 += '<div class="clear1"><hr /></div>';
                    //result1 += '<div class="w100"><span  class="f16 bld lft">' + t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0] + '</span></div>';
                    result1 += '<div class="clear"></div>';
                    result1 += '<div><span class="rgt  gray">' + OR.items[0].Stops + '</span></div>';   // <a href="#" id="' +i + 'Det" rel="' + i + '">Details</a></div>';
                    result1 += '<div class="clear1"></div>';
                    //result1 += '<div class="lft w50"><input type="checkbox" name="checkm" value="' + OR.items[0].LineNumber.toString() + '"  rel="' + OR.items[0].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                    result1 += '<div class="rgt w50 textalignright"> <input type="radio"  class="rgt" name="RR" value="' + OR.items[0].LineNumber + '" /> </div>';
                    //result1 += '<div class="clear"></div><div class="bld colorp italic">' + OR.items[0].AdtFareType + '</div>';
                    if ((OR.items[0].ValiDatingCarrier == 'SG') && (($.trim(OR.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OR.items[0].Searchvalue).search("AP14") >= 0)))
                    { result1 += '<div class="clear"></div><div class="bld colorp italic">Non Refundable</div>'; }
                    else if (OR.items[0].Trip == 'I')
                    { result1 += '<div class="clear"></div><div class="bld colorp italic">&nbsp;</div>'; }
                    else {
                        result1 += '<div class="clear"></div><div class="bld colorp italic">' + OR.items[0].AdtFareType + '</div>';
                    }
                    result1 += '</div>';

                    result1 += '<div id="main_' + i + "api1" + Provider + '_R" class="fltbox mrgbtm">';
                    for (var obr = 0; obr < 1; obr++) { //OR.items.length; obr++) {
                        result1 += '<div class="large-2 medium-2 small-3 columns">';
                        result1 += '<div>';

                        if (t.CheckMultipleCarrier(OR.items) == true) {
                            result1 += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png"/>';
                            result1 += '</div>';
                            result1 += '<div>Multiple Carriers</div>';
                            result1 += '<div class="airlineImage hide">' + OR.items[obr].AirLineName + '</div>';
                        }
                        else {
                            if ((OR.items[obr].MarketingCarrier == '6E') && ($.trim(OR.items[obr].sno).search("INDIGOCORP") >= 0)) {
                                result1 += '<img alt="" src="../Airlogo/smITZ.gif"/>';
                                result1 += '</div>';
                                result1 += '<div class="gray">' + OR.items[obr].FlightIdentification + '</div>';
                                result1 += '<div class="airlineImage">RWT Fare</div>';
                            }
                            else {
                                result1 += '<img alt="" src="../Airlogo/sm' + OR.items[obr].MarketingCarrier + '.gif"/>';
                                result1 += '</div>';
                                result1 += '<div class="gray">' + OR.items[obr].MarketingCarrier + '-' + OR.items[obr].FlightIdentification + '</div>';
                                result1 += '<div class="airlineImage">' + OR.items[obr].AirLineName + '</div>';
                            }

                        }

                        result1 += '</div>';
                        result1 += '<div class="large-8 medium-8 small-8 columns">';
                        result1 += '<div class="large-4 medium-4 small-4 columns">';
                        result1 += '<div class="f16">' + t.MakeupAdTime(OR.items[obr].DepartureTime) + '</div>';
                        // result1 += '<div>' + OR.items[obr].DepartureCityName + '</div>';


                        result1 += '</div>';

                        result1 += '<div class="large-4 medium-4 small-4 columns">';
                        result1 += '<div class="f16"> ' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime) + '</div>';
                        result1 += '<div class="arrtime hide">' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                        // result1 += '<div>' + OR.items[obr].ArrivalCityName + '</div>';
                        result1 += '</div>';
                        result1 += '<div class="large-4 medium-4 small-4 columns passenger">';
                        if (k == 0) {
                            var totDur = "";

                            if (OR.items[obr].Provider == "TB") {
                                totDur = t.GetTotalDuration(OR.items);
                            }
                            else {
                                totDur = t.MakeupTotDur(OR.items[obr].TotDur)
                            }

                            result1 += '<div class="f16 bld blue"> ' + totDur + '</div>';
                            var freeMeal = "";
                            //if (OR.items[obr].Provider == "1G")
                            //{ freeMeal = "Free Meals"; }

                            result1 += '<div class="stops">' + freeMeal + '</div>'; // ' + "Free Meals" + '
                            result1 += '<div class="totdur hide">' + t.MakeupTotDur(OR.items[obr].TotDur).replace(':', '') + '</div>';
                            result1 += '<div class="atime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OR.items[obr].DepartureTime).replace(':', '')) + '</div>';
                            result += '</div>';
                            result += '<div class="clear"></div>';
                            result1 += '</div>';
                            result += '<div class="large-12 medium-12 small-12 columns passenger">';
                            var rrfndR = 'n';
                            if (k == 0) {

                                var rnr = '<img src="' + UrlBase + 'images/non-refundable.png" title="Non-Refundable Fare" />';
                                if ($.trim(OR.items[obr].AdtFareType).toLowerCase() == "refundable") {
                                    rnr = '<img src="' + UrlBase + 'images/refundable.png"  title="Refundable Fare"/>';
                                    rrfndR = 'r';

                                }
                                if ($.trim(Provider).search("SFM") > 0) {

                                    rnr = rnr + '<img src="' + UrlBase + 'images/srf.png" title="Special Return Fare" />'
                                }

                                result1 += '<div class="bld colorp lft italic passenger" style="white-space:nowrap;">' + rnr + '</div>'; //OR.items[obr].AdtFareType 
                                //result1 += '<div><input type="checkbox" name="checkm" value="' + OR.items[obr].LineNumber.toString() + '"  rel="' + OR.items[obr].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a> </div>';
                                if ($.trim(Provider).search("SFM") > 0) {
                                    result1 += '<div class="srf hide">SRF</div>';
                                }
                                else { result1 += '<div class="srf hide">NRMLF</div>' }

                                var FAirType2 = "";
                                if (OR.items[obr].AdtFar == "CPN" || OR.items[obr].AdtFar == "Coupon Fare") {
                                    FAirType2 = "CPN";
                                }
                                else if (OR.items[obr].AdtFar == "CRP") {
                                    FAirType2 = "CRP";
                                }
                                else {
                                    FAirType2 = "NRM";
                                }
                                result1 += '<div class="AirlineFareType bld colorp italic hide">' + FAirType2 + '</div>';

                            }
                            result1 += '<div class="gridViewToolTip1 hide" title="' + OR.items[obr].LineNumber + '_R" >ss</div>';
                            result1 += '<div class="fltDetailslinkR cursorpointer lft" rel="fltdtlsR_' + OR.items[obr].LineNumber + '_R" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /> &nbsp;</div>';
                            result1 += '<div class="fade" id="fltdtlsR_' + OR.items[obr].LineNumber + '_R" >nbsp;</div>';
                            result1 += '<div class="fltBagDetailsR lft" rel="fltdtlsR_' + OR.items[obr].LineNumber + '_R"><img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                            result1 += '<span class="fltfareDetailsR cursorpointer lft" rel="FareRule_' + OR.items[obr].LineNumber + '_R" ><img src="' + UrlBase + 'images/fare-rules.png"  class="cursorpointer" alt="Fare Rule" title="Fare Rule" style=" cursor:pointer;"  /> &nbsp;</span><div class="fade"  title="' + OR.items[obr].LineNumber + '_R" > &nbsp;</div>';
                            result1 += '<div class="clear"></div>';
                        }

                        result1 += '</div>';
                        if (k == 0) {
                            result1 += '<div class="large-2 medium-2 small-2 columns right">';
                            result1 += '<div class="price f20 rgt colorp"><img src="' + UrlBase + 'Images/rsp.png"/>' + OR.items[obr].TotalFare + '</div>';
                            //result1 += '<img src="' + UrlBase + 'Images/rsp.png"  class="" style="position:relative; top:-13px;" />';
                            result1 += '<div class="clear"></div>';
                            result1 += '<div class="rgt passenger  gray"><span class="stops">' + OR.items[obr].Stops + '</span></div>';
                            for (var rfo = 0; rfo < OR.items.length; rfo++) {
                                result1 += '<div class="airstopR hide  gray">' + $.trim(OR.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OR.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                            }



                            result1 += '<div class="lft">';
                            if (k == 0) {
                                result1 += '<input type="radio" name="R" value="' + OR.items[obr].LineNumber + '" />';
                                result1 += '<div class="deptime hide">' + t.MakeupAdTime(OR.items[obr].DepartureTime).replace(':', '') + '</div>';
                                result1 += '<div class="rfnd bld hide">' + rrfndR + '</div>';

                                if (OR.items[obr].AdtFar != "NRM") {

                                    var FareTypeS2 = OR.items[obr].AdtFar;
                                    var FareTypeShow2 = "";
                                    if (FareTypeS2 == "CPN") {
                                        FareTypeShow2 = "Coupon Fare";
                                    }
                                    else if (FareTypeS2 == "CRP") {
                                        FareTypeShow2 = "Special Fare";
                                    }
                                    else {
                                        FareTypeShow2 = OR.items[obr].AdtFar;
                                    }

                                    //result1 += '<div class="passenger" style="color:red">' + FareTypeShow2 + '</div>';
                                }

                            }
                            result1 += '</div>';
                            result1 += '</div>';

                            if (OR.items[obr].AdtFar != "NRM") {

                                var FareTypeS3 = OR.items[obr].AdtFar;
                                var FareTypeShow3 = "";
                                if (FareTypeS3 == "CPN") {
                                    FareTypeShow3 = "Coupon Fare";
                                }
                                else if (FareTypeS3 == "CRP") {
                                    FareTypeShow3 = "Special Fare";
                                }
                                else if (FareTypeS3 == "SGNRML" || FareTypeS3 == "G8NRML" || FareTypeS3 == "6ENRML") {
                                    FareTypeShow3 = "";
                                }
                                else {
                                    FareTypeShow3 = OR.items[obr].AdtFar;
                                }
                                result1 += '<div class="passenger w100 restext">' + FareTypeShow3 + '</div>';
                            }
                            if (OR.items[obr].IsBagFare)
                                result1 += '<div class="w100 restext B3">Hand baggage fare only.<a href="#" onclick="displayBaggageDetails(\'' + OR.items[obr].MarketingCarrier + '\')"> <u>more details..</u></a></div>';
                        }
                        k++;
                        result1 += '<div class="clear"></div>';
                    }
                    result1 += '</div>';
                    result1 += '</div>';
                }

            }
        }
    }
    // result1 += '</div>';
    //  result1 += '</div>';

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
    //result += '<div id="divFrom" class="list" style="width:100%;">';
    if (resultArray[0].length > 0) {
        var LnOb = maxLineNumber //resultArray[0][resultArray[0].length - 1].LineNumber;
        for (var i = 1; i <= LnOb; i++) {
            var OB = JSLINQ(resultArray[0])
                  .Where(function (item) { return item.LineNumber == i + "api" + Provider; })
                  .Select(function (item) { return item });
            var k = 0;
            var O1 = "O";
            var R1 = "R";
            var M1 = "M";
            var D1 = "D";
            var unds = "_";

            var OF;
            var RF;
            var ft;
            if (OB.items.length > 0) {
                result += '<div class="list-item resO">';


                OF = JSLINQ(OB.items)
               .Where(function (item) { return item.Flight == "1"; })
               .Select(function (item) { return item });

                var fta = JSLINQ(OB.items)
                       .Select(function (item) { return item.Flight });
                ft = Math.max.apply(Math, fta.items);

                RF = JSLINQ(OB.items)
               .Where(function (item) { return item.Flight == "2"; })
               .Select(function (item) { return item });

                // for grid layout
                if (OF.items.length > 0) {
                    result += '<div class="fltboxnew hide">';
                    if (t.CheckMultipleCarrier(OF.items) == true) {

                        result += '<div><div class="w33 lft"><img alt=""   src="' + UrlBase + 'Airlogo/multiple.png"  /></div>';
                        result += '<div class="rgt w66 textalignright f16">Multiple Carriers</div></div>';

                    }
                    else {
                        if ((OF.items[0].MarketingCarrier == '6E') && ($.trim(OF.items[0].sno).search("INDIGOCORP") >= 0)) {
                            result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/smITZ.gif" title="' + OF.items[0].AirLineName + '"  /></div>';
                            result += '<div class="rgt w66 textalignright f16">' + OF.items[0].FlightIdentification + '</div></div>';
                        }
                        else {
                            result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OF.items[0].MarketingCarrier + '.gif" title="' + OF.items[0].AirLineName + '"  /></div>';
                            result += '<div class="rgt w66 textalignright f16">' + OF.items[0].MarketingCarrier + ' - ' + OF.items[0].FlightIdentification + '</div></div>';
                        }
                    }
                    result += '<div class="clear1"><hr /></div>';
                    result += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OF.items[0].DepartureTime) + '</span></div>';
                    result += '<div class="rgt w50 textalignright"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime) + '</span></div>';
                    result += '<div class="clear1"></div>';


                    if (t.DisplayPromotionalFare(OF.items[0]) == '') {
                        result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png"   /> &nbsp;</div>';
                    }
                    else {
                        result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png"   /> &nbsp;</div>';
                    }

                    result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                    result += '<div><span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + OB.items[0].LineNumber + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /> &nbsp;</span><div class="fade" id="fltdtls_' + OB.items[0].LineNumber + '_O" > &nbsp;</div></div>';
                    result += '<div class="fltBagDetailsR lft" rel="fltdtls_' + OB.items[0].LineNumber + '_O"><img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';


                    if ($.trim(Provider).search("SFM") > 0) {
                        result += '<div class="f20 rgt img5t bld colorp">' + Math.ceil(OF.items[0].TotalFare / 2) + '</div>';
                    } else {
                        result += '<div class="f20 rgt img5t bld colorp">' + OF.items[0].TotalFare + '</div>';
                    }
                    result += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                    result += '<div class="clear1"></div>';
                    if (RF.items.length > 0) {
                        result += '<div class="w100"><span  class="lft bld colormn">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0] + '</span><span class="rgt">' + OB.items[0].Stops + '</span></div>';
                    }
                    else {
                        result += '<div class="w100"><span class="lft bld colormn">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + '</span><span class="rgt  gray">' + OB.items[0].Stops + '</span></div>';
                    }
                    result += '<div class="clear"></div>';
                    //result += '<div><span class="bld">' + OF.items[0].AdtFareType + '</span> |  <span class="fltDetailslinkR cursorpointer textunderline" rel="' + i + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails"   /></span><span class="fade textunderline" id="fltdtls_' + i + '_O" >Details</span></div>'; //<a href="#" id="' + i + 'Det" rel="' + i + '">Details</a></div>';
                    //result += '<div class="lft w66"><input type="checkbox" name="checkm"  value="' + OB.items[0].LineNumber.toString() + '"   rel="' + OB.items[0].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                    result += '<div class="rgt w28"><input type="button"  value="Book" class="buttonfltbk rgt" title="' + OB.items[0].LineNumber + '"  id="' + OB.items[0].LineNumber + '_O" /></div>';
                    result += '<div class="clear"></div>';
                    if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0)))
                    { result += '<div class="bld w100 colorp italic">Non Refundable</div>'; }
                    else if (OF.items[0].Trip == 'I')
                    { result += '<div class="bld w100 colorp italic">&nbsp;</div>'; }
                    else {
                        result += '<div class="bld w100 colorp italic">' + OF.items[0].AdtFareType + '</div>';

                    }
                    result += '</div>';
                }
                result += '<div class="fltbox mrgbtm">';
            }
            result += '<div id="OneWay">';
            if (OF.items.length > 0) {
                result += '<div class="large-2 medium-2 small-3 columns" >';
                result += '<div  class="logoimg">';
                if (t.CheckMultipleCarrier(OF.items) == true) {
                    result += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                    result += '</div>';
                    result += '<div>Multiple Carriers</div>';
                    result += '<div class="airlineImage hide">' + OF.items[0].AirLineName + '</div>';
                }
                else {
                    if ((OF.items[0].MarketingCarrier == '6E') && ($.trim(OF.items[0].sno).search("INDIGOCORP") >= 0)) {
                        result += '<img alt="" src="../Airlogo/smITZ.gif"/>';
                        result += '</div>';
                        result += '<div class="gray"><span>' + OF.items[0].FlightIdentification + '</div>';
                        result += '<div class="airlineImage">RWT Fare</div>';
                    }
                    else {
                        result += '<img alt="" src="../Airlogo/sm' + OF.items[0].MarketingCarrier + '.gif"/>';
                        result += '</div>';
                        result += '<div class="gray"><span>' + OF.items[0].MarketingCarrier + '</span> - ' + OF.items[0].FlightIdentification + '</div>';
                        result += '<div class="airlineImage">' + OF.items[0].AirLineName + '</div>';
                    }

                }


                result += '</div>';
                result += '<div class="large-2 medium-2 small-3 columns">';
                result += '<div class="f16">' + t.MakeupAdTime(OF.items[0].DepartureTime) + '</div>';
                result += '<div>' + OF.items[0].Departure_Date + '</div>';
                result += '<div>' + OF.items[0].DepartureCityName + '</div>';
                result += '<div class="deptime hide">' + t.MakeupAdTime(OF.items[0].DepartureTime).replace(':', '') + '</div>';
                result += '<div class="dtime hide">' + t.GetCommomTimeForFilter(t.MakeupAdTime(OF.items[0].DepartureTime).replace(':', '')) + '</div>';
                result += '</div>';
                result += '<div class="large-2 medium-2 small-3 columns">';
                result += '<div class="f16">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime) + '</div>';
                result += '<div>' + OF.items[OF.items.length - 1].Arrival_Date + '</div>';
                result += '<div>' + OF.items[OF.items.length - 1].ArrivalCityName + '</div>';
                result += '<div class="arrtime hide">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                result += '</div>';
                result += '<div class="large-1 medium-1 small-3 columns passenger">';

                var totDur = "";

                if (OF.items[0].Provider == "TB") {
                    totDur = t.GetTotalDuration(OF.items);
                }
                else {
                    totDur = t.MakeupTotDur(OF.items[0].TotDur)
                }

                result += '<div class="f16 bld blue">' + totDur + ' &nbsp;</div>';
                result += '<div class="stops">' + OF.items[0].Stops + '</div>';
                var freeMeal = "";
                //if (OF.items[0].Provider == "1G")
                //{ freeMeal = "Free Meals"; }

                result += '<div class="stops">' + freeMeal + '</div>';
                result += '<div class="totdur hide">' + t.MakeupTotDur(OF.items[0].TotDur).replace(':', '') + '</div>';
                for (var rfo = 0; rfo < OF.items.length; rfo++) {
                    result += '<div class="airstopO hide  gray">' + $.trim(OF.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OF.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                }


                result += '</div>';
                //result += '<div class="w24 lft"><div class="rfnd bld colorp italic">' + OF.items[0].AdtFareType + '</div><div class="clear1"></div>';
                if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0)))
                { result += '<div class="w24 lft"><img src="' + UrlBase + 'refundable.png"/><div class="rfnd bld colorp italic hide">Non Refundable</div>'; }
                    //else if (OF.items[0].Trip == 'I')
                    //{ result += '<div class="large-2 medium-2 small-3 columns passenger"><div class="rfnd bld colorp italic">&nbsp;</div><div class="clear1"></div>'; }
                else {
                    var rnr = '<img src="' + UrlBase + 'images/non-refundable.png" title="Non-Refundable Fare" />';

                    var rrfnd = 'n';
                    if ($.trim(OF.items[0].AdtFareType).toLowerCase() == "refundable") {
                        rnr = '<img src="' + UrlBase + 'images/refundable.png" title="Refundable Fare" />';
                        rrfnd = 'r';
                    }
                    if ($.trim(Provider).search("SFM") > 0) {

                        rnr = rnr + '<img src="' + UrlBase + 'images/srf.png" title="Special Return Fare" />'
                    }

                    result += '<div class="large-3 medium-3 small-3 columns passenger">' + rnr + '<div class="rfnd bld colorp italic hide">' + rrfnd + '</div>';
                    //debugger;
                    var FAirType = "";
                    if (OF.items[0].AdtFar == "CPN" || OF.items[0].AdtFar == "Coupon Fare") {
                        FAirType = "CPN";
                    }
                    else if (OF.items[0].AdtFar == "CRP") {
                        FAirType = "CRP";
                    }
                    else {
                        FAirType = "NRM";
                    }
                    result += '<div class="AirlineFareType bld colorp italic hide">' + FAirType + '</div>';
                }
                if (t.DisplayPromotionalFare(OF.items[0]) == '') {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png"   />&nbsp; </div>';
                }
                else {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png"   />&nbsp; </div>';
                }
                result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                result += '<div class="lft"><a href="#" id="' + OB.items[0].LineNumber + 'Det" rel="' + OB.items[0].LineNumber + '"  class="fltDetailslink"><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Flight Details" title="Flight Details"   /></a></div>';
                result += '<div class="lft">&nbsp;<a href="#" id="' + OB.items[0].LineNumber + 'BagDet" rel="' + OB.items[0].LineNumber + '"  class="fltBagDetails"> <img src="../images/icons/baggage.png"   alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></a></div>';
                if (OB.items[0].ValiDatingCarrier != 'SG' && OB.items[0].ValiDatingCarrier != '6E' && OB.items[0].ValiDatingCarrier != 'G8')
                    result += '<span class="fareRuleToolTip cursorpointer lft" rel="FareRule_' + OB.items[0].LineNumber + '_O" ><img src="' + UrlBase + 'images/fare-rules.png"  class="cursorpointer" alt="Fare Rule" title="Fare Rule" style="cursor:pointer;" /> &nbsp;</span><div class="fade"  title="' + OB.items[0].LineNumber + '_O" > &nbsp;</div>';

                result += '</div>';
                result += '<div class="large-2 medium-2 small-3 columns rgt bld">';
                result += '<img src="' + UrlBase + 'Images/rsp.png" class="lft" style="margin-top:8px;" />';
                if ($.trim(Provider).search("SFM") > 0) {
                    result += '<div class="price f20 lft colorp">' + Math.ceil(OF.items[0].TotalFare / 2) + '</div>';
                }
                else {
                    result += '<div class="price f20 lft colorp">' + OF.items[0].TotalFare + '</div>';
                }
                result += '<div class="clear"></div>';
                //result += '<div class="lft"><input type="checkbox" name="checkm"  value="' + OB.items[0].LineNumber.toString() + '"  rel="' + OB.items[0].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';

                if ((parseInt(OB.items[0].AvailableSeats1) <= 5) && (OB.items[0].ValiDatingCarrier != 'SG')) {
                    result += '<span class="passenger" style="color: #004b91;font-size: 12px; padding:2px 5px; border-radius:4px; ">' + OB.items[0].AvailableSeats1 + ' Seat(s) Left!</span>';
                }

                result += '<div class="w100 rgt"><input type="button"  value="Book"  class="buttonfltbk" title="' + OB.items[0].LineNumber + '"  id="' + OB.items[0].LineNumber + '" /></div>';



                result += '<div class="clear1"></div>';
                result += '</div>';

                result += ' ';
            }

            if (OB.items[0].AdtFar != "NRM") {
                var FareTypeS4 = OB.items[0].AdtFar;
                var FareTypeShow4 = "";
                if (FareTypeS4 == "CPN") {
                    FareTypeShow4 = "Coupon Fare";
                }
                else if (FareTypeS4 == "CRP") {
                    FareTypeShow4 = "Special Fare";
                }
                else if (FareTypeS4 == "SGNRML" || FareTypeS4 == "G8NRML" || FareTypeS4 == "6ENRML") {
                    FareTypeShow4 = "";
                }
                else {
                    FareTypeShow4 = OB.items[0].AdtFar;
                }
                result += '<div class="w100 restext">' + FareTypeShow4 + '</div>';
            }
            // BaggageFare
            if (OB.items[0].IsBagFare)
                result += '<div class="w100 restext B1">Hand baggage fare only. <a href="#" onclick="displayBaggageDetails(\'' + OB.items[0].MarketingCarrier + '\')"><u>more details..</u></a></div>';
            result += '<div class="clear"></div>';
            result += '</div>';

            for (var ff = 2; ff <= ft; ff++) {
                var RF = JSLINQ(OB.items)
                .Where(function (item) { return item.Flight == ff.toString(); })
                .Select(function (item) { return item });


                result += '<div id="Return">';
                if (RF.items.length > 0) {
                    result += '<hr class="w80 mauto" style="border:none; border-top:1px solid #eee; float:left; " /> ';
                    result += '<div class="clear1"> </div> ';
                    result += '<div class="large-2 medium-2 small-3 columns">'
                    result += '<div>';
                    if (t.CheckMultipleCarrier(RF.items) == true) {
                        result += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                        result += '</div>';
                        result += '<div>Multiple Carriers</div>';
                        result += '<div class="airlineImage hide">' + RF.items[0].AirLineName + '</div>';
                    }
                    else {
                        if ((RF.items[0].MarketingCarrier == '6E') && ($.trim(RF.items[0].sno).search("INDIGOCORP") >= 0)) {
                            result += '<img alt="" src="../Airlogo/smITZ.gif"/>';
                            result += '</div>';
                            result += '<div class="gray"><span>' + RF.items[0].FlightIdentification + '</div>';
                            result += '<div class="airlineImage">RWT Fare</div>';
                        }
                        else {
                            result += '<img alt="" src="../Airlogo/sm' + RF.items[0].MarketingCarrier + '.gif"/>';
                            result += '</div>';
                            result += '<div class="gray"><span>' + RF.items[0].MarketingCarrier + '</span> - ' + RF.items[0].FlightIdentification + '</div>';
                            result += '<div class="airlineImage">' + RF.items[0].AirLineName + '</div>';
                        }

                    }


                    result += '</div>';
                    result += '<div class="large-2 medium-2 small-3 columns">';
                    result += '<div class="f16">' + t.MakeupAdTime(RF.items[0].DepartureTime) + '</div>';
                    result += '<div>' + RF.items[0].Departure_Date + '</div>';
                    result += '<div>' + RF.items[0].DepartureCityName + '</div>';
                    result += '</div>';
                    result += '<div class="large-2 medium-2 small-3 columns">';
                    result += '<div class="f16">' + t.MakeupAdTime(RF.items[RF.items.length - 1].ArrivalTime) + '</div>';
                    result += '<div>' + RF.items[RF.items.length - 1].Arrival_Date + '</div>';
                    result += '<div>' + RF.items[RF.items.length - 1].ArrivalCityName + '</div>';
                    result += '</div>';
                    result += '<div class="large-2 medium-2 small-3 columns">';

                    var totDur = "";

                    if (RF.items[0].Provider == "TB") {
                        totDur = t.GetTotalDuration(RF.items);
                    }
                    else {
                        totDur = t.MakeupTotDur(RF.items[0].TotDur)
                    }

                    result += '<div class="f16">' + totDur + ' &nbsp;</div>';
                    result += '<div  class="stops">' + RF.items[0].Stops + '</div>';
                    for (var rfi = 0; rfi < RF.items.length; rfi++) {
                        result += '<div class="airstopO hide  gray">' + $.trim(RF.items[rfi].Stops).toString().toLowerCase() + '_' + $.trim(RF.items[rfi].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                    }
                    result += '</div>    ';
                    result += '<div class="clear1"></div>';
                }
                result += '</div>';
            }
            result += '<div id="' + OB.items[0].LineNumber.toString() + '_" style="display:none;width:100%;"></div>';
            result += '<div class="clear"></div>';
            result += '</div>';
            result += '</div>';
        }
    }
    // result += '</div>';
    return result;
};

ResHelper.prototype.GetSelectedRoundtripFlight = function (resultArray) {
    var e = this;
    $("input:radio").click(function () {
        $('#msg1').html("");
        e.RtfTotalPayDiv.html("");
        $('#showfare').hide();
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

                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");

            });

            //var lineNumsO = lineNums.split('api')[0];

            $('#main_' + lineNums + '_O').removeClass("fltbox").addClass("fltbox01");
            $('#main1_' + lineNums + '_O').addClass("fltbox02");

            if ($('#main_' + lineNums + '_O').find("input[type='radio']").attr('checked') != "checked") {
                $('#main_' + lineNums + '_O').find("input[type='radio']").attr('checked', 'checked');
            }
            if ($('#main1_' + lineNums + '_O').find("input[type='radio']").attr('checked') != "checked") {
                $('#main1_' + lineNums + '_O').find("input[type='radio']").attr('checked', 'checked');
            }


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

            if (Rbook[0].LineNumber.search('SFM') > 0) {
                e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, "SRF"));
            } else {
                e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, ""));
            }
            $('.list-itemR').each(function () {

                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");
            });
            var lineNumsR = lineNums.split('api1')[0];

            $('#main_' + lineNums + '_R').removeClass("fltbox").addClass("fltbox01");
            $('#main_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
            $('#main1_' + lineNums + '_R').addClass("fltbox02");
            $('#main1_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
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


                    e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
                    totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
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

                                e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
                                e.RtfTotalPayDiv.css("display", "block");
                                e.RtfBookBtn.css("display", "block");
                                isSRF = true;

                                totcurrentFare = parseInt(Obook[0].TotalFare + Rbook[0].TotalFare);
                                $('#showfare').show();



                            }
                            else {
                                // popup  to select another fare
                                $('#msg1').html("Selected Fare is not available. Please select another flight to continue your booking.");
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

                                e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
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

                                    $('#msg1').html("Selected Fare is not available. Please select another flight to continue your booking.");
                                    e.RtfBookBtn.css("display", "none");
                                    totcurrentFare = 0;
                                }



                                //e.RtfFltSelectDivO.html("");
                                //e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook, ""));
                                if (Obook.length > 0 && Rbook.length > 0) {

                                    e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
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
                                e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
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

                                    $('#msg1').html("Selected Fare is not available. Please select another flight to continue your booking.");
                                    e.RtfBookBtn.css("display", "none");
                                    totcurrentFare = 0;
                                }



                                //e.RtfFltSelectDivR.html("");
                                //e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook, ""));
                                if (Obook.length > 0 && Rbook.length > 0) {
                                    e.RtfTotalPayDiv.html('Current Fare: <img src="' + UrlBase + 'images/crs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
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

                    $('#prevfare').html(prevFare);
                    $('#FareDiff').html(diff);

                    $('#msg1').html("The fare of the selected flight has changed.");

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
            $('#msg1').html("Selected Fare is not available. Please select another flight to continue your booking.");
        }
    });
};


ResHelper.prototype.DisplaySelectedFlight = function (objFlt, type) {
    var string;
    var FAirType0 = "";
    if (objFlt.length > 1) {

        var fltArray = new Array();

        for (var i = 0; i < objFlt.length; i++) {

            fltArray.push(objFlt[i].MarketingCarrier);
        }

        if (objFlt[0].AdtFar == "CPN" || objFlt[0].AdtFar == "Coupon Fare") {
            FAirType0 = "Coupon Fare";
        }
        else if (objFlt[0].AdtFar == "CRP") {
            FAirType0 = "Special Fare";
        }
        else {
            FAirType0 = "";
        }
        //result += '<div class="AirlineFareType bld colorp italic hide">' + FAirType0 + '</div>';



        var fltArray1 = fltArray.unique();
        var img1 = "";
        if (fltArray1.length > 1) {
            img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
        }
        else {
            if ((objFlt[0].MarketingCarrier == '6E') && ($.trim(objFlt[0].sno).search("INDIGOCORP") >= 0)) {
                img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/smITZ.gif"  /></div> <div>' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div>';
            }
            else {
                img1 = '<div large-2 medium-2 small-3 columns> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div> <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div>';
            }

        }
        string = '<div class="large-3 medium-3 small-3 columns"> ' + img1 + '</div>';
        string += '<div class="large-6 medium-6 small-6 columns"><div>' + objFlt[0].DepartureCityName + ' - ' + objFlt[objFlt.length - 1].ArrivalCityName + '</div><div class="passenger lft f16">' + objFlt[0].DepartureTime + ' - ' + objFlt[objFlt.length - 1].ArrivalTime + '</div><div class="passenger lft f12" style="padding:2px 10px;font-weight:bold">' + '&nbsp;&nbsp;' + objFlt[0].Departure_Date + '</div></div>';
        if (type == "SRF") {
            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld">INR ' + Math.ceil(parseInt(objFlt[0].TotalFare)) + '/-</div>';

        } else {

            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld">INR ' + objFlt[0].TotalFare + '/-</div>';
        }
    }
    else {
        //string = '<div class="lft w18"><div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div>  <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</div></div>';

        if (objFlt[0].AdtFar == "CPN" || objFlt[0].AdtFar == "Coupon Fare") {
            FAirType0 = "Coupon Fare";
        }
        else if (objFlt[0].AdtFar == "CRP") {
            FAirType0 = "Special Fare";
        }
        else {
            FAirType0 = "";
        }
        if ((objFlt[0].MarketingCarrier == '6E') && ($.trim(objFlt[0].sno).search("INDIGOCORP") >= 0)) {
            string = '<div class="large-2 medium-2 small-3 columns"><div> <img src="../AirLogo/smITZ.gif"  /></div> <div>' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div></div>';
        }
        else {
            string = '<div class="large-3 medium-3 small-3 columns"><div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div> <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</br><span class="restext">' + FAirType0 + '</span></div></div>';
        }

        string += '<div class="large-6 medium-6 small-6 columns"><div>' + objFlt[0].DepartureCityName + ' - ' + objFlt[0].ArrivalCityName + '</div><div class="passenger lft f16">' + objFlt[0].DepartureTime + ' - ' + objFlt[0].ArrivalTime + '</div><div class="passenger f12 bld" style="padding:2px 10px;font-weight:bold">' + '&nbsp;&nbsp;' + objFlt[0].Departure_Date + '</div></div>';


        if (type == "SRF") {
            string += '<div class="large-3 medium-3 small-3 columns f16 blue bld">INR ' + Math.ceil(parseInt(objFlt[0].TotalFare)) + '/-</div>';

            string += '<div class="large-3 medium-3 small-3 columns"><img src="' + UrlBase + 'images/srf.png" title="Special Return Fare" /></div>';
        } else { string += '<div class="large-3 medium-3 small-3 columns f16 blue bld">INR ' + objFlt[0].TotalFare + '/-</div>'; }
    }

    return string;
};
ResHelper.prototype.RTFFinalBook = function () {

    var e = this;
    e.RtfBookBtn.click(function () {

        if (Obook != null && Rbook != null) {
            $("#searchquery").hide();
            //            $("#div_Progress").show();
            //            $.blockUI({
            //                message: $("#waitMessage")
            //            });

            var obookline = Obook[0].LineNumber;
            var rbookline = Rbook[0].LineNumber;

            ChangedFarePopupShow(0, 0, 0, 'hide', 'D');

            if (Obook[0].LineNumber.search('SFM') > 0) {
                /// SPL
                var fltReturnArrayM = JSLINQ(CommanRTFArray)
                       .Where(function (item) { return item.LineNumber == obookline; })
                       .Select(function (item) { return item });




                var arr = new Array(fltReturnArrayM.items);
                var iscahem = arr[0][0].LineNumber;
                for (var i = 0; i < arr[0].length; i++) {
                    arr[0][i].ProductDetailQualifier = arr[0][i].LineNumber.split('ITZ')[1];
                    arr[0][i].LineNumber = arr[0][i].LineNumber.split('api')[0];


                }

                var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
                $.ajax({
                    url: t,
                    type: "POST",
                    data: JSON.stringify({
                        a: arr
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (e) {
                        for (var i = 0; i < arr[0].length; i++) {
                            arr[0][i].LineNumber = iscahem;
                        }


                        if (e.d.ChangeFareO.TrackId == "0") {
                            alert("Selected fare has been changed.Please select another flight.");
                            window.location = UrlBase + "Search.aspx";
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


                var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
                $.ajax({
                    url: t,
                    type: "POST",
                    data: JSON.stringify({
                        a: rtfArray
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
                            alert("Selected fare has been changed.Please select another flight.");
                            //$("#searchquery").show();
                            $(document).ajaxStop($.unblockUI);
                            window.location = UrlBase + "Search.aspx";
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
                        alert(t)
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
          .Where(function (item) { return item.AdtFar.length < 4 })
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
            FilterFareTypeShow = "Coupon Fare";
            FareTypeS1 = "CPN";
        }
        else if (marray[i] == "CRP" || marray[i] == "Special Fare") {
            FilterFareTypeShow = "Special Fare";
            FareTypeS1 = "CRP";
        }
        else {
            FilterFareTypeShow = "Normal";
            //FareTypeS1 = marray[i];
            FareTypeS1 = "NRM";
        }
        if (FareTypeS1 != "") {
            //CouponFare = false;
            //SpecialFare = false;
            //NormalFareF == false;
            str += '<div class="lft w8"> <input value="' + FareTypeS1 + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft w80" style="padding-top:3px;"><label for="' + FareTypeS1 + '">' + FilterFareTypeShow + '</label> </div> <div class="clear"> </div>';
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


        var str1 = '<div style="text-align: center; ">We are confirming your flight.Please wait for a moment.</div>';

        str1 += ' <div class="clear1"> </div> ';
        str1 += '<div class="w30 auto"  style="text-align: center; "><img alt="loading" width="50px" src="../images/loadingAnim.gif"/> </div> ';

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