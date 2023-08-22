var QSHandler;
$(document).ready(function () {
    QSHandler = new QSHelper;
    QSHandler.BindEvents()
});
var QSHelper = function () {
    this.flight = $("flight");
    this.txtDepCity1 = $("#txtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    this.rdbOneWay = $("#rdbOneWay");
    this.rdbRoundTrip = $("#rdbRoundTrip");
    this.txtDepDate = $("#txtDepDate");
    this.txtRetDate = $("#txtRetDate");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
    this.trRetDateRow = $("#trRetDateRow");
    this.TripType = $("input[name=TripType]");
    this.Adult = $("#Adult");
    this.Child = $("#Child");
    this.Infant = $("#Infant");
    this.Cabin = $("#Cabin");
    this.NStop = $("#NStop");
    this.txtAirline = $("#txtAirline");
    this.hidtxtAirline = $("#hidtxtAirline");
    this.chkNonstop = $("#chkNonstop");
    this.chkAdvSearch = $("#chkAdvSearch");
    this.trAdvSearchRow = $("#trAdvSearchRow");
    this.LCC_RTF = $("#LCC_RTF");
    this.GDS_RTF = $("#GDS_RTF")

    //For MultiCity 27-12-2016


    //this.txtDepDate1 = $("#txtDepDate1");
    //this.hidtxtDepDate1 = $("#hidtxtDepDate1");
    this.rdbMultiCity = $("#rdbMultiCity");
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
    // this.hidtxtDepDate3 = $("#hidtxtDepDate3");

    this.txtDepCity4 = $("#txtDepCity4");
    this.hidtxtDepCity4 = $("#hidtxtDepCity4");
    this.txtArrCity4 = $("#txtArrCity4");
    this.hidtxtArrCity4 = $("#hidtxtArrCity4");
    this.txtDepDate4 = $("#txtDepDate4");
    // this.hidtxtDepDate4 = $("#hidtxtDepDate4");

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

    // For Group Search
    this.GroupSearch = $("#CB_GroupSearch");
};
QSHelper.prototype.BindEvents = function () {
    var e = this;
    var t = e.queryStr();
    e.flight.val(t.flight);
    e.txtDepCity1.val(t.txtDepCity1);
    e.txtArrCity1.val(t.txtArrCity1);
    e.hidtxtDepCity1.val(t.hidtxtDepCity1);
    e.hidtxtArrCity1.val(t.hidtxtArrCity1);
    e.txtDepDate.val(t.txtDepDate);
    e.txtRetDate.val(t.txtRetDate);
    e.hidtxtDepDate.val(t.hidtxtDepDate);
    e.hidtxtRetDate.val(t.hidtxtRetDate);
    e.Adult.val(t.Adult);
    e.Child.val(t.Child);
    e.Infant.val(t.Infant);
    e.Cabin.val(t.Cabin);
    e.NStop.val(t.NStop);
    e.txtAirline.val(t.txtAirline);
    e.hidtxtAirline.val(t.hidtxtAirline);
    e.chkAdvSearch.val(t.chkAdvSearch);
    e.trAdvSearchRow.val(t.trAdvSearchRow);

    //For MultiCity 27-12-2016


    //e.txtDepDate1.val(t.txtDepDate1)
    //e.hidtxtDepDate1.val(t.hidtxtDepDate1)

    e.txtDepCity2.val(t.txtDepCity2)
    e.hidtxtDepCity2.val(t.hidtxtDepCity2)
    e.txtArrCity2.val(t.txtArrCity2)
    e.hidtxtArrCity2.val(t.hidtxtArrCity2)
    e.txtDepDate2.val(t.txtDepDate2)
    // e.hidtxtDepDate2.val(t.hidtxtDepDate2)


    e.txtDepCity3.val(t.txtDepCity3)
    e.hidtxtDepCity3.val(t.hidtxtDepCity3)
    e.txtArrCity3.val(t.txtArrCity3)
    e.hidtxtArrCity3.val(t.hidtxtArrCity3)
    e.txtDepDate3.val(t.txtDepDate3)
    // e.hidtxtDepDate3.val(t.hidtxtDepDate3)

    e.txtDepCity4.val(t.txtDepCity4)
    e.hidtxtDepCity4.val(t.hidtxtDepCity4)
    e.txtArrCity4.val(t.txtArrCity4)
    e.hidtxtArrCity4.val(t.hidtxtArrCity4)
    e.txtDepDate4.val(t.txtDepDate4)
    // e.hidtxtDepDate4.val(t.hidtxtDepDate4)

    e.txtDepCity5.val(t.txtDepCity5)
    e.hidtxtDepCity5.val(t.hidtxtDepCity5)
    e.txtArrCity5.val(t.txtArrCity5)
    e.hidtxtArrCity5.val(t.hidtxtArrCity5)
    e.txtDepDate5.val(t.txtDepDate5)
    //e.hidtxtDepDate5.val(t.hidtxtDepDate5)

    e.txtDepCity6.val(t.txtDepCity6)
    e.hidtxtDepCity6.val(t.hidtxtDepCity6)
    e.txtArrCity6.val(t.txtArrCity6)
    e.hidtxtArrCity6.val(t.hidtxtArrCity6)
    e.txtDepDate6.val(t.txtDepDate6)
    // e.hidtxtDepDate6.val(t.hidtxtDepDate6)



    e.GroupSearch.val(t.GroupSearch);
    if (t.TripType == "rdbOneWay" || t.TripType == "rdbOneWayF") {
        $("#rdbOneWay").attr("checked", true); e.rdbOneWay.val("rdbOneWay")
    }
    else if (t.TripType == "rdbRoundTrip" || t.TripType == "rdbRoundTripF") {
        $("#rdbRoundTrip").attr("checked", true);
        e.rdbRoundTrip.val("rdbRoundTrip")
        $("#trRetDateRow").show();
    }
    else if (t.TripType == "rdbMultiCity") {
        $("#rdbMultiCity").attr("checked", true);
        e.rdbMultiCity.val("rdbMultiCity")

    }
    if (t.NStop == "TRUE") {
        $("#chkNonstop").attr("checked", true); e.chkNonstop.val("TRUE")
    }
    else if (t.NStop == "FALSE") {
        $("#chkNonstop").attr("checked", false); e.chkNonstop.val("FALSE")
    }
    if (t.RTF == "TRUE") {
        $("#LCC_RTF").attr("checked", true); e.LCC_RTF.val("TRUE")
    }
    else if (t.RTF == "FALSE") {
        $("#LCC_RTF").attr("checked", false); e.LCC_RTF.val("FALSE")
    }
    if (t.GRTF == "TRUE") {
        $("#GDS_RTF").attr("checked", true); e.GDS_RTF.val("TRUE")
    }
    else if (t.GRTF == "FALSE") {
        $("#GDS_RTF").attr("checked", false); e.GDS_RTF.val("FALSE")
    }
    if (t.GroupSearch == "true") {
        $("#CB_GroupSearch").attr("checked", true); e.GroupSearch.val("true")
        $("#Traveller").hide();
    }
    else if (t.GroupSearch == "false") {
        $("#CB_GroupSearch").attr("checked", false); e.GroupSearch.val("false")
    }
};
QSHelper.prototype.queryStr = function () {
    var e = decodeURI(window.location.search.substring(1));
    if (e == false | e == "") return null;
    var t = e.split("&");
    var n = {};
    var r;
    for (r = 0; r < t.length; r++)
    {
        var i = t[r].indexOf("=");
        if (i == -1) n[t[r]] = "";
        else n[t[r].substring(0, i)] = t[r].substr(i + 1);
        if (r == 42) { break }   // new with multicity
       // if (r == 19) { break }
    }
   
    return n
}