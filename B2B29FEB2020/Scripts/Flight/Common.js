
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
function GetTimeSpan(strTime) {
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
Date.prototype.addHours = function(h) {
    this.setHours(this.getHours() + parseInt(h));
    return this;
}

//Add minutes to a date
//minutes should be integer
Date.prototype.addMinutes = function(m) {
    this.setMinutes(this.getMinutes() + parseInt(m));
    return this;
}

//Add days to a date
//days should be integer
Date.prototype.addDays = function(d) {
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
//allow only character
function isCharKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 65 && charCode <= 122 || charCode == 32 || charCode == 08) {
        return true;
    }
    else {

        return false;
    }
}
//allow only digits
//allow arrow keys, end and home keys for better UXP.
function NumericOnly(event) {
    var charCode = (event.keyCode ? event.keyCode : event.which);
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;

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


// **************************** BLOCK AIRLINE START ************************ //

// ON INSERT CLICK.............
$(function() {
    $("#ctl00_ContentPlaceHolder1_AirlineBlock1_Insert").click(function() {

        if ($("#txtAirline").val() == '') {
            $("#txtAirline").focus();
            alert('Please Enter AirlineName/Code')
            return false;
        }
    });
});

// ON SEARCH CLICK...................

$(function() {
    $("#ctl00_ContentPlaceHolder1_AirlineBlock1_btnSearch").click(function() {

        if ($("#txtAirline").val() == '') {
            $("#txtAirline").focus();
            alert('Please Enter AirlineName/Code')
            return false;
        }
    });
});



// **************************** BLOCK AIRLINE END ************************ //


// **************************** BLOCK FLIGHT START ************************ //
// ON INSERT CLICK.............
$(function() {
$("#ctl00_ContentPlaceHolder1_BlockFlight_Insert").click(function() {

if ($("#ctl00_ContentPlaceHolder1_BlockFlight_TR_Bok_Fltno").val() == '') {
    $("#ctl00_ContentPlaceHolder1_BlockFlight_TR_Bok_Fltno").focus();
            alert('Please Enter FlightNo')
            return false;
        }

    });
});


// **************************** BLOCK FLIGHT END ************************ //

// **************************** AIRLINE MARKUP START ************************ //


$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_btnSubmit").click(function() {
        if ($("#ctl00_ContentPlaceHolder1_ctl00_ddlMarkupOn option:selected").text() == '-Select-') {
            alert('Please select Markup on');
            document.getElementById('ctl00_ContentPlaceHolder1_ctl00_ddlMarkupOn').focus();
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_ddlShow option:selected").text() == '-Select-') {
            alert('Please select Show');
            document.getElementById('ctl00_ContentPlaceHolder1_ctl00_ddlShow').focus();
            return false;
        }
        if (document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue').value == '') {
            alert('Please Enter Markup value');
            document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue').focus();
            return false;
        }

    });
});



// **************************** AIRLINE MARKUP END ************************ //


// **************************** USER PROFILE START ************************ //

$(function() {
    $("#ctl00_ContentPlaceHolder1_UserProfile1_btnUpdate").click(function() {

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var email = document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txtEmail').value;
        if (reg.test(email) == false) {
            alert('Please provide correct Email', ' ');
            document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txtEmail').focus();
            return false;
        }

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var email = document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txtAltEmailId').value;
        if (reg.test(email) == false) {
            alert('Please provide correct Alternate Email', ' ');
            document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txtAltEmailId').focus();
            return false;
        }

        var mobile = $("#ctl00_ContentPlaceHolder1_UserProfile1_txtMob").val().length;
        if (mobile < 10) {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtMob").focus();
            alert('Please Enter Valid MobileNo')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtLandline").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtLandline").focus();
            alert('Please Enter Landline')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtPanNo").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtPanNo").focus();
            alert('Please Enter Pan CardNo')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtFax").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtFax").focus();
            alert('Please Enter FaxNo')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtAddress").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtAddress").focus();
            alert('Please Enter Address')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtCity").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtCity").focus();
            alert('Please Enter City')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtState").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtState").focus();
            alert('Please Enter State')
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txtCountry").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txtCountry").focus();
            alert('Please Enter Country')
            return false;

        }



    });
});






// **************************** USER PROFILE END ************************ //


