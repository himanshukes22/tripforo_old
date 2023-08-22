$(function () { if (actionType == null) { actionType = "mobile"; } BindOpratorByType("ddlMobileOprator", 'PREPAID', 'Operator'); });

//============================Start Mobile Recharge Section=====================================
$('input[name=mobiletype]').change(function () {
    if (this.value == 'Prepaid') {
        $("#btnMobileRecharge").html("Recharge");
        BindOpratorByType("ddlMobileOprator", 'PREPAID', 'Operator');
        $("#MobileCircleSection").removeClass("hidden");
        $("#MobileAmountSection").removeClass("hidden");
        $("#mobilenotesmsg").html("").addClass("hidden");
        $("#txtMobileNumber").val("");
    }
    else {
        $("#btnMobileRecharge").html("Get Bill");
        BindOpratorByType("ddlMobileOprator", 'MOBILE POSTPAID', 'Operator');
        $("#MobileCircleSection").addClass("hidden");
        $("#MobileAmountSection").addClass("hidden");
        $("#mobilenotesmsg").html("").addClass("hidden");
        $("#txtMobileNumber").val("");
    }
});

function RechargeEvent() {
    var thisbutton = $("#btnMobileRecharge");
    var txnmode = $("input[name='mobiletype']:checked").val();

    if (CheckFocusBlankValidation("txtMobileNumber")) return !1;
    if (CheckFocusDropDownBlankValidation("ddlMobileOprator")) return !1;
    if (txnmode.toLowerCase() == "prepaid") {
        if (CheckFocusDropDownBlankValidation("ddlMobileCircle")) return !1;
        if (CheckFocusBlankValidation("txtMobileAmount")) return !1;
    }

    var mobileno = $("#txtMobileNumber").val();
    var oprator = $("#ddlMobileOprator option:selected").val();
    var opratorname = $("#ddlMobileOprator option:selected").text();
    var isbillfetch = $("#ddlMobileOprator option:selected").data("isbillfetch");
    var circle = null; var amount = null;
    if (txnmode.toLowerCase() == "prepaid") { circle = $("#ddlMobileCircle").val(); amount = $("#txtMobileAmount").val(); }
    else { circle = 0; amount = 0; }

    if (mobileno.length == 10) {
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/MobileRecharge",
            data: '{mobile: ' + JSON.stringify(mobileno) + ',opratorname: ' + JSON.stringify(opratorname) + ',oprator: ' + JSON.stringify(oprator) + ',circle: ' + JSON.stringify(circle) + ',amount: ' + JSON.stringify(amount) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + ',rchtype: ' + JSON.stringify(txnmode) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d[0] == "000") {
                        if (txnmode.toLowerCase() == "prepaid") { ResetMobile(); }
                        $("#modelmobileheadbody").html("");
                        $("#modelmobileheadbody").html(data.d[1]);
                        $(".mobilemodelclickclass").click();
                    }
                    else if (data.d[0] == "failed") {
                        ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", data.d[1], "#d91717");
                    }
                    else if (data.d[0] == "reload") {
                        window.location.reload();
                    }
                    else {
                        $("#modelmobileheadbody").html("");
                        $("#modelmobileheadbody").html(data.d[1]);
                        $(".mobilemodelclickclass").click();
                    }
                }
                if (txnmode.toLowerCase() == "prepaid") {
                    $(thisbutton).html("Recharge").prop('disabled', false);
                }
                else {
                    $(thisbutton).html("Get Bill").prop('disabled', false);
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
    else {
        ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", "Mobile number not valid.", "#d91717");
    }
}

function PostPaidMobilePaymentSubmit() {
    var thisbutton = $("#btnPostPaidMobilePayment");

    var mobileno = $("#txtMobileNumber").val();
    var oprator = $("#ddlMobileOprator option:selected").val();
    var opratorname = $("#ddlMobileOprator option:selected").text();
    var isbillfetch = $("#ddlMobileOprator option:selected").data("isbillfetch");
    var circle = 0;
    var amount = $("#hdnTotalPostPaidMobilePaidAmt").val();
    var txnmode = "prepaid";

    var currtxnmode = $("input[name='mobiletype']:checked").val();

    if (amount != null && amount != "") {
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/MobileRecharge",
            data: '{mobile: ' + JSON.stringify(mobileno) + ',opratorname: ' + JSON.stringify(opratorname) + ',oprator: ' + JSON.stringify(oprator) + ',circle: ' + JSON.stringify(circle) + ',amount: ' + JSON.stringify(amount) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + ',rchtype: ' + JSON.stringify(txnmode) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d[0] == "000") {
                        ResetMobile();
                        $("#modelmobileheadbody").html("");
                        $("#modelmobileheadbody").html(data.d[1]);
                        if (currtxnmode.toLowerCase() == "prepaid") {
                            $(".mobilemodelclickclass").click();
                        }
                    }
                    else if (data.d[0] == "failed") {
                        ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", data.d[1], "#d91717");
                    }
                    else if (data.d[0] == "reload") {
                        window.location.reload();
                    }
                    else {
                        $("#modelmobileheadbody").html("");
                        $("#modelmobileheadbody").html(data.d[1]);
                        if (currtxnmode.toLowerCase() == "prepaid") {
                            $(".mobilemodelclickclass").click();
                        }
                    }
                }
                $(thisbutton).html("Bill Pay").prop('disabled', false);
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
}

