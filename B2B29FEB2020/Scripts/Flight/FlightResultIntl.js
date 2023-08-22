function BTNCLICK(e) {
    $("#searchquery").hide();
    $("#BTNDIV").hide();
    $("#div_Progress").show();
    $.blockUI({
        message: $("#waitMessage")
    });
    var t = UrlBase + "FLTSearch1.asmx/Insert_International_FltDetails";
    $.ajax({
        url: t,
        type: "POST",
        data: JSON.stringify({
            a: e
        }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function(e) {
            InsertedTID = e.d;
            Custinfo_data(InsertedTID)
        },
        error: function(e, t, n) {
            alert(t)
        }
    })
}
function SHOW_FARE_BREAKUP(e, t) {
    var n = UrlBase + "FLTSearch1.asmx/FareBreakupGAL";
    $.ajax({
        url: n,
        type: "POST",
        data: JSON.stringify({
            AirArray: e,
            Trip: "I"
        }),
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function(e) {
            CreateFareBreakUp(e.d[0], t);
            $.ajax.stop()
        },
        error: function(e, t, n) {
            alert(t)
        }
    })
}
function AirLineFilter(e) {
    for (var t = 0; t < e.length; t++) {
        var n = "#" + e[t];
        $(n).click(function() {
            Logic(e, StopSelector)
        })
    }
}
function AirFilterCheck(e) {
    for (var t = 0; t < e.length; t++) {
        var n = "#" + e[t];
        var r = "." + e[t];
        if ($(n).is(":checked")) {
            $(r).show()
        } else {
            $(r).hide()
        }
    }
}
function StopFilter(e) {
    for (var t = 0; t < e.length; t++) {
        var n = "#" + e[t];
        $(n).click(function() {
            Logic(AirSelector, e)
        })
    }
}
function Logic(e, t) {
    for (var n = 0; n < e.length; n++) {
        for (var r = 0; r < t.length; r++) {
            var i = "#" + e[n];
            var s = $(i);
            var o = "." + e[n];
            var u = $(o);
            var a = "#" + t[r];
            var f = $(a);
            var l = e[n] + t[r];
            var c = "[custom=" + l + "]";
            var h = $(c);
            if (s.is(":checked") & f.is(":checked") == true) {
                $(h).show()
            } else if (s.is(":checked") & f.is(":checked") == false) {
                $(h).hide()
            } else if (s.is(":checked") == false) {
                u.hide()
            }
        }
    }
}
function Filter_Price_Stop(e, t, n, r, i) {
    for (var s = 0; s < e.length; s++) {
        for (var o = 0; o < t.length; o++) {
            var u = "#" + e[s];
            var a = $(u);
            var f = "." + e[s];
            var l = $(f);
            var c = "#" + t[o];
            var h = $(c);
            var p = e[s] + t[o];
            var d = "[custom=" + p + "]";
            var v = $(d);
            if (i == "B") {
                if (e[s] == n & t[o] == r) {
                    $(v).show()
                } else if (e[s] == n & t[o] != r) {
                    $(v).hide()
                } else if (e[s] != n) {
                    l.hide()
                }
            } else if (i == "S") {
                if (t[o] == r) {
                    $(v).show()
                } else if (t[o] != r) {
                    $(v).hide()
                }
            } else if (i == "A") {
                if (e[s] == n) {
                    $(v).show()
                } else if (e[s] != n) {
                    $(v).hide()
                }
            }
        }
    }
}
function Custinfo_data(e) {
    if (e[0] == "0") {
        alert("Selected fare has been changed.Please select another flight.");
        $("#searchquery").show();
        $(document).ajaxStop($.unblockUI)
    } else {
        window.location = UrlBase + "International/PaxDetails.aspx?" + e + ",I"
    }
}
function removeDuplicates(e) {
    var t;
    var n = e.length;
    var r = [];
    var i = {};
    for (t = 0; t < n; t++) {
        i[e[t]] = 0
    }
    for (t in i) {
        r.push(t)
    }
    return r
}
function create_AirFilter(e, t) {
    var n = "<b>Filter by Airline :</b> <br/><table id='AirLineFilter' cellspacing='0' cellpadding='0' width='100%'>";
    var r = e.length;
    if (r >= 1 && r <= 6) {
        for (var i = 0; i < r; i++) {
            if (t[i] == 0 || e[i] == 0) { } else {
                n = n + "<tr><td><input type='checkbox' name='" + t[i] + "' id='" + e[i] + "' checked='checked' value='True'/>" + t[i] + "</td></tr>"
            }
        }
        n = n + "</table><br/>";
        $("#AirMatrix").html(n);
        $("#AirMatrix2").hide()
    } else if (r > 6) {
        for (var i = 0; i < 6; i++) {
            if (t[i] == 0 || e[i] == 0) { } else {
                n = n + "<tr><td><input type='checkbox' name='" + t[i] + "' id='" + e[i] + "' checked='checked' value='True'/>" + t[i] + "</td></tr>"
            }
        }
        n = n + "</table><br/>";
        $("#AirMatrix").html(n);
        var s = "<table id='AirLineFilter2' cellspacing='0' cellpadding='0'>";
        for (var i = 6; i < r; i++) {
            if (t[i] == 0 || e[i] == 0) { } else {
                s = s + "<tr><td><input type='checkbox' name='" + t[i] + "' id='" + e[i] + "' checked='checked' value='True'/>" + t[i] + "</td></tr>"
            }
        }
        s = s + "</table><br/>";
        $("#AirMatrix2").html(s);
        $("#AirMatrix2").show()
    }
    $("#AirLineFilter").show()
}
function create_StopFilter(e) {
    var t = "<table id='StopsFilter' cellspacing='0' cellpadding='0'>";
    var n = e.length;
    if (n >= 1) {
        for (var r = 0; r < n; r++) {
            if (e[r] == 0) { } else {
                t = t + "<tr><td><input type='checkbox' id='" + e[r] + "' checked='checked' value='True'/>" + e[r] + "</td></tr>"
            }
        }
        t = t + "</table><br/>"
    }
    $("#StopMatrix").html(t);
    $("#AirLineFilter").show()
}
function CreateFareBreakUp(e, t) {
    var n = e.AdtTax - e.AdtFSur;
    var r = e.AdtFare;
    var i = e.ChdTax - e.ChdFSur;
    var s = e.ChdFare;
    var o = e.InfTax - e.InfFSur;
    var u = e.InfFare;
    var a = e.TotMrkUp;
    var f = e.TotCB;
    var l = e.TotTds;
    strResult = "<table border='0' align='center' cellpadding='2' cellspacing='2' id='FareBreak' class='breakup'>";
    strResult = strResult + "<tr><td align='center' colspan='5' style='background:#272727; color:#fff;'>Fare Summary</td></tr>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td class='head2'>&nbsp;</td>";
    strResult = strResult + "<td class='head2'>Base Fare</td>";
    strResult = strResult + "<td class='head2'>Fuel Surcharge</td>"; // Previous Width 240
    strResult = strResult + "<td class='head2'>Other Tax</td>"; // Previous Width 240
    strResult = strResult + "<td class='head2'>Total Fare</td></tr>";
    //strResult = strResult + "";
    //Body
    //strResult = strResult + "";
    strResult = strResult + "<tr><td align='center'>ADT</td>";
    strResult = strResult + "<td align='center'>" + e.AdtBfare + "</td>";
    strResult = strResult + "<td align='center'>" + e.AdtFSur + "</td>"; //ADTYQ[1]
    strResult = strResult + "<td align='center'>" + n + "</td>"; //ADTOT[1]
    strResult = strResult + "<td align='center'>" + r + "</td></tr>"; //
    if (e.Child > 0) {
        strResult = strResult + "<tr><td align='center'>CHD</td>";
        strResult = strResult + "<td align='center'>" + e.ChdBFare + "</td>";
        strResult = strResult + "<td align='center'>" + e.ChdFSur + "</td>"; //CHDYQ[1]
        strResult = strResult + "<td align='center'>" + i + "</td>"; //CHDOT[1]
        strResult = strResult + "<td align='center'>" + s + "</td></tr>"; //List.ChdTotal

    }

    if (e.Infant > 0) {
        strResult = strResult + "<tr><td align='center'>INF</td>";
        strResult = strResult + "<td align='center'>" + e.InfBfare + "</td>";
        strResult = strResult + "<td align='center'>" + e.InfFSur + "</td>"; //INFYQ[1]
        strResult = strResult + "<td align='center'>" + o + "</td>"; //INFOT[1]
        strResult = strResult + "<td align='center'>" + u + "</td></tr>"; //List.InfTotal
        // strResult = strResult + "";
    }

    strResult = strResult + "<tr>";
    strResult = strResult + "<td colspan='5'>";
    strResult = strResult + "<table cellpadding='4' cellspacing='4'>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td class='border'>SvrTax : " + e.STax + "</td>";
    if (e.IsCorp == true) {
        strResult = strResult + "<td class='border'>Mgnt. Fee : " + e.TotMgtFee + "</td>";
        strResult = strResult + "<td class='border' border='0'></td>";
        strResult = strResult + "<td class='border'>(" + e.Adult + " ADT," + e.Child + " CHD," + e.Infant + " INF)</td>";
        strResult = strResult + "<td> " + e.TotalFare + "</td>";

        strResult = strResult + "</tr>";
        strResult = strResult + "<tr>";
        strResult = strResult + "<td ></td>";
        strResult = strResult + "<td ></td>";
        strResult = strResult + "<td ></td>";
        strResult = strResult + "<td style='font-weight:bold'>Net Fare :</td>";
        strResult = strResult + "<td>" + e.NetFare + "</td>";
    }
    else {
        strResult = strResult + "<td class='border'>Tran. Fee : " + e.TFee + "</td>";
        strResult = strResult + "<td class='border'>Tran. Charge : " + a + "</td>";
        strResult = strResult + "<td class='border'>(" + e.Adult + " ADT," + e.Child + " CHD," + e.Infant + " INF)</td>";
        strResult = strResult + "<td>" + e.TotalFare + "</td>";
        strResult = strResult + "</tr>";
        strResult = strResult + "<tr>";
        strResult = strResult + "<td class='border'>Commission : " + e.TotDis + "</td>";
        strResult = strResult + "<td class='border'>CashBack : " + f + "</td>";
        strResult = strResult + "<td class='border'>TDS : " + l + "</td>";
        strResult = strResult + "<td class='border'>NetFare :</td>";
        strResult = strResult + "<td class='border'>" + e.NetFare + "</td>";
    }





    strResult = strResult + "</tr>";
    strResult = strResult + "</table>";
    strResult = strResult + "</td>";
    strResult = strResult + "</tr>";
    strResult = strResult + "</table>";
    $("#" + t).qtip({
        overwrite: false,
        content: {
            text: strResult
        },
        show: {
            ready: true
        },
        style: {
            classes: "ui-tooltip-green"
        },
        position: {
            my: "top right",
            at: "bottom left"
        }
    })
}
function AirMatrix(e, t, n) {
    var r = e.length;
    var i = t.length;
    var s = "";
    var o;
    var u = new Array;
    var a = new Array;
    var f;
    s = s + "<table id='TMATRIX' width='99%' cellspacing='0' cellpadding='0' style='color:#000;'>";
    s = s + "<thead>";
    s = s + "<tr>";
    s = s + "<th bgcolor='#024F76' class='MatClick' id='H'>Airline →" + "<br/>Stops ↓</th>";
    for (o = 0; o < i; o++) {
        if (t[o] == 0 || n[o] == 0) { } else {
            s = s + "<th class='MatClick' id='A" + t[o] + "' style='background-image: url(../Common/Images/BG_new.jpg);background-repeat: repeat-x;'> <img src='../Airlogo/sm" + t[o] + ".gif' width='30px' /></th>"
        }
    }
    s = s + "</tr></thead>";
    s = s + "<tbody>";
    for (var l = 0; l <= 2; l++) {
        s = s + "<tr><td class='MatClick' id='S" + l + "' style='border:thin solid #01942a; text-align:center' width='auto'>" + l + "Stop</td>";
        for (o = 0; o < i; o++) {
            f = FilterMatrix(e, t[o], l);
            if (f.length > 0) {
                s = s + "<td style='border:thin solid #01942a; text-align:center' width='auto' class='MatClick' id='" + f[0].LineNumber + "'>" + f[0].TotalFare + "</td>"
            } else if (f.length == 0) {
                s = s + "<td style='border:thin solid #01942a; text-align:center' width='auto' class='MatClick'>-</td>"
            }
        }
        s = s + "</tr>"
    }
    s = s + "</tbody></table>";
    return s
}
function FilterMatrix(e, t, n) {
    var r = new Array;
    var i;
    var s;
    var o = 0;
    var u;
    for (var a = 1; a < e.length; a++) {
        for (var f = 0; f < e[a].length; f++) {
            u = ReturnStop(e[a]);
            if ((t == e[a][f].MarketingCarrier || t == null || t == "") && u == n) {
                r[o] = e[a];
                o++;
                break
            }
        }
    }
    if (r.length > 0) {
        s = MinFare(r);
        return s
    } else if (r.length == 0) {
        return s = r
    }
}
function ReturnStop(e) {
    var t;
    var n = 0;
    var r = 0;
    var i = 0;
    for (var s = 0; s < e.length; s++) {
        t = e[s].Stops.substring(0, 1);
        if (t == 0) {
            n = 0
        } else if (t == 1) {
            r = 1
        } else if (t >= 2) {
            i = 2
        }
    }
    if (i >= 2) {
        return i
    } else if (r == 1) {
        return r
    } else {
        return n
    }
}
function MinFare(e) {
    var t = e[0];
    min = e[0][0].TotalFare;
    for (i = 0; i < e.length; i++) {
        if (e[i][0].TotalFare < min) {
            min = e[i][0].TotalFare;
            t = e[i]
        }
    }
    return t
}
function SLIDER(e, t, n) {
    var r = parseInt(e);
    var i = parseInt(t);
    $("#slider-range").slider({
        range: true,
        animate: true,
        step: 500,
        min: parseInt(r),
        max: parseInt(i),
        values: [parseInt(r), parseInt(i)],
        slide: function(e, t) {
            $("#amount1").val(t.values[0]);
            $("#amount2").val(t.values[1])
        },
        change: function(e, t) {
            $("#minRtRange").val($("#slider-range").slider("values", 0));
            $("#maxRtRange").val($("#slider-range").slider("values", 1))
        },
        stop: function(e, t) {
            OW_SL = 1;
            n.fnDraw();
            OW_SL = 0;
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#amount1").val(r);
    $("#amount2").val(i)
}
function SLIDER_DEPTIME(e, t, n) {
    var r = "0000";
    var i = "2400";
    $("#slider-Deptime").slider({
        range: true,
        animate: true,
        step: 5,
        min: 0,
        max: 2400,
        values: [0, 2400],
        slide: function(e, t) {
            var n = $("#slider-Deptime").slider("values", 0).toString();
            var r = $("#slider-Deptime").slider("values", 1).toString();
            $("#Time1").val(Show_Time_24(n));
            $("#Time2").val(Show_Time_24(r))
        },
        change: function(e, t) {
            var n = parseInt($("#slider-Deptime").slider("values", 0)).toString();
            if (n.length == 1) {
                var r = "000";
                n = r.concat(n)
            } else if (n.length == 2) {
                var r = "00";
                n = r.concat(n)
            } else if (n.length == 3) {
                var r = "0";
                n = r.concat(n)
            }
            var i = parseInt($("#slider-Deptime").slider("values", 1)).toString();
            if (i.length == 1) {
                var r = "000";
                n = r.concat(i)
            } else if (i.length == 2) {
                var r = "00";
                n = r.concat(i)
            } else if (i.length == 3) {
                var r = "0";
                i = r.concat(i)
            }
            $("#minDepTime").val(ConvertTime(n));
            $("#maxDepTime").val(ConvertTime(i))
        },
        stop: function(e, t) {
            OW_SL = 1;
            n.fnDraw();
            OW_SL = 0;
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#Time1").val(Show_Time_24(r));
    $("#Time2").val(Show_Time_24(i))
}
function SLIDER_ARRTIME(e, t, n) {
    var r = "0000";
    var i = "2400";
    $("#slider-Inbound").slider({
        range: true,
        animate: true,
        step: 5,
        min: 0,
        max: 2400,
        values: [0, 2400],
        slide: function(e, t) {
            var n = $("#slider-Inbound").slider("values", 0).toString();
            var r = $("#slider-Inbound").slider("values", 1).toString();
            $("#Time3").val(Show_Time_24(n));
            $("#Time4").val(Show_Time_24(r))
        },
        change: function(e, t) {
            var n = parseInt($("#slider-Inbound").slider("values", 0)).toString();
            if (n.length == 1) {
                var r = "000";
                n = r.concat(n)
            } else if (n.length == 2) {
                var r = "00";
                n = r.concat(n)
            } else if (n.length == 3) {
                var r = "0";
                n = r.concat(n)
            }
            var i = parseInt($("#slider-Inbound").slider("values", 1)).toString();
            if (i.length == 1) {
                var r = "000";
                n = r.concat(i)
            } else if (i.length == 2) {
                var r = "00";
                n = r.concat(i)
            } else if (i.length == 3) {
                var r = "0";
                i = r.concat(i)
            }
            $("#minArrTime").val(n);
            $("#maxArrTime").val(i)
        },
        stop: function(e, t) {
            OW_SL = 1;
            n.fnDraw();
            OW_SL = 0;
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#Time3").val(Show_Time_24(r));
    $("#Time4").val(Show_Time_24(i))
}
function ConvertTime(e) {
    var t = e.toString();
    if (t.length == 1) {
        var n = "000";
        t = n.concat(t)
    } else if (t.length == 2) {
        var n = "00";
        t = n.concat(t)
    } else if (t.length == 3) {
        var n = "0";
        t = n.concat(t)
    }
    t = e.split(":");
    e = t[0] + t[1];
    return e
}
function Show_Time_24(e) {
    e = e.toString();
    if (e.length == 1) {
        var t = "000";
        e = t.concat(e)
    } else if (e.length == 2) {
        var t = "00";
        e = t.concat(e)
    } else if (e.length == 3) {
        var t = "0";
        e = t.concat(e)
    }
    var n;
    if (e == "2400") n = parseInt(e.substring(0, 2), 10).toString();
    else {
        n = parseInt(e.substring(0, 2) % 24, 10).toString()
    } if (n.length == 1) {
        var t = "0";
        n = t.concat(n).toString()
    }
    var r = parseInt(e.substring(2, 4) % 60, 10).toString();
    if (r.length == 1) {
        var t = "0";
        r = t.concat(r).toString()
    }
    return n + ":" + r
}
function ds(e, t, n, r) {
    var i;
    var s;
    var o;
    var u;
    var a;
    var f;
    var l;
    var c = "";
    var h = "";
    if (r == "O") {
        i = e.DepDate.split("/");
        if (n == "I") {
            o = new Date(i[2], parseInt(i[1]) - 1, parseInt(i[0]) + 1)
        } else if (n == "D") {
            o = new Date(i[2], parseInt(i[1]) - 1, parseInt(i[0]) - 1)
        }
        f = o.getMonth() + 1;
        a = f.toString();
        if (a.length == 1) {
            var p = "0";
            a = p.concat(a)
        }
        l = o.getDate().toString();
        if (l.length == 1) {
            var p = "0";
            l = p.concat(l)
        }
        c = l + "/" + a + "/" + o.getFullYear()
    } else if (r == "R") {
        i = e.DepDate.split("/");
        s = e.RetDate.split("/");
        if (n == "I") {
            o = new Date(i[2], parseInt(i[1]) - 1, parseInt(i[0]) + 1);
            u = new Date(s[2], parseInt(s[1]) - 1, parseInt(s[0]) + 1)
        } else if (n == "D") {
            o = new Date(i[2], parseInt(i[1]) - 1, parseInt(i[0]) - 1);
            u = new Date(s[2], parseInt(s[1]) - 1, parseInt(s[0]) - 1)
        }
        f = o.getMonth() + 1;
        a = f.toString();
        if (a.length == 1) {
            var p = "0";
            a = p.concat(a)
        }
        l = o.getDate().toString();
        if (l.length == 1) {
            var p = "0";
            l = p.concat(l)
        }
        c = l + "/" + a + "/" + o.getFullYear();
        f = "";
        l = "";
        a = "";
        f = u.getMonth() + 1;
        a = f.toString();
        if (a.length == 1) {
            var p = "0";
            a = p.concat(a)
        }
        l = u.getDate().toString();
        if (l.length == 1) {
            var p = "0";
            l = p.concat(l)
        }
        h = l + "/" + a + "/" + u.getFullYear()
    }
    var d = timediff(c, h, new Date);
    if (d == 0) {
        if (r == "O") {
            e.DepDate = c
        } else if (r == "R") {
            e.DepDate = c;
            e.RetDate = h
        }
        var v = "TripType=" + e.TripType1 + "&txtDepCity1=" + e.DepartureCity + "&txtArrCity1=" + e.ArrivalCity + "&hidtxtDepCity1=" + e.HidTxtDepCity + "&hidtxtArrCity1=" + e.HidTxtArrCity + "&Adult=" + e.Adult;
        v += "&Child=" + e.Child + "&Infant=" + e.Infant + "&Cabin=" + e.Cabin + "&txtAirline=" + e.AirLine + "&hidtxtAirline=" + e.HidTxtAirLine + "&txtDepDate=" + e.DepDate.toString() + "&txtRetDate=" + e.RetDate.toString();
        v += "&Nstop=" + e.Nstop + "&RTF=" + e.RTF + "&Trip=" + e.Trip1;
        document.getElementById("__VIEWSTATE").name = "NOVIEWSTATE";
        if (t == "I") {
            document.forms[0].action = UrlBase + "Flight/FltResultIntl.aspx?" + v
        }
        document.forms[0].submit()
    }
}
function timediff(e, t, n) {
    var r;
    n.setHours(0);
    n.setMinutes(0);
    n.setSeconds(0);
    n.setMilliseconds(0);
    var i = e.split("/");
    var s = new Date(i[2], i[1] - 1, i[0]);
    var o = 1e3 * 60 * 60 * 24;
    var u = s.getTime() - n.getTime();
    var a = Math.floor(u / o);
    if (a >= 0) {
        r = 0
    } else if (a < 0) {
        r = 1
    }
    return r
}
var RHandler;
$(document).ready(function() {
    RHandler = new ResHelper;
    RHandler.BindEvents()
});
var TotAirCode = new Array;
var AirNameList = new Array;
var TotStopList = new Array;
var DepTime = new Array;
var ArrTime = new Array;
var DepTimeOW = new Array;
var ArrTimeOW = new Array;
var DepTimeRT = new Array;
var ArrTimeRT = new Array;
var AirCodeOW = new Array;
var AirCodeRT = new Array;
var AirCodeOW = new Array;
var AirCodeRT = new Array;
var AirSelector = new Array;
var StopSelector = new Array;
var TotTrips;
var InsertedTID;
var OW_SL = 0;
var RT_SL = 0;
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
    this.TripType = $("input[name=TripType]");
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
    this.CurrentSearch = $("#CURSRCH");
    this.Tab1 = $("#Tab1");
    this.Tab2 = $("#Tab2");
    this.Filter = $("#Filter");
    this.showresult1 = $("#showresult1");
    this.showresult2 = $("#showresult2");
    this.Div1 = $("#Div1");
    this.ONLY1 = $("#ONLY1");
    this.BOTH2 = $("#BOTH2");
    this.TR2 = $("#TR2");
    this.TR3 = $("#TR3");
    this.searchquery = $("#searchquery")
};
ResHelper.prototype.BindEvents = function() {
    var e = this;
    e.GetResult(e)
};
ResHelper.prototype.GetResult = function(e) {
    var t = e;
    var n;
    var r = $("input[name='TripType']:checked").val();
    var i = t.hidtxtDepCity1.val().split(",");
    var s = t.hidtxtArrCity1.val().split(",");
    if (i[1] == "IN" && s[1] == "IN") {
        n = "D"
    } else {
        n = "I"
    }
    var o = new Boolean;
    if (t.chkNonstop.attr("checked") == true) {
        o = true
    } else {
        o = false
    }
    var u = new Boolean;
    if (t.LCC_RTF.attr("checked") == true) {
        u = true
    } else {
        u = false
    }
    var a;
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
        NStop: o
    };
    if (r == "rdbOneWay") {
        t.trRetDateRow.hide();
        a = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        t.searchquery.html(a);
        t.CurrentSearch.html("<b>" + a + "</b>")
    } else if (r == "rdbRoundTrip") {
        t.trRetDateRow.show();
        a = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        f = c.ArrivalCity + " To " + c.DepartureCity + " On  " + c.RetDate;
        l = a + "<br/>" + f;
        t.searchquery.html(l);
        t.CurrentSearch.html("<b>" + l + "</b>")
    }
    $.blockUI({
        message: $("#waitMessage")
    });
    var h;
    var p;
    var d;
    var v;
    var m;
    var g = "";
    var y = "";
    var b = "";
    var w = "";
    var E;
    var S;
    var x;
    var T;
    var N = new Array;
    var C;
    var k;
    var L;
    var A;
    var O;
    var M;
    var _;
    var D;
    var P;
    var H;
    var B;
    var j;
    var F = UrlBase + "FLTSearch1.asmx/Search_Flight";
    $.ajax({
        url: F,
        type: "POST",
        data: JSON.stringify({
            obj: c
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(e) {
            if (e.d == "" || e.d == null) {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                $(document).ajaxStop($.unblockUI);
            } else {
                if (e.d[0] == "" || e.d[0] == null) {
                    alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                    $(document).ajaxStop($.unblockUI);
                } else {
                    h = e.d[0].length - 1;
                    var r = 0;
                    var i = e.d[0][h].LineNumber;
                    var s = "";
                    var o = "H";
                    s = "tblResult" + r;
                    E = "<table id=" + s + " cellspacing='0' cellpadding='0' style='background-image: url(../Images/Fbreak1.jpg); border:2px solid #ccc; border-top:none;'>";
                    E = E + "<thead id=" + o + ">";
                    E = E + "<tr style='color:#fff; font-weight:bold; text-align:center;'>";
                    E = E + "<th> </th>";
                    E = E + "<th>Departure Date <img src='../Images/date.png' height='15px' /></th>";
                    E = E + "<th>Departs <span><img src='../Images/air_plane.png' height='15px' /></span></th>";
                    E = E + "<th>Arrives <span><img src='../Images/air_plane.png' height='15px' /></span></th>";
                    E = E + "<th>Stops <span><img src='../Images/stop.png' height='15px'/></span></th>";
                    E = E + "<th>Fare</th>";
                    E = E + "<th>Fare</th>";
                    E = E + "<th> </th>";
                    E = E + "</tr>";
                    E = E + "</thead>";
                    E = E + "<tbody>";
                    C = new Array;
                    C[0] = 0;
                    try {
                        for (var u = 1; u <= i; u++) {
                            var a = new Array;
                            var f = new Array;
                            var l = 0;
                            var p = false;
                            for (var d = 0; d <= h; d++) {
                                if (e.d[r][d].LineNumber == u) {
                                    a[l] = e.d[r][d];
                                    f[l] = a[l];
                                    l++;
                                    p = true
                                }
                            }
                            if (p == true) {
                                C[u] = f;
                                TotAirCode[u - 1] = f[0].MarketingCarrier;
                                AirNameList[u - 1] = f[0].AirLineName;
                                TotStopList[u - 1] = f[0].Stops;
                                DepTime[u - 1] = f[0].DepartureTime;
                                ArrTime[u - 1] = f[0].ArrivalTime;
                                var v = 0;
                                var g = "";
                                var y = "";
                                var S = "";
                                var x = "O" + u;
                                var T = "";
                                var M = "BTN";
                                var _;
                                var D = "";
                                var B = 0;
                                var j = "";
                                var F = a[0].MarketingCarrier + "0-Stop";
                                B = parseInt(a[0].Stops.substring(0, 1));
                                for (var I = 0; I <= a.length - 1; I++) {
                                    v = a[0].LineNumber;
                                    g += "<div style='height:60px'><img src='../Airlogo/sm" + a[I].MarketingCarrier + ".gif' alt='" + a[I].MarketingCarrier + "' height='20px' /><br/>";
                                    g += a[I].MarketingCarrier + " - " + a[I].FlightIdentification + "<br/>";
                                    g += a[I].AirLineName + "</div>";
                                    var q = a[I].DepartureDate.substring(4, 6);
                                    D += "<div style='height:60px;'>" + a[I].Departure_Date + " </div>";
                                    var R = "12";
                                    if (I == 0) {
                                        j = "<div style='height:60px;'>" + a[I].Stops + "<br />" + a[I].TotDur + "</div><br /><br />";
                                        X = a[I].Flight
                                    } else {
                                        if (X != a[I].Flight) {
                                            j += "<div style='height:60px;'>" + a[I].Stops + "<br />" + a[I].TotDur + "</div>";
                                            X = a[I].Flight
                                        }
                                    } if (B > 0 && I < B) { }
                                    y += "<div style='height:60px;'>" + a[I].DepartureTime.substring(0, 5).replace(":", "") + " Hrs.<br />" + a[I].DepartureCityName + "</div>";
                                    if (B > 0 && B < 2) {
                                        F = a[0].MarketingCarrier + "STOP" + B
                                    }
                                    if (B >= 2) {
                                        F = a[0].MarketingCarrier + "STOP2P"
                                    }
                                    S += "<div style='height:60px;'>" + a[I].ArrivalTime.substring(0, 5).replace(":", "") + " Hrs.<br />" + a[I].ArrivalCityName + "</br>" + a[I].ArrivalTerminal + "</div>";
                                    if (B > 0 && I < B) { }
                                }
                                M = M + u;
                                var U = f.slice();
                                var z = "xyz" + u;
                                E = E + "<tr id='" + u + "' class='" + a[0].MarketingCarrier + " bod1'  custom='" + a[0].MarketingCarrier + a[0].Stops + "'style='border:thin solid #f00'>";
                                E = E + "<td align='center' width='70px'>" + g + "</td>";
                                E = E + "<td style='valign:Middle; text-align:center; padding-top:25px'>" + D + "</td>";
                                E = E + "<td style='valign:Middle; text-align:center; padding-top:25px'>" + y + "</td>";
                                E = E + "<td style='valign:Middle; text-align:center; padding-top:25px'>" + S + "</td>";
                                E = E + "<td valign='Middle' align='center'>" + j + "</td>";
                                E = E + "<td valign='Middle' style='font-weight:bold;' align='center'>" + a[0].TotalFare + "</td>";
                                E = E + "<td valign='Middle' style='font-weight:bold;' align='center'>" + a[0].TotalFare + "<br/><a id=" + x + " class='Fare' href='javascript:;'>Fare Summary</a><br/><span style='color:#0000FF; font-style:italic;'>" + a[0].AdtFareType + "</span>";
                                if (((a[0].OrgDestFrom == "JED") || (a[0].OrgDestTo == "JED")) && (a[0].ValiDatingCarrier == "G9")) {
                                    E = E + "<br/><a href='javascript:;' id='impn" + x + "' class='intt'>Important Notice</a>";
                                }
                                E = E + "</td>";
                                E = E + "<td valign='Middle' align='center'><input id='" + M + "' type='button' width='100' class=buttonfltbk' val='" + M + "' value='Book'/></td>";
                                E = E + "</tr>"
                            } else {
                                C[u] = 0;
                                TotAirCode[u - 1] = 0;
                                AirNameList[u - 1] = 0;
                                TotStopList[u - 1] = 0;
                                DepTime[u - 1] = 0;
                                ArrTime[u - 1] = 0
                            }
                        }
                    } catch (W) { }
                    N[r] = C;
                    E = E + "</tbody>";
                    E = E + "</table>";
                    E = E + "<br/>";
                    t.Div1.html(E);
                    t.ONLY1.attr("style", "width: 100%;");
                    t.Tab2.show();
                    b = $("#tblResult0").dataTable({
                        sDom: '<"top"i>rt',
                        aaSorting: [
              [5, "asc"]
            ],
                        aoColumnDefs: [{
                            aTargets: [5],
                            bVisible: false
                        }, {
                            bSortable: false,
                            aTargets: [0, 1]
                        }, {
                            iDataSort: 5,
                            aTargets: [6]
                        }
            ],
                        bPaginate: false,
                        bInfo: true,
                        bAutoWidth: false
                    });
                    t.TR2.show();
                    $(document).ajaxStop($.unblockUI);
                    t.Filter.show();
                    AirSelector = removeDuplicates(TotAirCode);
                    AirNameList = removeDuplicates(AirNameList);
                    StopSelector = removeDuplicates(TotStopList);
                    create_AirFilter(AirSelector, AirNameList);
                    create_StopFilter(StopSelector);
                    var X = AirMatrix(C, AirSelector, AirNameList);
                    $("#AirMat").html(X);
                    w = $("#TMATRIX").dataTable({
                        sDom: '<"top"i>rt',
                        bPaginate: false,
                        bInfo: false,
                        bSort: false,
                        bAutoWidth: true
                    });
                    A = $("#tblResult0").find("tr:nth-child(1) td:eq(5)").text();
                    O = $("#tblResult0").find("tr:last td:eq(5)").text();
                    k = A;
                    var V = A.split("Fare Details");
                    k = V[0];
                    L = O;
                    var J = O.split("Fare Details");
                    L = J[0];
                    P = Math.min.apply(Math, DepTime);
                    H = Math.max.apply(Math, DepTime);
                    $("#slider-range").slider("enable");
                    $("#slider-Deptime").slider("enable");
                    AirLineFilter(AirSelector);
                    StopFilter(StopSelector);
                    SLIDER(k, L, b);
                    SLIDER_DEPTIME(P, H, b);
                    $("#H").click(function() {
                        OW_SL = 1;
                        b.fnDraw();
                        OW_SL = 0
                    });
                    $(".intt").mouseover(function() {
                        var imptid = $(this).attr("id");
                        $("#" + imptid).qtip({
                            overwrite: false,
                            content: {
                                text: "<div style='color: #000000; background-color:#eee; border: 2px Solid #000; padding:5px; line-height:130%; width:450px; text-align:justify;'><b>Flights to and from Jeddah effective 01st April</b><br />Jeddah flights with 4 digit flight numbers will operate to/from Hajj terminal in Jeddah. Only Umrah visa holders and GCC nationals (except Saudi nationals) are allowed to travel on these flights. Jeddah flights with 3 digit flight numbers will operate from north terminal and both Umrah/non Umrah passengers can travel on these flights.</div></div>"
                            },
                            show: {
                                ready: true
                            },
                            style: {
                                classes: "ui-tooltip-green"
                            },
                            position: {
                                my: "top right",
                                at: "bottom left"
                            }
                        })
                    });
                    $(".Fare").mouseover(function() {
                        var e = $(this).attr("id");
                        e = e.substring(1);
                        var t = C[e];
                        anchid = "O" + e;
                        SHOW_FARE_BREAKUP(t, anchid)
                    });
                    $(".PN").click(function() {
                        var e = $(this).attr("id");
                        var t;
                        var r;
                        if (e.substring(0, 1) == "P") {
                            t = "D"
                        } else if (e.substring(0, 1) == "N") {
                            t = "I"
                        }
                        if (e.substring(1, 2) == "O") {
                            r = "O"
                        } else if (e.substring(1, 2) == "R") {
                            r = "R"
                        }
                        ds(c, n, t, r)
                    });
                    $(".Book").click(function() {
                        b.$("tr").click(function() {
                            var e = $(this).attr("id");
                            var t = c.TripType1;
                            var n = C[e];
                            if (n == "" || n == null) {
                                alert("You Must have Been Unresponsive for a long time.Kindly Logut and Retry")
                            } else {
                                m = new Array(n);
                                BTNCLICK(m)
                            }
                        })
                    });
                    $(".Detail").click(function() {
                        b.$("tr").click(function() {
                            b.fnClose(last);
                            var e = $(this).attr("id");
                            var t = C[e];
                            var n = "D" + e;
                            last = this;
                            b.fnOpen(this, FlightDetail(t, n), "details")
                        })
                    });
                    $(".Close").click(function() {
                        b.fnClose(last)
                    });
                    $(".MatClick").click(function() {
                        var e = $(this).attr("id");
                        AirFilterCheck(AirSelector);
                        if (e.substring(0, 1) == "S") {
                            Filter_Price_Stop(AirSelector, StopSelector, "", e.substring(1, 2) + "-Stop", "S")
                        } else if (e.substring(0, 1) == "A") {
                            Filter_Price_Stop(AirSelector, StopSelector, e.substring(1, 3), 0 + "-Stop", "A")
                        } else if (e != "H") {
                            var t = e.substring(e.length, e.length - 3);
                            if (t == "ALL") { } else {
                                var n = C[e];
                                var r = n[0].MarketingCarrier;
                                var i = ReturnStop(n);
                                i = i + "-Stop";
                                Filter_Price_Stop(AirSelector, StopSelector, r, i, "B")
                            }
                        }
                    });
                    $(".ALLAIR").click(function() {
                        Logic(AirSelector, StopSelector)
                    })
                }
            }
        },
        error: function(e, t, n) {
            alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
            $(document).ajaxStop($.unblockUI);
            return false
        }
    })
};
$.fn.dataTableExt.afnFiltering.push(function(e, t, n) {
    var r = document.getElementById("amount1").value * 1;
    var i = document.getElementById("amount2").value * 1;
    var s = "0000";
    var o = "2400";
    var u = "0000";
    if (OW_SL == 1) {
        s = ConvertTime(document.getElementById("Time1").value);
        o = ConvertTime(document.getElementById("Time2").value)
    }
    var a = t[5] == "-" ? 0 : t[5] * 1;
    u = $(t[2]).text();
    u = u.substring(0, 4);
    if (r == "" && i == "" && s == 0 && o == 2400) {
        return true
    } else if (r <= a && i == "" && s == 0 && o == 2400 || r == "" && a <= i && s == 0 && o == 2400) {
        return true
    } else if (r == "" && i == "" && s <= u || r == "" && i == "" && u <= o) {
        return true
    } else if (r <= a && a <= i && s == 0 && o == 2400 || r <= a && i == "" && u >= s && o == 2400) {
        return true
    } else if (r <= a && i == "" && s == 0 && u <= o || r == "" && a <= i && u >= s && o == 2400) {
        return true
    } else if (r <= a && a <= i && u >= s && o == 2400 || r <= a && i == "" && u >= s && u <= o) {
        return true
    } else if (r <= a && a <= i && u >= s && u <= o || r == "" && a <= i && u >= s && u <= o) {
        return true
    } else return false
})

