﻿
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

$(document).ready(function() {
    RHandler = new ResHelper;
    RHandler.BindEvents()
});

function DiplayMsearch1(id) {
    $("#" + id).fadeToggle(1000);
}
function DiplayMsearch(obj) {

    //$(obj).parent().parent().parent().fadeToggle(1000);



    $('.fade').each(function() {

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
        $('.list-item').each(function() {

            $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
            $(this).find('.mrgbtmG').removeClass("fltbox02");

        });

        $('#main_' + Obook[0].LineNumber + '_O').removeClass("fltbox").addClass("fltbox01");
        $('#main1_' + Obook[0].LineNumber + '_O').addClass("fltbox02");




        if ($('#main_' + Obook[0].LineNumber + '_O').find("input[type='radio']").attr('checked') != "checked") {
            $('#main_' + Obook[0].LineNumber + '_O').find("input[type='radio']").attr('checked', 'checked');
        }
        if ($('#main1_' + Obook[0].LineNumber + '_O').find("input[type='radio']").attr('checked') != "checked") {
            $('#main1_' + Obook[0].LineNumber + '_O').find("input[type='radio']").attr('checked', 'checked');
        }
    }


    if (Rbook != null) {
        $('.list-itemR').each(function() {

            $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
            $(this).find('.mrgbtmG').removeClass("fltbox02");
        });
        $('#main_' + Rbook[0].LineNumber + '_R').removeClass("fltbox").addClass("fltbox01");
        $('#main_' + Rbook[0].LineNumber + '_R').find("input[type='radio']").attr('checked', 'checked');
        $('#main1_' + Rbook[0].LineNumber + '_R').addClass("fltbox02");
        $('#main1_' + Rbook[0].LineNumber + '_R').find("input[type='radio']").attr('checked', 'checked');
    }









}

var ResHelper = function() {
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

};
ResHelper.prototype.BindEvents = function() {
    var e = this;
    e.GetResult(e)
    e.eventFn();
    e.NextPrevSearch(e);
};


ResHelper.prototype.NextPrevSearch = function(e) {


    e.RtfToNextDay.click(function(event) {
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

    e.RtfToPrevDay.click(function(event) {
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



    e.RtfFromNextDay.click(function(event) {
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

    e.RtfFromPrevDay.click(function(event) {
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






    e.PrevDaySrch.click(function(event) {
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
        if (depdate.getMonth() < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }
        if (arrDate.getMonth() < 10) {
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
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };
    });

    e.NextDaySrch.click(function(event) {
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
        if (depdate.getMonth() < 10) {
            depM = "0" + depM;
        }
        else {
            depM = depM;
        }

        if (arrDate.getMonth() < 10) {
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
        if (Trip == "D") { window.location.href = UrlBase + 'Domestic/Result.aspx?' + dataString; }
        else if (Trip == "I") { window.location.href = UrlBase + 'International/FltResIntl.aspx?' + dataString; };

    });
};

ResHelper.prototype.eventFn = function() {
    var e = this;
    e.flterTabO.click(function() {
        divclspmatrix = 0;
        e.flterR.hide();
        e.flterO.show();
        e.flterTabO.removeClass('spn');
        $('.matrix').addClass('brdrred');
        $(".clspMatrix").switchClass("clspMatrix1", "clspMatrix");
        $('.list-itemR .fltbox').removeClass('brdrred');
        $('.list-itemR .fltboxnew').removeClass('brdrred');
        $('.list-item .fltbox').addClass('brdrred');
        $('.list-item .fltboxnew').addClass('brdrred');
        e.flterTabO.removeClass('spn1');
        e.flterTabO.addClass('spn1');
        e.flterTabR.addClass('spn');
        e.DivRefinetitle.html(e.txtDepCity1.val() + " to " + e.txtArrCity1.val());
        e.DivMatrixRtfO.show();
        e.DivMatrixRtfR.hide();
    });

    e.flterTabR.click(function() {
        divclspmatrix = 1;
        e.flterO.hide();
        e.flterR.show();
        e.flterTabR.removeClass('spn');
        e.flterTabR.removeClass('spn1');
        $(".clspMatrix").switchClass("clspMatrix1", "clspMatrix");
        $('.list-item .fltbox').removeClass('brdrred');
        $('.list-item .fltboxnew').removeClass('brdrred');
        $('.list-itemR .fltbox').addClass('brdrred');
        $('.list-itemR .fltboxnew').addClass('brdrred');
        $('.matrix').addClass('brdrred');
        e.flterTabR.addClass('spn1');
        e.flterTabO.addClass('spn');
        e.DivRefinetitle.html(e.txtArrCity1.val() + " to " + e.txtDepCity1.val());
        e.DivMatrixRtfO.hide();
        e.DivMatrixRtfR.show();

    });

    e.clspMatrix.click(function() {

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


    e.rtfResultDiv.click(function() {
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
    e.lccRTFDiv.click(function() {

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
            e.DivMatrix.html(t.GetMatrix(lccRtfResultArray[0], 'O'));
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
    e.gdsRTFDiv.click(function() {

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
            e.DivMatrix.html(e.GetMatrix(gdsRtfResultArray[0], 'O'));
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


ResHelper.prototype.Book = function(result) {
    var e = this;
    this.bookO = $(".buttonfltbk");
    e.bookO.click(function() {
        $("#searchquery").hide();
        $("#div_Progress").show();
        $.blockUI({
            message: $("#waitMessage")
        });
        var lineNum = $.trim($(this).attr("title"));
        var fltSelectedArray = JSLINQ(result[0])
                         .Where(function(item) { return item.LineNumber == lineNum; })
                         .Select(function(item) { return item });
        var arr = new Array(fltSelectedArray.items);
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
                success: function(e) {
                    InsertedTID = e.d;
                    if (InsertedTID[0] == "0") {
                        alert("Selected fare has been changed.Please select another flight.");
                        $("#searchquery").show();
                        $(document).ajaxStop($.unblockUI);
                    } else {
                        window.location = UrlBase + "International/PaxDetails.aspx?" + InsertedTID + ",I";
                    }
                },
                error: function(e, t, n) {
                    alert(t)
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
                success: function(e) {

                    InsertedTID = e.d;
                    if (InsertedTID[0] == "0") {
                        alert("Selected fare has been changed.Please select another flight.");
                        $("#searchquery").show();
                        //$("#BTNDIV").show();
                        // $("#div_Progress").hide();
                        $(document).ajaxStop($.unblockUI)
                    } else {
                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + InsertedTID;
                    }
                },
                error: function(e, t, n) {
                    alert(t)
                }
            })
        }
    });



};



ResHelper.prototype.ShowFareBreakUp = function(result) {

    var t = this;
    this.gridViewToolTip = $(".gridViewToolTip");
    t.gridViewToolTip.mouseover(function(event) {
        var th = this;
        var lineNum = $(th).next().attr('title').split('_');

        var fltSelectedArray;
        if (lineNum[1] == "R") {
            fltSelectedArray = JSLINQ(result[1])
                         .Where(function(item) { return item.LineNumber == lineNum[0]; })
                         .Select(function(item) { return item });
        }
        else {


            fltSelectedArray = JSLINQ(result[0])
                            .Where(function(item) { return item.LineNumber == lineNum[0]; })
                            .Select(function(item) { return item });
        }

        if (fltSelectedArray.items[0].Trip == "I") {
            var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
            $.ajax({
                url: n,
                type: "POST",
                data: JSON.stringify({
                    AirArray: fltSelectedArray.items,
                    Trip: "I"
                }),
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function(e) {
                    $(th).next().html(t.CreateFareBreakUp(e.d[0]));
                    t.gridViewToolTip.tooltip({

                        track: true,
                        delay: 0,
                        showURL: false,
                        fade: 100,
                        bodyHandler: function() {
                            return $($(th).next().html());
                        },
                        showURL: false

                    });
                },
                error: function(e, t, n) {
                    alert(t)
                }
            })

        }
        else {
            $(th).next().html(t.CreateFareBreakUp(fltSelectedArray.items[0]));
            t.gridViewToolTip.tooltip({

                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function() {
                    return $($(th).next().html());

                },
                showURL: false

            });
        }


    });


};
ResHelper.prototype.CreateFareBreakUp = function(e) {
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
    if (e.AdtFareType == 'Spl. Fare, No Commission')
    { a = (e.ADTAgentMrk * e.Adult) + (e.CHDAgentMrk * e.Child); }
    else
    { a = e.TotMrkUp; }
    var f = e.TotCB;
    var l = e.TotTds;
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
    strResult = strResult + "<td>" + n + "</td>";
    strResult = strResult + "<td>" + r + "</td></tr>";
    if (e.Child > 0) {
        strResult = strResult + "<tr><td class='bld'>CHD</td>";
        strResult = strResult + "<td>" + e.ChdBFare + "</td>";
        strResult = strResult + "<td>" + e.ChdFSur + "</td>";
        strResult = strResult + "<td>" + i + "</td>";
        strResult = strResult + "<td>" + s + "</td></tr>"
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
        strResult = strResult + "<td><span class='bld'>Tran. Charge:</span><br />" + a + "</td>";
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



ResHelper.prototype.DisplayPromotionalFare = function(e) {

    var pmf = '';
    try {
        //if ((e.fareBasis.substring(0, 1) == "P") && (e.ValiDatingCarrier == "6E")) {
        //    pmf = "Promotional Fare";
        // }
        //if ((e.AdtFareType == "Spl. Fare, No Commission") && (e.ValiDatingCarrier == "6E")) {
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


ResHelper.prototype.MakeupTotDur = function(totDur) {

    var tdur = "";
    if ($.trim(totDur) != "") {
        if ($.trim(totDur).search("hrs") > 0) {
            var hrsmin;

            tdur = $.trim(totDur).replace(" hrs", ":").replace(" min", "");
            hrsmin = tdur.split(":");
            tdur = ($.trim(hrsmin[0]).length > 1 ? hrsmin[0] : "0" + hrsmin[0]) + ":" + ($.trim(hrsmin[1]).length > 1 ? +$.trim(hrsmin[1]) : "0" + +$.trim(hrsmin[1]))

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

                tdur = (hrs.length > 1 ? hrs : "0" + hrs) + ":" + (rmin.length > 1 ? +rmin : "0" + +rmin)
            }


        }
    }
    return tdur;
};


ResHelper.prototype.MakeupAdTime = function(adTime) {
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

ResHelper.prototype.GetMinMaxPrice = function(result) {

    var marray = new Array();
    var OF = JSLINQ(result)
           .Select(function(item) { return item.TotalFare });

    marray.push(Math.min.apply(Math, OF.items));
    marray.push(Math.max.apply(Math, OF.items));
    return marray;

};

ResHelper.prototype.GetMinMaxTime = function(result) {

    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
           .Select(function(item) { return e.MakeupAdTime(item.DepartureTime).replace(':', '') });
    marray.push(Math.min.apply(Math, OF.items));
    marray.push(Math.max.apply(Math, OF.items));
    return marray;

};

ResHelper.prototype.GetUniqueAirline = function(result, cls, type) {
    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
          .OrderBy(function(item) { return item.TotalFare })
           .Select(function(item) { return item.AirLineName });

    marray = OF.items.unique1();

    var str = '<div class="jplist-group" data-control-type="Airlinefilter' + type + '" data-control-action="filter"  data-control-name="Airlinefilter' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'

    for (var i = 0; i < marray.length; i++) {


        str += '<div class="lft w15"> <input value="' + marray[i] + '"  id="CheckboxA' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft w80 t5"><label for="' + marray[i] + '">' + marray[i] + '</label> </div> <div class="clear2"> </div>';

    }
    str += '</div>';

    if (type == 'O') {
        e.airlineFilter.html(str);
    }
    else {
        e.airlineFilterR.html(str);
    }


};


ResHelper.prototype.GetStopFilter = function(result, cls, type) {

    var e = this;
    var marray = new Array();
    var OF = JSLINQ(result)
          .OrderBy(function(item) { return item.Stops })
           .Select(function(item) { return item.Stops });

    marray = OF.items.unique1();

    var str = '<div class="jplist-group" data-control-type="Stopfilter' + type + '" data-control-action="filter"  data-control-name="Stopfilter' + type + '"';
    str += ' data-path=".' + cls + '" data-logic="or">'

    for (var i = 0; i < marray.length; i++) {

        str += '<div class="clear1"> </div><div class="lft w15"> <input value="' + marray[i] + '"  id="CheckboxS' + type + i + 1 + '"  type="checkbox"  /> </div><div class="lft t5 w80"> <label for="' + marray[i] + '">' + marray[i] + '</label></div>';

    }
    str += '</div>';
    if (type == 'O') {
        e.stopFilter.html(str);
    }
    else {
        e.stopFilterR.html(str);
    }

};

ResHelper.prototype.GetMatrix = function(result, type) {

    var e = this;
    var stopArray = new Array();
    var OF1 = JSLINQ(result)
           .OrderBy(function(item) { return item.Stops })
           .Select(function(item) { return item.Stops });
    stopArray = OF1.items.unique();

    var AirArray = new Array();
    var OF = JSLINQ(result)
           .OrderBy(function(item) { return item.TotalFare })
           .Select(function(item) { return $.trim(item.MarketingCarrier) + "_" + $.trim(item.AirLineName) });

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

    str += '<table class="matrix" cellpadding="0" cellspacing="0" >';


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

ResHelper.prototype.GetFareForMatrix = function(airNameCode, stop, result, datapath) {
    var airL = airNameCode.split('_');

    var OB = JSLINQ(result)
             .Where(function(item) { return item.AirLineName == airL[1] && item.Stops == stop; })
             .Select(function(item) { return item.TotalFare })
    //.OrderByDescending(function (item) { return item.TotalFare })
    var minval = Math.min.apply(Math, OB.items);

    var str = '';

    if (OB.items.length > 0) {
        var datafltr = $.trim(stop).toString().toLowerCase() + '_' + $.trim(airL[1]).toString().toLowerCase().replace(' ', '_');
        str += '<div  data-path="' + datapath + '" data-button="true"  data-text="' + datafltr + '"  data-fltr="' + datafltr + '" >';
        str += '<img src="' + UrlBase + 'Images/rs.png" style="height:9px; margin-top:6px;" />&nbsp;' + minval;
        str += '</div>';
    }
    else {
        str = '-';
    }
    return str;
};



Array.prototype.unique = function() {
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
Array.prototype.unique1 = function() {
    var a = [];
    var l = this.length;
    for (var i = 0; i < l; i++) {
        if (i == 0)
        { a.push(this[i]); }
        else {
            var flg = false;
            for (var j = 0; j < a.length; j++) {
                if (a[j] === this[i]) {
                    flg = true;
                }
            }
            if (flg == false)
                a.push(this[i]);

        }
    }
    return a;
};


ResHelper.prototype.calFlightDur = function(departTime, arrivalTime) {
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

ResHelper.prototype.getFourDigitTime = function(val) {

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

ResHelper.prototype.GetFltDetails = function(result) {
    var e = this;
    $('.fltDetailslink').click(function(event) {

        event.preventDefault();
        // var main = $.parseJSON($('#' + this.rel + 'M').html());
        //   
        if (this.rel != null) {
            $('#' + this.rel + '_').slideUp();
            var lineNums = this.rel; //.split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };


            // var airc = lineNums[1] + "_" + lineNums[2];
            var OB = JSLINQ(result[0])
                                 .Where(function(item) { return item.LineNumber == lineNums; })
                                 .Select(function(item) { return item });

            var O = JSLINQ(OB.items)
                             .Where(function(item) { return item.Flight == "1"; })
                             .Select(function(item) { return item });

            var R = JSLINQ(OB.items)
           .Where(function(item) { return item.Flight == "2"; })
           .Select(function(item) { return item });
            var str1 = '<div>';
            if (O.items.length > 0) {
                str1 += '<div class="depcity"><span style="font-size:20px; float:right; position:relative; top:-5px; right:-5px; cursor:pointer;" onclick="Close(\'' + this.rel + '_\');" title="Click to close Details">X</span><div><span class="f20">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span>&nbsp;' + e.MakeupTotDur(O.items[0].TotDur) + '</div><div class="clear"></div>';
                for (var i = 0; i < O.items.length; i++) {
                    str1 += '<div class="lft w24"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '</div>'
                    str1 += '<div class="lft w10 bld textaligncenter"><h1>' + e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS</h1><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="lft w24" style="text-align:right;"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '<br />' + e.TerminalAirportInfo(O.items[i].DepartureTerminal, O.items[i].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w10 dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                    str1 += '<div class="lft w24"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '<br />' + e.TerminalAirportInfo(O.items[i].ArrivalTerminal, O.items[i].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }
            if (R.items.length > 0) {
                str1 += '</div><div class="depcity"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + R.items[0].TotDur + '<div class="clear"></div>';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<div class="lft w24"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '</div>'
                    str1 += '<div class="lft w10 f20 bld textaligncenter"><h1>' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS</h1><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="lft w24" style="text-align:right;"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + e.TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w10 dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                    str1 += '<div class="lft w24"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R.items[j].ArrivalLocation + '</span><br />' + R.items[j].ArrivalCityName + '<br />' + R.items[j].Arrival_Date + '<br />' + e.TerminalAirportInfo(R.items[j].ArrivalTerminal, R.items[j].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
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




    $('.fltBagDetails').click(function(event) {

        event.preventDefault();
        // var main = $.parseJSON($('#' + this.rel + 'M').html());
        //   
        if (this.rel != null) {
            $('#' + this.rel + '_').slideUp();
            var lineNums = this.rel; //.split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };


            // var airc = lineNums[1] + "_" + lineNums[2];
            var OB = JSLINQ(result[0])
                                 .Where(function(item) { return item.LineNumber == lineNums; })
                                 .Select(function(item) { return item });

            var O = JSLINQ(OB.items)
                             .Where(function(item) { return item.Flight == "1"; })
                             .Select(function(item) { return item });

            var R = JSLINQ(OB.items)
           .Where(function(item) { return item.Flight == "2"; })
           .Select(function(item) { return item });
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
            str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. b2b.ITZ.com does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            //str1 += ' <div class"rgt" onclick="Close(\'' + this.rel + '_\');" >X</div>';
            $('#' + this.rel + '_').html(str1);
            //$('#' + this.rel + '_').show();
            $('#' + this.rel + '_').slideToggle();
            // $('#' + this.id).toggleClass("fltDetailslink1");
            $('#' + this).hide();
        }
    });


};

function Close(id) {
    $('#' + id).hide();
}


ResHelper.prototype.BagInfo = function(bag) {

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

ResHelper.prototype.TerminalAirportInfo = function(terminal, airport) {
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

ResHelper.prototype.GetFltDetailsR = function(result) {
    var e = this;

    $('.fltDetailslinkR').click(function(event) {

        if ($(this).attr("rel") != null) {
            var lineNums = $(this).attr("rel").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            //var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");

            // var airc = lineNums[1] + "_" + lineNums[2];

            var OB;
            if ($.trim(lineNums[2]) == "O") {
                OB = JSLINQ(result[0])
                                    .Where(function(item) { return item.LineNumber == lineNums[1]; })
                                    .Select(function(item) { return item });
            }
            else if ($.trim(lineNums[2]) == "R") {
                OB = JSLINQ(result[1])
                                 .Where(function(item) { return item.LineNumber == lineNums[1]; })
                                 .Select(function(item) { return item });
            }
            var O = JSLINQ(OB.items)
                             .Where(function(item) { return item.Flight == "1"; })
                             .Select(function(item) { return item });
            var R = JSLINQ(OB.items)
           .Where(function(item) { return item.Flight == "2"; })
           .Select(function(item) { return item });
            var str1 = '';
            str1 += '<div style="width: 60%; margin: 10% 0 0 20%; padding: 0%; background: #f9f9f9; box-shadow:0px 0px 15px #333;">';
            str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; right:3px;font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');">X</div>';
            str1 += '<div class="f16 bld colormn padding1 lft">Flight Details</div><div>';

            if (O.items.length > 0) {
                try {
                    if ((parseInt(O.items[0].AvailableSeats1) <= 5) && (O.items[0].ValiDatingCarrier != 'SG')) {

                        str1 += '<div class="colorwht lft" style="background:#004b91; padding:2px 5px; border-radius:4px; color:#fff; position:relative; top:6px;">' + O.items[0].AvailableSeats1 + ' Seat(s) Left!</div><div class="clear1"></div>';
                    }
                } catch (ex) { }

                str1 += '<div class="depcity">';
                for (var i = 0; i < O.items.length; i++) {
                    if (i == 0) {
                        str1 += '<div class="lft w24"><div><span class="f20">' + O.items[0].DepartureCityName + '-' + O.items[O.items.length - 1].ArrivalCityName + '</span>&nbsp;<span class="f16">' + e.MakeupTotDur(O.items[0].TotDur) + '</span></div><div class="clear1"></div>';
                    }
                    else {
                        str1 += '<div class="clear1"></div><hr /><div class="clear1"></div><div class="lft w24">';
                    }
                    str1 += '<div class="lft w50"><img alt="" src="' + UrlBase + 'AirLogo/sm' + O.items[i].MarketingCarrier + '.gif" /><br />' + O.items[i].MarketingCarrier + ' - ' + O.items[i].FlightIdentification + '</div>';
                    str1 += '<div class="lft w50 bld textaligncenter"><h1>' + e.calFlightDur(O.items[i].DepartureTime.replace(":", ""), O.items[i].ArrivalTime.replace(":", "")) + ' HRS</h1><img src="' + UrlBase + 'Images/duration.png" /></div></div>';
                    str1 += '<div class="lft w33" style="text-align:right;"><span>' + O.items[i].DepartureLocation + '&nbsp;' + [O.items[i].DepartureTime.replace(":", "").slice(0, 2), ":", O.items[i].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + O.items[i].DepartureCityName + '<br />' + O.items[i].Departure_Date + '<br />' + e.TerminalAirportInfo(O.items[i].DepartureTerminal, O.items[i].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w8 dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>';
                    str1 += '<div class="lft w33"><span>' + [O.items[i].ArrivalTime.replace(":", "").slice(0, 2), ":", O.items[i].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + O.items[i].ArrivalLocation + '</span><br />' + O.items[i].ArrivalCityName + '<br />' + O.items[i].Arrival_Date + '<br />' + e.TerminalAirportInfo(O.items[i].ArrivalTerminal, O.items[i].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }
            if (R.items.length > 0) {
                str1 += '</div><div class="depcity1"><span>' + R.items[0].DepartureCityName + '-' + R.items[R.items.length - 1].ArrivalCityName + '</span>&nbsp;' + R.items[0].TotDur + '<div class="clear"></div>';
                for (var j = 0; j < R.items.length; j++) {
                    str1 += '<div class="lft w24"><img alt="" src="' + UrlBase + 'AirLogo/sm' + R.items[j].MarketingCarrier + '.gif" /><br />' + R.items[j].MarketingCarrier + ' - ' + R.items[j].FlightIdentification + '</div>'
                    str1 += '<div class="lft w10 f20 bld textaligncenter"><h1>' + e.calFlightDur(R.items[j].DepartureTime.replace(":", ""), R.items[j].ArrivalTime.replace(":", "")) + ' HRS</h1><img src="' + UrlBase + 'Images/duration.png" /></div>'
                    str1 += '<div class="lft w24" style="text-align:right;"><span>' + R.items[j].DepartureLocation + '&nbsp;' + [R.items[j].DepartureTime.replace(":", "").slice(0, 2), ":", R.items[j].DepartureTime.replace(":", "").slice(2)].join('') + '</span><br />' + R.items[j].DepartureCityName + '<br />' + R.items[j].Departure_Date + '<br />' + e.TerminalAirportInfo(R.items[j].DepartureTerminal, R.items[j].DepartureAirportName) + '</div>';
                    str1 += '<div class="lft w10 dvsrc"><img src="' + UrlBase + 'Images/air.png"/></div>'
                    str1 += '<div class="lft w24"><span>' + [R.items[j].ArrivalTime.replace(":", "").slice(0, 2), ":", R.items[j].ArrivalTime.replace(":", "").slice(2)].join('') + '&nbsp;' + R.items[j].ArrivalLocation + '</span><br />' + R.items[j].ArrivalCityName + '<br />' + R.items[j].Arrival_Date + '<br />' + e.TerminalAirportInfo(R.items[j].ArrivalTerminal, R.items[j].ArrivalAirportName) + '</div><div class="clear"></div>';
                }
            }
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
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



    $('.fltBagDetailsR').click(function(event) {

        if ($(this).attr("rel") != null) {
            var lineNums = $(this).attr("rel").split('_');
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            // var idr = 'fltdtls_' + $(this).attr("rel");
            var idr = $(this).attr("rel");

            // var airc = lineNums[1] + "_" + lineNums[2];

            var OB;
            if ($.trim(lineNums[2]) == "O") {
                OB = JSLINQ(result[0])
                                    .Where(function(item) { return item.LineNumber == lineNums[1]; })
                                    .Select(function(item) { return item });
            }
            else if ($.trim(lineNums[2]) == "R") {
                OB = JSLINQ(result[1])
                                 .Where(function(item) { return item.LineNumber == lineNums[1]; })
                                 .Select(function(item) { return item });
            }
            var O = JSLINQ(OB.items)
                             .Where(function(item) { return item.Flight == "1"; })
                             .Select(function(item) { return item });
            var R = JSLINQ(OB.items)
           .Where(function(item) { return item.Flight == "2"; })
           .Select(function(item) { return item });
            var str1 = '';
            str1 += '<div style="width: 60%; margin: 10% 0 0 20%; padding: 0%; background: #f9f9f9; box-shadow:0px 0px 15px #333;">';
            str1 += '<div style="cursor:pointer; float:right; position:relative; top:2px; right:3px;font-size:20px" onclick="DiplayMsearch(' + $.trim(idr) + ');" title="Click to close Details">X</div>';
            str1 += '<div class="f20 bld colormn padding1 lft">Baggage Details</div>';

            if (O.items.length > 0) {
                str1 += '<div class="depcity">';
                str1 += '<table class="w100 f12"><tr><td class="f16 bld w50">Sector</td><td class="f16 bld">Baggage Quantity</td></tr>';
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
            //try {
            //    if (O.items.length > 0) {
            //        str1 += '<div class="lft colormn"><img src="../images/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed" /> <span class="t5">' + O.items[0].BagInfo + '</span></div>';
            //    }
            //} catch (ex) { }
            str1 += '<div class="padding1 f10 w95 mauto lh13">The information presented above is as obtained from the airline reservation system. b2b.ITZ.com does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</div>';
            str1 += '<div class="clear1"></div>';
            str1 += '</div></div>';

        }
        $('#' + idr).html(str1);
        $('#' + idr).fadeToggle(1000);
    });



};

ResHelper.prototype.GetResult = function(e) {
    var Obook = null;
    var Rbook = null;

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

    var q = UrlBase + "Domestic/FltResult1.aspx/Search_Flight1";

    $.ajax({
        url: q,
        type: "POST",
        
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function(e, t, n) {

            alert("Sorry, we could not find a match for the destination you have entered..Kindly modify your search.");
            window.location = UrlBase + 'Search.aspx';

            return false
        },
        success: function(data) {
            resultG = data.d;
            var result = "";
            if (resultG == "" || resultG == null) {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly modify your search.");
                $(document).ajaxStop($.unblockUI)
                window.location = UrlBase + 'Search.aspx';

            } else {
                if (resultG[0] == "" || resultG[0] == null) {
                    alert("Sorry, we could not find a match for the destination you have entered.Kindly modify your search.");
                    $(document).ajaxStop($.unblockUI)
                    window.location = UrlBase + 'Search.aspx';

                } else {

                    if (n == "D" && u == false && a == false && r == "rdbRoundTrip") {


                        t.GetResultR(resultG);
                        t.OnewayH.hide();
                        t.RoundTripH.show();
                        t.DivMatrixRtfO.html(t.GetMatrix(resultG[0], 'O'));
                        t.DivMatrixRtfR.html(t.GetMatrix(resultG[1], 'R'));


                        t.FltrSortR();
                        t.flterR.hide();
                        t.flterTab.show();
                        t.GetSelectedRoundtripFlight(resultG);
                        t.RTFFinalBook();
                        t.ShowFareBreakUp(resultG);
                        t.hdnOnewayOrRound.val("RoundTrip");





                    }
                    else {
                        t.hdnOnewayOrRound.val("OneWay");
                        t.flterR.hide();
                        t.DivResult.html(t.GetResultO(resultG));
                        t.Book(resultG);
                        t.GetFltDetails(resultG);
                        t.ShowFareBreakUp(resultG);
                        t.DivMatrix.html(t.GetMatrix(resultG[0], 'O'));
                        t.FltrSort(resultG);


                    }
                    t.GetFltDetailsR(resultG);
                    t.MainSF.show();
                    FlightHandler = new FlightResult();
                    FlightHandler.BindEvents();
                    $(document).ajaxStop($.unblockUI)
                }
            }

        }
    });
};

ResHelper.prototype.GetResultSplRTFTrip = function(e, type) {
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

    var q = UrlBase+"FltResult1.aspx/Search_Flight1";
    /// UrlBase + <reference path="../../Domestic/FltResult1.aspx" />

    $.ajax({
        url: q,
        type: "POST",
        data: JSON.stringify({
            obj: c
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function(e, t, n) {

            alert("Sorry, we could not find a match for the destination you have entered..Kindly mordify your search.");
            // window.location = UrlBase + 'Search.aspx';

            return false
        },
        success: function(data) {
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
                    t.DivMatrix.html(t.GetMatrix(resultRTFSp[0], 'O'));
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



ResHelper.prototype.GetResultR = function(resultArray) {

    var t = this;
    var result = '';
    //  result += '<div class="lft w50">';
    result += '<div  class="list w100">';
    if (resultArray[0].length > 0) {
        var LnOb = resultArray[0][resultArray[0].length - 1].LineNumber;
        for (var i = 1; i <= LnOb; i++) {
            //var OB = (from ct in Model.fltsearch1[0] where ct.LineNumber == i select ct).ToList();

            var OB = JSLINQ(resultArray[0])
                     .Where(function(item) { return item.LineNumber == i; })
                     .Select(function(item) { return item });
            var k = 0;
            var O = "O";
            var unds = "_";
            if (OB.items.length > 0) {
                result += '<div class="list-item resR">';

                // grid layout

                result += '<div id="main1_' + i + '_O" class="fltboxnew brdrred mrgbtmG">';
                if (t.CheckMultipleCarrier(OB.items) == true) {

                    result += '<div><div class="w33 lft"><img alt=""   src="' + UrlBase + 'Airlogo/multiple.png" title=">Multiple Carriers"  /></div>';
                    result += '<div class="rgt w66 textalignright f16">Multiple Carriers</div></div>';

                }
                else {

                    result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OB.items[0].MarketingCarrier + '.gif" style="height:20px;" title="' + OB.items[0].AirLineName + '" /></div>';
                    result += '<div class="rgt w66 f16 textalignright">' + OB.items[0].MarketingCarrier + ' - ' + OB.items[0].FlightIdentification + '</div></div>';
                }
                result += '<div class="clear1"><hr /></div>';
                result += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OB.items[0].DepartureTime) + '</span></div>';
                result += '<div class="rgt textalignright w50"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime) + '</span></div>';
                result += '<div class="clear1"></div>';
                if (t.DisplayPromotionalFare(OB.items[0]) == '') {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" /> &nbsp;</div>';
                }
                else {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" /> &nbsp;</div>';
                }
                result += '<div class="gridViewToolTip1 hide"  title="' + i + '_O" >ss</div>';
                result += '<span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + i + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /> &nbsp;</span><div class="fade" id="fltdtls_' + i + '_O" > &nbsp;</div>';
                result += '<div class="fltBagDetailsR lft" rel="fltdtls_' + i + '_O"><img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                result += '<div class="f20 rgt img5t bld colorp">' + OB.items[0].TotalFare + '</div>';
                result += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                //result += '<div class="clear1"><hr /></div>';
                //result += '<div class="w100"><span  class="f16 bld lft">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + '</span></div>';
                result += '<div class="clear"></div>';
                result += '<div><span class="rgt">' + OB.items[0].Stops + '</span></div>';
                result += '<div class="clear1"></div>';
                //result += '<div class="lft w50"><input type="checkbox" value="' + i.toString() + '"  rel="' + i.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                result += '<div class="rgt w50 textalignright"><input type="radio"  class="rgt" name="RO" value="' + i + '" /></div>';
                //result += '<div class="clear"></div><div class="bld colorp italic">' + OB.items[0].AdtFareType + '</div>';
                if ((OB.items[0].ValiDatingCarrier == 'SG') && (($.trim(OB.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OB.items[0].Searchvalue).search("AP14") >= 0)))
                { result += '<div class="clear"></div><div class="bld colorp italic">Non Refundable</div>'; }
                else if (OB.items[0].Trip == 'I')
                { result += '<div class="clear"></div><div class="bld colorp italic">&nbsp;</div>'; }
                else {
                    result += '<div class="clear"></div><div class="bld colorp italic">' + OB.items[0].AdtFareType + '</div>';
                }
                result += '</div><div class="clear"></div>';



                result += '<div id="main_' + i + '_O" class="fltbox mrgbtm brdrred">';
                for (var obi = 0; obi < 1; obi++) { //OB.items.length; obi++) {
                    result += '<div class="w20 lft">';
                    result += '<div>';
                    if (t.CheckMultipleCarrier(OB.items) == true) {

                        result += '<img alt=""   src="' + UrlBase + 'Airlogo/multiple.png"/>';
                        result += '</div>';
                        result += '<div>Multiple Carriers</div>';
                        result += '<div class="airlineImage hide">' + OB.items[obi].AirLineName + '</div>';


                    }
                    else {

                        result += '<img alt=""  src="../Airlogo/sm' + OB.items[obi].MarketingCarrier + '.gif"/>';
                        result += '</div>';
                        result += '<div>' + OB.items[obi].MarketingCarrier + '-' + OB.items[obi].FlightIdentification + '</div>';
                        result += '<div class="airlineImage">' + OB.items[obi].AirLineName + '</div>';
                    }


                    result += '</div>';
                    result += '<div class="w20 lft">';
                    result += '<div class="f16">' + t.MakeupAdTime(OB.items[obi].DepartureTime) + '</div> ';
                    //  result += '<div>' + OB.items[obi].DepartureCityName + '</div>';
                    if (k == 0) {
                        result += '<div class="bld colorp italic" style="white-space:nowrap;">' + OB.items[obi].AdtFareType + '</div>';
                        //result += '<div><input type="checkbox"  value="' + OB.items[obi].LineNumber.toString() + '" rel="' + OB.items[obi].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a> </div>';
                    }
                    result += '</div>';
                    result += '<div class="w20 lft">';
                    result += '<div class="f16"> ' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime);
                    result += '<div class="arrtime hide">' + t.MakeupAdTime(OB.items[OB.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                    result += '</div>';
                    //  result += '<div>' + OB.items[obi].ArrivalCityName + '</div>';
                    result += '</div>';
                    result += '<div class="w20 lft">';
                    if (k == 0) {
                        result += '<div>' + t.MakeupTotDur(OB.items[obi].TotDur) + '</div>';
                        result += '<div class="totdur hide">' + t.MakeupTotDur(OB.items[obi].TotDur).replace(':', '') + '</div>';
                        result += '<div class="clear1"></div>';
                        if (t.DisplayPromotionalFare(OB.items[obi]) == '') {
                            result += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" /></div>';
                        }
                        else {
                            result += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" /></div>';
                        }
                        result += '<div class="gridViewToolTip1 hide" title="' + OB.items[obi].LineNumber + '_O" >ss</div>';
                        result += '<div class="fltDetailslinkR cursorpointer lft" rel="fltdtlsR_' + OB.items[obi].LineNumber + '_O" >&nbsp; <img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /> &nbsp;</div>';
                        result += '<span class="fade" id="fltdtlsR_' + OB.items[obi].LineNumber + '_O" >&nbsp; </span>';
                        result += '<div class="lft"   ><a href="#"  class="fltBagDetailsR" rel="fltdtlsR_' + OB.items[obi].LineNumber + '_O"> <img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></a></div>';
                        result += '<div class="clear"></div>';
                    }
                    result += '</div>';
                    if (k == 0) {
                        result += '<div class="w20 lft">';
                        result += '<div class="f20 rgt colorp">' + OB.items[obi].TotalFare + '</div>';
                        result += '<img src="' + UrlBase + 'Images/rsp.png"  class="rgt" style="position:relative; top:3px;" />';
                        result += '<div class="clear1"></div>';
                        result += '<div class="textalignright">(<span class="stops">' + OB.items[obi].Stops + '</span>)</div>';
                        for (var rf1 = 0; rf1 < OB.items.length; rf1++) {
                            result += '<div class="airstopO hide">' + $.trim(OB.items[rf1].Stops).toString().toLowerCase() + '_' + $.trim(OB.items[rf1].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                        }



                        result += '<div class="clear1"></div>';
                        result += '<div class="rgt">';
                        if (k == 0) {
                            result += '<input type="radio" name="O" value="' + OB.items[obi].LineNumber + '" />';
                            result += '<div class="deptime hide">' + t.MakeupAdTime(OB.items[obi].DepartureTime).replace(':', '') + '</div>';
                            result += '<div class="rfnd hide">' + OB.items[obi].AdtFareType + '</div> ';
                            result += '<div class="price hide">' + OB.items[obi].TotalFare + '</div>';
                        }
                        result += '</div>';
                        result += '</div>';
                        result += '<div class="clear"></div>';
                    }
                    k++;

                    result += '<div class="clear"></div>';
                }
                result += '</div>';
                result += '</div>';
            }
        }
    }
    result += '</div>';
    // result += '</div>';
    t.divFromR.html(result);
    var result1 = '';


    // result += '<div class="rgt w50">';
    result1 += '<div  class="listR w100">';
    if (resultArray.length > 1) {
        if (resultArray[1].length > 0) {
            //int LnOb = Model.fltsearch1[1][Model.fltsearch1[1].Count - 1].LineNumber;
            var LnOb = resultArray[1][resultArray[1].length - 1].LineNumber;
            for (var i = 1; i <= LnOb; i++) {
                //  var OR = (from ct in Model.fltsearch1[1] where ct.LineNumber == i select ct).ToList();
                var OR = JSLINQ(resultArray[1])
                    .Where(function(item) { return item.LineNumber == i; })
                    .Select(function(item) { return item });
                var k = 0;
                var R = "R";
                var unds = "_";
                if (OR.items.length > 0) {
                    result1 += '<div class="list-itemR">';
                    result1 += '<div id="main1_' + i + '_R" class="fltboxnew mrgbtmG">';
                    if (t.CheckMultipleCarrier(OR.items) == true) {
                        result1 += '<div><div class="w33 lft"><img alt="" src="' + UrlBase + 'Airlogo/multiple.png"  /></div>';
                        result1 += '<div class="rgt w66 textaligncenter f16">Multiple Carriers</div></div>';
                    }
                    else {
                        result1 += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OR.items[0].MarketingCarrier + '.gif"  title="' + OR.items[0].AirLineName + '" /></div>';
                        result1 += '<div class="rgt w66 textaligncenter f16">' + OR.items[0].MarketingCarrier + ' - ' + OR.items[0].FlightIdentification + '</div></div>';
                    }
                    result1 += '<div class="clear1"><hr /></div>';
                    result1 += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OR.items[0].DepartureTime) + '</span></div>';
                    result1 += '<div class="rgt w50 textalignright"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime) + '</span></div>';
                    result1 += '<div class="clear1"></div>';
                    if (t.DisplayPromotionalFare(OR.items[0]) == '') {
                        result1 += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" /> &nbsp;</div>';
                    }
                    else {
                        result1 += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" /> &nbsp;</div>';
                    }
                    result1 += '<div class="gridViewToolTip1 hide"  title="' + i + '_R" >ss</div>';
                    result1 += '<span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + i + '_R" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /> &nbsp;</span><div class="fade" id="fltdtls_' + i + '_R" > &nbsp;</div>';
                    result1 += '<div class="fltBagDetailsR lft" rel="fltdtls_' + i + '_R" ><img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                    result1 += '<div class="f20 rgt img5t bld colorp">' + OR.items[0].TotalFare + '</div>';
                    result1 += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                    //result1 += '<div class="clear1"><hr /></div>';
                    //result1 += '<div class="w100"><span  class="f16 bld lft">' + t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0] + '</span></div>';
                    result1 += '<div class="clear"></div>';
                    result1 += '<div><span class="rgt">' + OR.items[0].Stops + '</span></div>';   // <a href="#" id="' +i + 'Det" rel="' + i + '">Details</a></div>';
                    result1 += '<div class="clear1"></div>';
                    //result1 += '<div class="lft w50"><input type="checkbox" name="checkm" value="' + i.toString() + '"  rel="' + i.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                    result1 += '<div class="rgt w50 textalignright"> <input type="radio"  class="rgt" name="RR" value="' + i + '" /> </div>';
                    //result1 += '<div class="clear"></div><div class="bld colorp italic">' + OR.items[0].AdtFareType + '</div>';
                    if ((OR.items[0].ValiDatingCarrier == 'SG') && (($.trim(OR.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OR.items[0].Searchvalue).search("AP14") >= 0)))
                    { result1 += '<div class="clear"></div><div class="bld colorp italic">Non Refundable</div>'; }
                    else if (OR.items[0].Trip == 'I')
                    { result1 += '<div class="clear"></div><div class="bld colorp italic">&nbsp;</div>'; }
                    else {
                        result1 += '<div class="clear"></div><div class="bld colorp italic">' + OR.items[0].AdtFareType + '</div>';
                    }
                    result1 += '</div>';

                    result1 += '<div id="main_' + i + '_R" class="fltbox mrgbtm">';
                    for (var obr = 0; obr < 1; obr++) { //OR.items.length; obr++) {
                        result1 += '<div class="w20 lft">';
                        result1 += '<div>';

                        if (t.CheckMultipleCarrier(OR.items) == true) {
                            result1 += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png"/>';
                            result1 += '</div>';
                            result1 += '<div>Multiple Carriers</div>';
                            result1 += '<div class="airlineImage hide">' + OR.items[obr].AirLineName + '</div>';
                        }
                        else {

                            result1 += '<img alt="" src="../Airlogo/sm' + OR.items[obr].MarketingCarrier + '.gif"/>';
                            result1 += '</div>';
                            result1 += '<div>' + OR.items[obr].MarketingCarrier + '-' + OR.items[obr].FlightIdentification + '</div>';
                            result1 += '<div class="airlineImage">' + OR.items[obr].AirLineName + '</div>';
                        }

                        result1 += '</div>';
                        result1 += '<div class="w20 lft">';
                        result1 += '<div class="f16">' + t.MakeupAdTime(OR.items[obr].DepartureTime) + '</div>';
                        // result1 += '<div>' + OR.items[obr].DepartureCityName + '</div>';

                        if (k == 0) {
                            result1 += '<div class="bld colorp italic" style="white-space:nowrap;">' + OR.items[obr].AdtFareType + '</div>';
                            //result1 += '<div><input type="checkbox" name="checkm" value="' + OR.items[obr].LineNumber.toString() + '"  rel="' + OR.items[obr].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a> </div>';
                        }
                        result1 += '</div>';
                        result1 += '<div class="w20 lft">';
                        result1 += '<div class="f16"> ' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime) + '</div>';
                        result1 += '<div class="arrtime hide">' + t.MakeupAdTime(OR.items[OR.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                        // result1 += '<div>' + OR.items[obr].ArrivalCityName + '</div>';
                        result1 += '</div>';
                        result1 += '<div class="w20 lft">';
                        if (k == 0) {
                            result1 += '<div> ' + t.MakeupTotDur(OR.items[obr].TotDur) + '</div>';
                            result1 += '<div class="totdur hide">' + t.MakeupTotDur(OR.items[obr].TotDur).replace(':', '') + '</div>';
                            result1 += '<div class="clear1"></div>';
                            if (t.DisplayPromotionalFare(OR.items[obr]) == '') {
                                result1 += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" /> &nbsp;</div>';
                            }
                            else {
                                result1 += '<div class="gridViewToolTip lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" /> &nbsp;</div>';
                            }
                            result1 += '<div class="gridViewToolTip1 hide" title="' + OR.items[obr].LineNumber + '_R" >ss</div>';
                            result1 += '<div class="fltDetailslinkR cursorpointer lft" rel="fltdtlsR_' + OR.items[obr].LineNumber + '_R" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /> &nbsp;</div>';
                            result1 += '<div class="fade" id="fltdtlsR_' + OR.items[obr].LineNumber + '_R" >nbsp;</div>';
                            result1 += '<div class="fltBagDetailsR lft" rel="fltdtlsR_' + OR.items[obr].LineNumber + '_R"><img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';

                        }
                        result1 += '</div>';
                        if (k == 0) {
                            result1 += '<div class="w20 lft">';
                            result1 += '<div class="price f20 rgt colorp">' + OR.items[obr].TotalFare + '</div>';
                            result1 += '<img src="' + UrlBase + 'Images/rsp.png"  class="rgt" style="position:relative; top:3px;" />';
                            result1 += '<div class="clear1"></div>';
                            result1 += '<div class="textalignright">(<span class="stops">' + OR.items[obr].Stops + '</span>)</div>';
                            for (var rfo = 0; rfo < OR.items.length; rfo++) {
                                result1 += '<div class="airstopR hide">' + $.trim(OR.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OR.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                            }


                            result1 += '<div class="clear1"></div>';
                            result1 += '<div class="rgt">';
                            if (k == 0) {
                                result1 += '<input type="radio" name="R" value="' + OR.items[obr].LineNumber + '" />';
                                result1 += '<div class="deptime hide">' + t.MakeupAdTime(OR.items[obr].DepartureTime).replace(':', '') + '</div>';
                                result1 += '<div class="rfnd bld hide">' + OR.items[obr].AdtFareType + '</div>';
                            }
                            result1 += '</div>';
                            result1 += '</div>';
                            result1 += '<div class="clear"></div>';
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
    result1 += '</div>';
    //  result1 += '</div>';

    t.divToR.html(result1);
};


ResHelper.prototype.CheckMultipleCarrier = function(objFlt) {
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

ResHelper.prototype.GetResultO = function(resultArray) {
    var t = this;
    var result = '';
    result += '<div id="divFrom" class="list" style="width:100%;">';
    if (resultArray[0].length > 0) {
        var LnOb = resultArray[0][resultArray[0].length - 1].LineNumber;
        for (var i = 1; i <= LnOb; i++) {
            var OB = JSLINQ(resultArray[0])
                  .Where(function(item) { return item.LineNumber == i; })
                  .Select(function(item) { return item });
            var k = 0;
            var O1 = "O";
            var R1 = "R";
            var M1 = "M";
            var D1 = "D";
            var unds = "_";

            var OF;
            var RF;
            if (OB.items.length > 0) {
                result += '<div class="list-item resO">';


                OF = JSLINQ(OB.items)
               .Where(function(item) { return item.Flight == "1"; })
               .Select(function(item) { return item });

                var RF = JSLINQ(OB.items)
               .Where(function(item) { return item.Flight == "2"; })
               .Select(function(item) { return item });

                // for grid layout
                if (OF.items.length > 0) {
                    result += '<div class="fltboxnew">';
                    if (t.CheckMultipleCarrier(OF.items) == true) {

                        result += '<div><div class="w33 lft"><img alt=""   src="' + UrlBase + 'Airlogo/multiple.png"  /></div>';
                        result += '<div class="rgt w66 textalignright f16">Multiple Carriers</div></div>';

                    }
                    else {

                        result += '<div><div class="w33 lft"><img alt=""  src="../Airlogo/sm' + OF.items[0].MarketingCarrier + '.gif" title="' + OF.items[0].AirLineName + '"  /></div>';
                        result += '<div class="rgt w66 textalignright f16">' + OF.items[0].MarketingCarrier + ' - ' + OF.items[0].FlightIdentification + '</div></div>';
                    }
                    result += '<div class="clear1"><hr /></div>';
                    result += '<div class="lft w50"><span class="bld">Dep. </span><span class="f16">' + t.MakeupAdTime(OF.items[0].DepartureTime) + '</span></div>';
                    result += '<div class="rgt w50 textalignright"><span class="bld">Arr. </span><span class="f16">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime) + '</span></div>';
                    result += '<div class="clear1"></div>';


                    if (t.DisplayPromotionalFare(OF.items[0]) == '') {
                        result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" /> &nbsp;</div>';
                    }
                    else {
                        result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" /> &nbsp;</div>';
                    }

                    result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                    result += '<div><span class="fltDetailslinkR cursorpointer lft" rel="fltdtls_' + i + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /> &nbsp;</span><div class="fade" id="fltdtls_' + i + '_O" > &nbsp;</div></div>';
                    result += '<div class="fltBagDetailsR lft" rel="fltdtls_' + i + '_O"><img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></div>';
                    result += '<div class="f20 rgt img5t bld colorp">' + OF.items[0].TotalFare + '</div>';
                    result += '<div class="rgt t2"><img src="../images/rsp.png"/> </div>';
                    result += '<div class="clear1"></div>';
                    if (RF.items.length > 0) {
                        result += '<div class="w100"><span  class="lft bld colormn">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + "-" + t.hidtxtDepCity1.val().split(',')[0] + '</span><span class="rgt">' + OB.items[0].Stops + '</span></div>';
                    }
                    else {
                        result += '<div class="w100"><span class="lft bld colormn">' + t.hidtxtDepCity1.val().split(',')[0] + "-" + t.hidtxtArrCity1.val().split(',')[0] + '</span><span class="rgt">' + OB.items[0].Stops + '</span></div>';
                    }
                    result += '<div class="clear"></div>';
                    //result += '<div><span class="bld">' + OF.items[0].AdtFareType + '</span> |  <span class="fltDetailslinkR cursorpointer textunderline" rel="' + i + '_O" ><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Deails" style="height:20px;" /></span><span class="fade textunderline" id="fltdtls_' + i + '_O" >Details</span></div>'; //<a href="#" id="' + i + 'Det" rel="' + i + '">Details</a></div>';
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
                result += '<div class="w15 lft" >';
                result += '<div  class="logoimg">';
                if (t.CheckMultipleCarrier(OF.items) == true) {
                    result += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                    result += '</div>';
                    result += '<div>Multiple Carriers</div>';
                    result += '<div class="airlineImage hide">' + OF.items[0].AirLineName + '</div>';
                }
                else {
                    result += '<img alt="" src="../Airlogo/sm' + OF.items[0].MarketingCarrier + '.gif"/>';
                    result += '</div>';
                    result += '<div><span>' + OF.items[0].MarketingCarrier + '</span> - ' + OF.items[0].FlightIdentification + '</div>';
                    result += '<div class="airlineImage">' + OF.items[0].AirLineName + '</div>';
                }


                result += '</div>';
                result += '<div class="w18 lft">';
                result += '<div class="f20">' + t.MakeupAdTime(OF.items[0].DepartureTime) + '</div>';
                result += '<div>' + OF.items[0].Departure_Date + '</div>';
                result += '<div>' + OF.items[0].DepartureCityName + '</div>';
                result += '<div class="deptime hide">' + t.MakeupAdTime(OF.items[0].DepartureTime).replace(':', '') + '</div>';
                result += '</div>';
                result += '<div class="w18 lft">';
                result += '<div class="f20">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime) + '</div>';
                result += '<div>' + OF.items[OF.items.length - 1].Arrival_Date + '</div>';
                result += '<div>' + OF.items[OF.items.length - 1].ArrivalCityName + '</div>';
                result += '<div class="arrtime hide">' + t.MakeupAdTime(OF.items[OF.items.length - 1].ArrivalTime).replace(':', '') + '</div>';
                result += '</div>';
                result += '<div class="w10 lft">';
                result += '<div class="f20">' + t.MakeupTotDur(OF.items[0].TotDur) + ' &nbsp;</div>';
                result += '<div class="stops">' + OF.items[0].Stops + '</div>'
                result += '<div class="totdur hide">' + t.MakeupTotDur(OF.items[0].TotDur).replace(':', '') + '</div>';
                for (var rfo = 0; rfo < OF.items.length; rfo++) {
                    result += '<div class="airstopO hide">' + $.trim(OF.items[rfo].Stops).toString().toLowerCase() + '_' + $.trim(OF.items[rfo].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                }


                result += '</div>';
                //result += '<div class="w24 lft"><div class="rfnd bld colorp italic">' + OF.items[0].AdtFareType + '</div><div class="clear1"></div>';
                if ((OF.items[0].ValiDatingCarrier == 'SG') && (($.trim(OF.items[0].Searchvalue).search("AP7") >= 0) || ($.trim(OF.items[0].Searchvalue).search("AP14") >= 0)))
                { result += '<div class="w24 lft"><div class="rfnd bld colorp italic">Non Refundable</div><div class="clear1"></div>'; }
                else if (OF.items[0].Trip == 'I')
                { result += '<div class="w24 lft"><div class="rfnd bld colorp italic">&nbsp;</div><div class="clear1"></div>'; }
                else {
                    result += '<div class="w24 lft"><div class="rfnd bld colorp italic">' + OF.items[0].AdtFareType + '</div><div class="clear1"></div>';
                }
                if (t.DisplayPromotionalFare(OF.items[0]) == '') {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails.png" style="height:20px;" />&nbsp; </div>';
                }
                else {
                    result += '<div class="gridViewToolTip cursorpointer lft"><img src="' + UrlBase + 'images/icons/faredetails1.png" style="height:20px;" />&nbsp; </div>';
                }
                result += '<div class="gridViewToolTip1 hide"  title="' + OB.items[0].LineNumber + '_O" >ss</div>';
                result += '<div class="lft"><a href="#" id="' + OB.items[0].LineNumber + 'Det" rel="' + OB.items[0].LineNumber + '"  class="fltDetailslink"><img src="' + UrlBase + 'images/icons/information.png" class="cursorpointer" alt="Click to View Full Deails" title="Click to View Full Details" style="height:20px;" /></a></div>';
                result += '<div class="lft">&nbsp;<a href="#" id="' + OB.items[0].LineNumber + 'BagDet" rel="' + OB.items[0].LineNumber + '"  class="fltBagDetails"> <img src="../images/icons/baggage.png" style="height:20px;" alt="Baggages Allowed"  class="cursorpointer"title="Baggages Allowed"></a></div>';
                result += '</div>';
                result += '<div class="w15 rgt">';
                result += '<img src="' + UrlBase + 'Images/rsp.png" class="lft" style="margin-top:3px;" />';
                result += '<div class="price f20 lft colorp">' + OF.items[0].TotalFare + '</div>';
                result += '<div class="clear1"></div><div class="clear"></div>';
                //result += '<div class="lft"><input type="checkbox" name="checkm"  value="' + OB.items[0].LineNumber.toString() + '"  rel="' + OB.items[0].LineNumber.toString() + '"  class="CheckSub" /> <a href="#" class="emailbtn topopup"><img src="../images/msg.png"  /></a></div>';
                result += '<input type="button"  value="Book"  class="buttonfltbk lft" title="' + OB.items[0].LineNumber + '"  id="' + OB.items[0].LineNumber + '" />';
                if ((parseInt(OB.items[0].AvailableSeats1) <= 5) && (OB.items[0].ValiDatingCarrier != 'SG')) {
                    result += '<div class="clear"></div><span class="colorwht" style="background:#004b91; padding:2px 5px; border-radius:4px; color:#fff;">' + OB.items[0].AvailableSeats1 + ' Seat(s) Left!</span>';
                }
                result += '</div>';
                result += '<div class="clear1"></div>';
                result += ' ';
            }
            result += '</div>';
            result += '<div id="Return">';
            if (RF.items.length > 0) {
                result += '<hr class="w50 mauto" style="border:none; border-top:3px dotted #d1d1d1; margin-bottom:10px;" /> ';
                result += '<div class="w15 lft">'
                result += '<div>';
                if (t.CheckMultipleCarrier(RF.items) == true) {
                    result += '<img alt="" src="' + UrlBase + 'Airlogo/multiple.png" />';
                    result += '</div>';
                    result += '<div>Multiple Carriers</div>';
                    result += '<div class="airlineImage hide">' + RF.items[0].AirLineName + '</div>';
                }
                else {
                    result += '<img alt="" src="../Airlogo/sm' + RF.items[0].MarketingCarrier + '.gif"/>';
                    result += '</div>';
                    result += '<div><span>' + RF.items[0].MarketingCarrier + '</span> - ' + RF.items[0].FlightIdentification + '</div>';
                    result += '<div class="airlineImage">' + RF.items[0].AirLineName + '</div>';
                }


                result += '</div>';
                result += '<div class="w18 lft">';
                result += '<div class="f20">' + t.MakeupAdTime(RF.items[0].DepartureTime) + '</div>';
                result += '<div>' + RF.items[0].Departure_Date + '</div>';
                result += '<div>' + RF.items[0].DepartureCityName + '</div>';
                result += '</div>';
                result += '<div class="w18 lft">';
                result += '<div class="f20">' + t.MakeupAdTime(RF.items[RF.items.length - 1].ArrivalTime) + '</div>';
                result += '<div>' + RF.items[RF.items.length - 1].Arrival_Date + '</div>';
                result += '<div>' + RF.items[RF.items.length - 1].ArrivalCityName + '</div>';
                result += '</div>';
                result += '<div class="w10 lft">';
                result += '<div class="f20">' + t.MakeupTotDur(RF.items[0].TotDur) + ' &nbsp;</div>';
                result += '<div  class="stops">' + RF.items[0].Stops + '</div>';
                for (var rfi = 0; rfi < RF.items.length; rfi++) {
                    result += '<div class="airstopO hide">' + $.trim(RF.items[rfi].Stops).toString().toLowerCase() + '_' + $.trim(RF.items[rfi].AirLineName).toString().toLowerCase().replace(' ', '_') + '</div>';
                }
                result += '</div>    ';
                result += '<div class="clear1"></div>';
            }
            result += '</div>';
            result += '<div id="' + OB.items[0].LineNumber.toString() + '_" style="display:none;"></div>';
            result += '<div class="clear"></div>';
            result += '</div>';
            result += '</div>';
        }
    }
    result += '</div>';
    return result;
};

ResHelper.prototype.GetSelectedRoundtripFlight = function(resultArray) {
    var e = this;
    $("input:radio").click(function() {
        if (this.name == "O" || this.name == "RO") {
            //  Obook = null;// $.parseJSON($('#' + this.value).html());

            var lineNums = this.value;
            //var airItem = { "fltName":  content.fltName,"fltJson": content.fltsearch1 };
            ////var fltArray = JSLINQ(gdsJson)
            ////           .Where(function (item) { return item.fltName == lineNums[1]; })
            ////           .Select(function (item) { return item.fltJson; });

            var fltOneWayArray = JSLINQ(resultArray[0])
                      .Where(function(item) { return item.LineNumber == lineNums; })
                      .Select(function(item) { return item });

            Obook = fltOneWayArray.items;
            e.RtfFltSelectDivO.html(e.DisplaySelectedFlight(Obook));


            $('.list-item').each(function() {

                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");

            });

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
                      .Where(function(item) { return item.LineNumber == lineNums; })
                      .Select(function(item) { return item });
            Rbook = fltReturnArray.items;
            e.RtfFltSelectDivR.html(e.DisplaySelectedFlight(Rbook));
            $('.list-itemR').each(function() {

                $(this).find('.mrgbtm').removeClass("fltbox01").addClass("fltbox");
                $(this).find('.mrgbtmG').removeClass("fltbox02");
            });
            $('#main_' + lineNums + '_R').removeClass("fltbox").addClass("fltbox01");
            $('#main_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
            $('#main1_' + lineNums + '_R').addClass("fltbox02");
            $('#main1_' + lineNums + '_R').find("input[type='radio']").attr('checked', 'checked');
            e.RtfFltSelectDiv.css("display", "block");
        }

        if (Obook != null && Rbook != null) {
            e.RtfTotalPayDiv.html('<img src="' + UrlBase + 'images/rs.png" /> ' + parseInt(Obook[0].TotalFare + Rbook[0].TotalFare) + '/-<div class="clear1"></div>');
            e.RtfTotalPayDiv.css("display", "block");
            e.RtfBookBtn.css("display", "block");
        }

    });
};


ResHelper.prototype.DisplaySelectedFlight = function(objFlt) {
    var string;

    if (objFlt.length > 1) {

        var fltArray = new Array();

        for (var i = 0; i < objFlt.length; i++) {

            fltArray.push(objFlt[i].MarketingCarrier);
        }

        var fltArray1 = fltArray.unique();
        var img1 = "";
        if (fltArray1.length > 1) {
            img1 = '<div> <img src="../AirLogo/multiple.png"  /></div><div>Multiple Carrier</div>';
        }
        else {
            img1 = '<div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div> <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</div>';
        }
        string = '<div class="lft w18"> ' + img1 + '</div>';
        string += '<div class="lft w60" style="margin-left:2%;">' + objFlt[0].DepartureCityName + ' - ' + objFlt[objFlt.length - 1].ArrivalCityName + '<br />' + objFlt[0].DepartureTime + ' - ' + objFlt[objFlt.length - 1].ArrivalTime + '<br />' + objFlt[0].Departure_Date + '<img src="../images/duration.png" class="w10" />&nbsp;' + objFlt[0].TotDur + '</div>';
        string += '<div class="rgt w20 f16">INR ' + objFlt[0].TotalFare + '/-</div>';
    }
    else {
        string = '<div class="lft w18"><div> <img src="../AirLogo/sm' + objFlt[0].MarketingCarrier + '.gif"  /></div>  <div>' + objFlt[0].MarketingCarrier + '-' + objFlt[0].FlightIdentification + '</div></div>';
        string += '<div class="lft w60" style="margin-left:2%;">' + objFlt[0].DepartureCityName + ' - ' + objFlt[0].ArrivalCityName + '<br />' + objFlt[0].DepartureTime + ' - ' + objFlt[0].ArrivalTime + '<br />' + objFlt[0].Departure_Date + '<img src="../images/duration.png" class="w10" />&nbsp;' + objFlt[0].TotDur + '</div>';
        string += '<div class="rgt w20 f16">INR ' + objFlt[0].TotalFare + '/-</div>';
    }

    return string;
};
ResHelper.prototype.RTFFinalBook = function() {

    var e = this;
    e.RtfBookBtn.click(function() {

        if (Obook != null && Rbook != null) {
            $("#searchquery").hide();
            $("#div_Progress").show();
            $.blockUI({
                message: $("#waitMessage")
            });
            var rtfArray = new Array(Obook, Rbook);

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
                success: function(e) {

                    InsertedTID = e.d;
                    if (InsertedTID[0] == "0") {
                        alert("Selected fare has been changed.Please select another flight.");
                        $("#searchquery").show();
                        $(document).ajaxStop($.unblockUI);
                    } else {
                        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + InsertedTID;
                    }
                },
                error: function(e, t, n) {
                    alert(t)
                }
            });



        }


    });
};

ResHelper.prototype.FltrSortR = function() {
    this.GetStopFilter(resultG[0], 'stops', 'O');
    this.GetUniqueAirline(resultG[0], 'airlineImage', 'O');
    var mPr = this.GetMinMaxPrice(resultG[0]);
    var mT = this.GetMinMaxTime(resultG[0]);
    var e = this;
    //if (mPr[0] <= mPr[1] + 100) {
    //    mPr[1] = mPr[1] + 100;
    //}
    e.MainSF.jplist({
        debug: false,
        itemsBox: '.list'
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
                    ui_slider: function($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                           , max: mPr[1]
                           , range: true
                           , values: [mPr[0], mPr[1]]
                           , slide: function(event, ui) {
                               $prev.text(ui.values[0]);
                               $next.text(ui.values[1]);
                           }
                        });
                    }

                   , set_values: function($slider, $prev, $next) {

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
							    ui_slider: function($slider, $prev, $next) {

							        $slider.slider({
							            min: mT[0]
										, max: mT[1]
										, range: true
										, values: [mT[0], mT[1]]
										, slide: function(event, ui) {
										    $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
										    $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
										}
							        });
							    }

								, set_values: function($slider, $prev, $next) {

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

        }
    });




    /// for return filter

    this.GetStopFilter(resultG[1], 'stops', 'R');
    this.GetUniqueAirline(resultG[1], 'airlineImage', 'R');
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
                    ui_slider: function($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                           , max: mPr[1] + 1
                           , range: true
                           , values: [mPr[0], mPr[1] + 1]
                           , slide: function(event, ui) {
                               $prev.text(ui.values[0]);
                               $next.text(ui.values[1]);
                           }
                        });
                    }

                   , set_values: function($slider, $prev, $next) {

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
							    ui_slider: function($slider, $prev, $next) {

							        $slider.slider({
							            min: mT[0]
										, max: mT[1]
										, range: true
										, values: [mT[0], mT[1]]
										, slide: function(event, ui) {
										    $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
										    $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
										}
							        });
							    }

								, set_values: function($slider, $prev, $next) {

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

        }
    });






};

ResHelper.prototype.FltrSort = function(result) {
    this.GetStopFilter(result[0], 'stops', 'O');
    this.GetUniqueAirline(result[0], 'airlineImage', 'O');
    var mPr = this.GetMinMaxPrice(result[0]);
    var mT = this.GetMinMaxTime(result[0]);
    var e = this;

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
                    ui_slider: function($slider, $prev, $next) {

                        $slider.slider({
                            min: mPr[0]
                           , max: mPr[1]
                           , range: true
                           , values: [mPr[0], mPr[1]]
                           , slide: function(event, ui) {
                               $prev.text(ui.values[0]);
                               $next.text(ui.values[1]);
                           }
                        });
                    }

                   , set_values: function($slider, $prev, $next) {

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
							    ui_slider: function($slider, $prev, $next) {

							        $slider.slider({
							            min: mT[0]
										, max: mT[1]
										, range: true
										, values: [mT[0], mT[1]]
										, slide: function(event, ui) {
										    $prev.text(e.getFourDigitTime(ui.values[0]) + " Hrs");
										    $next.text(e.getFourDigitTime(ui.values[1]) + " Hrs");
										}
							        });
							    }

								, set_values: function($slider, $prev, $next) {

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

        }
    });



};

//function mtrxshhd() {
//    $(".matrix").toggelClass("hide");
//}