// Airline PLB Validation rakesh


//$(function() {


//    $("#ctl00_ContentPlaceHolder1_AirlinePlb1_btnSubmit").click(function() {


//       if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_DistrId").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_DistrId").focus();
//            alert('Please Enter Distributor ID')
//            return false;
//        }



//        if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TN_RBD").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TN_RBD").focus();
//            alert('Please Enter RBD')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TN_Sector").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TN_Sector").focus();
//            alert('Please Enter Sector')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").focus();
//            alert('Please Enter PLB On Basic')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").focus();
//            alert('Please Enter PLB on Basic YQ')
//            return false;
//        }





//    });
//});

// Air line PLB decimal After Input..... with Content Place holder

//plbonbasic

//$(function() {
//    $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").change(function(e) {
//        var values = $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic').val(str3)

//            }

//        }

//    });
//});

//end

//plb basic yq


//$(function() {
//    $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").change(function(e) {
//        var values = $("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ').val(str3)

//            }

//        }

//    });
//});

//plb basic yq end...


// Airline  PLB Validation end




// Air line PLB validation on  user Control page Rakesh 17/08/12



//$(function() {


//$("#ctl00_ContentPlaceHolder1_UC_btnSubmit").click(function() {


//// modification Grouptype DDl validation 17/08/12 Rakesh

//if ($('#ctl00_ContentPlaceHolder1_UC_DDLN_GroupType').val() == "") {

//    $("#ctl00_ContentPlaceHolder1_UC_DDLN_GroupType").focus(); 

//            alert("Please select GroupType");
//           
//            return false;
//        }
//  

//if ($("#TR_DistrIdPLB").val() == '') {
//    $("#TR_DistrIdPLB").focus();
//            alert('Please Enter Distributor ID')
//            return false;
//        }



//        if ($("#ctl00_ContentPlaceHolder1_UC_TN_RBD").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_UC_TN_RBD").focus();
//            alert('Please Enter RBD')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_UC_TN_Sector").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_UC_TN_Sector").focus();
//            alert('Please Enter Sector')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic").focus();
//            alert('Please Enter PLB On Basic')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ").focus();
//            alert('Please Enter PLB on Basic YQ')
//            return false;
//        }





//    });
//});

//// Air line PLB decimal After Input.....user control Page

//plbonbasic

//$(function() {
//$("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasic').val(str3)

//            }

//        }

//    });
//});

//end

//plb basic yq


//$(function() {
//$("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_UC_TR_PlbOnBasicYQ').val(str3)

//            }

//        }

//    });
//});











//Airline Commission validation  


//$(function() {


//    $("#ctl00_ContentPlaceHolder1_AirLineCommision1_ALC_Insert").click(function() {


//        if ($("#txtDistIDAC").val() == '') {
//            $("txtDistIDAC").focus();
//            alert('Please Enter Distributor ID')
//            return false;
//        }


//        if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").focus();
//            alert('Please Enter BASIC Amount')
//            return false;
//        }


//        if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").focus();
//            alert('Please Enter YQ Amount')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").focus();
//            alert('Please Enter BasicYQ Amount')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_CBACK").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_CBACK").focus();
//            alert('Please Enter CashBackAmount')
//            return false;
//        }
//    });
//});


// validation end Airline Commission validation  


// Airline Commission Decimal After Input.... 


//$(function acb() {
//$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic').val(str3)

//            }

//        }

//    });
//});







//// Airline Commission yq


//$(function acyq() {
//$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq').val(str3)

//            }

//        }

//    });
//});




/// basicyq Airline Commission 

//$(function acbyq() {
//$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ').val(str3)

//            }

//        }

//    });
//});






// new airline Commisssion  06/08/2012 rakesh.....



//$(function() {


//$("#ctl00_ContentPlaceHolder1_ACNEW1_ALC_Insert").click(function() {





//if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic").val() == '') {
//    $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic").focus();
//            alert('Please Enter BASIC Amount')
//            return false;
//        }


//        if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq").focus();
//            alert('Please Enter YQ Amount')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ").focus();
//            alert('Please Enter BasicYQ Amount')
//            return false;
//        }

//        if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_CBACK").val() == '') {
//            $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_CBACK").focus();
//            alert('Please Enter CashBackAmount')
//            return false;
//        }
//    });
//});



