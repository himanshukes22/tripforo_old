//Form Load function start
var SHandler;
$(document).ready(function() {
    QSHandler = new QSHelper();
     
    QSHandler.BindEvents();

});
//Form Load function End
var QSHelper = function() { }

QSHelper.prototype.BindEvents = function() {
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
    window.location.href = 'HtlResult.aspx'
    }
