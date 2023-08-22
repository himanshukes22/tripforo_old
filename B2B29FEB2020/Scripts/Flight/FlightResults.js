function FooterOperation(e, t) {
    var n = e.length;
    var r = "";
    var i = new Array;
    var s = new Array;
    $("#SLCFLT").show();
    Price = e[0].TotalFare;
    r = r + "<table cellspacing='0' cellpadding='0' style='float:left;'>";
    if (t == "OW") {
        r = r + "<tr><td colspan='2'><h4 style='color:#888; margin-bottom:2px;'>One Way Details</h4></td></tr>"
    } else if (t == "TP") {
        r = r + "<tr><td colspan='2'><h4 style='color:#888; margin-bottom:2px;'>Return Details</h4></td></tr>"
    }
    r = r + "<tr>";
    if (e.length == 1) {
        r = r + "<td valign='middle' width='50'> <img src='../Airlogo/sm" + e[0].ValiDatingCarrier + ".gif' width='30px' /><br />" + e[0].ValiDatingCarrier + "-" + e[0].FlightIdentification + "</td>"
    } else {
        r = r + "<td valign='middle' width='50'> <img src='../../Images/both.png' width='30px' /><br /><span style='font-size:9px;'>Multiple Carrier</span></td>"
    }
    r = r + "<td valign='middle'>";
    r = r + "<div style='width: 99%; float: left;'><b>Rs. " + e[0].TotalFare + "</b></div>";
    r = r + "<div style='width: 49%; float: left;'>" + e[0].DepartureLocation + "<br />" + e[0].DepartureTime.substring(0, 5).replace(":", "") + "Hrs.</div>";
    r = r + "<div style='width: 49%; float: left;'>" + e[e.length - 1].ArrivalLocation + "<br />" + e[e.length - 1].ArrivalTime.substring(0, 5).replace(":", "") + "Hrs.</div>";
    r = r + "</td></tr> </table>";
    $("#SLCFLT").show();
    if (t == "OW") {
        $("#Otable").val(r);
        i = e;
        $("#SLCO").html(r)
    } else if (t == "TP") {
        $("#Rtable").val(r);
        s = e;
        $("#SLCR").html(r)
    }
}