// NEW airline commission decimal after input 06.08.2012 rakesh......


//$(function () {
//$("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Basic').val(str3)

//            }

//        }

//    });
//});



//$(function () {
//$("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_Yq').val(str3)

//            }

//        }

//    });
//});





//$(function () {
//$("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ").change(function(e) {
//var values = $("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_ACNEW1_TR_Alc_BYQ').val(str3)

//            }

//        }

//    });
//});



//  Decimal After Input AIR Line Charge contentplacer holder used  svtax
//$(function() {
//    $("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax").change(function(e) {
//        var values = $("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax').val(str3)

//            }

//        }

//    });
//});


//// Tfee after decimal

//$(function Tfee() {
//    $("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee").change(function(e) {
//        var values = $("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee").val();

//        if (values.indexOf('.') == -1) {
//            if ($("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee").val().length > 2) {
//                str = values.substring(0, 2);
//                str2 = values.substring(2, values.length);
//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee').val(str3)
//            }
//        }
//        else {
//            if (values.indexOf('.') != 1) {
//                str = values.substring(0, 2);

//                var str1 = values.indexOf('.') + 1;
//                str2 = values.substring(str1, values.length);

//                str3 = str + "." + str2;
//                $('#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee').val(str3)
//            }

//        }

//    });
//});









// Airline Commission validation end
// ***************** NUMBER ONLY BY JQUERY ***************//

jQuery.fn.ForceNumericOnly = function() {
    return this.each(function() {
        $(this).keydown(function(e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};









// ***************  MODIFICATION RAKESH  24/08/2012 *****************

// NEW AIRLINE COMMISSION  validation  NEW Layout 24/08/2012 Rakesh



$(function() {


    $("#ctl00_ContentPlaceHolder1_ctl00_ALC_Insert").click(function() {



        if ($('#ctl00_ContentPlaceHolder1_ctl00_DDLR_ALC_Gtype').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_DDLR_ALC_Gtype").focus();

            alert("Please select GroupType");

            return false;
        }



        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic").focus();
            alert('Please Enter BASIC Amount')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq").focus();
            alert('Please Enter YQ Amount')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ").focus();
            alert('Please Enter BasicYQ Amount')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_CBACK").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_CBACK").focus();
            alert('Please Enter CashBackAmount')
            return false;
        }


        if ($("#TxtStDateAC").val() == '') {
            $("#TxtStDateAC").focus();
            alert('Please Enter Start Date')
            return false;
        }

        if ($("#TxtEndDateAC").val() == '') {
            $("#TxtEndDateAC").focus();
            alert('Please Enter End Date')
            return false;
        }


    });
});


// NEW DECIMAL AFTER INPUT AIRLINECOMMISSION NEW LAYOUT 24/08/2012


$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Basic').val(str3)

            }

        }

    });
});

// AC YQ

$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_Yq').val(str3)

            }

        }

    });
});


$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_Alc_BYQ').val(str3)

            }

        }

    });
});


// END DECIMAL AFTER INPUT AIRLINE COMMISSION 24.08.12

// -------------------AIRLINE PLB VALIDATION 24/08/2012--------------------



$(function() {


    $("#ctl00_ContentPlaceHolder1_ctl00_btnSubmit").click(function() {

        if ($('#ctl00_ContentPlaceHolder1_ctl00_DDLN_GroupType').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_DDLN_GroupType").focus();

            alert("Please select GroupType");

            return false;
        }





        if ($("#ctl00_ContentPlaceHolder1_ctl00_TN_RBD").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TN_RBD").focus();
            alert('Please Enter RBD')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TN_Sector").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TN_Sector").focus();
            alert('Please Enter Sector')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic").focus();
            alert('Please Enter PLB On Basic')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ").focus();
            alert('Please Enter PLB on Basic YQ')
            return false;
        }


        if ($("#TxtStDate").val() == '') {
            $("#TxtStDate").focus();
            alert('Please Enter Start Date')
            return false;
        }



        if ($("#TxtEndDate").val() == '') {
            $("#TxtEndDate").focus();
            alert('Please Enter End Date')
            return false;
        }



    });
});


//AIRLINE PLB DECIMAL AFTER INPUT 24/08/2012




