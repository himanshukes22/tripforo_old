//----------------Validate for Login Page----------------
function ValidateLogin() {
   if (document.getElementById('selAppType').value == 0) {
        alert("Select Type");
        return false;
    }
    if (document.getElementById('User_Email').value == '') {
       // alert("Enter User Name");
        Errormsg("User_Email");
        return false;
    }
    if (document.getElementById('Password').value == '') {
       // alert("Enter Password");
        Errormsg("Password");
        return false;
    }
    return true;
}
//----------------End Validate for Login Page----------------

//----------------Validate on Submit Button for Regisation Page----------------
function ValidateRegistration() {
    if (document.getElementById('txtfname').value == '') {
        Errormsg("txtfname");
        //alert("Enter First Name");
        return false;
    }
    else
        document.getElementById('txtfname').style.borderColor = "";
    if (document.getElementById('txtlname').value == '') {
        Errormsg("txtlname");
        //alert("Enter Last Name");
        return false;
    }
    else
        document.getElementById('txtlname').style.borderColor = "";
    if ($("#chkTrains").is(':checked')) {
        if (document.getElementById('txtDob').value == '') {
            Errormsg("txtDob");
            alert("Kindly select Date of Birth");
            return false;
        }
        else
            document.getElementById('txtDob').style.borderColor = "";
    }
    if (document.getElementById('txtagencyName').value == '') {
        Errormsg("txtagencyName");
       // alert("Enter Agency Name");
        return false;
    }
    else
        document.getElementById('txtagencyName').style.borderColor = "";
    if (document.getElementById('txtAddress').value == '') {
        Errormsg("txtAddress");
        //alert("Enter Address");
        return false;
    }
    else
        document.getElementById('txtAddress').style.borderColor = "";
    if (document.getElementById('txtpincode').value == '') {
        Errormsg("txtpincode");
        // alert("Enter Pincode");
        return false;
    }
    else
        document.getElementById('txtpincode').style.borderColor = "";
    if (isNaN(document.getElementById('txtpincode').value)) {
        Errormsg("txtpincode");
        alert("Please Enter valid Pincode");
        return false;
    }
    else
        document.getElementById('txtpincode').style.borderColor = "";
    if ($("#chkTrains").is(':checked')) {
        if (document.getElementById('txtPostOfcName').value == '') {
            Errormsg("txtPostOfcName");
            alert("Kindly fill Post Office");
            return false;
        }
        else
            document.getElementById('txtPostOfcName').style.borderColor = "";

        if (document.getElementById('txtOfcPh').value == '') {
            Errormsg("txtOfcPh");
            alert("Kindly fill Office Phone Number");
            return false;
        }
        else
            document.getElementById('txtOfcPh').style.borderColor = "";
    }
    if (document.getElementById('txtOfcPh').value != '') {
        if (document.getElementById('txtOfcPh').value.length < 6 || document.getElementById('txtOfcPh').value.length > 13 || isNaN(document.getElementById('txtOfcPh').value)) {
            Errormsg("txtOfcPh");
            alert("Please enter valid Office Phone Number");
            return false;
        }
        else
            document.getElementById('txtOfcPh').style.borderColor = "";
    }
    if ($("#chkTrains").is(':checked')) {
        if (document.getElementById('txtgstNo').value == '') {
            Errormsg("txtgstNo");
            alert("Kindly fill GST Number");
            return false;
        }
        else
            document.getElementById('txtgstNo').style.borderColor = "";

        if (document.getElementById('txtResAddress').value == '') {
            Errormsg("txtResAddress");
            alert("Kindly fill Residential Address");
            return false;
        }
        else
            document.getElementById('txtResAddress').style.borderColor = "";

        if (document.getElementById('DrpResCountry').value == '' || document.getElementById('DrpResCountry').value == 'Country' || document.getElementById('DrpResCountry').value == 'select') {
            alert("Select Residential Country");
            return false;
        }

        if (document.getElementById('DrpResState').value == '' || document.getElementById('DrpResState').value == 'State' || document.getElementById('DrpResState').value == 'select') {
            alert("Select Residential State");
            return false;
        }
        if (document.getElementById('DrpResCity').value == '' || document.getElementById('DrpResCity').value == 'City' || document.getElementById('DrpResCity').value == 'select') {
            alert("Select Residential City");
            return false;
        }
        if (document.getElementById('txtRespincode').value == '') {
            Errormsg("txtRespincode");
            alert("Kindly fill Residential Pincode");
            return false;
        }
        else
            document.getElementById('txtRespincode').style.borderColor = "";
        if (isNaN(document.getElementById('txtRespincode').value)) {
            Errormsg("txtRespincode");
            alert("Please Enter valid Pincode");
            return false;
        }
        else
            document.getElementById('txtRespincode').style.borderColor = "";

        if (document.getElementById('txtResOfcName').value == '') {
            Errormsg("txtResOfcName");
            alert("Kindly fill Residential Post Office");
            return false;
        }
        else
            document.getElementById('txtResOfcName').style.borderColor = "";

        if (document.getElementById('txtResPhNo').value == '') {
            Errormsg("txtResPhNo");
            alert("Kindly fill Residential Phone Number");
            return false;
        }
        else
            document.getElementById('txtResPhNo').style.borderColor = "";
        if (document.getElementById('txtResPhNo').value != '') {
            if (document.getElementById('txtResPhNo').value.length < 6 || document.getElementById('txtResPhNo').value.length > 13 || isNaN(document.getElementById('txtResPhNo').value)) {
                Errormsg("txtResPhNo");
                alert("Please enter valid Residential Phone Number");
                return false;
            }
            else
                document.getElementById('txtResPhNo').style.borderColor = "";
        }
    }
    if (document.getElementById('txtpanno').value == '') {
        Errormsg("txtpanno");
        //alert("Enter Pan Number");
        return false;
    }
    else
        document.getElementById('txtpanno').style.borderColor = "";
    if (document.getElementById('txtAdharNo').value == '') {
        Errormsg("txtAdharNo");
        return false;
    }
    else
        document.getElementById('txtAdharNo').style.borderColor = "";
    var url = window.location.host;
    //if (url == "b2b") {
    //    if ($("#chkTrains").is(':checked')) {
    //        if (document.getElementById('txtDigiCertNo').value == '') {
    //            Errormsg("txtDigiCertNo");
    //            return false;
    //        }
    //        else
    //            document.getElementById('txtDigiCertNo').style.borderColor = "";
    //        if (document.getElementById('txtBHM').value == '') {
    //            Errormsg("txtBHM");
    //            return false;
    //        }
    //        else
    //            document.getElementById('txtBHM').style.borderColor = "";
    //        if (document.getElementById('fuDigiCert').value == "") {
    //            alert("Select Digital Certificate");
    //            return false;
    //        }
    //        else
    //            document.getElementById('fuDigiCert').style.borderColor = "";
    //        if (document.getElementById('fuDigiCert').value != "") {
    //            var extension = document.getElementById('fuDigiCert').value.split('.').pop().toLowerCase();
    //            if ($.inArray(extension, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
    //                alert("Please Choose only .jpg, .gif, .png, .jpeg, .bmp extention file");
    //                document.getElementById('fuDigiCert').value = "";
    //                return false;
    //            }
    //        }
    //    }
    //}
    if (document.getElementById('txtmobile').value == '') {
        Errormsg("txtmobile");
        // alert("Enter Mobile Number");
        return false;
    }
    else
        document.getElementById('txtmobile').style.borderColor = "";
    if (document.getElementById('txtmobile').value.length < 10 || document.getElementById('txtmobile').value.length > 10 || isNaN(document.getElementById('txtmobile').value)) {
        Errormsg("txtmobile");
        alert("Please enter 10 digit mobile number");
        return false;
    }
    else
        document.getElementById('txtmobile').style.borderColor = "";
    if (document.getElementById('txtemail').value == '') {
        Errormsg("txtemail");
       // alert("Enter Agency EmailID");
        return false;
    }
    else
        document.getElementById('txtemail').style.borderColor = "";
    var pattern = new RegExp(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$/);
    if (!pattern.test(document.getElementById('txtemail').value)) {
        Errormsg("txtemail");
        alert(" Enter valid Email Id");
        return false;
    }
    else
        document.getElementById('txtemail').style.borderColor = "";
    if (document.getElementById('txtpass').value == '') {
        Errormsg("txtpass");
       // alert("Enter Password");
        return false;
    }
    else
        document.getElementById('txtpass').style.borderColor = "";
    var pwd = document.getElementById("txtpass").value;
    var cnfrmPwd = document.getElementById("txtcnfpass").value;
    if (pwd != cnfrmPwd) {
        Errormsg("txtcnfpass");
        alert("Passwords does not match");
        return false;
    }
    else
        document.getElementById('txtcnfpass').style.borderColor = "";
 
    if (document.getElementById('txtdesignation').value == '') {
        Errormsg("txtdesignation");
       // alert("Enter Designation");
        return false;
    }
    else
        document.getElementById('txtdesignation').style.borderColor = "";
    //if (document.getElementById('txtdob').value == '') {
    //    Errormsg("txtdob");
    //    //alert("Enter Date of Birth");
    //    return false;
    //}
    //else
    //    document.getElementById('txtdob').style.borderColor = "";
    //if (document.getElementById('txttelphn').value != '') {
    //    if (document.getElementById('txttelphn').value.length < 8 || document.getElementById('txttelphn').value.length > 15 || isNaN(document.getElementById('txttelphn').value)) {
    //        Errormsg("txttelphn");
    //        alert("Please Enter 8-15 digit Telephone Number");
    //        return false;
    //    }
    //    else
    //        document.getElementById('txttelphn').style.borderColor = "";
    //}
    return true;
}
function ValidateUpdateProfile() {
    if (document.getElementById('AgntNme').value == '') {
        Errormsg("AgntNme");
        return false;
    }
    else
        document.getElementById('AgntNme').style.borderColor = "";
    if (document.getElementById('fstNme').value == '') {
        Errormsg("fstNme");
        return false;
    }
    else 
        document.getElementById('fstNme').style.borderColor = "";
    if (document.getElementById('LNme').value == '') {
        Errormsg("LNme");
        return false;
    }
    else
        document.getElementById('LNme').style.borderColor = "";
    if (document.getElementById('Desig').value == '') {
        Errormsg("Desig");
        return false;
    }
    else
        document.getElementById('Desig').style.borderColor = "";
    if (document.getElementById('Addr').value == '') {
        Errormsg("Addr");
        return false;
    }
    else
        document.getElementById('Addr').style.borderColor = "";
    var contryId = document.getElementById('CntryNme');
    if (contryId.options[contryId.selectedIndex].text == 'Select') {
        alert("Select Country");
        return false;
    }
    else
        document.getElementById('CntryNme').style.borderColor = "";
    var stateID = document.getElementById('StateNme');
    if (stateID.options[stateID.selectedIndex].text == 'Select') {
        alert("Select State");
        return false;
    }
    else
        document.getElementById('StateNme').style.borderColor = "";
    var cityID = document.getElementById('CiyNme');
    if (cityID.options[cityID.selectedIndex].text == 'Select') {
        alert("Select City");
        return false;
    }
    else
        document.getElementById('CiyNme').style.borderColor = "";
    if (document.getElementById('PinCode').value == '') {
        Errormsg("PinCode");
        return false;
    }
    else
        document.getElementById('PinCode').style.borderColor = "";
    if (document.getElementById('PanNo').value == '') {
        Errormsg("PanNo");
        return false;
    }
    else
        document.getElementById('PanNo').style.borderColor = "";
    if (document.getElementById('file').value != "") {
        var extension = document.getElementById('file').value.split('.').pop().toLowerCase();
        if ($.inArray(extension, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
            alert("Please Choose only .jpg, .gif, .png, .jpeg, .bmp extention file");
            document.getElementById('file').value = "";
            return false;
        }
    }
    return true;
}
function Errormsg(contrlID) {
    document.getElementById(contrlID).focus();
    document.getElementById(contrlID).style.borderColor = "red";
    document.getElementById(contrlID).value = "";
}


//----------------End Validate on Submit Button for Regisation Page----------------

 function PrintDetails() {
     window.print();
 }
 function showdiv(ID) {
     if (document.getElementById(ID).style.display == "none") {
         document.getElementById(ID).style.display = "block";
     }
     else {
         document.getElementById(ID).style.display = "none";
     }
 }
//-----------------Validation On From Date-----------------
 function ValidateFromDate() {
     var fromDate = document.getElementById('txtFromDate').value;
     if (fromDate == '') {
         alert("Please select from date");
         return false;
     }
     return true;
 }
//-----------------End Validation On From Date-----------------

//-----------------Validation On To Date-----------------
 function ValidateToDate() {
     var fromDate = document.getElementById('txtFromDate').value;
     var toDate = document.getElementById('txtToDate').value;
     ValidateFromDate();
     if (toDate == '') {
         alert("Please select to date");
         return false;
     }
     if (fromDate != '' && toDate != '') {
         var fromDateSplitter = fromDate.split("/");
         var toDateSplitter = toDate.split("/");
         var toDateDay = toDateSplitter[0];
         var toDateMonth = toDateSplitter[1];
         var toDateYear = toDateSplitter[2];
         var fromDateDay = fromDateSplitter[0];
         var fromDateMonth = fromDateSplitter[1];
         var fromDateYear = fromDateSplitter[2];
         //var fromDt = fromDateYear + fromDateMonth + fromDateDay;
         //var toDt = toDateYear + toDateMonth + toDateDay;
         //var dtDiff = toDt - fromDt;
         //-------------Date Difference b/w two Dates----------------
         var str1 = fromDate.split('/');
         var str2 = toDate.split('/');
         var t1 = new Date(str1[2], str1[1] - 1, str1[0]); // yyyy,mm,dd
         var t2 = new Date(str2[2], str2[1] - 1, str2[0]);
         var diffMS = t2 - t1; // ms
         var diffS = diffMS / 1000;//Second
         var diffM = diffS / 60;//Minuts
         var diffH = diffM / 60;//Hours
         var diffD = diffH / 24;//Days
         //----------End of Date Difference b/w two Dates-------------
         if (fromDateYear > toDateYear) {
             alert("To date can not be less than From date");
             document.getElementById('txtToDate').value = fromDate;
             return false;
         }
         else if (fromDateYear == toDateYear) {
             if (fromDateMonth > toDateMonth) {
                 alert("To date can not be less than From date");
                 document.getElementById('txtToDate').value = fromDate;
                 return false;
             }
             else if (diffD > 31)
             {
                 alert("You can not search more than 1 month days");
                 document.getElementById('txtToDate').value = fromDate;
                 return false;
             }
             else if (fromDateMonth == toDateMonth) {
                 if (fromDateDay > toDateDay) {
                     alert("To date can not be less than From date");
                     document.getElementById('txtToDate').value = fromDate;
                     return false;
                 }
                 else if (diffD >31)
                 {
                     alert("You can not search more than 1 month days");
                     document.getElementById('txtToDate').value = fromDate;
                     return false;
                 }
             }
             else if (diffD > 31) {
                 alert("You can not search more than 1 month days");
                 document.getElementById('txtToDate').value = fromDate;
                 return false;
             }
         }
         else if (diffD > 31)
         {
             alert("You can not search more than 1 month days");
             document.getElementById('txtToDate').value = fromDate;
             return false;
         }
     }
     return true;
 }
//-----------------End Validation On To Date-----------------

//----------------Upload Document Show Hide function on Plus Minus Icon-------------
 function ShowDynamicDocDivV() {
     $('#divImg').hide();
     $('#divImgmin').hide();
     document.getElementById('divdoc1').style.display = "block";
     //$('#divdoc1').show();
 }
 function ShowDynamicDocDivV1() {
     $('#divImg').hide();
     $('#divImgmin').hide();
     //$('#divdoc1').show();
     document.getElementById('divdoc1').style.display = "block";
 }
 function ShowDynamicDocDivV2() {
     $('#divImg1').hide();
     $('#divImg1min').hide();
     //$('#divdoc2').show();
     document.getElementById('divdoc2').style.display = "block";
 }
 function HideDynamicDocDivV1() {
     $('#divImg').show();
     $('#divImgmin').show();
     //$('#divdoc1').hide();
     document.getElementById('divdoc1').style.display = "none";
 }
 function ShowDynamicDocDivV3() {
     $('#divImg2').hide();
     $('#divImg2min').hide();
    // $('#divdoc3').show();
     document.getElementById('divdoc3').style.display = "block";
 }
 function ShowDynamicDocDivV4() {
     $('#divImg3').hide();
     $('#divImg3min').hide();
     //$('#divdoc4').show();
     document.getElementById('divdoc4').style.display = "block";
 }
 function ShowDynamicDocDivV5() {
     $('#divImg5').hide();
     $('#divImg5min').hide();
     $('#divImg6min').show();
    // $('#divdoc5').show();
     document.getElementById('divdoc5').style.display = "block";
 }

 function HideDynamicDocDivV2() {
     $('#divImg1').show();
     $('#divImg1min').show();
     //$('#divdoc2').hide();
     document.getElementById('divdoc2').style.display = "none";
 }
 function HideDynamicDocDivV3() {
     $('#divImg2').show();
     $('#divImg2min').show();
     //$('#divdoc3').hide();
     document.getElementById('divdoc3').style.display = "none";
 }
 function HideDynamicDocDivV4() {
     $('#divImg3').show();
     $('#divImg3min').show();
     //$('#divdoc4').hide();
     document.getElementById('divdoc4').style.display = "none";
 }
 function HideDynamicDocDivV5() {
     $('#divImg6min').hide();
     //$('#divdoc5').hide();
     $('#divImg5').show();
     $('#divImg5min').show();
     document.getElementById('divdoc5').style.display = "none";
 }
//----------------End Upload Document Show Hide function on Plus Minus Icon-------------

 function ValidateDocuments() {
     if (document.getElementById('DocDesc').value == '') {
         alert("Enter Document Description");
         return false;
     }
     else if (document.getElementById('FileUploadDoc').value == "") {
         alert("Please Choose File");
         return false;
     }
     else if (document.getElementById('FileUploadDoc').value != "") {
         if (checkExtension('FileUploadDoc')==false)
             return false;
     }
     if (document.getElementById('divdoc1').style.display == "") {
         if (document.getElementById('DocDesc1').value == '') {
             alert("Enter Document Description");
             return false;
         }
         else if (document.getElementById('FileUpload1').value == "") {
             alert("Please Choose File");
             return false;
         }
         else if (document.getElementById('FileUpload1').value != "") {
             if (checkExtension('FileUpload1'))
                 return false;
         }
     }
     if (document.getElementById('divdoc2').style.display == "") {
         if (document.getElementById('DocDesc2').value == '') {
             alert("Enter Document Description");
             return false;
         }
         else if (document.getElementById('FileUpload2').value == "") {
             alert("Please Choose File");
             return false;
         }
         else if (document.getElementById('FileUpload2').value != "") {
             if(checkExtension('FileUpload2'))
                 return false;
         }
     }
     if (document.getElementById('divdoc3').style.display == "") {
         if (document.getElementById('DocDesc3').value == '') {
             alert("Enter Document Description");
             return false;
         }
         else if (document.getElementById('FileUpload3').value == "") {
             alert("Please Choose File");
             return false;
         }
         else if (document.getElementById('FileUpload3').value != "") {
             if(checkExtension('FileUpload3'))
                 return false;
         }
     }
     if (document.getElementById('divdoc4').style.display == "") {
         if (document.getElementById('DocDesc4').value == '') {
             alert("Enter Document Description");
             return false;
         }
         else if (document.getElementById('FileUpload4').value == "") {
             alert("Please Choose File");
             return false;
         }
         else if (document.getElementById('FileUpload4').value != "") {
             if(checkExtension('FileUpload4'))
                 return false;
         }
     }
     if (document.getElementById('divdoc5').style.display == "") {
         if (document.getElementById('DocDesc5').value == '') {
             alert("Enter Document Description");
             return false;
         }
         else if (document.getElementById('FileUpload5').value == "") {
             alert("Please Choose File");
             return false;
         }
         else if (document.getElementById('FileUpload5').value != "") {
             if (!checkExtension('FileUpload5'))
                 return false;
         }
     }
 }
 function checkExtension(cntrlID) {
     var extension = document.getElementById(cntrlID).value.split('.').pop().toLowerCase();
         if ($.inArray(extension, ['gif', 'png', 'jpg', 'jpeg', 'bmp','xls','docx','rar','txt','doc','pdf','zip','xlsx']) == -1) {
             alert("Please Choose only valid extention file");
             document.getElementById(cntrlID).value = "";
             return false;
         }
         return true;
 }
 

 function ValidateForget() {
     if (document.getElementById('txtPassword').value == '') {
         Errormsg("txtPassword");
         $("#errorPassword").html("Enter new password");
         return false;
     }
     else {
         document.getElementById('txtPassword').style.borderColor = "";
         $("#errorPassword").html("");
     }

     if (document.getElementById('txtRetypePassword').value == '') {
         Errormsg("txtRetypePassword");
         $("#errorRetypePassword").html("Retype new password");
         return false;
     }
     else {
         document.getElementById('txtRetypePassword').style.borderColor = "";
         $("#errorRetypePassword").html("");
     }

     var pwd = document.getElementById("txtPassword").value;
     var cnfrmPwd = document.getElementById("txtRetypePassword").value;
     if (pwd != cnfrmPwd) {
         Errormsg("txtRetypePassword");
         $("#errorRetypePassword").html("Password do not match");
         return false;
     }
     else {
         document.getElementById('txtRetypePassword').style.borderColor = "";
         $("#errorRetypePassword").html("");
     }
     return true;
 }

 function ValidateBankAccDetail() {
     if (document.getElementById('txtBankName').value == '') {
         alert("Enter Bank Name");
         return false;
     }
     if (document.getElementById('txtBranch').value == '') {
         alert("Enter Branch");
         return false;
     }
     if (document.getElementById('txtAccNo').value == '') {
         alert("Enter Account Number");
         return false;
     }
     if (document.getElementById('txtAccNo').value != '') {
         if (isNaN(document.getElementById('txtAccNo').value)) {
             alert("Enter Valid Account Number");
             return false;
         }
     }
     if (document.getElementById('txtIFSCCode').value == '') {
         alert("Enter IFSC Code");
         return false;
     }
     return true;
 }

 function ValidateRegistrationDubai() {
     if (document.getElementById('txtfname').value == '') {
         Errormsg("txtfname");
         //alert("Enter First Name");
         return false;
     }
     else
         document.getElementById('txtfname').style.borderColor = "";
     if (document.getElementById('txtlname').value == '') {
         Errormsg("txtlname");
         //alert("Enter Last Name");
         return false;
     }
     else
         document.getElementById('txtlname').style.borderColor = "";
     if (document.getElementById('txtagencyName').value == '') {
         Errormsg("txtagencyName");
         // alert("Enter Agency Name");
         return false;
     }
     else
         document.getElementById('txtagencyName').style.borderColor = "";
     if (document.getElementById('txtmobile').value == '') {
         Errormsg("txtmobile");
         // alert("Enter Mobile Number");
         return false;
     }
     else
         document.getElementById('txtmobile').style.borderColor = "";
     if (document.getElementById('txtmobile').value.length < 10 || document.getElementById('txtmobile').value.length > 10 || isNaN(document.getElementById('txtmobile').value)) {
         Errormsg("txtmobile");
         alert("Please enter 10 digit mobile number");
         return false;
     }
     else
         document.getElementById('txtmobile').style.borderColor = "";
     if (document.getElementById('txtemail').value == '') {
         Errormsg("txtemail");
         // alert("Enter Agency EmailID");
         return false;
     }
     else
         document.getElementById('txtemail').style.borderColor = "";
     var pattern = new RegExp(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$/);
     if (!pattern.test(document.getElementById('txtemail').value)) {
         Errormsg("txtemail");
         alert(" Enter valid Email Id");
         return false;
     }
     else
         document.getElementById('txtemail').style.borderColor = "";
     if (document.getElementById('txtpass').value == '') {
         Errormsg("txtpass");
         // alert("Enter Password");
         return false;
     }
     else
         document.getElementById('txtpass').style.borderColor = "";
     var pwd = document.getElementById("txtpass").value;
     var cnfrmPwd = document.getElementById("txtcnfpass").value;
     if (pwd != cnfrmPwd) {
         Errormsg("txtcnfpass");
         alert("Passwords does not match");
         return false;
     }
     else
         document.getElementById('txtcnfpass').style.borderColor = "";
     return true;
 }

 //----------------Validate on Submit Button for Regisation Page----------------
 function ValidateAddCompany() {
     if (document.getElementById('txtCmpnyName').value == '') {
         Errormsg("txtCmpnyName");
         return false;
     }
     else
         document.getElementById('txtCmpnyName').style.borderColor = "";
     if (document.getElementById('txtAdress').value == '') {
         Errormsg("txtAdress");
         return false;
     }
     else
         document.getElementById('txtAdress').style.borderColor = "";
     if (document.getElementById('txttelphn').value != '') {
         if (document.getElementById('txtOfcPh').value.length < 6 || document.getElementById('txttelphn').value.length > 13 || isNaN(document.getElementById('txttelphn').value)) {
             Errormsg("txttelphn");
             alert("Please enter valid Office Phone Number");
             return false;
         }
         else
             document.getElementById('txttelphn').style.borderColor = "";
     }
     if (document.getElementById('txtmob').value == '') {
         Errormsg("txtmob");
         return false;
     }
     else
         document.getElementById('txtmob').style.borderColor = "";
     if (document.getElementById('txtmob').value.length < 10 || document.getElementById('txtmob').value.length > 10 || isNaN(document.getElementById('txtmob').value)) {
         Errormsg("txtmob");
         alert("Please enter 10 digit mobile number");
         return false;
     }
     else
         document.getElementById('txtmob').style.borderColor = "";

     return true;
 }
 function ValidateUploadExpenseBill() {
     var inpfile = document.getElementById('hdnCounter').value;
     if (document.getElementById('hdnCounter').value > 0) {
         for (var i = 0; i <= inpfile; i++) {
             if (document.getElementById('fuTrvlbill[' + i + ']') != null) {
                 if (document.getElementById('fuTrvlbill[' + i + ']').value == "") {
                     alert("Please select .jpg, .gif, .png, .jpeg, .bmp,.pdf extention file");
                     return false;
                 }
                 if (document.getElementById('fuTrvlbill[' + i + ']').value != "") {
                     var extension = document.getElementById('fuTrvlbill[' + i + ']').value.split('.').pop().toLowerCase();
                     if ($.inArray(extension, ['gif', 'png', 'jpg', 'jpeg', 'bmp','pdf']) == -1) {
                         alert("Kindly Choose only .jpg, .gif, .png, .jpeg, .bmp,.pdf extention file");
                         document.getElementById('fuTrvlbill[' + i + ']').value = "";
                         return false;
                     }
                 }
             }
         }
     }
     else {
         if (document.getElementById('fuTrvlbill[0]').value == "") {
             alert("Please select .jpg, .gif, .png, .jpeg, .bmp,.pdf extention file");
             return false;
         }
         if (document.getElementById('fuTrvlbill[0]').value != "") {
             var extension = document.getElementById('fuTrvlbill[0]').value.split('.').pop().toLowerCase();
             if ($.inArray(extension, ['gif', 'png', 'jpg', 'jpeg', 'bmp','pdf']) == -1) {
                 alert("Please Choose only .jpg, .gif, .png, .jpeg, .bmp,.pdf extention file");
                 document.getElementById('fuTrvlbill[0]').value = "";
                 return false;
             }
         }
     }
 }

 function ValidateBookingPayment() {
     if (document.getElementById('rdCmpnyCard').checked) {
         if (document.getElementById('CC').value == '') {
             Errormsg("CC");
             return false;
         }
         else
             document.getElementById('CC').style.borderColor = "";
         if (document.getElementById('CC').value != '') {
             if (document.getElementById('CC').value.length < 16 || document.getElementById('CC').value.length > 16 || isNaN(document.getElementById('CC').value)) {
                 Errormsg("CC");
                 alert("Please enter valid Card Number");
                 return false;
             }
             else
                 document.getElementById('CC').style.borderColor = "";
         }
         if (document.getElementById('CCN').value == '') {
             Errormsg("CCN");
             return false;
         }
         else
             document.getElementById('CCN').style.borderColor = "";
         if (document.getElementById('CCMM').value == '0') {
             alert("Select Expiry Month");
             return false;
         }
         if (document.getElementById('PAYMENT_expiryYear').value == '0') {
             alert("Select Expiry Year");
             return false;
         }

     }
     return true;
 }