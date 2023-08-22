function ShowHide(visibles, checkin) {
    if (visibles == "show") {
        ValidateCheckinDate(checkin);
    }
    else if (visibles == "hide") {
        document.getElementById('htlRfndPopup').style.visibility = "hidden";
    }
}

function RemarkValidation(RmkType) {
    if ($("#txtRemarkss").val() == "") {
        alert("Please provide " + RmkType + " remark");
        $("#txtRemarkss").focus();
        return false;
    }
    //Validate Nights
    else if ($("#ChkParcialCan").checked) {
        if ($("#NoOfDay").val() == "") {
            alert("Please provide no of night for partial cancellation");
            $("#NoOfDay").focus();
            return false;
        }
        else if (parseInt($("#NoOfDay").val()) > parseInt($("#ctl00_ContentPlaceHolder1_night").innerText)) {
            alert("No of night should be less then total night of booking for partial cancellation");
            $("#NoOfDay").focus();
            return false;
        }
    }
    else
        if (confirm("Are you sure for cancel this booking ?"))
        return true;
    return false;
}

function Visibility() {
    if (document.getElementById('ChkParcialCan').checked) {
        document.getElementById('checkInOut').style.display = 'block';
        $("#Parcial").val("true");
        getvalue();
    }
    else {
        document.getElementById('checkInOut').style.display = 'none';
        $("#Parcial").val("false");
    }
}

function getvalue() {
    if (document.getElementById("Chekin").checked)
        document.getElementById("StartDate").value = "true"
    else
        document.getElementById("StartDate").value = "false"

    if (document.getElementById("Chekout").checked)
        document.getElementById("EndDate").value = "true"
    else
        document.getElementById("EndDate").value = "false"
}

//Checkin Date Validation from Today date
function ValidateCheckinDate(checkin) {
 
    var strChechin = checkin.split("-");
    var now = new Date();
    var strNow = now.format("dd/MM/yyyy").split("/");
    var ChechinDate = getNumberOfDays(strChechin[1], strChechin[0]) + strChechin[2];
    var TodayDate = getNumberOfDays(strNow[1], strNow[2]) + strNow[0];
    if ((ChechinDate - TodayDate) >= 0) {
        
        //$("#htlRfndPopup").show();
        document.getElementById('htlRfndPopup').style.visibility = "visible";
    }
    else {
        alert('Cancellation request can not be process for this booking.');
        return false; 
    }
}
//Date Calculation for Validation
function getNumberOfDays(month, year) {
    var noOfLpYear = parseInt((year - 1) / 4) - parseInt((year - 1) / 100) + parseInt((year - 1) / 400);
    var noOfYear = (year - 1) - noOfLpYear;
    var daysInYear = (noOfLpYear * 366) + (noOfYear * 365);
    var lpYear = 0;
    if ((year % 400 == 0 && year % 100 == 0) || (year % 4 == 0 && year % 100 != 0)) {
        lpYear = 1;
    }
    switch (month) {
        case '1': return daysInYear + 0;
        case '01': return daysInYear + 0;
        case '2': return daysInYear + 31;
        case '02': return daysInYear + 31;
        case '3': return daysInYear + lpYear + 59;
        case '03': return daysInYear + lpYear + 59;
        case '4': return daysInYear + lpYear + 89
        case '04': return daysInYear + lpYear + 89
        case '5': return daysInYear + lpYear + 120;
        case '05': return daysInYear + lpYear + 120;
        case '6': return daysInYear + lpYear + 150;
        case '06': return daysInYear + lpYear + 150;
        case '7': return daysInYear + lpYear + 181;
        case '07': return daysInYear + lpYear + 181;
        case '8': return daysInYear + lpYear + 212;
        case '08': return daysInYear + lpYear + 212;
        case '9': return daysInYear + lpYear + 242;
        case '09': return daysInYear + lpYear + 242;
        case '10': return daysInYear + lpYear + 273;
        case '11': return daysInYear + lpYear + 303;
        case '12': return daysInYear + lpYear + 334;
    }
}

function Refundpopup(OID, HotelName, Amount, Room, Night, Adult, child) {

    ShowHide('show', '');
    $("#OrderIDS").val(OID);
    $("#RemarkTitle").text("Hotel Cancellation for order id (" + OID + ")");
    $("#HotelName").text(HotelName);
    $("#amt").text(Amount);
    $("#room").text(Room);
    $("#night").text(Night);
    $("#adt").text(Adult);
    $("#chd").text(child);
}