$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasic').val(str3)

            }

        }

    });
});

//end

//plb basic yq


$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_PlbOnBasicYQ').val(str3)

            }

        }

    });
});




// AIRLINEPLB VALIDATION END 25/08/2012


// AIRLINE CHARGES VALIDATION  25/08/2012

$(function() {


    $("#ctl00_ContentPlaceHolder1_ctl00_ALCHG_Insert").click(function() {


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax").focus();
            alert('Please Enter Service Tax ')
            return false;
        }




        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee").focus();
            alert('Please Enter TransactionFee')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Icom").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Icom").focus();
            alert('Please Enter IATA Commission')
            return false;
        }





    });
});



// Airline Charges Decimal After Input 25/08/2012

$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Svtax').val(str3)

            }

        }

    });
});



$(function Tfee() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee").change(function(e) {
        var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee").val();

        if (values.indexOf('.') == -1) {
            if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee").val().length > 2) {
                str = values.substring(0, 2);
                str2 = values.substring(2, values.length);
                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee').val(str3)
            }
        }
        else {
            if (values.indexOf('.') != 1) {
                str = values.substring(0, 2);

                var str1 = values.indexOf('.') + 1;
                str2 = values.substring(str1, values.length);

                str3 = str + "." + str2;
                $('#ctl00_ContentPlaceHolder1_ctl00_TR_ALCHG_Tfee').val(str3)
            }

        }

    });
});

///.................. AIRLINE CHARGES Validation End 25/08/2012....................RAKESH










// air charge numeric only
$("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Svtax").ForceNumericOnly();

$("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Tfee").ForceNumericOnly();

$("#ctl00_ContentPlaceHolder1_AirLineCharges1_TR_ALCHG_Icom").ForceNumericOnly();



$("#ctl00_ContentPlaceHolder1_ALCHG_TR_ALCHG_Svtax").ForceNumericOnly();

$("#ctl00_ContentPlaceHolder1_ALCHG_TR_ALCHG_Tfee").ForceNumericOnly();

$("#ctl00_ContentPlaceHolder1_ALCHG_TR_ALCHG_Icom").ForceNumericOnly();






//airline commission numeric only
$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Basic").ForceNumericOnly();
$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_Yq").ForceNumericOnly();
$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_BYQ").ForceNumericOnly();
$("#ctl00_ContentPlaceHolder1_AirLineCommision1_TR_Alc_CBACK").ForceNumericOnly();


// airline PLB numeric only

$("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasic").ForceNumericOnly();
$("#ctl00_ContentPlaceHolder1_AirlinePlb1_TR_PlbOnBasicYQ").ForceNumericOnly();






// ******************** AIRLINE MARKUP DIST START *****************************//

$(function() {

    $("input[name$='mrkupType']").click(function() {

        var radio_value = $(this).val();

        if (radio_value == 'Fixed') {

           
            $("#rdbFixed").show("slow");
            $("#no_box").hide();
            $('#ctl00_ContentPlaceHolder1_ctl00_lblMrkAmt').val("Markup Amount");
            $('#mrktype').val("F");
        }
        else if (radio_value == 'Percentage') {
        
            $("#rdbPercentage").show("slow");
            $('#ctl00_ContentPlaceHolder1_ctl00_lblMrkAmt').val("Markup Percent");
            $('#mrktype').val("P");
            $("#yes_box").hide();
        }
    });
});

window.onload = $('#ctl00_ContentPlaceHolder1_ctl00_lblMrkAmt').val("Markup Amount");







$(function acb() {
    $("#ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue").change(function(e) {
        if ($("#mrktype").val() == "P") {
            var values = $("#ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue").val();
            if (values.indexOf('.') == -1) {
                if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue").val().length > 2) {
                    str = values.substring(0, 2);
                    str2 = values.substring(2, values.length);
                    str3 = str + "." + str2;
                    $('#ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue').val(str3)
                }
            }
            else {
                if (values.indexOf('.') != 1) {
                    str = values.substring(0, 2);

                    var str1 = values.indexOf('.') + 1;
                    str2 = values.substring(str1, values.length);

                    str3 = str + "." + str2;
                    $('#ctl00_ContentPlaceHolder1_ctl00_TR_MarkUpValue').val(str3)
                }
            }
        }
    });
});