function ResetMobile() {
    $("#txtMobileNumber").val("");
    $("#ddlMobileOprator").val("0").change();
    $("#ddlMobileCircle").val("0").change();
    //$("# option:selected").val("0");
    //$("# option:selected").val("0");   
    $("#txtMobileAmount").val("");
    $("#mobilenotesmsg").html("").addClass("hidden");
}

function RechShowMessagePopup(bodyMsg) {
    $("#modelheadbody").html(bodyMsg);
    $(".rechargesection").click();
}

$("#ddlMobileOprator").change(function () {
    $(".mobilenotesmsg").html("").addClass("hidden");
    $("#ddlMobileCircle").val("0").change();
    $("#txtMobileAmount").val("");

    var spkey = $(this).val();
    var fetchid = $("#ddlMobileOprator option:selected").data("fetchid");
    var isbillfetch = $("#ddlMobileOprator option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("mobile") + ',servicetype: ' + JSON.stringify("Mobile") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#mobilenotesmsg").html(data.d[1]).removeClass("hidden");

                    //if (isbillfetch.toLowerCase() == "false") {
                    //    $("#btnMobileRecharge").html("Pay Bill");
                    //}
                    //else {
                    //    $("#btnMobileRecharge").html("Get Bill");
                    //}
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

//============================Start DTH Recharge Section=====================================

$("#ddlDTHOprator").change(function () {
    $(".dthsecation").addClass("hidden")
    $("#dthnotesmsg").html("");
    $("#DTHAmount").val("").addClass("hidden");

    var spkey = $(this).val();
    var fetchid = $("#ddlDTHOprator option:selected").data("fetchid");
    var isbillfetch = $("#ddlDTHOprator option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("dth") + ',servicetype: ' + JSON.stringify("DTH") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    //$("#BindDTHSection").after(data.d[0]);
                    $("#dthnotesmsg").html(data.d[1]).removeClass("hidden");

                    $("#btnDTHRecharge").html("Recharge");

                    $(".dthsecation").removeClass("hidden")
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function DTHRechargeEvent() {
    var thisbutton = $("#btnDTHRecharge");

    if (CheckFocusDropDownBlankValidation("ddlDTHOprator")) return !1;
    if (CheckFocusBlankValidation("txtDTHNumber")) return !1;

    var dthnumber = $("#txtDTHNumber").val();
    if (dthnumber.length >= 6 && dthnumber.length <= 18) {
        if (CheckFocusBlankValidation("txtDTHAmount")) return !1;

        var spkeytext = $("#ddlDTHOprator option:selected").text();
        var spkey = $("#ddlDTHOprator option:selected").val();

        var optionsvalue = [];
        var optionsids = [];

        optionsvalue.push($("#txtDTHNumber").val());
        optionsvalue.push($("#txtDTHAmount").val());

        optionsids.push($("#txtDTHNumber"));
        optionsids.push($("#txtDTHAmount"));

        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        DoDirectPayment("DTHPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlDTHOprator", "modeldthheadbody", "dthmodelclickclass", "dthnotesmsg", $("#btnDTHRecharge"), "Recharge");
    }
    else {
        ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", "DTH number not valid.", "#d91717");
    }
}

//============================Start Electricity Recharge Section=====================================
$("#ddlElectricityBoard").change(function () {
    $(".electricitydynamicinput").remove();
    $("#ElectricityAmount").val("").addClass("hidden");

    var spkey = $(this).val();
    var fetchid = $("#ddlElectricityBoard option:selected").data("fetchid");
    var isbillfetch = $("#ddlElectricityBoard option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("electricity") + ',servicetype: ' + JSON.stringify("ELECTRICITY") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindElectricitySection").after(data.d[0]);
                    $("#electricitynotesmsg").html(data.d[1]).removeClass("hidden");

                    if (isbillfetch.toLowerCase() == "false") {
                        $("#btnElectricity").html("Pay Bill");
                    }
                    else {
                        $("#btnElectricity").html("Get Bill");
                    }
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function FeatchElectricityBill() {
    var iselectricityfieled = false;

    var thisbutton = $("#btnElectricity");
    var isbillfetch = $("#ddlElectricityBoard option:selected").data("isbillfetch");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlElectricityBoard")) return !1;

    $(".electricitydynamicinput").each(function (index, item) {
        var thisid = $(this).data("electricityid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                iselectricityfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                iselectricityfieled = false;
                return false;
            }
        }
        else {
            iselectricityfieled = false;
            return false;
        }
    });

    if (iselectricityfieled) {
        var spkey = $("#ddlElectricityBoard option:selected").val();
        var spkeytext = $("#ddlElectricityBoard option:selected").text();
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        if (isbillfetch.toLowerCase() == "true") {
            CurrBillFeatch("ElectricityBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlElectricityBoard", "modelelectricityheadbody", "electricitymodelclickclass", thisbutton, "Get Bill", "Electricity");
        }
        else {
            DoDirectPayment("ElectricityPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlElectricityBoard", "modelelectricityheadbody", "electricitymodelclickclass", "electricitynotesmsg", thisbutton, "Get Bill");
        }
    }
}

function ElectricityPaymentSubmit() {
    var spkeytext = $("#ddlElectricityBoard option:selected").text();
    var spkey = $("#ddlElectricityBoard option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".electricitydynamicinput").each(function (index, item) {
        var thisid = $(this).data("electricityid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                return false;
            }
        }
        else {
            return false;
        }
    });

    var amount = $("#hdnTotalElectricityPaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("ElectricityPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlElectricityBoard", "modelelectricityheadbody", "electricitynotesmsg", $("#btnElectricityPayment"), "Pay Bill");
}

//============================Start Landline Recharge Section=====================================
$("#ddlLandlineOperator").change(function () {
    $(".landlinedynamicinput").remove();

    var spkey = $(this).val();

    if (spkey != "0") {
        var fetchid = $("#ddlLandlineOperator option:selected").data("fetchid");
        var isbillfetch = $("#ddlLandlineOperator option:selected").data("isbillfetch");

        if (isbillfetch.toLowerCase() == "false") {
            $("#btnLandline").html("Recharge");
            // $("#btnLandline").attr("onclick", "DirectLandlinePaymentSubmit()");
        }
        else {
            $("#btnLandline").html("Get Bill");
            //$("#btnLandline").attr("onclick", "FeatchLandlineBill()");
        }

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("landline") + ',servicetype: ' + JSON.stringify("LANDLINE") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindLandlineSection").after(data.d[0]);
                    $("#landlinenotesmsg").html(data.d[1]).removeClass("hidden");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function FeatchLandlineBill() {
    var islandlinefieled = false;

    var thisbutton = $("#btnLandline");
    var isbillfetch = $("#ddlLandlineOperator option:selected").data("isbillfetch");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlLandlineOperator")) return !1;

    $(".landlinedynamicinput").each(function (index, item) {
        var thisid = $(this).data("landlineid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                islandlinefieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                islandlinefieled = false;
                return false;
            }
        }
        else {
            islandlinefieled = false;
            return false;
        }
    });

    if (islandlinefieled) {
        var spkey = $("#ddlLandlineOperator option:selected").val();
        var spkeytext = $("#ddlLandlineOperator option:selected").text();

        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        if (isbillfetch.toLowerCase() == "true") {
            CurrBillFeatch("LandlineBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlLandlineOperator", "modellandlineheadbody", "landlinemodelclickclass", thisbutton, "Get Bill", "Landline");
        }
        else {
            DoDirectPayment("LandlinePayment", spkey, spkeytext, optionsvalue, optionsids, "ddlLandlineOperator", "modellandlineheadbody", "landlinemodelclickclass", "landlinenotesmsg", thisbutton, "Get Bill");
        }
    }
}

function LandlinePaymentSubmit() {
    var spkeytext = $("#ddlLandlineOperator option:selected").text();
    var spkey = $("#ddlLandlineOperator option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".landlinedynamicinput").each(function (index, item) {
        var thisid = $(this).data("landlineid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {

                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                return false;
            }
        }
        else {
            return false;
        }
    });

    var amount = $("#hdnTotalLandlinePaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("LandlinePayment", spkey, spkeytext, optionsvalue, optionsids, "ddlLandlineOperator", "modellandlineheadbody", "landlinenotesmsg", $("#btnLandlinePayment"), "Pay Bill");
}


//============================Start Insurance Recharge Section=====================================
$("#ddlInsurance").change(function () {
    $(".insurancedynamicinput").remove();

    var spkey = $(this).val();
    var fetchid = $("#ddlInsurance option:selected").data("fetchid");
    var isbillfetch = $("#ddlInsurance option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("insurance") + ',servicetype: ' + JSON.stringify("INSURANCE") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindInsurance").after(data.d[0]);
                    $("#insurancenotesmsg").html(data.d[1]).removeClass("hidden");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function InsurancePremiumSubmit() {
    var isinsurancefieled = false;
    var thisbutton = $("#btnInsurancePremium");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlInsurance")) return !1;

    $(".insurancedynamicinput").each(function (index, item) {
        var thisid = $(this).data("insuranceid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                isinsurancefieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                isinsurancefieled = false;
                return false;
            }
        }
        else {
            isinsurancefieled = false;
            return false;
        }
    });

    if (isinsurancefieled) {
        var spkey = $("#ddlInsurance").val();
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        CurrBillFeatch("InsuranceBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlInsurance", "modelinsuranceheadbody", "insurancesection", thisbutton, "Get Premium", "Insurance");
    }
}

function InsurancePaymentSubmit() {
    var spkeytext = $("#ddlInsurance option:selected").text();
    var spkey = $("#ddlInsurance option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".insurancedynamicinput").each(function (index, item) {
        var thisid = $(this).data("insuranceid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {

                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                return false;
            }
        }
        else {
            return false;
        }
    });

    var amount = $("#hdnTotalInsurancePaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("InsurancePayment", spkey, spkeytext, optionsvalue, optionsids, "ddlInsurance", "modelinsuranceheadbody", "insurancenotesmsg", $("#btnInsurancePayment"), "Pay Bill");
}

//============================Start GAS Recharge Section=====================================
$("#ddlGasProvider").change(function () {
    $(".gasdynamicinput").remove();

    var spkey = $(this).val();
    var fetchid = $("#ddlGasProvider option:selected").data("fetchid");
    var isbillfetch = $("#ddlGasProvider option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("gas") + ', servicetype: ' + JSON.stringify("GAS") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindGasSection").after(data.d[0]);
                    $("#gasnotesmsg").html(data.d[1]).removeClass("hidden");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function GasBillPaySubmit() {
    var isgasfieled = false;

    var thisbutton = $("#btnGasSumit");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlGasProvider")) return !1;

    $(".gasdynamicinput").each(function (index, item) {
        var thisid = $(this).data("gasid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                isgasfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                isgasfieled = false;
                return false;
            }
        }
        else {
            isgasfieled = false;
            return false;
        }
    });

    if (isgasfieled) {
        var spkey = $("#ddlGasProvider").val();
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        CurrBillFeatch("GasBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlGasProvider", "modelgasheadbody", "gasmodelclickclass", thisbutton, "Get Bill", "Gas");
    }
}

function GasPaymentSubmit() {
    var spkeytext = $("#ddlGasProvider option:selected").text();
    var spkey = $("#ddlGasProvider option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".gasdynamicinput").each(function (index, item) {
        var thisid = $(this).data("gasid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());
                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                return false;
            }
        }
        else {
            return false;
        }
    });

    var amount = $("#hdnTotalGasPaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("GasPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlGasProvider", "modelgasheadbody", "gasnotesmsg", $("#btnGasPayment"), "Pay Bill");
}

//============================Start Broadband Recharge Section=====================================
$("#ddlInternetService").change(function () {
    $(".internetdynamicinput").remove();

    var spkey = $(this).val();
    var fetchid = $("#ddlInternetService option:selected").data("fetchid");
    var isbillfetch = $("#ddlInternetService option:selected").data("isbillfetch");

    if (spkey != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("internet") + ', servicetype: ' + JSON.stringify("INTERNET") + ',spkey: ' + JSON.stringify(spkey) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindInternetSec").after(data.d[0]);
                    $("#internetnotesmsg").html(data.d[1]).removeClass("hidden");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }

});

function BroadbandBillPaySubmit() {
    var isinternetfieled = false;

    var thisbutton = $("#btnBroadband");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlInternetService")) return !1;

    $(".internetdynamicinput").each(function (index, item) {
        var thisid = $(this).data("internetid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                isinternetfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                isinternetfieled = false;
                return false;
            }
        }
        else {
            isinternetfieled = false;
            return false;
        }
    });

    if (isinternetfieled) {
        var spkey = $("#ddlInternetService").val();
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        CurrBillFeatch("InternetBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlInternetService", "modelinternetheadbody", "internetmodelclickclass", thisbutton, "Get Bill", "Broadband");
    }
}

function BroadbandPaymentSubmit() {
    var spkeytext = $("#ddlInternetService option:selected").text();
    var spkey = $("#ddlInternetService option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".internetdynamicinput").each(function (index, item) {
        var thisid = $(this).data("internetid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                iselectricityfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                iselectricityfieled = false;
                return false;
            }
        }
        else {
            iselectricityfieled = false;
            return false;
        }
    });

    var amount = $("#hdnTotalBroadbandPaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("BroadbandPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlInternetService", "modelinternetheadbody", "internetnotesmsg", $("#btnBroadbandPayment"), "Pay Bill");
}

//============================Start Water Recharge Section=====================================
$("#ddlWaterBoard").change(function () {
    $(".waterdynamicinput").remove();

    var boardselected = $(this).val();
    var fetchid = $("#ddlWaterBoard option:selected").data("fetchid");
    var isbillfetch = $("#ddlWaterBoard option:selected").data("isbillfetch");

    if (boardselected != "0") {
        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/BindInputByLabelDel",
            data: '{nametype:' + JSON.stringify("water") + ', servicetype: ' + JSON.stringify("WATER") + ',spkey: ' + JSON.stringify(boardselected) + ', fetchid: ' + JSON.stringify(fetchid) + ',isbillfetch: ' + JSON.stringify(isbillfetch) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d != null) {
                    $("#BindWaterSection").after(data.d[0]);
                    $("#waternotesmsg").html(data.d[1]).removeClass("hidden");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
});

function WaterProceedSubmit() {
    var iswaterfieled = false;
    var thisbutton = $("#btnWaterProceed");

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    if (CheckFocusDropDownBlankValidation("ddlWaterBoard")) return !1;

    $(".waterdynamicinput").each(function (index, item) {
        var thisid = $(this).data("waterid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                iswaterfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                iswaterfieled = false;
                return false;
            }
        }
        else {
            iswaterfieled = false;
            return false;
        }
    });

    if (iswaterfieled) {
        var spkey = $("#ddlWaterBoard").val();
        $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

        CurrBillFeatch("WaterBillFeatch", spkey, optionsname, optionsvalue, optionsids, "ddlWaterBoard", "modelwaterheadbody", "watermodelclickclass", thisbutton, "Get Bill", "Water");
    }
}

function WaterPaymentSubmit() {
    var spkeytext = $("#ddlWaterBoard option:selected").text();
    var spkey = $("#ddlWaterBoard option:selected").val();

    var optionsname = [];
    var optionsvalue = [];
    var optionsids = [];

    $(".waterdynamicinput").each(function (index, item) {
        var thisid = $(this).data("waterid");
        var thislabel = $(this).data("labels");
        var minlength = $("#" + thisid).attr("minlength");
        var maxlength = $("#" + thisid).attr("maxlength") == undefined ? 0 : $("#" + thisid).attr("maxlength");

        var inputVal = $("#" + thisid).val();
        var inputplaceholder = $("#" + thisid).attr("placeholder");

        if (!CheckFocusBlankValidation(thisid)) {
            if (CheckMinMaxValidation(inputVal, parseInt(minlength), parseInt(maxlength))) {
                iselectricityfieled = true;
                optionsname.push(thislabel);
                optionsvalue.push($("#" + thisid).val());

                optionsids.push($("#" + thisid));
            }
            else {
                ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", (inputplaceholder + " not valid."), "#d91717");
                iselectricityfieled = false;
                return false;
            }
        }
        else {
            iselectricityfieled = false;
            return false;
        }
    });

    var amount = $("#hdnTotalWaterPaidAmt").val();
    optionsvalue.push(amount);

    DoPayment("WaterPayment", spkey, spkeytext, optionsvalue, optionsids, "ddlWaterBoard", "modelwaterheadbody", "waternotesmsg", $("#btnWaterPayment"), "Pay Bill");
}

//============================Common Section=====================================
var actionType = null;

$(".actiontype").click(function () {
    $(this).removeClass("active");
    actionType = $(this).data("valtext");    

    if (actionType == "mobile") { ResetMobile(); $("#rdoPrepaid").click(); }
    if (actionType == "dth") { $("#dthnotesmsg").html("").addClass("hidden"); $(".dthsecation").addClass("hidden"); $("#txtDTHNumber").val(""); $("#txtDTHAmount").val(""); BindOpratorByType("ddlDTHOprator", "DTH", 'Operator'); }
    if (actionType == "electricity") { $("#electricitynotesmsg").html("").removeClass("hidden"); $(".electricityynamicinput").remove(); BindOpratorByType("ddlElectricityBoard", "ELECTRICITY", 'Electricity Board'); }
    if (actionType == "landline") { $("#landlinenotesmsg").html("").removeClass("hidden"); $(".landlinedynamicinput").remove(); BindOpratorByType("ddlLandlineOperator", "LANDLINE", 'Operator'); }
    if (actionType == "insurance") { $("#insurancenotesmsg").html("").removeClass("hidden"); $(".insurancedynamicinput").remove(); BindOpratorByType("ddlInsurance", "INSURANCE", 'Insurer'); }
    if (actionType == "gas") { $("#gasnotesmsg").html("").removeClass("hidden"); $(".gasdynamicinput").remove(); BindOpratorByType("ddlGasProvider", "GAS", 'Gas Provider'); }
    if (actionType == "internet") { $("#internetnotesmsg").html("").removeClass("hidden"); $(".internetdynamicinput").remove(); BindOpratorByType("ddlInternetService", "INTERNET/ISP", 'Operator'); }
    if (actionType == "water") { $("#waternotesmsg").html("").removeClass("hidden"); $(".waterdynamicinput").remove(); BindOpratorByType("ddlWaterBoard", "WATER", 'Board'); }
    if (actionType == "billtranshistory") { BindBillTransHistory('today'); ReBindFilter(); }
});

function BindOpratorByType(dropdownid, servicetype, selectname) {
    $("#" + dropdownid).prop("disabled", true);
    $.ajax({
        type: "Post",
        url: "/DMT-Manager/BillPayment.aspx/BindOpratorByType",
        data: '{servicetype: ' + JSON.stringify(servicetype) + ',selectname: ' + JSON.stringify(selectname) + '}',
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            if (data.d != null) {
                $("#" + dropdownid).html(data.d);
                $("#" + dropdownid).prop("disabled", false);
            }
        }
    });
}

function ShowMessagePopup(headerHeading, headcolor, bodyMsg, bodycolor) {
    $(".successmessage").click();
    $(".modelheading").css("color", headcolor).html(headerHeading);
    $(".sucessmsg").css("color", bodycolor).html(bodyMsg);
}

function GetLabelUpdationNotes(billupdation) {
    var result = "";

    if (billupdation == "T+0") {
        result = "<i class='fa fa-star text-danger' style='font-size: 8px;'></i> Your service provider will take one working days to consider bill paid in their accounts.";
    }
    else if (billupdation == "T+1") {
        result = "<i class='fa fa-star text-danger' style='font-size: 8px;'></i> Your service provider will take two working days to consider bill paid in their accounts.";
    }
    else if (billupdation == "T+2") {
        result = "<i class='fa fa-star text-danger' style='font-size: 8px;'></i> Your service provider will take three working days to consider bill paid in their accounts.";
    }
    else if (billupdation == "T+3") {
        result = "<i class='fa fa-star text-danger' style='font-size: 8px;'></i> Your service provider will take four working days to consider bill paid in their accounts.";
    }
    return result;
}

function GetLabelMinMaxNotes(labelname, minlen, maxlen) {
    return "<i class='fa fa-star text-danger' style='font-size: 8px;'></i> Please enter your " + labelname + " from " + minlen + " to " + maxlen + " digit."
}

function CurrBillFeatch(poststr, spkey, optionsname, optionsvalue, optionsids, ddldropdown, modelbody, modelclickclass, thisbutton, thisbuttontext, actiontype) {
    $.ajax({
        type: "Post",
        contentType: "application/json; charset=utf-8",
        url: "/DMT-Manager/BillPayment.aspx/" + poststr,
        data: '{spkey: ' + JSON.stringify(spkey) + ',optionsname: ' + JSON.stringify(optionsname) + ',optionsvalue: ' + JSON.stringify(optionsvalue) + ',actiontype:' + JSON.stringify(actiontype) + '}',
        datatype: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d[0] == "000") {
                    //ResetFields("#" + ddldropdown, optionsids);
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);
                    $("." + modelclickclass).click();
                }
                else if (data.d[0] == "reload") {
                    window.location.reload();
                }
                else if (data.d[0] == "error") {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", data.d[1], "#d91717");
                }
                else {
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);
                    $("." + modelclickclass).click();
                }
            }
            $(thisbutton).html(thisbuttontext).prop('disabled', false);
        },
        failure: function (response) {
            alert("failed");
        }
    });
}

