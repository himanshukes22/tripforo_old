    var CHandler;
$(document).ready(function() {
    CHandler = new CusHelper;
    var hidtxt = "";
    CHandler.BindEvents();
    var hiddenchkOrderId = $(".ssssss");
    if (hiddenchkOrderId.length!=0)
    hidtxt = $($(".ssssss")[0]).text();
    if (hidtxt == "R")
        $(".tdSeatFare").show();
    else
        $(".tdSeatFare").hide();
    $(".ssssss").hide();

    $(".txtpaxname").keypress(function (e) {
//        var code = e.keyCode || e.which;

        var spacecode = $("#" + this.id).val().indexOf("  ");
//        if (code >= 65 && code <= 90 || code >= 97 && code <= 122 || code == 32 || code == 08 ||code == 46) {
//            if (spacecode != -1) {
//                $("#" + this.id).val($("#" + this.id).val().replace("  ", " "));
//                return false;
//            }
//            else
//            return true;
//        } else {
//            alert("Only alphabates are allowed");
//            return false;
//        }
    var charCode = (e.which) ? e.which : event.keyCode;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32 || charCode == 08) {
        if (spacecode != -1) {
            $("#" + this.id).val($("#" + this.id).val().replace("  ", " "));
            return false;
        }
        else
            return true;
    }
    else {

        return false;
    }
    });

    $("#txtprimarypax").keypress(function (e) {
//        var code = e.keyCode || e.which;
//        var spacecode = $("#" + this.id).val().indexOf("  ");
//        if (code >= 65 && code <= 90 || code >= 97 && code <= 122 || code == 32 || code == 08) {
//            if (spacecode != -1) {
//                $("#" + this.id).val($("#" + this.id).val().replace("  ", " "));
//                return false;
//            }
//            else
//                return true;
//        } else {
//            alert("Only alphabates are allowed");
//            return false;
    //        }
    var spacecode = $("#" + this.id).val().indexOf("  ");
    var charCode = (e.which) ? e.which : event.keyCode;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32 || charCode == 08) {
     if (spacecode != -1) {
                $("#" + this.id).val($("#" + this.id).val().replace("  ", " "));
                return false;
            }
            else 
        return true;
    }
    else {

        return false;
    }

    });
    $(".txtpaxage").keypress(function (e) {
        var code = e.keyCode || e.which;
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //alert("Digits Only");
            return false;
        }
    });
    $("#txtmob").keypress(function (e) {
        var code = e.keyCode || e.which;
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {           
            return false;
        }
    });

    
   });
