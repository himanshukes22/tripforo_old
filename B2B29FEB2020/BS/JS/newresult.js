
var nHandler;
var Original_Fare = 0; var title;
var ta_Netfare = 0; var ta_Totfare = 0;
var ta_totComm = 0; var ta_totTds = 0;
var admrkpAmt = 0; var agmrkpAmt = 0;
var totSeat = ""; var totseatFare = ""; var rssss = new Array();
var tttravelaer = new Array(); var tttravelaer1 = new Array(); var ttService = new Array();
var ttService1 = new Array(); var O_S_Result = ""; var R_S_Result = ""; var TserviceId = 0; var seatlrts;
var rel; var original_Fare = "";
var Imgnext = "http://" + window.location.host + UrlBase + "BS/Images/next.png";
var Imgnext1 = "http://" + window.location.host + UrlBase + "BS/Images/next1.png";
var ImgUP = "http://" + window.location.host + UrlBase + "BS/Images/UP.png";
var ImgDU = "http://" + window.location.host + UrlBase + "BS/Images/DU.png";
var imDown = "<img style='margin-top:6px;margin-right:2px;' class='lft' src='" + ImgDU + "' />";
$(document).ready(function () {
    nHandler = new newSearchHelper();
    nHandler.BindEvents();
    $("#matrix").hide(); $("#setmodifydiv").hide();
    // FunShowhideArrow();
});
var newSearchHelper = function () {
    this.CityId;
    this.CityName;
    this.DestCityId;
    this.DestCityName;
    this.JourneyDate;
    this.PAX;
    this.Type;
    this.TripType;
    this.ReturnDate;
    this.Jresult;
    this.demo = $("#demo");
    this.RDetails = $(".RDetails");
    this.ODetails = $(".ODetails");
    this.RSelect = $(".RSelect");
    this.OSelect = $(".OSelect");
    this.DtlS;
    this.StLs;
    this.totPrice;
    this.showdata = $("#showdata");
    this.modifySearch = $("#ModifySearch");
    this.BookTrip = $("#BookTrip");
    this.canpolicy = $(".canpolicy");
    this.canpolicyty;

}
newSearchHelper.prototype.BindEvents = function () {
    var h = this;
    var query = h.getquerystring();
    h.CityId = query.trim().split('&')[0].split('=')[1].replace(/%20/g, " ");
    h.CityName = query.trim().split('&')[1].split('=')[1].replace(/%20/g, " ").toProperCase();
    h.DestCityId = query.trim().split('&')[2].split('=')[1].replace(/%20/g, " ");
    h.DestCityName = query.trim().split('&')[3].split('=')[1].replace(/%20/g, " ").toProperCase();
    h.JourneyDate = query.trim().split('&')[4].split('=')[1].replace(/%20/g, " ");
    h.PAX = query.trim().split('&')[5].split('=')[1].replace(/%20/g, " ");
    h.Type = query.trim().split('&')[6].split('=')[1].replace(/%20/g, " ");
    h.TripType = query.trim().split('&')[7].split('=')[1].replace(/%20/g, " ");
    h.ReturnDate = query.trim().split('&')[8].split('=')[1].replace(/%20/g, " ");
    h.getAvalaibility(h);
    h.modifySearch.click(function () {
        $("#clspngclose").show();
        $("#txtsrc").val(h.CityName);
        $("#txthidsrc").val(h.CityId);
        $("#txtdest").val(h.DestCityName);
        $("#ddlpax").val(h.PAX)
        $("#txthiddest").val(h.DestCityId);
        $("#ddlseat").val(h.Type);
        if (h.TripType == "oneway") {
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
        h.UpdateDatemodifyDate(h.JourneyDate, "");
        h.UpdateDatemodifyDateR(h.ReturnDate, "");
        $("#setmodifydiv").show();
    });
    $(document).on("mouseover", ".gridViewToolTip", function (e) {
        var BuffImg = "<div style='padding:20px;'><img  src='" + UrlBase + "Images/loadingAnim.gif' class='loader' align='left'/></div>";
        var th = this; var ress = "";
        $(th).next().html(BuffImg);
        var condition = th.id.trim().substring(0, 1);
        if (condition == "B") {
            ress = h.Boarding(h, th.id);
            $("#" + th.id.trim().replace("Board", "Boardd")).show();
        }
        else if (condition == "D") {
            ress = h.Dropping(h, th.id);
            $("#" + th.id.trim().replace("Drop", "Dropp")).show();
        }
        else if (condition == "C") {
            ress = h.Canpolicy(h, th.id);
            $("#" + th.id.trim().replace("Can", "Cann")).show();
        }

        $(th).next().html(ress);
        $("#" + th.id).tooltip({
            track: true,
            delay: 0,
            showURL: false,
            fade: 100,
            bodyHandler: function () {
                return $($(th).next().html());
            },
            showURL: false
        });
    });
    $(document).on("mouseout", ".gridViewToolTip", function (e) {
        var condition = this.id.trim().substring(0, 1);
        if (condition == "B")
            $("#" + this.id.trim().replace("Board", "Boardd")).hide();
        else if (condition == "D")
            $("#" + this.id.trim().replace("Drop", "Dropp")).hide();
        else if (condition == "C")
            $("#" + this.id.trim().replace("Can", "Cann")).hide();
    });
    $(document).on("mouseover", ".gridViewToolTip1", function (e) {
        $("#" + this.id.trim()).show();
    });
    $(document).on("mouseout", ".gridViewToolTip1", function (e) {
        $("#" + this.id.trim()).hide();
    });
    $(document).on("click", "[data-control-name='sortMinFare'],[data-control-name='sortDept'],[data-control-name='sortArrival']", function (e) {

        if ($(".list1 .list-item").length != 1 && $(".list1 .list-item").length != 0 && $(".noRes1")[0].className == "noRes1 boxshadow bgf1 jplist-hidden") {
            if ($(this).attr("data-control-name") == "sortMinFare") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortDept']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortArrival']").find("img")[0].src = ImgDU;
            }
            else if ($(this).attr("data-control-name") == "sortDept") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortMinFare']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortArrival']").find("img")[0].src = ImgDU;
            }
            else if ($(this).attr("data-control-name") == "sortArrival") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortMinFare']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortDept']").find("img")[0].src = ImgDU;
            }
        }
    });
    $(document).on("click", "[data-control-name='sortMinFare1'],[data-control-name='sortDept1'],[data-control-name='sortArrival1']", function (e) {
        if ($(".list2 .list-item").length != 1 && $(".list2 .list-item").length != 0 && $(".noRes2")[0].className == "noRes2 boxshadow bgf1 jplist-hidden") {
            if ($(this).attr("data-control-name") == "sortMinFare1") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortDept1']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortArrival1']").find("img")[0].src = ImgDU;
            }
            else if ($(this).attr("data-control-name") == "sortDept1") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortMinFare1']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortArrival1']").find("img")[0].src = ImgDU;
            }
            else if ($(this).attr("data-control-name") == "sortArrival1") {
                if (ImgUP != $(this).find("img")[0].src)
                    $(this).find("img")[0].src = ImgUP;
                else
                    $(this).find("img")[0].src = ImgDU;
                $("[data-control-name='sortMinFare1']").find("img")[0].src = ImgDU;
                $("[data-control-name='sortDept1']").find("img")[0].src = ImgDU;
            }
        }
    });

    $(document).on("mouseover", "[data-control-name='sortMinFare'],[data-control-name='sortDept'],[data-control-name='sortArrival']", function (e) {
        $(this).toggleClass("toggleclassS");
    });
    $(document).on("mouseout", "[data-control-name='sortMinFare'],[data-control-name='sortDept'],[data-control-name='sortArrival']", function (e) {
        $(this).toggleClass("toggleclassS");
    });
    $(document).on("mouseover", "[data-control-name='sortMinFare1'],[data-control-name='sortDept1'],[data-control-name='sortArrival1']", function (e) {
        $(this).toggleClass("toggleclassS");
    });
    $(document).on("mouseout", "[data-control-name='sortMinFare1'],[data-control-name='sortDept1'],[data-control-name='sortArrival1']", function (e) {
        $(this).toggleClass("toggleclassS");
    });
    $(document).on("mouseover", ".gridViewToolTip1", function (e) {
        $("#" + this.id.trim()).show();
    });
    $(document).on("mouseout", ".gridViewToolTip1", function (e) {
        $("#" + this.id.trim()).hide();
    });
    $(document).on("click", ".ODetails", function (e) {
        h.DtlS = "";
        h.DtlS = this.id;
        var ODLayOut = "";
        ODLayOut = getDetailsLayout();
        $("#forDetails").html(ODLayOut);
        h.getCancelationPolicy(h);
        $('#forDetails').bPopup({
            speed: 650,
            transition: 'slideIn'
        });
    });
    $(document).on("click", ".list-view", function (e) {
        $("#oneWayDiv").show();
        $("#matrix").hide();
        $(".list-view")[0].style.backgroundImage = "url('../Images/list-btn.png')";
        $(".grid-view")[0].style.backgroundImage = "url('../Images/grid-btn-disabled.png')";
        if (h.TripType == "return")
            $("#RoundDivs").show();
        else
            $("#RoundDivs").hide();

    });
    $(document).on("click", ".grid-view", function (e) {
        $("#oneWayDiv").show();
        $(".grid-view")[0].style.backgroundImage = "url('../Images/grid-btn.png')";
        $(".list-view")[0].style.backgroundImage = "url('../Images/list-btn-disabled.png')";
        if (h.TripType == "return")
            $("#RoundDivs").show();
        else
            $("#RoundDivs").hide();
        $("#matrix").hide();
    });
    $(document).on("mouseout", ".brkdtls", function (e) {
        $("#" + this.id.replace("breakup", "farebrk")).hide();
    });
    $(document).on("mouseover", ".brkdtls", function (e) {

        var ddf = h.farebrkshow(h, this.id);
        $(".brkdtls").tooltip({
            track: true,
            delay: 0,
            showURL: false,
            fade: 100,
            bodyHandler: function () {
                return $($(th).next().html());
            },
            showURL: false

        });
        $("#" + this.id.replace("breakup", "farebrk")).show();
        $("#" + this.id.replace("breakup", "farebrk")).html("" + ddf + "");

    });
    $(document).on("click", ".RDetails", function (e) {
        h.DtlS = "";
        h.DtlS = this.id;
        var RDLayOut = "";
        RDLayOut = getDetailsLayout();
        $("#forDetails").html(RDLayOut);
        h.getCancelationPolicy(h);
        $('#forDetails').bPopup({
            speed: 650,
            transition: 'slideIn'
        });
    });

    $(document).on("click", ".OSelect", function (e) {
        h.StLs = "";
        h.StLs = this.id;
        // $("#" + this.id).hide();
        $("#select_OO_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).show();
        $("#select_OO_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).removeClass("HideBuffImg");
        $("#select_OO_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).addClass("ShowBuffImg");
        var seatLayOut = "";
        $("#divseat").html("");
        seatLayOut = h.getTripDetails(h);
    });
    $(document).on("click", ".RSelect", function (e) {
        h.StLs = "";
        h.StLs = this.id;
        var RseatLayOut = "";
        $("#divseat").html("");
        RseatLayOut = h.getTripDetails(h);
        $("#" + this.id).hide();
        $("#select_RR_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).show();
        $("#select_RR_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).removeClass("HideBuffImg");
        $("#select_RR_" + this.id.split('_')[2] + "_" + this.id.split('_')[3]).addClass("ShowBuffImg");
    });
    $(document).on("click", ".removeDetailsCss", function (e) {
        var id = "";
        id = this.id
        if (id == "clPolicy") {
            $("#clPolicy")[0].className = "";
            $("#clPolicy").addClass("tripbutton1 removeDetailsCss");
            $("#FrBreakup")[0].className = "";
            $("#FrBreakup")[0].className = "tripbutton2 removeDetailsCss";
            $("#BrdPoints")[0].className = "";
            $("#BrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            $("#DrdPoints")[0].className = "";
            $("#DrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            h.getCancelationPolicy(h);
        }
        else if (id == "FrBreakup") {
            $("#FrBreakup")[0].className = "";
            $("#FrBreakup").addClass("tripbutton1 removeDetailsCss");
            $("#clPolicy")[0].className = "";
            $("#clPolicy")[0].className = "tripbutton2 removeDetailsCss";
            $("#BrdPoints")[0].className = "";
            $("#BrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            $("#DrdPoints")[0].className = "";
            $("#DrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            h.FareBreakUp(h, id);
        }
        else if (id == "BrdPoints") {
            $("#BrdPoints")[0].className = "";
            $("#BrdPoints").addClass("tripbutton1 removeDetailsCss");
            $("#clPolicy")[0].className = "";
            $("#clPolicy")[0].className = "tripbutton2 removeDetailsCss";
            $("#FrBreakup")[0].className = "";
            $("#FrBreakup")[0].className = "tripbutton2 removeDetailsCss";
            $("#DrdPoints")[0].className = "";
            $("#DrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            h.BoardingPoints(h, id);
        }
        else if (id == "DrdPoints") {
            $("#DrdPoints")[0].className = "";
            $("#DrdPoints").addClass("tripbutton1 removeDetailsCss");
            $("#clPolicy")[0].className = "";
            $("#clPolicy")[0].className = "tripbutton2 removeDetailsCss";
            $("#FrBreakup")[0].className = "";
            $("#FrBreakup")[0].className = "tripbutton2 removeDetailsCss";
            $("#BrdPoints")[0].className = "";
            $("#BrdPoints")[0].className = "tripbutton2 removeDetailsCss";
            h.DroppingPoints(h, id);
        }
    });
    $(document).on("click", ".matrix-view", function (e) {
        var divmatrixlayout = ""; var divmatrixlayout1 = ""; var divmatrixlayout2 = ""; var totlist = new Array();
        totlist = rssss;
        for (var tot = 0; tot < totlist.length; tot++) {
            if (tot == 0) { // for oneWay
                divmatrixlayout += "<div class='prvnxt'><div id='prev'>prev</div><div id='next'>next</div></div>";

                divmatrixlayout += "<div class='steps1'>";
                divmatrixlayout += "<div class='serviceHead1'><div>Bus Operator</div> <img src='" + UrlBase + "BS/Images/arrowD.png'/></div>";
                divmatrixlayout += "<div class='serviceHead'><div>Service Type </div><img src='" + UrlBase + "BS/Images/arrow.png'/></div>";
                for (var tots = 0; tots < totlist[tot].length; tots++) {
                    divmatrixlayout += "<div  class='stepcarousels cc'>";
                    for (var totes = 0; totes < totlist[tot].length; totes++) {
                        if (tots == 0) {
                            divmatrixlayout1 += "<div id='" + tots + "," + totes + "'  class='stepcarouselss cc1'>" + setMatrixLayout(totlist[tot][totes].traveler, totlist[tot][totes].serviceType, tots, totes, totlist[tot]) + "</div>";
                            divmatrixlayout += "<div id='" + tots + "," + totes + "'  class='stepss1 cc1'>" + totlist[tot][totes].serviceType + "</div>";

                            divmatrixlayout2 += "<div id='" + tots + "," + totes + "'  class='stepss2'>" + totlist[tot][totes].traveler + "</div>";
                        }
                        else {
                            divmatrixlayout += "<div id='" + tots + "_" + totes + "'  class='stepcarouselss cc1'>" + setMatrixLayout(totlist[tot][totes].traveler, totlist[tot][totes].serviceType, tots, totes, totlist[tot]) + "</div>";
                        }
                    }
                    divmatrixlayout += "</div>";
                    if (tots == 0) {
                        divmatrixlayout += "<div  class='stepcarousels3'>" + divmatrixlayout2 + "</div>";
                        divmatrixlayout += "<div class='stepcarousels'>" + divmatrixlayout1 + "</div>";
                    }
                }
                divmatrixlayout += "<div class='clear'></div>";
                divmatrixlayout += "</div>";
            }
            else {
            }
        }
        $("#oneWayDiv").hide();
        $("#RoundDivs").hide();
        $("#matrix").show();
        $("#matrix").html(divmatrixlayout);
        var divs = $('.stepcarousels');
        for (var cc = 0; cc < divs.length; cc++) {
            var now = 0; // currently shown div        
            for (var d1 = 0; d1 < $(divs[cc]).find(".cc1").length; d1++) {
                if (d1 < 4) {
                    $(divs[cc]).find(".cc1").eq(d1).show()
                }
                else {
                    $(divs[cc]).find(".cc1").eq(d1).hide()
                }
            }
        }
        var kk = 0;
        $("div[id=next]").click(function () {
            if ($(divs[0]).find(".cc1").length - 4 < kk)
                kk = 0;
            else
                kk = kk + 4;

            for (eqq = 0; eqq < divs.length; eqq++) {
                $(divs[eqq]).find(".cc1").hide();

                for (var d1 = kk; d1 < parseInt(kk + 4) ; d1++) {
                    $(divs[eqq]).find(".cc1").eq(d1).show();
                }
            }
        });
        $("div[id=prev]").click(function () {
            if (kk != 0) {
                if ($(divs[0]).find(".cc1").length - 4 < kk)
                    kk = 0;
                else
                    kk = kk;

                for (eqq = 0; eqq < divs.length; eqq++) {
                    $(divs[eqq]).find(".cc1").hide();

                    for (var d1 = parseInt(kk - 4) ; d1 < parseInt(kk) ; d1++) {
                        $(divs[eqq]).find(".cc1").eq(d1).show();
                    }
                }
            }
        });
    });
    $(document).on("click", ".minFarerrrr", function (e) {
        if (this.id.substring(this.id.length - 1) == "1")
            fun_SeatSort1($(".totdata1"), $(".frscls1"), "", "1");
        else
            fun_SeatSort1($(".totdata2"), $(".farCls2"), "", "2");
    });

    $(document).on("click", "#BookTrip", function (e) {
        var OSResult = O_S_Result;
        var RSResult = R_S_Result
        h.insertSelected_SeatDetails(OSResult, RSResult);
    });
    $(document).on("click", ".jplist-reset-btn", function (e) {
        $(".totdata1").show();
        $(".totdata2").show();
        $(".list-view")[0].style.backgroundImage = "url('../Images/list-btn.png')";
        $(".grid-view")[0].style.backgroundImage = "url('../Images/grid-btn-disabled.png')";
        FunShowhideArrow();
        if (h.TripType == "return") {
            $("[data-control-name='sortMinFare']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortArrival']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortDept']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortMinFare1']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortArrival1']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortDept1']").find("img")[0].src = ImgDU;
        }
        else {
            $("[data-control-name='sortMinFare']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortArrival']").find("img")[0].src = ImgDU;
            $("[data-control-name='sortDept']").find("img")[0].src = ImgDU;
        }
    });
}
newSearchHelper.prototype.getquerystring = function () {
    return window.location.search.substring(1);
}
newSearchHelper.prototype.getAvalaibility = function (b) {
    var redBusBuffImage = "<img  src='" + UrlBase + "BS/Images/loading_bar.gif' class='loader' align='left'/>";
    var redBusImage = "BS/Images/busimage.jpg";
    var Url = UrlBase + "BS/WebService/CommonService.asmx/getJourneyResult";
    var jrneyDate = new Date(b.JourneyDate.replace(/%20/g, " ").replace(/\-/g, "/"));
    var RjrneyDate = new Date(b.ReturnDate.replace(/%20/g, " ").replace(/\-/g, "/"));
    var dayName = new Array("Sunday", "Monday", "TuesDay", "WednesDay", "ThursDay", "FriDay", "SaturDay");
    var month = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var Omonth = month[jrneyDate.getMonth()].toUpperCase();
    if (b.ReturnDate == "")
        var Rmonth = month[jrneyDate.getMonth()].toUpperCase();
    else
        var Rmonth = month[RjrneyDate.getMonth()].toUpperCase();
    $('#divloading').show();
    $('#page-content').hide();
    var msgsss = "";
    if (b.TripType == "return") {
        $("#onewAySrc").show(); $("#ReturnwAySrc").show();
        msgsss = "From " + b.CityName + " To " + b.DestCityName + " Travel On " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Omonth + " " + jrneyDate.getFullYear() + "<br/>";
        $("#onewAySrc").html(b.CityName.split(' ')[0] + " - " + b.DestCityName.split(' ')[0]);
        msgsss += "From " + b.DestCityName + " To " + b.CityName + " Travel On " + dayName[RjrneyDate.getDay()] + " " + RjrneyDate.getDate() + " " + Rmonth + " " + RjrneyDate.getFullYear() + "<br/>";
        $("#ReturnwAySrc").html(b.DestCityName.split(' ')[0] + " - " + b.CityName.split(' ')[0]);
    } else {
        $("#ReturnwAySrc").hide();
        msgsss = "From " + b.CityName + " To " + b.DestCityName + " Travel On " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Omonth + " " + jrneyDate.getFullYear() + "<br/>";
        $("#onewAySrc").html(b.CityName.split(' ')[0] + " - " + b.DestCityName.split(' ')[0]);
        $("#onewAySrc").hide();
    }

    $('#source').html(msgsss);
    $("#matrix").hide();
    $("#divloading").show();
    $.ajax({
        url: Url,
        data: "{'src':'" + b.CityName + "','dest':'" + b.DestCityName + "','jDate':'" + b.JourneyDate + "','noofpax':'" + b.PAX + "','seattype':'" + b.Type + "','ReturnDate':'" + b.ReturnDate + "','TripType':'" + b.TripType + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        asnyc: true,
        success: function (data) {


            h.response = data.d;
            rssss = new Array();
            rssss = data.d;
            var AvlLayOut = "";
            var AvlLayOutHeader1 = "";
            var AvlLayOutHeader2 = "";
            var AvlLayOut1 = "";
            var AvlLayOut2 = "";
            var AfareLayOut1 = "";
            var AfareLayOut2 = "";
            var mSelectedResult = "";

            mSelectedResult += "<div id='Showonewaytrip' class='Showonewaytrip'></div><div class='clear'></div><div id='ShowReturntrip'></div><div class='bgda padding1s cursorpointer boxshadow borderradius colorwhite bld w20 btn rgt textaligncenter ' id='BookTrip'>Book</div><div class='clear'></div>";
            mSelectedResult += "<div class=clear></div>";
            $("#mSelectedResult").html(mSelectedResult);
            tttravelaer = new Array(); ttService = new Array();
            //for (var i = 0; i < h.response.length; i++) {  
            for (var i = 0; i < 1; i++) {
                tttravelaer1 = new Array(); ttService1 = new Array();
                if (i == 0) {
                    if (b.TripType == "return") {
                        $("#rdatehide").show(); $("#returnDate").show(); $("#RoundDivs").show(); $("#RoundDivs").addClass("resultDivone"); $("#oneWayDiv").addClass("w51 lft");
                        AvlLayOut1 += "<div  class='list list1 box text-shadow' id='listoneWay'>";
                        AvlLayOut2 += "<div  class='list list2 box text-shadow' id='listReturn'>";
                        AvlLayOutHeader2 += "<div class='w48 padding1s f14 lft bgb lh30 bld colorwhite'>" + b.CityName.split(' ')[0] + "-" + b.DestCityName.split(' ')[0] + " On  " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Omonth + " " + jrneyDate.getFullYear() + "</div>";
                        AvlLayOutHeader2 += "<div class='w48 padding1s f14 lft bgb lh30 bld colorwhite'>" + b.DestCityName.split(' ')[0] + "-" + b.CityName.split(' ')[0] + " On  " + dayName[RjrneyDate.getDay()] + " " + RjrneyDate.getDate() + " " + Rmonth + " " + RjrneyDate.getFullYear() + "</div>";
                        AvlLayOutHeader2 += "<div class='clear'></div>";
                        AvlLayOutHeader2 += "<div class='w50 boxshadow lft bgf1 lh30 bld hideheader jplist-panel'><div class='hidden' data-control-type='default-sort' data-control-name='sort' data-control-action='sort'data-path='.frscls1' data-order='desc' data-type='number'></div><div class='w33 padding1s lft'></div><div class='block w60 rgt'><div class='hidden w33 lft cursorpointer' data-control-type='sortDept' data-control-name='sortDept' data-control-action='sort' data-path='.Onedept' data-order='desc' data-type='number' title='Sort by departure time'> " + imDown + "Dep.</div><div class='hidden w33 lft cursorpointer' data-control-type='sortArrival' data-control-name='sortArrival' data-control-action='sort' data-path='.OnedArr' data-order='desc' data-type='number' title='Sort by Arrival time'> " + imDown + "Arr.</div><div class='hidden rgt cursorpointer padding1s' data-control-type='sortMinFare' data-control-name='sortMinFare' data-control-action='sort' data-path='.frscls1' data-order='desc' data-type='number' title='Sort by price'> " + imDown + "Min Fare</div></div></div>";
                        AvlLayOutHeader2 += "<div class='w50 boxshadow rgt bgf1 lh30 bld hideheader jplist-panel'><div class='hidden' data-control-type='default-sort1' data-control-name='sort' data-control-action='sort'data-path='.farCls2' data-order='desc' data-type='number'></div><div class='w33 padding1s lft'></div><div class='block w60 rgt'><div class='hidden w33 lft cursorpointer' data-control-type='sortDept1' data-control-name='sortDept1' data-control-action='sort' data-path='.Onedept1' data-order='desc' data-type='number' title='Sort by departure time'> " + imDown + "Dep.</div><div class='hidden w33 lft cursorpointer' data-control-type='sortArrival1' data-control-name='sortArrival1' data-control-action='sort' data-path='.OnedArr1' data-order='desc' data-type='number' title='Sort by Arrival time'> " + imDown + "Arr.</div><div class='hidden rgt cursorpointer padding1s' data-control-type='sortMinFare1' data-control-name='sortMinFare1' data-control-action='sort' data-path='.farCls2' data-order='desc' data-type='number' title='Sort by price'> " + imDown + "Min Fare</div></div></div>";
                        $("#RoundDivsheader").html(AvlLayOutHeader2);
                    }
                    else {
                        $("#rdatehide").hide(); $("#returnDate").hide(); $("#RoundDivs").hide(); $("#oneWayDiv").addClass("resultDivround");
                        AvlLayOut1 += "<div  class='list list1 box text-shadow' id='listoneWay'>";
                        AvlLayOutHeader1 += "<div >" + b.CityName.split(' ')[0] + "-" + b.DestCityName.split(' ')[0] + " On  " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Omonth + " " + jrneyDate.getFullYear() + "</div>";
                        AvlLayOutHeader1 += "<div class='w100 boxshadow lft bgf1 lh30 bld hideheader jplist-panel'><div class='hidden' data-control-type='default-sort' data-control-name='sort' data-control-action='sort'data-path='.frscls1' data-order='desc' data-type='number'></div><div class='w33 padding1s lft'></div><div class='block w60 rgt'><div class='hidden w33 lft cursorpointer' data-control-type='sortDept' data-control-name='sortDept' data-control-action='sort' data-path='.Onedept' data-order='desc' data-type='number' title='Sort by departure time'> " + imDown + "Dep.</div><div class='hidden lft cursorpointer' data-control-type='sortArrival' data-control-name='sortArrival' data-control-action='sort' data-path='.OnedArr' data-order='desc' data-type='number' title='Sort by Arrival time'> " + imDown + "Arr.</div><div class='hidden padding1s rgt cursorpointer' data-control-type='sortMinFare' data-control-name='sortMinFare' data-control-action='sort' data-path='.frscls1' data-order='desc' data-type='number' title='Sort by price'> " + imDown + "Min Fare</div></div></div>";
                        $("#oneWayDivheader").html(AvlLayOutHeader1);
                    }
                    for (var nt = 0; nt < h.response.length; nt++) {
                        //var currentdate = new Date();
                        //var currentdatetime = currentdate.getTime();
                        //var searchdate = new Date(jrneyDate).setTime(h.response[i][nt].departTime);
                        //var currentdatetime = searchdate.getTime();

                        tttravelaer1.push(h.response[nt].traveler);
                        ttService1.push(h.response[nt].serviceType);
                        // if (h.response[i][nt].duplicateRecord == false) {
                        if (h.response.length > 1)
                            AvlLayOut1 += "<div class='list-item box border-list totdata1 bgw boxshadow rtgrid' style='width:100%;'>";
                        else
                            AvlLayOut1 += "<div class='list-item box border-list totdata1 rtgrid1'>";
                        if (h.response[nt].provider_name == "GS")
                            AvlLayOut1 += "<div class='CenterProvider'>GSRTC Bus</div>";
                        else if (h.response[nt].provider_name == "TY")
                            AvlLayOut1 += "<div class='CenterProvider'>Travel-Yaari Bus</div>";
                        else if (h.response[nt].provider_name == "RB")
                            AvlLayOut1 += "<div class='CenterProvider'>Red Bus</div>";
                        else if (h.response[nt].provider_name == "ES")
                            AvlLayOut1 += "<div class='CenterProvider'>E-Smart Travel</div>";
                        AvlLayOut1 += "<div class='serviceType'><div class='serviceTypeO bld cursorpointer colormain' title='" + h.response[nt].serviceType.toProperCase() + "'>" + h.response[nt].serviceType.toProperCase() + "</div><div class='Bustype travelertypeO f14 cursorpointer' title='" + h.response[nt].traveler.toProperCase() + "'>" + h.response[nt].traveler.toProperCase() + "</div></div>";
                        var acn = "";
                        if (h.response[nt].serviceType.toProperCase().indexOf("Non/ac") != -1 || h.response[nt].serviceType.toProperCase().indexOf("Non A/c") != -1)
                            acn = h.response[nt].serviceType.toProperCase().toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc");
                        else
                            acn = h.response[nt].serviceType.toProperCase();
                        AvlLayOut1 += "<div class='servicetyO hide'>" + acn + "</div>";
                        AvlLayOut1 += "<div class='block'>";
                        AvlLayOut1 += "<p class='desc hide'></p>";
                        AvlLayOut1 += "<p class='like hide'></p>";
                        AvlLayOut1 += "";
                        //   if (h.response[i][nt].provider_name == "GS" || h.response[i][nt].provider_name == "TY" || h.response[i][nt].provider_name == "ES" || h.response[i][nt].provider_name == "RB") {
                        AvlLayOut1 += "<div class='DepartureTime dept1'><div class='dptp bld colormain'>Dep:</div><div class='bld colormain bstfont' >" + getTimeDuration(h.response[nt].departTime, "0", "0") + "<i style='margin-top:10px;' class='fa fa-long-arrow-right rgt' aria-hidden='true'></i>" + "</div><div id='BoardO_" + i + "_" + nt + "' class='em7 gridViewToolTip colormain'>Boarding Point</div><div id='BoarddO_" + i + "_" + nt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                        AvlLayOut1 += "<div class='Onedept hide'>" + convertToMin(h.response[nt].departTime) + "</div>";
                        if (h.response[nt].arrTime == "-1" || h.response[nt].arrTime == null) {
                            AvlLayOut1 += "<div class='DepartureTime Arr1'><div class='dptp bld colormain'>Arr:</div><div class='bld colormain cursorpointer' title='arrival time is not available' >N.A</div><div id='DropO_" + i + "_" + nt + "' class='em7 gridViewToolTip colormain'>Dropping Point</div><div id='DroppO_" + i + "_" + nt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                            AvlLayOut1 += "<div class='OnedArr hide'>" + 0 + "</div>";
                        }
                        else {
                            AvlLayOut1 += "<div class='DepartureTime Arr1'><div class='dptp bld colormain'>Arr:</div><div class='bld colormain bstfont' >" + getTimeDuration(h.response[nt].arrTime, "0", "0") + "</div><div id='DropO_" + i + "_" + nt + "' class='em7 gridViewToolTip colormain'>Dropping Point</div><div id='DroppO_" + i + "_" + nt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                            AvlLayOut1 += "<div class='OnedArr hide'>" + convertToMin(h.response[nt].arrTime) + "</div>";
                        }




                        if (h.response.length > 1) {
                            AvlLayOut1 += "<div  class='DepartureTime'><b>" + h.response[nt].remainingSeat + "</b><br/>Remaining Seat</div>";

                            AfareLayOut1 = "</div>";

                        }
                        //else {
                        //    AvlLayOut1 += "<div >Remaining Seat</div>" + h.response[nt].remainingSeat + "</div>";
                        //    AvlLayOut1 += "<div  class='title'>";
                        //    AfareLayOut1 = "</div>";
                        //}



                        //     }
                        //else {
                        //    AvlLayOut1 += "<div class='DepartureTime dept1'><div class='dptp'>Dep:</div><div>" + getTimeDuration(h.response[i][nt].departTime, "0", "0") + "</div><div id='BoardO_" + i + "_" + nt + "' class='em7 gridViewToolTip'>Boarding Point</div><div id='BoarddO_" + i + "_" + nt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                        //    AvlLayOut1 += "<div class='Onedept hide'>" + h.response[i][nt].departTime + "</div>";
                        //    AvlLayOut1 += "<div class='DepartureTime Arr1'><div class='dptp'>Arr:</div><div>" + getTimeDuration(h.response[i][nt].arrTime, "0", "0") + "</div><div id='DropO_" + i + "_" + nt + "' class='em7 gridViewToolTip'>Dropping Point</div><div id='DroppO_" + i + "_" + nt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                        //    AvlLayOut1 += "<div class='OnedArr hide'>" + h.response[i][nt].arrTime + "</div>";
                        //}


                        AvlLayOut1 += "<div class='avl1 hide'>" + h.response[nt].remainingSeat + "</div>";
                        AvlLayOut1 += "<div  class='title pricessO'>";
                        AfareLayOut1 = "";


                        AvlLayOut1 += "<div class='ODetails cursorpointer rgt' id='Details_O_" + i + "_" + nt + "'>&nbsp; <img src='" + UrlBase + "BS/images/details.png' title='Details' /> </div><div id='breakup_O_" + i + "_" + nt + "' class='brkdtls cursorpointer rgt'>&nbsp; <img src='" + UrlBase + "BS/images/faredetails.png' /> </div><div id='CanO_" + i + "_" + nt + "' class='rgt cursorpointer gridViewToolTip' style='padding-top:9px;' > <img src='" + UrlBase + "BS/images/cancellationpolicy.png' /> </div><div id='CannO_" + i + "_" + nt + "' class='gridViewToolTip1'></div>";
                        if (h.response.length > 1) {
                            //AvlLayOut1 += "<span id='AvlSeat_O_" + i + "_" + nt + "' class='hide remaining1 f16 bld '></span>";
                            AvlLayOut1 += "<div id='farebrk_O_" + i + "_" + nt + "' class='tooltiphch' style='display:none;' ></div>";
                        }
                        else {
                            AvlLayOut1 += "<span id='AvlSeat_O_" + i + "_" + nt + "' class='remaining f16 bld'></span>";
                            AvlLayOut1 += "<div id='farebrk_O_" + i + "_" + nt + "' class=' tooltiphch' style='display:none;></div>";
                        }
                        AvlLayOut1 += "<div class='clear'></div><span id='farebrk_O_" + i + "_" + nt + "' class='hide remaining'></span>";
                        var slcts = "";
                        if (h.response[nt].remainingSeat == "0")
                            slcts += "<span rel='" + h.response[nt].remainingSeat + "' title='please Select another operator' class='OOSelect cursorpointer hide'id='select_O_" + i + "_" + nt + "'>SOLD-OUT</span>";
                        else
                            slcts += "<span rel='" + h.response[nt].remainingSeat + "' class='OSelect' id='select_O_" + i + "_" + nt + "'>SELECT</span>";
                        if (h.response[nt].provider_name == "GS") {
                            for (var p = 0; p < h.response[nt].Arr_taNetFare.length; p++) {
                                if (p == 0)
                                    AfareLayOut1 += h.response[nt].Arr_taNetFare[p] + " , ";
                                else
                                    AfareLayOut1 += h.response[nt].Arr_taNetFare[p] + " , ";
                            }
                            AvlLayOut1 += "<div style='text-align:left;' class='bld colorp' id='Fare_O_" + i + "_" + nt + "' title='" + AfareLayOut1.trim().substring(0, AfareLayOut1.trim().length - 1) + "'><img id='img_O_" + i + "_" + nt + "' src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='frscls1' id='HFare_O_" + i + "_" + nt + "'>" + h.response[nt].Arr_taNetFare[0] + slcts + "</span></div>";
                        }
                        else {
                            for (var p = 0; p < h.response[nt].Arr_totFare.length; p++) {

                                if (p == 0)
                                    AfareLayOut1 += h.response[nt].Arr_totFare[p] + " , ";
                                else
                                    AfareLayOut1 += h.response[nt].Arr_totFare[p] + " , ";
                            }
                            AvlLayOut1 += "<div  style='text-align:center;' class=' bld colorp' id='Fare_O_" + i + "_" + nt + "' title='" + AfareLayOut1.trim().substring(0, AfareLayOut1.trim().length - 1) + "'><img id='img_O_" + i + "_" + nt + "' src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='frscls1' id='HFare_O_" + i + "_" + nt + "'>" + SetMinFare(h.response[nt].Arr_totFare) + slcts + "</span></div>";
                        }

                        AvlLayOut1 += "<div class='hide'  id='select_OO_" + i + "_" + nt + "'>" + redBusBuffImage + "</div>";
                        AvlLayOut1 += "</div></div>";
                        AvlLayOut1 += "<div class='aminities'>" + FunSetAminities(h.response[nt].traveler) + "</div>";
                        AvlLayOut1 += "<div style='clear:both;'></div>";
                        //if (h.response[i][nt].dupliKeyId != null) {
                        //    AvlLayOut1 += "<div class='bld textaligncenter f10 w100 cursorpointer' id='S" + h.response[i][nt].dupliKeyId + "_" + i + "_" + nt + "' onclick='shoemoreoptionforsearch(this.id)' >Show More</div>";
                        //    AvlLayOut1 += "<div style='clear:both;'></div>";
                        //    AvlLayOut1 += "<div id='avl" + h.response[i][nt].dupliKeyId + "_" + i + "_" + nt + "'></div>";
                        //}
                        AvlLayOut1 += "</div>";

                        //if (h.response[i][nt].duplicateRecord == true)
                        //    AvlLayOut1 += "<div style='background-color:rgb(178, 30, 85);' onclick='shoemoreoptionforsearch(this.id)' id='S" + h.response[i][nt].dupliKeyId + nt + "'>show More</div><div style='background-color:rgb(178, 30, 85);' onclick='Hidemoreoptionforsearch(this.id)' id='H" + h.response[i][nt].dupliKeyId + nt + "' class='hide'>Hide Me</div></div>";
                        // }
                    }
                    AvlLayOut1 += "</div><div class='clear'></div>";
                    AvlLayOut1 += "<div class='clear'></div><div class='noRes1 boxshadow bgf1'><p>Sorry, we cannot find any buses for your search.Please try to modify your search.</p></div>";
                    $(".ResultPageLoding").hide();
                    $(".ResultPageLoding_box").hide();

                    $("#oneWayDiv").html(" ");
                    $("#oneWayDiv").append(AvlLayOut1);
                    tttravelaer.push(tttravelaer1);
                    ttService.push(ttService1);
                    var no = 0
                    $("#ServiceSort1").html(createNewserviceTypeFliter(no, ttService1));
                    $("#TravelerSort1").html(createNewTravelerTypeFliter(no, tttravelaer1));

                }
                ////else {
                ////    for (var rt = 0; rt < h.response.length; rt++) {
                ////        tttravelaer1.push(h.response[rt].traveler);
                ////        ttService1.push(h.response[rt].serviceType);
                ////        if (h.response.length > 1)
                ////            AvlLayOut2 += "<div class='list-item box border-list1 totdata2 boxshadow rtgrid'>";
                ////        else
                ////            AvlLayOut2 += "<div class='list-item box border-list1 totdata2 boxshadow rtgrid1'>";
                ////        if (h.response[rt].provider_name == "GS")
                ////            AvlLayOut2 += "<div class='CenterProvider'>GSRTC Bus</div>";
                ////        else if (h.response[rt].provider_name == "TY")
                ////            AvlLayOut2 += "<div class='CenterProvider'>Travel-Yaari Bus</div>";
                ////        else if (h.response[rt].provider_name == "RB")
                ////            AvlLayOut2 += "<div class='CenterProvider'>Red Bus</div>";
                ////        else if (h.response[rt].provider_name == "ES")
                ////            AvlLayOut2 += "<div class='CenterProvider'>E-Smart Travel</div>";
                ////        AvlLayOut2 += "<div class='serviceType'><div class='serviceTypeR bld f16 colormain cursorpointer' title='" + h.response[rt].serviceType.toProperCase() + "'>" + h.response[rt].serviceType.toProperCase() + "</div><div class='Bustype travelertypeR cursorpointer' title='" + h.response[rt].traveler.toProperCase() + "'>" + h.response[rt].traveler.toProperCase() + "</div></div>";
                ////        var acn = "";
                ////        if (h.response[rt].serviceType.toProperCase().indexOf("Non/ac") != -1 || h.response[rt].serviceType.toProperCase().indexOf("Non A/c") != -1)
                ////            acn = h.response[rt].serviceType.toProperCase().toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc")
                ////        else
                ////            acn = h.response[rt].serviceType.toProperCase();
                ////        AvlLayOut2 += "<div class='Rservicety hide'>" + acn + "</div>";
                ////        AvlLayOut2 += "<div class='block'>";
                ////        AvlLayOut2 += "<p style='display:none;' class='desc'></p>";
                ////        AvlLayOut2 += "";

                ////        //    if (h.response[i][rt].provider_name == "GS" || h.response[i][rt].provider_name == "TY" || h.response[i][rt].provider_name == "ES") {
                ////        AvlLayOut2 += "<div class='DepartureTime dept2'><div class='dptp colormain bld'>Dep:</div><div class='colormain bld'> " + getTimeDuration(h.response[rt].departTime, "0", "0") + "</div><div id='BoardR_" + i + "_" + rt + "' class='em7 gridViewToolTip colormain bld'>Boarding Point</div><div id='BoarddR_" + i + "_" + rt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                ////        AvlLayOut2 += "<div class='Onedept1 hide'>" + convertToMin(h.response[rt].departTime) + "</div>";
                ////        if (h.response[rt].arrTime == "-1" || h.response[rt].arrTime == null) {
                ////            AvlLayOut2 += "<div class='DepartureTime Arr2'><div class='dptp colormain bld'>Arr:</div><div class='colormain bld cursorpointer' title='Arrival time is not available'>N.A</div><div id='DropR_" + i + "_" + rt + "' class='em7 gridViewToolTip colormain bld'>Dropping Point</div><div id='DroppR_" + i + "_" + rt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                ////            AvlLayOut2 += "<div class='OnedArr1 hide'>0</div>";
                ////        }
                ////        else {
                ////            AvlLayOut2 += "<div class='DepartureTime Arr2'><div class='dptp colormain bld'>Arr:</div><div class='colormain bld'>" + getTimeDuration(h.response[rt].arrTime, "0", "0") + "</div><div id='DropR_" + i + "_" + rt + "' class='em7 gridViewToolTip colormain bld'>Dropping Point</div><div id='DroppR_" + i + "_" + rt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                ////            AvlLayOut2 += "<div class='OnedArr1 hide'>" + convertToMin(h.response[rt].arrTime) + "</div>";
                ////        }
                ////        //}
                ////        //else {
                ////        //    AvlLayOut2 += "<div class='DepartureTime dept2'><div class='dptp'>Dep:</div><div> " + getTimeDuration(h.response[i][rt].departTime, "0", "0") + "</div><div id='BoardR_" + i + "_" + rt + "' class='em7 gridViewToolTip'>Boarding Point</div><div id='BoarddR_" + i + "_" + rt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                ////        //    AvlLayOut2 += "<div class='Onedept1 hide'>" + h.response[i][rt].departTime + "</div>";
                ////        //    AvlLayOut2 += "<div class='DepartureTime Arr2'><div class='dptp'>Arr:</div><div>" + getTimeDuration(h.response[i][rt].arrTime, "0", "0") + "</div><div id='DropR_" + i + "_" + rt + "' class='em7 gridViewToolTip'>Dropping Point</div><div id='DroppR_" + i + "_" + rt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                ////        //    AvlLayOut2 += "<div class='OnedArr1 hide'>" + h.response[i][rt].arrTime + "</div>";
                ////        //}

                ////        AvlLayOut2 += "<div class='avl2 hide'>" + h.response[rt].remainingSeat + "</div>";
                ////        AvlLayOut2 += "<div  class='title pricessR'>"
                ////        AfareLayOut2 = "";
                ////        //  AvlLayOut2 += "<div class='RDetails cursorpointer textunderline' id='Details_R_" + i + "_" + rt + "'>Details</div><div id='breakup_R_" + i + "_" + rt + "' class='brkdtls cursorpointer textunderline italic'>FareBrakeup</div>";
                ////        AvlLayOut2 += "<div class='RDetails cursorpointer rgt' id='Details_R_" + i + "_" + rt + "'>&nbsp; <img src='" + UrlBase + "BS/images/details.png' title='Details' /> </div><div id='breakup_R_" + i + "_" + rt + "' class='brkdtls cursorpointer rgt'>&nbsp; <img src='" + UrlBase + "BS/images/faredetails.png' /> </div><div id='CanR_" + i + "_" + rt + "' class='rgt gridViewToolTip'> <img src='" + UrlBase + "BS/images/cancellationpolicy.png' /> </div><div id='CannR_" + i + "_" + rt + "' class='gridViewToolTip1'></div>";
                ////        AvlLayOut2 += "<span id='AvlSeat_R_" + i + "_" + rt + "' class='hide remaining1 f16 bld'></span>";
                ////        AvlLayOut2 += "<div id='farebrk_R_" + i + "_" + rt + "' class='hide tooltiphch'></div><div class='clear'></div>";
                ////        // AvlLayOut2 += "<div class='clear1'></div><span id='farebrk_O_" + i + "_" + nt + "' class='hide remaining'></span>";
                ////        if (h.response[rt].provider_name == "GS") {
                ////            for (var p = 0; p < h.response[rt].Arr_taNetFare.length; p++) {

                ////                if (p == 0)
                ////                    AfareLayOut2 += h.response[rt].Arr_taNetFare[p] + " , ";
                ////                else
                ////                    AfareLayOut2 += h.response[rt].Arr_taNetFare[p] + " , ";
                ////            }
                ////            AvlLayOut2 += "<div class='f16 bld colorp cursorpointer Rfarehide' id='Fare_R_" + i + "_" + rt + "'  title='" + AfareLayOut2.trim().substring(0, AfareLayOut2.trim().length - 1) + "'><img id='img_R_" + i + "_" + rt + "' src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='farCls2' id='HFare_R_" + i + "_" + rt + "'>" + h.response[rt].Arr_taNetFare[0] + "</span></div>";
                ////        }
                ////        else {
                ////            for (var p = 0; p < h.response[rt].Arr_totFare.length; p++) {
                ////                if (p == 0)
                ////                    AfareLayOut2 += h.response[rt].Arr_totFare[p] + " , ";
                ////                else
                ////                    AfareLayOut2 += h.response[rt].Arr_totFare[p] + " , ";
                ////            }
                ////            AvlLayOut2 += "<div class='f16 bld colorp cursorpointer Rfarehide' id='Fare_R_" + i + "_" + rt + "'  title='" + AfareLayOut2.trim().substring(0, AfareLayOut2.trim().length - 1) + "'><img id='img_R_" + i + "_" + rt + "' src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='farCls2' id='HFare_R_" + i + "_" + rt + "'>" + SetMinFare(h.response[rt].Arr_totFare) + "</span></div>";
                ////        }

                ////        if (h.response[rt].remainingSeat == "0")
                ////            AvlLayOut2 += "<div class='clear1'></div><span rel='" + h.response[rt].remainingSeat + "' title='please Select another operator' id='select_R_" + i + "_" + rt + "' class='OOSelect cursorpointer hide'>SOLD-OUT</span>";
                ////        else
                ////            AvlLayOut2 += "<div class='clear1'></div><span rel='" + h.response[rt].remainingSeat + "' class='RSelect hide' id='select_R_" + i + "_" + rt + "'>SELECT</span>";

                ////        AvlLayOut2 += "<div class='hide'  id='select_RR_" + i + "_" + rt + "'>" + redBusBuffImage + "</div>";
                ////        AvlLayOut2 += "</div></div>";
                ////        AvlLayOut2 += "<div class='aminities'>" + FunSetAminities(h.response[rt].traveler) + "</div>";
                ////        AvlLayOut2 += "<div style='clear:both;'></div>";
                ////        AvlLayOut2 += "</div>";
                ////    }
                ////    AvlLayOut2 += "</div>";
                ////    AvlLayOut2 += "<div class='clear'></div><div class='noRes2 boxshadow bgf1'><p>Sorry, we cannot find any buses for your search.Please try to modify your search.</p></div>";
                ////   // $("#RoundDivs").html(" ");
                ////    $("#RoundDivs").html(AvlLayOut2);
                ////   // tttravelaer.push(tttravelaer1);
                ////   // ttService.push(ttService1);
                ////    var nos = 1;
                //// //   $("#ServiceSort2").html(createNewserviceTypeFliter(nos, ttService1));
                ////   // $("#TravelerSort2").html(createNewTravelerTypeFliter(nos, tttravelaer1));
                ////}
            }
            if (h.response[0].length != 0)
                b.readyToslider(b);
            //$('#basic-modal-content').hide();
            //$('#page-content').show();
            $("#divloading").hide();

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
newSearchHelper.prototype.readyToslider = function (slide) {
    var Aprice; var Aprice1;
    var ADept; var ADept1;
    var AArrivel; var AArrivel1;
    var AAvl; var AAvl1;
    var Priceimage = "<img src='" + UrlBase + "Images/rs.png' style='height:10px;' />";
    Aprice = fun_SetMinMaxFare($(".pricessO"));
    Aprice1 = fun_SetMinMaxFare($(".pricessR"));
    ADept = fun_SetMinMaxDeptTime($(".Onedept"));
    ADept1 = fun_SetMinMaxDeptTime($(".Onedept1"));
    AArrivel = SetMinMaxArrTime($(".OnedArr"));
    AArrivel1 = SetMinMaxArrTime($(".OnedArr1"));
    AAvl = fun_SetMinMaxAvailableSeat($(".avl1"));
    AAvl1 = fun_SetMinMaxAvailableSeat($(".avl2"));
    slide.demo.jplist({
        //main options
        itemsBox: '.list1'
        , itemPath: '.border-list'
        , panelPath: '.jplist-panel'
        , noResults: '.noRes1'
        //save plugin state
        , storage: '' //'', 'cookies', 'localstorage'			
        , storageName: 'jplist-list-grid'
            , controlTypes: {
                'sortMinFare': {
                    className: 'DefaultSort1'
                    , options: {}
                },
                'sortDept': {
                    className: 'DefaultSort1'
                , options: {}
                },
                'sortArrival': {
                    className: 'DefaultSort1'
                 , options: {}
                },
                'sortSeats': {
                    className: 'DefaultSort1'
                , options: {}
                },
                'default-sort': {
                    className: 'DefaultSort'
                , options: {}
                },
                'Service0': {
                    className: 'CheckboxGroupFilter'
                 , options: {}
                },
                'range-slider-price1': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: Aprice[0]
                             , max: Aprice[1]
                             , range: true
                             , values: [Aprice[0], Aprice[1]]
                             , slide: function (event, ui) {
                                 $prev.html(Priceimage + ui.values[0]);
                                 $next.html(Priceimage + ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         $prev.html(Priceimage + $slider.slider('values', 0));
                         $next.html(Priceimage + $slider.slider('values', 1));
                     }
                 }
                },
                'range-slider-dept1': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: ADept[0]
                             , max: ADept[1]
                             , range: true
                             , values: [ADept[0], ADept[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         var mindepttime = "";
                         var maxdepttime = "";
                         mindepttime = $slider.slider('values', 0);
                         maxdepttime = $slider.slider('values', 1);
                         $prev.html(getTimeDuration(mindepttime.toString(), "0", "0"));
                         $next.html(getTimeDuration(maxdepttime.toString(), "0", "0"));
                         //$prev.html(setminutetotime(slide.JourneyDate, mindepttime));
                         //$next.html(setminutetotime(slide.JourneyDate, maxdepttime));
                     }
                 }
                },
                'range-slider-Arr1': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: AArrivel[0]
                             , max: AArrivel[1]
                             , range: true
                             , values: [AArrivel[0], AArrivel[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         var minArrtime = "";
                         var maxArrtime = "";
                         minArrtime = $slider.slider('values', 0);
                         maxArrtime = $slider.slider('values', 1);
                         $prev.html(getTimeDuration(minArrtime.toString(), "0", "0"));
                         $next.html(getTimeDuration(maxArrtime.toString(), "0", "0"));
                         //$prev.html(setminutetotime(slide.JourneyDate, minArrtime));
                         //$next.html(setminutetotime(slide.JourneyDate, maxArrtime));
                     }
                 }
                },
                'range-slider-Avl1': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: AAvl[0]
                             , max: AAvl[1]
                             , range: true
                             , values: [AAvl[0], AAvl[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         $prev.html($slider.slider('values', 0));
                         $next.html($slider.slider('values', 1));
                     }
                 }

                }
                    , 'checkbox-text-filter0T': {
                        className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                    }
                    }
            , 'checkbox-text-filter0S': {
                className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                    }
            }


            }
    });
    slide.demo.jplist({
        //main options
        itemsBox: '.list2'
        , itemPath: '.border-list1'
        , panelPath: '.jplist-panel'
           , noResults: '.noRes2'
        //save plugin state
        , storage: '' //'', 'cookies', 'localstorage'			
        , storageName: 'jplist-list-grid'
            , controlTypes: {
                'sortMinFare1': {
                    className: 'DefaultSort1'
                    , options: {}
                },
                'sortDept1': {
                    className: 'DefaultSort1'
                    , options: {}
                },
                'sortArrival1': {
                    className: 'DefaultSort1'
                    , options: {}
                },
                'sortSeats1': {
                    className: 'DefaultSort1'
                    , options: {}
                },
                'default-sort1': {
                    className: 'DefaultSort'
                    , options: {}
                },
                'Service1': {
                    className: 'CheckboxGroupFilter'
                     , options: {}
                },
                'range-slider-price2': {
                    className: 'RangeSlider'
                , options: {
                    //jquery ui range slider
                    ui_slider: function ($slider, $prev, $next) {

                        $slider.slider({
                            min: Aprice1[0]
                            , max: Aprice1[1]
                            , range: true
                            , values: [Aprice1[0], Aprice1[1]]
                            , slide: function (event, ui) {
                                $prev.html(Priceimage + ui.values[0]);
                                $next.html(Priceimage + ui.values[1]);
                            }
                        });
                    },
                    set_values: function ($slider, $prev, $next) {
                        $prev.html(Priceimage + $slider.slider('values', 0));
                        $next.html(Priceimage + $slider.slider('values', 1));
                    }
                }
                },
                'range-slider-dept2': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: ADept1[0]
                             , max: ADept1[1]
                             , range: true
                             , values: [ADept1[0], ADept1[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         var mindepttime1 = "";
                         var maxdepttime1 = "";
                         mindepttime1 = $slider.slider('values', 0);
                         maxdepttime1 = $slider.slider('values', 1);
                         $prev.html(getTimeDuration(mindepttime1.toString(), "0", "0"));
                         $next.html(getTimeDuration(maxdepttime1.toString(), "0", "0"));
                         //$prev.html(setminutetotime(slide.ReturnDate, mindepttime1));
                         //$next.html(setminutetotime(slide.ReturnDate, maxdepttime1));
                     }
                 }
                },
                'range-slider-Arr2': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: AArrivel1[0]
                             , max: AArrivel1[1]
                             , range: true
                             , values: [AArrivel1[0], AArrivel1[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         var minArrtime1 = "";
                         var maxArrtime1 = "";
                         minArrtime1 = $slider.slider('values', 0);
                         maxArrtime1 = $slider.slider('values', 1);
                         $prev.html(getTimeDuration(minArrtime1.toString(), "0", "0"));
                         $next.html(getTimeDuration(maxArrtime1.toString(), "0", "0"));
                         //$prev.html(setminutetotime(slide.ReturnDate, minArrtime1));
                         //$next.html(setminutetotime(slide.ReturnDate, maxArrtime1));
                     }
                 }
                }
                 , 'checkbox-text-filter1T': {
                     className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                    }
                 }
            , 'checkbox-text-filter1S': {
                className: 'CheckboxTextFilter'
                    , options: {
                        ignore: '' //regex for the characters to ignore, for example: [^a-zA-Z0-9]+
                    }
            },
                'range-slider-Avl2': {
                    className: 'RangeSlider'
                 , options: {
                     //jquery ui range slider
                     ui_slider: function ($slider, $prev, $next) {
                         $slider.slider({
                             min: AAvl1[0]
                             , max: AAvl1[1]
                             , range: true
                             , values: [AAvl1[0], AAvl1[1]]
                             , slide: function (event, ui) {
                                 $prev.html(ui.values[0]);
                                 $next.html(ui.values[1]);
                             }
                         });
                     },
                     set_values: function ($slider, $prev, $next) {
                         $prev.html($slider.slider('values', 0));
                         $next.html($slider.slider('values', 1));
                     }
                 }
                }

            }
    });
}
newSearchHelper.prototype.getCancelationPolicy = function (p) {
    var res = h.response[parseInt(p.DtlS.trim().split('_')[3])];
    var canPolicy = "";
    canPolicy = res.canPolicy_RB;
    var providerName = res.provider_name;
    var fare = res.seat_farewithMarkp;
    var jPBDdates;
    if (p.DtlS.trim().split('_')[2] == "0")
        jPBDdates = p.JourneyDate;
    else
        jPBDdates = p.ReturnDate;
    var DeptTime = res.departTime
    var PpartialCanAllowed = res.partialCanAllowed;
    if (providerName == "TY")
        GetclsRouteSchedule(res.serviceID, "Can", "showdata", jPBDdates, fare);
    else
        var policies = CreatePolilcy(canPolicy, providerName, fare, jPBDdates, DeptTime, PpartialCanAllowed);
    $("#showdata").html(policies);
}
newSearchHelper.prototype.FareBreakUp = function (f, thisid) {

    var FBrkRes = "";
    FBrkRes = CreateFareBreakup(h.response[parseInt(f.DtlS.trim().split('_')[3])]);
    $("#showdata").html(FBrkRes);
}
newSearchHelper.prototype.BoardingPoints = function (b, thisid) {
    var jBDdates;
    if (b.DtlS.trim().split('_')[2] == "0")
        jBDdates = b.JourneyDate;
    else
        jBDdates = b.ReturnDate;
    if (h.response[parseInt(b.DtlS.trim().split('_')[3])].provider_name == "GS") {
        GsGetBoarDingPoints(h.response[parseInt(b.DtlS.trim().split('_')[3])].serviceID, "board", "showdata");
    }
    else if (h.response[parseInt(b.DtlS.trim().split('_')[3])].provider_name == "RB" || h.response[parseInt(b.DtlS.trim().split('_')[3])].provider_name == "ES") {
        var Boarding = CreateBoardingPoints(h.response[parseInt(b.DtlS.trim().split('_')[3])], "board");
        $("#showdata").html(Boarding);
    }
    else if (h.response[parseInt(b.DtlS.trim().split('_')[3])].provider_name == "TY") {
        GetclsRouteSchedule(h.response[parseInt(b.DtlS.trim().split('_')[3])].serviceID, "board", "showdata", jBDdates);
    }
}
newSearchHelper.prototype.DroppingPoints = function (d, thisid) {
    var jDBDdates;
    if (d.DtlS.trim().split('_')[2] == "0")
        jDBDdates = d.JourneyDate;
    else
        jDBDdates = d.ReturnDate;
    if (h.response[parseInt(d.DtlS.trim().split('_')[3])].provider_name == "GS") {
        GsGetBoarDingPoints(h.response[parseInt(d.DtlS.trim().split('_')[3])].serviceID, "Drop", "showdata");
    }
    else if (h.response[parseInt(d.DtlS.trim().split('_')[3])].provider_name == "RB" || h.response[parseInt(d.DtlS.trim().split('_')[3])].provider_name == "ES") {
        var Dropping = CreateDroppingPoints(h.response[parseInt(d.DtlS.trim().split('_')[3])], "Drop");
        $("#showdata").html(Dropping);
    }
    else if (h.response[parseInt(d.DtlS.trim().split('_')[3])].provider_name == "TY") {
        GetclsRouteSchedule(h.response[parseInt(d.DtlS.trim().split('_')[3])].serviceID, "Drop", "showdata", jDBDdates);
    }
}
newSearchHelper.prototype.getTripDetails = function (s) {
    $('#divloading').show();
    var k = s; var datas = ""; var seatFare = ""; var MseatFare = ""; var jsDates;
    var sRes = h.response[parseInt(s.StLs.trim().split('_')[3])];
    for (var sf = 0; sf < sRes.seat_Originalfare.length; sf++) {
        seatFare += sRes.seat_Originalfare[sf] + ",";
        MseatFare += sRes.Arr_totFare[sf] + ",";
    }
    seatFare = seatFare.trim().substring(0, seatFare.trim().length - 1);
    MseatFare = MseatFare.trim().substring(0, MseatFare.trim().length - 1);
    if (s.StLs.trim().split('_')[1] == "O") {
        datas = "{'jdate':'" + s.JourneyDate + "','srcId':'" + s.CityName + "','destId':'" + s.DestCityName + "','serviceId':'" + sRes.serviceID + "','seattype':'" + sRes.serviceType + "','provider':'" + sRes.provider_name + "','fare':'" + seatFare + "','traveler':'" + sRes.traveler.replace("'", "") + "','farewithMarkp':'" + MseatFare + "'}";
        jsDates = s.JourneyDate;
    }
    else {
        datas = "{'jdate':'" + s.ReturnDate + "','srcId':'" + s.DestCityName + "','destId':'" + s.CityName + "','serviceId':'" + sRes.serviceID + "','seattype':'" + sRes.serviceType + "','provider':'" + sRes.provider_name + "','fare':'" + seatFare + "','traveler':'" + sRes.traveler.replace("'", "") + "','farewithMarkp':'" + MseatFare + "'}";
        jsDates = s.ReturnDate;
    }
    var Url = UrlBase + "BS/WebService/CommonService.asmx/getSeatLayOut";
    $.ajax({
        url: Url,
        contentType: "application/json; charset=utf-8",
        data: datas,
        dataType: "json",
        type: "POST",
        async: true,
        success: function (data) {
            $('#divloading').hide();
            var seatLayout = data.d;
            var seatArrangement = "";
            seatArrangement += "<div>";
            seatArrangement += "<div><img src='" + UrlBase + "BS/Images/cls.png' class='cls' align='right' />";
            seatArrangement += "<div class='lft w66'>";
            if (seatLayout == "timeOut") {
                alert("session-timeout");
                window.location.href = UrlBase + "BS/BusSearch.aspx";
            }
            seatArrangement += seatLayout;
            seatArrangement += "</div>";
            seatArrangement += "<div class='w30 rgt'>";
            seatArrangement += "<div><img src='Images/4.png'/> Available Seat<div class='clear1'></div><img src='Images/3.png'/> Reserved For Ladies<div class='clear1'></div><img src='Images/2.png'/> Selected Seat<div class='clear1'></div><img src='Images/1.png'/> Blocked Seat <div class='clear1'></div><img src='Images/s1.png'/> Available Seat<div class='clear1'></div><img src='Images/s2.png'/> Reserved For Ladies<div class='clear1'></div><img src='Images/s3.png'/> Selected Seat<div class='clear1'></div><img src='Images/s4.png'/> Blocked Seat</div>";
            //if (sRes.Dur_Time == "00:00:00" || sRes.Dur_Time == "00:00")
            //    seatArrangement += "<div class='clear1'></div><div class='f16'>Duration : Not available</div>";
            //else
            //    seatArrangement += "<div class='clear1'></div><div class='f16'>Duration : " + sRes.Dur_Time + "</div>";
            //seatArrangement += "<div>";
            //seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='s'></div>";
            //seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='f'></div>";
            //seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='AcSvctax'></div>"
            //seatArrangement += "<div style=' width:50%;'><input type='button' id='btnContinue' name='Procced' value='Continue' class='button33' /></div>";
            //seatArrangement += "</div>";
            //seatArrangement += "</div>";
            //seatArrangement += "<div class='clear'></div>";
            //seatArrangement += "<div>";
            //seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>Boarding Point:</div>";
            //seatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 20px;'>Dropping Point:</div>";
            //seatArrangement += "<div class='clear'></div>";
            //if (sRes.provider_name == "GS") {
            //    seatArrangement += "<div id='dibBoard'>" + GsPoints(sRes.serviceID) + "</div>";
            //    seatArrangement += "</div>";
            //    seatArrangement += "</div>";
            //    $("#divseat").html(seatArrangement);
            //    if (s.StLs.trim().split('_')[1] == "O") {
            //        //  $("#select_O_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
            //        $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
            //        $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
            //        $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
            //    }
            //    else {
            //        //  $("#select_R_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
            //        $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
            //        $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
            //        $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
            //    }
            if (sRes.Dur_Time == "00:00:00" || sRes.Dur_Time == "00:00")
                seatArrangement += "<div class='clear1'></div><div class='f16'>Duration : Not available</div>";
            else
                seatArrangement += "<div class='clear1'></div><div class='f16'>Duration : " + sRes.Dur_Time + "</div>";
            seatArrangement += "<div>";
            seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='s'></div>";
            seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='f'></div>";
            seatArrangement += "<div  style='margin: 0;padding: 0px 30px 30px 0px;text-align: left;font-weight: bold;color: #333333;height :12px;' id='AcSvctax'></div>"
            seatArrangement += "<div style=' width:50%;'><input type='button' id='btnContinue' name='Procced' value='Continue' class='button33' /></div>";
            seatArrangement += "</div>";
            seatArrangement += "</div>";
            seatArrangement += "<div class='clear'></div>";
            seatArrangement += "<div>";
            seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>Boarding Point:</div>";
            seatArrangement += "<div class='clear'></div>";


            if (sRes.provider_name == "GS") {
                seatArrangement += "<div id='dibBoard'>" + GsPoints(sRes.serviceID) + "</div>";
                seatArrangement += "</div>";
                seatArrangement += "</div>";
                $("#divseat").html(seatArrangement);
                if (s.StLs.trim().split('_')[1] == "O") {
                    //  $("#select_O_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                else {
                    //  $("#select_R_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                $('#divseat').bPopup({
                    speed: 650,
                    transition: 'slideIn'
                });
                s.funseatselectoption(s);
            }
            else if (sRes.provider_name == "RB" || sRes.provider_name == "ES") {
                if ($($(seatLayout)[1]).length != 0)
                    seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + $($(seatLayout)[1]).html() + "</div>";
                else
                    seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + CreateOption(sRes.bdPoint, "board") + "</div>";
                seatArrangement += "<div class='clear'></div>";
                seatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 10px;'>Dropping Point:</div>";
                seatArrangement += "<div class='clear'></div>";
                seatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + CreateOption(sRes.drPoint, "drop") + "</div>";
                seatArrangement += "</div>";
                seatArrangement += "</div>";
                $("#divseat").html(seatArrangement);
                if (s.StLs.trim().split('_')[1] == "O") {
                    //  $("#select_O_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                else {
                    // $("#select_R_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                $('#divseat').bPopup({
                    speed: 650,
                    transition: 'slideIn'
                });
                s.funseatselectoption(s);
            }
            else if (sRes.provider_name == "RB" || sRes.provider_name == "ES") {
                if ($($(seatLayout)[1]).length != 0)
                    seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + $($(seatLayout)[1]).html() + "</div>";
                else
                    seatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + CreateOption(sRes.bdPoint, "board") + "</div>";
                seatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 20px;'>" + CreateOption(sRes.drPoint, "drop") + "</div>";
                seatArrangement += "</div>";
                seatArrangement += "</div>";
                $("#divseat").html(seatArrangement);
                if (s.StLs.trim().split('_')[1] == "O") {
                    //  $("#select_O_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                else {
                    // $("#select_R_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                $('#divseat').bPopup({
                    speed: 650,
                    transition: 'slideIn'
                });
                s.funseatselectoption(s);
            }
            else if (sRes.provider_name == "TY") {
                seatArrangement += "<div id='dibBoard'>" + GetclsRouteSchedule(sRes.serviceID, "OPTION", "dibBoard", jsDates, "") + "</div>";
                seatArrangement += "</div>";
                seatArrangement += "</div>";
                $("#divseat").html(seatArrangement);
                if (s.StLs.trim().split('_')[1] == "O") {
                    //$("#select_O_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_OO_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                else {
                    // $("#select_R_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).show();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).hide();
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).removeClass("ShowBuffImg");
                    $("#select_RR_" + s.StLs.trim().split('_')[2] + "_" + s.StLs.trim().split('_')[3]).addClass("HideBuffImg");
                }
                $('#divseat').bPopup({
                    speed: 650,
                    transition: 'slideIn'
                });
                s.funseatselectoption(s);

            }
        }
    });
}
newSearchHelper.prototype.funseatselectoption = function (fun) {
    var ladiesseattxt = "";
    var cnt = 0;
    var sseat = "";
    var ActaxCharge = 0;
    h.totPrice = 0;
    ta_totComm = 0;
    ta_totTds = 0;
    ta_Totfare = 0;
    ta_Netfare = 0;
    admrkpAmt = 0;
    agmrkpAmt = 0;
    totSeat = "";
    totseatFare = "";
    original_Fare = "";
    h.provider = h.response[parseInt(fun.StLs.trim().split('_')[3])].provider_name;
    h.TravelerType = h.response[parseInt(fun.StLs.trim().split('_')[3])].traveler;

    $('#divseat div[title]' || 'GStbl div[title]')
                    .click(function () {
                        var comUrl = UrlBase + "BS/WebService/CommonService.asmx/getCommissionList";
                        if ($(this).attr("class") == "divAval") {
                            if (cnt < parseInt(fun.PAX)) {
                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "false,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {

                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------//
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                //                                        $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";
                                $(this).removeClass();
                                $(this).addClass("divSelect");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                            }
                        }
                        else if ($(this).attr("class") == "divHoriSleperAval") {
                            if (cnt < parseInt(fun.PAX)) {

                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "false,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {
                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------// 
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                // $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";
                                $(this).removeClass();
                                $(this).addClass("divHoriSleperSelect");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                            }
                        }
                        else if ($(this).attr("class") == "divVertiSleperAval") {
                            if (cnt < parseInt(fun.PAX)) {

                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "false,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {
                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------// 
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                // $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";
                                $(this).removeClass();
                                $(this).addClass("divVertiSleperSelect");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                            }
                        }
                        else if ($(this).attr("class") == "divLadies") {
                            if (cnt < parseInt(fun.PAX)) {

                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "true,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {
                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------// 
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                // $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";
                                $(this).removeClass();
                                $(this).addClass("divSelectLadies");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                            }
                        }
                        else if ($(this).attr("class") == "divHoriSleperLadies") {
                            if (cnt < parseInt(fun.PAX)) {

                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "true,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {
                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------// 
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                // $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";

                                $(this).removeClass();
                                $(this).addClass("divHoriSleperSelectLadies");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                                //                                    $(this).removeClass();
                                //                                    $(this).addClass("divHoriSleperSelect");
                            }
                        }
                        else if ($(this).attr("class") == "divVertiSleperLadies") {
                            if (cnt < parseInt(fun.PAX)) {

                                title = $(this).attr("title").split('|');
                                rel = $(this).attr("rel").split('|');
                                var seat = title[0].split(':');
                                var fare = title[1].split(':');
                                var fareArr = rel[1].split(':');
                                ladiesseattxt += "true,";
                                cnt += 1;
                                //-----------------------calculate commision and tds-----------------------------------//
                                $.ajax({
                                    url: comUrl,
                                    data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                    dataType: "json", type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    asnyc: true,
                                    success: function (data) {
                                        var com = data.d;
                                        if (com.length != 0) {
                                            ta_totComm += parseInt(com[0].adcomm);
                                            ta_totTds += parseInt(com[0].taTds);
                                            ta_Totfare += parseInt(com[0].taTotFare);
                                            ta_Netfare += parseInt(com[0].taNetFare);
                                            admrkpAmt += parseInt(com[0].admrkp);
                                            agmrkpAmt += parseInt(com[0].agmrkp);
                                        }
                                    }
                                });
                                //--------------------------------------------------------------------------------------// 
                                h.totPrice += parseInt($.trim(fare[1]));
                                sseat += $.trim(seat[1]) + ",";
                                $("#s").html("Seat:" + sseat); //cnt
                                // $("#s").html("Seat:" + cnt);
                                $("#f").html("Fare:" + h.totPrice);
                                ActaxCharge += parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                                $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                                totSeat += $.trim(seat[1]) + ",";
                                totseatFare += parseInt($.trim(fare[1])) + ",";
                                original_Fare += $.trim(fareArr[1]) + ",";
                                $(this).removeClass();
                                $(this).addClass("divVertiSleperSelectladies");
                            }
                            else {
                                alert('you have searched for "' + fun.PAX + '" passenger .if you want to book more seat please search again.');
                            }
                            //                                 
                            //                                    $(this).removeClass();
                            //                                    $(this).addClass("divVertiSleperSelectladies");
                        }
                        else if ($(this).attr("class") == "divBlock" || $(this).attr("class") == "divHoriSleperBlock" || $(this).attr("class") == "divVertiSleperBlock" || $(this).attr("class") == "divBlockladies" || $(this).attr("class") == "divHoriSleperBlockladies" || $(this).attr("class") == "divVertiSleperBlockladies") {
                            alert('This Seat is already Blocked');
                            return false;
                        }
                        else if ($(this).attr("class") == "divSelect") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("false,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);

                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divAval");
                        }
                        else if ($(this).attr("class") == "divSelectLadies") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("true,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divLadies");
                        }



                        else if ($(this).attr("class") == "divHoriSleperSelect") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("false,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divHoriSleperAval");
                        }

                        else if ($(this).attr("class") == "divHoriSleperSelectLadies") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("true,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divHoriSleperLadies");
                        }
                        else if ($(this).attr("class") == "divVertiSleperSelect") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("false,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divVertiSleperAval");
                        }

                        else if ($(this).attr("class") == "divVertiSleperSelectladies") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            ladiesseattxt = ladiesseattxt.replace("true,", "");
                            sseat = sseat.replace($.trim(seat[1]) + ",", "");
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divVertiSleperLadies");
                        }

                        else if ($(this).attr("class") == "divVertiSleperLadies") {
                            title = $(this).attr("title").split('|');
                            rel = $(this).attr("rel").split('|');
                            var seat = title[0].split(':');
                            var fare = title[1].split(':');
                            var fareArr = rel[1].split(':');
                            ladiesseattxt += "true,";
                            cnt -= 1;
                            //-----------------------calculate commision and tds-----------------------------------//
                            $.ajax({
                                url: comUrl,
                                data: "{'seatNo':'" + seat[1] + "','seatFare':'" + fareArr[1] + "','provider':'" + h.provider + "','Traveler':'" + h.TravelerType + "'}",
                                dataType: "json", type: "POST",
                                contentType: "application/json; charset=utf-8",
                                asnyc: false,
                                success: function (data) {
                                    var com = data.d;
                                    if (com.length != 0) {
                                        ta_totComm -= parseInt(com[0].adcomm);
                                        ta_totTds -= parseInt(com[0].taTds);
                                        ta_Totfare -= parseInt(com[0].taTotFare);
                                        ta_Netfare -= parseInt(com[0].taNetFare);
                                        admrkpAmt -= parseInt(com[0].admrkp);
                                        agmrkpAmt -= parseInt(com[0].agmrkp);
                                    }
                                }
                            });
                            //--------------------------------------------------------------------------------------//    
                            h.totPrice -= parseInt($.trim(fare[1]));
                            sseat += $.trim(seat[1]) + ",";
                            $("#s").html("Seat:" + sseat); //cnt
                            //$("#s").html("Seat:" + cnt);
                            $("#f").html("Fare:" + h.totPrice);
                            ActaxCharge -= parseInt($($("#" + $.trim(seat[1].replace(" ", "").replace("(", "").replace(")", "")) + " div")[5]).text().trim());;
                            $("#AcSvctax").html("A/C service tax:" + ActaxCharge);
                            if (cnt == 0) {
                                totSeat = "";
                                totseatFare = "";
                                original_Fare = "";
                            }
                            else {
                                totSeat = totSeat.replace($.trim(seat[1]) + ",", "");
                                totseatFare = totseatFare.replace(parseInt($.trim(fare[1])) + ",", "");
                                original_Fare = original_Fare.replace($.trim(fareArr[1]) + ",", "");
                            }
                            $(this).removeClass();
                            $(this).addClass("divVertiSleperSelectladies");
                        }
                    });

    $('#btnContinue').click(function () {
        if (totSeat == "" && totseatFare == "") {
            alert('please select your seat');
            return false;
        }
        else {
            ////if (cnt < parseInt(fun.PAX)) {
            ////    alert('You have searched for ' + parseInt(fun.PAX) + ' passenger ..please select !');
            ////    return false;
            ////}
            if (7 < parseInt(fun.PAX)) {
                alert('You have searched for ' + parseInt(fun.PAX) + ' passenger ..please select !');
                return false;
            }
            else {
                fun.PAX = cnt;

                if ($("#board>option:selected").text() == "") {
                    totSeat = "";
                    totseatFare = "";
                    original_Fare = ""; title;
                    ta_Netfare = 0; ta_Totfare = 0;
                    ta_totComm = 0; ta_totTds = 0;
                    admrkpAmt = 0; agmrkpAmt = 0;
                    alert('please select another bus operator');
                    return false;
                }
                else {
                    h.boardpoint = $("#board>option:selected").text();
                    h.droppoint = $("#drop>option:selected").text();
                    h.boardpointId = $("#board>option:selected").val();
                    h.droppointId = $("#drop>option:selected").val();
                    h.ladiesSeat = ladiesseattxt.substring(0, ladiesseattxt.length - 1);
                    var dpointk = h.droppoint.trim().substring(h.droppoint.trim().lastIndexOf('(') + 1, h.droppoint.trim().lastIndexOf(')'));
                    var bpointk = h.boardpoint.trim().substring(h.boardpoint.trim().lastIndexOf('(') + 1, h.boardpoint.trim().lastIndexOf(')'));
                    if (dpointk == "" || dpointk == undefined)
                        h.droppoint = h.droppoint + "(" + GetNewDateTimeF(h.HIDDEPARTDATE.val(), bpointk, h.DUrTimeAB) + ")";
                    fun.addSelectedSeatResult(h, fun);
                    $("#divseat").bPopup().close();
                    var cancelationpolicy = "";
                    var hResp = h.response[parseInt(fun.StLs.trim().split('_')[3])];
                    if (hResp.provider_name == "TY")
                        cancelationpolicy = h.TYcanPolicy;
                    else
                        cancelationpolicy = hResp.canPolicy_RB;
                    //       if (fun.StLs.trim().split('_')[1] == "R")
                    //    R_S_Result = setService(totSeat, hResp.serviceID, hResp.provider_name) + "_" + fun.DestCityName + "_" + fun.CityName + "_" + fun.ReturnDate + "_" + totSeat + "_" + h.ladiesSeat + "_" + totseatFare + "_" + hResp.traveler + "_" + hResp.serviceType + "_" + h.boardpoint.replace(/\_/g, ' ').trim() + "&" + h.boardpointId + "_" + h.droppoint.replace(/\_/g, ' ').trim() + "&" + h.droppointId + "_" + ta_totComm + "_" + ta_totTds + "_" + h.totPrice + "_" + ta_Totfare + "_" + ta_Netfare + "_" + hResp.idproofReq + "_" + admrkpAmt + "_" + agmrkpAmt + "_" + original_Fare + "_" + fun.PAX + "_" + hResp.provider_name + "_" + cancelationpolicy + "_" + hResp.partialCanAllowed;
                    //else
                    //    O_S_Result = setService(totSeat, hResp.serviceID, hResp.provider_name) + "_" + fun.CityName + "_" + fun.DestCityName + "_" + fun.JourneyDate + "_" + totSeat + "_" + h.ladiesSeat + "_" + totseatFare + "_" + hResp.traveler + "_" + hResp.serviceType + "_" + h.boardpoint.replace(/\_/g, ' ').trim() + "&" + h.boardpointId + "_" + h.droppoint.replace(/\_/g, ' ').trim() + "&" + h.droppointId + "_" + ta_totComm + "_" + ta_totTds + "_" + h.totPrice + "_" + ta_Totfare + "_" + ta_Netfare + "_" + hResp.idproofReq + "_" + admrkpAmt + "_" + agmrkpAmt + "_" + original_Fare + "_" + fun.PAX + "_" + hResp.provider_name + "_" + cancelationpolicy + "_" + hResp.partialCanAllowed;
                    //totSeat = "";
                    //totseatFare = "";
                    //original_Fare = ""; title;
                    //ta_Netfare = 0; ta_Totfare = 0;
                    //ta_totComm = 0; ta_totTds = 0;
                    //admrkpAmt = 0; agmrkpAmt = 0;

                    var AC_NONACSEat = "";
                    if (hResp.provider_name == "ES") {
                        ta_Netfare += ActaxCharge;
                        ta_Totfare += ActaxCharge;
                    }
                    AC_NONACSEat = setService(totSeat, hResp.serviceID, hResp.provider_name);
                    if (fun.StLs.trim().split('_')[1] == "R")
                        R_S_Result = hResp.serviceID + "_" + fun.DestCityName + "_" + fun.CityName + "_" + fun.ReturnDate + "_" + totSeat + "_" + h.ladiesSeat + "_" + totseatFare + "_" + hResp.traveler + "_" + hResp.serviceType + "_" + h.boardpoint.replace(/\_/g, ' ').trim() + "&" + h.boardpointId + "_" + h.droppoint.replace(/\_/g, ' ').trim() + "&" + h.droppointId + "_" + ta_totComm + "_" + ta_totTds + "_" + h.totPrice + "_" + ta_Totfare + "_" + ta_Netfare + "_" + hResp.idproofReq + "_" + admrkpAmt + "_" + agmrkpAmt + "_" + original_Fare + "_" + fun.PAX + "_" + hResp.provider_name + "_" + cancelationpolicy + "_" + hResp.partialCanAllowed + "_" + AC_NONACSEat.split(',')[0] + "_" + AC_NONACSEat.split(',')[1] + "_" + AC_NONACSEat.split(',')[2] + "_" + AC_NONACSEat.split(',')[3];
                    else
                        O_S_Result = hResp.serviceID + "_" + fun.CityName + "_" + fun.DestCityName + "_" + fun.JourneyDate + "_" + totSeat + "_" + h.ladiesSeat + "_" + totseatFare + "_" + hResp.traveler + "_" + hResp.serviceType + "_" + h.boardpoint.replace(/\_/g, ' ').trim() + "&" + h.boardpointId + "_" + h.droppoint.replace(/\_/g, ' ').trim() + "&" + h.droppointId + "_" + ta_totComm + "_" + ta_totTds + "_" + h.totPrice + "_" + ta_Totfare + "_" + ta_Netfare + "_" + hResp.idproofReq + "_" + admrkpAmt + "_" + agmrkpAmt + "_" + original_Fare + "_" + fun.PAX + "_" + hResp.provider_name + "_" + cancelationpolicy + "_" + hResp.partialCanAllowed + "_" + AC_NONACSEat.split(',')[0] + "_" + AC_NONACSEat.split(',')[1] + "_" + AC_NONACSEat.split(',')[2] + "_" + AC_NONACSEat.split(',')[3];

                    totSeat = "";
                    totseatFare = "";
                    original_Fare = ""; title;
                    ta_Netfare = 0; ta_Totfare = 0;
                    ta_totComm = 0; ta_totTds = 0;
                    admrkpAmt = 0; agmrkpAmt = 0;
                    ActaxCharge = 0;
                }
            }
        }
    });

    //$(document).on("mouseover", ".ES", function (e) {
    //    $("#" + $($(this)).find("div")[0].title.split('|')[0].split(':')[1].replace(" ", "").replace("(", "").replace(")", "")).show("slow");

    //});
    //$(document).on("mouseout", ".ES", function (e) {
    //    $("#" + $($(this)).find("div")[0].title.split('|')[0].split(':')[1].replace(" ", "").replace("(", "").replace(")", "")).hide("slow");
    //}); 
}
var flagOne = false; var flagTwo = false;
newSearchHelper.prototype.addSelectedSeatResult = function (V, fun1) {

    var slt = V.response[parseInt(fun1.StLs.trim().split('_')[3])];
    var dayName = new Array("Sunday", "Monday", "TuesDay", "WednesDay", "ThursDay", "FriDay", "SaturDay");
    var month = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var resultDetalsLayOut = "";
    if (fun1.StLs.trim().split('_')[1] == "R") {
        flagTwo = true;
        var jrneyDate = new Date(fun1.ReturnDate.replace(/%20/g, " ").replace(/\-/g, "/"));
        var Oomonth = month[jrneyDate.getMonth()].toUpperCase();
        resultDetalsLayOut += "<div class='f16 textaligncenter bld colorp'>From  " + fun1.DestCityName + " To  " + fun1.CityName + " Travel On " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Oomonth + " " + jrneyDate.getFullYear() + "</div>";
    }
    else {
        flagOne = true;
        var jrneyDate = new Date(fun1.JourneyDate.replace(/%20/g, " ").replace(/\-/g, "/"));
        var Rrmonth = month[jrneyDate.getMonth()].toUpperCase();
        resultDetalsLayOut += "<div class='f16 textaligncenter bld colorp'>From  " + fun1.CityName + " To  " + fun1.DestCityName + " Travel On " + dayName[jrneyDate.getDay()] + " " + jrneyDate.getDate() + " " + Rrmonth + " " + jrneyDate.getFullYear() + "</div>";
    }
    resultDetalsLayOut += "<table class='gridtable'>";
    resultDetalsLayOut += "<tr>";
    resultDetalsLayOut += "<th class='w20 padding1s'>Bus Operator</th>";
    resultDetalsLayOut += "<th class='w40 padding1s '>Service Type</th>";
    resultDetalsLayOut += "<th class='w5 padding1s'>Dep.</th>";
    resultDetalsLayOut += "<th class='w5 padding1s'>Arr.</th>";
    resultDetalsLayOut += "<th class='w8 padding1s'>Boarding</th>";
    resultDetalsLayOut += "<th class='w8 padding1s'>Dropping</th>";
    resultDetalsLayOut += "<th class='w8 padding1s'>Seat No.</th>";
    resultDetalsLayOut += "<th class='w5 padding1s'>Fare</th>";
    resultDetalsLayOut += "<tr><td colspan='8'></td></tr>";
    resultDetalsLayOut += "<td title='" + slt.traveler + "' class='w20 padding1s emp8 txtextra cursorpointer'>" + slt.traveler + "</td>";
    resultDetalsLayOut += "<td title='" + slt.serviceType + "' class='w40 padding1s emp8 txtextra cursorpointer'>" + slt.serviceType + "</td>";
    resultDetalsLayOut += "<td title='" + V.boardpoint.trim().substring(V.boardpoint.trim().lastIndexOf('(')).replace(/[(|)]/g, ' ') + "' class='w5 padding1s emp8 txtextra cursorpointer'>" + V.boardpoint.trim().substring(V.boardpoint.trim().lastIndexOf('(')).replace(/[(|)]/g, ' ') + "</td>";
    resultDetalsLayOut += "<td title='" + V.droppoint.trim().substring(V.droppoint.trim().lastIndexOf('(')).replace(/[(|)]/g, ' ') + "' class='w5 padding1s emp8 txtextra cursorpointer'>" + V.droppoint.trim().substring(V.droppoint.trim().lastIndexOf('(')).replace(/[(|)]/g, ' ') + "</td>";
    resultDetalsLayOut += "<td title='" + V.boardpoint.trim().substring(0, V.boardpoint.trim().lastIndexOf('(')) + "' class='w8 padding1s emp8 txtextra cursorpointer'>" + V.boardpoint.trim().substring(0, V.boardpoint.trim().lastIndexOf('(')) + "</td>";
    resultDetalsLayOut += "<td title='" + V.droppoint.trim().substring(0, V.droppoint.trim().lastIndexOf('(')) + "' class='w8 padding1s emp8 txtextra cursorpointer'>" + V.droppoint.trim().substring(0, V.droppoint.trim().lastIndexOf('(')) + "</td>";
    resultDetalsLayOut += "<td title='" + totSeat.trim().substring(0, totSeat.length - 1) + "' class='w8 padding1s emp8 txtextra cursorpointer'>" + totSeat.trim().substring(0, totSeat.length - 1) + "</td>";
    resultDetalsLayOut += "<td title='" + totseatFare.trim().substring(0, totseatFare.length - 1) + "' class='w5 padding1s emp8 txtextra cursorpointer'>" + h.totPrice + "</td>"; //totseatFare.trim().substring(0, totseatFare.length - 1); 
    resultDetalsLayOut += "</tr>";
    resultDetalsLayOut += "</table>";
    resultDetalsLayOut += "<div class='clear1'></div>";
    $(".bothwayselect").show();
    if (fun1.StLs.trim().split('_')[1] == "R")
        $("#ShowReturntrip").html(resultDetalsLayOut);
    else
        $("#Showonewaytrip").html(resultDetalsLayOut);
    if (fun1.TripType == "return") {
        if (flagOne == true && flagTwo == true)
            $("#BookTrip").show();
    }
    else {
        if (flagOne == true)
            $("#BookTrip").show();
    }
    $(".bothwayselect").slideDown();
}
newSearchHelper.prototype.farebrkshow = function (M, frId) {
    var mres = CreateFareBreakup(h.response[parseInt(frId.trim().split('_')[3])]);
    return mres;
}
newSearchHelper.prototype.Canpolicy = function (M, frId) {
    var canPolicy = "";
    var ress = h.response[parseInt(frId.trim().split('_')[2])];
    canPolicy = ress.canPolicy_RB;
    var providerName = ress.provider_name;
    var fare = ress.seat_farewithMarkp;
    var GetDate;
    if (frId.trim().split('_')[1] == "0")
        GetDate = M.JourneyDate;
    else
        GetDate = M.ReturnDate;
    var DeptTime = ress.departTime
    var PpartialCanAllowed = ress.partialCanAllowed;
    if (providerName == "TY")
        GetclsRouteSchedule(ress.serviceID, "Can", frId, GetDate, fare);
    else
        var policies = CreatePolilcy(canPolicy, providerName, fare, GetDate, DeptTime, PpartialCanAllowed);
    return policies;
}
newSearchHelper.prototype.Boarding = function (M, frId) {
    var jBdates;
    if (frId.trim().split('_')[1] == "0")
        jBdates = M.JourneyDate;
    else
        jBdates = M.ReturnDate;

    if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "GS")
        GsGetBoarDingPoints(h.response[parseInt(frId.trim().split('_')[2])].serviceID, "board", frId.trim().replace("Board", "Boardd"));
    else if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "RB" || h.response[parseInt(frId.trim().split('_')[2])].provider_name == "ES")
        var ress = CreateBoardingPoints(h.response[parseInt(frId.trim().split('_')[2])], "board");
    else if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "TY")
        GetclsRouteSchedule(h.response[parseInt(frId.trim().split('_')[2])].serviceID, "board", frId.trim().replace("Board", "Boardd"), jBdates);
    return ress;
}
newSearchHelper.prototype.Dropping = function (M, frId) {
    var jDdates;
    if (frId.trim().split('_')[1] == "0")
        jDdates = M.JourneyDate;
    else
        jDdates = M.ReturnDate;
    if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "GS")
        GsGetBoarDingPoints(h.response[parseInt(frId.trim().split('_')[2])].serviceID, "Drop", frId.trim().replace("Drop", "Dropp"));
    else if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "RB" || h.response[parseInt(frId.trim().split('_')[2])].provider_name == "ES")
        var ress = CreateDroppingPoints(h.response[parseInt(frId.trim().split('_')[2])], "Drop");
    else if (h.response[parseInt(frId.trim().split('_')[2])].provider_name == "TY")
        GetclsRouteSchedule(h.response[parseInt(frId.trim().split('_')[2])].serviceID, "Drop", frId.trim().replace("Drop", "Dropp"), jDdates);
    return ress;;
}
newSearchHelper.prototype.insertSelected_SeatDetails = function (OW, RW) {
    var seatUrl = UrlBase + "BS/WebService/CommonService.asmx/insertSelected_seatDetails";
    $.ajax({
        url: seatUrl,
        data: "{'Oneway':'" + OW.toString() + "','Return':'" + RW.toString() + "'}",
        dataType: "json", type: "POST",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: function (data) {

            if (data.d != "")
                window.location.href = UrlBase + "BS/CustomerInfo.aspx?ID=" + data.d + "";
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        { return textStatus; }
    });
}
newSearchHelper.prototype.UpdateDatemodifyDate = function (dateText, inst) {

    var dd = dateText.split('-');
    var selected_date = new Date(dd[1] + '/' + dd[2] + '/' + dd[0]);
    $("#month").html(month[selected_date.getMonth()].toUpperCase());
    $("#day").html(dayName[selected_date.getDay()]);
    $("#date").html($.trim(dd[2]));
    $("#year").html(selected_date.getFullYear());
    var date = dateText.split('-');
    var fdate = dd[2] + '/' + dd[1] + '/' + dd[0];
    $("#hiddepart").val(fdate);
}


newSearchHelper.prototype.UpdateDatemodifyDateR = function (dateTextR, inst) {
    var dd1 = dateTextR.split('-');
    var selected_date1 = new Date(dd1[1] + '/' + dd1[2] + '/' + dd1[0]);
    $("#Rmonth").html(month[selected_date1.getMonth()].toUpperCase());
    $("#Rday").html(dayName[selected_date1.getDay()]);
    $("#Rdate").html($.trim(dd1[2]));
    $("#Ryear").html(selected_date1.getFullYear());
    $("#Rhiddepart").val(dateTextR);
}
String.prototype.toProperCase = function () {
    return this.toLowerCase().replace(/^(.)|\s(.)/g,
        function ($1) { return $1.toUpperCase(); });
}
function getMinFare(arrayList) {
    var arrayl = new Array();
    for (var mA = 0; mA < arrayList.length; mA++) {
        arrayl.push(arrayList[mA].title.trim());
    }
    var lArr = arrayl.sort(function (a, b) {
        return (parseFloat(a) - parseFloat(b));
    });
    return lArr[0];
}
function SetMinFare(Setarray) {
    var arrayset = new Array();
    for (var mA = 0; mA < Setarray.length; mA++) {
        arrayset.push(Setarray[mA]);
    }
    var SetArr = arrayset.sort(function (a, b) {
        return (parseFloat(a) - parseFloat(b));
    });
    return SetArr[0];
}
function removeDuplicates(inputArray) {
    var i;
    var len = inputArray.length;
    var outputArray = [];
    var temp = {};
    for (i = 0; i < len; i++) {
        temp[inputArray[i]] = 0;
    }
    for (i in temp) {
        outputArray.push(i);
    }
    return outputArray;
}
function getTimeDuration(arrival, departure, departureDate) {
    var adurationTime; var durationTime; var journeyDate;
    var myArray = new Array();
    if (arrival.trim().indexOf(':') != -1 || departure.trim().indexOf(':') != -1) {
        if (arrival != "")
            myArray = arrival;
        else
            myArray = departure;
    }
    else {
        if (arrival != "" && departure == "0" && departureDate == "0") {
            //  setminutetotime(arrival)
            var hours = parseInt(parseInt(arrival) / 60);
            var reminder = parseInt(arrival) % 60;
            var djourneyDay = parseInt(parseInt(hours) / 24);
            var dhr = 0;
            dhr = parseInt(hours) % 24;
            if (djourneyDay == 1) {
                if (dhr >= 12) {
                    dhr = parseInt(dhr) - 12;
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    if (reminder < 10)
                        reminder = "0" + reminder;
                    durationTime = dhr + ':' + reminder + " " + "PM";
                }
                else {
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    if (reminder < 10)
                        reminder = "0" + reminder;
                    durationTime = dhr + ':' + reminder + " " + "AM";
                }
                //if (dhr >= 12)
                //    durationTime = dhr + ':' + reminder + " " + "PM";
                //else
                //    durationTime = dhr + ':' + reminder + " " + "AM";
            }
            else {


                if (reminder == 0) {
                    if (dhr >= 12) {
                        dhr = parseInt(dhr) - 12;
                        if (dhr < 10)
                            dhr = "0" + dhr;
                        durationTime = dhr + ':' + reminder + "0" + " " + "PM";
                    }
                    else {
                        if (dhr < 10)
                            dhr = "0" + dhr;
                        durationTime = dhr + ':' + reminder + "0" + " " + "AM";
                    }
                }
                else {
                    if (dhr >= 12) {
                        dhr = parseInt(dhr) - 12;
                        if (dhr < 10)
                            dhr = "0" + dhr;
                        if (reminder < 10)
                            reminder = "0" + reminder;
                        durationTime = dhr + ':' + reminder + " " + "PM";
                    }
                    else {
                        if (dhr < 10)
                            dhr = "0" + dhr;
                        if (reminder < 10)
                            reminder = "0" + reminder;
                        durationTime = dhr + ':' + reminder + " " + "AM";
                    }
                }
            }
            myArray.push(durationTime);
        }
        else {
            //-----------------------departure-------------------------//
            journeyDate = departureDate.split('-');
            var hours = parseInt(parseInt(departure) / 60);
            var reminder = parseInt(departure) % 60;
            var djourneyDay = parseInt(parseInt(hours) / 24);
            var dhr = parseInt(hours) % 24;
            if (reminder == 0) {
                if (dhr >= 12) {
                    dhr = parseInt(dhr) - 12;
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    durationTime = dhr + ':' + reminder + "0" + " " + "PM";
                }
                else {
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    durationTime = dhr + ':' + reminder + "0" + " " + "AM";
                }
            }
            else {
                if (dhr >= 12) {
                    dhr = parseInt(dhr) - 12;
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    if (reminder < 10)
                        reminder = "0" + reminder;
                    durationTime = dhr + ':' + reminder + " " + "PM";
                }
                else {
                    if (dhr < 10)
                        dhr = "0" + dhr;
                    if (reminder < 10)
                        reminder = "0" + reminder;
                    durationTime = dhr + ':' + reminder + " " + "AM";
                }
            }
            //--------------------------------------------//
            var d = new Date(journeyDate[1] + "/" + journeyDate[2] + "/" + journeyDate[0] + " " + durationTime);
            var date = d.getDate();
            //------------------arrival------------------//
            var hours1 = parseInt(parseInt(arrival) / 60);
            var reminder1 = parseInt(arrival) % 60;
            var aArrivalDay = parseInt(parseInt(hours1) / 24);
            var ahr = parseInt(hours1) % 24;
            if (aArrivalDay == 0) {
                if (reminder1 == 0) {
                    if (ahr >= 12) {
                        ahr = parseInt(ahr) - 12;
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        adurationTime = ahr + ':' + reminder1 + "0" + " " + "PM";
                    }
                    else {
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        adurationTime = ahr + ':' + reminder1 + "0" + " " + "AM";
                    }
                    var arrD = date;
                }
                else {
                    if (ahr >= 12) {
                        ahr = parseInt(ahr) - 12;
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        if (reminder1 < 10)
                            reminder1 = "0" + reminder1;
                        adurationTime = ahr + ':' + reminder1 + " " + "PM";
                    }
                    else {
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        if (reminder1 < 10)
                            reminder1 = "0" + reminder1;
                        adurationTime = ahr + ':' + reminder1 + " " + "AM";
                    }
                    var arrD = date;
                }
            }
            else {
                if (reminder1 == 0) {
                    if (ahr >= 12) {
                        ahr = parseInt(ahr) - 12;
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        adurationTime = ahr + ':' + reminder1 + "0" + " " + "PM";
                    }
                    else {
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        adurationTime = ahr + ':' + reminder1 + "0" + " " + "AM";
                    }
                    var arrD = d.getDate() + 1;
                }
                else {
                    if (ahr >= 12) {
                        ahr = parseInt(ahr) - 12;
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        if (reminder1 < 10)
                            reminder1 = "0" + reminder1;
                        adurationTime = ahr + ':' + reminder1 + " " + "PM";
                    }
                    else {
                        if (ahr < 10)
                            ahr = "0" + ahr;
                        if (reminder1 < 10)
                            reminder1 = "0" + reminder1;
                        adurationTime = ahr + ':' + reminder1 + " " + "AM";
                    }
                    var arrD = d.getDate() + 1;
                }
            }
            //------------------------------------------//
            var d1 = new Date(journeyDate[1] + "/" + arrD + "/" + journeyDate[0] + " " + adurationTime);
            //-------------------------------------------//
            var timeduration = d1 - d;
            var resultDiff = timeduration / 60 / 60 / 1000;
            var t = resultDiff.toString();
            var tt = t.split('.');
            if (tt[1] > 60) {
                var min = Math.round(parseInt(tt[1].substring(0, 2)) / 60);
                var rem = parseInt(tt[1].substring(0, 2)) % 60;
                resultDiff = parseInt(parseInt(tt[0]) + parseInt(min)) + "." + parseInt(rem);
            }
            myArray.push(durationTime, adurationTime, resultDiff + " " + "Hrs:mns", resultDiff);
        }
    }
    return myArray;

}
function CreateDroppingPoints(d) {
    var DropPoints = d.drPoint;
    var DroppointLayout = "";
    var drPointssss = "";
    DroppointLayout += "<div>";
    DroppointLayout += "<div class='f20'>Dropping Point Details </div>";
    DroppointLayout += "<div class='clear1'></div><hr />";
    if (DropPoints.length < 3)
        DroppointLayout += "<div class='clmaindiv'>";
    else
        DroppointLayout += "<div class='clmaindiv scroll'>";
    DroppointLayout += "<div class='BoardingPt' style='font-weight: bold;'>";
    DroppointLayout += "Dropping Point Name";
    DroppointLayout += "</div>";
    DroppointLayout += "<div class='BoardingPt' style='font-weight: bold;'>";
    DroppointLayout += "Dropping Point Location";
    DroppointLayout += "</div>";
    DroppointLayout += "<div class='BoardingPTime' style='font-weight: bold;'>";
    DroppointLayout += "Time";
    DroppointLayout += "</div>";
    DroppointLayout += "<div class='clear'></div>";
    for (var dr = 0; dr < DropPoints.length; dr++) {
        var drRes = $.parseJSON(DropPoints[dr].replace(",\n", " "));
        if (d.provider_name == "ES") {
            DroppointLayout += "<div class='BoardingPt'>";
            DroppointLayout += drRes.location;
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='BoardingPt'>";
            DroppointLayout += "";
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='BoardingPTime'>";
            DroppointLayout += drRes.time;
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='clear'></div>";
            DroppointLayout += "<hr/>";
            DroppointLayout += "<div class='clear'></div>";
        }
        else {
            DroppointLayout += "<div class='BoardingPt'>";
            DroppointLayout += drRes.bpName;
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='BoardingPt'>";
            DroppointLayout += drRes.location;
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='BoardingPTime'>";
            DroppointLayout += getTimeDuration(drRes.time, "0", "0");
            DroppointLayout += "</div>";
            DroppointLayout += "<div class='clear'></div>";
            DroppointLayout += "<hr/>";
            DroppointLayout += "<div class='clear'></div>";
        }
    }
    DroppointLayout += "</div>";
    return DroppointLayout;
}
function CreateBoardingPoints(b, boardPt) {
    var bordPointsS; var bordpointLayout = "";
    if (boardPt == "board") {
        bordPoints = b[0];
        if (b.bdPoint == undefined) {
            b.bdPoint = bordPoints;
        }
        bordpointLayout += "<div>";
        bordpointLayout += "<div class='f20'>Boarding Point Details </div>";
        bordpointLayout += "<div class='clear1'></div><hr />";
        if (b.bdPoint.length < 3)
            bordpointLayout += "<div class='clmaindiv'>";
        else
            bordpointLayout += "<div class='clmaindiv scroll'>";
        bordpointLayout += "<div class='BoardingPt bld w40'>";
        bordpointLayout += "Boarding Point Name";
        bordpointLayout += "</div>";
        bordpointLayout += "<div class='BoardingPt bld w40'>";
        bordpointLayout += "Boarding Point Location";
        bordpointLayout += "</div>";
        bordpointLayout += "<div class='BoardingPTime bld w20'>";
        bordpointLayout += "Time";
        bordpointLayout += "</div>";
    }
    else {
        bordPoints = b[1];
        if (b.bdPoint == undefined) {
            b.bdPoint = bordPoints;
        }
        bordpointLayout += "<div>";
        bordpointLayout += "<div class='f20'>Dropping Point Details </div>";
        bordpointLayout += "<div class='clear1'></div><hr />";
        if (b.bdPoint.length < 3)
            bordpointLayout += "<div class='clmaindiv'>";
        else
            bordpointLayout += "<div class='clmaindiv scroll'>";
        bordpointLayout += "<div class='BoardingPt w40' style='font-weight: bold;'>";
        bordpointLayout += "Dropping Point Name";
        bordpointLayout += "</div>";
        bordpointLayout += "<div class='BoardingPt w40' style='font-weight: bold;'>";
        bordpointLayout += "Dropping Point Location";
        bordpointLayout += "</div>";
        bordpointLayout += "<div class='BoardingPTime w20' style='font-weight: bold;'>";
        bordpointLayout += "Time";
        bordpointLayout += "</div>";
    }
    bordpointLayout += "<div class='clear'></div>";
    for (var bd = 0; bd < b.bdPoint.length; bd++) {
        var bdRes = $.parseJSON(b.bdPoint[bd].replace(",\n", " "));
        if (b.provider_name == "ES") {
            bordpointLayout += "<div class='BoardingPt w40'>";
            bordpointLayout += bdRes.location;
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='BoardingPt w40'>";
            bordpointLayout += "";
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='BoardingPTime w20'>";
            bordpointLayout += bdRes.time;
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='clear'></div>";
            bordpointLayout += "<hr/>";
            bordpointLayout += "<div class='clear'></div>";
        }
        else {
            bordpointLayout += "<div class='BoardingPt w40'>";
            if (bdRes.bpName == undefined)
                bordpointLayout += bdRes.location;
            else
                bordpointLayout += bdRes.bpName;
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='BoardingPt w40'>";
            bordpointLayout += bdRes.location;
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='BoardingPTime w20'>";
            bordpointLayout += getTimeDuration(bdRes.time, "0", "0");
            bordpointLayout += "</div>";
            bordpointLayout += "<div class='clear'></div>";
            bordpointLayout += "<hr/>";
            bordpointLayout += "<div class='clear'></div>";
        }
    }
    bordpointLayout += "</div>";
    return bordpointLayout;
}
function CreateOption(brdpoint, optionId) {
    var selectOption = "";
    selectOption += "<select name='" + optionId + "' id='" + optionId + "' style='width:150px;' class='drpBox'>";
    var bordPointsOp = brdpoint;
    for (var bd = 0; bd < bordPointsOp.length; bd++) {
        var bdRes = $.parseJSON(bordPointsOp[bd].replace(",\n", " ").replace(/\s/g, " "));
        if (bdRes.id == undefined) {
            if (bdRes.bpId != undefined)
                selectOption += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "(" + getTimeDuration(bdRes.time, "0", "0") + ")" + "</option>";
            else {
                if (bdRes.DropoffId == undefined)
                    selectOption += "<option value='" + bdRes.PickupId + "'>" + bdRes.Address + "(" + SetTYTime(bdRes.PickupTime) + ")" + "</option>";
                else
                    selectOption += "<option value='" + bdRes.DropoffId + "'>" + bdRes.DropoffName + "(" + SetTYTime(bdRes.DropoffTime) + ")" + "</option>";

                //  selectOption += "<option value='" + bdRes.DropoffId + "'>" + bdRes.DropoffName + "(" + SetTYTime(bdRes.DropoffTime) + ")" + "</option>";
            }
        }
        else {
            if (bdRes.time == "")
                selectOption += "<option value='" + bdRes.id + "'>" + bdRes.location + "(Not available )" + "</option>";
            else
                selectOption += "<option value='" + bdRes.id + "'>" + bdRes.location + "(" + bdRes.time + ")" + "</option>";
        }

    }
    selectOption += "</select>";
    return selectOption;
}
function SetTYTime(newDate) {
    var newDateTime = new Date(newDate);
    return newDateTime.format("hh:mm tt")
}
function CreateFareBreakup(f) {
    var brkLayout = "";
    brkLayout += "<div>";
    brkLayout += "<div class='clear1'></div>";
    brkLayout += "<div class='f20'>Fare Breakup Details </div>";
    brkLayout += "<div class='clear1'></div><hr />";
    if (f.provider_name == "GS")
        brkLayout += "<div><img src='Images/star_red.png' class='loader lft' /> <span class='bld'>Please Note:</span> Total fare and fare breakup wiil be display after confirmation of passenger. </div>";
    if (f.Arr_taNetFare.length < 6)
        brkLayout += "<div class='clear1'></div><div class='w100 bgw f12'>";
    else
        brkLayout += "<div class='clear1'></div><div class='scroll w100 bgmain f12'>";
    brkLayout += "<div class='clear'></div><div class='bld lft w15 padding1 brdr'>";
    brkLayout += "Fare";
    brkLayout += "</div>";
    brkLayout += "<div class='bld lft w20 padding1 brdr'>";
    brkLayout += "Srv. Charge";
    brkLayout += "</div>";
    brkLayout += "<div class='bld lft w20 padding1 brdr' style='padding-right:0;'>";
    brkLayout += "Commission";
    brkLayout += "</div>";
    brkLayout += "<div class='bld lft w15 padding1 brdr'>";
    brkLayout += "TDS";
    brkLayout += "</div>"
    brkLayout += "<div class='bld lft w15 padding1 brdr'>";
    brkLayout += "TotalFare";
    brkLayout += "</div>";
    brkLayout += "<div class='bld lft w15 padding1 brdr'>";
    brkLayout += "Net Fare";
    brkLayout += "</div>";
    brkLayout += "<div></div>";
    for (var br = 0; br < f.Arr_taNetFare.length; br++) {
        brkLayout += "<div class='clear'></div><div class='lft w15 padding1 brdr'>";
        brkLayout += Math.round(f.seat_Originalfare[br], 0);
        brkLayout += "</div>";
        brkLayout += "<div class='lft w20 padding1 brdr'>";
        brkLayout += f.Arr_serviceChrg[br];
        brkLayout += "</div>";
        brkLayout += "<div class='lft w20 padding1 brdr' style='padding-right:0;'>";
        brkLayout += f.Arr_adcomm[br];
        brkLayout += "</div>";
        brkLayout += "<div class='lft w15 padding1 brdr'>";
        brkLayout += f.Arr_taTds[br];
        brkLayout += "</div>"
        brkLayout += "<div class='lft w15 padding1 brdr'>";
        brkLayout += f.Arr_totFare[br];
        brkLayout += "</div>";
        brkLayout += "<div class='lft w15 padding1 brdr'>";
        brkLayout += f.Arr_taNetFare[br];
        brkLayout += "</div>";
        brkLayout += "<div class='clear'></div><div></div>";
    }
    brkLayout += "</div>";
    brkLayout += "</div>";
    return brkLayout;
}
function CreatePolilcy(policisss, providerName, fare, Getdate, DeptTime, ppCanAllow) {
    var DddTTT = "";
    var policy = "";
    var Cantime = "";
    var startTime = "";
    var endTime = "";
    var percentRs = "";
    var stm = 0;
    var stmSum = 0;
    var rs = "";
    rs = fare;
    var layoutOfCancelationPolicy = "";
    if (ppCanAllow == "false" || ppCanAllow == "False")
        layoutOfCancelationPolicy += "<div><span  class='f20'>Cancellation Policy</span><span class='rgt'>* Partial cancellation is NOT allowed</span></div><div class='clear1'></div><hr />";
    else
        layoutOfCancelationPolicy += "<div><span  class='f20'>Cancellation Policy</span><span class='rgt'>* Partial Cancellation Allowed</span></div><div class='clear1'></div><hr />";
    layoutOfCancelationPolicy += "<div class='clmaindiv'>";
    layoutOfCancelationPolicy += "<div class='clTime bld'>";
    layoutOfCancelationPolicy += "Cancellation Time";
    layoutOfCancelationPolicy += "</div>";
    layoutOfCancelationPolicy += "<div class='ClCharge bld'>";
    layoutOfCancelationPolicy += "Charges";
    layoutOfCancelationPolicy += "</div>";
    layoutOfCancelationPolicy += "<div class='clear'></div>";
    var cancelpolicy = "";
    var can_policy = "";
    var cplicy = "";
    var Ndate = "";
    DddTTT = Getdate + " " + getTimeDuration(DeptTime, "0", "0");
    if (providerName == "AB") {
        policy = h;
        //var policy = h.response[i].canPolicy_AB;
        var cutTime = policy[1].split(',');
        var refund = policy[2].split(',');
        if (($.trim(policy[3])).toUpperCase() == "P") {
            for (var c = 0; c <= cutTime.length - 1; c++) {
                for (var t = 0; t <= refund.length - 1; t++) {

                    if (c == t) {
                        if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                            cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                            can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "%<br />";
                            cplicy = "<div>" + cancelpolicy + "</div>";
                        }
                    }
                }
            }
        }
        else if (($.trim(policy[3])).toUpperCase() == "F") {
            for (var c = 0; c <= cutTime.length - 1; c++) {
                for (var t = 0; t <= refund.length - 1; t++) {
                    if (c == t) {
                        if ($.trim(cutTime[c]) != "" && $.trim(refund[t]) != "") {
                            cancelpolicy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                            can_policy += "Before" + " " + cutTime[c] + " " + "Hrs" + "   " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + refund[t] + "Rs/-<br />";
                            cplicy = "<div>" + cancelpolicy + "</div>";
                        }
                    }
                }
            }
        }
        layoutOfCancelationPolicy += cplicy;
    }
    else if (providerName == "RB") {
        policy = policisss.trim().split(';');
        Cantime = "";
        for (var ts = 0; ts < policy.length; ts++) {
            if (policy[ts] != "") {
                startTime = policy[ts].split(':')[0];
                endTime = policy[ts].split(':')[1];
                percentRs = policy[ts].split(':')[2];
                if (startTime == "0") {
                    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
                    layoutOfCancelationPolicy += "<div class='clTime'>After " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
                    layoutOfCancelationPolicy += "<div class='clear'></div>";
                }
                else if (endTime != "-1" && startTime[0] != "0") {
                    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
                    layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[1] + " to " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
                    layoutOfCancelationPolicy += "<div class='clear'></div>";
                }
                else if (endTime == "-1" && startTime[0] != "0") {
                    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
                    layoutOfCancelationPolicy += "<div class='clTime'> Till " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'> " + getPercentage(rs, percentRs) + "</div>";
                    layoutOfCancelationPolicy += "<div class='clear'></div>";
                }
            }
        }
    }
    else if (providerName == "GS") {
        Ndate = GetNewDate(Getdate, 0, 1, DeptTime);
        layoutOfCancelationPolicy += "<div class='clTime'>After " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, 25) + "</div>";
        layoutOfCancelationPolicy += "<div class='clear'></div>";

        Ndate = GetNewDate(DddTTT, 2, 5, DeptTime);
        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[1] + " to " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, 20) + "</div>";
        layoutOfCancelationPolicy += "<div class='clear'></div>";

        Ndate = GetNewDate(DddTTT, 6, 10, DeptTime);
        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[1] + " to " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, 15) + "</div>";
        layoutOfCancelationPolicy += "<div class='clear'></div>";

        Ndate = GetNewDate(DddTTT, 11, 20, DeptTime);
        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[1] + " to " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, 10) + "</div>";
        layoutOfCancelationPolicy += "<div class='clear'></div>";

        Ndate = GetNewDate(DddTTT, 21, 60, DeptTime);
        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[1] + " to " + Ndate.trim().split('@')[0] + "</div><div class='ClCharge'>" + getPercentage(rs, 5) + "</div>";
        layoutOfCancelationPolicy += "<div class='clear'></div>";
    }
    else if (providerName == "ES") {
        policy = $.parseJSON(policisss.replace(",\n", " "));
        for (var ts1 = 0; ts1 < policy.length; ts1++) {
            if (policy[ts1].cutoffTime == undefined) {
                layoutOfCancelationPolicy += "<div class='clTime'>" + policy[ts1] + "</div>";
                layoutOfCancelationPolicy += "<div class='clear'></div>";
            }
            else {
                if (policy[ts1].cutoffTime.indexOf('-') == -1) {
                    endTime = policy[ts1].cutoffTime.split('-')[0];
                    percentRs = policy[ts1].refundInPercentage;
                    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
                    layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
                    layoutOfCancelationPolicy += "<div class='clear'></div>";
                }
                else {
                    startTime = policy[ts1].cutoffTime.split('-')[0];
                    endTime = policy[ts1].cutoffTime.split('-')[1];
                    percentRs = policy[ts1].refundInPercentage;
                    Ndate = GetNewDateTimeFormate(DddTTT, startTime, endTime);
                    layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[0] + " to " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(rs, percentRs) + "</div>";
                    layoutOfCancelationPolicy += "<div class='clear'></div>";
                }
            }
        }
    }
    layoutOfCancelationPolicy += "</div>";
    policy = layoutOfCancelationPolicy;
    return policy;
}
function getPercentage(RS, PerCent) {
    var rsF = RS;
    var toFar = "";
    for (var trs = 0; trs < rsF.length; trs++) {
        if (rsF[trs] != null)
            toFar = toFar + "RS " + Math.round(parseFloat(parseFloat(rsF[trs]) * parseFloat(PerCent) / 100), 0) + " /";
    }
    toFar = toFar.trim().substring(0, toFar.trim().length - 1);
    return RS = toFar;
}
function GetNewDate(DDDD, StartTime, EndTime, detTime) {
    var now = new Date();
    DDDDArr = DDDD.trim().split('-');
    var Startate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2]) - parseFloat(StartTime)), parseFloat(detTime.trim().split(':')[0]), parseFloat(detTime.trim().split(':')[1]));
    var Enddate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2]) - parseFloat(EndTime)), parseFloat(detTime.trim().split(':')[0]), parseFloat(detTime.trim().split(':')[1]));
    //var OrigDS = Startate.toLocaleString();
    //var OrigDE = Enddate.toLocaleString();
    var OrigDS = Startate.format("dd/MM/yyyy hh:mm tt");
    var OrigDE = Enddate.format("dd/MM/yyyy hh:mm tt");
    return DDDD = OrigDS + "@" + OrigDE;
}
function GetNewDateTimeFormate(DDDD, StartTime, EndTime) {
    var now = new Date();
    DDDDArr = DDDD.trim().split('-');
    var Dttt = parseFloat(DDDD.trim().split(' ')[1].trim().split(':')[0]);
    var DtMM = parseFloat(DDDD.trim().split(' ')[1].trim().split(':')[1]);
    if (DDDD.trim().split(' ')[2] == "PM")
        Dttt = parseFloat(Dttt) + 12;
    else
        Dttt = Dttt;
    var Startate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(StartTime)), parseFloat(DtMM));
    var Enddate = new Date(parseFloat(DDDDArr[0]), (parseFloat(DDDDArr[1]) - 1), (parseFloat(DDDDArr[2])), (parseFloat(Dttt) - parseFloat(EndTime)), parseFloat(DtMM));
    var OrigDS = Startate.format("dd/MM/yyyy hh:mm tt");// Startate.toLocaleString();
    var OrigDE = Enddate.format("dd/MM/yyyy hh:mm tt");//Enddate.toLocaleString();
    return DDDD = OrigDS + "@" + OrigDE;
}
function getDetailsLayout() {
    var ODetailsLayOut = "";
    ODetailsLayOut += "<div class='cursorpointer'><img src='" + UrlBase + "BS/Images/cls.png' class='closeDetails' align='right' />";
    ODetailsLayOut += "<div>";
    ODetailsLayOut += "<div class='tripbutton1 removeDetailsCss' id='clPolicy' style='float: left; padding-left: 20px;  line-height: 25px;'>Cancellation Policy</div>";
    ODetailsLayOut += "<div class='tripbutton2 removeDetailsCss' id='FrBreakup' style='float: left; padding-left: 20px;  line-height: 25px;'>Fare BreakUp</div>";
    ODetailsLayOut += "<div class='tripbutton2 removeDetailsCss' id='BrdPoints' style='float: left; padding-left: 20px;  line-height: 25px;'>Boarding Points</div>";
    ODetailsLayOut += "<div class='tripbutton2 removeDetailsCss' id='DrdPoints' style='float: left; padding-left: 20px;  line-height: 25px;'>Dropping Points</div>";
    ODetailsLayOut += "</div><div class='clear'></div><div><hr class='headerline'/></div>";
    ODetailsLayOut += "<div class='clear'>";
    ODetailsLayOut += "<div id='showdata'></div>";
    ODetailsLayOut += "</div>";
    return ODetailsLayOut;
}
function GsGetBoarDingPoints(serviceId, boardPt, idforBind) {
    var seatlrt;
    var Url2 = UrlBase + "BS/WebService/CommonService.asmx/getBoardIngDropping";
    $.ajax({
        url: Url2,
        contentType: "application/json; charset=utf-8",
        data: "{'serviceId':'" + serviceId + "'}",
        dataType: "json",
        type: "POST",
        async: true,
        success: function (data) {
            seatlrt = data.d;
            seatlrt = CreateBoardingPoints(seatlrt, boardPt);
            $("#" + idforBind).html(seatlrt);

        }
    });
}
function GetclsRouteSchedule(serviceId, boardPt, idforBind, jjDate, faressssss) {
    var seatlrtsRes;
    if (TserviceId != parseInt(serviceId.split('_')[0])) {
        var Url2 = UrlBase + "BS/WebService/CommonService.asmx/ForGetclsRouteSchedule2";
        $.ajax({
            url: Url2,
            contentType: "application/json; charset=utf-8",
            data: "{'SearchId':'" + parseInt(serviceId.split('_')[0]) + "','jDates':'" + jjDate + "'}",
            dataType: "json",
            type: "POST",
            async: true,
            success: function (data) {
                seatlrts = data.d;
                if (idforBind != "") {
                    seatlrtsRes = FunCreateSearchSedule(seatlrts, boardPt, faressssss);
                    $("#" + idforBind.replace("Can", "Cann")).html(seatlrtsRes);
                }
                TserviceId = parseInt(serviceId.split('_')[0]);
            }
        });
    }
    else {
        seatlrtsRes = FunCreateSearchSedule(seatlrts, boardPt, faressssss);
        if ($("#" + idforBind.replace("Can", "Cann")).length == 0) {
            return seatlrtsRes;
        }
        else
            $("#" + idforBind.replace("Can", "Cann")).html(seatlrtsRes)
        TserviceId = parseInt(serviceId.split('_')[0]);

    }
}
function GsPoints(serviceId) {
    var seatlrt; var DDseatArrangement = "";
    var Url2 = UrlBase + "BS/WebService/CommonService.asmx/getBoardIngDropping";
    $.ajax({
        url: Url2,
        contentType: "application/json; charset=utf-8",
        data: "{'serviceId':'" + serviceId + "'}",
        dataType: "json",
        type: "POST",
        async: true,
        success: function (data) {
            seatlrt = data.d;
            var DropPoints = seatlrt[1];
            var selectOptionBoard = "";
            selectOptionBoard += "<select name='board' id='board' style='width:150px;' class='drpBox'>";
            for (var bd = 0; bd < seatlrt[0].length; bd++) {
                var bdRes = $.parseJSON(seatlrt[0][bd]);
                selectOptionBoard += "<option value='" + bdRes.bpId + "'>" + bdRes.location + "(" + getTimeDuration(bdRes.time, "0", "0") + ")" + "</option>";
            }
            selectOptionBoard += "</select>";
            var selectOptionDrop = "";
            selectOptionDrop += "<select name='drop' id='drop' style='width:150px;' class='drpBox'>";
            for (var dr = 0; dr < seatlrt[1].length; dr++) {
                var drRes = $.parseJSON(seatlrt[1][dr]);
                selectOptionDrop += "<option value='" + drRes.dpId + "'>" + drRes.location + "(" + getTimeDuration(drRes.time, "0", "0") + ")" + "</option>";
            }
            selectOptionDrop += "</select>";
            DDseatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + selectOptionBoard + "</div>";
            DDseatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 20px;'>" + selectOptionDrop + "</div>";
            return $("#dibBoard").html(DDseatArrangement);
        }
    });
}
function setMatrixLayout(operator, serviceType, row1, col1, arrlist) {
    var arrlist1 = arrlist;
    var arrlist2 = arrlist;
    if (serviceType == arrlist1[parseInt(row1)].serviceType && operator == arrlist1[parseInt(col1)].traveler) {
        return SetMinFare(arrlist1[parseInt(col1)].seat_farewithMarkp);
    }
    else {
        return "not available";
    }
    //if (serviceType + "," + operator == arrlist1[parseInt(row1)].serviceType + "," + arrlist1[parseInt(col1)].traveler) {
    //    return SetMinFare(arrlist1[parseInt(col1)].seat_farewithMarkp);
    //}
    //else {
    //    return "not";
    //}

}
function setonclickCss(id) {
    if (id == "onewAySrc") {
        $("#ReturnwAySrc").removeClass("modifytxt");
        $("#ReturnwAySrc").addClass("modifytxt1");
        $("#onewAySrc").removeClass("modifytxt1");
        $("#onewAySrc").addClass("modifytxt");
        $("#oneWayDiv .list-item").removeClass("border-list1");
        $("#oneWayDiv .list-item").addClass("border-list");
        $("#RoundDivs .list-item").removeClass("border-list");
        $("#RoundDivs .list-item").addClass("border-list1");
        $("#ServiceSort2").hide(); $("#TravelerSort2").hide(); $("#ServiceSort1").show(); $("#TravelerSort1").show();
        $("#divsliderShow1").show(); $("#divsliderShow2").hide();
        $("#DivDeptshow").show(); $("#DivDeptshow2").hide();
        $("#DivArrshow").show(); $("#DivArrshow2").hide();
        $("#DivAvlshow").show(); $("#DivAvlshowR").hide();
    }
    else {
        $("#onewAySrc").removeClass("modifytxt");
        $("#onewAySrc").addClass("modifytxt1");
        $("#ReturnwAySrc").removeClass("modifytxt1");
        $("#ReturnwAySrc").addClass("modifytxt");
        $("#oneWayDiv .list-item").removeClass("border-list");
        $("#oneWayDiv .list-item").addClass("border-list1");
        $("#RoundDivs .list-item").removeClass("border-list1");
        $("#RoundDivs .list-item").addClass("border-list");
        $("#ServiceSort1").hide(); $("#TravelerSort1").hide(); $("#ServiceSort2").show(); $("#TravelerSort2").show();
        $("#divsliderShow2").show(); $("#divsliderShow1").hide();
        $("#DivDeptshow2").show(); $("#DivDeptshow").hide();
        $("#DivArrshow2").show(); $("#DivArrshow").hide();
        $("#DivAvlshowR").show(); $("#DivAvlshow").hide();
    }
    FunShowhideArrow();
}
function convertToMin(ttimes) {
    var A = ttimes.toString();
    A = ttimes.split(':');
    var Hr = A[0];
    A = A[1].split(' ');
    var min = A[0];
    if ((A[1] == "AM") & (Hr == 12)) { Hr = "00"; }
    if (A[1] == "PM") { Hr = parseInt(Hr, 10) + 12 }
    return parseInt(Hr * 60) + parseInt(min);
}
function convertToMinGS(ttimess) {
    var A = ttimess.toString();
    A = ttimess.split(':');
    var Hr = A[0];
    var min = A[1];
    return parseInt(Hr * 60) + parseInt(min);
}
function SetMinHHAMPM(a) {
    var tm = "";
    var hours = parseFloat(String(a).split(':')[0]);
    var minutes = String(a).split(':')[1];
    AMPM = (hours >= 12) ? 'PM' : 'AM';
    hhhh = (hours >= 12) ? hours - 12 : hours;
    oooo = (hhhh >= 10) ? "" : "0";
    if (minutes.split(' ')[1] == undefined) {
        tm = oooo + hhhh + ":" + minutes + " " + AMPM;
    }
    else {
        tm = oooo + hhhh + ":" + minutes;
    }
    return tm;
}
function fun_SetMinMaxFare(fareList) {
    var priceList = ""; priceList = fareList; var priceArr = new Array();
    if (priceList.length != 0) {
        var arrPrice = new Array();
        for (var pri = 0; pri < priceList.length; pri++) {
            if ($(priceList[pri]).find("div").length != 0)

                arrPrice.push({ id: pri, price: $(priceList[pri]).text().trim().replace("SELECT | SOLD-OUT", "") })
        }
        var sortprice = arrPrice.sort(function (a, b) {
            return parseFloat(a.price) - parseFloat(b.price)
        });

        priceArr.push(parseFloat(sortprice[0].price));
        priceArr.push(parseFloat(sortprice[parseInt(sortprice.length) - 1].price));
    }
    else {
        priceArr.push(0);
        priceArr.push(0);
    }
    return priceArr;
}
function fun_SetMinMaxDeptTime(DeptTimeList) {
    var DeptList1 = ""; var DeptListArr = new Array();
    DeptList1 = DeptTimeList;
    if (DeptList1.length != 0) {
        var arrDept1 = new Array();
        for (var Dpt = 0; Dpt < DeptList1.length; Dpt++) {
            arrDept1.push({ id: Dpt, Dept: $(DeptList1[Dpt]).text().trim() });
        }
        var sortdept = arrDept1.sort(function (ce, de) {
            return parseFloat(ce.Dept) - parseFloat(de.Dept)
        });
        DeptListArr.push(parseFloat(sortdept[0].Dept));
        DeptListArr.push(parseFloat(sortdept[parseInt(sortdept.length) - 1].Dept));
    }
    else {
        DeptListArr.push(0);
        DeptListArr.push(0);
    }
    return DeptListArr;
}
function SetMinMaxArrTime(ArrTimeList) {
    var ArrList1 = ""; var ArrArray = new Array();
    ArrList1 = ArrTimeList;
    if (ArrList1.length != 0) {
        var arrArrt1 = new Array();
        for (var Apt = 0; Apt < ArrList1.length; Apt++) {
            arrArrt1.push({ id: Apt, Arr: $(ArrList1[Apt]).text().trim() });
        }
        var sortArr = arrArrt1.sort(function (Ae, Be) {
            return parseFloat(Ae.Arr) - parseFloat(Be.Arr)
        });
        ArrArray.push(parseFloat(sortArr[0].Arr));
        ArrArray.push(parseFloat(sortArr[parseInt(sortArr.length) - 1].Arr));
    }
    else {
        ArrArray.push(0);
        ArrArray.push(0);
    }
    return ArrArray;
}
function fun_SetMinMaxAvailableSeat(availableSeatList) {
    var AvlList1 = ""; var AvlArray = new Array();
    AvlList1 = availableSeatList;
    if (AvlList1.length != 0) {
        var arrAvl1 = new Array();
        for (var Avt = 0; Avt < AvlList1.length; Avt++) {
            var avl1 = $(AvlList1[Avt]).text().trim();
            arrAvl1.push({ id: Avt, Avl: avl1 });
        }
        var sortAVl = arrAvl1.sort(function (Av, Bv) {
            return parseFloat(Av.Avl) - parseFloat(Bv.Avl)
        });
        AvlArray.push(parseFloat(sortAVl[0].Avl));
        AvlArray.push(parseFloat(sortAVl[parseInt(sortAVl.length) - 1].Avl));
    }
    else {
        AvlArray.push(0);
        AvlArray.push(0);
    }
    return AvlArray;
}
function FunShowhideArrow() {
    $("#divsliderShow1").hide(); $("#divsliderShow2").hide();
    $("#DivDeptshow").hide(); $("#DivDeptshow2").hide();
    $("#DivArrshow").hide(); $("#DivArrshow2").hide();
    $("#DivAvlshow").hide(); $("#DivAvlshowR").hide();
    $(".filters img")[0].src = Imgnext;
    $(".filters img")[1].src = Imgnext;
    $(".filters img")[2].src = Imgnext;
    $(".filters img")[3].src = Imgnext;

}
function FunCreateSearchSedule(seduleRes, seduleType, faressss) {
    var bordpointLayout = ""; DroppointLayout = ""; var mainLayout = "";
    if (seduleRes.Route != null) {
        if (seduleType == "board") {

            bordpointLayout += "<div class='f20'>Boarding Point Details </div>";
            bordpointLayout += "<div class='clear1'></div><hr />";
            if (seduleRes.Pickup == null) {
                bordpointLayout += "<div>Boarding point is not available </div>";
            }
            else {
                if (seduleRes.Pickup.length < 2)
                    bordpointLayout += "<div class='clmaindiv'>";
                else
                    bordpointLayout += "<div class='clmaindiv scroll'>";
                bordpointLayout += "<table>";
                bordpointLayout += "<tr>";
                bordpointLayout += "<td class='bld bdpointss'>Name </td><td class='bld bdpointss'>LandMark;</td><td class='bld bdpointss'>Address </td><td class='bld bdpointss'>Contact No </td><td class='bld bdpointss'>Time </td>";
                bordpointLayout += "</tr>";
                for (var bd = 0; bd < seduleRes.Pickup.length; bd++) {
                    //   var Datetime = new Date(seduleRes.Pickup[bd].PkpTime);                 
                    var ddd = SetNewdateTime(seduleRes.Pickup[bd].PkpTime.split('T')[1]);
                    //   var ddd = Datetime.format("hh:mm tt");

                    bordpointLayout += "<tr style='border-bottom: thin solid #ddd;'>";
                    bordpointLayout += "<td class='bdpointss w20'>" + seduleRes.Pickup[bd].PickupName.replace(/\,/g, " ") + " &nbsp;</td><td class='bdpointss w20'>" + seduleRes.Pickup[bd].Landmark.replace(/\,/g, " ") + "&nbsp;</td><td class='bdpointss w20'>" + seduleRes.Pickup[bd].Address.replace(/\,/g, " ") + "&nbsp;</td><td  class='bdpointss w20'>" + seduleRes.Pickup[bd].Phone.replace(/\,/g, " ") + "&nbsp;</td><td class='bdpointss w20'>" + ddd + "&nbsp;</td>";
                    bordpointLayout += "</tr>";
                }
                bordpointLayout += "</table>";
                bordpointLayout += "</div>"
            }
            mainLayout = bordpointLayout;
        }
        else if (seduleType == "Drop") {
            DroppointLayout += "<div class='f20'>Dropping Point Details </div>";
            DroppointLayout += "<div class='clear1'></div><hr />";
            if (seduleRes.Dropoff == null) {
                DroppointLayout += "<table>";
                DroppointLayout += "<tr>";
                DroppointLayout += "<td class='bld'>Name</td><td style='width:100px'></td><td style='width:100px'></td><td style='width:100px' class='bld rgt'>Time</td>";
                DroppointLayout += "</tr>";
                //var Datetime = new Date(seduleRes.Route.ArrTime);
                //var ddd = Datetime.format("hh:mm tt");
                var ddd = SetNewdateTime(seduleRes.Route.ArrTime.split('T')[1]);
                DroppointLayout += "<tr style='border-bottom: thin solid #ddd;'>";
                DroppointLayout += "<td>" + seduleRes.Route.ToCityName + "</td><td style='width:100px'></td><td style='width:100px'></td><td style='width:100px' class='rgt'>" + ddd + "</td>";
                DroppointLayout += "</tr>";
                DroppointLayout += "</table>";
            }
            else {
                DroppointLayout += "<table>";
                DroppointLayout += "<tr>";
                DroppointLayout += "<td class='bld'>Name</td><td style='width:100px'></td><td style='width:100px'></td><td style='width:100px' class='bld rgt'>Time</td>";
                DroppointLayout += "</tr>";
                for (var dr = 0; dr < seduleRes.Dropoff.length; dr++) {
                    //var Datetime = new Date(seduleRes.Dropoff[dr].DrpTime);
                    //var ddd = Datetime.format("hh:mm tt");
                    var ddd = SetNewdateTime(seduleRes.Dropoff[dr].DrpTime.split('T')[1]);
                    DroppointLayout += "<tr style='border-bottom: thin solid #ddd;'>";
                    DroppointLayout += "<td>" + seduleRes.Dropoff[dr].DropoffName.replace(/\,/g, " ") + "</td><td style='width:100px'></td><td style='width:100px'></td><td style='width:100px' class='rgt'>" + ddd + "</td>";
                    DroppointLayout += "</tr>";
                }
                DroppointLayout += "</table>";
            }
            mainLayout = DroppointLayout;
        }
        else if (seduleType == "Can") {
            var stmSum = 0; var Ndate = ""; var ppCanAllow = "false";
            var layoutOfCancelationPolicy = "";
            if (ppCanAllow == "false")
                layoutOfCancelationPolicy += "<div><span  class='f20'>Cancellation Policy</span><span class='rgt'>* Partial cancellation is NOT allowed</span></div><div class='clear1'></div><hr />";
            else
                layoutOfCancelationPolicy += "<div><span  class='f20'>Cancellation Policy</span><span class='rgt'>* Partial Cancellation Allowed</span></div><div class='clear1'></div><hr />";
            layoutOfCancelationPolicy += "<div class='clmaindiv'>";
            layoutOfCancelationPolicy += "<div class='clTime bld'>";
            layoutOfCancelationPolicy += "Cancellation Time";
            layoutOfCancelationPolicy += "</div>";
            layoutOfCancelationPolicy += "<div class='ClCharge bld'>";
            layoutOfCancelationPolicy += "Charges";
            layoutOfCancelationPolicy += "</div>";
            layoutOfCancelationPolicy += "<div class='clear'></div>";
            for (var ts = 0; ts < seduleRes.CancellationCharges.length; ts++) {
                policy = seduleRes.CancellationCharges[ts];
                stm = parseInt(stmSum);
                startTime = parseInt(stm);
                endTime = parseInt(policy.MinsBeforeDeparture) / 60;
                var ddddDate = "";
                //   var Datetime = new Date(seduleRes.Route.DepTime);

                ddddDate = seduleRes.Route.DepTime.split('T')[0] + " " + SetNewdateTime(seduleRes.Route.DepTime.split('T')[1]);//Datetime.format("yyyy-MM-dd hh:mm tt");
                Ndate = GetNewDateTimeFormate(ddddDate, startTime, endTime);
                if (startTime == endTime) {
                    if (policy.ChargeFixed == "0")
                        layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(faressss, parseInt(100 - parseInt(policy.ChargePercentage))) + "</div>";
                    else
                        layoutOfCancelationPolicy += "<div class='clTime'>Before " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>RS " + policy.ChargeFixed + "</div>";
                }
                else {
                    if (policy.ChargeFixed == "0")
                        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[0] + " to " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>" + getPercentage(faressss, parseInt(100 - parseInt(policy.ChargePercentage))) + "</div>";
                    else
                        layoutOfCancelationPolicy += "<div class='clTime'>Between " + Ndate.trim().split('@')[0] + " to " + Ndate.trim().split('@')[1] + "</div><div class='ClCharge'>RS " + policy.ChargeFixed + "</div>";
                }
                layoutOfCancelationPolicy += "<div class='clear'></div>";
                stmSum = parseInt(policy.MinsBeforeDeparture) / 60;
                startTime = 0;
                endTime = 0;

            }
            mainLayout = layoutOfCancelationPolicy;
        }
        else if (seduleType == "OPTION") {
            var selectOptionBoard = ""; var selectOptionDrop = ""; var DDseatArrangement = "";
            selectOptionBoard += "<select name='board' id='board' style='width:150px;' class='drpBox'>";
            if (seduleRes.Pickup != null) {
                for (var ob = 0; ob < seduleRes.Pickup.length; ob++) {
                    //var Datetime = new Date(seduleRes.Pickup[ob].PkpTime);
                    //var ddd = Datetime.format("hh:mm tt");

                    var ddd = SetNewdateTime(seduleRes.Pickup[ob].PkpTime.split('T')[1]);
                    selectOptionBoard += "<option value='" + seduleRes.Pickup[ob].PickupId + "'>" + seduleRes.Pickup[ob].PickupName + "(" + ddd + ")" + "</option>";
                }
            }
            else {
                //var Datetime = new Date(seduleRes.Route.DepTime);
                //var ddd = Datetime.format("hh:mm tt");
                var ddd = SetNewdateTime(seduleRes.Route.DepTime.split('T')[1]);
                selectOptionBoard += "<option value='" + seduleRes.Route.FromCityId + "'>" + seduleRes.Route.FromCityName + "(" + ddd + ")" + "</option>";
            }
            selectOptionBoard += "</select>";
            selectOptionDrop += "<select name='drop' id='drop'  style='width:150px;' class='drpBox'>";
            if (seduleRes.Dropoff != null) {
                for (var od = 0; od < seduleRes.Dropoff.length; od++) {
                    // var Datetime = new Date(seduleRes.Dropoff[od].DrpTime);
                    //   var dddo = Datetime.format("hh:mm tt");
                    var dddo = SetNewdateTime(seduleRes.Dropoff[od].DrpTime.split('T')[1]);
                    selectOptionDrop += "<option value='" + seduleRes.Dropoff[od].DropoffId + "'>" + seduleRes.Dropoff[od].DropoffName.replace(/\,/g, " ") + "(" + dddo + ")" + "</option>";
                }
            }
            else {
                //var Datetime = new Date(seduleRes.Route.ArrTime);
                //var dddo = Datetime.format("hh:mm tt");
                var dddo = SetNewdateTime(seduleRes.Route.ArrTime.split('T')[1]);
                selectOptionDrop += "<option value='" + seduleRes.Route.ToCityId + "'>" + seduleRes.Route.ToCityName + "(" + dddo + ")" + "</option>";
            }
            selectOptionDrop += "</select>";
            DDseatArrangement += "<div style='font-weight:bold; color:#20313F; font-size:14px;float:left;margin-left: 10px;'>" + selectOptionBoard + "</div>";
            DDseatArrangement += "<div style='font-weight:bold;  color:#20313F; font-size:14px;float:left;margin-left: 20px;'>" + selectOptionDrop + "</div>";
            mainLayout = DDseatArrangement;
            var CanCharge = new Array();
            for (var plc = 0; plc < seduleRes.CancellationCharges.length; plc++) {
                policy = seduleRes.CancellationCharges[plc];
                CanCharge.push("{\"ChargeFixed\":\"" + policy.ChargeFixed + "\",\"ChargePercentage\":\"" + policy.ChargePercentage + "\",\"MinsBeforeDeparture\":\"" + policy.MinsBeforeDeparture + "\"}");
            }
            h.TYcanPolicy = CanCharge;
        }
    }
    else {
        mainLayout = "<div class='bld'>please select another operator .</div>";
    }
    return mainLayout;
}
function FunSetAminities(Aminities) {
    var Aminity = "";
    var strAminity = "";
    Aminity = Aminities;
    if (Aminity.indexOf("Charging Point") != -1 || Aminity.indexOf("CHARGING POINT") != -1)
        strAminity += "<img title='Charging Point' src='" + UrlBase + "BS/Images/charging.png' class='loader' align='left'/>";
    if (Aminity.indexOf("Blanket") != -1 || Aminity.indexOf("Blancket") != -1 || Aminity.indexOf("blanket") != -1 || Aminity.indexOf("BLANKET") != -1)
        strAminity += "<img title='Blanket' src='" + UrlBase + "BS/Images/blanket.png' class='loader' align='left'/>";
    if (Aminity.indexOf("WI-FI") != -1 || Aminity.indexOf("WiFi") != -1 || Aminity.indexOf("Wi Fi") != -1)
        strAminity += "<img title='Wi Fi' src='" + UrlBase + "BS/Images/wifi.png' class='loader' align='left'/>";
    if (Aminity.indexOf("Movies") != -1 || Aminity.indexOf("DVD") != -1 || Aminity.indexOf("Personal Entertainment Screen") != -1 || Aminity.indexOf("PERSONAL ENTERTAINMENT SCREEN") != -1 || Aminity.indexOf("Personal Screen") != -1) {
        if (Aminity.indexOf("Personal Entertainment Screen") != -1 || Aminity.indexOf("PERSONAL ENTERTAINMENT SCREEN") != -1 || Aminity.indexOf("Personal Screen") != -1)
            strAminity += "<img  title='Personal Entertainment Screen' src='" + UrlBase + "BS/Images/television.png' class='loader' align='left'/>";
        else
            strAminity += "<img title='Movies' src='" + UrlBase + "BS/Images/television.png' class='loader' align='left'/>";
    }
    if (Aminity.indexOf("Water Bottle") != -1 || Aminity.indexOf("Water") != -1 || Aminity.indexOf("WATER") != -1)
        strAminity += "<img title='Water Bottle' src='" + UrlBase + "BS/Images/waterbottle.png' class='loader' align='left'/>";
    return strAminity;
}
$(document).on("mouseover", ".farehide", function (e) {
    var fid = this.id;
    $("#" + this.id.replace("Fare_O_", "HFare_O_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_O_", "img_O_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_O_", "select_O_")).toggleClass("hide");
    //$(".sh").toggleClass("hide");
});
$(document).on("mouseout", ".farehide", function (e) {
    var fid = this.id;
    $("#" + this.id.replace("Fare_O_", "HFare_O_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_O_", "img_O_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_O_", "select_O_")).toggleClass("hide");
    //$(".sh").toggleClass("hide");
});
$(document).on("mouseover", ".Rfarehide", function (e) {
    var fid = this.id;
    $("#" + this.id.replace("Fare_R_", "HFare_R_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_R_", "img_R_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_R_", "select_R_")).toggleClass("hide");
    //$(".sh").toggleClass("hide");
});
$(document).on("mouseout", ".Rfarehide", function (e) {
    var fid = this.id;
    $("#" + this.id.replace("Fare_R_", "HFare_R_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_R_", "img_R_")).toggleClass("hide");
    $("#" + this.id.replace("Fare_R_", "select_R_")).toggleClass("hide");
    //$(".sh").toggleClass("hide");
});
$(document).on("click", "#alllservice", function (e) {
    $(".clsservices").toggleClass("hide");
    $(".sh").toggleClass("hide");
});
$(document).on("click", "#allloperators", function (e) {
    $(".clsoperators").toggleClass("hide");
    $(".op").toggleClass("hide");

});
$(document).on("click", ".cls", function (e) {
    $("#divseat").bPopup().close();
});
$(document).on("click", ".closeDetails", function (e) {
    $("#forDetails").bPopup().close();
});
$(document).on("click", ".viewShow", function (e) {
    $(".bothwayselect").slideDown();
});
$(document).on("mouseover", ".OSelect,.OOSelect", function (e) {

    //$("#" + this.id.replace("select_O_", "HFare_O_")).toggleClass("hide");
    //$("#" + this.id.replace("select_O_", "img_O_")).toggleClass("hide");
    var Commbreakup = "";
    var queries = window.location.search.substring(1);
    var s = $(this).attr("rel").split(',');

    Commbreakup += "Remaining Seat: " + $.trim(s[0]) + "";

    $("#" + this.id.replace("select", "AvlSeat")).show();
    $("#" + this.id.replace("select", "AvlSeat")).html(Commbreakup);
    if (h.response[parseInt(this.id.trim().split('_')[3])].provider_name == "TY") {
        //if(h.response.f.provider_name = "TY"){
        var svcId = h.response[parseInt(this.id.trim().split('_')[3])].serviceID;
        var JJJDates = h.response[parseInt(this.id.trim().split('_')[3])].serviceID.split(',')[1].substring(h.response[parseInt(this.id.trim().split('_')[3])].serviceID.split(',')[1].lastIndexOf('~') + 1)
        GetclsRouteSchedule(svcId, "OPTION", "", queries.trim().split('&')[4].split('=')[1].replace(/%20/g, " "), "")
    }
});
$(document).on("mouseout", ".OSelect,.OOSelect", function (e) {
    //$("#" + this.id.replace("select_O_", "HFare_O_")).toggleClass("hide");
    //$("#" + this.id.replace("select_O_", "img_O_")).toggleClass("hide");
    //$("#" + this.id).toggleClass("hide");
    //$("#" + this.id.replace("select", "AvlSeat")).hide();
});
$(document).on("mouseover", ".RSelect", function (e) {
    $("#" + this.id).toggleClass("hide");
    $("#" + this.id.replace("select_R_", "HFare_R_")).toggleClass("hide");
    $("#" + this.id.replace("select_R_", "img_R_")).toggleClass("hide");
    var Commbreakup = "";
    var queriesr = window.location.search.substring(1);
    var s = $(this).attr("rel").split(',');
    Commbreakup += "Remaining Seat: " + $.trim(s[0]) + "";
    $("#" + this.id.replace("select", "AvlSeat")).show();
    $("#" + this.id.replace("select", "AvlSeat")).html(Commbreakup);
    if (h.response[parseInt(this.id.trim().split('_')[3])].provider_name == "TY") {
        var svcId = h.response[parseInt(this.id.trim().split('_')[3])].serviceID;
        var JJJDates = h.response[parseInt(this.id.trim().split('_')[3])].serviceID.split(',')[1].substring(h.response[parseInt(this.id.trim().split('_')[3])].serviceID.split(',')[1].lastIndexOf('~') + 1)
        GetclsRouteSchedule(svcId, "OPTION", "", queriesr.trim().split('&')[8].split('=')[1].replace(/%20/g, " "), "")
    }
});
$(document).on("mouseout", ".RSelect", function (e) {
    $("#" + this.id).toggleClass("hide");
    $("#" + this.id.replace("select_R_", "HFare_R_")).toggleClass("hide");
    $("#" + this.id.replace("select_R_", "img_R_")).toggleClass("hide");
    $("#" + this.id.replace("select", "AvlSeat")).hide();
});
$(document).on("click", "#ShowallserViceType0", function (e) {
    var indexpp = 0;
    $("#ServiceSort1").html(Fun_ByserViceTypes(indexpp));
});
$(document).on("click", "#ShowallserViceTypeH0", function (e) {
    var indexpp = 0;
    $("#ServiceSort1").html(Fun_sorByserViceTypes(indexpp));
});
$(document).on("click", "#ShowallserViceType1", function (e) {
    var indexpp = 1;
    $("#ServiceSort2").html(Fun_ByserViceTypes(indexpp));
});
$(document).on("click", "#ShowallserViceTypeH1", function (e) {
    var indexpp = 1;
    $("#ServiceSort2").html(Fun_sorByserViceTypes(indexpp));
});
$(document).on("click", "#ShowallTravelerType0", function (e) {
    var indextt = 0;
    $("#TravelerSort1").html(Fun_TravelsersType(indextt));
});
$(document).on("click", "#ShowallTravelerTypeH0", function (e) {
    var indextt = 0;
    $("#TravelerSort1").html(Fun_sorByTravelsersType(indextt));
});
$(document).on("click", "#ShowallTravelerType1", function (e) {
    var indextt = 1;
    $("#TravelerSort2").html(Fun_TravelsersType(indextt));
});
$(document).on("click", "#ShowallTravelerTypeH1", function (e) {
    var indextt = 1;
    $("#TravelerSort2").html(Fun_sorByTravelsersType(indextt));
});
var prfilters = false; var Dtfilters = false; var Atfilters = false; var Avfilters = false;
$(".filters").click(function () {
    var id = this.id;
    var type = $(".modifytxt")[0].id;

    if (id == "filtersPrice") {
        if (prfilters == false) {
            if (type == "onewAySrc")
                $("#divsliderShow1").show();
            else
                $("#divsliderShow2").show();
            prfilters = true;
            $("#" + id + " img")[0].src = Imgnext1;
        }
        else {
            if (type == "onewAySrc")
                $("#divsliderShow1").hide();
            else
                $("#divsliderShow2").hide();
            prfilters = false;
            $("#" + id + " img")[0].src = Imgnext;
        }
    }
    else if (id == "filterDepttime") {
        if (Dtfilters == false) {
            if (type == "onewAySrc")
                $("#DivDeptshow").show();
            else
                $("#DivDeptshow2").show();
            Dtfilters = true;
            $("#" + id + " img")[0].src = Imgnext1;
        }
        else {
            if (type == "onewAySrc")
                $("#DivDeptshow").hide();
            else
                $("#DivDeptshow2").hide();
            Dtfilters = false;
            $("#" + id + " img")[0].src = Imgnext;
        }
    }
    else if (id == "filterArrtime") {
        if (Atfilters == false) {
            if (type == "onewAySrc")
                $("#DivArrshow").show();
            else
                $("#DivArrshow2").show();
            Atfilters = true;
            $("#" + id + " img")[0].src = Imgnext1;
        }
        else {
            if (type == "onewAySrc")
                $("#DivArrshow").hide();
            else
                $("#DivArrshow2").hide();
            Atfilters = false;
            $("#" + id + " img")[0].src = Imgnext;
        }
    }
    else if (id == "filteravlSeat") {
        if (Avfilters == false) {
            if (type == "onewAySrc")
                $("#DivAvlshow").show();
            else
                $("#DivAvlshowR").show();
            Avfilters = true;
            $("#" + id + " img")[0].src = Imgnext1;
        }
        else {
            if (type == "onewAySrc")
                $("#DivAvlshow").hide();
            else
                $("#DivAvlshowR").hide();
            Avfilters = false;
            $("#" + id + " img")[0].src = Imgnext;
        }
    }

});
$(".clspng").click(function () {
    $(this).hide();
    $("#setmodifydiv").fadeOut();
    $(".bothwayselect").slideUp();
    $("#clspngclose").hide();
});
function SetNewdateTime(a) {
    var tm = "";
    var hours = parseFloat(String(a).split(':')[0]);
    var minutes = String(a).split(':')[1];
    AMPM = (hours >= 12) ? 'PM' : 'AM';
    hhhh = (hours >= 12) ? hours - 12 : hours;
    oooo = (hhhh >= 10) ? "" : "0";
    if (minutes.split(' ')[1] == undefined) {
        tm = oooo + hhhh + ":" + minutes + " " + AMPM;
    }
    else {
        tm = oooo + hhhh + ":" + minutes;
    }
    return tm;

}
//function setService(seatNo, servId, providerName) {
//    if (providerName == "ES") {
//        var seType = ""; var sevtype = ""; var seTypess = ""; var sevtypess = "";
//        for (var Ts = 0; Ts < seatNo.split(",").length; Ts++) {
//            if (seatNo.split(",")[Ts] != "") {
//                seTypess = $("#" + seatNo.split(",")[Ts].replace(" ", "").replace("(", "").replace(")", "") + " div div")[2].textContent.split(":")[1];
//                sevtypess = $("#" + seatNo.split(",")[Ts].replace("(", "").replace(")", "") + " div div")[3].textContent.split(":")[1];
//                seTypes = (seTypess == "AC") ? 'True' : 'False';
//                sevtypes = (sevtypess == "SELEEPER") ? 'True' : 'False';
//                seType += seTypes + "*";
//                sevtype += sevtypes + "*";
//            }
//        }
//        return servId + "," + seType + "," + sevtype;
//    }
//    else {
//        return servId
//    }
//}
function setService(seatNo, servId, providerName) {
    var seType = ""; var sevtype = "";
    var serviceTaxAmount = "";
    var serviceTaxPer = "";
    var totalFareWithTaxes = "";
    var serviceTaxApplicable = "";
    if (providerName == "ES" || providerName == "TY" || providerName == "RB") {
        var seTypess = ""; var sevtypess = "";
        for (var Ts = 0; Ts < seatNo.split(",").length; Ts++) {
            if (seatNo.split(",")[Ts] != "") {
                seTypess = $($("#" + seatNo.split(",")[Ts].replace(" ", "").replace("(", "").replace(")", "") + " div div")[2]).text().trim().split(":")[1];
                sevtypess = $($("#" + seatNo.split(",")[Ts].replace(" ", "").replace("(", "").replace(")", "") + " div div")[3]).text().trim().split(":")[1];
                serviceTaxAmount += $($("#" + seatNo.split(",")[Ts].replace(" ", "").replace("(", "").replace(")", "") + " div div")[4]).text().trim() + "*";
                totalFareWithTaxes += $($("#" + seatNo.split(",")[Ts].replace(" ", "").replace("(", "").replace(")", "") + " div div")[6]).text().trim() + "*";
                seTypes = (seTypess == "AC") ? 'True' : 'False';
                sevtypes = (sevtypess == "SELEEPER") ? 'True' : 'False';
                seType += seTypes + "*";
                sevtype += sevtypes + "*";
            }
        }
        return seType + "," + sevtype + "," + serviceTaxAmount + "," + totalFareWithTaxes;
    }
    else {
        return seType + "," + sevtype + "," + serviceTaxAmount + "," + totalFareWithTaxes;
    }
}