//function DoPayment(poststr, spkey, optionsname, optionsvalue, optionsids, ddldropdown, modelbody, modelclickclass, thisbutton, thisbuttontext) {
function DoPayment(poststr, spkey, spkeytext, optionsvalue, optionsids, ddldropdown, modelbody, notesmsg, thisbutton, thisbuttontext) {
    $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

    $.ajax({
        type: "Post",
        contentType: "application/json; charset=utf-8",
        url: "/DMT-Manager/BillPayment.aspx/" + poststr,
        data: '{spkeytext: ' + JSON.stringify(spkeytext) + ',spkey: ' + JSON.stringify(spkey) + ',optionsvalue: ' + JSON.stringify(optionsvalue) + '}',
        datatype: "json",
        success: function (data) {
            if (data.d != null) {
                //$(".closepopup").click();
                if (data.d[0] == "000") {
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);

                    if (notesmsg == "dthnotesmsg") { $(".dthmodelclickclass").click(); }
                    ResetFields("#" + ddldropdown, optionsids, notesmsg);
                }
                else if (data.d[0] == "reload") {
                    window.location.reload();
                }
                else if (data.d[0] == "error") {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", data.d[1], "#d91717");
                }
                else {
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);

                    if (notesmsg == "dthnotesmsg") { $(".dthmodelclickclass").click(); }
                }
            }
            $(thisbutton).html(thisbuttontext).prop('disabled', false);
        },
        failure: function (response) {
            alert("failed");
        }
    });
}