var CusHelper = function() {
    this.sumFare;
}
CusHelper.prototype.BindEvents = function() {
    var h = this;
}
$("#ctl00_ContentPlaceHolder1_btnbook").click(function () {

    var re = /^[a-zA-Z ]*$/;
    var reNum = /^[0-9 ]*$/;
    var count = $(".txtpaxname");
    var divcount = $(".divERR");
    var i = 0;
    for (var cnt = 0; cnt <= count.length - 1; cnt++) {

        if ($(".dptitle")[cnt].id.indexOf("dptitle") >= 0) {
            if ($(".dptitle")[cnt].value == "select") {
                alert("select Title")
                return false;
            }
        }


        if ($(".txtpaxname")[cnt].id.indexOf("txtpaxname") >= 0) {
            if ($(".txtpaxname")[cnt].value == "") {
                $("#" + divcount[cnt].id + "").show();
                setTimeout(function () {
                    $("#" + divcount[cnt].id + "").show();
                    $("#" + divcount[cnt].id + "").html("Enter your name");
                    $("#" + divcount[cnt].id + "").fadeOut("slow");
                }, 2000);
                $("#" + $(".txtpaxname")[cnt].id).focus();
                return false;
            }
            if (!re.test($(".txtpaxname")[cnt].value)) {
                $("#" + divcount[cnt].id + "").show();
                setTimeout(function() {
                    $("#" + divcount[cnt].id + "").html("please enter char only");
                    $("#" + divcount[cnt].id + "").fadeOut("slow");
                }, 2000);
                $("#" + $(".txtpaxname")[cnt].id).focus();
                return false;
            }
        }
        //if ($(".txtpaxage")[cnt].id.indexOf("txtpaxage") >= 0) {

        //    if ($(".txtpaxage")[cnt].value == "") {
        //        $("#" + divcount[cnt].id + "").html("Enter Age");
        //        $("#" + divcount[cnt].id + "").show();
        //        setTimeout(function() {
        //            $("#" + divcount[cnt].id + "").fadeOut("slow");
        //        }, 2000);
        //        $("#" + $(".txtpaxage")[cnt].id).focus();
        //        return false;
        //    }
        if ($(".txtpaxage")[cnt].id.indexOf("txtpaxage") >= 0) {
            if ($(".txtpaxage")[cnt].value == "" || parseInt($(".txtpaxage")[cnt].value) == 0 || parseInt($(".txtpaxage")[cnt].value) > 100) {
                $("#" + divcount[cnt].id + "").html("Enter valid Age");
                $("#" + divcount[cnt].id + "").show();
                setTimeout(function () {
                    $("#" + divcount[cnt].id + "").fadeOut("slow");
                }, 2000);
                $("#" + $(".txtpaxage")[cnt].id).focus();
                return false;
            }

            if (!reNum.test($(".txtpaxage")[cnt].value)) {
                $("#" + divcount[cnt].id + "").html("please enter num only");
                $("#" + divcount[cnt].id + "").show();
                setTimeout(function() {
                    $("#" + divcount[cnt].id + "").fadeOut("slow");
                }, 2000);
                $("#" + $(".txtpaxage")[cnt].id).focus();
                return false;
            }
            if (parseInt($(".txtpaxage")[cnt].value)>121) {
                $("#" + divcount[cnt].id + "").html("max age 120 years old");
                $("#" + divcount[cnt].id + "").show();
                setTimeout(function () {
                    $("#" + divcount[cnt].id + "").fadeOut("slow");
                }, 2000);
                $("#" + $(".txtpaxage")[cnt].id).focus();
                return false;
            }
        }
      i += 1;
    }
    var paxnameArr = new Array();
    if ($("#txtprimarypax").val() != "" && $("#txtprimarypax").val() != undefined) {
        for (var cnt = 0; cnt <= count.length - 1; cnt++) {
            if (count[cnt].type == "text" && count[cnt].id.indexOf("txtpaxname") >= 0) {
                if (count[cnt].value != "") {
                    paxnameArr.push(count[cnt].value);
                }
            }
        }
        //var arrtext = "";
        //arrtext = $("#txtprimarypax").val();

        var MatchPax = $.inArray($("#txtprimarypax").val(), paxnameArr);//paxnameArr.indexOf($("#txtprimarypax").val())
        $.inArray($("#txtprimarypax").val(), paxnameArr)
        if (MatchPax < 0) {
            alert("please enter valid Primary passenger Name ");
            $("#txtprimarypax").focus();
            return false;
        }
    }
    if ($("#hidprovider").val() != "GS") {
        if ($("#txtprimarypax").val() == "") {
            alert("please enter Primary passenger Name ");
            $("#txtprimarypax").focus();
            return false;
        }
    }


    if ($("#txtmob").val() == "") {
        $("#divtxtmob").html("Enter Mobile Number");
        $("#divtxtmob").show();
        setTimeout(function() {
            $("#divtxtmob").fadeOut("slow");
        }, 2000);
        $("#txtmob").focus();
        return false;
    }
    if (!reNum.test($("#txtmob").val())) {
        $("#divtxtmob").html("please Enter 0nly Number");
        $("#divtxtmob").show();
        setTimeout(function() {
            $("#divtxtmob").fadeOut("slow");
        }, 2000);
        $("#txtmob").focus();
        return false;
    }
    if ($("#txtemail").val() == "") {
        $("#divtxtemail").html("Enter email address");
        $("#divtxtemail").show();
        setTimeout(function() {
            $("#divtxtemail").fadeOut("slow");
        }, 2000);
        $("#txtemail").focus();
        return false;
    }
    if ($("#txtaddress").val() == "") {
        $("#divtxtaddress").html("Enter Address");
        $("#divtxtaddress").show();
        setTimeout(function() {
            $("#divtxtaddress").fadeOut("slow");
        }, 2000);
        $("#txtaddress").focus();
        return false;
    }
    if ($("#txtcard").val() == "") {
        $("#divtxtcard").show();
        setTimeout(function() {
            $("#divtxtcard").fadeOut("slow");
        }, 2000);
        $("#txtcard").focus();
        return false;
    }
    if (confirm("Are you sure!")) {
        document.getElementById("wait").style.display = "block";
        return true;
    }
    else {
        return false;
    }
});
function conFrm() {
    if (confirm("Are you sure want to cancel the ticket!")) {
        document.getElementById("divcanwait").style.display = "block";
        return true;
    }
    else {
        return false;
    }
}
function close() {
    document.getElementById("divcanwait").style.display = "none";
    return true;
}
$(".brk").mouseover(function() {
    var s = $(this).attr("rel").split(',');
    var Commbreakup = "<table cellpadding='5' cellspacing='5' style='border:4px solid #616161; right:0px; top:0px; position:absolute; background:#eee; font-weight:bold;' class='totalfare'>";
    Commbreakup += "<tr>";
    Commbreakup += "<td>Commission(-): " + $.trim(s[0]) + "</td>";
    Commbreakup += "<td style=' border-left:1px solid #616161; padding:0 10px 0; border-right:1px solid #616161;'>TDS(+): " + $.trim(s[1]) + "</td>";
    var PaymentMode = $('#HdnPaymentMode').val();    
    if (PaymentMode == "WL")
    {
        Commbreakup += "<td>Net Fare:</td><td id='NetFare'>" + $.trim(s[2]) + "</td>";
    }
    else {
        Commbreakup += "<td>Net Fare:</td><td id='NetFare'>" + $('#HdnNetFare').val() + "</td>";
        //$('#ctl00_ContentPlaceHolder1_HdnNetFare').val(HdnOrgNetFare);
        //$('#ctl00_ContentPlaceHolder1_HdnNetFarePayAmt').val(HdnOrgNetFare);
    }
    

    Commbreakup += "</tr>";
    Commbreakup += "</table>";
    $("#divComm").show();
    $("#divComm").html(Commbreakup);

});
$(".brk").mouseout(function() {
    $("#divComm").hide();
});
$(".brknet").mouseover(function () {
    var s = $(this).attr("rel").split(',');
    var Commbreakup = "<table cellpadding='5' cellspacing='5' style='border:4px solid #616161;right:0px; top:0px; position:absolute; background:#eee; font-weight:bold;' class='totalfare'>";
    Commbreakup += "<tr>";
    //Commbreakup += "<td>Net Fare:</td><td id='NetFarePayAmt'>" + $.trim(s[0]) + "</td>";
    var PaymentMode = $('#HdnPaymentMode').val();
    if (PaymentMode == "WL") {
        Commbreakup += "<td>Net Fare:</td><td id='NetFarePayAmt'>" + $.trim(s[0]) + "</td>";
    }
    else {
        Commbreakup += "<td>Net Fare:</td><td id='NetFarePayAmt'>" + $('#HdnNetFarePayAmt').val() + "</td>";
        //$('#ctl00_ContentPlaceHolder1_HdnNetFare').val(HdnOrgNetFare);
        //$('#ctl00_ContentPlaceHolder1_HdnNetFarePayAmt').val(HdnOrgNetFare);
    }



    Commbreakup += "</tr>";
    Commbreakup += "</table>";
    $("#divComm").show();
    $("#divComm").html(Commbreakup);

});
$(".brknet").mouseout(function () {
    $("#divComm").hide();
});


