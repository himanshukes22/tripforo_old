//var querystring = location.search.replace('?', '').split('&');
//var queryObj = {};
//for (var i = 0; i < querystring.length; i++) {
//    var name = querystring[i].split('=')[0];
//    var value = querystring[i].split('=')[1];
//    queryObj[name] = value;
//}
//Form Load function start
// 
var SHandler;

$(document).ready(function() {
    QSHandler = new QSHelper();
    QSHandler.BindEvents();
});
//Form Load function End
var QSHelper = function() { }


QSHelper.prototype.BindEvents = function() {
    // 
    var h = this;
    var Q = h.queryStr();

    h.PostUrl(Q);


}
QSHelper.prototype.queryStr = function() {
    //get querystring(s) without the ?
    var urlParams = decodeURI(window.location.search.substring(1));

    //if no querystring, return null
    if (urlParams == false | urlParams == '') return null;

    //get key/value pairs
    var QSArray = urlParams.split("&");

    var keyValue_Collection = {};
    for (var value in QSArray) {
        var equalsignPosition = QSArray[value].indexOf("=");
        if (equalsignPosition == -1)
            keyValue_Collection[QSArray[value]] = '';
        else
            keyValue_Collection[QSArray[value].substring(0, equalsignPosition)] = QSArray[value].substr(equalsignPosition + 1);
    }
    return keyValue_Collection;
}


QSHelper.prototype.PostUrl = function(arrData) {
    var cntD;
    var cntA;
    cntD = arrData['hidtxtDepCity1'].split(',');
    cntA = arrData['hidtxtArrCity1'].split(',');
    // 
    if ((cntD[1] == 'IN') && (cntA[1] == 'IN')) {
        if (arrData['RTF'] == "TRUE")
        { window.location.href = 'LccRF/LccRResult.aspx?' + decodeURI(window.location.search.substring(1)); }
        else
        { window.location.href = 'Domestic/FltResultO.aspx?' + decodeURI(window.location.search.substring(1)); }
    }
    else {
        window.location.href = 'International/Result.aspx?' + decodeURI(window.location.search.substring(1));
    }
}