// ******************** AIRLINE MARKUP DIST END *****************************//

// ********************* UPDATE AGENT DETAILS START ******************* //
$(function() {
    $("#btn_update").click(function() {

        if ($("#txt_Address").val() == '') {
            $("txt_Address").focus();
            alert('Please Enter address')
            return false;
        }

        if ($("#txt_City").val() == '') {
            $("txt_City").focus();
            alert('Please Enter city')
            return false;
        }
        if ($("#txt_State").val() == '') {
            $("txt_State").focus();
            alert('Please Enter state')
            return false;
        }

        if ($("#txt_zip").val() == '') {
            $("txt_zip").focus();
            alert('Please Enter zip')
            return false;
        }
        if ($("#txt_Country").val() == '') {
            $("txt_Country").focus();
            alert('Please Enter country')
            return false;
        }

        if ($("#txt_zip").val() == '') {
            $("txt_zip").focus();
            alert('Please Enter zip')
            return false;
        }
        if ($("#txt_Fname").val() == '') {
            $("txt_Fname").focus();
            alert('Please Enter first name')
            return false;
        }

        if ($("#txt_Lname").val() == '') {
            $("txt_Lname").focus();
            alert('Please Enter last name')
            return false;
        }

        if ($("#txt_Mobile").val() == '') {
            $("txt_Mobile").focus();
            alert('Please Enter mobile')
            return false;
        }

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var email = document.getElementById('txt_Email').value;
        if (reg.test(email) == false) {
            alert('Please provide valid Emailid');
            document.getElementById('txt_Email').focus();
            return false;
        }
        if ($("#txt_Fax").val() == '') {
            $("txt_Fax").focus();
            alert('Please Enter fax no')
            return false;
        }
        if ($("#txt_Pan").val() == '') {
            $("txt_Pan").focus();
            alert('Please Enter pan no')
            return false;
        }


    });
})



// ********************* UPDATE AGENT DETAILS END ******************* //


// Check password






$(function() {
    $("#ctl00_ContentPlaceHolder1_UserProfile1_btn_Save").click(function() {

        if ($("#ctl00_ContentPlaceHolder1_UserProfile1_txt_password").val() != $("#ctl00_ContentPlaceHolder1_UserProfile1_txt_cpassword").val()) {
            $("#ctl00_ContentPlaceHolder1_UserProfile1_txt_cpassword").focus();
            alert('Password Not Match')
            return false;
        }
    });
});




//password  match javascript


function confirmPW() {
    var pw = document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txt_password').value;
    var pwconfirm = document.getElementById('ctl00_ContentPlaceHolder1_UserProfile1_txt_cpassword').value;
    if (pw != pwconfirm) {
        alert("Your confirmation password does not match your desired password. Please Try again")
        pw.focus();
    }
}














// ***************** NUMBER ONLY BY JQUERY ***************//

jQuery.fn.ForceNumericOnly = function() {
    return this.each(function() {
        $(this).keydown(function(e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};


// Register Agent Validation 27/08/2012 RAKESH



$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_Submit").click(function() {


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Fname").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Fname").focus();
            alert('Please Enter First Name')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Lname").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Lname").focus();
            alert('Please Enter Last Name')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_DOB").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_DOB").focus();
            alert('Please Enter DOB')
            return false;
        }



        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Addr").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Addr").focus();
            alert('Please Enter address')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_City").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_City").focus();
            alert('Please Enter city')
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Stat").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Stat").focus();
            alert('Please Enter state')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Count").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Count").focus();
            alert('Please Enter Country')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_zip").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_zip").focus();
            alert('Please Enter ZipCode')
            return false;
        }






        var Agmobile = $("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Mob").val().length;
        if (Agmobile < 10) {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Mob").focus();
            alert('Please Enter 10 Digit MobileNo')
            return false;

        }



        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var AGemail = document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Email').value;
        if (reg.test(AGemail) == false) {
            alert('Please provide valid Emailid');
            document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Email').focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_AgncyName").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_AgncyName").focus();
            alert('Please Enter Agency Name')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TN_Agt_Website").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TN_Agt_Website").focus();
            alert('Please Enter Website Address')
            return false;
        }


        //        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pan").val() == '') {
        //            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pan").focus();
        //            alert('Please Enter pan no')
        //            return false;
        //        }


        var regex1 = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var rapanno = document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pan').value;

        if (regex1.test(rapanno) == false) {
            alert('Please enter valid Pan Number');

            return false;
        }




        if ($('#ctl00_ContentPlaceHolder1_ctl00_DDLR_Agt_SecQ').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_DDLR_Agt_SecQ").focus();

            alert("Please select Security Question");

            return false;
        }



        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Ans").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Ans").focus();
            alert('Please Enter your Secret Answer')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pswd").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pswd").focus();
            alert('Please Enter Password')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Cnfm_Pswd").val() == '') {
            $("ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Cnfm_Pswd").focus();
            alert('Please Enter Confirmed Password')
            return false;
        }




        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Pswd").val() != $("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Cnfm_Pswd").val()) {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Agt_Cnfm_Pswd").focus();
            alert('Please Enter confirm password same as Password');
            return false;
        }

    });
});


