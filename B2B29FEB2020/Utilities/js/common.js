function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) { var urlparam = url[i].split('='); if (urlparam[0] == param) { return urlparam[1]; } }
}

function ValidateEmail(email) {
    var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    var valid = emailReg.test(email);
    if (!valid) { return false; }
    else { return true; }
}

function addErrorClass(i, msg) {
    removeErrorClass(i);
    $("#" + i).closest('div.form-validation').addClass('has-error');
    $("#" + i).closest('div.form-validation').append("<span class='help-error-block pull-right' title='This field is required.'><i class='fa fa-star'></i>&nbsp;" + msg + "</span>");
    $("#" + i).focus();
}

function addFocusErrorClass(i, msg) {
    removeErrorClass(i);
    $("#" + i).closest('div.form-validation').addClass('has-error');
    $("#" + i).focus();
}

function removeErrorClass(i) {
    $("#" + i).closest('div.form-validation').removeClass('has-error');
    $("#" + i).closest('div.form-validation').find('span.help-error-block').remove();
}

function CheckBlankValidation(i) {
    if ($("#" + i).val().trim() === "") {
        addErrorClass(i, 'This field is required.');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckNumberBlankValidation(i) {
    if ($("#" + i).val().trim() == "0") {
        addErrorClass(i, 'This field is required.');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckDropDownBlankValidation(i) {
    if ($("#" + i + " option:selected").val().trim() == "" || $("#" + i + " option:selected").val().trim() == "0") {
        addErrorClass(i, 'This field is required.');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckFocusBlankValidation(i) {
    if ($("#" + i).val() == "") {
        addFocusErrorClass(i, '');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckSameAccountValidation(i, t) {
    if ($("#" + i).val() != $("#" + t).val()) {
        addErrorClass(t, 'not same as account number you have entered.');
        return true;
    }
    else { removeErrorClass(t); }
}

function CheckFocusDropDownBlankValidation(i) {
    if ($("#" + i + " option:selected").val() == "" || $("#" + i + " option:selected").val().trim() == "0") {
        addFocusErrorClass(i, '');
        return true;
    }
    else { removeErrorClass(i); }
}

function BankCheckFocusDropDownBlankValidation(i) {
    var bankval = $("#" + i + " option:selected").val();
    var banktext = $("#" + i + " option:selected").text().toLowerCase();

    if (bankval == "" && banktext == "select bank") {
        addFocusErrorClass(i, '');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckFocusCancellationBlankValidation(i) {
    if ($("#" + i).val().trim() == "") {
        $("#" + i).css("border-bottom", "");
        $("#" + i).css("border", "1px solid red");
        $("#" + i).focus();
        return true;
    }
    else { $("#" + i).css("border", ""); }
}

function CheckFocusChekoutBlankValidation(i) {
    if ($("#" + i).val().trim() == "") {
        $("#" + i).css("border-bottom", "");
        $("#" + i).css("border-bottom", "1px solid red");
        $("#" + i).focus();
        return true;
    }
    else { $("#" + i).css("border-bottom", ""); }
}

function CheckEmailValidatoin(i) {
    if (!ValidateEmail($("#" + i).val().trim())) {
        addErrorClass(i, 'Email is not valid.');
        return true;
    }
    else { removeErrorClass(i); }
}

function PrintCheckEmailValidatoin(i) {
    if (!ValidateEmail($("#" + i).val().trim())) {
        alert("Email is not valid.");
        return true;
    }
}

function CheckBoxCheckedValidation(i) {
    if (!$("#" + i).is(':checked')) {
        addErrorClass(i, 'You must agree to it.');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckRadioButtonListCheckedValidation(i) {
    var radiolistchecked = '0';
    $('#' + i + ' input[type="radio"]').each(function () {
        if ($(this).is(':checked')) {
            radiolistchecked = '1';
        }
    });

    if (radiolistchecked == '0') {
        addErrorClass(i, 'You must select one of it.');
        return true;
    }
    else { removeErrorClass(i); }
}

function CheckSamePasswordValidation(i, t) {
    if ($("#" + i).val() != $("#" + t).val()) {
        addErrorClass(t, 'Not same as password you have entered.');
        return true;
    }
    else { removeErrorClass(t); }
}

function noSpaceValidation(evt, t) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 32) {
        addErrorClass($(t).attr('id'), 'This field can\'t contain space');
        return false;
    }
    removeErrorClass($(t).attr('id'));
    return true;
}

function isNumberValidation(evt, t) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57))) {
        addErrorClass($(t).attr('id'), 'Enter numeric values only');
        return false;
    }
    removeErrorClass($(t).attr('id'));
    return true;
}

function isNumberValidationPrevent(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57))) {
        return false;
    }
    return true;
}

function isStringValidationPrevent(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (!((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90))) {
        return false;
    }
    return true;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57))) {
        alert('Accept only numeric values');
        return false;
    }
    return true;
}

function isNumberKeyWithSpace(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && charCode != 32 && (charCode < 48 || charCode > 57)) {
        alert('Accept only numeric values');
        return false;
    }
    return true;
}

function isDecimalOnlyKey(event, e) {
    if ((event.which != 46 || $(e).val().indexOf('.') != -1) && ((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
}

function toTitleCase(str) {
    return str.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}

function openPopUp(theURL, winName, features) {
    mapWin = window.open(theURL, winName, features);
    mapWin.focus();
}

function validateNumbersOnly(input, kbEvent) {
    var keyCode, keyChar;
    keyCode = kbEvent.keyCode;

    if (window.event) {
        keyCode = kbEvent.keyCode; // IE
    }
    else {
        keyCode = kbEvent.which; //firefox
    }

    if (keyCode == null) { return true };
    // get character

    keyChar = String.fromCharCode(keyCode);
    var charSet = "0123456789. ";
    // check valid chars
    if (charSet.indexOf(keyChar) != -1) { return true };
    // control keys

    if (keyCode > 31 && keyCode != 32 && (keyCode < 48 || keyCode > 57)) {
        alert('Accept only numeric values');
        return false;
    }

    return true;
}

function isNumberOnlyKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && charCode != 32 && (charCode < 48 || charCode > 57)) {
        alert('Accept only numeric values');
        return false;
    }
    return true;
}

function isNumberOnlyKeyNoDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && charCode != 32 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

