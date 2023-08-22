//Place all functions that can be used by all modules here

//change date format from dd/mm/yyyy to javascript date objects
function ChangeDateFormat(val) {
    if (val && val != "") {
        var dts = val.split("/");        
        return new Date(dts[2], dts[1] - 1, dts[0]);        
    }
    else {
        return new Date();
    }
}


//format date for displaying purpose. e.g. 12 Aug, 2011
//Val = value of date in dd/mm/yyyy format
function FormatDateForDisplay(val) {
    var dt = ChangeDateFormat(val);
    var day = dt.getDate();
    var month = GetMonthShortName(dt.getMonth());
    var year = dt.getFullYear();

    return day + " " + month + ", " + year;
}


//Get short name of a month
//month = month number
function GetMonthName(month) {
    var m_names = [
        "January", "February", "March", "April",
        "May", "June", "July", "August",
        "September", "October", "November", "December"
    ];
    return m_names[month];
}


//Get short name of a month
//month = month number
function GetMonthShortName(month) {
    var m_names = [
        "Jan", "Feb", "Mar", "Apr",
        "May", "Jun", "Jul", "Aug",
        "Sep", "Oct", "Nov", "Dec"
    ];
    return m_names[month];
}

//Extracts date with time from date string ddmmyy and time string hhmm formats
//E.g. strDate = "280811", strTime = "1420"
function GetDateTimeFromString(strDate, strTime) {
    var dd = strDate.substr(0, 2);
    var mm = strDate.substr(2, 2);
    var yyyy = "20" + strDate.substr(4, 2);

    var hh = "00";
    var mins = "00";
    if (strTime) {
        hh = strTime.substr(0, 2);
        mins = strTime.substr(2, 2);
    }
        
    return new Date(yyyy, mm - 1, dd, hh, mins, 0);
}

//Exracts hours, minutes and seconds from a time string. E.g. 1430
function GetTimeSpan(strTime){
    var hh = strTime.substr(0, 2);
    var mins = strTime.substr(2, 2);
    var sec = 0;
    if (strTime.length > 4) {
        sec = strTime.substr(4, 2);
    }
    return {
        hours: hh,
        minutes: mins,
        seconds: sec
    }
}

//Add hours to a date
//hours shoud be integer
Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + parseInt(h));
    return this;
}

//Add minutes to a date
//minutes should be integer
Date.prototype.addMinutes = function (m) {
    this.setMinutes(this.getMinutes() + parseInt(m));
    return this;
}

//Add days to a date
//days should be integer
Date.prototype.addDays = function (d) {
    this.setDate(this.getDate() + parseInt(d));
    return this;
}




//validate email address
function ValidateEmail(test) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (test.match(reg)) {
        return true;
    }
    else {

        return false;
    }
}

//allow only alphabates & Space
//allow arrow keys, end and home keys for better UXP.
function ValidateAlphabets(event) {
    var code = (event.keyCode ? event.keyCode : event.which);
    if ((code >= 65 && code <= 90) || (code == 9 || code == 46 || code == 8 || code == 32 || code == 35 || code == 36 || code == 37 || code == 39)) {
        return true;
    }
    return false;
}

//allow only digits
//allow arrow keys, end and home keys for better UXP.
function NumericOnly(event) {
    var code = (event.keyCode ? event.keyCode : event.which);
    if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105) || (code == 9 || code == 46 || code == 8 || code == 35 || code == 36 || code == 37 || code == 39)) {
        return true;
    }
    return false;
}

//allow only alphanumeric characters
//allow arrow keys, end and home keys for better UXP.
function AlphaNumericOnly(event) {
    var code = (event.keyCode ? event.keyCode : event.which);
    if ((code >= 65 && code <= 90) || (code == 9 || code == 46 || code == 8 || code == 35 || code == 36 || code == 37 || code == 39) || (code >= 48 && code <= 57) || (code >= 96 && code <= 105)) {
        return true;
    }
    return false;
}

/*------------------------------------------------*/

//check if passed value contains only positive numbers 0 to 9 with space in between
function IsNumericWithSpace(val) {
    var reg = /^[0-9\s]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }        
}

//check if passed value contains only positive numbers 0 to 9 without space in between
function IsNumericWithoutSpace(val) {
    var reg = /^[0-9]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }    
}

//check if passed value contains only alphabets (case insensitive) with space in between
function IsAlphabetWithSpace(val) {
    var reg = /^[a-zA-Z\s]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }    
}

//check if passed value contains only alphabets (case insensitive) without space in between
function IsAlphabetWithoutSpace(val) {
    var reg = /^[a-zA-Z]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }
}

//check if passed value contains only alpha numeric characters with space in between
function IsAlphaNumericWithSpace(val) {
    var reg = /^[a-zA-Z0-9\s]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }
}

//added by Paul on 08/Sep/2011 to check if passed value contains only alpha numeric characters without space in between
function IsAlphaNumericWithoutSpace(val) {
    var reg = /^[a-zA-Z0-9]+$/;
    if (val.match(reg)) {
        return true;
    }
    else {
        return false;
    }
}