// Regiter Agent End





// Register Staff Start  27/08/2012 Rakesh //


$(function() {
    $("#ctl00_ContentPlaceHolder1_ctl00_btnRegister").click(function() {

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var RSemail = document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_UserId').value;
        if (reg.test(RSemail) == false) {
            alert('Please provide UserId as your Emailid');
            document.getElementById('ctl00_ContentPlaceHolder1_ctl00_TR_UserId').focus();
            return false;
        }
        var Password = $("#ctl00_ContentPlaceHolder1_ctl00_TR_Password").val().length;
        if (Password < 8) {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Password").focus();
            alert('Please Enter min 8 digit password');
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Password").val() != $("#ctl00_ContentPlaceHolder1_ctl00_TR_Confirm_Password").val()) {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Confirm_Password").focus();
            alert('Please Enter confirm password same as Password');
            return false;

        }


        if ($('#ctl00_ContentPlaceHolder1_ctl00_ddlUserType').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_ddlUserType").focus();

            alert("Please select UserType");

            return false;
        }





        if ($('#ctl00_ContentPlaceHolder1_ctl00_ddlDepartment').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_ddlDepartment").focus();

            alert("Please Select Department");

            return false;
        }






        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_FirstName").val() == '') {
            $("#ctl00_ContentPlaceHolder1_RegisterStaff1_TR_FirstName").focus();
            alert('Please Enter First Name')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_LastName").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_LastName").focus();
            alert('Please Enter Last Name')
            return false;

        }
        var mobile = $("#ctl00_ContentPlaceHolder1_ctl00_TR_MobileNo").val().length;
        if (mobile < 10) {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_MobileNo").focus();
            alert('Please Enter 10 Digit MobileNo')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_Address").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_Address").focus();
            alert('Please Enter Address')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TN_City").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TN_City").focus();
            alert('Please Enter City')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TN_State").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TN_State").focus();
            alert('Please Enter State')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_ZN_ZipCode").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_ZN_ZipCode").focus();
            alert('Please Enter 6 Digit Zip Code')
            return false;

        }


        if ($('#ctl00_ContentPlaceHolder1_ctl00_ddlStatus').val() == "") {

            $("#ctl00_ContentPlaceHolder1_ctl00_ddlStatus").focus();

            alert("Please Select Status");

            return false;
        }





    });
});





// **************************** Register Staff End ************************ //


// Registration Agent Validation On Login Page 29/08/2012 RAKESH