$(document).ready(function() {

 $("input[name=Can]:radio").change(function() {
        if ($('#ChkFullCan').is(':checked')) {
            $("#Parcial").val("false");
        }
        if ($('#ChkParcialCan').is(':checked')) {
            $("#Parcial").val("true");
        }
    });

//Cancelation Plicy show Hide Checkobx Start
    $("#ctl00_ContentPlaceHolder1_chkTC").click(function() {
        if ($('#ctl00_ContentPlaceHolder1_chkTC').is(':checked')) {
            $("#Canpolicy").show();
        }
        else {
            $("#Canpolicy").hide();
        }
    });
     //Cancelation Plicy show Hide Checkobx End
    $(".brekups").mouseenter(function() {
        if ($('#ctl00_ContentPlaceHolder1_lblProvider').text() == "EX") {
            $(".EXDetailsBreakup").show();
        }
        else {

            $(".FareBreakups").show();
            $(".DtlFareBreakups").show();
        }
    }).mouseleave(function() {
        if ($('#ctl00_ContentPlaceHolder1_lblProvider').text() == "EX") {
            $(".EXDetailsBreakup").hide();

        }
        else {
            $(".FareBreakups").hide();
            $(".DtlFareBreakups").hide();
        }
    });
//Checkout page validation strat
 $("#ctl00_ContentPlaceHolder1_btnPayment").click(function() {

    if ($("#ctl00_ContentPlaceHolder1_Fname").val() == "") {
        alert("Please Provide First Name");
        $("#ctl00_ContentPlaceHolder1_Fname").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_Lname").val() == "") {
        alert("Please Provide Last Name");
        $("#ctl00_ContentPlaceHolder1_Lname").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_TB_AddLine").val() == "") {
        alert("Please Provide Address");
        $("#ctl00_ContentPlaceHolder1_TB_AddLine").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_TB_City").val() == "") {
        alert("Please Provide City Name");
        $("#ctl00_ContentPlaceHolder1_TB_City").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_DDL_State").val() == "") {
        alert("Please Provide State Name");
        $("#ctl00_ContentPlaceHolder1_DDL_State").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_txtCountry").val() == "") {
        alert("Please Provide Country Name");
        $("#ctl00_ContentPlaceHolder1_txtCountry").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_TB_PinCode").val() == "") {
        alert("Please Provide Pin Code");
        $("#ctl00_ContentPlaceHolder1_TB_PinCode").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_txt_email").val() == "") {
          alert("Please Provide Email id");
        $("#ctl00_ContentPlaceHolder1_txt_email").focus();
        return false;
    }
    if ($("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").val() == "") {
        alert("Please Provide Phone Number");
        $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").focus();
        return false;
    }
    else {
        var phone = $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").val()
        if (phone.length < 10) {
            alert("Please Provide valid Phone Number with STD Code\n\n Valid Format is : 01146464140");
            $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").focus();
            return false;
        }
    }

    var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    var emailid = $("#ctl00_ContentPlaceHolder1_txt_email").val();
     
    var matchArray = emailid.match(emailPat);
    if (matchArray == null) {
        alert("Your email address seems incorrect. Please provide correct formate of email.");
        $("#ctl00_ContentPlaceHolder1_txt_email").focus();
        return false;
    }
    if ($('#ctl00_ContentPlaceHolder1_chkTC').is(':checked') == false) {
        alert("Please check the Terms & Conditions box.");
        $("#ctl00_ContentPlaceHolder1_chkTC").focus(); return false;
    }
    var elem = document.getElementById('tblrpt').getElementsByTagName('input');

    for (var i = 0; i < elem.length; i++) {
        if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "First Name" && elem[i].id.indexOf('txtFName') > 0) {
            alert("Please Provide Additinal Guest First Name");
            elem[i].focus();
            return false;
        }
    }
    return true;
        
    });
 //Checkout page validation End
});


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 48 && charCode <= 57 || charCode == 08) {
        return true;
    }
    else {
        return false;
    }
}
function isCharKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
        if (charCode > 90 && charCode < 97)
            return false;
        else
            return true;
    }
    else {
        return false;
    }
}

function RejectRemark(OID) {
    $("#OrderIDS").val(OID);
    document.getElementById('htlRfndPopup').style.visibility = "visible";
}
//Hotel Remark validation strat
function RemarkValidate() {
    if ($("#txtRemark").val() == "") {
        alert("Please provide reject remark");
        $("#txtRemark").focus();
        return false;
    }
    else
        if (confirm("Are you sure to Reject?"))
        return true;
    return false;
}
//Hotel Remark validation End

//Close Popup
function closepopup() {
    document.getElementById('htlRfndPopup').style.visibility = "hidden";
}

// Update Hotel Refund strat
function RefundUpdateValidate() {
    if ($("#txtRefundCharge").val() == "") {
        alert("Please Provide Cancellation Charge");
        $("#txtRefundCharge").focus();
        return false;
    }
    else if ($("#txtServiceCharge").val() == "") {
        alert("Please Provide Service Charge");
        $("#txtServiceCharge").focus();
        return false;
    }
    else if ($("#txtRemark").val() == "") {
        alert("Please Provide Update Remark");
        $("#txtRemark").focus();
        return false;
    }
    else
        if (confirm("Are you sure for Refund this booking id?"))
        return true;
    return false;
}
// Update Hotel Refund END
// Hold Booking Update validation
function HoldBookingValidate() {
    if ($("#txtHtlBID").val() == "") {
        alert("Please Provide Booking ID");
        $("#txtHtlBID").focus();
        return false;
    }
    else if ($("#txtHtlConfNo").val() == "") {
        alert("Please Provide Confirmation No");
        $("#txtHtlConfNo").focus();
        return false;
    }
    return true;
}