function BTNCLICK(e) {
    $("#searchquery").hide();
    $("#BTNDIV").hide();
    $("#div_Progress").show();
    $.blockUI({
        message: $("#waitMessage")
    });
    var t = UrlBase + "FLTSearch1.asmx/Insert_Selected_FltDetails";
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

function SHOW_FARE_RULE(e, t) {
    var n = UrlBase + "FLTSearch1.asmx/CalFareRule";
    $.ajax({
        url: n,
        type: "POST",
        data: JSON.stringify({
            AirArray: e,
            Trip: "D"
        }),
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function(e) {
            CreateFareBreakUp(e.d, t);
            $.ajax.stop()
        },
        error: function(e, t, n) {
            alert(t)
        }
    })
}

function SHOW_FARE_BREAKUP(e, t) {
    CreateFareBreakUp(e[0], t)
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
            var c = "[Custom=" + l + "]";
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

function Custinfo_data(e) {
    if (e[0] == "0") {
        alert("Selected fare has been changed.Please select another flight.");
        $("#searchquery").show();
        $("#BTNDIV").show();
        $("#div_Progress").hide();
        $(document).ajaxStop($.unblockUI)
    } else {
        window.location = UrlBase + "Domestic/PaxDetails.aspx?" + e
    }
}

function removeDuplicates(e) {
    var t;
    var n = e.length;
    var r = [];
    var i = {};
    for (t = 1; t < n; t++) {
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
    if (r >= 1) {
        for (var i = 0; i < r; i++) {
            n = n + "<tr><td><input type='checkbox' name='" + t[i] + "' id='" + e[i] + "' checked='checked' value='True'/>" + t[i] + "</td></tr>"
        }
        n = n + "</table><br/>"
    }
    $("#AirMatrix").html(n);
    $("#AirLineFilter").show()
}

function create_StopFilter(e) {
    var t = "<b>Filter by Stop :</b> <br/><table id='StopsFilter' cellspacing='0' width='100%' cellpadding='0'>";
    var n = e.length;
    if (n >= 1) {
        for (var r = 0; r < n; r++) {
            t = t + "<tr><td><input type='checkbox' id='" + e[r] + "' checked='checked' value='True'/>" + e[r] + "</td></tr>"
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
    //var a = e.TotMrkUp;
    var a;
    if (e.AdtFareType == 'Spl. Fare, No Commission')
    { a = (e.ADTAgentMrk * e.Adult) + (e.CHDAgentMrk * e.Child); }
    else
    { a = e.TotMrkUp; }
    var f = e.TotCB;
    var l = e.TotTds;
    strResult = "<table border='0' align='center' cellpadding='0' cellspacing='0' id='FareBreak' class='breakup'>";
    strResult = strResult + "<tr><td align='center' colspan='5'  style='background:#272727; line-height:20px; color:#fff;'>Fare Summary</td></tr>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td> </td>";
    strResult = strResult + " <td align='center' class='head2'>Base Fare</td>";
    strResult = strResult + "<td align='center' class='head2'>Fuel Surcharge</td>";
    strResult = strResult + "<td align='center' class='head2'>Other Tax</td>";
    strResult = strResult + "<td align='center' class='head2'>Total Fare</td></tr>";
    strResult = strResult + "<tr><td>ADT</td>";
    strResult = strResult + "<td align='center'>" + e.AdtBfare + "</td>";
    strResult = strResult + "<td align='center'>" + e.AdtFSur + "</td>";
    strResult = strResult + "<td align='center'>" + n + "</td>";
    strResult = strResult + "<td align='center'>" + r + "</td></tr>";
    if (e.Child > 0) {
        strResult = strResult + "<tr><td>CHD</td>";
        strResult = strResult + "<td align='center'>" + e.ChdBFare + "</td>";
        strResult = strResult + "<td align='center'>" + e.ChdFSur + "</td>";
        strResult = strResult + "<td align='center'>" + i + "</td>";
        strResult = strResult + "<td align='center'>" + s + "</td></tr>"
    }
    if (e.Infant > 0) {
        strResult = strResult + "<tr><td>INF</td>";
        strResult = strResult + "<td align='center'>" + e.InfBfare + "</td>";
        strResult = strResult + "<td align='center'>" + e.InfFSur + "</td>";
        strResult = strResult + "<td align='center'>" + o + "</td>";
        strResult = strResult + "<td align='center'>" + u + "</td></tr>"
    }
    strResult = strResult + "<tr>";
    strResult = strResult + "<td colspan='5'>";
    strResult = strResult + "<table>";
    strResult = strResult + "<tr>";
    strResult = strResult + "<td class='border'>SvrTax : " + e.STax + "</td>";
    if (e.IsCorp == true) {
        strResult = strResult + "<td class='border'>Mgnt. Fee : " + e.TotMgtFee + "</td>";
        strResult = strResult + "<td class='border'  border='0'></td>";
        strResult = strResult + "<td class='border'>(" + e.Adult + " ADT," + e.Child + " CHD," + e.Infant + " INF)</td>";
        strResult = strResult + "<td>" + e.TotalFare + "</td>";
        strResult = strResult + "</tr>";
        strResult = strResult + "<tr>";
        strResult = strResult + "<td></td>";
        strResult = strResult + "<td></td>";
        strResult = strResult + "<td></td>";
        strResult = strResult + "<td style='font-weight:bold'>NetFare :</td>";
        strResult = strResult + "<td class='head2'>" + e.NetFare + "</td>";
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
        strResult = strResult + "<td class='head2'>" + e.NetFare + "</td>";
    }

    strResult = strResult + "</tr>";
    strResult = strResult + "</table>";
    strResult = strResult + "</td>";
    strResult = strResult + "</tr>";
    strResult = strResult + "</table>";
    return strResult
}
function SLIDER(e, t, n, r) {
    var i = parseInt(e);
    var s = parseInt(t);
    $("#slider-range").slider({
        range: true,
        animate: true,
        step: 500,
        min: parseInt(i),
        max: parseInt(s),
        values: [parseInt(i), parseInt(s)],
        slide: function(e, t) {
            $("#amount1").val(t.values[0]);
            $("#amount2").val(t.values[1])
        },
        change: function(e, t) {
            $("#minRtRange").val($("#slider-range").slider("values", 0));
            $("#maxRtRange").val($("#slider-range").slider("values", 1))
        },
        stop: function(e, t) {

            if (TotTrips == 0) {
                n.fnDraw()
            } else {
                OW_SL = 1;
                n.fnDraw();
                OW_SL = 0;
                RT_SL = 1;
                r.fnDraw();
                RT_SL = 0
            }
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#amount1").val(i);
    $("#amount2").val(s)
}

function SLIDER_DEPTIME(e, t, n, r) {
    var i = "0000";
    var s = "2400";
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
            if (TotTrips == 0) {
                OW_SL = 1;
                n.fnDraw();
                OW_SL = 0
            } else {
                OW_SL = 1;
                n.fnDraw();
                OW_SL = 0;
                RT_SL = 1;
                r.fnDraw();
                RT_SL = 0
            }
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#Time1").val(Show_Time_24(i));
    $("#Time2").val(Show_Time_24(s))
}

function SLIDER_ARRTIME(e, t, n, r) {
    var i = "0000";
    var s = "2400";
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
            if (TotTrips == 0) {
                n.fnDraw()
            } else {
                RT_SL = 1;
                r.fnDraw();
                RT_SL = 0
            }
            AirFilterCheck(AirSelector);
            Logic(AirSelector, StopSelector)
        }
    });
    $("#Time3").val(Show_Time_24(i));
    $("#Time4").val(Show_Time_24(s))
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

function getInternetExplorerVersion() {
    var e = -1;
    if (navigator.appName == "Microsoft Internet Explorer") {
        var t = navigator.userAgent;
        var n = new RegExp("MSIE ([0-9]{1,}[.0-9]{0,})");
        if (n.exec(t) != null) e = parseFloat(RegExp.$1)
    }
    return e
}
var RHandler;
$(document).ready(function() {
    RHandler = new ResHelper;
    RHandler.BindEvents()
});
$("#S3").hide();
var TotAirCode = new Array;
var DepTimeOW = new Array;
var ArrTimeOW = new Array;
var DepTimeRT = new Array;
var ArrTimeRT = new Array;
var AirCodeOW = new Array;
var AirCodeRT = new Array;
var AirNameList = new Array;
var AirNameListOW = new Array;
var AirNameListRT = new Array;
var TotStopList = new Array;
var StopListOW = new Array;
var StopListRT = new Array;
var AirSelector = new Array;
var StopSelector = new Array;
var TotTrips;
var InsertedTID;
var OW_SL = 0;
var RT_SL = 0;
var Data;
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
    this.GDS_RTF = $("#GDS_RTF");
    this.CS = $("#CS");
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
    var a = new Boolean;
    if (t.GDS_RTF.attr("checked") == true) {
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
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        t.searchquery.html(f);
        f = "<b>" + f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant </b>";
        t.CS.html(f)
    } else if (r == "rdbRoundTrip") {
        f = c.DepartureCity + " To " + c.ArrivalCity + " On  " + c.DepDate;
        l = c.ArrivalCity + " To " + c.DepartureCity + " On  " + c.RetDate;
        var h = f + "<br/>" + l;
        t.searchquery.html(h);
        f = f + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        l = l + " For " + c.Adult + " Adult " + c.Child + " Child " + c.Infant + " Infant";
        h = "<b>" + h + "</b>";
        t.CS.html(h)
    }
    $.blockUI({
        message: $("#waitMessage")
    });
    var p;
    var d;
    var v;
    var m;
    var g;
    var y = "";
    var b = "";
    var w = "";
    var E = "";
    var S;
    var x = new Array;
    var T;
    var N;
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
    var F;
    var I;
    var q = UrlBase + "FLTSearch1.asmx/Search_Flight";
    var chkCnt = "chkCnt";
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
            return false
        },
        success: function(e) {
            if (e.d == "" || e.d == null) {
                alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                $(document).ajaxStop($.unblockUI)
            } else {
                if (e.d[0] == "" || e.d[0] == null) {
                    alert("Sorry, we could not find a match for the destination you have entered.Kindly mordify your search.");
                    $(document).ajaxStop($.unblockUI)
                } else {
                    TotTrips = e.d.length - 1;
                    for (var n = 0; n <= TotTrips; n++) {
                        p = e.d[n].length - 1;
                        var r = e.d[n][p].LineNumber;
                        Data = e.d;
                        var i = "";
                        if (n == 0) {
                            i = "OW";
                            for (var s = 0; s < p; s++) {
                                AirCodeOW[s] = e.d[n][s].MarketingCarrier;
                                AirNameListOW[s] = e.d[n][s].AirLineName;
                                StopListOW[s] = e.d[n][s].Stops;
                                DepTimeOW[s] = e.d[n][s].DepartureTime;
                                ArrTimeOW[s] = e.d[n][s].ArrivalTime
                            }
                        } else if (n == 1) {
                            i = "TP";
                            for (var s = 0; s < p; s++) {
                                AirCodeRT[s] = e.d[n][s].MarketingCarrier;
                                AirNameListRT[s] = e.d[n][s].AirLineName;
                                StopListRT[s] = e.d[n][s].Stops;
                                DepTimeRT[s] = e.d[n][s].DepartureTime;
                                ArrTimeRT[s] = e.d[n][s].ArrivalTime
                            }
                        }
                        var o = "";
                        var u = "H" + n;
                        o = "tblResult" + n;
                        E = "";
                        E = E + "<table id=" + o + " cellspacing='0' cellpadding='10' style='background-image: url(../Images/Fbreak1.jpg); border:2px solid #ccc; border-top:none;'>";
                        E = E + "<thead >";
                        E = E + "<tr style='color:#fff; font-weight:bold; text-align:center;'>";
                        if (TotTrips == 1) {
                            E = E + "<th> </th>"
                        }
                        E = E + "<th align='center' style='padding:5px 10px; text-align:center;'> </th>";
                        if (TotTrips == 0) {
                            E = E + "<th>Departure Date <img src='../Images/date.png' height='15px' /></th>";
                        }
                        E = E + "<th id=" + u + " style='padding:5px 10px; text-align:center;' id='H0'>Departs <span><img src='../Images/air_plane.png' height='15px' /></span></th>";
                        E = E + "<th>Arrives <span><img src='../Images/air_plane.png' height='15px' /></span></th>";
                        E = E + "<th>Stops <span><img src='../Images/stop.png' height='15px'/></span></th>";
                        E = E + "<th style='padding:5px 10px; text-align:center;'>Fare</th>";
                        E = E + "<th style='padding:5px 10px; text-align:center;'>Fare</th>";
                        if (TotTrips == 0) {
                            E = E + "<th>&nbsp;</th>"; // For Book Button
                            //E += "<th style='padding:5px 10px; text-align:center;'></th>";
                            $("#hdnOnewayOrRound").val("");
                            $("#hdnOnewayOrRound").val("OneWay");
                        }
                        if (TotTrips == 1) {
                            //E += "<th style='padding:5px 10px; text-align:center;'>&nbsp;</th>";
                            $("#hdnOnewayOrRound").val("");
                            $("#hdnOnewayOrRound").val("RoundTript");
                        }
                        E = E + "</tr>";
                        E = E + "</thead>";
                        E = E + "<tbody>";
                        T = new Array;
                        T[0] = 0;
                        for (var a = 1; a <= r; a++) {
                            try {
                                var f = new Array;
                                var l = new Array;
                                var c = 0;
                                for (var s = 0; s <= p; s++) {
                                    if (e.d[n][s].LineNumber == a) {
                                        f[c] = e.d[n][s];
                                        l[c] = f[c];
                                        c++
                                    }
                                }
                                T[a] = l;
                                var h = 0;
                                var d = "";
                                var S = "";
                                var M = "";
                                var q = "";
                                var R = "";
                                var U = "";
                                var z;
                                var W = "ID";
                                var X = "BTN";
                                var V = "cb" + n;
                                var J;
                                var K = "";
                                var Q = 0;
                                var G = f[0].MarketingCarrier + "0-Stop";
                                Q = parseInt(f[0].Stops.substring(0, 1));
                                if (TotTrips == 0) {
                                    q = "O" + a;
                                    R = "OW" + a
                                } else if (TotTrips == 1) {
                                    if (n == 0) {
                                        q = "O" + a;
                                        R = "FO" + a
                                    } else if (n == 1) {
                                        q = "R" + a;
                                        R = "FR" + a
                                    }
                                }
                                for (var Y = 0; Y <= f.length - 1; Y++) {
                                    h = f[0].LineNumber;
                                    d += "<div style='height:60px'><img src='../Airlogo/sm" + f[Y].MarketingCarrier + ".gif' alt='" + f[Y].MarketingCarrier + "' height='20px' /><br/>";
                                    d += f[Y].MarketingCarrier + " - " + f[Y].FlightIdentification + "<br/>";
                                    d += f[Y].AirLineName + "<br/>" + f[Y].BagInfo + "</div>";
                                    var Z = "12";
                                    K += "<div style='height:60px'>" + f[Y].Departure_Date + "<br/></div>";
                                    if (Y == 0) {
                                        U = "<div style='height:60px;'>" + f[Y].Stops + "<br />" + f[Y].TotDur + "</div><br /><br />";
                                        z = f[Y].Flight
                                    } else {
                                        if (z != f[Y].Flight) {
                                            U += "<div style='height:60px;'>" + f[Y].Stops + "<br />" + f[Y].TotDur + "</div>";
                                            z = f[Y].Flight
                                        }
                                    } if (Q > 0 && Y < Q) { }
                                    S += "<div style='height:60px'>" + f[Y].DepartureTime.substring(0, 5).replace(":", "") + " Hrs.<br />" + f[Y].DepartureCityName + "</div>";
                                    if (Q > 0 && Q < 2) {
                                        G = f[0].MarketingCarrier + "STOP" + Q
                                    }
                                    if (Q >= 2) {
                                        G = f[0].MarketingCarrier + "STOP2P"
                                    }
                                    M += "<div style='height:60px'>" + f[Y].ArrivalTime.substring(0, 5).replace(":", "") + " Hrs.<br />" + f[Y].ArrivalCityName + "</br>" + f[Y].ArrivalTerminal + "</div>";
                                    if (Q > 0 && Y < Q) { }
                                }
                                W = W + a;
                                X = X + a;
                                //changes made by pawan
                                chkCnt = chkCnt + i;
                                //end of changes made by pawan
                                var et = l.slice();
                                E = E + "<tr id='" + a + "' class='" + f[0].MarketingCarrier + "' Custom='" + f[0].MarketingCarrier + f[0].Stops + "'  style='border:1px solid #0d8204;'>";
                                if (TotTrips == 1) {
                                    E = E + "<td valign='Middle'><input type='radio' id='" + W + "' val='" + a + "' name='" + i + "' class='" + V + "' /></td>"
                                }
                                E = E + "<td valign='Top' align='center' width='70px'>" + d + "</td>";
                                if (TotTrips == 0) {
                                    E = E + "<td valign='Top' align='center'>" + K + "</td>"
                                }
                                E = E + "<td valign='Top' align='center'>" + S + "</td>";
                                E = E + "<td valign='Top' align='center'>" + M + "</td>";
                                E = E + "<td valign='Middle' align='center'>" + U + "</td>";
                                E = E + "<td valign='Middle' style='font-weight:bold;' align='center'>" + f[0].TotalFare + "</td>";
                                ///
                                var pmf = '';
                                try {
                                    if ((f[0].fareBasis.substring(0, 1) == "P") && (f[0].ValiDatingCarrier == "6E")) {
                                        pmf = "<br/><a href='javascript:;' id='impn" + X + "' class='intt'>Promotional Fare</a>";
                                    }
                                    if ((f[0].AdtFareType == "Spl. Fare, No Commission") && (f[0].ValiDatingCarrier == "6E")) {
                                        pmf = "<br/><a href='javascript:;' id='impnt" + X + "' class='inttt'>*T&C Apply.</a>";
                                    }
                                    if ((f[0].AvailableSeats == "SGNRML") && ((f[0].RBD == "E")||(f[0].RBD == "F")||(f[0].RBD == "H")||(f[0].RBD == "J")||(f[0].RBD == "K"))) {
                                        pmf = "<br/><a href='javascript:;' id='impntt" + X + "' class='sgintt'>Friend and Family special fares </a>";
                                    }
                                }
                                catch (fberr)
                                { }

                                ///

                                E = E + "<td valign='Middle' style='font-weight:bold;' align='center'>" + f[0].TotalFare + "<br/><a id=" + q + " class='Fare' href='javascript:;'>Fare Summary</a><br/><span style='color:#0000FF; font-style:italic;'>" + f[0].AdtFareType + " " + pmf + "</span><br/><div style='margin-top:5px; font-weight:normal;'><span style='position:relative; top:4px; margin-right:5px;'><input type='checkbox' id='" + chkCnt + "' name='" + chkCnt + "' class='CheckSub' /></span>Mail</div></td>";
                                if (TotTrips == 0) {
                                    E = E + "<td valign='Middle' align='center'><input id='" + X + "' type='button' class='buttonfltbk' val='" + X + "' value='Book Now' /></td>"
                                }

                                //changes made by pawan
                                //E += "<td valign='Middle' align='center' class='chkClass'></td>";
                                //end of changes mad by pawan
                                E = E + "</tr>"
                            } catch (tt) { }
                        }
                        x[n] = T;
                        E = E + "</tbody>";
                        E = E + "</table>";
                        E = E + "<br/>";
                        if (TotTrips == 0) {
                            t.Div1.html(E);
                            t.Div1.attr("style", "width: 100%;");
                            t.Tab2.show()
                        } else if (TotTrips == 1) {
                            if (o == "tblResult0") {
                                t.showresult1.html(E)
                            } else if (o == "tblResult1") {
                                t.showresult2.html(E)
                            }
                            t.BOTH2.attr("style", "width: 100%;");
                            t.Tab2.show()
                        }
                        ////new script added start
                        $(".intt").mouseover(function() {
                            var imptid = $(this).attr("id");
                            $("#" + imptid).qtip({
                                overwrite: false,
                                content: {
                                    text: "<div style='color: #000000; background-color:#eee; border: 2px Solid #000; padding:5px; line-height:130%; width:450px; text-align:justify;'>Charges will be INR 1500 per passenger per sector in case of cancellation or reissuance.</div></div>"
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
                        ////new script added end
                        ////new script added start for sg
                        $(".sgintt").mouseover(function() {
                            var imptid = $(this).attr("id");
                            $("#" + imptid).qtip({
                                overwrite: false,
                                content: {
                                text: "<div style='color: #000000; background-color:#eee; border: 2px Solid #000; padding:5px; line-height:130%; width:450px; text-align:justify;'>Cancellation/change fee of Rs.2000 per passenger per sector is applicable for these fares.</div></div>"
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
                        ////new script added end
                        if (TotTrips == 0) {
                            w = $("#tblResult0").dataTable({
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
                                bAutoWidth: false
                            });
                            t.TR2.show()
                        } else if (TotTrips == 1) {
                            if (o == "tblResult0") {
                                y = $("#tblResult0").dataTable({
                                    sDom: '<"top"i>rt',
                                    aaSorting: [
                                                [5, "asc"]
                                            ],
                                    aoColumnDefs: [{
                                        bSortable: false,
                                        aTargets: [0, 1, 4]
                                    }, {
                                        aTargets: [5],
                                        bVisible: false
                                    }, {
                                        iDataSort: 5,
                                        aTargets: [6]
                                    }
                                            ],
                                    bPaginate: false,
                                    bInfo: true,
                                    bAutoWidth: false
                                });
                                t.TR3.show()
                            } else if (o == "tblResult1") {
                                b = $("#tblResult1").dataTable({
                                    sDom: '<"top"i>rt',
                                    aaSorting: [
                                                [5, "asc"]
                                            ],
                                    aoColumnDefs: [{
                                        bSortable: false,
                                        aTargets: [0, 1, 4]
                                    }, {
                                        aTargets: [5],
                                        bVisible: false
                                    }, {
                                        iDataSort: 5,
                                        aTargets: [6]
                                    }
                                            ],
                                    bPaginate: false,
                                    bInfo: true,
                                    bAutoWidth: false
                                })
                            }
                        }
                    }
                    $(document).ajaxStop($.unblockUI);
                    TotAirCode = AirCodeOW;
                    AirNameList = AirNameListOW;
                    TotStopList = StopListOW;
                    if (TotTrips == 1) {
                        TotAirCode = AirCodeOW.concat(AirCodeRT);
                        AirNameList = AirNameListOW.concat(AirNameListRT);
                        TotStopList = StopListOW.concat(StopListRT)
                    }
                    AirSelector = removeDuplicates(TotAirCode);
                    AirNameList = removeDuplicates(AirNameList);
                    StopSelector = removeDuplicates(TotStopList);
                    create_AirFilter(AirSelector, AirNameList);
                    create_StopFilter(StopSelector);
                    if (TotTrips == 0) {
                        k = $("#tblResult0").find("tr:nth-child(1) td:eq(5)").text();
                        L = $("#tblResult0").find("tr:last td:eq(5)").text();
                        N = k;
                        var nt = k.split("FareBreakUp");
                        N = nt[0];
                        C = L;
                        var rt = L.split("FareBreakUp");
                        C = rt[0];
                        _ = Math.min.apply(Math, DepTimeOW);
                        D = Math.max.apply(Math, DepTimeOW)
                    }
                    if (TotTrips == 1) {
                        k = $("#tblResult0").find("tr:nth-child(1) td:eq(5)").text();
                        var nt = k.split("FareBreakUp");
                        k = nt[0];
                        L = $("#tblResult0").find("tr:last td:eq(5)").text();
                        var rt = L.split("FareBreakUp");
                        L = rt[0];
                        A = $("#tblResult1").find("tr:nth-child(1) td:eq(5)").text();
                        var nt = A.split("FareBreakUp");
                        A = nt[0];
                        O = $("#tblResult1").find("tr:last td:eq(5)").text();
                        var rt = O.split("FareBreakUp");
                        O = rt[0];
                        if (k <= A) {
                            N = k
                        } else {
                            N = A
                        } if (L > O) {
                            C = L
                        } else {
                            C = O
                        }
                        _ = Math.min.apply(Math, DepTimeOW).toString();
                        if (_.length == 1) {
                            var it = "000";
                            _ = it.concat(_)
                        } else if (_.length == 2) {
                            var it = "00";
                            _ = it.concat(_)
                        } else if (_.length == 3) {
                            var it = "0";
                            _ = it.concat(_)
                        }
                        D = Math.max.apply(Math, DepTimeOW).toString();
                        P = Math.min.apply(Math, DepTimeRT).toString();
                        if (P.length == 1) {
                            var it = "000";
                            P = it.concat(P)
                        } else if (P.length == 2) {
                            var it = "00";
                            P = it.concat(P)
                        } else if (P.length == 3) {
                            var it = "0";
                            P = it.concat(P)
                        }
                        H = Math.max.apply(Math, DepTimeRT).toString();
                        B = Math.min.apply(Math, ArrTimeOW).toString();
                        if (B.length == 1) {
                            var it = "000";
                            B = it.concat(B)
                        } else if (B.length == 2) {
                            var it = "00";
                            B = it.concat(B)
                        } else if (B.length == 3) {
                            var it = "0";
                            B = it.concat(B)
                        }
                        j = Math.max.apply(Math, ArrTimeOW).toString();
                        F = Math.min.apply(Math, ArrTimeRT).toString();
                        if (B.length == 1) {
                            var it = "000";
                            F = it.concat(F)
                        } else if (F.length == 2) {
                            var it = "00";
                            F = it.concat(F)
                        } else if (F.length == 3) {
                            var it = "0";
                            F = it.concat(F)
                        }
                        I = Math.max.apply(Math, ArrTimeRT).toString();
                        $("#slider-Inbound").slider("enable")
                    }
                    $("#slider-range").slider("enable");
                    $("#slider-Deptime").slider("enable");
                    AirLineFilter(AirSelector);
                    StopFilter(StopSelector);
                    if (TotTrips == 0) {
                        SLIDER(N, C, w, "");
                        SLIDER_DEPTIME(_, D, w, "")
                    } else if (TotTrips == 1) {
                        SLIDER(N, C, y, b);
                        SLIDER_DEPTIME(_, D, y, b)
                    }
                    $("#H0").click(function() {
                        OW_SL = 1;
                        if (TotTrips == 0) {
                            w.fnDraw()
                        } else if (TotTrips == 1) {
                            y.fnDraw()
                        }
                        OW_SL = 0
                    });
                    $("#H1").click(function() {
                        RT_SL = 1;
                        b.fnDraw();
                        RT_SL = 0
                    });
                    $(".FareRule").click(function() {
                        if (TotTrips == 0) {
                            var e = $(this).attr("id");
                            var t = e.substring(1);
                            var n = x[0][t];
                            SHOW_FARE_RULE(n, e)
                        } else if (TotTrips == 1) {
                            var e = $(this).attr("id");
                            var t = e.substring(0, 1);
                            var r = e.substring(1);
                            if (t == "O") {
                                var n = x[0][r]
                            } else if (t == "R") {
                                var n = x[1][r]
                            }
                            SHOW_FARE_RULE(n, e)
                        }
                    });
                    $(".Fare").live("mouseover", function() {
                        var e;
                        var t;
                        var n;
                        var r;
                        if (TotTrips == 0) {
                            e = $(this).attr("id");
                            t = e.substring(1);
                            n = x[0][t];
                            r = CreateFareBreakUp(n[0], e)
                        } else if (TotTrips == 1) {
                            e = $(this).attr("id");
                            t = e.substring(0, 1);
                            line1 = e.substring(1);
                            if (t == "O") {
                                n = x[0][line1]
                            } else if (t == "R") {
                                n = x[1][line1]
                            }
                            r = CreateFareBreakUp(n[0], e)
                        }
                        $("#" + e).qtip({
                            overwrite: false,
                            content: {
                                text: r
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
                    $(".Book").click(function() {
                        w.$("tr").click(function() {
                            var e = $(this).attr("id");
                            var t = x[0][e];
                            g = new Array(t);
                            BTNCLICK(g)
                        })
                    });
                    $(".cb0").click(function() {
                        y.$("tr").click(function() {
                            var e = y.fnGetData(this);
                            var t = $(e[0]).attr("val");
                            $('input:radio[class=cb0][id="ID' + t + '"]').attr("checked", "checked");
                            v = "OW";
                            $("#OFTH").val(t);
                            var n = x[0][t];
                            FooterOperation(n, v);
                            var r = $("#RFTH").val();
                            var i = $("#Rtable").val();
                            if (m == "TP") {
                                var s = x[1][r];
                                g = new Array(n, s);
                                $("#SLCR").html(i)
                            } else {
                                g = new Array(n)
                            }
                        })
                    });
                    $(".cb1").click(function() {
                        b.$("tr").click(function() {
                            var e = b.fnGetData(this);
                            var t = $(e[0]).attr("val");
                            $('input:radio[class=cb1][id="ID' + t + '"]').attr("checked", "checked");
                            m = "TP";
                            $("#RFTH").val(t);
                            var n = x[1][t];
                            FooterOperation(n, m);
                            var r = $("#OFTH").val();
                            var i = $("#Otable").val();
                            if (v == "OW") {
                                var s = x[0][r];
                                g = new Array(s, n);
                                $("#SLCO").html(i)
                            } else {
                                g = new Array(n)
                            }
                        })
                    });
                    $("#bookBtn").click(function() {
                        if ($(".cb0:checked").is(":checked") && $(".cb1:checked").is(":checked")) {
                            BTNCLICK(g)
                        } else {
                            alert("PLEASE SELECT BOTH CHOICES .IF YOU WANT TO SELECT ONLY ONE . THEN RE-SEARCH BY SELECTING CHOICE ONE WAY")
                        }
                    })
                }
            }
            try {
                $("#hdnMailString").val("");
                $("#hdnMailString").val($("#tblResult0_wrapper")[0].parentElement.parentElement.parentElement.parentElement.outerHTML);
            }
            catch (Err)
            { }

            ////$("#hdnMailString").val($("#tblResult0_wrapper").html());
        }
    })
};
$.fn.dataTableExt.afnFiltering.push(function(e, t, n) {
    var r = document.getElementById("amount1").value * 1;
    var i = document.getElementById("amount2").value * 1;
    var s = "0000";
    var o = "2400";
    var u;
    if (OW_SL == 1 || RT_SL == 1) {
        s = ConvertTime(document.getElementById("Time1").value);
        o = ConvertTime(document.getElementById("Time2").value)
    }
    var a = t[5] == "-" ? 0 : t[5] * 1;
    var f = getInternetExplorerVersion();
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

