//allow only digits
function NumericOnly(event) {
    var charCode = (event.keyCode ? event.keyCode : event.which);
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function PopupDiv(inputtpye) {
    // When site loaded, load the Popupbox First
    loadPopupBox();

    $('#mappopupBoxClose').click(function() {
        unloadPopupBox();
    });

    $('#Mapcontainer').click(function() {
        unloadPopupBox();
    });

    function unloadPopupBox() {    // TO Unload the Popupbox
        $('#MapPopup_box').fadeOut("slow");
        $("#Mapcontainer").css({ // this is just for style       
            "opacity": "1"
        });
    }

    function loadPopupBox() {    // To Load the Popupbox
        $('#MapPopup_box').fadeIn("slow");
        $("#Mapcontainer").css({ // this is just for style
            "opacity": "0.3"
        });
    }
    if (inputtpye == "Flight1") {
        document.getElementById('SecondFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "none";
        // document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "block";
        $("#Title").val($("#Sector1").text());
        fillFlight1text();
    }
    if (inputtpye == "Flight2") {
        //  document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "none";
        document.getElementById('SecondFlt').style.display = "block";
        $("#Title2").val($("#Sector2").text());
        // $("#dvExample").text('I am new content of the div.')

        fillFlight2text();
    }
    if (inputtpye == "Flight3") {
        //document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "none";
        document.getElementById('SecondFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "block";
        $("#Title3").val($("#Sector3").text()) ;
        fillFlight3text();
    }
    if (inputtpye == "Flight4") {
        // document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "none";
        document.getElementById('SecondFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "block";
        $("#Title4").val($("#Sector4").text());
        fillFlight4text();
    }
    if (inputtpye == "Flight5") {
        //document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "none";
        document.getElementById('SecondFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "block";
        $("#Title5").val($("#Sector5").text());
        fillFlight5text();
    }
    if (inputtpye == "Flight6") {
        //document.getElementById('PNRandTkt').style.display = "none";
        document.getElementById('firstFlt').style.display = "none";
        document.getElementById('SecondFlt').style.display = "none";
        document.getElementById('ThirdFlt').style.display = "none";
        document.getElementById('FourthFlt').style.display = "none";
        document.getElementById('FifthFlt').style.display = "none";
        document.getElementById('SixthFlt').style.display = "block";
        $("#Title6").val($("#Sector6").text());
        fillFlight6text();
    }
    //    if (inputtpye == "PnrandTkt") {
    //        document.getElementById('firstFlt').style.display = "none";
    //        document.getElementById('SecondFlt').style.display = "none";
    //        document.getElementById('ThirdFlt').style.display = "none";
    //        document.getElementById('FourthFlt').style.display = "none";
    //        document.getElementById('FifthFlt').style.display = "none";
    //        document.getElementById('SixthFlt').style.display = "none";
    //        document.getElementById('PNRandTkt').style.display = "block";
    //        fillServiceChargeText();
    // }
}

function EditValue(InputType) {
    if (InputType == "Flt1") {
        if ($("#txtDptD1").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD1").focus();
            return false;
        }
        else if ($("#txtDptT1").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT1").focus();
            return false;
        }
        else if ($("#txtArrD1").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD1").focus();
            return false;
        }
        else if ($("#txtArrT1").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT1").focus();
            return false;
        }
        else if ($("#txtFlt1").val() == "") {
            alert('please provide Flight Number');
            $("#txtFlt1").focus();
            return false;
        }
        else {
            $("#txt_date").val($("#txtDptD1").val());
            $("#txtDepTime").val($("#txtDptT1").val());
            $("#txtArivalDate").val($("#txtArrD1").val());
            $("#txtArivalTime").val($("#txtArrT1").val());
            $("#txtFltNo").val($("#txtFlt1").val());
            return true;
        }
    }
    if (InputType == "Flt11") {
        if ($("#txtDptD2").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD2").focus();
            return false;
        }
        else if ($("#txtDptT2").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT2").focus();
            return false;
        }
        else if ($("#txtArrD2").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD2").focus();
            return false;
        }
        else if ($("#txtArrT2").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT2").focus();
            return false;
        }
        else if ($("#txtflt2").val() == "") {
            alert('please provide Flight Number');
            $("#txtflt2").focus();
            return false;
        }
        else {
            $("#txt_date2").val($("#txtDptD2").val());
            $("#txtDepTime2").val($("#txtDptT2").val());
            $("#txtArivalDate2").val($("#txtArrD2").val());
            $("#txtArivalTime2").val($("#txtArrT2").val());
            $("#txtFltNo2").val($("#txtflt2").val());
            return true;
        }
    }
    if (InputType == "Flt111") {
        if ($("#txtDptD3").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD3").focus();
            return false;
        }
        else if ($("#txtDptT3").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT3").focus();
            return false;
        }
        else if ($("#txtArrD3").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD3").focus();
            return false;
        }
        else if ($("#txtArrT3").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT3").focus();
            return false;
        }
        else if ($("#txtflt3").val() == "") {
            alert('please provide Flight Number');
            $("#txtflt3").focus();
            return false;
        }
        else {
            $("#txt_date3").val($("#txtDptD3").val());
            $("#txtDepTime3").val($("#txtDptT3").val());
            $("#txtArivalDate3").val($("#txtArrD3").val());
            $("#txtArivalTime3").val($("#txtArrT3").val());
            $("#txtFltNo3").val($("#txtflt3").val());
            return true;
        }
    }
    if (InputType == "Flt1111") {
        if ($("#txtDptD4").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD4").focus();
            return false;
        }
        else if ($("#txtDptT4").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT4").focus();
            return false;
        }
        else if ($("#txtArrD4").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD2").focus();
            return false;
        }
        else if ($("#txtArrT4").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT4").focus();
            return false;
        }
        else if ($("#txtflt4").val() == "") {
            alert('please provide Flight Number');
            $("#txtflt4").focus();
            return false;
        }
        else {
            $("#txt_date4").val($("#txtDptD4").val());
            $("#txtDepTime4").val($("#txtDptT4").val());
            $("#txtArivalDate4").val($("#txtArrD4").val());
            $("#txtArivalTime4").val($("#txtArrT4").val());
            $("#txtFltNo4").val($("#txtflt4").val());
            return true;
        }
    }
    if (InputType == "Flt11111") {
        if ($("#txtDptD5").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD5").focus();
            return false;
        }
        else if ($("#txtDptT5").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT5").focus();
            return false;
        }
        else if ($("#txtArrD5").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD5").focus();
            return false;
        }
        else if ($("#txtArrT5").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT5").focus();
            return false;
        }
        else if ($("#txtflt5").val() == "") {
            alert('please provide Flight Number');
            $("#txtflt5").focus();
            return false;
        }
        else {
            $("#txt_date5").val($("#txtDptD5").val());
            $("#txtDepTime5").val($("#txtDptT5").val());
            $("#txtArivalDate5").val($("#txtArrD5").val());
            $("#txtArivalTime5").val($("#txtArrT5").val());
            $("#txtFltNo5").val($("#txtflt5").val());
            return true;
        }
    }
    if (InputType == "Flt111111") {
        if ($("#txtDptD6").val() == "") {
            alert('please provide Departure  Date');
            $("#txtDptD6").focus();
            return false;
        }
        else if ($("#txtDptT6").val() == "") {
            alert('please provide Departure Time');
            $("#txtDptT6").focus();
            return false;
        }
        else if ($("#txtArrD6").val() == "") {
            alert('please provide Arrival  Date');
            $("#txtArrD6").focus();
            return false;
        }
        else if ($("#txtArrT6").val() == "") {
            alert('please provide Arrival Time');
            $("#txtArrT6").focus();
            return false;
        }
        else if ($("#txtflt6").val() == "") {
            alert('please provide Flight Number');
            $("#txtflt6").focus();
            return false;
        }
        else {
            $("#txt_date6").val($("#txtDptD6").val());
            $("#txtDepTime6").val($("#txtDptT6").val());
            $("#txtArivalDate6").val($("#txtArrD6").val());
            $("#txtArivalTime6").val($("#txtArrT26").val());
            $("#txtFltNo6").val($("#txtflt6").val());
            return true;
        }
    }
    //    if (InputType == "tktpnr") {
    //        var eyz = $("#txt_Distr_Service_charge")
    //        if ($("#txtgdspnr").val() == "") {
    //            alert('please provide GDS PNR');
    //            $("#txtgdspnr").focus();
    //            return false;
    //        }
    //        else if ($("#txtairlinepnr").val() == "") {
    //            alert('please provide AirLine PNR');
    //            $("#txtairlinepnr").focus();
    //            return false;
    //        }
    //        else if ($("#txtTicketnumbre").val() == "") {
    //            alert('please provide Ticket Number');
    //            $("#txtTicketnumbre").focus();
    //            return false;
    //        }
    //        else if ($("#txtService_charge").val() == "") {
    //            alert('please provide Service Charge');
    //            $("#txtService_charge").focus();
    //            return false;
    //        }
    //        else {
    //            if (eyz != null) {
    //                if ($("#txtDistrService_charge").val() == "") {
    //                    alert('please provide Distributer Service Charge');
    //                    $("#txtDistrService_charge").focus();
    //                    return false;
    //                }
    //                else
    //                    $("#txt_Distr_Service_charge").val() = $("#txtDistrService_charge").val();
    //            }
    //            $("#txt_pnr").val() = $("#txtgdspnr").val();
    //            $("#txtAirPnr").val() = $("#txtairlinepnr").val();
    //            $("#txtTktNo").val() = $("#txtTicketnumbre").val();
    //            $("#txt_Service_charge").val() = $("#txtService_charge").val();
    //            return true;
    //        }
    //    }
}

//function fillServiceChargeText() {
//    $("#txtgdspnr").val() = $("#txt_pnr").val();
//    $("#txtairlinepnr").val() = $("#txtAirPnr").val();
//    $("#txtTicketnumbre").val() = $("#txtTktNo").val();
//    $("#txtService_charge").val() = $("#txt_Service_charge").val();
//    $("#txtDistrService_charge").val() = $("#txt_Distr_Service_charge").val();
//}

function fillFlight1text() {
    $("#txtDptD1").val($("#txt_date").val());
    $("#txtDptT1").val($("#txtDepTime").val());
    $("#txtArrD1").val($("#txtArivalDate").val());
    $("#txtArrT1").val($("#txtArivalTime").val());
    $("#txtFlt1").val($("#txtFltNo").val());
}
function fillFlight2text() {
    $("#txtDptD2").val($("#txt_date2").val());
    $("#txtDptT2").val($("#txtDepTime2").val());
    $("#txtArrD2").val($("#txtArivalDate2").val());
    $("#txtArrT2").val($("#txtArivalTime2").val());
    $("#txtflt2").val($("#txtFltNo2").val());
}
function fillFlight3text() {
    $("#txtDptD3").val($("#txt_date3").val());
    $("#txtDptT3").val($("#txtDepTime3").val());
    $("#txtArrD3").val($("#txtArivalDate3").val());
    $("#txtArrT3").val($("#txtArivalTime3").val());
    $("#txtflt3").val($("#txtFltNo3").val());
}
function fillFlight4text() {
    $("#txtDptD4").val($("#txt_date4").val());
    $("#txtDptT4").val($("#txtDepTime4").val());
    $("#txtArrD4").val($("#txtArivalDate4").val());
    $("#txtArrT4").val($("#txtArivalTime4").val());
    $("#txtflt4").val($("#txtFltNo4").val());
}
function fillFlight5text() {
    $("#txtDptD5").val($("#txt_date5").val());
    $("#txtDptT5").val($("#txtDepTime5").val());
    $("#txtArrD5").val($("#txtArivalDate5").val());
    $("#txtArrT5").val($("#txtArivalTime5").val());
    $("#txtflt5").val($("#txtFltNo5").val());
}
function fillFlight6text() {
    $("#txtDptD6").val($("#txt_date6").val());
    $("#txtDptT6").val($("#txtDepTime6").val());
    $("#txtArrD6").val($("#txtArivalDate6").val());
    $("#txtArrT6").val($("#txtArivalTime6").val());
    $("#txtflt6").val($("#txtFltNo6").val());
}

//Reissue Update Validation start
function ReissueValidate() {
    if ($("#txt_pnr").val() == "") {
        alert('please provide GDS PNR');
        $("#txt_pnr").focus();
        return false;
    }
    if ($("#txtAirPnr").val() == "") {
        alert('please provide Air Line PNR');
        $("#txtAirPnr").focus();
        return false;
    }
    if ($("#txtTktNo").val() == "") {
        alert('please provide Ticket No');
        $("#txtTktNo").focus();
        return false;
    }
    if ($("#txt_Reissue_charge").val() == "") {
        alert('please provide Reissue Charge');
        $("#txt_Reissue_charge").focus();
        return false;
    }
    if ($("#txt_farediff").val() == "") {
        alert('please provide Fare Difference');
        $("#txt_farediff").focus();
        return false;
    }
    if ($("#txtRemark").val() == "") {
        alert('please provide Remark');
        $("#txtRemark").focus();
        return false;
    }
    $("#div_Submit").hide();
    $("#div_Progress").show();
    return true;
}
//Reissue Update Validation End

//function RefundValidate() {
//    if (document.getElementById("txt_Rfnd_charge").value == "") {
//        alert('please provide Cancellation Charge');
//        document.getElementById("txt_Rfnd_charge").focus();
//        return false;
//    }
//    if (document.getElementById("txt_Service_charge").value == "") {
//        alert('please provide Service Charge');
//        document.getElementById("txt_Service_charge").focus();
//        return false;
//    }
//    if (document.getElementById("txt_Distr_Service_charge").value == "") {
//        alert('please provide Distributer Service Charge');
//        document.getElementById("txt_Distr_Service_charge").focus();
//        return false;
//    }
//    if (document.getElementById("txtRemark").value == "") {
//        alert('please provide Remark');
//        document.getElementById("txtRemark").focus();
//        return false;
//    }
//    return true;
//}

//function ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
//    document.getElementById('RfndPopup').style.visibility = "visible";
//    $("#txtRemark").focus();
//     $("#Paxname").text(PaxName);
//    $("#PNR").text(PNR);
//    $("#TktNo").text(TktNo);
//    $("#txtPaxid").val(ReqstID);
//    $("#RemarksType").val(RqustType);
//    $("#txtPaxType").val(PaxType);
//}
//function validateremark() {
//    if ($("#txtRemark").val() == "") {
//        alert("Please Provide " + $("#RemarksType").val() + " Remark");
//        $("#txtRemark").focus();
//        return false;
//    }
//    else {
//        return true;
//    }
//}
//function closepopup() {
//    document.getElementById('RfndPopup').style.visibility = "hidden";
//}
//function ResetInputTextvalue(a, b,c,d, e,f) {
//    $("#txtRemark").val("");
//    $("#txtPaxType").val("");
//}

//// Hotel popup start
//function HotelPopup(BID, HotelName) {
//    document.getElementById('RfndPopup').style.visibility = "visible";
//    $("#txtRemark").focus();
//    $("#Bookingid1").text(BID);
//    $("#Bookingid").val(BID);
//    $("#HotelName").text(HotelName);
//}
//function validateHtlRemark() {
//    if ($("#txtRemark").val() == "") {
//        alert("Please provide cancellation remark");
//        $("#txtRemark").focus();
//        return false;
//    }
//    else {
//        return true;
//    }
//}
//// Hotel popup end
//style="font-weight: bold; font-size: 14px;"


function ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
    debugger;
    //   document.getElementById('RfndPopup').style.visibility = "visible";
    $("#txtRemark").focus();
    $("#Paxname").text(PaxName);
    $("#PNR").text(PNR);
    $("#TktNo").text(TktNo);
    $("#txtPaxid").val(ReqstID);
    $("#RemarksType").val(RqustType);
    $("#RemarksTypetext").text(RqustType);
    $("#txtPaxType").val(PaxType);

}



//function ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
//    //   document.getElementById('RfndPopup').style.visibility = "visible";
//    $("#txtRemark").focus();
//    $("#Paxname").text(PaxName);
//    $("#PNR").text(PNR);
//    $("#TktNo").text(TktNo);
//    $("#txtPaxid").val(ReqstID);
//    $("#RemarksType").val(RqustType);
//    $("#txtPaxType").val(PaxType);

//}
function validateremark() {
    if ($("#txtRemark").val() == "") {
        alert("Please Provide " + $("#RemarksType").val() + " Remark");
        $("#txtRemark").focus();
        return false;
    }


    if ($('#trCancelledBy:visible').length > 0) {
        if ($("#ctl00_ContentPlaceHolder1_DrpCancelledBy option:selected").val() == "Select") {
            alert("Please Select cancelled By");
            $("#ctl00_ContentPlaceHolder1_DrpCancelledBy").focus();
            return false;
        }
    }


    return true;

}
function closepopup() {
    document.getElementById('RfndPopup').style.visibility = "hidden";
}
function ResetInputTextvalue(a, b, c, d, e, f) {
    $("#txtRemark").val("");
    $("#txtPaxType").val("");
}



function popupLoad(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
    debugger;
    $("#TktNoInfo").show();
    $("#PaxnameInfoResu").show();
    $("#PaxnameInfoRefnd").hide();
    ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType);
    if (RqustType == "REISSUE") {
        $("#trCancelledBy").hide();
    }
    else {
        $("#trCancelledBy").show();
    }
    loading(); // loading
    setTimeout(function () { // then show popup, deley in .5 second
        loadPopup(); // function show popup 
    }, 100); // .5 second
    if ($('.drop option').length == 0) {
        $("#trCancelledBy").hide();
    }
}
function RefundPopUpLoad(ReqstID, RqustType, PNR, Orderid) {
    debugger;
    $("#TktNoInfo").hide();
    $("#PaxnameInfoRefnd").show();
    $("#PaxnameInfoResu").hide();
    $("#PNR").text(PNR);
    $("#txtPNRNO").val(PNR);
    $("#txtOrderid").val(Orderid);
    $("#RemarksType").val(RqustType);
    $("#RemarksTypetext").text(RqustType);
    if (RqustType == "REISSUE") {
        $("#trCancelledBy").hide();
    }
    else {
        $("#trCancelledBy").show();
    }
    loading(); // loading
    setTimeout(function () { // then show popup, deley in .5 second
        loadPopup(); // function show popup 
    }, 100); // .5 second
    if ($('.drop option').length == 0) {
        $("#trCancelledBy").hide();
    }

    $.ajax({
        url: UrlBase + "Report/FlightRefundReisue.asmx/ShowRefundDetails",
        data: "{ 'orderid': '" + Orderid + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $("#Refunddtldata").html(data.d);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            jAlert(textStatus, 'Alert');
        }
    });
}



$(document).on("click", "#AllSector", function (e) {
    $('.Sectorcss').attr('checked', this.checked);
});

$(document).on("click", "#AllPax", function (e) {
    $('.Paxcss').attr('checked', this.checked);
});

$(document).on("click", ".Sectorcss", function (e) {
    if ($(".Sectorcss").length == $(".case:checked").length) {
        $("#AllSector").attr("checked", "checked");
    } else {
        $("#AllSector").removeAttr("checked");
    }

});

$(document).on("click", ".Paxcss", function (e) {
    if ($(".Paxcss").length == $(".case:checked").length) {
        $("#AllPax").attr("checked", "checked");
    } else {
        $("#AllPax").removeAttr("checked");
    }

});

$(document).on("click", "#ctl00_ContentPlaceHolder1_btnRemark", function (e) {
    debugger;
    if ($("#txtRemark").val() == "") {
        jAlert("Please Provide " + $("#ctl00_ContentPlaceHolder1_RemarksType").text() + " Remark", 'Alert');
        $("#txtRemark").focus();
        return false;
    }

    //if ($('#trCancelledBy:visible').length > 0) {
    //    if ($("#ctl00_ContentPlaceHolder1_DrpCancelledBy option:selected").val() == "Select") {
    //        jAlert("Please Select cancelled By", 'Alert');
    //        $("#ctl00_ContentPlaceHolder1_DrpCancelledBy").focus();
    //        return false;
    //    }
    //}
    //;
    //if ($("#RemarksType").val() == "REFUND" || $("#RemarksType").val() == "REISSUE") {
        var PaxIList = [], SectorList = [];
        $.each($("input[name='Secterdtl']:checked"), function () {
            SectorList.push($(this).val());
        });

        $.each($("input[name='Paxdtl']:checked"), function () {
            PaxIList.push($(this).val());
        });
        if (PaxIList.length == 0) {
            jAlert("Please Select Pax", 'Alert');
            return false;
        }
        else
            $("#txtPaxid").val(PaxIList.join(","));

        if (SectorList.length == 0) {
            jAlert("Please Select Sector", 'Alert');
            return false;
        }
        else
            $("#txtSectorid").val(SectorList.join(","))
    //}
});

$(document).on("click", ".fareRuleToolTip", function (e) {
    if ($.trim($(this).next().text()) != "") {
        $('#FruleTExt').html($(this).next().html());
    } else {
        $('#FruleTExt').html("fare rule not available.")
    }

    $('.fade').fadeToggle(1000);
});

$(document).on("click", ".close1", function (e) {
    $('.fade').each(function () {
        $(this).hide();
    });
});

function HourDeparturePopup(PaxID, RqustType, PNR, Orderid) {
    $("#txtPaxid_4HourDeparture").val(PaxID);
    $("#txtPNRNO").val(PNR);
    $("#txtOrderid").val(Orderid);
    $("#RemarksType").val(RqustType);
    loading(); // loading
    setTimeout(function () { // then show popup, deley in .5 second
        // function show popup 
        closeloading(); // fadeout loading
        $("#HourDeparturePopup").fadeIn(0500); // fadein popup div
        $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
        $("#backgroundPopup").fadeIn(0001);
    }, 100);
}

$("#toPopup").fadeOut("normal");
$("#backgroundPopup").fadeOut("normal");

$(document).on("click", ".btnok", function (e) {
    if ($("#RemarksType").val() == "REFUND")
        RefundPopUpLoad($("#txtPaxid_4HourDeparture").val(), $("#RemarksType").val(), $("#txtPNRNO").val(), $("#txtOrderid").val())
    //else
    //    popupLoad($("#txtPaxid_4HourDeparture").val(), $("#txtReqType4HourDeparture").val(), $("#txtPNRNO_4HourDeparture").val(), $("#txtTktNo_4HourDeparture").val(), $("#txtPaxName_4HourDeparture").val(), $("#txtPaxType_4HourDeparture").val())
    closepopupss();
});
$(document).on("click", ".close11", function (e) {
    closepopupss();
});
function closepopupss() {
    $("#HourDeparturePopup").fadeOut("normal");
    $("#backgroundPopup").fadeOut("normal");
}


function popupLoadReport(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType) {
    debugger;
    $("#TktNoInfo").show();
    $("#PaxnameInfoResu").show();
    $("#PaxnameInfoRefnd").hide();
    ReissueRefundPopup(ReqstID, RqustType, PNR, TktNo, PaxName, PaxType);
    if (RqustType == "REISSUE") {
        $("#trCancelledBy").hide();
    }
    else {
        $("#trCancelledBy").show();
    }
    loading(); // loading
    setTimeout(function () { // then show popup, deley in .5 second
        loadPopupReport(); // function show popup 
    }, 100); // .5 second
    if ($('.drop option').length == 0) {
        $("#trCancelledBy").hide();
    }
}
function RefundPopUpLoadReport(ReqstID, RqustType, PNR, Orderid) {
    debugger;
    $("#TktNoInfo").hide();
    $("#PaxnameInfoRefnd").show();
    $("#PaxnameInfoResu").hide();
    $("#PNR").text(PNR);
    $("#txtPNRNO").val(PNR); 
    $("#Odno").text(Orderid);
    $("#txtOrderid").val(Orderid);
    $("#RemarksType").val(RqustType);
    $("#RemarksTypetext").text(RqustType);
    if (RqustType == "REISSUE") {
        $("#trCancelledBy").hide();
    }
    else {
        $("#trCancelledBy").show();
    }
    loading(); // loading
    setTimeout(function () { // then show popup, deley in .5 second
        loadPopupReport(); // function show popup 
    }, 100); // .5 second
    if ($('.drop option').length == 0) {
        $("#trCancelledBy").hide();
    }

    $.ajax({
        url: UrlBase + "Report/FlightRefundReisue.asmx/ShowRefundDetailsRNC",
        data: "{ 'orderid': '" + Orderid + "','RqustType': '" + RqustType + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            debugger;
            $("#Refunddtldata").html(data.d);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            jAlert(textStatus, 'Alert');
        }
    });

}

$(document).on("click", ".btnokReport", function (e) {
    alert($("#RemarksType").val());
    if ($("#RemarksType").val() == "REFUND")
        RefundPopUpLoadReport($("#txtPaxid_4HourDeparture").val(), $("#RemarksType").val(), $("#txtPNRNO").val(), $("#txtOrderid").val())

    closepopupss();
});