$(function() {
    $("#ctl00_ContentPlaceHolder1_UCR_Submit").click(function() {


        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Fname").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Fname").focus();
            alert('Please Enter First Name')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Lname").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Lname").focus();
            alert('Please Enter Last Name')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_DOB").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_DOB").focus();
            alert('Please Enter DOB')
            return false;
        }



        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Addr").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Addr").focus();
            alert('Please Enter address')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_City").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_City").focus();
            alert('Please Enter city')
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Stat").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Stat").focus();
            alert('Please Enter state')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Count").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Count").focus();
            alert('Please Enter Country')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_zip").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_zip").focus();
            alert('Please Enter ZipCode')
            return false;
        }






        var Agmobile = $("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Mob").val().length;
        if (Agmobile < 10) {
            $("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Mob").focus();
            alert('Please Enter 10 Digit MobileNo')
            return false;

        }



        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var AGemail = document.getElementById('ctl00_ContentPlaceHolder1_UCR_TR_Agt_Email').value;
        if (reg.test(AGemail) == false) {
            alert('Please provide valid Emailid');
            document.getElementById('ctl00_ContentPlaceHolder1_UCR_TR_Agt_Email').focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_AgncyName").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_AgncyName").focus();
            alert('Please Enter Agency Name')
            return false;
        }

        //        if ($("#ctl00_ContentPlaceHolder1_UCR_TN_Agt_Website").val() == '') {
        //            $("ctl00_ContentPlaceHolder1_UCR_TN_Agt_Website").focus();
        //            alert('Please Enter Website Address')
        //            return false;
        //        }



        var regex1 = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;

        var rapan = document.getElementById('ctl00_ContentPlaceHolder1_UCR_TR_Agt_Pan').value;

        if (regex1.test(rapan) == false) {
            alert('Please enter valid Pan Number');

            return false;
        }




        if ($('#ctl00_ContentPlaceHolder1_UCR_DDLR_Agt_SecQ').val() == "") {

            $("#ctl00_ContentPlaceHolder1_UCR_DDLR_Agt_SecQ").focus();

            alert("Please select Security Question");

            return false;
        }



        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Ans").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Ans").focus();
            alert('Please Enter your Secret Answer')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Pswd").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Pswd").focus();
            alert('Please Enter Password')
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Cnfm_Pswd").val() == '') {
            $("ctl00_ContentPlaceHolder1_UCR_TR_Agt_Cnfm_Pswd").focus();
            alert('Please Enter Confirmed Password')
            return false;
        }




        if ($("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Pswd").val() != $("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Cnfm_Pswd").val()) {
            $("#ctl00_ContentPlaceHolder1_UCR_TR_Agt_Cnfm_Pswd").focus();
            alert('Please Enter confirm password same as Password');
            return false;
        }

    });
});


// Regiter Agent End




// Register Staff Management Link  03/09/2012 Rakesh



function PopupDiv() {

    // When site loaded, load the Popupbox First
    loadPopupBox();

    $('#popupBoxClose').click(function() {
        unloadPopupBox();
    });

    $('#container').click(function() {
        unloadPopupBox();
    });

    function unloadPopupBox() {    // TO Unload the Popupbox
        $('#popup_box').fadeOut("slow");
        $("#container").css({ // this is just for style       
            "opacity": "1"
        });
    }

    function loadPopupBox() {    // To Load the Popupbox
        $('#popup_box').fadeIn("slow");
        $("#container").css({ // this is just for style
            "opacity": "0.3"
        });
    }
}

function PopupDiv1() {

    // When site loaded, load the Popupbox First
    loadPopupBox();

    $('#popupBoxClose1').click(function() {
        unloadPopupBox();
    });

    $('#container1').click(function() {
        unloadPopupBox();
    });

    function unloadPopupBox() {    // TO Unload the Popupbox
        $('#popup_box1').fadeOut("slow");
        $("#container1").css({ // this is just for style       
            "opacity": "1"
        });
    }

    function loadPopupBox() {    // To Load the Popupbox
        $('#popup_box1').fadeIn("slow");
        $("#container1").css({ // this is just for style
            "opacity": "0.3"
        });
    }
}