function validatenumber(event, id) {
    var code = (event.keyCode ? event.keyCode : which);
    if (code < 46 || code > 59) {
        if (id.indexOf("txtpaxage") >= 0) {
            $("#divtxtpaxage").html("Numbers only");
            $("#divtxtpaxage").show();
            setTimeout(function() {
                $("#divtxtpaxage").fadeOut("slow");
            }, 2000);
            $("#" + id + "").focus();
            return false;
        }
        else if (id.indexOf("txtmob") >= 0) {
            $("#divtxtmob").html("Numbers only");
            $("#divtxtmob").show();
            setTimeout(function() {
                $("#divtxtmob").fadeOut("slow");
            }, 2000);
            $("#" + id + "").focus();
            return false;
        }

    }

}
function validateEmail(id) {
    var sEmail = $("#" + id + "").val();
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (!filter.test(sEmail)) {
        $("#div" + id + "").html("Enter valid email");
        $("#div" + id + "").show();
        setTimeout(function() {
            $("#div" + id + "").fadeOut("slow");
        }, 2000);
        $("#" + id + "").focus();
        return false;
    }

}

function chkcancelationType(id) {  
    document.getElementById('NFC').checked=false;
    document.getElementById('OMC').checked=false;
    document.getElementById('WOC') .checked=false;
    document.getElementById('NFC').checked = false;
    document.getElementById('WRID').style.display = "none";    
        if (id == "FC") {
            document.getElementById('Motkt').checked = false;
            document.getElementById('WR').checked = false;
            document.getElementById('FCT').style.display = "";                  
    }
    if (id == "Motkt") {
        document.getElementById('FC').checked = false;
        document.getElementById('FCT').style.display = "none";
    }
    if (id == "WR") {
        document.getElementById('WRID').style.display =""
        document.getElementById('WRC').checked = false;
        document.getElementById('WOC').checked = false;
        document.getElementById('FCT').style.display = "";     
    }
    if (id == "NFC") {
        document.getElementById('NFC').checked = true;
        document.getElementById('OMC').checked = false;
        document.getElementById('WRC').checked = false;
        document.getElementById('WOC').checked = false;
        document.getElementById('WR').checked = false;
        document.getElementById('FCT').style.display = "";                
    }
    if (id == "OMC") {
        document.getElementById('NFC').checked = false;
        document.getElementById('OMC').checked = true;
        document.getElementById('WRC').checked = false;
        document.getElementById('WOC').checked = false;
        document.getElementById('WR').checked = false;
        document.getElementById('FCT').style.display = "";                    
    }
    if (id == "WRC") {
        document.getElementById('WRID').style.display = ""
        document.getElementById('NFC').checked = false;
        document.getElementById('OMC').checked = false;
        document.getElementById('WRC').checked = true;
        document.getElementById('WOC').checked = false;
        document.getElementById('FCT').style.display = "";     
    }
    if (id == "WOC") {
        document.getElementById('WRID').style.display = ""
        document.getElementById('NFC').checked = false;
        document.getElementById('OMC').checked = false;
        document.getElementById('WRC').checked = false;
        document.getElementById('WOC').checked = true;
        document.getElementById('FCT').style.display = "";     
    }
    $("#HiddenField1").val(id);
}
$("#btnChkType").click(function() {
    var cltye = "NFC,OMC,WRC,WOC";
    var strmsgcltye = "";
    var HiddenF1 = $("#HiddenField1").val();
    if (HiddenF1 == "Motkt") {
        alert("not allowed .")
        return false;
    }
    if (HiddenF1.length == 3) {
        if (cltye.indexOf(HiddenF1) >= 0) {
           
        }
        else {
            alert("please Select Full Cancelation Type.")
            return false;
        }
    }
    else {
        alert("please Select Full Cancelation Type.")
        return false;
    }
});


$(document).on("mouseover", ".CommbreakupO", function (e) {
    $(".CommbreakupOS").show();
});
$(document).on("mouseout", ".CommbreakupO", function (e) {
    $(".CommbreakupOS").hide();
});
$(document).on("mouseover", ".CommbreakupR", function (e) {
    $(".CommbreakupRS").show();
});
$(document).on("mouseout", ".CommbreakupR", function (e) {
    $(".CommbreakupRS").hide();
});
$(document).on("mouseover", ".CommbreakupT", function (e) {
    $(".CommbreakupTT").show();
});
$(document).on("mouseout", ".CommbreakupT", function (e) {
    $(".CommbreakupTT").hide();
});
