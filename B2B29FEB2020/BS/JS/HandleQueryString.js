var QSHandler;
$(document).ready(function() {
    QSHandler = new QSHelper();
    QSHandler.BindEvents();
});
var QSHelper = function() {
    this.SOURCE = $("#txtsrc");
    this.HID_SOURCE = $("#txthidsrc");
    this.DESTINATION = $("#txtdest");
    this.HID_DESTINATION = $("#txthiddest");
    this.DEPARTDATE = $("#txtdate");
    this.HIDDEPARTDATE = $("#hiddepart");
    this.BtnSearch = $("#btnsearch");
    this.SEATTYPE = $("#ddlseat");
    this.PASSENGER = $("#ddlpax");
}
QSHelper.prototype.BindEvents = function() {
    var h = this;
    var q = h.getQueryStr();
    h.HID_SOURCE.val(q[0]);
    h.SOURCE.val(q[1]);
    h.HID_DESTINATION.val(q[2]);
    h.DESTINATION.val(q[3]);
    h.HIDDEPARTDATE.val(q[4]);
    h.PASSENGER.val(q[5]);
    h.SEATTYPE.val(q[6]);
}

QSHelper.prototype.getQueryStr = function() {
    var collection = {}; var k = 0;
    var pgUrl = window.location.search.substring(1);
    var qarray = pgUrl.split('&');
    for (var i = 0; i <= qarray.length - 1; i++) {
        var splt = qarray[i].split('=');
        if (splt.length > 0) {
            for (var j = 0; j < splt.length - 1; j++) {
                collection[k] = splt[j + 1];
            }
            k += 1;
        }
    }
    return collection;
}