$(function() {
    $("#ctl00_ContentPlaceHolder1_UC_btnRegister").click(function() {

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var RSemail = document.getElementById('ctl00_ContentPlaceHolder1_UC_TR_UserId').value;
        if (reg.test(RSemail) == false) {
            alert('Please provide UserId as your Emailid');
            document.getElementById('ctl00_ContentPlaceHolder1_UC_TR_UserId').focus();
            return false;
        }




        if ($("#ctl00_ContentPlaceHolder1_UC_TR_FirstName").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_TR_FirstName").focus();
            alert('Please Enter First Name')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UC_TR_LastName").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_TR_LastName").focus();
            alert('Please Enter Last Name')
            return false;

        }
        var mobile = $("#ctl00_ContentPlaceHolder1_UC_TR_MobileNo").val().length;
        if (mobile < 10) {
            $("#ctl00_ContentPlaceHolder1_UC_TR_MobileNo").focus();
            alert('Please Enter 10 Digit MobileNo')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UC_TR_Address").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_TR_Address").focus();
            alert('Please Enter Address')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UC_TN_City").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_TN_City").focus();
            alert('Please Enter City')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UC_TN_State").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_TN_State").focus();
            alert('Please Enter State')
            return false;

        }
        if ($("#ctl00_ContentPlaceHolder1_UC_ZN_ZipCode").val() == '') {
            $("#ctl00_ContentPlaceHolder1_UC_ZN_ZipCode").focus();
            alert('Please Enter 6 Digit Zip Code')
            return false;

        }



        var Password = $("#ctl00_ContentPlaceHolder1_UC_TR_Password").val().length;
        if (Password < 8) {
            $("#ctl00_ContentPlaceHolder1_UC_TR_Password").focus();
            alert('Please Enter min 8 digit password');
            return false;

        }

        if ($("#ctl00_ContentPlaceHolder1_UC_TR_Password").val() != $("#ctl00_ContentPlaceHolder1_UC_TR_Confirm_Password").val()) {
            $("#ctl00_ContentPlaceHolder1_UC_TR_Confirm_Password").focus();
            alert('Please Enter confirm password same as Password');
            return false;

        }


        if ($('#ctl00_ContentPlaceHolder1_UC_ddlUserType').val() == "") {

            $("#ctl00_ContentPlaceHolder1_UC_ddlUserType").focus();

            alert("Please select UserType");

            return false;
        }





        if ($('#ctl00_ContentPlaceHolder1_UC_ddlDepartment').val() == "") {

            $("#ctl00_ContentPlaceHolder1_UC_ddlDepartment").focus();

            alert("Please Select Department");

            return false;
        }


        if ($('#ctl00_ContentPlaceHolder1_UC_ddlStatus').val() == "") {

            $("#ctl00_ContentPlaceHolder1_UC_ddlStatus").focus();

            alert("Please Select Status");

            return false;
        }





    });
});
//


$(function() {
$("#ctl00_ContentPlaceHolder1_ctl00_ALCHG_Insert").click(function() {

if ($("#txtAirlineairchrg").val() == '') {
    $("#txtAirlineairchrg").focus();
            alert('Please Enter AirlineName/Code')
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_TR_SRVCHARGE").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_TR_SRVCHARGE").focus();
            alert('Please Enter ServiceCharge')
            return false;
        }

    });
});

//Validate Reisue and Remark
function ReissuRefundRemark() {
    if (document.getElementById("txtRemarks").value == "") {
        alert('Please Enter Remark');
        document.getElementById("txtRemarks").focus();
        return false;
    }

}
//
function Show(obj) {
    if (obj.checked) {
        //        document.getElementById("td_ret").style.display = "none";
        $("#div1").show();

    }
}
function Hide(obj) {
    if (obj.checked) {
        //        document.getElementById("td_ret").style.display = "block";
        $("#div1").hide();

    }
}


//$(function() {
//    $("#ctl00_ContentPlaceHolder1_ctl00_btnupdatestfp").click(function() {


//    var Password = $("#ctl00_ContentPlaceHolder1_ctl00_txtPass").val().length;
//        if (Password < 8) {
//            $("#ctl00_ContentPlaceHolder1_ctl00_txtPass").focus();
//            alert('Please Enter min 8 digit password');
//            return false;

//        }

//        if ($("#ctl00_ContentPlaceHolder1_ctl00_txtPass").val() != $("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").val()) {
//            $("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").focus();
//            alert('Please Enter confirm password same as Password');
//            return false;

//        }
//        

//    });
//});




//  Password Match Staff Profile



$(function() {
$("#ctl00_ContentPlaceHolder1_ctl00_btnupdatestfp").click(function() {

if ($("#ctl00_ContentPlaceHolder1_ctl00_txtPass").val() == '') {
    $("#ctl00_ContentPlaceHolder1_ctl00_txtPass").focus();
            alert('Please Enter 8 digit Password')
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").val() == '') {
            $("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").focus();
            alert('Please Enter Confirm Password')
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl00_txtPass").val() != $("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").val()) {
            $("#ctl00_ContentPlaceHolder1_ctl00_txtConfirmPass").focus();
            alert('Please Enter confirm password same as Password');
            return false;

        }
        
        

    });
});