function shoemoreoptionforsearch(id) {

    var rmainres = rssss[parseInt(id.split('_')[1])]
    var savllayout = "";
    var SfareLayOut1 = "";
    var redBusBuffImage = "<img  src='" + UrlBase + "BS/Images/loading_bar.gif' class='loader' align='left'/>";
    var arrnew = new Array();
    var quearyresult = rmainres.filter(function (x, y) {
        if (x.dupliKeyId == id.replace("S", "").split('_')[0] && x.duplicateRecord == true)
            return arrnew.push({ id: y, result: x });
    });
    var qt = 0;

    for (var qr = 0; qr < arrnew.length; qr++) {
        {

            qt = parseInt(parseInt(arrnew[qr].id));
            if (parseInt(id.split('_')[1]) > 1)
                savllayout += "<div class='list-item box border-list totdata1 bgw boxshadow rtgrid' style='line-height:28px;' >";
            else
                savllayout += "<div class='list-item box border-list totdata1 bgw boxshadow rtgrid1' style='line-height:28px;'>";
            if (arrnew[qr].result.provider_name == "GS")
                savllayout += "<div class='CenterProvider'>GSRTC Bus</div>";
            else if (arrnew[qr].result.provider_name == "TY")
                savllayout += "<div class='CenterProvider'>Travel-Yaari Bus</div>";
            else if (arrnew[qr].result.provider_name == "RB")
                savllayout += "<div class='CenterProvider'>Red Bus</div>";
            else if (arrnew[qr].result.provider_name == "ES")
                savllayout += "<div class='CenterProvider'>E-Smart Travel</div>";
            savllayout += "<div class='serviceType'><div class='serviceTypeO cursorpointer' title='" + arrnew[qr].result.serviceType.toProperCase() + "'>" + arrnew[qr].result.serviceType.toProperCase() + "</div><div class='Bustype travelertypeO cursorpointer' title='" + arrnew[qr].result.traveler.toProperCase() + "'>" + arrnew[qr].result.traveler.toProperCase() + "</div></div>";
            savllayout += "<div class='servicetyO hide'>" + arrnew[qr].result.serviceType.toProperCase().replace(/["~!@#$%^&*\(\)_+=`{}\[\]\|\\:;'<>,.\/?"\- \t\r\n]+/g, '') + "</div>";
            savllayout += "<div class='block'>";
            savllayout += "<p class='desc hide'></p>";
            savllayout += "<p class='like hide'></p>";

            if (arrnew[qr].result.provider_name == "GS" || arrnew[qr].result.provider_name == "TY" || arrnew[qr].result.provider_name == "ES") {
                savllayout += "<div class='DepartureTime dept1'><div class='dptp'>Dep:</div><div>" + getTimeDuration(arrnew[qr].result.departTime, "0", "0") + "</div><div id='BoardO_" + 0 + "_" + qt + "' class='em7 gridViewToolTip'>Boarding Point</div><div id='BoarddO_" + 0 + "_" + qt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                savllayout += "<div class='Onedept hide'>" + convertToMinGS(arrnew[qr].result.departTime) + "</div>";
                savllayout += "<div class='DepartureTime Arr1'><div class='dptp'>Arr:</div><div>" + getTimeDuration(arrnew[qr].result.arrTime, "0", "0") + "</div><div id='DropO_" + 0 + "_" + qt + "' class='em7 gridViewToolTip'>Dropping Point</div><div id='DroppO_" + 0 + "_" + qt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                savllayout += "<div class='OnedArr hide'>" + convertToMinGS(arrnew[qr].result.arrTime) + "</div>";
            }
            else {
                savllayout += "<div class='DepartureTime dept1'><div class='dptp'>Dep:</div><div>" + getTimeDuration(arrnew[qr].result.departTime, "0", "0") + "</div><div id='BoardO_" + 0 + "_" + qt + "' class='em7 gridViewToolTip'>Boarding Point</div><div id='BoarddO_" + 0 + "_" + qt + "' class='gridViewToolTip1' style='margin-top:0px;'>Boarding Point</div></div>";
                savllayout += "<div class='Onedept hide'>" + arrnew[qr].result.departTime + "</div>";
                savllayout += "<div class='DepartureTime Arr1'><div class='dptp'>Arr:</div><div>" + getTimeDuration(arrnew[qr].result.arrTime, "0", "0") + "</div><div id='DropO_" + 0 + "_" + qt + "' class='em7 gridViewToolTip'>Dropping Point</div><div id='DroppO_" + 0 + "_" + qt + "' class='gridViewToolTip1' style='margin-top:0px;'>Dropping Point</div></div>";
                savllayout += "<div class='OnedArr hide'>" + arrnew[qr].result.arrTime + "</div>";
            }
            savllayout += "<div class='avl1 hide'>" + arrnew[qr].result.remainingSeat + "</div>";
            savllayout += "<div  class='title pricessO'>"
            SfareLayOut1 = "";


            savllayout += "<div class='ODetails cursorpointer rgt' id='Details_O_" + 0 + "_" + qt + "'>&nbsp; <img src='" + UrlBase + "BS/images/details.png' title='Details' /> </div><div id='breakup_O_" + 0 + "_" + qt + "' class='brkdtls cursorpointer rgt'>&nbsp; <img src='" + UrlBase + "BS/images/faredetails.png' /> </div><div id='CanO_" + 0 + "_" + qt + "' class='rgt cursorpointer gridViewToolTip' style='padding-top: 8px;' > <img src='" + UrlBase + "BS/images/cancellationpolicy.png' /> </div><div id='CannO_" + 0 + "_" + qt + "' class='gridViewToolTip1'></div>";
            if (parseInt(id.split('_')[1]) > 1) {
                savllayout += "<span id='AvlSeat_O_" + 0 + "_" + qt + "' class='hide remaining1'></span>";
                savllayout += "<div id='farebrk_O_" + 0 + "_" + qt + "' class='hide tooltiphch'></div>";
            }
            else {
                savllayout += "<span id='AvlSeat_O_" + 0 + "_" + qt + "' class='hide remaining'></span>";
                savllayout += "<div id='farebrk_O_" + 0 + "_" + qt + "' class='hide tooltiphch'></div>";
            }
            savllayout += "<div class='clear'></div><span id='farebrk_O_" + 0 + "_" + qt + "' class='hide remaining'></span><div class='clear'></div>";
            var slct = "";
            if (arrnew[qr].result.remainingSeat == "0")
                slct += "<span title='please Select another operator' class='OOSelect cursorpointer'>SOLD-OUT</span>";
            else
                slct += "<span style='text-align:left;' rel='" + arrnew[qr].result.remainingSeat + "' class='OSelect' id='select_O_" + 0 + "_" + qt + "'>SELECT</span>";

            if (arrnew[qr].result.provider_name == "GS") {
                for (var p = 0; p < arrnew[qr].result.Arr_taNetFare.length; p++) {
                    if (p == 0)
                        SfareLayOut1 += arrnew[qr].result.Arr_taNetFare[p] + " , ";
                    else
                        SfareLayOut1 += arrnew[qr].result.Arr_taNetFare[p] + " , ";
                }
                savllayout += "<div class='f16 bld colorp' style='text-align:center;' title='" + SfareLayOut1.trim().substring(0, SfareLayOut1.trim().length - 1) + "'><img src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='frscls1'>" + arrnew[qr].result.Arr_taNetFare[0] + slct + "</span></div>";
            }
            else {
                for (var p = 0; p < arrnew[qr].result.Arr_totFare.length; p++) {

                    if (p == 0)
                        SfareLayOut1 += arrnew[qr].result.Arr_totFare[p] + " , ";
                    else
                        SfareLayOut1 += arrnew[qr].result.Arr_totFare[p] + " , ";
                }
                savllayout += "<div class='f16 bld colorp' style='text-align:center;' title='" + SfareLayOut1.trim().substring(0, SfareLayOut1.trim().length - 1) + "'><img src='" + UrlBase + "Images/rsp.png' style='height:12px;' /><span class='frscls1'>" + SetMinFare(arrnew[qr].result.Arr_totFare) + slct + "</span></div>";
            }

            savllayout += "<div class='hide'  id='select_OO_" + 0 + "_" + qt + "'>" + redBusBuffImage + "</div>";
            savllayout += "</div></div>";
            savllayout += "<div class='aminities'>" + FunSetAminities(arrnew[qr].result.traveler) + "</div>";
            savllayout += "<div style='clear:both;'></div>";
            savllayout += "</div>";
            savllayout += "<div onclick='Hidemoreoptionforsearch(this.id)' id='H" + arrnew[qr].result.dupliKeyId + qt + "'>Hide Me</div></div>";
        }
    }

    $("#" + id.replace("S", "avl")).show();
    $("#" + id.replace("S", "avl")).html(savllayout);
    //$("#" + id.replace("S", "")).show();
    //$("#" + id.replace("S", "H")).show();
    //$("#" + id).hide();
}
function Hidemoreoptionforsearch(id) {
    $("#" + id.replace("H", "")).hide();
    $("#" + id.replace("H", "S")).show();
    $("#" + id).hide();
}
function createNewserviceTypeFliter(Sindexid, servicet) {
    var loclist = "";
    if (servicet.length == 1)
        loclist = servicet;
    else
        loclist = removeDuplicates(servicet);
    var strservicelayout = "";
    strservicelayout += " <div><img class='lft'src='Images/next1.png' style='position: relative; top: 3px;' /><div class='f14 bld colorb closeopenss1 cursorpointer'>Filter By Service </div></div>";
    if (Sindexid == 0)
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Sindexid + "S' data-control-action='filter' data-control-name='checkbox-text-filter0S' data-path='.servicetyO' data-logic='or'>";
    else
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Sindexid + "S' data-control-action='filter' data-control-name='checkbox-text-filter1S' data-path='.Rservicety' data-logic='or'>";
    strservicelayout += "<div>";
    for (var sr = 0; sr < loclist.length; sr++) {
        if (sr < 3)
            strservicelayout += '<div class="lft w5" > <input value="' + loclist[sr].toProperCase().toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc") + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w85 textnowrap cursorpointer" style="height:50px; line-height: 18px;padding-top: 9px;"> <label for="' + loclist[sr].toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc") + '" title="' + loclist[sr] + '">' + loclist[sr].toProperCase() + '</label></div><div class="clear"> </div>';
        else
            strservicelayout += '<div class="lft w5 hide clsservices"> <input value="' + loclist[sr].toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc") + '"  id="CheckboxS' + sr + Sindexid + 'S"  type="checkbox"  /> </div><div class="lft w85 textnowrap cursorpointer hide clsservices"> <label for="' + loclist[sr].toProperCase().replace("Non/ac", "NotBc").replace("Non A/c", "NotBc") + '" title="' + loclist[sr] + '">' + loclist[sr].toProperCase() + '</label></div><div class="clear hide clsservices"> </div>';
    }
    if (loclist.length > 3)
        strservicelayout += "<div id='alllservice' class='f14 cursorpointer padding2s bld colorb closeopenss1 '>  <span class='sh'>Show all " + loclist.length + " service</span> <span class='sh hide'>Hide all " + loclist.length + " service</span></div><div class='clear'> </div>";
    strservicelayout += "</div></div>";
    return strservicelayout;

}
function createNewTravelerTypeFliter(Tindexid, operatorT) {
    var loclist = "";
    if (operatorT.length == 1)
        loclist = operatorT;
    else
        loclist = removeDuplicates(operatorT);
    var strservicelayout = "";
    strservicelayout += " <div><img class='lft'src='Images/next1.png' style='position: relative; top: 3px;' /><div class='f14 bld colorb closeopenss1 cursorpointer'>Filter By Bus Operator </div></div>";
    if (Tindexid == 0)
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Tindexid + "T' data-control-action='filter' data-control-name='checkbox-text-filter0T' data-path='.travelertypeO' data-logic='or'>";
    else
        strservicelayout += "<div class='jplist-group w95 padding2s' data-control-type='checkbox-text-filter" + Tindexid + "T' data-control-action='filter' data-control-name='checkbox-text-filter1T' data-path='.travelertypeR' data-logic='or'>";
    strservicelayout += "<div id='divlocation'>";
    for (var sr = 0; sr < loclist.length; sr++) {
        if (sr < 3)
            strservicelayout += '<div class="lft w5"> <input value="' + loclist[sr].toProperCase() + '"  id="CheckboxS' + sr + Tindexid + 'T"  type="checkbox"  /> </div><div class="lft w85 textnowrap cursorpointer" styel="Height:50px;" > <label for="' + loclist[sr].toProperCase() + '" title="' + loclist[sr] + '">' + loclist[sr].toProperCase() + '</label></div><div class="clear"> </div>';
        else
            strservicelayout += '<div class="lft w5 hide clsoperators"> <input value="' + loclist[sr].toProperCase() + '"  id="CheckboxS' + sr + Tindexid + 'T"  type="checkbox"  /> </div><div class="lft w85 textnowrap cursorpointer  hide clsoperators" styel="Height:50px;"> <label for="' + loclist[sr].toProperCase() + '" title="' + loclist[sr] + '">' + loclist[sr].toProperCase() + '</label></div><div class="clear hide clsoperators"> </div>';
    }
    if (loclist.length > 3)
        strservicelayout += "<div id='allloperators' class='f14 cursorpointer padding2s bld colorb closeopenss1 '> <span class='op'>Show all " + loclist.length + " operator</span> <span class='op hide'>Hide all " + loclist.length + " operator</span></div><div class='clear'> </div>";

    strservicelayout += "</div></div>";
    return strservicelayout;

}
function setminutetotime(datestring, minutein) {
    var twentyMinutesLater = new Date(datestring);
    var twentyMinutesLater1 = new Date();
    twentyMinutesLater1.getTime()
    twentyMinutesLater.setMinutes(parseInt(minutein));
    var dateoftime1 = twentyMinutesLater.format("hh:mm tt dd/MM/yyyy");
    return dateoftime1;
}



//function settimetotime(datestrings, timein) {
//    var ddddd = datestrings + "T" + timein;
//    var timeLater = new Date();
//    Date.parse(ddddd);
//    return timeLater.getTime();
//}