function DoDirectPayment(poststr, spkey, spkeytext, optionsvalue, optionsids, ddldropdown, modelbody, modelclassclick, notesmsg, thisbutton, thisbuttontext) {
    $(thisbutton).html("Please Wait... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);

    $.ajax({
        type: "Post",
        contentType: "application/json; charset=utf-8",
        url: "/DMT-Manager/BillPayment.aspx/" + poststr,
        data: '{spkeytext: ' + JSON.stringify(spkeytext) + ',spkey: ' + JSON.stringify(spkey) + ',optionsvalue: ' + JSON.stringify(optionsvalue) + '}',
        datatype: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d[0] == "000") {
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);
                    $("." + modelclassclick).click();

                    DirectResetFields("#" + ddldropdown, optionsids, notesmsg);
                }
                else if (data.d[0] == "reload") {
                    window.location.reload();
                }
                else if (data.d[0] == "error") {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Failed !", "#d91717", data.d[1], "#d91717");
                }
                else {
                    $("#" + modelbody).html("");
                    $("#" + modelbody).html(data.d[1]);
                    $("." + modelclassclick).click();
                }
            }
            $(thisbutton).html(thisbuttontext).prop('disabled', false);
        },
        failure: function (response) {
            alert("failed");
        }
    });
}

function CheckMinMaxValidation(inputVal, minlen, maxlen) {
    var inputlen = inputVal.length;

    var samelen = minlen == maxlen ? true : false;

    maxlen = samelen == true ? 0 : maxlen;

    if (maxlen > 0) {
        if (inputlen >= minlen && inputlen <= maxlen) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        if (minlen == inputlen) {
            return true;
        }
        else {
            return false;
        }
    }
}