// Hold Booking Update validation
function HoldBookingValidate() {
    if ($("#txtHtlBID").val() == "") {
        alert("Please Provide Booking ID");
        $("#txtHtlBID").focus();
        return false;
    }
    else if ($("#txtHtlConfNo").val() == "") {
        alert("Please Provide Confirmation No");
        $("#txtHtlConfNo").focus();
        return false;
    }
    else if ($("#txtRemark").val() == "") {
        alert("Please Provide Remark");
        $("#txtRemark").focus();
        return false;
    }
    return true;
}

function ShowHoldBookingPopup(OID, ReqType) {
    $("#OrderIDS").val(OID);
    if (ReqType == 'Reject') {
        document.getElementById('TR_Reject').style.visibility = "visible";
        document.getElementById('TRBooedUpdate').style.visibility = "hidden";
    }
    if (ReqType == 'Update') {
        document.getElementById('TRBooedUpdate').style.visibility = "visible";
        document.getElementById('TR_Reject').style.visibility = "hidden";
    }
    document.getElementById('htlRfndPopup').style.visibility = "visible";

    if (ReqType == 'ClosePopup') {
        document.getElementById('htlRfndPopup').style.visibility = "hidden";
        document.getElementById('TR_Reject').style.visibility = "hidden";
        document.getElementById('TRBooedUpdate').style.visibility = "hidden";
    }
}

function AvoideAnd_singleQuote(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
     
    if (charCode == 38 || charCode == 39 || charCode == 47)
        return false;
    else
        return true;
}

// Show cot information for 1 minut
function ShowAlertMassage() {
    $("#CotMsg").show();
    $('#CotMsg').delay(60000).queue(function() { $(this).hide(); $(this).dequeue(); })
}
// Show cot information for 1 minut
function ShowRoomAlertMassage(msg) {
    $("#ExtraRoomMsg").show();
    $("#ExtraRoomMsg").text(msg);
}
// Hold Booking Update validation end

//function CheckoutValidate() {

//    if ($("#ctl00_ContentPlaceHolder1_Fname").val() == "") {
//        alert("Please Provide First Name");
//        $("#ctl00_ContentPlaceHolder1_Fname").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_Lname").val() == "") {
//        alert("Please Provide Last Name");
//        $("#ctl00_ContentPlaceHolder1_Lname").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_TB_AddLine").val() == "") {
//        alert("Please Provide Address");
//        $("#ctl00_ContentPlaceHolder1_TB_AddLine").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_TB_City").val() == "") {
//        alert("Please Provide City Name");
//        $("#ctl00_ContentPlaceHolder1_TB_City").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_DDL_State").val() == "") {
//        alert("Please Provide State Name");
//        $("#ctl00_ContentPlaceHolder1_DDL_State").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_txtCountry").val() == "") {
//        alert("Please Provide Country Name");
//        $("#ctl00_ContentPlaceHolder1_txtCountry").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_TB_PinCode").val() == "") {
//        alert("Please Provide Pin Code");
//        $("#ctl00_ContentPlaceHolder1_TB_PinCode").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_txt_email").val() == "") {
//          alert("Please Provide Email id");
//        $("#ctl00_ContentPlaceHolder1_txt_email").focus();
//        return false;
//    }
//    if ($("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").val() == "") {
//        alert("Please Provide Phone Number");
//        $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").focus();
//        return false;
//    }
//    else {
//        var phone = $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").val()
//        if (phone.length < 10) {
//            alert("Please Provide valid Phone Number with STD Code\n\n Valid Format is : 01146464140");
//            $("#ctl00_ContentPlaceHolder1_txtCIPhoneNo").focus();
//            return false;
//        }
//    }

//    var emailPat = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
//    var emailid = $("#ctl00_ContentPlaceHolder1_txt_email").val();
//     
//    var matchArray = emailid.match(emailPat);
//    if (matchArray == null) {
//        alert("Your email address seems incorrect. Please provide correct formate of email.");
//        $("#ctl00_ContentPlaceHolder1_txt_email").focus();
//        return false;
//    }
//    if ($('#ctl00_ContentPlaceHolder1_chkTC').is(':checked') == false) {
//        alert("Please check the Terms & Conditions box.");
//        $("#ctl00_ContentPlaceHolder1_chkTC").focus(); return false;
//    }
//    var elem = document.getElementById('tblrpt').getElementsByTagName('input');

//    for (var i = 0; i < elem.length; i++) {
//        if (elem[i].type == "text" && elem[i].value == "" || elem[i].value == "First Name" && elem[i].id.indexOf('txtFName') > 0) {
//            alert("Please Provide Additinal Guest First Name");
//            elem[i].focus();
//            return false;
//        }
//    }
//    return true;
//}