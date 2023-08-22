//Form Load function start
var SRHandler;
// 
$(document).ready(function() {
    SRHandler = new SearchRHelper();
    SRHandler.BindEvents();
});

//Form Load function End

var SearchRHelper = function() {
     // 
this.tabs = $("#tabs");
this.tab1 = $("#tabs-1");

//Sear engine
 this.txtDepCity1 = $("#txtDepCity1");
    this.txtArrCity1 = $("#txtArrCity1");
    this.hidtxtDepCity1 = $("#hidtxtDepCity1");
    this.hidtxtArrCity1 = $("#hidtxtArrCity1");
    this.hidAirline = $("#hidAirLine");
    this.rdbOneWay = $("#rdbOneWay");
    this.rdbRoundTrip = $("#rdbRoundTrip");
    this.txtDepDate = $("#txtDepDate");
    this.txtRetDate = $("#txtRetDate");
    this.hidtxtDepDate = $("#hidtxtDepDate");
    this.hidtxtRetDate = $("#hidtxtRetDate");
    this.TripType = $('input[name=TripType]');
    this.Adult = $("#Adult");
    this.Child = $("#Child");
    this.Infant = $("#Infant");
    this.Cabin = $("#Cabin");
    this.txtAirline = $("#txtAirline");
    this.hidtxtAirline = $("#hidtxtAirline");
    this.chkNonstop = $("#chkNonstop");
   //

}
//$(function() {
//    $("#tabs").tabs();
//});
SearchRHelper.prototype.BindEvents = function() {
    // 
    var h = this;
    h.tabs.tabs();
    h.queryStr();
    h.SetSearchValue();
    
}
SearchRHelper.prototype.queryStr = function() {
    var h = this;
    
//    //get querystring(s) without the ?
//    var urlParams = decodeURI(window.location.search.substring(1));
    var strpath = window.location.pathname.split("/");
    var p = "";
    for (var i = 0; i < strpath.lengh; i++) {
        if (strpath[i] == "FltResultR") {
            p = strpath[i];
        }
    }
//    //if no querystring, return null
//    if (urlParams == false | urlParams == '') return null;
//    //get key/value pairs
//    var SQArray = urlParams.split("&");
    var keyValue_Collection = {};
//    var QSArray = urlParams.split("&");
//    for (var i = 0; i < QSArray.length; i++) {
//        var str = QSArray[i].split("=");
//        keyValue_Collection[str[0]] = str[1];
//    }
keyValue_Collection=queryObj;
    var currSearchStr;
    if (p == "FltResultR")
    {  currSearchStr = "From : " + keyValue_Collection["txtArrCity1"] + " | To : " + keyValue_Collection["txtDepCity1"] + " | Departure Date : " + keyValue_Collection["txtRetDate"] + "| Adults : " + keyValue_Collection["Adult"] + " | Childs : " + keyValue_Collection["Child"] + " | Infants : " + keyValue_Collection["Infant"] + "";}
    else
     { currSearchStr = "From : " + keyValue_Collection["txtDepCity1"] + " | To : " + keyValue_Collection["txtArrCity1"] + " | Departure Date : " + keyValue_Collection["txtDepDate"] + "| Adults : " + keyValue_Collection["Adult"] + " | Childs : " + keyValue_Collection["Child"] + " | Infants : " + keyValue_Collection["Infant"] + "";}
    h.tab1.html(currSearchStr);
}

SearchRHelper.prototype.SetSearchValue = function() {
    var h = this;
    // 
    h.txtDepCity1.val(queryObj["txtDepCity1"]);
    h.txtArrCity1.val(queryObj["txtArrCity1"]);
    h.hidtxtDepCity1.val(queryObj["hidtxtDepCity1"]);
    h.hidtxtArrCity1.val(queryObj["hidtxtArrCity1"]);
    h.txtAirline.val(queryObj["txtAirline"]);
    h.hidtxtAirline.val(queryObj["hidtxtAirline"]);
    h.txtDepDate.val(queryObj["txtDepDate"]);
    h.txtRetDate.val(queryObj["txtRetDate"]);
    h.hidtxtDepDate.val(queryObj["hidtxtDepDate"]);
    h.hidtxtRetDate.val(queryObj["hidtxtRetDate"]);
    h.Adult.val(queryObj["Adult"]);
    h.Child.val(queryObj["Child"]);
    h.Infant.val(queryObj["Infant"]);
    if (queryObj["TripType"]=="rdbOneWay")
    { h.rdbOneWay.attr('checked', true); $("#trRetDateRow").hide(); }
    else
    { h.rdbRoundTrip.attr('checked', true); $("#trRetDateRow").show(); }
    }