function ResetFields(ddldropdown, optionsids, notesmsg) {
    $(ddldropdown).val("0").change();
    $("#" + notesmsg).html("");

    $(optionsids).each(function (index, item) {
        $(this).val("");
    });
}

function DirectResetFields(ddldropdown, optionsids, notesmsg) {
    $(ddldropdown).val("0").change();
    $("#" + notesmsg).html("").addClass("hidden");

    $(optionsids).each(function (index, item) {
        $(this).val("");
    });
}
//-------------------------------------------------------------------------------

function BindBillTransHistory(defaultdate) {
    var fromdate = $("#txtBillFromDate").val();
    var todate = $("#txtBillToDate").val();
    var clintrefid = $("#txtClientRefID").val();
    var transtype = $("#ddlBillTransType option:selected").val();
    var status = $("#ddlBillTransStatus option:selected").val();

    $.ajax({
        type: "Post",
        contentType: "application/json; charset=utf-8",
        url: "/DMT-Manager/BillPayment.aspx/GetBillTransactionHistory",
        data: '{fromdate: ' + JSON.stringify(fromdate) + ',todate: ' + JSON.stringify(todate) + ',clintrefid: ' + JSON.stringify(clintrefid) + ',transtype: ' + JSON.stringify(transtype) + ',status: ' + JSON.stringify(status) + ',defaultdate: ' + JSON.stringify(defaultdate) + '}',
        datatype: "json",
        success: function (data) {
            if (data != null) {
                if (data.d[0] == "success") {
                    $("#BillTransRowDetails").html("");
                    $("#BillTransRowDetails").html(data.d[1]);
                }
                $("#btnBillSearchSubmit").html("Search").prop('disabled', false);
            }
        },
        failure: function (response) {
            alert("failed");
        }
    });
}

//function FilterBindBillTransHistory() {
//    var fromdate = $("#txtBillFromDate").val();
//    var todate = $("#txtBillToDate").val();
//    var clintrefid = $("#txtClientRefID").val();
//    var transtype = $("#ddlBillTransType option:selected").val();
//    var status = $("#ddlBillTransStatus option:selected").val();

//    $.ajax({
//        type: "Post",
//        contentType: "application/json; charset=utf-8",
//        url: "/DMT-Manager/BillPayment.aspx/GetBillTransactionHistory",
//        data: '{fromdate: ' + JSON.stringify(fromdate) + ',todate: ' + JSON.stringify(todate) + ',clintrefid: ' + JSON.stringify(clintrefid) + ',transtype: ' + JSON.stringify(transtype) + ',status: ' + JSON.stringify(status) + '}',
//        datatype: "json",
//        success: function (data) {
//            if (data != null) {
//                if (data.d[0] == "success") {
//                    $("#BillTransRowDetails").html("");
//                    $("#BillTransRowDetails").html(data.d[1]);
//                }
//            }
//        },
//        failure: function (response) {
//            alert("failed");
//        }
//    });
//}

function BillSearchSubmit() {
    $("#btnBillSearchSubmit").html("Searching... <i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true);
    BindBillTransHistory('filter');
}

function BillSearchClear() {
    $("#btnTransFilter").html("Search");
    $("#txtBillFromDate").val('');
    $("#txtBillToDate").val('');
    $("#txtClientRefID").val('');
    $("#ddlBillTransType").prop('selectedIndex', '').change();
    $("#ddlBillTransStatus").prop('selectedIndex', '').change();
    BindBillTransHistory('today');
}

function ReBindFilter() {
    $("#txtBillFromDate").val('');
    $("#txtBillToDate").val('');
    $("#txtClientRefID").val('');
    $("#ddlBillTransType").prop('selectedIndex', '').change();
    $("#ddlBillTransStatus").prop('selectedIndex', '').change();
}

function RefundBillFaildAmount(transid) {
    var thisbutton = $("#btnBillRefund_" + transid);

    var amount = thisbutton.data("amount");
    var trackid = thisbutton.data("trackid");
    var reportid = thisbutton.data("reportid");

    if (reportid != "") {
        $(thisbutton).html("<i class='fa fa-pulse fa-spinner'></i>");

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/ProcessBillToReFund",
            data: '{transid: ' + JSON.stringify(transid) + ',amount: ' + JSON.stringify(amount) + ',trackid: ' + JSON.stringify(trackid) + ',reportid: ' + JSON.stringify(reportid) + '}',
            datatype: "json",
            success: function (data) {
                $("#BillTransRowDetails").html("");
                $("#BillTransRowDetails").html(data.d[1]);

                if (data.d[0] == "false") {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Error Occurred !", "#d91717", "This Transaction is not for refund !", "#d91717");
                }
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
    else {
        ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;Error Occurred !", "#d91717", "This Transaction is not for refund !", "#d91717");
    }
}

function CallBackCheckStatus(billid) {
    var thisbutton = $("#btnCallBackCheckStatus_" + billid);

    var transid = $(thisbutton).data("transid");
    var clientrefid = $(thisbutton).data("refid");
    var status = $(thisbutton).data("status");

    if (transid != "" && clientrefid != "") {
        $(thisbutton).html("Checking...<i class='fa fa-pulse fa-spinner'></i>").prop('disabled', true).css("color", "blue");

        $.ajax({
            type: "Post",
            contentType: "application/json; charset=utf-8",
            url: "/DMT-Manager/BillPayment.aspx/ProcessToCallBackCheckStatus",
            data: '{transid: ' + JSON.stringify(transid) + ',refid: ' + JSON.stringify(clientrefid) + ',status: ' + JSON.stringify(status) + ',billid: ' + JSON.stringify(billid) + '}',
            datatype: "json",
            success: function (data) {
                if (data.d[0] == "000") {
                    if (data.d[3] != "") {
                        $("#BillTransRowDetails").html("");
                        $("#BillTransRowDetails").html(data.d[3]);
                    }

                    ShowMessagePopup(data.d[1], "#2dbe60", data.d[2], "#2dbe60");
                }
                else if (data.d[0] == "999") {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;" + data.d[0], "#d91717", data.d[1], "#d91717");
                }
                else {
                    ShowMessagePopup("<i class='fa fa-exclamation-triangle text-danger' aria-hidden='true'></i>&nbsp;" + data.d[0], "#d91717", data.d[1], "#d91717");
                }
                
                $(thisbutton).html("Check Status").prop('disabled', false).css("color", "#ff414d");
            },
            failure: function (response) {
                alert("failed");
            }
        });
    }
}